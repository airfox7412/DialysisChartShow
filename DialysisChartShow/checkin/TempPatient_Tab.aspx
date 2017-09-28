﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TempPatient_Tab.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.TempPatient_Tab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>临时病患</title>
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
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel_North" runat="server" Title="临时病患" Region="Center" Header="false" Cls="Panellogo" AutoScroll="true">
                    <Items>
                        <ext:Container ID="Container13" runat="server">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:TabPanel ID="TabPanel1" runat="server" Width="1200" UI="Success">
                                    <Items>
                                        <ext:Panel ID="Panel1" runat="server" Title="基本资料建档" Height="700">
                                            <DirectEvents>
                                                <Activate OnEvent="tab1_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader1" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel2" runat="server" Title="临时排班" Height="700">
                                            <DirectEvents>
                                                <Activate OnEvent="tab2_activate" Single="false" />
                                            </DirectEvents>
                                            <Loader ID="Loader2" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                    </Items>
                                </ext:TabPanel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
