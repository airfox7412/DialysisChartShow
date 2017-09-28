<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_System_03.aspx.cs" Inherits="Dialysis_Chart_Show.Systems.Dialysis_System_03" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血管通路类型维护</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="血管通路类型维护" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Height="600" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:ComboBox ID="ComboBoxGroup" runat="server" FieldLabel="分类" LabelWidth="100" LabelAlign="Right" ColumnWidth=".5"  EmptyText="选择一个分类" Margins="10 5 10 10" >
                                        <DirectEvents>
                                            <Select OnEvent="ChangGroup" />
                                        </DirectEvents>
                                    </ext:ComboBox> 
                                    <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="70" Margins="10 5 10 10" >
                                        <Listeners>
                                            <Click Handler="#{btnAdd}.disable(); #{Store2}.insert(0, {}); #{GridPanel2}.editingPlugin.startEditByPosition({row:0, column:0});" />
                                        </Listeners>
                                    </ext:Button>
                                    <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Accept" Width="70" Disabled="true" Margins="10 5 10 10" >
                                        <DirectEvents>
                                            <Click OnEvent="cmdSAVE" Before="return #{Store2}.isDirty();">
                                                <EventMask ShowMask="true" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="data" Value="#{Store2}.getChangedData()" Mode="Raw" Encode="true" />
                                                    <ext:Parameter Name="data2" Value="#{Store2}.getRecordsValues()" Mode="Raw" Encode="true" />
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
                                    <ext:Button ID="btnDelete" runat="server" Text="删除" Icon="Delete" Width="60" Disabled="true" Margins="10 5 10 10" Visible="false" >
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
                                <ext:Store ID="Store2" runat="server" PageSize="15" > 
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="NO" />
                                                <ext:ModelField Name="sid" />
                                                <ext:ModelField Name="pck_code" />
                                                <ext:ModelField Name="pck_name" />
                                                <ext:ModelField Name="drg_code" />
                                                <ext:ModelField Name="drg_name" />
                                                <ext:ModelField Name="intake" />
                                                <ext:ModelField Name="freq" />
                                                <ext:ModelField Name="medway" />
                                                <ext:ModelField Name="USED" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="drg_id" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Header="序" Width="70" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="pck_name" Header="分类" Width="150" Cls="x-grid-hd-inner">
                                        <Editor>
                                            <ext:ComboBox ID="ComboBoxPck" runat="server" TypeAhead="true" SelectOnTab="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column3" runat="server" DataIndex="drg_name" Header="名称" Width="200" Cls="x-grid-hd-inner" >
                                        <Editor>
                                            <ext:ComboBox ID="ComboBoxDrug" runat="server" TypeAhead="true" SelectOnTab="true" 
                                                DisplayField="drugname" ValueField="drugname" PageSize="10"  
                                                HideBaseTrigger="true" MinChars="1" TriggerAction="Query">
                                                <Store>
                                                    <ext:Store ID="Store3" runat="server" AutoLoad="true">
                                                        <Proxy>
                                                            <ext:AjaxProxy Url="../checkin/Drugs.ashx">
                                                                <ActionMethods Read="POST" />
                                                                <Reader>
                                                                    <ext:JsonReader RootProperty="drugs" TotalProperty="total" />
                                                                </Reader>
                                                            </ext:AjaxProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model3" runat="server">
                                                                <Fields>
                                                                    <ext:ModelField Name="py" />
                                                                    <ext:ModelField Name="drugname" />
                                                                </Fields>
                                                            </ext:Model>
                                                        </Model>
                                                    </ext:Store>
                                                </Store>
                                                <ListConfig LoadingText="寻找中...">
                                                    <ItemTpl ID="ItemTpl1" runat="server">
                                                        <Html>
                                                            <div>
                                                                <h1>{drugname}</h1>
                                                            </div>
                                                        </html>
                                                    </ItemTpl>
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" DataIndex="intake" Header="剂量" Width="100" Cls="x-grid-hd-inner" >
                                        <Editor>
                                            <ext:TextField ID="TextField3" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" DataIndex="medway" Header="使用方法" Width="90" Cls="x-grid-hd-inner" >
                                        <Editor>
                                            <ext:ComboBox ID="cb_medway" runat="server" TypeAhead="true" SelectOnTab="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" DataIndex="freq" Header="频率" Width="110" Cls="x-grid-hd-inner" >
                                        <Editor>
                                            <ext:ComboBox ID="cb_ordfreq" runat="server" TypeAhead="true" SelectOnTab="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="Column7" runat="server" Text="" DataIndex="USED" Header="使用" Width="60" StopSelection="false" Editable="true" />
                                    <ext:Column ID="Column8" runat="server" Flex="1" />
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
