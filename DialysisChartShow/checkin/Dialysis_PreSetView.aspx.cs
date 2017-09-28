using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Security.Cryptography;
using Ext.Net;
using Ext.Net.Utilities;

namespace Dialysis_Chart_Show.checkin
{
    public partial class Dialysis_PreSetView : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Patient_ID.Text = _PAT_IC; //Request.QueryString["pid"];
                if (Patient_ID.Text != "")
                    GetTabPanel();
            }
        }

        protected void GetTabPanel()
        {
            int a = 0;
            int i, j, daytype, dweek;
            string tname = "处方底板";
            AddTabBase(tname);

            string toDay = DateTime.Now.ToString("yyyy-MM-dd");
            string sSQL;

            string[] DateArray = new string[14];
            string[] DateArray_Sort = new string[14];

            //雙周排班
            sSQL = "SELECT a.appointment_date FROM appointment a ";
            sSQL += "LEFT JOIN pat_info b ON a.pif_id=b.pif_id ";
            sSQL += "WHERE b.pif_ic='" + Patient_ID.Text + "' AND a.appointment_date>='" + toDay + "' ";
            sSQL += "ORDER BY a.appointment_date";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (a = 0; a < dt.Rows.Count; a++)
                {
                    tname = dt.Rows[a]["appointment_date"].ToString();
                    DateArray[a] = tname + ChinaWeek((int)Convert.ToDateTime(tname).DayOfWeek) + "-";
                }
            }

            //預約排班
            sSQL = "SELECT a.apptst_patic, a.apptst_daytyp FROM appointment_setup a ";
            sSQL += "LEFT JOIN pat_info b ON a.apptst_patic=b.pif_ic ";
            sSQL += "WHERE b.pif_ic='" + Patient_ID.Text + "' ";
            sSQL += "ORDER BY a.apptst_daytyp";
            dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                for (j = 0; j < 7; j++)
                {
                    tname = DateTime.Now.AddDays(j + 1).ToString("yyyy-MM-dd");
                    dweek = (int)Convert.ToDateTime(tname).DayOfWeek;
                    for (i = 0; i < dt.Rows.Count; i++)
                    {
                        daytype = int.Parse(dt.Rows[i]["apptst_daytyp"].ToString());
                        if (daytype == dweek)
                        {
                            DateArray[a] = tname + ChinaWeek(dweek);
                            a++;
                            break;
                        }
                    }
                }
            }
            Array.Sort<string>(DateArray);
            string str1 = "";
            string str2 = "";
            for (a = 0; a < DateArray.Length; a++)
            {
                if (DateArray[a] != null)
                {
                    str1 = DateArray[a];
                    if (str1.Substring(0, 13) != str2)
                    {
                        AddTab(DateArray[a]);
                        str2 = str1.Substring(0, 13);
                    }
                }
            }
            TabPanel1.SetPreviousTabAsActive();
        }

        protected string ChinaWeek(int week)
        {
            string weekString = "";
            switch (week)
            {
                case 1:
                    weekString = "(一)";
                    break;
                case 2:
                    weekString = "(二)";
                    break;
                case 3:
                    weekString = "(三)";
                    break;
                case 4:
                    weekString = "(四)";
                    break;
                case 5:
                    weekString = "(五)";
                    break;
                case 6:
                    weekString = "(六)";
                    break;
                case 7:
                    weekString = "(日)";
                    break;
                default:
                    weekString = "(日)";
                    break;
            }
            return weekString;
        }

        protected void AddTabBase(string title)
        {
            Ext.Net.Panel panel = new Ext.Net.Panel
            {
                Title = title,
                Icon = Ext.Net.Icon.BookOpen,
                Closable = false,
                Layout = "Fit",
                TabConfig = new Ext.Net.Button()
                {
                    UI = UI.Success
                },
                Loader = new ComponentLoader
                {
                    Mode = LoadMode.Frame,
                    Url = "Dialysis_PreSetBase.aspx?personid=" + Patient_ID.Text + "&sdate=" + title
                }
            };

            panel.Loader.LoadMask.ShowMask = true;
            panel.Loader.LoadMask.Msg = "读取中..."; 
            if (CheckSchMod("base") == false)
            {
                panel.TabConfig.UI = Ext.Net.UI.Warning;
            }
            TabPanel1.Add(panel);
            panel.Render();
        }

        protected void AddTab(string title)
        {
            Ext.Net.Panel panel = new Ext.Net.Panel
            {
                Title = title.Substring(0, 13),
                Icon = Ext.Net.Icon.CalendarSelectDay,
                Closable = false,
                Layout = "Fit",
                TabConfig = new Ext.Net.Button() 
                { 
                    UI = UI.Success 
                },
                Loader = new ComponentLoader
                {
                    Mode = LoadMode.Frame,
                    Url = "Dialysis_PreSetSch.aspx?personid=" + Patient_ID.Text + "&sdate=" + title.Substring(0, 10)
                }
            };

            panel.Loader.LoadMask.ShowMask = true;
            panel.Loader.LoadMask.Msg = "读取中..."; 
            if (title.Length > 13)
            {
                if (title.Substring(13, 1) == "-")
                    panel.Icon = Ext.Net.Icon.Stop;
            }
            if (CheckSchMod(title.Substring(0, 10)) == false)
            {
                panel.TabConfig.UI = Ext.Net.UI.Warning;
            }

            TabPanel1.Add(panel);
            panel.Render();
        }

        protected Boolean CheckSchMod(string mdate)
        {
            string sSQL = "SELECT cln1_diadate FROM clinical1_doc_henan ";
            sSQL += "WHERE cln1_patic='" + Patient_ID.Text + "' AND cln1_diadate='" + mdate + "' ";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
                return true;
            else
                return false;
        }

        protected void tab1_activate(object sender, DirectEventArgs e)
        {
            string url = "Dialysis_PreSetLong.aspx?pid=" + Patient_ID.Text;
            Panel_Long.Loader.SuspendScripting();
            Panel_Long.Loader.Url = url;
            Panel_Long.Loader.DisableCaching = true;
            Panel_Long.LoadContent();
        }

        protected void tab2_activate(object sender, DirectEventArgs e)
        {
            string url = "Dialysis_PreSetShort.aspx?pid=" + Patient_ID.Text;
            Panel_Short.Loader.SuspendScripting();
            Panel_Short.Loader.Url = url;
            Panel_Short.Loader.DisableCaching = true;
            Panel_Short.LoadContent();
        }
    }
}