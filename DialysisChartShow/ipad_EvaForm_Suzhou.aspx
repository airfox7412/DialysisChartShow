<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_EvaForm_Suzhou.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_EvaForm_Suzhou" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化评估表</title>
    <style type="text/css">        
        <%--表頭--%>
        .label .x-label-value
        {
            width: 120px !important;
            height: 35px !important;
            font-size: 32px;
            color: #178951;
        }
        .label2 .x-label-value
        {
            font-size: 24px;
            color:Green;
            height:24px;
            text-align:right;
            width:100px;
        }
        
        <%--文字框加大--%>
        .x-border-box .x-form-text
        {
            height: 32px !important;
            font-size: 24px; 
        }
        <%--文字框頭 對齊右--%>
        .x-form-item-label-right
        {
            font-size: 24px; 
        }
        <%--文字框尾--%>
        .x-field-indicator
        {
            font-size: 24px; 
        }
        <%--Windows使用--%>
        .x-window-header-text-default
        {
            font-size: 24px;  
            line-height: 36px;
        }
        .x-box-item
        {
            height: 36px !important;
        }
        .x-btn .x-btn-center .x-btn-inner
        {
            font-size: 24px;  
        }
        <%--OPT與CHK--%>
        .x-form-item
        {
            font-size: 24px;
            height: 24px;
            <%--font: normal 25px tahoma,arial,verdana,sans-serif;--%>
        }

        .Xx-tool img
        {
            height: 35px;
            width: 30px;
        }
        .Xx-fieldset-header .x-fieldset-header-text
        {
            font-size: 18px; <%--large--%>
        }
        .Xx-form-display-field
        {
            font-size: 20px;
        }
        
        .x-form-checkbox, .x-form-radio
        {
            width: 34px;
            height: 34px;
            background-image: url("./Styles/che_btn.png");
        }
        
        .red
        {
            color: Red;
        }
        .red .x-form-field
        {
            color: blue;
        }
        
        .Text-black .x-form-field
        {
            color: black;
        }
        .Text-black-H .x-form-field
        {
            height: 100px !important;
            color: black !important;
        }
        .Text-red .x-form-field
        {
            color: red;
        }
        .Text-blue .x-form-field
        {
            color: blue;
        }
        .Text-blue-H .x-form-field
        {
            height: 100px !important;
            color: blue !important;
        }

        .CheckBox-red
        {
            color: Red;
            font-size: 24px;
        }

        .Radio-blue
        {
            color: Blue;
            font-size: 24px;
            height: 24px;
        }

        .Radio-black .Checkbox-black
        {
            color: Black;
            font-size: 24px;
            height: 24px;
        }
        
        .x-border-box .x-form-trigger
        {
            height: 30px !important;
            width: 17px !important;
            background-image: url("./Styles/trigger.png");
            cursor: pointer;
        }
        
        #ImageButton1, #ImageBtn_Home, #ImageBtn_back, #ImageBtn_save
        {
            height: 50px !important;
        }

        #ImageBtn_TurnOff 
        {
            height: 100px !important;
        }
        
        .x-panel-header-text-default
        {
            font-size: 32px; 
            line-height: 36px;
        }
        <%--panel head 自动--%>
        .Xx-panel-header-text {
            font-size: 32px;
            line-height: 36px;
        }
        <%--Grid Row--%>
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 26px;
            line-height: 28px; 
        }
        <%--Grid Column--%>
        .x-column-header-inner .x-column-header-text
        {
            font-size: 18px; <%--large--%>
            line-height: 28px; 
        }
        
        .x-boundlist-item 
        { 
            font-size: 24px; 
            color: blue
        }
        
        .Label-blue
        {
            font-size:24px;
            color:Blue;
        }        
        .x-fieldset-header
        {
            padding:10px;
            font-size:24px;
            color:Blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="pid_id" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="page" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="血液净化评估表(苏州医院)" BodyStyle="background-color:#EBF5FF !important;">
            <Items>
                <ext:Container ID="Container01" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:ImageButton ID="Btn_back" runat="server" ImageUrl="Styles/back2.png" OverImageUrl="Styles/back2over.png" Width="200" Height="50" PaddingSpec="2 50 2 2"> 
                            <DirectEvents>
                                <Click OnEvent="Btn_back_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container2" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:DateField ID="info_date" runat="server" FieldLabel="时间日期" LabelCls="Label-blue" LabelWidth="120" LabelAlign="Right" PaddingSpec="2 20 20 2" ReadOnly="true" Flex="1" Format="yyyy-MM-dd" />
                        <ext:TextField ID="txt_leader" runat="server" FieldLabel="责任组长" LabelCls="Label-blue" LabelWidth="120" LabelAlign="Right" PaddingSpec="2 20 20 2" Flex="1" >
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="txt_nurse" runat="server" FieldLabel="责任护士" LabelCls="Label-blue" LabelWidth="120" LabelAlign="Right" PaddingSpec="2 20 20 2" Flex="1" >
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items>
                </ext:Container>
                <ext:Panel ID="Panel1" runat="server" Collapsible="true" ColumnWidth="1" Layout="AnchorLayout" Title="一般护理评估" BodyStyle="background-color:#EBF5FF !important;">
                    <Items>
                        <ext:FieldSet ID="FieldSet1" runat="server" Title="1.入室方式" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup1" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_1_1" runat="server" Name="opt_1" Width="150" BoxLabel="自行步入" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_1_2" runat="server" Name="opt_1" Width="150" BoxLabel="轮椅" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_1_3" runat="server" Name="opt_1" Width="150" BoxLabel="推床" CheckedCls="CheckBox-red" />
                                    </Items>
                                    <LayoutConfig>
                                        <ext:CheckboxGroupLayoutConfig AutoFlex="false" />
                                    </LayoutConfig>
                                    <Defaults>
                                        <ext:Parameter Name="name" Value="ccType" />
                                        <ext:Parameter Name="style" Value="margin-right:15px;" />
                                    </Defaults>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>
                        
                        <ext:FieldSet ID="FieldSet2" runat="server" Title="2.血压" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup2" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_2_1" runat="server" Name="opt_2" Width="150" BoxLabel="正常" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_2_2" runat="server" Name="opt_2" Width="150" BoxLabel="偏高" CheckedCls="CheckBox-red" />                                        
                                        <ext:Radio ID="opt_2_3" runat="server" Name="opt_2" Width="150" BoxLabel="偏低" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_2_4" runat="server" Name="opt_2" Width="150" BoxLabel="未测" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_2" runat="server" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>
                        
                        <ext:FieldSet ID="FieldSet3" runat="server" Title="3.心率" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup3" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_3_1" runat="server" BoxLabel="正常" Width="150" Name="opt_3" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="偏快" Width="150" Name="opt_3" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_3_3" runat="server" BoxLabel="偏慢" Width="150" Name="opt_3" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_3" runat="server" IndicatorText="次/分" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet4" runat="server" Title="4.呼吸" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup4" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_4_1" runat="server" BoxLabel="正常" Name="opt_4" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_4_2" runat="server" BoxLabel="不规则呼吸" ColumnWidth=".2" Name="opt_4" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_4_3" runat="server" BoxLabel="无咳嗽" Name="opt_4" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_4_4" runat="server" BoxLabel="有咳嗽" Name="opt_4" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_4_5" runat="server" BoxLabel="无痰液" Name="opt_4" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_4_6" runat="server" BoxLabel="有痰液" Name="opt_4" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet5" runat="server" Title="5.体温" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup5" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_5_1" runat="server" BoxLabel="正常" Width="150" Name="opt_5" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="未测" Width="150" Name="opt_5" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_5_3" runat="server" BoxLabel="发热" Width="150" Name="opt_5" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_5" runat="server" IndicatorText="℃" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet6" runat="server" Title="6.生活自理能力" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup6" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_6_1" runat="server" BoxLabel="完全独立" Width="150" Name="opt_6" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_6_2" runat="server" BoxLabel="辅助" Width="150" Name="opt_6" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_6_3" runat="server" BoxLabel="依赖" Width="150" Name="opt_6" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet7" runat="server" Title="7.体力" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup7" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_7_1" runat="server" BoxLabel="良好" Width="150" Name="opt_7" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_7_2" runat="server" BoxLabel="一般" Width="150" Name="opt_7" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_7_3" runat="server" BoxLabel="差" Width="150" Name="opt_7" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet8" runat="server" Title="8.卧位" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup8" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_8_1" runat="server" BoxLabel="平卧位" Width="150" Name="opt_8" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_8_2" runat="server" BoxLabel="半卧位" Width="150" Name="opt_8" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_8_3" runat="server" BoxLabel="坐位" Width="150" Name="opt_8" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet9" runat="server" Title="9.食欲" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup9" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_9_1" runat="server" BoxLabel="良好" Width="150" Name="opt_9" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_9_2" runat="server" BoxLabel="一般" Width="150" Name="opt_9" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_9_3" runat="server" BoxLabel="差" Width="150" Name="opt_9" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet10" runat="server" Title="10.饮水量控制" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup10" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_10_1" runat="server" BoxLabel="好" Width="150" Name="opt_10" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_10_2" runat="server" BoxLabel="较好" Width="150" Name="opt_10" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_10_3" runat="server" BoxLabel="困难" Width="150" Name="opt_10" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet11" runat="server" Title="11.睡眠" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup11" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_11_1" runat="server" BoxLabel="良好" Width="150" Name="opt_11" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_11_2" runat="server" BoxLabel="一般" Width="150" Name="opt_11" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_11_3" runat="server" BoxLabel="差" Width="150" Name="opt_11" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet12" runat="server" Title="12.尿量" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup12" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_12_1" runat="server" BoxLabel="无" Width="100" Name="opt_12" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_12_2" runat="server" BoxLabel="有" Width="100" Name="opt_12" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_12" runat="server" IndicatorText="ml/d" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet13" runat="server" Title="13.大便" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup13" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:TextField ID="txt_13a" runat="server" IndicatorText="次/日" Width="150" Cls="Text-blue" />
                                        <ext:Radio ID="opt_13_1" runat="server" BoxLabel="便秘" Width="100" Name="opt_13" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="腹泻" Width="100" Name="opt_13" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label5" runat="server" Text="形状" Cls="label2" />
                                        <ext:TextField ID="txt_13b" runat="server" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet14" runat="server" Title="14.出血" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup14" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_14_1" runat="server" BoxLabel="无" Width="100" Name="opt_14" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_14_2" runat="server" BoxLabel="有" Name="opt_14" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label4" runat="server" Text="部位" Cls="label2" />
                                        <ext:TextField ID="txt_14" runat="server" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet15" runat="server" Title="15.用药情况" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup15" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_15_1" runat="server" BoxLabel="降压药" Name="opt_15" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_15_2" runat="server" BoxLabel="降糖药" Name="opt_15" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_15_3" runat="server" BoxLabel="抗凝药物" Name="opt_15" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_15_4" runat="server" BoxLabel="其他" Name="opt_15" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label3" runat="server" Text="药名" Cls="label2" />
                                        <ext:TextField ID="txt_15" runat="server" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel2" runat="server" Collapsible="true" ColumnWidth="1" Layout="AnchorLayout" Title="前次治疗后专科评估" BodyStyle="background-color:#EBF5FF !important;">
                    <Items>
                        <ext:FieldSet ID="FieldSet16" runat="server" Title="1.前次透析后情况" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup16" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_16_1" runat="server" BoxLabel="无不适" Name="opt_16" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_16_2" runat="server" BoxLabel="恶心、呕吐" Name="opt_16" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_16_3" runat="server" BoxLabel="头痛" Name="opt_16" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_16_4" runat="server" BoxLabel="头晕" Name="opt_16" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_16_5" runat="server" BoxLabel="低血压" Name="opt_16" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_16_6" runat="server" BoxLabel="其他" Name="opt_16" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_16" runat="server" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet17" runat="server" Title="2.脱水情况" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup17" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_17_1" runat="server" BoxLabel="达到干体重" Name="opt_17" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_17_2" runat="server" BoxLabel="少脱" Name="opt_17" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_17_3" runat="server" BoxLabel="多脱" Name="opt_17" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_17_4" runat="server" BoxLabel="调整干体重" Name="opt_17" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_17" runat="server" IndicatorText="kg" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet18" runat="server" Title="3.内瘘穿刺点情况" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup18" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_18_1" runat="server" BoxLabel="正常" Name="opt_18" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_18_2" runat="server" BoxLabel="出血" Name="opt_18" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_18_3" runat="server" BoxLabel="瘀血" Name="opt_18" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_18_4" runat="server" BoxLabel="血肿" Name="opt_18" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_18_5" runat="server" BoxLabel="绳梯" Name="opt_18" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_18_6" runat="server" BoxLabel="定点" Name="opt_18" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_18_7" runat="server" BoxLabel="区域" Name="opt_18" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel3" runat="server" Collapsible="true" ColumnWidth="1" Layout="AnchorLayout" Title="新患者情况" BodyStyle="background-color:#EBF5FF !important;">
                    <Items>
                        <ext:FieldSet ID="FieldSet19" runat="server" Title="1.是否首次透析" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup19" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_19_1" runat="server" BoxLabel="是" Width="80" Name="opt_19" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_19_2" runat="server" BoxLabel="否" Width="100" Name="opt_19" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label2" runat="server" Text="已行透析" Cls="label2" />
                                        <ext:TextField ID="txt_19a" runat="server" Width="80" FieldLabel="" IndicatorText="天" IndicatorCls="label2" Cls="Text-blue" />
                                        <ext:TextField ID="txt_19b" runat="server" Width="80" FieldLabel="" IndicatorText="月" IndicatorCls="label2" Cls="Text-blue" />
                                        <ext:TextField ID="txt_19c" runat="server" Width="80" FieldLabel="" IndicatorText="年" IndicatorCls="label2" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet20" runat="server" Title="2.外院透析处方" Layout="HBoxLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:TextField ID="txt_20a" runat="server" Width="130" LabelCls="Text-blue" IndicatorText="次/周" Cls="Text-blue" PaddingSpec="2 50 2 2" />
                                <ext:TextField ID="txt_20b" runat="server" Width="150" LabelCls="Text-blue" IndicatorText="小时/次" Cls="Text-blue" PaddingSpec="2 50 2 2" />
                                <ext:Label ID="Label1" runat="server" Text="抗凝剂及用量" Width="150" Cls="label2" />
                                <ext:TextField ID="txt_20c" runat="server" Width="100" ColumnWidth=".1" Cls="Text-blue" />
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet21" runat="server" Title="3.外院透析有无不适" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup20" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_21_1" runat="server" BoxLabel="无" Width="80" Name="opt_21" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_21_2" runat="server" BoxLabel="有" Width="80" Name="opt_21" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_21" runat="server" ColumnWidth=".1" Cls="Text-blue" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel4" runat="server" Collapsible="true" ColumnWidth="1" Layout="AnchorLayout" Title="单针双腔导管置管术后" BodyStyle="background-color:#EBF5FF !important;">
                    <Items>
                        <ext:FieldSet ID="FieldSet22" runat="server" Title="1.位置" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup21" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_22_1" runat="server" BoxLabel="临时性" Width="150" Name="opt_22" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_22_2" runat="server" BoxLabel="永久性" Width="150" Name="opt_22" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_22_3" runat="server" BoxLabel="颈内静脉" Width="150" Name="opt_22" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_22_4" runat="server" BoxLabel="股静脉" Width="150" Name="opt_22" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                                <ext:RadioGroup ID="RadioGroup211" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Label ID="Label22a" runat="server" Text="部位:" Cls="label2" Width="30" />
                                        <ext:Radio ID="opt_22a_1" runat="server" BoxLabel="左" Width="80" Name="opt_22a" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_22a_2" runat="server" BoxLabel="右" Width="80" Name="opt_22a" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label22b" runat="server" Text="" Cls="label" Width="10" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label22c" runat="server" Text="术后" Cls="label2" />
                                        <ext:TextField ID="txt_22" runat="server" IndicatorText="天" Width="100" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet23" runat="server" Title="2.伤口外观" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup22" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_23_1" runat="server" BoxLabel="清洁" Width="100" Name="opt_23" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_23_2" runat="server" BoxLabel="渗血" Width="100" Name="opt_23" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_23_3" runat="server" BoxLabel="红" Width="80" Name="opt_23" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_23_4" runat="server" BoxLabel="热" Width="80" Name="opt_23" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_23_5" runat="server" BoxLabel="痛" Width="80" Name="opt_23" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_23_6" runat="server" BoxLabel="血肿" Width="100" Name="opt_23" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label6" runat="server" Text="大小" Cls="label2" />
                                        <ext:TextField ID="txt_23" runat="server" Width="100" IndicatorText="cm" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet24" runat="server" Title="3.换药" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup23" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_24_1" runat="server" BoxLabel="1次/日" Width="130" Name="opt_24" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_24_2" runat="server" BoxLabel="1次/隔日" Width="200" Name="opt_24" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet25" runat="server" Title="4.导管流量" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup24" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_25_1" runat="server" BoxLabel="充足" Width="100" Name="opt_25" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_25_2" runat="server" BoxLabel="不足" Width="100" Name="opt_25" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_25_3" runat="server" BoxLabel="A-V反接" Width="200" Name="opt_25" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet26" runat="server" Title="5.有无发热" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup25" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_26_1" runat="server" BoxLabel="无" Width="100" Name="opt_26" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_26_2" runat="server" BoxLabel="有" Width="80" Name="opt_26" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_26" runat="server" Width="80" IndicatorText="℃" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel5" runat="server" Collapsible="true" ColumnWidth="1" Layout="AnchorLayout" Title="动静脉内瘘吻合术后" BodyStyle="background-color:#EBF5FF !important;">
                    <Items>
                        <ext:FieldSet ID="FieldSet27" runat="server" Title="1.位置" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup26" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_27_1" runat="server" BoxLabel="自体" Width="100" Name="opt_27" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_27_2" runat="server" BoxLabel="人工血管" Width="200" Name="opt_27" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                                <ext:RadioGroup ID="RadioGroup27" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Label ID="Label27a" runat="server" Text="部位:" Cls="label2" Width="30" />
                                        <ext:Radio ID="opt_27a_1" runat="server" BoxLabel="上左" Width="100" Name="opt_27a" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_27a_2" runat="server" BoxLabel="上右" Width="100" Name="opt_27a" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_27a_3" runat="server" BoxLabel="下左" Width="100" Name="opt_27a" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_27a_4" runat="server" BoxLabel="下右" Width="100" Name="opt_27a" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label27b" runat="server" Text="肢-术后" Cls="label2" Width="30" />
                                        <ext:TextField ID="txt_27" runat="server" Width="80" IndicatorText="天" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet28" runat="server" Title="2.上次透析穿刺状况" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup28" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_28_1" runat="server" BoxLabel="顺利" Width="100" Name="opt_28" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_28_2" runat="server" BoxLabel="二次穿刺" Width="150" Name="opt_28" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_28_3" runat="server" BoxLabel="穿刺处外观正常" Width="250" Name="opt_28" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_28_4" runat="server" BoxLabel="瘀血" Width="100" Name="opt_28" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_28_5" runat="server" BoxLabel="肿胀" Width="100" Name="opt_28" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet29" runat="server" Title="3.触诊/听诊血管杂音" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup29" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_29_1" runat="server" BoxLabel="正常" Width="100" Name="opt_29" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_29_2" runat="server" BoxLabel="弱" Width="100" Name="opt_29" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_29_3" runat="server" BoxLabel="无" Width="100" Name="opt_29" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet30" runat="server" Title="4.内瘘成熟训练" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup30" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_30_1" runat="server" BoxLabel="无" Width="100" Name="opt_30" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_30_2" runat="server" BoxLabel="有" Width="100" Name="opt_30" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_30" runat="server" Width="100" IndicatorText="次/日" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet31" runat="server" Title="5.内瘘使用年限" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup31" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_31_1" runat="server" BoxLabel="一个月内" Width="200" Name="opt_31" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_31_2" runat="server" BoxLabel="六个月内" Width="200" Name="opt_31" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_31_3" runat="server" BoxLabel="一年内" Width="200" Name="opt_31" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_31_4" runat="server" BoxLabel="一年以上" Width="200" Name="opt_31" CheckedCls="CheckBox-red" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel6" runat="server" Collapsible="true" ColumnWidth="1" Layout="AnchorLayout" Title="健康教育指导" BodyStyle="background-color:#EBF5FF !important;">
                    <Items>
                        <ext:FieldSet ID="FieldSet32" runat="server" Title="1.健康教育方式" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                            <ext:Container ID="Container32" runat="server" Layout="ColumnLayout" Padding="2">
                                <Items>
                                    <ext:Checkbox ID="chk_32_1" runat="server" BoxLabel="口头宣教" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                    <ext:Checkbox ID="chk_32_2" runat="server" BoxLabel="健教单张" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                    <ext:Checkbox ID="chk_32_3" runat="server" BoxLabel="PPT讲解" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                    <ext:Checkbox ID="chk_32_4" runat="server" BoxLabel="视频" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                </Items>
                            </ext:Container>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet33" runat="server" Title="2.饮食指导" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:Container ID="Container33" runat="server" Layout="ColumnLayout" Padding="2">
                                    <Items>
                                        <ext:Checkbox ID="chk_33_1" runat="server" BoxLabel="饮水控制" Width="150" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_33_2" runat="server" BoxLabel="蛋白质摄入" Width="180" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_33_3" runat="server" BoxLabel="高钾食物" Width="150" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_33_4" runat="server" BoxLabel="高磷食物" Width="150" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_33_5" runat="server" BoxLabel="怎样吃盐" Width="150" CheckedCls="CheckBox-red" Flex="1" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet34" runat="server" Title="3.运动指导" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:Container ID="Container34" runat="server" Layout="ColumnLayout" Padding="2">
                                    <Items>
                                        <ext:Checkbox ID="chk_34_1" runat="server" BoxLabel="那些运动可以做" Width="250" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_34_2" runat="server" BoxLabel="预防跌倒的方法" Width="250" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_34_3" runat="server" BoxLabel="立即停止运动的时刻" Width="300" CheckedCls="CheckBox-red" Flex="1" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet35" runat="server" Title="4.血管通路指导" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:Container ID="Container35" runat="server" Layout="ColumnLayout" Padding="2">
                                    <Items>
                                        <ext:Checkbox ID="chk_35_1" runat="server" BoxLabel="日常护理" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_35_2" runat="server" BoxLabel="内廔闭塞处理" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_35_3" runat="server" BoxLabel="止血带的使用" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_35_4" runat="server" BoxLabel="内廔成熟训练" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet36" runat="server" Title="5.体重管理" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                    <Items>
                                        <ext:Checkbox ID="chk_36_1" runat="server" BoxLabel="何谓干体重" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_36_2" runat="server" BoxLabel="体重增加的标准" Width="250" CheckedCls="CheckBox-red" Flex="1" />
                                        <ext:Checkbox ID="chk_36_3" runat="server" BoxLabel="干体重的调整" Width="200" CheckedCls="CheckBox-red" Flex="1" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>

                        <ext:FieldSet ID="FieldSet37" runat="server" Title="6.受教者" Layout="AnchorLayout" DefaultAnchor="100%">
                            <Items>
                                <ext:RadioGroup ID="RadioGroup37" runat="server" Anchor="none" Cls="Radio-black">
                                    <Items>
                                        <ext:Radio ID="opt_37_1" runat="server" BoxLabel="患者本人" Width="150" Name="opt_37" CheckedCls="CheckBox-red" />
                                        <ext:Label ID="Label37_1" runat="server" Text="照顾者" Cls="label2" Width="40" />
                                        <ext:Radio ID="opt_37_2" runat="server" BoxLabel="配偶" Width="100" Name="opt_37" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_37_3" runat="server" BoxLabel="父母" Width="100" Name="opt_37" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_37_4" runat="server" BoxLabel="子女" Width="100" Name="opt_37" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_37_5" runat="server" BoxLabel="护工" Width="100" Name="opt_37" CheckedCls="CheckBox-red" />
                                        <ext:Radio ID="opt_37_6" runat="server" BoxLabel="其他" Width="100" Name="opt_37" CheckedCls="CheckBox-red" />
                                        <ext:TextField ID="txt_37" runat="server" FieldLabel="" Width="80" />
                                    </Items>
                                </ext:RadioGroup>
                            </Items>
                        </ext:FieldSet>
                    </Items>
                </ext:Panel>
                <ext:Container ID="Container3" runat="server" Layout="AutoLayout" Region="Center">
                    <Items>
                        <ext:Button ID="BtnSave" runat="server" Text="评估表存盘" ColumnWidth=".2" Height="50">
                            <DirectEvents>
                                <Click OnEvent="BtnSave_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
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
    </div>
    </form>
</body>
</html>
