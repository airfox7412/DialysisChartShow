using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_06_01 : BaseForm
    {
        private string _TableName = "zinfo_f_01";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                _zInfo_Show(_TableName, _PAT_ID, info_date.Text);

                string sql = "SELECT a.pif_name,a.pif_sex,a.pif_dob,"+DateTime.Now.ToString("yyyy")+"-year(a.pif_dob) age,a.pif_address,a.pif_ic,a.pif_contactperson,a.pif_contact,a.pif_insurance ";
                sql += " from pat_info a";
                sql += " where  a.pif_id = '" + _PAT_ID + "' ";
                DataTable dt = db.Query(sql);

                if (dt.Rows.Count == 0)
                    return;
                txt_1.Text = dt.Rows[0]["pif_name"].ToString();//姓名
                if (dt.Rows[0]["pif_sex"].ToString() == "M")
                {
                    opt_2_1.Checked = true;
                }
                else if (dt.Rows[0]["pif_sex"].ToString() == "F")
                {
                    opt_2_2.Checked = true;
                }
                txt_3.Text = dt.Rows[0]["pif_dob"].ToString();//出生年月
                if (dt.Rows[0]["age"].ToString() == "")
                {
                }
                else
                {
                    num_4.Text = dt.Rows[0]["age"].ToString();//年齡
                }
                txt_9.Text = dt.Rows[0]["pif_address"].ToString();//家庭住址
                txt_13.Text = dt.Rows[0]["pif_contactperson"].ToString();//家屬姓名
                txt_15.Text = dt.Rows[0]["pif_contact"].ToString();//家屬電話
                if (dt.Rows[0]["pif_insurance"].ToString() == "00000")
                {
                    opt_17_1.Checked = true;
                }
                else if (dt.Rows[0]["pif_insurance"].ToString() == "00001")
                {
                    opt_17_2.Checked = true;
                }
                else if (dt.Rows[0]["pif_insurance"].ToString() == "00002")
                {
                    opt_17_3.Checked = true;
                }
                else if (dt.Rows[0]["pif_insurance"].ToString() == "00003")
                {
                    opt_17_4.Checked = true;
                }
                else if (dt.Rows[0]["pif_insurance"].ToString() == "00004")
                {
                    opt_17_5.Checked = true;
                }
                else if (dt.Rows[0]["pif_insurance"].ToString() == "00005")
                {
                    opt_17_6.Checked = true;
                }
                txt_12.Text = dt.Rows[0]["pif_ic"].ToString();//身分证号码
                txt_12.ReadOnly = true;
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            try
            {
                _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
                //X.Redirect("Dialysis_06_01.aspx?editmode=list");

                string sql = "update pat_info ";
                sql += " set pif_name = '" + txt_1.Text.Replace("'", "''") + "' ,";
                if (opt_2_1.Checked == true)
                {
                    sql += " pif_sex = 'M' ,";
                }
                else if (opt_2_2.Checked == true)
                {
                    sql += " pif_sex = 'F' ,";
                }
                sql += " pif_dob = '" + txt_3.RawText.Replace("'", "''") + "' ,";
                sql += " pif_address = '" + txt_9.Text.Replace("'", "''") + "' ,";
                sql += " pif_ic='" + txt_12.Text.Replace("'", "''") + "' ,";
                sql += " pif_contactperson = '" + txt_13.Text.Replace("'", "''") + "' ,";
                sql += " pif_contact = '" + txt_15.Text.Replace("'", "''") + "' ,";
                if (opt_17_1.Checked == true)
                {
                    sql += " pif_insurance = '00000' ";
                }
                else if (opt_17_2.Checked == true)
                {
                    sql += " pif_insurance = '00001' ";
                }
                else if (opt_17_3.Checked == true)
                {
                    sql += " pif_insurance = '00002' ";
                }
                else if (opt_17_4.Checked == true)
                {
                    sql += " pif_insurance = '00003' ";
                }
                else if (opt_17_5.Checked == true)
                {
                    sql += " pif_insurance = '00004' ";
                }
                else if (opt_17_6.Checked == true)
                {
                    sql += " pif_insurance = '00005' ";
                }

                sql += " where pif_id = '" + _PAT_ID + "' ";
                db.Excute(sql);
            }
            catch (Exception ex)
            {
                 _ErrorMsgShow(ex.Message.ToString());
            }
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }

    }
}