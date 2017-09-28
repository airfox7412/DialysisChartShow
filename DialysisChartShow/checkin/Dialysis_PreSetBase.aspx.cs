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
    public partial class Dialysis_PreSetBase : BaseForm
    {
        string docname;
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (Session["USER_NAME"] == null)
                X.Redirect("login.aspx");
            else
                docname = Session["USER_NAME"].ToString();

            if (!X.IsAjaxRequest)
            {
                Patient_ID.Text = Request.QueryString["personid"];
                sDATE.Text = Request.QueryString["sdate"];
                text_info_date.Text = sDATE.Text;
                Show_Person();
                
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
            sSQL += "pif_dob AS PERSON_AGE, pif_name, pif_imgloc ";
            sSQL += "FROM pat_info WHERE pif_ic='" + Patient_ID.Text + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                Tex_Patient_Name.Text = dt.Rows[0]["pif_name"].ToString();
                Tex_Patient_Gender.Text = dt.Rows[0]["PERSON_SEX"].ToString();
                int thisyear = int.Parse(DateTime.Now.ToString("yyyy"));
                int age = thisyear - int.Parse(dt.Rows[0]["PERSON_AGE"].ToString().Substring(0,4));
                Tex_Patient_Age.Text = age.ToString();

                string ipath = "../";
                string iimage = dt.Rows[0]["pif_imgloc"].ToString().Replace("./", "");
                if (iimage == "")
                {
                    if (Tex_Patient_Gender.Text == "男" || Tex_Patient_Gender.Text == "")
                        iimage = "images/male.png";
                    else
                    {
                        iimage = "images/female.png";
                    }
                } 
                Image1.ImageUrl = ipath + iimage;
            }
        }
        #endregion
        
        protected void GetComboxData()
        {
            string sSQL = "SELECT hp2_code AS CODE, hp2_name AS NAME FROM hpack2_setup WHERE hp2_status='Y'"; //透析器型號
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_machine_model, dt1, false, "NAME", "CODE");
            Common.SetComboBoxItem(cbo_machine_model2, dt1, true, "NAME", "CODE");
            cbo_machine_model.Select(0);
            cbo_machine_model2.Select(0); 
            
            sSQL = "SELECT hp3_code AS CODE, hp3_name AS NAME FROM hpack3_setup WHERE hp3_status='Y'"; //管路型號
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_Tube, dt1, false, "NAME", "CODE");
            Common.SetComboBoxItem(cbo_Tube2, dt1, true, "NAME", "CODE");
            cbo_Tube.Select(0);
            cbo_Tube2.Select(0); 

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='dialysis_type' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_Machinetype, dt1, false, "NAME", "CODE");
            cbo_Machinetype.Select(0);
            
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup WHERE pck_status='Y'";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt1, false, "NAME", "CODE");
            cbo_h_type.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='heparin' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBox10, dt1, false, "NAME", "CODE");
            SelectBox10.Select(0);
        }

        protected void ShowDialysis()
        {
            string sql;
            sql = "SELECT * FROM clinical1_doc_henan ";
            sql += "WHERE cln1_patic='" + Patient_ID.Text + "' AND cln1_diadate='base' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count == 0)
            {
                sql = "SELECT a.* FROM clinical1_nurse a ";
                sql += "WHERE a.cln1_patic='" + Patient_ID.Text + "' AND a.cln1_diadate <> '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                sql += "ORDER BY a.cln1_diadate DESC LIMIT 1";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    Common.SetComboBoxValue(cbo_machine_model, dt.Rows[0]["cln1_col26"].ToString(), false); //透析器型号
                    Common.SetComboBoxValue(cbo_machine_model2, dt.Rows[0]["cln1_col4"].ToString(), false); //透析器型号2
                    Common.SetComboBoxValue(cbo_Tube, dt.Rows[0]["cln1_col27"].ToString(), false); //管路型号
                    Common.SetComboBoxValue(cbo_Tube2, dt.Rows[0]["cln1_col47"].ToString(), false); //管路型号2

                    Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col27"].ToString(), false); //血管通路類型
                    Common.SetComboBoxValue(cbo_Machinetype, dt.Rows[0]["cln1_col3"].ToString(), false); //透析方式
                    Common.SetComboBoxValue(SelectBox10, dt.Rows[0]["cln1_col15"].ToString(), false); //抗凝药物
                    txt_weight_after_expect.Text = dt.Rows[0]["cln1_col6"].ToString(); //干体重
                    TextField3.Text = dt.Rows[0]["cln1_col7"].ToString(); //目标定容量
                    TextField8.Text = dt.Rows[0]["cln1_col13"].ToString(); //首次剂量
                    TextAdd.Text = dt.Rows[0]["cln1_col14"].ToString(); //追加量
                    TextAmount.Text = dt.Rows[0]["cln1_col17"].ToString(); //总量
                    TextField6.Text = dt.Rows[0]["cln1_col28"].ToString();
                    TextField7.Text = dt.Rows[0]["cln1_col29"].ToString();
                    TextField9.Text = dt.Rows[0]["cln1_col30"].ToString();
                    TextField10.Text = dt.Rows[0]["cln1_col31"].ToString();
                }
                text_info_date.RemoveCls("Text-blue");
                text_info_date.AddCls("Text-red");
            }
            else
            {
                Common.SetComboBoxValue(cbo_machine_model, dt.Rows[0]["cln1_col2"].ToString(), false); //透析器型号
                Common.SetComboBoxValue(cbo_machine_model2, dt.Rows[0]["cln1_col14"].ToString(), false); //透析器型号2
                Common.SetComboBoxValue(cbo_Tube, dt.Rows[0]["cln1_col15"].ToString(), false); //管路型号
                Common.SetComboBoxValue(cbo_Tube2, dt.Rows[0]["cln1_col16"].ToString(), false); //管路型号2

                Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col1"].ToString(), false); //血管通路類型
                Common.SetComboBoxValue(cbo_Machinetype, dt.Rows[0]["cln1_col3"].ToString(), false); //透析方式
                Common.SetComboBoxValue(SelectBox10, dt.Rows[0]["cln1_col5"].ToString(), false); //抗凝药物
                txt_weight_after_expect.Text = dt.Rows[0]["cln1_col4"].ToString(); //干体重
                TextField3.Text = dt.Rows[0]["cln1_col6"].ToString(); //目标定容量
                TextField8.Text = dt.Rows[0]["cln1_col7"].ToString(); //首次剂量
                TextAdd.Text = dt.Rows[0]["cln1_col8"].ToString(); //追加量
                TextAmount.Text = dt.Rows[0]["cln1_col9"].ToString(); //总量
                TextField6.Text = dt.Rows[0]["cln1_col10"].ToString();
                TextField7.Text = dt.Rows[0]["cln1_col11"].ToString();
                TextField9.Text = dt.Rows[0]["cln1_col12"].ToString();
                TextField10.Text = dt.Rows[0]["cln1_col13"].ToString();
            }
            if (TextField6.Text.Trim() == "")
                TextField6.Text = "2.0";
            if (TextField7.Text.Trim() == "")
                TextField7.Text = "1.5";
            if (TextField9.Text.Trim() == "")
                TextField9.Text = "35";
            if (TextField10.Text.Trim() == "")
                TextField10.Text = "138";
            dt.Dispose();
            db.myConnection.Close();
        }

        protected void BtnSave_Click(object sender, DirectEventArgs e)
        {
            string sql;
            sql = "SELECT cln1_diadate FROM clinical1_doc_henan ";
            sql += "WHERE cln1_patic='" + Patient_ID.Text + "' AND cln1_diadate='base' ";
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
                sql += "cln1_col13 = '" + TextField10.Text + "', ";
                sql += "cln1_col14 = '" + Common.GetComboBoxText(cbo_machine_model2) + "',"; //透析器型号2
                sql += "cln1_col15 = '" + Common.GetComboBoxText(cbo_Tube) + "', "; //管路型號
                sql += "cln1_col16 = '" + Common.GetComboBoxText(cbo_Tube2) + "' "; //管路型號2
                sql += "WHERE cln1_patic='" + Patient_ID.Text + "' AND cln1_diadate='base' ";
            }
            else
            {
                sql = "INSERT INTO clinical1_doc_henan(cln1_patic,cln1_diadate,";
                sql += "cln1_col1, cln1_col2, cln1_col3, cln1_col4, cln1_col5,";
                sql += "cln1_col6, cln1_col7, cln1_col8, cln1_col9,";
                sql += "cln1_col10, cln1_col11, cln1_col12, cln1_col13, ";
                sql += "cln1_col14, cln1_col15, cln1_col16) ";
                sql += "VALUES('" + Patient_ID.Text + "', 'base', ";
                sql += "'" + Common.GetComboBoxText(cbo_h_type) + "', ";
                sql += "'" + Common.GetComboBoxText(cbo_machine_model) + "', "; //透析器型号
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
                sql += "'" + TextField10.Text + "', ";
                sql += "'" + Common.GetComboBoxText(cbo_machine_model2) + "', "; //透析器型号2
                sql += "'" + Common.GetComboBoxText(cbo_Tube) + "', "; //管路型號
                sql += "'" + Common.GetComboBoxText(cbo_Tube2) + "') "; //管路型號2
            }
            db.Excute(sql);

            //更新pat_info
            sql = "UPDATE pat_info " +
                    "SET pif_hpack='" + Common.GetComboBoxValue(cbo_h_type) + "', " +
                        "pif_hpack2='" + Common.GetComboBoxValue(cbo_machine_model) + "', " +
                        "pif_hpack3='" + Common.GetComboBoxValue(cbo_Tube) + "', " +
                        "pif_hpack4='" + Common.GetComboBoxValue(cbo_machine_model2) + "', " +
                        "pif_hpack5='" + Common.GetComboBoxValue(cbo_Tube2) + "' " +
                    "WHERE pif_ic='" + Patient_ID.Text + "' ";
            db.Excute(sql);

            text_info_date.RemoveCls("Text-red");
            text_info_date.AddCls("Text-blue");
            _NotificationShow("<font size=4>储存成功!</font>");
        }

        protected new void _NotificationShow(string myMessage)
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
               
        protected void OnChangeType(object sender, DirectEventArgs e)
        {
            CheckType();
        }

        protected void CheckType()
        {
            if (cbo_Machinetype.Text == "HD+HP" || cbo_Machinetype.Text == "HDF+HP")
            {
                cbo_machine_model2.ReadOnly = false;
            }
            else
            {
                cbo_machine_model2.ReadOnly = true;
                cbo_machine_model2.Select(0);
            }
        }
    }
}