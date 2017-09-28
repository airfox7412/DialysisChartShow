<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_06_Alasamo.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_06_Alasamo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>季度小结</title>
    <style type="text/css">
        .label_blue .x-label-value 
        {
            color: Blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="sel_date" runat="server" />
        
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout" PaddingSpec="0 10 0 10">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="季度小结" AutoScroll="true" ButtonAlign="Center" UI="Info" Width="1000" AutoShow="true">
                    <Items>
                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:DateField ID="dat_1" runat="server" FieldLabel="入院时间" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" Format="yyyy-MM-dd" />
                                <ext:DateField ID="info_date" runat="server" FieldLabel="小结时间" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" Format="yyyy-MM-dd" />
                                <ext:TextField ID="txt_2" runat="server" FieldLabel="透析号" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_3" runat="server" FieldLabel="血管通路" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" />
                                <ext:TextField ID="txt_4" runat="server" FieldLabel="抗凝方式" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" />
                                <ext:TextField ID="txt_5" runat="server" FieldLabel="透析器" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_6" runat="server" FieldLabel="干体重" LabelAlign="Right" LabelWidth="80" IndicatorText="Kg" Padding="5" Width="250" />
                                <ext:TextField ID="txt_7" runat="server" FieldLabel="透析处方" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" />
                            </Items>
                        </ext:Container>
                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="透析前" Layout="FitLayout" Collapsible="true" Collapsed="false" Width="1100">
                            <Items>
                                <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="txt_11" runat="server" FieldLabel="Scr" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_12" runat="server" FieldLabel="Urea" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_13" runat="server" FieldLabel="Ua" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_14" runat="server" FieldLabel="K" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_15" runat="server" FieldLabel="Na" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="txt_16" runat="server" FieldLabel="Cl" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_17" runat="server" FieldLabel="HCO3" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_18" runat="server" FieldLabel="Ca" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_19" runat="server" FieldLabel="P" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="透析后" Layout="FitLayout" Collapsible="true" Collapsed="false" Width="1100">
                            <Items>
                                <ext:Container ID="Container8" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="txt_21" runat="server" FieldLabel="Scr" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_22" runat="server" FieldLabel="Urea" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_23" runat="server" FieldLabel="Ua" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_24" runat="server" FieldLabel="K" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_25" runat="server" FieldLabel="Na" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container9" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="txt_26" runat="server" FieldLabel="Cl" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_27" runat="server" FieldLabel="HCO3" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_28" runat="server" FieldLabel="Ca" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_29" runat="server" FieldLabel="P" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                        <ext:Container ID="Container10" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_30" runat="server" FieldLabel="URR" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="200" />
                                <ext:TextField ID="txt_31" runat="server" FieldLabel="Kt/V" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="200" />
                                <ext:TextField ID="txt_32" runat="server" FieldLabel="NPCR" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="200" />
                                <ext:TextField ID="txt_33" runat="server" FieldLabel="Kru" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="200" />
                                <ext:TextField ID="txt_34" runat="server" FieldLabel="iPTH" LabelAlign="Right" LabelWidth="80" IndicatorText="pg/ml" Padding="5" Width="200" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container11" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_35" runat="server" FieldLabel="HB" LabelAlign="Right" LabelWidth="80" IndicatorText="g/L" Padding="5" Width="200" />
                                <ext:TextField ID="txt_36" runat="server" FieldLabel="RBC" LabelAlign="Right" LabelWidth="80" IndicatorText="*10^12/L" Padding="5" Width="200" />
                                <ext:TextField ID="txt_37" runat="server" FieldLabel="HCT" LabelAlign="Right" LabelWidth="80" IndicatorText="L/L" Padding="5" Width="200" />
                                <ext:TextField ID="txt_38" runat="server" FieldLabel="WBC" LabelAlign="Right" LabelWidth="80" IndicatorText="*10^9/L" Padding="5" Width="200" />
                                <ext:TextField ID="txt_39" runat="server" FieldLabel="PLT" LabelAlign="Right" LabelWidth="80" IndicatorText="*10^9/L" Padding="5" Width="200" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container12" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_40" runat="server" FieldLabel="ALT" LabelAlign="Right" LabelWidth="80" IndicatorText="u/L" Padding="5" Width="160" />
                                <ext:TextField ID="txt_41" runat="server" FieldLabel="AST" LabelAlign="Right" LabelWidth="80" IndicatorText="u/L" Padding="5" Width="160" />
                                <ext:TextField ID="txt_42" runat="server" FieldLabel="GGT" LabelAlign="Right" LabelWidth="80" IndicatorText="u/L" Padding="5" Width="160" />
                                <ext:TextField ID="txt_43" runat="server" FieldLabel="ALP" LabelAlign="Right" LabelWidth="80" IndicatorText="u/L" Padding="5" Width="160" />
                                <ext:TextField ID="txt_44" runat="server" FieldLabel="TBIL" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="180" />
                                <ext:TextField ID="txt_45" runat="server" FieldLabel="DBIL" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="180" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container13" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_48" runat="server" FieldLabel="TP" LabelAlign="Right" LabelWidth="80" IndicatorText="g/L" Padding="5" Width="200" />
                                <ext:TextField ID="txt_49" runat="server" FieldLabel="ALB" LabelAlign="Right" LabelWidth="80" IndicatorText="g/L" Padding="5" Width="200" />
                                <ext:TextField ID="txt_50" runat="server" FieldLabel="PreALB" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="200" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container14" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_51" runat="server" FieldLabel="CHO" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                <ext:TextField ID="txt_52" runat="server" FieldLabel="TG" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                                <ext:TextField ID="txt_53" runat="server" FieldLabel="LDL" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="200" />
                                <ext:TextField ID="txt_54" runat="server" FieldLabel="HDL" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="200" />
                                <ext:TextField ID="txt_55" runat="server" FieldLabel="VLDL" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="200" />
                            </Items>
                        </ext:Container>
                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="抗感染筛查" Layout="FitLayout" Collapsible="true" Collapsed="false" Width="1100">
                            <Items>
                                <ext:Container ID="Container16" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="txt_56" runat="server" FieldLabel="HBsAg" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="250" />
                                        <ext:TextField ID="txt_57" runat="server" FieldLabel="HCV-Ab" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="250" />
                                        <ext:TextField ID="txt_58" runat="server" FieldLabel="HIV-Ab" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="250" />
                                        <ext:TextField ID="txt_59" runat="server" FieldLabel="RPR-Ab" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="250" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="乙肝五项" Layout="FitLayout" Collapsible="true" Collapsed="false" Width="1100">
                            <Items>
                                <ext:Container ID="Container18" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="txt_60" runat="server" FieldLabel="HBs-Ag" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_61" runat="server" FieldLabel="HBs-Ab" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_62" runat="server" FieldLabel="HBe-Ag" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_63" runat="server" FieldLabel="HBe-Ab" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                                        <ext:TextField ID="txt_64" runat="server" FieldLabel="HBc-Ab" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:FieldSet>
                        <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:Label ID="Label5" runat="server" Text="其他治疗" PaddingSpec="0 0 0 40" Cls="label_blue" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container20" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_65" runat="server" FieldLabel="降压药" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="500" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container21" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_66" runat="server" FieldLabel="铁剂名称" LabelAlign="Right" LabelWidth="80" IndicatorText="" Padding="5" Width="250" /> 
                                <ext:TextField ID="txt_71" runat="server" FieldLabel="用量" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container22" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_67" runat="server" FieldLabel="EPO用量及名称" LabelAlign="Right" LabelWidth="100" IndicatorText="" Padding="5" Width="250" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container23" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_68" runat="server" FieldLabel="磷结合剂" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" />
                                <ext:TextField ID="txt_72" runat="server" FieldLabel="用量" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container24" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_69" runat="server" FieldLabel="VitD制剂" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" />
                                <ext:TextField ID="txt_73" runat="server" FieldLabel="用量" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container25" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextArea ID="are_74" runat="server" FieldLabel="发生事件及处理" LabelAlign="Right" LabelWidth="110" Padding="5" Width="500" Height="100" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container26" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="txt_70" runat="server" FieldLabel="透析总评价" LabelAlign="Right" LabelWidth="110" Padding="5" Width="250" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container27" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextArea ID="are_75" runat="server" FieldLabel="下月透析注意事项" LabelAlign="Right" LabelWidth="110" Padding="5" Width="500" Height="100" />
                            </Items>
                        </ext:Container>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btn_back" runat="server" Icon="ArrowLeft" Text="返回" Width="100" UI="Warning">
                            <DirectEvents>
                                <Click OnEvent="btn_back_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                            <DirectEvents>
                                <Click OnEvent="Btn_Submit_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnPrint" runat="server" Icon="PrinterColor" Text="打印" Width="100" UI="Success">                                                                        
                            <DirectEvents>
                                <Click OnEvent="OnbtnPrint_Click">
                                    <EventMask ShowMask="true" />
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
                <ext:Window ID="PrintWindow" runat="server" Title="" Width="900" Height="650" Y="5" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader6" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
