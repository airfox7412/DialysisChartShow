<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>紀錄表</title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Width="200" Collapsible="false" Cls="color_title" Header="false">
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server"  Title="血液透析" RootVisible="false" AutoHeight="true"> 
                            <DirectEvents>
                                <ItemClick OnEvent="Node1_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                    </ExtraParams>
                                </ItemClick>
                            </DirectEvents>
                        </ext:TreePanel>
                        <ext:TreePanel ID="TreePanel2" runat="server"  Title="腹膜透析" RootVisible="false" AutoHeight="true"> 
                            <DirectEvents>
                                <ItemClick OnEvent="Node2_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                    </ExtraParams>
                                </ItemClick>
                            </DirectEvents>
                        </ext:TreePanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel2" runat="server" Region="Center" Border="false" Layout="FitLayout" ColumnWidth="1" >
                        <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                            <LoadMask ShowMask="true" Msg="读取中" />
                        </Loader>
                    </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
