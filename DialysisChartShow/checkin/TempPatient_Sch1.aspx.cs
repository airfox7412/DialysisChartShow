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
    public partial class TempPatient_Sch1 : BaseForm
    {
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (Session["PAT_NAME"] != null)
                {
                    txt_name.Text = Session["PAT_NAME"].ToString();
                    pat_ic.Text = Session["PAT_IC"].ToString();
                }
                //else
                //{
                //    Common._ErrorMsgShow("请先选择一位病患，才能进行排班！");
                //}
                GetWeek();
                Show_Floor();
                Show_Area();
                Show_TimeSec();
                Load_Appointment();
            }
        }

        protected void SetPatient(object sender, DirectEventArgs e)
        {
            String sSQL = "SELECT pif_id, pif_ic, pif_name, pif_sex, pif_docname FROM pat_info ";
            sSQL += "WHERE (1=1) AND ";
            if (txt_name.Text != "")
            {
                sSQL += "pif_name='" + txt_name.Text + "' ";
            }
            else if (txt_ic.Text != "")
            {
                sSQL += "pif_ic='" + txt_ic.Text + "' ";
            }

            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                _PAT_IC = dt.Rows[0]["pif_ic"].ToString();
                _PIF_NAME = dt.Rows[0]["pif_name"].ToString();
                _USER_NAME = dt.Rows[0]["pif_docname"].ToString();
                _PIF_SX = dt.Rows[0]["pif_sex"].ToString() + "性";
                _PAT_ID = dt.Rows[0]["pif_id"].ToString();
                Session["PAT_NAME"] = _PIF_NAME;
                Session["PAT_IC"] = _PAT_IC;
            }
        }
        protected void Load_Appointment()
        {
            var datasource = new List<Project>();
            string floor, timetype, area, bedno, mac_typ, machine;

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
                    //int j = GetWeekNo();
                    DateTime thisDay;
                    for (i = 0; i < 7; i++)
                    {
                        thisDay = DateTime.Now.AddDays(Convert.ToDouble(1 - Convert.ToInt16(DateTime.Now.DayOfWeek)) + i);
                        sql = "SELECT a.*, b.pif_name FROM appointment_change a ";
                        sql += "LEFT JOIN pat_info b ON a.ah_patic=b.pif_ic ";
                        sql += "WHERE a.ah_date='" + thisDay.ToString("yyyy-MM-dd") + "' AND a.ah_flr='" + floor + "' AND a.ah_sec='" + area + "' AND a.ah_bed='" + bedno + "' AND a.ah_timetyp='" + timetype + "' ";
                        DataTable dt0 = db.Query(sql);
                        if (dt0.Rows.Count > 0)
                            patname[i] = dt0.Rows[0]["pif_name"].ToString();
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

        protected void AddBooking0(object sender, DirectEventArgs e)
        {
            PatInfo_Query();
        }

        protected void AddBooking(object sender, DirectEventArgs e)
        {
            if (Session["PAT_NAME"] == null)
            {
                Common._ErrorMsgShow("请先选择一位病患，才能进行排班！");
                txt_name.Focus(true, 300);
            }
            else
            {
                string area = e.ExtraParams["Area"].ToString().Replace("区", "");
                string btype = e.ExtraParams["BedType"].ToString();
                string week = e.ExtraParams["Week"].ToString();
                string patic = Session["PAT_IC"].ToString();
                Char delimiter = ' ';
                string[] bedno = btype.Split(delimiter);

                string[] chweek = new string[7] { "周一", "周二", "周三", "周四", "周五", "周六", "周日" }; //取得中文星期
                int iweek = Convert.ToInt16(week);
                int thisweek = Convert.ToInt16(sWEEK.Text);
                DateTime thisDay = DateTime.Now.AddDays(Convert.ToDouble(0 - Convert.ToInt16(DateTime.Now.DayOfWeek)) + iweek);
                toDay = thisDay.ToString("yyyy-MM-dd");
                if (iweek >= thisweek)
                {
                    string sql1 = "SELECT apptst_id FROM appointment_setup ";
                    sql1 += "WHERE apptst_patic='" + patic + "' AND apptst_daytyp='" + week + "' ";
                    DataTable dt1 = db.Query(sql1);

                    string sql2 = "SELECT ah_id FROM appointment_change ";
                    sql2 += "WHERE ah_patic='" + patic + "' AND ah_date='" + toDay + "' ";
                    DataTable dt2 = db.Query(sql2);

                    if (dt1.Rows.Count == 0 && dt2.Rows.Count == 0)
                    {
                        sql2 = "SELECT ah_id FROM appointment_change ";
                        sql2 += "WHERE ah_patic='' AND ah_date='" + toDay + "' ";
                        sql2 += "AND ah_flr='" + cboFLOOR.Text + "' ";
                        sql2 += "AND ah_sec='" + area + "' ";
                        sql2 += "AND ah_bed='" + bedno[0] + "' ";
                        sql2 += "AND ah_timetyp='" + sTIME.Text + "' ";
                        sql2 += "AND ah_dycnt='" + week + "' ";
                        dt2 = db.Query(sql2);
                        if (dt2.Rows.Count > 0)
                        {
                            try
                            {
                                string sSQL = "UPDATE appointment_change SET ah_patic='" + patic + "' ";
                                sSQL += "WHERE ah_patic='' AND ah_date='" + toDay + "' ";
                                sSQL += "AND ah_flr='" + cboFLOOR.Text + "' ";
                                sSQL += "AND ah_sec='" + area + "' ";
                                sSQL += "AND ah_bed='" + bedno[0] + "' ";
                                sSQL += "AND ah_timetyp='" + sTIME.Text + "' ";
                                sSQL += "AND ah_dycnt='" + week + "' ";
                                db.Excute(sSQL);
                                _ErrorMsgShow(Session["PAT_NAME"].ToString() + "已經排入<br>" + area + "区, " + bedno[0] + "床");
                                Load_Appointment();
                            }
                            catch (Exception ex)
                            {
                                _NotificationShow("临时排班失败!");
                            }
                        }
                        else
                        {
                            try
                            {
                                string sSQL = "INSERT INTO appointment_change (ah_date,ah_patic,ah_flr,ah_sec,ah_bed,ah_mactyp,ah_timetyp,ah_stat,ah_dycnt) ";
                                sSQL += "VALUES('" + toDay + "','";
                                sSQL += patic + "','";
                                sSQL += sFLOOR.Text + "','";
                                sSQL += area + "','";
                                sSQL += bedno[0] + "','";
                                sSQL += bedno[1] + "','";
                                sSQL += sTIME.Text + "','N','";
                                sSQL += week + "')";
                                db.Excute(sSQL);
                                _ErrorMsgShow(Session["PAT_NAME"].ToString() + "已經排入<br>" + area + "区, " + bedno[0] + "床");
                                Load_Appointment();
                            }
                            catch (Exception ex)
                            {
                                _NotificationShow("临时排班失败!");
                            }
                        }
                    }
                    else
                    {
                        _NotificationShow("病患 " + Session["PAT_NAME"].ToString() + "</br>" + toDay + "</br>(" + chweek[iweek-1] + ")已经有排班!");
                    }
                }
                else
                {
                    _NotificationShow("今日之前不能进行排班!");
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

        protected void btnReset_Click(object sender, DirectEventArgs e)
        {
            Load_Appointment();
        }

        #region Window1 历史病患
        protected void btn_Query_Click(object sender, DirectEventArgs e)
        {
            PatInfo_Query();
        }
        #endregion

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
                _PAT_ID = Session["PAT_ID"].ToString();

                Store istore = GridList.GetStore();
                istore.RemoveAll();
                Window1.Hide();
                Load_Appointment();
            }
        }
        #endregion

        #region 查詢病患
        protected void PatInfo_Query()
        {
            string PATIC = SearchID.Text;
            string PATNAME = SearchName.Text;
            string sql;
            sql = " SELECT pif_id, pif_name, if(pif_sex = 'M','男','女') as sex, pif_dob, pif_ic, pif_docname FROM pat_info ";
            sql += "WHERE 1=1 ";
            if (!string.IsNullOrEmpty(PATNAME)) //姓名篩選
                sql += "AND pif_name like '%" + PATNAME + "%' ";
            if (!string.IsNullOrEmpty(PATIC)) //身分證號篩選
                sql += "AND pif_ic like '%" + PATIC + "%' ";
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
    }
}