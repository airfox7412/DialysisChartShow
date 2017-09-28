using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Dialysis_Chart_Show
{
    public partial class ipad_EvaForm_Suzhou : BaseForm
    {
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        public string _TableName = "zinfo_h_08";
        public DateTime _dateTime = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request.QueryString["pid"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                bedno.Text = Request.QueryString["bedno"];
                time.Text = Request.QueryString["time"];
                daytyp.Text = Request.QueryString["daytyp"];
                page.Text = Request.QueryString["page"];
                if (patient_id.Text == null)
                {
                    patient_id.Text = Request.QueryString["amp;pid"];
                    floor.Text = Request.QueryString["amp;floor"];
                    area.Text = Request.QueryString["amp;area"];
                    time.Text = Request.QueryString["amp;time"];
                    bedno.Text = Request.QueryString["amp;bedno"];
                    daytyp.Text = Request.QueryString["amp;daytyp"];
                }
                info_date.Text = _dateTime.ToString("yyyy-MM-dd");
                Show();
            }
        }

        protected void Show()
        {
            string sql = "SELECT pif_id FROM pat_info ";
            sql += "WHERE pif_ic='" + patient_id.Text + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
                pid_id.Text = dt.Rows[0]["pif_id"].ToString();
            _zInfo_Show(_TableName, pid_id.Text, _dateTime.ToString("yyyy-MM-dd"));
        }

        #region 上一頁
        protected void Btn_back_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("ipad_PatientList.aspx?editmode=page3&floor=" + floor.Text +
                                                                  "&area=" + area.Text +
                                                                  "&time=" + time.Text +
                                                                  "&bedno=" + bedno.Text +
                                                                  "&dayTyp=" + daytyp.Text);
        }
        #endregion

        protected void BtnSave_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, pid_id.Text, info_date.Text);
            //Common._NotificationShow("评估表存盘");
        }

        public string a
        {
            get
            {
                try
                {
                    return Session["a"].ToString();
                }
                catch
                {
                    //_NotificationShow_TimeOut();
                    return "";
                }
            }
            set
            {
                Session.Add("a", value);
            }
        }

        protected void text_click(object sender, EventArgs e)
        {
            TextField textID = (TextField)sender;
            a = textID.ID;
            Window1.Show();
            TextField_UserID.Focus(false, 100);
        }

        protected void btnDecrypt_Click(object sender, DirectEventArgs e)
        {
            string wactive_flag = ""; 
            string wacciv_id = "";

            if (TextField_UserID.Text == "")
            {
                Common._NotificationShow("请输入正确工号!");
                return;
            }
            else
            {
                string sql = "SELECT a.acclv_fname,a.acclv_funm,a.acclv_id ";
                sql += "FROM access_level a ";
                sql += "WHERE a.acclv_stfcode = '" + TextField_UserID.Text + "' ";

                DataTable dt = db.Query(sql);
                if (dt.Rows.Count == 1)
                {
                    wactive_flag = "1";
                    wacciv_id = dt.Rows[0]["acclv_id"].ToString();

                    if (a == "txt_nurse")
                    {
                        txt_nurse.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                    else if (a == "txt_leader")
                    {
                        txt_leader.Text = dt.Rows[0]["acclv_fname"].ToString();
                    }
                }
                else
                {
                    sql = "SELECT a.acclv_fname,a.acclv_funm ";
                    sql += "FROM access_level a ";
                    sql += "WHERE a.acclv_stfcode = '" + TextField_UserID.Text + "' ";
                    dt = db.Query(sql);
                    if (dt.Rows.Count > 0)
                    {
                        Common._NotificationShow("工号有兩筆以上相同，请重新输入!");
                        TextField_UserID.Text = "";
                        Window1.Close();
                        return;
                    }
                    else
                    {
                        Common._NotificationShow("工号不存在，请重新输入!");
                        TextField_UserID.Text = "";
                        return;
                    }
                }

                if (wactive_flag == "1")
                {
                    sql = "SELECT a.associate_id,a.associate_active";
                    sql += " FROM associate_list a ";
                    sql += "WHERE a.associate_id = '" + wacciv_id + "'";
                    sql += "AND   a.associate_active ='A" + "'";
                    DataTable dt1 = db.Query(sql);
                    if (dt1.Rows.Count == 0)
                    {
                        Common._NotificationShow("此工號已關閉，请重新输入!");
                        TextField_UserID.Text = "";
                        return;
                    }
                }
                TextField_UserID.Text = "";
                Window1.Close();
            }
        }

        protected void btnClose_Click(object sender, DirectEventArgs e)
        {
            TextField_UserID.Text = "";
            Window1.Close();
        }

    }
}