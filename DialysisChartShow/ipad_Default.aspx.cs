using System;
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

namespace Dialysis_Chart_Show
{

    public partial class ipad_Default : System.Web.UI.Page //BaseForm
    {
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        DBMysql db = new DBMysql();
        DataTable dt = new DataTable();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if ((Hospital == "Hospital_Henan" || Hospital == "Hospital_117") && (Session["USER_ID"] == null || Session["USER_NAME"] == null || Session["USER_RIGHT"] == null))
                {
                    X.Redirect("loginhenan.aspx");
                }
                else
                {
                    Set_GridPanel();
                    Show_Picture(); //顯示圖片
                    Show_TimeSec(); //顯示時段  
                    Show_Week(); //顯示星期
                    Show_FloorArea(); //顯示樓層，床區
                    if (Hospital == "Hospital_Henan" || Hospital == "Hospital_117")
                    {
                        Image1.Width = 300;
                        LoginUser.Hidden = false;
                        BtnLogin.Hidden = false;
                        LoginUser.Text = Session["USER_NAME"].ToString();
                    }
                    else
                    {
                        Image1.Width = 400;
                        LoginUser.Hidden = true;
                        BtnLogin.Hidden = true;
                    }

                }
            }
            //TaskManager1.Enabled = false;
            //TaskManager1.StopTask("servertime");
        }

        protected void Set_GridPanel()
        {
            if (Hospital == "Hospital_Henan")
            {
                grdBED_LIST.ColumnModel.Columns[1].Text = "透析器型号";
                grdBED_LIST.ColumnModel.Columns[5].Text = "肝素";
            }
            else if (Hospital == "Hospital_117")
            {
                grdBED_LIST.ColumnModel.Columns[1].Text = "透析器型号";
                grdBED_LIST.ColumnModel.Columns[5].Text = "肝素";
            }
        }

        private void Show_Picture()
        {
            //try
            //{
            //    string sPAD_HEAD = "Styles/mark1.jpg";
            //    DataTable dtPAD_HEAD = db.Query("SELECT * FROM general_setup WHERE genst_code='IPAD_HEAD'");
            //    if (dtPAD_HEAD.Rows.Count > 0)
            //    {
            //        sPAD_HEAD = dtPAD_HEAD.Rows[0]["genst_desc"].ToString();
            //        Image1.ImageUrl = sPAD_HEAD;
            //    }
            //}
            //catch (Exception ex)
            //{
            //    _ErrorMsgShow(ex.Message.ToString());
            //}
        }

        private void Show_TimeSec()
        {
            DateTime now = DateTime.Now;
            this.txtTIME.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            sDATE.Text = this.txtTIME.Text.Substring(0, 10);
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

        private void Show_FloorArea()
        {
            string sql = "SELECT distinct mac_flr, mac_sec FROM mac_setup WHERE 1=1 ORDER BY mac_flr, mac_sec";
            dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                if (Session["PAD_FLOOR"] == null)
                    sFLOOR.Text = dt.Rows[0]["mac_flr"].ToString();
                else
                {
                    if (Session["PAD_FLOOR"].ToString() != dt.Rows[0]["mac_flr"].ToString())
                    {
                        Session["PAD_FLOOR"] = dt.Rows[0]["mac_flr"].ToString();
                    }
                    sFLOOR.Text = Session["PAD_FLOOR"].ToString();
                }
                if (Session["PAD_AREA"] == null)
                    sAREA.Text = dt.Rows[0]["mac_sec"].ToString();
                else
                    sAREA.Text = Session["PAD_AREA"].ToString();
            }

            sql = "SELECT distinct mac_flr FROM mac_setup WHERE 1=1 ORDER BY mac_flr ";
            DataTable dt1 = db.Query(sql);
            for (int i = 0; i < dt1.Rows.Count; i++)
            {
                Ext.Net.ListItem litem;
                litem = new Ext.Net.ListItem(dt1.Rows[i]["mac_flr"].ToString() + "楼", dt1.Rows[i]["mac_flr"].ToString());
                this.cboFLOOR.Items.Add(litem);
            }
            this.cboFLOOR.GetStore().DataBind();

            if (dt1.Rows.Count > 0)
            {
                this.cboFLOOR.Disabled = false;
                this.cboFLOOR.Select(sFLOOR.Text);
                FILL_AREA(sFLOOR.Text, sAREA.Text);
                FILL_BED();
            }
            else
            {
                this.cboFLOOR.Disabled = true;
                this.cboAREA.Disabled = true;
            }

            db.myConnection.Close();
        }

        protected void cboTIME_Click(object sender, DirectEventArgs e)
        {
            //BaseForm bs = new BaseForm();
            sTIME.Text = Common.GetComboBoxValue(this.cboTIME);
            FILL_BED();
        }

        protected void cboFLOOR_Click(object sender, DirectEventArgs e)
        {
            //BaseForm bs = new BaseForm();
            sFLOOR.Text = Common.GetComboBoxValue(this.cboFLOOR);
            FILL_AREA(Common.GetComboBoxValue(this.cboFLOOR), "");
            FILL_BED();
        }
        
        protected void cboAREA_Click(object sender, DirectEventArgs e)
        {
            sAREA.Text = Common.GetComboBoxValue(this.cboAREA);
            FILL_BED();
        }

        protected void Show_Week()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    this.txtWEEK.Text = "星期一";
                    sWEEK.Text = "1";
                    break;
                case DayOfWeek.Tuesday:
                    this.txtWEEK.Text = "星期二";
                    sWEEK.Text = "2";
                    break;
                case DayOfWeek.Wednesday:
                    this.txtWEEK.Text = "星期三";
                    sWEEK.Text = "3";
                    break;
                case DayOfWeek.Thursday:
                    this.txtWEEK.Text = "星期四";
                    sWEEK.Text = "4";
                    break;
                case DayOfWeek.Friday:
                    this.txtWEEK.Text = "星期五";
                    sWEEK.Text = "5";
                    break;
                case DayOfWeek.Saturday:
                    this.txtWEEK.Text = "星期六";
                    sWEEK.Text = "6";
                    break;
                case DayOfWeek.Sunday:
                    this.txtWEEK.Text = "星期日";
                    sWEEK.Text = "7";
                    break;
            }
        }

        protected void FILL_AREA(string _floor, string _area)
        {
            Boolean bTEMP = false;
            string sTEMP = "";
            if (Session["PAD_AREA"] != null)
                sTEMP = Session["PAD_AREA"].ToString();
            System.Data.DataView dv = new System.Data.DataView();
            dv = dt.DefaultView;

            dv.RowFilter = "mac_flr='" + _floor + "' ";
            this.cboAREA.Items.Clear();
            for (int i = 0; i < dv.Count; i++)
            {
                Ext.Net.ListItem litem;
                litem = new Ext.Net.ListItem(dv[i]["mac_sec"].ToString() + "区", dv[i]["mac_sec"].ToString());
                this.cboAREA.Items.Add(litem);
                if (sTEMP == dv[i]["mac_sec"].ToString())
                    bTEMP = true;
            }
            this.cboAREA.GetStore().DataBind();
            if (dv.Count > 0)
            {
                if (bTEMP)
                    _area = sTEMP;
                else
                    _area = "";
                this.cboAREA.Disabled = false;
                if (_area == "")
                {
                    this.cboAREA.Select(0);
                    sAREA.Text = dv[0]["mac_sec"].ToString();
                }
                else
                {
                    this.cboAREA.Select(_area);
                    sAREA.Text = _area;
                }
            }
            else
            {
                this.cboAREA.Disabled = true;
            }
        }

        protected void REFRESH_BED(object sender, StoreReadDataEventArgs e)
        {
            FILL_BED();
        }

        protected void FILL_BED()
        {
            DateTime now = DateTime.Now;
            this.txtTIME.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            sDATE.Text = this.txtTIME.Text.Substring(0, 10);
            Show_Week();
            string sSQL = "";
            sSQL = "SELECT P.cln1_diadate FROM clinical1_nurse P WHERE A.cln1_patica=P.cln1_patica AND P.cln1_diadate<>'" + sDATE.Text + "' ORDER BY cln1_diadate DESC LIMIT 1";
            if (Hospital == "Hospital_Henan")
            {
                sSQL = "SELECT M.mac_bedno AS BED_NO, ";
                sSQL += "N.cln1_col26 AS MAC_MODEL, "; //透析器型號
                sSQL += "IFNULL(N.cln1_col3, M.mac_typ) AS MAC_TYPE, ";
                sSQL += "case UPPER(M.mac_status) when 'Y' then '正常' when 'N' then '保养中' end AS MAC_STATE, ";
                sSQL += "P.pif_name AS PERSON_NAME, ";
                sSQL += "N.cln1_col15 AS PERSON_ID, "; //肝素
                sSQL += "IFNULL(N.cln1_col5, V.pv_weight) AS PERSON_WEIGHT, "; //報到體重
                sSQL += "case V.pv_macstat when 'A' then '开' when 'S' then '关' end As PERSON_STATE, P.pif_ic AS PERSON_IC FROM mac_setup M ";
                sSQL += "LEFT JOIN appointment_setup A ON M.mac_flr=A.apptst_flr AND M.mac_sec=A.apptst_sec ";
                sSQL += "AND M.mac_bedno=A.apptst_bed AND A.apptst_daytyp='" + sWEEK.Text + "' AND A.apptst_timetyp='" + sTIME.Text + "' ";
                sSQL += "LEFT JOIN pat_visit V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + sDATE.Text + "' ";
                sSQL += "LEFT JOIN general_setup G ON M.mac_brand=G.genst_code AND G.genst_ctg='macbrd' ";
                sSQL += "LEFT JOIN pat_info P ON A.apptst_patic=P.pif_ic ";
                sSQL += "LEFT JOIN clinical1_nurse N ON A.apptst_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE.Text + "' "; //淨化參數表格
                sSQL += "WHERE M.mac_flr='" + sFLOOR.Text + "' AND M.mac_sec='" + sAREA.Text + "' ";
                sSQL += "ORDER BY M.mac_bedno ";
            }
            else if (Hospital == "Hospital_117")
            {
                sSQL = "SELECT M.mac_bedno AS BED_NO, ";
                sSQL += "G.genst_desc AS MAC_MODEL, "; //透析器型號
                sSQL += "M.mac_typ AS MAC_TYPE, ";
                sSQL += "case UPPER(M.mac_status) when 'Y' then '正常' when 'N' then '保养中' end AS MAC_STATE, ";
                sSQL += "P.pif_name AS PERSON_NAME, "; 
                sSQL += "(SELECT R.cln1_col15 FROM clinical1_nurse R WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate<>'" + sDATE.Text + "' ";
                sSQL += "ORDER BY R.cln1_diadate DESC LIMIT 1) AS PERSON_ID, "; //肝素
                sSQL += "N.cln1_col5 AS PERSON_WEIGHT, "; //報到體重
                sSQL += "case V.pv_macstat when 'A' then '开' when 'S' then '关' end as PERSON_STATE, P.pif_ic AS PERSON_IC FROM mac_setup M ";
                sSQL += "LEFT JOIN appointment_setup A ON M.mac_flr=A.apptst_flr AND M.mac_sec=A.apptst_sec ";
                sSQL += "AND M.mac_bedno=A.apptst_bed AND A.apptst_daytyp='" + sWEEK.Text + "' AND A.apptst_timetyp='" + sTIME.Text + "' ";
                sSQL += "LEFT JOIN pat_visit V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + sDATE.Text + "' ";
                sSQL += "LEFT JOIN general_setup G ON M.mac_brand=G.genst_code AND G.genst_ctg='macbrd' ";
                sSQL += "LEFT JOIN pat_info P ON A.apptst_patic=P.pif_ic ";
                sSQL += "LEFT JOIN clinical1_nurse N ON A.apptst_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE.Text + "' "; //淨化參數表格
                sSQL += "WHERE M.mac_flr='" + sFLOOR.Text + "' AND M.mac_sec='" + sAREA.Text + "' ";
                sSQL += "ORDER BY M.mac_bedno ";
            }
            else
            {
                sSQL = "SELECT M.mac_bedno AS BED_NO, ";
                sSQL += "G.genst_desc AS MAC_MODEL, ";
                sSQL += "IFNULL(N.cln1_col3, M.mac_typ) AS MAC_TYPE, ";
                sSQL += "case UPPER(M.mac_status) when 'Y' then '正常' when 'N' then '保养中' end AS MAC_STATE, ";
                sSQL += "P.pif_name AS PERSON_NAME, A.apptst_patic AS PERSON_ID, ";
                sSQL += "IFNULL(N.cln1_col5, V.pv_weight) AS PERSON_WEIGHT, "; //報到體重
                sSQL += "case V.pv_macstat when 'A' then '开' when 'S' then '关' end as PERSON_STATE, P.pif_ic AS PERSON_IC FROM mac_setup M ";
                sSQL += "LEFT JOIN appointment_setup A ON M.mac_flr=A.apptst_flr AND M.mac_sec=A.apptst_sec ";
                sSQL += "AND M.mac_bedno=A.apptst_bed AND A.apptst_daytyp='" + sWEEK.Text + "' AND A.apptst_timetyp='" + sTIME.Text + "' ";
                sSQL += "LEFT JOIN pat_visit V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + sDATE.Text + "' ";
                sSQL += "LEFT JOIN general_setup G ON M.mac_brand=G.genst_code AND G.genst_ctg='macbrd' ";
                sSQL += "LEFT JOIN pat_info P ON A.apptst_patic=P.pif_ic ";
                sSQL += "LEFT JOIN clinical1_nurse N ON A.apptst_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE.Text + "' "; //淨化參數表格
                sSQL += "WHERE M.mac_flr='" + sFLOOR.Text + "' AND M.mac_sec='" + sAREA.Text + "' ";
                sSQL += "ORDER BY M.mac_bedno ";
            }
            DataTable dt = db.Query(sSQL);

            //補上臨時預約
            if (Hospital == "Hospital_Henan")
            {
                sSQL = "SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, A.ah_timetyp, P.pif_name, V.pv_weight, ";
                sSQL += "case V.pv_macstat when 'A' then '开' when 'S' then '关' end as PERSON_STATE, P.pif_ic AS PERSON_IC, ";
                sSQL += "N.cln1_col26, N.cln1_col15, N.cln1_col5 ";
                sSQL += "FROM appointment_change A ";
                sSQL += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
                sSQL += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND V.pv_datevisit='" + sDATE.Text + "' ";
                sSQL += "LEFT JOIN clinical1_nurse N ON A.ah_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE.Text + "' "; //淨化參數表格
                sSQL += "WHERE ah_date='" + sDATE.Text + "' AND ah_timetyp='" + sTIME.Text + "' ";
            }
            else if (Hospital == "Hospital_117")
            {
                sSQL = "SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, A.ah_timetyp, P.pif_name, V.pv_weight, ";
                sSQL += "case V.pv_macstat when 'A' then '开' when 'S' then '关' end as PERSON_STATE, P.pif_ic AS PERSON_IC, ";
                sSQL += "N.cln1_col26, N.cln1_col15, N.cln1_col5 ";
                sSQL += "FROM appointment_change A ";
                sSQL += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
                sSQL += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND V.pv_datevisit='" + sDATE.Text + "' ";
                sSQL += "LEFT JOIN clinical1_nurse N ON A.ah_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE.Text + "' "; //淨化參數表格
                sSQL += "WHERE ah_date='" + sDATE.Text + "' AND ah_timetyp='" + sTIME.Text + "' ";
            }
            else
            {
                sSQL = "SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, A.ah_timetyp, P.pif_name, V.pv_weight, ";
                sSQL += "case V.pv_macstat when 'A' then '开' when 'S' then '关' end as PERSON_STATE, P.pif_ic AS PERSON_IC, ";
                sSQL += "N.cln1_col26, N.cln1_col15, N.cln1_col5 ";
                sSQL += "FROM appointment_change A ";
                sSQL += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
                sSQL += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND V.pv_datevisit='" + sDATE.Text + "' ";
                sSQL += "LEFT JOIN clinical1_nurse N ON A.ah_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE.Text + "' "; //淨化參數表格
                sSQL += "WHERE ah_date='" + sDATE.Text + "' AND ah_timetyp='" + sTIME.Text + "' ";           
            }
            DataTable dt2 = db.Query(sSQL);
            System.Data.DataView dv = dt.DefaultView;

            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dv.RowFilter = "BED_NO='" + dt2.Rows[i]["ah_bed"].ToString() + "' ";
                if (dv.Count > 0)
                {
                    if (Hospital == "Hospital_Henan")
                    {
                        dv[0]["MAC_MODEL"] = dt2.Rows[i]["cln1_col26"].ToString();
                        dv[0]["PERSON_ID"] = dt2.Rows[i]["cln1_col15"].ToString();
                        dv[0]["PERSON_WEIGHT"] = dt2.Rows[i]["cln1_col5"].ToString();
                    }
                    else if (Hospital == "Hospital_117")
                    {
                        dv[0]["MAC_MODEL"] = dt2.Rows[i]["cln1_col26"].ToString();
                        dv[0]["PERSON_ID"] = dt2.Rows[i]["cln1_col15"].ToString();
                        dv[0]["PERSON_WEIGHT"] = dt2.Rows[i]["cln1_col5"].ToString();
                    }
                    else
                    {
                        dv[0]["PERSON_ID"] = dt2.Rows[i]["ah_patic"].ToString();
                        dv[0]["PERSON_WEIGHT"] = dt2.Rows[i]["cln1_col5"].ToString();
                    }

                    dv[0]["PERSON_NAME"] = dt2.Rows[i]["pif_name"].ToString();
                    dv[0]["PERSON_STATE"] = dt2.Rows[i]["PERSON_STATE"].ToString();
                    dv[0]["PERSON_IC"] = dt2.Rows[i]["PERSON_IC"].ToString();
                }
            }
 
            Session.Add("PAD_TIME", sTIME.Text);
            Session.Add("PAD_FLOOR", sFLOOR.Text);
            Session.Add("PAD_AREA", sAREA.Text);
            ROW_CNT.Text = dt.Rows.Count.ToString();
            Store istore = grdBED_LIST.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void RowSelect(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);
            string s = selRow[0]["PERSON_NAME"].ToString();
            string pid = selRow[0]["PERSON_IC"].ToString(); //身分證號
            if (s != "")
            {
                //Session["Person_ID"] = s;
                string t = "ipad_PatientList.aspx?pid=" + pid + "&floor=" + sFLOOR.Text + "&area=" + sAREA.Text + "&time=" + sTIME.Text + "&bedno=" + selRow[0]["BED_NO"] + "&daytyp=" + sWEEK.Text;
                X.Redirect(t);
                //Response.Write("<script>window.open('" + t + "');</script>");
            }
        }

        #region 沒用到
        protected void Timer1_Timer(object sender, EventArgs e)
        {
            this.txtTIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //TextField1.Text = WIDTH.Text;
            //TextField2.Text = HIGHT.Text;
            //this.txtWEEK.Text = HIGHT.Text;
            //TaskManager1.Enabled = false;
            //TaskManager1.StopAll();
            //int i = 0;
            //if (HIGHT.Text != "")
            //    i = Convert.ToInt16(HIGHT.Text);
            //i = i - 144;
            //if (i < 420)
            //    i = 420;
            //grdBED_LIST.Height = i;
        }
        #endregion

        protected void BtnLogout_Click(object sender, DirectEventArgs e)
        {
            Session["USER_ID"] = null;
            Session["USER_NAME"] = null;
            Session["USER_RIGHT"] = null;
            X.Redirect("loginhenan.aspx");
        }
    }
}