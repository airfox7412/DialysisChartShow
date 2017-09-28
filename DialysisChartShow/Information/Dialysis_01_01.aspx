<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_01_01.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_01_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>原发病诊断</title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="原发病诊断" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="诊断日期" Format="yyyy-MM-dd" />
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="原发病诊断分类" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Items>
                                                <ext:Button ID="btn_1" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_1" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Radio ID="opt_1_1" runat="server" BoxLabel="原发性肾小球疾病" Name="opt_1" />
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="继发性肾小球疾病" Name="opt_1" />
                                                <ext:Radio ID="opt_1_3" runat="server" BoxLabel="遗传性及先天性肾病" Name="opt_1" />
                                                <ext:Radio ID="opt_1_4" runat="server" BoxLabel="肾小管间质疾病" Name="opt_1" />
                                                <ext:Radio ID="opt_1_5" runat="server" BoxLabel="泌尿系肿瘤" Name="opt_1" />
                                                <ext:Radio ID="opt_1_6" runat="server" BoxLabel="泌尿系感染和结石" Name="opt_1" />
                                                <ext:Radio ID="opt_1_7" runat="server" BoxLabel="肾脏切除术后" Name="opt_1" />
                                                <ext:Radio ID="opt_1_8" runat="server" BoxLabel="急性肾衰" Name="opt_1" />
                                                <ext:Radio ID="opt_1_9" runat="server" BoxLabel="移植肾失功" Name="opt_1" />
                                                <ext:Radio ID="opt_1_10" runat="server" BoxLabel="不详" Name="opt_1" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="原发性肾小球疾病" Layout="AnchorLayout" Collapsible="true" Collapsed="true" >
                                            <Items>
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_2" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Radio ID="opt_2_1" runat="server" BoxLabel="急性肾炎综合征" Name="opt_2" />
                                                <ext:Radio ID="opt_2_2" runat="server" BoxLabel="急进性肾炎综合征" Name="opt_2" />
                                                <ext:Radio ID="opt_2_3" runat="server" BoxLabel="慢性肾炎综合征" Name="opt_2" />
                                                <ext:Radio ID="opt_2_4" runat="server" BoxLabel="肾病综合征" Name="opt_2" />
                                                <ext:Radio ID="opt_2_5" runat="server" BoxLabel="血尿" Name="opt_2" />
                                                <ext:Radio ID="opt_2_6" runat="server" BoxLabel="孤立性蛋白尿" Name="opt_2" />
                                                <ext:Radio ID="opt_2_7" runat="server" BoxLabel="其它" Name="opt_2" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="其它原发性肾小球肾病" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="继发性肾小球疾病" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_4" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_4_1" runat="server" BoxLabel="高血压肾损害" Name="opt_4" />
                                                <ext:Radio ID="opt_4_2" runat="server" BoxLabel="糖尿病肾病" Name="opt_4" />
                                                <ext:Radio ID="opt_4_3" runat="server" BoxLabel="肥胖相关性肾病" Name="opt_4" />
                                                <ext:Radio ID="opt_4_4" runat="server" BoxLabel="淀粉样变性" Name="opt_4" />
                                                <ext:Radio ID="opt_4_5" runat="server" BoxLabel="多发骨髓瘤肾病" Name="opt_4" />
                                                <ext:Radio ID="opt_4_6" runat="server" BoxLabel="狼疮性肾炎" Name="opt_4" />
                                                <ext:Radio ID="opt_4_7" runat="server" BoxLabel="系统性血管炎肾脏损害" Name="opt_4" />
                                                <ext:Radio ID="opt_4_8" runat="server" BoxLabel="过敏紫癜性肾炎" Name="opt_4" />
                                                <ext:Radio ID="opt_4_9" runat="server" BoxLabel="血栓性微血管病" Name="opt_4" />
                                                <ext:Radio ID="opt_4_10" runat="server" BoxLabel="干燥综合征肾损害" Name="opt_4" />
                                                <ext:Radio ID="opt_4_11" runat="server" BoxLabel="硬皮病肾损害" Name="opt_4" />
                                                <ext:Radio ID="opt_4_12" runat="server" BoxLabel="类风湿性关节炎和强直性脊柱炎肾损害" Name="opt_4" />
                                                <ext:Radio ID="opt_4_13" runat="server" BoxLabel="银屑病肾损害" Name="opt_4" />
                                                <ext:Radio ID="opt_4_14" runat="server" BoxLabel="乙型肝炎病毒相关性肾炎" Name="opt_4" />
                                                <ext:Radio ID="opt_4_15" runat="server" BoxLabel="丙型肝炎病毒相关性肾炎" Name="opt_4" />
                                                <ext:Radio ID="opt_4_16" runat="server" BoxLabel="HIV相关性肾损害" Name="opt_4" />
                                                <ext:Radio ID="opt_4_17" runat="server" BoxLabel="流行性出血热肾损害" Name="opt_4" />
                                                <ext:Radio ID="opt_4_18" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="其它继发性肾小球肾病说明" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="遗传性及先天性肾病" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_6" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_6_1" runat="server" BoxLabel="多囊肾病" Name="opt_6" />
                                                <ext:Radio ID="opt_6_2" runat="server" BoxLabel="Alport综合征" Name="opt_6" />
                                                <ext:Radio ID="opt_6_3" runat="server" BoxLabel="薄基底膜肾病" Name="opt_6" />
                                                <ext:Radio ID="opt_6_4" runat="server" BoxLabel="近端肾小管损害及Fanconi综合征" Name="opt_6" />
                                                <ext:Radio ID="opt_6_5" runat="server" BoxLabel="Bartter综合征" Name="opt_6" />
                                                <ext:Radio ID="opt_6_6" runat="server" BoxLabel="Fabry病" Name="opt_6" />
                                                <ext:Radio ID="opt_6_7" runat="server" BoxLabel="脂蛋白肾病" Name="opt_6" />
                                                <ext:Radio ID="opt_6_8" runat="server" BoxLabel="其它" Name="opt_6" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="其它遗传性及先天性肾病" Name="" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="肾小管间质疾病" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_8" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_8_1" runat="server" BoxLabel="急性肾小管间质性肾炎" Name="opt_8" />
                                                <ext:Radio ID="opt_8_2" runat="server" BoxLabel="慢性肾小管间质性肾炎" Name="opt_8" />
                                                <ext:Radio ID="opt_8_3" runat="server" BoxLabel="急性肾小管坏死" Name="opt_8" />
                                                <ext:Radio ID="opt_8_4" runat="server" BoxLabel="肾小管性酸中毒" Name="opt_8" />
                                                <ext:Radio ID="opt_8_5" runat="server" BoxLabel="反流性肾病" Name="opt_8" />
                                                <ext:Radio ID="opt_8_6" runat="server" BoxLabel="梗阻性肾病" Name="opt_8" />
                                                <ext:Radio ID="opt_8_7" runat="server" BoxLabel="马兜铃酸肾病" Name="opt_8" />
                                                <ext:Radio ID="opt_8_8" runat="server" BoxLabel="造影剂肾病" Name="opt_8" />
                                                <ext:Radio ID="opt_8_9" runat="server" BoxLabel="重金属中毒性肾脏损害" Name="opt_8" />
                                                <ext:Radio ID="opt_8_10" runat="server" BoxLabel="放射性肾病及抗肿瘤药物所致的肾损害" Name="opt_8" />
                                                <ext:Radio ID="opt_8_11" runat="server" BoxLabel="痛风性肾病" Name="opt_8" />
                                                <ext:Radio ID="opt_8_12" runat="server" BoxLabel="其它" Name="opt_8" />
                                                <ext:TextField ID="txt_9" runat="server" FieldLabel="其他肾小管间质疾病" Name="" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="泌尿系感染和结石" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button7" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_1" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_10_1" runat="server" BoxLabel="慢性肾盂肾炎" Name="opt_10" />
                                                <ext:Radio ID="opt_10_2" runat="server" BoxLabel="泌尿系结核" Name="opt_10" />
                                                <ext:Radio ID="opt_10_3" runat="server" BoxLabel="肾结石" Name="opt_10" />
                                                <ext:Radio ID="opt_10_4" runat="server" BoxLabel="其它" Name="opt_10" />
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="其它泌尿系感染和结石" Name="" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet7" runat="server" Flex="1" Title="肾脏切除原因" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button8" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_12" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_12_1" runat="server" BoxLabel="肾脏肿瘤" Name="opt_12" />
                                                <ext:Radio ID="opt_12_2" runat="server" BoxLabel="肾结核" Name="opt_12" />
                                                <ext:Radio ID="opt_12_3" runat="server" BoxLabel="肾脏囊肿" Name="opt_12" />
                                                <ext:Radio ID="opt_12_4" runat="server" BoxLabel="肾发育不良" Name="opt_12" />
                                                <ext:Radio ID="opt_12_5" runat="server" BoxLabel="其它" Name="opt_12" />
                                                <ext:TextField ID="txt_13" runat="server" FieldLabel="其他肾脏切除原因" Name="" />
                                            </Items>
                                        </ext:FieldSet>
                                        <%--<ext:FieldSet ID="Field_ELDER_LSASMTS" runat="server" Title="" Layout="AnchorLayout"
                                            DefaultAnchor="100%">
                                            <Defaults>
                                                <ext:Parameter Name="LabelStyle" Value="padding-left:4px;" />
                                            </Defaults>
                                            <Items>
                                                <ext:RadioGroup ID="RadioGroup_EATING_CODE" runat="server" FieldLabel="进餐" AnchorHorizontal="100%"
                                                    Cls="x-check-group-alt">
                                                    <Items>
                                                        <ext:Radio ID="Radio_EATING_CODE1" runat="server" Note="可自理-独立完成" AnchorHorizontal="100%"
                                                            BoxLabel="0" Margin="3">
                                                        </ext:Radio>
                                                        <ext:Radio ID="Radio_EATING_CODE2" runat="server" Note="轻度依赖" AnchorHorizontal="100%"
                                                            BoxLabel="0" Margin="3">
                                                        </ext:Radio>
                                                        <ext:Radio ID="Radio_EATING_CODE3" runat="server" Note="中度依赖-需要协助，如切碎、搅拌食物等" AnchorHorizontal="100%"
                                                            BoxLabel="3" Margin="3" />
                                                        <ext:Radio ID="Radio_EATING_CODE4" runat="server" Note="不能自理-完全需要帮助" AnchorHorizontal="100%"
                                                            BoxLabel="5" Margin="3" />
                                                    </Items>
                                                </ext:RadioGroup>
                                                <ext:RadioGroup ID="RadioGroup_HYGIENE_CODE" runat="server" FieldLabel="梳洗">
                                                    <Items>
                                                        <ext:Radio ID="Radio_HYGIENE_CODE1" runat="server" Note="可自理-独立完成" AnchorHorizontal="100%"
                                                            BoxLabel="0" Margin="3" />
                                                        <ext:Radio ID="Radio_HYGIENE_CODE2" runat="server" Note="轻度依赖-能独立地洗头、梳头、洗脸、刷牙、剃须等；洗澡需要协助"
                                                            AnchorHorizontal="100%" BoxLabel="1" Margin="3" />
                                                        <ext:Radio ID="Radio_HYGIENE_CODE3" runat="server" Note="中度依赖-在协助下和适当的时间内，能完成部分梳洗活动"
                                                            AnchorHorizontal="100%" BoxLabel="3" Margin="3" />
                                                        <ext:Radio ID="Radio_HYGIENE_CODE4" runat="server" Note="不能自理-完全需要帮助" AnchorHorizontal="100%"
                                                            BoxLabel="7" Margin="3" />
                                                    </Items>
                                                </ext:RadioGroup>
                                                <ext:RadioGroup ID="RadioGroup_DRESSING_CODE" runat="server" FieldLabel="穿衣" Cls="x-check-group-alt"
                                                    AnchorHorizontal="100%" ColumnsNumber="4">
                                                    <Items>
                                                        <ext:Radio ID="Radio_DRESSING_CODE1" runat="server" Note="可自理-独立完成" AnchorHorizontal="100%"
                                                            BoxLabel="0" Margin="3" />
                                                        <ext:Radio ID="Radio_DRESSING_CODE2" runat="server" Note="轻度依赖" AnchorHorizontal="100%"
                                                            BoxLabel="0" Margin="3" />
                                                        <ext:Radio ID="Radio_DRESSING_CODE3" runat="server" Note="中度依赖-需要协助，在适当的时间内完成部分穿衣"
                                                            AnchorHorizontal="100%" BoxLabel="3" Margin="3" />
                                                        <ext:Radio ID="Radio_DRESSING_CODE4" runat="server" Note="不能自理-完全需要帮助" AnchorHorizontal="100%"
                                                            BoxLabel="5" Margin="3" />
                                                    </Items>
                                                </ext:RadioGroup>
                                                <ext:RadioGroup ID="RadioGroup_TOILETING_CODE" runat="server" FieldLabel="如厕" AnchorHorizontal="100%">
                                                    <Items>
                                                        <ext:Radio ID="Radio_TOILETING_CODE1" runat="server" Note="可自理-不需协助，可自控" AnchorHorizontal="100%"
                                                            BoxLabel="0" Margin="3" />
                                                        <ext:Radio ID="Radio_TOILETING_CODE2" runat="server" Note="轻度依赖-偶尔失禁，但基本上能如厕或使用便具"
                                                            AnchorHorizontal="100%" BoxLabel="1" Margin="3" />
                                                        <ext:Radio ID="Radio_TOILETING_CODE3" runat="server" Note="中度依赖-经常失禁，在很多提示和协助下尚能如厕或使用便具"
                                                            AnchorHorizontal="100%" BoxLabel="5" Margin="3" />
                                                        <ext:Radio ID="Radio_TOILETING_CODE4" runat="server" Note="不能自理-完全失禁，完全需要帮助" AnchorHorizontal="100%"
                                                            BoxLabel="10" Margin="3" />
                                                    </Items>
                                                </ext:RadioGroup>
                                                <ext:RadioGroup ID="RadioGroup_AMBULATORY_CODE" runat="server" FieldLabel="活动" Cls="x-check-group-alt">
                                                    <Items>
                                                        <ext:Radio ID="Radio_AMBULATORY_CODE1" runat="server" Note="可自理-独立完成所有活动" AnchorHorizontal="100%"
                                                            BoxLabel="0" Margin="3" />
                                                        <ext:Radio ID="Radio_AMBULATORY_CODE2" runat="server" Note="轻度依赖-借助较小的外力或辅助装置能完成站立、行走、上下楼梯等"
                                                            AnchorHorizontal="100%" BoxLabel="1" Margin="3" />
                                                        <ext:Radio ID="Radio_AMBULATORY_CODE3" runat="server" Note="中度依赖-借助较大的外力才能完成站立、行走，不能上下楼梯"
                                                            AnchorHorizontal="100%" BoxLabel="5" Margin="3" />
                                                        <ext:Radio ID="Radio_AMBULATORY_CODE4" runat="server" Note="不能自理-卧床不起，活动完全需要帮助" AnchorHorizontal="100%"
                                                            BoxLabel="10" Margin="3" />
                                                    </Items>
                                                </ext:RadioGroup>
                                            </Items>
                                        </ext:FieldSet>--%>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="Button6" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Button1" runat="server" Icon="Disk" Text="重置" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Clear_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
