using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Web.UI.HtmlControls;
namespace Dialysis_Chart_Show
{
    public partial class i_pad_default_new : System.Web.UI.Page //BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!X.IsAjaxRequest)
            {

                //Floor.Text = "";
                //Area.Text = "";
                //Time.Text = "";
                //daytyp.Text = "";
                //Bed_number.Text = "";

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
            if (datetime.DayOfWeek == DayOfWeek.Monday)
            {
                daytyp.Text = "1";
            }
            else if (datetime.DayOfWeek == DayOfWeek.Tuesday)
            {
                daytyp.Text = "2";
            }
            else if (datetime.DayOfWeek == DayOfWeek.Wednesday)
            {
                daytyp.Text = "3";
            }
            else if (datetime.DayOfWeek == DayOfWeek.Thursday)
            {
                daytyp.Text = "4";
            }
            else if (datetime.DayOfWeek == DayOfWeek.Friday)
            {
                daytyp.Text = "5";
            }
            else if (datetime.DayOfWeek == DayOfWeek.Saturday)
            {
                daytyp.Text = "6";
            }
            else if (datetime.DayOfWeek == DayOfWeek.Sunday)
            {
                daytyp.Text = "7";
            }
*/
        }

        private void HideRoom()
        {
            Btn_A.Hidden = true;
            Btn_B.Hidden = true;
            Btn_C.Hidden = true;
            Btn_D.Hidden = true;
            Btn_VIP.Hidden = true;
            Btn_E.Hidden = true;
            Btn_F.Hidden = true;
            Btn_G.Hidden = true;
        }

