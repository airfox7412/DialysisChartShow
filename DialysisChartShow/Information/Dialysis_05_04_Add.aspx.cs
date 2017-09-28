using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using System.Net;
using System.IO;
using System.Text;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show.Information {

    public partial class Dialysis_05_04_Add : BaseForm
    {
        public string toDay = DateTime.Now.ToString("yyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {         
            if (!X.IsAjaxRequest)
            {
                if (Request.QueryString["sid"] != null)
                    sid.Text = Request.QueryString["sid"].ToString();
                if (Request.QueryString["pat_date"] != null)
                    PAT_DATE.Text = Request.QueryString["pat_date"].ToString();
                Show();
            }
        }

        protected void Show()
        {
            if (sid.Text != null)
            {
                string sql = "SELECT * FROM pat_patrol WHERE sid=" + sid.Text;
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    PAT_DATE.Text = dt.Rows[0]["pat_date"].ToString();
                    Doct_Name.Text = dt.Rows[0]["pat_emp"].ToString().Replace(",", "");
                    TextArea1.Text = dt.Rows[0]["pat_note"].ToString();
                }
            }
        }

        protected void Btn_save_Click(object sender, DirectEventArgs e)
        {
            string toTime = DateTime.Now.ToString("HH:mm:ss");
            string sql;
            if (sid.Text.Equals(string.Empty))
            {
                sql = "INSERT into pat_patrol(pif_ic, pat_date, pat_time, pat_note, pat_emp) ";
                sql += "VALUES('" + _PAT_IC + "','" + _Get_YMD(PAT_DATE.Text) + "','" + toTime + "','" + TextArea1.Text + "','" + Doct_Name.Text + "')";
            }
            else
            {
                sql = "UPDATE pat_patrol SET pat_note='" + TextArea1.Text + "', pat_emp='" + Doct_Name.Text + "', pat_date='" + _Get_YMD(PAT_DATE.Text) + "' ";
                sql += "WHERE sid=" + sid.Text;
            }
            try
            {
                db.Excute(sql);
                Common._NotificationShow("<font size=4>储存成功!</font>");
            }
            catch
            {
                Common._NotificationShow("<font size=4>储存失败!</font>");
            }
        }
    }
}