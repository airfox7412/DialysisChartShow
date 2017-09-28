using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using MySql.Data;
using MySql.Data.MySqlClient;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;

namespace Dialysis_Chart_Show.tools
{
    public class pat
    {
        public string floor;
        public string area;
        public string time;
        public string bedno;
        /// <summary>
        /// 病患身分證號; pv_ic, pif_ic, pif_ic='" + Tex_Patient_ID.Text + "' ";
        /// </summary>
        public string pif_ic;
        public string weight_before;
        public string weight_after;
        public string name;
        public string sex;
        public string age;

        /// <summary>
        /// 分流方式, like. 自体内瘘
        /// </summary>
        private string _pif_hpack = null;
        /// <summary>
        /// vascular_access, 血管通路, 分流方式, like. 自体内瘘
        /// </summary>
        public string pif_hpack
        {
            get{
                if(_pif_hpack == null || _pif_hpack.Length == 0)
                {
                    //throw new Exception("_pif_hpack didn't initialize yet, still be null or ''");
                    return "";
                }
                return _pif_hpack;
            }
            set{
                _pif_hpack = value;
            }
        }

        /// <summary>
        /// 管路型号, like 血路管
        /// </summary>
        private string _pif_hpack3;
        /// <summary>
        /// 管路型号, like 血路管
        /// </summary>
        public string pif_hpack3
        {
            get
            {
                if (_pif_hpack3 == null)
                {
                    //throw new Exception("_pif_hpack3 didn't initialize yet, still be null");
                    return "";
                }
                return _pif_hpack3;
            }
            set
            {
                if (value == null || value.Length == 0)
                {
                    throw new Exception("Assign an invalid value ("+value+") to pif_hpack3 ");
                }
                _pif_hpack3 = value;
            }
        }

        /// <summary>
        /// 透析器型號
        /// </summary>
        private string _pif_hpack2;
        /// <summary>
        /// 透析器型號
        /// </summary>
        public string pif_hpack2
        {
            get
            {
                if (_pif_hpack2 == null)
                {
                    //throw new Exception("_pif_hpack2 didn't initialize yet, stil be null");
                    return "";
                }
                return _pif_hpack2;
            }
            set
            {
                _pif_hpack2 = value;
            }
        }

        public class update
        {

            /// <summary>
            /// vascular_access: 血管通路類型
            /// </summary>
            /// <param name="vascular_access">血管通路類型</param>
            /// <param name="dialysis_date"></param>
            static public void update_vascular_access(string vascular_access, string dialysis_date, string cln1_patic)
            {
                string sql = "UPDATE clinical1_nurse a set cln1_col34='" + vascular_access + "'" +
                      " WHERE a.cln1_patic='" + cln1_patic + "'" +  //Tex_Patient_ID.Text
                      " and a.cln1_diadate='" + dialysis_date + "'";
                DBMysql db = new DBMysql();
                db.Excute(sql);
                Common.SaveERR("update_vascular_access(...)", sql);
            }

