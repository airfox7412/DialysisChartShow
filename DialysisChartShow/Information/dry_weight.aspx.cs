using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;

namespace Dialysis_Chart_Show.Information
{
    public partial class dry_weight : BaseForm
    {
        private Ext.Net.Node root = new Ext.Net.Node();

        private void AddChild(Ext.Net.Node nn, string ss, string st)
        {
            nn.Text = st;
            nn.Icon = Icon.ReportUser;
            nn.NodeID = ss + "_";
            nn.Cls = "large-font";
            string sSQL = "";
            sSQL = "SELECT SUBSTRING(cln1_diadate,1,7) AS M, count(*) AS CNT " +
                     "FROM clinical1_nurse " +
                    "WHERE cln1_patic='" + _PAT_IC + "' " +
                    "GROUP BY SUBSTRING(cln1_diadate,1,7) " +
                    "ORDER BY SUBSTRING(cln1_diadate,1,7) ";
            DataTable dtM = db.Query(sSQL);
            System.Data.DataView dvM = dtM.DefaultView;

            //for (int i = 0; i < dtM.Rows.Count; i++)
            //{
            //    Ext.Net.Node rr = new Ext.Net.Node()
            //    {
            //        Text = dtM.Rows[i]["M"].ToString() + "(" + dtM.Rows[i]["CNT"].ToString() + ")",
            //        NodeID = ss + i.ToString(),
            //        Icon = Icon.Page,
            //        Cls = "blue-large-font",
            //        Leaf = true
            //    };
            //    nn.Children.Add(rr);
            //}
            sSQL = "SELECT SUBSTRING(cln1_diadate,1,4) AS Y, count(*) AS CNT " +
                     "FROM clinical1_nurse " +
                    "WHERE cln1_patic='" + _PAT_IC + "' " +
                    "GROUP BY SUBSTRING(cln1_diadate,1,4) " +
                    "ORDER BY SUBSTRING(cln1_diadate,1,4) ";
            DataTable dtY = db.Query(sSQL);
            for (int i = 0; i < dtY.Rows.Count; i++)
            {
                Ext.Net.Node rY = new Ext.Net.Node()
                {
                    Text = dtY.Rows[i]["Y"].ToString() + "(" + dtY.Rows[i]["CNT"].ToString() + ")",
                    NodeID = ss + (i).ToString(),
                    Icon = Icon.Page,
                    Cls = "blue-large-font",
                    Leaf = false
                };
                nn.Children.Add(rY);

                dvM.RowFilter = "M LIKE '" + dtY.Rows[i]["Y"].ToString() + "%' ";
                for (int j = 0; j < dvM.Count; j++)
                {
                    Ext.Net.Node rM = new Ext.Net.Node()
                    {
                        Text = dvM[j]["M"].ToString() + "(" + dvM[j]["CNT"].ToString() + ")",
                        NodeID = ss + (i).ToString() + "_" + (j).ToString(),
                        Icon = Icon.Page,
                        Cls = "blue-large-font",
                        Leaf = true
                    };
                    rY.Children.Add(rM);
                }
                //rY.Expanded = true;
            }
            if (dtY.Rows.Count == 0)
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
            nn.Expanded = true;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            //string gtoday = DateTime.Now.ToString("yyyy-MM-dd");
            //txt_year.Text = gtoday.Substring(0, 4);


            if (!X.IsAjaxRequest)
            {
                //txt_year.Text = Request["gyr"].ToString();

                //if (Request["gmth"].ToString() == "01")
                //{
                //    cmb_month.Value = "01";
                //}
                //if (Request["gmth"].ToString() == "02")
                //{
                //    cmb_month.Value = "02";
                //}
                //if (Request["gmth"].ToString() == "03")
                //{
                //    cmb_month.Value = "03";
                //}
                //if (Request["gmth"].ToString() == "04")
                //{
                //    cmb_month.Value = "04";
                //}
                //if (Request["gmth"].ToString() == "05")
                //{
                //    cmb_month.Value = "05";
                //}
                //if (Request["gmth"].ToString() == "06")
                //{
                //    cmb_month.Value = "06";
                //}
                //if (Request["gmth"].ToString() == "07")
                //{
                //    cmb_month.Value = "07";
                //}


                //this.TreePanel1.Width = Unit.Pixel(200);
                //this.TreePanel1.Height = Unit.Pixel(270);
                //this.TreePanel1.AutoScroll = true;

                Ext.Net.Button btnExpand = new Ext.Net.Button();
                btnExpand.Text = "展开";
                btnExpand.Listeners.Click.Handler = this.TreePanel1.ClientID + ".expandAll();";

                Ext.Net.Button btnCollapse = new Ext.Net.Button();
                btnCollapse.Text = "收合";
                btnCollapse.Listeners.Click.Handler = this.TreePanel1.ClientID + ".collapseAll();";

                Toolbar toolBar = new Toolbar();
                toolBar.ID = "Toolbar1";
                toolBar.Items.Add(btnExpand);
                toolBar.Items.Add(btnCollapse);
                this.TreePanel1.TopBar.Add(toolBar);

                //StatusBar statusBar1 = new StatusBar();
                //statusBar1.ID = "StatusBar1";
                //statusBar1.AutoClear = 1000;
                //this.TreePanel1.BottomBar.Add(statusBar1);

                //this.TreePanel1.Listeners.ItemClick.Handler = statusBar1.ClientID + ".setStatus({text: '点选: <b>' + record.data.text + '</b>', clear: false});";
                root.Text = "干体重";
                //root.Icon = Icon.ReportUser;
                //root.NodeID = "__";
                //root.Cls = "large-font";
                AddChild(root, "A", "干体重");

                root.Expanded = true;
                this.TreePanel1.Root.Add(root);

                //this.Panel2.Loader.SuspendScripting();
                //this.Panel2.Loader.Url = "./dry_weightlist.aspx?wgyr=" + Request["gyr"].ToString() + "&wgmth=" + Request["gmth"].ToString();
                //this.Panel2.Loader.DisableCaching = true;
                //this.Panel2.LoadContent();

            }
        }

