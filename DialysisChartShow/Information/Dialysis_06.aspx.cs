using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using System.Data;
using Ext.Net;
using System.Configuration;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_06 : BaseForm
    {
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

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
            root.Text = "基本资料表";
            root.Expanded = true;
            nodes.Add(root);

            if (Hospital == "Hospital_Alasamo")
            {
                string[] GroupName = { "基本资料", "病史", "体格检查", "实验室检查", "诊断", "打印", "用药纪录", "季度小结" };
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
            }
            else
            {
                string[] GroupName = { "基本资料", "病史", "体格检查", "实验室检查", "诊断", "打印", "用药纪录" };
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
            }

            TreePanel1.Render();
        }
        #endregion

        #region 點選TreePanel上的Node
        protected void Node_Click(object sender, DirectEventArgs e)
        {
            string sID = e.ExtraParams["rID"];
            int IntsID = Convert.ToInt16(sID) - 1;
            string url = "";

            if (sID != "root")
            {
                if (Hospital == "Hospital_Alasamo")
                {
                    string[] LinkArray = { "../checkin/Patient_Info.aspx", "Dialysis_06_012_Alasamo.aspx", "Dialysis_06_02_Alasamo.aspx", "Dialysis_06_04_Alasamo.aspx", "Dialysis_06_05_Alasamo.aspx", "../report/Report_Dialysis_h.aspx", "../checkin/Patient_Drug.aspx", "Dialysis_06_06_Alasamo_List.aspx" };
                    url = LinkArray[IntsID];
                    if (sID == "6")
                    {
                        url += "?_PAT_ID=" + _PAT_ID + "&_REPORT_NAME=131";
                    }
                }
                else
                {
                    string[] LinkArray = { "../checkin/Patient_Info.aspx", "Dialysis_06_012.aspx", "Dialysis_06_02.aspx", "Dialysis_06_04.aspx", "Dialysis_06_05.aspx", "../report/Report_Dialysis_h.aspx", "../checkin/Patient_Drug.aspx" };
                    url = LinkArray[IntsID];
                    if (sID == "6")
                    {
                        url += "?_PAT_ID=" + _PAT_IC + "&_REPORT_NAME=13";
                    }
                }
                loadAspxFile(url);
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