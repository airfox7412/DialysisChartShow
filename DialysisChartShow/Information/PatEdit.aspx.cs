using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class PatEdit : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                string sql = "SELECT a.pif_ic, a.pif_name,a.pif_sex,a.pif_dob,a.pif_address,a.pif_mrn,a.pif_contactperson,a.pif_contact ,c.info_survey_date";
                sql += " FROM pat_info a left join zinfo_maim c on a.pif_id=c.pat_id  WHERE pif_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sql);
                pif_ic.Text = dt.Rows[0]["pif_ic"].ToString();
                pif_name.Text = dt.Rows[0]["pif_name"].ToString();
                pif_sex.Text = dt.Rows[0]["pif_sex"].ToString();
                pif_dob.Text = dt.Rows[0]["pif_dob"].ToString();
                pif_address.Text = dt.Rows[0]["pif_address"].ToString();
                pif_mrn.Text = dt.Rows[0]["pif_mrn"].ToString();
                pif_contactperson.Text = dt.Rows[0]["pif_contactperson"].ToString();
                pif_contact.Text = dt.Rows[0]["pif_contact"].ToString();
                info_survey_date.Text = dt.Rows[0]["info_survey_date"].ToString();

            }
        }
        /// <summary>
        /// 儲存
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Submit_Click(object sender, DirectEventArgs e)
        {
            try 
            {
                Save_zInfo_Main(_PAT_ID,_Get_YMD(info_survey_date.Text));

                string sql = "SELECT a.pif_ic,a.pif_name,a.pif_sex,a.pif_dob,a.pif_address,a.pif_mrn,a.pif_contactperson,a.pif_contact ";
                sql += "FROM pat_info a WHERE pif_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count == 0)
                {
                    sql = "INSERT INTO pat_info (pif_ic,pif_name,pif_sex,pif_dob,pif_address,pif_mrn,pif_contactperson,pif_contact) ";
                    sql += "VALUES('" + pif_ic.Text + "','" + pif_name.Text + "','" + pif_sex.Text + "','" + pif_dob.RawText + "','" + pif_address.Text + "','" + pif_mrn.Text + "','" + pif_contactperson + "','" + pif_contact + "')";
                    db.Excute(sql);
                    
                    
                    _NotificationShow("信息储存完成");
                }
                else
                {
                    sql = "update pat_info ";
                    sql += " set pif_name = '" + pif_name.Text + "' ,";
                    sql += "pif_ic='" + pif_ic.Text + "' ,";
                    sql += " pif_sex = '" + pif_sex.Text + "' ,";
                    sql += " pif_dob = '" + pif_dob.RawText + "' ,";
                    sql += " pif_address = '" + pif_address.Text + "' ,";
                    sql += " pif_mrn = '" + pif_mrn.Text + "' ,";
                    sql += " pif_contactperson = '" + pif_contactperson.Text + "' ,";
                    sql += " pif_contact = '" + pif_contact.Text + "' ";
                    sql += " where pif_id = '" + _PAT_ID + "' ";
                    db.Excute(sql);

                    //sql = "INSERT INTO zinfo_maim (pat_id,info_survey_date) ";
                    //sql += "VALUES('" + _PAT_ID + "','" + info_survey_date.RawText + "')";
                    //db.Excute(sql);
                    _NotificationShow("信息储存完成");
                }
            }
            catch (Exception ex)
            {
                _ErrorMsgShow(ex.Message.ToString());
            }
        }

        protected void Save_zInfo_Main(string pat_id,string survey_date)
        {
            string sql ;
            sql="select * from zinfo_maim where pat_id='" + pat_id + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count ==0)
            {
                sql ="insert into zinfo_maim (pat_id,info_survey_date) values ('" + pat_id + "','" + survey_date + "')";
            }
            else
            {
                sql ="update zinfo_maim set info_survey_date='" + survey_date + "' where pat_id='" + pat_id + "'";
            }
            db.Excute(sql);
        }
        /// <summary>
        /// 回上一頁
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void Btn_Back_Click(object sender, DirectEventArgs e)
        {
            X.Redirect("Info_index.aspx");
        }
    }
}