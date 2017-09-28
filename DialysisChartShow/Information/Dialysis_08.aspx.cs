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
    public partial class Dialysis_08 : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            link();
        }

        protected void A01(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_01_01.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void A02(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_01_02.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void A03(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_01_03.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void A04(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_01_04.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void A05(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_01_05.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void A06(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_01_06.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void A07(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_01_07.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void A08(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_081.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        protected void B01(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_02_01.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void B02(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_02_02.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void B03(object sender, EventArgs e)
        {
            //Window1.Show();
            //Window1.Loader.SuspendScripting();
            //Window1.Loader.Url = "./Dialysis_02_03.aspx?editmode=list";
            //Window1.Loader.DisableCaching = true;
            //Window1.LoadContent();
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_02_031.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void B04(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            //Window1.Loader.Url = "./Dialysis_02_04.aspx?editmode=list";
            Window1.Loader.Url = "./Dialysis_02_041.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void B05(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_02_05.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void B06(object sender, EventArgs e)
        {
            string gtoday = DateTime.Now.ToString("yyyy-MM-dd");
            string gyear = gtoday.Substring(0, 4);
            string gmonth = gtoday.Substring(5, 2);

            Window1.Show();
            Window1.Loader.SuspendScripting();
            //Window1.Loader.Url = "./Dialysis_02_06.aspx?editmode=list";
            Window1.Loader.Url = "./dry_weight.aspx?gyr=" + gyear + "&gmth=" + gmonth;
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();

            //Window1.Show();
            //Window1.Loader.SuspendScripting();
            //Window1.Loader.Url = "./Dialysis_02_06.aspx?editmode=list";
            //Window1.Loader.DisableCaching = true;
            //Window1.LoadContent();
        }
        protected void B07(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_02_07.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        protected void C01(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_03_01.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void C02(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_03_02.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void C03(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_03_03.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void C04(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_03_04.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void C05(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_03_05.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void C06(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_03_06.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void C07(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_03_07.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        protected void C09(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./drug_list.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        protected void D01(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            //Window1.Loader.Url = "./Dialysis_04_01.aspx?editmode=list";
            Window1.Loader.Url = "./Dialysis_04.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void D02(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_04_02.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        protected void E01(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_05_01.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void E02(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_05_02.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        protected void F01(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_06_011.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void F02(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_06_012.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void F03(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_06_02.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void F04(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_06_04.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void F05(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_06_05.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void F06(object sender, EventArgs e)
        {
            //Window1.Show();
            //Window1.SuspendScripting();
            //Window1.Loader.Url = "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_IC + "&_REPORT_NAME=13";
            //Window1.Loader.DisableCaching = true;
            //Window1.LoadContent();
            //Dialysis_06 MyDialysis_06 = new Dialysis_06();
            //MyDialysis_06.reload_page6Rep();
            //ScriptManager.RegisterStartupScript(this.GetType(), "newWindow", String.Format("<script>window.open('{0}');</script>", "../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_IC + "&_REPORT_NAME=13"));
            //ScriptManager.RegisterStartupScript(this, typeof(string), "OPEN_WINDOW", "var Mleft = (screen.width/2)-(760/2);var Mtop = (screen.height/2)-(700/2);window.open('../report/Report_Dialysis_h.aspx?_PAT_ID=" + _PAT_IC + "&_REPORT_NAME=13"', null, 'height=700,width=760,status=yes,toolbar=no,scrollbars=yes,menubar=no,location=no,top=\'+Mtop+\', left=\'+Mleft+\'' );", true);
        
        }

        //protected void G01(object sender, EventArgs e)
        //{
        //    Window1.Show();
        //    Window1.Loader.SuspendScripting();
        //    Window1.Loader.Url = "./Dialysis_07_01.aspx?editmode=list";
        //    Window1.Loader.DisableCaching = true;
        //    Window1.LoadContent();
        //}

        // 2014-04-11 ada add begin
        protected void h01(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_0h_01.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void h02(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_0h_02.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void h03(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_0h_03.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void h04(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_0h_04.aspx?editmode=list";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        // 2014-04-11 ada add end

        protected void K01(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_09_01.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void K02(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_09_02.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }
        protected void K03(object sender, EventArgs e)
        {
            Window1.Show();
            Window1.Loader.SuspendScripting();
            Window1.Loader.Url = "./Dialysis_09_03.aspx";
            Window1.Loader.DisableCaching = true;
            Window1.LoadContent();
        }

        public void link()
        {
            //for (int i = 1; i < 8; i++)
            for (int i = 1; i < 9; i++)
            {
                string sql = "SELECT * FROM zinfo_a_0" + i + " a WHERE a.pat_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            zinfo_a_01.Visible = true;
                            break;
                        case 2:
                            zinfo_a_02.Visible = true;
                            break;
                        case 3:
                            zinfo_a_03.Visible = true;
                            break;
                        case 4:
                            zinfo_a_04.Visible = true;
                            break;
                        case 5:
                            zinfo_a_05.Visible = true;
                            break;
                        case 6:
                            zinfo_a_06.Visible = true;
                            break;
                        case 7:
                            zinfo_a_07.Visible = true;
                            break;
                        case 8:
                            zinfo_a_08.Visible = true;
                            break;
                    }
                }
            }

            for (int i = 1; i < 8; i++)
            {
                string sql = "SELECT * FROM zinfo_b_0" + i + " a WHERE a.pat_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            zinfo_b_01.Visible = true;
                            break;
                        case 2:
                            zinfo_b_02.Visible = true;
                            break;
                        case 3:
                            zinfo_b_03.Visible = true;
                            break;
                        case 4:
                            //zinfo_b_04.Visible = true;
                            break;
                        case 5:
                            zinfo_b_05.Visible = true;
                            break;
                        case 6:
                            zinfo_b_06.Visible = true;
                            break;
                        case 7:
                            zinfo_b_07.Visible = true;
                            break;
                    }
                }
            }
            DataTable dtB04 = db.Query("SELECT * FROM a_result_log WHERE PAT_NO='" + _PAT_ID + "' AND RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='G003') ");
            if (dtB04.Rows.Count > 0)
                zinfo_b_04.Visible = true;
            else
                zinfo_b_04.Visible = false;

            
            for (int i = 1; i < 8; i++)
            {
                string sql = "SELECT * FROM zinfo_c_0" + i + " a WHERE a.pat_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            //zinfo_c_01.Visible = true;
                            break;
                        case 2:
                            //zinfo_c_02.Visible = true;
                            break;
                        case 3:
                            //zinfo_c_03.Visible = true;
                            break;
                        case 4:
                            //zinfo_c_04.Visible = true;
                            break;
                        case 5:
                            //zinfo_c_05.Visible = true;
                            break;
                        case 6:
                            //zinfo_c_06.Visible = true;
                            break;
                        case 7:
                            //zinfo_c_07.Visible = true;
                            break;
                    }
                }
            }

            for (int i = 1; i < 3; i++)
            {
                string sql = "SELECT * FROM zinfo_d_0" + i + " a WHERE a.pat_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            zinfo_d_01.Visible = true;
                            break;
                        case 2:
                            zinfo_d_02.Visible = true;
                            break;
                    }
                }
            }
            DataTable dtD01 = db.Query("SELECT * FROM a_result_log WHERE PAT_NO='" + _PAT_ID + "' AND RESULT_CODE IN (SELECT OITEM_CODE AS RITEM_CODE FROM a_item_group WHERE GROUP_CODE='G001') ");
            if (dtD01.Rows.Count > 0)
                zinfo_d_01.Visible = true;
            else
                zinfo_d_01.Visible = false;

            for (int i = 1; i < 3; i++)
            {
                string sql = "SELECT * FROM zinfo_e_0" + i + " a WHERE a.pat_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            zinfo_e_01.Visible = true;
                            break;
                        case 2:
                            zinfo_e_02.Visible = true;
                            break;
                    }
                }
            }

            for (int i = 1; i < 5; i++)
            {
                string sql = "SELECT * FROM zinfo_h_0" + i + " a WHERE a.pat_id='" + _PAT_ID + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                {
                    switch (i)
                    {
                        case 1:
                            zinfo_h_01.Visible = true;
                            break;
                        case 2:
                            zinfo_h_02.Visible = true;
                            break;
                        case 3:
                            zinfo_h_03.Visible = true;
                            break;
                        case 4:
                            zinfo_h_04.Visible = true;
                            break;
                    }
                }
            }

            string sql2 = "SELECT * FROM zinfo_f_011 a WHERE a.pat_id='" + _PAT_ID + "'";
            DataTable dt2 = db.Query(sql2);
            if (dt2.Rows.Count > 0)
            {
                zinfo_f_01.Visible = true;
            }

            sql2 = "SELECT * FROM zinfo_f_012 a WHERE a.pat_id='" + _PAT_ID + "'";
            dt2 = db.Query(sql2);
            if (dt2.Rows.Count > 0)
            {
                zinfo_f_02.Visible = true;
            }
            
            sql2 = "SELECT * FROM zinfo_f_02 a WHERE a.pat_id='" + _PAT_ID + "'";
            dt2 = db.Query(sql2);
            if (dt2.Rows.Count > 0)
            {
                zinfo_f_03.Visible = true;
            }
            
            sql2 = "SELECT * FROM zinfo_f_04 a WHERE a.pat_id='" + _PAT_ID + "'";
            dt2 = db.Query(sql2);
            if (dt2.Rows.Count > 0)
            {
                zinfo_f_04.Visible = true;
            }
            
            sql2 = "SELECT * FROM zinfo_f_05 a WHERE a.pat_id='" + _PAT_ID + "'";
            dt2 = db.Query(sql2);
            if (dt2.Rows.Count > 0)
            {
                zinfo_f_05.Visible = true;
            }

            //sql2 = "SELECT * FROM zinfo_f_05 a WHERE a.pat_id='" + _PAT_ID + "'";
            //dt2 = db.Query(sql2);
            //if (dt2.Rows.Count > 0)
            //{
            //    zinfo_f_06.Visible = true;
            //}

            drug_list.Visible = false;
            DataTable dtLONG = db.Query("SELECT * FROM longterm_ordermgt WHERE lgord_patic='" + _PAT_IC + "' ");
            if (dtLONG.Rows.Count > 0)
                drug_list.Visible = true;
            DataTable dtSHORT = db.Query("SELECT * FROM shortterm_ordermgt WHERE shord_patic='" + _PAT_IC + "' ");
            if (dtSHORT.Rows.Count > 0)
                drug_list.Visible = true;
                

            DataTable dtK01 = db.Query("SELECT * FROM clinical1_nurse WHERE cln1_patic='" + _PAT_IC + "' ");
            if (dtK01.Rows.Count > 0)
                Dialysis_09_01.Visible = true;
            else
                Dialysis_09_01.Visible = false;

            DataTable dtK02 = db.Query("SELECT DISTINCT DATE_FORMAT(dialysis_date, '%Y-%m-%d') FROM data_list WHERE person_id='" + _PAT_IC + "' ");
            if (dtK01.Rows.Count > 0)
            {
                Dialysis_09_02.Visible = true;
                Dialysis_09_03.Visible = true;
            }
            else
            {
                Dialysis_09_02.Visible = false;
                Dialysis_09_03.Visible = false;
            }
        }
    }
}