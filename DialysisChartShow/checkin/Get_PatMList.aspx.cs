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
    public partial class Get_PatMList : BaseForm
    {
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");
        string SerialNo = "";
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                DateField1.Text = toDay;

                /* 每位病患的材料统计 */
                GridPanelBind2();
                cboTIME.Select(0);
            }
        }

        /* 药品清册(总量) */
        protected void SetTotalMaterial()
        {
            toDay = _Get_YMD2(this.DateField1.Text);
            SerialNo = toDay.Replace("-", "") + DateTime.Now.ToString("hhmmss");
            GetWeek();
            string sSQL1 = "SELECT * FROM dailyiv_itemlist ";
            sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' ";
            DataTable dt1 = db.Query(sSQL1);
            if (dt1.Rows.Count == 0)
            {
                string sSQL = "SELECT hp2_name AS pname, count(pif_hpack2) AS cnt FROM appointment_setup as A, pat_info as B, hpack2_setup C ";
                sSQL += "WHERE A.apptst_patic = B.pif_ic AND B.pif_hpack2 = C.hp2_code AND A.apptst_daytyp='" + sWEEK.Text + "' AND  LENGTH(pif_hpack2) > 1 AND c.hp2_status='Y' GROUP BY pif_hpack2 ";
                sSQL += "UNION ";
                sSQL += "SELECT hp3_name AS pname, count(pif_hpack3) AS cnt FROM appointment_setup as A, pat_info as B, hpack3_setup C ";
                sSQL += "WHERE A.apptst_patic = B.pif_ic AND B.pif_hpack3 = C.hp3_code AND A.apptst_daytyp='" + sWEEK.Text + "' AND  LENGTH(pif_hpack3) > 1 AND c.hp3_status='Y' GROUP BY pif_hpack3 ";
                sSQL += "UNION ";
                sSQL += "SELECT hps1_name AS pname, count(pif_hpacks1) AS cnt FROM appointment_setup as A, pat_info as B, hpacks1_setup C ";
                sSQL += "WHERE A.apptst_patic = B.pif_ic AND B.pif_hpacks1 = C.hps1_code AND A.apptst_daytyp='" + sWEEK.Text + "' AND  LENGTH(pif_hpacks1) > 1 AND c.hps1_status='Y' GROUP BY pif_hpacks1 ";
                sSQL += "UNION ";
                sSQL += "SELECT pdet_itemnm AS pname,SUM(pdet_qty) AS cnt FROM package_detail2 pd2, ";
                sSQL += "(SELECT pif_hpack AS pck_code FROM `appointment_setup` as A, pat_info as B ";
                sSQL += "WHERE A.apptst_patic = B.pif_ic AND A.apptst_daytyp='" + sWEEK.Text + "' AND  LENGTH(pif_hpack) > 1 ) ps ";
                sSQL += "WHERE ps.pck_code = pd2.pdet_code GROUP BY pdet_itemnm";
                DataTable dt = db.Query(sSQL);
                int r = 1;
                sSQL = "";
                foreach (DataRow row in dt.Rows)
                {
                    sSQL += "INSERT INTO dailyiv_itemlist (dyivl_serialno, dyivl_no, dyivl_item, dyivl_qty, dyivl_rec, dyivl_ivdate) ";
                    sSQL += "VALUES('" + SerialNo + "',";
                    sSQL += "'" + r.ToString() + "',";
                    sSQL += "'" + row["pname"].ToString() + "',";
                    sSQL += "'" + row["cnt"].ToString() + "',";
                    sSQL += "'N',";
                    sSQL += "'" + toDay + "'); ";
                    r++;
                }
                db.Excute(sSQL);
            }
        }

        /* 每位病患的材料统计 */
        protected void GridPanelBind2()
        {
            toDay = _Get_YMD2(this.DateField1.Text);
            string sSQL = "SELECT a.ivpl_id, a.ivpl_date, a.ivpl_serialno, a.ivpl_patname, a.ivpl_iv1, a.ivpl_iv2, a.ivpl_iv3, a.ivpl_iv4, a.ivpl_bedno, a.ivpl_mtyp, a.ivpl_flr, a.ivpl_time, ";
            sSQL += "CASE a.ivpl_time when '001' THEN '上午' when '002' THEN '下午' when '003' THEN '晚班' end AS timename, ";
            sSQL += "a.ivpl_iv5 AS Dialyzer, a.ivpl_iv6 AS Tube, a.ivpl_ivs1 AS Special, ";
            sSQL += "a.ivpl_col5, a.ivpl_col9 ";
            sSQL += "FROM ivpat_list a ";
            sSQL += "WHERE a.ivpl_date='" + toDay + "' ";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND a.ivpl_time='" + sTIME.Text + "' ";
            sSQL += "ORDER BY a.ivpl_bedno";
            DataTable dt = db.Query(sSQL);
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
            GridPanelBind2();
            cboTIME.Select(0);
        }

        protected void btnDetail_Click(object sender, DirectEventArgs e)
        {
            SetTotalMaterial();
            toDay = _Get_YMD2(this.DateField1.Text);
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
            if (dt.Rows.Count > 0)
                UpdatePatMList(SerialNo);
            else
                GetPatMList(SerialNo); //產生病患透析材料清單

            #region 臨時排班
            sSQL1 = "";
            sSQL = "SELECT a.ah_timetyp, a.ah_flr, a.ah_bed, a.ah_mactyp, b.pif_name, a.ah_patic, c.hp2_name, d.hp3_name, e.hps1_name FROM appointment_change a ";
            sSQL += "LEFT JOIN pat_info b ON a.ah_patic=b.pif_ic ";
            sSQL += "LEFT JOIN hpack2_setup c ON b.pif_hpack2=c.hp2_code AND c.hp2_status='Y' ";
            sSQL += "LEFT JOIN hpack3_setup d ON b.pif_hpack3=d.hp3_code AND d.hp3_status='Y' ";
            sSQL += "LEFT JOIN hpacks1_setup e ON b.pif_hpacks1=e.hps1_code AND e.hps1_status='Y' ";
            sSQL += "WHERE a.ah_date='" + toDay + "' ";
            sSQL += "ORDER BY a.ah_bed ";
            dt = db.Query(sSQL);
            foreach (DataRow row1 in dt.Rows)
            {
                string patic = row1["ah_patic"].ToString();
                string patname = row1["pif_name"].ToString();
                string bedno = row1["ah_bed"].ToString();
                string iv1 = "", iv2 = "", iv3 = "", iv4 = "";
                string mtyp = row1["ah_mactyp"].ToString();
                string iv5 = row1["hp2_name"].ToString();
                string iv6 = row1["hp3_name"].ToString();
                string col5 = "";
                string col9 = "";
                string flr = row1["ah_flr"].ToString();
                string time = row1["ah_timetyp"].ToString();
                string ivs1 = row1["hps1_name"].ToString();

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

                SQL2 = "SELECT * FROM clinical1_doc_henan ";
                SQL2 += "WHERE cln1_patic='" + patic + "' AND cln1_diadate='" + toDay + "' ";
                dt2 = db.Query(SQL2);
                if (dt2.Rows.Count == 0)
                {
                    SQL2 = "SELECT * FROM clinical1_doc_henan ";
                    SQL2 += "WHERE cln1_patic='" + patic + "' AND cln1_diadate='base' ";
                    dt2 = db.Query(SQL2);
                    if (dt2.Rows.Count > 0)
                    {
                        iv5 = dt2.Rows[0]["cln1_col2"].ToString();
                        mtyp = dt2.Rows[0]["cln1_col3"].ToString();
                        col5 = dt2.Rows[0]["cln1_col5"].ToString();
                        col9 = dt2.Rows[0]["cln1_col9"].ToString();
                    }
                    else
                    {
                        Common._NotificationShow(patname + ": 没有处方纪录");
                    }
                }
                else
                {
                    iv5 = dt2.Rows[0]["cln1_col2"].ToString();
                    mtyp = dt2.Rows[0]["cln1_col3"].ToString();
                    col5 = dt2.Rows[0]["cln1_col5"].ToString();
                    col9 = dt2.Rows[0]["cln1_col9"].ToString();
                }

                sSQL1 += "UPDATE ivpat_list SET ";
                sSQL1 += "ivpl_patic='" + patic + "',";
                sSQL1 += "ivpl_patname='" + patname + "',";
                sSQL1 += "ivpl_iv1='" + iv1 + "',";
                sSQL1 += "ivpl_iv2='" + iv2 + "',";
                sSQL1 += "ivpl_iv3='" + iv3 + "',";
                sSQL1 += "ivpl_iv4='" + iv4 + "',";
                sSQL1 += "ivpl_iv5='" + iv5 + "',";
                sSQL1 += "ivpl_iv6='" + iv6 + "',";
                sSQL1 += "ivpl_mtyp='" + mtyp + "',";
                sSQL1 += "ivpl_ivs1='" + ivs1 + "',"; //特殊材料
                sSQL1 += "ivpl_col5='" + col5 + "',"; //肝素类型
                sSQL1 += "ivpl_col9='" + col9 + "' "; //肝素量
                sSQL1 += "WHERE ivpl_serialno='" + SerialNo + "' AND ivpl_flr='" + flr + "' AND ivpl_bedno='" + bedno + "' AND ivpl_time='" + time + "'; ";

                SQL2 = "SELECT ivpl_bedno FROM ivpat_list ";
                SQL2 += "WHERE ivpl_serialno='" + SerialNo + "' AND ivpl_flr='" + flr + "' AND ivpl_bedno='" + bedno + "' AND ivpl_time='" + time + "'; ";
                dt2 = db.Query(SQL2);
                if (dt2.Rows.Count == 0)
                {
                    if (patic + patname != "")
                    {
                        sSQL1 += "INSERT INTO ivpat_list ";
                        sSQL1 += "(ivpl_date, ivpl_serialno, ivpl_patic, ivpl_patname, ivpl_iv1, ivpl_iv2, ivpl_iv3, ivpl_iv4, ivpl_iv5, ivpl_iv6, ivpl_bedno, ivpl_mtyp, ivpl_flr, ivpl_time, ivpl_ivs1, ivpl_col5, ivpl_col9) ";
                        sSQL1 += "VALUES('" + toDay + "',";
                        sSQL1 += "'" + SerialNo + "',";
                        sSQL1 += "'" + patic + "',";
                        sSQL1 += "'" + patname + "',";
                        sSQL1 += "'" + iv1 + "',";
                        sSQL1 += "'" + iv2 + "',";
                        sSQL1 += "'" + iv3 + "',";
                        sSQL1 += "'" + iv4 + "',";
                        sSQL1 += "'" + iv5 + "',";
                        sSQL1 += "'" + iv6 + "',";
                        sSQL1 += "'" + bedno + "',";
                        sSQL1 += "'" + mtyp + "',";
                        sSQL1 += "'" + flr + "',";
                        sSQL1 += "'" + time + "',";
                        sSQL1 += "'" + ivs1 + "',"; //特殊材料
                        sSQL1 += "'" + col5 + "',"; //肝素类型
                        sSQL1 += "'" + col9 + "'); "; //肝素量
                    }
                }
                else
                {
                    if (patic + patname == "")
                    {
                        sSQL1 += "DELETE FROM ivpat_list ";
                        sSQL1 += "WHERE ivpl_serialno='" + SerialNo + "' AND ivpl_flr='" + flr + "' AND ivpl_bedno='" + bedno + "' AND ivpl_time='" + time + "'; ";
                    }
                }
            }
            db.Excute(sSQL1);
            #endregion
            UpdateMList();

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

            GridPanelBind2(); //顯示病患透析材料清單
        }

        #region 產生病患透析材料清單
        private bool GetPatMList(string Sno)
        {
            try
            {
                string sSQL1 = "";
                string sSQL = "SELECT a.apptst_timetyp, a.apptst_flr, a.apptst_bed, a.apptst_mactyp, b.pif_name, a.apptst_patic, c.hp2_name, d.hp3_name, e.hps1_name FROM appointment_setup a ";
                sSQL += "LEFT JOIN pat_info b ON a.apptst_patic=b.pif_ic ";
                sSQL += "LEFT JOIN hpack2_setup c ON b.pif_hpack2=c.hp2_code AND c.hp2_status='Y' ";
                sSQL += "LEFT JOIN hpack3_setup d ON b.pif_hpack3=d.hp3_code AND d.hp3_status='Y' ";
                sSQL += "LEFT JOIN hpacks1_setup e ON b.pif_hpacks1=e.hps1_code AND e.hps1_status='Y' ";
                sSQL += "WHERE a.apptst_daytyp='" + sWEEK.Text + "' ";
                sSQL += "ORDER BY a.apptst_bed ";
                DataTable dt = db.Query(sSQL);
                foreach (DataRow row in dt.Rows)
                {
                    string patic = row["apptst_patic"].ToString();
                    string patname = row["pif_name"].ToString();
                    string bedno = row["apptst_bed"].ToString();
                    string iv1 = "", iv2 = "", iv3 = "", iv4 = "";
                    string mtyp = row["apptst_mactyp"].ToString();
                    string iv5 = row["hp2_name"].ToString();
                    string iv6 = row["hp3_name"].ToString();
                    //string mtyp = "";
                    //string iv5 = "";
                    string col5 = "";
                    string col9 = "";
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

                    SQL2 = "SELECT * FROM clinical1_doc_henan ";
                    SQL2 += "WHERE cln1_patic='" + patic + "' AND cln1_diadate='" + toDay + "' ";
                    dt2 = db.Query(SQL2);
                    if (dt2.Rows.Count == 0)
                    {
                        SQL2 = "SELECT * FROM clinical1_doc_henan ";
                        SQL2 += "WHERE cln1_patic='" + patic + "' AND cln1_diadate='base' ";
                        dt2 = db.Query(SQL2);
                        if (dt2.Rows.Count > 0)
                        {
                            iv5 = dt2.Rows[0]["cln1_col2"].ToString();
                            mtyp = dt2.Rows[0]["cln1_col3"].ToString();
                            col5 = dt2.Rows[0]["cln1_col5"].ToString();
                            col9 = dt2.Rows[0]["cln1_col9"].ToString();
                        }
                        else
                        {
                            Common._NotificationShow(patname + ": 没有处方纪录");
                        }
                    }
                    else
                    {
                        iv5 = dt2.Rows[0]["cln1_col2"].ToString();
                        mtyp = dt2.Rows[0]["cln1_col3"].ToString();
                        col5 = dt2.Rows[0]["cln1_col5"].ToString();
                        col9 = dt2.Rows[0]["cln1_col9"].ToString();
                    }

                    sSQL1 += "INSERT INTO ivpat_list ";
                    sSQL1 += "(ivpl_date, ivpl_serialno, ivpl_patic, ivpl_patname, ivpl_iv1, ivpl_iv2, ivpl_iv3, ivpl_iv4, ivpl_iv5, ivpl_iv6, ivpl_bedno, ivpl_mtyp, ivpl_flr, ivpl_time, ivpl_ivs1, ivpl_col5, ivpl_col9) ";
                    sSQL1 += "VALUES('" + toDay + "',";
                    sSQL1 += "'" + Sno + "',";
                    sSQL1 += "'" + patic + "',";
                    sSQL1 += "'" + patname + "',";
                    sSQL1 += "'" + iv1 + "',";
                    sSQL1 += "'" + iv2 + "',";
                    sSQL1 += "'" + iv3 + "',";
                    sSQL1 += "'" + iv4 + "',";
                    sSQL1 += "'" + iv5 + "',";
                    sSQL1 += "'" + iv6 + "',";
                    sSQL1 += "'" + bedno + "',";
                    sSQL1 += "'" + mtyp + "',";
                    sSQL1 += "'" + flr + "',";
                    sSQL1 += "'" + time + "',";
                    sSQL1 += "'" + ivs1 + "',"; //特殊材料
                    sSQL1 += "'" + col5 + "',"; //肝素类型
                    sSQL1 += "'" + col9 + "'); "; //肝素量
                }
                db.Excute(sSQL1);
                return true;
            }
            catch(Exception ex)
            {
                return false;
            }
        }
        #endregion

        #region 更新病患透析材料清單
        private bool UpdatePatMList(string Sno)
        {
            try
            {
                string sSQL1 = "";
                string sSQL = "SELECT a.apptst_timetyp, a.apptst_flr, a.apptst_bed, a.apptst_mactyp, b.pif_name, a.apptst_patic, c.hp2_name, d.hp3_name, e.hps1_name FROM appointment_setup a ";
                sSQL += "LEFT JOIN pat_info b ON a.apptst_patic=b.pif_ic ";
                sSQL += "LEFT JOIN hpack2_setup c ON b.pif_hpack2=c.hp2_code AND c.hp2_status='Y' ";
                sSQL += "LEFT JOIN hpack3_setup d ON b.pif_hpack3=d.hp3_code AND d.hp3_status='Y' ";
                sSQL += "LEFT JOIN hpacks1_setup e ON b.pif_hpacks1=e.hps1_code AND e.hps1_status='Y' ";
                sSQL += "WHERE a.apptst_daytyp='" + sWEEK.Text + "' ";
                sSQL += "ORDER BY a.apptst_bed ";
                DataTable dt = db.Query(sSQL);
                foreach (DataRow row in dt.Rows)
                {
                    string patic = row["apptst_patic"].ToString();
                    string patname = row["pif_name"].ToString();
                    string bedno = row["apptst_bed"].ToString();
                    string iv1 = "", iv2 = "", iv3 = "", iv4 = "";
                    string mtyp = row["apptst_mactyp"].ToString();
                    string iv5 = row["hp2_name"].ToString();
                    string iv6 = row["hp3_name"].ToString();
                    string col5 = "";
                    string col9 = "";
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

                    SQL2 = "SELECT * FROM clinical1_doc_henan ";
                    SQL2 += "WHERE cln1_patic='" + patic + "' AND cln1_diadate='" + toDay + "' ";
                    dt2 = db.Query(SQL2);
                    if (dt2.Rows.Count == 0)
                    {
                        SQL2 = "SELECT * FROM clinical1_doc_henan ";
                        SQL2 += "WHERE cln1_patic='" + patic + "' AND cln1_diadate='base' ";
                        dt2 = db.Query(SQL2);
                        if (dt2.Rows.Count > 0)
                        {
                            iv5 = dt2.Rows[0]["cln1_col2"].ToString();
                            mtyp = dt2.Rows[0]["cln1_col3"].ToString();
                            col5 = dt2.Rows[0]["cln1_col5"].ToString();
                            col9 = dt2.Rows[0]["cln1_col9"].ToString();
                        }
                        else
                        {
                            Common._NotificationShow(patname + ": 没有处方纪录");
                        }
                    }
                    else
                    {
                        iv5 = dt2.Rows[0]["cln1_col2"].ToString();
                        mtyp = dt2.Rows[0]["cln1_col3"].ToString();
                        col5 = dt2.Rows[0]["cln1_col5"].ToString();
                        col9 = dt2.Rows[0]["cln1_col9"].ToString();
                    }
                    sSQL1 += "UPDATE ivpat_list SET ";
                    sSQL1 += "ivpl_iv1='" + iv1 + "',";
                    sSQL1 += "ivpl_iv2='" + iv2 + "',";
                    sSQL1 += "ivpl_iv3='" + iv3 + "',";
                    sSQL1 += "ivpl_iv4='" + iv4 + "',";
                    sSQL1 += "ivpl_iv5='" + iv5 + "',";
                    sSQL1 += "ivpl_iv6='" + iv6 + "',";
                    sSQL1 += "ivpl_mtyp='" + mtyp + "',";
                    sSQL1 += "ivpl_ivs1='" + ivs1 + "',"; //特殊材料
                    sSQL1 += "ivpl_col5='" + col5 + "',"; //肝素类型
                    sSQL1 += "ivpl_col9='" + col9 + "' "; //肝素量
                    sSQL1 += "WHERE ivpl_serialno='" + Sno + "' AND ivpl_patic='" + patic + "'; ";
                }
                db.Excute(sSQL1);                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
        #endregion

        protected void cboTIME_Click(object sender, DirectEventArgs e)
        {
            sTIME.Text = Common.GetComboBoxValue(cboTIME).Trim();
            Common._NotificationShow("时段选择:" + Common.GetComboBoxText(cboTIME));//更新血透機數量
            UpdateMList();
            GridPanelBind2();
        }

        #region 列印
        protected void btnPrint_Click(object sender, DirectEventArgs e)
        {
            PrintWindow.Show();
            PrintWindow.Loader.SuspendScripting();
            PrintWindow.Loader.Url = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=s01&_INFO_DATE=" + _Get_YMD2(DateField1.Text) + "&_REPORT_P=" + sTIME.Text;
            PrintWindow.Loader.DisableCaching = true;
            PrintWindow.LoadContent();
        }
        #endregion

        #region 更新耗材統計數量
        private void UpdateMList()
        {
            //更新血透機數量
            string sSQL = "SELECT ivpl_iv5, count(ivpl_iv5) AS cnt FROM ivpat_list ";
            sSQL += "WHERE ivpl_date='" + toDay + "' ";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND ivpl_time='" + sTIME.Text + "' ";
            sSQL += "GROUP BY ivpl_iv5";
            DataTable dt = db.Query(sSQL);
            string sSQL1 = "";
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    sSQL1 += "UPDATE dailyiv_itemlist SET ";
                    sSQL1 += "dyivl_qty='" + row1["cnt"].ToString() + "' ";
                    sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' AND dyivl_item='" + row1["ivpl_iv5"].ToString() + "'; ";
                }
            }
            else
            {
                sSQL1 += "UPDATE dailyiv_itemlist SET dyivl_qty='' ";
                sSQL1 += "WHERE dyivl_ivdate='" + toDay + "'; ";
            }
            //管路型號
            sSQL = "SELECT ivpl_iv6, count(ivpl_iv6) AS cnt FROM ivpat_list ";
            sSQL += "WHERE ivpl_date='" + toDay + "' ";
            if (!string.IsNullOrEmpty(sTIME.Text))
                sSQL += "AND ivpl_time='" + sTIME.Text + "' ";
            sSQL += "GROUP BY ivpl_iv6";
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row1 in dt.Rows)
                {
                    sSQL1 += "UPDATE dailyiv_itemlist SET ";
                    sSQL1 += "dyivl_qty='" + row1["cnt"].ToString() + "' ";
                    sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' AND dyivl_item='" + row1["ivpl_iv6"].ToString() + "'; ";
                }
            }
            db.Excute(sSQL1);

            //更新耗材數量
            //sSQL = "SELECT sum(ivpl_iv1) AS cnt1, sum(ivpl_iv2) AS cnt2, sum(ivpl_iv3) AS cnt3, sum(ivpl_iv4) AS cnt4 FROM ivpat_list ";
            //sSQL += "WHERE ivpl_date='" + toDay + "' ";
            //if (!string.IsNullOrEmpty(sTIME.Text))
            //    sSQL += "AND ivpl_time='" + sTIME.Text + "' ";
            //dt = db.Query(sSQL);
            //if (dt.Rows.Count > 0)
            //{
            //    sSQL1 = "UPDATE dailyiv_itemlist SET dyivl_qty='" + dt.Rows[0]["cnt1"].ToString() + "' ";
            //    sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' AND dyivl_item='穿刺针'; ";
            //    sSQL1 += "UPDATE dailyiv_itemlist SET dyivl_qty='" + dt.Rows[0]["cnt2"].ToString() + "' ";
            //    sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' AND dyivl_item='护理包'; ";
            //    sSQL1 += "UPDATE dailyiv_itemlist SET dyivl_qty='" + dt.Rows[0]["cnt3"].ToString() + "' ";
            //    sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' AND dyivl_item='敷贴'; ";
            //    sSQL1 += "UPDATE dailyiv_itemlist SET dyivl_qty='" + dt.Rows[0]["cnt4"].ToString() + "' ";
            //    sSQL1 += "WHERE dyivl_ivdate='" + toDay + "' AND dyivl_item='肝素帽'; ";
            //    db.Excute(sSQL1);
            //}
        }
        #endregion
    }
}