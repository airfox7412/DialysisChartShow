using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using Ext.Net;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

namespace Dialysis_Chart_Show.Information {

    public partial class Dialysis_05_04 : BaseForm
    {
        public string toDay = DateTime.Now.ToString("yyyy-MM-dd");
        private string sel_info_date = string.Empty;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                start_date.Text = toDay.Substring(0, 8) + "01";
                end_date.Text = toDay;
                show_grid();
            }
        }

        protected void BtnQuery_Click(object sender, DirectEventArgs e)
        {
            show_grid();
        }

        protected void show_grid()
        {
            string sSQL = "SELECT * FROM pat_patrol ";
            //sSQL += "LEFT JOIN pat_info B ON A.pif_ic = B.pif_ic ";
            sSQL += "WHERE pif_ic='" + _PAT_IC + "' ";
            if (Doct_Name.Text == "")
            {
                sSQL += "AND pat_date>='" + _Get_YMD(start_date.Text) + "' AND pat_date<='" + _Get_YMD(end_date.Text) + "' ";
            }
            else
            {
                sSQL += "AND pat_emp like '%" + Doct_Name.Text + "%' ";
            }
            sSQL += "ORDER BY pat_date DESC";

            DataTable dt = db.Query(sSQL);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);            
            istore.DataBind();
        }

        protected void Delete_Click(object sender, DirectEventArgs e)
        {
            X.Msg.Confirm("删除", "<font size='4'>确定删除此笔资料?</font>",
                new MessageBoxButtonsConfig
                {
                    Yes = new MessageBoxButtonConfig
                    {
                        Handler = "Dialysis_05_04.DoDelDetail()",
                        Text = "确定"
                    },
                    No = new MessageBoxButtonConfig
                    {
                        Handler = "",
                        Text = "放弃"
                    }
                }).Show();
        }

        public void Do_Edit(object sender, DirectEventArgs e)
        {
            sid.Text = e.ExtraParams["sid"];
            string sSQL = "SELECT * FROM pat_patrol WHERE sid=" + sid.Text;
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                PAT_DATE2.Text = dt.Rows[0]["pat_date"].ToString();
                Doct_Name2.Text = dt.Rows[0]["pat_emp"].ToString().Replace(",", "");
                TextArea1.Text = dt.Rows[0]["pat_note"].ToString();
            }
            Window1.Show();
        }

        public void Do_Delete(object sender, DirectEventArgs e)
        {
            string sid = e.ExtraParams["sid"];
            string sSQL = "DELETE FROM pat_patrol WHERE sid=" + sid;
            db.Excute(sSQL);
            Common._NotificationShow("已经删除资料");
            show_grid();
        }

        protected void BtnAdd_Click(object sender, DirectEventArgs e)
        {
            sid.Text = "";
            PAT_DATE2.Text = _Get_YMD(end_date.Text);
            TextArea1.Text = "";
            Window1.Show();
        }
        
        protected void Btn_save_Click(object sender, DirectEventArgs e)
        {
            string toTime = DateTime.Now.ToString("HH:mm:ss");
            string sSQL;
            if (sid.Text.Equals(string.Empty))
            {
                sSQL = "INSERT into pat_patrol(pif_ic, pat_date, pat_time, pat_note, pat_emp) ";
                sSQL += "VALUES('" + _PAT_IC + "','" + _Get_YMD(PAT_DATE2.Text) + "','" + toTime + "','" + TextArea1.Text + "','" + Doct_Name2.Text + "')";
            }
            else
            {
                sSQL = "UPDATE pat_patrol SET pat_note='" + TextArea1.Text + "', pat_emp='" + Doct_Name2.Text + "', pat_date='" + _Get_YMD(PAT_DATE2.Text) + "' ";
                sSQL += "WHERE sid=" + sid.Text;
            }

            try
            {
                db.Excute(sSQL);
                Common._NotificationShow("<font size=4>储存成功!</font>");
            }
            catch
            {
                Common._NotificationShow("<font size=4>储存失败!</font>");
            }
            Window1.Close();            
            show_grid();
        }

        protected void BtnPrint_Click(object sender, DirectEventArgs e)
        {
            string Url = "../report/Report_Dialysis_h.aspx?_PAT_IC=" + _PAT_IC + "&_BEG_DATE=" + _Get_YMD(start_date.Text) + "&_END_DATE=" + _Get_YMD(end_date.Text) + "&_REPORT_NAME=100";
            PrintWindow.Show();
            PrintWindow.Loader.SuspendScripting();
            PrintWindow.Loader.Url = Url;
            PrintWindow.Loader.DisableCaching = true;
            PrintWindow.LoadContent();
        }
    }
}
