<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_08_List.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_0h_08_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>评估表</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="Hidden1" runat="server" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="评估表(苏州医院)" BodyStyle="background-color:#EBF5FF !important;">
            <Items>
                <ext:GridPanel ID="GridPanel1" runat="server" Cls="x-grid-custom">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <ext:ToolbarFill ID="ToolbarFill2" runat="server" />
                                <ext:Button ID="BtnAddS" runat="server" Text="增加" Icon="ApplicationFormEdit" Cls="Text-blue16" AutoPostBack="false">
                                    <DirectEvents>
                                        <Click OnEvent="BtnAdd_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Button5" runat="server" Text="删除" Icon="FolderMagnify" IconCls="Text-green16">
                                    <DirectEvents>
                                        <Click OnEvent="BtnDel_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store ID="Store1" runat="server" PageSize="25">
                            <Model>
                                <ext:Model ID="Model1" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="pat_id" Type="String" />
                                        <ext:ModelField Name="info_date" Type="String" />
                                        <ext:ModelField Name="txt_leader" Type="String" />
                                        <ext:ModelField Name="txt_nurse" Type="String" />
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
                            <ext:Column ID="pat_id" runat="server" Text="pat_id" DataIndex="pat_id" Width="100" Visible="false" />
                            <ext:Column ID="info_date" runat="server" Text="日期" DataIndex="info_date" Width="100" />
                            <ext:Column ID="txt_leader" runat="server" Text="责任组长" DataIndex="txt_leader" Width="100" />
                            <ext:Column ID="txt_nurse" runat="server" Text="责任护士" DataIndex="txt_nurse" Width="100" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                            <DirectEvents>
                                <Select OnEvent="RowSelect">
                                    <ExtraParams>
                                        <ext:Parameter Name="Values" Value="App.GridPanel1.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                    </ExtraParams>
                                </Select>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
            </Items>
        </ext:FormPanel>
    </div>
    </form>
</body>
</html>
