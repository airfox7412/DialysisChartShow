using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.Text;
using System.Net;

namespace Dialysis_Chart_Show
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Response.Redirect("login.aspx");
        }
    }
}