using System;
using System.Data;
using System.Xml;
using System.Net;
using System.Web;
using Hl7.Fhir.Model;
using System.IO;
using System.Collections.Generic;
using System.Configuration;
using NLog;
using System.Linq;
using System.Diagnostics;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Information
{
    public class DialysisClientUpLoad
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        // 上傳人口分布統計資料
        public bool UploadPD(string Trdate)
        {
            bool bCheck = false;
            string sURL = "";
            string ip = null;

            try
            {
                sURL = getSET("Dialysis_url") + getSET("Dialysis_Organization");
                string[] OrganizationInfo = ReadFhirOrganization(sURL);

                if (OrganizationInfo[0] == "nodata")
                    return false;

                string orgId = OrganizationInfo[0];
                string recipientId = OrganizationInfo[1];
                string sentDate = Trdate + "-" + DateTime.DaysInMonth(int.Parse(Trdate.Substring(0, 4)), int.Parse(Trdate.Substring(5, 2))).ToString();
                sURL = getSET("Dialysis_url") + getSET("Dialysis_PD") + "/" + sentDate;
                string[] modifier = GetPD(sURL);

                if (modifier[0] == "nodata")
                    return false;

                UploadStatistic target = new UploadStatistic();
                target.setFhirURI(getSET("FHIR_SERVER"));
                string sMdate = Trdate.Replace("-", "");
                string commId = "PD." + orgId + "." + sMdate;
                string commIdentifier = "PD." + orgId + "." + sMdate;
                string commIdentifierSystem = "http://www.datacom.com.cn/DialysisStatistic";
                string categoryCode = "PD";
                string categorySystem = "http://www.datacom.com.cn/DialysisStatisticCategory";
                string categoryName = "人口分布";
                string senderId = orgId;
                string eventDateTime = DateTime.UtcNow.ToString("s");
                Timing.UnitsOfTime durationUnit = Timing.UnitsOfTime.Mo;
                List<string[]> itemValue = new List<string[]>();
                itemValue.Add(new string[] { "-30", "http://www.datacom.com.cn/DialysisStatisticPD", "-30", "M", modifier[0] });
                itemValue.Add(new string[] { "-30", "http://www.datacom.com.cn/DialysisStatisticPD", "-30", "F", modifier[1] });
                itemValue.Add(new string[] { "30-39", "http://www.datacom.com.cn/DialysisStatisticPD", "30-39", "M", modifier[2] });
                itemValue.Add(new string[] { "30-39", "http://www.datacom.com.cn/DialysisStatisticPD", "30-39", "F", modifier[3] });
                itemValue.Add(new string[] { "40-49", "http://www.datacom.com.cn/DialysisStatisticPD", "40-49", "M", modifier[4] });
                itemValue.Add(new string[] { "40-49", "http://www.datacom.com.cn/DialysisStatisticPD", "40-49", "F", modifier[5] });
                itemValue.Add(new string[] { "50-59", "http://www.datacom.com.cn/DialysisStatisticPD", "50-59", "M", modifier[6] });
                itemValue.Add(new string[] { "50-59", "http://www.datacom.com.cn/DialysisStatisticPD", "50-59", "F", modifier[7] });
                itemValue.Add(new string[] { "60-69", "http://www.datacom.com.cn/DialysisStatisticPD", "60-69", "M", modifier[8] });
                itemValue.Add(new string[] { "60-69", "http://www.datacom.com.cn/DialysisStatisticPD", "60-69", "F", modifier[9] });
                itemValue.Add(new string[] { "70-79", "http://www.datacom.com.cn/DialysisStatisticPD", "70-79", "M", modifier[10] });
                itemValue.Add(new string[] { "70-79", "http://www.datacom.com.cn/DialysisStatisticPD", "70-79", "F", modifier[11] });
                itemValue.Add(new string[] { "80-89", "http://www.datacom.com.cn/DialysisStatisticPD", "80-89", "M", modifier[12] });
                itemValue.Add(new string[] { "80-89", "http://www.datacom.com.cn/DialysisStatisticPD", "80-89", "F", modifier[13] });
                itemValue.Add(new string[] { "90-", "http://www.datacom.com.cn/DialysisStatisticPD", "90-", "M", modifier[14] });
                itemValue.Add(new string[] { "90-", "http://www.datacom.com.cn/DialysisStatisticPD", "90-", "F", modifier[15] });
                itemValue.Add(new string[] { "SUM", "http://www.datacom.com.cn/DialysisStatisticPD", "SUM", "M", modifier[16] });
                itemValue.Add(new string[] { "SUM", "http://www.datacom.com.cn/DialysisStatisticPD", "SUM", "F", modifier[17] });
                itemValue.Add(new string[] { "Positive", "http://www.datacom.com.cn/DialysisStatisticPD", "Positive", "M", modifier[18] });
                itemValue.Add(new string[] { "Positive", "http://www.datacom.com.cn/DialysisStatisticPD", "Positive", "F", modifier[19] });
                itemValue.Add(new string[] { "Negitive", "http://www.datacom.com.cn/DialysisStatisticPD", "Negitive", "M", modifier[20] });
                itemValue.Add(new string[] { "Negitive", "http://www.datacom.com.cn/DialysisStatisticPD", "Negitive", "F", modifier[21] });
                logger.Trace("PD Event Date : " + eventDateTime);
                //Trace.WriteLine("PD Event Date : " + eventDateTime);
                target.CommunicationCreateUpdate(commId, commIdentifier, commIdentifierSystem, categoryCode, categorySystem, categoryName, senderId, recipientId, sentDate, eventDateTime, durationUnit, itemValue);

                if (HttpContext.Current != null)
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    System.Net.IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                    ip = SvrIP.ToString();
                }
                logger.Trace("PD IP Address : " + ip.ToString());
                //Trace.WriteLine("PD IP Address : " + ip.ToString());
                AuditObject ao = new AuditObject() { Reference = "Communication/" + commId, Name = sMdate + categoryName, Success = true };
                target.CommunicationAuditEventCreateUpdate("UL.PD", "Statistic", categoryCode, AuditEvent.AuditEventOutcome.N0, ip, "", senderId, recipientId, "", ao);

                bCheck = true;
            }
            catch (Exception e)
            {
                logger.Error("UploadPD error, error message: " + e.Message);
                return false;
            }
            return bCheck;

        }
        // 上傳血透年限分布統計資料
        public bool UploadDD(string Trdate)
        {
            bool bCheck = false;
            string sURL = "";

            try
            {
                sURL = getSET("Dialysis_url") + getSET("Dialysis_Organization");
                string[] OrganizationInfo = ReadFhirOrganization(sURL);
                string ip = null;

                if (OrganizationInfo[0] == "nodata")
                    return false;

                string orgId = OrganizationInfo[0];
                string recipientId = OrganizationInfo[1];
                string sentDate = Trdate + "-" + DateTime.DaysInMonth(int.Parse(Trdate.Substring(0, 4)), int.Parse(Trdate.Substring(5, 2))).ToString();
                sURL = getSET("Dialysis_url") + getSET("Dialysis_DD") + "/" + sentDate;
                string[] modifier = GetDD(sURL);

                if (modifier[0] == "nodata")
                    return false;

                UploadStatistic target = new UploadStatistic();
                target.setFhirURI(getSET("FHIR_SERVER"));
                string sMdate = Trdate.Replace("-", "");
                string commId = "DDD." + orgId + "." + sMdate;
                string commIdentifier = "DDD." + orgId + "." + sMdate;
                string commIdentifierSystem = "http://www.datacom.com.cn/DialysisStatistic";
                string categoryCode = "DDD";
                string categorySystem = "http://www.datacom.com.cn/DialysisStatisticCategory";
                string categoryName = "血透年限分布";
                string senderId = orgId;
                string eventDateTime = DateTime.UtcNow.ToString("s");

                Timing.UnitsOfTime durationUnit = Timing.UnitsOfTime.Mo;
                List<string[]> itemValue = new List<string[]>();
                itemValue.Add(new string[] { "0-1", "http://www.datacom.com.cn/DialysisStatisticDDD", "0-1", "M", modifier[0] });
                itemValue.Add(new string[] { "0-1", "http://www.datacom.com.cn/DialysisStatisticDDD", "0-1", "F", modifier[1] });
                itemValue.Add(new string[] { "1-3", "http://www.datacom.com.cn/DialysisStatisticDDD", "1-3", "M", modifier[2] });
                itemValue.Add(new string[] { "1-3", "http://www.datacom.com.cn/DialysisStatisticDDD", "1-3", "F", modifier[3] });
                itemValue.Add(new string[] { "3-5", "http://www.datacom.com.cn/DialysisStatisticDDD", "3-5", "M", modifier[4] });
                itemValue.Add(new string[] { "3-5", "http://www.datacom.com.cn/DialysisStatisticDDD", "3-5", "F", modifier[5] });
                itemValue.Add(new string[] { "5-7", "http://www.datacom.com.cn/DialysisStatisticDDD", "5-7", "M", modifier[6] });
                itemValue.Add(new string[] { "5-7", "http://www.datacom.com.cn/DialysisStatisticDDD", "5-7", "F", modifier[7] });
                itemValue.Add(new string[] { "7-9", "http://www.datacom.com.cn/DialysisStatisticDDD", "7-9", "M", modifier[8] });
                itemValue.Add(new string[] { "7-9", "http://www.datacom.com.cn/DialysisStatisticDDD", "7-9", "F", modifier[9] });
                itemValue.Add(new string[] { "9-", "http://www.datacom.com.cn/DialysisStatisticDDD", "9-", "M", modifier[10] });
                itemValue.Add(new string[] { "9-", "http://www.datacom.com.cn/DialysisStatisticDDD", "9-", "F", modifier[11] });
                itemValue.Add(new string[] { "SUM", "http://www.datacom.com.cn/DialysisStatisticDDD", "SUM", "M", modifier[16] });
                itemValue.Add(new string[] { "SUM", "http://www.datacom.com.cn/DialysisStatisticDDD", "SUM", "F", modifier[17] });
                target.CommunicationCreateUpdate(commId, commIdentifier, commIdentifierSystem, categoryCode, categorySystem, categoryName, senderId, recipientId, sentDate, eventDateTime, durationUnit, itemValue);
                logger.Trace("DDD Event Date : " + eventDateTime);
                //Trace.WriteLine("DDD Event Date : " + eventDateTime);
                if (HttpContext.Current != null)
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    System.Net.IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                    ip = SvrIP.ToString();
                }
                logger.Trace("DDD IP Address : " + ip.ToString());
                //Trace.WriteLine("DDD IP Address : " + ip.ToString());
                AuditObject ao = new AuditObject() { Reference = "Communication/" + commId, Name = sMdate + categoryName, Success = true };
                target.CommunicationAuditEventCreateUpdate("UL.DDD", "Statistic", categoryCode, AuditEvent.AuditEventOutcome.N0, ip, "", senderId, recipientId, "", ao);
                bCheck = true;

            }
            catch (Exception e)
            {
                logger.Error("UploadDD error, error message: " + e.Message);
                return false;
            }
            return bCheck;

        }
        // 上傳死亡率統計資料
        public bool UploadMR(string Trdate)
        {
            bool bCheck = false;
            string sURL = "";
            string ip = null;

            try
            {
                sURL = getSET("Dialysis_url") + getSET("Dialysis_Organization");
                string[] OrganizationInfo = ReadFhirOrganization(sURL);

                if (OrganizationInfo[0] == "nodata")
                    return false;

                string orgId = OrganizationInfo[0];
                string recipientId = OrganizationInfo[1];
                sURL = getSET("Dialysis_url") + getSET("Dialysis_MR") + "/" + Trdate;
                string[] modifier = GetMR(sURL);

                if (modifier[0] == "nodata")
                    return false;

                UploadStatistic target = new UploadStatistic();
                target.setFhirURI(getSET("FHIR_SERVER"));
                string sMdate = Trdate.Replace("-", "");
                string commId = "MR." + orgId + "." + sMdate;
                string commIdentifier = "MR." + orgId + "." + sMdate;
                string commIdentifierSystem = "http://www.datacom.com.cn/DialysisStatistic";
                string categoryCode = "MR";
                string categorySystem = "http://www.datacom.com.cn/DialysisStatisticCategory";
                string categoryName = "死亡率";
                string senderId = orgId;
                string sentDate = Trdate + "-" + DateTime.DaysInMonth(int.Parse(Trdate.Substring(0, 4)), int.Parse(Trdate.Substring(5, 2))).ToString();
                string eventDateTime = DateTime.UtcNow.ToString("s");
                Timing.UnitsOfTime durationUnit = Timing.UnitsOfTime.Mo;
                List<string[]> itemValue = new List<string[]>();
                itemValue.Add(new string[] { "deaths", "http://www.datacom.com.cn/DialysisStatisticMR", "死亡人数", "M", modifier[0] });
                itemValue.Add(new string[] { "deaths", "http://www.datacom.com.cn/DialysisStatisticMR", "死亡人数", "F", modifier[1] });
                itemValue.Add(new string[] { "population", "http://www.datacom.com.cn/DialysisStatisticMR", "总人数", "M", modifier[2] });
                itemValue.Add(new string[] { "population", "http://www.datacom.com.cn/DialysisStatisticMR", "总人数", "F", modifier[3] });
                target.CommunicationCreateUpdate(commId, commIdentifier, commIdentifierSystem, categoryCode, categorySystem, categoryName, senderId, recipientId, sentDate, eventDateTime, durationUnit, itemValue);
                logger.Trace("MR Event Date : " + eventDateTime);
                //Trace.WriteLine("MR Event Date : " + eventDateTime);
                if (HttpContext.Current != null)
                {
                    ip = HttpContext.Current.Request.UserHostAddress;
                }
                else
                {
                    System.Net.IPAddress SvrIP = new System.Net.IPAddress(Dns.GetHostByName(Dns.GetHostName()).AddressList[0].Address);
                    ip = SvrIP.ToString();
                }
                AuditObject ao = new AuditObject() { Reference = "Communication/" + commId, Name = sMdate + categoryName, Success = true };
                target.CommunicationAuditEventCreateUpdate("UL.MR", "Statistic", categoryCode, AuditEvent.AuditEventOutcome.N0, ip, "", senderId, recipientId, "", ao);
                bCheck = true;

            }
            catch (Exception e)
            {
                logger.Error("UploadMR error, error message: " + e.Message);
                return false;
            }
            return bCheck;

        }
        // 上傳血透品質統計資料
        public bool UploadDQ(string Trdate)
        {
            bool bCheck = false;
            string sURL = "";

            try
            {
                sURL = getSET("Dialysis_url") + getSET("Dialysis_Organization");
                string[] OrganizationInfo = ReadFhirOrganization(sURL);

                if (OrganizationInfo[0] == "nodata")
                    return false;

                string orgId = OrganizationInfo[0];
                string recipientId = OrganizationInfo[1];
                sURL = getSET("Dialysis_url") + getSET("Dialysis_DQ") + "/" + Trdate;
                string[] modifier = GetDQ(sURL);

                if (modifier[0] == "nodata")
                    return false;

                UploadStatistic target = new UploadStatistic();
                target.setFhirURI(getSET("FHIR_SERVER"));
                string sMdate = Trdate.Replace("-", "");
                string commId = "DQ." + orgId + "." + sMdate;
                string commIdentifier = "DQ." + orgId + "." + sMdate;
                string commIdentifierSystem = "http://www.datacom.com.cn/DialysisStatistic";
                string categoryCode = "DQ";
                string categorySystem = "http://www.datacom.com.cn/DialysisStatisticCategory";
                string categoryName = "血液透析品质";
                string senderId = orgId;
                string sentDate = Trdate + "-" + DateTime.DaysInMonth(int.Parse(Trdate.Substring(0, 4)), int.Parse(Trdate.Substring(5, 2))).ToString();
                string eventDateTime = DateTime.UtcNow.ToString("s");
                Timing.UnitsOfTime durationUnit = Timing.UnitsOfTime.Mo;
                List<string[]> itemValue = new List<string[]>();
                itemValue.Add(new string[] { "Hb_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "Hb 已检人数", null, modifier[0] });
                itemValue.Add(new string[] { "Hb_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "Hb 合格人数", null, modifier[1] });
                itemValue.Add(new string[] { "ALB_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "ALB 已检人数", null, modifier[2] });
                itemValue.Add(new string[] { "ALB_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "ALB 合格人数", null, modifier[3] });
                itemValue.Add(new string[] { "Ca_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "Ca 已检人数", null, modifier[4] });
                itemValue.Add(new string[] { "Ca_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "Ca 合格人数", null, modifier[5] });
                itemValue.Add(new string[] { "P_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "P 已检人数", null, modifier[6] });
                itemValue.Add(new string[] { "P_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "P 合格人数", null, modifier[7] });
                itemValue.Add(new string[] { "TS_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "TS 已检人数", null, modifier[8] });
                itemValue.Add(new string[] { "TS_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "TS 合格人数", null, modifier[9] });
                itemValue.Add(new string[] { "SF_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "SF 已检人数", null, modifier[10] });
                itemValue.Add(new string[] { "SF_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "SF 合格人数", null, modifier[11] });
                itemValue.Add(new string[] { "IPTH_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "IPTH 已检人数", null, modifier[12] });
                itemValue.Add(new string[] { "IPTH_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "IPTH 合格人数", null, modifier[13] });
                itemValue.Add(new string[] { "KtV_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "KtV 已检人数", null, modifier[14] });
                itemValue.Add(new string[] { "KtV_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "KtV 合格人数", null, modifier[15] });
                itemValue.Add(new string[] { "HBsAg_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "HBsAg 已检人数", null, modifier[16] });
                itemValue.Add(new string[] { "HBsAg_POSI", "http://www.datacom.com.cn/DialysisStatisticDQ", "HBsAg 阳性人数", null, modifier[17] });
                itemValue.Add(new string[] { "Anti-HCV_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "Anti-HCV 已检人数", null, modifier[18] });
                itemValue.Add(new string[] { "Anti-HCV_POSI", "http://www.datacom.com.cn/DialysisStatisticDQ", "Anti-HCV 阳性人数", null, modifier[19] });
                itemValue.Add(new string[] { "URR_ED", "http://www.datacom.com.cn/DialysisStatisticDQ", "URR 已检人数", null, modifier[20] });
                itemValue.Add(new string[] { "URR_PASS", "http://www.datacom.com.cn/DialysisStatisticDQ", "URR 合格人数", null, modifier[21] });
                itemValue.Add(new string[] { "FR", "http://www.datacom.com.cn/DialysisStatisticDQ", "瘘管重建人数", null, modifier[22] });
                itemValue.Add(new string[] { "Out", "http://www.datacom.com.cn/DialysisStatisticDQ", "脱离人数", null, modifier[23] });
                itemValue.Add(new string[] { "Population", "http://www.datacom.com.cn/DialysisStatisticDQ", "总人数", null, modifier[24] });
                target.CommunicationCreateUpdate(commId, commIdentifier, commIdentifierSystem, categoryCode, categorySystem, categoryName, senderId, recipientId, sentDate, eventDateTime, durationUnit, itemValue);
                logger.Trace("DQ Event Date : " + eventDateTime);
                //Trace.WriteLine("DQ Event Date : " + eventDateTime);
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
                AuditObject ao = new AuditObject() { Reference = "Communication/" + commId, Name = sMdate + categoryName, Success = true };
                target.CommunicationAuditEventCreateUpdate("UL.DQ", "Statistic", categoryCode, AuditEvent.AuditEventOutcome.N0, ip, "", senderId, recipientId, "", ao);
                bCheck = true;

            }
            catch (Exception e)
            {
                logger.Error("UploadDQ error, error message: " + e.Message);
                return false;
            }
            return bCheck;

        }

        public string GetAuditEvent(string sURL)
        {
            //logger.Info("GetAuditEvent: " + sURL);
            return GetHttpRequestString(sURL);
        }

        public string ReadAuditURI()
        {
            return getSET("FHIR_SERVER") + "/AuditEvent/";
        }

        public string ReadFhirID()
        {
            string sURL = "";
            sURL = getSET("Dialysis_url") + getSET("Dialysis_Organization");
            string[] OrganizationInfo = ReadFhirOrganization(sURL);
            return OrganizationInfo[0];
        }

        public string[] ReadFhirID01()
        {
            string sURL = "";
            sURL = getSET("Dialysis_url") + getSET("Dialysis_Organization");
            string[] OrganizationInfo = ReadFhirOrganization(sURL);
            return OrganizationInfo;
        }

        private string[] GetPD(string sURL)
        {
            DataTable dt = new DataTable();

            dt = ((DataSet)(GetHttpRequest(sURL))).Tables[0].Copy();

            if (dt.Rows.Count == 0)
            {
                logger.Info("Population Distribution (PD): dt rows count = 0. ");
                string[] nodata = { "nodata" };
                return nodata;
            }

            string[] modifier = { dt.Rows[0]["A2_M"].ToString(), dt.Rows[0]["A2_F"].ToString(), dt.Rows[0]["A3_M"].ToString(), dt.Rows[0]["A3_F"].ToString(), dt.Rows[0]["A4_M"].ToString(), dt.Rows[0]["A4_F"].ToString(), dt.Rows[0]["A5_M"].ToString(), dt.Rows[0]["A5_F"].ToString(), dt.Rows[0]["A6_M"].ToString(), dt.Rows[0]["A6_F"].ToString(), dt.Rows[0]["A7_M"].ToString(), dt.Rows[0]["A7_F"].ToString(), dt.Rows[0]["A8_M"].ToString(), dt.Rows[0]["A8_F"].ToString(), dt.Rows[0]["A9_M"].ToString(), dt.Rows[0]["A9_F"].ToString(), dt.Rows[0]["Total_M"].ToString(), dt.Rows[0]["Total_F"].ToString(), dt.Rows[0]["A98_M"].ToString(), dt.Rows[0]["A98_F"].ToString(), dt.Rows[0]["A99_M"].ToString(), dt.Rows[0]["A99_F"].ToString() };
            return modifier;
        }

        private string[] GetDD(string sURL)
        {
            DataTable dt = new DataTable();

            dt = ((DataSet)(GetHttpRequest(sURL))).Tables[0].Copy();

            if (dt.Rows.Count == 0)
            {
                logger.Info("Duration Distribution (DD): dt rows count = 0. ");
                string[] nodata = { "nodata" };
                return nodata;
            }

            string[] modifier = { dt.Rows[0]["A0_M"].ToString(), dt.Rows[0]["A0_F"].ToString(), dt.Rows[0]["A1_M"].ToString(), dt.Rows[0]["A1_F"].ToString(), dt.Rows[0]["A2_M"].ToString(), dt.Rows[0]["A2_F"].ToString(), dt.Rows[0]["A3_M"].ToString(), dt.Rows[0]["A3_F"].ToString(), dt.Rows[0]["A4_M"].ToString(), dt.Rows[0]["A4_F"].ToString(), dt.Rows[0]["A5_M"].ToString(), dt.Rows[0]["A5_F"].ToString(), dt.Rows[0]["A6_M"].ToString(), dt.Rows[0]["A6_F"].ToString(), dt.Rows[0]["A7_M"].ToString(), dt.Rows[0]["A7_F"].ToString(), dt.Rows[0]["Total_M"].ToString(), dt.Rows[0]["Total_F"].ToString() };
            return modifier;
        }

        private string[] GetMR(string sURL)
        {
            DataTable dt = new DataTable();

            dt = ((DataSet)(GetHttpRequest(sURL))).Tables[0].Copy();

            if (dt.Rows.Count == 0)
            {
                logger.Info("MR: dt rows count = 0. ");
                string[] nodata = { "nodata" };
                return nodata;
            }

            string[] modifier = { dt.Rows[0]["deaths_M"].ToString(), dt.Rows[0]["deaths_F"].ToString(), dt.Rows[0]["population_M"].ToString(), dt.Rows[0]["population_F"].ToString() };
            return modifier;
        }

        private string[] GetDQ(string sURL)
        {
            DataTable dt = new DataTable();

            dt = ((DataSet)(GetHttpRequest(sURL))).Tables[0].Copy();

            if (dt.Rows.Count == 0)
            {
                logger.Info("DQ: dt rows count = 0. ");
                string[] nodata = { "nodata" };
                return nodata;
            }

            string[] modifier = { dt.Rows[0]["Hb_ED"].ToString(), dt.Rows[0]["Hb_PASS"].ToString(), dt.Rows[0]["ALB_ED"].ToString(), dt.Rows[0]["ALB_PASS"].ToString(), dt.Rows[0]["Ca_ED"].ToString(), dt.Rows[0]["Ca_PASS"].ToString(), dt.Rows[0]["P_ED"].ToString(), dt.Rows[0]["P_PASS"].ToString(), dt.Rows[0]["TS_ED"].ToString(), dt.Rows[0]["TS_PASS"].ToString(), dt.Rows[0]["SF_ED"].ToString(), dt.Rows[0]["SF_PASS"].ToString(), dt.Rows[0]["IPTH_ED"].ToString(), dt.Rows[0]["IPTH_PASS"].ToString(), dt.Rows[0]["KtV_ED"].ToString(), dt.Rows[0]["KtV_PASS"].ToString(), dt.Rows[0]["HBsAg_ED"].ToString(), dt.Rows[0]["HBsAg_POSI"].ToString(), dt.Rows[0]["Anti-HCV_ED"].ToString(), dt.Rows[0]["Anti-HCV_POSI"].ToString(), dt.Rows[0]["URR_ED"].ToString(), dt.Rows[0]["URR_PASS"].ToString(), dt.Rows[0]["FR"].ToString(), dt.Rows[0]["Out"].ToString(), dt.Rows[0]["Population"].ToString() };
            return modifier;

        }

        private string[] ReadFhirOrganization(string sURL)
        {
            DataTable dtOrganization = new DataTable();
            string orgId, orgName, orgIdentifier, orgIdentifierSystem, orgPhone, orgType, orgTypeSystem, state, city, country, line, email, partOfOrgId, partOfOrgName;
            orgId = orgName = orgIdentifier = orgIdentifierSystem = orgPhone = orgType = orgTypeSystem = state = city = country = line = email = partOfOrgId = partOfOrgName = "";
            DataSet dsXML = GetHttpRequest(sURL); 
            dtOrganization = dsXML.Tables[0].Copy();
            //dtOrganization = ((DataSet)(GetHttpRequest(sURL))).Tables[0].Copy();

            if (dtOrganization.Rows.Count == 0)
            {
                logger.Error("dtOrganization error,  dt rows count = 0. ");
                string[] nodata = { "nodata" };
                return nodata;
            }

            foreach (DataRow dr in dtOrganization.Rows)
            {
                switch (dr["genst_code"].ToString())
                {
                    case "FHIR_Id":
                        orgId = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_Name":
                        orgName = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_IdentifierSystem":
                        orgIdentifierSystem = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_Identifier":
                        orgIdentifier = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_Type":
                        orgType = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_Telecom":
                        orgPhone = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_state":
                        state = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_city":
                        city = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_country":
                        country = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_line":
                        line = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_email":
                        email = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_partOfOrgId":
                        partOfOrgId = dr["genst_desc"].ToString();
                        break;
                    case "FHIR_partOfOrgName":
                        partOfOrgName = dr["genst_desc"].ToString();
                        break;
                }
            }
            string[] OrganizationInfo = { orgId, partOfOrgId };
            return OrganizationInfo;

        }

        private DataSet GetHttpRequest(string sURL)
        {
            DataSet dsXML = new DataSet();
            string dataString = GetHttpRequestString(sURL);

            if (dataString == "-1")
            {
                DataTable Table1 = dsXML.Tables.Add("Table1");
                return dsXML;
            }

            XmlDocument bodyDoc = new XmlDocument();
            bodyDoc.LoadXml(dataString);
            dsXML.ReadXml(new XmlTextReader(new StringReader(bodyDoc.InnerXml)), XmlReadMode.ReadSchema);
            return dsXML;
        }

        private string GetHttpRequestString(string sURL)
        {
            string strResult = string.Empty;
            HttpWebResponse httpWebResponse = null;
            StreamReader sr = null;
            //logger.Info("RESTful Web Service URI: " + sURL);

            try
            {
                HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(sURL);
                httpWebResponse = httpWebRequest.GetResponse() as HttpWebResponse;
            }
            catch (Exception e)
            {
                logger.Error("Read RESTful Web Service error, error message: " + e.Message);
                return "-1";
            }

            if (httpWebResponse.StatusCode == HttpStatusCode.OK)
            {
                sr = new StreamReader(httpWebResponse.GetResponseStream());
                strResult = sr.ReadToEnd();
                return strResult;
            }
            else
            {
                logger.Error("Read RESTful Web Service , httpWebResponse error");
                return "-1";
            }
        }

        private string getSET(string sKEY)
        {
            return ConfigurationManager.AppSettings[sKEY] == null ? "" : ConfigurationManager.AppSettings[sKEY].ToString();
        }

    }
}