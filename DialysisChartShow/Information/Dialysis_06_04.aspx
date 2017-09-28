<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_04.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_04" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>实验室检查</title>
    <style type="text/css">
    .mylabel
    {
         color:Blue;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server" Theme="Default" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="实验室检查" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="血常规" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:DateField ID="dat_1" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="Hb" IndicatorText="g/L" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="Hct" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="WBC" IndicatorText="×10^9/L" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="N"  />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="PLT"  IndicatorText="×10^9/L"/>
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="尿常规"  Width = "500"/>
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="尿沉渣"  />
                                                <ext:TextField ID="txt_9" runat="server" FieldLabel="其它"  />
                                                <%--<ext:TextField ID="txt_10" runat="server" FieldLabel="尿Na+" IndicatorText = "mmol/L"/>
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="尿渗透压" IndicatorText = "mOsm/kg‧H2O"/>--%>
                                                <%--<ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_12">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_12_1" runat="server"  BoxLabel="禁饮" Name="opt_12" />
                                                <ext:Radio ID="opt_12_2" runat="server" BoxLabel="任意尿" Name="opt_12" />--%>
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
                                                <ext:Radio ID="opt_13_1" runat="server" FieldLabel="粪潜血 " BoxLabel="阴性" Name="opt_13" />
                                                <ext:Radio ID="opt_13_2" runat="server" BoxLabel="阳性" Name="opt_13">
                                                    <%--<Listeners>
                                                        <Change Handler="if(this.checked){#{txt_14}.setDisabled(false);}else{#{txt_14}.setDisabled(true);}" />
                                                    </Listeners>--%>
                                                </ext:Radio>
                                                <ext:TextField ID="txt_14" runat="server" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="血生化" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:DateField ID="dat_15" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:TextField ID="txt_16" runat="server" FieldLabel="Ccr" IndicatorText="ml/min" />
                                                <ext:TextField ID="txt_17" runat="server" FieldLabel="BUN" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_18" runat="server" FieldLabel="Scr" IndicatorText=" μmol/L" />
                                                <ext:TextField ID="txt_19" runat="server" FieldLabel="UA" IndicatorText="μmol/L" />
                                                <ext:TextField ID="txt_20" runat="server" FieldLabel="K+" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_21" runat="server" FieldLabel="Na+" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_22" runat="server" FieldLabel="CL-" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_23" runat="server" FieldLabel="Ca2+" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_24" runat="server" FieldLabel="P3-" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_25" runat="server" FieldLabel="AKP" IndicatorText="u/L" />
                                                <ext:TextField ID="txt_26" runat="server" FieldLabel="T-CO2" IndicatorText="mmol/L" />
                                                <ext:Label ID = "Label10" runat  = "server" Text = "血糖" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_27" runat="server" FieldLabel="FBG" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_28" runat="server" FieldLabel="2hPG" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_29" runat="server" FieldLabel="HbA1" IndicatorText="%" />
                                                <ext:Label ID = "Label1" runat  = "server" Text = "肝功能" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_30" runat="server" FieldLabel="总胆红素" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_31" runat="server" FieldLabel="直接胆红素" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_32" runat="server" FieldLabel="ALT" IndicatorText="u/L" />
                                                <ext:TextField ID="txt_33" runat="server" FieldLabel="ALB" IndicatorText="u/L" />
                                                <ext:TextField ID="txt_34" runat="server" FieldLabel="Y-GT" IndicatorText="u/L" />
                                                <ext:TextField ID="txt_35" runat="server" FieldLabel="A/G"/>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="CKD者检查" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID = "Label3" runat  = "server" Text = "血脂" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_36" runat="server" FieldLabel="Chol" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_37" runat="server" FieldLabel="TG" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_38" runat="server" FieldLabel="HDL-CH" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_39" runat="server" FieldLabel="LDL-CH" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_40" runat="server" FieldLabel="iPTH" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_41" runat="server" FieldLabel="CRP" IndicatorText="mg/L" />
                                                <ext:TextField ID="txt_42" runat="server" FieldLabel="血清前白蛋白" IndicatorText="g/L" />
                                                <ext:Label ID = "Label4" runat  = "server" Text = "铁代谢" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_43" runat="server" FieldLabel="血清铁蛋白(SF)" IndicatorText="μg/L" />
                                                <%--<ext:TextField ID="txt_45" runat="server" FieldLabel="转运铁蛋白(TF)" IndicatorText="g/L" />--%>
                                                <ext:TextField ID="txt_46" runat="server" FieldLabel="血清铁(SI)" IndicatorText="μmol/L" />
                                                <ext:TextField ID="txt_47" runat="server" FieldLabel="总铁结合力" IndicatorText="μmol/L" />
                                                <ext:TextField ID="txt_44" runat="server" FieldLabel="转铁蛋白饱和度" IndicatorText="%" />
                                                <ext:Label ID = "Label2" runat  = "server" Text = "病毒学指标" Cls = "mylabel"/>
                                                <ext:Container ID="Container1" runat="server">
                                                    <Items>
                                                        <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_48">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_48_1" runat="server" FieldLabel="HbsAg" BoxLabel="阴性" Name="opt_48" />
                                                <ext:Radio ID="opt_48_2" runat="server" BoxLabel="阳性" Name="opt_48" />
                                                <ext:Radio ID="opt_48_3" runat="server" BoxLabel="弱阳性" Name="opt_48" />
                                                <ext:Radio ID="opt_48_4" runat="server" BoxLabel="未检" Name="opt_48" />
                                                    
                                                
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_49">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_49_1" runat="server" FieldLabel="HbsAb" BoxLabel="阴性" Name="opt_49" />
                                                <ext:Radio ID="opt_49_2" runat="server" BoxLabel="阳性" Name="opt_49"/>
                                                <ext:Radio ID="opt_49_3" runat="server" BoxLabel="弱阳性" Name="opt_49"/>
                                                <ext:Radio ID="opt_49_4" runat="server" BoxLabel="未检" Name="opt_49"/>
                                                <ext:Button ID="Button1" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_50">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_50_1" runat="server" FieldLabel="HbeAg" BoxLabel="阴性" Name="opt_50" />
                                                <ext:Radio ID="opt_50_2" runat="server" BoxLabel="阳性" Name="opt_50"/>
                                                <ext:Radio ID="opt_50_3" runat="server" BoxLabel="弱阳性" Name="opt_50"/>
                                                <ext:Radio ID="opt_50_4" runat="server" BoxLabel="未检" Name="opt_50"/>
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_51">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_51_1" runat="server" FieldLabel="HbeAb" BoxLabel="阴性" Name="opt_51" />
                                                <ext:Radio ID="opt_51_2" runat="server" BoxLabel="阳性" Name="opt_51"/>
                                                <ext:Radio ID="opt_51_3" runat="server" BoxLabel="弱阳性" Name="opt_51"/>
                                                <ext:Radio ID="opt_51_4" runat="server" BoxLabel="未检" Name="opt_51"/>
                                                <ext:Button ID="Button7" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_52">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_52_1" runat="server" FieldLabel="HbcAb" BoxLabel="阴性" Name="opt_52" />
                                                <ext:Radio ID="opt_52_2" runat="server" BoxLabel="阳性" Name="opt_52"/>
                                                <ext:Radio ID="opt_52_3" runat="server" BoxLabel="弱阳性" Name="opt_52"/>
                                                <ext:Radio ID="opt_52_4" runat="server" BoxLabel="未检" Name="opt_52"/>
                                                <ext:Button ID="Button8" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_53">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_53_1" runat="server" FieldLabel="HCV" BoxLabel="阴性" Name="opt_53" />
                                                <ext:Radio ID="opt_53_2" runat="server" BoxLabel="阳性" Name="opt_53"/>
                                                <ext:Radio ID="opt_53_3" runat="server" BoxLabel="弱阳性" Name="opt_53"/>
                                                <ext:Radio ID="opt_53_4" runat="server" BoxLabel="未检" Name="opt_53"/>
                                                <ext:Button ID="Button9" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_54">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_54_1" runat="server" FieldLabel="HIV-Ab" BoxLabel="阴性" Name="opt_54" />
                                                <ext:Radio ID="opt_54_2" runat="server" BoxLabel="阳性" Name="opt_54"/>
                                                <ext:Radio ID="opt_54_3" runat="server" BoxLabel="弱阳性" Name="opt_54"/>
                                                <ext:Radio ID="opt_54_4" runat="server" BoxLabel="未检" Name="opt_54"/>
                                                <ext:Button ID="Button10" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_55">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_55_1" runat="server" FieldLabel="TP-XC" BoxLabel="阴性" Name="opt_55" />
                                                <ext:Radio ID="opt_55_2" runat="server" BoxLabel="阳性" Name="opt_55"/>
                                                <ext:Radio ID="opt_55_3" runat="server" BoxLabel="弱阳性" Name="opt_55"/>
                                                <ext:Radio ID="opt_55_4" runat="server" BoxLabel="未检" Name="opt_55"/>
                                                <%--<ext:TextField ID="txt_48" runat="server" FieldLabel="乙"  />
                                                <ext:TextField ID="txt_49" runat="server" FieldLabel="丙"  />
                                                <ext:TextField ID="txt_51" runat="server" FieldLabel="HBsAg"  />
                                                <ext:TextField ID="txt_52" runat="server" FieldLabel="HBsAb"  />
                                                <ext:TextField ID="txt_53" runat="server" FieldLabel="HBcAg"  />
                                                <ext:TextField ID="txt_54" runat="server" FieldLabel="HBcAb"  />
                                                <ext:TextField ID="txt_55" runat="server" FieldLabel="HBeAb"  />--%>
                                                <ext:TextField ID="txt_56" runat="server" FieldLabel="其它"  />
                                            </Items>
                                        </ext:FieldSet>
                                        <%--<ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="AKI者检查" Layout="AnchorLayout">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID = "Label3" runat  = "server" Text = "加做血气分析" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_56" runat="server" FieldLabel="PH"/>
                                                <ext:TextField ID="txt_57" runat="server" FieldLabel="HCO3" IndicatorText="mmol/L" />
                                                <ext:TextField ID="txt_58" runat="server" FieldLabel="PO2" IndicatorText="mmHg" />
                                                <ext:TextField ID="txt_59" runat="server" FieldLabel="PCO2" IndicatorText="mmHg" />
                                            </Items>
                                        </ext:FieldSet>--%>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="器械检查" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_60" runat="server" FieldLabel="心电图" Width = "500"/>
                                                <ext:TextField ID="txt_61" runat="server" FieldLabel="胸片" Width = "500"/>
                                                <ext:TextField ID="txt_62" runat="server" FieldLabel="心胸比" IndicatorText="%" />
                                                <ext:TextField ID="txt_63" runat="server" FieldLabel="超声心动图（心功能）" Width = "500"/>
                                                <ext:TextField ID="txt_64" runat="server" FieldLabel="B超双肾" Width = "500"/>
                                                <ext:Label ID = "Label5" runat  = "server" Text = "AKI者" Cls = "mylabel"/>
                                                <ext:TextField ID="txt_65" runat="server" FieldLabel="APACHEII评分" Width = "500"/>
                                                <ext:TextField ID="txt_66" runat="server" FieldLabel="ATN指数(ATN-ISN)" Width = "500"/>
                                                <ext:TextField ID="txt_67" runat="server" FieldLabel="其它" Width = "500"/>
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
