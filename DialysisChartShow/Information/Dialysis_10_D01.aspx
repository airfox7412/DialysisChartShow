<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_D01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_D01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>单个患者</title>
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
                    btn.up('panel').down('chart').download();
                }
            });
        };
  </script>

    <style type = "text/css">
    .myTitle
    {
         font-weight:bold;  
         color:Black;
        }
        
    <%--panel head 自动--%>
    .x-panel-header-text {
        font-size: 16px;
        font-weight: bold;
        line-height: 20px;
    }
    <%--cell字型大小  自动 --%>
    .x-grid-row .x-grid-cell { 
        font-size: 13px;
    }
    <%--grid column head  手动Cls="x-grid-hd-inner"--%>
    .x-grid-hd-inner {
        font-size: 12px;
        font-weight: bold;
    }
    <%--grid column 上色  手动tdCls="custom-column"--%>
    .x-grid-row .custom-column { 
        font-weight: bold;
    }
    
    <%--tree node text size 手动Cls="large-font"--%>
    .large-font 
    {
        font-size: 16px !important; 
        height: 22px !important;
    }
    .blue-large-font 
    {
        font-size: 16px !important; 
        height: 22px !important;
        color: blue !important;
    }
    
    .x-status-text 
    {
        font-size:18px !important;
        color: red !important;
    }
    
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />     
        <ext:Viewport ID="Viewport1" runat="server">
            <Items>
                <%--单个患者--%>
                <ext:FormPanel ID="FormPanel_11" runat="server" Border="false" Region="Center" ColumnWidth="1">
                    <Items>
                        <ext:Panel ID="Panel_12" runat="server" Border="false" Region="Center">
                            <Items>
                                <ext:Container ID="Container4" runat="server" Frame="true" Layout="ColumnLayout" Padding="5" >
                                    <Items>
                                        <ext:ComboBox ID="cb_patlist" runat="server" FieldLabel="姓名" LabelWidth="60" IndicatorText="*" IndicatorCls="emptyColor" 
                                            LabelAlign="Right" DisplayField="patname" ValueField="patname" 
                                            TypeAhead="false" HideBaseTrigger="true" PageSize="10" MinChars="1" TriggerAction="Query"
                                            PaddingSpec="2 10 2 2">
                                            <Store>
                                                <ext:Store ID="Store14" runat="server" AutoLoad="true">
                                                    <Proxy>
                                                        <ext:AjaxProxy Url="../Patinfos.ashx">
                                                            <ActionMethods Read="POST" />
                                                            <Reader>
                                                                <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                            </Reader>
                                                        </ext:AjaxProxy>
                                                    </Proxy>
                                                    <Model>
                                                        <ext:Model ID="Model14" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="patic" />
                                                                <ext:ModelField Name="patname" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <ListConfig LoadingText="寻找中...">
                                                <ItemTpl ID="ItemTpl2" runat="server">
                                                    <Html>
                                                        <div>
                                                            {patname}
                                                        </div>
                                                    </html>
                                                </ItemTpl>
                                            </ListConfig>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="ComboBoxGroup" runat="server" FieldLabel="大分类" LabelWidth="80" DisplayField="GROUP_NAME" LabelAlign="Right" EmptyText="选择一个分类" PaddingSpec="4 10 4 10">
                                            <DirectEvents>
                                                <Select OnEvent="ChangGroup" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cboCODE12" runat="server" FieldLabel="检查名称" EmptyText="未选择" LabelWidth="70" LabelAlign="Right" Editable="false" Width="220" PaddingSpec="4 10 4 10">
                                            <DirectEvents>
                                                <Change OnEvent="cboCODE12_Click">
                                                    <EventMask ShowMask="true" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:ComboBox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container5" runat="server" Frame="true" Layout="ColumnLayout" >
                                    <Items>
                                        <ext:TextField ID="txtRESULT_CODE12" runat="server" FieldLabel="检查代码" LabelAlign="Right" LabelWidth="70" Width="170" PaddingSpec="5 10 5 10" ReadOnly="true" />
                                        <ext:TextField ID="txtNORMAL12" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" LabelWidth="100" PaddingSpec="5 10 5 10" Width="250" ReadOnly="true" />
                                        <ext:Button ID="btn_Query12" runat="server" Icon="Find" Text="查询" Width="90" PaddingSpec="5 10 5 10" >
                                            <DirectEvents>
                                                <Click OnEvent="btn_Query12_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <%--<ext:Button ID="Button1" runat="server" Text="保存图像" Icon="Disk" Handler="exportChart" />--%>
                                    </Items>
                                </ext:Container>
                                <%--曲线波动图GridPanel--%>
                                <ext:Hidden ID="Hidden15" runat="server" />
                                <ext:GridPanel ID="GridPanel15" runat="server" Title="曲线波动图" Region="Center" Height="250" Width="800">                                       
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
                                                        <ext:ModelField Name="病历号" Type="Int" />
                                                        <ext:ModelField Name="检验日期" Type="String" />
                                                        <ext:ModelField Name="检验代码" Type="String" />
                                                        <ext:ModelField Name="检验名称" Type="String" />
                                                        <ext:ModelField Name="检验结果" Type="Float" />
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
                                <ext:Panel ID="PanelChart" runat="server" InnerPadding="0 40 0 40" Width="800" Height="350">
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar26" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill14" runat="server" />
                                                <%--<ext:Button ID="Button1" runat="server" Text="Reload Data" Icon="ArrowRefresh" OnDirectClick="ReloadData" />--%>
                                                <ext:Button ID="Button2" runat="server" Text="保存图像" Icon="Disk" Handler="saveChart" />
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Items>
                                        <ext:CartesianChart ID="Chart1" runat="server" InsetPadding="40" InnerPadding="0 40 0 40" Width="800" Height="350">
                                            <%--<LegendConfig ID="LegendConfig1" runat="server" Position="Right" />--%>
                                            <Store>
                                                <ext:Store ID="Store1" runat="server" AutoDataBind="true">
                                                    <Model>
                                                        <ext:Model ID="Model1" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="RESULT_DATE" />
                                                                <ext:ModelField Name="RESULT_VALUE_N" />
                                                                <ext:ModelField Name="RESULT_VALUE_L" />
                                                                <ext:ModelField Name="RESULT_VALUE_H" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>                                                
                                            </Store>
                                            <Interactions>
                                                <ext:PanZoomInteraction ZoomOnPanGesture="true" />
                                            </Interactions>
                                            <Axes>
                                                <ext:CategoryAxis Title="检验日期" Fields="RESULT_DATE" Position="Bottom" >
                                                    <Label FillStyle="#0000FF" Font="12px Arial Unicode MS" />
                                                </ext:CategoryAxis>
                                                <ext:NumericAxis  Title="检验结果" Fields="RESULT_VALUE_N,RESULT_VALUE_L,RESULT_VALUE_H" Position="Left" Minimum="0" Maximum="100" >
                                                    <Label FillStyle="#0185d7" Font="12px Arial Unicode MS" />
                                                        <GridConfig>
                                                            <Odd Opacity="1" FillStyle="#ddd" StrokeStyle="#bbb" StrokeOpacity="0.5" />
                                                        </GridConfig>
                                                </ext:NumericAxis>
                                            </Axes>
                                            <Series>
                                                <ext:LineSeries Titles="达标上限" XField="RESULT_DATE" YField="RESULT_VALUE_H">
                                                    <HighlightConfig>
                                                        <ext:Sprite FillStyle="#000" Radius="7" LineWidth="7" StrokeStyle="#fff" />
                                                    </HighlightConfig>
                                                </ext:LineSeries>
                                                <ext:LineSeries Titles="检验结果" XField="RESULT_DATE" YField="RESULT_VALUE_N" >
                                                    <HighlightConfig>
                                                        <ext:Sprite FillStyle="#000" Radius="7" LineWidth="7" StrokeStyle="#fff" />
                                                    </HighlightConfig>
                                                </ext:LineSeries>
                                                <ext:LineSeries Titles="达标下限" XField="RESULT_DATE" YField="RESULT_VALUE_L">
                                                    <HighlightConfig>
                                                        <ext:Sprite FillStyle="#000" Radius="7" LineWidth="7" StrokeStyle="#fff" />
                                                    </HighlightConfig>
                                                </ext:LineSeries>
                                            </Series>
                                        </ext:CartesianChart>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport> 
    </form>
</body>
</html>
