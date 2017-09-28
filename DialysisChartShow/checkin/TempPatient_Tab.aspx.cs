using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.checkin
{
    public partial class TempPatient_Tab : BaseForm
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string url = "TempPatient_Tab.aspx";
                if (Session["USER_ID"] == null)
                    X.Redirect("login.aspx?target=" + url);
                //else
                    //GetTabPanel();
            }
        }


        protected void tab1_activate(object sender, DirectEventArgs e)
        {
            Panel1.Loader.SuspendScripting();
            Panel1.Loader.Url = "Patient_Info.aspx?kind=TempPatient";
            Panel1.Loader.DisableCaching = true;
            Panel1.LoadContent();
        }

        protected void tab2_activate(object sender, DirectEventArgs e)
        {
            Panel2.Loader.SuspendScripting();
            Panel2.Loader.Url = "TempPatient_Sch1.aspx";
            Panel2.Loader.DisableCaching = true;
            Panel2.LoadContent();
        }

        protected void GetTabPanel()
        {
            AddTab1();
            TabPanel1.SetPreviousTabAsActive();
        }

        protected void AddTab1()
        {
            Ext.Net.Panel panel1 = new Ext.Net.Panel
            {
                Title = "基本资料建档",
                Icon = Ext.Net.Icon.Add,
                Closable = false,
                Layout = "Fit",
                Loader = new ComponentLoader
                {
                    Mode = LoadMode.Frame,
                    Url = "TempPatient.aspx"
                }
            };
            TabPanel1.Add(panel1);

            Ext.Net.Panel panel2 = new Ext.Net.Panel
            {
                Title = "临时排班",
                Icon = Ext.Net.Icon.Add,
                Closable = false,
                Layout = "Fit",
                Loader = new ComponentLoader
                {
                    Mode = LoadMode.Frame,
                    Url = "TempPatient_Sch1.aspx"
                }
            };
            TabPanel1.Add(panel2);
        }
    }
}