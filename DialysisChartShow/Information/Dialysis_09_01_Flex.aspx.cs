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
    public partial class Dialysis_09_01_Flex : BaseForm
    {
        string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string PAT_ID = _Request("_PAT_ID").ToString();
                string INFO_DATE = _Request("_INFO_DATE").ToString();
                string KIND = _Request("_KIND").ToString();

                string sql = "SELECT a.pif_id ,a.pif_name, a.pif_ic, b.* FROM pat_info a ";
                sql += "LEFT JOIN clinical1_nurse b ON a.pif_ic=b.cln1_patic ";
                sql += "WHERE a.pif_ic = '" + PAT_ID + "' AND b.cln1_diadate = '" + INFO_DATE + "' ";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
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
                    if (KIND == "3")
                    {
                        switch (sProcessOfPurifyingTheBlood)
                        {
                            case "Hospital_Suzhou":
                                sPage += "Dialysis_09_01_SuzhouS.aspx";
                                break;
                            case "Hospital_117":
                                sPage += "Dialysis_09_01_117S.aspx";
                                break;
                            case "Hospital_Xian":
                                sPage += "Dialysis_09_01_XianS.aspx";
                                break;
                            case "Hospital_Henan":
                                sPage += "Dialysis_09_01_HenanS.aspx";
                                break;
                            case "Standard":
                                sPage += "Dialysis_09_01_StandardS.aspx";
                                break;
                            default:
                                sPage += "Dialysis_09_01_StandardS.aspx";
                                break;
                        }
                    }
                    else
                    {
                        switch (sProcessOfPurifyingTheBlood)
                        {
                            case "Hospital_Suzhou":
                                sPage += "Dialysis_09_01_Suzhou.aspx";
                                break;
                            case "Hospital_117":
                                sPage += "Dialysis_09_01_117.aspx";
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
                    paraStr += "&date=" + INFO_DATE;
                    X.Redirect(sPage + paraStr);
                }
                else
                    Common._NotificationShow("找不到血液净化资料");
            }
            
        }
    }
}