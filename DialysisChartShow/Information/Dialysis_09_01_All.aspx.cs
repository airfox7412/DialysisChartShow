using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.Web.Hosting;
using System.IO;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_01_All : BaseForm
    {
        string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                _PAT_ID = Session["PAT_ID"].ToString();
                //_PAT_IC = Session["PAT_IC"].ToString();
                show_grid();
            }
            
        }

        protected void show_grid()
        {
            string sql = "SELECT a.cln1_diadate, a.cln1_col1, a.cln1_col34, a.cln1_col5, a.cln1_col6, ";
            sql += "a.cln1_col7, a.cln1_col8, a.cln1_col13, a.cln1_col14, a.cln1_col15 ";
            sql += "FROM clinical1_nurse a ";
            sql += " WHERE a.cln1_patic = '" + _PAT_IC + "' ";
            sql += "ORDER BY a.cln1_diadate DESC";
            DataTable dt = db.Query(sql);

            Store istore = Grid_clinical1_nurse.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();

        }

        protected void RowSelect(object sender, DirectEventArgs e)
        {
            //Grid_clinical1_nurse.Hidden = true;
            string json = e.ExtraParams["Values"];
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);

            string sql = "SELECT a.pif_id ,a.pif_name, a.pif_ic, b.* FROM pat_info a ";
            sql += "LEFT JOIN clinical1_nurse b ON a.pif_ic=b.cln1_patic ";
            sql += "WHERE a.pif_id = '" + _PAT_ID + "' AND b.cln1_diadate = '" + selRow[0]["info_date"].ToString() + "' ";
            DataTable dt = db.Query(sql);

            string patient_ic = dt.Rows[0]["pif_ic"].ToString();
            string patient_name = dt.Rows[0]["pif_name"].ToString();
            string machine_type = dt.Rows[0]["cln1_col26"].ToString();
            string hpack = dt.Rows[0]["cln1_col4"].ToString();
            string hpack3 = "";
            string mechine_model = "";
            string patient_weight = dt.Rows[0]["cln1_col5"].ToString();
            string bedno = "";
            string floor = "";
            string area = "";
            string time = "";
            string daytyp = "";

            string sPage = "";
            switch (Hospital)
            {
                case "Hospital_Suzhou":
                    sPage += "Dialysis_09_01_Suzhou.aspx";
                    break;
                case "Hospital_117":
                    sPage += "Dialysis_09_01_Henan.aspx"; //"Dialysis_09_01_117.aspx";
                    break;
                case "Hospital_Xian":
                    sPage += "Dialysis_09_01_Xian.aspx";
                    break;
                case "Hospital_Henan":
                    sPage += "Dialysis_09_01_Henan.aspx";
                    break;
                case "Standard":
                    sPage += "Dialysis_09_01_Standard.aspx";
                    break;
                default:
                    sPage += "Dialysis_09_01_Standard.aspx";
                    break;
            }
            string paraStr = "?patient_id=" + patient_ic;
            paraStr += "&patient_name=" + patient_name;
            paraStr += "&machine_type=" + machine_type;
            paraStr += "&hpack=" + hpack;
            paraStr += "&hpack3=" + hpack3;
            paraStr += "&mechine_model=" + mechine_model;
            paraStr += "&patient_weight=" + patient_weight;
            paraStr += "&bedno=" + bedno;
            paraStr += "&floor=" + floor;
            paraStr += "&area=" + area;
            paraStr += "&time=" + time;
            paraStr += "&daytyp=" + daytyp;
            paraStr += "&page=2";
            paraStr += "&date=" + selRow[0]["info_date"].ToString();
            X.Redirect(sPage + paraStr);
        }

        protected void OnRunEdit1(object sender, DirectEventArgs e)
        {
            string info_date = e.ExtraParams["info_date"].ToString();

            string sql = "SELECT a.pif_id ,a.pif_name, a.pif_ic, b.* FROM pat_info a ";
            sql += "LEFT JOIN clinical1_nurse b ON a.pif_ic=b.cln1_patic ";
            sql += "WHERE a.pif_id = '" + _PAT_ID + "' AND b.cln1_diadate = '" + info_date + "' ";
            DataTable dt = db.Query(sql);

            string patient_ic = dt.Rows[0]["pif_ic"].ToString();
            string patient_name = dt.Rows[0]["pif_name"].ToString();
            string machine_type = dt.Rows[0]["cln1_col26"].ToString();
            string hpack = dt.Rows[0]["cln1_col4"].ToString();
            string hpack3 = "";
            string mechine_model = "";
            string patient_weight = dt.Rows[0]["cln1_col5"].ToString();
            string bedno = "";
            string floor = "";
            string area = "";
            string time = "";
            string daytyp = "";

            string sPage = "";
            string paraStr = "";

            switch (Hospital)
            {
                case "Hospital_Suzhou":
                    sPage += "Dialysis_09_01_Suzhou.aspx";
                    break;
                case "Hospital_117":
                    sPage += "Dialysis_09_01_Henan.aspx"; //"Dialysis_09_01_117.aspx";
                    break;
                case "Hospital_Xian":
                    sPage += "Dialysis_09_01_Xian.aspx";
                    break;
                case "Hospital_Henan":
                    sPage += "Dialysis_09_01_Henan.aspx";
                    break;
                case "Standard":
                    sPage += "Dialysis_09_01_Standard.aspx";
                    break;
                default:
                    sPage += "Dialysis_09_01_Standard.aspx";
                    break;
            }
            paraStr = "?patient_id=" + patient_ic;
            paraStr += "&patient_name=" + patient_name;
            paraStr += "&machine_type=" + machine_type;
            paraStr += "&hpack=" + hpack;
            paraStr += "&hpack3=" + hpack3;
            paraStr += "&mechine_model=" + mechine_model;
            paraStr += "&patient_weight=" + patient_weight;
            paraStr += "&bedno=" + bedno;
            paraStr += "&floor=" + floor;
            paraStr += "&area=" + area;
            paraStr += "&time=" + time;
            paraStr += "&daytyp=" + daytyp;
            paraStr += "&page=2";
            paraStr += "&date=" + info_date;
            X.Redirect(sPage + paraStr);
        }

        protected void OnRunEdit2(object sender, DirectEventArgs e)
        {
            string info_date = e.ExtraParams["info_date"].ToString();

            string sql = "SELECT a.pif_id ,a.pif_name, a.pif_ic, b.* FROM pat_info a ";
            sql += "LEFT JOIN clinical1_nurse b ON a.pif_ic=b.cln1_patic ";
            sql += "WHERE a.pif_id = '" + _PAT_ID + "' AND b.cln1_diadate = '" + info_date + "' ";
            DataTable dt = db.Query(sql);

            string patient_ic = dt.Rows[0]["pif_ic"].ToString();
            string patient_name = dt.Rows[0]["pif_name"].ToString();
            string machine_type = dt.Rows[0]["cln1_col26"].ToString();
            string hpack = dt.Rows[0]["cln1_col4"].ToString();
            string hpack3 = "";
            string mechine_model = "";
            string patient_weight = dt.Rows[0]["cln1_col5"].ToString();
            string bedno = "";
            string floor = "";
            string area = "";
            string time = "";
            string daytyp = "";

            string sPage = "";
            string paraStr = "";

            switch (Hospital)
            {
                case "Hospital_Suzhou":
                    sPage += "Dialysis_09_01_Suzhou.aspx";
                    break;
                case "Hospital_117":
                    sPage += "Dialysis_09_01_HenanS.aspx"; //"Dialysis_09_01_117.aspx";
                    break;
                case "Hospital_Xian":
                    sPage += "Dialysis_09_01_Xian.aspx";
                    break;
                case "Hospital_Henan":
                    sPage += "Dialysis_09_01_HenanS.aspx";
                    break;
                case "Standard":
                    sPage += "Dialysis_09_01_Standard.aspx";
                    break;
                default:
                    sPage += "Dialysis_09_01_Standard.aspx";
                    break;
            }
            paraStr = "?patient_id=" + patient_ic;
            paraStr += "&patient_name=" + patient_name;
            paraStr += "&machine_type=" + machine_type;
            paraStr += "&hpack=" + hpack;
            paraStr += "&hpack3=" + hpack3;
            paraStr += "&mechine_model=" + mechine_model;
            paraStr += "&patient_weight=" + patient_weight;
            paraStr += "&bedno=" + bedno;
            paraStr += "&floor=" + floor;
            paraStr += "&area=" + area;
            paraStr += "&time=" + time;
            paraStr += "&daytyp=" + daytyp;
            paraStr += "&page=2";
            paraStr += "&date=" + info_date;
            X.Redirect(sPage + paraStr);
        }
    }
}