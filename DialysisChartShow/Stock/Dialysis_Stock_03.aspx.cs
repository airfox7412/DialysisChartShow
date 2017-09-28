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
    public partial class Dialysis_Stock_03 : BaseForm
    {
        public class InvReturnModel {
            public string dyivl_no;
            public string dyivl_id;
            public string dyivl_item;
            public int dyivl_qty;
            public int right_qty;
            public string dyivl_serialno;
            public string dyivl_ivdate;
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

        protected void btnSearch_Click(object sender, DirectEventArgs e)
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

        protected void LoadRight(string serialNo) {
            string sSQL = "SELECT dyivl_id, dyivl_no, dyivl_item, dyivl_qty, right_qty, dyivl_serialno, dyivl_ivdate FROM dailyiv_itemlist ";
            sSQL += "WHERE dyivl_serialno='" + serialNo + "';";
            DataTable dt = db.Query(sSQL);
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
                LoadRight(serialno);
                SNO.Text = serialno;
                RPT_DATE.Text = ivdate;
            }
        }

        protected void BtnSaveRight_Click(object sender, DirectEventArgs e) {
            ChangeRecords<InvReturnModel> recs = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<InvReturnModel>();
            string dateString = DateTime.Now.ToString("yyyy-MM-dd");
            try
            {
                string sSQL = "";
                foreach (InvReturnModel curItem in recs.Updated)
                {
                    sSQL += "UPDATE dailyiv_itemlist SET ";
                    sSQL += "right_qty='" + curItem.right_qty + "' ";
                    sSQL += "WHERE dyivl_id=" + curItem.dyivl_id + " AND dyivl_rec='N'; ";
                }
                db.Excute(sSQL);
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow(ex.Message.ToString());
            }

            LoadRight(SNO.Text);
        }

        #region 列印
        protected void btnPrint_Click(object sender, DirectEventArgs e)
        {
            BtnSaveRight_Click(sender, e);
            string sno = SNO.Text;
            string date = RPT_DATE.Text;

            if (sno != null && sno.Length > 0)
            {
                PrintWindow.Show();
                PrintWindow.Loader.SuspendScripting();
                PrintWindow.Loader.Url = "../report/Report_Dialysis_h.aspx?_INFO_DATE=" + date + "&_REPORT_NAME=s03&_REPORT_P=" + sno;
                PrintWindow.Loader.DisableCaching = true;
                PrintWindow.LoadContent();

                // update the print log
                string dateString = DateTime.Now.ToString("yyyy-MM-dd");
                string SQLT_IV_UPDATE = "UPDATE dailyiv_list SET dyiv_printdate = '{0}' WHERE dyiv_serialno = '{1}'; ";
                string sqlUpdate = string.Format(SQLT_IV_UPDATE, dateString, sno);
                db.Excute(sqlUpdate);
                //扣庫存
                string sSQL = "SELECT * FROM dailyiv_itemlist ";
                sSQL += "WHERE dyivl_serialno='" + sno + "' AND dyivl_rec='N'";
                DataTable dt = db.Query(sSQL);
                int outamt = 0;
                int invamt = 0;
                foreach (DataRow row in dt.Rows)
                {
                    string item = row["dyivl_item"].ToString();
                    int qty = int.Parse(row["right_qty"].ToString()); //正確的領用數量
                    string sql = "SELECT invs_outamt, invs_invamt FROM inventory_stock ";
                    sql += "WHERE invs_name='" + item + "'";
                    DataTable dt1 = db.Query(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        outamt = int.Parse(dt1.Rows[0]["invs_outamt"].ToString()) + qty;
                        invamt = int.Parse(dt1.Rows[0]["invs_invamt"].ToString()) - qty;
                        try
                        {
                            sql = "UPDATE inventory_stock SET ";
                            sql += "invs_outamt='" + outamt + "',";
                            sql += "invs_invamt='" + invamt + "',";
                            sql += "invs_lastupdate='" + date + "' ";
                            sql += "WHERE invs_name='" + item + "'; ";

                            sql += "UPDATE dailyiv_itemlist SET ";
                            sql += "dyivl_rec='Y', ";
                            sql += "dyivl_qty=right_qty ";
                            sql += "WHERE dyivl_serialno='" + sno + "'; ";
                            db.Excute(sql);
                        }
                        catch (Exception ex)
                        {
                            _NotificationShow("储存失败!");
                        }
                    }
                }
                //扣庫存
            }
            else
            {
                _NotificationShow("请选择要列印的项目。");
            }
        }
        #endregion
    }
}