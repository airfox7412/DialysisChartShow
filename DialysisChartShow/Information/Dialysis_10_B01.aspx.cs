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
    public partial class Dialysis_10_B01 : BaseForm
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                txtRESULT_CODE.Text = Session["sCODE"].ToString();
                btn_Query1_Click();
            }
        }

        protected void hh(object sender, DirectEventArgs e)
        {
            btn_Query1_Click();
        }

        protected void btn_Query1_Click()
        {
            //檢驗
            string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
            string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());
            string sSQL = "";
            string sRESULT_CODE = this.txtRESULT_CODE.Text;
            if (sRESULT_CODE != "")
            {
                //找檢查項目
                sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
                DataTable dtCODE = db.Query(sSQL);
                if (dtCODE.Rows.Count > 0)
                {
                    this.txtRESULT_NAME.Text = dtCODE.Rows[0]["RITEM_NAME"].ToString();
                    this.txtRESULT_UNIT.Text = dtCODE.Rows[0]["RITEM_UNIT"].ToString();
                    this.txtNORMAL.Text = dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE.Rows[0]["RITEM_UNIT"].ToString();
                }

                //四捨五入
                if (chkFORMAT.Checked)
                    txtFORMAT.Text = dtCODE.Rows[0]["RITEM_FORMAT"].ToString();
                else
                    txtFORMAT.Text = "";

                //找受檢人
                sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                         "FROM pat_visit A " +
                         "LEFT JOIN pat_info B " +
                           "ON A.pv_ic=B.pif_ic " +
                        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                        "ORDER BY B.pif_id ";
                //2014-07-31 ada 改成 有登記就算人(血透人数的计算按照  这个人只要排过班就算，不分楼层，不管有没有血透过。)
                sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                         "FROM pat_info B " +
                         " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                        "WHERE 1=1 " +
                        "ORDER BY B.pif_id ";
                DataTable dtTOTAL = db.Query(sSQL);
                this.txtTOTAL1.Text = dtTOTAL.Rows.Count.ToString();

                //找有做檢查的人
                sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + this.txtRESULT_NAME.Text + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                         "FROM a_result_log A " +
                         "LEFT JOIN pat_info B " +
                           "ON A.PAT_NO=B.pif_id " +
                         " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                        "WHERE A.RESULT_VER=0 " +
                          "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                          "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                          "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                        "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
                DataTable dtCHECK = db.Query(sSQL);

                //四捨五入
                if (txtFORMAT.Text != "")
                {
                    for (int n = 0; n < dtCHECK.Rows.Count; n++)
                    {
                        dtCHECK.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK.Rows[n]["RESULT_VALUE_N"]).ToString(txtFORMAT.Text));
                        dtCHECK.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK.Rows[n]["RESULT_VALUE_N"]).ToString(txtFORMAT.Text);
                    }
                }
                else
                {
                    for (int n = 0; n < dtCHECK.Rows.Count; n++)
                    {
                        dtCHECK.Rows[n]["RESULT_VALUE_T"] = dtCHECK.Rows[n]["RESULT_VALUE_N"].ToString();
                    }
                }

                Store istore22 = this.GridPanel22.GetStore();
                istore22.DataSource = db.GetDataArray_AddRowNum(dtCHECK);
                istore22.DataBind();
                this.txtCHECK.Text = dtCHECK.Rows.Count.ToString();

                //算合格人數
                if (dtCODE.Rows[0]["RITEM_LOW1"].ToString() == "")
                    dtCODE.Rows[0]["RITEM_LOW1"] = "0";
                if (dtCODE.Rows[0]["RITEM_HIGH1"].ToString() == "")
                    dtCODE.Rows[0]["RITEM_HIGH1"] = "99999";
                System.Data.DataView dvCHECK;
                dvCHECK = dtCHECK.DefaultView;
                dvCHECK.RowFilter = "RESULT_VALUE_N>=" + dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE.Rows[0]["RITEM_HIGH1"].ToString();
                Store istore21 = this.GridPanel21.GetStore();
                istore21.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
                istore21.DataBind();
                this.txtCHECK_Y.Text = dvCHECK.Count.ToString();

                //算不合格人數
                //System.Data.DataView dvCHECK_N;
                //dvCHECK_N = dtCHECK.DefaultView;
                dvCHECK.RowFilter = "RESULT_VALUE_N<" + dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE.Rows[0]["RITEM_HIGH1"].ToString();
                this.txtCHECK_N.Text = dvCHECK.Count.ToString();

                Store istore1 = this.GridPanel1.GetStore();
                istore1.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
                istore1.DataBind();

                //this.txtERR.Text = ""; 
                //受檢人-有做檢查的人
                DataTable dtUNCHECK = dtTOTAL.Copy();
                System.Data.DataView dvUNCHECK;
                dvUNCHECK = dtUNCHECK.DefaultView;
                for (int i = 0; i < dtCHECK.Rows.Count; i++)
                {
                    dvUNCHECK.RowFilter = "pv_ic='" + dtCHECK.Rows[i]["pif_ic"] + "'";
                    if (dvUNCHECK.Count > 0)
                        dvUNCHECK[0].Delete();
                }

                dtUNCHECK.AcceptChanges();
                this.txtUNCHECK.Text = dtUNCHECK.Rows.Count.ToString();
                Store istore2 = this.GridPanel2.GetStore();
                istore2.DataSource = db.GetDataArray_AddRowNum(dtUNCHECK);
                istore2.DataBind();

                if (this.txtTOTAL1.Text == "0")
                {
                    this.txtCHECK_P.Text = "0";
                    this.txtUNCHECK_P.Text = "0";
                }
                else
                {
                    this.txtCHECK_P.Text = Percent(Convert.ToDouble(this.txtCHECK.Text) / Convert.ToDouble(this.txtTOTAL1.Text) * 100);
                    this.txtUNCHECK_P.Text = Percent(Convert.ToDouble(this.txtUNCHECK.Text) / Convert.ToDouble(this.txtTOTAL1.Text) * 100);
                }

                if (this.txtCHECK.Text == "0")
                {
                    this.txtCHECK_YP.Text = "0";
                    this.txtCHECK_NP.Text = "0";
                }
                else
                {
                    this.txtCHECK_YP.Text = Percent(Convert.ToDouble(this.txtCHECK_Y.Text) / Convert.ToDouble(this.txtCHECK.Text) * 100);
                    this.txtCHECK_NP.Text = Percent(Convert.ToDouble(this.txtCHECK_N.Text) / Convert.ToDouble(this.txtCHECK.Text) * 100);
                }
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
        protected void ToXml_1(object sender, EventArgs e)
        {
            string json = this.Hidden1.Value.ToString();
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

        protected void ToExcel_1(object sender, EventArgs e)
        {
            string json = this.Hidden1.Value.ToString();
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

        protected void ToCsv_1(object sender, EventArgs e)
        {
            string json = this.Hidden1.Value.ToString();
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

        protected void ToXml_2(object sender, EventArgs e)
        {
            string json = this.Hidden2.Value.ToString();
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

        protected void ToExcel_2(object sender, EventArgs e)
        {
            string json = this.Hidden2.Value.ToString();
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

        protected void ToCsv_2(object sender, EventArgs e)
        {
            string json = this.Hidden2.Value.ToString();
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

        protected void ToXml_21(object sender, EventArgs e)
        {
            string json = this.Hidden21.Value.ToString();
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

        protected void ToExcel_21(object sender, EventArgs e)
        {
            string json = this.Hidden21.Value.ToString();
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

        protected void ToCsv_21(object sender, EventArgs e)
        {
            string json = this.Hidden21.Value.ToString();
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

        protected void ToXml_22(object sender, EventArgs e)
        {
            string json = this.Hidden22.Value.ToString();
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

        protected void ToExcel_22(object sender, EventArgs e)
        {
            string json = this.Hidden22.Value.ToString();
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

        protected void ToCsv_22(object sender, EventArgs e)
        {
            string json = this.Hidden22.Value.ToString();
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