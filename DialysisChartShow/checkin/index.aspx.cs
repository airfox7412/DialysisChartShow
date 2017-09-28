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

namespace Dialysis_Chart_Show.checkin
{
    public partial class index : System.Web.UI.Page
    {
        string _UserID = "";
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

            _UserID = Text_User.Text;
            if (Text_User.Text == "" || Text_Pass.Text == "")
            {
                Common._NotificationShow("请输入正确代号及密码!");
            }
            else
            {
                string ipath = "localhost";
                string sql = "SELECT genst_desc FROM general_setup WHERE genst_ctg='IPConnect'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                    ipath = @"http://" + dt.Rows[0]["genst_desc"].ToString(); //路徑從資料庫取得

                sql = "SELECT acclv_passwd, acclv_type FROM access_level ";
                sql += "WHERE acclv_stfcode='" + Text_User.Text + "' ";
                dt = db.Query(sql);

                if (dt.Rows.Count == 1)
                {
                    string url = ipath + "/myhaisv4/gettype.php?USER_ID=" + Text_User.Text;
                    HttpWebRequest MyHttpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                    HttpWebResponse MyHttpWebResponse = (HttpWebResponse)MyHttpWebRequest.GetResponse();
                    Stream MyStream = MyHttpWebResponse.GetResponseStream();
                    StreamReader MyStreamReader = new StreamReader(MyStream, Encoding.GetEncoding("UTF-8"));
                    string getdata = MyStreamReader.ReadToEnd();
                    MyStreamReader.Close();
                    MyStream.Close();

                    string[] Astr = Regex.Split(getdata, ",");
                    if (Astr[1] == Text_Pass.Text)
                    {
                        if (Astr[2] == "DC" || Astr[2] == "DH")
                        {
                            Session["USER_NAME"] = getUserName(_UserID);
                            Session["USER_ID"] = _UserID;
                            Session["USER_RIGHT"] = Astr[2];
                            X.Redirect("PatientList.aspx");
                        }
                        else if (Astr[2] == "NU")
                        {
                            Session["USER_NAME"] = getUserName(_UserID);
                            Session["USER_ID"] = _UserID;
                            Session["USER_RIGHT"] = Astr[2];
                            X.Redirect("PatientList_Nurse.aspx");
                        }
                        else
                        {
                            Common._NotificationShow("只有醫師可開立醫囑，请重新输入!");
                        }
                    }
                    else
                    {
                        Common._NotificationShow("密码错误，请重新输入!");
                    }
                }
                else
                {
                    Common._NotificationShow("工号不存在，请重新输入!");
                }
            }
            Text_User.Text = "";
            Text_Pass.Text = "";
            Text_User.Focus(false, 100);
        }

        /// <summary>
        /// 用_UserID去access_level這個table找使用人
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        static public String getUserName(String userId)
        {
            String result;
            String sql_stmt = "SELECT * FROM access_level WHERE acclv_stfcode='" + userId + "' ";
            DataTable dtUSER_NAME2 = tools.DBMysql.query(sql_stmt, out result);
            if (dtUSER_NAME2.Rows.Count > 0)
                return dtUSER_NAME2.Rows[0]["acclv_fname"].ToString();
            else
                return userId;
        }

        protected void loginUser(object sender, DirectEventArgs e)
        {
            BtnLogin_Click(sender, e);
        }
    }
}