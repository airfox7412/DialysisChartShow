using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using Ext.Net;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

namespace Dialysis_Chart_Show.Information
{
    public partial class Pat_info_list : BaseForm
    {
        string helpwiz = ConfigurationManager.AppSettings["helpwiz"].ToString();

        #region 使用当前病患，測試用
        protected void btn_Query1_Click(object sender, DirectEventArgs e)
        {
            string hospitalName = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
            if (Text_Name.Text == "ii")
                X.Redirect(ConfigurationManager.AppSettings["iPAD"].ToString());
            if (Text_Name.Text == "dd")
            {
                if (hospitalName.Trim().Equals("Hospital_Henan"))
                    X.Redirect("../Henan/index.aspx");
                else if (hospitalName.Trim().Equals("Hospital_Xian"))
                    X.Redirect("../Xian/index.aspx");
                else
                    X.Redirect("../Henan/index.aspx");
            }
            else
            {
                show_now();
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //this.txtURL.Text = Request.RawUrl;
                //this.txtURL.Hidden = true;
                show_history();
            }
        }

        #region 历史病患
        protected void btn_Query_Click(object sender, DirectEventArgs e)
        {
            show_history();
        }

        protected void show_history()
        {
            string sql;
            DateTime datetime = DateTime.Now;
            sql = " SELECT a.pif_id, a.pif_name, if (a.pif_sex = 'M','男','女') as sex, a.pif_dob, a.pif_ic,";
            sql += " if (b.next_visit_date > '','腹透','血透') as txt_10, ";
            sql += " e.diadate AS FirstDate, '' AS InfoDate, ";
            sql += " if (g.BI > 0,'V',' ') as txt_101,";
            //sql += "'' as txt_101,";
            sql += " case f.opt_1 when '1' then '退出' when '2' then '肾移植' when '3' then '转出' when '4' then '死亡' when '5' then '转入' else '' end AS opt_52,";
            sql += " f.info_date,' ',a.pif_docname,c.info_survey_date,'ignore' ";
            sql += "FROM pat_info a ";
            sql += "LEFT JOIN zinfo_maim c ON a.pif_id=c.pat_id ";
            sql += "LEFT JOIN (SELECT cln1_patic, MAX(cln1_diadate) AS diadate FROM clinical1_nurse group by cln1_patic) e  ON a.pif_ic = e.cln1_patic ";
            sql += "LEFT JOIN zinfo_a_07  f  ON a.pif_id = f.pat_id ";
            sql += "LEFT JOIN (SELECT pat_id, MAX(dat_3) AS next_visit_date FROM zinfo_p_06 group by pat_id) b ON a.pif_id=b.pat_id ";
            sql += "LEFT JOIN BI_SUM_View g ON a.pif_id = g.pat_no ";
            sql += "WHERE 1=1";
            if (!string.IsNullOrEmpty(Text_Name.Text)) //姓名篩選
                sql += " and a.pif_name like '%" + Text_Name.Text + "%'";
            if (!string.IsNullOrEmpty(Cbo_Gender.Text)) //姓別篩選
                sql += " and a.pif_sex ='" + GetComboBoxValue(Cbo_Gender) + "'";
            if (!string.IsNullOrEmpty(Text_ID.Text)) //身分證號篩選
                if (Text_ID.Text.Substring(0, 1) != "#")
                    sql += " and a.pif_ic like '%" + Text_ID.Text + "%'";
                else
                    sql += " and a.pif_id='" + Text_ID.Text.Substring(1) + "'";
            sql += " ORDER BY a.pif_id ";

            DataTable dt = db.Query(sql);
            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }
        #endregion

