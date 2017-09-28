using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Collections;

namespace Dialysis_Chart_Show.Ipad
{
    public partial class Dialysis_0h_02 : BaseForm
    {
        private string _TableName = "zinfo_h_02";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request.QueryString["pid"];
                _PAT_ID = patient_id.Text;
                floor.Text = Request["floor"].ToString();
                area.Text = Request["area"].ToString();
                time.Text = Request["time"].ToString();
                bedno.Text = Request["bedno"].ToString();
                daytyp.Text = Request["daytyp"].ToString();

                sel_info_date = _Request("sel_info_date");
                switch (_Request("editmode"))
                {
                    case "list":
                        FormPanel1.Visible = false;
                        ShowList();
                        break;
                    case "show":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        setView();
                        btn_save.Visible = false;
                        btn_close.Visible = true;
                        FormPanel1.Title = FormPanel1.Title + "-浏览";
                        break;
                    case "edit":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        setView();
                        btn_save.Visible = true;
                        btn_close.Visible = true;
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

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_0h_02.aspx?editmode=list");
        }

        private void ShowList()
        {
            string[] col_0 = { "诊断日期", "info_date" };
            string[] col_1 = { "视诊血管通路物理检查小结", "txt_52" };
            string[] col_2 = { "触诊物理检查小结", "txt_60" };
            string[] col_3 = { "听诊物理检查小结", "txt_64" };
            string[] col_4 = { "血管通路物理检查诊断1", "txt_65" };
            string[] col_5 = { "血管通路物理检查诊断2", "txt_66" };
            string[] col_6 = { "血管通路物理检查诊断3", "txt_67" };
            
            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);
            acol.Add(col_3);
            acol.Add(col_4);
            acol.Add(col_5);
            acol.Add(col_6);

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "血管通路动静脉内瘘物理检查评估表");
        }

        protected void Btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }
        
        protected void setView()
        {
            if (opt_44_2.Checked == true)
                txt_45.Hidden = false;
            if (opt_17_2.Checked == true)
            {
                opt_18_1.Hidden = false;
                opt_18_2.Hidden = false;
                opt_18_3.Hidden = false;
                txt_19.Hidden = false;
            }
            if (opt_49_2.Checked == true)
                txt_50.Hidden = false;
            if (opt_31_2.Checked == true)
            {
                opt_32_1.Hidden = false;
                opt_32_2.Hidden = false;
            }
            if (opt_53_2.Checked == true)
            {
                opt_54_1.Hidden = false;
                opt_54_2.Hidden = false;
                opt_54_3.Hidden = false;
            }
            if (opt_56_2.Checked == true)
            {
                Label24.Hidden = false;
                opt_57_1.Hidden = false;
                opt_57_2.Hidden = false;
                opt_58_1.Hidden = false;
                opt_58_2.Hidden = false;
                opt_58_3.Hidden = false;
            }
        }

        protected void hh(object sender, DirectEventArgs e)
        {
            if (opt_44_2.Checked == true)
            {
                txt_45.Hidden = false;
            }
            else
            {
                txt_45.Hidden = true;
            }
        }

        protected void ii(object sender, DirectEventArgs e)
        {
            if (opt_17_2.Checked == true)
            {
                opt_18_1.Hidden = false;
                opt_18_2.Hidden = false;
                opt_18_3.Hidden = false;
                txt_19.Hidden = false;
            }
            else
            {
                opt_18_1.Hidden = true;
                opt_18_2.Hidden = true;
                opt_18_3.Hidden = true;
                txt_19.Hidden = true;
            }
        }

        protected void jj(object sender, DirectEventArgs e)
        {
            if (opt_49_2.Checked == true)
            {
                txt_50.Hidden = false;
            }
            else
            {
                txt_50.Hidden = true;
            }
        }

        protected void kk(object sender, DirectEventArgs e)
        {
            if (opt_31_2.Checked == true)
            {
                opt_32_1.Hidden = false;
                opt_32_2.Hidden = false;
            }
            else
            {
                opt_32_1.Hidden = true;
                opt_32_2.Hidden = true;
            }
        }

        protected void ll(object sender, DirectEventArgs e)
        {
            if (opt_53_2.Checked == true)
            {
                opt_54_1.Hidden = false;
                opt_54_2.Hidden = false;
                opt_54_3.Hidden = false;
            }
            else
            {
                opt_54_1.Hidden = true;
                opt_54_2.Hidden = true;
                opt_54_3.Hidden = true;
            }
        }

        protected void mm(object sender, DirectEventArgs e)
        {
            if (opt_56_2.Checked == true)
            {
                Label24.Hidden = false;
                opt_57_1.Hidden = false;
                opt_57_2.Hidden = false;
                opt_58_1.Hidden = false;
                opt_58_2.Hidden = false;
                opt_58_3.Hidden = false;
            }
            else
            {
                Label24.Hidden = true;
                opt_57_1.Hidden = true;
                opt_57_2.Hidden = true;
                opt_58_1.Hidden = true;
                opt_58_2.Hidden = true;
                opt_58_3.Hidden = true;
            }
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
            Btn_Close_Click(sender, e);
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("../ipad_EvaForm.aspx?pid=" + patient_id.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }
    }
}