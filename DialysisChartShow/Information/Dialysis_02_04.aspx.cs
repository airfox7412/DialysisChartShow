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
    public partial class Dialysis_02_04 : BaseForm
    {
        private string _TableName = "zinfo_b_04";
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
            X.Redirect("Dialysis_02_04.aspx?editmode=list");
        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }
        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_02_04.aspx?editmode=list");
        }
        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_02_04.aspx?editmode=list");
        }
        private void ShowList()
        {
            string[] col_0 = { "检查日期", "info_date" };
            string[] col_1 = { "身高", "num_1" };
            string[] col_2 = { "体重", "num_2" };
            string[] col_3 = { "BMI", "num_3" };
            string[] col_4 = { "体表面积", "num_4" };
            string[] col_5 = { "透前尿素", "num_5" };
            string[] col_6 = { "透后尿素", "num_6" };
            string[] col_7 = { "透析时间", "num_7" };
            string[] col_8 = { "超滤量", "num_8" };
            string[] col_9 = { "URR", "num_9" };
            string[] col_10 = { "spKt/V", "num_10" };


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


            _Fill_Html_Table(_TableName, _PAT_ID, acol, "透析充分性列表");
        }

        protected void text_BMISurface(object sender, EventArgs e)
        {
            if (num_1.Text != "" && num_2.Text != "")
            {
                double h = double.Parse(num_1.Text);
                double w = double.Parse(num_2.Text);
                double h1 = h / 100;
                double bmi = w / Math.Pow(h1, 2);//BMI計算
                num_3.Text = bmi.ToString();


                double surface = Math.Pow(h, 0.725) * Math.Pow(w, 0.425) * 0.007184 ;//體表面積計算
                num_4.Text = surface.ToString();
                //en.wikipedia.org/wiki/Body_surface_area
            }
        }

        protected void text_urr(object sender, EventArgs e)
        {
            if (num_2.Text != "" && num_5.Text != "" && num_6.Text != "" && num_7.Text != "" && num_8.Text != "")
            { 
                //Ln表自然對數

                //R表透析後BUN/透析前BUN
                double bun_B = double.Parse(num_5.Text);//透析前BUN
                double bun_A = double.Parse(num_6.Text);//透析後
                double r = bun_A / bun_B;

                //t表透析時間（hours）
                double t = double.Parse(num_7.Text);//h

                //UF表超過濾之體積（liters）
                double uf = double.Parse(num_8.Text);//L

                //W表病人透析後之體重（Kg）
                double w = double.Parse(num_2.Text);

                // URR = 100 * ( 1 - r )
                double urr = 100 * (1 - r);
                num_9.Text = urr.ToString();

                //Kt/V自然對數公式（natural logarithm formula）：

                //Kt/V = -Ln ( R - 0.008 * t ) + ( 4 - 3.5 * R ) * UF/W
                double spKtV = -Math.Log(r - 0.008 * t) + (4 - 3.5 * r) * uf / w;
                num_10.Text = spKtV.ToString();
            }
        }

    }
}