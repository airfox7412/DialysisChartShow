<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchTable.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.SchTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>本周排班</title>
    <link href="../css/grid.css" rel="stylesheet"/>

    <script type="text/javascript">
        var change = function (value, metadata, record, row, col) {
            if (record.get("Kind") == "Y") {
                metadata.style = "color:red;";
            }
            else {
                metadata.style = "color:green;";
            }
            return value;
        };

        var myRenderer1 = function (value, metadata) {
            if (App.sWEEK.value === "1") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer2 = function (value, metadata) {
            if (App.sWEEK.value === "2") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer3 = function (value, metadata) {
            if (App.sWEEK.value === "3") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer4 = function (value, metadata) {
            if (App.sWEEK.value === "4") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer5 = function (value, metadata) {
            if (App.sWEEK.value === "5") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };

        var myRenderer6 = function (value, metadata) {
            if (App.sWEEK.value === "6") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer7 = function (value, metadata) {
            if (App.sWEEK.value === "7") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };

        function ChgStr(value) {
            var PatName = value.replace(/ST/, " (s)");
            return PatName;
        }

        var onContextMenu = function (grid, rowIndex, e, menu) {
            menu.record = grid.getStore().getAt(rowIndex);
            menu.showAt(e.getXY());
        };

        var onClick = function (grid) {
            Ext.fly(grid.view.getCell(
                grid.currentSelection.row,
                grid.currentSelection.column)).removeClass("selected-cell");

            Ext.net.DirectMethods.Filter(
                grid.currentSelection.fieldName,
                grid.currentSelection.data);
        };

        function deleteuser() {
            var record = ItemGrid.getSelectionModel().getSelected();
            alert("删除：" + record.data.Week1.toString());
            //Ext.net.DirectMethods.Deletestudent(record.data.stuid.toString());
        }     
    </script>
    <script src="<%=ResolveClientUrl("~/Scripts/checkin/SchTable.js") %>" language="javascript" type="text/javascript"></script>
</head>
<body >
    <form id="form1" runat="server">
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="sTIME" runat="server" />
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="txtWEEK" runat="server" />
        <ext:Hidden ID="sFLOOR" runat="server" />
        <ext:Hidden ID="sAREA" runat="server" />
        <ext:Hidden ID="sBED_NO" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" Locale="zh-CN" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel12" runat="server" Region="Center" Title="排班表" Header="true" AutoScroll="true">
                    <Items>                    
                        <ext:GridPanel ID="GridPanel1" runat="server" EnableColumnMove="false" EnableColumnResize="false" EnableColumnHide="false" Border="true" 
                            ColumnLines="true" Cls="x-grid-custom" Width="1200" Height="600" ContextMenuID="RowContextMenu">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server" UI="Default">
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
                                        <ext:Button ID="btnSave" runat="server" Text="换床保存" Icon="Accept" Width="100" UI="Info">
                                            <DirectEvents>
                                                <Click OnEvent="cmdSAVE" Before="return #{Store1}.isDirty();">
                                                    <EventMask ShowMask="true" />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="data" Value="#{Store1}.getChangedData()" Mode="Raw" Encode="true" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnReset" runat="server" Text="重置" Icon="Cancel" Width="100" UI="Info">
                                            <DirectEvents>
                                                <Click OnEvent="btnReset_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="Button1" runat="server" Text="排班维护" Icon="ClockEdit" Width="100" UI="Info">
                                            <DirectEvents>
                                                <Click OnEvent="btnEdit_Click">
                                                    <EventMask ShowMask="true" Msg="载入中,请稍待..." MinDelay="300" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnPrint" runat="server" Icon="PrinterColor" IconAlign="Left" Text="打印" Width="100" UI="Success">                                                                        
                                            <DirectEvents>
                                                <Click OnEvent="OnbtnPrint_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server" GroupField="Area">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="BedType">
                                            <Fields>
                                                <ext:ModelField Name="Floor" />
                                                <ext:ModelField Name="Area" />
                                                <ext:ModelField Name="BedType" />
                                                <ext:ModelField Name="BedNo" />
                                                <ext:ModelField Name="MachineType" />
                                                <ext:ModelField Name="Week1" Type="Object" />
                                                <ext:ModelField Name="Week2" Type="Object" />
                                                <ext:ModelField Name="Week3" Type="Object" />
                                                <ext:ModelField Name="Week4" Type="Object" />
                                                <ext:ModelField Name="Week5" Type="Object" />
                                                <ext:ModelField Name="Week6" Type="Object" />
                                                <ext:ModelField Name="Week7" Type="Object" />
                                                <ext:ModelField Name="Machine" />
                                                <ext:ModelField Name="TimeType" />
                                                <ext:ModelField Name="Kind" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Column1" runat="server" Text="床/机型" Align="Right" Sortable="false" DataIndex="BedType" Width="100" Locked="true">
                                        <Renderer Fn="change" />
                                    </ext:Column>
                                    <ext:Column ID="Column3" runat="server" Text="周一" Align="Center" Sortable="false" DataIndex="Week1" Width="130">
                                        <Renderer Fn="myRenderer1" />
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" Text="周二" Align="Center" Sortable="false" DataIndex="Week2" Width="130">
                                        <Renderer Fn="myRenderer2" />
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" Text="周三" Align="Center" Sortable="false" DataIndex="Week3" Width="130">
                                        <Renderer Fn="myRenderer3" />
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" Text="周四" Align="Center" Sortable="false" DataIndex="Week4" Width="130">
                                        <Renderer Fn="myRenderer4" />
                                    </ext:Column>
                                    <ext:Column ID="Column7" runat="server" Text="周五" Align="Center" Sortable="false" DataIndex="Week5" Width="130">
                                        <Renderer Fn="myRenderer5" />
                                    </ext:Column>
                                    <ext:Column ID="Column8" runat="server" Text="周六" Align="Center" Sortable="false" DataIndex="Week6" Width="130">
                                        <Renderer Fn="myRenderer6" />
                                    </ext:Column>
                                    <ext:Column ID="Column9" runat="server" Text="周日" Align="Center" Sortable="false" DataIndex="Week7" Width="130">
                                        <Renderer Fn="myRenderer7" />
                                    </ext:Column>
                                    <ext:Column ID="Column10" runat="server" Text="机器品牌" Align="Left" Sortable="false" DataIndex="Machine" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Features>
                                <ext:Grouping ID="Grouping1" runat="server" HideGroupedHeader="true" GroupHeaderTplString="{name}区 / {rows.length} 床" />
                            </Features>
                            <View>
                                <ext:GridView ID="GridView1" runat="server">
                                    <Plugins>
                                        <ext:CellDragDrop ID="CellDragDrop1" runat="server" EnforceType="true" DropBackgroundColor="Green" ApplyEmptyText="false" />
                                    </Plugins>
                                </ext:GridView>
                            </View>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true" />
                            </SelectionModel>
                            <Listeners>
                                <RowContextMenu Handler="onContextMenu(this, rowIndex, e, RowContextMenu);" />
                                <ItemContextMenu Handler="e.preventDefault();" />
                                <ViewReady Handler = "handlerSchTable.onOverride();" />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <%--視窗部分--%>
                <ext:Window ID="PrintWindow" runat="server" Title="" Width="900" Height="700" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader6" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
                <%--視窗部分--%>
                <ext:Menu ID="RowContextMenu" runat="server">
                    <Items>
                        <ext:MenuItem ID="MenuItem1" runat="server" Text="排班" Icon="Add">
                            <Listeners>
                                <Click Handler="onClick(GridPanel1);" />
                            </Listeners>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem2" runat="server" Text="移除" Icon="Cancel">
                            <Listeners>
                                <Click Fn="deleteuser" />
                            </Listeners>
                        </ext:MenuItem>
                    </Items>
                </ext:Menu>
            </Items>    
        </ext:Viewport>
    </form>
</body>
</html>
