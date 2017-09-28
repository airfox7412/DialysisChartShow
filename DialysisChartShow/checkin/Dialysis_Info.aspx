<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Info.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Dialysis_Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel1" runat="server" Region="West" Width="200" MinWidth="200" bodyStyle="background-color:#dfe8f6" Header="false"
                    MaxWidth="200" Split="true" Collapsible="true" OverflowY="Scroll" Cls="color_title">
                    <Items>
                        <ext:MenuPanel ID="MenuPanel1" runat="server" Title="血液净化" AutoHeight="true" >
                            <Menu ID="Menu1" runat="server">
                                <Items>
                                </Items>
                            </Menu>
                        </ext:MenuPanel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel2" runat="server" Region="Center" Border="false" Layout="fit" ColumnWidth="1" >
                        <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                            <LoadMask ShowMask="true" />
                        </Loader>
                        <Items>
                            <ext:Panel ID="Panel3" runat="server" Layout="fit"   AutoScroll="true">
                                <Items>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Panel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
