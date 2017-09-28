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

namespace Dialysis_Chart_Show.checkin
{
    public partial class Dialysis_PreSetEdit : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();

        string docname;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_NAME"] == null)
                X.Redirect("index.aspx");
            else
                docname = Session["USER_NAME"].ToString();

            if (!X.IsAjaxRequest)
            {
                Patient_ID.Text = Request.QueryString["personid"];
                sDATE.Text = Request.QueryString["sdate"];
                text_info_date.Text = sDATE.Text;
                Show_Person();
                Show_image();
                
                hpack.Text = cbo_h_type.Text;
                string sql = "SELECT COUNT(*) as cnts FROM clinical1_nurse WHERE cln1_patic='" + Patient_ID.Text + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                    DialysisTimes.Text = dt.Rows[0]["cnts"].ToString();
                else
                    DialysisTimes.Text = "0";

                sql = "SELECT pif_hpacks1 FROM pat_info WHERE pif_ic='" + Patient_ID.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    if (dt.Rows[0]["pif_hpacks1"].ToString() == "")
                        TextBaseTimes.Text = "0";
                    else
                        TextBaseTimes.Text = dt.Rows[0]["pif_hpacks1"].ToString();
                }
                if (TextBaseTimes.Text == null)
                    TextBaseTimes.Text = "0";
                TextTimes.Text = (int.Parse(TextBaseTimes.Text) + int.Parse(DialysisTimes.Text)).ToString();
                
                GetComboxData(); //取得各項COMBOBOX資料
                
