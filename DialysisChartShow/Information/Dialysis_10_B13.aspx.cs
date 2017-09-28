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
    public partial class Dialysis_10_B13 : BaseForm
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                txtRESULT_CODE3.Text = Session["sCODE"].ToString();
                btn_Query9_Click();
            }
        }

        protected void btn_Query9_Click()
        {
            //檢驗 HBsAg 4032, AntiHCV 4033
            string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
            string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());
            string sSQL = "";
            string sRESULT_CODE = this.txtRESULT_CODE3.Text;
            if (sRESULT_CODE != "")
            {
                if (sBEG_DATE == "")
                    sBEG_DATE = "2000-01-01";
                if (sEND_DATE == "")
                    sEND_DATE = "9999-12-31";
                //找檢查項目
                sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
                DataTable dtCODE3 = db.Query(sSQL);
                if (dtCODE3.Rows.Count > 0)
                {
                    this.txtRESULT_NAME3.Text = dtCODE3.Rows[0]["RITEM_NAME"].ToString();
                    this.txtRESULT_UNIT3.Text = dtCODE3.Rows[0]["RITEM_UNIT"].ToString();
                    this.txtNORMAL3.Text = dtCODE3.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE3.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE3.Rows[0]["RITEM_UNIT"].ToString();
                }

                //找有做檢查的人
                sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + this.txtRESULT_NAME3.Text + "' AS RESULT_NAME,  A.RESULT_VALUE_T " +
                       "FROM a_result_log A " +
                       "LEFT JOIN pat_info B ON A.PAT_NO=B.pif_id " +
                       "LEFT JOIN zinfo_a_07 f ON f.pat_id=B.pif_id AND f.opt_1 IN('','5') " +
                       "WHERE A.RESULT_VER=0 " +
                       "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                       "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                       "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    //"AND A.RESULT_VALUE_T IN ('阴性','阳性') " +
                        "AND (A.RESULT_VALUE_T LIKE '阴性%' OR A.RESULT_VALUE_T LIKE '阳性%') " +
                        "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
                DataTable dtTOTAL9 = db.Query(sSQL);
                this.txtTOTAL9.Text = dtTOTAL9.Rows.Count.ToString();

                //old ?? Store istore2 = this.GridPanel12.GetStore();
                Store istore2 = this.GridPanel11.GetStore();
                istore2.DataSource = db.GetDataArray_AddRowNum(dtTOTAL9);
                istore2.DataBind();

                sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + this.txtRESULT_NAME3.Text + "' AS RESULT_NAME, A.RESULT_VALUE_T " +
                         "FROM a_result_log A " +
                         "LEFT JOIN pat_info B " +
                           "ON A.PAT_NO=B.pif_id " +
                         " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                        "WHERE A.RESULT_VER=0 " +
                          "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                          "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                          "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                          "AND A.RESULT_VALUE_T='" + "阳性" + "' " +
                        "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
                DataTable dtPOSITIVE = db.Query(sSQL);
                this.txtPOSITIVE.Text = dtPOSITIVE.Rows.Count.ToString();

                Store istore1 = this.GridPanel10.GetStore();
                istore1.DataSource = db.GetDataArray_AddRowNum(dtPOSITIVE);
                istore1.DataBind();
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
        protected void ToXml_10(object sender, EventArgs e)
        {
            string json = this.Hidden10.Value.ToString();
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

        protected void ToExcel_10(object sender, EventArgs e)
        {
            string json = this.Hidden10.Value.ToString();
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

        protected void ToCsv_10(object sender, EventArgs e)
        {
            string json = this.Hidden10.Value.ToString();
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

        protected void ToXml_11(object sender, EventArgs e)
        {
            string json = this.Hidden11.Value.ToString();
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

        protected void ToExcel_11(object sender, EventArgs e)
        {
            string json = this.Hidden11.Value.ToString();
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

        protected void ToCsv_11(object sender, EventArgs e)
        {
            string json = this.Hidden11.Value.ToString();
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