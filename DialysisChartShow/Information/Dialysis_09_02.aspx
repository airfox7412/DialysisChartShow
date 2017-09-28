<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_02.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>净化过程明细</title>
    <link href="../css/dialysis_01.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:GridPanel ID="GridPanel1" runat="server" Region="West" Width="120" Cls="x-grid-custom">
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
                            <ext:Column ID="Col" runat="server" Text="血液透析日期" DataIndex="Dialysis_date" Width="120" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                            <DirectEvents>
                                <Select OnEvent="RowSelect">
                                    <ExtraParams>
                                        <ext:Parameter Name="Dialysis_date" Value="#{GridPanel1}.getRowsValues({ selectedOnly : true })"
                                            Mode="Raw" Encode="true" />
                                    </ExtraParams>
                                </Select>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
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
