using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class dry_weight_Chart : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                BDATE.Text = Request.QueryString["BEG_DATE"];
                EDATE.Text = Request.QueryString["END_DATE"];
                var jsons = new List<object>();

                string sSQL;
                sSQL = "SELECT cln1_diadate, cln1_col5, cln1_col8, cln1_col6 FROM clinical1_nurse ";
                sSQL += "WHERE cln1_patic='" + _PAT_IC + "' ";
                sSQL += "AND cln1_diadate BETWEEN '" + BDATE.Text + "' AND '" + EDATE.Text + "' ";
                sSQL += "ORDER BY cln1_diadate DESC";
                DataTable dt = db.Query(sSQL);
                double b_weight, a_weight, d_weight;
                foreach(DataRow irow in dt.Rows)
                {
                    try
                    {
                        a_weight = double.Parse(irow["cln1_col8"].ToString());
                    }
                    catch
                    {
                        a_weight = 0;
                    }
                    try
                    {
                        b_weight = double.Parse(irow["cln1_col5"].ToString());
                    }
                    catch
                    {
                        b_weight = 0;
                    }

                    try
                    {
                        d_weight = double.Parse(irow["cln1_col6"].ToString());
                    }
                    catch
                    {
                        d_weight = 0;
                    }

                    jsons.Add(new
                    {
                        month = irow["cln1_diadate"].ToString(), //日期
                        before_weight = b_weight,
                        after_weight = a_weight,
                        dry_weight = d_weight
                    });
                }
                this.Chart1.GetStore().DataSource = jsons;
            }
        }
    }
}