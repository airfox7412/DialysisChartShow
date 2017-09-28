using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Dialysis_Chart_Show.tools;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Xml.Xsl;
using System.Xml;
using NLog;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Configuration;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_10_B10 : BaseForm
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                btn_Query4_Click();
            }
        }

        protected void btn_Query4_Click()
        {
            //住院率
            string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
            string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());
            string sSQL = "";

            txtDATE4.Text = sBEG_DATE + " ~ " + sEND_DATE;

            //找受檢人
            sSQL = "SELECT DISTINCT A.pv_ic " +
                     "FROM pat_visit A " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' ";
            //2014-07-31 ada 改成 有登記就算人(血透人数的计算按照  这个人只要排过班就算，不分楼层，不管有没有血透过。)
            sSQL = "SELECT B.pif_ic AS pv_ic " +
                     "FROM pat_info B " +

                     " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +

                    "WHERE 1=1 ";
            DataTable dtTOTAL4 = db.Query(sSQL);
            this.txtTOTAL4.Text = dtTOTAL4.Rows.Count.ToString();

            //找住院的人
            sSQL = "SELECT B.pif_name, B.pif_sex, B.pif_ic, A.pat_id AS PAT_NO, " +
                          "A.info_date AS HOSP_DATE, A.txt_27 AS HOSP_CAUSE " +
                     "FROM zinfo_e_01 A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pat_id=B.pif_id " +

                    " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +

                    "WHERE A.info_date>='" + sBEG_DATE + "' " +
                      "AND A.info_date<='" + sEND_DATE + "' " +
                      "AND A.opt_26=1 ";
            DataTable dtHOSP = db.Query(sSQL);
            this.txtHOSP.Text = dtHOSP.Rows.Count.ToString();
            for (int i = 0; i < dtHOSP.Rows.Count; i++)
            {
                switch (dtHOSP.Rows[i]["pif_sex"].ToString().Trim())
                {
                    case "F":
                        dtHOSP.Rows[i]["pif_sex"] = "女";
                        break;
                    case "M":
                        dtHOSP.Rows[i]["pif_sex"] = "男";
                        break;
                }
            }
            Store istore1 = this.GridPanel5.GetStore();
            istore1.DataSource = db.GetDataArray_AddRowNum(dtHOSP);
            istore1.DataBind();

            if (this.txtTOTAL4.Text == "0")
            {
                this.txtHOSP_P.Text = "0";
            }
            else
            {
                this.txtHOSP_P.Text = Percent(Convert.ToDouble(this.txtHOSP.Text) / Convert.ToDouble(this.txtTOTAL4.Text) * 1000);
            }
        }

        private string Percent(double dd)
        {
            string ss = dd.ToString("0.00");
            if (ss.Substring(ss.Length - 1, 1) == "0")
                ss = ss.Substring(0, ss.Length - 1);
            if (ss.Substring(ss.Length - 1, 1) == "0")
                ss = ss.Substring(0, ss.Length - 1);
            if (ss.Substring(ss.Length - 1, 1) == ".")
                ss = ss.Substring(0, ss.Length - 1);
            return ss;
        }

        #region 匯出格式檔案
        protected void ToXml_5(object sender, EventArgs e)
        {
            string json = this.Hidden5.Value.ToString();
            StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
            XmlNode xml = eSubmit.Xml;
            string strXml = xml.OuterXml;
            this.Response.Clear();
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xml");
            this.Response.AddHeader("Content-Length", strXml.Length.ToString());
            this.Response.ContentType = "application/xml";
            this.Response.Write(strXml);
            this.Response.End();
        }

        protected void ToExcel_5(object sender, EventArgs e)
        {
            string json = this.Hidden5.Value.ToString();
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

        protected void ToCsv_5(object sender, EventArgs e)
        {
            string json = this.Hidden5.Value.ToString();
            StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
            XmlNode xml = eSubmit.Xml;
            this.Response.Clear();
            this.Response.ContentType = "application/octet-stream";
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
            XslCompiledTransform xtCsv = new XslCompiledTransform();
            xtCsv.Load(Server.MapPath("Csv.xsl"));
            xtCsv.Transform(xml, null, this.Response.OutputStream);
            this.Response.End();
        }
        #endregion
    }
}