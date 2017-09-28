<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchMenu.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.SchMenu" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>排班选单</title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="ip_url" runat="server" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="PanelT" runat="server" Region="West" Title="病患排班" Header="true" AutoScroll="true" Resizable="false" Collapsible="true">
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server" Header="false" Width="160" RootVisible="false"> 
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
                        <LoadMask ShowMask="true" Msg="读取中" />
                    </Loader> 
                </ext:Panel> 
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
