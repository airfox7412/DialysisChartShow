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
    public partial class Dialysis_06_05_Alasamo : BaseForm
    {
        private string _TableName = "zinfo_f_05_alasamo";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
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
                are_1.Text = dt.Rows[0]["are_1"].ToString();
                txt_1.Text = dt.Rows[0]["txt_1"].ToString();
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            string info_date = DateTime.Now.ToString("yyyy-MM-dd");
            string sSQL = "SELECT * FROM " + _TableName + " ";
            sSQL += "WHERE pat_id='" + _PAT_ID + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count == 0)
            {
                sSQL = "INSERT INTO " + _TableName + " (pat_id, info_date, info_user, are_1, txt_1) ";
                sSQL += "VALUES('" + _PAT_ID + "',";
                sSQL += "'" + info_date + "',";
                sSQL += "'" + Session["USER_NAME"].ToString() + "',";
                sSQL += "'" + are_1.Text + "',";
                sSQL += "'" + txt_1.Text + "')";
            }
            else
            {
                sSQL = "UPDATE " + _TableName + " SET ";
                sSQL += "are_1='" + are_1.Text + "',";
                sSQL += "txt_1='" + txt_1.Text + "' ";
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