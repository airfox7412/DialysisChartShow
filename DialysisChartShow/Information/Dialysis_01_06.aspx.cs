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
    public partial class Dialysis_01_06 : BaseForm
    {
        private string _TableName = "zinfo_a_06";
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
            X.Redirect("Dialysis_01_06.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_01_06.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_01_06.aspx?editmode=list");
        }
        private void ShowList()
        {
            string[] col_0 = { "诊断日期", "info_date" };
            string[] col_1 = { "过敏反应", "chk_1" };
            string[] col_2 = { "其他过敏反应", "txt_2" };
            string[] col_3 = { "透析器材过敏", "chk_3" };
            string[] col_4 = { "透析膜", "chk_4" };
            string[] col_5 = { "其它透析膜说明", "txt_5" };
            string[] col_6 = { "请填写具体透析器型号", "txt_6" };
            string[] col_7 = { "消毒剂", "chk_7" };
            string[] col_8 = { "其它消毒剂说明", "txt_8" };
            string[] col_9 = { "药物过敏", "chk_9" };
            string[] col_10 = { "抗生素", "txt_10" };
            string[] col_11 = { "静脉铁剂", "chk_11" };
            string[] col_12 = { "蔗糖铁", "txt_12" };
            string[] col_13 = { "右旋糖苷铁", "txt_13" };
            string[] col_14 = { "肝素", "opt_14" };
            string[] col_15 = { "其它药物过敏说明", "txt_15" };

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

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "过敏诊断列表");
        }
    }
}