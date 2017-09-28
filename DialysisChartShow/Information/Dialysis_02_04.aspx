<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_02_04.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_02_04" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="透析充分性（血透）" AutoScroll="true"
                    ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:Label ID="label1" runat="server" Text="注：每3个月填写一次" />
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="处方" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="num_1" runat="server" FieldLabel="身高" IndicatorText="m 【注意：单位米（m），允许范围0.5-2.5】">
                                                    <DirectEvents>
                                                        <Blur OnEvent="text_BMISurface">
                                                        </Blur>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="num_2" runat="server" FieldLabel="体重" IndicatorText=" Kg 【填写此次透析充分性评价时干体重】">
                                                    <DirectEvents>
                                                        <Blur OnEvent="text_BMISurface">
                                                        </Blur>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="num_3" runat="server" FieldLabel="BMI" IndicatorText="【注意：自动计算】"
                                                    ReadOnly="true" />
                                                <ext:TextField ID="num_4" runat="server" FieldLabel="体表面积" IndicatorText="m^2【注意：自动计算】"
                                                    ReadOnly="true" />
                                                <ext:TextField ID="num_5" runat="server" FieldLabel="透前尿素" IndicatorText=" mmol/L">
                                                    <DirectEvents>
                                                        <Blur OnEvent="text_urr">
                                                        </Blur>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="num_6" runat="server" FieldLabel="透后尿素" IndicatorText="mmol/L">
                                                    <DirectEvents>
                                                        <Blur OnEvent="text_urr">
                                                        </Blur>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="num_7" runat="server" FieldLabel="透析时间" IndicatorText="h">
                                                    <DirectEvents>
                                                        <Blur OnEvent="text_urr">
                                                        </Blur>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="num_8" runat="server" FieldLabel="超滤量" IndicatorText="L">
                                                    <DirectEvents>
                                                        <Blur OnEvent="text_urr">
                                                        </Blur>
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="num_9" runat="server" FieldLabel="URR" IndicatorText="% 【注意：自动计算】"
                                                    ReadOnly="true" />
                                                <ext:TextField ID="num_10" runat="server" FieldLabel="spKt/V" IndicatorText="【注意：自动计算】"
                                                    ReadOnly="true" />
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_clear" runat="server" Icon="Disk" Text="重置" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Clear_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Btn_Close" runat="server" Icon="Disk" Text="关闭" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
