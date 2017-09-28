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
    public partial class Dialysis_02_05 : BaseForm
    {
        private string _TableName = "zinfo_b_05";
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
            X.Redirect("Dialysis_02_05.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_02_05.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_02_05.aspx?editmode=list");
        }
        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }
        private void ShowList()
        {
            string[] col_0 = { "是否使用", "opt_1" };
            string[] col_1 = { "日期", "info_date" };
            string[] col_2 = { "抗凝剂", "opt_2" };
            string[] col_3 = { "肝素首剂量", "num_3" };
            string[] col_4 = { "肝素追加剂量", "num_4" };
            string[] col_5 = { "肝素总剂量", "num_5" };
            string[] col_6 = { "低分子肝素", "opt_6" };
            string[] col_7 = { "低分子肝素钠", "opt_7" };
            string[] col_8 = { "其它低分子肝素钠", "txt_8" };
            string[] col_9 = { "低分子肝素钙", "opt_9" };
            string[] col_10 = { "其他低分子肝素钙", "txt_10" };
            string[] col_11 = { "低分子肝素首剂量", "num_11" };
            string[] col_12 = { "低分子肝素追加剂量", "num_12" };
            string[] col_13 = { "低分子肝素追加时间", "num_13" };
            string[] col_14 = { "低分子肝素总剂量", "num_14" };
            string[] col_15 = { "其它抗凝剂", "txt_15" };
            string[] col_16 = { "首剂量", "txt_16" };
            string[] col_17 = { "追加剂量", "txt_17" };
            string[] col_18 = { "总剂量", "txt_18" };

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


            _Fill_Html_Table(_TableName, _PAT_ID, acol, "抗凝剂列表");
        }
    }
}