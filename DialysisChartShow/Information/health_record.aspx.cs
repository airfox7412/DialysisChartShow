using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dialysis_Chart_Show.Information
{
    public partial class health_record : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = "./health_record01.aspx";
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }

        protected void reload_btn1(object sender, EventArgs e)
        {

            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = "./health_record01.aspx";
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }

        protected void reload_btn2(object sender, EventArgs e)
        {

            this.Panel2.Loader.SuspendScripting();
            this.Panel2.Loader.Url = "./health_record02.aspx";
            this.Panel2.Loader.DisableCaching = true;
            this.Panel2.LoadContent();
        }
    }
}