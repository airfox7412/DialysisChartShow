<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_PreSetView.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Dialysis_PreSetView" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>处方模板</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:Hidden ID="Patient_ID" runat="server" />
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="CrispTouch" />        
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel id="Panel_Loader1" runat="server" Region="North" AutoScroll="true" Cls="Panellogo">
                <Items>
                    <ext:Container ID="Container1" runat="server">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                        </LayoutConfig>
                        <Items> 
                            <ext:TabPanel ID="TabPanel1" runat="server" Width="1200" Height="770" UI="Primary">
                                <Items>                                    
                                    <%--长期医嘱用药--%>                            
                                    <ext:Panel ID="Panel_Long" runat="server" Title="长期医嘱" Icon="MedalGold1">
                                        <TabConfig runat="server" UI="Success" />
                                        <LayoutConfig>
                                            <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                                        </LayoutConfig>
                                        <DirectEvents>
                                            <Activate OnEvent="tab1_activate" />
                                        </DirectEvents>
                                        <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                                            <LoadMask ShowMask="true" Msg="读取中..." />
                                        </Loader>
                                    </ext:Panel> 
                                    <%--短期医嘱用药--%>                                  
                                    <ext:Panel ID="Panel_Short" runat="server" Title="短期医嘱" Icon="MedalBronze1">
                                        <TabConfig runat="server" UI="Success" />
                                        <LayoutConfig>
                                            <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                                        </LayoutConfig>
                                        <DirectEvents>
                                            <Activate OnEvent="tab2_activate" />
                                        </DirectEvents>
                                        <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                                            <LoadMask ShowMask="true" Msg="读取中..."  />
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
