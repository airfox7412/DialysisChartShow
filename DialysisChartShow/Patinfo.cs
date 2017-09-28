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
    public class Patinfo
    {
        public Patinfo(string patic, string patname)
                {
                    this.patic = patic;
                    this.patname = patname;
                }
        public Patinfo()
        {
        }
        public string patic { get; set; }
        public string patname { get; set; }

        public static Paging<Patinfo> PatinfosPaging(int start, int limit, string sort, string dir, string filter)
        {
            //List<Patinfo> patinfos = Patinfo.Getpatname;
            List<Patinfo> patinfos = new List<Patinfo>();
            if (filter.Length > 0)
            {
                patinfos.Clear();
                DBMysql db = new DBMysql();
                List<Patinfo> data = new List<Patinfo>();

                string sql = "SELECT PY, HZ, ZM FROM pinyin ";
                if (filter != "*")
                    sql += "WHERE PY LIKE '%" + filter + "%' AND ZM='" + filter.Substring(0, 1) + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0) //使用拼音輸入
                {
                    sql = "SELECT pif_ic, pif_name FROM pat_info WHERE (1=0 ";
                    for (int i = 0; i < dt.Rows.Count; i++)
                        sql += "OR pif_name LIKE '%" + dt.Rows[i]["HZ"].ToString() + "%' ";
                    sql += ") ";
                    DataTable dt1 = db.Query(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            Patinfo patinfo = new Patinfo();
                            patinfo.patic = dt1.Rows[j]["pif_ic"].ToString();
                            patinfo.patname = dt1.Rows[j]["pif_name"].ToString();
                            patinfos.Add(patinfo);
                        }
                    }
                    dt1.Dispose();
                }
                else
                {
                    sql = "SELECT pif_ic, pif_name FROM pat_info "; //直接輸入中文字
                    sql += "WHERE pif_name LIKE '%" + filter + "%' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt.Rows.Count; j++)
                        {
                            Patinfo patinfo = new Patinfo();
                            patinfo.patic = dt.Rows[j]["pif_ic"].ToString(); ;
                            patinfo.patname = dt.Rows[j]["pif_name"].ToString();
                            patinfos.Add(patinfo);
                        }
                    }
                }
                dt.Dispose();
                db.Close();
            }

            if (!string.IsNullOrEmpty(sort))
            {
                patinfos.Sort(delegate(Patinfo x, Patinfo y)
                {
                    object a;
                    object b;

                    int direction = dir == "DESC" ? -1 : 1;

                    a = x.GetType().GetProperty(sort).GetValue(x, null);
                    b = y.GetType().GetProperty(sort).GetValue(y, null);

                    return CaseInsensitiveComparer.Default.Compare(a, b) * direction;
                });
            }

            if ((start + limit) > patinfos.Count)
            {
                limit = patinfos.Count - start;
            }
            List<Patinfo> rangePatinfos = (start < 0 || limit < 0) ? patinfos : patinfos.GetRange(start, limit);
            return new Paging<Patinfo>(rangePatinfos, patinfos.Count);
        }

        public static List<Patinfo> Getpatname
        {
            get
            {
                string typeing = "ZHONG";
                DBMysql db = new DBMysql();
                List<Patinfo> data = new List<Patinfo>();
                string sql = "SELECT PY, HZ, ZM FROM pinyin WHERE PY LIKE '%" + typeing + "%' AND ZM='" + typeing.Substring(0, 1) + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    string sql1 = "SELECT pif_ic, pif_name FROM pat_info WHERE 1=0 ";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql1 += "OR pif_name LIKE '" + dt.Rows[i]["HZ"].ToString() + "%' ";
                    }

                    DataTable dt1 = db.Query(sql1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            Patinfo patinfo = new Patinfo();
                            patinfo.patic = dt1.Rows[j]["pif_ic"].ToString();
                            patinfo.patname = dt1.Rows[j]["pif_name"].ToString();
                            data.Add(patinfo);
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