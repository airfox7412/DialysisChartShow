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
    public partial class ipad_detaillist_Suzhou : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

        string cln1_col1, cln1_col6, cln1_col10, cln1_col13, cln1_col14, cln1_col15, cln1_col17, cln1_col18, cln1_col19;
        string cln1_col26, cln1_col27, cln1_col34;
        string Col11, coltext11;
        string Col21, coltext21;
        string Col31, coltext31;
        string Col41, coltext41;
        string Col50;

        protected void Page_Load(object sender, EventArgs e)
        {
            TextField1.Hidden = true;            
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request.QueryString["patient_id"];
                patient_name.Text = Request.QueryString["patient_name"];
                machine_type.Text = Request.QueryString["machine_type"];
                mechine_model.Text = Request.QueryString["mechine_model"];
                bedno.Text = Request.QueryString["bedno"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                daytyp.Text = Request.QueryString["daytyp"];
                hpack.Text = Request.QueryString["hpack"];
                hpack3.Text = Request.QueryString["hpack3"];
                txt_weight_before.Text = Request.QueryString["patient_weight"]; 
                page.Text = Request.QueryString["page"];
                if (patient_name.Text == null)
                {
                    patient_name.Text = Request.QueryString["amp;patient_name"];
                    machine_type.Text = Request.QueryString["amp;machine_type"];
                    mechine_model.Text = Request.QueryString["amp;mechine_model"];
                    bedno.Text = Request.QueryString["amp;bedno"];
                    floor.Text = Request.QueryString["amp;floor"];
                    area.Text = Request.QueryString["amp;area"];
                    time.Text = Request.QueryString["amp;time"];
                    daytyp.Text = Request.QueryString["amp;daytyp"];
                    hpack.Text = Request.QueryString["amp;hpack"];
                    hpack3.Text = Request.QueryString["amp;hpack3"];
                    txt_weight_before.Text = Request.QueryString["amp;patient_weight"]; 
                    page.Text = Request.QueryString["amp;page"];
                }
                patient_weight.Text = txt_weight_before.Text; //體重
                Show();
            }
        }

        protected void GetComboxData()
        {
            string sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='diagnosis' ";
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_diagnosis, dt1, true, "NAME", "CODE");
            cbo_diagnosis.Select(0);

            sSQL = "SELECT hp2_code AS CODE, hp2_name AS NAME FROM hpack2_setup"; //透析器型號
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_mechine_model, dt1, true, "NAME", "CODE");
            cbo_mechine_model.Select(0);

            sSQL = "SELECT hp3_code AS CODE, hp3_name AS NAME FROM hpack3_setup"; //血管通路，目前沒用到
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_hpack3, dt1, true, "NAME", "CODE");
            cbo_hpack3.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='dialysis_type' ";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_dialysis_type, dt1, true, "NAME", "CODE");
            cbo_dialysis_type.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='change_type' ";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_change_type, dt1, true, "NAME", "CODE");
            cbo_change_type.Select(0);

            //sSQL = "SELECT hp_code AS CODE, hp_subgrp AS NAME FROM h_type"; //舊程式使用h_type與FLEX使用不同資料表
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt1, true, "NAME", "CODE");
            cbo_h_type.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectEPO' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxEPO, dt1, true, "NAME", "CODE");
            SelectBoxEPO.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectCa' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectCheckbox3, dt1, true, "NAME", "CODE");
            SelectCheckbox3.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectThinner' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectCheckbox4, dt1, true, "NAME", "CODE");
            SelectCheckbox4.Select(0);

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectOther' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectCheckbox5, dt1, true, "NAME", "CODE");
            SelectCheckbox5.Select(0);
            dt1.Dispose();

            Common.SetComboBoxValue(cbo_h_type, hpack.Text, true); //血管通路類型
            Common.SetComboBoxValue(cbo_hpack3, hpack3.Text, true); //血管通路
            Common.SetComboBoxValue(cbo_mechine_model, mechine_model.Text, true); //透析器型號
        }

        protected void Show()
        {
            GetComboxData(); //取得各項COMBOBOX資料
            Label2.Text = patient_name.Text;
            Label4.Text = floor.Text;
            Label6.Text = bedno.Text;
            string sql;
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate <> '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "ORDER BY cln1_diadate DESC LIMIT 1";
            DataTable dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                info_date1.Text = dt0.Rows[0]["cln1_diadate"].ToString(); //上次日期
                cln1_col1 = dt0.Rows[0]["cln1_col1"].ToString();
                cln1_col6 = dt0.Rows[0]["cln1_col6"].ToString(); //干体重
                cln1_col13 = dt0.Rows[0]["cln1_col13"].ToString();
                cln1_col14 = dt0.Rows[0]["cln1_col14"].ToString();
                cln1_col15 = dt0.Rows[0]["cln1_col15"].ToString();
                cln1_col26 = dt0.Rows[0]["cln1_col26"].ToString();
                cln1_col27 = dt0.Rows[0]["cln1_col27"].ToString();
                cln1_col17 = dt0.Rows[0]["cln1_col17"].ToString(); //透析液钙
                cln1_col18 = dt0.Rows[0]["cln1_col18"].ToString(); //置换方式
                cln1_col19 = dt0.Rows[0]["cln1_col19"].ToString(); //置换方式
                cln1_col34 = dt0.Rows[0]["cln1_col34"].ToString(); //血管通路
            };
            dt0.Dispose();

            // page1: 治療參數, page2: 血液淨化 

            if (page.Text == "2") 
            {
                info_date1.Text = DateTime.Now.ToString("yyyy-MM-dd"); //今天日期
            }

            Common.SetComboBoxValue(cbo_dialysis_type, machine_type.Text, false);//透析方式

            DataTable dt = new DataTable();
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate='" + info_date1.Text + "' ";
            if (info_date1.Text == DateTime.Now.ToString("yyyy-MM-dd"))
                sql += "AND (NOT cln1_col5 IS NULL)";
            dt = db.Query(sql);
            if (dt.Rows.Count == 0)
            {
                sql = "SELECT * FROM clinical1_nurse ";
                sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate='" + info_date1.Text + "'";
                DataTable dt1 = db.Query(sql);
                if (dt1.Rows.Count > 0)
                    cln1_col10 = dt1.Rows[0]["cln1_col10"].ToString();
                dt1.Dispose();
                Common.SetComboBoxValue(cbo_diagnosis, cln1_col1, false);//诊断
                Common.SetComboBoxValue(cbo_mechine_model, cln1_col26, false);//透析器型号
                Common.SetComboBoxValue(SelectBox3, cln1_col17, false);//透析液钙
                Common.SetComboBoxValue(cbo_change_type, cln1_col18, false);//置换方式
                Common.SetComboBoxValue(cbo_h_type, cln1_col34, false);//血管通路
                txt_weight_after_expect.Text = cln1_col6; //干體重
                TextField5.Text = cln1_col10;
                TextField8.Text = cln1_col13;
                TextField9.Text = cln1_col14;
                TextField10.Text = cln1_col15;
                TextField11.Text = cln1_col19;
            }
            else
            {
                sql = "SELECT a.*, ";
                sql += "b.cln1_col11 as col11, b.cln1_col12 as col12, ";
                sql += "b.cln1_col21 as col21, b.cln1_col22 as col22, ";
                sql += "b.cln1_col31 as col31, b.cln1_col32 as col32, ";
                sql += "b.cln1_col41 as col41, b.cln1_col42 as col42, ";
                sql += "b.cln1_col50 as col50 FROM clinical1_nurse a ";
                sql += "LEFT JOIN clinical1_nurse_suzhou b ON a.cln1_patic=b.cln1_patic AND a.cln1_diadate=b.cln1_diadate ";
                sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate='" + info_date1.Text + "'";
                dt = db.Query(sql);
                DataRow row = dt.Rows[0];
                if (row["cln1_col1"].ToString()=="")
                    Common.SetComboBoxValue(cbo_diagnosis, cln1_col1, false);//诊断
                else
                    Common.SetComboBoxValue(cbo_diagnosis, row["cln1_col1"].ToString(), false);

                Common.SetComboBoxValue(cbo_mechine_model, row["cln1_col26"].ToString(), false);//透析器型号
                string tube_model = Common.get_tube_model(row, "cln1_col27");
                if (tube_model != null)
                    Common.SetComboBoxValue(cbo_hpack3, tube_model, false);

                txt_weight_after.Text = row["cln1_col8"].ToString(); //透析后体重
                if (row["cln1_col6"].ToString() == "")
                    txt_weight_after_expect.Text = cln1_col6; //干體重
                else
                    txt_weight_after_expect.Text = row["cln1_col6"].ToString(); //干体重
                try
                {
                    decimal try_parse = Convert.ToDecimal(dt.Rows[0]["cln1_col5"].ToString());
                    txt_weight_before.Text = dt.Rows[0]["cln1_col5"].ToString(); //透析前体重
                }
                catch { }

                TextField5.Text = dt.Rows[0]["cln1_col10"].ToString(); //透析開始時間
                TextField6.Text = dt.Rows[0]["cln1_col11"].ToString(); //透析結束時間
                TextField7.Text = dt.Rows[0]["cln1_col12"].ToString(); //透析合計時間
                TextField3.Text = dt.Rows[0]["cln1_col7"].ToString();

                if (row["cln1_col13"].ToString() == "") //肝素首量
                    TextField8.Text = cln1_col13;
                else
                    TextField8.Text = row["cln1_col13"].ToString();

                if (row["cln1_col14"].ToString() == "") //追加量
                    TextField9.Text = cln1_col14;
                else
                    TextField9.Text = row["cln1_col14"].ToString();

                if (row["cln1_col15"].ToString() == "") //低分子肝素
                    TextField10.Text = cln1_col15;
                else
                    TextField10.Text = row["cln1_col15"].ToString();

                if (row["cln1_col19"].ToString() == "") //置换量
                    TextField11.Text = cln1_col19;
                else
                    TextField11.Text = dt.Rows[0]["cln1_col19"].ToString();

                TextField12.Text = dt.Rows[0]["cln1_col20"].ToString();
                TextField13.Text = dt.Rows[0]["cln1_col21"].ToString();
                TextField14.Text = dt.Rows[0]["cln1_col22"].ToString();
                TextField131.Text = dt.Rows[0]["cln1_col25"].ToString();
                TextField24.Text = dt.Rows[0]["cln1_col35"].ToString();
                TextField25.Text = dt.Rows[0]["cln1_col36"].ToString();
                TextField23.Text = dt.Rows[0]["cln1_col33"].ToString();
                if (dt.Rows[0]["cln1_col17"].ToString() == "")
                    Common.SetComboBoxValue(SelectBox3, cln1_col17, false);//透析液钙
                else
                    Common.SetComboBoxValue(SelectBox3, dt.Rows[0]["cln1_col17"].ToString(), false);//透析液钙

                Common.SetComboBoxValue(cbo_change_type, dt.Rows[0]["cln1_col18"].ToString(), false);//置换方式
                Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col34"].ToString(), false);//血管通路

                //不自動帶 BEG
                if ((page.Text == "1") || (row["cln1_diadate"].ToString() == info_date1.Text))
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
                    RField1.Text = coltext11;
                    RField2.Text = coltext21;
                    RField3.Text = coltext31;
                    RField4.Text = coltext41;

                    if (Radio11.InputValue == Col11)
                    {
                        Radio11.Checked = true;
                        Radio12.Checked = false;
                        Radio13.Checked = false;
                    }
                    else if (Radio12.InputValue == Col11)
                    {
                        Radio12.Checked = true;
                        Radio11.Checked = false;
                        Radio13.Checked = false;
                    }
                    else if (Radio13.InputValue == Col11)
                    {
                        Radio13.Checked = true;
                        Radio11.Checked = false;
                        Radio12.Checked = false;
                    }

                    if (Radio21.InputValue == Col21)
                    {
                        Radio21.Checked = true;
                        Radio22.Checked = false;
                    }
                    else if (Radio22.InputValue == Col21)
                    {
                        Radio22.Checked = true;
                        Radio21.Checked = false;
                    }

                    if (Radio31.InputValue == Col31)
                    {
                        Radio31.Checked = true;
                        Radio32.Checked = false;
                    }
                    else if (Radio32.InputValue == Col31)
                    {
                        Radio32.Checked = true;
                        Radio31.Checked = false;
                    }

                    if (Radio41.InputValue == Col41)
                    {
                        Radio41.Checked = true;
                        Radio42.Checked = false;
                    }
                    else if (Radio42.InputValue == Col41)
                    {
                        Radio42.Checked = true;
                        Radio41.Checked = false;
                    }

                    if (Radio51.InputValue == Col50)
                    {
                        Radio51.Checked = true;
                        Radio52.Checked = false;
                    }
                    else if (Radio52.InputValue == Col50)
                    {
                        Radio52.Checked = true;
                        Radio51.Checked = false;
                    }

                    TextArea1.Text = row["cln1_col23"].ToString();

                    string[] check = row["cln1_col16"].ToString().Split(new Char[] { ',' });
                    TextCheckbox2.Text = "0";
                    TextCheckbox3q.Text = "0";
                    TextCheckbox4q.Text = "0";
                    TextCheckbox5q.Text = "0";
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
                            TextCheckbox2.Text = dt.Rows[0]["cln1_col29"].ToString();
                        }
                        else if (check[i] == "钙剂")
                        {
                            Checkbox3.Checked = true;
                            Common.SetComboBoxValue(SelectCheckbox3, dt.Rows[0]["cln1_col30"].ToString(), true);
                            TextCheckbox3q.Text = dt.Rows[0]["cln1_col30q"].ToString();
                        }
                        else if (check[i] == "稀释液")
                        {
                            Checkbox4.Checked = true;
                            Common.SetComboBoxValue(SelectCheckbox4, dt.Rows[0]["cln1_col31"].ToString(), true);
                            TextCheckbox4q.Text = dt.Rows[0]["cln1_col31q"].ToString();
                        }
                        else if (check[i] == "其它")
                        {
                            Checkbox5.Checked = true;
                            Common.SetComboBoxValue(SelectCheckbox5, dt.Rows[0]["cln1_col32"].ToString(), true);
                            TextCheckbox5q.Text = dt.Rows[0]["cln1_col32q"].ToString();
                        }
                    }
                }
            }

            if (page.Text == "1") //非今日之最後一筆資料
            {
                if (dt.Rows.Count > 0)
                {
                    decimal before_weight = Convert.ToDecimal(txt_weight_before.Text);
                    if (txt_weight_before.Text != patient_weight.Text)
                    {
                        txt_weight_before.RemoveCls("Text-red");
                        txt_weight_before.AddCls("Text-black");
                    }
                    TextField3.Text = dt.Rows[0]["cln1_col7"].ToString();
                    try
                    {
                        if (TextField3.Text != (before_weight - Convert.ToDecimal(txt_weight_after_expect.Text)).ToString())
                        {
                            TextField3.RemoveCls("Text-red");
                            TextField3.AddCls("Text-black");
                        }
                    }
                    catch { }
                    init_PAGE1();
                    show_Panel2(info_date1.Text); //淨化明細 
                }
            }
            else
            {
                init_PAGE2();
                Show_TPRBP();

                pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text);
                if (pat_obj.status != pat.pat_status.not_login)
                {
                    ImageBtn_TurnOff.Enabled = true;
                    ImageBtn_TurnOff.Visible = true;
                    if (pat_obj.status == pat.pat_status.stop) //是否已經停機判斷
                    {
                        ImageBtn_TurnOff.ImageUrl = "Styles/Red_Btn.png";
                    }
                    else if (pat_obj.status == pat.pat_status.not_stop)
                    {
                        ImageBtn_TurnOff.ImageUrl = "Styles/Green_Btn.png";
                    }
                }
            }
            
            dt.Dispose();
            CheckcboValue();
            db.myConnection.Close();
        }

        protected void Show_TPRBP()
        {
            string person_id = patient_id.Text;
            string DialysisDate = DateTime.Now.ToString("yyyy-MM-dd");
            String sql = "SELECT a.dialysis_date, a.dialysis_time, a.column_7, a.column_6, a.column_3, a.column_10, a.column_8, a.column_4, ";
            sql += "b.cln2_t,b.cln2_p,b.cln2_r,b.cln2_bp,b.cln2_rmk,b.cln2_user,b.cln2_time FROM clinical2_nurse as b ";
            sql += "LEFT JOIN data_list as a ON a.person_id=b.cln2_patic AND a.dialysis_date=b.cln2_date AND a.dialysis_time=b.cln2_time ";
            sql += "WHERE a.person_id='" + person_id + "' AND a.dialysis_date='" + DialysisDate + "' ";
            sql += "ORDER BY a.dialysis_time";
            DataTable dt = db.Query(sql);

            Store istore = Grid_Show_TPRBP.GetStore();
            istore.DataSource =  GetDataArray(dt);
            istore.DataBind();
            db.myConnection.Close();
        }

        protected void show_Panel2(string date)
        {
            Panel2.Loader.SuspendScripting();
            Panel2.Loader.Url = "Information/Dialysis_09_02_detail.aspx?date=" + date + "&page=2&patient_id=" + patient_id.Text + "&bedno=" + bedno.Text + "&floor=" +floor.Text;
            Panel2.Loader.DisableCaching = true;
            Panel2.LoadContent();
        }

        protected void init_PAGE1()
        {
            Panel2.Visible = true;
            Panel1.Visible = false;
            ImageButton1.Visible = false;
            Grid_Show_TPRBP.Visible = false;
            FormPanel1.Title = "治疗参数(苏州医院)";

            cbo_diagnosis.ReadOnly = true;
            cbo_mechine_model.ReadOnly = true;
            cbo_hpack3.ReadOnly = true;
            txt_weight_before.ReadOnly = true; 
            txt_weight_after_expect.ReadOnly = true;
            TextField3.ReadOnly = true;
            txt_weight_after.ReadOnly = true;
            TextField8.ReadOnly = true;
            TextField9.ReadOnly = true;
            TextField10.ReadOnly = true;
            
            Checkbox1.ReadOnly = true;
            Checkbox2.ReadOnly = true;
            Checkbox3.ReadOnly = true;
            Checkbox4.ReadOnly = true;
            Checkbox5.ReadOnly = true;

            SelectBoxEPO.ReadOnly = true;
            TextCheckbox2.ReadOnly = true;
            SelectCheckbox3.ReadOnly = true;
            SelectCheckbox4.ReadOnly = true;
            SelectCheckbox5.ReadOnly = true;

            SelectBox3.ReadOnly = true;
            cbo_dialysis_type.ReadOnly = true;
            cbo_change_type.ReadOnly = true;
            cbo_h_type.ReadOnly = true;            
            
            TextField11.ReadOnly = true;
            TextArea1.ReadOnly = true;

            Radio11.ReadOnly = true;
            Radio12.ReadOnly = true;
            Radio13.ReadOnly = true;
            Radio21.ReadOnly = true;
            Radio22.ReadOnly = true;
            Radio31.ReadOnly = true;
            Radio32.ReadOnly = true;
            Radio41.ReadOnly = true;
            Radio42.ReadOnly = true;
            Radio51.ReadOnly = true;
            Radio52.ReadOnly = true;

            cbo_diagnosis.RemoveCls("Text-black");
            cbo_diagnosis.AddCls("Text-blue");

            cbo_mechine_model.RemoveCls("Text-black");
            cbo_mechine_model.AddCls("Text-blue");

            cbo_hpack3.RemoveCls("Text-black");
            cbo_hpack3.AddCls("Text-blue");

            txt_weight_before.RemoveCls("Text-black");
            txt_weight_before.AddCls("Text-blue");
            TextField3.RemoveCls("Text-black");
            TextField3.AddCls("Text-blue");
            SelectBox3.RemoveCls("Text-black");
            SelectBox3.AddCls("Text-blue");
            cbo_dialysis_type.RemoveCls("Text-black");
            cbo_dialysis_type.AddCls("Text-blue");
            cbo_change_type.RemoveCls("Text-black");
            cbo_change_type.AddCls("Text-blue");
            cbo_h_type.RemoveCls("Text-black");
            cbo_h_type.AddCls("Text-blue");
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
                    if (Radio11.Checked == true)
                    {
                        Col11 = Radio11.InputValue;
                        coltext11 = "";
                    }
                    else if (Radio12.Checked == true)
                    {
                        Col11 = Radio12.InputValue;
                        coltext11 = "";
                    }
                    else if (Radio13.Checked == true)
                    {
                        Col11 = Radio13.InputValue;
                        coltext11 = RField1.Text;
                    }

                    if (Radio21.Checked == true)
                    {
                        Col21 = Radio21.InputValue;
                        coltext21 = "";
                    }
                    else if (Radio22.Checked == true)
                    {
                        Col21 = Radio22.InputValue;
                        coltext21 = RField2.Text;
                    }

                    if (Radio31.Checked == true)
                    {
                        Col31 = Radio31.InputValue;
                        coltext31 = "";
                    }
                    else if (Radio32.Checked == true)
                    {
                        Col31 = Radio32.InputValue;
                        coltext31 = RField3.Text;
                    }

                    if (Radio41.Checked == true)
                    {
                        Col41 = Radio41.InputValue;
                        coltext41 = "";
                    }
                    else if (Radio42.Checked == true)
                    {
                        Col41 = Radio42.InputValue;
                        coltext41 = RField4.Text;
                    }

                    if (Radio51.Checked == true)
                    {
                        Col50 = Radio51.InputValue;
                    }
                    else if (Radio52.Checked == true)
                    {
                        Col50 = Radio52.InputValue;
                    }

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

                if (TextCheckbox2.Text == "") TextCheckbox2.Text = "0";
                if (TextCheckbox3q.Text == "") TextCheckbox3q.Text = "0";
                if (TextCheckbox4q.Text == "") TextCheckbox4q.Text = "0";
                if (TextCheckbox5q.Text == "") TextCheckbox5q.Text = "0";
                string checktext = ",";
                if (SelectBoxEPO.Text.Trim() != "")
                {
                    checktext += "EPO,";
                }
                if (TextCheckbox2.Text.Trim() != "0")
                {
                    checktext += "左卡,";
                }
                if (SelectCheckbox3.Text.Trim() != "")
                {
                    checktext += "钙剂,";
                }
                if (SelectCheckbox4.Text.Trim() != "")
                {
                    checktext += "稀释液,";
                }
                if (SelectCheckbox5.Text.Trim() != "")
                {
                    checktext += "其它";
                }

                sql = "SELECT a.cln1_diadate FROM clinical1_nurse a ";
                sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate = '" + info_date1.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical1_nurse a ";
                    sql += " SET a.cln1_col1 = '" + Common.GetComboBoxText(cbo_diagnosis) + "',";
                    sql += "     a.cln1_col2 = '" + bedno.Text + "',";
                    sql += "     a.cln1_col3 = '" + machine_type.Text + "',";
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
                    sql += "     a.cln1_col28 = '" + SelectBoxEPO.Text.Replace("'","''") + "',";
                    sql += "     a.cln1_col29 = '" + TextCheckbox2.Text + "',";
                    sql += "     a.cln1_col30 = '" + SelectCheckbox3.Text + "',";
                    sql += "     a.cln1_col31 = '" + SelectCheckbox4.Text + "',";
                    sql += "     a.cln1_col32 = '" + SelectCheckbox5.Text + "',";
                    if (TextCheckbox3q.Text == "") TextCheckbox3q.Text = "0";
                    if (TextCheckbox4q.Text == "") TextCheckbox4q.Text = "0";
                    if (TextCheckbox5q.Text == "") TextCheckbox5q.Text = "0";
                    sql += "     a.cln1_col30q = " + TextCheckbox3q.Text + ",";
                    sql += "     a.cln1_col31q = " + TextCheckbox4q.Text + ",";
                    sql += "     a.cln1_col32q = " + TextCheckbox5q.Text + ",";

                    sql += "     a.cln1_col17 = '" + SelectBox3.Text + "',";
                    sql += "     a.cln1_col18 = '" + Common.GetComboBoxText(cbo_change_type) + "',";
                    sql += "     a.cln1_col19 = '" + TextField11.Text + "',";
                    sql += "     a.cln1_col20 = '" + TextField12.Text + "',";
                    sql += "     a.cln1_col21 = '" + TextField13.Text + "',";
                    sql += "     a.cln1_col22 = '" + TextField14.Text + "',";
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
                    sql += "     a.cln1_col36 = '" + TextField25.Text + "',";
                    sql += "     a.cln1_user = '',";
                    sql += "     a.cln1_dateadded = ''";
                    sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' ";
                    sql += "  AND a.cln1_diadate = '" + info_date1.Text + "' ";
                }
                else
                {
                    sql = "INSERT into clinical1_nurse(cln1_patic,cln1_diadate,";
                    sql += "cln1_col1,cln1_col2,cln1_col3,cln1_col4,cln1_col5,";

                    sql += "cln1_col6,cln1_col7,cln1_col8,cln1_col9,cln1_col10,";
                    sql += "cln1_col11,cln1_col12,cln1_col13,cln1_col14,cln1_col15,";
                    sql += "cln1_col16,cln1_col17,cln1_col18,cln1_col19,cln1_col20,";
                    
                    sql += "cln1_col21,cln1_col22,cln1_col23,cln1_col24,cln1_col25,";
                    sql += "cln1_col26,cln1_col27,";
                    sql += "cln1_col28,cln1_col29,cln1_col30,cln1_col31,cln1_col32, cln1_col30q,cln1_col31q,cln1_col32q,"; //加上數量
                    sql += "cln1_col33,cln1_col34,cln1_col35,";
                    sql += "cln1_col36,cln1_user,cln1_dateadded) ";
                    sql += "VALUES('" + patient_id.Text + "','" + info_date1.Text + "','";
                    sql += Common.GetComboBoxText(cbo_diagnosis) + "','" + bedno.Text + "','" + machine_type.Text + "','" + hpack.Text + "','" + txt_weight_before.Text + "','";
                    //sql += Common.GetComboBoxText(cbo_diagnosis) + "','" + bedno.Text + "','" + machine_type.Text + "','" + txt_weight_before.Text + "','";
                    sql += txt_weight_after_expect.Text + "','" + TextField3.Text + "','" + txt_weight_after.Text + "','" + info_date1.Text + "','" + TextField5.Text + "','";
                    sql += TextField6.Text + "','" + TextField7.Text + "','" + TextField8.Text + "','" + TextField9.Text + "','" + TextField10.Text + "','";
                    sql += checktext.Substring(1); //cln1_col16                    
                    sql += "','" + SelectBox3.Text + "','" + Common.GetComboBoxText(cbo_change_type) + "','" + TextField11.Text + "','" + TextField12.Text + "',";
                    
                    sql += "'" + TextField13.Text + "','" + TextField14.Text + "','" + TextArea1.Text + "','','',";                    
                    sql += "'" + Common.GetComboBoxText(cbo_mechine_model) + "','" + Common.GetComboBoxText(cbo_hpack3) + "',";
                    //擬用藥 EPO...Value
                    sql += "'" + SelectBoxEPO.Text + "','" + TextCheckbox2.Text + "','" + SelectCheckbox3.Text + "','" + SelectCheckbox4.Text + "','" + SelectCheckbox5.Text + "',";
                    sql += TextCheckbox3q.Text + "," + TextCheckbox4q.Text + "," + TextCheckbox5q.Text + ",";
                    //擬用藥
                    sql += "'" + TextField23.Text + "','" + Common.GetComboBoxText(cbo_h_type) + "','" + TextField24.Text + "',";
                    sql += "'" + TextField25.Text + "','','')";
                }
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
                         "SET pv_macno='" + Common.GetComboBoxValue(cbo_dialysis_type) + "', " +
                             "pv_weight='" + txt_weight_before.Text + "', " +
                             "pv_hpack='" + Common.GetComboBoxValue(cbo_h_type) + "', " + //20160321 Alex
                             "pv_hpack2='" + Common.GetComboBoxValue(cbo_mechine_model) + "', " +
                             "pv_hpack3='" + Common.GetComboBoxValue(cbo_hpack3) + "' " + //20160321 Alex
                       "WHERE pv_ic='" + patient_id.Text + "' " +
                         "AND pv_datevisit='" + info_date1.Text + "' ";
                db.Excute(sql);


                sql = "SELECT * FROM clinical3_nurse ";
                sql += " where cln3_patic = '" + patient_id.Text + "' ";
                sql += "  and cln3_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical3_nurse ";
                    sql += "SET cln3_doc1 = '" + TextField23.Text + "', ";
                    sql += "    cln3_nur1 = '" + TextField25.Text + "' ";
                    sql += "WHERE cln3_patic = '" + patient_id.Text + "' ";
                    sql += "AND cln3_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                }
                else
                {
                    sql = "INSERT INTO clinical3_nurse(cln3_patic,cln3_date,cln3_doc1,cln3_nur1) ";
                    sql += "VALUES('" + patient_id.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + TextField23.Text + "','" + TextField25.Text + "')";
                }

                CheckcboValue();
                Common._NotificationShow("<font size=4>储存成功!</font>");

                //db.Close();
            }
            //}
            //catch (Exception ex)
            //{
            //    _ErrorMsgShow(ex.Message.ToString());
            //}
        }

        protected void Btn_save_bp_Click(object sender, DirectEventArgs e)
        {
            if (TextField1.Text == "")
            {
                if (TextField17.Text == "" && TextField18.Text == "" && TextField19.Text == "" && TextField20.Text == "" && TextField21.Text == "" && TextArea2.Text == "")
                {
                    Common._NotificationShow("<font size=4>请输入资料!</font>");
                    return;
                }
                try
                {
                    string sql = "SELECT dialysis_time FROM data_list ";
                    sql += "WHERE person_id = '" + patient_id.Text + "' AND dialysis_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                    sql += "AND column_11 <>'' ";
                    sql += "AND ABS(TIME_TO_SEC(TIMEDIFF('" + DateTime.Now.ToString("HH:mm:ss") + "',STR_TO_DATE(dialysis_time,'%H:%i:%s'))))<=10 ";
                    sql += "ORDER BY SUBSTR(TIMEDIFF(CURRENT_TIME(),STR_TO_DATE(dialysis_time,'%H:%i:%s')) FROM -8 FOR 8) ASC LIMIT 1 ";
                    DataTable dt = db.Query(sql); //抓出最接近現在時間的一筆資料

                    DateTime date1 = DateTime.Now;
                    if (dt.Rows.Count > 0) //資料庫原有時間超過10秒便重新再輸入一筆新的
                    {
                        TextField1.Text = "";
                        Common._NotificationShow("已经储存过了");
                        db.myConnection.Close();
                        return;
                    }
                    else
                    {
                        if (TextFieldTime.Text == "")
                        {
                            string timenow = "";
                            timenow = string.Format("{0:00}", date1.Hour) + ":" + string.Format("{0:00}", date1.Minute) + ":" + string.Format("{0:00}", date1.Second);
                            TextFieldTime.Text = timenow;
                            TextField1.Text = date1.ToString("yyyy-MM-dd") + " " + TextFieldTime.Text;
                        }
                        sql = "INSERT INTO clinical2_nurse(cln2_patic,cln2_date,cln2_time,cln2_t,cln2_p,cln2_r,cln2_bp,cln2_rmk,cln2_user,cln2_dateadded) ";
                        sql += "VALUES('" + patient_id.Text + "','" + date1.ToString("yyyy-MM-dd");
                        sql += "','" + TextFieldTime.Text + "','" + TextField17.Text + "','";
                        sql += TextField18.Text + "','" + TextField19.Text + "','";
                        sql += TextField20.Text + "','" + TextArea2.Text+ "','";
                        sql += TextField21.Text + "',''); ";

                        sql += "INSERT INTO data_list(person_id, floor_no, bed_no, dialysis_date, dialysis_time";
                        sql += ", column_7, column_6, column_3, column_10, column_8, column_4, column_11) ";
                        sql += "VALUES('" + patient_id.Text + "','";
                        sql += floor.Text + "','";
                        sql += bedno.Text + "','";
                        sql += date1.ToString("yyyy-MM-dd") + "','" + TextFieldTime.Text + "',";
                        if (TextField_7.Text.Trim() == "")
                            sql += "NULL, ";
                        else
                            sql += TextField_7.Text + ", "; 
                        if (TextField_6.Text.Trim() == "")
                            sql += "NULL, ";
                        else
                            sql += TextField_6.Text + ", ";
                        if (TextField_3.Text.Trim() == "")
                            sql += "NULL, ";
                        else
                            sql += TextField_3.Text + ",";
                        if (TextField_10.Text.Trim() == "")
                            sql += "NULL, ";
                        else
                            sql += TextField_10.Text + ", ";
                        if (TextField_8.Text.Trim() == "")
                            sql += "NULL, ";
                        else
                            sql += TextField_8.Text + ", ";
                        if (TextField_4.Text.Trim() == "")
                            sql += "NULL, 1);";
                        else
                            sql += TextField_4.Text + ", 1);";
                    }
                    db.Excute(sql);
                    Common._NotificationShow("<font size=4>储存成功!</font>");
                    Show_TPRBP();
                    TextField1.Text = "";
                    TextField_10.Text = "";
                    TextField_8.Text = "";
                    TextField17.Text = "";
                    TextField18.Text = "";
                    TextField19.Text = "";
                    TextField20.Text = "";
                    TextArea2.Text = "";
                    TextFieldTime.Text = "";
                }
                catch (Exception ex)
                {
                    Common._NotificationShow("<font size=4>资料储存失败!</font>");
                }
            }
            else
            {
                string[] s = TextField1.Text.Split(new Char[] { ',' });
                if (s.Length == 3)
                {                    
                    string sDATE = DateTime.Now.ToString("yyyy-MM-dd");
                    string sTIME = s[0];
                    string sID = s[2];
                    string sql = "start transaction;";
                    sql += "UPDATE data_list ";
                    sql += "    SET dialysis_time = '" + TextFieldTime.Text + "', ";
                    if (TextField_7.Text.Trim() == "")
                        sql += "column_7=NULL, ";
                    else
                        sql += "column_7=" + TextField_7.Text + ", ";
                    if (TextField_6.Text.Trim() == "")
                        sql += "column_6=NULL, ";
                    else
                        sql += "column_6=" + TextField_6.Text + ", ";
                    if (TextField_3.Text.Trim() == "")
                        sql += "column_3=NULL, ";
                    else
                        sql += "column_3=" + TextField_3.Text + ", ";
                    if (TextField_10.Text.Trim() == "")
                        sql += "column_10=NULL, ";
                    else
                        sql += "column_10=" + TextField_10.Text + ", ";
                    if (TextField_8.Text.Trim() == "")
                        sql += "column_8=NULL, ";
                    else
                        sql += "column_8=" + TextField_8.Text + ", ";
                    if (TextField_4.Text.Trim() == "")
                        sql += "column_4=NULL ";
                    else
                        sql += "column_4=" + TextField_4.Text + " ";
                    sql += "WHERE person_id = '" + patient_id.Text + "' ";
                    sql += "  AND dialysis_date = '" + sDATE + "' ";
                    sql += "  AND dialysis_time = '" + sTIME + "'; ";

                    sql += "UPDATE clinical2_nurse ";
                    sql += "  SET cln2_t = '" + TextField17.Text + "',";
                    sql += "      cln2_p = '" + TextField18.Text + "',";
                    sql += "      cln2_r = '" + TextField19.Text + "',";
                    sql += "      cln2_bp = '" + TextField20.Text + "',";
                    sql += "      cln2_rmk = '" + TextArea2.Text + "',";
                    sql += "      cln2_user = '" + TextField21.Text + "',";
                    sql += "      cln2_time = '" + TextFieldTime.Text + "' ";
                    sql += "WHERE cln2_id = '" + sID + "' ";
                    sql += "  AND cln2_date = '" + sDATE + "' ";
                    sql += "  AND cln2_time = '" + sTIME + "'; ";

                    sql += "commit;";

                    try
                    {
                        db.Excute(sql);
                    }
                    catch (MySql.Data.MySqlClient.MySqlException ex)
                    {
                        if (ex.Number == 1022)
                        {
                            Common._NotificationShow("资料储存失败! 是否有重複的時間?");
                        }
                        else
                        {
                            Common._NotificationShow("资料储存失败!");
                        }
                    }
                    finally
                    {
                        Panel1.Title = "净化过程明细";
                        Show_TPRBP();
                        Common._NotificationShow("<font size=4>修改成功!</font>");
                        TextField1.Text = "";
                        TextField_7.Text = "";
                        TextField_6.Text = "";
                        TextField_3.Text = "";
                        TextField_10.Text = "";
                        TextField_8.Text = "";
                        TextField_4.Text = "";
                        TextField17.Text = "";
                        TextField18.Text = "";
                        TextField19.Text = "";
                        TextField20.Text = "";
                        TextField21.Text = "";
                        TextArea2.Text = "";
                        TextFieldTime.Text = "";
                    }
                }
            }
            db.myConnection.Close();
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
                    else if (a == "TextField21")
                    {
                        TextField21.Text = dt.Rows[0]["acclv_fname"].ToString();
                        DateTime date1 = DateTime.Now;
                        TextFieldTime.Text = string.Format("{0:00}", date1.Hour) + ":" + string.Format("{0:00}", date1.Minute) + ":" + string.Format("{0:00}", date1.Second); 
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

        /** Jeffrey added at 2015/11/19, not finished yet
        /// <summary>
        /// 設定目标定容量那個欄位
        /// </summary>
        private void set_target_volumn()
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
        **/

        protected void Edit_Click(object sender, DirectEventArgs e)
        {
            //DBMysql db = new DBMysql();
            string sTIME = e.ExtraParams["TIME"];
            TextField1.Text = sTIME + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql = "SELECT a.dialysis_date, a.dialysis_time, a.column_7, a.column_6, a.column_3, a.column_10, a.column_8, a.column_4, ";
            sql += "b.cln2_t,b.cln2_p,b.cln2_r,b.cln2_bp,b.cln2_rmk,b.cln2_user,b.cln2_dateadded, cln2_id FROM clinical2_nurse as b ";
            sql += "LEFT JOIN data_list as a ON a.person_id=b.cln2_patic AND a.dialysis_date=b.cln2_date AND a.dialysis_time=b.cln2_time ";
            sql += "WHERE a.person_id = '" + patient_id.Text + "' ";
            sql += "  AND a.dialysis_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "  AND a.dialysis_time = '" + sTIME + "' ";
            DataTable dt2 = db.Query(sql);
            if (dt2.Rows.Count > 0)
            {
                TextField1.Text = TextField1.Text + "," + dt2.Rows[0]["cln2_id"].ToString();

                TextField_7.Text = dt2.Rows[0]["Column_7"].ToString();
                TextField_6.Text = dt2.Rows[0]["Column_6"].ToString();
                TextField_3.Text = dt2.Rows[0]["Column_3"].ToString();
                TextField_10.Text = dt2.Rows[0]["Column_10"].ToString();
                TextField_8.Text = dt2.Rows[0]["Column_8"].ToString();
                TextField_4.Text = dt2.Rows[0]["Column_4"].ToString();

                TextField17.Text = dt2.Rows[0]["cln2_t"].ToString();
                TextField18.Text = dt2.Rows[0]["cln2_p"].ToString();
                TextField19.Text = dt2.Rows[0]["cln2_r"].ToString();
                TextField20.Text = dt2.Rows[0]["cln2_bp"].ToString();
                TextField21.Text = dt2.Rows[0]["cln2_user"].ToString();
                TextArea2.Text = dt2.Rows[0]["cln2_rmk"].ToString();
                TextFieldTime.Text = dt2.Rows[0]["dialysis_time"].ToString();
                Panel1.Title = "净化过程明细 - 修改";
                Common._NotificationShow("一分钟内没存盘，自动取消修改");
                TaskManager1.StartTask("COUNT_DOWN");
            }

            //db.Close();
        }

        protected void Timer1_Timer(object sender, EventArgs e)
        {
            //TextArea2.Text = DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss"); //偵錯使用

            if (TextField1.Text != "")
            {
                string[] s = TextField1.Text.Split(new Char[] { ',' });
                if (s.Length == 3)
                {
                    TimeSpan iDiff = new TimeSpan(DateTime.Now.Ticks - Convert.ToDateTime(s[1]).Ticks );
                    if (iDiff.TotalSeconds > 60)
                    {
                        TextField1.Text = "";
                        Panel1.Title = "净化过程明细";
                        TextField_7.Text = "";
                        TextField_6.Text = "";
                        TextField_3.Text = "";
                        TextField_10.Text = "";
                        TextField_8.Text = "";
                        TextField_4.Text = "";
                        TextField1.Text = "";
                        TextField17.Text = "";
                        TextField18.Text = "";
                        TextField19.Text = "";
                        TextField20.Text = "";
                        TextField21.Text = "";
                        TextArea2.Text = "";
                        TaskManager1.StopTask("COUNT_DOWN");
                    }
                }
                else
                {
                    TextField1.Text = "";
                    Panel1.Title = "净化过程明细";
                    TextField_7.Text = "";
                    TextField_6.Text = "";
                    TextField_3.Text = "";
                    TextField_10.Text = "";
                    TextField_8.Text = "";
                    TextField_4.Text = "";
                    TextField1.Text = "";
                    TextField17.Text = "";
                    TextField18.Text = "";
                    TextField19.Text = "";
                    TextField20.Text = "";
                    TextField21.Text = "";
                    TextArea2.Text = "";
                    TaskManager1.StopTask("COUNT_DOWN");
                }
            }
            if (TextField1.Text == "")
                TaskManager1.StopTask("COUNT_DOWN");
        }

        protected void textreplace(TextField tex)
        {
            tex.Text.Replace("'", "''");
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

        #region 回首頁
        protected void Btn_Home_Click(object sender, DirectEventArgs e)
        {
            X.Redirect(ConfigurationManager.AppSettings["iPAD"].ToString().Replace("../", ""));
        }
        #endregion

        #region 治療參數
        protected void Btn_detail01_Click(object sender, DirectEventArgs e)
        {
            if (page.Text == "2")
            {
                //DBMysql db = new DBMysql();
                string sql;
                sql = "SELECT a.* ";
                sql += "FROM clinical1_nurse a ";
                sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' ";
                sql += " and a.cln1_diadate <> '" + DateTime.Now.ToString("yyyy-MM-dd") + "'"; //確認是否有歷史資料
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    string sURL;
                    switch (sProcessOfPurifyingTheBlood)
                    {
                        case "Hospital_Suzhou":
                            sURL = "ipad_detaillist_Suzhou.aspx";
                            break;
                        case "Hospital_117":
                            sURL = "ipad_detaillist_117.aspx";
                            break;
                        case "Hospital_Xian":
                            sURL = "ipad_detaillist_Xian.aspx";
                            break;
                        case "Hospital_Henan":
                            sURL = "ipad_detaillist_Henan.aspx";
                            break;
                        case "Standard":
                            sURL = "ipad_detaillist_Standard.aspx";
                            break;
                        default:
                            sURL = "ipad_detaillist.aspx";
                            break;
                    }
                    X.Redirect(sURL + "?patient_id=" + patient_id.Text +
                                        "&patient_name=" + patient_name.Text +
                                        "&machine_type=" + machine_type.Text +
                                        "&mechine_model=" + cbo_mechine_model.SelectedItem.Value +
                                        "&hpack=" + cbo_h_type.Text +
                                        "&hpack3=" + cbo_hpack3.SelectedItem.Value +
                                        "&patient_weight=" + patient_weight.Text +
                                        "&bedno=" + bedno.Text +
                                        "&floor=" + floor.Text +
                                        "&area=" + area.Text +
                                        "&time=" + time.Text +
                                        "&daytyp=" + daytyp.Text + "&page=1");
                }
                else
                {
                    Common._ErrorMsgShow("查无前次资料");
                    return;
                }
            }
        }
        #endregion

        #region 血液淨化
        protected void Btn_detail_Click(object sender, DirectEventArgs e)
        {
            if (page.Text == "1")
            {
                pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text, patient_id.Text);

                pat.pat_status pat_status = pat.get_pat_status(pat_obj);
                if (pat_status == pat.pat_status.not_login)
                {
                    Common._ErrorMsgShow("病患尚未登入!");
                    return;
                }
                string sURL;
                switch (sProcessOfPurifyingTheBlood)
                {
                    case "Hospital_Suzhou":
                        sURL = "ipad_detaillist_Suzhou.aspx";
                        break;
                    case "Hospital_117":
                        sURL = "ipad_detaillist_117.aspx";
                        break;
                    case "Hospital_Xian":
                        sURL = "ipad_detaillist_Xian.aspx";
                        break;
                    case "Hospital_Henan":
                        sURL = "ipad_detaillist_Henan.aspx";
                        break;
                    case "Standard":
                        sURL = "ipad_detaillist_Standard.aspx";
                        break;
                    default:
                        sURL = "ipad_detaillist.aspx";
                        break;
                }
                X.Redirect(sURL + "?patient_id=" + patient_id.Text +
                                        "&patient_name=" + patient_name.Text +
                                        "&machine_type=" + machine_type.Text +
                                        "&mechine_model=" + cbo_mechine_model.SelectedItem.Value +
                                        "&hpack=" + cbo_h_type.Text +
                                        "&hpack3=" + cbo_hpack3.SelectedItem.Value +
                                        "&patient_weight=" + patient_weight.Text +
                                        "&bedno=" + bedno.Text +
                                        "&floor=" + floor.Text +
                                        "&area=" + area.Text +
                                        "&time=" + time.Text +
                                        "&daytyp=" + daytyp.Text + "&page=2");
                return;
            }
        }
        #endregion

        #region 淨化小結
        protected void Btn_detail02_Click(object sender, DirectEventArgs e)
        {
            pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text, patient_id.Text);
            pat.pat_status pat_status = pat.get_pat_status(pat_obj);
            if (pat_status == pat.pat_status.not_login)
            {
                Common._ErrorMsgShow("病患尚未登入!");
                return;
            }
            else if (pat_status == pat.pat_status.not_stop)
            {
                Common._ErrorMsgShow("机器尚未停机!");
                return;
            }

            string sURL;
            switch (sProcessOfPurifyingTheBlood)
            {
                case "Hospital_Suzhou":
                    sURL = "ipad_detaillist02_Suzhou.aspx";
                    break;
                case "Hospital_117":
                    sURL = "ipad_detaillist02_117.aspx";
                    break;
                case "Hospital_Xian":
                    sURL = "ipad_detaillist02_Xian.aspx";
                    break;
                case "Hospital_Henan":
                    sURL = "ipad_detaillist02_Henan.aspx";
                    break;
                case "Standard":
                    sURL = "ipad_detaillist02_Standard.aspx";
                    break;
                default:
                    sURL = "ipad_detaillist02.aspx";
                    break;
            }
            X.Redirect(sURL + "?patient_id=" + patient_id.Text +
                                "&patient_name=" + patient_name.Text +
                                "&bedno=" + bedno.Text +
                                "&floor=" + floor.Text +
                                "&mechine_model=" + cbo_mechine_model.SelectedItem.Value +
                                "&hpack=" + cbo_h_type.Text +
                                "&hpack3=" + cbo_hpack3.SelectedItem.Value +
                                "&area=" + area.Text +
                                "&time=" + time.Text +
                                "&daytyp=" + daytyp.Text);
        }
        #endregion

        #region 上一頁
        protected void Btn_back_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("ipad_PatientList.aspx?editmode=page3&floor=" + floor.Text +
                                                                  "&area=" + area.Text +
                                                                  "&time=" + time.Text +
                                                                  "&bedno=" + bedno.Text +
                                                                  "&dayTyp=" + daytyp.Text);
        }
        #endregion

        #region 圖形按鈕關機
        protected void ImageBtn_TurnOff_click(object sender, DirectEventArgs e)
        {
            string today = DateTime.Now.ToString("yyyy-MM-dd");
            string Status = "";
            string sql = "SELECT pv_macstat FROM pat_visit ";
            string sqlw = "WHERE pv_ic='" + patient_id.Text + "' AND pv_datevisit = '" + today + "' ";

            sql += sqlw;
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pv_macstat"].ToString() == "S")
                {
                    ImageBtn_TurnOff.ImageUrl = "Styles/Green_Btn.png";
                    Status = "'A'";
                }
                else
                {
                    ImageBtn_TurnOff.ImageUrl = "Styles/Red_Btn.png";
                    pat.update.set_dialysis_time(patient_id.Text, floor.Text, bedno.Text, info_date1.Text);
                    Status = "'S'";
                    ShowStopTime();
                }
                sql = "UPDATE pat_visit SET pv_macstat = " + Status + " ";
                sql += sqlw;
                db.Excute(sql);
            }
        }
        #endregion

        public void ShowStopTime()
        {
            string sql = "SELECT cln1_col11, cln1_col12 FROM clinical1_nurse ";
            sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate='" + info_date1.Text + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                TextField6.Text = dt.Rows[0]["cln1_col11"].ToString();
                TextField7.Text = dt.Rows[0]["cln1_col12"].ToString();
            }
            dt.Dispose();
        }
    }
}