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
    public partial class Dialysis_Device_02 : BaseForm
    {
        #region class dv_02
        public class dv_02
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
            public string HBsAg
            {
                get;
                set;
            }
            public string AntiHBs
            {
                get;
                set;
            }
            public string HBeAg
            {
                get;
                set;
            }
            public string AntiHBe
            {
                get;
                set;
            }
            public string AntiHBc
            {
                get;
                set;
            }
            public string AnitHCV
            {
                get;
                set;
            }
            public string Aids
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
            string[] GroupName = new string[4] { "阴性", "阳性", "弱阳性", "未检" };
            SetSingleCombo(cbo_HBsAg, GroupName);
            SetSingleCombo(cbo_AntiHBs, GroupName);
            SetSingleCombo(cbo_HBeAg, GroupName);
            SetSingleCombo(cbo_AntiHBe, GroupName);
            SetSingleCombo(cbo_AntiHBc, GroupName);
            SetSingleCombo(cbo_AntiHCV, GroupName);
            SetSingleCombo(cbo_Aids, GroupName);
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT dvId, createDate, empName, HBsAg, AntiHBs, HBeAg, AntiHBe, AntiHBc, AnitHCV, Aids FROM dv_02 ";
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
                ChangeRecords<dv_02> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<dv_02>();
                foreach (dv_02 updated in sdh.Updated)
                {
                    sSQL += "UPDATE dv_02 ";
                    sSQL += "SET createDate='" + _Get_YMD2(updated.createDate.ToString()) + "', ";
                    sSQL += "empName='" + updated.empName + "', ";
                    sSQL += "HBsAg='" + updated.HBsAg + "', ";
                    sSQL += "AntiHBs='" + updated.AntiHBs + "', ";
                    sSQL += "HBeAg='" + updated.HBeAg + "', ";
                    sSQL += "AntiHBe='" + updated.AntiHBe + "', ";
                    sSQL += "AntiHBc='" + updated.AntiHBc + "', ";
                    sSQL += "AnitHCV='" + updated.AnitHCV + "', ";
                    sSQL += "Aids='" + updated.Aids + "', ";
                    sSQL += "WHERE dvid='" + updated.dvId + "'; ";
                }

                foreach (dv_02 updated in sdh.Created)
                {
                    sSQL = "INSERT INTO dv_02 ";
                    sSQL += "(createDate, empName, HBsAg, AntiHBs, HBeAg, AntiHBe, AntiHBc, AnitHCV, Aids) ";
                    sSQL += "VALUES ('" + _Get_YMD2(updated.createDate.ToString()) + "','";
                    sSQL += updated.empName + "','";
                    sSQL += updated.HBsAg + "','";
                    sSQL += updated.AntiHBs + "','";
                    sSQL += updated.HBeAg + "','";
                    sSQL += updated.AntiHBe + "','";
                    sSQL += updated.AntiHBc + "','";
                    sSQL += updated.AnitHCV + "','";
                    sSQL += updated.Aids + "'); ";
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
        }
        #endregion

        protected void cmdDelete(object sender, DirectEventArgs e)
        {
            string dvId = e.ExtraParams["dvId"];
            string sSQL = "DELETE FROM dv_02 WHERE dvId=" + dvId;
            if (db.Excute(sSQL))
            {
                Common._NotificationShow("资料已删除");
                cmdCANCEL(sender, e);
            }
        }
    }
}