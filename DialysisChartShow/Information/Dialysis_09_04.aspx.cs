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
    public partial class Dialysis_09_04 : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                txtBegin_DATE.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
                txtEnd_DATE.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Today);
                getList();
            }

        }

        protected void getList()
        {
            string sql = "select a.cln1_patic, e.pif_name, ";
            sql += "(SELECT COUNT(*) FROM clinical1_nurse f WHERE f.cln1_diadate>='" + _Get_YMD2(txtBegin_DATE.Text) + "' AND f.cln1_diadate<='" + _Get_YMD2(txtEnd_DATE.Text) + "' AND (f.cln1_col16 like 'EPO%' OR f.cln1_col16 like '%左%') AND f.cln1_patic=a.cln1_patic) as Column16, ";
            sql += "(SELECT COUNT(*) FROM clinical1_nurse b WHERE b.cln1_diadate>='" + _Get_YMD2(txtBegin_DATE.Text) + "' AND b.cln1_diadate<='" + _Get_YMD2(txtEnd_DATE.Text) + "' AND b.cln1_col16 like 'EPO%' AND b.cln1_col28 like '%怡宝%' AND b.cln1_patic=a.cln1_patic) as epo3000, ";
            sql += "(SELECT COUNT(*) FROM clinical1_nurse c WHERE c.cln1_diadate>='" + _Get_YMD2(txtBegin_DATE.Text) + "' AND c.cln1_diadate<='" + _Get_YMD2(txtEnd_DATE.Text) + "' AND c.cln1_col16 like 'EPO%' AND c.cln1_col28 like '%益比奧%' AND c.cln1_patic=a.cln1_patic) as epo10000, ";
            sql += "(SELECT COUNT(*) FROM clinical1_nurse d WHERE d.cln1_diadate>='" + _Get_YMD2(txtBegin_DATE.Text) + "' AND d.cln1_diadate<='" + _Get_YMD2(txtEnd_DATE.Text) + "' AND d.cln1_col16 like '%左%' AND d.cln1_patic=a.cln1_patic) as lcard ";
            sql += "FROM clinical1_nurse a ";
            sql += "LEFT JOIN pat_info e ON e.pif_ic=a.cln1_patic ";
            sql += "WHERE a.cln1_diadate>='" + _Get_YMD2(txtBegin_DATE.Text) + "' AND a.cln1_diadate<='" + _Get_YMD2(txtEnd_DATE.Text) + "' ";
            if (txtName.Text!="")
                sql += "AND e.pif_name LIKE '%" + txtName.Text + "%' ";
            sql += "GROUP BY a.cln1_patic ";
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