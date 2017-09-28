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
    public partial class Dialysis_Device_05 : BaseForm
    {
        #region class dv05
        public class dv_05
        {
            public string dv_id
            {
                get;
                set;
            }
            public string infoDate
            {
                get;
                set;
            }
            public string CenterArea
            {
                get;
                set;
            }
            public string DialysisArea
            {
                get;
                set;
            }
            public string BedZone1
            {
                get;
                set;
            }
            public string BedZone2
            {
                get;
                set;
            }
            public string BedZone3
            {
                get;
                set;
            }
            public string BedZone4
            {
                get;
                set;
            }
            public string dvKind1
            {
                get;
                set;
            }
            public string dvKind2
            {
                get;
                set;
            }
            public string dvKind3
            {
                get;
                set;
            }
            public string dvKind4
            {
                get;
                set;
            }
            public string Eng1
            {
                get;
                set;
            }
            public string Eng2
            {
                get;
                set;
            }
            public string Eng3
            {
                get;
                set;
            }
            public string Eng4
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
                    GridPanelBind();
                }
            }
        }

        protected void GridPanelBind()
        {
            string sSQL = "";
            sSQL =  "SELECT dv_id, infoDate, CenterArea, DialysisArea, BedZone1, BedZone2, ";
            sSQL += "BedZone3, BedZone4, Eng1, Eng2, Eng3, Eng4, dvKind1, dvKind2, dvKind3, ";
            sSQL += "dvKind4 FROM dv_05 ORDER BY infoDate";
            DataTable dt = db.Query(sSQL);
            Store istore = this.GridPanel1.GetStore();
            istore.DataSource = db.GetDataArray_AddRowNum(dt);
            istore.DataBind();
        }

        #region 添加
        protected void cmdADD(object sender, DirectEventArgs e)
        {
            this.btnAdd.Disabled = true;
            info_date.Text = toDay;
            Window1.Show();
        }
        #endregion

        #region 删除
        protected void dvinfo_Delete(object sender, DirectEventArgs e)
        {
            string dvid = e.ExtraParams["dvid"];
            string sSQL = "DELETE FROM dv_05 WHERE dv_id='" + dvid + "'; ";
            if (db.Excute(sSQL))
            {
                Common._NotificationShow("资料已删除");
            }
            GridPanelBind();
        }
        #endregion

        #region 修改
        protected void Edit_dv05(object sender, DirectEventArgs e)
        {
            string dv_id = e.ExtraParams["dv_id"].ToString();
            string infoDate = e.ExtraParams["infoDate"].ToString();
            string CenterArea = e.ExtraParams["CenterArea"].ToString();
            string DialysisArea = e.ExtraParams["DialysisArea"].ToString();
            string BedZone1 = e.ExtraParams["BedZone1"].ToString();
            string BedZone2 = e.ExtraParams["BedZone2"].ToString();
            string BedZone3 = e.ExtraParams["BedZone3"].ToString();
            string BedZone4 = e.ExtraParams["BedZone4"].ToString();
            string dvKind1 = e.ExtraParams["dvKind1"].ToString();
            string dvKind2 = e.ExtraParams["dvKind2"].ToString();
            string dvKind3 = e.ExtraParams["dvKind3"].ToString();
            string dvKind4 = e.ExtraParams["dvKind4"].ToString();
            string Eng1 = e.ExtraParams["Eng1"].ToString();
            string Eng2 = e.ExtraParams["Eng2"].ToString();
            string Eng3 = e.ExtraParams["Eng3"].ToString();
            string Eng4 = e.ExtraParams["Eng4"].ToString();
            info_date.Text = infoDate;
            txt_dvKind1.Text = dvKind1;
            txt_dvKind2.Text = dvKind2;
            txt_dvKind3.Text = dvKind3;
            txt_dvKind4.Text = dvKind4;
            txt_centerArea.Text = CenterArea;
            txt_dialysisArea.Text = DialysisArea;
            txt_bedZone1.Text = BedZone1;
            txt_bedZone2.Text = BedZone2;
            txt_bedZone3.Text = BedZone3;
            txt_bedZone4.Text = BedZone4;
            txt_engineer1.Text = Eng1;
            txt_engineer2.Text = Eng2;
            txt_engineer3.Text = Eng3;
            txt_engineer4.Text = Eng4;
            Window1.Show();
            this.btnAdd.Disabled = true;
        }
        #endregion

        #region 資料編輯視窗 OK
        protected void BtnAccept_Click(object sender, DirectEventArgs e)
        {
            DataTable dt;
            string sql = "";
            sql = "SELECT dv_id FROM dv_05 ";
            sql += "WHERE infoDate = '" + _Get_YMD2(info_date.Text) + "'";
            dt = db.Query(sql);
            if (dt.Rows.Count > 0)
            {
                string index = dt.Rows[0]["dv_id"].ToString();

                sql = "UPDATE dv_05 ";
                sql += "SET infoDate='" + _Get_YMD2(info_date.Text) + "', ";
                sql += "CenterArea='" + txt_centerArea.Text + "', ";
                sql += "DialysisArea='" + txt_dialysisArea.Text + "', ";
                sql += "BedZone1='" + txt_bedZone1.Text + "', ";
                sql += "BedZone2='" + txt_bedZone2.Text + "', ";
                sql += "BedZone3='" + txt_bedZone3.Text + "', ";
                sql += "BedZone4='" + txt_bedZone4.Text + "', ";
                sql += "dvKind1='" + txt_dvKind1.Text + "', ";
                sql += "dvKind2='" + txt_dvKind2.Text + "', ";
                sql += "dvKind3='" + txt_dvKind3.Text + "', ";
                sql += "dvKind4='" + txt_dvKind4.Text + "', ";
                sql += "Eng1='" + txt_engineer1.Text + "', ";
                sql += "Eng2='" + txt_engineer2.Text + "', ";
                sql += "Eng3='" + txt_engineer3.Text + "', ";
                sql += "Eng4='" + txt_engineer4.Text + "' ";
                sql += "WHERE dv_id='" + index + "';";
            }
            else
            {
                sql = "INSERT INTO dv_05 ";
                sql += "(infoDate, CenterArea, DialysisArea, BedZone1, BedZone2, BedZone3, BedZone4, dvKind1, dvKind2, dvKind3, dvKind4, Eng1, Eng2, Eng3, Eng4) ";
                sql += "VALUES ('" + _Get_YMD2(info_date.Text) + "','";
                sql += txt_centerArea.Text + "','";
                sql += txt_dialysisArea.Text + "','";
                sql += txt_bedZone1.Text + "','";
                sql += txt_bedZone2.Text + "','";
                sql += txt_bedZone3.Text + "','";
                sql += txt_bedZone4.Text + "','";
                sql += txt_dvKind1.Text + "','";
                sql += txt_dvKind2.Text + "','";
                sql += txt_dvKind3.Text + "','";
                sql += txt_dvKind4.Text + "','";
                sql += txt_engineer1.Text + "','";
                sql += txt_engineer2.Text + "','";
                sql += txt_engineer3.Text + "','";
                sql += txt_engineer4.Text + "');";
            }
            try
            {
                db.Excute(sql);
            }
            catch
            {
            }
            ClearWindow();
            Window1.Close();
            GridPanelBind();
            this.btnAdd.Enable(true);
        }
        #endregion

        #region 資料編輯視窗 Cancel
        protected void BtnCancel_Click(object sender, DirectEventArgs e)
        {
            Window1.Close();
            ClearWindow();
            this.btnAdd.Enable(true);
        }
        #endregion

        protected void ClearWindow()
        {
            info_date.Text = "";
            txt_dvKind1.Text = "";
            txt_dvKind2.Text = "";
            txt_dvKind3.Text = "";
            txt_dvKind4.Text = "";
            txt_centerArea.Text = "";
            txt_dialysisArea.Text = "";
            txt_bedZone1.Text = "";
            txt_bedZone2.Text = "";
            txt_bedZone3.Text = "";
            txt_bedZone4.Text = "";
            txt_engineer1.Text = "";
            txt_engineer2.Text = "";
            txt_engineer3.Text = "";
            txt_engineer4.Text = "";
        }
    }
}