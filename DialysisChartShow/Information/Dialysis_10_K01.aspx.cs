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
    public class PieItem
    {
        public string PIE_NAME
        {
            get;
            set;
        }
        public int PIE_DATA
        {
            get;
            set;
        }
    }

    public partial class Dialysis_10_K01 : BaseForm
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GetGroupName();
                SetItems();
            }
        }

        protected void btn_Query12_Click(object sender, DirectEventArgs e)
        {
            ShowGridPanel();
        }

        private void GetGroupName()
        {
            string sSQL = "SELECT GROUP_NAME NAME, GROUP_CODE CODE FROM a_item_group ";
            sSQL += "WHERE GROUP_USED='Y' ";
            sSQL += "ORDER BY GROUP_CLASS ";
            DataTable dt = db.Query(sSQL);
            Common.SetComboBoxItem(ComboBoxGroup, dt, false, "NAME", "CODE");
            ComboBoxGroup.Select(0);
        }

        private void SetItems()
        {
            string sSQL = "SELECT RITEM_CLASS, RITEM_CODE, RITEM_TYPE, RITEM_NAME_S, RITEM_NAME, RITEM_UNIT, RITEM_LOW1, RITEM_HIGH1 ";
            sSQL += "FROM a_ritem_setup ";
            sSQL += "WHERE RITEM_CLASS='G001' ";
            sSQL += "AND RITEM_USED='Y' ";
            DataTable dt = db.Query(sSQL);
            SetComboBoxItem(cboCODE12, dt, false, "RITEM_NAME", "RITEM_CODE");
            //cboCODE11.Text = dt.Rows.Count == 0 ? "没有资料" : dt.Rows[0]["RITEM_NAME"].ToString();
            cboCODE12.Select(0);
        }

        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            SetItems();
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

        protected void ShowGridPanel()
        {
            string sRESULT_CODE = txtRESULT_CODE12.Text;
            string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
            string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());
            string sSQL = "";

            if (sRESULT_CODE != "")
            {
                sSQL = "SELECT B.pif_name, B.pif_ic, CASE B.pif_sex WHEN 'M' THEN '男' WHEN 'F' THEN '女' ELSE '' END as sex, " +
                        "A.PAT_NO, A.RESULT_DATE, A.RESULT_CODE, '" + Common.GetComboBoxText(cboCODE12) + "' AS RESULT_NAME, A.RESULT_VALUE_N, " +
                        "C.RITEM_LOW1, C.RITEM_HIGH1 " +
                        "FROM a_result_log A " +
                        "LEFT JOIN pat_info B ON A.PAT_NO=B.pif_id " +
                        "LEFT JOIN a_ritem_setup C ON A.RESULT_CLASS=C.RITEM_CLASS AND A.RESULT_CODE=C.RITEM_CODE " +
                        "WHERE A.RESULT_VER=0 " +
                        "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                        "AND A.RESULT_DATE>='" + sBEG_DATE + "' AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                        "ORDER BY A.PAT_NO, A.RESULT_CODE, A.RESULT_DATE, B.pif_name, B.pif_ic ";
                DataTable dtCHECK = db.Query(sSQL);
                Store istore1 = this.GridPanel15.GetStore();
                istore1.DataSource = db.GetDataArray_AddRowNum(dtCHECK);
                istore1.DataBind();
                ShowPieChart(dtCHECK);
            }
        }

        protected void ShowPieChart(DataTable dtCHECK)
        {
            try
            {
                var LineData = new List<PieItem>();
                DataTable dt_count = new DataTable();
                string sRESULT_CODE = txtRESULT_CODE12.Text;
                string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
                string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());

                string sSQL = "SELECT sn, low_condition, high_condition FROM a_setting ";
                sSQL += "WHERE code='" + sRESULT_CODE + "' ";
                sSQL += "ORDER BY sn";
                DataTable dt = db.Query(sSQL);
                foreach (DataRow dr in dt.Rows)
                {
                    string vname = "Cond_" + dr["sn"].ToString();
                    string low_condition = dr["low_condition"].ToString();
                    string high_condition = dr["high_condition"].ToString();
                    sSQL = "SELECT * FROM a_result_log A " +
                        "WHERE A.RESULT_VER=0 " +
                        "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                        "AND A.RESULT_DATE>='" + sBEG_DATE + "' AND A.RESULT_DATE<='" + sEND_DATE + "' ";
                    if (low_condition != "")
                    {
                        sSQL += "AND " + low_condition + "A.RESULT_VALUE_N ";
                    }
                    if (high_condition != "")
                    {
                        sSQL += "AND A.RESULT_VALUE_N" + high_condition;
                    }
                    dt_count = db.Query(sSQL);
                    logger.Error("SQL: " + sSQL);
                    if (dt_count.Rows.Count > 0)
                    {
                        PieItem record = new PieItem
                        {
                            PIE_NAME = low_condition + "Value" + high_condition,
                            PIE_DATA = dt_count.Rows.Count
                        };
                        LineData.Add(record);
                    }
                }                              
                this.Chart1.GetStore().Data = LineData;
                this.Chart1.GetStore().DataBind();
            }
            catch (Exception ex)
            {
                Common._NotificationShow(ex.Message.ToString());
            }
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

        protected void btn_Query2_Click(object sender, DirectEventArgs e)
        {
            string code = Common.GetComboBoxValue(cboCODE12);
            string sSQL = "SELECT low_condition, high_condition FROM a_setting ";
            sSQL += "WHERE code='" + code + "' ";
            sSQL += "ORDER BY sn"; 
            DataTable dt = db.Query(sSQL);
            Store istore1 = this.GridList.GetStore();
            istore1.DataSource = db.GetDataArray_AddRowNum(dt);
            istore1.DataBind();
            Window1.Show();
        }

        protected void btnAdd_Click(object sender, DirectEventArgs e)
        {
            
        }

        protected void Win_Close(object sender, DirectEventArgs e)
        {
            Window1.Hide();
        }

    }
}