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
    public partial class Dialysis_Stock_01 : BaseForm
    {
        #region class inventory_stock
        public class inventory_stock
        {
            public string invs_id
            {
                get;
                set;
            }
            public string invs_ctg
            {
                get;
                set;
            }
            public string invs_name
            {
                get;
                set;
            }
            public string invs_inamt
            {
                get;
                set;
            }
            public string invs_outamt
            {
                get;
                set;
            }
            public string invs_invamt
            {
                get;
                set;
            }
            public string invs_minstock
            {
                get;
                set;
            }
            public string invs_lastupdate
            {
                get;
                set;
            }
            public string invs_lastamt
            {
                get;
                set;
            }
            public string invs_lastusr
            {
                get;
                set;
            }
            public string invs_status
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
        }
        #endregion

        string toDay = DateTime.Now.ToString("yyyy-MM-dd");

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                CategoryGroupInit();
                LoadGridPanelStock();
            }
        }

        protected void LoadGridPanelStock()
        {
            string SQLT_SELECT_ALL = @"
                    SELECT invs_id, invs_ctg, invs_name, invs_inamt, invs_outamt, invs_invamt, invs_minstock, price, cost, invs_lastupdate
                    FROM inventory_stock 
                    WHERE invs_status='Y' 
                    ORDER BY invs_id;";
            string SQLT_SELECT_BY_CATEGORY = @"
                    SELECT invs_id, invs_ctg, invs_name, invs_inamt, invs_outamt, invs_invamt, invs_minstock, price, cost, invs_lastupdate 
                    FROM inventory_stock
                    WHERE invs_category={0}
                    ORDER BY invs_id;";
            string sSql = null;
            string cbSel = (string)cbInvCategory.Value;

            if (cbSel == null || cbSel == "0") {
                sSql = SQLT_SELECT_ALL;
            } else {
                sSql = string.Format(SQLT_SELECT_BY_CATEGORY, cbSel);
            }

            DataTable dt = db.Query(sSql);

            StoreStock.DataSource = db.GetDataArray_AddRowNum(dt);
            StoreStock.DataBind();
        }

        protected void cmdDelete(object sender, DirectEventArgs e)
        {
            try
            {
                string invsid = e.ExtraParams["invsid"];
                string sSQL = "UPDATE inventory_stock SET invs_status='N',";
                sSQL += "invs_lastupdate='" + DateTime.Now.ToString("yyyy-MM-dd") + "',";
                sSQL += "invs_lastamt=invs_invamt,";
                sSQL += "invs_lastusr='" + Session["USER_NAME"].ToString() + "' ";
                sSQL += "WHERE invs_id=" + invsid + "; ";
                db.Excute(sSQL);
                Common._NotificationShow("资料已删除");
                LoadGridPanelStock();
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow("资料删除失败!");
            }
        }

        private void CategoryGroupInit() {
            this.StoreInvCategory.DataSource = new object[] {
                new object[] {0, "(全部)"},
                new object[] {1, "材料"},
                new object[] {2, "药品"},
            };

            cbInvCategory.SetValue("0");
        }

        protected void onInvCategoryGroupChanged(object sender, DirectEventArgs e) {
            LoadGridPanelStock();
        }

        protected void GenPutIn(object sender, DirectEventArgs e)
        {
            string invs_id = e.ExtraParams["invs_id"];
            string sSQL = "SELECT * FROM inventory_stock ";
            sSQL += "WHERE invs_id='" + invs_id + "'";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
            {
                txt_ctg.Text = dt.Rows[0]["invs_ctg"].ToString();
                txt_name.Text = dt.Rows[0]["invs_name"].ToString();
                txt_safeqty.Text = dt.Rows[0]["invs_minstock"].ToString();
                txt_inamt.Text = dt.Rows[0]["invs_inamt"].ToString();
                txt_invamt.Text = dt.Rows[0]["invs_invamt"].ToString();
                txt_price.Text = dt.Rows[0]["price"].ToString();
                txt_cost.Text = dt.Rows[0]["cost"].ToString();
                Window1.Show();
                txt_putinqty.Text = "";
                txt_putinqty.Focus(false, 100);
            }
            else
            {
                Common._ErrorMsgShow("资料异常,无法建立入库!");
            }
        }

        protected void OnPutin_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string sSQL = "";
                if (txt_putinqty.Text != "" && txt_putinqty.Text != "0")
                {
                    string serialno = DateTime.Now.ToString("yyyyMMddHHmmss");
                    string toDay = DateTime.Now.ToString("yyyy-MM-dd");
                    sSQL = "INSERT INTO inv_received (ivrec_code, ivrec_name, ivrec_serialno, ivrec_amt, ivrec_daterec, ivrec_user) ";
                    sSQL += "VALUES('" + txt_ctg.Text + "',";
                    sSQL += "'" + txt_name.Text + "',";
                    sSQL += "'" + serialno + "',";
                    sSQL += "'" + txt_putinqty.Text + "',";
                    sSQL += "'" + toDay + "',";
                    sSQL += "'" + Session["USER_NAME"].ToString() + "'); ";
                }
                else
                {
                    txt_putinqty.Text = "0";
                }

                int amt = int.Parse(txt_invamt.Text) + int.Parse(txt_putinqty.Text);
                int amt1 = int.Parse(txt_inamt.Text) + int.Parse(txt_putinqty.Text);
                sSQL += "UPDATE inventory_stock SET ";
                sSQL += "invs_invamt='" + amt.ToString() + "',";
                sSQL += "invs_inamt='" + amt1.ToString() + "',";
                sSQL += "invs_minstock='" + txt_safeqty.Text + "',";
                sSQL += "price=" + txt_price.Text + ",";
                sSQL += "cost=" + txt_cost.Text + " ";
                sSQL += "WHERE invs_name='" + txt_name.Text + "';";
                db.Excute(sSQL);
                Window1.Hide();
                LoadGridPanelStock();
                Common._NotificationShow("入庫单,存盘成功!");
            }
            catch (Exception)
            {
                Common._ErrorMsgShow("入庫单,存盘失败");
            }
        }

        #region 添加
        protected void btnAdd_Click(object sender, DirectEventArgs e)
        {
            Window2.Title = "新增" + Common.GetComboBoxText(cbInvCategory);
            Window2.Show();
            txt_ctg2.Focus(false, 100);
        }
        #endregion

        protected void OnSave_Click(object sender, DirectEventArgs e)
        {
            try
            {
                string InvCategory = Common.GetComboBoxValue(cbInvCategory);
                if(InvCategory == "0")
                {
                    InvCategory = "1";
                }
                if (txt_price2.Text == "")
                    txt_price2.Text = "0";
                if (txt_cost2.Text == "")
                    txt_cost2.Text = "0";
                string sSQL = "INSERT INTO inventory_stock (invs_category,invs_ctg,invs_name,invs_inamt,invs_outamt,invs_invamt,invs_minstock,invs_lastupdate,invs_lastusr,price,cost,invs_status) ";
                sSQL += "VALUES('" + InvCategory + "',";
                sSQL += "'" + txt_ctg2.Text + "',";
                sSQL += "'" + txt_name2.Text + "','0','0','0',";
                sSQL += "'" + txt_safeqty2.Text + "',";
                sSQL += "'" + DateTime.Now.ToString("yyyy-MM-dd") + "',";
                sSQL += "'" + Session["USER_NAME"].ToString() + "',";
                sSQL += "'" + txt_price2.Text + "',";
                sSQL += "'" + txt_cost2.Text + "','Y')";
                db.Excute(sSQL);
                LoadGridPanelStock();
                Common._NotificationShow("材料入庫,存盘成功!");
                txt_ctg2.Text = "";
                txt_name2.Text = "";
                txt_safeqty2.Text = "";
                txt_price2.Text = "";
                txt_cost2.Text = "";
            }
            catch (Exception ex)
            {
                Common._ErrorMsgShow("材料入庫,存盘失败");
            }
            Window2.Close();
        }
    }
}