using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;
using System.Data;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show.checkin
{
    public partial class Patient_Info : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                txt_height.Hidden = true;
                info_date.Text = DateTime.Now.ToString("yyyy-MM-dd");
                SetComboBox();
                if (Request.QueryString["kind"] == "TempPatient")
                {
                    BtnClear.Hidden = false;
                    BtnSearch.Hidden = false;
                }
                else
                {
                    LoadPatinfo(Session["PAT_ID"].ToString());
                }
            }
        }

        protected void SetComboBox()
        {
            string sSQL = "SELECT ins_name AS NAME, ins_code AS CODE FROM ins_setup";
            DataTable dt = db.Query(sSQL);
            Common.SetComboBoxItem(ComboBox_ins, dt, true, "NAME", "CODE");

            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup WHERE pck_status='Y'"; //血管通路类型
            dt = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_h_type, dt, true, "NAME", "CODE");

            sSQL = "SELECT hp2_code AS CODE, hp2_name AS NAME FROM hpack2_setup WHERE hp2_status='Y'"; //透析器型號
            dt = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_machine_model, dt, true, "NAME", "CODE");

            sSQL = "SELECT hp3_code AS CODE, hp3_name AS NAME FROM hpack3_setup WHERE hp3_status='Y'"; //血管通路
            dt = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_hpack3, dt, true, "NAME", "CODE");

            sSQL = "SELECT name AS NAME FROM access_level ";
            sSQL += "WHERE (type='DH' OR type='DC') AND active='A' ";
            dt = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_docname, dt, true, "NAME", "NAME");

            Ext.Net.ListItem litem1;
            litem1 = new Ext.Net.ListItem(" ", " ");
            ComboBox_grp.Items.Add(litem1);
            litem1 = new Ext.Net.ListItem("A", "A");
            ComboBox_grp.Items.Add(litem1);
            litem1 = new Ext.Net.ListItem("B", "B");
            ComboBox_grp.Items.Add(litem1);
            litem1 = new Ext.Net.ListItem("AB", "AB");
            ComboBox_grp.Items.Add(litem1);
            litem1 = new Ext.Net.ListItem("O", "O");
            ComboBox_grp.Items.Add(litem1);
        }

        private Boolean CheckRequired()        
        {
            if (txt_name.Text == "" || 
                ComboBox_ins.Text == "" || 
                txt_dob.Text == "" || 
                cbo_h_type.Text == "" || 
                cbo_machine_model.Text == "" || 
                cbo_hpack3.Text == "" || 
                cbo_docname.Text == "" )
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected bool LoadPatinfo(string PAT_ID)
        {
            string sql = "SELECT a.*, b.*, c.* FROM pat_info a ";
            sql += "LEFT JOIN zinfo_f_011 b ON a.pif_id=b.pat_id ";
            sql += "LEFT JOIN blood_group c ON a.pif_ic=c.bgrp_patic ";
            sql += "WHERE a.pif_id = '" + PAT_ID + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                txt_name.Text = dt.Rows[0]["pif_name"].ToString();
                txt_ic.Text = dt.Rows[0]["pif_ic"].ToString(); //身分證號
                txt_mrn.Text = dt.Rows[0]["pif_mrn"].ToString(); //IC卡號
                txt_dob.Text = dt.Rows[0]["pif_dob"].ToString(); //出生日期
                if (dt.Rows[0]["pif_pattyp"].ToString() == "H")
                {
                    opt_s1.Checked = true;
                    opt_s2.Checked = false;
                }
                else if (dt.Rows[0]["pif_pattyp"].ToString() == "W")
                {
                    opt_s1.Checked = false;
                    opt_s2.Checked = true;
                }
                else
                {
                    opt_s1.Checked = false;
                    opt_s2.Checked = false;
                }
                if (dt.Rows[0]["pif_pattyp"].ToString() == "W")
                {
                }
                if (dt.Rows[0]["pif_sex"].ToString() == "M")
                {
                    opt_2_1.Checked = true;
                    opt_2_2.Checked = false;
                }
                else if (dt.Rows[0]["pif_sex"].ToString() == "F")
                {
                    opt_2_1.Checked = false;
                    opt_2_2.Checked = true;
                }
                else
                {
                    opt_2_1.Checked = false;
                    opt_2_2.Checked = false;
                }

                txt_address.Text = dt.Rows[0]["pif_address"].ToString(); //住址
                txt_13.Text = dt.Rows[0]["pif_contactperson"].ToString();
                txt_15.Text = dt.Rows[0]["pif_contact"].ToString();
                txt_insid.Text = dt.Rows[0]["pif_insid"].ToString(); //醫保號
                info_date.Text = dt.Rows[0]["pif_createdate"].ToString();

                txt_5.Text = dt.Rows[0]["txt_5"].ToString(); //民族
                txt_6.Text = dt.Rows[0]["txt_6"].ToString(); //住院号
                txt_7.Text = dt.Rows[0]["txt_7"].ToString(); //透析号
                txt_8.Text = dt.Rows[0]["txt_8"].ToString(); //工作单位
                txt_10.Text = dt.Rows[0]["txt_10"].ToString(); //联系电话
                txt_11.Text = dt.Rows[0]["txt_11"].ToString(); //手机
                txt_14.Text = dt.Rows[0]["txt_14"].ToString(); //家属关系
                txt_18.Text = dt.Rows[0]["txt_18"].ToString(); //其它费用

                Common.SetComboBox(ComboBox_grp, dt.Rows[0]["bgrp_grp"].ToString());
                if (dt.Rows[0]["bgrp_aids"].ToString() == "Y")
                    Checkbox_aids.Checked = true;
                if (dt.Rows[0]["bgrp_syphilis"].ToString() == "Y")
                    Checkbox_syphilis.Checked = true;
                if (dt.Rows[0]["bgrp_hbv"].ToString() == "Y")
                    Checkbox_hbv.Checked = true;
                if (dt.Rows[0]["bgrp_hcv"].ToString() == "Y")
                    Checkbox_hcv.Checked = true;
                if (dt.Rows[0]["bgrp_diabetic"].ToString() == "Y")
                    Checkbox_diabetic.Checked = true;

                ComboBox_ins.Text = dt.Rows[0]["pif_insurance"].ToString();
                cbo_h_type.Text = dt.Rows[0]["pif_hpack"].ToString();
                cbo_machine_model.Text = dt.Rows[0]["pif_hpack2"].ToString();
                cbo_hpack3.Text = dt.Rows[0]["pif_hpack3"].ToString();
                cbo_docname.Text = dt.Rows[0]["pif_docname"].ToString();
                return true;
            }
            else
                return false;
        }

        protected void Btn_Save_Click(object sander, DirectEventArgs e)
        {
            if (CheckRequired() == false)
            {
                Common._ErrorMsgShow("请填写必要栏位(*)");
            }
            else
            {
                string pattype = "", patsex = "";

                if (opt_s1.Checked == true)
                {
                    pattype = "H";
                }
                else if (opt_s2.Checked == true)
                {
                    pattype = "W";
                }

                if (opt_2_1.Checked == true)
                {
                    patsex = "M";
                }
                else if (opt_2_2.Checked == true)
                {
                    patsex = "F";
                }

                string sSQL = "";
                sSQL = "SELECT pif_id,pif_ic,pif_name FROM pat_info ";
                sSQL += "WHERE pif_name='" + txt_name.Text + "' AND pif_ic='" + txt_ic.Text + "' ";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count == 0) //新增
                {
                    try
                    {
                        sSQL = "INSERT INTO pat_info (pif_name, pif_ic, pif_mrn, pif_dob, pif_height, pif_pattyp, pif_sex, pif_address, pif_contactperson, pif_contact, ";
                        sSQL += "pif_insurance, pif_insid, pif_hpack, pif_hpack2, pif_hpack3, pif_docname, pif_createdate) ";
                        sSQL += "VALUES('" + txt_name.Text + "',";
                        sSQL += "'" + txt_ic.Text + "',";
                        sSQL += "'" + txt_mrn.Text + "',";
                        sSQL += "'" + _Get_YMD2(txt_dob.Text) + "',";
                        sSQL += "'" + txt_height.Text + "',";
                        sSQL += "'" + pattype + "',";
                        sSQL += "'" + patsex + "',";
                        sSQL += "'" + txt_address.Text + "',";
                        sSQL += "'" + txt_13.Text + "',";
                        sSQL += "'" + txt_15.Text + "',";
                        sSQL += "'" + ComboBox_ins.Text + "',"; //保险类别
                        sSQL += "'" + txt_insid.Text + "',"; //其它费用
                        sSQL += "'" + cbo_h_type.Text + "',";
                        sSQL += "'" + cbo_machine_model.Text + "',";
                        sSQL += "'" + cbo_hpack3.Text + "',";
                        sSQL += "'" + cbo_docname.Text + "',";
                        sSQL += "'" + _Get_YMD2(info_date.Text) + "'); ";
                        db.Excute(sSQL);

                        sSQL = "SELECT pif_id, pif_ic, pif_name FROM pat_info ";
                        sSQL += "WHERE pif_name='" + txt_name.Text + "' AND pif_ic='" + txt_ic.Text + "' ";
                        DataTable dt1 = db.Query(sSQL);
                        if (dt1.Rows.Count > 0)
                        {
                            _PAT_ID = dt1.Rows[0]["pif_id"].ToString();
                            _PAT_IC = dt1.Rows[0]["pif_ic"].ToString();
                            _PIF_NAME = dt1.Rows[0]["pif_name"].ToString();
                            sSQL = "INSERT INTO zinfo_f_011 (pat_id, info_date, info_USER, num_4, txt_5, txt_6, txt_7, txt_8, txt_10, txt_11, txt_14, txt_18) ";
                            sSQL += "VALUES('" + _PAT_ID + "',";
                            sSQL += "'" + _Get_YMD2(info_date.Text) + "',";
                            sSQL += "'" + Session["USER_ID"].ToString() + "',";
                            sSQL += "'" + num_4.Text + "',"; //年龄
                            sSQL += "'" + txt_5.Text + "',"; //民族
                            sSQL += "'" + txt_6.Text + "',"; //住院号
                            sSQL += "'" + txt_7.Text + "',"; //透析号
                            sSQL += "'" + txt_8.Text + "',"; //工作单位
                            sSQL += "'" + txt_10.Text + "',"; //联系电话
                            sSQL += "'" + txt_11.Text + "',"; //手机
                            sSQL += "'" + txt_14.Text + "',"; //家属关系
                            sSQL += "'" + txt_18.Text + "'); "; //其它费用
                            db.Excute(sSQL);
                        }

                        //血型...................................
                        string aids, syphilis, hbv, hcv, diabetic;
                        if (Checkbox_aids.Checked == true)
                            aids = "Y";
                        else
                            aids = "N";
                        if (Checkbox_syphilis.Checked == true)
                            syphilis = "Y";
                        else
                            syphilis = "N";
                        if (Checkbox_hbv.Checked == true)
                            hbv = "Y";
                        else
                            hbv = "N";
                        if (Checkbox_hcv.Checked == true)
                            hcv = "Y";
                        else
                            hcv = "N";
                        if (Checkbox_diabetic.Checked == true)
                            diabetic = "Y";
                        else
                            diabetic = "N";
                        sSQL = "INSERT INTO blood_group (bgrp_grp, bgrp_patic, bgrp_aids, bgrp_syphilis, bgrp_hbv, bgrp_hcv, bgrp_diabetic) ";
                        sSQL += "VALUES('" + ComboBox_grp.Text + "',";
                        sSQL += "'" + _PAT_IC + "',";
                        sSQL += "'" + aids + "',";
                        sSQL += "'" + syphilis + "',";
                        sSQL += "'" + hbv + "',";
                        sSQL += "'" + hcv + "',";
                        sSQL += "'" + diabetic + "');";
                        db.Excute(sSQL);
                        Common._NotificationShow("资料储存成功!");

                        _USER_NAME = cbo_docname.Text;
                        _PIF_SX = patsex;
                        Session["PAT_NAME"] = _PIF_NAME;
                        Session["PAT_IC"] = _PAT_IC;
                        pifid.Text = _PAT_ID;
                    }
                    catch (Exception ex)
                    {
                        Common._ErrorMsgShow(ex.Message.ToString());
                    }
                }
                else //異動更新
                {
                    _PAT_ID = dt.Rows[0]["pif_id"].ToString();
                    _PAT_IC = dt.Rows[0]["pif_ic"].ToString();
                    _PIF_NAME = dt.Rows[0]["pif_name"].ToString();
                    try
                    {
                        sSQL = "UPDATE pat_info ";
                        sSQL += "SET pif_name='" + txt_name.Text + "', ";
                        sSQL += "pif_ic='" + txt_ic.Text + "', "; //身分證號
                        sSQL += "pif_mrn='" + txt_mrn.Text + "', "; //IC卡號
                        sSQL += "pif_dob='" + _Get_YMD2(txt_dob.Text) + "', "; //出生日期
                        sSQL += "pif_pattyp='" + pattype + "', ";
                        sSQL += "pif_sex='" + patsex + "', ";
                        sSQL += "pif_address='" + txt_address.Text + "', "; //住址
                        sSQL += "pif_contactperson = '" + txt_13.Text + "', ";
                        sSQL += "pif_contact='" + txt_15.Text + "', ";
                        sSQL += "pif_insurance='" + ComboBox_ins.Text + "', ";
                        sSQL += "pif_insid='" + txt_insid.Text + "', "; //醫保號
                        sSQL += "pif_hpack='" + cbo_h_type.Text + "', "; //
                        sSQL += "pif_hpack2='" + cbo_machine_model.Text + "', "; //
                        sSQL += "pif_hpack3='" + cbo_hpack3.Text + "', "; //
                        sSQL += "pif_docname='" + cbo_docname.Text + "', ";
                        sSQL += "pif_createdate='" + _Get_YMD2(info_date.Text) + "' ";
                        sSQL += "WHERE pif_id='" + _PAT_ID + "'; ";

                        sSQL += "UPDATE zinfo_f_011 ";
                        sSQL += "SET info_date='" + _Get_YMD2(info_date.Text) + "', ";
                        sSQL += "info_USER='" + Session["USER_ID"].ToString() + "', ";
                        sSQL += "txt_5='" + txt_5.Text + "', "; //民族
                        sSQL += "txt_6='" + txt_6.Text + "', "; //住院号
                        sSQL += "txt_7='" + txt_7.Text + "', "; //透析号
                        sSQL += "txt_8='" + txt_8.Text + "', "; //工作单位
                        sSQL += "txt_10='" + txt_10.Text + "', "; //联系电话
                        sSQL += "txt_11='" + txt_11.Text + "', "; //手机
                        sSQL += "txt_14='" + txt_14.Text + "', "; //家属关系
                        sSQL += "txt_18='" + txt_18.Text + "' "; //其它费用
                        sSQL += "WHERE pat_id='" + _PAT_ID + "'; ";

                        string aids, syphilis, hbv, hcv, diabetic;
                        if (Checkbox_aids.Checked == true)
                            aids = "Y";
                        else
                            aids = "N";
                        if (Checkbox_syphilis.Checked == true)
                            syphilis = "Y";
                        else
                            syphilis = "N";
                        if (Checkbox_hbv.Checked == true)
                            hbv = "Y";
                        else
                            hbv = "N";
                        if (Checkbox_hcv.Checked == true)
                            hcv = "Y";
                        else
                            hcv = "N";
                        if (Checkbox_diabetic.Checked == true)
                            diabetic = "Y";
                        else
                            diabetic = "N";

                        sSQL += "UPDATE blood_group ";
                        sSQL += "SET bgrp_grp='" + ComboBox_grp.Text + "', ";
                        sSQL += "bgrp_aids='" + aids + "', ";
                        sSQL += "bgrp_syphilis='" + syphilis + "', "; 
                        sSQL += "bgrp_hbv='" + hbv + "', ";
                        sSQL += "bgrp_hcv='" + hcv + "', ";
                        sSQL += "bgrp_diabetic='" + diabetic + "' "; 
                        sSQL += "WHERE bgrp_patic='" + _PAT_IC + "'; ";

                        db.Excute(sSQL);
                        Common._NotificationShow("资料修改成功!");
                    }
                    catch (Exception ex)
                    {
                        _ErrorMsgShow(ex.Message.ToString());
                    }
                }
            }
        }

        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }

        protected void ChangeBirthday(object sender, DirectEventArgs e)
        {
            try
            {
                num_4.Text = (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(txt_dob.Text.Substring(0, 4))).ToString(); //年齡
            }
            catch (Exception)
            {
                num_4.Text = "";
            }
        }

        protected void Confirm(object sender, DirectEventArgs e)
        {
            //X.Msg.Alert("提示", "您确定要清除重置！").Show();
        }

        protected void BtnClear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
            pifid.Text = "";
        }

        protected void BtnSearch_Click(object sender, DirectEventArgs e)
        {
            PatInfo_Query();
        }

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

        #region Window1 历史病患
        protected void btnQuery_Click(object sender, DirectEventArgs e)
        {
            PatInfo_Query();
        }
        #endregion

        #region Window1 選擇病患
        protected void Dialysis_detail(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);
            _PAT_ID = selRow[0]["pat_id"].ToString();
            _PAT_IC = selRow[0]["pat_ic"].ToString();
            _PIF_NAME = selRow[0]["pif_name"].ToString();
            _USER_NAME = selRow[0]["pif_docname"].ToString();
            _PIF_SX = selRow[0]["pif_sex"].ToString() + "性";

            if (LoadPatinfo(_PAT_ID) == true)
            {
                Store istore = GridList.GetStore();
                istore.RemoveAll();
            }
            Window1.Close();
            pifid.Text = _PAT_ID;
        }
        #endregion

        protected void Win_Close(object sender, DirectEventArgs e)
        {
            Common._NotificationShow("关闭视窗</br>开始进行修改");
        }

        protected void ConfirmEdit(object sender, DirectEventArgs e)
        {
            X.Msg.Alert("提示", "已有此病患资料，现在载入修改？").Show();
        }
    }
}