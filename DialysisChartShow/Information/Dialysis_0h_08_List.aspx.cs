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

    public partial class Dialysis_0h_08_List : BaseForm
    {
        public string toDay=DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                show_grid();
            }

        }

        protected void show_grid()
        {
            string SQL = "SELECT pat_id, info_date, info_user, txt_leader, txt_nurse FROM zinfo_h_08 ";
            SQL += "WHERE pat_id='" + _PAT_ID + "' ";
            SQL += "ORDER BY info_date DESC";
            DataTable dt = db.Query(SQL);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void RowSelect(object sender, DirectEventArgs e) 
        {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);
            string pat_id = selRow[0]["pat_id"].ToString();
            string info_date = selRow[0]["info_date"].ToString();
            X.Redirect("Dialysis_0h_08_Edit.aspx?info_date=" + info_date);
        }

        protected void BtnAdd_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Dialysis_0h_08.aspx");
        }

        protected void BtnDel_Click(object sender, DirectEventArgs e)
        {
        }
    }
}