using System;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

using System.Xml.Xsl;
using System.Xml;

namespace Dialysis_Chart_Show.Information
{
    public partial class DialysisTwoWeekList : BaseForm
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                getTwoWeekList();
            }

        }

        protected void getTwoWeekList()
        {
            int iCount = 0, iCs = 3, iOrn = 0;
            string sql, sColumn, sColumnNAME;
            string sFlag = "0", sNo = "0", sFlSec = "0";
            DateTime today = DateTime.Now;
            string sDate = today.ToString("yyyy-MM-dd");
            string sMdate = today.ToString("yyyy-MM-dd").Substring(0, 8);
            string weekType = (0 == (iOrn = today.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString();
            string sTitle1 = " " + sMdate + "01 ~ " + today.ToString("yyyy-MM-dd").Substring(8, 2);
            string sTitle = "双周排班";
            DateTime nextMonday = today.AddDays((0 == (iOrn = ((int)DayOfWeek.Monday - (int)today.DayOfWeek + 7) % 7)) ? 7 : iOrn);

            sql = @"SELECT a.*,p.pif_id,p.pif_name as patrefid, concat(pv_floor,pv_sec) as FlSec, concat(pv_floor,pv_sec,pv_bedno,time_type) as sNo,p.pif_ic as patic FROM appointment a,pat_info p " +
                   "where p.pif_id not in (select b.pif_id from pat_info b " +
                   "inner join (select a.pat_id,a.opt_1 from zinfo_a_07 a " +
                   "inner join (select pat_id,max(info_date) AS last_date from zinfo_a_07 group by pat_id) b " +
                   "on a.pat_id=b.pat_id and a.info_date=b.last_date) f " +
                   "on b.pif_id = f.pat_id and f.opt_1 in('1','2','3','4')) " +
                   "AND a.pif_id = p.pif_id " +
                   "AND appointment_date BETWEEN '" + nextMonday.ToString("yyyy-MM-dd") + "' AND '" + nextMonday.AddDays(13).ToString("yyyy-MM-dd") + "' " +
                   "ORDER BY sNo, appointment_date ";

            DataTable dt = db.Query(sql);
            DataTable dtNCL = new DataTable();

            for (int i = 1; i < 17; i++)
            {
                sColumn = "C" + i.ToString();
                dtNCL.Columns.Add(sColumn);
            }

            DataRow drN = dtNCL.NewRow();
            drN["C1"] = "区域";
            drN["C2"] = "日期";

            for (iCs = 3; iCs < 17; iCs++)
            {
                sColumnNAME = "C" + iCs.ToString();
                drN[sColumnNAME] = string.Format("{0:M}", nextMonday.AddDays(iCs - 3));
            }

            dtNCL.Rows.Add(drN);

            if (dt.Rows.Count > 0)
            {
                foreach (DataRow dr in dt.Rows)
                {
                    int iTtype = int.Parse(dr["time_type"].ToString());
                    sNo = dr["sNo"].ToString();

                    if (sNo != sFlag)
                    {
                        if (sFlag != "0")
                            dtNCL.Rows.Add(drN);
                        iCount = 0;
                        drN = dtNCL.NewRow();
                        sFlag = sNo;
                    }

                    if (sFlSec != dr["FlSec"].ToString())
                    {
                        drN = dtNCL.NewRow();
                        drN["C1"] = dr["pv_floor"].ToString() + "楼";
                        drN["C2"] = dr["pv_sec"].ToString() + "区";
                        dtNCL.Rows.Add(drN);
                        sFlSec = dr["FlSec"].ToString();
                        drN = dtNCL.NewRow();
                    }

                    if (iCount == 0)
                    {
                        switch (dr["time_type"].ToString())
                        {
                            case "001":
                                drN["C2"] = "早";
                                break;
                            case "002":
                                drN["C2"] = "中";
                                break;
                            case "003":
                                drN["C2"] = "晚";
                                break;
                        }
                        drN["C1"] = dr["pv_bedno"].ToString();
                    }

                    if (sFlag != "0")
                    {
                        if (dr["appointment_date"].ToString() == nextMonday.ToString("yyyy-MM-dd"))
                            drN["C3"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(1).ToString("yyyy-MM-dd"))
                            drN["C4"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(2).ToString("yyyy-MM-dd"))
                            drN["C5"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(3).ToString("yyyy-MM-dd"))
                            drN["C6"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(4).ToString("yyyy-MM-dd"))
                            drN["C7"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(5).ToString("yyyy-MM-dd"))
                            drN["C8"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(6).ToString("yyyy-MM-dd"))
                            drN["C9"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(7).ToString("yyyy-MM-dd"))
                            drN["C10"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(8).ToString("yyyy-MM-dd"))
                            drN["C11"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(9).ToString("yyyy-MM-dd"))
                            drN["C12"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(10).ToString("yyyy-MM-dd"))
                            drN["C13"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(11).ToString("yyyy-MM-dd"))
                            drN["C14"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(12).ToString("yyyy-MM-dd"))
                            drN["C15"] = dr["patrefid"].ToString();
                        if (dr["appointment_date"].ToString() == nextMonday.AddDays(13).ToString("yyyy-MM-dd"))
                            drN["C16"] = dr["patrefid"].ToString();
                    }
                    iCount++;
                }
            }
            else
            {
                drN = dtNCL.NewRow();
                drN["C1"] = "无资料:";
                drN["C2"] = "请移驾双周预约排班";
            }
            dtNCL.Rows.Add(drN);
            db.myConnection.Close();

            Store istore = Grid_TwoWeek_List.GetStore();
            istore.DataSource = db.GetDataArray(dtNCL);
            istore.DataBind();
        }

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

    }
}