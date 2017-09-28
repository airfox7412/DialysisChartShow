using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using Ext.Net;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

namespace Dialysis_Chart_Show
{
    public partial class Dialysis_05_041 : BaseForm
    {
        public string sProcessOfPurifyingTheBlood = ConfigurationManager.AppSettings["ProcessOfPurifyingTheBlood"].ToString();

        protected void Page_Load(object sender, EventArgs e)
        {         
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = _PAT_IC;
                sid.Text = Request["sid"].ToString();
                Show();
            }
        }

        protected void Show()
        {
            string sql = "SELECT * FROM pat_patrol WHERE sid=" + sid.Text;
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                TextArea1.Text = dt.Rows[0]["pat_note"].ToString();
                TextField1.Text = dt.Rows[0]["pat_emp"].ToString();
            }
        }
        
        protected void Btn_save_Click(object sender, DirectEventArgs e)
        {
            string toTime = DateTime.Now.ToString("HH:mm:ss");
            string sql;
            sql = "UPDATE pat_patrol SET pat_note='" + TextArea1.Text + "', pat_emp='" + TextField1.Text + "' ";
            sql += "WHERE sid=" + sid.Text;
            try
            {
                db.Excute(sql);
                Common._NotificationShow("<font size=4>储存成功!</font>");
                X.Redirect("Dialysis_05_04.aspx");
            }
            catch (Exception ex)
            {
                Common._NotificationShow("<font size=4>储存失败!</font>");
            }
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
                string sql = "SELECT a.acclv_fname,a.acclv_funm,a.acclv_id";
                sql += " FROM access_level a ";
                sql += "WHERE a.acclv_stfcode = '" + TextField_UserID.Text + "' ";

                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    wactive_flag = "1";
                    wacciv_id = dt.Rows[0]["acclv_id"].ToString();
                    TextField1.Text = TextField1.Text + dt.Rows[0]["acclv_fname"].ToString() + ","; ;
                }
                else
                {
                    Common._NotificationShow("工号不存在，请重新输入!");
                    TextField_UserID.Text = "";
                    return;
                }

                if (wactive_flag == "1")
                {
                    sql = "SELECT a.associate_id,a.associate_active";
                    sql += " FROM associate_list a ";
                    sql += "WHERE a.associate_id     = '" + wacciv_id + "'";
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

        public void ErrorMsgShow(string myMessage)
        {
            X.Msg.Show(new MessageBoxConfig
            {
                Title = "错误信息",
                Message = myMessage,
                Width = 300,
                Buttons = MessageBox.Button.OK,
                Closable = false,
                Progress = false
            });
        }

        protected void textreplace(TextField tex)
        {
            tex.Text.Replace("'", "''");
        }

        public object[] GetDataArray(DataTable dt)
        {
            object[] objx = new Object[dt.Rows.Count];
            int i = 0;

            foreach (DataRow irow in dt.Rows)
            {
                object[] objy = new object[dt.Columns.Count];
                for (int j = 0; j < dt.Columns.Count; j++)
                {
                    objy[j] = irow[j];
                }
                objx[i] = objy;
                i++;
            }
            return objx;
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
    }
}