        #region 當前病患篩選
        protected void show_now() // 沒有_PAT_ID的時候, 用這個函式來顯示頁面(不論是歷史病患或是當前病患)
        {
            DateTime datetime = DateTime.Now;
            int iOrn = 0;
            string weekType = (0 == (iOrn = datetime.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString();
            String date = datetime.ToString("yyyy-MM-dd");
            String sql = "SELECT p.pif_id , P.pif_name AS PERSON_NAME, if (P.pif_sex = 'M','男','女') as sex, P.pif_dob, P.pif_ic, ";            
            sql += " if (b.next_visit_date > '','腹透','血透') as txt_10,";            
            sql += " b.next_visit_date,";
            sql += " e.dat_9,";
            sql += " e.info_date as info_date1,";            
            sql += " if (g.BI > 0,'V',' ') as txt_101,";            
            sql += " case f.opt_1 ";
            sql += " when '1' then '退出' ";
            sql += " when '2' then '移植' ";
            sql += " when '3' then '转出' ";
            sql += " when '4' then '死亡'";
            sql += " when '5' then '转入' ";
            sql += " else '' ";
            sql += " end AS opt_52,";
            sql += " f.info_date, ' ', p.pif_docname,";
            sql += " V.pv_weight AS PERSON_WEIGHT,";
            sql += " V.pv_macstat AS PERSON_STATE ";
            sql += " FROM mac_setup M";
            sql += " LEFT JOIN appointment_setup A ON M.mac_flr  = A.apptst_flr ";
            sql += " AND M.mac_sec = A.apptst_sec AND M.mac_bedno = A.apptst_bed AND A.apptst_daytyp='" + weekType + "'";
            sql += " LEFT JOIN pat_visit         V ON A.apptst_patic=V.pv_ic      AND V.pv_datevisit='" + date + "'";
            sql += " LEFT JOIN pat_info          P ON A.apptst_patic=P.pif_ic ";
            sql += " LEFT JOIN zinfo_f_012       e ON P.pif_id = e.pat_id ";
            sql += " LEFT JOIN zinfo_a_07        f ON P.pif_id = f.pat_id ";
            sql += " LEFT JOIN (SELECT pat_id, MAX(dat_3) AS next_visit_date FROM zinfo_p_06 group by pat_id) b ON P.pif_id=b.pat_id";
            sql += " LEFT JOIN BI_SUM_View       g ON P.pif_id = g.pat_no";
            sql += " WHERE P.pif_ic IS NOT NULL";
            sql += " ORDER BY A.apptst_timetyp, M.mac_flr, M.mac_bedno";
            //this.CommandColumn1.Text = "填寫信息";
            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
            istore.DataBind();
        }
        #endregion

        #region 轉換星期 1 ~ 7: 一周天數
        static String getWeekType()
        {
            string weekType = "";
            switch (DateTime.Now.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    weekType = "1";
                    break;
                case DayOfWeek.Tuesday:
                    weekType = "2";
                    break;
                case DayOfWeek.Wednesday:
                    weekType = "3";
                    break;
                case DayOfWeek.Thursday:
                    weekType = "4";
                    break;
                case DayOfWeek.Friday:
                    weekType = "5";
                    break;
                case DayOfWeek.Saturday:
                    weekType = "6";
                    break;
                case DayOfWeek.Sunday:
                    weekType = "7";
                    break;
                default:
                    throw new Exception("getWeekType failure");
            }
            return weekType;
        }
        #endregion

        #region 到統計report
        protected void btn_Statistics_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("../report/Rpt_View_Dialysis.aspx");
        }
        #endregion

        #region 刪除病患資料
        protected void Delete(object sender, DirectEventArgs e)
        {
            _PAT_ID = e.ExtraParams["pat_id"].ToString();
            string sql = "DELETE FROM pat_info WHERE pif_id='" + _PAT_ID + "' ";
            db.Excute(sql);
            _NotificationShow("讯息删除完成。");
            btn_Query_Click(sender, e);
        }
        #endregion

        #region 修改病患資料
        protected void NoteEdit(object sender, DirectEventArgs e)
        {
            _PAT_ID = e.ExtraParams["pat_id"].ToString();
            X.Redirect("PatEdit.aspx");
        }
        #endregion

        #region 超过或低于标准参考值
        protected void BioIndicators(object sender, DirectEventArgs e)
        {
            Session["PAT_ID"] = e.ExtraParams["pat_id"].ToString();
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Biochemical_Indicators_list.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        #endregion

        #region 當月使用藥品耗材查詢
        protected void Dialysis_13(object sender, DirectEventArgs e)
        {
            _PAT_IC = e.ExtraParams["pat_ic"].ToString();
            _PIF_NAME = e.ExtraParams["pif_name"].ToString();
            string USER_ID = Session["USER_ID"].ToString();
            string usertype = Session["USER_RIGHT"].ToString();
            Window4.Show();
            Window4.Loader.SuspendScripting();
            Window4.Loader.Url = "Dialysis_13_new.aspx?PAT_IC=" + _PAT_IC + "&PAT_NAME=" + _PIF_NAME + "&USER_ID=" + USER_ID + "&USER_TYPE=" + usertype;
            Window4.Loader.DisableCaching = true;
            Window4.LoadContent();
        }
        #endregion

        #region 臨床小幫手
        protected void Dialysis_help(object sender, DirectEventArgs e)
        {
            if (helpwiz == "true")
            {
                _PAT_ID = e.ExtraParams["pat_ic"].ToString();
                Window2.Show();
                Window2.Loader.SuspendScripting();
                Window2.Loader.Url = "./Dialysis_help.aspx?sel_PAT_NO=" + _PAT_ID;
                Window2.Loader.DisableCaching = true;
                Window2.LoadContent();
            }
            else
            {
                ImageCommand ic = new ImageCommand();
                if (ic.CommandName == "DoctorHelp")
                    ic.Icon = Icon.BinEmpty;
            }
        }
        #endregion

        #region 未检测名单
        protected void ShowBioNotCheckedList(object sender, DirectEventArgs e)
        {
            //Window3.Show();
            //Window3.Loader.SuspendScripting();
            ////Window3.Loader.Url = "./Biochemical_Not_Checked_List.aspx;     // 每月未檢驗的病人統計名單 初版
            //Window3.Loader.Url = "./Biochemical_Not_Checked_List_V2.aspx"; // 每月未檢驗的病人統計名單 二版
            //Window3.Loader.DisableCaching = true;
            //Window3.LoadContent();
        }
        #endregion

        #region [查看信息]
        protected void Dialysis_detail(object sender, DirectEventArgs e)
        {
            _PAT_IC = e.ExtraParams["pat_ic"].ToString();
            //_PIF_NAME = e.ExtraParams["pif_name"].ToString();
            //_USER_NAME = e.ExtraParams["pif_docname"].ToString();
            //_PIF_SX = e.ExtraParams["pif_sex"].ToString() + "性";

            string sql = "SELECT pif_id, pif_name, pif_sex, pif_docname FROM pat_info ";
            sql += " where pif_ic = '" + _PAT_IC + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pif_docname"].ToString() != null)
                    _PatDocName = dt.Rows[0]["pif_docname"].ToString();
                else
                    _PatDocName = "";

                _PIF_NAME = dt.Rows[0]["pif_name"].ToString();
                _PIF_SX = dt.Rows[0]["pif_sex"].ToString();
                if (_PIF_SX == "M")
                    _PIF_SX = "男性";
                else if (_PIF_SX == "F")
                    _PIF_SX = "女性";
                else
                    _PIF_SX = "";
                Session["PAT_ID"] = dt.Rows[0]["pif_id"].ToString();
                _PAT_ID = Session["PAT_ID"].ToString();
            }
        }
        #endregion
    }
}