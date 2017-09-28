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
    public partial class Dialysis_Device_04 : BaseForm
    {
        #region class dv_04
        public class dv_04
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
            public string dv_Kind 
            {
                get;
                set;
            }
            public string dv_Brand 
            {
                get;
                set;
            }
            public string dv_Type 
            {
                get;
                set;
            }
            public string dv_Serialno 
            {
                get;
                set;
            }
            public string dv_sdate 
            {
                get;
                set;
            }
            public string dv_ddate
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
            string[] GroupName = new string[3] { "血透机", "腹透机", "CRRT" };
            SetSingleCombo(cbo_macType, GroupName);
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT * FROM dv_04 ";
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
            this.btnDelete.Disabled = false;
        }
        #endregion

        #region 修改
        protected void cmdSAVE(object sender, DirectEventArgs e)
        {
            try
            {
                string sSQL = "";
                ChangeRecords<dv_04> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<dv_04>();
                foreach (dv_04 updated in sdh.Updated)
                {
                    sSQL += "UPDATE dv_04 ";
                    sSQL += "SET createDate='" + _Get_YMD2(updated.createDate.ToString()) + "',";
                    sSQL += "dv_Kind='" + updated.dv_Kind + "', ";
                    sSQL += "dv_Brand='" + updated.dv_Brand + "', ";
                    sSQL += "dv_Type='" + updated.dv_Type + "', ";
                    sSQL += "dv_Serialno='" + updated.dv_Serialno + "', ";
                    if (updated.dv_sdate != null)
                        sSQL += "dv_sdate='" + _Get_YMD2(updated.dv_sdate.ToString()) + "',";
                    else
                        sSQL += "dv_sdate='', ";
                    if (updated.dv_ddate != null)
                        sSQL += "dv_ddate='" + _Get_YMD2(updated.dv_ddate.ToString()) + "' ";
                    else
                        sSQL += "dv_ddate='' ";
                    sSQL += "WHERE dvId='" + updated.dvId + "'; ";
                }

                foreach (dv_04 updated in sdh.Created)
                {
                    sSQL = "INSERT INTO dv_04 ";
                    sSQL += "(createDate, dv_Kind, dv_Brand, dv_Type, dv_Serialno, dv_sdate, dv_ddate) ";
                    sSQL += "VALUES ('" + _Get_YMD2(updated.createDate.ToString()) + "',";
                    sSQL += "'" + updated.dv_Kind + "',";
                    sSQL += "'" + updated.dv_Brand + "',";
                    sSQL += "'" + updated.dv_Type + "',";
                    sSQL += "'" + updated.dv_Serialno + "',";
                    if (updated.dv_sdate != null)
                        sSQL += "'" + _Get_YMD2(updated.dv_sdate.ToString()) + "',";
                    else
                        sSQL += "'',";
                    if (updated.dv_ddate != null)
                        sSQL += "'" + _Get_YMD2(updated.dv_ddate.ToString()) + "')";
                    else
                        sSQL += "'')";
                }
                db.Excute(sSQL);
                cmdCANCEL(sender, e);
                Common._NotificationShow("设备档案存盘成功!");
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
            this.btnAdd.Disabled = false;
            this.btnSave.Disabled = true;
            this.btnCANCEL.Disabled = true;
            this.btnDelete.Disabled = true;
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
                string mac_id;
                string sSQL = "";
                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    mac_id = row.SelectSingleNode("mac_id").InnerXml;
                    sSQL += "DELETE FROM mac_setup WHERE mac_id='" + mac_id + "'; ";
                }
                if (db.Excute(sSQL))
                {
                    this.ResourceManager1.AddScript("Ext.Msg.alert('提示资讯', '资料已删除');");
                }
            }
        }
        #endregion

        protected void mac_Delete(object sender, DirectEventArgs e)
        {
            string dvId = e.ExtraParams["dvId"];
            string sSQL = "DELETE FROM dv_04 WHERE dvId='" + dvId + "'; ";
            if (db.Excute(sSQL))
            {
                Common._NotificationShow("资料已删除");
                cmdCANCEL(sender, e);
            }
        }
    }
}