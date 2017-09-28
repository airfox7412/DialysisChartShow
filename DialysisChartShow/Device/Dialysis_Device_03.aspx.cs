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
    public partial class Dialysis_Device_03 : BaseForm
    {
        #region class dv_03
        public class dv_03
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
            public string vacType
            {
                get;
                set;
            }
            public string vacDose
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
            string[] GroupName = new string[2] { "甲肝疫苗", "乙肝疫苗" };
            SetSingleCombo(cbo_vacType, GroupName);
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT * FROM dv_03 ";
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
                ChangeRecords<dv_03> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<dv_03>();
                foreach (dv_03 updated in sdh.Updated)
                {
                    sSQL += "UPDATE dv_03 ";
                    sSQL += "SET createDate='" + _Get_YMD2(updated.createDate.ToString()) + "', ";
                    sSQL += "empName='" + updated.empName + "', ";
                    sSQL += "vacType='" + updated.vacType + "', ";
                    sSQL += "vacDose='" + updated.vacDose + "' ";
                    sSQL += "WHERE dvId='" + updated.dvId + "';";
                }

                foreach (dv_03 updated in sdh.Created)
                {
                    sSQL = "INSERT INTO dv_03 ";
                    sSQL += "(createDate, empName, vacType, vacDose) ";
                    sSQL += "VALUES ('" + _Get_YMD2(updated.createDate.ToString()) + "','";
                    sSQL += updated.empName + "','";
                    sSQL += updated.vacType + "','";
                    sSQL += updated.vacDose + "'); ";
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

        protected void cmdDelete(object sender, DirectEventArgs e)
        {
            string dv_Id = e.ExtraParams["dvId"];
            string sSQL = "DELETE FROM dv_03 WHERE dvId='" + dv_Id + "';";
            if (db.Excute(sSQL))
            {
                Common._NotificationShow("资料已删除");
                cmdCANCEL(sender, e);
            }
        }
    }
}