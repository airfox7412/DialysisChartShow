<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_System_05.aspx.cs" Inherits="Dialysis_Chart_Show.Systems.Dialysis_System_05" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>权限设定维护</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        var invalidateOk = function (reason) {
            App.txtPwd1.setValidation(reason);
            App.txtPwd2.setValidation(reason);
            App.txtPwd1.validate();
            App.txtPwd2.validate();

            Ext.MessageBox.show({
                title: '认证错误',
                msg: reason,
                buttons: Ext.MessageBox.OK,
                animateTarget: 'Window1',
                icon: 'Error'
            });
        };

        var handleOk = function () {
            if (App.txtPwd1.toString() == App.txtPwd2.toString()) {
                App.Window1.close();
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="Userid" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="权限设定维护" Region="North" Collapsible="false" Header="false" AutoScroll="true">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <ext:ComboBox ID="ComboBoxGroup" runat="server" FieldLabel="使用授权" LabelWidth="100" LabelAlign="Right" ColumnWidth=".5"  EmptyText="选择一个使用授权" Margins="10 5 10 10" >
                                <DirectEvents>
                                    <Select OnEvent="ChangGroup">
                                        <EventMask ShowMask="true" Msg="读取中" />
                                    </Select>
                                </DirectEvents>
                            </ext:ComboBox> 
                            <ext:Button ID="btnAdd" runat="server" Text="添加" Icon="Add" Width="70" Margins="10 5 10 10" >
                                <Listeners>
                                    <Click Handler="#{btnAdd}.disable(); #{Store1}.insert(0, {}); #{GridPanel2}.editingPlugin.startEditByPosition({row:0, column:0});" />
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
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Height="600" Cls="x-grid-custom" >
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15" > 
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="NO" />
                                                <ext:ModelField Name="acclv_id" />
                                                <ext:ModelField Name="name" />
                                                <ext:ModelField Name="usrnm" />
                                                <ext:ModelField Name="passwd" />
                                                <ext:ModelField Name="type" />
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
                                    <ext:Column ID="Column1" runat="server" DataIndex="acclv_id" Header="代码" Width="80">
                                        <Editor>
                                            <ext:TextField ID="TextField1" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column2" runat="server" DataIndex="name" Header="姓名" Width="150">
                                        <Editor>
                                            <ext:TextField ID="TextField2" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column3" runat="server" DataIndex="usrnm" Header="账号" Width="100">
                                        <Editor>
                                            <ext:TextField ID="TextField3" runat="server" AllowBlank="false" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" DataIndex="" Header="密码" Width="100" RightCommandAlign="false">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Edit" Icon="NoteEdit" Text="********">
                                                <ToolTip Text="修改密码" />
                                            </ext:ImageCommand>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="ChangePwd">
                                                <ExtraParams>
                                                    <ext:Parameter Name="acclv_id" Value="record.data.acclv_id" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" DataIndex="type" Header="使用授权" Width="110">
                                        <Editor>
                                            <ext:ComboBox ID="ComboBox1" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:CheckColumn ID="Col_drg_status" runat="server" Text="" DataIndex="USED" Header="使用" Width="80" StopSelection="false" Editable="true" />
                                    <ext:Column ID="Column6" runat="server" Flex="1" />
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
                <ext:Window ID="Window1" runat="server" Title="修改密码" Width="350" Height="200" Icon="Lock" Closable="false" Resizable="false" Draggable="true" Modal="true" BodyPadding="5" Layout="Form" Hidden="true">
                    <Items>
                        <ext:TextField ID="txtPwd1" runat="server" FieldLabel="新的密码" InputType="Password" AllowBlank="false" BlankText="你的密码是需要的." Text="" EnableKeyEvents="true">
                            <DirectEvents>
                                <KeyPress OnEvent="Next_KeyPress">
                                    <ExtraParams>
                                        <ext:Parameter Name="keynum" Value="e.getKey()" Mode="Raw" />
                                    </ExtraParams>
                                </KeyPress>
                            </DirectEvents>
                        </ext:TextField>
                        <ext:TextField ID="txtPwd2" runat="server" FieldLabel="再次输入密码" InputType="Password" AllowBlank="false" BlankText="你的密码是需要的." Text="" EnableKeyEvents="true">
                            <DirectEvents>
                                <KeyPress OnEvent="Login_KeyPress" Failure="invalidateLogin(result.errorMessage);" ShowWarningOnFailure="false">
                                    <ExtraParams>
                                        <ext:Parameter Name="keynum" Value="e.getKey()" Mode="Raw" />
                                    </ExtraParams>
                                </KeyPress>
                            </DirectEvents>
                        </ext:TextField>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button1" runat="server" Text="确定修改" Icon="Accept">
                            <DirectEvents>
                                <Click OnEvent="Edit_Click">
                                    <EventMask ShowMask="true" Msg="Verifying..." MinDelay="500" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button2" runat="server" Text="放弃修改" Icon="Cancel">
                            <Listeners>
                                <Click Handler="#{Window1}.hide()" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>