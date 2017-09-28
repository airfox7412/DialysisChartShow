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

namespace Dialysis_Chart_Show.checkin
{
    public partial class drug_mod : BaseForm
    {
        #region class drug_modual
        public class drug_modual
        {
            public string sid
            {
                get;
                set;
            }
            public string drg_name
            {
                get;
                set;
            }
            public string intake
            {
                get;
                set;
            }
            public string freq
            {
                get;
                set;
            }
            public string medway
            {
                get;
                set;
            }
            public string USED
            {
                get;
                set;
            }
        }
        #endregion

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GetComboBoxAll();
                GridPanelBind();
            }
        }

        private void GetComboBoxAll()
        {
            string sSQL = "SELECT genst_code AS CODE, genst_desc AS NAME FROM general_setup WHERE genst_ctg='drgfreq' ";//用藥頻率
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cb_ordfreq, dt1, true, "NAME", "NAME");

            sSQL = "SELECT med_code AS CODE, med_name AS NAME FROM med_way ";//給藥方式
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cb_medway, dt1, true, "NAME", "NAME");
        }        

        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT a.sid, c.drg_name, a.intake, a.freq, a.medway, IF(a.status='Y',true,false) as USED ";
            sSQL += "FROM drug_modual a ";
            sSQL += "LEFT JOIN drug_list c ON a.drg_code=c.drg_code ";
            sSQL += "ORDER BY a.sid ";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        #region 添加
        protected void cmdADD(object sender, DirectEventArgs e)
        {
            this.btnSave.Disabled = false;
            this.btnCANCEL.Disabled = false;
            this.btnAdd.Disabled = true;
        }
        #endregion

        #region 修改
        protected void cmdSAVE(object sender, DirectEventArgs e)
        {
            try
            {
                string sSQL = "";
                string drgcode;
                string itemUsed = "N";
                ChangeRecords<drug_modual> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<drug_modual>();
                foreach (drug_modual updated in sdh.Updated)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                        itemUsed = "Y";
                    drgcode = GetValue(updated.drg_name);
                    sSQL += "UPDATE drug_modual SET ";
                    sSQL += "drg_code='" + drgcode + "', ";
                    sSQL += "intake='" + updated.intake + "', ";
                    sSQL += "freq='" + updated.freq + "', ";
                    sSQL += "medway='" + updated.medway + "', ";
                    sSQL += "status='" + itemUsed + "' ";
                    sSQL += "WHERE sid='" + updated.sid + "'; ";
                    db.Excute(sSQL);
                }

                foreach (drug_modual updated in sdh.Created)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                    {
                        itemUsed = "Y";
                    }
                    drgcode = GetValue(updated.drg_name);
                    sSQL = "INSERT INTO drug_modual ";
                    sSQL += "(drg_code, intake, freq, medway, status) ";
                    sSQL += "VALUES (";
                    sSQL += "'" + drgcode + "', ";
                    sSQL += "'" + updated.intake + "', ";
                    sSQL += "'" + updated.freq + "', ";
                    sSQL += "'" + updated.medway + "', ";
                    sSQL += "'" + itemUsed + "') ";
                    db.Excute(sSQL);
                }
                cmdCANCEL(sender, e);
            }
            catch(Exception ex)
            {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }
        #endregion

        #region 取消新增動作
        protected void cmdCANCEL(object sender, DirectEventArgs e)
        {
            this.btnAdd.Disabled = false;
            this.btnSave.Disabled = true;
            this.btnCANCEL.Disabled = true;
            GridPanelBind();
        }
        #endregion

        public string GetValue(string str1)
        {
            string sql;
            sql = "SELECT drg_code FROM drug_list ";
            sql += "WHERE drg_name='" + str1 + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["drg_code"].ToString();
            else
                return "";
        }
    }
}