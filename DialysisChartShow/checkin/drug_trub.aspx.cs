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
    public partial class drug_tube : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GetComboBoxAll();
                //GridPanelBind();
            }
        }

        private void GetComboBoxAll()
        {
            string sSQL;
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup ";
            DataTable dt = db.Query(sSQL);
            Common.SetComboBoxItem(ComboBoxGroup, dt, true, "NAME", "CODE");
            //Common.SetComboBoxValue(ComboBoxGroup, "自体内瘘", false);

            Common.SetComboBoxItem(ComboBoxPck, dt, true, "NAME", "NAME");

            sSQL = "SELECT genst_code AS CODE, genst_desc AS NAME FROM general_setup WHERE genst_ctg='drgfreq'";//用藥頻率
            DataTable dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cb_ordfreq, dt1, true, "NAME", "NAME");
            cb_ordfreq.Select(0);

            sSQL = "SELECT med_code AS CODE, med_name AS NAME FROM med_way";//給藥方式
            dt1 = db.Query(sSQL);
            Common.SetComboBoxItem(cb_medway, dt1, true, "NAME", "NAME");
        }        

        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            //string pckcode = Common.GetComboBoxValue(ComboBoxGroup);
            string pckcode = ComboBoxGroup.Text;
            string sSQL = "SELECT a.sid, b.pck_name, c.drg_name, a.intake, a.freq, a.medway, IF(a.status='Y',true,false) as USED ";
            sSQL += "FROM shortterm_ord a ";
            sSQL += "LEFT JOIN package_setup b ON a.pck_code=b.pck_code ";
            sSQL += "LEFT JOIN drug_list c ON a.drg_code=c.drg_code ";
            sSQL += "WHERE a.pck_code='" + pckcode + "' ";
            sSQL += "ORDER BY a.sid ";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        #region class Shortterm_ord
        public class Shortterm_ord
        {
            public string sid
            {
                get;
                set;
            }
            public string pck_name
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
                string pckcode, drgcode;
                string itemUsed = "N";
                ChangeRecords<Shortterm_ord> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<Shortterm_ord>();
                foreach (Shortterm_ord updated in sdh.Updated)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                        itemUsed = "Y";
                    pckcode = GetValue("Table1", updated.pck_name);
                    drgcode = GetValue("Table2", updated.drg_name);
                    sSQL += "UPDATE shortterm_ord SET ";
                    sSQL += "pck_code='" + pckcode + "', ";
                    sSQL += "drg_code='" + drgcode + "', ";
                    sSQL += "intake='" + updated.intake + "', ";
                    sSQL += "freq='" + updated.freq + "', ";
                    sSQL += "medway='" + updated.medway + "', ";
                    sSQL += "status='" + itemUsed + "' ";
                    sSQL += "WHERE sid='" + updated.sid + "'; ";
                }

                foreach (Shortterm_ord updated in sdh.Created)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                    {
                        itemUsed = "Y";
                    }
                    pckcode = GetValue("Table1", updated.pck_name);
                    drgcode = GetValue("Table2", updated.drg_name);
                    sSQL = "INSERT INTO shortterm_ord ";
                    sSQL += "(pck_code, drg_code, intake, freq, medway, status) ";
                    sSQL += "VALUES ('" + pckcode + "', ";
                    sSQL += "'" + drgcode + "', ";
                    sSQL += "'" + updated.intake + "', ";
                    sSQL += "'" + updated.freq + "', ";
                    sSQL += "'" + updated.medway + "', ";
                    sSQL += "'" + itemUsed + "'); ";
                }
                db.Excute(sSQL);
                cmdCANCEL(sender, e);
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }
        #endregion

        #region 取消新增動作
        protected void cmdCANCEL(object sender, DirectEventArgs e)
        {
            //Store2.RemoveAll(true);
            GridPanelBind();
            this.btnSave.Disabled = true;
            this.btnDelete.Disabled = true;
            this.btnCANCEL.Disabled = true;
            this.btnAdd.Disabled = false;
        }
        #endregion

        #region 刪除檢驗分類
        protected void cmdDelete(object sender, DirectEventArgs e)
        {
            string json = e.ExtraParams["Values"];

            if (string.IsNullOrEmpty(json))
            {
                return;
            }
            else
            {
                XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + json + "}}");
                string drg_id;
                string sSQL = "";
                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    drg_id = row.SelectSingleNode("drg_id").InnerXml;
                    sSQL += "DELETE FROM drug_list ";
                    sSQL += "WHERE drg_id='" + drg_id + "' ";
                }
                if (db.Excute(sSQL))
                {
                    this.ResourceManager1.AddScript("Ext.Msg.alert('提示资讯', '资料已删除');");
                }
            }
        }
        #endregion

        public string GetValue(string TableName, string str1)
        {
            string sql;
            if (TableName == "Table1")
            {
                sql = "SELECT pck_code FROM package_setup ";
                sql += "WHERE pck_name='" + str1 + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count>0)
                    return dt.Rows[0]["pck_code"].ToString();
            }
            else
            {
                sql = "SELECT drg_code FROM drug_list ";
                sql += "WHERE drg_name='" + str1 + "'";
                DataTable dt = db.Query(sql);
                if (dt.Rows.Count > 0)
                    return dt.Rows[0]["drg_code"].ToString();
            }
            return "";
        }
    }
}