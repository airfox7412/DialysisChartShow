<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_05.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_05" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病程记录</title>
    <script type="text/javascript">
        var showMenu = function (view, node, item, index, e) {
          
            var s = node.get("text");

            if (s == "病程记录" || s == "无" || s == "添加") {
                e.stopEvent();
            }
            else if (s == "常规记录" || s == "特殊记录") {
                var menu = App.MenuA;
                this.menuNode = node;
                menu.nodeName = s; 
                view.getSelectionModel().select(node);
                menu.showAt([e.getXY()[0], e.getXY()[1] + 10]);
                e.stopEvent();
            }
            else {
                var menu2 = App.MenuE;
                this.menuNode = node;
                menu2.nodeName = s;
                view.getSelectionModel().select(node);
                menu2.showAt([e.getXY()[0], e.getXY()[1] + 10]);
                e.stopEvent();
            }
        };

    </script>
    <style type="text/javascript">
    .bold-text{
        font-weight:bold;
        padding-left: 32px;
        margin-bottom:2px;
        font-size:110%;
    }
        
    x-status-text 
    {
        font-size:18px !important;
        color: red !important;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Menu ID="MenuA" runat="server">
                    <Items>
                        <ext:Label ID="NodeNameA" runat="server" Cls="bold-text" />
                        <ext:MenuSeparator />
                        <ext:MenuItem ID="MenuItemA" runat="server" Text="添加" Icon="Add">
                            <%--<Listeners>
                                <Click Handler="#{TreePanel1}.appendChild(#{TreePanel1}.menuNode, 'New');" />
                            </Listeners>--%>
                        </ext:MenuItem>
                    </Items>
                    <Listeners>
                        <Show Handler="#{NodeNameA}.setText(this.nodeName);" />
                    </Listeners>
                </ext:Menu>
        
                <ext:Menu ID="MenuE" runat="server">
                    <Items>
                        <ext:Label ID="NodeNameE" runat="server" Cls="bold-text" />
                        <ext:MenuSeparator />
                        <ext:MenuItem ID="MenuItemE" runat="server" Text="修改" Icon="Pencil">
                            <%--<Listeners>
                                <Click Handler="#{TreePanel1}.editingPlugin.startEdit(#{TreePanel1}.menuNode, 0);" />
                            </Listeners>--%>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItemP" runat="server" Text="打印" Icon="Printer">
                            <%--<Listeners>
                                <Click Handler="#{TreePanel1}.editNode(#{TreePanel1}.menuNode, 'text', 'TEST');" />
                            </Listeners>--%>
                        </ext:MenuItem>
                    </Items>
                    <Listeners>
                        <Show Handler="#{NodeNameE}.setText(this.nodeName);" />
                    </Listeners>
                </ext:Menu>

                <ext:Panel ID="Panel2" runat="server" Title="病程记录" Region="West" Width="200" MinWidth="200" bodyStyle="background-color:#dfe8f6" 
                    MaxWidth="200" Collapsible="false" Cls="color_title">
                    <Items>
                        <ext:TextField ID="txtNODE_ID" runat="server" Width="50" />
                        <ext:TextField ID="txtNODE_TEXT" runat="server" Width="50" />
                        <ext:TreePanel ID="TreePanel1" runat="server" OnReadData="NodeLoad" RootVisible="true" RowLines="true" >
                            <DirectEvents>
                                <ItemClick OnEvent="Node_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                        <ext:Parameter Name="rTEXT" Value="record.data.text" Mode="Raw" />
                                    </ExtraParams>
                                </ItemClick>
                            </DirectEvents>

                            <%--<Listeners>
                                <ItemContextMenu Fn="showMenu" StopEvent="true" />
                                <RemoteActionRefusal Handler="Ext.Msg.alert('Action refusal', e.message);" />
                            </Listeners>--%>

                        </ext:TreePanel> 

                        <ext:MenuPanel ID="MenuPanel15" runat="server" AutoHeight="true" >
                            <Menu ID="Menu13" runat="server">
                                <Items>
                                    <ext:MenuItem ID="MenuItem1" runat="server" Text="病程记录" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page1"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem2" runat="server" Text="血管通路记录" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page2"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem3" runat="server" Text="查房记录" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page3"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                </Items>
                            </Menu>
                        </ext:MenuPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" Border="false" Layout="FitLayout" ColumnWidth="1" >
                        <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                            <LoadMask ShowMask="true" />
                        </Loader>
                    </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
