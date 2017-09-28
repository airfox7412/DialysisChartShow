<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Stock_02.aspx.cs" Inherits="Dialysis_Chart_Show.Stock.Dialysis_Stock_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>当日预估材料-领料单</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <link href="../css/grid-r.css" rel="stylesheet"/>
    <script type="text/javascript">
        var template = 'color:{0};';
        var change = function (value, meta, record, row, col) {
            switch (record.get("ivpl_mtyp")) {
                case 'HD':
                    meta.style = Ext.String.format(template, "blue");
                    break;
                case 'HDF':
                    meta.style = Ext.String.format(template, "red");
                    break;
                default:
                    meta.style = Ext.String.format(template, "green");
            }
            return value;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="sTIME" runat="server" />
        <ext:Hidden ID="DailySerialNo" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />       
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items> 
                <ext:Panel ID="DetailPanel" runat="server" Title="库存表" Region="West" Collapsible="false" Header="false" AutoScroll="true" Width="280" Frame="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Resizable="false" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                        <ext:DateField ID="DateField1" runat="server" FieldLabel="日期选择" LabelAlign="Right" LabelWidth="70" ColumnWidth=".1" Format="yyyy-MM-dd">
                                            <DirectEvents>
                                                <Change OnEvent="cmdQuery">
                                                    <EventMask ShowMask="true" Msg="查询中..." MinDelay="300" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:DateField>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server"> 
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="no"/>
                                                <ext:ModelField Name="dyivl_no"/>
                                                <ext:ModelField Name="dyivl_item" />
                                                <ext:ModelField Name="dyivl_qty" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="no" Direction="ASC" />
                                    </Sorters>
                                    <Listeners>
                                        <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                                    </Listeners>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <ext:Column ID="Column11" runat="server" DataIndex="dyivl_no" Header="序" Width="60" />
                                    <ext:Column ID="Column12" runat="server" DataIndex="dyivl_item" Header="耗材名称" Width="150" />
                                    <ext:Column ID="Column13" runat="server" DataIndex="dyivl_qty" Header="数量" Width="70" Align="Right" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel> 
                <ext:Panel ID="PanelR" runat="server" Region="Center" Title="病患清单" AutoScroll="true" Header="false" Frame="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel2" runat="server" Resizable="false" Cls="x-grid-custom-r" Height="600">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server">
                                    <Items>                                       
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
                                        <ext:Button ID="btnDetail" runat="server" Text="病患清单" Icon="Accept" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="btnDetail_Click">
                                                    <EventMask ShowMask="true" Msg="搜集资料中，请稍后..." />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>                                        
                                        <ext:Button ID="btnPrint" runat="server" Text="确认/打印" Icon="Printer" Width="100">
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
                                <ext:Store ID="Store2" runat="server" PageSize="15"> 
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" >
                                            <Fields>
                                                <ext:ModelField Name="NO"/>
                                                <ext:ModelField Name="ivpl_id" />
                                                <ext:ModelField Name="ivpl_date" />
                                                <ext:ModelField Name="ivpl_serialno" />
                                                <ext:ModelField Name="ivpl_patname" />
                                                <ext:ModelField Name="ivpl_iv1" />
                                                <ext:ModelField Name="ivpl_iv2" />
                                                <ext:ModelField Name="ivpl_iv3" />
                                                <ext:ModelField Name="ivpl_iv4" />
                                                <ext:ModelField Name="ivpl_bedno" />
                                                <ext:ModelField Name="ivpl_mtyp" />
                                                <ext:ModelField Name="ivpl_flr" />
                                                <ext:ModelField Name="ivpl_time" />
                                                <ext:ModelField Name="timename" />
                                                <ext:ModelField Name="Dialyzer" />
                                                <ext:ModelField Name="Tube" />
                                                <ext:ModelField Name="Special" />
                                                <ext:ModelField Name="Dialyzer2" />
                                                <ext:ModelField Name="Tube2" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="ivpl_id" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel2" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Header="序" Width="60" />
                                    <ext:Column ID="Column22" runat="server" DataIndex="ivpl_flr" Header="楼层" Width="70" />
                                    <ext:Column ID="Column23" runat="server" DataIndex="ivpl_bedno" Header="床号" Width="70" />
                                    <ext:Column ID="Column21" runat="server" DataIndex="timename" Header="时段" Width="70" />
                                    <ext:Column ID="Column24" runat="server" DataIndex="ivpl_mtyp" Header="机型" Width="70">
                                        <Renderer Fn="change" />
                                    </ext:Column>
                                    <ext:Column ID="Column25" runat="server" DataIndex="ivpl_patname" Header="病患姓名" Width="100" />
                                    <ext:Column ID="Column26" runat="server" DataIndex="Dialyzer" Header="透析器型号" Width="140" />
                                    <ext:Column ID="Column27" runat="server" DataIndex="Tube" Header="管路型号" Width="100" />
                                    <ext:Column ID="Column28" runat="server" DataIndex="Special" Header="特殊材料" Width="100" />
                                    <ext:Column ID="Column4" runat="server" DataIndex="ivpl_iv1" Header="穿刺针" Width="70" />
                                    <ext:Column ID="Column5" runat="server" DataIndex="ivpl_iv2" Header="护理包" Width="70" />
                                    <ext:Column ID="Column6" runat="server" DataIndex="ivpl_iv3" Header="敷贴" Width="70" />
                                    <ext:Column ID="Column7" runat="server" DataIndex="ivpl_iv4" Header="肝素帽" Width="70" />
                                    <ext:Column ID="Column1" runat="server" DataIndex="Dialyzer2" Header="透析器型号2" Width="140" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="Tube2" Header="管路型号2" Width="100" />
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:BufferedRenderer ID="BufferedRenderer1" runat="server" />
                            </Plugins>
                            <View>
                                <ext:GridView ID="GridView1" runat="server" TrackOver="false" />
                            </View>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store2" />
                            </BottomBar>
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