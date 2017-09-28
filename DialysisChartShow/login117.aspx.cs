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
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;

namespace Dialysis_Chart_Show
{
    public partial class login117 : System.Web.UI.Page
    {
        string _UserID = "";
        DBMysql db = new DBMysql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request.QueryString["patient_id"];
                patient_name.Text = Request.QueryString["patient_name"];
                machine_type.Text = Request.QueryString["machine_type"];
                hpack.Text = Request.QueryString["hpack"];
                bedno.Text = Request.QueryString["bedno"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                daytyp.Text = Request.QueryString["daytyp"];
                hpack3.Text = Request.QueryString["hpack3"];
                mechine_model.Text = Request.QueryString["mechine_model"];
                txt_weight_before.Text = Request.QueryString["patient_weight"];
                errcnt.Text = "0";

                if (patient_name.Text == null)
                {
                    patient_name.Text = Request.QueryString["amp;patient_name"];
                    machine_type.Text = Request.QueryString["amp;machine_type"];
                    hpack.Text = Request.QueryString["amp;hpack"];
                    bedno.Text = Request.QueryString["amp;bedno"];
                    floor.Text = Request.QueryString["amp;floor"];
                    area.Text = Request.QueryString["amp;area"];
                    time.Text = Request.QueryString["amp;time"];
                    daytyp.Text = Request.QueryString["amp;daytyp"];
                    hpack3.Text = Request.QueryString["amp;hpack3"];
                    mechine_model.Text = Request.QueryString["amp;mechine_model"];
                    txt_weight_before.Text = Request.QueryString["amp;patient_weight"];
                    errcnt.Text = "0";
                }
                Text_User.Focus(false, 100);
            }
        }

        protected void BtnLogin_Click(object sender, DirectEventArgs e)
        {
            _UserID = Text_User.Text;
            if (Text_User.Text == "" || Text_Pass.Text == "")
            {
                Common._NotificationShow("请输入正确代号及密码!");
                errcnt.Text = (Convert.ToInt16(errcnt.Text) + 1).ToString();
            }
            else
            {
                string[] Astr = checkuser(Text_User.Text);
                if (Astr[1] == Text_Pass.Text)
                {
                    if (Astr[2] == "NU" || Astr[2] == "DC" || Astr[2] == "DH")
                    {
                        Session["USER_NAME"] = getUserName(_UserID);
                        Session["USER_ID"] = _UserID;
                        Session["USER_RIGHT"] = Astr[2];
                        X.Redirect("ipad_detaillist_117.aspx?patient_id=" + patient_id.Text +
                                    "&patient_name=" + patient_name.Text +
                                    "&machine_type=" + machine_type.Text +
                                    "&mechine_model=" + mechine_model.Text +
                                    "&hpack=" + hpack.Text +
                                    "&hpack3=" + hpack3.Text +
                                    "&patient_weight=" + patient_weight.Text +
                                    "&bedno=" + bedno.Text +
                                    "&floor=" + floor.Text +
                                    "&area=" + area.Text +
                                    "&time=" + time.Text +
                                    "&daytyp=" + daytyp.Text);
                    }
                    else
                    {
                        Common._NotificationShow("只有醫師可開立醫囑，请重新输入!");
                    }
                }
                else
                {
                    Common._NotificationShow("密码错误，请重新输入!");
                    errcnt.Text = (Convert.ToInt16(errcnt.Text) + 1).ToString();
                }
            }
            Text_User.Text = "";
            Text_Pass.Text = "";
            Text_User.Focus(false, 100);
            if (errcnt.Text == "3")
            {
                X.Redirect("ipad_PatientList.aspx?pid=" + patient_id.Text +
                    "&floor=" + floor.Text +
                    "&area=" + area.Text +
                    "&time=" + time.Text +
                    "&bedno=" + bedno.Text +
                    "&daytyp=");
            }
        }

        #region 用_UserID去access_level這個table找使用人
        private String getUserName(String userId)
        {
            String sql = "SELECT * FROM access_level WHERE usrnm='" + userId + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["name"].ToString();
            else
                return userId;
        }
        #endregion
        
        #region 取得帳密認證
        private string[] checkuser(string UserID)
        {
            string[] Astr = { "", "", "" };
            string sSQL = "SELECT passwd, type FROM access_level WHERE usrnm='" + UserID + "' AND active='A'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                JiaMiJieMi aeskey = new JiaMiJieMi();
                string pwd = aeskey.AES_Decrypt(aeskey.Base64Decrypt(dt.Rows[0]["passwd"].ToString()));
                Astr[0] = "";
                Astr[1] = pwd;
                Astr[2] = dt.Rows[0]["type"].ToString();
            }
            return Astr;
        }
        #endregion
    }
}