<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_02.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_06_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>体格检查记录</title>
    <style type="text/css">
        .mylabel
        {
            color: Blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server" Theme="Default" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="体格检查记录" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="记录日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="体格检查记录" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_1" runat="server" FieldLabel="T" IndicatorText="℃" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="P" IndicatorText="次/分" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="R" IndicatorText="次/分" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="身高" IndicatorText="cm" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="体重" IndicatorText="kg" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="BP" IndicatorText="mmHg" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="BP部位" />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="尿量" IndicatorText="ml/24h" />
                                                <ext:Button ID="Button22" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_9">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_9_1" runat="server" FieldLabel="营养" BoxLabel="良好" Name="opt_9" />
                                                <ext:Radio ID="opt_9_2" runat="server" BoxLabel="中等" Name="opt_9" />
                                                <ext:Radio ID="opt_9_3" runat="server" BoxLabel="不良" Name="opt_9" />
                                                <ext:Button ID="Button1" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_10">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_10_1" runat="server" FieldLabel="面容" BoxLabel="急性" Name="opt_10" />
                                                <ext:Radio ID="opt_10_2" runat="server" BoxLabel="慢性" Name="opt_10" />
                                                <ext:Radio ID="opt_10_3" runat="server" BoxLabel="痛苦" Name="opt_10" />
                                                <ext:Radio ID="opt_10_4" runat="server" BoxLabel="安静" Name="opt_10" />
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_11">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_11_1" runat="server" FieldLabel="神志" BoxLabel="清楚" Name="opt_11" />
                                                <ext:Radio ID="opt_11_2" runat="server" BoxLabel="嗜睡" Name="opt_11" />
                                                <ext:Radio ID="opt_11_3" runat="server" BoxLabel="昏睡" Name="opt_11" />
                                                <ext:Radio ID="opt_11_4" runat="server" BoxLabel="昏迷" Name="opt_11" />
                                                <ext:Radio ID="opt_11_5" runat="server" BoxLabel="谵妄" Name="opt_11" />
                                                <ext:Radio ID="opt_11_6" runat="server" BoxLabel="模糊" Name="opt_11" />
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_12">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_12_1" runat="server" FieldLabel="体位" BoxLabel="平卧" Name="opt_12" />
                                                <ext:Radio ID="opt_12_2" runat="server" BoxLabel="半卧" Name="opt_12" />
                                                <ext:Radio ID="opt_12_3" runat="server" BoxLabel="端坐" Name="opt_12" />
                                                <ext:Radio ID="opt_12_4" runat="server" BoxLabel="自动体位" Name="opt_12" />
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <%--<Listeners>
                                                        <Click Handler="#{opt_14_1}.setDisabled(true);#{opt_14_2}.setDisabled(true);#{opt_14_3}.setDisabled(true);" />
                                                    </Listeners>--%>
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_13;opt_14">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_13_1" runat="server" FieldLabel="贫血貌" BoxLabel="无" Name="opt_13">
                                                    
                                                </ext:Radio>
                                                <ext:Radio ID="opt_13_2" runat="server" BoxLabel="有" Name="opt_13">
                                                    <DirectEvents>
                                                        <Change OnEvent="aa"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_14_1" runat="server" BoxLabel="轻" Name="opt_14" Hidden="true" />
                                                <ext:Radio ID="opt_14_2" runat="server" BoxLabel="中" Name="opt_14" Hidden="true" />
                                                <ext:Radio ID="opt_14_3" runat="server" BoxLabel="重" Name="opt_14" Hidden="true" />
                                                <ext:Button ID="Button6" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_15;opt_1">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_1_1" runat="server" FieldLabel="皮肤" BoxLabel="正常" Name="opt_1" />
                                                    
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="异常" Name="opt_1" >
                                                    <DirectEvents>
                                                        <Change OnEvent="bb"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>   
                                                <ext:Radio ID="opt_15_1" runat="server" BoxLabel="皮疹" Name="opt_15" Hidden="true" />
                                                <ext:Radio ID="opt_15_2" runat="server" BoxLabel="出血" Name="opt_15" Hidden="true" />
                                                <ext:Radio ID="opt_15_3" runat="server" BoxLabel="灰暗" Name="opt_15" Hidden="true"  />
                                                <ext:Radio ID="opt_15_4" runat="server" BoxLabel="尿素霜" Name="opt_15" Hidden="true"/>
                                                <ext:Radio ID="opt_15_5" runat="server" BoxLabel="抓痕" Name="opt_15" Hidden="true"/>
                                                
                                                <ext:Button ID="Button7" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <%--<Listeners>
                                                        <Click Handler="#{opt_17_1}.setDisabled(true);#{opt_17_2}.setDisabled(true);#{opt_17_3}.setDisabled(true);" />
                                                    </Listeners>--%>
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_16;opt_17">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_16_1" runat="server" FieldLabel="黄疸" BoxLabel="无" Name="opt_16">
                                                    
                                                </ext:Radio>
                                                <ext:Radio ID="opt_16_2" runat="server" BoxLabel="有" Name="opt_16">
                                                    <DirectEvents>
                                                        <Change OnEvent="cc"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_17_1" runat="server" BoxLabel="轻" Name="opt_17" Hidden="true" />
                                                <ext:Radio ID="opt_17_2" runat="server" BoxLabel="中" Name="opt_17" Hidden="true" />
                                                <ext:Radio ID="opt_17_3" runat="server" BoxLabel="重" Name="opt_17" Hidden="true"/>
                                                <ext:Button ID="Button9" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    
                                                    <%--<Listeners>
                                                        <Click Handler="#{opt_19_1}.setDisabled(true);#{opt_19_2}.setDisabled(true);#{opt_19_3}.setDisabled(true);#{txt_19}.setValue('');" />
                                                    </Listeners>--%>
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_18;opt_19">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                    
                                                </ext:Button>
                                                <ext:Radio ID="opt_18_1" runat="server" FieldLabel="水肿" BoxLabel="无" Name="opt_18">
                                                    
                                                    <%--<Listeners>
                                                        <Change Handler="if(this.checked){#{txt_19}.setValue('');}" />
                                                    </Listeners>--%>
                                                    <%--<DirectEvents>
                                                        <Change OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_19">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Change>
                                                    </DirectEvents>--%>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_18_2" runat="server" BoxLabel="有" Name="opt_18">
                                                    <DirectEvents>
                                                        <Change OnEvent="dd"></Change>
                                                    </DirectEvents>
                                                    <%--<Listeners>
                                                        <Change Handler="if(this.checked){#{opt_19_1}.setDisabled(false);#{opt_19_2}.setDisabled(false);#{opt_19_3}.setDisabled(false);#{txt_19}.setDisabled(false);}else{#{opt_19_1}.setDisabled(true);#{opt_19_2}.setDisabled(true);#{opt_19_3}.setDisabled(true);#{txt_19}.setDisabled(true);}" />
                                                    </Listeners>--%>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_19_1" runat="server" BoxLabel="轻" Name="opt_19" Hidden="true"/>
                                                <ext:Radio ID="opt_19_2" runat="server" BoxLabel="中" Name="opt_19" Hidden="true"/>
                                                <ext:Radio ID="opt_19_3" runat="server" BoxLabel="重" Name="opt_19" Hidden="true"/>
                                                <ext:TextField ID="txt_19" runat="server" FieldLabel="部位" Hidden="true" LabelAlign="Right"/>
                                                <ext:Button ID="Button11" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_20">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_20_1" runat="server" FieldLabel="视力" BoxLabel="正常" Name="opt_20" />
                                                <ext:Radio ID="opt_20_2" runat="server" BoxLabel="模糊" Name="opt_20" />
                                                <ext:Radio ID="opt_20_3" runat="server" BoxLabel="光感" Name="opt_20" />
                                                <ext:Radio ID="opt_20_4" runat="server" BoxLabel="失明" Name="opt_20" />
                                                <ext:Button ID="Button12" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <Listeners>
                                                        <Click Handler="#{txt_22}.setValue('');" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_21">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        
                                                    
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_21_1" runat="server" FieldLabel="五官" BoxLabel="正常" Name="opt_21">
                                                    <Listeners>
                                                        <Change Handler="if(this.checked){#{txt_22}.setValue('');}" />
                                                    </Listeners>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_21_2" runat="server" BoxLabel="异常" Name="opt_21">
                                                    <DirectEvents>
                                                        <Change OnEvent="ee"></Change>
                                                    </DirectEvents>
                                                    <%--<Listeners>
                                                        <Change Handler="if(this.checked){#{txt_22}.setDisabled(false);}else{#{txt_22}.setDisabled(true);}" />
                                                    </Listeners>--%>
                                                </ext:Radio>
                                                <ext:TextField ID="txt_22" runat="server" FieldLabel="具体描述" Hidden="true" LabelAlign="Right"/>
                                                <ext:Button ID="Button13" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_23">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_23_1" runat="server" FieldLabel="颈静脉" BoxLabel="正常" Name="opt_23" />
                                                <ext:Radio ID="opt_23_2" runat="server" BoxLabel="怒张" Name="opt_23" />
                                                <ext:Button ID="Button14" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_24">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_24_1" runat="server" FieldLabel="甲状腺" BoxLabel="正常" Name="opt_24" />
                                                <ext:Radio ID="opt_24_2" runat="server" BoxLabel="增大" Name="opt_24" />
                                                <ext:Button ID="Button15" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_25">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_25_1" runat="server" FieldLabel="肺部呼吸音" BoxLabel="正常" Name="opt_25" />
                                                <ext:Radio ID="opt_25_2" runat="server" BoxLabel="增强" Name="opt_25" />
                                                <ext:Radio ID="opt_25_3" runat="server" BoxLabel="减弱" Name="opt_25" />
                                                <ext:Button ID="Button16" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <Listeners>
                                                        <Click Handler="#{txt_27}.setValue('');" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_26">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_26_1" runat="server" FieldLabel="啰音" BoxLabel="无" Name="opt_26">
                                                    <Listeners>
                                                        <Change Handler="if(this.checked){#{txt_27}.setValue('');}" />
                                                    </Listeners>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_26_2" runat="server" BoxLabel="有" Name="opt_26">
                                                    <DirectEvents>
                                                        <Change OnEvent="ff"></Change>
                                                    </DirectEvents>
                                                    <%--<Listeners>
                                                        <Change Handler="if(this.checked){#{txt_27}.setDisabled(false);}else{#{txt_27}.setDisabled(true);}" />
                                                    </Listeners>--%>
                                                </ext:Radio>
                                                <ext:TextField ID="txt_27" runat="server" FieldLabel="具体描述" Hidden="true" LabelAlign="Right"/>
                                                <ext:Label ID="Label1" runat="server" Text="心脏" Cls="mylabel" />
                                                <ext:Container ID="Container13" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button17" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_28">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_28_1" runat="server" FieldLabel="心界" BoxLabel="正常" Name="opt_28" />
                                                <ext:Radio ID="opt_28_2" runat="server" BoxLabel="扩大" Name="opt_28" />
                                                <ext:TextField ID="txt_29" runat="server" FieldLabel="心尖博动距左锁骨中线" IndicatorText="cm"
                                                    LabelAlign="Right" />
                                                <ext:TextField ID="txt_30" runat="server" FieldLabel="心率" IndicatorText="次/分" LabelAlign="Right" />
                                                <ext:Button ID="Button18" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_31">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_31_1" runat="server" FieldLabel="心律" BoxLabel="齐" Name="opt_31" />
                                                <ext:Radio ID="opt_31_2" runat="server" BoxLabel="不齐" Name="opt_31" />
                                                <ext:Button ID="Button19" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <Listeners>
                                                        <Click Handler="#{txt_33}.setValue('');" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_32">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_32_1" runat="server" FieldLabel="杂音" BoxLabel="无" Name="opt_32">
                                                    <Listeners>
                                                        <Change Handler="if(this.checked){#{txt_33}.setValue('')}" />
                                                    </Listeners>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_32_2" runat="server" BoxLabel="有" Name="opt_32">
                                                    <DirectEvents>
                                                        <Change OnEvent="gg"></Change>
                                                    </DirectEvents>
                                                    <%--<Listeners>
                                                        <Change Handler="if(this.checked){#{txt_33}.setDisabled(false);}else{#{txt_33}.setDisabled(true);}" />
                                                    </Listeners>--%>
                                                </ext:Radio>
                                                <ext:TextField ID="txt_33" runat="server" FieldLabel="具体描述" Hidden="true" LabelAlign="Right"/>
                                                <ext:Button ID="Button20" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_34">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_34_1" runat="server" FieldLabel="心包磨擦音" BoxLabel="无" Name="opt_34" />
                                                <ext:Radio ID="opt_34_2" runat="server" BoxLabel="有" Name="opt_34" />
                                                <ext:Label ID="Label2" runat="server" Text="腹部" Cls="mylabel" />
                                                <ext:Container ID="Container4" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button21" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_35">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_35_1" runat="server" FieldLabel="腹部" BoxLabel="隆" Name="opt_35"/>
                                                <ext:Radio ID="opt_35_2" runat="server" BoxLabel="平" Name="opt_35" />
                                                <ext:Radio ID="opt_35_3" runat="server" BoxLabel="凹" Name="opt_35" />
                                                <ext:Button ID="Button23" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <Listeners>
                                                        <Click Handler="#{txt_37}.setValue('');" />
                                                    </Listeners>
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_36">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_36_1" runat="server" FieldLabel="压痛" BoxLabel="无" Name="opt_36">
                                                   <%-- <Listeners>
                                                        <Change Handler="if(this.checked){#{txt_37}.setValue('')}" />
                                                    </Listeners>--%>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_36_2" runat="server" BoxLabel="有" Name="opt_36">
                                                    <DirectEvents>
                                                        <Change OnEvent="hh"></Change>
                                                    </DirectEvents>
                                                   <%-- <Listeners>
                                                        <Change Handler="if(this.checked){#{txt_37}.setDisabled(false);}else{#{txt_37}.setDisabled(true);}" />
                                                    </Listeners>--%>
                                                </ext:Radio>
                                                <ext:TextField ID="txt_37" runat="server" FieldLabel="具体描述" Hidden="true" LabelAlign="Right"/>

                                                <ext:Label ID="Label3" runat="server" Text="肝,脾" Cls="mylabel" />
                                                <ext:TextField ID="txt_38" runat="server" FieldLabel="肋下" LabelAlign="Right" IndicatorText="cm" />
                                                <ext:TextField ID="txt_39" runat="server" FieldLabel="剑下" LabelAlign="Right" IndicatorText="cm" />
                                                <ext:Button ID="Button24" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_40">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_40_1" runat="server" FieldLabel="质地" BoxLabel="质软" Name="opt_40"/>
                                                <ext:Radio ID="opt_40_2" runat="server" BoxLabel="质韧" Name="opt_40" />
                                                <ext:Radio ID="opt_40_3" runat="server" BoxLabel="质硬" Name="opt_40" />
                                                <ext:TextField ID="txt_41" runat="server" FieldLabel="脾:肋下" LabelAlign="Right" IndicatorText="cm" />
                                                <%--<ext:Button ID="Button25" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_43">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_43_1" runat="server" FieldLabel = "下肢凹陷性浮肿" BoxLabel="无" Name="opt_43"  LabelAlign = "Right" />
                                                <ext:Radio ID="opt_43_2" runat="server" BoxLabel="轻" Name="opt_43" />
                                                <ext:Radio ID="opt_43_3" runat="server" BoxLabel="中" Name="opt_43" />
                                                <ext:Radio ID="opt_43_4" runat="server" BoxLabel="重" Name="opt_43" />--%>
                                                <ext:Button ID="Button26" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_42">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_42_1" runat="server" FieldLabel="四肢动脉搏动" BoxLabel="正常" Name="opt_42"/>
                                                <ext:Radio ID="opt_42_2" runat="server" BoxLabel="减弱" Name="opt_42" />
                                                <ext:Radio ID="opt_42_3" runat="server" BoxLabel="消失" Name="opt_42" />
                                                <ext:TextField ID="txt_43" runat="server" FieldLabel="部位" LabelAlign="Right" />
                                                
                                                <ext:Radio ID="opt_2_1" runat="server" FieldLabel="骨关节" BoxLabel="正常" Name="opt_2" />
                                                <ext:Radio ID="opt_2_2" runat="server" BoxLabel="异常" Name="opt_2" >
                                                    <DirectEvents>
                                                        <Change OnEvent="ii"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Checkbox ID="chk_44_1" runat="server" BoxLabel="畸形" Hidden="true" />
                                                <ext:Checkbox ID="chk_44_2" runat="server" BoxLabel="叩痛" Hidden="true"/>
                                                <ext:Checkbox ID="chk_44_3" runat="server" BoxLabel="红肿" Hidden="true"/>
                                                <ext:Checkbox ID="chk_44_4" runat="server" BoxLabel="活动受限" Hidden="true"/>
                                                <ext:TextField ID="txt_45" runat="server" FieldLabel="部位" LabelAlign="Right" Hidden="true"/>
                                                <ext:Label ID="Label5" runat="server" Text="神经系统" Cls="mylabel" />
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Button ID="Button27" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <Listeners>
                                                        <Click Handler="#{txt_47}.setValue('');" />
                                                    </Listeners>
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_46;txt_47">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                        <ext:Radio ID="opt_46_1" runat="server" FieldLabel="肌力" BoxLabel="正常" Name="opt_46">
                                                            <Listeners>
                                                                <Change Handler="if(this.checked){#{txt_47}.setValue('')}" />
                                                            </Listeners>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_46_2" runat="server" BoxLabel="异常" Name="opt_46">
                                                            <%--<Listeners>
                                                                <Change Handler="if(this.checked){#{txt_47}.setDisabled(false);}else{#{txt_47}.setDisabled(true);}" />
                                                            </Listeners>--%>
                                                            <DirectEvents>
                                                        <Change OnEvent="jj"></Change>
                                                    </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_47" runat="server" FieldLabel="具体描述" Hidden="true" LabelAlign="Right"/>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <Listeners>
                                                        <Click Handler="#{txt_49}.setValue('');" />
                                                    </Listeners>
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_48">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                        <ext:Radio ID="opt_48_1" runat="server" FieldLabel="肌张力" BoxLabel="正常" Name="opt_48">
                                                            <Listeners>
                                                                <Change Handler="if(this.checked){#{txt_49}.setValue('')}" />
                                                            </Listeners>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_48_2" runat="server" BoxLabel="异常" Name="opt_48">
                                                           <%-- <Listeners>
                                                                <Change Handler="if(this.checked){#{txt_49}.setDisabled(false);}else{#{txt_49}.setDisabled(true);}" />
                                                            </Listeners>--%>
                                                            <DirectEvents>
                                                        <Change OnEvent="kk"></Change>
                                                    </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_49" runat="server" FieldLabel="具体描述" Hidden="true" LabelAlign="Right"/>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Button ID="Button8" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <Listeners>
                                                        <Click Handler="#{txt_51}.setValue('');" />
                                                    </Listeners>
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_50">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                        <ext:Radio ID="opt_50_1" runat="server" FieldLabel="生理反射" BoxLabel="正常" Name="opt_50">
                                                            <Listeners>
                                                                <Change Handler="if(this.checked){#{txt_51}.setValue('')}" />
                                                            </Listeners>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_50_2" runat="server" BoxLabel="异常" Name="opt_50">
                                                            <%--<Listeners>
                                                                <Change Handler="if(this.checked){#{txt_51}.setDisabled(false);}else{#{txt_51}.setDisabled(true);}" />
                                                            </Listeners>--%>
                                                            <DirectEvents>
                                                        <Change OnEvent="ll"></Change>
                                                    </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_51" runat="server" FieldLabel="具体描述" Hidden="true" LabelAlign="Right"/>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label6" runat="server" Text="病理反射" Cls="mylabel" />
                                                <ext:Container ID="Container5" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button30" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <Listeners>
                                                        <Click Handler="#{txt_53}.setValue('');" />
                                                    </Listeners>
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_52">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_52_1" runat="server" FieldLabel="病理反射" BoxLabel="正常" Name="opt_52" >
                                                    <Listeners>
                                                        <Change Handler="if(this.checked){#{txt_53}.setValue('')}" />
                                                    </Listeners>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_52_2" runat="server" BoxLabel="异常" Name="opt_52">
                                                    <%--<Listeners>
                                                        <Change Handler="if(this.checked){#{txt_53}.setDisabled(false);}else{#{txt_53}.setDisabled(true);}" />
                                                    </Listeners>--%>
                                                    <DirectEvents>
                                                        <Change OnEvent="mm"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:TextField ID="txt_53" runat="server" FieldLabel="具体描述" Hidden="true" LabelAlign="Right"/>
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
                                <%--<ext:Button ID="Btn_Close" runat="server" Icon="Disk" Text="关闭" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>--%>
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
