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
    public partial class Dialysis_03_07 : BaseForm
    {
        private string _TableName = "zinfo_c_07";
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
            X.Redirect("Dialysis_03_07.aspx?editmode=list");

        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }
        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_03_07.aspx?editmode=list");
        }
        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_03_07.aspx?editmode=list");
        }
        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }
        private void ShowList()
        {
            string[] col_0 = { "是否使用", "opt_1" };
            string[] col_1 = { "处方日期", "info_date" };
            string[] col_2 = { "营养支持药物分类", "chk_2" };
            string[] col_3 = { "左旋肉碱名称", "opt_3" };
            string[] col_4 = { "其它左旋肉碱名称", "txt_4" };
            string[] col_5 = { "用药方式", "opt_5" };
            string[] col_6 = { "单次给药剂量", "num_6" };
            string[] col_7 = { "服药频率", "opt_7" };
            string[] col_8 = { "其它服药频率", "num_8" };
            string[] col_9 = { "用药方式", "opt_9" };
            string[] col_10 = { "单次给药剂量", "num_10" };
            string[] col_11 = { "服药频率", "opt_11" };
            string[] col_12 = { "其它服药频率", "num_12" };
            string[] col_13 = { "用药方式", "opt_13" };
            string[] col_14 = { "单次给药剂量", "num_14" };
            string[] col_15 = { "服药频率", "opt_15" };
            string[] col_16 = { "其它服药频率", "num_16" };
            string[] col_17 = { "用药方式", "opt_17" };
            string[] col_18 = { "单次给药剂量", "num_18" };
            string[] col_19 = { "服药频率", "opt_19" };
            string[] col_20 = { "其它服药频率", "num_20" };
            string[] col_21 = { "α酮酸名称", "opt_21" };
            string[] col_22 = { "其它", "txt_22" };
            string[] col_23 = { "用药方式", "opt_23" };
            string[] col_24 = { "单次给药剂量", "num_24" };
            string[] col_25 = { "服药频率", "opt_25" };
            string[] col_26 = { "其它服药频率", "num_26" };
            string[] col_27 = { "其他营养支持药物名称", "txt_27" };
            string[] col_28 = { "用药方式", "opt_28" };
            string[] col_29 = { "单次给药剂量", "num_29" };
            string[] col_30 = { "服药频率", "opt_30" };
            string[] col_31 = { "其它服药频率", "num_31" };
            string[] col_32 = { "降脂药物分类", "opt_32" };
            string[] col_33 = { "他汀类药物 ", "opt_33" };
            string[] col_34 = { "其它他汀类药物 ", "txt_34" };
            string[] col_35 = { "贝特类药物", "opt_35" };
            string[] col_36 = { "其它贝特类药物", "txt_36" };
            string[] col_37 = { "其它降脂药物", "txt_37" };
            string[] col_38 = { "单次给药剂量", "num_38" };
            string[] col_39 = { "服药频率", "opt_39" };
            string[] col_40 = { "其它服药频率", "num_40" };
            string[] col_41 = { "抗血小板药物分类", "opt_41" };
            string[] col_42 = { "其它抗血小板药物", "txt_42" };
            string[] col_43 = { "单次给药剂量", "num_43" };
            string[] col_44 = { "服药频率", "opt_44" };
            string[] col_45 = { "其它服药频率", "num_45" };
            string[] col_46 = { "中成药名称", "chk_46" };
            string[] col_47 = { "其它", "txt_47" };

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
            acol.Add(col_24);
            acol.Add(col_25);
            acol.Add(col_26);
            acol.Add(col_27);
            acol.Add(col_28);
            acol.Add(col_29);
            acol.Add(col_30);
            acol.Add(col_31);
            acol.Add(col_32);
            acol.Add(col_33);
            acol.Add(col_34);
            acol.Add(col_35);
            acol.Add(col_36);
            acol.Add(col_37);
            acol.Add(col_38);
            acol.Add(col_39);
            acol.Add(col_40);
            acol.Add(col_41);
            acol.Add(col_42);
            acol.Add(col_43);
            acol.Add(col_44);
            acol.Add(col_45);
            acol.Add(col_46);
            acol.Add(col_47);




            _Fill_Html_Table(_TableName, _PAT_ID, acol, "其它药物治疗列表");
        }
    }
}