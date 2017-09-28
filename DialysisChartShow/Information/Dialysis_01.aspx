<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>诊断信息</title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="Panel2" runat="server" Title="诊断信息" Region="West" Width="200" MinWidth="200" bodyStyle="background-color:#dfe8f6"
                    MaxWidth="200" Split="true" Collapsible="False" Cls="color_title">
                    <Items>
                        <ext:MenuPanel ID="MenuPanel15" runat="server" AutoHeight="true" >
                            <Menu ID="Menu13" runat="server">
                                <Items>
                                    <ext:MenuItem ID="MenuItem48" runat="server" Text="原发病诊断" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page1" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem51" runat="server" Text="病理诊断"  Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page2" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem4" runat="server" Text="并发症诊断" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page3" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem56" runat="server" Text="传染病诊断" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page4" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem1" runat="server" Text="肿瘤诊断" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page5" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem2" runat="server" Text="过敏诊断" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page6" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem3" runat="server" Text="转归情况" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page7" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem5" runat="server" Text="新诊断信息" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page8" />
                                        </DirectEvents>
                                    </ext:MenuItem>
                                </Items>
                            </Menu>
                        </ext:MenuPanel>
                        <%--<ext:MenuPanel ID="MenuPanel2" runat="server" AutoHeight="true" >
                            <Menu ID="Menu14" runat="server">
                                <Items>
                                    <ext:MenuItem ID="MenuItem6" runat="server" Text="新诊断信息" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page8"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                </Items>
                            </Menu>
                        </ext:MenuPanel>--%>
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
