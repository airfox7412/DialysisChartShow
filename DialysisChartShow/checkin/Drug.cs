using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Web;
using System.Xml;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

namespace Dialysis_Chart_Show.checkin
{
    public class Drug
    {
        public Drug(string py, string drugname)
                {
                    this.py = py;
                    this.drugname = drugname;
                }
        public Drug()
        {
        }
        public string py { get; set; }
        public string drugname { get; set; }

        public static Paging<Drug> drugsPaging(int start, int limit, string sort, string dir, string filter)
        {
            List<Drug> drugs = Drug.Getdrugname;
            if (filter.Length > 0)
            {
                drugs.Clear();
                DBMysql db = new DBMysql();
                List<Drug> data = new List<Drug>();

                string sql = "SELECT drg_name FROM drug_list "; //簡碼輸入
                sql += "WHERE short_code LIKE '%" + filter + "%' AND drg_status='Y'";
                DataTable dt1 = db.Query(sql);
                if (dt1.Rows.Count > 0)
                {
                    for (int j = 0; j < dt1.Rows.Count; j++)
                    {
                        Drug drug = new Drug();
                        drug.py = filter;
                        drug.drugname = dt1.Rows[j]["drg_name"].ToString(); //drg_name
                        drugs.Add(drug);
                    }
                }
                else //直接輸入中文字
                {
                    sql = "SELECT drg_name FROM drug_list ";
                    sql += "WHERE drg_name LIKE '%" + filter + "%' AND drg_status='Y' ";
                    dt1 = db.Query(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            Drug drug = new Drug();
                            drug.py = filter;
                            drug.drugname = dt1.Rows[j]["drg_name"].ToString(); //drg_name
                            drugs.Add(drug);
                        }
                    }
                    else
                    {
                        sql = "SELECT PY, HZ, ZM FROM pinyin ";
                        if (filter != "*")
                            sql += "WHERE PY LIKE '" + filter + "%' AND ZM='" + filter.Substring(0, 1) + "' ";
                        DataTable dt = db.Query(sql);
                        if (dt.Rows.Count > 0) //使用拼音輸入
                        {
                            sql = "SELECT drg_name FROM drug_list WHERE (1=0 ";
                            for (int i = 0; i < dt.Rows.Count; i++)
                                sql += "OR drg_name LIKE '" + dt.Rows[i]["HZ"].ToString() + "%' ";
                            sql += ") AND drg_status='Y'";
                            dt1 = db.Query(sql);
                            if (dt1.Rows.Count > 0)
                            {
                                for (int j = 0; j < dt1.Rows.Count; j++)
                                {
                                    Drug drug = new Drug();
                                    drug.py = filter.Substring(0, 1);
                                    drug.drugname = dt1.Rows[j]["drg_name"].ToString(); //drg_name
                                    drugs.Add(drug);
                                }
                            }
                        }
                        dt.Dispose();
                    }
                    dt1.Dispose();
                    db.Close();
                }
            }
            //if (!string.IsNullOrEmpty(filter) && filter != "*")
            //{
            //    drugs.RemoveAll(drug => !drug.py.StartsWith(filter));
            //}

            if (!string.IsNullOrEmpty(sort))
            {
                drugs.Sort(delegate(Drug x, Drug y)
                {
                    object a;
                    object b;

                    int direction = dir == "DESC" ? -1 : 1;

                    a = x.GetType().GetProperty(sort).GetValue(x, null);
                    b = y.GetType().GetProperty(sort).GetValue(y, null);

                    return CaseInsensitiveComparer.Default.Compare(a, b) * direction;
                });
            }

            if ((start + limit) > drugs.Count)
            {
                limit = drugs.Count - start;
            }
            List<Drug> rangeDrugs = (start < 0 || limit < 0) ? drugs : drugs.GetRange(start, limit);
            return new Paging<Drug>(rangeDrugs, drugs.Count);
        }

        public static List<Drug> Getdrugname
        {
            get
            {
                string typeing = "ZHONG";
                DBMysql db = new DBMysql();
                List<Drug> data = new List<Drug>();
                string sql = "SELECT PY, HZ, ZM FROM pinyin WHERE PY LIKE '%" + typeing + "%' AND ZM='" + typeing.Substring(0, 1) + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    string sql1 = "SELECT drg_name FROM drug_list WHERE 1=0 ";
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        sql1 += "OR drg_name LIKE '" + dt.Rows[i]["HZ"].ToString() + "%' ";
                    }

                    DataTable dt1 = db.Query(sql1);
                    if (dt1.Rows.Count > 0)
                    {
                        for (int j = 0; j < dt1.Rows.Count; j++)
                        {
                            Drug drug = new Drug();
                            drug.py = typeing.Substring(0, 1);
                            drug.drugname = dt1.Rows[j]["drg_name"].ToString(); //drg_name
                            data.Add(drug);
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