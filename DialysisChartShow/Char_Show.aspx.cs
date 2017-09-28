using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;


namespace Dialysis_Chart_Show
{
    public partial class Char_Show : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            Session["PERSON_ID"] = TextBox1.Text;
            Session["DATE"] = TextBox2.Text;
            Session["REPORT"] = "1";
            Response.Redirect("report/Rpt_View.aspx");
            
           
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            Session["PERSON_ID"] = TextBox1.Text;
            Session["DATE"] = TextBox2.Text;
            Session["REPORT"] = "2";
            Response.Redirect("report/Rpt_View.aspx");
        }
    }
}