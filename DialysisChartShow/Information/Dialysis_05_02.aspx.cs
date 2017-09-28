using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_05_02 : BaseForm
    {
        private string _TableName = "zinfo_e_02";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                _PAT_ID = Session["PAT_ID"].ToString();
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
                        btn_clear.Visible = false;
                        btn_save.Visible = false;
                        FormPanel1.Title = FormPanel1.Title + "-浏览";
                        show_pic();
                        break;
                    case "edit":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        setView();
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
                            show_pic();
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
            X.Redirect("Dialysis_05_02.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
            Container1.Hidden = false;
            Container11.Hidden = false;
            Container2.Hidden = false;
            Container12.Hidden = false;
            Container3.Hidden = false;
            Container13.Hidden = false;
            Container4.Hidden = false;
            Container14.Hidden = false;
            Container5.Hidden = true;
            Label11.Hidden = false;
            Label12.Hidden = false;
            Label13.Hidden = false;
            Label14.Hidden = false;
            Container21.Hidden = false;
            Container22.Hidden = false;
            txt_6.Text = "";
            Btn_picA.ImageUrl = "";
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_05_02.aspx?editmode=list");
        }

        protected void Btn_pic1_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            Container21.Hidden = false;
            Container22.Hidden = true;
            txt_6.Text = "../Styles/PIC_1.jpg";
            Btn_picA.ImageUrl = txt_6.Text;
            txt_12.Text = "桡动脉-头静脉端端吻合";
        }

        protected void Btn_pic2_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            Container21.Hidden = false;
            Container22.Hidden = true;
            txt_6.Text = "../Styles/PIC_2.jpg";
            Btn_picA.ImageUrl = txt_6.Text;
            txt_12.Text = "桡动脉-头静脉端侧吻合";
        }

        protected void Btn_pic3_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            Container21.Hidden = true;
            Container22.Hidden = false;
            txt_6.Text = "../Styles/PIC_3.jpg";
            Btn_picA.ImageUrl = txt_6.Text;
            txt_12.Text = "肱动脉-头静脉端侧吻合(1)";
        }

        protected void Btn_pic4_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            Container21.Hidden = true;
            Container22.Hidden = false;
            txt_6.Text = "../Styles/PIC_4.jpg";
            Btn_picA.ImageUrl = txt_6.Text;
            txt_12.Text = "肱动脉-头静脉端侧吻合(2)";
        }

        protected void Btn_pic5_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            Container21.Hidden = true;
            Container22.Hidden = false;
            txt_6.Text = "../Styles/PIC_5.jpg";
            Btn_picA.ImageUrl = txt_6.Text;
            txt_12.Text = "肱动脉-正中静脉端侧吻合(近)";
        }
        protected void Btn_pic6_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            Container21.Hidden = true;
            Container22.Hidden = false;
            txt_6.Text = "../Styles/PIC_6.jpg";
            Btn_picA.ImageUrl = txt_6.Text;
            txt_12.Text = "肱动脉-正中静脉端侧吻合(远)";
        }
        protected void Btn_pic7_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            Container21.Hidden = true;
            Container22.Hidden = false;
            txt_6.Text = "../Styles/PIC_7.jpg";
            Btn_picA.ImageUrl = txt_6.Text;
            txt_12.Text = "肱动脉-正中静脉侧侧吻合";
        }
        protected void Btn_pic8_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            Container21.Hidden = true;
            Container22.Hidden = false;
            txt_6.Text = "../Styles/PIC_8.jpg";
            Btn_picA.ImageUrl = txt_6.Text;
            txt_12.Text = "";
        }

        protected void Btn_picA_Click(object sender, DirectEventArgs e)
        {
            Container1.Hidden = false;
            Container11.Hidden = false;
            Container2.Hidden = false;
            Container12.Hidden = false;
            Container3.Hidden = false;
            Container13.Hidden = false;
            Container4.Hidden = false;
            Container14.Hidden = false;
            Container5.Hidden = true;
            Label11.Hidden = false;
            Label12.Hidden = false;
            Label13.Hidden = false;
            Label14.Hidden = false;
            Container21.Hidden = false;
            Container22.Hidden = false;
            txt_6.Text = "";
            Btn_picA.ImageUrl = "";
            txt_12.Text = "";
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_05_02.aspx?editmode=list");
        }
        private void ShowList()
        {
            string[] col_0 = { "建档日期", "info_date" };
            string[] col_1 = { "手术操作记录", "are_7" };
            string[] col_2 = { "手术操作记录", "are_1" };
            string[] col_3 = { "住院病情记录", "are_2" };
            string[] col_4 = { "特殊病情记录及治疗", "are_3" };
            //string[] col_4 = { "特殊治疗", "are_4" };
            string[] col_5 = { "抢救记录", "are_5" };
            string[] col_6 = { "是否廔管重建", "opt_8" };
            string[] col_7 = { "重建原因", "opt_9" };

            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);
            acol.Add(col_3);
            acol.Add(col_4);
            acol.Add(col_5);
            acol.Add(col_6);
            acol.Add(col_7);



            _Fill_Html_Table(_TableName, _PAT_ID, acol, "血管通路记录列表");
        }

        protected void show_pic()
        {
            db = new tools.DBMysql();
            string sql = "select a.txt_6 ";
            sql+="FROM zinfo_e_02 a ";
            sql+="WHERE a.pat_id = '"+_PAT_ID+"' ";
            sql += "and a.info_date = '" + sel_info_date + "' ";

            DataTable dt = db.Query(sql);

            Container1.Hidden = true;
            Container11.Hidden = true;
            Container2.Hidden = true;
            Container12.Hidden = true;
            Container3.Hidden = true;
            Container13.Hidden = true;
            Container4.Hidden = true;
            Container14.Hidden = true;
            Container5.Hidden = false;
            Label11.Hidden = true;
            Label12.Hidden = true;
            Label13.Hidden = true;
            Label14.Hidden = true;
            txt_6.Text = dt.Rows[0]["txt_6"].ToString();
            Btn_picA.ImageUrl = txt_6.Text;
            switch (txt_6.Text)
            {
                case "../Styles/PIC_1.jpg":
                    Container21.Hidden = false;
                    Container22.Hidden = true;
                    break;
                case "../Styles/PIC_2.jpg":
                    Container21.Hidden = false;
                    Container22.Hidden = true;
                    break;
                case "../Styles/PIC_3.jpg":
                    Container21.Hidden = true;
                    Container22.Hidden = false;
                    break;
                case "../Styles/PIC_4.jpg":
                    Container21.Hidden = true;
                    Container22.Hidden = false;
                    break;
                case "../Styles/PIC_5.jpg":
                    Container21.Hidden = true;
                    Container22.Hidden = false;
                    break;
                case "../Styles/PIC_6.jpg":
                    Container21.Hidden = true;
                    Container22.Hidden = false;
                    break;
                case "../Styles/PIC_7.jpg":
                    Container21.Hidden = true;
                    Container22.Hidden = false;
                    break;
                case "../Styles/PIC_8.jpg":
                    Container21.Hidden = true;
                    Container22.Hidden = false;
                    break;
                default:
                    Container21.Hidden = true;
                    Container22.Hidden = true;
                    break;
            }
        }

        protected void ii(object sender, DirectEventArgs e)
        {
            if (opt_8_1.Checked == true)
            {
                Container8.Hidden = false;
                Container9.Hidden = false;
            }
            else
            {
                Container8.Hidden = true;
                Container9.Hidden = true;
            }
        }
        protected void jj(object sender, DirectEventArgs e)
        {
            if (opt_9_7.Checked == true)
            {
                txt_10.Hidden = false;
            }
            else
            {
                txt_10.Hidden = true;
            }
        }

        protected void setView()
        {
            if (opt_8_1.Checked == true)
            {
                Container8.Hidden = false;
                Container9.Hidden = false;
            }
            else
            {
                Container8.Hidden = true;
                Container9.Hidden = true;
            }
            if (opt_9_7.Checked == true)
            {
                txt_10.Hidden = false;
            }
            else
            {
                txt_10.Hidden = true;
            }
        }
    }
}