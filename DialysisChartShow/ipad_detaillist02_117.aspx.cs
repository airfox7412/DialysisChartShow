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

namespace Dialysis_Chart_Show
{
    public partial class ipad_detaillist02_117 : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='dialysis_symptom' ";
                DataTable dt1 = db.Query(sSQL);
                Common.SetComboBoxItem(cbo_symptom, dt1, false, "NAME", "CODE");
                cbo_symptom.Select(0);

                sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='blood_pressure' ";
                dt1 = db.Query(sSQL);
                Common.SetComboBoxItem(cbo_pressure, dt1, false, "NAME", "CODE");
                cbo_pressure.Select(0);
                dt1.Dispose();

                patient_id.Text = Request.QueryString["patient_id"];
                patient_name.Text = Request.QueryString["patient_name"];
                machine_type.Text = Request.QueryString["machine_type"];
                bedno.Text = Request.QueryString["bedno"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                daytyp.Text = Request.QueryString["daytyp"];
                TextField16.Text = Request.QueryString["hpack"];
                hpack.Text = Request.QueryString["hpack"];
                hpack3.Text = Request.QueryString["hpack3"];
                mechine_model.Text = Request.QueryString["mechine_model"];
                txt_weight_before.Text = Request.QueryString["patient_weight"];
                if (patient_name.Text == null)
                {
                    patient_name.Text = Request.QueryString["amp;patient_name"];
                    machine_type.Text = Request.QueryString["amp;machine_type"];
                    bedno.Text = Request.QueryString["amp;bedno"];
                    floor.Text = Request.QueryString["amp;floor"];
                    area.Text = Request.QueryString["amp;area"];
                    time.Text = Request.QueryString["amp;time"];
                    daytyp.Text = Request.QueryString["amp;daytyp"];
                    TextField16.Text = Request.QueryString["amp;hpack"];
                    hpack.Text = Request.QueryString["amp;hpack"];
                    hpack3.Text = Request.QueryString["amp;hpack3"];
                    mechine_model.Text = Request.QueryString["amp;mechine_model"];
                    txt_weight_before.Text = Request.QueryString["amp;patient_weight"];
                }

                Show();
                show_time();
                db.myConnection.Close();
            }
        }

        protected void Show()
        {
            Label2.Text = patient_name.Text;
            Label4.Text = floor.Text;
            Label6.Text = bedno.Text;

            string sql = "SELECT a.cln1_col11, a.cln1_col12, a.cln1_col4, a.cln1_col33, a.cln1_col35, b.* FROM clinical1_nurse a ";
            sql += "LEFT JOIN clinical3_nurse b ON a.cln1_patic=b.cln3_patic AND a.cln1_diadate=b.cln3_date ";
            sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate = '" + DateTime.Now.ToString("yyyy-MM-dd") + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                //TextField3.Text = dt.Rows[0]["cln3_time"].ToString();
                TextField4.Text = dt.Rows[0]["cln3_rmk"].ToString();
                TextField5.Text = dt.Rows[0]["cln3_a1"].ToString();
                TextField6.Text = dt.Rows[0]["cln3_a2"].ToString();
                TextField7.Text = dt.Rows[0]["cln3_b1"].ToString();
                TextField8.Text = dt.Rows[0]["cln3_b2"].ToString();
                //TextFieldCatheterAccess.Text = dt.Rows[0]["cln3_CatheterAccess"].ToString();
                //TextFieldMuscleAtrophy.Text = dt.Rows[0]["cln3_MuscleAtrophy"].ToString();
                Common.SetComboBoxValue(cbo_symptom, dt.Rows[0]["cln3_ysa"].ToString(), false);
                Common.SetComboBoxValue(cbo_pressure, dt.Rows[0]["cln3_pressure"].ToString(), false);
                TextArea2.Text = dt.Rows[0]["cln3_rmk2"].ToString();
                TextField2.Text = DateTime.Now.ToString("yyyy-MM-dd") + " " + dt.Rows[0]["cln1_col11"].ToString();
                TextField3.Text = dt.Rows[0]["cln1_col12"].ToString();
                TextField9.Text = dt.Rows[0]["cln1_col33"].ToString();
                TextField10.Text = dt.Rows[0]["cln1_col35"].ToString();
                TextField16.Text = dt.Rows[0]["cln1_col4"].ToString();
            }
            dt.Dispose();
        }

        protected void show_time()
        {
            string sql = "SELECT cast(cln1_col5 as DECIMAL(6,1))-cast(cln1_col8 as DECIMAL(6,1)) column_2 FROM clinical1_nurse ";
            sql += "WHERE cln1_patic='" + patient_id.Text + "' ";
            sql += "  AND cln1_diadate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "  AND cln1_col5<>'' AND cln1_col8<>'' ";
            sql += "  AND (cln1_col5 - cln1_col8) >=0";//抓透析前體重減掉透析後且不小於零體重

            DataTable dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                TextField4.Text = dt0.Rows[0]["column_2"].ToString();//脫水
            }
            else//沒有透析後體重則抓脫水最大值
            {
                sql = "SELECT column_2 FROM data_list ";
                sql += "WHERE person_id = '" + patient_id.Text + "' ";
                sql += "  AND dialysis_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                sql += "  AND column_2<>'' ";
                sql += "ORDER BY dialysis_time DESC limit 1";//抓脫水不等於零最後一筆
                dt0 = db.Query(sql);
                if (dt0.Rows.Count != 0 && TextField4.Text == dt0.Rows[0]["column_2"].ToString())
                {
                    TextField4.Text = dt0.Rows[0]["column_2"].ToString();//脫水
                }
            }
            //===========================================================================================================
            sql = "SELECT MIN(SUBSTRING_INDEX(cln2_bp,'/',1)) minA, MAX(SUBSTRING_INDEX(cln2_bp,'/',1)) maxA, ";
            sql += "MIN(SUBSTRING_INDEX(cln2_bp,'/',-1)) minB, MAX(SUBSTRING_INDEX(cln2_bp,'/',-1)) maxB ";
            sql += "FROM clinical2_nurse ";
            sql += "WHERE cln2_patic = '" + patient_id.Text + "' ";
            sql += "  AND cln2_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "  AND SUBSTRING_INDEX(cln2_bp,'/',1)<>'' AND SUBSTRING_INDEX(cln2_bp,'/',-1)<>'' ";
            sql += "  AND cln2_bp LIKE '%/%'";
            dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                TextField5.Text = dt0.Rows[0]["minA"].ToString();
                TextField6.Text = dt0.Rows[0]["maxA"].ToString();
                TextField7.Text = dt0.Rows[0]["minB"].ToString();
                TextField8.Text = dt0.Rows[0]["maxB"].ToString();
            }
        }

        protected void Btn_save_Click(object sender, DirectEventArgs e)
        {
            string sql = "SELECT a.* ", scln3_bld = "1";

            // 2015年7月27日 下午 01:20:23 svn Revision: 3122 當時或之前. 
            // ipad 净化过程小结 與  pc版血透 myhaisv4 净化过程小结 各唱各的調
            // ipad 將 血压 寫在 clinical3_nurse 資料表 cln3_pressure 欄位
            // pc版血透myhaisv4 將 血压 寫在 clinical3_nurse 資料表 cln3_bld 欄位
            switch (Common.GetComboBoxText(cbo_pressure))
            {
                case "基本正常":
                    scln3_bld = "1";
                    break;
                case "较高":
                    scln3_bld = "2";
                    break;
                case "较低":
                    scln3_bld = "3";
                    break;
                default:
                    throw new Exception("GetComboBoxText cbo_pressure failure");
            }

            sql += "  FROM clinical3_nurse a ";
            sql += " where a.cln3_patic = '" + patient_id.Text + "' ";
            sql += "  and a.cln3_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count == 1)
            {
                sql = "UPDATE clinical3_nurse b ";
                sql += "set b.cln3_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "',";
                sql += "  b.cln3_time = '" + ttt.Text + "',";
                sql += "  b.cln3_rmk = '" + TextField4.Text + "',";
                sql += "  b.cln3_a1 = '" + TextField5.Text + "',";
                sql += "  b.cln3_a2 = '" + TextField6.Text + "',";
                sql += "  b.cln3_b1 = '" + TextField7.Text + "',";
                sql += "  b.cln3_b2 = '" + TextField8.Text + "',";
                sql += "  b.cln3_ysa = '" + Common.GetComboBoxText(cbo_symptom) + "',";
                sql += "  b.cln3_pressure = '" + Common.GetComboBoxText(cbo_pressure) + "',";
                sql += "  b.cln3_bld = '";
                sql += scln3_bld;

                sql += "', ";
                sql += "    b.cln3_yn = '" + (Common.GetComboBoxText(cbo_symptom) == "无症状" ? "N" : "Y");
                sql += "', ";

                sql += "    b.cln3_rmk2 = '" + TextArea2.Text + "' ";
                sql += " where b.cln3_patic = '" + patient_id.Text + "' ";
                sql += "   AND b.cln3_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            }
            else
            {
                sql = "INSERT into clinical3_nurse(cln3_patic,cln3_date,cln3_time," +
                                                  "cln3_a1,cln3_a2,cln3_b1,cln3_b2," +
                                                  "cln3_CatheterAccess,cln3_MuscleAtrophy,cln3_ysa,cln3_pressure,cln3_DialysisMachine,cln3_HeparinPumpArtery,cln3_HeparinPumpVein," +
                                                  "cln3_bld,cln3_yn,cln3_rmk2,cln3_rmk) ";//
                sql += "VALUES('" + patient_id.Text + "','" + DateTime.Now.ToString("yyyy-MM-dd") + "','" + ttt.Text + "'," +
                              "'" + TextField5.Text + "','" + TextField6.Text + "','" + TextField7.Text + "','" + TextField8.Text + "'," +
                              "'',''," +
                              "'" + Common.GetComboBoxText(cbo_symptom) + "','" + Common.GetComboBoxText(cbo_pressure) + "'," +
                              "'','','','";

                sql += scln3_bld;

                sql += "','";
                sql += Common.GetComboBoxText(cbo_symptom) == "无症状" ? "N" : "Y";
                sql += "','" + TextArea2.Text + "','" + TextField4.Text + "')";
            }
            db.Excute(sql);
            Common._NotificationShow("储存成功!");
            //db.Close();
        }

        #region 回首頁
        protected void Btn_Home_Click(object sender, DirectEventArgs e)
        {
            X.Redirect(ConfigurationManager.AppSettings["iPAD"].ToString().Replace("../", ""));
        }
        #endregion

        #region 治療參數
        protected void Btn_detail01_Click(object sender, DirectEventArgs e)
        {
            string sql;
            sql = "SELECT a.* ";
            sql += "FROM clinical1_nurse a ";
            sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' ";
            sql += " and a.cln1_diadate <> '" + DateTime.Now.ToString("yyyy-MM-dd") + "'"; //確認是否有歷史資料
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                string sURL;
                switch (sProcessOfPurifyingTheBlood)
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
                    case "Hospital_Henan":
                        sURL = "ipad_detaillist_Henan.aspx";
                        break;
                    case "Standard":
                        sURL = "ipad_detaillist_Standard.aspx";
                        break;
                    default:
                        sURL = "ipad_detaillist.aspx";
                        break;
                }
                X.Redirect(sURL + "?patient_id=" + patient_id.Text +
                                    "&patient_name=" + patient_name.Text +
                                    "&machine_type=" + machine_type.Text +
                                    "&mechine_model=" + mechine_model.Text +
                                    "&hpack=" + hpack.Text +
                                    "&hpack3=" + hpack3.Text +
                                    "&patient_weight=" + patient_weight.Text +
                                    "&bedno=" + bedno.Text +
                                    "&floor=" + floor.Text +
                                    "&area=" + area.Text +
                                    "&time=" + time.Text +
                                    "&daytyp=" + daytyp.Text + "&page=1");
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
            pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text, patient_id.Text);

            pat.pat_status pat_status = pat.get_pat_status(pat_obj);
            if (pat_status == pat.pat_status.not_login)
            {
                Common._ErrorMsgShow("病患尚未登入!");
                return;
            }
            string sURL;
            switch (sProcessOfPurifyingTheBlood)
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
                case "Hospital_Henan":
                    sURL = "ipad_detaillist_Henan.aspx";
                    break;
                case "Standard":
                    sURL = "ipad_detaillist_Standard.aspx";
                    break;
                default:
                    sURL = "ipad_detaillist.aspx";
                    break;
            }
            X.Redirect(sURL + "?patient_id=" + patient_id.Text +
                                    "&patient_name=" + patient_name.Text +
                                    "&machine_type=" + machine_type.Text +
                                    "&mechine_model=" + mechine_model.Text +
                                    "&hpack=" + hpack.Text +
                                    "&hpack3=" + hpack3.Text +
                                    "&patient_weight=" + patient_weight.Text +
                                    "&bedno=" + bedno.Text +
                                    "&floor=" + floor.Text +
                                    "&area=" + area.Text +
                                    "&time=" + time.Text +
                                    "&daytyp=" + daytyp.Text + "&page=2");
            return;
        }
        #endregion

        #region 淨化小結
        protected void Btn_detail02_Click(object sender, DirectEventArgs e)
        {
        }
        #endregion

        #region 上一頁
        protected void Btn_back_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("ipad_PatientList.aspx?editmode=page3&floor=" + Label4.Text +
                                                                  "&area=" + area.Text +
                                                                  "&time=" + time.Text +
                                                                  "&bedno=" + Label6.Text +
                                                                  "&dayTyp=" + daytyp.Text);
        }
        #endregion
    }
}