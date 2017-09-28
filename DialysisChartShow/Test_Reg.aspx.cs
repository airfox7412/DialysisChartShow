using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Ext.Net;
using Microsoft.Win32;

namespace Dialysis_Chart_Show
{
    public partial class Test_Reg : System.Web.UI.Page
    {

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!X.IsAjaxRequest)
            {
                string str1="";
                RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"Software\Datacom\product\DCS42");
                string[] n=regkey.GetValueNames();
                for(int i=0;i<n.Length;i++)
                {
                    str1 += n[i] + ": " + regkey.GetValue(n[i]) + "\r\n\r\n";
                }
                txtReg.Text = str1;
            }
        }
    }
}