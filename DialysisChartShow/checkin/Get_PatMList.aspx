<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Get_PatMList.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Get_PatMList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>当日病患预估材料領料单</title>
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
                <ext:Panel ID="PanelR" runat="server" Region="Center" Title="病患清单" AutoScroll="true" Header="false" Frame="true">
                    <Items>
                        <ext:GridPanel ID="GridPanel2" runat="server" Resizable="false" Cls="x-grid-custom-r" Height="600">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server">
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
                                        <ext:Button ID="btnDetail" runat="server" Text="查询清单" Icon="Accept" Width="100">
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
                                                <ext:ModelField Name="ivpl_col5" />
                                                <ext:ModelField Name="ivpl_col9" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="ivpl_bedno" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel2" runat="server" >
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Header="序号" Width="70" />
                                    <ext:Column ID="Column22" runat="server" DataIndex="ivpl_flr" Header="楼层" Width="70" />
                                    <ext:Column ID="Column23" runat="server" DataIndex="ivpl_bedno" Header="床号" Width="70" />
                                    <ext:Column ID="Column21" runat="server" DataIndex="timename" Header="时段" Width="70" />
                                    <ext:Column ID="Column24" runat="server" DataIndex="ivpl_mtyp" Header="透析方式" Width="90">
                                        <Renderer Fn="change" />
                                    </ext:Column>
                                    <ext:Column ID="Column25" runat="server" DataIndex="ivpl_patname" Header="病患姓名" Width="100" />
                                    <ext:Column ID="Column26" runat="server" DataIndex="Dialyzer" Header="透析器型号" Width="140" />
                                    <ext:Column ID="Column27" runat="server" DataIndex="ivpl_col5" Header="肝素类型" Width="100" />
                                    <ext:Column ID="Column28" runat="server" DataIndex="ivpl_col9" Header="肝素量" Width="100" />
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