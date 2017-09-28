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
    public partial class login : System.Web.UI.Page
    {
        string License = ConfigurationManager.AppSettings["1PageCount"].ToString();
        string hid = "";
        string _UserID = "";
        string _UserPass = "";
        DBMysql db = new DBMysql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                JiaMiJieMi aeskey = new JiaMiJieMi();
                string plocal = "";
                if (License.Length < 32) // keypro
                {
                    Rockey4ND rockey = new Rockey4ND();
                    hid = rockey.open();
                    if (hid != "")
                    {
                        string sSQL = "SELECT genst_desc FROM general_setup WHERE genst_ctg='License' AND genst_code='Plate'";
                        DataTable dt = db.Query(sSQL);
                        if (dt.Rows.Count > 0)
                        {
                            plocal = dt.Rows[0]["genst_desc"].ToString();
                        }
                        if (rockey.verify(hid, plocal))
                        {
                            string totalbed = aeskey.AES_Decrypt(aeskey.Base64Decrypt(plocal.Substring(32, 32)));
                            Window1.Title = "登入窗口: " + hid + "-" + totalbed;
                            Window1.Show();
                        }
                        else
                        {
                            Common._ErrorMsgShow("USB授权钥匙,认证编号不符合!");
                        }
                    }
                    else
                    {
                        Window1.Hide();
                    }
                    rockey.close();
                }
                else // softkey
                {
                    try
                    {
                        //RegistryKey regkey = Registry.CurrentUser.OpenSubKey(@"SOFTWARE\Datacom\Product\DCS42");
                        //string[] n = regkey.GetValueNames();
                        //string hid = regkey.GetValue(n[0]).ToString();
                        string hid = License;
                        if (hid.Length == 32)
                        {
                            hid = aeskey.AES_Decrypt(aeskey.Base64Decrypt(hid));
                            if (hid != "")
                            {
                                string sSQL = "SELECT genst_desc FROM general_setup WHERE genst_ctg='License' AND genst_code='Plate'";
                                DataTable dt = db.Query(sSQL);
                                if (dt.Rows.Count > 0)
                                {
                                    plocal = dt.Rows[0]["genst_desc"].ToString();
                                }
                                string sid = aeskey.AES_Decrypt(aeskey.Base64Decrypt(plocal.Substring(0, 32)));
                                if (hid == sid)
                                {
                                    string totalbed = aeskey.AES_Decrypt(aeskey.Base64Decrypt(plocal.Substring(32, 32)));
                                    Window1.Title = "登入窗口: " + hid + "-" + totalbed;
                                    Window1.Show();
                                }
                                else
                                    Common._ErrorMsgShow("授权钥匙,认证编号不符合!");
                            }
                            else
                            {
                                Window1.Hide();
                            }
                        }
                        else
                        {
                            Common._ErrorMsgShow("授权钥匙,认证编号不符合!");
                            Window1.Hide();
                        }
                    }
                    catch (Exception ex)
                    {
                        Common._ErrorMsgShow(ex.Message.ToString());
                    }
                }

                if (!string.IsNullOrEmpty(Request.QueryString["url"]))
                {
                    txtUrl.Text = Request.QueryString["url"];
                }
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
                    e.Success = true;
                    X.Redirect("Dialysis_Info.aspx");
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
                if (_UserID == "" || _UserPass == "")
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
                        X.Redirect("Dialysis_Info.aspx");
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