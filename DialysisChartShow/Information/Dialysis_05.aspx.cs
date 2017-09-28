using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using System.Configuration;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_05 : BaseForm //System.Web.UI.Page
    {
        public string Hispital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

        private Ext.Net.Node root = new Ext.Net.Node();
        private Ext.Net.Node rA = new Ext.Net.Node();
        private Ext.Net.Node rB = new Ext.Net.Node();
        private Ext.Net.Node rC = new Ext.Net.Node();
        private Ext.Net.Node rD = new Ext.Net.Node();
        private Ext.Net.Node rE = new Ext.Net.Node();
        private Ext.Net.Node rZ = new Ext.Net.Node();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.txtNODE_ID.Hidden = true;
                this.txtNODE_TEXT.Hidden = true;
                this.TreePanel1.Hidden = true;

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
                btnCount.Text = "前3";
                //btnCount.Click += btnCount_Click;
                btnCount.AutoPostBack = true;

                Ext.Net.Button btnEdit = new Ext.Net.Button();
                btnEdit.Text = "修改";
                btnEdit.Icon = Icon.Pencil;
                btnEdit.Click += btnEdit_Click;
                btnEdit.AutoPostBack = true;
                
                Ext.Net.Button btnPrint = new Ext.Net.Button();
                btnPrint.Text = "打印";
                btnPrint.Icon = Icon.Printer; 
                btnPrint.Click += btnPrint_Click;
                btnPrint.AutoPostBack = true;

                Toolbar toolBar = new Toolbar();
                toolBar.ID = "Toolbar1";
                toolBar.Items.Add(btnExpand);
                toolBar.Items.Add(btnCollapse);
                //toolBar.Items.Add(btnCount);
                //toolBar.Items.Add(btnEdit);
                //toolBar.Items.Add(btnPrint);
                this.TreePanel1.TopBar.Add(toolBar);

                StatusBar statusBar1 = new StatusBar();
                statusBar1.ID = "StatusBar1";
                statusBar1.AutoClear = 1000;
                this.TreePanel1.BottomBar.Add(statusBar1);

                this.TreePanel1.Listeners.ItemClick.Handler = statusBar1.ClientID + ".setStatus({text: '点选: <b>' + record.data.text + '</b>', clear: false});";

                string sSQL;

                root.Text = "病程记录";
                //root.Icon = Icon.ReportUser;
                root.NodeID = "__";
                root.Cls = "large-font";
                
                
                //Ext.Net.Node rA = new Ext.Net.Node();
                rA.Text = "病程记录";
                rA.Icon = Icon.ReportUser;
                rA.NodeID = "A_";
                rA.Cls = "large-font";
                //AddChild(rA, "A", "G001", 3);
                Ext.Net.Node rA0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "A_A",
                    Icon = Icon.Add,
                    Cls = "large-font",
                    Leaf = true
                };
                rA.Children.Add(rA0);
                sSQL = "SELECT info_date FROM zinfo_e_01 WHERE pat_id=" + _PAT_ID + " ORDER BY info_date DESC ";
                DataTable dt1 = db.Query(sSQL);
                for (int i = 0; i < dt1.Rows.Count; i++)
                {
                    Ext.Net.Node rr = new Ext.Net.Node()
                    {
                        Text = dt1.Rows[i]["info_date"].ToString(),
                        NodeID = "A" + i.ToString(),
                        Icon = Icon.Page,
                        Cls = "large-font",
                        Leaf = true
                    };
                    rA.Children.Add(rr);
                }
                //if (rA.Children.Count == 0)
                //{
                //    Ext.Net.Node rA0 = new Ext.Net.Node()
                //    {
                //        Text = "添加",
                //        NodeID = "A_A",
                //        Icon = Icon.ControlBlank,
                //        Cls = "large-font",
                //        Leaf = true
                //    };
                //    rA.Children.Add(rA0);
                //}
                rA.Expanded = true;


                //Ext.Net.Node rB = new Ext.Net.Node();
                rB.Text = "手术记录";
                rB.Icon = Icon.ReportUser;
                rB.NodeID = "B_";
                rB.Cls = "large-font";
                //AddChild(rB, "B", "G003", 3);
                Ext.Net.Node rB0 = new Ext.Net.Node()
                {
                    Text = "添加",
                    NodeID = "B_B",
                    Icon = Icon.Add,
                    Cls = "large-font",
                    Leaf = true
                };
                rB.Children.Add(rB0);
                sSQL = "SELECT info_date FROM zinfo_e_02 WHERE pat_id=" + _PAT_ID + " ORDER BY info_date DESC ";
                DataTable dt2 = db.Query(sSQL);
                for (int i = 0; i < dt2.Rows.Count; i++)
                {
                    Ext.Net.Node rr = new Ext.Net.Node()
                    {
                        Text = dt2.Rows[i]["info_date"].ToString(),
                        NodeID = "B" + i.ToString(),
                        Icon = Icon.Page,
                        Cls = "large-font",
                        Leaf = true
                    };
                    rB.Children.Add(rr);
                }
                //if (rB.Children.Count == 0)
                //{
                //    Ext.Net.Node rB0 = new Ext.Net.Node()
                //    {
                //        Text = "添加",
                //        NodeID = "B_B",
                //        Icon = Icon.ControlBlank,
                //        Cls = "large-font",
                //        Leaf = true
                //    };
                //    rB.Children.Add(rB0);
                //}
                rB.Expanded = true;
                //Ext.Net.Node rC = new Ext.Net.Node();
                rC.Text = "";
                rC.Icon = Icon.ReportUser;
                rC.NodeID = "C_";
                rC.Cls = "large-font";
                //AddChild(rC, "C", "G002", 3);
                rC.Expanded = true;
                //
                rD.Text = "";
                rD.Icon = Icon.ReportUser;
                rD.NodeID = "D_";
                rD.Cls = "large-font";
                //AddChild(rD, "D", "G004", 3);
                rD.Expanded = true;
                //
                rZ.Text = "";
                rZ.Icon = Icon.ReportUser;
                rZ.NodeID = "Z_";
                rZ.Cls = "large-font";
                //AddChild(rZ, "Z", "", 3);
                rZ.Expanded = true;
                //                
                

                root.Children.Add(rA);
                root.Children.Add(rB);
                //root.Children.Add(rC);
                //root.Children.Add(rD);
                //root.Children.Add(rZ);

                root.Expanded = true;
                this.TreePanel1.Root.Add(root);
                //this.LastData();
            }
        }

        protected void reload_page1(object sender, EventArgs e)
        {
            string url = "./Dialysis_05_01.aspx";
            if (Hispital == "Hospital_Henan" || Hispital == "Hospital_Hebei" || Hispital == "Hospital_Suzhou")
                url = "./Dialysis_05_03.aspx";
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = url;
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page2(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_05_02.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page3(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_05_04.aspx";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void RemoteEdit(object sender, RemoteEditEventArgs e)
        {
            e.Accept = true;

            foreach (RowChanges change in e.Changes)
            {
                if (change.Field == "text" && change.IsDirty<string>())
                {
                    change.SetValue(change.Value<string>() + "_echo");
                }
                else if (change.Field == "number")
                {
                    change.SetValue(change.Value<int>() * 10);
                }
                else if (change.Field == "date")
                {
                    change.SetValue(change.Value<DateTime>().AddYears(10));
                }
            }

            //   You can refuse action
            //   e.Accept = false;
            //   e.RefusalMessage = "Error";
        }

        
        protected void RemoteAppend(object sender, RemoteAppendEventArgs e)
        {
            e.Accept = true;
            e.Attributes = new
            {
                text = e.Text + "_new"
            };
        }

        protected void RemoteMove(object sender, RemoteMoveEventArgs e)
        {
            e.Accept = true;
        }

        protected void Node_Prn(object sender, DirectEventArgs e)
        {
            string sID = e.ExtraParams["rID"];
            string sTEXT = e.ExtraParams["rTEXT"];
            string sPAT_NO = _PAT_ID;
            //string sDATE = "";
        }
        
        protected void Node_Edit(object sender, DirectEventArgs e)
        {
            string sID = e.ExtraParams["rID"];
            string sTEXT = e.ExtraParams["rTEXT"];
            string sPAT_NO = _PAT_ID;
            //string sDATE = "";
        }
        
        protected void Node_Add(object sender, DirectEventArgs e)
        {
            string sID = e.ExtraParams["rID"];
            string sTEXT = e.ExtraParams["rTEXT"];
            string sPAT_NO = _PAT_ID;
            //string sDATE = "";
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Ext.Net.Button bb = (Ext.Net.Button)sender;
            string sID = this.txtNODE_ID.Text;
            string sTEXT = this.txtNODE_TEXT.Text;
            if (sID.Substring(1, 1) != "_")
            {
                //修改
                switch (sID.Substring(0, 1))
                {
                    case "A":
                        this.Panel1.Loader.SuspendScripting();
                        this.Panel1.Loader.Url = "./Dialysis_05_01.aspx?editmode=edit&editmode2=edit&sel_info_date=" + sTEXT;
                        this.Panel1.Loader.DisableCaching = true;
                        this.Panel1.LoadContent();
                        break;
                    case "B":
                        this.Panel1.Loader.SuspendScripting();
                        this.Panel1.Loader.Url = "./Dialysis_05_02.aspx?editmode=edit&editmode2=edit&sel_info_date=" + sTEXT;
                        this.Panel1.Loader.DisableCaching = true;
                        this.Panel1.LoadContent();
                        break;
                }
            }
        }

        protected void btnPrint_Click(object sender, EventArgs e)
        {
            Ext.Net.Button bb = (Ext.Net.Button)sender;
            string sID = this.txtNODE_ID.Text;
            string sTEXT = this.txtNODE_TEXT.Text;
            if (sID.Substring(1, 1) != "_")
            {
                //打印
                switch (sID.Substring(0, 1))
                {
                    case "A":
                        this.Panel1.Loader.SuspendScripting();
                        this.Panel1.Loader.Url = "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + sTEXT + "&_REPORT_NAME=6";
                        this.Panel1.Loader.DisableCaching = true;
                        this.Panel1.LoadContent();
                        break;
                    case "B":
                        //this.Panel1.Loader.SuspendScripting();
                        //this.Panel1.Loader.Url = "./Dialysis_05_02.aspx?editmode=show&sel_info_date=" + sTEXT;
                        //this.Panel1.Loader.DisableCaching = true;
                        //this.Panel1.LoadContent();
                        break;
                }
            }
        }

        protected void Node_Click(object sender, DirectEventArgs e)
        {
            string sID = e.ExtraParams["rID"];
            string sTEXT = e.ExtraParams["rTEXT"];
            string sPAT_NO = _PAT_ID;
            
            this.txtNODE_ID.Text = sID;
            this.txtNODE_TEXT.Text = sTEXT;
 
            if (sID.Substring(1, 1) != "_")
            {
                //瀏覽
                switch (sID.Substring(0, 1))
                {
                    case "A":
                        this.Panel1.Loader.SuspendScripting();
                        this.Panel1.Loader.Url = "./Dialysis_05_01.aspx?editmode=show&sel_info_date=" + sTEXT;
                        this.Panel1.Loader.DisableCaching = true;
                        this.Panel1.LoadContent();
                        break;
                    case "B":
                        this.Panel1.Loader.SuspendScripting();
                        this.Panel1.Loader.Url = "./Dialysis_05_02.aspx?editmode=show&sel_info_date=" + sTEXT;
                        this.Panel1.Loader.DisableCaching = true;
                        this.Panel1.LoadContent();
                        break;
                }
            }
            else 
            {
                if (sTEXT == "添加")
                {
                    //新增
                    switch (sID.Substring(0, 1))
                    {
                        case "A":
                            this.Panel1.Loader.SuspendScripting();
                            this.Panel1.Loader.Url = "./Dialysis_05_01.aspx?editmode=edit&editmode2=add";
                            this.Panel1.Loader.DisableCaching = true;
                            this.Panel1.LoadContent();
                            break;
                        case "B":
                            this.Panel1.Loader.SuspendScripting();
                            this.Panel1.Loader.Url = "./Dialysis_05_02.aspx?editmode=edit&editmode2=add";
                            this.Panel1.Loader.DisableCaching = true;
                            this.Panel1.LoadContent();
                            break;
                    }
                }
            }
        }
    }
}