using System;
using System.Data;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Collections;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_23 : BaseForm
    {
        private string _TableName = "zinfo_p_03";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                sel_info_date = _Request("sel_info_date");
                switch (_Request("editmode"))
                {
                    case "list":
                        FormPanel1.Visible = false;
                        ShowList();
                        break;
                    case "show":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        btn_clear.Visible = false;
                        btn_save.Visible = false;
                        FormPanel1.Title = FormPanel1.Title + "-浏览";
                        break;
                    case "edit":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        btn_clear.Visible = true;
                        btn_save.Visible = true;
                        Btn_Close.Visible = false;
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

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
            X.Redirect("Dialysis_09_23.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }
        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_09_23.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_09_23.aspx?editmode=list");
        }
        private void ShowList()
        {
            string[] col_0 = { "初次腹膜透析日期", "info_date" };
            string[] col_1 = { "治疗模式", "opt_1" };
            string[] col_2 = { "主要换液者", "opt_2" };
            string[] col_3 = { "第几次感染", "txt_3" };
            string[] col_4 = { "上次感染日期", "dat_4" };
            string[] col_5 = { "有无长菌", "opt_5" };
            string[] col_6 = { "菌种", "txt_6" };
            string[] col_7 = { "病患是否有住院", "opt_7" };
            string[] col_8 = { "住院日期", "dat_8" };
            string[] col_9 = { "出院日期", "dat_9" };
            string[] col_10 = { "感染日期", "dat_10" };
            string[] col_11 = { "合并 Exit inf", "opt_11" };
            string[] col_12 = { "合并Tunnel inf", "opt_12" };
            string[] col_13 = { "感染原因", "chk_13" };

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
            acol.Add(col_10);
            acol.Add(col_11);
            acol.Add(col_12);
            acol.Add(col_13);

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "腹膜炎记录");
        }
    }
}