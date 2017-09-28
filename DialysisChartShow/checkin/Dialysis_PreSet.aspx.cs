﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.checkin
{
    public partial class Dialysis_PreSet : BaseForm //System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string url = "Dialysis_PreSetView.aspx";
                if (Session["USER_ID"] == null)
                    X.Redirect("login.aspx?target=" + url);
                else
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
            root.Text = "處方模板";
            root.Expanded = true;
            nodes.Add(root);

            string[] GroupName = new string[2] { "處方模板","醫囑用藥" };
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
            string[] LinkArray = new string[2] { "Dialysis_PreSetView.aspx", "Patient_Drug.aspx" };
            if (sID != "root")
            {
                    loadAspxFile("./" + LinkArray[IntsID] );
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