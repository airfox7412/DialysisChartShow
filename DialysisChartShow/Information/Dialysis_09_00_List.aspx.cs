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

namespace Dialysis_Chart_Show.Information {

    public partial class Dialysis_09_00_List : BaseForm
    {
        public string sDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                txtBegin_DATE.Text = sDay;
                txtEnd_DATE.Text = sDay;
                show_grid();
            }

        }

        protected void BtnQuery_Click(object sender, DirectEventArgs e)
        {
            show_grid();
        }

        protected void show_grid()
        {
            string SQL = "SELECT * FROM shift ";
            SQL += "WHERE sdate IN ('" + _Get_YMD2(txtBegin_DATE.Text) + "', '" + _Get_YMD2(txtEnd_DATE.Text) + "') ";
            SQL += "ORDER BY sdate DESC";
            DataTable dt = db.Query(SQL);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void RowSelect(object sender, DirectEventArgs e) 
        {
            string sid = e.ExtraParams["Values"];
            X.Redirect("Dialysis_09_00_Edit.aspx?sid=" + sid);
        }

        protected void BtnAdd_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Dialysis_09_00_Add.aspx");
        }

        protected void BtnDel_Click(object sender, DirectEventArgs e)
        {
        }
    }
}