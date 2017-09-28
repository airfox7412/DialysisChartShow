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
using System.Net;
using System.IO;
using System.Text;

namespace Dialysis_Chart_Show.Information
{

  public partial class Dialysis_13 : BaseForm  //System.Web.UI.Page
  {
    private static DataTable dt_ritem_setup;
    private static DataTable dt_ritem_setup12;
    private static DateTime d_ritem_setup;
    private string sURL = "";
    private string sCODE = "";
    //protected DBMysql db = new DBMysql();
    //private  Ext.Net.Node root = new Ext.Net.Node();
    //private  Ext.Net.Node rA = new Ext.Net.Node();
    //private  Ext.Net.Node rB = new Ext.Net.Node();
    //private  Ext.Net.Node rC = new Ext.Net.Node();
    //private  Ext.Net.Node rD = new Ext.Net.Node();
    //private StatusBar statusBar1 = new StatusBar();

    private void SetNode(Ext.Net.Node nNode, string sText, string sDataPath, string sNodeID, Ext.Net.Node nFather)
    {
      nNode.Text = sText;
      nNode.DataPath = sDataPath;
      nNode.NodeID = sNodeID;
      nNode.Cls = "large-font";
      nFather.Children.Add(nNode);
    }

    private void AddNode(Ext.Net.Node nf, string sText, string sDataPath, string sNodeID)
    {
      Ext.Net.Node rN = new Ext.Net.Node()
      {
        Text = sText,
        DataPath = sDataPath,
        NodeID = sNodeID,
        Icon = Icon.Report,
        Cls = "large-font",
        Leaf = true
      };
      nf.Children.Add(rN);
    }

    private string sel_PAT_NO = "";
    private string sel_PAT_NAME = "";
    private string USER_ID = "";
    string getdata;
     
    protected new void Page_Load(object sender, EventArgs e)
    {

      if (!X.IsAjaxRequest)
      {
//        Lab_patid.Text = "身份證號 : " + _Request("sel_PAT_NO");
        sel_PAT_NO = _Request("sel_PAT_NO");
        USER_ID = _Request("USER_ID");
        getdata = _Request("USER_TYPE");
        string s1SQL = "";
        s1SQL = "SELECT pif_name FROM pat_info WHERE pif_ic='" + sel_PAT_NO + "' ";
        DataTable dt1CODE = db.Query(s1SQL);
        if (dt1CODE.Rows.Count > 0)
        {
          sel_PAT_NAME = dt1CODE.Rows[0]["pif_name"].ToString();
        }
        if (dt_ritem_setup == null)
        {
          GetGroupName();
          d_ritem_setup = DateTime.Now;
        }
        else
        {
          TimeSpan iDiff = new TimeSpan(DateTime.Now.Ticks - d_ritem_setup.Ticks);
          if (iDiff.TotalSeconds > 120)
          {
            GetGroupName();
            d_ritem_setup = DateTime.Now;
          }
        }

        //20150627 ANDY
        string sSQL = "";
        string W_QUALITY = "";
//        sSQL = "SELECT * FROM general_setup WHERE genst_ctg='QUALITY' AND genst_desc='Y'";
//        DataTable dtCODE = db.Query(sSQL);
//        if (dtCODE.Rows.Count > 0)
//        {
//          W_QUALITY = "Y";
//        }

        this.txtGridPanel7.Hidden = true;
        DateTime dBEG = DateTime.Now;
        DateTime dEND = DateTime.Now;
        dBEG = Convert.ToDateTime(dBEG.ToString("yyyy-MM-") + "01");
        dEND = dBEG;
        dEND = dEND.AddMonths(1);
        dEND = dEND.AddDays(-1);
        this.beg_date.Text = dBEG.ToString("yyyy-MM-dd");
        this.end_date.Text = dEND.ToString("yyyy-MM-dd");


        this.txtNODE_ID.Hidden = true;
        this.txtNODE_TEXT.Hidden = true;
        //Ext.Net.TreePanel tree = new Ext.Net.TreePanel();

        this.TreePanel1.ID = "TreePanel1";
        //this.TreePanel1.Width = Unit.Pixel(200);
        this.TreePanel1.Height = Unit.Pixel(600);
        this.TreePanel1.AutoScroll = true;

        Ext.Net.Button btnExpand = new Ext.Net.Button();
        btnExpand.Text = "展开";
        btnExpand.Listeners.Click.Handler = this.TreePanel1.ClientID + ".expandAll();";

        Ext.Net.Button btnCollapse = new Ext.Net.Button();
        btnCollapse.Text = "收合";
        btnCollapse.Listeners.Click.Handler = this.TreePanel1.ClientID + ".collapseAll();";

        Toolbar toolBar = new Toolbar();
        toolBar.ID = "ToolbarT";
        toolBar.Items.Add(btnExpand);
        toolBar.Items.Add(btnCollapse);
        this.TreePanel1.TopBar.Add(toolBar);

        StatusBar statusBar1 = new StatusBar();
        statusBar1.ID = "StatusBarT";
        statusBar1.AutoClear = 1000;
        this.TreePanel1.BottomBar.Add(statusBar1);

        this.TreePanel1.Listeners.ItemClick.Handler = statusBar1.ClientID + ".setStatus({text: '点选: <b>' + record.data.text + '</b>', clear: false});";
        //tree.Listeners.ItemExpand.Handler = statusBar.ClientID + ".setStatus({text: 'Node Expanded: <b>' + item.data.text + '</b>', clear: false});";
        //tree.Listeners.ItemExpand.Buffer = 30;
        //tree.Listeners.ItemCollapse.Handler = statusBar.ClientID + ".setStatus({text: 'Node Collapsed: <b>' + item.data.text + '</b>', clear: false});";
        //tree.Listeners.ItemCollapse.Buffer = 30;

        Ext.Net.Node root = new Ext.Net.Node();
        root.Text = "质量分析";
        root.Icon = Icon.ReportUser;
        root.NodeID = "__";
        root.Cls = "large-font";
        Ext.Net.Node rA = new Ext.Net.Node();
        rA.Text = "A.质量管理基础数据";
        rA.NodeID = "A_";
        rA.Cls = "large-font";
        Ext.Net.Node rB = new Ext.Net.Node();
        rB.Text = "B.质量指标数据";
        rB.NodeID = "B_";
        rB.Cls = "large-font";

        Ext.Net.Node rC = new Ext.Net.Node();
        rC.Text = "C.自订条件查询";
        rC.NodeID = "C_";
        rC.Cls = "large-font";
        rC.Leaf = true;

        Ext.Net.Node rD = new Ext.Net.Node();
        rD.Text = "D.单个患者";
        rD.NodeID = "D_";
        rD.Cls = "large-font";
        rD.Leaf = true;

        Ext.Net.Node rE = new Ext.Net.Node();
        rE.Text = "E.耗材使用数量";
        rE.NodeID = "E_";
        rE.Cls = "large-font";
        rE.Leaf = true;

        //2015.05.26 Andy F.品质监控指标 
        //20150627 ANDY               
        Ext.Net.Node rF = new Ext.Net.Node();
        if (W_QUALITY == "Y")
        {
          rF.Text = "F.品质监控指标";
          rF.NodeID = "F_";
          rF.Cls = "large-font";
          rF.Leaf = true;
        };

        //20150627 ANDY
        Ext.Net.Node rG = new Ext.Net.Node();
        if (W_QUALITY == "Y")
        {
          rG.Text = "G.品质监控指标查詢";
          rG.NodeID = "G_";
          rG.Cls = "large-font";
          rG.Leaf = true;
        };

        //A
        AddNode(rA, "统计分析", "../report/Rpt_View_Dialysis.aspx", "A0");
        AddNode(rA, "基本病患统计", "../report/Report_Dialysis_h.aspx?_REPORT_NAME=5", "A1");
        AddNode(rA, "血透年龄统计", "../report/Report_Dialysis_h.aspx?_REPORT_NAME=10", "A2");
        AddNode(rA, "护士工作量统计", "../report/Report_Dialysis_h.aspx?_REPORT_NAME=8", "A4");
        AddNode(rA, "血透中出现症状统计", "../report/Report_Dialysis_h.aspx?_REPORT_NAME=9", "A5");
        Ext.Net.Node rBx = new Ext.Net.Node();
        SetNode(rBx, "1.实验室检查", "", "Bx", rB);
        AddNode(rB, "2.kt/v", "", "B1");
        AddNode(rB, "3.住院率", "", "B10");
        AddNode(rB, "4.死亡率", "", "B11");
        AddNode(rB, "5.HBsAg转阳率", "", "B13");
        AddNode(rB, "6.antiHCV转阳率", "", "B14");

        AddNode(rBx, "1.血红蛋白Hb", "", "B8");      //4003
        AddNode(rBx, "2.白蛋白ALB", "", "B3");       //4008
        AddNode(rBx, "3.钙Ca", "", "B21");           //4021
        AddNode(rBx, "4.磷P", "", "B22");            //4023
        AddNode(rBx, "5.转铁蛋白饱和度", "", "B23"); //4050
        AddNode(rBx, "6.铁蛋白", "", "B24");         //4027
        AddNode(rBx, "7.iPTH", "", "B25");           //4030


        root.Children.Add(rA);
        root.Children.Add(rB);
        root.Children.Add(rC);
        root.Children.Add(rD);
        root.Children.Add(rE);

        //20150627 ANDY
        if (W_QUALITY == "Y")
        {
          root.Children.Add(rF); //2015.05.26 Andy F.品质监控指标               
          root.Children.Add(rG); //2015.06.26
        };


//20160215 add by ssi for label aaaaa
        rA.Expanded = true;
        rB.Expanded = true;
        rBx.Expanded = true;
        root.Expanded = true;
        this.TreePanel1.Root.Add(root);
        this.Panel_13.Hidden = false;
        this.GridPanel18.Hidden = true;
        btn_Query13X_Click();

      }
    }

