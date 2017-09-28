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
    public partial class loginXian : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Text_User.Focus(false, 100);
            }
        }

        protected void BtnLogin_Click(object sender, DirectEventArgs e)
        {
            if (Text_User.Text == "")
            {
                Common._NotificationShow("请输入正确工号!");
            }
            else
            {
                string[] Astr = checkuser(Text_User.Text);
                if (Astr[2] == "NU" || Astr[2] == "DC" || Astr[2] == "DH")
                {
                    Session["USER_NAME"] = getUserName(Text_User.Text);
                    Session["USER_ID"] = Text_User.Text;
                    Session["USER_RIGHT"] = Astr[2];
                    e.Success = true;
                    X.Redirect("ipad_Default.aspx");
                }
                else
                {
                    Common._NotificationShow("请重新输入!");
                    Text_User.Text = "";
                    Text_User.Focus(false, 100);
                }
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

        #region 開啟輸入人員資料
        protected void text_click(object sender, DirectEventArgs e)
        {
            string key = e.ExtraParams["keynum"].ToString();
            if (key == "13")
            {
                BtnLogin_Click(sender, e);
                e.Success = true;
            }
        }
        #endregion
    }
}