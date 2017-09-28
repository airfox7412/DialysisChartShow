using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using Ext.Net;

namespace Dialysis_Chart_Show.Systems
{
    public partial class Dialysis_System_07 : BaseForm
    {
        public JiaMiJieMi aeskey = new JiaMiJieMi();
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (Session["USER_ID"] == null)
                {
                    X.Redirect("login.aspx");
                }
                else
                {
                    Rockey4ND rockey = new Rockey4ND();
                    string hid = rockey.open();
                    string keystr = rockey.ReadKey();
                    string sSQL = "SELECT genst_desc FROM general_setup WHERE genst_ctg='License' AND genst_code='Plate'";
                    DataTable dt = db.Query(sSQL);
                    if (dt.Rows.Count > 0)
                    {
                        Keyid.Text = aeskey.AES_Decrypt(aeskey.Base64Decrypt(dt.Rows[0]["genst_desc"].ToString().Substring(0, 32)));
                        Bedno.Text = aeskey.AES_Decrypt(aeskey.Base64Decrypt(dt.Rows[0]["genst_desc"].ToString().Substring(32, 32)));
                        window1.Title = hid;
                    }
                    try
                    {
                        New_Keyid.Text = aeskey.AES_Decrypt(aeskey.Base64Decrypt(keystr.Substring(0, 32)));
                        New_Bedno.Text = aeskey.AES_Decrypt(aeskey.Base64Decrypt(keystr.Substring(32, 32)));
                    }
                    catch (Exception ex)
                    {
                        Common._ErrorMsgShow(ex.Message.ToString());
                    }
                    rockey.close();                    
                }
            }
        }

        protected void BtnOK_Click(object sender, DirectEventArgs e)
        {
            Rockey4ND rockey = new Rockey4ND();
            string hid = rockey.open();

            string hidencode = aeskey.Base64Encrypt(aeskey.AES_Encrypt(Keyid.Text));
            string bedencode = aeskey.Base64Encrypt(aeskey.AES_Encrypt(Bedno.Text));
            rockey.WriteKey(hidencode + bedencode);
            rockey.close();
            string sSQL = "UPDATE general_setup SET genst_desc='" + hidencode + bedencode + "' ";
            sSQL += "WHERE genst_ctg='License' AND genst_code='Plate'";
            db.Excute(sSQL);
            Common._NotificationShow("Update Successed...");
        }

        protected void BtnNewKey_Click(object sender, DirectEventArgs e)
        {
            Rockey4ND rockey = new Rockey4ND();
            string hid = rockey.open();

            string hidencode = aeskey.Base64Encrypt(aeskey.AES_Encrypt(New_Keyid.Text));
            string bedencode = aeskey.Base64Encrypt(aeskey.AES_Encrypt(New_Bedno.Text));
            rockey.WriteKey(hidencode + bedencode);
            rockey.close();
            Common._NotificationShow("Write Successed for New Key...");
        }

        protected void BtnRead_Click(object sender, DirectEventArgs e)
        {
            Rockey4ND rockey = new Rockey4ND();
            string hid = rockey.open();
            string keystr = rockey.ReadKey();
            try
            {
                New_Keyid.Text = aeskey.AES_Decrypt(aeskey.Base64Decrypt(keystr.Substring(0, 32)));
                New_Bedno.Text = aeskey.AES_Decrypt(aeskey.Base64Decrypt(keystr.Substring(32, 32)));
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
            rockey.close();
        }

        protected void BtnWtoS_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string hidencode = aeskey.Base64Encrypt(aeskey.AES_Encrypt(New_Keyid.Text));
                string bedencode = aeskey.Base64Encrypt(aeskey.AES_Encrypt(New_Bedno.Text));
                string sSQL = "UPDATE general_setup SET genst_desc='" + hidencode + bedencode + "' ";
                sSQL += "WHERE genst_ctg='License' AND genst_code='Plate'";
                db.Excute(sSQL);
                Common._NotificationShow("Update SQL Successed...");
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }
        
    }
}