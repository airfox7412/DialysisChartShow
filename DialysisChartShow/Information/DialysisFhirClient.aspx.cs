using System;
using Ext.Net;
using System.Data;
using System.Xml.Xsl;
using System.Xml;
using NLog;
using System.Linq;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using Dialysis_Chart_Show.Fhir;
using System.Collections.Concurrent;
using Hl7.Fhir.Model;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Information
{
    public partial class DialysisFhirClient : BaseForm
    {
        string sYEAR, sMonth, sType;
        private static Logger logger = LogManager.GetCurrentClassLogger();
        DialysisClientUpLoad DialysisToFhir = new DialysisClientUpLoad();
        Dialysis_Chart_Show.Fhir.UploadAdminALL uploadAdminAll = new UploadAdminALL();
        Dialysis_Chart_Show.Fhir.UploadMedicationALL UploadMedicationALL = new Dialysis_Chart_Show.Fhir.UploadMedicationALL();
        DataTable dtFhir = new DataTable();
        DataTable dtrFhir = new DataTable();
        System.Data.DataView dvFhir;
        DataRow drN;
        // 加入病患基本資料及醫事人員 DataTable
        //DataTable dtPatient = new DataTable();
        //DataTable dtPractitioner = new DataTable();
        //DataTable dtMedication = new DataTable();
        //-----------------------
        private int iiCNT = 15;
        // 加入月份描述物件 - Added by Evan 20160826
        private List<MonthDesc> monthDescList = null;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                initYearItems();
                initMonthDesc();
                initialAuditList();
                getAuditList();
                BuildTree1(TreePanel1.Root);
            }
        }

        public string Trdate
        {
            get { return sYEAR + "-" + sMonth; }
        }

        protected void AuditList_Refresh(object sender, StoreReadDataEventArgs e)
        {
            getAuditList();
            BuildTree1(TreePanel1.Root);
        }

        protected void initialAuditList()
        {
            DateTime dEND = DateTime.Now.AddMonths(-1);
            sYEAR = dEND.ToString("yyyy");
            sMonth = dEND.ToString("MM");
            cboYEAR.Value = sYEAR;
            cboYEAR.Text = sYEAR;
            ComboBoxChange(sYEAR);
            cboMonth.Value = sMonth;
            cboType.Value = sType = "ALL";
            //cboType.Value = sType = "医嘱用药";
            cboType.Text = sType;
        }

        #region initYearItems() - 自動加入年份
        protected void initYearItems()
        {
            DateTime nowDate = DateTime.Now;
            DataTable dtYear = new DataTable();
            dtYear.Columns.Add("ITEM_NAME");
            dtYear.Columns.Add("ITEM_VALUE");
            int startYear = nowDate.Year - 5;
            for (int i = startYear; i <= nowDate.Year; i++)
            {
                drN = dtYear.NewRow();
                drN["ITEM_NAME"] = i.ToString();
                drN["ITEM_VALUE"] = i.ToString();
                dtYear.Rows.Add(drN);
            }
            SetComboBoxItem(cboYEAR, dtYear, false, "ITEM_NAME", "ITEM_VALUE");
            cboYEAR.Text = nowDate.Year.ToString();
        }
        #endregion

        #region initMonthDesc() - 使用月份描述物件
        // 使用月份描述物件 - Added by Evan 20160826
        protected void initMonthDesc()
        {
            if (monthDescList != null)
                return;
            monthDescList = new List<MonthDesc>();
            MonthDesc month0 = new MonthDesc(); month0.Numble = "01"; month0.NameCht = "一月"; monthDescList.Add(month0);
            MonthDesc month1 = new MonthDesc(); month1.Numble = "02"; month1.NameCht = "二月"; monthDescList.Add(month1);
            MonthDesc month2 = new MonthDesc(); month2.Numble = "03"; month2.NameCht = "三月"; monthDescList.Add(month2);
            MonthDesc month3 = new MonthDesc(); month3.Numble = "04"; month3.NameCht = "四月"; monthDescList.Add(month3);
            MonthDesc month4 = new MonthDesc(); month4.Numble = "05"; month4.NameCht = "五月"; monthDescList.Add(month4);
            MonthDesc month5 = new MonthDesc(); month5.Numble = "06"; month5.NameCht = "六月"; monthDescList.Add(month5);
            MonthDesc month6 = new MonthDesc(); month6.Numble = "07"; month6.NameCht = "七月"; monthDescList.Add(month6);
            MonthDesc month7 = new MonthDesc(); month7.Numble = "08"; month7.NameCht = "八月"; monthDescList.Add(month7);
            MonthDesc month8 = new MonthDesc(); month8.Numble = "09"; month8.NameCht = "九月"; monthDescList.Add(month8);
            MonthDesc month9 = new MonthDesc(); month9.Numble = "10"; month9.NameCht = "十月"; monthDescList.Add(month9);
            MonthDesc month10 = new MonthDesc(); month10.Numble = "11"; month10.NameCht = "十一月"; monthDescList.Add(month10);
            MonthDesc month11 = new MonthDesc(); month11.Numble = "12"; month11.NameCht = "十二月"; monthDescList.Add(month11);
        }
        #endregion

        #region setDataTableColumn() - 設定資料表欄位
        protected void setDataTableColumn()
        {
                string sColumn;
                for (int i = 1; i < 7; i++)
                {
                    sColumn = "C" + i.ToString();
                    dtFhir.Columns.Add(sColumn);
                    //dtPatient.Columns.Add(sColumn);
                    //dtPractitioner.Columns.Add(sColumn);
                }
        }
        #endregion

        private object[] GetDataArray_AddRowNum(DataTable dt)
        {
            object[] objx = new object[dt.Rows.Count];
            int i = 0;

            foreach (DataRow irow in dt.Rows)
            {
                object[] objy = new object[5];
                objy[0] = i + 1;
                for (int j = 0; j < 4; j++)
                {
                    objy[j + 1] = irow[j];
                }

                objx[i] = objy;
                i++;
            }
            return objx;
        }

        protected void getAuditList()
        {
            if (!(GetAuditEvent(DialysisToFhir.ReadFhirID())))
            {
                drN = dtFhir.NewRow();
                drN["C1"] = "";
                drN["C2"] = "无资料";
                drN["C3"] = "";
                drN["C4"] = "";
                drN["C5"] = "";
                dtFhir.Rows.Add(drN);
            }
            dvFhir = dtFhir.DefaultView;
            string sMdate = DateTime.Now.ToString("yyyyMM");
            dvFhir.Sort = "C1 DESC";
            Store istoreGridList = this.Grid_List.GetStore();
            istoreGridList.DataSource = GetDataArray_AddRowNum(dvFhir.ToTable());
            istoreGridList.DataBind();
        }

        protected bool GetAuditEvent(string sFHIR_Id)
        {
            string sURL = "";
            string sFHIR_SERVER = DialysisToFhir.ReadAuditURI();
            logger.Trace("Read Fhir Duration Organization : " + sFHIR_Id);
            // 測試用, 如果沒得到機構ID硬塞入117醫院的機構ID - Added by Evan 20160826
            //if (sFHIR_Id.Equals("nodata"))
            //    sFHIR_Id = "DC87348500.8688";
            // --------------------
            setDataTableColumn();

            string[] sURLs =
            {
                sFHIR_SERVER + "UL.PD." + sFHIR_Id, 
                sFHIR_SERVER + "UL.DDD." + sFHIR_Id, 
                sFHIR_SERVER + "UL.MR." + sFHIR_Id, 
                sFHIR_SERVER + "UL.DQ." + sFHIR_Id,
                sFHIR_SERVER + "UL.PATIENT." + sFHIR_Id + "/_history/",
                sFHIR_SERVER + "UL.PRACTITIONER." + sFHIR_Id + "/_history/",
                sFHIR_SERVER + "UL.MEDICATION." + sFHIR_Id + "/_history/",
            };

            for (int i = 0; i < sURLs.Length; i++)
            {
                sURL = sURLs[i];
                GetAEinfo(sURL);
            }

            if (dtFhir.Rows.Count <= 0)
                return false;
            return true;
        }

        private void GetAEinfo(string sURL)
        {
            string AuditEventInfo;
            string sUploadDateTime;
            string sName;
            string sReference;
            string sMonthReport;
            DateTime sMdate = DateTime.Now.AddMonths(-1);
            AuditEventInfo = DialysisToFhir.GetAuditEvent(sURL);
            if (AuditEventInfo == "-1") 
                return;

            //string auditEventType = AuditEventInfo.Substring(49, 2);
            //string historyType = sURL.Substring(69, 7);
            string auditEventType = sURL.Substring(69, 3);
            int urlLength = sURL.Length;
            if (
                auditEventType.Equals("PD.") ||
                auditEventType.Equals("DDD") ||
                auditEventType.Equals("MR.") ||
                auditEventType.Equals("DQ.") 
               )
            {
                ClassAuditEvent oAE = JsonConvert.DeserializeObject<ClassAuditEvent>(AuditEventInfo);
                var q = from p in oAE.@object
                    select p;
                foreach (var ae in q)
                {
                    sReference = ae.reference.reference.ToString();
                    if (sReference.Substring(0, 13).Equals("Communication"))
                    {
                        drN = dtFhir.NewRow();
                        sUploadDateTime = ae.identifier.value.ToString();
                        sName = ae.name.ToString();
                        sMonthReport = sName.Substring(0, 6) + "." + sReference.Substring(14, 3);
                        drN["C1"] = sUploadDateTime.Substring(0, 4) + "-" + sUploadDateTime.Substring(4, 2) + "-" + 
                                    sUploadDateTime.Substring(6, 2) + " " + sUploadDateTime.Substring(8, 2) + ":" + 
                                    sUploadDateTime.Substring(10, 2) + ":" + sUploadDateTime.Substring(12, 2);
                        drN["C2"] = sName.Substring(0, 4) + "年" + sName.Substring(4, 2) + "月";
                        drN["C3"] = sName.Substring(6);
                        drN["C4"] = "上传成功";
                        drN["C5"] = sReference;
                        drN["C6"] = sMonthReport;       //sName.Substring(0, 6) + "." + sReference.Substring(14, 3);
                        //strtmp = sName.Substring(0, 4) + "/" + sName.Substring(4, 2) + "/28";
                        //if (Convert.ToDateTime(strtmp) >= sMdate) { drN["C4"] = "成功"; } else { drN["C4"] = "待傳"; };
                        dtFhir.Rows.Add(drN);
                        logger.Trace("identifier : " + ae.identifier.value.ToString());
                        logger.Trace("reference : " + ae.reference.reference.ToString());
                        logger.Trace("name : " + ae.name.ToString());
                    }
                }
            }
            else if (auditEventType.Equals("PAT"))
            {
                // 顯示病患基本數據上傳紀錄
                ClassAuditEventHistory aeh = JsonConvert.DeserializeObject<ClassAuditEventHistory>(AuditEventInfo);
                var q = from p in aeh.entry
                    select p;
                foreach (var en in q)
                {
                    string versionID = en.resource.meta.versionId.ToString();
                    string lastUpdated = en.resource.meta.lastUpdated.ToString();
                    //drN = dtPatient.NewRow();
                    drN = dtFhir.NewRow();
                    drN["C1"] = lastUpdated.Substring(0, 10) + " " + lastUpdated.Substring(11, 8);
                    drN["C2"] = lastUpdated.Substring(0, 4) + "年" + lastUpdated.Substring(5, 2) + "月" + lastUpdated.Substring(8, 2) + "日";
                    drN["C3"] = "病患基本数据";
                    drN["C4"] = "上传成功";
                    drN["C5"] = "Patient";
                    drN["C6"] = lastUpdated.Substring(0, 4) + lastUpdated.Substring(5, 2) + ".PAT";
                    //dtPatient.Rows.Add(drN);
                    dtFhir.Rows.Add(drN);
                }
            }
            else if (auditEventType.Equals("PRA"))
            {
                // 顯示醫事人員上傳紀錄
                ClassAuditEventHistory aeh = JsonConvert.DeserializeObject<ClassAuditEventHistory>(AuditEventInfo);
                var q = from p in aeh.entry
                    select p;
                foreach (var en in q)
                {
                    string versionID = en.resource.meta.versionId.ToString();
                    string lastUpdated = en.resource.meta.lastUpdated.ToString();
                    //drN = dtPractitioner.NewRow();
                    drN = dtFhir.NewRow();
                    drN["C1"] = lastUpdated.Substring(0, 10) + " " + lastUpdated.Substring(11, 8);
                    drN["C2"] = lastUpdated.Substring(0, 4) + "年" + lastUpdated.Substring(5, 2) + "月" + lastUpdated.Substring(8, 2) + "日";
                    drN["C3"] = "医事人员";
                    drN["C4"] = "上传成功";
                    drN["C5"] = "Practitioner";
                    drN["C6"] = lastUpdated.Substring(0, 4) + lastUpdated.Substring(5, 2) + ".PRA";
                    //dtPractitioner.Rows.Add(drN);
                    dtFhir.Rows.Add(drN);
                }
            }
            else if (auditEventType.Equals("MED"))
            {
                // 顯示醫囑用藥上傳紀錄
                ClassAuditEventHistory aeh = JsonConvert.DeserializeObject<ClassAuditEventHistory>(AuditEventInfo);
                var q = from p in aeh.entry
                        select p;
                foreach (var en in q)
                {
                    string versionID = en.resource.meta.versionId.ToString();
                    string lastUpdated = en.resource.meta.lastUpdated.ToString();
                    //drN = dtPractitioner.NewRow();
                    drN = dtFhir.NewRow();
                    drN["C1"] = lastUpdated.Substring(0, 10) + " " + lastUpdated.Substring(11, 8);
                    drN["C2"] = lastUpdated.Substring(0, 4) + "年" + lastUpdated.Substring(5, 2) + "月" + lastUpdated.Substring(8, 2) + "日";
                    drN["C3"] = "医嘱用药";
                    drN["C4"] = "上传成功";
                    drN["C5"] = "Medication";
                    drN["C6"] = lastUpdated.Substring(0, 4) + lastUpdated.Substring(5, 2) + ".MED";
                    //dtPractitioner.Rows.Add(drN);
                    dtFhir.Rows.Add(drN);
                }
            }
        }

        protected void BtnSend_Click(object sender, DirectEventArgs e)
        {
            // 參數 isBatchUpload 可開關批次上傳功能 - Added by Evan 20160831
            bool isBatchUpload = true;
            initMonthDesc();
            logger.Info("step1 : BtnSend begin");
            sYEAR = GetComboBoxValue(cboYEAR);
            sMonth = GetComboBoxValue(cboMonth);
            sType = GetComboBoxValue(cboType);
            // 如把 isBatchUpload 設為 false, 則可先將批次上傳關掉 - Added by Evan 20160831
            if (this.chk_01.Checked && isBatchUpload)
            {
                DateTime dBeg = new DateTime(Convert.ToInt16(sYEAR), 01, 01);
                DateTime dEnd = new DateTime(Convert.ToInt16(sYEAR), Convert.ToInt16(sMonth), DateTime.DaysInMonth(Convert.ToInt16(sYEAR), Convert.ToInt16(sMonth)));
                DateTime comdate = dEnd;
                getAuditList();
                dvFhir = dtFhir.DefaultView;
                while (dBeg.CompareTo(comdate) < 0)
                {
                    _NotificationShow(comdate.Month.ToString() + "月份处理中...");
                    LabelMonth.Text = " " + comdate.Month.ToString() + "月份处理中...";

                    dvFhir.RowFilter = "C6='" + comdate.ToString("yyyyMM") + ".PD.'";
                    if (dvFhir.Count <= 0)
                    {
                        if (DialysisToFhir.UploadPD(comdate.ToString("yyyy-MM")))
                            _NotificationShow(comdate.ToString("yyyy-MM") + "人口分布 信息上传完成");
                        else
                            _NotificationShow(comdate.ToString("yyyy-MM") + "人口分布 信息上传失败");
                    }
                    dvFhir.RowFilter = "C6='" + comdate.ToString("yyyyMM") + ".DDD'";
                    if (dvFhir.Count <= 0)
                    {
                        if (DialysisToFhir.UploadDD(comdate.ToString("yyyy-MM")))
                            _NotificationShow(comdate.ToString("yyyy-MM") + "血透年限分布 信息上传完成");
                        else
                            _NotificationShow(comdate.ToString("yyyy-MM") + "血透年限分布 信息上传失败");
                    }
                    dvFhir.RowFilter = "C6='" + comdate.ToString("yyyyMM") + ".MR.'";
                    if (dvFhir.Count <= 0)
                    {
                        if (DialysisToFhir.UploadMR(comdate.ToString("yyyy-MM")))
                            _NotificationShow(comdate.ToString("yyyy-MM") + "死亡率 信息上传完成");
                        else
                            _NotificationShow(comdate.ToString("yyyy-MM") + "死亡率 信息上传失败");
                    }
                    dvFhir.RowFilter = "C6='" + comdate.ToString("yyyyMM") + ".DQ.'";
                    if (dvFhir.Count <= 0)
                    {
                        if (DialysisToFhir.UploadDQ(comdate.ToString("yyyy-MM")))
                            _NotificationShow(comdate.ToString("yyyy-MM") + "血液透析品质 信息上传完成");
                        else
                            _NotificationShow(comdate.ToString("yyyy-MM") + "血液透析品质 信息上传失败");
                    }
                    comdate = comdate.AddMonths(-1);
                };
            }
            else
            {
                // 此函式只上傳上月統計資料 - Remarked by Evan 20160906
                UploadFhirData(sType);
                getAuditList();
            }
            LabelMonth.Text = " 处理完成...";
            BuildTree1(TreePanel1.Root);
        }

        protected void UploadFhirData(string sType)
        {
            switch (sType)
            {
                case "人口分布":
                    if (DialysisToFhir.UploadPD(Trdate))
                        _NotificationShow("人口分布 信息上传完成");
                    else
                        _NotificationShow("人口分布 信息上传失败");
                    break;
                case "血透年限分布":
                    if (DialysisToFhir.UploadDD(Trdate))
                        _NotificationShow("血透年限分布 信息上传完成");
                    else
                        _NotificationShow("血透年限分布 信息上传失败");
                    break;
                case "死亡率":
                    if (DialysisToFhir.UploadMR(Trdate))
                        _NotificationShow("死亡率 信息上传完成");
                    else
                        _NotificationShow("死亡率 信息上传失败");
                    break;
                case "血液透析品质":
                    if (DialysisToFhir.UploadDQ(Trdate))
                        _NotificationShow("血液透析品质 信息上传完成");
                    else
                        _NotificationShow("血液透析品质 信息上传失败");
                    break;
                case "医事人员":
                    if (uploadAdminAll.UploadPractitioner(Trdate))
                        _NotificationShow("医事人员 信息上传完成");
                    else
                        _NotificationShow("医事人员 信息上传失败");
                    break;
                case "病患基本数据":
                    if (uploadAdminAll.UploadPatient(Trdate))
                        _NotificationShow("病患基本数据 信息上传完成");
                    else
                        _NotificationShow("病患基本数据 信息上传失败");
                    break;
                case "医嘱用药":
                    //if (UploadMedicationALL.UploadMedicationTwo(Trdate))
                    if (UploadMedicationALL.UploadMedicationNew(Trdate))
                        _NotificationShow("医嘱用药 信息上传完成");
                    else
                        _NotificationShow("医嘱用药 信息上传失败");
                    break;
                default:
                    logger.Info("step5 exec 人口分布: begin");
                    if (DialysisToFhir.UploadPD(Trdate))
                        _NotificationShow("人口分布 信息上传完成");
                    else
                        _NotificationShow("人口分布 信息上传失败");
                    logger.Info("step5 exec 人口分布: end");
                    logger.Info("step6 exec 血透年限分布: begin");
                    if (DialysisToFhir.UploadDD(Trdate))
                        _NotificationShow("血透年限分布 信息上传完成");
                    else
                        _NotificationShow("血透年限分布 信息上传失败");
                    logger.Info("step6 exec 血透年限分布: end");
                    logger.Info("step7 exec 死亡率: begin");
                    if (DialysisToFhir.UploadMR(Trdate))
                        _NotificationShow("死亡率 信息上传完成");
                    else
                        _NotificationShow("死亡率 信息上传失败");
                    logger.Info("step7 exec 死亡率: end");
                    logger.Info("step8 exec 血液透析品质: begin");
                    if (DialysisToFhir.UploadDQ(Trdate))
                        _NotificationShow("血液透析品质 信息上传完成");
                    else
                        _NotificationShow("血液透析品质 信息上传失败");
                    logger.Info("step8 exec 血液透析品质: end");
                    logger.Info("step9 switch: end");
                    break;
            }
        }

        protected void chk_01_Event(object sender, DirectEventArgs e)
        {
            //return;
            //if (this.chk_01.Checked)
            //{
            //    this.chk_02.Checked = true;
            //    this.PanelU.Hidden = true;
            //    this.PanelV.Hidden = false;
            //    this.BegYear.Value = this.cboYEAR.Value;
            //    this.BegYear.Text = this.cboYEAR.Text;
            //}
        }

        protected void chk_02_Event(object sender, DirectEventArgs e)
        {
            //if (!this.chk_02.Checked)
            //{
            //    this.chk_01.Checked = false;
            //    this.PanelU.Hidden = false;
            //    this.PanelV.Hidden = true;
            //    this.cboYEAR.Value = this.BegYear.Value;
            //    this.cboYEAR.Text = this.BegYear.Text;
            //}
        }

        protected void ChangeGroup(object sender, DirectEventArgs e)
        {
            ComboBoxChange(GetComboBoxValue(this.cboYEAR));
        }

        protected void ComboBoxChange(string sSelectedYEAR)
        {
            DateTime nowDate = DateTime.Now;
            // 加入顯示月份隨日期變化，當年度的顯示月份最多顯示到前一個月份 - Added by Evan 20160826
            initMonthDesc();
            DataTable dtMonth = new DataTable();
            dtMonth.Columns.Add("ITEM_NAME");
            dtMonth.Columns.Add("ITEM_VALUE");

            if (sSelectedYEAR.Equals(nowDate.Year.ToString()))
            {
                for (int i = 0; i < nowDate.Month - 1; i++)
                {
                    drN = dtMonth.NewRow();
                    drN["ITEM_NAME"] = monthDescList[i].NameCht;
                    drN["ITEM_VALUE"] = monthDescList[i].Numble;
                    dtMonth.Rows.Add(drN);
                }
            }
            else
            {
                for (int i = 0; i < monthDescList.Count; i++)
                {
                    drN = dtMonth.NewRow();
                    drN["ITEM_NAME"] = monthDescList[i].NameCht;
                    drN["ITEM_VALUE"] = monthDescList[i].Numble;
                    dtMonth.Rows.Add(drN);
                }
            }

            SetComboBoxItem(cboMonth, dtMonth, false, "ITEM_NAME", "ITEM_VALUE");
            cboMonth.Select(0);
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

        private DataTable tranT(DataTable dt)
        {
            return dt;
        }

        #region 取得樹狀目錄1
        private Ext.Net.NodeCollection BuildTree1(Ext.Net.NodeCollection nodes)
        {
            Ext.Net.Button btnExpand = new Ext.Net.Button();
            btnExpand.Text = "展开";
            btnExpand.Listeners.Click.Handler = TreePanel1.ClientID + ".expandAll();";

            Ext.Net.Button btnCollapse = new Ext.Net.Button();
            btnCollapse.Text = "收合";
            btnCollapse.Listeners.Click.Handler = TreePanel1.ClientID + ".collapseAll();";

            Ext.Net.Button btnCount = new Ext.Net.Button();
            btnCount.Text = "前" + iiCNT.ToString();
            btnCount.Click += btnCount_Click;
            btnCount.Visible = false;
            btnCount.AutoPostBack = true;

            Toolbar toolBar = new Toolbar(); //Tree Head 按鈕
            toolBar.ID = "Toolbar";
            toolBar.Items.Add(btnExpand);
            toolBar.Items.Add(btnCollapse);
            toolBar.Items.Add(btnCount);
            TreePanel1.TopBar.Add(toolBar);

            Ext.Net.Node root = new Ext.Net.Node();
            root.Text = "PD.";
            root.NodeID = "__";
            root.Cls = "large-font";
            root.Expanded = true;
            //root.Expanded = false;
            nodes.Add(root);
            int iii = 0;
            // 加入人口分布節點
            dvFhir.RowFilter = "C5 like '%PD%'";
            dvFhir.Sort = "C2 DESC";
            iii = dvFhir.Count;
            Ext.Net.Node groupNode = new Ext.Net.Node()
            {
                Text = "人口分布 (" + iii.ToString() +")",
                Icon = Icon.ReportUser,
                NodeID = "PD" + "_",
                Cls = "large-font"
            };
            root.Children.Add(groupNode);
            AddChild(groupNode, "PD", "PD", "PD", iiCNT, dvFhir.ToTable());
            groupNode.Expanded = false;
            // 加入血透年限分布節點
            dvFhir.RowFilter = "C5 like '%DDD%'";
            dvFhir.Sort = "C2 DESC";
            iii = dvFhir.Count;
            groupNode = new Ext.Net.Node()
            {
                Text = "血透年限分布 (" + iii.ToString() + ")",
                Icon = Icon.ReportUser,
                NodeID = "DDD" + "_",
                Cls = "large-font"
            };
            root.Children.Add(groupNode);
            AddChild(groupNode, "DDD", "DDD", "DDD", iiCNT, dvFhir.ToTable());
            groupNode.Expanded = false;
            // 加入死亡率節點
            dvFhir.RowFilter = "C5 like '%MR%'";
            dvFhir.Sort = "C2 DESC";
            iii = dvFhir.Count;
            groupNode = new Ext.Net.Node()
            {
                Text = "死亡率 (" + iii.ToString() + ")",
                Icon = Icon.ReportUser,
                NodeID = "MR" + "_",
                Cls = "large-font"
            };
            root.Children.Add(groupNode);
            AddChild(groupNode, "MR", "MR", "MR", iiCNT, dvFhir.ToTable());
            groupNode.Expanded = false;
            // 加入血透品質節點
            dvFhir.RowFilter = "C5 like '%DQ%'";
            dvFhir.Sort = "C2 DESC";
            iii = dvFhir.Count;
            groupNode = new Ext.Net.Node()
            {
                Text = "血液透析品质 (" + iii.ToString() + ")",
                Icon = Icon.ReportUser,
                NodeID = "DQ" + "_",
                Cls = "large-font"
            };
            root.Children.Add(groupNode);
            AddChild(groupNode, "DQ", "DQ", "DQ", iiCNT, dvFhir.ToTable());
            groupNode.Expanded = false;
            // 加入医事人员節點
            dvFhir.RowFilter = "C5 like '%Practitioner%'";
            dvFhir.Sort = "C2 DESC";
            iii = dvFhir.Count;
            groupNode = new Ext.Net.Node()
            {
                Text = "医事人员 (" + iii.ToString() + ")",
                Icon = Icon.ReportUser,
                NodeID = "PR" + "_",
                Cls = "large-font"
            };
            root.Children.Add(groupNode);
            //AddChild(groupNode, "PR", "PR", "PR", dtPractitioner.Columns.Count, dtPractitioner);
            AddChild(groupNode, "PR", "PR", "PR", iiCNT, dvFhir.ToTable());
            groupNode.Expanded = false;
            // 加入病患基本数据節點
            dvFhir.RowFilter = "C5 like '%Patient%'";
            dvFhir.Sort = "C2 DESC";
            iii = dvFhir.Count;
            groupNode = new Ext.Net.Node()
            {
                Text = "病患基本数据 (" + iii.ToString() + ")",
                Icon = Icon.ReportUser,
                NodeID = "PI" + "_",
                Cls = "large-font"
            };
            root.Children.Add(groupNode);
            //AddChild(groupNode, "PI", "PI", "PI", dtPatient.Columns.Count, dtPatient);
            AddChild(groupNode, "PI", "PI", "PI", iiCNT, dvFhir.ToTable());
            groupNode.Expanded = false;
            // 加入医嘱用药節點
            dvFhir.RowFilter = "C5 like '%Medication%'";
            dvFhir.Sort = "C2 DESC";
            iii = dvFhir.Count;
            groupNode = new Ext.Net.Node()
            {
                Text = "医嘱用药 (" + iii.ToString() + ")",
                Icon = Icon.ReportUser,
                NodeID = "MO" + "_",
                Cls = "large-font"
            };
            root.Children.Add(groupNode);
            //AddChild(groupNode, "PI", "PI", "PI", dtPatient.Columns.Count, dtPatient);
            AddChild(groupNode, "MO", "MO", "MO", iiCNT, dvFhir.ToTable());
            groupNode.Expanded = false;
            //--------------------
            TreePanel1.Render();
            TreePanel1.Expand(false);
            return nodes;
        }
        #endregion

        #region 建立樹狀目錄的子明細
        private void AddChild(Ext.Net.Node nn, string ss, string st, string sg, int ii, DataTable dt)
        {
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    Ext.Net.Node cnode = null;
                    if (ss.Equals("PD") || ss.Equals("DDD") || ss.Equals("MR") || ss.Equals("DQ"))
                    {
                        cnode = new Ext.Net.Node()
                        {
                            Text = dt.Rows[i]["C2"].ToString(),
                            NodeID = ss + i.ToString(),
                            Icon = Icon.Page,
                            Cls = "blue-large-font",
                            Leaf = true
                        };
                    }
                    else
                    {
                        cnode = new Ext.Net.Node()
                        {
                            Text = dt.Rows[i]["C1"].ToString(),
                            NodeID = ss + i.ToString(),
                            Icon = Icon.Page,
                            Cls = "blue-large-font",
                            Leaf = true
                        };
                    }
                    nn.Children.Add(cnode);
                }
            }
            else
            {
                Ext.Net.Node cnode = new Ext.Net.Node()
                {
                    Text = "无检验纪录",
                    NodeID = ss + "_" + "NEW",
                    Icon = Icon.PageWhite,
                    Cls = "blue-large-font",
                    Leaf = true
                };
                nn.Children.Add(cnode);
            }
            nn.Expanded = true;
        }
        #endregion

        #region 切換顯示日期統計筆數 TreePanel上的按鈕，顯示全部或是最近15筆
        protected void btnCount_Click(object sender, EventArgs e)
        {
            Ext.Net.Button btnCount = (Ext.Net.Button)sender;

            string sPAT_NO = _PAT_ID;
            //foreach (Ext.Net.Node nn in root1.Children)
            //    for (int i = nn.Children.Count - 1; i >= 0; i--)
            //        nn.Children.RemoveAt(i);

            if (btnCount.Text != "全部")
            {
                btnCount.Text = "全部";
            }
            else
            {
                btnCount.Text = "前" + iiCNT.ToString();
            }
        }
        #endregion

        #region 清空功能，目前沒使用
        protected void Empty_Click(object sender, DirectEventArgs e)
        {
            return;
        }
        #endregion

        #region 月份描述物件
        // 月份描述物件 - Added by Evan 20160829
        class MonthDesc
        {
            private string number;
            private string nameCht;
            public string Numble { get { return number; } set { number = value; } }
            public string NameCht { get { return nameCht; } set { nameCht = value; } }
        }
        #endregion
    }
}