<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Device_05.aspx.cs" Inherits="Dialysis_Chart_Show.Device.Dialysis_Device_05" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>透析中心信息</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="透析中心信息" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="70" Margins="10 5 10 10" >
                                    <%-- 
                                    <Listeners>
                                        <Click Handler="#{btnAdd}.disable(); #{Store1}.insert(0, {}); #{GridPanel1}.editingPlugin.startEditByPosition({row:0, column:0});" />
                                    </Listeners>
                                    --%>
                                    <DirectEvents>
                                        <Click OnEvent="cmdADD"></Click>
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
                                                <ext:ModelField Name="dv_id" />
                                                <ext:ModelField Name="infoDate" />
                                                <ext:ModelField Name="CenterArea" />
                                                <ext:ModelField Name="DialysisArea" />
                                                <ext:ModelField Name="BedZone1" />
                                                <ext:ModelField Name="BedZone2" />
                                                <ext:ModelField Name="BedZone3" />
                                                <ext:ModelField Name="BedZone4" />
                                                <ext:ModelField Name="Eng1" />
                                                <ext:ModelField Name="Eng2" />
                                                <ext:ModelField Name="Eng3" />
                                                <ext:ModelField Name="Eng4" />
                                                <ext:ModelField Name="dvKind1" />
                                                <ext:ModelField Name="dvKind2" />
                                                <ext:ModelField Name="dvKind3" />
                                                <ext:ModelField Name="dvKind4" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="infoDate" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Header="序" Width="60" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="infoDate" Header="检查日期" Width="100" />
                                    <ext:Column ID="Column3" runat="server" Text="总面积 单位︰平米">
                                        <Columns>
                                            <ext:Column ID="Column31" runat="server" DataIndex="CenterArea" Header="血液透析中心" Align="Center" Width="130" />
                                            <ext:Column ID="Column32" runat="server" DataIndex="DialysisArea" Header="透析单元" Align="Center" Width="100" />
                                        </Columns>
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" Text="床位 单位︰张">
                                        <Columns>
                                            <ext:Column ID="Column41" runat="server" DataIndex="BedZone1" Header="普通治疗区" Align="Center" Width="120" />
                                            <ext:Column ID="Column42" runat="server" DataIndex="BedZone2" Header="乙肝隔离治疗区" Align="Center" Width="150" />
                                            <ext:Column ID="Column43" runat="server" DataIndex="BedZone3" Header="丙肝隔离治疗区" Align="Center" Width="150" />
                                            <ext:Column ID="Column44" runat="server" DataIndex="BedZone4" Header="其他隔离治疗区" Align="Center" Width="150" />
                                        </Columns>
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" Text="工程技术人员 单位︰人数">
                                        <Columns>
                                            <ext:Column ID="Column51" runat="server" DataIndex="Eng1" Header="正高" Align="Center" Width="80" />
                                            <ext:Column ID="Column52" runat="server" DataIndex="Eng2" Header="副高" Align="Center" Width="80" />
                                            <ext:Column ID="Column53" runat="server" DataIndex="Eng3" Header="中级" Align="Center" Width="80" />
                                            <ext:Column ID="Column54" runat="server" DataIndex="Eng4" Header="初级" Align="Center" Width="80" />
                                        </Columns>
                                    </ext:Column>
                                    <ext:Column ID="Column10" runat="server" RightCommandAlign="false" Flex="1" >
                                        <Commands>
                                            <ext:ImageCommand CommandName="dv_Del" Icon="Cancel">
                                                <ToolTip Text="删除" />
                                            </ext:ImageCommand> 
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="dvinfo_Delete">
                                                <Confirmation ConfirmRequest="true" Title="请确认" Message="删除后不可恢复，确定删除?" />
                                                <EventMask ShowMask="true" Target="Page" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="dvid" Value="record.data.dv_id" Mode="Raw" />
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
                                <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single">                                
                                    <DirectEvents>
                                        <Select OnEvent="Edit_dv05">
                                            <EventMask ShowMask="true" Msg="处理中….." Target="CustomTarget" CustomTarget="#{pnlTableLayout}" />                                                        
                                            <ExtraParams>
                                                <ext:Parameter Name="dv_id" Value="record.data.dv_id" Mode="Raw" />
                                                <ext:Parameter Name="infoDate" Value="record.data.infoDate" Mode="Raw" />
                                                <ext:Parameter Name="CenterArea" Value="record.data.CenterArea" Mode="Raw" />
                                                <ext:Parameter Name="DialysisArea" Value="record.data.DialysisArea" Mode="Raw" />
                                                <ext:Parameter Name="BedZone1" Value="record.data.BedZone1" Mode="Raw" />
                                                <ext:Parameter Name="BedZone2" Value="record.data.BedZone2" Mode="Raw" />
                                                <ext:Parameter Name="BedZone3" Value="record.data.BedZone3" Mode="Raw" />
                                                <ext:Parameter Name="BedZone4" Value="record.data.BedZone4" Mode="Raw" />
                                                <ext:Parameter Name="dvKind1" Value="record.data.dvKind1" Mode="Raw" />
                                                <ext:Parameter Name="dvKind2" Value="record.data.dvKind2" Mode="Raw" />
                                                <ext:Parameter Name="dvKind3" Value="record.data.dvKind3" Mode="Raw" />
                                                <ext:Parameter Name="dvKind4" Value="record.data.dvKind4" Mode="Raw" />
                                                <ext:Parameter Name="Eng1" Value="record.data.Eng1" Mode="Raw" />
                                                <ext:Parameter Name="Eng2" Value="record.data.Eng2" Mode="Raw" />
                                                <ext:Parameter Name="Eng3" Value="record.data.Eng3" Mode="Raw" />
                                                <ext:Parameter Name="Eng4" Value="record.data.Eng4" Mode="Raw" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel> 
                            <Plugins>
                                <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1" />
                            </Plugins>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel> 
                <ext:Window ID="Window1" runat="server" Title="透析中心信息" Hidden="true" Width="480" Height="630">
                    <Items>
                        <ext:Container ID="Container_Long1" runat="server" Layout="FitLayout">
                            <Items>
                                <ext:Hidden ID="dvInfo" runat="server" />
                                <ext:Hidden ID="id" runat="server" />
                                <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" ColumnWidth="1" LabelWidth="220" Format="yyyy-MM-dd" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" Flex="1" />
                                <ext:TextField ID="txt_dvKind1" runat="server" FieldLabel="水处理系统--直供式单级反渗系统" LabelWidth="220" IndicatorText="台" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_dvKind2" runat="server" FieldLabel="水处理系统--直供式双级反渗系统" LabelWidth="220" IndicatorText="台" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_dvKind3" runat="server" FieldLabel="水处理系统--非直供式单级反渗系统" LabelWidth="220" IndicatorText="台" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_dvKind4" runat="server" FieldLabel="水处理系统--非直供式双级反渗系统" LabelWidth="220" IndicatorText="台" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_centerArea" runat="server" FieldLabel="血液透析中心--总面积" LabelWidth="220" IndicatorText="平米" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_dialysisArea" runat="server" FieldLabel="透析单元--总面积" LabelWidth="220" IndicatorText="平米" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_bedZone1" runat="server" FieldLabel="床位--普通治疗区" LabelWidth="220" IndicatorText="张" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_bedZone2" runat="server" FieldLabel="床位--乙肝隔离治疗区" LabelWidth="220" IndicatorText="张" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_bedZone3" runat="server" FieldLabel="床位--丙肝隔离治疗区" LabelWidth="220" IndicatorText="张" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_bedZone4" runat="server" FieldLabel="床位--其他隔离治疗区" LabelWidth="220" IndicatorText="张" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_engineer1" runat="server" FieldLabel="工程技术人员--正高" LabelWidth="220" IndicatorText="人数" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_engineer2" runat="server" FieldLabel="工程技术人员--副高" LabelWidth="220" IndicatorText="人数" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_engineer3" runat="server" FieldLabel="工程技术人员--中级" LabelWidth="220" IndicatorText="人数" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                                <ext:TextField ID="txt_engineer4" runat="server" FieldLabel="工程技术人员--初级" LabelWidth="220" IndicatorText="人数" ColumnWidth="1" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                            </Items>
                        </ext:Container> 
                    </Items>
                    <Buttons>
                        <ext:Button ID="BtnAccept" runat="server" Icon="Accept" Text="确认" Width="150" Height="30">
                            <DirectEvents>
                                <Click OnEvent="BtnAccept_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button1" runat="server" Icon="Cancel" Text="取消" Width="150" Height="30">
                            <DirectEvents>
                                <Click OnEvent="BtnCancel_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>