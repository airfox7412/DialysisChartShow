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
    public class DBMysql
    {
        string MySqlString="";

        public MySqlConnection myConnection;
        public MySqlDataAdapter myAdapter;
        public MySqlCommandBuilder myCommandBuilder;
        public MySqlCommand myCommand;
        public DBMysql()
        {
            try
            {
                MySqlString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString();
                myConnection = new MySqlConnection(MySqlString);
                myConnection.Open();
            }
            catch (MySql.Data.MySqlClient.MySqlException ex)
            {
                switch (ex.Number)
                {
                    case 0:
                        SaveERR("DBMysql() : ", "无法连线到资料库.");
                        Common._ErrorMsgShow(ex.Message);
                        break;
                    case 1045:
                        SaveERR("DBMysql() : ", "使用者帐号或密码错误,请再试一次.");
                        break;
                } 
            }
        }

        /// <summary>
        /// false: not login; true: login
        /// </summary>
        /// <param name="floor"></param>
        /// <param name="area"></param>
        /// <param name="time"></param>
        /// <param name="beno"></param>
        /// <returns></returns>
        static public bool does_login(pat pat_obj)
        {
            string floor = pat_obj.floor, area = pat_obj.area, time = pat_obj.time, bedno = pat_obj.bedno;

            string sql = "SELECT b.pif_name name,b.pif_sex,b.pif_ic id," + DateTime.Now.Year.ToString() + "-year(b.pif_dob) age,b.pif_hpack,a.pv_macno,a.pv_weight,a.pv_macstat ";

            sql += "from pat_visit a,pat_info b  ";
            sql += "where a.pv_datevisit = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "and a.pv_floor = '" + floor + "' ";
            sql += "and a.pv_sec = '" + area + "' ";
            sql += "and a.pv_time = '" + time + "' ";
            sql += "and a.pv_bedno = '" + bedno + "' ";
            sql += "and a.pv_ic = b.pif_ic";
            string result;
            DataTable dt = DBMysql.query(sql, out result);

            if (dt.Rows.Count == 0)
            {
                return false;
            }
            // if(dt.Rows.Count > 0)
            // {
            //     throw new Exception("在同一天而且同一個時段而且同一張床查到兩筆以上的淨化資料");
            // }
            return true;
        }

        /// <summary>
        /// true: stop; false: N/A
        /// pv_ic, pif_ic, pif_ic='" + Tex_Patient_ID.Text + "' ";
        /// </summary>
        /// <returns></returns>
        static public bool does_stop(string pv_ic)
        {
            string sql = "SELECT a.* ";
            sql += "FROM pat_visit a ";
            sql += "WHERE a.pv_ic = '" + pv_ic + "' ";
            sql += "  AND a.pv_datevisit = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "  AND a.pv_macstat = 'S'";
            string result;
            DataTable dt = DBMysql.query(sql, out result);
            if (dt.Rows.Count == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Never return NULL
        /// </summary>
        /// <param name="sql_stmt"></param>
        /// <returns></returns>
        static public DataTable query(String sql_stmt)
        {
            String result;
            DataTable dt = query(sql_stmt, out result);
            return dt == null ? new DataTable() : dt;
        }

        /// <summary>
        /// Never return NULL;
        /// result: null --> normal; not null --> abnormal
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="result"></param>
        /// <returns></returns>
        static public DataTable query(string sql, out string result, string connectionStr = null)
        {
            DataTable dt = new DataTable();
            result = null;

            MySqlConnection mySqlConnection = null;
            MySqlDataAdapter mySqlAdapter = null;
            try
            {
                string mySqlConnectStr = (connectionStr != null ? connectionStr : ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString());
                mySqlConnection = new MySqlConnection(mySqlConnectStr);
                mySqlAdapter = new MySqlDataAdapter(sql, mySqlConnection);
                mySqlAdapter.Fill(dt);
                result = null;
            }
            catch (Exception ex)
            {
                result = "sql: " + sql + ", " + ex.Message;
            }
            finally
            {
                if (mySqlAdapter != null)
                {
                    mySqlAdapter.Dispose();
                }
                if (mySqlConnection != null)
                {
                    mySqlConnection.Dispose();
                    mySqlConnection.Close();
                }
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable Query2(string sql, DataTable dt)
        {
            try
            {
                //myAdapter = new MySqlDataAdapter();
                //myAdapter.SelectCommand = new MySqlCommand(sql,myConnection);
                myAdapter = new MySqlDataAdapter(sql, MySqlString);
                myAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
                SaveERR("Query2 : " + sql, s);
            }
            return dt;
        }

        /// <summary>
        /// 獨立 Query, 查完自動回收 connection
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// 
        public DataTable QueryIndependent(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                using (MySqlConnection connection = new MySqlConnection(MySqlString))
                {
                    myAdapter = new MySqlDataAdapter(sql, connection);
                    myAdapter.Fill(dt);
                }
            }
            catch (Exception ex)
            {
                SaveERR("Query Error : " + sql, ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// 原始 Query, 支持一個 connection 做多次查詢, 查詢完畢建議呼叫 Close()
        /// 直接以 DBMysql() Constructor 所 open 的 myConnection 執行查詢
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        /// 
        public DataTable Query(string sql)
        {
            DataTable dt = new DataTable();
            try
            {
                myAdapter = new MySqlDataAdapter(sql, myConnection);
                myAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                SaveERR("Query : " + sql, ex.Message);
            }
            return dt;
        }

        /// <summary>
        /// Fill, 支持一個 connection 做多次 Fill, 查詢完畢建議呼叫 Close()
        /// 直接以 DBMysql() Constructor 所 open 的 myConnection 執行 SQL
        /// </summary>
        /// <param name="sql"></param>
        /// <param name="dt"></param>
        /// <returns></returns>
        public DataTable Fill(string sql, DataTable dt)
        {

            try
            {
                myAdapter = new MySqlDataAdapter(sql, myConnection);
                myAdapter.Fill(dt);
            }
            catch (Exception ex)
            {
                string s = ex.ToString();
            }
            return dt;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sql"></param>
        /// <returns></returns>
        public Boolean Excute(string sql)
        {
            string sFILE = "c:\\web.log\\" + DateTime.Now.ToString("yyyyMMdd") + ".log";
            MySqlCommand cmd = new MySqlCommand();
            cmd.Connection = myConnection;
            if (myConnection.State == ConnectionState.Open)
            {
                using (FileStream fs = File.Open(sFILE, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " Y\t" + myConnection.State.ToString() + "\t" + sql + "\r\n");
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
            }
            else
            {
                myConnection.Open();
                using (FileStream fs = File.Open(sFILE, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
                {
                    Byte[] info = new UTF8Encoding(true).GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + " N\t" + myConnection.State.ToString() + "\t" + sql + "\r\n");
                    fs.Write(info, 0, info.Length);
                    fs.Close();
                }
            }
            if (sql != "")
            {
                cmd.CommandText = sql;
                cmd.ExecuteNonQuery();
            }
            return true;
        }

        public void Close()
        {
            if (myAdapter != null)
            {
                myAdapter.Dispose();
            }
            if (myConnection != null)
            {
                myConnection.Dispose();
                myConnection.Close();
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public object[] GetDataArray(DataTable dt)
        {
            object[] objx = new Object[dt.Rows.Count];
            int i = 0;
            foreach (DataRow irow in dt.Rows)
            {
                object[] objy = new object[dt.Columns.Count];

                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    objy[j] = irow[j];
                }
                objx[i] = objy;
                i++;
            }
            return objx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public object[] GetDataArray_AddRowNum(DataTable dt)
        {
            object[] objx = new Object[dt.Rows.Count];
            int i = 0;

            foreach (DataRow irow in dt.Rows)
            {
                object[] objy = new object[dt.Columns.Count + 1];
                objy[0] = i + 1;
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    objy[j + 1] = irow[j];
                }

                objx[i] = objy;
                i++;
            }
            return objx;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="s1"></param>
        /// <param name="s2"></param>
        public void SaveERR(string s1, string s2)
        {
            string sFILE = "c:\\web.log\\ERR_" + DateTime.Now.ToString("yyyyMMdd") + ".txt";
            using (FileStream fs = File.Open(sFILE, FileMode.Append, FileAccess.Write, FileShare.ReadWrite))
            {
                Byte[] info = new UTF8Encoding(true).GetBytes(DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "\t" + s1 + "\t" + s2 + "\r\n");
                fs.Write(info, 0, info.Length);
                fs.Close();
            }
        }
    }
    
}