            /// <summary>
            /// cln1_patic: Tex_Patient_ID.Text
            /// </summary>
            static public void register_to_clinical1_nurse(string cln1_patic, string vascular_access, string tube_model, string before_weight)
            {
                DateTime t = DateTime.Now;
                string timenow = string.Format("{0:00}", t.Hour) + ":" + string.Format("{0:00}", t.Minute) + ":" + string.Format("{0:00}", t.Second);
                string today = DateTime.Now.ToString("yyyy-MM-dd");

                DBMysql db = new DBMysql();
                string sql = "";
                sql = "SELECT cln1_diadate FROM clinical1_nurse ";
                sql += "WHERE cln1_patic='" + cln1_patic + "' AND cln1_diadate='" + today + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    sql = "UPDATE clinical1_nurse SET ";
                    sql += "cln1_col10='" + timenow + "' ";
                    //sql += "cln1_dateadded='" + today + "',";
                    //sql += "cln1_col34='" + vascular_access + "',"; //血管通路類型
                    //sql += "cln1_col27='" + tube_model + "',"; //管路型號
                    //sql += "cln1_col5='" + before_weight + "' "; //透析前體重
                    sql += "WHERE cln1_patic='" + cln1_patic + "' AND cln1_diadate='" + today + "'";
                }
                else
                {
                    #region 套用治療參數模板
                    Boolean dflag = true;
                    string cln1_col4 = "", cln1_col34 = "", cln1_col3 = "", cln1_col6 = "";
                    string cln1_col7 = "", cln1_col13 = "", cln1_col14 = "", cln1_col15 = "", cln1_col17 = "";
                    string cln1_col28 = "", cln1_col29 = "", cln1_col30 = "", cln1_col31 = "";

                    sql = "SELECT * FROM clinical1_doc_henan ";
                    sql += "WHERE cln1_patic='" + cln1_patic + "' AND cln1_diadate='" + today + "' ";
                    DataTable dt_doc = db.Query(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "SELECT * FROM clinical1_doc_henan ";
                        sql += "WHERE cln1_patic='" + cln1_patic + "' AND cln1_diadate='base' ";
                        dt_doc = db.Query(sql);
                        if (dt_doc.Rows.Count == 0)
                        {
                            dt_doc.Dispose();
                            db.myConnection.Close();
                            dflag = false;
                        }
                        else
                        {
                            dflag = true;
                        }
                    }
                    if (dflag)
                    {
                        cln1_col4 = dt_doc.Rows[0]["cln1_col1"].ToString(); //血管通路類型
                        cln1_col34 = dt_doc.Rows[0]["cln1_col2"].ToString(); //透析器型号
                        cln1_col3 = dt_doc.Rows[0]["cln1_col3"].ToString(); //透析方式
                        cln1_col6 = dt_doc.Rows[0]["cln1_col4"].ToString(); //干体重
                        cln1_col7 = dt_doc.Rows[0]["cln1_col6"].ToString(); //目标定容量
                        cln1_col13 = dt_doc.Rows[0]["cln1_col7"].ToString(); //首次剂量
                        cln1_col14 = dt_doc.Rows[0]["cln1_col8"].ToString(); //追加量
                        cln1_col15 = dt_doc.Rows[0]["cln1_col5"].ToString(); //抗凝药物
                        cln1_col17 = dt_doc.Rows[0]["cln1_col9"].ToString(); //总量
                        cln1_col28 = dt_doc.Rows[0]["cln1_col10"].ToString(); //透析液: 钾
                        cln1_col29 = dt_doc.Rows[0]["cln1_col11"].ToString(); //钙
                        cln1_col30 = dt_doc.Rows[0]["cln1_col12"].ToString(); //碳酸氢根
                        cln1_col31 = dt_doc.Rows[0]["cln1_col13"].ToString(); //钠

                        sql = "INSERT INTO clinical1_nurse(cln1_patic, cln1_diadate, cln1_col10, cln1_dateadded, cln1_col5, ";
                        sql += "cln1_col4, cln1_col34, cln1_col3, cln1_col6, cln1_col15, ";
                        sql += "cln1_col13, cln1_col14, cln1_col17,";
                        sql += "cln1_col7, cln1_col28, cln1_col29, cln1_col30, cln1_col31) ";
                        sql += "VALUES('" + cln1_patic + "', '" + today + "','" + timenow + "','" + today + "','" + before_weight + "',";
                        sql += "'" + cln1_col4 + "','" + cln1_col34 + "','" + cln1_col3 + "','" + cln1_col6 + "','" + cln1_col15 + "',";
                        sql += "'" + cln1_col13 + "','" + cln1_col14 + "','" + cln1_col17 + "',";
                        sql += "'" + cln1_col7 + "','" + cln1_col28 + "','" + cln1_col29 + "','" + cln1_col30 + "','" + cln1_col31 + "')";
                    }
                    #endregion
                    else
                    {
                        sql = "INSERT INTO clinical1_nurse(cln1_patic, cln1_diadate, cln1_col10, cln1_dateadded, cln1_col5) ";
                        sql += "VALUES('" + cln1_patic + "', '" + today + "','" + timenow + "','" + today + "','" + before_weight + "')";
                    }
                    Common.SaveERR("register_to_clinical1_nurse", sql);
                }                    
                db.Excute(sql);
                dt.Dispose();
            }


            /// <summary>
            /// cln1_patic: Tex_Patient_ID.Text
            /// </summary>
            static public void register_to_clinical1_nurse(string cln1_patic, string vascular_access, string tube_model)
            {
                // cln1_col27 管路型號
                DateTime t = DateTime.Now;
                string timenow = string.Format("{0:00}", t.Hour) + ":" + string.Format("{0:00}", t.Minute) + ":" + string.Format("{0:00}", t.Second);
                string sql = "INSERT INTO clinical1_nurse(cln1_patic,cln1_diadate,cln1_col10,cln1_dateadded, cln1_col34, cln1_col27) ";
                sql += "VALUES('" + cln1_patic + "', '" + DateTime.Now.ToString("yyyy-MM-dd");
                sql += "','" + timenow + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "', '" + vascular_access + "', '" + tube_model + "')";
                DBMysql db = new DBMysql();
                db.Excute(sql);
                //   Common.SaveERR("register_to_clinical1_nurse", sql);
            }

