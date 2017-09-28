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
    public partial class Dialysis_04_01 : BaseForm
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                GetGroupName();
                ComboBoxGroup.Text = Request.QueryString["GroupName"]; 
            }
            GridPanelBind();
        }

        private void GetGroupName()
        {
            string sSQL;
            sSQL = "SELECT GROUP_NAME, GROUP_CODE FROM a_item_group ";
            //sSQL += "WHERE GROUP_USED='Y' ";
            sSQL += "ORDER BY GROUP_CLASS ";
            DataTable dt = db.Query(sSQL);
            Store istore = this.ComboBoxGroup.GetStore();
            istore.DataSource = db.GetDataArray(dt);
            istore.DataBind();
        }        

        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            string GroupCode = GetGroupCode((string)this.ComboBoxGroup.Value);
            string sSQL = "SELECT RITEM_CLASS,RITEM_CODE,RITEM_TYPE,RITEM_NAME_S,RITEM_NAME,RITEM_UNIT,";
                sSQL += "IF(RITEM_USED='Y',true,false) as USED ";
                sSQL += "FROM a_ritem_setup ";
                sSQL += "WHERE RITEM_CLASS='" + GroupCode + "' ";
                sSQL += "ORDER BY RITEM_USED DESC";
                DataTable dt = db.Query(sSQL);
                Store istore = this.GridPanel1.GetStore();
                istore.DataSource = db.GetDataArray_AddRowNum(dt);
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

        #region class ritem_setup
        public class ritem_setup
        {
            public string RITEM_CLASS
            {
                get;
                set;
            }
            public string RITEM_CODE
            {
                get;
                set;
            }
            public string RITEM_TYPE
            {
                get;
                set;
            }
            public string RITEM_NAME_S
            {
                get;
                set;
            }
            public string RITEM_NAME
            {
                get;
                set;
            }
            public string RITEM_UNIT
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
                string RitemUsed = "N";
                ChangeRecords<ritem_setup> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<ritem_setup>();
                foreach (ritem_setup updated in sdh.Created)
                {
                    if (updated.USED.ToString() == "True")
                    {
                        RitemUsed = "Y";
                    }
                    sSQL = "INSERT INTO a_ritem_setup ";
                    sSQL += "(RITEM_CODE, RITEM_TYPE, RITEM_NAME_S, RITEM_NAME, RITEM_UNIT, RITEM_CLASS, RITEM_USED) ";
                    sSQL += "VALUES('" + updated.RITEM_CODE + "', ";
                    sSQL += "'" + updated.RITEM_TYPE + "', ";
                    sSQL += "'" + updated.RITEM_NAME_S + "', ";
                    sSQL += "'" + updated.RITEM_NAME + "', ";
                    sSQL += "'" + updated.RITEM_UNIT + "', ";
                    sSQL += "'" + GetGroupCode((string)this.ComboBoxGroup.Value) + "', ";
                    sSQL += "'" + RitemUsed + "') ";
                    db.Excute(sSQL);
                }

                foreach (ritem_setup updated in sdh.Updated)
                {
                    if ((updated.USED.ToString() == "1") || (updated.USED.ToString() == "True"))
                    {
                        RitemUsed = "Y";
                    }
                    sSQL = "UPDATE a_ritem_setup ";
                    sSQL += "SET RITEM_CODE='" + updated.RITEM_CODE + "', ";
                    sSQL += "RITEM_TYPE='" + updated.RITEM_TYPE + "', ";
                    sSQL += "RITEM_NAME_S='" + updated.RITEM_NAME_S + "', ";
                    sSQL += "RITEM_NAME='" + updated.RITEM_NAME + "', ";
                    sSQL += "RITEM_UNIT='" + updated.RITEM_UNIT + "', ";
                    sSQL += "RITEM_USED='" + RitemUsed + "' ";
                    sSQL += "WHERE RITEM_CLASS='" + updated.RITEM_CLASS + "' AND RITEM_CODE='" + updated.RITEM_CODE + "'; ";
                    db.Excute(sSQL);
                }

                Store2.CommitChanges();
                Store2.Reload();
                btnSave.Disabled = true;
                btnDelete.Disabled = true;
                btnCANCEL.Disabled = true;
                btnAdd.Disabled = false;
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
                string RITEM_CODE;
                string RITEM_CLASS;
                string sSQL = "";
                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    RITEM_CODE = row.SelectSingleNode("RITEM_CODE").InnerXml;
                    RITEM_CLASS = row.SelectSingleNode("RITEM_CLASS").InnerXml;
                    sSQL += "DELETE FROM a_ritem_setup ";
                    sSQL += "WHERE RITEM_CODE='" + RITEM_CODE + "' ";
                    sSQL += "AND RITEM_CLASS='" + RITEM_CLASS + "'; ";
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