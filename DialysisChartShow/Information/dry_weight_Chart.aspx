<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dry_weight_Chart.aspx.cs" Inherits="Dialysis_Chart_Show.Information.dry_weight_Chart" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html>
<head id="Head1" runat="server">
    <title>Area Chart - Ext.NET Examples</title>

    <script type="text/javascript">
        var downloadWithOptions = function (chart, form) {
            var opts = form.getForm().getValues(false, false, false, true);

            // simple empty values remove, optimize it if it is required
            Ext.Object.each(opts, function (key, value, myself) {
                if (Ext.isObject(value)) {
                    Ext.Object.each(value, function (key, value, myself) {
                        if (Ext.isEmpty(value)) {
                            delete myself[key];
                        }
                    });

                    if (Ext.Object.isEmpty(value)) {
                        delete myself[key];
                    }
                }
                else if (Ext.isEmpty(value)) {
                    delete myself[key];
                }
            });
            chart.download(opts);
        };
    </script>
</head>
<body>
    <form id="Form1" runat="server">
        <ext:Hidden runat="server" ID="BDATE" />
        <ext:Hidden runat="server" ID="EDATE" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Container ID="Container1" runat="server" OverflowY="Auto">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Pack="Center" Align="Center" />
                    </LayoutConfig>
                    <Items>
                        <ext:Panel ID="Panel1" runat="server" Width="800" BodyStyle="background: transparent !important">
                            <LayoutConfig>
                                <ext:VBoxLayoutConfig Pack="Center" Align="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:CartesianChart ID="Chart1" runat="server" Height="500" Width="700" StyleSpec="background:#fff;" InsetPadding="40">
                                    <Store>
                                        <ext:Store ID="Store1" runat="server">
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="month" />
                                                        <ext:ModelField Name="before_weight" />
                                                        <ext:ModelField Name="after_weight" />
                                                        <ext:ModelField Name="dry_weight" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>

                                    <Background Fill="white" />

                                    <Interactions>
                                        <ext:PanZoomInteraction ZoomOnPanGesture="false" />
                                        <ext:ItemHighlightInteraction />
                                    </Interactions>

                                    <LegendConfig runat="server" Dock="Bottom" />

                                    <Items>
                                        <ext:TextSprite Text="干体重趋势图 " FontSize="22" Width="100" Height="30" X="40" Y="20" />
                                    </Items>
                                    <Axes>
                                        <ext:NumericAxis Fields="before_weight,after_weight,dry_weight" Position="Left" Grid="true" Minimum="0" Maximum="120">
                                            <Renderer Handler="return layoutContext.renderer(label) + '';" />
                                        </ext:NumericAxis>
                                        <ext:CategoryAxis Position="Bottom" Fields="month" Grid="true">
                                            <Label RotationDegrees="-45" />
                                        </ext:CategoryAxis>
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries XField="month" YField="before_weight" Title="透析前">
                                            <StyleSpec>
                                                <ext:Sprite GlobalAlpha="0.8" LineWidth="3" />
                                            </StyleSpec>
                                            <Marker>
                                                <ext:Sprite Radius="4" />
                                            </Marker>
                                            <Tooltip ID="Tooltip1" runat="server" TrackMouse="true" StyleSpec="background: #00f">
                                                <Renderer Handler="toolTip.setTitle(record.get('month') + ': ' + record.get('before_weight'));" />
                                            </Tooltip>
                                        </ext:LineSeries>
                                        <ext:LineSeries XField="month" YField="after_weight" Title="透析后">
                                            <StyleSpec>
                                                <ext:Sprite GlobalAlpha="0.8" LineWidth="3" />
                                            </StyleSpec>
                                            <Marker>
                                                <ext:Sprite Radius="4" />
                                            </Marker>
                                            <Label Field="after_weight" Display="Over" FontSize="9" />
                                            <Tooltip ID="Tooltip2" runat="server" TrackMouse="true" StyleSpec="background: #00f">
                                                <Renderer Handler="toolTip.setTitle(record.get('month') + ': ' + record.get('after_weight'));" />
                                            </Tooltip>
                                        </ext:LineSeries>
                                        <ext:AreaSeries XField="month" YField="dry_weight" Title="干体重">
                                            <StyleSpec>
                                                <ext:Sprite GlobalAlpha="0.8" LineWidth="3" />
                                            </StyleSpec>
                                            <Marker>
                                                <%--<ext:CircleSprite GlobalAlpha="0" ScalingX="0.01" ScalingY="0.01" Duration="200" Easing="EaseOut" />--%>
                                                <ext:Sprite Radius="4" />
                                            </Marker>
                                            <HighlightDefaults>
                                                <ext:CircleSprite GlobalAlpha="1" ScalingX="1.5" ScalingY="1.5" />
                                            </HighlightDefaults>
                                            <Tooltip ID="Tooltip3" runat="server" TrackMouse="true" StyleSpec="background: #00f">
                                                <Renderer Handler="toolTip.setTitle(record.get('month') + ': ' + record.get('dry_weight'));" />
                                            </Tooltip>
                                        </ext:AreaSeries>
                                    </Series>
                                </ext:CartesianChart>

                                <ext:GridPanel ID="GridPanel1" runat="server" StoreID="Store1" Width="410" UI="Success" Border="true">
                                    <ColumnModel>
                                        <Columns>
                                            <ext:Column ID="Column1" runat="server" Text="日期" DataIndex="month" Sortable="false" />
                                            <ext:Column ID="Column2" runat="server" Text="透析前" DataIndex="before_weight" Sortable="false" />
                                            <ext:Column ID="Column3" runat="server" Text="透析后" DataIndex="after_weight" Sortable="false" />
                                            <ext:Column ID="Column4" runat="server" Text="干体重" DataIndex="dry_weight" Sortable="false" />
                                        </Columns>
                                    </ColumnModel>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
