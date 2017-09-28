using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Collections;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_00_Add : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                info_user.Text = _USER_NAME;
            }
        }

        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            string sSQL = "INSERT INTO shift (sdate, stime, subject, user, remark) VALUES(";
            sSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "', ";
            sSQL += "'" + DateTime.Now.ToString("hh:mm:ss") + "', ";
            sSQL += "'" + info_subject.Text + "', ";
            sSQL += "'" + info_user.Text + "', ";
            sSQL += "'" + info_remark.Text + "') ";
            db.Excute(sSQL);
            X.Redirect("Dialysis_09_00_List.aspx");
        }

        protected void Btn_Close_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Dialysis_09_00_List.aspx");
        }

        protected void Btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }
    }
}