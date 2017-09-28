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

namespace Dialysis_Chart_Show.Systems
{
    public partial class Dialysis_System_01 : BaseForm
    {
        #region class DrugList
        public class DrugList
        {
            public string drg_id
            {
                get;
                set;
            }
            public string drg_grp
            {
                get;
                set;
            }
            public string drg_name
            {
                get;
                set;
            }
            public string drg_code
            {
                get;
                set;
            }
            public string price
            {
                get;
                set;
            }
            public string cost
            {
                get;
                set;
            }
            public string short_code
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
                GetGroupName();
                GridPanelBind();

            }
        }

        private void GetGroupName()
        {
            string sSQL;
            sSQL = "SELECT drg_grp AS GROUP_NAME FROM drug_list ";
            sSQL += "GROUP BY drg_grp ";
            sSQL += "ORDER BY drg_grp ";
            DataTable dt = db.Query(sSQL);
            Common.SetComboBoxItem(Cb_Group, dt, false, "GROUP_NAME", "GROUP_NAME");

            Ext.Net.ListItem litem2;
            litem2 = new Ext.Net.ListItem("(全部)", "(全部)");
            ComboBoxGroup.Items.Add(litem2);
            if (dt.Rows.Count > 0)
            {
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    litem2 = new Ext.Net.ListItem(dt.Rows[i]["GROUP_NAME"].ToString(), dt.Rows[i]["GROUP_NAME"].ToString());
                    ComboBoxGroup.Items.Add(litem2);
                }
                ComboBoxGroup.GetStore().DataBind();
                Common.SetComboBoxValue(ComboBoxGroup, "药品", false);
            }
        }        

        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT drg_id, drg_grp, drg_name, drg_code, price, cost, short_code, IF(drg_status='Y',true,false) as USED ";
            sSQL += "FROM drug_list ";
            if (ComboBoxGroup.Text != "(全部)")
            {
                sSQL += "WHERE drg_grp='" + ComboBoxGroup.Text + "' ";
            }
            sSQL += "ORDER BY drg_id ";
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
            string SQLT_DRUG_UPDATE = @"
                START TRANSACTION;

                UPDATE drug_list
                SET drg_grp='{0}',
                    drg_name='{1}',
                    drg_code='{2}',
                    drg_status='{3}',
                    price='{4}',
                    cost='{5}',
                    short_code='{6}'
                WHERE drg_id='{7}';

                INSERT INTO inventory_stock (invs_ctg, invs_name, invs_inamt, invs_outamt, invs_invamt, invs_minstock, invs_category)
                SELECT drug_list.drg_code, drug_list.drg_name, 0, 0 ,0, 0, 2
                FROM (
                        select *
                        from drug_list
                        where drg_id = '{7}'
                ) as drug_list left join inventory_stock on drug_list.drg_code=inventory_stock.invs_ctg and inventory_stock.invs_category = 2
                WHERE drg_status='Y' and drg_grp='药品' and inventory_stock.invs_ctg is null;

                COMMIT;";

            string SQLT_DRUG_CREATE = @"
                START TRANSACTION;

                INSERT INTO drug_list (drg_grp, drg_name, drg_code, drg_status, price, cost, short_code) 
                VALUES ('{0}', '{1}', '{2}', '{3}', '{4}', '{5}', '{6}');

                INSERT INTO inventory_stock (invs_ctg, invs_name, invs_inamt, invs_outamt, invs_invamt, invs_minstock, invs_category)
                SELECT drug_list.drg_code, drug_list.drg_name, 0, 0 ,0, 0, 2
                FROM (
                        select *
                        from drug_list
                        where drg_id = LAST_INSERT_ID()
                ) as drug_list left join inventory_stock on drug_list.drg_code=inventory_stock.invs_ctg and inventory_stock.invs_category = 2
                WHERE drg_status='Y' and drg_grp='药品' and inventory_stock.invs_ctg is null;

                COMMIT; ";

            try {
                string sSQL = "";
                string itemUsed = "N";
                ChangeRecords<DrugList> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<DrugList>();
                foreach (DrugList updated in sdh.Updated) {
                    // used flag transform
                    if (updated.USED != null && ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))) {
                        itemUsed = "Y";
                    }

                    sSQL = string.Format(SQLT_DRUG_UPDATE,
                        updated.drg_grp,
                        updated.drg_name,
                        updated.drg_code,
                        itemUsed,
                        updated.price,
                        updated.cost,
                        updated.short_code,
                        updated.drg_id);

                    db.Excute(sSQL);
                }

                foreach (DrugList updated in sdh.Created) {
                    // used flag transform
                    if (updated.USED != null && ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))) {
                        itemUsed = "Y";
                    }

                    sSQL = string.Format(SQLT_DRUG_CREATE,
                        updated.drg_grp,
                        updated.drg_name,
                        updated.drg_code,
                        itemUsed,
                        updated.price,
                        updated.cost,
                        updated.short_code);

                    db.Excute(sSQL);
                }
                cmdCANCEL(sender, e);

                //Store2.CommitChanges();
                //Store2.Reload();
                btnAdd.Disabled = false;
                btnSave.Disabled = true;
                btnDelete.Disabled = true;
                btnCANCEL.Disabled = true;
                btnExportMedicine.Disabled = false;
            } catch (Exception ex) {
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
            this.btnExportMedicine.Disabled = false;
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

        protected void cmdExportMedicine(object sender, DirectEventArgs e) {
            string sSQL = @"
                INSERT INTO inventory_stock (invs_ctg, invs_name, invs_inamt, invs_outamt, invs_invamt, invs_minstock, invs_category)
                SELECT drug_list.drg_code, drug_list.drg_name, 0, 0 ,0, 0, 2
                FROM myhaisv4.drug_list
	                left join inventory_stock on drug_list.drg_code=inventory_stock.invs_ctg and inventory_stock.invs_category = 2
                WHERE drg_status='Y' and drg_grp='药品' and inventory_stock.invs_ctg is null;";

            try {
                db.Excute(sSQL);
            } catch (Exception ex) {
                Common._ErrorMsgShow(ex.Message.ToString());
            }
        }
    }
}