            /// <summary>
            /// 分流方式        pif_hpack
            /// 透析器型號      pif_hpack2
            /// 管路型號        pif_hpack3
            /// 病患身分證號    pif_ic
            /// </summary>
            /// <param name="pif_hpack"></param>
            /// <param name="pif_hpack2"></param>
            /// <param name="pif_hpack3"></param>
            /// <param name="pif_ic"></param>
            static public void update_pat_info(pat pat_obj)
            {
                DBMysql db = new DBMysql();
                string sql = "";
                try
                {
                    sql = "UPDATE  pat_info SET pif_hpack='" + pat_obj.pif_hpack + "', " +
                                                 "pif_hpack2='" + pat_obj.pif_hpack2 + "', " +
                                                 "pif_hpack3='" + pat_obj.pif_hpack3 + "' " +
                                           "WHERE pif_ic='" + pat_obj.pif_ic + "'";
                    db.Excute(sql);
                }
                catch (Exception ex)
                {
                    Common.SaveERR("Update pat_info", ex.Message);
                }
            }

            static public void set_dialysis_time(string person_id, string floor_no, string bed_no, string dia_date)
            {
                DBMysql db = new DBMysql();
                string sql = "SELECT cln1_col10 FROM clinical1_nurse ";
                sql += "WHERE cln1_patic='" + person_id + "' AND cln1_diadate='" + dia_date + "'";
                DataTable dt_list = db.Query(sql);
                string imin = "";
                if (dt_list.Rows.Count > 0)
                    imin = dt_list.Rows[0]["cln1_col10"].ToString(); //透析開始時間

                //sql = "select max(dialysis_time) as imax,min(dialysis_time) as imin from data_list";
                //sql += " where person_id='" + person_id + "' and floor_no='" + floor_no + "'";
                //sql += " and bed_no='" + bed_no + "' and dialysis_date='" + dia_date + "'";
                //dt_list = db.Query(sql);
                //if (dt_list == null || dt_list.Rows.Count == 0 || dt_list.Rows[0]["imin"] == null)
                //    return;
                //string imax = dt_list.Rows[0]["imax"].ToString();
                string imax = DateTime.Now.ToString("HH:mm:ss");
                DateTime tmax = Convert.ToDateTime(imax);
                DateTime tmin = Convert.ToDateTime(imin);

                //20160429 Alex 透析開始時間 cln1_col10不更新
                sql = "UPDATE clinical1_nurse SET ";
                var timeDiff = new TimeSpan(tmax.Ticks - tmin.Ticks);
                //if (timeDiff.TotalMinutes > 0)
                //{
                    // 透析結束時間 cln1_col11, 透析時間 cln1_col12
                    sql += "cln1_col11='" + imax + "',cln1_col12='" + timeDiff.ToString() + "' ";
                //}
                string where = "WHERE cln1_patic='" + person_id + "' AND cln1_diadate='" + dia_date + "'";
                sql += where;

                db.Excute(sql);
                db.Close();
            }
           
            /// <summary>
            /// pv_ic: Tex_Patient_ID.Text; macno: Tex_Machine_type.Text; weight: Tex_Patient_weight.Text;
            /// weight: before weight
            /// </summary>
            /// <param name="pv_ic"></param>
            static public void insert_new_pat_to_pat_visit(pat pat_obj, string macno, string weight)
            {
                string sql = "INSERT into pat_visit (pv_mrn, " +
                                                        "pv_ic, " +
                                                        "pv_datevisit, " +
                                                        "pv_floor, " +
                                                        "pv_sec, " +
                                                        "pv_time, " +
                                                        "pv_bedno, " +
                                                        "pv_macno, " +
                                                        "pv_hpack, " +
                                                        "pv_hpack2, " +
                                                        "pv_hpack3, " +
                                                        "pv_pattyp, " +
                                                        "pv_weight, " +
                                                        "pv_macstat) " +
                                               "VALUES('', " +
                                                      "'" + pat_obj.pif_ic + "', " +
                                                      "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', " +
                                                      "'" + pat_obj.floor + "', " +
                                                      "'" + pat_obj.area + "', " +
                                                      "'" + pat_obj.time + "', " +
                                                      "'" + pat_obj.bedno + "', " +
                                                      "'" + macno + "', " +
                                                      "'" + pat_obj.pif_hpack + "', " +  // ComboBox為什麼還要做Replace("'", "''") --> 多餘
                                                      "'" + pat_obj.pif_hpack2 + "', " +
                                                      "'" + pat_obj.pif_hpack3 + "', " +
                                                      "'H', " +
                                                      "'" + weight + "', " +
                                                      "'A') ";

                // 分流方式        pif_hpack, like. 自体内瘘
                // 透析器型號      pif_hpack2
                // 管路型號        pif_hpack3
                // 病患身分證號    pif_ic
                if (pat_obj == null
                   || pat_obj.pif_hpack == null
                   || pat_obj.pif_hpack2 == null
                   || pat_obj.pif_hpack3 == null
                   || pat_obj.pif_ic == null)
                {
                    throw new Exception("please check the sql: " + sql);
                }
                if (pat_obj.pif_hpack.Length == 0
                    || pat_obj.pif_hpack2.Length == 0
                    || pat_obj.pif_hpack3.Length == 0
                    || pat_obj.pif_ic.Length == 0)
                {
                    throw new Exception("please check the sql: " + sql);
                }
                DBMysql db = new DBMysql();
                db.Excute(sql);
            }
        }

