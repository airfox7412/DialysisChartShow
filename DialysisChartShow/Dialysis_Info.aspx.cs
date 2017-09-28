using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using System.Xml.Xsl;
using System.Xml;

namespace Dialysis_Chart_Show
{
    public partial class Dialysis_Info : BaseForm
    {
        //判斷是否要顯示空床
        string CheckinEmpty = ConfigurationManager.AppSettings["CheckinEmpty"]; 

        //統計資料上傳是否開啟
        string sShowFHIR = ConfigurationManager.AppSettings["ShowFHIR"] == null ? "" : ConfigurationManager.AppSettings["ShowFHIR"].ToString();

        //临床小帮手是否開啟
        string helpwiz = ConfigurationManager.AppSettings["helpwiz"].ToString();

        string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

        public string url;
        String userId;
        private static DataTable dtAREA;
        private static Message i18nMessage = new Message(Message.Language.zh_cn);

        protected void ShowPatient()
        {
            String i18nUser = i18nMessage.GetMessage("UserId") + ": ";
            i18nUser += Session["USER_NAME"].ToString();
            Lab_user_name.Text = i18nUser.Trim();
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (Hospital == "Hospital_Henan" || Hospital == "Hospital_Hebei" || Hospital == "Hospital_Alasamo")
                {
                    Button_12.QTipCfg.Text = "病患报到";
                }
                else
                {
                    Button_12.QTipCfg.Text = "病历资料";
                }
                if (Session["USER_ID"] == null)
                {
                    X.Redirect("login.aspx");
                }
                else
                {
                    ShowPatient();
                    show_history();
                    Refreach_Label();
                }
            }
        }

        #region 工作流程
        //排班
        protected void ImageButton11_click(object sender, DirectEventArgs e)
        {
            url = "checkin/SchMenu.aspx";
            DirectLoad();
        }

        //報到
        protected void ImageButton12_click(object sender, DirectEventArgs e)
        {
            if (Session["USER_NAME"] == null || Session["USER_RIGHT"] == null)
                X.Redirect("login.aspx");
            else
            {
                if (Session["USER_RIGHT"].ToString() != "AD" && Session["USER_RIGHT"].ToString() != "DC" && Session["USER_RIGHT"].ToString() != "DH")
                {
                    Common._ErrorMsgShow("权限不足无法使用");
                }
                else
                {
                    Panel_Loader1.Hidden = true;
                    Panel_Loader2.Hidden = true;
                    Panel_Loader3.Hidden = false;
                    Show_Checkin();
                }
            }
        }

        //临时病患床位排班
        protected void ImageButton121_click(object sender, DirectEventArgs e)
        {
            if (Session["USER_NAME"] == null || Session["USER_RIGHT"] == null)
                X.Redirect("login.aspx");
            else
            {
                if (Session["USER_RIGHT"].ToString() != "AD" && Session["USER_RIGHT"].ToString() != "DC" && Session["USER_RIGHT"].ToString() != "DH")
                {
                    Common._ErrorMsgShow("权限不足无法使用");
                }
                else
                {
                    Session["PAT_NAME"] = null;
                    url = "checkin/TempPatient_Tab.aspx";
                    DirectLoad();
                }
            }
        }

        //治疗计画
        protected void ImageButton13_click(object sender, DirectEventArgs e)
        {
            ShowPatient();
            url = "checkin/Dialysis_PreSetView.aspx";
            if (Session["USER_ID"] == null || Session["USER_NAME"] == null || Session["USER_RIGHT"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
            {
                if (Session["USER_RIGHT"].ToString() != "AD" && Session["USER_RIGHT"].ToString() != "DC" && Session["USER_RIGHT"].ToString() != "DH")
                {
                    Common._ErrorMsgShow("权限不足无法使用");
                }
                else
                {
                    userId = Session["USER_ID"].ToString();
                    //if (Session["PAT_IC"] == null)
                    //{
                        Session["redirect"] = url;
                        PatInfo_Query();
                    //}
                    //else
                    //{
                    //   DirectLoad();
                    //}
                }
            }
        }

        //首诊四问
        protected void ImageButton141_click(object sender, DirectEventArgs e)
        {
            ShowPatient();
            url = "checkin/Dialysis_Question4.aspx";
            if (Session["USER_ID"] == null || Session["USER_NAME"] == null || Session["USER_RIGHT"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
            {
                if (Session["USER_RIGHT"].ToString() != "AD" && Session["USER_RIGHT"].ToString() != "DC" && Session["USER_RIGHT"].ToString() != "DH")
                {
                    Common._ErrorMsgShow("权限不足无法使用");
                }
                else
                {
                    loadAspxFile();
                }
            }
        }

        //处方模版
        protected void ImageButton14_click(object sender, DirectEventArgs e)
        {
            if (Session["USER_NAME"] == null || Session["USER_RIGHT"] == null)
                X.Redirect("login.aspx");
            else
            {
                if (Session["USER_RIGHT"].ToString() != "AD" && Session["USER_RIGHT"].ToString() != "DC" && Session["USER_RIGHT"].ToString() != "DH")
                {
                    Common._ErrorMsgShow("权限不足无法使用");
                }
                else
                {
                    url = "checkin/Dialysis_ModPreSet.aspx";
                    DirectLoad();
                }
            }
        }

        #region 病历項目
        //血透病患总览
        protected void ImageButton21_click(object sender, DirectEventArgs e)
        {
            //url = "Information/Pat_info_list.aspx";
            //DirectLoad();
            Panel_Loader1.Hidden = true;
            Panel_Loader2.Hidden = false;
            Panel_Loader3.Hidden = true;
            show_history();
        }

        //血液净化过程
        protected void ImageButton22_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_09.aspx";
            loadAspxFile();
        }

        //血透信息纪录
        protected void ImageButton23_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_02.aspx";
            loadAspxFile();
        }

        //实验室及辅助检查
        protected void ImageButton24_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_04.aspx";
            loadAspxFile();
        }

        //诊断信息
        protected void ImageButton25_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_01.aspx";
            loadAspxFile();
        }

