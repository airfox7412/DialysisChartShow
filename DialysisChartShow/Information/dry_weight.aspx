<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dry_weight.aspx.cs" Inherits="Dialysis_Chart_Show.Information.dry_weight" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>干体重</title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <%--<ext:Panel ID="Panel1" runat="server" Region="North" Width="800" MinWidth="200" Border="false" MaxWidth="1024" Cls="color_title" Hidden="true" >
                    <Items>
                        <ext:Container ID="Container5" runat="server" Frame="true" Layout="ColumnLayout" >
                            <Items>
                                <ext:TextField ID="txt_year" LabelWidth="50" runat="server" FieldLabel="年月" Width="100" LabelCls="my-Field" />
                                <ext:SelectBox ID="cmb_month" runat="server" FieldLabel="月" LabelCls="my-Field" Cls="my-TextField" LabelWidth="100" LabelAlign="Right" > 
                                    <Items>
                                        <ext:ListItem Text=" ----------- " Value="99" />
                                        <ext:ListItem Text="一月" Value="01" />
                                        <ext:ListItem Text="二月" Value="02" />
                                        <ext:ListItem Text="三月" Value="03" />
                                        <ext:ListItem Text="四月" Value="04" />
                                        <ext:ListItem Text="五月" Value="05" />
                                        <ext:ListItem Text="六月" Value="06" />
                                        <ext:ListItem Text="七月" Value="07" />
                                        <ext:ListItem Text="八月" Value="08" />
                                        <ext:ListItem Text="九月" Value="09" />
                                        <ext:ListItem Text="十月" Value="10" />
                                        <ext:ListItem Text="十一月" Value="11" />
                                        <ext:ListItem Text="十二月" Value="12" />
                                    </Items>          
                                </ext:SelectBox>              
                                <ext:Button ID="btn_getlist" runat="server" Text="搜寻" ColumnWidth=".1" >
                                    <DirectEvents>
                                        <Click OnEvent="display_weightlist" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>--%>
                    
                <ext:Panel ID="Panel4" runat="server" Region="West" Border="true" Width="200" AutoScroll="true">
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server" Border="false" Region="Center" Title="干体重" Layout="FitLayout">
                            <DirectEvents>
                                <ItemClick OnEvent="Node_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                        <ext:Parameter Name="rTEXT" Value="record.data.text" Mode="Raw" />
                                    </ExtraParams>
                                </ItemClick>
                            </DirectEvents>
                        </ext:TreePanel> 
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel3" runat="server" Region="Center" Border="false" Layout="FitLayout" ColumnWidth="1" >
                    <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
