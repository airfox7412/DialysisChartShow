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
using System.Configuration;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_06_011 : BaseForm
    {
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //if (Hospital == "Hospital_Henan")
                //    txt_7.FieldLabel = "住院科室";
                SetComboBox();
                LoadPatinfo(_PAT_ID);
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
            if (txt_name.Text == "" || txt_ic.Text == "" ||
                ComboBox_ins.Text == "" ||
                txt_dob.Text == "" ||
                cbo_h_type.Text == "" ||
                cbo_machine_model.Text == "" ||
                cbo_hpack3.Text == "" ||
                cbo_docname.Text == "")
            {
                return false;
            }
            else
            {
                return true;
            }
        }

        protected void Btn_Save_Click(object sander, DirectEventArgs e)
        {
            if (CheckRequired() == false)
            {
                Common._ErrorMsgShow("请填写必要栏位(*)");
            }
            else
            {
                string pattype = "", patsex = "", marry = "";

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

                if (opt_3_1.Checked == true)
                {
                    marry = "已婚";
                }
                else if (opt_3_2.Checked == true)
                {
                    marry = "未婚";
                }

                string sSQL = "";
                sSQL = "SELECT pif_id,pif_ic,pif_name FROM pat_info ";
                sSQL += "WHERE pif_name='" + txt_name.Text + "' AND pif_ic='" + txt_ic.Text + "' ";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count > 0)
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
                        sSQL += "pif_height='" + txt_height.Text + "', "; //體重
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
                        sSQL += "txt_3='" + marry + "', "; //婚姻狀態
                        sSQL += "txt_5='" + txt_5.Text + "', "; //民族
                        sSQL += "txt_6='" + txt_6.Text + "', "; //住院号
                        sSQL += "txt_7='" + txt_7.Text + "', "; //透析号
                        sSQL += "txt_8='" + txt_8.Text + "', "; //工作单位
                        sSQL += "txt_9='" + txt_9.Text + "', "; //住院科室
                        sSQL += "txt_10='" + txt_10.Text + "', "; //联系电话
                        sSQL += "txt_11='" + txt_11.Text + "', "; //手机
                        sSQL += "txt_12='" + txt_12.Text + "', "; //邮编
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

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            //_ClearForm();
            LoadPatinfo(_PAT_ID);
        }

        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }

        //資料庫要建欄位4,5,6,7,8,10,11,14,16,18

        protected void Confirm(object sender, DirectEventArgs e)
        {
            //X.Msg.Alert("提示", "您确定要清除重置！").Show();
        }

        protected void ChangeBirthday(object sender, DirectEventArgs e)
        {
            num_4.Text = (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(txt_dob.Text.Substring(0, 4))).ToString(); //年齡
        }

        protected bool LoadPatinfo(string PAT_ID)
        {
            try
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
                    num_4.Text = (int.Parse(DateTime.Now.ToString("yyyy")) - int.Parse(dt.Rows[0]["pif_dob"].ToString().Substring(0, 4))).ToString();//年齡
                    txt_height.Text = dt.Rows[0]["pif_height"].ToString(); //體重
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

                    if (dt.Rows[0]["txt_3"].ToString() == "已婚") //婚姻状态
                    {
                        opt_3_1.Checked = true;
                        opt_3_2.Checked = false;
                    }
                    else if (dt.Rows[0]["txt_3"].ToString() == "未婚")
                    {
                        opt_3_2.Checked = true;
                        opt_3_1.Checked = false;
                    }
                    txt_address.Text = dt.Rows[0]["pif_address"].ToString(); //住址
                    txt_13.Text = dt.Rows[0]["pif_contactperson"].ToString();
                    txt_15.Text = dt.Rows[0]["pif_contact"].ToString();
                    txt_insid.Text = dt.Rows[0]["pif_insid"].ToString(); //醫保號
                    info_date.Text = dt.Rows[0]["pif_createdate"].ToString();

                    ComboBox_ins.Text = dt.Rows[0]["pif_insurance"].ToString();
                    cbo_h_type.Text = dt.Rows[0]["pif_hpack"].ToString();
                    cbo_machine_model.Text = dt.Rows[0]["pif_hpack2"].ToString();
                    cbo_hpack3.Text = dt.Rows[0]["pif_hpack3"].ToString();
                    cbo_docname.Text = dt.Rows[0]["pif_docname"].ToString();

                    //num_4.Text = dt.Rows[0]["num_4"].ToString();
                    txt_5.Text = dt.Rows[0]["txt_5"].ToString(); //民族
                    txt_6.Text = dt.Rows[0]["txt_6"].ToString(); //住院号
                    txt_7.Text = dt.Rows[0]["txt_7"].ToString(); //透析号
                    txt_8.Text = dt.Rows[0]["txt_8"].ToString(); //工作单位
                    txt_10.Text = dt.Rows[0]["txt_10"].ToString(); //联系电话
                    txt_11.Text = dt.Rows[0]["txt_11"].ToString(); //手机
                    txt_14.Text = dt.Rows[0]["txt_14"].ToString(); //家属关系
                    txt_18.Text = dt.Rows[0]["txt_18"].ToString(); //其它费用
                    txt_9.Text = dt.Rows[0]["txt_9"].ToString(); //住院科室
                    txt_12.Text = dt.Rows[0]["txt_12"].ToString(); //邮编

                    //Common.SetComboBox(ComboBox_grp, dt.Rows[0]["bgrp_grp"].ToString());
                    ComboBox_grp.Text = dt.Rows[0]["bgrp_grp"].ToString();
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
                }
                return true;
            }
            catch(Exception ex)
            {
                _ErrorMsgShow(ex.Message.ToString());
                return false;
            }
        }

        protected void QueryPatient(object sender, DirectEventArgs e)
        {
            String sSQL = "SELECT pif_id, pif_ic, pif_name, pif_sex, pif_docname FROM pat_info ";
            sSQL += "WHERE pif_name='" + txt_name.Text + "' ";
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
                LoadPatinfo(_PAT_ID);
            }
        }

        protected void QueryPatientIC(object sender, DirectEventArgs e)
        {
            String sSQL = "SELECT pif_id, pif_ic, pif_name, pif_sex, pif_docname FROM pat_info ";
            sSQL += "WHERE pif_ic='" + txt_ic.Text + "' ";
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
                LoadPatinfo(_PAT_ID);
            }
        }

        protected void BtnSave_Click(object sander, DirectEventArgs e)
        {
            if (CheckRequired() == false)
            {
                Common._ErrorMsgShow("请填写必要栏位(*)");
            }
            else
            {
                string pattype = "", patsex = "", marry = "";

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

                if (opt_3_1.Checked == true)
                {
                    marry = "已婚";
                }
                else if (opt_3_2.Checked == true)
                {
                    marry = "未婚";
                }


                string sSQL = "";
                sSQL = "SELECT * FROM zinfo_f_011 ";
                sSQL += "WHERE pat_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count == 0)
                {
                    sSQL = "INSERT INTO zinfo_f_011 (pat_id, info_date, info_user) ";
                    sSQL += "VALUES(";
                    sSQL += "'" + _PAT_ID + "',";
                    sSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";
                    sSQL += "'" + Session["USER_NAME"].ToString() + "')";
                    db.Excute(sSQL);
                }

                sSQL = "SELECT pif_id, pif_ic, pif_name FROM pat_info ";
                sSQL += "WHERE pif_id=" + _PAT_ID;
                dt = db.Query(sSQL);
                if (dt.Rows.Count > 0) //異動更新
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
                        sSQL += "pif_height='" + txt_height.Text + "', "; //體重
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
                        sSQL += "SET info_date='" + _Get_YMD2(info_date.Text) + "',";
                        sSQL += "info_USER='" + Session["USER_ID"].ToString() + "',";
                        sSQL += "txt_3='" + marry + "',"; //婚姻状态
                        sSQL += "num_4='" + num_4.Text + "',"; //婚姻状态
                        sSQL += "txt_5='" + txt_5.Text + "',"; //民族
                        sSQL += "txt_6='" + txt_6.Text + "',"; //住院号
                        sSQL += "txt_7='" + txt_7.Text + "',"; //透析号
                        sSQL += "txt_8='" + txt_8.Text + "',"; //工作单位
                        sSQL += "txt_9='" + txt_9.Text + "',"; //住院科室
                        sSQL += "txt_10='" + txt_10.Text + "',"; //联系电话
                        sSQL += "txt_11='" + txt_11.Text + "',"; //手机
                        sSQL += "txt_12='" + txt_12.Text + "',"; //邮编
                        sSQL += "txt_14='" + txt_14.Text + "',"; //家属关系
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
    }
}