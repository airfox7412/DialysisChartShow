<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Get_Drug.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Get_Drug" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>当日病患预估药品領料单</title>
    <link href="../css/grid.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="sTIME" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="库存表" Region="West" Collapsible="false" Header="false" AutoScroll="true" Width="1000" Frame="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Resizable="false" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:DateField ID="DateField1" runat="server" FieldLabel="日期选择" LabelAlign="Right" LabelWidth="60" Width="190" Format="yyyy-MM-dd">
                                            <DirectEvents>
                                                <Change OnEvent="cmdQuery">
                                                    <EventMask ShowMask="true" Msg="查询中..." MinDelay="300" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:DateField>                                        
                                        <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="30" LabelAlign="Right" Width="120">
                                            <Items>
                                                <ext:ListItem Value=" " Text="全部" />
                                                <ext:ListItem Value="001" Text="上午" />
                                                <ext:ListItem Value="002" Text="下午" />
                                                <ext:ListItem Value="003" Text="晚班" />
                                            </Items>
                                            <DirectEvents>
                                                <Select OnEvent = "cboTIME_Click">
                                                    <EventMask ShowMask="true" Msg="查询中..." MinDelay="300" />
                                                </Select>
                                            </DirectEvents>
                                        </ext:SelectBox> 
                                        <ext:Button ID="btnDetail" runat="server" Text="查询" Icon="Accept" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="btnDetail_Click">
                                                    <EventMask ShowMask="true" Msg="搜集资料中，请稍后..." />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>                                        
                                        <ext:Button ID="btnPrint" runat="server" Text="打印" Icon="Printer" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="btnPrint_Click">
                                                    <EventMask ShowMask="true" Msg="准备打印中，请稍后..." />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>   
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server"> 
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="NO"/>
                                                <ext:ModelField Name="pname" />
                                                <ext:ModelField Name="cnt" />
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
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Header="序号" Width="70" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="pname" Header="名称" Width="300" />
                                    <ext:Column ID="Column3" runat="server" DataIndex="cnt" Header="数量" Width="70" Align="Right" />
                                    <ext:Column ID="Column4" runat="server" Flex="1" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
                <ext:Window ID="PrintWindow" runat="server" Title="" Y="10" Width="900" Height="700" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader6" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>