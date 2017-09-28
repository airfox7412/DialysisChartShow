﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Stock.aspx.cs" Inherits="Dialysis_Chart_Show.Stock.Dialysis_Stock" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>庫存管理</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="PanelT" runat="server" Region="West" AutoScroll="true" Resizable="false" Border="true">
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server" Header="false" Width="200" RootVisible="false"> 
                            <DirectEvents>
                                <ItemClick OnEvent="Node_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                    </ExtraParams>
                                </ItemClick>
                            </DirectEvents>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="PanelR" runat="server" Region="Center" Layout="FitLayout" AnchorHorizontal="100%" AnchorVertical="100%">
                    <Loader ID="Loader1" runat="server" Mode="Frame" ManuallyTriggered="true" AutoLoad="true" Url="">
                        <LoadMask ShowMask="true" />
                    </Loader> 
                </ext:Panel> 
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>