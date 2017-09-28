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
    public partial class Dialysis_06_05 : BaseForm
    {
        private string _TableName = "zinfo_f_05";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //_zInfo_Show(_TableName, _PAT_ID, info_date.Text);
                Show_zinfo_f05();
            }
        }

        protected void Show_zinfo_f05()
        {
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                info_date.Text = dt.Rows[0]["info_date"].ToString();
                txt_1.Text = dt.Rows[0]["txt_1"].ToString();
                //txt_2.Text = dt.Rows[0]["txt_2"].ToString();
                txt_3.Text = dt.Rows[0]["txt_3"].ToString();
                txt_4.Text = dt.Rows[0]["txt_4"].ToString();
                txt_5.Text = dt.Rows[0]["txt_5"].ToString();
                txt_6.Text = dt.Rows[0]["txt_6"].ToString();
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            //_zInfo_Save(_TableName, _PAT_ID, info_date.Text);

            string infodate = _Get_YMD(info_date.Text);
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count == 0)
            {
                sSQL = "INSERT INTO " + _TableName + " (pat_id, info_date, info_user, txt_1, txt_2, txt_3, txt_4, txt_5, txt_6) ";
                sSQL += "VALUES('" + _PAT_ID + "',";
                sSQL += "'" + infodate + "',";
                sSQL += "'" + Session["USER_NAME"].ToString() + "',";
                sSQL += "'" + txt_1.Text + "',";
                sSQL += "'',";
                sSQL += "'" + txt_3.Text + "',";
                sSQL += "'" + txt_4.Text + "',";
                sSQL += "'" + txt_5.Text + "',";
                sSQL += "'" + txt_6.Text + "') ";
            }
            else
            {
                sSQL = "UPDATE " + _TableName + " SET ";
                sSQL += "info_date='" + infodate + "',";
                sSQL += "txt_1='" + txt_1.Text + "',";
                sSQL += "txt_2='',";
                sSQL += "txt_3='" + txt_3.Text + "',";
                sSQL += "txt_4='" + txt_4.Text + "',";
                sSQL += "txt_5='" + txt_5.Text + "',";
                sSQL += "txt_6='" + txt_6.Text + "' ";
                sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            }
            db.Excute(sSQL);
            _NotificationShow("存盘成功!");
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