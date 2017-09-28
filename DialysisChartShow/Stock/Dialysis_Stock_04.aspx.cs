using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using Dialysis_Chart_Show.tools;
using Ext.Net;

namespace Dialysis_Chart_Show.Stock
{
    public partial class Dialysis_Stock_04 : BaseForm
    {
        public class InvReturnModel {
            public string No;
            public string Id;
            public string Name;
            public int PreAmt;
            public int RetAmt;
            public string SerialNo;
            public string DateReturn;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateField1.Text = String.Format("{0:yyyy-MM-dd}", FirstDay);
                DateField2.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                GridPanelBind();
            }
        }

        protected void BtnSearch_Click(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT dyiv_serialno, dyiv_usrnm, dyiv_printdate, dyiv_ivdate FROM dailyiv_list ";
            sSQL += "WHERE dyiv_ivdate BETWEEN '" + _Get_YMD2(DateField1.Text) + "' AND '" + _Get_YMD2(DateField2.Text) + "' ";
            sSQL += "ORDER BY dyiv_ivdate DESC";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        protected void LoadInvReturn(string serialNo) {
            string SQLT_RETURN_SEL = @"
                    SELECT invr_no, invr_id, invr_name, invr_preamt, invr_rtnamt, invr_serialno, invr_datereturn
                    FROM inv_return
                    WHERE invr_serialno = '{0}';";
            string SQLT_ITEMLIST_SEL = @"
                    SELECT dyivl_no as invr_no,
                        null as invr_id,
                        dyivl_item as invr_name,
                        dyivl_qty as invr_preamt,
                        null as invr_rtnamt,
                        dyivl_serialno as invr_serialno,
                        null as invr_datereturn
                    FROM dailyiv_itemlist
                    WHERE dyivl_serialno = '{0}'
                    ORDER BY dyivl_id;";

            string sql = string.Format(SQLT_RETURN_SEL, serialNo);

            DataTable dt = db.Query(sql);

            // we have to generate the return item list from dailyiv_itemlist if not exist in inv_return
            if (dt.Rows.Count == 0) {
                sql = string.Format(SQLT_ITEMLIST_SEL, serialNo);
                dt = db.Query(sql);
            }

            Store istore = this.GridPanel2.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void RowSelect(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            string serialno = selRow[0]["dyiv_serialno"].ToString();
            string usrnm = selRow[0]["dyiv_usrnm"].ToString();
            string printdate = selRow[0]["dyiv_printdate"];
            string ivdate = selRow[0]["dyiv_ivdate"];

            if (serialno != "") {
                LoadInvReturn(serialno);
            }
        }

        #region 列印
        protected void BtnPrint_Click(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["SelectedRow"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);
            ChangeRecords<InvReturnModel> recs = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<InvReturnModel>();
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");

            if (selRow.Count() > 0)
            {
                dateString = selRow[0]["dyiv_ivdate"];
            }

            // 原则上这样的行为只会有 insert.
            string SQLT_INV_RETURN_UPDATE = @"
                UPDATE inv_return
                SET invr_rtnamt='{0}',
                    invr_datereturn='{1}'
                WHERE invr_id = '{2}'; ";
            string SQLT_INV_RETURN_INSERT = @"
                INSERT INTO inv_return (invr_no, invr_name, invr_preamt, invr_rtnamt, invr_serialno, invr_datereturn)
                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}'); ";

            // inserting or update?
            try {
                if (recs.Updated.Count > 0) {
                    int id;
                    string sql = "";
                    if (int.TryParse(recs.Updated[0].Id, out id))
                    {
                        foreach (InvReturnModel curItem in recs.Updated)
                        {
                            sql += string.Format(SQLT_INV_RETURN_UPDATE,
                                curItem.RetAmt,
                                dateString,
                                curItem.Id);
                        }
                    }
                    else
                    {
                        foreach (InvReturnModel curItem in recs.Updated)
                        {
                            sql += string.Format(SQLT_INV_RETURN_INSERT,
                                curItem.No,
                                curItem.Name,
                                curItem.PreAmt,
                                curItem.RetAmt,
                                curItem.SerialNo,
                                dateString);
                        }
                        db.Excute(sql);
                    }
                }

                if (selRow.Count() > 0) {
                    string serialno = selRow[0]["dyiv_serialno"].ToString();
                    string usrnm = selRow[0]["dyiv_usrnm"].ToString();
                    string printdate = selRow[0]["dyiv_printdate"];
                    string ivdate = selRow[0]["dyiv_ivdate"];

                    PrintWindow.Show();
                    PrintWindow.Loader.SuspendScripting();
                    PrintWindow.Loader.Url = "../report/Report_Dialysis_h.aspx?_INFO_DATE=" + ivdate + "&_REPORT_NAME=s04&_REPORT_P=" + serialno;
                    PrintWindow.Loader.DisableCaching = true;
                    PrintWindow.LoadContent();

                    //入庫存
                    string sSQL = "SELECT * FROM inv_return ";
                    sSQL += "WHERE invr_serialno='" + serialno + "' AND invr_rtnamt<>'0' ";
                    DataTable dt = db.Query(sSQL);
                    int inamt = 0;
                    int invamt = 0;
                    foreach (DataRow row in dt.Rows)
                    {
                        string item = row["invr_name"].ToString();
                        int qty = int.Parse(row["invr_preamt"].ToString());
                        string sql = "SELECT invs_inamt, invs_invamt FROM inventory_stock ";
                        sql += "WHERE invs_name='" + item + "'";
                        DataTable dt1 = db.Query(sql);
                        if (dt1.Rows.Count > 0)
                        {
                            inamt = int.Parse(dt1.Rows[0]["invs_inamt"].ToString()) - qty;
                            invamt = int.Parse(dt1.Rows[0]["invs_invamt"].ToString()) + qty;
                            try
                            {
                                sql = "UPDATE inventory_stock SET ";
                                sql += "invs_inamt='" + inamt + "',";
                                sql += "invs_invamt='" + invamt + "',";
                                sql += "invs_lastupdate='" + printdate + "' ";
                                sql += "WHERE invs_name='" + item + "'; ";

                                sql += "UPDATE inv_return SET invr_rec='Y' ";
                                sql += "WHERE invr_serialno='" + serialno + "';";
                                db.Excute(sql);
                            }
                            catch (Exception ex)
                            {
                                _NotificationShow("储存失败!");
                            }
                        }
                    }
                    //入庫存
                } else {
                    _NotificationShow("请选择要列印的项目。");
                }
            } catch (Exception ex) {
                Common._ErrorMsgShow(ex.Message.ToString());
            }            
        }
        #endregion

        protected void BtnSaveReturn_Click(object sender, DirectEventArgs e) {
            ChangeRecords<InvReturnModel> recs = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<InvReturnModel>();
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            string SQLT_INV_RETURN_UPDATE = @"
                UPDATE inv_return
                SET invr_rtnamt='{0}',
                    invr_datereturn='{1}'
                WHERE invr_id = '{2}';";
            string SQLT_INV_RETURN_INSERT = @"
                INSERT INTO inv_return (invr_no, invr_name, invr_preamt, invr_rtnamt, invr_serialno, invr_datereturn)
                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}');";

            // inserting or update?
            try {
                if (recs.Updated.Count > 0) {
                    int id;
                    if (int.TryParse(recs.Updated[0].Id, out id)) {
                        foreach (InvReturnModel curItem in recs.Updated) {
                            string sql = string.Format(SQLT_INV_RETURN_UPDATE,
                                curItem.RetAmt,
                                dateString,
                                curItem.Id);

                            db.Excute(sql);
                        }
                    } else {
                        foreach (InvReturnModel curItem in recs.Updated) {
                            string sql = string.Format(SQLT_INV_RETURN_INSERT,
                                curItem.No,
                                curItem.Name,
                                curItem.PreAmt,
                                curItem.RetAmt,
                                curItem.SerialNo,
                                dateString);

                            db.Excute(sql);
                        }
                    }
                    LoadInvReturn(recs.Updated[0].SerialNo);
                }
                
            } catch (Exception ex) {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }
    }
}