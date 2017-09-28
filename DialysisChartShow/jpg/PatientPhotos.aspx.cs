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

namespace Dialysis_Chart_Show.jpg
{
    public partial class PatientPhotos : System.Web.UI.Page
    {
        string floor;
        string sec;
        string time;
        string bedno;
        protected void Page_Load(object sender, EventArgs e)
        {
            DBMysql db = new DBMysql();
            try
            {
                

                floor = Request["pv_floor"].ToString();
                sec = Request["pv_sec"].ToString();

                string dt1 = "12:00:01";
                string dt2 = "18:00:01";

                time = DateTime.Now.ToString("HH:mm:ss");
                if (time.CompareTo(dt1) < 0)
                {
                    time="001";
                }
                else if(time.CompareTo(dt2) < 0)
                {
                    time="002";
                }
                else
                {
                    time="003";
                }


                bedno = Request["pv_bedno"].ToString();
                string sql = "select b.pif_imgloc ,b.pif_name ,b.pif_ic ,b.pif_dob ,b.pif_sex from pat_visit a ,pat_info b ";
                sql += "where a.pv_ic=b.pif_ic and a.pv_floor='" + floor + "' and a.pv_sec='" + sec + "' and a.pv_time='" + time + "' and a.pv_bedno='" + bedno + "'";
                DataTable dt = db.Query(sql);
                string iimage = dt.Rows[0]["pif_imgloc"].ToString();
                iimage = iimage.Replace("./patimages/", "").Replace("./images/", "");

                string ipath = ConfigurationManager.AppSettings["pat_images"].ToString();
                Image1.ImageUrl = ipath + iimage;

                string dateDOB = dt.Rows[0]["pif_dob"].ToString();
                dateDOB = dateDOB.Remove(4, 1);
                dateDOB = dateDOB.Remove(6, 1);
                int now = int.Parse(DateTime.Today.ToString("yyyyMMdd"));
                int dob = int.Parse(dateDOB);
                string dif = (now - dob).ToString();
                string age = "0";
                if (dif.Length > 4)
                    age = dif.Substring(0, dif.Length - 4);


                Lab_pv_floor.Text = "楼层:" + floor + "";
                Lab_pv_sec.Text = "区:" + sec + "";
                Lab_pv_bedno.Text = "床号" + bedno + "";
                Lab_pif_name.Text = "姓名:" + dt.Rows[0]["pif_name"].ToString() + "";
                Lab_pif_ic.Text = "身分证:" + dt.Rows[0]["pif_ic"].ToString() + "";
                Lab_age.Text = "年龄:" + age + "";
                Lab_pif_sex.Text = "性别:" + dt.Rows[0]["pif_sex"].ToString() + "";


            }
            catch
            {

            }
        }
    }
}