using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Drawing;
using System.Web.Services;
using System.Configuration;

namespace Dialysis_Chart_Show.checkin
{
    public partial class WebForm_Test : System.Web.UI.Page
    {
        public string Myhaisv4 = ConfigurationManager.AppSettings["pat_images"].ToString() + "Myhaisv4/";
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");
        DBMysql db = new DBMysql();
        private static DataTable dtAREA;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //if (Session["USER_NAME"] == null || Session["USER_RIGHT"] == null)
                //    X.Redirect("index.aspx");
                //else
                //{
                //    if (Session["USER_RIGHT"].ToString() != "DC" && Session["USER_RIGHT"].ToString() != "DH")
                //    {
                //        Session["USER_RIGHT"] = "";
                //        X.Redirect("index.aspx");
                //    }
                //}
                txtDate.Text = toDay;
                Show_TimeSec(); //顯示時段  
                Show_FloorArea(); //顯示樓層，床區 
            }
        }

        #region 顯示時段
        private void Show_TimeSec()
        {
            Ext.Net.ListItem litem1 = new Ext.Net.ListItem("上午", "001");
            cboTIME.Items.Add(litem1);
            litem1 = new Ext.Net.ListItem("下午", "002");
            cboTIME.Items.Add(litem1);
            litem1 = new Ext.Net.ListItem("晚班", "003");
            cboTIME.Items.Add(litem1);

            DateTime now = DateTime.Now;
            if (Session["PAD_TIME"] == null)
            {
                int Hm = int.Parse(now.ToString("HHmm"));
                if (1 <= Hm && Hm <= 1159)
                {
                    sTIME.Text = "001";
                    cboTIME.Select(sTIME.Text);
                }
                else if (1200 <= Hm && Hm <= 1759)
                {
                    sTIME.Text = "002";
                    cboTIME.Select(sTIME.Text);
                }
                else if (1800 <= Hm && Hm <= 2400)
                {
                    sTIME.Text = "003";
                    cboTIME.Select(sTIME.Text);
                }
            }
            else
            {
                sTIME.Text = Session["PAD_TIME"].ToString();
                cboTIME.Select(sTIME.Text);
            }
        }
        #endregion

        #region 顯示樓層，床區
        private void Show_FloorArea()
        {
            string sql = "SELECT distinct mac_flr, mac_sec FROM mac_setup ORDER BY mac_flr, mac_sec ";
            dtAREA = db.Query(sql);
            if (dtAREA.Rows.Count > 0)
            {
                if (Session["PAD_FLOOR"] == null)
                    sFLOOR.Text = dtAREA.Rows[0]["mac_flr"].ToString();
                else
                    sFLOOR.Text = Session["PAD_FLOOR"].ToString();
            }

            sql = "SELECT distinct mac_flr FROM mac_setup ORDER BY mac_flr ";
            DataTable dtFLOOR = db.Query(sql);
            for (int i = 0; i < dtFLOOR.Rows.Count; i++)
            {
                Ext.Net.ListItem litem;
                litem = new Ext.Net.ListItem(dtFLOOR.Rows[i]["mac_flr"].ToString() + "楼", dtFLOOR.Rows[i]["mac_flr"].ToString());
                cboFLOOR.Items.Add(litem);
            }
            cboFLOOR.GetStore().DataBind();
            if (dtFLOOR.Rows.Count > 0)
            {
                cboFLOOR.Disabled = false;
                cboFLOOR.Select(sFLOOR.Text);
                FILL_AREA(sFLOOR.Text, sAREA.Text);
                FILL_BED();
            }
            else
            {
                cboFLOOR.Disabled = true;
            }
            db.myConnection.Close();
        }
        #endregion

        protected void REFRESH_BED(object sender, StoreReadDataEventArgs e)
        {
            FILL_BED();
        }

        protected void FILL_BED()
        {
            GetWeek();
            string sSQL = "";
            sSQL = "SELECT M.mac_sec AS AREA, M.mac_bedno AS BED_NO, ";
            sSQL += "REPLACE(P.pif_imgloc,'./','" + Myhaisv4 + "') AS url, ";
            sSQL += "CASE ";
            sSQL += "WHEN EXISTS(SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + txtDate.Text + "') ";
            sSQL += "THEN (SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + txtDate.Text + "') ";
            sSQL += "ELSE (SELECT cln1_col2 FROM clinical1_doc_henan WHERE cln1_patic=A.apptst_patic AND cln1_diadate='base') ";
            sSQL += "END AS MAC_MODEL, ";
            sSQL += "M.mac_typ AS MAC_TYPE, ";
            sSQL += "case UPPER(M.mac_status) when 'Y' then '正常' when 'N' then '保养中' end AS MAC_STATE, ";
            sSQL += "P.pif_name AS PERSON_NAME, A.apptst_patic AS PERSON_ID, ";
            sSQL += "case P.pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX, ";
            sSQL += "P.pif_height AS PERSON_HEIGHT, ";
            sSQL += "N.cln1_col5 AS PERSON_WEIGHT, ";
            sSQL += "case V.pv_macstat when 'A' then '开' when 'S' then '关' end as PERSON_STATE ";
            sSQL += "FROM mac_setup M ";
            sSQL += "LEFT JOIN appointment_setup A ON M.mac_flr=A.apptst_flr ";
            sSQL += "AND M.mac_sec=A.apptst_sec AND M.mac_bedno=A.apptst_bed AND A.apptst_daytyp='" + sWEEK.Text + "' AND A.apptst_timetyp='" + sTIME.Text + "' ";
            sSQL += "LEFT JOIN pat_visit V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + txtDate.Text + "' ";
            sSQL += "LEFT JOIN general_setup G ON M.mac_brand=G.genst_code AND G.genst_ctg='macbrd' ";
            sSQL += "LEFT JOIN pat_info P ON A.apptst_patic=P.pif_ic ";
            sSQL += "LEFT JOIN clinical1_nurse N ON A.apptst_patic=N.cln1_patic AND N.cln1_diadate='" + txtDate.Text + "' "; //淨化參數表格
            sSQL += "WHERE M.mac_flr='" + sFLOOR.Text + "' AND A.apptst_patic<>'' ";
            if (sAREA.Text.Trim() != "全区")
            {
                sSQL += "AND M.mac_sec='" + sAREA.Text + "' ";
            }
            sSQL += "ORDER BY M.mac_bedno  ";
            DataTable dt = db.Query(sSQL);

            //補上臨時預約
            sSQL = "SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, A.ah_bed, A.ah_timetyp, P.pif_name, ";
            sSQL += "case P.pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX, ";
            sSQL += "P.pif_height, V.pv_weight, ";
            sSQL += "case V.pv_macstat when 'A' then '开' when 'S' then '关' end as PERSON_STATE, ";
            sSQL += "N.cln1_col5 ";
            sSQL += "FROM appointment_change A ";
            sSQL += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
            sSQL += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND V.pv_datevisit='" + txtDate.Text + "' ";
            sSQL += "LEFT JOIN clinical1_nurse N ON A.ah_patic=N.cln1_patic AND N.cln1_diadate='" + txtDate.Text + "' "; //淨化參數表格
            sSQL += "WHERE ah_date='" + txtDate.Text + "' AND ah_timetyp='" + sTIME.Text + "' AND A.ah_patic<>'' ";
            if (sAREA.Text.Trim() != "全区")
            {
                sSQL += "AND M.mac_sec='" + sAREA.Text + "' ";
            }
            DataTable dt2 = db.Query(sSQL);
            System.Data.DataView dv = dt.DefaultView;

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dv.RowFilter = "PERSON_ID='" + dt2.Rows[i]["ah_patic"].ToString() + "' ";
                if (dv.Count > 0)
                {
                    dv[0]["PERSON_NAME"] = "";
                    dv[0]["PERSON_WEIGHT"] = "";
                    dv[0]["PERSON_STATE"] = "";
                    dv[0]["PERSON_ID"] = "";
                    dv[0]["PERSON_HEIGHT"] = "";
                    dv[0]["PERSON_SEX"] = "";
                }

                dv.RowFilter = "BED_NO='" + dt2.Rows[i]["ah_bed"].ToString() + "' ";
                if (dv.Count > 0)
                {
                    dv[0]["PERSON_NAME"] = dt2.Rows[i]["pif_name"].ToString();
                    dv[0]["PERSON_WEIGHT"] = dt2.Rows[i]["cln1_col5"].ToString();
                    dv[0]["PERSON_STATE"] = dt2.Rows[i]["PERSON_STATE"].ToString();
                    dv[0]["PERSON_ID"] = dt2.Rows[i]["ah_patic"].ToString();
                    dv[0]["PERSON_SEX"] = dt2.Rows[i]["PERSON_SEX"].ToString();
                    dv[0]["PERSON_HEIGHT"] = dt2.Rows[i]["pif_height"].ToString();
                }
            }

            Session.Add("PAD_TIME", sTIME.Text);
            Session.Add("PAD_FLOOR", sFLOOR.Text);
            Session.Add("PAD_AREA", sAREA.Text);
            ROW_CNT.Text = dt.Rows.Count.ToString();
            Store istore = ImageView.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void FILL_AREA(string _floor, string _area)
        {
            Boolean bTEMP = false;
            string sTEMP = "";
            if (Session["PAD_AREA"] != null)
                sTEMP = Session["PAD_AREA"].ToString();
            System.Data.DataView dv = new System.Data.DataView();
            dv = dtAREA.DefaultView;

            dv.RowFilter = "mac_flr='" + _floor + "' ";
            cboAREA.Items.Clear();
            Ext.Net.ListItem litem;
            litem = new Ext.Net.ListItem("全区", "全区");
            cboAREA.Items.Add(litem);
            for (int i = 0; i < dv.Count; i++)
            {
                litem = new Ext.Net.ListItem(dv[i]["mac_sec"].ToString() + "区", dv[i]["mac_sec"].ToString());
                cboAREA.Items.Add(litem);
                if (sTEMP == dv[i]["mac_sec"].ToString())
                    bTEMP = true;
            }
            cboAREA.GetStore().DataBind();
            if (dv.Count > 0)
            {
                if (bTEMP)
                    _area = sTEMP;
                else
                    _area = "";
                cboAREA.Disabled = false;
                if (_area == "")
                {
                    cboAREA.Select(0);
                    sAREA.Text = "全区";
                }
                else
                {
                    cboAREA.Select(_area);
                    sAREA.Text = _area;
                }
            }
            else
            {
                cboAREA.Disabled = true;
            }
        }

        #region 取得星期
        protected void GetWeek()
        {
            DateTime date1 = DateTime.Parse(toDay);
            switch (date1.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    txtWEEK.Text = "星期一";
                    sWEEK.Text = "1";
                    break;
                case DayOfWeek.Tuesday:
                    txtWEEK.Text = "星期二";
                    sWEEK.Text = "2";
                    break;
                case DayOfWeek.Wednesday:
                    txtWEEK.Text = "星期三";
                    sWEEK.Text = "3";
                    break;
                case DayOfWeek.Thursday:
                    txtWEEK.Text = "星期四";
                    sWEEK.Text = "4";
                    break;
                case DayOfWeek.Friday:
                    txtWEEK.Text = "星期五";
                    sWEEK.Text = "5";
                    break;
                case DayOfWeek.Saturday:
                    txtWEEK.Text = "星期六";
                    sWEEK.Text = "6";
                    break;
                case DayOfWeek.Sunday:
                    txtWEEK.Text = "星期日";
                    sWEEK.Text = "7";
                    break;
            }
        }
        #endregion

        protected void cboTIME_Click(object sender, DirectEventArgs e)
        {
            sTIME.Text = cboTIME.Value.ToString();
            sTIME.Text = "001";
            sFLOOR.Text = "3";
            sAREA.Text = "A";
            FILL_BED();
        }

        protected void RowItemSelect(object sender, DirectEventArgs e)
        {
            string url;
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            string pid = selRow[0]["PERSON_ID"].ToString();
            string pname = selRow[0]["PERSON_NAME"].ToString();
            string bedno = selRow[0]["BED_NO"];
            string mactype = selRow[0]["MAC_TYPE"];
            string area = selRow[0]["AREA"];
            if (pid != "")
            {
                url = "Patient_detail.aspx?personid=" + pid + "&patient_name=" + pname + "&machine_type=" + mactype + "&floor=" + sFLOOR.Text +
                    "&area=" + area + "&time=" + sTIME.Text + "&bedno=" + bedno + "&daytyp=" + sWEEK.Text + "&sdate=" + toDay;
                loadAspxFile(url);
                //Panel_Left.ToggleCollapse();
            }
        }

        private void loadAspxFile(String url)
        {
            Panel1.Loader.SuspendScripting();
            Panel1.Loader.Url = url;
            Panel1.Loader.DisableCaching = true;
            Panel1.LoadContent();
        }

        protected void ChangePatient(object sender, DirectEventArgs e)
        {
            Common._ErrorMsgShow("JKCMJSMCJFDMCKFDMMFDK");
        }
    }
}