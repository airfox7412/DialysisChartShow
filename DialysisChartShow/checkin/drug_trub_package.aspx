<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drug_trub_package.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.drug_trub_package" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血管通路耗材组合</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
               
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Region="North" Collapsible="false" Border="false" Header="false" >
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Cls="x-grid-custom" Height="630">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:ComboBox ID="ComboBoxGroup" runat="server" FieldLabel="分类" LabelWidth="100" LabelAlign="Right" ColumnWidth=".5"  EmptyText="选择一个分类" Margins="10 5 10 10" >
                                            <DirectEvents>
                                                <Select OnEvent="ChangGroup">                                                
                                                    <EventMask ShowMask="true" Msg="读取中..." />
                                                </Select>
                                            </DirectEvents>
                                        </ext:ComboBox> 
                                        <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="100" Margins="10 5 10 10" >
                                            <Listeners>
                                                <Click Handler="#{btnAdd}.disable(); #{Store2}.insert(0, {}); #{GridPanel2}.editingPlugin.startEditByPosition({row:0, column:0});" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Accept" Width="100" Disabled="true" Margins="10 5 10 10" >
                                            <DirectEvents>
                                                <Click OnEvent="cmdSAVE" Before="return #{Store2}.isDirty();">
                                                    <EventMask ShowMask="true" Msg="保存中..." />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="data" Value="#{Store2}.getChangedData()" Mode="Raw" Encode="true" />
                                                        <ext:Parameter Name="data2" Value="#{Store2}.getRecordsValues()" Mode="Raw" Encode="true" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnCANCEL" runat="server" Text="取消" Icon="Cancel" Width="100" Disabled="true" Margins="10 5 10 10" >                                
                                            <DirectEvents>
                                                <Click OnEvent="cmdCANCEL">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button> 
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store2" runat="server" PageSize="25" > 
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="NO" />
                                                <ext:ModelField Name="pdet_id" />
                                                <ext:ModelField Name="pck_name" />
                                                <ext:ModelField Name="pdet_itemcd" />
                                                <ext:ModelField Name="pdet_itemnm" />
                                                <ext:ModelField Name="pdet_qty" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="NO" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Width="50" />
                                    <ext:Column ID="pck_code" runat="server" DataIndex="pck_name" Header="分类" Width="150">
                                        <Editor>
                                            <ext:ComboBox ID="ComboBoxPck" runat="server" TypeAhead="true" SelectOnTab="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" DataIndex="pdet_itemnm" Header="材料名称" Width="200">
                                        <Editor>
                                            <ext:ComboBox ID="ComboBoxMaterial" runat="server" TypeAhead="true" SelectOnTab="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column7" runat="server" DataIndex="pdet_itemcd" Header="材料编号" Width="100" Visible="false">
                                        <Editor>
                                            <ext:TextField ID="TextField2" runat="server" ReadOnly="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" DataIndex="pdet_qty" Header="数量" Width="100">                                            
                                        <Editor>
                                            <ext:TextField ID="TextField3" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column2" runat="server" Width="20">
                                        <Commands>
                                            <ext:ImageCommand CommandName="cmdDel" Icon="Cancel">
                                                <ToolTip Text="删除" />
                                            </ext:ImageCommand> 
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="cmdDelete">
                                                <Confirmation ConfirmRequest="true" Title="请确认" Message="删除后不可恢复，确定删除?" />
                                                <EventMask ShowMask="true" Target="Page" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="pdet_id" Value="record.data.pdet_id" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents> 
                                    </ext:Column>
                                    <ext:Column ID="Column10" runat="server" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" />
                            </BottomBar>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel1" runat="server">
                                    <Listeners>
                                        <Select Handler="#{btnSave}.enable();#{btnCancel}.enable();#{btnDelete}.enable();" />
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
