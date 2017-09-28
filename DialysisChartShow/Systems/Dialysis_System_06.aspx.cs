using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using System.Configuration;
using Dialysis_Chart_Show.tools;
using Ext.Net;

namespace Dialysis_Chart_Show.Systems
{
    public partial class Dialysis_System_06 : BaseForm
    {
        #region class mac_setup
        public class mac_setup
        {
            public string mac_id
            {
                get;
                set;
            }
            public string mac_flr
            {
                get;
                set;
            }
            public string mac_sec
            {
                get;
                set;
            }
            public string mac_bedno
            {
                get;
                set;
            }
            public string mac_typ
            {
                get;
                set;
            }
            public string mac_com
            {
                get;
                set;
            }
            public string brand
            {
                get;
                set;
            }
            public string USED
            {
                get;
                set;
            }
            public string kind
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
                GetBrandName();
                GridPanelBind();
                string License = ConfigurationManager.AppSettings["1PageCount"].ToString();
                Rockey4ND rockey = new Rockey4ND();
                if (License.Length < 32) // keypro
                {
                    string hid = rockey.open();
                }
                string sSQL = "SELECT genst_desc FROM general_setup WHERE genst_ctg='License' AND genst_code='Plate'";
                DataTable dt = db.Query(sSQL);
                if (dt.Rows.Count > 0)
                {
                    JiaMiJieMi aeskey = new JiaMiJieMi();
                    Authorize.Text = aeskey.AES_Decrypt(aeskey.Base64Decrypt(dt.Rows[0]["genst_desc"].ToString().Substring(32, 32)));
                }
                if (License.Length < 32) // keypro
                {
                    rockey.close();
                }
            }
        }

        private void GetBrandName()
        {
            string sSQL;
            sSQL = "SELECT genst_desc AS NAME, genst_code AS CODE FROM general_setup ";
            sSQL += "WHERE genst_ctg='macbrd' ";
            sSQL += "ORDER BY genst_code ";
            DataTable dt = db.Query(sSQL);
            Common.SetComboBoxItem(Cb_brand, dt, false, "NAME", "NAME");
        }

        private string GetBrandCode(string name)
        {
            string sSQL = "SELECT genst_desc AS NAME, genst_code AS CODE FROM general_setup ";
            sSQL += "WHERE genst_ctg='macbrd' AND genst_desc='" + name + "' ";
            DataTable dt = db.Query(sSQL);
            if (dt.Rows.Count > 0)
                return dt.Rows[0]["CODE"].ToString();
            else
                return "";
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT a.mac_id, a.mac_flr, a.mac_sec, a.mac_bedno, a.mac_typ, a.mac_com, b.genst_desc as brand, ";
            sSQL += "IF(a.mac_status='Y',true,false) as USED, IF(a.mac_kind='Y',true,false) as kind ";
            sSQL += "FROM mac_setup a ";
            sSQL += "LEFT JOIN general_setup b ON b.genst_code=a.mac_brand AND b.genst_ctg='macbrd' ";
            sSQL += "ORDER BY a.mac_id";
            DataTable dt = db.Query(sSQL);
            NowBed.Text = dt.Rows.Count.ToString();
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
                string itemUsed = "N";
                string kindUsed = "N";
                ChangeRecords<mac_setup> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<mac_setup>();

                if (int.Parse(Authorize.Text) >= int.Parse(NowBed.Text)) //判斷是否達到授權數以內，就可以增加
                {
                    if(sdh.Created.Count>0)
                    {
                        sSQL = "";
                        foreach (mac_setup updated in sdh.Created)
                        {
                            if (updated.USED.ToString() == "true" || updated.USED.ToString() == "1")
                                itemUsed = "Y";
                            if (updated.kind.ToString() == "true" || updated.kind.ToString() == "1")
                                kindUsed = "Y";
                            sSQL += "INSERT INTO mac_setup ";
                            sSQL += "(mac_flr, mac_sec, mac_bedno, mac_brand, mac_typ, mac_com, mac_status, mac_kind) ";
                            sSQL += "VALUES ('" + updated.mac_flr + "', ";
                            sSQL += "'" + updated.mac_sec + "', ";
                            sSQL += "'" + updated.mac_bedno + "', ";
                            sSQL += "'" + GetBrandCode(updated.brand) + "', ";
                            sSQL += "'" + updated.mac_typ + "', ";
                            sSQL += "'" + updated.mac_com + "', ";
                            sSQL += "'" + itemUsed + "', ";
                            sSQL += "'" + kindUsed + "'); ";
                        }
                        db.Excute(sSQL);
                    }
                    cmdCANCEL(sender, e);
                }
                else
                {
                    Common._ErrorMsgShow("已经到达授权上限，无法储存！");
                }

                if (sdh.Updated.Count>0)
                {
                    sSQL = "";
                    foreach (mac_setup updated in sdh.Updated)
                    {
                        if (updated.USED.ToString() == "true" || updated.USED.ToString() == "1")
                            itemUsed = "Y";
                        if (updated.kind.ToString() == "true" || updated.kind.ToString() == "1")
                            kindUsed = "Y";
                        sSQL += "UPDATE mac_setup ";
                        sSQL += "SET mac_flr='" + updated.mac_flr + "', ";
                        sSQL += "mac_sec='" + updated.mac_sec + "', ";
                        sSQL += "mac_bedno='" + updated.mac_bedno + "', ";
                        sSQL += "mac_brand='" + GetBrandCode(updated.brand) + "', ";
                        sSQL += "mac_typ='" + updated.mac_typ + "', ";
                        sSQL += "mac_com='" + updated.mac_com + "', ";
                        sSQL += "mac_status='" + itemUsed + "', ";
                        sSQL += "mac_kind='" + kindUsed + "' ";
                        sSQL += "WHERE mac_id='" + updated.mac_id + "'; ";
                    }
                    db.Excute(sSQL);
                }
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

        #region 刪除
        //protected void cmdDelete(object sender, DirectEventArgs e)
        //{
        //    string json = e.ExtraParams["Values"];

        //    if (string.IsNullOrEmpty(json))
        //    {
        //        return;
        //    }
        //    else
        //    {
        //        XmlNode xml = JSON.DeserializeXmlNode("{records:{record:" + json + "}}");
        //        string mac_id;
        //        string sSQL = "";
        //        foreach (XmlNode row in xml.SelectNodes("records/record"))
        //        {
        //            mac_id = row.SelectSingleNode("mac_id").InnerXml;
        //            sSQL += "DELETE FROM mac_setup WHERE mac_id='" + mac_id + "'; ";
        //        }
        //        if (db.Excute(sSQL))
        //        {
        //            this.ResourceManager1.AddScript("Ext.Msg.alert('提示资讯', '资料已删除');");
        //        }
        //    }
        //}
        #endregion

        protected void mac_Delete(object sender, DirectEventArgs e)
        {
            string macid = e.ExtraParams["macid"];
            string sSQL = "DELETE FROM mac_setup WHERE mac_id=" + macid;
            if (db.Excute(sSQL))
            {
                Common._NotificationShow("资料已删除");
                cmdCANCEL(sender, e);
            }
        }
    }
}