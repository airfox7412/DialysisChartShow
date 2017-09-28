using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using Ext.Net;

namespace Dialysis_Chart_Show.Systems
{
    public partial class Config : BaseForm
    {
        string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        
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
            root.Text = "系統設置";
            root.Expanded = true;
            nodes.Add(root);

            string[] GroupName = new string[] { "药品维护", "透析器型号", "血管通路维护", "管路类型", "管路型号", "权限设定维护", "病区/床号维护" };
            Icon[] icon_array = new Icon[] { Icon.Star, Icon.Star, Icon.Star, Icon.Star, Icon.Star, Icon.Star, Icon.Star };
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
            if (Session["USER_RIGHT"].ToString() == "AD" && Session["USER_ID"].ToString() == "admin")
            {
                Ext.Net.Node groupNode = new Ext.Net.Node()
                {
                    Text = " 授权管理",
                    NodeID = "8",
                    Icon = Icon.KeyStart,
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
            string[] url = new string[] { "Dialysis_System_01.aspx", "Dialysis_System_02.aspx", "Dialysis_System_03.aspx", "Dialysis_System_04.aspx", "Dialysis_System_08.aspx", "Dialysis_System_05.aspx", "Dialysis_System_06.aspx", "Dialysis_System_07.aspx" };
            string sID = e.ExtraParams["rID"];
            if (sID != "root")
            {
                loadAspxFile(url[Convert.ToInt16(sID) - 1]);
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