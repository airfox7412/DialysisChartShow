<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_01.aspx.cs" Inherits="Dialysis_Chart_Show.Ipad.Dialysis_0h_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <style type = "text/css">
    .x-title-text
    {
        font-size: 24px;
    }
    .x-window-header-text-default
    {
        font-size: 24px;
        line-height: 24px;
    }
    .x-btn-button
    {
        height:32px;
        width:32px;
        line-height: 30px;
    }
        
    .x-border-box .x-form-text
    {
        height: 36px !important;
        font-size: x-large;
    }
    .x-box-item
    {
        height: 24px !important;
    }
    
    .x-form-item-label, .x-field-indicator, .x-label-value, .x-form-cb-label, .x-form-cb-label-default, .x-form-cb-label-after
    {
        font-size: 24px;
    }
    
    .x-component .x-fieldset-header-text .x-fieldset-header-text-collapsible .x-component-default
    {
        font-size: 36px;
        color:Yellow;
    }
    
    .mylabel
    {
         color:Blue;
    }
    .mylabel1
    { 
         color:Blue;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="pid_id" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="NeptuneTouch" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="首次血液透析护理评估措施记录单" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="时间日期" LabelWidth="120" Format="yyyy-MM-dd" />
                                        <ext:TextField ID="txt_1" runat="server" FieldLabel="评估护士" LabelWidth="120" IndicatorText="" />
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="透析当日首次评估" Layout="AnchorLayout"  Collapsible="true" Collapsed="false">
                                            <Items>
                                                <ext:Label ID="Label2" runat="server" Text="1.外院转入/新进入的透析患者首次透析需了解：" Cls="mylabel1" PaddingSpec="10 10 10 10" />
                                                <ext:Container ID="Container32" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_2" runat="server" FieldLabel="a.开始透析治疗时间" LabelWidth="220" />
                                                   </Items>
                                                </ext:Container>
                                                        
                                                <ext:Container ID="Container98" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_3" runat="server" FieldLabel="b.透析处方：剂量" LabelWidth="200" IndicatorText="次/周" Flex="1" />
                                                        <ext:TextField ID="txt_4" runat="server" FieldLabel="" LabelWidth="120" IndicatorText="h/次" Flex="1" />
                                                   </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container97" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_5" runat="server" FieldLabel="透析方法" LabelWidth="120" IndicatorText="" Flex="1" />
                                                        <ext:TextField ID="txt_6" runat="server" FieldLabel="滤器" LabelWidth="100" LabelAlign="Right" IndicatorText="" Flex="1" />
                                                        <ext:TextField ID="txt_7" runat="server" FieldLabel="透析器" LabelWidth="100" LabelAlign="Right" IndicatorText="" Flex="1" />
                                                   </Items>
                                                </ext:Container>
                                                <ext:Label Text="c.透析过程" />
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_8_1" runat="server" FieldLabel="1" LabelAlign="Right" BoxLabel="有症状" Name="opt_8" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_8_2" runat="server" BoxLabel="无症状" Name="opt_8" PaddingSpec="10 20 10 20" />
                                                   </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_1" runat="server" FieldLabel="2" LabelAlign="Right" BoxLabel="高血压" Name="opt_9" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_9_2" runat="server" BoxLabel="低血压" Name="opt_9" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_10_1" runat="server" FieldLabel="3" LabelAlign="Right" BoxLabel="水肿" Name="opt_10" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_10_2" runat="server" BoxLabel="脱水" Name="opt_10" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_10_3" runat="server" BoxLabel="发热" Name="opt_10" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_10_4" runat="server" BoxLabel="疼痛" Name="opt_10" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_11_1" runat="server" FieldLabel="4" LabelAlign="Right" BoxLabel="皮肤" Name="opt_11" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_11_2" runat="server" BoxLabel="粘膜出血" Name="opt_11" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_11_3" runat="server" BoxLabel="呕血" Name="opt_11" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_11_4" runat="server" BoxLabel="便血" Name="opt_11" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_12_1" runat="server" FieldLabel="5" LabelAlign="Right" BoxLabel="皮肤搔痒" Name="opt_12" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_12_2" runat="server" BoxLabel="褥疮" Name="opt_12" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_12_3" runat="server" BoxLabel="口腔破溃" Name="opt_12" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_1" runat="server" FieldLabel="6" LabelAlign="Right" BoxLabel="呼吸困难" Name="opt_13" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="紫绀" Name="opt_13" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_13_3" runat="server" BoxLabel="咳嗽" Name="opt_13" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_13_4" runat="server" BoxLabel="咯血" Name="opt_13" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_13_5" runat="server" BoxLabel="咯痰" Name="opt_13" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_14_1" runat="server" FieldLabel="7" LabelAlign="Right" BoxLabel="惊厥" Name="opt_14" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_14_2" runat="server" BoxLabel="眩晕" Name="opt_14" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_14_3" runat="server" BoxLabel="心悸" Name="opt_14" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_15_1" runat="server" FieldLabel="8" LabelAlign="Right" BoxLabel="腹泻" Name="opt_15" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_15_2" runat="server" BoxLabel="便秘" Name="opt_15" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_15_3" runat="server" BoxLabel="腹胀" Name="opt_15" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_1" runat="server" FieldLabel="9" LabelAlign="Right" BoxLabel="尿频" Name="opt_16" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_16_2" runat="server" BoxLabel="尿急" Name="opt_16" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_16_3" runat="server" BoxLabel="尿痛" Name="opt_16" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_16_4" runat="server" BoxLabel="血尿" Name="opt_16" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container33" runat="server" Layout="ColumnLayout" Padding="10">
                                                    <Items>
                                                        <ext:Radio ID="opt_58_1" runat="server" FieldLabel="10.是否做过铁剂过敏测试" LabelAlign="Right" LabelWidth="300" BoxLabel="是" Name="opt_58" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_58_2" runat="server" BoxLabel="否" Name="opt_58" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container34" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_59_1" runat="server" FieldLabel="11.对铁剂是否有过敏反应" LabelAlign="Right" LabelWidth="300" BoxLabel="是" Name="opt_59" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_59_2" runat="server" BoxLabel="否" Name="opt_59" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container35" runat="server" Layout="HBoxLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_17" runat="server" FieldLabel="12.既往病史" LabelWidth="150" LabelAlign="Right" Flex="1" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container36" runat="server" Layout="HBoxLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_18" runat="server" FieldLabel="药物过敏史" LabelWidth="150" LabelAlign="Right" Flex="1" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container37" runat="server" Layout="HBoxLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_19" runat="server" FieldLabel="13肢体缺如" LabelWidth="150" LabelAlign="Right" Flex="1" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container12" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_20_1" runat="server" FieldLabel="14" LabelAlign="Right" BoxLabel="意识障碍" Name="opt_20" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_20_2" runat="server" BoxLabel="感觉障碍" Name="opt_20" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_20_3" runat="server" BoxLabel="睡眠障碍" Name="opt_20" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container13" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_21_1" runat="server" FieldLabel="15" LabelAlign="Right" BoxLabel="运动麻痹" Name="opt_21" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_21_2" runat="server" BoxLabel="肌肉障碍" Name="opt_21" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container14" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_22_1" runat="server" FieldLabel="16.血管通路" LabelWidth="200" BoxLabel="临时" Name="opt_22" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_22_2" runat="server" BoxLabel="永久导管" Name="opt_22" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_22_3" runat="server" BoxLabel="AVF" Name="opt_22" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_22_4" runat="server" BoxLabel="其它" Name="opt_22" PaddingSpec="10 20 10 20">
                                                            <DirectEvents>
                                                                <Change OnEvent="jj"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_23" runat="server" Hidden="true" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Label ID="Label8" runat="server" Text="17.体外循环凝血情况：" PaddingSpec="10 20 10 20" />
                                                <ext:Container ID="Container15" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_24_1" runat="server" FieldLabel="部位" LabelAlign="Right" BoxLabel="A血路" Name="opt_24" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_24_2" runat="server" BoxLabel="V血路" Name="opt_24" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_24_3" runat="server" BoxLabel="透析器" Name="opt_24" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_24_4" runat="server" BoxLabel="全部" Name="opt_24" PaddingSpec="10 20 10 20" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container16" runat="server" Layout="ColumnLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_25_1" runat="server" FieldLabel="程度" LabelAlign="Right" BoxLabel="血丝" Name="opt_25" PaddingSpec="10 20 10 20"  />
                                                        <ext:Radio ID="opt_25_2" runat="server" BoxLabel="血块" Name="opt_25" PaddingSpec="10 20 10 20"  />
                                                        <ext:Radio ID="opt_25_3" runat="server" BoxLabel="无法回血" Name="opt_25" PaddingSpec="10 20 10 20"  />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container31" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_26_1" runat="server" FieldLabel="18 当日容量达标情况" LabelWidth="300" BoxLabel="达到干体重" Name="opt_26" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_26_2" runat="server" BoxLabel="多脱" Name="opt_26" PaddingSpec="10 20 10 20"  >
                                                            <DirectEvents>
                                                                <Change OnEvent="hh"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_27" runat="server" IndicatorText="kg" Hidden="true" />
                                                        <ext:Radio ID="opt_26_3" runat="server" BoxLabel="少脱" Name="opt_26" PaddingSpec="10 20 10 20">
                                                            <DirectEvents>
                                                                <Change OnEvent="ii"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_28" runat="server" IndicatorText="kg" Hidden="true" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container20" runat="server" Layout="HBoxLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_29" runat="server" Width="750" FieldLabel="19.本次透析情况" LabelWidth="200" LabelAlign="Right" Flex="1" /> 
                                                    </Items>
                                                </ext:Container>                                               
                                            </Items>
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>                                           
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="护理措施" Layout="AnchorLayout" Collapsible="true" Collapsed="false" >
                                            <Items>
                                                <ext:Label ID="Label3" runat="server" Text="1.干体重的评估：体重控制范围及如何确定干体重" Cls="mylabel1" />
                                                <ext:TextField ID="txt_30" runat="server" Flex="1" />
                                                <ext:TextField ID="txt_31" runat="server" FieldLabel="2.首次透析患者脱水(医嘱)" LabelWidth="300" />
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_32" runat="server" FieldLabel="3.测体温4次/日(置管/感染其他原因)，吸氧" LabelWidth="500" IndicatorText="L/分" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container24" runat="server" Layout="ColumnLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_33" runat="server" FieldLabel="4.血管通路" IndicatorText="导管建立时间，" />
                                                        <ext:TextField ID="txt_34" runat="server" IndicatorText="AVF建立时间" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container22" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label6" runat="server" Text="5.：" Cls="mylabel1" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_35_1" runat="server" BoxLabel="内瘘护理" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_2" runat="server" BoxLabel="双腔导管护理" Name="opt_35" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_36" runat="server" Width="750" FieldLabel="6.饮食指导" LabelWidth="125" LabelCls="mylabel1" PaddingSpec="10 20 10 20" />
                                                <ext:Label ID="Label4" runat="server" Text="7.调阅上次透析记录并了解：" LabelWidth="250" Cls="mylabel1" PaddingSpec="20 20 20 20" />
                                                <ext:Container ID="Container11" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Label ID="Label10" runat="server" Text="a.脱水情况：与干体重相关问题已与" PaddingSpec="10 10 10 50" />
                                                        <ext:TextField ID="txt_38" runat="server" FieldLabel="" IndicatorText="医生联系" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container17" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Label ID="Label11" runat="server" Text="b.与机器相关问题已与" PaddingSpec="10 10 10 50" />
                                                        <ext:TextField ID="txt_39" runat="server" FieldLabel="" IndicatorText="工程师联系" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container18" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Label ID="Label12" runat="server" Text="c.体外循环凝血问体与" PaddingSpec="10 10 10 50" />
                                                        <ext:TextField ID="txt_40" runat="server" FieldLabel="" IndicatorText="医生联系" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Label ID="Label13" runat="server" Text="d.与血管通路相关问题与" LabelWidth ="125" PaddingSpec="10 10 10 50" />
                                                        <ext:TextField ID="txt_41" runat="server" FieldLabel="" IndicatorText="医生联系" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container23" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:TextField ID="txt_42" runat="server" Width="750" FieldLabel="e.其他问题" LabelWidth="125" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_43" runat="server" Width="750" FieldLabel="8.采取安全措施" LabelWidth="200" LabelCls="mylabel1" PaddingSpec="10 20 10 20" />
                                                <ext:TextField ID="txt_44" runat="server" Width="750" FieldLabel="9.床边隔离" LabelWidth="125" LabelCls="mylabel1" PaddingSpec="10 20 10 20" />
                                                <ext:TextField ID="txt_45" runat="server" Width="750" FieldLabel="10.心电监护" LabelWidth="150" LabelCls="mylabel1" PaddingSpec="10 20 10 20" />
                                                <ext:TextField ID="txt_46" runat="server" Width="750" FieldLabel="11.备急救器材和药品" LabelWidth="250" LabelCls="mylabel1" PaddingSpec="10 20 10 20" />
                                            </Items>
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="健康教育与沟通" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_47" runat="server" Width="750" FieldLabel="1.自我介绍" LabelWidth="150" LabelCls="mylabel1" />
                                                <ext:TextField ID="txt_48" runat="server" Width="750" FieldLabel="2.问候患者" LabelWidth="150" LabelCls="mylabel1" />
                                                <ext:Label ID="Label16" runat="server" Text="3.教会患者相关家庭保健知识" Cls="mylabel1" />
                                                <ext:Checkbox ID="chk_49_1" runat="server" BoxLabel="a.会测血压，数脉搏" PaddingSpec="10 0 10 0" />
                                                <ext:Checkbox ID="chk_49_2" runat="server" BoxLabel="b.会听内瘘杂音" PaddingSpec="10 0 10 0" />
                                                <ext:Checkbox ID="chk_49_3" runat="server" BoxLabel="c.内瘘锻炼与维护" PaddingSpec="10 0 10 0" />
                                                <ext:Checkbox ID="chk_49_4" runat="server" BoxLabel="d.指导自行压迫止血方法" PaddingSpec="10 0 10 0" />
                                                <ext:Checkbox ID="chk_49_5" runat="server" BoxLabel="e.双腔留置导管维护" PaddingSpec="10 0 10 0" />
                                                <ext:Checkbox ID="chk_49_6" runat="server" BoxLabel="f.饮食指导(水、钾、磷)" PaddingSpec="10 0 10 0" />
                                                <ext:Checkbox ID="chk_49_7" runat="server" BoxLabel="g.干体重及体重增加的计算方法" PaddingSpec="10 0 10 0" />
                                                
                                                <ext:Label ID="Label17" runat="server" Text="4.教会患者使用相关药物" Cls="mylabel1" />
                                                <ext:Checkbox ID="chk_50_1" runat="server" BoxLabel="降压药" />
                                                <ext:Checkbox ID="chk_50_2" runat="server" BoxLabel="缓泻剂" />
                                                <ext:Checkbox ID="chk_50_3" runat="server" BoxLabel="促红素" />
                                                <ext:Checkbox ID="chk_50_4" runat="server" BoxLabel="钙制剂" />
                                                <ext:Checkbox ID="chk_50_5" runat="server" BoxLabel="左卡尼丁" />
                                                <ext:Checkbox ID="chk_50_6" runat="server" BoxLabel="铁剂" />
                                                <ext:TextField ID="txt_51" runat="server" FieldLabel="其它" LabelAlign="Right" IndicatorText="" />
                                                
                                                <ext:Label ID="Label18" runat="server" Text="5.登记联系患者方法" Cls="mylabel1" />
                                                <ext:Container ID="Container25" runat="server" Layout="HBoxLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_52" runat="server" Width="750"  FieldLabel="a.家庭住址" LabelWidth="150" IndicatorText="" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container26" runat="server" Layout="HBoxLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_53" runat="server" Width="750" FieldLabel="b.家庭主要成员" LabelWidth="200" IndicatorText="" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container29" runat="server" Layout="HBoxLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_54" runat="server" FieldLabel="b.联系电话" LabelWidth="150" IndicatorText="" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container30" runat="server" Layout="HBoxLayout" Padding="10">
                                                    <Items>
                                                        <ext:TextField ID="txt_55" runat="server" FieldLabel="c.缴费类型" LabelWidth="150" IndicatorText="" PaddingSpec="10 10 10 50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container28" runat="server" Layout="HBoxLayout">
                                                    <Items>
                                                        <ext:Radio ID="opt_56_1" runat="server" FieldLabel="d.前往医院方式" LabelWidth="200" BoxLabel="公共汽车" Name="opt_56" PaddingSpec="10 10 10 50" />
                                                        <ext:Radio ID="opt_56_2" runat="server" BoxLabel="打车" Name="opt_56" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_56_3" runat="server" BoxLabel="地铁" Name="opt_56" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_56_4" runat="server" BoxLabel="专人接送" Name="opt_56" PaddingSpec="10 20 10 20" />
                                                        <ext:Radio ID="opt_56_5" runat="server" BoxLabel="其它" Name="opt_56" PaddingSpec="10 20 10 20">
                                                            <DirectEvents>
                                                                <Change OnEvent="kk"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_57" runat="server" Hidden="true" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100" Height="50">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_close" runat="server" Icon="Disk" Text="关闭" Width="100" Height="50">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click" />
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
