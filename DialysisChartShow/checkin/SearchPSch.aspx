<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SearchPSch.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.SearchPSch" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病患排班</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        var myRenderer1 = function (value, metadata) {
            if (App.sWEEK.value === "1") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
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
                return value.Name;
            } else {
                return "";
            }
        };
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
                <ext:Panel ID="Panel12" runat="server" Region="Center" Title="排班表" Header="true" AutoScroll="true">
                    <Items>                    
                        <ext:GridPanel ID="GridPanel1" runat="server" EnableColumnMove="false" EnableColumnResize="false" EnableColumnHide="false" Border="true" 
                            ColumnLines="true" Cls="x-grid-custom" Width="1200" Height="600">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server" UI="Default">
                                    <Items>
                                        <ext:ComboBox ID="Text_Name" runat="server" FieldLabel="姓名" LabelWidth="50" Width="150" Cls="Text-blue" LabelAlign="Right"
                                            DisplayField="patname" ValueField="patname" TypeAhead="false" HideTrigger="true" MinChars="1" TriggerAction="Query">
                                            <ListConfig LoadingText="寻找中...">
                                                <ItemTpl ID="ItemTpl11" runat="server">
                                                    <Html>
                                                        <div>{patname}</div>
                                                    </html>
                                                </ItemTpl>                                       
                                            </ListConfig>
                                            <Store>
                                                <ext:Store ID="Store12" runat="server" AutoLoad="false">
                                                    <Proxy>
                                                        <ext:AjaxProxy Url="~/Patinfos.ashx">
                                                            <ActionMethods Read="POST" />
                                                            <Reader>
                                                                <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                            </Reader>
                                                        </ext:AjaxProxy>
                                                    </Proxy>
                                                    <Model>
                                                        <ext:Model ID="Model12" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="patic" />
                                                                <ext:ModelField Name="patname" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <DirectEvents>
                                                <Change OnEvent="QueryIC" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:TextField ID="Text_IC" runat="server" FieldLabel="身份证号" LabelWidth="70" LabelAlign="Right" Width="250" /> 
                                        <ext:Button ID="Button1" runat="server" Text="寻找" Icon="Eye" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="btnQuery_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnReset" runat="server" Text="重置" Icon="Cancel" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="btnReset_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnInsert" runat="server" Text="排班" Icon="CalendarSelectDay" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="btnInsert_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="BedType">
                                            <Fields>
                                                <ext:ModelField Name="pat_ic" />
                                                <ext:ModelField Name="pat_name" />
                                                <ext:ModelField Name="floor" />
                                                <ext:ModelField Name="area" />
                                                <ext:ModelField Name="bedno" />
                                                <ext:ModelField Name="ddate" />
                                                <ext:ModelField Name="daytype" />
                                                <ext:ModelField Name="timetype" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="序号" Width="70"  />
                                    <ext:Column ID="Column6" runat="server" Text="排班日期" Align="Center" Sortable="false" DataIndex="ddate" Width="100" />
                                    <ext:Column ID="Column7" runat="server" Text="星期" Align="Center" Sortable="false" DataIndex="daytype" Width="100" />
                                    <ext:Column ID="Column8" runat="server" Text="时段" Align="Center" Sortable="false" DataIndex="timetype" Width="100" />
                                    <ext:Column ID="Column3" runat="server" Text="楼层" Align="Center" Sortable="false" DataIndex="floor" Width="80" />
                                    <ext:Column ID="Column4" runat="server" Text="区" Align="Center" Sortable="false" DataIndex="area" Width="80" />
                                    <ext:Column ID="Column5" runat="server" Text="床号" Align="Center" Sortable="false" DataIndex="bedno" Width="80" />
                                    <%--<ext:Column ID="Column1" runat="server" Text="身分证号" Align="Left" Sortable="false" DataIndex="pat_ic" Width="180" />--%>
                                    <ext:Column ID="Column2" runat="server" Text="姓名" Align="Center" Sortable="false" DataIndex="pat_name" Width="100" />
                                    <ext:Column ID="Column9" runat="server" Text="" Align="Center" Sortable="false" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" SingleSelect="true" ID="RowSelectionModel1" />
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Window ID="Window1" runat="server" Title="临时排班" Width="430" Height="600" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
            </Items>    
        </ext:Viewport>
    </form>
</body>
</html>