                if (Session["info_date"] != null)
                    text_info_date.Text = Session["info_date"].ToString();
                ShowDialysis();
            }
        }

        #region 顯示病患資料
        protected void Show_Person()
        {
            String sSQL;
            sSQL = "SELECT ";
            sSQL += "case pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX, ";
            sSQL += "pif_dob AS PERSON_AGE, pif_name ";
            sSQL += "FROM pat_info WHERE pif_ic='" + Patient_ID.Text + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                Tex_Patient_Name.Text = dt.Rows[0]["pif_name"].ToString();
                Tex_Patient_Gender.Text = dt.Rows[0]["PERSON_SEX"].ToString();
                int thisyear = int.Parse(DateTime.Now.ToString("yyyy"));
                int age = thisyear - int.Parse(dt.Rows[0]["PERSON_AGE"].ToString().Substring(0,4));
                Tex_Patient_Age.Text = age.ToString();
            }
            
            //pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text);

            //Common.SetComboBoxValue(cbo_Machinetype, machine_type.Text, true); //透析方式
            set_time_text();

            //if (cbo_h_type.Text.Trim() == "") //血管通路類型未設定或不正確
            //{
            //    if (pat_obj.pif_hpack == "")
            //        cbo_h_type.LabelCls = "blink";
            //}
            //else
            //    cbo_h_type.LabelCls = "my-Field";

            if (cbo_machine_model.Text.Trim() == "") //透析器型號未設定或不正確
                cbo_machine_model.LabelCls = "blink";
            else
                cbo_machine_model.LabelCls = "my-Field";
        }
        #endregion
        
        private void set_time_text()
        {
            if (time.Text == "001")
            {
                time.Text = "上午";
            }
            else if (time.Text == "002")
            {
                time.Text = "下午";
            }
            else if (time.Text == "003")
            {
                time.Text = "晚上";
            }
        }

        protected void Show_image()
        {
            string sql = "SELECT a.pif_sex, a.pif_imgloc  FROM pat_info a ";
            sql += "where a.pif_ic = '" + Patient_ID.Text + "' ";

            DataTable dt = db.Query(sql);
            if (dt.Rows.Count != 0)
            {
                string iimage = dt.Rows[0]["pif_imgloc"].ToString();
                iimage = iimage.Replace("./", "/myhaisv4/");

                string ipath = ConfigurationManager.AppSettings["pat_images"].ToString();
                Image1.ImageUrl = ipath + iimage;
            }
            else
            {
                Image1.ImageUrl = "/myhaisv4/images/male_256.png";
            }
        }

        protected void GetComboxData()
        {
            string sSQL = "SELECT hp2_code AS CODE, hp2_name AS NAME FROM hpack2_setup WHERE hp2_status='Y'"; //透析器型號
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_machine_model, dt1, false, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='dialysis_type' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_Machinetype, dt1, false, "NAME", "CODE");

            //sSQL = "SELECT hp_code AS CODE, hp_subgrp AS NAME FROM h_type"; //舊程式使用h_type與FLEX使用不同資料表
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup WHERE pck_status='Y'";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt1, false, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='heparin' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBox10, dt1, false, "NAME", "CODE");
            
            sSQL = "SELECT a.pv_floor, a.pv_sec, a.pv_bedno, a.time_type, a.appointment_date AS NAME FROM appointment a ";
            sSQL += "LEFT JOIN pat_info b ON a.pif_id=b.pif_id ";
            sSQL += "WHERE b.pif_ic='" + Patient_ID.Text + "' AND a.appointment_date='" + sDATE + "' ";
            sSQL += "ORDER BY a.appointment_date DESC";
            dt1 = db.Query(sSQL);
            if (dt1.Rows.Count > 0)
            {
                text_info_date.Text = dt1.Rows[0]["NAME"].ToString();
                floor.Text = dt1.Rows[0]["pv_floor"].ToString();
                area.Text = dt1.Rows[0]["pv_sec"].ToString();
                bedno.Text = dt1.Rows[0]["pv_bedno"].ToString();
                time.Text = dt1.Rows[0]["time_type"].ToString();
                set_time_text();
            }
        }

        protected void ShowDialysis()
        {
            string sql;
            sql = "SELECT * FROM clinical1_doc_henan ";
            sql += "WHERE cln1_patic='" + Patient_ID.Text + "' AND cln1_diadate='" + text_info_date.Text + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col1"].ToString(), false); //血管通路類型
                Common.SetComboBoxValue(cbo_machine_model, dt.Rows[0]["cln1_col2"].ToString(), false); //透析器型号
                Common.SetComboBoxValue(cbo_Machinetype, dt.Rows[0]["cln1_col3"].ToString(), false); //透析方式
                txt_weight_after_expect.Text = dt.Rows[0]["cln1_col4"].ToString(); //干体重
                Common.SetComboBoxValue(SelectBox10, dt.Rows[0]["cln1_col5"].ToString(), false); //抗凝药物
                TextField3.Text = dt.Rows[0]["cln1_col6"].ToString(); //目标定容量
                TextField8.Text = dt.Rows[0]["cln1_col7"].ToString(); //首次剂量
                TextAdd.Text = dt.Rows[0]["cln1_col8"].ToString(); //追加量
                TextAmount.Text = dt.Rows[0]["cln1_col9"].ToString(); //总量
                TextField6.Text = dt.Rows[0]["cln1_col10"].ToString();
                TextField7.Text = dt.Rows[0]["cln1_col11"].ToString();
                TextField9.Text = dt.Rows[0]["cln1_col12"].ToString();
                TextField10.Text = dt.Rows[0]["cln1_col13"].ToString(); 
            }
            else
            {
                sql = "SELECT * FROM clinical1_doc_henan ";
                sql += "WHERE cln1_patic='" + Patient_ID.Text + "' AND cln1_diadate='base' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col1"].ToString(), false); //血管通路類型
                    Common.SetComboBoxValue(cbo_machine_model, dt.Rows[0]["cln1_col2"].ToString(), false); //透析器型号
                    Common.SetComboBoxValue(cbo_Machinetype, dt.Rows[0]["cln1_col3"].ToString(), false); //透析方式
                    txt_weight_after_expect.Text = dt.Rows[0]["cln1_col4"].ToString(); //干体重
                    Common.SetComboBoxValue(SelectBox10, dt.Rows[0]["cln1_col5"].ToString(), false); //抗凝药物
                    TextField3.Text = dt.Rows[0]["cln1_col6"].ToString(); //目标定容量
                    TextField8.Text = dt.Rows[0]["cln1_col7"].ToString(); //首次剂量
                    TextAdd.Text = dt.Rows[0]["cln1_col8"].ToString(); //追加量
                    TextAmount.Text = dt.Rows[0]["cln1_col9"].ToString(); //总量
                    TextField6.Text = dt.Rows[0]["cln1_col10"].ToString();
                    TextField7.Text = dt.Rows[0]["cln1_col11"].ToString();
                    TextField9.Text = dt.Rows[0]["cln1_col12"].ToString();
                    TextField10.Text = dt.Rows[0]["cln1_col13"].ToString();
                }
                TextField6.Text = "2.0";
                TextField7.Text = "1.5";
                TextField9.Text = "35";
                TextField10.Text = "138";
                text_info_date.RemoveCls("Text-blue");
                text_info_date.AddCls("Text-red");
            }
            sql = "SELECT a.pv_floor, a.pv_sec, a.pv_bedno, a.time_type FROM appointment a ";
            sql += "LEFT JOIN pat_info b ON a.pif_id=b.pif_id ";
            sql += "WHERE b.pif_ic='" + Patient_ID.Text + "' AND appointment_date>='" + text_info_date.Text + "' ";
            sql += "ORDER BY a.pif_id";
            dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                floor.Text = dt.Rows[0]["pv_floor"].ToString();
                area.Text = dt.Rows[0]["pv_sec"].ToString();
                bedno.Text = dt.Rows[0]["pv_bedno"].ToString();
                time.Text = dt.Rows[0]["time_type"].ToString();
                set_time_text();
            }
            dt.Dispose();
            db.myConnection.Close();
        }

        protected void BtnSave_Click(object sender, DirectEventArgs e)
        {
            string sql;
            sql = "SELECT cln1_diadate FROM clinical1_doc_henan ";
            sql += "WHERE cln1_patic='" + Patient_ID.Text + "' AND cln1_diadate='" + text_info_date.Text + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "UPDATE clinical1_doc_henan ";
                sql += "SET cln1_col1 = '" + Common.GetComboBoxText(cbo_h_type) + "',"; //血管通路類型
                sql += "cln1_col2 = '" + Common.GetComboBoxText(cbo_machine_model) + "',"; //透析器型号
                sql += "cln1_col3 = '" + Common.GetComboBoxText(cbo_Machinetype) + "',"; //透析方式
                sql += "cln1_col4 = '" + txt_weight_after_expect.Text + "',"; //干体重
                sql += "cln1_col5 = '" + Common.GetComboBoxText(SelectBox10) + "',"; //抗凝药物
                sql += "cln1_col6 = '" + TextField3.Text + "',"; //目标定容量
                sql += "cln1_col7 = '" + TextField8.Text + "',"; //首次剂量
                sql += "cln1_col8 = '" + TextAdd.Text + "',"; //追加量
                sql += "cln1_col9 = '" + TextAmount.Text + "', "; //总量
                sql += "cln1_col10 = '" + TextField6.Text + "', ";
                sql += "cln1_col11 = '" + TextField7.Text + "', ";
                sql += "cln1_col12 = '" + TextField9.Text + "', ";
                sql += "cln1_col13 = '" + TextField10.Text + "' ";
                sql += "WHERE cln1_patic='" + Patient_ID.Text + "' AND cln1_diadate='" + text_info_date.Text + "' ";
            }
            else
            {
                sql = "INSERT INTO clinical1_doc_henan(cln1_patic,cln1_diadate,";
                sql += "cln1_col1, cln1_col2, cln1_col3, cln1_col4, cln1_col5,";
                sql += "cln1_col6, cln1_col7, cln1_col8, cln1_col9,";
                sql += "cln1_col10, cln1_col11, cln1_col12, cln1_col13) ";
                sql += "VALUES('" + Patient_ID.Text + "', '" + text_info_date.Text + "', ";
                sql += "'" + Common.GetComboBoxText(cbo_h_type) + "', ";
                sql += "'" + Common.GetComboBoxText(cbo_machine_model) + "', ";
                sql += "'" + Common.GetComboBoxText(cbo_Machinetype) + "', ";
                sql += "'" + txt_weight_after_expect.Text + "', ";
                sql += "'" + Common.GetComboBoxText(SelectBox10) + "', ";
                sql += "'" + TextField3.Text + "', ";
                sql += "'" + TextField8.Text + "', ";
                sql += "'" + TextAdd.Text + "', ";
                sql += "'" + TextAmount.Text + "', ";
                sql += "'" + TextField6.Text + "', ";
                sql += "'" + TextField7.Text + "', ";
                sql += "'" + TextField9.Text + "', ";
                sql += "'" + TextField10.Text + "') ";
            }
            db.Excute(sql);
            text_info_date.RemoveCls("Text-red");
            text_info_date.AddCls("Text-blue");
            _NotificationShow("<font size=4>储存成功!</font>");
        }

        protected void _NotificationShow(string myMessage)
        {
            Notification.Show( new NotificationConfig {
                Title = "系统信息",
                Icon = Ext.Net.Icon.Accept,
                AlignCfg = new NotificationAlignConfig
                {
                    OffsetX = 0,
                    OffsetY = -200                     
                },
                Html = "<font size='4'>" + myMessage + "</font>"
            });
        }

        public string a
        {
            get
            {
                try
                {
                    return Session["a"].ToString();
                }
                catch
                {
                    //_NotificationShow_TimeOut();
                    return "";
                }
            }
            set
            {
                Session.Add("a", value);
            }
        }

        public void ErrorMsgShow(string myMessage)
        {
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "错误信息",
                Message = myMessage,
                Width = 300,
                Buttons = MessageBox.Button.OK,
                Closable = false,
                Progress = false
            });
        }

        public object[] GetDataArray(DataTable dt)
        {
            object[] objx = new Object[dt.Rows.Count];
            int i = 0;

            foreach (DataRow irow in dt.Rows)
            {
                object[] objy = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    objy[j] = irow[j];
                }
                objx[i] = objy;
                i++;
            }
            return objx;
        }
    }
}