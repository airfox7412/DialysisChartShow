﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Collections;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_0h_08_Edit : BaseForm
    {
        private string _TableName = "zinfo_h_08";
        private DateTime _dateTime = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                infodate.Text = Request.QueryString["info_date"];
                Show();
                SelectChangeTextState(opt_2_2, opt_2_3, txt_2);
                SelectChangeTextState(opt_3_2, opt_3_3, txt_3);
                SelectChangeTextState(opt_5_3, txt_5);
                SelectChangeTextState(opt_12_2, txt_12);
                SelectChangeTextState(opt_13_2, txt_13b);
                SelectChangeTextState(opt_14_2, txt_14);
                SelectChangeTextState(opt_15_4, txt_15);
                SelectChangeTextState(opt_16_6, txt_16);
                SelectChangeTextState(opt_17_2, opt_17_3, txt_17);
                SelectChangeTextState(opt_19_2, txt_19a);
                SelectChangeTextState(opt_19_2, txt_19b);
                SelectChangeTextState(opt_19_2, txt_19c);
                SelectChangeTextState(opt_21_2, txt_21);
                SelectChangeTextState(opt_22_3, opt_22_4, Label22a);
                SelectChangeTextState(opt_22_3, opt_22_4, opt_22a_1);
                SelectChangeTextState(opt_22_3, opt_22_4, opt_22a_2);
                SelectChangeTextState(opt_23_6, txt_23);
                SelectChangeTextState(opt_26_2, txt_26);
                SelectChangeTextState(opt_30_2, txt_30);
                SelectChangeTextState(opt_37_6, txt_37);
            }
        }

        protected void Show()
        {
            info_date.Text = infodate.Text;
            _zInfo_Show(_TableName, _PAT_ID, infodate.Text);
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
            X.Redirect("Dialysis_0h_08_List.aspx");
        }

        protected void Btn_Print_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + infodate.Text + "&_REPORT_NAME=99");
        }

        protected void Btn_Close_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Dialysis_0h_08_List.aspx");
        }

        protected void Btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }

        //protected void Btn_Close_Click(object sander, DirectEventArgs e)
        //{
        //    X.Redirect("Dialysis_0h_08.aspx?editmode=list");
        //}

        protected void SelectChangeTextState(Ext.Net.Radio radio, Ext.Net.TextField text)
        {
            if (radio.Checked == true)
                text.Hidden = false;
            else
                text.Hidden = true;
        }

        protected void SelectChangeTextState(Ext.Net.Radio radio1, Ext.Net.Radio radio2, Ext.Net.TextField text)
        {
            if (radio1.Checked || radio2.Checked)
                text.Hidden = false;
            else
                text.Hidden = true;
        }

        protected void SelectChangeTextState(Ext.Net.Radio radio1, Ext.Net.Radio radio2, Ext.Net.Radio radio3)
        {
            if (radio1.Checked || radio2.Checked)
                radio3.Hidden = false;
            else
                radio3.Hidden = true;
        }

        protected void SelectChangeTextState(Ext.Net.Radio radio1, Ext.Net.Radio radio2, Ext.Net.Label label)
        {
            if (radio1.Checked || radio2.Checked)
                label.Hidden = false;
            else
                label.Hidden = true;
        }

        protected void opt_2_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_2_2, opt_2_3, txt_2);
        }

        protected void opt_3_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_3_2, opt_3_3, txt_3);
        }

        protected void opt_5_3_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_5_3, txt_5);
        }

        protected void opt_12_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_12_2, txt_12);
        }

        protected void opt_13_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_13_2, txt_13b);
        }

        protected void opt_14_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_14_2, txt_14);
        }

        protected void opt_15_4_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_15_4, txt_15);
        }

        protected void opt_16_6_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_16_6, txt_16);
        }

        protected void opt_17_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_17_2, opt_17_3, txt_17);
        }

        protected void opt_19_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_19_2, txt_19a);
            SelectChangeTextState(opt_19_2, txt_19b);
            SelectChangeTextState(opt_19_2, txt_19c);
        }

        protected void opt_21_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_21_2, txt_21);
        }

        protected void opt_22_3_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_22_3, opt_22_4, Label22a);
            SelectChangeTextState(opt_22_3, opt_22_4, opt_22a_1);
            SelectChangeTextState(opt_22_3, opt_22_4, opt_22a_2);
        }

        protected void opt_23_6_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_23_6, txt_23);
        }

        protected void opt_26_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_26_2, txt_26);
        }

        protected void opt_30_2_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_30_2, txt_30);
        }

        protected void opt_37_6_Selected(object sender, DirectEventArgs e)
        {
            SelectChangeTextState(opt_37_6, txt_37);
        }

    }
}