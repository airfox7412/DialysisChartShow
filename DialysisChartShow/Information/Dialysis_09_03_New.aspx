<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_03_New.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_03_New" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <link href="../css/dialysis_01.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:GridPanel ID="GridPanel1" runat="server" Region="West" Width="150">
                    <Store>
                        <ext:Store ID="Store1" runat="server">
                            <Model>
                                <ext:Model ID="Model1" runat="server" Name="Symptom">
                                    <Fields>
                                        <ext:ModelField Name="Dialysis_date" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Reader>
                                <ext:ArrayReader />
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:Column ID="Col" runat="server" Text="血液透析日期" DataIndex="Dialysis_date" Width="100" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Simple" />
                        <%--<ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Simple">
                            <DirectEvents>
                                <Select OnEvent="RowSelect2">
                                    <ExtraParams>
                                        <ext:Parameter Name="Dialysis_date" Value="#{GridPanel1}.getRowsValues({ selectedOnly : true })"
                                            Mode="Raw" Encode="true" />
                                    </ExtraParams>
                                </Select>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>--%>
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>                                        
                                <ext:Button ID="btnPrint" runat="server" Text="打印" Icon="Printer" Width="100" UI="Success">
                                    <DirectEvents>
                                        <Click OnEvent="btnPrint_Click">
                                            <EventMask ShowMask="true" Msg="准备打印中，请稍后..." />
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="Ext.encode(#{GridPanel1}.getRowsValues({selectedOnly:true}))" Mode="Raw" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>  
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                </ext:GridPanel>
                <ext:Panel ID="Panel1" runat="server" Region="Center" Border="false" Layout="FitLayout" ColumnWidth="1">
                    <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
