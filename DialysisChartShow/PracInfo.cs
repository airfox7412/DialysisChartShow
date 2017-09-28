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
    public class PracInfo
    {
        public string fname { get; set; }
        public string stfcode { get; set; }

        // Default Constructor
        public PracInfo() : 
            this("admin", "admin")
        {
        }
        // Constructor
        public PracInfo(string fname, string stfcode)
        {
            this.fname = fname;
            this.stfcode = stfcode;
        }

        public static Paging<PracInfo> PracInfosPaging(int start, int limit, string sort, string dir, string filter)
        {
            List<PracInfo> pracInfos = PracInfo.Getpracname;
            if (filter.Length > 0)
            {
                pracInfos.Clear();
                DBMysql db = new DBMysql();
                List<PracInfo> data = new List<PracInfo>();

                string sql = "SELECT PY, HZ, ZM FROM pinyin ";
                if (filter != "*")
                    sql += "WHERE PY LIKE '%" + filter + "%' AND ZM='" + filter.Substring(0, 1) + "' ";

                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0) //使用拼音輸入
                {
                    sql = "SELECT acclv_fname, acclv_stfcode FROM access_level WHERE (1=0 ";
                    for (int i = 0; i < dt.Rows.Count; i++)
                        sql += "OR acclv_fname LIKE '%" + dt.Rows[i]["HZ"].ToString() + "%' ";
                    sql += ") ";
                    DataTable dt1 = db.Query(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            PracInfo pracInfo = new PracInfo();
                            pracInfo.fname = dt1.Rows[j]["acclv_fname"].ToString();
                            pracInfo.stfcode = dt1.Rows[j]["acclv_stfcode"].ToString();
                            pracInfos.Add(pracInfo);
                        }
                    }
                    dt1.Dispose();
                }
                else //直接輸入中文字
                {
                    sql = "SELECT acclv_fname, acclv_stfcode FROM access_level "; 
                    sql += "WHERE acclv_fname LIKE '%" + filter + "%' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            PracInfo pracInfo = new PracInfo();
                            pracInfo.fname = dt.Rows[j]["acclv_fname"].ToString();
                            pracInfo.stfcode = dt.Rows[j]["acclv_stfcode"].ToString();
                            pracInfos.Add(pracInfo);
                        }
                    }
                }
                dt.Dispose();
                db.Close();
            }

            if (!string.IsNullOrEmpty(sort))
            {
                pracInfos.Sort(delegate(PracInfo x, PracInfo y)
                {
                    object a;
                    object b;

                    int direction = dir == "DESC" ? -1 : 1;

                    a = x.GetType().GetProperty(sort).GetValue(x, null);
                    b = y.GetType().GetProperty(sort).GetValue(y, null);

                    return CaseInsensitiveComparer.Default.Compare(a, b) * direction;
                });
            }

            if ((start + limit) > pracInfos.Count)
            {
                limit = pracInfos.Count - start;
            }
            List<PracInfo> rangePracInfos = (start < 0 || limit < 0) ? pracInfos : pracInfos.GetRange(start, limit);
            return new Paging<PracInfo>(rangePracInfos, pracInfos.Count);
        }

        public static List<PracInfo> Getpracname
        {
            get
            {
                string typeing = "ZHONG";
                DBMysql db = new DBMysql();
                List<PracInfo> data = new List<PracInfo>();
                string sql = "SELECT PY, HZ, ZM FROM pinyin WHERE PY LIKE '%" + typeing + "%' AND ZM='" + typeing.Substring(0, 1) + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    string sql1 = "SELECT acclv_fname, acclv_stfcode FROM access_level WHERE 1=0 ";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql1 += "OR acclv_fname LIKE '" + dt.Rows[i]["HZ"].ToString() + "%' ";
                    }

                    DataTable dt1 = db.Query(sql1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            PracInfo pracInfo = new PracInfo();
                            pracInfo.fname = dt1.Rows[j]["acclv_fname"].ToString();
                            pracInfo.stfcode = dt1.Rows[j]["acclv_stfcode"].ToString(); 
                            data.Add(pracInfo);
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