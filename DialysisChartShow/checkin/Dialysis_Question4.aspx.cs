using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.checkin
{
    public partial class Dialysis_Question4 : BaseForm
    {
        private string _TableName = "zinfo_a_09";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Show_Person(); //顯示病患資料
                _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            string info_date = DateTime.Now.ToString("yyyy-MM-dd");
            string info_user = Session["USER_ID"].ToString();
            _UserID = info_user;
            _zInfo_Save(_TableName, _PAT_ID, info_date);
        }


        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }
        
        #region 顯示病患資料
        protected void Show_Person()
        {
            string iimage = "";
            string ipath = "../";
            String sSQL;
            sSQL = "SELECT pif_id, pif_ic, pif_name, pif_sex AS PERSON_SEX, pif_dob AS PERSON_AGE, pif_imgloc AS PAT_IMG, B.txt_9 AS Clinic, C.cln1_col2 AS BED_NO ";
            sSQL += "FROM pat_info A ";
            sSQL += "LEFT JOIN zinfo_f_011 B ON A.pif_id=B.pat_id ";
            sSQL += "LEFT JOIN clinical1_nurse C ON A.pif_ic=C.cln1_patic ";
            sSQL += "WHERE A.pif_ic='" + _PAT_IC + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                Patient_Name.Text = dt.Rows[0]["pif_name"].ToString();
                int thisyear = int.Parse(DateTime.Now.ToString("yyyy"));
                int age = thisyear - int.Parse(dt.Rows[0]["PERSON_AGE"].ToString().Substring(0, 4));
                Patient_Age.Text = age.ToString();
                if (dt.Rows[0]["PAT_IMG"].ToString() != "")
                {
                    iimage = dt.Rows[0]["PAT_IMG"].ToString().Replace("./", "");
                }
                else
                {
                    if (dt.Rows[0]["PERSON_SEX"].ToString() == "M" || dt.Rows[0]["PERSON_SEX"].ToString() == "")
                    {
                        iimage = "images/male.png";
                    }
                    else
                    {
                        iimage = "images/female.png";
                    }
                }
                Image1.ImageUrl = ipath + iimage;
                Clinic.Text = dt.Rows[0]["Clinic"].ToString();
                bedno.Text = dt.Rows[0]["BED_NO"].ToString();
            }
        }
        #endregion

        protected void Btn_Print_Click(object Sender, DirectEventArgs e)
        {
            PrintWindow.Show();
            PrintWindow.Loader.SuspendScripting();
            PrintWindow.Loader.Url = "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_ID + "&_REPORT_NAME=A09";
            PrintWindow.Loader.DisableCaching = true;
            PrintWindow.LoadContent();
        }
    }
}