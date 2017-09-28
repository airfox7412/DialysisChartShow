<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TempPatient_Sch.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.TempPatient_Sch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>临时排班</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        var prepareToolbar1 = function (grid, toolbar, rowIndex, record) {
            if (record.data.Week1 == "") {
                toolbar.items.get(0).hide();
                toolbar.items.get(1).show();
                if (App.sWEEK.value === "1") {
                    toolbar.items.get(1).setDisabled(false);
                }
            }
            else {
                toolbar.items.get(0).show();
                toolbar.items.get(1).hide();
                if (App.sWEEK.value === "1") {
                    toolbar.items.get(0).setDisabled(false);
                }
            }
            toolbar.updateLayout();
        };
        var prepareToolbar2 = function (grid, toolbar, rowIndex, record) {
            if (record.data.Week2 == "") {
                toolbar.items.get(0).hide();
                toolbar.items.get(1).show();
                if (App.sWEEK.value === "2") {
                    toolbar.items.get(1).setDisabled(false);
                }
            }
            else {
                toolbar.items.get(0).show();
                toolbar.items.get(1).hide();
                if (App.sWEEK.value === "2") {
                    toolbar.items.get(0).setDisabled(false);
                }
            }
            toolbar.updateLayout();
        };
        var prepareToolbar3 = function (grid, toolbar, rowIndex, record) {
            if (record.data.Week3 == "") {
                toolbar.items.get(0).hide();
                toolbar.items.get(1).show();
                if (App.sWEEK.value === "3") {
                    toolbar.items.get(1).setDisabled(false);
                }
            }
            else {
                toolbar.items.get(0).show();
                toolbar.items.get(1).hide();
                if (App.sWEEK.value === "1") {
                    toolbar.items.get(0).setDisabled(false);
                }
            }
            toolbar.updateLayout();
        };
        var prepareToolbar4 = function (grid, toolbar, rowIndex, record) {
            if (record.data.Week4 == "") {
                toolbar.items.get(0).hide();
                toolbar.items.get(1).show();
                if (App.sWEEK.value === "4") {
                    toolbar.items.get(1).setDisabled(false);
                }
            }
            else {
                toolbar.items.get(0).show();
                toolbar.items.get(1).hide();
                if (App.sWEEK.value === "1") {
                    toolbar.items.get(0).setDisabled(false);
                }
            }
            toolbar.updateLayout();
        };
        var prepareToolbar5 = function (grid, toolbar, rowIndex, record) {
            if (record.data.Week5 == "") {
                toolbar.items.get(0).hide();
                toolbar.items.get(1).show();
                if (App.sWEEK.value === "5") {
                    toolbar.items.get(1).setDisabled(false);
                }
            }
            else {
                toolbar.items.get(0).show();
                toolbar.items.get(1).hide();
                if (App.sWEEK.value === "5") {
                    toolbar.items.get(0).setDisabled(false);
                }
            }
            toolbar.updateLayout();
        };
        var prepareToolbar6 = function (grid, toolbar, rowIndex, record) {
            if (record.data.Week6 == "") {
                toolbar.items.get(0).hide();
                toolbar.items.get(1).show();
                if (App.sWEEK.value === "6") {
                    toolbar.items.get(1).setDisabled(false);
                }
            }
            else {
                toolbar.items.get(0).show();
                toolbar.items.get(1).hide();
                if (App.sWEEK.value === "6") {
                    toolbar.items.get(0).setDisabled(false);
                }
            }
            toolbar.updateLayout();
        };
        var prepareToolbar7 = function (grid, toolbar, rowIndex, record) {
            if (record.data.Week7 == "") {
                toolbar.items.get(0).hide();
                toolbar.items.get(1).show();
                if (App.sWEEK.value === "7") {
                    toolbar.items.get(1).setDisabled(false);
                }
            }
            else {
                toolbar.items.get(0).show();
                toolbar.items.get(1).hide();
                if (App.sWEEK.value === "7") {
                    toolbar.items.get(0).setDisabled(false);
                }
            }
            toolbar.updateLayout();
        };
    </script>
