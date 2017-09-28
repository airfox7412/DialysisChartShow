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

namespace Dialysis_Chart_Show.checkin
{
    public partial class SearchPSch : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (Text_Name.Text == "" && Session["PAT_NAME"] != null)
                    Text_Name.Text = Session["PAT_NAME"].ToString();
                if (Text_IC.Text == "" && Session["PAT_IC"] != null)
                    Text_IC.Text = Session["PAT_IC"].ToString();
                if (Request.QueryString["patic"] == null)
                {
                    Toolbar2.Hidden = false;
                }
                else
                {
                    Text_IC.Text = Request.QueryString["patic"];
                    Toolbar2.Hidden = true;
                }
                Load_Appointment();
            }
        }

        protected void Load_Appointment()
        {
            toDay = DateTime.Now.ToString("yyyy-MM-dd");
            string sSQL = "";
            //sSQL = "SELECT a.apptst_patic AS pat_ic, b.pif_name AS pat_name, a.apptst_flr AS floor, a.apptst_sec AS area, a.apptst_bed AS bedno, a.apptst_mactyp AS mactype, a.apptst_daytyp AS daytype, a.apptst_timetyp AS timetype ";
            //sSQL += "FROM appointment_setup a ";
            //sSQL += "LEFT JOIN pat_info b ON b.pif_ic=a.apptst_patic ";
            //sSQL += "WHERE b.pif_ic='" + Text_IC.Text + "' ";
            //sSQL += "UNION ";
            //sSQL += "SELECT a.ah_patic AS pat_ic, b.pif_name AS pat_name, a.ah_flr AS floor, a.ah_sec AS area, a.ah_bed AS bedno, a.ah_mactyp AS mactype, a.ah_dycnt AS daytype, a.ah_timetyp AS timetype ";
            //sSQL += "FROM appointment_change a ";
            //sSQL += "LEFT JOIN pat_info b ON b.pif_ic=a.ah_patic ";
            //sSQL += "WHERE a.ah_patic='" + Text_IC.Text + "' AND a.ah_date>='" + toDay + "' ";
            sSQL = "SELECT b.pif_ic AS pat_ic, b.pif_name AS pat_name, a.pv_floor AS floor, a.pv_sec AS area, a.pv_bedno AS bedno, a.appointment_date as ddate, '0' AS daytype, a.time_type as timetype ";
            sSQL += "FROM appointment a ";
            sSQL += "LEFT JOIN pat_info b ON a.pif_id=b.pif_id ";
            sSQL += "WHERE (b.pif_name='" + Text_Name.Text + "' OR b.pif_ic='" + Text_IC.Text + "') AND a.appointment_date>='" + toDay + "' ";
            sSQL += "UNION ";
            sSQL += "SELECT a.ah_patic AS pat_ic, b.pif_name AS pat_name, a.ah_flr AS floor, a.ah_sec AS area, a.ah_bed AS bedno, a.ah_date AS ddate, a.ah_dycnt AS daytype, a.ah_timetyp AS timetype ";
            sSQL += "FROM appointment_change a ";
            sSQL += "LEFT JOIN pat_info b ON a.ah_patic=b.pif_ic ";
            sSQL += "WHERE (b.pif_name='" + Text_Name.Text + "' OR a.ah_patic='" + Text_IC.Text + "') AND a.ah_date>='" + toDay + "' ";
            sSQL += "ORDER BY ddate";
            DataTable dt = db.Query(sSQL);
            System.Data.DataView dv = dt.DefaultView;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                DateTime ddate = Convert.ToDateTime(dt.Rows[i]["ddate"].ToString());
                dv[i]["daytype"] = GetWeekNo((int)ddate.DayOfWeek);
                dv[i]["timetype"] = set_ctime(dt.Rows[i]["timetype"].ToString());
            }
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
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

        protected void btnQuery_Click(object sender, DirectEventArgs e)
        {
            Load_Appointment();
        }

        protected void btnReset_Click(object sender, DirectEventArgs e)
        {
            Text_Name.Text = "";
            Text_IC.Text = "";
            Load_Appointment();
        }

        protected void QueryIC(object sender, DirectEventArgs e)
        {
            if (Text_Name.Text.Length >= 2)
            {
                string sSQL = "SELECT pif_ic FROM pat_info ";
                sSQL += "WHERE pif_name='" + Text_Name.Text + "'";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count > 0)
                    Text_IC.Text = dt.Rows[0]["pif_ic"].ToString();
                else
                    Text_IC.Text = "";
            }
        }

        protected void btnInsert_Click(object sender, DirectEventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "PSchInsert.aspx?PIC=" + Text_IC.Text + "&PNAME=" + Text_Name.Text;
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
    }
}