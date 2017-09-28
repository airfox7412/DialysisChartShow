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

namespace Dialysis_Chart_Show.checkin
{
    public partial class Get_Drug : BaseForm
    {
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                DateField1.Text = toDay;
                GridPanelBind();
                cboTIME.Select(0);
            }
        }

        protected void GridPanelBind()
        {
            GetWeek();
            string sSQL = "";
            sSQL += "SELECT dyivl_item AS pname, dyivl_qty AS cnt FROM drug_itemlist ";
            sSQL += "WHERE dyivl_ivdate='" + toDay + "'";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        #region 取得星期
        protected void GetWeek()
        {
            DateTime date1 = DateTime.Parse(toDay);
            switch (date1.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    sWEEK.Text = "1";
                    break;
                case DayOfWeek.Tuesday:
                    sWEEK.Text = "2";
                    break;
                case DayOfWeek.Wednesday:
                    sWEEK.Text = "3";
                    break;
                case DayOfWeek.Thursday:
                    sWEEK.Text = "4";
                    break;
                case DayOfWeek.Friday:
                    sWEEK.Text = "5";
                    break;
                case DayOfWeek.Saturday:
                    sWEEK.Text = "6";
                    break;
                case DayOfWeek.Sunday:
                    sWEEK.Text = "7";
                    break;
            }
        }
        #endregion
        
        protected void cmdQuery(object sender, DirectEventArgs e)
        {
            toDay = _Get_YMD2(this.DateField1.Text);
            GridPanelBind();
            cboTIME.Select(0);
        }

        protected void cboTIME_Click(object sender, DirectEventArgs e)
        {
            sTIME.Text = Common.GetComboBoxValue(cboTIME).Trim();
            btnDetail_Click(sender, e);
            Common._NotificationShow("时段选择:" + Common.GetComboBoxText(cboTIME));
        }

        protected void btnDetail_Click(object sender, DirectEventArgs e)
        {
            bool dflag = false;
            string SerialNo = DateTime.Now.ToString("yyyyMMddhhmmss");
            string sSQL2 = "SELECT * FROM drug_itemlist ";
            sSQL2 += "WHERE dyivl_ivdate='" + toDay + "' ";
            DataTable dt2 = db.Query(sSQL2);
            if (dt2.Rows.Count > 0)
            {
                SerialNo = dt2.Rows[0]["dyivl_serialno"].ToString();
                dflag = true;
            }

            int rno = 1;
            string sSQL = "SELECT C.drg_name AS pname, count(C.drg_name) AS cnt FROM appointment_setup A ";
            sSQL += "LEFT JOIN longterm_ordermgt B ON A.apptst_patic=B.lgord_patic ";
            sSQL += "LEFT JOIN drug_list C ON B.lgord_drug=C.drg_code ";
            sSQL += "WHERE A.apptst_daytyp='" + sWEEK.Text + "' AND B.lgord_dateord<='" + toDay + "' AND B.lgord_actst='00001' ";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND A.apptst_timetyp='" + sTIME.Text + "' ";
            sSQL += "GROUP BY C.drg_name ";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                string sSQL1 = "";
                foreach (DataRow row in dt.Rows)
                {
                    sSQL2 = "SELECT * FROM drug_itemlist ";
                    sSQL2 += "WHERE dyivl_serialno='" + SerialNo + "' AND dyivl_item='" + row["pname"].ToString() + "' ";
                    dt2 = db.Query(sSQL2);
                    //if (dflag == false)
                    if (dt2.Rows.Count == 0)
                    {
                        sSQL1 += "INSERT INTO drug_itemlist (dyivl_serialno, dyivl_no, dyivl_item, dyivl_qty, right_qty, dyivl_rec, dyivl_ivdate) ";
                        sSQL1 += "VALUES('" + SerialNo + "',";
                        sSQL1 += "'" + rno.ToString() + "',";
                        sSQL1 += "'" + row["pname"].ToString() + "',";
                        sSQL1 += "'" + row["cnt"].ToString() + "',";
                        sSQL1 += "'" + row["cnt"].ToString() + "',";
                        sSQL1 += "'N',";
                        sSQL1 += "'" + toDay + "'); ";
                        rno++;
                    }
                    else
                    {
                        sSQL1 += "UPDATE drug_itemlist SET dyivl_qty='" + row["cnt"].ToString() + "', right_qty='" + row["cnt"].ToString() + "' ";
                        sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' AND dyivl_item='" + row["pname"].ToString() + "'; ";
                    }
                }
                db.Excute(sSQL1);
            }

            sSQL = "SELECT C.drg_name AS pname, count(C.drg_name) AS cnt FROM appointment_setup A ";
            sSQL += "LEFT JOIN shortterm_ordermgt B ON A.apptst_patic=B.shord_patic ";
            sSQL += "LEFT JOIN drug_list C ON B.shord_drug=C.drg_code ";
            sSQL += "WHERE A.apptst_daytyp='" + sWEEK.Text + "' AND B.shord_dateord='" + toDay + "' AND B.shord_actst='00001' ";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND A.apptst_timetyp='" + sTIME.Text + "' ";
            sSQL += "GROUP BY C.drg_name ";
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                string sSQL1 = "";
                foreach (DataRow row in dt.Rows)
                {
                    if (dflag == false)
                    {
                        sSQL1 += "INSERT INTO drug_itemlist (dyivl_serialno, dyivl_no, dyivl_item, dyivl_qty, right_qty, dyivl_rec, dyivl_ivdate) ";
                        sSQL1 += "VALUES('" + SerialNo + "',";
                        sSQL1 += "'" + rno.ToString() + "',";
                        sSQL1 += "'" + row["pname"].ToString() + "',";
                        sSQL1 += "'" + row["cnt"].ToString() + "',";
                        sSQL1 += "'" + row["cnt"].ToString() + "',";
                        sSQL1 += "'N',";
                        sSQL1 += "'" + toDay + "'); ";
                        rno++;
                    }
                    else
                    {
                        sSQL1 += "UPDATE drug_itemlist SET dyivl_qty='" + row["cnt"].ToString() + "', right_qty='" + row["cnt"].ToString() + "' ";
                        sSQL1 += "WHERE dyivl_item='" + row["pname"].ToString() + "'; ";
                    }
                }
                db.Excute(sSQL1);
            }
            GridPanelBind();
        }

        #region 列印
        protected void btnPrint_Click(object sender, DirectEventArgs e)
        {
            btnDetail_Click(sender, e);
            toDay = DateTime.Now.ToString("yyyy-MM-dd");
            string seldate = _Get_YMD2(this.DateField1.Text);
            string sSQL2 = "SELECT * FROM drug_itemlist ";
            sSQL2 += "WHERE dyivl_ivdate='" + toDay + "' ";
            DataTable dt2 = db.Query(sSQL2);
            string SerialNo="";
            if (dt2.Rows.Count > 0)
            {
                SerialNo = dt2.Rows[0]["dyivl_serialno"].ToString();
            }

            PrintWindow.Show();
            PrintWindow.Loader.SuspendScripting();
            PrintWindow.Loader.Url = "../report/Report_Dialysis_h.aspx?_INFO_DATE=" + seldate + "&_REPORT_NAME=s05&_REPORT_P=" + SerialNo;
            PrintWindow.Loader.DisableCaching = true;
            PrintWindow.LoadContent();
        }
        #endregion
    }
}