</head>
<body >
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" Locale="zh-CN" />
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="sTIME" runat="server" />
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="txtWEEK" runat="server" />
        <ext:Hidden ID="sFLOOR" runat="server" />
        <ext:Hidden ID="sAREA" runat="server" />
        <ext:Hidden ID="sBED_NO" runat="server" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel12" runat="server" Region="Center" Title="排班表" Header="false">
                    <Items>                    
                        <ext:GridPanel ID="GridPanel1" runat="server" Cls="x-grid-custom" Border="true" EnableColumnMove="false" EnableColumnResize="false" EnableColumnHide="false" Height="630">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server" UI="Default">
                                    <Items>
                                        <ext:ComboBox ID="cboFLOOR" FieldLabel="楼层" runat="server" LabelWidth="60" LabelAlign="Right" Width="150" Cls="Text-blue" >
                                            <DirectEvents>
                                                <Select OnEvent = "Query_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cboArea" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="150" Cls="Text-blue" >
                                            <DirectEvents>
                                                <Select OnEvent = "Query_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cboTIME" FieldLabel="时段" runat="server" LabelWidth="60" LabelAlign="Right" Width="150" Cls="Text-blue" >
                                            <Items>
                                                <ext:ListItem Value="001" Text="上午" />
                                                <ext:ListItem Value="002" Text="下午" />
                                                <ext:ListItem Value="003" Text="晚班" />
                                            </Items>
                                            <DirectEvents>
                                                <Select OnEvent = "Query_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server" GroupField="Area">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="BedType">
                                            <Fields>
                                                <ext:ModelField Name="Area" />
                                                <ext:ModelField Name="BedType" />
                                                <ext:ModelField Name="Week1" />
                                                <ext:ModelField Name="Week2" />
                                                <ext:ModelField Name="Week3" />
                                                <ext:ModelField Name="Week4" />
                                                <ext:ModelField Name="Week5" />
                                                <ext:ModelField Name="Week6" />
                                                <ext:ModelField Name="Week7" />
                                                <ext:ModelField Name="Machine" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Column1" runat="server" Text="床/机型" Align="Right" Sortable="false" DataIndex="BedType" Width="100" />
                                    <ext:CommandColumn runat="server" Text="周一" Align="Center" Sortable="false" DataIndex="Week1" Width="120">
                                        <Commands>
                                            <ext:GridCommand CommandName="Booking" Icon="Cancel" Text="排床" Disabled="true" />
                                            <ext:GridCommand CommandName="Add" Icon="Add" Text="空床" Disabled="true" />
                                        </Commands>
                                        <PrepareToolbar Fn="prepareToolbar1" />
                                    </ext:CommandColumn>
                                    <ext:CommandColumn runat="server" Text="周二" Align="Center" Sortable="false" DataIndex="Week2" Width="120">
                                        <Commands>
                                            <ext:GridCommand CommandName="Booking" Icon="Cancel" Text="排床" Disabled="true" />
                                            <ext:GridCommand CommandName="Add" Icon="Add" Text="空床" Disabled="true" />
                                        </Commands>
                                        <PrepareToolbar Fn="prepareToolbar2" />
                                    </ext:CommandColumn>
                                    <ext:CommandColumn runat="server" Text="周三" Align="Center" Sortable="false" DataIndex="Week3" Width="120">
                                        <Commands>
                                            <ext:GridCommand CommandName="Booking" Icon="Cancel" Text="排床" Disabled="true" />
                                            <ext:GridCommand CommandName="Add" Icon="Add" Text="空床" Disabled="true" />
                                        </Commands>
                                        <PrepareToolbar Fn="prepareToolbar3" />
                                        <Listeners>
                                            <Command Handler="Ext.Msg.alert(command, record.data.Area + '-'+ record.data.BedType);" />
                                        </Listeners>
                                    </ext:CommandColumn>
                                    <ext:CommandColumn runat="server" Text="周四" Align="Center" Sortable="false" DataIndex="Week4" Width="120">
                                        <Commands>
                                            <ext:GridCommand CommandName="Booking" Icon="Cancel" Text="排床" Disabled="true" />
                                            <ext:GridCommand CommandName="Add" Icon="Add" Text="空床" Disabled="true" />
                                        </Commands>
                                        <PrepareToolbar Fn="prepareToolbar4" />
                                    </ext:CommandColumn>
                                    <ext:CommandColumn runat="server" Text="周五" Align="Center" Sortable="false" DataIndex="Week5" Width="120">
                                        <Commands>
                                            <ext:GridCommand CommandName="Booking" Icon="Cancel" Text="排床" Disabled="true" />
                                            <ext:GridCommand CommandName="Add" Icon="Add" Text="空床" Disabled="true" />
                                        </Commands>
                                        <PrepareToolbar Fn="prepareToolbar5" />
                                    </ext:CommandColumn>
                                    <ext:CommandColumn runat="server" Text="周六" Align="Center" Sortable="false" DataIndex="Week6" Width="120">
                                        <Commands>
                                            <ext:GridCommand CommandName="Booking" Icon="Cancel" Text="排床" Disabled="true" />
                                            <ext:GridCommand CommandName="Add" Icon="Add" Text="空床" Disabled="true" />
                                        </Commands>
                                        <PrepareToolbar Fn="prepareToolbar6" />
                                    </ext:CommandColumn>
                                    <ext:CommandColumn runat="server" Text="周日" Align="Center" Sortable="false" DataIndex="Week7" Width="120">
                                        <Commands>
                                            <ext:GridCommand CommandName="Booking" Icon="Cancel" Text="排床" Disabled="true" />
                                            <ext:GridCommand CommandName="Add" Icon="Add" Text="空床" Disabled="true" />
                                        </Commands>
                                        <PrepareToolbar Fn="prepareToolbar7" />
                                    </ext:CommandColumn>
                                    <ext:Column ID="Column10" runat="server" Text="机器品牌" Align="Left" Sortable="false" DataIndex="Machine" Width="200" Draggable="false"/>
                                </Columns>
                            </ColumnModel>
                            <Features>
                                <ext:Grouping ID="Grouping1" runat="server" HideGroupedHeader="true" GroupHeaderTplString="{name} / {rows.length} 床" />
                            </Features>
                            <Plugins>
                                <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1" />
                            </Plugins>
                            <SelectionModel>
                            <ext:CellSelectionModel ID="CellSelectionModel1" runat="server">
                            </ext:CellSelectionModel>
                        </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>    
        </ext:Viewport>
    </form>
</body>
</html>
