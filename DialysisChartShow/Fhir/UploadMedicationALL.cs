using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using Dialysis_Chart_Show.tools;
using NLog;
using Dialysis_Chart_Show.Information;
using Newtonsoft.Json;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UploadMedicationALL
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //private string orgId = Cbo_Hospital.SelectedItem.Value;
        //private string orgName = Cbo_Hospital.Text;
        private string orgId = "DC32000800.8066";
        private string orgName = "南京医科大学血液净化中心";
        private static object syncHandle = new object();
        private static object syncHandle1 = new object();
        private static object syncHandle2 = new object();
        private static object syncHandlest1 = new object();
        private static object syncHandlest2 = new object();
        private static string sChoiseHospital;
        DialysisClientUpLoad DialysisToFhir = new DialysisClientUpLoad();
        Dialysis2FHIR_v13.UploadAdmin uploadAdmin = new Dialysis2FHIR_v13.UploadAdmin();
        DataTable dtFhir = new DataTable();
        DataTable dtrFhir = new DataTable();
        System.Data.DataView dvFhir; string slastUpdated = "";
        DataRow drN;

        public bool UploadMedicationTwo(string TrDate)
        {
            string[] OrganizationInfo;
            try
            {
                OrganizationInfo = DialysisToFhir.ReadFhirID01();
                if (OrganizationInfo[0] == "nodata")
                    return false;
                orgId = OrganizationInfo[0];
                orgName = OrganizationInfo[1];
            }
            catch (Exception)
            {
                return false;
            }
            DateTime lastUpdated;
            getAuditList();
            string sqlLT = "", sqlST = "";
            //try
            //{
                lastUpdated = new DateTime(Convert.ToInt16(slastUpdated.Substring(0, 4)), Convert.ToInt16(slastUpdated.Substring(5, 2)), Convert.ToInt16(slastUpdated.Substring(8, 2)));
            //    sqlLT = " select lgord_id, lgord_patic, lgord_dateord from longterm_ordermgt where lgord_dateord>='" + lastUpdated.ToString("yyyy-MM-dd") + "' group by lgord_patic, lgord_dateord";
            //    sqlST = " select shord_id, shord_patic, shord_dateord from shortterm_ordermgt where shord_dateord>='" + lastUpdated.ToString("yyyy-MM-dd") + "' group by shord_patic, shord_dateord";
            //}
            //catch (Exception)
            //{
            //    sqlLT = " select lgord_id, lgord_patic, lgord_dateord from longterm_ordermgt group by lgord_patic, lgord_dateord";
            //    sqlST = " select shord_id, shord_patic, shord_dateord from shortterm_ordermgt group by shord_patic, shord_dateord";
            //}
                sqlLT = " select lgord_id, lgord_patic, lgord_dateord from longterm_ordermgt where lgord_dateord>='" + "2016-08-01" + "' group by lgord_patic, lgord_dateord";
            UploadMedication uploadMedication = new UploadMedication();
            DBMysql db = new DBMysql();
            DataTable listMedicationDT = new DataTable();
            DataTable stListMedicationDT = new DataTable();

            try
            {
                List<AuditObject> ulClinicalList = uploadAdmin.AuditEventSearchNewestSuccessList(orgId, "UL.MEDICATION");
                List<AuditObject> updateClinicalList = ulClinicalList;
                //sql = " select lgord_id, lgord_patic, lgord_dateord from longterm_ordermgt group by lgord_patic, lgord_dateord limit 50";

                ConcurrentBag<AuditObject> successList = new ConcurrentBag<AuditObject>();
                ConcurrentBag<AuditObject> failList = new ConcurrentBag<AuditObject>();
                // 因新增 Clinical Audit Event 而搬移程式 - Added by Evan 20160910
                ConcurrentBag<AuditObject> successMedicatioinList = null;
                ConcurrentBag<AuditObject> failMedicatioinList = null;
                //prepare longterm medicationOrder data for medication upload

                listMedicationDT = db.Query(sqlLT);
                // 測試時先將檢查 "之前 AuditEvent 時間" 後才更新的機制遮蔽 - Remarked by Evan 20160910
                if (listMedicationDT.Rows.Count > 0)
                {
                    string lgord_id = "", lgord_patic = "", lgord_dateord = "", acclv_stfcode = "", pif_name = "";
                    // TODO: same patient same date but some medicationOrder by difference doc issue
                    // current assign by last medicationOrder
                    int listMedicationOrderCounter = 1;
                    AuditObject existObj = new AuditObject();
                    Stopwatch sw1 = new Stopwatch();
                    sw1.Start();
                    ////Parallel.ForEach(listMedicationDT.AsEnumerable(), row =>
                    // 測試時只執行一次, 實際使用時抓取所有資料 - Remarked by Evan 20160910
                    foreach (DataRow row in listMedicationDT.Rows)          // 抓取所有資料
                    //DataRow row = listMedicationDT.Rows[0];                 // 只執行一次
                    {
                        existObj = null;
                        try
                        {
                            existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["lgord_patic"].ToString());
                        }
                        catch (Exception)
                        {
                            existObj = null;
                        }
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime mpDateTime = new DateTime();
                        if (row["lgord_dateord"].ToString().Contains("/"))
                            mpDateTime = DateTime.ParseExact(row["lgord_dateord"].ToString().Trim(), "dd/MM/yyyy", culture);
                        else
                            mpDateTime = DateTime.ParseExact(row["lgord_dateord"].ToString().Trim(), "yyyy-MM-dd", culture);

                        DateTime lastUpdateDateTime = DateTime.Now;
                        if (existObj != null)
                        {
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            //    lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc.Substring(0, 8), "yyyyMMdd", culture);
                        }
                        else
                            existObj = new AuditObject { Name = DateTime.Now.ToString("yyyyMMddHHmmssFFF"), Reference = orgId + "." + row["lgord_patic"].ToString() };
                        //if (existObj != null && (existObj.Desc == null || mpDateTime > lastUpdateDateTime))
                        {
                            //logger.Info("Reference hit:" + existObj.Reference + "/" + existObj.Name);
                            try
                            {
                                lgord_id = row["lgord_id"].ToString();
                                lgord_patic = row["lgord_patic"].ToString();
                                lgord_dateord = row["lgord_dateord"].ToString();
                                successMedicatioinList = new ConcurrentBag<AuditObject>();
                                failMedicatioinList = new ConcurrentBag<AuditObject>();

                                DataTable medicationDT = new DataTable();
                                //prepare medicationOrder data for every listMedicationOrder
                                //medicationDT = db.Query(" select lgord_id, lgord_patic, lgord_dateord, lgord_timeord, lgord_usr1, al.acclv_stfcode, lgord_drug, lgord_actst, lgord_dtactst, lgord_usr2, lgord_comment, lgord_intake, lgord_freq, lgord_nurs, lgord_timest, lgord_medway from longterm_ordermgt lto left join access_level al  on al.acclv_fname =  lto.lgord_usr1 where lgord_patic = '" + lgord_patic + "' and lgord_dateord = '" + lgord_dateord + "' group by lgord_id");
                                medicationDT = db.Query(" select lgord_id, lgord_patic, lgord_dateord, lgord_timeord, lgord_usr1, al.acclv_stfcode, lgord_drug, lgord_actst, lgord_dtactst, lgord_usr2, lgord_comment, lgord_intake, lgord_freq, lgord_nurs, lgord_timest, lgord_medway, drg_name from longterm_ordermgt lto left join access_level al  on al.acclv_fname =  lto.lgord_usr1 left join drug_list dl  on dl.drg_code =  lto.lgord_drug where lgord_patic = '" + lgord_patic + "' and lgord_dateord = '" + lgord_dateord + "' group by lgord_id");
                                //logger.Info("In:lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ", row=" + medicationDT.Rows.Count);
                                if (medicationDT.Rows.Count > 0)
                                {
                                    string lgord_timeord = "", lgord_usr1 = "", lgord_drug = "", lgord_actst = "", lgord_dtactst = "", lgord_usr2 = "", lgord_comment = "";
                                    string lgord_intake = "", lgord_freq = "", lgord_nurs = "", lgord_timest = "", lgord_medway = "", drg_name = "";
                                    int medicationOrderCounter = 0;
                                    //Parallel.ForEach(medicationDT.AsEnumerable(), mrow =>
                                    foreach (DataRow mrow in medicationDT.Rows)
                                    {
                                        medicationOrderCounter++;
                                        logger.Info("");
                                        logger.Info("medicationOrder Create/Update:lgord_id=" + mrow["lgord_id"].ToString() + " , " + medicationOrderCounter.ToString() + "/" + medicationDT.Rows.Count.ToString());
                                        lgord_id = mrow["lgord_id"].ToString();
                                        lgord_timeord = mrow["lgord_timeord"].ToString();
                                        lgord_usr1 = mrow["lgord_usr1"].ToString();
                                        acclv_stfcode = mrow["acclv_stfcode"].ToString();
                                        lgord_drug = mrow["lgord_drug"].ToString();
                                        lgord_actst = mrow["lgord_actst"].ToString();
                                        lgord_dtactst = mrow["lgord_dtactst"].ToString();
                                        lgord_usr2 = mrow["lgord_usr2"].ToString();
                                        lgord_comment = mrow["lgord_comment"].ToString();
                                        lgord_intake = mrow["lgord_intake"].ToString();
                                        lgord_freq = mrow["lgord_freq"].ToString();
                                        lgord_nurs = mrow["lgord_nurs"].ToString();
                                        lgord_timest = mrow["lgord_timest"].ToString();
                                        lgord_medway = mrow["lgord_medway"].ToString();
                                        drg_name = mrow["drg_name"].ToString();
                                        try
                                        {
                                            AuditObject medResultAudit = uploadMedication.MedicationOrderCreateUpdate(orgId,
                                                lgord_id, lgord_patic, lgord_dateord, lgord_timeord, lgord_usr1, acclv_stfcode,
                                                lgord_drug, lgord_actst, lgord_dtactst, lgord_usr2, lgord_comment, lgord_intake,
                                                lgord_freq, lgord_nurs, lgord_medway, drg_name, medicationOrderCounter.ToString());
                                            if (medResultAudit.Success)
                                            {
                                                successMedicatioinList.Add(medResultAudit);
                                                logger.Info("Longterm MedicationOrder Create Success@:lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ", created=" + successMedicatioinList.Count.ToString());
                                            }
                                            else
                                            {
                                                failMedicatioinList.Add(medResultAudit);
                                                logger.Info("Longterm MedicationOrder Create Fail@" + "lgord_id=" + lgord_id + ",lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord);
                                            }
                                        }
                                        catch (Exception ex)
                                        {
                                            failMedicatioinList.Add(new AuditObject { Name = lgord_id, Desc = lgord_patic + "_" + lgord_dateord });
                                            logger.Error(ex.Message);
                                            logger.Error("Longterm MedicationOrder Create Fail@" + "lgord_id=" + lgord_id + ",lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord);
                                            logger.Info("Longterm MedicationOrder Create catch Fail@" + "lgord_id=" + lgord_id + ",lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord);
                                        }
                                    };
                                    //logger.Info("MedicationOrder Create/Update exec:" + listMedicationOrderCounter + "/" + listMedicationDT.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                                    //logger.Info("MedicationOrder Create/Update:" + (medicationOrderCounter - 1) + "/" + medicationDT.Rows.Count);
                                }
                                logger.Info("Out:lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ", created=" + successMedicatioinList.Count.ToString());

                                string MP_GROUP_CODE = "LTMP";
                                string MP_GROUP_NAME = "长期医嘱用药";
                                try
                                {
                                    AuditObject listMedResultAudit = uploadMedication.ListMedicationOrderCreateUpdate(orgId, lgord_dateord, acclv_stfcode,
                                         lgord_patic, pif_name, lgord_id, MP_GROUP_CODE, MP_GROUP_NAME, successMedicatioinList.ToList<AuditObject>());
                                    if (listMedResultAudit.Success)
                                    {
                                        successList.Add(listMedResultAudit);
                                        //AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                        //updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                        //logger.Info("Longterm ListMedicationOrder Create Success@" + "lgord_id=" + lgord_id + ",lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",successMedicatioinListSize=" + successMedicatioinList.Count.ToString());
                                    }
                                    else
                                    {
                                        failList.Add(listMedResultAudit);
                                        logger.Info("Longterm ListMedicationOrder Create Fail@" + "lgord_id=" + lgord_id + ",lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",successMedicatioinListSize=" + successMedicatioinList.Count.ToString());
                                    }
                                }
                                catch (Exception e)
                                {
                                    failList.Add(new AuditObject { Name = lgord_id, Desc = lgord_patic + "_" + lgord_dateord });
                                    logger.Info("Longterm ListMedicationOrder Create catch Fail@" + "lgord_id=" + lgord_id + ",lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",successMedicatioinListSize=" + successMedicatioinList.Count.ToString());
                                    //logger.Info(e.Message);
                                }
                                listMedicationOrderCounter++;
                            }
                            catch (Exception ex)
                            {
                                failList.Add(new AuditObject { Name = lgord_id, Desc = lgord_patic + "_" + lgord_dateord });
                                logger.Error(ex.Message + ex.StackTrace);
                                logger.Error("Longterm ListMedicationOrder Create Fail@" + "lgord_id=" + lgord_id + ",lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",successMedicatioinListSize=" + successMedicatioinList.Count.ToString());
                                logger.Info("Longterm ListMedicationOrder Create Fail@" + "lgord_id=" + lgord_id + ",lgord_patic=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",successMedicatioinListSize=" + successMedicatioinList.Count.ToString());
                            }
                        }
                    };
                    sw1.Stop();
                    logger.Info("Longterm ListMedicationOrder Create/Update exec:" + (listMedicationOrderCounter - 1).ToString() + "/" + listMedicationDT.Rows.Count.ToString() + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    sw1.Restart();
                    // 新增 Clinical Audit Event - Added by Evan 20160910
                    try
                    {
                        if (successMedicatioinList.Count > 0)
                        {
                            uploadAdmin.AuditEventClinicalCreateUpdate("UL.CLINICAL", lgord_patic, "", acclv_stfcode, "Patient", "List", orgId, orgId,
                                successMedicatioinList.ToList<AuditObject>(), Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.CLINICAL"),
                                HttpContext.Current.Request.UserHostAddress);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create catch Fail@" + "UL.CLINICAL");
                    }
                    //---------------------------------------------------
                    try
                    {
                        if (successList.Count > 0)
                        {
                            //uploadAdmin.SecurityEventCreateUpdate(null, "Organization", orgId, orgId, successList.ToList<AuditObject>(),
                            //Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.SecurityEvent.SecurityEventOutcome.N0,
                            //new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                            //HttpContext.Current.Request.UserHostAddress);
                            uploadAdmin.AuditEventMedicationCreateUpdate("UL.MEDICATION", orgId, "Organization", orgId, orgId,
                                successList.ToList<AuditObject>(), Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.MEDICATION"),
                                HttpContext.Current.Request.UserHostAddress);
                        }
                        //if (failList.Count > 0)
                        //    uploadAdmin.AuditEventMedicationCreateUpdate("UL.MEDICATION", orgId, "Organization", orgId, orgId,
                        //       failList.ToList<AuditObject>(), Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                        //       new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.MEDICATION"),
                        //       HttpContext.Current.Request.UserHostAddress);
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create catch Fail@" + "UL.MEDICATION");
                    }
                    sw1.Stop();
                    logger.Info("MedicationAudit Create/Update total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                }
                // 以下為短期醫囑更新上傳 - Remarked by Evan 20160912
                //ConcurrentBag<AuditObject> stSuccessList = new ConcurrentBag<AuditObject>();
                //ConcurrentBag<AuditObject> stFailList = new ConcurrentBag<AuditObject>();
                ////prepare shortterm medicationOrder data for medication upload
                //stListMedicationDT = db.Query(sqlST);
                //if (stListMedicationDT.Rows.Count > 0)
                //{
                //    string shord_id = "";
                //    string shord_patic = "";
                //    string shord_dateord = "";
                //    // TODO: same patient same date but some medicationOrder by difference doc issue
                //    // current assign by last medicationOrder
                //    string acclv_stfcode = "";
                //    int stListMedicationOrderCounter = 0;
                //    Stopwatch sw2 = new Stopwatch();
                //    sw2.Start();

                //    AuditObject existObj = null;
                //    //Parallel.ForEach(stListMedicationDT.AsEnumerable(), row =>
                //    foreach (DataRow row in stListMedicationDT.Rows)
                //    {
                //        existObj = null;
                //        try
                //        {
                //            existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["shord_patic"].ToString());
                //        }
                //        catch (Exception)
                //        {
                //            existObj = null;
                //        }
                //        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                //        DateTime mpDateTime = new DateTime();
                //        if (row["shord_dateord"].ToString().Contains("/"))
                //            mpDateTime = DateTime.ParseExact(row["shord_dateord"].ToString().Trim(), "dd/MM/yyyy", culture);
                //        else
                //            mpDateTime = DateTime.ParseExact(row["shord_dateord"].ToString().Trim(), "yyyy-MM-dd", culture);

                //        DateTime lastUpdateDateTime = new DateTime();
                //        if (existObj != null)
                //            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                //                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);

                //        //if (existObj != null && (existObj.Desc == null || mpDateTime > lastUpdateDateTime))
                //        {
                //            //logger.Info("Reference hit:" + existObj.Reference + "/" + existObj.Name);

                //            ConcurrentBag<AuditObject> stSuccessMedicatioinList = null;
                //            ConcurrentBag<AuditObject> stFailMedicatioinList = null;
                //            try
                //            {
                //                shord_id = row["shord_id"].ToString();
                //                shord_patic = row["shord_patic"].ToString();
                //                shord_dateord = row["shord_dateord"].ToString();
                //                stSuccessMedicatioinList = new ConcurrentBag<AuditObject>();
                //                stFailMedicatioinList = new ConcurrentBag<AuditObject>();

                //                DataTable stMedicationDT = new DataTable();
                //                //prepare medicationOrder data for every listMedicationOrder
                //                stMedicationDT = db.Query(" select shord_id, shord_patic, shord_dateord, shord_timeord, shord_usr1, al.acclv_stfcode, shord_drug, shord_actst, shord_dtactst, shord_usr2, shord_comment, shord_intake, shord_freq, shord_nurs, shord_medway, drg_name from shortterm_ordermgt sto left join access_level al  on al.acclv_fname =  sto.shord_usr1 left join drug_list dl  on dl.drg_code =  sto.shord_drug where shord_patic = '" + shord_patic + "' and shord_dateord = '" + shord_dateord + "' group by shord_id");
                //                logger.Info("In:shord_id=" + shord_id + ",shord_dateord=" + shord_dateord + ", row=" + stMedicationDT.Rows.Count.ToString());
                //                if (stMedicationDT.Rows.Count > 0)
                //                {
                //                    string shord_timeord = "", shord_usr1 = "", shord_drug = "", shord_actst = "", shord_dtactst = "", shord_usr2 = "", shord_comment = "", shord_intake = "";
                //                    string shord_freq = "", shord_nurs = "", shord_medway = "", drg_name = "";
                //                    int stMedicationOrderCounter = 0;
                //                    //Parallel.ForEach(stMedicationDT.AsEnumerable(), mrow =>
                //                    foreach (DataRow mrow in stMedicationDT.Rows)
                //                    {
                //                        stMedicationOrderCounter++;
                //                        logger.Info("MedicationOrder Create/Update@shord_id= " + mrow["shord_id"].ToString() + " , " + stMedicationOrderCounter.ToString() + "/" + stMedicationDT.Rows.Count.ToString());
                //                        shord_id = mrow["shord_id"].ToString();
                //                        shord_timeord = mrow["shord_timeord"].ToString();
                //                        shord_usr1 = mrow["shord_usr1"].ToString();
                //                        acclv_stfcode = mrow["acclv_stfcode"].ToString();
                //                        shord_drug = mrow["shord_drug"].ToString();
                //                        shord_actst = mrow["shord_actst"].ToString();
                //                        shord_dtactst = mrow["shord_dtactst"].ToString();
                //                        shord_usr2 = mrow["shord_usr2"].ToString();
                //                        shord_comment = mrow["shord_comment"].ToString();
                //                        shord_intake = mrow["shord_intake"].ToString();
                //                        shord_freq = mrow["shord_freq"].ToString();
                //                        shord_nurs = mrow["shord_nurs"].ToString();
                //                        shord_medway = mrow["shord_medway"].ToString();
                //                        drg_name = mrow["drg_name"].ToString();
                //                        try
                //                        {
                //                            AuditObject medResultAudit = uploadMedication.MedicationOrderCreateUpdate(orgId,
                //                                shord_id, shord_patic, shord_dateord, shord_timeord, shord_usr1, acclv_stfcode,
                //                                shord_drug, shord_actst, shord_dtactst, shord_usr2, shord_comment, shord_intake,
                //                                shord_freq, shord_nurs, shord_medway, drg_name, stMedicationOrderCounter.ToString());
                //                            if (medResultAudit.Success)
                //                            {
                //                                stSuccessMedicatioinList.Add(medResultAudit);
                //                                logger.Info("Shortterm MedicationOrder Create Success@" + "shord_id=" + shord_id + ",shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord);
                //                            }
                //                            else
                //                            {
                //                                stFailMedicatioinList.Add(medResultAudit);
                //                                logger.Info("Shortterm MedicationOrder Create Fail@" + "shord_id=" + shord_id + ",shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord);
                //                            }
                //                        }
                //                        catch (Exception ex)
                //                        {
                //                            stFailMedicatioinList.Add(new AuditObject { Name = shord_id, Desc = shord_patic + "_" + shord_dateord });
                //                            logger.Error(ex.Message);
                //                            logger.Error("Shortterm MedicationOrder Create Fail@" + "shord_id=" + shord_id + ",shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord);
                //                            logger.Info("Shortterm MedicationOrder Create catch Fail@" + "shord_id=" + shord_id + ",shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord);
                //                        }
                //                    };
                //                    //logger.Info("MedicationOrder Create/Update exec:" + listMedicationOrderCounter + "/" + listMedicationDT.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                //                    //logger.Info("MedicationOrder Create/Update:" + (medicationOrderCounter - 1) + "/" + medicationDT.Rows.Count);
                //                }
                //                logger.Info("Out:shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord + ", created=" + stSuccessMedicatioinList.Count.ToString());
                //                string MP_GROUP_CODE = "STMP";
                //                string MP_GROUP_NAME = "短期医嘱用药";
                //                try
                //                {
                //                    AuditObject listMedResultAudit = uploadMedication.ListMedicationOrderCreateUpdate(orgId,
                //                        shord_dateord, acclv_stfcode, shord_patic, shord_id, MP_GROUP_CODE, MP_GROUP_NAME, stSuccessMedicatioinList.ToList<AuditObject>());
                //                    if (listMedResultAudit.Success)
                //                    {
                //                        stSuccessList.Add(listMedResultAudit);
                //                        //AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                //                        //updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                //                        logger.Info("Shortterm ListMedicationOrder Create Success@" + "shord_id=" + shord_id + ",shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",stSuccessMedicatioinListSize=" + stSuccessMedicatioinList.Count);
                //                    }
                //                    else
                //                    {
                //                        stFailList.Add(listMedResultAudit);
                //                        logger.Info("Shortterm ListMedicationOrder Create False@" + "shord_id=" + shord_id + ",shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",stSuccessMedicatioinListSize=" + stSuccessMedicatioinList.Count);
                //                    }
                //                }
                //                catch (Exception)
                //                {
                //                    stFailList.Add(new AuditObject { Name = shord_id, Desc = shord_patic + "_" + shord_dateord });
                //                    logger.Info("Shortterm ListMedicationOrder Create catch False@" + "shord_id=" + shord_id + ",shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",stSuccessMedicatioinListSize=" + stSuccessMedicatioinList.Count);
                //                }
                //            }
                //            catch (Exception ex)
                //            {
                //                stFailList.Add(new AuditObject { Name = shord_id, Desc = shord_patic + "_" + shord_dateord });
                //                logger.Error(ex.Message + ex.StackTrace);
                //                logger.Error("Shortterm ListMedicationOrder Create Fail@" + "shord_id=" + shord_id + ",shord_patic=" + shord_patic + ",shord_dateord=" + shord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",stSuccessMedicatioinListSize=" + stSuccessMedicatioinList.Count);
                //            }
                //        }
                //    };
                //    //}
                //    sw2.Stop();
                //    logger.Info("Shortterm ListMedicationOrder Create/Update exec:" + (stListMedicationOrderCounter - 1) + "/" + stListMedicationDT.Rows.Count + " total cost " + (sw2.ElapsedMilliseconds / 1000).ToString() + " seconds");
                //}

                //Stopwatch sw = new Stopwatch();
                //sw.Start();
                //try
                //{
                //    if (stSuccessList.Count > 0)
                //    {
                //        /*
                //        uploadAdmin.SecurityEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                //            Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.SecurityEvent.SecurityEventOutcome.N0,
                //            new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                //            HttpContext.Current.Request.UserHostAddress);
                //        */
                //        uploadAdmin.AuditEventMedicationCreateUpdate("UL.MEDICATION", orgId, "Organization", orgId, orgId,
                //            stSuccessList.ToList<AuditObject>(), Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                //            new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.MEDICATION"),
                //            HttpContext.Current.Request.UserHostAddress);
                //    }
                //    //if (stFailList.Count > 0)
                //    //    uploadAdmin.AuditEventMedicationCreateUpdate("UL.MEDICATION", orgId, "Organization", orgId, orgId,
                //    //       stFailList.ToList<AuditObject>(), Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                //    //       new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.MEDICATION"),
                //    //       HttpContext.Current.Request.UserHostAddress);
                //}
                //catch (Exception ex)
                //{
                //    logger.Error(ex.Message);
                //    logger.Error("Audit Create catch Fail@" + "UL_CLINICAL_MEDICATION");
                //}
                //sw.Stop();
                //logger.Info("MedicationAudit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
            }
            catch (Exception uploadMedicationException)
            {
                logger.Error(uploadMedicationException.Message);
                logger.Error("UploadMedication Error:" + uploadMedicationException.Message);
                return true;
            }
            finally
            {
            }
            return true;
        }

        protected void getAuditList()
        {
            if (GetAuditEvent(orgId))
            //if (GetAuditEvent(DialysisToFhir.ReadFhirID()))
            {
                drN = dtFhir.NewRow();
                drN["C1"] = "无资料:";
                dtFhir.Rows.Add(drN);
            }
            dvFhir = dtFhir.DefaultView;
            string sMdate = DateTime.Now.ToString("yyyyMM");
            dvFhir.Sort = "C1 DESC";
        }

        protected bool GetAuditEvent(string sFHIR_Id)
        {
            string sURL = "", sFHIR_SERVER = DialysisToFhir.ReadAuditURI();
            bool bCheck = true;
            string sColumn;

            for (int i = 1; i < 7; i++)
            {
                sColumn = "C" + i.ToString();
                dtFhir.Columns.Add(sColumn);
            }

            string[] sURLs = { sFHIR_SERVER + "UL.MEDICATION." + sFHIR_Id, "" };
            //string[] sURLs = { sFHIR_SERVER + "UL.MEDICATIONORDER." + sFHIR_Id, "" };

            for (int i = 0; i < 1; i++)
            {
                sURL = sURLs[i];
                GetAEinfo(sURL);
            }
            if (dtFhir.Rows.Count > 0)
                bCheck = false;
            return bCheck;
        }

        private void GetAEinfo(string sURL)
        {
            string AuditEventInfo, sUploadDT, sName, sReference, desc;
            DateTime sMdate = DateTime.Now.AddMonths(-1);
            AuditEventInfo = DialysisToFhir.GetAuditEvent(sURL);
            if (!(AuditEventInfo == "-1"))
            {
                ClassAuditEvent oAE = JsonConvert.DeserializeObject<ClassAuditEvent>(AuditEventInfo);
                slastUpdated = oAE.meta.lastUpdated.Substring(0, 10);
                var q = from p in oAE.@object
                        select p;
                foreach (var ae in q)
                {
                    drN = dtFhir.NewRow();
                    if (ae.identifier.system == null) sUploadDT = "";
                    else sUploadDT = ae.identifier.system.ToString();
                    sName = ae.name.ToString();
                    sReference = oAE.@event.type.code.ToString() + sName;
                    sReference = ae.reference.reference.ToString();
                    desc = ae.description.ToString();
                    drN["C1"] = sUploadDT;
                    drN["C2"] = sName;
                    drN["C3"] = desc;
                    drN["C4"] = "上传成功";
                    drN["C5"] = sReference;
                    drN["C6"] = oAE.@event.type.code.ToString() + sName;
                    //if (Convert.ToDateTime(strtmp) >= sMdate) { drN["C4"] = "成功"; } else { drN["C4"] = "待傳"; };
                    dtFhir.Rows.Add(drN);
                    logger.Trace("identifier : " + ae.identifier.value.ToString());
                    logger.Trace("reference : " + ae.reference.reference.ToString());
                    logger.Trace("name : " + ae.name.ToString());
                }
            }
        }

        public bool UploadMedicationNew(string TrDate)
        {
            string[] OrganizationInfo;
            try
            {
                OrganizationInfo = DialysisToFhir.ReadFhirID01();
                if (OrganizationInfo[0] == "nodata")
                    return false;
                orgId = OrganizationInfo[0];
                orgName = OrganizationInfo[1];
            }
            catch (Exception)
            {
                return false;
            }
            getAuditList();

            slastUpdated = "2016-09-01";
            //string sqlLTPatList = "SELECT DISTINCT lgord_patic FROM longterm_ordermgt WHERE lgord_dateord>='2016-09-01'";
            string sqlLTPatList = "SELECT DISTINCT lgord_patic FROM longterm_ordermgt WHERE lgord_dateord>='" + slastUpdated + "'";
            string sqlSTPatList = "SELECT DISTINCT shord_patic FROM shortterm_ordermgt WHERE shord_dateord>='" + slastUpdated + "'";

            //string sqlPatLTMed = "SELECT lgord_id, lgord_patic, lgord_dateord, lgord_timeord, lgord_usr1, al.acclv_stfcode, pi.pif_name, " + 
            //                        " lgord_drug, lgord_actst, lgord_dtactst, lgord_usr2, lgord_comment, lgord_intake, lgord_freq, " + 
            //                        " lgord_nurs, lgord_timest, lgord_medway, drg_name FROM longterm_ordermgt lto " +
            //                        " LEFT JOIN access_level al ON al.acclv_fname = lto.lgord_usr1 LEFT JOIN drug_list dl ON dl.drg_code = lto.lgord_drug" +
            //                        " LEFT JOIN pat_info pi ON pi.pif_ic = lto.lgord_patic" + " WHERE lgord_patic = '" + curPatic + 
            //                        "' AND lgord_dateord >= '" + slastUpdated + "' ORDER BY lgord_dateord";
            string sqlPatLTCol = "SELECT lgord_id, lgord_patic, lgord_dateord, lgord_timeord, lgord_usr1, al.acclv_stfcode, pi.pif_name, " + 
                                    " lgord_drug, lgord_actst, lgord_dtactst, lgord_usr2, lgord_comment, lgord_intake, lgord_freq, " + 
                                    " lgord_nurs, lgord_timest, lgord_medway, drg_name FROM longterm_ordermgt lto " +
                                    " LEFT JOIN access_level al ON al.acclv_fname = lto.lgord_usr1 LEFT JOIN drug_list dl ON dl.drg_code = lto.lgord_drug" +
                                    " LEFT JOIN pat_info pi ON pi.pif_ic = lto.lgord_patic" + " WHERE lgord_patic = '";
            string sqlPatSTCol = "SELECT shord_id, shord_patic, shord_dateord, shord_timeord, shord_usr1, al.acclv_stfcode, pi.pif_name, " +
                                    " shord_drug, shord_actst, shord_dtactst, shord_usr2, shord_comment, shord_intake, shord_freq, " +
                                    " shord_nurs, shord_medway, drg_name FROM shortterm_ordermgt lto " +
                                    " LEFT JOIN access_level al ON al.acclv_fname = lto.shord_usr1 LEFT JOIN drug_list dl ON dl.drg_code = lto.shord_drug" +
                                    " LEFT JOIN pat_info pi ON pi.pif_ic = lto.shord_patic" + " WHERE shord_patic = '";

            UploadMedicationOrder(sqlLTPatList, sqlPatLTCol, "lgord_patic", "lgord_dateord",
                                  "' AND lgord_dateord >= '", "' ORDER BY lgord_dateord",
                                  "lgord_patic", "lgord_dateord", "lgord_id", "lgord_timeord", "lgord_usr1", "pif_name",
                                  "acclv_stfcode", "lgord_drug", "lgord_actst", "lgord_dtactst", "lgord_usr2",
                                  "lgord_comment", "lgord_intake", "lgord_freq", "lgord_nurs",
                                  "lgord_medway", "drg_name", "LTMP", "长期医嘱用药");

            UploadMedicationOrder(sqlSTPatList, sqlPatSTCol, "shord_patic", "shord_dateord",
                                  "' AND shord_dateord >= '", "' ORDER BY shord_dateord",
                                  "shord_patic", "shord_dateord", "shord_id", "shord_timeord", "shord_usr1", "pif_name",
                                  "acclv_stfcode", "shord_drug", "shord_actst", "shord_dtactst", "shord_usr2",
                                  "shord_comment", "shord_intake", "shord_freq", "shord_nurs",
                                  "shord_medway", "drg_name", "STMP", "短期医嘱用药");
                                 
            return true;
        }

        protected void UploadMedicationOrder(string sqlPatList, string sqlPatTCol, string colNamePatIC, string colNameDateOrd, 
                                             string sqlDateOrder, string sqlDateSort, 
                                             string colNamePatic, string colNameDateord, string colNameId, string colNameTimeord, string colNameUsr1, string colNamePName, 
                                             string colNameSafecode, string colNameDrug, string colNameActst, string colNameDtactst, string colNameUsr2, 
                                             string colNameComment, string colNameIntake, string colNameFreq, string colNameNurs, 
                                             string colNameMedway, string colNameDrgname, string MP_GROUP_CODE, string MP_GROUP_NAME)
                                             
        {
            UploadMedication uploadMedication = new UploadMedication();
            DBMysql db = new DBMysql();
            // 取得不重複的患者 Patient IC 清單
            DataTable patListDataTable = db.Query(sqlPatList);
            string curPatic = "";
            string sqlPatMed = "";
            //string colNamePatIC = "lgord_patic";
            //string colNameDateOrd = "lgord_dateord";
            //string sqlDateOrder = "' AND lgord_dateord >= '";
            //string sqlDateSort = "' ORDER BY lgord_dateord";
            //string colNamePatic = "lgord_patic";
            //string colNameDateord = "lgord_dateord";
            //string colNameId = "lgord_id";
            //string colNameTimeord = "lgord_timeord";
            //string colNameUsr1 = "lgord_usr1";
            //string colNamePName = "pif_name";
            //string colNameSafecode = "acclv_stfcode";
            //string colNameDrug = "lgord_drug";
            //string colNameActst = "lgord_actst";
            //string colNameDtactst = "lgord_dtactst";
            //string colNameUsr2 = "lgord_usr2";
            //string colNameComment = "lgord_comment";
            //string colNameIntake = "lgord_intake";
            //string colNameFreq = "lgord_freq";
            //string colNameNurs = "lgord_nurs";
            //string colNameTimest = "lgord_timest";
            //string colNameMedway = "lgord_medway";
            //string colNameDrgname = "drg_name";
            //string MP_GROUP_CODE = "LTMP";
            //string MP_GROUP_NAME = "长期医嘱用药";

            // 配置紀錄所有患者的 AuditObject 容器, 用於更新 AuditEvent/UL.MEDICATIIONORDER
            ConcurrentBag<AuditObject> successClinList = new ConcurrentBag<AuditObject>();
            try
            {
                // 取得已註冊的資料清單
                List<AuditObject> ulClinicalList = uploadAdmin.AuditEventSearchNewestSuccessList(orgId, "UL.MEDICATION");  // "UL.MEDICATIONORDER"
                List<AuditObject> updateClinicalList = ulClinicalList;
                // 配置紀錄所有患者的 AuditObject 容器, 用於更新 AuditEvent/UL.CLINICAL
                ConcurrentBag<AuditObject> successPatList = null;
                ConcurrentBag<AuditObject> failPatList = null;
                ConcurrentBag<AuditObject> successMedicationList = null;
                ConcurrentBag<AuditObject> failMedicationList = null;
                // 將資料庫中逐個患者的 Medication Order 資料取出
                foreach (DataRow r in patListDataTable.Rows)
                {
                    // 配置紀錄所有患者的 AuditObject 容器, 用於更新 AuditEvent/UL.CLINICAL
                    successPatList = new ConcurrentBag<AuditObject>();
                    failPatList = new ConcurrentBag<AuditObject>();
                    // 配置紀錄單一患者的 AuditObject 容器, 用於更新 List
                    successMedicationList = new ConcurrentBag<AuditObject>();
                    failMedicationList = new ConcurrentBag<AuditObject>();
                    // 取得單一患者所有資料
                    curPatic = r[colNamePatIC].ToString();
                    sqlPatMed = sqlPatTCol + curPatic + sqlDateOrder + slastUpdated + sqlDateSort;
                    DataTable patLTMedDataTable = db.Query(sqlPatMed);
                    int medicationCountByDay = 1;
                    string curMedOrdDate = string.Empty;
                    string oldMedOrdDate = string.Empty;
                    string lgord_patic = "", lgord_dateord = "", lgord_id = "", acclv_stfcode = "", pif_name = "";
                    foreach (DataRow moRow in patLTMedDataTable.Rows)
                    {
                        curMedOrdDate = moRow[colNameDateOrd].ToString();
                        if (curMedOrdDate.Equals(oldMedOrdDate))
                            medicationCountByDay++;
                        else
                            medicationCountByDay = 1;

                        AuditObject existObj = null;
                        try
                        {
                            existObj = ulClinicalList.Find(x => (x.Reference.Split('.')[2] == moRow[colNamePatIC].ToString()));
                        }
                        catch (Exception)
                        {
                            existObj = null;
                        }
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime mpDateTime = new DateTime();
                        if (moRow[colNameDateOrd].ToString().Contains("/"))
                            mpDateTime = DateTime.ParseExact(moRow[colNameDateOrd].ToString().Trim(), "dd/MM/yyyy", culture);
                        else
                            mpDateTime = DateTime.ParseExact(moRow[colNameDateOrd].ToString().Trim(), "yyyy-MM-dd", culture);

                        DateTime lastUpdateDateTime = DateTime.Now;
                        if (existObj != null)
                        {
                            try
                            {
                                if (!String.IsNullOrWhiteSpace(existObj.Identifier))
                                    lastUpdateDateTime = DateTime.ParseExact(existObj.Identifier.Substring(0, 8), "yyyyMMdd", culture);
                            }
                            catch (Exception e)
                            {
                                //lastUpdateDateTime = 
                            }
                        }
                        else
                        {
                            existObj = new AuditObject { Name = DateTime.Now.ToString("yyyyMMddHHmmssFFF"), Reference = "Patient/" + orgId + "." + moRow[colNamePatIC].ToString() };
                        }

                        string lgord_timeord = "", lgord_usr1 = "", lgord_drug = "", lgord_actst = "", lgord_dtactst = "", lgord_usr2 = "";
                        string lgord_comment = "", lgord_intake = "", lgord_freq = "", lgord_nurs = "", lgord_medway = "", drg_name = "";
                        //string lgord_timest = ""; 
                        logger.Info("MedicationOrder Create/Update:lgord_id=" + moRow[colNameId].ToString() + " , " + medicationCountByDay.ToString() +
                                    "/" + patLTMedDataTable.Rows.Count.ToString() + " at " + mpDateTime.ToString("yyyy-MM-dd"));
                        lgord_patic = moRow[colNamePatic].ToString();
                        lgord_dateord = moRow[colNameDateord].ToString();
                        lgord_id = moRow[colNameId].ToString();
                        lgord_timeord = moRow[colNameTimeord].ToString();
                        lgord_usr1 = moRow[colNameUsr1].ToString();
                        pif_name = moRow[colNamePName].ToString();
                        acclv_stfcode = moRow[colNameSafecode].ToString();
                        lgord_drug = moRow[colNameDrug].ToString();
                        lgord_actst = moRow[colNameActst].ToString();
                        lgord_dtactst = moRow[colNameDtactst].ToString();
                        lgord_usr2 = moRow[colNameUsr2].ToString();
                        lgord_comment = moRow[colNameComment].ToString();
                        lgord_intake = moRow[colNameIntake].ToString();
                        lgord_freq = moRow[colNameFreq].ToString();
                        lgord_nurs = moRow[colNameNurs].ToString();
                        //lgord_timest = moRow[colNameTimest].ToString();
                        lgord_medway = moRow[colNameMedway].ToString();
                        drg_name = moRow[colNameDrgname].ToString();
                        // 更新 MedicationOrder Resource
                        try
                        {
                            AuditObject medResultAudit = uploadMedication.MedicationOrderCreateUpdate(orgId,
                                    lgord_id, lgord_patic, lgord_dateord, lgord_timeord, lgord_usr1, acclv_stfcode,
                                    lgord_drug, lgord_actst, lgord_dtactst, lgord_usr2, lgord_comment, lgord_intake,
                                    lgord_freq, lgord_nurs, lgord_medway, drg_name, medicationCountByDay.ToString());
                            if (medResultAudit.Success)
                            {
                                successMedicationList.Add(medResultAudit);
                                logger.Info("Longterm MedicationOrder Create Success@:lgord_patic=" + lgord_patic + "," + colNameDateOrd + "=" + 
                                            lgord_dateord + ", created=" + successMedicationList.Count.ToString());
                            }
                            else
                            {
                                failMedicationList.Add(medResultAudit);
                                logger.Info("Longterm MedicationOrder Create Fail@" + "lgord_id=" + lgord_id + ","+ colNamePatIC + "=" + 
                                            lgord_patic + ",lgord_dateord=" + lgord_dateord);
                            }
                        }
                        catch (Exception ex)
                        {
                            failMedicationList.Add(new AuditObject { Name = lgord_id, Desc = lgord_patic + "_" + lgord_dateord });
                            logger.Error(ex.Message);
                            logger.Error("Longterm MedicationOrder Create Fail@" + "lgord_id=" + lgord_id + "," + colNamePatIC + "=" + lgord_patic + "," + colNameDateOrd + "=" + lgord_dateord);
                            logger.Info("Longterm MedicationOrder Create catch Fail@" + "lgord_id=" + lgord_id + "," + colNamePatIC + "=" + lgord_patic + ",lgord_dateord=" + lgord_dateord);
                        }

                        oldMedOrdDate = curMedOrdDate;
                    }
                    // 更新 List Resource - MedicationOrder
                    try
                    {
                        AuditObject listMedResultAudit = uploadMedication.ListMedicationOrderCreateUpdate(orgId, lgord_dateord, acclv_stfcode,
                             lgord_patic, pif_name, lgord_id, MP_GROUP_CODE, MP_GROUP_NAME, successMedicationList.ToList<AuditObject>());
                        if (listMedResultAudit.Success)
                        {
                            successPatList.Add(listMedResultAudit);
                            //logger.Info("Longterm ListMedicationOrder Create Success@" + "lgord_id=" + lgord_id + "," + colNamePatIC + "=" + lgord_patic + ",lgord_dateord=" + lgord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",successMedicatioinListSize=" + successMedicatioinList.Count.ToString());
                        }
                        else
                        {
                            failPatList.Add(listMedResultAudit);
                            logger.Info("Longterm ListMedicationOrder Create Fail@" + "lgord_id=" + lgord_id + "," + colNamePatIC + "=" + lgord_patic + "," + colNameDateOrd + "=" + lgord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",successMedicationListSize=" + successMedicationList.Count.ToString());
                        }
                    }
                    catch (Exception e)
                    {
                        failPatList.Add(new AuditObject { Name = lgord_id, Desc = lgord_patic + "_" + lgord_dateord });
                        logger.Info("Longterm ListMedicationOrder Create catch Fail@" + "lgord_id=" + lgord_id + "," + colNamePatIC + "=" + lgord_patic + "," + colNameDateOrd + "=" + lgord_dateord + ",acclv_stfcode=" + acclv_stfcode + ",successMedicationListSize=" + successMedicationList.Count.ToString());
                        logger.Info(e.Message);
                    }

                    // 更新 AuditEvent/UL.CLINICAL
                    try
                    {
                        if (successPatList.Count > 0)
                        {
                            AuditObject clinicalResult = uploadAdmin.AuditEventClinicalCreateUpdate("UL.CLINICA",
                                    lgord_patic, pif_name, acclv_stfcode, "Patient", "List", orgId, orgId,
                                    successPatList.ToList<AuditObject>(), Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                    new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.CLINICAL"),
                                    HttpContext.Current.Request.UserHostAddress);
                            if (clinicalResult.Success)
                                successClinList.Add(clinicalResult);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error("Audit Create catch Fail@" + "UL.CLINICAL");
                        logger.Error(ex.Message);
                    }
                }
                // 更新 AuditEvent/MEDICATIONORDER
                try
                {
                    if (successClinList.Count > 0)
                    {
                        uploadAdmin.AuditEventMedicationCreateUpdate("UL.MEDICATION", orgId, "Organization", orgId, orgId,
                                successClinList.ToList<AuditObject>(), Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.MEDICATION"),
                                HttpContext.Current.Request.UserHostAddress);
                    }
                }
                catch (Exception ex)
                {
                    logger.Error("Audit Create catch Fail@" + "UL.MEDICATION");
                    logger.Error(ex.Message);
                }
            }
            catch (Exception e)
            {
                logger.Error(e.Message);
            }
        }
    }
}
