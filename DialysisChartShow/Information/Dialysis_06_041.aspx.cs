using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Dialysis_Chart_Show.tools;
using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_06_041 : BaseForm //System.Web.UI.Page
    {
        private int iiCNT = 7;
        private string sCODE = "";
        private string sSQL = "";
        private DBMysql db = new DBMysql();
        private DataRow dr;
        private Ext.Net.Node root = new Ext.Net.Node();
        //private Ext.Net.Node rA = new Ext.Net.Node();
        private Ext.Net.Node rB = new Ext.Net.Node();
        private Ext.Net.Node rC = new Ext.Net.Node();
        private Ext.Net.Node rD = new Ext.Net.Node();
        private Ext.Net.Node rE = new Ext.Net.Node();
        private Ext.Net.Node rF = new Ext.Net.Node();
        private Ext.Net.Node rG = new Ext.Net.Node();
        private Ext.Net.Node rH = new Ext.Net.Node();
        private Ext.Net.Node rY = new Ext.Net.Node();
        //private Ext.Net.Node rZ = new Ext.Net.Node();
        private RESULT_LOG dr5011;//身高
        private RESULT_LOG dr5012;//体重
        private RESULT_LOG dr5013;//BMI
        private RESULT_LOG dr5014;//体表面积
        private RESULT_LOG dr5015;//透析时间
        private RESULT_LOG dr5016;//超滤量
        private RESULT_LOG dr5017;//URR
        private RESULT_LOG dr5018;//sp Kt/V
        private RESULT_LOG dr4047;//BUN
        private RESULT_LOG dr4048;//血透後BUN
            
        #region class RESULT_LOG

        public class RESULT_LOG
        {
            public string PAT_NO
            {
                get;
                set;
            }
            public string RESULT_DATE
            {
                get;
                set;
            }
            public string RESULT_CODE
            {
                get;
                set;
            }
            public string RITEM_CLASS
            {
                get;
                set;
            }
            public string RITEM_TYPE
            {
                get;
                set;
            }
            public string RITEM_NAME
            {
                get;
                set;
            }
            public string RITEM_NAME_S
            {
                get;
                set;
            }
            public string RESULT_VALUE_O
            {
                get;
                set;
            }
            public string RITEM_UNIT
            {
                get;
                set;
            }
            public string RESULT_VALUE_N
            {
                get;
                set;
            }
            public string RITEM_LOW1
            {
                get;
                set;
            }
            public string RITEM_HIGH1
            {
                get;
                set;
            }
            public string RESULT_DAYS
            {
                get;
                set;
            }
            public Int64 ROW_ID
            {
                get;
                set;
            }
            public string RESULT_VALUE
            {
                get;
                set;
            }
        }

        #endregion

        /// <summary>
        /// 建立左邊的樹
        /// </summary>
        /// <param name="nn">Tree Node</param>
        /// <param name="ss">Node.NodeID</param>
        /// <param name="st">Node.Text</param>
        /// <param name="sg">a_item_group.GROUP_CODE</param>
        /// <param name="ii">資料筆數(Top ii)，0=全部資料</param>
        private void AddChild(Ext.Net.Node nn, string ss, string st, string sg, int ii)
        {
            nn.Text = st;
            nn.Icon = Icon.ReportUser;
            nn.NodeID = ss + "_";
            nn.Cls = "large-font";
                
            string s = "";
            string sPAT_NO = _PAT_ID;
            if (sg != "")
                s = " AND RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sg + "' AND GROUP_USED='Y') ";
            if (ii == 0)
            {
                sSQL = "SELECT RESULT_DATE, COUNT(*) AS CNT ";
                sSQL += " FROM a_result_log ";
                sSQL += "WHERE PAT_NO='" + sPAT_NO + "' ";
                sSQL += "  AND RESULT_VER=0 ";
                //sSQL += "  AND RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sg + "') ";
                sSQL += s;
                sSQL += "GROUP BY RESULT_DATE ";
                sSQL += "ORDER BY RESULT_DATE DESC ";
            }
            else
            {
                sSQL = "SELECT RESULT_DATE, COUNT(*) AS CNT ";
                sSQL += " FROM a_result_log ";
                sSQL += "WHERE PAT_NO='" + sPAT_NO + "' ";
                sSQL += "  AND RESULT_VER=0 ";
                //sSQL += "  AND RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sg + "') ";
                sSQL += s;
                sSQL += "GROUP BY RESULT_DATE ";
                sSQL += "ORDER BY RESULT_DATE DESC ";
                sSQL += "LIMIT 0," + ii.ToString() + " ";
            }
            DataTable dt = db.Query(sSQL);

            //2015.04.10 andy 如果顯示 只實驗室及輔助檢查, 只出現首次檢查結果
            //for (int i = 0; i < dt.Rows.Count; i++)
            for (int i = 0; i < 1; i++)
            {
                Ext.Net.Node rr = new Ext.Net.Node()
                {
                    Text = dt.Rows[i]["RESULT_DATE"].ToString() + "(" + dt.Rows[i]["CNT"].ToString() + ")",
                    NodeID = ss + i.ToString(),
                    Icon = Icon.Page,
                    Cls = "blue-large-font",
                    Leaf = true
                };
                nn.Children.Add(rr);
            }
            if (dt.Rows.Count == 0)
            {
                Ext.Net.Node rr = new Ext.Net.Node()
                {
                    Text = "无",
                    NodeID = ss + "NEW",
                    Icon = Icon.PageWhite,
                    Cls = "blue-large-font",
                    Leaf = true
                };
                nn.Children.Add(rr);
            }
            //nn.Expanded = true;
            nn.Expanded = false;
        }

        private void AddChild2(Ext.Net.Node nn, string ss, string st, string sg, int ii)
        {
            nn.Text = st;
            nn.Icon = Icon.ReportUser;
            nn.NodeID = ss + "_";
            nn.Cls = "blue-large-font";
            nn.Leaf = true;

            //string s = "";
            //string sPAT_NO = _PAT_ID;
            //if (sg != "")
            //    s = " AND RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sg + "' AND GROUP_USED='Y') ";
            //if (ii == 0)
            //{
            //    sSQL = "SELECT RESULT_DATE, COUNT(*) AS CNT ";
            //    sSQL += " FROM a_result_log ";
            //    sSQL += "WHERE PAT_NO='" + sPAT_NO + "' ";
            //    sSQL += "  AND RESULT_VER=0 ";
            //    //sSQL += "  AND RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sg + "') ";
            //    sSQL += s;
            //    sSQL += "GROUP BY RESULT_DATE ";
            //    sSQL += "ORDER BY RESULT_DATE DESC ";
            //}
            //else
            //{
            //    sSQL = "SELECT RESULT_DATE, COUNT(*) AS CNT ";
            //    sSQL += " FROM a_result_log ";
            //    sSQL += "WHERE PAT_NO='" + sPAT_NO + "' ";
            //    sSQL += "  AND RESULT_VER=0 ";
            //    //sSQL += "  AND RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sg + "') ";
            //    sSQL += s;
            //    sSQL += "GROUP BY RESULT_DATE ";
            //    sSQL += "ORDER BY RESULT_DATE DESC ";
            //    sSQL += "LIMIT 0," + ii.ToString() + " ";
            //}
            //DataTable dt = db.Query(sSQL);
            //for (int i = 0; i < dt.Rows.Count; i++)
            //{
            //    Ext.Net.Node rr = new Ext.Net.Node()
            //    {
            //        Text = dt.Rows[i]["RESULT_DATE"].ToString() + "(" + dt.Rows[i]["CNT"].ToString() + ")",
            //        NodeID = ss + i.ToString(),
            //        Icon = Icon.Page,
            //        Cls = "blue-large-font",
            //        Leaf = true
            //    };
            //    nn.Children.Add(rr);
            //}
            //if (dt.Rows.Count == 0)
            //{
            //    Ext.Net.Node rr = new Ext.Net.Node()
            //    {
            //        Text = "无",
            //        NodeID = ss + "NEW",
            //        Icon = Icon.PageWhite,
            //        Cls = "blue-large-font",
            //        Leaf = true
            //    };
            //    nn.Children.Add(rr);
            //}
            //nn.Expanded = true;
        }

        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.PanelR.Title = "辅助检查";
                this.PanelT.Title = "辅助检查";
                this.GridPanel2.Title = "辅助检查";
                this.txtGROUP.Text = "G004";
                this.txtGROUP_NAME.Text = "辅助检查";
                this.GridPanel1.Title = this.txtGROUP_NAME.Text;
                this.txtCODE.Hidden = true;
                this.txtGROUP.Hidden = true;
                this.txtSTATUS.Hidden = true;
                this.txtPAT_NO.Hidden = true;
 
                //string sPAT_NO = _PAT_ID; // Request.QueryString["_PAT_ID"].ToString();
                this.txtPAT_NO.Text = _PAT_ID;
                this.TreePanel1.ID = "TreePanel1";
                //this.TreePanel1.Width = Unit.Pixel(200);
                this.TreePanel1.Height = Unit.Pixel(400);
                this.TreePanel1.AutoScroll = true;
                
                Ext.Net.Button btnExpand = new Ext.Net.Button();
                btnExpand.Text = "展开";
                btnExpand.Listeners.Click.Handler = this.TreePanel1.ClientID + ".expandAll();";

                Ext.Net.Button btnCollapse = new Ext.Net.Button();
                btnCollapse.Text = "收合";
                btnCollapse.Listeners.Click.Handler = this.TreePanel1.ClientID + ".collapseAll();";

                Ext.Net.Button btnCount = new Ext.Net.Button();
                btnCount.Text = "前" + iiCNT.ToString();
                btnCount.Click += btnCount_Click;
                btnCount.AutoPostBack = true;

                Toolbar toolBar = new Toolbar();
                toolBar.ID = "Toolbar1";
                toolBar.Items.Add(btnExpand);
                toolBar.Items.Add(btnCollapse);
                //toolBar.Items.Add(btnCount);
                this.TreePanel1.TopBar.Add(toolBar);

                StatusBar statusBar1 = new StatusBar();
                statusBar1.ID = "StatusBar1";
                statusBar1.AutoClear = 1000;
                this.TreePanel1.BottomBar.Add(statusBar1);

                this.TreePanel1.Listeners.ItemClick.Handler = statusBar1.ClientID + ".setStatus({text: '点选: <b>' + record.data.text + '</b>', clear: false});";
                
                //Ext.Net.Node root = new Ext.Net.Node();
                root.Text = "全部检查结果";
                //root.Icon = Icon.ReportUser;
                root.NodeID = "__";
                root.Cls = "large-font";
                //AddChild(rA, "A", "实验室检查", "G001", iiCNT);
                AddChild2(rB, "B", "血常规", "G007", iiCNT);
                AddChild2(rC, "C", "血型", "G008", iiCNT);
                AddChild2(rD, "D", "输血四项", "G009", iiCNT);
                AddChild2(rE, "E", "血凝分析", "G010", iiCNT);
                AddChild2(rF, "F", "尿液分析", "G011", iiCNT);
                AddChild2(rG, "G", "生化二组", "G012", iiCNT);
                AddChild2(rH, "H", "粪便常规", "G013", iiCNT);


                //南京2附院要顯示 南京市大厂医院不顯示  2015.02.13 ANDY
                string sPC_HEAD = "";
                DataTable dtPC_HEAD = db.Query("SELECT *  FROM general_setup WHERE  genst_code='RPT_NAME'");
                if (dtPC_HEAD.Rows.Count > 0)
                {
                    sPC_HEAD = dtPC_HEAD.Rows[0]["genst_desc"].ToString();
                }
                //南京市大厂医院不顯示 2015.02.13 ANDY
                if (sPC_HEAD != "南京市大厂医院")
                {
                    AddChild(rY, "Y", "辅助检查", "G004", iiCNT); 
                }
                //2015.02.04 andy  mark不顯示
                //AddChild(rY, "Y", "辅助检查", "G004", iiCNT);

                //AddChild(rZ, "Z", "检查结果总评价", "", iiCNT);
                root.Children.Add(rY);
                //root.Children.Add(rA);
                root.Children.Add(rB);
                root.Children.Add(rC);
                root.Children.Add(rD);
                root.Children.Add(rE);
                root.Children.Add(rF);
                root.Children.Add(rG);
                root.Children.Add(rH);
                //root.Children.Add(rZ);

                root.Expanded = true;
                this.TreePanel1.Root.Add(root);

                //取得所有檢查項目
                sSQL = "SELECT RITEM_CODE, RITEM_NAME ";
                sSQL += " FROM a_ritem_setup ";
                sSQL += "WHERE RITEM_USED='Y' ";
                sSQL += "ORDER BY RITEM_CLASS, RITEM_TYPE, RITEM_SN ";
                DataTable dtITEM = db.Query(sSQL);

                this.cboRITEM_CODE.AddItem("全部", "");
                for (int i = 0; i < dtITEM.Rows.Count; i++)
                {
                    dr = dtITEM.Rows[i];
                    this.cboRITEM_CODE.AddItem(dr["RITEM_NAME"].ToString(), dr["RITEM_CODE"].ToString());
                }

                ////得所有檢查組合
                //sSQL = "SELECT DISTINCT GROUP_CODE, GROUP_NAME ";
                //sSQL += " FROM a_item_group ";
                //sSQL += "WHERE GROUP_USED='Y' ";
                //sSQL += "ORDER BY GROUP_CODE ";
                //DataTable dtGROUP = db.Query(sSQL);

                //this.cboGROUP.AddItem("不限", "");
                //for (int i = 0; i < dtGROUP.Rows.Count; i++)
                //{
                //    dr = dtGROUP.Rows[i];
                //    this.cboGROUP.AddItem(dr["GROUP_NAME"].ToString(), dr["GROUP_CODE"].ToString());
                //}

                //this.LastData();
            }
        }

        /// <summary>
        /// 讀取每個檢驗項目最近一筆資料
        /// </summary>
        private void LastData()
        {
            this.txtRESULT_DATE.Disabled = false;
            this.cboRITEM_CODE.Disabled = false;
            this.btnSAVE.Disabled = true;
            this.btnCALC.Disabled = true;
            this.btnCANCEL.Disabled = true;
            this.btnREAD.Disabled = false;
            this.btnLAST.Disabled = false;
            this.btnADD.Disabled = false;
            

            string sPAT_NO = _PAT_ID;
            //sSQL = "SELECT A.PAT_NO, MAX(A.RESULT_DATE) AS RESULT_DATE, A.RESULT_CODE, ";
            //sSQL += "      B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, ";
            //sSQL += "      B.RITEM_UNIT, A.RESULT_VALUE_N AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, ";
            //sSQL += "      CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID ";
            //sSQL += " FROM a_result_log A ";
            //sSQL += " LEFT JOIN a_ritem_setup B ";
            //sSQL += "   ON A.RESULT_CODE=B.RITEM_CODE ";
            //sSQL += "  AND A.RESULT_DATE>=B.BEG_DATE ";
            //sSQL += "  AND A.RESULT_DATE<=B.END_DATE ";
            //sSQL += "WHERE A.RESULT_VER=0 ";
            //sSQL += "  AND A.PAT_NO=" + sPAT_NO + " ";
            //sSQL += "GROUP BY A.PAT_NO, A.RESULT_CODE, B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, ";
            //sSQL += "         A.RESULT_VALUE_T, A.RESULT_VALUE_N, B.RITEM_UNIT, B.RITEM_LOW1, B.RITEM_HIGH1, RESULT_DAYS, A.ROW_ID ";
            //sSQL += "ORDER BY B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_SN, B.RITEM_CODE ";

            sSQL = "SELECT D.PAT_NO, MAX(D.RESULT_DATE) AS RESULT_DATE, D.RESULT_CODE, " +
                          "B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, " +
                          "A.RESULT_VALUE_T AS RESULT_VALUE_O, " +
                          "B.RITEM_UNIT, A.RESULT_VALUE_N AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, " +
                          "CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID " +
                     "FROM a_result_log D " +
                     "LEFT JOIN a_result_log A " +
                       "ON A.PAT_NO=D.PAT_NO " +
                      "AND A.RESULT_DATE=D.RESULT_DATE " +
                      "AND A.RESULT_CODE=D.RESULT_CODE " +
                      "AND A.RESULT_VER=0 " +
                     "LEFT JOIN a_ritem_setup B " +
                       "ON A.RESULT_CODE=B.RITEM_CODE " +
                      "AND A.RESULT_DATE>=B.BEG_DATE " +
                      "AND A.RESULT_DATE<=B.END_DATE " +
                    "WHERE D.PAT_NO=" + sPAT_NO + " " +
                      "AND D.RESULT_VER=0 " +
                    "GROUP BY D.PAT_NO, D.RESULT_CODE " +
                    "ORDER BY B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_SN, B.RITEM_CODE ";

            sSQL = "SELECT A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, " +
                          "B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, " +
                          "A.RESULT_VALUE_T AS RESULT_VALUE_O, " +
                          "B.RITEM_UNIT, A.RESULT_VALUE_N AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, " +
                          "CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID " +
                     "FROM a_result_log A, (SELECT PAT_NO, MAX(RESULT_DATE) AS RESULT_DATE, RESULT_CODE " +
                                             "FROM a_result_log " +
                                            "WHERE PAT_NO=" + sPAT_NO + " " +
                                              "AND RESULT_VER=0 " +
                                            "GROUP BY PAT_NO, RESULT_CODE) D " +
                     "LEFT JOIN a_ritem_setup B " +
                       "ON D.RESULT_CODE=B.RITEM_CODE " +
                      "AND D.RESULT_DATE>=B.BEG_DATE " +
                      "AND D.RESULT_DATE<=B.END_DATE " +
                    "WHERE A.PAT_NO=D.PAT_NO " +
                      "AND A.RESULT_DATE=D.RESULT_DATE " +
                      "AND A.RESULT_CODE=D.RESULT_CODE " +
                      "AND A.RESULT_VER=0 " +
                    "ORDER BY B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_SN, B.RITEM_CODE ";
            DataTable dt = db.Query(sSQL);

            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                if (Convert.IsDBNull(dr["RITEM_UNIT"]))
                    dr["RITEM_UNIT"] = string.Empty;
                if (Convert.IsDBNull(dr["RITEM_HIGH1"]))
                    dr["RITEM_HIGH1"] = string.Empty;
                if (Convert.IsDBNull(dr["RITEM_LOW1"]))
                    dr["RITEM_LOW1"] = string.Empty;
                if (Convert.IsDBNull(dr["RESULT_VALUE_O"]))
                    dr["RESULT_VALUE_O"] = string.Empty;

                DateTime dt1 = Convert.ToDateTime(dr["RESULT_DATE"].ToString());
                DateTime dt2 = Convert.ToDateTime(dr["RESULT_DAYS"].ToString());
                Double ts1 = new TimeSpan(dt2.Ticks - dt1.Ticks).TotalDays;

                if (ts1 <= 30)
                    dr["RESULT_DAYS"] = Convert.ToInt64(ts1).ToString() + "天以前";
                else
                {
                    Double ts2 = ts1 / 30;
                    if (ts2 <= 12)
                        dr["RESULT_DAYS"] = Convert.ToInt64(ts2).ToString() + "个月以前";
                    else
                    {
                        Double ts3 = ts2 / 12;
                        dr["RESULT_DAYS"] = Convert.ToInt64(ts3).ToString() + "年以前";
                    }
                }
            }

            dt.AcceptChanges();

            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
            this.GridPanel1.ColumnModel.Columns[10].Hidden = true;
            this.GridPanel1.ColumnModel.Columns[13].Hidden = false;
        }


        /// <summary>
        /// 依據日期讀取檢驗項目
        /// </summary>
        protected void cmdREAD(object sender, DirectEventArgs e)
        {
            //if (txtPAT_NO.Text != "")
            //    sPAT_NO = txtPAT_NO.Text;

            this.txtSTATUS.Text = "READ";
            string sGROUP = this.txtGROUP.Text;

            this.txtRESULT_DATE.Disabled = false;
            this.cboRITEM_CODE.Disabled = false;
            this.btnSAVE.Disabled = true;
            this.btnCALC.Disabled = true;
            this.btnCANCEL.Disabled = true;
            this.btnREAD.Disabled = false;
            this.btnLAST.Disabled = false;
            this.btnADD.Disabled = false;
            this.GridPanel1.Title = this.txtGROUP_NAME.Text;

            string sPAT_NO = _PAT_ID;
            string sDATE = _Get_YMD2(this.txtRESULT_DATE.Text);
            sCODE = GetComboBoxValue(cboRITEM_CODE);
            if (sDATE != "" || sCODE != "")
            {
                if (sGROUP == "")
                {
                    sSQL = "SELECT A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, ";
                    sSQL += "      B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, ";
                    sSQL += "      B.RITEM_UNIT, A.RESULT_VALUE_T AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, ";
                    sSQL += "      CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID, A.RESULT_VALUE_N AS RESULT_VALUE ";
                    sSQL += " FROM a_result_log A ";
                    sSQL += " LEFT JOIN a_ritem_setup B ";
                    sSQL += "   ON A.RESULT_CODE=B.RITEM_CODE ";
                    sSQL += "  AND A.RESULT_DATE>=B.BEG_DATE ";
                    sSQL += "  AND A.RESULT_DATE<=B.END_DATE ";
                    sSQL += "WHERE A.RESULT_VER=0 ";
                    if (sDATE != "")
                        sSQL += "AND A.RESULT_DATE='" + sDATE + "' ";
                    if (sCODE != "")
                        sSQL += "AND A.RESULT_CODE='" + sCODE + "' ";
                    sSQL += "  AND A.PAT_NO=" + sPAT_NO + " ";
                    sSQL += "ORDER BY B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_SN, A.RESULT_DATE DESC, B.RITEM_CODE ";
                }
                else
                {
                    sSQL = "SELECT A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, ";
                    sSQL += "      B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, ";
                    sSQL += "      B.RITEM_UNIT, A.RESULT_VALUE_T AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, ";
                    sSQL += "      CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID, A.RESULT_VALUE_N AS RESULT_VALUE, C.GROUP_SN ";
                    sSQL += " FROM a_result_log A ";
                    sSQL += " LEFT JOIN a_item_group C ";
                    sSQL += "   ON C.OITEM_CODE=A.RESULT_CODE ";
                    sSQL += "  AND C.GROUP_CODE='" + sGROUP + "' ";
                    sSQL += " LEFT JOIN a_ritem_setup B ";
                    sSQL += "   ON A.RESULT_CODE=B.RITEM_CODE ";
                    sSQL += "  AND A.RESULT_DATE>=B.BEG_DATE ";
                    sSQL += "  AND A.RESULT_DATE<=B.END_DATE ";
                    sSQL += "WHERE A.RESULT_VER=0 ";
                    if (sDATE != "")
                        sSQL += "AND A.RESULT_DATE='" + sDATE + "' ";
                    if (sCODE != "")
                        sSQL += "AND A.RESULT_CODE='" + sCODE + "' ";
                    sSQL += "  AND A.RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sGROUP + "') ";
                    sSQL += "  AND A.PAT_NO=" + sPAT_NO + " ";
                    sSQL += "ORDER BY C.GROUP_SN, A.RESULT_DATE DESC, B.RITEM_CODE ";
                }
                DataTable dt = db.Query(sSQL);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (Convert.IsDBNull(dr["RITEM_UNIT"]))
                        dr["RITEM_UNIT"] = string.Empty;
                    if (Convert.IsDBNull(dr["RITEM_HIGH1"]))
                        dr["RITEM_HIGH1"] = string.Empty;
                    if (Convert.IsDBNull(dr["RITEM_LOW1"]))
                        dr["RITEM_LOW1"] = string.Empty;
                    if (Convert.IsDBNull(dr["RESULT_VALUE_O"]))
                        dr["RESULT_VALUE_O"] = string.Empty;
                    if (Convert.IsDBNull(dr["RESULT_VALUE_N"]))
                        dr["RESULT_VALUE_N"] = string.Empty;

                    DateTime dt1 = Convert.ToDateTime(dr["RESULT_DATE"].ToString());
                    DateTime dt2 = Convert.ToDateTime(dr["RESULT_DAYS"].ToString());
                    Double ts1 = new TimeSpan(dt2.Ticks - dt1.Ticks).TotalDays;

                    if (ts1 <= 30)
                        dr["RESULT_DAYS"] = Convert.ToInt64(ts1).ToString() + "天以前";
                    else
                    {
                        Double ts2 = ts1 / 30;
                        if (ts2 <= 12)
                            dr["RESULT_DAYS"] = Convert.ToInt64(ts2).ToString() + "个月以前";
                        else
                        {
                            Double ts3 = ts2 / 12;
                            dr["RESULT_DAYS"] = Convert.ToInt64(ts3).ToString() + "年以前";
                        }
                    }
                }
                dt.AcceptChanges();

                Store istore = this.GridPanel1.GetStore();
                istore.DataSource = db.GetDataArray_AddRowNum(dt);
                istore.DataBind();
                this.GridPanel1.ColumnModel.Columns[10].Hidden = true;
                this.GridPanel1.ColumnModel.Columns[13].Hidden = false;
            }
            else
            {
                //X.Msg.Alert("警告", "检查日期资料不正确").Show();
                _NotificationShow("警告，检查日期与检查项目资料不正确");
                this.txtRESULT_DATE.Focus(true, 1000);
            }
        }

        /// <summary>
        /// 讀取每個檢驗項目最近一筆資料
        /// </summary>
        protected void cmdLAST(object sender, DirectEventArgs e)
        {
            this.LastData();
            this.txtSTATUS.Text = "LAST";
        }

        /// <summary>
        /// 取消新增動作
        /// </summary>
        protected void cmdCANCEL(object sender, DirectEventArgs e)
        {
            this.GridPanel1.ColumnModel.Columns[10].Hidden = true;
            this.GridPanel1.ColumnModel.Columns[13].Hidden = false;
            Store1.RemoveAll(true);
            cmdREAD(sender, e);
            this.txtSTATUS.Text = "CANCEL";
        }

        /// <summary>
        /// 新增動作，一定要有日期，依照日期來新增
        /// </summary>
        protected void cmdADD(object sender, DirectEventArgs e)
        {
            //Ext.Net.Button button1 = (Ext.Net.Button)sender;
            //button1.Disabled = true;
            //Ext.Net.Button button2 = (Ext.Net.Button)btnSAVE;
            //button2.Disabled = false;
            //if (txtPAT_NO.Text != "")
            //    sPAT_NO = txtPAT_NO.Text;
            this.txtSTATUS.Text = "ADD";
            string sGROUP = this.txtGROUP.Text;
            
            string sPAT_NO = _PAT_ID;
            string sDATE = _Get_YMD2(this.txtRESULT_DATE.Text);
            if (sDATE != "")
            {
                //this.GridPanel1.Title = "检查结果　　　　　　　　　　F1=阴性，F2弱阳性，F3=阳性，F4=强阳性，F5=未检";
                this.GridPanel1.Title = this.txtGROUP_NAME.Text + "　　　　　　　F1=阴性，F2弱阳性，F3=阳性，F4=强阳性，F5=未检";
                if (sGROUP == "")
                {
                    sSQL = "SELECT " + sPAT_NO + " AS PAT_NO, '" + sDATE + "' AS RESULT_DATE, B.RITEM_CODE AS RESULT_CODE, ";
                    sSQL += "      B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, ";
                    sSQL += "      B.RITEM_UNIT, A.RESULT_VALUE_T AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, ";
                    sSQL += "      CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID, A.RESULT_VALUE_N AS RESULT_VALUE, B.RITEM_WORDS ";
                    sSQL += " FROM a_ritem_setup B ";
                    sSQL += " LEFT JOIN a_result_log A ";
                    sSQL += "   ON A.RESULT_CODE=B.RITEM_CODE ";
                    sSQL += "  AND A.RESULT_DATE='" + sDATE + "' ";
                    sSQL += "  AND A.RESULT_VER=0 ";
                    sSQL += "  AND A.PAT_NO=" + sPAT_NO + " ";
                    sSQL += "WHERE B.BEG_DATE<='" + sDATE + "' ";
                    sSQL += "  AND B.END_DATE>='" + sDATE + "' ";
                    sSQL += "ORDER BY B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_SN, B.RITEM_CODE ";
                }
                else
                {
                    sSQL = "SELECT " + sPAT_NO + " AS PAT_NO, '" + sDATE + "' AS RESULT_DATE, B.RITEM_CODE AS RESULT_CODE, ";
                    sSQL += "      B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, ";
                    sSQL += "      B.RITEM_UNIT, A.RESULT_VALUE_T AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, ";
                    sSQL += "      CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID, A.RESULT_VALUE_N AS RESULT_VALUE, B.RITEM_WORDS, C.GROUP_SN ";
                    sSQL += " FROM a_ritem_setup B ";
                    sSQL += " LEFT JOIN a_item_group C ";
                    sSQL += "   ON C.OITEM_CODE=B.RITEM_CODE ";
                    sSQL += "  AND C.GROUP_CODE='" + sGROUP + "' ";
                    sSQL += " LEFT JOIN a_result_log A ";
                    sSQL += "   ON A.RESULT_CODE=B.RITEM_CODE ";
                    sSQL += "  AND A.RESULT_DATE='" + sDATE + "' ";
                    sSQL += "  AND A.RESULT_VER=0 ";
                    sSQL += "  AND A.PAT_NO=" + sPAT_NO + " ";
                    sSQL += "WHERE B.BEG_DATE<='" + sDATE + "' ";
                    sSQL += "  AND B.END_DATE>='" + sDATE + "' ";
                    sSQL += "  AND B.RITEM_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sGROUP + "') ";
                    sSQL += "ORDER BY C.GROUP_SN, A.RESULT_DATE DESC, B.RITEM_CODE ";
                }
                DataTable dt = db.Query(sSQL);

                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    dr = dt.Rows[i];
                    if (Convert.IsDBNull(dr["RITEM_UNIT"]))
                        dr["RITEM_UNIT"] = string.Empty;
                    if (Convert.IsDBNull(dr["RITEM_HIGH1"]))
                        dr["RITEM_HIGH1"] = string.Empty;
                    if (Convert.IsDBNull(dr["RITEM_LOW1"]))
                        dr["RITEM_LOW1"] = string.Empty;
                    if (Convert.IsDBNull(dr["RESULT_VALUE_O"]))
                        dr["RESULT_VALUE_O"] = string.Empty;
                    if (Convert.IsDBNull(dr["RESULT_VALUE_N"]))
                        dr["RESULT_VALUE_N"] = string.Empty;
                    if (Convert.IsDBNull(dr["RITEM_WORDS"]))
                        dr["RITEM_WORDS"] = string.Empty;
                    if (Convert.IsDBNull(dr["ROW_ID"]))
                        dr["ROW_ID"] = 0;
                }
                dt.AcceptChanges();

                Store istore = this.GridPanel1.GetStore();
                istore.DataSource = db.GetDataArray_AddRowNum(dt);
                istore.DataBind();
                this.btnSAVE.Disabled = false;
                this.btnCALC.Disabled = false;
                this.btnCANCEL.Disabled = false;
                this.btnREAD.Disabled = true;
                this.btnLAST.Disabled = true;
                this.btnADD.Disabled = true;
                this.txtRESULT_DATE.Disabled = true;
                this.cboRITEM_CODE.Disabled = true;
                this.GridPanel1.ColumnModel.Columns[10].Hidden = false;
                this.GridPanel1.ColumnModel.Columns[13].Hidden = true;
            }
            else
            {
                //X.Msg.Alert("警告", "检查日期资料不正确").Show();
                _NotificationShow("警告，检查日期资料不正确");
                this.txtRESULT_DATE.Focus(false, 1000);
            }
        }

        /// <summary>
        /// 儲存新增結果，排除計算方式的項目
        /// </summary>
        protected void cmdSAVE(object sender, DirectEventArgs e)
        {
            this.GridPanel1.ColumnModel.Columns[10].Hidden = true;
            this.GridPanel1.ColumnModel.Columns[13].Hidden = false;
            
            ChangeRecords<RESULT_LOG> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<RESULT_LOG>();
            
            //foreach (RESULT_LOG created in sdh.Created)
            //{
            //}
            //foreach (RESULT_LOG deleted in sdh.Deleted)
            //{
            //}
            foreach (RESULT_LOG updated in sdh.Updated)
            {
                //計算結果另外更新(cmdCALC)
                if (updated.RESULT_CODE != "5013" && updated.RESULT_CODE != "5014" && updated.RESULT_CODE != "5017" && updated.RESULT_CODE != "5018")
                {
                    if (updated.ROW_ID != 0)
                    {
                        sSQL = "UPDATE a_result_log A, (SELECT (MAX(RESULT_VER)+1) AS RESULT_VER " +
                                                         "FROM a_result_log " +
                                                        "WHERE PAT_NO=" + updated.PAT_NO.ToString() + " " +
                                                          "AND RESULT_DATE='" + updated.RESULT_DATE + "' " +
                                                          "AND RESULT_CODE='" + updated.RESULT_CODE + "') B " +
                                  "SET A.RESULT_VER=B.RESULT_VER " +
                                "WHERE A.ROW_ID=" + updated.ROW_ID.ToString() + " ";
                        db.Excute(sSQL);
                    }
                    double z = 0;

                    if (Double.TryParse(updated.RESULT_VALUE_N, out z))
                        z = Convert.ToDouble(updated.RESULT_VALUE_N);
                    sSQL = "INSERT INTO a_result_log (RESULT_DATE, " +
                                                     "RESULT_CODE, " +
                                                     "RESULT_VER, " +
                                                     "RESULT_VALUE_T, " +
                                                     "RESULT_VALUE_N, " +
                                                     "KIN_DATE, " +
                                                     "KIN_USER, " +
                                                     "PAT_NO) " +
                                            "VALUE ('" + updated.RESULT_DATE + "', " +
                                                   "'" + updated.RESULT_CODE + "', " +
                                                   " " + "0" + " , " +
                                                   "'" + updated.RESULT_VALUE_N.Replace("'", "''") + "', " +
                                                   " " + z.ToString() + " , " +
                                                   "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                   "'" + _UserID + "', " +
                                                   " " + updated.PAT_NO.ToString() + " )";
                    db.Excute(sSQL);
                    //Store1.GetById(updated.RESULT_CODE).Commit();
                    //Store1.GetById("RESULT_CODE=" + updated.RESULT_CODE).Commit();
                }
            }
            Store1.CommitChanges();

            cmdCALC(sender, e);
            
            cmdREAD(sender, e);
            this.txtSTATUS.Text = "SAVE";
        }

        /// <summary>
        /// 新增動作完成，在增加計算方式的結果
        /// </summary>
        protected void cmdCALC(object sender, DirectEventArgs e)
        {
            List<RESULT_LOG> sdi = new StoreDataHandler(e.ExtraParams["datb"]).ObjectData<RESULT_LOG>();

            for (int i = 0; i < sdi.Count; i++)
            {
                if (sdi[i].RESULT_CODE == "5011")
                    dr5011 = sdi[i];
                if (sdi[i].RESULT_CODE == "5012")
                    dr5012 = sdi[i];
                if (sdi[i].RESULT_CODE == "5013")
                    dr5013 = sdi[i];
                if (sdi[i].RESULT_CODE == "5014")
                    dr5014 = sdi[i];
                if (sdi[i].RESULT_CODE == "5015")
                    dr5015 = sdi[i];
                if (sdi[i].RESULT_CODE == "5016")
                    dr5016 = sdi[i];
                if (sdi[i].RESULT_CODE == "5017")
                    dr5017 = sdi[i];
                if (sdi[i].RESULT_CODE == "5018")
                    dr5018 = sdi[i];
                if (sdi[i].RESULT_CODE == "4047")
                    dr4047 = sdi[i];
                if (sdi[i].RESULT_CODE == "4048")
                    dr4048 = sdi[i];
            }

            //en.wikipedia.org/wiki/Body_surface_area
            //BMI計算=體重(kg)/身高(米)^2      bmi=5013 = 體重=5012 / Math.Pow(身高=5011(公分)/100, 2);//BMI計算
            if (dr5013 != null && dr5012 != null && dr5011 != null)
            {
                string s5013 = "";
                double z5012 = 0;
                double z5011 = 0;
                if (Double.TryParse(dr5012.RESULT_VALUE_N, out z5012))
                    z5012 = Convert.ToDouble(dr5012.RESULT_VALUE_N);
                if (Double.TryParse(dr5011.RESULT_VALUE_N, out z5011))
                    z5011 = Convert.ToDouble(dr5011.RESULT_VALUE_N);
                if (z5012 != 0 && z5011 != 0)
                {
                    s5013 = (z5012 / Math.Pow((z5011 / 100), 2)).ToString("0.000");
                    if (dr5013.RESULT_VALUE_O != s5013)
                        NewResult(dr5013, s5013);
                }
            }

            //double surface=5014 = Math.Pow(身高=5011(公分), 0.725) * Math.Pow(體重=5012, 0.425) * 0.007184 ;//體表面積計算
            if (dr5014 != null && dr5011 != null && dr5012 != null)
            {
                string s5014 = "";
                double z5011 = 0;
                double z5012 = 0;
                if (Double.TryParse(dr5011.RESULT_VALUE_N, out z5011))
                    z5011 = Convert.ToDouble(dr5011.RESULT_VALUE_N);
                if (Double.TryParse(dr5012.RESULT_VALUE_N, out z5012))
                    z5012 = Convert.ToDouble(dr5012.RESULT_VALUE_N);
                if (z5011 != 0 && z5012 != 0)
                {
                    s5014 = (Math.Pow(z5011, 0.725) * Math.Pow(z5012, 0.425) * 0.007184).ToString("0.000");
                    if (dr5014.RESULT_VALUE_O != s5014)
                        NewResult(dr5014, s5014);
                }
            }

            //URR=5017 = 100 × (1 - (透析後BUN=4048 / 透析前BUN=4047) )
            if (dr5017 != null && dr4048 != null && dr4047 != null)
            {
                string s5017 = "";
                double z4048 = 0;
                double z4047 = 0;
                if (Double.TryParse(dr4048.RESULT_VALUE_N, out z4048))
                    z4048 = Convert.ToDouble(dr4048.RESULT_VALUE_N);
                if (Double.TryParse(dr4047.RESULT_VALUE_N, out z4047))
                    z4047 = Convert.ToDouble(dr4047.RESULT_VALUE_N);
                if (z4048 != 0 && z4047 != 0)
                {
                    s5017 = (100 * (1 - (z4048 / z4047))).ToString("0.000");
                    if (dr5017.RESULT_VALUE_O != s5017)
                        NewResult(dr5017, s5017);
                }
            }

            //Kt/V自然對數公式（natural logarithm formula）
            //Kt/V = -Ln ( R - 0.008 * t ) + ( 4 - 3.5 * R ) * UF/W
            //double spKtV=5018 = -Math.Log((透析後BUN=4048 / 透析前BUN=4047) - 0.008 * 表透析時間=5015(小時)) 
            //                  + (4 - 3.5 * (透析後BUN=4048 / 透析前BUN=4047)) * 超過濾之體積=5016 / 體重=5012(kg);
            if (dr5018 != null && dr4048 != null && dr4047 != null && dr5015 != null && dr5016 != null && dr5012 != null)
            {
                string s5018 = "";
                double z4048 = 0;
                double z4047 = 0;
                double z5015 = 0;
                double z5016 = 0;
                double z5012 = 0;
                if (Double.TryParse(dr4048.RESULT_VALUE_N, out z4048))
                    z4048 = Convert.ToDouble(dr4048.RESULT_VALUE_N);
                if (Double.TryParse(dr4047.RESULT_VALUE_N, out z4047))
                    z4047 = Convert.ToDouble(dr4047.RESULT_VALUE_N);
                if (Double.TryParse(dr5015.RESULT_VALUE_N, out z5015))
                    z5015 = Convert.ToDouble(dr5015.RESULT_VALUE_N);
                if (Double.TryParse(dr5016.RESULT_VALUE_N, out z5016))
                    z5016 = Convert.ToDouble(dr5016.RESULT_VALUE_N);
                if (Double.TryParse(dr5012.RESULT_VALUE_N, out z5012))
                    z5012 = Convert.ToDouble(dr5012.RESULT_VALUE_N);
                if (z4048 != 0 && z4047 != 0 && z5015 != 0 && z5016 != 0 && z5012 != 0)
                {
                    s5018 = (-Math.Log((z4048 / z4047) - 0.008 * z5015) + (4 - 3.5 * (z4048 / z4047)) * z5016 / z5012).ToString("0.0");
                    if (dr5018.RESULT_VALUE_O != s5018)
                        NewResult(dr5018, s5018);
                }
            }
        }

        /// <summary>
        /// 新增一筆a_result_log，RESULT_VER=0表示目前或是最後修改紀錄，1~n愈大愈近，1是最早輸入的結果
        /// </summary>
        private void NewResult(RESULT_LOG nr, string ns)
        {
            if (nr.ROW_ID != 0)
            {
                sSQL = "UPDATE a_result_log A, (SELECT (MAX(RESULT_VER)+1) AS RESULT_VER " +
                                                 "FROM a_result_log " +
                                                "WHERE PAT_NO=" + nr.PAT_NO.ToString() + " " +
                                                  "AND RESULT_DATE='" + nr.RESULT_DATE + "' " +
                                                  "AND RESULT_CODE='" + nr.RESULT_CODE + "') B " +
                          "SET A.RESULT_VER=B.RESULT_VER " +
                        "WHERE A.ROW_ID=" + nr.ROW_ID.ToString() + " ";
                db.Excute(sSQL);
            }
            sSQL = "INSERT INTO a_result_log (RESULT_DATE, " +
                                             "RESULT_CODE, " +
                                             "RESULT_VER, " +
                                             "RESULT_VALUE_T, " +
                                             "RESULT_VALUE_N, " +
                                             "KIN_DATE, " +
                                             "KIN_USER, " +
                                             "PAT_NO) " +
                                    "VALUE ('" + nr.RESULT_DATE + "', " +
                                           "'" + nr.RESULT_CODE + "', " +
                                           " " + "0" + " , " +
                                           "'" + ns + "', " +
                                           " " + ns + " , " +
                                           "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                           "'" + _UserID + "', " +
                                           " " + nr.PAT_NO.ToString() + " )";
            db.Excute(sSQL);
        }

        /// <summary>
        /// TreePanel上的按鈕，顯示全部或是最近3筆
        /// </summary>
        protected void btnCount_Click(object sender, EventArgs e)
        {
            Ext.Net.Button bb = (Ext.Net.Button)sender;

            string sPAT_NO = _PAT_ID;
            foreach (Ext.Net.Node nn in root.Children)
                for (int i = nn.Children.Count - 1; i >= 0; i--)
                    nn.Children.RemoveAt(i);
            
            //for (int i = rA.Children.Count - 1; i >= 0; i--)
            //    rA.Children.RemoveAt(i);
            //for (int i = rB.Children.Count - 1; i >= 0; i--)
            //    rB.Children.RemoveAt(i);
            //for (int i = rC.Children.Count - 1; i >= 0; i--)
            //    rC.Children.RemoveAt(i);
            //for (int i = rD.Children.Count - 1; i >= 0; i--)
            //    rD.Children.RemoveAt(i);
            //for (int i = rE.Children.Count - 1; i >= 0; i--)
            //    rE.Children.RemoveAt(i);
            //for (int i = rZ.Children.Count - 1; i >= 0; i--)
            //    rZ.Children.RemoveAt(i);

            if (bb.Text != "全部")
            {
                bb.Text = "全部";
                //AddChild(rA, "A", "实验室检查", "G001", 0);
                AddChild2(rB, "B", "血常规", "G007", 0);
                AddChild2(rC, "C", "血型", "G008", 0);
                AddChild2(rD, "D", "输血四项", "G009", 0);
                AddChild2(rE, "E", "血凝分析", "G010", 0);
                AddChild2(rF, "F", "尿液分析", "G011", 0);
                AddChild2(rG, "G", "生化二组", "G012", 0);
                AddChild2(rH, "H", "粪便常规", "G013", 0);
                AddChild(rY, "Y", "辅助检查", "G004", 0);
            }
            else
            {
                bb.Text = "前" + iiCNT.ToString();
                //AddChild(rA, "A", "实验室检查", "G001", iiCNT);
                AddChild2(rB, "B", "血常规", "G007", iiCNT);
                AddChild2(rC, "C", "血型", "G008", iiCNT);
                AddChild2(rD, "D", "输血四项", "G009", iiCNT);
                AddChild2(rE, "E", "血凝分析", "G010", iiCNT);
                AddChild2(rF, "F", "尿液分析", "G011", iiCNT);
                AddChild2(rG, "G", "生化二组", "G012", iiCNT);
                AddChild2(rH, "H", "粪便常规", "G013", iiCNT);
                AddChild(rY, "Y", "辅助检查", "G004", iiCNT);
            }

        }

        /// <summary>
        /// 點選TreePanel上的Node
        /// </summary>
        protected void Node_Click(object sender, DirectEventArgs e)
        {
            string sID = "";
            string sTEXT = "";
           
            sID = e.ExtraParams["rID"];
            sTEXT = e.ExtraParams["rTEXT"];
            string[] t = sTEXT.Split(new char[] { '(' });
            if (sID.Substring(1, 1) != "_" && sTEXT != "无")
            {
                txtRESULT_DATE.Text = t[0];
                if (cboRITEM_CODE.SelectedItem != null)
                {
                    cboRITEM_CODE.SelectedItem.Index = 0;
                    cboRITEM_CODE.SelectedItem.Text = "全部";
                    cboRITEM_CODE.SelectedItem.Value = "";
                    cboRITEM_CODE.Select(0);
                }
                Panel12.Expand();

                switch (sID.Substring(0, 1))
                {
                    case "A":
                        this.txtGROUP.Text = "G001";
                        this.txtGROUP_NAME.Text = "实验室检查";
                        break;
                    case "B":
                        //this.txtGROUP.Text = "G003";
                        //this.txtGROUP_NAME.Text = "透析充分性";
                        this.txtGROUP.Text = "G007";
                        this.txtGROUP_NAME.Text = "血常规";
                        break;
                    case "C":
                        //this.txtGROUP.Text = "G002";
                        //this.txtGROUP_NAME.Text = "血压测量";
                        this.txtGROUP.Text = "G008";
                        this.txtGROUP_NAME.Text = "血型";
                        break;
                    case "D":
                        this.txtGROUP.Text = "G009";
                        this.txtGROUP_NAME.Text = "输血四项";
                        break;
                    case "E":
                        //this.txtGROUP.Text = "G005";
                        //this.txtGROUP_NAME.Text = "透析检查总评价";
                        this.txtGROUP.Text = "G010";
                        this.txtGROUP_NAME.Text = "血凝分析";
                        break;
                    case "F":
                        this.txtGROUP.Text = "G011";
                        this.txtGROUP_NAME.Text = "尿液分析";
                        break;
                    case "G":
                        this.txtGROUP.Text = "G012";
                        this.txtGROUP_NAME.Text = "生化二组";
                        break;
                    case "H":
                        this.txtGROUP.Text = "G013";
                        this.txtGROUP_NAME.Text = "粪便常规";
                        break;
                    case "Y":
                        this.txtGROUP.Text = "G004";
                        this.txtGROUP_NAME.Text = "辅助检查";
                        break;
                    case "Z":
                        this.txtGROUP.Text = "";
                        this.txtGROUP_NAME.Text = "检查结果总评价";
                        break;
                    default:
                        this.txtGROUP.Text = "";
                        this.txtGROUP_NAME.Text = "";
                        break;
                }
                cmdREAD(sender, e);
                this.GridPanel1.Title = this.txtGROUP_NAME.Text;
            }
            else
            {
                string sGROUP = "";
                switch (sID.Substring(0, 1))
                {
                    case "A":
                        this.txtGROUP.Text = "G001";
                        this.txtGROUP_NAME.Text = "实验室检查";
                        break;
                    case "B":
                        //this.txtGROUP.Text = "G003";
                        //this.txtGROUP_NAME.Text = "透析充分性";
                        this.txtGROUP.Text = "G007";
                        this.txtGROUP_NAME.Text = "血常规";
                        sGROUP = this.txtGROUP.Text;
                        break;
                    case "C":
                        //this.txtGROUP.Text = "G002";
                        //this.txtGROUP_NAME.Text = "血压测量";
                        this.txtGROUP.Text = "G008";
                        this.txtGROUP_NAME.Text = "血型";
                        sGROUP = this.txtGROUP.Text;
                        break;
                    case "D":
                        this.txtGROUP.Text = "G009";
                        this.txtGROUP_NAME.Text = "输血四项";
                        sGROUP = this.txtGROUP.Text;
                        break;
                    case "E":
                        //this.txtGROUP.Text = "G005";
                        //this.txtGROUP_NAME.Text = "透析检查总评价";
                        this.txtGROUP.Text = "G010";
                        this.txtGROUP_NAME.Text = "血凝分析";
                        sGROUP = this.txtGROUP.Text;
                        break;
                    case "F":
                        this.txtGROUP.Text = "G011";
                        this.txtGROUP_NAME.Text = "尿液分析";
                        sGROUP = this.txtGROUP.Text;
                        break;
                    case "G":
                        this.txtGROUP.Text = "G012";
                        this.txtGROUP_NAME.Text = "生化二组";
                        sGROUP = this.txtGROUP.Text;
                        break;
                    case "H":
                        this.txtGROUP.Text = "G013";
                        this.txtGROUP_NAME.Text = "粪便常规";
                        sGROUP = this.txtGROUP.Text;
                        break;
                    case "Y":
                        this.txtGROUP.Text = "G004";
                        this.txtGROUP_NAME.Text = "辅助检查";
                        break;
                    case "Z":
                        this.txtGROUP.Text = "";
                        this.txtGROUP_NAME.Text = "检查结果总评价";
                        break;
                    default:
                        this.txtGROUP.Text = "";
                        this.txtGROUP_NAME.Text = "";
                        break;
                }
                
                if (sGROUP != "")
                {
                    string sPAT_NO = _PAT_ID;
                    sSQL = "SELECT A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, " +
                          "B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, " +
                          "A.RESULT_VALUE_T AS RESULT_VALUE_O, " +
                          "B.RITEM_UNIT, A.RESULT_VALUE_N AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, " +
                          "CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID " +
                     "FROM a_result_log A, (SELECT PAT_NO, MIN(RESULT_DATE) AS RESULT_DATE, RESULT_CODE " +
                                             "FROM a_result_log " +
                                            "WHERE PAT_NO=" + sPAT_NO + " " +
                                              "AND RESULT_VER=0 " +
                                            "GROUP BY PAT_NO, RESULT_CODE) D " +
                     "LEFT JOIN a_ritem_setup B " +
                       "ON D.RESULT_CODE=B.RITEM_CODE " +
                      "AND D.RESULT_DATE>=B.BEG_DATE " +
                      "AND D.RESULT_DATE<=B.END_DATE " +
                    "WHERE A.PAT_NO=D.PAT_NO " +
                      "AND A.RESULT_DATE=D.RESULT_DATE " +
                      "AND A.RESULT_CODE=D.RESULT_CODE " +
                      "AND A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='" + sGROUP + "') " +
                    "ORDER BY B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_SN, B.RITEM_CODE ";
                    DataTable dt = db.Query(sSQL);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        if (Convert.IsDBNull(dr["RITEM_UNIT"]))
                            dr["RITEM_UNIT"] = string.Empty;
                        if (Convert.IsDBNull(dr["RITEM_HIGH1"]))
                            dr["RITEM_HIGH1"] = string.Empty;
                        if (Convert.IsDBNull(dr["RITEM_LOW1"]))
                            dr["RITEM_LOW1"] = string.Empty;
                        if (Convert.IsDBNull(dr["RESULT_VALUE_O"]))
                            dr["RESULT_VALUE_O"] = string.Empty;

                        DateTime dt1 = Convert.ToDateTime(dr["RESULT_DATE"].ToString());
                        DateTime dt2 = Convert.ToDateTime(dr["RESULT_DAYS"].ToString());
                        Double ts1 = new TimeSpan(dt2.Ticks - dt1.Ticks).TotalDays;

                        if (ts1 <= 30)
                            dr["RESULT_DAYS"] = Convert.ToInt64(ts1).ToString() + "天以前";
                        else
                        {
                            Double ts2 = ts1 / 30;
                            if (ts2 <= 12)
                                dr["RESULT_DAYS"] = Convert.ToInt64(ts2).ToString() + "个月以前";
                            else
                            {
                                Double ts3 = ts2 / 12;
                                dr["RESULT_DAYS"] = Convert.ToInt64(ts3).ToString() + "年以前";
                            }
                        }
                    }

                    dt.AcceptChanges();

                    Store istore = this.GridPanel1.GetStore();
                    istore.DataSource = db.GetDataArray_AddRowNum(dt);
                    istore.DataBind();
                    this.GridPanel1.ColumnModel.Columns[10].Hidden = true;
                    this.GridPanel1.ColumnModel.Columns[13].Hidden = false;
                }
                        
            }
            this.GridPanel1.Title = this.txtGROUP_NAME.Text;
        }

        /// <summary>
        /// 顯示單項檢驗的歷次結果
        /// </summary>
        protected void logEXAM(object sender, DirectEventArgs e)
        {
            if (this.txtSTATUS.Text != "ADD")
            {
                string ssPAT_NO = e.ExtraParams["PAT_NO"];
                string ssRESULT_DATE = e.ExtraParams["RESULT_DATE"];
                string ssRESULT_CODE = e.ExtraParams["RESULT_CODE"];
                if (this.txtCODE.Text == (ssRESULT_CODE + "A"))
                {
                    this.PanelR.Collapse();
                    this.txtCODE.Text = "";
                }
                else
                {
                    this.txtCODE.Text = ssRESULT_CODE + "A";
                    sSQL = "SELECT A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, ";
                    sSQL += "      B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, ";
                    sSQL += "      B.RITEM_UNIT, A.RESULT_VALUE_T AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, ";
                    sSQL += "      CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID, A.KIN_DATE, A.KIN_USER ";
                    sSQL += " FROM a_result_log A ";
                    sSQL += " LEFT JOIN a_ritem_setup B ";
                    sSQL += "   ON A.RESULT_CODE=B.RITEM_CODE ";
                    sSQL += "  AND A.RESULT_DATE>=B.BEG_DATE ";
                    sSQL += "  AND A.RESULT_DATE<=B.END_DATE ";
                    sSQL += "WHERE A.RESULT_VER=0 ";
                    sSQL += "  AND A.RESULT_CODE='" + ssRESULT_CODE + "' ";
                    sSQL += "  AND A.PAT_NO=" + ssPAT_NO + " ";
                    sSQL += "ORDER BY A.RESULT_DATE DESC ";
                    DataTable dt = db.Query(sSQL);

                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        dr = dt.Rows[i];
                        if (Convert.IsDBNull(dr["RITEM_UNIT"]))
                            dr["RITEM_UNIT"] = string.Empty;
                        if (Convert.IsDBNull(dr["RITEM_HIGH1"]))
                            dr["RITEM_HIGH1"] = string.Empty;
                        if (Convert.IsDBNull(dr["RITEM_LOW1"]))
                            dr["RITEM_LOW1"] = string.Empty;
                        if (Convert.IsDBNull(dr["RESULT_VALUE_O"]))
                            dr["RESULT_VALUE_O"] = string.Empty;

                        DateTime dt1 = Convert.ToDateTime(dr["RESULT_DATE"].ToString());
                        DateTime dt2 = Convert.ToDateTime(dr["RESULT_DAYS"].ToString());
                        Double ts1 = new TimeSpan(dt2.Ticks - dt1.Ticks).TotalDays;

                        if (ts1 <= 30)
                            dr["RESULT_DAYS"] = Convert.ToInt64(ts1).ToString() + "天以前";
                        else
                        {
                            Double ts2 = ts1 / 30;
                            if (ts2 <= 12)
                                dr["RESULT_DAYS"] = Convert.ToInt64(ts2).ToString() + "个月以前";
                            else
                            {
                                Double ts3 = ts2 / 12;
                                dr["RESULT_DAYS"] = Convert.ToInt64(ts3).ToString() + "年以前";
                            }
                        }
                    }
                    dt.AcceptChanges();

                    if (dt.Rows.Count ==1)
                        _NotificationShow("注意，只有一(本)次检查");
                    Store istore = GridPanel2.GetStore();
                    istore.DataSource = db.GetDataArray_AddRowNum(dt);
                    istore.DataBind();
                    //this.GridPanel2.ColumnModel.Columns[0].Hidden = true;
                    //this.GridPanel2.ColumnModel.Columns[2].Hidden = true;
                    //this.GridPanel2.ColumnModel.Columns[4].Hidden = true;
                    //this.GridPanel2.ColumnModel.Columns[6].Hidden = true;
                    this.GridPanel2.Title = "历次结果";
                    this.PanelR.Expand();
                }
            }
        }

        /// <summary>
        /// 顯示該報告的歷次修改
        /// </summary>
        protected void logEDIT(object sender, DirectEventArgs e)
        {
            if (this.txtSTATUS.Text != "ADD")
            {
                string ssPAT_NO = e.ExtraParams["PAT_NO"];
                string ssRESULT_DATE = e.ExtraParams["RESULT_DATE"];
                string ssRESULT_CODE = e.ExtraParams["RESULT_CODE"];
                if (this.txtCODE.Text == (ssRESULT_CODE + "B"))
                {
                    this.PanelR.Collapse();
                    this.txtCODE.Text = "";
                }
                else
                {
                    this.txtCODE.Text = ssRESULT_CODE + "B";
                    sSQL = "SELECT A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, ";
                    sSQL += "      B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, ";
                    sSQL += "      B.RITEM_UNIT, A.RESULT_VALUE_T AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, ";
                    sSQL += "      CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID, A.KIN_DATE, A.KIN_USER ";
                    sSQL += " FROM a_result_log A ";
                    sSQL += " LEFT JOIN a_ritem_setup B ";
                    sSQL += "   ON A.RESULT_CODE=B.RITEM_CODE ";
                    sSQL += "  AND A.RESULT_DATE>=B.BEG_DATE ";
                    sSQL += "  AND A.RESULT_DATE<=B.END_DATE ";
                    sSQL += "WHERE A.RESULT_VER<>0 ";
                    sSQL += "  AND A.RESULT_DATE='" + ssRESULT_DATE + "' ";
                    sSQL += "  AND A.RESULT_CODE='" + ssRESULT_CODE + "' ";
                    sSQL += "  AND A.PAT_NO=" + ssPAT_NO + " ";
                    sSQL += "ORDER BY A.RESULT_VER ";
                    DataTable dt = db.Query(sSQL);

                    if (dt.Rows.Count == 0)
                    {
                        _NotificationShow("注意，没有修改过的记录");
                    }
                    else
                    {
                        for (int i = 0; i < dt.Rows.Count; i++)
                        {
                            dr = dt.Rows[i];
                            if (Convert.IsDBNull(dr["RITEM_UNIT"]))
                                dr["RITEM_UNIT"] = string.Empty;
                            if (Convert.IsDBNull(dr["RITEM_HIGH1"]))
                                dr["RITEM_HIGH1"] = string.Empty;
                            if (Convert.IsDBNull(dr["RITEM_LOW1"]))
                                dr["RITEM_LOW1"] = string.Empty;
                            if (Convert.IsDBNull(dr["RESULT_VALUE_O"]))
                                dr["RESULT_VALUE_O"] = string.Empty;
                            if (Convert.IsDBNull(dr["RESULT_VALUE_N"]))
                                dr["RESULT_VALUE_N"] = string.Empty;
                        }
                        dt.AcceptChanges();
                    }

                    Store istore = GridPanel2.GetStore();
                    istore.DataSource = db.GetDataArray_AddRowNum(dt);
                    istore.DataBind();
                    //this.GridPanel2.ColumnModel.Columns[10].Hidden = true;
                    //this.GridPanel2.ColumnModel.Columns[13].Hidden = false;
                    this.GridPanel2.Title = "修改历程";
                    this.PanelR.Expand();
                }
            }
        }    
        


        protected void RowSelect(object sender, DirectEventArgs e)
        {
            //txtWORDS.Text = e.ExtraParams["RITEM_WORDS"];
            //if (txtWORDS.Text == "")
            //    txtWORDS.Hidden = true;
            //else
            //    txtWORDS.Hidden = false; 
        }
    }
}