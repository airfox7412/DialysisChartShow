using System;
using System.IO;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using Dialysis_Chart_Show.tools;
using NLog;
using System.Threading;
using Dialysis_Chart_Show.Information;
using Newtonsoft.Json;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    public class UploadAdminALL
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
        DataTable dtFhir = new DataTable();
        DataTable dtrFhir = new DataTable();
        System.Data.DataView dvFhir;
        string slastUpdated = ""; 
        DataRow drN;

        public bool UploadPractitioner(string TrDate)
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
            UploadAdmin uploadAdmin = new UploadAdmin();
            try
            {
                uploadAdmin.OrganizationCreateUpdate(orgId);
                logger.Info("Organization Create/Update:" + orgId);
            }
            catch (Exception ex)
            {
                logger.Error(ex.Message);
            }

            //多筆醫師處置人員
            DBMysql db = new DBMysql();
            DataTable Mydt = new DataTable();
            string sSQL="SELECT usrnm, name, type, active FROM access_level ";
            sSQL+= "ORDER BY acclv_id";
            Mydt = db.Query(sSQL);

            if (Mydt.Rows.Count > 0)
            {
                string acclv_stfcode = "Admin";
                string acclv_fname = "Administrator";
                string acclv_type = "Doctor";
                Boolean acclv_active = true;
                DateTime lastUpdated;
                getAuditList(); 
                try
                {
                    lastUpdated = new DateTime(Convert.ToInt16(slastUpdated.Substring(0, 4)), Convert.ToInt16(slastUpdated.Substring(5, 2)), Convert.ToInt16(slastUpdated.Substring(8, 2)));
                }
                catch (Exception)
                {
                    lastUpdated = DateTime.Now.AddMonths(-1);
                }

                ConcurrentBag<AuditObject> successList = new ConcurrentBag<AuditObject>();
                ConcurrentBag<AuditObject> failList = new ConcurrentBag<AuditObject>();
                int practitionerCounter = 0;
                AuditObject resultAudit = new AuditObject();
                resultAudit.Success = false;

                Stopwatch sw = new Stopwatch();
                sw.Start();
                foreach (DataRow row in Mydt.Rows)
                //Parallel.ForEach(Mydt.AsEnumerable(), row =>
                {
                    lock (syncHandle)
                    {
                        acclv_stfcode = row["usrnm"].ToString();
                        acclv_fname = row["name"].ToString();
                        acclv_type = row["type"].ToString();
                        if (row["active"].ToString() == "A")
                            { acclv_active = true; }
                        else
                            { acclv_active = false; }
                    }
                    practitionerCounter++; 
                    logger.Info("Practitioner Create " + practitionerCounter.ToString() + "/" + Mydt.Rows.Count.ToString() + " @" + "acclv_stfcode=" + acclv_stfcode + ",acclv_fname=" + acclv_fname);
                    dvFhir.RowFilter = "C1='" + acclv_stfcode + "'";
                    if (dvFhir.Count > 0)
                    {
                        // 先拿掉要 5 天以上才能更新的門檻 - Remarked by Evan 20160906
                        //if (lastUpdated.CompareTo(DateTime.Now.AddDays(-5)) < 0)
                        {
                            if (dvFhir[0].Row["C3"].ToString() == acclv_fname.GetHashCode().ToString())
                                continue;
                        }
                        //else 
                        //    continue;
                        //------------------------------
                    }

                    try
                    {
                        // TODO: Create Practitioner
                        //orgId, orgName, Stracclv_stfcode, Stracclv_fname, Stracclv_type, typeSystem, Bacclv_active, tel, email
                        //    logger.Info("Practitioner Create/Update:" + practitionerCounter + "/" + Mydt.Rows.Count);
                        resultAudit = uploadAdmin.PractitionerCreateUpdate(orgId, orgName, acclv_stfcode, acclv_fname, acclv_type, "", acclv_active, "", "");
                        if (resultAudit.Success)
                            successList.Add(resultAudit);
                        else
                            failList.Add(resultAudit);
                    }
                    catch (Exception ex)
                    {
                        failList.Add(new AuditObject { Success = false, Name = acclv_stfcode, Desc = acclv_fname });
                        logger.Error(ex.Message);
                        logger.Info(ex.Message);
                        logger.Error("Practitioner Create catch Fail: " + practitionerCounter.ToString() + "/" + Mydt.Rows.Count.ToString() + " @" + "acclv_stfcode=" + acclv_stfcode + ",acclv_fname=" + acclv_fname);
                    }
                    //while (interval.CompareTo(DateTime.Now) > 0) { if (flag) { logger.Info("--------------"); flag = false; } };
                };
                sw.Stop();
                logger.Info("Practitioner Create/Update exec:" + practitionerCounter.ToString() + "/" + Mydt.Rows.Count.ToString() + " total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                string ip = null;
                if (HttpContext.Current != null)
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    System.Net.IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                    ip = SvrIP.ToString();
                }

                try
                {
                    if (successList.Count > 0)
                        uploadAdmin.AuditEventPracCreateUpdate("UL.PRACTITIONER", orgId, "Organization", orgId, orgId, successList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.PRACTITIONER"), ip);
                    //if (successList.Count > 0)
                    //    uploadAdmin.AuditEventCreateUpdate("UL.Practitioner", "Organization", orgId, orgId, successList.ToList<AuditObject>(),
                    //        Hl7.Fhir.Model.AuditEvent.AuditEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                    //        new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_PRACTITIONER"), ip);
                    //if (failList.Count > 0)
                    //    uploadAdmin.AuditEventCreateUpdate("UL.Practitioner", "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                    //        Hl7.Fhir.Model.AuditEvent.AuditEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                    //        new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_PRACTITIONER"), ip);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                    logger.Error("AuditEvent Create catch Fail@" + "UL.PRACTITIONER");
                }
            }
            return true;
        }
     
        public bool UploadPatient(string TrDate)
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
            try
            {
                UploadAdmin uploadAdmin = new UploadAdmin();
                try
                {
                    uploadAdmin.OrganizationCreateUpdate(orgId);
                    logger.Info("Organization Create/Update:" + orgId);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }
                DBMysql db = new DBMysql();
                DataTable Mydt = new DataTable();
                //病患上傳資料
                Mydt = new DataTable();
                string sql = "select pif_ic, pif_name, pif_pattyp, pif_mrn, pif_dob, pif_sex, pif_address, pif_contactperson, pif_contact, pif_imgloc, pif_insurance, pif_insid, pif_docname  " +
                        "   from pat_info  "+
                        "     left join zinfo_a_07  f on pat_info.pif_id = f.pat_id and f.opt_1 not in('','5') and  f.info_date <= '" + TrDate + "' " +
                        "   WHERE pif_dob<= CURDATE() and SUBSTR(PIF_DOB,1,1) IN ('1','2') " +
                        "     AND PIF_SEX<>'' and pat_info.pif_id not in (select b.pif_id from pat_info b "+
                        "                inner join (select a.pat_id,a.opt_1, a.info_date from zinfo_a_07 a "+
                        "                inner join (select pat_id,max(info_date) AS last_date from zinfo_a_07 group by pat_id) b "+
                        "                  on a.pat_id=b.pat_id and a.info_date=b.last_date) f "+
                        "                  on b.pif_id = f.pat_id and f.opt_1 in('1','2','3','4') and f.info_date<='" + TrDate + "') " +
                        "    group by pif_ic limit 10";
                Mydt = db.Query(sql);
                if (Mydt.Rows.Count > 0)
                {
                    string pif_ic = "", pif_contactperson = "" , pif_address = "" , pif_sex = "" , pif_dob = "" , pif_mrn = "", pif_pattyp = "", pif_name = "";
                    string pif_contact = ""; //tel
                    string pif_imgloc = "", pif_docname = "", pif_insid = "", pif_insurance = "";
                    DateTime lastUpdated;
                    getAuditList();
                    try
                    {
                        lastUpdated = new DateTime(Convert.ToInt16(slastUpdated.Substring(0, 4)), Convert.ToInt16(slastUpdated.Substring(5, 2)), Convert.ToInt16(slastUpdated.Substring(8, 2)));
                    }
                    catch (Exception)
                    {
                        lastUpdated = DateTime.Now;
                    }

                    ConcurrentBag<AuditObject> successList = new ConcurrentBag<AuditObject>();
                    ConcurrentBag<AuditObject> failList = new ConcurrentBag<AuditObject>();
                    int patientCounter = 0;

                    Stopwatch sw1 = new Stopwatch();
                    sw1.Start();
                    foreach (DataRow row in Mydt.Rows)
                    //Parallel.ForEach(Mydt.AsEnumerable(), row =>
                    {
                        patientCounter++;
                        logger.Info("");
                        logger.Info("Patient information: " + patientCounter.ToString() + "/" + Mydt.Rows.Count.ToString() + "@" + "pif_ic=" + row["pif_ic"].ToString() + ",pif_name=" + row["pif_name"].ToString());
                        dvFhir.RowFilter = "C1='" + row["pif_ic"].ToString() + "'";
                        if (dvFhir.Count > 0)
                        {
                            // 先拿掉要 5 天以上才能更新的門檻 - Remarked by Evan 20160906
                            //if (lastUpdated.CompareTo(DateTime.Now.AddDays(-5)) < 0)
                            {
                                if (dvFhir[0].Row["C3"].ToString() == (row["pif_name"].ToString() + row["pif_docname"].ToString() + row["pif_dob"].ToString()).GetHashCode().ToString())
                                {
                                    logger.Info("Patient information pif_ic no transfer:@pif_ic=" + row["pif_ic"].ToString() + ",pif_name=" + row["pif_name"].ToString());
                                    continue;
                                }
                            }
                            //else
                            //{
                            //    logger.Info("Patient information pif_ic no transfer:@pif_ic=" + row["pif_ic"].ToString() + ",pif_name=" + row["pif_name"].ToString());
                            //    continue;
                            //}
                            //------------------------------
                        }
                        
                        pif_ic = row["pif_ic"].ToString();
                        pif_name = row["pif_name"].ToString();
                        pif_pattyp = row["pif_pattyp"].ToString();
                        pif_mrn = row["pif_mrn"].ToString();
                        pif_dob = row["pif_dob"].ToString();
                        pif_sex = row["pif_sex"].ToString();
                        pif_address = row["pif_address"].ToString();
                        pif_contactperson = row["pif_contactperson"].ToString();
                        pif_contact = row["pif_contact"].ToString();
                        pif_imgloc = row["pif_imgloc"].ToString();
                        pif_insurance = row["pif_insurance"].ToString();
                        pif_insid = row["pif_insid"].ToString();
                        pif_docname = row["pif_docname"].ToString();
                        try
                        {
                            AuditObject resultAudit;
                            resultAudit = uploadAdmin.PatientCreateUpdate(orgId, orgName, pif_ic, pif_name, pif_dob, pif_pattyp,
                                pif_mrn, pif_sex, pif_address, pif_contactperson, pif_contact, pif_imgloc,
                                pif_insurance, pif_insid, pif_docname, "");

                            if (resultAudit.Success)
                            {
                                successList.Add(resultAudit);
                                logger.Info("Patient Create Success@" + "pif_ic=" + pif_ic + ",pif_name=" + pif_name);
                            }
                            else
                            {
                                failList.Add(resultAudit);
                                logger.Info("Patient Create Fail@" + "pif_ic=" + pif_ic + ",pif_name=" + pif_name);
                            }
                        }
                        catch (Exception ex)
                        {
                            failList.Add(new AuditObject { Success = false, Name = pif_ic, Desc = pif_name });
                            logger.Error(ex.Message);
                            logger.Error("Patient Create catch Fail@" + "pif_ic=" + pif_ic + ",pif_name=" + pif_name);
                            logger.Info("Patient Create catch Fail@" + "pif_ic=" + pif_ic + ",pif_name=" + pif_name);
                        }
                    };
                    sw1.Stop();
                    logger.Info("Patient Create/Update exec:" + patientCounter.ToString() + "/" + Mydt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");

                    sw1.Restart();
                    try
                    {
                        if (successList.Count > 0)
                        {
                            string ip = null;
                            if (HttpContext.Current != null)
                            {
                                ip = HttpContext.Current.Request.UserHostAddress;
                            }
                            else
                            {
                                System.Net.IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                                ip = SvrIP.ToString();
                            }

                            uploadAdmin.AuditEventPatientCreateUpdate("UL.PATIENT", orgId, "Organization", orgId, orgId,
                               successList.ToList<AuditObject>(), Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                               new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL.PATIENT"),
                               HttpContext.Current.Request.UserHostAddress);
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create catch Fail@" + "UL.PATIENT");
                    }
                    sw1.Stop();
                    logger.Info("PatientAudit Create/Update total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                }
            }
            catch (Exception uploadAdminException)
            {
                logger.Error(uploadAdminException.Message);
                logger.Error("UploadAdmin Error:" + uploadAdminException.Message);
                return false;
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

            string[] sURLs = { sFHIR_SERVER + "UL.PRACTITIONER." + sFHIR_Id, sFHIR_SERVER + "UL.PATIENT." + sFHIR_Id };

            for (int i = 0; i < 2; i++)
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
                slastUpdated = oAE.meta.lastUpdated.Substring(0,10);
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
                    //if (Convert.ToDateTime(strtmp) >= sMdate)
                    //    { drN["C4"] = "成功"; }
                    //else
                    //    { drN["C4"] = "待傳"; };
                    dtFhir.Rows.Add(drN);
                    logger.Trace("identifier : " + ae.identifier.value.ToString());
                    logger.Trace("reference : " + ae.reference.reference.ToString());
                    logger.Trace("name : " + ae.name.ToString());
                }
            }
        }

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

        public void UploadAdminTwo()
        {
            try
            {
                UploadAdmin uploadAdmin = new UploadAdmin();
                try
                {
                    uploadAdmin.OrganizationCreateUpdate(orgId);
                    logger.Info("Organization Create/Update:" + orgId);
                }
                catch (Exception ex)
                {
                    logger.Error(ex.Message);
                }

                //多筆醫師處置人員
                //DBOracle db = new DBOracle();
                DBMysql db = new DBMysql();
                DataTable Mydt = new DataTable();
                Mydt = db.Query("select acclv_stfcode, acclv_fname, acclv_type, acclv_active from access_level");

                if (Mydt.Rows.Count > 0)
                {
                    //orgId = "H57069000.8602";
                    //orgId = "H32000800.8066";
                    //orgName = "南京市大厂医院血液净化中心";
                    //orgName = "南京医科大学血液净化中心";
                    string acclv_stfcode = "Admin";
                    string acclv_fname = "Administrator";
                    string acclv_type = "Doctor";
                    Boolean acclv_active = true;
                    string acclv_id = "1";

                    /*if (new TimeSpan(DateTime.Now.Ticks - newestSuccessListDateTime.Ticks).Days < 5)
                    {
                        DataTable MydtFilter = new DataTable();
                        foreach (DataRow row in Mydt.Rows)
                        {
                            if (!newstSuccessIdList.Contains(row["acclv_stfcode"].ToString()))
                                MydtFilter.ImportRow(row);
                        }
                        Mydt = MydtFilter;
                    }
                    if (Mydt.Rows.Count == 0)
                        logger.Info("Practitioner Creaete/Update List is Empty");
                    */

                    ConcurrentBag<AuditObject> successList = new ConcurrentBag<AuditObject>();
                    ConcurrentBag<AuditObject> failList = new ConcurrentBag<AuditObject>();
                    int practitionerCounter = 1;

                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    //foreach (DataRow row in Mydt.Rows)
                    Parallel.ForEach(Mydt.AsEnumerable(), row =>
                    {
                        lock (syncHandle)
                        {
                            acclv_stfcode = row["acclv_stfcode"].ToString();
                            acclv_fname = row["acclv_fname"].ToString();
                            acclv_type = row["acclv_type"].ToString();
                            //acclv_id = Mydt.Rows[i]["acclv_id"].ToString();
                        }
                        logger.Info("Practitioner information@" + "acclv_stfcode=" + acclv_stfcode + ",acclv_fname=" + acclv_fname);
                        try
                        {
                            // TODO: Create Practitioner
                            //orgId, orgName, Stracclv_stfcode, Stracclv_fname, Stracclv_type, typeSystem, Bacclv_active, tel, email
                            AuditObject resultAudit = uploadAdmin.PractitionerCreateUpdate(orgId, orgName, acclv_stfcode, acclv_fname, acclv_type, "", acclv_active, "", "");
                            logger.Info("Practitioner Create/Update:" + practitionerCounter + "/" + Mydt.Rows.Count);
                            if (resultAudit.Success)
                            {
                                successList.Add(resultAudit);
                                logger.Info("Practitioner Create Success@" + "acclv_stfcode=" + acclv_stfcode + ",acclv_fname=" + acclv_fname);
                            }
                            else
                            {
                                failList.Add(resultAudit);
                                logger.Info("Practitioner Create Fail@" + "acclv_stfcode=" + acclv_stfcode + ",acclv_fname=" + acclv_fname);
                            }
                            practitionerCounter++;
                        }
                        catch (Exception ex)
                        {
                            failList.Add(new AuditObject { Success = false, Name = acclv_stfcode, Desc = acclv_fname });
                            logger.Error(ex.Message);
                            logger.Error("Practitioner Create catch Fail@" + "acclv_stfcode=" + acclv_stfcode + ",acclv_fname=" + acclv_fname);
                        }
                    });
                    sw.Stop();
                    logger.Info("Practitioner Create/Update exec:" + practitionerCounter + "/" + Mydt.Rows.Count + " total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    string ip = null;
                    if (HttpContext.Current != null)
                    {
                        ip = HttpContext.Current.Request.UserHostAddress;
                    }
                    else
                    {
                        System.Net.IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                        ip = SvrIP.ToString();
                    }

                    try
                    {
                        if (successList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate("UL.Practitioner", "Organization", orgId, orgId, successList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.AuditEvent.AuditEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_PRACTITIONER"), ip);
                        if (failList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate("UL.Practitioner", "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.AuditEvent.AuditEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_PRACTITIONER"), ip);
                         
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("AuditEvent Create Fail@" + "UL_PRACTITIONER");
                    }
                }
                //病患上傳資料
                Mydt = new DataTable();
                Mydt = db.Query(" select pif_ic, pif_name, pif_pattyp, pif_mrn, pif_dob, pif_sex, pif_address, pif_contactperson, pif_contact, pif_imgloc, pif_insurance, pif_insid, pif_docname from pat_info group by pif_ic");
                if (Mydt.Rows.Count > 0)
                {
                    string pif_ic = "";
                    string pif_name = "";
                    string pif_pattyp = "";
                    string pif_mrn = "";
                    string pif_dob = "";
                    string pif_sex = "";
                    string pif_address = "";
                    string pif_contactperson = "";
                    string pif_contact = ""; //tel
                    string pif_imgloc = "";
                    string pif_insurance = "";
                    string pif_insid = "";
                    string pif_docname = "";

                    ConcurrentBag<AuditObject> successList = new ConcurrentBag<AuditObject>();
                    ConcurrentBag<AuditObject> failList = new ConcurrentBag<AuditObject>();
                    int patientCounter = 1;

                    Stopwatch sw1 = new Stopwatch();
                    sw1.Start();
                    //foreach (DataRow row in Mydt.Rows)
                    Parallel.ForEach(Mydt.AsEnumerable(), row =>
                    {
                        lock (syncHandle1)
                        {
                            pif_ic = row["pif_ic"].ToString();
                            pif_name = row["pif_name"].ToString();
                            pif_pattyp = row["pif_pattyp"].ToString();
                            pif_mrn = row["pif_mrn"].ToString();
                            pif_dob = row["pif_dob"].ToString();
                            pif_sex = row["pif_sex"].ToString();
                            pif_address = row["pif_address"].ToString();
                            pif_contactperson = row["pif_contactperson"].ToString();
                            pif_contact = row["pif_contact"].ToString();
                            pif_imgloc = row["pif_imgloc"].ToString();
                            pif_insurance = row["pif_insurance"].ToString();
                            pif_insid = row["pif_insid"].ToString();
                            pif_docname = row["pif_docname"].ToString();
                            //acclv_id = Mydt.Rows[i]["acclv_id"].ToString();
                        }
                        logger.Info("Patient information@" + "pif_ic=" + pif_ic + ",pif_name=" + pif_name);
                        try
                        {
                            // TODO: Create Patient
                            //Str_pif_ic, Str_pif_name, Str_pif_dob, Str_pif_pattyp, Str_pif_mrn, Str_pif_sex, Str_pif_address, Str_pif_contactperson, Str_pif_contact, Str_pif_imgloc, Str_pif_insurance, Str_pif_insid, Str_pif_docname, email
                            AuditObject resultAudit = uploadAdmin.PatientCreateUpdate(orgId, orgName, pif_ic, pif_name, pif_dob, pif_pattyp,
                                pif_mrn, pif_sex, pif_address, pif_contactperson, pif_contact, pif_imgloc,
                                pif_insurance, pif_insid, pif_docname, "");
                            logger.Info("Patient Create/Update:" + patientCounter + "/" + Mydt.Rows.Count);
                            if (resultAudit.Success)
                            {
                                successList.Add(resultAudit);
                                logger.Info("Patient Create Success@" + "pif_ic=" + pif_ic + ",pif_name=" + pif_name);
                            }  
                            else
                            {
                                failList.Add(resultAudit);
                                logger.Info("Patient Create Fail@" + "pif_ic=" + pif_ic + ",pif_name=" + pif_name);
                            }      
                            patientCounter++;
                        }
                        catch (Exception ex)
                        {
                            failList.Add(new AuditObject { Success = false, Name = pif_ic, Desc = pif_name });
                            logger.Error(ex.Message);
                            logger.Error("Patient Create catch Fail@" + "pif_ic=" + pif_ic + ",pif_name=" + pif_name);
                        }
                    });
                    sw1.Stop();
                    logger.Info("Patient Create/Update exec:" + patientCounter + "/" + Mydt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");

                    sw1.Restart();
                    try
                    {
                        if (successList.Count > 0)
                        {
                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, successList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.SecurityEvent.SecurityEventOutcome.N0,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                             */
                            List<AuditObject> ulClinicalList = successList.ToList<AuditObject>();
                            string auditDateTime = DateTime.Now.ToString("yyyyMMddHHmmss");
                            foreach (AuditObject obj in ulClinicalList)
                            {
                                obj.Name = auditDateTime;
                                obj.Desc = "";
                            }
                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                ulClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                HttpContext.Current.Request.UserHostAddress);
                        }
                        /*if (failList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.AuditEvent.AuditEventAction.U, Hl7.Fhir.Model.SecurityEvent.AuditEventOutcome.N4,
                                new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                         */
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create Fail@" + "UL_PATIENT");
                    }
                    sw1.Stop();
                    logger.Info("PatientAudit Create/Update total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                }
            }
            catch (Exception uploadAdminException)
            {
                logger.Error(uploadAdminException.Message);
                logger.Error("UploadAdmin Error:" + uploadAdminException.Message);
            }
            finally
            {
            }
        }
    }
}
