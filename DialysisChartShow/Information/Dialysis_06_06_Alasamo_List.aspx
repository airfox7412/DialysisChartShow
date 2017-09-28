<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_06_Alasamo_List.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_06_Alasamo_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>阿拉善盟季度小结</title>
    <link href="../css/dialysis_01.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="季度小结" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" SortableColumns="false" Cls="x-grid-custom">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="pat_id" Type="Int" />
                                                <ext:ModelField Name="info_date" Type="String" />
                                                <ext:ModelField Name="dat_1" Type="String" />
                                                <ext:ModelField Name="info_user" Type="String" />
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
                                    <ext:Column ID="indate" runat="server" Text="入院时间" DataIndex="dat_1" Width="100" />
                                    <ext:Column ID="date" runat="server" Text="小结时间" DataIndex="info_date" Width="100" />
                                    <%--<ext:Column ID="user" runat="server" Text="填写人" DataIndex="info_user" Width="100" />--%>
                                    <ext:Column ID="empty" runat="server" Text="" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                                    <DirectEvents>
                                        <Select OnEvent="RowSelect">
                                            <ExtraParams>
                                                <ext:Parameter Name="info_date" Value="record.data.info_date" Mode="Raw" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>    
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store1" />
                            </BottomBar>
                            <View>
                                <ext:GridView ID="GridView1" runat="server" StripeRows="true" />
                            </View>
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Button ID="Btn_Add" runat="server" Text="新增" UI="Success">
                                            <DirectEvents>
                                                <Click OnEvent="Btn_Add_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                        </ext:GridPanel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
