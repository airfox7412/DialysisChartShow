using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Text;

namespace Dialysis_Chart_Show.checkin
{
    public partial class DrugMod_ListAll : System.Web.UI.UserControl
    {
        DBMysql db = new DBMysql();
        
        public Store GridStore1 { get; set; }
        public Store GridStore2 { get; set; }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
            }
            ReadDrugMod_List();
        }

        public void ReadDrugMod_List()
        {
            string sql = "SELECT a.sid, b.drg_name, a.intake, a.medway, a.freq ";
            sql += "FROM drug_modual a ";
            sql += "LEFT JOIN drug_list b ON a.drg_code=b.drg_code ";
            sql += "WHERE a.status='Y' ";
            sql += "ORDER BY a.drg_code";
            DataTable dt = db.Query(sql);
            Store istore = Grid_DrugTerm.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
            dt.Dispose();
        }

        //public object[] GetDataArray(DataTable dt)
        //{
        //    object[] objx = new Object[dt.Rows.Count];
        //    int i = 0;

        //    foreach (DataRow irow in dt.Rows)
        //    {
        //        object[] objy = new object[dt.Columns.Count];
        //        for (int j = 0; j < dt.Columns.Count; j++)
        //        {
        //            objy[j] = irow[j];
        //        }
        //        objx[i] = objy;
        //        i++;
        //    }
        //    return objx;
        //}

        public void Show()
        {
            this.DetailsWindow.Show();
        }

        public void SetDrugModList(string person_id, string drugkind, string docname)
        {
            PationID.Text = person_id;
            DrugKind.Text = drugkind;
            DocName.Text = docname;
            ReadDrugMod_List();
        }
        
        private string[] GetDrugValue(string sid)
        {
            string[] array = new string[4];
            string sql = "SELECT * FROM drug_modual ";
            sql += "WHERE sid=" + sid;
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                array[0] = dt.Rows[0]["drg_code"].ToString();
                array[1] = dt.Rows[0]["intake"].ToString();
                array[2] = dt.Rows[0]["freq"].ToString();
                array[3] = dt.Rows[0]["medway"].ToString();
            }
            dt.Dispose();
            return array;
        }

        protected void SaveSelRow(object sender, DirectEventArgs e)
        {
            string sql = "";
            string drg_code, drg_intake, drg_freq, drg_medway;
            string toDay = DateTime.Now.ToString("yyyy-MM-dd");
            string toTime = DateTime.Now.ToString("HH:mm");
            string[] StrArray = new string[4];

            RowSelectionModel sm = this.Grid_DrugTerm.GetSelectionModel() as RowSelectionModel;

            foreach (SelectedRow row in sm.SelectedRows)
            {
                StrArray = GetDrugValue(row.RecordID.ToString());
                drg_code = StrArray[0];
                drg_intake = StrArray[1];
                drg_freq = StrArray[2];
                drg_medway = StrArray[3];
                if (DrugKind.Text == "L")
                {
                    sql += "INSERT INTO longterm_ordermgt(lgord_patic, lgord_dateord, lgord_timeord, lgord_usr1, lgord_dtactst, lgord_usr2, ";
                    sql += "lgord_drug, lgord_intake, lgord_freq, lgord_medway, lgord_actst) ";
                    sql += "VALUES('" + PationID.Text + "','" + toDay + "','" + toTime + "','" + DocName.Text + "','','',";
                    sql += "'" + drg_code + "','" + drg_intake + "','" + drg_freq + "','" + drg_medway + "','00001'); ";
                }
                else //==S
                {
                    sql += "INSERT INTO shortterm_ordermgt(shord_patic, shord_dateord, shord_timeord, shord_usr1, shord_dtactst, shord_usr2, ";
                    sql += "shord_drug, shord_intake, shord_freq, shord_medway, shord_actst) ";
                    sql += "VALUES('" + PationID.Text + "','" + toDay + "','" + toTime + "','" + DocName.Text + "','','',";
                    sql += "'" + drg_code + "','" + drg_intake + "','" + drg_freq + "','" + drg_medway + "','00001'); ";                    
                }
            }
            db.Excute(sql);

            Store istore = Grid_DrugTerm.GetStore();
            istore.RemoveAll();
            DetailsWindow.Close();

            //更新長期醫囑清單
            if (DrugKind.Text == "L")
            {
                sql = "SELECT a.lgord_id,a.lgord_dateord,a.lgord_timeord,a.lgord_usr1,b.drg_name,a.lgord_intake,a.lgord_freq,a.lgord_medway,a.lgord_comment, ";
                sql += "a.lgord_patic,a.lgord_drug, a.lgord_dtactst, a.lgord_usr2, ";
                sql += "IF(a.lgord_actst='00002',true,false) AS USED ";
                sql += " FROM longterm_ordermgt a ";
                sql += "LEFT JOIN drug_list b ON a.lgord_drug = b.drg_code ";
                sql += "WHERE a.lgord_patic = '" + PationID.Text + "' ";
                sql += "ORDER BY a.lgord_dateord, a.lgord_timeord DESC";
                DataTable dt = db.Query(sql);
                GridStore1.DataSource = db.GetDataArray(dt);
                GridStore1.DataBind();
            }
            else //更新短期醫囑清單
            {
                sql = "SELECT a.shord_id,a.shord_dateord,a.shord_timeord,a.shord_usr1,b.drg_name,a.shord_intake,a.shord_freq,a.shord_medway,a.shord_comment, ";
                sql += "a.shord_patic,a.shord_drug, a.shord_dtactst, a.shord_usr2, ";
                sql += "IF(a.shord_actst='00002',true,false) AS USED ";
                sql += "FROM shortterm_ordermgt a ";
                sql += "LEFT JOIN drug_list b ON a.shord_drug = b.drg_code ";
                sql += "WHERE a.shord_patic = '" + PationID.Text + "' ";
                sql += "ORDER BY a.shord_dateord, a.shord_timeord DESC";
                DataTable dt = db.Query(sql);
                GridStore2.DataSource = db.GetDataArray(dt);
                GridStore2.DataBind();
            }
        }
    }
}