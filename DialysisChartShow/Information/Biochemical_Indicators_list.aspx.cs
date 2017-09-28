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

namespace Dialysis_Chart_Show.Information
{
    public partial class Biochemical_Indicators_list : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                _PAT_ID = Session["PAT_ID"].ToString();
                Show_Biochemical_Indicators();
            }

        }
        protected void Show_Biochemical_Indicators()
        {
            string sql;
            //sql = "SELECT RESULT_DATE ";
            //sql += "FROM a_result_log ";
            //sql += "WHERE PAT_NO='" + _PAT_ID + "' AND RESULT_VER=0 ";
            //sql += "ORDER BY RESULT_DATE DESC LIMIT 1 ";
            //DataTable dt1 = db.Query(sql);
            //if (dt1.Rows.Count > 0)
            //{
                sql = "SELECT DISTINCT A.RESULT_CODE, B.RITEM_NAME, A.RESULT_VALUE_T AS RESULT_VALUE, B.RITEM_LOW1, B.RITEM_HIGH1, A.RESULT_DATE ";
                sql += "FROM a_result_log A ";
                sql += "LEFT JOIN a_item_group C ON C.OITEM_CODE=A.RESULT_CODE AND C.GROUP_CODE IN ('G001','G003') ";
                sql += "LEFT JOIN a_ritem_setup B ON A.RESULT_CODE=B.RITEM_CODE ";
                sql += "WHERE A.RESULT_VER=0 ";
                //sql += "AND A.RESULT_DATE='" + dt1.Rows[0][0].ToString() + "' ";
                sql += "AND A.RESULT_CODE IN ('4003','4008','4021','4023','4027','4030','4050','5018') ";
                sql += "AND A.PAT_NO='" + _PAT_ID + "' ";
                sql += "ORDER BY C.GROUP_SN, A.RESULT_DATE DESC, B.RITEM_CODE ";
            //}
            //else
            //{
            //    sql = "SELECT A.RESULT_CODE, B.RITEM_NAME, A.RESULT_VALUE_T AS RESULT_VALUE, B.RITEM_LOW1, B.RITEM_HIGH1, B.RESULT_DATE ";
            //    sql += "FROM a_result_log A, a_ritem_setup B ";
            //    sql += "WHERE 1=0";
            //}
            DataTable dt = db.Query(sql);

            Store istore = Grid_Lab.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
            istore.DataBind();
        }

        protected void On_ShowChart(object sender, DirectEventArgs e)
        {
            string RESULT_CODE = e.ExtraParams["RESULT_CODE"];
            string RITEM_NAME = e.ExtraParams["RITEM_NAME"];
            string RITEM_LOW = e.ExtraParams["RITEM_LOW1"];
            string RITEM_HIGH = e.ExtraParams["RITEM_HIGH1"];
            string URL = "Lab_Abnormal_Chart.aspx?RESULT_CODE=" + RESULT_CODE + "&RITEM_NAME=" + RITEM_NAME + "&RITEM_LOW=" + RITEM_LOW + "&RITEM_HIGH=" + RITEM_HIGH;
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = URL;
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
            Window1.Show();
        }
    }
}