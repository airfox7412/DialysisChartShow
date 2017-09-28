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
    public partial class Dialysis_10_B11 : BaseForm
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                btn_Query2_Click();
            }
        }

        protected void btn_Query2_Click()
        {
            //死亡率
            string sBEG_DATE = _Get_YMD2(Session["sBEG_DATE"].ToString());
            string sEND_DATE = _Get_YMD2(Session["sEND_DATE"].ToString());
            string sSQL = "";

            txtDATE2.Text = sBEG_DATE + " ~ " + sEND_DATE;
            sSQL = "SELECT DISTINCT A.pv_ic " +
                     "FROM pat_visit A " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' ";
            //2014-07-31 ada 改成 有登記就算人(血透人数的计算按照  这个人只要排过班就算，不分楼层，不管有没有血透过。)
            sSQL = "SELECT B.pif_ic AS pv_ic " +
                     "FROM pat_info B " +

                     " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +

                    "WHERE 1=1 ";
            DataTable dtTOTAL2 = db.Query(sSQL);
            this.txtTOTAL2.Text = dtTOTAL2.Rows.Count.ToString();

            //找死亡的人
            //OLD
            //sSQL = "SELECT B.pif_name, B.pif_ic, A.pat_id AS PAT_NO, 0 as AGE, 0 as AGE_HD, A.txt_56 AS DIE_CAUSE, " +
            //              "A.info_date AS DIE_DATE, B.pif_dob, C.txt_9 AS HD_DATE, B.pif_sex, A.chk_55 " +
            //         "FROM zinfo_e_01 A " +
            //         "LEFT JOIN pat_info B " +
            //           "ON A.pat_id=B.pif_id " +
            //         "LEFT JOIN zinfo_f_012 C " +
            //           "ON A.pat_id=C.pat_id " +
            //        "WHERE A.info_date>='" + sBEG_DATE + "' " +
            //          "AND A.info_date<='" + sEND_DATE + "' " +
            //          "AND A.opt_52=4 ";

            //NEW 20150608 ANDY
            sSQL = "SELECT B.pif_name, B.pif_ic, A.pat_id AS PAT_NO, 0 as AGE, 0 as AGE_HD, '' AS DIE_CAUSE," +
                                      "A.info_date AS DIE_DATE,  B.pif_dob, C.dat_9 AS HD_DATE, B.pif_sex, A.chk_7 as chk_55 " +
                                 "FROM zinfo_a_07 A " +
                                 "LEFT JOIN pat_info B " +
                                   "ON  A.pat_id=B.pif_id " +
                                 "  AND A.opt_1 in('4') " +
                                 "LEFT JOIN zinfo_f_012 C " +
                                   "ON A.pat_id=C.pat_id " +
                                "WHERE A.info_date>='" + sBEG_DATE + "' " +
                                  "AND A.info_date<='" + sEND_DATE + "' ";


            DataTable dtDIE = db.Query(sSQL);
            this.txtDIE.Text = dtDIE.Rows.Count.ToString();
            string sDIE_CAUSE = "";
            string schk_55 = "";
            string sDIE_DATE = "";
            int iAGE = 0;
            int iAGE_HD = 0;
            for (int i = 0; i < dtDIE.Rows.Count; i++)
            {
                sDIE_CAUSE = "";
                sDIE_DATE = dtDIE.Rows[i]["DIE_DATE"].ToString();

                if (dtDIE.Rows[i]["pif_dob"].ToString().Length >= 4)
                {
                    if (Int32.TryParse(dtDIE.Rows[i]["pif_dob"].ToString().Substring(0, 4), out iAGE))
                    {
                        iAGE = Convert.ToInt32(dtDIE.Rows[i]["pif_dob"].ToString().Substring(0, 4));
                        dtDIE.Rows[i]["AGE"] = Convert.ToInt16(sDIE_DATE.Substring(0, 4)) - iAGE;
                    }
                    else
                    {
                        dtDIE.Rows[i]["AGE"] = 0;
                        //OLD sDIE_CAUSE += "'出生日期'资料错误，";
                        sDIE_CAUSE += "";
                    }
                }
                else
                {
                    dtDIE.Rows[i]["AGE"] = 0;
                    //OLD sDIE_CAUSE += "'出生日期'资料错误，";
                    sDIE_CAUSE += "";
                }

                if (sDIE_CAUSE == "")
                {
                    schk_55 = dtDIE.Rows[i]["chk_55"].ToString();
                    if (schk_55.Substring(0, 1) == "1")
                        sDIE_CAUSE += "心血管事件，";
                    if (schk_55.Substring(1, 1) == "1")
                        sDIE_CAUSE += "脑血管事件，";
                    if (schk_55.Substring(2, 1) == "1")
                        sDIE_CAUSE += "感染，";
                    if (schk_55.Substring(3, 1) == "1")
                        sDIE_CAUSE += "消化道出血等出血性疾病，";

                    //OLD MARK 20150608 ANDY
                    //if (dtDIE.Rows[i]["DIE_CAUSE"].ToString() != "")
                    //    sDIE_CAUSE += dtDIE.Rows[i]["DIE_CAUSE"].ToString() + "，";
                }

                //OLD MARK 20150608 ANDY 
                if (sDIE_CAUSE != "")
                {
                    dtDIE.Rows[i]["DIE_CAUSE"] = sDIE_CAUSE.Substring(0, sDIE_CAUSE.Length - 1);
                }
                switch (dtDIE.Rows[i]["pif_sex"].ToString().Trim())
                {
                    case "F":
                        dtDIE.Rows[i]["pif_sex"] = "女";
                        break;
                    case "M":
                        dtDIE.Rows[i]["pif_sex"] = "男";
                        break;
                }
            }
            Store istore1 = this.GridPanel3.GetStore();
            istore1.DataSource = db.GetDataArray_AddRowNum(dtDIE);
            istore1.DataBind();

            if (this.txtTOTAL2.Text == "0")
            {
                this.txtDIE_P.Text = "0";
            }
            else
            {
                this.txtDIE_P.Text = Percent(Convert.ToDouble(this.txtDIE.Text) / Convert.ToDouble(this.txtTOTAL2.Text) * 1000);
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
        protected void ToXml_3(object sender, EventArgs e)
        {
            string json = this.Hidden3.Value.ToString();
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

        protected void ToExcel_3(object sender, EventArgs e)
        {
            string json = this.Hidden3.Value.ToString();
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

        protected void ToCsv_3(object sender, EventArgs e)
        {
            string json = this.Hidden3.Value.ToString();
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