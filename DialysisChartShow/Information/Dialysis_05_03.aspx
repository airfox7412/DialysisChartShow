<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_05_03.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_05_03" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病程记录</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="Hidden1" runat="server">
        </ext:Hidden>
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:GridPanel ID="Grid_clinical1_nurse" runat="server" Cls="x-grid-custom">
                    <Store>
                        <ext:Store ID="Store1" runat="server" PageSize="25">
                            <Model>
                                <ext:Model ID="Model1" runat="server">
                                    <Fields>
                                        <ext:ModelField Name="pif_ic" Type="String" />
                                        <ext:ModelField Name="info_date" Type="String" />
                                        <ext:ModelField Name="data_1" Type="String" />
                                        <ext:ModelField Name="data_2" Type="String" />
                                        <ext:ModelField Name="data_3" Type="String" />
                                        <ext:ModelField Name="data_4" Type="String" />
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
                            <ext:Column ID="COL_PIF_IC" runat="server" Text="PIF_IC" DataIndex="pif_ic" Width="100" Hidden=true/>
                            <ext:Column ID="date" runat="server" Text="日期.." DataIndex="info_date" Width="100" />
                            <ext:Column ID="Column3" runat="server" Text="诊断" DataIndex="data_1" Width="80"/>
                            <ext:Column ID="Column4" runat="server" Text="透析方式" DataIndex="data_2" Width="100"/>
                            <ext:Column ID="Column5" runat="server" Text="干體重" DataIndex="data_3" Width="100" Align="Right"/>
                            <ext:Column ID="Column6" runat="server" Text="目標定容量" DataIndex="data_4" Width="150" Align="Right"/>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                            <DirectEvents>
                                <Select OnEvent="RowSelect">
                                    <ExtraParams>
                                        <ext:Parameter Name="Values" Value="App.Grid_clinical1_nurse.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                    </ExtraParams>
                                </Select>
                            </DirectEvents>
                        </ext:RowSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
                <ext:Window ID="WndReport" runat="server" Title="" Width="1000" Height="600" Y="0" Modal="true" AutoRender="false" Collapsible="true" Maximizable="true" Hidden="true" >
                    <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
