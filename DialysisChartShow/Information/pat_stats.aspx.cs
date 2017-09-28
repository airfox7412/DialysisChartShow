using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;

namespace Dialysis_Chart_Show.Information
{
    public partial class pat_stats : BaseForm 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            inser_age();
            age_show();
        }
        protected int[] AgeStatis(DataTable dt)
        {
            int[] A = { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                try
                {
                    string dob = dt.Rows[i]["pif_dob"].ToString();
                    dob = dob.Substring(0, 4);
                    string year = DateTime.Now.ToString("yyyy");
                    int age = Int32.Parse(year) - Int32.Parse(dob);

                    if (age < 10)
                    {
                        A[0] += 1;
                    }
                    else if (age < 20)
                    {
                        A[1] += 1;
                    }
                    else if (age < 30)
                    {
                        A[2] += 1;
                    }
                    else if (age < 40)
                    {
                        A[3] += 1;
                    }
                    else if (age < 50)
                    {
                        A[4] += 1;
                    }
                    else if (age < 60)
                    {
                        A[5] += 1;
                    }
                    else if (age < 70)
                    {
                        A[6] += 1;
                    }
                    else if (age < 80)
                    {
                        A[7] += 1;
                    }
                    else if (age < 90)
                    {
                        A[8] += 1;
                    }
                    else if (age < 100)
                    {
                        A[9] += 1;
                    }
                    else
                    {
                        A[10] += 1;
                    }
                }
                catch { }
                
            }
            return A;

        }
        protected void inser_age()
        {
            string[] A = { "0~9", "10~19", "20~29", "30~39", "40~49", "50~59", "60~69", "70~79", "80~89", "90~99", "99以上" };
            string sql = "SELECT a.pif_dob from pat_info a WHERE a.pif_sex='M'";//男
            DataTable dt = db.Query(sql);
            int[] B = AgeStatis(dt);

            sql = "SELECT a.pif_dob from pat_info a WHERE a.pif_sex='F'";//女
            dt = db.Query(sql);
            int[] C = AgeStatis(dt);
            sql = "TRUNCATE TABLE pat_stats";
            db.Excute(sql);
            for (int i = 0; i < B.Length; i++)
            {
                string D = (B[i] + C[i]).ToString();
                sql = "INSERT pat_stats (age,male,female,total) VALUES('" + A[i] + "','" + B[i].ToString() + "','" + C[i].ToString() + "','" + D.ToString() + "')";
                db.Excute(sql);
            }
        }
        protected void age_show()
        {
            string[] col_0 = { "年齡", "age" };
            string[] col_1 = { "男", "male" };
            string[] col_2 = { "女", "female" };
            string[] col_3 = { "小計", "total" };

            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);
            acol.Add(col_3);
            string ihtml;

            string sql;
            sql = "SELECT a.age,a.female,a.male,a.total FROM pat_stats a";
            DataTable dt= db.Query(sql);

            ihtml = "<br>";
            ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?editmode=edit&editmode2=add';return false;\">添加</a><br>";
            ihtml += "<link href=\"CSS.css\" rel=\"stylesheet\" type=\"text/css\">";
            ihtml += "<div id=\"Table2\" align=left style=\"OVERFLOW: auto; HEIGHT: 450\">";
            ihtml += "<table border=\"1\" >";

            ihtml += "<tr>";
            //ihtml += "<td  >操作</td>";
            for (int i = 0; i < acol.Count; i++)
            {
                string[] a = (string[])acol[i];
                ihtml += "<td  >" + a[0] + "</td>";
            }
            ihtml += "</tr>";

            foreach (DataRow irow in dt.Rows)
            {
                ihtml += "<tr>";
                ihtml += "<td >";
                //ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?editmode=show&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 浏览 </a>";
                //ihtml += "  ";
                //ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?&editmode=edit&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 修改 </a>";
                //ihtml += "  ";
                //ihtml += "<a href=\"javascript:void(0);\" oncontextmenu=\"event.returnValue=false;\" onclick=\"window.location.href='?&editmode=delete&sel_info_date=" + irow["info_date"].ToString() + "';return false;\"> 删除 </a>";
                ihtml += "  ";
                ihtml += "</td>";

                for (int i = 0; i < acol.Count; i++)
                {
                    string[] selcol = (string[])acol[i];
                    ihtml += "<td >";
                    //ihtml += _Get_Column_Value(selcol, irow);
                    ihtml += "</td>";
                }

                ihtml += "</tr>";
            }


            ihtml += "</table>";
            ihtml += "</div>";
            Response.Write(ihtml);
        }
    }
}