using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using NLog;

namespace Dialysis_Chart_Show
{
    public partial class appointment : System.Web.UI.Page
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        BaseForm bs = new BaseForm();

        /// <summary>
        /// 排班轉檔作業
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Page_Load(object sender, EventArgs e)
        {
            //
            this.Panel4.Hidden = true;
            this.txtMESSAGE.Text = "排班轉檔......";
            if (!X.IsAjaxRequest)
            {

            }
            // 未來排班轉檔程式要從 Web 搬移出去 , 在 windows 排程中 執行 

            //
            DBMysql db = new DBMysql();
            DateTime now = DateTime.Now;
            this.txtDATE.Text = now.ToString("yyyy-MM-dd");
            this.txtTIME.Text = now.ToString("yyyy-MM-dd HH:mm:ss");
            string w_apptst_flr = "";
            string sql = "";
            GET_WEEK();
            string w_flag = "";
            //目前時間
            int Hm = int.Parse(now.ToString("HHmm"));
            //Hm = 2300;
            //this.txtWEEK.Text = "星期日";

            if (this.txtWEEK.Text == "星期日") 
            {
                if (Hm >= 2200 && w_flag == "")
                {
                    //星期日 22:00開始 排班轉檔       
                    DateTime dt = DateTime.Now;
                    DateTime w_date1;
                    DateTime w_date2;
                    DateTime w_date3;
                    DateTime w_date4;
                    DateTime w_date5;
                    DateTime w_date6;
                    DateTime w_date7;
                    w_date1 = dt.AddDays(1);
                    string s_date1 = w_date1.ToString("yyyy-MM-dd");                   
                    w_date2 = dt.AddDays(2);
                    string s_date2 = w_date2.ToString("yyyy-MM-dd");
                    w_date3 = dt.AddDays(3);
                    string s_date3 = w_date3.ToString("yyyy-MM-dd");
                    w_date4 = dt.AddDays(4);
                    string s_date4 = w_date4.ToString("yyyy-MM-dd");
                    w_date5 = dt.AddDays(5);
                    string s_date5 = w_date5.ToString("yyyy-MM-dd");
                    w_date6 = dt.AddDays(6);
                    string s_date6 = w_date6.ToString("yyyy-MM-dd");
                    w_date7 = dt.AddDays(7);
                    string s_date7 = w_date7.ToString("yyyy-MM-dd");   
                          
                    //
                    try
                    {
                        //select a.*,b.pif_id,b.pif_name from appointment a, pat_info b where a.pif_id = b.pif_id order by a.pif_id                        
                        //SELECT a.*,b.pif_id,b.pif_name as patrefid,b.pif_ic as patic  FROM appointment a,pat_info b 
                        //where  a.pif_id = b.pif_id 
                        //and appointment_date in ('2015-06-22','2015-06-23','2015-06-24','2015-06-25','2015-06-26','2015-06-27','2015-06-28') ORDER BY appointment_date 
                        //20150909 Andy
                        sql = "";
                        sql = "DELETE FROM appointment_setup";
                        DataTable appointment_DATA = db.Query(sql);

                        sql = "";
                        sql = "SELECT mac_flr, mac_sec, mac_bedno, mac_typ FROM mac_setup";
                        DataTable mac_setup_DATA = db.Query(sql);        

                        sql = "";
                        sql = "SELECT a.*,b.pif_id,b.pif_name as patrefid,b.pif_ic as patic  FROM appointment a,pat_info b where  a.pif_id = b.pif_id  and  appointment_date in ('" +
                                   s_date1 + "','" + s_date2 + "','" + s_date3 + "','" + s_date4 + "','" +
                                   s_date5 + "','" + s_date6 + "','" + s_date7 + "')" + " ORDER BY appointment_date ";
                        DataTable dtappointment_DATA = db.Query(sql);

                        foreach (DataRow dr in dtappointment_DATA.Rows)
                        {
                            try
                            {
                                dr["mac_type"] = mac_setup_DATA.Select("mac_flr = '" + dr["pv_floor"].ToString() + "' and mac_sec = '" + dr["pv_sec"].ToString() + "' and mac_bedno = '" + dr["pv_bedno"].ToString() + "'")[0]["mac_typ"].ToString();
                            }
                            catch (Exception ex)
                            {
                                logger.Error(ex.Message + ":" + ex.StackTrace);
                            }
                        }
                           
                        //if (dtappointment_DATA.Rows.Count > 0)
                        for (int i = 0; i < dtappointment_DATA.Rows.Count; i++)
                        {
                            sql  = "";
                            sql  = "insert into  appointment_setup  (apptst_flr,apptst_sec,";
                            sql += "apptst_bed,apptst_mactyp,apptst_patrefid,apptst_patic,apptst_wktyp,apptst_daytyp,apptst_timetyp) ";
                            sql += " values ('"; 
                            sql +=  dtappointment_DATA.Rows[i]["pv_floor"].ToString() + "','";
                            sql +=  dtappointment_DATA.Rows[i]["pv_sec"].ToString()   + "','";                           
                            sql +=  dtappointment_DATA.Rows[i]["pv_bedno"].ToString() + "','"; 
                            sql +=  dtappointment_DATA.Rows[i]["mac_type"].ToString() + "','"; 
                            sql +=  dtappointment_DATA.Rows[i]["patrefid"].ToString() + "','";
                            sql +=  dtappointment_DATA.Rows[i]["patic"].ToString() + "','";
                            //1 
                            if (dtappointment_DATA.Rows[i]["appointment_date"].ToString() == s_date1 )
                            {
                                sql += "135"  + "','";
                                sql += "1"    + "','";
                                sql +=  dtappointment_DATA.Rows[i]["time_type"].ToString() + "'";
                            }
                            if (dtappointment_DATA.Rows[i]["appointment_date"].ToString() == s_date2)
                            {
                                sql += "246" + "','";
                                sql += "2" + "','";
                                sql += dtappointment_DATA.Rows[i]["time_type"].ToString() + "'";
                            }
                            if (dtappointment_DATA.Rows[i]["appointment_date"].ToString() == s_date3)
                            {
                                sql += "135" + "','";
                                sql += "3" + "','";
                                sql += dtappointment_DATA.Rows[i]["time_type"].ToString() + "'";
                            }
                            if (dtappointment_DATA.Rows[i]["appointment_date"].ToString() == s_date4)
                            {
                                sql += "246" + "','";
                                sql += "4" + "','";
                                sql += dtappointment_DATA.Rows[i]["time_type"].ToString() + "'";
                            }
                            if (dtappointment_DATA.Rows[i]["appointment_date"].ToString() == s_date5)
                            {
                                sql += "135" + "','";
                                sql += "5" + "','";
                                sql += dtappointment_DATA.Rows[i]["time_type"].ToString() + "'";
                            }
                            if (dtappointment_DATA.Rows[i]["appointment_date"].ToString() == s_date6)
                            {
                                sql += "246" + "','";
                                sql += "6" + "','";
                                sql += dtappointment_DATA.Rows[i]["time_type"].ToString() + "'";
                            }
                            if (dtappointment_DATA.Rows[i]["appointment_date"].ToString() == s_date7)
                            {
                                sql += "135" + "','";
                                sql += "7" + "','";
                                sql += dtappointment_DATA.Rows[i]["time_type"].ToString() + "'";
                            }
                            sql +=  ")";
                            db.Excute(sql);                                               
                        }
                    }
                    catch (Exception ex)
                    {
                        Common._ErrorMsgShow(ex.Message.ToString());
                        //logger.Error(ex.Message + ":" + ex.StackTrace);
                    } 
                }
            }

            w_flag = "Y";
        }
        //

        //WEEK
        protected void GET_WEEK()
        {
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    this.txtWEEK.Text = "星期一";
                    sWEEK.Text = "1";
                    break;
                case DayOfWeek.Tuesday:
                    this.txtWEEK.Text = "星期二";
                    sWEEK.Text = "2";
                    break;
                case DayOfWeek.Wednesday:
                    this.txtWEEK.Text = "星期三";
                    sWEEK.Text = "3";
                    break;
                case DayOfWeek.Thursday:
                    this.txtWEEK.Text = "星期四";
                    sWEEK.Text = "4";
                    break;
                case DayOfWeek.Friday:
                    this.txtWEEK.Text = "星期五";
                    sWEEK.Text = "5";
                    break;
                case DayOfWeek.Saturday:
                    this.txtWEEK.Text = "星期六";
                    sWEEK.Text = "6";
                    break;
                case DayOfWeek.Sunday:
                    this.txtWEEK.Text = "星期日";
                    sWEEK.Text = "7";
                    break;
            }
        }
        //


    }
}