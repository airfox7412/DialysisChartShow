<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_04_Xian.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_04_Xian" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>拟用药使用统计表</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        var saveData1 = function () {
            App.export.setValue(Ext.encode(App.GridPanelList.getRowsValues({ selectedOnly: false })));
        };
    </script>
</head>
<body>
    <form id="formList" runat="server">
        <ext:Hidden ID="export" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="拟用药使用统计表" AutoScroll="true" ButtonAlign="Center" Frame="true" Padding="10">
                    <Items>
                        <ext:Panel ID="Panel1" runat="server" Border="false">
                            <Items>
                                <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:DateField ID="txtDATE" runat="server" FieldLabel="日期" LabelWidth="60" Format="yyyy-MM-dd" LabelAlign="Right" Width="200"  PaddingSpec="5 2 2 2" />
                                        <ext:TextField ID="txtName" runat="server" FieldLabel="姓名" LabelWidth="60" LabelAlign="Right" Width="160" PaddingSpec="5 2 2 2" />
                                        <ext:Label ID="sp1" runat="server" Width="20" PaddingSpec="5 2 2 2" />
                                        <ext:Button ID="btnQuery" runat="server" Text="查询" Icon="FolderExplore" Width="100" OnDirectClick="cmdQuery" Margins="5 2 2 2" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                        <ext:GridPanel ID="GridPanelList" runat="server" Cls="x-grid-custom">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="25">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="ID号" Type="String" />
                                                <ext:ModelField Name="姓名" Type="String" />
                                                <ext:ModelField Name="低分子肝素" Type="String" />
                                                <ext:ModelField Name="首次剂量" Type="String" />
                                                <ext:ModelField Name="追加量" Type="String" />
                                                <ext:ModelField Name="EPO" Type="String" />
                                                <ext:ModelField Name="左卡" Type="String" />
                                                <ext:ModelField Name="甲钴铵" Type="String" />
                                                <ext:ModelField Name="蔗糖铁" Type="String" />
                                                <ext:ModelField Name="透析液钾" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="common" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>                    
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Id" runat="server" DataIndex="ID号" Text="ID号" Width="100" />
                                    <ext:Column ID="Name" runat="server" DataIndex="姓名" Text="姓名" Width="100" />
                                    <ext:Column ID="Column1" runat="server" DataIndex="低分子肝素" Text="低分子肝素" Width="100" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="首次剂量" Text="首次剂量" Width="100" />
                                    <ext:Column ID="Column3" runat="server" DataIndex="追加量" Text="追加量" Width="100" />
                                    <ext:Column ID="Column4" runat="server" DataIndex="EPO" Text="EPO" Width="100" />
                                    <ext:Column ID="Column5" runat="server" DataIndex="左卡" Text="左卡" Width="100" />
                                    <ext:Column ID="Column6" runat="server" DataIndex="甲钴铵" Text="甲钴铵" Width="100" />
                                    <ext:Column ID="Column7" runat="server" DataIndex="蔗糖铁" Text="蔗糖铁" Width="100" />
                                    <ext:Column ID="Column8" runat="server" DataIndex="透析液钾" Text="透析液钾" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="btnToExcel" runat="server" Text="To Excel" AutoPostBack="true" OnClick="cmdToExcel" Icon="PageExcel">
                                            <Listeners>
                                                <Click Fn="saveData1" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" StoreID="Store1" />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
