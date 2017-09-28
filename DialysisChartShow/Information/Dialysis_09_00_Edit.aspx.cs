using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Collections;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_00_Edit : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                sid.Text = Request.QueryString["sid"];
                Show();
            }
        }

        protected void Show()
        {
            string sSQL = "SELECT * FROM shift ";
            sSQL += "WHERE sid=" + sid.Text;
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                txt_sdate.Text = dt.Rows[0]["sdate"].ToString();
                info_subject.Text = dt.Rows[0]["subject"].ToString();
                info_remark.Text = dt.Rows[0]["remark"].ToString();
                info_user.Text = dt.Rows[0]["user"].ToString();
            }
            dt.Dispose();
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            string sSQL = "UPDATE shift SET ";
            sSQL += "subject='" + info_subject.Text + "', ";
            //sSQL += "user='" + info_user.Text + "', ";
            sSQL += "remark='" + info_remark.Text + "' ";
            sSQL += "WHERE sid=" + sid.Text;
            db.Excute(sSQL);
            X.Redirect("Dialysis_09_00_List.aspx");
        }

        protected void Btn_Print_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + sid.Text + "&_REPORT_NAME=99");
        }

        protected void Btn_Close_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Dialysis_09_00_List.aspx");
        }
    }
}