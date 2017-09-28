using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Xml;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

namespace Dialysis_Chart_Show
{
    public class Doctor
    {
        public Doctor(string acclv_id, string acclv_fname)
        {
            this.acclv_id = acclv_id;
            this.acclv_fname = acclv_fname;
        }
        public Doctor()
        {
        }
        public string acclv_id { get; set; }
        public string acclv_fname { get; set; }

        public static Paging<Doctor> DoctorsPaging(int start, int limit, string sort, string dir, string filter)
        {
            List<Doctor> doctors = new List<Doctor>();
            if (filter.Length > 0)
            {
                doctors.Clear();
                DBMysql db = new DBMysql();
                List<Doctor> data = new List<Doctor>();

                string sql = "SELECT PY, HZ, ZM FROM pinyin ";
                if (filter != "*")
                    sql += "WHERE PY LIKE '" + filter + "%' AND ZM='" + filter.Substring(0, 1) + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0) //使用拼音輸入
                {
                    sql = "SELECT a.acclv_id, a.acclv_fname FROM access_level a ";
                    sql += "INNER JOIN associate_list b ON a.acclv_id=b.associate_id ";
                    sql += "WHERE (b.associate_type='DC' OR b.associate_type='DH') AND (1=0 ";
                    for (int i = 0; i < dt.Rows.Count; i++)
                        sql += "OR a.acclv_fname LIKE '%" + dt.Rows[i]["HZ"].ToString() + "%' ";
                    sql += ") ";
                    DataTable dt1 = db.Query(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            Doctor doctor = new Doctor();
                            doctor.acclv_id = dt1.Rows[j]["acclv_id"].ToString();
                            doctor.acclv_fname = dt1.Rows[j]["acclv_fname"].ToString();
                            doctors.Add(doctor);
                        }
                    }
                    dt1.Dispose();
                }
                else
                {
                    sql = "SELECT a.acclv_id, a.acclv_fname FROM access_level a "; //直接輸入中文字
                    sql += "INNER JOIN associate_list b ON a.acclv_id=b.associate_id ";
                    sql += "WHERE (b.associate_type='DC' OR b.associate_type='DH') AND a.acclv_fname LIKE '%" + filter + "%' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            Doctor doctor = new Doctor();
                            doctor.acclv_id = dt.Rows[j]["acclv_id"].ToString();
                            doctor.acclv_fname = dt.Rows[j]["acclv_fname"].ToString();
                            doctors.Add(doctor);
                        }
                    }
                }
                dt.Dispose();
                db.Close();
            }

            if (!string.IsNullOrEmpty(sort))
            {
                doctors.Sort(delegate(Doctor x, Doctor y)
                {
                    object a;
                    object b;

                    int direction = dir == "DESC" ? -1 : 1;

                    a = x.GetType().GetProperty(sort).GetValue(x, null);
                    b = y.GetType().GetProperty(sort).GetValue(y, null);

                    return CaseInsensitiveComparer.Default.Compare(a, b) * direction;
                });
            }

            if ((start + limit) > doctors.Count)
            {
                limit = doctors.Count - start;
            }
            List<Doctor> rangeDoctors = (start < 0 || limit < 0) ? doctors : doctors.GetRange(start, limit);
            return new Paging<Doctor>(rangeDoctors, doctors.Count);
        }

        public static List<Doctor> Getpatname
        {
            get
            {
                string typeing = "ZHONG";
                DBMysql db = new DBMysql();
                List<Doctor> data = new List<Doctor>();
                string sql = "SELECT PY, HZ, ZM FROM pinyin WHERE PY LIKE '" + typeing + "%' AND ZM='" + typeing.Substring(0, 1) + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    string sql1 = "SELECT acclv_id, acclv_fname FROM access_level WHERE 1=0 ";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql1 += "OR acclv_fname LIKE '" + dt.Rows[i]["HZ"].ToString() + "%' ";
                    }

                    DataTable dt1 = db.Query(sql1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            Doctor doctor = new Doctor();
                            doctor.acclv_id = dt1.Rows[j]["acclv_id"].ToString();
                            doctor.acclv_fname = dt1.Rows[j]["acclv_fname"].ToString();
                            data.Add(doctor);
                        }
                    }
                    dt1.Dispose();
                }
                dt.Dispose();
                db.Close();
                return data;
            }
        }
    }
}