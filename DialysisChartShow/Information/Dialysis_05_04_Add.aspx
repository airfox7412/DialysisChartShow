<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_05_04_Add.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_05_04_Add" %>

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
        <%--<ext:Hidden ID="Doct_Name" runat="server" />--%>

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
        <ext:Panel ID="Panel1" runat="server" ButtonAlign="Center" Title="查房记录">
            <Items>
                <ext:Container ID="Container21" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:DateField ID="PAT_DATE" runat="server" FieldLabel="查房日期" Format="yyyy-MM-dd" LabelWidth="100" LabelAlign="Right" Width="200" />
                        <ext:ComboBox ID="Doct_Name" runat="server" FieldLabel="查房人员" IndicatorText="*" LabelWidth="100" LabelAlign="Right" Width="200" 
                            DisplayField="acclv_fname" ValueField="acclv_fname" TypeAhead="false" HideBaseTrigger="true" MinChars="1" TriggerAction="Query" PageSize="10">
                            <Store>
                                <ext:Store ID="Store1" runat="server" AutoLoad="true">
                                    <Proxy>
                                        <ext:AjaxProxy Url="../Doctors.ashx">
                                            <ActionMethods Read="POST" />
                                            <Reader>
                                                <ext:JsonReader RootProperty="Doctors" TotalProperty="total" />
                                            </Reader>
                                        </ext:AjaxProxy>
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
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
    </form>
</body>
</html>