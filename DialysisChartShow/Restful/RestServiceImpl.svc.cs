using System;
using System.IO;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Dialysis_Chart_Show.tools;

using NLog;

namespace RestService
{
    public class RestServiceImpl : IRestServiceImpl
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();

        public AppointmentResult[] CreateAppointment(InsertAppointmentData[] rData)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DBMysql db = new DBMysql();
            List<AppointmentResult> resultArray = new List<AppointmentResult>();

            foreach (InsertAppointmentData ap in rData)
            {
                AppointmentResult result = new AppointmentResult();
                try
                {
                    result.pv_floor = ap.pv_floor;
                    result.pv_sec = ap.pv_sec;
                    result.pv_bedno = ap.pv_bedno;
                    result.mac_type = ap.mac_type;
                    result.appointment_date = ap.appointment_date;
                    result.time_type = ap.time_type;
                    result.pif_id = ap.pif_id;
                    result.status = ap.status;
                    result.descciption = ap.descciption;
                    result.create_by = ap.create_by;

                    if (String.IsNullOrWhiteSpace(ap.pv_floor))
                        throw new Exception("Empty pv_floor");
                    if (String.IsNullOrWhiteSpace(ap.pv_sec))
                        throw new Exception("Empty pv_sec");
                    if (String.IsNullOrWhiteSpace(ap.pv_bedno))
                        throw new Exception("Empty pv_bedno");
                    if (String.IsNullOrWhiteSpace(ap.appointment_date))
                        throw new Exception("Empty appointment_date");
                    else
                        DateTime.ParseExact(ap.appointment_date, "yyyy-MM-dd", culture);
                    if (String.IsNullOrWhiteSpace(ap.time_type))
                        throw new Exception("Empty time_type");
                    if (ap.pif_id == 0)
                        throw new Exception("Empty pif_id");

                    string sql = "";
                    sql += "INSERT INTO appointment (pv_floor,pv_sec,pv_bedno,mac_type,appointment_date,time_type,pif_id,status,descciption,create_by) VALUES ('";
                    sql += result.pv_floor + "','" + result.pv_sec + "','" + result.pv_bedno + "','" + result.mac_type + "','";
                    sql += result.appointment_date + "','" + result.time_type + "','" + result.pif_id + "','" + result.status + "','";
                    sql += result.descciption + "','" + result.create_by + "')";
                    if (!db.Excute(sql))
                        resultArray.Add(result);
                }
                catch (Exception e)
                {
                    result.message = e.Message;
                    resultArray.Add(result);
                }
            }

