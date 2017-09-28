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
    public partial class Dialysis_System_02 : BaseForm
    {

        #region class hpack2_setup
        public class hpack2_setup
        {
            public string hp2_id
            {
                get;
                set;
            }
            public string hp2_name
            {
                get;
                set;
            }
            public string hp2_code
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
                GridPanelBind();
            }
        } 

        protected void GridPanelBind()
        {
            string sSQL = "SELECT hp2_id, hp2_name, hp2_code, IF(hp2_status='Y',true,false) as USED ";
            sSQL += "FROM hpack2_setup ";
            sSQL += "ORDER BY hp2_id ";
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
                string itemUsed = "N";
                ChangeRecords<hpack2_setup> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<hpack2_setup>();
                foreach (hpack2_setup updated in sdh.Updated)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                        itemUsed = "Y";
                    sSQL += "UPDATE hpack2_setup ";
                    sSQL += "SET hp2_name='" + updated.hp2_name + "', ";
                    sSQL += "hp2_code='" + updated.hp2_code + "', ";
                    sSQL += "hp2_status='" + itemUsed + "' ";
                    sSQL += "WHERE hp2_id='" + updated.hp2_id + "'; ";
                    db.Excute(sSQL);
                }

                foreach (hpack2_setup updated in sdh.Created)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                    {
                        itemUsed = "Y";
                    }
                    sSQL = "INSERT INTO hpack2_setup ";
                    sSQL += "(hp2_name, hp2_code, hp2_status) ";
                    sSQL += "VALUES ('" + updated.hp2_name + "', ";
                    sSQL += "'" + updated.hp2_code + "', ";
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
                string hp2_id;
                string sSQL = "";
                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    hp2_id = row.SelectSingleNode("hp2_id").InnerXml;
                    sSQL += "DELETE FROM hpack2_setup ";
                    sSQL += "WHERE hp2_id='" + hp2_id + "' ";
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