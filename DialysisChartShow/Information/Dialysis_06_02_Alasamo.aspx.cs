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
    public partial class Dialysis_06_02_Alasamo : BaseForm
    {
        private string _TableName = "zinfo_f_02_alasamo";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Show_zinfo_f02();
            }
        }

        protected void Show_zinfo_f02()
        {
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id=" + _PAT_ID;
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    string check = dt.Rows[0]["txt_14"].ToString();
                    Radio r = this.RadioGroup14.Items.Cast<Radio>().First<Radio>(item => item.InputValue == check);
                    r.Checked = true;

                    check = dt.Rows[0]["txt_15"].ToString();
                    r = this.RadioGroup15.Items.Cast<Radio>().First<Radio>(item => item.InputValue == check);
                    r.Checked = true;
                    string[] RadioItem = dt.Rows[0]["txt_16"].ToString().Split(new Char[] { '|' });
                    for (int i = 0; i < RadioItem.Length; i++)
                    {
                        if (RadioItem[i] == "1" || RadioItem[i] == "2")
                        {
                            r = this.RadioGroup16_1.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                        if (RadioItem[i] == "3" || RadioItem[i] == "4")
                        {
                            r = this.RadioGroup16_2.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                        if (RadioItem[i] == "5" || RadioItem[i] == "6")
                        {
                            r = this.RadioGroup16_3.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                    }

                    RadioItem = dt.Rows[0]["txt_17"].ToString().Split(new Char[] { '|' });
                    for (int i = 0; i < RadioItem.Length; i++)
                    {
                        if (RadioItem[i] == "1" || RadioItem[i] == "2")
                        {
                            r = this.RadioGroup17_1.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                        if (RadioItem[i] == "3" || RadioItem[i] == "4")
                        {
                            r = this.RadioGroup17_2.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                        if (RadioItem[i] == "5" || RadioItem[i] == "6")
                        {
                            r = this.RadioGroup17_3.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                    }

                    RadioItem = dt.Rows[0]["txt_18"].ToString().Split(new Char[] { '|' });
                    for (int i = 0; i < RadioItem.Length; i++)
                    {
                        if (RadioItem[i] == "1" || RadioItem[i] == "2")
                        {
                            r = this.RadioGroup18_1.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                        if (RadioItem[i] == "3" || RadioItem[i] == "4")
                        {
                            r = this.RadioGroup18_2.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                        if (RadioItem[i] == "5" || RadioItem[i] == "6")
                        {
                            r = this.RadioGroup18_3.Items.Cast<Radio>().First<Radio>(item => item.InputValue == RadioItem[i]);
                            r.Checked = true;
                        }
                    }
                }
                catch (Exception ex)
                {
                }

                txt_1.Text = dt.Rows[0]["txt_1"].ToString();
                txt_2.Text = dt.Rows[0]["txt_2"].ToString();
                txt_3.Text = dt.Rows[0]["txt_3"].ToString();
                txt_4.Text = dt.Rows[0]["txt_4"].ToString();
                txt_5.Text = dt.Rows[0]["txt_5"].ToString();
                txt_6.Text = dt.Rows[0]["txt_6"].ToString();
                txt_7.Text = dt.Rows[0]["txt_7"].ToString();
                txt_8.Text = dt.Rows[0]["txt_8"].ToString();
                txt_9.Text = dt.Rows[0]["txt_9"].ToString();
                txt_10.Text = dt.Rows[0]["txt_10"].ToString();
                txt_11.Text = dt.Rows[0]["txt_11"].ToString();
                txt_12.Text = dt.Rows[0]["txt_12"].ToString();
                txt_13.Text = dt.Rows[0]["txt_13"].ToString();
                //txt_14.Text = dt.Rows[0]["txt_14"].ToString();
                //txt_15.Text = dt.Rows[0]["txt_15"].ToString();
                //txt_16.Text = dt.Rows[0]["txt_16"].ToString();
                //txt_17.Text = dt.Rows[0]["txt_17"].ToString();
                //txt_18.Text = dt.Rows[0]["txt_18"].ToString();
                txt_20.Text = dt.Rows[0]["txt_20"].ToString();
                txt_21.Text = dt.Rows[0]["txt_21"].ToString();
                txt_22.Text = dt.Rows[0]["txt_22"].ToString();
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            string checktext14 = "";
            string checktext15 = "";
            string checktext16 = "";
            string checktext17 = "";
            string checktext18 = "";
            try
            {
                checktext14 = RadioGroup14.CheckedItems[0].InputValue;
                checktext15 = RadioGroup15.CheckedItems[0].InputValue;
                checktext16 = RadioGroup16_1.CheckedItems[0].InputValue + "|";
                checktext16 += RadioGroup16_2.CheckedItems[0].InputValue + "|";
                checktext16 += RadioGroup16_3.CheckedItems[0].InputValue + "|";
                checktext17 = RadioGroup17_1.CheckedItems[0].InputValue + "|";
                checktext17 += RadioGroup17_2.CheckedItems[0].InputValue + "|";
                checktext17 += RadioGroup17_3.CheckedItems[0].InputValue + "|";
                checktext18 = RadioGroup18_1.CheckedItems[0].InputValue + "|";
                checktext18 += RadioGroup18_2.CheckedItems[0].InputValue + "|";
                checktext18 += RadioGroup18_3.CheckedItems[0].InputValue + "|";
            }
            catch(Exception ex)
            {
            }
            string info_date = DateTime.Now.ToString("yyyy-MM-dd");
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count == 0)
            {
                sSQL = "INSERT INTO " + _TableName + " (pat_id, info_date, info_user, txt_1, txt_2, txt_3, txt_4, txt_5, txt_6, txt_7, txt_8, txt_9, ";
                sSQL += "txt_10, txt_11, txt_12, txt_13, txt_14, txt_15, txt_16, txt_17, txt_18, txt_20, txt_21, txt_22) ";
                sSQL += "VALUES('" + _PAT_ID + "',";
                sSQL += "'" + info_date + "',";
                sSQL += "'" + Session["USER_NAME"].ToString() + "',";
                sSQL += "'" + txt_1.Text + "',";
                sSQL += "'" + txt_2.Text + "',";
                sSQL += "'" + txt_3.Text + "',";
                sSQL += "'" + txt_4.Text + "',";
                sSQL += "'" + txt_5.Text + "',";
                sSQL += "'" + txt_6.Text + "',";
                sSQL += "'" + txt_7.Text + "',";
                sSQL += "'" + txt_8.Text + "',";
                sSQL += "'" + txt_9.Text + "',";
                sSQL += "'" + txt_10.Text + "',";
                sSQL += "'" + txt_11.Text + "',";
                sSQL += "'" + txt_12.Text + "',";
                sSQL += "'" + txt_13.Text + "',";
                sSQL += "'" + checktext14 + "',";
                sSQL += "'" + checktext15 + "',";
                sSQL += "'" + checktext16 + "',";
                sSQL += "'" + checktext17 + "',";
                sSQL += "'" + checktext18 + "',";
                sSQL += "'" + txt_20.Text + "',";
                sSQL += "'" + txt_21.Text + "',";
                sSQL += "'" + txt_22.Text + "') ";
            }
            else
            {
                sSQL = "UPDATE " + _TableName + " SET ";
                sSQL += "info_date='" + info_date + "',";
                sSQL += "info_user='" + Session["USER_NAME"].ToString() + "',";
                sSQL += "txt_1='" + txt_1.Text + "',";
                sSQL += "txt_2='" + txt_2.Text + "',";
                sSQL += "txt_3='" + txt_3.Text + "',";
                sSQL += "txt_4='" + txt_4.Text + "',";
                sSQL += "txt_5='" + txt_5.Text + "',";
                sSQL += "txt_6='" + txt_6.Text + "',";
                sSQL += "txt_7='" + txt_7.Text + "',";
                sSQL += "txt_8='" + txt_8.Text + "',";
                sSQL += "txt_9='" + txt_9.Text + "',";
                sSQL += "txt_10='" + txt_10.Text + "',";
                sSQL += "txt_11='" + txt_11.Text + "',";
                sSQL += "txt_12='" + txt_12.Text + "',";
                sSQL += "txt_13='" + txt_13.Text + "',";
                sSQL += "txt_14='" + checktext14 + "',";
                sSQL += "txt_15='" + checktext15 + "',";
                sSQL += "txt_16='" + checktext16 + "',";
                sSQL += "txt_17='" + checktext17 + "',";
                sSQL += "txt_18='" + checktext18 + "',";
                sSQL += "txt_20='" + txt_20.Text + "',";
                sSQL += "txt_21='" + txt_21.Text + "',";
                sSQL += "txt_22='" + txt_22.Text + "' ";
                sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            }
            db.Excute(sSQL);
            _NotificationShow("存盘成功!");
        }
    }
}