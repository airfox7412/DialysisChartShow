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
    public partial class ipad_EvaForm : BaseForm
    {
        public string Hospital = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();
        public DateTime _dateTime = DateTime.Now;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                pid.Text = Request.QueryString["pid"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                bedno.Text = Request.QueryString["bedno"];
                time.Text = Request.QueryString["time"];
                daytyp.Text = Request.QueryString["daytyp"];
                info_date.Text = _dateTime.ToString("yyyy-MM-dd");
                //Show();
            }
        }

        protected void Show()
        {
            string sql = "SELECT pif_id FROM pat_info ";
            sql += "WHERE pif_ic='" + patient_id.Text + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
                pid.Text = dt.Rows[0]["pif_id"].ToString();
        }

        protected void Button1_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Ipad/Dialysis_0h_01.aspx?editmode=edit&pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }

        protected void Button2_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Ipad/Dialysis_0h_02.aspx?editmode=edit&pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }

        protected void Button3_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Ipad/Dialysis_0h_03.aspx?editmode=edit&pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }

        protected void Button4_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Ipad/Dialysis_0h_04.aspx?editmode=edit&pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }

        protected void Button5_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Ipad/Dialysis_0h_05.aspx?editmode=edit&pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }

        protected void Button6_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Ipad/Dialysis_0h_06.aspx?editmode=edit&pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }

        protected void Button7_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Ipad/Dialysis_0h_07.aspx?editmode=edit&pid=" + pid.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }

        #region 上一頁
        protected void Button8_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("ipad_PatientList.aspx?pid=" + patient_id.Text + "&floor=" + floor.Text + "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text);
        }
        #endregion

    }
}