    protected void Node_Click(object sender, DirectEventArgs e)
    {
      this.txtNODE_ID.Text = e.ExtraParams["rID"];
      this.txtNODE_TEXT.Text = e.ExtraParams["rTEXT"];
      this.txtRESULT_CODE.Text = "";
      //string sURL = "";
      //string sCODE = "";
      panelHide();

      //20150627 ANDY
      string sSQL = "";
      string W_QUALITY = "";
      sSQL = "SELECT * FROM general_setup WHERE genst_ctg='QUALITY' AND genst_desc='Y'";
      DataTable dtCODE = db.Query(sSQL);
      if (dtCODE.Rows.Count > 0)
      {
        //this.txtRESULT_NAME.Text = dtCODE.Rows[0]["RITEM_NAME"].ToString();
        W_QUALITY = "Y";
      }

      switch (this.txtNODE_ID.Text)
      {
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
          sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=7&_BEG_DATE=" + _Get_YMD2(this.beg_date.Text) + "&_END_DATE=" + _Get_YMD2(this.end_date.Text);
          break;
        case "A4"://穿刺次数(小计)
          sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=8&_BEG_DATE=" + _Get_YMD2(this.beg_date.Text) + "&_END_DATE=" + _Get_YMD2(this.end_date.Text);
          break;
        case "A5"://血透中出现症状症状统计
          sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=9&_BEG_DATE=" + _Get_YMD2(this.beg_date.Text) + "&_END_DATE=" + _Get_YMD2(this.end_date.Text);
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
        case "B1": //1.Kt/V检查比率
          sCODE = "5018";
          break;
        case "B2": //2.平均透析效率
          sCODE = "5018";
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
        case "F_": //F.品质监控指标 2015.05.26 Andy
          //20150627 ANDY
          if (W_QUALITY == "Y")
          {
            sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=98";
          }
          break;
        default:
          break;
      }

      this.txtRESULT_CODE.Text = sCODE;
      if (sURL != "")
      {
        Window1.Show();
        Window1.Loader.SuspendScripting();
        Window1.Loader.Url = sURL;
        Window1.Loader.DisableCaching = true;
        Window1.LoadContent();
      }
      else if (sCODE != "")
      {
        this.Panel_1.Hidden = false;
        btn_Query1_Click(sender, e);
      }
      else
      {
        switch (this.txtNODE_ID.Text)
        {
          case "E_": //耗材使用数量
            this.Panel_13.Hidden = false;
            btn_Query13_Click(sender, e);
            break;
          case "C_": //自订条件查询
            this.Panel_11.Hidden = false;
            btn_Query11_Click(sender, e);
            break;
          case "G_": //監控指標查询
            //20150627 ANDY
            if (W_QUALITY == "Y")
            {
              this.Panel_11Q.Hidden = false;
              btn_Query11Q_Click(sender, e);
            }
            break;
          case "D_": //单个患者
            //X.Msg.Alert("注意", "请从【实验室及辅助检查】操作，点选检验代码后的图示").Show();
            this.Panel_12.Hidden = false;
            break;
          case "D8": //血红蛋白Hb
            this.txtRESULT_CODE12.Text = "4003";
            this.Panel_12.Hidden = false;
            btn_Query12_Click(sender, e);
            break;
          case "D3": //白蛋白ALB
            this.txtRESULT_CODE12.Text = "4008";
            this.Panel_12.Hidden = false;
            btn_Query12_Click(sender, e);
            break;
          case "D21": //钙Ca
            this.txtRESULT_CODE12.Text = "4021";
            this.Panel_12.Hidden = false;
            btn_Query12_Click(sender, e);
            break;
          case "D22": //磷P
            this.txtRESULT_CODE12.Text = "4023";
            this.Panel_12.Hidden = false;
            btn_Query12_Click(sender, e);
            break;
          case "D23": //转铁蛋白饱和度
            this.txtRESULT_CODE12.Text = "4050";
            this.Panel_12.Hidden = false;
            btn_Query12_Click(sender, e);
            break;
          case "D24": //铁蛋白
            this.txtRESULT_CODE12.Text = "4027";
            this.Panel_12.Hidden = false;
            btn_Query12_Click(sender, e);
            break;
          case "D25": //IPTH
            this.txtRESULT_CODE12.Text = "4030";
            this.Panel_12.Hidden = false;
            btn_Query12_Click(sender, e);
            break;

          case "B11": //死亡率
            this.Panel_2.Hidden = false;
            btn_Query2_Click(sender, e);
            break;
          case "B7": //瘘管重建率
            this.Panel_3.Hidden = false;
            btn_Query3_Click(sender, e);
            break;
          case "B10": //住院率
            this.Panel_4.Hidden = false;
            btn_Query4_Click(sender, e);
            break;
          case "B15": //3.转归率
            this.Panel_7.Hidden = false;
            btn_Query7_Click(sender, e);
            break;
          case "B16": //有症状
            this.Panel_5.Hidden = false;
            btn_Query5_Click(sender, e);
            break;
          case "C11": //4.死亡率
            this.Panel_6.Hidden = false;
            btn_Query6_Click(sender, e);
            break;
          case "C7": //1.瘘管重建率
            this.Panel_6.Hidden = false;
            btn_Query6_Click(sender, e);
            break;
          case "C10": //1.住院率
            this.Panel_6.Hidden = false;
            btn_Query6_Click(sender, e);
            break;
          case "C16": //有症状
            this.Panel_6.Hidden = false;
            btn_Query6_Click(sender, e);
            break;
          case "C3":
            this.Panel_8.Hidden = false;
            this.GridPanel9.Title = "每月统计 - Albumin检查";
            this.txtRESULT_CODE2.Text = "4008";
            btn_Query8_Click(sender, e);
            break;
          case "C8":
            this.Panel_8.Hidden = false;
            this.GridPanel9.Title = "每月统计 - Hb检查";
            this.txtRESULT_CODE2.Text = "4003";
            btn_Query8_Click(sender, e);
            break;
          case "C1":
            this.Panel_8.Hidden = false;
            this.GridPanel9.Title = "每月统计 - Kt/V检查";
            this.txtRESULT_CODE2.Text = "5018";
            btn_Query8_Click(sender, e);
            break;
          case "C15":
            this.Panel_8.Hidden = false;
            this.GridPanel9.Title = "每月统计 - Hct检查";
            this.txtRESULT_CODE2.Text = "4004";
            btn_Query8_Click(sender, e);
            break;
          case "B13":
            this.Panel_9.Hidden = false;
            this.txtRESULT_CODE3.Text = "4032";
            btn_Query9_Click(sender, e);
            break;
          case "B14":
            this.Panel_9.Hidden = false;
            this.txtRESULT_CODE3.Text = "4033";
            btn_Query9_Click(sender, e);
            break;
          case "C13":
            this.Panel_10.Hidden = false;
            this.txtRESULT_CODE4.Text = "4032";
            btn_Query10_Click(sender, e);
            break;
          case "C14":
            this.Panel_10.Hidden = false;
            this.txtRESULT_CODE4.Text = "4033";
            btn_Query10_Click(sender, e);
            break;
          default:
            break;
        }

      }

    }

    private void panelHide()
    {
      this.Panel_1.Hidden = true;
      this.Panel_2.Hidden = true;
      this.Panel_3.Hidden = true;
      this.Panel_4.Hidden = true;
      this.Panel_5.Hidden = true;
      this.Panel_6.Hidden = true;
      this.Panel_7.Hidden = true;
      this.Panel_8.Hidden = true;
      this.Panel_9.Hidden = true;
      this.Panel_10.Hidden = true;
      this.Panel_11.Hidden = true;
      this.Panel_12.Hidden = true;
//      this.Panel_13.Hidden = true;
    }

    protected void btn_Query1_Click(object sender, DirectEventArgs e)
    {
      //檢驗
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";
      string sRESULT_CODE = this.txtRESULT_CODE.Text;
      if (sRESULT_CODE != "")
      {
        if (sBEG_DATE == "")
          sBEG_DATE = "2000-01-01";
        if (sEND_DATE == "")
          sEND_DATE = "9999-12-31";
        //找檢查項目
        sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
        DataTable dtCODE = db.Query(sSQL);
        if (dtCODE.Rows.Count > 0)
        {
          this.txtRESULT_NAME.Text = dtCODE.Rows[0]["RITEM_NAME"].ToString();
          this.txtRESULT_UNIT.Text = dtCODE.Rows[0]["RITEM_UNIT"].ToString();
          this.txtNORMAL.Text = dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE.Rows[0]["RITEM_UNIT"].ToString();
        }

        //四捨五入
        if (chkFORMAT.Checked)
          txtFORMAT.Text = dtCODE.Rows[0]["RITEM_FORMAT"].ToString();
        else
          txtFORMAT.Text = "";

        //找受檢人
        sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                 "FROM pat_visit A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.pv_ic=B.pif_ic " +
                "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                  "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "ORDER BY B.pif_id ";
        //2014-07-31 ada 改成 有登記就算人(血透人数的计算按照  这个人只要排过班就算，不分楼层，不管有没有血透过。)
        sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                 "FROM pat_info B " +
                 " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                "WHERE 1=1 " +
                "ORDER BY B.pif_id ";
        DataTable dtTOTAL = db.Query(sSQL);
        this.txtTOTAL1.Text = dtTOTAL.Rows.Count.ToString();

        //找有做檢查的人
        sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + this.txtRESULT_NAME.Text + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                 "FROM a_result_log A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.PAT_NO=B.pif_id " +
                 " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
        DataTable dtCHECK = db.Query(sSQL);

        //四捨五入
        if (txtFORMAT.Text != "")
        {
          for (int n = 0; n < dtCHECK.Rows.Count; n++)
          {
            dtCHECK.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK.Rows[n]["RESULT_VALUE_N"]).ToString(txtFORMAT.Text));
            dtCHECK.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK.Rows[n]["RESULT_VALUE_N"]).ToString(txtFORMAT.Text);
          }
        }
        else
        {
          for (int n = 0; n < dtCHECK.Rows.Count; n++)
          {
            dtCHECK.Rows[n]["RESULT_VALUE_T"] = dtCHECK.Rows[n]["RESULT_VALUE_N"].ToString();
          }
        }

        Store istore22 = this.GridPanel22.GetStore();
        istore22.DataSource = db.GetDataArray_AddRowNum(dtCHECK);
        istore22.DataBind();
        this.txtCHECK.Text = dtCHECK.Rows.Count.ToString();

        //算合格人數
        if (dtCODE.Rows[0]["RITEM_LOW1"].ToString() == "")
          dtCODE.Rows[0]["RITEM_LOW1"] = "0";
        if (dtCODE.Rows[0]["RITEM_HIGH1"].ToString() == "")
          dtCODE.Rows[0]["RITEM_HIGH1"] = "99999";
        System.Data.DataView dvCHECK;
        dvCHECK = dtCHECK.DefaultView;
        dvCHECK.RowFilter = "RESULT_VALUE_N>=" + dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE.Rows[0]["RITEM_HIGH1"].ToString();
        Store istore21 = this.GridPanel21.GetStore();
        istore21.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
        istore21.DataBind();
        this.txtCHECK_Y.Text = dvCHECK.Count.ToString();

        //算不合格人數
        //System.Data.DataView dvCHECK_N;
        //dvCHECK_N = dtCHECK.DefaultView;
        dvCHECK.RowFilter = "RESULT_VALUE_N<" + dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE.Rows[0]["RITEM_HIGH1"].ToString();
        this.txtCHECK_N.Text = dvCHECK.Count.ToString();

        Store istore1 = this.GridPanel1.GetStore();
        istore1.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
        istore1.DataBind();

        //this.txtERR.Text = ""; 
        //受檢人-有做檢查的人
        DataTable dtUNCHECK = dtTOTAL.Copy();
        System.Data.DataView dvUNCHECK;
        dvUNCHECK = dtUNCHECK.DefaultView;
        for (int i = 0; i < dtCHECK.Rows.Count; i++)
        {
          dvUNCHECK.RowFilter = "pv_ic='" + dtCHECK.Rows[i]["pif_ic"] + "'";
          if (dvUNCHECK.Count > 0)
            dvUNCHECK[0].Delete();
          //else
          //    this.txtERR.Text += "{" + dtCHECK.Rows[i]["pif_name"].ToString() + "-" + dtCHECK.Rows[i]["pif_ic"].ToString() + "," + dtCHECK.Rows[i]["PAT_NO"].ToString() + "}";
        }

        dtUNCHECK.AcceptChanges();
        this.txtUNCHECK.Text = dtUNCHECK.Rows.Count.ToString();
        Store istore2 = this.GridPanel2.GetStore();
        istore2.DataSource = db.GetDataArray_AddRowNum(dtUNCHECK);
        istore2.DataBind();

        if (this.txtTOTAL1.Text == "0")
        {
          this.txtCHECK_P.Text = "0";
          this.txtUNCHECK_P.Text = "0";
        }
        else
        {
          this.txtCHECK_P.Text = Percent(Convert.ToDouble(this.txtCHECK.Text) / Convert.ToDouble(this.txtTOTAL1.Text) * 100);
          this.txtUNCHECK_P.Text = Percent(Convert.ToDouble(this.txtUNCHECK.Text) / Convert.ToDouble(this.txtTOTAL1.Text) * 100);
        }

