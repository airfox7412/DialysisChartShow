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


namespace Dialysis_Chart_Show.Information
{
    public partial class dry_weightlist : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            string gtyr = Request["wgyr"].ToString();
            string gtmth = Request["wgmth"].ToString();
            string seldt = gtyr + "-" + gtmth;

            if (gtmth == "99")
            {

                DBMysql db = new DBMysql();
                string sql;
                sql = "SELECT cln1_diadate, cln1_col6 from clinical1_nurse ";
                sql += "where cln1_patic = '" + _PAT_IC + "' ";
                sql += "and substr(cln1_diadate,1,4) = '" + gtyr + "' ";
                sql += "order by cln1_diadate DESC, cln1_id DESC";
                DataTable dt = db.Query(sql);

                Store istore = Grid_Weight.GetStore();
                istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
                istore.DataBind();
            }
            else 
            {
                DBMysql db = new DBMysql();
                string sql;
                sql = "SELECT cln1_diadate, cln1_col6 from clinical1_nurse ";
                sql += "where cln1_patic = '" + _PAT_IC + "' ";
                sql += "and substr(cln1_diadate,1,7) = '" + seldt + "' ";
                sql += "order by cln1_diadate DESC, cln1_id DESC";
                DataTable dt = db.Query(sql);

                Store istore = Grid_Weight.GetStore();
                istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
                istore.DataBind();
            }

        }
       
    }
}