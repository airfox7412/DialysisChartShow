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
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;


namespace Dialysis_Chart_Show
{
    public partial class ipad_PatientList : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        public bool autointo = Boolean.Parse(ConfigurationManager.AppSettings["autointo"].ToString());
        string cln3_time, cln3_a1, cln3_a2, cln3_b1, cln3_b2, cln1_col4;
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        //2015.01.05 查出有換床的記錄準備塞到list以便做lookup用id查找這個人是否在換床之列
        public class PropertyClass
        {
            private string ah_bed;
            public string Ah_bed
            {
                get{return ah_bed;}
                set { ah_bed = value; }
            }

            private string ah_patic;
            public string Ah_patic
            {
                get { return ah_patic; }
                set { ah_patic = value; }
            }

            private string ah_flr	;
            public string Ah_flr	
            {
                get { return ah_flr	; }
                set { ah_flr = value; }
            }
	
	        private string ah_sec;
            public string Ah_sec
            {
                get{return ah_sec;}
                set { ah_sec = value; }
            }

            private string ah_timetyp;
            public string Ah_timetyp	
            {
                get { return ah_timetyp; }
                set { ah_timetyp = value; }
            }
	
	        private string pif_name;
            public string Pif_name
            {
                get{return pif_name;}
                set { pif_name = value; }
            }

            private string pv_weight;
            public string Pv_weight
            {
                get { return pv_weight; }
                set { pv_weight = value; }
            }

            private string pv_macstat;
            public string Pv_macstat
            {
                get { return pv_macstat; }
                set { pv_macstat = value; }
            }
	
	        private string pif_sex;
            public string Pif_sex
            {
                get{return pif_sex;}
                set { pif_sex = value; }
            }

	    }

        private List<PropertyClass> GetListfromDataTable(DataTable MyDt)
        {
            DataTable dtMain = MyDt;
            List<PropertyClass> propClass = new List<PropertyClass>();
            foreach (DataRow dr in dtMain.Rows)
            {
                //Object of the propery class
                PropertyClass objPC = new PropertyClass();
                objPC.Ah_bed = dr["ah_bed"].ToString();
                objPC.Ah_patic = dr["ah_patic"].ToString();
                objPC.Ah_flr = dr["ah_flr"].ToString();
                objPC.Ah_sec = dr["ah_sec"].ToString();
                //objPC.Ah_bed4 = dr["ah_bed(4)"].ToString();
                objPC.Ah_timetyp = dr["ah_timetyp"].ToString();
                objPC.Pif_name = dr["pif_name"].ToString();
                objPC.Pv_weight = dr["pv_weight"].ToString();
                objPC.Pv_macstat = dr["pv_macstat"].ToString();
                objPC.Pif_sex = dr["pif_sex"].ToString();

                propClass.Add(objPC);
            }
            return propClass;
        }

