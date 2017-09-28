<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Biochemical_Indicators_list.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Biochemical_Indicators_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
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
    <script language="javascript" type="text/javascript">
        var template = 'color:{0};';
        var change = function (value, meta, record, row, col) {
            meta.style = Ext.String.format(template, "blue");
            if (record.get("RITEM_HIGH1") != "")
                if (parseFloat(value) > parseFloat(record.get("RITEM_HIGH1")))
                    meta.style = Ext.String.format(template, "red");
            if (record.get("RITEM_LOW1") != "")
                if (parseFloat(value) < parseFloat(record.get("RITEM_LOW1")))
                    meta.style = Ext.String.format(template, "green");
            return value + "  ";
        };

        var ldrug_actst = function (value) {
            return Ext.String.format(template, (value == "使用") ? "green" : "red", value);
        };

        var sdrug_actst = function (value) {
            return Ext.String.format(template, (value == "使用") ? "green" : "red", value);
        };
        
    </script>
</head>
<body>
    <form id="formBI" width = "500" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Panel ID="PanelR" runat="server" Border="false" AutoScroll="true" Cls="Panellogo">
            <Items>
                <ext:GridPanel ID="Grid_Lab" runat="server" Cls="x-grid-custom">
                    <Store>
                        <ext:Store ID="Store" runat="server">
                            <Model>
                                <ext:Model ID="Model" runat="server" Name="LongDrugUse">
                                    <Fields>
                                        <ext:ModelField Name="lno" Type="String" />
                                        <ext:ModelField Name="RESULT_CODE" Type="String" />
                                        <ext:ModelField Name="RITEM_NAME" Type="String" />
                                        <ext:ModelField Name="RESULT_VALUE" Type="String" />
                                        <ext:ModelField Name="RITEM_LOW1" Type="String" />
                                        <ext:ModelField Name="RITEM_HIGH1" Type="String" />
                                        <ext:ModelField Name="RESULT_DATE" Type="String" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Reader>
                                <ext:ArrayReader />
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:Column ID="Column0" runat="server" DataIndex="lno" Text="" Width="40" />
                            <ext:Column ID="Column6" runat="server" DataIndex="RESULT_DATE" Text="检验日期" Width="100" />
                            <ext:Column ID="Column01" runat="server" DataIndex="RESULT_CODE" Text="检验代码" Width="100" />
                            <ext:Column ID="Column02" runat="server" DataIndex="RITEM_NAME" Text="检验简称" Width="150" />
                            <ext:Column ID="Column03" Header="检验结果" runat="server" DataIndex="RESULT_VALUE" Width="100">
                                <Renderer Fn="change" />
                            </ext:Column>
                            <ext:Column ID="Column04" runat="server" DataIndex="RITEM_LOW1" Text="检验低值" Width="100" />
                            <ext:Column ID="Column05" runat="server" DataIndex="RITEM_HIGH1" Text="检验高值" Width="100" />
                            <ext:Column ID="Column7" runat="server" Text="趋势图" RightCommandAlign="false" Flex="1">
                                <Commands>
                                    <ext:ImageCommand Icon="ChartLine" CommandName="BiochemicalIndicators" Text="趋势图">
                                        <ToolTip Text="检验趋势图" />
                                    </ext:ImageCommand>
                                </Commands>
                                <DirectEvents>
                                    <Command OnEvent="On_ShowChart">
                                        <ExtraParams>
                                            <ext:Parameter Name="RESULT_CODE" Value="record.data.RESULT_CODE" Mode="Raw" />
                                            <ext:Parameter Name="RITEM_NAME" Value="record.data.RITEM_NAME" Mode="Raw" />
                                            <ext:Parameter Name="RITEM_LOW1" Value="record.data.RITEM_LOW1" Mode="Raw" />
                                            <ext:Parameter Name="RITEM_HIGH1" Value="record.data.RITEM_HIGH1" Mode="Raw" />
                                        </ExtraParams>
                                    </Command>
                                </DirectEvents>
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Window ID="Window1" runat="server" Width="700" Height="500" Hidden="true">
            <Loader ID="WinLoader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
    </form>
</body>
</html>
