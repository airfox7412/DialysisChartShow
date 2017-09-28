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
    public partial class TempPatient_Sch : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GetWeek();
                Show_Floor();
                Show_Area();
                Show_TimeSec();
                Load_Appointment();
            }
        }

        protected void Load_Appointment()
        {
            var datasource = new List<Project>();
            string floor, timetype, area, bedno, mac_typ, machine;
            try
            {
                floor = Common.GetComboBoxValue(cboFLOOR);
            }
            catch (Exception ex)
            {
                floor = sFLOOR.Text;
            }

            try
            {
                timetype = Common.GetComboBoxValue(cboTIME);
            }
            catch (Exception ex)
            {
                timetype = sTIME.Text;
            }

            try
            {
                area = Common.GetComboBoxValue(cboArea);
            }
            catch (Exception ex)
            {
                area = sAREA.Text;
            }

            string sql = "SELECT a.mac_flr, a.mac_sec, a.mac_bedno, a.mac_typ, b.genst_desc AS machine FROM mac_setup a ";
            sql += "LEFT JOIN general_setup b ON b.genst_code=a.mac_brand ";
            sql += "WHERE a.mac_status='Y' AND mac_flr='" + floor + "' AND b.genst_ctg='macbrd' ";
            if (area != "全区")
            {
                sql += "AND a.mac_sec='" + area + "' ";
            }
            sql += "ORDER BY a.mac_sec, a.mac_bedno";
            DataTable dt1 = db.Query(sql);
            if (dt1.Rows.Count > 0)
            {
                foreach (DataRow dr1 in dt1.Rows)
                {
                    area = dr1["mac_sec"].ToString();
                    bedno = dr1["mac_bedno"].ToString();
                    mac_typ = dr1["mac_typ"].ToString();
                    machine = dr1["machine"].ToString();
                    sql = "SELECT apptst_patrefid, apptst_daytyp FROM appointment_setup ";
                    sql += "WHERE apptst_flr='" + floor + "' AND apptst_sec='" + area + "' AND apptst_bed='" + bedno + "' AND apptst_timetyp='" + timetype + "' ";
                    sql += "ORDER BY apptst_daytyp";
                    DataTable dt2 = db.Query(sql);
                    string[] patname = new string[7];
                    int i = 0;
                    int k = 0;
                    for (i = 0; i < 7; i++)
                        patname[i] = "";

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        i = int.Parse(dr2["apptst_daytyp"].ToString()) - 1;
                        patname[i] = dr2["apptst_patrefid"].ToString();
                    }

                    #region 增加臨時預約病患
                    int j = GetWeekNo();
                    for (i = 0; i < 7; i++)
                    {
                        if (i == j)
                        {
                            sql = "SELECT a.*, b.pif_name FROM appointment_change a ";
                            sql += "LEFT JOIN pat_info b ON a.ah_patic=b.pif_ic ";
                            sql += "WHERE a.ah_date='" + toDay + "' AND a.ah_flr='" + floor + "' AND a.ah_sec='" + area + "' AND a.ah_bed='" + bedno + "' AND a.ah_timetyp='" + timetype + "' ";
                            DataTable dt0 = db.Query(sql);
                            if (dt0.Rows.Count > 0)
                                patname[i] = dt0.Rows[0]["pif_name"].ToString();
                        }
                    }
                    #endregion

                    datasource.Add(new Project(area + "区", bedno, mac_typ, patname[0], patname[1], patname[2], patname[3], patname[4], patname[5], patname[6], machine));
                }
                Store1.DataSource = datasource;
                Store1.DataBind();
            }
        }

        public class Project
        {
            public Project(string area, string bedtype, string mactype, string name1, string name2, string name3, string name4, string name5, string name6, string name7, string machine)
            {
                Area = area;
                Machine = machine;
                BedType = bedtype + " " + mactype;
                Week1 = name1;
                Week2 = name2;
                Week3 = name3;
                Week4 = name4;
                Week5 = name5;
                Week6 = name6;
                Week7 = name7;
            }

            public string Area { get; set; }
            public string BedType { get; set; }
            public string Week1 { get; set; }
            public string Week2 { get; set; }
            public string Week3 { get; set; }
            public string Week4 { get; set; }
            public string Week5 { get; set; }
            public string Week6 { get; set; }
            public string Week7 { get; set; }
            public string Machine { get; set; }
        }

        #region 顯示時段
        private void Show_TimeSec()
        {
            DateTime now = DateTime.Now;
            if (Session["SCH_TIME"] == null)
            {
                int Hm = int.Parse(now.ToString("HHmm"));
                if (1 <= Hm && Hm <= 1159)
                {
                    sTIME.Text = "001";
                    cboTIME.Select(sTIME.Text);
                }
                else if (1200 <= Hm && Hm <= 1759)
                {
                    sTIME.Text = "002";
                    cboTIME.Select(sTIME.Text);
                }
                else if (1800 <= Hm && Hm <= 2400)
                {
                    sTIME.Text = "003";
                    cboTIME.Select(sTIME.Text);
                }
            }
            else
            {
                sTIME.Text = Session["SCH_TIME"].ToString();
                cboTIME.Select(sTIME.Text);
            }
        }
        #endregion

        #region 顯示樓層
        protected void Show_Floor()
        {
            string sSQL = "SELECT distinct mac_flr AS CODE, CONCAT(mac_flr,'楼') AS NAME FROM mac_setup";
            DataTable dt1 = db.Query(sSQL);
            if (dt1.Rows.Count > 0)
            {
                sFLOOR.Text = dt1.Rows[0]["CODE"].ToString();
                Common.SetComboBoxItem(cboFLOOR, dt1, false, "NAME", "CODE");
            }
            cboFLOOR.Select(0);
        }
        #endregion

        #region 顯示床区
        protected void Show_Area()
        {
            string sSQL = "SELECT distinct mac_sec AS CODE, CONCAT(mac_sec,'区') AS NAME FROM mac_setup";
            DataTable dt1 = db.Query(sSQL);
            Ext.Net.ListItem items;
            items = new Ext.Net.ListItem("全区", "全区");
            cboArea.Items.Add(items);
            foreach (DataRow dr in dt1.Rows)
            {
                items = new Ext.Net.ListItem(dr["NAME"].ToString(), dr["CODE"].ToString());
                cboArea.Items.Add(items);
            }
            cboArea.Select(0);
            sAREA.Text = "全区";
        }
        #endregion

        #region 取得星期
        protected void GetWeek()
        {
            DateTime date1 = DateTime.Now;
            switch (date1.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    txtWEEK.Text = "星期一";
                    sWEEK.Text = "1";
                    break;
                case DayOfWeek.Tuesday:
                    txtWEEK.Text = "星期二";
                    sWEEK.Text = "2";
                    break;
                case DayOfWeek.Wednesday:
                    txtWEEK.Text = "星期三";
                    sWEEK.Text = "3";
                    break;
                case DayOfWeek.Thursday:
                    txtWEEK.Text = "星期四";
                    sWEEK.Text = "4";
                    break;
                case DayOfWeek.Friday:
                    txtWEEK.Text = "星期五";
                    sWEEK.Text = "5";
                    break;
                case DayOfWeek.Saturday:
                    txtWEEK.Text = "星期六";
                    sWEEK.Text = "6";
                    break;
                case DayOfWeek.Sunday:
                    txtWEEK.Text = "星期日";
                    sWEEK.Text = "7";
                    break;
            }
        }
        #endregion

        protected int GetWeekNo()
        {
            DateTime date1 = DateTime.Now;
            int WeekNo = 0;
            switch (date1.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    WeekNo = 0;
                    break;
                case DayOfWeek.Tuesday:
                    WeekNo = 1;
                    break;
                case DayOfWeek.Wednesday:
                    WeekNo = 2;
                    break;
                case DayOfWeek.Thursday:
                    WeekNo = 3;
                    break;
                case DayOfWeek.Friday:
                    WeekNo = 4;
                    break;
                case DayOfWeek.Saturday:
                    WeekNo = 5;
                    break;
                case DayOfWeek.Sunday:
                    WeekNo = 6;
                    break;
            }
            return WeekNo;
        }

        protected void Query_Click(object sender, DirectEventArgs e)
        {
            Load_Appointment();
        }

        protected void SelectBed(object sender, DirectEventArgs e)
        {
            Common._ErrorMsgShow("AAAAAA");
        }
    }
}