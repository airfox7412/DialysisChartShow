using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_06_06_Alasamo : BaseForm
    {
        private string _TableName = "zinfo_f_06_alasamo";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                sel_date.Text = Request.QueryString["sel_date"];
                Show_zinfo_f06();
                if (txt_3.Text == "")
                {
                    string sSQL = "SELECT a.*, c.txt_7 AS t7, d.info_date AS InDate FROM clinical1_doc_henan a ";
                    sSQL += "LEFT JOIN pat_info b ON a.cln1_patic=b.pif_ic ";
                    sSQL += "LEFT JOIN zinfo_f_011 c ON c.pat_id=b.pif_id ";
                    sSQL += "LEFT JOIN zinfo_f_012_alasamo d ON d.pat_id=b.pif_id ";
                    sSQL += "WHERE a.cln1_patic='" + _PAT_IC + "' AND a.cln1_diadate='base'";
                    DataTable dt = db.Query(sSQL);
                    if (dt.Rows.Count > 0)
                    {
                        dat_1.Text = dt.Rows[0]["InDate"].ToString() + " 12:00";
                        txt_2.Text = dt.Rows[0]["t7"].ToString();
                        txt_3.Text = dt.Rows[0]["cln1_col1"].ToString();
                        txt_4.Text = dt.Rows[0]["cln1_col5"].ToString();
                        txt_6.Text = dt.Rows[0]["cln1_col4"].ToString();
                        txt_5.Text = dt.Rows[0]["cln1_col2"].ToString();
                    }
                }
            }
        }

        protected void Show_zinfo_f06()
        {
            _zInfo_Show(_TableName, _PAT_ID, sel_date.Text);
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
        }

        protected void OnbtnPrint_Click(object sender, DirectEventArgs e)
        {
            PrintWindow.Show();
            PrintWindow.Loader.SuspendScripting();
            PrintWindow.Loader.Url = "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + sel_date.Text + "&_REPORT_NAME=f6a";
            PrintWindow.Loader.DisableCaching = true;
            PrintWindow.LoadContent();
        }

        protected void btn_back_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Dialysis_06_06_Alasamo_List.aspx");
        }
    }
}