        //病程记录
        protected void ImageButton26_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_05.aspx";
            loadAspxFile();
        }

        //血透评估表
        protected void ImageButton27_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_0h.aspx";
            loadAspxFile();
        }
        #endregion

        #region 統計項目
        //质量分析统计
        protected void ImageButton31_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_10.aspx";
            DirectLoad();
        }

        //透析用水/液量测
        protected void ImageButton32_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_12.aspx";
            DirectLoad();
        }

        //統計資料上傳
        protected void ImageButton33_click(object sender, DirectEventArgs e)
        {
            if (sShowFHIR == "true")
              url="Information/DialysisFhirClient.aspx";
            else
              url= "Information/Dialysis_12.aspx";
            DirectLoad();
        }
        #endregion

        #region 管理項目
        //患者信息索引
        protected void ImageButton41_click(object sender, DirectEventArgs e)
        {
            url = "Information/Dialysis_06.aspx"; 
            if (Session["USER_ID"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
                userId = Session["USER_ID"].ToString();

            Session["redirect"] = url;
            PatInfo_Query();
        }

        //预估领料纪录
        protected void ImageButton15_click(object sender, DirectEventArgs e)
        {
            if (Session["USER_ID"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
            {
                url = "checkin/Material_List.aspx";
                DirectLoad();
            }
        }
        #endregion

        //库存管理纪录
        protected void ImageButton42_click(object sender, DirectEventArgs e)
        {
            if (Session["USER_ID"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
            {
                if (Session["USER_RIGHT"].ToString() == "SK")
                    Common._ErrorMsgShow("您没有权限访问!");
                else
                {
                    url = "Stock/Dialysis_Stock.aspx";
                    DirectLoad();
                }
            }
        }

        //设备管理
        protected void ImageButton45_click(object sender, DirectEventArgs e)
        {
            if (Session["USER_ID"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
            {
                if (Session["USER_RIGHT"].ToString() == "SK")
                    Common._ErrorMsgShow("您没有权限访问!");
                else
                {
                    url = "Device/Dialysis_Device.aspx";
                    DirectLoad();
                }
            }
        }

        //系統設置
        protected void ImageButton43_click(object sender, DirectEventArgs e)
        {
            if (Session["USER_ID"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
            {
                if (Session["USER_RIGHT"].ToString() == "AD" || Session["USER_RIGHT"].ToString() == "DH")
                {
                    url = "Systems/Config.aspx";
                    DirectLoad();
                }
                else
                {
                    Common._ErrorMsgShow("您没有权限访问!");
                }
            }
        }

        //登出
        protected void ImageButton44_click(object sender, DirectEventArgs e)
        {
            Session["USER_NAME"] = null;
            Session["USER_RIGHT"] = null;
            Session["USER_ID"] = null;
            Session["PAT_IC"] = null;
            _PAT_IC = "";
            Session["PAT_ID"] = null;
            _PAT_ID = "";
            X.Redirect("login.aspx");
        }
        #endregion

        //若沒有選擇病患就先選擇一位病患
        private void loadAspxFile()
        {
            Panel_Loader1.Hidden = false;
            Panel_Loader2.Hidden = true;
            Panel_Loader3.Hidden = true;

            if (Session["USER_ID"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
                userId = Session["USER_ID"].ToString();

            if (_PAT_IC == "")
            {
                Session["redirect"] = url;
                PatInfo_Query();
            }
            else
            {
                Panel_Loader1.Loader.SuspendScripting();
                Panel_Loader1.Loader.Url = url;
                Panel_Loader1.Loader.DisableCaching = true;
                Panel_Loader1.LoadContent();
                Refreach_Label();
            }
        }

        //直接轉往功能項目
        private void DirectLoad()
        {
            Panel_Loader1.Hidden = false;
            Panel_Loader2.Hidden = true;
            Panel_Loader3.Hidden = true;

            if (Session["USER_ID"] == null)
                X.Redirect("login.aspx?target=" + url);
            else
                userId = Session["USER_ID"].ToString();

            Panel_Loader1.Loader.SuspendScripting();
            Panel_Loader1.Loader.Url = url;
            Panel_Loader1.Loader.DisableCaching = true;
            Panel_Loader1.LoadContent();
        }

        #region 查詢病患
        protected void PatInfo_Query()
        {
            string PATIC = SearchID.Text;
            string PATNAME = SearchName.Text;
            string sql;
            sql = " SELECT pif_id, pif_name, if(pif_sex = 'M','男','女') as sex, pif_dob, pif_ic, pif_docname FROM pat_info ";
            sql += "WHERE 1=1 ";
            if (!string.IsNullOrEmpty(PATNAME)) //姓名篩選
                sql += "AND pif_name like '%" + PATNAME + "%' ";
            if (!string.IsNullOrEmpty(PATIC)) //身分證號篩選
                sql += "AND pif_ic like '%" + PATIC + "%' ";
            sql += "ORDER BY pif_id ";

            DataTable dt = db.Query(sql);
            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
            Window1.Show();
        }
        #endregion
        
        protected void Win_Close(object sender, DirectEventArgs e)
        {
            Refreach_Label();
            if (String.IsNullOrEmpty(_PAT_IC) == false)
            {
                url = Session["redirect"].ToString();
                DirectLoad();
            }
        }

        #region Window1 历史病患
        protected void btn_Query_Click(object sender, DirectEventArgs e)
        {
            PatInfo_Query();
        }
        #endregion

        #region Window1 病患查找-選擇病患
        protected void Dialysis_detail(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);
            _PAT_IC = selRow[0]["pat_ic"].ToString();
            _PIF_NAME = selRow[0]["pif_name"].ToString();
            _USER_NAME = selRow[0]["pif_docname"].ToString();
            _PIF_SX = selRow[0]["pif_sex"].ToString() + "性";

            string sql = "SELECT pif_id, pif_name, pif_docname FROM pat_info ";
            sql += " where pif_ic = '" + _PAT_IC + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pif_docname"].ToString() != null)
                    _PatDocName = dt.Rows[0]["pif_docname"].ToString();
                else
                    _PatDocName = "";
                Session["PAT_ID"] = dt.Rows[0]["pif_id"].ToString();
                _PAT_ID = Session["PAT_ID"].ToString();

                Store istore = GridList.GetStore();
                istore.RemoveAll();
                Window1.Close();
            }
        }
        #endregion

        #region 历史病患
        protected void btn_QueryHistory_Click(object sender, DirectEventArgs e)
        {
            show_history();
        }
        #endregion

        #region 選擇历史病患
        protected void show_history()
        {
            string sql;
            DateTime datetime = DateTime.Now;
            sql = " SELECT a.pif_id, a.pif_name, if (a.pif_sex = 'M','男','女') as sex, a.pif_dob, a.pif_ic, ";
            sql += "if (b.next_visit_date > '','腹透','血透') as txt_10, ";
            sql += "e.diadate AS FirstDate, '' AS InfoDate, ";
            sql += "if (g.BI > 0,'V',' ') as txt_101, ";
            sql += "case f.opt_1 when '1' then '退出' when '2' then '肾移植' when '3' then '转出' when '4' then '死亡' when '5' then '转入' else '' end AS opt_52, ";
            sql += "f.info_date, a.pif_docname ";
            sql += "FROM pat_info a ";
            sql += "LEFT JOIN zinfo_maim c ON a.pif_id=c.pat_id ";
            sql += "LEFT JOIN (SELECT cln1_patic, MAX(cln1_diadate) AS diadate FROM clinical1_nurse group by cln1_patic) e  ON a.pif_ic = e.cln1_patic ";
            sql += "LEFT JOIN zinfo_a_07  f  ON a.pif_id = f.pat_id ";
            sql += "LEFT JOIN (SELECT pat_id, MAX(dat_3) AS next_visit_date FROM zinfo_p_06 group by pat_id) b ON a.pif_id=b.pat_id ";
            sql += "LEFT JOIN BI_SUM_View g ON a.pif_id = g.pat_no ";
            sql += "WHERE 1=1";
            if (!string.IsNullOrEmpty(Text_Name.Text)) //姓名篩選
                sql += " and a.pif_name like '%" + Text_Name.Text + "%'";
            if (!string.IsNullOrEmpty(Cbo_Gender.Text)) //姓別篩選
                sql += " and a.pif_sex ='" + GetComboBoxValue(Cbo_Gender) + "'";
            if (!string.IsNullOrEmpty(Text_ID.Text)) //身分證號篩選
                if (Text_ID.Text.Substring(0, 1) != "#")
                    sql += " and a.pif_ic like '%" + Text_ID.Text + "%'";
                else
                    sql += " and a.pif_id='" + Text_ID.Text.Substring(1) + "'";
            if (Common.GetComboBoxText(cbo_Status).ToString() != "")
            {
                sql += " AND f.opt_1=" + Common.GetComboBoxValue(cbo_Status).ToString();
            }
            else
            {
                sql += " AND f.opt_1 IS Null";
            }
            sql += " ORDER BY a.pif_id ";

            DataTable dt = db.Query(sql);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }
        #endregion

        #region 使用当前病患，測試用
        protected void btn_QueryNow_Click(object sender, DirectEventArgs e)
        {
            if (Text_Name.Text == "ii")
            {
                Text_Name.Text = "";
                Window win = new Window
                {
                    ID = "window_new",
                    Title = "iPad 模拟画面",
                    Width = Unit.Pixel(1000),
                    Height = Unit.Pixel(800),
                    Modal = true,
                    Loader = new ComponentLoader
                    {
                        Url = "ipad_Default.aspx",
                        Mode = LoadMode.Frame,
                        LoadMask =
                        {
                            ShowMask = true
                        }
                    }
                };
                win.Render(this.Form); 
            }
            else
            {
                show_now();
            }
        }
        #endregion

        #region 當前病患篩選
        protected void show_now() //沒有_PAT_ID的時候, 用這個函式來顯示頁面(不論是歷史病患或是當前病患)
        {
            DateTime datetime = DateTime.Now;
            int iOrn = 0;
            string weekType = (0 == (iOrn = datetime.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString();
            String date = datetime.ToString("yyyy-MM-dd");
            String sql;

            sql = "SELECT p.pif_id, P.pif_name, if (P.pif_sex = 'M','男','女') as sex, P.pif_dob, P.pif_ic, ";
            sql += "if (b.next_visit_date > '','腹透','血透') as txt_10, ";
            sql += "b.next_visit_date AS FirstDate, e.dat_9 AS InfoDate, ";
            sql += "if (g.BI > 0,'V',' ') as txt_101, ";
            sql += "case f.opt_1  when '1' then '退出' when '2' then '移植' when '3' then '转出' when '4' then '死亡' when '5' then '转入' else '' end AS opt_52, ";
            sql += "f.info_date, p.pif_docname ";
            sql += "FROM mac_setup M ";
            sql += "LEFT JOIN appointment_setup A ON M.mac_flr  = A.apptst_flr ";
            sql += "AND M.mac_sec = A.apptst_sec AND M.mac_bedno = A.apptst_bed AND A.apptst_daytyp='" + weekType + "'";
            sql += "LEFT JOIN pat_visit         V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + date + "'";
            sql += "LEFT JOIN pat_info          P ON A.apptst_patic=P.pif_ic ";
            sql += "LEFT JOIN zinfo_f_012       e ON P.pif_id = e.pat_id ";
            sql += "LEFT JOIN zinfo_a_07        f ON P.pif_id = f.pat_id ";
            sql += "LEFT JOIN (SELECT pat_id, MAX(dat_3) AS next_visit_date FROM zinfo_p_06 group by pat_id) b ON P.pif_id=b.pat_id ";
            sql += "LEFT JOIN BI_SUM_View       g ON P.pif_id = g.pat_no ";
            sql += "WHERE P.pif_id IS NOT NULL ";
            sql += "ORDER BY A.apptst_timetyp, M.mac_flr, M.mac_bedno";
            DataTable dt = db.Query(sql);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }
        #endregion

        #region [病患瀏覽]選擇一位病患
        protected void Select_PatInfo(object sender, DirectEventArgs e)
        {
            _PAT_IC = e.ExtraParams["pat_ic"].ToString();
            _PIF_NAME = e.ExtraParams["pif_name"].ToString();
            _USER_NAME = e.ExtraParams["pif_docname"].ToString();
            _PIF_SX = e.ExtraParams["pif_sex"].ToString() + "性";
            _PAT_ID = e.ExtraParams["pat_id"].ToString();
            Session["PAT_ID"] = _PAT_ID;
            Refreach_Label();

            url = "Information/Dialysis_06.aspx";
            Session["redirect"] = url;
            if (_PAT_IC == "" && _PAT_ID == "")
            {
                PatInfo_Query();
            }
            else
            {
                DirectLoad();
            }
        }
        #endregion

        #region 超过或低于标准参考值
        protected void BioIndicators(object sender, DirectEventArgs e)
        {
            Session["PAT_ID"] = e.ExtraParams["pat_id"].ToString();
            Window2.Show();
            Window2.Loader.SuspendScripting();
            Window2.Loader.Url = "Information/Biochemical_Indicators_list.aspx";
            Window2.Loader.DisableCaching = true;
            Window2.LoadContent();
        }
        #endregion

        #region 當月使用藥品耗材查詢
        protected void Dialysis_13(object sender, DirectEventArgs e)
        {
            //_PAT_IC = e.ExtraParams["pat_ic"].ToString();
            //_PIF_NAME = e.ExtraParams["pif_name"].ToString();
            Session["PAT_IC"] = e.ExtraParams["pat_ic"].ToString();
            Session["PAT_NAME"] = e.ExtraParams["pif_name"].ToString();
            //string USER_ID = Session["USER_ID"].ToString();
            //string usertype = Session["USER_RIGHT"].ToString();
            Window5.Show();
            Window5.Loader.SuspendScripting();
            Window5.Loader.Url = "Information/Dialysis_13_new.aspx"; //?PAT_IC=" + _PAT_IC + "&PAT_NAME=" + _PIF_NAME + "&USER_ID=" + USER_ID + "&USER_TYPE=" + usertype;
            Window5.Loader.DisableCaching = true;
            Window5.LoadContent();
        }
        #endregion

        #region 临床小帮手
        protected void Dialysis_help(object sender, DirectEventArgs e)
        {
            if (helpwiz == "true")
            {
                _PAT_IC = e.ExtraParams["pat_ic"].ToString();
                _PIF_NAME = e.ExtraParams["pif_name"].ToString();
                Window3.Show();
                Window3.Loader.SuspendScripting();
                Window3.Loader.Url = "Information/Dialysis_help.aspx";
                Window3.Loader.DisableCaching = true;
                Window3.LoadContent();
            }
            else
            {
                ImageCommand ic = new ImageCommand();
                if (ic.CommandName == "DoctorHelp")
                    ic.Icon = Icon.BinEmpty;
            }
        }
        #endregion

        #region 更新選擇病患顯示 Refreach_Label
        protected void Refreach_Label()
        {
            if (String.IsNullOrEmpty(_PAT_IC) == false)
            {
                String i18nName = i18nMessage.GetMessage("Name") + ": ";
                String i18nSex = i18nMessage.GetMessage("Sex") + ": ";
                String i18nAge = i18nMessage.GetMessage("Age") + ": ";
                String i18nSocialId = i18nMessage.GetMessage("SocialId") + ": ";
                String i18nDoctor = i18nMessage.GetMessage("Doctor") + ": ";
                i18nName += _PIF_NAME + " ";
                i18nSocialId += _PAT_IC + " ";
                i18nSex += _PIF_SX + " ";
                i18nDoctor += _PatDocName + " ";

                Lab_name.Text = i18nName;
                Lab_patid.Text = i18nSocialId;
                Lab_sex.Text = i18nSex;
                Lab_docname.Text = i18nDoctor;
            }
        }
        #endregion

        protected void Show_Checkin()
        {
            sDATE.Text = DateTime.Now.ToString("yyyy-MM-dd");
            GetWeek();
            Label_Date.Text = sDATE.Text + " " + txtWEEK.Text;

            Show_TimeSec(); //顯示時段  
            Show_FloorArea(); //顯示樓層，床區 
            FormPanel2.Enabled = true;
            TextQuery.Focus(true, 100);
        }

        #region 顯示時段
        private void Show_TimeSec()
        {
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

        #region 取得星期
        protected void GetWeek()
        {
            DateTime date1 = DateTime.Parse(sDATE.Text);
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
            Ext.Net.ListItem litem;
            litem = new Ext.Net.ListItem("全区", "全区");
            this.cboAREA.Items.Add(litem);
            for (int i = 0; i < dv.Count; i++)
            {
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
                    sAREA.Text = "全区";
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
                this.cboFLOOR.Items.Add(litem);
            }
            this.cboFLOOR.GetStore().DataBind();
            if (dtFLOOR.Rows.Count > 0)
            {
                this.cboFLOOR.Disabled = false;
                this.cboFLOOR.Select(sFLOOR.Text);
                FILL_AREA(sFLOOR.Text, sAREA.Text);
                FILL_BED();
            }
            else
            {
                this.cboFLOOR.Disabled = true;
            }
            db.myConnection.Close();
        }
        #endregion

        protected void cboTIME_Click(object sender, DirectEventArgs e)
        {
            sTIME.Text = Common.GetComboBoxValue(this.cboTIME);
            FILL_BED();
        }

        protected void cboFLOOR_Click(object sender, DirectEventArgs e)
        {
            sFLOOR.Text = Common.GetComboBoxValue(this.cboFLOOR);
            FILL_BED();
        }

        protected void cboAREA_Click(object sender, DirectEventArgs e)
        {
            sAREA.Text = Common.GetComboBoxValue(this.cboAREA);
            FILL_BED();
        }

        protected void REFRESH_BED(object sender, StoreReadDataEventArgs e)
        {
            FILL_BED();
        }

        protected void FILL_BED()
        {
            string sp_pic = "data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAAEAAAABCAIAAACQd1PeAAAAAXNSR0IArs4c6QAAAARnQU1BAACxjwv8YQUAAAAJcEhZcwAADsMAAA7DAcdvqGQAAAAadEVYdFNvZnR3YXJlAFBhaW50Lk5FVCB2My41LjEwMPRyoQAAAAxJREFUGFdj+P//PwAF/gL+pzWBhAAAAABJRU5ErkJggg==";
            GetWeek();
            string sSQL = "";
            sSQL = "SELECT M.mac_sec AS AREA, M.mac_bedno AS BED_NO, ";
            sSQL += "CASE ";
            sSQL += "WHEN EXISTS(SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE.Text + "') ";
            sSQL += "THEN (SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE.Text + "') ";
            sSQL += "ELSE (SELECT cln1_col2 FROM clinical1_doc_henan WHERE cln1_patic=A.apptst_patic AND cln1_diadate='base') ";
            sSQL += "END AS MAC_MODEL, ";
            sSQL += "M.mac_typ AS MAC_TYPE, ";
            sSQL += "case UPPER(M.mac_status) when 'Y' then '正常' when 'N' then '保养中' end AS MAC_STATE, ";
            sSQL += "P.pif_name AS PERSON_NAME, A.apptst_patic AS PERSON_IC, ";
            sSQL += "case P.pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX, ";
            sSQL += "P.pif_height AS PERSON_HEIGHT, ";
            sSQL += "IFNULL(N.cln1_col5, V.pv_weight) AS PERSON_WEIGHT, ";
            sSQL += "IF(STRCMP(N.cln1_col5,''), 'images/tick_16.png', '" + sp_pic + "') as img_url, ";
            sSQL += "case V.pv_macstat when '' then '" + sp_pic + "' when 'A' then 'images/start_16.png' when 'S' then 'images/stop_16.png' end as PERSON_STATE, ";
            sSQL += "P.pif_id AS PERSON_ID ";
            sSQL += "FROM mac_setup M ";
            sSQL += "LEFT JOIN appointment_setup A ON M.mac_flr=A.apptst_flr ";
            sSQL += "AND M.mac_sec=A.apptst_sec AND M.mac_bedno=A.apptst_bed AND A.apptst_daytyp='" + sWEEK.Text + "' AND A.apptst_timetyp='" + sTIME.Text + "' ";
            sSQL += "LEFT JOIN pat_info P ON A.apptst_patic=P.pif_ic ";
            sSQL += "LEFT JOIN clinical1_nurse N ON A.apptst_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE.Text + "' "; //淨化參數表格
            sSQL += "LEFT JOIN general_setup G ON M.mac_brand=G.genst_code AND G.genst_ctg='macbrd' ";
            sSQL += "LEFT JOIN pat_visit V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + sDATE.Text + "' ";
            sSQL += "WHERE M.mac_flr='" + sFLOOR.Text + "' ";
            if (cb_patlist.Text != "")
            {
                sSQL += "AND P.pif_name LIKE '" + cb_patlist.Text + "%' ";
            }
            if (sAREA.Text.Trim() != "全区")
            {
                sSQL += "AND M.mac_sec='" + sAREA.Text + "' ";
            }
            sSQL += "ORDER BY M.mac_bedno";
            DataTable dt = db.Query(sSQL);

            //補上更換床資料
            sSQL = "SELECT A.ah_bed, A.ah_patic AS PERSON_IC, A.ah_flr, A.ah_sec, A.ah_bed, A.ah_timetyp, P.pif_name AS PERSON_NAME, ";
            sSQL += "case P.pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX, ";
            sSQL += "P.pif_height AS PERSON_HEIGHT, ";
            sSQL += "N.cln1_col5 AS PERSON_WEIGHT, ";
            sSQL += "IF(STRCMP(N.cln1_col5,''), 'images/tick_16.png', '" + sp_pic + "') as img_url, ";
            sSQL += "case V.pv_macstat when '' then '" + sp_pic + "' when 'A' then 'images/start_16.png' when 'S' then 'images/stop_16.png' end as PERSON_STATE, ";
            sSQL += "CASE ";
            sSQL += "WHEN EXISTS(SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.ah_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE.Text + "') ";
            sSQL += "THEN (SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.ah_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE.Text + "') ";
            sSQL += "ELSE (SELECT cln1_col2 FROM clinical1_doc_henan WHERE A.ah_patic=cln1_patic AND cln1_diadate='base') ";
            sSQL += "END AS MAC_MODEL, ";
            sSQL += "P.pif_id AS PERSON_ID ";
            sSQL += "FROM appointment_change A ";
            sSQL += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
            sSQL += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND V.pv_datevisit='" + sDATE.Text + "' ";
            sSQL += "LEFT JOIN clinical1_nurse N ON A.ah_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE.Text + "' "; //淨化參數表格
            sSQL += "WHERE ah_date='" + sDATE.Text + "' AND ah_timetyp='" + sTIME.Text + "' ";
            DataTable dt2 = db.Query(sSQL);
            if (dt2.Rows.Count > 0)
            {
                System.Data.DataView dv = dt.DefaultView;

                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    dv.RowFilter = "BED_NO='" + dt2.Rows[i]["ah_bed"].ToString() + "' ";
                    if (dv.Count > 0)
                    {
                        if (dt2.Rows[i]["PERSON_ID"].ToString() != "")
                        {
                            dv[0]["PERSON_ID"] = dt2.Rows[i]["PERSON_ID"].ToString();
                        }
                        dv[0]["PERSON_IC"] = dt2.Rows[i]["PERSON_IC"].ToString();
                        dv[0]["PERSON_NAME"] = dt2.Rows[i]["PERSON_NAME"].ToString();
                        dv[0]["PERSON_SEX"] = dt2.Rows[i]["PERSON_SEX"].ToString();
                        dv[0]["PERSON_HEIGHT"] = dt2.Rows[i]["PERSON_HEIGHT"].ToString();
                        dv[0]["PERSON_WEIGHT"] = dt2.Rows[i]["PERSON_WEIGHT"].ToString();
                        dv[0]["PERSON_STATE"] = dt2.Rows[i]["PERSON_STATE"].ToString();
                        dv[0]["img_url"] = dt2.Rows[i]["img_url"].ToString();
                        dv[0]["PERSON_STATE"] = dt2.Rows[i]["PERSON_STATE"].ToString();
                        dv[0]["MAC_MODEL"] = dt2.Rows[i]["MAC_MODEL"].ToString();
                    }
                }
            }

            if (CheckinEmpty == "false")
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    if (dt.Rows[i]["PERSON_NAME"].ToString() == "")
                        dt.Rows[i].Delete();
                }
                dt.AcceptChanges();
            }
            Session.Add("PAD_TIME", sTIME.Text);
            Session.Add("PAD_FLOOR", sFLOOR.Text);
            Session.Add("PAD_AREA", sAREA.Text);
            ROW_CNT.Text = dt.Rows.Count.ToString();

            Store istore = grdBED_LIST.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();

            TextQuery.Focus(false, 100);
        }

        protected void RowSelect(object sender, DirectEventArgs e)
        {
            //Panel_Left.Collapse();
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            string pic = selRow[0]["PERSON_IC"].ToString();
            string pname = selRow[0]["PERSON_NAME"].ToString();
            string bedno = selRow[0]["BED_NO"];
            string mactype = selRow[0]["MAC_TYPE"];
            string area = selRow[0]["AREA"];
            string pid = selRow[0]["PERSON_ID"].ToString();
            string weight = selRow[0]["PERSON_WEIGHT"].ToString();
            if (pic != "")
            {
                _PAT_ID = pid;
                _PAT_IC = pic;
                Session["PAT_IC"] = pic;
                _PIF_NAME = pname;
                _USER_NAME = Session["USER_NAME"].ToString();
                FormPanel2.Enabled = false;

                Panel_Loader1.Hidden = false;
                Panel_Loader2.Hidden = true;
                Panel_Loader3.Hidden = true;
                Store istore = grdBED_LIST.GetStore();
                istore.RemoveAll();

                string url = "checkin/Patient_detail.aspx?personid=" + pic + "&patient_name=" + pname + "&machine_type=" + mactype + "&floor=" + sFLOOR.Text;
                url += "&area=" + area + "&time=" + sTIME.Text + "&bedno=" + bedno + "&daytyp=" + sWEEK.Text + "&sdate=" + sDATE.Text + "&patient_weight=" + weight;

                Panel_Loader1.Loader.SuspendScripting();
                Panel_Loader1.Loader.Url = url;
                Panel_Loader1.Loader.DisableCaching = true;
                Panel_Loader1.LoadContent();
                Refreach_Label();
            }
        }

        #region 刷IC卡後，自動進入報到明細
        protected void BtnQuery_Click(object sender, DirectEventArgs e)
        {
            string sql = "SELECT a.pif_ic, b.apptst_patrefid, b.apptst_flr, b.apptst_sec, b.apptst_bed, b.apptst_mactyp, b.apptst_wktyp, b.apptst_daytyp, b.apptst_timetyp FROM pat_info a ";
            sql += "LEFT JOIN appointment_setup b ON a.pif_ic=b.apptst_patic ";
            sql += "WHERE pif_mrn='" + TextQuery.Text + "' ";
            sql += "AND b.apptst_flr='" + sFLOOR.Text + "' ";
            sql += "AND b.apptst_timetyp='" + sTIME.Text + "' ";
            sql += "AND b.apptst_daytyp='" + sWEEK.Text + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                string pid = dt.Rows[0]["pif_ic"].ToString();
                string pname = dt.Rows[0]["apptst_patrefid"].ToString();
                string bedno = dt.Rows[0]["apptst_bed"].ToString();
                string mactype = dt.Rows[0]["apptst_mactyp"].ToString();
                string area = dt.Rows[0]["apptst_sec"].ToString();
                Common._NotificationShow("开始报到");
                string url = "checkin/Patient_detail.aspx?personid=" + pid + "&patient_name=" + pname + "&machine_type=" + mactype + "&floor=" + sFLOOR.Text;
                url += "&area=" + area + "&time=" + sTIME.Text + "&bedno=" + bedno + "&daytyp=" + sWEEK.Text + "&sdate=" + sDATE.Text;

                _PAT_IC = pid;
                Session["PAT_IC"] = pid;
                _PIF_NAME = pname;
                _USER_NAME = Session["USER_NAME"].ToString();
                FormPanel2.Enabled = false;

                Panel_Loader1.Hidden = false;
                Panel_Loader2.Hidden = true;
                Panel_Loader3.Hidden = true;
                Store istore = grdBED_LIST.GetStore();
                istore.RemoveAll();

                Panel_Loader1.Loader.SuspendScripting();
                Panel_Loader1.Loader.Url = url;
                Panel_Loader1.Loader.DisableCaching = true;
                Panel_Loader1.LoadContent();
                Refreach_Label();
            }
            else
            {
                Common._NotificationShow("查无此人或已逾时" + TextQuery.Text);
            }
            TextQuery.Text = "";
            TextQuery.Focus(false, 100);
        }
        #endregion

        protected void BtnSearch_Click(object sender, DirectEventArgs e)
        {
            if (cb_patlist.Text.Length == 10)
            {
                BtnQuery_Click(sender, e);
            }
            else
            {
                FILL_BED();
            }
        }

        protected void BtnReset_Click(object sender, DirectEventArgs e)
        {
            Text_ID.Text = "";
            Cbo_Gender.Text = "";
            cbo_Status.Text = "";
            Text_Name.Text = "";
            Text_Name.Focus(false, 100);
        }

        protected void OnbtnPrint_Click(object sender, DirectEventArgs e)
        {
            GetWeek();
            PrintWindow.Show();
            PrintWindow.Loader.SuspendScripting();
            PrintWindow.Loader.Url = "report/Report_Dialysis_h.aspx?_REPORT_NAME=checkin&_INFO_DATE=" + sDATE.Text + "&_REPORT_P=" + sWEEK.Text + "&_REPORT_sQM=" + sFLOOR.Text;
            PrintWindow.Loader.DisableCaching = true;
            PrintWindow.LoadContent();
        }

        public string ConvertDataTabletoString(DataTable dt)
        {
            System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            List<Dictionary<string, object>> rows = new List<Dictionary<string, object>>();
            Dictionary<string, object> row;
            foreach (DataRow dr in dt.Rows)
            {
                row = new Dictionary<string, object>();
                foreach (DataColumn col in dt.Columns)
                {
                    row.Add(col.ColumnName, dr[col]);
                }
                rows.Add(row);
            }
            return serializer.Serialize(rows);
        }
    }
}