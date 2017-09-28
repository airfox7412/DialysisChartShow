using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using Dialysis_Chart_Show.tools;
using Ext.Net;

namespace Dialysis_Chart_Show.Stock
{
    public partial class Dialysis_Stock_06 : BaseForm
    {
        #region class inv_received
        public class inv_received
        {
            public string ivrec_id
            {
                get;
                set;
            }
            public string ivrec_name
            {
                get;
                set;
            }
            public string ivrec_serialno
            {
                get;
                set;
            }
            public string ivrec_amt
            {
                get;
                set;
            }
            public string ivrec_daterec
            {
                get;
                set;
            }
            public string ivrec_user
            {
                get;
                set;
            }
        }
        #endregion

        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                DateTime FirstDay = new DateTime(DateTime.Now.Year, DateTime.Now.Month, 1);
                DateField1.Text = String.Format("{0:yyyy-MM-dd}", FirstDay);
                DateField2.Text = String.Format("{0:yyyy-MM-dd}", DateTime.Now);
                GridPanelBind();
            }
        }

        protected void cmdQuery(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT ivrec_id, ivrec_code, ivrec_name, ivrec_amt, ivrec_daterec, ivrec_user FROM inv_received ";
            sSQL += "WHERE ivrec_daterec BETWEEN '" + _Get_YMD2(this.DateField1.Text) + "' AND '" + _Get_YMD2(this.DateField2.Text) + "' ";
            sSQL += "ORDER BY ivrec_daterec";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }
    }
}