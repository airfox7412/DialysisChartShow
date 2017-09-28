using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Collections;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_0h_04 : BaseForm
    {
        private string _TableName = "zinfo_h_04";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //_zInfo_Show(_TableName, _PAT_ID, info_date.Text);
                sel_info_date = _Request("sel_info_date");
                switch (_Request("editmode"))
                {
                    case "list":
                        FormPanel1.Visible = false;
                        ShowList();
                        break;
                    case "show":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        btn_print.Visible = true;
                        btn_clear.Visible = false;
                        btn_save.Visible = false;
                        btn_close.Visible = true;
                        FormPanel1.Title = FormPanel1.Title + "-浏览";
                        break;
                    case "edit":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        btn_print.Visible = false;
                        btn_clear.Visible = true;
                        btn_save.Visible = true;
                        btn_close.Visible = false;
                        if (_Request("editmode2") == "add")
                        {
                            FormPanel1.Title = FormPanel1.Title + "-添加";
                        }
                        else
                        {
                            FormPanel1.Title = FormPanel1.Title + "-修改";
                        }
                        break;
                    case "delete":
                        _zInfo_Confirm_Delete(sel_info_date);
                        break;
                }
            }
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);

            X.Redirect("Dialysis_0h_04.aspx?editmode=list");
        }

        protected void Btn_Print_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + _Request("sel_info_date") + "&_REPORT_NAME=4");
        }

        protected void Btn_Clear_Click(object sender, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_0h_04.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_0h_04.aspx?editmode=list");
        }

        private void ShowList()
        {
            string[] col_0 = { "评估日期", "info_date" };
            string[] col_1 = { "评估护士", "txt_1" };
            string[] col_2 = { "严重程度评分1", "txt_3" };
            string[] col_3 = { "严重程度评分2", "txt_4" };
            string[] col_4 = { "分布范围评分1", "txt_7" };
            string[] col_5 = { "分布范围评分2", "txt_8" };
            string[] col_6 = { "发作频率评分1", "txt_11" };
            string[] col_7 = { "发作频率评分2", "txt_12" };
            string[] col_8 = { "夜间睡眠障碍评分", "txt_15" };
            string[] col_9 = { "总分", "txt_2" };

            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);
            acol.Add(col_3);
            acol.Add(col_4);
            acol.Add(col_5);
            acol.Add(col_6);
            acol.Add(col_7);
            acol.Add(col_8);
            acol.Add(col_9);
            
            _Fill_Html_Table(_TableName, _PAT_ID, acol, "血液透析患者皮肤瘙痒评估表(Sergio)");
        }

        private void _Total()
        {
            int i = 0;
            if (txt_3.Text != "")
                i = i + Convert.ToInt16(txt_3.Text);
            if (txt_4.Text != "")
                i = i + Convert.ToInt16(txt_4.Text);
            if (txt_7.Text != "")
                i = i + Convert.ToInt16(txt_7.Text);
            if (txt_8.Text != "")
                i = i + Convert.ToInt16(txt_8.Text);
            if (txt_11.Text != "")
                i = i + Convert.ToInt16(txt_11.Text);
            if (txt_12.Text != "")
                i = i + Convert.ToInt16(txt_12.Text);
            if (txt_15.Text != "")
                i = i + Convert.ToInt16(txt_15.Text);
            txt_2.Text = i.ToString(); 
        }

        protected void _CountRadio5(object sender, DirectEventArgs e)
        {
            if (opt_5_1.Checked == true)
                txt_3.Text = "1";
            if (opt_5_2.Checked == true)
                txt_3.Text = "2";
            if (opt_5_3.Checked == true)
                txt_3.Text = "3";
            if (opt_5_4.Checked == true)
                txt_3.Text = "4";
            if (opt_5_5.Checked == true)
                txt_3.Text = "5";
            _Total();
        }
        protected void _CountRadio6(object sender, DirectEventArgs e)
        {
            if (opt_6_1.Checked == true)
                txt_4.Text = "1";
            if (opt_6_2.Checked == true)
                txt_4.Text = "2";
            if (opt_6_3.Checked == true)
                txt_4.Text = "3";
            if (opt_6_4.Checked == true)
                txt_4.Text = "4";
            if (opt_6_5.Checked == true)
                txt_4.Text = "5";
            _Total();
        }
        protected void _CountRadio9(object sender, DirectEventArgs e)
        {
            if (opt_9_1.Checked == true)
                txt_7.Text = "1";
            if (opt_9_2.Checked == true)
                txt_7.Text = "2";
            if (opt_9_3.Checked == true)
                txt_7.Text = "3";
            _Total();
        }
        protected void _CountRadio10(object sender, DirectEventArgs e)
        {
            if (opt_10_1.Checked == true)
                txt_8.Text = "1";
            if (opt_10_2.Checked == true)
                txt_8.Text = "2";
            if (opt_10_3.Checked == true)
                txt_8.Text = "3";
            _Total();
        }
        protected void _CountRadio13(object sender, DirectEventArgs e)
        {
            if (opt_13_1.Checked == true)
                txt_11.Text = "1";
            if (opt_13_2.Checked == true)
                txt_11.Text = "2";
            if (opt_13_3.Checked == true)
                txt_11.Text = "3";
            if (opt_13_4.Checked == true)
                txt_11.Text = "4";
            if (opt_13_5.Checked == true)
                txt_11.Text = "5";
            _Total();
        }
        protected void _CountRadio14(object sender, DirectEventArgs e)
        {
            if (opt_14_1.Checked == true)
                txt_12.Text = "1";
            if (opt_14_2.Checked == true)
                txt_12.Text = "2";
            if (opt_14_3.Checked == true)
                txt_12.Text = "3";
            if (opt_14_4.Checked == true)
                txt_12.Text = "4";
            if (opt_14_5.Checked == true)
                txt_12.Text = "5";
            _Total();
        }
        protected void _CountRadio16(object sender, DirectEventArgs e)
        {
            if (opt_16_1.Checked == true)
                txt_15.Text = "2";
            if (opt_16_2.Checked == true)
                txt_15.Text = "4";
            if (opt_16_3.Checked == true)
                txt_15.Text = "6";
            if (opt_16_4.Checked == true)
                txt_15.Text = "8";
            if (opt_16_5.Checked == true)
                txt_15.Text = "10";
            if (opt_16_6.Checked == true)
                txt_15.Text = "12";
            if (opt_16_7.Checked == true)
                txt_15.Text = "14";
            _Total();
        }


    }
}