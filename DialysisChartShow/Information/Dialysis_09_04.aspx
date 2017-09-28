<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_04.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_04" %>
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
                                        <ext:DateField ID="txtBegin_DATE" runat="server" FieldLabel="日期" LabelWidth="60" Format="yyyy-MM-dd" LabelAlign="Right" Width="200"  PaddingSpec="5 2 2 2" />
                                        <ext:DateField ID="txtEnd_DATE" runat="server" FieldLabel="~" LabelWidth="20" Format="yyyy-MM-dd"  LabelAlign="Right" Width="150"  PaddingSpec="5 2 2 2" />                                        
                                        <ext:TextField ID="txtName" runat="server" FieldLabel="姓名" LabelWidth="60" LabelAlign="Right" Width="160" PaddingSpec="5 2 2 2" />
                                        <ext:Label ID="sp1" runat="server" Width="20" PaddingSpec="5 2 2 2" />
                                        <ext:Button ID="btnQuery" runat="server" Text="查询" Icon="FolderExplore" Width="100" OnDirectClick="cmdQuery" Margins="5 2 2 2" />
                                        <ext:Label ID="sp2" runat="server" Width="20" PaddingSpec="5 2 2 2" />                                        
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
                                                <ext:ModelField Name="身分证号" Type="String" />
                                                <ext:ModelField Name="姓名" Type="String" />
                                                <ext:ModelField Name="注射费" Type="String" />
                                                <ext:ModelField Name="怡宝" Type="String" />
                                                <ext:ModelField Name="益比奥" Type="String" />
                                                <ext:ModelField Name="左卡" Type="String" />
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
                                    <ext:Column ID="cln1_patic" runat="server" DataIndex="身分证号" Text="身分证号" Width="170" />
                                    <ext:Column ID="pif_name" runat="server" DataIndex="姓名" Text="姓名" Width="100" />
                                    <ext:Column ID="Column16" runat="server" DataIndex="注射费" Text="注射费" Width="100" />
                                    <ext:Column ID="epo3000" runat="server" DataIndex="怡宝" Text="怡宝" Width="80" />
                                    <ext:Column ID="epo10000" runat="server" DataIndex="益比奥" Text="益比奥" Width="80" />
                                    <ext:Column ID="leftcard" runat="server" DataIndex="左卡" Text="左卡" Width="80" />
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
