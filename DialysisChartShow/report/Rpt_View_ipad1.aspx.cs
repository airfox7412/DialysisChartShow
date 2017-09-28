using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Data.SqlClient;
using System.Collections;

namespace Dialysis_Chart_Show.report
{
    public partial class Rpt_View_ipad1 : System.Web.UI.Page
    {
        
        string person_id, date1,date2, report,ipad;
        protected void Page_Load(object sender, EventArgs e)
        {
            
            try
            {
                ipad = Request["ipad"].ToString();
            }
            catch
            { }
            if (ipad == null)
            {
                btn1.Visible = false;
                btn2.Visible = false;
            }
            
        }
        private void Page_Init(object sender, EventArgs e)
        {
            try
            {
                person_id = Request["PERSON_ID"].ToString();
                date1 = Request["DATE1"].ToString();
                date2 = Request["DATE2"].ToString();
                report = Request["REPORT"].ToString();
                Label1.Text = Request["patient_name"].ToString();
                ReportDocument rpt = new ReportDocument();

                switch (report)
                {
                    case "A":
                        //rpt.Load(Server.MapPath("All report.rpt"));
                        this.doAllRpr();
                        break;
                    case "B":
                        rpt.Load(Server.MapPath("Ap report.rpt"));
                        break;
                    case "C":
                        rpt.Load(Server.MapPath("Dp report.rpt"));
                        break;
                    case "D":
                        rpt.Load(Server.MapPath("Dc report.rpt"));
                        break;
                    case "E":
                        rpt.Load(Server.MapPath("Vp report.rpt"));
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

                //給日期起訖
                rpt.ParameterFields["dialysis_date"].CurrentValues.Clear();
                rpt.ParameterFields["dialysis_date"].DefaultValues.Clear();
                rpt.ParameterFields["dialysis_date"].CurrentValues.AddRange(DateTime.Parse(date1), DateTime.Parse(date2), RangeBoundType.BoundInclusive, RangeBoundType.BoundInclusive);


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
 
            }
        }
        protected void doAllRpr()
        {
          
            string person_id, date1, date2, report, patient_name;
            person_id = Request["PERSON_ID"].ToString();
            date1 = Request["DATE1"].ToString();
            date2 = Request["DATE2"].ToString();
            report = Request["REPORT"].ToString();
            patient_name = Request["PATIENT_NAME"].ToString();

            string sqlcmd = "";
            sqlcmd = sqlcmd + " select c.pif_name , a.person_id ,a.floor_no,a.bed_no,a.dialysis_date,a.dialysis_time,a.column_7,a.column_8,a.column_9,a.column_41, (hour(dialysis_time)*60 + minute(dialysis_time)) - b.col_min  Elapsedtime from data_list a, ";
            sqlcmd = sqlcmd + " (select person_id,floor_no,bed_no,dialysis_date,(hour(MIN(dialysis_time))*60 + MINUTE(MIN(dialysis_time))) as col_min from data_list ";
            sqlcmd = sqlcmd + " group by person_id,floor_no,bed_no,dialysis_date) b ,pat_info c ";
            sqlcmd = sqlcmd + " where a.person_id= b.person_id ";
            sqlcmd = sqlcmd + " and a.floor_no=b.floor_no ";
            sqlcmd = sqlcmd + " and a.bed_no=b.bed_no ";
            sqlcmd = sqlcmd + " and a.dialysis_date = b.dialysis_date ";
            sqlcmd = sqlcmd + " and a.person_id = c.pif_ic ";
            sqlcmd = sqlcmd + " and  a.person_id ='" + person_id + "' ";
            sqlcmd = sqlcmd + " order by a.dialysis_date,a.dialysis_time ";


            DBMysql db = new DBMysql();
            DataTable dt = db.Query(sqlcmd);

            ReportDocument objRpt = new ReportDocument();
            objRpt.Load(Server.MapPath("All report.rpt"));

            //以下順序一定要對，先令報表中參數都為靜態，就是拋轉但報表不接
            //參數都在sqlcmd兜好一切
            objRpt.SetDataSource(dt);
            //CrystalReportViewer1.ReportSource = objRpt;
            CRViewer.ReportSource = objRpt;
            objRpt.SetParameterValue(0, person_id.ToString());

            //給日期起訖
            objRpt.ParameterFields["dialysis_date"].CurrentValues.Clear();
            objRpt.ParameterFields["dialysis_date"].DefaultValues.Clear();
            objRpt.ParameterFields["dialysis_date"].CurrentValues.AddRange(DateTime.Parse(date1), DateTime.Parse(date2), RangeBoundType.BoundInclusive, RangeBoundType.BoundInclusive);
            //塞入參數值後不要再refresh了
        }
    }
}