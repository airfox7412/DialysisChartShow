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
    public partial class drug_trub_package : BaseForm
    {
        #region class package_detail2
        public class package_detail2
        {
            public string pdet_id
            {
                get;
                set;
            }
            public string pck_name
            {
                get;
                set;
            }
            public string pdet_itemcd
            {
                get;
                set;
            }
            public string pdet_itemnm
            {
                get;
                set;
            }
            public string pdet_qty
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
                //GridPanelBind();
            }
        }

        private void GetComboBoxAll()
        {
            string sSQL;
            sSQL = "SELECT pck_code AS CODE, pck_name AS NAME FROM package_setup ";
            DataTable dt = db.Query(sSQL);
            Common.SetComboBoxItem(ComboBoxGroup, dt, true, "NAME", "CODE");
            Common.SetComboBoxItem(ComboBoxPck, dt, true, "NAME", "NAME");

            sSQL = "SELECT invs_ctg AS CODE, invs_name AS NAME FROM inventory_stock ";
            sSQL += "WHERE invs_category='1'";
            dt = db.Query(sSQL);
            Common.SetComboBoxItem(ComboBoxMaterial, dt, true, "NAME", "NAME");            
        }        

        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            string pckcode = ComboBoxGroup.Text;
            string sSQL = "SELECT a.pdet_id, b.pck_name, a.pdet_itemcd, a.pdet_itemnm, a.pdet_qty ";
            sSQL += "FROM package_detail2 a ";
            sSQL += "LEFT JOIN package_setup b ON b.pck_code=a.pdet_code ";
            sSQL += "WHERE a.pdet_code='" + pckcode + "' ";
            sSQL += "ORDER BY a.pdet_id ";
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
                string pckcode;
                string sSQL = "";
                ChangeRecords<package_detail2> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<package_detail2>();
                foreach (package_detail2 updated in sdh.Updated)
                {
                    pckcode = GetValue(updated.pck_name);
                    sSQL += "UPDATE package_detail2 SET ";
                    sSQL += "pdet_code='" + pckcode + "', ";
                    sSQL += "pdet_itemcd='" + GetCode(updated.pdet_itemnm) + "', ";
                    sSQL += "pdet_itemnm='" + updated.pdet_itemnm + "', ";
                    sSQL += "pdet_qty='" + updated.pdet_qty + "' ";
                    sSQL += "WHERE pdet_id='" + updated.pdet_id + "'; ";
                }

                foreach (package_detail2 updated in sdh.Created)
                {
                    pckcode = GetValue(updated.pck_name);
                    sSQL = "INSERT INTO package_detail2 ";
                    sSQL += "(pdet_code, pdet_itemcd, pdet_itemnm, pdet_qty) ";
                    sSQL += "VALUES ('" + pckcode + "', ";
                    sSQL += "'" + GetCode(updated.pdet_itemnm) + "', ";
                    sSQL += "'" + updated.pdet_itemnm + "', ";
                    sSQL += "'" + updated.pdet_qty + "'); ";
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
            this.btnCANCEL.Disabled = true;
            this.btnAdd.Disabled = false;
        }
        #endregion

        public string GetValue(string str1)
        {
            string sql;
            sql = "SELECT pck_code FROM package_setup ";
            sql += "WHERE pck_name='" + str1 + "'";
            DataTable dt = db.Query(sql);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["pck_code"].ToString();
            return "";
        }

        public string GetCode(string str1)
        {
            string sSQL = "SELECT invs_ctg FROM inventory_stock ";
            sSQL += "WHERE invs_category='1' AND invs_name='" + str1 + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                return dt.Rows[0]["invs_ctg"].ToString();
            }
            else
                return "";
        }

        protected void cmdDelete(object sender, DirectEventArgs e)
        {
            string pdet_id = e.ExtraParams["pdet_id"];
            string sSQL = "DELETE FROM package_detail2 WHERE pdet_id=" + pdet_id;
            if (db.Excute(sSQL))
            {
                Common._NotificationShow("资料已删除");
                cmdCANCEL(sender, e);
            }
        }
    }
}