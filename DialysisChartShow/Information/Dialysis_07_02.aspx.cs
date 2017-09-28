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
    public partial class Dialysis_07_02 : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                show_image();
            }
        }
        /// <summary>
        /// 顯示病歷
        /// </summary>
        protected void show_image()
        {
            string sql = "SELECT b.filename  FROM pat_info a LEFT JOIN zinfo_g_02 b ON a.pif_ic=b.pat_ic WHERE a.pif_id='"+_PAT_ID+"'";
            DataTable dt =db.Query(sql);
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                Ext.Net.Image image = new Ext.Net.Image();
                image.ID = "Image"+i+"";
                image.ImageUrl = "../upload/" + dt.Rows[i]["filename"] + "";
                this.Panel1.Controls.Add(image);
            }
        }
        
    }
}