using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_0h_01 : BaseForm
    {
        private string _TableName = "zinfo_h_01";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                _zInfo_Show(_TableName, _PAT_ID, info_date.Text);
                if (opt_26_2.Checked == true)
                    txt_27.Hidden = false;
                if (opt_26_3.Checked == true)
                    txt_28.Hidden = false;
                if (opt_22_4.Checked == true)
                    txt_23.Hidden = false;
                if (opt_56_5.Checked == true)
                    txt_57.Hidden = false;
            }
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);

        }

        protected void Btn_Print_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_REPORT_NAME=1");
        }
        
        protected void Btn_Clear_Click(object sender, DirectEventArgs e)
        {
            _ClearForm();
        }

        //protected void Btn_Close_Click(object sander, DirectEventArgs e)
        //{
        //    X.Redirect("Dialysis_0h_01.aspx?editmode=list");
        //}

        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }


        protected void hh(object sender, DirectEventArgs e)
        {
            if (opt_26_2.Checked == true)
            {
                txt_27.Hidden = false;
            }
            else
            {
                //txt_27.Text = "";
                txt_27.Hidden = true;
            }
        }

        protected void ii(object sender, DirectEventArgs e)
        {
            if (opt_26_3.Checked == true)
            {
                txt_28.Hidden = false;
            }
            else
            {
                txt_28.Hidden = true;
            }
        }

        protected void jj(object sender, DirectEventArgs e)
        {
            if (opt_22_4.Checked == true)
            {
                txt_23.Hidden = false;
            }
            else
            {
                txt_23.Hidden = true;
            }
        }

        protected void kk(object sender, DirectEventArgs e)
        {
            if (opt_56_5.Checked == true)
            {
                txt_57.Hidden = false;
            }
            else
            {
                txt_57.Hidden = true;
            }
        }

    }
}