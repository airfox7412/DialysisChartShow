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
    public partial class Dialysis_06_04 : BaseForm
    {
        private string _TableName = "zinfo_f_04";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            string info_date = "";
            info_date = DateTime.Now.ToString("yyyy-MM-dd");
            _zInfo_Save(_TableName, _PAT_ID, info_date);
            //X.Redirect("Dialysis_06_02.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }
    }
}