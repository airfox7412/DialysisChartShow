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
    public partial class SchTable : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.ResourceManager1.RegisterIcon(Icon.Neighbourhood);
                GetWeek();
                Show_Floor();
                Show_Area();
                Show_TimeSec();
                Load_Appointment();
            }
        }

        protected void Load_Appointment()
        {
            var datasource = new List<BedWeeklySchedule>();
            string floor, timetype, area, bedno, mac_typ, machine, kind;

            floor = Common.GetComboBoxValue(cboFLOOR);
            if (floor == "")
                floor = sFLOOR.Text;

            area = Common.GetComboBoxValue(cboArea);
            if (area == "")
                area = sAREA.Text;

            timetype = Common.GetComboBoxValue(cboTIME);
            if (timetype == "")
                timetype = sTIME.Text;
            else
                sTIME.Text = timetype;

            string sql = "SELECT a.mac_flr, a.mac_sec, a.mac_bedno, a.mac_typ, a.mac_kind, b.genst_desc AS machine FROM mac_setup a ";
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
                    kind = dr1["mac_kind"].ToString();

                    sql = "SELECT a.apptst_patic, a.apptst_daytyp, b.pif_name FROM appointment_setup a ";
                    sql += "LEFT JOIN pat_info b ON b.pif_ic=a.apptst_patic ";
                    sql += "WHERE a.apptst_flr='" + floor + "' AND a.apptst_sec='" + area + "' AND a.apptst_bed='" + bedno + "' AND a.apptst_timetyp='" + timetype + "' ";
                    sql += "ORDER BY a.apptst_daytyp";
                    DataTable dt2 = db.Query(sql);
                    BedWeeklySchedule.Patient[] patname = new BedWeeklySchedule.Patient[7];
                    int i = 0;
                    for (i = 0; i < 7; i++) {
                        patname[i] = new BedWeeklySchedule.Patient {
                            Id = "",
                            Name = "",
                            IsChanged = false
                        };
                    }

                    foreach (DataRow dr2 in dt2.Rows)
                    {
                        i = int.Parse(dr2["apptst_daytyp"].ToString()) - 1;
                        string patic = dr2["apptst_patic"].ToString();
                        string pif_name = dr2["pif_name"].ToString();
                        if (dr2["apptst_daytyp"].ToString() == sWEEK.Text)
                        {
                            string sSQL = "SELECT pv_ic FROM pat_visit WHERE pv_ic='" + patic + "' AND pv_datevisit='" + toDay + "'";
                            DataTable sdt = db.Query(sSQL);
                            if (sdt.Rows.Count > 0)
                            {
                                pif_name += "ST";
                            }
                        }
                        patname[i] = new BedWeeklySchedule.Patient {
                            Id = patic,
                            Name = pif_name
                        };
                    }

                    #region 增加臨時預約病患
                    //int j = GetWeekNo();
                    DateTime thisDay;
                    for (i = 0; i < 7; i++)
                    {
                        int iweek = Convert.ToInt16(DateTime.Now.DayOfWeek);
                        if (iweek == 0) iweek = 7;
                        thisDay = DateTime.Now.AddDays(Convert.ToDouble(1 - iweek) + i);
                        sql = "SELECT a.*, b.pif_name FROM appointment_change a ";
                        sql += "LEFT JOIN pat_info b ON a.ah_patic=b.pif_ic ";
                        sql += "WHERE a.ah_date='" + thisDay.ToString("yyyy-MM-dd") + "' AND a.ah_flr='" + floor + "' AND a.ah_sec='" + area + "' AND a.ah_bed='" + bedno + "' AND a.ah_timetyp='" + timetype + "' ";
                        DataTable dt0 = db.Query(sql);
                        if (dt0.Rows.Count > 0)
                        {
                            string patId = dt0.Rows[0]["ah_patic"] == null ? "" : dt0.Rows[0]["ah_patic"].ToString();
                            if (patId != "")
                            {
                                //dt2前面宣告使用過: 檢查沒有排入預設排班者，就是臨時排班病人
                                dt2 = db.Query("SELECT apptst_bed FROM appointment_setup WHERE apptst_patic='" + patId + "'");
                                string patName = dt0.Rows[0]["pif_name"].ToString();
                                if (dt0.Rows[0]["ah_dycnt"].ToString() == sWEEK.Text)
                                {
                                    string sSQL = "SELECT pv_ic FROM pat_visit WHERE pv_ic='" + dt0.Rows[0]["ah_patic"].ToString() + "' AND pv_datevisit='" + toDay + "'";
                                    DataTable sdt = db.Query(sSQL);
                                    if (sdt.Rows.Count > 0)
                                    {
                                        patName += "ST";
                                    }
                                }
                                if (dt2.Rows.Count == 0)
                                {
                                    patName += "(临)";
                                }
                                patname[i] = new BedWeeklySchedule.Patient
                                {
                                    Id = patId,
                                    Name = patName,
                                    IsChanged = true
                                };
                            }
                            else
                            {
                                patname[i] = new BedWeeklySchedule.Patient
                                {
                                    Id = "",
                                    Name = "",
                                    IsChanged = true
                                };
                            }
                        }
                    }
                    #endregion
                    datasource.Add(new BedWeeklySchedule(floor, area, bedno, mac_typ, patname[0], patname[1], patname[2], patname[3], patname[4], patname[5], patname[6], machine, timetype, kind));
                }

                Store store = GridPanel1.GetStore();
                store.DataSource = datasource;
                store.DataBind();
            }
        }

        public class BedWeeklySchedule
        {
            public class Patient {
                public string Id { get; set; }
                public string Name { get; set; }
                public bool IsChanged { get; set; }     // is changed from original appointment_setup
            }

            public BedWeeklySchedule(string floor, string area, string bedNo, string mactype, Patient name1, Patient name2, Patient name3, Patient name4, Patient name5, Patient name6, Patient name7, string machine, string timeType, string kind)
            {
                Floor = floor;
                Area = area;
                Machine = machine;
                BedType = bedNo + " " + mactype;
                BedNo = bedNo;
                MachineType = mactype;
                Week1 = name1;
                Week2 = name2;
                Week3 = name3;
                Week4 = name4;
                Week5 = name5;
                Week6 = name6;
                Week7 = name7;
                TimeType = timeType;
                Kind = kind;
            }

            public string Floor { get; set; }
            public string Area { get;set; }
            public string BedType { get; set; }
            public string BedNo { get;set; }
            public string MachineType { get; set; }
            public Patient Week1 { get; set; }
            public Patient Week2 { get; set; }
            public Patient Week3 { get; set; }
            public Patient Week4 { get; set; }
            public Patient Week5 { get; set; }
            public Patient Week6 { get; set; }
            public Patient Week7 { get; set; }
            public string Machine { get; set; }
            public string TimeType { get; set; }
            public string Kind { get; set; }
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

        protected DateTime GetDateTimeFromDayType(int nDayType) {
            // dayType definition in appointment_setup: [1~7] = [Monday ~ Sunday]
            DateTime dtNow = DateTime.Now;
            int nDayToday = (int)dtNow.DayOfWeek;

            if(nDayToday == 0) {
                nDayToday = 7;  // sunday in DayOfWeek is 0, we use 7 here
            }

            return dtNow.AddDays(nDayType - nDayToday);
        }

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

        protected void DropEnd(object sender, DirectEventArgs e)
        {
            Common._NotificationShow("拖拉");
        }

        protected void cmdSAVE(object sender, DirectEventArgs e) {
            try {
                string SQLT_APPOINTMENT_SEL = @"
                        SELECT apptst_patrefid, apptst_patic, apptst_daytyp FROM appointment_setup
                        WHERE apptst_flr='{0}' AND apptst_sec='{1}' AND apptst_bed='{2}' AND apptst_timetyp='{3}' and apptst_daytyp='{4}';";
                string SQLT_APPOINTMENT_CHANGE_SEL = @"SELECT * FROM appointment_change
                                WHERE ah_date='{0}' AND ah_flr='{1}' AND ah_sec='{2}' AND ah_bed='{3}' AND ah_timetyp='{4}';";
                string SQLT_APPOINTMENT_CHANGE_UPDATE = @"UPDATE appointment_change
                                SET ah_patic = '{0}'
                                WHERE ah_date='{1}' AND ah_flr='{2}' AND ah_sec='{3}' AND ah_bed='{4}' AND ah_timetyp='{5}';";
                string SQLT_APPOINTMENT_CHANGE_INSERT = @"INSERT INTO appointment_change (ah_date, ah_patic, ah_flr, ah_sec, ah_bed, ah_mactyp, ah_timetyp, ah_stat, ah_dycnt)
                                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}', 'N', '{7}');";
                ChangeRecords<BedWeeklySchedule> recs = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<BedWeeklySchedule>();

                foreach(BedWeeklySchedule curBed in recs.Updated) {
                    // analysis if patient changed from Monday to Sunday
                    int iweek = GetWeekNo() + 1;
                    for (int i = iweek; i <= 7; i++) //大於或等於今天日期才可以換床
                    {
                        BedWeeklySchedule.Patient TmpPatient = null; 
                        BedWeeklySchedule.Patient newPatient = null;
                        switch (i)
                        {
                            case 1:
                                TmpPatient = curBed.Week1;
                                break;
                            case 2:
                                TmpPatient = curBed.Week2;
                                break;
                            case 3:
                                TmpPatient = curBed.Week3;
                                break;
                            case 4:
                                TmpPatient = curBed.Week4;
                                break;
                            case 5:
                                TmpPatient = curBed.Week5;
                                break;
                            case 6:
                                TmpPatient = curBed.Week6;
                                break;
                            case 7:
                                TmpPatient = curBed.Week7;
                                break;
                            default:
                                break;
                        }

                        if (TmpPatient.Name.Contains("(s)") == false)
                        {
                            newPatient = TmpPatient;
                            // original patient state before saving(combine patient in appointment_setup & appointment_change
                            // patient in appointment_setup table
                            string oldPatientId = null;
                            DateTime dtCurAnalysis = GetDateTimeFromDayType(i);
                            string sql = string.Format(SQLT_APPOINTMENT_SEL,
                                curBed.Floor,
                                curBed.Area,
                                curBed.BedNo,
                                curBed.TimeType,
                                i.ToString()
                            );
                            DataTable dtAppointSetup = db.Query(sql);

                            // patient in appointment_change table
                            string dateStringChange = GetDateTimeFromDayType(i).ToString("yyyy-MM-dd");
                            string sqlChange = string.Format(SQLT_APPOINTMENT_CHANGE_SEL,
                                    dateStringChange,
                                    curBed.Floor,
                                    curBed.Area,
                                    curBed.BedNo,
                                    curBed.TimeType
                                    );
                            DataTable dtChange = db.Query(sqlChange);

                            // get real patient state here. (the state before saving)
                            DataRow drChg = null;
                            if (dtChange.Rows.Count > 0)
                            {
                                drChg = dtChange.Rows[0];
                                oldPatientId = drChg["ah_patic"].ToString();
                            }
                            else
                            {
                                if (dtAppointSetup.Rows.Count > 0)
                                {
                                    oldPatientId = dtAppointSetup.Rows[0]["apptst_patic"].ToString();
                                }
                                else
                                {
                                    oldPatientId = "";  // no patient
                                }

                            }

                            // patient changed
                            if (newPatient.Id != oldPatientId)
                            {
                                // we already have record in the appointment change table
                                if (drChg != null)
                                {
                                    string sqlUpdate = string.Format(SQLT_APPOINTMENT_CHANGE_UPDATE,
                                            newPatient.Id,
                                            dateStringChange,
                                            curBed.Floor,
                                            curBed.Area,
                                            curBed.BedNo,
                                            curBed.TimeType);

                                    db.Excute(sqlUpdate);
                                }
                                else
                                {
                                    // not exist in appoint_change table, insert it
                                    string sqlInsert = string.Format(SQLT_APPOINTMENT_CHANGE_INSERT,
                                        dateStringChange,
                                        newPatient.Id,
                                        curBed.Floor,
                                        curBed.Area,
                                        curBed.BedNo,
                                        curBed.MachineType,
                                        curBed.TimeType,
                                        i.ToString());

                                    db.Excute(sqlInsert);
                                }
                            }
                        }
                    }
                }

                Load_Appointment();
            } catch (Exception ex) {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }

        protected void btnReset_Click(object sender, DirectEventArgs e)
        {
            Load_Appointment();
        }

        protected void btnEdit_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("SchTableEdit.aspx");
        }

        protected void OnbtnPrint_Click(object sender, DirectEventArgs e)
        {
            string floor, timetype, area;

            floor = Common.GetComboBoxValue(cboFLOOR);
            if (floor == "")
                floor = sFLOOR.Text;

            area = Common.GetComboBoxValue(cboArea);
            if (area == "")
                area = sAREA.Text;

            timetype = Common.GetComboBoxValue(cboTIME);
            if (timetype == "")
                timetype = sTIME.Text;
            else
                sTIME.Text = timetype;
            
            PrintWindow.Show();
            PrintWindow.Loader.SuspendScripting();
            PrintWindow.Loader.Url = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=weeksch" + "&_REPORT_sQM=" + sFLOOR.Text + "&_REPORT_P=" + timetype;
            PrintWindow.Loader.DisableCaching = true;
            PrintWindow.LoadContent();
        }
    }
}