<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drug_mod.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.drug_mod" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>医嘱用药模板维护</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <style type="text/css">    
    .x-boundlist-item
    {
        color:Blue;
        font-size:11px;
    }
    </style>
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
                                        <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="100" Margins="10 5 10 10" >
                                            <Listeners>
                                                <Click Handler="#{btnAdd}.disable(); #{Store1}.insert(0, {}); #{GridPanel1}.editingPlugin.startEditByPosition({row:0, column:0});" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="btnSave" runat="server" Text="保存" Icon="Accept" Width="100" Disabled="true" Margins="10 5 10 10" >
                                            <DirectEvents>
                                                <Click OnEvent="cmdSAVE" Before="return #{Store1}.isDirty();">
                                                    <EventMask ShowMask="true" Msg="保存中..." />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="data" Value="#{Store1}.getChangedData()" Mode="Raw" Encode="true" />
                                                        <ext:Parameter Name="data2" Value="#{Store1}.getRecordsValues()" Mode="Raw" Encode="true" />
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
                                <ext:Store ID="Store1" runat="server" PageSize="10" >
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="sid">
                                            <Fields>
                                                <ext:ModelField Name="NO"/>
                                                <ext:ModelField Name="sid" />
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
                                        <ext:DataSorter Property="NO" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn runat="server" Header="序" Width="70" />
                                    <ext:Column ID="drg_code" runat="server" DataIndex="drg_name" Header="名称" Width="200">
                                        <Editor>
                                            <ext:ComboBox ID="ComboBoxDrug" runat="server" TypeAhead="true" SelectOnTab="true" 
                                                DisplayField="drugname" ValueField="drugname" PageSize="15"  
                                                HideBaseTrigger="true" MinChars="1" TriggerAction="Query">
                                                <Store>
                                                    <ext:Store ID="Store2" runat="server" AutoLoad="true">
                                                        <Proxy>
                                                            <ext:AjaxProxy Url="Drugs.ashx">
                                                                <ActionMethods Read="POST" />
                                                                <Reader>
                                                                    <ext:JsonReader RootProperty="drugs" TotalProperty="total" />
                                                                </Reader>
                                                            </ext:AjaxProxy>
                                                        </Proxy>
                                                        <Model>
                                                            <ext:Model ID="Model2" runat="server">
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
                                                            <div>{drugname}</div>
                                                        </html>
                                                    </ItemTpl>
                                                </ListConfig>
                                            </ext:ComboBox>
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="intake" runat="server" DataIndex="intake" Header="剂量" Width="100">
                                        <Editor>
                                            <ext:TextField ID="TextField3" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="medway" runat="server" DataIndex="medway" Header="使用方法" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cb_medway" runat="server" TypeAhead="true" SelectOnTab="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="freq" runat="server" DataIndex="freq" Header="频率" Width="100">
                                        <Editor>
                                            <ext:ComboBox ID="cb_ordfreq" runat="server" TypeAhead="true" SelectOnTab="true" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="Col_drg_status" runat="server" DataIndex="USED" Header="使用" StopSelection="false" Editable="true" Width="70" />
                                    <ext:Column ID="Column1" runat="server" DataIndex="" Header="" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:CellSelectionModel runat="server">
                                    <Listeners>
                                        <Select Handler="#{btnSave}.enable();#{btnCancel}.enable();#{btnDelete}.enable();" />
                                    </Listeners>
                                </ext:CellSelectionModel>
                            </SelectionModel>
                            <Plugins>
                                <ext:CellEditing runat="server" ClicksToEdit="1" />
                            </Plugins>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel> 
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
