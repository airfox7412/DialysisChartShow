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
    public partial class ipad_detaillist_Xian : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        string cln1_col1, cln1_col3, cln1_col6, cln1_col10, cln1_col13, cln1_col14, cln1_col15, cln1_col16, cln1_col17, cln1_col18, cln1_col19;
        string cln1_col26, cln1_col27, cln1_col34, cln1_col35, cln1_col37, cln1_col38;
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

                if (Session["USER_NAME"] == null || Session["USER_RIGHT"] == null)
                    X.Redirect("loginXian.aspx?patient_id=" + patient_id.Text +
                                        "&patient_name=" + patient_name.Text +
                                        "&machine_type=" + machine_type.Text +
                                        "&mechine_model=" + mechine_model.Text +
                                        "&hpack=" + hpack.Text +
                                        "&hpack3=" + hpack3.Text +
                                        "&patient_weight=" + patient_weight.Text +
                                        "&bedno=" + bedno.Text +
                                        "&floor=" + floor.Text +
                                        "&area=" + area.Text +
                                        "&time=" + time.Text +
                                        "&daytyp=" + daytyp.Text +
                                        "&page=" + page.Text);
                if (Session["USER_RIGHT"] != null)
                {
                    if (Session["USER_RIGHT"].ToString() == "NU")
                    {
                        nurse_flag.Text = "true";
                        SetFields(true);
                    }
                    else //if (Session["USER_RIGHT"].ToString() == "DC" || Session["USER_RIGHT"].ToString() != "DH")
                    {
                        nurse_flag.Text = "false";
                        SetFields(false);
                    }

                    patient_weight.Text = txt_weight_before.Text; //體重
                    Show();
                }
            }
        }

        public void SetFields(bool flag)
        {
            cbo_diagnosis.ReadOnly = flag;
            cbo_mechine_model.ReadOnly = flag;
            info_date1.ReadOnly = flag;
            cbo_dialysis_type.ReadOnly = flag;
            txt_weight_before.ReadOnly = flag;
            txt_weight_after_expect.ReadOnly = flag;
            txt_weight_after.ReadOnly = flag;
            TextField3.ReadOnly = flag;
            TextField5.ReadOnly = flag;
            SelectBox10.ReadOnly = flag;
            TextField6.ReadOnly = flag;
            TextCln1_col35.ReadOnly = flag;
            TextField7.ReadOnly = flag;
            TextFirst.ReadOnly = flag;

            TextAdd.ReadOnly = flag;
            TextAmount.ReadOnly = flag;
            cbo_hpack3.ReadOnly = flag;
            cbo_h_type.ReadOnly = flag;
            TextReplaceAmount.ReadOnly = flag;
            TextField2.ReadOnly = flag;
            TextField4.ReadOnly = flag;
            TextField9.ReadOnly = flag;
            TextField10.ReadOnly = flag;
            TextField11.ReadOnly = flag;
            TextField15.ReadOnly = flag;

            CheckboxEPO.ReadOnly = flag;
            SelectBoxEPO.ReadOnly = flag;
            CheckboxLCard.ReadOnly = flag;
            SelectBoxLcard.ReadOnly = flag;
            CheckboxMecobalamin.ReadOnly = flag;
            SelectBoxMecobalamin.ReadOnly = flag;
            CheckboxIron.ReadOnly = flag;
            SelectBoxIron.ReadOnly = flag;
            CheckboxCalcitriol.ReadOnly = flag;
            SelectBoxCalcitriol.ReadOnly = flag;
            CheckboxSodium.ReadOnly = flag;
            SelectBoxSodium.ReadOnly = flag;
            CheckboxPotassium.ReadOnly = flag;
            SelectBoxPotassium.ReadOnly = flag;
            TextArea1.ReadOnly = flag;

            TextField12.Visible = flag;
            TextField13.Visible = flag;
            TextField131.Visible = flag;
            TextField23.Visible = !flag;
        }

        protected void Show()
        {
            GetComboxData(); //取得各項COMBOBOX資料
            Label2.Text = patient_name.Text;
            Label4.Text = floor.Text;
            Label6.Text = bedno.Text;
            string sql;
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate <> '" + toDay + "' ";
            sql += "ORDER BY cln1_diadate DESC LIMIT 1";
            DataTable dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                info_date1.Text = dt0.Rows[0]["cln1_diadate"].ToString(); //上次日期
                cln1_col1 = dt0.Rows[0]["cln1_col1"].ToString();
                cln1_col3 = dt0.Rows[0]["cln1_col3"].ToString();
                cln1_col6 = dt0.Rows[0]["cln1_col6"].ToString();
                cln1_col13 = dt0.Rows[0]["cln1_col13"].ToString();
                cln1_col14 = dt0.Rows[0]["cln1_col14"].ToString();
                cln1_col15 = dt0.Rows[0]["cln1_col15"].ToString();
                cln1_col16 = dt0.Rows[0]["cln1_col16"].ToString();
                cln1_col26 = dt0.Rows[0]["cln1_col26"].ToString();
                cln1_col27 = dt0.Rows[0]["cln1_col27"].ToString();
                cln1_col17 = dt0.Rows[0]["cln1_col17"].ToString(); //
                cln1_col18 = dt0.Rows[0]["cln1_col18"].ToString(); //置换方式
                cln1_col19 = dt0.Rows[0]["cln1_col19"].ToString(); //置换方式
                cln1_col34 = dt0.Rows[0]["cln1_col34"].ToString(); //血管通路
                cln1_col35 = dt0.Rows[0]["cln1_col35"].ToString();
                cln1_col37 = dt0.Rows[0]["cln1_col37"].ToString();
                cln1_col38 = dt0.Rows[0]["cln1_col38"].ToString(); 
            };
            dt0.Dispose();

            // page1: 治療參數, page2: 血液淨化 

            if (page.Text == "2") 
            {
                info_date1.Text = toDay; //今天日期
            }

            Common.SetComboBoxValue(cbo_dialysis_type, machine_type.Text, false);//透析方式

            DataTable dt = new DataTable();
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate='" + info_date1.Text + "' ";
            if (info_date1.Text == toDay)
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
                Common.SetComboBoxValue(cbo_dialysis_type, cln1_col3, false);//透析方式
                Common.SetComboBoxValue(cbo_h_type, cln1_col34, false);//血管通路
                Common.SetComboBoxValue(SelectBox10, cln1_col15, false);//抗凝剂
                TextCln1_col35.Text = cln1_col35;
                TextField5.Text = cln1_col10;
                TextFirst.Text = cln1_col13;
                TextAdd.Text = cln1_col14;
                TextAmount.Text = cln1_col17;
                TextReplaceAmount.Text = cln1_col19; //置換量
            }
            else
            {
                sql = "SELECT a.*, c.cln1_col11 AS Xian1, c.cln1_col12 AS Xian2, c.cln1_col21 AS Xian3, ";
                sql += "c.cln1_col22 AS Xian4, c.cln1_col31 AS Xian5, c.cln1_col32 AS Xian6, ";
                sql += "b.cln1_col11 as col11, b.cln1_col12 as col12, ";
                sql += "b.cln1_col21 as col21, b.cln1_col22 as col22, ";
                sql += "b.cln1_col31 as col31, b.cln1_col32 as col32, ";
                sql += "b.cln1_col41 as col41, b.cln1_col42 as col42, ";
                sql += "b.cln1_col50 as col50 FROM clinical1_nurse a ";
                sql += "LEFT JOIN clinical1_nurse_suzhou b ON a.cln1_patic=b.cln1_patic AND a.cln1_diadate=b.cln1_diadate ";
                sql += "LEFT JOIN clinical1_nurse_Xian c ON a.cln1_patic=c.cln1_patic AND a.cln1_diadate=c.cln1_diadate ";
                sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate='" + info_date1.Text + "'";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                DataRow row = dt.Rows[0];

                if (row["cln1_col1"].ToString() == "")
                    Common.SetComboBoxValue(cbo_diagnosis, cln1_col1, false);//诊断
                else
                    Common.SetComboBoxValue(cbo_diagnosis, row["cln1_col1"].ToString(), false);

                if (row["cln1_col26"].ToString()=="")
                    Common.SetComboBoxValue(cbo_mechine_model, cln1_col26, false);//透析器型号
                else
                    Common.SetComboBoxValue(cbo_mechine_model, row["cln1_col26"].ToString(), false);//透析器型号

                if (row["cln1_col34"].ToString() == "")
                    Common.SetComboBoxValue(cbo_h_type, cln1_col34, false);//血管通路
                else
                    Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col34"].ToString(), false);//血管通路

                if (row["cln1_col3"].ToString() == "")
                    Common.SetComboBoxValue(cbo_dialysis_type, cln1_col3, false);//透析方式
                else
                    Common.SetComboBoxValue(cbo_dialysis_type, row["cln1_col3"].ToString(), false);

                if (row["cln1_col15"].ToString() == "")
                    Common.SetComboBoxValue(SelectBox10, cln1_col15, false);//抗凝剂
                else
                    Common.SetComboBoxValue(SelectBox10, dt.Rows[0]["cln1_col15"].ToString(), false);//抗凝剂
                if (row["cln1_col35"].ToString() == "")
                    TextCln1_col35.Text = cln1_col35;
                else
                    TextCln1_col35.Text = row["cln1_col35"].ToString(); // 抗凝剂量

                string tube_model = Common.get_tube_model(row, "cln1_col27");
                if (tube_model != null)
                    Common.SetComboBoxValue(cbo_hpack3, tube_model, false); //管路型号

                txt_weight_after.Text = row["cln1_col8"].ToString(); //透析后体重
                if (row["cln1_col6"].ToString() == "")
                    txt_weight_after_expect.Text = cln1_col6; //干體重
                else
                    txt_weight_after_expect.Text = row["cln1_col6"].ToString(); //干体重
                try
                {
                    txt_weight_before.Text = dt.Rows[0]["cln1_col5"].ToString(); //透析前体重
                    if (dt.Rows[0]["cln1_col7"].ToString() == "")
                        TextField3.Text = (Convert.ToDecimal(txt_weight_before.Text) - Convert.ToDecimal(txt_weight_after_expect.Text)).ToString();
                    else
                        TextField3.Text = dt.Rows[0]["cln1_col7"].ToString();
                }
                catch { }

                if (row["cln1_col13"].ToString() == "") //首次剂量
                    TextFirst.Text = cln1_col13;
                else
                    TextFirst.Text = row["cln1_col13"].ToString();

                if (row["cln1_col14"].ToString() == "") //追加量
                    TextAdd.Text = cln1_col14;
                else
                    TextAdd.Text = row["cln1_col14"].ToString();

                TextField5.Text = dt.Rows[0]["cln1_col10"].ToString(); //透析開始時間
                TextField6.Text = dt.Rows[0]["cln1_col11"].ToString(); //透析結束時間
                TextField7.Text = dt.Rows[0]["cln1_col12"].ToString(); //透析合計時間
                TextAmount.Text = row["cln1_col17"].ToString();
                TextReplaceAmount.Text = dt.Rows[0]["cln1_col19"].ToString(); //置換量

                // 由 clinical1_nurse_xian 取得的資料
                TextField2.Text = row["Xian1"].ToString();  // 體溫
                TextField4.Text = row["Xian2"].ToString();  // 心跳
                TextField9.Text = row["Xian3"].ToString();  // 呼吸
                TextField10.Text = row["Xian4"].ToString(); // 血壓
                TextField11.Text = row["Xian5"].ToString(); // 血流量
                TextField15.Text = row["Xian6"].ToString(); // 治疗时间

                TextField12.Text = dt.Rows[0]["cln1_col20"].ToString();
                TextField13.Text = dt.Rows[0]["cln1_col21"].ToString();
                TextField131.Text = dt.Rows[0]["cln1_col25"].ToString();
                TextField23.Text = dt.Rows[0]["cln1_col33"].ToString();

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

                    TextArea1.Text = row["cln1_col23"].ToString();
                    string[] check;
                    if (row["cln1_col16"].ToString()=="")
                        check = cln1_col16.Split(new Char[] { ',' });
                    else
                        check = row["cln1_col16"].ToString().Split(new Char[] { ',' });
                    for (int i = 0; i < check.Length; i++)
                    {
                        if (check[i] == "EPO")
                        {
                            CheckboxEPO.Checked = true;
                            Common.SetComboBoxValue(SelectBoxEPO, dt.Rows[0]["cln1_col28"].ToString(), true);
                        }
                        else if (check[i] == "左卡")
                        {
                            CheckboxLCard.Checked = true;
                            Common.SetComboBoxValue(SelectBoxLcard, dt.Rows[0]["cln1_col29"].ToString(), true);
                        }
                        else if (check[i].Equals("甲钴铵"))
                        {
                            CheckboxMecobalamin.Checked = true;
                            Common.SetComboBoxValue(SelectBoxMecobalamin, dt.Rows[0]["cln1_col30"].ToString(), true);
                        }
                        else if (check[i].Equals("铁剂"))
                        {
                            CheckboxIron.Checked = true;
                            Common.SetComboBoxValue(SelectBoxIron, dt.Rows[0]["cln1_col31"].ToString(), true);
                        }
                        else if (check[i].Equals("溉醇"))
                        {
                            CheckboxCalcitriol.Checked = true;
                            Common.SetComboBoxValue(SelectBoxCalcitriol, dt.Rows[0]["cln1_col32"].ToString(), true);
                        }
                        else if (check[i].Equals("透析液钠"))
                        {
                            CheckboxSodium.Checked = true;
                            if (dt.Rows[0]["cln1_col37"].ToString().Trim()=="")
                                Common.SetComboBoxValue(SelectBoxSodium, cln1_col37, true);
                            else
                                Common.SetComboBoxValue(SelectBoxSodium, dt.Rows[0]["cln1_col37"].ToString(), true);
                        }
                        else if (check[i].Equals("透析液钾"))
                        {
                            CheckboxPotassium.Checked = true;
                            if (dt.Rows[0]["cln1_col38"].ToString().Trim() == "")
                                Common.SetComboBoxValue(SelectBoxPotassium, cln1_col38, true);
                            else
                                Common.SetComboBoxValue(SelectBoxPotassium, dt.Rows[0]["cln1_col38"].ToString(), true);
                        }
                    }
                }
}
            }

            if (page.Text == "1") //非今日之最後一筆資料
            {
                if (dt.Rows.Count > 0)
                {
                    decimal before_weight = 0;
                    if (!txt_weight_before.Text.Trim().Equals(string.Empty))
                        before_weight = Convert.ToDecimal(txt_weight_before.Text);
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

        #region 各類Combobox取值
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

            //sSQL = "SELECT hp_code AS CODE, hp_subgrp AS NAME FROM h_type"; //舊程式使用h_type與FLEX使用不同資料表
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt1, true, "NAME", "CODE");
            cbo_h_type.Select(0);
            dt1.Dispose();
            // 取得抗凝剂類型
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='heparin' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBox10, dt1, true, "NAME", "CODE");
            SelectBox10.Select(0);
            // 取得 EPO 資料
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectEPO' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxEPO, dt1, true, "NAME", "CODE");
            SelectBoxEPO.Select(0);
            // 取得左卡資料
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectLeftCard' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxLcard, dt1, true, "NAME", "CODE");
            SelectBoxLcard.Select(0);
            // 取得甲鈷銨資料
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectMecobalamin' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxMecobalamin, dt1, true, "NAME", "CODE");
            SelectBoxMecobalamin.Select(0);
            // 取得鐵劑資料
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectIron' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxIron, dt1, true, "NAME", "CODE");
            SelectBoxIron.Select(0);
            // 取得溉醇資料
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectCalcitriol' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxCalcitriol, dt1, true, "NAME", "CODE");
            SelectBoxCalcitriol.Select(0);
            // 取得透析液鈉資料
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectSodium' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxSodium, dt1, true, "NAME", "CODE");
            SelectBoxSodium.Select(0);
            // 取得透析液鉀資料
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='SelectPotassium' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBoxPotassium, dt1, true, "NAME", "CODE");
            SelectBoxPotassium.Select(0);

            Common.SetComboBoxValue(cbo_h_type, hpack.Text, true); //血管通路類型
            Common.SetComboBoxValue(cbo_hpack3, hpack3.Text, true); //血管通路
            Common.SetComboBoxValue(cbo_mechine_model, mechine_model.Text, true); //透析器型號
        }
        #endregion

        protected void Show_TPRBP()
        {
            string person_id = patient_id.Text;
            string DialysisDate = toDay;
            

            String sql = "SELECT a.dialysis_date, a.dialysis_time, a.column_7, a.column_6, a.column_3, a.column_12, a.column_8, a.column_4, ";
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
            FormPanel1.Title = "治疗参数(西安武警)";

            cbo_diagnosis.ReadOnly = true;
            cbo_mechine_model.ReadOnly = true;
            cbo_hpack3.ReadOnly = true;
            txt_weight_before.ReadOnly = true; 
            txt_weight_after_expect.ReadOnly = true;
            TextField3.ReadOnly = true;
            txt_weight_after.ReadOnly = true;
            TextFirst.ReadOnly = true;
            TextAdd.ReadOnly = true;
            TextAmount.ReadOnly = true;
            SelectBox10.ReadOnly = true;

            cbo_dialysis_type.ReadOnly = true;
            cbo_h_type.ReadOnly = true;            
            
            TextArea1.ReadOnly = true;

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
            cbo_dialysis_type.RemoveCls("Text-black");
            cbo_dialysis_type.AddCls("Text-blue");
            cbo_h_type.RemoveCls("Text-black");
            cbo_h_type.AddCls("Text-blue");
        }

        protected void init_PAGE2()
        {
            TextField12.RemoveCls("Text-blue");
            TextField12.AddCls("red");
            TextField13.RemoveCls("Text-blue");
            TextField13.AddCls("red");
            TextField23.RemoveCls("Text-blue");
            TextField23.AddCls("red");
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
                string checktext = ",";
                if (SelectBoxEPO.Text.Trim() != "")
                {
                    checktext += "EPO,";
                }
                if (SelectBoxLcard.Text.Trim() != "")
                {
                    checktext += "左卡,";
                }
                if (SelectBoxMecobalamin.Text.Trim() != "")
                {
                    checktext += "甲钴铵,";
                }
                if (SelectBoxIron.Text.Trim() != "")
                {
                    checktext += "铁剂,";
                }
                if (SelectBoxCalcitriol.Text.Trim() != "")
                {
                    checktext += "溉醇,";
                }
                if (SelectBoxSodium.Text.Trim() != "")
                {
                    checktext += "透析液钠,";
                }
                if (SelectBoxPotassium.Text.Trim() != "")
                {
                    checktext += "透析液钾,";
                }

                sql = "SELECT a.cln1_diadate FROM clinical1_nurse a ";
                sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate = '" + info_date1.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
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
                    sql += "     a.cln1_col13 = '" + TextFirst.Text + " ',";
                    sql += "     a.cln1_col14 = '" + TextAdd.Text + " ',";
                    sql += "     a.cln1_col15 = '" + SelectBox10.Text + "',";
                    if (checktext.Length > 1)
                    {
                        sql += " a.cln1_col16 = '" + checktext.Substring(1) + "',";
                    }
                    else
                    {
                        sql += " a.cln1_col16 = '',";
                    }
                    sql += "     a.cln1_col17 = '" + TextAmount.Text + "',";
                    sql += "     a.cln1_col19 = '" + TextReplaceAmount.Text + "',";
                    sql += "     a.cln1_col20 = '" + TextField12.Text + "',";
                    sql += "     a.cln1_col21 = '" + TextField13.Text + "',";
                    //sql += "     a.cln1_col22 = '" + TextField14.Text + "',";
					sql += "     a.cln1_col25 = '" + TextField131.Text + "',";
                    sql += "     a.cln1_col23 = '" + TextArea1.Text + "',";
                    sql += "     a.cln1_col26 = '" + Common.GetComboBoxText(cbo_mechine_model) + "', ";
                    sql += "     a.cln1_col28 = '" + Common.GetComboBoxText(SelectBoxEPO) + "', ";
                    sql += "     a.cln1_col29 = '" + Common.GetComboBoxText(SelectBoxLcard) + "', ";
                    sql += "     a.cln1_col30 = '" + Common.GetComboBoxText(SelectBoxMecobalamin) + "', ";
                    sql += "     a.cln1_col31 = '" + Common.GetComboBoxText(SelectBoxIron) + "', ";
                    sql += "     a.cln1_col32 = '" + Common.GetComboBoxText(SelectBoxCalcitriol) + "', ";
                    sql += "     a.cln1_col33 = '" + TextField23.Text + "',";
                    if (cbo_hpack3.Text == "")
                        sql += "a.cln1_col27 = '血管路', ";
                    else
                        sql += "a.cln1_col27 = '" + Common.GetComboBoxText(cbo_hpack3) + "', ";
                    sql += "     a.cln1_col34 = '" + Common.GetComboBoxText(cbo_h_type) + "',";
                    sql += "     a.cln1_col35 = '" + TextCln1_col35.Text + "',"; //抗凝剂量
                    //sql += "     a.cln1_col36 = '" + TextField25.Text + "',";
                    sql += "     a.cln1_col37 = '" + Common.GetComboBoxText(SelectBoxSodium) + "', ";
                    sql += "     a.cln1_col38 = '" + Common.GetComboBoxText(SelectBoxPotassium) + "', ";
                    sql += "     a.cln1_user = '' ";            //sql += "     a.cln1_user = '',";
                    //sql += "     a.cln1_dateadded = '' ";
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
                    sql += "cln1_col28,cln1_col29,";            // 加上擬用藥數量 EPO 左卡
                    sql += "cln1_col30,cln1_col31,cln1_col32,"; // 甲鈷銨 鐵劑 溉醇
                    sql += "cln1_col37,cln1_col38,";            // 透析液鈉 透析液鉀
                    sql += "cln1_col33,cln1_col34,cln1_col35,";
                    sql += "cln1_col36,cln1_user,cln1_dateadded) ";
                    sql += "VALUES('" + patient_id.Text + "','" + info_date1.Text + "','";
                    sql += Common.GetComboBoxText(cbo_dialysis_type) + "','" + bedno.Text + "','" + machine_type.Text + "','" + hpack.Text + "','" + txt_weight_before.Text + "','";
                    //sql += Common.GetComboBoxText(cbo_diagnosis) + "','" + bedno.Text + "','" + machine_type.Text + "','" + txt_weight_before.Text + "','";
                    sql += txt_weight_after_expect.Text + "','" + TextField3.Text + "','" + txt_weight_after.Text + "','" + info_date1.Text + "','" + TextField5.Text + "','";
                    sql += TextField6.Text + "','" + TextField7.Text + "','" + TextFirst.Text + "','" + TextAdd.Text + "','" + Common.GetComboBoxText(SelectBox10) + "','";
                    sql += checktext.Substring(1) + "','"; //cln1_col16 拟用药
                    sql += TextAmount.Text + "','','" + TextReplaceAmount.Text + "','" + TextField12.Text + "','"; //cln1_col19 置換量
                    sql += TextField13.Text + "','','" + TextArea1.Text + "','','" + TextField131.Text + "','";
                    sql += Common.GetComboBoxText(cbo_mechine_model) + "','" + Common.GetComboBoxText(cbo_hpack3) + "','";
                    //擬用藥 EPO...Value
                    //sql += "'','',";
                    sql += Common.GetComboBoxText(SelectBoxEPO) + "','" + Common.GetComboBoxText(SelectBoxLcard) + "','";
                    sql += Common.GetComboBoxText(SelectBoxMecobalamin) + "','" + Common.GetComboBoxText(SelectBoxIron) + "','" + Common.GetComboBoxText(SelectBoxCalcitriol) + "','";
                    sql += Common.GetComboBoxText(SelectBoxSodium) + "','" + Common.GetComboBoxText(SelectBoxPotassium) + "','";
                    //擬用藥
                    sql += TextField23.Text + "','" + Common.GetComboBoxText(cbo_h_type) + "','";
                    sql += TextCln1_col35.Text + "','','','')"; //抗凝剂量
                }
                db.Excute(sql);

                //更新clinical1_nurse_xian
                sql = "SELECT n.cln1_diadate FROM clinical1_nurse_xian n ";
                sql += "WHERE n.cln1_patic = '" + patient_id.Text + "' AND n.cln1_diadate = '" + info_date1.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical1_nurse_xian n ";
                    sql += " SET n.cln1_col11 = '" + TextField2.Text + "',";
                    sql += "     n.cln1_col12 = '" + TextField4.Text + "',";
                    sql += "     n.cln1_col21 = '" + TextField9.Text + "',";
                    sql += "     n.cln1_col22 = '" + TextField10.Text + "',";
                    sql += "     n.cln1_col31 = '" + TextField11.Text + "',";
                    sql += "     n.cln1_col32 = '" + TextField15.Text + "' ";
                    sql += "WHERE n.cln1_patic = '" + patient_id.Text + "' ";
                    sql += "  AND n.cln1_diadate = '" + info_date1.Text + "' ";
                }
                else
                {
                    sql = "INSERT into clinical1_nurse_xian(cln1_patic,cln1_diadate,";
                    sql += "cln1_col11, cln1_col12, cln1_col21, cln1_col22, cln1_col31, cln1_col32) ";
                    sql += "VALUES('" + patient_id.Text + "','" + info_date1.Text + "','";
                    sql += TextField2.Text + "','" + TextField4.Text + "','" + TextField9.Text + "','";
                    sql += TextField10.Text + "','" + TextField11.Text + "','" + TextField15.Text + "')";
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

                //更新clinical3_nurse
                sql = "SELECT * FROM clinical3_nurse ";
                sql += " where cln3_patic = '" + patient_id.Text + "' ";
                sql += "  and cln3_date = '" + toDay + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical3_nurse ";
                    sql += "SET cln3_doc1 = '" + TextField23.Text + "', ";
                    sql += "    cln3_nur1 = '" + TextField13.Text + "' ";
                    sql += "WHERE cln3_patic = '" + patient_id.Text + "' ";
                    sql += "AND cln3_date = '" + toDay + "' ";
                }
                else
                {
                    sql = "INSERT INTO clinical3_nurse(cln3_patic,cln3_date,cln3_doc1,cln3_nur1) ";
                    sql += "VALUES('" + patient_id.Text + "','" + toDay + "','" + TextField23.Text + "','" + TextField13.Text + "')";
                }
                db.Excute(sql);

                CheckcboValue();
                Common._NotificationShow("<font size=4>储存成功!</font>");
			}
		}
	
        protected void Btn_save_bp_Click(object sender, DirectEventArgs e)
        {
            if (TextField1.Text == "")
            {
                if (TextField17.Text == "" && TextField18.Text == "" && TextField19.Text == "" && TextField20.Text == "" && TextArea2.Text == "")
                {
                    Common._NotificationShow("<font size=4>请输入资料!</font>");
                    return;
                }
                else if (TextField21.Text == "")
                {
                    Common._NotificationShow("<font size=4>请输入记录人!</font>");
                    return;
                }
                try
                {
                    string sql = "SELECT dialysis_time FROM data_list ";
                    sql += "WHERE person_id = '" + patient_id.Text + "' AND dialysis_date = '" + toDay + "' ";
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
                        sql += ", column_7, column_6, column_3, column_12, column_8, column_4, column_11) ";
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
                    string sDATE = toDay;
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
                        sql += "column_12=NULL, ";
                    else
                        sql += "column_12=" + TextField_10.Text + ", ";
                    // ---

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
                    sql += "      cln2_user = '" + TextField21.Text + "', ";
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

        #region Work for txt_weight_after_expect
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
        #endregion

        #region Work for 目标定容量
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
        #endregion

        protected void Edit_Click(object sender, DirectEventArgs e)
        {
            //DBMysql db = new DBMysql();
            string sTIME = e.ExtraParams["TIME"];
            TextField1.Text = sTIME + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql = "SELECT a.dialysis_date, a.dialysis_time, a.column_7, a.column_6, a.column_3, a.column_12, a.column_8, a.column_4, ";
            sql += "b.cln2_t,b.cln2_p,b.cln2_r,b.cln2_bp,b.cln2_rmk,b.cln2_user,b.cln2_dateadded, cln2_id FROM clinical2_nurse as b ";
            sql += "LEFT JOIN data_list as a ON a.person_id=b.cln2_patic AND a.dialysis_date=b.cln2_date AND a.dialysis_time=b.cln2_time ";
            sql += "WHERE a.person_id = '" + patient_id.Text + "' ";
            sql += "  AND a.dialysis_date = '" + toDay + "' ";
            sql += "  AND a.dialysis_time = '" + sTIME + "' ";
            DataTable dt2 = db.Query(sql);
            if (dt2.Rows.Count > 0)
            {
                TextField1.Text = TextField1.Text + "," + dt2.Rows[0]["cln2_id"].ToString();

                TextField_7.Text = dt2.Rows[0]["Column_7"].ToString();
                TextField_6.Text = dt2.Rows[0]["Column_6"].ToString();
                TextField_3.Text = dt2.Rows[0]["Column_3"].ToString();
                TextField_10.Text = dt2.Rows[0]["Column_12"].ToString(); //超濾量
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
                sql += " and a.cln1_diadate <> '" + toDay + "'"; //確認是否有歷史資料
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
            // 淨化小結, 這個存在的目的僅僅是為了取一個比Btn_detail02_Click更容易用Ctrl-F找的名字而已
            summary_result();
        }

        /// <summary>
        /// 淨化小結, 這個存在的目的僅僅是為了取一個比Btn_detail02_Click更容易用Ctrl-F找的名字而已
        /// </summary>
        private void summary_result()
        {
            // start of added by jeffrey at 2015/11/18
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

        #region 選擇抗凝剂種類
        protected void select_Heparin(object sender, DirectEventArgs e)
        {
            try
            {
                TextCln1_col35.FieldLabel = SelectBox10.Text;
                if (SelectBox10.Text.Equals("阿加曲班") || SelectBox10.Text.Equals("枸橼酸钠"))
                    TextCln1_col35.IndicatorText = "mg";
                else
                    TextCln1_col35.IndicatorText = "";
            }
            catch { }
        }
        #endregion

        #region 圖形按鈕關機
        protected void ImageBtn_TurnOff_click(object sender, DirectEventArgs e)
        {
            string today = toDay;
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