using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_05_01 : BaseForm
    {
        private string _TableName = "zinfo_e_01";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                sel_info_date = _Request("sel_info_date");
                switch (_Request("editmode"))
                {
                    case "list":
                        FormPanel1.Visible = false;
                        ShowList();
                        break;
                    case "show":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        setView();
                        btn_print.Visible = true;
                        btn_clear.Visible = false;
                        btn_save.Visible = false;
                        btn_close.Visible = true;
                        FormPanel1.Title = FormPanel1.Title + "-浏览";
                        break;
                    case "edit":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        setView();
                        btn_print.Visible = false;
                        btn_clear.Visible = true;
                        btn_save.Visible = true;
                        btn_close.Visible = false;
                        if (_Request("editmode2") == "add")
                        {
                            FormPanel1.Title = FormPanel1.Title + "-添加";
                            info_date.Text = "";
                            GetData(sender, e);
                        }
                        else
                        {
                            FormPanel1.Title = FormPanel1.Title + "-修改";
                            GetData(sender, e);
                        }
                        break;
                    case "delete":
                        _zInfo_Confirm_Delete(sel_info_date);
                        break;
                }
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);

            //2015.04.16 andy zinfo_a_07
            //zinfo_e_01 常規記錄 chk_55:心血管事件,?血管事件,感染,消化道出血等出血性疾病
            //zinfo_a_07 新增一筆 ??情?

            //2015.04.27 常規記錄:??     opt_52 退出,?移植,?出,死亡,?入
            //2015.04.27 常規記錄:死亡原因chk_55  心血管事件,?血管事件,感染,消化道出血等出血性疾病
            if (chk_55_1.Checked == true || chk_55_2.Checked == true || chk_55_3.Checked == true || chk_55_4.Checked == true || txt_56.Text != "")
            {

            }
            else
            {
                return;
            }
            string wchk_7 = "";
            string wopt_52 = "";
            //zinfo_e_01:常規記錄:死亡原因chk_55 心血管事件,?血管事件,感染,消化道出血等出血性疾病
            if (chk_55_1.Checked == true)
            {
                wchk_7 = "10000";
                wopt_52 = "4";
            }
            if (chk_55_2.Checked == true)
            {
                wchk_7 = "01000";
                wopt_52 = "4";
            }
            if (chk_55_3.Checked == true)
            {
                wchk_7 = "00100";
                wopt_52 = "4";
            }
            if (chk_55_4.Checked == true)
            {
                wchk_7 = "00010";
                wopt_52 = "4";
            }
            if (txt_56.Text != "" )
            {
                wchk_7 = "00001";
                wopt_52 = "4";
            }
            string sSQL = "";
            string winfo_date = _Get_YMD(info_date.Text);
            sSQL = "  SELECT * ";
            sSQL += " FROM zinfo_a_07 ";
            sSQL += " WHERE pat_id    = '" + _PAT_ID    + "'";
            sSQL += " AND   info_date = '" + winfo_date + "'";
            DataTable dt2 = db.Query(sSQL);
            if (dt2.Rows.Count == 1)
            {
                //常規記錄:病患資料+記錄日期 同步更新 轉出情況Zinfo_a_07
                //chk_7:心血管事件，脑血管事件，感染，消化道出血等出血性疾病，其它
                sSQL = " UPDATE zinfo_a_07 set chk_07 ='" +  wchk_7 + "'," +
                       " opt_1 = '" + wopt_52 + "'" +
                       " WHERE pat_id    ='" + _PAT_ID    + "'" +
                       " AND   info_date ='" + winfo_date + "'"; 
                db.Excute(sSQL);
            }
            else
            {
                //2015.04.27 新增 04.27  andy
                //zinfo_e_01 常規記錄 
                // = 4 死亡
                //
                //zinfo_a_07 診斷訊息  
                //           opt_1:??情? :退出，?移植，?出，死亡，?入
                //           chk_7:死亡原因 :心血管事件，?血管事件，感染，消化道出血等出血性疾病，其它
                //
                //zinfo_e_01(常規記錄):chk_55:心血管事件,?血管事件,感染,消化道出血等出血性疾病 :0001
                //zinfo_e_01(常規記錄):chk_52:退出,?移植,?出,死亡,?入 1 2 3 (4) 5
                sSQL = "INSERT INTO zinfo_a_07 (pat_id,info_date,info_user,opt_1,opt_2,txt_3,opt_4,opt_5,txt_6,chk_7,chk_8,txt_9,chk_10,txt_11,chk_12,txt_13,txt_14) ";
                sSQL += "VALUES('" + _PAT_ID + "','" + winfo_date + "',";
                sSQL += "'" + "',";
                sSQL += "'" + wopt_52 + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + wchk_7 + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "',";
                sSQL += "'" + "')";
                db.Excute(sSQL);

            }          
            //

            X.Redirect("Dialysis_05_01.aspx?editmode=list");
        }

        protected void Btn_Print_Click(object sender, DirectEventArgs e)
        {
            //X.Redirect("../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + _Request("sel_info_date") + "&_REPORT_NAME=6");
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + _Request("sel_info_date") + "&_REPORT_NAME=6";
            //Window1.Loader.Url = "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + _Request("sel_info_date") + "&_REPORT_NAME=61";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_05_01.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);
            X.Redirect("Dialysis_05_01.aspx?editmode=list");
        }

        private void ShowList()
        {
            string[] col_0 = { "建档日期", "info_date" };
            string[] col_1 = { "透析处方", "opt_1" };
            string[] col_2 = { "HD(次/W)", "num_2" };
            string[] col_3 = { "HD(h/次)", "opt_3" };
            string[] col_4 = { "HDF(次/W)", "num_4" };
            string[] col_5 = { "HDF(2h/次)", "opt_5" };
            string[] col_6 = { "血管通路位置", "opt_6" };
            string[] col_7 = { "通路类型", "opt_7" };
            string[] col_8 = { "肝素首量(mg)", "num_8" };
            string[] col_9 = { "追加量(mg)", "num_9" };
            string[] col_10 = { "低分子肝素(IU)", "num_10" };
            string[] col_11 = { "透析器型号", "opt_11" };
            string[] col_12 = { "其他透析器", "txt_12" };
            string[] col_13 = { "干体重(kg)", "num_13" };
            string[] col_14 = { "干体重较前", "opt_14" };
            string[] col_15 = { "血压控制", "opt_15" };
            //string[] col_16 = { "血压范围1(mmHg)", "num_16" };
            //string[] col_17 = { "血压范围2(mmHg)", "num_17" };
            //string[] col_18 = { "血压范围3(mmHg)", "num_18" };
            //string[] col_19 = { "血压范围4(mmHg)", "num_19" };

            //string[] col_20 = { "容量控制", "opt_20" };
            //string[] col_21 = { "容量(kg)", "num_21" };
            //string[] col_22 = { "约占干体重(%)", "num_22" };

            //string[] col_23 = { "血管通路功能", "opt_23" };
            //string[] col_24 = { "血流量", "num_24" };
            //string[] col_25 = { "主要不适、处理情况", "are_25" };
            //string[] col_26 = { "是否住院", "opt_26" };
            //string[] col_27 = { "住院病因", "txt_27" };
            //string[] col_28 = { "住院费用", "txt_28" };
            //string[] col_29 = { "住院主要检查、化验", "are_29" };
            //string[] col_30 = { "透析一般情况", "opt_30" };
            //string[] col_31 = { "URR(%)", "num_31" };
            //string[] col_32 = { "KT/V", "num_32" };
            //string[] col_33 = { "心脑血管系统", "opt_33" };
            //string[] col_34 = { "心脑血管系统相关事件", "txt_34" };
            //string[] col_35 = { "降压药物", "txt_35" };
            //string[] col_36 = { "贫血程度", "opt_36" };
            //string[] col_37 = { "Hb(g/L)", "num_37" };
            //string[] col_38 = { "EPO剂里(u)", "num_38" };
            //string[] col_39 = { "EPO(次/周)", "num_39" };
            //string[] col_40 = { "其他", "txt_40" };
            //string[] col_41 = { "铁代谢", "opt_41" };
            //string[] col_42 = { "前白蛋白", "opt_42" };
            //string[] col_43 = { "铁蛋白", "txt_43" };
            //string[] col_44 = { "转铁蛋白饱和度(%)", "num_44" };
            //string[] col_45 = { "钙(mmol/L)", "num_45" };
            //string[] col_46 = { "磷(mmol/L)", "num_46" };
            //string[] col_47 = { "iPTH(ng/L)", "num_47" };
            //string[] col_48 = { "营养指标", "txt_48" };
            //string[] col_49 = { "肝炎指标", "txt_49" };
            //string[] col_50 = { "GPT(u)", "num_50" };
            //string[] col_51 = { "GOT(u)", "num_51" };
            //string[] col_52 = { "转归", "opt_52" };
            //string[] col_53 = { "特殊病情、检查及处理", "txt_53" };
            //string[] col_54 = { "今后透析诊疗计划", "txt_54" };

            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);
            acol.Add(col_3);
            acol.Add(col_4);
            acol.Add(col_5);
            acol.Add(col_6);
            acol.Add(col_7);
            acol.Add(col_8);
            acol.Add(col_9);
            acol.Add(col_10);
            acol.Add(col_11);
            acol.Add(col_12);
            acol.Add(col_13);
            acol.Add(col_14);
            acol.Add(col_15);
            //acol.Add(col_16);
            //acol.Add(col_17);
            //acol.Add(col_18);
            //acol.Add(col_19);
            //acol.Add(col_20);
            //acol.Add(col_21);
            //acol.Add(col_22);
            //acol.Add(col_23);
            //acol.Add(col_24);
            //acol.Add(col_25);
            //acol.Add(col_26);
            //acol.Add(col_27);
            //acol.Add(col_28);
            //acol.Add(col_29);
            //acol.Add(col_30);
            //acol.Add(col_31);
            //acol.Add(col_32);
            //acol.Add(col_33);
            //acol.Add(col_34);
            //acol.Add(col_35);
            //acol.Add(col_36);
            //acol.Add(col_37);
            //acol.Add(col_38);
            //acol.Add(col_39);
            //acol.Add(col_40);
            //acol.Add(col_41);
            //acol.Add(col_42);
            //acol.Add(col_43);
            //acol.Add(col_44);
            //acol.Add(col_45);
            //acol.Add(col_46);
            //acol.Add(col_47);
            //acol.Add(col_48);
            //acol.Add(col_49);
            //acol.Add(col_50);
            //acol.Add(col_51);
            //acol.Add(col_52);
            //acol.Add(col_53);
            //acol.Add(col_54);



            _Fill_Html_Table(_TableName, _PAT_ID, acol, "病程记录列表");
        }

        protected void ii(object sender, DirectEventArgs e)
        {
            if (opt_52_4.Checked == true)
            {
                Container18.Hidden = false;
            }
            else
            {
                Container18.Hidden = true;
            }
        }

        protected void setView()
        {
            if (opt_52_4.Checked == true)
            {
                Container18.Hidden = false;
            }
            else
            {
                Container18.Hidden = true;
            }
        }

        protected void GetData(object sender, EventArgs e)
        {
            string sINFO_DATE = _Get_YMD2(info_date.Text);
            if (sINFO_DATE == "")
                return;
            string sDATA;
            string sSQL;
            DataTable dt_clinical1_nurse = new DataTable();
            DataTable dt;

            //ps: hpack2_setup(透析機器型號設置) andy
            info_date.IndicatorText = "　评估年月：" + sINFO_DATE.Substring(0, 7);
            sSQL = "SELECT * FROM pat_info " +
                     "LEFT JOIN hpack2_setup " +
                       "ON pat_info.pif_hpack2=hpack2_setup.hp2_code " +
                    "WHERE pif_id=" + _PAT_ID + " ";
            dt = new DataTable();
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                //透析器(透析器型号)
                if (opt_11_1.Checked == false && opt_11_2.Checked == false && opt_11_3.Checked == false &&
                    opt_11_4.Checked == false && opt_11_5.Checked == false && opt_11_6.Checked == false &&
                    opt_11_7.Checked == false)
                {
                    sDATA = dt.Rows[0]["hp2_name"].ToString();
                    switch (sDATA)
                    {
                        case "H000000001": //"Toray TS-1.3 S":
                            opt_11_1.Checked = true;
                            break;
                        case "H000000002": //"Toray TS-1.3 U":
                            opt_11_2.Checked = true;
                            break;
                        case "H000000003": //"Toray TS-1.6 SL":
                            opt_11_3.Checked = true;
                            break;
                        case "H000000004": //"Toray TS-1.8 SL":
                            opt_11_4.Checked = true;
                            break;
                        case "H000000005": //"旭化成REXEED 15UC":
                            opt_11_5.Checked = true;
                            break;
                        case "H000000007": //"尼普洛FB-150U":
                            opt_11_6.Checked = true;
                            break;
                        case "H000000006": //"B1-1.6H":
                            opt_11_7.Checked = true;
                            break;
                        default:
                            txt_12.Text = dt.Rows[0]["hp2_name"].ToString();
                            break;
                    }
                }
                //
                //if (.Text == "")
                //    .Text = dt.Rows[0][""].ToString();
            }
            dt.Dispose();

            //ps:clinical1_nurs(血液淨化紀錄) andy
            sSQL = "SELECT * FROM clinical1_nurse WHERE cln1_patic='" + _PAT_IC + "' AND cln1_diadate='" + sINFO_DATE + "' ";
            dt = new DataTable();
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                //透析一般參數(血管通路類型)
                if (opt_7_1.Checked == false && opt_7_2.Checked == false && opt_7_3.Checked == false && opt_7_4.Checked == false)
                {
                    sDATA = dt.Rows[0]["cln1_col4"].ToString();
                    switch (sDATA)
                    {
                        case "P000000003": //"临时中心静脉置管":
                            opt_7_1.Checked = true;
                            break;
                        case "P000000004": //"长期中心静脉置管":
                            opt_7_2.Checked = true;
                            break;
                        case "P000000001": //"自体内瘘":
                            opt_7_3.Checked = true;
                            break;
                        case "P000000002": //"移植血管":
                            opt_7_4.Checked = true;
                            break;
                        default:
                            break;
                    }
                }
                //體重(乾體重)
                if (num_13.Text == "")
                    num_13.Text = dt.Rows[0]["cln1_col6"].ToString();
                //抗凝方案(肝素首量)
                if (num_8.Text == "")
                    num_8.Text = dt.Rows[0]["cln1_col13"].ToString();
                //抗凝方案(追加量)
                if (num_9.Text == "")
                    num_9.Text = dt.Rows[0]["cln1_col14"].ToString();
                //抗凝方案(低分子肝素)
                if (num_10.Text == "")
                    num_10.Text = dt.Rows[0]["cln1_col15"].ToString();
            }
            dt.Dispose();




            int iCNT = 0;
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

            //ps:clinical2_nurse(血液淨化過程明細)
            sSQL = "SELECT * FROM clinical2_nurse " +
                    "WHERE cln2_patic='" + _PAT_IC + "' " +
                      "AND cln2_bp LIKE '%/%' " +
                      "AND cln2_date LIKE '" + sINFO_DATE.Substring(0,8) + "%' " +
                    "ORDER BY cln2_date, cln2_time ";
            dt = new DataTable();
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                TextArea1.Text = "";
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
                        TextArea1.Text += iCNT.ToString() + ". " + dt.Rows[i]["cln2_date"].ToString() + " " + 
                                                                   dt.Rows[i]["cln2_time"].ToString() + " " + 
                                                                   dt.Rows[i]["cln2_bp"].ToString() + Environment.NewLine;
                    }
                    else
                        TextArea1.Text += "X. " + dt.Rows[i]["cln2_date"].ToString() + " " + 
                                                  dt.Rows[i]["cln2_time"].ToString() + " " + 
                                                  dt.Rows[i]["cln2_bp"].ToString() + Environment.NewLine; 
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
            else
            {
            }
            num_16.Text = bpsLOW_MIN;
            num_17.Text = bpsLOW_MAX;
            num_18.Text = bpsHIGH_MIN;
            num_19.Text = bpsHIGH_MAX;
            num_57.Text = bpsLOW_AVG;
            num_58.Text = bpsHIGH_AVG;
            dt.Dispose();

            //ps:a_result_log檢驗記錄檔 andy
            sSQL = "SELECT RESULT_DATE, RESULT_CODE, RESULT_VALUE_T " +
                     "FROM a_result_log " +
                    "WHERE PAT_NO=" + _PAT_ID + " " +
                      "AND RESULT_DATE='" + sINFO_DATE + "' " +
                      "AND RESULT_VER=0 ";
            dt = new DataTable();
            dt = db.Query(sSQL);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                switch(dt.Rows[i]["RESULT_CODE"].ToString())
                {
                    case "5017":
                        num_31.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//URR
                        break;
                    case "5018":
                        num_32.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//KT/V
                        break;
                    case "4003":
                        num_37.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//Hb
                        break;
                    case "4027":
                        txt_43.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//铁蛋白
                        break;
                    case "4050":
                        num_44.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//转铁蛋白饱和度
                        break;
                    case "4021":
                        num_45.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//钙
                        break;
                    case "4023":
                        num_46.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//磷
                        break;
                    case "4030":
                        num_47.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//iPTH
                        break;
                    case "4009":
                        num_50.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//GOT=AST
                        break;
                    case "4010":
                        num_51.Text = dt.Rows[i]["RESULT_VALUE_T"].ToString();//GPT=ALT
                        break;
                }
            }
            dt.Dispose();

            //sSQL = "SELECT * FROM zinfo_d_01 WHERE pat_id=" + _PAT_ID + " AND dat_1='" + sINFO_DATE + "' ";
            //dt = new DataTable();
            //dt = db.Query(sSQL);
            //if (dt.Rows.Count > 0)
            //{
            //    //Hb
            //    if (num_37.Text == "")
            //        num_37.Text = dt.Rows[0]["num_3"].ToString();
            //    //铁蛋白
            //    if (txt_43.Text == "")
            //        txt_43.Text = dt.Rows[0]["num_14"].ToString();
            //    //转铁蛋白饱和度
            //    if (num_44.Text == "")
            //        num_44.Text = dt.Rows[0]["num_12"].ToString();
            //}
            //dt.Dispose();

            //sSQL = "SELECT * FROM zinfo_d_01 WHERE pat_id=" + _PAT_ID + " AND dat_6='" + sINFO_DATE + "' ";
            //dt = new DataTable();
            //dt = db.Query(sSQL);
            //if (dt.Rows.Count > 0)
            //{
            //    //钙
            //    if (num_45.Text == "")
            //        num_45.Text = dt.Rows[0]["num_7"].ToString();
            //    //磷
            //    if (num_46.Text == "")
            //        num_46.Text = dt.Rows[0]["num_8"].ToString();
            //    //iPTH
            //    if (num_47.Text == "")
            //        num_47.Text = dt.Rows[0]["num_9"].ToString();
            //}
            //dt.Dispose();

            //sSQL = "SELECT * FROM zinfo_d_01 WHERE pat_id=" + _PAT_ID + " AND dat_15='" + sINFO_DATE + "' ";
            //dt = new DataTable();
            //dt = db.Query(sSQL);
            //if (dt.Rows.Count > 0)
            //{
            //    //GOT=AST
            //    if (num_50.Text == "")
            //        num_50.Text = dt.Rows[0]["num_22"].ToString();
            //    //GPT=ALT
            //    if (num_51.Text == "")
            //        num_51.Text = dt.Rows[0]["num_23"].ToString();

            //}
            //dt.Dispose();
        }
    }
}