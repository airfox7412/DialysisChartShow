<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="pat_search.aspx.cs" Inherits="Dialysis_Chart_Show.pat_search" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病人查找</title>
    <link href="css/dialysis_01.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN" Theme="Triton" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="pnlTableLayout" runat="server" Header="false" AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <ext:ComboBox ID="Text_Name" runat="server" FieldLabel="姓名" LabelWidth="50" Width="200" Cls="Text-blue" LabelAlign="Right"
                                    DisplayField="patname" ValueField="patname" TypeAhead="false" PageSize="20" HideTrigger="true" MinChars="1" TriggerAction="Query">                            
                                    <ListConfig LoadingText="寻找中...">
                                        <ItemTpl ID="ItemTpl1" runat="server">
                                            <Html>
                                                <div>{patname}</div>
                                            </html>
                                        </ItemTpl>
                                    </ListConfig>
                                    <Store>
                                        <ext:Store ID="Store2" runat="server" AutoLoad="false">
                                            <Proxy>
                                                <ext:AjaxProxy Url="../Patinfos.ashx">
                                                    <ActionMethods Read="POST" />
                                                    <Reader>
                                                        <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                    </Reader>
                                                </ext:AjaxProxy>
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="Model2" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="patic" />
                                                        <ext:ModelField Name="patname" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                </ext:ComboBox>
                                <ext:TextField ID="Text_ID" runat="server" FieldLabel="身份证号" LabelWidth="100" LabelAlign="Right" Width="250" />
                                <ext:Button ID="btn_Query" runat="server" Icon="Find" Text="病患查找" Width="100" MarginSpec="10 10 5 5">
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>                      
                        <ext:GridPanel ID="GridList" runat="server" Padding="3" SortableColumns="false" Resizable="false" Cls="x-grid-custom">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="10">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="pat_id" />
                                            <ext:ModelField Name="pif_name" />
                                            <ext:ModelField Name="pif_sex" />
                                            <ext:ModelField Name="pif_dob" />
                                            <ext:ModelField Name="pat_ic" />
                                            <ext:ModelField Name="txt_10" />
                                            <ext:ModelField Name="FirstDate" />
                                            <ext:ModelField Name="InfoDate" />
                                            <ext:ModelField Name="txt_101" />
                                            <ext:ModelField Name="opt_52" />
                                            <ext:ModelField Name="info_date" />    
                                            <ext:ModelField Name="BIO_NotChecked" />                                                       
                                            <ext:ModelField Name="pif_docname" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Reader>
                                    <ext:ArrayReader />
                                </Reader>
                                <Sorters>
                                    <ext:DataSorter Property="pat_id" Direction="ASC" />
                                </Sorters>
                            </ext:Store>
                            </Store>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Text="序" Width="50" />
                                    <ext:Column ID="Column3" runat="server" Text="姓名" DataIndex="pif_name" Width="80" />
                                    <ext:Column ID="Column4" runat="server" Text="性别" DataIndex="pif_sex" Width="60" />                                            
                                    <ext:Column ID="Column5" runat="server" Text="出生日期" DataIndex="pif_dob" Width="110" />
                                    <ext:Column ID="Column6" runat="server" Text="身份证号" DataIndex="pat_ic" Width="190" />                                            
                                    <ext:Column ID="Column7" runat="server" Text="血/腹" DataIndex="txt_10" Width="60" />
                                    <ext:Column ID="Column15" runat="server" Text="经治医生" DataIndex="pif_docname" Region="Center" Width="100" />
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:BufferedRenderer ID="BufferedRenderer1" runat="server" />
                            </Plugins>
                            <View>
                                <ext:GridView ID="GridView1" runat="server" TrackOver="false" />
                            </View>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">                                
                                    <DirectEvents>
                                        <Select OnEvent="Dialysis_detail">
                                            <EventMask ShowMask="true" Msg="处理中….." Target="CustomTarget" CustomTarget="#{pnlTableLayout}" />                                                        
                                            <ExtraParams>
                                                <ext:Parameter Name="pat_ic" Value="record.data.pat_ic" Mode="Raw">
                                                </ext:Parameter>
                                                <ext:Parameter Name="pif_name" Value="record.data.pif_name" Mode="Raw">
                                                </ext:Parameter>
                                                <ext:Parameter Name="pif_sex" Value="record.data.pif_sex" Mode="Raw">
                                                </ext:Parameter>
                                                <ext:Parameter Name="pif_docname" Value="record.data.pif_docname" Mode="Raw">
                                                </ext:Parameter>
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>      
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store1" />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>    
        </ext:Viewport>
    </form>
</body>
</html>

