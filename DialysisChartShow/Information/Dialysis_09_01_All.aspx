<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_01_All.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_01_All" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化记录</title>
    <link href="../css/dialysis_01.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="Hidden1" runat="server" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血液净化记录" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:GridPanel ID="Grid_clinical1_nurse" runat="server" SortableColumns="false" Cls="x-grid-custom">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="info_date" Type="String" />
                                                <ext:ModelField Name="data_1" Type="String" />
                                                <ext:ModelField Name="data_2" Type="String" />
                                                <ext:ModelField Name="data_3" Type="String" />
                                                <ext:ModelField Name="data_4" Type="String" />
                                                <ext:ModelField Name="data_5" Type="String" />
                                                <ext:ModelField Name="data_6" Type="String" />
                                                <ext:ModelField Name="data_7" Type="String" />
                                                <ext:ModelField Name="data_8" Type="String" />
                                                <ext:ModelField Name="data_9" Type="String" />
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
                                    <ext:Column ID="ColumnEdit1" runat="server" Text="" Width="60">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Edit1" Icon="TableEdit" Text="记录" />
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="OnRunEdit1">
                                                <ExtraParams>
                                                    <ext:Parameter Name="info_date" Value="record.data.info_date" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="ColumnEdit2" runat="server" Text="" Width="60">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Edit2" Icon="TableGear" Text="小结" />
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="OnRunEdit2">
                                                <ExtraParams>
                                                    <ext:Parameter Name="info_date" Value="record.data.info_date" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="date" runat="server" Text="日期.." DataIndex="info_date" Width="100" />
                                    <ext:Column ID="diagno" runat="server" Text="诊断" DataIndex="data_1" Width="80"/>
                                    <ext:Column ID="Column4" runat="server" Text="血管通路类型" DataIndex="data_2" Width="130" Align="Left" />
                                    <ext:Column ID="Column2" runat="server" Text="透析前体重(Kg)" DataIndex="data_3" Width="150" Align="Right"/>
                                    <ext:Column ID="Column3" runat="server" Text="干体重(Kg)" DataIndex="data_4" Width="120" Align="Right"/>
                                    <ext:Column ID="Column1" runat="server" Text="目标定容量(Kg)" DataIndex="data_5" Width="150" Align="Right"/>
                                    <ext:Column ID="Column5" runat="server" Text="透析后体重(Kg)" DataIndex="data_6" Width="150" Align="Right"/>
                                    <ext:Column ID="Column6" runat="server" Text="肝素首量(mg)" DataIndex="data_7" Width="140" Align="Right"/>
                                    <ext:Column ID="Column7" runat="server" Text="追加量(mg/h)" DataIndex="data_8" Width="140" Align="Right"/>
                                    <ext:Column ID="Column8" runat="server" Text="低分子肝素(u)" DataIndex="data_9" Align="Left" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <%--<SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                                    <DirectEvents>
                                        <Select OnEvent="RowSelect">
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="#{Grid_clinical1_nurse}.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>--%>     
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store1" />
                            </BottomBar>
                            <View>
                                <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                                </ext:GridView>
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
