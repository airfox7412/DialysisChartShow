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
    public partial class Dialysis_System_08 : BaseForm
    {

        #region class hpack3_setup
        public class hpack3_setup
        {
            public string hp3_id
            {
                get;
                set;
            }
            public string hp3_code
            {
                get;
                set;
            }
            public string hp3_name
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
            string sSQL = "SELECT hp3_id, hp3_name, hp3_code, IF(hp3_status='Y',true,false) as USED ";
            sSQL += "FROM hpack3_setup ";
            sSQL += "ORDER BY hp3_id ";
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
                ChangeRecords<hpack3_setup> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<hpack3_setup>();
                foreach (hpack3_setup updated in sdh.Updated)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                        itemUsed = "Y";
                    sSQL += "UPDATE hpack3_setup ";
                    sSQL += "SET hp3_name='" + updated.hp3_name + "', ";
                    sSQL += "hp3_code='" + updated.hp3_code + "', ";
                    sSQL += "hp3_status='" + itemUsed + "' ";
                    sSQL += "WHERE hp3_id='" + updated.hp3_id + "'; ";
                    db.Excute(sSQL);
                }

                foreach (hpack3_setup updated in sdh.Created)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "true"))
                    {
                        itemUsed = "Y";
                    }
                    sSQL = "INSERT INTO hpack3_setup ";
                    sSQL += "(hp3_name, hp3_code, hp3_status) ";
                    sSQL += "VALUES ('" + updated.hp3_name + "', ";
                    sSQL += "'" + updated.hp3_code + "', ";
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
            this.btnCANCEL.Disabled = true;
            this.btnAdd.Disabled = false;
        }
        #endregion
    }
}