<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_00_List.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_00_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>交班记录表</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="交班记录表">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:DateField ID="txtBegin_DATE" runat="server" FieldLabel="日期" LabelWidth="60" Format="yyyy-MM-dd" LabelAlign="Right" Width="200"  PaddingSpec="5 2 2 2" />
                                        <ext:DateField ID="txtEnd_DATE" runat="server" FieldLabel="~" LabelWidth="20" Format="yyyy-MM-dd"  LabelAlign="Right" Width="150"  PaddingSpec="5 2 2 2" />
                                        <ext:Button ID="btnQuery" runat="server" Text="查询" Icon="FolderExplore" Width="100" Margins="5 2 2 2" UI="Info">
                                            <DirectEvents>
                                                <Click OnEvent="BtnQuery_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                        <ext:Button ID="BtnAdd" runat="server" Text="增加" Icon="ApplicationFormEdit" Cls="Text-blue16" AutoPostBack="false" UI="Success">
                                            <DirectEvents>
                                                <Click OnEvent="BtnAdd_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="BtnDel" runat="server" Text="删除" Icon="FolderMagnify" IconCls="Text-green16" Disabled="true">
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
                                                <ext:ModelField Name="sid" Type="String" />
                                                <ext:ModelField Name="sdate" Type="String" />
                                                <ext:ModelField Name="stime" Type="String" />
                                                <ext:ModelField Name="user" Type="String" />
                                                <ext:ModelField Name="subject" Type="String" />
                                                <ext:ModelField Name="remark" Type="String" />
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
                                    <ext:Column ID="sdate" runat="server" Text="日期" DataIndex="sdate" Width="100" />
                                    <ext:Column ID="stime" runat="server" Text="时间" DataIndex="stime" Width="100" />
                                    <ext:Column ID="user" runat="server" Text="交办人" DataIndex="user" Width="100" />
                                    <ext:Column ID="subject" runat="server" Text="交办事项" DataIndex="subject" Width="200" />
                                    <ext:Column ID="remark" runat="server" Text="备注" DataIndex="remark" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                                    <DirectEvents>
                                        <Select OnEvent="RowSelect">
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="record.data.sid" Mode="Raw" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
