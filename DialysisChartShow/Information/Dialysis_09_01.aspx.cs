using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_01 : BaseForm
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                show_grid();
            }
            
        }

        protected void show_grid()
        {
            DBMysql db = new DBMysql();
            string sql = "SELECT a.cln1_diadate,a.cln1_col1,a.cln1_col34,a.cln1_col5, a.cln1_col6,";
            sql += " a.cln1_col7,a.cln1_col8,a.cln1_col13,a.cln1_col14,a.cln1_col15 ";
            sql += "  FROM clinical1_nurse a,pat_info b ";
            sql += " WHERE b.pif_id = '" + _PAT_ID + "' ";
            sql += "   AND a.cln1_patic = b.pif_ic ";
            sql += "ORDER BY a.cln1_diadate DESC";

            DataTable dt = db.Query(sql);

            //sql = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup ";
            //DataTable dt4 = db.Query(sql);
            //System.Data.DataView dv4 = dt4.DefaultView;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dv4.RowFilter = "CODE='" + dt.Rows[i]["cln1_col4"].ToString() + "' ";
            //    if (dv4.Count > 0)
            //        dt.Rows[i]["cln1_col4"] = dv4[0]["NAME"].ToString();
            //}

            //20160321 Alex 沒作用刪除
            //sql = "SELECT hp_code AS CODE, hp_subgrp AS NAME FROM h_type ";
            //sql = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup"; //修正與FLEX抓取相同資料表
            //DataTable dt34 = db.Query(sql);
            //System.Data.DataView dv34 = dt34.DefaultView;
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    dv34.RowFilter = "CODE='" + dt.Rows[i]["cln1_col34"].ToString() + "' ";
            //    if (dv34.Count > 0)
            //        dt.Rows[i]["cln1_col34"] = dv34[0]["NAME"].ToString();
            //}



            Store istore = Grid_clinical1_nurse.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();

        }

        protected void RowSelect(object sender, DirectEventArgs e)
        {
            Grid_clinical1_nurse.Hidden = true;

            string json = e.ExtraParams["Values"];
            Dictionary<string,string>[]selRow= JSON.Deserialize<Dictionary<string,string>[]>(json);

            string sql = "SELECT a.cln1_diadate,a.cln1_col1,a.cln1_col2,a.cln1_col3,a.cln1_col4,a.cln1_col5, ";
            sql += " a.cln1_col6,a.cln1_col7,a.cln1_col8,a.cln1_col9,a.cln1_col10,a.cln1_col11,a.cln1_col12, ";
            sql += " a.cln1_col13,a.cln1_col14,a.cln1_col15,a.cln1_col16,a.cln1_col17,a.cln1_col18,a.cln1_col19, ";
            sql += " a.cln1_col20,a.cln1_col21,a.cln1_col22,a.cln1_col23, ";
            sql += " a.cln1_col28,a.cln1_col29,a.cln1_col30,a.cln1_col31,a.cln1_col32, ";
            sql += " a.cln1_col33,a.cln1_col34,a.cln1_col35,a.cln1_col36 ";
            sql += " FROM clinical1_nurse a,pat_info b ";
            sql += " WHERE b.pif_id = '" + _PAT_ID + "' ";
            sql += " AND a.cln1_patic = b.pif_ic ";
            sql += " AND a.cln1_diadate = '" + selRow[0]["info_date"].ToString() + "' ";

            DataTable dt = db.Query(sql);
            SelectBox1.Text = dt.Rows[0]["cln1_col1"].ToString();
            TextField2.Text = dt.Rows[0]["cln1_col2"].ToString();
            TextField3.Text = dt.Rows[0]["cln1_col3"].ToString();
            TextField4.Text = dt.Rows[0]["cln1_col34"].ToString();
            TextField5.Text = dt.Rows[0]["cln1_col5"].ToString();
            TextField6.Text = dt.Rows[0]["cln1_col6"].ToString();
            TextField7.Text = dt.Rows[0]["cln1_col7"].ToString();
            TextField8.Text = dt.Rows[0]["cln1_col8"].ToString();
            TextField9.Text = dt.Rows[0]["cln1_diadate"].ToString();
            TextField10.Text = dt.Rows[0]["cln1_col10"].ToString();
            TextField11.Text = dt.Rows[0]["cln1_col11"].ToString();
            TextField12.Text = dt.Rows[0]["cln1_col12"].ToString();
            TextField13.Text = dt.Rows[0]["cln1_col13"].ToString();
            TextField14.Text = dt.Rows[0]["cln1_col14"].ToString();
            TextField15.Text = dt.Rows[0]["cln1_col15"].ToString();
            //TextField.Text = dt.Rows[0]["cln1_col16"].ToString();
            string[] check = dt.Rows[0]["cln1_col16"].ToString().Split(new Char[] { ',' });

            TextCheckbox1.Hidden = true;
            TextCheckbox2.Hidden = true;
            TextCheckbox3.Hidden = true;
            TextCheckbox4.Hidden = true;
            TextCheckbox5.Hidden = true;
            for (int i = 0; i < check.Length; i++)
            {
                if (check[i] == "EPO")
                {
                    Checkbox1.Checked = true;
                    TextCheckbox1.Hidden = false;
                }
                else if (check[i] == "左卡")
                {
                    Checkbox2.Checked = true;
                    TextCheckbox2.Hidden = false;
                }
                else if (check[i] == "铁剂")
                {
                    Checkbox3.Checked = true;
                    TextCheckbox3.Hidden = false;
                }
                else if (check[i] == "钙剂")
                {
                    Checkbox4.Checked = true;
                    TextCheckbox4.Hidden = false;
                }
                else if (check[i] == "抗菌素/其它")
                {
                    Checkbox5.Checked = true;
                    TextCheckbox5.Hidden = false;
                }
            }
            TextCheckbox1.Text = dt.Rows[0]["cln1_col28"].ToString();
            TextCheckbox2.Text = dt.Rows[0]["cln1_col29"].ToString();
            TextCheckbox3.Text = dt.Rows[0]["cln1_col30"].ToString();
            TextCheckbox4.Text = dt.Rows[0]["cln1_col31"].ToString();
            TextCheckbox5.Text = dt.Rows[0]["cln1_col32"].ToString();

            SelectBox3.Text = dt.Rows[0]["cln1_col17"].ToString();
            SelectBox4.Text = dt.Rows[0]["cln1_col18"].ToString();
            TextField18.Text = dt.Rows[0]["cln1_col19"].ToString();
            TextField19.Text = dt.Rows[0]["cln1_col20"].ToString();
            TextField20.Text = dt.Rows[0]["cln1_col21"].ToString();
            TextField21.Text = dt.Rows[0]["cln1_col22"].ToString();

            TextField17.Text = dt.Rows[0]["cln1_col35"].ToString();
            TextField22.Text = dt.Rows[0]["cln1_col36"].ToString();
            TextField23.Text = dt.Rows[0]["cln1_col33"].ToString();

            TextArea1.Text = dt.Rows[0]["cln1_col23"].ToString();


            Panel1.Hidden = false;
        }

        protected void GetEvent1(object sender, DirectEventArgs e)
        {
            if (Checkbox1.Checked)
                TextCheckbox1.Hidden = false;
            else
            {
                TextCheckbox1.Text = "0";
                TextCheckbox1.Hidden = true;
            }
        }

        protected void GetEvent2(object sender, DirectEventArgs e)
        {
            if (Checkbox2.Checked)
                TextCheckbox2.Hidden = false;
            else
            {
                TextCheckbox2.Text = "0";
                TextCheckbox2.Hidden = true;
            }
        }

        protected void GetEvent3(object sender, DirectEventArgs e)
        {
            if (Checkbox3.Checked)
                TextCheckbox3.Hidden = false;
            else
            {
                TextCheckbox3.Text = "0";
                TextCheckbox3.Hidden = true;
            }
        }

        protected void GetEvent4(object sender, DirectEventArgs e)
        {
            if (Checkbox4.Checked)
                TextCheckbox4.Hidden = false;
            else
            {
                TextCheckbox4.Text = "0";
                TextCheckbox4.Hidden = true;
            }
        }

        protected void GetEvent5(object sender, DirectEventArgs e)
        {
            if (Checkbox5.Checked)
                TextCheckbox5.Hidden = false;
            else
            {
                TextCheckbox5.Text = "0";
                TextCheckbox5.Hidden = true;
            }
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            DBMysql db = new DBMysql();
            string checktext = "";
            if (Checkbox1.Checked == true)
            {
                checktext += ",EPO";
            }
            if (Checkbox2.Checked == true)
            {
                checktext += ",左卡";
            }
            if (Checkbox3.Checked == true)
            {
                checktext += ",铁剂";
            }
            if (Checkbox4.Checked == true)
            {
                checktext += ",钙剂";
            }
            if (Checkbox5.Checked == true)
            {
                checktext += ",抗菌素/其它";
            }

            string sql = "update clinical1_nurse a,pat_info b ";
            sql += "set a.cln1_col1 = '" + SelectBox1.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col2 = '" + TextField2.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col3 = '" + TextField3.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col34 = '" + TextField4.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col5 = '" + TextField5.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col6 = '" + TextField6.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col7 = '" + TextField7.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col8 = '" + TextField8.Text.Replace("'", "''") + "',";
            //sql += "a.cln1_col9 = '" + TextField9.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col10 = '" + TextField10.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col11 = '" + TextField11.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col12 = '" + TextField12.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col13 = '" + TextField13.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col14 = '" + TextField14.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col15 = '" + TextField15.Text.Replace("'", "''") + "',";
            if (checktext.Length > 1)
            {
                sql += "a.cln1_col16 = '" + checktext.Substring(1) + "',";
            }
            else
            {
                sql += "a.cln1_col16 = '',";
            }
            sql += "a.cln1_col28 = '" + TextCheckbox1.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col29 = '" + TextCheckbox2.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col30 = '" + TextCheckbox3.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col31 = '" + TextCheckbox4.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col32 = '" + TextCheckbox5.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col17 = '" + SelectBox3.Text + "',";
            sql += "a.cln1_col18 = '" + SelectBox4.Text + "',";
            sql += "a.cln1_col19 = '" + TextField18.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col20 = '" + TextField19.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col21 = '" + TextField20.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col22 = '" + TextField21.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col35 = '" + TextField17.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col36 = '" + TextField22.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col33 = '" + TextField23.Text.Replace("'", "''") + "',";
            sql += "a.cln1_col23 = '" + TextArea1.Text.Replace("'", "''") + "' ";
            sql += " WHERE b.pif_id = '" + _PAT_ID + "' ";
            sql += " AND a.cln1_patic = b.pif_ic ";
            sql += " AND a.cln1_diadate = '" + TextField9.Text + "' ";
            db.Excute(sql);
            Panel1.Hidden = true;

            Grid_clinical1_nurse.Hidden = false;
            show_grid();



            //增加一筆常規記錄zinfo_e_01 Andy 20150601 
               //1.hpack2_setup    透析機器型號設置
               //2.clinical1_nurs  血液淨化紀錄 
               //3.clinical2_nurse 血液淨化過程明細
               //4.a_result_log    檢驗記錄檔 

            //1.透析機器型號設置 hp2_id資料序號:key 自動
            DataTable dt;
            string sSQL     = "";
            string w_opt_11 = ""; 
            string w_pif_ic = "";
            sSQL = "SELECT * FROM pat_info " +
                                 "LEFT JOIN hpack2_setup " +
                                 "ON    pat_info.pif_hpack2 = hpack2_setup.hp2_code " +
                                 "WHERE pif_id=" + _PAT_ID + " ";
            dt = new DataTable();
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                //透析器名稱 hp2_name
                w_opt_11 = dt.Rows[0]["hp2_name"].ToString();  
                w_pif_ic = dt.Rows[0]["pif_ic"].ToString();
            }
            dt.Dispose();


            //2.clinical1_nurs  血液淨化紀錄 
            //cli_id淨化流水號 :key 自動
            //病人身分證號 cln1_patic + 日期cln1_diadate
            string winfo_date = _Get_YMD(TextField9.Text);
            string w_opt_7  = "";
            string w_num_13 = "";
            string w_num_8  = "";
            string w_num_9  = "";
            string w_num_10 = "";
            sSQL = "SELECT * FROM clinical1_nurse WHERE cln1_patic='" + w_pif_ic + "' AND cln1_diadate='" + TextField9.Text + "' ";
            dt = new DataTable();
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                //血管通路類型 cln1_col4
                w_opt_7 = dt.Rows[0]["cln1_col4"].ToString();
                //體重(乾體重)
                w_num_13 = dt.Rows[0]["cln1_col6"].ToString();
                //抗凝方案(甘素首量)
                w_num_8 = dt.Rows[0]["cln1_col13"].ToString();
                //抗凝方案(追加量)
                w_num_9 = dt.Rows[0]["cln1_col14"].ToString();
                //抗凝方案(低分子肝素)
                w_num_10 = dt.Rows[0]["cln1_col15"].ToString();
            }
            dt.Dispose();


            //3.clinical2_nurse 血液淨化過程明細
            //KEY:cln2_id:血液淨化流水號 自動
                string w_TextArea1 = "";
                int iCNT = 0;                
                string w_num_16 = "";
                string w_num_17 = "";
                string w_num_18 = "";
                string w_num_19 = "";
                string w_num_57 = "";
                string w_num_58 = "";
                double bpiLOW_MIN = 9999;
                double bpiLOW_MAX = 0;
                double bpiLOW_SUM = 0;
                double bpiLOW_AVG = 0;
                double bpiHIGH_MIN = 9999;
                double bpiHIGH_MAX = 0;
                double bpiHIGH_SUM = 0;
                double bpiHIGH_AVG = 0;
                string bpsLOW_MIN = "";
                string bpsLOW_MAX = "";
                string bpsLOW_AVG = "";
                string bpsHIGH_MIN = "";
                string bpsHIGH_MAX = "";
                string bpsHIGH_AVG = "";
                sSQL = "SELECT * FROM clinical2_nurse " +
                       "WHERE cln2_patic='"       + w_pif_ic + "' " +
                       " AND cln2_bp LIKE '%/%' " +
                       " AND cln2_date LIKE '"    + TextField9.Text + "%' " +
                       " ORDER BY cln2_date, cln2_time ";
            dt = new DataTable();
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string[] t = dt.Rows[i]["cln2_bp"].ToString().Split('/');
                    double zL = 0;
                    double zH = 0;
                    if (Double.TryParse(t[1], out zL))
                        zL = Convert.ToDouble(t[1]);
                    if (Double.TryParse(t[0], out zH))
                        zH = Convert.ToDouble(t[0]);
                    if ((zH > zL) && (zL > 0))
                    {
                        bpiLOW_SUM = bpiLOW_SUM + zL;
                        bpiHIGH_SUM = bpiHIGH_SUM + zH;
                        if (zH > bpiHIGH_MAX)
                            bpiHIGH_MAX = zH;
                        if (zL > bpiLOW_MAX)
                            bpiLOW_MAX = zL;
                        if (zH < bpiHIGH_MIN)
                            bpiHIGH_MIN = zH;
                        if (zL < bpiLOW_MIN)
                            bpiLOW_MIN = zL;
                        iCNT++;
                        w_TextArea1 += iCNT.ToString() + ". " + dt.Rows[i]["cln2_date"].ToString() + " " +
                                                                   dt.Rows[i]["cln2_time"].ToString() + " " +
                                                                   dt.Rows[i]["cln2_bp"].ToString() + Environment.NewLine;
                    }
                    else
                    {
                        w_TextArea1 += "X. " + dt.Rows[i]["cln2_date"].ToString() + " " +
                                                  dt.Rows[i]["cln2_time"].ToString() + " " +
                                                  dt.Rows[i]["cln2_bp"].ToString() + Environment.NewLine;
                    }
                }
                bpiLOW_AVG = bpiLOW_SUM / iCNT;
                if (bpiLOW_AVG != 0)
                    bpsLOW_AVG = bpiLOW_AVG.ToString("0.0");
                bpiHIGH_AVG = bpiHIGH_SUM / iCNT;
                if (bpiHIGH_AVG != 0)
                    bpsHIGH_AVG = bpiHIGH_AVG.ToString("0.0");
                if (bpiLOW_MIN != 9999)
                    bpsLOW_MIN = bpiLOW_MIN.ToString();
                if (bpiLOW_MAX != 0)
                    bpsLOW_MAX = bpiLOW_MAX.ToString();
                if (bpiHIGH_MIN != 9999)
                    bpsHIGH_MIN = bpiHIGH_MIN.ToString();
                if (bpiHIGH_MAX != 0)
                    bpsHIGH_MAX = bpiHIGH_MAX.ToString();
            }
            //血壓範圍：
            w_num_16 = bpsLOW_MIN;
            w_num_17 = bpsLOW_MAX;
            w_num_18 = bpsHIGH_MIN;
            w_num_19 = bpsHIGH_MAX;
            w_num_57 = bpsLOW_AVG;
            w_num_58 = bpsHIGH_AVG;
            dt.Dispose();
            //


            //4.a_result_log檢驗記錄檔 ROW_ID自動編號 :KEY
            //              病患資料列序號 PAT_NO
            sSQL = "";
            string w_num_31 ="";
            string w_num_32 ="";
            string w_num_37 ="";
            string w_txt_43 ="";
            string w_num_44 ="";
            string w_num_45 ="";
            string w_num_46 ="";
            string w_num_47 ="";
            string w_num_50 ="";
            string w_num_51 ="";
            sSQL = "SELECT RESULT_DATE, RESULT_CODE, RESULT_VALUE_T " +
                     " FROM a_result_log " +
                     " WHERE PAT_NO="      + _PAT_ID + " "   +
                     " AND RESULT_DATE='"  + TextField9.Text + "' " +
                     " AND RESULT_VER=0 ";
            dt = new DataTable();
            dt = db.Query(sSQL);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch(dt.Rows[i]["RESULT_CODE"].ToString())
                {
                    case "5017":
                        w_num_31 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//URR
                        break;
                    case "5018":
                        w_num_32 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//KT/V
                        break;
                    case "4003":
                        w_num_37 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//Hb
                        break;
                    case "4027":
                        w_txt_43 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//鐵蛋白
                        break;
                    case "4050":
                        w_num_44 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//鐵蛋白飽和度
                        break;
                    case "4021":
                        w_num_45 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//鈣
                        break;
                    case "4023":
                        w_num_46 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//磷
                        break;
                    case "4030":
                        w_num_47 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//iPTH
                        break;
                    case "4009":
                        w_num_50 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//GOT=AST
                        break;
                    case "4010":
                        w_num_51 = dt.Rows[i]["RESULT_VALUE_T"].ToString();//GPT=ALT
                        break;
                }
            }
             dt.Dispose();
            //

            //寫入常規記錄
            //PAT_ID 1/404
            //病患資料序號pat_id + 記錄日期INFO_DATE           
            //string winfo_date = _Get_YMD(TextField9.Text);

             string ww_opt_7 = "";
             switch (w_opt_7)
             {
                 case "临时中心静脉置管": //"临时中心静脉置管":
                     ww_opt_7 = "1";
                     break;
                 case "长期中心静脉置管": //"长期中心静脉置管":
                     ww_opt_7 = "2";
                     break;
                 case "自体内瘘":         //"自体内瘘":
                     ww_opt_7 = "3";
                     break;
                 case "移植血管":         //"移植血管":
                     ww_opt_7 = "4";
                     break;
                 default:
                     break;
             }

             string ww_opt_11="";
             switch (w_opt_11)
             {
                        case "Toray TS-1.3 S": //"Toray TS-1.3 S":
                            ww_opt_11 = "1";
                            break;
                        case "Toray TS-1.3 U": //"Toray TS-1.3 U":
                            ww_opt_11 = "2";
                            break;
                        case "Toray TS-1.6 SL": //"Toray TS-1.6 SL":
                            ww_opt_11 = "3";
                            break;
                        case "Toray TS-1.8 SL": //"Toray TS-1.8 SL":
                            ww_opt_11 = "4";
                            break;
                        case "旭化成REXEED 15UC": //"旭化成REXEED 15UC":
                            ww_opt_11= "5";
                            break;
                        case "尼普洛FB-150U": //"尼普洛FB-150U":
                            ww_opt_11 = "6";
                            break;
                        case "B1-1.6H": //"B1-1.6H":
                            ww_opt_11 = "7";
                            break;
                        default:
                            ww_opt_11 = "0";
                            break;            
             }

            sSQL = "";
            sSQL = "  SELECT * ";
            sSQL += " FROM zinfo_e_01 ";
            sSQL += " WHERE pat_id    = '" + _PAT_ID + "'";
            sSQL += " AND   info_date = '" + winfo_date + "'";
            DataTable dt2 = db.Query(sSQL);
            if (dt2.Rows.Count == 1)
            {
                sSQL = " UPDATE zinfo_e_01 set opt_7 = '"  + ww_opt_7 + "'," +
                                             " num_8  = '" + w_num_8  + "',"  +
                                             " num_9  = '" + w_num_9  + "',"  +
                                             " num_10 = '" + w_num_10 + "',"  +
                                             " opt_11 = '" + ww_opt_11 + "',"  +
                                             " num_13 = '" + w_num_13 + "',"  +
                                             " num_16 = '" + w_num_16 + "',"  +
                                             " num_17 = '" + w_num_17 + "',"  +
                                             " num_18 = '" + w_num_18 + "',"  +
                                             " num_19 = '" + w_num_19 + "',"  +
                                             " num_31 = '" + w_num_31 + "',"  +
                                             " num_32 = '" + w_num_32 + "',"  +
                                             " num_37 = '" + w_num_37 + "',"  +
                                             " txt_43 = '" + w_txt_43 + "',"  +
                                             " num_44 = '" + w_num_44 + "',"  +
                                             " num_45 = '" + w_num_45 + "',"  +
                                             " num_46 = '" + w_num_46 + "',"  +
                                             " num_47 = '" + w_num_47 + "',"  +
                                             " num_50 = '" + w_num_50 + "',"  +
                                             " num_51 = '" + w_num_51 + "',"  +
                                             " num_57 = '" + w_num_57 + "',"  +
                                             " num_58 = '" + w_num_58 + "',"  +
                                             "info_user = 'Admin"     + "'"  +
                       " WHERE pat_id    ='" + _PAT_ID + "'" +
                       " AND   info_date ='" + winfo_date + "'";
                db.Excute(sSQL);
            }
            else
            {
                sSQL = "INSERT INTO zinfo_e_01 (pat_id,info_date,info_user,opt_7,num_8,num_9,num_10,opt_11,num_13,num_16,num_17,num_18,num_19,num_31,num_32,num_37,txt_43,num_44,num_45,num_46,num_47,num_50,num_51,num_57,num_58) ";
                sSQL += "VALUES('" + _PAT_ID + "','" + winfo_date + "',";
                sSQL += "'Admin" + "',";
                sSQL += "'" + ww_opt_7 + "',";
                sSQL += "'" + w_num_8  + "',";
                sSQL += "'" + w_num_9  + "',";
                sSQL += "'" + w_num_10 + "',";
                sSQL += "'" + ww_opt_11 + "',";
                sSQL += "'" + w_num_13 + "',";
                sSQL += "'" + w_num_16 + "',";
                sSQL += "'" + w_num_17 + "',";
                sSQL += "'" + w_num_18 + "',";
                sSQL += "'" + w_num_19 + "',";
                sSQL += "'" + w_num_31 + "',";
                sSQL += "'" + w_num_32 + "',";
                sSQL += "'" + w_num_37 + "',";
                sSQL += "'" + w_txt_43 + "',";
                sSQL += "'" + w_num_44 + "',";
                sSQL += "'" + w_num_45 + "',";
                sSQL += "'" + w_num_46 + "',";
                sSQL += "'" + w_num_47 + "',";
                sSQL += "'" + w_num_50 + "',";
                sSQL += "'" + w_num_51 + "',";
                sSQL += "'" + w_num_57 + "',";
                sSQL += "'" + w_num_58 + "'";
                sSQL += "" + ")";
                db.Excute(sSQL);
            }
            dt2.Dispose();
            //
            _NotificationShow("储存成功!");            
        }

        public static string a;
        protected void text_click(object sender, EventArgs e)
        {
            TextField textID = (TextField)sender;
            a = textID.ID;
            Window1.Show();
        }

        protected void btnDecrypt_Click(object sender, DirectEventArgs e)
        {
            //if (TextField1.Text == "" || TextField16.Text == "")
            if (TextField1.Text == "" )
            {
                _ErrorMsgShow("请输入工号!");
                return;
            }
            else
            {
                DBMysql db = new DBMysql();
                string sql = "SELECT a.acclv_fname,a.acclv_funm ";
                sql += "  FROM access_level a ";
                sql += "where a.acclv_stfcode = '" + TextField1.Text.Replace("'", "''") + "' ";
                //sql += "  and a.acclv_funm = '" + TextField16.Text.Replace("'", "''") + "' ";
 

                DataTable dt = db.Query(sql);
                if (dt.Rows.Count == 1)
                {
                    if (a == "TextField19")
                    {
                        TextField19.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField20")
                    {
                        TextField20.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField21")
                    {
                        TextField21.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField17")
                    {
                        TextField17.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField22")
                    {
                        TextField22.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "TextField23")
                    {
                        TextField23.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                }
                else
                {
                    //AlexShen 20151210
                    //sql = "SELECT a.acclv_fname,a.acclv_funm ";
                    //sql += "  FROM access_level a ";
                    //sql += "where a.acclv_stfcode = '" + TextField1.Text.Replace("'", "''") + "' ";
                    //dt = db.Query(sql);
                    //if (dt.Rows.Count > 0)
                    //{
                        //_ErrorMsgShow("密码错误请重新输入!");
                        //TextField16.Text = "";

                        //return;
                    //}
                    //else
                    {
                        _ErrorMsgShow("登入失败请重新输入!");
                        //TextField16.Text = "";
                        TextField1.Text = "";
                        return;
                    }
                }

                a = "";
                TextField1.Text = "";
                //TextField16.Text = "";
                Window1.Close();
            }
        }

        protected void btnClose_Click(object sender, DirectEventArgs e)
        {
            a = "";
            TextField1.Text = "";
            //TextField16.Text = "";
            Window1.Close();
        }
    }
}