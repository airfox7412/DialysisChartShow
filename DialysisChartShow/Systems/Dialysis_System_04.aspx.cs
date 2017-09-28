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
    public partial class Dialysis_System_04 : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GridPanelBind();
            }
        }       

        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT pck_id, pck_code, pck_name, pck_dateadded, IF(pck_status='Y',true,false) as USED ";
            sSQL += "FROM package_setup ";
            sSQL += "ORDER BY pck_code ";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        #region class package_setup
        public class package_setup
        {
            public string pck_id
            {
                get;
                set;
            }
            public string pck_code
            {
                get;
                set;
            }
            public string pck_name
            {
                get;
                set;
            }
            public string pck_dateadded
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
                string toDay = DateTime.Now.ToString("yyyy-MM-dd");
                string sSQL = "";
                string itemUsed = "N";
                ChangeRecords<package_setup> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<package_setup>();
                foreach (package_setup updated in sdh.Updated)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                        itemUsed = "Y";
                    sSQL += "UPDATE package_setup SET ";
                    sSQL += "pck_code='" + updated.pck_code + "', ";
                    sSQL += "pck_name='" + updated.pck_name + "', ";
                    sSQL += "pck_status='" + itemUsed + "' ";
                    sSQL += "WHERE pck_id='" + updated.pck_id + "'; ";
                    db.Excute(sSQL);
                }

                foreach (package_setup updated in sdh.Created)
                {
                    sSQL = "INSERT INTO package_setup ";
                    sSQL += "(pck_code, pck_name, pck_dateadded, pck_status) ";
                    sSQL += "VALUES (";
                    sSQL += "'" + updated.pck_code + "', ";
                    sSQL += "'" + updated.pck_name + "', ";
                    sSQL += "'" + toDay + "', 'Y') ";
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
    }
}