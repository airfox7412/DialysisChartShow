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
    public partial class Dialysis_10_C01 : BaseForm
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GetGroupName();
            }
        }

        protected void btn_Query11_Click(object sender, DirectEventArgs e)
        {
            //自订义检验
            string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
            string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());
            string gcode = GetComboBoxValue(ComboBoxGroup);
            string rcode = GetComboBoxValue(cboCODE11);
            string sRESULT_CODE = txtRESULT_CODE11.Text;
            double dLOW = 0;
            double dHIGH = 0;

            if (Double.TryParse(txtRESULT_LOW.Text, out dLOW))
                dLOW = Convert.ToDouble(txtRESULT_LOW.Text);
            if (Double.TryParse(txtRESULT_HIGH.Text, out dHIGH))
                dHIGH = Convert.ToDouble(txtRESULT_HIGH.Text);

            txtRESULT_LOW.Text = dLOW.ToString();
            txtRESULT_HIGH.Text = dHIGH.ToString();

            if ((sRESULT_CODE != "") && (dHIGH >= dLOW) && (dHIGH > 0))
            {
                if (sBEG_DATE == "")
                    sBEG_DATE = "2000-01-01";
                if (sEND_DATE == "")
                    sEND_DATE = "9999-12-31";

                string sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, '" + GetComboBoxText(cboCODE11) + "' AS RESULT_NAME, A.RESULT_VALUE_N ";
                sSQL += "FROM a_result_log A ";
                sSQL += "LEFT JOIN pat_info B ON A.PAT_NO=B.pif_id ";
                sSQL += "LEFT JOIN zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1=5 ";
                sSQL += "WHERE A.RESULT_VER=0 ";
                sSQL += "AND A.RESULT_VALUE_N>=" + dLOW.ToString() + " ";
                sSQL += "AND A.RESULT_VALUE_N<=" + dHIGH.ToString() + " ";
                sSQL += "AND A.RESULT_CODE='" + sRESULT_CODE + "' ";
                sSQL += "AND A.RESULT_DATE>='" + sBEG_DATE + "' AND A.RESULT_DATE<='" + sEND_DATE + "' ";
                sSQL += "ORDER BY A.PAT_NO, A.RESULT_CODE, A.RESULT_DATE";
                DataTable dtCHECK = db.Query(sSQL);
                if (dtCHECK.Rows.Count > 0)
                {
                    //算合格人數
                    Store istore1 = this.GridPanel13.GetStore();
                    istore1.DataSource = db.GetDataArray_AddRowNum(dtCHECK);
                    istore1.DataBind();
                }
            }
        }

        private void GetGroupName()
        {
            string sSQL = "SELECT GROUP_NAME NAME, GROUP_CODE CODE FROM a_item_group ";
            sSQL += "WHERE GROUP_USED='Y' ";
            sSQL += "ORDER BY GROUP_CLASS ";
            DataTable dt = db.Query(sSQL);
            Common.SetComboBoxItem(ComboBoxGroup, dt, false, "NAME", "CODE");
        }

        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            string sSQL = "SELECT RITEM_CLASS, RITEM_CODE, RITEM_TYPE, RITEM_NAME_S, RITEM_NAME, RITEM_UNIT, RITEM_LOW1, RITEM_HIGH1 ";
            sSQL += "FROM a_ritem_setup ";
            sSQL += "WHERE RITEM_CLASS='" + GetComboBoxValue(ComboBoxGroup) + "' ";
            sSQL += "AND RITEM_USED='Y' ";
            DataTable dt = db.Query(sSQL);
            SetComboBoxItem(cboCODE11, dt, false, "RITEM_NAME", "RITEM_CODE");
            //cboCODE11.Text = dt.Rows.Count == 0 ? "没有资料" : dt.Rows[0]["RITEM_NAME"].ToString();
            cboCODE11.Select(0);
        }

        protected void cboCODE11_Click(object sender, DirectEventArgs e)
        {
            string sSQL = "SELECT * FROM a_ritem_setup ";
            sSQL += "WHERE RITEM_CLASS='" + GetComboBoxValue(ComboBoxGroup) + "' AND RITEM_CODE='" + GetComboBoxValue(cboCODE11) + "' ";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                this.txtRESULT_CODE11.Text = dt.Rows[0]["RITEM_CODE"].ToString();
                this.txtRESULT_LOW.Text = dt.Rows[0]["RITEM_LOW1"].ToString();
                this.txtRESULT_HIGH.Text = dt.Rows[0]["RITEM_HIGH1"].ToString();
                this.txtNORMAL11.Text = dt.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dt.Rows[0]["RITEM_HIGH1"].ToString() + " " + dt.Rows[0]["RITEM_UNIT"].ToString();
            }
        }

        #region 匯出格式檔案
        protected void ToXml_13(object sender, EventArgs e)
        {
            string json = this.Hidden13.Value.ToString();
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

        protected void ToExcel_13(object sender, EventArgs e)
        {
            string json = this.Hidden13.Value.ToString();
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

        protected void ToCsv_13(object sender, EventArgs e)
        {
            string json = this.Hidden13.Value.ToString();
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