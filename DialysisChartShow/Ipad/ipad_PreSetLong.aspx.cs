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

namespace Dialysis_Chart_Show.ipad
{
    public partial class ipad_PreSetLong : BaseForm
    {
        public string docname;
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void BtnModL_Click(object sender, DirectEventArgs e)
        {
            docname = Session["USER_NAME"].ToString();
            WindowGetMod.SetDrugModList(Patient_ID.Text, "L", docname);
            WindowGetMod.Show();
        }

        protected void Page_Init(object sender, EventArgs e)
        {
            WindowGetMod.GridStore1 = this.Store1;
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                if (Session["USER_NAME"] == null)
                {
                    X.Redirect("../ipad_login.aspx");
                }
                else
                {
                    Patient_ID.Text = Request.QueryString["pid"];
                    Show_Longdrug();
                }
            }
        }

        protected void Show_Longdrug()
        {
            string sql = "SELECT a.lgord_id,a.lgord_dateord,a.lgord_timeord,a.lgord_usr1,b.drg_name,a.lgord_intake,a.lgord_freq,a.lgord_medway,a.lgord_comment,a.lgord_dtactst ";
            sql += ", CASE a.lgord_actst WHEN '00001' THEN '' WHEN '00002' THEN '停用' END As Status ";
            sql += "FROM longterm_ordermgt a ";
            sql += "LEFT JOIN drug_list b ON a.lgord_drug=b.drg_code ";
            sql += "WHERE a.lgord_patic='" + Patient_ID.Text + "' ";
            sql += "ORDER BY a.lgord_dateord DESC, Status";
            DataTable dt = db.Query(sql);
            Store istore1 = Grid_Long_Term.GetStore();
            istore1.DataSource = db.GetDataArray(dt);
            istore1.DataBind();
            dt.Dispose();
        }

        //public object[] GetDataArray(DataTable dt)
        //{
        //    object[] objx = new Object[dt.Rows.Count];
        //    int i = 0;

        //    foreach (DataRow irow in dt.Rows)
        //    {
        //        object[] objy = new object[dt.Columns.Count];
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            objy[j] = irow[j];
        //        }
        //        objx[i] = objy;
        //        i++;
        //    }
        //    return objx;
        //}

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

        protected void BtnAccept_Click(object sender, DirectEventArgs e)
        {
            string sql = "";
            string ildrgcode;
            docname = Session["USER_NAME"].ToString();

            if (cb_druglist.Text == "")
            {
                Common._ErrorMsgShow("注意 : *注记为 '必填栏位' ");
                return;
            }

            sql = "SELECT drg_code FROM drug_list ";
            sql += "WHERE drg_name='" + cb_druglist.Text + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                ildrgcode = dt.Rows[0]["drg_code"].ToString();
            }
            else
            {
                ildrgcode = "";
            }

            string toTime = DateTime.Now.ToString("HH:mm");

            if (drugkind.Text == "L")
            {
                sql = "INSERT INTO longterm_ordermgt(lgord_patic,lgord_dateord,lgord_timeord,lgord_usr1,";
                sql += "lgord_drug,lgord_actst,lgord_dtactst,lgord_usr2,lgord_comment,lgord_intake,lgord_freq,lgord_nurs,lgord_medway) ";
                sql += "VALUES('" + Patient_ID.Text + "','" + toDay + "','" + toTime + "','" + docname;
                sql += "','" + ildrgcode + "','00001','','','" + txt_ordremark.Text + "','" + txt_ordcount.Text + "','" + Common.GetComboBoxText(cb_ordfreq) + "','','" + Common.GetComboBoxText(cb_medway) + "')";
            }
            else if (drugkind.Text == "LE")
            {
                sql = "UPDATE longterm_ordermgt SET ";
                sql += "lgord_timeord='" + toTime + "', ";
                sql += "lgord_usr2='" + docname + "', ";
                sql += "lgord_drug='" + ildrgcode + "', ";
                sql += "lgord_intake='" + txt_ordcount.Text + "', ";
                sql += "lgord_freq='" + Common.GetComboBoxText(cb_ordfreq) + "', ";
                sql += "lgord_medway='" + Common.GetComboBoxText(cb_medway) + "', ";
                sql += "lgord_comment='" + txt_ordremark.Text + "', ";
                sql += "lgord_dtactst='" + toDay + "' ";
                sql += "WHERE lgord_id='" + id.Text + "' ";
            }
            db.Excute(sql);

            cb_druglist.Text = "";
            txt_ordcount.Text = "";
            txt_ordremark.Text = "";
            cb_ordfreq.Text = "";
            cb_medway.Text = "";
            Window_Drug.Hide();
            Show_Longdrug();
        }

        protected void BtnCancel_Click(object sender, DirectEventArgs e)
        {
            cb_druglist.Text = "";
            Window_Drug.Hide();
        }

        protected void Load_ComboBox()
        {
            string sSQL = "SELECT genst_code AS CODE, genst_desc AS NAME FROM general_setup WHERE genst_ctg='drgfreq'";//用藥頻率
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cb_ordfreq, dt1, true, "NAME", "CODE");
            cb_ordfreq.Select(0);

            sSQL = "SELECT med_code AS CODE, med_name AS NAME FROM med_way";//給藥方式
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cb_medway, dt1, true, "NAME", "CODE");
            cb_medway.Select(0);
        }

        protected void BtnAddLong_Click(object sender, DirectEventArgs e)
        {
            Load_ComboBox();
            drugkind.Text = "L";
            txt_ordcount.Text = "";
            txt_ordremark.Text = "";
            Window_Drug.Title = "长期医嘱用药";
            Window_Drug.Show();
            cb_druglist.Focus(false, 100);
        }

        protected void EditLong_Click(object sender, DirectEventArgs e)
        {
            Load_ComboBox();
            drugkind.Text = "LE";
            id.Text = e.ExtraParams["id"];
            cb_druglist.Text = e.ExtraParams["name"];
            txt_ordcount.Text = e.ExtraParams["intake"];
            txt_ordremark.Text = e.ExtraParams["comment"];
            Common.SetComboBox(cb_medway, e.ExtraParams["medway"]);
            Common.SetComboBox(cb_ordfreq, e.ExtraParams["freq"]);
            Window_Drug.Title = "长期医嘱用药";
            Window_Drug.Show();
            cb_druglist.Focus(false, 100);
        }

        protected void BtnStop_Click(object sender, DirectEventArgs e)
        {
            string sql = "UPDATE longterm_ordermgt SET lgord_actst='00002', lgord_dtactst='" + toDay + "' ";
            sql += "WHERE lgord_id='" + id.Text + "' ";
            db.Excute(sql);
            Window_Drug.Hide();

            Show_Longdrug();
            Common._NotificationShow("停用医嘱用药");
        }

        protected void BtnDelete_Click(object sender, DirectEventArgs e)
        {
            string sql = "DELETE FROM longterm_ordermgt WHERE lgord_id='" + id.Text + "' ";
            db.Excute(sql);
            Window_Drug.Hide();
            Show_Longdrug();
            Common._NotificationShow("删除医嘱用药");
        }

        protected void OnbtnPrintL_Click(object sender, DirectEventArgs e)
        {
            PrintWindow.Show();
            PrintWindow.Loader.SuspendScripting();
            PrintWindow.Loader.Url = "../report/Report_Dialysis_h.aspx?_REPORT_NAME=drug_term&_PAT_IC=" + Patient_ID.Text + "&_REPORT_P=long";
            PrintWindow.Loader.DisableCaching = true;
            PrintWindow.LoadContent();
        }
    }
}