using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;
using Dialysis_Chart_Show.tools;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Xml.Xsl;
using System.Xml;
using System.Net;
using System.IO;
using System.Text;

namespace Dialysis_Chart_Show.Information
{

    public partial class Dialysis_13_new : BaseForm  //System.Web.UI.Page
    {
        private void SetNode(Ext.Net.Node nNode, string sText, string sDataPath, string sNodeID, Ext.Net.Node nFather)
        {
            nNode.Text = sText;
            nNode.DataPath = sDataPath;
            nNode.NodeID = sNodeID;
            nNode.Cls = "large-font";
            nFather.Children.Add(nNode);
        }

        private void AddNode(Ext.Net.Node nf, string sText, string sDataPath, string sNodeID)
        {
            Ext.Net.Node rN = new Ext.Net.Node()
              {
                  Text = sText,
                  DataPath = sDataPath,
                  NodeID = sNodeID,
                  Icon = Icon.Report,
                  Cls = "large-font",
                  Leaf = true
              };
            nf.Children.Add(rN);
        }

        private string PAT_IC = "";
        private string PAT_NAME = "";
        private string USER_ID = "";
        private string USER_LEVEL;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //PAT_IC = _Request("PAT_IC");
                //PAT_NAME = _Request("PAT_NAME");
                //USER_ID = _Request("USER_ID");
                //USER_LEVEL = _Request("USER_TYPE");
                PAT_IC = Session["PAT_IC"].ToString();
                PAT_NAME = Session["PAT_NAME"].ToString();
                USER_ID = Session["USER_ID"].ToString();
                USER_LEVEL = Session["USER_RIGHT"].ToString();
                ShowMaterial();
            }
        }

        protected void ShowMaterial()
        {
            //耗材使用数量
            DateTime dBEG = DateTime.Now;
            DateTime dEND = DateTime.Now;
            dBEG = Convert.ToDateTime(dBEG.ToString("yyyy-MM-") + "01");
            string sBEG_DATE = dBEG.ToString("yyyy-MM-dd");
            dEND = dBEG;
            dEND = dEND.AddMonths(1);
            dEND = dEND.AddDays(-1);
            string sEND_DATE = dEND.ToString("yyyy-MM-dd");
            string sSQL = "";
            this.GridPanel18.Hidden = true;
            Lab_amount.Text = "";
            this.Column156.Hidden = true;

            sSQL = "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, COUNT(a.pv_datevisit) AS USE_CNT, D.hp2_name AS ITEM_NAME, COUNT(D.hp2_name) AS ITEM_CNT, E.price*COUNT(d.hp2_name) AS ITEM_SUM   " +
                      "FROM pat_visit A  " +
                       "LEFT JOIN pat_info C ON A.pv_ic=C.pif_ic  " +
                       "LEFT JOIN hpack2_setup D ON A.pv_hpack2=D.hp2_code  " + //材料
                       "LEFT JOIN inventory_stock E ON E.invs_ctg=D.hp2_code  " + //庫存材料
                      "WHERE A.pv_datevisit>='" + sBEG_DATE + "' AND A.pv_datevisit<='" + sEND_DATE + "' AND A.pv_ic='" + PAT_IC + "' AND D.hp2_name IS NOT NULL  " +
                      "GROUP BY D.hp2_name  " +
                     "UNION ALL  " +
                     "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, COUNT(a.pv_datevisit) AS USE_CNT, D.hp3_name AS ITEM_NAME, COUNT(D.hp3_name) AS ITEM_CNT, E.price*COUNT(D.hp3_name) AS ITEM_SUM  " +
                      "FROM pat_visit A  " +
                       "LEFT JOIN pat_info C ON A.pv_ic=C.pif_ic  " +
                       "LEFT JOIN hpack3_setup D ON A.pv_hpack3=D.hp3_code  " + //材料
                       "LEFT JOIN inventory_stock E ON E.invs_ctg=D.hp3_code  " + //庫存材料
                      "WHERE A.pv_datevisit>='" + sBEG_DATE + "' AND A.pv_datevisit<='" + sEND_DATE + "' AND A.pv_ic='" + PAT_IC + "' AND D.hp3_name IS NOT NULL  " +
                      "GROUP BY D.hp3_name  " +
                     "UNION ALL  " +
                     "SELECT DISTINCT A.pv_ic AS PERSON_IC, C.pif_name AS PESRON_NAME, COUNT(a.pv_datevisit) AS USE_CNT, D.pdet_itemnm AS ITEM_NAME, COUNT(D.pdet_itemnm) AS ITEM_CNT, E.price*COUNT(D.pdet_itemnm)*pdet_qty AS ITEM_SUM   " +
                      "FROM pat_visit A  " +
                       "LEFT JOIN pat_info C ON A.pv_ic=C.pif_ic  " +
                       "LEFT JOIN package_detail2 D ON C.pif_hpack=D.pdet_code AND D.pdet_itemcd not IN ('0001','0002')  " +  //穿刺針護理包
                       "LEFT JOIN inventory_stock E ON E.invs_ctg=D.pdet_itemcd  " +  //庫存材料
                      "WHERE A.pv_datevisit>='" + sBEG_DATE + "' AND A.pv_datevisit<='" + sEND_DATE + "' AND A.pv_ic='" + PAT_IC + "' AND D.pdet_itemnm IS NOT NULL  " +
                      "GROUP BY D.pdet_itemnm  " +
                     "UNION ALL  " +
                     "SELECT DISTINCT a.pv_ic AS PERSON_IC, c.pif_name AS PESRON_NAME, COUNT(a.pv_datevisit) AS USE_CNT, d.drg_name AS ITEM_NAME, COUNT(d.drg_name) AS ITEM_CNT, D.price*COUNT(d.drg_name) AS ITEM_SUM  " +
                      "FROM pat_visit AS a  " +
                       "JOIN shortterm_ordermgt AS b ON a.pv_ic = b.shord_patic AND a.pv_datevisit >= b.shord_dateord AND (b.shord_actst = '00001' OR (b.shord_actst = '00002' AND a.pv_datevisit < b.shord_dtactst))  " +
                       "LEFT JOIN pat_info AS c ON a.pv_ic = c.pif_ic  " +
                       "LEFT JOIN drug_list AS d ON b.shord_drug = d.drg_code " + //藥品
                      "where a.pv_ic='" + PAT_IC + "' and a.pv_datevisit>='" + sBEG_DATE + "' and a.pv_datevisit<='" + sEND_DATE + "'  " +
                      "GROUP BY d.drg_name  " +
                     "UNION ALL " +
                     "SELECT DISTINCT a.pv_ic AS PERSON_IC, c.pif_name AS PESRON_NAME, COUNT(a.pv_datevisit) AS USE_CNT, d.drg_name AS ITEM_NAME, COUNT(d.drg_name) AS ITEM_CNT, d.price*COUNT(d.drg_name) AS ITEM_SUM    " +
                      "FROM pat_visit AS a  " +
                       "JOIN longterm_ordermgt AS b ON a.pv_ic = b.lgord_patic AND a.pv_datevisit >= b.lgord_dateord AND (b.lgord_actst = '00001' OR (b.lgord_actst = '00002' AND a.pv_datevisit < b.lgord_actst))  " +
                       "LEFT JOIN pat_info AS c ON a.pv_ic = c.pif_ic  " +
                       "LEFT JOIN drug_list AS d ON b.lgord_drug = d.drg_code  " + //藥品
                      "where a.pv_ic='" + PAT_IC + "' and a.pv_datevisit>='" + sBEG_DATE + "' and a.pv_datevisit<='" + sEND_DATE + "'  " +
                      "GROUP BY d.drg_name";

            DataTable dt18 = db.Query(sSQL);
            Store istore18 = this.GridPanel18.GetStore();
            double dd = 0;
            if (dt18.Rows.Count > 0)
            {
                for (int n = 0; n < dt18.Rows.Count; n++)
                {
                    try
                    {
                        dd = dd + Convert.ToDouble(dt18.Rows[n]["ITEM_SUM"].ToString());
                    }
                    catch
                    {

                    };
                };
            };

            istore18.DataSource = db.GetDataArray_AddRowNum(dt18);
            Lab_patid.Text = "姓名 : " + PAT_NAME;
            if (USER_LEVEL == "AD" || USER_LEVEL == "HN" || USER_LEVEL == "DC" || USER_LEVEL == "DH")
            {
                Lab_amount.Text = "總計金額：" + Convert.ToString(dd);
                this.Column156.Hidden = false;
            }
            istore18.DataBind();
            this.GridPanel18.Hidden = false;
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

        protected void ToExcel_18(object sender, EventArgs e)
        {
            string json = this.Hidden18.Value.ToString();
            StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
            XmlNode xml = eSubmit.Xml;
            this.Response.Clear();
            this.Response.ContentType = "application/vnd.ms-excel";
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".xls");
            XslCompiledTransform xtExcel = new XslCompiledTransform();
            xtExcel.Load(Server.MapPath("Excel.xsl"));
            xtExcel.Transform(xml, null, this.Response.OutputStream);
            this.Response.End();
        }

        protected void ToCsv_18(object sender, EventArgs e)
        {
            string json = this.Hidden18.Value.ToString();
            StoreSubmitDataEventArgs eSubmit = new StoreSubmitDataEventArgs(json, null);
            XmlNode xml = eSubmit.Xml;
            this.Response.Clear();
            this.Response.ContentType = "application/octet-stream";
            this.Response.AddHeader("Content-Disposition", "attachment; filename=" + System.DateTime.Now.ToString("yyyyMMdd_HHmmss") + ".csv");
            XslCompiledTransform xtCsv = new XslCompiledTransform();
            xtCsv.Load(Server.MapPath("Csv.xsl"));
            xtCsv.Transform(xml, null, this.Response.OutputStream);
            this.Response.End();
        }
    }
}