<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type = "text/css">
    .mylabel
    {
         color:Blue;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="病历" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="记录日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet12" runat="server" Flex="1" Title="基本资料" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                        
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_1" runat="server" FieldLabel="姓名"  />
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_2">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_2_1" runat="server" FieldLabel = "性别" BoxLabel="男" Name="opt_2" />
                                                <ext:Radio ID="opt_2_2" runat="server" BoxLabel="女" Name="opt_2" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="出生年月" />
                                                <ext:TextField ID="num_4" runat="server" FieldLabel="年龄"  ReadOnly = "true" IndicatorText = "*自动计算"/>
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="民族"  />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="住院号"  />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="透析号"  />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="工作单位"  />
                                                <ext:TextField ID="txt_9" runat="server" FieldLabel="家庭住址" Width = "500"/>
                                                <ext:TextField ID="txt_10" runat="server" FieldLabel="联系电话"  />
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="手机"  />
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="身份证号码"  />
                                                <ext:Label ID = "Label1" runat  = "server" Text = "家属"  Cls = "mylabel"/>
                                                <ext:TextField ID="txt_13" runat="server" FieldLabel="家属姓名"  LabelAlign= "Right"/>
                                                <ext:TextField ID="txt_14" runat="server" FieldLabel="家属关系"  LabelAlign= "Right"/>
                                                <ext:TextField ID="txt_15" runat="server" FieldLabel="家属电话"  LabelAlign= "Right"/>
                                                <ext:TextField ID="txt_16" runat="server" FieldLabel="首次透析时间"  />
                                                <ext:Button ID="Button1" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_17">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_17_1" runat="server" FieldLabel = "费用" BoxLabel="自费" Name="opt_17" LabelAlign= "Right"/>
                                                <ext:Radio ID="opt_17_2" runat="server" BoxLabel="职工医保" Name="opt_17" />
                                                <ext:Radio ID="opt_17_3" runat="server" BoxLabel="居民医保" Name="opt_17" />
                                                <ext:Radio ID="opt_17_4" runat="server" BoxLabel="农合医保" Name="opt_17" />
                                                <ext:Radio ID="opt_17_5" runat="server" BoxLabel="省公费" Name="opt_17" />
                                                <ext:Radio ID="opt_17_6" runat="server" BoxLabel="市公费" Name="opt_17" />
                                                <ext:TextField ID="txt_18" runat="server" FieldLabel="其他费用"  LabelAlign= "Right"/>
                                                <%--<ext:TextField ID="txt_19" runat="server" FieldLabel="资料记录时间" ReadOnly = "true" />--%>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="导致慢性肾脏病（CKD）的基础疾病，病程" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID = "Label2" runat  = "server" Text = "慢性肾小球肾炎" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_20" runat="server" FieldLabel="慢性肾小球肾炎"  LabelAlign = "Right"/>
                                                <ext:TextField ID="txt_21" runat="server" FieldLabel="病程"  LabelAlign = "Right"/>
                                                <ext:TextField ID="txt_22" runat="server" FieldLabel="病理"  LabelAlign = "Right"/>

                                                <ext:Label ID = "Label3" runat  = "server" Text = "慢性间质性肾炎" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_23" runat="server" FieldLabel="慢性间质性肾炎"  LabelAlign = "Right"/>
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_24">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_24_1" runat="server" FieldLabel = "病因" BoxLabel="药物" Name="opt_24"  LabelAlign = "Right"/>
                                                <ext:Radio ID="opt_24_2" runat="server" BoxLabel="毒物" Name="opt_24" />
                                                <ext:Radio ID="opt_24_3" runat="server" BoxLabel="免疫性疾病" Name="opt_24" />
                                                <ext:Radio ID="opt_24_4" runat="server" BoxLabel="代谢紊乱" Name="opt_24" />
                                                <ext:Radio ID="opt_24_5" runat="server" BoxLabel="感染" Name="opt_24" />
                                                <ext:Radio ID="opt_24_6" runat="server" BoxLabel="梗阻" Name="opt_24" />
                                                <ext:Checkbox ID="chk_25_1" runat="server" BoxLabel="血管炎" FieldLabel="系统性" />
                                                <ext:Checkbox ID="chk_25_2" runat="server" BoxLabel="多囊肾" />
                                                <ext:Checkbox ID="chk_25_3" runat="server" BoxLabel="痛风 " />
                                                <ext:Checkbox ID="chk_25_4" runat="server" BoxLabel="梗阻性肾病 " />
                                                <ext:Checkbox ID="chk_25_5" runat="server" BoxLabel="SLE" />
                                                <ext:Checkbox ID="chk_25_6" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_26" runat="server" FieldLabel="其他系统性"  LabelAlign= "Right"/>

                                                <ext:Label ID = "Label4" runat  = "server" Text = "高血压" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_27" runat="server" FieldLabel="高血压病"  LabelAlign = "Right" IndicatorText = "期"/>
                                                <ext:Checkbox ID="chk_28_1" runat="server" BoxLabel="心" FieldLabel="合并症" LabelAlign = "Right"  />
                                                <ext:Checkbox ID="chk_28_2" runat="server" BoxLabel="脑" />
                                                <ext:Checkbox ID="chk_28_3" runat="server" BoxLabel="肾" />
                                                <ext:TextField ID="txt_29" runat="server" FieldLabel="其他合并症"  LabelAlign = "Right" />
                                                
                                                <ext:Label ID = "Label5" runat  = "server" Text = "糖尿病" Cls = "mylabel" />
                                                <ext:Container ID="Container5" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_30">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Radio ID="opt_30_1" runat="server" FieldLabel = "糖尿病" BoxLabel="1型" Name="opt_30"  LabelAlign = "Right"/>
                                                <ext:Radio ID="opt_30_2" runat="server" BoxLabel="2型" Name="opt_30" />
                                                <ext:TextField ID="txt_31" runat="server" FieldLabel="DM"  LabelAlign = "Right" IndicatorText = "年"/>
                                                <ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_32">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_32_1" runat="server" FieldLabel = "血糖控制" BoxLabel="好" Name="opt_32"  LabelAlign = "Right"/>
                                                <ext:Radio ID="opt_32_2" runat="server" BoxLabel="一般" Name="opt_32" />
                                                <ext:Radio ID="opt_32_3" runat="server" BoxLabel="差" Name="opt_32" />

                                                <ext:Label ID = "Label6" runat  = "server" Text = "药物过敏" Cls = "mylabel"/>
                                                <ext:Container ID="Container6" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button6" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_33">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_33_1" runat="server" FieldLabel = "药物过敏" BoxLabel="无" Name="opt_33"  LabelAlign = "Right"/>
                                                <ext:Radio ID="opt_33_2" runat="server" BoxLabel="有" Name="opt_33" />
                                                <ext:TextField ID="txt_34" runat="server" FieldLabel="过敏药物"  LabelAlign = "Right" />

                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="病史" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>         
                                                <ext:TextField ID="txt_35" runat="server" FieldLabel="主诉" Width = "500" />
                                                <ext:TextField ID="txt_36" runat="server" FieldLabel="既往史" Width = "500" />
                                                <ext:TextField ID="txt_37" runat="server" FieldLabel="家族史" Width = "500" />
                                                <ext:TextField ID="txt_38" runat="server" FieldLabel="其他重要病史补充" Width = "500" />
                                                <ext:TextArea  ID = "are_39" runat = "server" FieldLabel = "既往诊治经过" Width = "500" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="导致急性肾亏损伤（AKI）的病因，病程及合并症原因" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>         
                                                <ext:Button ID="Button7" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_40">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_40_1" runat="server" FieldLabel = "肾前性" BoxLabel="血容量不足" Name="opt_40"  LabelAlign = "Right"/>
                                                <ext:Radio ID="opt_40_2" runat="server" BoxLabel="脱水" Name="opt_40" />
                                                <ext:Radio ID="opt_40_3" runat="server" BoxLabel="低血压休克" Name="opt_40" />
                                                <ext:Radio ID="opt_40_4" runat="server" BoxLabel="心衰" Name="opt_40" />
                                                <ext:Radio ID="opt_40_5" runat="server" BoxLabel="脓毒败血症" Name="opt_40" />
                                                <ext:Radio ID="opt_40_6" runat="server" BoxLabel="肝肾综合症" Name="opt_40" />
                                                <ext:Radio ID="opt_40_7" runat="server" BoxLabel="肾动脉狭窄+应用ACEI类药物" Name="opt_40" />
                                                <ext:Radio ID="opt_40_8" runat="server" BoxLabel="其他" Name="opt_40" />
                                                <ext:TextField ID="txt_41" runat="server" FieldLabel="其他肾前性"  LabelAlign = "Right" />

                                                <ext:Button ID="Button8" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_42">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_42_1" runat="server" FieldLabel = "肾实质性" BoxLabel="急性肾小管坏死-ATN" Name="opt_42"  LabelAlign = "Right"/>
                                                <ext:Radio ID="opt_42_2" runat="server" BoxLabel="缺血" Name="opt_42" />
                                                <ext:Radio ID="opt_42_3" runat="server" BoxLabel="败血症" Name="opt_42" />
                                                <ext:Radio ID="opt_42_4" runat="server" BoxLabel="毒素如肌红蛋白" Name="opt_42" />
                                                <ext:Radio ID="opt_42_5" runat="server" BoxLabel="本 - 周氏蛋白" Name="opt_42" />
                                                <ext:Radio ID="opt_42_6" runat="server" BoxLabel="药物如庆大霉素、链霉素、卡那霉素、马兜铃酸、血管紧张素转换酶抑制剂、非甾体抗炎药、环孢霉素、化疗药、造影剂" Name="opt_42" />
                                                <ext:Radio ID="opt_42_7" runat="server" BoxLabel="急性间质性肾炎" Name="opt_42" />
                                                <ext:Radio ID="opt_42_8" runat="server" BoxLabel="急进性高血压" Name="opt_42" />
                                                <ext:Radio ID="opt_42_9" runat="server" BoxLabel="溶血性尿毒症综合症" Name="opt_42" />
                                                <ext:Radio ID="opt_42_10" runat="server" BoxLabel="肿瘤溶解氧综合症" Name="opt_42" />
                                                <%--<ext:Radio ID="opt_42_10" runat="server" BoxLabel="其他" Name="opt_42" />--%>
                                                <ext:TextField ID="txt_43" runat="server" FieldLabel="其他肾实质性"  LabelAlign = "Right" />
                                                
                                                <ext:Button ID="Button9" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_44">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_44_1" runat="server" FieldLabel = "肾后性" BoxLabel="肾静脉栓塞" Name="opt_44"  LabelAlign = "Right"/>
                                                <ext:Radio ID="opt_44_2" runat="server" BoxLabel="腹腔压力增高" Name="opt_44" />
                                                <ext:Radio ID="opt_44_3" runat="server" BoxLabel="尿路结石" Name="opt_44" />
                                                <ext:Radio ID="opt_44_4" runat="server" BoxLabel="前列腺增生导致尿路梗阻" Name="opt_44" />
                                                <ext:Radio ID="opt_44_5" runat="server" BoxLabel="其他" Name="opt_44" />
                                                <ext:TextField ID="txt_45" runat="server" FieldLabel="其他肾后性"  LabelAlign = "Right" />
                                                
                                                <ext:Label ID = "Label7" runat  = "server" Text = "病程" Cls = "mylabel"/>
                                                <ext:Container ID="Container7" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button10" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_46">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container1" runat="server" Layout = "ColumnLayout">
                                                    <Items>
                                                        
                                                        <ext:Radio ID="opt_46_1" runat="server" FieldLabel="尿量" BoxLabel="多" Name="opt_46" LabelAlign="Right" ColumnWidth = ".2" />
                                                        <ext:Radio ID="opt_46_2" runat="server" BoxLabel="少" Name="opt_46" ColumnWidth = ".1"/>
                                                        <ext:Radio ID="opt_46_3" runat="server" BoxLabel="正常" Name="opt_46" ColumnWidth = ".1"/>
                                                        <ext:TextField ID="txt_47" runat="server" FieldLabel="持续"  IndicatorText="天"  LabelAlign="Right" ColumnWidth = ".2"/>
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_48" runat="server" FieldLabel="尿量" IndicatorText="ml/24h"  LabelAlign="Right"/>
                                                <ext:TextField ID="txt_49" runat="server" FieldLabel="Scr较前升高" IndicatorText="μmol/L"  LabelAlign="Right"/>
                                                <ext:TextField ID="txt_50" runat="server" FieldLabel="Scr较前上升" IndicatorText="%"  LabelAlign="Right"/>
                                                
                                                <ext:Label ID = "Label8" runat  = "server" Text = "合并症" Cls = "mylabel"/>
                                                <ext:Container ID="Container8" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button11" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_51">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_51_1" runat="server" FieldLabel="多脏器衰竭（MOF）" BoxLabel="无" Name="opt_51" LabelAlign="Right" />
                                                <ext:Radio ID="opt_51_2" runat="server" BoxLabel="有" Name="opt_51" />
                                                <ext:Button ID="Button12" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_52">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_52_1" runat="server" FieldLabel="部位" BoxLabel="心" Name="opt_52" LabelAlign="Right" />
                                                <ext:Radio ID="opt_52_2" runat="server" BoxLabel="肺" Name="opt_52" />
                                                <ext:Radio ID="opt_52_3" runat="server" BoxLabel="肝" Name="opt_52" />
                                                <ext:Radio ID="opt_52_4" runat="server" BoxLabel="脑" Name="opt_52" />
                                                <ext:Radio ID="opt_52_5" runat="server" BoxLabel="其他" Name="opt_52" />
                                                <ext:TextField ID="txt_53" runat="server" FieldLabel="其他部位"  LabelAlign = "Right" />
                                                <ext:TextField ID="txt_54" runat="server" FieldLabel="感染"  LabelAlign="Right"/>
                                                <ext:TextField ID="txt_55" runat="server" FieldLabel="感染部位" LabelAlign="Right"/>
                                                <ext:TextArea ID = "are_56" runat = "server" FieldLabel = "其他重要病史补充" LabelAlign = "Right" Width = "500" />
                                                <ext:TextArea ID = "are_57" runat = "server" FieldLabel = "既往诊断，治疗经过" LabelAlign = "Right" Width = "500"/>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="诱因" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>         
                                                <ext:Button ID="Button13" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_58">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_58_1" runat="server" FieldLabel="有无诱因" BoxLabel="无" Name="opt_58" LabelAlign="Right" />
                                                <ext:Radio ID="opt_58_2" runat="server" BoxLabel="有" Name="opt_58" />
                                                <ext:Button ID="Button14" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_59">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_59_1" runat="server" FieldLabel="诱因" BoxLabel="感染" Name="opt_59" LabelAlign="Right" />
                                                <ext:Radio ID="opt_59_2" runat="server" BoxLabel="脱水" Name="opt_59" />
                                                <ext:Radio ID="opt_59_3" runat="server" BoxLabel="电解质紊乱" Name="opt_59" />
                                                <ext:Radio ID="opt_59_4" runat="server" BoxLabel="手术" Name="opt_59" />
                                                <ext:Radio ID="opt_59_5" runat="server" BoxLabel="创伤" Name="opt_59" />
                                                <ext:Radio ID="opt_59_6" runat="server" BoxLabel="肾毒性药物" Name="opt_59" />
                                                <ext:Radio ID="opt_59_7" runat="server" BoxLabel="过敏" Name="opt_59" />
                                                <ext:Radio ID="opt_59_8" runat="server" BoxLabel="其他" Name="opt_59" />
                                                <ext:TextField ID="txt_60" runat="server" FieldLabel="其他诱因"  LabelAlign = "Right" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="尿毒症症状及持续时间" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>         
                                                <ext:Label ID="Label9" runat="server" Text="消化系统" Cls="mylabel" />
                                                <ext:Container ID="Container9" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button15" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_61">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>

                                                <ext:Radio ID="opt_61_1" runat="server" FieldLabel="消化系统" BoxLabel="口臭" Name="opt_61" LabelAlign="Right" />
                                                <ext:Radio ID="opt_61_2" runat="server" BoxLabel="味苦" Name="opt_61" />
                                                <ext:Radio ID="opt_61_3" runat="server" BoxLabel="纳差" Name="opt_61" />
                                                <ext:Radio ID="opt_61_4" runat="server" BoxLabel="恶心" Name="opt_61" />
                                                <ext:Radio ID="opt_61_5" runat="server" BoxLabel="呕吐" Name="opt_61" />
                                                <ext:Radio ID="opt_61_6" runat="server" BoxLabel="腹泻" Name="opt_61" />
                                                <ext:Radio ID="opt_61_7" runat="server" BoxLabel="其他" Name="opt_61" />
                                                <ext:TextField ID="txt_62" runat="server" FieldLabel="其他" LabelAlign="Right"/>
                                                <ext:TextField ID="txt_63" runat="server" FieldLabel="病程" LabelAlign="Right"/>

                                                <ext:Label ID = "Label10" runat  = "server" Text = "血液系统" Cls = "mylabel"/>
                                                <ext:Container ID="Container10" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button16" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_64">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_64_1" runat="server" FieldLabel="血液系统（出血）" BoxLabel="鼻" Name="opt_64" LabelAlign="Right" />
                                                <ext:Radio ID="opt_64_2" runat="server" BoxLabel="牙龈" Name="opt_64" />
                                                <ext:Radio ID="opt_64_3" runat="server" BoxLabel="呕血" Name="opt_64" />
                                                <ext:Radio ID="opt_64_4" runat="server" BoxLabel="黑便" Name="opt_64" />
                                                <ext:Radio ID="opt_64_5" runat="server" BoxLabel="血尿" Name="opt_64" />
                                                <ext:Radio ID="opt_64_6" runat="server" BoxLabel="皮肤" Name="opt_64" />
                                                <ext:Radio ID="opt_64_7" runat="server" BoxLabel="颅内" Name="opt_64" />
                                                <ext:TextField ID="txt_65" runat="server" FieldLabel="贫血" IndicatorText = "度" LabelAlign="Right"/>

                                                <ext:Label ID = "Label11" runat  = "server" Text = "神经系统" Cls = "mylabel"/>
                                                <ext:Container ID="Container11" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button17" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_66">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_66_1" runat="server" FieldLabel="神经系统" BoxLabel="头痛" Name="opt_66" LabelAlign="Right" />
                                                <ext:Radio ID="opt_66_2" runat="server" BoxLabel="失眠" Name="opt_66" />
                                                <ext:Radio ID="opt_66_3" runat="server" BoxLabel="嗜睡" Name="opt_66" />
                                                <ext:Radio ID="opt_66_4" runat="server" BoxLabel="淡漠" Name="opt_66" />
                                                <ext:Radio ID="opt_66_5" runat="server" BoxLabel="兴奋" Name="opt_66" />
                                                <ext:Radio ID="opt_66_6" runat="server" BoxLabel="烦躁" Name="opt_66" />
                                                <ext:Radio ID="opt_66_7" runat="server" BoxLabel="昏迷" Name="opt_66" />
                                                <ext:Radio ID="opt_66_8" runat="server" BoxLabel="抽搐" Name="opt_66" />
                                                <ext:Radio ID="opt_66_9" runat="server" BoxLabel="其他" Name="opt_66" />
                                                <ext:TextField ID="txt_67" runat="server" FieldLabel="其他" LabelAlign="Right"/>

                                                <ext:Label ID = "Label12" runat  = "server" Text = "心血管系統" Cls = "mylabel"/>
                                                <ext:Container ID="Container12" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button18" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_68">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_68_1" runat="server" FieldLabel="高血压" BoxLabel="无" Name="opt_68" LabelAlign="Right" />
                                                <ext:Radio ID="opt_68_2" runat="server" BoxLabel="有" Name="opt_68" />
                                                <ext:Button ID="Button19" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_69">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_69_1" runat="server" FieldLabel="药物控制效果 " BoxLabel="好 " Name="opt_69" LabelAlign="Right" />
                                                <ext:Radio ID="opt_69_2" runat="server" BoxLabel="一般" Name="opt_69" />
                                                <ext:Radio ID="opt_69_3" runat="server" BoxLabel="差" Name="opt_69" />
                                                <ext:Container ID="Container2" runat="server" Layout = "ColumnLayout" Padding = "2">
                                                    <Items>
                                                        <ext:TextField ID="txt_70" runat="server" FieldLabel="血压一般水平" IndicatorText = "mmHg" ColumnWidth = ".3" LabelAlign = "Right"/>
                                                        <ext:Label ID = "Label13" runat = "server" Text = "　~　" />
                                                        <ext:TextField ID="txt_71" runat="server" IndicatorText = "mmHg" ColumnWidth = ".2"/>
                                                        <ext:Label ID = "Label14" runat = "server" Text = "　/　" />
                                                        <ext:TextField ID="txt_72" runat="server" IndicatorText = "mmHg" ColumnWidth = ".2"/>
                                                        <ext:Label ID = "Label15" runat = "server" Text = "　~　" />
                                                        <ext:TextField ID="txt_73" runat="server" IndicatorText = "mmHg" ColumnWidth = ".2"/>
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_74" runat="server" FieldLabel="病程" LabelAlign="Right" Padding = "2"/>
                                                <ext:Button ID="Button20" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_75">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_75_1" runat="server" FieldLabel="心衰发作史" BoxLabel="无" Name="opt_75" LabelAlign="Right" />
                                                <ext:Radio ID="opt_75_2" runat="server" BoxLabel="有" Name="opt_75" />
                                                <ext:TextField ID="txt_76" runat="server" FieldLabel="心衰发作次数" LabelAlign="Right"/>
                                                <ext:TextField ID="txt_77" runat="server" FieldLabel="心衰发作时间 " LabelAlign="Right"/>
                                                <ext:Button ID="Button21" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_78">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_78_1" runat="server" FieldLabel="心前区疼痛、心慌" BoxLabel="无" Name="opt_78" LabelAlign="Right" />
                                                <ext:Radio ID="opt_78_2" runat="server" BoxLabel="有" Name="opt_78" />
                                                <ext:TextField ID="txt_79" runat="server" FieldLabel="心律失常类型 " LabelAlign="Right"/>

                                                <ext:Label ID = "Label16" runat  = "server" Text = "皮肤肌肉骨骼" Cls = "mylabel"/>
                                                <ext:Container ID="Container13" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button22" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_80">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_80_1" runat="server" FieldLabel="关节疼痛、皮肤瘙痒" BoxLabel="无" Name="opt_80" LabelAlign="Right" />
                                                <ext:Radio ID="opt_80_2" runat="server" BoxLabel="有" Name="opt_80" />
                                                <ext:TextField ID="txt_81" runat="server" FieldLabel="部位" LabelAlign="Right"/>

                                                <ext:TextField ID="txt_82" runat="server" FieldLabel="其他" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet7" runat="server" Flex="1" Title="用药史" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_83" runat="server" FieldLabel="ACEI" />
                                                <ext:TextField ID="txt_84" runat="server" FieldLabel="ARB" />
                                                <ext:TextField ID="txt_85" runat="server" FieldLabel="CCB" />
                                                <ext:TextField ID="txt_86" runat="server" FieldLabel="β-受体阻滞剂" />
                                                <ext:TextField ID="txt_87" runat="server" FieldLabel="其他" />
                                                <ext:TextField ID="txt_88" runat="server" FieldLabel="降脂药物" />

                                                <ext:Label ID = "Label17" runat  = "server" Text = "调节钙磷代谢" Cls = "mylabel"/>
                                                <ext:Container ID="Container3" runat="server" Layout = "ColumnLayout" Padding = "2">
                                                    <Items>
                                                        <ext:Label ID = "Label21" runat = "server" Text = "　   磷结合剂　"  ColumnWidth = ".2"/>
                                                        <ext:TextField ID="txt_89" runat="server" FieldLabel="名" LabelWidth = "20" ColumnWidth = ".2" LabelAlign = "Right"/>
                                                        <ext:TextField ID="txt_90" runat="server" FieldLabel = "量" LabelWidth = "20" ColumnWidth = ".2" LabelAlign = "Right"/>
                                                        </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container4" runat="server" Layout = "ColumnLayout" Padding = "2">
                                                    <Items>
                                                        <ext:Label ID = "Label18" runat = "server" Text = "　   活性维生素D3"  ColumnWidth = ".2"/>
                                                        <ext:TextField ID="txt_91" runat="server" FieldLabel="名" LabelWidth = "20" ColumnWidth = ".2" LabelAlign = "Right"/>
                                                        <ext:TextField ID="txt_92" runat="server" FieldLabel = "量" LabelWidth = "20" ColumnWidth = ".2" LabelAlign = "Right"/>
                                                        </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_93" runat="server" FieldLabel="EPO" />
                                                <ext:TextField ID="txt_94" runat="server" FieldLabel="铁剂" />
                                                <ext:TextField ID="txt_95" runat="server" FieldLabel="其他" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet8" runat="server" Flex="1" Title="既往肾替代治疗史" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_96" runat="server" FieldLabel="首次血透时间" />
                                                <ext:TextField ID="txt_97" runat="server" FieldLabel="腹透时间" />
                                                <ext:TextField ID="txt_98" runat="server" FieldLabel="CRRT时间" />
                                                <ext:TextField ID="txt_99" runat="server" FieldLabel="治疗方案" Width = "500" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet9" runat="server" Flex="1" Title="目前血管通路" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button23" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_100">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_100_1" runat="server" FieldLabel="内瘘" BoxLabel="自体" Name="opt_100" />
                                                <ext:Radio ID="opt_100_2" runat="server" BoxLabel="移植" Name="opt_100" />
                                                <ext:TextField ID="txt_101" runat="server" FieldLabel="手术时间" />
                                                <ext:Button ID="Button24" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_102">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_102_1" runat="server" FieldLabel="中心静脉导管" BoxLabel="颈内静脉" Name="opt_102" />
                                                <ext:Radio ID="opt_102_2" runat="server" BoxLabel="锁骨下静脉" Name="opt_102" />
                                                <ext:Radio ID="opt_102_3" runat="server" BoxLabel="股静脉" Name="opt_102" />

                                                <ext:TextField ID="txt_103" runat="server" FieldLabel="留置时间" />
                                                <ext:TextField ID="txt_104" runat="server" FieldLabel="临时穿刺血管" />
                                                <ext:TextField ID="txt_105" runat="server" FieldLabel="穿刺部位" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet10" runat="server" Flex="1" Title="修正诊断" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_106" runat="server" FieldLabel="修正诊断" />
                                                <ext:DateField ID="dat_107" runat="server" FieldLabel="诊断日期" Format="yyyy-MM-dd" >
                                                </ext:DateField>
                                                <ext:TextField ID="txt_108" runat="server" FieldLabel="医生" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet11" runat="server" Flex="1" Title="入院诊断" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_109" runat="server" FieldLabel="病因" />
                                                <ext:TextField ID="txt_110" runat="server" FieldLabel="病理" />
                                                <ext:TextField ID="txt_111" runat="server" FieldLabel="功能" />
                                                <ext:TextField ID="txt_112" runat="server" FieldLabel="并发症" />
                                                <ext:TextField ID="txt_113" runat="server" FieldLabel="伴发症" />
                                                <ext:DateField ID="dat_114" runat="server" FieldLabel="诊断日期" Format="yyyy-MM-dd" >
                                                </ext:DateField>
                                                <ext:TextField ID="txt_115" runat="server" FieldLabel="医生" />
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
