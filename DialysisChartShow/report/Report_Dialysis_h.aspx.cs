using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;
using Ext.Net;
using System.Data;
using Dialysis_Chart_Show.tools;
using NLog;

namespace Dialysis_Chart_Show.report
{
    public partial class Report_Dialysis_h : BaseForm //System.Web.UI.Page
    {
        ReportDocument rpt = new ReportDocument();

        private static Logger logger = LogManager.GetCurrentClassLogger();

        internal Dictionary<string, string> DataTable2Dict(DataTable dt) {
            EnumerableRowCollection<Dictionary<string, string>> c =  dt.AsEnumerable()
                .Select( // ...then iterate through the columns...
                    row => dt.Columns.Cast<DataColumn>().ToDictionary( // ...and find the key value pairs for the dictionary
                    column => column.ColumnName,    // Key
                    column => row[column] as string // Value
                )
            );

            IEnumerator<Dictionary<string, string>> ienum = c.GetEnumerator();

            if (ienum.MoveNext()) {
                return ienum.Current;
            }

            return null;
        }

        #region 關閉 ReportDocument，可解決報表Job上限數問題
        protected void Page_UnLoad(object sender, EventArgs e)
        {
            rpt.Close();
            rpt.Dispose();
        }
        #endregion

        protected void Page_Init(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string sPAT_ID = Request["_PAT_ID"] == null ? string.Empty : Request["_PAT_ID"].ToString();
                string sPAT_IC = Request["_PAT_IC"] == null ? string.Empty : Request["_PAT_IC"].ToString();
                string sINFO_DATE = Request["_INFO_DATE"] == null ? string.Empty : Request["_INFO_DATE"].ToString();
                string sREPORT_NAME = Request["_REPORT_NAME"] == null ? "1" : Request["_REPORT_NAME"].ToString();
                string sBEG_DATE = Request["_BEG_DATE"] == null ? string.Empty : Request["_BEG_DATE"].ToString();
                string sEND_DATE = Request["_END_DATE"] == null ? string.Empty : Request["_END_DATE"].ToString();
                string sREPORT_P = Request["_REPORT_P"] == null ? string.Empty : Request["_REPORT_P"].ToString();
                string sREPORT_YEAR = Request["_REPORT_sYEAR"] == null ? string.Empty : Request["_REPORT_sYEAR"].ToString();
                string sREPORT_QT = Request["_REPORT_sQT"] == null ? string.Empty : Request["_REPORT_sQT"].ToString();
                string sREPORT_QM = Request["_REPORT_sQM"] == null ? string.Empty : Request["_REPORT_sQM"].ToString();
                string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

                int i1PageCount = 16;
                if (int.TryParse(ConfigurationManager.AppSettings["1PageCount"].ToString(), out  i1PageCount))
                    i1PageCount = int.Parse(ConfigurationManager.AppSettings["1PageCount"].ToString());

                string sReport = ""; //報表檔名稱

                string sRPT_LOGO = ""; //Server.MapPath("../Styles/上海中山医院512.jpg");
                DataTable dtLOGO = db.Query("SELECT *  FROM general_setup WHERE  genst_code='RPT_LOGO'");
                if (dtLOGO.Rows.Count > 0)
                {
                    sRPT_LOGO = dtLOGO.Rows[0]["genst_desc"].ToString();
                    sRPT_LOGO = Server.MapPath(sRPT_LOGO);
                }
                string sRPT_NAME = ""; //"南京医科大学第二附属医院";
                DataTable dtNAME = db.Query("SELECT *  FROM general_setup WHERE  genst_code='RPT_NAME'");
                if (dtNAME.Rows.Count > 0)
                {
                    sRPT_NAME = dtNAME.Rows[0]["genst_desc"].ToString();
                }
                
                ConnectionInfo connInfo = new ConnectionInfo();
                //Server=192.168.1.118;Database=myhaisv3;UID=root;PWD=; CharSet=utf8
                //資料庫連線設定無效，阿亮是使用ODBC連線。 Alex說:這裡使用ODBC，不是用ADO暈倒
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

                switch (sREPORT_NAME)
                {
                    case "checkin":
                        rpt.Load(Server.MapPath("dialysis_report_checkin.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "weeksch":
                        rpt.Load(Server.MapPath("dialysis_report_weeksch.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "drug_term":
                        rpt.Load(Server.MapPath("dialysis_report_drug_term.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "HTest":   //H.血液透析化验数据表
                        rpt.Load(Server.MapPath("report_HTest.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "1":
                        rpt.Load(Server.MapPath("dialysis_report_h01.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "2":
                        rpt.Load(Server.MapPath("dialysis_report_h02.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "3":
                        rpt.Load(Server.MapPath("dialysis_report_h03.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "4":
                        rpt.Load(Server.MapPath("dialysis_report_h04.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "5":
                        rpt.Load(Server.MapPath("dialysis_report_age.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "6":
                        rpt.Load(Server.MapPath("dialysis_report_e01.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        string sPatDocName = _PatDocName;
                        rpt.DataDefinition.FormulaFields["DOC_NAME"].Text = "ToText('" + sPatDocName + "')";
                        break;
                    case "61":
                        rpt.Load(Server.MapPath("dialysis_report_e011.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "7":
                        rpt.Load(Server.MapPath("dialysis_report_cnt.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "8":
                        rpt.Load(Server.MapPath("dialysis_report_cntA.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "9":
                        rpt.Load(Server.MapPath("dialysis_report_err2.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "10":
                        rpt.Load(Server.MapPath("dialysis_report_age2.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "11":
                        rpt.Load(Server.MapPath("dialysis_report_dry.rpt"));
                        break;
                    case "122":
                        sReport = "dialysis_report_HebeiList.rpt";
                        rpt.Load(Server.MapPath(sReport));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "12":
                    case "121":
                        #region 依不同醫院，載入不同報表樣板
                        switch (Hospital)
                        {
                            case "Hospital_Suzhou":
                                sReport = "dialysis_report_PSuzhou.rpt";
                                break;
                            case "Hospital_117":
                                sReport = "dialysis_report_P117.rpt";
                                break;
                            case "Hospital_Henan":
                                sReport = "dialysis_report_PHenan.rpt";
                                break;
                            case "Hospital_Luyi":
                                sReport = "dialysis_report_PLuyi.rpt";
                                break;
                            case "Hospital_Hebei":
                                sReport = "dialysis_report_PHebei.rpt";
                                break;
                            case "Hospital_Alasamo":
                                sReport = "dialysis_report_PAlasamo.rpt";
                                break;                            
                            case "Hospital_Xian":
                                sReport = "dialysis_report_PXian.rpt";
                                break;
                            case "Standard":
                                sReport = "dialysis_report4.rpt";
                                break;
                            default:
                                sReport = "dialysis_report4.rpt";
                                break;
                        }
                        #endregion
                        rpt.Load(Server.MapPath(sReport));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "13":
                        sReport = "report4.rpt";
                        rpt.Load(Server.MapPath(sReport));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "131":
                        sReport = "report4_Alasamo.rpt";
                        rpt.Load(Server.MapPath(sReport));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "f6a":
                        sReport = "report_f06_Alasamo.rpt";
                        rpt.Load(Server.MapPath(sReport));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "95":
                        rpt.Load(Server.MapPath("dialysis_report_h05.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "96":
                        rpt.Load(Server.MapPath("dialysis_report_h06.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "97":
                        rpt.Load(Server.MapPath("dialysis_report_h07.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "98":
                        rpt.Load(Server.MapPath("Quality.rpt"));
                        //rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        //20150528 ANDY
                        btn_HIS_Query_Click();
                        break;
                    case "99":  //新增蘇州醫院表單 - 血液透析患者評估表
                        rpt.Load(Server.MapPath("dialysis_report_h08.rpt"));
                        //rpt.DataDefinition.FormulaFields["RPT_LOGO"].Text = "ToText('" + sRPT_LOGO + "')";
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "100":  //新增蘇州醫院表單 - 查房紀錄表
                        rpt.Load(Server.MapPath("dialysis_report_e04.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "s01":  //新增病患材料清單含肝素
                        rpt.Load(Server.MapPath("dialysis_report_s01.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "s02":  //新增病患預估材料單
                        rpt.Load(Server.MapPath("dialysis_report_s02.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "s03":   //新增材料領料單
                        rpt.Load(Server.MapPath("dialysis_report_s03.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "s04":   //材料退料單
                        rpt.Load(Server.MapPath("dialysis_report_s04.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "s05":  //新增病患預估药品單
                        rpt.Load(Server.MapPath("dialysis_report_s05.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "E01":   //年评鉴质量指标
                        rpt.Load(Server.MapPath("QualityEvaluation.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "E02":   //月评鉴质量指标
                        rpt.Load(Server.MapPath("QualityEvaluationMonth.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "A09":   //首診四問
                        rpt.Load(Server.MapPath("dialysis_report_a09.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    case "J01":
                        rpt.Load(Server.MapPath("dialysis_report_j01.rpt"));
                        rpt.DataDefinition.FormulaFields["RPT_NAME"].Text = "ToText('" + sRPT_NAME + "')";
                        break;
                    default:
                        Console.WriteLine("Default case");
                        break;
                }

                if (sREPORT_NAME == "5") //基本病患统计
                {
                    #region sREPORT_NAME == "5"

                    DataSet1.report_ageDataTable dt = new DataSet1.report_ageDataTable();
                    DataSet ds = new DataSet();
                    DataRow dr;

                    string sql = "SELECT FLOOR((EXTRACT(YEAR FROM CURDATE())-CAST(SUBSTR(PIF_DOB,1,4) AS UNSIGNED INTEGER))/10 ) AS AGE, ";
                    sql += "PIF_SEX AS SEX, COUNT(*) AS CNT ";
                    sql += "FROM pat_info ";
                    sql += "LEFT JOIN zinfo_a_07  f ON pat_info.pif_id = f.pat_id AND f.opt_1 in('','5') ";
                    //sql += "WHERE SUBSTR(PIF_DOB,1,1) IN ('1','2') AND PIF_DOB<>'2935/7/21' AND PIF_SEX<>'' ";
                    sql += "WHERE PIF_SEX<>'' ";
                    sql += "GROUP BY AGE, SEX";

                    DataTable dtAGE = db.Query(sql);

                    dr = dt.NewRow();
                    dr["Total_A"] = "0";
                    dr["Total_M"] = "0";
                    dr["Total_F"] = "0";

                    for (int i = 2; i <= 9; i++)
                    {
                        dr["A" + i.ToString() + "_A"] = "0";
                        dr["A" + i.ToString() + "_M"] = "0";
                        dr["A" + i.ToString() + "_F"] = "0";
                    }

                    string sSEX;
                    int iAGE;
                    int iCNT;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        sSEX = dtAGE.Rows[i]["SEX"].ToString();
                        iAGE = Convert.ToInt16(dtAGE.Rows[i]["AGE"].ToString());
                        iCNT = Convert.ToInt16(dtAGE.Rows[i]["CNT"].ToString());
                        if (iAGE > 9)
                            iAGE = 9;
                        else if (iAGE <= 2)
                            iAGE = 2;

                        dr["Total_A"] = Convert.ToInt16(dr["Total_A"]) + iCNT;
                        dr["A" + iAGE.ToString() + "_A"] = Convert.ToString(Convert.ToInt16(dr["A" + iAGE.ToString() + "_A"]) + iCNT);

                        if (sSEX == "M" || sSEX == "F")
                        {
                            dr["Total_" + sSEX] = Convert.ToInt16(dr["Total_" + sSEX]) + iCNT;
                            dr["A" + iAGE.ToString() + "_" + sSEX] = Convert.ToString(Convert.ToInt16(dr["A" + iAGE.ToString() + "_" + sSEX]) + iCNT);
                        }
                    }

                    dt.Rows.Add(dr);
                    ds.Tables.Add(dt);
                    rpt.SetDataSource(ds);

                    #endregion
                }

                else if (sREPORT_NAME == "61")
                {
                    #region sREPORT_NAME == "61"

                    DataSet ds = new DataSet();
                    DataSetDialysis.dialysis_report_e011_headDataTable dt = new DataSetDialysis.dialysis_report_e011_headDataTable();

                    string sql = @"
                        SELECT  P.pif_ic,
                            T1.cln1_diadate,  /* 紀錄日期 */
                            P.pif_id, /* pat_no in a_result_log */
                            P.pif_name,  /* 姓名 */
                            T1.cln1_col33, /* 經治醫師 */
                            T1.cln1_col40, /* 醫囑 */
                            T1.cln1_col1, /* 透析處方 */
                            T1.cln1_col3, /* 透析方式 */
                            /* 血管通路位置 */
                            T1.cln1_col34, /* 通路類型 */
                            T1.cln1_col13, /* 抗擬肝素_首量 */
                            T1.cln1_col14, /* 抗擬肝素_追加量 */
                            T1.cln1_col15, /* 抗擬肝素_低分子肝素量 */
                            T1.cln1_col26, /* 透析器型號 */
                            T1.cln1_col6, /* 乾體重 */
                            if((T1_PREV.cln1_col6) is null or (T1.cln1_col6 is null),
                                null,
                                if(t1.cln1_col6 > t1_prev.cln1_col6,
                                    '上调',
                                    if(t1.cln1_col6=t1_prev.cln1_col6, '维持', '下调')
                                )
                            ) as cln1_col6_compare,/* 乾體重較前期 */
                            T3.cln3_a1, /* 血壓範圍 收縮壓 min*/
                            T3.cln3_a2, /* 血壓範圍 收縮壓 max*/
                            T3.cln3_b1, /* 血壓範圍 舒張壓 min*/
                            T3.cln3_b2, /* 血壓範圍 舒張壓 max*/
                            T1.cln1_col7, /* 目標定容量 */
                            TRUNCATE(T1.cln1_col7/T1.cln1_col6*100, 2) as cln1_col7_rate, /* 目標定容量/乾體重 比率 */
                            data_list.column_4, /* 血流量 */
                            T1.cln1_col23, /* 透析前症狀 */
                            P.pif_pattyp,/* 是否住院 */
                            T3.cln3_rmk2 /* 特殊病情、檢查及處理*/

                          FROM pat_info P
                            LEFT JOIN clinical1_nurse T1 ON T1.cln1_patic=P.pif_ic
                            LEFT JOIN  (
                              SELECT person_id, dialysis_date, avg(column_4) as column_4
                              FROM data_list
                              WHERE person_id='{0}' AND dialysis_date='{2}'
                              group by person_id, dialysis_date
                            ) as data_list on T1.cln1_patic=data_list.person_id AND T1.cln1_diadate=data_list.dialysis_date
                            LEFT JOIN clinical3_nurse T3 ON T1.cln1_patic=T3.cln3_patic AND T1.cln1_diadate=T3.cln3_date
                            LEFT JOIN (
                                SELECT cln1_patic, cln1_diadate, cln1_col6
                                FROM clinical1_nurse
                                WHERE cln1_patic='{1}' AND cln1_diadate<'{2}'
                                order by cln1_diadate desc
                                LIMIT 1
                            ) as T1_PREV on T1.cln1_patic=T1_PREV.cln1_patic
                            /*inner join a_result_log a on a.pat_no = pif_id and a.result_date=dialysis_date */
    
                          WHERE P.pif_id='{0}' AND T1.cln1_diadate='{2}'
                          ORDER BY T1.cln1_patic, T1.cln1_diadate desc";
                    string sSQL = string.Format(sql, sPAT_ID, sPAT_IC, sINFO_DATE).Replace("\r\n", "");
                    db.Fill(sSQL, dt);

                    //int patId = 0;
                    //if (dt.Rows.Count > 0) {
                    //    patId = Int32.Parse(dt.Rows[0]["pif_id"].ToString());
                    //}
                    // 下面是需要動態運算部分
                    sql = @"
                        SELECT a.RESULT_CODE, a.RESULT_VALUE_T, b.RITEM_NAME, b.RITEM_NAME_S, b.ritem_low1, b.ritem_high1
	                    FROM a_result_log a
		                    LEFT JOIN  a_ritem_setup b ON a.RESULT_CLASS=b.RITEM_CLASS AND a.RESULT_CODE=b.RITEM_CODE
	                    WHERE a.RESULT_CLASS='G001'
		                    AND a.RESULT_DATE='{1}'
		                    AND a.PAT_NO={0}
		                    AND a.RESULT_VER=0
	                    ORDER BY a.RESULT_DATE, a.RESULT_CODE";

                    //DataTable dt1 = db.Query(string.Format(sql, patId, sINFO_DATE));
                    sSQL = string.Format(sql, sPAT_ID, sINFO_DATE).Replace("\r\n","").Replace("\t","");
                    DataTable dt1 = db.Query(sSQL);
                    DataSetDialysis.flat_a_result_logDataTable dtLab = new DataSetDialysis.flat_a_result_logDataTable();
                    if (dt1.Rows.Count > 0) {
                        DataRow drLab = dtLab.NewRow();
                        drLab["pif_id"] = sPAT_ID;// patId;
                        drLab["diadate"] = sINFO_DATE;

                        foreach (DataRow dr in dt1.Rows) {
                            float lowVal = 0, highVal = 0;
                            if (dr["ritem_low1"] != DBNull.Value) {
                                lowVal = float.Parse(dr["ritem_low1"].ToString());
                                highVal = float.Parse(dr["ritem_high1"].ToString());
                            }

                            switch (dr["RESULT_CODE"].ToString()) {
                                case "4003":    // Hb
                                    drLab["Hb"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                                case "4009":    // GOT
                                    drLab["GOT"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                                case "4010":    // GPT
                                    drLab["GPT"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                                case "4021":    // 鈣
                                    drLab["Ca"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                                case "4023":    // 磷
                                    drLab["P"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                                case "4026": {   // 總鐵結合力
                                        if (dr["RESULT_VALUE_T"] != DBNull.Value) {
                                            float val = float.Parse(dr["RESULT_VALUE_T"].ToString());
                                            if (val <= highVal && val >= lowVal) {
                                                drLab["SerumTotalIronBinding"] = dr["RESULT_VALUE_T"].ToString() + "(正常)";
                                            } else {
                                                drLab["SerumTotalIronBinding"] = dr["RESULT_VALUE_T"].ToString() + "(异常)";
                                            }
                                        }
                                    }
                                    break;
                                case "4027": {   // 鐵蛋白
                                        if (dr["RESULT_VALUE_T"] != DBNull.Value) {
                                            float val = float.Parse(dr["RESULT_VALUE_T"].ToString());
                                            if (val <= highVal && val >= lowVal) {
                                                drLab["SerumFerritsn"] = dr["RESULT_VALUE_T"].ToString() + "(正常)";
                                            } else {
                                                drLab["SerumFerritsn"] = dr["RESULT_VALUE_T"].ToString() + "(异常)"; ;
                                            }
                                        }
                                    }
                                    break;
                                case "4030":    // iPTH
                                    drLab["iPTH"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                                case "4050":    // 转铁蛋白饱和度
                                    drLab["TransferrinSaturation"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                                case "4070": {   // 血清前白蛋白
                                        if (dr["RESULT_VALUE_T"] != DBNull.Value) {
                                            float val = float.Parse(dr["RESULT_VALUE_T"].ToString());
                                            if (val <= highVal && val >= lowVal) {
                                                drLab["Prealbumin"] = dr["RESULT_VALUE_T"].ToString() + "(正常)";
                                            } else {
                                                drLab["Prealbumin"] = dr["RESULT_VALUE_T"].ToString() + "(异常)"; ;
                                            }
                                        }
                                    }
                                    break;
                                case "5017":    // URR
                                    drLab["URR"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                                case "5018":    // KTV
                                    drLab["KTV"] = dr["RESULT_VALUE_T"].ToString();
                                    break;
                            }
                        }

                        dtLab.Rows.Add(drLab);
                    }

                    dt.AcceptChanges();
                    ds.Tables.Add(dt);
                    ds.Tables.Add(dtLab);
                    rpt.SetDataSource(ds);

                    #endregion
                }

                else if (sREPORT_NAME == "7")
                {
                    #region sREPORT_NAME == "7"

                    DataSet1.report_cntDataTable dt = new DataSet1.report_cntDataTable();
                    DataSet1.report_msgDataTable dt2 = new DataSet1.report_msgDataTable();

                    DataSet ds = new DataSet();
                    DataRow dr;
                    DataRow dr2;
                    String sWHERE = "";
                    String sDATE = "";

                    if (sBEG_DATE == "")
                    {
                        if (sEND_DATE == "")
                        {
                            sWHERE = "";
                            sDATE = "日期区间：无";
                        }
                        else
                        {
                            sWHERE = "WHERE cln1_diadate<='" + sEND_DATE + "' ";
                            sDATE = "日期区间：" + sEND_DATE + "之前";
                        }
                    }
                    else
                    {
                        if (sEND_DATE == "")
                        {
                            sWHERE = "WHERE cln1_diadate>='" + sBEG_DATE + "' ";
                            sDATE = "日期区间：" + sBEG_DATE + "之后";
                        }
                        else
                        {
                            sWHERE = "WHERE cln1_diadate<='" + sEND_DATE + "' " +
                                       "AND cln1_diadate>='" + sBEG_DATE + "' ";
                            sDATE = "日期区间：" + sBEG_DATE + "~" + sEND_DATE;
                        }
                    }

                    string sql = "SELECT cln1_diadate AS DIA_DATE, cln1_col3 AS DIA_TYPE, cln1_col20 AS DIA_NURSE, count(*) as CNT " +
                                   "FROM clinical1_nurse " + sWHERE +
                                  "GROUP BY cln1_diadate, cln1_col3, cln1_col20 " +
                                  "ORDER BY cln1_col20, cln1_diadate, cln1_col3 ";

                    DataTable dtTMP = db.Query(sql);
                    //dtTMP = db.Query(sql);

                    for (int i = 0; i < dtTMP.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr.ItemArray = dtTMP.Rows[i].ItemArray;
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);

                    dr2 = dt2.NewRow();
                    dr2["sDATA"] = sDATE;
                    dt2.Rows.Add(dr2);
                    ds.Tables.Add(dt2);

                    rpt.SetDataSource(ds);

                    #endregion
                }

                else if (sREPORT_NAME == "8")
                {
                    #region sREPORT_NAME == "8"

                    DataSet1.report_cntADataTable dt = new DataSet1.report_cntADataTable();
                    DataSet1.report_msgDataTable dt2 = new DataSet1.report_msgDataTable();
                    //20穿刺, 21上機, 35下機, 22核對, 36巡視
                    DataSet ds = new DataSet();
                    DataRow dr;
                    DataRow dr2;
                    String sWHERE = "";
                    String sDATE = "";

                    if (sBEG_DATE == "")
                    {
                        if (sEND_DATE == "")
                        {
                            sWHERE = "";
                            sDATE = "日期区间：无";
                        }
                        else
                        {
                            sWHERE = "WHERE cln1_diadate<='" + sEND_DATE + "' ";
                            sDATE = "日期区间：" + sEND_DATE + "之前";
                        }
                    }
                    else
                    {
                        if (sEND_DATE == "")
                        {
                            sWHERE = "WHERE cln1_diadate>='" + sBEG_DATE + "' ";
                            sDATE = "日期区间：" + sBEG_DATE + "之后";
                        }
                        else
                        {
                            sWHERE = "WHERE cln1_diadate<='" + sEND_DATE + "' " +
                                       "AND cln1_diadate>='" + sBEG_DATE + "' ";
                            sDATE = "日期区间：" + sBEG_DATE + "~" + sEND_DATE;
                        }
                    }

                    string sql = "SELECT acclv_fname AS DIA_NURSE, " +
                                        "0 as DIA_CNT20, 0 as DIA_CNT21, 0 as DIA_CNT22, " +
                                        "0 as DIA_CNT35, 0 as DIA_CNT36 " +
                                   "FROM access_level";
                    DataTable dtNURSE = db.Query(sql);

                    sql = "SELECT a.cln1_col20 AS DIA_NURSE, count(*) as CNT " +
                            "FROM clinical1_nurse a" +
                            " join pat_info   b on a.cln1_patic = b.pif_ic " +
                            " join zinfo_a_07 c on b.pif_id     = c.pat_id and c.opt_1 in('','5') " + sWHERE +
                           "  GROUP BY cln1_col20 ";
                    DataTable dtTMP20 = db.Query(sql);

                    sql = "SELECT a.cln1_col21 AS DIA_NURSE, count(*) as CNT " +
                            "FROM clinical1_nurse a" +
                            " join pat_info   b on a.cln1_patic = b.pif_ic " +
                            " join zinfo_a_07 c on b.pif_id     = c.pat_id and c.opt_1 in('','5') " + sWHERE +
                           "GROUP BY cln1_col21 ";
                    DataTable dtTMP21 = db.Query(sql);

                    sql = "SELECT a.cln1_col22 AS DIA_NURSE, count(*) as CNT " +
                            "FROM clinical1_nurse a" +
                            " join pat_info   b on a.cln1_patic = b.pif_ic " +
                            " join zinfo_a_07 c on b.pif_id     = c.pat_id and c.opt_1 in('','5') " + sWHERE +
                           "GROUP BY cln1_col22 ";
                    DataTable dtTMP22 = db.Query(sql);

                    sql = "SELECT a.cln1_col35 AS DIA_NURSE, count(*) as CNT " +
                            "FROM clinical1_nurse a" +
                            " join pat_info   b on a.cln1_patic = b.pif_ic " +
                            " join zinfo_a_07 c on b.pif_id     = c.pat_id and c.opt_1 in('','5') " + sWHERE +
                           "GROUP BY cln1_col35 ";
                    DataTable dtTMP35 = db.Query(sql);

                    sql = "SELECT a.cln1_col36 AS DIA_NURSE, count(*) as CNT " +
                            "FROM clinical1_nurse a" +
                            " join pat_info   b on a.cln1_patic = b.pif_ic " +
                            " join zinfo_a_07 c on b.pif_id     = c.pat_id and c.opt_1 in('','5') " + sWHERE +
                           "GROUP BY cln1_col36 ";
                    DataTable dtTMP36 = db.Query(sql);

                    System.Data.DataView DV = dtNURSE.DefaultView;

                    for (int i = 0; i < dtTMP20.Rows.Count; i++)
                    {
                        DV.RowFilter = "DIA_NURSE='" + dtTMP20.Rows[i]["DIA_NURSE"] + "'";
                        if (DV.Count > 0)
                            DV[0]["DIA_CNT20"] = dtTMP20.Rows[i]["CNT"];
                    }
                    for (int i = 0; i < dtTMP21.Rows.Count; i++)
                    {
                        DV.RowFilter = "DIA_NURSE='" + dtTMP21.Rows[i]["DIA_NURSE"] + "'";
                        if (DV.Count > 0)
                            DV[0]["DIA_CNT21"] = dtTMP21.Rows[i]["CNT"];
                    }
                    for (int i = 0; i < dtTMP22.Rows.Count; i++)
                    {
                        DV.RowFilter = "DIA_NURSE='" + dtTMP22.Rows[i]["DIA_NURSE"] + "'";
                        if (DV.Count > 0)
                            DV[0]["DIA_CNT22"] = dtTMP22.Rows[i]["CNT"];
                    }
                    for (int i = 0; i < dtTMP35.Rows.Count; i++)
                    {
                        DV.RowFilter = "DIA_NURSE='" + dtTMP35.Rows[i]["DIA_NURSE"] + "'";
                        if (DV.Count > 0)
                            DV[0]["DIA_CNT35"] = dtTMP35.Rows[i]["CNT"];
                    }
                    for (int i = 0; i < dtTMP36.Rows.Count; i++)
                    {
                        DV.RowFilter = "DIA_NURSE='" + dtTMP36.Rows[i]["DIA_NURSE"] + "'";
                        if (DV.Count > 0)
                            DV[0]["DIA_CNT36"] = dtTMP36.Rows[i]["CNT"];
                    }

                    for (int i = 0; i < dtNURSE.Rows.Count; i++)
                    {
                        if (Convert.ToInt16(dtNURSE.Rows[i]["DIA_CNT20"].ToString()) +
                            Convert.ToInt16(dtNURSE.Rows[i]["DIA_CNT21"].ToString()) +
                            Convert.ToInt16(dtNURSE.Rows[i]["DIA_CNT22"].ToString()) +
                            Convert.ToInt16(dtNURSE.Rows[i]["DIA_CNT35"].ToString()) +
                            Convert.ToInt16(dtNURSE.Rows[i]["DIA_CNT36"].ToString()) > 0)
                        {
                            dr = dt.NewRow();
                            dr.ItemArray = dtNURSE.Rows[i].ItemArray;
                            dt.Rows.Add(dr);
                        }
                    }
                    ds.Tables.Add(dt);

                    dr2 = dt2.NewRow();
                    dr2["sDATA"] = sDATE;
                    dt2.Rows.Add(dr2);
                    ds.Tables.Add(dt2);

                    rpt.SetDataSource(ds);

                    #endregion
                }

                else if (sREPORT_NAME == "9") //血透中出现症状统计
                {
                    #region sREPORT_NAME == "9"

                    DataSet1.report_err2DataTable dt = new DataSet1.report_err2DataTable();
                    DataSet1.report_err2ADataTable dt3 = new DataSet1.report_err2ADataTable();
                    DataSet1.report_msgDataTable dt2 = new DataSet1.report_msgDataTable();

                    String sDATE = "";
                    DataSet ds = new DataSet();
                    DataRow dr;
                    DataRow dr2;

                    if (sBEG_DATE == "")
                        if (sEND_DATE == "")
                            sDATE = "日期区间：无";
                        else
                            sDATE = "日期区间：" + sEND_DATE + "之前";
                    else
                        if (sEND_DATE == "")
                            sDATE = "日期区间：" + sBEG_DATE + "之后";
                        else
                            sDATE = "日期区间：" + sBEG_DATE + "~" + sEND_DATE;
                    string sSQL = "";

                    sSQL = "SELECT SUBSTR(A.cln3_date,1,7) AS Y_M, COUNT(*) AS TOTAL, 0 AS ERROR, '0' AS ERROR_P ";
                    sSQL += "FROM clinical3_nurse A ";
                    sSQL += "LEFT JOIN pat_info B ON A.cln3_patic=B.pif_ic ";
                    sSQL += "LEFT JOIN zinfo_a_07 f ON B.pif_id=f.pat_id AND f.opt_1=5 ";
                    sSQL += "WHERE 1=1 ";
                    if (sBEG_DATE != "")
                        sSQL += "AND A.cln3_date>='" + sBEG_DATE + "' ";
                    if (sEND_DATE != "")
                        sSQL += "AND A.cln3_date<='" + sEND_DATE + "' ";
                    sSQL += "GROUP BY SUBSTR(A.cln3_date,1,7) "; //年月
                    DataTable dtTOTAL6 = db.Query(sSQL);

                    sSQL = "SELECT SUBSTR(A.cln3_date,1,7) AS Y_M, A.cln3_ysa AS ERROR_NAME, COUNT(*) AS ERROR_CNT ";
                    sSQL += "FROM clinical3_nurse A ";
                    sSQL += "LEFT JOIN pat_info B ON A.cln3_patic=B.pif_ic ";
                    sSQL += "LEFT JOIN zinfo_a_07 f ON B.pif_id=f.pat_id AND f.opt_1=5 ";
                    sSQL += "WHERE 1=1 ";
                    if (sBEG_DATE != "")
                        sSQL += "AND A.cln3_date>='" + sBEG_DATE + "' ";
                    if (sEND_DATE != "")
                        sSQL += "AND A.cln3_date<='" + sEND_DATE + "' ";
                    sSQL += "GROUP BY SUBSTR(A.cln3_date,1,7), A.cln3_ysa " +
                            "ORDER BY SUBSTR(A.cln3_date,1,7), A.cln3_ysa ";
                    DataTable dtERR2A = db.Query(sSQL);
                    System.Data.DataView dvERR2A = dtERR2A.DefaultView;
                    for (int i = 0; i < dtTOTAL6.Rows.Count; i++)
                    {
                        dvERR2A.RowFilter = "Y_M='" + dtTOTAL6.Rows[i]["Y_M"].ToString() + "' AND ERROR_NAME='无症状' ";
                        if (dvERR2A.Count > 1)
                        {
                            if (Convert.ToInt16(dtTOTAL6.Rows[i]["TOTAL"]) != 0)
                            {
                                dtTOTAL6.Rows[i]["ERROR"] = Convert.ToInt16(dtTOTAL6.Rows[i]["TOTAL"]) - Convert.ToInt16(dvERR2A[0]["ERROR_CNT"]) - Convert.ToInt16(dvERR2A[1]["ERROR_CNT"]);
                                dtTOTAL6.Rows[i]["ERROR_P"] = (Convert.ToDouble(dtTOTAL6.Rows[i]["ERROR"]) * 100 / Convert.ToDouble(dtTOTAL6.Rows[i]["TOTAL"])).ToString("0.00");
                            }
                            dvERR2A[1].Delete();
                            dvERR2A[0].Delete();
                        }
                        else if (dvERR2A.Count > 0)
                        {
                            if (Convert.ToInt16(dtTOTAL6.Rows[i]["TOTAL"]) != 0)
                            {
                                dtTOTAL6.Rows[i]["ERROR"] = Convert.ToInt16(dtTOTAL6.Rows[i]["TOTAL"]) - Convert.ToInt16(dvERR2A[0]["ERROR_CNT"]);
                                dtTOTAL6.Rows[i]["ERROR_P"] = (Convert.ToDouble(dtTOTAL6.Rows[i]["ERROR"]) * 100 / Convert.ToDouble(dtTOTAL6.Rows[i]["TOTAL"])).ToString("0.00");
                            }
                            dvERR2A[0].Delete();
                        }
                    }
                    dtERR2A.AcceptChanges();

                    for (int i = 0; i < dtTOTAL6.Rows.Count; i++)
                    {
                        dr = dt.NewRow();
                        dr.ItemArray = dtTOTAL6.Rows[i].ItemArray;
                        dt.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt);

                    for (int i = 0; i < dtERR2A.Rows.Count; i++)
                    {
                        dr = dt3.NewRow();
                        dr.ItemArray = dtERR2A.Rows[i].ItemArray;
                        dt3.Rows.Add(dr);
                    }
                    ds.Tables.Add(dt3);

                    dr2 = dt2.NewRow();
                    dr2["sDATA"] = sDATE;
                    dt2.Rows.Add(dr2);
                    ds.Tables.Add(dt2);
                    rpt.SetDataSource(ds);
                    #endregion
                }

                else if (sREPORT_NAME == "10") //血透年龄统计
                {
                    #region sREPORT_NAME == "10"

                    DataSet1.report_ageDataTable dt = new DataSet1.report_ageDataTable();

                    DataSet ds = new DataSet();
                    DataRow dr;

                    string sql = "SELECT (EXTRACT(YEAR FROM CURDATE())-CAST(SUBSTR(pif_createdate,1,4) AS UNSIGNED INTEGER)) as AGE, PIF_SEX AS SEX, COUNT(*) as CNT ";
                    sql += "FROM pat_info ";
                    sql += "LEFT JOIN zinfo_a_07 f ON pat_info.pif_id = f.pat_id AND f.opt_1 in('','5') ";
                    //sql += "WHERE SUBSTR(pif_createdate,1,1) IN ('1','2') AND pif_createdate<= CURDATE() AND PIF_SEX<>'' ";
                    sql += "WHERE PIF_SEX<>'' ";
                    sql += "GROUP BY AGE, SEX";

                    DataTable dtAGE = db.Query(sql);

                    dr = dt.NewRow();
                    dr["Total_A"] = "0";
                    dr["Total_M"] = "0";
                    dr["Total_F"] = "0";

                    for (int i = 2; i <= 9; i++)
                    {
                        dr["A" + i.ToString() + "_A"] = "0";
                        dr["A" + i.ToString() + "_M"] = "0";
                        dr["A" + i.ToString() + "_F"] = "0";
                    }

                    string sSEX;
                    int iAGE;
                    int iiAGE;
                    int iCNT;
                    for (int i = 0; i < dtAGE.Rows.Count; i++)
                    {
                        sSEX = dtAGE.Rows[i]["SEX"].ToString();
                        iAGE = Convert.ToInt16(dtAGE.Rows[i]["AGE"].ToString());
                        iCNT = Convert.ToInt16(dtAGE.Rows[i]["CNT"].ToString());
                        switch (iAGE)
                        {
                            case 0: iiAGE = 0; break;
                            case 1: iiAGE = 1; break;
                            case 2: iiAGE = 1; break;
                            case 3: iiAGE = 2; break;
                            case 4: iiAGE = 2; break;
                            case 5: iiAGE = 3; break;
                            case 6: iiAGE = 3; break;
                            case 7: iiAGE = 4; break;
                            case 8: iiAGE = 4; break;
                            case 9: iiAGE = 5; break;
                           default: iiAGE = 5; break;
                        }

                        dr["Total_A"] = Convert.ToInt16(dr["Total_A"]) + iCNT;
                        dr["A" + (9 - iiAGE).ToString() + "_A"] = Convert.ToString(Convert.ToInt16(dr["A" + (9 - iiAGE).ToString() + "_A"]) + iCNT);

                        if (sSEX == "M" || sSEX == "F")
                        {
                            dr["Total_" + sSEX] = Convert.ToInt16(dr["Total_" + sSEX]) + iCNT;
                            dr["A" + (9 - iiAGE).ToString() + "_" + sSEX] = Convert.ToString(Convert.ToInt16(dr["A" + (9 - iiAGE).ToString() + "_" + sSEX]) + iCNT);
                        }
                    }

                    dt.Rows.Add(dr);
                    ds.Tables.Add(dt);
                    rpt.SetDataSource(ds);

                    #endregion
                }

                else if (sREPORT_NAME == "11") //干體重報表
                {
                    #region sREPORT_NAME == "11"
                    ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                    if (sPAT_ID != "")
                    {
                        pdv.Value = sPAT_ID;
                        rpt.ParameterFields["pat_ic"].CurrentValues.Clear();
                        rpt.ParameterFields["pat_ic"].DefaultValues.Clear();
                        rpt.ParameterFields["pat_ic"].CurrentValues.Add(pdv);
                    }

                    //給日期起訖
                    //rpt.ParameterFields["date"].CurrentValues.AddRange(DateTime.Parse(sBEG_DATE), DateTime.Parse(sEND_DATE), RangeBoundType.BoundInclusive, RangeBoundType.BoundInclusive);
                    rpt.ParameterFields["date"].CurrentValues.AddRange(sBEG_DATE, sEND_DATE, RangeBoundType.BoundInclusive, RangeBoundType.BoundInclusive);

                    #endregion
                }

                else if (sREPORT_NAME == "12" || sREPORT_NAME == "121") //淨化小結報表
                {
                    #region sREPORT_NAME == "12"
                    //bool bCheck = false;

                    DataTable dtLIST_HEAD, dtLIST_BODY;

                    switch (Hospital)
                    {
                        case "Hospital_Suzhou":
                            DataSetDialysis.dialysis_report4_HEADDataTable dtHospital_Suzhou_HEAD = new DataSetDialysis.dialysis_report4_HEADDataTable();
                            DataSetDialysis.dialysis_report4_BODYDataTable dtHospital_Suzhou_BODY = new DataSetDialysis.dialysis_report4_BODYDataTable();
                            dtLIST_HEAD = dtHospital_Suzhou_HEAD;
                            dtLIST_BODY = dtHospital_Suzhou_BODY;
                            break;
                        case "Hospital_117":
                            DataSetDialysis.dialysis_report4_HEADDataTable dtHospital_117_HEAD = new DataSetDialysis.dialysis_report4_HEADDataTable();
                            DataSetDialysis.dialysis_report4_BODYDataTable dtHospital_117_BODY = new DataSetDialysis.dialysis_report4_BODYDataTable();
                            dtLIST_HEAD = dtHospital_117_HEAD;
                            dtLIST_BODY = dtHospital_117_BODY;
                            break;
                        case "Hospital_Henan":
                            DataSetDialysis.dialysis_report4_HEADDataTable dtHospital_Henan_HEAD = new DataSetDialysis.dialysis_report4_HEADDataTable();
                            DataSetDialysis.dialysis_report4_BODYDataTable dtHospital_Henan_BODY = new DataSetDialysis.dialysis_report4_BODYDataTable();
                            dtLIST_HEAD = dtHospital_Henan_HEAD;
                            dtLIST_BODY = dtHospital_Henan_BODY;
                            break;
                        case "Hospital_Hebei":
                            DataSetDialysis.dialysis_report4_HEADDataTable dtHospital_Hebei_HEAD = new DataSetDialysis.dialysis_report4_HEADDataTable();
                            DataSetDialysis.dialysis_report4_BODYDataTable dtHospital_Hebei_BODY = new DataSetDialysis.dialysis_report4_BODYDataTable();
                            dtLIST_HEAD = dtHospital_Hebei_HEAD;
                            dtLIST_BODY = dtHospital_Hebei_BODY;
                            break;
                        // 增加西安醫院介面 - 20160608 Added by Evan
                        case "Hospital_Xian":
                            DataSetDialysis.dialysis_report4_HEADDataTable dtHospital_Xian_HEAD = new DataSetDialysis.dialysis_report4_HEADDataTable();
                            DataSetDialysis.dialysis_report4_BODYDataTable dtHospital_Xian_BODY = new DataSetDialysis.dialysis_report4_BODYDataTable();
                            dtLIST_HEAD = dtHospital_Xian_HEAD;
                            dtLIST_BODY = dtHospital_Xian_BODY;
                            break;
                        case "Standard":
                            DataSetDialysis.dialysis_report4_HEADDataTable dtStandard_HEAD = new DataSetDialysis.dialysis_report4_HEADDataTable();
                            DataSetDialysis.dialysis_report4_BODYDataTable dtStandard_BODY = new DataSetDialysis.dialysis_report4_BODYDataTable();
                            dtLIST_HEAD = dtStandard_HEAD;
                            dtLIST_BODY = dtStandard_BODY;
                            break;
                        default:
                            DataSetDialysis.dialysis_report4_HEADDataTable dtdefault_HEAD = new DataSetDialysis.dialysis_report4_HEADDataTable();
                            DataSetDialysis.dialysis_report4_BODYDataTable dtdefault_BODY = new DataSetDialysis.dialysis_report4_BODYDataTable();
                            dtLIST_HEAD = dtdefault_HEAD;
                            dtLIST_BODY = dtdefault_BODY;
                            break;
                    }
                    //20160527 Alex 修正程序
                    string sql; // 靜態字串的串接 Literal String寫法! 用來表示多行文字的SQL語法 提升效能
                    if (Hospital == "Hospital_Suzhou")
                    {
                        sql = @"SELECT P.pif_dob AS PERSON_AGE, P.pif_name AS PERSON_NAME, " +
                                     "P.pif_ic AS PERSON_ID, P.pif_sex AS PERSON_SEX, " +
                                     "T1.cln1_diadate AS DIA_DATE, T1.*, T3.*, " +
                                     "TS.cln1_col11 AS clns_11,TS.cln1_col12 AS clns_12,TS.cln1_col21 AS clns_13,TS.cln1_col22 AS clns_14,TS.cln1_col31 AS clns_15,TS.cln1_col32 AS clns_16,TS.cln1_col41 AS clns_17,TS.cln1_col42 AS clns_18,TS.cln1_col50 AS clns_19 " +
                                "FROM clinical1_nurse T1 " +
                                "LEFT JOIN clinical3_nurse T3 ON T1.cln1_patic=T3.cln3_patic AND T1.cln1_diadate=T3.cln3_date " +
                                "LEFT JOIN clinical1_nurse_suzhou TS ON T1.cln1_patic=TS.cln1_patic AND T1.cln1_diadate=TS.cln1_diadate " +
                                "LEFT JOIN pat_info P ON T1.cln1_patic=P.pif_ic " +
                               "WHERE T1.cln1_patic='" + sPAT_ID + "' AND T1.cln1_diadate='" + sINFO_DATE + "' " +
                               "ORDER BY T1.cln1_patic, T1.cln1_diadate ";
                    }
                    else if (Hospital == "Hospital_117")
                    {
                        sql = @"SELECT P.pif_dob AS PERSON_AGE, P.pif_name AS PERSON_NAME, " +
                                     "P.pif_ic AS PERSON_ID, P.pif_sex AS PERSON_SEX, P.pif_insid AS PERSON_IC, " + //20160510 Alex 身分證號改醫保號 pif_insid
                                     "T1.cln1_diadate AS DIA_DATE, T1.*, T3.* " +
                                "FROM clinical1_nurse T1 " +
                                "LEFT JOIN clinical3_nurse T3 ON T1.cln1_patic=T3.cln3_patic AND T1.cln1_diadate=T3.cln3_date " +
                                "LEFT JOIN pat_info P ON T1.cln1_patic=P.pif_ic " +
                               "WHERE T1.cln1_patic='" + sPAT_ID + "' AND T1.cln1_diadate='" + sINFO_DATE + "' " +
                               "ORDER BY T1.cln1_patic, T1.cln1_diadate ";
                    }
                    else if (Hospital == "Hospital_Xian") //西安武警醫院
                    {
                        sql = @"SELECT P.pif_dob AS PERSON_AGE, P.pif_name AS PERSON_NAME, " +
                                     "P.pif_ic AS PERSON_ID, P.pif_sex AS PERSON_SEX, P.pif_mrn AS PERSON_IC, " +
                                     "'0' AS TotalDialysis, '0' AS LastWeight, " +
                                     "T1.cln1_diadate AS DIA_DATE, T1.*, T3.*, " +
                                     "X.cln1_col11 AS henan_col11, X.cln1_col12 AS henan_col12, X.cln1_col21 AS henan_col21, X.cln1_col22 AS henan_col22, " +
                                     "X.cln1_col31 AS col31, X.cln1_col32 AS col32 " +
                                "FROM clinical1_nurse T1 " +
                                "LEFT JOIN clinical3_nurse T3 ON T1.cln1_patic=T3.cln3_patic AND T1.cln1_diadate=T3.cln3_date " +
                                "LEFT JOIN pat_info P ON T1.cln1_patic=P.pif_ic " +
                                "LEFT JOIN clinical1_nurse_xian X ON T1.cln1_patic=X.cln1_patic AND T1.cln1_diadate=X.cln1_diadate " +
                               "WHERE T1.cln1_patic='" + sPAT_ID + "' AND T1.cln1_diadate='" + sINFO_DATE + "' " +
                               "ORDER BY T1.cln1_patic, T1.cln1_diadate ";
                    }
                    else if (Hospital == "Hospital_Henan" ||
                        Hospital == "Hospital_Hebei" ||
                        Hospital == "Hospital_Alasamo") //河南人民醫院
                    {
                        sql = @"SELECT P.pif_dob AS PERSON_AGE, P.pif_name AS PERSON_NAME, " +
                                     "P.pif_ic AS PERSON_ID, P.pif_sex AS PERSON_SEX, P.pif_mrn AS PERSON_IC, " +
                                     "'0' AS TotalDialysis, '0' AS LastWeight, " +
                                     "T1.cln1_diadate AS DIA_DATE, T1.*, T3.*, " +
                                     "H.cln1_col11 AS henan_col11, H.cln1_col12 AS henan_col12, H.cln1_col21 AS henan_col21, H.cln1_col22 AS henan_col22, F.txt_6, F.txt_7, F.txt_9, " +
                                     "CASE WHEN T1.cln1_col24='病危' THEN '病危' WHEN T1.cln1_col24='病重' THEN '病重' ELSE '一般' END AS col24 " +
                                "FROM clinical1_nurse T1 " +
                                "LEFT JOIN clinical3_nurse T3 ON T1.cln1_patic=T3.cln3_patic AND T1.cln1_diadate=T3.cln3_date " +
                                "LEFT JOIN pat_info P ON T1.cln1_patic=P.pif_ic " +
                                "LEFT JOIN clinical1_nurse_henan H ON T1.cln1_patic=H.cln1_patic AND T1.cln1_diadate=H.cln1_diadate " +
                                "LEFT JOIN zinfo_f_011 F ON F.pat_id=P.pif_id " +
                               "WHERE T1.cln1_patic='" + sPAT_ID + "' AND T1.cln1_diadate='" + sINFO_DATE + "' " +
                               "ORDER BY T1.cln1_patic, T1.cln1_diadate ";
                    }
                    else //Standard
                    {
                        sql = @"SELECT P.pif_dob AS PERSON_AGE, P.pif_name AS PERSON_NAME, " +
                                     "P.pif_ic AS PERSON_ID, P.pif_sex AS PERSON_SEX, " + //20160510 Alex 身分證號 pif_ic
                                     "T1.cln1_diadate AS DIA_DATE, T1.*, T3.* " +
                                "FROM clinical1_nurse T1 " +
                                "LEFT JOIN clinical3_nurse T3 ON T1.cln1_patic=T3.cln3_patic AND T1.cln1_diadate=T3.cln3_date " +
                                "LEFT JOIN pat_info P ON T1.cln1_patic=P.pif_ic " +
                               "WHERE T1.cln1_patic='" + sPAT_ID + "' AND T1.cln1_diadate='" + sINFO_DATE + "' " +
                               "ORDER BY T1.cln1_patic, T1.cln1_diadate ";
                    }
                    db.Fill(sql, dtLIST_HEAD);

                    if (dtLIST_HEAD.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLIST_HEAD.Rows.Count; i++)
                        {
                            switch (dtLIST_HEAD.Rows[i]["PERSON_SEX"].ToString().ToUpper())
                            {
                                case "M":
                                    dtLIST_HEAD.Rows[i]["PERSON_SEX"] = "男";
                                    break;
                                case "F":
                                    dtLIST_HEAD.Rows[i]["PERSON_SEX"] = "女";
                                    break;
                            }

                            int iAGE = 0;
                            string sAGE = dtLIST_HEAD.Rows[i]["PERSON_AGE"].ToString().Substring(0, 4);
                            if (int.TryParse(sAGE, out  iAGE))
                                iAGE = int.Parse(sAGE);
                            if (iAGE != 0)
                                dtLIST_HEAD.Rows[i]["PERSON_AGE"] = (DateTime.Now.Year - iAGE).ToString();
                            dtLIST_HEAD.Rows[i]["cln1_5_8"] = dtLIST_HEAD.Rows[i]["cln3_rmk"].ToString();

                            if (Hospital == "Hospital_Suzhou")
                            {
                                string sMagicString = "";
                                char[] split = { ',', ';' };
                                string[] strMagic = dtLIST_HEAD.Rows[i]["cln1_col16"].ToString().Split(split);
                                for (int j = 0; j < strMagic.Length; j++)
                                {
                                    if (j > 0)
                                        sMagicString += "　|　";
                                    switch (strMagic[j])
                                    {
                                        case "EPO":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col28"].ToString() + " u";
                                            break;
                                        case "左卡":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col29"].ToString() + " g";
                                            break;
                                        case "钙剂":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col30"].ToString();
                                            break;
                                        case "稀释液":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col31"].ToString();
                                            break;
                                        case "其它":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col32"].ToString();
                                            break;
                                    }
                                }
                                dtLIST_HEAD.Rows[i]["cln1_col16"] = sMagicString;
                            }
                            else if (Hospital == "Hospital_117")
                            {
                                string sMagicString = "";
                                char[] split = { ',', ';' };
                                string[] strMagic = dtLIST_HEAD.Rows[i]["cln1_col36"].ToString().Split(split);
                                for (int j = 0; j < strMagic.Length; j++)
                                {
                                    switch (strMagic[j])
                                    {
                                        case "EPO":
                                            sMagicString += "重组人红细胞生成素注射液: " + dtLIST_HEAD.Rows[i]["cln1_col28"].ToString() + "\r\n";
                                            break;
                                        case "左卡":
                                            sMagicString += "0.9%氯化钠注射液10ml+左卡尼汀注射液: " + dtLIST_HEAD.Rows[i]["cln1_col29"].ToString() + "\r\n";
                                            break;
                                        case "铁剂":
                                            sMagicString += "蔗糖铁注射液: " + dtLIST_HEAD.Rows[i]["cln1_col30"].ToString() + "\r\n";
                                            break;
                                        case "骨化三醇":
                                            sMagicString += "骨化三醇注射液: " + dtLIST_HEAD.Rows[i]["cln1_col31"].ToString() + "\r\n";
                                            break;
                                        case "弥可保":
                                            sMagicString += "甲钴胺注射液: " + dtLIST_HEAD.Rows[i]["cln1_col32"].ToString() + "\r\n";
                                            break;
                                        case "维生素B12":
                                            sMagicString += "维生素B12: " + dtLIST_HEAD.Rows[i]["cln1_col37"].ToString() + "\r\n";
                                            break;
                                    }
                                }
                                dtLIST_HEAD.Rows[i]["cln1_col36"] = sMagicString + dtLIST_HEAD.Rows[i]["cln1_col23"].ToString();
                            }
                            else if (Hospital == "Hospital_Henan" ||
                                Hospital == "Hospital_Hebei" ||
                                Hospital == "Hospital_Alasamo")
                            {
                                //cln1_col16 = 透析時間 4h, 3.5h, 3h...
                            }
                            else if (Hospital == "Hospital_Xian")
                            {
                                string sMagicString = "";
                                char[] split = { ',', ';' };
                                string[] strMagic = dtLIST_HEAD.Rows[i]["cln1_col16"].ToString().Split(split);
                                for (int j = 0; j < strMagic.Length; j++)
                                {
                                    if (j > 0)
                                        sMagicString += "　|　";
                                    switch (strMagic[j])
                                    {
                                        case "EPO":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col28"].ToString() + " u";  // 加入單位 - Added by Evan 20160617
                                            break;
                                        case "左卡":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col29"].ToString() + " g";  // 加入單位 - Added by Evan 20160617
                                            break;
                                        case "甲钴铵":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col30"].ToString() + " mg";
                                            break;
                                        case "铁剂":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col31"].ToString() + " ug";
                                            break;
                                        case "溉醇":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col32"].ToString() + " ml";
                                            break;
                                        case "透析液钠":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col37"].ToString() + " mmol/L";
                                            break;
                                        case "透析液钾":
                                            sMagicString += strMagic[j] + ":" + dtLIST_HEAD.Rows[i]["cln1_col38"].ToString();
                                            break;
                                    }
                                }
                                dtLIST_HEAD.Rows[i]["cln1_col16"] = sMagicString;
                            }
                            else if (Hospital == "Standard")
                            {
                                string sMagicString = "";
                                char[] split = { ',', ';' };
                                string[] strMagic = dtLIST_HEAD.Rows[i]["cln1_col16"].ToString().Split(split);
                                for (int j = 0; j < strMagic.Length; j++)
                                {
                                    if (j > 0)
                                        sMagicString += "　|　";
                                    switch (strMagic[j])
                                    {
                                        case "EPO":
                                            sMagicString += strMagic[j];
                                            break;
                                        case "左卡":
                                            sMagicString += strMagic[j];
                                            break;
                                        case "铁剂":
                                            sMagicString += strMagic[j];
                                            break;
                                        case "钙剂":
                                            sMagicString += strMagic[j];
                                            break;
                                        case "抗菌素/其它":
                                            sMagicString += strMagic[j];
                                            break;
                                    }
                                }
                                dtLIST_HEAD.Rows[i]["cln1_col16"] = sMagicString;
                            }
                            else
                            {
                                string sMagicString = "";
                                char[] split = { ',', ';' };
                                string[] strMagic = dtLIST_HEAD.Rows[i]["cln1_col16"].ToString().Split(split);
                                for (int j = 0; j < strMagic.Length; j++)
                                {
                                    if (j > 0)
                                        sMagicString += "　|　";
                                    switch (strMagic[j])
                                    {
                                        case "EPO":
                                            sMagicString += strMagic[j];
                                            break;
                                        case "左卡":
                                            sMagicString += strMagic[j];
                                            break;
                                        case "铁剂":
                                            sMagicString += strMagic[j];
                                            break;
                                        case "钙剂":
                                            sMagicString += strMagic[j];
                                            break;
                                        case "抗菌素/其它":
                                            sMagicString += strMagic[j];
                                            break;
                                    }
                                }
                                dtLIST_HEAD.Rows[i]["cln1_col16"] = sMagicString;
                            }
                        }

                        if (Hospital == "Hospital_Henan" || //河南人民醫院 
                            Hospital == "Hospital_Hebei" || //河北醫院
                            Hospital == "Hospital_Alasamo") //阿拉善盟
                        {
                            int dialysis = 0;
                            sql = "SELECT COUNT(cln1_patic) AS dcnt FROM clinical1_nurse WHERE cln1_patic='" + sPAT_ID + "' AND cln1_diadate<='" + sINFO_DATE + "'";
                            DataTable dt = db.Query(sql);
                            if (dt.Rows.Count > 0)
                                dialysis = int.Parse(dt.Rows[0]["dcnt"].ToString());
                            sql = "SELECT pif_basetimes FROM pat_info WHERE pif_ic='" + sPAT_ID + "'";
                            dt = db.Query(sql);
                            if (dt.Rows.Count > 0 && dt.Rows[0]["pif_basetimes"].ToString() != "")
                            {
                                dialysis += int.Parse(dt.Rows[0]["pif_basetimes"].ToString());
                            }
                            dt.Dispose();
                            dtLIST_HEAD.Rows[0]["TotalDialysis"] = dialysis; //計算透析次數

                            sql = "SELECT cln1_col8 FROM clinical1_nurse ";
                            sql += "WHERE cln1_patic='" + sPAT_ID + "' AND cln1_diadate<>'" + sINFO_DATE + "' ";
                            sql += "ORDER BY cln1_diadate DESC LIMIT 1";
                            dt = db.Query(sql);
                            if (dt.Rows.Count > 0)
                                dtLIST_HEAD.Rows[0]["LastWeight"] = dt.Rows[0]["cln1_col8"].ToString(); //上次透析後體重
                            //nurse = dtLIST_HEAD.Rows[0]["cln1_col21"].ToString();
                            //exetime = dtLIST_HEAD.Rows[0]["cln1_col10"].ToString();
                        }
                        dtLIST_HEAD.AcceptChanges();

                        //bool bCntPage = true, b1Page = true, bPrint = true;
                        //DataRow DR;
                        sql = "SELECT DL.person_id AS PERSON_ID, DATE_FORMAT(DL.dialysis_date, '%Y-%m-%d') AS DIA_DATE, DL.dialysis_time AS TIME, ";
                        //DL.column_3 As column_2 超濾率
                        if (
                            Hospital == "Hospital_Suzhou" || 
                            Hospital == "Hospital_Henan" || 
                            Hospital == "Hospital_Hebei" || 
                            Hospital == "Hospital_Xian" // 加入西安武警醫院
                           ) 
                        {
                            sql += "DL.column_3 AS column_2, ";
                        }
                        //DL.column_2 已超濾
                        else
                        {
                            sql += "DL.column_2 AS column_2, ";
                        }

                        sql += "DL.column_4, DL.column_6, DL.column_7, DL.column_8, ";

                        //DL.column_12 超濾量
                        if (Hospital == "Hospital_Xian") // 西安武警醫院
                        {
                            sql += "DL.column_12 AS column_10, ";
                        }
                        else
                        {
                            sql += "DL.column_10 AS column_10, ";
                        }

                        sql += "T2.cln2_t AS col_T, T2.cln2_p AS col_P, T2.cln2_r AS col_R, T2.cln2_bp AS col_BP, T2.cln2_rmk AS col_RMK ";
                        sql += "FROM data_list DL ";
                        sql += "LEFT JOIN clinical2_nurse T2 ON DL.person_id = T2.cln2_patic AND DL.dialysis_date = T2.cln2_date AND DL.dialysis_time = T2.cln2_time ";
                        sql += "WHERE DL.person_id='" + sPAT_ID + "' AND DL.dialysis_date='" + sINFO_DATE + "' ";
                        sql += "AND (DL.column_2 IS NOT NULL "; //20160427 Alex Modify
                        sql += "OR DL.column_3 IS NOT NULL ";
                        sql += "OR DL.column_4 IS NOT NULL ";
                        sql += "OR DL.column_6 IS NOT NULL ";
                        sql += "OR DL.column_7 IS NOT NULL ";
                        sql += "OR DL.column_8 IS NOT NULL ";
                        sql += "OR DL.column_10 IS NOT NULL ";
                        sql += "OR T2.cln2_t IS NOT NULL ";
                        sql += "OR T2.cln2_p IS NOT NULL ";
                        sql += "OR T2.cln2_r IS NOT NULL ";
                        sql += "OR T2.cln2_bp IS NOT NULL ";
                        sql += "OR T2.cln2_rmk IS NOT NULL) "; 
                        sql += "ORDER BY DL.person_id, DL.dialysis_date, DL.dialysis_time ";
                        db.Fill(sql, dtLIST_BODY);
                        
                        if (sREPORT_NAME == "121")
                        {
                            if (dtLIST_BODY.Rows.Count > i1PageCount)
                            {
                                DateTime Time1 = DateTime.ParseExact(dtLIST_BODY.Rows[0]["TIME"].ToString(), "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                DateTime Time2;
                                int mins;

                                for (int i = 1; i < dtLIST_BODY.Rows.Count - 1; i++)
                                {
                                    Time2 = DateTime.ParseExact(dtLIST_BODY.Rows[i]["TIME"].ToString(), "HH:mm:ss", System.Globalization.CultureInfo.InvariantCulture);
                                    mins=Time1.Subtract(Time2).Duration().Minutes;
                                    if (mins >= 30)
                                        Time1 = Time2;
                                    else if (dtLIST_BODY.Rows[i]["col_T"].ToString()=="" && dtLIST_BODY.Rows[i]["col_P"].ToString()=="" && dtLIST_BODY.Rows[i]["col_R"].ToString()==""
                                        && dtLIST_BODY.Rows[i]["col_BP"].ToString() == "" && dtLIST_BODY.Rows[i]["col_RMK"].ToString() == "")
                                        dtLIST_BODY.Rows[i].Delete();
                                }
                                dtLIST_BODY.AcceptChanges();
                            }
                        }

                        DataSet dsLIST = new DataSet();
                        String strMsg = null;
                        Dictionary<String, string> dict = null;
                        dsLIST.Tables.Add(dtLIST_HEAD);
                        dsLIST.Tables.Add(dtLIST_BODY);

                        dict = DataTable2Dict(dtLIST_BODY);
                        strMsg = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
                        if (Hospital == "Hospital_Henan" ||
                            Hospital == "Hospital_Hebei" ||
                            Hospital == "Hospital_Alasamo" ||
                            Hospital == "Hospital_Xian")
                        {
                            DataSetDialysis.dialysis_report41_BODYDataTable dtHospital_Henan_BODY2 = new DataSetDialysis.dialysis_report41_BODYDataTable();
                            sql = "SELECT a.lgord_patic AS PERSON_ID, "; //a.lgord_timeord AS DIA_DATE, ";
                            sql += "(SELECT TIME_FORMAT(TIMEDIFF(STR_TO_DATE(cln1_col10, '%H:%i'),3000),'%H:%i') AS DIA_DATE FROM clinical1_nurse ";
                            sql += "WHERE cln1_patic='" + sPAT_ID + "' AND cln1_diadate='" + sINFO_DATE + "') AS DIA_DATE, ";
                            //sql += "a.lgord_timeord AS DIA_DATE,";
                            sql += "b.drg_name AS DRUG_NAME,a.lgord_intake AS INTAKE,a.lgord_medway AS MEDWAY,a.lgord_freq AS FREQ, ";
                            sql += "a.lgord_usr1 AS DOC_NAME, a.lgord_nurs AS NURSE_NAME, a.lgord_dtactst AS EXE_TIME ";
                            sql += "FROM longterm_ordermgt a ";
                            sql += "LEFT JOIN drug_list b ON a.lgord_drug=b.drg_code "; //長期醫囑
                            sql += "WHERE a.lgord_patic='" + sPAT_ID + "' AND a.lgord_actst='00001' ";
                            sql += "UNION ALL ";
                            sql += "SELECT c.shord_patic AS PERSON_ID, "; //c.shord_timeord AS DIA_DATE, ";
                            sql += "(SELECT TIME_FORMAT(TIMEDIFF(STR_TO_DATE(cln1_col10, '%H:%i'),3000),'%H:%i') AS DIA_DATE FROM clinical1_nurse ";
                            sql += "WHERE cln1_patic='" + sPAT_ID + "' AND cln1_diadate='" + sINFO_DATE + "') AS DIA_DATE, ";
                            //sql += "c.shord_timeord AS DIA_DATE,";
                            sql += "d.drg_name AS DRUG_NAME,c.shord_intake AS INTAKE,c.shord_medway AS MEDWAY,c.shord_freq AS FREQ, ";
                            sql += "c.shord_usr1 AS DOC_NAME, c.shord_nurs AS NURSE_NAME, c.shord_dtactst AS EXE_TIME ";
                            sql += "FROM shortterm_ordermgt c ";
                            sql += "LEFT JOIN drug_list d ON c.shord_drug=d.drg_code "; //短期醫囑
                            sql += "WHERE c.shord_patic='" + sPAT_ID + "' AND c.shord_dateord='" + sINFO_DATE + "' AND c.shord_actst='00001'";
                            db.Fill(sql, dtHospital_Henan_BODY2);
                            dsLIST.Tables.Add(dtHospital_Henan_BODY2);
                        }
                        rpt.SetDataSource(dsLIST);
                    }
                    #endregion
                }

                else if (sREPORT_NAME == "122") //淨化小結報表
                {
                    #region sREPORT_NAME == "122"
                    string[] SelDate = sINFO_DATE.Split(',');
                    string sDate = "''";
                    for (int i = 0; i < SelDate.Length; i++)
                    {
                        sDate += ",'" + SelDate[i] + "'";
                    }
                    DataTable dtLIST_HEAD, dtLIST_BODY;
                    DataSetDialysis.dialysis_report4_HEADDataTable dtHospital_Henan_HEAD = new DataSetDialysis.dialysis_report4_HEADDataTable();
                    DataSetDialysis.dialysis_report4_BODYDataTable dtHospital_Henan_BODY = new DataSetDialysis.dialysis_report4_BODYDataTable();
                    dtLIST_HEAD = dtHospital_Henan_HEAD;
                    dtLIST_BODY = dtHospital_Henan_BODY;
                    
                    string sql;
                    sql = @"SELECT P.pif_dob AS PERSON_AGE, P.pif_name AS PERSON_NAME, " +
                                    "P.pif_ic AS PERSON_ID, P.pif_sex AS PERSON_SEX, P.pif_mrn AS PERSON_IC, " +
                                    "'0' AS TotalDialysis, '0' AS LastWeight, " +
                                    "T1.cln1_diadate AS DIA_DATE, T1.*, T3.*, " +
                                    "H.cln1_col11 AS henan_col11, H.cln1_col12 AS henan_col12, H.cln1_col21 AS henan_col21, H.cln1_col22 AS henan_col22, F.txt_6, F.txt_7, F.txt_9 " +
                            "FROM clinical1_nurse T1 " +
                            "LEFT JOIN clinical3_nurse T3 ON T1.cln1_patic=T3.cln3_patic AND T1.cln1_diadate=T3.cln3_date " +
                            "LEFT JOIN pat_info P ON T1.cln1_patic=P.pif_ic " +
                            "LEFT JOIN clinical1_nurse_henan H ON T1.cln1_patic=H.cln1_patic AND T1.cln1_diadate=H.cln1_diadate " +
                            "LEFT JOIN zinfo_f_011 F ON F.pat_id=P.pif_id " +
                            "WHERE T1.cln1_patic='" + sPAT_ID + "' AND T1.cln1_diadate IN (" + sDate + ") " +
                            "ORDER BY T1.cln1_patic, T1.cln1_diadate ";
                    db.Fill(sql, dtLIST_HEAD);

                    if (dtLIST_HEAD.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtLIST_HEAD.Rows.Count; i++)
                        {
                            switch (dtLIST_HEAD.Rows[i]["PERSON_SEX"].ToString().ToUpper())
                            {
                                case "M":
                                    dtLIST_HEAD.Rows[i]["PERSON_SEX"] = "男";
                                    break;
                                case "F":
                                    dtLIST_HEAD.Rows[i]["PERSON_SEX"] = "女";
                                    break;
                            }

                            int iAGE = 0;
                            string sAGE = dtLIST_HEAD.Rows[i]["PERSON_AGE"].ToString().Substring(0, 4);
                            if (int.TryParse(sAGE, out  iAGE))
                                iAGE = int.Parse(sAGE);
                            if (iAGE != 0)
                                dtLIST_HEAD.Rows[i]["PERSON_AGE"] = (DateTime.Now.Year - iAGE).ToString();
                            dtLIST_HEAD.Rows[i]["cln1_5_8"] = dtLIST_HEAD.Rows[i]["cln3_rmk"].ToString();
                            
                            int dialysis = 0;
                            sql = "SELECT COUNT(cln1_patic) AS dcnt FROM clinical1_nurse WHERE cln1_patic='" + sPAT_ID + "'";
                            DataTable dt = db.Query(sql);
                            if (dt.Rows.Count > 0)
                                dialysis = int.Parse(dt.Rows[0]["dcnt"].ToString());
                            sql = "SELECT pif_hpacks1 FROM pat_info WHERE pif_ic='" + sPAT_ID + "'";
                            dt = db.Query(sql);
                            if (dt.Rows.Count > 0 && dt.Rows[0]["pif_hpacks1"].ToString() != "")
                            {
                                dialysis += int.Parse(dt.Rows[0]["pif_hpacks1"].ToString());
                            }
                            dt.Dispose();
                            dtLIST_HEAD.Rows[0]["TotalDialysis"] = dialysis; //計算透析次數

                            sql = "SELECT cln1_col8 FROM clinical1_nurse ";
                            sql += "WHERE cln1_patic='" + sPAT_ID + "' AND cln1_diadate<>'" + sINFO_DATE + "' ";
                            sql += "ORDER BY cln1_diadate DESC LIMIT 1";
                            dt = db.Query(sql);
                            if (dt.Rows.Count > 0)
                                dtLIST_HEAD.Rows[0]["LastWeight"] = dt.Rows[0]["cln1_col8"].ToString(); //上次透析後體重
                        }
                        dtLIST_HEAD.AcceptChanges();

                        sql = "SELECT DL.person_id AS PERSON_ID, DATE_FORMAT(DL.dialysis_date, '%Y-%m-%d') AS DIA_DATE, DL.dialysis_time AS TIME, ";
                        //DL.column_3 As column_2 超濾率
                        if (
                            Hospital == "Hospital_Suzhou" || 
                            Hospital == "Hospital_Henan" || 
                            Hospital == "Hospital_Hebei" || 
                            Hospital == "Hospital_Xian" || 
                            Hospital == "Hospital_Alasamo"       // 加入西安武警醫院
                           )
                        {
                            sql += "DL.column_3 AS column_2, ";
                        }
                        else //DL.column_2 已超濾
                        {
                            sql += "DL.column_2 AS column_2, ";
                        }
                        sql += "DL.column_4, DL.column_6, DL.column_7, DL.column_8, ";

                        //DL.column_12 超濾量
                        if (Hospital == "Hospital_Xian") // 西安武警醫院
                        {
                            sql += "DL.column_12 AS column_10, ";
                        }
                        else
                        {
                            sql += "DL.column_10 AS column_10, ";
                        }

                        sql += "T2.cln2_t AS col_T, T2.cln2_p AS col_P, T2.cln2_r AS col_R, T2.cln2_bp AS col_BP, T2.cln2_rmk AS col_RMK ";
                        sql += "FROM data_list DL ";
                        sql += "LEFT JOIN clinical2_nurse T2 ON DL.person_id = T2.cln2_patic AND DL.dialysis_date = T2.cln2_date AND DL.dialysis_time = T2.cln2_time ";
                        sql += "WHERE DL.person_id='" + sPAT_ID + "' AND DL.dialysis_date IN (" + sDate + ")  ";
                        sql += "AND (DL.column_2 IS NOT NULL "; 
                        sql += "OR DL.column_3 IS NOT NULL ";
                        sql += "OR DL.column_4 IS NOT NULL ";
                        sql += "OR DL.column_6 IS NOT NULL ";
                        sql += "OR DL.column_7 IS NOT NULL ";
                        sql += "OR DL.column_8 IS NOT NULL ";
                        sql += "OR DL.column_10 IS NOT NULL ";
                        sql += "OR T2.cln2_t IS NOT NULL ";
                        sql += "OR T2.cln2_p IS NOT NULL ";
                        sql += "OR T2.cln2_r IS NOT NULL ";
                        sql += "OR T2.cln2_bp IS NOT NULL ";
                        sql += "OR T2.cln2_rmk IS NOT NULL) ";
                        sql += "ORDER BY DL.person_id, DL.dialysis_date, DL.dialysis_time ";
                        db.Fill(sql, dtLIST_BODY);
                        
                        DataSet dsLIST = new DataSet();
                        String strMsg = null;
                        Dictionary<String, string> dict = null;
                        dsLIST.Tables.Add(dtLIST_HEAD);
                        dsLIST.Tables.Add(dtLIST_BODY);

                        dict = DataTable2Dict(dtLIST_BODY);
                        strMsg = Newtonsoft.Json.JsonConvert.SerializeObject(dict);
                        if (Hospital == "Hospital_Henan" ||
                            Hospital == "Hospital_Hebei" ||
                            Hospital == "Hospital_Alasamo")
                        {
                            DataSetDialysis.dialysis_report41_BODYDataTable dtHospital_Henan_BODY2 = new DataSetDialysis.dialysis_report41_BODYDataTable();
                            sql = "SELECT a.lgord_patic AS PERSON_ID, "; //a.lgord_timeord AS DIA_DATE, ";
                            sql += "(SELECT TIME_FORMAT(TIMEDIFF(STR_TO_DATE(cln1_col10, '%H:%i'),3000),'%H:%i') AS DIA_DATE FROM clinical1_nurse ";
                            sql += "WHERE cln1_patic='" + sPAT_ID + "' AND cln1_diadate IN (" + sDate + ") AS DIA_DATE, ";
                            sql += "b.drg_name AS DRUG_NAME,a.lgord_intake AS INTAKE,a.lgord_medway AS MEDWAY,a.lgord_freq AS FREQ, ";
                            sql += "a.lgord_usr1 AS DOC_NAME, a.lgord_nurs AS NURSE_NAME, a.lgord_dtactst AS EXE_TIME ";
                            sql += "FROM longterm_ordermgt a ";
                            sql += "LEFT JOIN drug_list b ON a.lgord_drug=b.drg_code "; //長期醫囑
                            sql += "WHERE a.lgord_patic='" + sPAT_ID + "' AND a.lgord_actst='00001' ";
                            sql += "UNION ALL ";
                            sql += "SELECT c.shord_patic AS PERSON_ID, "; //c.shord_timeord AS DIA_DATE, ";
                            sql += "(SELECT TIME_FORMAT(TIMEDIFF(STR_TO_DATE(cln1_col10, '%H:%i'),3000),'%H:%i') AS DIA_DATE FROM clinical1_nurse ";
                            sql += "WHERE cln1_patic='" + sPAT_ID + "' AND cln1_diadate IN (" + sDate + ")  AS DIA_DATE, ";
                            sql += "d.drg_name AS DRUG_NAME,c.shord_intake AS INTAKE,c.shord_medway AS MEDWAY,c.shord_freq AS FREQ, ";
                            sql += "c.shord_usr1 AS DOC_NAME, c.shord_nurs AS NURSE_NAME, c.shord_dtactst AS EXE_TIME ";
                            sql += "FROM shortterm_ordermgt c ";
                            sql += "LEFT JOIN drug_list d ON c.shord_drug=d.drg_code "; //短期醫囑
                            sql += "WHERE c.shord_patic='" + sPAT_ID + "' AND c.shord_dateord IN (" + sDate + ")  AND c.shord_actst='00001'";
                            db.Fill(sql, dtHospital_Henan_BODY2);
                            dsLIST.Tables.Add(dtHospital_Henan_BODY2);
                        }
                        rpt.SetDataSource(dsLIST);
                    }
                    #endregion
                }

                else if (sREPORT_NAME == "13")
                {
                    #region sREPORT_NAME == "13"

                    string sql;
                    sql = "SELECT P.pif_id, P.pif_name, P.pif_dob, P.pif_address, P.pif_contact, P.pif_insurance, P.pif_ic, P.pif_contactperson, P.pif_sex, " +
                                 "F2.txt_1 AS F2_txt_1, F2.txt_2 AS F2_txt_2, F2.txt_3 AS F2_txt_3, F2.txt_4 AS F2_txt_4, " +
                                 "F2.txt_5 AS F2_txt_5, F2.txt_6 AS F2_txt_6, F2.txt_7 AS F2_txt_7, F2.txt_8 AS F2_txt_8, F2.opt_9 AS F2_opt_9, " +
                                 "F2.opt_10 AS F2_opt_10, F2.opt_11 AS F2_opt_11, F2.opt_12 AS F2_opt_12, F2.opt_13 AS F2_opt_13, F2.opt_14 AS F2_opt_14, " +
                                 "F2.opt_15 AS F2_opt_15, F2.opt_16 AS F2_opt_16, F2.opt_17 AS F2_opt_17, F2.opt_18 AS F2_opt_18, F2.opt_19 AS F2_opt_19, F2.txt_19 AS F2_txt_19, " +
                                 "F2.opt_20 AS F2_opt_20, F2.opt_21 AS F2_opt_21, F2.txt_22 AS F2_txt_22, F2.opt_23 AS F2_opt_23, F2.opt_24 AS F2_opt_24, " +
                                 "F2.opt_25 AS F2_opt_25, F2.opt_26 AS F2_opt_26, F2.txt_27 AS F2_txt_27, F2.opt_28 AS F2_opt_28, F2.txt_29 AS F2_txt_29, " +
                                 "F2.txt_30 AS F2_txt_30, F2.opt_31 AS F2_opt_31, F2.opt_32 AS F2_opt_32, F2.txt_33 AS F2_txt_33, F2.opt_34 AS F2_opt_34, " +
                                 "F2.opt_35 AS F2_opt_35, F2.opt_36 AS F2_opt_36, F2.txt_37 AS F2_txt_37, F2.txt_38 AS F2_txt_38, F2.txt_39 AS F2_txt_39, " +
                                 "F2.opt_40 AS F2_opt_40, F2.txt_41 AS F2_txt_41, F2.opt_42 AS F2_opt_42, F2.txt_43 AS F2_txt_43, F2.chk_44 AS F2_chk_44, " +
                                 "F2.txt_45 AS F2_txt_45, F2.opt_46 AS F2_opt_46, F2.txt_47 AS F2_txt_47, F2.opt_48 AS F2_opt_48, F2.txt_49 AS F2_txt_49, " +
                                 "F2.opt_50 AS F2_opt_50, F2.txt_51 AS F2_txt_51, F2.opt_52 AS F2_opt_52, F2.txt_53 AS F2_txt_53, F2.opt_1 AS F2_opt_1, F2.opt_2 AS F2_opt_2, " +
                                 "F5.info_date AS F5_info_date, F5.txt_1 AS F5_txt_1, F5.txt_3 AS F5_txt_3, F5.txt_4 AS F5_txt_4, F5.txt_5 AS F5_txt_5, F5.txt_6 AS F5_txt_6, " +
                                 "F11.info_date AS F11_info_date, F11.num_4 AS F11_num_4, F11.txt_5 AS F11_txt_5, F11.txt_6 AS F11_txt_6, F11.txt_7 AS F11_txt_7, F11.txt_8 AS F11_txt_8, " +
                                 "F11.txt_10 AS F11_txt_10, F11.txt_11 AS F11_txt_11, F11.txt_14 AS F11_txt_14, F11.txt_16 AS F11_txt_16, F11.txt_18 AS F11_txt_18, " +
                                 "F12.are_1 AS F12_are_1, F12.are_2 AS F12_are_2, F12.are_3 AS F12_are_3, F12.are_4 AS F12_are_4, F12.are_5 AS F12_are_5, " +
                                 "F12.opt_7 AS F12_opt_7, F12.txt_8 AS F12_txt_8, F12.dat_9 AS F12_txt_9, F12.txt_10 AS F12_txt_10, F12.txt_11 AS F12_txt_11, " +
                                 "F12.txt_12 AS F12_txt_12, F12.txt_13 AS F12_txt_13, F12.txt_14 AS F12_txt_14, F12.opt_15 AS F12_opt_15, F12.txt_16 AS F12_txt_16, F12.txt_17 AS F12_txt_17, F12.txt_18 AS F12_txt_18 " +
                            "FROM pat_info P " +
                            "LEFT JOIN zinfo_f_02 F2 " +
                              "ON CAST(P.pif_id AS char(10))=CAST(F2.pat_id AS char(10)) " +
                            "LEFT JOIN zinfo_f_05 F5 " +
                              "ON CAST(P.pif_id AS char(10))=CAST(F5.pat_id AS char(10)) " +
                            "LEFT JOIN zinfo_f_011 F11 " +
                              "ON CAST(P.pif_id AS char(10))=CAST(F11.pat_id AS char(10)) " +
                            "LEFT JOIN zinfo_f_012 F12 " +
                              "ON CAST(P.pif_id AS char(10))=CAST(F12.pat_id AS char(10)) " +
                           "WHERE P.pif_ic='" + sPAT_ID + "' ";

                    DataSetDialysis.report4_HEADDataTable dtPERSON = new DataSetDialysis.report4_HEADDataTable();
                    DataSetDialysis.report4_BODYDataTable dtEXAM = new DataSetDialysis.report4_BODYDataTable();

                    db.Fill(sql, dtPERSON);
                    if (dtPERSON.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPERSON.Rows.Count; i++)
                        {
                            switch (dtPERSON.Rows[i]["pif_sex"].ToString().ToUpper())
                            {
                                case "M":
                                    dtPERSON.Rows[i]["pif_sex"] = "男";
                                    break;
                                case "F":
                                    dtPERSON.Rows[i]["pif_sex"] = "女";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["pif_insurance"].ToString().Trim())
                            {
                                case "00000":
                                    dtPERSON.Rows[i]["pif_insurance"] = "自费";
                                    break;
                                case "00001":
                                    dtPERSON.Rows[i]["pif_insurance"] = "职工医保";
                                    break;
                                case "00002":
                                    dtPERSON.Rows[i]["pif_insurance"] = "居民医保";
                                    break;
                                case "00003":
                                    dtPERSON.Rows[i]["pif_insurance"] = "农合医保";
                                    break;
                                case "00004":
                                    dtPERSON.Rows[i]["pif_insurance"] = "省公费";
                                    break;
                                case "00005":
                                    dtPERSON.Rows[i]["pif_insurance"] = "市公费";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F12_opt_7"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F12_opt_7"] = "无";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F12_opt_7"] = "有";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F12_opt_15"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F12_opt_15"] = "颈内静脉";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F12_opt_15"] = "锁骨下静脉";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F12_opt_15"] = "股静脉";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_9"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_9"] = "良好";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_9"] = "中等";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_9"] = "不良";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_52"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_52"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_52"] = "异常  具体描述:" + dtPERSON.Rows[i]["F2_txt_53"].ToString().Trim();
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_50"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_50"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_50"] = "异常  具体描述:" + dtPERSON.Rows[i]["F2_txt_51"].ToString().Trim();
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_48"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_48"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_48"] = "异常  具体描述:" + dtPERSON.Rows[i]["F2_txt_49"].ToString().Trim();
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_46"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_46"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_46"] = "异常  具体描述:" + dtPERSON.Rows[i]["F2_txt_47"].ToString().Trim();
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_42"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_42"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_42"] = "减弱";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_42"] = "消失";
                                    break;
                                case "4":
                                    dtPERSON.Rows[i]["F2_opt_42"] = "部位";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_40"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_40"] = "质软";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_40"] = "质韧";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_40"] = "质硬";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_36"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_36"] = "无";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_36"] = "有  具体描述:" + dtPERSON.Rows[i]["F2_txt_37"].ToString().Trim();
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_35"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_35"] = "隆";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_35"] = "平";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_35"] = "凹";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_34"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_34"] = "无";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_34"] = "有";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_32"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_32"] = "无";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_32"] = "有  具体描述:" + dtPERSON.Rows[i]["F2_txt_33"].ToString().Trim();
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_31"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_31"] = "齐";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_31"] = "不齐";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_28"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_28"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_28"] = "扩大";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_26"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_26"] = "无";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_26"] = "有  具体描述:" + dtPERSON.Rows[i]["F2_txt_27"].ToString().Trim();
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_25"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_25"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_25"] = "增强";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_25"] = "减弱";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_24"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_24"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_24"] = "增大";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_23"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_23"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_23"] = "怒张";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_21"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_21"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_21"] = "异常  具体描述:" + dtPERSON.Rows[i]["F2_txt_22"].ToString().Trim();
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_20"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_20"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_20"] = "模糊";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_20"] = "光感";
                                    break;
                                case "4":
                                    dtPERSON.Rows[i]["F2_opt_20"] = "失明";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_18"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_18"] = "无";
                                    break;
                                case "2":
                                    switch (dtPERSON.Rows[i]["F2_opt_17"].ToString().Trim())
                                    {
                                        case "1":
                                            dtPERSON.Rows[i]["F2_opt_18"] = "轻  部位:  " + dtPERSON.Rows[i]["F2_txt_19"].ToString().Trim();
                                            break;
                                        case "2":
                                            dtPERSON.Rows[i]["F2_opt_18"] = "中  部位:  " + dtPERSON.Rows[i]["F2_txt_19"].ToString().Trim();
                                            break;
                                        case "3":
                                            dtPERSON.Rows[i]["F2_opt_18"] = "重  部位:  " + dtPERSON.Rows[i]["F2_txt_19"].ToString().Trim();
                                            break;
                                    }
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_16"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_16"] = "无";
                                    break;
                                case "2":
                                    switch (dtPERSON.Rows[i]["F2_opt_17"].ToString().Trim())
                                    {
                                        case "1":
                                            dtPERSON.Rows[i]["F2_opt_16"] = "轻";
                                            break;
                                        case "2":
                                            dtPERSON.Rows[i]["F2_opt_16"] = "中";
                                            break;
                                        case "3":
                                            dtPERSON.Rows[i]["F2_opt_16"] = "重";
                                            break;
                                    }
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_1"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_15"] = "正常";//?
                                    break;
                                case "2":
                                    switch (dtPERSON.Rows[i]["F2_opt_15"].ToString().Trim())
                                    {
                                        case "1":
                                            dtPERSON.Rows[i]["F2_opt_15"] = "皮疹";
                                            break;
                                        case "2":
                                            dtPERSON.Rows[i]["F2_opt_15"] = "出血";
                                            break;
                                        case "3":
                                            dtPERSON.Rows[i]["F2_opt_15"] = "灰暗";
                                            break;
                                        case "4":
                                            dtPERSON.Rows[i]["F2_opt_15"] = "尿素霜";
                                            break;
                                        case "5":
                                            dtPERSON.Rows[i]["F2_opt_15"] = "抓痕";
                                            break;
                                    }
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_13"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_13"] = "无";
                                    break;
                                case "2":
                                    switch (dtPERSON.Rows[i]["F2_opt_14"].ToString().Trim())
                                    {
                                        case "1":
                                            dtPERSON.Rows[i]["F2_opt_13"] = "轻";
                                            break;
                                        case "2":
                                            dtPERSON.Rows[i]["F2_opt_13"] = "中";
                                            break;
                                        case "3":
                                            dtPERSON.Rows[i]["F2_opt_13"] = "重";
                                            break;
                                    }
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_12"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_12"] = "平卧";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_12"] = "半卧";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_12"] = "端坐";
                                    break;
                                case "4":
                                    dtPERSON.Rows[i]["F2_opt_12"] = "自动体位";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_11"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_11"] = "清楚";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_11"] = "嗜睡";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_11"] = "昏睡";
                                    break;
                                case "4":
                                    dtPERSON.Rows[i]["F2_opt_11"] = "昏迷";
                                    break;
                                case "5":
                                    dtPERSON.Rows[i]["F2_opt_11"] = "谵妄";
                                    break;
                                case "6":
                                    dtPERSON.Rows[i]["F2_opt_11"] = "模糊";
                                    break;
                            }

                            switch (dtPERSON.Rows[i]["F2_opt_10"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_opt_10"] = "急性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_opt_10"] = "慢性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[i]["F2_opt_10"] = "痛苦";
                                    break;
                                case "4":
                                    dtPERSON.Rows[i]["F2_opt_10"] = "安静";
                                    break;
                            }

                            string chk_44 = "";
                            dtPERSON.Rows[i]["F2_chk_44"] = dtPERSON.Rows[i]["F2_chk_44"].ToString() + "0000";
                            if (dtPERSON.Rows[i]["F2_chk_44"].ToString().Trim().Substring(0, 1) == "1")
                                chk_44 += "畸形、";
                            if (dtPERSON.Rows[i]["F2_chk_44"].ToString().Trim().Substring(1, 1) == "1")
                                chk_44 += "叩痛、";
                            if (dtPERSON.Rows[i]["F2_chk_44"].ToString().Trim().Substring(2, 1) == "1")
                                chk_44 += "红肿、";
                            if (dtPERSON.Rows[i]["F2_chk_44"].ToString().Trim().Substring(3, 1) == "1")
                                chk_44 += "活动受限、";
                            if (chk_44 != "")
                                chk_44 = chk_44.Substring(0, chk_44.Length - 1);
                            switch (dtPERSON.Rows[i]["F2_opt_2"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[i]["F2_chk_44"] = "正常";
                                    break;
                                case "2":
                                    dtPERSON.Rows[i]["F2_chk_44"] = chk_44 + "    部位: " + dtPERSON.Rows[i]["F2_txt_45"].ToString().Trim();
                                    break;
                            }
                        }
                        dtPERSON.AcceptChanges();
                        string sPAT_NO = dtPERSON.Rows[0]["pif_id"].ToString();
                        /*
                                                sql = "SELECT A.PAT_NO AS pif_id, A.RESULT_DATE, A.RESULT_CODE, " +
                                                             "B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_NAME, B.RITEM_NAME_S, " +
                                                             "A.RESULT_VALUE_T AS RESULT_VALUE_O, " +
                                                             "B.RITEM_UNIT, A.RESULT_VALUE_N AS RESULT_VALUE_N, B.RITEM_LOW1, B.RITEM_HIGH1, " +
                                                             "CAST(CURRENT_DATE AS CHAR) AS RESULT_DAYS, A.ROW_ID " +
                                                        "FROM a_result_log A, (SELECT PAT_NO, MAX(RESULT_DATE) AS RESULT_DATE, RESULT_CODE " +
                                                                                "FROM a_result_log " +
                                                                               "WHERE PAT_NO=" + sPAT_NO + " " +
                                                                                 "AND RESULT_VER=0 " +
                                                                               "GROUP BY PAT_NO, RESULT_CODE) D " +
                                                        "LEFT JOIN a_ritem_setup B " +
                                                          "ON D.RESULT_CODE=B.RITEM_CODE " +
                                                         //"AND D.RESULT_DATE>=B.BEG_DATE " +
                                                         //"AND D.RESULT_DATE<=B.END_DATE " +
                                                       "WHERE A.PAT_NO=D.PAT_NO " +
                                                         "AND A.RESULT_DATE=D.RESULT_DATE " +
                                                         "AND A.RESULT_CODE=D.RESULT_CODE " +
                                                         "AND A.RESULT_VER=0 " +
                                                       "ORDER BY B.RITEM_CLASS, B.RITEM_TYPE, B.RITEM_SN, B.RITEM_CODE ";
                        */
                        sql = "SELECT * FROM zinfo_f_04 WHERE pat_id = " + sPAT_NO;

                        db.Fill(sql, dtEXAM);
                        dtEXAM.AcceptChanges();

                        System.Data.DataView dvEXAM = new System.Data.DataView();
                        dvEXAM = dtEXAM.DefaultView;
                        string sCODE = "";

                        sCODE = "4003";
                        //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                        if (dvEXAM.Count > 0)
                        {
                            dtPERSON.Rows[0]["DATE_4"] = dvEXAM[0]["dat_1"].ToString();
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_2"].ToString();
                            sCODE = "4004";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_3"].ToString();

                            sCODE = "4001";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_4"].ToString();
                            sCODE = "4055";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_5"].ToString();
                            sCODE = "4006";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_6"].ToString();

                            sCODE = "4056";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_8"].ToString();
                            sCODE = "4057";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_9"].ToString();

                            sCODE = "4059";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            switch (dvEXAM[0]["opt_13"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性" + dvEXAM[0]["txt_14"].ToString();
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性" + dvEXAM[0]["txt_14"].ToString();
                                    break;
                            }
                            //dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            sCODE = "4061";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_16"].ToString();
                            sCODE = "4048";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                        }

                        if (dvEXAM.Count > 0)
                        {
                            dtPERSON.Rows[0]["DATE_5"] = dvEXAM[0]["dat_15"].ToString();
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_17"].ToString();

                            sCODE = "4051";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_18"].ToString();
                            sCODE = "4017";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_19"].ToString();
                            sCODE = "4019";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_20"].ToString();

                            sCODE = "4018";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_21"].ToString();
                            sCODE = "4020";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_22"].ToString();
                            sCODE = "4021";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_23"].ToString();

                            sCODE = "4023";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_24"].ToString();
                            sCODE = "4011";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_25"].ToString();
                            sCODE = "4063";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_26"].ToString();
                            sCODE = "4064";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_27"].ToString();
                            sCODE = "4065";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_28"].ToString();
                            sCODE = "4066";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_29"].ToString();

                            sCODE = "4012";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_30"].ToString();
                            sCODE = "4067";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_31"].ToString();
                            sCODE = "4010";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_32"].ToString();

                            sCODE = "4008";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_33"].ToString();
                            sCODE = "4068";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_34"].ToString();
                            sCODE = "4069";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_35"].ToString();

                            sCODE = "4013";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_36"].ToString();
                            sCODE = "4014";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_37"].ToString();

                            sCODE = "4035";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_38"].ToString();
                            sCODE = "4034";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_39"].ToString();

                            sCODE = "4030";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_40"].ToString();
                            sCODE = "4053";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_41"].ToString();

                            sCODE = "4070";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_42"].ToString();

                            sCODE = "4027";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_43"].ToString();
                            sCODE = "4049";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_46"].ToString();

                            sCODE = "4026";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_47"].ToString();
                            sCODE = "4050";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_44"].ToString();

                            //<ext:Radio ID="opt_48_1" runat="server" FieldLabel="HbsAg" BoxLabel="阴性" Name="opt_48" />
                            //    ...
                            //<ext:Radio ID="opt_55_1" runat="server" FieldLabel="TP-XC" BoxLabel="阴性" Name="opt_55" />
                            //<ext:TextField ID="txt_56" runat="server" FieldLabel="其它"  />

                            sCODE = "4032";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            //    dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            switch (dvEXAM[0]["opt_48"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "弱阳性";
                                    break;
                                case "4":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "未检";
                                    break;
                            }

                            sCODE = "4037";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            //    dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            switch (dvEXAM[0]["opt_49"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "弱阳性";
                                    break;
                                case "4":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "未检";
                                    break;
                            }

                            sCODE = "4038";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            //    dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            switch (dvEXAM[0]["opt_50"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "弱阳性";
                                    break;
                                case "4":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "未检";
                                    break;
                            }

                            sCODE = "4072";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            //    dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            switch (dvEXAM[0]["opt_51"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "弱阳性";
                                    break;
                                case "4":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "未检";
                                    break;
                            }

                            sCODE = "4073";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            //    dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            switch (dvEXAM[0]["opt_52"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "弱阳性";
                                    break;
                                case "4":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "未检";
                                    break;
                            }

                            sCODE = "4033";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            //    dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            switch (dvEXAM[0]["opt_53"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "弱阳性";
                                    break;
                                case "4":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "未检";
                                    break;
                            }

                            sCODE = "4043";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            //    dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            switch (dvEXAM[0]["opt_54"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "弱阳性";
                                    break;
                                case "4":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "未检";
                                    break;
                            }

                            sCODE = "4044";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            //    dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["RESULT_VALUE_O"].ToString();
                            switch (dvEXAM[0]["opt_55"].ToString().Trim())
                            {
                                case "1":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阴性";
                                    break;
                                case "2":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "阳性";
                                    break;
                                case "3":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "弱阳性";
                                    break;
                                case "4":
                                    dtPERSON.Rows[0]["CODE_" + sCODE] = "未检";
                                    break;
                            }

                            sCODE = "5041";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_60"].ToString();
                            sCODE = "5042";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_61"].ToString();
                            sCODE = "5043";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_62"].ToString();
                            sCODE = "5044";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_63"].ToString();
                            sCODE = "5045";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_64"].ToString();
                            sCODE = "5046";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_65"].ToString();
                            sCODE = "5047";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_66"].ToString();
                            sCODE = "5048";
                            //dvEXAM.RowFilter = "RESULT_CODE='" + sCODE + "' ";
                            //if (dvEXAM.Count > 0)
                            dtPERSON.Rows[0]["CODE_" + sCODE] = dvEXAM[0]["txt_67"].ToString();
                        }

                    }
                    DataSet dsPERSON = new DataSet();
                    dsPERSON.Tables.Add(dtPERSON);
                    dsPERSON.Tables.Add(dtEXAM);
                    rpt.SetDataSource(dsPERSON);

                    #endregion
                }

                else if (sREPORT_NAME == "131")
                {
                    #region 阿拉善盟首次病歷報告
                    string sql = "SELECT P.pif_id, P.pif_ic, P.pif_name, P.pif_sex, P.pif_dob, P.pif_address, P.pif_insurance, ";
                    sql += "B.bgrp_grp, "; 
                    sql += "F11.num_4 AS Age, F11.txt_5 AS Nation, F11.txt_6 AS RoomNo, F11.txt_8 AS JobUnit, F11.txt_10 AS Telphone, F11.txt_12 AS ZipCode, ";                    
                    sql += "F12.txt_1 AS CheckinDate, F12.txt_2 AS F12txt2, F12.txt_3 AS F12txt3, F12.txt_3c AS F12txt3c, F12.txt_4 AS F12txt4, F12.txt_5 AS F12txt5, ";
                    sql += "F12.txt_6 AS F12txt6, F12.txt_7 AS F12txt7, F12.txt_8 AS F12txt8, F12.txt_9 AS F12txt9, F12.txt_10 AS F12txt10, F12.txt_11 AS F12txt11, ";
                    sql += "F12.txt_12 AS F12txt12, F12.txt_13 AS F12txt13, F12.txt_14 AS F12txt14, F12.txt_15 AS F12txt15, F12.txt_20 AS F12txt20, F12.txt_21 AS F12txt21, F12.txt_22 AS F12txt22, ";                    
                    sql += "F2.txt_1 AS F2txt1, F2.txt_2 AS F2txt2, F2.txt_3 AS F2txt3, F2.txt_4 AS F2txt4, F2.txt_5 AS F2txt5, F2.txt_6 AS F2txt6, ";
                    sql += "F2.txt_7 AS F2txt7, F2.txt_8 AS F2txt8, F2.txt_9 AS F2txt9, F2.txt_10 AS F2txt10, F2.txt_11 AS F2txt11, F2.txt_12 AS F2txt12, ";
                    sql += "F2.txt_13 AS F2txt13, F2.txt_14 AS F2txt14, F2.txt_15 AS F2txt15, F2.txt_16 AS F2txt16, F2.txt_17 AS F2txt17, F2.txt_18 AS F2txt18, ";
                    sql += "F2.txt_19 AS F2txt19, F2.txt_20 AS F2txt20, F2.txt_21 AS F2txt21, F2.txt_22 AS F2txt22, ";
                    sql += "F4.txt_1 AS F4txt1, F4.txt_2 AS F4txt2, F4.txt_3 AS F4txt3, F4.txt_4 AS F4txt4, F4.txt_5 AS F4txt5, F4.txt_6 AS F4txt6, F4.txt_7 AS F4txt7, F4.txt_8 AS F4txt8, F4.txt_9 AS F4txt9, F4.txt_10 AS F4txt10, ";
                    sql += "F4.txt_11 AS F4txt11, F4.txt_12 AS F4txt12, F4.txt_13 AS F4txt13, F4.txt_14 AS F4txt14, F4.txt_15 AS F4txt15, F4.txt_16 AS F4txt16, F4.txt_17 AS F4txt17, F4.txt_18 AS F4txt18, F4.txt_19 AS F4txt19, F4.txt_20 AS F4txt20, ";
                    sql += "F4.txt_21 AS F4txt21, F4.txt_22 AS F4txt22, F4.txt_23 AS F4txt23, F4.txt_24 AS F4txt24, F4.txt_25 AS F4txt25, F4.txt_26 AS F4txt26, F4.txt_27 AS F4txt27, F4.txt_28 AS F4txt28, F4.txt_29 AS F4txt29, F4.txt_30 AS F4txt30, ";
                    sql += "F4.txt_31 AS F4txt31, F4.txt_32 AS F4txt32, F4.txt_33 AS F4txt33, F4.txt_34 AS F4txt34, F4.txt_35 AS F4txt35, F4.txt_36 AS F4txt36, F4.txt_37 AS F4txt37, F4.txt_38 AS F4txt38, F4.txt_39 AS F4txt39, F4.txt_40 AS F4txt40, ";
                    sql += "F4.txt_41 AS F4txt41, F4.txt_42 AS F4txt42, F4.txt_43 AS F4txt43, F4.txt_44 AS F4txt44, F4.txt_45 AS F4txt45, F4.txt_46 AS F4txt46, F4.txt_47 AS F4txt47, F4.txt_48 AS F4txt48, F4.txt_49 AS F4txt49, F4.txt_50 AS F4txt50, ";
                    sql += "F4.txt_51 AS F4txt51, F4.txt_52 AS F4txt52, F4.txt_53 AS F4txt53, F4.txt_54 AS F4txt54, F4.txt_55 AS F4txt55, F4.txt_56 AS F4txt56, F4.txt_57 AS F4txt57, F4.txt_58 AS F4txt58, F4.txt_59 AS F4txt59, F4.txt_60 AS F4txt60, ";
                    sql += "F4.txt_61 AS F4txt61, F4.txt_62 AS F4txt62, F4.txt_63 AS F4txt63, F4.txt_64 AS F4txt64, F4.txt_65 AS F4txt65, F4.txt_66 AS F4txt66, F4.txt_67 AS F4txt67, ";                    
                    sql += "F5.txt_1 AS F5txt1, F5.txt_2 AS F5txt2, F5.txt_3 AS F5txt3, F5.txt_4 AS F5txt4, F5.txt_5 AS F5txt5, F5.txt_6 AS F5txt6 ";
                    sql += "FROM pat_info P ";
                    sql += "LEFT JOIN blood_group B ON P.pif_ic=B.bgrp_patic ";
                    sql += "LEFT JOIN zinfo_f_011 F11 ON P.pif_id=F11.pat_id ";
                    sql += "LEFT JOIN zinfo_f_012_alasamo F12 ON P.pif_id=F12.pat_id ";
                    sql += "LEFT JOIN zinfo_f_02_alasamo F2 ON P.pif_id=F2.pat_id ";
                    sql += "LEFT JOIN zinfo_f_04_alasamo F4 ON P.pif_id=F4.pat_id ";
                    sql += "LEFT JOIN zinfo_f_05 F5 ON P.pif_id=F5.pat_id ";
                    sql += "WHERE P.pif_id='" + sPAT_ID + "' ";

                    DataSetDialysis.dialysis_first_HEADDataTable dtPERSON = new DataSetDialysis.dialysis_first_HEADDataTable();
                    //DataSetDialysis.dialysis_first_BODYDataTable dtEXAM = new DataSetDialysis.dialysis_first_BODYDataTable();

                    db.Fill(sql, dtPERSON);
                    if (dtPERSON.Rows.Count > 0)
                    {
                        for (int i = 0; i < dtPERSON.Rows.Count; i++)
                        {
                            if (dtPERSON.Rows[i]["pif_sex"].ToString() == "M")
                                dtPERSON.Rows[i]["pif_sex"] = "男";
                            else if (dtPERSON.Rows[i]["pif_sex"].ToString() == "F")
                                dtPERSON.Rows[i]["pif_sex"] = "女";
                            dtPERSON.Rows[i]["pif_dob"] = _Get_Cht_YMD(dtPERSON.Rows[i]["pif_dob"].ToString());
                            dtPERSON.Rows[i]["Age"] = dtPERSON.Rows[i]["Age"].ToString() + "岁";

                            dtPERSON.AcceptChanges();
                            string sql1 = "SELECT ins_name FROM ins_setup WHERE ins_code='" + dtPERSON.Rows[i]["pif_insurance"].ToString() + "'";
                            DataTable dt1 = db.Query(sql1);
                            if (dt1.Rows.Count > 0)
                            {
                                dtPERSON.Rows[i]["pif_insurance"] = dt1.Rows[0]["ins_name"].ToString();
                            }
                            dt1.Dispose();

                            dtPERSON.Rows[i]["CheckinDate"] = _Get_Cht_YMDHM(dtPERSON.Rows[i]["CheckinDate"].ToString());

                            string tpic = Server.MapPath("../images/True.jpg");
                            string fpic = Server.MapPath("../images/False.jpg");
                            if (dtPERSON.Rows[i]["F2txt14"].ToString() == "1")
                            {
                                rpt.DataDefinition.FormulaFields["F2_141"].Text = "ToText('" + tpic + "')";
                                rpt.DataDefinition.FormulaFields["F2_142"].Text = "ToText('" + fpic + "')";
                            }
                            else if (dtPERSON.Rows[i]["F2txt14"].ToString() == "2")
                            {
                                rpt.DataDefinition.FormulaFields["F2_141"].Text = "ToText('" + fpic + "')";
                                rpt.DataDefinition.FormulaFields["F2_142"].Text = "ToText('" + tpic + "')";
                            }
                            else
                            {
                                rpt.DataDefinition.FormulaFields["F2_141"].Text = "ToText('" + fpic + "')";
                                rpt.DataDefinition.FormulaFields["F2_142"].Text = "ToText('" + fpic + "')";
                            }

                            if (dtPERSON.Rows[i]["F2txt15"].ToString() == "1")
                            {
                                rpt.DataDefinition.FormulaFields["F2_151"].Text = "ToText('" + tpic + "')";
                                rpt.DataDefinition.FormulaFields["F2_152"].Text = "ToText('" + fpic + "')";
                            }
                            else if (dtPERSON.Rows[i]["F2txt15"].ToString() == "2")
                            {
                                rpt.DataDefinition.FormulaFields["F2_151"].Text = "ToText('" + fpic + "')";
                                rpt.DataDefinition.FormulaFields["F2_152"].Text = "ToText('" + tpic + "')";
                            }
                            else 
                            {
                                rpt.DataDefinition.FormulaFields["F2_151"].Text = "ToText('" + fpic + "')";
                                rpt.DataDefinition.FormulaFields["F2_152"].Text = "ToText('" + fpic + "')";
                            }
                            
                            rpt.DataDefinition.FormulaFields["F2_161"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_162"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_163"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_164"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_165"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_166"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_171"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_172"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_173"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_174"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_175"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_176"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_181"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_182"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_183"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_184"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_185"].Text = "ToText('" + fpic + "')";
                            rpt.DataDefinition.FormulaFields["F2_186"].Text = "ToText('" + fpic + "')";
                            string[] RadioItem = dtPERSON.Rows[i]["F2txt16"].ToString().Split(new Char[] { '|' });
                            for (int r = 0; r < RadioItem.Length; r++)
                            {
                                if (RadioItem[r] == "1")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_161"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_162"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "2")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_161"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_162"].Text = "ToText('" + tpic + "')";
                                }
                                else if (RadioItem[r] == "3")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_163"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_164"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "4")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_163"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_164"].Text = "ToText('" + tpic + "')";
                                }
                                else if (RadioItem[r] == "5")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_165"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_166"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "6")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_165"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_166"].Text = "ToText('" + tpic + "')";
                                }
                            }

                            RadioItem = dtPERSON.Rows[i]["F2txt17"].ToString().Split(new Char[] { '|' });
                            for (int r = 0; r < RadioItem.Length; r++)
                            {
                                if (RadioItem[r] == "1")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_171"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_172"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "2")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_171"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_172"].Text = "ToText('" + tpic + "')";
                                }
                                else if (RadioItem[r] == "3")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_173"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_174"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "4")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_173"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_174"].Text = "ToText('" + tpic + "')";
                                }
                                else if (RadioItem[r] == "5")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_175"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_176"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "6")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_175"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_176"].Text = "ToText('" + tpic + "')";
                                }
                            }

                            RadioItem = dtPERSON.Rows[i]["F2txt18"].ToString().Split(new Char[] { '|' });
                            for (int r = 0; r < RadioItem.Length; r++)
                            {
                                if (RadioItem[r] == "1")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_181"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_182"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "2")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_181"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_182"].Text = "ToText('" + tpic + "')";
                                }
                                else if (RadioItem[r] == "3")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_183"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_184"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "4")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_183"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_184"].Text = "ToText('" + tpic + "')";
                                }
                                else if (RadioItem[r] == "5")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_185"].Text = "ToText('" + tpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_186"].Text = "ToText('" + fpic + "')";
                                }
                                else if (RadioItem[r] == "6")
                                {
                                    rpt.DataDefinition.FormulaFields["F2_185"].Text = "ToText('" + fpic + "')";
                                    rpt.DataDefinition.FormulaFields["F2_186"].Text = "ToText('" + tpic + "')";
                                }
                            }

                            if (dtPERSON.Rows[i]["F4txt46"].ToString() == "1")
                            {
                                rpt.DataDefinition.FormulaFields["HCV_01"].Text = "ToText('" + tpic + "')";
                                rpt.DataDefinition.FormulaFields["HCV_02"].Text = "ToText('" + fpic + "')";
                            }
                            else if (dtPERSON.Rows[i]["F4txt46"].ToString() == "2")
                            {
                                rpt.DataDefinition.FormulaFields["HCV_01"].Text = "ToText('" + fpic+ "')";
                                rpt.DataDefinition.FormulaFields["HCV_02"].Text = "ToText('" + tpic + "')";
                            }
                            else
                            {
                                rpt.DataDefinition.FormulaFields["HCV_01"].Text = "ToText('" + fpic+ "')";
                                rpt.DataDefinition.FormulaFields["HCV_02"].Text = "ToText('" + fpic + "')";
                            }

                            if (dtPERSON.Rows[i]["F4txt47"].ToString() == "1")
                            {
                                rpt.DataDefinition.FormulaFields["HCV_03"].Text = "ToText('" + tpic + "')";
                                rpt.DataDefinition.FormulaFields["HCV_04"].Text = "ToText('" + fpic + "')";
                            }
                            else if (dtPERSON.Rows[i]["F4txt47"].ToString() == "2")
                            {
                                rpt.DataDefinition.FormulaFields["HCV_03"].Text = "ToText('" + fpic + "')";
                                rpt.DataDefinition.FormulaFields["HCV_04"].Text = "ToText('" + tpic + "')";
                            }
                            else
                            {
                                rpt.DataDefinition.FormulaFields["HCV_03"].Text = "ToText('" + fpic + "')";
                                rpt.DataDefinition.FormulaFields["HCV_04"].Text = "ToText('" + fpic + "')";
                            }
                            //db.Fill(sql, dtEXAM);
                            //dtEXAM.AcceptChanges();
                            //System.Data.DataView dvEXAM = new System.Data.DataView();
                            //dvEXAM = dtEXAM.DefaultView;
                        }
                    }
                    DataSet dsPERSON = new DataSet();
                    dsPERSON.Tables.Add(dtPERSON);
                    //dsPERSON.Tables.Add(dtEXAM);
                    rpt.SetDataSource(dsPERSON);
                    #endregion
                }

                else if (sREPORT_NAME == "f6a")
                {
                    #region 阿拉善盟首次病歷報告
                    string sql = "SELECT P.pif_ic, P.pif_name, P.pif_sex, ";
                    sql += "F11.num_4 AS Age, F11.txt_5 AS Nation, F11.txt_6 AS RoomNo, ";
                    sql += "F6.* ";
                    sql += "FROM pat_info P ";
                    sql += "LEFT JOIN zinfo_f_011 F11 ON P.pif_id=F11.pat_id ";
                    sql += "LEFT JOIN zinfo_f_06_alasamo F6 ON P.pif_id=F6.pat_id ";
                    sql += "WHERE P.pif_id='" + sPAT_ID + "' ";

                    DataSetDialysis.zinfo_f06DataTable dtPERSON = new DataSetDialysis.zinfo_f06DataTable();
                    db.Fill(sql, dtPERSON);
                    if (dtPERSON.Rows.Count > 0)
                    {
                        if (dtPERSON.Rows[0]["pif_sex"].ToString() == "M")
                            dtPERSON.Rows[0]["pif_sex"] = "男";
                        else if (dtPERSON.Rows[0]["pif_sex"].ToString() == "F")
                            dtPERSON.Rows[0]["pif_sex"] = "女";
                        dtPERSON.Rows[0]["Age"] = dtPERSON.Rows[0]["Age"].ToString() + "岁";
                        dtPERSON.AcceptChanges();
                    }
                    DataSet dsPERSON = new DataSet();
                    dsPERSON.Tables.Add(dtPERSON);
                    rpt.SetDataSource(dsPERSON);
                    #endregion
                }

                else if (sREPORT_NAME == "95")
                {
                    #region sREPORT_NAME == "95"

                    string MyUser = "";
                    MyUser = Session["_USER_ID"].ToString();
                    string sql = "";
                    DataSet1.zinfo_h_05DataTable dt_h05 = new DataSet1.zinfo_h_05DataTable();
                    DataSet ds = new DataSet();
                    sql = " select    '" + MyUser + "'    as user_id , a.*,b.pif_ic , b.pif_name from  zinfo_h_05  a ";
                    sql = sql + " left join  pat_info b  on a.pat_id  = b.pif_id where pat_id = '" + sPAT_ID + "' ";
                    //DataTable dt_h051 = db.Query(sql);
                    db.Fill(sql, dt_h05);
                    ds.Tables.Add(dt_h05);
                    rpt.SetDataSource(ds);

                    #endregion
                }

                else if (sREPORT_NAME == "96")
                {
                    #region sREPORT_NAME == "96"

                    string sql = "";
                    DataSet1.zinfo_h_06DataTable dt_h06 = new DataSet1.zinfo_h_06DataTable();
                    DataSet ds = new DataSet();
                    sql = " select    a.*, b.pif_ic , b.pif_name from  zinfo_h_06  a ";
                    sql = sql + " left join  pat_info b  on a.pat_id  = b.pif_id where pat_id = '" + sPAT_ID + "' ";
                    //DataTable dt_h051 = db.Query(sql);
                    db.Fill(sql, dt_h06);
                    ds.Tables.Add(dt_h06);
                    rpt.SetDataSource(ds);

                    #endregion
                }

                else if (sREPORT_NAME == "97")
                {
                    #region sREPORT_NAME == "97"

                    string sql = "";
                    DataSet1.zinfo_h_07DataTable dt_h07 = new DataSet1.zinfo_h_07DataTable();
                    DataSet ds = new DataSet();
                    sql = " select    a.*, b.pif_ic , b.pif_name from  zinfo_h_07  a ";
                    sql = sql + " left join  pat_info b  on a.pat_id  = b.pif_id where pat_id = '" + sPAT_ID + "' ";
                    //DataTable dt_h051 = db.Query(sql);
                    db.Fill(sql, dt_h07);
                    ds.Tables.Add(dt_h07);
                    rpt.SetDataSource(ds);
                    #endregion
                }
                else if (sREPORT_NAME == "99")
                {
                    #region sREPORT_NAME == "99"
                    string sql = string.Empty;
                    DataTable dtEva_Data;
                    DataSetDialysis.zinfo_h_08DataTable dt_h08 = new DataSetDialysis.zinfo_h_08DataTable();
                    dtEva_Data = dt_h08;
                    DataSet ds = new DataSet();
                    try
                    {
                        sql = "SELECT a.* " + "FROM zinfo_h_08 a ";
                        sql += "WHERE a.pat_id='" + sPAT_ID +"'";
                        sql += " AND a.info_date='" + sINFO_DATE + "'";
                        sql += " ORDER BY info_date";
                        db.Fill(sql, dtEva_Data);
                        //dtEva_Data.AcceptChanges();
                        ds.Tables.Add(dtEva_Data);
                        rpt.SetDataSource(ds);
                    }
                    catch (DataSourceException ex)
                    {
                        string errorMsg = ex.Message.ToString();
                    }
                    #endregion
                }
                else if (sREPORT_NAME == "100")
                {
                    #region sREPORT_NAME == "100"
                    string sql = string.Empty;
                    DataTable dtRecData;
                    DataSetDialysis.dialysis_report_e04DataTable dt_e04 = new DataSetDialysis.dialysis_report_e04DataTable();
                    dtRecData = dt_e04;
                    DataSet ds = new DataSet();
                    try
                    {
                        sql = "SELECT pif_ic, pat_date, pat_time, pat_note, pat_emp FROM pat_patrol ";
                        sql += "WHERE pif_ic='" + sPAT_IC + "' AND pat_date BETWEEN '" + sBEG_DATE + "' AND '" + sEND_DATE + "' ";
                        sql += "ORDER BY pat_date";
                        db.Fill(sql, dt_e04);
                        ds.Tables.Add(dt_e04);
                        rpt.SetDataSource(ds);
                        rpt.DataDefinition.FormulaFields["PAT_NAME"].Text = "ToText('" + _PIF_NAME + "')";
                    }
                    catch (DataSourceException ex)
                    {
                        string errorMsg = ex.Message.ToString();
                    }
                    #endregion
                }
                else if (sREPORT_NAME == "s01" || sREPORT_NAME == "s02")
                {
                    #region 病患材料单
                    DataSet ds1 = new DataSet();
                    string pltime = sREPORT_P;
                    string weekday = GetWeek(sINFO_DATE);

                    string sql1 = "SELECT dyivl_item AS pname, dyivl_qty AS cnt FROM dailyiv_itemlist ";
                    sql1 += "WHERE dyivl_ivdate='" + sINFO_DATE + "' AND dyivl_qty!='' ";

                    string sql2 = "SELECT ivpl_id, ivpl_date, ivpl_serialno, ivpl_patname, ivpl_iv1, ivpl_iv2, ivpl_iv3, ivpl_iv4, ivpl_bedno, ivpl_mtyp, ivpl_flr, ";
                    sql2 += "CASE ivpl_time when '001' THEN '上午' when '002' THEN '下午' when '003' THEN '晚班' end AS timename, ";
                    sql2 += "CONCAT(ivpl_iv5,'\n\r',ivpl_iv7) AS Dialyzer, CONCAT(ivpl_iv6,'\n\r',ivpl_iv8) AS Tube, ivpl_ivs1 AS Special, ivpl_col5, ivpl_col9 ";
                    sql2 += "FROM ivpat_list ";
                    sql2 += "WHERE ivpl_date='" + sINFO_DATE + "' ";
                    if (pltime != null && pltime != "")
                        sql2 += "AND ivpl_time='" + pltime + "' ";
                    sql2 += "ORDER BY ivpl_bedno";
                    if (sREPORT_NAME == "s01")
                    {
                        rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('病患材料清单')";
                    }
                    else if (sREPORT_NAME == "s02")
                    {
                        rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('每日病患材料单')";
                    }
                    GeneratePickMaterialAndDrugReport02(rpt, sINFO_DATE, sql1, sql2);
                    #endregion
                }
                else if (sREPORT_NAME == "s03") 
                {
                    # region 领料单

                    DataSet ds1 = new DataSet();
                    string sno = sREPORT_P;
                    string pickDate = sINFO_DATE;
                    string SQLT_SEL = @"
                        SELECT dyivl_no as no, dyivl_item as pname, right_qty as cnt
                        FROM dailyiv_itemlist
                        WHERE dyivl_serialno = '{0}';";
                    DataSetDialysis.Report_TotalMedDataTable dt = new DataSetDialysis.Report_TotalMedDataTable();
                    DataSet ds = new DataSet();
                    string sql = string.Format(SQLT_SEL, sno);

                    db.Fill(sql, dt);
                    ds.Tables.Add(dt);
                    rpt.SetDataSource(ds);

                    // set report varaibels
                    rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('材料领料单')";
                    rpt.DataDefinition.FormulaFields["SERIAL_NO"].Text = "ToText('" + sno + "')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + pickDate + "')";

                    #endregion
                }
                else if (sREPORT_NAME == "s04")
                {
                    # region 材料退料单

                    DataSet ds1 = new DataSet();
                    string sno = sREPORT_P;
                    string pickDate = sINFO_DATE;
                    string SQLT_SEL = @"
                        SELECT invr_no as no, invr_name as pname, invr_preamt as cnt, invr_rtnamt as ret_amt
                        FROM inv_return
                        WHERE invr_serialno = '{0}';";
                    DataSetDialysis.Report_TotalMedDataTable dt = new DataSetDialysis.Report_TotalMedDataTable();
                    DataSet ds = new DataSet();
                    string sql = string.Format(SQLT_SEL, sno);

                    db.Fill(sql, dt);
                    ds.Tables.Add(dt);
                    rpt.SetDataSource(ds);

                    // set report varaibels
                    rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('材料退料单')";
                    rpt.DataDefinition.FormulaFields["SERIAL_NO"].Text = "ToText('" + sno + "')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + pickDate + "')";

                    #endregion
                }
                else if (sREPORT_NAME == "s05")
                {
                    # region 药品领料单
                    DataSet ds1 = new DataSet();
                    string sno = sREPORT_P;
                    string pickDate = sINFO_DATE;
                    string SQLT_SEL = @"SELECT dyivl_no as no, dyivl_item as pname, right_qty as cnt FROM drug_itemlist
                        WHERE dyivl_ivdate = '{0}';";
                    DataSetDialysis.Report_TotalMedDataTable dt = new DataSetDialysis.Report_TotalMedDataTable();
                    DataSet ds = new DataSet();
                    string sql = string.Format(SQLT_SEL, pickDate);

                    db.Fill(sql, dt);
                    ds.Tables.Add(dt);
                    rpt.SetDataSource(ds);

                    // set report varaibels
                    rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('药品领料单')";
                    rpt.DataDefinition.FormulaFields["SERIAL_NO"].Text = "ToText('" + sno + "')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + pickDate + "')";
                    #endregion
                }
                else if (sREPORT_NAME == "E01")
                {
                    # region 评鉴质量指标
                    DataSetDialysis.MachineKindDataTable dtMachineKind = new DataSetDialysis.MachineKindDataTable();
                    DataTable dtLIST_BODY = dtMachineKind;
                    DataSet ds = new DataSet();

                    DataSetDialysis.DeadListDataTable dtDeadList = new DataSetDialysis.DeadListDataTable();
                    DataTable dtDeadList_BODY = dtDeadList;
                    DataSet dsDeadList = new DataSet();

                    string sSQL = "";
                    DataTable dt = new DataTable();
                    string pickDate = sINFO_DATE;
                    string toYear = pickDate.Substring(0, 4);

                    //血透機廠牌分類數
                    sSQL = "SELECT a.mac_brand AS Bno, count(a.mac_brand) AS Quantity, b.genst_desc AS Machine FROM mac_setup a ";
                    sSQL += "LEFT JOIN general_setup b ON b.genst_ctg='macbrd' AND b.genst_code=a.mac_brand ";
                    sSQL += "GROUP BY a.mac_brand";
                    db.Fill(sSQL, dtLIST_BODY);
                    ds.Tables.Add(dtLIST_BODY);
                    rpt.SetDataSource(ds);

                    int TotalMachine = 0;
                    foreach (DataRow dr in dtLIST_BODY.Rows)
                    {
                        TotalMachine += int.Parse(dr["Quantity"].ToString());
                    }
                    rpt.DataDefinition.FormulaFields["TotalMachine"].Text = "ToText('" + TotalMachine.ToString() + "台')";

                    //專職醫師與護理人員數
                    sSQL = "SELECT a.type, count(a.type) AS cnt FROM access_level a ";
                    sSQL += "WHERE a.type='DC' OR a.type='DH' OR a.type='HN' OR a.type='NU' ";
                    sSQL += "GROUP BY a.type";
                    dt = db.Query(sSQL);
                    int doc = 0, nus = 0;
                    foreach(DataRow dr in dt.Rows)
                    {
                        if (dr["type"].ToString() == "DC" || dr["type"].ToString() == "DH")
                            doc += int.Parse(dr["cnt"].ToString());
                        if (dr["type"].ToString() == "NU" || dr["type"].ToString() == "HN")
                            nus += int.Parse(dr["cnt"].ToString());
                    }
                    rpt.DataDefinition.FormulaFields["DocPeople"].Text = "ToText('" + doc.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["NusPeople"].Text = "ToText('" + nus.ToString() + "人')";

                    //年度血透總例數陰陽性數
                    sSQL = "SELECT SUBSTR(a.cln1_diadate,1,7) AS Mon, count(a.cln1_diadate) AS cnt FROM clinical1_nurse a ";
                    sSQL += "WHERE SUBSTR(a.cln1_diadate,1,4)='" + toYear + "' ";
                    sSQL += "GROUP BY SUBSTR(a.cln1_diadate,1,7)";
                    dt = db.Query(sSQL);
                    int M1 = 0, M2 = 0, M3 = 0, M4 = 0, M5 = 0, M6 = 0, M7 = 0, M8 = 0, M9 = 0, M10 = 0, M11 = 0, M12 = 0;
                    int TotalM = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["Mon"].ToString().Substring(5, 2) == "01")
                            M1 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "02")
                            M2 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "03")
                            M3 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "04")
                            M4 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "05")
                            M5 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "06")
                            M6 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "07")
                            M7 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "08")
                            M8 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "09")
                            M9 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "10")
                            M10 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "11")
                            M11 = int.Parse(dr["cnt"].ToString());
                        if (dr["Mon"].ToString().Substring(5, 2) == "12")
                            M12 = int.Parse(dr["cnt"].ToString());
                    }
                    TotalM = M1 + M2 + M3 + M4 + M5 + M6 + M7 + M8 + M9 + M10 + M11 + M12;
                    rpt.DataDefinition.FormulaFields["DM1"].Text = "ToText('0/" + M1.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM2"].Text = "ToText('0/" + M2.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM3"].Text = "ToText('0/" + M3.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM4"].Text = "ToText('0/" + M4.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM5"].Text = "ToText('0/" + M5.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM6"].Text = "ToText('0/" + M6.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM7"].Text = "ToText('0/" + M7.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM8"].Text = "ToText('0/" + M8.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM9"].Text = "ToText('0/" + M9.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM10"].Text = "ToText('0/" + M10.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM11"].Text = "ToText('0/" + M11.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["DM12"].Text = "ToText('0/" + M12.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["TotalM"].Text = "ToText('" + TotalM.ToString() + "人')";

                    //年度血透總例數透析類型數
                    sSQL = "SELECT a.cln1_col3, count(a.cln1_col3) AS cnt FROM clinical1_nurse a ";
                    sSQL += "WHERE SUBSTR(a.cln1_diadate,1,4)='" + toYear + "' ";
                    sSQL += "GROUP BY a.cln1_col3";
                    dt = db.Query(sSQL);
                    int HD = 0, HFD = 0, HDF = 0, HF = 0, TotalHD = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["cln1_col3"].ToString() == "HD")
                            HD += int.Parse(dr["cnt"].ToString());
                        if (dr["cln1_col3"].ToString() == "HFD")
                            HFD += int.Parse(dr["cnt"].ToString());
                        if (dr["cln1_col3"].ToString() == "HDF")
                            HDF += int.Parse(dr["cnt"].ToString());
                        if (dr["cln1_col3"].ToString() == "HF")
                            HF += int.Parse(dr["cnt"].ToString());
                    }
                    rpt.DataDefinition.FormulaFields["HD"].Text = "ToText('" + HD.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["HFD"].Text = "ToText('" + HFD.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["HDF"].Text = "ToText('" + HDF.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["HF"].Text = "ToText('" + HF.ToString() + "人')";
                    TotalHD = HD + HFD + HDF + HF;
                    rpt.DataDefinition.FormulaFields["TotalDH"].Text = "ToText('" + TotalHD.ToString() + "人')";

                    //年度透析人數
                    sSQL = "SELECT count(DISTINCT a.cln1_patic) AS cnt, ";
                    sSQL += "(SELECT count(*) FROM zinfo_a_07 d WHERE SUBSTR(d.info_date,1,4)='" + toYear + "') AS dead ";
                    sSQL += "FROM clinical1_nurse a ";
                    sSQL += "WHERE SUBSTR(a.cln1_diadate,1,4)='" + toYear + "' ";
                    dt = db.Query(sSQL);
                    float d_times = 0, dead = 0, dead_per = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        d_times = float.Parse(dr["cnt"].ToString());
                        dead = float.Parse(dr["dead"].ToString());
                        dead_per = dead / d_times;
                    }
                    rpt.DataDefinition.FormulaFields["DEAD"].Text = "ToText('" + dead.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["DEAD_P"].Text = "ToText('" + String.Format("{0:P2}", dead_per) + "')";


                    sSQL = "SELECT b.pif_name AS pat_name, a.info_date AS dead_date, a.txt_3 AS Memo FROM zinfo_a_07 a ";
                    sSQL += "LEFT JOIN pat_info b ON b.pif_id=a.pat_id ";
                    sSQL += "WHERE SUBSTR(a.info_date,1,4)='" + toYear + "' ";
                    db.Fill(sSQL, dtDeadList_BODY);
                    dsDeadList.Tables.Add(dtDeadList_BODY);
                    rpt.SetDataSource(dsDeadList);

                    //年度血壓控制(透析期間血壓90/60~150/90)
                    string sSQL15 = "SELECT count(*) FROM clinical3_nurse a ";
                    sSQL15 += "WHERE a.cln3_a1>=90 AND a.cln3_a2<=150 AND a.cln3_b1>=60 AND a.cln3_b2<=90 ";

                    //年度腹膜透析歷次
                    string sSQL18 = "SELECT count(*) from clinical1_nurse a ";
                    sSQL18 += "WHERE a.cln3_a1>=90 AND a.cln3_a2<=150 AND a.cln3_b1>=60 AND a.cln3_b2<=90 ";

                    // set report varaibels
                    rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('评鉴质量指标')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + pickDate + "')";

                    #endregion
                }
                else if (sREPORT_NAME == "E02")
                {
                    # region 评鉴质量指标
                    DataSetDialysis.MachineKindDataTable dtMachineKind = new DataSetDialysis.MachineKindDataTable();
                    DataTable dtLIST_BODY = dtMachineKind;
                    DataSet ds = new DataSet();

                    DataSetDialysis.DeadListDataTable dtDeadList = new DataSetDialysis.DeadListDataTable();
                    DataTable dtDeadList_BODY = dtDeadList;
                    DataSet dsDeadList = new DataSet();

                    string sSQL = "";
                    DataTable dt = new DataTable();
                    string pickDate = sINFO_DATE;
                    string toYearMonth = pickDate.Substring(0, 7);

                    //血透機廠牌分類數
                    sSQL = "SELECT a.mac_brand AS Bno, count(a.mac_brand) AS Quantity, b.genst_desc AS Machine FROM mac_setup a ";
                    sSQL += "LEFT JOIN general_setup b ON b.genst_ctg='macbrd' AND b.genst_code=a.mac_brand ";
                    sSQL += "GROUP BY a.mac_brand";
                    db.Fill(sSQL, dtLIST_BODY);
                    ds.Tables.Add(dtLIST_BODY);
                    rpt.SetDataSource(ds);

                    int TotalMachine = 0;
                    foreach (DataRow dr in dtLIST_BODY.Rows)
                    {
                        TotalMachine += int.Parse(dr["Quantity"].ToString());
                    }
                    rpt.DataDefinition.FormulaFields["TotalMachine"].Text = "ToText('" + TotalMachine.ToString() + "台')";

                    //專職醫師與護理人員數
                    sSQL = "SELECT a.type, count(a.type) AS cnt FROM access_level a ";
                    sSQL += "WHERE a.type='DC' OR a.type='DH' OR a.type='HN' OR a.type='NU' ";
                    sSQL += "GROUP BY a.type";
                    dt = db.Query(sSQL);
                    int doc = 0, nus = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["type"].ToString() == "DC" || dr["type"].ToString() == "DH")
                            doc += int.Parse(dr["cnt"].ToString());
                        if (dr["type"].ToString() == "NU" || dr["type"].ToString() == "HN")
                            nus += int.Parse(dr["cnt"].ToString());
                    }
                    rpt.DataDefinition.FormulaFields["DocPeople"].Text = "ToText('" + doc.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["NusPeople"].Text = "ToText('" + nus.ToString() + "人')";

                    //年度血透總例數陰陽性數
                    sSQL = "SELECT SUBSTR(a.cln1_diadate,1,7) AS Mon, count(a.cln1_diadate) AS cnt FROM clinical1_nurse a ";
                    sSQL += "WHERE SUBSTR(a.cln1_diadate,1,7)='" + toYearMonth + "' ";
                    sSQL += "GROUP BY SUBSTR(a.cln1_diadate,1,7)";
                    dt = db.Query(sSQL);
                    int M1 = 0, M2 = 0, M3 = 0, M4 = 0, M5 = 0, M6 = 0, M7 = 0, M8 = 0, M9 = 0, M10 = 0, M11 = 0, M12 = 0;
                    int TotalM = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        M1 = int.Parse(dr["cnt"].ToString());
                    }
                    TotalM = M1 + M2 + M3 + M4 + M5 + M6 + M7 + M8 + M9 + M10 + M11 + M12;
                    rpt.DataDefinition.FormulaFields["DM1"].Text = "ToText('0/" + M1.ToString() + "')";
                    rpt.DataDefinition.FormulaFields["TotalM"].Text = "ToText('" + TotalM.ToString() + "人')";

                    //血透總例數透析類型數
                    sSQL = "SELECT a.cln1_col3, count(a.cln1_col3) AS cnt FROM clinical1_nurse a ";
                    sSQL += "WHERE SUBSTR(a.cln1_diadate,1,7)='" + toYearMonth + "' ";
                    sSQL += "GROUP BY a.cln1_col3";
                    dt = db.Query(sSQL);
                    int HD = 0, HFD = 0, HDF = 0, HF = 0, TotalHD = 0, OTHER = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        if (dr["cln1_col3"].ToString() == "HD")
                            HD += int.Parse(dr["cnt"].ToString());
                        else if (dr["cln1_col3"].ToString() == "HFD")
                            HFD += int.Parse(dr["cnt"].ToString());
                        else if (dr["cln1_col3"].ToString() == "HDF")
                            HDF += int.Parse(dr["cnt"].ToString());
                        else if (dr["cln1_col3"].ToString() == "HF")
                            HF += int.Parse(dr["cnt"].ToString());
                        else
                            OTHER += int.Parse(dr["cnt"].ToString());
                    }
                    rpt.DataDefinition.FormulaFields["HD"].Text = "ToText('" + HD.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["HFD"].Text = "ToText('" + HFD.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["HDF"].Text = "ToText('" + HDF.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["HF"].Text = "ToText('" + HF.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["OTHER"].Text = "ToText('" + OTHER.ToString() + "人')";
                    TotalHD = HD + HFD + HDF + HF + OTHER;
                    rpt.DataDefinition.FormulaFields["TotalDH"].Text = "ToText('" + TotalHD.ToString() + "人')";

                    //透析人數
                    sSQL = "SELECT count(DISTINCT a.cln1_patic) AS cnt, ";
                    sSQL += "(SELECT count(*) FROM zinfo_a_07 d WHERE SUBSTR(d.info_date,1,7)='" + toYearMonth + "') AS dead ";
                    sSQL += "FROM clinical1_nurse a ";
                    sSQL += "WHERE SUBSTR(a.cln1_diadate,1,7)='" + toYearMonth + "' ";
                    dt = db.Query(sSQL);
                    float d_times = 0, dead = 0, dead_per = 0;
                    foreach (DataRow dr in dt.Rows)
                    {
                        d_times = float.Parse(dr["cnt"].ToString());
                        dead = float.Parse(dr["dead"].ToString());
                        dead_per = dead / d_times;
                    }
                    rpt.DataDefinition.FormulaFields["DEAD"].Text = "ToText('" + dead.ToString() + "人')";
                    rpt.DataDefinition.FormulaFields["DEAD_P"].Text = "ToText('" + String.Format("{0:P2}", dead_per) + "')";


                    sSQL = "SELECT b.pif_name AS pat_name, a.info_date AS dead_date, a.txt_3 AS Memo FROM zinfo_a_07 a ";
                    sSQL += "LEFT JOIN pat_info b ON b.pif_id=a.pat_id ";
                    sSQL += "WHERE SUBSTR(a.info_date,1,7)='" + toYearMonth + "' ";
                    db.Fill(sSQL, dtDeadList_BODY);
                    dsDeadList.Tables.Add(dtDeadList_BODY);
                    rpt.SetDataSource(dsDeadList);

                    //血壓控制(透析期間血壓90/60~150/90)
                    string sSQL15 = "SELECT count(*) FROM clinical3_nurse a ";
                    sSQL15 += "WHERE a.cln3_a1>=90 AND a.cln3_a2<=150 AND a.cln3_b1>=60 AND a.cln3_b2<=90 ";

                    //腹膜透析歷次
                    string sSQL18 = "SELECT count(*) from clinical1_nurse a ";
                    sSQL18 += "WHERE a.cln3_a1>=90 AND a.cln3_a2<=150 AND a.cln3_b1>=60 AND a.cln3_b2<=90 ";

                    // set report varaibels
                    rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('" + pickDate.Substring(5,2) + "月质量指标')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + pickDate + "')";

                    #endregion
                }
                else if (sREPORT_NAME == "A09")
                {
                    #region 首診四問
                    string sql = string.Empty;
                    DataTable dtEva_Data;
                    DataSetDialysis.zinfo_a_09DataTable dt_a09 = new DataSetDialysis.zinfo_a_09DataTable();
                    dtEva_Data = dt_a09;
                    DataSet ds = new DataSet();
                    try
                    {
                        sql = "SELECT * " + "FROM zinfo_a_09 ";
                        sql += "WHERE pat_id='" + sPAT_ID + "' ";
                        db.Fill(sql, dtEva_Data);
                        ds.Tables.Add(dtEva_Data);
                        rpt.SetDataSource(ds);
                        rpt.DataDefinition.FormulaFields["t_jpg"].Text = "ToText('" + Server.MapPath("~/images/True.jpg") + "')";
                        rpt.DataDefinition.FormulaFields["f_jpg"].Text = "ToText('" + Server.MapPath("~/images/False.jpg") + "')";
                    }
                    catch (DataSourceException ex)
                    {
                        string errorMsg = ex.Message.ToString();
                    }
                    #endregion
                }
                else if (sREPORT_NAME == "checkin")
                {
                    # region 今日报到名单
                    DataSetDialysis.CheckinDataTable dtCheckin = new DataSetDialysis.CheckinDataTable();
                    DataTable dtLIST_BODY = dtCheckin;
                    DataSet ds = new DataSet();
                    string sDATE = sINFO_DATE;
                    string sFLOOR = sREPORT_QM;
                    string sWEEK = sREPORT_P;
                    string sSQL = "SELECT '上午' AS TimeType, ";
                    sSQL += "M.mac_sec AS AREA, M.mac_bedno AS BED_NO, ";
                    sSQL += "CASE WHEN EXISTS(SELECT R.cln1_col26 FROM clinical1_nurse R ";
                    sSQL += "WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE + "') THEN ";
                    sSQL += "(SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE + "') ";
                    sSQL += "ELSE ";
                    sSQL += "(SELECT cln1_col2 FROM clinical1_doc_henan WHERE cln1_patic=A.apptst_patic AND cln1_diadate='base') END AS MAC_MODEL, ";
                    sSQL += "M.mac_typ AS MAC_TYPE, P.pif_name AS PERSON_NAME, ";
                    sSQL += "case P.pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX ";
                    sSQL += "FROM mac_setup M ";
                    sSQL += "LEFT JOIN appointment_setup A ON M.mac_flr=A.apptst_flr AND M.mac_sec=A.apptst_sec AND M.mac_bedno=A.apptst_bed AND A.apptst_daytyp='" + sWEEK + "' ";
                    sSQL += "AND apptst_timetyp='001' ";
                    sSQL += "LEFT JOIN pat_info P ON A.apptst_patic=P.pif_ic ";
                    sSQL += "LEFT JOIN clinical1_nurse N ON A.apptst_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE + "' ";
                    sSQL += "LEFT JOIN general_setup G ON M.mac_brand=G.genst_code AND G.genst_ctg='macbrd' ";
                    sSQL += "LEFT JOIN pat_visit V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + sDATE + "' ";
                    sSQL += "WHERE M.mac_flr='" + sFLOOR + "' "; //AND A.apptst_timetyp is Not Null "; //AND PERSON_NAME is not Null ";
                    //sSQL += "ORDER BY BED_NO";

                    sSQL += "UNION ALL ";
                    sSQL += "SELECT '下午' AS TimeType, ";
                    sSQL += "M.mac_sec AS AREA, M.mac_bedno AS BED_NO, ";
                    sSQL += "CASE WHEN EXISTS(SELECT R.cln1_col26 FROM clinical1_nurse R ";
                    sSQL += "WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE + "') THEN ";
                    sSQL += "(SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE + "') ";
                    sSQL += "ELSE ";
                    sSQL += "(SELECT cln1_col2 FROM clinical1_doc_henan WHERE cln1_patic=A.apptst_patic AND cln1_diadate='base') END AS MAC_MODEL, ";
                    sSQL += "M.mac_typ AS MAC_TYPE, P.pif_name AS PERSON_NAME, ";
                    sSQL += "case P.pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX ";
                    sSQL += "FROM mac_setup M ";
                    sSQL += "LEFT JOIN appointment_setup A ON M.mac_flr=A.apptst_flr AND M.mac_sec=A.apptst_sec AND M.mac_bedno=A.apptst_bed AND A.apptst_daytyp='" + sWEEK + "' ";
                    sSQL += "AND apptst_timetyp='002' ";
                    sSQL += "LEFT JOIN pat_info P ON A.apptst_patic=P.pif_ic ";
                    sSQL += "LEFT JOIN clinical1_nurse N ON A.apptst_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE + "' ";
                    sSQL += "LEFT JOIN general_setup G ON M.mac_brand=G.genst_code AND G.genst_ctg='macbrd' ";
                    sSQL += "LEFT JOIN pat_visit V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + sDATE + "' ";
                    sSQL += "WHERE M.mac_flr='" + sFLOOR + "' ";

                    sSQL += "UNION ALL ";
                    sSQL += "SELECT '晚班' AS TimeType, ";
                    sSQL += "M.mac_sec AS AREA, M.mac_bedno AS BED_NO, ";
                    sSQL += "CASE WHEN EXISTS(SELECT R.cln1_col26 FROM clinical1_nurse R ";
                    sSQL += "WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE + "') THEN ";
                    sSQL += "(SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.apptst_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE + "') ";
                    sSQL += "ELSE ";
                    sSQL += "(SELECT cln1_col2 FROM clinical1_doc_henan WHERE cln1_patic=A.apptst_patic AND cln1_diadate='base') END AS MAC_MODEL, ";
                    sSQL += "M.mac_typ AS MAC_TYPE, P.pif_name AS PERSON_NAME, ";
                    sSQL += "case P.pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX ";
                    sSQL += "FROM mac_setup M ";
                    sSQL += "LEFT JOIN appointment_setup A ON M.mac_flr=A.apptst_flr AND M.mac_sec=A.apptst_sec AND M.mac_bedno=A.apptst_bed AND A.apptst_daytyp='" + sWEEK + "' ";
                    sSQL += "AND apptst_timetyp='003' ";
                    sSQL += "LEFT JOIN pat_info P ON A.apptst_patic=P.pif_ic ";
                    sSQL += "LEFT JOIN clinical1_nurse N ON A.apptst_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE + "' ";
                    sSQL += "LEFT JOIN general_setup G ON M.mac_brand=G.genst_code AND G.genst_ctg='macbrd' ";
                    sSQL += "LEFT JOIN pat_visit V ON A.apptst_patic=V.pv_ic AND V.pv_datevisit='" + sDATE + "' ";
                    sSQL += "WHERE M.mac_flr='" + sFLOOR + "' ";
                    sSQL += "ORDER BY TimeType, AREA, BED_NO";
                    dtLIST_BODY = db.Query(sSQL);

                    //補上更換床資料
                    sSQL = "SELECT ";
                    sSQL += "case A.ah_timetyp when '001' then '上午' when '002' then '下午' when '003' then '晚班' end AS TimeType, ";
                    sSQL += "A.ah_flr, A.ah_sec, A.ah_bed AS BED_NO, ";
                    sSQL += "CASE ";
                    sSQL += "WHEN EXISTS(SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.ah_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE + "') THEN ";
                    sSQL += "(SELECT R.cln1_col26 FROM clinical1_nurse R WHERE A.ah_patic=R.cln1_patic AND R.cln1_diadate='" + sDATE + "') ";
                    sSQL += "ELSE ";
                    sSQL += "(SELECT cln1_col2 FROM clinical1_doc_henan WHERE A.ah_patic=cln1_patic AND cln1_diadate='base') END AS MAC_MODEL, ";
                    sSQL += "P.pif_name AS PERSON_NAME, ";
                    sSQL += "case P.pif_sex when 'M' then '男' when 'F' then '女' end as PERSON_SEX ";
                    sSQL += "FROM appointment_change A ";
                    sSQL += "LEFT JOIN pat_info P ON A.ah_patic=P.pif_ic ";
                    sSQL += "LEFT JOIN pat_visit V ON A.ah_patic=V.pv_ic AND V.pv_datevisit='" + sDATE + "' ";
                    sSQL += "LEFT JOIN clinical1_nurse N ON A.ah_patic=N.cln1_patic AND N.cln1_diadate='" + sDATE + "' ";
                    sSQL += "WHERE ah_date='" + sDATE + "' ";
                    DataTable dt2 = db.Query(sSQL);
                    if (dt2.Rows.Count > 0)
                    {
                        System.Data.DataView dv = new System.Data.DataView();
                        dv = dtLIST_BODY.DefaultView;
                        for (int i = 0; i < dt2.Rows.Count; i++)
                        {
                            dv.RowFilter = "BED_NO='" + dt2.Rows[i]["BED_NO"].ToString() + "' AND TimeType='" + dt2.Rows[i]["TimeType"].ToString() + "' ";
                            if (dv.Count > 0)
                            {
                                dv[0]["PERSON_NAME"] = dt2.Rows[i]["PERSON_NAME"].ToString();
                                dv[0]["PERSON_SEX"] = dt2.Rows[i]["PERSON_SEX"].ToString();
                                dv[0]["MAC_MODEL"] = dt2.Rows[i]["MAC_MODEL"].ToString();
                                dv[0]["TimeType"] = dt2.Rows[i]["TimeType"].ToString();
                            }
                        }
                    }
                    for (int i = 0; i < dtLIST_BODY.Rows.Count; i++)
                    {
                        if (dtLIST_BODY.Rows[i]["PERSON_NAME"].ToString() == "")
                            dtLIST_BODY.Rows[i].Delete();
                    }
                    dtLIST_BODY.AcceptChanges();
                    rpt.SetDataSource(dtLIST_BODY);
                    rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('今日报到名单')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + sDATE + "')";
                    #endregion
                }
                else if (sREPORT_NAME == "weeksch")
                {
                    # region 本周排班名单
                    string sDATE = DateTime.Now.ToString("yyyy-MM-dd");
                    string floor = sREPORT_QM;
                    string timetype = sREPORT_P; //時間區段-上午晚
                    string area, bedno, mac_typ, machine;

                    var DataSource = new List<PrintSchedule>();
                    string sql = "SELECT a.mac_flr, a.mac_sec, a.mac_bedno, a.mac_typ, b.genst_desc AS machine FROM mac_setup a ";
                    sql += "LEFT JOIN general_setup b ON b.genst_code=a.mac_brand ";
                    sql += "WHERE a.mac_status='Y' AND mac_flr='" + floor + "' AND b.genst_ctg='macbrd' ";
                    sql += "ORDER BY a.mac_sec, a.mac_bedno";
                    DataTable dt1 = db.Query(sql);
                    if (dt1.Rows.Count > 0)
                    {
                        foreach (DataRow dr1 in dt1.Rows)
                        {
                            area = dr1["mac_sec"].ToString();
                            bedno = dr1["mac_bedno"].ToString();
                            mac_typ = dr1["mac_typ"].ToString();
                            machine = dr1["machine"].ToString();
                            sql = "SELECT a.apptst_patic, a.apptst_daytyp, b.pif_name FROM appointment_setup a ";
                            sql += "LEFT JOIN pat_info b ON b.pif_ic=a.apptst_patic ";
                            sql += "WHERE a.apptst_flr='" + floor + "' AND a.apptst_sec='" + area + "' AND a.apptst_bed='" + bedno + "' AND a.apptst_timetyp='" + timetype + "' ";
                            sql += "ORDER BY a.apptst_daytyp";
                            DataTable dt2 = db.Query(sql);
                            string[] patname = new string[7];
                            int i = 0;
                            for (i = 0; i < 7; i++)
                            {
                                patname[i] = "";
                            }

                            foreach (DataRow dr2 in dt2.Rows)
                            {
                                i = int.Parse(dr2["apptst_daytyp"].ToString()) - 1;
                                string pif_name = dr2["pif_name"].ToString();
                                patname[i] = pif_name;
                            }

                            #region 增加臨時預約病患
                            DateTime thisDay;
                            for (i = 0; i < 7; i++)
                            {
                                int iweek = Convert.ToInt16(DateTime.Now.DayOfWeek);
                                if (iweek == 0) iweek = 7;
                                thisDay = DateTime.Now.AddDays(Convert.ToDouble(1 - iweek) + i);
                                sql = "SELECT a.*, b.pif_name FROM appointment_change a ";
                                sql += "LEFT JOIN pat_info b ON a.ah_patic=b.pif_ic ";
                                sql += "WHERE a.ah_date='" + thisDay.ToString("yyyy-MM-dd") + "' AND a.ah_flr='" + floor + "' AND a.ah_sec='" + area + "' AND a.ah_bed='" + bedno + "' AND a.ah_timetyp='" + timetype + "' ";
                                DataTable dt0 = db.Query(sql);
                                if (dt0.Rows.Count > 0)
                                {
                                    string patId = dt0.Rows[0]["ah_patic"] == null ? "" : dt0.Rows[0]["ah_patic"].ToString();
                                    if (patId != "")
                                    {
                                        //dt2前面宣告使用過: 檢查沒有排入預設排班者，就是臨時排班病人
                                        dt2 = db.Query("SELECT apptst_bed FROM appointment_setup WHERE apptst_patic='" + patId + "'");
                                        string patName = dt0.Rows[0]["pif_name"].ToString();
                                        if (dt2.Rows.Count == 0)
                                        {
                                            patName += "(临)";
                                        }
                                        patname[i] = patName;
                                    }
                                    else
                                    {
                                        patname[i] = "";
                                    }
                                }
                            }
                            #endregion
                            DataSource.Add(new PrintSchedule(floor, area, bedno, mac_typ, patname[0], patname[1], patname[2], patname[3], patname[4], patname[5], patname[6], machine, timetype));
                        }
                    }
                    rpt.SetDataSource(DataSource);
                    rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('本周排班名单')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + sDATE + "')";
                    if (sREPORT_P == "001")
                        sREPORT_P = "上午";
                    else if (sREPORT_P == "002")
                        sREPORT_P = "下午";
                    else if (sREPORT_P == "003")
                        sREPORT_P = "晚上";
                    rpt.DataDefinition.FormulaFields["TimeType"].Text = "ToText('" + sREPORT_P + "')";

                    #endregion
                }
                else if (sREPORT_NAME == "drug_term")
                {
                    #region 醫囑清單
                    string sql = "";
                    if (sREPORT_P == "long")
                    {
                        rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('长期医嘱用药')";
                        sql = "SELECT a.lgord_dateord AS dateord, ";
                        sql += "a.lgord_timeord AS timeord, ";
                        sql += "a.lgord_usr1 AS user1, ";
                        sql += "b.drg_name AS drg_name, ";
                        sql += "a.lgord_intake AS intake, ";
                        sql += "a.lgord_freq AS freq, ";
                        sql += "a.lgord_medway AS medway, ";
                        if (Hospital == "Hospital_Alasamo")
                        {
                            sql += "a.lgord_nurs AS comment, ";
                        }
                        else
                        {
                            sql += "a.lgord_comment AS comment, ";
                        }
                        sql += "a.lgord_dtactst AS dtactst, ";
                        sql += "CASE a.lgord_actst WHEN '00001' THEN '' WHEN '00002' THEN '停用' END AS status ";
                        sql += "FROM longterm_ordermgt a ";
                        sql += "LEFT JOIN drug_list b ON a.lgord_drug=b.drg_code ";
                        sql += "WHERE a.lgord_patic='" + _PAT_IC + "' ";
                        sql += "ORDER BY Status";
                    }
                    else if (sREPORT_P == "short")
                    {
                        rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('短期医嘱用药')";
                        string time = DateTime.Now.ToString("HH:mm");
                        sql = "SELECT ";
                        sql += "a.shord_dateord AS dateord, ";
                        sql += "a.shord_timeord AS timeord, ";
                        sql += "a.shord_usr1 AS user1, ";
                        sql += "b.drg_name AS drg_name, ";
                        sql += "a.shord_intake AS intake, ";
                        sql += "a.shord_freq AS freq, ";
                        sql += "a.shord_medway AS medway, ";
                        if (Hospital == "Hospital_Alasamo")
                        {
                            sql += "a.shord_nurs AS comment, ";
                        }
                        else
                        {
                            sql += "a.shord_comment AS comment, ";
                        }
                        sql += "a.shord_dtactst AS dtactst, ";
                        sql += "CASE a.shord_actst WHEN '00001' THEN '' WHEN '00002' THEN '停用' END As status ";
                        sql += "FROM shortterm_ordermgt a ";
                        sql += "LEFT JOIN drug_list b ON a.shord_drug = b.drg_code ";
                        sql += "WHERE a.shord_patic='" + _PAT_IC + "' ";
                        sql += "AND a.shord_dateord='" + DateTime.Now.ToString("yyyy-MM-dd") + "' ";
                        sql += "ORDER BY Status";
                    }
                    DataTable dt = db.Query(sql);
                    rpt.SetDataSource(dt);
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    rpt.DataDefinition.FormulaFields["PAT_NAME"].Text = "ToText('" + _PIF_NAME + "')";
                    #endregion
                }
                else if (sREPORT_NAME == "HTest")
                {
                    #region H.血液透析化验数据表
                    var DataSource = new List<PrintTest>();
                    DataSource.Add(new PrintTest("乙肝三系（1次/6月）", "", "", "", "", "", ""));
                    DataSource.Add(new PrintTest("日期", "HBsAg", "HBeAg", "抗HCV", "", "", ""));
                    string tdate = "", item1 = "", item2 = "", item3 = "", item4 = "", item5 = "", item6 = "";

                    string sSQL1 = "";
                    string sSQL = "SELECT RESULT_DATE AS RDATE FROM a_result_log ";
                    sSQL += "WHERE PAT_NO='" + _PAT_ID + "' ";
                    sSQL += "AND RESULT_DATE BETWEEN '" + sBEG_DATE + "' AND '" + sEND_DATE + "' ";
                    sSQL += "AND RESULT_CODE IN ('4032','4038','4033') ";
                    sSQL += "GROUP BY RDATE ";
                    sSQL += "ORDER BY RDATE DESC";
                    DataTable dt = db.Query(sSQL);
                    foreach (DataRow dr in dt.Rows)
                    {
                        item1 = "";
                        item2 = "";
                        item3 = "";
                        item4 = "";
                        item5 = "";
                        item6 = "";
                        tdate = dr["RDATE"].ToString();
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4032' ";
                        DataTable dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item1 = dt1.Rows[0]["Tvalue"].ToString();
                        } 
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4038' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item2 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4033' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item3 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        DataSource.Add(new PrintTest(tdate, item1, item2, item3, item4, item5, item6));
                    }
                    DataSource.Add(new PrintTest("", "", "", "", "", "", ""));
                    DataSource.Add(new PrintTest("血常规、电解质、肾功能（1次/月）", "", "", "", "", "", ""));
                    DataSource.Add(new PrintTest("日期", "HGB", "K", "Ca", "P", "透前尿素", "肌酣")); 
                    sSQL = "SELECT RESULT_DATE AS RDATE FROM a_result_log ";
                    sSQL += "WHERE PAT_NO='" + _PAT_ID + "' ";
                    sSQL += "AND RESULT_DATE BETWEEN '" + sBEG_DATE + "' AND '" + sEND_DATE + "' ";
                    sSQL += "AND RESULT_CODE IN ('4003','4019','4021','4023','4047','4052') ";
                    sSQL += "GROUP BY RDATE ";
                    sSQL += "ORDER BY RDATE DESC";
                    dt = db.Query(sSQL);
                    foreach (DataRow dr in dt.Rows)
                    {
                        item1 = "";
                        item2 = "";
                        item3 = "";
                        item4 = "";
                        item5 = "";
                        item6 = "";
                        tdate = dr["RDATE"].ToString();
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4003' ";
                        DataTable dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item1 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4019' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item2 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4021' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item3 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4023' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item4 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4047' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item5 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4052' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item6 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        DataSource.Add(new PrintTest(tdate, item1, item2, item3, item4, item5, item6));
                    }
                    DataSource.Add(new PrintTest("", "", "", "", "", "", ""));
                    DataSource.Add(new PrintTest("血生化（1次/3月）", "", "", "", "", "", ""));
                    DataSource.Add(new PrintTest("日期", "透后尿素", "透后尿素", "KT/V", "URR", "PCR", "白蛋白")); 
                    sSQL = "SELECT RESULT_DATE AS RDATE FROM a_result_log ";
                    sSQL += "WHERE PAT_NO='" + _PAT_ID + "' ";
                    sSQL += "AND RESULT_DATE BETWEEN '" + sBEG_DATE + "' AND '" + sEND_DATE + "' ";
                    sSQL += "AND RESULT_CODE IN ('4048','4055','5018','5017','4043','4008') ";
                    sSQL += "GROUP BY RDATE ";
                    sSQL += "ORDER BY RDATE DESC";
                    dt = db.Query(sSQL);
                    foreach (DataRow dr in dt.Rows)
                    {
                        item1 = "";
                        item2 = "";
                        item3 = "";
                        item4 = "";
                        item5 = "";
                        item6 = "";
                        tdate = dr["RDATE"].ToString();
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4048' ";
                        DataTable dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item1 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4055' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item2 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='5018' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item3 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='5017' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item4 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4043' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item5 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4008' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item6 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        DataSource.Add(new PrintTest(tdate, item1, item2, item3, item4, item5, item6));
                    }
                    DataSource.Add(new PrintTest("", "", "", "", "", "", ""));
                    DataSource.Add(new PrintTest("iPHT、SF、CRP（1次/3月）", "", "", "", "", "", ""));
                    DataSource.Add(new PrintTest("日期", "iPHT", "TSAT", "SF铁蛋白", "CRP", "", "")); 
                    sSQL = "SELECT RESULT_DATE AS RDATE FROM a_result_log ";
                    sSQL += "WHERE PAT_NO='" + _PAT_ID + "' ";
                    sSQL += "AND RESULT_DATE BETWEEN '" + sBEG_DATE + "' AND '" + sEND_DATE + "' ";
                    sSQL += "AND RESULT_CODE IN ('4030','4050','4027','4053','5043','5044') ";
                    sSQL += "GROUP BY RDATE ";
                    sSQL += "ORDER BY RDATE DESC";
                    dt = db.Query(sSQL);
                    foreach (DataRow dr in dt.Rows)
                    {
                        item1 = "";
                        item2 = "";
                        item3 = "";
                        item4 = "";
                        item5 = "";
                        item6 = "";
                        tdate = dr["RDATE"].ToString();
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4030' ";
                        DataTable dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item1 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4050' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item2 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4027' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item3 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='4053' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item4 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        DataSource.Add(new PrintTest(tdate, item1, item2, item3, item4, item5, item6));
                    }
                    DataSource.Add(new PrintTest("", "", "", "", "", "", ""));
                    DataSource.Add(new PrintTest("日期", "心超", "", "", "", "", ""));
                    sSQL = "SELECT RESULT_DATE AS RDATE FROM a_result_log ";
                    sSQL += "WHERE PAT_NO='" + _PAT_ID + "' ";
                    sSQL += "AND RESULT_DATE BETWEEN '" + sBEG_DATE + "' AND '" + sEND_DATE + "' ";
                    sSQL += "AND RESULT_CODE IN ('5043','5044') ";
                    sSQL += "GROUP BY RDATE ";
                    sSQL += "ORDER BY RDATE DESC";
                    dt = db.Query(sSQL);
                    foreach (DataRow dr in dt.Rows)
                    {
                        item1 = "";
                        item2 = "";
                        item3 = "";
                        item4 = "";
                        item5 = "";
                        item6 = "";
                        tdate = dr["RDATE"].ToString();
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='5043' ";
                        DataTable dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item1 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        sSQL1 = "SELECT RESULT_VALUE_T AS Tvalue FROM a_result_log WHERE RESULT_DATE='" + dr["RDATE"].ToString() + "' AND RESULT_CODE='5044' ";
                        dt1 = db.Query(sSQL1);
                        if (dt1.Rows.Count > 0)
                        {
                            item2 = dt1.Rows[0]["Tvalue"].ToString();
                        }
                        DataSource.Add(new PrintTest(tdate, item1, item2, item3, item4, item5, item6));
                    }
                    rpt.SetDataSource(DataSource);
                    rpt.DataDefinition.FormulaFields["REPORT_TITLE"].Text = "ToText('血液透析化验数据表')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + DateTime.Now.ToString("yyyy-MM-dd") + "')";
                    string Pat_Name = "", Pat_Sex = "", Pat_Age = "";
                    sSQL = "SELECT pif_name AS name, ";
                    sSQL += "CASE pif_sex WHEN 'M' THEN '男' WHEN 'F' THEN '女' END AS sex, ";
                    sSQL += DateTime.Now.Year.ToString() + "-year(pif_dob) AS age FROM pat_info ";
                    sSQL += "WHERE pif_id='" + _PAT_ID + "'";
                    dt = db.Query(sSQL);
                    if (dt.Rows.Count>0)
                    {
                        Pat_Name = dt.Rows[0]["name"].ToString();
                        Pat_Sex = dt.Rows[0]["sex"].ToString();
                        Pat_Age = dt.Rows[0]["age"].ToString();
                    }
                    rpt.DataDefinition.FormulaFields["Pat_Name"].Text = "ToText('姓名:" + Pat_Name + "')";
                    rpt.DataDefinition.FormulaFields["Pat_Sex"].Text = "ToText('性别:" + Pat_Sex + "')";
                    rpt.DataDefinition.FormulaFields["Pat_Age"].Text = "ToText('年龄:" + Pat_Age + "')";
                    #endregion
                }
                else if (sREPORT_NAME == "J01")
                {
                    #region sREPORT_NAME == "J01"
                    DataSet1.report_msgDataTable dt_HEAD = new DataSet1.report_msgDataTable();
                    DataSet1.report_cnt1DataTable dt_BODY1 = new DataSet1.report_cnt1DataTable();
                    DataSet1.report_cnt2DataTable dt_BODY2 = new DataSet1.report_cnt2DataTable();

                    DataTable dtLIST_HEAD = dt_HEAD;
                    DataTable dtLIST_BODY1 = dt_BODY1;
                    DataTable dtLIST_BODY2 = dt_BODY2;

                    DataSet dsLIST = new DataSet();
                    string sDATE = "日期区间：" + sBEG_DATE + "~" + sEND_DATE;

                    DataRow dr = dt_HEAD.NewRow();
                    dr["sDATA"] = sDATE;
                    dt_HEAD.Rows.Add(dr);
                    dsLIST.Tables.Add(dt_HEAD);
                    string sSQL = "SELECT '" + sDATE + "' as DIA_DATE, cln1_col3 as DIA_TYPE, COUNT(cln1_col3) as DIA_COUNT FROM clinical1_nurse " +
                                    "WHERE cln1_diadate BETWEEN '" + sBEG_DATE + "' AND '" + sEND_DATE + "' " +
                                    //"AND cln1_col3<>'' " +
                                    "GROUP BY cln1_col3 " +
                                    "ORDER BY cln1_col3 ";
                    db.Fill(sSQL, dtLIST_BODY1);
                    dsLIST.Tables.Add(dtLIST_BODY1);

                    sSQL = "SELECT '" + sDATE + "' as DIA_DATE, cln1_col34 as DIA_TYPE, COUNT(cln1_col34) as DIA_COUNT FROM clinical1_nurse " +
                                    "WHERE cln1_diadate BETWEEN '" + sBEG_DATE + "' AND '" + sEND_DATE + "' " +
                                    //"AND cln1_col34<>'' " +
                                    "GROUP BY cln1_col34 " +
                                    "ORDER BY cln1_col34 ";
                    db.Fill(sSQL, dtLIST_BODY2);
                    dsLIST.Tables.Add(dtLIST_BODY2);
                    
                    rpt.SetDataSource(dsLIST);

                    #endregion
                }
                else
                {
                    ParameterDiscreteValue pdv = new ParameterDiscreteValue();
                    if (sPAT_ID != "")
                    {
                        pdv.Value = sPAT_ID;
                        rpt.ParameterFields["pat_id"].CurrentValues.Clear();
                        rpt.ParameterFields["pat_id"].DefaultValues.Clear();
                        rpt.ParameterFields["pat_id"].CurrentValues.Add(pdv);
                    }
                    if (sINFO_DATE != "")
                    {
                        pdv.Value = sINFO_DATE;
                        rpt.ParameterFields["info_date"].CurrentValues.Clear();
                        rpt.ParameterFields["info_date"].DefaultValues.Clear();
                        rpt.ParameterFields["info_date"].CurrentValues.Add(pdv);
                    }
                }

                CRViewer.ReportSource = rpt;

                TableLogOnInfos tableLogOnInfos = CRViewer.LogOnInfo;

                foreach (TableLogOnInfo tableLogOnInfo in tableLogOnInfos)
                {
                    tableLogOnInfo.ConnectionInfo = connInfo;
                }
                //20160513 Alex 直接導出PDF檔案
                //rpt.ExportToHttpResponse(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat, Response, true, "PersonDetails");
                //Response.Flush();

                //直接印出到印表機
                //rpt.PrintToPrinter(1, true, 0, 0);
            }
        }

        //20150610 ANDY
        protected void btn_HIS_Query_Click()
        {
            string sql;
            DBMysql db = new DBMysql();
            //2015-06-11
            DateTime dBEG = DateTime.Now;
            DateTime dEND = DateTime.Now;
            //2015-06-01//
            dBEG = Convert.ToDateTime(dBEG.ToString("yyyy-MM-") + "01");
            dEND = dBEG;

            DateTime YMMdd = DateTime.Now;
            YMMdd = Convert.ToDateTime(YMMdd.ToString("yyyy-MM-dd"));
            string w_year = YMMdd.ToString("yyyy-MM-dd");
            string w_month = YMMdd.ToString("yyyy-MM-dd");
            string w_dd = YMMdd.ToString("yyyy-MM-dd");
            string w_MMdd = YMMdd.ToString("yyyy-MM-dd");
            //now 2015
            w_year = w_year.Substring(0, 4);
            //06
            w_month = w_month.Substring(5, 2);
            //13
            w_dd = w_dd.Substring(8, 2);
            //06-13
            w_MMdd = w_MMdd.Substring(5, 5);

            //up
            DateTime ww_year_up;
            DateTime ww_month_up;
            DateTime ww_day_up;
            //2014-06-01   2015-06-01
            ww_year_up = dEND.AddYears(-1);
            //2015-05-01   2015-06-01
            ww_month_up = dEND.AddMonths(-1);
            //2015-05-31
            ww_day_up = dEND.AddDays(-1);
            //up year
            string w_yeara = ww_year_up.ToString("yyyy-MM-dd");
            //up month
            string w_montha = ww_month_up.ToString("yyyy-MM-dd");
            //up month lastday
            string w_daya = ww_day_up.ToString("yyyy-MM-dd");
            //2014
            w_yeara = w_yeara.Substring(0, 4);
            //05
            w_montha = w_montha.Substring(5, 2);
            //31
            w_daya = w_daya.Substring(8, 2);

            //2015-07-01
            dEND = dEND.AddMonths(1);
            //2015-06-30   //
            dEND = dEND.AddDays(-1);
            string w_beg_date = "2013-05-01";
            w_beg_date = "2013-12-01";
            //2015-06-01 //
            w_beg_date = dBEG.ToString("yyyy-MM-dd");
            string w_end_date = "2013-05-31";
            w_end_date = "2013-12-31";
            //2015-06-30 //
            w_end_date = dEND.ToString("yyyy-MM-dd");

            //2015-06-01-2015-06-30//
            string sBEG_DATE = _Get_YMD2(w_beg_date);
            string sEND_DATE = _Get_YMD2(w_end_date);
            string sSQL = "";
            string w_hqname = "";
            string w_run_flag = "";
            //月
            if (w_dd == "01")
            {
                if (w_MMdd == "01-01")
                {
                    sBEG_DATE = w_yeara + "-12-01";
                    sEND_DATE = w_yeara + "-12-31";
                    w_hqname = "月報";
                    w_run_flag = "m";
                }
                else
                {
                    sBEG_DATE = sBEG_DATE.Substring(0, 4) + "-" + w_montha + "-01";
                    sEND_DATE = sEND_DATE.Substring(0, 4) + "-" + w_montha + "-" + w_daya;
                    w_hqname = "月報";
                    w_run_flag = "m";
                }
            }

            //[月] 20150803 ANDY
            string sREPORT_P = Request["_REPORT_P"] == null ? string.Empty : Request["_REPORT_P"].ToString();//20150803 ANDY
            string sREPORT_YEAR = Request["_REPORT_sYEAR"] == null ? string.Empty : Request["_REPORT_sYEAR"].ToString();//20150803 ANDY
            string sREPORT_QT = Request["_REPORT_sQT"] == null ? string.Empty : Request["_REPORT_sQT"].ToString();//20150803 ANDY
            string sREPORT_QM = Request["_REPORT_sQM"] == null ? string.Empty : Request["_REPORT_sQM"].ToString();  //20150803 ANDY
            sREPORT_QM = "6月";
            if (sREPORT_P == "1" && sREPORT_QT == "月")
            {
                sBEG_DATE = sREPORT_YEAR + "-" + sREPORT_QM.Substring(0, 1) + "-01";
                sEND_DATE = sREPORT_YEAR + "-" + sREPORT_QM.Substring(0, 1) + "-31";
                if (sREPORT_QM.Substring(0, 1) == "1" || sREPORT_QM.Substring(0, 1) == "2" || sREPORT_QM.Substring(0, 1) == "3" || sREPORT_QM.Substring(0, 1) == "4" ||
                     sREPORT_QM.Substring(0, 1) == "5" || sREPORT_QM.Substring(0, 1) == "6" || sREPORT_QM.Substring(0, 1) == "7" || sREPORT_QM.Substring(0, 1) == "8" ||
                     sREPORT_QM.Substring(0, 1) == "9")
                {
                    sBEG_DATE = sREPORT_YEAR + "-0" + sREPORT_QM.Substring(0, 1) + "-01";
                    sEND_DATE = sREPORT_YEAR + "-0" + sREPORT_QM.Substring(0, 1) + "-31";
                }
                w_hqname = "月报";
                w_run_flag = "m";
                goto BEGG;
            }

            //季
            if (w_MMdd == "01-02")
            {
                sBEG_DATE = w_yeara + "-10-01";
                sEND_DATE = w_yeara + "-12-31";
                w_hqname = "季报";
                w_run_flag = "s";
            }
            if (w_MMdd == "04-02")
            {
                sBEG_DATE = sBEG_DATE.Substring(0, 4) + "-01-01";
                sEND_DATE = sEND_DATE.Substring(0, 4) + "-03-31";
                w_hqname = "季报";
                w_run_flag = "s";
            }
            if (w_MMdd == "07-02")
            {
                sBEG_DATE = sBEG_DATE.Substring(0, 4) + "-04-01";
                sEND_DATE = sEND_DATE.Substring(0, 4) + "-06-30";
                w_hqname = "季报";
                w_run_flag = "s";
            }
            if (w_MMdd == "10-02")
            {
                sBEG_DATE = sBEG_DATE.Substring(0, 4) + "-07-01";
                sEND_DATE = sEND_DATE.Substring(0, 4) + "-09-30";
                w_hqname = "季报";
                w_run_flag = "s";
            }

            //半年
            if (w_MMdd == "01-03" || w_MMdd == "07-03")
            {
                if (w_MMdd == "01-03")
                {
                    sBEG_DATE = w_yeara + "-07-01";
                    sEND_DATE = w_yeara + "-12-31";
                    w_hqname = "半年报";
                    w_run_flag = "h";
                }
                else
                {
                    sBEG_DATE = sBEG_DATE.Substring(0, 4) + "-01-01";
                    sEND_DATE = sEND_DATE.Substring(0, 4) + "-06-30";
                    w_hqname = "半年报";
                    w_run_flag = "h";
                }
            }

            //年
            if (w_MMdd == "01-04")
            {
                sBEG_DATE = w_yeara + "-01-01";
                sEND_DATE = w_yeara + "-12-31";
                w_hqname = "年报";
                w_run_flag = "y";
            }

            if (w_run_flag == "")
            {
                w_hqname = "月报";
                w_run_flag = "a";
            }

            //年報 MARK 20150611 ANDY 
        //if (sBEG_DATE == "")
        //    sBEG_DATE = "2000-01-01";
        //else
        //    sBEG_DATE = sBEG_DATE.Substring(0, 4) + "-01-01";

            //if (sEND_DATE == "")
        //    sEND_DATE = "9999-12-31";
        //else
        //    sEND_DATE = sEND_DATE.Substring(0, 4) + "-12-31";

            // [統計日期區間] 
        // 年報  :w_dates  w_datee 2015-01-01 - 2015-12-31 
        // 半年報:
        // 季報  :
        // 月報  : 2015-06-01 ~ 2016-06-30

BEGG:
            //            sBEG_DATE = "2015-01-01";      
            //            sEND_DATE = "2016-12-31";      
            string w_date_se = sBEG_DATE + " ~ " + sEND_DATE;
            string w_dates = sBEG_DATE;
            string w_datee = sEND_DATE;

            string sRPT_NAME = "";
            DataTable dtNAME = db.Query("SELECT *  FROM general_setup WHERE  genst_code='RPT_NAME'");
            if (dtNAME.Rows.Count > 0)
            {
                sRPT_NAME = dtNAME.Rows[0]["genst_desc"].ToString();
            }
            string w_rpt_name = sRPT_NAME;
            //[打印日期]
            DateTime w_pr = DateTime.Now;
            string w_hq_date3 = w_pr.ToString("yyyy-MM-dd");
            //[病人人數w_txtTOTAL4]
            sSQL = "SELECT DISTINCT A.pv_ic " +
                     "FROM pat_visit A " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' ";
            //有登記就算人(血透人数的计算按照  这个人只要排过班就算，不分楼层，不管有没有血透过。)
            sSQL = "SELECT B.pif_ic AS pv_ic " +
                     "FROM pat_info B " +
                     " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 ";
            DataTable dtTOTAL4 = db.Query(sSQL);
            string w_txtTOTAL4 = dtTOTAL4.Rows.Count.ToString();

            //[住院率 w_txtHOSP_P.]
            //找住院的人           
            sSQL = "SELECT B.pif_name, B.pif_sex, B.pif_ic, A.pat_id AS PAT_NO, " +
                          "A.info_date AS HOSP_DATE, A.txt_27 AS HOSP_CAUSE " +
                     "FROM zinfo_e_01 A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pat_id=B.pif_id " +
                       " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.info_date>='" + sBEG_DATE + "' " +
                      "AND A.info_date<='" + sEND_DATE + "' " +
                      "AND A.opt_26=1 ";

            DataTable dtHOSP = db.Query(sSQL);
            string w_txtHOSP = dtHOSP.Rows.Count.ToString();
            string w_txtHOSP_P;
            for (int i = 0; i < dtHOSP.Rows.Count; i++)
            {
                switch (dtHOSP.Rows[i]["pif_sex"].ToString().Trim())
                {
                    case "F":
                        dtHOSP.Rows[i]["pif_sex"] = "女";
                        break;
                    case "M":
                        dtHOSP.Rows[i]["pif_sex"] = "男";
                        break;
                }
            }
            if (w_txtTOTAL4 == "0")
            {
                w_txtHOSP_P = "0";
            }
            else
            {
                w_txtHOSP_P = Percent(Convert.ToDouble(w_txtHOSP) / Convert.ToDouble(w_txtTOTAL4) * 1000);
            }

            //死亡率 w_txtDIE_P.
            //找死亡的人
            sSQL = "SELECT B.pif_name, B.pif_ic, A.pat_id AS PAT_NO, 0 as AGE, 0 as AGE_HD, '' AS DIE_CAUSE," +
                                      "A.info_date AS DIE_DATE,  B.pif_dob, C.dat_9 AS HD_DATE, B.pif_sex, A.chk_7 as chk_55 " +
                                 "FROM zinfo_a_07 A " +
                                 "LEFT JOIN pat_info B " +
                                   "ON  A.pat_id=B.pif_id " +
                                 "  AND A.opt_1 in('4') " +
                                 "LEFT JOIN zinfo_f_012 C " +
                                   "ON A.pat_id=C.pat_id " +
                                "WHERE A.info_date>='" + sBEG_DATE + "' " +
                                  "AND A.info_date<='" + sEND_DATE + "' ";
            DataTable dtDIE = db.Query(sSQL);
            string w_txtDIE = dtDIE.Rows.Count.ToString();
            string sDIE_CAUSE = "";
            string schk_55 = "";
            string sDIE_DATE = "";
            int iAGE = 0;
            int iAGE_HD = 0;
            string w_txtDIE_P;
            for (int i = 0; i < dtDIE.Rows.Count; i++)
            {
                sDIE_CAUSE = "";
                sDIE_DATE = dtDIE.Rows[i]["DIE_DATE"].ToString();

                if (dtDIE.Rows[i]["pif_dob"].ToString().Length >= 4)
                {
                    if (Int32.TryParse(dtDIE.Rows[i]["pif_dob"].ToString().Substring(0, 4), out iAGE))
                    {
                        iAGE = Convert.ToInt32(dtDIE.Rows[i]["pif_dob"].ToString().Substring(0, 4));
                        dtDIE.Rows[i]["AGE"] = Convert.ToInt16(sDIE_DATE.Substring(0, 4)) - iAGE;
                    }
                    else
                    {
                        //dtDIE.Rows[i]["AGE"] = 0;
                        //sDIE_CAUSE += "'出生日期'资料错误，";
                    }
                }
                else
                {
                    //dtDIE.Rows[i]["AGE"] = 0;
                    //sDIE_CAUSE += "'出生日期'资料错误，";
                }
            }
            //Store istore1 = this.GridPanel3.GetStore();
            //istore1.DataSource = db.GetDataArray_AddRowNum(dtDIE);
            //istore1.DataBind();
            //string w_txtDIE_P;
            if (w_txtTOTAL4 == "0")
            {
                w_txtDIE_P = "0";
            }
            else
            {
                w_txtDIE_P = Percent(Convert.ToDouble(w_txtDIE) / Convert.ToDouble(w_txtTOTAL4) * 1000);
            }

            //[血红蛋白Hb.]           
            string sRESULT_CODE = "4003";
            string w_txtRESULT_NAME;
            string w_txtRESULT_UNIT;
            string w_txtNORMAL;
            string w_txtFORMAT;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE = db.Query(sSQL);
            if (dtCODE.Rows.Count > 0)
            {
                w_txtRESULT_NAME = dtCODE.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT = dtCODE.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL = dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE.Rows[0]["RITEM_UNIT"].ToString();
            }
            //四捨五入            
            w_txtFORMAT = dtCODE.Rows[0]["RITEM_FORMAT"].ToString();
            //找受檢人
            sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                     "FROM pat_visit A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pv_ic=B.pif_ic " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                    "ORDER BY B.pif_id ";
            sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                     "FROM pat_info B " +
                     " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 " +
                    "ORDER BY B.pif_id ";
            DataTable dtTOTAL = db.Query(sSQL);
            string w_txtTOTAL1 = dtTOTAL.Rows.Count.ToString();
            //找有做檢查的人    
            sSQL = "";
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE1 = db.Query(sSQL);
            if (dtCODE1.Rows.Count > 0)
            {
                w_txtRESULT_NAME = dtCODE1.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT = dtCODE1.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL = dtCODE1.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE1.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE1.Rows[0]["RITEM_UNIT"].ToString();
            }

            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + dtCODE1.Rows[0]["RITEM_NAME"].ToString() + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                   "FROM a_result_log A " +
                   "LEFT JOIN pat_info B " +
                    "ON A.PAT_NO=B.pif_id " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCHECK = db.Query(sSQL);
            //四捨五入
            if (w_txtFORMAT != "")
            {
                for (int n = 0; n < dtCHECK.Rows.Count; n++)
                {
                    dtCHECK.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT));
                    dtCHECK.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT);
                }
            }
            else
            {
                for (int n = 0; n < dtCHECK.Rows.Count; n++)
                {
                    dtCHECK.Rows[n]["RESULT_VALUE_T"] = dtCHECK.Rows[n]["RESULT_VALUE_N"].ToString();
                }
            }
            string w_txtCHECK = dtCHECK.Rows.Count.ToString();

            //算合格人數
            if (dtCODE.Rows[0]["RITEM_LOW1"].ToString() == "")
                dtCODE.Rows[0]["RITEM_LOW1"] = "0";
            if (dtCODE.Rows[0]["RITEM_HIGH1"].ToString() == "")
                dtCODE.Rows[0]["RITEM_HIGH1"] = "99999";
            System.Data.DataView dvCHECK;
            dvCHECK = dtCHECK.DefaultView;
            dvCHECK.RowFilter = "RESULT_VALUE_N>=" + dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_Y = dvCHECK.Count.ToString();
            //算不合格人數
            dvCHECK.RowFilter = "RESULT_VALUE_N<" + dtCODE.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_N = dvCHECK.Count.ToString();
            //受檢人-有做檢查的人
            DataTable dtUNCHECK = dtTOTAL.Copy();
            System.Data.DataView dvUNCHECK;
            dvUNCHECK = dtUNCHECK.DefaultView;
            string w_txtUNCHECK;
            string w_txtCHECK_P;
            string w_txtUNCHECK_P;
            string w_txtCHECK_YP;
            string w_txtCHECK_NP;
            for (int i = 0; i < dtCHECK.Rows.Count; i++)
            {
                dvUNCHECK.RowFilter = "pv_ic='" + dtCHECK.Rows[i]["pif_ic"] + "'";
                if (dvUNCHECK.Count > 0)
                    dvUNCHECK[0].Delete();
            }
            dtUNCHECK.AcceptChanges();
            w_txtUNCHECK = dtUNCHECK.Rows.Count.ToString();
            if (w_txtTOTAL1 == "0")
            {
                w_txtCHECK_P = "0";
                w_txtUNCHECK_P = "0";
            }
            else
            {
                w_txtCHECK_P = Percent(Convert.ToDouble(w_txtCHECK) / Convert.ToDouble(w_txtTOTAL1) * 100);
                w_txtUNCHECK_P = Percent(Convert.ToDouble(w_txtUNCHECK) / Convert.ToDouble(w_txtTOTAL1) * 100);
            }

            if (w_txtCHECK == "0")
            {
                w_txtCHECK_YP = "0";
                w_txtCHECK_NP = "0";
            }
            else
            {
                w_txtCHECK_YP = Percent(Convert.ToDouble(w_txtCHECK_Y) / Convert.ToDouble(w_txtCHECK) * 100);
                w_txtCHECK_NP = Percent(Convert.ToDouble(w_txtCHECK_N) / Convert.ToDouble(w_txtCHECK) * 100);
            }

            //[血清白蛋白ALB.] 
            sRESULT_CODE = "4008";
            string w_txtRESULT_NAME_A;
            string w_txtRESULT_UNIT_A;
            string w_txtNORMAL_A;
            string w_txtFORMAT_A;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_A = db.Query(sSQL);
            if (dtCODE_A.Rows.Count > 0)
            {
                w_txtRESULT_NAME_A = dtCODE_A.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_A = dtCODE_A.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_A = dtCODE_A.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_A.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_A.Rows[0]["RITEM_UNIT"].ToString();
            }
            //四捨五入            
            w_txtFORMAT_A = dtCODE_A.Rows[0]["RITEM_FORMAT"].ToString();
            //找受檢人
            sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                     "FROM pat_visit A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pv_ic=B.pif_ic " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                    "ORDER BY B.pif_id ";
            sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                     "FROM pat_info B " +
                     " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 " +
                    "ORDER BY B.pif_id ";
            DataTable dtTOTAL_A = db.Query(sSQL);
            string w_txtTOTAL1_A = dtTOTAL_A.Rows.Count.ToString();
            //找有做檢查的人    
            sSQL = "";
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE1_A = db.Query(sSQL);
            if (dtCODE1_A.Rows.Count > 0)
            {
                w_txtRESULT_NAME_A = dtCODE1_A.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_A = dtCODE1_A.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_A = dtCODE1_A.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE1_A.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE1_A.Rows[0]["RITEM_UNIT"].ToString();
            }
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + dtCODE1_A.Rows[0]["RITEM_NAME"].ToString() + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                   "FROM a_result_log A " +
                   "LEFT JOIN pat_info B " +
                   "ON A.PAT_NO=B.pif_id " +
                   " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCHECK_A = db.Query(sSQL);
            //四捨五入
            if (w_txtFORMAT_A != "")
            {
                for (int n = 0; n < dtCHECK_A.Rows.Count; n++)
                {
                    dtCHECK_A.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK_A.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_A));
                    dtCHECK_A.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK_A.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_A);
                }
            }
            else
            {
                for (int n = 0; n < dtCHECK_A.Rows.Count; n++)
                {
                    dtCHECK_A.Rows[n]["RESULT_VALUE_T"] = dtCHECK_A.Rows[n]["RESULT_VALUE_N"].ToString();
                }
            }
            string w_txtCHECK_A = dtCHECK_A.Rows.Count.ToString();
            //算合格人數
            if (dtCODE_A.Rows[0]["RITEM_LOW1"].ToString() == "")
                dtCODE_A.Rows[0]["RITEM_LOW1"] = "0";
            if (dtCODE_A.Rows[0]["RITEM_HIGH1"].ToString() == "")
                dtCODE_A.Rows[0]["RITEM_HIGH1"] = "99999";
            System.Data.DataView dvCHECK_A;
            dvCHECK_A = dtCHECK_A.DefaultView;
            dvCHECK_A.RowFilter = "RESULT_VALUE_N>=" + dtCODE_A.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE_A.Rows[0]["RITEM_HIGH1"].ToString();
            //Store istore21 = this.GridPanel21.GetStore();
            //istore21.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
            //istore21.DataBind();
            string w_txtCHECK_Y_A = dvCHECK_A.Count.ToString();
            //算不合格人數
            dvCHECK_A.RowFilter = "RESULT_VALUE_N<" + dtCODE_A.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE_A.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_N_A = dvCHECK_A.Count.ToString();
            //this.txtERR.Text = ""; 
            //受檢人-有做檢查的人
            DataTable dtUNCHECK_A = dtTOTAL_A.Copy();
            System.Data.DataView dvUNCHECK_A;
            dvUNCHECK_A = dtUNCHECK_A.DefaultView;
            string w_txtUNCHECK_A;
            string w_txtCHECK_P_A;
            string w_txtUNCHECK_P_A;
            string w_txtCHECK_YP_A;
            string w_txtCHECK_NP_A;
            for (int i = 0; i < dtCHECK_A.Rows.Count; i++)
            {
                dvUNCHECK_A.RowFilter = "pv_ic='" + dtCHECK_A.Rows[i]["pif_ic"] + "'";
                if (dvUNCHECK_A.Count > 0)
                    dvUNCHECK_A[0].Delete();
            }
            dtUNCHECK_A.AcceptChanges();
            w_txtUNCHECK_A = dtUNCHECK_A.Rows.Count.ToString();
            if (w_txtTOTAL1_A == "0")
            {
                w_txtCHECK_P_A = "0";
                w_txtUNCHECK_P_A = "0";
            }
            else
            {
                w_txtCHECK_P_A = Percent(Convert.ToDouble(w_txtCHECK_A) / Convert.ToDouble(w_txtTOTAL1_A) * 100);
                w_txtUNCHECK_P_A = Percent(Convert.ToDouble(w_txtUNCHECK_A) / Convert.ToDouble(w_txtTOTAL1_A) * 100);
            }

            if (w_txtCHECK_A == "0")
            {
                w_txtCHECK_YP_A = "0";
                w_txtCHECK_NP_A = "0";
            }
            else
            {
                w_txtCHECK_YP_A = Percent(Convert.ToDouble(w_txtCHECK_Y_A) / Convert.ToDouble(w_txtCHECK_A) * 100);
                w_txtCHECK_NP_A = Percent(Convert.ToDouble(w_txtCHECK_N_A) / Convert.ToDouble(w_txtCHECK_A) * 100);
            }

            //[鈣Ca.]  
            sRESULT_CODE = "4021";
            string w_txtRESULT_NAME_D;
            string w_txtRESULT_UNIT_D;
            string w_txtNORMAL_D;
            string w_txtFORMAT_D;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_D = db.Query(sSQL);
            if (dtCODE_D.Rows.Count > 0)
            {
                w_txtRESULT_NAME_D = dtCODE_D.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_D = dtCODE_D.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_D = dtCODE_D.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_D.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_D.Rows[0]["RITEM_UNIT"].ToString();
            }
            //四捨五入            
            w_txtFORMAT_D = dtCODE_D.Rows[0]["RITEM_FORMAT"].ToString();
            //找受檢人
            sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                     "FROM pat_visit A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pv_ic=B.pif_ic " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                    "ORDER BY B.pif_id ";
            sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                     "FROM pat_info B " +
                     " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 " +
                    "ORDER BY B.pif_id ";
            DataTable dtTOTAL_D = db.Query(sSQL);
            string w_txtTOTAL1_D = dtTOTAL_D.Rows.Count.ToString();
            //找有做檢查的人    
            sSQL = "";
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE1_D = db.Query(sSQL);
            if (dtCODE1_D.Rows.Count > 0)
            {
                w_txtRESULT_NAME_D = dtCODE1_D.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_D = dtCODE1_D.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_D = dtCODE1_D.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE1_D.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE1_D.Rows[0]["RITEM_UNIT"].ToString();
            }
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + dtCODE1_D.Rows[0]["RITEM_NAME"].ToString() + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                   "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                     " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCHECK_D = db.Query(sSQL);
            //四捨五入
            if (w_txtFORMAT_D != "")
            {
                for (int n = 0; n < dtCHECK_D.Rows.Count; n++)
                {
                    dtCHECK_D.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK_D.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_D));
                    dtCHECK_D.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK_D.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_D);
                }
            }
            else
            {
                for (int n = 0; n < dtCHECK_D.Rows.Count; n++)
                {
                    dtCHECK_D.Rows[n]["RESULT_VALUE_T"] = dtCHECK_D.Rows[n]["RESULT_VALUE_N"].ToString();
                }
            }
            // Store istore22 = this.GridPanel22.GetStore();
            //istore22.DataSource = db.GetDataArray_AddRowNum(dtCHECK);
            //istore22.DataBind();
            string w_txtCHECK_D = dtCHECK_D.Rows.Count.ToString();
            //算合格人數
            if (dtCODE_D.Rows[0]["RITEM_LOW1"].ToString() == "")
                dtCODE_D.Rows[0]["RITEM_LOW1"] = "0";
            if (dtCODE_D.Rows[0]["RITEM_HIGH1"].ToString() == "")
                dtCODE_D.Rows[0]["RITEM_HIGH1"] = "99999";
            System.Data.DataView dvCHECK_D;
            dvCHECK_D = dtCHECK_D.DefaultView;

            dvCHECK_D.RowFilter = "RESULT_VALUE_N>=" + dtCODE_D.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE_D.Rows[0]["RITEM_HIGH1"].ToString();
            //Store istore21 = this.GridPanel21.GetStore();
            //istore21.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
            //istore21.DataBind();
            string w_txtCHECK_Y_D = dvCHECK_D.Count.ToString();
            //算不合格人數
            //System.Data.DataView dvCHECK_N;
            //dvCHECK_N = dtCHECK.DefaultView;
            dvCHECK_D.RowFilter = "RESULT_VALUE_N<" + dtCODE_D.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE_D.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_N_D = dvCHECK_D.Count.ToString();
            //Store istore1 = this.GridPanel1.GetStore();
            //istore1.DataSource = db.GetDataArray_AddRowNum(dvCHECK.ToTable());
            //istore1.DataBind();
            //this.txtERR.Text = ""; 
            //受檢人-有做檢查的人
            DataTable dtUNCHECK_D = dtTOTAL_D.Copy();
            System.Data.DataView dvUNCHECK_D;
            dvUNCHECK_D = dtUNCHECK_D.DefaultView;
            string w_txtUNCHECK_D;
            string w_txtCHECK_P_D;
            string w_txtUNCHECK_P_D;
            string w_txtCHECK_YP_D;
            string w_txtCHECK_NP_D;
            for (int i = 0; i < dtCHECK_D.Rows.Count; i++)
            {
                dvUNCHECK_D.RowFilter = "pv_ic='" + dtCHECK_D.Rows[i]["pif_ic"] + "'";
                if (dvUNCHECK_D.Count > 0)
                    dvUNCHECK_D[0].Delete();
            }
            dtUNCHECK_D.AcceptChanges();
            w_txtUNCHECK_D = dtUNCHECK_D.Rows.Count.ToString();
            //Store istore2 = this.GridPanel2.GetStore();
            //istore2.DataSource = db.GetDataArray_AddRowNum(dtUNCHECK);
            //istore2.DataBind();
            if (w_txtTOTAL1_D == "0")
            {
                w_txtCHECK_P_D = "0";
                w_txtUNCHECK_P_D = "0";
            }
            else
            {
                w_txtCHECK_P_D = Percent(Convert.ToDouble(w_txtCHECK_D) / Convert.ToDouble(w_txtTOTAL1_D) * 100);
                w_txtUNCHECK_P_D = Percent(Convert.ToDouble(w_txtUNCHECK_D) / Convert.ToDouble(w_txtTOTAL1_D) * 100);
            }

            if (w_txtCHECK_D == "0")
            {
                w_txtCHECK_YP_D = "0";
                w_txtCHECK_NP_D = "0";
            }
            else
            {
                w_txtCHECK_YP_D = Percent(Convert.ToDouble(w_txtCHECK_Y_D) / Convert.ToDouble(w_txtCHECK_D) * 100);
                w_txtCHECK_NP_D = Percent(Convert.ToDouble(w_txtCHECK_N_D) / Convert.ToDouble(w_txtCHECK_D) * 100);
            }

            //[磷P.]  
            sRESULT_CODE = "4023";
            string w_txtRESULT_NAME_E;
            string w_txtRESULT_UNIT_E;
            string w_txtNORMAL_E;
            string w_txtFORMAT_E;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_E = db.Query(sSQL);
            if (dtCODE_E.Rows.Count > 0)
            {
                w_txtRESULT_NAME_E = dtCODE_E.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_E = dtCODE_E.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_E = dtCODE_E.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_E.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_E.Rows[0]["RITEM_UNIT"].ToString();
            }
            //四捨五入            
            w_txtFORMAT_E = dtCODE_E.Rows[0]["RITEM_FORMAT"].ToString();
            //找受檢人
            sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                     "FROM pat_visit A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pv_ic=B.pif_ic " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                    "ORDER BY B.pif_id ";
            sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                     "FROM pat_info B " +
                     " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 " +
                    "ORDER BY B.pif_id ";
            DataTable dtTOTAL_E = db.Query(sSQL);
            string w_txtTOTAL1_E = dtTOTAL_E.Rows.Count.ToString();
            //找有做檢查的人    
            sSQL = "";
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE1_E = db.Query(sSQL);
            if (dtCODE1_E.Rows.Count > 0)
            {
                w_txtRESULT_NAME_E = dtCODE1_E.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_E = dtCODE1_E.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_E = dtCODE1_E.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE1_E.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE1_E.Rows[0]["RITEM_UNIT"].ToString();
            }
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + dtCODE1_E.Rows[0]["RITEM_NAME"].ToString() + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                   "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCHECK_E = db.Query(sSQL);
            //四捨五入
            if (w_txtFORMAT_E != "")
            {
                for (int n = 0; n < dtCHECK_E.Rows.Count; n++)
                {
                    dtCHECK_E.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK_E.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_E));
                    dtCHECK_E.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK_E.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_E);
                }
            }
            else
            {
                for (int n = 0; n < dtCHECK_E.Rows.Count; n++)
                {
                    dtCHECK_E.Rows[n]["RESULT_VALUE_T"] = dtCHECK_E.Rows[n]["RESULT_VALUE_N"].ToString();
                }
            }
            string w_txtCHECK_E = dtCHECK_E.Rows.Count.ToString();
            //算合格人數
            if (dtCODE_E.Rows[0]["RITEM_LOW1"].ToString() == "")
                dtCODE_E.Rows[0]["RITEM_LOW1"] = "0";
            if (dtCODE_E.Rows[0]["RITEM_HIGH1"].ToString() == "")
                dtCODE_E.Rows[0]["RITEM_HIGH1"] = "99999";
            System.Data.DataView dvCHECK_E;
            dvCHECK_E = dtCHECK_E.DefaultView;
            dvCHECK_E.RowFilter = "RESULT_VALUE_N>=" + dtCODE_E.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE_E.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_Y_E = dvCHECK_E.Count.ToString();
            //算不合格人數
            dvCHECK_E.RowFilter = "RESULT_VALUE_N<" + dtCODE_E.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE_E.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_N_E = dvCHECK_E.Count.ToString();
            //受檢人-有做檢查的人
            DataTable dtUNCHECK_E = dtTOTAL_E.Copy();
            System.Data.DataView dvUNCHECK_E;
            dvUNCHECK_E = dtUNCHECK_E.DefaultView;
            string w_txtUNCHECK_E;
            string w_txtCHECK_P_E;
            string w_txtUNCHECK_P_E;
            string w_txtCHECK_YP_E;
            string w_txtCHECK_NP_E;
            for (int i = 0; i < dtCHECK_E.Rows.Count; i++)
            {
                dvUNCHECK_E.RowFilter = "pv_ic='" + dtCHECK_E.Rows[i]["pif_ic"] + "'";
                if (dvUNCHECK_E.Count > 0)
                    dvUNCHECK_E[0].Delete();
            }
            dtUNCHECK_E.AcceptChanges();
            w_txtUNCHECK_E = dtUNCHECK_E.Rows.Count.ToString();
            if (w_txtTOTAL1_E == "0")
            {
                w_txtCHECK_P_E = "0";
                w_txtUNCHECK_P_E = "0";
            }
            else
            {
                w_txtCHECK_P_E = Percent(Convert.ToDouble(w_txtCHECK_E) / Convert.ToDouble(w_txtTOTAL1_E) * 100);
                w_txtUNCHECK_P_E = Percent(Convert.ToDouble(w_txtUNCHECK_E) / Convert.ToDouble(w_txtTOTAL1_E) * 100);
            }

            if (w_txtCHECK_E == "0")
            {
                w_txtCHECK_YP_E = "0";
                w_txtCHECK_NP_E = "0";
            }
            else
            {
                w_txtCHECK_YP_E = Percent(Convert.ToDouble(w_txtCHECK_Y_E) / Convert.ToDouble(w_txtCHECK_E) * 100);
                w_txtCHECK_NP_E = Percent(Convert.ToDouble(w_txtCHECK_N_E) / Convert.ToDouble(w_txtCHECK_E) * 100);
            }
            //

            //[轉鐵蛋白飽合度 4050.] 
            sRESULT_CODE = "4050";
            string w_txtRESULT_NAME_F;
            string w_txtRESULT_UNIT_F;
            string w_txtNORMAL_F;
            string w_txtFORMAT_F;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_F = db.Query(sSQL);
            if (dtCODE_F.Rows.Count > 0)
            {
                w_txtRESULT_NAME_F = dtCODE_F.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_F = dtCODE_F.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_F = dtCODE_F.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_F.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_F.Rows[0]["RITEM_UNIT"].ToString();
            }
            //四捨五入            
            w_txtFORMAT_F = dtCODE_F.Rows[0]["RITEM_FORMAT"].ToString();

            //找受檢人
            sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                     "FROM pat_visit A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pv_ic=B.pif_ic " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                    "ORDER BY B.pif_id ";
            sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                     "FROM pat_info B " +
                     " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 " +
                    "ORDER BY B.pif_id ";
            DataTable dtTOTAL_F = db.Query(sSQL);
            string w_txtTOTAL1_F = dtTOTAL_F.Rows.Count.ToString();
            //找有做檢查的人    
            sSQL = "";
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE1_F = db.Query(sSQL);
            if (dtCODE1_F.Rows.Count > 0)
            {
                w_txtRESULT_NAME_F = dtCODE1_F.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_F = dtCODE1_F.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_F = dtCODE1_F.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE1_F.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE1_F.Rows[0]["RITEM_UNIT"].ToString();
            }
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + dtCODE1_F.Rows[0]["RITEM_NAME"].ToString() + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                   "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCHECK_F = db.Query(sSQL);
            //四捨五入
            if (w_txtFORMAT_F != "")
            {
                for (int n = 0; n < dtCHECK_F.Rows.Count; n++)
                {
                    dtCHECK_F.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK_F.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_F));
                    dtCHECK_F.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK_F.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_F);
                }
            }
            else
            {
                for (int n = 0; n < dtCHECK_F.Rows.Count; n++)
                {
                    dtCHECK_F.Rows[n]["RESULT_VALUE_T"] = dtCHECK_F.Rows[n]["RESULT_VALUE_N"].ToString();
                }
            }

            string w_txtCHECK_F = dtCHECK_F.Rows.Count.ToString();
            //算合格人數
            if (dtCODE_F.Rows[0]["RITEM_LOW1"].ToString() == "")
                dtCODE_F.Rows[0]["RITEM_LOW1"] = "0";
            if (dtCODE_F.Rows[0]["RITEM_HIGH1"].ToString() == "")
                dtCODE_F.Rows[0]["RITEM_HIGH1"] = "99999";
            System.Data.DataView dvCHECK_F;
            dvCHECK_F = dtCHECK_F.DefaultView;
            dvCHECK_F.RowFilter = "RESULT_VALUE_N>=" + dtCODE_F.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE_F.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_Y_F = dvCHECK_F.Count.ToString();
            //算不合格人數
            dvCHECK_F.RowFilter = "RESULT_VALUE_N<" + dtCODE_F.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE_F.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_N_F = dvCHECK_F.Count.ToString();
            //受檢人-有做檢查的人
            DataTable dtUNCHECK_F = dtTOTAL_F.Copy();
            System.Data.DataView dvUNCHECK_F;
            dvUNCHECK_F = dtUNCHECK_F.DefaultView;
            string w_txtUNCHECK_F;
            string w_txtCHECK_P_F;
            string w_txtUNCHECK_P_F;
            string w_txtCHECK_YP_F;
            string w_txtCHECK_NP_F;
            for (int i = 0; i < dtCHECK_F.Rows.Count; i++)
            {
                dvUNCHECK_F.RowFilter = "pv_ic='" + dtCHECK_F.Rows[i]["pif_ic"] + "'";
                if (dvUNCHECK_F.Count > 0)
                    dvUNCHECK_F[0].Delete();
            }

            dtUNCHECK_F.AcceptChanges();
            w_txtUNCHECK_F = dtUNCHECK_F.Rows.Count.ToString();
            if (w_txtTOTAL1_F == "0")
            {
                w_txtCHECK_P_F = "0";
                w_txtUNCHECK_P_F = "0";
            }
            else
            {
                w_txtCHECK_P_F = Percent(Convert.ToDouble(w_txtCHECK_F) / Convert.ToDouble(w_txtTOTAL1_F) * 100);
                w_txtUNCHECK_P_F = Percent(Convert.ToDouble(w_txtUNCHECK_F) / Convert.ToDouble(w_txtTOTAL1_F) * 100);
            }

            if (w_txtCHECK_F == "0")
            {
                w_txtCHECK_YP_F = "0";
                w_txtCHECK_NP_F = "0";
            }
            else
            {
                w_txtCHECK_YP_F = Percent(Convert.ToDouble(w_txtCHECK_Y_F) / Convert.ToDouble(w_txtCHECK_F) * 100);
                w_txtCHECK_NP_F = Percent(Convert.ToDouble(w_txtCHECK_N_F) / Convert.ToDouble(w_txtCHECK_F) * 100);
            }
            //

            //[鐵蛋白SF.]
            sRESULT_CODE = "4027";
            string w_txtRESULT_NAME_G;
            string w_txtRESULT_UNIT_G;
            string w_txtNORMAL_G;
            string w_txtFORMAT_G;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_G = db.Query(sSQL);
            if (dtCODE_G.Rows.Count > 0)
            {
                w_txtRESULT_NAME_G = dtCODE_G.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_G = dtCODE_G.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_G = dtCODE_G.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_G.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_G.Rows[0]["RITEM_UNIT"].ToString();
            }
            //四捨五入            
            w_txtFORMAT_G = dtCODE_G.Rows[0]["RITEM_FORMAT"].ToString();
            //找受檢人
            sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                     "FROM pat_visit A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pv_ic=B.pif_ic " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                    "ORDER BY B.pif_id ";
            sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                     "FROM pat_info B " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 " +
                    "ORDER BY B.pif_id ";
            DataTable dtTOTAL_G = db.Query(sSQL);
            string w_txtTOTAL1_G = dtTOTAL_G.Rows.Count.ToString();
            //找有做檢查的人    
            sSQL = "";
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE1_G = db.Query(sSQL);
            if (dtCODE1_G.Rows.Count > 0)
            {
                w_txtRESULT_NAME_G = dtCODE1_G.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_G = dtCODE1_G.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_G = dtCODE1_G.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE1_G.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE1_G.Rows[0]["RITEM_UNIT"].ToString();
            }
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + dtCODE1_G.Rows[0]["RITEM_NAME"].ToString() + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                   "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCHECK_G = db.Query(sSQL);
            //四捨五入
            if (w_txtFORMAT_G != "")
            {
                for (int n = 0; n < dtCHECK_G.Rows.Count; n++)
                {
                    dtCHECK_G.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK_G.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_G));
                    dtCHECK_G.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK_G.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_G);
                }
            }
            else
            {
                for (int n = 0; n < dtCHECK_G.Rows.Count; n++)
                {
                    dtCHECK_G.Rows[n]["RESULT_VALUE_T"] = dtCHECK_G.Rows[n]["RESULT_VALUE_N"].ToString();
                }
            }
            string w_txtCHECK_G = dtCHECK_G.Rows.Count.ToString();
            //算合格人數
            if (dtCODE_G.Rows[0]["RITEM_LOW1"].ToString() == "")
                dtCODE_G.Rows[0]["RITEM_LOW1"] = "0";
            if (dtCODE_G.Rows[0]["RITEM_HIGH1"].ToString() == "")
                dtCODE_G.Rows[0]["RITEM_HIGH1"] = "99999";
            System.Data.DataView dvCHECK_G;
            dvCHECK_G = dtCHECK_G.DefaultView;
            dvCHECK_G.RowFilter = "RESULT_VALUE_N>=" + dtCODE_G.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE_G.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_Y_G = dvCHECK_G.Count.ToString();
            //算不合格人數
            dvCHECK_G.RowFilter = "RESULT_VALUE_N<" + dtCODE_G.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE_G.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_N_G = dvCHECK_G.Count.ToString();
            //受檢人-有做檢查的人
            DataTable dtUNCHECK_G = dtTOTAL_G.Copy();
            System.Data.DataView dvUNCHECK_G;
            dvUNCHECK_G = dtUNCHECK_G.DefaultView;
            string w_txtUNCHECK_G;
            string w_txtCHECK_P_G;
            string w_txtUNCHECK_P_G;
            string w_txtCHECK_YP_G;
            string w_txtCHECK_NP_G;
            for (int i = 0; i < dtCHECK_G.Rows.Count; i++)
            {
                dvUNCHECK_G.RowFilter = "pv_ic='" + dtCHECK_G.Rows[i]["pif_ic"] + "'";
                if (dvUNCHECK_G.Count > 0)
                    dvUNCHECK_G[0].Delete();
            }
            dtUNCHECK_G.AcceptChanges();
            w_txtUNCHECK_G = dtUNCHECK_G.Rows.Count.ToString();
            if (w_txtTOTAL1_G == "0")
            {
                w_txtCHECK_P_G = "0";
                w_txtUNCHECK_P_G = "0";
            }
            else
            {
                w_txtCHECK_P_G = Percent(Convert.ToDouble(w_txtCHECK_G) / Convert.ToDouble(w_txtTOTAL1_G) * 100);
                w_txtUNCHECK_P_G = Percent(Convert.ToDouble(w_txtUNCHECK_G) / Convert.ToDouble(w_txtTOTAL1_G) * 100);
            }

            if (w_txtCHECK_G == "0")
            {
                w_txtCHECK_YP_G = "0";
                w_txtCHECK_NP_G = "0";
            }
            else
            {
                w_txtCHECK_YP_G = Percent(Convert.ToDouble(w_txtCHECK_Y_G) / Convert.ToDouble(w_txtCHECK_G) * 100);
                w_txtCHECK_NP_G = Percent(Convert.ToDouble(w_txtCHECK_N_G) / Convert.ToDouble(w_txtCHECK_G) * 100);
            }
            //

            //IPTH                
            sRESULT_CODE = "4030";
            string w_txtRESULT_NAME_H;
            string w_txtRESULT_UNIT_H;
            string w_txtNORMAL_H;
            string w_txtFORMAT_H;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_H = db.Query(sSQL);
            if (dtCODE_H.Rows.Count > 0)
            {
                w_txtRESULT_NAME_H = dtCODE_H.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_H = dtCODE_H.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_H = dtCODE_H.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_H.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_H.Rows[0]["RITEM_UNIT"].ToString();
            }
            //四捨五入            
            w_txtFORMAT_H = dtCODE_H.Rows[0]["RITEM_FORMAT"].ToString();
            //找受檢人
            sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                     "FROM pat_visit A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pv_ic=B.pif_ic " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                    "ORDER BY B.pif_id ";
            sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                     "FROM pat_info B " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 " +
                    "ORDER BY B.pif_id ";
            DataTable dtTOTAL_H = db.Query(sSQL);
            string w_txtTOTAL1_H = dtTOTAL_H.Rows.Count.ToString();
            //找有做檢查的人    
            sSQL = "";
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE1_H = db.Query(sSQL);
            if (dtCODE1_H.Rows.Count > 0)
            {
                w_txtRESULT_NAME_H = dtCODE1_H.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_H = dtCODE1_H.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_H = dtCODE1_H.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE1_H.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE1_H.Rows[0]["RITEM_UNIT"].ToString();
            }
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + dtCODE1_H.Rows[0]["RITEM_NAME"].ToString() + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                   "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCHECK_H = db.Query(sSQL);
            //四捨五入
            if (w_txtFORMAT_H != "")
            {
                for (int n = 0; n < dtCHECK_H.Rows.Count; n++)
                {
                    dtCHECK_H.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK_H.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_H));
                    dtCHECK_H.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK_H.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_H);
                }
            }
            else
            {
                for (int n = 0; n < dtCHECK_H.Rows.Count; n++)
                {
                    dtCHECK_H.Rows[n]["RESULT_VALUE_T"] = dtCHECK_H.Rows[n]["RESULT_VALUE_N"].ToString();
                }
            }
            string w_txtCHECK_H = dtCHECK_H.Rows.Count.ToString();
            //算合格人數
            if (dtCODE_H.Rows[0]["RITEM_LOW1"].ToString() == "")
                dtCODE_H.Rows[0]["RITEM_LOW1"] = "0";
            if (dtCODE_H.Rows[0]["RITEM_HIGH1"].ToString() == "")
                dtCODE_H.Rows[0]["RITEM_HIGH1"] = "99999";
            System.Data.DataView dvCHECK_H;
            dvCHECK_H = dtCHECK_H.DefaultView;
            dvCHECK_H.RowFilter = "RESULT_VALUE_N>=" + dtCODE_H.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE_H.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_Y_H = dvCHECK_H.Count.ToString();
            //算不合格人數
            dvCHECK_H.RowFilter = "RESULT_VALUE_N<" + dtCODE_H.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE_H.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_N_H = dvCHECK_H.Count.ToString();
            //受檢人-有做檢查的人
            DataTable dtUNCHECK_H = dtTOTAL_H.Copy();
            System.Data.DataView dvUNCHECK_H;
            dvUNCHECK_H = dtUNCHECK_H.DefaultView;
            string w_txtUNCHECK_H;
            string w_txtCHECK_P_H;
            string w_txtUNCHECK_P_H;
            string w_txtCHECK_YP_H;
            string w_txtCHECK_NP_H;
            for (int i = 0; i < dtCHECK_H.Rows.Count; i++)
            {
                dvUNCHECK_H.RowFilter = "pv_ic='" + dtCHECK_H.Rows[i]["pif_ic"] + "'";
                if (dvUNCHECK_H.Count > 0)
                    dvUNCHECK_H[0].Delete();
            }
            dtUNCHECK_H.AcceptChanges();
            w_txtUNCHECK_H = dtUNCHECK_H.Rows.Count.ToString();
            if (w_txtTOTAL1_H == "0")
            {
                w_txtCHECK_P_H = "0";
                w_txtUNCHECK_P_H = "0";
            }
            else
            {
                w_txtCHECK_P_H = Percent(Convert.ToDouble(w_txtCHECK_H) / Convert.ToDouble(w_txtTOTAL1_H) * 100);
                w_txtUNCHECK_P_H = Percent(Convert.ToDouble(w_txtUNCHECK_H) / Convert.ToDouble(w_txtTOTAL1_H) * 100);
            }

            if (w_txtCHECK_H == "0")
            {
                w_txtCHECK_YP_H = "0";
                w_txtCHECK_NP_H = "0";
            }
            else
            {
                w_txtCHECK_YP_H = Percent(Convert.ToDouble(w_txtCHECK_Y_H) / Convert.ToDouble(w_txtCHECK_H) * 100);
                w_txtCHECK_NP_H = Percent(Convert.ToDouble(w_txtCHECK_N_H) / Convert.ToDouble(w_txtCHECK_H) * 100);
            }
            //

            //kt/v 5018
            sRESULT_CODE = "5018";
            string w_txtRESULT_NAME_I;
            string w_txtRESULT_UNIT_I;
            string w_txtNORMAL_I;
            string w_txtFORMAT_I;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_I = db.Query(sSQL);
            if (dtCODE_I.Rows.Count > 0)
            {
                w_txtRESULT_NAME_I = dtCODE_I.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_I = dtCODE_I.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_I = dtCODE_I.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_I.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_I.Rows[0]["RITEM_UNIT"].ToString();
            }
            //四捨五入            
            w_txtFORMAT_I = dtCODE_I.Rows[0]["RITEM_FORMAT"].ToString();
            //找受檢人
            sSQL = "SELECT DISTINCT B.pif_name, A.pv_ic, B.pif_id " +
                     "FROM pat_visit A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pv_ic=B.pif_ic " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' " +
                    "ORDER BY B.pif_id ";
            sSQL = "SELECT B.pif_name, B.pif_ic AS pv_ic, B.pif_id " +
                     "FROM pat_info B " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE 1=1 " +
                    "ORDER BY B.pif_id ";
            DataTable dtTOTAL_I = db.Query(sSQL);
            string w_txtTOTAL1_I = dtTOTAL_I.Rows.Count.ToString();
            //找有做檢查的人    
            sSQL = "";
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE1_I = db.Query(sSQL);
            if (dtCODE1_I.Rows.Count > 0)
            {
                w_txtRESULT_NAME_I = dtCODE1_I.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_I = dtCODE1_I.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_I = dtCODE1_I.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE1_I.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE1_I.Rows[0]["RITEM_UNIT"].ToString();
            }
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, '" + dtCODE1_I.Rows[0]["RITEM_NAME"].ToString() + "' AS RESULT_NAME, '' AS RESULT_VALUE_T, AVG(A.RESULT_VALUE_N) AS RESULT_VALUE_N " +
                   "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                    " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCHECK_I = db.Query(sSQL);
            //四捨五入
            if (w_txtFORMAT_I != "")
            {
                for (int n = 0; n < dtCHECK_I.Rows.Count; n++)
                {
                    dtCHECK_I.Rows[n]["RESULT_VALUE_N"] = Convert.ToDouble(Convert.ToDouble(dtCHECK_I.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_I));
                    dtCHECK_I.Rows[n]["RESULT_VALUE_T"] = Convert.ToDouble(dtCHECK_I.Rows[n]["RESULT_VALUE_N"]).ToString(w_txtFORMAT_I);
                }
            }
            else
            {
                for (int n = 0; n < dtCHECK_I.Rows.Count; n++)
                {
                    dtCHECK_I.Rows[n]["RESULT_VALUE_T"] = dtCHECK_I.Rows[n]["RESULT_VALUE_N"].ToString();
                }
            }
            string w_txtCHECK_I = dtCHECK_I.Rows.Count.ToString();
            //算合格人數
            if (dtCODE_I.Rows[0]["RITEM_LOW1"].ToString() == "")
                dtCODE_I.Rows[0]["RITEM_LOW1"] = "0";
            if (dtCODE_I.Rows[0]["RITEM_HIGH1"].ToString() == "")
                dtCODE_I.Rows[0]["RITEM_HIGH1"] = "99999";
            System.Data.DataView dvCHECK_I;
            dvCHECK_I = dtCHECK_I.DefaultView;
            dvCHECK_I.RowFilter = "RESULT_VALUE_N>=" + dtCODE_I.Rows[0]["RITEM_LOW1"].ToString() + " AND RESULT_VALUE_N<=" + dtCODE_I.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_Y_I = dvCHECK_I.Count.ToString();
            //算不合格人數
            dvCHECK_I.RowFilter = "RESULT_VALUE_N<" + dtCODE_I.Rows[0]["RITEM_LOW1"].ToString() + " OR RESULT_VALUE_N>" + dtCODE_I.Rows[0]["RITEM_HIGH1"].ToString();
            string w_txtCHECK_N_I = dvCHECK_I.Count.ToString();
            //受檢人-有做檢查的人
            DataTable dtUNCHECK_I = dtTOTAL_I.Copy();
            System.Data.DataView dvUNCHECK_I;
            dvUNCHECK_I = dtUNCHECK_I.DefaultView;
            string w_txtUNCHECK_I;
            string w_txtCHECK_P_I;
            string w_txtUNCHECK_P_I;
            string w_txtCHECK_YP_I;
            string w_txtCHECK_NP_I;
            for (int i = 0; i < dtCHECK_I.Rows.Count; i++)
            {
                dvUNCHECK_I.RowFilter = "pv_ic='" + dtCHECK_I.Rows[i]["pif_ic"] + "'";
                if (dvUNCHECK_I.Count > 0)
                    dvUNCHECK_I[0].Delete();
            }
            dtUNCHECK_I.AcceptChanges();
            w_txtUNCHECK_I = dtUNCHECK_I.Rows.Count.ToString();
            if (w_txtTOTAL1_I == "0")
            {
                w_txtCHECK_P_I = "0";
                w_txtUNCHECK_P_I = "0";
            }
            else
            {
                w_txtCHECK_P_I = Percent(Convert.ToDouble(w_txtCHECK_I) / Convert.ToDouble(w_txtTOTAL1_I) * 100);
                w_txtUNCHECK_P_I = Percent(Convert.ToDouble(w_txtUNCHECK_I) / Convert.ToDouble(w_txtTOTAL1_I) * 100);
            }

            if (w_txtCHECK_I == "0")
            {
                w_txtCHECK_YP_I = "0";
                w_txtCHECK_NP_I = "0";
            }
            else
            {
                w_txtCHECK_YP_I = Percent(Convert.ToDouble(w_txtCHECK_Y_I) / Convert.ToDouble(w_txtCHECK_I) * 100);
                w_txtCHECK_NP_I = Percent(Convert.ToDouble(w_txtCHECK_N_I) / Convert.ToDouble(w_txtCHECK_I) * 100);
            }
            //                     

            //瘘管重建

            //找受檢人
            //sSQL = "SELECT B.pif_name, A.pv_ic, B.pif_id " +
            //         "FROM pat_visit A " +
            //         "LEFT JOIN pat_info B " +
            //           "ON A.pv_ic=B.pif_ic " +
            //        "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
            //          "AND A.pv_datevisit<='" + sEND_DATE + "' " +
            //        "ORDER BY B.pif_id ";
            sSQL = "SELECT A.pv_ic, A.pv_datevisit " +
                     "FROM      pat_visit A " +
                     "left join pat_info  B " +
                     " on A.pv_ic=B.pif_ic   " +
                     "left join zinfo_a_07 f" +
                     " on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.pv_datevisit>='" + sBEG_DATE + "' " +
                      "AND A.pv_datevisit<='" + sEND_DATE + "' ";
            DataTable dtTOTAL_12 = db.Query(sSQL);
            string w_txtTOTAL12 = dtTOTAL_12.Rows.Count.ToString();

            //找瘘管重建的人
            sSQL = "SELECT B.pif_name, B.pif_sex, B.pif_ic, A.pat_id AS PAT_NO, " +
                          "A.info_date AS HD_DATE, A.txt_10 AS HD_CAUSE, A.opt_9 " +
                     "FROM zinfo_e_02 A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.pat_id=B.pif_id " +
                     " left join zinfo_a_07 f on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.info_date>='" + sBEG_DATE + "' " +
                      "AND A.info_date<='" + sEND_DATE + "' " +
                      "AND A.opt_8=1 ";
            DataTable dtHD_12 = db.Query(sSQL);
            string w_txtHD_12 = dtHD_12.Rows.Count.ToString();
            for (int i = 0; i < dtHD_12.Rows.Count; i++)
            {
                switch (dtHD_12.Rows[i]["opt_9"].ToString())
                {
                    case "1":
                        dtHD_12.Rows[i]["HD_CAUSE"] = "导管感染";
                        break;
                    case "2":
                        dtHD_12.Rows[i]["HD_CAUSE"] = "内瘘阻塞";
                        break;
                    case "3":
                        dtHD_12.Rows[i]["HD_CAUSE"] = "血流量过小(内瘘狭窄)";
                        break;
                    case "4":
                        dtHD_12.Rows[i]["HD_CAUSE"] = "血流量过大(内瘘成熟)";
                        break;
                    case "5":
                        dtHD_12.Rows[i]["HD_CAUSE"] = "长期导管移位";
                        break;
                    case "6":
                        dtHD_12.Rows[i]["HD_CAUSE"] = "窃流症候群";
                        break;
                }
                switch (dtHD_12.Rows[i]["pif_sex"].ToString().Trim())
                {
                    case "F":
                        dtHD_12.Rows[i]["pif_sex"] = "女";
                        break;
                    case "M":
                        dtHD_12.Rows[i]["pif_sex"] = "男";
                        break;
                }
            }
            string w_txtHD_P_L;
            if (w_txtTOTAL12 == "0")
            {
                w_txtHD_P_L = "0";
            }
            else
            {
                w_txtHD_P_L = Percent(Convert.ToDouble(w_txtHD_12) / Convert.ToDouble(w_txtTOTAL12) * 100);
            }
            ///////

            //檢驗 HBsAg 4032
            sRESULT_CODE = "4032";
            string w_txtRESULT_NAME_J;
            string w_txtRESULT_UNIT_J;
            string w_txtNORMAL_J;
            string w_txtFORMAT_J;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_J = db.Query(sSQL);
            if (dtCODE_J.Rows.Count > 0)
            {
                w_txtRESULT_NAME_J = dtCODE_J.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_J = dtCODE_J.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_J = dtCODE_J.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_J.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_J.Rows[0]["RITEM_UNIT"].ToString();
            }
            //找有做檢查的人
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE,  A.RESULT_VALUE_T " +
                     "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                    " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                      "AND A.RESULT_VALUE_T IN ('阴性','阳性') " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCODE1_J = db.Query(sSQL);
            string w_txtTOTAL9_J = dtCODE1_J.Rows.Count.ToString();

            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, A.RESULT_VALUE_T " +
                     "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                     " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                      "AND A.RESULT_VALUE_T='" + "阳性" + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtPOSITIVE_J = db.Query(sSQL);
            string w_txtPOSITIVE_J = dtPOSITIVE_J.Rows.Count.ToString();
            string w_txtPOSITIVE_JP;
            if (w_txtTOTAL4 == "0")
            {
                w_txtPOSITIVE_JP = "0";
            }
            else
            {
                w_txtPOSITIVE_JP = Percent(Convert.ToDouble(w_txtPOSITIVE_J) / Convert.ToDouble(w_txtTOTAL4) * 1000);
            }
            //

            //檢驗 antihcv 4033
            sRESULT_CODE = "4033";
            string w_txtRESULT_NAME_K;
            string w_txtRESULT_UNIT_K;
            string w_txtNORMAL_K;
            string w_txtFORMAT_K;
            //找檢查項目
            sSQL = "SELECT * FROM a_ritem_setup WHERE RITEM_CODE='" + sRESULT_CODE + "' ";
            DataTable dtCODE_K = db.Query(sSQL);
            if (dtCODE_K.Rows.Count > 0)
            {
                w_txtRESULT_NAME_K = dtCODE_K.Rows[0]["RITEM_NAME"].ToString();
                w_txtRESULT_UNIT_K = dtCODE_K.Rows[0]["RITEM_UNIT"].ToString();
                w_txtNORMAL_K = dtCODE_K.Rows[0]["RITEM_LOW1"].ToString() + " ~ " + dtCODE_K.Rows[0]["RITEM_HIGH1"].ToString() + " " + dtCODE_K.Rows[0]["RITEM_UNIT"].ToString();
            }
            //找有做檢查的人
            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE,A.RESULT_VALUE_T " +
                     "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                    " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                      "AND A.RESULT_VALUE_T IN ('阴性','阳性') " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtCODE1_K = db.Query(sSQL);
            string w_txtTOTAL9_K = dtCODE1_K.Rows.Count.ToString();

            sSQL = "SELECT B.pif_name, B.pif_ic, A.PAT_NO, A.RESULT_CODE, A.RESULT_VALUE_T " +
                     "FROM a_result_log A " +
                     "LEFT JOIN pat_info B " +
                       "ON A.PAT_NO=B.pif_id " +
                     " left join zinfo_a_07 f  on B.pif_id = f.pat_id and f.opt_1 in('','5') " +
                    "WHERE A.RESULT_VER=0 " +
                      "AND A.RESULT_CODE='" + sRESULT_CODE + "' " +
                      "AND A.RESULT_DATE>='" + sBEG_DATE + "' " +
                      "AND A.RESULT_DATE<='" + sEND_DATE + "' " +
                      "AND A.RESULT_VALUE_T='" + "阳性" + "' " +
                    "GROUP BY A.PAT_NO, A.RESULT_CODE, B.pif_name, B.pif_ic ";
            DataTable dtPOSITIVE_K = db.Query(sSQL);
            string w_txtPOSITIVE_K = dtPOSITIVE_K.Rows.Count.ToString();
            string w_txtPOSITIVE_KP;
            if (w_txtTOTAL4 == "0")
            {
                w_txtPOSITIVE_KP = "0";
            }
            else
            {
                w_txtPOSITIVE_KP = Percent(Convert.ToDouble(w_txtPOSITIVE_K) / Convert.ToDouble(w_txtTOTAL4) * 1000);
            }
            //


            sSQL = "delete from hospital_quality ";
            db.Excute(sSQL);
            sSQL = "";
            sSQL = "SELECT * FROM hospital_quality WHERE hq_date1='" + w_dates + "' ";
            sSQL += " and hq_date2='" + w_datee + "' ";
            DataTable dtT = db.Query(sSQL);
            if (dtT.Rows.Count == 0)
            {
                sql = " insert into  hospital_quality  ( hq_date1,hq_date2,hq_date3,hq_txt_10,hq_name,hq_num1,hq_d1,hq_d11,hq_d3,hq_d31,hq_d4,hq_d41,";
                sql += " hq_d5,hq_d51,hq_d6,hq_d61,hq_d7,hq_d71,hq_d8,hq_d81,hq_d9,hq_d91,hq_d10,hq_d11a,hq_d12,hq_d13,hq_d14) ";
                sql += " values ('" + w_dates + "','" + w_datee + "','" + w_hq_date3 + "','" + w_rpt_name + "','" + w_hqname + "','" + w_txtTOTAL4 + "','" + w_txtCHECK_NP_A + "','" + w_txtCHECK_YP_A + "','";
                sql += w_txtCHECK_NP + "','" + w_txtCHECK_YP + "','";
                sql += w_txtCHECK_NP_D + "','" + w_txtCHECK_YP_D + "','";
                sql += w_txtCHECK_NP_E + "','" + w_txtCHECK_YP_E + "','";
                sql += w_txtCHECK_NP_F + "','" + w_txtCHECK_YP_F + "','";
                sql += w_txtCHECK_NP_G + "','" + w_txtCHECK_YP_G + "','";
                sql += w_txtCHECK_NP_H + "','" + w_txtCHECK_YP_H + "','";
                sql += w_txtCHECK_NP_I + "','" + w_txtCHECK_YP_I + "','";
                sql += w_txtHOSP_P + "','" + w_txtDIE_P + "','";
                sql += w_txtHD_P_L + "','";
                sql += w_txtPOSITIVE_JP + "','" + w_txtPOSITIVE_KP + "')";
                db.Excute(sql);
            }

            sSQL = "";
            sSQL = "delete from hospital_quality_history where hq_date1='" + w_dates + "' ";
            sSQL += " and hq_date2='" + w_datee + "' ";
            db.Excute(sSQL);
            sql = "";
            sql = " insert into  hospital_quality_history  ( hq_date1,hq_date2,hq_date3,hq_txt_10,hq_name,hq_num1,hq_d1,hq_d11,hq_d3,hq_d31,hq_d4,hq_d41,";
            sql += " hq_d5,hq_d51,hq_d6,hq_d61,hq_d7,hq_d71,hq_d8,hq_d81,hq_d9,hq_d91,hq_d10,hq_d11a,hq_d12,hq_d13,hq_d14) ";
            sql += " values ('" + w_dates + "','" + w_datee + "','" + w_hq_date3 + "','" + w_rpt_name + "','" + w_hqname + "','" + w_txtTOTAL4 + "','" + w_txtCHECK_NP_A + "','" + w_txtCHECK_YP_A + "','";
            sql += w_txtCHECK_NP + "','" + w_txtCHECK_YP + "','";
            sql += w_txtCHECK_NP_D + "','" + w_txtCHECK_YP_D + "','";
            sql += w_txtCHECK_NP_E + "','" + w_txtCHECK_YP_E + "','";
            sql += w_txtCHECK_NP_F + "','" + w_txtCHECK_YP_F + "','";
            sql += w_txtCHECK_NP_G + "','" + w_txtCHECK_YP_G + "','";
            sql += w_txtCHECK_NP_H + "','" + w_txtCHECK_YP_H + "','";
            sql += w_txtCHECK_NP_I + "','" + w_txtCHECK_YP_I + "','";
            sql += w_txtHOSP_P + "','" + w_txtDIE_P + "','";
            sql += w_txtHD_P_L + "','";
            sql += w_txtPOSITIVE_JP + "','" + w_txtPOSITIVE_KP + "')";
            db.Excute(sql);
        }
        
        private string Percent(double dd)
        {
            string ss = dd.ToString("0.00");
            if (ss.Substring(ss.Length - 1, 1) == "0")
                ss = ss.Substring(0, ss.Length - 1);
            if (ss.Substring(ss.Length - 1, 1) == "0")
                ss = ss.Substring(0, ss.Length - 1);
            if (ss.Substring(ss.Length - 1, 1) == ".")
                ss = ss.Substring(0, ss.Length - 1);
            return ss;
        }
        
        #region 取得星期
        protected string GetWeek(string repDay)
        {
            DateTime date1 = DateTime.Parse(repDay);
            switch (date1.DayOfWeek)
            {
                case DayOfWeek.Monday:
                    return "1";
                case DayOfWeek.Tuesday:
                    return "2";
                case DayOfWeek.Wednesday:
                    return "3";
                case DayOfWeek.Thursday:
                    return "4";
                case DayOfWeek.Friday:
                    return "5";
                case DayOfWeek.Saturday:
                    return "6";
                case DayOfWeek.Sunday:
                    return "7";
            }
            return "1";
        }
        #endregion

        #region 列印藥品及材料領料單02
        protected void GeneratePickMaterialAndDrugReport02(ReportDocument rpt, string sINFO_DATE, string sql1, string sql2)
        {
            DataTable dtTotalMed;
            DataTable dtMedDetail;
            DataSet ds1 = new DataSet();
            string weekday = GetWeek(sINFO_DATE);
            try
            {
                DataSetDialysis.Report_TotalMedDataTable dtS02a = new DataSetDialysis.Report_TotalMedDataTable(); // 取藥品明細總數
                dtTotalMed = dtS02a;
                db.Fill(sql1, dtTotalMed);
                int i = 1;
                foreach (DataRow r in dtTotalMed.Rows)
                {
                    r["no"] = i.ToString();
                    i++;
                }
                ds1.Tables.Add(dtTotalMed);
                
                DataSetDialysis.Report_MedDetailDataTable dtS02b = new DataSetDialysis.Report_MedDetailDataTable(); // 取每個病患的藥品明細
                dtMedDetail = dtS02b;
                db.Fill(sql2, dtMedDetail);
                i = 1;
                foreach (DataRow r in dtMedDetail.Rows)
                {
                    r["no"] = i.ToString();
                    i++;
                }
                ds1.Tables.Add(dtMedDetail);
                
                // 取流水號及日期
                try
                {
                    string serialNo = dtMedDetail.Rows[0]["ivpl_serialno"].ToString();
                    string pickDate = dtMedDetail.Rows[0]["ivpl_date"].ToString();
                    rpt.DataDefinition.FormulaFields["SERIAL_NO"].Text = "ToText('" + serialNo + "')";
                    rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + pickDate + "')";
                }
                catch (Exception) 
                {
                    Common._ErrorMsgShow("没有内容!");
                }
                rpt.SetDataSource(ds1);
            }
            catch (DataSourceException ex)
            {
                string errorMsg = ex.Message.ToString();
            }
        }
        #endregion

        #region 列印藥品及材料領料單
        protected void GeneratePickMaterialAndDrugReport(ReportDocument rpt, string sINFO_DATE, string sql1, string sql2)
        {
            DataTable dtTotalMed;
            DataTable dtMedDetail;
            DataSet ds1 = new DataSet();
            string weekday = GetWeek(sINFO_DATE.Substring(0, 4) + "-" + sINFO_DATE.Substring(4, 2) + "-" + sINFO_DATE.Substring(6, 2));
            try
            {
                // 取藥品明細總數
                DataSetDialysis.Report_TotalMedDataTable dtS02a = new DataSetDialysis.Report_TotalMedDataTable();
                dtTotalMed = dtS02a;
                db.Fill(sql1, dtTotalMed);
                int i = 1;
                foreach (DataRow r in dtTotalMed.Rows)
                {
                    r["no"] = i.ToString();
                    i++;
                }
                ds1.Tables.Add(dtTotalMed);
                // 取每個病患的藥品明細
                DataSetDialysis.Report_MedDetailDataTable dtS02b = new DataSetDialysis.Report_MedDetailDataTable();
                dtMedDetail = dtS02b;
                db.Fill(sql2, dtMedDetail);
                i = 1;
                foreach (DataRow r in dtMedDetail.Rows)
                {
                    r["no"] = i.ToString();
                    i++;
                }
                ds1.Tables.Add(dtMedDetail);
                // 取流水號及日期
                string serialNo = sINFO_DATE;
                string pickDate = sINFO_DATE.Substring(0, 4) + "-" + sINFO_DATE.Substring(4, 2) + "-" + sINFO_DATE.Substring(6, 2);
                rpt.DataDefinition.FormulaFields["SERIAL_NO"].Text = "ToText('" + serialNo + "')";
                rpt.DataDefinition.FormulaFields["PICK_DATE"].Text = "ToText('" + pickDate + "')";

                rpt.SetDataSource(ds1);
            }
            catch (DataSourceException ex)
            {
                string errorMsg = ex.Message.ToString();
            }
        }
        #endregion

        #region 打印本周排班List Class
        public class PrintSchedule
        {
            public PrintSchedule(string floor, string area, string bedNo, string mactype, string name1, string name2, string name3, string name4, string name5, string name6, string name7, string machine, string timeType)
            {
                Area = area;
                BedNo = bedNo;
                Floor = floor;
                Machine = machine;
                MachineType = mactype;
                TimeType = timeType;
                Week1 = name1;
                Week2 = name2;
                Week3 = name3;
                Week4 = name4;
                Week5 = name5;
                Week6 = name6;
                Week7 = name7;
            }
                        
            public string Area { get; set; }
            public string BedNo { get; set; }
            public string Floor { get; set; }
            public string Machine { get; set; }
            public string MachineType { get; set; }
            public string TimeType { get; set; }
            public string Week1 { get; set; }
            public string Week2 { get; set; }
            public string Week3 { get; set; }
            public string Week4 { get; set; }
            public string Week5 { get; set; }
            public string Week6 { get; set; }
            public string Week7 { get; set; }
        }
        #endregion

        public class PrintTest
        {
            public PrintTest(string tDay, string item1, string item2, string item3, string item4, string item5, string item6)
            {
                TestDate = tDay;
                Item1 = item1;
                Item2 = item2;
                Item3 = item3;
                Item4 = item4;
                Item5 = item5;
                Item6 = item6;
            }

            public string TestDate { get; set; }
            public string Item1 { get; set; }
            public string Item2 { get; set; }
            public string Item3 { get; set; }
            public string Item4 { get; set; }
            public string Item5 { get; set; }
            public string Item6 { get; set; }
        }
    }
}