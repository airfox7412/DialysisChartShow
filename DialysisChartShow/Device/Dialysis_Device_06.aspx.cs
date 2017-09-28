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

namespace Dialysis_Chart_Show.Device
{
    public partial class Dialysis_Device_06 : BaseForm
    {
        #region class dv_06
        public class dv_06
        {
            public string dvId 
            {
                get;
                set;
            }
            public string createDate 
            {
                get;
                set;
            }
            public string dv_machine 
            {
                get;
                set;
            }
            public string dv_content
            {
                get;
                set;
            }
            public string dv_worker 
            {
                get;
                set;
            }
            public string dv_company
            {
                get;
                set;
            }
            public string dv_staff 
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
                if (Session["USER_ID"] == null)
                {
                    X.Redirect("login.aspx");
                }
                else
                {
                    SetComboBox();
                    GridPanelBind();
                }
            }
        }

        protected void SetComboBox()
        {
            string sSQL = "SELECT dv_Type AS TypeName FROM dv_04 ";
            sSQL += "GROUP BY dv_Type ";
            sSQL += "ORDER BY dv_Type ";
            DataTable dt = db.Query(sSQL);
            Common.SetComboBoxItem(cbo_macType, dt, false, "TypeName", "TypeName");
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT * FROM dv_06 ";
            sSQL += "ORDER BY dvId";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        #region 添加
        protected void cmdADD(object sender, DirectEventArgs e)
        {
            this.btnAdd.Disabled = true;
            this.btnSave.Disabled = false;
            this.btnCANCEL.Disabled = false;
        }
        #endregion

        #region 修改
        protected void cmdSAVE(object sender, DirectEventArgs e)
        {
            try
            {
                string sSQL = "";
                ChangeRecords<dv_06> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<dv_06>();
                foreach (dv_06 updated in sdh.Updated)
                {
                    sSQL += "UPDATE dv_06 ";
                    sSQL += "SET createDate='" + _Get_YMD2(updated.createDate.ToString()) + "',";
                    sSQL += "dv_machine='" + updated.dv_machine + "',";
                    sSQL += "dv_content='" + updated.dv_content + "',";
                    sSQL += "dv_worker='" + updated.dv_worker + "',";
                    sSQL += "dv_company='" + updated.dv_company + "',";
                    sSQL += "dv_staff='" + updated.dv_staff + "' ";
                    sSQL += "WHERE dvId='" + updated.dvId + "'; ";
                }

                foreach (dv_06 updated in sdh.Created)
                {
                    sSQL = "INSERT INTO dv_06 ";
                    sSQL += "(createDate, dv_machine, dv_content, dv_worker, dv_company, dv_staff) ";
                    sSQL += "VALUES ('" + _Get_YMD2(updated.createDate.ToString()) + "',";
                    sSQL += "'" + updated.dv_machine + "',";
                    sSQL += "'" + updated.dv_content + "',";
                    sSQL += "'" + updated.dv_worker + "',";
                    sSQL += "'" + updated.dv_company + "',";
                    sSQL += "'" + updated.dv_staff.ToString() + "')";
                }
                db.Excute(sSQL);
                cmdCANCEL(sender, e);
                Common._NotificationShow("设备保养存盘成功!");
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
            GridPanelBind();
            this.btnAdd.Disabled = false;
            this.btnSave.Disabled = true;
            this.btnCANCEL.Disabled = true;
        }
        #endregion
        
        protected void cmd_Delete(object sender, DirectEventArgs e)
        {
            string dvId = e.ExtraParams["dvId"];
            string sSQL = "DELETE FROM dv_06 WHERE dvId='" + dvId + "'; ";
            if (db.Excute(sSQL))
            {
                Common._NotificationShow("资料已删除");
                cmdCANCEL(sender, e);
            }
        }
    }
}