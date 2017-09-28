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

namespace Dialysis_Chart_Show.Stock
{
    public partial class Dialysis_Stock : System.Web.UI.Page 
    {
        DBMysql db = new DBMysql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
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
            root.Text = "库存管理";
            root.Expanded = true;
            nodes.Add(root);

            string[] GroupName = { "库存表", "每日病患材料单", "领料单", "退料单", "耗材使用统计", "进货明细" };
            Icon[] icon_array = { Icon.Book, Icon.Report, Icon.Report, Icon.Report, Icon.Report, Icon.Report };
            for (int i = 0; i < GroupName.Length; i++)
            {
                Ext.Net.Node groupNode = new Ext.Net.Node()
                {
                    Text = GroupName[i],
                    NodeID = (i + 1).ToString(),
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
            if (sID != "root")
            {
                loadAspxFile("./Dialysis_Stock_0" + sID + ".aspx");
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