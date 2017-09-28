using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_07 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }
        
        protected void reload_page2(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_07_01/Default.aspx";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }
        protected void reload_page3(object sender, EventArgs e)
        {

            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "./Dialysis_07_02.aspx";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }
    }
}