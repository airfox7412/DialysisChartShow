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
    public partial class ipad_history_117 : BaseForm // System.Web.UI.Page
    {
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");
        public string sTIME="";

        string cln1_col1, cln1_col6, cln1_col10, cln1_col13, cln1_col14, cln1_col15, cln1_col17, cln1_col18, cln1_col19;
        string cln1_col26, cln1_col27, cln1_col34;
        string Col11, coltext11;
        string Col21, coltext21;
        string Col31, coltext31;
        string Col41, coltext41;
        string Col50;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request.QueryString["patient_id"];
                patient_name.Text = Request.QueryString["patient_name"];
                machine_type.Text = Request.QueryString["machine_type"];
                mechine_model.Text = Request.QueryString["mechine_model"];
                bedno.Text = Request.QueryString["bedno"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                daytyp.Text = Request.QueryString["daytyp"];
                hpack.Text = Request.QueryString["hpack"];
                hpack3.Text = Request.QueryString["hpack3"];
                txt_weight_before.Text = Request.QueryString["patient_weight"];
                if (patient_name.Text == null)
                {
                    patient_name.Text = Request.QueryString["amp;patient_name"];
                    machine_type.Text = Request.QueryString["amp;machine_type"];
                    mechine_model.Text = Request.QueryString["amp;mechine_model"];
                    bedno.Text = Request.QueryString["amp;bedno"];
                    floor.Text = Request.QueryString["amp;floor"];
                    area.Text = Request.QueryString["amp;area"];
                    time.Text = Request.QueryString["amp;time"];
                    daytyp.Text = Request.QueryString["amp;daytyp"];
                    hpack.Text = Request.QueryString["amp;hpack"];
                    hpack3.Text = Request.QueryString["amp;hpack3"];
                    txt_weight_before.Text = Request.QueryString["amp;patient_weight"]; 
                }
                patient_weight.Text = txt_weight_before.Text; //體重
                init_PAGE1();
                Show(); 
                Show_TPRBP();
            }
        }

        protected void Show()
        {
            GetComboxData(); //取得各項COMBOBOX資料
            Label2.Text = patient_name.Text;
            Label4.Text = floor.Text;
            Label6.Text = bedno.Text;
            string sql;
            sql = "SELECT * FROM clinical1_nurse ";
            sql += "WHERE cln1_patic = '" + patient_id.Text + "' AND cln1_diadate <> '" + toDay + "' ";
            sql += "ORDER BY cln1_diadate DESC LIMIT 1";
            DataTable dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
            {
                info_date1.Text = dt0.Rows[0]["cln1_diadate"].ToString(); //上次日期
                cln1_col1 = dt0.Rows[0]["cln1_col1"].ToString();
                cln1_col6 = dt0.Rows[0]["cln1_col6"].ToString();
                cln1_col13 = dt0.Rows[0]["cln1_col13"].ToString();
                cln1_col14 = dt0.Rows[0]["cln1_col14"].ToString();
                cln1_col15 = dt0.Rows[0]["cln1_col15"].ToString();
                cln1_col26 = dt0.Rows[0]["cln1_col26"].ToString();
                cln1_col27 = dt0.Rows[0]["cln1_col27"].ToString();
                cln1_col17 = dt0.Rows[0]["cln1_col17"].ToString(); //
                cln1_col18 = dt0.Rows[0]["cln1_col18"].ToString(); //置换方式
                cln1_col19 = dt0.Rows[0]["cln1_col19"].ToString(); //置换方式
                cln1_col34 = dt0.Rows[0]["cln1_col34"].ToString(); //血管通路
            };
            dt0.Dispose();

            // 治療參數
            Common.SetComboBoxValue(cbo_dialysis_type, machine_type.Text, false);//透析方式

            DataTable dt = new DataTable();
            sql = "SELECT a.*, c.cln1_col11 AS Henan1, c.cln1_col12 AS Henan2, c.cln1_col21 AS Henan3, c.cln1_col22 AS Henan4, ";
            sql += "b.cln1_col11 as col11, b.cln1_col12 as col12, ";
            sql += "b.cln1_col21 as col21, b.cln1_col22 as col22, ";
            sql += "b.cln1_col31 as col31, b.cln1_col32 as col32, ";
            sql += "b.cln1_col41 as col41, b.cln1_col42 as col42, ";
            sql += "b.cln1_col50 as col50 FROM clinical1_nurse a ";
            sql += "LEFT JOIN clinical1_nurse_suzhou b ON a.cln1_patic=b.cln1_patic AND a.cln1_diadate=b.cln1_diadate ";
            sql += "LEFT JOIN clinical1_nurse_henan c ON a.cln1_patic=c.cln1_patic AND a.cln1_diadate=c.cln1_diadate ";
            sql += "WHERE a.cln1_patic = '" + patient_id.Text + "' AND a.cln1_diadate='" + info_date1.Text + "'";
            dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                DataRow row = dt.Rows[0];

                if (row["cln1_col1"].ToString() == "")
                    Common.SetComboBoxValue(cbo_diagnosis, cln1_col1, false);//诊断
                else
                    Common.SetComboBoxValue(cbo_diagnosis, row["cln1_col1"].ToString(), false);

                if (row["cln1_col26"].ToString() == "")
                    Common.SetComboBoxValue(cbo_hpack2, cln1_col26, false);//透析器型号
                else
                    Common.SetComboBoxValue(cbo_hpack2, row["cln1_col26"].ToString(), false);//透析器型号

                if (row["cln1_col34"].ToString() == "")
                    Common.SetComboBoxValue(cbo_h_type, cln1_col34, false);//血管通路
                else
                    Common.SetComboBoxValue(cbo_h_type, dt.Rows[0]["cln1_col34"].ToString(), false);//血管通路

                if (row["cln1_col15"].ToString() == "")
                    Common.SetComboBoxValue(SelectBox10, cln1_col15, false);//肝素
                else
                    Common.SetComboBoxValue(SelectBox10, dt.Rows[0]["cln1_col15"].ToString(), false);//肝素

                string tube_model = Common.get_tube_model(row, "cln1_col27");
                if (tube_model != null)
                    Common.SetComboBoxValue(cbo_hpack3, tube_model, false); //管路型号

                txt_weight_after.Text = row["cln1_col8"].ToString(); //透析后体重
                if (row["cln1_col6"].ToString() == "")
                    txt_weight_after_expect.Text = cln1_col6; //干體重
                else
                    txt_weight_after_expect.Text = row["cln1_col6"].ToString(); //干体重
                try
                {
                    txt_weight_before.Text = dt.Rows[0]["cln1_col5"].ToString(); //透析前体重
                    if (dt.Rows[0]["cln1_col7"].ToString() == "")
                        TextField3.Text = (Convert.ToDecimal(txt_weight_before.Text) - Convert.ToDecimal(txt_weight_after_expect.Text)).ToString();
                    else
                        TextField3.Text = dt.Rows[0]["cln1_col7"].ToString();
                }
                catch (Exception ex)
                {
                    //目标定容量
                    TextField3.Text = "";
                }

                TextField5.Text = dt.Rows[0]["cln1_col10"].ToString(); //透析開始時間
                TextField6.Text = dt.Rows[0]["cln1_col11"].ToString(); //透析結束時間
                TextField7.Text = dt.Rows[0]["cln1_col12"].ToString(); //透析合計時間
                TextField8.Text = row["cln1_col13"].ToString();
                TextAdd.Text = row["cln1_col14"].ToString();
                TextAmount.Text = row["cln1_col17"].ToString();

                TextField2.Text = row["Henan1"].ToString();
                TextField4.Text = row["Henan2"].ToString();
                TextField9.Text = row["Henan3"].ToString();
                TextField10.Text = row["Henan4"].ToString();

                TextField12.Text = dt.Rows[0]["cln1_col20"].ToString();
                TextField13.Text = dt.Rows[0]["cln1_col21"].ToString();
                TextField14.Text = dt.Rows[0]["cln1_col22"].ToString();
                TextField131.Text = dt.Rows[0]["cln1_col25"].ToString();
                TextField24.Text = dt.Rows[0]["cln1_col35"].ToString();
                TextField25.Text = dt.Rows[0]["cln1_col36"].ToString();
                TextField23.Text = dt.Rows[0]["cln1_col33"].ToString();

                Col11 = dt.Rows[0]["col11"].ToString();
                coltext11 = dt.Rows[0]["col12"].ToString();
                Col21 = dt.Rows[0]["col21"].ToString();
                coltext21 = dt.Rows[0]["col22"].ToString();
                Col31 = dt.Rows[0]["col31"].ToString();
                coltext31 = dt.Rows[0]["col32"].ToString();
                Col41 = dt.Rows[0]["col41"].ToString();
                coltext41 = dt.Rows[0]["col42"].ToString();
                Col50 = dt.Rows[0]["col50"].ToString();

                TextArea1.Text = row["cln1_col23"].ToString();

                string[] check = row["cln1_col16"].ToString().Split(new Char[] { ',' });

                try
                {
                    if (txt_weight_before.Text == "")
                        txt_weight_before.Text = "0";
                    decimal before_weight = Convert.ToDecimal(txt_weight_before.Text);
                    if (txt_weight_before.Text != patient_weight.Text)
                    {
                        txt_weight_before.RemoveCls("Text-red");
                        txt_weight_before.AddCls("Text-black");
                    }

                    TextField3.Text = dt.Rows[0]["cln1_col7"].ToString();
                    decimal dec1 = 0;
                    if (txt_weight_after_expect.Text != "")
                        dec1 = Convert.ToDecimal(txt_weight_after_expect.Text);
                    else
                        dec1 = 0;
                    if (TextField3.Text != (before_weight - dec1).ToString())
                    {
                        TextField3.RemoveCls("Text-red");
                        TextField3.AddCls("Text-black");
                    }
                }
                catch (Exception ex)
                {
                    Common._ErrorMsgShow(ex.Message.ToString());
                }
            }
        }

        #region 各類Combobox取值
        protected void GetComboxData()
        {
            string sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='heparin' ORDER BY CLASS2_CODE";
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(SelectBox10, dt1, true, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='diagnosis' ";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_diagnosis, dt1, true, "NAME", "CODE");

            sSQL = "SELECT hp2_code AS CODE, hp2_name AS NAME FROM hpack2_setup WHERE hp2_status='Y'"; //透析器型號
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_hpack2, dt1, true, "NAME", "CODE");
            Common.SetComboBoxItem(cbo_hpack4, dt1, true, "NAME", "CODE");

            sSQL = "SELECT hp3_code AS CODE, hp3_name AS NAME FROM hpack3_setup WHERE hp3_status='Y'"; //管路
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_hpack3, dt1, true, "NAME", "CODE");
            Common.SetComboBoxItem(cbo_hpack5, dt1, true, "NAME", "CODE");

            //sSQL = "SELECT hp_code AS CODE, hp_subgrp AS NAME FROM h_type"; //舊程式使用h_type與FLEX使用不同資料表
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup WHERE pck_status='Y'";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt1, true, "NAME", "CODE");

            sSQL = "SELECT CLASS2_CODE AS CODE, CLASS2_NAME AS NAME FROM a_class2 WHERE CLASS1_CODE='dialysis_type' ";
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_dialysis_type, dt1, true, "NAME", "CODE");
            cbo_dialysis_type.Select(0);

            dt1.Dispose();

            Common.SetComboBoxValue(cbo_h_type, hpack.Text, true); //血管通路類型
            Common.SetComboBoxValue(cbo_hpack3, hpack3.Text, true); //血管通路
            Common.SetComboBoxValue(cbo_hpack2, mechine_model.Text, true); //透析器型號
        }
        #endregion

        protected void Show_TPRBP()
        {
            try
            {
                String sql = "SELECT a.dialysis_date, a.dialysis_time, a.column_7, a.column_6, a.column_3, a.column_10, a.column_8, a.column_4, ";
                sql += "b.cln2_t,b.cln2_p,b.cln2_r,b.cln2_bp,b.cln2_rmk,b.cln2_user,b.cln2_time FROM clinical2_nurse as b ";
                sql += "LEFT JOIN data_list as a ON a.person_id=b.cln2_patic AND a.dialysis_date=b.cln2_date AND a.dialysis_time=b.cln2_time ";
                sql += "WHERE a.person_id='" + patient_id.Text + "' AND a.dialysis_date='" + toDay + "' ";
                sql += "ORDER BY a.dialysis_time";
                DataTable dt = db.Query(sql);
                Store istore = Grid_Show_TPRBP.GetStore();
                istore.DataSource =  GetDataArray(dt);
                istore.DataBind();
                db.myConnection.Close();
            }
            catch {
                Common._ErrorMsgShow("资料开启错误！");
            }
        }

        protected void init_PAGE1()
        {
            Grid_Show_TPRBP.Visible = true;
            FormPanel1.Title = "治疗参数";

            cbo_diagnosis.ReadOnly = true;
            cbo_hpack2.ReadOnly = true;
            cbo_hpack3.ReadOnly = true;
            txt_weight_before.ReadOnly = true; 
            txt_weight_after_expect.ReadOnly = true;
            TextField3.ReadOnly = true;
            txt_weight_after.ReadOnly = true;
            TextField8.ReadOnly = true;
            TextAdd.ReadOnly = true;
            TextAmount.ReadOnly = true;
            SelectBox10.ReadOnly = true;

            cbo_dialysis_type.ReadOnly = true;
            cbo_h_type.ReadOnly = true;            
            
            TextArea1.ReadOnly = true;

            cbo_diagnosis.RemoveCls("Text-black");
            cbo_diagnosis.AddCls("Text-blue");

            cbo_hpack2.RemoveCls("Text-black");
            cbo_hpack2.AddCls("Text-blue");

            cbo_hpack3.RemoveCls("Text-black");
            cbo_hpack3.AddCls("Text-blue");

            txt_weight_before.RemoveCls("Text-black");
            txt_weight_before.AddCls("Text-blue");
            TextField3.RemoveCls("Text-black");
            TextField3.AddCls("Text-blue");
            cbo_dialysis_type.RemoveCls("Text-black");
            cbo_dialysis_type.AddCls("Text-blue");
            cbo_h_type.RemoveCls("Text-black");
            cbo_h_type.AddCls("Text-blue");
        }

        private void Update_DrugTime(string nus)
        {
            try
            {
                string today = toDay;
                string totime = DateTime.Now.ToString("HH:mm");
                string sql = "";
                sql = "UPDATE longterm_ordermgt SET ";
                sql += "lgord_nurs='" + nus + "', lgord_dtactst='" + totime + "' ";
                sql += "WHERE lgord_patic='" + patient_id.Text + "'; ";

                sql += "UPDATE shortterm_ordermgt SET ";
                sql += "shord_nurs='" + nus + "', shord_dtactst='" + totime + "' ";
                sql += "WHERE shord_patic='" + patient_id.Text + "' AND shord_dateord='" + today + "'; ";
                db.Excute(sql);
            }
            catch(Exception)
            {
                Common._ErrorMsgShow("Update use Drug time Error.");
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

        #region 計算透析時間
        protected void text_CalTime(object sender, EventArgs e)
        {
            try
            {
                DateTime tmin = Convert.ToDateTime(TextField5.Text);
                DateTime tmax = Convert.ToDateTime(TextField6.Text);
                var timeDiff = new TimeSpan(tmax.Ticks - tmin.Ticks);
                TextField7.Text = timeDiff.ToString();
            }
            catch
            { }
        }
        #endregion

        #region 回首頁
        protected void Btn_Home_Click(object sender, DirectEventArgs e)
        {
            string sURL = "ipad_Default.aspx";
            X.Redirect(sURL);
        }
        #endregion

        #region 治療參數
        protected void Btn_detail01_Click(object sender, DirectEventArgs e)
        {
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
            string sURL = "ipad_detaillist_117.aspx";
            sURL += "?patient_id=" + patient_id.Text +
                    "&patient_name=" + patient_name.Text +
                    "&machine_type=" + machine_type.Text +
                    "&mechine_model=" + Common.GetComboBoxValue(cbo_hpack2) +
                    "&hpack=" + Common.GetComboBoxValue(cbo_h_type) +
                    "&hpack3=" + Common.GetComboBoxValue(cbo_hpack3) +
                    "&patient_weight=" + patient_weight.Text +
                    "&bedno=" + bedno.Text +
                    "&floor=" + floor.Text +
                    "&area=" + area.Text +
                    "&time=" + time.Text +
                    "&daytyp=" + daytyp.Text;
            X.Redirect(sURL);
        }
        #endregion

        #region 淨化小結
        protected void Btn_detail02_Click(object sender, DirectEventArgs e)
        {
            pat pat_obj = new pat(floor.Text, area.Text, time.Text, bedno.Text, patient_id.Text);
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

            string sURL = "ipad_detaillist02_117.aspx";
            sURL += "?patient_id=" + patient_id.Text +
                                "&patient_name=" + patient_name.Text +
                                "&bedno=" + bedno.Text +
                                "&floor=" + floor.Text +
                                "&mechine_model=" + Common.GetComboBoxValue(cbo_hpack2) +
                                "&hpack=" + Common.GetComboBoxValue(cbo_h_type) +
                                "&hpack3=" + Common.GetComboBoxValue(cbo_hpack3) +
                                "&area=" + area.Text +
                                "&time=" + time.Text +
                                "&daytyp=" + daytyp.Text;
            X.Redirect(sURL);
        }
        #endregion

        #region 上一頁
        protected void Btn_back_Click(object sender, DirectEventArgs e)
        {
            string sURL = "ipad_PatientList.aspx?editmode=page3&floor=" + floor.Text +
                                                                  "&area=" + area.Text +
                                                                  "&time=" + time.Text +
                                                                  "&bedno=" + bedno.Text +
                                                                  "&dayTyp=" + daytyp.Text;
            X.Redirect(sURL);
        }
        #endregion

        protected void cbo_dialysis_Change(object sender, DirectEventArgs e)
        {
            CheckType();
            if (Common.GetComboBoxText(cbo_dialysis_type) == "HDF" || Common.GetComboBoxText(cbo_dialysis_type) == "HDF+HP")
            {
                sp1.Hidden = true;
                TextField11.Hidden = false;
                TextAmount.LabelWidth = 160;
            }
            else
            {
                sp1.Hidden = false;
                TextField11.Hidden = true;
                TextAmount.LabelWidth = 185;
            }
        }

        protected void CheckType()
        {

            if (cbo_dialysis_type.Text == "HD+HP" || cbo_dialysis_type.Text == "HDF+HP")
            {
                cbo_hpack4.ReadOnly = false;
            }
            else
            {
                cbo_hpack4.ReadOnly = true;
                cbo_hpack4.Select(0);
            }
        }
    }
}