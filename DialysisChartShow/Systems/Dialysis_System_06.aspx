<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_System_06.aspx.cs" Inherits="Dialysis_Chart_Show.Systems.Dialysis_System_06" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病区/床号维护</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        var template = 'color:{0};';
        var change = function (value, meta, record, row, col) {
            if (record.get("kind") == true) {
                meta.style = Ext.String.format(template, "red");
            }
            else{
                meta.style = Ext.String.format(template, "green");
            }
            return value;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="病区/床号维护" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <ext:TextField ID="Authorize" runat="server" FieldLabel="总床位数" LabelWidth="100" LabelAlign="Right" LabelCls="label_blue" Width="150" ReadOnly="true" />
                                <ext:TextField ID="NowBed" runat="server" FieldLabel="目前总床位数" LabelWidth="100" LabelAlign="Right" LabelCls="label_red" Width="150" ReadOnly="true" />
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
                        <ext:GridPanel ID="GridPanel1" runat="server" Height="600" Cls="x-grid-custom" >
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15" > 
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="NO" Type="Int" />
                                                <ext:ModelField Name="mac_id" Type="Int" />
                                                <ext:ModelField Name="mac_flr" />
                                                <ext:ModelField Name="mac_sec" />
                                                <ext:ModelField Name="mac_bedno" />
                                                <ext:ModelField Name="mac_typ" />
                                                <ext:ModelField Name="mac_com" />
                                                <ext:ModelField Name="brand" />
                                                <ext:ModelField Name="USED" />
                                                <ext:ModelField Name="kind" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="mac_id" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Header="序" Width="60" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="mac_flr" Header="楼层" Width="80">
                                        <Editor>
                                            <ext:TextField ID="TextField1" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column3" runat="server" DataIndex="mac_sec" Header="区" Width="60">
                                        <Renderer Fn="change" />
                                        <Editor>
                                            <ext:TextField ID="TextField2" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="Col_kind" runat="server" Text="" DataIndex="kind" Header="阳" Width="60" StopSelection="false" Editable="true" />
                                    <ext:Column ID="Column4" runat="server" DataIndex="mac_bedno" Header="床号" Width="80">
                                        <Editor>
                                            <ext:TextField ID="TextField3" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" DataIndex="brand" Header="品牌" Width="200">
                                        <Editor>
                                            <ext:ComboBox ID="Cb_brand" runat="server" TypeAhead="true" SelectOnTab="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" DataIndex="mac_typ" Header="机器" Width="80">
                                        <Editor>
                                            <ext:TextField ID="TextField5" runat="server" AllowBlank="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column7" runat="server" DataIndex="mac_com" Header="Com" Width="80">
                                        <Editor>
                                            <ext:TextField ID="TextField6" runat="server" AllowBlank="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="Col_status" runat="server" Text="" DataIndex="USED" Header="使用" Width="80" StopSelection="false" Editable="true" />
                                    <ext:Column ID="Column8" runat="server" Width="20">
                                        <Commands>
                                            <ext:ImageCommand CommandName="mac_Del" Icon="Cancel">
                                                <ToolTip Text="删除" />
                                            </ext:ImageCommand> 
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="mac_Delete">
                                                <Confirmation ConfirmRequest="true" Title="请确认" Message="删除后不可恢复，确定删除?" />
                                                <EventMask ShowMask="true" Target="Page" />
                                                <ExtraParams>
                                                    <ext:Parameter Name="macid" Value="record.data.mac_id" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents> 
                                    </ext:Column>
                                    <ext:Column ID="Column9" runat="server" Flex="1" />
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