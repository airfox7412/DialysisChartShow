using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Lab_Abnormal_Chart : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string RESULT_CODE = Request.QueryString["RESULT_CODE"];
                string RITEM_NAME = Request.QueryString["RITEM_NAME"];
                string RITEM_LOW = Request.QueryString["RITEM_LOW"];
                string RITEM_HIGHT = Request.QueryString["RITEM_HIGH"];

                TextSprite TextSprite1 = new TextSprite(); 
                TextSprite1.Text = RITEM_NAME + "趋势图";
                TextSprite1.FontSize = "22";
                TextSprite1.Width = 100;
                TextSprite1.Height = 30;
                TextSprite1.X = 40;
                TextSprite1.Y = 20;
                Chart1.Items.Add(TextSprite1);

                var jsons = new List<object>();

                string sSQL;
                sSQL = "SELECT RESULT_DATE, RESULT_VALUE_N FROM a_result_log ";
                sSQL += "WHERE RESULT_VER=0 AND PAT_NO=" + _PAT_ID + " AND RESULT_CODE='" + RESULT_CODE + "' ";
                sSQL += "ORDER BY RESULT_DATE DESC";
                DataTable dt = db.Query(sSQL);
                foreach (DataRow irow in dt.Rows)
                {
                    jsons.Add(new
                    {
                        month = irow["RESULT_DATE"].ToString(), //日期
                        value = double.Parse(irow["RESULT_VALUE_N"].ToString()),
                        value_l = double.Parse(RITEM_LOW),
                        value_h = double.Parse(RITEM_HIGHT)
                    });
                }
                this.Chart1.GetStore().DataSource = jsons;
            }
        }
    }
}