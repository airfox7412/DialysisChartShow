using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_081 : BaseForm
    {
        private string _TableName = "zinfo_a_08";

        #region class EXAM_RESULT_LOG
        public class EXAM_RESULT_LOG
        {
            public string EXAM_DATE
            {
                get;
                set;
            }
            public string EXAM_NO
            {
                get;
                set;
            }
            public string EXAM_RESULT
            {
                get;
                set;
            }
            public string EXAM_DOCTOR
            {
                get;
                set;
            }
            public string EXAM_HOSPITAL
            {
                get;
                set;
            }
            public Int64 ROW_ID
            {
                get;
                set;
            }
            public string NEW_ID
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
                this.txtNODE_ID.Hidden = true;
                this.txtNODE_TEXT.Hidden = true;

                this.TreePanel1.ID = "TreePanel1";
                this.TreePanel1.Height = Unit.Pixel(400);
                this.TreePanel1.AutoScroll = true;

                Ext.Net.Button btnExpand = new Ext.Net.Button();
                btnExpand.Text = "展开";
                btnExpand.Listeners.Click.Handler = this.TreePanel1.ClientID + ".expandAll();";

                Ext.Net.Button btnCollapse = new Ext.Net.Button();
                btnCollapse.Text = "收合";
                btnCollapse.Listeners.Click.Handler = this.TreePanel1.ClientID + ".collapseAll();";

                Toolbar toolBar = new Toolbar();
                toolBar.ID = "ToolbarT";
                toolBar.Items.Add(btnExpand);
                toolBar.Items.Add(btnCollapse);
                //this.TreePanel1.TopBar.Add(toolBar);

                StatusBar statusBar1 = new StatusBar();
                statusBar1.ID = "StatusBarT";
                statusBar1.AutoClear = 1000;
                this.TreePanel1.BottomBar.Add(statusBar1);

                this.TreePanel1.Listeners.ItemClick.Handler = statusBar1.ClientID + ".setStatus({text: '点选: <b>' + record.data.text + '</b>', clear: false});";
                //tree.Listeners.ItemExpand.Handler = statusBar.ClientID + ".setStatus({text: 'Node Expanded: <b>' + item.data.text + '</b>', clear: false});";
                //tree.Listeners.ItemExpand.Buffer = 30;
                //tree.Listeners.ItemCollapse.Handler = statusBar.ClientID + ".setStatus({text: 'Node Collapsed: <b>' + item.data.text + '</b>', clear: false});";
                //tree.Listeners.ItemCollapse.Buffer = 30;

                Ext.Net.Node root = new Ext.Net.Node();
                root.Text = "新诊断信息";
                root.Icon = Icon.ReportUser;
                root.NodeID = "__";
                root.Cls = "large-font";

                Ext.Net.Node rA = new Ext.Net.Node();
                rA.Text = "一、临床诊断";
                rA.NodeID = "A1";
                rA.Cls = "large-font";
                rA.Leaf = true;

                Ext.Net.Node rB = new Ext.Net.Node();
                rB.Text = "二、合并症诊断";
                rB.NodeID = "B1";
                rB.Cls = "large-font";
                rB.Leaf = true;

                Ext.Net.Node rC = new Ext.Net.Node();
                rC.Text = "三、病因诊断";
                rC.NodeID = "C1";
                rC.Cls = "large-font";
                rC.Leaf = true;

                Ext.Net.Node rD = new Ext.Net.Node();
                rD.Text = "四、肾脏病理诊断";
                rD.NodeID = "D1";
                rD.Cls = "large-font";
                rD.Leaf = true;

                root.Children.Add(rA);
                root.Children.Add(rB);
                root.Children.Add(rC);
                root.Children.Add(rD);

                root.Expanded = true;

                this.TreePanel1.Root.Add(root);

                System.Data.DataTable DT = db.Query("SELECT * FROM zinfo_a_diag WHERE PAT_NO='" + _PAT_ID + "' ");

                Store istore = this.GridPanel1.GetStore();
                istore.DataSource = db.GetDataArray(DT);
                istore.DataBind();

                _zInfo_Show(_TableName, _PAT_ID, info_date.Text);
            }
        }

        private void panelHide()
        {
            this.Panel_1.Hidden = true;
            this.Panel_2.Hidden = true;
            this.Panel_3.Hidden = true;
            this.Panel_4.Hidden = true;
            this.Panel_5.Hidden = true;
        }
        
        protected void Node_Click(object sender, DirectEventArgs e)
        {
            this.txtNODE_ID.Text = e.ExtraParams["rID"];
            this.txtNODE_TEXT.Text = e.ExtraParams["rTEXT"];
            panelHide();

            switch (this.txtNODE_ID.Text)
            {
                case "A1"://临床诊断
                    this.Panel_1.Hidden = false;
                    this.Panel_5.Hidden = false;
                    Panel_5.Title = "一、临床诊断";
                    break;
                case "B1"://合并症诊断
                    this.Panel_2.Hidden = false;
                    this.Panel_5.Hidden = false;
                    Panel_5.Title = "二、合并症诊断";
                    break;
                case "C1"://病因诊断
                    this.Panel_3.Hidden = false;
                    this.Panel_5.Hidden = false;
                    Panel_5.Title = "三、病因诊断";
                    break;
                case "D1"://肾脏病理诊断
                    this.Panel_4.Hidden = false;
                    break;
                default:
                    break;
            }
        }

        protected void jj(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                this.txt_2.RemoveCls("TextField-Hide");
            else
                this.txt_2.AddCls("TextField-Hide");
        }

        protected void kk(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                this.txt_13.RemoveCls("TextField-Hide");
            else
                this.txt_13.AddCls("TextField-Hide");
        }

        protected void ll(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                this.txt_29.RemoveCls("TextField-Hide");
            else
                this.txt_29.AddCls("TextField-Hide");
        }

        protected void mm(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                this.txt_30.RemoveCls("TextField-Hide");
            else
                this.txt_30.AddCls("TextField-Hide");
        }

        protected void nn(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                this.txt_31.RemoveCls("TextField-Hide");
            else
                this.txt_31.AddCls("TextField-Hide");
        }

        protected void oo(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                this.txt_40.RemoveCls("TextField-Hide");
            else
                this.txt_40.AddCls("TextField-Hide");
        }

        protected void pp(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                this.txt_44.RemoveCls("TextField-Hide");
            else
                this.txt_44.AddCls("TextField-Hide");
        }

        protected void qq(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                this.txt_47.RemoveCls("TextField-Hide");
            else
                this.txt_47.AddCls("TextField-Hide");
        }

        protected void rr(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                this.txt_49.RemoveCls("TextField-Hide");
            else
                this.txt_49.AddCls("TextField-Hide");
        }

        protected void ss(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                this.txt_42.RemoveCls("TextField-Hide");
            else
                this.txt_42.AddCls("TextField-Hide");
        }

        protected void tt(object sender, DirectEventArgs e)
        {
            Ext.Net.ChangeRecords<EXAM_RESULT_LOG> sdh = new StoreDataHandler(e.ExtraParams["data"]).BatchObjectData<EXAM_RESULT_LOG>();

            foreach (EXAM_RESULT_LOG created in sdh.Created)
            {
                created.EXAM_DATE = created.EXAM_DATE.Substring(0, 10);
                Common._NotificationShow("新增<br>" + created.EXAM_DATE + "<br>" + created.EXAM_NO + "<br>" + created.EXAM_RESULT);
                string sSQL = "";
                sSQL = "INSERT INTO zinfo_a_diag (EXAM_DATE, EXAM_NO, EXAM_RESULT, EXAM_DOCTOR, EXAM_HOSPITAL, KIN_DATE, KIN_USER, PAT_NO) " +
                          "VALUE ('" + created.EXAM_DATE.Substring(0, 10) + "', " +
                                 "'" + created.EXAM_NO + "', " +
                                 "'" + created.EXAM_RESULT + "', " +
                                 "'" + created.EXAM_DOCTOR + "', " +
                                 "'" + created.EXAM_HOSPITAL + "', " +
                                 "'" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                                 "'" + _UserID + "', " +
                                 "'" + _PAT_ID + "')";
                db.Excute(sSQL);
                created.NEW_ID = null;
            }

            foreach (EXAM_RESULT_LOG updated in sdh.Updated)
            {
                updated.EXAM_DATE = updated.EXAM_DATE.Substring(0, 10);
                Common._NotificationShow("修改<br>" + updated.EXAM_DATE + "<br>" + updated.EXAM_NO + "<br>" + updated.EXAM_RESULT);
                string sSQL = "";
                sSQL = "UPDATE zinfo_a_diag " +
                          "SET EXAM_DATE='" + updated.EXAM_DATE.Substring(0, 10) + "', " +
                              "EXAM_NO='" + updated.EXAM_NO + "', " +
                              "EXAM_RESULT='" + updated.EXAM_RESULT + "', " +
                              "EXAM_DOCTOR='" + updated.EXAM_DOCTOR + "', " +
                              "EXAM_HOSPITAL='" + updated.EXAM_HOSPITAL + "', " +
                              "KIN_DATE='" + System.DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "', " +
                              "KIN_USER='" + _UserID + "', " +
                              "PAT_NO='" + _PAT_ID + "' " +
                        "WHERE ROW_ID=" + updated.ROW_ID.ToString() + " ";
                db.Excute(sSQL);
            }
            //似乎無此刪除功能
            foreach (EXAM_RESULT_LOG deleted in sdh.Deleted)
            {
                Common._NotificationShow("删除<br>" + deleted.EXAM_DATE + "<br>" + deleted.EXAM_NO + "<br>" + deleted.EXAM_RESULT);
                string sSQL = "";
                sSQL = "DELETE FROM zinfo_a_diag WHERE ROW_ID=" + deleted.ROW_ID.ToString() + " ";
                db.Excute(sSQL);
            }
            Store1.CommitChanges();
        }

        protected void uu(object sender, DirectEventArgs e)
        {
            Store1.RejectChanges();
        }

        #region 三、病因诊断-> 全部展开
        protected void OnExpansion(object sender, DirectEventArgs e)
        {
            Container_A.Hidden = false;
            Container_Aa.Hidden = false;
            Container_Aa1.Hidden = false;
            Container_Aa2.Hidden = false;
            Container_Ab.Hidden = false;
            Container_Ab1.Hidden = false;
            Container_Ab2.Hidden = false;
            Container_Ab2a.Hidden = false;
            Container_Ab2b.Hidden = false;
            Container_Ab3.Hidden = false;
            Container_Ab3d.Hidden = false;
            Container_Ab3e.Hidden = false;
            Container_Ab4.Hidden = false;
            Container_Ab5.Hidden = false;
            Container_Ab5a.Hidden = false;
            Container_Ab5b.Hidden = false;
            Container_Ab5c.Hidden = false;
            Container_Ac.Hidden = false;
            Container_Ac1.Hidden = false;
            Container_B.Hidden = false;
            Container_Ba.Hidden = false;
            Container_Ba1.Hidden = false;
            Container_Ba2.Hidden = false;
            Container_Ba3.Hidden = false;
            Container_Ba4.Hidden = false;
            Container_Ba5.Hidden = false;
            Container_Bb.Hidden = false;
            Container_Bc.Hidden = false;
            Container_Bd.Hidden = false;
            Container_Be.Hidden = false;
            Container_Bf.Hidden = false;
            Container_Bg.Hidden = false;
        }
        #endregion

        protected void cmd_A(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_A.Hidden = false;
            else
                Container_A.Hidden = true;
        }

        protected void cmd_Aa(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Aa.Hidden = false;
            else
                Container_Aa.Hidden = true;
        }

        protected void cmd_Aa1(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Aa1.Hidden = false;
            else
                Container_Aa1.Hidden = true;
        }

        protected void cmd_Aa2(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Aa2.Hidden = false;
            else
                Container_Aa2.Hidden = true;
        }

        protected void cmd_Ab(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab.Hidden = false;
            else
                Container_Ab.Hidden = true;
        }

        protected void cmd_Ab1(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab1.Hidden = false;
            else
                Container_Ab1.Hidden = true;
        }

        protected void cmd_Ab2(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab2.Hidden = false;
            else
                Container_Ab2.Hidden = true;
        }

        protected void cmd_Ab2a(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ab2a.Hidden = false;
            else
                Container_Ab2a.Hidden = true;
        }

        protected void cmd_Ab2b(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ab2b.Hidden = false;
            else
                Container_Ab2b.Hidden = true;
        }

        protected void cmd_Ab3(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab3.Hidden = false;
            else
                Container_Ab3.Hidden = true;
        }

        protected void cmd_Ab3d(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ab3d.Hidden = false;
            else
                Container_Ab3d.Hidden = true;
        }

        protected void cmd_Ab3e(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ab3e.Hidden = false;
            else
                Container_Ab3e.Hidden = true;
        }

        protected void cmd_Ab4(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab4.Hidden = false;
            else
                Container_Ab4.Hidden = true;
        }

        protected void cmd_Ab5(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab5.Hidden = false;
            else
                Container_Ab5.Hidden = true;
        }

        protected void cmd_Ab5a(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab5a.Hidden = false;
            else
                Container_Ab5a.Hidden = true;
        }

        protected void cmd_Ab5b(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab5b.Hidden = false;
            else
                Container_Ab5b.Hidden = true;
        }

        protected void cmd_Ab5c(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ab5c.Hidden = false;
            else
                Container_Ab5c.Hidden = true;
        }

        protected void cmd_Ac(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ac.Hidden = false;
            else
                Container_Ac.Hidden = true;
        }

        protected void cmd_Ac1(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ac1.Hidden = false;
            else
                Container_Ac1.Hidden = true;
        }

        protected void cmd_B(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_B.Hidden = false;
            else
                Container_B.Hidden = true;
        }

        protected void cmd_Ba(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Ba.Hidden = false;
            else
                Container_Ba.Hidden = true;
        }

        protected void cmd_Ba1(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ba1.Hidden = false;
            else
                Container_Ba1.Hidden = true;
        }

        protected void cmd_Ba2(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ba2.Hidden = false;
            else
                Container_Ba2.Hidden = true;
        }

        protected void cmd_Ba3(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ba3.Hidden = false;
            else
                Container_Ba3.Hidden = true;
        }

        protected void cmd_Ba4(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ba4.Hidden = false;
            else
                Container_Ba4.Hidden = true;
        }

        protected void cmd_Ba5(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Radio)sender).Checked == true)
                Container_Ba5.Hidden = false;
            else
                Container_Ba5.Hidden = true;
        }

        protected void cmd_Bb(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Bb.Hidden = false;
            else
                Container_Bb.Hidden = true;
        }

        protected void cmd_Bc(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Bc.Hidden = false;
            else
                Container_Bc.Hidden = true;
        }

        protected void cmd_Bd(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Bd.Hidden = false;
            else
                Container_Bd.Hidden = true;
        }

        protected void cmd_Be(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Be.Hidden = false;
            else
                Container_Be.Hidden = true;
        }

        protected void cmd_Bf(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Bf.Hidden = false;
            else
                Container_Bf.Hidden = true;
        }

        protected void cmd_Bg(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                Container_Bg.Hidden = false;
            else
                Container_Bg.Hidden = true;
        }

        protected void cmd_Bh(object sender, DirectEventArgs e)
        {
            if (((Ext.Net.Checkbox)sender).Checked == true)
                this.txt_50.RemoveCls("TextField-Hide");
            else
                this.txt_50.AddCls("TextField-Hide");
        }

        protected void cmdSAVE_Click(object sender, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);
        }

    }
}