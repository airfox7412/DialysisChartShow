<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_05_01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_05_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" >
    <title></title>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
    <ext:ResourceManager ID="ResourceManager2" runat="server" >
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit" >
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="病程记录" AutoScroll="true" ButtonAlign="Center" >
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center" >
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false" >
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="记录日期" Format="yyyy-MM-dd" IndicatorText="　(未选择)" >
                                            <DirectEvents>
                                                <Change OnEvent="GetData" >
                                                   <%-- <ExtraParams>
                                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                                    </ExtraParams>--%>
                                                </Change> 
                                            </DirectEvents>
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="透析一般参数" Layout="AnchorLayout" Collapsible="true" Collapsed="false" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_1_1" runat="server" FieldLabel="透析处方" BoxLabel="HF" Name="opt_1" />
                                                        <ext:Radio ID="opt_1_2" runat="server" BoxLabel="LF" Name="opt_1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="num_2" runat="server" FieldLabel="HD" IndicatorText="次/W" />
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_3_1" runat="server" FieldLabel="HD" BoxLabel="2h/次" Name="opt_3" />
                                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="3h/次" Name="opt_3" />
                                                        <ext:Radio ID="opt_3_3" runat="server" BoxLabel="4h/次" Name="opt_3" />
                                                        <ext:Radio ID="opt_3_4" runat="server" BoxLabel="4.5h/次" Name="opt_3" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="num_4" runat="server" FieldLabel="HDF" IndicatorText="次/W" />
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_5_1" runat="server" FieldLabel="HDF" BoxLabel="2h/次" Name="opt_5" />
                                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="3h/次" Name="opt_5" />
                                                        <ext:Radio ID="opt_5_3" runat="server" BoxLabel="4h/次" Name="opt_5" />
                                                        <ext:Radio ID="opt_5_4" runat="server" BoxLabel="4.5h/次" Name="opt_5" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_6_1" runat="server" FieldLabel="血管通路位置" BoxLabel="左臂上" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_2" runat="server" BoxLabel="左臂下" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_3" runat="server" BoxLabel="右臂上" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_4" runat="server" BoxLabel="右臂下" Name="opt_6" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_7_1" runat="server" FieldLabel="通路类型" BoxLabel="临时中心静脉置管" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_2" runat="server" BoxLabel="长期中心静脉置管" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_3" runat="server" BoxLabel="自体内瘘" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_4" runat="server" BoxLabel="移植血管" Name="opt_7" />
                                                        <%--<ext:Radio ID="opt_7_1" runat="server" FieldLabel="通路类型" BoxLabel="自体内瘘　" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_2" runat="server" BoxLabel="移植内瘘　" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_3" runat="server" BoxLabel="直接穿刺V-V　" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_4" runat="server" BoxLabel="直接穿刺A-V　" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_5" runat="server" BoxLabel="颈内V长期导管　" Name="opt_7" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container66" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                       <ext:Radio ID="opt_7_6" runat="server" FieldLabel="　" LabelAlign="Right" BoxLabel="颈内V临时导管　" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_7" runat="server" BoxLabel="颈外V长期导管　" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_8" runat="server" BoxLabel="劲外V临时导管　" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_9" runat="server" BoxLabel="股V长期导管　" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_10" runat="server" BoxLabel="股V临时导管　" Name="opt_7" />--%>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="抗凝方案" Layout="AnchorLayout" Collapsible="true" Collapsed="false" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="num_8" runat="server" FieldLabel="肝素首量" IndicatorText="mg" />
                                                <ext:TextField ID="num_9" runat="server" FieldLabel="追加量" IndicatorText="mg" />
                                                <ext:TextField ID="num_10" runat="server" FieldLabel="低分子肝素" IndicatorText="IU" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="透析器" Layout="AnchorLayout" Collapsible="true" Collapsed="false" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>         
                                                <ext:Radio ID="opt_11_1" runat="server" FieldLabel="透析器型号" BoxLabel="Toray TS-1.3S" Name="opt_11" />
                                                <ext:Radio ID="opt_11_2" runat="server" BoxLabel="Toray TS-1.3U" Name="opt_11" />
                                                <ext:Radio ID="opt_11_3" runat="server" BoxLabel="Toray TS-1.6SL" Name="opt_11" />
                                                <ext:Radio ID="opt_11_4" runat="server" BoxLabel="Toray TS-1.8SL" Name="opt_11" />
                                                <ext:Radio ID="opt_11_5" runat="server" BoxLabel="旭化成REXEED 15UC" Name="opt_11" />
                                                <ext:Radio ID="opt_11_6" runat="server" BoxLabel="尼普洛FB150U" Name="opt_11" />
                                                <ext:Radio ID="opt_11_7" runat="server" BoxLabel="B1-1.6H" Name="opt_11" />
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="其他透析器" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="体重" Layout="AnchorLayout" Collapsible="true" Collapsed="false" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="num_13" runat="server" FieldLabel="干体重" IndicatorText="kg" />
                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_14_1" runat="server" FieldLabel="干体重较前" BoxLabel="维持" Name="opt_14" />
                                                        <ext:Radio ID="opt_14_2" runat="server" BoxLabel="上调" Name="opt_14" />
                                                        <ext:Radio ID="opt_14_3" runat="server" BoxLabel="下调" Name="opt_14" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="一般情况" Layout="AnchorLayout" Collapsible="true" Collapsed="false" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label1" runat="server" Text="血压控制" />
                                                <ext:Container ID="Container17" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_15_1" runat="server" FieldLabel="血压控制" BoxLabel="好" Name="opt_15" LabelAlign="Right"/>
                                                        <ext:Radio ID="opt_15_2" runat="server" BoxLabel="一般" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_3" runat="server" BoxLabel="差" Name="opt_15" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:TextField ID="num_16" runat="server" FieldLabel="血压范围" IndicatorText="(mmHg)" Width="200" LabelWidth="100" LabelAlign="Right" />
                                                        <ext:Label ID="Label2" runat="server" Text="　~　" Width="40" />
                                                        <ext:TextField ID="num_17" runat="server" IndicatorText="(mmHg)" Width="100" />
                                                        <ext:Label ID="Label3" runat="server" Text="　/　" Width="40" />
                                                        <ext:TextField ID="num_18" runat="server" IndicatorText="(mmHg)" Width="100" />
                                                        <ext:Label ID="Label4" runat="server" Text="　~　" Width="40" />
                                                        <ext:TextField ID="num_19" runat="server" IndicatorText="(mmHg)" Width="100" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container19" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:TextField ID="num_57" runat="server" FieldLabel="平均血压" IndicatorText="(mmHg)" Width="200" LabelWidth="100" LabelAlign="Right" />
                                                        <ext:Label ID="Label8" runat="server" Text="　/　" Width="40" />
                                                        <ext:TextField ID="num_58" runat="server" IndicatorText="(mmHg)" Width="100" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextArea ID="TextArea1" runat="server" FieldLabel="血压明细" Width="400" LabelAlign="Right" Padding="2" />
                                                
                                                <ext:Label ID="Label5" runat="server" Text="容量控制" />
                                                <ext:Container ID="Container16" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_20_1" runat="server" FieldLabel="容量控制" BoxLabel="好" Name="opt_20" LabelAlign="Right"/>
                                                        <ext:Radio ID="opt_20_2" runat="server" BoxLabel="一般" Name="opt_20" />
                                                        <ext:Radio ID="opt_20_3" runat="server" BoxLabel="差" Name="opt_20" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:TextField ID="num_21" runat="server" FieldLabel="容量" IndicatorText="kg" LabelAlign="Right"/>
                                                <ext:TextField ID="num_22" runat="server" FieldLabel="约占干体重" IndicatorText="%" LabelAlign="Right"/>

                                                <ext:Label ID="Label6" runat="server" Text="血管通路" />
                                                <ext:Container ID="Container15" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_23_1" runat="server" FieldLabel="血管通路功能" BoxLabel="好" Name="opt_23" LabelAlign="Right"/>
                                                    <ext:Radio ID="opt_23_2" runat="server" BoxLabel="一般" Name="opt_23" />
                                                        <ext:Radio ID="opt_23_3" runat="server" BoxLabel="差" Name="opt_23" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:TextField ID="num_24" runat="server" FieldLabel="血流量" IndicatorText="ml/min" LabelAlign="Right" Width="300"/>
                                                <ext:TextArea ID="are_25" runat="server" FieldLabel="主要不适、处理情况" Width="500" >
                                                </ext:TextArea>
                                                <ext:Container ID="Container14" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_26_1" runat="server" FieldLabel="是否住院" BoxLabel="是" Name="opt_26" />
                                                        <ext:Radio ID="opt_26_2" runat="server" BoxLabel="否" Name="opt_26" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:TextField ID="txt_27" runat="server" FieldLabel="住院病因" Width="500"/>
                                                <ext:TextField ID="txt_28" runat="server" FieldLabel="住院费用" Width="250"/>
                                                <ext:TextArea ID="are_29" runat="server" FieldLabel="住院主要检查、化验" Width="500" >
                                                </ext:TextArea>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="透析治疗总评价" Layout="AnchorLayout" Collapsible="true" Collapsed="false" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_30_1" runat="server" FieldLabel="透析一般情况" BoxLabel="平稳" Name="opt_30" />
                                                        <ext:Radio ID="opt_30_2" runat="server" BoxLabel="基本平稳" Name="opt_30" />
                                                        <ext:Radio ID="opt_30_3" runat="server" BoxLabel="有症状" Name="opt_30" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:TextField ID="num_31" runat="server" FieldLabel="URR" IndicatorText="%" />
                                                <ext:TextField ID="num_32" runat="server" FieldLabel="KT/V" />
                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_33_1" runat="server" FieldLabel="心脑血管系统" BoxLabel="平稳" Name="opt_33" />
                                                        <ext:Radio ID="opt_33_2" runat="server" BoxLabel="基本平稳" Name="opt_33" />
                                                        <ext:Radio ID="opt_33_3" runat="server" BoxLabel="发生相关事件" Name="opt_33" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:TextField ID="txt_34" runat="server" FieldLabel="心脑血管系统相关事件"/>
                                                <ext:TextField ID="txt_35" runat="server" FieldLabel="降压药物" />

                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_36_1" runat="server" FieldLabel="贫血程度" BoxLabel="正常" Name="opt_36" />
                                                        <ext:Radio ID="opt_36_2" runat="server" BoxLabel="輕度貧血" Name="opt_36" />
                                                        <ext:Radio ID="opt_36_3" runat="server" BoxLabel="中度貧血" Name="opt_36" />
                                                        <ext:Radio ID="opt_36_4" runat="server" BoxLabel="重度貧血" Name="opt_36" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:TextField ID="num_37" runat="server" FieldLabel="Hb" IndicatorText="g/L"/>
                                                <ext:TextField ID="num_38" runat="server" FieldLabel="EPO剂里" IndicatorText="u" />
                                                <ext:TextField ID="num_39" runat="server" FieldLabel="EPO" IndicatorText="次/周"/>
                                                <ext:TextField ID="txt_40" runat="server" FieldLabel="其他" />

                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_41_1" runat="server" FieldLabel="铁代谢" BoxLabel="正常" Name="opt_41" />
                                                        <ext:Radio ID="opt_41_2" runat="server" BoxLabel="异常" Name="opt_41" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_42_1" runat="server" FieldLabel="前白蛋白" BoxLabel="正常" Name="opt_42" />
                                                        <ext:Radio ID="opt_42_2" runat="server" BoxLabel="异常" Name="opt_42" />
                                                    </Items>
                                                </ext:Container>        
                                                <ext:TextField ID="txt_43" runat="server" FieldLabel="铁蛋白" />
                                                <ext:TextField ID="num_44" runat="server" FieldLabel="转铁蛋白饱和度" IndicatorText="%" />
                                                <ext:TextField ID="num_45" runat="server" FieldLabel="钙" IndicatorText="mmol/L" />
                                                <ext:TextField ID="num_46" runat="server" FieldLabel="磷" IndicatorText="mmol/L" />
                                                <ext:TextField ID="num_47" runat="server" FieldLabel="iPTH" IndicatorText="ng/L" />
                                                <ext:TextField ID="txt_48" runat="server" FieldLabel="营养指标" />
                                                <ext:TextField ID="txt_49" runat="server" FieldLabel="肝炎指标" />

                                                <ext:TextField ID="num_50" runat="server" FieldLabel="GPT" IndicatorText="u" />
                                                <ext:TextField ID="num_51" runat="server" FieldLabel="GOT" IndicatorText="u" />
                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2" >
                                                    <Items>
                                                        <ext:Radio ID="opt_52_1" runat="server" FieldLabel="转归" BoxLabel="退出" Name="opt_52" />
                                                        <ext:Radio ID="opt_52_2" runat="server" BoxLabel="肾移植" Name="opt_52" />
                                                        <ext:Radio ID="opt_52_3" runat="server" BoxLabel="转出" Name="opt_52" />
                                                        <ext:Radio ID="opt_52_4" runat="server" BoxLabel="死亡" Name="opt_52" >
                                                            <DirectEvents>
                                                                <Change OnEvent="ii" />
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_52_5" runat="server" BoxLabel="转入" Name="opt_52" />
                                                    </Items>
                                                </ext:Container>      
                                                <ext:Container ID="Container18" runat="server" Layout="ColumnLayout" Padding="2" Hidden="true" >
                                                    <Items>  
                                                        <%--<ext:Label ID="Label7" runat="server" Width="100" />--%>
                                                        <ext:Checkbox ID="chk_55_1" runat="server" FieldLabel="死亡原因" BoxLabel="心血管事件" />
                                                        <ext:Checkbox ID="chk_55_2" runat="server" BoxLabel="脑血管事件" />
                                                        <ext:Checkbox ID="chk_55_3" runat="server" BoxLabel="感染" />
                                                        <ext:Checkbox ID="chk_55_4" runat="server" BoxLabel="消化道出血等出血性疾病" />
                                                        <ext:TextField ID="txt_56" runat="server" FieldLabel="其他" LabelAlign="Right" />
                                                    </Items>
                                                </ext:Container>      
                                                <ext:TextField ID="txt_53" runat="server" FieldLabel="特殊病情、检查及处理" Width="500"/>
                                                <ext:TextField ID="txt_54" runat="server" FieldLabel="今后透析诊疗计划" Width="500"/>
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_print" runat="server" Icon="Printer" Text="打印" Width="100" >
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Print_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100" >
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_clear" runat="server" Icon="Disk" Text="重置" Width="100" >
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Clear_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_close" runat="server" Icon="Disk" Text="关闭" Width="100" >
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
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Title="" 
            Width="1000"
            Height="520"
            Y="0"
            Modal="true"
            AutoRender="false"
            Collapsible="true"
            Maximizable="true"
            Hidden="true" >
            <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                            <LoadMask ShowMask="true" />
                        </Loader>
        </ext:Window>
    </div>
    </form>
</body>
</html>
