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

namespace Dialysis_Chart_Show
{
    public partial class ipad_checkin : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        public bool autointo = Boolean.Parse(ConfigurationManager.AppSettings["autointo"].ToString());
        string cln1_col4;

        string cln1_col1, cln1_col6, cln1_col13, cln1_col14, cln1_col15, cln1_col17, cln1_col23;
        string cln1_col26, cln1_col27, cln1_col34, cln1_col28, cln1_col29, cln1_col30, cln1_col31;
        string cln1_col47;
        string Henan1, Henan2, Henan3, Henan4;
        string pck_name, hp2_name, hp3_name, hp4_name, hp5_name;
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");
        public string docname;

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Text_Patient_ID.Text = Request.QueryString["pid"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                bedno.Text = Request.QueryString["bedno"];
                daytyp.Text = Request.QueryString["daytyp"];

                if (Hospital == "Hospital_Alasamo")
                {
                    BtnBlood.Text = "血压量测";
                    BtnBlood.Hidden = false;
                    TextField37.FieldLabel = "透前血压";
                    TextField37.IndicatorText = "mmhg";
                    TextField38.FieldLabel = "透后血压";
                    TextField38.IndicatorText = "mmhg";
                    SelectBox39.FieldLabel = "血管通路情况";
                    TextField37.Hidden = false;
                }
                else if (Hospital == "Hospital_Hebei")
                {
                    BtnBlood.Text = "置换量";
                    BtnBlood.Hidden = false;
                    TextField37.FieldLabel = "置换量";
                    TextField37.IndicatorText = "";
                    TextField38.FieldLabel = "透析后凝血";
                    TextField38.IndicatorText = "";
                    SelectBox39.FieldLabel = "机器消毒时间";
                    TextField37.Hidden = true;
                }
                else
                {
                    TextField37.Hidden = false;
                    BtnBlood.Hidden = true;
                }

                if (Session["USER_NAME"] == null)
                {
                    X.Redirect("ipad_login.aspx");
                }
                docname = Session["USER_NAME"].ToString();

                Show_Patient();
                SetComboBoxItem();
                ShowDialysis();
                Show_drug();
                Show_image();

                hpack.Text = cbo_h_type.Text;
                string sql = "SELECT COUNT(*) as cnts FROM clinical1_nurse WHERE cln1_patic='" + Text_Patient_ID.Text + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                    DialysisTimes.Text = dt.Rows[0]["cnts"].ToString();
                else
                    DialysisTimes.Text = "0";

                sql = "SELECT pif_basetimes FROM pat_info WHERE pif_ic='" + Text_Patient_ID.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    TextBaseTimes.Text = dt.Rows[0]["pif_basetimes"].ToString();
                }
                try
                {
                    TextTimes.Text = (int.Parse(TextBaseTimes.Text) + int.Parse(DialysisTimes.Text)).ToString();
                }
                catch { }
                FirstInsert();
            }
        }
        
        private void SetComboBoxItem()
        {
            string sSQL;

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='diagnosis' ";
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_diagnosis, dt1, true, "NAME", "CODE"); //診斷

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='dialysis_type' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_Machinetype, dt1, true, "NAME", "CODE"); //透析方式

            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt1, true, "NAME", "CODE"); //血管通路類型

            sSQL = "SELECT hp2_code AS CODE, hp2_name AS NAME FROM hpack2_setup";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_machine_model, dt1, true, "NAME", "CODE"); //透析器型号 
            Common.SetComboBoxItem(cbo_machine_model2, dt1, true, "NAME", "CODE"); //透析器型号2
            
            sSQL = "SELECT hp3_code AS CODE, hp3_name AS NAME FROM hpack3_setup";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_hpack3, dt1, true, "NAME", "CODE"); //管路型号
            Common.SetComboBoxItem(cbo_hpack5, dt1, true, "NAME", "CODE"); //管路型号2

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='heparin' ORDER BY CLASS2_CODE";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBox10, dt1, true, "NAME", "CODE"); //抗凝药物
            
            Ext.Net.ListItem litem1, litem2;
            litem1 = new Ext.Net.ListItem(toDay, toDay);
            cb_info_date.Items.Add(litem1);

            sSQL = "SELECT cln1_diadate AS CODE, ";
            sSQL += "CONCAT(cln1_diadate,' | ', cln1_col5) AS NAME1, ";
            sSQL += "CONCAT(cln1_diadate,' | ', cln1_col8) AS NAME2 ";
            sSQL += "FROM clinical1_nurse ";
            sSQL += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate<>'" + toDay + "' ";
            sSQL += "ORDER BY CODE DESC LIMIT 5";
            dt1 = db.Query(sSQL);
            if (dt1.Rows.Count > 0)
            {
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    litem1 = new Ext.Net.ListItem(dt1.Rows[i]["NAME1"].ToString(), dt1.Rows[i]["CODE"].ToString());
                    cb_info_date.Items.Add(litem1);
                    litem2 = new Ext.Net.ListItem(dt1.Rows[i]["NAME2"].ToString(), dt1.Rows[i]["CODE"].ToString());
                    cb_WeightAfter.Items.Add(litem2);
                }
                cb_info_date.GetStore().DataBind();
                cb_WeightAfter.GetStore().DataBind();
            }

            Double d1 = 4;
            for (int j = 0; j < 7; j++)
            {
                litem1 = new Ext.Net.ListItem(d1.ToString() + "h", d1.ToString() + "h");
                SelectDialysisTime.Items.Add(litem1);
                d1 = d1 - 0.5;
            }
        }

        protected void ShowDialysis()
        {
            string sql;
            sql = "SELECT a.*, c.cln1_col11 AS Henan1, c.cln1_col12 AS Henan2, c.cln1_col21 AS Henan3, c.cln1_col22 AS Henan4 ";
            sql += "FROM clinical1_nurse a ";
            sql += "LEFT JOIN clinical1_nurse_henan c ON a.cln1_patic=c.cln1_patic AND a.cln1_diadate=c.cln1_diadate ";
            sql += "WHERE a.cln1_patic='" + Text_Patient_ID.Text + "' AND a.cln1_diadate <> '" + toDay + "' ";
            sql += "ORDER BY a.cln1_diadate DESC LIMIT 1";
            DataTable dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                //cb_info_date.Text = dt0.Rows[0]["cln1_diadate"].ToString(); //上次日期
                cln1_col1 = dt0.Rows[0]["cln1_col1"].ToString();
                cln1_col6 = dt0.Rows[0]["cln1_col6"].ToString();
                cln1_col13 = dt0.Rows[0]["cln1_col13"].ToString();
                cln1_col15 = dt0.Rows[0]["cln1_col15"].ToString();
                cln1_col26 = dt0.Rows[0]["cln1_col26"].ToString(); //透析器型号
                cln1_col27 = dt0.Rows[0]["cln1_col27"].ToString(); //血管通路类型
                cln1_col34 = dt0.Rows[0]["cln1_col34"].ToString(); //血管通路
                cln1_col4 = dt0.Rows[0]["cln1_col4"].ToString();   //血管通路2
                cb_WeightAfter.Text = dt0.Rows[0]["cln1_col8"].ToString(); //前次透析後體重
                cln1_col14 = dt0.Rows[0]["cln1_col14"].ToString();
                cln1_col17 = dt0.Rows[0]["cln1_col17"].ToString();
                cln1_col23 = dt0.Rows[0]["cln1_col23"].ToString();
                cln1_col28 = dt0.Rows[0]["cln1_col28"].ToString(); //透析液
                cln1_col29 = dt0.Rows[0]["cln1_col29"].ToString();
                cln1_col30 = dt0.Rows[0]["cln1_col30"].ToString();
                cln1_col31 = dt0.Rows[0]["cln1_col31"].ToString();
                Henan1 = dt0.Rows[0]["Henan1"].ToString();
                Henan2 = dt0.Rows[0]["Henan2"].ToString();
                Henan3 = dt0.Rows[0]["Henan3"].ToString();
                Henan4 = dt0.Rows[0]["Henan4"].ToString();
            };
            dt0.Dispose();

            // page1: 治療參數, page2: 血液淨化 
            cb_info_date.Text = toDay; //今天日期

            DataTable dt = new DataTable();
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate='" + cb_info_date.Text + "' AND cln1_col15 IS NOT NULL";
            dt = db.Query(sql);
            if (dt.Rows.Count == 0)
            {
                DataTable dt1 = new DataTable();
                sql = "SELECT pif_hpack, pif_hpack2, pif_hpack3, pif_hpack4, pif_hpack5 FROM pat_info ";
                sql += "WHERE pif_ic='" + Text_Patient_ID.Text + "' ";
                dt1 = db.Query(sql);
                if (dt1.Rows.Count > 0)
                {
                    pck_name = dt1.Rows[0]["pif_hpack"].ToString();
                    hp2_name = dt1.Rows[0]["pif_hpack2"].ToString();
                    hp3_name = dt1.Rows[0]["pif_hpack3"].ToString();
                    hp4_name = dt1.Rows[0]["pif_hpack4"].ToString();
                    hp5_name = dt1.Rows[0]["pif_hpack5"].ToString();
                }
                dt1.Dispose();

                Common.SetComboBoxValue(cbo_diagnosis, cln1_col1, false); //诊断

                if (cln1_col34 == "") cln1_col34 = pck_name;
                Common.SetComboBoxValue(cbo_h_type, cln1_col34, false); //血管通路类型 

                if (cln1_col26 == "") cln1_col26 = hp2_name;
                Common.SetComboBoxValue(cbo_machine_model, cln1_col26, false); //透析器型号

                if (cln1_col4 == "") cln1_col4 = hp4_name;
                Common.SetComboBoxValue(cbo_machine_model2, cln1_col4, false); //透析器型号2

                if (cln1_col27 == "") cln1_col27 = hp3_name;
                Common.SetComboBoxValue(cbo_hpack3, cln1_col27, false); //管路型號
                if (cln1_col47 == "") cln1_col47 = hp5_name;
                Common.SetComboBoxValue(cbo_hpack5, cln1_col47, false); //管路型號二

                TextField8.Text = cln1_col13;

                Common.SetComboBoxValue(SelectBox10, cln1_col15, false); //肝素

                TextAdd.Text = cln1_col14;
                TextAmount.Text = cln1_col17;

                TextField6.Text = cln1_col28;
                TextField7.Text = cln1_col29;
                TextField9.Text = cln1_col30;
                TextField10.Text = cln1_col31;

                TextField1.Text = Henan1;
                TextField2.Text = Henan2;
                TextField4.Text = Henan3;
                TextField5.Text = Henan4;
                TextArea1.Text = cln1_col23;
                cbo_Col24.Select(0);

                //套用治療參數模板
                sql = "SELECT * FROM clinical1_doc_henan ";
                sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate='" + cb_info_date.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count == 0)
                {
                    cb_info_date.RemoveCls("Text-blue"); //未儲存顯示紅色日期
                    cb_info_date.AddCls("Text-red");

                    sql = "SELECT * FROM clinical1_doc_henan ";
                    sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate='base' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count == 0)
                    {
                        dt.Dispose();
                        CheckcboValue();
                        db.myConnection.Close();
                        return;
                    }
                }

                Common.SetComboBoxValue(cbo_machine_model, dt.Rows[0]["cln1_col2"].ToString(), false); //透析器型号
                Common.SetComboBoxValue(cbo_machine_model2, dt.Rows[0]["cln1_col14"].ToString(), false); //透析器型号2
                Common.SetComboBoxValue(cbo_hpack3, dt.Rows[0]["cln1_col15"].ToString(), false); //管路型号
                Common.SetComboBoxValue(cbo_hpack5, dt.Rows[0]["cln1_col16"].ToString(), false); //管路型号2

                Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col1"].ToString(), false); //血管通路類型
                Common.SetComboBoxValue(cbo_Machinetype, dt.Rows[0]["cln1_col3"].ToString(), false); //透析方式
                txt_weight_after_expect.Text = dt.Rows[0]["cln1_col4"].ToString(); //干体重
                Common.SetComboBoxValue(SelectBox10, dt.Rows[0]["cln1_col5"].ToString(), false); //抗凝药物
                txt_TargetWeight.Text = dt.Rows[0]["cln1_col6"].ToString();

                sql = "SELECT cln1_diadate AS CODE, ";
                sql += "CONCAT(cln1_diadate,' | ', cln1_col7) AS NAME3 ";
                sql += "FROM clinical1_nurse ";
                sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate<>'" + toDay + "' ";
                sql += "ORDER BY CODE DESC LIMIT 5";
                dt1 = db.Query(sql);
                if (dt1.Rows.Count == 0)
                {
                    Ext.Net.ListItem litem3 = new Ext.Net.ListItem(txt_TargetWeight.Text, txt_TargetWeight.Text); //目标定容量
                    cb_TargetWeight.Items.Add(litem3);
                    cb_TargetWeight.GetStore().DataBind();
                }
                else
                {
                    Ext.Net.ListItem litem3 = new Ext.Net.ListItem(txt_TargetWeight.Text, txt_TargetWeight.Text); //目标定容量
                    cb_TargetWeight.Items.Add(litem3);
                    for (int i = 0; i < dt1.Rows.Count; i++)
                    {
                        litem3 = new Ext.Net.ListItem(dt1.Rows[i]["NAME3"].ToString(), dt1.Rows[i]["CODE"].ToString());
                        cb_TargetWeight.Items.Add(litem3);
                    }
                    cb_TargetWeight.GetStore().DataBind();
                }
                cb_TargetWeight.Select(0);
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
                sql = "SELECT a.*, c.cln1_col11 AS Henan1, c.cln1_col12 AS Henan2, c.cln1_col21 AS Henan3, c.cln1_col22 AS Henan4, ";
                sql += "b.cln1_col11 as col11, b.cln1_col12 as col12, ";
                sql += "b.cln1_col21 as col21, b.cln1_col22 as col22, ";
                sql += "b.cln1_col31 as col31, b.cln1_col32 as col32, ";
                sql += "b.cln1_col41 as col41, b.cln1_col42 as col42, ";
                sql += "b.cln1_col50 as col50 FROM clinical1_nurse a ";
                sql += "LEFT JOIN clinical1_nurse_suzhou b ON a.cln1_patic=b.cln1_patic AND a.cln1_diadate=b.cln1_diadate ";
                sql += "LEFT JOIN clinical1_nurse_henan c ON a.cln1_patic=c.cln1_patic AND a.cln1_diadate=c.cln1_diadate ";
                sql += "WHERE a.cln1_patic='" + Text_Patient_ID.Text + "' AND a.cln1_diadate='" + cb_info_date.Text + "'";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    txt_weight_after_expect.Text = row["cln1_col6"].ToString(); //干体重
                    try
                    {
                        decimal try_parse = Convert.ToDecimal(row["cln1_col5"].ToString());
                        txt_weight_before.Text = row["cln1_col5"].ToString(); //透析前体重
                        //Tex_weight.Text = txt_weight_before.Text;
                    }
                    catch { }

                    sql = "SELECT cln1_diadate AS CODE, ";
                    sql += "CONCAT(cln1_diadate,' | ', cln1_col7) AS NAME3 ";
                    sql += "FROM clinical1_nurse ";
                    sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate<>'" + toDay + "' ";
                    sql += "ORDER BY CODE DESC LIMIT 5";
                    DataTable dt1 = db.Query(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        txt_TargetWeight.Text = row["cln1_col7"].ToString(); //目标定容量
                        Ext.Net.ListItem litem3 = new Ext.Net.ListItem(txt_TargetWeight.Text, txt_TargetWeight.Text); //目标定容量
                        cb_TargetWeight.Items.Add(litem3);
                        for (int i = 0; i < dt1.Rows.Count; i++)
                        {
                            litem3 = new Ext.Net.ListItem(dt1.Rows[i]["NAME3"].ToString(), dt1.Rows[i]["CODE"].ToString());
                            cb_TargetWeight.Items.Add(litem3);
                        }
                    }
                    else
                    {
                        txt_TargetWeight.Text = row["cln1_col7"].ToString(); //目标定容量
                        Ext.Net.ListItem litem3 = new Ext.Net.ListItem(txt_TargetWeight.Text, txt_TargetWeight.Text); //目标定容量
                        cb_TargetWeight.Items.Add(litem3);
                    }
                    cb_TargetWeight.GetStore().DataBind();
                    cb_TargetWeight.Select(0);

                    txt_weight_after.Text = row["cln1_col8"].ToString();
                    TextField8.Text = row["cln1_col13"].ToString();
                    TextAdd.Text = row["cln1_col14"].ToString();
                    TextAmount.Text = row["cln1_col17"].ToString();

                    TextField6.Text = row["cln1_col28"].ToString();
                    TextField7.Text = row["cln1_col29"].ToString();
                    TextField9.Text = row["cln1_col30"].ToString();
                    TextField10.Text = row["cln1_col31"].ToString();

                    TextField1.Text = row["Henan1"].ToString();
                    TextField2.Text = row["Henan2"].ToString();
                    TextField4.Text = row["Henan3"].ToString();
                    TextField5.Text = row["Henan4"].ToString();

                    Common.SetComboBoxValue(cbo_diagnosis, row["cln1_col1"].ToString(), false); //诊断
                    Common.SetComboBoxValue(cbo_h_type, row["cln1_col34"].ToString(), false); //血管通路類型

                    Common.SetComboBoxValue(cbo_machine_model, row["cln1_col26"].ToString(), false); //透析器型号
                    Common.SetComboBoxValue(cbo_machine_model2, row["cln1_col4"].ToString(), false); //透析器型号2

                    Common.SetComboBoxValue(cbo_hpack3, row["cln1_col27"].ToString(), false); //管路型號
                    Common.SetComboBoxValue(cbo_hpack5, row["cln1_col47"].ToString(), false); //管路型號二

                    Common.SetComboBoxValue(SelectBox10, row["cln1_col15"].ToString(), false); //肝素
                    Common.SetComboBoxValue(cbo_Machinetype, row["cln1_col3"].ToString(), false); //透析方式
                    Common.SetComboBoxValue(SelectDialysisTime, dt.Rows[0]["cln1_col16"].ToString(), false); //透析時間
                    TextArea1.Text = row["cln1_col23"].ToString();
                    Common.SetComboBoxValue(cbo_Col24, dt.Rows[0]["cln1_col24"].ToString(), false); 

                    if (Hospital == "Hospital_Alasamo" || Hospital == "Hospital_Hebei") //血壓量測 OR 置换量
                    {
                        try
                        {
                            TextField37.Text = dt.Rows[0]["cln1_col37"].ToString();
                            TextField38.Text = dt.Rows[0]["cln1_col38"].ToString();
                            Common.SetComboBoxValue(SelectBox39, dt.Rows[0]["cln1_col39"].ToString(), false);
                        }
                        catch (Exception ex) { }
                    }
                    else
                    {
                        //TextField11.Text = dt.Rows[0]["cln1_col37"].ToString();
                        TextField11.Text = dt.Rows[0]["cln1_col19"].ToString();
                    }
                }
            }
            dt.Dispose();
            CheckcboValue();
            CheckType();
        }

        protected void Btnsave_Click(object sender, DirectEventArgs e)
        {
            docname = Session["USER_NAME"].ToString();
            string sql;
            DataTable dt;
            if (cbo_machine_model.Text.Trim() == "")
            {
                Common._ErrorMsgShow("请输入透析器型号!");
                return;
            }
            else if (cbo_h_type.Text.Trim() == "")
            {
                Common._ErrorMsgShow("请输入血管通路!");
                return;
            }
            else if (cbo_Machinetype.Text.Trim() == "")
            {
                Common._ErrorMsgShow("请输入透析方式!");
                return;
            }
            else if (txt_weight_before.Text == "")
            {
                Common._ErrorMsgShow("请输入透析前体重!");
                return;
            }

            sql = "SELECT a.cln1_diadate FROM clinical1_nurse a ";
            sql += "WHERE a.cln1_patic='" + Text_Patient_ID.Text + "' AND a.cln1_diadate='" + toDay + "' ";
            dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "UPDATE clinical1_nurse a ";
                sql += " SET a.cln1_col1='" + Common.GetComboBoxText(cbo_diagnosis) + "',";
                sql += "     a.cln1_col2='" + bedno.Text + "',";
                sql += "     a.cln1_col3='" + Common.GetComboBoxText(cbo_Machinetype) + "',";
                sql += "     a.cln1_col5='" + txt_weight_before.Text + "',";
                sql += "     a.cln1_col6='" + txt_weight_after_expect.Text + "',";
                sql += "     a.cln1_col7='" + txt_TargetWeight.Text + "',"; //目标定容量
                sql += "     a.cln1_col8='" + txt_weight_after.Text + "',";
                sql += "     a.cln1_col9='" + cb_info_date.Text + "',";
                sql += "     a.cln1_col13='" + TextField8.Text + "',";
                sql += "     a.cln1_col14='" + TextAdd.Text + "',";
                sql += "     a.cln1_col15='" + Common.GetComboBoxText(SelectBox10) + "',"; //肝素
                sql += "     a.cln1_col16='" + Common.GetComboBoxText(SelectDialysisTime) + "',"; //透析時間
                sql += "     a.cln1_col17='" + TextAmount.Text + "',";
                sql += "     a.cln1_col23='" + TextArea1.Text + "',";
                sql += "     a.cln1_col24='" + Common.GetComboBoxText(cbo_Col24) + "',";
                sql += "     a.cln1_col26='" + Common.GetComboBoxText(cbo_machine_model) + "', "; //透析器型号
                sql += "     a.cln1_col4='" + Common.GetComboBoxText(cbo_machine_model2) + "',"; //透析器型号2
                sql += "     a.cln1_col27='" + Common.GetComboBoxText(cbo_hpack3) + "', "; //管路型號
                sql += "     a.cln1_col47='" + Common.GetComboBoxText(cbo_hpack5) + "', "; //管路型號2
                sql += "     a.cln1_col28='" + TextField6.Text + "', ";
                sql += "     a.cln1_col29='" + TextField7.Text + "', ";
                sql += "     a.cln1_col30='" + TextField9.Text + "', ";
                sql += "     a.cln1_col31='" + TextField10.Text + "', ";
                sql += "     a.cln1_col33='" + docname + "', ";
                sql += "     a.cln1_col34='" + Common.GetComboBoxText(cbo_h_type) + "',";
                if (Hospital == "Hospital_Alasamo" || Hospital == "Hospital_Hebei")
                {
                    sql += "a.cln1_col37='" + TextField37.Text + "', ";
                    sql += "a.cln1_col38='" + TextField38.Text + "', ";
                    sql += "a.cln1_col39='" + Common.GetComboBoxText(SelectBox39) + "',";
                }
                else
                {
                    sql += "a.cln1_col19='" + TextField11.Text + "', ";
                    sql += "a.cln1_col37='" + TextField11.Text + "', ";
                }
                sql += "a.cln1_col40='" + TextArea2.Text + "', ";
                sql += "     a.cln1_user='" + docname + "',";
                sql += "     a.cln1_dateadded='' ";
                sql += "WHERE a.cln1_patic='" + Text_Patient_ID.Text + "' ";
                sql += "  AND a.cln1_diadate='" + toDay + "' ";
            }
            else
            {
                sql = "INSERT into clinical1_nurse(cln1_patic,cln1_diadate,";
                sql += "cln1_col1,cln1_col2,cln1_col3,cln1_col4,"; //cln1_col4=透析器型号2
                sql += "cln1_col5,cln1_col6,cln1_col7,cln1_col8,cln1_col9,cln1_col10,";
                sql += "cln1_col11,cln1_col12,cln1_col13,cln1_col14,cln1_col15,";
                sql += "cln1_col16,cln1_col17,cln1_col18,cln1_col19,cln1_col20,";
                sql += "cln1_col21,cln1_col22,cln1_col23,cln1_col24,cln1_col25,";
                sql += "cln1_col26,cln1_col27,cln1_col47,"; //cln1_col47=管路型號2
                sql += "cln1_col28,cln1_col29,cln1_col30,cln1_col31,";
                sql += "cln1_col33, cln1_col34,";//cln1_col34=血管通路类型1
                if (Hospital == "Hospital_Alasamo" || Hospital == "Hospital_Hebei")
                {
                    sql += "cln1_col37,cln1_col38,cln1_col39,";
                }
                sql += "cln1_col40, cln1_user,cln1_dateadded) ";
                sql += "VALUES('" + Text_Patient_ID.Text + "','" + toDay + "','";
                sql += Common.GetComboBoxText(cbo_diagnosis) + "','" + bedno.Text + "','";
                sql += Common.GetComboBoxText(cbo_Machinetype) + "','";
                sql += Common.GetComboBoxText(cbo_machine_model2) + "','"; //透析器型号2
                sql += txt_weight_before.Text + "','";
                sql += txt_weight_after_expect.Text + "','" + txt_TargetWeight.Text + "','" + txt_weight_after.Text + "','" + cb_info_date.Text + "','','','','";
                sql += TextField8.Text + "','" + TextAdd.Text + "','" + Common.GetComboBoxText(SelectBox10) + "','";
                sql += Common.GetComboBoxText(SelectDialysisTime) + "','" + TextAmount.Text + "','" + TextField11.Text + "','','',";
                sql += "'','','" + TextArea1.Text + "','" + Common.GetComboBoxText(cbo_Col24) + "','',";
                sql += "'" + Common.GetComboBoxText(cbo_machine_model) + "',";
                sql += "'" + Common.GetComboBoxText(cbo_hpack3) + "',"; //管路型號
                sql += "'" + Common.GetComboBoxText(cbo_hpack5) + "',"; //管路型號2
                sql += "'" + TextField6.Text + "','" + TextField7.Text + "','" + TextField9.Text + "','" + TextField10.Text + "',"; //透析液: 钾+ 钙 + 碳酸氢根 + 钠
                sql += "'" + docname + "', ";
                sql += "'" + Common.GetComboBoxText(cbo_h_type) + "', "; //血管通路类型
                if (Hospital == "Hospital_Alasamo" || Hospital == "Hospital_Hebei")
                {
                    sql += "'" + TextField37.Text + "','" + TextField38.Text + "','" + Common.GetComboBoxText(SelectBox39) + "', ";
                }
                sql += "'" + TextArea2.Text + "','" + docname + "','')";
            }
            db.Excute(sql);

            //更新clinical1_nurse_henan
            sql = "SELECT cln1_diadate FROM clinical1_nurse_henan ";
            sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate='" + toDay + "' ";
            dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "UPDATE clinical1_nurse_henan ";
                sql += " SET cln1_col11='" + TextField1.Text + "',";
                sql += "     cln1_col12='" + TextField2.Text + "',";
                sql += "     cln1_col21='" + TextField4.Text + "',";
                sql += "     cln1_col22='" + TextField5.Text + "' ";
                sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' ";
                sql += "  AND cln1_diadate='" + toDay + "' ";
            }
            else
            {
                sql = "INSERT INTO clinical1_nurse_henan(cln1_patic,cln1_diadate,";
                sql += "cln1_col11,cln1_col12,cln1_col21,cln1_col22) ";
                sql += "VALUES('" + Text_Patient_ID.Text + "','" + toDay + "','";
                sql += TextField1.Text + "','" + TextField2.Text + "','" + TextField4.Text + "','" + TextField5.Text + "')";
            }
            db.Excute(sql);

            //更新pat_info
            sql = "UPDATE pat_info " +
                    "SET pif_hpack='" + Common.GetComboBoxValue(cbo_h_type) + "', " +
                        "pif_hpack2='" + Common.GetComboBoxValue(cbo_machine_model) + "', " +
                        "pif_hpack3='" + Common.GetComboBoxValue(cbo_hpack3) + "', " +
                        "pif_hpack4='" + Common.GetComboBoxValue(cbo_machine_model2) + "', " +
                        "pif_hpack5='" + Common.GetComboBoxValue(cbo_hpack5) + "' " +
                    "WHERE pif_ic='" + Text_Patient_ID.Text + "' ";
            db.Excute(sql);

            //更新pat_visit
            sql = "UPDATE pat_visit " +
                        "SET pv_weight='" + txt_weight_before.Text + "', " +
                            "pv_hpack='" + Common.GetComboBoxValue(cbo_h_type) + "', " +
                            "pv_hpack2='" + Common.GetComboBoxValue(cbo_machine_model) + "', " +
                            "pv_hpack3='" + Common.GetComboBoxValue(cbo_hpack3) + "', " +
                            "pv_hpack4='" + Common.GetComboBoxValue(cbo_machine_model2) + "', " +
                            "pv_hpack5='" + Common.GetComboBoxValue(cbo_hpack5) + "' " +
                    "WHERE pv_ic='" + Text_Patient_ID.Text + "' AND pv_datevisit='" + cb_info_date.Text + "' ";
            db.Excute(sql);

            sql = "SELECT * FROM clinical3_nurse ";
            sql += "WHERE cln3_patic='" + Text_Patient_ID.Text + "' AND cln3_date='" + toDay + "' ";
            dt = db.Query(sql);
            if (dt.Rows.Count == 0)
            {
                sql = "INSERT INTO clinical3_nurse(cln3_patic,cln3_date,cln3_rmk2) ";
                sql += "VALUES('" + Text_Patient_ID.Text + "','" + toDay + "','本次透析正常结束，无异常状况，患者透析过程无特殊，安全下机。')";
            }
            db.Excute(sql);

            //更新處方模版
            sql = "SELECT * FROM clinical1_doc_henan ";
            sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate>'" + toDay + "' ";
            sql += "ORDER BY cln1_diadate ";
            dt = db.Query(sql);
            string cln1_id = "";
            if (dt.Rows.Count > 0)
            {
                cln1_id = dt.Rows[0]["cln1_id"].ToString();
                sql = "UPDATE clinical1_doc_henan SET ";
                sql += "cln1_col1='" + Common.GetComboBoxText(cbo_h_type) + "',"; //血管通路类型
                sql += "cln1_col2='" + Common.GetComboBoxText(cbo_machine_model) + "',"; //透析器型号
                sql += "cln1_col3='" + Common.GetComboBoxText(cbo_Machinetype) + "',"; //透析方式
                sql += "cln1_col4='" + txt_weight_after_expect.Text + "',"; //干体重
                sql += "cln1_col5='" + Common.GetComboBoxText(SelectBox10) + "',"; //抗凝药物
                sql += "cln1_col6='" + cb_TargetWeight.Text + "',"; //目标定容量
                sql += "cln1_col7='" + TextField8.Text + "',"; //首次剂量
                sql += "cln1_col8='" + TextAdd.Text + "',"; //追加量
                sql += "cln1_col9='" + TextAmount.Text + "' "; //总量
                sql += "WHERE cln1_id=" + cln1_id;
                db.Excute(sql);
            }

            CheckcboValue();
            Common._NotificationShow("<font size=4>储存成功!</font>");
        }

        protected void Show_Patient()
        {
            string sSQL = "";
            if (Text_Patient_ID.Text != "")
            {
                sSQL = "SELECT A.*, P.pif_id, P.pif_basetimes, P.pif_name, V.pv_weight, V.pv_macstat, P.pif_sex, " + DateTime.Now.Year.ToString() + "-year(P.pif_dob) AS Age ";
                sSQL += "FROM pat_info P ";
                sSQL += "LEFT JOIN appointment_change A ON A.ah_patic=P.pif_ic ";
                sSQL += "LEFT JOIN pat_visit V ON V.pv_ic=P.pif_ic AND V.pv_datevisit='" + toDay + "' ";
                sSQL += "WHERE P.pif_ic='" + Text_Patient_ID.Text + "'";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count > 0)
                {
                    pid.Text = dt.Rows[0]["pif_id"].ToString();
                    TextBaseTimes.Text = dt.Rows[0]["pif_basetimes"].ToString();
                    Text_Patient_Name.Text = dt.Rows[0]["pif_name"].ToString();
                    Text_Patient_Age.Text = dt.Rows[0]["Age"].ToString();
                    if (dt.Rows[0]["pif_sex"].ToString() == "F")
                    {
                        Text_Patient_Gender.Text = "女";
                    }
                    else if (dt.Rows[0]["pif_sex"].ToString() == "M")
                    {
                        Text_Patient_Gender.Text = "男";
                    }
                }
            }
        }

        private string get_vascular_access(out string cln1_diadate)
        {
            string sql = "SELECT cln1_col34 as vascular_access, cln1_diadate " +
                            "FROM clinical1_nurse " +
                           "WHERE cln1_patic='" + Text_Patient_ID.Text + "' " +
                           "ORDER BY cln1_diadate DESC " +
                           "LIMIT 0,1 ";
            //string result;
            DataTable dt_h_type = db.Query(sql);
            if (dt_h_type.Rows.Count > 0)
            {
                cln1_diadate = dt_h_type.Rows[0]["cln1_diadate"].ToString();
                return dt_h_type.Rows[0]["vascular_access"].ToString();
            }
            cln1_diadate = null;
          //  Common._ErrorMsgShow("數據庫裡找不到血管通路");
          //  Common.SaveERR("debug", sql + "數據庫裡找不到血管通路");
            return null;
        }

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

        protected void Show_drug()
        {
            string sql;
            DataTable dt = new DataTable();
            //sql = "SELECT a.lgord_id, a.lgord_dateord, a.lgord_timeord, a.lgord_usr1, b.drg_name, ";
            //sql += "a.lgord_intake, a.lgord_freq, a.lgord_medway, a.lgord_comment, a.lgord_nurs, a.lgord_dtactst, a.lgord_patic, a.lgord_drug ";
            //sql += "FROM longterm_ordermgt a ";
            //sql += "LEFT JOIN drug_list b ON a.lgord_drug = b.drg_code ";
            //sql += "WHERE a.lgord_patic = '" + Text_Patient_ID.Text + "' AND a.lgord_actst = '00001'";//只取使用中藥物
            //DataTable dt = db.Query(sql);
            //Store istore = Grid_Long_Term.GetStore();
            //istore.DataSource = db.GetDataArray(dt);
            //istore.DataBind();

            //sql = "SELECT a.shord_id,a.shord_dateord,a.shord_timeord,a.shord_usr1, b.drg_name, ";
            //sql += "a.shord_intake, a.shord_freq, a.shord_medway, a.shord_comment, a.shord_nurs, a.shord_dtactst ";
            //sql += "FROM shortterm_ordermgt a ";
            //sql += "LEFT JOIN drug_list b ON a.shord_drug = b.drg_code ";
            //sql += "WHERE a.shord_patic = '" + Text_Patient_ID.Text + "' AND a.shord_dateord = '"+ toDay + "' AND a.shord_actst = '00001'";//只取使用中藥物
            //dt = db.Query(sql);
            //Store istore2 = Grid_Short_Term.GetStore();
            //istore2.DataSource = db.GetDataArray(dt);
            //istore2.DataBind();
        }
        
        protected void Show_image()
        {
            string ipath = ConfigurationManager.AppSettings["pat_images"].ToString(); 
            string iimage = "";
            string sql = "SELECT a.pif_sex, a.pif_imgloc  FROM pat_info a ";
            sql += "WHERE a.pif_ic = '" + Text_Patient_ID.Text + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count != 0)
            {
                iimage = dt.Rows[0]["pif_imgloc"].ToString().Replace("./", "");
                if (iimage == "")
                {
                    if (dt.Rows[0]["pif_sex"].ToString() == "M" || dt.Rows[0]["pif_sex"].ToString() == "")
                    {
                        iimage = "images/male.png";
                    }
                    else
                    {
                        iimage = "images/female.png";
                    }
                }
            }
            else
            {
                Image1.ImageUrl = "images/male.png";
            }
            Image1.ImageUrl = iimage;
        }

        private bool IsNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^\d+(\.)?\d*$");
            return r.IsMatch(strNumber);
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

        public void Update_clinical1_nurse(string person_id, string floor_no, string bed_no)
        {
            try
            {
                string sql = "SELECT * FROM clinical1_nurse WHERE cln1_patic='" + person_id + "' AND cln1_diadate='" + toDay + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count == 0)
                    return;
                else
                    pat.update.set_dialysis_time(person_id, floor_no, bed_no, toDay); //20160105 Alex 處理儲存透析結束時間
            }
            catch(Exception ex)
            {
                Common.SaveERR("Update_clinical1_nurse", ex.Message);
            }
        }

        protected void btnClose_Click(object sender, DirectEventArgs e)
        {
            w_LOGIN.Close();
        }

        protected void btnShClose_Click(object sender, DirectEventArgs e)
        {
            w_PHARLOGIN.Close();
        }        

        protected void btnDecrypt_Click(object sender, DirectEventArgs e)
        {
            if (w_txtUSER.Text == "")
            {
                Common._NotificationShow("请输入正确工号!");
                return;
            }
            else
            {
                string sql = "SELECT a.acclv_fname,a.acclv_funm ";
                sql += " FROM access_level a ";
                sql += "WHERE a.acclv_stfcode = '" + w_txtUSER.Text + "' ";

                DataTable dt = db.Query(sql);
                if (dt.Rows.Count == 1)
                {
                    sql = "UPDATE longterm_ordermgt SET ";
                    sql += "lgord_nurs='" + dt.Rows[0]["acclv_fname"] + "',";
                    sql += "lgord_timest='" + DateTime.Now.ToString("HH:mm:ss") + "' ";
                    sql += "WHERE lgord_id=" + DRUG_ROWID.Text + " ";
                    db.Excute(sql);
                    Show_drug();
                    w_txtUSER.Text = "";
                    w_LOGIN.Close();
                }
                else
                {
                    w_txtUSER.Text = "";
                    Common._NotificationShow("工号不存在，请重新输入!");
                }
            }
        }

        protected void Btn_ordershortdrug_Click(object sender, DirectEventArgs e)
        {
            w_PHARLOGIN.Show();
            txt_stfcode.Focus(false, 100);
        }

        #region 取得帳號權限(呼叫PHP取得USER的權限)
        protected void btnpharlogin_Click(object sender, DirectEventArgs e)
        {
            if (txt_stfcode.Text == "" || w_txtPASS.Text == "")
            {
                Common._NotificationShow("请输入正确工号及密码!");
                return;
            }
            else
            {
                string sSQL = "SELECT passwd, type, name FROM access_level WHERE usrnm='" + txt_stfcode.Text + "' AND active='A'";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count > 0)
                {
                    JiaMiJieMi aeskey = new JiaMiJieMi();
                    string pwd = aeskey.AES_Decrypt(aeskey.Base64Decrypt(dt.Rows[0]["passwd"].ToString()));
                    string usertype = dt.Rows[0]["type"].ToString();
                    string docname = dt.Rows[0]["name"].ToString();
                    if (pwd == w_txtPASS.Text)
                    {
                        if (usertype == "DC" || usertype == "DH")
                        {
                            string urlstr = "i_pad_ordershortdrug.aspx?person_id=" + Text_Patient_ID.Text;
                            urlstr += "&patient_name=" + Text_Patient_Name.Text + "&pat_sex=" + Text_Patient_Gender.Text + "&pat_docname=" + docname;
                            urlstr += "&floor=" + floor.Text + "&bedno=" + bedno.Text + "&area=" + area.Text + "&time=" + time.Text + "&dayTyp=" + daytyp.Text;
                            X.Redirect(urlstr);
                        }
                        else
                        {
                            Common._NotificationShow("只有醫師可開立醫囑，请重新输入!");
                            txt_stfcode.Text = "";
                            w_txtPASS.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        Common._NotificationShow("密码错误，请重新输入!");
                        w_txtPASS.Text = "";
                        return;
                    }
                }
                else
                {
                    Common._NotificationShow("工号不存在，请重新输入!");
                    txt_stfcode.Text = "";
                    w_txtPASS.Text = "";
                    return;
                }
            }
        }
        #endregion
        
        #region 上傳照片
        protected void GetPatImg(object sender, DirectEventArgs e)
        {
            if (this.UploadImage.HasFile)
            {
                string strImg = string.Empty;
                if (UploadImage.PostedFile != null && UploadImage.PostedFile.ContentLength > 0)
                {
                    strImg = this.UploadImage.PostedFile.FileName;
                    string strExt = System.IO.Path.GetExtension(strImg).ToLower();
                    if (strExt != ".jpg" && strExt != ".jepg" && strExt != ".bmp" && strExt != ".gif")
                    {
                        Common._NotificationShow("抱歉仅支持.jpg，jepg，bmp，gif");
                        return;
                    }
                }
                fileUpload(strImg);
            }
            else
            {
                Common._NotificationShow("请选择照片");
            }
        }
        #endregion

        #region 上传图片处理
        public void fileUpload(string strFilePath)
        {
            string strPictureName = string.Empty;//上传后的图片名，以当前时间为文件名，确保文件名没有重复           
            if (!string.IsNullOrEmpty(strFilePath))
            {
                string strMapPath = "";
                try
                {
                    string strFileName = System.IO.Path.GetFileName(strFilePath);// System.IO.Path.GetExtension(strFilePath).ToLower();
                    strPictureName = DateTime.Now.Ticks.ToString() + strFileName;
                    try
                    {
                        strMapPath = Server.MapPath("./") + "patimages\\";
                    }
                    catch (Exception)
                    {
                        strMapPath = "D:\\Temp\\patimages\\";
                    }
                    Common._NotificationShow("照片上传中");
                    string strPath = strMapPath + strPictureName;
                    UploadImage.PostedFile.SaveAs(strPath);
                    Image1.ImageUrl = "patimages/"+ strPictureName;
                    string sql = "UPDATE pat_info SET pif_imgloc='patimages/" + strPictureName + "' "; //20160510 Alex
                    sql += "WHERE pif_ic='" + Text_Patient_ID.Text + "'";
                    db.Excute(sql);
                    db.Close();
                }
                catch (Exception)
                {
                    Common._ErrorMsgShow("上传照片失败");
                }
            }
            else
            {
                string path = Directory.GetCurrentDirectory();// 用于获得应用程序当前工作目录
                Common._NotificationShow("请选择照片");
            }
        }
        #endregion
        
        protected void LongNurse_Click(object sender, DirectEventArgs e)
        {
            Show_drug();
        }

        protected void Nurse_Click(object sender, DirectEventArgs e)
        {
            drugkind.Text = e.ExtraParams["drugkind"];
            drugid.Text = e.ExtraParams["DRUG_ROWID"];
            if (TextExeTime.Text == "")
                TextExeTime.Text = DateTime.Now.ToString("hh:mm");
            else
                TextExeTime.Text = e.ExtraParams["DRUG_TIME"];
            WindowTime.Title = "用药时间";
            this.WindowTime.Show();
            TextExeTime.Focus(false, 100);
        }

        #region 儲存护士执行时间
        protected void btnTime_Click(object sender, DirectEventArgs e)
        {
            string sql = "";
            if (drugkind.Text == "L")
            {
                sql = "UPDATE longterm_ordermgt SET ";
                sql += "lgord_dtactst='" + TextExeTime.Text + "' ";
                sql += "WHERE lgord_id=" + drugid.Text + " ";
            }
            else if (drugkind.Text == "S")
            {
                sql = "UPDATE shortterm_ordermgt SET ";
                sql += "shord_dtactst='" + TextExeTime.Text + "' ";
                sql += "WHERE shord_id=" + drugid.Text + " ";
            }
            db.Excute(sql);
            this.WindowTime.Close();
            Show_drug();
        }
        #endregion

        protected void btnTimeClose_Click(object sender, DirectEventArgs e)
        {
            WindowTime.Close();
        }

        protected void TextTimes_Click(object sender, DirectEventArgs e)
        {
            BaseTimes.Show();
            TextBaseTimes.Focus(false, 100);
        }

        protected void btnTClose_Click(object sender, DirectEventArgs e)
        {
            BaseTimes.Hide();
        }

        #region 儲存透析基本次數
        protected void btnT_Click(object sender, DirectEventArgs e)
        {
            if (TextBaseTimes.Text != "")
            {
                db.Excute("UPDATE pat_info SET pif_basetimes=" + TextBaseTimes.Text + " WHERE pif_ic='" + Text_Patient_ID.Text + "' ");
                Common._NotificationShow("儲存透析基本次數<br>完成");
                TextTimes.Text = (int.Parse(TextBaseTimes.Text) + int.Parse(DialysisTimes.Text)).ToString();
                BaseTimes.Hide();
            }
        }
        #endregion
                
        protected void ChangeW(object sender, DirectEventArgs e)
        {
            try
            {
                txt_TargetWeight.Text = cb_TargetWeight.Text;
                if (cb_TargetWeight.SelectedItem.Index > 0)
                {
                    cb_TargetWeight.Select(0);
                }
            }
            catch { }
        }

        protected void BtnMemoAdd_Click(object sender, DirectEventArgs e)
        {
            string sql = "";
            sql = "SELECT cln1_col40 FROM clinical1_nurse ";
            sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate='" + txt_orddate.Text + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                TextArea2.Text = dt.Rows[0]["cln1_col40"].ToString();
            }
            dt.Dispose();
            Window2.Title = "医嘱";
            Window2.Show();
            TextArea2.Focus(false, 100);
        }

        protected void BtnReloadData_Click(object sender, DirectEventArgs e)
        {
            X.Msg.Confirm("提示", "您确定要重读治疗计画么？", new JFunction("ReloadMed.Activate(result);", "result")).Show();
        }

        [DirectMethod(Namespace = "ReloadMed")]
        public void Activate(string result)
        {
            if (result == "yes")
            {
                string sql;

                //套用治療參數模板
                sql = "SELECT * FROM clinical1_doc_henan ";
                sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate='" + cb_info_date.Text + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    //cb_TargetWeight.Text = dt.Rows[0]["cln1_col6"].ToString(); //目标定容量
                    TextField8.Text = dt.Rows[0]["cln1_col7"].ToString(); //首次剂量
                    TextAdd.Text = dt.Rows[0]["cln1_col8"].ToString(); //追加量
                    TextAmount.Text = dt.Rows[0]["cln1_col9"].ToString(); //总量
                    TextField6.Text = dt.Rows[0]["cln1_col10"].ToString();
                    TextField7.Text = dt.Rows[0]["cln1_col11"].ToString();
                    TextField9.Text = dt.Rows[0]["cln1_col12"].ToString();
                    TextField10.Text = dt.Rows[0]["cln1_col13"].ToString();
                }
                else //基礎模板
                {
                    sql = "SELECT * FROM clinical1_doc_henan ";
                    sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' AND cln1_diadate='base' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        //cb_TargetWeight.Text = dt.Rows[0]["cln1_col6"].ToString(); //目标定容量
                        TextField8.Text = dt.Rows[0]["cln1_col7"].ToString(); //首次剂量
                        TextAdd.Text = dt.Rows[0]["cln1_col8"].ToString(); //追加量
                        TextAmount.Text = dt.Rows[0]["cln1_col9"].ToString(); //总量
                        TextField6.Text = dt.Rows[0]["cln1_col10"].ToString();
                        TextField7.Text = dt.Rows[0]["cln1_col11"].ToString();
                        TextField9.Text = dt.Rows[0]["cln1_col12"].ToString();
                        TextField10.Text = dt.Rows[0]["cln1_col13"].ToString();
                    }
                }
                dt.Dispose();
                X.Msg.Notify("提示", "重新读取治疗计画完成").Show();
            }
        }

        protected void BtnBloodAdd_Click(object sender, DirectEventArgs e)
        {
            if (Hospital == "Hospital_Alasamo")
            {
                Window4.Title = "血压量测";
            }
            else if (Hospital == "Hospital_Hebei")
            {
                Window4.Title = "置换量";
            }
            Window4.Show();
        }

        protected void BtnSaveB_Click(object sender, DirectEventArgs e)
        {
            Btnsave_Click(sender, e); //會先判斷有無報到，再儲存報到明細
            Window4.Hide();
        }

        protected void BtnCancelB_Click(object sender, DirectEventArgs e)
        {
            Window4.Hide();
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
                TextField11.Disabled = false;
            }
            else
            {
                cbo_machine_model2.ReadOnly = true;
                cbo_machine_model2.Select(0);
            }

            if (cbo_Machinetype.Text == "HDF" || cbo_Machinetype.Text == "HDF+HP")
            {
                TextField11.Disabled = false;
            }
            else
            {
                TextField11.Disabled = true;
            }
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

            if (cbo_machine_model.Text.Trim() == "")
            {
                cbo_machine_model.LabelCls = "blink";
            }
            else
            {
                cbo_machine_model.LabelCls = "my-Field";
            }
        }

        protected void BtnSaveW_Click(object sender, DirectEventArgs e)
        {
            ShowDialysis(); //再重新抓一次資料
            Btnsave_Click(sender, e); //會先判斷有無報到，再儲存報到明細
            Window2.Hide();
        }

        protected void BtnCancel2_Click(object sender, DirectEventArgs e)
        {
            Window2.Hide();
        }

        protected void BtnBack_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("ipad_index.aspx");
        }

        protected void tab1_activate(object sender, DirectEventArgs e)
        {
            string url = "ipad/ipad_PreSetLong.aspx?pid=" + Text_Patient_ID.Text;
            Panel_Long.Loader.SuspendScripting();
            Panel_Long.Loader.Url = url;
            Panel_Long.Loader.DisableCaching = true;
            Panel_Long.LoadContent();
        }

        protected void tab2_activate(object sender, DirectEventArgs e)
        {
            string url = "ipad/ipad_PreSetShort.aspx?pid=" + Text_Patient_ID.Text;
            Panel_Short.Loader.SuspendScripting();
            Panel_Short.Loader.Url = url;
            Panel_Short.Loader.DisableCaching = true;
            Panel_Short.LoadContent();
        }

        private string GetHCode(string name)
        {
            string sSQL = "SELECT pck_code FROM package_setup WHERE pck_name='" + name + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["pck_code"].ToString();
            else
                return "";
        }

        protected string Get_ShortDrug()
        {
            string sql;
            string drg_code = "''";
            sql = "SELECT drg_code FROM shortterm_ord ";
            sql += "WHERE pck_code IN (SELECT pck_code FROM package_setup WHERE pck_name IN ('" + hpack.Text + "','" + cbo_h_type.Text + "'))";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                foreach (DataRow row in dt.Rows)
                {
                    drg_code += ",'" + row["drg_code"].ToString() + "'";
                }
            }
            return drg_code;
        }

        public void FirstInsert()
        {
            docname = Session["USER_NAME"].ToString();
            string sql = "";
            sql += "SELECT shord_dateord FROM shortterm_ordermgt ";
            sql += "WHERE shord_patic='" + Text_Patient_ID.Text + "' AND shord_dateord='" + toDay + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count == 0)
            {
                string shord_drug = Get_ShortDrug();
                sql = "DELETE FROM shortterm_ordermgt ";
                sql += "WHERE shord_patic='" + Text_Patient_ID.Text + "' AND shord_dateord='" + toDay + "' ";
                sql += "AND shord_drug IN (" + shord_drug + ")";
                db.Excute(sql);
                string pck_code = GetHCode(cbo_h_type.Text);
                sql = "SELECT * FROM shortterm_ord ";
                sql += "WHERE pck_code='" + pck_code + "' AND status='Y'";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    string time = DateTime.Now.ToString("HH:mm");
                    string drg_code, drg_intake, drg_freq, drg_medway;
                    sql = "";
                    foreach (DataRow row in dt.Rows)
                    {
                        drg_code = row["drg_code"].ToString();
                        drg_intake = row["intake"].ToString();
                        drg_freq = row["freq"].ToString();
                        drg_medway = row["medway"].ToString();
                        sql += "INSERT shortterm_ordermgt(shord_patic, shord_dateord, shord_timeord, shord_usr1, shord_usr2, ";
                        sql += "shord_drug, shord_intake, shord_freq, shord_medway, shord_actst) ";
                        sql += "VALUES('" + Text_Patient_ID.Text + "',";
                        sql += "'" + toDay + "',";
                        sql += "'" + time + "',";
                        sql += "'" + docname + "',";
                        sql += "'" + docname + "',";
                        sql += "'" + drg_code + "',";
                        sql += "'" + drg_intake + "',";
                        sql += "'" + drg_freq + "',";
                        sql += "'" + drg_medway + "',";
                        sql += "'00001'); ";
                    }
                    db.Excute(sql);
                }
            }
        }
    }
}