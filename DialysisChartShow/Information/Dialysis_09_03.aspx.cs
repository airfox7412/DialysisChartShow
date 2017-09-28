using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_03 : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                show_grid();
            }
        }
        protected void show_grid()
        {
            string sql;
            sql = "SELECT DISTINCT DATE_FORMAT(a.dialysis_date, '%Y-%m-%d') ";
            sql += "from data_list a where a.person_id = '" + _PAT_IC + "' ";
            sql += "ORDER BY dialysis_date DESC";
            DataTable dt = db.Query(sql);
            Store istore = GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
        }
        protected void RowSelect(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Dialysis_date"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            string date = selRow[0]["Dialysis_date"].ToString();
            this.Panel1.Loader.SuspendScripting();
            this.Panel1.Loader.Url = "../report/Rpt_View.aspx?person_id=" + _PAT_IC + "&date=" + date + "&report=4";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
        }

        protected void RowSelect2(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Dialysis_date"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            string date = selRow[0]["Dialysis_date"].ToString();

            // 新增程式, 加入計算脫水量程式 - Added by Evan 20160620
            string cln1_col4 = string.Empty;
            //DataTable dt;
            DataTable dt0;
            string sql = "";
            //sql = "SELECT a.cln1_col8 FROM clinical1_nurse a ";
            //sql += "WHERE a.cln1_patic='" + _PAT_IC + "' ";
            //sql += "  AND a.cln1_diadate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            //sql += "  AND a.cln1_col8<>'' ";
            //dt = db.Query(sql);
            //if 
            sql = "SELECT cast(a.cln1_col5 as DECIMAL(6,1))-cast(a.cln1_col8 as DECIMAL(6,1)) column_2 FROM clinical1_nurse a ";
            sql += "WHERE a.cln1_patic='" + _PAT_IC + "' ";
            sql += "  AND a.cln1_diadate='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            sql += "  AND a.cln1_col5<>'' ";
            sql += "  AND a.cln1_col8<>'' ";
            sql += "  AND (a.cln1_col5 - a.cln1_col8) >=0"; //抓透析前體重減掉透析後且不小於零體重
            dt0 = db.Query(sql);
            if (dt0.Rows.Count > 0)
                cln1_col4 = dt0.Rows[0]["column_2"].ToString();//脫水
            else //沒有透析後體重則抓脫水最大值
            {
                sql = "SELECT a.column_2 FROM data_list a ";
                sql += "WHERE a.person_id = '" + _PAT_IC + "' ";
                sql += "  AND a.dialysis_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                sql += "  AND a.column_2<>'' ";
                sql += "ORDER BY a.dialysis_time DESC limit 1";//抓脫水不等於零最後一筆
                dt0 = db.Query(sql);
                if (dt0.Rows.Count != 0 && cln1_col4 == dt0.Rows[0]["column_2"].ToString())
                    cln1_col4 = dt0.Rows[0]["column_2"].ToString();//脫水
            }
            sql = "UPDATE clinical3_nurse SET ";
            sql += "  cln3_rmk = '" + cln1_col4 + "'";
            sql += " WHERE cln3_patic = '" + _PAT_IC + "' ";
            sql += "   AND cln3_date = '" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
            db.Excute(sql);
            //------------------------------------------------------
            this.Panel1.Loader.SuspendScripting();
            //this.Panel1.Loader.Url = "../report/Rpt_View.aspx?person_id=" + _PAT_IC + "&date=" + date + "&report=4";
            this.Panel1.Loader.Url = "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_IC + "&_INFO_DATE=" + date + "&_REPORT_NAME=12";
            this.Panel1.Loader.DisableCaching = true;
            this.Panel1.LoadContent();
            
        }
        public void getdate(string myMessage)
        {
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "错误信息",
                Message = myMessage,
                Width = 300,
                Buttons = MessageBox.Button.OK
            });
        }
        public void getdate2(string myMessage)
        {
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "错误信息",
                Message = myMessage,
                Width = 300,
                Buttons = MessageBox.Button.OK
            });
        }
    }
}