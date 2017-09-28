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
    public partial class Dialysis_12_01 : BaseForm
    {
        private string _TableName = "dialysis_water1";
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
            X.Redirect("Dialysis_12_01.aspx?editmode=list");
        }


        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_12_01.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_12_01.aspx?editmode=list");
        }


        private void ShowList()
        {
            string[] col_0 = { "检查日期", "info_date" };
            string[] col_1 = { "细菌菌落-反渗机入口", "txt_11" };
            string[] col_2 = { "反渗机入口描述", "are_12" };
            string[] col_3 = { "反渗机出口", "txt_13" };
            string[] col_4 = { "反渗机出口描述", "are_14" };
            string[] col_5 = { "透析机入口",     "txt_15" };
            string[] col_6 = { "透析机入口描述", "are_16" };
            string[] col_7 = { "内毒素检测-反渗机入口", "txt_21" };
            string[] col_8 = { "反渗机入口描述", "are_22" };
            string[] col_9 = { "反渗机出口", "txt_23" };
            string[] col_10 = { "反渗机出口描述", "are_24" };
            string[] col_11 = { "透析机入口",    "txt_25" };
            string[] col_12 = { "透析机入口描述", "are_26" };
            string[] col_13 = { "余氯检测-游离氯", "txt_31" };
            string[] col_14 = { "游离氯描述", "are_32" };
            string[] col_15 = { "总氯",          "txt_33" };
            string[] col_16 = { "总氯描述", "are_34" };
            string[] col_17 = { "硬度检测-硬度",   "txt_41" };
            string[] col_18 = { "硬度描述", "are_42" };
            string[] col_19 = { "硬度", "txt_43" };
            string[] col_20 = { "硬度描述", "are_44" };


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

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "透析用水检测");
        }
    }
}