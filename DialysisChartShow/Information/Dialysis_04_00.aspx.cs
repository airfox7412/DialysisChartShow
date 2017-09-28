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

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_04_00 : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
            }
            GridPanelBind();
        } 

        protected void GridPanelBind()
        {
            string sSQL;
            sSQL = "SELECT GROUP_CODE, GROUP_NAME, GROUP_NAME_E, GROUP_CLASS, ";
            sSQL += "IF(GROUP_USED='Y',true,false) as USED ";
            sSQL += "FROM a_item_group ";
            sSQL += "ORDER BY GROUP_CLASS ";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }
        
        #region 取得檢查名稱
        private string GetGroupName(string sID)
        {
            string sSQL = "SELECT GROUP_CODE, GROUP_NAME FROM a_item_group ";
            sSQL += "WHERE GROUP_CODE='" + sID.Substring(0, 4) + "' ";
            sSQL += "GROUP BY GROUP_CODE ";
            try
            {
                DataTable dt = db.Query(sSQL);
                return dt.Rows[0]["GROUP_NAME"].ToString();
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region 取得檢查碼
        private string GetGroupCode(string sName)
        {
            string sSQL = "SELECT GROUP_CODE, GROUP_NAME FROM a_item_group ";
            sSQL += "WHERE GROUP_NAME='" + sName + "' ";
            sSQL += "GROUP BY GROUP_CODE ";
            try
            {
                DataTable dt = db.Query(sSQL);
                return dt.Rows[0]["GROUP_CODE"].ToString();
            }
            catch
            {
                return "";
            }
        }
        #endregion

        #region class ITEM_GROUP
        public class ITEM_GROUP
        {
            public string GROUP_CODE
            {
                get;
                set;
            }
            public string GROUP_NAME
            {
                get;
                set;
            }
            public string GROUP_NAME_E
            {
                get;
                set;
            }
            public string GROUP_CLASS
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
        }
        #endregion

        #region 修改
        protected void cmdSAVE(object sender, DirectEventArgs e)
        {
            string sSQL = "";
            string GroupUsed = "N";
            ChangeRecords<ITEM_GROUP> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<ITEM_GROUP>();
            foreach (ITEM_GROUP updated in sdh.Created)
            {
                if (updated.USED.ToString() == "true")
                {
                    GroupUsed = "Y";
                }
                sSQL = "INSERT INTO a_item_group ";
                sSQL += "(GROUP_CODE, GROUP_NAME, GROUP_NAME_E, GROUP_CLASS, GROUP_USED) ";
                sSQL += "VALUES ('" + updated.GROUP_CODE + "', ";
                sSQL += "'" + updated.GROUP_NAME + "', ";
                sSQL += "'" + updated.GROUP_NAME_E + "', ";
                sSQL += "'" + updated.GROUP_CLASS + "', ";
                sSQL += "'" + GroupUsed + "') ";
                db.Excute(sSQL);
            }

            foreach (ITEM_GROUP updated in sdh.Updated)
            {
                if (updated.USED.ToString()=="true")
                {
                    GroupUsed = "Y";
                }
                sSQL = "UPDATE a_item_group ";
                sSQL += "SET GROUP_NAME='" + updated.GROUP_NAME + "', ";
                sSQL += "GROUP_NAME_E='" + updated.GROUP_NAME_E + "', ";
                sSQL += "GROUP_CLASS='" + updated.GROUP_CLASS + "', ";
                sSQL += "GROUP_USED='" + GroupUsed + "' ";
                sSQL += "WHERE GROUP_CODE='" + updated.GROUP_CODE + "'; ";
                db.Excute(sSQL);
            }            
            Store1.CommitChanges();
            Store1.Reload();
            btnCancel.Disabled = true;
            btnSave.Disabled = true;
            btnDelete.Disabled = true;
            btnAdd.Disabled = false;
        }
        #endregion

        #region 取消新增動作
        protected void cmdCANCEL(object sender, DirectEventArgs e)
        {
            //Store1.RemoveAll(true);
            GridPanelBind();
            btnCancel.Disabled = true;
            btnSave.Disabled = true;
            btnDelete.Disabled = true;
            btnAdd.Disabled = false;
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
                string GROUP_CODE;
                string sSQL = "";
                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    GROUP_CODE = row.SelectSingleNode("GROUP_CODE").InnerXml;
                    sSQL += "DELETE FROM a_item_group ";
                    sSQL += "WHERE GROUP_CODE='" + GROUP_CODE + "' ";
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