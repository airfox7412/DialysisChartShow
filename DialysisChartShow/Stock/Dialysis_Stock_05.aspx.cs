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
    public partial class Dialysis_Stock_05 : BaseForm
    {
        #region class dailyiv_itemlist
        public class dailyiv_itemlist
        {
            public string dyivl_id
            {
                get;
                set;
            }
            public string dyivl_serialno
            {
                get;
                set;
            }
            public string dyivl_no
            {
                get;
                set;
            }
            public string dyivl_item
            {
                get;
                set;
            }
            public string dyivl_qty
            {
                get;
                set;
            }
            public string dyivl_rec
            {
                get;
                set;
            }
            public string dyivl_ivdate
            {
                get;
                set;
            }
        }
        #endregion

        DateTime FirstDay = DateTime.Now.AddDays(-DateTime.Now.Day + 1);
        DateTime LastDay = DateTime.Now.AddMonths(1).AddDays(-DateTime.Now.AddMonths(1).Day);

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                DateField1.Text = FirstDay.ToString("yyyy-MM-dd");
                DateField2.Text = LastDay.ToString("yyyy-MM-dd");
                GridPanelBind();
            }
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT dyivl_id, dyivl_serialno, dyivl_no, dyivl_item, SUM(dyivl_qty), dyivl_rec, dyivl_ivdate FROM dailyiv_itemlist ";
            sSQL += "WHERE dyivl_ivdate>='" + _Get_YMD2(this.DateField1.Text) + "' ";
            sSQL += "AND dyivl_ivdate<='" + _Get_YMD2(this.DateField2.Text) + "' ";
            sSQL += "GROUP BY dyivl_item ";
            sSQL += "ORDER BY dyivl_item";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        protected void cmdQuery(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }
    }
}