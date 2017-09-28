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
    public partial class Dialysis_01_03 :  BaseForm
    {
        private string _TableName = "zinfo_a_03";
        private string sel_info_date ="";
        
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest )
            {   
                sel_info_date =_Request("sel_info_date");
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
            X.Redirect("Dialysis_01_03.aspx?editmode=list");
        }


        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_01_03.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_01_03.aspx?editmode=list");
        }
        

        private void ShowList()
        {
            string[] col_0 = {"检查日期","info_date"};
            string[] col_1 = { "并发症分类", "chk_1" };
            string[] col_2 = { "骨矿物质代谢紊乱", "chk_2" };
            string[] col_3 = { "其它骨矿物质代谢紊乱", "txt_3" };
            string[] col_4 = { "淀粉样变性", "chk_4" };
            string[] col_5 = { "其它淀粉样变性", "txt_5" };
            string[] col_6 = { "呼吸系统并发症", "chk_6" };
            string[] col_7 = { "其它呼吸系统并发症", "txt_7" };
            string[] col_8 = { "心血管", "chk_8" };
            string[] col_9 = { "其它心血管并发症", "txt_9" };
            string[] col_10 = { "神经系统", "chk_10" };
            string[] col_11 = { "其它神经系统并发症", "txt_11" };
            string[] col_12 = { "消化系统并发症", "chk_12" };
            string[] col_13 = { "其他消化系统并发症", "txt_13" };
            string[] col_14 = { "其它并发症", "txt_14" };
            string[] col_15 = { "具体情况描述", "txt_15" };

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

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "并发症诊断列表");
        }
    }
}