<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Stock_01.aspx.cs" Inherits="Dialysis_Chart_Show.Stock.Dialysis_Stock_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>库存表</title>
    <link href="../css/grid.css" rel="stylesheet" />
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
        <Items>
            <ext:Panel ID="DetailPanel" runat="server" Title="库存表" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                <Items>
                    <ext:GridPanel ID="GridPanel1" runat="server" Padding="3" SortableColumns="true" Resizable="false" Cls="x-grid-custom">
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:ComboBox ID="cbInvCategory" runat="server" FieldLabel="分类" LabelWidth="100" DisplayField="name" ValueField="code"
                                        LabelAlign="Right" ColumnWidth=".5" EmptyText="选择一个分类" Margins="10 5 10 10">
                                        <Store>
                                            <ext:Store ID="StoreInvCategory" runat="server">
                                                <Model>
                                                    <ext:Model ID="ModelInvCategory" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="code" />
                                                            <ext:ModelField Name="name" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                            </ext:Store>
                                        </Store>
                                        <DirectEvents>
                                            <Select OnEvent="onInvCategoryGroupChanged" />
                                        </DirectEvents>
                                    </ext:ComboBox>
                                    <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="70" Margins="10 5 10 10">
                                        <DirectEvents>
                                            <Click OnEvent="btnAdd_Click">
                                                <EventMask ShowMask="true" />
                                            </Click>
                                        </DirectEvents>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <Store>
                            <ext:Store ID="StoreStock" runat="server" PageSize="15">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="NO" />
                                            <ext:ModelField Name="invs_id" />
                                            <ext:ModelField Name="invs_ctg" />
                                            <ext:ModelField Name="invs_name" />
                                            <ext:ModelField Name="invs_inamt" />
                                            <ext:ModelField Name="invs_outamt" />
                                            <ext:ModelField Name="invs_invamt" />
                                            <ext:ModelField Name="invs_minstock" />
                                            <ext:ModelField Name="price" />
                                            <ext:ModelField Name="cost" />
                                            <ext:ModelField Name="invs_lastupdate" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                                <Reader>
                                    <ext:ArrayReader />
                                </Reader>
                                <Sorters>
                                    <ext:DataSorter Property="invs_ctg" Direction="ASC" />
                                </Sorters>
                                <Listeners>
                                    <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                </Listeners>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:RowNumbererColumn ID="Column1" runat="server" Header="序号" Width="70" Cls="x-grid-hd-inner" />
                                <ext:Column ID="Column2" runat="server" DataIndex="invs_ctg" Header="材料编号" Width="120" />
                                <ext:Column ID="Column3" runat="server" DataIndex="invs_name" Header="材料名称" Width="250" />
                                <ext:Column ID="Column4" runat="server" DataIndex="invs_inamt" Header="总入库量" Width="100">
                                    <Commands>
                                        <ext:ImageCommand CommandName="Edit" Icon="NoteEdit">
                                            <ToolTip Text="入库" />
                                        </ext:ImageCommand>
                                    </Commands>
                                    <DirectEvents>
                                        <Command OnEvent="GenPutIn">
                                            <ExtraParams>
                                                <ext:Parameter Name="invs_id" Value="record.data.invs_id" Mode="Raw" />
                                            </ExtraParams>
                                        </Command>
                                    </DirectEvents>
                                </ext:Column>
                                <ext:Column ID="Column5" runat="server" DataIndex="invs_outamt" Header="总出库量" Width="100" />
                                <ext:Column ID="Column6" runat="server" DataIndex="invs_invamt" Header="库存" Width="100" />
                                <ext:Column ID="Column7" runat="server" DataIndex="invs_minstock" Header="安全存量" Width="100" />
                                <ext:Column ID="Column8" runat="server" DataIndex="price" Header="价格" Width="70" />
                                <ext:Column ID="Column9" runat="server" DataIndex="cost" Header="成本" Width="70" />
                                <ext:Column ID="Column10" runat="server" DataIndex="invs_lastupdate" Header="最后异动日期" Width="120" />
                                <ext:Column ID="Column11" runat="server" Width="20">
                                    <Commands>
                                        <ext:ImageCommand CommandName="invs_Del" Icon="Cancel">
                                            <ToolTip Text="删除" />
                                        </ext:ImageCommand>
                                    </Commands>
                                    <DirectEvents>
                                        <Command OnEvent="cmdDelete">
                                            <Confirmation ConfirmRequest="true" Title="请确认" Message="删除后不可恢复，确定删除?" />
                                            <EventMask ShowMask="true" Target="Page" />
                                            <ExtraParams>
                                                <ext:Parameter Name="invsid" Value="record.data.invs_id" Mode="Raw" />
                                            </ExtraParams>
                                        </Command>
                                    </DirectEvents>
                                </ext:Column>
                            </Columns>
                        </ColumnModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server" DisplayInfo="true" DisplayMsg="显示 分类明细 {0} - {1} of {2}"
                                EmptyMsg="没有 分类明细 可显示" />
                        </BottomBar>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
            <ext:Window ID="Window1" runat="server" Title="入庫单" Width="500" Height="250" Icon="Lock" Closable="false" Resizable="false" Draggable="true" Modal="true" BodyPadding="5" Layout="Form" Hidden="true">
                <Items>
                    <ext:TextField ID="txt_id" runat="server" Hidden="true" />
                    <ext:TextField ID="txt_inamt" runat="server" Hidden="true" /> 
                    <ext:TextField ID="txt_invamt" runat="server" Hidden="true" /> 
                    <ext:Container ID="Container1" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                        <Items>                         
                            <ext:TextField ID="txt_ctg" runat="server" FieldLabel="编号" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" ReadOnly="true" />
                            <ext:TextField ID="txt_name" runat="server" FieldLabel="名称" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_price" runat="server" FieldLabel="价格" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                            <ext:TextField ID="txt_cost" runat="server" FieldLabel="成本" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container3" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_safeqty" runat="server" FieldLabel="安全存量" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                            <ext:TextField ID="txt_putinqty" runat="server" FieldLabel="入库量" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="Button1" runat="server" Text="存盘" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="OnPutin_Click">
                                <EventMask ShowMask="true" Msg="存盘中请稍后..." MinDelay="500" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="Button2" runat="server" Text="放弃" Icon="Cancel">
                        <Listeners>
                            <Click Handler="#{Window1}.hide()" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
            <ext:Window ID="Window2" runat="server" Title="新增" Width="500" Height="250" Icon="Lock" Closable="false" Resizable="false" Draggable="true" Modal="true" BodyPadding="5" Layout="Form" Hidden="true">
                <Items>
                    <ext:TextField ID="txt_id2" runat="server" Hidden="true" />
                    <ext:TextField ID="txt_invamt2" runat="server" Hidden="true" /> 
                    <ext:Container ID="Container21" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                        <Items>                         
                            <ext:TextField ID="txt_ctg2" runat="server" FieldLabel="编号" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                            <ext:TextField ID="txt_name2" runat="server" FieldLabel="名称" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container22" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_price2" runat="server" FieldLabel="价格" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                            <ext:TextField ID="txt_cost2" runat="server" FieldLabel="成本" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container23" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_safeqty2" runat="server" FieldLabel="安全存量" ColumnWidth=".5" LabelAlign="Right" LabelWidth="60" PaddingSpec="10 10 0 2" Flex="1" />
                        </Items>
                    </ext:Container>
                </Items>
                <Buttons>
                    <ext:Button ID="Button21" runat="server" Text="存盘" Icon="Accept">
                        <DirectEvents>
                            <Click OnEvent="OnSave_Click">
                                <EventMask ShowMask="true" Msg="存盘中请稍后..." MinDelay="500" />
                            </Click>
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="Button22" runat="server" Text="放弃" Icon="Cancel">
                        <Listeners>
                            <Click Handler="#{Window2}.hide()" />
                        </Listeners>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        </Items>
    </ext:Viewport>
    </form>
</body>
</html>
