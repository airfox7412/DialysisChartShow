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

namespace Dialysis_Chart_Show
{

    public partial class Dialysis_Schedul : System.Web.UI.Page //BaseForm
    {
        private static DataTable dtAREA;

        public static Size ScreenResolution
        {
            get
            {
                return (Size)HttpContext.Current.Session["ScreenResolution"];
            }
            set
            {
                HttpContext.Current.Session["ScreenResolution"] = value;
            }

        }

        [WebMethod()]
        public static void setResolution(int width, int height)
        {
            ScreenResolution = new Size(width, height);
        }

        protected void Page_Load(object sender, EventArgs e)
        {

            if (!X.IsAjaxRequest)
            {
                //TextField1.Hidden = true;
                //TextField2.Hidden = true;

                DBMysql db = new DBMysql();

                try
                {
                    string sPAD_HEAD = "Styles/mark1.png";
                    //string sPAD_HEAD = "";
                    DataTable dtPAD_HEAD = db.Query("SELECT *  FROM general_setup WHERE  genst_code='IPAD_HEAD'");
                    if (dtPAD_HEAD.Rows.Count > 0)
                    {
                        sPAD_HEAD = dtPAD_HEAD.Rows[0]["genst_desc"].ToString();
                        //sPAD_HEAD = Server.MapPath(sPAD_HEAD);
                        Image2.ImageUrl = sPAD_HEAD;
                    }
                }
                catch (Exception ex)
                {
                    //_ErrorMsgShow(ex.Message.ToString());
                }

                DateTime now = DateTime.Now;
                //this.txtDATE.Text = now.ToString("yyyy-MM-dd");

                this.txtTIME.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
                this.txtTIME1.Text = now.ToString("yyyy-MM-dd");                

                //sDATE.Text = this.txtDATE.Text;
                sDATE.Text = this.txtTIME.Text.Substring(0, 10);
                //this.txtWEEK.Text = now.ToString("ddd");
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

                GET_WEEK();


                this.WYEAR.Text = now.ToString("yyyy");
                this.WMON.Text = now.ToString("MM");
                this.cboYEAR.Disabled = false;
                YEAR_CHECK(this.WYEAR.Text);
                MON_CHECK(this.WMON.Text);           


                if (dtAREA == null)
                    dtAREA = db.Query("SELECT distinct mac_flr, mac_sec FROM mac_setup WHERE 1=1 ORDER BY mac_flr, mac_sec ");
                if (dtAREA.Rows.Count > 0)
                {
                    if (Session["PAD_FLOOR"] == null)
                        sFLOOR.Text = dtAREA.Rows[0]["mac_flr"].ToString();
                    else
                        sFLOOR.Text = Session["PAD_FLOOR"].ToString();
                    if (Session["PAD_AREA"] == null)
                        sAREA.Text = dtAREA.Rows[0]["mac_sec"].ToString();
                    else
                        sAREA.Text = Session["PAD_AREA"].ToString();
                }
                System.Data.DataTable dtFLOOR = db.Query("SELECT distinct mac_flr FROM mac_setup WHERE 1=1 ORDER BY mac_flr ");
                for (int i = 0; i < dtFLOOR.Rows.Count; i++)
                {
                    Ext.Net.ListItem litem;
                    litem = new Ext.Net.ListItem(dtFLOOR.Rows[i]["mac_flr"].ToString() + "楼", dtFLOOR.Rows[i]["mac_flr"].ToString());
                    this.cboFLOOR.Items.Add(litem);
                }
                this.cboFLOOR.GetStore().DataBind();
                if (dtFLOOR.Rows.Count > 0)
                {
                    this.cboFLOOR.Disabled = false;
                    this.cboFLOOR.Select(sFLOOR.Text);
                    //SetComboBoxValue(this.cboFLOOR, "05楼", false);
                    FILL_AREA(sFLOOR.Text, sAREA.Text);
                    WFLOOR.Text = sFLOOR.Text;
                    WAREA.Text = sAREA.Text;

                    //Column9.Text = WFLOOR.Text + "楼";
                    //Column11.Text = WAREA.Text + "區";
                    //FILL_BED();
                    //FILL_BEDN();
                }
                else
                {
                    this.cboFLOOR.Disabled = true;
                    this.cboAREA.Disabled = true;
                }
                //Timer1.Enabled = true;
                //TaskManager1.Enabled = true;
                TaskManager1.StopTask("servertime");


                //2015.03.25 姓名 Andy
                System.Data.DataTable dtName = db.Query("select DISTINCT apptst_patrefid from appointment_setup order by apptst_patrefid");
                for (int i = 0; i < dtName.Rows.Count; i++)
                {
                    Ext.Net.ListItem litem1;
                    litem1 = new Ext.Net.ListItem(dtName.Rows[i]["apptst_patrefid"].ToString(), dtName.Rows[i]["apptst_patrefid"].ToString());
                    this.SelectBox1.Items.Add(litem1);
                }
                this.SelectBox1.GetStore().DataBind();
                if (dtName.Rows.Count > 0)
                {
                    this.SelectBox1.Disabled = false;
                    this.SelectBox1.Select(sNAME.Text);
                    //FILL_AREA(sFLOOR.Text, sAREA.Text);
                    //FILL_BED();
                }
                else
                {
                    //this.cboFLOOR.Disabled = true;
                    //this.cboAREA.Disabled = true;
                }
                //

                //2015.03.25 GP 身分證號 Andy
                System.Data.DataTable dtpatic = db.Query("select DISTINCT apptst_patic from appointment_setup order by apptst_patic");
                for (int i = 0; i < dtpatic.Rows.Count; i++)
                {
                    Ext.Net.ListItem litem2;
                    litem2 = new Ext.Net.ListItem(dtpatic.Rows[i]["apptst_patic"].ToString(), dtpatic.Rows[i]["apptst_patic"].ToString());
                    this.SelectBox2.Items.Add(litem2);
                }
                this.SelectBox2.GetStore().DataBind();
                if (dtpatic.Rows.Count > 0)
                {
                    this.SelectBox2.Disabled = false;
                    this.SelectBox2.Select(spatic.Text);
                }
                else
                {
                }
                //
               
                
            }

        }


