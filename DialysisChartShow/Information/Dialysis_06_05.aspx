<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_05.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_05" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>入院诊断</title>
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
        <ext:ResourceManager ID="ResourceManager2" runat="server" Theme="Default" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="入院诊断" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="诊断日期" LabelWidth="90" LabelAlign="Right" Format="yyyy-MM-dd" />
                                        <ext:FieldSet ID="FieldSet11" runat="server" Flex="1" Title="入院诊断" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_1" runat="server" FieldLabel="病因" LabelWidth="80" LabelAlign="Right" Width="1000" />
                                                <%--<ext:TextField ID="txt_2" runat="server" FieldLabel="病理" LabelWidth="80" LabelAlign="Right" />--%>
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="功能" LabelWidth="80" LabelAlign="Right" Width="1000" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="并发症" LabelWidth="80" LabelAlign="Right" Width="1000" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="伴发症" LabelWidth="80" LabelAlign="Right" Width="1000" />
                                                <ext:ComboBox ID="txt_6" runat="server" FieldLabel="医生" LabelWidth="80" LabelAlign="Right" ColumnWidth=".2" IndicatorText="*" IndicatorCls="Text-red" Cls="Text-blue"
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
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_clear" runat="server" Icon="Disk" Text="重置" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Clear_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
