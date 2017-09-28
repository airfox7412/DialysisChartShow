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
    public partial class Dialysis_Device_01 : BaseForm
    {
        #region class dv_01
        public class dv_01
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
            public string empName
            {
                get;
                set;
            }
            public string empSex
            {
                get;
                set;
            }
            public string idKind
            {
                get;
                set;
            }
            public string idNo
            {
                get;
                set;
            }
            public string empKind
            {
                get;
                set;
            }
            public string license
            {
                get;
                set;
            }
            public string eduLevel
            {
                get;
                set;
            }
            public string jobTitle
            {
                get;
                set;
            }
            public string jobDate
            {
                get;
                set;
            }
            public string quitDate
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
            Ext.Net.ListItem litem1;
            string[] GroupName = new string[3] { "","男","女" };
            for (int i = 0; i < GroupName.Length; i++)
            {
                litem1 = new Ext.Net.ListItem(GroupName[i], GroupName[i]);
                cbo_Sex.Items.Add(litem1);
            }

            GroupName = new string[4] { "身分证", "军官证", "护照", "其他" };
            for (int i = 0; i < GroupName.Length; i++)
            {
                litem1 = new Ext.Net.ListItem(GroupName[i], GroupName[i]);
                cbo_idkind.Items.Add(litem1);
            }

            GroupName = new string[4] { "医生", "护士", "工勤人员", "技术人员" };
            for (int i = 0; i < GroupName.Length; i++)
            {
                litem1 = new Ext.Net.ListItem(GroupName[i], GroupName[i]);
                cbo_empkind.Items.Add(litem1);
            }

            GroupName = new string[5] { "博士", "硕士", "学士", "大专", "中专" };
            for (int i = 0; i < GroupName.Length; i++)
            {
                litem1 = new Ext.Net.ListItem(GroupName[i], GroupName[i]);
                cbo_eduLevel.Items.Add(litem1);
            }

            GroupName = new string[7] { "住院医师", "主治医师", "副主任医师", "主任医师", "护士", "护师", "主管护师" };
            for (int i = 0; i < GroupName.Length; i++)
            {
                litem1 = new Ext.Net.ListItem(GroupName[i], GroupName[i]);
                cbo_jobTitle.Items.Add(litem1);
            }
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT * FROM dv_01 ";
            sSQL += "ORDER BY createDate";
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
                ChangeRecords<dv_01> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<dv_01>();
                foreach (dv_01 updated in sdh.Updated)
                {
                    sSQL += "UPDATE dv_01 ";
                    sSQL += "SET createDate='" + _Get_YMD2(updated.createDate.ToString()) + "', ";
                    sSQL += "empName='" + updated.empName + "', ";
                    sSQL += "empSex='" + updated.empSex + "', ";
                    sSQL += "idKind='" + updated.idKind + "', ";
                    sSQL += "idNo='" + updated.idNo + "', ";
                    sSQL += "empKind='" + updated.empKind + "', ";
                    sSQL += "license='" + updated.license + "', ";
                    sSQL += "eduLevel='" + updated.eduLevel + "', ";
                    sSQL += "jobTitle='" + updated.jobTitle + "', ";
                    if (updated.jobDate != null)
                        sSQL += "jobDate='" + _Get_YMD2(updated.jobDate.ToString()) + "',";
                    else
                        sSQL += "jobDate='',";
                    if (updated.quitDate != null)
                        sSQL += "quitDate='" + _Get_YMD2(updated.quitDate.ToString()) + "' ";
                    else
                        sSQL += "quitDate='' ";
                    sSQL += "WHERE dvId='" + updated.dvId + "'; ";
                }

                foreach (dv_01 updated in sdh.Created)
                {
                    sSQL = "INSERT INTO dv_01 ";
                    sSQL += "(createDate, empName, empSex, idKind, idNo, empKind, license, eduLevel, jobTitle, jobDate, quitDate) ";
                    sSQL += "VALUES ('" + _Get_YMD2(updated.createDate.ToString()) + "','";
                    sSQL += updated.empName + "','";
                    sSQL += updated.empSex + "','";
                    sSQL += updated.idKind + "','";
                    sSQL += updated.idNo + "','";
                    sSQL += updated.empKind + "','";
                    sSQL += updated.license + "','";
                    sSQL += updated.eduLevel + "','";
                    sSQL += updated.jobTitle + "',";
                    if (updated.jobDate != null)
                        sSQL += "'" + _Get_YMD2(updated.jobDate.ToString()) + "',";
                    else
                        sSQL += "'',";
                    if (updated.quitDate != null)
                        sSQL += "'" + _Get_YMD2(updated.quitDate.ToString()) + "')";
                    else
                        sSQL += "'')";
                }
                db.Excute(sSQL);
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
            string sSQL = "DELETE FROM dv_01 WHERE dvId='" + dvId + "'; ";
            if (db.Excute(sSQL))
            {
                Common._NotificationShow("资料已删除");
                cmdCANCEL(sender, e);
            }
        }
    }
}