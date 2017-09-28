using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using Ext.Net;
using System.Data;
using System.Collections;
using System.Drawing;
using System.Web.Services;

namespace Dialysis_Chart_Show
{
    public partial class BaseForm : System.Web.UI.Page
    {
        protected DBMysql db = new DBMysql();
        private Hashtable _haTable;
        private Hashtable _objTable;

        public string _PAT_ID//
        {
            get
            {
                try
                {
                    return Session["_PAT_ID"] == null ? string.Empty : Session["_PAT_ID"].ToString();
                }
                catch
                {
                    //X.Redirect("~/Information/Info_index.aspx", true); 
                    _NotificationShow_TimeOut();
                    return "";
                }
            }
            set
            {
                Session.Add("_PAT_ID", value);
            }
        }

        public string _PAT_IC//身份证号
        {
            get
            {
                try
                {
                    return Session["_PAT_IC"] == null ? string.Empty : Session["_PAT_IC"].ToString();
                }
                catch
                {
                    _NotificationShow_TimeOut();
                    return "";
                }
            }
            set
            {
                Session.Add("_PAT_IC", value);
            }
        }

        public string _PIF_NAME//姓名
        {
            get
            {
                try
                {
                    return Session["_PIF_NAME"] == null ? string.Empty : Session["_PIF_NAME"].ToString();
                }
                catch
                {
                    X.Redirect("~/Information/Info_index.aspx"); 
                    //_NotificationShow_TimeOut();
                    return "test";
                }
            }
            set
            {
                Session.Add("_PIF_NAME", value);
            }
        }

        public string _PIF_SX//性别
        {
            get
            {
                try
                {
                    return Session["_PIF_SX"] == null ? string.Empty : Session["_PIF_SX"].ToString();
                }
                catch
                {
                    X.Redirect("~/Information/Info_index.aspx");
                    //_NotificationShow_TimeOut();
                    return "test";
                }
            }
            set
            {
                Session.Add("_PIF_SX", value);
            }
        }

        public string _UserID//登入者
        {
            get
            {
                return Session["_USER_ID"] == null ? string.Empty : Session["_USER_ID"].ToString();
                //return "testuser";
            }
            set
            {
                Session.Add("_USER_ID", value);
            }
            
        }

        public string _USER_NAME//"经治医生
        {
            get
            {
                try
                {
                    return Session["_USER_NAME"] == null ? string.Empty : Session["_USER_NAME"].ToString();
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                Session.Add("_USER_NAME", value);
            }
        }

        public string _PatDocName//"经治医生
        {
            get
            {
                try
                {
                    //return Session["_USER_NAME"] == null ? string.Empty : Session["_USER_NAME"].ToString();
                    string sql = "SELECT a.pif_name, a.pif_docname ";
                    sql += " from pat_info a ";
                    sql += " where  a.pif_id = '" + _PAT_ID + "' ";
                    DataTable dt = db.Query(sql);
                    if (dt.Rows.Count == 0)
                        return "";
                    else
                        return dt.Rows[0]["pif_docname"].ToString();//姓名
                }
                catch
                {
                    return "";
                }
            }
            set
            {
                Session.Add("_PatDocName", value);
            }
        }

        public void _NotificationShow_TimeOut()
        {
            _NotificationShow("连线已逾时。");
        }

        public void _NotificationShow(string myMessage)
        {
            Notification.Show(new NotificationConfig
            {
                Title = "系统信息",
                Icon = Ext.Net.Icon.Accept,
                Html = myMessage
            });
        }

        public void _ErrorMsgShow(string myMessage)
        {
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "错误信息",
                Message = myMessage,
                Width = 300,
                Buttons = MessageBox.Button.OK
            });
        }

