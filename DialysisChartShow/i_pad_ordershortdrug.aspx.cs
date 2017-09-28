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
using System.Text;
using System.Security.Cryptography;
using System.IO;

namespace Dialysis_Chart_Show
{
    public partial class i_pad_doclogin : System.Web.UI.Page
    {
       DBMysql db = new DBMysql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                System.Data.DataTable dtDRUGGRP = db.Query("SELECT distinct drg_grp FROM drug_list");
                for (int i = 0; i < dtDRUGGRP.Rows.Count; i++)
                {
                    Ext.Net.ListItem litem;
                    litem = new Ext.Net.ListItem(dtDRUGGRP.Rows[i]["drg_grp"].ToString());
                    this.cbo_druggrp.Items.Add(litem);
                }
                this.cbo_druggrp.GetStore().DataBind();

                System.Data.DataTable dtDRUGFREQ = db.Query("SELECT genst_desc FROM general_setup where genst_ctg = 'drgfreq'");
                for (int k = 0; k < dtDRUGFREQ.Rows.Count; k++)
                {
                    Ext.Net.ListItem litem2;
                    litem2 = new Ext.Net.ListItem(dtDRUGFREQ.Rows[k]["genst_desc"].ToString());
                    this.cmb_ordfreq.Items.Add(litem2);
                }
                this.cmb_ordfreq.GetStore().DataBind();

                //給藥方式
                System.Data.DataTable dtDRUGFRER = db.Query("SELECT med_name FROM med_way");
                for (int k = 0; k < dtDRUGFRER.Rows.Count; k++)
                {
                    Ext.Net.ListItem litem3;
                    litem3 = new Ext.Net.ListItem(dtDRUGFRER.Rows[k]["med_name"].ToString());
                    this.cmd_medway.Items.Add(litem3);
                }
                this.cmd_medway.GetStore().DataBind();


                patient_id.Text = Request.QueryString["person_id"];

                patient_name.Text = Request.QueryString["patient_name"];
                patient_sex.Text = Request.QueryString["pat_sex"];
                txt_orddoc.Text = Request.QueryString["pat_docname"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                bedno.Text = Request.QueryString["bedno"];
                daytyp.Text = Request.QueryString["daytyp"];
                if (patient_name.Text == null)
                {
                    patient_name.Text = Request.QueryString["amp;patient_name"];
                    patient_sex.Text = Request.QueryString["amp;pat_sex"];
                    txt_orddoc.Text = Request.QueryString["amp;pat_docname"];
                    floor.Text = Request.QueryString["amp;floor"];
                    area.Text = Request.QueryString["amp;area"];
                    time.Text = Request.QueryString["amp;time"];
                    bedno.Text = Request.QueryString["amp;bedno"];
                    daytyp.Text = Request.QueryString["amp;daytyp"];
                }
                

                txt_orddate.Text = DateTime.Now.ToString("yyyy-MM-dd");
                txt_ordtime.Text = DateTime.Now.ToString("HH:mm");

                show();
                Show_shortdrughistory();
            }

            
        }
        protected void show()
        {
            Label2.Text = patient_name.Text;
            Label4.Text = patient_id.Text;
            Label6.Text = patient_sex.Text;
        }

