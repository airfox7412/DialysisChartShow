<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_03_03.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_03_03" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="抗高血压药" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:FieldSet ID="FieldSet8" runat="server" Flex="1" Title="是否使用" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_1">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_1_1" runat="server" BoxLabel="是" Name="opt_1" />
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="否" Name="opt_1" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="处方日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="抗高血压药" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_2_1" runat="server" BoxLabel="钙通道阻滞剂" FieldLabel="处方" />
                                                <ext:Checkbox ID="chk_2_2" runat="server" BoxLabel="ACEI" />
                                                <ext:Checkbox ID="chk_2_3" runat="server" BoxLabel="ARB" />
                                                <ext:Checkbox ID="chk_2_4" runat="server" BoxLabel="α阻滞剂" />
                                                <ext:Checkbox ID="chk_2_5" runat="server" BoxLabel="β阻滞剂" />
                                                <ext:Checkbox ID="chk_2_6" runat="server" BoxLabel="αβ阻滞剂" />
                                                <ext:Checkbox ID="chk_2_7" runat="server" BoxLabel="中枢性降压药" />
                                                <ext:Checkbox ID="chk_2_8" runat="server" BoxLabel="利尿剂" />
                                                <ext:Checkbox ID="chk_2_9" runat="server" BoxLabel="其它" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="钙通道阻滞剂 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button1" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_3">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_3_1" runat="server" BoxLabel="硝苯地平" Name="opt_3" FieldLabel="通用名" />
                                                <ext:Radio ID="opt_3_2" runat="server" BoxLabel="尼群地平" Name="opt_3" />
                                                <ext:Radio ID="opt_3_3" runat="server" BoxLabel="氨氯地平" Name="opt_3" />
                                                <ext:Radio ID="opt_3_4" runat="server" BoxLabel="非洛地平" Name="opt_3" />
                                                <ext:Radio ID="opt_3_5" runat="server" BoxLabel="尼卡地平" Name="opt_3" />
                                                <ext:Radio ID="opt_3_6" runat="server" BoxLabel="其它" Name="opt_3" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="其它请说明" />
                                                <ext:TextField ID="num_5" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_6">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_6_1" runat="server" BoxLabel="停药" Name="opt_6" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_6_2" runat="server" BoxLabel="1次/日" Name="opt_6" />
                                                <ext:Radio ID="opt_6_3" runat="server" BoxLabel="2次/日" Name="opt_6" />
                                                <ext:Radio ID="opt_6_4" runat="server" BoxLabel="3次/日" Name="opt_6" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="ACEI" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_7">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_7_1" runat="server" BoxLabel="卡托普利" Name="opt_7" FieldLabel="通用名" />
                                                <ext:Radio ID="opt_7_2" runat="server" BoxLabel="依那普利" Name="opt_7" />
                                                <ext:Radio ID="opt_7_3" runat="server" BoxLabel="贝那普利" Name="opt_7" />
                                                <ext:Radio ID="opt_7_4" runat="server" BoxLabel="雷米普利" Name="opt_7" />
                                                <ext:Radio ID="opt_7_5" runat="server" BoxLabel="福辛普利" Name="opt_7" />
                                                <ext:Radio ID="opt_7_6" runat="server" BoxLabel="其它" Name="opt_7" />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="其它请说明" />
                                                <ext:TextField ID="num_9" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_10">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_10_1" runat="server" BoxLabel="停药" Name="opt_10" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_10_2" runat="server" BoxLabel="1次/日" Name="opt_10" />
                                                <ext:Radio ID="opt_10_3" runat="server" BoxLabel="2次/日" Name="opt_10" />
                                                <ext:Radio ID="opt_10_4" runat="server" BoxLabel="3次/日" Name="opt_10" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="ARB" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button6" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_11">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_11_1" runat="server" BoxLabel="氯沙坦" Name="opt_11" FieldLabel="通用名" />
                                                <ext:Radio ID="opt_11_2" runat="server" BoxLabel="缬沙坦" Name="opt_11" />
                                                <ext:Radio ID="opt_11_3" runat="server" BoxLabel="厄贝沙坦" Name="opt_11" />
                                                <ext:Radio ID="opt_11_4" runat="server" BoxLabel="替米沙坦" Name="opt_11" />
                                                <ext:Radio ID="opt_11_5" runat="server" BoxLabel="奥美沙坦" Name="opt_11" />
                                                <ext:Radio ID="opt_11_6" runat="server" BoxLabel="其它" Name="opt_11" />
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="其它请说明" />
                                                <ext:TextField ID="num_13" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button7" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_14">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_14_1" runat="server" BoxLabel="停药" Name="opt_14" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_14_2" runat="server" BoxLabel="1次/日" Name="opt_14" />
                                                <ext:Radio ID="opt_14_3" runat="server" BoxLabel="2次/日" Name="opt_14" />
                                                <ext:Radio ID="opt_14_4" runat="server" BoxLabel="3次/日" Name="opt_14" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="α阻滞剂" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button8" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_15">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_15_1" runat="server" BoxLabel="哌唑嗪" Name="opt_15" FieldLabel="通用名" />
                                                <ext:Radio ID="opt_15_2" runat="server" BoxLabel="酚妥拉明" Name="opt_15" />
                                                <ext:Radio ID="opt_15_3" runat="server" BoxLabel="妥拉唑啉" Name="opt_15" />
                                                <ext:Radio ID="opt_15_4" runat="server" BoxLabel="乌拉地尔" Name="opt_15" />
                                                <ext:Radio ID="opt_15_5" runat="server" BoxLabel="萘哌地尔" Name="opt_15" />
                                                <ext:Radio ID="opt_15_6" runat="server" BoxLabel="其它" Name="opt_15" />
                                                <ext:TextField ID="txt_16" runat="server" FieldLabel="其它请说明" />
                                                <ext:TextField ID="num_17" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button9" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_18">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_18_1" runat="server" BoxLabel="停药" Name="opt_18" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_18_2" runat="server" BoxLabel="1次/日" Name="opt_18" />
                                                <ext:Radio ID="opt_18_3" runat="server" BoxLabel="2次/日" Name="opt_18" />
                                                <ext:Radio ID="opt_18_4" runat="server" BoxLabel="3次/日" Name="opt_18" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="β阻滞剂" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button10" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_19">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_19_1" runat="server" BoxLabel="普萘洛尔" Name="opt_19" FieldLabel="通用名" />
                                                <ext:Radio ID="opt_19_2" runat="server" BoxLabel="阿替洛尔" Name="opt_19" />
                                                <ext:Radio ID="opt_19_3" runat="server" BoxLabel="美托洛尔" Name="opt_19" />
                                                <ext:Radio ID="opt_19_4" runat="server" BoxLabel="比索洛尔" Name="opt_19" />
                                                <ext:Radio ID="opt_19_5" runat="server" BoxLabel="塞利洛尔" Name="opt_19" />
                                                <ext:Radio ID="opt_19_6" runat="server" BoxLabel="其它" Name="opt_19" />
                                                <ext:TextField ID="txt_20" runat="server" FieldLabel="其它请说明" />
                                                <ext:TextField ID="num_21" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button11" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_22">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_22_1" runat="server" BoxLabel="停药" Name="opt_22" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_22_2" runat="server" BoxLabel="1次/日" Name="opt_22" />
                                                <ext:Radio ID="opt_22_3" runat="server" BoxLabel="2次/日" Name="opt_22" />
                                                <ext:Radio ID="opt_22_4" runat="server" BoxLabel="3次/日" Name="opt_22" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet7" runat="server" Flex="1" Title="αβ阻滞剂 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button12" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_23">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_23_1" runat="server" BoxLabel="卡维地洛" Name="opt_23" FieldLabel="通用名" />
                                                <ext:Radio ID="opt_23_2" runat="server" BoxLabel="阿罗洛尔（阿尔马尔）" Name="opt_23" />
                                                <ext:Radio ID="opt_23_3" runat="server" BoxLabel="拉贝洛尔" Name="opt_23" />
                                                <ext:Radio ID="opt_23_4" runat="server" BoxLabel="其它" Name="opt_23" />
                                                <ext:TextField ID="txt_24" runat="server" FieldLabel="其它请说明" />
                                                <ext:TextField ID="num_25" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button13" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_26">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_26_1" runat="server" BoxLabel="停药" Name="opt_26" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_26_2" runat="server" BoxLabel="1次/日" Name="opt_26" />
                                                <ext:Radio ID="opt_26_3" runat="server" BoxLabel="2次/日" Name="opt_26" />
                                                <ext:Radio ID="opt_26_4" runat="server" BoxLabel="3次/日" Name="opt_26" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet9" runat="server" Flex="1" Title="中枢性降压药" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button14" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_27">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_27_1" runat="server" BoxLabel="可乐定" Name="opt_27" FieldLabel="通用名" />
                                                <ext:Radio ID="opt_27_2" runat="server" BoxLabel="甲基多巴" Name="opt_27" />
                                                <ext:TextField ID="txt_28" runat="server" FieldLabel="其它请说明" />
                                                <ext:TextField ID="num_29" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button15" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_30">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_30_1" runat="server" BoxLabel="停药" Name="opt_30" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_30_2" runat="server" BoxLabel="1次/日" Name="opt_30" />
                                                <ext:Radio ID="opt_30_3" runat="server" BoxLabel="2次/日" Name="opt_30" />
                                                <ext:Radio ID="opt_30_4" runat="server" BoxLabel="3次/日" Name="opt_30" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet10" runat="server" Flex="1" Title="利尿剂 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button16" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_31">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_31_1" runat="server" BoxLabel="呋塞米" Name="opt_31" FieldLabel="通用名" />
                                                <ext:Radio ID="opt_31_2" runat="server" BoxLabel="布美他尼" Name="opt_31" />
                                                <ext:Radio ID="opt_31_3" runat="server" BoxLabel="氢氯噻嗪" Name="opt_31" />
                                                <ext:Radio ID="opt_31_4" runat="server" BoxLabel="螺内酯" Name="opt_31" />
                                                <ext:Radio ID="opt_31_5" runat="server" BoxLabel="氨苯蝶啶" Name="opt_31" />
                                                <ext:Radio ID="opt_31_6" runat="server" BoxLabel="其它" Name="opt_31" />
                                                <ext:TextField ID="txt_32" runat="server" FieldLabel="其它请说明" />
                                                <ext:TextField ID="num_33" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button17" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_34">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_34_1" runat="server" BoxLabel="停药" Name="opt_34" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_34_2" runat="server" BoxLabel="1次/日" Name="opt_34" />
                                                <ext:Radio ID="opt_34_3" runat="server" BoxLabel="2次/日" Name="opt_34" />
                                                <ext:Radio ID="opt_34_4" runat="server" BoxLabel="3次/日" Name="opt_34" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet11" runat="server" Flex="1" Title="其他药物 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_35" runat="server" FieldLabel="通用名" />
                                                <ext:TextField ID="num_36" runat="server" FieldLabel="剂量" IndicatorText="mg" />
                                                <ext:Button ID="Button18" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_37">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_37_1" runat="server" BoxLabel="停药" Name="opt_37" FieldLabel="服药次数" />
                                                <ext:Radio ID="opt_37_2" runat="server" BoxLabel="1次/日" Name="opt_37" />
                                                <ext:Radio ID="opt_37_3" runat="server" BoxLabel="2次/日" Name="opt_37" />
                                                <ext:Radio ID="opt_37_4" runat="server" BoxLabel="3次/日" Name="opt_37" />
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
