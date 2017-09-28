<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_ModPreSet.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Dialysis_ModPreSet" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>处方模版</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <style type="text/css">
    .Panellogo .x-autocontainer-innerCt
    {
        /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#1e5799+0,2989d8+100,207cca+100,7db9e8+100 */
        background: #1e5799; /* Old browsers */
        background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
        background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
        background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0 ); /* IE6-9 */ 
    }
    .x-form-text-default 
    {
        font-weight:normal;
    }
    .x-form-item-label-default
    {
        color:White;
    }
    .x-form-cb-label-default
    {
        color:White;
    }
    .x-form-radio-default
    {
        color:Yellow;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel_Left" runat="server" Title="处方模版" Region="Center" Header="false" AutoScroll="true" Cls="Panellogo">
                    <Items>
                        <ext:Container ID="Container10" runat="server">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:Panel ID="PanelT" runat="server" Region="West" AutoScroll="true" Resizable="false" Border="true">
                                    <Items>
                                        <ext:TreePanel ID="TreePanel1" runat="server" Header="false" RootVisible="false" Width="160" Height="650"> 
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
                                <ext:Panel ID="PanelR" runat="server" Region="Center" Layout="FitLayout" Width="1050" Height="650">
                                    <Loader ID="Loader2" runat="server" Mode="Frame" ManuallyTriggered="true" AutoLoad="true" Url="">
                                        <LoadMask ShowMask="true" Msg="读取中" />
                                    </Loader> 
                                </ext:Panel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
