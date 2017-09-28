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

namespace Dialysis_Chart_Show.Systems
{
    public partial class login : System.Web.UI.Page
    {
        string hid = "";
        string _UserID = "";
        string _UserPass = "";
        DBMysql db = new DBMysql();

        protected void Page_Load(object sender, EventArgs e)
        {            
            if (!X.IsAjaxRequest)
            {
                Rockey4ND rockey = new Rockey4ND();
                hid = rockey.open();
                if (hid != "")
                {
                    Window1.Title = "登入窗口: " + hid;
                    Window1.Show();
                    txtUsername.Focus(true, 100);
                }
            }
        }

        protected void Button1_Click(object sender, DirectEventArgs e)
        {
            _UserID = txtUsername.Text;
            _UserPass = txtPassword.Text;
            if (txtUsername.Text != "admin" || txtPassword.Text == "")
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
                    e.Success = true;
                    X.Redirect("Dialysis_System_07.aspx");
                }
                else
                {
                    e.ErrorMessage = "用户名或密码错误，请重新输入!";
                    e.Success = false;
                }
            }
            txtUrl.Text = "login.aspx";
        }

        protected void Next_KeyPress(object sender, DirectEventArgs e)
        {
            string key = e.ExtraParams["keynum"].ToString();
            if (key == "13")
            {
                txtPassword.Focus(true, 100);
            }
        }

        protected void Login_KeyPress(object sender, DirectEventArgs e)
        {
            string key = e.ExtraParams["keynum"].ToString();
            if (key == "13")
            {
                _UserID = txtUsername.Text;
                _UserPass = txtPassword.Text;
                if (_UserID != "admin" || _UserPass == "")
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
                        e.Success = true;
                        X.Redirect("Dialysis_System_07.aspx");
                    }
                    else
                    {
                        e.Success = false;
                        e.ErrorMessage = "用户名或密码错误，请重新输入!";
                        txtUsername.Focus(true, 100);
                    }
                }
            }
        }

        #region 用_UserID去access_level這個table找使用人
        private String getUserName(String userId)
        {
            String sql = "SELECT * FROM access_level WHERE acclv_stfcode='" + userId + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["name"].ToString();//dt.Rows[0]["acclv_fname"].ToString();
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