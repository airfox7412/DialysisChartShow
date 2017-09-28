<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Device_01.aspx.cs" Inherits="Dialysis_Chart_Show.Device.Dialysis_Device_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>人员档案</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        function dateFormat() {
            Ext.util.Format.dateRenderer('m-d-Y');
        }
   </script>
</head>
<body>
   <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="DetailPanel" runat="server" Title="人员档案" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                            <Items>
                                <ext:GridPanel ID="GridPanel1" runat="server" Padding="3" SortableColumns="true" Resizable="false" Cls="x-grid-custom">
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
                                    <Store>
                                        <ext:Store ID="Store1" runat="server" PageSize="15" > 
                                            <Model>
                                                <ext:Model ID="Model1" runat="server" >
                                                    <Fields>
                                                        <ext:ModelField Name="No" />
                                                        <ext:ModelField Name="dvId" />
                                                        <ext:ModelField Name="createDate" Type="Date" />
                                                        <ext:ModelField Name="empName" />
                                                        <ext:ModelField Name="empSex" />
                                                        <ext:ModelField Name="idKind" />
                                                        <ext:ModelField Name="idNo" />
                                                        <ext:ModelField Name="empKind" />
                                                        <ext:ModelField Name="license" />
                                                        <ext:ModelField Name="eduLevel" />
                                                        <ext:ModelField Name="jobTitle" />
                                                        <ext:ModelField Name="jobDate" Type="Date" />
                                                        <ext:ModelField Name="quitDate" Type="Date" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                            <Sorters>
                                                <ext:DataSorter Property="invs_id" Direction="ASC" />
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
                                                    <ext:DateField ID="sel_createDate" runat="server" Format="yyyy-MM-dd" >
                                                    <%--
                                                        <Listeners>
                                                             <Blur Handler=" alert(this.value.toString()); "></Blur>
                                                        </Listeners>
                                                    --%>
                                                    </ext:DateField>
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
                                            <ext:Column ID="Column4" runat="server" DataIndex="empSex" Header="性别" Width="70">
                                                <Editor>
                                                    <ext:ComboBox ID="cbo_Sex" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="Column5" runat="server" DataIndex="idKind" Header="证件类型" Width="100">
                                                <Editor>
                                                    <ext:ComboBox ID="cbo_idkind" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="Column6" runat="server" DataIndex="idNo" Header="证件编号" Width="100">
                                                <Editor>
                                                    <ext:TextField ID="TextField2" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="Column7" runat="server" DataIndex="empKind" Header="人员类型" Width="100">
                                                <Editor>
                                                    <ext:ComboBox ID="cbo_empkind" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="Column8" runat="server" DataIndex="license" Header="上岗证书" Width="100">
                                                <Editor>
                                                    <ext:TextField ID="TextField3" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="Column9" runat="server" DataIndex="eduLevel" Header="最高学历" Width="90">
                                                <Editor>
                                                    <ext:ComboBox ID="cbo_eduLevel" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="Column10" runat="server" DataIndex="jobTitle" Header="职称" Width="100">
                                                <Editor>
                                                    <ext:ComboBox ID="cbo_jobTitle" runat="server" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:DateColumn ID="DateColumn11" runat="server" DataIndex="jobDate" Header="最早来中心日期" Width="130" Format="yyyy-MM-dd" >
                                                <Editor>
                                                    <ext:DateField ID="sel_jobDate" runat="server" Format="yyyy-MM-dd" />
                                                </Editor>
                                            </ext:DateColumn>
                                            <ext:DateColumn ID="DateColumn12" runat="server" DataIndex="quitDate" Header="离职日期" Width="100" Format="yyyy-MM-dd" >
                                                <Editor>
                                                    <ext:DateField ID="sel_quitDate" runat="server" Format="yyyy-MM-dd" />
                                                </Editor>
                                            </ext:DateColumn>
                                            <ext:Column ID="Column13" runat="server" Width="20">
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
                                        <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1" ClicksToMoveEditor="1" AutoCancel="false">
                                        </ext:CellEditing>
                                    </Plugins>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>                 
                    </Items>
                </ext:FormPanel> 
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>