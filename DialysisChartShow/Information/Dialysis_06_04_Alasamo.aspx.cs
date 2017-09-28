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
    public partial class Dialysis_06_04_Alasamo : BaseForm
    {
        private string _TableName = "zinfo_f_04_alasamo";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Show_zinfo_f04();
            }
        }

        protected void Show_zinfo_f04()
        {
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id=" + _PAT_ID;
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                try
                {
                    string check = dt.Rows[0]["txt_46"].ToString();
                    Radio r = this.RadioGroup46.Items.Cast<Radio>().First<Radio>(item => item.InputValue == check);
                    r.Checked = true;

                    check = dt.Rows[0]["txt_47"].ToString();
                    r = this.RadioGroup47.Items.Cast<Radio>().First<Radio>(item => item.InputValue == check);
                    r.Checked = true;
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
                txt_14.Text = dt.Rows[0]["txt_14"].ToString();
                txt_15.Text = dt.Rows[0]["txt_15"].ToString();
                txt_16.Text = dt.Rows[0]["txt_16"].ToString();
                txt_17.Text = dt.Rows[0]["txt_17"].ToString();
                txt_18.Text = dt.Rows[0]["txt_18"].ToString();
                txt_19.Text = dt.Rows[0]["txt_19"].ToString();
                txt_20.Text = dt.Rows[0]["txt_20"].ToString();
                txt_21.Text = dt.Rows[0]["txt_21"].ToString();
                txt_22.Text = dt.Rows[0]["txt_22"].ToString();
                txt_23.Text = dt.Rows[0]["txt_23"].ToString();
                txt_24.Text = dt.Rows[0]["txt_24"].ToString();
                txt_25.Text = dt.Rows[0]["txt_25"].ToString();
                txt_26.Text = dt.Rows[0]["txt_26"].ToString();
                txt_27.Text = dt.Rows[0]["txt_27"].ToString();
                txt_28.Text = dt.Rows[0]["txt_28"].ToString();
                txt_29.Text = dt.Rows[0]["txt_29"].ToString();
                txt_30.Text = dt.Rows[0]["txt_30"].ToString();
                txt_31.Text = dt.Rows[0]["txt_31"].ToString();
                txt_32.Text = dt.Rows[0]["txt_32"].ToString();
                txt_33.Text = dt.Rows[0]["txt_33"].ToString();
                txt_34.Text = dt.Rows[0]["txt_34"].ToString();
                txt_35.Text = dt.Rows[0]["txt_35"].ToString();
                txt_36.Text = dt.Rows[0]["txt_36"].ToString();
                txt_37.Text = dt.Rows[0]["txt_37"].ToString();
                txt_38.Text = dt.Rows[0]["txt_38"].ToString();
                txt_39.Text = dt.Rows[0]["txt_39"].ToString();
                txt_40.Text = dt.Rows[0]["txt_40"].ToString();
                txt_41.Text = dt.Rows[0]["txt_41"].ToString();
                txt_42.Text = dt.Rows[0]["txt_42"].ToString();
                txt_43.Text = dt.Rows[0]["txt_43"].ToString();
                txt_44.Text = dt.Rows[0]["txt_44"].ToString();
                txt_45.Text = dt.Rows[0]["txt_45"].ToString();
                //txt_46.Text = dt.Rows[0]["txt_46"].ToString();
                //txt_47.Text = dt.Rows[0]["txt_47"].ToString();
                txt_48.Text = dt.Rows[0]["txt_48"].ToString();
                txt_49.Text = dt.Rows[0]["txt_49"].ToString();
                txt_50.Text = dt.Rows[0]["txt_50"].ToString();
                txt_51.Text = dt.Rows[0]["txt_51"].ToString();
                txt_60.Text = dt.Rows[0]["txt_60"].ToString();
                txt_61.Text = dt.Rows[0]["txt_61"].ToString();
                txt_62.Text = dt.Rows[0]["txt_62"].ToString();
                txt_63.Text = dt.Rows[0]["txt_63"].ToString();
                txt_64.Text = dt.Rows[0]["txt_64"].ToString();
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {

            string checktext46 = "";
            string checktext47 = "";
            try
            {
                checktext46 = RadioGroup46.CheckedItems[0].InputValue;
                checktext47 = RadioGroup47.CheckedItems[0].InputValue;
            }
            catch (Exception ex)
            {
            }

            string info_date = DateTime.Now.ToString("yyyy-MM-dd");
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id=" + _PAT_ID;
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count == 0)
            {
                sSQL = "INSERT INTO " + _TableName + " (pat_id, info_date, info_user, ";
                sSQL += "txt_1, txt_2, txt_3, txt_4, txt_5, txt_6, txt_7, txt_8, txt_9, ";
                sSQL += "txt_10, txt_11, txt_12, txt_13, txt_14, txt_15, txt_16, txt_17, txt_18, txt_19, ";
                sSQL += "txt_20, txt_21, txt_22, txt_23, txt_24, txt_25, txt_26, txt_27, txt_28, txt_29, ";
                sSQL += "txt_30, txt_31, txt_32, txt_33, txt_34, txt_35, txt_36, txt_37, txt_38, txt_39, ";
                sSQL += "txt_40, txt_41, txt_42, txt_43, txt_44, txt_45, txt_46, txt_47, txt_48, txt_49, ";
                sSQL += "txt_50, txt_51, ";
                sSQL += "txt_60, txt_61, txt_62, txt_63, txt_64) ";
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
                sSQL += "'" + txt_14.Text + "',";
                sSQL += "'" + txt_15.Text + "',";
                sSQL += "'" + txt_16.Text + "',";
                sSQL += "'" + txt_17.Text + "',";
                sSQL += "'" + txt_18.Text + "',";
                sSQL += "'" + txt_19.Text + "',";
                sSQL += "'" + txt_20.Text + "',";
                sSQL += "'" + txt_21.Text + "',";
                sSQL += "'" + txt_22.Text + "',";
                sSQL += "'" + txt_23.Text + "',";
                sSQL += "'" + txt_24.Text + "',";
                sSQL += "'" + txt_25.Text + "',";
                sSQL += "'" + txt_26.Text + "',";
                sSQL += "'" + txt_27.Text + "',";
                sSQL += "'" + txt_28.Text + "',";
                sSQL += "'" + txt_29.Text + "',";
                sSQL += "'" + txt_30.Text + "',";
                sSQL += "'" + txt_31.Text + "',";
                sSQL += "'" + txt_32.Text + "',";
                sSQL += "'" + txt_33.Text + "',";
                sSQL += "'" + txt_34.Text + "',";
                sSQL += "'" + txt_35.Text + "',";
                sSQL += "'" + txt_36.Text + "',";
                sSQL += "'" + txt_37.Text + "',";
                sSQL += "'" + txt_38.Text + "',";
                sSQL += "'" + txt_39.Text + "',";
                sSQL += "'" + txt_40.Text + "',";
                sSQL += "'" + txt_41.Text + "',";
                sSQL += "'" + txt_42.Text + "',";
                sSQL += "'" + txt_43.Text + "',";
                sSQL += "'" + txt_44.Text + "',";
                sSQL += "'" + txt_45.Text + "',";
                sSQL += "'" + checktext46 + "',";
                sSQL += "'" + checktext47 + "',";
                sSQL += "'" + txt_48.Text + "',";
                sSQL += "'" + txt_49.Text + "',";
                sSQL += "'" + txt_50.Text + "',";
                sSQL += "'" + txt_51.Text + "',";
                sSQL += "'" + txt_60.Text + "',";
                sSQL += "'" + txt_61.Text + "',";
                sSQL += "'" + txt_62.Text + "',";
                sSQL += "'" + txt_63.Text + "',";
                sSQL += "'" + txt_64.Text + "') ";
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
                sSQL += "txt_14='" + txt_14.Text + "',";
                sSQL += "txt_15='" + txt_15.Text + "',";
                sSQL += "txt_16='" + txt_16.Text + "',";
                sSQL += "txt_17='" + txt_17.Text + "',";
                sSQL += "txt_18='" + txt_18.Text + "',";
                sSQL += "txt_19='" + txt_19.Text + "',";
                sSQL += "txt_20='" + txt_20.Text + "',";
                sSQL += "txt_21='" + txt_21.Text + "',";
                sSQL += "txt_22='" + txt_22.Text + "',";
                sSQL += "txt_23='" + txt_23.Text + "',";
                sSQL += "txt_24='" + txt_24.Text + "',";
                sSQL += "txt_25='" + txt_25.Text + "',";
                sSQL += "txt_26='" + txt_26.Text + "',";
                sSQL += "txt_27='" + txt_27.Text + "',";
                sSQL += "txt_28='" + txt_28.Text + "',";
                sSQL += "txt_29='" + txt_29.Text + "',";
                sSQL += "txt_30='" + txt_30.Text + "',";
                sSQL += "txt_31='" + txt_31.Text + "',";
                sSQL += "txt_32='" + txt_32.Text + "',";
                sSQL += "txt_33='" + txt_33.Text + "',";
                sSQL += "txt_34='" + txt_34.Text + "',";
                sSQL += "txt_35='" + txt_35.Text + "',";
                sSQL += "txt_36='" + txt_36.Text + "',";
                sSQL += "txt_37='" + txt_37.Text + "',";
                sSQL += "txt_38='" + txt_38.Text + "',";
                sSQL += "txt_39='" + txt_39.Text + "',";
                sSQL += "txt_30='" + txt_40.Text + "',";
                sSQL += "txt_41='" + txt_41.Text + "',";
                sSQL += "txt_42='" + txt_42.Text + "',";
                sSQL += "txt_43='" + txt_43.Text + "',";
                sSQL += "txt_44='" + txt_44.Text + "',";
                sSQL += "txt_45='" + txt_45.Text + "',";
                sSQL += "txt_46='" + checktext46 + "',";
                sSQL += "txt_47='" + checktext47 + "',";
                sSQL += "txt_48='" + txt_48.Text + "',";
                sSQL += "txt_49='" + txt_49.Text + "',";
                sSQL += "txt_50='" + txt_50.Text + "',";
                sSQL += "txt_51='" + txt_51.Text + "' ";
                sSQL += "txt_60='" + txt_60.Text + "',";
                sSQL += "txt_61='" + txt_61.Text + "',";
                sSQL += "txt_62='" + txt_62.Text + "',";
                sSQL += "txt_63='" + txt_63.Text + "',";
                sSQL += "txt_64='" + txt_64.Text + "' ";
                sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            }
            db.Excute(sSQL);
            _NotificationShow("存盘成功!");
        }
    }
}