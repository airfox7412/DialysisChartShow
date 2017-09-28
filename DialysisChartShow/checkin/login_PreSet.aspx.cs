using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using Ext.Net;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

namespace Dialysis_Chart_Show.checkin
{
    public partial class login_PreSet : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        private string USER_ID = "";
        private string USER_TYPE = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string PAT_ID = Request.Form["PAT_ID"];
                if (Request.Form["USER_ID"] != "" && Request.Form["USER_ID"] != null)
                {
                    USER_ID = Request.Form["USER_ID"];
                    USER_TYPE = Request.Form["USER_TYPE"];
                }
                else
                {
                    X.Redirect("index.aspx");
                };

                Session["PAT_ID"] = PAT_ID;
                Session["PAT_NAME"] = GetPatName(PAT_ID);
                Session["USER_NAME"] = GetUserName(USER_ID);
                Session["USER_ID"] = USER_ID;
                Session["USER_TYPE"] = USER_TYPE;
                Session["USER_RIGHT"] = USER_TYPE;
                X.Redirect("Dialysis_PreSet.aspx");
            }
        }

        public String GetPatName(String Id)
        {
            String sql = "SELECT pif_name FROM pat_info WHERE pif_id='" + Id + "' OR pif_ic='" + Id +"' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["pif_name"].ToString();
            else
                return "";
        }

        public String GetUserName(String userId)
        {
            String sql = "SELECT * FROM access_level WHERE acclv_stfcode='" + userId + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["acclv_fname"].ToString();
            else
                return userId;
        }
    }
}