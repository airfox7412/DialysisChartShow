using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using System.Data;
using Dialysis_Chart_Show.tools;
using Ext.Net;

//using System.Web.Security;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;

namespace Dialysis_Chart_Show.report
{
    public partial class Rpt_View_ipad0 : System.Web.UI.Page
    {
        protected DBMysql db = new DBMysql();
        string person_id, date, report;

        private void Page_Init(object sender, EventArgs e)
        {
            try
            {
                person_id = Request["PERSON_ID"].ToString();
                date = Request["DATE"].ToString();
                report = Request["REPORT"].ToString();
                Label1.Text = Request["patient_name"].ToString();
                ReportDocument rpt = new ReportDocument();
                switch (report)
                {
                    case "1":
                        rpt.Load(Server.MapPath("dialysis.rpt"));
                        break;
                    case "2":
                        rpt.Load(Server.MapPath("data2.rpt"));
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
                DataSet1.Data2_HDataTable dtH = new DataSet1.Data2_HDataTable();
                DataSet1.Data2_DDataTable dtD = new DataSet1.Data2_DDataTable();
                DataRow dr;
                DataSet ds = new DataSet();
                if (report != "2")
                {
                    ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                    pdv.Value = person_id;
                    rpt.ParameterFields["person_id"].CurrentValues.Clear();
                    rpt.ParameterFields["person_id"].DefaultValues.Clear();
                    rpt.ParameterFields["person_id"].CurrentValues.Add(pdv);

                    pdv.Value = date;
                    rpt.ParameterFields["dialysis_date"].CurrentValues.Clear();
                    rpt.ParameterFields["dialysis_date"].DefaultValues.Clear();
                    rpt.ParameterFields["dialysis_date"].CurrentValues.Add(pdv);
                }
                else
                {
                    string sql = "";
                    sql = "select c.pif_name, a.person_id, a.floor_no, a.bed_no, DATE_FORMAT(a.dialysis_date,'%Y-%m-%d') AS dialysis_date, a.dialysis_time, " +
                                 "a.column_1, a.column_2, a.column_3, a.column_4, a.column_5, " +
                                 "a.column_6, a.column_7, a.column_8, a.column_9, a.column_10, a.column_41, b.maxtime " +
                            "from data_list a, " +
                                "(select person_id, floor_no, bed_no, dialysis_date, max(dialysis_time) as maxtime " +
                                   "from data_list group by person_id, floor_no, bed_no, dialysis_date) b, pat_info c " +
                           "where a.person_id=b.person_id " +
                             "and a.floor_no=b.floor_no " +
                             "and a.bed_no=b.bed_no " +
                             "and a.dialysis_date=b.dialysis_date " +
                             "and a.dialysis_time=b.maxtime " +
                             "and a.person_id=c.pif_ic " +
                             "and a.person_id='" + person_id + "' " +
                             "and a.dialysis_date='" + date + "' ";
                    DataTable dt_H = db.Query(sql);
                    for (int i = 0; i < dt_H.Rows.Count; i++)
                    {
                        dr = dtH.NewRow();
                        dr.ItemArray = dt_H.Rows[i].ItemArray;
                        dtH.Rows.Add(dr);
                    }

                    sql = "select c.pif_name, a.person_id, a.floor_no, a.bed_no, DATE_FORMAT(a.dialysis_date,'%Y-%m-%d') AS dialysis_date, a.dialysis_time, " +
                                 "a.column_7, a.column_8, a.column_9, a.column_41, " +
                                 "(hour(dialysis_time)*60 + minute(dialysis_time)) - b.col_min Elapsedtime " +
                            "from data_list a, " +
                                "(select person_id, floor_no, bed_no, dialysis_date, " +
                                       "(hour(MIN(dialysis_time))*60 + MINUTE(MIN(dialysis_time))) as col_min " +
                                   "from data_list " +
                                  "group by person_id, floor_no, bed_no, dialysis_date) b, pat_info c " +
                           "where a.person_id=b.person_id " +
                             "and a.floor_no=b.floor_no " +
                             "and a.bed_no=b.bed_no " +
                             "and a.dialysis_date=b.dialysis_date " +
                             "and a.person_id=c.pif_ic " +
                             "and a.person_id='" + person_id + "' " +
                             "and a.dialysis_date='" + date + "' " +
                           "order by a.dialysis_date, a.dialysis_time ";
                    DataTable dt_D = db.Query(sql);
                    for (int i = 0; i < dt_D.Rows.Count; i++)
                    {
                        dr = dtD.NewRow();
                        dr.ItemArray = dt_D.Rows[i].ItemArray;
                        dtD.Rows.Add(dr);
                    }

                    ds.Tables.Add(dtH);
                    ds.Tables.Add(dtD);
                    rpt.SetDataSource(ds);
                }
                CRViewer.ReportSource = rpt;

                //ConnectionInfo connInfo = new ConnectionInfo();
                //connInfo.ServerName = "192.168.1.130";
                //connInfo.DatabaseName = "mysql";
                //connInfo.UserID = "root";
                //connInfo.Password = "";

                //TableLogOnInfos tableLogOnInfos = CRViewer.LogOnInfo;

                //foreach (TableLogOnInfo tableLogOnInfo in tableLogOnInfos)
                //{
                //    tableLogOnInfo.ConnectionInfo = connInfo;
                //}
            }
            catch (Exception ex)
            {
                //_ErrorMsgShow(ex.Message.ToString());
                string s = ex.ToString();
            }
        }

        public string mess;
        public bool Valid;
        //驗證text為日期格式
        protected void CustomValidator1_ServerValidate(object source, ServerValidateEventArgs args)
        {
            test(args.Value,out mess,out Valid);
            CustomValidator1.ErrorMessage = mess;
            args.IsValid = Valid;
        }

        public void test(string text,out string mess,out bool Valid)
        {
            DateTime dt;
            if (string.IsNullOrEmpty(text))
            {
                mess = "該欄位必填";
                Valid = false;
            }
            else if (DateTime.TryParseExact(text, "yyyy-MM-dd", null, System.Globalization.DateTimeStyles.AllowWhiteSpaces, out dt) == false)
            {
                mess = "日期格式為 yyyy-MM-dd";
                Valid = false;
            }
            else
            {
                mess = "";
                Valid = true;
            }
        }

        protected void CustomValidator2_ServerValidate(object source, ServerValidateEventArgs args)
        {
            test(args.Value, out mess, out Valid);
            CustomValidator2.ErrorMessage = mess;
            args.IsValid = Valid;
        }

        protected void Button1_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                X.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=B" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                X.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=A" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                X.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=C" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                X.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=D" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                X.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=E" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

    }
}