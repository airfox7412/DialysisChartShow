using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Collections;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show.Ipad
{
    public partial class Dialysis_0h_07 : BaseForm
    {
        private string _TableName = "zinfo_h_07";
        private string sel_info_date = "";
        private int iSCORE;

        protected void Page_Load(object sender, EventArgs e)
        {
            patient_id.Text = Request.QueryString["pid"];
            _PAT_ID = patient_id.Text;
            floor.Text = Request["floor"].ToString();
            area.Text = Request["area"].ToString();
            time.Text = Request["time"].ToString();
            bedno.Text = Request["bedno"].ToString();
            daytyp.Text = Request["daytyp"].ToString();

            if (!X.IsAjaxRequest)
            {
                if (string.IsNullOrEmpty(_PAT_ID))
                {
                    _PAT_ID = _Request("_PAT_ID");
                    _PAT_IC = _Request("_PAT_IC");
                    _PIF_NAME = _Request("_PIF_NAME");
                    _USER_NAME = _Request("_USER_NAME");
                    _PIF_SX = _Request("_PIF_SX");
                    string sUSER_ID = Request.QueryString["_USER_ID"] == null ? string.Empty : Request.QueryString["_USER_ID"].ToString();
                    if (sUSER_ID != "")
                    {
                        _UserID = sUSER_ID;
                    }
                    else
                    {
                        if (_UserID == "")
                            _UserID = "test";
                    }
                }

                if (_Request("sel_info_date") == "")
                {
                    sel_info_date = DateTime.Now.ToString("yyyy-MM-dd");
                }
                else
                {
                    sel_info_date = _Request("sel_info_date");
                }

                switch (_Request("editmode"))
                {
                    case "list":
                        FormPanel1.Visible = false;
                        ShowList();
                        break;
                    case "show":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        btn_save.Visible = false;
                        btn_close.Visible = true;
                        FormPanel1.Title = FormPanel1.Title + "-浏览";
                        break;
                    case "edit":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        txt_1.Text = Session["USER_NAME"].ToString();
                        section.Text = area.Text;
                        bed_no.Text = bedno.Text;

                        btn_save.Visible = true;
                        btn_close.Visible = true;
                        FormPanel1.Title = FormPanel1.Title + "-修改";
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

            X.Redirect("Dialysis_0h_07.aspx?editmode=list");
        }

        private void ShowList()
        {
            string[] col_0 = { "评估日期", "info_date" };
            string[] col_1 = { "评估护士", "txt_1" };
            string[] col_3 = { "病区", "section" };
            string[] col_4 = { "床号", "bed_no" };

            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_3);
            acol.Add(col_4);

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "预防跌倒护理措施评估表");
        }

        private static T CType<T>(object obj)
        {
            try { return (T)obj; }
            catch { return default(T); }
        }


        private void _CountResult()
        {
            txt_37.Text = iSCORE.ToString();

            string sRULE = "";
            int iOPT = 0;

            if (opt_2_2.Checked == true)
                iOPT = iOPT + 1;
            else
                if (opt_2_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;

            if (opt_3_2.Checked == true)
                iOPT = iOPT + 1;
            else
                if (opt_3_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;

            if (opt_4_2.Checked == true )
                iOPT = iOPT + 1;
			else
                if (opt_4_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;

            if (opt_5_2.Checked == true )
                iOPT = iOPT + 1;
			else
                if (opt_5_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;

                iOPT = iOPT + 0;
            if (opt_6_2.Checked == true)
                iOPT = iOPT + 1;
			else
                if (opt_6_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;

                iOPT = iOPT + 0;
            if (opt_7_2.Checked == true)
                iOPT = iOPT + 1;
			else
                if (opt_7_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;


            if (opt_8_2.Checked == true)
                iOPT = iOPT + 1;
			else
                if (opt_8_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;


            if (opt_9_2.Checked == true)
                iOPT = iOPT + 1;
			else
                if (opt_9_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;

            if (opt_10_2.Checked == true)
                iOPT = iOPT + 1;
			else
                if (opt_10_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;

			if (opt_11_2.Checked == true)
                iOPT = iOPT + 1;
            else
                if (opt_11_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;


            if (opt_12_2.Checked == true)
                iOPT = iOPT + 1;
            else
                if (opt_12_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;


            if (opt_13_2.Checked == true)
                iOPT = iOPT + 1;
            else
                if (opt_13_3.Checked == true)
                    iOPT = iOPT + 2;
                else
                    iOPT = iOPT + 0;
            FormPanel1.Title = "预防跌倒护理措施评估表 总分=" + iSCORE.ToString() + "  " + sRULE;
        }

        protected void _CountRadio0(object sender, DirectEventArgs e)
        {
            string radname = "";
            iSCORE = 0;
            _Count_Radio(base.Form.Controls, radname);

            _CountResult();
        }

        protected void _CountRadio1(object sender, DirectEventArgs e)
        {
            string radname = "";
            iSCORE = 0;
            _Count_Radio(base.Form.Controls, radname);
            _CountResult();
        }

        protected void _CountRadio2(object sender, DirectEventArgs e)
        {
            string radname = "";
            iSCORE = 0;
            _Count_Radio(base.Form.Controls, radname);
            _CountResult();
        }

        private void _Count_Radio(System.Web.UI.ControlCollection objcol, string radname)
        {
            try
            {
                int iID;
                int iNO;

                foreach (object obj in objcol)
                {
                    System.Type itype = obj.GetType();
                    if (itype.Namespace == "Ext.Net" || itype.Namespace == "System.Web.UI.HtmlControls")
                    {
                        switch (itype.Name)
                        {
                            case "HtmlForm":
                                System.Web.UI.HtmlControls.HtmlForm htmlform = (System.Web.UI.HtmlControls.HtmlForm)obj;
                                _Count_Radio(htmlform.Controls, radname);
                                break;
                            case "Viewport":
                                Ext.Net.Viewport viewport = (Ext.Net.Viewport)obj;
                                _Count_Radio(viewport.Controls, radname);
                                break;
                            case "FormPanel":
                                Ext.Net.FormPanel formpanel = (Ext.Net.FormPanel)obj;
                                _Count_Radio(formpanel.Controls, radname);
                                break;
                            case "Panel":
                                Ext.Net.Panel panel = (Ext.Net.Panel)obj;
                                _Count_Radio(panel.Controls, radname);
                                break;
                            case "TabPanel":
                                Ext.Net.TabPanel tabpanel = (Ext.Net.TabPanel)obj;
                                _Count_Radio(tabpanel.Controls, radname);
                                break;
                            case "Container":
                                Ext.Net.Container container = (Ext.Net.Container)obj;
                                _Count_Radio(container.Controls, radname);
                                break;
                            case "FieldSet":
                                Ext.Net.FieldSet fieldset = (Ext.Net.FieldSet)obj;
                                _Count_Radio(fieldset.Controls, radname);
                                break;
                            case "ContentContainer":
                                Ext.Net.ContentContainer contentcontainer = (Ext.Net.ContentContainer)obj;
                                _Count_Radio(contentcontainer.Controls, radname);
                                break;
                            case "RadioGroup":
                                Ext.Net.RadioGroup radioGroup = (Ext.Net.RadioGroup)obj;
                                _Count_Radio(radioGroup.Controls, radname);
                                break;
                            case "Radio":
                                Ext.Net.Radio radio = (Ext.Net.Radio)obj;

                                string[] s = radio.ID.Split('_');
                                iID = Convert.ToInt16(s[1]);
                                iNO = Convert.ToInt16(s[2]);

                                if (iID > 1 && iID < 38)
                                {
                                    if (radio.Value.Equals(true))
                                    {
                                        iSCORE = iSCORE + iNO - 1;
                                    }
                                }
                                break;
                            case "":
                                break;

                        }
                    }
                }
            }
            catch (Exception ex)
            {
                _ErrorMsgShow(ex.Message.ToString());
            }
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text, this.txt_1.Text.Trim(), this.section.Text.Trim(), this.bed_no.Text.Trim());
            Common._NotificationShow("储存成功！");
            Btn_Close_Click(sender, e);
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("../ipad_EvaForm.aspx?pid=" + patient_id.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }

    }
}