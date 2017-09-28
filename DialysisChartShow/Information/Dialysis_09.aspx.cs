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

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09 : BaseForm
    {
        string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                BuildTree1(TreePanel1.Root);
                BuildTree2(TreePanel2.Root);
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
            root.Text = "血液透析";
            root.Expanded = true;
            nodes.Add(root);

            string[] GroupName = new string[] { "交班记录表", "血液净化记录", "净化过程明细", "血液净化记录表", "拟用药使用统计表", "血液透析患者评估表", "拟用药使用统计表" };
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
                
                if (i <= 3)
                    root.Children.Add(groupNode);
                if (Hospital == "Hospital_Suzhou" && (i == 4 || i == 5)) //蘇州醫院專用
                    root.Children.Add(groupNode);
                else if (Hospital == "Hospital_Xian" && i == 6) //武警醫院專用
                    root.Children.Add(groupNode);
            }
            TreePanel1.Render();
        }
        #endregion

        #region 取得樹狀目錄2
        private void BuildTree2(Ext.Net.NodeCollection nodes)
        {
            if (nodes == null)
            {
                nodes = new Ext.Net.NodeCollection();
            }
            Ext.Net.Node root = new Ext.Net.Node();
            root.Text = "血液透析";
            root.Expanded = true;
            nodes.Add(root);

            string[] GroupName = new string[] { "植管记录", "回诊记录", "腹膜炎记录", "感染记录及追踪", "护理评估", "随访记录" };
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
            TreePanel2.Render();
        }
        #endregion

        #region 點選TreePanel1上的Node
        protected void Node1_Click(object sender, DirectEventArgs e)
        {
            string[] url = new string[] { "Dialysis_09_00_List.aspx", "Dialysis_09_01_All.aspx", "Dialysis_09_02.aspx", "Dialysis_09_03.aspx", "Dialysis_09_04.aspx", "Dialysis_0h_08_List.aspx", "Dialysis_09_04_Xian.aspx" };
            string sID = e.ExtraParams["rID"];
            if (sID != "root")
            {
                loadAspxFile(url[Convert.ToInt16(sID) - 1]);
            }
        }
        #endregion

        #region 點選TreePanel2上的Node
        protected void Node2_Click(object sender, DirectEventArgs e)
        {
            string[] url = new string[] { "Dialysis_09_21.aspx", "Dialysis_09_22.aspx", "Dialysis_09_23.aspx", "Dialysis_09_24.aspx", "Dialysis_09_25.aspx", "Dialysis_09_26.aspx" };
            string sID = e.ExtraParams["rID"];
            if (sID != "root")
            {
                loadAspxFile(url[Convert.ToInt16(sID) - 1]+"?editmode=list");
            }
        }
        #endregion

        private void loadAspxFile(String url)
        {
            Panel2.Loader.SuspendScripting();
            Panel2.Loader.Url = url;
            Panel2.Loader.DisableCaching = true;
            Panel2.LoadContent();
        }
    }
}