<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_08.aspx.cs" 
    Inherits="Dialysis_Chart_Show.Information.Dialysis_0h_08" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type = "text/css">
    .mylabel
    {
         color:Blue;
    }
    .mylabel1
    {
         font-weight:bold;  
         color:Black;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血液透析患者评估表" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel1" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="时间日期" Format="yyyy-MM-dd" Visible="false">
                                        </ext:DateField>
                                        <ext:TextField ID="txt_leader" runat="server" FieldLabel="责任组长" IndicatorText="" >
                                            <DirectEvents>
                                                <Focus OnEvent="text_click" />
                                            </DirectEvents>
                                            <Listeners>
                                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                                            </Listeners>
                                        </ext:TextField>
                                        <ext:TextField ID="txt_nurse" runat="server" FieldLabel="责任护士" IndicatorText="" >
                                            <DirectEvents>
                                                <Focus OnEvent="text_click" />
                                            </DirectEvents>
                                            <Listeners>
                                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                                            </Listeners>
                                        </ext:TextField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="一般护理评估" Layout="AnchorLayout"  Collapsible="true" Collapsed="false">
                                            <Items>
                                                <ext:Label ID="Label1" runat="server" Text="1.入室方式：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_1_1" runat="server" BoxLabel="自行步入" Name="opt_1" />
                                                        <ext:Radio ID="opt_1_2" runat="server" BoxLabel="轮椅" Name="opt_1" />
                                                        <ext:Radio ID="opt_1_3" runat="server" BoxLabel="推床" Name="opt_1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label2" runat="server" Text="2.血压：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_2_1" runat="server" BoxLabel="正常" Name="opt_2" />
                                                        <ext:Radio ID="opt_2_2" runat="server" BoxLabel="偏高" Name="opt_2" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_2_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_2_3" runat="server" BoxLabel="偏低" Name="opt_2" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_2_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_2_4" runat="server" BoxLabel="未测" Name="opt_2" />
                                                        <ext:TextField ID="txt_2" runat="server" Width="120" IndicatorText="mmHg" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label3" runat="server" Text="3.心率：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_3_1" runat="server" BoxLabel="正常" Name="opt_3" />
                                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="偏快" Name="opt_3" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_3_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_3_3" runat="server" BoxLabel="偏慢" Name="opt_3" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_3_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_3_4" runat="server" BoxLabel="未测" Name="opt_3" />
                                                        <ext:TextField ID="txt_3" runat="server" Width="120" IndicatorText="次/分" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label4" runat="server" Text="4.呼吸：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_4_1" runat="server" BoxLabel="正常" Name="opt_4" />
                                                        <ext:Radio ID="opt_4_2" runat="server" BoxLabel="不规则呼吸" Name="opt_4" />
                                                        <ext:Radio ID="opt_4_3" runat="server" BoxLabel="无咳嗽" Name="opt_4" />
                                                        <ext:Radio ID="opt_4_4" runat="server" BoxLabel="有咳嗽" Name="opt_4" />
                                                        <ext:Radio ID="opt_4_5" runat="server" BoxLabel="无痰液" Name="opt_4" />
                                                        <ext:Radio ID="opt_4_6" runat="server" BoxLabel="有痰液" Name="opt_4" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label5" runat="server" Text="5.体温：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_1" runat="server" BoxLabel="正常" Name="opt_5" />
                                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="未测" Name="opt_5" />
                                                        <ext:Radio ID="opt_5_3" runat="server" BoxLabel="发热" Name="opt_5" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_5_3_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_5" runat="server" Width="70" IndicatorText="℃" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label6" runat="server" Text="6.生活自理能力：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_6_1" runat="server" BoxLabel="完全独立" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_2" runat="server" BoxLabel="辅助" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_3" runat="server" BoxLabel="依赖" Name="opt_6" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label7" runat="server" Text="7.体力：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_7_1" runat="server" BoxLabel="良好" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_2" runat="server" BoxLabel="一般" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_3" runat="server" BoxLabel="差" Name="opt_7" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label8" runat="server" Text="8.卧位：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_8_1" runat="server" BoxLabel="平卧位" Name="opt_8" />
                                                        <ext:Radio ID="opt_8_2" runat="server" BoxLabel="半卧位" Name="opt_8" />
                                                        <ext:Radio ID="opt_8_3" runat="server" BoxLabel="坐位" Name="opt_8" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label9" runat="server" Text="9.食欲：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_1" runat="server" BoxLabel="良好" Name="opt_9" />
                                                        <ext:Radio ID="opt_9_2" runat="server" BoxLabel="一般" Name="opt_9" />
                                                        <ext:Radio ID="opt_9_3" runat="server" BoxLabel="差" Name="opt_9" />
                                                   </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label10" runat="server" Text="10.饮水量控制：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_10_1" runat="server" BoxLabel="好" Name="opt_10" />
                                                        <ext:Radio ID="opt_10_2" runat="server" BoxLabel="较好" Name="opt_10" />
                                                        <ext:Radio ID="opt_10_3" runat="server" BoxLabel="困难" Name="opt_10" />
                                                   </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label11" runat="server" Text="11.睡眠：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_11_1" runat="server" BoxLabel="良好" Name="opt_11" />
                                                        <ext:Radio ID="opt_11_2" runat="server" BoxLabel="一般" Name="opt_11" />
                                                        <ext:Radio ID="opt_11_3" runat="server" BoxLabel="差" Name="opt_11" />
                                                   </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label12" runat="server" Text="12.尿量：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_12_1" runat="server" BoxLabel="无" Name="opt_12" />
                                                        <ext:Radio ID="opt_12_2" runat="server" BoxLabel="有" Name="opt_12" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_12_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_12" runat="server" Width="80" IndicatorText="ml/d" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label13" runat="server" Text="13.大便：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_13a" runat="server" Width="80" IndicatorText="次/日" />
                                                        <ext:Radio ID="opt_13_1" runat="server" BoxLabel="便秘" Name="opt_13" />
                                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="腹泻" Name="opt_13" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_13_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_13b" runat="server" FieldLabel="性状" LabelWidth="30" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label14" runat="server" Text="14.出血：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container14" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_14_1" runat="server" BoxLabel="无" Name="opt_14" />
                                                        <ext:Radio ID="opt_14_2" runat="server" BoxLabel="有" Name="opt_14" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_14_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_14" runat="server" Width="200" FieldLabel="部位" LabelWidth="30" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label15" runat="server" Text="15.用药情况：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container15" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_15_1" runat="server" BoxLabel="降压药" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_2" runat="server" BoxLabel="降糖药" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_3" runat="server" BoxLabel="抗凝药物" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_4" runat="server" BoxLabel="其他" Name="opt_15" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_15_4_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_15" runat="server" Width="200" FieldLabel="药名" LabelWidth="30" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="前次治疗后专科评估" Layout="AnchorLayout"  Collapsible="true" Collapsed="false">
                                            <Items>
                                                <ext:Label ID="Label16" runat="server" Text="1.前次透析后情况：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container16" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_1" runat="server" BoxLabel="无不适" Name="opt_16" />
                                                        <ext:Radio ID="opt_16_2" runat="server" BoxLabel="恶心、呕吐" Name="opt_16" />
                                                        <ext:Radio ID="opt_16_3" runat="server" BoxLabel="头痛" Name="opt_16" />
                                                        <ext:Radio ID="opt_16_4" runat="server" BoxLabel="头晕" Name="opt_16" />
                                                        <ext:Radio ID="opt_16_5" runat="server" BoxLabel="低血压" Name="opt_16" />
                                                        <ext:Radio ID="opt_16_6" runat="server" BoxLabel="其他" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_16_6_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_16" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label17" runat="server" Text="2.脱水情况：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container17" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_17_1" runat="server" BoxLabel="达到干体重" Name="opt_17" />
                                                        <ext:Radio ID="opt_17_2" runat="server" BoxLabel="少脱" Name="opt_17" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_17_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_17_3" runat="server" BoxLabel="多脱" Name="opt_17" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_17_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_17_4" runat="server" BoxLabel="调整干体重" Name="opt_17" />
                                                        <ext:TextField ID="txt_17" runat="server" Width="70" IndicatorText="kg" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label18" runat="server" Text="3.内瘘穿刺点情况：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container18" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_18_1" runat="server" BoxLabel="正常" Name="opt_18" />
                                                        <ext:Radio ID="opt_18_2" runat="server" BoxLabel="出血" Name="opt_18" />
                                                        <ext:Radio ID="opt_18_3" runat="server" BoxLabel="瘀血" Name="opt_18" />
                                                        <ext:Radio ID="opt_18_4" runat="server" BoxLabel="血肿" Name="opt_18" />
                                                        <ext:Radio ID="opt_18_5" runat="server" BoxLabel="绳梯" Name="opt_18" />
                                                        <ext:Radio ID="opt_18_6" runat="server" BoxLabel="定点" Name="opt_18" />
                                                        <ext:Radio ID="opt_18_7" runat="server" BoxLabel="区域" Name="opt_18" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="新患者情况" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Items>
                                                <ext:Label ID="Label19" runat="server" Text="1.是否首次透析：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container19" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_19_1" runat="server" BoxLabel="是" Name="opt_19" />
                                                        <ext:Radio ID="opt_19_2" runat="server" BoxLabel="否" Name="opt_19" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_19_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_19a" runat="server" Width="190" FieldLabel="(已行透析" FieldWidth="60" IndicatorText="天" />
                                                        <ext:TextField ID="txt_19b" runat="server" Width="80" FieldLabel="" IndicatorText="月" />
                                                        <ext:TextField ID="txt_19c" runat="server" Width="80" FieldLabel="" IndicatorText="年)" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label20" runat="server" Text="2.外院透析处方：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container20" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_20a" runat="server" Width="80" IndicatorText="次/周" />
                                                        <ext:TextField ID="txt_20b" runat="server" Width="100" IndicatorText="小时/次  ." />
                                                        <ext:TextField ID="txt_20c" runat="server" Width="400" FieldLabel="抗凝剂及用量" FieldWidth="50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label21" runat="server" Text="3.外院透析有无不适：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container21" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_21_1" runat="server" BoxLabel="无" Name="opt_21" />
                                                        <ext:Radio ID="opt_21_2" runat="server" BoxLabel="有" Name="opt_21" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_21_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_21" runat="server" Width="120" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="单针双腔导管置管术后" Layout="AnchorLayout"  Collapsible="true" Collapsed="false">
                                            <Items>
                                                <ext:Label ID="Label22" runat="server" Text="1.位置：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container22" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_22_1" runat="server" BoxLabel="临时性" Name="opt_22" />
                                                        <ext:Radio ID="opt_22_2" runat="server" BoxLabel="永久性" Name="opt_22" />
                                                        <ext:Radio ID="opt_22_3" runat="server" BoxLabel="颈内静脉" Name="opt_22" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_22_3_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_22_4" runat="server" BoxLabel="股静脉" Name="opt_22" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_22_3_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Container ID="Container22a" runat="server" Layout="ColumnLayout" Padding="2">
                                                            <Items>
                                                                <ext:Label ID="Label22a" runat="server" Text="部位:" Cls="label" Width ="30" />
                                                                <ext:Radio ID="opt_22a_1" runat="server" BoxLabel="左" Name="opt_22a" />
                                                                <ext:Radio ID="opt_22a_2" runat="server" BoxLabel="右" Name="opt_22a" />
                                                            </Items>
                                                        </ext:Container>
                                                        <%--<ext:Container ID="Container22b" runat="server" Layout="ColumnLayout" Padding="2">
                                                            <Items>
                                                                <ext:Label ID="Label22b1" runat="server" Text="(" Cls="label" Width ="10" />
                                                                <ext:Radio ID="opt_22a_3" runat="server" BoxLabel="左" Name="opt_22a" />
                                                                <ext:Radio ID="opt_22a_4" runat="server" BoxLabel="右" Name="opt_22a" />
                                                                <ext:Label ID="Label22b2" runat="server" Text=")" Cls="label" Width ="10" />
                                                            </Items>
                                                        </ext:Container>--%>
                                                        <ext:Label ID="Label22b" runat="server" Text="" Cls="label" Width ="10" />
                                                        <ext:Label ID="Label22c" runat="server" Text="术后" Cls="label" Width ="30" />
                                                        <ext:TextField ID="txt_22" runat="server" Width="80" IndicatorText="天" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label23" runat="server" Text="2.伤口外观：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container23" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_23_1" runat="server" BoxLabel="清洁" Name="opt_23" />
                                                        <ext:Radio ID="opt_23_2" runat="server" BoxLabel="渗血" Name="opt_23" />
                                                        <ext:Radio ID="opt_23_3" runat="server" BoxLabel="红" Name="opt_23" />
                                                        <ext:Radio ID="opt_23_4" runat="server" BoxLabel="热" Name="opt_23" />
                                                        <ext:Radio ID="opt_23_5" runat="server" BoxLabel="痛" Name="opt_23" />
                                                        <ext:Radio ID="opt_23_6" runat="server" BoxLabel="血肿" Name="opt_23" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_23_6_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Label ID="Label23a" runat="server" Text="" Cls="label" Width ="10" />
                                                        <ext:TextField ID="txt_23" runat="server" Text="大小"  Width="100" IndicatorText="cm" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label24" runat="server" Text="3.换药：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container24" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_24_1" runat="server" BoxLabel="1次/日" Name="opt_24" />
                                                        <ext:Radio ID="opt_24_2" runat="server" BoxLabel="1次/隔日" Name="opt_24" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label25" runat="server" Text="4.导管流量：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container25" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_25_1" runat="server" BoxLabel="充足" Name="opt_25" />
                                                        <ext:Radio ID="opt_25_2" runat="server" BoxLabel="不足" Name="opt_25" />
                                                        <ext:Radio ID="opt_25_3" runat="server" BoxLabel="A-V反接" Name="opt_25" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label26" runat="server" Text="5.有无发热：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container26" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_26_1" runat="server" BoxLabel="无" Name="opt_26" />
                                                        <ext:Radio ID="opt_26_2" runat="server" BoxLabel="有" Name="opt_26" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_26_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_26" runat="server" Width="80" IndicatorText="℃" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="动静脉内瘘吻合术后" Layout="AnchorLayout"  Collapsible="true" Collapsed="false">
                                            <Items>
                                                <ext:Label ID="Label27" runat="server" Text="1.位置：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container27" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_27_1" runat="server" BoxLabel="自体" Name="opt_27" />
                                                        <ext:Radio ID="opt_27_2" runat="server" BoxLabel="人工血管" Name="opt_27" />
                                                        <ext:Container ID="Container27_1a" runat="server" Layout="ColumnLayout" Padding="2">
                                                            <Items>
                                                                <ext:Label ID="Label27a" runat="server" Text="部位:" Cls="label" Width ="30" />
                                                                 <ext:Radio ID="opt_27a_1" runat="server" BoxLabel="上左" Name="opt_27a" />
                                                                <ext:Radio ID="opt_27a_2" runat="server" BoxLabel="上右" Name="opt_27a" />
                                                                <ext:Radio ID="opt_27a_3" runat="server" BoxLabel="下左" Name="opt_27a" />
                                                                <ext:Radio ID="opt_27a_4" runat="server" BoxLabel="下右" Name="opt_27a" />
                                                                <ext:Label ID="Label27b" runat="server" Text="肢" Cls="label" Width ="20" />
                                                            </Items>
                                                        </ext:Container>
                                                        <ext:Label ID="Label27_3" runat="server"  Text="术后" Cls="label" Width ="30" />
                                                        <ext:TextField ID="txt_27" runat="server" Width="80" IndicatorText="天" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label28" runat="server" Text="2.上次透析穿刺状况：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container28" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_28_1" runat="server" BoxLabel="顺利" Name="opt_28" />
                                                        <ext:Radio ID="opt_28_2" runat="server" BoxLabel="二次穿刺" Name="opt_28" />
                                                        <ext:Radio ID="opt_28_3" runat="server" BoxLabel="穿刺处外观正常" Name="opt_28" />
                                                        <ext:Radio ID="opt_28_4" runat="server" BoxLabel="瘀血" Name="opt_28" />
                                                        <ext:Radio ID="opt_28_5" runat="server" BoxLabel="肿胀" Name="opt_28" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label29" runat="server" Text="3.触诊/听诊血管杂音：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container29" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_29_1" runat="server" BoxLabel="正常" Name="opt_29" />
                                                        <ext:Radio ID="opt_29_2" runat="server" BoxLabel="弱" Name="opt_29" />
                                                        <ext:Radio ID="opt_29_3" runat="server" BoxLabel="无" Name="opt_29" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label30" runat="server" Text="4.内瘘成熟训练：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container30" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_30_1" runat="server" BoxLabel="无" Name="opt_30" />
                                                        <ext:Radio ID="opt_30_2" runat="server" BoxLabel="有" Name="opt_30" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_30_2_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_30" runat="server" Width="80" IndicatorText="次/日" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label31" runat="server" Text="5.内瘘使用年限：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container31" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_31_1" runat="server" BoxLabel="一个月内" Name="opt_31" />
                                                        <ext:Radio ID="opt_31_2" runat="server" BoxLabel="六个月内" Name="opt_31" />
                                                        <ext:Radio ID="opt_31_3" runat="server" BoxLabel="一年内" Name="opt_31" />
                                                        <ext:Radio ID="opt_31_4" runat="server" BoxLabel="一年以上" Name="opt_31" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet><%-- --%>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="健康教育指导" Layout="AnchorLayout"  Collapsible="true" Collapsed="false">
                                            <Items>
                                                <ext:Label ID="Label32" runat="server" Text="1.健康教育方式：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container32" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_32_1" runat="server" BoxLabel="口头宣教" />
                                                        <ext:Checkbox ID="chk_32_2" runat="server" BoxLabel="健教单张" />
                                                        <ext:Checkbox ID="chk_32_3" runat="server" BoxLabel="PPT讲解" />
                                                        <ext:Checkbox ID="chk_32_4" runat="server" BoxLabel="视频" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label33" runat="server" Text="2.饮食指导：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container33" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_33_1" runat="server" BoxLabel="饮水控制" />
                                                        <ext:Checkbox ID="chk_33_2" runat="server" BoxLabel="蛋白质摄入" />
                                                        <ext:Checkbox ID="chk_33_3" runat="server" BoxLabel="高钾食物" />
                                                        <ext:Checkbox ID="chk_33_4" runat="server" BoxLabel="高磷食物" />
                                                        <ext:Checkbox ID="chk_33_5" runat="server" BoxLabel="怎样吃盐" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label34" runat="server" Text="3.运动指导：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container34" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_34_1" runat="server" BoxLabel="那些运动可以做" />
                                                        <ext:Checkbox ID="chk_34_2" runat="server" BoxLabel="预防跌倒的方法" />
                                                        <ext:Checkbox ID="chk_34_3" runat="server" BoxLabel="立即停止运动的时刻" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label35" runat="server" Text="4.血管通路指导：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container35" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_35_1" runat="server" BoxLabel="日常护理" />
                                                        <ext:Checkbox ID="chk_35_2" runat="server" BoxLabel="内廔闭塞处理" />
                                                        <ext:Checkbox ID="chk_35_3" runat="server" BoxLabel="止血带的使用" />
                                                        <ext:Checkbox ID="chk_35_4" runat="server" BoxLabel="内廔成熟训练" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label36" runat="server" Text="5.体重管理：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container36" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_36_1" runat="server" BoxLabel="何谓干体重" />
                                                        <ext:Checkbox ID="chk_36_2" runat="server" BoxLabel="体重增加的标准" />
                                                        <ext:Checkbox ID="chk_36_3" runat="server" BoxLabel="干体重的调整" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label37" runat="server" Text="6.受教者：" Cls="mylabel1" Width ="500" />
                                                <ext:Container ID="Container37" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_37_1" runat="server" BoxLabel="患者本人" Name="opt_37" />
                                                        <ext:Label ID="Label37_1" runat="server" Text="照顾者"  Cls="label" Width ="40" />
                                                        <ext:Radio ID="opt_37_2" runat="server" BoxLabel="配偶" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_3" runat="server" BoxLabel="父母" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_4" runat="server" BoxLabel="子女" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_5" runat="server" BoxLabel="护工" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_6" runat="server" BoxLabel="其他" Name="opt_37" >
                                                            <DirectEvents>
                                                                <Change OnEvent="opt_37_6_Selected"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_37" runat="server" FieldLabel="" Width="80" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="Button_Print" runat="server" Icon="Printer" Text="打印" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Print_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Button_Save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Button_Close" runat="server" Icon="ArrowTurnLeft" Text="返回" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
                <ext:Window ID="Window1" runat="server" Title="请输入工号" Height="160" Closable="false" 
                    Width="350" BodyStyle="background-color: #fff;" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center">
                    <Items>
                        <ext:TextField ID="TextField_UserID" runat="server" FieldLabel="工号" ColumnWidth="1" LabelAlign="Right" Padding="5" />
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button4" runat="server" Icon="Accept" Text="确认" Width="150" Height="40">
                            <DirectEvents>
                                <Click OnEvent="btnDecrypt_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button5" runat="server" Icon="Cancel" Text="取消" Width="150" Height="40">
                            <DirectEvents>
                                <Click OnEvent="btnClose_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