        public enum pat_status { not_login, not_stop, stop };

        private pat_status _status;
        public pat_status status{
            get
            {
                _status = get_pat_status(this);
                return _status;
            }
            set
            {
                _status = status;
            }
        }

        #region 檢查今天開機與否？(pat_visit)
        static public bool register_today(string pif_ic)
        {
            DBMysql db = new DBMysql();
            string sql = "SELECT * FROM pat_visit " +
                               "WHERE pv_ic = '" + pif_ic + "' AND pv_datevisit = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                return true;
            }
            return false;
        }
        #endregion

        static public pat_status get_pat_status(pat pat_obj)
        {
            string floor = pat_obj.floor, area = pat_obj.area, time = pat_obj.time, bedno = pat_obj.bedno;

            if (DBMysql.does_login(pat_obj))
            {
                if (DBMysql.does_stop(pat_obj.pif_ic))
                {
                    return pat_status.stop;
                }
                else
                {
                    // 因為已經登入, 所以does_stop回傳false才能視為not stop, 否則does_stop回傳false是N/A
                    return pat_status.not_stop;
                }
            }
            return pat_status.not_login;

        }

        private void refresh()
        {
            string sql = "SELECT b.pif_hpack, b.pif_hpack2 as mechine_model, "+
                "b.pif_hpack3, b.pif_name name, b.pif_sex, b.pif_ic, " + 
                DateTime.Now.Year.ToString() + "-year(b.pif_dob) age, "+
                "b.pif_hpack,a.pv_macno,a.pv_weight,a.pv_macstat ";
            sql += "from pat_visit a,pat_info b  ";
            sql += "where a.pv_datevisit = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "and a.pv_floor = '" + floor + "' ";
            sql += "and a.pv_sec = '" + area + "' ";
            sql += "and a.pv_time = '" + time + "' ";
            sql += "and a.pv_bedno = '" + bedno + "' ";
            sql += "and a.pv_ic = b.pif_ic";
            string result;
            DataTable dt = DBMysql.query(sql, out result);
            if (dt == null || dt.Rows.Count == 0)
            {
                status = pat_status.not_login;
                return;
            }
            
            DataRow row = dt.Rows[0];
            name = row["name"].ToString();
            sex = row["pif_sex"].ToString();
            pif_ic = row["pif_ic"].ToString();
            age = row["age"].ToString();
            pif_hpack2 = row["mechine_model"].ToString();
            string tmp = Common.get_tube_model(row);
            if (tmp == null || tmp.Length == 0)
            {
                Common.SaveERR("debug", "pif_hpack3 is empty with sql: " + sql);
            }
            else
            {
                pif_hpack3 = tmp;
            }
            if (pif_hpack3 == "" || _pif_hpack3.Length == 0)
            {
                Common.SaveERR("debug", "在數據庫裡找不到管路型号, with sql: "+sql);
                //throw new Exception("在數據庫裡找不到管路型号");
            }
            if (row["pv_macstat"].ToString() == "S")//是否已經停機判斷
            {
                //已關機
                status = pat_status.stop;
            }
            else
            {
                status = pat_status.not_stop;
            }
           
        }

        /// <summary>
        /// _pat_id --> Tex_Patient_ID.Text
        /// </summary>
        /// <param name="_floor"></param>
        /// <param name="_area"></param>
        /// <param name="_time"></param>
        /// <param name="_bedno"></param>
        /// <param name="_pat_id"></param>
        public pat(string _floor, string _area, string _time, string _bedno, string _pif_ic = null)
        {
            floor = _floor;
            area = _area;
            time = _time;
            bedno = _bedno;
            if (_pif_ic != null)
            {
                pif_ic = _pif_ic;
            }
            refresh();
        }


    }
    
}
