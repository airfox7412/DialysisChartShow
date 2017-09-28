<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Device_02.aspx.cs" Inherits="Dialysis_Chart_Show.Device.Dialysis_Device_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员肝炎指标</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="人员肝炎指标" Region="North" Collapsible="false" Header="false" AutoScroll="true">
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
                                                <ext:ModelField Name="NO" Type="Int" />
                                                <ext:ModelField Name="dvId" Type="Int" />
                                                <ext:ModelField Name="createDate" Type="Date" />
                                                <ext:ModelField Name="empName" />
                                                <ext:ModelField Name="HBsAg" />
                                                <ext:ModelField Name="AntiHBs" />
                                                <ext:ModelField Name="HBeAg" />
                                                <ext:ModelField Name="AntiHBe" />
                                                <ext:ModelField Name="AntiHBc" />
                                                <ext:ModelField Name="AnitHCV" />
                                                <ext:ModelField Name="Aids" />
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
                                    <ext:DateColumn ID="DateColumn2" runat="server" DataIndex="createDate" Header="检查日期" Width="100" Format="yyyy-MM-dd" >
                                        <Editor>
                                            <ext:DateField ID="sel_createDate" runat="server" Format="yyyy-MM-dd" />
                                        </Editor>
                                    </ext:DateColumn>
                                    <ext:Column ID="Column3" runat="server" DataIndex="empName" Header="姓名" Width="100">
                                        <Editor>
                                            <%-- <ext:TextField ID="TextField1" runat="server" /> --%>
                                            <ext:ComboBox ID="cb_praclist" runat="server" IndicatorCls="emptyColor" 
                                                LabelAlign="Right" DisplayField="fname" ValueField="fname" Width="100" 
                                                TypeAhead="false" HideBaseTrigger="true" PageSize="10" MinChars="1" TriggerAction="Query"
                                                PaddingSpec="2 10 2 2" EmptyText="姓名" EmptyCls="emptyColor">
                                                <Store>
                                                    <ext:Store ID="Store3" runat="server" AutoLoad="true">
                                                        <Proxy>
                                                            <ext:AjaxProxy Url="../PracInfos.ashx">
                                                                <ActionMethods Read="POST" />
                                                                <Reader>
                                                                    <ext:JsonReader RootProperty="PracInfos" TotalProperty="total" />
                                                                </Reader>
                                                            </ext:AjaxProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model3" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="stfcode" />
                                                                    <ext:ModelField Name="fname" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig LoadingText="寻找中..."  >
                                                    <ItemTpl ID="ItemTpl2" runat="server">
                                                        <Html>
                                                            <div>
                                                                <h4>{fname}</h4>
                                                            </div>
                                                        </html>
                                                    </ItemTpl>
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" DataIndex="HBsAg" Header="HBsAg" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cbo_HBsAg" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" DataIndex="AntiHBs" Header="AntiHBs" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cbo_AntiHBs" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" DataIndex="HBeAg" Header="HBeAg" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cbo_HBeAg" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column7" runat="server" DataIndex="AntiHBe" Header="AntiHBe" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cbo_AntiHBe" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column8" runat="server" DataIndex="AntiHBc" Header="AntiHBc" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cbo_AntiHBc" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column9" runat="server" DataIndex="AnitHCV" Header="AnitHCV" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cbo_AntiHCV" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column11" runat="server" DataIndex="Aids" Header="艾滋病" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cbo_Aids" runat="server" />
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
                                                    <ext:Parameter Name="dvId" Value="record.data.dvId" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents> 
                                    </ext:Column>
                                    <ext:Column ID="Column10" runat="server" Flex="1" />
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