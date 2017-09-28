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
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Dialysis_Chart_Show.checkin
{
    public partial class Patient_Drug : BaseForm
    {
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (Session["USER_NAME"] == null)
                {
                    X.Redirect("../login.aspx");
                }
                Patient_ID.Text = _PAT_IC;
            }
        }
        protected void tab1_activate(object sender, DirectEventArgs e)
        {
            string url = "./Dialysis_PreSetLong.aspx?pid=" + Patient_ID.Text;
            Panel_Long.Loader.SuspendScripting();
            Panel_Long.Loader.Url = url;
            Panel_Long.Loader.DisableCaching = true;
            Panel_Long.LoadContent();
        }

        protected void tab2_activate(object sender, DirectEventArgs e)
        {
            string url = "./Dialysis_PreSetShort.aspx?pid=" + Patient_ID.Text;
            Panel_Short.Loader.SuspendScripting();
            Panel_Short.Loader.Url = url;
            Panel_Short.Loader.DisableCaching = true;
            Panel_Short.LoadContent();
        }
    }
}