using System;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

using System.Xml.Xsl;
using System.Xml;

namespace Dialysis_Chart_Show.Information
{
    public partial class Biochemical_Not_Checked_List : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Show_Bio_Not_Checked_List();
            }
        }

        protected void Show_Bio_Not_Checked_List()
        {
            DBMysql db = new DBMysql();
            int iFlag = 0, iCount = 0, iCs = 4, iOrn = 0;
            string sql, sColumn, sColumnNAME, sColumnVALUE;
            DateTime today = DateTime.Now;
            string sDate = today.ToString("yyyy-MM-dd");
            string sMdate = today.ToString("yyyy-MM-dd").Substring(0, 8);
            string weekType = (0 == (iOrn = today.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString();
            string sTitle1 = " " + sMdate + "01 ~ " + today.ToString("yyyy-MM-dd").Substring(8, 2);
            string sTitle2 = "未检测项目";

            sql = "SELECT pi.pif_id, pi.PIF_IC, ars.RESULT_VALUE, pi.PIF_NAME, pi.RESULT_CODE, pi.RITEM_NAME, if (ars.RESULT_VALUE > 0, 0, 1) as NotChecked ";
            sql += "FROM ";
            sql += "(SELECT pio.pif_id, pio.pif_name, pio.pif_ic, bcl.RESULT_CODE, bcl.RITEM_NAME ";
            sql += "FROM pat_info pio ";
            sql += "NATURAL JOIN bio_check_list bcl where pio.pif_id IN ";
            sql += "(SELECT P.pif_id FROM mac_setup M ";
            sql += "LEFT JOIN appointment_setup A ON M.mac_flr = A.apptst_flr ";
            sql += "AND M.mac_sec = A.apptst_sec AND M.mac_bedno = A.apptst_bed AND A.apptst_daytyp='" + weekType + "' ";
            sql += "INNER JOIN pat_info P ON A.apptst_patic=P.pif_ic) and bcl.I_MONTH = 1) pi ";
            sql += "LEFT JOIN ";
            sql += "(SELECT  arl.pat_no, arl.result_date, arl.result_code, arl.RESULT_VALUE_T AS RESULT_VALUE ";
            sql += "FROM a_result_log arl ";
            sql += "where arl.RESULT_CODE IN (SELECT RESULT_CODE FROM bio_check_list where I_MONTH = 1) ";
            sql += "and result_date >= '" + sMdate + "01' and result_date <= '" + sDate + "') ars on pi.pif_id = ars.pat_no and pi.RESULT_CODE = ars.RESULT_CODE ";
            sql += "ORDER BY pi.pif_id, pi.RESULT_CODE ";

            DataTable dt = db.Query(sql);
            DataTable dtNCL = new DataTable();
            if (dt.Rows.Count > 0)
            {
                for (int i = 1; i < 46; i++)
                {
                    sColumn = "C" + i.ToString();
                    dtNCL.Columns.Add(sColumn);
                }
                DataRow drN = dtNCL.NewRow();

                foreach (DataRow dr in dt.Rows)
                {
                    int iPid = int.Parse(dr["pif_id"].ToString());

                    if (iPid != iFlag)
                    {
                        if (iFlag != 0)
                        {
                            drN["C3"] += iCount.ToString() + "项:　";
                            dtNCL.Rows.Add(drN);
                            iCount = 0;
                            iCs = 4;
                            drN = dtNCL.NewRow();
                        }
                        iFlag = iPid;
                    }

                    if (iCs > 5)
                    {
                        if (iCs % 2 == 0)
                            iCs += 2;
                    }
                    sColumnNAME = "C" + iCs.ToString();
                    sColumnVALUE = "C" + (iCs + 2).ToString();
                    if (iCount == 0)
                    {
                        drN["C1"] = dr["PIF_NAME"].ToString();
                        drN["C2"] = sTitle1;
                        drN["C3"] = sTitle2;
                        drN[sColumnNAME] = dr["RITEM_NAME"].ToString();
                        drN[sColumnVALUE] = dr["NotChecked"].ToString() == "1" ? "X" : dr["RESULT_VALUE"].ToString();
                    }
                    else
                    {
                        drN[sColumnNAME] = dr["RITEM_NAME"].ToString();
                        drN[sColumnVALUE] = dr["NotChecked"].ToString() == "1" ? "X" : dr["RESULT_VALUE"].ToString();
                    }
                    iCount++;
                    iCs++;
                }
                drN["C3"] += iCount.ToString() + "项:　";
                dtNCL.Rows.Add(drN);

                DataTable dtNCL2 = new DataTable();
                for (int i = 1; i < 4; i++)
                {
                    sColumn = "C" + i.ToString();
                    dtNCL2.Columns.Add(sColumn);
                }
                DataRow drN2 = dtNCL2.NewRow();

                foreach (DataRow dr in dtNCL.Rows)
                {
                    for (iCs = 1; iCs < 47; iCs++)
                    {
                        if (iCs < 4)
                        {
                            sColumnNAME = "C" + iCs.ToString();
                            drN2[sColumnNAME] = dr[sColumnNAME].ToString();
                        }
                        else if (iCs % 2 == 0)
                        {
                            dtNCL2.Rows.Add(drN2);
                            drN2 = dtNCL2.NewRow();
                        }

                        if (iCs > 3 && iCs < 46)
                        {
                            sColumnNAME = "C" + iCs.ToString();
                            sColumnVALUE = "C" + (iCs % 2 + 2).ToString();
                            drN2[sColumnVALUE] = dr[sColumnNAME].ToString();
                        }
                    }
                }

                Store istore = Grid_BioNotChecked_List.GetStore();
                istore.DataSource = db.GetDataArray_AddRowNum(dtNCL2);
                istore.DataBind();
            }
        }

        protected void Show_Bio_Not_Checked_List_orn()
        {
            DBMysql db = new DBMysql();
            int iFlag = 0, iCount = 0, iOrn = 0;
            string sql;
            DateTime today = DateTime.Now;
            string sDate = today.ToString("yyyy-MM-dd");
            string sMdate = today.ToString("yyyy-MM-dd").Substring(0, 8);
            string weekType = (0 == (iOrn = today.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString();
            string sTitle = " " + sMdate + "01 ~ " + sDate + "　未检测项目　";

            sql = "SELECT pi.pif_id, pi.PIF_IC, ars.RESULT_VALUE, pi.PIF_NAME, pi.RESULT_CODE, pi.RITEM_NAME, if (ars.RESULT_VALUE > 0, 0, 1) as NotChecked ";
            sql += "FROM ";
            sql += "(SELECT pio.pif_id, pio.pif_name, pio.pif_ic, bcl.RESULT_CODE, bcl.RITEM_NAME ";
            sql += "FROM pat_info pio ";
            sql += "NATURAL JOIN bio_check_list bcl where pio.pif_id IN ";
            sql += "(SELECT P.pif_id FROM mac_setup M ";
            sql += "LEFT JOIN appointment_setup A ON M.mac_flr = A.apptst_flr ";
            sql += "AND M.mac_sec = A.apptst_sec AND M.mac_bedno = A.apptst_bed AND A.apptst_daytyp='" + weekType + "' ";
            sql += "INNER JOIN pat_info P ON A.apptst_patic=P.pif_ic) and bcl.I_MONTH = 1) pi ";
            sql += "LEFT JOIN ";
            sql += "(SELECT  arl.pat_no, arl.result_date, arl.result_code, arl.RESULT_VALUE_T AS RESULT_VALUE ";
            sql += "FROM a_result_log arl ";
            sql += "where arl.RESULT_CODE IN (SELECT RESULT_CODE FROM bio_check_list where I_MONTH = 1) ";
            sql += "and result_date >= '" + sMdate + "01' and result_date <= '" + sDate + "') ars on pi.pif_id = ars.pat_no and pi.RESULT_CODE = ars.RESULT_CODE ";
            sql += "ORDER BY pi.pif_id, pi.RESULT_CODE ";

            DataTable dt = db.Query(sql);
            DataTable dtNCL = new DataTable();
            foreach (DataColumn item in dt.Columns)
            {
                string sColumn = item.ToString();
                if (sColumn == "PIF_IC" || sColumn == "RESULT_VALUE" || sColumn == "PIF_NAME")
                    dtNCL.Columns.Add(sColumn);
            }
            DataRow drN = dtNCL.NewRow();

            // 相同的資料,不同的版面,重新排版,未來有時間可考慮寫成陽春型排版系統(.net java 都有人做過) 例:excel vba  

            foreach (DataRow dr in dt.Rows)
            {
                int iPid = int.Parse(dr["pif_id"].ToString());

                if (iPid != iFlag)
                {
                    if (iFlag != 0)
                    {
                        drN["RESULT_VALUE"] += iCount.ToString() + " 项:　";
                        dtNCL.Rows.Add(drN);
                        iCount = 0;
                        drN = dtNCL.NewRow();
                    }
                    iFlag = iPid;
                }
                if (dr["NotChecked"].ToString() == "1")
                {
                    if (iCount == 0)
                    {
                        drN["PIF_IC"] = dr["PIF_NAME"].ToString();
                        drN["RESULT_VALUE"] = sTitle;
                        drN["PIF_NAME"] = dr["RITEM_NAME"].ToString();
                    }
                    else
                        drN["PIF_NAME"] += ", " + dr["RITEM_NAME"].ToString();
                    iCount++;
                }
            }
            drN["RESULT_VALUE"] += iCount.ToString() + " 项:　";
            dtNCL.Rows.Add(drN);
            Store istore = Grid_BioNotChecked_List.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dtNCL);
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