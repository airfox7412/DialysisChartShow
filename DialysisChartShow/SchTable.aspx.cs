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

namespace Dialysis_Chart_Show
{
    public partial class SchTable : System.Web.UI.Page
    {
        DBMysql db = new DBMysql();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                this.Store1.DataSource = new List<Project>
                 {
                    new Project("A区", "费森尤斯-FX80", "01 HD", "周全领", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("A区", "费森尤斯-FX80", "02 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("A区", "费森尤斯-FX80", "03 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("A区", "费森尤斯-FX80", "04 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("B区", "德郎-18P", "05 HDF", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("B区", "德郎-18P", "06 HDF", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("B区", "德郎-18P", "07 HDF", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("B区", "德郎-18P", "08 HDF", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("B区", "德郎-18P", "09 HDF", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("C区", "金宝-AK96", "10 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("C区", "金宝-AK96", "11 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("C区", "金宝-AK96", "12 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("C区", "金宝-AK96", "13 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("C区", "金宝-AK96", "14 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("C区", "金宝-AK96", "15 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云"),
                    new Project("C区", "金宝-AK96", "16 HD", "韩彩云", "周全领", "周全领", "韩彩云", "周全领", "周全领", "韩彩云")
                 };
                this.Store1.DataBind();
            }
        }

        public class Project
        {
            public Project(string area, string machine, string bedtype, string name1, string name2, string name3, string name4, string name5, string name6, string name7)
            {
                this.Area = area;
                this.Machine = machine;
                this.BedType = bedtype;
                this.Week1 = name1;
                this.Week2 = name2;
                this.Week3 = name3;
                this.Week4 = name4;
                this.Week5 = name5;
                this.Week6 = name6;
                this.Week7 = name7;
            }

            public string Area { get;set; }
            public string BedType { get;set; }
            public string Week1 { get; set; }
            public string Week2 { get; set; }
            public string Week3 { get; set; }
            public string Week4 { get; set; }
            public string Week5 { get; set; }
            public string Week6 { get; set; }
            public string Week7 { get; set; }
            public string Machine { get; set; }
        }
    }
}