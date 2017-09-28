using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using Ext.Net;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show.checkin
{
    public partial class SchMenu : System.Web.UI.Page 
    {
        DBMysql db = new DBMysql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string sql = "SELECT genst_desc FROM general_setup WHERE genst_ctg='IPConnect' AND genst_code='00001'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    ip_url.Text = "http://" + dt.Rows[0]["genst_desc"].ToString();
                }
                BuildTree1(TreePanel1.Root);
            }
        }

        #region 取得樹狀目錄1
        private void BuildTree1(Ext.Net.NodeCollection nodes)
        {
            if (nodes == null)
            {
                nodes = new Ext.Net.NodeCollection();
            }
            Ext.Net.Node root = new Ext.Net.Node();
            root.Text = "排班选单";
            root.Expanded = true;
            nodes.Add(root);

            string[] GroupName = { "本周排班", "双周预约排班", "双周预约排班打印", "固定排班", "排班查询" };
            Icon[] icon_array = { Icon.User, Icon.User, Icon.Report, Icon.Table, Icon.User };
            for (int i = 0; i < GroupName.Length; i++)
            {
                Ext.Net.Node groupNode = new Ext.Net.Node()
                {
                    Text = GroupName[i],
                    NodeID = (i+1).ToString(),
                    Icon = icon_array[i],
                    Cls = "large-font",
                    Leaf = true
                };
                root.Children.Add(groupNode);
            }
            TreePanel1.Render();
        }
        #endregion

        #region 點選TreePanel上的Node
        protected void Node_Click(object sender, DirectEventArgs e)
        {
            string sID = e.ExtraParams["rID"];
            int intsID = int.Parse(sID) - 1;
            string[] Link = { "SchTable.aspx", "/Schedule/index.html?locale=zh_CN", "DialysisTwoWeekList.aspx", "/Schedule/static.html?locale=zh_CN", "SearchPSch.aspx" };

            if (sID != "root")
            {
                if (intsID==1)
                {
                    PanelT.Collapse();
                    loadAspxFile(ip_url.Text + Link[intsID]);
                }
                else if (intsID==3)
                {
                    PanelT.Collapse();
                    loadAspxFile(ip_url.Text + Link[intsID]);
                }
                loadAspxFile(Link[intsID]);
            }
        }
        #endregion    
    
        private void loadAspxFile(String url)
        {
            this.PanelR.Loader.SuspendScripting();
            this.PanelR.Loader.Url = url;
            this.PanelR.Loader.DisableCaching = true;
            this.PanelR.LoadContent();
        }
    }
}