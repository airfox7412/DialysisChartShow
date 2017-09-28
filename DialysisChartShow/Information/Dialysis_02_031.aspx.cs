using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using Dialysis_Chart_Show.tools;
using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_02_031 : BaseForm
    {
        private int iiCNT = 0;
        private string sCODE = "";
        private string sSQL = "";
        private DataRow dr;
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
        string btnText = ""; //前7

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

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.txtPAT_NO.Text = _PAT_ID;
                BuildTree1(TreePanel1.Root);
                //GetAllRitem(); 
            }
        }

        #region 取得樹狀目錄1
        private Ext.Net.NodeCollection BuildTree1(Ext.Net.NodeCollection nodes)
        {
            Ext.Net.Button btnExpand = new Ext.Net.Button();
            btnExpand.Text = "展开";
            btnExpand.Listeners.Click.Handler = TreePanel1.ClientID + ".expandAll();";

            Ext.Net.Button btnCollapse = new Ext.Net.Button();
            btnCollapse.Text = "收合";
            btnCollapse.Listeners.Click.Handler = TreePanel1.ClientID + ".collapseAll();";

            Ext.Net.Button btnCount = new Ext.Net.Button();
            btnCount.Text = btnText;
            btnCount.Click += btnCount_Click;
            //btnCount.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(btnCount_Click);
            btnCount.AutoPostBack = false;

            Toolbar toolBar = new Toolbar(); //Tree Head 按鈕
            toolBar.ID = "Toolbar";
            toolBar.Items.Add(btnExpand);
            toolBar.Items.Add(btnCollapse);
            toolBar.Items.Add(btnCount);
            TreePanel1.TopBar.Add(toolBar);

            Ext.Net.Node root = new Ext.Net.Node();
            root.Text = "实验室检查";
            root.NodeID = "__";
            root.Cls = "large-font";
            root.Expanded = true;
            nodes.Add(root);

            string sSQL = "SELECT GROUP_CODE, GROUP_NAME FROM a_item_group ";
            sSQL += "WHERE GROUP_CODE='G002' ";
            sSQL += "ORDER BY GROUP_CLASS ";
            DataTable dt = db.Query(sSQL);

            string groupcode, groupname;
            foreach (DataRow row in dt.Rows)
            {
                groupcode = row.Field<string>(0);
                groupname = row.Field<string>(1);
                Ext.Net.Node groupNode = new Ext.Net.Node()
                {
                    Text = groupname,
                    Icon = Icon.ReportUser,
                    NodeID = groupcode + "_",
                    Cls = "large-font"
                };
                root.Children.Add(groupNode);
                AddChild(groupNode, groupcode, groupname, groupcode, iiCNT);
            }
            TreePanel1.Render();
            return nodes;
        }
        #endregion

        #region 建立樹狀目錄的子明細
        private void AddChild(Ext.Net.Node nn, string ss, string st, string sg, int ii)
        {
            string sPAT_NO = _PAT_ID;
            string sSQL = "";
            if (ss != "G004")
            {
                sSQL = "SELECT RESULT_DATE AS TestData, COUNT(*) AS CNT FROM a_result_log ";
                sSQL += "WHERE PAT_NO='" + sPAT_NO + "' AND RESULT_CLASS='" + sg + "' AND RESULT_VER=0 ";
                sSQL += "GROUP BY RESULT_DATE ";
                sSQL += "ORDER BY RESULT_DATE DESC ";
            }
            else //G004 輔助檢查
            {
                sSQL = "SELECT dat_1 AS TestData, COUNT(*) AS CNT FROM zinfo_d_02 ";
                sSQL += "WHERE pat_id='" + sPAT_NO + "' ";
                sSQL += "GROUP BY dat_1 ";
                sSQL += "ORDER BY dat_1 DESC ";
            }
            if (ii != 0)
            {
                sSQL += "LIMIT 0," + ii.ToString();
            }

            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Ext.Net.Node cnode = new Ext.Net.Node()
                    {
                        Text = dt.Rows[i]["TestData"].ToString() + "(" + dt.Rows[i]["CNT"].ToString() + ")",
                        NodeID = ss + i.ToString(),
                        Icon = Icon.Page,
                        Cls = "blue-large-font",
                        Leaf = true
                    };
                    nn.Children.Add(cnode);
                }
            }
            else
            {
                Ext.Net.Node cnode = new Ext.Net.Node()
                {
                    Text = "无检验纪录",
                    NodeID = ss + "_" + "NEW",
                    Icon = Icon.PageWhite,
                    Cls = "blue-large-font",
                    Leaf = true
                };
                nn.Children.Add(cnode);
            }
            nn.Expanded = true;
        }
        #endregion

        #region 取得所有檢查項目
        private void GetAllRitem()
        {
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
        }
        #endregion

        #region 取得檢查大分類名稱
        private string GetGroupName(string sID)
        {
            string sSQL = "SELECT GROUP_CODE, GROUP_NAME FROM a_item_group ";
            sSQL += "WHERE GROUP_CODE='" + sID.Substring(0, 4) + "' ";
            sSQL += "GROUP BY GROUP_CODE ";
            try
            {
                DataTable dt = db.Query(sSQL);
                return dt.Rows[0]["GROUP_NAME"].ToString();
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 取得檢查大分類代碼
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

        #region 點選TreePanel上的Node
        protected void Node_Click(object sender, DirectEventArgs e)
        {
            string sID = "";
            string sTEXT = "";

            PanelR.Hidden = false;
            DetailPanel.Hidden = false;
            Panel19.Hidden = true;

            sID = e.ExtraParams["rID"];
            sTEXT = e.ExtraParams["rTEXT"];
            string[] t = sTEXT.Split(new char[] { '(' });
            if (sID == "__")
            {
                return;
            }
            this.txtGROUP.Text = sID.Substring(0, 4);
            this.txtGROUP_NAME.Text = GetGroupName(sID);
            txtRESULT_DATE.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
            if (this.txtGROUP.Text == "G004") //輔助檢查
            {
                PanelR.Hidden = true;
                DetailPanel.Hidden = true;
                Panel19.Hidden = false;
                Panel19.Loader.SuspendScripting();
                Panel19.Loader.Url = "./Dialysis_04_02.aspx?editmode=list";
                Panel19.Loader.DisableCaching = true;
                Panel19.LoadContent();
            }
            else
            {
                if (sID.Substring(4, 1) != "_" && sTEXT != "无检验纪录")
                {
                    txtRESULT_DATE.Text = t[0];
                    //if (cboRITEM_CODE.SelectedItem != null)
                    //{
                    //    cboRITEM_CODE.SelectedItem.Index = 0;
                    //    cboRITEM_CODE.SelectedItem.Text = "全部";
                    //    cboRITEM_CODE.SelectedItem.Value = "";
                    //    cboRITEM_CODE.Select(0);
                    //}
                    DetailPanel.Expand();
                    cmdREAD(sender, e);
                }
                else
                {
                    cmdREAD(sender, e);
                }
                this.DetailPanel.Title = this.txtGROUP_NAME.Text;
            }
        }
        #endregion

        #region 依據日期讀取檢驗項目
        protected void cmdREAD(object sender, DirectEventArgs e)
        {
            this.txtSTATUS.Text = "READ";
            this.txtRESULT_DATE.Disabled = false;
            this.cboRITEM_CODE.Disabled = false;
            this.btnSAVE.Disabled = true;
            //this.btnCALC.Disabled = true;
            this.btnCANCEL.Disabled = true;
            this.btnREAD.Disabled = false;
            this.btnLAST.Disabled = false;
            this.btnADD.Disabled = false;
            //this.DetailPanel.Title = "检验结果";
            this.DetailPanel.Title = this.txtGROUP_NAME.Text;
            ReadDB();
        }
        #endregion

        #region 讀取並顯示已檢驗項目
        protected void ReadDB()
        {
            string sGROUP = this.txtGROUP.Text;
            string sPAT_NO = _PAT_ID;
            string sDATE = _Get_YMD2(this.txtRESULT_DATE.Text);
            sCODE = GetComboBoxValue(cboRITEM_CODE);
            if (sDATE != "" || sCODE != "")
            {
                sSQL = "SELECT a.ROW_ID, a.RESULT_DATE, a.RESULT_CLASS, a.RESULT_CODE, a.RESULT_VALUE_T, a.RESULT_VALUE_N, a.PAT_NO, ";
                sSQL += "b.RITEM_TYPE, b.RITEM_NAME, b.RITEM_NAME_S, b.RITEM_UNIT, b.RITEM_LOW1, b.RITEM_HIGH1, CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, b.RITEM_WORDS ";
                sSQL += "FROM a_result_log a ";
                sSQL += "LEFT JOIN  a_ritem_setup b ";
                sSQL += "ON a.RESULT_CLASS=b.RITEM_CLASS AND a.RESULT_CODE=b.RITEM_CODE ";
                sSQL += "WHERE a.RESULT_CLASS='" + sGROUP + "' ";
                sSQL += "AND a.RESULT_DATE='" + sDATE + "' ";
                sSQL += "AND a.PAT_NO=" + sPAT_NO + " ";
                sSQL += "AND a.RESULT_VER=0 ";
                sSQL += "ORDER BY a.RESULT_DATE, a.RESULT_CODE ";
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
                    if (Convert.IsDBNull(dr["RESULT_VALUE_T"]))
                        dr["RESULT_VALUE_T"] = string.Empty;
                    if (Convert.IsDBNull(dr["RESULT_VALUE_N"]))
                        dr["RESULT_VALUE_N"] = string.Empty;
                    if (Convert.IsDBNull(dr["RITEM_WORDS"]))
                        dr["RITEM_WORDS"] = string.Empty;
                    if (Convert.IsDBNull(dr["ROW_ID"]))
                        dr["ROW_ID"] = 0;

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
                Store istore = this.GridPanel1.GetStore();
                istore.DataSource = db.GetDataArray_AddRowNum(dt);
                istore.DataBind();
                this.GridPanel1.ColumnModel.Columns[9].Hidden = true;
                this.GridPanel1.ColumnModel.Columns[10].Hidden = false;
                this.GridPanel1.ColumnModel.Columns[11].Hidden = false;
                this.GridPanel1.ColumnModel.Columns[12].Hidden = false;
                this.GridPanel1.ColumnModel.Columns[13].Hidden = false;
            }
            else
            {
                X.Msg.Alert("警告", "检查日期资料不正确").Show();
                _NotificationShow("警告，检查日期与检查项目资料不正确");
                this.txtRESULT_DATE.Focus(true, 1000);
            }
        }
        #endregion

        #region 讀取每個檢驗項目最近一筆資料
        protected void cmdLAST(object sender, DirectEventArgs e)
        {
            this.LastData();
            this.txtSTATUS.Text = "LAST";
        }
        #endregion

        #region 讀取每個檢驗項目最近一筆資料
        private void LastData()
        {
            this.txtRESULT_DATE.Disabled = false;
            this.cboRITEM_CODE.Disabled = false;
            this.btnSAVE.Disabled = true;
            //this.btnCALC.Disabled = true;
            this.btnCANCEL.Disabled = true;
            this.btnREAD.Disabled = false;
            this.btnLAST.Disabled = false;
            this.btnADD.Disabled = false;
            this.DetailPanel.Title = "检验结果";
            string sGROUP = this.txtGROUP.Text;

            string sPAT_NO = _PAT_ID;
            sSQL = "SELECT a.ROW_ID, a.RESULT_DATE, a.RESULT_CLASS, a.RESULT_CODE, a.RESULT_VALUE_T, a.RESULT_VALUE_N, a.PAT_NO, ";
            sSQL += "b.RITEM_TYPE, b.RITEM_NAME, b.RITEM_NAME_S, b.RITEM_UNIT, b.RITEM_LOW1, b.RITEM_HIGH1, CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, b.RITEM_WORDS ";
            sSQL += "FROM a_result_log a ";
            sSQL += "LEFT JOIN  a_ritem_setup b ";
            sSQL += "ON a.RESULT_CLASS=RITEM_CLASS AND a.RESULT_CODE=b.RITEM_CODE ";
            sSQL += "WHERE a.RESULT_CLASS='" + sGROUP + "' ";
            sSQL += "AND a.PAT_NO=" + sPAT_NO + " ";
            sSQL += "AND a.RESULT_VER=0 ";
            sSQL += "ORDER BY a.RESULT_DATE DESC, a.RESULT_CODE ";
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
                if (Convert.IsDBNull(dr["RESULT_VALUE_T"]))
                    dr["RESULT_VALUE_T"] = string.Empty;
                if (Convert.IsDBNull(dr["RESULT_VALUE_N"]))
                    dr["RESULT_VALUE_N"] = string.Empty;
                if (Convert.IsDBNull(dr["RITEM_WORDS"]))
                    dr["RITEM_WORDS"] = string.Empty;
                if (Convert.IsDBNull(dr["ROW_ID"]))
                    dr["ROW_ID"] = 0;

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
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
            this.GridPanel1.ColumnModel.Columns[9].Hidden = true;
            this.GridPanel1.ColumnModel.Columns[10].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[11].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[12].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[13].Hidden = false;
        }
        #endregion

        #region 添加：新增動作，一定要有日期，依照日期來新增
        protected void cmdADD(object sender, DirectEventArgs e)
        {
            this.txtSTATUS.Text = "ADD";
            string sGROUP = this.txtGROUP.Text;

            string sPAT_NO = _PAT_ID;
            string sDATE = _Get_YMD2(this.txtRESULT_DATE.Text);
            if (sDATE != "")
            {
                this.DetailPanel.Title = this.txtGROUP_NAME.Text + "　　　　　　　F1=阴性，F2弱阳性，F3=阳性，F4=强阳性，F5=未检";
                sSQL = "SELECT B.ROW_ID, '" + sDATE + "' AS RESULT_DATE, A.RITEM_CLASS, A.RITEM_CODE, B.RESULT_VALUE_T, B.RESULT_VALUE_N, " + sPAT_NO + " AS PAT_NO, ";
                sSQL += "A.RITEM_TYPE, A.RITEM_NAME, A.RITEM_NAME_S, A.RITEM_UNIT, A.RITEM_LOW1, A.RITEM_HIGH1, CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.RITEM_WORDS ";
                sSQL += "FROM a_ritem_setup A ";
                sSQL += "LEFT JOIN a_result_log B ";
                sSQL += "ON A.RITEM_CODE=B.RESULT_CODE AND A.RITEM_CLASS=B.RESULT_CLASS ";
                sSQL += "AND B.RESULT_DATE='" + sDATE + "' AND B.PAT_NO=" + sPAT_NO + " ";
                sSQL += "AND B.RESULT_VER=0 ";
                sSQL += "WHERE A.RITEM_CLASS='" + sGROUP + "' AND A.RITEM_USED='Y' ";
                sSQL += "ORDER BY A.RITEM_CODE";

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
                    if (Convert.IsDBNull(dr["RESULT_VALUE_T"]))
                        dr["RESULT_VALUE_T"] = string.Empty;
                    //if (Convert.IsDBNull(dr["RESULT_VALUE_N"]))
                    //    dr["RESULT_VALUE_N"] = string.Empty;
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
                //this.btnCALC.Disabled = false;
                this.btnCANCEL.Disabled = false;
                this.btnREAD.Disabled = true;
                this.btnLAST.Disabled = true;
                this.btnADD.Disabled = true;
                this.txtRESULT_DATE.Disabled = true;
                this.cboRITEM_CODE.Disabled = true;
                this.GridPanel1.ColumnModel.Columns[9].Hidden = false;
                this.GridPanel1.ColumnModel.Columns[9].Width = 80;
                this.GridPanel1.ColumnModel.Columns[10].Hidden = true;
                //this.GridPanel1.ColumnModel.Columns[11].Hidden = false;
                //this.GridPanel1.ColumnModel.Columns[12].Hidden = false;
                this.GridPanel1.ColumnModel.Columns[13].Hidden = true;
                //this.GridPanel1.ColumnModel.Columns[14].Width = 300;
                //this.GridPanel1.ColumnModel.Columns[14].Hidden = false;
            }
            else
            {
                //X.Msg.Alert("警告", "检查日期资料不正确").Show();
                _NotificationShow("警告，检查日期资料不正确");
                this.txtRESULT_DATE.Focus(false, 1000);
            }
        }
        #endregion

        #region 處理保存按鈕(儲存新增結果，排除計算方式的項目)
        protected void cmdSAVE(object sender, DirectEventArgs e)
        {

            this.GridPanel1.ColumnModel.Columns[9].Hidden = true;
            this.GridPanel1.ColumnModel.Columns[10].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[11].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[12].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[13].Hidden = false;

            ChangeRecords<RESULT_LOG> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<RESULT_LOG>();

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
                                                          "AND RESULT_CLASS='" + this.txtGROUP.Text + "' " +
                                                          "AND RESULT_CODE='" + updated.RESULT_CODE + "') B " +
                                "SET A.RESULT_VER=B.RESULT_VER " +
                                "WHERE A.ROW_ID=" + updated.ROW_ID.ToString() + " ";
                        db.Excute(sSQL);
                    }
                    double z = 0;

                    if (Double.TryParse(updated.RESULT_VALUE_N, out z))
                        z = Convert.ToDouble(updated.RESULT_VALUE_N);
                    sSQL = "INSERT INTO a_result_log (RESULT_DATE, " +
                                                     "RESULT_CLASS, " +
                                                     "RESULT_CODE, " +
                                                     "RESULT_VER, " +
                                                     "RESULT_VALUE_T, " +
                                                     "RESULT_VALUE_N, " +
                                                     "KIN_DATE, " +
                                                     "KIN_USER, " +
                                                     "PAT_NO) " +
                                            "VALUE ('" + updated.RESULT_DATE + "', " +
                                                   "'" + this.txtGROUP.Text + "', " +
                                                   "'" + updated.RESULT_CODE + "', " +
                                                   " " + "0" + " , " +
                                                   "'" + updated.RESULT_VALUE_N.Replace("'", "''") + "', " +
                                                   " " + z.ToString() + " , " +
                                                   "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                                   "'" + _UserID + "', " +
                                                   " " + updated.PAT_NO.ToString() + " )";
                    db.Excute(sSQL);
                }
            }
            Store1.CommitChanges();
            cmdREAD(sender, e);
            this.txtSTATUS.Text = "SAVE";
            BuildTree1(TreePanel1.Root);
        }
        #endregion

        #region 取消新增動作
        protected void cmdCANCEL(object sender, DirectEventArgs e)
        {
            this.GridPanel1.ColumnModel.Columns[9].Hidden = true;
            this.GridPanel1.ColumnModel.Columns[10].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[11].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[12].Hidden = false;
            this.GridPanel1.ColumnModel.Columns[13].Hidden = false;
            //this.GridPanel1.ColumnModel.Columns[14].Width = 0;
            //this.GridPanel1.ColumnModel.Columns[14].Hidden = true;
            Store1.RemoveAll(true);
            cmdREAD(sender, e);
            this.txtSTATUS.Text = "CANCEL";
        }
        #endregion

        #region TreePanel上的按鈕，更新Tree
        protected void btnRefresh_Click(object sender, EventArgs e)
        {
            Ext.Net.Button btnRefresh = (Ext.Net.Button)sender;
            string sPAT_NO = _PAT_ID;
        }
        #endregion

        #region 切換顯示日期統計筆數 TreePanel上的按鈕，顯示全部或是最近7筆
        protected void btnCount_Click(object sender, EventArgs e)
        {
            //Ext.Net.Button btn = (Ext.Net.Button)sender;
            if (btnText != "全部")
                btnText = "全部";
            else
                btnText = "前" + iiCNT.ToString();
        }
        #endregion

        #region 顯示單項檢驗的歷次結果
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
                    sSQL += "   ON A.RESULT_CODE=B.RITEM_CODE AND A.RESULT_CLASS=B.RITEM_CLASS ";
                    sSQL += "WHERE A.RESULT_VER=0 ";
                    sSQL += "  AND A.RESULT_CLASS='" + this.txtGROUP.Text + "' ";
                    sSQL += "  AND A.RESULT_CODE='" + ssRESULT_CODE + "' ";
                    sSQL += "  AND A.PAT_NO=" + ssPAT_NO + " ";
                    sSQL += "ORDER BY A.RESULT_DATE DESC ";
                    DataTable dt = db.Query(sSQL);

                    //String sql_stmt = "SELECT A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, " +
                    //" B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, " +
                    //" B.RITEM_UNIT, A.RESULT_VALUE_T AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, " +
                    //" CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID, A.KIN_DATE, A.KIN_USER " +
                    //" FROM a_result_log A " +
                    //" LEFT JOIN a_ritem_setup B " +
                    //"   ON A.RESULT_CODE=B.RITEM_CODE " +
                    //" WHERE A.RESULT_VER<>0 " +
                    //" AND A.RESULT_DATE='" + resultDate + "' " +
                    //" AND A.RESULT_CLASS='" + resultClass + "' " +
                    //" AND A.RESULT_CODE='" + resultCode + "' " +
                    //" AND A.PAT_NO=" + patNo + " " +
                    //" ORDER BY A.RESULT_VER ";

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

                    if (dt.Rows.Count == 1)
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
        #endregion

        #region 修改檢驗：單位,高低值
        protected void modifyUnitHighLow(object sender = null, DirectEventArgs e = null)
        {
            frmUnitRange.Show();
            TextCode.Text = e.ExtraParams["RESULT_CODE"];
            TextUnit.Text = e.ExtraParams["RITEM_UNIT"];
            TextHigh.Text = e.ExtraParams["RITEM_HIGH1"];
            TextLow.Text = e.ExtraParams["RITEM_LOW1"];
            _NotificationShow("單位: " + e.ExtraParams["RITEM_UNIT"] + ", 高值: " + e.ExtraParams["RITEM_HIGH1"] + ", 低值: " + e.ExtraParams["RITEM_LOW1"]);
        }
        #endregion

        #region 儲存檢驗：單位,高低值
        protected void btnUpdateUnitRange_Click(object sender, DirectEventArgs e)
        {
            _NotificationShow("檢驗代碼: " + TextCode.Text + ", 單位: " + TextUnit.Text + ", 高值: " + TextHigh.Text + ", 低值: " + TextLow.Text);
            String sql_stmt = "update a_ritem_setup set ritem_unit='" + TextUnit.Text + "', " +
                "ritem_low1='" + TextLow.Text + "', ritem_high1='" + TextHigh.Text + "' " +
                " WHERE RITEM_CODE='" + TextCode.Text + "'";
            //DBMysql db = new DBMysql();
            db.Excute(sql_stmt);
            frmUnitRange.Hide();
            cmdREAD(sender, e);
        }
        #endregion

        #region 放棄檢驗：單位,高低值
        protected void btnCANCELUnitRange_Click(object sender, DirectEventArgs e)
        {
            frmUnitRange.Close();
        }
        #endregion

        #region 取得歷史檢驗紀錄
        static public DataTable getModifiedHistory(String resultClass, String resultCode, String resultDate, String patNo, out String message)
        {
            String sql_stmt = "SELECT A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, " +
                    " B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, A.RESULT_VALUE_T AS RESULT_VALUE_O, " +
                    " B.RITEM_UNIT, A.RESULT_VALUE_T AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, " +
                    " CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID, A.KIN_DATE, A.KIN_USER " +
                    " FROM a_result_log A " +
                    " LEFT JOIN a_ritem_setup B " +
                    "   ON A.RESULT_CODE=B.RITEM_CODE AND A.RESULT_CLASS=B.RITEM_CLASS " +
                    " WHERE A.RESULT_VER<>0 " +
                    " AND A.RESULT_DATE='" + resultDate + "' " +
                    " AND A.RESULT_CLASS='" + resultClass + "' " +
                    " AND A.RESULT_CODE='" + resultCode + "' " +
                    " AND A.PAT_NO=" + patNo + " " +
                    " ORDER BY A.RESULT_VER ";
            String result;
            DataTable dt = DBMysql.query(sql_stmt, out result);
            if (dt.Rows.Count == 0)
            {
                message = "注意，没有修改过的记录";
            }
            else
            {
                message = null;
                DataRow dr;
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
            return dt;
        }
        #endregion

        #region 顯示該報告的歷次修改
        /// <summary>
        /// 顯示該報告的歷次修改
        /// </summary>
        protected void logEDIT(object sender, DirectEventArgs e)
        {
            if (this.txtSTATUS.Text == "ADD")
            {
                _NotificationShow("目前是在添加, 所以不能看歷次修改");
                return;
            }

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
                /**** removed by jeffrey at 2015/12/02
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
                ****/
                string sGROUP = this.txtGROUP.Text;
                String message;
                DataTable dt = getModifiedHistory(sGROUP, ssRESULT_CODE, ssRESULT_DATE, ssPAT_NO, out message);
                if (message != null)
                {
                    _NotificationShow(message);
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
        #endregion

        #region 維護檢驗大分類
        protected void MaintainItemGroup(object sender, DirectEventArgs e)
        {
            string GroupName = "实验室检查";
            string GroupCode = "G001";// e.ExtraParams["rID"]; 
            GroupName = GetGroupName(GroupCode);
            PanelR.Hidden = true;
            DetailPanel.Hidden = true;
            Panel19.Hidden = false;
            Panel19.Loader.SuspendScripting();
            Panel19.Loader.Url = "./Dialysis_04_00.aspx?GroupName=" + GroupName;
            Panel19.Loader.DisableCaching = true;
            Panel19.LoadContent();
        }
        #endregion

        #region 維護檢驗小分類
        protected void MaintainItem(object sender, DirectEventArgs e)
        {
            string GroupName = "实验室检查";
            string GroupCode = "G001";// e.ExtraParams["rID"]; 
            GroupName = GetGroupName(GroupCode);
            PanelR.Hidden = true;
            DetailPanel.Hidden = true;
            Panel19.Hidden = false;
            Panel19.Loader.SuspendScripting();
            Panel19.Loader.Url = "./Dialysis_04_01.aspx?GroupName=" + GroupName;
            Panel19.Loader.DisableCaching = true;
            Panel19.LoadContent();
        }
        #endregion

        #region 新增動作完成，在增加計算方式的結果(未使用)
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
        #endregion

        #region 計算方式 NewResult (未使用)
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
        #endregion

        #region 刪除檢驗(目前尚未使用)
        protected void cmdDelete(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];

            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            else
            {
                //XML will be represent as
                //<records>
                //   <record><Name>Alcoa Inc</Name><Price>29.01</Price><Change>0.42</Change><PctChange>1.47</PctChange></record>
                //        ...  
                //   <record>...</record>
                //</records>
                XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + json + "}}");
                string PatNo = _PAT_ID;
                string RESULT_DATE;
                string RESULT_CODE;
                string sSQL = "";
                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    PatNo = _PAT_ID;
                    RESULT_DATE = row.SelectSingleNode("RESULT_DATE").InnerXml;
                    RESULT_CODE = row.SelectSingleNode("RESULT_CODE").InnerXml;
                    sSQL += "DELETE FROM a_result_log ";
                    sSQL += "WHERE PAT_NO='" + PatNo + "' ";
                    sSQL += "AND RESULT_DATE='" + RESULT_DATE + "' ";
                    sSQL += "AND RESULT_CODE='" + RESULT_CODE + "'; ";
                }
                db.Excute(sSQL);
                this.ResourceManager1.AddScript("Ext.Msg.alert('Submitted', '资料已删除');");
                cmdREAD(sender, e);
            }
        }
        #endregion
    }
}