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
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Dialysis_Chart_Show
{
    public partial class Dialysis_09_01_Henan : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        string Col11, coltext11;
        string Col21, coltext21;
        string Col31, coltext31;
        string Col41, coltext41;
        string Col50;

        protected void Page_Load(object sender, EventArgs e)
        {         
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request["patient_id"].ToString();
                patient_name.Text = Request["patient_name"].ToString();
                machine_type.Text = Request["machine_type"].ToString(); 
                hpack.Text = Request["hpack"].ToString();
                floor.Text = Request["floor"].ToString();
                bedno.Text = Request["bedno"].ToString();
                Label4.Text = Request["floor"].ToString();
                Label6.Text = Request["bedno"].ToString();
                area.Text = Request["area"].ToString();
                time.Text = Request["time"].ToString();
                daytyp.Text = Request["daytyp"].ToString();
                page.Text = Request["page"].ToString();
                info_date1.Text = Request["date"].ToString();
                Show();
            }
        }

        protected void Show()
        {
            GetComboxData(); //取得各項COMBOBOX資料
            Label2.Text = patient_name.Text;
            DataTable dt = new DataTable();
            string sql;
            sql = "SELECT a.*, c.cln1_col11 AS Henan1, c.cln1_col12 AS Henan2, c.cln1_col21 AS Henan3, c.cln1_col22 AS Henan4, ";
            sql += "b.cln1_col11 as col11, b.cln1_col12 as col12, ";
            sql += "b.cln1_col21 as col21, b.cln1_col22 as col22, ";
            sql += "b.cln1_col31 as col31, b.cln1_col32 as col32, ";
            sql += "b.cln1_col41 as col41, b.cln1_col42 as col42, ";
            sql += "b.cln1_col50 as col50 FROM clinical1_nurse a ";
            sql += "LEFT JOIN clinical1_nurse_suzhou b ON a.cln1_patic=b.cln1_patic AND a.cln1_diadate=b.cln1_diadate ";
            sql += "LEFT JOIN clinical1_nurse_henan c ON a.cln1_patic=c.cln1_patic AND a.cln1_diadate=c.cln1_diadate ";
            sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate='" + info_date1.Text + "'";
            dt = db.Query(sql);
            DataRow row = dt.Rows[0];
            Common.SetComboBoxValue(cbo_diagnosis, row["cln1_col1"].ToString(), false);
            Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col34"].ToString(), false);//血管通路
            Common.SetComboBoxValue(cbo_mechine_model, row["cln1_col26"].ToString(), false);//透析器型号
            string tube_model = Common.get_tube_model(row, "cln1_col27");
            if (tube_model != null)
                Common.SetComboBoxValue(cbo_hpack3, tube_model, false);
            Common.SetComboBoxValue(cbo_dialysis_type, row["cln1_col3"].ToString(), false);//透析方式
            Common.SetComboBoxValue(SelectBox10, row["cln1_col15"].ToString(), false);//肝素
            bedno.Text = row["cln1_col2"].ToString();
            Label6.Text = row["cln1_col2"].ToString();

            txt_weight_after.Text = row["cln1_col8"].ToString(); //透析后体重
            txt_weight_after_expect.Text = row["cln1_col6"].ToString(); //干体重
            txt_weight_before.Text = dt.Rows[0]["cln1_col5"].ToString(); //透析前体重
            TextField3.Text = dt.Rows[0]["cln1_col7"].ToString();

            TextField5.Text = dt.Rows[0]["cln1_col10"].ToString(); //透析開始時間
            TextField6.Text = dt.Rows[0]["cln1_col11"].ToString(); //透析結束時間
            TextField7.Text = dt.Rows[0]["cln1_col12"].ToString(); //透析合計時間
            TextField8.Text = row["cln1_col13"].ToString();
            TextAdd.Text = row["cln1_col14"].ToString();
            TextAmount.Text = row["cln1_col17"].ToString();

            TextField2.Text = row["Henan1"].ToString();
            TextField4.Text = row["Henan2"].ToString();
            TextField9.Text = row["Henan3"].ToString();
            TextField10.Text = row["Henan4"].ToString();

            TextField12.Text = dt.Rows[0]["cln1_col20"].ToString();
            TextField13.Text = dt.Rows[0]["cln1_col21"].ToString();
            TextField14.Text = dt.Rows[0]["cln1_col22"].ToString();
            TextField131.Text = dt.Rows[0]["cln1_col25"].ToString();
            TextField24.Text = dt.Rows[0]["cln1_col35"].ToString();
            TextField25.Text = dt.Rows[0]["cln1_col36"].ToString();
            TextField23.Text = dt.Rows[0]["cln1_col33"].ToString();

            //不自動帶 BEG
            Col11 = dt.Rows[0]["col11"].ToString();
            coltext11 = dt.Rows[0]["col12"].ToString();
            Col21 = dt.Rows[0]["col21"].ToString();
            coltext21 = dt.Rows[0]["col22"].ToString();
            Col31 = dt.Rows[0]["col31"].ToString();
            coltext31 = dt.Rows[0]["col32"].ToString();
            Col41 = dt.Rows[0]["col41"].ToString();
            coltext41 = dt.Rows[0]["col42"].ToString();
            Col50 = dt.Rows[0]["col50"].ToString();

            TextArea1.Text = row["cln1_col23"].ToString();

            string[] check = row["cln1_col16"].ToString().Split(new Char[] { ',' });
            //for (int i = 0; i < check.Length; i++)
            //{
            //    if (check[i] == "EPO")
            //    {
            //        Checkbox1.Checked = true;
            //    }
            //    else if (check[i] == "左卡")
            //    {
            //        Checkbox2.Checked = true;
            //    }
            //    else if (check[i] == "铁剂")
            //    {
            //        Checkbox3.Checked = true;
            //    }
            //    else if (check[i] == "钙剂")
            //    {
            //        Checkbox4.Checked = true;
            //    }
            //    else if (check[i] == "抗菌素/其它")
            //    {
            //        Checkbox5.Checked = true;
            //    }
            //}
            init_PAGE2();
            
            dt.Dispose();
            CheckcboValue();
            db.myConnection.Close();
        }

        protected void init_PAGE2()
        {
            TextField12.RemoveCls("Text-blue");
            TextField12.AddCls("red");
            TextField13.RemoveCls("Text-blue");
            TextField13.AddCls("red");
            TextField14.RemoveCls("Text-blue");
            TextField14.AddCls("red");
            TextField23.RemoveCls("Text-blue");
            TextField23.AddCls("red");
            TextField24.RemoveCls("Text-blue");
            TextField24.AddCls("red");
            TextField25.RemoveCls("Text-blue");
            TextField25.AddCls("red");
            TextField131.RemoveCls("Text-blue");
            TextField131.AddCls("red");
        }

        protected void Btn_save_Click(object sender, DirectEventArgs e)
        {
            string sql;
            DataTable dt;
            if (cbo_mechine_model.Text.Trim() == "")
            {
                Common._ErrorMsgShow("请输入透析器型号!");
                return;
            }
            else if (cbo_h_type.Text.Trim() == "")
            {
                Common._ErrorMsgShow("请输入血管通路!");
                return;
            }
            else
            {
                try //深静脉置管
                {
                    //DBMysql db = new DBMysql();
                    sql = "SELECT cln1_diadate FROM clinical1_nurse_suzhou ";
                    sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate = '" + info_date1.Text + "' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "UPDATE clinical1_nurse_suzhou SET ";
                        sql += "cln1_col11 = '" + Col11 + "',";
                        sql += "cln1_col12 = '" + coltext11 + "',";
                        sql += "cln1_col21 = '" + Col21 + "',";
                        sql += "cln1_col22 = '" + coltext21 + "',";
                        sql += "cln1_col31 = '" + Col31 + "',";
                        sql += "cln1_col32 = '" + coltext31 + "',";
                        sql += "cln1_col41 = '" + Col41 + "',";
                        sql += "cln1_col42 = '" + coltext41 + "',";
                        sql += "cln1_col50 = '" + Col50 + "' ";
                        sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate = '" + info_date1.Text + "' ";
                    }
                    else
                    {
                        sql = "INSERT INTO clinical1_nurse_suzhou (cln1_patic, cln1_diadate,";
                        sql += "cln1_col11, cln1_col12, cln1_col21, cln1_col22, cln1_col31, cln1_col32, cln1_col41, cln1_col42, cln1_col50) ";
                        sql += "VALUES('" + patient_id.Text + "','" + info_date1.Text + "',";
                        sql += "'" + Col11 + "','" + coltext11 + "',";
                        sql += "'" + Col21 + "','" + coltext21 + "',";
                        sql += "'" + Col31 + "','" + coltext31 + "',";
                        sql += "'" + Col41 + "','" + coltext41 + "',";
                        sql += "'" + Col50 + "')";
                    }
                    db.Excute(sql); //深静脉置管
                }
                catch //深静脉置管，儲存失敗
                { 
                }

                string checktext = ",";
                //if (Checkbox1.Checked == true)
                //{
                //    checktext += "EPO,";
                //}
                //if (Checkbox2.Checked == true)
                //{
                //    checktext += "左卡,";
                //}
                //if (Checkbox3.Checked == true)
                //{
                //    checktext += "铁剂,";
                //}
                //if (Checkbox4.Checked == true)
                //{
                //    checktext += "钙剂,";
                //}
                //if (Checkbox5.Checked == true)
                //{
                //    checktext += "抗菌素/其它";
                //}
                sql = "UPDATE clinical1_nurse a ";
                sql += " SET a.cln1_col1 = '" + Common.GetComboBoxText(cbo_diagnosis) + "',";
                sql += "     a.cln1_col2 = '" + bedno.Text + "',";
                sql += "     a.cln1_col3 = '" + cbo_dialysis_type.Text + "',";
                sql += "     a.cln1_col4 = '" + hpack.Text + "',";
                sql += "     a.cln1_col5 = '" + txt_weight_before.Text + "',";
                sql += "     a.cln1_col6 = '" + txt_weight_after_expect.Text + "',";
                sql += "     a.cln1_col7 = '" + TextField3.Text + "',";

                if (txt_weight_after.Text != "")
                    sql += "     a.cln1_col8 = '" + txt_weight_after.Text + "',";
                    
                sql += "     a.cln1_col9 = '" + info_date1.Text + "',";
                sql += "     a.cln1_col10 = '" + TextField5.Text + "',";
                sql += "     a.cln1_col11 = '" + TextField6.Text + "',";
                sql += "     a.cln1_col12 = '" + TextField7.Text + "',";
                sql += "     a.cln1_col13 = '" + TextField8.Text + "',";
                sql += "     a.cln1_col14 = '" + TextAdd.Text + "',";
                sql += "     a.cln1_col15 = '" + Common.GetComboBoxText(SelectBox10) + "',";
                if (checktext.Length > 1)
                {
                    sql += " a.cln1_col16 = '" + checktext.Substring(1) + "',";
                }
                else
                {
                    sql += " a.cln1_col16 = '',";
                }
                sql += "     a.cln1_col17 = '" + TextAmount.Text + "',";
                sql += "     a.cln1_col20 = '" + TextField12.Text + "',";
                sql += "     a.cln1_col21 = '" + TextField13.Text + "',";
                sql += "     a.cln1_col22 = '" + TextField14.Text + "',";
				sql += "     a.cln1_col25 = '" + TextField131.Text + "',";
                sql += "     a.cln1_col23 = '" + TextArea1.Text + "',";
                sql += "     a.cln1_col26 = '" + Common.GetComboBoxText(cbo_mechine_model) + "', ";
                //sql += "     a.cln1_col28 = '" + Common.GetComboBoxText(SelectBoxEPO) + "', ";
                //sql += "     a.cln1_col29 = '" + Common.GetComboBoxText(SelectBoxLcard) + "', ";
                sql += "     a.cln1_col33 = '" + TextField23.Text + "',";
                if (cbo_hpack3.Text == "")
                    sql += "a.cln1_col27 = '血管路', ";
                else
                    sql += "a.cln1_col27 = '" + Common.GetComboBoxText(cbo_hpack3) + "', ";
                sql += "     a.cln1_col34 = '" + Common.GetComboBoxText(cbo_h_type) + "',";
                sql += "     a.cln1_col35 = '" + TextField24.Text + "',";
                sql += "     a.cln1_col36 = '" + TextField25.Text + "',";
                sql += "     a.cln1_user = '',";
                sql += "     a.cln1_dateadded = '' ";
                sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' ";
                sql += "  AND a.cln1_diadate = '" + info_date1.Text + "' ";
                db.Excute(sql);

                //更新clinical1_nurse_henan
                sql = "SELECT cln1_diadate FROM clinical1_nurse_henan ";
                sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate = '" + info_date1.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical1_nurse_henan ";
                    sql += " SET cln1_col11 = '" + TextField2.Text + "',";
                    sql += "     cln1_col12 = '" + TextField4.Text + "',";
                    sql += "     cln1_col21 = '" + TextField9.Text + "',";
                    sql += "     cln1_col22 = '" + TextField10.Text + "' ";
                    sql += "WHERE cln1_patic = '" + patient_id.Text + "' ";
                    sql += "  AND cln1_diadate = '" + info_date1.Text + "' ";
                }
                else
                {
                    sql = "INSERT into clinical1_nurse_henan(cln1_patic,cln1_diadate,";
                    sql += "cln1_col11,cln1_col12,cln1_col21,cln1_col22,cln1_col31,cln1_col32) ";
                    sql += "VALUES('" + patient_id.Text + "','" + info_date1.Text + "','";
                    sql += TextField2.Text + "','" + TextField4.Text + "','" + TextField9.Text + "','" + TextField10.Text + "','','')";
                }
                db.Excute(sql);

                //更新pat_info
                sql = "UPDATE pat_info " +
                         "SET pif_hpack='" + Common.GetComboBoxValue(cbo_h_type).Replace("'", "''") + "', " + //20160321 Alex
                             "pif_hpack2='" + Common.GetComboBoxValue(cbo_mechine_model).Replace("'", "''") + "', " +
                             "pif_hpack3='" + Common.GetComboBoxValue(cbo_hpack3).Replace("'", "''") + "' " + //20160321 Alex
                       "WHERE pif_ic='" + patient_id.Text + "' ";
                db.Excute(sql);

                //更新pat_visit
                sql = "UPDATE pat_visit " +
                         "SET pv_macno='" + Common.GetComboBoxValue(cbo_dialysis_type).Replace("'", "''") + "', " +
                             "pv_weight='" + txt_weight_before.Text + "', " +
                             "pv_hpack='" + Common.GetComboBoxValue(cbo_h_type).Replace("'", "''") + "', " + //20160321 Alex
                             "pv_hpack2='" + Common.GetComboBoxValue(cbo_mechine_model).Replace("'", "''") + "', " +
                             "pv_hpack3='" + Common.GetComboBoxValue(cbo_hpack3).Replace("'", "''") + "' " + //20160321 Alex
                       "WHERE pv_ic='" + patient_id.Text + "' " +
                         "AND pv_datevisit='" + info_date1.Text + "' ";
                db.Excute(sql);


                sql = "SELECT * FROM clinical3_nurse ";
                sql += " where cln3_patic = '" + patient_id.Text + "' and cln3_date = '" + info_date1.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical3_nurse ";
                    sql += "SET cln3_doc1 = '" + TextField23.Text + "', ";
                    sql += "    cln3_nur1 = '" + TextField25.Text + "' ";
                    sql += "WHERE cln3_patic = '" + patient_id.Text + "' ";
                    sql += "AND cln3_date = '" + info_date1.Text + "' ";
                }
                else
                {
                    sql = "INSERT INTO clinical3_nurse(cln3_patic,cln3_date,cln3_doc1,cln3_nur1) ";
                    sql += "VALUES('" + patient_id.Text + "','" + info_date1.Text + "','" + TextField23.Text + "','" + TextField25.Text + "')";
                }

                CheckcboValue();
                Common._NotificationShow("<font size=4>储存成功!</font>");
            }
        }

        protected void text_click(object sender, EventArgs e)
        {
            if (page.Text == "2")
            {
                TextField textID = (TextField)sender;
                a = textID.ID;
                Window1.Show();
                TextField_UserID.Focus(false, 100);
            }
        }

        protected void btnDecrypt_Click(object sender, DirectEventArgs e)
        {
            string wactive_flag = ""; 
            string wacciv_id = "";

            if (TextField_UserID.Text == "")
            {
                Common._NotificationShow("请输入正确工号!");
                return;
            }
            else
            {
                string sql = "SELECT a.acclv_fname,a.acclv_funm,a.acclv_id";
                sql += " FROM access_level a ";
                sql += "WHERE a.acclv_stfcode = '" + TextField_UserID.Text + "' ";

                DataTable dt = db.Query(sql);
                if (dt.Rows.Count == 1)
                {
                    wactive_flag = "1";
                    wacciv_id = dt.Rows[0]["acclv_id"].ToString();

                    if (a == "TextField12")
                    {
                        TextField12.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField13")
                    {
                        TextField13.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField14")
                    {
                        TextField14.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField24")
                    {
                        TextField24.Text = dt.Rows[0]["acclv_fname"].ToString(); 
                    }
                    else if (a == "TextField25")
                    {
                        TextField25.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField23")
                    {
                        TextField23.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField131")
                    {
                        TextField131.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                }
                else
                {
                    sql = "SELECT a.acclv_fname,a.acclv_funm ";
                    sql += " FROM access_level a ";
                    sql += "WHERE a.acclv_stfcode = '" + TextField_UserID.Text + "' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        Common._NotificationShow("工号有兩筆以上相同，请重新输入!");
                        TextField_UserID.Text = "";                       
                        Window1.Close();
                        return;
                    }
                    else
                    {
                        Common._NotificationShow("工号不存在，请重新输入!");
                        TextField_UserID.Text = "";
                        return;
                    }
                }

                if (wactive_flag == "1")
                {
                    sql = "SELECT a.associate_id,a.associate_active";
                    sql += " FROM associate_list a ";
                    sql += "WHERE a.associate_id     = '" + wacciv_id + "'";
                    sql += "AND   a.associate_active ='A" + "'";
                    DataTable dt1 = db.Query(sql);
                    if (dt1.Rows.Count == 0)
                    {
                        Common._NotificationShow("此工號已關閉，请重新输入!");
                        TextField_UserID.Text = "";
                        return;
                    }
                }
                TextField_UserID.Text = "";
                Window1.Close();
            }
        }

        protected void btnClose_Click(object sender, DirectEventArgs e)
        {
            TextField_UserID.Text = "";
            Window1.Close();
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

        /// <summary>
        /// Work for txt_weight_after_expect
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void text_deactivate(object sender, EventArgs e)
        {
            try
            {
                if (txt_weight_after_expect.Text != "")
                {
                    TextField3.Text = (Convert.ToDecimal(txt_weight_before.Text) - Convert.ToDecimal(txt_weight_after_expect.Text)).ToString();
                    TextField3.RemoveCls("Text-black");
                    TextField3.AddCls("Text-red");
                }

            }
            catch
            { 
            }
        }

        /// <summary>
        /// Work for 目标定容量
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void TextField3_change(object sender, EventArgs e)
        {
            try
            {
                if (TextField3.Text != (Convert.ToDecimal(txt_weight_before.Text) - Convert.ToDecimal(txt_weight_after_expect.Text)).ToString())
                {
                    TextField3.RemoveCls("Text-red");
                    TextField3.AddCls("Text-black");
                }
            }
            catch
            { }
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

        protected void CheckcboValue()
        {
            //顯示顏色提醒
            if (cbo_h_type.Text.Trim() == "")
            {
                cbo_h_type.LabelCls = "blink";
            }
            else
            {
                cbo_h_type.LabelCls = "my-Field";
            } 
            
            if (cbo_hpack3.Text.Trim() == "")
            {
                cbo_hpack3.LabelCls = "blink";
            }
            else
            {
                cbo_hpack3.LabelCls = "my-Field";
            }

            if (cbo_mechine_model.Text.Trim() == "")
            {
                cbo_mechine_model.LabelCls = "blink";
            }
            else
            {
                cbo_mechine_model.LabelCls = "my-Field";
            }
        }


        protected void GetComboxData()
        {
            string sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='diagnosis' ";
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_diagnosis, dt1, true, "NAME", "CODE");
            //cbo_diagnosis.Select(0);

            sSQL = "SELECT hp2_code AS CODE, hp2_name AS NAME FROM hpack2_setup"; //透析器型號
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_mechine_model, dt1, true, "NAME", "CODE");
            //cbo_mechine_model.Select(0);

            sSQL = "SELECT hp3_code AS CODE, hp3_name AS NAME FROM hpack3_setup"; //血管通路，目前沒用到
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_hpack3, dt1, true, "NAME", "CODE");
            //cbo_hpack3.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='dialysis_type' ";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_dialysis_type, dt1, true, "NAME", "CODE");
            //cbo_dialysis_type.Select(0);

            //sSQL = "SELECT hp_code AS CODE, hp_subgrp AS NAME FROM h_type"; //舊程式使用h_type與FLEX使用不同資料表
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt1, true, "NAME", "CODE");
            //cbo_h_type.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='heparin' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBox10, dt1, true, "NAME", "CODE");
            //SelectBox10.Select(0);

            //Common.SetComboBoxValue(cbo_h_type, Request["hpack"].ToString(), true); //血管通路類型
            //Common.SetComboBoxValue(cbo_hpack3, Request["hpack3"].ToString(), true); //血管通路
            //Common.SetComboBoxValue(cbo_mechine_model, Request["mechine_model"].ToString(), true); //透析器型號

            //txt_weight_before.Text = Request["patient_weight"].ToString(); //體重  added by jeffrey at 2015/11/19
            //patient_weight.Text = Request["patient_weight"].ToString(); //體重
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
        
        #region 計算透析時間
        protected void text_CalTime(object sender, EventArgs e)
        {
            try
            {
                DateTime tmin = Convert.ToDateTime(TextField5.Text);
                DateTime tmax = Convert.ToDateTime(TextField6.Text);
                var timeDiff = new TimeSpan(tmax.Ticks - tmin.Ticks);
                TextField7.Text = timeDiff.ToString();
            }
            catch
            { }
        }
        #endregion

        #region 上一頁
        protected void Btn_back_Click(object sender, DirectEventArgs e)
        {
            string sURL = "Dialysis_09_01_All.aspx";
            X.Redirect(sURL);
        }
        #endregion
    }
}