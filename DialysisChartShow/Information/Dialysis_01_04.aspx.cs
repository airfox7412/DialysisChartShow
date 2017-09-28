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
    public partial class Dialysis_01_04 : BaseForm
    {
        private string _TableName = "zinfo_a_04";
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
            DataTable dtIC = db.Query("SELECT * FROM pat_info WHERE pif_id='" + _PAT_ID + "' ");
            if (dtIC.Rows.Count > 0)
            {
                DataTable dtBG = db.Query("SELECT * FROM blood_group WHERE bgrp_patic='" + dtIC.Rows[0]["pif_ic"].ToString() + "' ");
                if (dtBG.Rows.Count == 0)
                {
                    db.Excute("INSERT INTO blood_group (bgrp_patic, bgrp_hcv, bgrp_hbv, bgrp_aids, bgrp_syphilis) " +
                              "VALUES ('" + dtIC.Rows[0]["pif_ic"].ToString() + "', 'N', 'N', 'N', 'N') ");
                }
                if (chk_1_2.Checked)  //丙肝
                    db.Excute("UPDATE blood_group SET bgrp_hcv='Y' WHERE bgrp_patic='" + dtIC.Rows[0]["pif_ic"].ToString() + "' ");
                if (chk_1_3.Checked)  //乙肝
                    db.Excute("UPDATE blood_group SET bgrp_hbv='Y' WHERE bgrp_patic='" + dtIC.Rows[0]["pif_ic"].ToString() + "' ");
                if (chk_1_4.Checked)  //艾滋病
                    db.Excute("UPDATE blood_group SET bgrp_aids='Y' WHERE bgrp_patic='" + dtIC.Rows[0]["pif_ic"].ToString() + "' ");
                if (chk_1_5.Checked)  //梅毒
                    db.Excute("UPDATE blood_group SET bgrp_syphilis='Y' WHERE bgrp_patic='" + dtIC.Rows[0]["pif_ic"].ToString() + "' ");
            }
            X.Redirect("Dialysis_01_04.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }
        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_01_04.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_01_04.aspx?editmode=list");
        }
        private void ShowList()
        {
            string[] col_0 = { "检查日期", "info_date" };
            string[] col_1 = { "并发症分类", "chk_1" };
            string[] col_2 = { "其它请说明", "txt_2" };

            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "传染病诊断列表");
        }
    }
}