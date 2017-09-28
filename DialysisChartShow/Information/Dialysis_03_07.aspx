<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_03_07.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_03_07" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="其它药物治疗" AutoScroll="true" ButtonAlign="Center">
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
                                                <ext:Button ID="Button16" runat="server" Icon="ScriptDelete" ToolTip="清空">
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
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="营养支持药物" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_2_1" runat="server" BoxLabel="左旋肉碱" FieldLabel="营养支持药物分类" />
                                                <ext:Checkbox ID="chk_2_2" runat="server" BoxLabel="叶酸" />
                                                <ext:Checkbox ID="chk_2_3" runat="server" BoxLabel="维生素B6" />
                                                <ext:Checkbox ID="chk_2_4" runat="server" BoxLabel="维生素B12" />
                                                <ext:Checkbox ID="chk_2_5" runat="server" BoxLabel="α酮酸" />
                                                <ext:Checkbox ID="chk_2_6" runat="server" BoxLabel="其它" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="左旋肉碱 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
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
                                                <ext:Radio ID="opt_3_1" runat="server" BoxLabel="雷卡" Name="opt_3" FieldLabel="左旋肉碱名称" />
                                                <ext:Radio ID="opt_3_2" runat="server" BoxLabel="芯能" Name="opt_3" />
                                                <ext:Radio ID="opt_3_3" runat="server" BoxLabel="贝康亭" Name="opt_3" />
                                                <ext:Radio ID="opt_3_4" runat="server" BoxLabel="可益能" Name="opt_3" />
                                                <ext:Radio ID="opt_3_5" runat="server" BoxLabel="东维力" Name="opt_3" />
                                                <ext:Radio ID="opt_3_6" runat="server" BoxLabel="其它" Name="opt_3" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="其它左旋肉碱名称" />
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_5">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_5_1" runat="server" BoxLabel="口服" Name="opt_5" FieldLabel="用药方式" />
                                                <ext:Radio ID="opt_5_2" runat="server" BoxLabel="静脉" Name="opt_5" />
                                                <ext:TextField ID="num_6" runat="server" FieldLabel="单次给药剂量 " IndicatorText="mg" />
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
                                                <ext:Radio ID="opt_7_1" runat="server" BoxLabel="停药" Name="opt_7" FieldLabel="服药频率" />
                                                <ext:Radio ID="opt_7_2" runat="server" BoxLabel="1次/日" Name="opt_7" />
                                                <ext:Radio ID="opt_7_3" runat="server" BoxLabel="2次/日" Name="opt_7" />
                                                <ext:Radio ID="opt_7_4" runat="server" BoxLabel="3次/日" Name="opt_7" />
                                                <ext:Radio ID="opt_7_5" runat="server" BoxLabel="1次/每次透析" Name="opt_7" />
                                                <ext:Radio ID="opt_7_6" runat="server" BoxLabel="其它" Name="opt_7" />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="其它服药频率" IndicatorText="次/日 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet12" runat="server" Flex="1" Title="叶酸 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_9">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_9_1" runat="server" BoxLabel="口服" Name="opt_9" FieldLabel="用药方式" />
                                                <ext:Radio ID="opt_9_2" runat="server" BoxLabel="静脉" Name="opt_9" />
                                                <ext:TextField ID="num_10" runat="server" FieldLabel="单次给药剂量 " IndicatorText="mg" />
                                                <ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_11">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_11_1" runat="server" BoxLabel="停药" Name="opt_11" FieldLabel="服药频率" />
                                                <ext:Radio ID="opt_11_2" runat="server" BoxLabel="1次/日" Name="opt_11" />
                                                <ext:Radio ID="opt_11_3" runat="server" BoxLabel="2次/日" Name="opt_11" />
                                                <ext:Radio ID="opt_11_4" runat="server" BoxLabel="3次/日" Name="opt_11" />
                                                <ext:Radio ID="opt_11_5" runat="server" BoxLabel="1次/每次透析" Name="opt_11" />
                                                <ext:Radio ID="opt_11_6" runat="server" BoxLabel="其它" Name="opt_11" />
                                                <ext:TextField ID="num_12" runat="server" FieldLabel="其它服药频率" IndicatorText="次/日 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet13" runat="server" Flex="1" Title="维生素B6" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button6" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_13">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_13_1" runat="server" BoxLabel="口服" Name="opt_13" FieldLabel="用药方式" />
                                                <ext:Radio ID="opt_13_2" runat="server" BoxLabel="静脉" Name="opt_13" />
                                                <ext:TextField ID="num_14" runat="server" FieldLabel="单次给药剂量 " IndicatorText="mg" />
                                                <ext:Button ID="Button7" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_15">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_15_1" runat="server" BoxLabel="停药" Name="opt_15" FieldLabel="服药频率" />
                                                <ext:Radio ID="opt_15_2" runat="server" BoxLabel="1次/日" Name="opt_15" />
                                                <ext:Radio ID="opt_15_3" runat="server" BoxLabel="2次/日" Name="opt_15" />
                                                <ext:Radio ID="opt_15_4" runat="server" BoxLabel="3次/日" Name="opt_15" />
                                                <ext:Radio ID="opt_15_5" runat="server" BoxLabel="其它" Name="opt_15" />
                                                <ext:TextField ID="num_16" runat="server" FieldLabel="其它服药频率" IndicatorText="次/日 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet14" runat="server" Flex="1" Title="维生素B12 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button8" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_17">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_17_1" runat="server" BoxLabel="口服" Name="opt_17" FieldLabel="用药方式" />
                                                <ext:Radio ID="opt_17_2" runat="server" BoxLabel="静脉" Name="opt_17" />
                                                <ext:Radio ID="opt_17_3" runat="server" BoxLabel="肌注" Name="opt_17" />
                                                <ext:TextField ID="num_18" runat="server" FieldLabel="单次给药剂量 " IndicatorText="mg" />
                                                <ext:Button ID="Button9" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_19">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_19_1" runat="server" BoxLabel="停药" Name="opt_19" FieldLabel="服药频率" />
                                                <ext:Radio ID="opt_19_2" runat="server" BoxLabel="1次/日" Name="opt_19" />
                                                <ext:Radio ID="opt_19_3" runat="server" BoxLabel="2次/日" Name="opt_19" />
                                                <ext:Radio ID="opt_19_4" runat="server" BoxLabel="3次/日" Name="opt_19" />
                                                <ext:Radio ID="opt_19_5" runat="server" BoxLabel="1次/每次透析" Name="opt_19" />
                                                <ext:Radio ID="opt_19_6" runat="server" BoxLabel="其它" Name="opt_19" />
                                                <ext:TextField ID="num_20" runat="server" FieldLabel="其它服药频率" IndicatorText="次/日 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet15" runat="server" Flex="1" Title="α酮酸" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button10" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_21">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_21_1" runat="server" BoxLabel="开同" Name="opt_21" FieldLabel="α酮酸名称" />
                                                <ext:Radio ID="opt_21_2" runat="server" BoxLabel="科罗迪" Name="opt_21" />
                                                <ext:Radio ID="opt_21_3" runat="server" BoxLabel="血特" Name="opt_21" />
                                                <ext:Radio ID="opt_21_4" runat="server" BoxLabel="其它" Name="opt_21" />
                                                <ext:TextField ID="txt_22" runat="server" FieldLabel="其它" />
                                                <ext:Button ID="Button11" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_23">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_23_1" runat="server" BoxLabel="口服" Name="opt_23" FieldLabel="用药方式" />
                                                <ext:Radio ID="opt_23_2" runat="server" BoxLabel="静脉" Name="opt_23" />
                                                <ext:TextField ID="num_24" runat="server" FieldLabel="单次给药剂量 " IndicatorText="g" />
                                                <ext:Button ID="Button12" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_25">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_25_1" runat="server" BoxLabel="停药" Name="opt_25" FieldLabel="服药频率" />
                                                <ext:Radio ID="opt_25_2" runat="server" BoxLabel="1次/日" Name="opt_25" />
                                                <ext:Radio ID="opt_25_3" runat="server" BoxLabel="2次/日" Name="opt_25" />
                                                <ext:Radio ID="opt_25_4" runat="server" BoxLabel="3次/日" Name="opt_25" />
                                                <ext:Radio ID="opt_25_5" runat="server" BoxLabel="其它" Name="opt_25" />
                                                <ext:TextField ID="num_26" runat="server" FieldLabel="其它服药频率" IndicatorText="次/日 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet16" runat="server" Flex="1" Title="其他营养支持药物 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_27" runat="server" FieldLabel="其他营养支持药物名称" />
                                                <ext:Button ID="Button13" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_28">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_28_1" runat="server" BoxLabel="口服" Name="opt_28" FieldLabel="用药方式" />
                                                <ext:Radio ID="opt_28_2" runat="server" BoxLabel="静脉" Name="opt_28" />
                                                <ext:TextField ID="num_29" runat="server" FieldLabel="单次给药剂量 " IndicatorText="mg" />
                                                <ext:Button ID="Button14" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_30">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_30_1" runat="server" BoxLabel="停药" Name="opt_30" FieldLabel="服药频率" />
                                                <ext:Radio ID="opt_30_2" runat="server" BoxLabel="1次/日" Name="opt_30" />
                                                <ext:Radio ID="opt_30_3" runat="server" BoxLabel="2次/日" Name="opt_30" />
                                                <ext:Radio ID="opt_30_4" runat="server" BoxLabel="3次/日" Name="opt_30" />
                                                <ext:Radio ID="opt_30_5" runat="server" BoxLabel="其它" Name="opt_30" />
                                                <ext:TextField ID="num_31" runat="server" FieldLabel="其它服药频率" IndicatorText="次/日 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet17" runat="server" Flex="1" Title="降脂药物" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button15" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_32">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_32_1" runat="server" BoxLabel="他汀类" Name="opt_32" FieldLabel="降脂药物分类" />
                                                <ext:Radio ID="opt_32_2" runat="server" BoxLabel="贝特类" Name="opt_32" />
                                                <ext:Radio ID="opt_32_3" runat="server" BoxLabel="烟酸类" Name="opt_32" />
                                                <ext:Radio ID="opt_32_4" runat="server" BoxLabel="胆酸螯合剂" Name="opt_32" />
                                                <ext:Radio ID="opt_32_5" runat="server" BoxLabel="胆固醇吸收抑制剂" Name="opt_32" />
                                                <ext:Radio ID="opt_32_6" runat="server" BoxLabel="其它" Name="opt_32" />
                                                <ext:Button ID="Button17" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_33">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_33_1" runat="server" BoxLabel="洛伐他汀" Name="opt_33" FieldLabel="他汀类药物" />
                                                <ext:Radio ID="opt_33_2" runat="server" BoxLabel="辛伐他汀" Name="opt_33" />
                                                <ext:Radio ID="opt_33_3" runat="server" BoxLabel="普伐他汀" Name="opt_33" />
                                                <ext:Radio ID="opt_33_4" runat="server" BoxLabel="氟伐他汀" Name="opt_33" />
                                                <ext:Radio ID="opt_33_5" runat="server" BoxLabel="阿托伐他汀" Name="opt_33" />
                                                <ext:Radio ID="opt_33_6" runat="server" BoxLabel="其它" Name="opt_33" />
                                                <ext:TextField ID="txt_34" runat="server" FieldLabel="其它他汀类药物" />
                                                <ext:Button ID="Button18" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_35">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_35_1" runat="server" BoxLabel="环丙贝特" Name="opt_35" FieldLabel="贝特类药物" />
                                                <ext:Radio ID="opt_35_2" runat="server" BoxLabel="苯扎贝特" Name="opt_35" />
                                                <ext:Radio ID="opt_35_3" runat="server" BoxLabel="非诺贝特" Name="opt_35" />
                                                <ext:Radio ID="opt_35_4" runat="server" BoxLabel="吉非贝齐" Name="opt_35" />
                                                <ext:Radio ID="opt_35_5" runat="server" BoxLabel="其它" Name="opt_35" />
                                                <ext:TextField ID="txt_36" runat="server" FieldLabel="其它贝特类药物" />
                                                <ext:TextField ID="txt_37" runat="server" FieldLabel="其它降脂药物" />
                                                <ext:TextField ID="num_38" runat="server" FieldLabel="单次给药剂量 " IndicatorText="mg" />
                                                <ext:Button ID="Button19" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_39">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_39_1" runat="server" BoxLabel="停药" Name="opt_39" FieldLabel="服药频率" />
                                                <ext:Radio ID="opt_39_2" runat="server" BoxLabel="1次/日" Name="opt_39" />
                                                <ext:Radio ID="opt_39_3" runat="server" BoxLabel="2次/日" Name="opt_39" />
                                                <ext:Radio ID="opt_39_4" runat="server" BoxLabel="3次/日" Name="opt_39" />
                                                <ext:TextField ID="num_40" runat="server" FieldLabel="其它服药频率" IndicatorText="次/日 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="抗血小板药物" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button20" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_41">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_41_1" runat="server" BoxLabel="阿司匹林" Name="opt_41" FieldLabel="抗血小板药物分类" />
                                                <ext:Radio ID="opt_41_2" runat="server" BoxLabel="潘生丁" Name="opt_41" />
                                                <ext:Radio ID="opt_41_3" runat="server" BoxLabel="抵克立得" Name="opt_41" />
                                                <ext:Radio ID="opt_41_4" runat="server" BoxLabel="氯吡格雷" Name="opt_41" />
                                                <ext:Radio ID="opt_41_5" runat="server" BoxLabel="其它" Name="opt_41" />
                                                <ext:TextField ID="txt_42" runat="server" FieldLabel="其它抗血小板药物" />
                                                <ext:TextField ID="num_43" runat="server" FieldLabel="单次给药剂量 " IndicatorText="mg" />
                                                <ext:Button ID="Button21" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_44">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_44_1" runat="server" BoxLabel="停药" Name="opt_44" FieldLabel="服药频率" />
                                                <ext:Radio ID="opt_44_2" runat="server" BoxLabel="1次/日" Name="opt_44" />
                                                <ext:Radio ID="opt_44_3" runat="server" BoxLabel="2次/日" Name="opt_44" />
                                                <ext:Radio ID="opt_44_4" runat="server" BoxLabel="3次/日" Name="opt_44" />
                                                <ext:Radio ID="opt_44_5" runat="server" BoxLabel="其它" Name="opt_44" />
                                                <ext:TextField ID="num_45" runat="server" FieldLabel="其它服药频率" IndicatorText="次/日 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="中成药" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_46_1" runat="server" BoxLabel="大黄苏打片 " FieldLabel="中成药名称 " />
                                                <ext:Checkbox ID="chk_46_2" runat="server" BoxLabel="尿毒清 " />
                                                <ext:Checkbox ID="chk_46_3" runat="server" BoxLabel="肾衰宁 " />
                                                <ext:Checkbox ID="chk_46_4" runat="server" BoxLabel="百令胶囊 " />
                                                <ext:Checkbox ID="chk_46_5" runat="server" BoxLabel="金水宝 " />
                                                <ext:Checkbox ID="chk_46_6" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_47" runat="server" FieldLabel="其它" />
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
