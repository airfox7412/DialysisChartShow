using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.IO;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_02_01 : BaseForm
    {
        private string _TableName = "zinfo_b_01";
        private string sel_info_date = "";
        private Hashtable _haTable;
        private Hashtable _objTable;

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
                        //this.BuildSet1();
                        break;
                    case "show":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        btn_clear.Visible = false;
                        btn_save.Visible = false;
                        FormPanel1.Title = FormPanel1.Title + "-浏览";
                        ShowPic();
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
                        ShowPic();
                        break;
                    case "delete":
                        _zInfo_Confirm_Delete(sel_info_date);
                        break;
                }
            }
        }

        private void ShowPic()
        {
            string sSQL = "SELECT txt_9 FROM " + _TableName + " ";
            sSQL += "WHERE pat_id='" + _PAT_ID + "' AND info_date='" + sel_info_date + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                txt_9.Text = dt.Rows[0]["txt_9"].ToString();
                Image1.ImageUrl = "../patimages/dialysis/" + txt_9.Text;
            }
            dt.Dispose();
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            try
            {
                _zInfo_Save(_TableName, _PAT_ID, info_date.Text); //zinfo_b_01
                X.Redirect("Dialysis_02_01.aspx?editmode=list");
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow("保存失败!"+ ex.Message);
            }
        }


        #region 上傳照片
        protected void GetPatImg(object sender, DirectEventArgs e)
        {
            if (this.UploadImage.HasFile)
            {
                string strImg = string.Empty;
                if (UploadImage.PostedFile != null && UploadImage.PostedFile.ContentLength > 0)
                {
                    strImg = this.UploadImage.PostedFile.FileName;
                    string strExt = System.IO.Path.GetExtension(strImg).ToLower();
                    if (strExt != ".jpg" && strExt != ".jepg" && strExt != ".bmp" && strExt != ".gif")
                    {
                        Common._NotificationShow("抱歉仅支持.jpg，jepg，bmp，gif");
                        return;
                    }
                }
                fileUpload(strImg);
            }
            else
            {
                Common._NotificationShow("请选择照片");
            }
        }
        #endregion

        #region 上传图片处理
        public void fileUpload(string strFilePath)
        {
            string strPictureName = string.Empty;//上传后的图片名，以当前时间为文件名，确保文件名没有重复           
            if (!string.IsNullOrEmpty(strFilePath))
            {
                string strMapPath = "";
                try
                {
                    string strFileName = System.IO.Path.GetFileName(strFilePath);// System.IO.Path.GetExtension(strFilePath).ToLower();
                    strPictureName = _Get_YMD2(info_date.Text) + "_" + _PAT_ID + "_" + strFileName;
                    try
                    {
                        strMapPath = Server.MapPath("../") + "patimages\\dialysis\\";
                    }
                    catch (Exception ex)
                    {
                        Common._ErrorMsgShow("照片上传失败:" + ex.Message);
                    }
                    string strPath = strMapPath + strPictureName;
                    UploadImage.PostedFile.SaveAs(strPath);
                    Image1.ImageUrl = "../patimages/dialysis/" + strPictureName;
                    txt_9.Text = strPictureName;
                    //string sql = "SELECT * FROM " + _TableName + " ";
                    //sql += "WHERE pat_id='" + _PAT_ID + "' AND info_date='" + _Get_YMD2(info_date.Text) + "'";
                    //DataTable dt = db.Query(sql);
                    //if (dt.Rows.Count > 0)
                    //{
                    //    sql = "UPDATE " + _TableName + " SET pictureUrl='" + strPictureName + "' ";
                    //    sql += "WHERE pat_id='" + _PAT_ID + "' AND info_date='" + _Get_YMD2(info_date.Text) + "'";
                    //}
                    //else
                    //{
                    //    sql = "INSERT INTO " + _TableName + "(pat_id, info_date, pictureUrl) VALUES (";
                    //    sql += "'" + _PAT_ID + "', ";
                    //    sql += "'" + _Get_YMD2(info_date.Text) + "', ";
                    //    sql += "'" + strPictureName + "') ";
                    //}

                    //db.Excute(sql);
                    Common._NotificationShow("照片上传完成!");
                }
                catch (Exception ex)
                {
                    Common._ErrorMsgShow("上传照片失败:" + ex.Message);
                }
            }
            else
            {
                string path = Directory.GetCurrentDirectory();// 用于获得应用程序当前工作目录
                Common._NotificationShow("请选择照片");
            }
        }
        #endregion

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_02_01.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_02_01.aspx?editmode=list");
        }
        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }
        private void ShowList()
        {
            string[] col_0 = { "使用时期", "info_date" };
            string[] col_1 = { "血管通路类型", "opt_1" };
            string[] col_2 = { "血管通路位置（左-右）", "opt_2" };
            string[] col_3 = { "血管通路位置", "opt_3" };
            string[] col_4 = { "中心静脉置管方法", "opt_4" };
            string[] col_5 = { "血管通路位置", "opt_5" };
            string[] col_6 = { "改变原因", "chk_6" };
            string[] col_7 = { "其它改变原因", "txt_7" };
            string[] col_8 = { "其它血管通路类型", "txt_8" };

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

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "血管通路列表");
        }

        private void AddField(ModelField field)
        {
            if (X.IsAjaxRequest)
            {
                this.Store1.AddField(field);
            }
            else
            {
                this.Store1.Model[0].Fields.Add(field);
            }
        }

        public class Project
        {
            public Project(string field0, string field1, string field2, string field3, string field4, string field5, string field6, string field7, string field8)
            {
                info_date = field0;
                opt_1 = field1;
                opt_2 = field2;
                opt_3 = field3;
                opt_4 = field4;
                opt_5 = field5;
                opt_6 = field6;
                opt_7 = field7;
                opt_8 = field8;
            }

            public string info_date { get; set; }
            public string opt_1 { get; set; }
            public string opt_2 { get; set; }
            public string opt_3 { get; set; }
            public string opt_4 { get; set; }
            public string opt_5 { get; set; }
            public string opt_6 { get; set; }
            public string opt_7 { get; set; }
            public string opt_8 { get; set; }
        }

        private void BindSet1()
        {
            string[] col_0 = { "使用时期", "info_date" };
            string[] col_1 = { "血管通路类型", "opt_1" };
            string[] col_2 = { "血管通路位置（左-右）", "opt_2" };
            string[] col_3 = { "血管通路位置", "opt_3" };
            string[] col_4 = { "中心静脉置管方法", "opt_4" };
            string[] col_5 = { "血管通路位置", "opt_5" };
            string[] col_6 = { "改变原因", "chk_6" };
            string[] col_7 = { "其它改变原因", "txt_7" };
            string[] col_8 = { "其它血管通路类型", "txt_8" };

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

            ArrayList acol2 = new ArrayList();
            var datasource = new List<Project>(); 
            _haTable = new Hashtable();
            _objTable = new Hashtable();
            _Get_Field(base.Form.Controls);

            string sql = "select * from " + _TableName + " where pat_id='" + _PAT_ID + "'";
            DataTable dt = db.Query(sql);
            foreach (DataRow irow in dt.Rows)
            {
                for (int i = 0; i < acol.Count; i++)
                {
                    string[] selcol = (string[])acol[i];
                    acol2.Add(_Get_Column_Value(selcol, irow));
                }
                datasource.Add(new Project(acol2[0].ToString(), acol2[1].ToString(), acol2[2].ToString(), acol2[3].ToString(), acol2[4].ToString(), acol2[5].ToString(), acol2[6].ToString(), acol2[7].ToString(), acol2[8].ToString()));
            }
            Store1.DataSource = datasource;
            Store1.DataBind();
        }

        private void BuildSet1()
        {
            Panel1.Visible = true;
            if (X.IsAjaxRequest)
            {
                this.Store1.RemoveFields();
            }

            string[] col_0 = { "使用时期", "info_date" };
            string[] col_1 = { "血管通路类型", "opt_1" };
            string[] col_2 = { "血管通路位置（左-右）", "opt_2" };
            string[] col_3 = { "血管通路位置", "opt_3" };
            string[] col_4 = { "中心静脉置管方法", "opt_4" };
            string[] col_5 = { "血管通路位置", "opt_5" };
            string[] col_6 = { "改变原因", "chk_6" };
            string[] col_7 = { "其它改变原因", "txt_7" };
            string[] col_8 = { "其它血管通路类型", "txt_8" };

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
            int i;
            for (i = 0; i < acol.Count; i++)
            {
                string[] a = (string[])acol[i];
                this.AddField(new ModelField(a[1]));
            }
            this.Store1.RebuildMeta();
            this.BindSet1();

            for (i = 0; i < acol.Count; i++)
            {
                string[] a = (string[])acol[i];
                this.GridPanel1.ColumnModel.Columns.Add(new Column { DataIndex = a[1], Text = a[0], Width=200 });
            }

            if (X.IsAjaxRequest)
            {
                this.GridPanel1.Reconfigure();
            }
        }

        protected void RefreshDataSet(object sender, StoreReadDataEventArgs e)
        {
            this.BuildSet1();
        }

        protected void btn_Add_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Dialysis_02_01.aspx?editmode=edit&editmode2=add");
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
    }
}