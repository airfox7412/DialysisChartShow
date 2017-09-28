using System;
using System.Collections.Generic;
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
using System.Net;

namespace Dialysis_Chart_Show
{
    public partial class Dialysis_09_01_117 : System.Web.UI.Page
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

                txt_weight_before.Text = Request["patient_weight"].ToString(); //體重  added by jeffrey at 2015/11/19
                patient_weight.Text = Request["patient_weight"].ToString(); //體重

                bedno.Text = Request["bedno"].ToString();
                floor.Text = Request["floor"].ToString();
                area.Text = Request["area"].ToString();
                time.Text = Request["time"].ToString();
                daytyp.Text = Request["daytyp"].ToString();
                page.Text = Request["page"].ToString();
                info_date1.Text = Request["date"].ToString();

                show();
                db.myConnection.Close();
            }
        }

        protected void show()
        {
            GetComboxData(); //取得各項COMBOBOX資料
            Label2.Text = patient_name.Text;
            Label4.Text = floor.Text;
            Label6.Text = bedno.Text;
            DataTable dt = new DataTable();

            string sql = "SELECT a.*, ";
            sql += "b.cln1_col11 as col11, b.cln1_col12 as col12, ";
            sql += "b.cln1_col21 as col21, b.cln1_col22 as col22, ";
            sql += "b.cln1_col31 as col31, b.cln1_col32 as col32, ";
            sql += "b.cln1_col41 as col41, b.cln1_col42 as col42, ";
            sql += "b.cln1_col50 as col50, ";
            sql += "c.cln3_DialysisMachine, c.cln3_HeparinPumpArtery, c.cln3_HeparinPumpVein, c.cln3_CatheterAccess, c.cln3_MuscleAtrophy ";
            sql += "FROM clinical1_nurse a ";
            sql += "LEFT JOIN clinical1_nurse_suzhou b ON a.cln1_patic=b.cln1_patic AND a.cln1_diadate=b.cln1_diadate ";
            sql += "LEFT JOIN clinical3_nurse c ON a.cln1_patic=c.cln3_patic AND a.cln1_diadate=c.cln3_date ";
            sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate='" + info_date1.Text + "'";
            dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];
                Common.SetComboBoxValue(cbo_diagnosis, row["cln1_col1"].ToString(), false);
                Common.SetComboBoxValue(cbo_mechine_model, row["cln1_col26"].ToString(), false);//透析器型号
                string tube_model = Common.get_tube_model(row, "cln1_col27");
                if (tube_model != null)
                    Common.SetComboBoxValue(cbo_hpack3, tube_model, false);

                Common.SetComboBoxValue(cbo_dialysis_type, row["cln1_col3"].ToString(), false);//透析方式
                if (cbo_dialysis_type.Text == "HDF" || cbo_dialysis_type.Text == "HF")
                {
                    TextField11.Text = "25";
                }
                txt_weight_after.Text = row["cln1_col8"].ToString(); //透析后体重
                txt_weight_after_expect.Text = row["cln1_col6"].ToString(); //干体重
                try
                {
                    decimal try_parse = Convert.ToDecimal(dt.Rows[0]["cln1_col5"].ToString());
                    txt_weight_before.Text = dt.Rows[0]["cln1_col5"].ToString(); //透析前体重
                }
                catch { }

                TextTotalCap.Text = dt.Rows[0]["cln1_dateadded"].ToString(); //总定容量
                TextField5.Text = dt.Rows[0]["cln1_col10"].ToString(); //透析開始時間
                TextField6.Text = dt.Rows[0]["cln1_col11"].ToString(); //透析結束時間
                TextField7.Text = dt.Rows[0]["cln1_col12"].ToString(); //透析合計時間

                TextField3.Text = dt.Rows[0]["cln1_col7"].ToString();
                TextField8.Text = row["cln1_col13"].ToString();
                TextField9.Text = row["cln1_col14"].ToString();
                TextField10.Text = row["cln1_col15"].ToString();

                TextField11.Text = dt.Rows[0]["cln1_col19"].ToString();
                TextField13.Text = dt.Rows[0]["cln1_col21"].ToString();
                TextField131.Text = dt.Rows[0]["cln1_col25"].ToString();
                TextField24.Text = dt.Rows[0]["cln1_col35"].ToString();
                TextField23.Text = dt.Rows[0]["cln1_col33"].ToString();
                Common.SetComboBoxValue(cbo_SelDialysisNa, dt.Rows[0]["cln1_col17"].ToString(), false);//透析液钙
                Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col34"].ToString(), false);//血管通路
                //Common.SetComboBoxValue(cbo_change_type, dt.Rows[0]["cln1_col18"].ToString(), false);//置换方式(沒用到)

                //淨化小節移過來的
                Common.SetComboBoxValue(cbo_DialysisMachine, dt.Rows[0]["cln3_DialysisMachine"].ToString(), false);//透析器凝血
                Common.SetComboBoxValue(cbo_HeparinPump_a, dt.Rows[0]["cln3_HeparinPumpArtery"].ToString(), false);//动脉壶凝血
                Common.SetComboBoxValue(cbo_HeparinPump_v, dt.Rows[0]["cln3_HeparinPumpVein"].ToString(), false);//静脉壶凝血
                TextFieldCatheterAccess.Text = dt.Rows[0]["cln3_CatheterAccess"].ToString();
                TextFieldMuscleAtrophy.Text = dt.Rows[0]["cln3_MuscleAtrophy"].ToString();
                //淨化小節移過來的

                //不自動帶 BEG
                if (row["cln1_diadate"].ToString() == info_date1.Text)
                {
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

                    for (int i = 0; i < check.Length; i++)
                    {
                        if (check[i] == "EPO")
                        {
                            Checkbox1.Checked = true;
                            Common.SetComboBoxValue(SelectBoxEPO, dt.Rows[0]["cln1_col28"].ToString(), true);
                        }
                        else if (check[i] == "左卡")
                        {
                            Checkbox2.Checked = true;
                            Common.SetComboBoxValue(SelectBoxLcard, dt.Rows[0]["cln1_col29"].ToString(), true);
                        }
                        else if (check[i] == "铁剂")
                        {
                            Checkbox3.Checked = true;
                            Common.SetComboBoxValue(SelectBoxFe, dt.Rows[0]["cln1_col30"].ToString(), true);
                        }
                        else if (check[i] == "骨化三醇")
                        {
                            Checkbox4.Checked = true;
                            Common.SetComboBoxValue(SelectBoxCalcitriol, dt.Rows[0]["cln1_col31"].ToString(), true);
                        }
                        else if (check[i] == "弥可保")
                        {
                            Checkbox5.Checked = true;
                            Common.SetComboBoxValue(SelectBoxMethycobal, dt.Rows[0]["cln1_col32"].ToString(), true);
                        }
                        else if (check[i] == "维生素B12")
                        {
                            Checkbox6.Checked = true;
                            Common.SetComboBoxValue(SelectBoxB12, dt.Rows[0]["cln1_col37"].ToString(), true);
                        }
                    }
                }
            }
            
            init_PAGE2();

            dt.Dispose();
            CheckcboValue();
            db.myConnection.Close();
        }

        protected void init_PAGE2()
        {
            TextField13.RemoveCls("Text-blue");
            TextField13.AddCls("red");
            TextField23.RemoveCls("Text-blue");
            TextField23.AddCls("red");
            TextField24.RemoveCls("Text-blue");
            TextField24.AddCls("red");
            TextField131.RemoveCls("Text-blue");
            TextField131.AddCls("red");
        }

        protected void Btn_save_Click(object sender, DirectEventArgs e)
        {
            string sql;
            DataTable dt;
            if (cbo_mechine_model.Text.Trim() == "")
            {
                ErrorMsgShow("请输入透析器型号!");
                return;
            }
            else if (cbo_h_type.Text.Trim() == "")
            {
                Common._ErrorMsgShow("请输入血管通路!");
                return;
            }
            else
            {
                string checktext = ",";
                if (SelectBoxEPO.Text.Trim() != "")
                {
                    checktext += "EPO,";
                }
                if (SelectBoxLcard.Text.Trim() != "")
                {
                    checktext += "左卡,";
                }
                if (SelectBoxFe.Text.Trim() != "")
                {
                    checktext += "铁剂,";
                }
                if (SelectBoxCalcitriol.Text.Trim() != "")
                {
                    checktext += "骨化三醇,";
                }
                if (SelectBoxMethycobal.Text.Trim() != "")
                {
                    checktext += "弥可保,";
                }
                if (SelectBoxB12.Text.Trim() != "")
                {
                    checktext += "维生素B12";
                }

            sql = "UPDATE clinical1_nurse a ";
                sql += " SET a.cln1_col1 = '" + Common.GetComboBoxText(cbo_diagnosis) + "',";
                sql += "     a.cln1_col2 = '" + bedno.Text + "',";
                sql += "     a.cln1_col3 = '" + cbo_dialysis_type.Text + "',";
                sql += "     a.cln1_col4 = '" + hpack.Text + "',";
                sql += "     a.cln1_col5 = '" + txt_weight_before.Text + "',";
                sql += "     a.cln1_col6 = '" + txt_weight_after_expect.Text + "',";
                sql += "     a.cln1_col7 = '" + TextField3.Text + "',";

                if (txt_weight_after.Text != "")
                    sql += " a.cln1_col8 = '" + txt_weight_after.Text + "',";
                    
                sql += "     a.cln1_col9 = '" + info_date1.Text + "',";
                sql += "     a.cln1_col10 = '" + TextField5.Text + "',";
                sql += "     a.cln1_col11 = '" + TextField6.Text + "',";
                sql += "     a.cln1_col12 = '" + TextField7.Text + "',";
                sql += "     a.cln1_col13 = '" + TextField8.Text + "',";
                sql += "     a.cln1_col14 = '" + TextField9.Text + "',";
                sql += "     a.cln1_col15 = '" + TextField10.Text + "',";
                if (checktext.Length > 1)
                {
                    sql += " a.cln1_col16 = '" + checktext.Substring(1) + "',";
                }
                else
                {
                    sql += " a.cln1_col16 = '',";
                }
                sql += "     a.cln1_col28 = '" + SelectBoxEPO.Text + "',";
                sql += "     a.cln1_col29 = '" + SelectBoxLcard.Text + "',";
                sql += "     a.cln1_col30 = '" + SelectBoxFe.Text + "',";
                sql += "     a.cln1_col31 = '" + SelectBoxCalcitriol.Text + "',";
                sql += "     a.cln1_col32 = '" + SelectBoxMethycobal.Text + "',";
                sql += "     a.cln1_col37 = '" + SelectBoxB12.Text + "',";
                sql += "     a.cln1_col30q = 0,";
                sql += "     a.cln1_col31q = 0,";
                sql += "     a.cln1_col32q = 0,";

                sql += "     a.cln1_col17 = '" + cbo_SelDialysisNa.Text + "',";
                sql += "     a.cln1_col18 = '',";
                sql += "     a.cln1_col19 = '" + TextField11.Text + "',";
                sql += "     a.cln1_col20 = '',";
                sql += "     a.cln1_col21 = '" + TextField13.Text + "',";
                sql += "     a.cln1_col22 = '',";
				sql += "     a.cln1_col25 = '" + TextField131.Text + "',";
                sql += "     a.cln1_col23 = '" + TextArea1.Text + "',";
                sql += "     a.cln1_col26 = '" + Common.GetComboBoxText(cbo_mechine_model) + "', ";
                sql += "     a.cln1_col33 = '" + TextField23.Text + "',";
                if (cbo_hpack3.Text == "")
                    sql += "a.cln1_col27 = '血管路', ";
                else
                    sql += "a.cln1_col27 = '" + Common.GetComboBoxText(cbo_hpack3) + "', ";
                sql += "     a.cln1_col34 = '" + Common.GetComboBoxText(cbo_h_type) + "',";
                sql += "     a.cln1_col35 = '" + TextField24.Text + "',";
                sql += "     a.cln1_col36 = '',";
                sql += "     a.cln1_user = '',";
                sql += "     a.cln1_dateadded = '" + TextTotalCap.Text+ "' ";
                sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' ";
                sql += "  AND a.cln1_diadate = '" + info_date1.Text + "' ";
                db.Excute(sql);

                //更新pat_info
                sql = "UPDATE pat_info " +
                         "SET pif_hpack='" + Common.GetComboBoxValue(cbo_h_type) + "', " + //20160321 Alex
                             "pif_hpack2='" + Common.GetComboBoxValue(cbo_mechine_model) + "', " +
                             "pif_hpack3='" + Common.GetComboBoxValue(cbo_hpack3) + "' " + //20160321 Alex
                       "WHERE pif_ic='" + patient_id.Text + "' ";
                db.Excute(sql);

                //更新pat_visit
                sql = "UPDATE pat_visit " +
                         "SET pv_macno='" + Common.GetComboBoxValue(cbo_dialysis_type) + "', " + //透析方式
                             "pv_weight='" + txt_weight_before.Text + "', " +
                             "pv_hpack='" + Common.GetComboBoxValue(cbo_h_type) + "', " + //20160321 Alex
                             "pv_hpack2='" + Common.GetComboBoxValue(cbo_mechine_model) + "', " +
                             "pv_hpack3='" + Common.GetComboBoxValue(cbo_hpack3) + "' " + //20160321 Alex
                       "WHERE pv_ic='" + patient_id.Text + "' " +
                         "AND pv_datevisit='" + info_date1.Text + "' ";
                db.Excute(sql);


                sql = "SELECT * FROM clinical3_nurse ";
                sql += " where cln3_patic = '" + patient_id.Text + "' ";
                sql += "  and cln3_date = '" + info_date1.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical3_nurse SET ";
                    sql += "cln3_CatheterAccess = '" + TextFieldCatheterAccess.Text + "',";
                    sql += "cln3_MuscleAtrophy = '" + TextFieldMuscleAtrophy.Text + "',";
                    sql += "cln3_DialysisMachine = '" + Common.GetComboBoxText(cbo_DialysisMachine) + "',";
                    sql += "cln3_HeparinPumpArtery = '" + Common.GetComboBoxText(cbo_HeparinPump_a) + "',";
                    sql += "cln3_HeparinPumpVein = '" + Common.GetComboBoxText(cbo_HeparinPump_v) + "' ";
                    sql += "WHERE cln3_patic = '" + patient_id.Text + "' AND cln3_date='" + info_date1.Text + "' ";
                }
                else
                {
                    sql = "INSERT INTO clinical3_nurse(cln3_patic, cln3_date, cln3_CatheterAccess, cln3_MuscleAtrophy, cln3_DialysisMachine, cln3_HeparinPumpArtery, cln3_HeparinPumpVein) ";
                    sql += "VALUES('" + patient_id.Text + "','" + info_date1.Text + "',";
                    sql += "'" + TextFieldCatheterAccess.Text + "',";
                    sql += "'" + TextFieldMuscleAtrophy.Text + "',";
                    sql += "'" + Common.GetComboBoxText(cbo_DialysisMachine) + "',";
                    sql += "'" + Common.GetComboBoxText(cbo_HeparinPump_a) + "',";
                    sql += "'" + Common.GetComboBoxText(cbo_HeparinPump_v) + "')";
                }
                db.Excute(sql);

                CheckcboValue();
                Common._NotificationShow("<font size=4>储存成功!</font>");
                db.myConnection.Close();
            }
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

                    if (a == "TextField13")
                    {
                        TextField13.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField24")
                    {
                        TextField24.Text = dt.Rows[0]["acclv_fname"].ToString();
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

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelDialysisNa' ";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_SelDialysisNa, dt1, true, "NAME", "CODE");
            //cbo_SelDialysisNa.Select(0);

            //sSQL = "SELECT hp_code AS CODE, hp_subgrp AS NAME FROM h_type"; //舊程式使用h_type與FLEX使用不同資料表
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt1, true, "NAME", "CODE");
            //cbo_h_type.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectEPO' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxEPO, dt1, true, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectLc' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxLcard, dt1, true, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectFe' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxFe, dt1, true, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectCalcitriol' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxCalcitriol, dt1, true, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectMethycobal' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxMethycobal, dt1, true, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectB12' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxB12, dt1, true, "NAME", "CODE");

            //從小結移過來
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='dialysis_machine' ";
            DataTable dt_DialysisMachine = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_DialysisMachine, dt_DialysisMachine, false, "NAME", "CODE");
            //cbo_DialysisMachine.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='heparin_pump' ";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_HeparinPump_a, dt1, false, "NAME", "CODE");
            //cbo_HeparinPump_a.Select(0);
            Common.SetComboBoxItem(cbo_HeparinPump_v, dt1, false, "NAME", "CODE");
            //cbo_HeparinPump_v.Select(0);
            dt1.Dispose();
            //從小結移過來
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

        #region 設定透析方式HDF/HD，置换量為25L
        protected void text_dtype(object sender, EventArgs e)
        {
            if (cbo_dialysis_type.Text == "HDF" || cbo_dialysis_type.Text == "HF")
            {
                TextField11.Text = "25";
            }
        }
        #endregion
    }
}