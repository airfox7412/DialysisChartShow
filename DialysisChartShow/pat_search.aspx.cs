using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Dialysis_Chart_Show.tools;
using Ext.Net;
using System.Configuration;
using System.Data;
using System.Net;
using System.IO;
using System.Text;

namespace Dialysis_Chart_Show
{
    public partial class pat_search : BaseForm
    {
        protected new void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                show_history();
            }
        }

        #region 历史病患
        protected void btn_Query_Click(object sender, DirectEventArgs e)
        {
            show_history();
        }

        protected void show_history()
        {
            string sql;
            DateTime datetime = DateTime.Now;
            sql = " SELECT a.pif_id, a.pif_name, if (a.pif_sex = 'M','男','女') as sex, a.pif_dob, a.pif_ic,";
            sql += " if (b.next_visit_date > '','腹透','血透') as txt_10, ";
            sql += " e.diadate AS FirstDate, '' AS InfoDate, ";
            sql += " if (g.BI > 0,'V',' ') as txt_101,";
            sql += " case f.opt_1 when '1' then '退出' when '2' then '肾移植' when '3' then '转出' when '4' then '死亡' when '5' then '转入' else '' end AS opt_52,";
            sql += " f.info_date,' ',a.pif_docname,c.info_survey_date,'ignore' ";
            sql += "FROM pat_info a ";
            sql += "LEFT JOIN zinfo_maim c ON a.pif_id=c.pat_id ";
            sql += "LEFT JOIN (SELECT cln1_patic, MAX(cln1_diadate) AS diadate FROM clinical1_nurse group by cln1_patic) e  ON a.pif_ic = e.cln1_patic ";
            sql += "LEFT JOIN zinfo_a_07  f  ON a.pif_id = f.pat_id ";
            sql += "LEFT JOIN (SELECT pat_id, MAX(dat_3) AS next_visit_date FROM zinfo_p_06 group by pat_id) b ON a.pif_id=b.pat_id ";
            sql += "LEFT JOIN BI_SUM_View g ON a.pif_id = g.pat_no ";
            sql += "WHERE 1=1";
            if (!string.IsNullOrEmpty(Text_Name.Text)) //姓名篩選
                sql += " and a.pif_name like '%" + Text_Name.Text + "%'";
            if (!string.IsNullOrEmpty(Text_ID.Text)) //身分證號篩選
                if (Text_ID.Text.Substring(0, 1) != "#")
                    sql += " and a.pif_ic like '%" + Text_ID.Text + "%'";
                else
                    sql += " and a.pif_id='" + Text_ID.Text.Substring(1) + "'";
            sql += " ORDER BY a.pif_id ";

            DataTable dt = db.Query(sql);
            Store istore = GridList.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }
        #endregion
        
        #region 選擇病患
        protected void Dialysis_detail(object sender, DirectEventArgs e)
        {
            _PAT_IC = e.ExtraParams["pat_ic"].ToString();
            _PIF_NAME = e.ExtraParams["pif_name"].ToString();
            _USER_NAME = e.ExtraParams["pif_docname"].ToString();
            _PIF_SX = e.ExtraParams["pif_sex"].ToString() + "性";

            string sql = "SELECT pif_id, pif_name, pif_docname FROM pat_info ";
            sql += " where pif_ic = '" + _PAT_IC + "' ";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                if (dt.Rows[0]["pif_docname"].ToString() != null)
                    _PatDocName = dt.Rows[0]["pif_docname"].ToString();
                else
                    _PatDocName = "";
                Session["PAT_ID"] = dt.Rows[0]["pif_id"].ToString();
                _PAT_ID = Session["PAT_ID"].ToString();

                X.Js.AddScript("parentAutoLoadControl.close();");
            }
        }
        #endregion
    }
}