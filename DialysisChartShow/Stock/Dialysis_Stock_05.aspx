<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Stock_05.aspx.cs" Inherits="Dialysis_Chart_Show.Stock.Dialysis_Stock_05" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>耗材使用统计</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="耗材使用统计" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Padding="3" SortableColumns="false" Resizable="false" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:DateField ID="DateField1" runat="server" Text="日期" Format="yyyy-MM-dd" ColumnWidth=".1" />
                                        <ext:DateField ID="DateField2" runat="server" Text="~" Format="yyyy-MM-dd" ColumnWidth=".1" />
                                        <ext:Button ID="Search" runat="server" Text="查寻" Icon="Zoom" Width="70" Margins="10 5 10 10">
                                            <DirectEvents>
                                                <Click OnEvent="cmdQuery">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15" > 
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="NO" />
                                                <ext:ModelField Name="dyivl_id" />
                                                <ext:ModelField Name="dyivl_serialno" />
                                                <ext:ModelField Name="dyivl_no" />
                                                <ext:ModelField Name="dyivl_item" />
                                                <ext:ModelField Name="dyivl_qty" />
                                                <ext:ModelField Name="dyivl_rec" />
                                                <ext:ModelField Name="dyivl_ivdate" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="invs_id" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Text="序" Width="70" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="dyivl_item" Header="耗材名称" Width="250" />
                                    <ext:Column ID="Column3" runat="server" DataIndex="dyivl_qty" Header="使用数量" Width="100" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server"                      
                                    DisplayInfo="true"
                                    DisplayMsg="显示 分类明细 {0} - {1} of {2}"
                                    EmptyMsg="没有 分类明细 可显示"                
                                    />
                            </BottomBar>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel1" runat="server">
                                    <Listeners>
                                        <Select Handler="#{btnSave}.enable();#{btnCancel}.enable();" />
                                    </Listeners>
                                </ext:CellSelectionModel>
                            </SelectionModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1" />
                            </Plugins>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel> 
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>