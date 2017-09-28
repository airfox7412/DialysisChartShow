<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_K01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_K01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自订条件统计</title>
    <script type="text/javascript">

        var saveData15 = function () {
            App.Hidden15.setValue(Ext.encode(App.GridPanel15.getRowsValues({ selectedOnly: false })));
        };

        var exportChart = function (btn) {
            Ext.MessageBox.confirm('确认下载', '图表下载为图像 ?', function (choice) {
                if (choice == 'yes') {
                    var data = btn.up('panel').down('chart').getImage().data;
                    App.direct.Download(data, { isUpload: true });
                }
            });
        };

        var saveChart = function (btn) {
            Ext.MessageBox.confirm('确认下载', '图表下载为图像 ?', function (choice) {
                if (choice == 'yes') {
                    btn.up('panel').down('polar').download();
                }
            });
        };
        var tipRenderer = function (toolTip, record, context) {
            var total = 0;

            App.Chart1.getStore().each(function (rec) {
                total += rec.get('PIE_DATA');
            });

            toolTip.setTitle(Math.round(record.get('PIE_DATA') / total * 100) + '%');
        };
  </script>
</head>
<body>
    <form id="form1" runat="server">

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />     
<%--        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>--%>
                <ext:FormPanel ID="FormPanel_11" runat="server" Border="false" Region="Center" ColumnWidth="1" Height="900">
                    <Items>
                        <ext:Panel ID="Panel_12" runat="server" Border="false" Region="Center">
                            <Items>
                                <ext:Container ID="Container4" runat="server" Frame="true" Layout="ColumnLayout" Padding="5" >
                                    <Items>
                                        <ext:ComboBox ID="ComboBoxGroup" runat="server" FieldLabel="大分类" LabelWidth="80" DisplayField="GROUP_NAME" LabelAlign="Right" EmptyText="选择一个分类" MarginSpec="4 10 4 10" Hidden="true">
                                            <DirectEvents>
                                                <Select OnEvent="ChangGroup" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cboCODE12" runat="server" FieldLabel="检查名称" EmptyText="未选择" LabelWidth="70" LabelAlign="Right" Width="250" MarginSpec="5 10 5 10">
                                            <DirectEvents>
                                                <Change OnEvent="cboCODE12_Click">
                                                    <EventMask ShowMask="true" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtRESULT_CODE12" runat="server" FieldLabel="检查代码" LabelAlign="Right" LabelWidth="70" Width="150" MarginSpec="5 10 5 10" ReadOnly="true" Hidden="true" />
                                        <ext:TextField ID="txtNORMAL12" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" LabelWidth="100" MarginSpec="5 10 5 10" Width="250" ReadOnly="true" />
                                        <ext:Button ID="btn_Query12" runat="server" Icon="Find" Text="查询" Width="90" MarginSpec="5 10 5 10">
                                            <DirectEvents>
                                                <Click OnEvent="btn_Query12_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                       <ext:Button ID="Button2" runat="server" Icon="ChartBar" Text="条件设定" Width="110" MarginSpec="5 10 5 10">
                                            <DirectEvents>
                                                <Click OnEvent="btn_Query2_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Container>
                                <ext:Hidden ID="Hidden15" runat="server" />
                                <ext:GridPanel ID="GridPanel15" runat="server" Title="比例图" Region="Center" Height="250" Width="800" Collapsible="true">                                       
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar15" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill15" runat="server" />
                                                <ext:Button ID="Button15a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_15" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData15" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button15b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_15" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData15" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button15c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_15" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData15" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Store>
                                        <ext:Store ID="Store15" runat="server" PageSize="10">
                                            <Model>
                                                <ext:Model ID="Model15" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" Type="String" />
                                                        <ext:ModelField Name="身份证号" Type="String" />
                                                        <ext:ModelField Name="性别" Type="String" />
                                                        <ext:ModelField Name="病历号" Type="Int" />
                                                        <ext:ModelField Name="检验日期" Type="String" />
                                                        <ext:ModelField Name="检验代码" Type="String" />
                                                        <ext:ModelField Name="检验名称" Type="String" />
                                                        <ext:ModelField Name="检验结果" Type="Float" />
                                                        <ext:ModelField Name="RITEM_LOW" Type="Float" />
                                                        <ext:ModelField Name="RITEM_HIGH" Type="Float" />
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
                                    <ColumnModel ID="ColumnModel15" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column103" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column104" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column105" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column106" Header="病历号" runat="server" DataIndex="病历号" Width="60" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column107" Header="检验日期" runat="server" DataIndex="检验日期" Width="100" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column108" Header="检验代码" runat="server" DataIndex="检验代码" Width="80" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column109" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner"  />
                                            <ext:NumberColumn ID="Column110" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" Align="Right">
                                                <%--<Commands>
                                                    <ext:ImageCommand CommandName="ZoomIn" Icon="ZoomIn" Style="margin-left:5px !important;" >
                                                        <ToolTip Text="查询" />
                                                    </ext:ImageCommand>
                                                </Commands>
                                                <DirectEvents>
                                                    <Command OnEvent="Find_IC" >
                                                        <ExtraParams>
                                                            <ext:Parameter Name="PAT_IC" Value="record.data.身份证号" Mode="Raw"/>
                                                        </ExtraParams> 
                                                    </Command> 
                                                </DirectEvents>--%>
                                            </ext:NumberColumn>
                                        </Columns>
                                    </ColumnModel>  
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar15" runat="server" StoreID="Store15" />
                                    </BottomBar>
                                </ext:GridPanel>
                                <ext:Panel ID="PanelChart" runat="server" InnerPadding="0 40 0 40" Width="800" Height="600" Layout="FitLayout">
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar26" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                                <ext:Button ID="Button1" runat="server" Text="保存图像" Icon="Disk" Handler="saveChart" />
                                                <ext:Button ID="Button4" runat="server" Text="甜甜圈" EnableToggle="true">
                                                    <Listeners>
                                                        <Toggle Handler="App.Chart1.series[0].setDonut(pressed ? 35 : false); App.Chart1.redraw();" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Items>
                                        <ext:PolarChart ID="Chart1" runat="server" StandardTheme="Category1" Shadow="true" InsetPadding="50" InnerPadding="50">
                                            <Background Fill="white" />
                                            <AnimationConfig Duration="500" Easing="EaseIn" />
                                            <LegendConfig ID="LegendConfig1" runat="server" Dock="Right" />
                                            <Store>
                                                <ext:Store ID="Store1" runat="server" AutoDataBind="true">
                                                    <Model>
                                                        <ext:Model ID="Model1" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="PIE_NAME" />
                                                                <ext:ModelField Name="PIE_DATA" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <Interactions>
                                                <ext:RotatePie3DInteraction />
                                            </Interactions>
                                            <Series>
                                                <%--<ext:PieSeries AngleField="PIE_DATA" ShowInLegend="true" Donut="0" HighlightMargin="20">
                                                    <Label Field="PIE_NAME" Display="Rotate" FontSize="18" FontFamily="Arial" />
                                                    <Tooltip ID="Tooltip1" runat="server" TrackMouse="true" Width="140" Height="28">
                                                        <Renderer Fn="tipRenderer" />
                                                    </Tooltip>
                                                </ext:PieSeries>--%>
                                                <ext:Pie3DSeries AngleField="PIE_DATA" Donut="0" Distortion="0.5">
                                                    <Label Field="PIE_NAME" Display="Rotate" FontSize="18" FontFamily="Arial" />
                                                    <Tooltip ID="Tooltip1" runat="server" TrackMouse="true" Width="50" Height="20">
                                                        <Renderer Fn="tipRenderer" />
                                                    </Tooltip>
                                                    <StyleSpec>
                                                        <ext:SeriesSprite StrokeStyle="white" Opacity="0.9" />
                                                    </StyleSpec>
                                                </ext:Pie3DSeries>
                                            </Series>
                                        </ext:PolarChart>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel> 
                <ext:Window ID="Window1" runat="server" Title="条件设定" Width="600" Height="330" Modal="true" Hidden="true" CloseAction="Hide" UI="Success" Resizable="false">
                    <Items>
                        <ext:FormPanel ID="FormPanel1" runat="server"> 
                            <Items>                   
                                <ext:GridPanel ID="GridList" runat="server" Cls="x-grid-custom" AutoScroll="true">
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar14" runat="server">
                                            <Items>
                                                <ext:Button ID="btnAdd" runat="server" Text="新增条件" Icon="New" Width="100" UI="Success">
                                                    <DirectEvents>
                                                        <Click OnEvent="btnAdd_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Store>
                                        <ext:Store ID="Store17" runat="server" PageSize="10">
                                        <Model>
                                            <ext:Model ID="Model17" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="sn" />
                                                    <ext:ModelField Name="low_condition" />
                                                    <ext:ModelField Name="high_condition" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                        <Reader>
                                            <ext:ArrayReader />
                                        </Reader>
                                        <Sorters>
                                            <ext:DataSorter Property="sn" Direction="ASC" />
                                        </Sorters>
                                    </ext:Store>
                                    </Store>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                    <ColumnModel ID="ColumnModel14" runat="server">
                                        <Columns>
                                            <ext:Column ID="Column1" runat="server" Text="序" DataIndex="sn" Width="50" />
                                            <ext:Column ID="Column94" runat="server" Text="条件" DataIndex="low_condition" Width="100" />
                                            <ext:Column ID="Column96" runat="server" Text="条件" DataIndex="high_condition" Width="100" />
                                        </Columns>
                                    </ColumnModel>
                                </ext:GridPanel>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <DirectEvents>
                        <BeforeClose OnEvent="Win_Close" />
                    </DirectEvents>
                </ext:Window> 
<%--            </Items>
        </ext:Viewport>--%> 
    </form>
</body>
</html>