		public string a
        {
            get
            {
                try
                {
                    return Session["a"].ToString();
                }
                catch
                {
                    //_NotificationShow_TimeOut();
                    return "";
                }
            }
            set
            {
                Session.Add("a", value);
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        { 
            if (!X.IsAjaxRequest)
            {
                if (Hospital == "Hospital_Suzhou" ||
                    Hospital == "Hospital_Hebei" ||
                    Hospital == "Hospital_Henan" ||
                    Hospital == "Hospital_Xian")
                {
                    Btn_TurnOn.ColumnWidth = 0.8;
                    Btn_h08.Hidden = false;
                }
                else if (Hospital == "Hospital_117" && (Session["USER_RIGHT"].ToString() == "DC" || Session["USER_RIGHT"].ToString() == "DH"))
                {
                    Btn_TurnOn.Hidden = true;
                    ImageBtn_TurnOff.Hidden = true;
                }
                else
                {
                    Btn_TurnOn.ColumnWidth = 0.8;
                    Btn_htest.Hidden = false;
                }

                string sSQL;
                //透析器型号
                sSQL = "SELECT hp2_code AS CODE, hp2_name AS NAME FROM hpack2_setup";
                DataTable dt_hpack2 = db.Query(sSQL);
                Common.SetComboBoxItem(cbo_mechine_model, dt_hpack2, true, "NAME", "CODE");
                cbo_mechine_model.Select(0);

                //管路型号
                sSQL = "SELECT hp3_code AS CODE, hp3_name AS NAME FROM hpack3_setup";
                DataTable dt_hpack3 = db.Query(sSQL);
                Common.SetComboBoxItem(cbo_hpack3, dt_hpack3, true, "NAME", "CODE");
                cbo_hpack3.Select(0);

                //血管通路類型
                sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup";
                DataTable dt_h_type = db.Query(sSQL);                
                Common.SetComboBoxItem(cbo_h_type, dt_h_type, true, "NAME", "CODE");
                cbo_h_type.Select(0);

                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                bedno.Text = Request.QueryString["bedno"];
                daytyp.Text = Request.QueryString["daytyp"];
                Text_Patient_ID.Text = Request.QueryString["pid"];

                //Show_Person();
                Show_Patient();
                Show_drug();
                Show_image();

                string sql = "SELECT COUNT(*) as cnts FROM clinical1_nurse WHERE cln1_patic='" + Text_Patient_ID.Text + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                    DialysisTimes.Text = dt.Rows[0]["cnts"].ToString();
                else
                    DialysisTimes.Text = "0";

                //sql = "SELECT pif_basetimes FROM pat_info WHERE pif_ic='" + Text_Patient_ID.Text + "' ";
                //dt = db.Query(sql);
                //if (dt.Rows.Count > 0)
                //{
                //    TextBaseTimes.Text = dt.Rows[0]["pif_basetimes"].ToString();
                //    pid.Text = dt.Rows[0]["pif_id"].ToString();
                //}
                try
                {
                    TextTimes.Text = (int.Parse(TextBaseTimes.Text) + int.Parse(DialysisTimes.Text)).ToString();
                }
                catch
                {
                    TextTimes.Text = "";
                }
                    
                dt.Dispose();
                db.myConnection.Close();
            }
            
        }

        protected void Show_Person()
        {
            string sql;
            string sWORK = "";

            DataTable dt;
            pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text);         
            if(pat_obj.status != pat.pat_status.not_login)
            {
                if(pat_obj.status == pat.pat_status.stop) //是否已經停機判斷
                {
                    //已關機
                    Btn_TurnOn.Disabled = true;
                    ImageBtn_TurnOff.ImageUrl = "Styles/Red_Btn.png";
                    ImageBtn_TurnOff.Disabled = false;
                }
                else if (pat_obj.status == pat.pat_status.not_stop)
                {
                    Btn_TurnOn.Disabled = true;
                    ImageBtn_TurnOff.Disabled = false;
                    ImageBtn_TurnOff.ImageUrl = "Styles/Green_Btn.png";
                    Text_Patient_weight.ReadOnly = true;
                }

                mechine_not_stop(pat_obj);
            }
            else
            {
			    //床位互換換至今日缺席沒來者
                bool tmpOccupyNoCome = false;
                bool changeBed = false;//換床 flag預設是false;
                //先查詢出已經換床的，預備下邊要處理後續資料用
                string sql00 = "SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, A.ah_bed, A.ah_timetyp, P.pif_name, V.pv_weight, V.pv_macstat, P.pif_sex ";
                sql00 += "FROM appointment_change A ";
                sql00 += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
                sql00 += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND V.pv_datevisit='" + toDay + "' ";
                sql00 += "WHERE ah_date='" + toDay + "' ";
                sql00 += "AND ah_timetyp=  '" + time.Text + "'  AND A.ah_patic is not null ";
                DataTable dt00 = db.Query(sql00);

                #region 尚未登入
                dt = pat_not_login(out sql);
                if (dt.Rows.Count == 0)
                {
                    Common.SaveERR("pat_not_login", "with sql: " + sql);
                    dt.Clear();
                    sql = "SELECT b.pif_name name1, b.pif_name as name, b.pif_sex, a.ah_patic id," + DateTime.Now.Year.ToString() + "-year(b.pif_dob) age, b.pif_hpack, ";
                    sql += "IFNULL(c.cln1_col3, a.ah_mactyp) AS apptst_mactyp ";
                    sql += "FROM appointment_change a ";
                    sql += "LEFT JOIN pat_info b ON a.ah_patic = b.pif_ic ";
                    sql += "LEFT JOIN clinical1_nurse c ON a.ah_patic = c.cln1_patic ";
                    sql += "WHERE a.ah_flr = '" + floor.Text + "' ";
                    sql += "AND a.ah_sec = '" + area.Text + "' ";
                    sql += "AND a.ah_timetyp = '" + time.Text + "' ";
                    sql += "AND a.ah_bed = '" + bedno.Text + "' ";
                    sql += "AND a.ah_dycnt = '" + daytyp.Text + "' ";

                    dt = db.Query(sql);
                    if (dt != null && dt.Rows.Count > 0)
                    {
                        sWORK += dt.Rows[0]["name"];

                        dt.Rows[0]["name"] += "(临)";
                    }

                    //NEW 改讀 appointment_setup    Andy 2014.12.31
                    string sql1 = " select * from  appointment_setup ";
                    sql1 += "WHERE apptst_patrefid = '" + sWORK + "' ";
                    DataTable dt11 = db.Query(sql1);

                    //OLD 原為 >0 改為=0  Andy 2014.12.31 
                    if (dt11 != null && dt11.Rows.Count > 0)
                    {
                        //NEW 
                        dt.Rows[0]["name"] = dt11.Rows[0]["apptst_patrefid"];
                    }
                    else
                    {
                        if (dt.Rows.Count > 0)
                            dt.Rows[0]["name"] = sWORK + "(临)";
                    }
                }
                else
                {
                    //2015.01.05 Frank將有換床查出的虛擬表格塞到list然後用id去查，
                    //如果在名單內把flag changeBed設為true，ipad瀏覽時要依換床名單把人名性別身分證號年齡換成對的，下面床位資訊不變
                        List<PropertyClass> pc = GetListfromDataTable(dt00);
                        string myString = dt.Rows[0]["id"].ToString();
                         
                        var match = pc.FirstOrDefault(Ah_patic => Ah_patic.Ah_patic.Contains(myString));

                        if (match != null)
                            changeBed = true;
							 
						//床位互換換至今日缺席沒來者
						//以下是缺席沒來然後臨時的換床
                        string tmpOccupyNoComeBed = bedno.Text;//上一頁傳過來的 selectedrow床位資訊的身分證號
                        //床位互換換至今日缺席沒來者
						//先去查這個床上目前安置的那個人身分證id是誰
                        string  ah_patic ="";
                        DataTable dt66 = new DataTable();
                        string sql66= "";
                        sql66 += "SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, A.ah_bed, A.ah_timetyp, P.pif_name, V.pv_weight, V.pv_macstat, P.pif_sex, ";
                        sql66 += DateTime.Now.Year.ToString() + "-year(P.pif_dob) age FROM appointment_change A ";
                        sql66 += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
                        sql66 += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND  V.pv_datevisit='" + toDay + "' ";
                        sql66 += "WHERE ah_date='" + toDay + "' ";
                        sql66 += "AND ah_timetyp=  '" + time.Text + "' ";
                        sql66 += "AND A.ah_patic IS NOT NULL ";
                        sql66 += "AND ah_bed = '" + bedno.Text + "' ";
                        dt66= db.Query(sql66);
                        if (dt66.Rows.Count > 0)
                        {
                            ah_patic = dt66.Rows[0]["ah_patic"].ToString();

                            var matchtmp = pc.FirstOrDefault(Ah_patic => Ah_patic.Ah_patic.Contains(ah_patic));

                            if (matchtmp != null)
                                tmpOccupyNoCome = true;
                        }
                        dt66.Dispose();
                        
                    if (changeBed)
                    {
                        DataTable dt99 = new DataTable();
                        string sql99 = "";
                        sql99 += "SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, A.ah_bed, A.ah_timetyp, P.pif_name, V.pv_weight, V.pv_macstat,  P.pif_sex, ";
                        sql99 += DateTime.Now.Year.ToString() + "-year(P.pif_dob) age FROM appointment_change A ";
                        sql99 += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
                        sql99 += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND  V.pv_datevisit='" + toDay + "' ";
                        sql99 += "WHERE ah_date='" + toDay + "' ";
                        sql99 += "AND ah_timetyp=  '" + time.Text + "' ";
                        sql99 += "AND A.ah_patic is not null ";
                        sql99 += "AND ah_bed = '" + bedno.Text + "'";
                        dt99= db.Query(sql99);
                        if (dt99.Rows.Count > 0)
                        {
                            DataRow row = dt99.Rows[0];
                            set_name_gender_id_age(
                                        row["pif_name"].ToString(), 
                                        row["pif_sex"].ToString(), 
                                        row["ah_patic"].ToString(), 
                                        row["age"].ToString());
                        }
                        dt99.Dispose();
                        changeBed = false;
                    }
					//床位互換換至今日缺席沒來者
					else if(tmpOccupyNoCome)
					{
                            DataTable dt999 = new DataTable();
                            string sql999 = "";
                            sql999 = sql999 + " SELECT A.ah_bed, A.ah_patic, A.ah_flr, A.ah_sec, A.ah_bed, A.ah_timetyp, P.pif_name, V.pv_weight, V.pv_macstat , P.pif_sex , " + DateTime.Now.Year.ToString() + "-year(P.pif_dob) age   FROM appointment_change A LEFT JOIN ";
                            sql999 = sql999 + " pat_info P ON A.ah_patic=P.pif_ic LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND  V.pv_datevisit='" + toDay + "' ";
                            sql999 = sql999 + " WHERE ah_date='" + toDay + "' AND ah_timetyp=  '" + time.Text + "' AND A.ah_patic is not null AND  ";
                            sql999 = sql999 + "  ah_bed = '" + tmpOccupyNoComeBed + "'  ";
                            dt999 = db.Query(sql999);
                            if (dt999.Rows.Count > 0)
                            {
                                DataRow row = dt999.Rows[0];
                                set_name_gender_id_age(
                                            row["pif_name"].ToString(),
                                            row["pif_sex"].ToString(), 
                                            row["ah_patic"].ToString(),
                                            row["age"].ToString());
                            }
                            dt999.Dispose();
                            tmpOccupyNoCome = false;  
						}
                    else
                    {
                        DataRow row = dt.Rows[0];
                        set_name_gender_id_age(
                                    row["name"].ToString(), 
                                    row["pif_sex"].ToString(),
                                    row["id"].ToString(),
                                    row["age"].ToString());
                    }

                    Text_area.Text = area.Text;
                    Text_Bed_NO.Text = bedno.Text;

                    Text_Machine_type.Text = dt.Rows[0]["apptst_mactyp"].ToString();

                    set_time_text();

                    String wcol34, sSQL;
                    String wcln1_diadate = "";

                    wcol34 = get_vascular_access(out wcln1_diadate);
                    if (wcol34 == "" || wcol34 == null)
                    {                    
                        sSQL = "SELECT * FROM pat_info WHERE pif_ic='" + Text_Patient_ID.Text + "' ";
                        DataTable dt_pat_info = db.Query(sSQL);
                        String wpif_hpack = "";
                        if (dt_pat_info.Rows.Count > 0)
                        {
                            wpif_hpack = dt_pat_info.Rows[0]["pif_hpack"].ToString();
                        }

                        sSQL = "SELECT * FROM package_setup " +
                                "WHERE pck_code='" + wpif_hpack + "' ";
                        DataTable dt_p_s = db.Query(sSQL);
                        String wpck_name = "";
                        if (dt_p_s.Rows.Count > 0)
                        {
                            wpck_name = dt_p_s.Rows[0]["pck_name"].ToString();
                        }

                        pat.update.update_vascular_access(wpck_name, wcln1_diadate, Text_Patient_ID.Text);
                    }
                    pif_hpack.Text = dt.Rows[0]["pif_hpack"].ToString();
                    string cln1_diadate;
                    string vascular_access = get_vascular_access(out cln1_diadate);
                    Common.SetComboBoxValue(cbo_h_type, vascular_access, false); //血管通路
                    set_hpack();                    
                #endregion
                }
            }

            if (cbo_h_type.Text.Trim() == "") //血管通路類型未設定或不正確
            {
                if (pat_obj.pif_hpack == "")
                {
                    cbo_h_type.LabelCls = "blink";
                }
            }
            else
            {
                cbo_h_type.LabelCls = "my-Field";
            }

            if (cbo_mechine_model.Text.Trim() == "") //透析器型號未設定或不正確
            {
                cbo_mechine_model.LabelCls = "blink";
            }
            else
            {
                cbo_mechine_model.LabelCls = "my-Field";
            }

            if (cbo_hpack3.Text.Trim() == "") //血管型號未設定或不正確
            {
                cbo_hpack3.LabelCls = "blink";
            }
            else
            {
                cbo_hpack3.LabelCls = "my-Field";
            }
        }

        private void set_hpack()
        {
            string sql = "SELECT b.weight, b.sca_time ";
            sql += "FROM pat_info a, data_scales b ";
            sql += "WHERE a.pif_ic='" + Text_Patient_ID.Text + "' ";
            sql += "AND a.pif_mrn=b.card_no ";
            sql += "AND b.sca_date='" + toDay + "' ";
            sql += "ORDER BY b.sca_time DESC ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                Text_Patient_weight.Text = dt.Rows[0]["weight"].ToString(); //獲得體重
            }
            else //河南，醫師先報到，沒帶卡手動輸入體重
            {
                sql = "SELECT cln1_col5 FROM clinical1_nurse ";
                sql += "WHERE cln1_patic='" + Text_Patient_ID.Text + "' ";
                sql += "AND cln1_diadate='" + toDay + "' ";
                dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    Text_Patient_weight.Text = dt.Rows[0]["cln1_col5"].ToString(); //獲得體重
                }
            }

