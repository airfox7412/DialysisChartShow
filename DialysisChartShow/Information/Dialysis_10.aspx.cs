using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Dialysis_Chart_Show.tools;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Xml.Xsl;
using System.Xml;
using NLog;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Dialysis_Chart_Show.Information
{
    public class Item
    {
        public string RESULT_DATE
        {
            get;
            set;
        }
        public double RESULT_VALUE_N
        {
            get;
            set;
        }
        public double RESULT_VALUE_L
        {
            get;
            set;
        }
        public double RESULT_VALUE_H
        {
            get;
            set;
        }
    }

    public partial class Dialysis_10 : BaseForm
    {
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        private static Logger logger = LogManager.GetCurrentClassLogger();
        DataTable dtFhir = new DataTable();
        DataRow drN;
        public string Trdate;
        public string TrTypes;
        //private static DataTable dt_ritem_setup;
        //private static DataTable dt_ritem_setup12;
        //private static DateTime d_ritem_setup;
        private string sURL = "";
        private string sCODE = "";
        //DialysisClientUpLoad DialysisToFhir = new DialysisClientUpLoad();

        private void SetNode(Ext.Net.Node nNode, string sText, string sNodeID, Ext.Net.Node nFather)
        {
            nNode.Text = sText;
            nNode.NodeID = sNodeID;
            nNode.Cls = "large-font";
            nFather.Children.Add(nNode);
        }

        private void AddNode(Ext.Net.Node nf, string sText, string sNodeID)
        {
            Ext.Net.Node rN = new Ext.Net.Node()
            {
                Text = sText,
                NodeID = sNodeID,
                Icon = Icon.Report,
                Cls = "large-font",
                Leaf = true
            };
            nf.Children.Add(rN);
        }

        private void rootChildrenAdd(Ext.Net.Node root, string sText)
        {
            string Nodeid=sText.Substring(0, 1);

            Ext.Net.Node rNode = new Ext.Net.Node();
            rNode.Text = sText;
            rNode.NodeID = Nodeid + "_";
            rNode.Cls = "large-font";
            rNode.Icon = Icon.Folder;

            if (Nodeid == "A")
            {
                AddNode(rNode, "统计分析", "A0");
                AddNode(rNode, "基本病患统计", "A1");
                AddNode(rNode, "血透年龄统计", "A2");
                //AddNode(rA, "护士工作量统计", "../report/Report_Dialysis_h.aspx?_REPORT_NAME=8", "A4");
                AddNode(rNode, "血透中出现症状统计", "A5");
            }
            else if (Nodeid == "B")
            {
                Ext.Net.Node rBx = new Ext.Net.Node();
                SetNode(rBx, "1.实验室检查", "Bx", rNode);
                
                AddNode(rBx, "1.血红蛋白Hb", "B8");      //4003
                AddNode(rBx, "2.白蛋白ALB", "B3");       //4008
                AddNode(rBx, "3.钙Ca", "B21");           //4021
                AddNode(rBx, "4.磷P", "B22");            //4023
                AddNode(rBx, "5.转铁蛋白饱和度", "B23"); //4050
                AddNode(rBx, "6.铁蛋白", "B24");         //4027
                AddNode(rBx, "7.iPTH", "B25");           //4030
                AddNode(rBx, "8.kt/v", "B1");            //5018

                AddNode(rNode, "2.住院率", "B10");
                AddNode(rNode, "3.死亡率", "B11");
                AddNode(rNode, "4.HBsAg转阳率", "B13");
                AddNode(rNode, "5.antiHCV转阳率", "B14");
            }
            else if (Nodeid == "E")
            {
                AddNode(rNode, "1.年度评鉴质量指标", "E01");
                AddNode(rNode, "2.月度质量指标", "E02");
            }
            else if (Nodeid == "F" || Nodeid == "H")
            {
                rNode.Icon = Icon.Page;
                rNode.Leaf = true;
            }
            else
            {
                rNode.Icon = Icon.ApplicationForm;
                rNode.Leaf = true;
            }
            root.Children.Add(rNode);
        }

        #region 取得樹狀目錄1
        private void BuildTree1()
        {
            string sSQL = "";
            string W_QUALITY = "";
            sSQL = "SELECT * FROM general_setup WHERE genst_ctg='QUALITY' AND genst_desc='Y'";
            DataTable dtCODE = db.Query(sSQL);
            if (dtCODE.Rows.Count > 0)
            {
                W_QUALITY = "Y";
            }

            TreePanel1.ID = "TreePanel1";
            TreePanel1.Height = Unit.Pixel(650);
            TreePanel1.AutoScroll = true;

            Ext.Net.Button btnExpand = new Ext.Net.Button();
            btnExpand.Text = "展开";
            btnExpand.Listeners.Click.Handler = TreePanel1.ClientID + ".expandAll();";

            Ext.Net.Button btnCollapse = new Ext.Net.Button();
            btnCollapse.Text = "收合";
            btnCollapse.Listeners.Click.Handler = TreePanel1.ClientID + ".collapseAll();";

            Toolbar toolBar = new Toolbar();
            toolBar.ID = "ToolbarT";
            toolBar.Items.Add(btnExpand);
            toolBar.Items.Add(btnCollapse);
            TreePanel1.TopBar.Add(toolBar);

            StatusBar statusBar1 = new StatusBar();
            statusBar1.ID = "StatusBarT";
            statusBar1.AutoClear = 1000;
            TreePanel1.BottomBar.Add(statusBar1);
            TreePanel1.Listeners.ItemClick.Handler = statusBar1.ClientID + ".setStatus({text: '点选: <b>' + record.data.text + '</b>', clear: false});";

            Ext.Net.Node root = new Ext.Net.Node();
            root.Text = "质量分析";
            root.Icon = Icon.ReportUser;
            root.NodeID = "__";
            root.Cls = "large-font";

            rootChildrenAdd(root, "A.质量管理基础数据");
            rootChildrenAdd(root, "B.质量指标数据");
            rootChildrenAdd(root, "C.自订条件查询");
            rootChildrenAdd(root, "D.单个患者");
            rootChildrenAdd(root, "E.评鉴质量指标");

            if (W_QUALITY == "Y")
            {
                rootChildrenAdd(root, "F.品质监控指标");
                rootChildrenAdd(root, "G.品质监控指标查詢");
                rootChildrenAdd(root, "H.血液透析化验数据表");
            }

            Ext.Net.Node rI = new Ext.Net.Node();
            if (Hospital == "Hospital_Alasamo")
            {
                rootChildrenAdd(root, "I.血透月报表");
            }

            rootChildrenAdd(root, "J.血透管路及人次统计");
            rootChildrenAdd(root, "K.自订条件统计");
            root.Expanded = true;

            TreePanel1.Root.Add(root);
            //TreePanel1.ExpandAll();
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //if (dt_ritem_setup == null)
                //{
                //    GetGroupName();
                //    d_ritem_setup = DateTime.Now;
                //}
                //else
                //{
                //    TimeSpan iDiff = new TimeSpan(DateTime.Now.Ticks - d_ritem_setup.Ticks);
                //    if (iDiff.TotalSeconds > 120)
                //    {
                //        GetGroupName();
                //        d_ritem_setup = DateTime.Now;
                //    }
                //}

                //this.txtGridPanel7.Hidden = true;
                DateTime dBEG = DateTime.Now;
                DateTime dEND = DateTime.Now;
                dBEG = Convert.ToDateTime(dBEG.ToString("yyyy-MM-") + "01");
                dEND = dBEG;
                dEND = dEND.AddMonths(1);
                dEND = dEND.AddDays(-1);
                this.beg_date.Text = dBEG.ToString("yyyy-MM-dd");
                this.end_date.Text = dEND.ToString("yyyy-MM-dd");

                if (Session["sBEG_DATE"] != null)
                    beg_date.Text = Session["sBEG_DATE"].ToString();
                if (Session["sEND_DATE"] != null)
                    end_date.Text = Session["sEND_DATE"].ToString();
                
                this.txtNODE_ID.Hidden = true;
                this.txtNODE_TEXT.Hidden = true;

                BuildTree1();
            }
        }

        protected void Node_Click(object sender, DirectEventArgs e)
        {
            txtNODE_ID.Text = e.ExtraParams["rID"];
            txtNODE_TEXT.Text = e.ExtraParams["rTEXT"];
            Session["sBEG_DATE"] = beg_date.Text;
            Session["sEND_DATE"] = end_date.Text;
            sURL = "";
            sCODE = "";
            switch (this.txtNODE_ID.Text)
            {
                case "F_": //F.品质监控指标
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=98";
                    break;
                case "C_": //自订条件查询
                    sURL = "Dialysis_10_C01.aspx";
                    break;
                case "D_": //单个患者
                    sURL = "Dialysis_10_D01.aspx";
                    break;
                case "G_": //監控指標查询
                    sURL = "Dialysis_10_G01.aspx";
                    break;
                case "H_": //H.血液透析化验数据表
                    PatInfo_Query();
                    break;
                case "I_": //I.血透月报表
                    sURL = "Dialysis_10_I_Alasamo_List.aspx";
                    break;
                case "J_": //I.血透月报表
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=J01&_BEG_DATE=" + _Get_YMD2(beg_date.Text) + "&_END_DATE=" + _Get_YMD2(end_date.Text);
                    break;
                case "K_": //K.自订条件统计
                    sURL = "Dialysis_10_K01.aspx";
                    break;
                case "A0"://统计分析
                    sURL = "../report/Rpt_View_Dialysis.aspx";
                    break;
                case "A1"://基本病患统计
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=5";
                    break;
                case "A2"://血透年龄统计
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=10";
                    break;
                case "A3"://穿刺次数(每日)
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=7&_BEG_DATE=" + _Get_YMD2(beg_date.Text) + "&_END_DATE=" + _Get_YMD2(end_date.Text);
                    break;
                case "A4"://穿刺次数(小计)
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=8&_BEG_DATE=" + _Get_YMD2(beg_date.Text) + "&_END_DATE=" + _Get_YMD2(end_date.Text);
                    break;
                case "A5"://血透中出现症状症状统计
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=9&_BEG_DATE=" + _Get_YMD2(beg_date.Text) + "&_END_DATE=" + _Get_YMD2(end_date.Text);
                    break;
                case "B1": //1.Kt/V检查比率
                    sCODE = "5018";
                    break;
                case "B2": //2.平均透析效率
                    sCODE = "5018";
                    break;
                case "B3": //3.Albumin检查比率
                    sCODE = "4008";
                    break;
                case "B4": //4.平均Albumin值
                    sCODE = "4008";
                    break;
                case "B5": //1.Hct检查比率
                    sCODE = "4004";
                    break;
                case "B8": //8.Hb检查比率
                    sCODE = "4003";
                    break;
                case "B9": //9.平均Hb值
                    sCODE = "4003";
                    break;
                case "B10": //住院率                      
                    sURL = "Dialysis_10_B10.aspx";
                    break;
                case "B11": //死亡率
                    sURL = "Dialysis_10_B11.aspx";
                    break;
                case "B13": //HBsAg转阳率
                    Session["sCODE"] = "4032";
                    sURL = "Dialysis_10_B13.aspx";
                    break;
                case "B14": //antiHCV转阳率
                    Session["sCODE"] = "4033";
                    sURL = "Dialysis_10_B13.aspx";
                    break;
                case "B21": //钙Ca
                    sCODE = "4021";
                    break;
                case "B22": //磷P
                    sCODE = "4023";
                    break;
                case "B23": //转铁蛋白饱和度
                    sCODE = "4050";
                    break;
                case "B24": //铁蛋白
                    sCODE = "4027";
                    break;
                case "B25": //IPTH
                    sCODE = "4030";
                    break;
                case "E01": //评鉴质量指标
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=E01&_INFO_DATE=" + _Get_Cht_YMD(DateTime.Now.ToString());
                    break;
                case "E02": //月指標查询
                    sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=E02&_INFO_DATE=" + _Get_YMD2(beg_date.Text);
                    break;
                //case "B7": //瘘管重建率
                //    this.Panel_3.Hidden = false;
                //    btn_Query3_Click(sender, e);
                //    break;
                //case "B15": //3.转归率
                //    this.Panel_7.Hidden = false;
                //    btn_Query7_Click(sender, e);
                //    break;
                //case "B16": //有症状
                //    this.Panel_5.Hidden = false;
                //    btn_Query5_Click(sender, e);
                //    break;
                //case "C11": //4.死亡率
                //    this.Panel_6.Hidden = false;
                //    btn_Query6_Click(sender, e);
                //    break;
                //case "C7": //1.瘘管重建率
                //    this.Panel_6.Hidden = false;
                //    btn_Query6_Click(sender, e);
                //    break;
                //case "C10": //1.住院率
                //    this.Panel_6.Hidden = false;
                //    btn_Query6_Click(sender, e);
                //    break;
                //case "C16": //有症状
                //    this.Panel_6.Hidden = false;
                //    btn_Query6_Click(sender, e);
                //    break;
                //case "C3":
                //    this.Panel_8.Hidden = false;
                //    this.GridPanel9.Title = "每月统计 - Albumin检查";
                //    this.txtRESULT_CODE2.Text = "4008";
                //    btn_Query8_Click(sender, e);
                //    break;
                //case "C8":
                //    this.Panel_8.Hidden = false;
                //    this.GridPanel9.Title = "每月统计 - Hb检查";
                //    this.txtRESULT_CODE2.Text = "4003";
                //    btn_Query8_Click(sender, e);
                //    break;
                //case "C1":
                //    this.Panel_8.Hidden = false;
                //    this.GridPanel9.Title = "每月统计 - Kt/V检查";
                //    this.txtRESULT_CODE2.Text = "5018";
                //    btn_Query8_Click(sender, e);
                //    break;
                //case "C15":
                //    this.Panel_8.Hidden = false;
                //    this.GridPanel9.Title = "每月统计 - Hct检查";
                //    this.txtRESULT_CODE2.Text = "4004";
                //    btn_Query8_Click(sender, e);
                //    break;
                //case "C13":
                //    this.Panel_10.Hidden = false;
                //    this.txtRESULT_CODE4.Text = "4032";
                //    btn_Query10_Click(sender, e);
                //    Session["sCODE"] = "4032";
                //    break;
                //case "C14":
                //    //this.Panel_10.Hidden = false;
                //    //this.txtRESULT_CODE4.Text = "4033";
                //    //btn_Query10_Click(sender, e);
                //    break;
                //case "D8": //血红蛋白Hb
                //    this.txtRESULT_CODE12.Text = "4003";
                //    this.Panel_12.Hidden = false;
                //    btn_Query12_Click(sender, e);
                //    break;
                //case "D3": //白蛋白ALB
                //    this.txtRESULT_CODE12.Text = "4008";
                //    this.Panel_12.Hidden = false;
                //    btn_Query12_Click(sender, e);
                //    break;
                //case "D21": //钙Ca
                //    this.txtRESULT_CODE12.Text = "4021";
                //    this.Panel_12.Hidden = false;
                //    btn_Query12_Click(sender, e);
                //    break;
                //case "D22": //磷P
                //    this.txtRESULT_CODE12.Text = "4023";
                //    this.Panel_12.Hidden = false;
                //    btn_Query12_Click(sender, e);
                //    break;
                //case "D23": //转铁蛋白饱和度
                //    this.txtRESULT_CODE12.Text = "4050";
                //    this.Panel_12.Hidden = false;
                //    btn_Query12_Click(sender, e);
                //    break;
                //case "D24": //铁蛋白
                //    this.txtRESULT_CODE12.Text = "4027";
                //    this.Panel_12.Hidden = false;
                //    btn_Query12_Click(sender, e);
                //    break;
                //case "D25": //IPTH
                //    this.txtRESULT_CODE12.Text = "4030";
                //    this.Panel_12.Hidden = false;
                //    btn_Query12_Click(sender, e);
                //    break;
                default:
                    break;
            }
            //this.txtRESULT_CODE.Text = sCODE;
            if (sCODE != "") //實驗室檢查
            {
                Session["sCODE"] = sCODE;
                Panel_Center.Hidden = false;
                Panel_Center.Loader.SuspendScripting();
                Panel_Center.Loader.Url = "Dialysis_10_B01.aspx";
                Panel_Center.Loader.DisableCaching = true;
                Panel_Center.LoadContent();
            }
            else
            {
                Panel_Center.Hidden = false;
                Panel_Center.Show();
                Panel_Center.Loader.SuspendScripting();
                Panel_Center.Loader.Url = sURL;
                Panel_Center.Loader.DisableCaching = true;
                Panel_Center.LoadContent();
            }
        }

        //protected void btn_Query3_Click(object sender, DirectEventArgs e)
        //{
        //    //瘘管重建
        //    string sBEG_DATE = _Get_YMD2(beg_date.Text);
        //    string sEND_DATE = _Get_YMD2(end_date.Text);
        //    string sSQL = "";

        //    if (sBEG_DATE == "")
        //        sBEG_DATE = "2000-01-01";
        //    if (sEND_DATE == "")
        //        sEND_DATE = "9999-12-31";
        //    //找受檢人
        //    sSQL = "SELECT A.pv_ic, A.pv_datevisit " +
        //             "FROM pat_visit A " +
        //            "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //              "AND A.pv_datevisit<='" + sEND_DATE + "' ";
        //    DataTable dtTOTAL3 = db.Query(sSQL);
        //    this.txtTOTAL3.Text = dtTOTAL3.Rows.Count.ToString();

        //    //找瘘管重建的人
        //    sSQL = "SELECT B.pif_name, B.pif_sex, B.pif_ic, A.pat_id AS PAT_NO, " +
        //                  "A.info_date AS HD_DATE, A.txt_10 AS HD_CAUSE, A.opt_9 " +
        //             "FROM zinfo_e_02 A " +
        //             "LEFT JOIN pat_info B " +
        //               "ON A.pat_id=B.pif_id " +
        //            "WHERE A.info_date>='" + sBEG_DATE + "' " +
        //              "AND A.info_date<='" + sEND_DATE + "' " +
        //              "AND A.opt_8=1 ";
        //    DataTable dtHD = db.Query(sSQL);
        //    this.txtHD.Text = dtHD.Rows.Count.ToString();
        //    for (int i = 0; i < dtHD.Rows.Count; i++)
        //    {
        //        switch (dtHD.Rows[i]["opt_9"].ToString())
        //        {
        //            case "1":
        //                dtHD.Rows[i]["HD_CAUSE"] = "导管感染";
        //                break;
        //            case "2":
        //                dtHD.Rows[i]["HD_CAUSE"] = "内瘘阻塞";
        //                break;
        //            case "3":
        //                dtHD.Rows[i]["HD_CAUSE"] = "血流量过小(内瘘狭窄)";
        //                break;
        //            case "4":
        //                dtHD.Rows[i]["HD_CAUSE"] = "血流量过大(内瘘成熟)";
        //                break;
        //            case "5":
        //                dtHD.Rows[i]["HD_CAUSE"] = "长期导管移位";
        //                break;
        //            case "6":
        //                dtHD.Rows[i]["HD_CAUSE"] = "窃流症候群";
        //                break;
        //        }
        //        switch (dtHD.Rows[i]["pif_sex"].ToString().Trim())
        //        {
        //            case "F":
        //                dtHD.Rows[i]["pif_sex"] = "女";
        //                break;
        //            case "M":
        //                dtHD.Rows[i]["pif_sex"] = "男";
        //                break;
        //        }
        //    }
        //    Store istore1 = this.GridPanel4.GetStore();
        //    istore1.DataSource = db.GetDataArray_AddRowNum(dtHD);
        //    istore1.DataBind();

        //    if (this.txtTOTAL3.Text == "0")
        //    {
        //        this.txtHD_P.Text = "0";
        //    }
        //    else
        //    {
        //        this.txtHD_P.Text = Percent(Convert.ToDouble(this.txtHD.Text) / Convert.ToDouble(this.txtTOTAL3.Text) * 100);
        //    }
        //}

        //protected void btn_Query5_Click(object sender, DirectEventArgs e)
        //{
        //    //有症状
        //    string sBEG_DATE = _Get_YMD2(beg_date.Text);
        //    string sEND_DATE = _Get_YMD2(end_date.Text);
        //    string sSQL = "";

        //    if (sBEG_DATE == "")
        //        sBEG_DATE = "2000-01-01";
        //    if (sEND_DATE == "")
        //        sEND_DATE = "9999-12-31";
        //    //找受檢人
        //    sSQL = "SELECT A.pv_ic, A.pv_datevisit " +
        //             "FROM pat_visit A " +
        //            "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //              "AND A.pv_datevisit<='" + sEND_DATE + "' ";
        //    DataTable dtTOTAL5 = db.Query(sSQL);
        //    this.txtTOTAL5.Text = dtTOTAL5.Rows.Count.ToString();

        //    //找有症状的人
        //    sSQL = "SELECT B.pif_name, B.pif_sex, B.pif_ic, B.pif_id AS PAT_NO, " +
        //                  "A.cln3_date AS HOSP_DATE " +
        //             "FROM clinical3_nurse A " +
        //             "LEFT JOIN pat_info B " +
        //               "ON A.cln3_patic=B.pif_ic " +
        //            "WHERE A.cln3_date>='" + sBEG_DATE + "' " +
        //              "AND A.cln3_date<='" + sEND_DATE + "' " +
        //              "AND A.cln3_yn='Y' ";
        //    DataTable dtSYMPTON = db.Query(sSQL);
        //    this.txtSYMPTON.Text = dtSYMPTON.Rows.Count.ToString();
        //    for (int i = 0; i < dtSYMPTON.Rows.Count; i++)
        //    {
        //        switch (dtSYMPTON.Rows[i]["pif_sex"].ToString().Trim())
        //        {
        //            case "F":
        //                dtSYMPTON.Rows[i]["pif_sex"] = "女";
        //                break;
        //            case "M":
        //                dtSYMPTON.Rows[i]["pif_sex"] = "男";
        //                break;
        //        }
        //    }
        //    Store istore1 = this.GridPanel6.GetStore();
        //    istore1.DataSource = db.GetDataArray_AddRowNum(dtSYMPTON);
        //    istore1.DataBind();

        //    if (this.txtTOTAL5.Text == "0")
        //    {
        //        this.txtSYMPTON_P.Text = "0";
        //    }
        //    else
        //    {
        //        this.txtSYMPTON_P.Text = Percent(Convert.ToDouble(this.txtSYMPTON.Text) / Convert.ToDouble(this.txtTOTAL5.Text) * 100);
        //    }
        //}

        //protected void btn_Query6_Click(object sender, DirectEventArgs e)
        //{
        //    //有症状
        //    string sBEG_DATE = _Get_YMD2(beg_date.Text);
        //    string sEND_DATE = _Get_YMD2(end_date.Text);
        //    string sSQL = "";

        //    if (sBEG_DATE == "")
        //        sBEG_DATE = "2000-01-01";
        //    if (sEND_DATE == "")
        //        sEND_DATE = "9999-12-31";
        //    //找受檢人
        //    sSQL = "SELECT SUBSTR(A.pv_datevisit,1,7) AS Y_M, COUNT(*) AS TOTAL, 0 AS ERROR, '0' AS ERROR_P " +
        //             "FROM pat_visit A " +
        //            "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //              "AND A.pv_datevisit<='" + sEND_DATE + "' " +
        //            "GROUP BY SUBSTR(A.pv_datevisit,1,7) ";
        //    DataTable dtTOTAL6 = db.Query(sSQL);
        //    this.txtTOTAL6.Text = dtTOTAL6.Rows.Count.ToString();

        //    switch (this.txtNODE_ID.Text)
        //    {
        //        case "C11": //4.死亡率
        //            this.GridPanel7.Title = "每月统计 - 死亡率";
        //            this.txtGridPanel7.Text = "每月统计 - 死亡率";
        //            sSQL = "SELECT SUBSTR(A.info_date,1,7) AS Y_M, COUNT(*) AS ERROR " +
        //                     "FROM zinfo_e_01 A " +
        //                    "WHERE A.info_date>='" + sBEG_DATE + "' " +
        //                      "AND A.info_date<='" + sEND_DATE + "' " +
        //                      "AND A.opt_52=4 " +
        //                    "GROUP BY SUBSTR(A.info_date,1,7) ";
        //            break;
        //        case "C7": //1.瘘管重建率
        //            this.GridPanel7.Title = "每月统计 - 瘘管重建率";
        //            this.txtGridPanel7.Text = "每月统计 - 瘘管重建率";
        //            sSQL = "SELECT SUBSTR(A.info_date,1,7) AS Y_M, COUNT(*) AS ERROR " +
        //                     "FROM zinfo_e_02 A " +
        //                    "WHERE A.info_date>='" + sBEG_DATE + "' " +
        //                      "AND A.info_date<='" + sEND_DATE + "' " +
        //                      "AND A.opt_8=1 " +
        //                    "GROUP BY SUBSTR(A.info_date,1,7) ";
        //            break;
        //        case "C10": //1.住院率
        //            this.GridPanel7.Title = "每月统计 - 住院率";
        //            this.txtGridPanel7.Text = "每月统计 - 住院率";
        //            sSQL = "SELECT SUBSTR(A.info_date,1,7) AS Y_M, COUNT(*) AS ERROR " +
        //                     "FROM zinfo_e_01 A " +
        //                    "WHERE A.info_date>='" + sBEG_DATE + "' " +
        //                      "AND A.info_date<='" + sEND_DATE + "' " +
        //                      "AND A.opt_26=1 " +
        //                    "GROUP BY SUBSTR(A.info_date,1,7) ";
        //            break;
        //        case "C16": //有症状
        //            this.GridPanel7.Title = "每月统计 - 血透中有症状";
        //            this.txtGridPanel7.Text = "每月统计 - 血透中有症状";
        //            sSQL = "SELECT SUBSTR(A.cln3_date,1,7) AS Y_M, COUNT(*) AS ERROR " +
        //                     "FROM clinical3_nurse A " +
        //                    "WHERE A.cln3_date>='" + sBEG_DATE + "' " +
        //                      "AND A.cln3_date<='" + sEND_DATE + "' " +
        //                      "AND A.cln3_yn='Y' " +
        //                    "GROUP BY SUBSTR(A.cln3_date,1,7) ";
        //            break;
        //        default:
        //            break;

        //    }
        //    if (sSQL != "")
        //    {
        //        DataTable dtSYMPTON2 = db.Query(sSQL);
        //        this.txtSYMPTON2.Text = dtSYMPTON2.Rows.Count.ToString();

        //        System.Data.DataView dvTOTAL6;
        //        dvTOTAL6 = dtTOTAL6.DefaultView;
        //        for (int i = 0; i < dtSYMPTON2.Rows.Count; i++)
        //        {
        //            dvTOTAL6.RowFilter = "Y_M='" + dtSYMPTON2.Rows[i]["Y_M"].ToString() + "' ";
        //            if (dvTOTAL6.Count > 0)
        //            {
        //                dvTOTAL6[0]["ERROR"] = dtSYMPTON2.Rows[i]["ERROR"];
        //                if (Convert.ToInt16(dvTOTAL6[0]["TOTAL"]) != 0)
        //                    dvTOTAL6[0]["ERROR_P"] = Percent(Convert.ToDouble(dtSYMPTON2.Rows[i]["ERROR"]) * 100 / Convert.ToDouble(dvTOTAL6[0]["TOTAL"]));
        //            }
        //        }
        //        Store istore1 = this.GridPanel7.GetStore();
        //        istore1.DataSource = db.GetDataArray_AddRowNum(dtTOTAL6);
        //        istore1.DataBind();

        //        if (this.txtTOTAL6.Text == "0")
        //        {
        //            this.txtSYMPTON2_P.Text = "0";
        //        }
        //        else
        //        {
        //            this.txtSYMPTON2_P.Text = Percent(Convert.ToDouble(this.txtSYMPTON2.Text) / Convert.ToDouble(this.txtTOTAL6.Text) * 100);
        //        }
        //    }
        //}

        //protected void btn_Query7_Click(object sender, DirectEventArgs e)
        //{
        //    //轉歸率
        //    string sBEG_DATE = _Get_YMD2(beg_date.Text);
        //    string sEND_DATE = _Get_YMD2(end_date.Text);
        //    string sSQL = "";

        //    if (sBEG_DATE == "")
        //        sBEG_DATE = "2000-01-01";
        //    if (sEND_DATE == "")
        //        sEND_DATE = "9999-12-31";
        //    //找受檢人
        //    sSQL = "SELECT DISTINCT A.pv_ic, A.pv_datevisit " +
        //             "FROM pat_visit A " +
        //            "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //              "AND A.pv_datevisit<='" + sEND_DATE + "' ";
        //    sSQL = "SELECT SUBSTR(A.pv_datevisit,1,7) AS Y_M, COUNT(*) AS TOTAL, 0 AS ERROR, '0' AS ERROR_P " +
        //             "FROM pat_visit A " +
        //            "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //              "AND A.pv_datevisit<='" + sEND_DATE + "' " +
        //            "GROUP BY SUBSTR(A.pv_datevisit,1,7) ";
        //    DataTable dtTOTAL7 = db.Query(sSQL);
        //    this.txtTOTAL7.Text = dtTOTAL7.Rows.Count.ToString();

        //    //找轉歸的人
        //    sSQL = "SELECT DISTINCT SUBSTR(A.info_date,1,7) AS Y_M, COUNT(G.opt_52) AS TOTAL, " +
        //                  "COUNT(B.opt_52) AS TYPE1, '0' AS TYPE1_P, " +
        //                  "COUNT(C.opt_52) AS TYPE2, '0' AS TYPE2_P, " +
        //                  "COUNT(D.opt_52) AS TYPE3, '0' AS TYPE3_P, " +
        //                  "COUNT(E.opt_52) AS TYPE4, '0' AS TYPE4_P, " +
        //                  "COUNT(F.opt_52) AS TYPE5, '0' AS TYPE5_P " +
        //                     "FROM zinfo_e_01 A " +
        //                     "LEFT JOIN zinfo_e_01 B " +
        //                       "ON A.pat_id=B.pat_id " +
        //                      "AND A.info_date=B.info_date " +
        //                      "AND B.opt_52=1 " +
        //                     "LEFT JOIN zinfo_e_01 C " +
        //                       "ON A.pat_id=C.pat_id " +
        //                      "AND A.info_date=C.info_date " +
        //                      "AND C.opt_52=2 " +
        //                     "LEFT JOIN zinfo_e_01 D " +
        //                       "ON A.pat_id=D.pat_id " +
        //                      "AND A.info_date=D.info_date " +
        //                      "AND D.opt_52=3 " +
        //                     "LEFT JOIN zinfo_e_01 E " +
        //                       "ON A.pat_id=E.pat_id " +
        //                      "AND A.info_date=E.info_date " +
        //                      "AND E.opt_52=4 " +
        //                     "LEFT JOIN zinfo_e_01 F " +
        //                       "ON A.pat_id=B.pat_id " +
        //                      "AND A.info_date=F.info_date " +
        //                      "AND F.opt_52=5 " +
        //                     "LEFT JOIN zinfo_e_01 G " +
        //                       "ON A.pat_id=G.pat_id " +
        //                      "AND A.info_date=G.info_date " +
        //                      "AND G.opt_52<>0 " +
        //                    "WHERE A.info_date>='" + sBEG_DATE + "' " +
        //                      "AND A.info_date<='" + sEND_DATE + "' " +
        //                    "GROUP BY SUBSTR(A.info_date,1,7) ";

        //    DataTable dtCHANGE = db.Query(sSQL);
        //    this.txtCHANGE.Text = dtCHANGE.Rows.Count.ToString();
        //    for (int i = 0; i < dtCHANGE.Rows.Count; i++)
        //    {
        //        if (Convert.ToInt16(dtCHANGE.Rows[i]["TOTAL"]) != 0)
        //        {
        //            dtCHANGE.Rows[i]["TYPE1_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE1"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
        //            dtCHANGE.Rows[i]["TYPE2_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE2"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
        //            dtCHANGE.Rows[i]["TYPE3_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE3"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
        //            dtCHANGE.Rows[i]["TYPE4_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE4"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
        //            dtCHANGE.Rows[i]["TYPE5_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE5"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
        //        }
        //        else
        //        {
        //            dtCHANGE.Rows[i]["TYPE1_P"] = "";
        //            dtCHANGE.Rows[i]["TYPE2_P"] = "";
        //            dtCHANGE.Rows[i]["TYPE3_P"] = "";
        //            dtCHANGE.Rows[i]["TYPE4_P"] = "";
        //            dtCHANGE.Rows[i]["TYPE5_P"] = "";
        //        }
        //    }
        //    Store istore1 = this.GridPanel8.GetStore();
        //    istore1.DataSource = db.GetDataArray_AddRowNum(dtCHANGE);
        //    istore1.DataBind();

        //    if (this.txtTOTAL7.Text == "0")
        //    {
        //        this.txtCHANGE_P.Text = "0";
        //    }
        //    else
        //    {
        //        this.txtCHANGE_P.Text = Percent(Convert.ToDouble(this.txtCHANGE.Text) / Convert.ToDouble(this.txtTOTAL7.Text) * 100);
        //    }
        //}

        //protected void btn_Query8_Click(object sender, DirectEventArgs e)
        //{
        //    //
        //    string sBEG_DATE = _Get_YMD2(beg_date.Text);
        //    string sEND_DATE = _Get_YMD2(end_date.Text);
        //    string sSQL = "";
        //    string sRESULT_CODE = this.txtRESULT_CODE2.Text;
        //    int iCHECK;
        //    int iCHECK_Y;
        //    int iCHECK_N;

        //    if (sRESULT_CODE != "")
        //    {
        //        if (sBEG_DATE == "")
        //            sBEG_DATE = "2000-01-01";
        //        if (sEND_DATE == "")
        //            sEND_DATE = "9999-12-31";
        //        //找檢查項目
        //        sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
        //        DataTable dtCODE2 = db.Query(sSQL);
        //        if (dtCODE2.Rows.Count > 0)
        //        {
        //            this.txtRESULT_NAME2.Text = dtCODE2.Rows[0]["RITEM_NAME"].ToString();
        //            this.txtRESULT_UNIT2.Text = dtCODE2.Rows[0]["RITEM_UNIT"].ToString();
        //            this.txtNORMAL2.Text = dtCODE2.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE2.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE2.Rows[0]["RITEM_UNIT"].ToString();
        //        }

        //        //找受檢人
        //        sSQL = "SELECT SUBSTR(A.pv_datevisit,1,7) AS Y_M, COUNT(*) AS TOTAL, " +
        //                      "0 AS CHECKS, '0' AS CHECKS_P, 0 AS UNCHECK, '0' AS UNCHECK_P, " +
        //                      "0 AS CHECK_Y, '0' AS CHECK_Y_P, 0 AS CHECK_N, '0' AS CHECK_N_P " +
        //                 "FROM pat_visit A " +
        //                "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //                  "AND A.pv_datevisit<='" + sEND_DATE + "' " +
        //                "GROUP BY SUBSTR(A.pv_datevisit,1,7) ";
        //        DataTable dtTOTAL8 = db.Query(sSQL);
        //        this.txtTOTAL8.Text = dtTOTAL8.Rows.Count.ToString();

        //        //找有做檢查的人
        //        sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N, SUBSTR(A.RESULT_DATE,1,7) AS Y_M " +
        //                 "FROM a_result_log A " +
        //                 "LEFT JOIN pat_info B " +
        //                   "ON A.PAT_NO=B.pif_id " +
        //                "WHERE A.RESULT_VER=0 " +
        //                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
        //                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
        //                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
        //                "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic, Y_M ";
        //        DataTable dtCHECK2 = db.Query(sSQL);

        //        System.Data.DataView dvCHECK2;
        //        dvCHECK2 = dtCHECK2.DefaultView;
                
        //        for (int i = 0; i < dtTOTAL8.Rows.Count; i++)
        //        {
        //            //算受檢人數
        //            dvCHECK2.RowFilter = "Y_M='" + dtTOTAL8.Rows[i]["Y_M"].ToString() + "' ";
        //            iCHECK = dvCHECK2.Count;
        //            dtTOTAL8.Rows[i]["CHECKS"] = iCHECK;
        //            dtTOTAL8.Rows[i]["UNCHECK"] = Convert.ToInt16(dtTOTAL8.Rows[i]["TOTAL"]) - iCHECK;

        //            //算合格人數
        //            dvCHECK2.RowFilter = "Y_M='" + dtTOTAL8.Rows[i]["Y_M"].ToString() + "' " +
        //                            " AND RESULT_VALUE_N>=" + dtCODE2.Rows[0]["RITEM_LOW1"].ToString() +
        //                            " AND RESULT_VALUE_N<=" + dtCODE2.Rows[0]["RITEM_HIGH1"].ToString();
        //            iCHECK_Y = dvCHECK2.Count;
        //            dtTOTAL8.Rows[i]["CHECK_Y"] = iCHECK_Y;

        //            //算不合格人數
        //            dvCHECK2.RowFilter = "Y_M='" + dtTOTAL8.Rows[i]["Y_M"].ToString() + "' " +
        //                            " AND (RESULT_VALUE_N<" + dtCODE2.Rows[0]["RITEM_LOW1"].ToString() +
        //                             " OR RESULT_VALUE_N>" + dtCODE2.Rows[0]["RITEM_HIGH1"].ToString() + ") ";
        //            iCHECK_N = dvCHECK2.Count;
        //            dtTOTAL8.Rows[i]["CHECK_N"] = iCHECK_N;

        //            if (iCHECK != 0)
        //            {
        //                dtTOTAL8.Rows[i]["CHECK_Y_P"] = Percent(Convert.ToDouble(iCHECK_Y) * 100 / Convert.ToDouble(iCHECK)) + "%";
        //                dtTOTAL8.Rows[i]["CHECK_N_P"] = Percent(Convert.ToDouble(iCHECK_N) * 100 / Convert.ToDouble(iCHECK)) + "%";
        //            }
        //            if (Convert.ToInt16(dtTOTAL8.Rows[i]["TOTAL"]) != 0)
        //            {
        //                dtTOTAL8.Rows[i]["CHECKS_P"] = Percent(Convert.ToDouble(iCHECK) * 100 / Convert.ToDouble(dtTOTAL8.Rows[i]["TOTAL"])) + "%";
        //                dtTOTAL8.Rows[i]["UNCHECK_P"] = Percent(Convert.ToDouble(Convert.ToInt16(dtTOTAL8.Rows[i]["TOTAL"]) - iCHECK) * 100 / Convert.ToDouble(dtTOTAL8.Rows[i]["TOTAL"])) + "%";
        //            }

        //        }

        //        Store istore1 = this.GridPanel9.GetStore();
        //        istore1.DataSource = db.GetDataArray_AddRowNum(dtTOTAL8);
        //        istore1.DataBind();

        //    }
        //}

        //protected void btn_Query10_Click(object sender, DirectEventArgs e)
        //{
        //    //檢驗 HBsAg, AntiHCV
        //    string sBEG_DATE = _Get_YMD2(beg_date.Text);
        //    string sEND_DATE = _Get_YMD2(end_date.Text);
        //    string sSQL = "";
        //    string sRESULT_CODE = this.txtRESULT_CODE4.Text;
        //    if (sRESULT_CODE != "")
        //    {
        //        if (sBEG_DATE == "")
        //            sBEG_DATE = "2000-01-01";
        //        if (sEND_DATE == "")
        //            sEND_DATE = "9999-12-31";
        //        //找檢查項目
        //        sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
        //        DataTable dtCODE4 = db.Query(sSQL);
        //        if (dtCODE4.Rows.Count > 0)
        //        {
        //            this.txtRESULT_NAME4.Text = dtCODE4.Rows[0]["RITEM_NAME"].ToString();
        //            this.txtRESULT_UNIT4.Text = dtCODE4.Rows[0]["RITEM_UNIT"].ToString();
        //            this.txtNORMAL4.Text = dtCODE4.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE4.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE4.Rows[0]["RITEM_UNIT"].ToString();
        //        }

        //        //找有做檢查的人
        //        sSQL = "SELECT SUBSTR(A.RESULT_DATE,1,7) AS Y_M, COUNT(*) AS TOTAL, 0 AS ERROR, '0' AS ERROR_P " +
        //                 "FROM a_result_log A " +
        //                "WHERE A.RESULT_VER=0 " +
        //                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
        //                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
        //                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
        //                  "AND A.RESULT_VALUE_T IN ('阴性','阳性') " +
        //                "GROUP BY SUBSTR(A.RESULT_DATE,1,7) ";
        //        DataTable dtTOTAL10 = db.Query(sSQL);
        //        this.txtTOTAL10.Text = dtTOTAL10.Rows.Count.ToString();

        //        sSQL = "SELECT SUBSTR(A.RESULT_DATE,1,7) AS Y_M, COUNT(*) AS ERROR " +
        //                 "FROM a_result_log A " +
        //                "WHERE A.RESULT_VER=0 " +
        //                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
        //                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
        //                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
        //                  "AND A.RESULT_VALUE_T='" + "阳性" + "' " +
        //                "GROUP BY SUBSTR(A.RESULT_DATE,1,7) ";
        //        DataTable dtPOSITIVE2 = db.Query(sSQL);

        //        System.Data.DataView dvTOTAL10;
        //        dvTOTAL10 = dtTOTAL10.DefaultView;
        //        for (int i = 0; i < dtPOSITIVE2.Rows.Count; i++)
        //        {
        //            dvTOTAL10.RowFilter = "Y_M='" + dtPOSITIVE2.Rows[i]["Y_M"].ToString() + "' ";
        //            if (dvTOTAL10.Count > 0)
        //            {
        //                dvTOTAL10[0]["ERROR"] = dtPOSITIVE2.Rows[i]["ERROR"];
        //                if (Convert.ToInt16(dvTOTAL10[0]["TOTAL"]) != 0)
        //                    dvTOTAL10[0]["ERROR_P"] = Percent(Convert.ToDouble(dtPOSITIVE2.Rows[i]["ERROR"]) * 100 / Convert.ToDouble(dvTOTAL10[0]["TOTAL"]));
        //            }
        //        }

        //        Store istore1 = this.GridPanel11.GetStore();
        //        istore1.DataSource = db.GetDataArray_AddRowNum(dtTOTAL10);
        //        istore1.DataBind();
        //    }
        //}

        //private void GetAEinfo(string sURL)
        //{
        //    string AuditEventInfo, sUploadDT, sName;

        //    AuditEventInfo = GetHttpRequestString(sURL);
        //    ClassAuditEvent oAE = JsonConvert.DeserializeObject<ClassAuditEvent>(AuditEventInfo);
        //    var q = from p in oAE.@object
        //            select p;
        //    foreach (var ae in q)
        //    {
        //        drN = dtFhir.NewRow();
        //        sUploadDT = ae.identifier.value.ToString();
        //        sName = ae.name.ToString();
        //        drN["C1"] = sUploadDT.Substring(0, 4) + "-" + sUploadDT.Substring(4, 2) + "-" + sUploadDT.Substring(6, 2) + "　" + sUploadDT.Substring(8, 2) + ":" + sUploadDT.Substring(10, 2) + ":" + sUploadDT.Substring(12, 2);
        //        drN["C2"] = sName.Substring(0, 4) + "年" + sName.Substring(4, 2) + "月";
        //        drN["C3"] = sName.Substring(6);
        //        dtFhir.Rows.Add(drN);
        //        logger.Trace("identifier : " + ae.identifier.value.ToString());
        //        logger.Trace("reference : " + ae.reference.reference.ToString());
        //        logger.Trace("name : " + ae.name.ToString());
        //    }

        //}

        //private string GetHttpRequestString(string sURL)
        //{
        //    string strResult = string.Empty;
        //    HttpWebResponse httpWebResponse = null;
        //    StreamReader sr = null;

        //    try
        //    {
        //        HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sURL);
        //        httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
        //    }
        //    catch (Exception e)
        //    {
        //        logger.Error("Read RESTful Web Service error, error message: " + e.Message);
        //        return "-1";
        //    }

        //    if (httpWebResponse.StatusCode == HttpStatusCode.OK)
        //    {
        //        sr = new StreamReader(httpWebResponse.GetResponseStream());
        //        strResult = sr.ReadToEnd();
        //        return strResult;
        //    }
        //    else
        //    {
        //        logger.Error("Read RESTful Web Service , httpWebResponse error");
        //        return "-1";
        //    }
        //}

        private string Percent(double dd)
        {
            string ss = dd.ToString("0.00");
            if (ss.Substring(ss.Length - 1, 1) == "0")
                ss = ss.Substring(0, ss.Length - 1);
            if (ss.Substring(ss.Length - 1, 1) == "0")
                ss = ss.Substring(0, ss.Length - 1);
            if (ss.Substring(ss.Length - 1, 1) == ".")
                ss = ss.Substring(0, ss.Length - 1);
            return ss;
        }

        #region 匯出格式檔案

        //protected void ToXml_4(object sender, EventArgs e)
        //{
        //    string json = this.Hidden4.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    string strXml = xml.OuterXml;
        //    this.Response.Clear();
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
        //    this.Response.AddHeader("Content-Length", strXml.Length.ToString());
        //    this.Response.ContentType = "application/xml";
        //    this.Response.Write(strXml);
        //    this.Response.End();
        //}

        //protected void ToExcel_4(object sender, EventArgs e)
        //{
        //    string json = this.Hidden4.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        //    XslCompiledTransform xtExcel = new XslCompiledTransform();
        //    xtExcel.Load(Server.MapPath("Excel.xsl"));
        //    xtExcel.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToCsv_4(object sender, EventArgs e)
        //{
        //    string json = this.Hidden4.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/octet-stream";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
        //    XslCompiledTransform xtCsv = new XslCompiledTransform();
        //    xtCsv.Load(Server.MapPath("Csv.xsl"));
        //    xtCsv.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToXml_6(object sender, EventArgs e)
        //{
        //    string json = this.Hidden6.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    string strXml = xml.OuterXml;
        //    this.Response.Clear();
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
        //    this.Response.AddHeader("Content-Length", strXml.Length.ToString());
        //    this.Response.ContentType = "application/xml";
        //    this.Response.Write(strXml);
        //    this.Response.End();
        //}

        //protected void ToExcel_6(object sender, EventArgs e)
        //{
        //    string json = this.Hidden6.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        //    XslCompiledTransform xtExcel = new XslCompiledTransform();
        //    xtExcel.Load(Server.MapPath("Excel.xsl"));
        //    xtExcel.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToCsv_6(object sender, EventArgs e)
        //{
        //    string json = this.Hidden6.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/octet-stream";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
        //    XslCompiledTransform xtCsv = new XslCompiledTransform();
        //    xtCsv.Load(Server.MapPath("Csv.xsl"));
        //    xtCsv.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToXml_7(object sender, EventArgs e)
        //{
        //    string json = this.Hidden7.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    string strXml = xml.OuterXml;
        //    this.Response.Clear();
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
        //    this.Response.AddHeader("Content-Length", strXml.Length.ToString());
        //    this.Response.ContentType = "application/xml";
        //    this.Response.Write(strXml);
        //    this.Response.End();
        //}

        //protected void ToExcel_7(object sender, EventArgs e)
        //{
        //    string json = this.Hidden7.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + this.txtGridPanel7.Text.Replace(" ", "") + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        //    XslCompiledTransform xtExcel = new XslCompiledTransform();
        //    xtExcel.Load(Server.MapPath("Excel.xsl"));
        //    xtExcel.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToCsv_7(object sender, EventArgs e)
        //{
        //    string json = this.Hidden7.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/octet-stream";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
        //    XslCompiledTransform xtCsv = new XslCompiledTransform();
        //    xtCsv.Load(Server.MapPath("Csv.xsl"));
        //    xtCsv.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToXml_8(object sender, EventArgs e)
        //{
        //    string json = this.Hidden8.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    string strXml = xml.OuterXml;
        //    this.Response.Clear();
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
        //    this.Response.AddHeader("Content-Length", strXml.Length.ToString());
        //    this.Response.ContentType = "application/xml";
        //    this.Response.Write(strXml);
        //    this.Response.End();
        //}

        //protected void ToExcel_8(object sender, EventArgs e)
        //{
        //    string json = this.Hidden8.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        //    XslCompiledTransform xtExcel = new XslCompiledTransform();
        //    xtExcel.Load(Server.MapPath("Excel.xsl"));
        //    xtExcel.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToCsv_8(object sender, EventArgs e)
        //{
        //    string json = this.Hidden8.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/octet-stream";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
        //    XslCompiledTransform xtCsv = new XslCompiledTransform();
        //    xtCsv.Load(Server.MapPath("Csv.xsl"));
        //    xtCsv.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToXml_9(object sender, EventArgs e)
        //{
        //    string json = this.Hidden9.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    string strXml = xml.OuterXml;
        //    this.Response.Clear();
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
        //    this.Response.AddHeader("Content-Length", strXml.Length.ToString());
        //    this.Response.ContentType = "application/xml";
        //    this.Response.Write(strXml);
        //    this.Response.End();
        //}

        //protected void ToExcel_9(object sender, EventArgs e)
        //{
        //    string json = this.Hidden9.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        //    XslCompiledTransform xtExcel = new XslCompiledTransform();
        //    xtExcel.Load(Server.MapPath("Excel.xsl"));
        //    xtExcel.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToCsv_9(object sender, EventArgs e)
        //{
        //    string json = this.Hidden9.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/octet-stream";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
        //    XslCompiledTransform xtCsv = new XslCompiledTransform();
        //    xtCsv.Load(Server.MapPath("Csv.xsl"));
        //    xtCsv.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToXml_12(object sender, EventArgs e)
        //{
        //    string json = this.Hidden12.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    string strXml = xml.OuterXml;
        //    this.Response.Clear();
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
        //    this.Response.AddHeader("Content-Length", strXml.Length.ToString());
        //    this.Response.ContentType = "application/xml";
        //    this.Response.Write(strXml);
        //    this.Response.End();
        //}

        //protected void ToExcel_12(object sender, EventArgs e)
        //{
        //    string json = this.Hidden12.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/vnd.ms-excel";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
        //    XslCompiledTransform xtExcel = new XslCompiledTransform();
        //    xtExcel.Load(Server.MapPath("Excel.xsl"));
        //    xtExcel.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}

        //protected void ToCsv_12(object sender, EventArgs e)
        //{
        //    string json = this.Hidden12.Value.ToString();
        //    StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
        //    XmlNode xml = eSubmit.Xml;
        //    this.Response.Clear();
        //    this.Response.ContentType = "application/octet-stream";
        //    this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
        //    XslCompiledTransform xtCsv = new XslCompiledTransform();
        //    xtCsv.Load(Server.MapPath("Csv.xsl"));
        //    xtCsv.Transform(xml, null, this.Response.OutputStream);
        //    this.Response.End();
        //}
        #endregion

        protected void SaveBeginDate(object sender, DirectEventArgs e)
        {
            Session["sBEG_DATE"] = beg_date.Text;
        }

        protected void SaveEndDate(object sender, DirectEventArgs e)
        {
            Session["sEND_DATE"] = end_date.Text;
        }

        #region 查詢病患
        protected void PatInfo_Query()
        {
            string PATIC = SearchID.Text;
            string PATNAME = SearchName.Text;
            string sql;
            sql = " SELECT pif_id, pif_name, if(pif_sex = 'M','男','女') as sex, pif_dob, pif_ic, pif_docname FROM pat_info ";
            sql += "WHERE 1=1 ";
            if (!string.IsNullOrEmpty(PATNAME)) //姓名篩選
                sql += "AND pif_name like '%" + PATNAME + "%' ";
            if (!string.IsNullOrEmpty(PATIC)) //身分證號篩選
                sql += "AND pif_ic like '%" + PATIC + "%' ";
            sql += "ORDER BY pif_id ";

            DataTable dt = db.Query(sql);
            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
            Window2.Show();
        }
        #endregion

        #region Window2 病患查找-選擇病患
        protected void Dialysis_detail(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            string sql = "SELECT pif_id, pif_ic FROM pat_info ";
            sql += "WHERE pif_ic = '" + selRow[0]["pat_ic"].ToString() + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                Session["PAT_ID"] = dt.Rows[0]["pif_id"].ToString();
                Session["PAT_IC"] = dt.Rows[0]["pif_ic"].ToString();
                _PAT_IC = Session["PAT_IC"].ToString();
                _PAT_ID = Session["PAT_ID"].ToString();
                Store istore = GridList.GetStore();
                istore.RemoveAll();
            }
            Window2.Hide();

            if (txtNODE_ID.Text == "H_")
            {
                sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=HTest&_BEG_DATE=" + _Get_YMD2(beg_date.Text) + "&_END_DATE=" + _Get_YMD2(end_date.Text);
            }
            Panel_Center.Hidden = false;
            Panel_Center.Show();
            Panel_Center.Loader.SuspendScripting();
            Panel_Center.Loader.Url = sURL;
            Panel_Center.Loader.DisableCaching = true;
            Panel_Center.LoadContent();
        }
        #endregion

        #region Window2 历史病患
        protected void btn_Query_Click(object sender, DirectEventArgs e)
        {
            PatInfo_Query();
        }
        #endregion

        #region 關閉查詢病患
        protected void Win_Close(object sender, DirectEventArgs e)
        {
            Session["PAT_ID"] = "";
            Session["PAT_IC"] = "";
            Window2.Hide();
        }
        #endregion
    }
}