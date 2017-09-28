<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_13_new.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_13_new" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
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
    <script type="text/javascript">
          var saveData18 = function () {
            App.Hidden18.setValue(Ext.encode(App.GridPanel18.getRowsValues({ selectedOnly: false })));
          };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="Hidden18" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout" >
            <Items>
                <ext:Panel ID="PanelR" runat="server" Border="false" AutoScroll="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel18" runat="server" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar18" runat="server">
                                    <Items>
                                        <ext:Label ID="Lab_patid" runat="server" ColumnWidth=".28" Width="100" />
                                        <ext:Label ID="Label3" runat="server" ColumnWidth=".1" Width="100" />
                                        <ext:Label ID="Lab_amount" runat="server" ColumnWidth=".28" Region="West" Width="100" />
                                        <ext:ToolbarFill ID="ToolbarFill18" runat="server" />
                                        <ext:Button ID="Button18b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_18" Icon="PageExcel">
                                            <Listeners>
                                                <Click Fn="saveData18" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="Button18c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_18" Icon="PageAttach">
                                            <Listeners>
                                                <Click Fn="saveData18" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store18" runat="server" >
                                    <Model>
                                        <ext:Model ID="Model18" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="编号" Type="Int" />
                                                <ext:ModelField Name="身份证号" />
                                                <ext:ModelField Name="使用人" />
                                                <ext:ModelField Name="血透次数" />
                                                <ext:ModelField Name="耗材名称" />
                                                <ext:ModelField Name="使用数量" />
                                                <ext:ModelField Name="金额小计" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel18" runat="server"  >
                                <Columns>
                                    <ext:Column ID="Column132" Header="编号" runat="server" DataIndex="编号" Width="70" />
                                    <ext:Column ID="Column133" Header="藥品耗材名称" runat="server" DataIndex="耗材名称" Width="200" />
                                    <ext:Column ID="Column134" Header="血透次数" runat="server" DataIndex="血透次数" Width="100" />
                                    <ext:Column ID="Column135" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Visible="False" />
                                    <ext:Column ID="Column137" Header="使用人" runat="server" DataIndex="使用人" Width="70" Visible="False" />
                                    <ext:Column ID="Column136" Header="使用数量" runat="server" DataIndex="使用数量" Width="110" Align="Right" />
                                    <ext:Column ID="Column156" Header="金额小计" runat="server" DataIndex="金额小计" Width="110" Align="Right" Hidden="True" />
                                    <ext:Column ID="Column1" runat="server" Flex="1" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