        protected void Btn_3F_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            //Floor.Text = "03";
            //Btn_A.Hidden = true;
            //Btn_B.Hidden = true;
            //Btn_C.Hidden = true;
            //Btn_D.Hidden = true;
            //Btn_VIP.Hidden = true;
            //Btn_E.Hidden = false;
            //Btn_F.Hidden = false;
            //Btn_G.Hidden = false;
            HideRoom();
            if (Floor.Text != "03")
            {
                Floor.Text = "03";
                Btn_E.Hidden = false;
                Btn_F.Hidden = false;
                Btn_G.Hidden = false;
            }
            else
            {
                Floor.Text = "";
            }
        }

        protected void Btn_5F_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            //Floor.Text = "05";
            //Btn_A.Hidden = false;
            //Btn_B.Hidden = false;
            //Btn_C.Hidden = false;
            //Btn_D.Hidden = false;
            //Btn_VIP.Hidden = false;
            //Btn_E.Hidden = true;
            //Btn_F.Hidden = true;
            //Btn_G.Hidden = true;
            HideRoom();
            if (Floor.Text != "05")
            {
                Floor.Text = "05";
                Btn_A.Hidden = false;
                Btn_B.Hidden = false;
                Btn_C.Hidden = false;
                Btn_D.Hidden = false;
                Btn_VIP.Hidden = false;
            }
            else
            {
                Floor.Text = "";
            }
        }

        protected void Btn_A_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            selectArea();
            Area.Text = "A";
            Button1.Hidden = false;
            Button2.Hidden = false;
            Button3.Hidden = false;
            Button4.Hidden = false;
            Button5.Hidden = false;
            Button6.Hidden = false;
            Button7.Hidden = false;
            Button8.Hidden = false;
            Button9.Hidden = false;
            Button10.Hidden = false;
            Button11.Hidden = false;
            Button11_1.Hidden = true;
            Button12.Hidden = true;
            Button13.Hidden = true;
            Button14.Hidden = true;
            Button15.Hidden = true;
            Button16.Hidden = true;
            Button17.Hidden = true;
            Button18.Hidden = true;
            Button19.Hidden = true;
            Button20.Hidden = true;
            Button21.Hidden = true;
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
            Button32.Hidden = true;
            Button33.Hidden = true;
            Button34.Hidden = true;
            Button35.Hidden = true;
            Button36.Hidden = true;
            Button37.Hidden = true;
            Button38.Hidden = true;
            Button39.Hidden = true;
            Button40.Hidden = true;
            Button41.Hidden = true;
            Button42.Hidden = true;
            Button43.Hidden = true;
            Button44.Hidden = true;
            Button45.Hidden = true;
            Button46.Hidden = true;
            Button46_1.Hidden = true;
            Button47.Hidden = true;
            Button48.Hidden = true;
            Button49.Hidden = true;
            Button50.Hidden = true;
            Button51.Hidden = true;
            Button51_1.Hidden = true;
            Button52.Hidden = true;
            Button53.Hidden = true;
            Button54.Hidden = true;
            Button55.Hidden = true;
            Button56.Hidden = true;
            Button57.Hidden = true;
            Button58.Hidden = true;

            Buttonv1.Hidden = true;
            Buttonv2.Hidden = true;
            Buttonv3.Hidden = true;
            Buttonv4.Hidden = true;

            Button81.Hidden = true;
            Button82.Hidden = true;
            Button83.Hidden = true;
            Button84.Hidden = true;
            Button85.Hidden = true;
            Button86.Hidden = true;
            Button87.Hidden = true;
            Button88.Hidden = true;
            Button89.Hidden = true;
            Button90.Hidden = true;
            Button91.Hidden = true;
            Button92.Hidden = true;
            Button93.Hidden = true;
            Button94.Hidden = true;
            Button95.Hidden = true;
            Button95_1.Hidden = true;
            Button96.Hidden = true;
            Button97.Hidden = true;
            Button98.Hidden = true;
            Button99.Hidden = true;
            Button100.Hidden = true;
            Button101.Hidden = true;
            Button102.Hidden = true;
            Button103.Hidden = true;
            Button104.Hidden = true;
            Button105.Hidden = true;
            Button106.Hidden = true;
            Button107.Hidden = true;
            Button108.Hidden = true;
            Button109.Hidden = true;
            Button110.Hidden = true;
            Button111.Hidden = true;
            Button112.Hidden = true;
            Button113.Hidden = true;
            Button114.Hidden = true;
            Button115.Hidden = true;
        }

        protected void Btn_B_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            selectArea();
            Area.Text = "B";
            Button1.Hidden = true;
            Button2.Hidden = true;
            Button3.Hidden = true;
            Button4.Hidden = true;
            Button5.Hidden = true;
            Button6.Hidden = true;
            Button7.Hidden = true;
            Button8.Hidden = true;
            Button9.Hidden = true;
            Button10.Hidden = true;
            Button11.Hidden = true;

            Button11_1.Hidden = false;
            Button12.Hidden = false;
            Button13.Hidden = false;
            Button14.Hidden = false;
            Button15.Hidden = false;
            Button16.Hidden = false;
            Button17.Hidden = false;
            Button18.Hidden = false;
            Button19.Hidden = false;
            Button20.Hidden = false;
            Button21.Hidden = false;
            Button22.Hidden = false;
            Button23.Hidden = false;
            Button24.Hidden = false;
            Button25.Hidden = false;
            Button26.Hidden = false;
            Button27.Hidden = false;
            Button28.Hidden = false;
            Button29.Hidden = false;
            Button30.Hidden = false;

            Button31.Hidden = true;
            Button32.Hidden = true;
            Button33.Hidden = true;
            Button34.Hidden = true;
            Button35.Hidden = true;
            Button36.Hidden = true;
            Button37.Hidden = true;
            Button38.Hidden = true;
            Button39.Hidden = true;
            Button40.Hidden = true;
            Button41.Hidden = true;
            Button42.Hidden = true;
            Button43.Hidden = true;
            Button44.Hidden = true;
            Button45.Hidden = true;
            Button46.Hidden = true;
            Button46_1.Hidden = true;
            Button47.Hidden = true;
            Button48.Hidden = true;
            Button49.Hidden = true;
            Button50.Hidden = true;
            Button51.Hidden = true;
            Button51_1.Hidden = true;
            Button52.Hidden = true;
            Button53.Hidden = true;
            Button54.Hidden = true;
            Button55.Hidden = true;
            Button56.Hidden = true;
            Button57.Hidden = true;
            Button58.Hidden = true;

            Buttonv1.Hidden = true;
            Buttonv2.Hidden = true;
            Buttonv3.Hidden = true;
            Buttonv4.Hidden = true;

            Button81.Hidden = true;
            Button82.Hidden = true;
            Button83.Hidden = true;
            Button84.Hidden = true;
            Button85.Hidden = true;
            Button86.Hidden = true;
            Button87.Hidden = true;
            Button88.Hidden = true;
            Button89.Hidden = true;
            Button90.Hidden = true;
            Button91.Hidden = true;
            Button92.Hidden = true;
            Button93.Hidden = true;
            Button94.Hidden = true;
            Button95.Hidden = true;
            Button95_1.Hidden = true;
            Button96.Hidden = true;
            Button97.Hidden = true;
            Button98.Hidden = true;
            Button99.Hidden = true;
            Button100.Hidden = true;
            Button101.Hidden = true;
            Button102.Hidden = true;
            Button103.Hidden = true;
            Button104.Hidden = true;
            Button105.Hidden = true;
            Button106.Hidden = true;
            Button107.Hidden = true;
            Button108.Hidden = true;
            Button109.Hidden = true;
            Button110.Hidden = true;
            Button111.Hidden = true;
            Button112.Hidden = true;
            Button113.Hidden = true;
            Button114.Hidden = true;
            Button115.Hidden = true;
        }

        protected void Btn_C_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            selectArea();
            Area.Text = "C";
            Button1.Hidden = true;
            Button2.Hidden = true;
            Button3.Hidden = true;
            Button4.Hidden = true;
            Button5.Hidden = true;
            Button6.Hidden = true;
            Button7.Hidden = true;
            Button8.Hidden = true;
            Button9.Hidden = true;
            Button10.Hidden = true;
            Button11.Hidden = true;

            Button11_1.Hidden = true;
            Button12.Hidden = true;
            Button13.Hidden = true;
            Button14.Hidden = true;
            Button15.Hidden = true;
            Button16.Hidden = true;
            Button17.Hidden = true;
            Button18.Hidden = true;
            Button19.Hidden = true;
            Button20.Hidden = true;
            Button21.Hidden = true;
            Button22.Hidden = true;
            Button23.Hidden = true;
            Button24.Hidden = true;
            Button25.Hidden = true;
            Button26.Hidden = true;
            Button27.Hidden = true;
            Button28.Hidden = true;
            Button29.Hidden = true;
            Button30.Hidden = true;

            Button31.Hidden = false;
            Button32.Hidden = false;
            Button33.Hidden = false;
            Button34.Hidden = false;
            Button35.Hidden = false;
            Button36.Hidden = false;
            Button37.Hidden = false;
            Button38.Hidden = false;
            Button39.Hidden = false;
            Button40.Hidden = false;
            Button41.Hidden = false;
            Button42.Hidden = false;
            Button43.Hidden = false;
            Button44.Hidden = false;
            Button45.Hidden = false;
            Button46.Hidden = false;

            Button46_1.Hidden = true;
            Button47.Hidden = true;
            Button48.Hidden = true;
            Button49.Hidden = true;
            Button50.Hidden = true;
            Button51.Hidden = true;
            Button51_1.Hidden = true;
            Button52.Hidden = true;
            Button53.Hidden = true;
            Button54.Hidden = true;
            Button55.Hidden = true;
            Button56.Hidden = true;
            Button57.Hidden = true;
            Button58.Hidden = true;

            Buttonv1.Hidden = true;
            Buttonv2.Hidden = true;
            Buttonv3.Hidden = true;
            Buttonv4.Hidden = true;

            Button81.Hidden = true;
            Button82.Hidden = true;
            Button83.Hidden = true;
            Button84.Hidden = true;
            Button85.Hidden = true;
            Button86.Hidden = true;
            Button87.Hidden = true;
            Button88.Hidden = true;
            Button89.Hidden = true;
            Button90.Hidden = true;
            Button91.Hidden = true;
            Button92.Hidden = true;
            Button93.Hidden = true;
            Button94.Hidden = true;
            Button95.Hidden = true;
            Button95_1.Hidden = true;
            Button96.Hidden = true;
            Button97.Hidden = true;
            Button98.Hidden = true;
            Button99.Hidden = true;
            Button100.Hidden = true;
            Button101.Hidden = true;
            Button102.Hidden = true;
            Button103.Hidden = true;
            Button104.Hidden = true;
            Button105.Hidden = true;
            Button106.Hidden = true;
            Button107.Hidden = true;
            Button108.Hidden = true;
            Button109.Hidden = true;
            Button110.Hidden = true;
            Button111.Hidden = true;
            Button112.Hidden = true;
            Button113.Hidden = true;
            Button114.Hidden = true;
            Button115.Hidden = true;
        }

        protected void Btn_D_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            selectArea();
            Area.Text = "D";
            Button1.Hidden = true;
            Button2.Hidden = true;
            Button3.Hidden = true;
            Button4.Hidden = true;
            Button5.Hidden = true;
            Button6.Hidden = true;
            Button7.Hidden = true;
            Button8.Hidden = true;
            Button9.Hidden = true;
            Button10.Hidden = true;
            Button11.Hidden = true;

            Button11_1.Hidden = true;
            Button12.Hidden = true;
            Button13.Hidden = true;
            Button14.Hidden = true;
            Button15.Hidden = true;
            Button16.Hidden = true;
            Button17.Hidden = true;
            Button18.Hidden = true;
            Button19.Hidden = true;
            Button20.Hidden = true;
            Button21.Hidden = true;
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
            Button32.Hidden = true;
            Button33.Hidden = true;
            Button34.Hidden = true;
            Button35.Hidden = true;
            Button36.Hidden = true;
            Button37.Hidden = true;
            Button38.Hidden = true;
            Button39.Hidden = true;
            Button40.Hidden = true;
            Button41.Hidden = true;
            Button42.Hidden = true;
            Button43.Hidden = true;
            Button44.Hidden = true;
            Button45.Hidden = true;
            Button46.Hidden = true;

            Button46_1.Hidden = false;
            Button47.Hidden = false;
            Button48.Hidden = false;
            Button49.Hidden = false;
            Button50.Hidden = false;
            Button51.Hidden = false;
            Button51_1.Hidden = false;
            Button52.Hidden = false;
            Button53.Hidden = false;
            Button54.Hidden = false;
            Button55.Hidden = false;
            Button56.Hidden = false;
            Button57.Hidden = false;
            Button58.Hidden = false;

            Buttonv1.Hidden = true;
            Buttonv2.Hidden = true;
            Buttonv3.Hidden = true;
            Buttonv4.Hidden = true;

            Button81.Hidden = true;
            Button82.Hidden = true;
            Button83.Hidden = true;
            Button84.Hidden = true;
            Button85.Hidden = true;
            Button86.Hidden = true;
            Button87.Hidden = true;
            Button88.Hidden = true;
            Button89.Hidden = true;
            Button90.Hidden = true;
            Button91.Hidden = true;
            Button92.Hidden = true;
            Button93.Hidden = true;
            Button94.Hidden = true;
            Button95.Hidden = true;
            Button95_1.Hidden = true;
            Button96.Hidden = true;
            Button97.Hidden = true;
            Button98.Hidden = true;
            Button99.Hidden = true;
            Button100.Hidden = true;
            Button101.Hidden = true;
            Button102.Hidden = true;
            Button103.Hidden = true;
            Button104.Hidden = true;
            Button105.Hidden = true;
            Button106.Hidden = true;
            Button107.Hidden = true;
            Button108.Hidden = true;
            Button109.Hidden = true;
            Button110.Hidden = true;
            Button111.Hidden = true;
            Button112.Hidden = true;
            Button113.Hidden = true;
            Button114.Hidden = true;
            Button115.Hidden = true;
        }

        protected void Btn_VIP_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            selectArea();
            Area.Text = "VIP";
            Button1.Hidden = true;
            Button2.Hidden = true;
            Button3.Hidden = true;
            Button4.Hidden = true;
            Button5.Hidden = true;
            Button6.Hidden = true;
            Button7.Hidden = true;
            Button8.Hidden = true;
            Button9.Hidden = true;
            Button10.Hidden = true;
            Button11.Hidden = true;

            Button11_1.Hidden = true;
            Button12.Hidden = true;
            Button13.Hidden = true;
            Button14.Hidden = true;
            Button15.Hidden = true;
            Button16.Hidden = true;
            Button17.Hidden = true;
            Button18.Hidden = true;
            Button19.Hidden = true;
            Button20.Hidden = true;
            Button21.Hidden = true;
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
            Button32.Hidden = true;
            Button33.Hidden = true;
            Button34.Hidden = true;
            Button35.Hidden = true;
            Button36.Hidden = true;
            Button37.Hidden = true;
            Button38.Hidden = true;
            Button39.Hidden = true;
            Button40.Hidden = true;
            Button41.Hidden = true;
            Button42.Hidden = true;
            Button43.Hidden = true;
            Button44.Hidden = true;
            Button45.Hidden = true;
            Button46.Hidden = true;

            Button46_1.Hidden = true;
            Button47.Hidden = true;
            Button48.Hidden = true;
            Button49.Hidden = true;
            Button50.Hidden = true;
            Button51.Hidden = true;
            Button51_1.Hidden = true;
            Button52.Hidden = true;
            Button53.Hidden = true;
            Button54.Hidden = true;
            Button55.Hidden = true;
            Button56.Hidden = true;
            Button57.Hidden = true;
            Button58.Hidden = true;

            Buttonv1.Hidden = false;
            Buttonv2.Hidden = false;
            Buttonv3.Hidden = false;
            Buttonv4.Hidden = false;

            Button81.Hidden = true;
            Button82.Hidden = true;
            Button83.Hidden = true;
            Button84.Hidden = true;
            Button85.Hidden = true;
            Button86.Hidden = true;
            Button87.Hidden = true;
            Button88.Hidden = true;
            Button89.Hidden = true;
            Button90.Hidden = true;
            Button91.Hidden = true;
            Button92.Hidden = true;
            Button93.Hidden = true;
            Button94.Hidden = true;
            Button95.Hidden = true;
            Button95_1.Hidden = true;
            Button96.Hidden = true;
            Button97.Hidden = true;
            Button98.Hidden = true;
            Button99.Hidden = true;
            Button100.Hidden = true;
            Button101.Hidden = true;
            Button102.Hidden = true;
            Button103.Hidden = true;
            Button104.Hidden = true;
            Button105.Hidden = true;
            Button106.Hidden = true;
            Button107.Hidden = true;
            Button108.Hidden = true;
            Button109.Hidden = true;
            Button110.Hidden = true;
            Button111.Hidden = true;
            Button112.Hidden = true;
            Button113.Hidden = true;
            Button114.Hidden = true;
            Button115.Hidden = true;
        }

        protected void Btn_E_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            selectArea();
            Area.Text = "E";
            Button1.Hidden = true;
            Button2.Hidden = true;
            Button3.Hidden = true;
            Button4.Hidden = true;
            Button5.Hidden = true;
            Button6.Hidden = true;
            Button7.Hidden = true;
            Button8.Hidden = true;
            Button9.Hidden = true;
            Button10.Hidden = true;
            Button11.Hidden = true;

            Button11_1.Hidden = true;
            Button12.Hidden = true;
            Button13.Hidden = true;
            Button14.Hidden = true;
            Button15.Hidden = true;
            Button16.Hidden = true;
            Button17.Hidden = true;
            Button18.Hidden = true;
            Button19.Hidden = true;
            Button20.Hidden = true;
            Button21.Hidden = true;
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
            Button32.Hidden = true;
            Button33.Hidden = true;
            Button34.Hidden = true;
            Button35.Hidden = true;
            Button36.Hidden = true;
            Button37.Hidden = true;
            Button38.Hidden = true;
            Button39.Hidden = true;
            Button40.Hidden = true;
            Button41.Hidden = true;
            Button42.Hidden = true;
            Button43.Hidden = true;
            Button44.Hidden = true;
            Button45.Hidden = true;
            Button46.Hidden = true;

            Button46_1.Hidden = true;
            Button47.Hidden = true;
            Button48.Hidden = true;
            Button49.Hidden = true;
            Button50.Hidden = true;
            Button51.Hidden = true;
            Button51_1.Hidden = true;
            Button52.Hidden = true;
            Button53.Hidden = true;
            Button54.Hidden = true;
            Button55.Hidden = true;
            Button56.Hidden = true;
            Button57.Hidden = true;
            Button58.Hidden = true;

            Buttonv1.Hidden = true;
            Buttonv2.Hidden = true;
            Buttonv3.Hidden = true;
            Buttonv4.Hidden = true;

            Button81.Hidden = false;
            Button82.Hidden = false;
            Button83.Hidden = false;
            Button84.Hidden = false;
            Button85.Hidden = false;
            Button86.Hidden = false;
            Button87.Hidden = false;
            Button88.Hidden = false;
            Button89.Hidden = false;
            Button90.Hidden = false;
            Button91.Hidden = false;
            Button92.Hidden = false;

            Button93.Hidden = true;
            Button94.Hidden = true;
            Button95.Hidden = true;
            Button95_1.Hidden = true;
            Button96.Hidden = true;
            Button97.Hidden = true;
            Button98.Hidden = true;
            Button99.Hidden = true;
            Button100.Hidden = true;
            Button101.Hidden = true;
            Button102.Hidden = true;
            Button103.Hidden = true;
            Button104.Hidden = true;
            Button105.Hidden = true;
            Button106.Hidden = true;
            Button107.Hidden = true;
            Button108.Hidden = true;
            Button109.Hidden = true;
            Button110.Hidden = true;
            Button111.Hidden = true;
            Button112.Hidden = true;
            Button113.Hidden = true;
            Button114.Hidden = true;
            Button115.Hidden = true;
        }

        protected void Btn_F_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            selectArea();
            Area.Text = "F";
            
            Button1.Hidden = true;
            Button2.Hidden = true;
            Button3.Hidden = true;
            Button4.Hidden = true;
            Button5.Hidden = true;
            Button6.Hidden = true;
            Button7.Hidden = true;
            Button8.Hidden = true;
            Button9.Hidden = true;
            Button10.Hidden = true;
            Button11.Hidden = true;

            Button11_1.Hidden = true;
            Button12.Hidden = true;
            Button13.Hidden = true;
            Button14.Hidden = true;
            Button15.Hidden = true;
            Button16.Hidden = true;
            Button17.Hidden = true;
            Button18.Hidden = true;
            Button19.Hidden = true;
            Button20.Hidden = true;
            Button21.Hidden = true;
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
            Button32.Hidden = true;
            Button33.Hidden = true;
            Button34.Hidden = true;
            Button35.Hidden = true;
            Button36.Hidden = true;
            Button37.Hidden = true;
            Button38.Hidden = true;
            Button39.Hidden = true;
            Button40.Hidden = true;
            Button41.Hidden = true;
            Button42.Hidden = true;
            Button43.Hidden = true;
            Button44.Hidden = true;
            Button45.Hidden = true;
            Button46.Hidden = true;

            Button46_1.Hidden = true;
            Button47.Hidden = true;
            Button48.Hidden = true;
            Button49.Hidden = true;
            Button50.Hidden = true;
            Button51.Hidden = true;
            Button51_1.Hidden = true;
            Button52.Hidden = true;
            Button53.Hidden = true;
            Button54.Hidden = true;
            Button55.Hidden = true;
            Button56.Hidden = true;
            Button57.Hidden = true;
            Button58.Hidden = true;

            Buttonv1.Hidden = true;
            Buttonv2.Hidden = true;
            Buttonv3.Hidden = true;
            Buttonv4.Hidden = true;

            Button81.Hidden = true;
            Button82.Hidden = true;
            Button83.Hidden = true;
            Button84.Hidden = true;
            Button85.Hidden = true;
            Button86.Hidden = true;
            Button87.Hidden = true;
            Button88.Hidden = true;
            Button89.Hidden = true;
            Button90.Hidden = true;
            Button91.Hidden = true;
            Button92.Hidden = true;

            Button93.Hidden = false;
            Button94.Hidden = false;
            Button95.Hidden = false;
            Button95_1.Hidden = false;
            Button96.Hidden = false;
            Button97.Hidden = false;
            Button98.Hidden = false;
            Button99.Hidden = false;
            Button100.Hidden = false;
            Button101.Hidden = false;
            Button102.Hidden = false;
            Button103.Hidden = false;
            Button104.Hidden = false;

            Button105.Hidden = true;
            Button106.Hidden = true;
            Button107.Hidden = true;
            Button108.Hidden = true;
            Button109.Hidden = true;
            Button110.Hidden = true;
            Button111.Hidden = true;
            Button112.Hidden = true;
            Button113.Hidden = true;
            Button114.Hidden = true;
            Button115.Hidden = true;
        }

        protected void Btn_G_Click(object sender, DirectEventArgs e)
        {
            Panel6.Hidden = true;
            Panel7.Hidden = true;
            selectArea();
            Area.Text = "G";
            
            Button1.Hidden = true;
            Button2.Hidden = true;
            Button3.Hidden = true;
            Button4.Hidden = true;
            Button5.Hidden = true;
            Button6.Hidden = true;
            Button7.Hidden = true;
            Button8.Hidden = true;
            Button9.Hidden = true;
            Button10.Hidden = true;
            Button11.Hidden = true;

            Button11_1.Hidden = true;
            Button12.Hidden = true;
            Button13.Hidden = true;
            Button14.Hidden = true;
            Button15.Hidden = true;
            Button16.Hidden = true;
            Button17.Hidden = true;
            Button18.Hidden = true;
            Button19.Hidden = true;
            Button20.Hidden = true;
            Button21.Hidden = true;
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
            Button32.Hidden = true;
            Button33.Hidden = true;
            Button34.Hidden = true;
            Button35.Hidden = true;
            Button36.Hidden = true;
            Button37.Hidden = true;
            Button38.Hidden = true;
            Button39.Hidden = true;
            Button40.Hidden = true;
            Button41.Hidden = true;
            Button42.Hidden = true;
            Button43.Hidden = true;
            Button44.Hidden = true;
            Button45.Hidden = true;
            Button46.Hidden = true;

            Button46_1.Hidden = true;
            Button47.Hidden = true;
            Button48.Hidden = true;
            Button49.Hidden = true;
            Button50.Hidden = true;
            Button51.Hidden = true;
            Button51_1.Hidden = true;
            Button52.Hidden = true;
            Button53.Hidden = true;
            Button54.Hidden = true;
            Button55.Hidden = true;
            Button56.Hidden = true;
            Button57.Hidden = true;
            Button58.Hidden = true;

            Buttonv1.Hidden = true;
            Buttonv2.Hidden = true;
            Buttonv3.Hidden = true;
            Buttonv4.Hidden = true;

            Button81.Hidden = true;
            Button82.Hidden = true;
            Button83.Hidden = true;
            Button84.Hidden = true;
            Button85.Hidden = true;
            Button86.Hidden = true;
            Button87.Hidden = true;
            Button88.Hidden = true;
            Button89.Hidden = true;
            Button90.Hidden = true;
            Button91.Hidden = true;
            Button92.Hidden = true;

            Button93.Hidden = true;
            Button94.Hidden = true;
            Button95.Hidden = true;
            Button95_1.Hidden = true;
            Button96.Hidden = true;
            Button97.Hidden = true;
            Button98.Hidden = true;
            Button99.Hidden = true;
            Button100.Hidden = true;
            Button101.Hidden = true;
            Button102.Hidden = true;
            Button103.Hidden = true;
            Button104.Hidden = true;

            Button105.Hidden = false;
            Button106.Hidden = false;
            Button107.Hidden = false;
            Button108.Hidden = false;
            Button109.Hidden = false;
            Button110.Hidden = false;
            Button111.Hidden = false;
            Button112.Hidden = false;
            Button113.Hidden = false;
            Button114.Hidden = false;
            Button115.Hidden = false;
        }

        protected void Btn_Morning_Click(object sender, DirectEventArgs e)
        {
            selectTime();
            Time.Text = "001";
            Panel6.Hidden = false;
            Panel7.Hidden = false;
        }

        protected void Btn_Afternoon_Click(object sender, DirectEventArgs e)
        {
            selectTime();
            Time.Text = "002";
            Panel6.Hidden = false;
            Panel7.Hidden = false;
        }

        protected void Btn_Night_Click(object sender, DirectEventArgs e)
        {
            selectTime();
            Time.Text = "003";
            Panel6.Hidden = false;
            Panel7.Hidden = false;
        }


        protected void selectArea()
        {
            //System.Media.SystemSounds.Asterisk.Play();
            if (Floor.Text == "")
            {
                BaseForm bs = new BaseForm();
                bs._NotificationShow("请输入楼层!");
                return;
            }
                
        }

        protected void selectTime()
        {
            //System.Media.SystemSounds.Asterisk.Play();
            if (Floor.Text == "" || Area.Text == "")
            {
                BaseForm bs = new BaseForm();
                bs._NotificationShow("请输入楼层及床区!");
                return;
            }
        }

        protected void select_Bed()
        {
            //System.Media.SystemSounds.Asterisk.Play();
            if (Floor.Text == "" || Area.Text == "" || Time.Text == "")
            {
                BaseForm bs = new BaseForm();
                bs._NotificationShow("请输入楼层,床区及时段!");
                return;
            }
            //else
            //{
            //    DBMysql db = new DBMysql();
            //    string sql;
            //    sql = "select DISTINCT a.apptst_bed from appointment_setup a ";
            //    sql += "where a.apptst_flr = '" + Floor.Text + "' ";
            //    sql += " and a.apptst_sec = '" + Area.Text + "' ";
            //    sql += " and a.apptst_timetyp= '" + Time.Text + "'";
            //    DataTable dt = db.Query(sql);
            //    fillDataComboBox(dt, com_Select_Time);
            //}
        }

        protected void select_fin()
        {
            //System.Media.SystemSounds.Asterisk.Play();
            if (Floor.Text == "" || Area.Text == "" || Time.Text == "" || Bed_number.Text == "")
            {
                BaseForm bs = new BaseForm();
                bs._NotificationShow("请输入楼层,床区,时段及床位!");
                return;
            }
            else
            {
                //Button1.OnClientClick = "javascript:window.open('ipad_PatientList.aspx?editmode=page1');";
                //Session["Floor"] = Floor.Text;
                //Session["Area"] = Area.Text;
                //Session["Time"] = Time.Text;
                //Session["BedNo"] = Bed_number.Text;
                //Session["DayTyp"] = daytyp.Text;
                //Response.Redirect("ipad_PatientList.aspx?editmode=page1");
                //Response.Redirect("ipad_PatientList.aspx?floor=" + Floor.Text + "&area=" + Area.Text + "&time=" + Time.Text + "&bedno=" + Bed_number.Text + "&daytyp=" + daytyp.Text);
            }
        }

        //protected void com_Select_Time_Click(object sender, DirectEventArgs e)
        //{
        //    Session["Floor"] = Floor.Text;
        //    Session["Area"] = Area.Text;
        //    Session["Time"] = Time.Text;
        //    Session["BedNo"] = com_Select_Time./SelectedItem.Value;
        //    Session["DayTyp"] = daytyp.Text;
        //    Response.Redirect("ipad_PatientList.aspx");
        //}


        //protected void Button1_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "01"; select_fin(); clear(); }
        //protected void Button2_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "02"; select_fin(); clear(); }
        //protected void Button3_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "03"; select_fin(); clear(); }
        //protected void Button4_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "04"; select_fin(); clear(); }
        //protected void Button5_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "05"; select_fin(); clear(); }
        //protected void Button6_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "06"; select_fin(); clear(); }
        //protected void Button7_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "07"; select_fin(); clear(); }
        //protected void Button8_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "08"; select_fin(); clear(); }
        //protected void Button9_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "09"; select_fin(); clear(); }
        //protected void Button10_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "10"; select_fin(); clear(); }
        //protected void Button11_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "11"; select_fin(); clear(); }
        //protected void Button11_1_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "11-1"; select_fin(); clear(); }
        //protected void Button12_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "12"; select_fin(); clear(); }
        //protected void Button13_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "13"; select_fin(); clear(); }
        //protected void Button14_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "14"; select_fin(); clear(); }
        //protected void Button15_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "15"; select_fin(); clear(); }
        //protected void Button16_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "16"; select_fin(); clear(); }
        //protected void Button17_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "17"; select_fin(); clear(); }
        //protected void Button18_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "18"; select_fin(); clear(); }
        //protected void Button19_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "19"; select_fin(); clear(); }
        //protected void Button20_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "20"; select_fin(); clear(); }
        //protected void Button21_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "21"; select_fin(); clear(); }
        //protected void Button22_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "22"; select_fin(); clear(); }
        //protected void Button23_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "23"; select_fin(); clear(); }
        //protected void Button24_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "24"; select_fin(); clear(); }
        //protected void Button25_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "25"; select_fin(); clear(); }
        //protected void Button26_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "26"; select_fin(); clear(); }
        //protected void Button27_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "27"; select_fin(); clear(); }
        //protected void Button28_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "28"; select_fin(); clear(); }
        //protected void Button29_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "29"; select_fin(); clear(); }
        //protected void Button30_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "30"; select_fin(); clear(); }
        //protected void Button31_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "31"; select_fin(); clear(); }
        //protected void Button32_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "32"; select_fin(); clear(); }
        //protected void Button33_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "33"; select_fin(); clear(); }
        //protected void Button34_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "34"; select_fin(); clear(); }
        //protected void Button35_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "35"; select_fin(); clear(); }
        //protected void Button36_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "36"; select_fin(); clear(); }
        //protected void Button37_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "37"; select_fin(); clear(); }
        //protected void Button38_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "38"; select_fin(); clear(); }
        //protected void Button39_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "39"; select_fin(); clear(); }
        //protected void Button40_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "40"; select_fin(); clear(); }
        //protected void Button41_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "41"; select_fin(); clear(); }
        //protected void Button42_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "42"; select_fin(); clear(); }
        //protected void Button43_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "43"; select_fin(); clear(); }
        //protected void Button44_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "44"; select_fin(); clear(); }
        //protected void Button45_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "45"; select_fin(); clear(); }
        //protected void Button46_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "46"; select_fin(); clear(); }
        //protected void Button46_1_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "46-1"; select_fin(); clear(); }
        //protected void Button47_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "47"; select_fin(); clear(); }
        //protected void Button48_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "48"; select_fin(); clear(); }
        //protected void Button49_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "49"; select_fin(); clear(); }
        //protected void Button50_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "50"; select_fin(); clear(); }
        //protected void Button51_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "51"; select_fin(); clear(); }
        //protected void Button51_1_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "51-1"; select_fin(); clear(); }
        //protected void Button52_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "52"; select_fin(); clear(); }
        //protected void Button53_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "53"; select_fin(); clear(); }
        //protected void Button54_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "54"; select_fin(); clear(); }
        //protected void Button55_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "55"; select_fin(); clear(); }
        //protected void Button56_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "56"; select_fin(); clear(); }
        //protected void Button57_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "57"; select_fin(); clear(); }
        //protected void Button58_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "58"; select_fin(); clear(); }

        //protected void Buttonv1_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "v1"; select_fin(); clear(); }
        //protected void Buttonv2_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "v2"; select_fin(); clear(); }
        //protected void Buttonv3_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "v3"; select_fin(); clear(); }
        //protected void Buttonv4_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "v4"; select_fin(); clear(); }

        //protected void Button81_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "81"; select_fin(); clear(); }
        //protected void Button82_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "82"; select_fin(); clear(); }
        //protected void Button83_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "83"; select_fin(); clear(); }
        //protected void Button84_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "84"; select_fin(); clear(); }
        //protected void Button85_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "85"; select_fin(); clear(); }
        //protected void Button86_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "86"; select_fin(); clear(); }
        //protected void Button87_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "87"; select_fin(); clear(); }
        //protected void Button88_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "88"; select_fin(); clear(); }
        //protected void Button89_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "89"; select_fin(); clear(); }
        //protected void Button90_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "90"; select_fin(); clear(); }
        //protected void Button91_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "91"; select_fin(); clear(); }
        //protected void Button92_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "92"; select_fin(); clear(); }
        //protected void Button93_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "93"; select_fin(); clear(); }
        //protected void Button94_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "94"; select_fin(); clear(); }
        //protected void Button95_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "95"; select_fin(); clear(); }
        //protected void Button95_1_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "95-1"; select_fin(); clear(); }
        //protected void Button96_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "96"; select_fin(); clear(); }
        //protected void Button97_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "97"; select_fin(); clear(); }
        //protected void Button98_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "98"; select_fin(); clear(); }
        //protected void Button99_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "99"; select_fin(); clear(); }

        //protected void Button100_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "100"; select_fin(); clear(); }
        //protected void Button101_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "101"; select_fin(); clear(); }
        //protected void Button102_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "102"; select_fin(); clear(); }
        //protected void Button103_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "103"; select_fin(); clear(); }
        //protected void Button104_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "104"; select_fin(); clear(); }
        //protected void Button105_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "105"; select_fin(); clear(); }
        //protected void Button106_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "106"; select_fin(); clear(); }
        //protected void Button107_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "107"; select_fin(); clear(); }
        //protected void Button108_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "108"; select_fin(); clear(); }
        //protected void Button109_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "109"; select_fin(); clear(); }
        //protected void Button110_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "110"; select_fin(); clear(); }
        //protected void Button111_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "111"; select_fin(); clear(); }
        //protected void Button112_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "112"; select_fin(); clear(); }
        //protected void Button113_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "113"; select_fin(); clear(); }
        //protected void Button114_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "114"; select_fin(); clear(); }
        //protected void Button115_click(object sender, DirectEventArgs e) { select_Bed(); Bed_number.Text = "115"; select_fin(); clear(); }


        protected void Btn_clear_Click(object sender, DirectEventArgs e)
        {
        }
        
        protected void Timer1_Timer(object sender, EventArgs e)
        {
            Tex_Datetime.Text = DateTime.Now.ToString("yyyy/MM/dd HH:mm:ss");
        }

        protected void clear()
        {
            Btn_3F.EnableToggle = false;
            Btn_5F.EnableToggle = false;
            Btn_A.EnableToggle = false;
            Btn_B.EnableToggle = false;
            Btn_C.EnableToggle = false;
            Btn_D.EnableToggle = false;
            Btn_VIP.EnableToggle = false;
            Btn_E.EnableToggle = false;
            Btn_F.EnableToggle = false;
            Btn_G.EnableToggle = false;
            Btn_Morning.EnableToggle = false;
            Btn_Afternoon.EnableToggle = false;
            Btn_Night.EnableToggle = false;

            Button1.EnableToggle = false;
            Button2.EnableToggle = false;
            Button3.EnableToggle = false;
            Button4.EnableToggle = false;
            Button5.EnableToggle = false;
            Button6.EnableToggle = false;
            Button7.EnableToggle = false;
            Button8.EnableToggle = false;
            Button9.EnableToggle = false;
            Button10.EnableToggle = false;
            Button11.EnableToggle = false;
            Button11_1.EnableToggle = false;
            Button12.EnableToggle = false;
            Button13.EnableToggle = false;
            Button14.EnableToggle = false;
            Button15.EnableToggle = false;
            Button16.EnableToggle = false;
            Button17.EnableToggle = false;
            Button18.EnableToggle = false;
            Button19.EnableToggle = false;
            Button20.EnableToggle = false;
            Button21.EnableToggle = false;
            Button22.EnableToggle = false;
            Button23.EnableToggle = false;
            Button24.EnableToggle = false;
            Button25.EnableToggle = false;
            Button26.EnableToggle = false;
            Button27.EnableToggle = false;
            Button28.EnableToggle = false;
            Button29.EnableToggle = false;
            Button30.EnableToggle = false;
            Button31.EnableToggle = false;
            Button32.EnableToggle = false;
            Button33.EnableToggle = false;
            Button34.EnableToggle = false;
            Button35.EnableToggle = false;
            Button36.EnableToggle = false;
            Button37.EnableToggle = false;
            Button38.EnableToggle = false;
            Button39.EnableToggle = false;
            Button40.EnableToggle = false;
            Button41.EnableToggle = false;
            Button42.EnableToggle = false;
            Button43.EnableToggle = false;
            Button44.EnableToggle = false;
            Button45.EnableToggle = false;
            Button46.EnableToggle = false;
            Button46_1.EnableToggle = false;
            Button47.EnableToggle = false;
            Button48.EnableToggle = false;
            Button49.EnableToggle = false;
            Button50.EnableToggle = false;
            Button51.EnableToggle = false;
            Button51_1.EnableToggle = false;
            Button52.EnableToggle = false;
            Button53.EnableToggle = false;
            Button54.EnableToggle = false;
            Button55.EnableToggle = false;
            Button56.EnableToggle = false;
            Button57.EnableToggle = false;
            Button58.EnableToggle = false;

            Buttonv1.EnableToggle = false;
            Buttonv2.EnableToggle = false;
            Buttonv3.EnableToggle = false;
            Buttonv4.EnableToggle = false;

            Button81.EnableToggle = false;
            Button82.EnableToggle = false;
            Button83.EnableToggle = false;
            Button84.EnableToggle = false;
            Button85.EnableToggle = false;
            Button86.EnableToggle = false;
            Button87.EnableToggle = false;
            Button88.EnableToggle = false;
            Button89.EnableToggle = false;
            Button90.EnableToggle = false;
            Button91.EnableToggle = false;
            Button92.EnableToggle = false;
            Button93.EnableToggle = false;
            Button94.EnableToggle = false;
            Button95.EnableToggle = false;
            Button95_1.EnableToggle = false;
            Button96.EnableToggle = false;
            Button97.EnableToggle = false;
            Button98.EnableToggle = false;
            Button99.EnableToggle = false;
            Button100.EnableToggle = false;
            Button101.EnableToggle = false;
            Button102.EnableToggle = false;
            Button103.EnableToggle = false;
            Button104.EnableToggle = false;
            Button105.EnableToggle = false;
            Button106.EnableToggle = false;
            Button107.EnableToggle = false;
            Button108.EnableToggle = false;
            Button109.EnableToggle = false;
            Button110.EnableToggle = false;
            Button111.EnableToggle = false;
            Button112.EnableToggle = false;
            Button113.EnableToggle = false;
            Button114.EnableToggle = false;
            Button115.EnableToggle = false;
            //Floor.Text = "";
            //Area.Text = "";
            //Time.Text = "";
            //Bed_number.Text = "";
        }
    }
}