        if (this.txtCHECK.Text == "0")
        {
          this.txtCHECK_YP.Text = "0";
          this.txtCHECK_NP.Text = "0";
        }
        else
        {
          this.txtCHECK_YP.Text = Percent(Convert.ToDouble(this.txtCHECK_Y.Text) / Convert.ToDouble(this.txtCHECK.Text) * 100);
          this.txtCHECK_NP.Text = Percent(Convert.ToDouble(this.txtCHECK_N.Text) / Convert.ToDouble(this.txtCHECK.Text) * 100);
        }
      }
    }

    protected void btn_Query2_Click(object sender, DirectEventArgs e)
    {
      //死亡率
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";

      //20150609 ANDY MARK
      //if (sBEG_DATE == "")
      //    sBEG_DATE = "2000-01-01";
      //else
      //    sBEG_DATE = sBEG_DATE.Substring(0, 4) + "-01-01";

      //if (sEND_DATE == "")
      //    sEND_DATE = "9999-12-31";
      //else
      //    sEND_DATE = sEND_DATE.Substring(0, 4) + "-12-31";

      txtDATE2.Text = sBEG_DATE + " ~ " + sEND_DATE;

      //找受檢人
      //sSQL = "SELECT B.pif_name, A.pv_ic, B.pif_id " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info B " +
      //           "ON A.pv_ic=B.pif_ic " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //        "ORDER BY B.pif_id ";
      sSQL = "SELECT DISTINCT A.pv_ic " +
               "FROM pat_visit A " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' ";
      //2014-07-31 ada 改成 有登記就算人(血透人数的计算按照  这个人只要排过班就算，不分楼层，不管有没有血透过。)
      sSQL = "SELECT B.pif_ic AS pv_ic " +
               "FROM pat_info B " +

               " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +

              "WHERE 1=1 ";
      DataTable dtTOTAL2 = db.Query(sSQL);
      this.txtTOTAL2.Text = dtTOTAL2.Rows.Count.ToString();

      //找死亡的人
      //OLD
      //sSQL = "SELECT B.pif_name, B.pif_ic, A.pat_id AS PAT_NO, 0 as AGE, 0 as AGE_HD, A.txt_56 AS DIE_CAUSE, " +
      //              "A.info_date AS DIE_DATE, B.pif_dob, C.txt_9 AS HD_DATE, B.pif_sex, A.chk_55 " +
      //         "FROM zinfo_e_01 A " +
      //         "LEFT JOIN pat_info B " +
      //           "ON A.pat_id=B.pif_id " +
      //         "LEFT JOIN zinfo_f_012 C " +
      //           "ON A.pat_id=C.pat_id " +
      //        "WHERE A.info_date>='" + sBEG_DATE + "' " +
      //          "AND A.info_date<='" + sEND_DATE + "' " +
      //          "AND A.opt_52=4 ";

      //NEW 20150608 ANDY
      sSQL = "SELECT B.pif_name, B.pif_ic, A.pat_id AS PAT_NO, 0 as AGE, 0 as AGE_HD, '' AS DIE_CAUSE," +
                                "A.info_date AS DIE_DATE,  B.pif_dob, C.dat_9 AS HD_DATE, B.pif_sex, A.chk_7 as chk_55 " +
                           "FROM zinfo_a_07 A " +
                           "LEFT JOIN pat_info B " +
                             "ON  A.pat_id=B.pif_id " +
                           "  AND A.opt_1 in('4') " +
                           "LEFT JOIN zinfo_f_012 C " +
                             "ON A.pat_id=C.pat_id " +
                          "WHERE A.info_date>='" + sBEG_DATE + "' " +
                            "AND A.info_date<='" + sEND_DATE + "' ";


      DataTable dtDIE = db.Query(sSQL);
      this.txtDIE.Text = dtDIE.Rows.Count.ToString();
      string sDIE_CAUSE = "";
      string schk_55 = "";
      string sDIE_DATE = "";
      int iAGE = 0;
      int iAGE_HD = 0;
      for (int i = 0; i < dtDIE.Rows.Count; i++)
      {
        sDIE_CAUSE = "";
        sDIE_DATE = dtDIE.Rows[i]["DIE_DATE"].ToString();

        if (dtDIE.Rows[i]["pif_dob"].ToString().Length >= 4)
        {
          if (Int32.TryParse(dtDIE.Rows[i]["pif_dob"].ToString().Substring(0, 4), out iAGE))
          {
            iAGE = Convert.ToInt32(dtDIE.Rows[i]["pif_dob"].ToString().Substring(0, 4));

            //dtDIE.Rows[i]["AGE"] = Convert.ToInt16(sDIE_DATE.Substring(0, 4)) - iAGE + 1;
            //2015.05.02 andy
            dtDIE.Rows[i]["AGE"] = Convert.ToInt16(sDIE_DATE.Substring(0, 4)) - iAGE;
          }
          else
          {
            dtDIE.Rows[i]["AGE"] = 0;
            //OLD sDIE_CAUSE += "'出生日期'资料错误，";
            sDIE_CAUSE += "";
          }
        }
        else
        {
          dtDIE.Rows[i]["AGE"] = 0;
          //OLD sDIE_CAUSE += "'出生日期'资料错误，";
          sDIE_CAUSE += "";
        }


        //2015.04.28 andy  暫mark 
        //if (dtDIE.Rows[i]["HD_DATE"].ToString().Length >= 4)
        //{
        //    if (Int32.TryParse(dtDIE.Rows[i]["HD_DATE"].ToString().Substring(0, 4), out iAGE_HD))
        //    {
        //        iAGE_HD = Convert.ToInt32(dtDIE.Rows[i]["HD_DATE"].ToString().Substring(0, 4));
        //        dtDIE.Rows[i]["AGE_HD"] = Convert.ToInt16(sDIE_DATE.Substring(0, 4)) - iAGE_HD + 1;
        //    }
        //    else
        //    {
        //        dtDIE.Rows[i]["AGE_HD"] = 0;
        //        sDIE_CAUSE += "'首次血透日期'资料错误，";
        //    }
        //}
        //else
        //{
        //    dtDIE.Rows[i]["AGE_HD"] = 0;
        //    sDIE_CAUSE += "'首次血透日期'资料错误，";
        //}

        if (sDIE_CAUSE == "")
        {
          schk_55 = dtDIE.Rows[i]["chk_55"].ToString();
          if (schk_55.Substring(0, 1) == "1")
            sDIE_CAUSE += "心血管事件，";
          if (schk_55.Substring(1, 1) == "1")
            sDIE_CAUSE += "脑血管事件，";
          if (schk_55.Substring(2, 1) == "1")
            sDIE_CAUSE += "感染，";
          if (schk_55.Substring(3, 1) == "1")
            sDIE_CAUSE += "消化道出血等出血性疾病，";

          //OLD MARK 20150608 ANDY
          //if (dtDIE.Rows[i]["DIE_CAUSE"].ToString() != "")
          //    sDIE_CAUSE += dtDIE.Rows[i]["DIE_CAUSE"].ToString() + "，";
        }

        //OLD MARK 20150608 ANDY 
        if (sDIE_CAUSE != "")
        {
          dtDIE.Rows[i]["DIE_CAUSE"] = sDIE_CAUSE.Substring(0, sDIE_CAUSE.Length - 1);
        }
        switch (dtDIE.Rows[i]["pif_sex"].ToString().Trim())
        {
          case "F":
            dtDIE.Rows[i]["pif_sex"] = "女";
            break;
          case "M":
            dtDIE.Rows[i]["pif_sex"] = "男";
            break;
        }
      }
      Store istore1 = this.GridPanel3.GetStore();
      istore1.DataSource = db.GetDataArray_AddRowNum(dtDIE);
      istore1.DataBind();

      if (this.txtTOTAL2.Text == "0")
      {
        this.txtDIE_P.Text = "0";
      }
      else
      {
        this.txtDIE_P.Text = Percent(Convert.ToDouble(this.txtDIE.Text) / Convert.ToDouble(this.txtTOTAL2.Text) * 1000);
      }
    }

    protected void btn_Query3_Click(object sender, DirectEventArgs e)
    {
      //瘘管重建
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";

      if (sBEG_DATE == "")
        sBEG_DATE = "2000-01-01";
      if (sEND_DATE == "")
        sEND_DATE = "9999-12-31";
      //找受檢人
      //sSQL = "SELECT B.pif_name, A.pv_ic, B.pif_id " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info B " +
      //           "ON A.pv_ic=B.pif_ic " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //        "ORDER BY B.pif_id ";
      sSQL = "SELECT A.pv_ic, A.pv_datevisit " +
               "FROM pat_visit A " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' ";
      DataTable dtTOTAL3 = db.Query(sSQL);
      this.txtTOTAL3.Text = dtTOTAL3.Rows.Count.ToString();

      //找瘘管重建的人
      sSQL = "SELECT B.pif_name, B.pif_sex, B.pif_ic, A.pat_id AS PAT_NO, " +
                    "A.info_date AS HD_DATE, A.txt_10 AS HD_CAUSE, A.opt_9 " +
               "FROM zinfo_e_02 A " +
               "LEFT JOIN pat_info B " +
                 "ON A.pat_id=B.pif_id " +
              "WHERE A.info_date>='" + sBEG_DATE + "' " +
                "AND A.info_date<='" + sEND_DATE + "' " +
                "AND A.opt_8=1 ";
      DataTable dtHD = db.Query(sSQL);
      this.txtHD.Text = dtHD.Rows.Count.ToString();
      for (int i = 0; i < dtHD.Rows.Count; i++)
      {
        switch (dtHD.Rows[i]["opt_9"].ToString())
        {
          case "1":
            dtHD.Rows[i]["HD_CAUSE"] = "导管感染";
            break;
          case "2":
            dtHD.Rows[i]["HD_CAUSE"] = "内瘘阻塞";
            break;
          case "3":
            dtHD.Rows[i]["HD_CAUSE"] = "血流量过小(内瘘狭窄)";
            break;
          case "4":
            dtHD.Rows[i]["HD_CAUSE"] = "血流量过大(内瘘成熟)";
            break;
          case "5":
            dtHD.Rows[i]["HD_CAUSE"] = "长期导管移位";
            break;
          case "6":
            dtHD.Rows[i]["HD_CAUSE"] = "窃流症候群";
            break;
        }
        switch (dtHD.Rows[i]["pif_sex"].ToString().Trim())
        {
          case "F":
            dtHD.Rows[i]["pif_sex"] = "女";
            break;
          case "M":
            dtHD.Rows[i]["pif_sex"] = "男";
            break;
        }
      }
      Store istore1 = this.GridPanel4.GetStore();
      istore1.DataSource = db.GetDataArray_AddRowNum(dtHD);
      istore1.DataBind();

      if (this.txtTOTAL3.Text == "0")
      {
        this.txtHD_P.Text = "0";
      }
      else
      {
        this.txtHD_P.Text = Percent(Convert.ToDouble(this.txtHD.Text) / Convert.ToDouble(this.txtTOTAL3.Text) * 100);
      }
    }

    protected void btn_Query4_Click(object sender, DirectEventArgs e)
    {
      //住院率
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";

      //20150609 ANDY MARK
      //if (sBEG_DATE == "")
      //    sBEG_DATE = "2000-01-01";
      //else
      //    sBEG_DATE = sBEG_DATE.Substring(0, 4) + "-01-01";

      //if (sEND_DATE == "")
      //    sEND_DATE = "9999-12-31";
      //else
      //    sEND_DATE = sEND_DATE.Substring(0, 4) + "-12-31";

      txtDATE4.Text = sBEG_DATE + " ~ " + sEND_DATE;

      //找受檢人
      //sSQL = "SELECT B.pif_name, A.pv_ic, B.pif_id " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info B " +
      //           "ON A.pv_ic=B.pif_ic " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //        "ORDER BY B.pif_id ";
      sSQL = "SELECT DISTINCT A.pv_ic " +
               "FROM pat_visit A " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' ";
      //2014-07-31 ada 改成 有登記就算人(血透人数的计算按照  这个人只要排过班就算，不分楼层，不管有没有血透过。)
      sSQL = "SELECT B.pif_ic AS pv_ic " +
               "FROM pat_info B " +

               " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +

              "WHERE 1=1 ";
      DataTable dtTOTAL4 = db.Query(sSQL);
      this.txtTOTAL4.Text = dtTOTAL4.Rows.Count.ToString();

      //找住院的人
      sSQL = "SELECT B.pif_name, B.pif_sex, B.pif_ic, A.pat_id AS PAT_NO, " +
                    "A.info_date AS HOSP_DATE, A.txt_27 AS HOSP_CAUSE " +
               "FROM zinfo_e_01 A " +
               "LEFT JOIN pat_info B " +
                 "ON A.pat_id=B.pif_id " +

              " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +

              "WHERE A.info_date>='" + sBEG_DATE + "' " +
                "AND A.info_date<='" + sEND_DATE + "' " +
                "AND A.opt_26=1 ";
      DataTable dtHOSP = db.Query(sSQL);
      this.txtHOSP.Text = dtHOSP.Rows.Count.ToString();
      for (int i = 0; i < dtHOSP.Rows.Count; i++)
      {
        switch (dtHOSP.Rows[i]["pif_sex"].ToString().Trim())
        {
          case "F":
            dtHOSP.Rows[i]["pif_sex"] = "女";
            break;
          case "M":
            dtHOSP.Rows[i]["pif_sex"] = "男";
            break;
        }
      }
      Store istore1 = this.GridPanel5.GetStore();
      istore1.DataSource = db.GetDataArray_AddRowNum(dtHOSP);
      istore1.DataBind();

      if (this.txtTOTAL4.Text == "0")
      {
        this.txtHOSP_P.Text = "0";
      }
      else
      {
        this.txtHOSP_P.Text = Percent(Convert.ToDouble(this.txtHOSP.Text) / Convert.ToDouble(this.txtTOTAL4.Text) * 1000);
      }
    }

    protected void btn_Query5_Click(object sender, DirectEventArgs e)
    {
      //有症状
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";

      if (sBEG_DATE == "")
        sBEG_DATE = "2000-01-01";
      if (sEND_DATE == "")
        sEND_DATE = "9999-12-31";
      //找受檢人
      //sSQL = "SELECT B.pif_name, A.pv_ic, B.pif_id " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info B " +
      //           "ON A.pv_ic=B.pif_ic " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //        "ORDER BY B.pif_id ";
      sSQL = "SELECT A.pv_ic, A.pv_datevisit " +
               "FROM pat_visit A " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' ";
      DataTable dtTOTAL5 = db.Query(sSQL);
      this.txtTOTAL5.Text = dtTOTAL5.Rows.Count.ToString();

      //找有症状的人
      sSQL = "SELECT B.pif_name, B.pif_sex, B.pif_ic, B.pif_id AS PAT_NO, " +
                    "A.cln3_date AS HOSP_DATE " +
               "FROM clinical3_nurse A " +
               "LEFT JOIN pat_info B " +
                 "ON A.cln3_patic=B.pif_ic " +
              "WHERE A.cln3_date>='" + sBEG_DATE + "' " +
                "AND A.cln3_date<='" + sEND_DATE + "' " +
                "AND A.cln3_yn='Y' ";
      DataTable dtSYMPTON = db.Query(sSQL);
      this.txtSYMPTON.Text = dtSYMPTON.Rows.Count.ToString();
      for (int i = 0; i < dtSYMPTON.Rows.Count; i++)
      {
        switch (dtSYMPTON.Rows[i]["pif_sex"].ToString().Trim())
        {
          case "F":
            dtSYMPTON.Rows[i]["pif_sex"] = "女";
            break;
          case "M":
            dtSYMPTON.Rows[i]["pif_sex"] = "男";
            break;
        }
      }
      Store istore1 = this.GridPanel6.GetStore();
      istore1.DataSource = db.GetDataArray_AddRowNum(dtSYMPTON);
      istore1.DataBind();

      if (this.txtTOTAL5.Text == "0")
      {
        this.txtSYMPTON_P.Text = "0";
      }
      else
      {
        this.txtSYMPTON_P.Text = Percent(Convert.ToDouble(this.txtSYMPTON.Text) / Convert.ToDouble(this.txtTOTAL5.Text) * 100);
      }
    }

    protected void btn_Query6_Click(object sender, DirectEventArgs e)
    {
      //有症状
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";

      if (sBEG_DATE == "")
        sBEG_DATE = "2000-01-01";
      if (sEND_DATE == "")
        sEND_DATE = "9999-12-31";
      //找受檢人
      //sSQL = "SELECT B.pif_name, A.pv_ic, B.pif_id " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info B " +
      //           "ON A.pv_ic=B.pif_ic " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //        "ORDER BY B.pif_id ";
      sSQL = "SELECT SUBSTR(A.pv_datevisit,1,7) AS Y_M, COUNT(*) AS TOTAL, 0 AS ERROR, '0' AS ERROR_P " +
               "FROM pat_visit A " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
              "GROUP BY SUBSTR(A.pv_datevisit,1,7) ";
      DataTable dtTOTAL6 = db.Query(sSQL);
      this.txtTOTAL6.Text = dtTOTAL6.Rows.Count.ToString();

      switch (this.txtNODE_ID.Text)
      {
        case "C11": //4.死亡率
          this.GridPanel7.Title = "每月统计 - 死亡率";
          this.txtGridPanel7.Text = "每月统计 - 死亡率";
          sSQL = "SELECT SUBSTR(A.info_date,1,7) AS Y_M, COUNT(*) AS ERROR " +
                   "FROM zinfo_e_01 A " +
                  "WHERE A.info_date>='" + sBEG_DATE + "' " +
                    "AND A.info_date<='" + sEND_DATE + "' " +
                    "AND A.opt_52=4 " +
                  "GROUP BY SUBSTR(A.info_date,1,7) ";
          break;
        case "C7": //1.瘘管重建率
          this.GridPanel7.Title = "每月统计 - 瘘管重建率";
          this.txtGridPanel7.Text = "每月统计 - 瘘管重建率";
          sSQL = "SELECT SUBSTR(A.info_date,1,7) AS Y_M, COUNT(*) AS ERROR " +
                   "FROM zinfo_e_02 A " +
                  "WHERE A.info_date>='" + sBEG_DATE + "' " +
                    "AND A.info_date<='" + sEND_DATE + "' " +
                    "AND A.opt_8=1 " +
                  "GROUP BY SUBSTR(A.info_date,1,7) ";
          break;
        case "C10": //1.住院率
          this.GridPanel7.Title = "每月统计 - 住院率";
          this.txtGridPanel7.Text = "每月统计 - 住院率";
          sSQL = "SELECT SUBSTR(A.info_date,1,7) AS Y_M, COUNT(*) AS ERROR " +
                   "FROM zinfo_e_01 A " +
                  "WHERE A.info_date>='" + sBEG_DATE + "' " +
                    "AND A.info_date<='" + sEND_DATE + "' " +
                    "AND A.opt_26=1 " +
                  "GROUP BY SUBSTR(A.info_date,1,7) ";
          break;
        case "C16": //有症状
          this.GridPanel7.Title = "每月统计 - 血透中有症状";
          this.txtGridPanel7.Text = "每月统计 - 血透中有症状";
          sSQL = "SELECT SUBSTR(A.cln3_date,1,7) AS Y_M, COUNT(*) AS ERROR " +
                   "FROM clinical3_nurse A " +
                  "WHERE A.cln3_date>='" + sBEG_DATE + "' " +
                    "AND A.cln3_date<='" + sEND_DATE + "' " +
                    "AND A.cln3_yn='Y' " +
                  "GROUP BY SUBSTR(A.cln3_date,1,7) ";
          break;
        default:
          break;

      }
      if (sSQL != "")
      {
        DataTable dtSYMPTON2 = db.Query(sSQL);
        this.txtSYMPTON2.Text = dtSYMPTON2.Rows.Count.ToString();

        System.Data.DataView dvTOTAL6;
        dvTOTAL6 = dtTOTAL6.DefaultView;
        for (int i = 0; i < dtSYMPTON2.Rows.Count; i++)
        {
          dvTOTAL6.RowFilter = "Y_M='" + dtSYMPTON2.Rows[i]["Y_M"].ToString() + "' ";
          if (dvTOTAL6.Count > 0)
          {
            dvTOTAL6[0]["ERROR"] = dtSYMPTON2.Rows[i]["ERROR"];
            if (Convert.ToInt16(dvTOTAL6[0]["TOTAL"]) != 0)
              dvTOTAL6[0]["ERROR_P"] = Percent(Convert.ToDouble(dtSYMPTON2.Rows[i]["ERROR"]) * 100 / Convert.ToDouble(dvTOTAL6[0]["TOTAL"]));
          }
        }
        Store istore1 = this.GridPanel7.GetStore();
        istore1.DataSource = db.GetDataArray_AddRowNum(dtTOTAL6);
        istore1.DataBind();

        if (this.txtTOTAL6.Text == "0")
        {
          this.txtSYMPTON2_P.Text = "0";
        }
        else
        {
          this.txtSYMPTON2_P.Text = Percent(Convert.ToDouble(this.txtSYMPTON2.Text) / Convert.ToDouble(this.txtTOTAL6.Text) * 100);
        }
      }
    }

    protected void btn_Query7_Click(object sender, DirectEventArgs e)
    {
      //轉歸率
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";

      if (sBEG_DATE == "")
        sBEG_DATE = "2000-01-01";
      if (sEND_DATE == "")
        sEND_DATE = "9999-12-31";
      //找受檢人
      //sSQL = "SELECT B.pif_name, A.pv_ic, B.pif_id " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info B " +
      //           "ON A.pv_ic=B.pif_ic " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //        "ORDER BY B.pif_id ";
      sSQL = "SELECT DISTINCT A.pv_ic, A.pv_datevisit " +
               "FROM pat_visit A " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' ";
      sSQL = "SELECT SUBSTR(A.pv_datevisit,1,7) AS Y_M, COUNT(*) AS TOTAL, 0 AS ERROR, '0' AS ERROR_P " +
               "FROM pat_visit A " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
              "GROUP BY SUBSTR(A.pv_datevisit,1,7) ";
      DataTable dtTOTAL7 = db.Query(sSQL);
      this.txtTOTAL7.Text = dtTOTAL7.Rows.Count.ToString();

      //找轉歸的人
      sSQL = "SELECT DISTINCT SUBSTR(A.info_date,1,7) AS Y_M, COUNT(G.opt_52) AS TOTAL, " +
                    "COUNT(B.opt_52) AS TYPE1, '0' AS TYPE1_P, " +
                    "COUNT(C.opt_52) AS TYPE2, '0' AS TYPE2_P, " +
                    "COUNT(D.opt_52) AS TYPE3, '0' AS TYPE3_P, " +
                    "COUNT(E.opt_52) AS TYPE4, '0' AS TYPE4_P, " +
                    "COUNT(F.opt_52) AS TYPE5, '0' AS TYPE5_P " +
                       "FROM zinfo_e_01 A " +
                       "LEFT JOIN zinfo_e_01 B " +
                         "ON A.pat_id=B.pat_id " +
                        "AND A.info_date=B.info_date " +
                        "AND B.opt_52=1 " +
                       "LEFT JOIN zinfo_e_01 C " +
                         "ON A.pat_id=C.pat_id " +
                        "AND A.info_date=C.info_date " +
                        "AND C.opt_52=2 " +
                       "LEFT JOIN zinfo_e_01 D " +
                         "ON A.pat_id=D.pat_id " +
                        "AND A.info_date=D.info_date " +
                        "AND D.opt_52=3 " +
                       "LEFT JOIN zinfo_e_01 E " +
                         "ON A.pat_id=E.pat_id " +
                        "AND A.info_date=E.info_date " +
                        "AND E.opt_52=4 " +
                       "LEFT JOIN zinfo_e_01 F " +
                         "ON A.pat_id=B.pat_id " +
                        "AND A.info_date=F.info_date " +
                        "AND F.opt_52=5 " +
                       "LEFT JOIN zinfo_e_01 G " +
                         "ON A.pat_id=G.pat_id " +
                        "AND A.info_date=G.info_date " +
                        "AND G.opt_52<>0 " +
                      "WHERE A.info_date>='" + sBEG_DATE + "' " +
                        "AND A.info_date<='" + sEND_DATE + "' " +
                      "GROUP BY SUBSTR(A.info_date,1,7) ";

      DataTable dtCHANGE = db.Query(sSQL);
      this.txtCHANGE.Text = dtCHANGE.Rows.Count.ToString();
      for (int i = 0; i < dtCHANGE.Rows.Count; i++)
      {
        if (Convert.ToInt16(dtCHANGE.Rows[i]["TOTAL"]) != 0)
        {
          dtCHANGE.Rows[i]["TYPE1_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE1"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
          dtCHANGE.Rows[i]["TYPE2_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE2"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
          dtCHANGE.Rows[i]["TYPE3_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE3"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
          dtCHANGE.Rows[i]["TYPE4_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE4"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
          dtCHANGE.Rows[i]["TYPE5_P"] = Percent(Convert.ToDouble(dtCHANGE.Rows[i]["TYPE5"]) * 100 / Convert.ToDouble(dtCHANGE.Rows[i]["TOTAL"])) + "%";
        }
        else
        {
          dtCHANGE.Rows[i]["TYPE1_P"] = "";
          dtCHANGE.Rows[i]["TYPE2_P"] = "";
          dtCHANGE.Rows[i]["TYPE3_P"] = "";
          dtCHANGE.Rows[i]["TYPE4_P"] = "";
          dtCHANGE.Rows[i]["TYPE5_P"] = "";
        }
      }
      Store istore1 = this.GridPanel8.GetStore();
      istore1.DataSource = db.GetDataArray_AddRowNum(dtCHANGE);
      istore1.DataBind();

      if (this.txtTOTAL7.Text == "0")
      {
        this.txtCHANGE_P.Text = "0";
      }
      else
      {
        this.txtCHANGE_P.Text = Percent(Convert.ToDouble(this.txtCHANGE.Text) / Convert.ToDouble(this.txtTOTAL7.Text) * 100);
      }
    }

    protected void btn_Query8_Click(object sender, DirectEventArgs e)
    {
      //
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";
      string sRESULT_CODE = this.txtRESULT_CODE2.Text;
      int iCHECK;
      int iCHECK_Y;
      int iCHECK_N;

      if (sRESULT_CODE != "")
      {
        if (sBEG_DATE == "")
          sBEG_DATE = "2000-01-01";
        if (sEND_DATE == "")
          sEND_DATE = "9999-12-31";
        //找檢查項目
        sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
        DataTable dtCODE2 = db.Query(sSQL);
        if (dtCODE2.Rows.Count > 0)
        {
          this.txtRESULT_NAME2.Text = dtCODE2.Rows[0]["RITEM_NAME"].ToString();
          this.txtRESULT_UNIT2.Text = dtCODE2.Rows[0]["RITEM_UNIT"].ToString();
          this.txtNORMAL2.Text = dtCODE2.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE2.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE2.Rows[0]["RITEM_UNIT"].ToString();
        }

        //找受檢人
        sSQL = "SELECT SUBSTR(A.pv_datevisit,1,7) AS Y_M, COUNT(*) AS TOTAL, " +
                      "0 AS CHECKS, '0' AS CHECKS_P, 0 AS UNCHECK, '0' AS UNCHECK_P, " +
                      "0 AS CHECK_Y, '0' AS CHECK_Y_P, 0 AS CHECK_N, '0' AS CHECK_N_P " +
                 "FROM pat_visit A " +
                "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                  "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "GROUP BY SUBSTR(A.pv_datevisit,1,7) ";
        DataTable dtTOTAL8 = db.Query(sSQL);
        this.txtTOTAL8.Text = dtTOTAL8.Rows.Count.ToString();

        //找有做檢查的人
        sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N, SUBSTR(A.RESULT_DATE,1,7) AS Y_M " +
                 "FROM a_result_log A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.PAT_NO=B.pif_id " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic, Y_M ";
        DataTable dtCHECK2 = db.Query(sSQL);

        System.Data.DataView dvCHECK2;
        dvCHECK2 = dtCHECK2.DefaultView;
        //System.Data.DataView dvCHECK2_Y;
        //dvCHECK2_Y = dtCHECK2.DefaultView;
        //System.Data.DataView dvCHECK2_N;
        //dvCHECK2_N = dtCHECK2.DefaultView;

        for (int i = 0; i < dtTOTAL8.Rows.Count; i++)
        {
          //算受檢人數
          dvCHECK2.RowFilter = "Y_M='" + dtTOTAL8.Rows[i]["Y_M"].ToString() + "' ";
          iCHECK = dvCHECK2.Count;
          dtTOTAL8.Rows[i]["CHECKS"] = iCHECK;
          dtTOTAL8.Rows[i]["UNCHECK"] = Convert.ToInt16(dtTOTAL8.Rows[i]["TOTAL"]) - iCHECK;

          //算合格人數
          dvCHECK2.RowFilter = "Y_M='" + dtTOTAL8.Rows[i]["Y_M"].ToString() + "' " +
                          " AND RESULT_VALUE_N>=" + dtCODE2.Rows[0]["RITEM_LOW1"].ToString() +
                          " AND RESULT_VALUE_N<=" + dtCODE2.Rows[0]["RITEM_HIGH1"].ToString();
          iCHECK_Y = dvCHECK2.Count;
          dtTOTAL8.Rows[i]["CHECK_Y"] = iCHECK_Y;

          //算不合格人數
          dvCHECK2.RowFilter = "Y_M='" + dtTOTAL8.Rows[i]["Y_M"].ToString() + "' " +
                          " AND (RESULT_VALUE_N<" + dtCODE2.Rows[0]["RITEM_LOW1"].ToString() +
                           " OR RESULT_VALUE_N>" + dtCODE2.Rows[0]["RITEM_HIGH1"].ToString() + ") ";
          iCHECK_N = dvCHECK2.Count;
          dtTOTAL8.Rows[i]["CHECK_N"] = iCHECK_N;

          if (iCHECK != 0)
          {
            dtTOTAL8.Rows[i]["CHECK_Y_P"] = Percent(Convert.ToDouble(iCHECK_Y) * 100 / Convert.ToDouble(iCHECK)) + "%";
            dtTOTAL8.Rows[i]["CHECK_N_P"] = Percent(Convert.ToDouble(iCHECK_N) * 100 / Convert.ToDouble(iCHECK)) + "%";
          }
          if (Convert.ToInt16(dtTOTAL8.Rows[i]["TOTAL"]) != 0)
          {
            dtTOTAL8.Rows[i]["CHECKS_P"] = Percent(Convert.ToDouble(iCHECK) * 100 / Convert.ToDouble(dtTOTAL8.Rows[i]["TOTAL"])) + "%";
            dtTOTAL8.Rows[i]["UNCHECK_P"] = Percent(Convert.ToDouble(Convert.ToInt16(dtTOTAL8.Rows[i]["TOTAL"]) - iCHECK) * 100 / Convert.ToDouble(dtTOTAL8.Rows[i]["TOTAL"])) + "%";
          }

        }

        Store istore1 = this.GridPanel9.GetStore();
        istore1.DataSource = db.GetDataArray_AddRowNum(dtTOTAL8);
        istore1.DataBind();

      }
    }

    protected void btn_Query9_Click(object sender, DirectEventArgs e)
    {
      //檢驗 HBsAg 4032, AntiHCV 4033
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";
      string sRESULT_CODE = this.txtRESULT_CODE3.Text;
      if (sRESULT_CODE != "")
      {
        if (sBEG_DATE == "")
          sBEG_DATE = "2000-01-01";
        if (sEND_DATE == "")
          sEND_DATE = "9999-12-31";
        //找檢查項目
        sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
        DataTable dtCODE3 = db.Query(sSQL);
        if (dtCODE3.Rows.Count > 0)
        {
          this.txtRESULT_NAME3.Text = dtCODE3.Rows[0]["RITEM_NAME"].ToString();
          this.txtRESULT_UNIT3.Text = dtCODE3.Rows[0]["RITEM_UNIT"].ToString();
          this.txtNORMAL3.Text = dtCODE3.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE3.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE3.Rows[0]["RITEM_UNIT"].ToString();
        }

        //找受檢人
        //sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
        //         "FROM pat_visit A " +
        //         "LEFT JOIN pat_info B " +
        //           "ON A.pv_ic=B.pif_ic " +
        //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
        //        "ORDER BY B.pif_id ";
        //DataTable dtTOTAL = db.Query(sSQL);
        //this.txtTOTAL1.Text = dtTOTAL.Rows.Count.ToString();

        //找有做檢查的人
        sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + this.txtRESULT_NAME3.Text + "' AS RESULT_NAME,  A.RESULT_VALUE_T " +
                 "FROM a_result_log A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.PAT_NO=B.pif_id " +
                " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                  "AND A.RESULT_VALUE_T IN ('阴性','阳性') " +
                "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
        DataTable dtTOTAL9 = db.Query(sSQL);
        this.txtTOTAL9.Text = dtTOTAL9.Rows.Count.ToString();

        //old ?? Store istore2 = this.GridPanel12.GetStore();
        Store istore2 = this.GridPanel11.GetStore();
        istore2.DataSource = db.GetDataArray_AddRowNum(dtTOTAL9);
        istore2.DataBind();

        sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + this.txtRESULT_NAME3.Text + "' AS RESULT_NAME, A.RESULT_VALUE_T " +
                 "FROM a_result_log A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.PAT_NO=B.pif_id " +
                 " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                  "AND A.RESULT_VALUE_T='" + "阳性" + "' " +
                "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
        DataTable dtPOSITIVE = db.Query(sSQL);
        this.txtPOSITIVE.Text = dtPOSITIVE.Rows.Count.ToString();

        Store istore1 = this.GridPanel10.GetStore();
        istore1.DataSource = db.GetDataArray_AddRowNum(dtPOSITIVE);
        istore1.DataBind();
      }
    }

    protected void btn_Query10_Click(object sender, DirectEventArgs e)
    {
      //檢驗 HBsAg, AntiHCV
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";
      string sRESULT_CODE = this.txtRESULT_CODE4.Text;
      if (sRESULT_CODE != "")
      {
        if (sBEG_DATE == "")
          sBEG_DATE = "2000-01-01";
        if (sEND_DATE == "")
          sEND_DATE = "9999-12-31";
        //找檢查項目
        sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
        DataTable dtCODE4 = db.Query(sSQL);
        if (dtCODE4.Rows.Count > 0)
        {
          this.txtRESULT_NAME4.Text = dtCODE4.Rows[0]["RITEM_NAME"].ToString();
          this.txtRESULT_UNIT4.Text = dtCODE4.Rows[0]["RITEM_UNIT"].ToString();
          this.txtNORMAL4.Text = dtCODE4.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE4.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE4.Rows[0]["RITEM_UNIT"].ToString();
        }

        //找受檢人
        //sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
        //         "FROM pat_visit A " +
        //         "LEFT JOIN pat_info B " +
        //           "ON A.pv_ic=B.pif_ic " +
        //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
        //        "ORDER BY B.pif_id ";
        //DataTable dtTOTAL = db.Query(sSQL);
        //this.txtTOTAL1.Text = dtTOTAL.Rows.Count.ToString();

        //找有做檢查的人
        sSQL = "SELECT SUBSTR(A.RESULT_DATE,1,7) AS Y_M, COUNT(*) AS TOTAL, 0 AS ERROR, '0' AS ERROR_P " +
                 "FROM a_result_log A " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                  "AND A.RESULT_VALUE_T IN ('阴性','阳性') " +
                "GROUP BY SUBSTR(A.RESULT_DATE,1,7) ";
        DataTable dtTOTAL10 = db.Query(sSQL);
        this.txtTOTAL10.Text = dtTOTAL10.Rows.Count.ToString();

        sSQL = "SELECT SUBSTR(A.RESULT_DATE,1,7) AS Y_M, COUNT(*) AS ERROR " +
                 "FROM a_result_log A " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                  "AND A.RESULT_VALUE_T='" + "阳性" + "' " +
                "GROUP BY SUBSTR(A.RESULT_DATE,1,7) ";
        DataTable dtPOSITIVE2 = db.Query(sSQL);

        System.Data.DataView dvTOTAL10;
        dvTOTAL10 = dtTOTAL10.DefaultView;
        for (int i = 0; i < dtPOSITIVE2.Rows.Count; i++)
        {
          dvTOTAL10.RowFilter = "Y_M='" + dtPOSITIVE2.Rows[i]["Y_M"].ToString() + "' ";
          if (dvTOTAL10.Count > 0)
          {
            dvTOTAL10[0]["ERROR"] = dtPOSITIVE2.Rows[i]["ERROR"];
            if (Convert.ToInt16(dvTOTAL10[0]["TOTAL"]) != 0)
              dvTOTAL10[0]["ERROR_P"] = Percent(Convert.ToDouble(dtPOSITIVE2.Rows[i]["ERROR"]) * 100 / Convert.ToDouble(dvTOTAL10[0]["TOTAL"]));
          }
        }

        Store istore1 = this.GridPanel11.GetStore();
        istore1.DataSource = db.GetDataArray_AddRowNum(dtTOTAL10);
        istore1.DataBind();
      }
    }


    //20150803 andy [監控數據查詢]
    protected void btn_Print11Q_Click(object sender, DirectEventArgs e)
    {

      sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=98&_REPORT_P=1";
      sURL += "&_REPORT_sYEAR=" + sYEAR.Text + "&_REPORT_sQT=" + sQT.Text;
      Window1.Show();
      Window1.Loader.SuspendScripting();
      Window1.Loader.Url = sURL;
      Window1.Loader.DisableCaching = true;
      Window1.LoadContent();
    }

    //20150627 ANDY [監控數據查詢]
    protected void btn_Query11Q_Click(object sender, DirectEventArgs e)
    {
      //2015-06-01 2015-06-30
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";
      //年代码
      //string sRESULT_CODE = this.txtRESULT_CODE11Q.Text;
      string sYEAR_CODE = this.sYEAR.Text;
      string sQT_CODE = this.sQT.Text;
      string w_year = "";
      string w_QT = "";
      if (sYEAR_CODE == "002")
      {
        w_year = "2013";
      }
      if (sYEAR_CODE == "003")
      {
        w_year = "2014";
      }
      if (sYEAR_CODE == "004")
      {
        w_year = "2015";
      }
      if (sYEAR_CODE == "005")
      {
        w_year = "2016";
      }
      if (sYEAR_CODE == "006")
      {
        w_year = "2017";
      }
      if (sYEAR_CODE == "007")
      {
        w_year = "2018";
      }

      if (sQT_CODE == "002")
      {
        w_QT = "";
      }
      if (sQT_CODE == "003")
      {
        w_QT = "月";
      }
      if (sQT_CODE == "004")
      {
        w_QT = "季";
      }
      if (sQT_CODE == "005")
      {
        w_QT = "半年";
      }
      if (sQT_CODE == "006")
      {
        w_QT = "年";
      }

      if (sBEG_DATE == "")
        sBEG_DATE = "2013-01-01";
      if (sEND_DATE == "")
        sEND_DATE = "2020-12-31";

      //sql = " insert into  hospital_quality_history  ( hq_date1,hq_date2,hq_date3,hq_txt_10,hq_name,hq_num1,hq_d1,hq_d11,hq_d3,hq_d31,hq_d4,hq_d41,";
      //sql += " hq_d5,hq_d51,hq_d6,hq_d61,hq_d7,hq_d71,hq_d8,hq_d81,hq_d9,hq_d91,hq_d10,hq_d11a,hq_d12,hq_d13,hq_d14) ";

      sSQL = "SELECT hq_date1,hq_date2,hq_txt_10," +
             " hq_name,hq_num1,hq_d1,hq_d11,hq_d3,hq_d31,hq_d4,hq_d41,hq_d5,hq_d51,hq_d6,hq_d61,hq_d7,hq_d71,hq_d8,hq_d81,hq_d9,hq_d91,hq_d10,hq_d11a,hq_d12,hq_d13,hq_d14" +
             " FROM hospital_quality_history";
      //20150627 年度
      if (w_year != "" && w_QT == "年")
      {
        sSQL += " where hq_date1 like '" + w_year + "%'";
        sSQL += " and hq_name ='年報'";
        sSQL += " order by hq_date1,hq_name";
      }
      if (w_year != "" && w_QT == "")
      {
        sSQL += " where hq_date1 like '" + w_year + "%'";
        sSQL += " order by hq_date1,hq_name";
      }
      if (w_year != "" && w_QT == "季")
      {
        sSQL += " where hq_date1 like '" + w_year + "%'";
        sSQL += " and hq_name ='季報'";
        sSQL += " order by hq_date1,hq_name";
      }
      if (w_year != "" && w_QT == "半年")
      {
        sSQL += " where hq_date1 like '" + w_year + "%'";
        sSQL += " and hq_name ='半年報'";
        sSQL += " order by hq_date1,hq_name";
      }
      if (w_year != "" && w_QT == "月")
      {
        sSQL += " where hq_date1 like '" + w_year + "%'";
        sSQL += " and hq_name ='月報'";
        sSQL += " order by hq_date1,hq_name";
      }
      DataTable dtCHECK = db.Query(sSQL);

      System.Data.DataView dvCHECK;
      dvCHECK = dtCHECK.DefaultView;

      Store istore1 = this.GridPanel13Q.GetStore();
      istore1.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
      istore1.DataBind();
    }
    //

    protected void btn_Query11_Click(object sender, DirectEventArgs e)
    {
      //檢驗
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";
      string sRESULT_CODE = this.txtRESULT_CODE11.Text;
      double dLOW = 0;
      double dHIGH = 0;

      if (Double.TryParse(txtRESULT_LOW.Text, out dLOW))
        dLOW = Convert.ToDouble(txtRESULT_LOW.Text);
      if (Double.TryParse(txtRESULT_HIGH.Text, out dHIGH))
        dHIGH = Convert.ToDouble(txtRESULT_HIGH.Text);

      txtRESULT_LOW.Text = dLOW.ToString();
      txtRESULT_HIGH.Text = dHIGH.ToString();

      if ((sRESULT_CODE != "") && (dHIGH >= dLOW) && (dHIGH > 0))
      {
        if (sBEG_DATE == "")
          sBEG_DATE = "2000-01-01";
        if (sEND_DATE == "")
          sEND_DATE = "9999-12-31";
        //找檢查項目
        //sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
        //DataTable dtCODE = db.Query(sSQL);
        //if (dtCODE.Rows.Count > 0)
        //{
        //    this.txtRESULT_NAME11.Text = dtCODE.Rows[0]["RITEM_NAME"].ToString();
        //    this.txtRESULT_UNIT11.Text = dtCODE.Rows[0]["RITEM_UNIT"].ToString();
        //    this.txtNORMAL11.Text = dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE.Rows[0]["RITEM_UNIT"].ToString();
        //}

        //找受檢人
        sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                 "FROM pat_visit A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.pv_ic=B.pif_ic " +
                   " left join zinfo_a_07 c on B.pif_id     = c.pat_id and c.opt_1 in('','5') " +
                "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                  "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "ORDER BY B.pif_id ";
        DataTable dtTOTAL11 = db.Query(sSQL);
        //this.txtTOTAL11.Text = dtTOTAL11.Rows.Count.ToString();

        //找有做檢查的人
        sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                 "FROM a_result_log A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.PAT_NO=B.pif_id " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
        sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, '" + cboCODE11.Text + "' AS RESULT_NAME, A.RESULT_VALUE_N " +
                 "FROM a_result_log A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.PAT_NO=B.pif_id " +
                 "  left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_VALUE_N>=" + dLOW.ToString() + " " +
                  "AND A.RESULT_VALUE_N<=" + dHIGH.ToString() + " " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                "ORDER BY A.PAT_NO, A.RESULT_CODE, A.RESULT_DATE, B.pif_name, B.pif_ic ";
        DataTable dtCHECK = db.Query(sSQL);
        //this.txtCHECK2.Text = dtCHECK.Rows.Count.ToString();

        //算合格人數
        System.Data.DataView dvCHECK;
        dvCHECK = dtCHECK.DefaultView;
        //dvCHECK.RowFilter = "RESULT_VALUE_N>=" + dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE.Rows[0]["RITEM_HIGH1"].ToString();
        //this.txtCHECK_Y2.Text = dvCHECK.Count.ToString();

        //算不合格人數
        //System.Data.DataView dvCHECK_N;
        //dvCHECK_N = dtCHECK.DefaultView;
        //dvCHECK.RowFilter = "RESULT_VALUE_N<" + dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE.Rows[0]["RITEM_HIGH1"].ToString();
        //this.txtCHECK_N2.Text = dvCHECK.Count.ToString();

        Store istore1 = this.GridPanel13.GetStore();
        istore1.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
        istore1.DataBind();

        //this.txtERR.Text = ""; 
        //受檢人-有做檢查的人
        //DataTable dtUNCHECK = dtTOTAL11.Copy();
        //System.Data.DataView dvUNCHECK;
        //dvUNCHECK = dtUNCHECK.DefaultView;
        //for (int i = 0; i < dtCHECK.Rows.Count; i++)
        //{
        //    dvUNCHECK.RowFilter = "pv_ic='" + dtCHECK.Rows[i]["pif_ic"] + "'";
        //    if (dvUNCHECK.Count > 0)
        //        dvUNCHECK[0].Delete();
        //    //else
        //    //    this.txtERR.Text += "{" + dtCHECK.Rows[i]["pif_name"].ToString() + "-" + dtCHECK.Rows[i]["pif_ic"].ToString() + "," + dtCHECK.Rows[i]["PAT_NO"].ToString() + "}";
        //}

        //dtUNCHECK.AcceptChanges();
        //this.txtUNCHECK2.Text = dtUNCHECK.Rows.Count.ToString();
        //Store istore2 = this.GridPanel13.GetStore();
        //istore2.DataSource = db.GetDataArray_AddRowNum(dtUNCHECK);
        //istore2.DataBind();

        //if (this.txtTOTAL11.Text == "0")
        //{
        //    this.txtCHECK_P2.Text = "0";
        //    this.txtUNCHECK_P2.Text = "0";
        //}
        //else
        //{
        //    this.txtCHECK_P2.Text = Percent(Convert.ToDouble(this.txtCHECK2.Text) / Convert.ToDouble(this.txtTOTAL11.Text) * 100);
        //    this.txtUNCHECK_P2.Text = Percent(Convert.ToDouble(this.txtUNCHECK2.Text) / Convert.ToDouble(this.txtTOTAL11.Text) * 100);
        //}

        //if (this.txtCHECK2.Text == "0")
        //{
        //    this.txtCHECK_YP2.Text = "0";
        //    this.txtCHECK_NP2.Text = "0";
        //}
        //else
        //{
        //    this.txtCHECK_YP2.Text = Percent(Convert.ToDouble(this.txtCHECK_Y2.Text) / Convert.ToDouble(this.txtCHECK2.Text) * 100);
        //    this.txtCHECK_NP2.Text = Percent(Convert.ToDouble(this.txtCHECK_N2.Text) / Convert.ToDouble(this.txtCHECK2.Text) * 100);
        //}
      }
    }

    protected void btn_Query12_Click(object sender, DirectEventArgs e)
    {
      //檢驗
      string sBEG_DATE = _Get_YMD2(this.txtBEG_DATE12.Text);
      string sEND_DATE = _Get_YMD2(this.txtEND_DATE12.Text);
      string sSQL = "";
      string sRESULT_CODE = this.txtRESULT_CODE12.Text;

      //txtRESULT_LOW12.Text = dLOW.ToString();
      //txtRESULT_HIGH12.Text = dHIGH.ToString();

      if (sRESULT_CODE != "")
      {
        if (sBEG_DATE == "")
          sBEG_DATE = "2000-01-01";
        if (sEND_DATE == "")
          sEND_DATE = "9999-12-31";
        //找檢查項目
        //sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
        //DataTable dtCODE = db.Query(sSQL);
        //if (dtCODE.Rows.Count > 0)
        //{
        //    this.txtRESULT_NAME12.Text = dtCODE.Rows[0]["RITEM_NAME"].ToString();
        //    this.txtRESULT_UNIT12.Text = dtCODE.Rows[0]["RITEM_UNIT"].ToString();
        //    this.txtNORMAL12.Text = dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE.Rows[0]["RITEM_UNIT"].ToString();
        //}

        DataRow[] dr;
        dr = dt_ritem_setup12.Select("RITEM_CODE='" + GetComboBoxValue(cboCODE12) + "' ");

        //找受檢人
        //sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
        //         "FROM pat_visit A " +
        //         "LEFT JOIN pat_info B " +
        //           "ON A.pv_ic=B.pif_ic " +
        //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
        //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
        //        "ORDER BY B.pif_id ";
        //DataTable dtTOTAL12 = db.Query(sSQL);
        //this.txtTOTAL12.Text = dtTOTAL11.Rows.Count.ToString();

        //找有做檢查的人
        sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, '" + txtRESULT_NAME12.Text + "' AS RESULT_NAME, A.RESULT_VALUE_N " +
                 "FROM a_result_log A " +
                 "LEFT JOIN pat_info B " +
                   "ON A.PAT_NO=B.pif_id " +
                   " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                "WHERE A.RESULT_VER=0 " +
                  "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                  "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                  "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                  "AND (B.PIF_NAME LIKE '%" + txtPERSON_NAME12.Text + "%' OR B.PIF_IC LIKE '%" + txtPERSON_NAME12.Text + "%') " +
                "ORDER BY A.PAT_NO, A.RESULT_CODE, A.RESULT_DATE, B.pif_name, B.pif_ic ";
        DataTable dtCHECK = db.Query(sSQL);

        Store istore1 = this.GridPanel15.GetStore();
        istore1.DataSource = db.GetDataArray_AddRowNum(dtCHECK);
        istore1.DataBind();

        int iCHART_L = 999;
        int iCHART_H = 0;
        double dd = 0;
        if (Double.TryParse(dr[0]["RITEM_HIGH1"].ToString(), out dd))
          dd = Convert.ToDouble(dr[0]["RITEM_HIGH1"].ToString());
        if (dd > iCHART_H)
          iCHART_H = Convert.ToInt16(dd);
        if (dd > iCHART_H)
          iCHART_H = iCHART_H + 1;

        if (Double.TryParse(dr[0]["RITEM_LOW1"].ToString(), out dd))
          dd = Convert.ToDouble(dr[0]["RITEM_LOW1"].ToString());
        if (dd < iCHART_L)
          iCHART_L = Convert.ToInt16(dd);

        object[] oo = new Object[dtCHECK.Rows.Count];

        for (int i = 0; i < dtCHECK.Rows.Count; i++)
        {
          if (Double.TryParse(dtCHECK.Rows[i]["RESULT_VALUE_N"].ToString(), out dd))
            dd = Convert.ToDouble(dtCHECK.Rows[i]["RESULT_VALUE_N"]);
          if (dd > iCHART_H)
            iCHART_H = Convert.ToInt16(dd);
          if (dd > iCHART_H)
            iCHART_H = iCHART_H + 1;
          if (dd < iCHART_L)
            iCHART_L = Convert.ToInt16(dd);

          oo[i] = new
          {
            RESULT_DATE = "(" + (i + 1).ToString() + ") " + dtCHECK.Rows[i]["RESULT_DATE"],
            RESULT_VALUE_N = dtCHECK.Rows[i]["RESULT_VALUE_N"],
            RESULT_VALUE_L = Convert.ToDouble(dr[0]["RITEM_LOW1"].ToString()),
            RESULT_VALUE_H = Convert.ToDouble(dr[0]["RITEM_HIGH1"].ToString())
          };
        }

        //((CategoryAxis)Chart1.Axes[0]).SetTitle("检验日期 " + txtRESULT_NAME12.Text +" "+ this.txtNORMAL12.Text);

        ((NumericAxis)Chart1.Axes[1]).SetMinimum(iCHART_L);
        ((NumericAxis)Chart1.Axes[1]).SetMaximum(iCHART_H);
        this.Chart1.GetStore().Data = oo;
        this.Chart1.GetStore().DataBind();

      }
    }

    protected void btn_Query13_Click(object sender, DirectEventArgs e)
    {
      //耗材使用数量
      string sBEG_DATE = _Get_YMD2(this.beg_date.Text);
      string sEND_DATE = _Get_YMD2(this.end_date.Text);
      string sSQL = "";

      if (sBEG_DATE == "")
        sBEG_DATE = "2000-01-01";
      if (sEND_DATE == "")
        sEND_DATE = "9999-12-31";

      //sSQL = "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, A.pv_datevisit AS USE_DATE, D.hp2_name AS ITEM_NAME, 1 AS ITEM_CNT " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info C " +
      //           "ON A.pv_ic=C.pif_ic " +
      //         "LEFT JOIN hpack2_setup D " +
      //           "ON A.pv_hpack2=D.hp2_code " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //          "AND D.hp2_name IS NOT NULL " +
      //        "UNION ALL " +
      //       "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, A.pv_datevisit AS USE_DATE, D.hp3_name AS ITEM_NAME, 1 AS ITEM_CNT " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info C " +
      //           "ON A.pv_ic=C.pif_ic " +
      //         "LEFT JOIN hpack3_setup D " +
      //           "ON A.pv_hpack3=D.hp3_code " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //          "AND D.hp3_name IS NOT NULL " +
      //        "UNION ALL " +
      //       "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, A.pv_datevisit AS USE_DATE, D.pdet_itemnm AS ITEM_NAME, pdet_qty AS ITEM_CNT " +
      //         "FROM pat_visit A " +
      //         "LEFT JOIN pat_info C " +
      //           "ON A.pv_ic=C.pif_ic " +
      //         "LEFT JOIN package_detail2 D " +
      //           "ON A.pv_hpack=D.pdet_code " +
      //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
      //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
      //          "AND D.pdet_itemnm IS NOT NULL " +
      //        "ORDER BY PERSON_IC, USE_DATE, ITEM_NAME ";
      sSQL = "SELECT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, A.pv_datevisit AS USE_DATE, D.hp2_name AS ITEM_NAME, 1 AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN pat_info C " +
                 "ON A.pv_ic=C.pif_ic " +
               "LEFT JOIN hpack2_setup D " +
                 "ON A.pv_hpack2=D.hp2_code " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.hp2_name IS NOT NULL " +
              "UNION ALL " +
             "SELECT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, A.pv_datevisit AS USE_DATE, D.hp3_name AS ITEM_NAME, 1 AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN pat_info C " +
                 "ON A.pv_ic=C.pif_ic " +
               "LEFT JOIN hpack3_setup D " +
                 "ON A.pv_hpack3=D.hp3_code " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.hp3_name IS NOT NULL " +
              "UNION ALL " +
             "SELECT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, A.pv_datevisit AS USE_DATE, D.pdet_itemnm AS ITEM_NAME, pdet_qty AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN pat_info C " +
                 "ON A.pv_ic=C.pif_ic " +
               "LEFT JOIN package_detail2 D " +
                 "ON A.pv_hpack=D.pdet_code " +
                "AND D.pdet_itemcd NOT IN ('0001','0002') " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.pdet_itemnm IS NOT NULL " +
              "ORDER BY PERSON_IC, USE_DATE, ITEM_NAME ";
      DataTable dt18 = db.Query(sSQL);
      Store istore18 = this.GridPanel18.GetStore();
      istore18.DataSource = db.GetDataArray_AddRowNum(dt18);
      istore18.DataBind();

      sSQL = "SELECT A.pv_datevisit AS USE_DATE, D.hp2_name AS ITEM_NAME, COUNT(*) AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN hpack2_setup D " +
                 "ON A.pv_hpack2=D.hp2_code " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.hp2_name IS NOT NULL " +
              "GROUP BY A.pv_datevisit, D.hp2_name " +
              "UNION ALL " +
             "SELECT A.pv_datevisit AS USE_DATE, D.hp3_name AS ITEM_NAME, COUNT(*) AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN hpack3_setup D " +
                 "ON A.pv_hpack3=D.hp3_code " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.hp3_name IS NOT NULL " +
              "GROUP BY A.pv_datevisit, D.hp3_name " +
              "UNION ALL " +
             "SELECT A.pv_datevisit AS USE_DATE, D.pdet_itemnm AS ITEM_NAME, SUM(pdet_qty) AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN package_detail2 D " +
                 "ON A.pv_hpack=D.pdet_code " +
                "AND D.pdet_itemcd NOT IN ('0001','0002') " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.pdet_itemnm IS NOT NULL " +
              "GROUP BY A.pv_datevisit, D.pdet_itemnm " +
              "ORDER BY USE_DATE, ITEM_NAME ";
      DataTable dt17 = db.Query(sSQL);
      Store istore17 = this.GridPanel17.GetStore();
      istore17.DataSource = db.GetDataArray_AddRowNum(dt17);
      istore17.DataBind();

      sSQL = "SELECT D.hp2_name AS ITEM_NAME, COUNT(*) AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN hpack2_setup D " +
                 "ON A.pv_hpack2=D.hp2_code " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.hp2_name IS NOT NULL " +
              "GROUP BY D.hp2_name " +
              "UNION ALL " +
             "SELECT D.hp3_name AS ITEM_NAME, COUNT(*) AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN hpack3_setup D " +
                 "ON A.pv_hpack3=D.hp3_code " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.hp3_name IS NOT NULL " +
              "GROUP BY D.hp3_name " +
              "UNION ALL " +
             "SELECT D.pdet_itemnm AS ITEM_NAME, SUM(pdet_qty) AS ITEM_CNT " +
               "FROM pat_visit A " +
               "LEFT JOIN package_detail2 D " +
                 "ON A.pv_hpack=D.pdet_code " +
              "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                "AND D.pdet_itemnm IS NOT NULL " +
                "AND D.pdet_itemcd NOT IN ('0001','0002') " +
              "GROUP BY D.pdet_itemnm " +
              "ORDER BY ITEM_NAME ";
      DataTable dt16 = db.Query(sSQL);
      Store istore16 = this.GridPanel16.GetStore();
      istore16.DataSource = db.GetDataArray_AddRowNum(dt16);
      istore16.DataBind();

    }

    protected void btn_Query13X_Click()
    {
      //耗材使用数量
      DateTime dBEG = DateTime.Now;
      DateTime dEND = DateTime.Now;
      dBEG = Convert.ToDateTime(dBEG.ToString("yyyy-MM-") + "01");
      string sBEG_DATE = dBEG.ToString("yyyy-MM-dd");
      dEND = dBEG;
      dEND = dEND.AddMonths(1);
      dEND = dEND.AddDays(-1);
      string sEND_DATE = dEND.ToString("yyyy-MM-dd");
      string sSQL = "";
//            Lab_patid.Text =  "身份證號 : " + sel_PAT_NO;
      this.GridPanel18.Hidden = true;
      Lab_amount.Text = "";
      this.Column156.Hidden = true; 

      sSQL = "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, COUNT(D.hp2_name) AS USE_DATE, D.hp2_name AS ITEM_NAME, COUNT(D.hp2_name) AS ITEM_CNT, e.price*COUNT(d.hp2_name) AS ITEM_SUM   " +
                "FROM pat_visit A  " +
                 "LEFT JOIN pat_info C ON A.pv_ic=C.pif_ic  " +
                 "LEFT JOIN hpack2_setup D ON A.pv_hpack2=D.hp2_code  " +
                 "LEFT JOIN price as e on D.hp2_code = e.hp2_code  " +
                "WHERE A.pv_datevisit>='" + sBEG_DATE + "' AND A.pv_datevisit<='" + sEND_DATE + "' AND A.pv_ic='" + sel_PAT_NO + "' AND D.hp2_name IS NOT NULL  " +
                "GROUP BY D.hp2_name  " +
               "UNION ALL  " +
               "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, COUNT(D.hp3_name) AS USE_DATE, D.hp3_name AS ITEM_NAME, COUNT(D.hp3_name) AS ITEM_CNT, e.price*COUNT(d.hp3_name) AS ITEM_SUM  " +
                "FROM pat_visit A  " +
                 "LEFT JOIN pat_info C ON A.pv_ic=C.pif_ic  " +
                 "LEFT JOIN hpack3_setup D ON A.pv_hpack3=D.hp3_code  " +
                 "LEFT JOIN price as e on D.hp3_code = e.hp3_code  " +
                "WHERE A.pv_datevisit>='" + sBEG_DATE + "' AND A.pv_datevisit<='" + sEND_DATE + "' AND A.pv_ic='" + sel_PAT_NO + "' AND D.hp3_name IS NOT NULL  " +
                "GROUP BY D.hp3_name  " +
               "UNION ALL  " +
               "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, COUNT(D.pdet_itemnm) AS USE_DATE, D.pdet_itemnm AS ITEM_NAME, (COUNT(D.pdet_itemnm)*pdet_qty) AS ITEM_CNT, e.price*COUNT(D.pdet_itemnm)*pdet_qty AS ITEM_SUM   " +
                "FROM pat_visit A  " +
                 "LEFT JOIN pat_info C ON A.pv_ic=C.pif_ic  " +
                 "LEFT JOIN package_detail2 D ON C.pif_hpack=D.pdet_code AND D.pdet_itemcd not IN ('0001','0002')  " +
                 "LEFT JOIN price as e on D.pdet_itemcd = e.genst_code02  " +
                "WHERE A.pv_datevisit>='" + sBEG_DATE + "' AND A.pv_datevisit<='" + sEND_DATE + "' AND A.pv_ic='" + sel_PAT_NO + "' AND D.pdet_itemnm IS NOT NULL  " +
                "GROUP BY D.pdet_itemnm  " +
               "UNION ALL  " +
               "SELECT DISTINCT a.pv_ic AS PERSON_IC, c.pif_name AS PESRON_NAME, COUNT(d.drg_name) AS USE_DATE, d.drg_name AS ITEM_NAME, COUNT(d.drg_name) AS ITEM_CNT, e.price*COUNT(d.drg_name) AS ITEM_SUM  " +
                "FROM pat_visit AS a  " +
                 "JOIN shortterm_ordermgt AS b ON a.pv_ic = b.shord_patic AND a.pv_datevisit >= b.shord_dateord AND (b.shord_actst = '00001' OR (b.shord_actst = '00002' AND a.pv_datevisit < b.shord_dtactst))  " +
                 "LEFT JOIN pat_info AS c ON a.pv_ic = c.pif_ic  " +
                 "LEFT JOIN drug_list AS d ON b.shord_drug = d.drg_code " +
                 "LEFT JOIN price as e on b.shord_drug = e.drug_code  " +
                "where a.pv_ic='" + sel_PAT_NO + "' and a.pv_datevisit>='" + sBEG_DATE + "' and a.pv_datevisit<='" + sEND_DATE + "'  " +
                "GROUP BY d.drg_name  " +
               "UNION ALL " +
               "SELECT DISTINCT a.pv_ic AS PERSON_IC, c.pif_name AS PESRON_NAME, COUNT(d.drg_name) AS USE_DATE, d.drg_name AS ITEM_NAME, COUNT(d.drg_name) AS ITEM_CNT, e.price*COUNT(d.drg_name) AS ITEM_SUM    " +
                "FROM pat_visit AS a  " +
                 "JOIN longterm_ordermgt AS b ON a.pv_ic = b.lgord_patic AND a.pv_datevisit >= b.lgord_dateord AND (b.lgord_actst = '00001' OR (b.lgord_actst = '00002' AND a.pv_datevisit < b.lgord_actst))  " +
                 "LEFT JOIN pat_info AS c ON a.pv_ic = c.pif_ic  " +
                 "LEFT JOIN drug_list AS d ON b.lgord_drug = d.drg_code  " +
                 "LEFT JOIN price as e on b.lgord_drug = e.drug_code  " +
                "where a.pv_ic='" + sel_PAT_NO + "' and a.pv_datevisit>='" + sBEG_DATE + "' and a.pv_datevisit<='" + sEND_DATE + "'  " +
                "GROUP BY d.drg_name"; 

      DataTable dt18 = db.Query(sSQL);
      Store istore18 = this.GridPanel18.GetStore();
//      string url = "/myhaisv4/gettype.php?USER_ID=" + USER_ID;
      double dd = 0;
//      string sum="";
      if (dt18.Rows.Count > 0)
      {
        for (int n = 0; n < dt18.Rows.Count; n++)
        {
//          sum=dt18.Rows[n]["ITEM_SUM"].ToString();
          try
          {
            dd = dd + Convert.ToDouble(dt18.Rows[n]["ITEM_SUM"].ToString());
          }
          catch
          {

          };
        };
//        sel_PAT_NAME = dt18.Rows[0]["ITEM_SUM"].ToString();
      };
//      sSQL = "select genst_desc from general_setup where genst_ctg='IPConnect'";
//      DataTable dt19 = db.Query(sSQL);

//      if (dt18.Rows.Count > 0 && dt19.Rows[0]["genst_desc"].ToString()!="" && dt19.Rows[0]["genst_desc"].ToString() != null)
//      {
//        TextArea2.Text = TextArea2.Text + dt19.Rows[0]["genst_desc"].ToString();
//      }
//      MessageBox.Show(getdata.ToString()); 
      
      istore18.DataSource = db.GetDataArray_AddRowNum(dt18);
      Lab_patid.Text = "姓名 : " + sel_PAT_NAME;
      if (getdata == "AD" || getdata == "HN" || getdata == "DC" || getdata == "DH")
      {
        Lab_amount.Text = "金額："+Convert.ToString(dd);
        this.Column156.Hidden = false; 
      }
      istore18.DataBind();
      this.GridPanel18.Hidden = false; 
//      TextArea2.Visible = true;
//      TextArea2.Text = " ==> " + getdata + " ; dt18.Rows.Count = " + Convert.ToString(dt18.Rows.Count)+" ;";
//      TextArea2.Text = TextArea2.Text + sSQL;
    }

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

    protected void ToXml_1(object sender, EventArgs e)
    {
      string json = this.Hidden1.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_1(object sender, EventArgs e)
    {
      string json = this.Hidden1.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_1(object sender, EventArgs e)
    {
      string json = this.Hidden1.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_2(object sender, EventArgs e)
    {
      string json = this.Hidden2.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_2(object sender, EventArgs e)
    {
      string json = this.Hidden2.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_2(object sender, EventArgs e)
    {
      string json = this.Hidden2.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_3(object sender, EventArgs e)
    {
      string json = this.Hidden3.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_3(object sender, EventArgs e)
    {
      string json = this.Hidden3.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_3(object sender, EventArgs e)
    {
      string json = this.Hidden3.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_4(object sender, EventArgs e)
    {
      string json = this.Hidden4.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_4(object sender, EventArgs e)
    {
      string json = this.Hidden4.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_4(object sender, EventArgs e)
    {
      string json = this.Hidden4.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_5(object sender, EventArgs e)
    {
      string json = this.Hidden5.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_5(object sender, EventArgs e)
    {
      string json = this.Hidden5.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_5(object sender, EventArgs e)
    {
      string json = this.Hidden5.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_6(object sender, EventArgs e)
    {
      string json = this.Hidden6.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_6(object sender, EventArgs e)
    {
      string json = this.Hidden6.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_6(object sender, EventArgs e)
    {
      string json = this.Hidden6.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_7(object sender, EventArgs e)
    {
      string json = this.Hidden7.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_7(object sender, EventArgs e)
    {
      string json = this.Hidden7.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + this.txtGridPanel7.Text.Replace(" ", "") + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_7(object sender, EventArgs e)
    {
      string json = this.Hidden7.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_8(object sender, EventArgs e)
    {
      string json = this.Hidden8.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_8(object sender, EventArgs e)
    {
      string json = this.Hidden8.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_8(object sender, EventArgs e)
    {
      string json = this.Hidden8.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_9(object sender, EventArgs e)
    {
      string json = this.Hidden9.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_9(object sender, EventArgs e)
    {
      string json = this.Hidden9.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_9(object sender, EventArgs e)
    {
      string json = this.Hidden9.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_10(object sender, EventArgs e)
    {
      string json = this.Hidden10.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_10(object sender, EventArgs e)
    {
      string json = this.Hidden10.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_10(object sender, EventArgs e)
    {
      string json = this.Hidden10.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_11(object sender, EventArgs e)
    {
      string json = this.Hidden11.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_11(object sender, EventArgs e)
    {
      string json = this.Hidden11.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_11(object sender, EventArgs e)
    {
      string json = this.Hidden11.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_12(object sender, EventArgs e)
    {
      string json = this.Hidden12.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_12(object sender, EventArgs e)
    {
      string json = this.Hidden12.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_12(object sender, EventArgs e)
    {
      string json = this.Hidden12.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_13(object sender, EventArgs e)
    {
      string json = this.Hidden13.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_13(object sender, EventArgs e)
    {
      string json = this.Hidden13.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_13(object sender, EventArgs e)
    {
      string json = this.Hidden13.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    //20150627 ANDY
    protected void ToXml_13Q(object sender, EventArgs e)
    {
      string json = this.Hidden13Q.Value.ToString();
      //json = json.Replace(" ", "").Replace("/", "&frasl;");
      json = json.Replace(" ", "").Replace("/", "_");
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    //20150627 ANDY
    protected void ToExcel_13Q(object sender, EventArgs e)
    {
      string json = this.Hidden13Q.Value.ToString();
      //json = json.Replace(" ", "").Replace("/", "&frasl;");
      json = json.Replace(" ", "").Replace("/", "_");
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    //20150627 ANDY
    protected void ToCsv_13Q(object sender, EventArgs e)
    {
      string json = this.Hidden13Q.Value.ToString();
      //json = json.Replace(" ", "").Replace("/", "&frasl;");
      json = json.Replace(" ", "").Replace("/", "_");
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }
    //


    protected void ToXml_14(object sender, EventArgs e)
    {
      string json = this.Hidden14.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_14(object sender, EventArgs e)
    {
      string json = this.Hidden14.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_14(object sender, EventArgs e)
    {
      string json = this.Hidden14.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    //20150627 ANDY
    protected void ToXml_14Q(object sender, EventArgs e)
    {
      string json = this.Hidden14Q.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    //20150627 ANDY
    protected void ToExcel_14Q(object sender, EventArgs e)
    {
      string json = this.Hidden14Q.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    //20150627 ANDY
    protected void ToCsv_14Q(object sender, EventArgs e)
    {
      string json = this.Hidden14Q.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }
    //

    protected void ToXml_15(object sender, EventArgs e)
    {
      string json = this.Hidden15.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_15(object sender, EventArgs e)
    {
      string json = this.Hidden15.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_15(object sender, EventArgs e)
    {
      string json = this.Hidden15.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_16(object sender, EventArgs e)
    {
      string json = this.Hidden16.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_16(object sender, EventArgs e)
    {
      string json = this.Hidden16.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_16(object sender, EventArgs e)
    {
      string json = this.Hidden16.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_17(object sender, EventArgs e)
    {
      string json = this.Hidden17.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_17(object sender, EventArgs e)
    {
      string json = this.Hidden17.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_17(object sender, EventArgs e)
    {
      string json = this.Hidden17.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void Amount(object sender, EventArgs e)
    {
      Lab_amount.Text = "金額：";
      Window2.Show();
      Window2.Loader.SuspendScripting();
      Window2.Loader.ID="金額";
      Window2.Loader.DisableCaching = true;
      Window2.LoadContent();
    }

    protected void ToXml_18(object sender, EventArgs e)
    {
      string json = this.Hidden18.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_18(object sender, EventArgs e)
    {
      string json = this.Hidden18.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_18(object sender, EventArgs e)
    {
      string json = this.Hidden18.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_21(object sender, EventArgs e)
    {
      string json = this.Hidden21.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_21(object sender, EventArgs e)
    {
      string json = this.Hidden21.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_21(object sender, EventArgs e)
    {
      string json = this.Hidden21.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToXml_22(object sender, EventArgs e)
    {
      string json = this.Hidden22.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      string strXml = xml.OuterXml;
      this.Response.Clear();
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
      this.Response.AddHeader("Content-Length", strXml.Length.ToString());
      this.Response.ContentType = "application/xml";
      this.Response.Write(strXml);
      this.Response.End();
    }

    protected void ToExcel_22(object sender, EventArgs e)
    {
      string json = this.Hidden22.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/vnd.ms-excel";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
      XslCompiledTransform xtExcel = new XslCompiledTransform();
      xtExcel.Load(Server.MapPath("Excel.xsl"));
      xtExcel.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void ToCsv_22(object sender, EventArgs e)
    {
      string json = this.Hidden22.Value.ToString();
      StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
      XmlNode xml = eSubmit.Xml;
      this.Response.Clear();
      this.Response.ContentType = "application/octet-stream";
      this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
      XslCompiledTransform xtCsv = new XslCompiledTransform();
      xtCsv.Load(Server.MapPath("Csv.xsl"));
      xtCsv.Transform(xml, null, this.Response.OutputStream);
      this.Response.End();
    }

    protected void cboCODE11_Click(object sender, DirectEventArgs e)
    {
      DataRow[] dr;
      dr = dt_ritem_setup.Select("RITEM_CODE='" + GetComboBoxValue(cboCODE11) + "' ");
      if (dr.Length > 0)
      {
        this.txtRESULT_CODE11.Text = dr[0]["RITEM_CODE"].ToString();
        this.txtRESULT_NAME11.Text = dr[0]["RITEM_NAME"].ToString();
        this.txtRESULT_UNIT11.Text = dr[0]["RITEM_UNIT"].ToString();
        this.txtRESULT_LOW.Text = dr[0]["RITEM_LOW1"].ToString();
        this.txtRESULT_HIGH.Text = dr[0]["RITEM_HIGH1"].ToString();
        this.txtNORMAL11.Text = dr[0]["RITEM_LOW1"].ToString() + " ~ " + dr[0]["RITEM_HIGH1"].ToString() + " " + dr[0]["RITEM_UNIT"].ToString();
      }
    }

    //20150627 ANDY 監控數據查詢
    protected void cboCODE11Q_Click(object sender, DirectEventArgs e)
    {
      BaseForm bs = new BaseForm();
      sYEAR.Text = bs.GetComboBoxValue(this.cboCODE11Q);

      sYEAR.Text = bs.GetComboBoxText(this.cboCODE11Q);

      //DataRow[] dr;
      //dr = dt_ritem_setup.Select("RITEM_CODE='" + GetComboBoxValu(cboCODE11Q) + "' ");
      //if (dr.Length > 0)
      //{
      //    this.txtRESULT_CODE11Q.Text = dr[0]["RITEM_CODE"].ToString();
      //    this.txtRESULT_NAME11Q.Text = dr[0]["RITEM_NAME"].ToString();
      //    this.txtRESULT_UNIT11Q.Text = dr[0]["RITEM_UNIT"].ToString();
      //    this.txtRESULT_LOWQ.Text    = dr[0]["RITEM_LOW1"].ToString();
      //    this.txtRESULT_HIGHQ.Text   = dr[0]["RITEM_HIGH1"].ToString();
      //    this.txtNORMAL11Q.Text      = dr[0]["RITEM_LOW1"].ToString() + " ~ " + dr[0]["RITEM_HIGH1"].ToString() + " " + dr[0]["RITEM_UNIT"].ToString();
      //}
    }

    //20150627 ANDY 
    protected void cboCODE11QT_Click(object sender, DirectEventArgs e)
    {
      BaseForm bs = new BaseForm();
      sQT.Text = bs.GetComboBoxValue(this.cboCODE11QT);
      sQT.Text = bs.GetComboBoxText(this.cboCODE11QT);
    }


    protected void cboCODE12_Click(object sender, DirectEventArgs e)
    {
      DataRow[] dr;
      dr = dt_ritem_setup12.Select("RITEM_CODE='" + GetComboBoxValue(cboCODE12) + "' ");
      if (dr.Length > 0)
      {
        this.txtRESULT_CODE12.Text = dr[0]["RITEM_CODE"].ToString();
        this.txtRESULT_NAME12.Text = dr[0]["RITEM_NAME"].ToString();
        this.txtRESULT_UNIT12.Text = dr[0]["RITEM_UNIT"].ToString();
        this.txtNORMAL12.Text = dr[0]["RITEM_LOW1"].ToString() + " ~ " + dr[0]["RITEM_HIGH1"].ToString() + " " + dr[0]["RITEM_UNIT"].ToString();
      }
    }

    protected void ReloadData(object sender, DirectEventArgs e)
    {
      this.Chart1.GetStore().DataBind();
    }

    protected void hh(object sender, DirectEventArgs e)
    {
      btn_Query1_Click(sender, e);
    }

    protected void Find_IC(object sender, DirectEventArgs e)
    {
      txtPERSON_NAME12.Text = e.ExtraParams["PAT_IC"];
      btn_Query12_Click(sender, e);
    }

    private void GetGroupName()
    {
      string sSQL;
      sSQL = "SELECT GROUP_NAME, GROUP_CODE FROM a_item_group ";
      sSQL += "WHERE GROUP_USED='Y' ";
      sSQL += "ORDER BY GROUP_CLASS ";
      DataTable dt = db.Query(sSQL);
      SetComboBoxItem(ComboBoxGroup, dt, false, "GROUP_NAME", "GROUP_CODE");
      SetComboBoxItem(ComboBoxGroup1, dt, false, "GROUP_NAME", "GROUP_CODE");
    }

    #region 取得檢查碼
    private string GetGroupCode(string sName)
    {
      string sSQL = "SELECT GROUP_CODE, GROUP_NAME FROM a_item_group ";
      sSQL += "WHERE GROUP_NAME='" + sName + "' ";
      sSQL += "GROUP BY GROUP_CODE ";
      try
      {
        DataTable dt = db.Query(sSQL);
        return dt.Rows[0]["GROUP_CODE"].ToString();
      }
      catch
      {
        return "";
      }
    }
    #endregion

    protected void ChangGroup(object sender, DirectEventArgs e)
    {
      ComboBoxChang();
    }

    protected void ComboBoxChang()
    {
      string GroupCode = GetGroupCode((string)this.ComboBoxGroup.Value);
      string sSQL = "SELECT RITEM_CLASS,RITEM_CODE,RITEM_TYPE,RITEM_NAME_S,RITEM_NAME,RITEM_UNIT,RITEM_LOW1,RITEM_HIGH1 ";
      sSQL += "FROM a_ritem_setup ";
      sSQL += "WHERE RITEM_CLASS='" + GroupCode + "' ";
      sSQL += "AND RITEM_USED='Y' ";
      //sSQL += "ORDER BY RITEM_USED DESC";
      dt_ritem_setup12 = db.Query(sSQL);
      SetComboBoxItem(cboCODE12, dt_ritem_setup12, false, "RITEM_NAME", "RITEM_CODE");
      cboCODE12.Text = dt_ritem_setup12.Rows.Count == 0 ? "没有资料" : dt_ritem_setup12.Rows[0]["RITEM_NAME"].ToString();
    }

    protected void ChangGroup1(object sender, DirectEventArgs e)
    {
      ComboBoxChang1();
    }

    protected void ComboBoxChang1()
    {
      string GroupCode = GetGroupCode((string)this.ComboBoxGroup1.Value);
      string sSQL = "SELECT RITEM_CLASS,RITEM_CODE,RITEM_TYPE,RITEM_NAME_S,RITEM_NAME,RITEM_UNIT,RITEM_LOW1,RITEM_HIGH1 ";
      sSQL += "FROM a_ritem_setup ";
      sSQL += "WHERE RITEM_CLASS='" + GroupCode + "' ";
      sSQL += "AND RITEM_USED='Y' ";
      //sSQL += "ORDER BY RITEM_USED DESC";
      dt_ritem_setup = db.Query(sSQL);
      SetComboBoxItem(cboCODE11, dt_ritem_setup, false, "RITEM_NAME", "RITEM_CODE");
      cboCODE11.Text = dt_ritem_setup.Rows.Count == 0 ? "没有资料" : dt_ritem_setup.Rows[0]["RITEM_NAME"].ToString();
    }


  }
}