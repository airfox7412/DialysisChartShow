<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_04_01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_04_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>實驗室檢驗項目維護</title>
    <style type = "text/css">
    <%--panel head 自动--%>
    .x-panel-header-text {
        font-size: 16px;
        font-weight: bold;
        line-height: 20px;
    }
    <%--cell字型大小  自动 ?--%>
    .x-grid-row .x-grid-cell { 
        font-size: 13px;
    }
    <%--grid column head  手动Cls="x-grid-hd-inner"--%>
    .x-grid-hd-inner {
        font-size: 12px;
        font-weight: bold;
    }
    <%--grid column 上色  手动tdCls="custom-column"--%>
    .x-grid-row .custom-column { 
        font-weight: bold;
    }
    
    <%--tree node text size 手动Cls="large-font"--%>
    .large-font 
    {
        font-size: 16px !important; 
        height: 22px !important;
    }
    .blue-large-font 
    {
        font-size: 16px !important; 
        height: 22px !important;
        color: blue !important;
    }
    
    .x-status-text 
    {
        font-size:18px !important;
        color: red !important;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="border">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Region="North" Height="606" Collapsible="false" Border="false" Title="检验项目小分类维护" >
                    <Items>
                        <ext:Container ID="Container5" runat="server" Frame="true" Layout="HBoxLayout" >
                            <Items>
                                <ext:Label ID="Label3" runat="server" Text="" Width="10" />
                                <ext:Label ID="Label1" runat="server" Text="大分类" cls="blue-large-font" />
                                <ext:ComboBox ID="ComboBoxGroup" runat="server" DisplayField="GROUP_NAME" EmptyText="选择一个分类" >                                    
                                    <Store>
                                        <ext:Store ID="Store1" runat="server">
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="GROUP_NAME" />
                                                        <ext:ModelField Name="GROUP_CODE" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>            
                                        </ext:Store>    
                                    </Store> 
                                    <DirectEvents>
                                        <Select OnEvent="ChangGroup" />
                                    </DirectEvents>
                                </ext:ComboBox> 
                                <ext:Label ID="GAP1" runat="server" Text="" Width="10" />
                                <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="60" >
                                    <Listeners>
                                        <Click Handler="#{btnAdd}.disable(); #{Store2}.insert(0, {}); #{GridPanel2}.editingPlugin.startEditByPosition({row:0, column:0});" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Label ID="GAP2" runat="server" Text="" Width="10" />
                                <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Accept" Width="70" Disabled="true" >
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
                                <ext:Label ID="GAP3" runat="server" Text="" Width="10" />
                                <ext:Button ID="btnCANCEL" runat="server" Text="取消" Icon="Cancel" Width="70" Disabled="true" >                                
                                    <DirectEvents>
                                        <Click OnEvent="cmdCANCEL">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>  
                                <ext:Label ID="GAP4" runat="server" Text="" Width="10" />                                        
                                <ext:Button ID="btnDelete" runat="server" Text="删除" Icon="Delete" Width="60" Disabled="true">
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
                        </ext:Container> 
                    </Items>
                    <Items>
                        <ext:Panel ID="PanelM" runat="server" Region="Center" Border="false" >
                            <Items>
                                <ext:GridPanel ID="GridPanel1" runat="server" StripeRows="true" TrackMouseOver="true" Height="534" AutoExpandColumn="RITEM_CLASS" >
                                    <Store>
                                        <ext:Store ID="Store2" runat="server" ItemID="RESULT_CODE" > 
                                            <Model>
                                                <ext:Model ID="Model2" runat="server" Name="RESULT" >
                                                    <Fields>
                                                        <ext:ModelField Name="NO" Type="Int" />
                                                        <ext:ModelField Name="RITEM_CLASS" />
                                                        <ext:ModelField Name="RITEM_CODE" />
                                                        <ext:ModelField Name="RITEM_TYPE" />
                                                        <ext:ModelField Name="RITEM_NAME_S" />
                                                        <ext:ModelField Name="RITEM_NAME" />
                                                        <ext:ModelField Name="RITEM_UNIT" />
                                                        <ext:ModelField Name="USED" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                            <Sorters>
                                                <ext:DataSorter Property="RITEM_CLASS" Direction="ASC" />
                                            </Sorters>
                                            <Listeners>
                                                <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel1" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="colRITEM_CLASS" Header="大分类" runat="server" DataIndex="RITEM_CLASS" Width="50" Cls="x-grid-hd-inner" Hidden="true" />
                                            <ext:Column ID="colNO" Header="序号" runat="server" DataIndex="NO" Width="50" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="colRESULT_CODE" runat="server" DataIndex="RITEM_CODE" Width="80" Cls="x-grid-hd-inner" Text="检验代码">
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="false" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="colRITEM_TYPE" runat="server" DataIndex="RITEM_TYPE" Width="80" Cls="x-grid-hd-inner" Text="小分类" >
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="false" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="colRITEM_NAME" Header="检验简称" runat="server" DataIndex="RITEM_NAME_S" Width="110" Cls="x-grid-hd-inner" >
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="false" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="colRITEM_NAME_S" Header="检验名称" runat="server" DataIndex="RITEM_NAME" Width="110" Cls="x-grid-hd-inner" >
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="true" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="ColRITEM_UNIT" Header="检验单位" runat="server" DataIndex="RITEM_UNIT" Width="90" Cls="x-grid-hd-inner" >
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="true" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn ID="ColRITEM_USED" Header="使用" runat="server" Text="" DataIndex="USED" Width="40" StopSelection="false" Editable="true" />
                                        </Columns>
                                    </ColumnModel>
                                    <%--<BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar1" 
                                            runat="server"                      
                                            DisplayInfo="true"
                                            DisplayMsg="显示 分类明细 {0} - {1} of {2}"
                                            EmptyMsg="没有 分类明细 可显示"                
                                            />
                                    </BottomBar>--%>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Multi">
                                            <Listeners>
                                                <Select Handler="#{btnSave}.enable();#{btnCancel}.enable();#{btnDelete}.enable();" />
                                                <Deselect Handler="if (!#{GridPanel1}.selModel.hasSelection()) {
                                                                       #{btnDelete}.disable();
                                                                   }" />
                                            </Listeners>
                                        </ext:RowSelectionModel>
                                    </SelectionModel>
                                    <View>
                                        <ext:GridView ID="GridView1" runat="server" StripeRows="true" >                   
                                        </ext:GridView>
                                    </View>
                                    <Plugins>
                                        <ext:CellEditing ID="CellEditing1" runat="server" />
                                    </Plugins>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel> 
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
