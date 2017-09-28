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
    public partial class SchTableEdit : BaseForm
    {
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
                        patname[i] = new BedWeeklySchedule.Patient
                        {
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
        
        #region Window1 历史病患
        protected void btn_Query_Click(object sender, DirectEventArgs e)
        {
            PatInfo_Query();
        }
        #endregion

        #region 查詢病患
        protected void PatInfo_Query()
        {
            string PATIC = SearchID.Text;
            string PATNAME = SearchName.Text;
            string sql;
            sql = " SELECT p.pif_id, p.pif_name, if(p.pif_sex = 'M','男','女') as sex, p.pif_dob, p.pif_ic, p.pif_docname, ";
            sql += "if((SELECT count(*) FROM blood_group bg WHERE bg.bgrp_patic=p.pif_ic AND (bg.bgrp_aids='Y' OR bg.bgrp_syphilis='Y' OR bg.bgrp_hbv='Y' OR bg.bgrp_hcv='Y' OR bg.bgrp_diabetic='Y'))=1,'Y','N') AS pif_kind ";
            sql += "FROM pat_info p ";
            sql += "WHERE 1=1 ";
            if (!string.IsNullOrEmpty(PATNAME)) //姓名篩選
                sql += "AND p.pif_name like '%" + PATNAME + "%' ";
            if (!string.IsNullOrEmpty(PATIC)) //身分證號篩選
                sql += "AND p.pif_ic like '%" + PATIC + "%' ";
            sql += "ORDER BY pif_id ";

            DataTable dt = db.Query(sql);
            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
            Window1.Show();
        }
        #endregion
        
        protected void Win_Close(object sender, DirectEventArgs e)
        {
            Window1.Hide();
        }

        #region Window1 選擇病患
        protected void Dialysis_detail(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);
            _PAT_IC = selRow[0]["pat_ic"].ToString();
            _PIF_NAME = selRow[0]["pif_name"].ToString();
            _USER_NAME = selRow[0]["pif_docname"].ToString();
            _PIF_SX = selRow[0]["pif_sex"].ToString() + "性";

            string sql = "SELECT pif_id, pif_name, pif_docname FROM pat_info ";
            sql += " where pif_ic = '" + _PAT_IC + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pif_docname"].ToString() != null)
                    _PatDocName = dt.Rows[0]["pif_docname"].ToString();
                else
                    _PatDocName = "";
                Session["PAT_ID"] = dt.Rows[0]["pif_id"].ToString();
                Session["PAT_NAME"] = dt.Rows[0]["pif_name"].ToString();
                _PAT_ID = Session["PAT_ID"].ToString();

                Store istore = GridList.GetStore();
                istore.RemoveAll();
                Window1.Hide();
                AddBooking();
            }
        }
        #endregion

        protected void DeletePatient(object sender, DirectEventArgs e)            
        {
            string commandName = e.ExtraParams["command"];
            Session["area"] = e.ExtraParams["Area"].ToString().Replace("区", "");
            Session["btype"] = e.ExtraParams["BedType"].ToString();
            Session["week"] = e.ExtraParams["Week"].ToString();
            string PatientId = e.ExtraParams["PatientId"].ToString();
            if (int.Parse(Session["week"].ToString()) < int.Parse(sWEEK.Text))
            {
                return;
            }
            if (commandName == "Add" && PatientId == "")
            {
                PatInfo_Query();
            }
            else if (commandName == "Delete" && PatientId != "")
            {
                string area = Session["area"].ToString();
                string btype = Session["btype"].ToString();
                string week = Session["week"].ToString();

                int iweek = Convert.ToInt16(week);

                int iweek2 = Convert.ToInt16(DateTime.Now.DayOfWeek);
                if (iweek2 == 0) iweek2 = 7;
                DateTime thisDay = DateTime.Now.AddDays(Convert.ToDouble(0 - iweek2) + iweek);
                toDay = thisDay.ToString("yyyy-MM-dd");
                string sql0 = "SELECT pv_macstat FROM pat_visit ";
                sql0 += "WHERE pv_ic='" + PatientId + "' AND pv_datevisit='" + toDay + "' ";
                DataTable dt0 = db.Query(sql0);
                if (dt0.Rows.Count > 0) //若已開機則無法刪除
                {
                    if (dt0.Rows[0]["pv_macstat"].ToString() == "A") //若已開機則無法刪除 
                    {
                        Common._NotificationShow("已开机不能删除!");
                        return;
                    }
                }
                try
                {
                    string sql1 = "SELECT * FROM appointment_change ";
                    sql1 += "WHERE ah_patic='" + PatientId + "' AND ah_date='" + toDay + "' ";
                    DataTable dt1 = db.Query(sql1);
                    if (dt1.Rows.Count > 0)
                    {
                        sql1 = "UPDATE appointment_change SET ah_patic='' ";
                        sql1 += "WHERE ah_patic='" + PatientId + "' AND ah_date='" + toDay + "' ";
                    }
                    else
                    {
                        Char delimiter = ' ';
                        string[] bedno = btype.Split(delimiter);

                        sql1 = "INSERT INTO appointment_change ";
                        sql1 += "SET ah_date='" + toDay + "',";
                        sql1 += "ah_patic='',";
                        sql1 += "ah_flr='" + sFLOOR.Text + "',";
                        sql1 += "ah_sec='" + area + "',";
                        sql1 += "ah_bed='" + bedno[0] + "',";
                        sql1 += "ah_mactyp='" + bedno[1] + "',";
                        sql1 += "ah_timetyp='" + sTIME.Text + "',";
                        sql1 += "ah_stat='N',";
                        sql1 += "ah_dycnt='" + week + "' ";
                    }
                    db.Excute(sql1);
                    Common._NotificationShow("删除成功");
                    Load_Appointment();
                }
                catch (Exception ex)
                {
                    Common._ErrorMsgShow("删除失败:" + ex.Message.ToString());
                }
            }
        }

        protected void AddBooking()
        {
            Boolean insFlag = false;
            string area = Session["area"].ToString();
            string btype = Session["btype"].ToString();
            string week = Session["week"].ToString();
            Char delimiter = ' ';
            string[] bedno = btype.Split(delimiter);

            string[] chweek = new string[7] { "周一", "周二", "周三", "周四", "周五", "周六", "周日" }; //取得中文星期
            int iweek = Convert.ToInt16(week);
            int thisweek = Convert.ToInt16(sWEEK.Text);

            int iweek2 = Convert.ToInt16(DateTime.Now.DayOfWeek);
            if (iweek2 == 0) iweek2 = 7;
            DateTime thisDay = DateTime.Now.AddDays(Convert.ToDouble(0 - iweek2) + iweek);
            toDay = thisDay.ToString("yyyy-MM-dd");
            if (iweek < thisweek)
            {
                _NotificationShow("今日之前不能进行排班!");
                return;
            }
            else
            {
                insFlag = true;
                string sql1 = "SELECT * FROM appointment_setup ";
                sql1 += "WHERE apptst_patic='" + _PAT_IC + "' AND apptst_daytyp='" + week + "'";
                DataTable dt1 = db.Query(sql1);
                if (dt1.Rows.Count > 0)
                {
                    string sql2 = "SELECT * FROM appointment_change ";
                    //sql2 += "WHERE ah_patic='' AND ah_date='" + toDay + "' ";
                    sql2 += "WHERE ah_date='" + toDay + "' ";
                    sql2 += "AND ah_flr='" + dt1.Rows[0]["apptst_flr"].ToString() + "' ";
                    sql2 += "AND ah_sec='" + dt1.Rows[0]["apptst_sec"].ToString() + "' ";
                    sql2 += "AND ah_bed='" + dt1.Rows[0]["apptst_bed"].ToString() + "' ";
                    sql2 += "AND ah_timetyp='" + dt1.Rows[0]["apptst_timetyp"].ToString() + "' ";
                    DataTable dt2 = db.Query(sql2);
                    if (dt2.Rows.Count == 0) //判斷床位是否已刪除
                    {
                        insFlag = false;
                    }
                }
                
                string sql3 = "SELECT * FROM appointment_change ";
                sql3 += "WHERE ah_patic='" + _PAT_IC + "' AND ah_date='" + toDay + "' ";
                DataTable dt3 = db.Query(sql3);
                if (dt3.Rows.Count > 0)
                {
                    insFlag = false;
                }
            }

            if (insFlag == false)
            {
                _ErrorMsgShow("病患 " + Session["PAT_NAME"].ToString() + "</br>" + toDay + "</br>(" + chweek[iweek - 1] + ")已经有排班!");
                return;
            }
            else
            {
                string sql3 = "SELECT ah_id FROM appointment_change ";
                sql3 += "WHERE ah_date='" + toDay + "' ";
                sql3 += "AND ah_flr='" + cboFLOOR.Text + "' ";
                sql3 += "AND ah_sec='" + area + "' ";
                sql3 += "AND ah_bed='" + bedno[0] + "' ";
                sql3 += "AND ah_timetyp='" + sTIME.Text + "' ";
                sql3 += "AND ah_dycnt='" + week + "' ";
                DataTable dt3 = db.Query(sql3);
                if (dt3.Rows.Count > 0)
                {
                    try
                    {
                        string sSQL = "UPDATE appointment_change SET ah_patic='" + _PAT_IC + "' ";
                        sSQL += "WHERE ah_patic='' AND ah_date='" + toDay + "' ";
                        sSQL += "AND ah_flr='" + cboFLOOR.Text + "' ";
                        sSQL += "AND ah_sec='" + area + "' ";
                        sSQL += "AND ah_bed='" + bedno[0] + "' ";
                        sSQL += "AND ah_timetyp='" + sTIME.Text + "' ";
                        sSQL += "AND ah_dycnt='" + week + "' ";
                        db.Excute(sSQL);
                        _NotificationShow(Session["PAT_NAME"].ToString() + "已經排入<br>" + area + "区, " + bedno[0] + "床");
                        Load_Appointment();
                    }
                    catch (Exception ex)
                    {
                        _ErrorMsgShow("临时排班失败!");
                    }
                }
                else
                {
                    try
                    {
                        string sSQL = "INSERT INTO appointment_change (ah_date,ah_patic,ah_flr,ah_sec,ah_bed,ah_mactyp,ah_timetyp,ah_stat,ah_dycnt) ";
                        sSQL += "VALUES('" + toDay + "','";
                        sSQL += _PAT_IC + "','";
                        sSQL += sFLOOR.Text + "','";
                        sSQL += area + "','";
                        sSQL += bedno[0] + "','";
                        sSQL += bedno[1] + "','";
                        sSQL += sTIME.Text + "','N','";
                        sSQL += week + "')";
                        db.Excute(sSQL);
                        _NotificationShow(Session["PAT_NAME"].ToString() + "已經排入<br>" + area + "区, " + bedno[0] + "床");
                        Load_Appointment();
                    }
                    catch (Exception ex)
                    {
                        _ErrorMsgShow("临时排班失败!");
                    }
                }
            }
        }

        protected new void _ErrorMsgShow(string msg)
        {
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "信息",
                Message = "<font size='4'>" + msg + "</font>",
                Icon = (MessageBox.Icon)Enum.Parse(typeof(MessageBox.Icon), MessageBox.Icon.INFO.ToString()),
                Buttons = MessageBox.Button.OK
            });
        }

        protected new void _NotificationShow(string msg)
        {
            Notification.Show(new NotificationConfig
            {
                Title = "系统信息",
                Icon = Ext.Net.Icon.Accept,
                AlignCfg = new NotificationAlignConfig
                {
                    OffsetX = 0,
                    OffsetY = -100
                },
                Html = "<font size='4'>" + msg + "</font>"
            });
        }

        protected void btnBack_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("SchTable.aspx");
        }
    }
}