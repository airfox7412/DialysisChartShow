using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_02_detail_Editor : System.Web.UI.UserControl
    {
        DBMysql db = new DBMysql();
 
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                ReadDataList();
            }
        }

        public void ReadDataList()
        {
            string sql1 = "SELECT dialysis_time, column_7, column_6, column_2, column_3, column_10, column_8, column_4 FROM data_list ";
            sql1 += "WHERE person_id='" + PationID.Text + "' ";
            sql1 += "AND dialysis_date='" + DialysisDate.Text + "' ";
            sql1 += "AND dialysis_time='" + OldTime.Text + "'";
            DataTable dt = db.Query(sql1);
            if (dt.Rows.Count > 0)
            {
                diagno.Text = dt.Rows[0]["column_7"].ToString();
                Column4.Text = dt.Rows[0]["column_6"].ToString();
                Column2.Text = dt.Rows[0]["column_2"].ToString();
                Column23.Text = dt.Rows[0]["column_3"].ToString();
                Column3.Text = dt.Rows[0]["column_10"].ToString();
                Column1.Text = dt.Rows[0]["column_8"].ToString();
                Column5.Text = dt.Rows[0]["column_4"].ToString();
            }
            dt.Clear();

            string sql2 = "SELECT cln2_date, cln2_time, cln2_t, cln2_p, cln2_r, cln2_bp, cln2_rmk, cln2_user, cln2_dateadded FROM clinical2_nurse ";
            sql2 += "WHERE cln2_patic='" + PationID.Text + "' ";
            sql2 += "AND cln2_date='" + DialysisDate.Text + "' ";
            sql2 += "AND cln2_time='" + OldTime.Text + "'";
            dt = db.Query(sql2);
            if (dt.Rows.Count > 0)
            {
                Column6.Text = dt.Rows[0]["cln2_t"].ToString();
                Column7.Text = dt.Rows[0]["cln2_p"].ToString();
                Column8.Text = dt.Rows[0]["cln2_r"].ToString();
                Column9.Text = dt.Rows[0]["cln2_bp"].ToString();
                Column10.Text = dt.Rows[0]["cln2_rmk"].ToString();
                Column11.Text = dt.Rows[0]["cln2_user"].ToString();
                //Column12.Text = dt.Rows[0]["cln2_dateadded"].ToString();
            }
            diagno.SelectText();
            diagno.Focus();
        }

        public Store GridStore { get; set; }

        public void Show()
        {
            this.DetailsWindow.Show();
        }

        public void SetDataList(string person_id, string dialysis_date, string dialysis_time)
        {
            PationID.Text = person_id;
            DialysisDate.Text = dialysis_date;
            if (dialysis_time != null)
            {
                DialysisTime.Text = dialysis_time;
                OldTime.Text = dialysis_time;
            }

            diagno.Text = "";
            Column4.Text = "";
            Column2.Text = "";
            Column3.Text = "";
            Column1.Text = "";
            Column5.Text = "";
            Column6.Text = "";
            Column7.Text = "";
            Column8.Text = "";
            Column9.Text = "";
            Column10.Text = "";
            Column11.Text = "";
            ReadDataList();
        }

        protected void SaveDataList(object sender, DirectEventArgs e)
        {
            string PationID = e.ExtraParams["PationID"];
            string DialysisDate = e.ExtraParams["DialysisDate"];
            string DialysisTime = e.ExtraParams["DialysisTime"];
            string sql1 = "";
            string sql0 = "SELECT dialysis_time, column_7, column_6, column_2, column_3, column_10, column_8, column_4 FROM data_list ";
            sql0 += "WHERE person_id='" + PationID + "' ";
            sql0 += "AND dialysis_date='" + DialysisDate + "' ";
            sql0 += "AND dialysis_time='" + OldTime.Text + "'";
            DataTable dt0 = db.Query(sql0);
            if (dt0.Rows.Count > 0)
            {
                sql1 += "UPDATE data_list SET ";
                sql1 += "dialysis_time='" + DialysisTime + "', ";
                sql1 += "column_7='" + diagno.Text + "', ";
                sql1 += "column_6='" + Column4.Text + "', ";
                sql1 += "column_2='" + Column2.Text + "', ";
                sql1 += "column_3='" + Column23.Text + "', ";
                sql1 += "column_10='" + Column3.Text + "', ";
                sql1 += "column_8='" + Column1.Text + "', ";
                sql1 += "column_4='" + Column5.Text + "' ";
                sql1 += "WHERE person_id='" + PationID + "' ";
                sql1 += "AND dialysis_date='" + DialysisDate + "' ";
                sql1 += "AND dialysis_time='" + OldTime.Text + "'; ";
            }
            else
            {
                dt0.Clear();
                sql0 = "SELECT pv_floor, pv_bedno FROM pat_visit ";
                sql0 += "WHERE pv_ic='" + PationID + "' ";
                sql0 += "AND pv_datevisit='" + DialysisDate + "' ";
                dt0 = db.Query(sql0);
                string pv_floor = dt0.Rows[0]["pv_floor"].ToString();
                string pv_bedno = dt0.Rows[0]["pv_bedno"].ToString();

                sql1 += "INSERT INTO data_list ";
                sql1 += "(person_id, dialysis_date, dialysis_time, floor_no, bed_no, column_7, column_6, column_2, column_10, column_8, column_4) ";
                sql1 += "VALUES('" + PationID + "','" + DialysisDate + "','" + DialysisTime + "','" + pv_floor + "','" + pv_bedno + "','";
                sql1 += diagno.Text + "','" + Column4.Text + "','" + Column2.Text + "','";
                sql1 += Column3.Text + "','" + Column1.Text + "','" + Column5.Text + "'); ";
            }

            sql0 = "SELECT cln2_date, cln2_time, cln2_t, cln2_p, cln2_r, cln2_bp, cln2_rmk, cln2_user, cln2_dateadded FROM clinical2_nurse ";
            sql0 += "WHERE cln2_patic='" + PationID + "' ";
            sql0 += "AND cln2_date='" + DialysisDate + "' ";
            sql0 += "AND cln2_time='" + OldTime.Text + "'";
            dt0 = db.Query(sql0);
            if (dt0.Rows.Count > 0)
            {
                sql1 += "UPDATE clinical2_nurse SET ";
                sql1 += "cln2_time='" + DialysisTime + "', ";
                sql1 += "cln2_t='" + Column6.Text + "', ";
                sql1 += "cln2_p='" + Column7.Text + "', ";
                sql1 += "cln2_r='" + Column8.Text + "', ";
                sql1 += "cln2_bp='" + Column9.Text + "', ";
                sql1 += "cln2_rmk='" + Column10.Text + "', ";
                sql1 += "cln2_user='" + Column11.Text + "' ";
                sql1 += "WHERE cln2_patic='" + PationID + "' ";
                sql1 += "AND cln2_date='" + DialysisDate + "' ";
                sql1 += "AND cln2_time='" + OldTime.Text + "';";
            }
            else
            {
                sql1 += "INSERT INTO clinical2_nurse ";
                sql1 += "(cln2_patic, cln2_date, cln2_time, cln2_t, cln2_p, cln2_r, cln2_bp, cln2_rmk, cln2_user) ";
                sql1 += "VALUES('" + PationID + "','" + DialysisDate + "','" + DialysisTime + "','";
                sql1 += Column6.Text + "','" + Column7.Text + "','" + Column8.Text + "','" + Column9.Text + "','";
                sql1 += Column10.Text + "','" + Column11.Text + "')";
            }
            db.Excute(sql1);

            this.DetailsWindow.Hide();
            GridStore.Reload();
        }

        protected void BtnDel_Click(object sender, DirectEventArgs e)
        {
            Session["PationID"] = e.ExtraParams["PationID"];
            Session["DialysisDate"] = e.ExtraParams["DialysisDate"];
            Session["DialysisTime"] = e.ExtraParams["DialysisTime"];
            X.Msg.Confirm("提示", "您确定要删除明细么？", new JFunction("DeleteData.Activate(result);", "result")).Show();
        }

        [DirectMethod(Namespace = "DeleteData")]
        public void Activate(string result)
        {
            if (result == "yes")
            {
                string sql;
                sql = "DELETE FROM data_list ";
                sql += "WHERE person_id='" + Session["PationID"].ToString() + "' ";
                sql += "AND dialysis_date='" + Session["DialysisDate"].ToString() + "' ";
                sql += "AND dialysis_time='" + Session["dialysis_time"].ToString() + "'; ";

                sql += "DELETE FROM clinical2_nurse ";
                sql += "WHERE cln2_patic='" + Session["PationID"].ToString() + "' ";
                sql += "AND cln2_date='" + Session["DialysisDate"].ToString() + "' ";
                sql += "AND cln2_time='" + Session["dialysis_time"].ToString() + "'; ";

                db.Excute(sql);
                X.Msg.Notify("提示", "已经删除明细").Show();
            }
        }
    }
}