            sql = "SELECT pif_hpack, pif_hpack2 as mechine_model, pif_hpack3 ";
            sql += "FROM pat_info ";
            sql += "WHERE pif_ic='" + Text_Patient_ID.Text + "' ";
            dt = db.Query(sql);
            if (dt.Rows.Count != 0)
            {
                DataRow row = dt.Rows[0];
                if (row["pif_hpack"].ToString().Length > 0)
                {
                    Common.SetComboBoxValue(cbo_h_type, row["pif_hpack"].ToString(), false);//血管通路类型
                }
                if (row["mechine_model"].ToString().Length > 0)
                {
                    Common.SetComboBoxValue(cbo_mechine_model, row["mechine_model"].ToString(), false);//透析機型號
                }
                if (row["pif_hpack3"].ToString()!="")
                {
                    Common.SetComboBoxValue(cbo_hpack3, row["pif_hpack3"].ToString(), false);  //管路型號
                }
            }
        }

        private string get_vascular_access(out string cln1_diadate)
        {
            string sql = "SELECT cln1_col34 as vascular_access, cln1_diadate " +
                            "FROM clinical1_nurse " +
                           "WHERE cln1_patic='" + Text_Patient_ID.Text + "' " +
                           "ORDER BY cln1_diadate DESC " +
                           "LIMIT 0,1 ";
            //string result;
            DataTable dt_h_type = db.Query(sql);
            if (dt_h_type.Rows.Count > 0)
            {
                cln1_diadate = dt_h_type.Rows[0]["cln1_diadate"].ToString();
                return dt_h_type.Rows[0]["vascular_access"].ToString();
            }
            cln1_diadate = null;
          //  Common._ErrorMsgShow("數據庫裡找不到血管通路");
          //  Common.SaveERR("debug", sql + "數據庫裡找不到血管通路");
            return null;
        }
        private void set_time_text()
        {
            if (time.Text == "001")
            {
                Text_Time.Text = "上午";
            }
            else if (time.Text == "002")
            {
                Text_Time.Text = "下午";
            }
            else if (time.Text == "003")
            {
                Text_Time.Text = "晚上";
            }
        }
        private void set_name_gender_id_age(string name, string pif_sex, string id, string age)
        {
            Text_Patient_Name.Text = name;
            if (pif_sex == "F")
            {
                Text_Patient_Gender.Text = "女";
            }
            else if (pif_sex == "M")
            {
                Text_Patient_Gender.Text = "男";
            }
            Text_Patient_ID.Text = id;
            Text_Patient_Age.Text = age;
        }

        /// 尚未登入
        private DataTable pat_not_login(out string sql)
        {
            sql = "SELECT a.apptst_patrefid name1, b.pif_name as name, b.pif_sex, a.apptst_patic id, " + DateTime.Now.Year.ToString() + "-year(b.pif_dob) age, b.pif_hpack, ";
            sql += "IFNULL(c.cln1_col3, a.apptst_mactyp) AS apptst_mactyp ";
            sql += "FROM appointment_setup a ";
            sql += "LEFT JOIN pat_info b ON a.apptst_patic = b.pif_ic ";
            sql += "LEFT JOIN clinical1_nurse c ON a.apptst_patic = c.cln1_patic ";
            sql += "WHERE a.apptst_flr = '" + floor.Text + "' ";
            sql += "AND a.apptst_sec = '" + area.Text + "' ";
            sql += "AND a.apptst_timetyp = '" + time.Text + "' ";
            sql += "AND a.apptst_bed = '" + bedno.Text + "' ";
            sql += "AND a.apptst_daytyp = '" + daytyp.Text + "' ";
            return db.Query(sql);
        }

        /// 已經登入未停止洗透機
        private void mechine_not_stop(pat pat_obj)
        {
            string sql = "";
            if (Hospital == "Hospital_Henan")
            {
                sql = "SELECT b.pif_hpack, b.pif_hpack2 as mechine_model, b.pif_hpack3, b.pif_name name, b.pif_sex, b.pif_ic id," + DateTime.Now.Year.ToString() + "-year(b.pif_dob) age,b.pif_hpack,";
                sql += "N.cln1_col3 AS MAC_TYPE,a.pv_weight,a.pv_macstat ";
                sql += "FROM pat_visit a ";
                sql += "LEFT JOIN pat_info b ON a.pv_ic=b.pif_ic ";
                sql += "LEFT JOIN clinical1_nurse N ON a.pv_ic=N.cln1_patic AND N.cln1_diadate='" + toDay + "' "; //淨化參數表格
                sql += "WHERE a.pv_datevisit = '" + toDay + "' ";
                sql += "AND a.pv_floor = '" + floor.Text + "' ";
                sql += "AND a.pv_sec = '" + area.Text + "' ";
                sql += "AND a.pv_time = '" + time.Text + "' ";
                sql += "AND a.pv_bedno = '" + bedno.Text + "' ";
            }
            else
            {
                sql = "SELECT b.pif_hpack, b.pif_hpack2 as mechine_model, b.pif_hpack3, b.pif_name name, b.pif_sex, b.pif_ic id," + DateTime.Now.Year.ToString() + "-year(b.pif_dob) age,b.pif_hpack,";
                sql += "a.pv_macno AS MAC_TYPE, a.pv_weight, a.pv_macstat ";
                sql += "FROM pat_visit a ";
                sql += "LEFT JOIN pat_info b ON a.pv_ic=b.pif_ic ";
                sql += "WHERE a.pv_datevisit = '" + toDay + "' ";
                sql += "AND a.pv_floor = '" + floor.Text + "' ";
                sql += "AND a.pv_sec = '" + area.Text + "' ";
                sql += "AND a.pv_time = '" + time.Text + "' ";
                sql += "AND a.pv_bedno = '" + bedno.Text + "' ";
            }

            DataTable dt = db.Query(sql);

            Text_Patient_Name.Text = pat_obj.name;       // dt.Rows[0]["name"].ToString();
            if (pat_obj.sex.Equals("F"))                 // if (dt.Rows[0]["pif_sex"].ToString() == "F")
            {
                Text_Patient_Gender.Text = "女";
            }
            else if (pat_obj.sex.Equals("M"))           //else if (dt.Rows[0]["pif_sex"].ToString() == "M")
            {
                Text_Patient_Gender.Text = "男";
            }
            Text_Patient_ID.Text = pat_obj.pif_ic;       // dt.Rows[0]["id"].ToString();
            Text_Patient_Age.Text = pat_obj.age;         // dt.Rows[0]["age"].ToString();

            //體重
            if (dt.Rows[0]["pv_weight"].ToString() != "")
            {
                Text_Patient_weight.Text = dt.Rows[0]["pv_weight"].ToString();
            }
            //血管通路类型
            if (dt.Rows[0]["pif_hpack"].ToString() != "")
            {
                Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["pif_hpack"].ToString(), false);
            }
            //透析器型号
            if (dt.Rows[0]["mechine_model"].ToString() != "")
            {
                Common.SetComboBoxValue(cbo_mechine_model, dt.Rows[0]["mechine_model"].ToString(), false);
            }
            // 管路型号
            string tube_model = pat_obj.pif_hpack3;     // Common.get_tube_model(dt.Rows[0]);
            if (tube_model != "")
            {
                Common.SetComboBoxValue(cbo_hpack3, tube_model, false);
            }
            if (Btn_TurnOn.Disabled == true)
            {
                cbo_h_type.ReadOnly = true;
                cbo_mechine_model.ReadOnly = true;
                cbo_hpack3.ReadOnly = true;
            }

            Text_area.Text = area.Text;
            Text_Bed_NO.Text = bedno.Text;

            Text_Machine_type.Text = dt.Rows[0]["MAC_TYPE"].ToString();

            if (time.Text == "001")
            {
                Text_Time.Text = "上午";
            }
            else if (time.Text == "002")
            {
                Text_Time.Text = "下午";
            }
            else if (time.Text == "003")
            {
                Text_Time.Text = "晚上";
            }

            pif_hpack.Text = dt.Rows[0]["pif_hpack"].ToString();

            string cln1_diadate;
            string vascular_access = get_vascular_access(out cln1_diadate);
            Common.SetComboBoxValue(cbo_h_type, vascular_access, false);//血管通路             
        }

        protected void Show_drug()
        {
            string sql;
            sql = "SELECT a.lgord_id, a.lgord_dateord, a.lgord_timeord, a.lgord_usr1, b.drg_name, ";
            sql += "a.lgord_intake, a.lgord_freq, a.lgord_medway, a.lgord_comment, a.lgord_nurs, a.lgord_dtactst, a.lgord_patic, a.lgord_drug ";
            sql += "FROM longterm_ordermgt a ";
            sql += "LEFT JOIN drug_list b ON a.lgord_drug = b.drg_code ";
            sql += "WHERE a.lgord_patic = '" + Text_Patient_ID.Text + "' AND a.lgord_actst = '00001'";//只取使用中藥物
            DataTable dt = db.Query(sql);

            Store istore = Grid_Long_Term.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();

            sql = "SELECT a.shord_id,a.shord_dateord,a.shord_timeord,a.shord_usr1, b.drg_name, ";
            sql += "a.shord_intake, a.shord_freq, a.shord_medway, a.shord_comment, a.shord_nurs, a.shord_dtactst ";
            sql += "FROM shortterm_ordermgt a ";
            sql += "LEFT JOIN drug_list b ON a.shord_drug = b.drg_code ";
            sql += "WHERE a.shord_patic = '" + Text_Patient_ID.Text + "' AND a.shord_dateord = '"+ toDay + "' AND a.shord_actst = '00001'";//只取使用中藥物
            dt = db.Query(sql);

            Store istore2 = Grid_Short_Term.GetStore();
            istore2.DataSource = db.GetDataArray(dt);
            istore2.DataBind();

            sql = "SELECT b.pdet_itemnm AS item, b.pdet_qty AS number FROM pat_info a, package_detail2 b ";
            sql += "WHERE a.pif_ic = '" + Text_Patient_ID.Text + "' AND a.pif_hpack = b.pdet_code ";
            sql += "UNION ";
            sql += "SELECT c.hp3_name AS item,'1' AS number FROM pat_info a,hpack3_setup c ";
            sql += "WHERE a.pif_ic = '" + Text_Patient_ID.Text + "' AND a.pif_hpack3 = c.hp3_code ";
            dt = db.Query(sql);

            Store istore3 = Grid_Other_hpack.GetStore();
            istore3.DataSource = db.GetDataArray(dt);
            istore3.DataBind();
        }
        
        protected void Show_image()
        {
            string ipath = ConfigurationManager.AppSettings["pat_images"].ToString(); 
            string iimage = "";
            string sql = "SELECT a.pif_sex, a.pif_imgloc  FROM pat_info a ";
            sql += "WHERE a.pif_ic = '" + Text_Patient_ID.Text + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count != 0)
            {
                iimage = dt.Rows[0]["pif_imgloc"].ToString().Replace("./", "");
                if (iimage == "")
                {
                    if (dt.Rows[0]["pif_sex"].ToString() == "M" || dt.Rows[0]["pif_sex"].ToString() == "")
                    {
                        iimage = "images/male.png";
                    }
                    else
                    {
                        iimage = "images/female.png";
                    }
                }
            }
            else
            {
                Image1.ImageUrl = "images/male.png";
            }
            Image1.ImageUrl = iimage;
        }

        private bool IsNumber(string strNumber)
        {
            System.Text.RegularExpressions.Regex r = new System.Text.RegularExpressions.Regex(@"^\d+(\.)?\d*$");
            return r.IsMatch(strNumber);
        }

        protected void Btn_TurnOn_Click(object sender, DirectEventArgs e)
        {
            if(Text_Patient_weight.Text =="")
            {
                Common._ErrorMsgShow("请输入体重!");
                return;
            }
            else if (!IsNumber(Text_Patient_weight.Text))
            {
                Common._ErrorMsgShow("请注意体重 非数字!");
                return;
            }
            else if (cbo_mechine_model.Text == "")
            {
                Common._ErrorMsgShow("请输入透析器型号!");
                return;
            }
            else if (cbo_hpack3.Text == "")
            {
                Common._ErrorMsgShow("请输入管路型号!");
                return;
            }

            Btn_TurnOn.Disabled = true;
            Text_Patient_weight.ReadOnly = true;
            ImageBtn_TurnOff.Disabled = false;
            ImageBtn_TurnOff.ImageUrl = "Styles/Green_Btn.png";

            if (pat.register_today(Text_Patient_ID.Text))
            {
                Common._NotificationShow("今天,病人已经注册!");
                return;
            }
            else
            {
                pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text, Text_Patient_ID.Text);
                pat_obj.pif_hpack = Common.GetComboBoxValue(cbo_h_type);
                pat_obj.pif_hpack2 = Common.GetComboBoxValue(cbo_mechine_model);
                pat_obj.pif_hpack3 = Common.GetComboBoxValue(cbo_hpack3);
                pat.update.insert_new_pat_to_pat_visit(pat_obj, Text_Machine_type.Text, Text_Patient_weight.Text);

                pat.update.update_pat_info(pat_obj);
                
                //clinical1_nurse若有資料則更新，沒有資料則新增，20160408 Alex
                pat.update.register_to_clinical1_nurse(Text_Patient_ID.Text, Common.GetComboBoxText(cbo_h_type), Common.GetComboBoxText(cbo_hpack3), Text_Patient_weight.Text);
                if (autointo)
                    Btn_detail_Click(sender, e); //自動轉入血液淨化頁面
            }
        }

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

        public void Update_clinical1_nurse(string person_id, string floor_no, string bed_no)
        {
            try
            {
                string sql = "SELECT * FROM clinical1_nurse WHERE cln1_patic='" + person_id + "' AND cln1_diadate='" + toDay + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count == 0)
                    return;
                else
                    pat.update.set_dialysis_time(person_id, floor_no, bed_no, toDay); //20160105 Alex 處理儲存透析結束時間
            }
            catch(Exception ex)
            {
                Common.SaveERR("Update_clinical1_nurse", ex.Message);
            }
        }

        protected void btnClose_Click(object sender, DirectEventArgs e)
        {
            w_LOGIN.Close();
        }

        protected void btnShClose_Click(object sender, DirectEventArgs e)
        {
            w_PHARLOGIN.Close();
        }        

        protected void btnDecrypt_Click(object sender, DirectEventArgs e)
        {
            if (w_txtUSER.Text == "")
            {
                Common._NotificationShow("请输入正确工号!");
                return;
            }
            else
            {
                string sql = "SELECT a.acclv_fname,a.acclv_funm ";
                sql += " FROM access_level a ";
                sql += "WHERE a.acclv_stfcode = '" + w_txtUSER.Text + "' ";

                DataTable dt = db.Query(sql);
                if (dt.Rows.Count == 1)
                {
                    sql = "UPDATE longterm_ordermgt SET ";
                    sql += "lgord_nurs='" + dt.Rows[0]["acclv_fname"] + "',";
                    sql += "lgord_timest='" + DateTime.Now.ToString("HH:mm:ss") + "' ";
                    sql += "WHERE lgord_id=" + DRUG_ROWID.Text + " ";
                    db.Excute(sql);
                    Show_drug();
                    w_txtUSER.Text = "";
                    w_LOGIN.Close();
                }
                else
                {
                    w_txtUSER.Text = "";
                    Common._NotificationShow("工号不存在，请重新输入!");
                }
            }
        }

        protected void Btn_ordershortdrug_Click(object sender, DirectEventArgs e)
        {
            w_PHARLOGIN.Show();
            txt_stfcode.Focus(false, 100);
        }

        #region 取得帳號權限(呼叫PHP取得USER的權限)
        protected void btnpharlogin_Click(object sender, DirectEventArgs e)
        {
            if (txt_stfcode.Text == "" || w_txtPASS.Text == "")
            {
                Common._NotificationShow("请输入正确工号及密码!");
                return;
            }
            else
            {
                string sSQL = "SELECT passwd, type, name FROM access_level WHERE usrnm='" + txt_stfcode.Text + "' AND active='A'";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count > 0)
                {
                    JiaMiJieMi aeskey = new JiaMiJieMi();
                    string pwd = aeskey.AES_Decrypt(aeskey.Base64Decrypt(dt.Rows[0]["passwd"].ToString()));
                    string usertype = dt.Rows[0]["type"].ToString();
                    string docname = dt.Rows[0]["name"].ToString();
                    if (pwd == w_txtPASS.Text)
                    {
                        if (usertype == "DC" || usertype == "DH")
                        {
                            string urlstr = "i_pad_ordershortdrug.aspx?person_id=" + Text_Patient_ID.Text;
                            urlstr += "&patient_name=" + Text_Patient_Name.Text + "&pat_sex=" + Text_Patient_Gender.Text + "&pat_docname=" + docname;
                            urlstr += "&floor=" + floor.Text + "&bedno=" + bedno.Text + "&area=" + area.Text + "&time=" + time.Text + "&dayTyp=" + daytyp.Text;
                            X.Redirect(urlstr);
                        }
                        else
                        {
                            Common._NotificationShow("只有醫師可開立醫囑，请重新输入!");
                            txt_stfcode.Text = "";
                            w_txtPASS.Text = "";
                            return;
                        }
                    }
                    else
                    {
                        Common._NotificationShow("密码错误，请重新输入!");
                        w_txtPASS.Text = "";
                        return;
                    }
                }
                else
                {
                    Common._NotificationShow("工号不存在，请重新输入!");
                    txt_stfcode.Text = "";
                    w_txtPASS.Text = "";
                    return;
                }
            }
        }
        #endregion
        
        #region 上傳照片
        protected void GetPatImg(object sender, DirectEventArgs e)
        {
            if (this.UploadImage.HasFile)
            {
                string strImg = string.Empty;
                if (UploadImage.PostedFile != null && UploadImage.PostedFile.ContentLength > 0)
                {
                    strImg = this.UploadImage.PostedFile.FileName;
                    string strExt = System.IO.Path.GetExtension(strImg).ToLower();
                    if (strExt != ".jpg" && strExt != ".jepg" && strExt != ".bmp" && strExt != ".gif")
                    {
                        Common._NotificationShow("抱歉仅支持.jpg，jepg，bmp，gif");
                        return;
                    }
                }
                fileUpload(strImg);
            }
            else
            {
                Common._NotificationShow("请选择照片");
            }
        }
        #endregion

        #region 上传图片处理
        public void fileUpload(string strFilePath)
        {
            string strPictureName = string.Empty;//上传后的图片名，以当前时间为文件名，确保文件名没有重复           
            if (!string.IsNullOrEmpty(strFilePath))
            {
                string strMapPath = "";
                try
                {
                    string strFileName = System.IO.Path.GetFileName(strFilePath);// System.IO.Path.GetExtension(strFilePath).ToLower();
                    strPictureName = DateTime.Now.Ticks.ToString() + strFileName;
                    try
                    {
                        strMapPath = Server.MapPath("./") + "patimages\\";
                    }
                    catch (Exception ex)
                    {
                        strMapPath = "D:\\Temp\\patimages\\";
                    }
                    Common._NotificationShow("照片上传中");
                    string strPath = strMapPath + strPictureName;
                    UploadImage.PostedFile.SaveAs(strPath);
                    Image1.ImageUrl = "patimages/"+ strPictureName;
                    string sql = "UPDATE pat_info SET pif_imgloc='patimages/" + strPictureName + "' "; //20160510 Alex
                    sql += "WHERE pif_ic='" + Text_Patient_ID.Text + "'";
                    db.Excute(sql);
                    db.Close();
                }
                catch (Exception ex)
                {
                    Common._ErrorMsgShow("上传照片失败");
                }
            }
            else
            {
                string path = Directory.GetCurrentDirectory();// 用于获得应用程序当前工作目录
                Common._NotificationShow("请选择照片");
            }
        }
        #endregion

        #region 血液淨化小節項目及儲存
        protected void Show()
        {
            string sql = "";
            sql = "SELECT Max(a.dialysis_time) b,TIMEDIFF(Max(a.dialysis_time),MIN(a.dialysis_time)) c FROM data_list a ";
            sql += " WHERE a.person_id ='" + Text_Patient_ID.Text + "' ";
            sql += " AND a.column_11 <>'' ";
            sql += " AND a.dialysis_date = '" + toDay + "' ";
            DataTable dt0 = db.Query(sql);
            if (dt0.Rows.Count != 0)
                cln3_time = dt0.Rows[0]["b"].ToString();

            sql = "SELECT cast(a.cln1_col5 as DECIMAL(6,1))-cast(a.cln1_col8 as DECIMAL(6,1)) column_2 FROM clinical1_nurse a ";
            sql += "WHERE a.cln1_patic='" + Text_Patient_ID.Text + "' ";
            sql += "  AND a.cln1_diadate='" + toDay + "' ";
            sql += "  AND a.cln1_col5<>'' ";
            sql += "  AND a.cln1_col8<>'' ";
            sql += "  AND (a.cln1_col5 - a.cln1_col8) >=0"; //抓透析前體重減掉透析後且不小於零體重
            dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
                cln1_col4 = dt0.Rows[0]["column_2"].ToString();//脫水
            else //沒有透析後體重則抓脫水最大值
            {
                sql = "SELECT a.column_2 FROM data_list a ";
                sql += "WHERE a.person_id = '" + Text_Patient_ID.Text + "' ";
                sql += "  AND a.dialysis_date = '" + toDay + "' ";
                sql += "  AND a.column_2<>'' ";
                sql += "ORDER BY a.dialysis_time DESC limit 1";//抓脫水不等於零最後一筆
                dt0 = db.Query(sql);
                if (dt0.Rows.Count != 0 && cln1_col4 == dt0.Rows[0]["column_2"].ToString())
                    cln1_col4 = dt0.Rows[0]["column_2"].ToString();//脫水
            }
            //===========================================================================================================
            sql = "SELECT MIN(SUBSTRING_INDEX(cln2_bp,'/',1)) minA, MAX(SUBSTRING_INDEX(cln2_bp,'/',1)) maxA, ";
            sql += "MIN(SUBSTRING_INDEX(cln2_bp,'/',-1)) minB, MAX(SUBSTRING_INDEX(cln2_bp,'/',-1)) maxB ";
            sql += "FROM clinical2_nurse ";
            sql += "WHERE cln2_patic = '" + Text_Patient_ID.Text + "' ";
            sql += "  AND cln2_date = '" + toDay + "' ";
            sql += "  AND SUBSTRING_INDEX(cln2_bp,'/',1)<>'' AND SUBSTRING_INDEX(cln2_bp,'/',-1)<>'' ";
            sql += "  AND cln2_bp LIKE '%/%'";
            dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                cln3_a1 = dt0.Rows[0]["minA"].ToString(); // minA.ToString();
                cln3_a2 = dt0.Rows[0]["maxA"].ToString(); // maxA.ToString();
                cln3_b1 = dt0.Rows[0]["minB"].ToString(); // minB.ToString();
                cln3_b2 = dt0.Rows[0]["maxB"].ToString(); // maxB.ToString();
            }
        }

        private void SaveClick()
        {
            Show(); //透析小節顯示及處理
            string sql = "SELECT * FROM clinical3_nurse ";
            sql += "WHERE cln3_patic='" + Text_Patient_ID.Text + "' AND cln3_date='" + toDay + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                sql = "UPDATE clinical3_nurse SET ";
                sql += "  cln3_time = '" + cln3_time + "',";
                sql += "  cln3_rmk = '" + cln1_col4 + "',";
                sql += "  cln3_a1 = '" + cln3_a1 + "',";
                sql += "  cln3_a2 = '" + cln3_a2 + "',";
                sql += "  cln3_b1 = '" + cln3_b1 + "',";
                sql += "  cln3_b2 = '" + cln3_b2 + "',";
                sql += "  cln3_ysa = '无症状',";
                sql += "  cln3_pressure = '基本正常',";
                sql += "  cln3_bld='1', ";
                sql += "  cln3_yn = 'N', ";
                sql += "  cln3_rmk2 = '' ";
                sql += " WHERE cln3_patic = '" + Text_Patient_ID.Text + "' ";
                sql += "   AND cln3_date = '" + toDay + "' ";
            }
            else
            {
                sql = "INSERT into clinical3_nurse(cln3_patic,cln3_date,cln3_time," +
                                                      "cln3_a1,cln3_a2,cln3_b1,cln3_b2," +
                                                      "cln3_CatheterAccess,cln3_MuscleAtrophy,cln3_ysa,cln3_pressure,cln3_DialysisMachine,cln3_HeparinPumpArtery,cln3_HeparinPumpVein," +
                                                      "cln3_bld,cln3_yn,cln3_rmk2,cln3_rmk) ";//
                sql += "VALUES('" + Text_Patient_ID.Text + "','" + toDay + "','" + cln3_time + "'," +
                              "'" + cln3_a1 + "','" + cln3_a2 + "','" + cln3_b1 + "','" + cln3_b2 + "'," +
                              "'',''," +
                              "'无症状','基本正常'," +
                              "'','','','1','N','','" + cln1_col4 + "')";
            }
            db.Excute(sql);
            Common._NotificationShow("储存成功!");
            dt.Dispose();
        }
        #endregion

        protected void LongNurse_Click(object sender, DirectEventArgs e)
        {
            Show_drug();
        }

        protected void Nurse_Click(object sender, DirectEventArgs e)
        {
            drugkind.Text = e.ExtraParams["drugkind"];
            drugid.Text = e.ExtraParams["DRUG_ROWID"];
            if (TextExeTime.Text == "")
                TextExeTime.Text = DateTime.Now.ToString("hh:mm");
            else
                TextExeTime.Text = e.ExtraParams["DRUG_TIME"];
            WindowTime.Title = "用药时间";
            this.WindowTime.Show();
            TextExeTime.Focus(false, 100);
        }

        #region 儲存护士执行时间
        protected void btnTime_Click(object sender, DirectEventArgs e)
        {
            string sql = "";
            if (drugkind.Text == "L")
            {
                sql = "UPDATE longterm_ordermgt SET ";
                sql += "lgord_dtactst='" + TextExeTime.Text + "' ";
                sql += "WHERE lgord_id=" + drugid.Text + " ";
            }
            else if (drugkind.Text == "S")
            {
                sql = "UPDATE shortterm_ordermgt SET ";
                sql += "shord_dtactst='" + TextExeTime.Text + "' ";
                sql += "WHERE shord_id=" + drugid.Text + " ";
            }
            db.Excute(sql);
            this.WindowTime.Close();
            Show_drug();
        }
        #endregion

        protected void btnTimeClose_Click(object sender, DirectEventArgs e)
        {
            WindowTime.Close();
        }

        protected void TextTimes_Click(object sender, DirectEventArgs e)
        {
            BaseTimes.Show();
            TextBaseTimes.Focus(false, 100);
        }

        protected void btnTClose_Click(object sender, DirectEventArgs e)
        {
            BaseTimes.Hide();
        }

        #region 儲存透析基本次數
        protected void btnT_Click(object sender, DirectEventArgs e)
        {
            if (TextBaseTimes.Text != "")
            {
                db.Excute("UPDATE pat_info SET pif_basetimes=" + TextBaseTimes.Text + " WHERE pif_ic='" + Text_Patient_ID.Text + "' ");
                Common._NotificationShow("儲存透析基本次數<br>完成");
                TextTimes.Text = (int.Parse(TextBaseTimes.Text) + int.Parse(DialysisTimes.Text)).ToString();
                BaseTimes.Hide();
            }
        }
        #endregion

        #region 回病患列表
        protected void Btn_Home_Click(object sender, DirectEventArgs e)
        {
            X.Redirect(ConfigurationManager.AppSettings["iPAD"].ToString().Replace("../", ""));
        }
        #endregion

        #region 治療參數
        protected void Btn_detail01_Click(object sender, DirectEventArgs e)
        {
            string sql;
            sql = "SELECT * ";
            sql += "FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + Text_Patient_ID.Text + "' ";
            sql += " AND cln1_diadate <> '" + toDay + "'"; //確認是否有歷史資料
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                string sURL;
                switch (Hospital)
                {
                    case "Hospital_Suzhou":
                        sURL = "ipad_detaillist_Suzhou.aspx";
                        break;
                    case "Hospital_117":
                        sURL = "ipad_detaillist_117.aspx";
                        break;
                    case "Hospital_Xian":
                        sURL = "ipad_detaillist_Xian.aspx";
                        break;
                    case "Hospital_Alasamo":
                        sURL = "ipad_history_Alasamo.aspx";
                        break;
                    case "Hospital_Henan":
                        sURL = "ipad_history_Henan.aspx";
                        break;
                    case "Hospital_Hebei":
                        sURL = "ipad_history_Hebei.aspx";
                        break;
                    case "Standard":
                        sURL = "ipad_detaillist_Standard.aspx";
                        break;
                    default:
                        sURL = "ipad_detaillist.aspx";
                        break;
                }
                X.Redirect(sURL + "?patient_id=" + Text_Patient_ID.Text +
                                      "&patient_name=" + Text_Patient_Name.Text +
                                      "&machine_type=" + Text_Machine_type.Text +
                                      "&mechine_model=" + cbo_mechine_model.SelectedItem.Value +
                                      "&hpack=" + cbo_h_type.Text +
                                      "&hpack3=" + cbo_hpack3.SelectedItem.Value +
                                      "&patient_weight=" + Text_Patient_weight.Text +
                                      "&bedno=" + bedno.Text +
                                      "&floor=" + floor.Text +
                                      "&area=" + area.Text +
                                      "&time=" + time.Text +
                                      "&daytyp=" + daytyp.Text);
                return;
            }
            else
            {
                Common._ErrorMsgShow("查无前次资料");
                return;
            }
        }
        #endregion

        #region 血液淨化
        protected void Btn_detail_Click(object sender, DirectEventArgs e)
        {
            // start of added by jeffrey at 2015/11/18
            //pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text, Text_Patient_ID.Text);

            //pat.pat_status pat_status = pat.get_pat_status(pat_obj);
            //if (pat_status == pat.pat_status.not_login)
            //{
            //    Common._ErrorMsgShow("病患尚未登入!");
            //    return;
            //}
            string sURL;
            switch (Hospital)
            {
                case "Hospital_Suzhou":
                    sURL = "ipad_detaillist_Suzhou.aspx";
                    break;
                case "Hospital_117":
                    sURL = "ipad_detaillist_117.aspx";
                    break;
                case "Hospital_Xian":
                    sURL = "ipad_detaillist_Xian.aspx";
                    break;
                case "Hospital_Alasamo":
                    sURL = "ipad_detaillist_Alasamo.aspx";
                    break;
                case "Hospital_Henan":
                    sURL = "ipad_detaillist_Henan.aspx";
                    break;
                case "Hospital_Hebei":
                    sURL = "ipad_detaillist_Hebei.aspx";
                    break;
                case "Standard":
                    sURL = "ipad_detaillist_Standard.aspx";
                    break;
                default:
                    sURL = "ipad_detaillist.aspx";
                    break;
            }
            if ((cbo_h_type.SelectedItem != null) && (cbo_mechine_model.SelectedItem != null) && (cbo_hpack3.SelectedItem != null))
            {
                sURL += "?patient_id=" + Text_Patient_ID.Text;
                sURL += "&patient_name=" + Text_Patient_Name.Text;
                sURL += "&machine_type=" + Server.UrlEncode(Text_Machine_type.Text);
                sURL += "&mechine_model=" + cbo_mechine_model.SelectedItem.Value;
                sURL += "&hpack=" + cbo_h_type.Text;
                sURL += "&hpack3=" + cbo_hpack3.SelectedItem.Value;
                sURL += "&patient_weight=" + Text_Patient_weight.Text;
                sURL += "&bedno=" + Server.UrlEncode(bedno.Text);
                sURL += "&floor=" + floor.Text;
                sURL += "&area=" + area.Text;
                sURL += "&time=" + time.Text;
                sURL += "&daytyp=" + daytyp.Text + "&page=2";
                X.Redirect(sURL);
            }
            return;
        }
        #endregion

        #region 淨化小結
        protected void Btn_detail02_Click(object sender, DirectEventArgs e)
        {
            pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text, Text_Patient_ID.Text);
            pat.pat_status pat_status = pat.get_pat_status(pat_obj);
            if (pat_status == pat.pat_status.not_login)
            {
                Common._ErrorMsgShow("病患尚未登入!");
                return;
            }
            else if (pat_status == pat.pat_status.not_stop)
            {
                Common._ErrorMsgShow("机器尚未停机!");
                return;
            }
            string sURL;
            switch (Hospital)
            {
                case "Hospital_Suzhou":
                    sURL = "ipad_detaillist02_Suzhou.aspx";
                    break;
                case "Hospital_117":
                    sURL = "ipad_detaillist02_117.aspx";
                    break;
                case "Hospital_Xian":
                    sURL = "ipad_detaillist02_Xian.aspx";
                    break;
                case "Hospital_Alasamo":
                    sURL = "ipad_detaillist02_Alasamo.aspx";
                    break;
                case "Hospital_Henan":
                    sURL = "ipad_detaillist02_Henan.aspx";
                    break;
                case "Hospital_Hebei":
                    sURL = "ipad_detaillist02_Hebei.aspx";
                    break;
                case "Standard":
                    sURL = "ipad_detaillist02_Standard.aspx";
                    break;
                default:
                    sURL = "ipad_detaillist02.aspx";
                    break;
            }
            X.Redirect(sURL + "?patient_id=" + Text_Patient_ID.Text +
                                "&patient_name=" + Text_Patient_Name.Text +
                                "&bedno=" + bedno.Text +
                                "&floor=" + floor.Text +
                                "&mechine_model=" + cbo_mechine_model.SelectedItem.Value +
                                "&hpack=" + cbo_h_type.Text +
                                "&hpack3=" + cbo_hpack3.SelectedItem.Value +
                                "&area=" + area.Text +
                                "&time=" + time.Text +
                                "&daytyp=" + daytyp.Text);
        }
        #endregion

        #region 監控數據
        protected void Btn_Report_Click(object sender, DirectEventArgs e)
        {
            string sql = "SELECT DISTINCT person_id FROM data_list ";
            sql += "WHERE person_id='" + Text_Patient_ID.Text + "' AND dialysis_date='" + toDay + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                string urlstr = "report/Rpt_View_ipad0.aspx?person_id=" + Text_Patient_ID.Text + "&date=" + toDay + "&patient_name=" + Text_Patient_Name.Text + "&report=2";
                X.Redirect(urlstr);
            }
            else
            {
                Common._NotificationShow("目前没有数据!");
            }
        }
        #endregion

        #region 圖形按鈕關機
        protected void ImageBtn_TurnOff_click(object sender, DirectEventArgs e)
        {
            string Status = "";
            string sql = "SELECT pv_macstat FROM pat_visit ";
            string sqlw = "WHERE pv_ic='" + Text_Patient_ID.Text + "' AND pv_datevisit = '" + toDay + "' ";

            sql += sqlw;
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pv_macstat"].ToString() == "S")
                {
                    ImageBtn_TurnOff.ImageUrl = "Styles/Green_Btn.png";
                    Status = "'A'";
                }
                else
                {
                    ImageBtn_TurnOff.ImageUrl = "Styles/Red_Btn.png";
                    pat.update.set_dialysis_time(Text_Patient_ID.Text, floor.Text, bedno.Text, toDay);
                    Status = "'S'";
                }
                sql = "UPDATE pat_visit SET pv_macstat = " + Status + " ";
                sql += sqlw;
                db.Excute(sql);
            }
        }
        #endregion

        #region 血透評估表
        protected void Btn_h08_Click(object sender, DirectEventArgs e)
        {
            string sUrl = "";
            if (Hospital == "Hospital_Suzhou")
            {
                sUrl = "ipad_EvaForm_Suzhou.aspx?pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text;
            }
            else if (Hospital == "Hospital_Hebei" || Hospital == "Hospital_Henan")
            {
                sUrl = "ipad_EvaForm.aspx?pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text;
                
            }
            //X.Redirect(sUrl);
            string script = "window.open('" + sUrl + "', 'myNewWindow')";
            this.Button1.AddScript(script);
        }
        #endregion

        protected void Show_Patient()
        {
            pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text);
            if (pat_obj.status != pat.pat_status.not_login)
            {
                if (pat_obj.status == pat.pat_status.stop) //是否已經停機判斷
                {
                    Btn_TurnOn.Disabled = true; //已關機
                    ImageBtn_TurnOff.ImageUrl = "Styles/Red_Btn.png";
                    ImageBtn_TurnOff.Disabled = false;
                }
                else if (pat_obj.status == pat.pat_status.not_stop)
                {
                    Btn_TurnOn.Disabled = true;
                    ImageBtn_TurnOff.Disabled = false;
                    ImageBtn_TurnOff.ImageUrl = "Styles/Green_Btn.png";
                    Text_Patient_weight.ReadOnly = true;
                }

                mechine_not_stop(pat_obj);
            }
            string sSQL = ""; 
            if (Text_Patient_ID.Text != "")
            {
                sSQL = "SELECT A.*, P.pif_id, P.pif_basetimes, P.pif_name, V.pv_weight, V.pv_macstat, P.pif_sex, " + DateTime.Now.Year.ToString() + "-year(P.pif_dob) AS Age ";
                sSQL += "FROM pat_info P ";
                sSQL += "LEFT JOIN appointment_change A ON A.ah_patic=P.pif_ic ";
                sSQL += "LEFT JOIN pat_visit V ON V.pv_ic=P.pif_ic AND V.pv_datevisit='" + toDay + "' ";
                sSQL += "WHERE P.pif_ic='" + Text_Patient_ID.Text + "'";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count > 0)
                {
                    set_name_gender_id_age(dt.Rows[0]["pif_name"].ToString(), dt.Rows[0]["pif_sex"].ToString(), Text_Patient_ID.Text, dt.Rows[0]["Age"].ToString());
                    pid.Text = dt.Rows[0]["pif_id"].ToString(); 
                    TextBaseTimes.Text = dt.Rows[0]["pif_basetimes"].ToString();
                    Text_area.Text = area.Text;
                    Text_Bed_NO.Text = bedno.Text;
                    if (dt.Rows[0]["ah_mactyp"].ToString() != "")
                    {
                        Text_Machine_type.Text = dt.Rows[0]["ah_mactyp"].ToString();
                    }
                    set_time_text(); 
                    set_hpack();
                    if (dt.Rows[0]["pv_weight"].ToString() != "")
                    {
                        Text_Patient_weight.Text = dt.Rows[0]["pv_weight"].ToString();
                    }
                }
            }
        }

        #region 血透化驗數據表
        protected void Btn_htest_Click(object sender, DirectEventArgs e)
        {
            string beg_date = DateTime.Now.ToString("yyyy-MM-") + "01";
            string end_date = DateTime.Now.ToString("yyyy-MM-dd"); ;
            string sURL = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=HTest&_BEG_DATE=" + beg_date + "&_END_DATE=" + end_date;
            string script = "window.open('" + sURL + "', 'myNewWindow')";
            this.Button1.AddScript(script);
        }
        #endregion
    }
}