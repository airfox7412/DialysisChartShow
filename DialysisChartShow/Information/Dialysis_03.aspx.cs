using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_03 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        protected void reload_page1(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_03_01.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page2(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_03_02.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page3(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_03_03.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page4(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_03_04.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page5(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_03_05.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page6(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_03_06.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void reload_page7(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_03_07.aspx?editmode=list";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }
    }
}