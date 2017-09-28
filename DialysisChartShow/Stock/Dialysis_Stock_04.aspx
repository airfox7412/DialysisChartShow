﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Stock_04.aspx.cs" Inherits="Dialysis_Chart_Show.Stock.Dialysis_Stock_04" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>退料单</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <link href="../css/grid-r.css" rel="stylesheet"/>
    <script src="<%=ResolveClientUrl("~/Scripts/Stock/Dialysis_Stock_04.js") %>" language="javascript" type="text/javascript"></script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server"/>
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items> 
                <ext:Panel ID="Panel1" runat="server" Title="退料申请单" Region="North" Collapsible="false" Header="false" Height="200" Frame="true" AutoScroll="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Resizable="false" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:DateField ID="DateField1" runat="server" FieldLabel="日期选择" LabelAlign="Right" LabelWidth="70" Width="200" Format="yyyy-MM-dd" />
                                        <ext:DateField ID="DateField2" runat="server" FieldLabel="~" LabelAlign="Right" LabelWidth="10" Width="150" Format="yyyy-MM-dd" />
                                        <ext:Button ID="ButtonSearch" runat="server" Text="搜寻" Icon="Zoom" Width="100" UI="Success">
                                            <DirectEvents>
                                                <Click OnEvent="BtnSearch_Click">
                                                    <EventMask ShowMask="true" Msg="搜集资料中，请稍后..." />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>                                       
                                        <ext:Button ID="btnPrint" runat="server" Text="确认/打印" Icon="Printer" Width="100" UI="Primary">
                                            <Listeners>
                                                <Click Handler="handlerDialysisStock04.onBtnSaveReeturnBefore();"/>
                                            </Listeners>
                                            <DirectEvents>
                                                <Click OnEvent="BtnPrint_Click">
                                                    <EventMask ShowMask="true" Msg="准备打印中，请稍后..." />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="SelectedRow" Value="#{GridPanel1}.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                                        <ext:Parameter Name="data" Value="#{Store2}.getChangedData()" Mode="Raw" Encode="true" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>                                        
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server"> 
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="dyiv_serialno" >
                                            <Fields>
                                                <ext:ModelField Name="no"/>
                                                <ext:ModelField Name="dyiv_serialno"/>
                                                <ext:ModelField Name="dyiv_usrnm" />
                                                <ext:ModelField Name="dyiv_printdate" />
                                                <ext:ModelField Name="dyiv_ivdate" />
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
                            <ColumnModel ID="ColumnModel2" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Header="序号" Width="70" />
                                    <ext:Column ID="Column1" runat="server" DataIndex="dyiv_serialno" Header="申请单ID" Width="140" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="dyiv_usrnm" Header="申请人" Width="80" />
                                    <ext:Column ID="colApplyDate" runat="server" DataIndex="dyiv_ivdate" Header="申请时间" Width="100" Align="Right" />
                                    <ext:Column ID="Column3" runat="server" DataIndex="dyiv_printdate" Header="申请时间" Width="100" Align="Right" />
                                    <ext:Column ID="Column4" runat="server" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                                    <DirectEvents>
                                        <Select OnEvent="RowSelect">
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="#{GridPanel1}.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel> 
                <ext:Panel ID="PanelR" runat="server" Region="Center" Title="退料清单" AutoScroll="true" Header="false" Frame="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel2" runat="server" Resizable="false" Cls="x-grid-custom-r" AutoScroll="true">
                            <Store>
                                <ext:Store ID="Store2" runat="server"> 
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" IDProperty="invr_id" >
                                            <Fields>
                                                <ext:ModelField Name="No"/>
                                                <ext:ModelField Name="Id"/>
                                                <ext:ModelField Name="Name"/>
                                                <ext:ModelField Name="PreAmt"/>
                                                <ext:ModelField Name="RetAmt" AllowNull="true" />
                                                <ext:ModelField Name="SerialNo" />
                                                <ext:ModelField Name="DataReturn" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="dyivl_id" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:Column ID="Column5" runat="server" DataIndex="No" Header="序号" Width="70" />
                                    <ext:Column ID="Column6" runat="server" DataIndex="Name" Header="材料名称" Width="150" />
                                    <ext:Column ID="Column7" runat="server" DataIndex="PreAmt" Header="领用数量" Width="100" Align="Right" />
                                    <ext:Column ID="ColumnReturn" runat="server" DataIndex="RetAmt" Header="退回数量" Width="100" Align="Center">
                                        <Editor>
                                            <ext:TextField ID="TextFieldReturnQty" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column8" runat="server" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:RowEditing ID="RowEditingReturn" runat="server" ClicksToMoveEditor="1" AutoCancel="false"/>
                            </Plugins>
                            <Listeners>
                                <ViewReady Handler="handlerDialysisStock04.onViewReady();" />
                                <ValidateEdit Fn="handlerDialysisStock04.onValidateEdit" />
                                <BeforeEdit Fn="handlerDialysisStock04.onBeforeEdit" />
                            </Listeners>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel> 
                <ext:Window ID="PrintWindow" runat="server" Title="" Y="10" Width="900" Height="700" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader6" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>