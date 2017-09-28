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

namespace Dialysis_Chart_Show.report
{
    public partial class Rpt_View : BaseForm
    {
        string person_id, date, report, date1, date2, date3, date4;
        private void Page_Init(object sender, EventArgs e)
        {
            string sRPT_LOGO = ""; //Server.MapPath("../Styles/上海中山医院512.jpg");
            DataTable dtLOGO = db.Query("SELECT *  FROM general_setup WHERE  genst_code='RPT_LOGO'");
            if (dtLOGO.Rows.Count > 0)
            {
                sRPT_LOGO = dtLOGO.Rows[0]["genst_desc"].ToString();
                sRPT_LOGO = Server.MapPath(sRPT_LOGO);
            }
            string sRPT_NAME = ""; 
            DataTable dtNAME = db.Query("SELECT *  FROM general_setup WHERE  genst_code='RPT_NAME'");
            if (dtNAME.Rows.Count > 0)
            {
                sRPT_NAME = dtNAME.Rows[0]["genst_desc"].ToString();
            }

            try
            {
                person_id = Request["PERSON_ID"].ToString();
                date = Request["DATE"].ToString();
                report = Request["REPORT"].ToString();

                if(report == "3")
                {
                    date1 = Request["DATE1"].ToString();
                    date2 = Request["DATE2"].ToString();
                    date3 = Request["DATE3"].ToString();
                    date4 = Request["DATE4"].ToString();
                }

                ReportDocument rpt = new ReportDocument();

                switch (report)
                {
                    case "1":
                        rpt.Load(Server.MapPath("dialysis.rpt"));
                        break;
                    case "2":
                        if (!this.IsdoData22HDFRpt(person_id, date))
                            this.doData22Rpt(person_id, date);
                        else
                            this.doData22HDFRpt(person_id, date);
                        break;
                    case "3":
                        rpt.Load(Server.MapPath("report.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";  
                        break;
                    case "4":
                        rpt.Load(Server.MapPath("dialysis_report2.rpt"));                       
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";  
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

                if (report == "3")
                {
                    pdv.Value = date1;
                    rpt.ParameterFields["dialysis_datef012"].CurrentValues.Clear();
                    rpt.ParameterFields["dialysis_datef012"].DefaultValues.Clear();
                    rpt.ParameterFields["dialysis_datef012"].CurrentValues.Add(pdv);

                    pdv.Value = date2;
                    rpt.ParameterFields["dialysis_datef02"].CurrentValues.Clear();
                    rpt.ParameterFields["dialysis_datef02"].DefaultValues.Clear();
                    rpt.ParameterFields["dialysis_datef02"].CurrentValues.Add(pdv);

                    pdv.Value = date3;
                    rpt.ParameterFields["dialysis_datef04"].CurrentValues.Clear();
                    rpt.ParameterFields["dialysis_datef04"].DefaultValues.Clear();
                    rpt.ParameterFields["dialysis_datef04"].CurrentValues.Add(pdv);

                    pdv.Value = date4;
                    rpt.ParameterFields["dialysis_datef05"].CurrentValues.Clear();
                    rpt.ParameterFields["dialysis_datef05"].DefaultValues.Clear();
                    rpt.ParameterFields["dialysis_datef05"].CurrentValues.Add(pdv);

                }

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
                TableLogOnInfos tableLogOnInfos = CRViewer.LogOnInfo;
                
                foreach (TableLogOnInfo tableLogOnInfo in tableLogOnInfos)
                {
                    tableLogOnInfo.ConnectionInfo = connInfo;
                }

                
                
            }
            catch (Exception ex)
            {
                _ErrorMsgShow(ex.Message.ToString());
            }
        }

        protected void doData1Rpt()
        {
            string person_id, date, report;
            person_id = Request["PERSON_ID"].ToString();
            date = Request["DATE"].ToString();
            report = Request["REPORT"].ToString();

            DataSet1.Data2_HDataTable dtH = new DataSet1.Data2_HDataTable();
            DataSet1.Data2_DDataTable dtD = new DataSet1.Data2_DDataTable();
            DataRow dr;
            DataSet ds = new DataSet();

            string sqlcmd = "";
            sqlcmd = sqlcmd + "select c.pif_name ,a.person_id ,a.floor_no,a.bed_no,  DATE_FORMAT(a.dialysis_date,'%Y-%m-%d') as dialysis_date,   a.dialysis_time,a.column_1,a.column_2,a.column_3,a.column_4,a.column_5,a.column_6,a.column_7,";
            sqlcmd = sqlcmd + " a.column_8 ,a.column_9 ,a.column_10,a.column_41,b.maxtime from data_list a,";
            sqlcmd = sqlcmd + " (select person_id,floor_no,bed_no,dialysis_date,max(dialysis_time) as maxtime from data_list ";
            sqlcmd = sqlcmd + " group by person_id,floor_no,bed_no,dialysis_date) b, pat_info c ";
            sqlcmd = sqlcmd + " where a.person_id= b.person_id ";
            sqlcmd = sqlcmd + " and a.floor_no=b.floor_no ";
            sqlcmd = sqlcmd + " and a.bed_no=b.bed_no ";
            sqlcmd = sqlcmd + " and a.dialysis_date = b.dialysis_date ";
            sqlcmd = sqlcmd + " and a.dialysis_time = b.maxtime ";
            sqlcmd = sqlcmd + " and a.person_id = c.pif_ic ";
            sqlcmd = sqlcmd + " and  a.person_id ='" + person_id + "' and  a.dialysis_date ='" + date + "' ";


            DataTable dt_H = db.Query(sqlcmd);

            for (int i = 0; i < dt_H.Rows.Count; i++)
            {
                dr = dtH.NewRow();
                dr.ItemArray = dt_H.Rows[i].ItemArray;
                dtH.Rows.Add(dr);
            }

            sqlcmd = " select c.pif_name , a.person_id ,a.floor_no,a.bed_no,a.dialysis_date,a.dialysis_time,a.column_7,a.column_8,a.column_9,a.column_41, (hour(dialysis_time)*60 + minute(dialysis_time)) - b.col_min  Elapsedtime from data_list a, ";
            sqlcmd = sqlcmd + " (select person_id,floor_no,bed_no,dialysis_date,(hour(MIN(dialysis_time))*60 + MINUTE(MIN(dialysis_time))) as col_min from data_list  ";
            sqlcmd = sqlcmd + " group by person_id,floor_no,bed_no,dialysis_date) b ,pat_info c";
            sqlcmd = sqlcmd + " where a.person_id= b.person_id ";
            sqlcmd = sqlcmd + " and a.floor_no=b.floor_no";
            sqlcmd = sqlcmd + " and a.bed_no=b.bed_no";
            sqlcmd = sqlcmd + " and a.dialysis_date = b.dialysis_date";
            sqlcmd = sqlcmd + " and a.person_id = c.pif_ic";
            sqlcmd = sqlcmd + " and  a.person_id ='" + person_id + "' and  a.dialysis_date ='" + date + "' ";
            sqlcmd = sqlcmd + " order by a.dialysis_date,a.dialysis_time";

            DataTable dt_D = db.Query(sqlcmd);
            for (int i = 0; i < dt_D.Rows.Count; i++)
            {
                dr = dtD.NewRow();
                dr.ItemArray = dt_D.Rows[i].ItemArray;
                dtD.Rows.Add(dr);
            }

            ds.Tables.Add(dtH);
            ds.Tables.Add(dtD);

            ReportDocument objRpt = new ReportDocument();
            //objRpt.Load(Server.MapPath("data1.rpt"));
            objRpt.Load(Server.MapPath("data1.rpt"));

            //以下順序一定要對，先令報表中參數都為靜態，就是拋轉但報表不接
            //參數都在sqlcmd兜好一切
            objRpt.SetDataSource(ds);
            //只塞第二個DataTable就好
            //objRpt.SetDataSource(dt_D);

            CRViewer.ReportSource = objRpt;
            objRpt.SetParameterValue(0, person_id.ToString());

            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = date;
            objRpt.ParameterFields["dialysis_date"].CurrentValues.Clear();
            objRpt.ParameterFields["dialysis_date"].DefaultValues.Clear();
            objRpt.ParameterFields["dialysis_date"].CurrentValues.Add(pdv);
        }

        protected void doData22Rpt(string person_id, string date)
        {
            DataSet1.Data2_HDataTable dtH = new DataSet1.Data2_HDataTable();
            DataSet1.Data2_DDataTable dtD = new DataSet1.Data2_DDataTable();
            DataRow dr;
            DataSet ds = new DataSet();
            string sqlcmd = null;
            sqlcmd = "select c.pif_name, a.person_id, a.floor_no, a.bed_no, DATE_FORMAT(a.dialysis_date,'%Y-%m-%d') AS dialysis_date, a.dialysis_time, " +
                               "a.column_1, a.column_2, a.column_3, a.column_4, a.column_5, " +
                               "a.column_6, a.column_7, a.column_8, a.column_9, a.column_10, a.column_41, b.maxtime " +
                          "from data_list a, " +
                              "(select person_id, floor_no, bed_no, dialysis_date, max(dialysis_time) as maxtime " +                              
                              " from data_list " + 
                              " where person_id = '" + person_id + "' " +
                           "group by person_id, floor_no, bed_no, dialysis_date) b, pat_info c " +
                           "where a.person_id=b.person_id " +
                           "and a.floor_no=b.floor_no " +
                           "and a.bed_no=b.bed_no " +
                           "and a.dialysis_date=b.dialysis_date " +
                           "and a.dialysis_time=b.maxtime " +
                           "and a.person_id=c.pif_ic " +
                           "and a.person_id='" + person_id + "' " +
                           "and a.dialysis_date='" + date + "' ";


            DataTable dt_H = db.Query(sqlcmd);

            for (int i = 0; i < dt_H.Rows.Count; i++)
            {
                dr = dtH.NewRow();
                dr.ItemArray = dt_H.Rows[i].ItemArray;
                dtH.Rows.Add(dr);
            }
            sqlcmd = null;
            sqlcmd = "select c.pif_name, a.person_id, a.floor_no, a.bed_no, DATE_FORMAT(a.dialysis_date,'%Y-%m-%d') AS dialysis_date, a.dialysis_time, " +
                                 "a.column_7, a.column_8, a.column_9, a.column_41, " +
                                 "(hour(dialysis_time)*60 + minute(dialysis_time)) - b.col_min Elapsedtime " +
                            "from data_list a, " +
                                "(select person_id, floor_no, bed_no, dialysis_date, " +
                                       "(hour(MIN(dialysis_time))*60 + MINUTE(MIN(dialysis_time))) as col_min " +
                                   "from data_list " +

                                   " where person_id = '" + person_id + "' " +

                                  "group by person_id, floor_no, bed_no, dialysis_date) b, pat_info c " +
                           "where a.person_id=b.person_id " +
                             "and a.floor_no=b.floor_no " +
                             "and a.bed_no=b.bed_no " +
                             "and a.dialysis_date=b.dialysis_date " +
                             "and a.person_id=c.pif_ic " +
                             "and a.person_id='" + person_id + "' " +
                             "and a.dialysis_date='" + date + "' " +
                           "order by a.dialysis_date, a.dialysis_time ";
            DataTable dt_D = db.Query(sqlcmd);
            for (int i = 0; i < dt_D.Rows.Count; i++)
            {
                dr = dtD.NewRow();
                dr.ItemArray = dt_D.Rows[i].ItemArray;
                dtD.Rows.Add(dr);
            }
            ds.Tables.Add(dtH);
            ds.Tables.Add(dtD);

            ReportDocument objRpt = new ReportDocument();
            objRpt.Load(Server.MapPath("data22.rpt"));

            //以下順序一定要對，先令報表中參數都為靜態，就是拋轉但報表不接
            //參數都在sqlcmd兜好一切
            objRpt.SetDataSource(ds);
            //只塞第二個DataTable就好
            //objRpt.SetDataSource(dt_D);
            CRViewer.ReportSource = objRpt;
        }


        protected void doData22HDFRpt(string person_id, string date)
        {
            DataSet1.Data2_HDataTable dtH = new DataSet1.Data2_HDataTable();
            DataSet1.Data2_DDataTable dtD = new DataSet1.Data2_DDataTable();
            DataRow dr;
            DataSet ds = new DataSet();
            string sqlcmd = null;
            sqlcmd = "select c.pif_name, a.person_id, a.floor_no, a.bed_no, DATE_FORMAT(a.dialysis_date,'%Y-%m-%d') AS dialysis_date, a.dialysis_time, " +
                               "a.column_1, a.column_2, a.column_3, a.column_4, a.column_5, " +
                               "a.column_6, a.column_7, a.column_8, a.column_9, a.column_10, a.column_41, b.maxtime,a.column_47,a.column_48,a.column_49 " +
                          "from data_list a, " +
                              "(select person_id, floor_no, bed_no, dialysis_date, max(dialysis_time) as maxtime " +
                              " from data_list " +
                              " where person_id = '" + person_id + "' " +
                              " group by person_id, floor_no, bed_no, dialysis_date) b, pat_info c " +
                         "where a.person_id=b.person_id " +
                           "and a.floor_no=b.floor_no " +
                           "and a.bed_no=b.bed_no " +
                           "and a.dialysis_date=b.dialysis_date " +
                           "and a.dialysis_time=b.maxtime " +
                           "and a.person_id=c.pif_ic " +
                           "and a.person_id='" + person_id + "' " +
                           "and a.dialysis_date='" + date + "' ";


            DataTable dt_H = db.Query(sqlcmd);

            for (int i = 0; i < dt_H.Rows.Count; i++)
            {
                dr = dtH.NewRow();
                dr.ItemArray = dt_H.Rows[i].ItemArray;
                dtH.Rows.Add(dr);
            }
            sqlcmd = null;
            sqlcmd = "select c.pif_name, a.person_id, a.floor_no, a.bed_no, DATE_FORMAT(a.dialysis_date,'%Y-%m-%d') AS dialysis_date, a.dialysis_time, " +
                                 "a.column_7, a.column_8, a.column_9, a.column_41, " +
                                 "(hour(dialysis_time)*60 + minute(dialysis_time)) - b.col_min Elapsedtime " +
                            "from data_list a, " +
                                "(select person_id, floor_no, bed_no, dialysis_date, " +
                                       "(hour(MIN(dialysis_time))*60 + MINUTE(MIN(dialysis_time))) as col_min " +
                                   "from data_list " +

                                   " where person_id = '" + person_id + "' " +

                                  " group by person_id, floor_no, bed_no, dialysis_date) b, pat_info c " +
                           "where a.person_id=b.person_id " +
                             "and a.floor_no=b.floor_no " +
                             "and a.bed_no=b.bed_no " +
                             "and a.dialysis_date=b.dialysis_date " +
                             "and a.person_id=c.pif_ic " +
                             "and a.person_id='" + person_id + "' " +
                             "and a.dialysis_date='" + date + "' " +
                           "order by a.dialysis_date, a.dialysis_time ";


            DataTable dt_D = db.Query(sqlcmd);
            for (int i = 0; i < dt_D.Rows.Count; i++)
            {
                dr = dtD.NewRow();
                dr.ItemArray = dt_D.Rows[i].ItemArray;
                dtD.Rows.Add(dr);
            }

            ds.Tables.Add(dtH);
            ds.Tables.Add(dtD);

            ReportDocument objRpt = new ReportDocument();
            objRpt.Load(Server.MapPath("data22HDF.rpt"));

            //以下順序一定要對，先令報表中參數都為靜態，就是拋轉但報表不接
            //參數都在sqlcmd兜好一切
            objRpt.SetDataSource(ds);
            //只塞第二個DataTable就好
            CRViewer.ReportSource = objRpt; 
        }

        protected Boolean IsdoData22HDFRpt(string person_id, string date)
        {
            String sdateofWeek = DateTime.Parse(date).DayOfWeek.ToString();
            string sSQL = "select * from  appointment_setup ";
            sSQL += "WHERE apptst_patic='" + person_id + "' AND apptst_daytyp='" + sdateofWeek + "' AND apptst_mactyp='HDF'  ";
            DataTable dtHDF = db.Query(sSQL);
            if (dtHDF.Rows.Count > 0)
                return true;
            else
                return false;
        }
    }
}