        protected void Show_shortdrughistory()
        {
            DBMysql db = new DBMysql();
            string sql;

            //old sql = "SELECT a.shord_id,a.shord_dateord,a.shord_timeord,a.shord_usr1,b.drg_name,a.shord_intake,a.shord_freq,a.shord_comment,c.genst_desc ";
            //2015.01.20 andy 短期醫囑:增加給藥方式 執行護士 顯示
            sql = "SELECT a.shord_id,a.shord_dateord,a.shord_timeord,a.shord_usr1,b.drg_name,a.shord_intake,a.shord_freq,a.shord_medway,a.shord_nurs,c.genst_desc,a.shord_comment ";
            sql += "from shortterm_ordermgt a,drug_list b, general_setup c ";
            sql += "where a.shord_patic = '" + patient_id.Text+ "' ";
            sql += "and a.shord_drug = b.drg_code ";
            sql += "and c.genst_ctg = 'ActiveStatus' and a.shord_actst = c.genst_code order by a.shord_id DESC";

            DataTable dt = db.Query(sql);

            Store istore2 = Grid_Show_ORDSHORT.GetStore();
            istore2.DataSource = db.GetDataArray(dt);
            istore2.DataBind();

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

        //2015.01.20 andy 護士名稱
        //protected void cmb_stfcode(object sender, DirectEventArgs e)
        //{
        //    BaseForm bs = new BaseForm();
        //    DBMysql db = new DBMysql();

        //    string sql2 = "SELECT acclv_fname FROM access_level where acclv_stfcode = '" + txt_nuser_stfcode.Text + "'";           

        //    DataTable dt2 = db.Query(sql2);
        //    if (dt2.Rows.Count != 0)
        //    {
        //        txt_nuser_name.Text = dt2.Rows[0]["acclv_fname"].ToString();
        //    }
        //    else
        //    {
        //        txt_nuser_name.Text = "";
        //    }
        //}




        protected void cmb_onchange(object sender, DirectEventArgs e)
        {
            BaseForm bs = new BaseForm();
            //sFLOOR.Text = bs.GetComboBoxValu(this.cbo_druggrp);
            FILL_DRGLIST(bs.GetComboBoxValue(this.cbo_druggrp));
        }

        protected void FILL_DRGLIST(string _drggrp)
        {

            DBMysql db = new DBMysql();

            string sSQL = "SELECT drg_name FROM drug_list where drg_grp = '" + _drggrp + "'";

            System.Data.DataTable dtDRUGLIST = db.Query(sSQL);
            for (int k = 0; k < dtDRUGLIST.Rows.Count; k++)
            {
                Ext.Net.ListItem litem2;
                litem2 = new Ext.Net.ListItem(dtDRUGLIST.Rows[k]["drg_name"].ToString());
                this.cbo_druglist.Items.Add(litem2);
            }
            this.cbo_druglist.GetStore().DataBind();

            
        }

        //2015.01.20 andy 短期醫囑:增加給藥方式 執行護士
        //2015.04.16 andy 
        protected void Btn_save_drg_Click(object sender, DirectEventArgs e)
        {
            // DBMysql db = new DBMysql();
            string sql;
            string ildrgcode;

            //if (cbo_druggrp.Text == "" || cbo_druglist.Text == "" || txt_ordcount.Text == "" ||
            //    cmd_medway.Text == "" || cmb_ordfreq.Text == "" || txt_nuser_stfcode.Text == "")

            if (cbo_druggrp.Text == "" || cbo_druglist.Text == "" || txt_ordcount.Text == "" ||
                cmd_medway.Text == "" || cmb_ordfreq.Text == "")
            {
                Common._ErrorMsgShow("注意 : *注记为 '必填栏位' ");
                return;
            }


            string sql2 = "SELECT drg_code ";
            sql2 += "FROM drug_list ";
            sql2 += "WHERE drg_name = '" + cbo_druglist.Text + "'";
           
            DataTable dt2 = db.Query(sql2);
            if (dt2.Rows.Count != 0)
            {
                ildrgcode = dt2.Rows[0]["drg_code"].ToString();
            }
            else
            {
                ildrgcode = "";
            }


            //2015.01.20 04.22 andy 護士工號
            //string sql3 = "";
            //string wacclv_id = "";
            //sql3 = " select acclv_id from access_level where acclv_stfcode = '" + txt_nuser_stfcode.Text + "' ";
            //DataTable dt3 = db.Query(sql3);
            //if (dt3.Rows.Count != 0)
            //{
            //    wacclv_id = dt3.Rows[0]["acclv_id"].ToString();
            //}
            //else
            //{
            //    bs._ErrorMsgShow("注意.护士工号未填或不存在!");
            //    return;
            //}

            //2015.01.20 04.22 andy 護士工號
            //string sql5 = "";
            //sql5 = "SELECT  a.associate_id,a.associate_active";
            //sql5 += " FROM  associate_list a ";
            //sql5 += " WHERE a.associate_id     = '" + wacclv_id + "'";
            //sql5 += " AND   a.associate_active ='A" + "'";
            //DataTable dt5 = db.Query(sql5);
            //if (dt5.Rows.Count == 0)
            //{
            //    bs._NotificationShow("此工號已關閉，请重新输入!");
            //    return;
            //}


            //2015.01.20 andy 增加 給藥方式(med_medway) 執行護士(txt_nuser_stfcode):之前是傳空值 (護士姓名 與備註串一起)
            //txt_ordremark.Text = txt_ordremark.Text + "  ,護士姓名:" + txt_nuser_name.Text ;
            //txt_ordremark.Text = txt_ordremark.Text + "  " + txt_nuser_name.Text;
            txt_ordremark.Text = txt_ordremark.Text;

            sql = "INSERT INTO shortterm_ordermgt(shord_patic,shord_dateord,shord_timeord,shord_usr1,shord_drug,shord_actst,shord_dtactst,shord_usr2,shord_comment,shord_intake,shord_freq,shord_nurs,shord_medway) ";
            sql += "VALUES('" + patient_id.Text + "','" + txt_orddate.Text + "','" + txt_ordtime.Text + "','" + txt_orddoc.Text;
            sql += "','" + ildrgcode + "','00001','','','" + txt_ordremark.Text + "','" + txt_ordcount.Text + "','" + cmb_ordfreq.Text + "','','" + cmd_medway.Text + "')";
            //sql += "','" + ildrgcode + "','00001','','','" + txt_ordremark.Text + "','" + txt_ordcount.Text + "','" + cmb_ordfreq.Text + "','" + txt_nuser_stfcode.Text + "','" + cmd_medway.Text + "')";
            db.Excute(sql);
            Show_shortdrughistory();
            txt_ordtime.Text = DateTime.Now.ToString("HH:mm");
            txt_ordcount.Text = "";
            txt_ordremark.Text = "";
            cmb_ordfreq.Text = "";
        }


        protected void Btn_home_Click(object sender, DirectEventArgs e)
        {
            X.Redirect(ConfigurationManager.AppSettings["iPAD"].ToString().Replace("../", ""));
        }
        protected void Btn_back_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("ipad_PatientList.aspx?editmode=page1&floor=" + floor.Text +
                                                                  "&area=" + area.Text +
                                                                  "&time=" + time.Text +
                                                                  "&bedno=" + bedno.Text +
                                                                  "&dayTyp=" + daytyp.Text);
        }


        
    }
}