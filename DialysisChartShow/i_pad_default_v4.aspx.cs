using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
//using System.Web.UI.HtmlControls;

namespace Dialysis_Chart_Show
{
    public partial class i_pad_default_v4 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Area.Text = "A";
            if (!X.IsAjaxRequest)
            {
                try
                {
                    DBMysql db = new DBMysql();

                    string sPAD_HEAD = "Styles/mark.png";
                    DataTable dtPAD_HEAD = db.Query("SELECT *  FROM general_setup WHERE  genst_code='IPAD_HEAD'");
                    if (dtPAD_HEAD.Rows.Count > 0)
                    {
                        sPAD_HEAD = dtPAD_HEAD.Rows[0]["genst_desc"].ToString();
                        //sPAD_HEAD = Server.MapPath(sPAD_HEAD);
                        Image2.ImageUrl = sPAD_HEAD;
                    }
                }
                catch (Exception ex)
                {
                    //_ErrorMsgShow(ex.Message.ToString());
                }
            }

            DateTime datetime = DateTime.Now;
            Tex_Datetime.Text = datetime.ToString("yyyy/MM/dd HH:mm:ss");

            int iOrn = 0;
            daytyp.Text = (0 == (iOrn = datetime.DayOfWeek.GetHashCode()) ? 7 : iOrn).ToString(); // 一般程式(Basic,C,Delphi...) 週末指星期六,其實星期日是每週的第一日。
            /*
                        switch (datetime.DayOfWeek)
                        {
                            case DayOfWeek.Monday:
                                daytyp.Text = "1";
                                break;
                            case DayOfWeek.Tuesday:
                                daytyp.Text = "2";
                                break;
                            case DayOfWeek.Wednesday:
                                daytyp.Text = "3";
                                break;
                            case DayOfWeek.Thursday:
                                daytyp.Text = "4";
                                break;
                            case DayOfWeek.Friday:
                                daytyp.Text = "5";
                                break;
                            case DayOfWeek.Saturday:
                                daytyp.Text = "6";
                                break;
                            case DayOfWeek.Sunday:
                                daytyp.Text = "7";
                                break;
                        }
            */

        }

       
        protected void Btn_3F_Click(object sender, DirectEventArgs e)
        {
            if (Floor.Text == "03")
                Floor.Text = "";
            else
                Floor.Text = "03";
            ShowPanel();
        }

        protected void Btn_2F_Click(object sender, DirectEventArgs e)
        {
            if (Floor.Text == "02")
                Floor.Text = "";
            else
                Floor.Text = "02";
            ShowPanel();
        }

        protected void Btn_Morning_Click(object sender, DirectEventArgs e)
        {
            if (Time.Text == "001")
                Time.Text = "";
            else
                Time.Text = "001";
            ShowPanel();
        }

        protected void Btn_Afternoon_Click(object sender, DirectEventArgs e)
        {
            if (Time.Text == "002")
                Time.Text = "";
            else
                Time.Text = "002";
            ShowPanel();
        }

        protected void Btn_Night_Click(object sender, DirectEventArgs e)
        {
            if (Time.Text == "003")
                Time.Text = "";
            else
                Time.Text = "003";
            ShowPanel();
        }

        private void ShowPanel()
        {
            if ((Time.Text == "") || (Floor.Text == ""))
            {
                Panel2F.Hidden = true;
                Panel3F_1.Hidden = true;
                Panel3F_2.Hidden = true;
            }
            else
            {
                switch (Floor.Text)
                {
                    case "02":
                        Panel2F.Hidden = false;
                        Button1.Hidden = false;
                        Button2.Hidden = false;
                        Panel3F_1.Hidden = true;
                        Panel3F_2.Hidden = true;
                        //Button3.Hidden = true;
                        //Button4.Hidden = true;
                        //Button5.Hidden = true;
                        //Button6.Hidden = true;
                        //Button7.Hidden = true;
                        //Button8.Hidden = true;
                        //Button9.Hidden = true;
                        //Button10.Hidden = true;
                        //Button11.Hidden = true;
                        //Button12.Hidden = true;
                        //Button13.Hidden = true;
                        //Button14.Hidden = true;
                        //Button15.Hidden = true;
                        //Button16.Hidden = true;
                        //Button17.Hidden = true;
                        //Button18.Hidden = true;
                        //Button19.Hidden = true;
                        //Button20.Hidden = true;
                        //Button21.Hidden = true;
                        Button22.Hidden = true;
                        Button23.Hidden = true;
                        Button24.Hidden = true;
                        Button25.Hidden = true;
                        Button26.Hidden = true;
                        Button27.Hidden = true;
                        Button28.Hidden = true;
                        Button29.Hidden = true;
                        Button30.Hidden = true;
                        Button31.Hidden = true;
                        //Button32.Hidden = true;
                        //Button33.Hidden = true;
                        //Button34.Hidden = true;
                       break;
                    case "03":
                        Panel2F.Hidden = true;
                        Button1.Hidden = true;
                        Button2.Hidden = true;
                        Panel3F_1.Hidden = false;
                        Panel3F_2.Hidden = false;
                        //Button3.Hidden = false;
                        //Button4.Hidden = false;
                        //Button5.Hidden = false;
                        //Button6.Hidden = false;
                        //Button7.Hidden = false;
                        //Button8.Hidden = false;
                        //Button9.Hidden = false;
                        //Button10.Hidden = false;
                        //Button11.Hidden = false;
                        //Button12.Hidden = false;
                        //Button13.Hidden = false;
                        //Button14.Hidden = false;
                        //Button15.Hidden = false;
                        //Button16.Hidden = false;
                        //Button17.Hidden = false;
                        //Button18.Hidden = false;
                        //Button19.Hidden = false;
                        //Button20.Hidden = false;
                        //Button21.Hidden = false;
                        Button22.Hidden = false;
                        Button23.Hidden = false;
                        Button24.Hidden = false;
                        Button25.Hidden = false;
                        Button26.Hidden = false;
                        Button27.Hidden = false;
                        Button28.Hidden = false;
                        Button29.Hidden = false;
                        Button30.Hidden = false;
                        Button31.Hidden = false;
                        //Button32.Hidden = false;
                        //Button33.Hidden = false;
                        //Button34.Hidden = false;
                        break;
                }
            }
        }

       
    }
}