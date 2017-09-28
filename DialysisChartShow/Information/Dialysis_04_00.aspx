<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_04_00.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_04_00" %>

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
                <ext:Panel ID="DetailPanel" runat="server" Region="North" Height="606" Collapsible="false" Border="false" Title="检验项目大分类维护" >
                    <Items>
                        <ext:Container ID="Container1" runat="server" Frame="true" Layout="HBoxLayout" >
                            <Items>
                                <ext:Label ID="Label3" runat="server" Text="" Width="10" />
                                <ext:Label ID="Label1" runat="server" Text="检验分类" cls="blue-large-font" />
                                <ext:Label ID="GAP1" runat="server" Text="" Width="10" />
                                <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="60" >
                                    <Listeners>
                                        <Click Handler="#{Store1}.insert(0, {}); #{GridPanel1}.editingPlugin.startEditByPosition({row:0, column:0});" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Label ID="GAP2" runat="server" Text="" Width="10" />
                                <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Accept" Width="70" Disabled="true" >
                                    <DirectEvents>
                                        <Click OnEvent="cmdSAVE" Before="return #{Store1}.isDirty();">
                                            <ExtraParams>
                                                <ext:Parameter Name="data" Value="#{Store1}.getChangedData()" Mode="Raw" Encode="true" />
                                                <ext:Parameter Name="data2" Value="#{Store1}.getRecordsValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Label ID="GAP3" runat="server" Text="" Width="10" />
                                <ext:Button ID="btnCancel" runat="server" Text="取消" Icon="Cancel" Width="70" OnDirectClick="cmdCANCEL" Disabled="true" >
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
                                <ext:GridPanel ID="GridPanel1" runat="server" StripeRows="true" TrackMouseOver="true" Region="Center" Height="534"  AutoExpandColumn="GROUP_CLASS" >
                                    <Store>
                                        <ext:Store ID="Store1" runat="server" ItemID="RESULT_CODE" > 
                                            <Model>
                                                <ext:Model ID="Model1" runat="server" Name="RESULT" >
                                                    <Fields>
                                                        <ext:ModelField Name="GROUP_CODE" />
                                                        <ext:ModelField Name="GROUP_NAME" />
                                                        <ext:ModelField Name="GROUP_NAME_E" />
                                                        <ext:ModelField Name="GROUP_CLASS" />
                                                        <ext:ModelField Name="USED" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                            <Sorters>
                                                <ext:DataSorter Property="GROUP_CLASS" Direction="ASC" />
                                            </Sorters>
                                            <Listeners>
                                                <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                            </Listeners>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel1" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="colGROUP_CODE" runat="server" DataIndex="GROUP_CODE" Width="80" Cls="x-grid-hd-inner" Text="大分类">
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="false" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="colGROUP_NAME" runat="server" DataIndex="GROUP_NAME" Width="110" Cls="x-grid-hd-inner" Text="分类名称" >
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="false" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="colGROUP_NAME_E" runat="server" DataIndex="GROUP_NAME_E" Width="110" Cls="x-grid-hd-inner" Text="英文名称" >
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="true" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:Column ID="colGROUP_CLASS" runat="server" DataIndex="GROUP_CLASS" Width="80" Cls="x-grid-hd-inner" Text="排序">
                                                <Editor>
                                                    <ext:TextField runat="server" AllowBlank="true" />
                                                </Editor>
                                            </ext:Column>
                                            <ext:CheckColumn ID="ColGROUP_USED" Header="使用" runat="server" Text="" DataIndex="USED" Width="40" StopSelection="false" Editable="true" />
                                        </Columns>
                                    </ColumnModel>
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
                                        <ext:GridView ID="GridView1" runat="server" StripeRows="true" />
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
