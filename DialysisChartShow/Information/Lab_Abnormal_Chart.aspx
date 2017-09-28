<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Lab_Abnormal_Chart.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Lab_Abnormal_Chart" %>

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

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Container ID="Container1" runat="server" OverflowY="Auto">
                    <LayoutConfig>
                        <ext:VBoxLayoutConfig Pack="Center" Align="Center" />
                    </LayoutConfig>
                    <Items>
                        <ext:Panel ID="Panel1" runat="server" Width="650">
                            <LayoutConfig>
                                <ext:VBoxLayoutConfig Pack="Center" Align="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:CartesianChart ID="Chart1" runat="server" Width="650" Height="450" StyleSpec="background:#fff;" InsetPadding="30">
                                    <Store>
                                        <ext:Store ID="Store1" runat="server">
                                            <Model>
                                                <ext:Model ID="Model1" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="month" />
                                                        <ext:ModelField Name="value" />
                                                        <ext:ModelField Name="value_l" />
                                                        <ext:ModelField Name="value_h" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                    <Axes>
                                        <ext:CategoryAxis Position="Bottom" Fields="month" Grid="true">
                                            <Label RotationDegrees="-45" />
                                        </ext:CategoryAxis>
                                        <ext:NumericAxis Fields="value" Position="Left" Grid="true" Minimum="0" Maximum="400">
                                            <Renderer Handler="return layoutContext.renderer(label) + '';" />
                                        </ext:NumericAxis>
                                        <ext:NumericAxis Fields="value_h" Position="Right" Grid="false" Minimum="0" Maximum="400" Hidden="true" />
                                        <ext:NumericAxis Fields="value_l" Position="Right" Grid="false" Minimum="0" Maximum="400" Hidden="true" />
                                    </Axes>
                                    <Series>
                                        <ext:LineSeries XField="month" YField="value_l">
                                            <HighlightConfig>
                                                <ext:CircleSprite Radius="4" />
                                            </HighlightConfig>
                                            <Marker>
                                                <ext:Sprite SpriteType="Circle" Radius="2" LineWidth="0" />
                                            </Marker>
                                        </ext:LineSeries>

                                        <ext:LineSeries XField="month" YField="value" Smooth="3">
                                            <StyleSpec>
                                                <ext:Sprite LineWidth="4" />
                                            </StyleSpec>
                                            <HighlightConfig>
                                                <ext:CircleSprite Radius="7" />
                                            </HighlightConfig>
                                            <Marker>
                                                <ext:Sprite SpriteType="Circle" Radius="4" LineWidth="0" />
                                            </Marker>
                                            <Label Field="value" Display="None" />
                                            <Tooltip ID="Tooltip1" runat="server" TrackMouse="true" StyleSpec="background:#00f">
                                                <Renderer Handler="toolTip.setTitle(record.get('month') + ': ' + record.get('value'));" />
                                            </Tooltip>
                                        </ext:LineSeries>

                                        <ext:LineSeries XField="month" YField="value_h">
                                            <HighlightConfig>
                                                <ext:CircleSprite Radius="4" />
                                            </HighlightConfig>
                                            <Marker>
                                                <ext:Sprite SpriteType="Circle" Radius="2" LineWidth="0" />
                                            </Marker>
                                        </ext:LineSeries>
                                    </Series>
                                    <Plugins>
                                        <ext:VerticalMarker ID="VerticalMarker1" runat="server" ShowXLabel="false" Snap="true"  />
                                    </Plugins>
                                </ext:CartesianChart>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