        public void _EmptyFieldShow(string myMessage)
        {
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "栏位输入提示",
                Message = "「" + myMessage + "」 不可空白",
                Width = 300,
                Buttons = MessageBox.Button.OK
            });
        }

        protected string _Request(string request_str)
        {
            try
            {
                return Request[request_str].ToString();
            }
            catch
            {
                return "";
            }
        }
                
        public void SetComboBoxItem(ComboBox cc, DataTable dd, Boolean bb, string sNAME, string sCODE)
        {
            Ext.Net.ListItem litem;
            if (cc == null)
                return;
            cc.Items.Clear();
            if (bb == true)
            {
                litem = new Ext.Net.ListItem("　", " ");
                cc.Items.Add(litem);
            }
            for (int i = 0; i < dd.Rows.Count; i++)
            {
                litem = new Ext.Net.ListItem(dd.Rows[i][sNAME].ToString(), dd.Rows[i][sCODE].ToString());
                cc.Items.Add(litem);
            }
            cc.GetStore().DataBind();
            //cc.DataBind();
        }

        public string GetComboBoxText(ComboBox cc)
        {
            return (cc.SelectedItem == null) ? string.Empty : cc.SelectedItem.Text.ToString();
        }

        public string GetComboBoxValue(ComboBox cc)
        {
            string value="";
            if (cc.SelectedItem.Value != null)
            {
                value = (cc.SelectedItem == null) ? string.Empty : cc.SelectedItem.Value.ToString();
            }
            return value;
        }

        public void SetComboBoxValue(ComboBox cc, string vv, Boolean bb)
        {
            Boolean b = false;
            for (int i = 0; i < cc.Items.Count; i++)
                if ((cc.Items[i].Text == vv) || (cc.Items[i].Value == vv))
                {
                    cc.Select(i);
                    cc.Text = cc.Items[i].Text;
                    b = true;
                    break;
                }
            if ((b == false) && (bb == true))
                cc.Select(0);
        }
        
        public string _Get_YMD(string idate)
        {
            if (idate.Trim() == "")
            {
                return "";
            }
            else
            {
                idate = idate.Replace("GMT+0800", "").Replace("(台北標準時間)", "");
                DateTime t = Convert.ToDateTime(idate);
                return string.Format("{0:yyyy-MM-dd}", t);
            }
        }

        public string _Get_YMD2(string idate)
        {
            if (idate.Trim() == "")
            {
                return "";
            }
            else
            {
                idate = idate.Replace("GMT+0800", "").Replace("(台北標準時間)", "");
                DateTime t = Convert.ToDateTime(idate);
                string s = string.Format("{0:yyyy-MM-dd}", t);
                s = s.Replace("0001-01-01", "");
                return s;
            }
        }

        public string _Get_Cht_YMD(string idate)
        {
            if (idate.Trim() == "")
            {
                return "";
            }
            else
            {
                idate = idate.Replace("GMT+0800", "").Replace("(台北標準時間)", "");
                DateTime t = Convert.ToDateTime(idate);
                return string.Format("{0:yyyy年MM月dd日}", t);
            }
        }

        public string _Get_Cht_YMDHM(string idate)
        {
            if (idate.Trim() == "")
            {
                return "";
            }
            else
            {
                idate += ":00";
                idate = idate.Replace("GMT+0800", "").Replace("(台北標準時間)", "");
                DateTime t = Convert.ToDateTime(idate);
                return string.Format("{0:yyyy年MM月dd日hh時mm分}", t);
            }
        }

        protected void _ClearRadio(string radname)
        {
            _Clear_Radio(base.Form.Controls,radname);
        }

        private void _Clear_Radio(System.Web.UI.ControlCollection objcol, string radname)
        {
            try
            {
                foreach (object obj in objcol)
                {
                    System.Type itype = obj.GetType();
                    if (itype.Namespace == "Ext.Net" || itype.Namespace == "System.Web.UI.HtmlControls")
                    {
                        switch (itype.Name)
                        {
                            case "HtmlForm":
                                System.Web.UI.HtmlControls.HtmlForm htmlform = (System.Web.UI.HtmlControls.HtmlForm)obj;
                                _Clear_Radio(htmlform.Controls,radname);
                                break;
                            case "Viewport":
                                Ext.Net.Viewport viewport = (Ext.Net.Viewport)obj;
                                _Clear_Radio(viewport.Controls,radname);
                                break;
                            case "FormPanel":
                                Ext.Net.FormPanel formpanel = (Ext.Net.FormPanel)obj;
                                _Clear_Radio(formpanel.Controls, radname);
                                break;
                            case "Panel":
                                Ext.Net.Panel panel = (Ext.Net.Panel)obj;
                                _Clear_Radio(panel.Controls, radname);
                                break;
                            case "TabPanel":
                                Ext.Net.TabPanel tabpanel = (Ext.Net.TabPanel)obj;
                                _Clear_Radio(tabpanel.Controls, radname);
                                break;
                            case "Container":
                                Ext.Net.Container container = (Ext.Net.Container)obj;
                                _Clear_Radio(container.Controls, radname);
                                break;
                            case "FieldSet":
                                Ext.Net.FieldSet fieldset = (Ext.Net.FieldSet)obj;
                                _Clear_Radio(fieldset.Controls, radname);
                                break;
                            case "ContentContainer":
                                Ext.Net.ContentContainer contentcontainer = (Ext.Net.ContentContainer)obj;
                                _Clear_Radio(contentcontainer.Controls, radname);
                                break;
                            case "RadioGroup":
                                Ext.Net.RadioGroup radioGroup = (Ext.Net.RadioGroup)obj;
                                _Clear_Radio(radioGroup.Controls, radname);
                                break;
                            case "Radio":
                                Ext.Net.Radio radio = (Ext.Net.Radio)obj;

                                string[] s = radname.Split(';');
                                for (int i =0; i <s.Length; i++)
                                {
                                    if (radio.Name ==s[i])
                                    {
                                        radio.Value =false;
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

        //清除畫面時列外物件的ID
        private string[] _exclude_obj;

        #region 清空畫面上資料
        /// <summary>
        /// 清空畫面上資料 Ext.Net TextField TextArea DateField TimeField ComboBox Checkbox Hidden
        /// </summary>
        /// <param name="objcol">除外物件 string[] _exclude_obj</param>
        /// 
        protected void _ClearForm(string[] exclude_arr)
        {
            _exclude_obj = exclude_arr;
            _Clear_Form(base.Form.Controls);
        }

        protected void _ClearForm()
        {
            _exclude_obj = new string[] { };
            _Clear_Form(base.Form.Controls);
        }

        private void _Clear_Form(System.Web.UI.ControlCollection objcol)
        {
            try
            {
                foreach (object obj in objcol)
                {
                    System.Type itype = obj.GetType();
                    if (itype.Namespace == "Ext.Net" || itype.Namespace == "System.Web.UI.HtmlControls")
                    {
                        switch (itype.Name)
                        {
                            case "HtmlForm":
                                System.Web.UI.HtmlControls.HtmlForm htmlform = (System.Web.UI.HtmlControls.HtmlForm)obj;
                                _Clear_Form(htmlform.Controls);
                                break;
                            case "Viewport":
                                Ext.Net.Viewport viewport = (Ext.Net.Viewport)obj;
                                _Clear_Form(viewport.Controls);
                                break;
                            case "FormPanel":
                                Ext.Net.FormPanel formpanel = (Ext.Net.FormPanel)obj;
                                _Clear_Form(formpanel.Controls);
                                break;
                            case "Panel":
                                Ext.Net.Panel panel = (Ext.Net.Panel)obj;
                                _Clear_Form(panel.Controls);
                                break;
                            case "GridPanel":
                                Ext.Net.GridPanel gridPanel = (Ext.Net.GridPanel)obj;
                                if (_exclude_obj.Contains(gridPanel.ID) == false)
                                {
                                    //Store istore= gridPanel.GetStore();
                                    //istore.RemoveAll();
                                    //istore.LoadData(_cv.GetDataArray_Add_Empty(new DataTable()));
                                }
                                break;
                            case "TabPanel":
                                Ext.Net.TabPanel tabpanel = (Ext.Net.TabPanel)obj;
                                _Clear_Form(tabpanel.Controls);
                                break;
                            case "Container":
                                Ext.Net.Container container = (Ext.Net.Container)obj;
                                _Clear_Form(container.Controls);
                                break;
                            case "FieldSet":
                                Ext.Net.FieldSet fieldset = (Ext.Net.FieldSet)obj;
                                _Clear_Form(fieldset.Controls);
                                break;
                            case "ContentContainer":
                                Ext.Net.ContentContainer contentcontainer = (Ext.Net.ContentContainer)obj;
                                _Clear_Form(contentcontainer.Controls);
                                break;
                            case "RadioGroup":
                                Ext.Net.RadioGroup radioGroup = (Ext.Net.RadioGroup)obj;
                                _Clear_Form(radioGroup.Controls);
                                break;
                            case "TextField":
                                Ext.Net.TextField textfield = (Ext.Net.TextField)obj;
                                if (_exclude_obj.Contains(textfield.ID) == false)
                                    textfield.Text = "";
                                break;
                            case "TextArea":
                                Ext.Net.TextArea textarea = (Ext.Net.TextArea)obj;
                                if (_exclude_obj.Contains(textarea.ID) == false)
                                    textarea.Text = "";
                                break;
                            case "DateField":
                                Ext.Net.DateField datefield = (Ext.Net.DateField)obj;
                                if (_exclude_obj.Contains(datefield.ID) == false)
                                    datefield.Text = "";
                                break;
                            case "TimeField":
                                Ext.Net.TimeField timefield = (Ext.Net.TimeField)obj;
                                if (_exclude_obj.Contains(timefield.ID) == false)
                                    timefield.RawText = "";
                                break;
                            case "ComboBox":
                                Ext.Net.ComboBox combobox = (Ext.Net.ComboBox)obj;
                                if (_exclude_obj.Contains(combobox.ID) == false)
                                {
                                    combobox.SelectedItems.Clear();
                                    combobox.Text = "";
                                }
                                break;
                            case "Checkbox":
                                Ext.Net.Checkbox checkbox = (Ext.Net.Checkbox)obj;
                                if (_exclude_obj.Contains(checkbox.ID) == false)
                                    checkbox.SetRawValue(false);
                                break;
                            case "Hidden":
                                Ext.Net.Hidden hidden = (Ext.Net.Hidden)obj;
                                if (_exclude_obj.Contains(hidden.ID) == false)
                                    hidden.Text = "";
                                break;
                            case "Radio":
                                Ext.Net.Radio radio = (Ext.Net.Radio)obj;

                                radio.Value =false;
                                break;
                            case "MultiCombo":
                                Ext.Net.MultiCombo multiCombo = (Ext.Net.MultiCombo)obj;
                                if (_exclude_obj.Contains(multiCombo.ID) == false)
                                {
                                    multiCombo.SelectedItems.Clear();
                                    multiCombo.RawText = "";
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
        #endregion

        #region 储存资料
        protected void _zInfo_Save(string tableName,string pat_id,string info_date,string txt_1,string section,string bed_no)
         {
            if (tableName != "")
            {
                try
                {
                    string info_user = _UserID;
                    info_date = _Get_YMD(info_date);
                    if (info_date == "0001-01-01")
                    {
                        info_date = _Get_YMD(DateTime.Now.ToString());
                    }

                    string sql;
                    if (tableName == "zinfo_a_01" || tableName == "zinfo_a_02" || tableName == "zinfo_f_01" || 
                        tableName == "zinfo_f_02" || tableName == "zinfo_f_011" || tableName == "zinfo_f_012" ||
                        tableName == "zinfo_f_04" || tableName == "zinfo_f_05" || tableName == "zinfo_f_06_alasamo" ||
                        tableName == "zinfo_h_01" || tableName == "zinfo_a_08")
                    {
                        sql = "SELECT * FROM " + tableName + " WHERE pat_id='" + pat_id + "'";
                    }
                    else
                    {
                        sql = "SELECT * FROM " + tableName + " WHERE pat_id='" + pat_id + "' and info_date='" + info_date + "'";
                    }
                    DataTable dt = db.Query(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "INSERT INTO " + tableName + " (pat_id,info_date,info_user,txt_1,section,bed_no) ";
                        sql += " VALUES ('" + pat_id + "','" + info_date + "','" + info_user + "','" + txt_1 + "','" + section + "','" + bed_no + "')";
                        db.Excute(sql);
                    }

                    _haTable = new Hashtable();
                    _objTable = new Hashtable();

                    _Get_Field(base.Form.Controls);

                    sql = "UPDATE " + tableName + " SET info_user='" + info_user + "',info_date='" + info_date + "',                      ";
                    for (int i = 4 ; i < dt.Columns.Count; i++)
                    {
                        sql += dt.Columns[i].ColumnName + "='" + _FieldValue(dt.Columns[i].ColumnName) + "',";
                    }

                    sql = sql.Substring(0, sql.Length - 1);

                    if (tableName == "zinfo_a_01" || tableName == "zinfo_a_02" || tableName == "zinfo_f_01" || 
                        tableName == "zinfo_f_02" || tableName == "zinfo_f_011" || tableName == "zinfo_f_012" ||
                        tableName == "zinfo_f_04" || tableName == "zinfo_f_05" || tableName == "zinfo_f_06_alasamo" ||
                        tableName == "zinfo_h_01" || tableName == "zinfo_a_08")
                    {
                        sql += " WHERE pat_id='" + pat_id + "'";
                    }
                    else
                    {
                        sql += " , txt_1='" + txt_1 + "', section='" + section + "',  bed_no='" + bed_no + "'  ";
                        sql += " WHERE pat_id='" + pat_id + "' AND info_date='" + info_date + "'";
                    }
                    db.Excute(sql);
                    _NotificationShow("信息储存完成");
                }
                catch (Exception ex)
                {
                    _ErrorMsgShow(ex.Message.ToString());
                }
            }
        }
		
        protected void _zInfo_Save(string tableName, string pat_id, string info_date)
        {
            if (tableName != "")
            {
                try
                {
                    string info_user = _UserID;
                    info_date = _Get_YMD(info_date);
                    if (info_date == "0001-01-01")
                    {
                        info_date = _Get_YMD(DateTime.Now.ToString());
                    }

                    string sql;
                    if (tableName == "zinfo_a_01" || tableName == "zinfo_a_02" || tableName == "zinfo_f_01" ||
                        tableName == "zinfo_f_02" || tableName == "zinfo_f_011" || tableName == "zinfo_f_012" || tableName == "dialysis_water1" || tableName == "dialysis_water2" ||
                        tableName == "zinfo_f_04" || tableName == "zinfo_f_05" || tableName == "zinfo_h_01" || 
                        tableName == "zinfo_a_08" || tableName == "zinfo_a_09")
                    {
                        sql = "SELECT * FROM " + tableName + " WHERE pat_id='" + pat_id + "'";
                    }
                    else
                    {
                        sql = "SELECT * FROM " + tableName + " WHERE pat_id='" + pat_id + "' AND info_date='" + info_date + "'";
                    }
                    DataTable dt = db.Query(sql);
                    if (dt.Rows.Count == 0)
                    {
                        sql = "INSERT INTO " + tableName + " (pat_id,info_date,info_user) ";
                        sql += " VALUES ('" + pat_id + "','" + info_date + "','" + info_user + "')";
                        db.Excute(sql);
                    }

                    _haTable = new Hashtable();
                    _objTable = new Hashtable();

                    _Get_Field(base.Form.Controls);

                    sql = "UPDATE " + tableName + " SET info_user='" + info_user + "',info_date='" + info_date + "',";
                    for (int i = 3; i < dt.Columns.Count; i++)
                    {
                        if (_FieldValue(dt.Columns[i].ColumnName) == "NULL")
                            sql += dt.Columns[i].ColumnName + "=" + _FieldValue(dt.Columns[i].ColumnName) + ",";
                        else
                            sql += dt.Columns[i].ColumnName + "='" + _FieldValue(dt.Columns[i].ColumnName) + "',";
                    }

                    sql = sql.Substring(0, sql.Length - 1);

                    if (tableName == "zinfo_a_01" || tableName == "zinfo_a_02" || tableName == "zinfo_f_01" ||
                        tableName == "zinfo_f_02" || tableName == "zinfo_f_011" || tableName == "zinfo_f_012" || tableName == "dialysis_water1" || tableName == "dialysis_water2" ||
                        tableName == "zinfo_f_04" || tableName == "zinfo_f_05" || tableName == "zinfo_h_01" || 
                        tableName == "zinfo_a_08" || tableName == "zinfo_a_09")
                    {
                        sql += " WHERE pat_id='" + pat_id + "'";
                    }
                    else
                    {
                        sql += " WHERE pat_id='" + pat_id + "' AND info_date='" + info_date + "'";
                    }

                    db.Excute(sql);

                    _NotificationShow("信息储存完成");
                }
                catch (Exception ex)
                {
                    _ErrorMsgShow(ex.Message.ToString());
                }
            }
        }

        private string _FieldValue(string colname)
        {
            string rtn_value ="";
            switch (colname.Substring(0,3))
            {
                case "dat":
                    if (_haTable.ContainsKey(colname))
                    {
                        rtn_value = _Get_YMD(_haTable[colname].ToString());
                        if (rtn_value == "0001-01-01")
                        {
                            rtn_value = "";
                        }
                    }
                    break;
                case "txt":
                    if (_haTable.ContainsKey(colname))
                    {
                        rtn_value = _haTable[colname].ToString();
                    }
                    break;
                case "are":
                    if (_haTable.ContainsKey(colname))
                    {
                        rtn_value = _haTable[colname].ToString();
                    }
                    break;
                case "cbo":
                    //if (_haTable.ContainsKey(colname))
                    //{
                    //    rtn_value = _haTable[colname].ToString();
                    //}
                    break;
                case "opt":
                    for(int i=1; i <100; i++)
                    {
                        string ikey =colname + "_" + i.ToString();
                        if (_haTable.ContainsKey(ikey))
                        {
                            if ((bool)_haTable[ikey] == true)
                            {
                                rtn_value = i.ToString();
                                break;
                            }
                        }     
                    }
                    if (rtn_value == "")
                        rtn_value = "0";
                    break;
                case "chk":
                    for (int i = 1; i < 100; i++)
                    {
                        string ikey = colname + "_" + i.ToString();
                        if (_haTable.ContainsKey(ikey))
                        {
                            if ((bool)_haTable[ikey] == true)
                            {
                                rtn_value +="1";
                            }
                            else
                            {
                                rtn_value += "0";
                            }
                        }
                    }
                    break;
                case "num":
                    if (_haTable.ContainsKey(colname))
                    {
                        rtn_value = _haTable[colname].ToString();
                        if (rtn_value == "" || rtn_value == "0")
                            rtn_value = "NULL";
                    }
                    break;
            }
            rtn_value =rtn_value.Replace("'","''");
            return rtn_value;
        }

        protected void _Get_Field(System.Web.UI.ControlCollection objcol)
        {
            try
            {
                foreach (object obj in objcol)
                {
                    System.Type itype = obj.GetType();
                    if (itype.Namespace == "Ext.Net" || itype.Namespace == "System.Web.UI.HtmlControls")
                    {
                        switch (itype.Name)
                        {
                            case "HtmlForm":
                                System.Web.UI.HtmlControls.HtmlForm htmlform = (System.Web.UI.HtmlControls.HtmlForm)obj;
                                _Get_Field(htmlform.Controls);
                                break;
                            case "Viewport":
                                Ext.Net.Viewport viewport = (Ext.Net.Viewport)obj;
                                _Get_Field(viewport.Controls);
                                break;
                            case "FormPanel":
                                Ext.Net.FormPanel formpanel = (Ext.Net.FormPanel)obj;
                                _Get_Field(formpanel.Controls);
                                break;
                            case "Panel":
                                Ext.Net.Panel panel = (Ext.Net.Panel)obj;
                                _Get_Field(panel.Controls);
                                break;
                            case "TabPanel":
                                Ext.Net.TabPanel tabpanel = (Ext.Net.TabPanel)obj;
                                _Get_Field(tabpanel.Controls);
                                break;
                            case "Container":
                                Ext.Net.Container container = (Ext.Net.Container)obj;
                                _Get_Field(container.Controls);
                                break;
                            case "FieldSet":
                                Ext.Net.FieldSet fieldset = (Ext.Net.FieldSet)obj;
                                _Get_Field(fieldset.Controls);
                                break;
                            case "ContentContainer":
                                Ext.Net.ContentContainer contentcontainer = (Ext.Net.ContentContainer)obj;
                                _Get_Field(contentcontainer.Controls);
                                break;
                            case "RadioGroup":
                                Ext.Net.RadioGroup radioGroup = (Ext.Net.RadioGroup)obj;
                                _Get_Field(radioGroup.Controls);
                                break;
                            case "TextField":
                                Ext.Net.TextField textfield = (Ext.Net.TextField)obj;
                                _haTable.Add(textfield.ID,textfield.Text);
                                _objTable.Add(textfield.ID, textfield);
                                break;
                            case "TextArea":
                                Ext.Net.TextArea textareafield = (Ext.Net.TextArea)obj;
                                _haTable.Add(textareafield.ID, textareafield.Text);
                                _objTable.Add(textareafield.ID, textareafield);
                                break;
                            case "Checkbox":
                                Ext.Net.Checkbox checkbox = (Ext.Net.Checkbox)obj;
                                _haTable.Add(checkbox.ID,checkbox.Checked);
                                _objTable.Add(checkbox.ID,checkbox);
                                break;
                            case "Radio":
                                Ext.Net.Radio radio = (Ext.Net.Radio)obj;
                                _haTable.Add(radio.ID,radio.Value);
                                _objTable.Add(radio.ID,radio);
                                break;
                            case "DateField":
                                Ext.Net.DateField datefield = (Ext.Net.DateField)obj;
                                _haTable.Add(datefield.ID,datefield.Text);
                                _objTable.Add(datefield.ID,datefield);
                                break;
                            case "":

                                break;

                        }
                    }
                }
            }
            catch
            {

            }
        }

        #endregion

        #region 刪除資料
        protected void _zInfo_Confirm_Delete(string sel_info_date)
        {
            X.Msg.Confirm("Confirm", "确定删除信息??", new MessageBoxButtonsConfig
            {
                Yes = new MessageBoxButtonConfig
                {
                    Handler = "#{DirectMethods}.zInfo_delete('" + sel_info_date + "')",
                    Text ="是",
                },
                No = new MessageBoxButtonConfig
                {
                    Text ="否",
                }
            }).Show();
        }

        protected void _zInfo_Delete(string tableName,string pat_id,string info_date)
        {
            if (tableName != "")
            {
                try
                {

                    string sql = "delete from " + tableName + " where pat_id='" + pat_id + "' and info_date='" + info_date + "'";
                    db.Excute(sql);
                    _NotificationShow("讯息删除完成。");

                }
                catch (Exception ex)
                {
                    _ErrorMsgShow(ex.Message.ToString());
                }
            }
        }

        #endregion

        #region 秀资料 
        protected void _zInfo_Show(string tableName,string pat_id,string info_date)
        {
            if (tableName != "")
            {
                string sql;
                if (tableName == "zinfo_f_01" || tableName == "zinfo_f_02" || tableName == "zinfo_f_04" || tableName == "zinfo_f_05" ||
                    tableName == "zinfo_f_012" || tableName == "zinfo_h_01" || 
                    tableName == "zinfo_a_01" || tableName == "zinfo_a_02" || 
                    tableName == "zinfo_a_08" || tableName == "zinfo_a_09")
                {
                    sql = "select * from " + tableName + " where pat_id='" + pat_id + "'";
                }
                else
                {
                    sql = "select * from " + tableName + " where pat_id='" + pat_id + "' and info_date='" + info_date + "'";
                }


                _haTable = new Hashtable();
                _objTable = new Hashtable();
                _Get_Field(base.Form.Controls);

                DataTable dt = db.Query(sql);

                if (dt.Rows.Count == 0)
                {
                    try
                    {
                        Ext.Net.DateField datefield = (Ext.Net.DateField)_objTable["info_date"];
                        info_date = _Get_YMD(DateTime.Now.ToString());
                        datefield.Text = info_date;
                        sql = "select * from " + tableName + " where pat_id='" + pat_id + "' and info_date='" + info_date + "'";
                        dt = db.Query(sql);
                        if (dt.Rows.Count > 0)
                        {
                            _NotificationShow("已存在 日期 :" + info_date + " 之信息，保存後将覆盖该日期之信息。");
                        }
                    }
                    catch
                    {

                    }
                    return;
                }


                for (int i = 1; i < dt.Columns.Count; i++)
                {
                    string colName = dt.Columns[i].ColumnName;
                    switch (colName.Substring(0, 3))
                    {
                        case "dat":
                            if (_objTable.ContainsKey(colName))
                            {
                                Ext.Net.DateField datefield = (Ext.Net.DateField)_objTable[colName];
                                datefield.Text = dt.Rows[0][i].ToString();
                            }
                            break;
                        case "txt":
                            if (_objTable.ContainsKey(colName))
                            {
                                Ext.Net.TextField textfield = (Ext.Net.TextField)_objTable[colName];
                                textfield.Text = dt.Rows[0][i].ToString();
                            }
                            break;
                        case "are":
                            if (_objTable.ContainsKey(colName))
                            {
                                Ext.Net.TextArea textfield = (Ext.Net.TextArea)_objTable[colName];
                                textfield.Text = dt.Rows[0][i].ToString();
                            }
                            break;
                        case "cbo":
                            break;
                        case "opt":
                            if (dt.Rows[0][i].ToString() != "0")
                            {
                                colName += "_" + dt.Rows[0][i].ToString();
                                if (_objTable.ContainsKey(colName))
                                {
                                    Ext.Net.Radio radio = (Ext.Net.Radio)_objTable[colName];
                                    radio.Value = true;
                                }
                            }
                            break;
                        case "chk":
                            string chk_value = dt.Rows[0][i].ToString();
                            for (int posi = 1; posi <= chk_value.Length; posi++)
                            {
                                string x = chk_value.Substring(posi - 1, 1);
                                string colname_num = colName + "_" + posi.ToString();
                                if (x == "1")
                                {
                                    if (_objTable.ContainsKey(colname_num))
                                    {
                                        Ext.Net.Checkbox checkbox = (Ext.Net.Checkbox)_objTable[colname_num];
                                        checkbox.Checked = true;
                                    }
                                }
                                else
                                {
                                    if (_objTable.ContainsKey(colname_num))
                                    {
                                        Ext.Net.Checkbox checkbox = (Ext.Net.Checkbox)_objTable[colname_num];
                                        checkbox.SetRawValue(false);
                                    }
                                }
                            }
                            break;
                        case "num":
                            if (_objTable.ContainsKey(colName))
                            {
                                Ext.Net.TextField textfield = (Ext.Net.TextField)_objTable[colName];
                                textfield.Text = dt.Rows[0][i].ToString();
                                if (textfield.Text == "0.000")
                                    textfield.Text = "";
                            }
                            break;
                        default:
                            if (colName.Equals("info_date"))
                            {
                                if (_objTable.ContainsKey(colName))
                                {
                                    Ext.Net.DateField datefield = (Ext.Net.DateField)_objTable[colName];
                                    datefield.Text = dt.Rows[0][i].ToString();
                                }
                            }

                            if (colName.Equals("section"))
                            {
                                if (_objTable.ContainsKey(colName))
                                {
                                    Ext.Net.TextField textfield = (Ext.Net.TextField)_objTable[colName];
                                    textfield.Text = dt.Rows[0][i].ToString();
                                }
                            }

                            if (colName.Equals("bed_no"))
                            {
                                if (_objTable.ContainsKey(colName))
                                {
                                    Ext.Net.TextField textfield = (Ext.Net.TextField)_objTable[colName];
                                    textfield.Text = dt.Rows[0][i].ToString();
                                }
                            }
                            break;
                    }
                }
            }
        }
        #endregion

        #region 画出html.table

        protected void _Fill_Html_Table(string table_name,string pat_id, ArrayList acol,string title_str)
        {
            string sql ="select * from " + table_name + " where pat_id='" + pat_id + "'";
            DataTable dt = db.Query(sql);

            _haTable = new Hashtable();
            _objTable = new Hashtable();
            _Get_Field(base.Form.Controls);

            string ihtml;
            ihtml = "<p>" + title_str + "</p>";
            ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?editmode=edit&editmode2=add';return false;\">添加</a><br>";
            ihtml += "<link href=\"CSS.css\" rel=\"stylesheet\" type=\"text/css\">";
            ihtml += "<div id=\"Table2\" align=left style=\"OVERFLOW: auto; HEIGHT: 450\">";
            ihtml +="<table>";
            ihtml += "<tr class='trheadcolor'>";
            ihtml += "<td><p>操作</p></td>";
            for (int i =0 ; i <acol.Count ; i++)
            {
                string[] a = (string[])acol[i];
                ihtml += "<td><p>" + a[0] + "</p></td>";
            }
            ihtml +="</tr>";

            foreach (DataRow irow in dt.Rows)
            {
                ihtml += "<tr>";
                ihtml += "<td><p>";
                ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?editmode=show&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 浏览 </a>";
                ihtml +="  ";
                ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?&editmode=edit&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 修改 </a>";
                ihtml += "  ";
                ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?&editmode=delete&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 删除 </a>";
                ihtml += "  ";
                ihtml += "</p></td>";

                for (int i =0 ; i < acol.Count ; i++)
                {
                    string[] selcol = (string[])acol[i];
                    ihtml += "<td><p>";
                    ihtml += _Get_Column_Value(selcol,irow);
                    ihtml += "</p></td>";
                }
                ihtml += "</tr>";
            }
            ihtml +="</table>";
            ihtml += "</div>";
            Response.Write(ihtml);
        }

        private string _Get_Column_Value(string[] sel, DataRow selrow)
        {
            string rtn_str ="";
            for (int i=1; i <sel.Length ; i++)
            {
                string colName = sel[i];
                if (colName.Equals("info_date"))
                {
                    rtn_str = selrow[colName].ToString() + "、";
                }
                if (colName.Equals("section"))
                {
                    rtn_str = selrow[colName].ToString() + "、";
                }
                if (colName.Equals("bed_no"))
                {
                    rtn_str = selrow[colName].ToString() + "、";
                }
                else
                {
                    switch (colName.Substring(0,3))
                    {
                        case "dat":
                            rtn_str += selrow[colName].ToString() + "、";
                            break;
                        case "txt":
                            rtn_str += selrow[colName].ToString() + "、";
                            break;
                        case "are":
                            rtn_str += selrow[colName].ToString() + "、";
                            break;
                        case "num":
                            rtn_str += selrow[colName].ToString() + "、";
                            break;
                        case "cbo":
                            break;
                        case "opt":
                            string optVal = selrow[colName].ToString();
                            string optsel = colName + "_" + optVal.ToString();
                            if (_haTable.ContainsKey(optsel))
                            {
                                Ext.Net.Radio radio = (Ext.Net.Radio)_objTable[optsel];
                                rtn_str += radio.BoxLabel + "、";
                            }
                            break;
                        case "chk":
                            string chkVal = selrow[colName].ToString();
                            for (int x = 1; x <= chkVal.Length ; x++)
                            {
                                if (chkVal.Substring(x-1,1).Equals("1"))
                                {
                                    string chksel = colName + "_" + x.ToString();
                                    if ( _haTable.ContainsKey(chksel))
                                    {
                                        Ext.Net.Checkbox checkbox = (Ext.Net.Checkbox)_objTable[chksel];
                                        rtn_str += checkbox.BoxLabel + "、";
                                    }
                                }
                            }
                            break;
                    }
                }
            }

            if (rtn_str.Length >0)
            {
                rtn_str = rtn_str.Substring(0,rtn_str.Length -1);
            }
            return rtn_str;
        }
        #endregion

        #region 画出html.table 與query一樣

        protected void _Fill_Table(string sql, ArrayList acol)
        {

            
            DataTable dt = db.Query(sql);

            _haTable = new Hashtable();
            _objTable = new Hashtable();

            _Get_Field(base.Form.Controls);

            string ihtml;

            ihtml = "<br>";
            //ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?editmode=edit&editmode2=add';return false;\">添加</a><br>";
            ihtml += "<link href=\"CSS.css\" rel=\"stylesheet\" type=\"text/css\">";
            ihtml += "<div id=\"Table2\" align=left style=\"OVERFLOW: auto; HEIGHT: 450\">";
            ihtml += "<table border=\"1\" >";

            ihtml += "<tr>";
            //ihtml += "<td  >操作</td>";
            for (int i = 0; i < acol.Count; i++)
            {
                string[] a = (string[])acol[i];
                ihtml += "<td  >" + a[0] + "</td>";
            }
            ihtml += "</tr>";

            foreach (DataRow irow in dt.Rows)
            {
                ihtml += "<tr>";
                //ihtml += "<td >";
                //ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?editmode=show&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 浏览 </a>";
                //ihtml += "  ";
                //ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?&editmode=edit&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 修改 </a>";
                //ihtml += "  ";
                //ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?&editmode=delete&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 删除 </a>";
                //ihtml += "  ";
                //ihtml += "</td>";

                for (int i = 0; i < acol.Count; i++)
                {
                    string[] selcol = (string[])acol[i];
                    ihtml += "<td >";
                    //ihtml += "RTRYR";
                    ihtml += _Column_Value(selcol, irow);
                    ihtml += "</td>";
                }

                ihtml += "</tr>";
            }


            ihtml += "</table>";
            ihtml += "</div>";
            Response.Write(ihtml);
        }

        private string _Column_Value(string[] sel, DataRow selrow)
        {
            string rtn_str = "";
            for (int i = 1; i < sel.Length; i++)
            {
                string colName = sel[i];
                rtn_str = selrow[colName].ToString();
                break;
            }
            return rtn_str;
        }
        #endregion

        //public static Size ScreenResolution
        //{  
        //    get    
        //    {
        //        return (Size)HttpContext.Current.Session["ScreenResolution"];
        //    }
        //    set    
        //    {
        //        HttpContext.Current.Session["ScreenResolution"] = value; 
        //    }
        //}

        //[WebMethod()]
        //public static void setResolution(int width, int height)
        //{
        //    ScreenResolution = new Size(width, height);
        //}

        #region 設定單一 ComboBox 中的選項
        protected void SetSingleCombo(ComboBox cbo, string[] GroupName)
        {
            Ext.Net.ListItem litem1;
            for (int i = 0; i < GroupName.Length; i++)
            {
                litem1 = new Ext.Net.ListItem(GroupName[i], GroupName[i]);
                cbo.Items.Add(litem1);
            }
        }
        #endregion
    }
}