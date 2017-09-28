<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_01_02.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_01_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>病理诊断</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="病理诊断" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="诊断日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="病理诊断分类 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_1_1" runat="server" BoxLabel="未行肾活检检查" />
                                                <ext:Checkbox ID="chk_1_2" runat="server" BoxLabel="原发性肾小球疾病" />
                                                <ext:Checkbox ID="chk_1_3" runat="server" BoxLabel="继发性肾小球疾病" />
                                                <ext:Checkbox ID="chk_1_4" runat="server" BoxLabel="遗传性及先天性肾病" />
                                                <ext:Checkbox ID="chk_1_5" runat="server" BoxLabel="肾小管间质疾病" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="原发性肾小球疾病" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_2_1" runat="server" BoxLabel="肾小球轻微病变" />
                                                <ext:Checkbox ID="chk_2_2" runat="server" BoxLabel="微小病变性肾病" />
                                                <ext:Checkbox ID="chk_2_3" runat="server" BoxLabel="局灶节段性肾小球损害" />
                                                <ext:Checkbox ID="chk_2_4" runat="server" BoxLabel="膜性肾病" />
                                                <ext:Checkbox ID="chk_2_5" runat="server" BoxLabel="系膜增殖性肾炎" />
                                                <ext:Checkbox ID="chk_2_6" runat="server" BoxLabel="IgA肾病" />
                                                <ext:Checkbox ID="chk_2_7" runat="server" BoxLabel="毛细血管内增殖性肾炎" />
                                                <ext:Checkbox ID="chk_2_8" runat="server" BoxLabel="膜增殖性肾炎" />
                                                <ext:Checkbox ID="chk_2_9" runat="server" BoxLabel="新月体肾炎" />
                                                <ext:Checkbox ID="chk_2_10" runat="server" BoxLabel="硬化性肾炎" />
                                                <ext:Checkbox ID="chk_2_11" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="其它原发性肾小球疾病" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="继发性肾小球疾病" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_4_1" runat="server" BoxLabel="高血压肾损害" />
                                                <ext:Checkbox ID="chk_4_2" runat="server" BoxLabel="糖尿病肾病" />
                                                <ext:Checkbox ID="chk_4_3" runat="server" BoxLabel="肥胖相关性肾病" />
                                                <ext:Checkbox ID="chk_4_4" runat="server" BoxLabel="淀粉样变性" />
                                                <ext:Checkbox ID="chk_4_5" runat="server" BoxLabel="多发骨髓瘤肾病" />
                                                <ext:Checkbox ID="chk_4_6" runat="server" BoxLabel="狼疮性肾炎" />
                                                <ext:Checkbox ID="chk_4_7" runat="server" BoxLabel="系统性血管炎肾脏损害" />
                                                <ext:Checkbox ID="chk_4_8" runat="server" BoxLabel="过敏紫癜性肾炎" />
                                                <ext:Checkbox ID="chk_4_9" runat="server" BoxLabel="抗基底膜肾炎(Goodpasture综合征)" />
                                                <ext:Checkbox ID="chk_4_10" runat="server" BoxLabel="血栓性微血管病" />
                                                <ext:Checkbox ID="chk_4_11" runat="server" BoxLabel="干燥综合征肾损害" />
                                                <ext:Checkbox ID="chk_4_12" runat="server" BoxLabel="硬皮病肾损害" />
                                                <ext:Checkbox ID="chk_4_13" runat="server" BoxLabel="类风湿性关节炎和强直性脊柱炎肾损害" />
                                                <ext:Checkbox ID="chk_4_14" runat="server" BoxLabel="银屑病肾损害" />
                                                <ext:Checkbox ID="chk_4_15" runat="server" BoxLabel="乙型肝炎病毒相关性肾炎" />
                                                <ext:Checkbox ID="chk_4_16" runat="server" BoxLabel="丙型肝炎病毒相关性肾炎" />
                                                <ext:Checkbox ID="chk_4_17" runat="server" BoxLabel="HIV相关性肾损害" />
                                                <ext:Checkbox ID="chk_4_18" runat="server" BoxLabel="流行性出血热肾损害" />
                                                <ext:Checkbox ID="chk_4_19" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="其它继发性肾小球疾病" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="遗传性及先天性肾病" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_6_1" runat="server" BoxLabel="Alport综合征" />
                                                <ext:Checkbox ID="chk_6_2" runat="server" BoxLabel="薄基底膜肾病" />
                                                <ext:Checkbox ID="chk_6_3" runat="server" BoxLabel="近端肾小管损害及Fanconi综合征" />
                                                <ext:Checkbox ID="chk_6_4" runat="server" BoxLabel="Bartter综合征" />
                                                <ext:Checkbox ID="chk_6_5" runat="server" BoxLabel="Fabry病" />
                                                <ext:Checkbox ID="chk_6_6" runat="server" BoxLabel="脂蛋白肾病" />
                                                <ext:Checkbox ID="chk_6_7" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="其它遗传性及先天性肾病" Name="" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="肾小管间质疾病" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_8_1" runat="server" BoxLabel="急性肾小管间质性肾炎" />
                                                <ext:Checkbox ID="chk_8_2" runat="server" BoxLabel="慢性肾小管间质性肾炎" />
                                                <ext:Checkbox ID="chk_8_3" runat="server" BoxLabel="急性肾小管坏死" />
                                                <ext:Checkbox ID="chk_8_4" runat="server" BoxLabel="马兜铃酸肾病" />
                                                <ext:Checkbox ID="chk_8_5" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_9" runat="server" FieldLabel="其它肾小管间质疾病" Name="" />
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="Button6" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Button1" runat="server" Icon="Disk" Text="重置" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Clear_Click">
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
