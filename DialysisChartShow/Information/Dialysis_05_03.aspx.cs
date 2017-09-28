using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

namespace Dialysis_Chart_Show.Information {
    public partial class Dialysis_05_03 : BaseForm {
        private static string SQL_SELECT_BY_PATIENT = @"
            SELECT pif_ic, cln1_diadate, cln1_col1, cln1_col3, cln1_col6, cln1_col7
            FROM pat_info as P left join clinical1_nurse as C on p.pif_ic = C.cln1_patic
            WHERE P.pif_id = '{0}'
            ORDER BY cln1_diadate DESC;";

        protected void Page_Load(object sender, EventArgs e) {
            if (!X.IsAjaxRequest) {
                show_grid();
            }

        }

        protected void show_grid() {
            DBMysql db = new DBMysql();

            string sql = string.Format(SQL_SELECT_BY_PATIENT, _PAT_ID);
            DataTable dt = db.Query(sql);

            Store istore = Grid_clinical1_nurse.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void RowSelect(object sender, DirectEventArgs e) {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);
            string selRowDate = selRow[0]["info_date"].ToString();
            string pif_ic = selRow[0]["pif_ic"].ToString();

            WndReport.Show();
            WndReport.Loader.SuspendScripting();
            WndReport.Loader.Url = "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + selRowDate + "&_REPORT_NAME=61&_PAT_IC=" + pif_ic;
            WndReport.Loader.DisableCaching = true;
            WndReport.LoadContent();
        }
    }
}