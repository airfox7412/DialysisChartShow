using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_06_02 : BaseForm
    {
        private string _TableName = "zinfo_f_02";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                _zInfo_Show(_TableName ,_PAT_ID,info_date.Text);
                if (opt_13_2.Checked == true)
                {
                    opt_14_1.Hidden = false;
                    opt_14_2.Hidden = false;
                    opt_14_3.Hidden = false;
                }
                if (opt_16_2.Checked == true)
                {
                    opt_17_1.Hidden = false;
                    opt_17_2.Hidden = false;
                    opt_17_3.Hidden = false;
                }
                if (opt_18_2.Checked == true)
                {
                    opt_19_1.Hidden = false;
                    opt_19_2.Hidden = false;
                    opt_19_3.Hidden = false;
                    txt_19.Hidden = false;
                }
                if (opt_21_2.Checked == true)
                {
                    txt_22.Hidden = false;
                }
                if (opt_26_2.Checked == true)
                {
                    txt_27.Hidden = false;
                }
                if (opt_32_2.Checked == true)
                {
                    txt_33.Hidden = false;
                }
                if (opt_36_2.Checked == true)
                {
                    txt_37.Hidden = false;
                }
                if (opt_46_2.Checked == true)
                {
                    txt_47.Hidden = false;
                }
                if (opt_48_2.Checked == true)
                {
                    txt_49.Hidden = false;
                }
                if (opt_50_2.Checked == true)
                {
                    txt_51.Hidden = false;
                }
                if (opt_52_2.Checked == true)
                {
                    txt_53.Hidden = false;
                }
                if (opt_1_2.Checked == true)
                {
                    opt_15_1.Hidden = false;
                    opt_15_2.Hidden = false;
                    opt_15_3.Hidden = false;
                    opt_15_4.Hidden = false;
                    opt_15_5.Hidden = false;
                }
                if (opt_2_2.Checked == true)
                {
                    chk_44_1.Hidden = false;
                    chk_44_2.Hidden = false;
                    chk_44_3.Hidden = false;
                    chk_44_4.Hidden = false;
                    txt_45.Hidden = false;
                }
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
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
        protected void aa(object sender, DirectEventArgs e)
        {
            if (opt_13_2.Checked == true)
            {
                opt_14_1.Hidden = false;
                opt_14_2.Hidden = false;
                opt_14_3.Hidden = false;
            }
            else
            {
                opt_14_1.Checked = false;
                opt_14_2.Checked = false;
                opt_14_3.Checked = false;
                opt_14_1.Hidden = true;
                opt_14_2.Hidden = true;
                opt_14_3.Hidden = true;
                
            }
        }
        protected void bb(object sender, DirectEventArgs e)
        {
            if (opt_1_2.Checked == true)
            {
                opt_15_1.Hidden = false;
                opt_15_2.Hidden = false;
                opt_15_3.Hidden = false;
                opt_15_4.Hidden = false;
                opt_15_5.Hidden = false;
            }
            else
            {
                opt_15_1.Checked = false;
                opt_15_2.Checked = false;
                opt_15_3.Checked = false;
                opt_15_4.Checked = false;
                opt_15_5.Checked = false;
                opt_15_1.Hidden = true;
                opt_15_2.Hidden = true;
                opt_15_3.Hidden = true;
                opt_15_4.Hidden = true;
                opt_15_5.Hidden = true;
                
                
            }
        }
        protected void cc(object sender, DirectEventArgs e)
        {
            if (opt_16_2.Checked == true)
            {
                opt_17_1.Hidden = false;
                opt_17_2.Hidden = false;
                opt_17_3.Hidden = false;
            }
            else
            {
                opt_17_1.Checked = false;
                opt_17_2.Checked = false;
                opt_17_3.Checked = false;
                opt_17_1.Hidden = true;
                opt_17_2.Hidden = true;
                opt_17_3.Hidden = true;


            }
        }
        protected void dd(object sender, DirectEventArgs e)
        {
            if (opt_18_2.Checked == true)
            {
                opt_19_1.Hidden = false;
                opt_19_2.Hidden = false;
                opt_19_3.Hidden = false;
                txt_19.Hidden = false;
            }
            else
            {
                opt_19_1.Checked = false;
                opt_19_2.Checked = false;
                opt_19_3.Checked = false;
                txt_19.Text = "";
                opt_19_1.Hidden = true;
                opt_19_2.Hidden = true;
                opt_19_3.Hidden = true;
                txt_19.Hidden = true;

            }
        }
        protected void ee(object sender, DirectEventArgs e)
        {
            if (opt_21_2.Checked == true)
            {
                txt_22.Hidden = false;
            }
            else
            {
                txt_22.Text = "";
                txt_22.Hidden = true;
            }
        }
        protected void ff(object sender, DirectEventArgs e)
        {
            if (opt_26_2.Checked == true)
            {
                txt_27.Hidden = false;
            }
            else
            {
                txt_27.Text = "";
                txt_27.Hidden = true;
            }
        }
        protected void gg(object sender, DirectEventArgs e)
        {
            if (opt_32_2.Checked == true)
            {
                txt_33.Hidden = false;
            }
            else
            {
                txt_33.Text = "";
                txt_33.Hidden = true;
            }
        }
        protected void hh(object sender, DirectEventArgs e)
        {
            if (opt_36_2.Checked == true)
            {
                txt_37.Hidden = false;
            }
            else
            {
                txt_37.Text = "";
                txt_37.Hidden = true;
            }
        }
        protected void ii(object sender, DirectEventArgs e)
        {
            if (opt_2_2.Checked == true)
            {
                chk_44_1.Hidden = false;
                chk_44_2.Hidden = false;
                chk_44_3.Hidden = false;
                chk_44_4.Hidden = false;
                txt_45.Hidden = false;
            }
            else
            {
                chk_44_1.Checked = false;
                chk_44_2.Checked = false;
                chk_44_3.Checked = false;
                chk_44_4.Checked = false;
                txt_45.Text = "";
                chk_44_1.Hidden = true;
                chk_44_2.Hidden = true;
                chk_44_3.Hidden = true;
                chk_44_4.Hidden = true;
                txt_45.Hidden = true;
            }
        }
        protected void jj(object sender, DirectEventArgs e)
        {
            if (opt_46_2.Checked == true)
            {
                txt_47.Hidden = false;
            }
            else
            {
                txt_47.Text = "";
                txt_47.Hidden = true;
            }
        }
        protected void kk(object sender, DirectEventArgs e)
        {
            if (opt_48_2.Checked == true)
            {
                txt_49.Hidden = false;
            }
            else
            {
                txt_49.Text = "";
                txt_49.Hidden = true;
            }
        }
        protected void ll(object sender, DirectEventArgs e)
        {
            if (opt_50_2.Checked == true)
            {
                txt_51.Hidden = false;
            }
            else
            {
                txt_51.Text = "";
                txt_51.Hidden = true;
            }
        }
        protected void mm(object sender, DirectEventArgs e)
        {
            if (opt_52_2.Checked == true)
            {
                txt_53.Hidden = false;
            }
            else
            {
                txt_53.Text = "";
                txt_53.Hidden = true;
            }
        }
    }
}