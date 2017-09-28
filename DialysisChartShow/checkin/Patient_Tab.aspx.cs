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

namespace Dialysis_Chart_Show.checkin
{
    public partial class Patient_Tab : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();
        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                patient_id.Text = Request.QueryString["personid"];
                patient_name.Text = Request.QueryString["patient_name"];
                machine_type.Text = Request.QueryString["machine_type"];
                machine_model.Text = Request.QueryString["mechine_model"];
                bedno.Text = Request.QueryString["bedno"];
                floor.Text = Request.QueryString["floor"];
                area.Text = Request.QueryString["area"];
                time.Text = Request.QueryString["time"];
                daytyp.Text = Request.QueryString["daytyp"];
                hpack.Text = Request.QueryString["hpack"];
                hpack3.Text = Request.QueryString["hpack3"];
                patient_weight.Text = Request.QueryString["patient_weight"];
                AddTab(TabPanel1, "信息");
                AddTab(TabPanel1, "医嘱");
                AddTab(TabPanel1, "纪录");
                TabPanel1.SetPreviousTabAsActive();
            }
        }

        protected void AddTab(TabPanel theTabPanel, string title)
        {
            Ext.Net.Panel panel = new Ext.Net.Panel
            {
                Title = title,
                Icon = Ext.Net.Icon.Application,
                Closable = false,
                Layout = "Fit",
                Loader = new ComponentLoader
                {
                    Mode = LoadMode.Frame,
                    Url = "Patient_detail.aspx?personid=" + patient_id.Text + "&patient_name=" + patient_name.Text + "&machine_type=" + machine_type.Text + "&floor=" + floor.Text +
                        "&area=" + area.Text + "&time=" + time.Text + "&bedno=" + bedno.Text + "&daytyp=" + daytyp.Text + "&sdate=" + toDay
                }
            };
            theTabPanel.Add(panel);
            panel.Render();
        }
    }
}