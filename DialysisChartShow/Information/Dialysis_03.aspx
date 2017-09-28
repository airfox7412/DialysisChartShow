<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_03.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_03" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="Panel2" runat="server"  Region="West" Width="200" MinWidth="200" bodyStyle="background-color:#dfe8f6"
                    MaxWidth="200" Split="true" Collapsible="true" OverflowY="Scroll" Cls="color_title">
                    <Items>
                        <ext:MenuPanel ID="MenuPanel15" runat="server" AutoHeight="true" >
                            <Menu ID="Menu13" runat="server">
                                <Items>
                                    <ext:MenuItem ID="MenuItem48" runat="server" Text="促红素" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page1" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem51" runat="server" Text="铁剂" HrefTarget="Rhs_02.aspx"
                                        Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page2" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem4" runat="server" Text="抗高血压药" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page3" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem56" runat="server" Text="活性维生素D" HrefTarget="Rhs_03.aspx"
                                        Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page4" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem1" runat="server" Text="钙剂" HrefTarget="Rhs_04.aspx"
                                        Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page5" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem2" runat="server" Text="降磷药物" HrefTarget="Rhs_05.aspx"
                                        Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page6" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem3" runat="server" Text="其它药物治疗" HrefTarget="Rhs_05.aspx"
                                        Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page7" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                </Items>
                            </Menu>
                        </ext:MenuPanel>
                        
                        
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" Border="false" Layout="fit" Height="800" 
                        ColumnWidth="1" >
                        <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                            <LoadMask ShowMask="true" />
                        </Loader>
                        <Items>
                            <ext:Container ID="Container3" runat="server" Layout="fit">
                                <Items>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
