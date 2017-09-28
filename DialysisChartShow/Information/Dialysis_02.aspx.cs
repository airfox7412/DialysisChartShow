using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_02 : BaseForm //System.Web.UI.Page
    {

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
            root.Text = "血透信息";
            root.Expanded = true;
            nodes.Add(root);

            string[] GroupName = new string[7] { "血管通路", "透析处方", "血压测量", "透析充分性", "抗凝剂", "干体重", "合用其他透析模式" };
            for (int i = 0; i < GroupName.Length; i++)
            {
                Ext.Net.Node groupNode = new Ext.Net.Node()
                {
                    Text = GroupName[i],
                    NodeID = (i + 1).ToString(),
                    Icon = Icon.Star,
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
            int IntsID = Convert.ToInt16(sID) - 1;
            string[] LinkArray = new string[7] { "Dialysis_02_01.aspx", "Dialysis_02_02.aspx", "Dialysis_02_031.aspx", "Dialysis_02_041.aspx", "Dialysis_02_05.aspx", "Dialysis_02_06.aspx", "Dialysis_02_07.aspx" };
            if (sID != "root")
            {
                if (IntsID != 5)
                {
                    loadAspxFile("./" + LinkArray[IntsID] + "?editmode=list");
                }
                else
                {
                    string gtoday = DateTime.Now.ToString("yyyy-MM-dd");
                    string gyear = gtoday.Substring(0, 4);
                    string gmonth = gtoday.Substring(5, 2);
                    loadAspxFile("./dry_weight.aspx?gyr=" + gyear + "&gmth=" + gmonth);
                }
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