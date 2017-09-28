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
    public partial class Dialysis_0h_05 : BaseForm
    {
        private string _TableName = "zinfo_h_05";
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

            X.Redirect("Dialysis_0h_05.aspx?editmode=list");
        }

        protected void Btn_Print_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + _Request("sel_info_date") + "&_REPORT_NAME=95");
        }

        protected void Btn_Clear_Click(object sender, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_0h_05.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_0h_05.aspx?editmode=list");
        }

        private void ShowList()
        {
            string[] col_0 = { "评估日期", "info_date" };
            string[] col_1 = { "评估时间", "txt_1" };
            string[] col_2 = { "部位", "txt_2" };
            string[] col_3 = { "评分", "txt_3" };
            string[] col_4 = { "评估护士", "txt_4" };
            
            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);
            acol.Add(col_3);
            acol.Add(col_4);
      
            _Fill_Html_Table(_TableName, _PAT_ID, acol, "疼痛评分表");
        }

        
    }
}