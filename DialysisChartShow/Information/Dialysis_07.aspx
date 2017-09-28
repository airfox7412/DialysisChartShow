<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_07.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_07" %>

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
                                    
                                    <ext:MenuItem ID="MenuItem1" runat="server" Text="病历上传" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page2"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem2" runat="server" Text="病历显示" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page3"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                </Items>
                            </Menu>
                        </ext:MenuPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" Border="false" Layout="fit" 
                        ColumnWidth="1" >
                        <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                            <LoadMask ShowMask="true" />
                        </Loader>
                        <Items>
                            <ext:Panel ID="Panel3" runat="server" Layout="fit"   AutoScroll="true">
                                            <Items>
                                            </Items>
                                        </ext:Panel>
                            <%--<ext:Container ID="Container3" runat="server" Layout="fit">
                                <Items>
                                </Items>
                            </ext:Container>--%>
                        </Items>
                    </ext:Panel>
                <%-- <ext:TabPanel ID="TabPanel1" runat="server" Region="Center" Layout="Fit" Split="true">
                   <Items>
                   <ext:Panel ID="Tab1" Title="个人首页" runat="server" >
                       <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                           <LoadMask ShowMask="true" />
                       </Loader>
                   </ext:Panel>
                   </Items>
                </ext:TabPanel>--%>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
