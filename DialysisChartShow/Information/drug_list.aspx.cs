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

namespace Dialysis_Chart_Show.Information
{
    public partial class drug_list : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
                    
                Show_longdrug();
                Show_shortdrug();
               
        }
        protected void Show_longdrug()
        {
            DBMysql db = new DBMysql();
            string sql;
            //old sql = "SELECT a.lgord_dateord, a.lgord_timeord,a.lgord_usr1,b.drg_name,a.lgord_intake,a.lgord_freq,c.genst_desc,a.lgord_dtactst, a.lgord_usr2,a.lgord_comment ";
            sql = "SELECT a.lgord_dateord, a.lgord_timeord,a.lgord_usr1,b.drg_name,a.lgord_intake,a.lgord_freq,a.lgord_medway,c.genst_desc,a.lgord_dtactst, a.lgord_usr2,a.lgord_comment ";
            sql += "from longterm_ordermgt a, drug_list b, general_setup c";
            sql += " where a.lgord_patic = '" + _PAT_IC + "'";
            sql += " and a.lgord_drug = b.drg_code ";
            sql += " and c.genst_ctg = 'ActiveStatus' ";
            sql += " and a.lgord_actst = c.genst_code order by a.lgord_id ASC";
            DataTable dt = db.Query(sql);
        
            Store istore = Grid_Long_Term.GetStore();
            istore.DataSource =  db.GetDataArray_AddRowNum(db.Query(sql));
            istore.DataBind();
        }

        protected void Show_shortdrug()
        {
            DBMysql db = new DBMysql();
            string sql;
            sql = "SELECT a.shord_dateord, a.shord_timeord,a.shord_usr1,b.drg_name,a.shord_intake,a.shord_freq,a.shord_medway,c.genst_desc,a.shord_dtactst, a.shord_usr2,a.shord_comment ";
            sql += "from shortterm_ordermgt a, drug_list b, general_setup c";
            sql += " where a.shord_patic = '" + _PAT_IC + "'";
            sql += " and a.shord_drug = b.drg_code ";
            sql += " and c.genst_ctg = 'ActiveStatus' ";
            sql += " and a.shord_actst = c.genst_code order by a.shord_id ASC";
            DataTable dt = db.Query(sql);

            Store istore = Grid_Short_Term.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(db.Query(sql));
            istore.DataBind();
        }

        

    }
}