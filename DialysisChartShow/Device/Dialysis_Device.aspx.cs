using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Xml;
using Ext.Net;
using Dialysis_Chart_Show.tools;

namespace Dialysis_Chart_Show.Device
{
    public partial class Dialysis_Device : System.Web.UI.Page 
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                BuildTree1(TreePanel1.Root);
            }
        }

        #region 取得樹狀目錄1
        private void BuildTree1(Ext.Net.NodeCollection nodes)
        {
            if (nodes == null)
            {
                nodes = new Ext.Net.NodeCollection();
            }
            Ext.Net.Node root = new Ext.Net.Node();
            root.Text = "设备管理";
            root.Expanded = true;
            nodes.Add(root);

            string[] GroupName = { "人员档案", "人员肝炎指标", "人员疫苗接种", "设备档案", "设备保养纪录", "透析中心信息" };
            Icon[] icon_array = { Icon.User, Icon.User, Icon.User, Icon.Table, Icon.TableGear, Icon.TableGear };
            for (int i = 0; i < GroupName.Length; i++)
            {
                Ext.Net.Node groupNode = new Ext.Net.Node()
                {
                    Text = GroupName[i],
                    NodeID = (i+1).ToString(),
                    Icon = icon_array[i],
                    Cls = "large-font",
                    Leaf = true
                };
                root.Children.Add(groupNode);
            }
            TreePanel1.Render();
        }
        #endregion

        #region 點選TreePanel上的Node
        protected void Node_Click(object sender, DirectEventArgs e)
        {
            string[] ProgName = { "Dialysis_Device_01", "Dialysis_Device_02", "Dialysis_Device_03", "Dialysis_Device_04", "Dialysis_Device_06", "Dialysis_Device_05" };
            string sID = e.ExtraParams["rID"].ToString();
            if (sID != "root")
            {
                int progId = int.Parse(e.ExtraParams["rID"].ToString()) - 1;
                loadAspxFile("./" + ProgName[progId] + ".aspx");
            }
        }
        #endregion    
    
        private void loadAspxFile(String url)
        {
            this.PanelR.Loader.SuspendScripting();
            this.PanelR.Loader.Url = url;
            this.PanelR.Loader.DisableCaching = true;
            this.PanelR.LoadContent();
        }
    }
}