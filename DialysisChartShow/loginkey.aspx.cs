using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Ext.Net;
using Microsoft.Win32;

namespace Dialysis_Chart_Show
{
    public partial class loginkey : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        string _UserID = "";
        string _UserPass = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Window1.Show();
                txtUsername.Focus(true, 100);
            }
        }

        protected void Button1_Click(object sender, DirectEventArgs e)
        {
            _UserID = txtUsername.Text;
            _UserPass = txtPassword.Text;
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                Common._NotificationShow("请输入正确代号及密码!");
            }
            else
            {
                string[] Astr = checkuser(_UserID);
                if (Astr[1] == _UserPass)
                {
                    Session["USER_NAME"] = getUserName(_UserID);
                    Session["USER_ID"] = _UserID;
                    Session["USER_RIGHT"] = Astr[2];
                    JiaMiJieMi aeskey = new JiaMiJieMi();
                    string Key = aeskey.Base64Encrypt(aeskey.AES_Encrypt(txtKey.Text));
                    Registry.SetValue(@"HKEY_CURRENT_USER\Software\Datacom\product\DCS42", "License", Key);
                    Common._ErrorMsgShow("OK OK OK");
                    e.Success = true;
                    X.Redirect("login.aspx");
                }
                else
                {
                    e.ErrorMessage = "用户名或密码错误，请重新输入!";
                    e.Success = false;
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
    }
}