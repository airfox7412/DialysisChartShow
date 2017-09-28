using System;
using Ext.Net;
using System.Data;
using Dialysis_Chart_Show.tools;
using System.Xml.Xsl;
using System.Xml;
using NLog;
using System.Drawing;
using System.IO;
using System.Collections.Generic;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_10_D01 : BaseForm
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GetGroupName();
            }
        }

        protected void btn_Query12_Click(object sender, DirectEventArgs e)
        {
            //單個患者檢驗查詢
            string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
            string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());
            string sSQL = "";
            string sRESULT_CODE = this.txtRESULT_CODE12.Text;

            if (sRESULT_CODE != "")
            {
                if (sBEG_DATE == "")
                    sBEG_DATE = "2000-01-01";
                if (sEND_DATE == "")
                    sEND_DATE = "9999-12-31";

                sSQL = "SELECT * FROM a_ritem_setup ";
                sSQL += "WHERE RITEM_CLASS='" + GetComboBoxValue(ComboBoxGroup) + "' AND RITEM_CODE='" + GetComboBoxValue(cboCODE12) + "' ";
                DataTable dt = db.Query(sSQL);
                //DataRow[] dr;
                //dr = dt_ritem_setup12.Select("RITEM_CODE='" + GetComboBoxValue(cboCODE12) + "' ");

                //找有做檢查的人
                sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, '" + cb_patlist.Text + "' AS RESULT_NAME, A.RESULT_VALUE_N " +
                         "FROM a_result_log A " +
                         "LEFT JOIN pat_info B ON A.PAT_NO=B.pif_id " +
                         "LEFT JOIN zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1=5 " +
                        "WHERE A.RESULT_VER=0 " +
                          "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                          "AND A.RESULT_DATE>='" + sBEG_DATE + "' AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                          "AND (B.PIF_NAME LIKE '%" + cb_patlist.Text + "%' OR B.PIF_IC LIKE '%" + cb_patlist.Text + "%') " +
                        "ORDER BY A.PAT_NO, A.RESULT_CODE, A.RESULT_DATE, B.pif_name, B.pif_ic ";
                DataTable dtCHECK = db.Query(sSQL);
                Store istore1 = this.GridPanel15.GetStore();
                istore1.DataSource = db.GetDataArray_AddRowNum(dtCHECK);
                istore1.DataBind();

                try
                {
                    int iCHART_L = 999;
                    int iCHART_H = 0;
                    double dd = 0;
                    if (Double.TryParse(dt.Rows[0]["RITEM_HIGH1"].ToString(), out dd))
                        dd = Convert.ToDouble(dt.Rows[0]["RITEM_HIGH1"].ToString());
                    if (dd > iCHART_H)
                        iCHART_H = Convert.ToInt16(dd);
                    if (dd > iCHART_H)
                        iCHART_H = iCHART_H + 1;

                    if (Double.TryParse(dt.Rows[0]["RITEM_LOW1"].ToString(), out dd))
                        dd = Convert.ToDouble(dt.Rows[0]["RITEM_LOW1"].ToString());
                    if (dd < iCHART_L)
                        iCHART_L = Convert.ToInt16(dd);

                    var LineData = new List<Item>();

                    for (int i = 0; i < dtCHECK.Rows.Count; i++)
                    {
                        if (Double.TryParse(dtCHECK.Rows[i]["RESULT_VALUE_N"].ToString(), out dd))
                            dd = Convert.ToDouble(dtCHECK.Rows[i]["RESULT_VALUE_N"]);
                        if (dd > iCHART_H)
                            iCHART_H = Convert.ToInt16(dd);
                        if (dd > iCHART_H)
                            iCHART_H = iCHART_H + 1;
                        if (dd < iCHART_L)
                            iCHART_L = Convert.ToInt16(dd);

                        Item record = new Item
                        {
                            RESULT_DATE = "(" + (i + 1).ToString() + ") " + dtCHECK.Rows[i]["RESULT_DATE"].ToString(),
                            RESULT_VALUE_N = Convert.ToDouble(dtCHECK.Rows[i]["RESULT_VALUE_N"]),
                            RESULT_VALUE_L = Convert.ToDouble(dt.Rows[0]["RITEM_LOW1"]),
                            RESULT_VALUE_H = Convert.ToDouble(dt.Rows[0]["RITEM_HIGH1"])
                        };
                        LineData.Add(record);
                    }

                    ((CategoryAxis)Chart1.Axes[0]).SetTitle("检验日期 " + cb_patlist.Text + " " + this.txtNORMAL12.Text);

                    ((NumericAxis)Chart1.Axes[1]).SetMinimum(iCHART_L);
                    ((NumericAxis)Chart1.Axes[1]).SetMaximum(iCHART_H);
                    this.Chart1.GetStore().Data = LineData;
                    this.Chart1.GetStore().DataBind();
                }
                catch (Exception ex)
                {
                    Common._NotificationShow(ex.Message.ToString());
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
            SetComboBoxItem(cboCODE12, dt, false, "RITEM_NAME", "RITEM_CODE");
            //cboCODE11.Text = dt.Rows.Count == 0 ? "没有资料" : dt.Rows[0]["RITEM_NAME"].ToString();
            cboCODE12.Select(0);
        }

        protected void cboCODE12_Click(object sender, DirectEventArgs e)
        {
            string sSQL = "SELECT * FROM a_ritem_setup ";
            sSQL += "WHERE RITEM_CLASS='" + GetComboBoxValue(ComboBoxGroup) + "' AND RITEM_CODE='" + GetComboBoxValue(cboCODE12) + "' ";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                this.txtRESULT_CODE12.Text = dt.Rows[0]["RITEM_CODE"].ToString();
                this.txtNORMAL12.Text = dt.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dt.Rows[0]["RITEM_HIGH1"].ToString() + " " + dt.Rows[0]["RITEM_UNIT"].ToString();
            }
        }

        protected void ReloadData(object sender, DirectEventArgs e)
        {
            this.Chart1.GetStore().DataBind();
        }

        protected void Find_IC(object sender, DirectEventArgs e)
        {
            //txtPERSON_NAME12.Text = e.ExtraParams["PAT_IC"];
            btn_Query12_Click(sender, e);
        }
        
        public void Download(string data)
        {
            data = data.Substring(data.IndexOf("base64,") + 7);
            byte[] bitmapData = Convert.FromBase64String(data);

            Response.Clear();
            Response.AddHeader("Content-Disposition", "attachment; filename=chart.png");
            Response.ContentType = "image/png";

            using (System.Drawing.Image image = System.Drawing.Image.FromStream(new MemoryStream(bitmapData)))
            {
                using (MemoryStream ms = new MemoryStream())
                {
                    image.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                    ms.WriteTo(Response.OutputStream);
                }
            }

            Response.End();
        }

        #region 匯出格式檔案

        protected void ToXml_15(object sender, EventArgs e)
        {
            string json = this.Hidden15.Value.ToString();
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

        protected void ToExcel_15(object sender, EventArgs e)
        {
            string json = this.Hidden15.Value.ToString();
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

        protected void ToCsv_15(object sender, EventArgs e)
        {
            string json = this.Hidden15.Value.ToString();
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