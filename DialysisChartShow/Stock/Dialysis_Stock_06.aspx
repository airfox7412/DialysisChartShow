<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Stock_06.aspx.cs" Inherits="Dialysis_Chart_Show.Stock.Dialysis_Stock_06" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>进货明细</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="进货明细" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Padding="3" SortableColumns="false" Resizable="false" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:DateField ID="DateField1" runat="server" Text="日期" ColumnWidth=".1" Format="yyyy-MM-dd" />
                                        <ext:DateField ID="DateField2" runat="server" Text="~" ColumnWidth=".1" Format="yyyy-MM-dd" />
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
                                                <ext:ModelField Name="NO" Type="Int" />
                                                <ext:ModelField Name="ivrec_id" />
                                                <ext:ModelField Name="ivrec_code" />
                                                <ext:ModelField Name="ivrec_name" />
                                                <ext:ModelField Name="ivrec_amt" />
                                                <ext:ModelField Name="ivrec_daterec" />
                                                <ext:ModelField Name="ivrec_user" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="ivrec_id" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Text="序" Width="70" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="ivrec_code" Header="材料编号" Width="120" />
                                    <ext:Column ID="Column3" runat="server" DataIndex="ivrec_name" Header="耗材名称" Width="150" />
                                    <ext:Column ID="Column4" runat="server" DataIndex="ivrec_amt" Header="数量" Width="100" />
                                    <ext:Column ID="Column5" runat="server" DataIndex="ivrec_daterec" Header="进货日期" Width="100" />
                                    <ext:Column ID="Column6" runat="server" DataIndex="ivrec_user" Header="入庫人員" Width="100" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server"                      
                                    DisplayInfo="true"
                                    DisplayMsg="显示 分类明细 {0} - {1} of {2}"
                                    EmptyMsg="没有 分类明细 可显示"                
                                    />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel> 
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>