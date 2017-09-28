using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using System.IO;
using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_04_02 : BaseForm
    {
        private string _TableName = "zinfo_d_02";
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
                        Container1.Hidden = true;
                        Container2.Hidden = true;
                        Container3.Hidden = true;
                        Container4.Hidden = true;
                        show();
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
                        show();
                        break;
                    case "delete":
                        _zInfo_Confirm_Delete(sel_info_date);
                        break;
                }
            }
        }

        private void show()
        {
            if (this.txt_32.Text != "")
            {
                this.Image1.ImageUrl = "../upload/" + this.txt_32.Text;
                this.Image1.Hidden = false;
            }
            if (this.txt_34.Text != "")
            {
                this.Image2.ImageUrl = "../upload/" + this.txt_34.Text;
                this.Image2.Hidden = false;
            }
            if (this.txt_36.Text != "")
            {
                this.Image3.ImageUrl = "../upload/" + this.txt_36.Text;
                this.Image3.Hidden = false;
            }
            if (this.txt_38.Text != "")
            {
                this.Image4.ImageUrl = "../upload/" + this.txt_38.Text;
                this.Image4.Hidden = false;
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            if (_Request("editmode2") == "add")
            {
                string info_date = "";
                info_date = DateTime.Now.ToString("yyyy-MM-dd");
                _zInfo_Save(_TableName, _PAT_ID, info_date);
                X.Redirect("Dialysis_04_02.aspx?editmode=list");
            }
            else
            {
                sel_info_date = _Request("sel_info_date");
                _zInfo_Save(_TableName, _PAT_ID, sel_info_date);
                X.Redirect("Dialysis_04_02.aspx?editmode=list");
            }
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            //_ClearForm();
            X.Redirect("Dialysis_04_02.aspx?editmode=list");
        }
        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_04_02.aspx?editmode=list");
        }
        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_04_02.aspx?editmode=list");
        }
        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }
        private void ShowList()
        {
            string[] col_0 = { "建档日期", "info_date" };
            string[] col_1 = { "检查日期", "dat_1" };
            string[] col_2 = { "胸部X线检查-诊断结果", "are_33" };
            //string[] col_2 = { "心胸比", "num_2" };
            //string[] col_3 = { "诊断", "chk_3" };
            //string[] col_4 = { "其它请说明", "txt_4" };
            string[] col_3 = { "检查日期", "dat_5" };
            string[] col_4 = { "心电图检查-诊断结果", "are_35" };
            //string[] col_6 = { "诊断", "chk_6" };
            //string[] col_7 = { "其它请说明", "txt_7" };
            string[] col_5 = { "检查日期", "dat_8" };
            string[] col_6 = { "超声心动图检查-诊断结果", "are_37" };
            //string[] col_9 = { "左室内径", "num_9" };
            //string[] col_10 = { "左室舒张末期内径", "num_10" };
            //string[] col_11 = { "室间隔厚度", "num_11" };
            //string[] col_12 = { "左室后壁厚度", "num_12" };
            //string[] col_13 = { "射血分数", "num_13" };
            //string[] col_14 = { "心包积液", "opt_14" };
            //string[] col_15 = { "左房扩大", "opt_15" };
            //string[] col_16 = { "右房扩大", "opt_16" };
            //string[] col_17 = { "左室扩大", "opt_17" };
            //string[] col_18 = { "右室扩大", "opt_18" };
            //string[] col_19 = { "左室增厚", "opt_19" };
            //string[] col_20 = { "室间隔增厚", "opt_20" };
            //string[] col_21 = { "左室舒张功能减低", "opt_21" };
            //string[] col_22 = { "二尖瓣钙化", "opt_22" };
            //string[] col_23 = { "二尖瓣返流", "opt_23" };
            //string[] col_24 = { "三尖瓣返流", "opt_24" };
            //string[] col_25 = { "主动脉返流", "opt_25" };
            string[] col_7 = { "检查日期", "dat_26" };
            string[] col_8 = { "颈动脉超声-诊断结果", "are_39" };
            //string[] col_27 = { "内膜增厚", "opt_27" };
            //string[] col_28 = { "中膜增厚", "opt_28" };
            //string[] col_29 = { "斑块形成", "opt_29" };
            //string[] col_30 = { "狭窄", "opt_30" };
            //string[] col_31 = { "闭塞", "opt_31" };

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
            //acol.Add(col_9);
            //acol.Add(col_10);
            //acol.Add(col_11);
            //acol.Add(col_12);
            //acol.Add(col_13);
            //acol.Add(col_14);
            //acol.Add(col_15);
            //acol.Add(col_16);
            //acol.Add(col_17);
            //acol.Add(col_18);
            //acol.Add(col_19);
            //acol.Add(col_20);
            //acol.Add(col_21);
            //acol.Add(col_22);
            //acol.Add(col_23);
            //acol.Add(col_24);
            //acol.Add(col_25);
            //acol.Add(col_26);
            //acol.Add(col_27);
            //acol.Add(col_28);
            //acol.Add(col_29);
            //acol.Add(col_30);
            //acol.Add(col_31);


            _Fill_Html_Table(_TableName, _PAT_ID, acol, "辅助检查列表");
        }

        protected void Upload_Click1(object sender, DirectEventArgs e)
        {
            string tpl = "档案名称：{0}<br/>大小：{1} bytes";
            string UploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"];
            string UploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"];
            string Path = Server.MapPath(UploadFilePath);
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            if (this.FileUploadField1.HasFile)
            {
                HttpPostedFile file = this.FileUploadField1.PostedFile;
                string fn = System.IO.Path.GetFileName(file.FileName);
                Path = Path + fn;

                if (File.Exists(Path) && !Checkbox1.Checked)
                {
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.ERROR,
                        Title = "警告",
                        Message = "档案已经存在，请重新命名"
                    });
                }
                else
                {
                    file.SaveAs(Path);
                    this.txt_32.Text = fn; // file.FileName;
                    this.Image1.ImageUrl = "../upload/" + fn;
                    this.Image1.Hidden = false;
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Title = "成功",
                        Message = string.Format(tpl, this.FileUploadField1.PostedFile.FileName, this.FileUploadField1.PostedFile.ContentLength)
                    });
                }
            }
            else
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "警告",
                    Message = "没有档案可以上传"
                });
            }
        }

        protected void Upload_Click2(object sender, DirectEventArgs e)
        {
            string tpl = "档案名称：{0}<br/>大小：{1} bytes";
            string UploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"];
            string UploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"];
            string Path = Server.MapPath(UploadFilePath);
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            if (this.FileUploadField2.HasFile)
            {
                HttpPostedFile file = this.FileUploadField2.PostedFile;
                string fn = System.IO.Path.GetFileName(file.FileName);
                Path = Path + fn;

                if (File.Exists(Path) && !Checkbox2.Checked)
                {
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.ERROR,
                        Title = "警告",
                        Message = "档案已经存在，请重新命名"
                    });
                }
                else
                {
                    file.SaveAs(Path);
                    this.txt_34.Text = fn;
                    this.Image2.ImageUrl = "../upload/" + fn;
                    this.Image2.Hidden = false;
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Title = "成功",
                        Message = string.Format(tpl, this.FileUploadField2.PostedFile.FileName, this.FileUploadField2.PostedFile.ContentLength)
                    });
                }
            }
            else
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "警告",
                    Message = "没有档案可以上传"
                });
            }
        }

        protected void Upload_Click3(object sender, DirectEventArgs e)
        {
            string tpl = "档案名称：{0}<br/>大小：{1} bytes";
            string UploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"];
            string UploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"];
            string Path = Server.MapPath(UploadFilePath);
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            if (this.FileUploadField3.HasFile)
            {
                HttpPostedFile file = this.FileUploadField3.PostedFile;
                string fn = System.IO.Path.GetFileName(file.FileName);
                Path = Path + fn;

                if (File.Exists(Path) && !Checkbox3.Checked)
                {
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.ERROR,
                        Title = "警告",
                        Message = "档案已经存在，请重新命名"
                    });
                }
                else
                {
                    file.SaveAs(Path);
                    this.txt_36.Text = fn;
                    this.Image3.ImageUrl = "../upload/" + fn;
                    this.Image3.Hidden = false;
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Title = "成功",
                        Message = string.Format(tpl, this.FileUploadField3.PostedFile.FileName, this.FileUploadField3.PostedFile.ContentLength)
                    });
                }
            }
            else
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "警告",
                    Message = "没有档案可以上传"
                });
            }
        }

        protected void Upload_Click4(object sender, DirectEventArgs e)
        {
            string tpl = "档案名称：{0}<br/>大小：{1} bytes";
            string UploadFilePath = ConfigurationManager.AppSettings["UploadFilePath"];
            string UploadFileUrl = ConfigurationManager.AppSettings["UploadFileUrl"];
            string Path = Server.MapPath(UploadFilePath);
            if (!Directory.Exists(Path))
                Directory.CreateDirectory(Path);

            if (this.FileUploadField4.HasFile)
            {
                HttpPostedFile file = this.FileUploadField4.PostedFile;
                string fn = System.IO.Path.GetFileName(file.FileName);
                Path = Path + fn;

                if (File.Exists(Path) && !Checkbox4.Checked)
                {
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.ERROR,
                        Title = "警告",
                        Message = "档案已经存在，请重新命名"
                    });
                }
                else
                {
                    file.SaveAs(Path);
                    this.txt_38.Text = fn;
                    this.Image4.ImageUrl = "../upload/" + fn;
                    this.Image4.Hidden = false;
                    X.Msg.Show(new MessageBoxConfig
                    {
                        Buttons = MessageBox.Button.OK,
                        Icon = MessageBox.Icon.INFO,
                        Title = "成功",
                        Message = string.Format(tpl, this.FileUploadField4.PostedFile.FileName, this.FileUploadField4.PostedFile.ContentLength)
                    });
                }
            }
            else
            {
                X.Msg.Show(new MessageBoxConfig
                {
                    Buttons = MessageBox.Button.OK,
                    Icon = MessageBox.Icon.ERROR,
                    Title = "警告",
                    Message = "没有档案可以上传"
                });
            }
        }

    }
}