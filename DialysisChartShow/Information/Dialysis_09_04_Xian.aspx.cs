using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using System.Drawing;
using System.Web.Services;
using Dialysis_Chart_Show.tools;
using System.Xml.Xsl;
using System.Xml;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_04_Xian : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                txtDATE.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
                getList();
            }

        }

        protected void getList()
        {
            string sql = "select e.pif_mrn AS id, e.pif_name AS Name, ";
            sql += "a.cln1_col35 AS Column1, a.cln1_col13 AS Column2, a.cln1_col14 AS Column3, a.cln1_col28 AS Column4,";
            sql += "a.cln1_col29 AS Column5, a.cln1_col30 AS Column6, a.cln1_col31 AS Column7, a.cln1_col38 AS Column8 ";
            sql += "FROM clinical1_nurse a ";
            sql += "LEFT JOIN pat_info e ON e.pif_ic=a.cln1_patic ";
            sql += "WHERE a.cln1_diadate='" + _Get_YMD2(txtDATE.Text) + "' ";
            if (txtName.Text!="")
                sql += "AND e.pif_name LIKE '%" + txtName.Text + "%' ";
            sql += "ORDER BY a.cln1_patic";

            DataTable dt = db.Query(sql);
            db.myConnection.Close();
            Store istore = GridPanelList.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void cmdQuery(object sender, EventArgs e)
        {
            getList();
        }

        protected void cmdToExcel(object sender, EventArgs e)
        {
            string json = this.export.Value.ToString();
            StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
            XmlNode xml = eSubmit.Xml;
            this.Response.Clear();
            this.Response.ContentType = "application/vnd.ms-excel";
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
            XslCompiledTransform xtExcel = new XslCompiledTransform();
            xtExcel.Load(Server.MapPath("Excel.xsl"));
            xtExcel.Transform(xml, null, this.Response.OutputStream);
            this.Response.End();
        }

    }
}