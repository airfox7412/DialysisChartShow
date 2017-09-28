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
using System.Web.Hosting;
using System.IO;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_06_06_Alasamo_List : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                show_grid();
            }            
        }

        protected void show_grid()
        {
            string sql = "SELECT pat_id, info_date, dat_1, info_user FROM zinfo_f_06_alasamo ";
            sql += " WHERE pat_id = '" + _PAT_ID + "' ";
            sql += "ORDER BY info_date DESC";
            DataTable dt = db.Query(sql);

            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();

        }

        protected void RowSelect(object sender, DirectEventArgs e)
        {
            string sel_date = e.ExtraParams["info_date"].ToString();
            X.Redirect("Dialysis_06_06_Alasamo.aspx?sel_date=" + sel_date);
        }

        protected void Btn_Add_Click(object sender, DirectEventArgs e)
        {
            string sel_date = DateTime.Now.ToString("yyyy-MM-dd");
            X.Redirect("Dialysis_06_06_Alasamo.aspx?sel_date=" + sel_date);
        }
    }
}