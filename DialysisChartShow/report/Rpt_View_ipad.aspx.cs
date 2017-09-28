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

//using System.Web.Security;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;

namespace Dialysis_Chart_Show.report
{
    public partial class Rpt_View_ipad : System.Web.UI.Page
    {
        protected DBMysql db = new DBMysql();

        //protected void Page_Load(object sender, EventArgs e)
        //{

        //}
        string person_id, date, report;
        private void Page_Init(object sender, EventArgs e)
        {


            try
            {
                //person_id = Session["PERSON_ID"].ToString();
                //date = Session["DATE"].ToString();
                //report = Session["REPORT"].ToString();

                //if (person_id =="")
                //{
                person_id =  Request["PERSON_ID"].ToString();
                //}
                //if (date == "")
                //{
                date = Request["DATE"].ToString();
                //}
                //if (report =="")
                //{
                report =  Request["REPORT"].ToString();
                //}
                Label1.Text = Request["patient_name"].ToString();
                ReportDocument rpt = new ReportDocument();

                //int caseSwitch = 1;
                switch (report)
                {
                    case "1":
                        rpt.Load(Server.MapPath("dialysis.rpt"));
                        break;
                    case "2":
                        rpt.Load(Server.MapPath("data1.rpt"));
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }
                ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                pdv.Value = person_id;
                rpt.ParameterFields["person_id"].CurrentValues.Clear();
                rpt.ParameterFields["person_id"].DefaultValues.Clear();
                rpt.ParameterFields["person_id"].CurrentValues.Add(pdv);

                pdv.Value = date;
                rpt.ParameterFields["dialysis_date"].CurrentValues.Clear();
                rpt.ParameterFields["dialysis_date"].DefaultValues.Clear();
                rpt.ParameterFields["dialysis_date"].CurrentValues.Add(pdv);


                CRViewer.ReportSource = rpt;

                ConnectionInfo connInfo = new ConnectionInfo();

                //Server=192.168.1.118;Database=myhaisv3;UID=root;PWD=; CharSet=utf8
                //資料庫連線設定無效，阿亮是使用ODBC連線。
                string[] MySqlString = ConfigurationManager.ConnectionStrings["ApplicationServices"].ToString().Split(';');
                for (int i = 0; i < MySqlString.Length; i++)
                {
                    switch (MySqlString[i].ToUpper().Substring(0, 3))
                    {
                        case "SER":
                            connInfo.ServerName = MySqlString[i].Split('=')[1];
                            break;
                        case "DAT":
                            connInfo.DatabaseName = MySqlString[i].Split('=')[1];
                            break;
                        case "UID":
                            connInfo.UserID = MySqlString[i].Split('=')[1];
                            break;
                        case "PWD":
                            connInfo.Password = MySqlString[i].Split('=')[1];
                            break;
                    }
                }
                //connInfo. ServerName = "192.168.1.130";
                //connInfo. DatabaseName = "mysql";
                //connInfo. UserID = "root";
                //connInfo. Password = "";

                TableLogOnInfos tableLogOnInfos = CRViewer.LogOnInfo;

                foreach (TableLogOnInfo tableLogOnInfo in tableLogOnInfos)
                {
                    tableLogOnInfo.ConnectionInfo = connInfo;
                }
            }
            catch (Exception ex)
            {
                //_ErrorMsgShow(ex.Message.ToString());
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
                Response.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=B" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

        protected void Button5_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                Response.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=A" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                Response.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=C" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

        protected void Button3_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                Response.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=D" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

        protected void Button4_Click(object sender, EventArgs e)
        {
            if (CustomValidator1.IsValid && CustomValidator2.IsValid)
            {
                Response.Redirect("Rpt_View_ipad1.aspx?person_id=" + person_id + "&date1=" + TextBox1.Text + "&date2=" + TextBox2.Text + "&report=E" + "&patient_name=" + Label1.Text + "&ipad=2");
            }
        }

    }
}