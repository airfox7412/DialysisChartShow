<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_01_03.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_01_03" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>并发症</title>
    <style type="text/css">        
        table, th, td {
            border-width:1px;
            border-style: outset;
            border-color:Gray;            
        }
        .trheadcolor
        {
            background-color:#5ABCE0;
        }
        p {
            display: block;
            font-size:14px;
            font-weight:bold; 
            margin:5px 5px 5px 5px;           
        }
    </style>
    <script language="javascript" type="text/javascript">
        var showResult = function (btn) {
            Ext.Msg.notify("Button Click", "You clicked the " + btn + " button");
        };

        var showResultText = function (btn, text) {
            Ext.Msg.notify("Button Click", "You clicked the " + btn + 'button and entered the text "' + text + '".');
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager2" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="并发症" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd" />
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="并发症分类" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_1_1" runat="server" BoxLabel="无" />
                                                <ext:Checkbox ID="chk_1_2" runat="server" BoxLabel="肾性贫血" />
                                                <ext:Checkbox ID="chk_1_3" runat="server" BoxLabel="骨矿物质代谢紊乱" />
                                                <ext:Checkbox ID="chk_1_4" runat="server" BoxLabel="高血压" />
                                                <ext:Checkbox ID="chk_1_5" runat="server" BoxLabel="低血压" />
                                                <ext:Checkbox ID="chk_1_6" runat="server" BoxLabel="淀粉样变性" />
                                                <ext:Checkbox ID="chk_1_7" runat="server" BoxLabel="呼吸系统" />
                                                <ext:Checkbox ID="chk_1_8" runat="server" BoxLabel="心血管系统" />
                                                <ext:Checkbox ID="chk_1_9" runat="server" BoxLabel="神经系统" />
                                                <ext:Checkbox ID="chk_1_10" runat="server" BoxLabel="消化系统" />
                                                <ext:Checkbox ID="chk_1_11" runat="server" BoxLabel="皮肤瘙痒" />
                                                <ext:Checkbox ID="chk_1_12" runat="server" BoxLabel="其它" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="骨矿物质代谢紊乱" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_2_1" runat="server" BoxLabel="高转运骨病（需骨活检支持）" />
                                                <ext:Checkbox ID="chk_2_2" runat="server" BoxLabel="低转运骨病（需骨活检支持）" />
                                                <ext:Checkbox ID="chk_2_3" runat="server" BoxLabel="混合型骨病（需骨活检支持）" />
                                                <ext:Checkbox ID="chk_2_4" runat="server" BoxLabel="转移性钙化" />
                                                <ext:Checkbox ID="chk_2_5" runat="server" BoxLabel="骨质疏松" />
                                                <ext:Checkbox ID="chk_2_6" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="其它骨矿物质代谢紊乱" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="淀粉样变性 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_4_1" runat="server" BoxLabel="腕管综合症" />
                                                <ext:Checkbox ID="chk_4_2" runat="server" BoxLabel="心脏损害" />
                                                <ext:Checkbox ID="chk_4_3" runat="server" BoxLabel="骨损害" />
                                                <ext:Checkbox ID="chk_4_4" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="其它淀粉样变性" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="呼吸系统并发症" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_6_1" runat="server" BoxLabel="肺部感染" />
                                                <ext:Checkbox ID="chk_6_2" runat="server" BoxLabel="结核" />
                                                <ext:Checkbox ID="chk_6_3" runat="server" BoxLabel="胸膜炎" />
                                                <ext:Checkbox ID="chk_6_4" runat="server" BoxLabel="胸腔积液" />
                                                <ext:Checkbox ID="chk_6_5" runat="server" BoxLabel="尿毒症肺炎 " />
                                                <ext:Checkbox ID="chk_6_6" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="其它呼吸系统并发症" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="心血管" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_8_1" runat="server" BoxLabel="心律失常" />
                                                <ext:Checkbox ID="chk_8_2" runat="server" BoxLabel="心功能不全" />
                                                <ext:Checkbox ID="chk_8_3" runat="server" BoxLabel="急性左心衰竭" />
                                                <ext:Checkbox ID="chk_8_4" runat="server" BoxLabel="缺血性心脏病" />
                                                <ext:Checkbox ID="chk_8_5" runat="server" BoxLabel="心包炎" />
                                                <ext:Checkbox ID="chk_8_6" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_9" runat="server" FieldLabel="其它心血管并发症" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="神经系统" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_10_1" runat="server" BoxLabel="脑梗塞" />
                                                <ext:Checkbox ID="chk_10_2" runat="server" BoxLabel="脑出血" />
                                                <ext:Checkbox ID="chk_10_3" runat="server" BoxLabel="神经病变" />
                                                <ext:Checkbox ID="chk_10_4" runat="server" BoxLabel="尿毒性脑病" />
                                                <ext:Checkbox ID="chk_10_5" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="其它神经系统并发症 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet7" runat="server" Flex="1" Title="消化系统并发症 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_12_1" runat="server" BoxLabel="肝硬化" />
                                                <ext:Checkbox ID="chk_12_2" runat="server" BoxLabel="消化道出血" />
                                                <ext:Checkbox ID="chk_12_3" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_13" runat="server" FieldLabel="其他消化系统并发症" />
                                                <ext:TextField ID="txt_14" runat="server" FieldLabel="其它并发症" />
                                                <ext:TextField ID="txt_15" runat="server" FieldLabel="具体情况描述  " />
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
    </form>
</body>
</html>
