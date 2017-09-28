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
    public partial class Rpt_View_Dialysis : System.Web.UI.Page
    {
        private void Page_Init(object sender, EventArgs e)
        {
            try
            {
                DBMysql db = new DBMysql();

                string sRPT_LOGO = "";
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

                #region 統計分析-病患人數
                int W_COUNT = 0;
                int W_ZINFO07_COUNT = 0;
                DataTable dtPAT_INFO = db.Query("SELECT COUNT(*) AS COUNT FROM PAT_INFO ");
                if (dtPAT_INFO.Rows.Count > 0)
                {
                    W_COUNT = Convert.ToInt32(dtPAT_INFO.Rows[0]["COUNT"].ToString());
                }
                DataTable dtzinfo07 = db.Query("SELECT COUNT(*) as ZINFO07_COUNT FROM zinfo_a_07 WHERE opt_1 = '1' OR opt_1 = '2' OR opt_1 = '3' OR opt_1 = '4' ORDER BY PAT_ID");
                if (dtzinfo07.Rows.Count > 0)
                {
                    W_ZINFO07_COUNT = Convert.ToInt32(dtzinfo07.Rows[0]["ZINFO07_COUNT"].ToString());
                }
                int W_TOTAL = 0; //線上透析總人數
                W_TOTAL = W_COUNT - W_ZINFO07_COUNT; //減去死亡或退出
                string sqlCHKREG = "select genst_desc from general_setup where genst_ctg = 'statistics'" + " and genst_code = '001" + "'";
                DataTable dt = db.Query(sqlCHKREG);
                string sql = "";
                if (dt.Rows.Count == 0)
                {
                    sql = "INSERT into general_setup (genst_ctg, genst_code, genst_desc) VALUES('statistics', '001', '" + W_TOTAL + "') ";
                }
                else
                {
                    sql = "UPDATE  general_setup a set genst_desc='" + W_TOTAL + "' WHERE a.genst_ctg='statistics' AND a.genst_code='001'";
                }
                db.Excute(sql);
                #endregion

                ReportDocument rpt = new ReportDocument();
                rpt.Load(Server.MapPath("statistics.rpt"));
                rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";

                ParameterDiscreteValue pdv = new ParameterDiscreteValue();

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
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }
    }
}