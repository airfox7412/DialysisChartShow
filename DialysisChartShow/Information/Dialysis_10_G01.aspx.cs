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
    public partial class Dialysis_10_G01 : BaseForm
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
            }
        }

        //品质监控指标查询
        protected void btn_Query11Q_Click(object sender, DirectEventArgs e)
        {
            //2015-06-01 2015-06-30
            string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
            string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());
            string sSQL = "";
            //年代码
            //string sRESULT_CODE = this.txtRESULT_CODE11Q.Text;
            string sYEAR_CODE = this.sYEAR.Text;
            string sQT_CODE = this.sQT.Text;
            string w_year = "";
            string w_QT = "";
            if (sYEAR_CODE == "002")
            {
                w_year = "2013";
            }
            if (sYEAR_CODE == "003")
            {
                w_year = "2014";
            }
            if (sYEAR_CODE == "004")
            {
                w_year = "2015";
            }
            if (sYEAR_CODE == "005")
            {
                w_year = "2016";
            }
            if (sYEAR_CODE == "006")
            {
                w_year = "2017";
            }
            if (sYEAR_CODE == "007")
            {
                w_year = "2018";
            }

            if (sQT_CODE == "002")
            {
                w_QT = "";
            }
            if (sQT_CODE == "003")
            {
                w_QT = "月";
            }
            if (sQT_CODE == "004")
            {
                w_QT = "季";
            }
            if (sQT_CODE == "005")
            {
                w_QT = "半年";
            }
            if (sQT_CODE == "006")
            {
                w_QT = "年";
            }

            if (sBEG_DATE == "")
                sBEG_DATE = "2013-01-01";
            if (sEND_DATE == "")
                sEND_DATE = "2020-12-31";

            sSQL = "SELECT hq_date1,hq_date2,hq_txt_10," +
                   " hq_name,hq_num1,hq_d1,hq_d11,hq_d3,hq_d31,hq_d4,hq_d41,hq_d5,hq_d51,hq_d6,hq_d61,hq_d7,hq_d71,hq_d8,hq_d81,hq_d9,hq_d91,hq_d10,hq_d11a,hq_d12,hq_d13,hq_d14" +
                   " FROM hospital_quality_history";
            //20150627 年度
            if (w_year != "" && w_QT == "年")
            {
                sSQL += " where hq_date1 like '" + w_year + "%'";
                sSQL += " and hq_name ='年報'";
                sSQL += " order by hq_date1,hq_name";
            }
            if (w_year != "" && w_QT == "")
            {
                sSQL += " where hq_date1 like '" + w_year + "%'";
                sSQL += " order by hq_date1,hq_name";
            }
            if (w_year != "" && w_QT == "季")
            {
                sSQL += " where hq_date1 like '" + w_year + "%'";
                sSQL += " and hq_name ='季報'";
                sSQL += " order by hq_date1,hq_name";
            }
            if (w_year != "" && w_QT == "半年")
            {
                sSQL += " where hq_date1 like '" + w_year + "%'";
                sSQL += " and hq_name ='半年報'";
                sSQL += " order by hq_date1,hq_name";
            }
            if (w_year != "" && w_QT == "月")
            {
                sSQL += " where hq_date1 like '" + w_year + "%'";
                sSQL += " and hq_name ='月報'";
                sSQL += " order by hq_date1,hq_name";
            }
            DataTable dtCHECK = db.Query(sSQL);

            System.Data.DataView dvCHECK;
            dvCHECK = dtCHECK.DefaultView;

            Store istore1 = this.GridPanel13Q.GetStore();
            istore1.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
            istore1.DataBind();
        }

        //监控数据查询
        protected void cboCODE11Q_Click(object sender, DirectEventArgs e)
        {
            sYEAR.Text = Common.GetComboBoxText(cboCODE11Q);
        }

        protected void cboCODE11QT_Click(object sender, DirectEventArgs e)
        {
            sQT.Text = Common.GetComboBoxText(cboCODE11QT);
        }

        //[監控數據打印]
        protected void btn_Print11Q_Click(object sender, DirectEventArgs e)
        {
            string sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=98&_REPORT_P=1";
            sURL += "&_REPORT_sYEAR=" + sYEAR.Text + "&_REPORT_sQT=" + sQT.Text;

            Window1.Hidden = false;
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = sURL;
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        #region 匯出格式檔案

        protected void ToXml_13Q(object sender, EventArgs e)
        {
            string json = this.Hidden13Q.Value.ToString();
            //json = json.Replace(" ", "").Replace("/", "&frasl;");
            json = json.Replace(" ", "").Replace("/", "_");
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

        protected void ToExcel_13Q(object sender, EventArgs e)
        {
            string json = this.Hidden13Q.Value.ToString();
            //json = json.Replace(" ", "").Replace("/", "&frasl;");
            json = json.Replace(" ", "").Replace("/", "_");
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

        protected void ToCsv_13Q(object sender, EventArgs e)
        {
            string json = this.Hidden13Q.Value.ToString();
            //json = json.Replace(" ", "").Replace("/", "&frasl;");
            json = json.Replace(" ", "").Replace("/", "_");
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

        protected void ToXml_14Q(object sender, EventArgs e)
        {
            string json = this.Hidden14Q.Value.ToString();
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

        protected void ToExcel_14Q(object sender, EventArgs e)
        {
            string json = this.Hidden14Q.Value.ToString();
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

        protected void ToCsv_14Q(object sender, EventArgs e)
        {
            string json = this.Hidden14Q.Value.ToString();
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