using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show.checkin
{
    public partial class PSchInsert : BaseForm
    {
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (Request.QueryString["PIC"] != null)
                {
                    PIC.Text = Request.QueryString["PIC"].ToString();
                    PNAME.Text = Request.QueryString["PNAME"].ToString();
                    string sSQL = "SELECT * FROM pat_info WHERE pif_ic='" + PIC.Text + "'";
                    DataTable dt = db.Query(sSQL);
                    if (dt.Rows.Count > 0)
                        PID.Text = dt.Rows[0]["pif_id"].ToString();
                }
                InsertDate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                timetype.Select(0);
            }
        }

        protected string GetWeekNo(int iWeek)
        {
            string[] sWeek = { "周日", "周一", "周二", "周三", "周四", "周五", "周六" };
            return sWeek[iWeek];
        }

        private string set_ctime(string time)
        {
            int tint = int.Parse(time) - 1;
            string[] ttype = { "上午", "下午", "晚上" };
            return ttype[tint];
        }

        protected void Query_click(object sender, DirectEventArgs e)
        {
            DateTime date1 = Convert.ToDateTime(InsertDate.Text);
            string sSQL;
            sSQL = "SELECT a.mac_flr AS floor, a.mac_sec AS area, a.mac_bedno AS bedno ";
            sSQL += ",case '" + Common.GetComboBoxValue(timetype) + "' when '001' then '上午' when '002' then '下午' when '003' then '晚上' end AS timetype ";
            sSQL += "FROM mac_setup a ";
            sSQL += "WHERE a.mac_bedno not in ";
            sSQL += "(SELECT c.mac_bedno FROM mac_setup c ";
            sSQL += "LEFT JOIN appointment_setup b ON b.apptst_flr =c.mac_flr AND b.apptst_sec =c.mac_sec AND b.apptst_bed =c.mac_bedno ";
            sSQL += "WHERE c.mac_status='Y' AND b.apptst_daytyp='" + ((int)date1.DayOfWeek).ToString() + "' ";
            if (timetype.Text != "")
            {
                sSQL += "AND b.apptst_timetyp='" + Common.GetComboBoxValue(timetype) + "' ";
            }
            sSQL += ") ORDER BY a.mac_flr, a.mac_sec, a.mac_bedno";

            DataTable dt = db.Query(sSQL);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }

        protected void Add_Click(object sender, DirectEventArgs e)
        {
            string sSQL = "";
            bool booking = false;
            string commandName = e.ExtraParams["command"];
            string floor = e.ExtraParams["floor"].ToString();
            string area = e.ExtraParams["area"].ToString();
            string bedno = e.ExtraParams["bedno"].ToString();
            string username = Session["USER_NAME"].ToString();

            DateTime selectDay = Convert.ToDateTime(InsertDate.SelectedDate);
            toDay = DateTime.Now.ToString("yyyy-MM-dd");

            int iweek = (int)selectDay.DayOfWeek;

            if (selectDay < DateTime.Now)
            {
                _NotificationShow("今日之前不能进行排班!");
                return;
            }
            else
            {
                booking = true;
                string sql1 = "SELECT * FROM appointment_setup ";
                sql1 += "WHERE apptst_patic='" + _PAT_IC + "' AND apptst_daytyp='" + iweek + "'";
                DataTable dt1 = db.Query(sql1);
                if (dt1.Rows.Count > 0)
                {
                    string sql2 = "SELECT * FROM appointment_change ";
                    sql2 += "WHERE ah_date='" + toDay + "' ";
                    sql2 += "AND ah_flr='" + dt1.Rows[0]["apptst_flr"].ToString() + "' ";
                    sql2 += "AND ah_sec='" + dt1.Rows[0]["apptst_sec"].ToString() + "' ";
                    sql2 += "AND ah_bed='" + dt1.Rows[0]["apptst_bed"].ToString() + "' ";
                    sql2 += "AND ah_timetyp='" + dt1.Rows[0]["apptst_timetyp"].ToString() + "' ";
                    DataTable dt2 = db.Query(sql2);
                    if (dt2.Rows.Count == 0) //判斷床位是否已刪除
                    {
                        booking = false;
                    }
                }

                string sql3 = "SELECT * FROM appointment_change ";
                sql3 += "WHERE ah_patic='" + _PAT_IC + "' AND ah_date='" + toDay + "' ";
                DataTable dt3 = db.Query(sql3);
                if (dt3.Rows.Count > 0)
                {
                    booking = false;
                }
            }

            if (booking == false)
            {
                Common._ErrorMsgShow("病患 " + Session["PAT_NAME"].ToString() + "</br>" + toDay + "</br>(" + GetWeekNo(iweek) + ")已经有排班!");
                return;
            }
            else
            {
                sSQL = "INSERT INTO appointment (pv_floor, pv_sec, pv_bedno, mac_type, appointment_date, time_type, pif_id, status, create_by, create_on) ";
                sSQL += "VALUES(";
                sSQL += "'" + floor + "',";
                sSQL += "'" + area + "',";
                sSQL += "'" + bedno + "',";
                sSQL += "'HD',";
                sSQL += "'" + _Get_YMD2(InsertDate.Text) + "',";
                sSQL += "'" + Common.GetComboBoxValue(timetype) + "',";
                sSQL += "'" + PID.Text + "',";
                sSQL += "'booked',";
                sSQL += "'" + username + "',";
                sSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd hh:mm:ss") + "')";
                Common._ErrorMsgShow(sSQL);
                //db.Excute(sSQL);
            }

        }
    }
}