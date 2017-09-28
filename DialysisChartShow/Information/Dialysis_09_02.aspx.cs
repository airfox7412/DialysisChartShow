using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_02 : BaseForm
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
            string sql;
            sql = "SELECT DISTINCT DATE_FORMAT(a.dialysis_date, '%Y-%m-%d') ";
            sql += "from data_list a where a.person_id = '" + _PAT_IC + "' ";
            sql += "ORDER BY dialysis_date DESC";
            DataTable dt = db.Query(sql);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
        }

        /// <summary>
        /// 血液淨化過程 --> 淨化過程明細
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void RowSelect(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Dialysis_date"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            string date = selRow[0]["Dialysis_date"].ToString();
            //string orrcpdt = selRow[0]["orrcpdt"].ToString();
            //string gender = selRow[0]["gender"].ToString();
            Panel1.Loader.SuspendScripting();
            Panel1.Loader.Url = "Dialysis_09_02_detail.aspx?date=" + date;
            Panel1.Loader.DisableCaching = true;
            Panel1.LoadContent();
        }
    }
}