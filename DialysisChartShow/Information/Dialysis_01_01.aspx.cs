using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_01_01 : BaseForm
    {
        string _TableName ="zinfo_a_01";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
                _zInfo_Show(_TableName ,_PAT_ID,info_date.Text);
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
        }

        protected void Btn_Clear_Click(object sender, DirectEventArgs e)
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