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

namespace Dialysis_Chart_Show.Information
{
    public partial class Info_index : BaseForm
    {
        private string USER_ID = "";
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string PAT_ID = Request.Form["PAT_ID"];
                if (Request.Form["USER_ID"]!="" && Request.Form["USER_ID"]!=null) {
                  USER_ID = Request.Form["USER_ID"];
                }
                else
                {
                  USER_ID = _Request("USER_ID");
                };
                if (Session["USER_ID"] == null)
                {
                    if (USER_ID != null && USER_ID !="")
                    {
                        Session["PAT_ID"] = PAT_ID;
                        Session["USER_ID"] = USER_ID;
                    }
                    else
                    {
                        Response.Redirect("/myhaisv4/myhaisv4.html");
                    }
                }

                try
                {
                    //DBMysql db = new DBMysql();

                    string sPC_HEAD = "logo001Big.jpg";
                    DataTable dtPC_HEAD = db.Query("SELECT *  FROM general_setup WHERE  genst_code='PC_HEAD'");
                    if (dtPC_HEAD.Rows.Count > 0)
                    {
                        sPC_HEAD = dtPC_HEAD.Rows[0]["genst_desc"].ToString();
                        //sPAD_HEAD = Server.MapPath(sPAD_HEAD);
                        Image1.ImageUrl = sPC_HEAD;
                    }
                }
                catch (Exception ex)
                {
                    _ErrorMsgShow(ex.Message.ToString());
                }

                this.txtURL.Text = Request.RawUrl;
                this.txtURL.Hidden = true;
                string sPAT_ID = "";
                string sUSER_ID = "";

                if (Session["PAT_ID"] != null)
                {
                    sPAT_ID = Session["PAT_ID"].ToString();
                }

                if (Session["USER_ID"] != null)
                {
                    sUSER_ID = Session["USER_ID"].ToString();
                }
                //string sPAT_ID = Request.QueryString["_PAT_ID"] == null ? string.Empty : Request.QueryString["_PAT_ID"].ToString();
                //string sUSER_ID = Request.QueryString["_USER_ID"] == null ? string.Empty : Request.QueryString["_USER_ID"].ToString();
                if (sUSER_ID != "")
                {
                    _UserID = sUSER_ID;
                }
                else
                {
                    if (_UserID == "") _UserID = "test";
                    Response.Redirect("/myhaisv4/myhaisv4.html");
                }
                this.txtUSER.Text = _UserID;
                this.txtUSER.Hidden = true;

