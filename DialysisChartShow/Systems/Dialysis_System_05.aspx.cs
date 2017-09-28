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
    public partial class Dialysis_System_05 : BaseForm
    {
        #region class access_level
        public class access_level
        {
            public string acclv_id
            {
                get;
                set;
            }
            public string name
            {
                get;
                set;
            }
            public string usrnm
            {
                get;
                set;
            }
            public string passwd
            {
                get;
                set;
            }
            public string type
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
                if (Session["USER_ID"] == null)
                {
                    X.Redirect("login.aspx");
                }
                else
                {
                    if (Session["USER_RIGHT"].ToString() == "AD" || Session["USER_RIGHT"].ToString() == "DH")
                    {
                        GetGroupName();
                        GridPanelBind();
                    }
                    else
                    {
                        Common._ErrorMsgShow("您没有权限访问!");
                    }
                }
            }
        }

        private void GetGroupName()
        {
            string[] typecode = { "DH", "HN", "NU", "DC", "SK", "AD" };
            string[] typename = { "主任", "护理长", "护士", "医生", "行政", "系统管理员" };
            Ext.Net.ListItem litem1;
            for (int i = 0; i < typecode.Length; i++)
            {
                litem1 = new Ext.Net.ListItem(typename[i], typecode[i]);
                ComboBoxGroup.Items.Add(litem1);
                ComboBox1.Items.Add(litem1);
            }
        }

        protected string getTypeName(string typecode)
        {
            string typename = "";
            if (typecode == "DH")
            {
                typename = "主任";
            }
            else if (typecode == "HN")
            {
                typename = "护理长";
            }
            else if (typecode == "NU")
            {
                typename = "护士";
            }
            else if (typecode == "DC")
            {
                typename = "医生";
            }
            else if (typecode == "SK") 
            {
                typename = "行政";
            }
            else if (typecode == "AD")
            {
                typename = "系统管理员";
            }
            return typename;
        }

        protected string getTypeCode(string typename)
        {
            string typecode = typename;
            if (typename == "主任")
            {
                typecode = "DH";
            }
            else if (typename == "护理长")
            {
                typecode = "HN";
            }
            else if (typename == "护士")
            {
                typecode = "NU";
            }
            else if (typename == "医生")
            {
                typecode = "DC";
            }
            else if (typename == "行政")
            {
                typecode = "SK";
            }
            else if (typename == "系统管理员")
            {
                typecode = "AD";
            }
            return typecode;
        }
        protected void ChangGroup(object sender, DirectEventArgs e)
        {
            GridPanelBind();
        }

        protected void GridPanelBind()
        {
            string sSQL = "SELECT acclv_id, name, usrnm, passwd, type, IF(active='A',true,false) as USED ";
            sSQL += "FROM access_level ";
            sSQL += "WHERE usrnm<>'admin' ";
            if (ComboBoxGroup.Text != "")
            {
                sSQL += "AND type='" + GetComboBoxValue(ComboBoxGroup) + "' ";
            }
            sSQL += "ORDER BY acclv_id ";
            DataTable dt = db.Query(sSQL);

            System.Data.DataView dv = dt.DefaultView;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dv[i]["type"] = getTypeName(dt.Rows[i]["type"].ToString());
            }
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
                ChangeRecords<access_level> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<access_level>();
                foreach (access_level updated in sdh.Updated)
                {
                    if (updated.USED.ToString() == "1" || updated.USED.ToString() == "true")
                        itemUsed = "A";
                    else
                        itemUsed = "NA";
                    sSQL += "UPDATE access_level ";
                    sSQL += "SET name='" + updated.name + "', ";
                    sSQL += "usrnm='" + updated.usrnm + "', ";
                    sSQL += "passwd='" + updated.passwd + "', ";
                    sSQL += "type='" + getTypeCode(updated.type) + "', ";
                    sSQL += "active='" + itemUsed + "' ";
                    sSQL += "WHERE acclv_id=" + updated.acclv_id + "; ";
                    db.Excute(sSQL);
                }

                foreach (access_level updated in sdh.Created)
                {
                    if (updated.USED.ToString() == "1" || updated.USED.ToString() == "true")
                        itemUsed = "A";
                    else
                        itemUsed = "NA";
                    sSQL = "INSERT INTO access_level ";
                    sSQL += "(name, usrnm, passwd, type, active) ";
                    sSQL += "VALUES ('" + updated.name + "', ";
                    sSQL += "'" + updated.usrnm + "', ";
                    sSQL += "'" + updated.passwd + "', ";
                    sSQL += "'" + getTypeCode(updated.type) + "', ";
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
                string acclv_id;
                string sSQL = "";
                foreach (XmlNode row in xml.SelectNodes("records/record"))
                {
                    acclv_id = row.SelectSingleNode("acclv_id").InnerXml;
                    sSQL += "DELETE FROM access_level ";
                    sSQL += "WHERE acclv_id='" + acclv_id + "' ";
                }
                if (db.Excute(sSQL))
                {
                    this.ResourceManager1.AddScript("Ext.Msg.alert('提示资讯', '资料已删除');");
                }
            }
        }
        #endregion

        protected void ChangePwd(object sender, DirectEventArgs e)
        {
            Userid.Text = e.ExtraParams["acclv_id"];
            if (Userid.Text != "1" || Session["USER_RIGHT"].ToString()=="AD")
            {
                txtPwd1.Text = "";
                txtPwd2.Text = "";
                Window1.Show();
                txtPwd1.Focus(true, 100);
            }
        }

        protected void Next_KeyPress(object sender, DirectEventArgs e)
        {
            string key = e.ExtraParams["keynum"].ToString();
            if (key == "13")
            {
                txtPwd2.Focus(true, 100);
            }
        }

        protected void Login_KeyPress(object sender, DirectEventArgs e)
        {
            string key = e.ExtraParams["keynum"].ToString();
            if (key == "13")
            {
                Edit_Click(sender, e);
            }
        }

        protected void Edit_Click(object sender, DirectEventArgs e)
        {
            if (txtPwd1.Text == txtPwd2.Text)
            {
                try
                {
                    JiaMiJieMi aeskey = new JiaMiJieMi();
                    string pwdencode = aeskey.Base64Encrypt(aeskey.AES_Encrypt(txtPwd1.Text));
                    //string pwd = aeskey.AES_Decrypt(aeskey.Base64Decrypt(pwdencode));
                    string sSQL = "UPDATE access_level SET passwd='" + pwdencode + "' ";
                    sSQL += "WHERE acclv_id=" + Userid.Text;
                    db.Excute(sSQL);
                    Window1.Close();
                    Common._NotificationShow("密码修改成功!");
                }
                catch (Exception ex)
                {
                    Common._NotificationShow("密码修改失败!");
                }
            }
            else
            {
                Common._NotificationShow("请重新输入正确密码!");
                txtPwd1.Focus(true, 100);
            }
        }
    }
}