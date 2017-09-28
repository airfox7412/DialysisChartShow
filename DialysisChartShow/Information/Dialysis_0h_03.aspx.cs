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
    public partial class Dialysis_0h_03 : BaseForm
    {
        private string _TableName = "zinfo_h_03";
        private string sel_info_date = "";
        private int iSCORE;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                //_zInfo_Show(_TableName, _PAT_ID, info_date.Text);

                //if (txt_37.Text == "")
                //    Global2.g_iSCORE = 0;
                //else
                //    Global2.g_iSCORE = Convert.ToInt16(txt_37.Text);


                if (string.IsNullOrEmpty(_PAT_ID))
                {
                    _PAT_ID = _Request("_PAT_ID");
                    _PAT_IC = _Request("_PAT_IC");
                    _PIF_NAME = _Request("_PIF_NAME");
                    _USER_NAME = _Request("_USER_NAME");
                    _PIF_SX = _Request("_PIF_SX");
                    //_UserID = _Request("_USER_ID");
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
                        //_zInfo_Confirm_Delete(sel_info_date);
                        _zInfo_Confirm_Delete(sel_info_date);
                        break;
                }
            }
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
            X.Redirect("Dialysis_0h_03.aspx?editmode=list");
        }

        protected void Btn_Print_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_INFO_DATE=" + _Request("sel_info_date") + "&_REPORT_NAME=3");
        }

        protected void Btn_Clear_Click(object sender, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_0h_03.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_0h_03.aspx?editmode=list");
        }

        private void ShowList()
        {
            string[] col_0 = { "评估日期", "info_date" };
            string[] col_1 = { "评估护士", "txt_1" };
            string[] col_2 = { "总分", "txt_37" };
            string[] col_3 = { "血管护理计划", "opt_38" };

            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);
            acol.Add(col_3);

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "动静脉内瘘闭塞高危因素评估表");
        }

        private static T CType<T>(object obj)
	    {
	        try { return (T)obj; }
	        catch { return default(T); }
	    }

        private void _CountResult()
        {
            //txt_1.Text = txt_1.Text + Global2.g_iCNT.ToString() + "-" + _opt.ID.ToString() + "(" + _opt.Checked.ToString() + ")";
            //Global2.g_iCNT++;
            //iSCORE = Global2.g_iSCORE;

            txt_37.Text = iSCORE.ToString();

            string sRULE = "";
            int iLEVEL = 0;
            int iOPT = 0;

            //一般危险群
            //评估得分表：总分<10分
            if (iSCORE < 10)
            {
                iLEVEL = 0;
                sRULE = "一般危险群(评估得分表：总分<10分)";
            }
            
            //中危险群(符合以下任一项条件者勾选此项)
            //1.评估得分表：总分11-20分
            if (iSCORE >= 11 && iSCORE <= 20)
            {
                iLEVEL = 1;
                sRULE = "中危险群(1.评估得分表：总分11-20分)";
            }
            //2.60岁以上、血管性质为Graft
            if (opt_3_2.Checked == true && (opt_26_2.Checked == true || opt_26_3.Checked == true))
            {
                iLEVEL = 1;
                sRULE = "中危险群(2.60岁以上、血管性质为Graft)";
            }
            //3.血管条件得分为6分
            iOPT = 0;
            if (opt_14_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_14_3.Checked == true)
                iOPT = iOPT + 2;
            if (opt_15_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_15_3.Checked == true)
                iOPT = iOPT + 2;
            if (opt_16_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_16_3.Checked == true)
                iOPT = iOPT + 2;
            if (opt_17_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_17_3.Checked == true)
                iOPT = iOPT + 2;
            if (opt_18_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_18_3.Checked == true)
                iOPT = iOPT + 2;
            if (iOPT >= 6)
            {
                iLEVEL = 1;
                sRULE = "中危险群(3.血管条件得分为6分)";
            }
            //4.透析状态评估有2项异常
            iOPT = 0;
            if (opt_30_2.Checked == true || opt_30_3.Checked == true)
                iOPT = iOPT + 1;
            if (opt_31_2.Checked == true || opt_31_3.Checked == true)
                iOPT = iOPT + 1;
            if (opt_32_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_33_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_34_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_35_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_36_2.Checked == true)
                iOPT = iOPT + 1;
            if (iOPT >= 2)
            {
                iLEVEL = 1;
                sRULE = "中危险群(4.透析状态评估有2项异常)";
            }

            //高危险群(符合以下任一项条件者勾选此项)
            //1.评估得分表：总分21分以上
            if (iSCORE >= 21)
            {
                iLEVEL = 2;
                sRULE = "高危险群(1.评估得分表：总分21分以上)";
            }
            //2.男性、60岁以上、血管性质为Graft
            if (opt_2_2.Checked == true && opt_3_2.Checked == true &&  (opt_26_2.Checked == true || opt_26_3.Checked == true))
            {
                iLEVEL = 2;
                sRULE = "高危险群(2.男性、60岁以上、血管性质为Graft)";
            }
            //3.血管条件得分为8分
            iOPT = 0;
            if (opt_14_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_14_3.Checked == true)
                iOPT = iOPT + 2;
            if (opt_15_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_15_3.Checked == true)
                iOPT = iOPT + 2;
            if (opt_16_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_16_3.Checked == true)
                iOPT = iOPT + 2;
            if (opt_17_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_17_3.Checked == true)
                iOPT = iOPT + 2;
            if (opt_18_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_18_3.Checked == true)
                iOPT = iOPT + 2;
            if (iOPT >= 8)
            {
                iLEVEL = 2;
                sRULE = "高危险群(3.血管条件得分为8分)";
            }
            //4.透析状态评估有3项以上异常
            iOPT = 0;
            if (opt_30_2.Checked == true || opt_30_3.Checked == true)
                iOPT = iOPT + 1;
            if (opt_31_2.Checked == true || opt_31_3.Checked == true)
                iOPT = iOPT + 1;
            if (opt_32_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_33_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_34_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_35_2.Checked == true)
                iOPT = iOPT + 1;
            if (opt_36_2.Checked == true)
                iOPT = iOPT + 1;
            if (iOPT >= 3)
            {
                iLEVEL = 2;
                sRULE = "高危险群(4.透析状态评估有3项以上异常)";
            }

            switch (iLEVEL)
            {
                case 0:
                    opt_38_1.Checked = true;
                    break;
                case 1:
                    opt_38_2.Checked = true;
                    break;
                case 2:
                    opt_38_3.Checked = true;
                    break;
            }

            FormPanel1.Title = "动静脉内瘘闭塞高危因素评估表 总分=" + iSCORE.ToString() + "  " + sRULE;
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
            
            //分開效果不好，還是使用全部搜尋再加總分數
            //Ext.Net.Radio _opt = CType<Ext.Net.Radio>(sender);
            //if (_opt.Checked == true)
            //    Global2.g_iSCORE = Global2.g_iSCORE + 1;
            //else
            //    Global2.g_iSCORE = Global2.g_iSCORE - 1;
            
            _CountResult();
        }

        protected void _CountRadio2(object sender, DirectEventArgs e)
        {
            string radname = "";
            iSCORE = 0;
            _Count_Radio(base.Form.Controls, radname);
            
            //分開效果不好，還是使用全部搜尋再加總分數
            //Ext.Net.Radio _opt = CType<Ext.Net.Radio>(sender);
            //if (_opt.Checked == true)
            //    Global2.g_iSCORE = Global2.g_iSCORE + 2;
            //else
            //    Global2.g_iSCORE = Global2.g_iSCORE - 2;

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

    }
}