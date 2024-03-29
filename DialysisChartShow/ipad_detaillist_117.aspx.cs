﻿using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using System.Net;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show
{
    public partial class ipad_detaillist_117 : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        string cln3_time, cln3_rmk, cln3_rmk2, cln3_a1, cln3_a2, cln3_b1, cln3_b2, cln3_ysa, cln3_pressure, cln1_col4;
        string hName = "117";
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        string cln1_col1, cln1_col3, cln1_col6, cln1_col13, cln1_col14, cln1_col15, cln1_col17, cln1_col18, cln1_col19;
        string cln1_col26, cln1_col27, cln1_col34;
        string info_date;

        protected void Page_Load(object sender, EventArgs e)
        {
            //TextField1.Hidden = true;            
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request.QueryString["patient_id"];
                patient_name.Text = Request.QueryString["patient_name"];
                machine_type.Text = Request.QueryString["machine_type"];
                hpack.Text = Request.QueryString["hpack"];
                bedno.Text = Request.QueryString["bedno"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                daytyp.Text = Request.QueryString["daytyp"];
                hpack3.Text = Request.QueryString["hpack3"];
                mechine_model.Text = Request.QueryString["mechine_model"];
                txt_weight_before.Text = Request.QueryString["patient_weight"];
                page.Text = Request.QueryString["page"];
                info_date = Request.QueryString["infodate"];

                if (patient_name.Text == null)
                {
                    patient_name.Text = Request.QueryString["amp;patient_name"];
                    machine_type.Text = Request.QueryString["amp;machine_type"];
                    hpack.Text = Request.QueryString["amp;hpack"];
                    bedno.Text = Request.QueryString["amp;bedno"];
                    floor.Text = Request.QueryString["amp;floor"];
                    area.Text = Request.QueryString["amp;area"];
                    time.Text = Request.QueryString["amp;time"];
                    daytyp.Text = Request.QueryString["amp;daytyp"];
                    hpack3.Text = Request.QueryString["amp;hpack3"];
                    mechine_model.Text = Request.QueryString["amp;mechine_model"];
                    txt_weight_before.Text = Request.QueryString["amp;patient_weight"];
                    page.Text = Request.QueryString["amp;page"];
                }

                if (Session["USER_NAME"] == null || Session["USER_RIGHT"] == null)
                {
                    X.Redirect("login117.aspx?patient_id=" + patient_id.Text +
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
                }
                //else
                //{
                //    Session["USER_RIGHT"] = "";
                //    X.Redirect("ipad_PatientList.aspx?floor=" + floor.Text +
                //        "&area=" + area.Text +
                //        "&time=" + time.Text +
                //        "&bedno=" + bedno.Text +
                //        "&daytyp=" + daytyp.Text);
                //}
                if (Session["USER_RIGHT"] != null)
                {
                    if (Session["USER_RIGHT"].ToString() == "NU")
                    {
                        nurse_flag.Text = "true";
                        cbo_diagnosis.ReadOnly = true;
                        cbo_mechine_model.ReadOnly = true;
                        cbo_dialysis_type.ReadOnly = true;
                        txt_weight_before.ReadOnly = true;
                        txt_weight_after_expect.ReadOnly = true;
                        TextField3.ReadOnly = true;
                        txt_weight_after.ReadOnly = true;
                        //TextTotalCap.ReadOnly = true;
                        cb_info_date.ReadOnly = true;
                        TextField5.ReadOnly = true;
                        TextField6.ReadOnly = true;
                        TextField7.ReadOnly = true;
                        TextField8.ReadOnly = true;
                        TextField9.ReadOnly = true;
                        TextField10.ReadOnly = true;
                        Checkbox1.ReadOnly = true;
                        SelectBoxEPO.ReadOnly = true;
                        Checkbox2.ReadOnly = true;
                        SelectBoxLcard.ReadOnly = true;
                        Checkbox3.ReadOnly = true;
                        SelectBoxFe.ReadOnly = true;
                        Checkbox4.ReadOnly = true;
                        SelectBoxCalcitriol.ReadOnly = true;
                        Checkbox5.ReadOnly = true;
                        SelectBoxMethycobal.ReadOnly = true;
                        Checkbox6.ReadOnly = true;
                        SelectBoxB12.ReadOnly = true;
                        TextArea1.ReadOnly = true;

                        cbo_SelDialysisNa.ReadOnly = true;
                        TextField11.ReadOnly = true;
                        cbo_hpack3.ReadOnly = true;
                        cbo_h_type.ReadOnly = true;
                        TextTotalCap.ReadOnly = true;

                        cbo_DialysisMachine.Visible = true;
                        cbo_HeparinPump_a.Visible = true;
                        cbo_HeparinPump_v.Visible = true;
                        TextFieldCatheterAccess.Visible = true;
                        TextFieldMuscleAtrophy.Visible = true;
                        TextField13.Visible = true; //護士簽名
                        TextField24.Visible = true; //護士簽名
                        TextField131.Visible = true; //護士簽名
                        Panel1.Visible = true;
                    }
                    else //if (Session["USER_RIGHT"].ToString() == "DC" || Session["USER_RIGHT"].ToString() != "DH")
                    {
                        nurse_flag.Text = "false";
                        cbo_DialysisMachine.Visible = false;
                        cbo_HeparinPump_a.Visible = false;
                        cbo_HeparinPump_v.Visible = false;
                        TextFieldCatheterAccess.Visible = false;
                        TextFieldMuscleAtrophy.Visible = false;
                        TextField13.Visible = false;
                        TextField24.Visible = false;
                        TextField131.Visible = false;
                        Panel1.Visible = false;
                        ImageBtn_TurnOff.Hidden = true;
                    }
                }
                patient_weight.Text = txt_weight_before.Text; //體重
                SetSelectBox();
                ShowFields();
                CheckcboValue();
                Show_TPRBP(); //淨化明細
            }
        }

        protected void ShowFields()
        {
            string sql;
            Label2.Text = patient_name.Text;
            Label4.Text = floor.Text;
            Label6.Text = bedno.Text;
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate <> '" + toDay + "' ";
            sql += "ORDER BY cln1_diadate DESC LIMIT 1";
            DataTable dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                //cb_info_date.Text = dt0.Rows[0]["cln1_diadate"].ToString(); //上次日期，捉取上次的各項資料
                cln1_col1 = dt0.Rows[0]["cln1_col1"].ToString();
                cln1_col3 = dt0.Rows[0]["cln1_col3"].ToString();
                cln1_col6 = dt0.Rows[0]["cln1_col6"].ToString();
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
                cb_info_date.Text = toDay; //今天日期

            DataTable dt = new DataTable();
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate='" + info_date + "' ";
            if (info_date == toDay)
                sql += "AND cln1_col5 IS NOT NULL";
            dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "SELECT a.*, ";
                sql += "b.cln1_col11 as col11, b.cln1_col12 as col12, ";
                sql += "b.cln1_col21 as col21, b.cln1_col22 as col22, ";
                sql += "b.cln1_col31 as col31, b.cln1_col32 as col32, ";
                sql += "b.cln1_col41 as col41, b.cln1_col42 as col42, ";
                sql += "b.cln1_col50 as col50, ";
                sql += "c.cln3_DialysisMachine, c.cln3_HeparinPumpArtery, c.cln3_HeparinPumpVein, c.cln3_CatheterAccess, c.cln3_MuscleAtrophy ";
                sql += "FROM clinical1_nurse a ";
                sql += "LEFT JOIN clinical1_nurse_suzhou b ON a.cln1_patic=b.cln1_patic AND a.cln1_diadate=b.cln1_diadate ";
                sql += "LEFT JOIN clinical3_nurse c ON a.cln1_patic=c.cln3_patic AND a.cln1_diadate=c.cln3_date ";
                sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate='" + info_date + "'";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    DataRow row = dt.Rows[0];
                    if (row["cln1_col1"].ToString() == "")
                        Common.SetComboBoxValue(cbo_diagnosis, cln1_col1, false);
                    else
                        Common.SetComboBoxValue(cbo_diagnosis, row["cln1_col1"].ToString(), false); //诊断

                    if (row["cln1_col26"].ToString() == "")
                        Common.SetComboBoxValue(cbo_mechine_model, cln1_col26, false);
                    else
                        Common.SetComboBoxValue(cbo_mechine_model, row["cln1_col26"].ToString(), false); //透析器型号

                    if (row["cln1_col3"].ToString() == "")
                    {
                        Common.SetComboBoxValue(cbo_dialysis_type, cln1_col3, false);
                        if (cbo_dialysis_type.Text == "HDF" || cbo_dialysis_type.Text == "HF")
                        {
                            TextField11.Text = "25";
                        }
                    }
                    else
                        Common.SetComboBoxValue(cbo_dialysis_type, row["cln1_col3"].ToString(), false);//透析方式

                    if (row["cln1_col27"].ToString() == "")
                        Common.SetComboBoxValue(cbo_hpack3, cln1_col27, false);
                    else
                        Common.SetComboBoxValue(cbo_hpack3, row["cln1_col27"].ToString(), false); //管路型号

                    if (row["cln1_col6"].ToString() == "")
                        txt_weight_after_expect.Text = cln1_col6; 
                    else
                        txt_weight_after_expect.Text = row["cln1_col6"].ToString(); //干体重

                    txt_weight_after.Text = row["cln1_col8"].ToString(); //透析后体重
                    try
                    {
                        txt_weight_before.Text = dt.Rows[0]["cln1_col5"].ToString(); //透析前体重
                        if (dt.Rows[0]["cln1_col7"].ToString() == "")
                            TextField3.Text = (Convert.ToDecimal(txt_weight_before.Text) - Convert.ToDecimal(txt_weight_after_expect.Text)).ToString();
                        else
                            TextField3.Text = dt.Rows[0]["cln1_col7"].ToString();
                    }
                    catch { }

                    if (dt.Rows[0]["cln1_dateadded"].ToString() != cb_info_date.Text) //原開機就寫入今天日期，因沒用到此欄位，故拿來給117用
                        TextTotalCap.Text = dt.Rows[0]["cln1_dateadded"].ToString(); //总定容量

                    TextField5.Text = dt.Rows[0]["cln1_col10"].ToString(); //透析開始時間
                    TextField6.Text = dt.Rows[0]["cln1_col11"].ToString(); //透析結束時間
                    TextField7.Text = dt.Rows[0]["cln1_col12"].ToString(); //透析合計時間
                    if (row["cln1_col13"].ToString() == "")
                        TextField8.Text = cln1_col13;
                    else
                        TextField8.Text = row["cln1_col13"].ToString();
                    TextField9.Text = row["cln1_col14"].ToString();
                    if (row["cln1_col15"].ToString() == "")
                        TextField10.Text = cln1_col15;
                    else
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
                    if ((page.Text == "1") || (row["cln1_diadate"].ToString() == info_date))
                    {
                        TextArea1.Text = row["cln1_col23"].ToString();

                        string[] check = row["cln1_col36"].ToString().Split(new Char[] { ',' });

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
                    //show_Panel2(cb_info_date.Text); //淨化明細 
                }
            }
            else
                init_PAGE2();
            dt.Dispose();
            //db.myConnection.Close();
        }

        protected void SetSelectBox()
        {
            string sSQL;
            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='diagnosis' ";
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
            //SelectBoxEPO.Select(0);

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
            ////從小結移過來 

            info_date = toDay;
            if (page.Text == "1")
            {
                sSQL = "SELECT cln1_diadate AS CODE, cln1_diadate AS NAME, cln1_diadate AS DiaDate FROM clinical1_nurse ";
                sSQL += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate <> '" + toDay + "' ";
                sSQL += "ORDER BY cln1_diadate DESC LIMIT 5";
                dt1 = db.Query(sSQL);
                Common.SetComboBoxItem(cb_info_date, dt1, false, "NAME", "CODE");
                cb_info_date.ReadOnly = false;
                if (Session["info_date"] != null)
                {
                    info_date = Session["info_date"].ToString();
                    Common.SetComboBoxValue(cb_info_date, info_date, false);
                }
                else
                    cb_info_date.Text = dt1.Rows[0]["DiaDate"].ToString();
                cb_info_date.LabelCls = "blink"; //透析日期
                ImageBtn_TurnOff.Enabled = false;
                ImageBtn_TurnOff.Visible = false;
            }
            else
            {
                sSQL = "SELECT cln1_diadate AS CODE, cln1_diadate AS NAME FROM clinical1_nurse ";
                sSQL += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate='" + toDay + "' ";
                sSQL += "ORDER BY cln1_diadate DESC LIMIT 1";
                dt1 = db.Query(sSQL);
                Common.SetComboBoxItem(cb_info_date, dt1, false, "NAME", "CODE");
                cb_info_date.Select(0);
                cb_info_date.ReadOnly = true;

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
        }

        protected void Show_TPRBP()
        {
            //DBMysql db = new DBMysql();
            string person_id = patient_id.Text;
            string DialysisDate = cb_info_date.Text;
            String sql = "SELECT b.cln2_date, b.cln2_time, a.column_7, a.column_6, a.column_2, a.column_10, a.column_8, a.column_4, ";
            sql += "b.cln2_t,b.cln2_p,b.cln2_r,b.cln2_bp,b.cln2_rmk,b.cln2_user, cln2_dateadded, a.dialysis_date, a.dialysis_time FROM clinical2_nurse as b ";
            sql += "LEFT JOIN data_list as a ON a.person_id=b.cln2_patic AND a.dialysis_date=b.cln2_date AND a.dialysis_time=b.cln2_time ";
            sql += "WHERE b.cln2_patic='" + person_id + "' AND b.cln2_date='" + DialysisDate + "' ";
            sql += "ORDER BY b.cln2_time";
            DataTable dt = db.Query(sql);
            Store istore = Grid_Show_TPRBP.GetStore();
            istore.DataSource = GetDataArray(dt);
            istore.DataBind();
            //db.myConnection.Close();
        }

        //protected void show_Panel2(string date)
        //{
        //    Panel2.Loader.SuspendScripting();
        //    Panel2.Loader.Url = "Information/Dialysis_09_02_detail.aspx?date=" + date + "&page=2&patient_id=" + patient_id.Text + "&bedno=" + bedno.Text + "&floor=" + floor.Text;
        //    Panel2.Loader.DisableCaching = true;
        //    Panel2.LoadContent();
        //}

        protected void init_PAGE1()
        {
            //Panel2.Visible = true;
            //Panel1.Visible = false;
            ImageBtn_save1.Visible = false;
            ImageBtn_TurnOff.Visible = false;
            //Grid_Show_TPRBP.Visible = false;
            FormPanel1.Title = "治疗参数(" + hName + ")";

            txt_weight_before.ReadOnly = true;
            txt_weight_after_expect.ReadOnly = true;
            TextField3.ReadOnly = true;
            txt_weight_after.ReadOnly = true;
            TextField8.ReadOnly = true;
            TextField9.ReadOnly = true;
            TextField10.ReadOnly = true;

            SelectBoxEPO.ReadOnly = true; 
            SelectBoxLcard.ReadOnly = true;
            SelectBoxFe.ReadOnly = true;
            SelectBoxCalcitriol.ReadOnly = true;
            SelectBoxMethycobal.ReadOnly = true;
            SelectBoxB12.ReadOnly = true;
            Checkbox1.ReadOnly = true;
            Checkbox2.ReadOnly = true;
            Checkbox3.ReadOnly = true;
            Checkbox4.ReadOnly = true;
            Checkbox5.ReadOnly = true;
            Checkbox6.ReadOnly = true;

            cbo_diagnosis.ReadOnly = true;
            cbo_mechine_model.ReadOnly = true;
            cbo_hpack3.ReadOnly = true;
            cbo_SelDialysisNa.ReadOnly = true;
            cbo_dialysis_type.ReadOnly = true;
            cbo_h_type.ReadOnly = true;
            cbo_SelDialysisNa.ReadOnly = true;

            TextField11.ReadOnly = true;
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
            cbo_SelDialysisNa.RemoveCls("Text-black");
            cbo_SelDialysisNa.AddCls("Text-blue");
            cbo_dialysis_type.RemoveCls("Text-black");
            cbo_dialysis_type.AddCls("Text-blue");
            cbo_h_type.RemoveCls("Text-black");
            cbo_h_type.AddCls("Text-blue");
            cbo_SelDialysisNa.RemoveCls("Text-black");
            cbo_SelDialysisNa.AddCls("Text-blue");

            cbo_DialysisMachine.ReadOnly = true; //透析器凝血
            cbo_HeparinPump_a.ReadOnly = true; //动脉壶凝血
            cbo_HeparinPump_v.ReadOnly = true; //静脉壶凝血
        }

        protected void init_PAGE2()
        {
            FormPanel1.Title = "血液净化记录(" + hName + ")";
            TextField13.RemoveCls("Text-blue");
            TextField13.AddCls("red");
            TextField23.RemoveCls("Text-blue");
            TextField23.AddCls("red");
            TextField24.RemoveCls("Text-blue");
            TextField24.AddCls("red");
            TextField131.RemoveCls("Text-blue");
            TextField131.AddCls("red");
        }

        public string GetCheckStr()
        {
            string checkstr = "";
            if (SelectBoxEPO.Text.Trim() != "")
            {
                checkstr += "EPO,";
            }
            if (SelectBoxLcard.Text.Trim() != "")
            {
                checkstr += "左卡,";
            }
            if (SelectBoxFe.Text.Trim() != "")
            {
                checkstr += "铁剂,";
            }
            if (SelectBoxCalcitriol.Text.Trim() != "")
            {
                checkstr += "骨化三醇,";
            }
            if (SelectBoxMethycobal.Text.Trim() != "")
            {
                checkstr += "弥可保,";
            }
            if (SelectBoxB12.Text.Trim() != "")
            {
                checkstr += "维生素B12";
            }
            return checkstr;
        }

        protected void ImageBtn_save1_Click(object sender, DirectEventArgs e)
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
                string checktext = GetCheckStr();

                sql = "SELECT cln1_diadate FROM clinical1_nurse ";
                sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate = '" + cb_info_date.Text + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical1_nurse ";
                    sql += " SET ";
                    if (nurse_flag.Text == "true")
                    {
                        sql += "     cln1_dateadded = '" + TextTotalCap.Text + "', ";
                        sql += "     cln1_col21 = '" + TextField13.Text + "',";
                        sql += "     cln1_col35 = '" + TextField24.Text + "',";
                        sql += "     cln1_col25 = '" + TextField131.Text + "' ";
                    }
                    else
                    {
                        sql += "     cln1_col1 = '" + Common.GetComboBoxText(cbo_diagnosis) + "',";
                        sql += "     cln1_col2 = '" + bedno.Text + "',";
                        sql += "     cln1_col3 = '" + Common.GetComboBoxText(cbo_dialysis_type) + "',";
                        sql += "     cln1_col4 = '" + hpack.Text + "',";
                        sql += "     cln1_col5 = '" + txt_weight_before.Text + "',";
                        sql += "     cln1_col6 = '" + txt_weight_after_expect.Text + "',";
                        sql += "     cln1_col7 = '" + TextField3.Text + "',";
                        sql += "     cln1_col8 = '" + txt_weight_after.Text + "',";
                        sql += "     cln1_col9 = '" + cb_info_date.Text + "',";
                        sql += "     cln1_col10 = '" + TextField5.Text + "',";
                        sql += "     cln1_col11 = '" + TextField6.Text + "',";
                        sql += "     cln1_col12 = '" + TextField7.Text + "',";
                        sql += "     cln1_col13 = '" + TextField8.Text + "',";
                        sql += "     cln1_col14 = '" + TextField9.Text + "',";
                        sql += "     cln1_col15 = '" + TextField10.Text + "',";
                        sql += "     cln1_col28 = '" + SelectBoxEPO.Text + "',";
                        sql += "     cln1_col29 = '" + SelectBoxLcard.Text + "',";
                        sql += "     cln1_col30 = '" + SelectBoxFe.Text + "',";
                        sql += "     cln1_col31 = '" + SelectBoxCalcitriol.Text + "',";
                        sql += "     cln1_col32 = '" + SelectBoxMethycobal.Text + "',";
                        sql += "     cln1_col37 = '" + SelectBoxB12.Text + "',";
                        sql += "     cln1_col30q = 0,";
                        sql += "     cln1_col31q = 0,";
                        sql += "     cln1_col32q = 0,";
                        sql += "     cln1_col17 = '" + cbo_SelDialysisNa.Text + "',";
                        sql += "     cln1_col18 = '',";
                        sql += "     cln1_col19 = '" + TextField11.Text + "',";
                        sql += "     cln1_col20 = '',";
                        sql += "     cln1_col22 = '',";
                        sql += "     cln1_col23 = '" + TextArea1.Text + "',";
                        sql += "     cln1_col26 = '" + Common.GetComboBoxText(cbo_mechine_model) + "', ";
                        sql += "     cln1_col33 = '" + TextField23.Text + "',";
                        sql += "     cln1_col27 = '" + Common.GetComboBoxText(cbo_hpack3) + "', ";
                        sql += "     cln1_col34 = '" + Common.GetComboBoxText(cbo_h_type) + "',";
                        sql += "     cln1_col36 = '" + checktext + "',";
                        sql += "     cln1_user = '',";
                        sql += "     cln1_dateadded = '" + TextTotalCap.Text + "' ";
                    }
                    sql += "WHERE cln1_patic = '" + patient_id.Text + "' ";
                    sql += "  AND cln1_diadate = '" + cb_info_date.Text + "' ";
                }
                else
                {
                    if (nurse_flag.Text == "true")
                    {
                        sql = "INSERT into clinical1_nurse(cln1_patic,cln1_diadate,cln1_col21,cln1_col35,cln1_col25) ";
                        sql += "VALUES('" + patient_id.Text + "','" + cb_info_date.Text + "','";
                        sql += "'" + TextField13.Text + "','" + TextField24.Text + "','" + TextField131.Text + "') ";
                    }
                    else
                    {
                        sql = "INSERT into clinical1_nurse(cln1_patic,cln1_diadate,";
                        sql += "cln1_col1,cln1_col2,cln1_col3,cln1_col4,cln1_col5,";

                        sql += "cln1_col6,cln1_col7,cln1_col8,cln1_col9,cln1_col10,";
                        sql += "cln1_col11,cln1_col12,cln1_col13,cln1_col14,cln1_col15,";
                        sql += "cln1_col17,cln1_col18,cln1_col19,cln1_col20,";

                        sql += "cln1_col22,cln1_col23,";
                        sql += "cln1_col26,cln1_col27,";
                        sql += "cln1_col28,cln1_col29,cln1_col30,cln1_col31,cln1_col32, cln1_col37, cln1_col30q,cln1_col31q,cln1_col32q,"; //加上數量
                        sql += "cln1_col33,cln1_col34,";
                        sql += "cln1_col36,cln1_user,cln1_dateadded) ";
                        sql += "VALUES('" + patient_id.Text + "','" + cb_info_date.Text + "','";
                        sql += Common.GetComboBoxText(cbo_diagnosis) + "','" + bedno.Text + "','" + machine_type.Text + "','" + hpack.Text + "','" + txt_weight_before.Text + "','";
                        sql += txt_weight_after_expect.Text + "','" + TextField3.Text + "','" + txt_weight_after.Text + "','" + cb_info_date.Text + "','" + TextField5.Text + "','";
                        sql += TextField6.Text + "','" + TextField7.Text + "','" + TextField8.Text + "','" + TextField9.Text + "','" + TextField10.Text + "','";                 
                        sql += cbo_SelDialysisNa.Text + "','','" + TextField11.Text + "',";
                        sql += "'" + TextArea1.Text + "','','',";
                        sql += "'" + Common.GetComboBoxText(cbo_mechine_model) + "','" + Common.GetComboBoxText(cbo_hpack3) + "',";
                        //擬用藥 EPO, 左卡, 铁剂, 骨化三醇, 弥可保
                        sql += "'" + SelectBoxEPO.Text + "','" + SelectBoxLcard.Text + "','" + SelectBoxFe.Text + "','" + SelectBoxCalcitriol.Text + "','" + SelectBoxMethycobal.Text + "',";
                        sql += "'" + SelectBoxB12.Text + "',"; //cln1_col37=维生素B12
                        sql += "0,0,0,";
                        //擬用藥
                        sql += "'" + TextField23.Text + "','" + Common.GetComboBoxText(cbo_h_type) + "',"; //医生, 血管通路类型
                        sql += "'" + checktext.Substring(1) + "',";
                        sql += "'','" + TextTotalCap.Text + "')"; //总定容量
                    }
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
                         "AND pv_datevisit='" + cb_info_date.Text + "' ";
                db.Excute(sql);

                if (nurse_flag.Text == "true")
                {
                    sql = "SELECT * FROM clinical3_nurse ";
                    sql += " where cln3_patic = '" + patient_id.Text + "' ";
                    sql += "  and cln3_date = '" + toDay + "' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        sql = "UPDATE clinical3_nurse SET ";
                        sql += "cln3_CatheterAccess = '" + TextFieldCatheterAccess.Text + "',";
                        sql += "cln3_MuscleAtrophy = '" + TextFieldMuscleAtrophy.Text + "',";
                        sql += "cln3_DialysisMachine = '" + Common.GetComboBoxText(cbo_DialysisMachine) + "',";
                        sql += "cln3_HeparinPumpArtery = '" + Common.GetComboBoxText(cbo_HeparinPump_a) + "',";
                        sql += "cln3_HeparinPumpVein = '" + Common.GetComboBoxText(cbo_HeparinPump_v) + "' ";
                        sql += "WHERE cln3_patic = '" + patient_id.Text + "' AND cln3_date = '" + toDay + "' ";
                    }
                    else
                    {
                        sql = "INSERT INTO clinical3_nurse(cln3_patic, cln3_date, cln3_CatheterAccess, cln3_MuscleAtrophy, cln3_DialysisMachine, cln3_HeparinPumpArtery, cln3_HeparinPumpVein) ";
                        sql += "VALUES('" + patient_id.Text + "','" + toDay + "',";
                        sql += "'" + TextFieldCatheterAccess.Text + "',";
                        sql += "'" + TextFieldMuscleAtrophy.Text + "',";
                        sql += "'" + Common.GetComboBoxText(cbo_DialysisMachine) + "',";
                        sql += "'" + Common.GetComboBoxText(cbo_HeparinPump_a) + "',";
                        sql += "'" + Common.GetComboBoxText(cbo_HeparinPump_v) + "')";
                    }
                    db.Excute(sql);
                }
                CheckcboValue();
                Common._NotificationShow("<font size=4>储存成功!</font>");
            }
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
                        sql += TextField20.Text + "','" + TextArea2.Text + "','";
                        sql += TextField21.Text + "','" + TextFieldTime.Text + "'); ";

                        sql += "INSERT INTO data_list(person_id, floor_no, bed_no, dialysis_date, dialysis_time";
                        sql += ", column_7, column_6, column_2, column_10, column_8, column_4, column_11) ";
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
                        if (TextField_2.Text.Trim() == "")
                            sql += "NULL, ";
                        else
                            sql += TextField_2.Text + ",";
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
                    TextField_2.Text = "";
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
                    if (TextField_2.Text.Trim() == "")
                        sql += "column_2=NULL, ";
                    else
                        sql += "column_2=" + TextField_2.Text + ", ";
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
                    sql += "      cln2_user = '" + TextField21.Text + "', ";
                    sql += "      cln2_time = '" + TextFieldTime.Text + "', ";
                    sql += "      cln2_dateadded = '" + TextFieldTime.Text + "' ";
                    sql += "WHERE cln2_id = " + sID + "; ";
                    //sql += "  AND cln2_date = '" + sDATE + "' AND cln2_time = '" + sTIME + "'; ";

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
                        TextField_2.Text = "";
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
                if (a == "TextField13" || a == "TextField24" || a == "TextField131")
                    nurse_flag.Text = "true";
                else
                    nurse_flag.Text = "false";

                if (Session["USER_RIGHT"].ToString() == "NU" && a != "TextField23")
                {
                    Window1.Show();
                    TextField_UserID.Focus(false, 100);
                }
                else if (Session["USER_RIGHT"].ToString() == "DC" && a == "TextField23")
                {
                    Window1.Show();
                    TextField_UserID.Focus(false, 100);
                }
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

        protected void Edit_Click(object sender, DirectEventArgs e)
        {            
            string sTIME = e.ExtraParams["TIME"];
            TextField1.Text = sTIME + "," + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss");

            string sql = "SELECT a.dialysis_date, a.dialysis_time, a.column_7, a.column_6, a.column_2, a.column_10, a.column_8, a.column_4, ";
            sql += "b.cln2_t,b.cln2_p,b.cln2_r,b.cln2_bp,b.cln2_rmk,b.cln2_user,b.cln2_dateadded, cln2_id, cln2_time FROM clinical2_nurse as b ";
            sql += "LEFT JOIN data_list as a ON a.person_id=b.cln2_patic AND a.dialysis_date=b.cln2_date AND a.dialysis_time=b.cln2_time ";
            sql += "WHERE cln2_patic = '" + patient_id.Text + "' AND b.cln2_date = '" + cb_info_date.Text + "' AND b.cln2_time = '" + sTIME + "' ";
            DataTable dt2 = db.Query(sql);
            if (dt2.Rows.Count > 0)
            {
                TextField1.Text = TextField1.Text + "," + dt2.Rows[0]["cln2_id"].ToString();

                TextField_7.Text = dt2.Rows[0]["Column_7"].ToString();
                TextField_6.Text = dt2.Rows[0]["Column_6"].ToString();
                TextField_2.Text = dt2.Rows[0]["Column_2"].ToString();
                TextField_10.Text = dt2.Rows[0]["Column_10"].ToString();
                TextField_8.Text = dt2.Rows[0]["Column_8"].ToString();
                TextField_4.Text = dt2.Rows[0]["Column_4"].ToString();

                TextField17.Text = dt2.Rows[0]["cln2_t"].ToString();
                TextField18.Text = dt2.Rows[0]["cln2_p"].ToString();
                TextField19.Text = dt2.Rows[0]["cln2_r"].ToString();
                TextField20.Text = dt2.Rows[0]["cln2_bp"].ToString();
                TextField21.Text = dt2.Rows[0]["cln2_user"].ToString();
                TextArea2.Text = dt2.Rows[0]["cln2_rmk"].ToString();
                TextFieldTime.Text = dt2.Rows[0]["cln2_time"].ToString();
                Panel1.Title = "净化过程明细 - 修改";
                Common._NotificationShow("三分钟内没存盘，自动取消修改");
                TaskManager1.StartTask("COUNT_DOWN");
            }

            //db.Close();
        }

        protected void Timer1_Timer(object sender, EventArgs e)
        {
            if (TextField1.Text != "")
            {
                string[] s = TextField1.Text.Split(new Char[] { ',' });
                if (s.Length == 3)
                {
                    TimeSpan iDiff = new TimeSpan(DateTime.Now.Ticks - Convert.ToDateTime(s[1]).Ticks);
                    if (iDiff.TotalSeconds > 60)
                    {
                        TextField1.Text = "";
                        Panel1.Title = "净化过程明细";
                        TextField_7.Text = "";
                        TextField_6.Text = "";
                        TextField_2.Text = "";
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
                    TextField_2.Text = "";
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

        #region 設定txt_weight_after_expect顏色
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

        #region 設定目标定容量顏色
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

        #region 顯示空白閃爍提醒
        protected void CheckcboValue()
        {
            //顯示顏色提醒
            if (cbo_diagnosis.Text.Trim() == "") //诊断
                cbo_diagnosis.LabelCls = "blink";
            else
                cbo_diagnosis.LabelCls = "my-Field";

            if (cbo_h_type.Text.Trim() == "") //血管通路类型
                cbo_h_type.LabelCls = "blink";
            else
                cbo_h_type.LabelCls = "my-Field";

            if (cbo_hpack3.Text.Trim() == "") //管路型号
                cbo_hpack3.LabelCls = "blink";
            else
                cbo_hpack3.LabelCls = "my-Field";

            if (cbo_mechine_model.Text.Trim() == "") //透析器型号
                cbo_mechine_model.LabelCls = "blink";
            else
                cbo_mechine_model.LabelCls = "my-Field";

            //cbo_diagnosis.Render();
            //cbo_h_type.Render();
            //cbo_hpack3.Render();
            //cbo_mechine_model.Render();
        }
        #endregion

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
            string sql;
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' ";
            sql += "AND cln1_diadate <> '" + toDay + "'"; //確認是否有歷史資料
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                string sURL = "ipad_history_117.aspx";
                sURL += "?patient_id=" + patient_id.Text +
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
                        "&daytyp=" + daytyp.Text + "&page=1";
                X.Redirect(sURL);
            }
            else
            {
                Common._ErrorMsgShow("查无前次资料");
                return;
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
                string sURL = "ipad_detaillist_117.aspx";
                X.Redirect(sURL + "?patient_id=" + patient_id.Text +
                                        "&patient_name=" + patient_name.Text +
                                        "&machine_type=" + machine_type.Text +
                                        "&mechine_model=" + cbo_mechine_model.Text +
                                        "&hpack=" + cbo_h_type.Text +
                                        "&hpack3=" + cbo_hpack3.Text +
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

            string sURL = "ipad_detaillist02_117.aspx";
            X.Redirect(sURL + "?patient_id=" + patient_id.Text +
                                "&patient_name=" + patient_name.Text +
                                "&bedno=" + bedno.Text +
                                "&floor=" + floor.Text +
                                "&mechine_model=" + cbo_mechine_model.Text +
                                "&hpack=" + hpack.Text +
                                "&hpack3=" + hpack3.Text +
                                "&area=" + area.Text +
                                "&time=" + time.Text +
                                "&daytyp=" + daytyp.Text);
        }
        #endregion

        #region 上一頁
        protected void Btn_back_Click(object sender, DirectEventArgs e)
        {
            //X.Redirect("ipad_PatientList.aspx?editmode=page3&floor=" + floor.Text +
            //                                                      "&area=" + area.Text +
            //                                                      "&time=" + time.Text +
            //                                                      "&bedno=" + bedno.Text +
            //                                                      "&dayTyp=" + daytyp.Text);
            string sURL = "ipad_PatientList.aspx?pid=" + patient_id.Text +
                "&floor=" + floor.Text +
                "&area=" + area.Text +
                "&time=" + time.Text +
                "&bedno=" + bedno.Text +
                "&dayTyp=" + daytyp.Text;
            X.Redirect(sURL);
        }
        #endregion

        #region 治療參數變更日期
        protected void ChangDialysisDate(object sender, DirectEventArgs e)
        {
            if (cb_info_date.Text != toDay && page.Text == "1")
            {
                Session["info_date"] = cb_info_date.Text;
                X.Redirect("ipad_detaillist_117.aspx?patient_id=" + patient_id.Text +
                                    "&patient_name=" + patient_name.Text +
                                    "&machine_type=" + machine_type.Text +
                                    "&mechine_model=" + cbo_mechine_model.Text +
                                    "&hpack=" + cbo_h_type.Text +
                                    "&hpack3=" + cbo_hpack3.Text +
                                    "&patient_weight=" + patient_weight.Text +
                                    "&bedno=" + bedno.Text +
                                    "&floor=" + floor.Text +
                                    "&area=" + area.Text +
                                    "&time=" + time.Text +
                                    "&daytyp=" + daytyp.Text + "&page=1");
            }
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
                    pat.update.set_dialysis_time(patient_id.Text, floor.Text, bedno.Text, cb_info_date.Text);
                    Status = "'S'";
                    ShowStopTime();
                    SaveClick(); //儲存淨化小結
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
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate='" + cb_info_date.Text + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                TextField6.Text = dt.Rows[0]["cln1_col11"].ToString();
                TextField7.Text = dt.Rows[0]["cln1_col12"].ToString();
            }
            dt.Dispose();
        }

        #region 血液淨化小節項目及儲存
        protected void Show()
        {
            string sql = "";
            sql = "SELECT Max(a.dialysis_time) b,TIMEDIFF(Max(a.dialysis_time),MIN(a.dialysis_time)) c FROM data_list a ";
            sql += " WHERE a.person_id ='" + patient_id.Text + "' ";
            sql += " AND a.column_11 <>'' ";
            sql += " AND a.dialysis_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            DataTable dt0 = db.Query(sql);
            if (dt0.Rows.Count != 0)
                cln3_time = dt0.Rows[0]["b"].ToString();

            sql = "SELECT cast(cln1_col5 as DECIMAL(6,1))-cast(cln1_col8 as DECIMAL(6,1)) column_2 FROM clinical1_nurse ";
            sql += "WHERE cln1_patic='" + patient_id.Text + "' ";
            sql += "  AND cln1_diadate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "  AND cln1_col5<>'' AND cln1_col8<>'' ";
            sql += "  AND (cln1_col5 - cln1_col8) >=0"; //抓透析前體重減掉透析後且不小於零體重
            dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
                cln1_col4 = dt0.Rows[0]["column_2"].ToString();//脫水
            else //沒有透析後體重則抓脫水最大值
            {
                sql = "SELECT column_2 FROM data_list ";
                sql += "WHERE person_id = '" + patient_id.Text + "' ";
                sql += "  AND dialysis_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                sql += "  AND column_2<>'' ";
                sql += "ORDER BY dialysis_time DESC limit 1";//抓脫水不等於零最後一筆
                dt0 = db.Query(sql);
                if (dt0.Rows.Count != 0 && cln1_col4 == dt0.Rows[0]["column_2"].ToString())
                    cln1_col4 = dt0.Rows[0]["column_2"].ToString();//脫水
            }
            //===========================================================================================================
            sql = "SELECT MIN(SUBSTRING_INDEX(cln2_bp,'/',1)) minA, MAX(SUBSTRING_INDEX(cln2_bp,'/',1)) maxA, ";
            sql += "MIN(SUBSTRING_INDEX(cln2_bp,'/',-1)) minB, MAX(SUBSTRING_INDEX(cln2_bp,'/',-1)) maxB ";
            sql += "FROM clinical2_nurse ";
            sql += "WHERE cln2_patic = '" + patient_id.Text + "' ";
            sql += "  AND cln2_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "  AND SUBSTRING_INDEX(cln2_bp,'/',1)<>'' AND SUBSTRING_INDEX(cln2_bp,'/',-1)<>'' ";
            sql += "  AND cln2_bp LIKE '%/%'";
            dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                //string[,] all = new string[dt0.Rows.Count, dt0.Columns.Count];

                //for (int r = 0; r < dt0.Rows.Count; r++)
                //{
                //    for (int c = 0; c < dt0.Columns.Count; c++)
                //    {
                //        all[r, c] = dt0.Rows[r][c].ToString();
                //    }
                //}

                ////預設每一列的第一個元素為max
                //int maxA = to_int(all[0, 0]);
                //int maxB = to_int(all[0, 1]);
                ////預設每一列的第一個元素為min
                //int minA = to_int(all[0, 0]);
                //int minB = to_int(all[0, 1]);
                //for (int j = 0; j < dt0.Rows.Count; j++)
                //{
                //    //在迴圈中作判斷，如果比max大，就把它放到max
                //    if (to_int(all[j, 0]) > maxA)
                //        maxA = to_int(all[j, 0]);
                //    if (to_int(all[j, 1]) > maxB)
                //        maxB = to_int(all[j, 1]);
                //    //如果比min小，就把它放到min
                //    if (to_int(all[j, 0]) < minA)
                //        minA = to_int(all[j, 0]);
                //    if (to_int(all[j, 1]) < minB)
                //        minB = to_int(all[j, 1]);
                //}

                cln3_a1 = dt0.Rows[0]["minA"].ToString(); // minA.ToString();
                cln3_a2 = dt0.Rows[0]["maxA"].ToString(); // maxA.ToString();
                cln3_b1 = dt0.Rows[0]["minB"].ToString(); // minB.ToString();
                cln3_b2 = dt0.Rows[0]["maxB"].ToString(); // maxB.ToString();
            }
        }

        //排除可能的错误 (clinical2_nurse.cln2_bp有造成錯誤的字元)
        private int to_int(string a)
        {
            try
            {
                a = a.Replace(".", "").Replace(" ", "");
                return Convert.ToInt16(a);
            }
            catch
            {
                return 0;
            }
        }

        private void SaveClick()
        {
            Show(); //透析小節顯示及處理
            string sql = "SELECT * FROM clinical3_nurse ";
            sql += "WHERE cln3_patic='" + patient_id.Text + "' AND cln3_date='" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "UPDATE clinical3_nurse SET ";
                sql += "  cln3_time = '" + cln3_time + "',";
                sql += "  cln3_rmk = '" + cln1_col4 + "',";
                sql += "  cln3_a1 = '" + cln3_a1 + "',";
                sql += "  cln3_a2 = '" + cln3_a2 + "',";
                sql += "  cln3_b1 = '" + cln3_b1 + "',";
                sql += "  cln3_b2 = '" + cln3_b2 + "',";
                sql += "  cln3_ysa = '无症状',";
                sql += "  cln3_pressure = '基本正常',";
                sql += "  cln3_bld='1', ";
                sql += "  cln3_yn = 'N', ";
                sql += "  cln3_rmk2 = '" + cln3_rmk2 + "' ";
                sql += " WHERE cln3_patic = '" + patient_id.Text + "' ";
                sql += "   AND cln3_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            }
            else
            {
                sql = "INSERT into clinical3_nurse(cln3_patic,cln3_date,cln3_time," +
                                                      "cln3_a1,cln3_a2,cln3_b1,cln3_b2," +
                                                      "cln3_CatheterAccess,cln3_MuscleAtrophy,cln3_ysa,cln3_pressure,cln3_DialysisMachine,cln3_HeparinPumpArtery,cln3_HeparinPumpVein," +
                                                      "cln3_bld,cln3_yn,cln3_rmk2,cln3_rmk) ";//
                sql += "VALUES('" + patient_id.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + cln3_time + "'," +
                              "'" + cln3_a1 + "','" + cln3_a2 + "','" + cln3_b1 + "','" + cln3_b2 + "'," +
                              "'',''," +
                              "'无症状','基本正常'," +
                              "'','','','1','N','" + cln3_rmk2 + "','" + cln1_col4 + "')";
            }
            db.Excute(sql);
            Common._NotificationShow("储存成功!");
            dt.Dispose();
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