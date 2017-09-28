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

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_06_012_Alasamo : BaseForm
    {
        private string _TableName = "zinfo_f_012_alasamo";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                ShowMedicalRecord();
            }
        }

        protected void ShowMedicalRecord()
        {
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id=" + _PAT_ID;
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                txt_1d.Text = dt.Rows[0]["txt_1"].ToString().Substring(0, 10);
                txt_1t.Text = dt.Rows[0]["txt_1"].ToString().Substring(11, 5);
                txt_2.Text = dt.Rows[0]["txt_2"].ToString();
                try
                {
                    string[] check = dt.Rows[0]["txt_3"].ToString().Split(new Char[] { '|' });
                    for (int i = 0; i < check.Length; i++)
                    {
                        if (check[i] == "慢性肾炎")
                        {
                            txt_3_1.Checked = true;
                        }
                        if (check[i] == "慢性肾盂肾炎")
                        {
                            txt_3_2.Checked = true;
                        }
                        if (check[i] == "慢性间质性肾炎")
                        {
                            txt_3_3.Checked = true;
                        }
                        if (check[i] == "梗阻性肾病")
                        {
                            txt_3_4.Checked = true;
                        }
                        if (check[i] == "RPGNI-I-III-IV-V")
                        {
                            txt_3_5.Checked = true;
                        }
                        if (check[i] == "多囊肾")
                        {
                            txt_3_6.Checked = true;
                        }
                        if (check[i] == "原发性小血管炎")
                        {
                            txt_3_7.Checked = true;
                        }
                        if (check[i] == "SLE")
                        {
                            txt_3_8.Checked = true;
                        }
                        if (check[i] == "SS")
                        {
                            txt_3_9.Checked = true;
                        }
                        if (check[i] == "高血压肾损害")
                        {
                            txt_3_10.Checked = true;
                        }
                        if (check[i] == "糖尿病肾病")
                        {
                            txt_3_11.Checked = true;
                        }
                        if (check[i] == "不详")
                        {
                            txt_3_12.Checked = true;
                        }
                        if (check[i] == "其他")
                        {
                            txt_3_13.Checked = true;
                        }
                    }
                }
                catch (Exception)
                {
                }

                txt_3c.Text = dt.Rows[0]["txt_3c"].ToString();
                txt_4.Text = dt.Rows[0]["txt_4"].ToString();
                txt_5.Text = dt.Rows[0]["txt_5"].ToString();
                txt_6.Text = dt.Rows[0]["txt_6"].ToString();
                txt_7.Text = dt.Rows[0]["txt_7"].ToString();
                txt_8.Text = dt.Rows[0]["txt_8"].ToString();
                txt_9.Text = dt.Rows[0]["txt_9"].ToString();
                //txt_10.Text = dt.Rows[0]["txt_10"].ToString();
                txt_11.Text = dt.Rows[0]["txt_11"].ToString();
                txt_12.Text = dt.Rows[0]["txt_12"].ToString();
                txt_13.Text = dt.Rows[0]["txt_13"].ToString();
                txt_14.Text = dt.Rows[0]["txt_14"].ToString();
                txt_15.Text = dt.Rows[0]["txt_15"].ToString();
                txt_20.Text = dt.Rows[0]["txt_20"].ToString();
                txt_21.Text = dt.Rows[0]["txt_21"].ToString();
                txt_22.Text = dt.Rows[0]["txt_22"].ToString();
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            string checktext = "";
            try
            {
                if (txt_3_1.Checked == true)
                {
                    checktext += "慢性肾炎|";
                }
                if (txt_3_2.Checked == true)
                {
                    checktext += "慢性肾盂肾炎|";
                }
                if (txt_3_3.Checked == true)
                {
                    checktext += "慢性间质性肾炎|";
                }
                if (txt_3_4.Checked == true)
                {
                    checktext += "梗阻性肾病|";
                }
                if (txt_3_5.Checked == true)
                {
                    checktext += "RPGNI-I-III-IV-V|";
                }
                if (txt_3_6.Checked == true)
                {
                    checktext += "多囊肾|";
                }
                if (txt_3_7.Checked == true)
                {
                    checktext += "原发性小血管炎|";
                }
                if (txt_3_8.Checked == true)
                {
                    checktext += "SLE|";
                }
                if (txt_3_9.Checked == true)
                {
                    checktext += "SS|";
                }
                if (txt_3_10.Checked == true)
                {
                    checktext += "高血压肾损害|";
                }
                if (txt_3_11.Checked == true)
                {
                    checktext += "糖尿病肾病|";
                }
                if (txt_3_12.Checked == true)
                {
                    checktext += "不详|";
                }
                if (txt_3_13.Checked == true)
                {
                    checktext += "其他";
                }
                if (txt_1t.Text == "")
                {
                    Common._ErrorMsgShow("请填写入院时间!");
                    return;
                }
            }
            catch (Exception)
            {
            }

            string info_date = DateTime.Now.ToString("yyyy-MM-dd");
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count == 0)
            {
                sSQL = "INSERT INTO " + _TableName + " (pat_id, info_date, info_user, txt_1, txt_2, txt_3, txt_4, txt_5, txt_6, txt_7, txt_8, ";
                //sSQL += "txt_9, txt_10, txt_11, txt_12, ";
                sSQL += "txt_13, txt_14, txt_15, txt_20, txt_21, txt_22) ";
                sSQL += "VALUES('" + _PAT_ID + "',";
                sSQL += "'" + info_date + "',";
                sSQL += "'" + Session["USER_NAME"].ToString() + "',";
                sSQL += "'" + _Get_YMD2(txt_1d.Text) + " " + txt_1t.Text + "',";
                sSQL += "'" + txt_2.Text + "',";
                sSQL += "'" + checktext + "',";
                sSQL += "'" + txt_4.Text + "',";
                sSQL += "'" + txt_5.Text + "',";
                sSQL += "'" + txt_6.Text + "',";
                sSQL += "'" + txt_7.Text + "',";
                sSQL += "'" + txt_8.Text + "',";
                sSQL += "'" + txt_9.Text + "',";
                //sSQL += "'" + txt_10.Text + "',";
                sSQL += "'" + txt_11.Text + "',";
                sSQL += "'" + txt_12.Text + "',";
                sSQL += "'" + txt_13.Text + "',";
                sSQL += "'" + txt_14.Text + "',";
                sSQL += "'" + txt_15.Text + "',";
                sSQL += "'" + txt_20.Text + "', ";
                sSQL += "'" + txt_21.Text + "', ";
                sSQL += "'" + txt_22.Text + "') ";
            }
            else
            {
                sSQL = "UPDATE " + _TableName + " SET ";
                sSQL += "info_date='" + info_date + "',";
                sSQL += "info_user='" + Session["USER_NAME"].ToString() + "',";
                sSQL += "txt_1='" + _Get_YMD2(txt_1d.Text) + " " + txt_1t.Text + "',";
                sSQL += "txt_2='" + txt_2.Text + "',";
                sSQL += "txt_3='" + checktext + "',";
                sSQL += "txt_4='" + txt_4.Text + "',";
                sSQL += "txt_5='" + txt_5.Text + "',";
                sSQL += "txt_6='" + txt_6.Text + "',";
                sSQL += "txt_7='" + txt_7.Text + "',";
                sSQL += "txt_8='" + txt_8.Text + "',";
                sSQL += "txt_9='" + txt_9.Text + "',";
                //sSQL += "txt_10='" + txt_10.Text + "',";
                sSQL += "txt_11='" + txt_11.Text + "',";
                sSQL += "txt_12='" + txt_12.Text + "',";
                sSQL += "txt_13='" + txt_13.Text + "',";
                sSQL += "txt_14='" + txt_14.Text + "',";
                sSQL += "txt_15='" + txt_15.Text + "',";
                sSQL += "txt_20='" + txt_20.Text + "', ";
                sSQL += "txt_21='" + txt_21.Text + "', ";
                sSQL += "txt_22='" + txt_22.Text + "' ";
                sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            }
            db.Excute(sSQL);
            _NotificationShow("存盘成功!");
        }
    }
}