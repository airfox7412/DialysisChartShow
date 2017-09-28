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
    public partial class Get_Material : BaseForm
    {
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");
        string SerialNo = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                DateField1.Text = toDay;

                /* 药品清册(总量) */
                GridPanelBind();

                /* 每位病患的材料统计 */
                GridPanelBind2();
            }
        }

        /* 药品清册(总量) */
        protected void GridPanelBind()
        {
            GetWeek(); 
            string sSQL = "SELECT hp2_name AS pname, count(pif_hpack2) AS cnt FROM appointment_setup as A, pat_info as B, hpack2_setup C ";
            sSQL += "where A.apptst_patic = B.pif_ic and B.pif_hpack2 = C.hp2_code and A.apptst_daytyp='" + sWEEK.Text + "' and  LENGTH(pif_hpack2) > 1 AND c.hp2_status='Y'";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND A.apptst_timetyp='" + sTIME.Text + "' ";
            sSQL += "GROUP BY pif_hpack2 ";
            sSQL += "union ";
            sSQL += "SELECT hp3_name AS pname, count(pif_hpack3) AS cnt FROM appointment_setup as A, pat_info as B, hpack3_setup C ";
            sSQL += "where A.apptst_patic = B.pif_ic and B.pif_hpack3 = C.hp3_code and A.apptst_daytyp='" + sWEEK.Text + "' and  LENGTH(pif_hpack3) > 1  AND c.hp3_status='Y'";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND A.apptst_timetyp='" + sTIME.Text + "' ";
            sSQL += "GROUP BY pif_hpack3 ";
            sSQL += "union ";
            sSQL += "SELECT hps1_name AS pname, count(pif_hpacks1) AS cnt FROM appointment_setup as A, pat_info as B, hpacks1_setup C ";
            sSQL += "where A.apptst_patic = B.pif_ic and B.pif_hpacks1 = C.hps1_code and A.apptst_daytyp='" + sWEEK.Text + "' and  LENGTH(pif_hpacks1) > 1 AND c.hps1_status='Y' ";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND A.apptst_timetyp='" + sTIME.Text + "' ";
            sSQL += "GROUP BY pif_hpacks1 ";
            sSQL += "union ";
            sSQL += "SELECT pdet_itemnm AS pname,SUM(pdet_qty) AS cnt FROM package_detail2 pd2, ";
            sSQL += "(SELECT pif_hpack AS pck_code FROM appointment_setup as A, pat_info as B ";
            sSQL += "where A.apptst_patic = B.pif_ic and A.apptst_daytyp='" + sWEEK.Text + "' and  LENGTH(pif_hpack) > 1 ";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND A.apptst_timetyp='" + sTIME.Text + "' ";
            sSQL += ") ps ";
            sSQL += "where ps.pck_code = pd2.pdet_code GROUP BY pdet_itemnm";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        /* 每位病患的材料统计 */
        protected void GridPanelBind2()
        {
            string sSQL = "SELECT ivpl_id, ivpl_date, ivpl_serialno, ivpl_patname, ivpl_iv1, ivpl_iv2, ivpl_iv3, ivpl_iv4, ivpl_bedno, ivpl_mtyp, ivpl_flr, ivpl_time, ";
            sSQL += "CASE ivpl_time when '001' THEN '上午' when '002' THEN '下午' when '003' THEN '晚班' end AS timename, ";
            sSQL += "ivpl_iv5 AS Dialyzer, ivpl_iv6 AS Tube, ivpl_ivs1 AS Special ";
            sSQL += "FROM ivpat_list ";
            sSQL += "WHERE ivpl_date='" + toDay + "' ";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND ivpl_time='" + sTIME.Text + "' ";
            sSQL += "ORDER BY ivpl_bedno";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                DailySerialNo.Text = dt.Rows[0]["ivpl_serialno"].ToString();
            }
            Store istore = this.GridPanel2.GetStore();
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
            cboTIME.Text = "全部";
            GridPanelBind();
            GridPanelBind2();
        }

        protected void btnDetail_Click(object sender, DirectEventArgs e)
        {
            GetWeek();
            string sSQL1 = "SELECT dyivl_serialno FROM dailyiv_itemlist ";
            sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' ";
            sSQL1 += "GROUP BY dyivl_serialno";
            DataTable dt1 = db.Query(sSQL1);
            if (dt1.Rows.Count > 0)
            {
                SerialNo = dt1.Rows[0]["dyivl_serialno"].ToString();
            }
            sSQL1 = "";
            string sSQL = "SELECT * FROM ivpat_list WHERE ivpl_date='" + toDay + "' ";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count == 0)
            {
                sSQL = "SELECT a.apptst_timetyp, a.apptst_flr, a.apptst_bed, a.apptst_mactyp, b.pif_name, a.apptst_patic, c.hp2_name, d.hp3_name, e.hps1_name FROM appointment_setup a ";
                sSQL += "LEFT JOIN pat_info b ON a.apptst_patic=b.pif_ic ";
                sSQL += "LEFT JOIN hpack2_setup c ON b.pif_hpack2=c.hp2_code AND c.hp2_status='Y' ";
                sSQL += "LEFT JOIN hpack3_setup d ON b.pif_hpack3=d.hp3_code AND d.hp3_status='Y' ";
                sSQL += "LEFT JOIN hpacks1_setup e ON b.pif_hpacks1=e.hps1_code AND e.hps1_status='Y' ";
                sSQL += "WHERE a.apptst_daytyp='" + sWEEK.Text + "' ";
                sSQL += "ORDER BY a.apptst_bed ";
                dt = db.Query(sSQL);
                foreach (DataRow row in dt.Rows)
                {
                    string patic = row["apptst_patic"].ToString();
                    string patname = row["pif_name"].ToString();
                    string bedno = row["apptst_bed"].ToString();
                    string mtyp = row["apptst_mactyp"].ToString();
                    string iv1 = "", iv2 = "", iv3 = "", iv4 = "";
                    string iv5 = row["hp2_name"].ToString();
                    string iv6 = row["hp3_name"].ToString();
                    string flr = row["apptst_flr"].ToString();
                    string time = row["apptst_timetyp"].ToString();
                    string ivs1 = row["hps1_name"].ToString();

                    string SQL2 = "SELECT b.pdet_itemcd, b.pdet_qty FROM pat_info a ";
                    SQL2 += "LEFT JOIN package_detail2 b ON a.pif_hpack = b.pdet_code ";
                    SQL2 += "WHERE a.pif_ic='" + patic + "' ";
                    DataTable dt2 = db.Query(SQL2);
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        if (row2["pdet_itemcd"].ToString() == "00001")
                            iv1 = row2["pdet_qty"].ToString();
                        if (row2["pdet_itemcd"].ToString() == "00002")
                            iv2 = row2["pdet_qty"].ToString();
                        if (row2["pdet_itemcd"].ToString() == "00003")
                            iv3 = row2["pdet_qty"].ToString();
                        if (row2["pdet_itemcd"].ToString() == "00004")
                            iv4 = row2["pdet_qty"].ToString();
                    }
                    dt2.Dispose();

                    sSQL1 += "INSERT INTO ivpat_list ";
                    sSQL1 += "(ivpl_date, ivpl_serialno, ivpl_patname, ivpl_iv1, ivpl_iv2, ivpl_iv3, ivpl_iv4, ivpl_iv5, ivpl_iv6, ivpl_bedno, ivpl_mtyp, ivpl_flr, ivpl_time, ivpl_ivs1) ";
                    sSQL1 += "VALUES('" + toDay + "',";
                    sSQL1 += "'" + SerialNo + "',";
                    sSQL1 += "'" + patname + "',";
                    sSQL1 += "'" + iv1.ToString() + "',";
                    sSQL1 += "'" + iv2.ToString() + "',";
                    sSQL1 += "'" + iv3.ToString() + "',";
                    sSQL1 += "'" + iv4.ToString() + "',";
                    sSQL1 += "'" + iv5.ToString() + "',";
                    sSQL1 += "'" + iv6.ToString() + "',";
                    sSQL1 += "'" + bedno + "',";
                    sSQL1 += "'" + mtyp + "',";
                    sSQL1 += "'" + flr + "',";
                    sSQL1 += "'" + time + "',";
                    sSQL1 += "'" + ivs1 + "'); "; //特殊材料
                }
                db.Excute(sSQL1);
            }
            GridPanelBind2();
            sSQL = "SELECT * FROM dailyiv_list ";
            sSQL += "WHERE dyiv_ivdate='" + toDay + "'";
            dt = db.Query(sSQL);
            if (dt.Rows.Count == 0)
            {
                sSQL1 = "INSERT INTO dailyiv_list (dyiv_ivdate, dyiv_serialno, dyiv_usrnm, dyiv_printdate) ";
                sSQL1 += "VALUES('" + toDay + "',";
                sSQL1 += "'" + SerialNo + "',";
                sSQL1 += "'" + Session["USER_NAME"].ToString() + "',";
                sSQL1 += "'" + toDay + "'); ";
                sSQL1 += "UPDATE dailyiv_itemlist SET dyivl_serialno='" + SerialNo + "' ";
                sSQL1 += "WHERE dyivl_ivdate='" + toDay + "'; ";
                db.Excute(sSQL1);
            }
        }

        protected void cboTIME_Click(object sender, DirectEventArgs e)
        {
            sTIME.Text = Common.GetComboBoxValue(cboTIME).Trim();
            GridPanelBind();
            GridPanelBind2();
            Common._NotificationShow("时段选择:" + cboTIME.Text);
        }

        #region 列印
        protected void btnPrint_Click(object sender, DirectEventArgs e)
        {
            btnDetail_Click(sender, e);
            toDay = DateTime.Now.ToString("yyyy-MM-dd");
            string seldate = _Get_YMD2(this.DateField1.Text);
            X.Redirect("../report/Report_Dialysis_h.aspx?_INFO_DATE=" + seldate + "&_REPORT_NAME=s02" + "&_REPORT_P=" + cboTIME.Value.ToString());
        }
        #endregion
    }
}