        //protected void display_weightlist(object sender, DirectEventArgs e)
        //{
        //    string selmth = cmb_month.SelectedItem.Value;
        //    string selyr = txt_year.Text;

        //    if (selyr == "")
        //    {

        //        BaseForm bs = new BaseForm();
        //        bs._ErrorMsgShow("年的标准选择不能为空!");
        //        return;
        //    }
        //    else 
        //    {
        //        //this.Panel2.Loader.SuspendScripting();
        //        //this.Panel2.Loader.Url = "./dry_weightlist.aspx?wgyr=" + selyr + "&wgmth=" + selmth;
        //        //this.Panel2.Loader.DisableCaching = true;
        //        //this.Panel2.LoadContent();
                
        //        string sBEG_DATE;
        //        string sEND_DATE;

        //        if (selmth == "99")
        //        {
        //            sBEG_DATE = selyr + "-01-01";
        //            sEND_DATE = selyr + "-12-31";
        //        }
        //        else
        //        {
        //            sBEG_DATE = selyr + "-" + selmth + "-01";
        //            DateTime DD = Convert.ToDateTime(sBEG_DATE);
        //            sEND_DATE = DD.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd"); 
        //        }
        //        this.Panel3.Loader.SuspendScripting();
        //        this.Panel3.Loader.Url = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=11&_PAT_ID=" + _PAT_IC + "&_BEG_DATE=" + sBEG_DATE + "&_END_DATE=" + sEND_DATE;
        //        this.Panel3.Loader.DisableCaching = true;
        //        this.Panel3.LoadContent();
        //    }
        //}

        protected void Node_Click(object sender, DirectEventArgs e)
        {
            string sID = "";
            string sTEXT = "";
            string selmth = "";
            string selyr = "";

            sID = e.ExtraParams["rID"];
            sTEXT = e.ExtraParams["rTEXT"];
            string[] t = sTEXT.Split(new char[] { '(' });
            string[] u = t[0].Split(new char[] { '-' });

            //this.Panel2.Loader.SuspendScripting();
            if (u.Length < 2)
                selmth = "99";
            else
                selmth = u[1];

            selyr = u[0];
            //this.Panel2.Loader.Url = "./dry_weightlist.aspx?wgyr=" + selyr + "&wgmth=" + selmth;
            //this.Panel2.Loader.DisableCaching = true;
            //this.Panel2.LoadContent();

            string sBEG_DATE;
            string sEND_DATE;

            
            if (selmth == "99")
            {
                sBEG_DATE = selyr + "-01-01";
                sEND_DATE = selyr + "-12-31";
            }
            else
            {
                sBEG_DATE = selyr + "-" + selmth + "-01";
                DateTime DD = Convert.ToDateTime(sBEG_DATE);
                sEND_DATE = DD.AddMonths(1).AddDays(-1).ToString("yyyy-MM-dd");
            }
            this.Panel3.Loader.SuspendScripting();
            this.Panel3.Loader.Url = "dry_weight_Chart.aspx?BEG_DATE=" + sBEG_DATE + "&END_DATE=" + sEND_DATE;
            //this.Panel3.Loader.Url = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=11&_PAT_ID=" + _PAT_IC + "&_BEG_DATE=" + sBEG_DATE + "&_END_DATE=" + sEND_DATE;
            this.Panel3.Loader.DisableCaching = true;
            this.Panel3.LoadContent();
        }
    
    }
}