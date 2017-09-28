using System;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

using System.Xml.Xsl;
using System.Xml;

namespace Dialysis_Chart_Show.Information
{
    public partial class Biochemical_Not_Checked_List_V2 : BaseForm
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
            int iFlag = 0, iCount = 0, iCs = 6, iOrn = 0;
            string sql, sColumn, sColumnNAME, sColumnVALUE;
            DateTime today = DateTime.Now;
            string sDate = today.ToString("yyyy-MM-dd");
            string sMdate = today.ToString("yyyy-MM-dd").Substring(0, 8);
            string weekType = (0 == (iOrn = today.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString();
            string sTitle1 = " " + sMdate + "01 ~ " + today.ToString("yyyy-MM-dd").Substring(8, 2);
            string sTitle2 = "未检测项目";

            sql = "SELECT COUNT(RESULT_CODE)-1 FROM bio_check_list where I_MONTH = 1";
            DataTable dtC = db.Query(sql);
            int iMax = int.Parse(dtC.Rows[0][0].ToString()) / 5 * 10 + 16;

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
            for (int i = 1; i < iMax; i++)
            {
                sColumn = "C" + i.ToString();
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
                        drN["C3"] += iCount.ToString() + "项:　";
                        dtNCL.Rows.Add(drN);
                        iCount = 0;
                        iCs = 6;
                        drN = dtNCL.NewRow();
                    }
                    iFlag = iPid;
                }

                if (iCs % 10 == 1)
                    iCs += 5;
                sColumnNAME = "C" + iCs.ToString();
                sColumnVALUE = "C" + (iCs + 5).ToString();
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
                if (dr["NotChecked"].ToString() == "1")
                    iCount++;
                iCs++;
            }
            drN["C3"] += iCount.ToString() + "项:　";
            dtNCL.Rows.Add(drN);

            DataTable dtNCL2 = new DataTable();
            for (int i = 1; i < 7; i++)
            {
                sColumn = "C" + i.ToString();
                dtNCL2.Columns.Add(sColumn);
            }
            DataRow drN2 = dtNCL2.NewRow();

            foreach (DataRow dr in dtNCL.Rows)
            {
                dtNCL2.Rows.Add(drN2);
                drN2 = dtNCL2.NewRow();
                for (iCs = 1; iCs < iMax; iCs++)
                {
                    if (iCs < 4)
                    {
                        sColumnNAME = "C" + iCs.ToString();
                        drN2[sColumnNAME] = dr[sColumnNAME].ToString();
                    }
                    else if (iCs % 10 == 1 || iCs % 10 == 6)
                    {
                        dtNCL2.Rows.Add(drN2);
                        drN2 = dtNCL2.NewRow();
                    }

                    if (iCs > 5 && iCs < iMax)
                    {
                        sColumnNAME = "C" + iCs.ToString();
                        sColumnVALUE = (iCs % 10 == 0 || iCs % 10 == 5) ? "C" + (iCs % 5 + 6).ToString() : "C" + (iCs % 5 + 1).ToString();
                        drN2[sColumnVALUE] = dr[sColumnNAME].ToString();
                    }
                }
            }

            Store istore = Grid_BioNotChecked_List.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dtNCL2);
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

        protected void sendSMS_Click(object sender, EventArgs e)
        {
            int iOrn = 0;
            DateTime today = DateTime.Now;
            string sDate = today.ToString("yyyy-MM-dd");
            string sMdate = today.ToString("yyyy-MM-dd").Substring(0, 8);
            string weekType = (0 == (iOrn = today.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString();
            string sql = "";
            string pif_id = "";
            string phone = "";

            RowSelectionModel sm = this.Grid_BioNotChecked_List.GetSelectionModel() as RowSelectionModel;
            
            foreach (SelectedRow row in sm.SelectedRows) //row.RecordID = Patient Name
            {
                sql = "SELECT pif_id, pif_contact FROM pat_info WHERE pif_name='" + row.RecordID + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    pif_id = dt.Rows[0]["pif_id"].ToString();
                    phone = dt.Rows[0]["pif_contact"].ToString();
                }

                sql = "SELECT RITEM_NAME FROM bio_check_list ";
                sql += "WHERE NOT RESULT_CODE IN (SELECT RESULT_CODE FROM a_result_log WHERE result_date >= '" + sMdate + "01' and result_date <= '" + sDate + "' AND PAT_NO='" + pif_id + "') ";
                sql += "AND I_MONTH=1 ";
                dt = db.Query(sql);
                string code="";
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    code += dt.Rows[i]["RITEM_NAME"].ToString() + ", ";
                }
                dt.Dispose();
                code = row.RecordID + "你好: 本月还有" + code + "项目未检验";
                sql = "INSERT sms (mobile, content, senddate) VALUES('" + phone + "','" + code + "','" + today.ToString("yyyy-MM-dd HH:mm:ss") + "')";
                db.Excute(sql);
                Common._NotificationShow(row.RecordID + "(已排程發送中)");
            }
        }
    }
}