        protected void nurse_Click(object sender, DirectEventArgs e)
        {           
            string sched_year = e.ExtraParams["sched_year"];
            string sched_mon = e.ExtraParams["sched_mon"];
            string sched_flr = e.ExtraParams["sched_flr"];
            string sched_sec = e.ExtraParams["sched_sec"];
            string sched_bedno = e.ExtraParams["sched_bedno"];
            string sched_mactype = e.ExtraParams["sched_mactyp"];
            string sched_time = e.ExtraParams["sched_time"];           
            string select_flag = e.ExtraParams["select_flag"];
            string sched_DAY = e.ExtraParams["sched_" + select_flag + "D"];            
        }


        protected void cboTIME_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            sTIME.Text = bs.GetComboBoxValue(this.cboTIME);
            //FILL_BEDN();
        }

        protected void cboFLOOR_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            sFLOOR.Text = bs.GetComboBoxValue(this.cboFLOOR);
            WFLOOR.Text = sFLOOR.Text;
            FILL_AREA(bs.GetComboBoxValue(this.cboFLOOR), "");           
            //FILL_BEDN();
        }

        protected void cboAREA_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            sAREA.Text = bs.GetComboBoxValue(this.cboAREA);
            WAREA.Text = sAREA.Text;
            //FILL_BEDN();
        }

        //2015.03.25 姓名 Andy
        protected void cboNAME_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            sNAME.Text = bs.GetComboBoxValue(this.SelectBox1);
            WNAME.Text = sNAME.Text;
        }


        //2015.03.25 身分證號 Andy
        protected void cboPATIC_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            spatic.Text = bs.GetComboBoxValue(this.SelectBox2); 
        }

        //2015.03.25 查詢 Andy
        protected void btn_Query_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            DateTime now = DateTime.Now;
            this.txtTIME.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            sDATE.Text = this.txtTIME.Text.Substring(0, 10);
            GET_WEEK();
            string sSQL = "";


            sSQL = "SELECT A.sched_year  AS sched_year,A.sched_mon  AS sched_mon, " +
                   " A.sched_flr  AS sched_flr,A.sched_sec  AS sched_sec,A.sched_bedno AS sched_bedno,A.sched_mactyp AS sched_mactyp,A.sched_timen  AS sched_timen," +
                   " A.SCHED_1D_NAME  AS SCHED_1D_NAME,A.SCHED_2D_NAME AS SCHED_2D_NAME,A.SCHED_3D_NAME AS SCHED_3D_NAME,A.SCHED_4D_NAME AS SCHED_4D_NAME,A.SCHED_5D  AS SCHED_5D," +
                   "       A.SCHED_6D  AS SCHED_6D,A.SCHED_7D AS SCHED_7D,A.SCHED_8D AS SCHED_8D,A.SCHED_9D AS SCHED_9D,A.SCHED_10D AS SCHED_10D," +
                   "       A.SCHED_11D AS SCHED_11D,A.SCHED_12D AS SCHED_12D,A.SCHED_13D AS SCHED_13D,A.SCHED_14D AS SCHED_14D,A.SCHED_15D AS SCHED_15D" +
                   " FROM data_sched A " +
                   " WHERE A.sched_year = '" + WYEAR.Text + "' and A.sched_mon ='" + WMON.Text + "'" +
                   "  and  A.sched_flr  = '" + WFLOOR.Text + "' and A.sched_sec ='" + WAREA.Text + "'";                   

            DBMysql db = new DBMysql();
            System.Data.DataTable dt = db.Query(sSQL);
            Store istore = grdBED_LISTN.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();

            if (dt.Rows.Count > 0)
            {

            };


            //Column5.Text = "25"; 
            Panel5.Title = WFLOOR.Text + "楼" + WAREA.Text + "区";
        }


         protected void btn_MOD_Click(object sender, DirectEventArgs e)
        {

        }


        //2015.03.25 重置 Andy
        protected void btn_set_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
        }

        //2015.03.25 存盤 Andy
        protected void btn_save_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
        }

        //2015.03.25 打印 Andy
        protected void btn_print_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
        }

        //2015.03.25  Andy
        protected void btn_YEAR_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            //sYEAR.Text = bs.GetComboBoxValu(this.cboYEAR);
            sYEAR.Text = bs.GetComboBoxText(this.cboYEAR);
            WYEAR.Text = sYEAR.Text;
            Panel2.Title = sYEAR.Text + " 年 " + sMON.Text + " 月";
        }

        //2015.03.25  Andy
        protected void btn_MON_Click(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            //sMON.Text = bs.GetComboBoxValu(this.cboMON);
            sMON.Text = bs.GetComboBoxText(this.cboMON);
            WMON.Text = sMON.Text; 
            Panel2.Title = sYEAR.Text + " 年 " + sMON.Text + " 月";
        }


        protected void YEAR_CHECK(string _year)
        {
            if (_year == "2013")
            {
                this.cboYEAR.Select("001");
            }
            if (_year == "2014")
            {
                this.cboYEAR.Select("002");
            }
            if (_year == "2015")
            {
                this.cboYEAR.Select("003");
            }
            if (_year == "2016")
            {
                this.cboYEAR.Select("004");
            }
            if (_year == "2017")
            {
                this.cboYEAR.Select("005");
            }
            if (_year == "2018")
            {
                this.cboYEAR.Select("006");
            }
            if (_year == "2019")
            {
                this.cboYEAR.Select("007");
            }
            if (_year == "2020")
            {
                this.cboYEAR.Select("008");
            }
            if (_year == "2021")
            {
                this.cboYEAR.Select("009");
            }
            if (_year == "2022")
            {
                this.cboYEAR.Select("010");
            }
            if (_year == "2023")
            {
                this.cboYEAR.Select("011");
            }
        }


         protected void MON_CHECK(string _mon)
        {
            if (_mon == "03")
            {
                this.cboMON.Select("003");
            }
            if (_mon == "04")
            {
                this.cboMON.Select("004");
            }
            if (_mon == "05")
            {
                this.cboMON.Select("005");
            }
            if (_mon == "06")
            {
                this.cboMON.Select("006");
            }
            if (_mon == "07")
            {
                this.cboMON.Select("007");
            }
            if (_mon == "08")
            {
                this.cboMON.Select("008");
            }
        }



        protected void GET_WEEK()
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
            dv = dtAREA.DefaultView;
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
            //FILL_BEDN();
        }

        protected void FILL_BED()
        {

            //if (GetComboBoxValu(this.cboFLOOR) != "")
            //    sFLOOR.Text = GetComboBoxValu(this.cboFLOOR);
            //if (GetComboBoxValu(this.cboAREA) != "")
            //    sAREA.Text = GetComboBoxValu(this.cboAREA);
            DateTime now = DateTime.Now;
            //this.txtDATE.Text = now.ToString("yyyy-MM-dd");
            this.txtTIME.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            //sDATE.Text = this.txtDATE.Text;
            sDATE.Text = this.txtTIME.Text.Substring(0, 10);
            GET_WEEK();
            string sSQL = "";
            sSQL = "SELECT M.mac_bedno AS BED_NO, G.genst_desc AS MAC_MODEL, M.mac_typ AS MAC_TYPE, M.mac_status AS MAC_STATE, " +
                          "P.pif_name AS PERSON_NAME, A.apptst_patic AS PERSON_ID, " +
                          "V.pv_weight AS PERSON_WEIGHT, V.pv_macstat AS PERSON_STATE " +

                     "FROM mac_setup M " +
                     "LEFT JOIN appointment_setup A " +
                       "ON M.mac_flr=A.apptst_flr " +
                      "AND M.mac_sec=A.apptst_sec " +
                      "AND M.mac_bedno=A.apptst_bed " +
                      "AND A.apptst_daytyp='" + sWEEK.Text + "' " +
                      "AND A.apptst_timetyp='" + sTIME.Text + "' " +
                     "LEFT JOIN pat_visit V " +
                       "ON A.apptst_patic=V.pv_ic " +
                      "AND V.pv_datevisit='" + sDATE.Text + "' " +
                     "LEFT JOIN general_setup G " +
                       "ON M.mac_brand=G.genst_code " +
                      "AND G.genst_ctg='macbrd' " +
                     "LEFT JOIN pat_info P " +
                       "ON A.apptst_patic=P.pif_ic " +
                    "WHERE M.mac_flr='" + sFLOOR.Text + "' " +
                      "AND M.mac_sec='" + sAREA.Text + "' " +
                    "ORDER BY CONVERT(SUBSTRING_INDEX(M.mac_bedno, '-', 1),UNSIGNED INTEGER), M.mac_bedno ";
            //                "AND M.mac_sec='" + sAREA.Text + "' " +
            DBMysql db = new DBMysql();
            System.Data.DataTable dt = db.Query(sSQL);

            //補上臨時預約 2015.03.26 ANDY 暫MARK
            sSQL = "SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, " +
                          "A.ah_bed, A.ah_timetyp, P.pif_name, " +
                          "V.pv_weight, V.pv_macstat " +
                     "FROM appointment_change A " +
                     "LEFT JOIN pat_info P " +
                       "ON A.ah_patic=P.pif_ic " +
                    "LEFT JOIN pat_visit V " +
                       "ON A.ah_patic=V.pv_ic " +
                      "AND V.pv_datevisit='" + sDATE.Text + "' " +
                     "WHERE ah_date='" + sDATE.Text + "' " +
                      "AND ah_timetyp='" + sTIME.Text + "' ";
            System.Data.DataTable dt2 = db.Query(sSQL);
            System.Data.DataView dv = dt.DefaultView;           
                 

            //Column5.Text = sDATE.Text;
            Column5.Text = "25";


            //2015.01.07 這裡是臨時預約跟換床沒關 先把flag旗標關閉
            //bool changeFlag = false;

            //2014.01.05 Frank  開始點名換床的人
            for (int i = 0; i < dt2.Rows.Count; i++)
            {
                dv.RowFilter = "PERSON_ID='" + dt2.Rows[i]["ah_patic"].ToString() + "' ";
                if (dv.Count > 0)
                {
                    dv[0]["PERSON_NAME"] = "";
                    dv[0]["PERSON_WEIGHT"] = "";
                    dv[0]["PERSON_STATE"] = "";
                    dv[0]["PERSON_ID"] = "";
                    //2015.01.07 這裡是臨時預約跟換床沒關 先把flag旗標關閉
                    //changeFlag = true;
                }

                dv.RowFilter = "BED_NO='" + dt2.Rows[i]["ah_bed"].ToString() + "' ";
                if (dv.Count > 0)
                {
                    dv[0]["PERSON_ID"] = dt2.Rows[i]["ah_patic"].ToString();
                    //2015.01.07 這裡是臨時預約跟換床沒關 先把flag旗標關閉
                    //if (changeFlag)
                    //dv[0]["PERSON_NAME"] = dt2.Rows[i]["pif_name"].ToString() + "";
                    //2015.01.07 這裡是臨時預約跟換床沒關 先把flag旗標關閉
                    //else

                    //    dv[0]["PERSON_NAME"] = dt2.Rows[i]["pif_name"].ToString() + "(临)";
                    dv[0]["PERSON_NAME"] = dt2.Rows[i]["pif_name"].ToString();

                    dv[0]["PERSON_WEIGHT"] = dt2.Rows[i]["pv_weight"].ToString();
                    dv[0]["PERSON_STATE"] = dt2.Rows[i]["pv_macstat"].ToString();
                }
            }

            Session.Add("PAD_TIME", sTIME.Text);
            Session.Add("PAD_FLOOR", sFLOOR.Text);
            Session.Add("PAD_AREA", sAREA.Text);
            ROW_CNT.Text = dt.Rows.Count.ToString();
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch (dt.Rows[i]["PERSON_STATE"].ToString())
                {
                    case "A":
                        dt.Rows[i]["PERSON_STATE"] = "开";
                        break;
                    case "S":
                        dt.Rows[i]["PERSON_STATE"] = "关";
                        break;
                }
                switch (dt.Rows[i]["MAC_STATE"].ToString())
                {
                    case "Y":
                        dt.Rows[i]["MAC_STATE"] = "正常";
                        break;
                    case "y":
                        dt.Rows[i]["MAC_STATE"] = "正常";
                        break;
                    case "N":
                        dt.Rows[i]["MAC_STATE"] = "保养中";
                        break;
                    case "n":
                        dt.Rows[i]["MAC_STATE"] = "保养中";
                        break;
                }
            }
            dt.AcceptChanges();
            Store istore = grdBED_LIST.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }


        //2015.03.26 ANDY 新床位 
        protected void FILL_BEDN()
        {
            DateTime now = DateTime.Now;
            this.txtTIME.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            sDATE.Text = this.txtTIME.Text.Substring(0, 10);
            GET_WEEK();
            string sSQL = "";
            sSQL = "SELECT A.sched_year  AS sched_year,A.sched_mon  AS sched_mon, " +
                   " A.sched_flr  AS sched_flr,A.sched_sec  AS sched_sec,A.sched_bedno AS sched_bedno,A.sched_mactyp AS sched_mactyp,A.sched_timen  AS sched_timen," +
                   " A.SCHED_1D_NAME  AS SCHED_1D_NAME,A.SCHED_2D_NAME AS SCHED_2D_NAME,A.SCHED_3D_NAME AS SCHED_3D_NAME,A.SCHED_4D_NAME AS SCHED_4D_NAME,A.SCHED_5D  AS SCHED_5D," +
                   "       A.SCHED_6D  AS SCHED_6D,A.SCHED_7D AS SCHED_7D,A.SCHED_8D AS SCHED_8D,A.SCHED_9D AS SCHED_9D,A.SCHED_10D AS SCHED_10D," +
                   "       A.SCHED_11D AS SCHED_11D,A.SCHED_12D AS SCHED_12D,A.SCHED_13D AS SCHED_13D,A.SCHED_14D AS SCHED_14D,A.SCHED_15D AS SCHED_15D" +
                   " FROM data_sched A ";
            DBMysql db = new DBMysql();
            System.Data.DataTable dt = db.Query(sSQL);
            Store istore = grdBED_LISTN.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();

            if (dt.Rows.Count > 0)
            {
                
            };


            //Column5.Text = "25"; 
            Panel5.Title = WFLOOR.Text + "楼" + WAREA.Text + "区";          
        }
        //



        protected void Timer1_Timer(object sender, EventArgs e)
        {
            this.txtTIME.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");
            //TextField1.Text = WIDTH.Text;
            //TextField2.Text = HIGHT.Text;
            //this.txtWEEK.Text = HIGHT.Text;
            TaskManager1.Enabled = false;
            TaskManager1.StopAll();
            int i = 0;
            if (HIGHT.Text != "")
                i = Convert.ToInt16(HIGHT.Text);
            i = i - 144;
            if (i < 420)
                i = 420;
            grdBED_LIST.Height = i;
        }

        protected void RowSelect(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            //string s = selRow[0]["PERSON_ID"].ToString();

            //if (s != "")
            //{
            //    //Session["Person_ID"] = s;
            //    string t = "ipad_PatientList.aspx?editmode=page1&floor=" + sFLOOR.Text + "&area=" + sAREA.Text + "&time=" + sTIME.Text + "&bedno=" + selRow[0]["BED_NO"] + "&daytyp=" + sWEEK.Text;
            //    X.Redirect(t);
            //    //Response.Write("<script>window.open('" + t + "');</script>");
            //}
        }


    }
}