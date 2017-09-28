using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using System.Data;
using Dialysis_Chart_Show.tools;
using System.Xml;
using Ext.Net;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_09_02_detail : BaseForm
    {

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                show_grid();
            }
        }

        #region 血液淨化過程 --> 淨化過程明細
        protected void show_grid()
        {
            String DialysisDate = Request["date"].ToString().Trim();
            if (Dialysis_Date.Text == null)
            {
                Dialysis_Date.Text = DialysisDate;
            }
            patient_id.Text = Request["patient_id"] == null ? _PAT_IC : Request["patient_id"].ToString().Trim();
            String person_id = patient_id.Text;

            String sql = "SELECT cast(b.cln2_date as date) dialysis_date, cast(b.cln2_time as time) dialysis_time, 0 column_7,0 column_6,0 column_2,0 column_3,0 column_10,0 column_8,0 column_4 , ";
            sql += "b.cln2_t,b.cln2_p,b.cln2_r,b.cln2_bp,b.cln2_rmk,b.cln2_user,b.cln2_dateadded ";
            sql += "FROM clinical2_nurse b ";
            sql += "WHERE b.cln2_patic='" + person_id + "' AND b.cln2_date='" + DialysisDate + "' ";
            sql += "and (b.cln2_patic , b.cln2_date,b.cln2_time) not in ";
            sql += "(select person_id,dialysis_date,dialysis_time from data_list where person_id='" + person_id + "' and dialysis_date='" + DialysisDate + "') ";

            #region union for select
            String union_sql = " union (" +
                   "SELECT a.dialysis_date, a.dialysis_time, a.column_7,a.column_6,a.column_2,a.column_3,a.column_10,a.column_8,a.column_4 , ";
            union_sql += "b.cln2_t,b.cln2_p,b.cln2_r,b.cln2_bp,b.cln2_rmk,b.cln2_user,b.cln2_dateadded ";
            union_sql += "FROM data_list a ";
            union_sql += "LEFT JOIN ";
            String left_join = "(select * from clinical2_nurse  WHERE cln2_patic='" + person_id + "' AND cln2_date='" + DialysisDate + "') b " +
                        "ON a.person_id = b.cln2_patic and a.dialysis_date=b.cln2_date and a.dialysis_time= b.cln2_time";
            union_sql += left_join;
            union_sql += " WHERE a.person_id='" + person_id + "' AND a.dialysis_date='" + DialysisDate + "')";
            #endregion

            sql = sql + union_sql + " ORDER BY dialysis_time";

            DataTable dt = db.Query(sql);
            Store istore = Grid_DataList.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }
        #endregion

        #region show_table() 沒用到
        protected void show_table()
        {
            List<String[]> list_col = new List<String[]>(){
                                            new String[] { "时间", "dialysis_time" },
                                            new String[] { "电导", "column_7" },
                                            new String[] { "温度 °C", "column_6" },
                                            new String[] { "已超滤 kg", "column_2" },
                                            new String[] { "跨膜压 mmHg", "column_10" },
                                            new String[] { "静脉压 mmHg", "column_8" },
                                            new String[] { "血流量 ml/min", "column_4" },
                                            new String[] { "T °C", "cln2_t" },
                                            new String[] { "P 次/分", "cln2_p" },
                                            new String[] { "R 次/分", "cln2_r" },
                                            new String[] { "BP mmHg", "cln2_bp" },
                                            new String[] { "病情及处理", "cln2_rmk" },
                                            new String[] { "记录人", "cln2_user" },
                                            new String[] { "纪录时间", "cln2_dateadded" }
                                        };

            ArrayList acol = new ArrayList();
            foreach (String[] array in list_col)
            {
                acol.Add(array);
            }

            String date = Request["date"].ToString().Trim();
            String select = "SELECT a.dialysis_time, a.column_7, a.column_6, a.column_2, " + 
                            "a.column_10,a.column_8,a.column_4 , " +
                            "b.cln2_t,b.cln2_r,b.cln2_p,b.cln2_bp,b.cln2_rmk,b.cln2_user,b.cln2_dateadded ";
            String from = " FROM data_list a " +
                          " LEFT JOIN clinical2_nurse b on a.person_id = b.cln2_patic " +
                          " and a.dialysis_date=b.cln2_date " +
                          " and a.dialysis_time= b.cln2_time ";
            String where = " WHERE a.person_id='"+_PAT_IC+"' AND a.dialysis_date='" + date + "'";
            String sql_stmt = select + from + where;
            DataTable dt = tools.DBMysql.query(sql_stmt);
            _Fill_Table(sql_stmt, acol);
        }
        #endregion

        protected void SaveDataList(object sender, DirectEventArgs e)
        {
            string PationID = e.ExtraParams["PationID"];
            string DialysisDate = e.ExtraParams["DialysisDate"];
            string DialysisTime = e.ExtraParams["DialysisTime"];
            string sql1 = "";
            string sql0 = "SELECT dialysis_time, column_7, column_6, column_2, column_3, column_10, column_8, column_4 FROM data_list ";
            sql0 += "WHERE person_id='" + PationID + "' ";
            sql0 += "AND dialysis_date='" + DialysisDate + "' ";
            sql0 += "AND dialysis_time='" + OldTime.Text + "'";
            DataTable dt0 = db.Query(sql0);
            if (dt0.Rows.Count > 0)
            {
                sql1 += "UPDATE data_list SET ";
                sql1 += "dialysis_time='" + DialysisTime + "', ";
                sql1 += "column_7='" + TextField1.Text + "', ";
                sql1 += "column_6='" + TextField2.Text + "', ";
                sql1 += "column_2='" + TextField3.Text + "', ";
                sql1 += "column_3='" + TextField4.Text + "', ";
                sql1 += "column_10='" + TextField5.Text + "', ";
                sql1 += "column_8='" + TextField6.Text + "', ";
                sql1 += "column_4='" + TextField7.Text + "' ";
                sql1 += "WHERE person_id='" + PationID + "' ";
                sql1 += "AND dialysis_date='" + DialysisDate + "' ";
                sql1 += "AND dialysis_time='" + OldTime.Text + "'; ";
            }
            else
            {
                dt0.Clear();
                sql0 = "SELECT pv_floor, pv_bedno FROM pat_visit ";
                sql0 += "WHERE pv_ic='" + PationID + "' ";
                sql0 += "AND pv_datevisit='" + DialysisDate + "' ";
                dt0 = db.Query(sql0);
                string pv_floor = dt0.Rows[0]["pv_floor"].ToString();
                string pv_bedno = dt0.Rows[0]["pv_bedno"].ToString();

                sql1 += "INSERT INTO data_list ";
                sql1 += "(person_id, dialysis_date, dialysis_time, floor_no, bed_no, column_7, column_6, column_2, column_10, column_8, column_4) ";
                sql1 += "VALUES('" + PationID + "','" + DialysisDate + "','" + DialysisTime + "','" + pv_floor + "','" + pv_bedno + "','";
                sql1 += TextField1.Text + "','" + TextField2.Text + "','" + TextField3.Text + "','";
                sql1 += TextField4.Text + "','" + TextField5.Text + "','" + TextField6.Text + "'); ";
            }

            sql0 = "SELECT cln2_date, cln2_time, cln2_t, cln2_p, cln2_r, cln2_bp, cln2_rmk, cln2_user, cln2_dateadded FROM clinical2_nurse ";
            sql0 += "WHERE cln2_patic='" + PationID + "' ";
            sql0 += "AND cln2_date='" + DialysisDate + "' ";
            sql0 += "AND cln2_time='" + OldTime.Text + "'";
            dt0 = db.Query(sql0);
            if (dt0.Rows.Count > 0)
            {
                sql1 += "UPDATE clinical2_nurse SET ";
                sql1 += "cln2_time='" + DialysisTime + "', ";
                sql1 += "cln2_t='" + TextField8.Text + "', ";
                sql1 += "cln2_p='" + TextField9.Text + "', ";
                sql1 += "cln2_r='" + TextField10.Text + "', ";
                sql1 += "cln2_bp='" + TextField11.Text + "', ";
                sql1 += "cln2_rmk='" + TextField12.Text + "', ";
                sql1 += "cln2_user='" + TextField13.Text + "' ";
                sql1 += "WHERE cln2_patic='" + PationID + "' ";
                sql1 += "AND cln2_date='" + DialysisDate + "' ";
                sql1 += "AND cln2_time='" + OldTime.Text + "';";
            }
            else
            {
                sql1 += "INSERT INTO clinical2_nurse ";
                sql1 += "(cln2_patic, cln2_date, cln2_time, cln2_t, cln2_p, cln2_r, cln2_bp, cln2_rmk, cln2_user) ";
                sql1 += "VALUES('" + PationID + "','" + DialysisDate + "','" + DialysisTime + "','";
                sql1 += TextField8.Text + "','" + TextField9.Text + "','" + TextField10.Text + "','" + TextField11.Text + "','";
                sql1 += TextField12.Text + "','" + TextField13.Text + "')";
            }
            db.Excute(sql1);

            DetailsWindow.Hide();
            show_grid();
        }

        protected void EditDetails(object sender, DirectEventArgs e)
        {
            string json = "[" + e.ExtraParams["Values"] + "]";
            Dictionary<string, string>[] selRow = JSON.Deserialize<Dictionary<string, string>[]>(json);
            string person_id = patient_id.Text;
            string dialysis_date = selRow[0]["dialysis_date"].ToString().Substring(0, 10);
            string dialysis_time = "";
            if (selRow[0]["dialysis_time"] != null)
            {
                dialysis_time = selRow[0]["dialysis_time"].ToString();
            }
            OldTime.Text = dialysis_time;

            string column_7 = selRow[0]["column_7"].ToString();
            string column_6 = selRow[0]["column_6"].ToString();
            string column_2 = "";
            if (selRow[0]["column_2"] != null)
            {
                column_2 = selRow[0]["column_2"].ToString();
            }
            string column_3 = "";
            if (selRow[0]["column_3"] != null)
            {
                column_3 = selRow[0]["column_3"].ToString();
            }
            string column_10 = selRow[0]["column_10"].ToString();
            string column_8 = selRow[0]["column_8"].ToString();
            string column_4 = selRow[0]["column_4"].ToString();
            string cln2_t = "";
            if (selRow[0]["cln2_t"] != null)
            {
                cln2_t = selRow[0]["cln2_t"].ToString();
            }
            string cln2_p = "";
            if (selRow[0]["cln2_p"] != null)
            {
                cln2_p = selRow[0]["cln2_p"].ToString();
            }

            string cln2_r = "";
            if (selRow[0]["cln2_r"] != null)
            {
                cln2_r = selRow[0]["cln2_r"].ToString();
            }

            string cln2_bp = "";
            if (selRow[0]["cln2_bp"] != null)
            {
                cln2_bp = selRow[0]["cln2_bp"].ToString();
            }

            string cln2_rmk = "";
            if (selRow[0]["cln2_rmk"] != null)
            {
                cln2_rmk = selRow[0]["cln2_rmk"].ToString();
            }

            PationID.Text = person_id;
            DialysisDate.Text = dialysis_date;
            DialysisTime.Text = dialysis_time;
            TextField1.Text = column_7;
            TextField2.Text = column_6;
            TextField3.Text = column_2;
            TextField4.Text = column_3;
            TextField5.Text = column_10;
            TextField6.Text = column_8;
            TextField7.Text = column_4;
            TextField8.Text = cln2_t;
            TextField9.Text = cln2_p;
            TextField10.Text = cln2_r;
            TextField11.Text = cln2_bp;
            TextField12.Text = cln2_rmk;
            DetailsWindow.Show();
        }

        protected void BtnDel_Click(object sender, DirectEventArgs e)
        {
            Session["PationID"] = PationID.Text;
            Session["DialysisDate"] = DialysisDate.Text;
            Session["DialysisTime"] = OldTime.Text;
            X.Msg.Confirm("提示", "您确定要删除明细么？", new JFunction("DeleteData.Activate(result);", "result")).Show();
        }

        [DirectMethod(Namespace = "DeleteData")]
        public void Activate(string result)
        {
            if (result == "yes")
            {
                string sql;
                sql = "DELETE FROM data_list ";
                sql += "WHERE person_id='" + Session["PationID"].ToString() + "' ";
                sql += "AND dialysis_date='" + Session["DialysisDate"].ToString() + "' ";
                sql += "AND dialysis_time='" + Session["DialysisTime"].ToString() + "'; ";

                sql += "DELETE FROM clinical2_nurse ";
                sql += "WHERE cln2_patic='" + Session["PationID"].ToString() + "' ";
                sql += "AND cln2_date='" + Session["DialysisDate"].ToString() + "' ";
                sql += "AND cln2_time='" + Session["DialysisTime"].ToString() + "'; ";

                db.Excute(sql);
                DetailsWindow.Close();
                show_grid();
                X.Msg.Notify("提示", "已经删除明细").Show();
            }
        }
    }
}