<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Device_04.aspx.cs" Inherits="Dialysis_Chart_Show.Device.Dialysis_Device_04" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>设备档案</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="设备档案" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="70" Margins="10 5 10 10" >
                                    <Listeners>
                                        <Click Handler="#{btnAdd}.disable(); #{Store1}.insert(0, {}); #{GridPanel1}.editingPlugin.startEditByPosition({row:0, column:0});" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Accept" Width="70" Disabled="true" Margins="10 5 10 10" >
                                    <DirectEvents>
                                        <Click OnEvent="cmdSAVE" Before="return #{Store1}.isDirty();">
                                            <EventMask ShowMask="true" />
                                            <ExtraParams>
                                                <ext:Parameter Name="data" Value="#{Store1}.getChangedData()" Mode="Raw" Encode="true" />
                                                <ext:Parameter Name="data2" Value="#{Store1}.getRecordsValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btnCANCEL" runat="server" Text="取消" Icon="Cancel" Width="70" Disabled="true" Margins="10 5 10 10" >                                
                                    <DirectEvents>
                                        <Click OnEvent="cmdCANCEL">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>                                      
                                <ext:Button ID="btnDelete" runat="server" Text="删除" Icon="Delete" Width="70" Disabled="true" Margins="10 5 10 10" Visible="false">
                                    <DirectEvents>
                                        <Click OnEvent="cmdDelete" Success="#{GridPanel1}.getStore().reload();" Failure="Ext.net.Notification.show({html: result.errorMessage,title: '提示'});">
                                            <Confirmation ConfirmRequest="true" Title="请确认" Message="删除后不可恢复，确定删除?" />
                                            <EventMask ShowMask="true" Target="Page" />
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="Ext.encode(#{GridPanel1}.getRowsValues({selectedOnly : true}))" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>  
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Padding="3" SortableColumns="false" Resizable="false" Cls="x-grid-custom">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15" > 
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="NO" />
                                                <ext:ModelField Name="dvId" />
                                                <ext:ModelField Name="createDate" Type="Date" />
                                                <ext:ModelField Name="dv_Kind" />
                                                <ext:ModelField Name="dv_Brand" />
                                                <ext:ModelField Name="dv_Type" />
                                                <ext:ModelField Name="dv_Serialno" />
                                                <ext:ModelField Name="dv_sdate" Type="Date" />
                                                <ext:ModelField Name="dv_ddate" Type="Date" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="dvId" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Header="序" Width="60" />
                                    <ext:DateColumn ID="DateColumn2" runat="server" DataIndex="createDate" Header="填写日期" Width="100" Format="yyyy-MM-dd" >
                                        <Editor>
                                            <ext:DateField ID="sel_createDate" runat="server" Format="yyyy-MM-dd" />
                                        </Editor>
                                    </ext:DateColumn>
                                    <ext:Column ID="Column3" runat="server" DataIndex="dv_Kind" Header="设备类型" Width="100" >
                                        <Editor>
                                            <ext:ComboBox ID="cbo_macType" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" DataIndex="dv_Brand" Header="设备品牌" Width="100" >
                                        <Editor>
                                            <ext:TextField ID="TextField2" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" DataIndex="dv_Type" Header="设备型号" Width="100" >
                                        <Editor>
                                            <ext:TextField ID="TextField3" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" DataIndex="dv_Serialno" Header="设备序列号" Width="500" >
                                        <Editor>
                                            <ext:TextField ID="TextField4" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:DateColumn ID="DateColumn7" runat="server" DataIndex="dv_sdate" Header="启用日期" Width="100" Format="yyyy-MM-dd" >
                                        <Editor>
                                            <ext:DateField ID="sel_dv_sdate" runat="server" Format="yyyy-MM-dd" />
                                        </Editor>
                                    </ext:DateColumn>
                                    <ext:DateColumn ID="DateColumn8" runat="server" DataIndex="dv_ddate" Header="报废日期" Width="100" Format="yyyy-MM-dd" >
                                        <Editor>
                                            <ext:DateField ID="sel_dv_ddate" runat="server" Format="yyyy-MM-dd" />
                                        </Editor>
                                    </ext:DateColumn>
                                    <ext:Column ID="Column10" runat="server" Width="20">
                                        <Commands>
                                            <ext:ImageCommand CommandName="invs_Del" Icon="Cancel">
                                                <ToolTip Text="删除" />
                                            </ext:ImageCommand> 
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="mac_Delete">
                                                <Confirmation ConfirmRequest="true" Title="请确认" Message="删除后不可恢复，确定删除?" />
                                                <EventMask ShowMask="true" Target="Page" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="dvId" Value="record.data.dvId" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents> 
                                    </ext:Column>
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