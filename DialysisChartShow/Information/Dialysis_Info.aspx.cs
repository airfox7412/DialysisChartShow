using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;
using System.Data;
using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_Info : BaseForm
    {
        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        String userId;
        string sShowFHIR = ConfigurationManager.AppSettings["ShowFHIR"] == null ? "" : ConfigurationManager.AppSettings["ShowFHIR"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            //this.Lab_name.Hidden = false;
            //this.Lab_patid.Hidden = false;
            //this.Lab_sex.Hidden = false;
            //this.Lab_info_pif_docname.Hidden = false;
            //this.Lab_info_user_name.Hidden = false;
            //this.Button7.Text = sShowFHIR == "true" ? "统计资料上传" : "透析用水/液量测";

            if (!X.IsAjaxRequest)
            {
                try
                {
                    String sPC_HEAD = "logo001Big.jpg";
                    DataTable dtPC_HEAD = db.Query("SELECT *  FROM general_setup WHERE  genst_code='PC_HEAD'");
                    if (dtPC_HEAD.Rows.Count > 0)
                    {
                        sPC_HEAD = dtPC_HEAD.Rows[0]["genst_desc"].ToString();
                        //sPAD_HEAD = Server.MapPath(sPAD_HEAD);
                        //Image1.ImageUrl = sPC_HEAD;
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMsgShow(ex.Message.ToString());
                }
            }
            //
            // Set string for i18n
            //
            Message i18nMessage = new Message(Message.Language.zh_cn);
            String i18nName = i18nMessage.GetMessage("Name");
            String i18nSex = i18nMessage.GetMessage("Sex");
            String i18nAge = i18nMessage.GetMessage("Age");
            String i18nSocialId = i18nMessage.GetMessage("SocialId");
            String i18nDoctor = i18nMessage.GetMessage("Doctor");
            String i18nUser = i18nMessage.GetMessage("UserId");

            if (String.IsNullOrEmpty(_PAT_ID))
            {
                _PAT_ID = Request.QueryString["_PAT_ID"];
                _PAT_IC = Request.QueryString["_PAT_IC"];
                _PIF_NAME = Request.QueryString["_PIF_NAME"];
                _USER_NAME = Request.QueryString["_USER_NAME"];
                _PIF_SX = Request.QueryString["_PIF_SX"];

                _PatDocName = Request.QueryString["_PatDocName"];

                //string [] sINFO = HttpContext.Current.Server.UrlDecode(Request.Url.Query).Split(new Char [] {'&'}) ;
                //foreach (string s in sINFO)
                //{
                //    string[] sDATA = s.Split(new Char[] { '=' });
                //    if (sDATA[0].IndexOf("_PAT_ID") >= 0)
                //        _PAT_ID = sDATA[1];
                //    else if (sDATA[0].IndexOf("_PAT_IC") >= 0)
                //        _PAT_IC = sDATA[1];
                //    else if (sDATA[0].IndexOf("_PIF_NAME") >= 0)
                //        _PIF_NAME = sDATA[1];
                //    else if (sDATA[0].IndexOf("_USER_NAME") >= 0)
                //        _USER_NAME = sDATA[1];
                //    else if (sDATA[0].IndexOf("_PIF_SX") >= 0)
                //        _PIF_SX = sDATA[1];
                //    else if (sDATA[0].IndexOf("_USER_ID") >= 0)
                //        _UserID = sDATA[1];
                //}
                //if (String.IsNullOrEmpty(_PAT_ID))
                //{
                //    X.Redirect("Info_index.aspx");
                //}
                //else
                //{
                    //Lab_name.Text = i18nName + " : " + _PIF_NAME;
                    //Lab_patid.Text = i18nSocialId + " : " + _PAT_IC;
                    //Lab_sex.Text = i18nSex + " : " + _PIF_SX;
                    //Lab_info_pif_docname.Text = i18nDoctor + " : " + _PatDocName;
                    //Lab_info_user_name.Text = i18nUser + " : " + getUserName(_UserID);
                //}
            }
            else
            {
                //Lab_name.Text = i18nName + " : " + _PIF_NAME;
                //Lab_patid.Text = i18nSocialId + " : " + _PAT_IC;
                //Lab_sex.Text = i18nSex + " : " + _PIF_SX;
                //Lab_info_pif_docname.Text = i18nDoctor + " : " + _PatDocName;
                //Lab_info_user_name.Text = i18nUser + " : " + getUserName(_UserID);
            }
            Session["USER_NAME"] = getUserName(_UserID); //Alex 20151221
            Session["USER_ID"] = _UserID; //20160216 by ssi
        }

        /// <summary>
        /// 用_UserID去access_level這個table找使用人
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        static public String getUserName(String userId)
        {
            String result;
            String sql_stmt = "SELECT * FROM access_level WHERE acclv_stfcode='" + userId + "' ";
            DataTable dtUSER_NAME2 = tools.DBMysql.query(sql_stmt, out result);
            if (dtUSER_NAME2.Rows.Count > 0)
                return dtUSER_NAME2.Rows[0]["acclv_fname"].ToString();
            else
                return userId;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_House_click(object sender, EventArgs e)
        {
            X.Redirect(ConfigurationManager.AppSettings["hose"].ToString());
        }

        /// <summary>
        /// 血透病患总览
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Searck_click(object sender, EventArgs e)
        {
            userId = Session["USER_ID"].ToString();
            Session["PAT_ID"]="";
            X.Redirect("Info_index.aspx");
            //loadAspxFile("./Info_index.aspx");
        }

        /// <summary>
        /// 诊断信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_1_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_01.aspx");
        }

        /// <summary>
        /// 血透信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_2_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_02.aspx");
        }

        /// <summary>
        /// 治疗信息
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_3_click(object sender, EventArgs e)
        {
            //loadAspxFile("./drug_list.aspx"); 
            loadAspxFile("./drug_Tree.aspx");
        }

        /// <summary>
        /// 实验室及辅助检查
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_4_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_04.aspx");
        }

        /// <summary>
        /// 病程记录
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_5_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_05.aspx");
        }

        /// <summary>
        /// 血液净化首次病历
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_6_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_06.aspx");
        }

        /// <summary>
        /// 特殊资料, 目前沒有使用
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_7_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_07.aspx");
        }

        /// <summary>
        /// 基本信息索引
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_8_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_08.aspx");
        }

        /// <summary>
        /// 血液净化过程
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_9_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_09.aspx");
        }

        /// <summary>
        /// 质量分析统计
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_10_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_10.aspx");

            //this.Container2.Hidden = false;
            //this.Lab_name.Hidden = true;
            //this.Lab_patid.Hidden = true;
            //this.Lab_sex.Hidden = true;
            //this.Lab_info_pif_docname.Hidden = true;
            //this.Lab_info_user_name.Hidden = false;
        }

        /// <summary>
        /// 血透评估表
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_11_click(object sender, EventArgs e)
        {
            loadAspxFile("./Dialysis_0h.aspx");
        }
        
        /// <summary>
        /// 透析用水/液量测
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_12_click(object sender, EventArgs e)
        {
            string sPartaspx = sShowFHIR == "true" ? "./DialysisFhirClient.aspx" : "./Dialysis_12.aspx";
            loadAspxFile(sPartaspx);
        }

      /// <summary>
        /// 當月耗材查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_13_click(object sender, EventArgs e)
        {
            userId = Session["USER_ID"].ToString();
//            loadAspxFile("./Dialysis_13.aspx?sel_PAT_NO=" + _PAT_IC + "&USER_ID=" + userId);
            loadAspxFile("./Biochemical_Not_Checked_List.aspx");

        }

        /// <summary>
        /// 將每次載入aspx的動作重覆的部份抽離出一份function
        /// </summary>
        /// <param name="url"></param>
        private void loadAspxFile(String url)
        {
            //Panel_Center.Loader.SuspendScripting();
            //Panel_Center.Loader.Url = url;
            //Panel_Center.Loader.DisableCaching = true;
            //Panel_Center.LoadContent();
        }

        protected void logo_DblClick(object sender, EventArgs e)
        {
            X.Redirect(ConfigurationManager.AppSettings["hose"].ToString());
        }
    }
}