            /*var response = new CreateResponseData
            {
                appointmentResults = resultArray.ToArray()
            };
            return response;*/
            return resultArray.ToArray();
        }

        public AppointmentData[] SearchAppointment(SearchRequestData rData)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DBMysql db = new DBMysql();
            DataTable appointmentDT = new DataTable();
            List<AppointmentData> appointmentArray = new List<AppointmentData>();
            string m = "";

            try
            {
                DateTime queryStartDate = new DateTime();
                if (String.IsNullOrWhiteSpace(rData.startDate))
                    throw new Exception("Empty startDate");
                else
                    queryStartDate = DateTime.ParseExact(rData.startDate, "yyyy-MM-dd", culture);
                if (rData.period == 0)
                    throw new Exception("Empty period");

                DateTime queryEndDate = queryStartDate.AddDays(rData.period - 1);

                string sql = "SELECT id, pv_floor, pv_sec, pv_bedno, mac_type, appointment_date, time_type, pif_id, status, descciption, create_by, create_on";
                sql += " FROM appointment";
                sql += " WHERE 1=1";
                sql += " AND appointment_date >= '" + rData.startDate + "' AND appointment_date <= '" + queryEndDate.ToString("yyyy-MM-dd") + "'";

                if (!String.IsNullOrWhiteSpace(rData.pv_floor))
                    sql += " AND pv_floor = '" + rData.pv_floor + "'";
                if (!String.IsNullOrWhiteSpace(rData.pv_sec))
                    sql += " AND pv_sec = '" + rData.pv_sec + "'";

                sql += " ORDER BY appointment_date ";
                appointmentDT = db.Query(sql);

                if (appointmentDT.Rows.Count > 0)
                {
                    foreach (DataRow row in appointmentDT.Rows)
                    {
                        AppointmentData data = new AppointmentData();
                        data.id = Convert.ToInt32(row["id"].ToString());
                        data.pv_floor = row["pv_floor"].ToString();
                        data.pv_sec = row["pv_sec"].ToString();
                        data.pv_bedno = row["pv_bedno"].ToString();
                        data.mac_type = row["mac_type"].ToString();
                        data.appointment_date = row["appointment_date"].ToString();
                        data.time_type = row["time_type"].ToString();
                        data.pif_id = Convert.ToInt32(row["pif_id"].ToString());
                        data.status = row["status"].ToString();
                        data.descciption = row["descciption"].ToString();
                        data.create_by = row["create_by"].ToString();
                        data.create_on = row["create_on"].ToString();
                        appointmentArray.Add(data);
                    }
                }
            }
            catch (Exception e)
            {
                m = e.Message;
            }

            /*var response = new SearchResponseData
            {
                appointments = appointmentArray.ToArray(),
                message = m
            };
            return response;*/
            return appointmentArray.ToArray();
        }

        public DeleteResponseData DeleteAppointment(int[] rData)
        {
            DBMysql db = new DBMysql();
            string m = "";

            string sql = "";
            sql += "DELETE FROM appointment WHERE";

            if (rData.Length > 0)
            {
                int count = 0;
                foreach (int id in rData)
                {
                    if (count == 0)
                        sql += " id = " + id;
                    else
                        sql += " OR id = " + id;
                    count++;
                }
                try
                {
                    db.Excute(sql);
                }
                catch (Exception e)
                {
                    m = e.Message;
                }
            }
            var response = new DeleteResponseData
            {
                message = m
            };
            return response;
        }

        public Stream ReadAppointment(string startDate, string period, string pv_floor, string pv_sec)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DBMysql db = new DBMysql();
            DataTable appointmentDT = new DataTable();
            string resultJson = "";

            try
            {
                DateTime queryStartDate = new DateTime();
                if (String.IsNullOrWhiteSpace(startDate))
                    throw new Exception("Empty startDate");
                else
                    queryStartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", culture);
                if (String.IsNullOrWhiteSpace(period))
                    throw new Exception("Empty period");

                DateTime queryEndDate = queryStartDate.AddDays(Convert.ToDouble(period) - 1);

                string sql = "SELECT id, pv_floor, pv_sec, pv_bedno, mac_type, appointment_date, time_type, pif_id, status, descciption, create_by, create_on";
                sql += " FROM appointment";
                sql += " WHERE 1=1";
                sql += " AND appointment_date >= '" + startDate + "' AND appointment_date <= '" + queryEndDate.ToString("yyyy-MM-dd") + "'";

                if (!String.IsNullOrWhiteSpace(pv_floor) && !pv_floor.Equals("~"))
                    sql += " AND pv_floor = '" + pv_floor + "'";
                if (!String.IsNullOrWhiteSpace(pv_sec) && !pv_sec.Equals("~"))
                    sql += " AND pv_sec = '" + pv_sec + "'";

                sql += " ORDER BY appointment_date ";
                appointmentDT = db.Query(sql);

                if (appointmentDT.Rows.Count > 0)
                {
                    resultJson = JsonConvert.SerializeObject(appointmentDT, Formatting.None);
                }
            }
            catch (Exception e)
            {
                resultJson = e.Message;
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(resultJson));
        }

        public AppointmentResult[] CreateAppointmentStatic(InsertAppointmentData[] rData)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DBMysql db = new DBMysql();
            List<AppointmentResult> resultArray = new List<AppointmentResult>();

            foreach (InsertAppointmentData ap in rData)
            {
                AppointmentResult result = new AppointmentResult();
                try
                {
                    result.pv_floor = ap.pv_floor;
                    result.pv_sec = ap.pv_sec;
                    result.pv_bedno = ap.pv_bedno;
                    result.mac_type = ap.mac_type;
                    result.appointment_date = ap.appointment_date;
                    result.time_type = ap.time_type;
                    result.pif_id = ap.pif_id;
                    result.status = ap.status;
                    result.descciption = ap.descciption;
                    result.create_by = ap.create_by;

                    if (String.IsNullOrWhiteSpace(ap.pv_floor))
                        throw new Exception("Empty pv_floor");
                    if (String.IsNullOrWhiteSpace(ap.pv_sec))
                        throw new Exception("Empty pv_sec");
                    if (String.IsNullOrWhiteSpace(ap.pv_bedno))
                        throw new Exception("Empty pv_bedno");
                    if (String.IsNullOrWhiteSpace(ap.appointment_date))
                        throw new Exception("Empty appointment_date");
                    else
                        DateTime.ParseExact(ap.appointment_date, "yyyy-MM-dd", culture);
                    if (String.IsNullOrWhiteSpace(ap.time_type))
                        throw new Exception("Empty time_type");
                    if (ap.pif_id == 0)
                        throw new Exception("Empty pif_id");

                    string sql = "";
                    sql += "INSERT INTO appointment_static (pv_floor,pv_sec,pv_bedno,mac_type,appointment_date,time_type,pif_id,status,descciption,create_by) VALUES ('";
                    sql += result.pv_floor + "','" + result.pv_sec + "','" + result.pv_bedno + "','" + result.mac_type + "','";
                    sql += result.appointment_date + "','" + result.time_type + "','" + result.pif_id + "','" + result.status + "','";
                    sql += result.descciption + "','" + result.create_by + "')";
                    if (!db.Excute(sql))
                        resultArray.Add(result);
                }
                catch (Exception e)
                {
                    result.message = e.Message;
                    resultArray.Add(result);
                }
            }

            /*var response = new CreateResponseData
            {
                appointmentResults = resultArray.ToArray()
            };
            return response;*/
            return resultArray.ToArray();
        }

        public AppointmentData[] SearchAppointmentStatic(SearchRequestData rData)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DBMysql db = new DBMysql();
            DataTable appointmentDT = new DataTable();
            List<AppointmentData> appointmentArray = new List<AppointmentData>();
            string m = "";

            try
            {
                DateTime queryStartDate = new DateTime();
                if (String.IsNullOrWhiteSpace(rData.startDate))
                    throw new Exception("Empty startDate");
                else
                    queryStartDate = DateTime.ParseExact(rData.startDate, "yyyy-MM-dd", culture);
                if (rData.period == 0)
                    throw new Exception("Empty period");

                DateTime queryEndDate = queryStartDate.AddDays(rData.period - 1);

                string sql = "SELECT id, pv_floor, pv_sec, pv_bedno, mac_type, appointment_date, time_type, pif_id, status, descciption, create_by, create_on";
                sql += " FROM appointment_static";
                sql += " WHERE 1=1";
                sql += " AND appointment_date >= '" + rData.startDate + "' AND appointment_date <= '" + queryEndDate.ToString("yyyy-MM-dd") + "'";

                if (!String.IsNullOrWhiteSpace(rData.pv_floor))
                    sql += " AND pv_floor = '" + rData.pv_floor + "'";
                if (!String.IsNullOrWhiteSpace(rData.pv_sec))
                    sql += " AND pv_sec = '" + rData.pv_sec + "'";

                sql += " ORDER BY appointment_date ";
                appointmentDT = db.Query(sql);

                if (appointmentDT.Rows.Count > 0)
                {
                    foreach (DataRow row in appointmentDT.Rows)
                    {
                        AppointmentData data = new AppointmentData();
                        data.id = Convert.ToInt32(row["id"].ToString());
                        data.pv_floor = row["pv_floor"].ToString();
                        data.pv_sec = row["pv_sec"].ToString();
                        data.pv_bedno = row["pv_bedno"].ToString();
                        data.mac_type = row["mac_type"].ToString();
                        data.appointment_date = row["appointment_date"].ToString();
                        data.time_type = row["time_type"].ToString();
                        data.pif_id = Convert.ToInt32(row["pif_id"].ToString());
                        data.status = row["status"].ToString();
                        data.descciption = row["descciption"].ToString();
                        data.create_by = row["create_by"].ToString();
                        data.create_on = row["create_on"].ToString();
                        appointmentArray.Add(data);
                    }
                }
            }
            catch (Exception e)
            {
                m = e.Message;
            }

            /*var response = new SearchResponseData
            {
                appointments = appointmentArray.ToArray(),
                message = m
            };
            return response;*/
            return appointmentArray.ToArray();
        }

        public DeleteResponseData DeleteAppointmentStatic(int[] rData)
        {
            DBMysql db = new DBMysql();
            string m = "";

            string sql = "";
            sql += "DELETE FROM appointment_static WHERE";

            if (rData.Length > 0)
            {
                int count = 0;
                foreach (int id in rData)
                {
                    if (count == 0)
                        sql += " id = " + id;
                    else
                        sql += " OR id = " + id;
                    count++;
                }
                try
                {
                    db.Excute(sql);
                }
                catch (Exception e)
                {
                    m = e.Message;
                }
            }
            var response = new DeleteResponseData
            {
                message = m
            };
            return response;
        }

        public Stream ReadAppointmentStatic(string startDate, string period, string pv_floor, string pv_sec)
        {
            IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DBMysql db = new DBMysql();
            DataTable appointmentDT = new DataTable();
            string resultJson = "";

            try
            {
                DateTime queryStartDate = new DateTime();
                if (String.IsNullOrWhiteSpace(startDate))
                    throw new Exception("Empty startDate");
                else
                    queryStartDate = DateTime.ParseExact(startDate, "yyyy-MM-dd", culture);
                if (String.IsNullOrWhiteSpace(period))
                    throw new Exception("Empty period");

                DateTime queryEndDate = queryStartDate.AddDays(Convert.ToDouble(period) - 1);

                string sql = "SELECT id, pv_floor, pv_sec, pv_bedno, mac_type, appointment_date, time_type, pif_id, status, descciption, create_by, create_on";
                sql += " FROM appointment_static";
                sql += " WHERE 1=1";
                sql += " AND appointment_date >= '" + startDate + "' AND appointment_date <= '" + queryEndDate.ToString("yyyy-MM-dd") + "'";

                if (!String.IsNullOrWhiteSpace(pv_floor) && !pv_floor.Equals("~"))
                    sql += " AND pv_floor = '" + pv_floor + "'";
                if (!String.IsNullOrWhiteSpace(pv_sec) && !pv_sec.Equals("~"))
                    sql += " AND pv_sec = '" + pv_sec + "'";

                sql += " ORDER BY appointment_date ";
                appointmentDT = db.Query(sql);

                if (appointmentDT.Rows.Count > 0)
                {
                    resultJson = JsonConvert.SerializeObject(appointmentDT, Formatting.None);
                }
            }
            catch (Exception e)
            {
                resultJson = e.Message;
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(resultJson));
        }

        public DataSet ReadFhirOrganization()
        {
            //IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DataSet ds = new DataSet();
            DBMysql db = new DBMysql();
            DataTable dtOrganization = new DataTable();
            string result = "";

            try
            {
                string sql = "SELECT genst_code,genst_desc FROM general_setup WHERE genst_ctg = 'HOSPITAL' AND genst_code LIKE 'FHIR_%' ORDER BY genst_id";
                dtOrganization = db.Query(sql);
                ds.Tables.Add(dtOrganization);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return ds;

        }

        //=====20160406 modify by ssi begin ==========================================================================================================================
        public DataSet ReadFhirPopulationDistribution(string sTrdate)
        {
            //IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DataSet ds = new DataSet();
            DBMysql db = new DBMysql();
            DataTable dtOrganization = new DataTable();
            string result = "";

            DataTable dt = new DataTable();
            string sColumn = "";
            string Trdate = "";
            DateTime dEND = DateTime.Now;
            dEND = dEND.AddMonths(-1);
            Trdate = sTrdate == "" ? dEND.ToString("yyyy-MM-dd") : sTrdate;

            dt.Columns.Add("Total_M");
            dt.Columns.Add("Total_F");
            dt.Columns.Add("Total_A");
            dt.Columns.Add("A98_M");
            dt.Columns.Add("A98_F");
            dt.Columns.Add("A98_A");
            dt.Columns.Add("A99_M");
            dt.Columns.Add("A99_F");
            dt.Columns.Add("A99_A");

            for (int i = 2; i < 10; i++)
            {
                sColumn = "A" + i.ToString() + "_M";
                dt.Columns.Add(sColumn);
                sColumn = "A" + i.ToString() + "_F";
                dt.Columns.Add(sColumn);
                sColumn = "A" + i.ToString() + "_A";
                dt.Columns.Add(sColumn);
            };

            DataRow dr;

            try
            {
                string sql = "SELECT FLOOR((EXTRACT(YEAR FROM CURDATE())-CAST(SUBSTR(PIF_DOB,1,4) AS UNSIGNED INTEGER))/10 ) AS AGE,  " +
                               " PIF_SEX AS SEX, COUNT(*) AS CNT " +
                               " FROM pat_info  " +
                               " LEFT JOIN zinfo_a_07  f ON pat_info.pif_id = f.pat_id AND f.opt_1 IN ('','5') AND f.info_date <= '" + Trdate + "'  " +
                             " WHERE SUBSTR(PIF_DOB,1,1) IN ('1','2')  " +
                                " AND PIF_DOB<>CURDATE()  " +
                                " AND PIF_SEX<>'' AND pat_info.pif_id NOT IN (SELECT b.pif_id FROM pat_info b " +
               " INNER JOIN (SELECT a.pat_id,a.opt_1, a.info_date FROM zinfo_a_07 a " +
               " INNER JOIN (SELECT pat_id,max(info_date) AS last_date FROM zinfo_a_07 GROUP BY pat_id) b " +
                 " ON a.pat_id=b.pat_id AND a.info_date=b.last_date) f " +
                 " ON b.pif_id = f.pat_id AND f.opt_1 IN ('1','2','3','4') AND f.info_date<='" + Trdate + "')  " +
                              " GROUP BY AGE, SEX  " +
                              " UNION ALL   " +
                " SELECT 98 AS AGE, PIF_SEX AS SEX, COUNT(*) AS CNT   " +
                               " FROM pat_info  " +
                                " LEFT JOIN zinfo_a_07  f ON pat_info.pif_id = f.pat_id AND f.opt_1 IN ('','5') AND f.info_date <= '" + Trdate + "' " +
                                 " JOIN blood_group a ON pat_info.pif_ic = a.bgrp_patic AND NOT (bgrp_aids='N' AND bgrp_syphilis='N' AND bgrp_hbv='N' AND bgrp_hcv='N')  " +
                              " WHERE SUBSTR(PIF_DOB,1,1) IN ('1','2')   " +
                                " AND PIF_DOB<= CURDATE()  " +
                                " AND PIF_SEX<>'' AND pat_info.pif_id NOT IN (SELECT b.pif_id FROM pat_info b " +
               " INNER JOIN (SELECT a.pat_id,a.opt_1, a.info_date FROM zinfo_a_07 a " +
               " INNER JOIN (SELECT pat_id,max(info_date) AS last_date FROM zinfo_a_07 GROUP BY pat_id) b " +
                 " ON a.pat_id=b.pat_id AND a.info_date=b.last_date) f " +
                 " ON b.pif_id = f.pat_id AND f.opt_1 IN ('1','2','3','4') AND f.info_date<='" + Trdate + "')   " +
                              " GROUP by SEX  " +
                              " UNION ALL   " +
                " SELECT 99 AS AGE, pat_info.PIF_SEX AS SEX, COUNT(*) AS CNT     " +
                    " FROM pat_info    " +
                    " JOIN blood_group a ON pat_info.pif_ic = a.bgrp_patic AND (a.bgrp_aids='N' AND a.bgrp_syphilis='N' AND a.bgrp_hbv='N' AND a.bgrp_hcv='N')     " +
                    " WHERE SUBSTR(pat_info.PIF_DOB,1,1) IN ('1','2')    " +
                    " AND pat_info.PIF_DOB<= CURDATE()   " +
                    " AND pat_info.PIF_SEX<>'' AND pat_info.pif_id NOT IN (SELECT b.pif_id FROM pat_info b " +
               " INNER JOIN (SELECT a.pat_id,a.opt_1, a.info_date FROM zinfo_a_07 a " +
               " INNER JOIN (SELECT pat_id,max(info_date) AS last_date FROM zinfo_a_07 GROUP BY pat_id) b " +
                 " ON a.pat_id=b.pat_id AND a.info_date=b.last_date) f " +
                 " ON b.pif_id = f.pat_id AND f.opt_1 IN ('1','2','3','4') AND f.info_date<='" + Trdate + "')     " +
                    " GROUP by SEX   " +
                " union ALL   " +
                " SELECT 99 AS AGE, pat_info.PIF_SEX AS SEX, COUNT(*) AS CNT     " +
                    " FROM pat_info WHERE pif_ic NOT IN (SELECT bgrp_patic FROM blood_group)   " +
                  " AND SUBSTR(PIF_DOB,1,1) IN ('1','2')  " +
                  " AND PIF_DOB < CURDATE()  " +
                  " AND PIF_SEX <> '' AND pat_info.pif_id NOT IN (SELECT b.pif_id FROM pat_info b " +
               " INNER JOIN (SELECT a.pat_id,a.opt_1, a.info_date FROM zinfo_a_07 a " +
               " INNER JOIN (SELECT pat_id,max(info_date) AS last_date FROM zinfo_a_07 GROUP BY pat_id) b " +
                 " ON a.pat_id=b.pat_id AND a.info_date=b.last_date) f " +
                 " ON b.pif_id = f.pat_id AND f.opt_1 IN ('1','2','3','4') AND f.info_date<='" + Trdate + "')   " +
                    " GROUP by SEX   " ;


                DataTable dtAGE = db.Query(sql);

                dr = dt.NewRow();
                dr["Total_M"] = "0";
                dr["Total_F"] = "0";
                dr["Total_A"] = "0";
                dr["A98_M"] = "0";
                dr["A98_F"] = "0";
                dr["A98_A"] = "0";
                dr["A99_M"] = "0";
                dr["A99_F"] = "0";
                dr["A99_A"] = "0";

                for (int i = 2; i <= 9; i++)
                {
                    dr["A" + i.ToString() + "_M"] = "0";
                    dr["A" + i.ToString() + "_F"] = "0";
                    dr["A" + i.ToString() + "_A"] = "0";
                }

                string sSEX;
                int iAGE;
                int iCNT;
                for (int i = 0; i < dtAGE.Rows.Count; i++)
                {
                    sSEX = dtAGE.Rows[i]["SEX"].ToString();
                    iAGE = Convert.ToInt16(dtAGE.Rows[i]["AGE"].ToString());
                    iCNT = Convert.ToInt16(dtAGE.Rows[i]["CNT"].ToString());
                    if (iAGE == 98)
                    {
                        dr["A98_A"] = Convert.ToString(Convert.ToInt16(dr["A98_A"]) + iCNT);
                        if (sSEX == "M" || sSEX == "F")
                        {
//                            dr["Total_" + sSEX] = Convert.ToInt16(dr["Total_" + sSEX]) + iCNT;
                            dr["A98_" + sSEX] = Convert.ToString(Convert.ToInt16(dr["A98_" + sSEX]) + iCNT);
                        };
                        continue;
                    };
                    if (iAGE == 99)
                    {
                        dr["A99_A"] = Convert.ToString(Convert.ToInt16(dr["A99_A"]) + iCNT);
                        if (sSEX == "M" || sSEX == "F")
                        {
//                            dr["Total_" + sSEX] = Convert.ToInt16(dr["Total_" + sSEX]) + iCNT;
                            dr["A99_" + sSEX] = Convert.ToString(Convert.ToInt16(dr["A99_" + sSEX]) + iCNT);
                        };
                        continue;
                    };


                    if (iAGE > 9) iAGE = 9;
                    else if (iAGE <= 2) iAGE = 2;

                    dr["Total_A"] = Convert.ToInt16(dr["Total_A"]) + iCNT;
                    dr["A" + iAGE.ToString() + "_A"] = Convert.ToString(Convert.ToInt16(dr["A" + iAGE.ToString() + "_A"]) + iCNT);

                    if (sSEX == "M" || sSEX == "F")
                    {
                        dr["Total_" + sSEX] = Convert.ToInt16(dr["Total_" + sSEX]) + iCNT;
                        dr["A" + iAGE.ToString() + "_" + sSEX] = Convert.ToString(Convert.ToInt16(dr["A" + iAGE.ToString() + "_" + sSEX]) + iCNT);
                    }
                }

                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return ds;
        }
        //=====20160406 modify by ssi end ==========================================================================================================================

        //=====20160330 add by ssi begin ==========================================================================================================================
        public DataSet ReadFhirDurationDistribution(string sTrdate)
        {
            //IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DataSet ds = new DataSet();
            DBMysql db = new DBMysql();
            DataTable dtOrganization = new DataTable();
            string result = "";

            DataTable dt = new DataTable();
            string sColumn = "";
            string Trdate = "";
            DateTime dEND = DateTime.Now;
            dEND = dEND.AddMonths(-1);
            Trdate = sTrdate == "" ? dEND.ToString("yyyy-MM-dd")  : sTrdate ;

            dt.Columns.Add("Total_M");
            dt.Columns.Add("Total_F");
            dt.Columns.Add("Total_A");

            for (int i = 0; i < 10; i++)
            {
                sColumn = "A" + i.ToString() + "_M";
                dt.Columns.Add(sColumn);
                sColumn = "A" + i.ToString() + "_F";
                dt.Columns.Add(sColumn);
                sColumn = "A" + i.ToString() + "_A";
                dt.Columns.Add(sColumn);
            }
            DataRow dr;
            try
            {
              string sql = "SELECT (EXTRACT(YEAR FROM CURDATE())-CAST(SUBSTR(pif_createdate,1,4) AS UNSIGNED INTEGER)) as AGE, PIF_SEX AS SEX, COUNT(*) as CNT  " +
                               " FROM pat_info  " +
                               " LEFT JOIN zinfo_a_07  f ON pat_info.pif_id = f.pat_id AND f.opt_1 NOT IN ('','5') AND  f.info_date <= '" + Trdate + "' " +
                              " WHERE pif_dob<= CURDATE()  " +
                                " AND PIF_SEX<>'' AND pat_info.pif_id NOT IN (SELECT b.pif_id FROM pat_info b " +
                " INNER JOIN (SELECT a.pat_id,a.opt_1, a.info_date FROM zinfo_a_07 a " +
               " INNER JOIN (SELECT pat_id,max(info_date) AS last_date FROM zinfo_a_07 GROUP BY pat_id) b " +
                 " ON a.pat_id=b.pat_id AND a.info_date=b.last_date) f  " +
                 " ON b.pif_id = f.pat_id AND f.opt_1 IN ('1','2','3','4') AND f.info_date<='" + Trdate + "')  " +
                              " GROUP BY AGE, SEX";

                DataTable dtAGE = db.Query(sql);
                logger.Trace("ReadFhirDurationDistribution SQL: " + sql);

                dr = dt.NewRow();
                dr["Total_M"] = "0";
                dr["Total_F"] = "0";
                dr["Total_A"] = "0";

                for (int i = 0; i <= 9; i++)
                {
                    dr["A" + i.ToString() + "_M"] = "0";
                    dr["A" + i.ToString() + "_F"] = "0";
                    dr["A" + i.ToString() + "_A"] = "0";
                }

                string sSEX;
                int iAGE;
                int iiAGE;
                int iCNT;
                for (int i = 0; i < dtAGE.Rows.Count; i++)
                {
                    sSEX = dtAGE.Rows[i]["SEX"].ToString();
                    iAGE = Convert.ToInt16(dtAGE.Rows[i]["AGE"].ToString());
                    iCNT = Convert.ToInt16(dtAGE.Rows[i]["CNT"].ToString());
                    switch (iAGE)
                    {
                        case 0: iiAGE = 0; break;
                        case 1: iiAGE = 1; break;
                        case 2: iiAGE = 1; break;
                        case 3: iiAGE = 2; break;
                        case 4: iiAGE = 2; break;
                        case 5: iiAGE = 3; break;
                        case 6: iiAGE = 3; break;
                        case 7: iiAGE = 4; break;
                        case 8: iiAGE = 4; break;
                        case 9: iiAGE = 5; break;
                        default: iiAGE = 5; break;
                    }
                    //                        if (iAGE > 9)
                    //                            iAGE = 9;
                    //                        else if (iAGE <= 2)
                    //                            iAGE = 2;

                    dr["Total_A"] = Convert.ToInt16(dr["Total_A"]) + iCNT;
                    dr["A" + iiAGE.ToString() + "_A"] = Convert.ToString(Convert.ToInt16(dr["A" + iiAGE.ToString() + "_A"]) + iCNT);

                    if (sSEX == "M" || sSEX == "F")
                    {
                        dr["Total_" + sSEX] = Convert.ToInt16(dr["Total_" + sSEX]) + iCNT;
                        dr["A" + iiAGE.ToString() + "_" + sSEX] = Convert.ToString(Convert.ToInt16(dr["A" + iiAGE.ToString() + "_" + sSEX]) + iCNT);
                    }
                }

                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return ds;

        }
        //=====20160330 add by ssi end ==========================================================================================================================

        //=====20160407 add by ssi begin ==========================================================================================================================
        public DataSet ReadFhirMRDistribution(string sTrdate)
        {
            //IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DataSet ds = new DataSet();
            DBMysql db = new DBMysql();
            DataTable dtOrganization = new DataTable();
            string result = "";

            DataTable dt = new DataTable();
            string sColumn = "";

            dt.Columns.Add("deaths_M");
            dt.Columns.Add("deaths_F");
            dt.Columns.Add("population_M");
            dt.Columns.Add("population_F");

            DataRow dr;
            string Trdate = "", sql = "";
            DateTime dEND = DateTime.Now;
            dEND = dEND.AddMonths(-1);
            //Trdate = dEND.ToString("yyyy-MM") + "%";
            //string sql = "SELECT genst_code,genst_desc FROM general_setup WHERE genst_ctg = 'HOSPITAL' AND genst_code = 'Trdate' ORDER BY genst_id";
            //DataTable dt2 = db.Query(sql);
            //if (dt2.Rows.Count >= 1) 
            //    Trdate = dt2.Rows[0]["genst_desc"].ToString();
            logger.Trace("ReadFhirMRDistribution 查詢月份: " + sTrdate);
            Trdate = sTrdate ==  "" ? dEND.ToString("yyyy-MM") + "%" : sTrdate +"%";

            try
            {
              sql = "SELECT 'population' as DESCR, PIF_SEX AS SEX, COUNT(*) as CNT  " +
                 " FROM pat_info B  " +
                    " LEFT JOIN zinfo_a_07 f ON B.pif_id = f.pat_id AND f.opt_1 IN ('','5')  " +
                  " WHERE b.PIF_SEX<>''  " +
                    " AND SUBSTR(b.PIF_DOB,1,1) IN ('1','2') " +
                    " AND b.PIF_DOB<= CURDATE()  " +
                  " GROUP by SEX  " +
                  "UNION ALL  " +
                 "SELECT 'deaths' as DESCR, b.PIF_SEX AS SEX, COUNT(*) as CNT " +
                                 " FROM zinfo_a_07 A  " +
                                 " LEFT JOIN pat_info B ON  A.pat_id=B.pif_id AND A.opt_1 IN ('4')  " +
                                 " LEFT JOIN zinfo_f_012 C ON A.pat_id=C.pat_id  " +
                                " WHERE A.info_date like '" + Trdate  + "' "+
                                  " AND b.PIF_SEX<>''  " +
                                  " AND SUBSTR(b.PIF_DOB,1,1) IN ('1','2') " +
                                  " AND b.PIF_DOB<= CURDATE()  " +
                                " GROUP by SEX ";

                DataTable dtAGE = db.Query(sql);
                logger.Trace("ReadFhirMRDistribution SQL: " + sql);

                dr = dt.NewRow();
                dr["deaths_M"] = "0";
                dr["deaths_F"] = "0";
                dr["population_M"] = "0";
                dr["population_F"] = "0";
                string sSEX; string sDESCR;

                for (int i = 0; i < dtAGE.Rows.Count; i++)
                {
                    sSEX = dtAGE.Rows[i]["SEX"].ToString();
                    sDESCR = dtAGE.Rows[i]["DESCR"].ToString();
                    if (sSEX == "M" || sSEX == "F")
                    {
                        dr[sDESCR + "_" + sSEX] = dtAGE.Rows[i]["CNT"].ToString();
                    }
                }
                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                result = e.Message;
            }
            return ds;
        }
        //=====20160407 add by ssi end ==========================================================================================================================

        //=====20160407 add by ssi begin ==========================================================================================================================
        public DataSet ReadFhirDQDistribution(string sTrdate)
        {
            //IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DataSet ds = new DataSet();
            DBMysql db = new DBMysql();
            DataTable dtOrganization = new DataTable();
            string result = "";

            DataTable dt = new DataTable();
            string Trdate = "";
            DateTime dEND = DateTime.Now;
            dEND = dEND.AddMonths(-1);
            //Trdate = dEND.ToString("yyyy-MM") + "%";
            //string sSQL = "SELECT genst_code,genst_desc FROM general_setup WHERE genst_ctg = 'HOSPITAL' AND genst_code = 'Trdate' ORDER BY genst_id";
            //DataTable dt2 = db.Query(sSQL);
            //if (dt2.Rows.Count >= 1) 
            //    Trdate = dt2.Rows[0]["genst_desc"].ToString();
            logger.Trace("ReadFhirDQDistribution 查詢月份: " + sTrdate);
            Trdate = sTrdate == "" ? dEND.ToString("yyyy-MM") + "%" : sTrdate + "%";
            
            dt.Columns.Add("Hb_ED"); dt.Columns.Add("Hb_PASS");
            dt.Columns.Add("ALB_ED"); dt.Columns.Add("ALB_PASS");
            dt.Columns.Add("Ca_ED"); dt.Columns.Add("Ca_PASS");
            dt.Columns.Add("P_ED"); dt.Columns.Add("P_PASS");
            dt.Columns.Add("TS_ED"); dt.Columns.Add("TS_PASS");
            dt.Columns.Add("SF_ED"); dt.Columns.Add("SF_PASS");
            dt.Columns.Add("IPTH_ED"); dt.Columns.Add("IPTH_PASS");
            dt.Columns.Add("KtV_ED"); dt.Columns.Add("KtV_PASS");
            dt.Columns.Add("HBsAg_ED"); dt.Columns.Add("HBsAg_POSI");
            dt.Columns.Add("Anti-HCV_ED"); dt.Columns.Add("Anti-HCV_POSI");
            dt.Columns.Add("Out"); dt.Columns.Add("Population");
            dt.Columns.Add("URR_ED"); dt.Columns.Add("URR_PASS");
            dt.Columns.Add("FR");
            DataRow dr;
            string sRITEM_LOW1 = ""; string sRITEM_HIGH1 = ""; int sum; string sRESULT_VALUE_N = "";
            double zRITEM_LOW1 = 0; double zRITEM_HIGH1 = 99999; double zRESULT_VALUE_N = 0;
            try
            {
                dr = dt.NewRow();

                //=====20160411 add by ssi begin ==========================================================================================================================
                //[血红蛋白Hb.]           
                //找檢查項目 取出合格區間上下值
                string sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4003' ";
                logger.Trace("找檢查項目 血红蛋白Hb 取出合格區間上下值 SQL: " + sql);
                DataTable dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    sRITEM_LOW1 = dtCODE1.Rows[0]["RITEM_LOW1"].ToString();
                    sRITEM_HIGH1 = dtCODE1.Rows[0]["RITEM_HIGH1"].ToString();
                    if (!Double.TryParse(sRITEM_LOW1, out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(sRITEM_HIGH1, out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = "SELECT (SELECT RESULT_VALUE_N FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4003') as RESULT_VALUE_N, a.PAT_NO, a.RESULT_DATE " +
                    " FROM a_result_log AS A " +
                    " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND A.PAT_NO = f.pat_id " +
                    " WHERE A.RESULT_VER=0 AND A.RESULT_CODE='4003'  " +
                      " AND A.RESULT_DATE like '" + Trdate + "' " +
                    " GROUP BY a.PAT_NO ";
                logger.Trace("找檢查項目受檢人 血红蛋白Hb 檢驗值 SQL: " + sql);
                DataTable dtAGE = db.Query(sql);
                dr["Hb_ED"] = "0"; dr["Hb_PASS"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["Hb_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        if (!Double.TryParse(dtAGE.Rows[i]["RESULT_VALUE_N"].ToString(), out zRESULT_VALUE_N)) zRESULT_VALUE_N = 99999;
                        if (zRITEM_LOW1 <= zRESULT_VALUE_N && zRESULT_VALUE_N <= zRITEM_HIGH1) sum++;
                    }
                    dr["Hb_PASS"] = Convert.ToString(sum);
                }

                //=====20160411 add by ssi begin ==========================================================================================================================
                //[血清白蛋白ALB.] 
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4008' ";
                logger.Trace("找檢查項目 血清白蛋白ALB 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    sRITEM_LOW1 = dtCODE1.Rows[0]["RITEM_LOW1"].ToString();
                    sRITEM_HIGH1 = dtCODE1.Rows[0]["RITEM_HIGH1"].ToString();
                    if (!Double.TryParse(sRITEM_LOW1, out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(sRITEM_HIGH1, out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = "SELECT (SELECT RESULT_VALUE_N FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4008') as RESULT_VALUE_N, a.PAT_NO, max(a.RESULT_DATE) " +
                   " FROM a_result_log AS A " +
                   " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND A.PAT_NO = f.pat_id " +
                   " WHERE A.RESULT_VER=0 AND A.RESULT_CODE='4008'  " +
                     " AND A.RESULT_DATE like '" + Trdate + "' " +
                   " GROUP BY a.PAT_NO ";
                logger.Trace("找檢查項目受檢人 血清白蛋白ALB 檢驗值 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["ALB_ED"] = "0"; dr["ALB_PASS"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["ALB_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        sRESULT_VALUE_N = dtAGE.Rows[i]["RESULT_VALUE_N"].ToString();
                        if (sRESULT_VALUE_N == "") sRESULT_VALUE_N = "0";
                        try
                        {
                            if (zRITEM_LOW1 <= Convert.ToDouble(sRESULT_VALUE_N) && Convert.ToDouble(sRESULT_VALUE_N) <= zRITEM_HIGH1)
                            {
                                sum += 1;
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    dr["ALB_PASS"] = Convert.ToString(sum);
                }

                //=====20160412 add by ssi begin ==========================================================================================================================
                //[鈣Ca.]  
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4021' ";
                logger.Trace("找檢查項目 鈣Ca 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    sRITEM_LOW1 = dtCODE1.Rows[0]["RITEM_LOW1"].ToString();
                    sRITEM_HIGH1 = dtCODE1.Rows[0]["RITEM_HIGH1"].ToString();
                    if (!Double.TryParse(sRITEM_LOW1, out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(sRITEM_HIGH1, out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = "SELECT (SELECT RESULT_VALUE_N FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4021') as RESULT_VALUE_N, a.PAT_NO, max(a.RESULT_DATE) " +
                  " FROM a_result_log AS A " +
                  " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND A.PAT_NO = f.pat_id " +
                  " WHERE A.RESULT_VER=0 AND A.RESULT_CODE='4021'  " +
                    " AND A.RESULT_DATE like '" + Trdate + "' " +
                  " GROUP BY a.PAT_NO ";
                logger.Trace("找檢查項目受檢人 鈣Ca 檢驗值 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["Ca_ED"] = "0"; dr["Ca_PASS"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["Ca_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        sRESULT_VALUE_N = dtAGE.Rows[i]["RESULT_VALUE_N"].ToString();
                        if (sRESULT_VALUE_N == "") sRESULT_VALUE_N = "0";
                        try
                        {
                            if (zRITEM_LOW1 <= Convert.ToDouble(sRESULT_VALUE_N) && Convert.ToDouble(sRESULT_VALUE_N) <= zRITEM_HIGH1)
                            {
                                sum += 1;
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    dr["Ca_PASS"] = Convert.ToString(sum);
                }

                //=====20160412 add by ssi begin ==========================================================================================================================
                //[磷P.]  
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4023' ";
                logger.Trace("找檢查項目 磷P 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    sRITEM_LOW1 = dtCODE1.Rows[0]["RITEM_LOW1"].ToString();
                    sRITEM_HIGH1 = dtCODE1.Rows[0]["RITEM_HIGH1"].ToString();
                    if (!Double.TryParse(sRITEM_LOW1, out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(sRITEM_HIGH1, out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = "SELECT (SELECT RESULT_VALUE_N FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4023') as RESULT_VALUE_N, a.PAT_NO, max(a.RESULT_DATE) " +
                  " FROM a_result_log AS A " +
                  " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND A.PAT_NO = f.pat_id " +
                  " WHERE A.RESULT_VER=0 AND A.RESULT_CODE='4023'  " +
                    " AND A.RESULT_DATE like '" + Trdate + "' " +
                  " GROUP BY a.PAT_NO ";
                logger.Trace("找檢查項目受檢人 磷P 檢驗值 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["P_ED"] = "0"; dr["P_PASS"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["P_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        sRESULT_VALUE_N = dtAGE.Rows[i]["RESULT_VALUE_N"].ToString();
                        if (sRESULT_VALUE_N == "") sRESULT_VALUE_N = "0";
                        try
                        {
                            if (zRITEM_LOW1 <= Convert.ToDouble(sRESULT_VALUE_N) && Convert.ToDouble(sRESULT_VALUE_N) <= zRITEM_HIGH1)
                            {
                                sum += 1;
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    dr["P_PASS"] = Convert.ToString(sum);
                }

                //=====20160412 add by ssi begin ==========================================================================================================================
                //[轉鐵蛋白飽合度TS.] 
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4050' ";
                logger.Trace("找檢查項目 轉鐵蛋白飽合度TS 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    sRITEM_LOW1 = dtCODE1.Rows[0]["RITEM_LOW1"].ToString();
                    sRITEM_HIGH1 = dtCODE1.Rows[0]["RITEM_HIGH1"].ToString();
                    if (!Double.TryParse(sRITEM_LOW1, out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(sRITEM_HIGH1, out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = "SELECT (SELECT RESULT_VALUE_N FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4050') as RESULT_VALUE_N, a.PAT_NO, max(a.RESULT_DATE) " +
                  " FROM a_result_log AS A " +
                  " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND A.PAT_NO = f.pat_id " +
                  " WHERE A.RESULT_VER=0 AND A.RESULT_CODE='4050'  " +
                    " AND A.RESULT_DATE like '" + Trdate + "' " +
                  " GROUP BY a.PAT_NO ";
                logger.Trace("找檢查項目受檢人 轉鐵蛋白飽合度TS 檢驗值 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["TS_ED"] = "0"; dr["TS_PASS"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["TS_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        sRESULT_VALUE_N = dtAGE.Rows[i]["RESULT_VALUE_N"].ToString();
                        if (sRESULT_VALUE_N == "") sRESULT_VALUE_N = "0";
                        try
                        {
                            if (zRITEM_LOW1 <= Convert.ToDouble(sRESULT_VALUE_N) && Convert.ToDouble(sRESULT_VALUE_N) <= zRITEM_HIGH1)
                            {
                                sum += 1;
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    dr["TS_PASS"] = Convert.ToString(sum);
                }

                //=====20160412 add by ssi begin ==========================================================================================================================
                //[鐵蛋白SF.]
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4027' ";
                logger.Trace("找檢查項目 鐵蛋白SF 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    sRITEM_LOW1 = dtCODE1.Rows[0]["RITEM_LOW1"].ToString();
                    sRITEM_HIGH1 = dtCODE1.Rows[0]["RITEM_HIGH1"].ToString();
                    if (!Double.TryParse(sRITEM_LOW1, out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(sRITEM_HIGH1, out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = "SELECT (SELECT RESULT_VALUE_N FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4027') as RESULT_VALUE_N, a.PAT_NO, max(a.RESULT_DATE) " +
                  " FROM a_result_log AS A " +
                  " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND A.PAT_NO = f.pat_id " +
                  " WHERE A.RESULT_VER=0 AND A.RESULT_CODE='4027'  " +
                    " AND A.RESULT_DATE like '" + Trdate + "' " +
                  " GROUP BY a.PAT_NO ";
                logger.Trace("找檢查項目受檢人 鐵蛋白SF 檢驗值 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["SF_ED"] = "0"; dr["SF_PASS"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["SF_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        sRESULT_VALUE_N = dtAGE.Rows[i]["RESULT_VALUE_N"].ToString();
                        if (sRESULT_VALUE_N == "") sRESULT_VALUE_N = "0";
                        try
                        {
                            if (zRITEM_LOW1 <= Convert.ToDouble(sRESULT_VALUE_N) && Convert.ToDouble(sRESULT_VALUE_N) <= zRITEM_HIGH1)
                            {
                                sum += 1;
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    dr["SF_PASS"] = Convert.ToString(sum);
                }

                //=====20160412 add by ssi begin ==========================================================================================================================
                //IPTH  甲狀旁腺激素              
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4030' ";
                logger.Trace("找檢查項目 IPTH甲狀旁腺激素 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    sRITEM_LOW1 = dtCODE1.Rows[0]["RITEM_LOW1"].ToString();
                    sRITEM_HIGH1 = dtCODE1.Rows[0]["RITEM_HIGH1"].ToString();
                    if (!Double.TryParse(sRITEM_LOW1, out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(sRITEM_HIGH1, out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = "SELECT (SELECT RESULT_VALUE_N FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4030') as RESULT_VALUE_N, a.PAT_NO, max(a.RESULT_DATE) " +
                  " FROM a_result_log AS A " +
                  " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND A.PAT_NO = f.pat_id " +
                  " WHERE A.RESULT_VER=0 AND A.RESULT_CODE='4030'  " +
                    " AND A.RESULT_DATE like '" + Trdate + "' " +
                  " GROUP BY a.PAT_NO ";
                logger.Trace("找檢查項目受檢人 IPTH甲狀旁腺激素 檢驗值 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["IPTH_ED"] = "0"; dr["IPTH_PASS"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["IPTH_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        sRESULT_VALUE_N = dtAGE.Rows[i]["RESULT_VALUE_N"].ToString();
                        if (sRESULT_VALUE_N == "") sRESULT_VALUE_N = "0";
                        try
                        {
                            if (zRITEM_LOW1 <= Convert.ToDouble(sRESULT_VALUE_N) && Convert.ToDouble(sRESULT_VALUE_N) <= zRITEM_HIGH1)
                            {
                                sum += 1;
                            }
                        }
                        catch (Exception e)
                        {
                        }
                    }
                    dr["IPTH_PASS"] = Convert.ToString(sum);
                }

                //=====20160413 add by ssi begin ==========================================================================================================================
                //sp kt/V 5018
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='5018' ";
                logger.Trace("找檢查項目 sp kt/V5018 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql); zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    if (!Double.TryParse(dtCODE1.Rows[0]["RITEM_LOW1"].ToString(), out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(dtCODE1.Rows[0]["RITEM_HIGH1"].ToString(), out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }

                //找檢查項目受檢人代號 
                sql = "SELECT a.PAT_NO as pat_no" +
                  " FROM a_result_log as a " +
                    " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND a.PAT_NO = f.pat_id " +
                  " WHERE RESULT_VER=0 AND RESULT_CODE IN ('4047','4048','5015','5016','5012')  " +
                    " AND RESULT_DATE like '" + Trdate + "' " +
                  " GROUP BY PAT_NO  ";
                logger.Trace("找檢查項目 sp kt/V5018 受檢人代號 SQL: " + sql);
                DataTable dtpat_no = db.Query(sql);
                dr["KtV_ED"] = "0"; dr["KtV_PASS"] = "0";
                string s4047 = ""; string s4048 = ""; string s5012 = ""; string s5015 = ""; string s5016 = ""; string s5018 = "";
                double z4047 = 0; double z4048 = 0; double z5012 = 0; double z5015 = 0; double z5016 = 0; double z5018 = 0;
                int total = 0; sum = 0;
                if (dtpat_no.Rows.Count > 0)
                {
                    //找檢查項目受檢人檢驗值 
                    for (int i = 0; i < dtpat_no.Rows.Count; i++)
                    {
                        dtAGE.Clear();
                        sql = "SELECT a.RESULT_CODE, RESULT_VALUE_N" +
                          " FROM a_result_log as a " +
                            " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND a.PAT_NO = f.pat_id " +
                          " WHERE RESULT_VER=0 AND RESULT_CODE IN ('4047','4048','5015','5016','5012')  " +
                            " AND RESULT_DATE like CONCAT(SUBSTR(ADDDATE(CURDATE(),INTERVAL 0 MONTH),1,7),'%') " +
                            " AND PAT_NO='" + dtpat_no.Rows[i]["pat_no"].ToString() + "' " +
                          " ORDER BY RESULT_DATE  ";
                        logger.Trace("找檢查項目受檢人 sp kt/V5018 檢驗值 SQL: " + sql);
                        dtAGE = db.Query(sql);
                        if (dtAGE.Rows.Count > 4)
                        {
                            total = total + 1;
                            for (int j = 0; j < dtAGE.Rows.Count; j++)
                            {
                                switch (dtAGE.Rows[j]["RESULT_CODE"].ToString())
                                {
                                    case "4047": s4047 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                    case "4048": s4048 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                    case "5012": s5012 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                    case "5015": s5015 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                    case "5016": s5016 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                    default: s5018 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                }
                            }

                            if (!Double.TryParse(s4047, out z4047)) z4047 = 0;
                            if (!Double.TryParse(s4048, out z4048)) z4048 = 0;
                            if (!Double.TryParse(s5012, out z5012)) z5012 = 0;
                            if (!Double.TryParse(s5015, out z5015)) z5015 = 0;
                            if (!Double.TryParse(s5016, out z5016)) z5016 = 0;

                            //                            z4048 = 2.7; z4047 = 2.5; z5015 = 4; z5016 = 3; z5012 = 68;
                            if (z4048 != 0 && z4047 != 0 && z5015 != 0 && z5016 != 0 && z5012 != 0)
                            {
                                z5018 = (-Math.Log((z4048 / z4047) - 0.008 * z5015) + (4 - 3.5 * (z4048 / z4047)) * z5016 / z5012);
                                if (zRITEM_LOW1 <= z5018 && z5018 <= zRITEM_HIGH1) sum++;
                            }
                        }
                    }
                }
                dr["KtV_ED"] = Convert.ToString(total);
                dr["KtV_PASS"] = Convert.ToString(sum);

                //=====20160414 add by ssi begin ==========================================================================================================================
                //檢驗 HBsAg 4032
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4032' ";
                logger.Trace("找檢查項目 HBsAg4032 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    if (!Double.TryParse(dtCODE1.Rows[0]["RITEM_LOW1"].ToString(), out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(dtCODE1.Rows[0]["RITEM_HIGH1"].ToString(), out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = " SELECT (SELECT RESULT_VALUE_T FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4032') as RESULT_VALUE_T, A.PAT_NO, max(a.result_date) " +
                             " FROM a_result_log A  " +
                             " LEFT JOIN pat_info B ON A.PAT_NO=B.pif_id  " +
                             " LEFT JOIN zinfo_a_07 f  ON B.pif_id = f.pat_id AND f.opt_1 IN ('','5')  " +
                            " WHERE A.RESULT_VER=0  " +
                              " AND A.RESULT_CODE = '4032' " +
                                " 	AND A.RESULT_DATE '" + Trdate + "' " +
                              " AND A.RESULT_VALUE_T IN ('阴性','阳性')  " +
                            " GROUP BY A.PAT_NO ";
                logger.Trace("找檢查項目受檢人 HBsAg4032 檢驗值 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["HBsAg_ED"] = "0"; dr["HBsAg_POSI"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["HBsAg_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++) if (dtAGE.Rows[i]["RESULT_VALUE_T"].ToString() == "阳性") sum++;
                    dr["HBsAg_POSI"] = Convert.ToString(sum);
                }

                //=====20160415 add by ssi begin ==========================================================================================================================
                //檢驗 antihcv 4033
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='4033' ";
                logger.Trace("找檢查項目 antihcv4033 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql);
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    if (!Double.TryParse(dtCODE1.Rows[0]["RITEM_LOW1"].ToString(), out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(dtCODE1.Rows[0]["RITEM_HIGH1"].ToString(), out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }
                //找檢查項目受檢人檢驗值 
                sql = " SELECT (SELECT RESULT_VALUE_T FROM a_result_log WHERE RESULT_VER=0 AND pat_no=a.pat_no AND result_date=max(a.result_date) AND RESULT_CODE='4033') as RESULT_VALUE_T, A.PAT_NO, max(a.result_date) " +
                          " FROM a_result_log A  " +
                          " LEFT JOIN pat_info B ON A.PAT_NO=B.pif_id  " +
                          " LEFT JOIN zinfo_a_07 f  ON B.pif_id = f.pat_id AND f.opt_1 IN ('','5')  " +
                         " WHERE A.RESULT_VER=0  " +
                           " AND A.RESULT_CODE = '4033' " +
                             " 	AND A.RESULT_DATE like '" + Trdate + "' " +
                           " AND A.RESULT_VALUE_T IN ('阴性','阳性')  " +
                         " GROUP BY A.PAT_NO ";
                logger.Trace("找檢查項目受檢人 antihcv4033 檢驗值 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["Anti-HCV_ED"] = "0"; dr["Anti-HCV_POSI"] = "0";
                if (dtAGE.Rows.Count > 0)
                {
                    dr["Anti-HCV_ED"] = Convert.ToString(dtAGE.Rows.Count);
                    sum = 0;
                    for (int i = 0; i < dtAGE.Rows.Count; i++) if (dtAGE.Rows[i]["RESULT_VALUE_T"].ToString() == "阳性") sum++;
                    dr["Anti-HCV_POSI"] = Convert.ToString(sum);
                }

                //=====20160415 add by ssi begin ==========================================================================================================================
                //URR=5017 = 100 × (1 - (透析後BUN=4048 / 透析前BUN=4047) )
                //找檢查項目 取出合格區間上下值
                sql = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='5017' ";
                logger.Trace("找檢查項目 URR5017 取出合格區間上下值 SQL: " + sql);
                dtCODE1 = db.Query(sql); zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                zRITEM_LOW1 = 0; zRITEM_HIGH1 = 99999;
                if (dtCODE1.Rows.Count > 0)
                {
                    if (!Double.TryParse(dtCODE1.Rows[0]["RITEM_LOW1"].ToString(), out zRITEM_LOW1)) zRITEM_LOW1 = 0;
                    if (!Double.TryParse(dtCODE1.Rows[0]["RITEM_HIGH1"].ToString(), out zRITEM_HIGH1)) zRITEM_HIGH1 = 99999;
                }

                //找檢查項目受檢人代號 
                sql = "SELECT a.PAT_NO as pat_no" +
                  " FROM a_result_log as a " +
                    " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND a.PAT_NO = f.pat_id " +
                  " WHERE RESULT_VER=0 AND RESULT_CODE IN ('4047','4048')  " +
                    " AND RESULT_DATE like '" + Trdate + "' " +
                  " GROUP BY PAT_NO  ";
                logger.Trace("找檢查項目 URR5017 受檢人代號 SQL: " + sql);
                dtpat_no = db.Query(sql);
                dr["URR_ED"] = "0"; dr["URR_PASS"] = "0";
                s4047 = ""; s4048 = ""; string s5017 = "";
                z4047 = 0; z4048 = 0; double z5017 = 0;
                total = 0; sum = 0;
                if (dtpat_no.Rows.Count > 0)
                {
                    //找檢查項目受檢人檢驗值 
                    for (int i = 0; i < dtpat_no.Rows.Count; i++)
                    {
                        dtAGE.Clear();
                        sql = "SELECT a.RESULT_CODE, RESULT_VALUE_N" +
                          " FROM a_result_log as a " +
                            " LEFT JOIN zinfo_a_07 AS f ON f.opt_1 IN ('','5') AND a.PAT_NO = f.pat_id " +
                          " WHERE RESULT_VER=0 AND RESULT_CODE IN ('4047','4048')  " +
                            " AND RESULT_DATE like '" + Trdate + "' " +
                            " AND PAT_NO='" + dtpat_no.Rows[i]["pat_no"].ToString() + "' " +
                          " ORDER BY RESULT_DATE  ";
                        logger.Trace("找檢查項目受檢人 URR5017 檢驗值 SQL: " + sql);
                        dtAGE = db.Query(sql);
                        if (dtAGE.Rows.Count > 1)
                        {
                            total = total + 1;
                            for (int j = 0; j < dtAGE.Rows.Count; j++)
                            {
                                switch (dtAGE.Rows[j]["RESULT_CODE"].ToString())
                                {
                                    case "4047": s4047 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                    case "4048": s4048 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                    default: s5017 = dtAGE.Rows[j]["RESULT_VALUE_N"].ToString(); break;
                                }
                            }

                            if (!Double.TryParse(s4047, out z4047)) z4047 = 0;
                            if (!Double.TryParse(s4048, out z4048)) z4048 = 0;

                            // z4048 = 2.7; z4047 = 2.5; 
                            if (z4048 != 0 && z4047 != 0)
                            {
                                z5017 = 100 * (1 - (z4048 / z4047));
                                if (zRITEM_LOW1 <= z5017 && z5017 <= zRITEM_HIGH1) sum++;
                            }
                        }
                    }
                }
                dr["URR_ED"] = Convert.ToString(total); dr["URR_PASS"] = Convert.ToString(sum);

                //=====20160418 add by ssi begin ==========================================================================================================================
                //瘘管重建人数 
                sql = " SELECT A.pat_id AS PAT_NO, A.info_date AS HD_DATE, A.txt_10 AS HD_CAUSE, A.opt_9  " +
                        " FROM zinfo_e_02 A  " +
                        " LEFT JOIN zinfo_a_07 f ON A.pat_id = f.pat_id AND f.opt_1 IN ('','5')  " +
                        " WHERE A.info_date like '" + Trdate + "' " +
                            " AND A.opt_8=1  " +
                        " GROUP BY a.PAT_id ";
                logger.Trace("瘘管重建人数 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["FR"] = "0"; if (dtAGE.Rows.Count > 0) dr["FR"] = Convert.ToString(dtAGE.Rows.Count);

                //=====20160420 add by ssi begin ==========================================================================================================================
                //脱离人数 
                sql = " SELECT pat_id AS PAT_NO, info_date AS HD_DATE " +
                        " FROM zinfo_a_07  " +
                        " WHERE info_date like CONCAT(SUBSTR(ADDDATE(CURDATE(),INTERVAL 0 MONTH),1,7),'%') " +
                        " AND (opt_1=2 or (opt_1=1 AND opt_2=1)) " +
                        " GROUP BY PAT_id ";
                logger.Trace("脱离人数 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["Out"] = "0"; 
                if (dtAGE.Rows.Count > 0) dr["Out"] = Convert.ToString(dtAGE.Rows.Count);

                //=====20160420 add by ssi begin ==========================================================================================================================
                //总人数
                sql = " SELECT pv_id, pv_datevisit " +
                          " FROM pat_visit a " +
                            " LEFT JOIN zinfo_a_07 f ON A.pv_id = f.pat_id AND f.opt_1 IN ('','5')  " +
                          " WHERE pv_datevisit like '" + Trdate + "' " +
                           " GROUP BY pv_id ";
                logger.Trace("总人数 SQL: " + sql);
                dtAGE = db.Query(sql);
                dr["Population"] = "0"; 
                if (dtAGE.Rows.Count > 0) dr["Population"] = Convert.ToString(dtAGE.Rows.Count);












                dt.Rows.Add(dr);
                ds.Tables.Add(dt);
            }
            catch (Exception e)
            {
                result = e.Message;
                logger.Error("ReadFhirDQDistribution error: " + result);
            }
            return ds;
        }
        //=====20160407 add by ssi end ==========================================================================================================================



        public Stream ReadPatient()
        {
            //IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
            DBMysql db = new DBMysql();
            DataTable patientDT = new DataTable();
            string resultJson = "";

            try
            {               
                string sql = "SELECT p.pif_id, p.pif_name, if(p.pif_sex = 'M','男','女') as sex, p.pif_dob, p.pif_ic, p.pif_docname, ";
                sql += "if((SELECT count(*) FROM blood_group bg WHERE bg.bgrp_patic=p.pif_ic AND (bg.bgrp_aids='Y' OR bg.bgrp_syphilis='Y' OR bg.bgrp_hbv='Y' OR bg.bgrp_hcv='Y' OR bg.bgrp_diabetic='Y'))=1,'Y','N') AS Kind ";
                sql += " FROM pat_info p";
                sql += " WHERE p.pif_id NOT IN (SELECT b.pif_id FROM pat_info b";
                sql += " INNER JOIN (SELECT a.pat_id,a.opt_1 FROM zinfo_a_07 a";
                sql += " INNER JOIN (SELECT pat_id,max(info_date) AS last_date FROM zinfo_a_07 GROUP BY pat_id) b";
                sql += " ON a.pat_id=b.pat_id AND a.info_date=b.last_date) f";
                sql += " ON b.pif_id = f.pat_id";
                sql += " AND f.opt_1 IN ('1','2','3','4'))";
                sql += " ORDER BY pif_id ";
                logger.Trace("ReadPatient SQL: " + sql);
                patientDT = db.Query(sql);

                if (patientDT.Rows.Count > 0)
                {
                    resultJson = JsonConvert.SerializeObject(patientDT, Formatting.None);
                }
            }
            catch (Exception e)
            {
                resultJson = e.Message;
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(resultJson));
        }

        public Stream ReadBed()
        {
            DBMysql db = new DBMysql();
            DataTable bedDT = new DataTable();
            string resultJson = "";

            try
            {
                string sql = "SELECT pv_floor, pv_sec, pv_bedno, com_no FROM bed_setup";
                sql += " WHERE 1=1";
                bedDT = db.Query(sql);

                if (bedDT.Rows.Count > 0)
                {
                    resultJson = JsonConvert.SerializeObject(bedDT, Formatting.None);
                }
            }
            catch (Exception e)
            {
                resultJson = e.Message;
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(resultJson));
        }

        public Stream ReadMac()
        {
            DBMysql db = new DBMysql();
            DataTable macDT = new DataTable();
            string resultJson = "";

            try
            {
                string sql = "SELECT mac_id, mac_flr, mac_sec, mac_bedno, mac_typ, mac_status, mac_com, mac_brand, mac_kind ";
                sql += " FROM mac_setup";
                sql += " WHERE 1=1";
                macDT = db.Query(sql);

                if (macDT.Rows.Count > 0)
                {
                    resultJson = JsonConvert.SerializeObject(macDT, Formatting.None);
                }
            }
            catch (Exception e)
            {
                resultJson = e.Message;
            }
            return new MemoryStream(Encoding.UTF8.GetBytes(resultJson));
        }

        private string queryAll()
        {
            DBMysql db = new DBMysql();
            DataTable appointmentDT = new DataTable();
            string sql = "SELECT id, pv_floor, pv_sec, pv_bedno, mac_type, appointment_date, time_type, pif_id, status, descciption, create_by";
            sql += " FROM appointment";
            sql += " WHERE 1=1";

            //if (Text_Name.Text != "") sql += " AND pif_name like '%" + Text_Name.Text + "%'";
            //if (Cbo_Gender.SelectedItem != null) sql += " AND pif_sex ='" + GetComboBoxValu(Cbo_Gender) + "'";
            //if (Text_ID.Text != "")
            //{
            //if (Text_ID.Text.Substring(0, 1) != "#")
            //    sql += " AND pif_ic like '%" + Text_ID.Text + "%'";
            //else
            //    sql += " AND pif_id='" + Text_ID.Text.Substring(1) + "'";
            //}
            //sql += " ORDER BY pif_id ";
            appointmentDT = db.Query(sql);
            string json = JsonConvert.SerializeObject(appointmentDT, Formatting.Indented);
            return json;
        }
    }

    /*[DataContract]
    public class CreateRequestData
    {
        [DataMember]
        public InsertAppointmentData[] appointments { get; set; }
    }*/

    [DataContract]
    public class InsertAppointmentData
    {
        [DataMember]
        public string pv_floor { get; set; }

        [DataMember]
        public string pv_sec { get; set; }

        [DataMember]
        public string pv_bedno { get; set; }

        [DataMember]
        public string mac_type { get; set; }

        [DataMember]
        public string appointment_date { get; set; }

        [DataMember]
        public string time_type { get; set; }

        [DataMember]
        public int pif_id { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string descciption { get; set; }

        [DataMember]
        public string create_by { get; set; }
    }

    /*[DataContract]
    public class CreateResponseData
    {
        [DataMember]
        public AppointmentResult[] appointmentResults { get; set; }
    }*/

    [DataContract]
    public class AppointmentResult
    {
        [DataMember]
        public string message { get; set; }

        [DataMember]
        public string pv_floor { get; set; }

        [DataMember]
        public string pv_sec { get; set; }

        [DataMember]
        public string pv_bedno { get; set; }

        [DataMember]
        public string mac_type { get; set; }

        [DataMember]
        public string appointment_date { get; set; }

        [DataMember]
        public string time_type { get; set; }

        [DataMember]
        public int pif_id { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string descciption { get; set; }

        [DataMember]
        public string create_by { get; set; }
    }


    [DataContract]
    public class SearchRequestData
    {
        [DataMember]
        public string startDate { get; set; }

        [DataMember]
        public int period { get; set; }

        [DataMember]
        public string pv_floor { get; set; }

        [DataMember]
        public string pv_sec { get; set; }
    }

    /*[DataContract]
    public class SearchResponseData
    {
        [DataMember]
        public AppointmentData[] appointments { get; set; }

        [DataMember]
        public string message { get; set; }
    }*/

    [DataContract]
    public class AppointmentData
    {
        [DataMember]
        public int id { get; set; }

        [DataMember]
        public string pv_floor { get; set; }

        [DataMember]
        public string pv_sec { get; set; }

        [DataMember]
        public string pv_bedno { get; set; }

        [DataMember]
        public string mac_type { get; set; }

        [DataMember]
        public string appointment_date { get; set; }

        [DataMember]
        public string time_type { get; set; }

        [DataMember]
        public int pif_id { get; set; }

        [DataMember]
        public string status { get; set; }

        [DataMember]
        public string descciption { get; set; }

        [DataMember]
        public string create_by { get; set; }

        [DataMember]
        public string create_on { get; set; }
    }


    /*[DataContract]
    public class DeleteRequestData
    {
        [DataMember]
        public int[] id { get; set; }
    }*/

    [DataContract]
    public class DeleteResponseData
    {
        [DataMember]
        public string message { get; set; }
    }
}
