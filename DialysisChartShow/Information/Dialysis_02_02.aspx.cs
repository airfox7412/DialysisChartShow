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
    public partial class Dialysis_02_02 : BaseForm
    {
        private string _TableName = "zinfo_b_02";
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
            X.Redirect("Dialysis_02_02.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_02_02.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_02_02.aspx?editmode=list");
        }
        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }

        private void ShowList()
        {
            string[] col_0 = { "处方日期", "info_date" };
            string[] col_1 = { "HD次数", "num_1" };
            string[] col_2 = { "HD治疗时间", "num_2" };
            string[] col_3 = { "HDF次数", "opt_3" };
            string[] col_4 = { "其它HDF次数说明", "txt_4" };
            string[] col_5 = { "HDF治疗时间", "num_5" };
            string[] col_6 = { "HP次数", "opt_6" };
            string[] col_7 = { "其它HP次数说明", "txt_7" };
            string[] col_8 = { "HP治疗时间", "num_8" };
            string[] col_9 = { "透析浓缩液", "chk_9" };
            string[] col_10 = { "透析浓缩A液", "opt_10" };
            string[] col_11 = { "透析液钾离子浓度", "opt_11" };
            string[] col_12 = { "其他钾离子浓度值", "num_12" };
            string[] col_13 = { "透析液钙离子浓度", "opt_13" };
            string[] col_14 = { "其他鈣离子浓度值", "num_14" };
            string[] col_15 = { "含糖透析液", "opt_15" };
            string[] col_16 = { "氨基酸透析液", "opt_16" };
            string[] col_17 = { "透析浓缩B液", "opt_17" };
            string[] col_18 = { "类型", "chk_18" };
            string[] col_19 = { "通量", "opt_19" };
            string[] col_20 = { "使用", "opt_20" };
            string[] col_21 = { "透析膜", "chk_21" };
            string[] col_22 = { "其它请说明", "txt_22" };
            string[] col_23 = { "膜面积", "opt_23" };


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
            acol.Add(col_21);
            acol.Add(col_22);
            acol.Add(col_23);


            _Fill_Html_Table(_TableName, _PAT_ID, acol, "透析处方列表");
        }
    }
}