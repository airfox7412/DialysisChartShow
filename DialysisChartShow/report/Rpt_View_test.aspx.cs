using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using CrystalDecisions.CrystalReports.Engine;
using CrystalDecisions.Shared;
using System.Configuration;

namespace Dialysis_Chart_Show.report
{
    public partial class Rpt_View_test : System.Web.UI.Page
    {
        private void Page_Init(object sender, EventArgs e)
        {

            ReportDocument rpt = new ReportDocument();

            rpt.Load(Server.MapPath("test.rpt"));

            ParameterDiscreteValue pdv = new ParameterDiscreteValue();
            pdv.Value = "320102195808270413";
            rpt.ParameterFields["person_id"].CurrentValues.Clear();
            rpt.ParameterFields["person_id"].DefaultValues.Clear();
            rpt.ParameterFields["person_id"].CurrentValues.Add(pdv);

            //給日期起訖
            rpt.ParameterFields["dialysis_date"].CurrentValues.AddRange(DateTime.Parse("2013-07-01"), DateTime.Parse("2013-08-31"), RangeBoundType.BoundInclusive, RangeBoundType.BoundInclusive);

            
            CRViewer.ReportSource = rpt;

        }
    }
}