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
    public partial class Dialysis_09_26 : BaseForm
    {
        private string _TableName = "zinfo_p_06";
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
            X.Redirect("Dialysis_09_26.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }
        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_09_26.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);
            X.Redirect("Dialysis_09_26.aspx?editmode=list");
        }
        private void ShowList()
        {
            string[] col_0 = { "家访日期", "info_date" };
            string[] col_1 = { "评估者", "txt_1" };
            string[] col_2 = { "家访次数", "txt_2" };
            string[] col_3 = { "下次家访日期", "dat_3" };
            string[] col_4 = { "腹膜透析起始日", "dat_9" };
            string[] col_5 = { "曾接受血液透析", "txt_10" };
            string[] col_6 = { "多久(年)", "txt_11" };
            string[] col_7 = { "多久(月)", "txt_12" };
            string[] col_8 = { "使用系统", "opt_13" };
            string[] col_9 = { "处方", "opt_14" };
            string[] col_10 = { "浓度", "opt_15" };
            string[] col_11 = { "换液次数", "opt_19" };
            string[] col_12 = { "容量", "opt_20" };
            string[] col_13 = { "腹膜炎", "opt_21" };
            string[] col_14 = { "最后一次发生日期", "txt_22" };
            string[] col_15 = { "菌种", "txt_23" };
            string[] col_16 = { "原因", "txt_24" };
            string[] col_17 = { "Exit Site Infection", "opt_25" };
            string[] col_18 = { "最后一次发生日期", "txt_26" };
            string[] col_19 = { "菌种", "txt_27" };
            string[] col_20 = { "原因", "txt_28" };

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
            acol.Add(col_14);
            acol.Add(col_15);
            acol.Add(col_16);
            acol.Add(col_17);
            acol.Add(col_18);
            acol.Add(col_19);
            acol.Add(col_20);

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "随访记录");
        }
    }
}