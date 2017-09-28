<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_0h" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>血透評估表</title> 
    <style type = "text/css">
    .myTreePanel 
    {
        border: none;
        padding: 0 10px;
    }
    .myTitle
    {
         font-weight:bold;  
         color:Black;
    }        
    .x-status-text 
    {
        font-size:18px !important;
        color: red !important;
    }        
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager3" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="border" >
            <Items>
                <ext:TextField ID="txtNODE_ID" runat="server" />
                <ext:TextField ID="txtNODE_TEXT" runat="server" />
                        
                <ext:Panel ID="Panel2" runat="server" Title ="血透评估表" Region="West" Width="250" MinWidth="250" bodyStyle="background-color:#dfe8f6"
                    MaxWidth="250" Split="true" Collapsible="true" Cls=".myTitle">
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server" Cls=".myTreePanel" OnReadData="NodeLoad" Hidden="true" >
                            <DirectEvents>
                                <ItemClick OnEvent="Node_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                        <ext:Parameter Name="rTEXT" Value="record.data.text" Mode="Raw" />
                                    </ExtraParams>
                                </ItemClick>
                            </DirectEvents>
                        </ext:TreePanel> 

                    
                        <ext:MenuPanel ID="MenuPanel15" runat="server" AutoHeight="true" >
                            <Menu ID="Menu13" runat="server">
                                <Items>

                                    <ext:MenuItem ID="MenuItem1" runat="server" Text="1首次血液透析护理评估措施记录单" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page1"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem2" runat="server" Text="2血管通路动静脉内瘘物理检查评估表" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page2"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem3" runat="server" Text="3动静脉内瘘闭塞高危因素评估表" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page3"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem4" runat="server" Text="4血液透析患者皮肤瘙痒评估表(Sergio)" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page4"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem5" runat="server" Text="5疼痛评分表" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page5"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem6" runat="server" Text="6住院病人预防跌倒护理评估表" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page6"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <ext:MenuItem ID="MenuItem7" runat="server" Text="7预防跌倒护理措施评估表" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page7"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>
                                    <%--<ext:MenuItem ID="MenuItem8" runat="server" Text="8血液透析患者评估表" Icon="Star" Cls="color_10">
                                        <DirectEvents>
                                            <Click OnEvent = "reload_page8"></Click>
                                        </DirectEvents>
                                    </ext:MenuItem>--%>
                                </Items>
                            </Menu>
                        </ext:MenuPanel>
                        
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel1" runat="server" Border="false" Layout="fit" Region="Center" ColumnWidth="1" >
                    <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
