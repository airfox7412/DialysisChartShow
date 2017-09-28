using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;

namespace Dialysis_Chart_Show.Ipad
{
    public partial class Dialysis_0h_01 : BaseForm
    {
        private string _TableName = "zinfo_h_01";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request.QueryString["pid"];
                _PAT_ID = patient_id.Text;
                floor.Text = Request["floor"].ToString();
                area.Text = Request["area"].ToString();
                time.Text = Request["time"].ToString();
                bedno.Text = Request["bedno"].ToString();
                daytyp.Text = Request["daytyp"].ToString();

                _zInfo_Show(_TableName, _PAT_ID, info_date.Text);
                txt_1.Text = Session["USER_NAME"].ToString();
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

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
            Btn_Close_Click(sender, e);
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("../ipad_EvaForm.aspx?pid=" + patient_id.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }
    }
}