                if (sPAT_ID != "")
                {
                    string sSQL;
                    //sSQL = "select a.pif_id, a.pif_name, a.pif_sex, a.pif_dob, a.pif_ic, b.lgord_usr1 as info_user_name, c.info_survey_date ";
                    sSQL = "select a.pif_docname,    a.pif_id, a.pif_name, a.pif_sex, a.pif_dob, a.pif_ic, b.lgord_usr1 as info_user_name, c.info_survey_date ";
                    sSQL += " from pat_info a ";
                    sSQL += " left join longterm_ordermgt b ";
                    sSQL += "   on a.pif_ic=b.lgord_patic ";
                    sSQL += "  and CONCAT(b.lgord_dateord, b.lgord_timeord)= ";
                    sSQL += "           (select max(CONCAT(lgord_dateord, lgord_timeord)) ";
                    sSQL += "              from longterm_ordermgt ";
                    sSQL += "             where lgord_patic=a.pif_ic) ";
                    sSQL += " left join zinfo_maim c ";
                    sSQL += "   on a.pif_id=c.pat_id ";
                    sSQL += "where pif_ic='" + sPAT_ID + "' ";

                    DataTable dt = db.Query(sSQL);
                    if (dt.Rows.Count > 0)
                    {
                        _PAT_ID = dt.Rows[0]["pif_id"].ToString();
                        _PAT_IC = dt.Rows[0]["pif_ic"].ToString();
                        _PIF_NAME = dt.Rows[0]["pif_name"].ToString();
                        _USER_NAME = dt.Rows[0]["info_user_name"].ToString();
                        _PatDocName = dt.Rows[0]["pif_docname"].ToString();
                        //_PIF_SX = "";
                        //Frank 20141231 bug fix _PIF_SX 公用欄位不宜隨意清空
                        if (dt.Rows[0]["pif_sex"].ToString() == "M")
                            _PIF_SX = "男性";
                        if (dt.Rows[0]["pif_sex"].ToString() == "F")
                            _PIF_SX = "女性";

                        Response.Redirect("Dialysis_Info.aspx");
                    }
                    else
                    {
                        show1();
                    }
                }
                else
                {
                    show1();
                }
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_House_click(object sender, EventArgs e)
        {
            Response.Redirect(ConfigurationManager.AppSettings["hose"].ToString());
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void MyData_Refresh(object sender, StoreReadDataEventArgs e)
        {
            show();
        }

        /// <summary>
        /// 有透析
        /// </summary>
        protected void show_err2()
        {
            string sql;
            sql = "select a.pif_id, a.pif_name, a.pif_sex, a.pif_dob, a.pif_ic, ";
            sql += "'' AS lgord_usr1, '' AS info_survey_date";
            sql += " from pat_info a ";
            sql += "where (a.pif_sex not in ('M','F') or length(a.pif_dob)<>10 or length(REPLACE(a.pif_dob, '/', ''))<>8) ";
            sql += "  and a.pif_ic in (SELECT DISTINCT cln1_patic FROM clinical1_nurse) ";
            sql += "order by a.pif_id ";
            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
            istore.DataBind();
        }

        /// <summary>
        /// 有資料
        /// </summary>
        protected void show_err1()
        {
            string sql;
            sql = "select a.pif_id, a.pif_name, a.pif_sex, a.pif_dob, a.pif_ic, ";
            sql += "'' AS lgord_usr1, '' AS info_survey_date";
            sql += " from pat_info a ";
            sql += "where a.pif_sex not in ('M','F') or length(a.pif_dob)<>10 or length(REPLACE(a.pif_dob, '/', ''))<>8 ";
            sql += "order by a.pif_id ";
            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
            istore.DataBind();
        }

        /// <summary>
        /// 
        /// </summary>
        protected void show()
        {
            string sql;
            DateTime datetime = DateTime.Now;
            //String ldate = datetime.AddMonths(-1).ToString("yyyy-MM-dd").Substring(0, 8);
            //string days = DateTime.DaysInMonth(int.Parse(ldate.Substring(0, 4)), int.Parse(ldate.Substring(5, 2))).ToString();

            //string chkmth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");

            //sql = "select DISTINCT a.pif_id ,a.pif_name,a.pif_sex,a.pif_dob,a.pif_ic,b.lgord_usr1,c.info_survey_date";
            //sql += " from pat_info a";
            //sql += " left join longterm_ordermgt b on a.pif_ic=b.lgord_patic ";
            //sql += " and concat(b.lgord_dateord ,b.lgord_timeord) = ";
            //sql += " (select max(CONCAT(lgord_dateord ,lgord_timeord)) from longterm_ordermgt where lgord_patic=a.pif_ic)";
            //sql += " left join zinfo_maim c on a.pif_id=c.pat_id where 1=1";

            //sql = "select DISTINCT a.pif_id ,a.pif_name,a.pif_sex,a.pif_dob,a.pif_ic,b.lgord_usr1,c.info_survey_date,d.info_date";
            //sql += " from pat_info a";
            //sql += " left join longterm_ordermgt b on a.pif_ic=b.lgord_patic ";
            //sql += " and concat(b.lgord_dateord ,b.lgord_timeord) = ";
            //sql += " (select max(CONCAT(lgord_dateord ,lgord_timeord)) from longterm_ordermgt where lgord_patic=a.pif_ic)";
            //sql += " left join zinfo_maim c on a.pif_id=c.pat_id";
            //sql += " left join zinfo_f_012 d on a.pif_id = d.pat_id and SUBSTR(d.info_date,1,7) = '" + chkmth + "' ";
            //sql += " where 1=1";

            //old andy 20150602 
            //sql = "select DISTINCT a.pif_id ,a.pif_name,if (a.pif_sex = 'M','男','女') as sex,a.pif_dob,a.pif_ic,a.pif_docname,c.info_survey_date,d.info_date";
            //sql += " from pat_info a";
            //sql += " left join zinfo_maim c on a.pif_id=c.pat_id";
            //sql += " left join zinfo_e_01 d on a.pif_id = d.pat_id and SUBSTR(d.info_date,1,7) = '" + chkmth + "' ";
            //sql += " where 1=1";

            //new andy 20150602
            sql = " select DISTINCT a.pif_id ,a.pif_name,if (a.pif_sex = 'M','男','女') as sex,a.pif_dob,a.pif_ic,";
            sql += " if (b.next_visit_date > '','腹透','血透') as txt_10,";
            sql += " b.next_visit_date,";
            sql += " e.dat_9,";
            sql += " e.info_date as info_date1,";
            sql += " if (g.BI > 0,'V',' ') as txt_101,";

            sql += " case f.opt_1 ";
            //sql += " case f.opt_52 ";

            sql += " when '1' then '退出'";
            sql += " when '2' then '肾移植'";
            sql += " when '3' then '转出'";
            sql += " when '4' then '死亡'";
            sql += " when '5' then '转入'";
            sql += " else ''";
            sql += " end,";
            //sql += " f.info_date,dchk.NotChecked,a.pif_docname,c.info_survey_date,'ignore'";
            sql += " f.info_date,' ',a.pif_docname,c.info_survey_date,'ignore'";
            sql += " from pat_info a";
            sql += " left join zinfo_maim c on a.pif_id=c.pat_id";
            sql += " left join zinfo_f_012 e  on a.pif_id = e.pat_id";
            sql += " left join zinfo_a_07  f  on a.pif_id = f.pat_id";
            sql += " left join (SELECT pat_id, MAX(dat_3) AS next_visit_date FROM zinfo_p_06 group by pat_id) b on a.pif_id=b.pat_id";
            sql += " left join BI_SUM_View g on a.pif_id = g.pat_no";
            //sql += " left join (SELECT pif_id,SUM(NotChecked) AS NotChecked FROM";
            //sql += " (SELECT pi.pif_id, pi.pif_name, pi.pif_ic, pi.RESULT_CODE, ars.RESULT_VALUE, if (ars.RESULT_VALUE > 0, 0, 1) as NotChecked";
            //sql += " FROM (SELECT pio.pif_id, pio.pif_name, pio.pif_ic, bcl.RESULT_CODE";
            //sql += " FROM pat_info pio";
            //sql += " NATURAL JOIN bio_check_list bcl";
            //sql += " where bcl.I_MONTH = 1) pi";
            //sql += " LEFT JOIN (SELECT  arl.pat_no, arl.result_date, arl.result_code, arl.RESULT_VALUE_T AS RESULT_VALUE";
            //sql += " FROM a_result_log arl";
            //sql += " where arl.RESULT_CODE IN (SELECT RESULT_CODE FROM bio_check_list where I_MONTH = 1)";
            //sql += " and result_date >= '" + ldate + "01' and result_date <= '" + ldate + days + "') ars on  pi.pif_id = ars.pat_no and pi.RESULT_CODE = ars.RESULT_CODE";
            //sql += " ORDER BY pi.pif_id) dc";
            //sql += " GROUP BY pif_id) dchk on a.pif_id = dchk.pif_id";
            sql += " where 1=1";

            if (Text_Name.Text != "")
                sql += " and a.pif_name like '%" + Text_Name.Text + "%'";

            if (Cbo_Gender.SelectedItem != null)
                sql += " and a.pif_sex ='" + GetComboBoxValue(Cbo_Gender) + "'";

            if (Text_ID.Text != "")
                if (Text_ID.Text.Substring(0, 1) != "#")
                    sql += " and a.pif_ic like '%" + Text_ID.Text + "%'";
                else
                    sql += " and a.pif_id='" + Text_ID.Text.Substring(1) + "'";

            sql += " order by a.pif_id ";

            this.CommandColumn1.Text = "查看信息";
            //((GridCommand)this.CommandColumn1.Commands[0]).Text = "查看信息";

            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
            istore.DataBind();
        }

        /// <summary>
        /// 历史病患
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Query_Click(object sender, DirectEventArgs e)
        {
            //20150720 ANDY test S
            DateTime now = DateTime.Now;
            string W_TEST1 = now.ToString("yyyy-MM-dd HH:mm:ss");
            string ii = "";
            if (Text_Name.Text == "TT")
            {
                //ErrorMsgShow("TESTS=" + W_TEST1);
                test();
                //DateTime now2 = DateTime.Now;
                //string W_TEST10 = now2.ToString("yyyy-MM-dd HH:mm:ss");
                //ErrorMsgShow("TESTE=" + W_TEST10);
                return;
            };
            //20150720 ANDY test E


            if (Text_Name.Text == "ii")
                //20150724 移到當前病患  Andy
                //Response.Redirect(ConfigurationManager.AppSettings["iPAD"].ToString());
                ii = "";
            else if (Text_Name.Text == "hh")
                Response.Redirect(ConfigurationManager.AppSettings["hose"].ToString());
            else if (Text_Name.Text == "err1")
                show_err1();
            else if (Text_Name.Text == "err2")
                show_err2();
            else
                show();
        }

        /// <summary>
        /// 当前病患
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Query1_Click(object sender, DirectEventArgs e)
        {
            DateTime now = DateTime.Now;
            string W_TEST1 = now.ToString("yyyy-MM-dd HH:mm:ss");
            if (Text_Name.Text == "ii")
                Response.Redirect(ConfigurationManager.AppSettings["iPAD"].ToString());
            else
            {
                show1();
            }
        }

        /// <summary>
        /// 沒有_PAT_ID的時候, 用這個函式來顯示頁面(不論是歷史病患或是當前病患)
        /// </summary>
        protected void show1()
        {
            //string chkmth = DateTime.Now.AddMonths(-1).ToString("yyyy-MM");
            //String weekType = getWeekType();
            DateTime datetime = DateTime.Now;
            int iOrn = 0;
            string weekType = (0 == (iOrn = datetime.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString(); // 一般程式(Basic,C,Delphi...) 週末指星期六,其實星期日是每週的第一日。
            String date = datetime.ToString("yyyy-MM-dd");
            //String ldate = datetime.AddMonths(-1).ToString("yyyy-MM-dd").Substring(0, 8);
            //string days = DateTime.DaysInMonth(int.Parse(ldate.Substring(0, 4)), int.Parse(ldate.Substring(5, 2))).ToString();

            String sql = "  SELECT p.pif_id ,";
            sql += " P.pif_name AS PERSON_NAME,";
            sql += " if (P.pif_sex = 'M','男','女') as sex,";
            sql += " P.pif_dob, ";
            sql += " P.pif_ic, ";
            //sql += " if (e.txt_10 = '','血透','腹透') as txt_10,"; 
            sql += " if (b.next_visit_date > '','腹透','血透') as txt_10,";
            sql += " b.next_visit_date,";
            sql += " e.dat_9,";
            sql += " e.info_date as info_date1,";
            sql += " if (g.BI > 0,'V',' ') as txt_101,";
            sql += " case f.opt_1  when '1' then '退出' when '2' then '移植' when '3' then '转出' when '4' then '死亡'";
            sql += " when '5' then '转入' else '' end,";
            sql += " f.info_date,";
            sql += " ' ',";
            //sql += " dchk.NotChecked,";
            sql += " p.pif_docname,";
            sql += " V.pv_weight AS PERSON_WEIGHT,";
            sql += " V.pv_macstat AS PERSON_STATE ";
            sql += " FROM mac_setup M LEFT JOIN appointment_setup A ON M.mac_flr  = A.apptst_flr ";
            sql += " AND M.mac_sec = A.apptst_sec AND M.mac_bedno = A.apptst_bed AND A.apptst_daytyp='" + weekType + "'";
            sql += " LEFT JOIN pat_visit         V ON A.apptst_patic=V.pv_ic      AND V.pv_datevisit='" + date + "'";
            sql += " LEFT JOIN pat_info          P ON A.apptst_patic=P.pif_ic ";
            sql += " left join zinfo_f_012       e on P.pif_id = e.pat_id ";
            sql += " left join zinfo_a_07        f on P.pif_id = f.pat_id ";
            sql += " left join (SELECT pat_id, MAX(dat_3) AS next_visit_date FROM zinfo_p_06 group by pat_id) b on P.pif_id=b.pat_id";
            sql += " left join BI_SUM_View       g on P.pif_id = g.pat_no";
            //sql += " left join (SELECT pif_id,SUM(NotChecked) AS NotChecked FROM";
            //sql += " (SELECT pi.pif_id, pi.pif_name, pi.pif_ic, pi.RESULT_CODE, ars.RESULT_VALUE, if (ars.RESULT_VALUE > 0, 0, 1) as NotChecked";
            //sql += " FROM (SELECT pio.pif_id, pio.pif_name, pio.pif_ic, bcl.RESULT_CODE";
            //sql += " FROM pat_info pio";
            //sql += " NATURAL JOIN bio_check_list bcl";
            //sql += " where bcl.I_MONTH = 1) pi";
            //sql += " LEFT JOIN (SELECT  arl.pat_no, arl.result_date, arl.result_code, arl.RESULT_VALUE_T AS RESULT_VALUE";
            //sql += " FROM a_result_log arl";
            //sql += " where arl.RESULT_CODE IN (SELECT RESULT_CODE FROM bio_check_list where I_MONTH = 1)";
            //sql += " and result_date >= '" + ldate + "01' and result_date <= '" + ldate + days + "') ars on  pi.pif_id = ars.pat_no and pi.RESULT_CODE = ars.RESULT_CODE";
            //sql += " ORDER BY pi.pif_id) dc";
            //sql += " GROUP BY pif_id) dchk on p.pif_id = dchk.pif_id";

            this.CommandColumn1.Text = "填寫信息";
            //((GridCommand)this.CommandColumn1.Commands[0]).Text = "查看信息";

            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
            istore.DataBind();
        }

        /// <summary>
        /// 1 ~ 7: 一周天數
        /// </summary>
        /// <returns></returns>
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

        /// <summary>
        /// 
        /// </summary>
        protected void test()
        {
            //1.[血液淨化過程明細clinical2_nurse] 
            //                 淨化時間:cln2_time ex:count:4筆
            // 血液淨化流水號  病人身分證  淨化日期  淨化時間  T**(溫度) P**(心跳) R**(次/分) BP**(血壓) 病情及處理 記錄人 記錄時間
            // 血液淨化流水號	cln2_id
            // 病人身分證	    cln2_patic
            // 淨化日期	        cln2_date
            // 淨化時間	        cln2_time
            // T**(溫度)    	cln2_t            7血液淨化過程明細clinical2_nurse
            // P**(心跳)        cln2_p            8
            // R**(次/分)	    cln2_r            9
            // BP**(血壓)	    cln2_bp          10
            // 病情及處理	    cln2_rmk         11 
            // 記錄人	        cln2_user        12
            // 記錄時間	        cln2_dateadded   13
            string sql = "";
            string tpetime = "";
            string cln2_t = "";
            string cln2_r = "";
            string cln2_p = "";
            string cln2_bp = "";
            string cln2_rmk = "";
            string cln2_user = "";
            string cln2_dateadded = "";
            string cln2_id = "";
            string ldt = "";
            string tchklsttime = "";
            string tcln1ftm = "";
            string lsttime = "";
            DateTime now4 = DateTime.Now;
            string W_TEST4 = now4.ToString("yyyy-MM-dd HH:mm:ss");
            string W_TEST5 = "";
            string W_TEST6 = "";
            int tc2cnt;
            string tcol22 = "";
            string tcol23 = "";
            string tcoldate = "";
            string tcoltime = "";

            sql = "select * from clinical2_nurse where cln2_patic ='" + "32011195707253610'" +
                  " and cln2_date ='" + "2014-12-31'";
            DataTable dt_TEST4 = db.Query(sql);

            //1s
            if (dt_TEST4.Rows.Count > 0)
            {
                //1a-s
                for (int n = 0; n < dt_TEST4.Rows.Count; n++)
                {

                    //2.[淨化過程明細機器資料data_list]  淨化時間:cln2_time
                    //病人身分證號	person_id
                    //樓層   	    floor_no
                    //床號        	bed_no
                    //透析日期	    dialysis_date
                    //透析時間	    dialysis_time
                    //               column_1
                    //已超濾	        column_2       3.淨化過程明細機器資料data_list
                    //               column_3
                    //血流量	        column_4       6.淨化過程明細機器資料data_list
                    //               column_5
                    //溫度	        column_6       2.淨化過程明細機器資料data_list
                    //電導	        column_7       1.淨化過程明細機器資料data_list
                    //靜脈壓	        column_8       5.淨化過程明細機器資料data_list
                    //               column_9
                    //跨膜壓	        column_10      4.淨化過程明細機器資料data_list
                    tpetime = dt_TEST4.Rows[0]["cln2_time"].ToString();//淨化時間

                    sql = "";
                    sql = "select * from data_list where person_id = '32011195707253610' and dialysis_date = '2014-12-31' and dialysis_time = '" +
                          tpetime + "'";
                    DataTable dt_TEST5 = db.Query(sql);

                    //2s 大部分是有筆數的 dt_TEST5.Rows.Count
                    if (dt_TEST5.Rows.Count > 0)
                    {
                        //不顯示
                    }
                    else
                    {
                        //少部分沒有筆數
                        cln2_t = dt_TEST4.Rows[0]["cln2_t"].ToString();               //7    T**(溫度)  :血液淨化過程明細clinical2_nurse
                        cln2_r = dt_TEST4.Rows[0]["cln2_r"].ToString();               //9    R**(次/分) :血液淨化過程明細clinical2_nurse
                        cln2_p = dt_TEST4.Rows[0]["cln2_p"].ToString();               //8    P**(心跳)) :血液淨化過程明細clinical2_nurse
                        cln2_bp = dt_TEST4.Rows[0]["cln2_bp"].ToString();              //10   BP**(血壓) :血液淨化過程明細clinical2_nurse     
                        cln2_rmk = dt_TEST4.Rows[0]["cln2_rmk"].ToString();             //     病情及處理 :血液淨化過程明細clinical2_nurse 
                        cln2_user = dt_TEST4.Rows[0]["cln2_user"].ToString();      //     記錄人     :血液淨化過程明細clinical2_nurse 
                        cln2_dateadded = dt_TEST4.Rows[0]["cln2_dateadded"].ToString(); //     記錄時間   :血液淨化過程明細clinical2_nurse 
                        cln2_id = dt_TEST4.Rows[0]["cln2_id"].ToString();        //     id         :血液淨化過程明細clinical2_nurse 
                    }
                    //2e                   

                }
                //1a-e
                //test
                DateTime now5 = DateTime.Now;
                W_TEST5 = now5.ToString("yyyy-MM-dd HH:mm:ss");
            }//1e

            //3s
            //淨化過程明細機器資料:data_list 
            //                     透析日期:dialysis_date ex:2014-12-31 1筆
            sql = "";
            sql = "select distinct(dialysis_date) from data_list where person_id = '32011195707253610' and dialysis_date = '2014-12-31' order by dialysis_date DESC limit 1";
            DataTable dt_TEST = db.Query(sql);
            if (dt_TEST.Rows.Count > 0)
            {
                ldt = dt_TEST.Rows[0]["dialysis_date"].ToString();//淨化過程明細機器資料 透析日期dialysis_date
                //血液淨化紀錄(clinical1_nurse)  
                //             透析開始時間cln1_col10
                //             透析結束時間cln1_col11 
                //             透析時間	 cln1_col12

                sql = "";
                sql = "select * from clinical1_nurse where cln1_patic = '32011195707253610' and cln1_diadate = '2014-12-31' and cln1_col10 <> ''";
                tchklsttime = "N";
                DataTable dt_TEST6 = db.Query(sql);
                //3a-s
                if (dt_TEST6.Rows.Count > 0)
                {
                    tcln1ftm = dt_TEST6.Rows[0]["cln1_col11"].ToString();//透析結束時間cln1_col11 
                    sql = "";
                    sql = "select * from data_list where person_id = '32011195707253610' and dialysis_date = '2014-12-31' and column_11 <> '' ORDER BY dialysis_time DESC LIMIT 1";
                    DataTable dt_TEST7 = db.Query(sql);

                    //3b-s
                    if (dt_TEST7.Rows.Count > 0)
                    {
                        lsttime = dt_TEST7.Rows[0]["dialysis_time"].ToString();//透析時間

                        //3c-s 透析結束時間cln1_col11 
                        if (tcln1ftm != "")
                        {
                            tchklsttime = "Y";
                        }
                        else
                        {
                            tchklsttime = "N";
                        }
                        //3c-e

                    }
                    else
                    {
                        tchklsttime = "N";
                    }
                    //3b-e

                }
                else
                {
                    tchklsttime = "N";
                }
                //3a-e
                DateTime now6 = DateTime.Now;
                W_TEST6 = now6.ToString("yyyy-MM-dd HH:mm:ss");
                ErrorMsgShow("TESTE 4=" + W_TEST4 + " /TEST5=" + W_TEST5 + "/TEST6=" + W_TEST6);

                //////淨化過程明細機器資料data_list ed:47筆//////
                sql = "";
                sql = "select * from data_list where person_id = '32011195707253610' and dialysis_date = '2014-12-31' and column_11 <> ''";
                DataTable dt_TEST8 = db.Query(sql);
                tc2cnt = 0;

                if (dt_TEST8.Rows.Count > 0)
                {
                    tcol22 = dt_TEST8.Rows[0]["column_22"].ToString();
                    tcol23 = dt_TEST8.Rows[0]["column_23"].ToString();
                    tcoldate = dt_TEST8.Rows[0]["dialysis_date"].ToString();
                    tcoltime = dt_TEST8.Rows[0]["dialysis_time"].ToString();

                    sql = "select * from clinical2_nurse where cln2_patic = '32011195707253610' and cln2_date = '$tcoldate' and cln2_time = 'tcoltime'";
                }

            }
            //3e

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="myMessage"></param>
        public void ErrorMsgShow(string myMessage)
        {
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "错误信息",
                Message = myMessage,
                Width = 300,
                Buttons = MessageBox.Button.OK,
                Closable = false,
                Progress = false
            });
        }

        /// <summary>
        /// 到統計report
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btn_Statistics_Click(object sender, DirectEventArgs e)
        {
            Response.Redirect("../report/Rpt_View_Dialysis.aspx");
        }

        /// <summary>
        /// [查看信息]
        /// 到血液透晰畫面
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Dialysis(object sender, DirectEventArgs e)
        {
            _PAT_ID = e.ExtraParams["pat_id"].ToString();
            _PAT_IC = e.ExtraParams["pat_ic"].ToString();
            _PIF_NAME = e.ExtraParams["pif_name"].ToString();
            _USER_NAME = e.ExtraParams["pif_docname"].ToString();

            //Frank 20141231 前端欄位呈現值已變男女，引數判斷就要變成用男女判斷 測試時抓下現在值是甚麼
            _PIF_SX = e.ExtraParams["pif_sex"].ToString();

            string sql = "SELECT a.pif_name, a.pif_docname ";
            sql += " from pat_info a";
            sql += " where  a.pif_id = '" + _PAT_ID + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows[0]["pif_docname"].ToString() != null)
                _PatDocName = dt.Rows[0]["pif_docname"].ToString();
            else
                _PatDocName = "";
            // _PatDocName = e.ExtraParams["pif_docname"].ToString();

            //Frank 20141231 bug fix _PIF_SX 公用欄位不宜隨意清空
            //_PIF_SX = "";
            //Frank 20141231 前端欄位呈現值已變男女，引數判斷就要變成用男女判斷
            //if (e.ExtraParams["pif_sex"].ToString() == "M")
            if (e.ExtraParams["pif_sex"].ToString() == "男")
            {
                _PIF_SX = "男性";
            }
            //Frank 20141231 前端欄位呈現值已變男女，引數判斷就要變成用男女判斷
            //if (e.ExtraParams["pif_sex"].ToString() == "F")
            if (e.ExtraParams["pif_sex"].ToString() == "女")
            {
                _PIF_SX = "女性";
            }
            Session["PAT_ID"] = _PAT_ID;
            Response.Redirect("Dialysis_Info.aspx");

        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Delete(object sender, DirectEventArgs e)
        {

            _PAT_ID = e.ExtraParams["pat_id"].ToString();
            string sql = "DELETE FROM pat_info WHERE pif_id='" + _PAT_ID + "' ";
            db.Excute(sql);
            _NotificationShow("讯息删除完成。");
            btn_Query_Click(sender, e);

        }

        /// <summary>
        /// 修改病患資料
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void NoteEdit(object sender, DirectEventArgs e)
        {

            _PAT_ID = e.ExtraParams["pat_id"].ToString();
            Response.Redirect("PatEdit.aspx");

        }

        /// <summary>
        /// 超过或低于标准参考值
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void BioIndicators(object sender, DirectEventArgs e)
        {
            _PAT_ID = e.ExtraParams["pat_id"].ToString();
//            TextArea1.Text = e.ExtraParams["pat_id"].ToString();

            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Biochemical_Indicators_list.aspx?sel_PAT_NO=" + _PAT_ID;
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        /// <summary>
        /// 當月使用藥品耗材查詢
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Dialysis_13(object sender, DirectEventArgs e)
        {
            _PAT_ID = e.ExtraParams["pat_ic"].ToString();
            USER_ID = Session["USER_ID"].ToString();
            TextArea1.Visible = true;
//            TextArea1.Text = "USER_ID = " + USER_ID;
            Window2.Width = 600;
            Window2.Height = 400;
            Window2.Show();
            Window2.Loader.SuspendScripting();
            Window2.Loader.Url = "./Dialysis_13.aspx?sel_PAT_NO=" + _PAT_ID + "&USER_ID=" + USER_ID;
            Window2.Loader.DisableCaching = true;
            Window2.LoadContent();
        }

        #region 臨床小幫手
        protected void Dialysis_help(object sender, DirectEventArgs e)
        {
            _PAT_ID = e.ExtraParams["pat_ic"].ToString();
            Window2.Width = 700;
            Window2.Height = 700;
            Window2.Show();
            Window2.Loader.SuspendScripting();
            Window2.Loader.Url = "./Dialysis_help.aspx?sel_PAT_NO=" + _PAT_ID;
            Window2.Loader.DisableCaching = true;
            Window2.LoadContent();
        }
        #endregion

        /// <summary>
        /// 未检测名单
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void ShowBioNotCheckedList(object sender, DirectEventArgs e)
        {
            //_PAT_ID = e.ExtraParams["pat_id"].ToString();

            Window3.Show();
            Window3.Loader.SuspendScripting();
            //Window3.Loader.Url = "./WebForm1.aspx";
            //Window3.Loader.Url = "./Biochemical_Not_Checked_List.aspx;     // 每月未檢驗的病人統計名單 初版
            //Window3.Loader.Url = "./Biochemical_Not_Checked_List_V2.aspx;  // 每月未檢驗的病人統計名單 二版
            Window3.Loader.Url = "./Biochemical_Not_Checked_List_V2.aspx";
            Window3.Loader.DisableCaching = true;
            Window3.LoadContent();
        }



    }
}