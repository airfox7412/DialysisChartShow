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

namespace Dialysis_Chart_Show.checkin
{
    public partial class Dialysis_Info : BaseForm
    {
        string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
            }
            SetMenu(MenuPanel1.Menu);
        }

        protected void SetMenu(Ext.Net.Menu menu)
        {
            var MenuItem1 = new Ext.Net.MenuItem();
            MenuItem1.ID = "MenuItem1";
            MenuItem1.Text = "净化过程明细";
            MenuItem1.Icon = Icon.Star;
            MenuItem1.Cls = "color_1";
            MenuItem1.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(reload_menu1);

            var MenuItem2 = new Ext.Net.MenuItem();
            MenuItem2.ID = "MenuItem2";
            MenuItem2.Text = "血液净化记录";
            MenuItem2.Icon = Icon.Star;
            MenuItem2.Cls = "color_10";
            MenuItem2.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(reload_menu2);

            var MenuItem3 = new Ext.Net.MenuItem();
            MenuItem3.ID = "MenuItem3";
            MenuItem3.Text = "血液净化小结";
            MenuItem3.Icon = Icon.Star;
            MenuItem3.Cls = "color_10";
            MenuItem3.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(reload_menu3);

            var MenuItem4 = new Ext.Net.MenuItem();
            MenuItem4.ID = "MenuItem4";
            MenuItem4.Text = "血液净化记录表";
            MenuItem4.Icon = Icon.Star;
            MenuItem4.Cls = "color_10";
            MenuItem4.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(reload_menu4);

            //var MenuItem5 = new Ext.Net.MenuItem();
            //MenuItem5.ID = "MenuItem5";
            //MenuItem5.Text = "实时监控一";
            //MenuItem5.Icon = Icon.Star;
            //MenuItem5.Cls = "color_10";
            //MenuItem5.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(reload_menu5);

            //var MenuItem6 = new Ext.Net.MenuItem();
            //MenuItem6.ID = "MenuItem6";
            //MenuItem6.Text = "实时监控二";
            //MenuItem6.Icon = Icon.Star;
            //MenuItem6.Cls = "color_10";
            //MenuItem6.DirectEvents.Click.Event += new ComponentDirectEvent.DirectEventHandler(reload_menu6);

            menu.Items.Add(MenuItem1);
            menu.Items.Add(MenuItem2);
            menu.Items.Add(MenuItem3);
            menu.Items.Add(MenuItem4);
            //menu.Items.Add(MenuItem5);
            //menu.Items.Add(MenuItem6);
        }

        protected void reload_menu1(object sender, EventArgs e)
        {
            string sReport;
            sReport = "./Dialysis_01.aspx";
            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = sReport;
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }

        protected void reload_menu2(object sender, EventArgs e)
        {
            string sReport;
            sReport = "./Dialysis_02.aspx";
            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = sReport;
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }

        protected void reload_menu3(object sander, EventArgs e)
        {
            string sReport;
            sReport = "./Dialysis_03.aspx";
            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = sReport;
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }

        protected void reload_menu4(object sander, EventArgs e)
        {
            string sReport;
            sReport = "./Dialysis_04.aspx";
            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = sReport;
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }

        protected void reload_menu5(object sander, EventArgs e)
        {
            string sReport;
            sReport = "./Dialysis_05.aspx";
            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = sReport;
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }

        protected void reload_menu6(object sander, EventArgs e)
        {
            string sReport;
            sReport = "./Dialysis_06.aspx";
            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = sReport;
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }
    }
}