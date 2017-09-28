<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_05_04.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_05_04" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>查房记录</title>
     <script type="text/javascript">
         var enterKeyPressHandler = function (f, e) {
             if (e.getKey() == e.ENTER) {
                 Ext.Net.DirectMethods.show();
                 e.stopEvent();
             }
         };
    </script>
</head>
<body>
    <form id="form1" runat="server">    
        <ext:Hidden ID="sid" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Panel ID="Panel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="查房记录" BodyStyle="background-color:#EBF5FF !important;">
            <Items>
                <ext:GridPanel ID="GridPanel1" runat="server" >
                    <TopBar>
                        <ext:Toolbar ID="Toolbar2" runat="server">
                            <Items>
                                <ext:DateField ID="start_date" runat="server" FieldLabel="查房日期" Format="yyyy-MM-dd" LabelWidth="60" Width="200" Visible="true" />
                                <ext:DateField ID="end_date" runat="server" FieldLabel="～" Format="yyyy-MM-dd" LabelWidth="10" Width="150" Visible="true" />
                                <ext:ComboBox ID="Doct_Name" runat="server" FieldLabel="医生" LabelWidth="50" ColumnWidth=".2" IndicatorText="*" IndicatorCls="Text-red" Cls="Text-blue" LabelAlign="Right"
                                    DisplayField="acclv_fname" ValueField="acclv_fname" TypeAhead="false" PageSize="20" HideBaseTrigger="true" MinChars="1" TriggerAction="Query" PaddingSpec="3 20 0 0">
                                    <Store>
                                        <ext:Store ID="Store2" runat="server" AutoLoad="true">
                                            <Proxy>
                                                <ext:AjaxProxy Url="../Doctors.ashx">
                                                    <ActionMethods Read="POST" />
                                                    <Reader>
                                                        <ext:JsonReader RootProperty="Doctors" TotalProperty="total" />
                                                    </Reader>
                                                </ext:AjaxProxy>
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="Model2" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="acclv_id" />
                                                        <ext:ModelField Name="acclv_fname" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ListConfig LoadingText="寻找中...">
                                        <ItemTpl ID="ItemTpl1" runat="server">
                                            <Html>
                                                <div>
                                                    <h1>{acclv_fname}</h1>
                                                </div>
                                            </html>
                                        </ItemTpl>
                                    </ListConfig>
                                    <Listeners>
                                        <KeyDown Fn="enterKeyPressHandler" />
                                    </Listeners>
                                </ext:ComboBox>
                                <ext:Button ID="BtnQuery" runat="server" Text="查詢" Icon="Find" Cls="Text-blue16" AutoPostBack="false" Visible="true">
                                    <DirectEvents>
                                        <Click OnEvent="BtnQuery_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="BtnPrint" runat="server" Text="打印" Icon="Printer" Cls="Text-blue16" AutoPostBack="false" Visible="true">
                                    <DirectEvents>
                                        <Click OnEvent="BtnPrint_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="BtnAddS" runat="server" Text="增加" Icon="ApplicationFormEdit" Cls="Text-blue16" AutoPostBack="false">
                                    <DirectEvents>
                                        <Click OnEvent="BtnAdd_Click" />
                                    </DirectEvents>
                                </ext:Button>
                               <%-- <ext:Button ID="Button5" runat="server" Text="删除" Icon="FolderMagnify" IconCls="Text-green16">
                                    <DirectEvents>
                                        <Click OnEvent="BtnDel_Click" />
                                    </DirectEvents>
                                </ext:Button>--%>
                                <ext:ToolbarFill ID="ToolbarFill2" runat="server" />
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Store>
                        <ext:Store ID="Store1" runat="server" PageSize="25">
                            <Model>
                                <ext:Model ID="Model1" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="sid" Type="Int" />
                                        <ext:ModelField Name="pif_ic" />
                                        <ext:ModelField Name="pat_date" />
                                        <ext:ModelField Name="pat_time" />
                                        <ext:ModelField Name="pat_note" />
                                        <ext:ModelField Name="pat_emp" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Reader>
                                <ext:ArrayReader />
                            </Reader>
                            <Sorters>
                                <ext:DataSorter Property="common" Direction="ASC" />
                            </Sorters>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:Column ID="pat_date" runat="server" Text="日期" DataIndex="pat_date" Width="100" />
                            <ext:Column ID="pat_time" runat="server" Text="时间" DataIndex="pat_time" Width="80" />
                            <ext:Column ID="pat_note" runat="server" Text="查房内容" DataIndex="pat_note" Width="300" />
                            <ext:Column ID="pat_emp" runat="server" Text="处置人员" DataIndex="pat_emp" Width="80" />
                            <ext:Column ID="Edit" runat="server" Text="修改" Width="50">
                                <Commands>
                                    <ext:ImageCommand CommandName="Edit" Icon="Pencil" />
                                </Commands>
                                <DirectEvents>
                                    <Command OnEvent="Do_Edit">
                                        <ExtraParams>
                                            <ext:Parameter Name="sid" Value="record.data.sid" Mode="Raw"/>
                                        </ExtraParams>
                                    </Command>
                                </DirectEvents>
                            </ext:Column>
                            <ext:Column ID="Del" runat="server" Text="删除" Width="50">
                                <Commands>
                                    <ext:ImageCommand CommandName="Delete" Icon="Cancel" />
                                </Commands>
                                <DirectEvents>
                                    <Command OnEvent="Do_Delete">
                                        <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要删除么？" />
                                        <ExtraParams>
                                            <ext:Parameter Name="sid" Value="record.data.sid" Mode="Raw"/>
                                        </ExtraParams>
                                    </Command>
                                </DirectEvents>
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
        <ext:Window ID="PrintWindow" runat="server" Title="" Width="900" Height="700" Modal="true" AutoRender="false" Hidden="true">
            <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
        <ext:Window ID="Window1" runat="server" Title="查房记录" Width="900" Height="350" Modal="true" AutoRender="false" Hidden="true">
            <Items>
                <ext:Panel ID="FormPanel1" runat="server" ButtonAlign="Center" Header="false">
                    <Items>
                        <ext:Container ID="Container21" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                            <Items>
                                <ext:DateField ID="PAT_DATE2" runat="server" FieldLabel="查房日期" Format="yyyy-MM-dd" LabelWidth="100" LabelAlign="Right" Width="250" />
                                <ext:ComboBox ID="Doct_Name2" runat="server" FieldLabel="查房人员" IndicatorText="*" LabelWidth="100" LabelAlign="Right" Width="250" 
                                    DisplayField="acclv_fname" ValueField="acclv_fname" TypeAhead="false" HideBaseTrigger="true" MinChars="1" TriggerAction="Query" PageSize="10">
                                    <Store>
                                        <ext:Store ID="Store3" runat="server" AutoLoad="true">
                                            <Proxy>
                                                <ext:AjaxProxy Url="../Doctors.ashx">
                                                    <ActionMethods Read="POST" />
                                                    <Reader>
                                                        <ext:JsonReader RootProperty="Doctors" TotalProperty="total" />
                                                    </Reader>
                                                </ext:AjaxProxy>
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="Model3" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="acclv_id" />
                                                        <ext:ModelField Name="acclv_fname" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <ListConfig LoadingText="寻找中...">
                                        <ItemTpl ID="ItemTpl2" runat="server">
                                            <Html>
                                                <div>
                                                    <h1>{acclv_fname}</h1>
                                                </div>
                                            </html>
                                        </ItemTpl>
                                    </ListConfig>
                                    <Listeners>
                                        <KeyDown Fn="enterKeyPressHandler" />
                                    </Listeners>
                                </ext:ComboBox>
                            </Items> 
                        </ext:Container>
                        <ext:Container ID="Container23" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                            <Items>
                                <ext:TextArea ID="TextArea1" runat="server" FieldLabel="查房记录" LabelWidth="100" ColumnWidth=".8" LabelAlign="Right" PaddingSpec="10 50 0 2" Height="200" Flex="1" />
                            </Items> 
                        </ext:Container>
                        <ext:Container ID="Container24" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                            <Items> 
                                <ext:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Styles/save.png" Height="50" ColumnWidth="1" OverImageUrl="~/Styles/saveover.png" >
                                    <DirectEvents>
                                        <Click OnEvent="Btn_save_Click" />
                                    </DirectEvents>
                                </ext:ImageButton>
                            </Items> 
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Window>
    </form>
</body>
</html>
