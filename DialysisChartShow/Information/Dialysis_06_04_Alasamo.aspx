<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_04_Alasamo.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_04_Alasamo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>实验室检查</title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="化验" AutoScroll="true" ButtonAlign="Center" UI="Info">
                    <Items>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_1" runat="server" FieldLabel="Hb" LabelAlign="Right" LabelWidth="80" IndicatorText="g/dl" Padding="5" Width="200" />
                            <ext:TextField ID="txt_2" runat="server" FieldLabel="HCT" LabelAlign="Right" LabelWidth="80" IndicatorText="%" Padding="5" Width="200" />
                            <ext:TextField ID="txt_3" runat="server" FieldLabel="RBC" LabelAlign="Right" LabelWidth="80" IndicatorText="10^9/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_4" runat="server" FieldLabel="WBC" LabelAlign="Right" LabelWidth="80" IndicatorText="10^9/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_5" runat="server" FieldLabel="PLT" LabelAlign="Right" LabelWidth="80" IndicatorText="10^9/L" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                        <Items>
                        <ext:TextField ID="txt_6" runat="server" FieldLabel="尿" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                        <ext:TextField ID="txt_7" runat="server" FieldLabel="Glu" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                        <ext:TextField ID="txt_8" runat="server" FieldLabel="BIL" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                        <ext:TextField ID="txt_9" runat="server" FieldLabel="尿胆原" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_10" runat="server" FieldLabel="Scr" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_11" runat="server" FieldLabel="BUN" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_12" runat="server" FieldLabel="Cr" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_13" runat="server" FieldLabel="Ua" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_14" runat="server" FieldLabel="Ccr" LabelAlign="Right" LabelWidth="80" IndicatorText="ml/min" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_15" runat="server" FieldLabel="ALT" LabelAlign="Right" LabelWidth="80" IndicatorText="IU/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_16" runat="server" FieldLabel="AST" LabelAlign="Right" LabelWidth="80" IndicatorText="IU/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_17" runat="server" FieldLabel="GGT" LabelAlign="Right" LabelWidth="80" IndicatorText="IU/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_18" runat="server" FieldLabel="ALP" LabelAlign="Right" LabelWidth="80" IndicatorText="IU/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_19" runat="server" FieldLabel="TBIL" LabelAlign="Right" LabelWidth="80" IndicatorText="μmol/L" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_20" runat="server" FieldLabel="TP" LabelAlign="Right" LabelWidth="80" IndicatorText="g/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_21" runat="server" FieldLabel="ALB" LabelAlign="Right" LabelWidth="80" IndicatorText="g/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_22" runat="server" FieldLabel="Glu" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_23" runat="server" FieldLabel="TG" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_24" runat="server" FieldLabel="TCHO" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_25" runat="server" FieldLabel="HDL" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                            <ext:TextField ID="txt_26" runat="server" FieldLabel="LDL" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container7" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_27" runat="server" FieldLabel="电解质: K" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_28" runat="server" FieldLabel="Na" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_29" runat="server" FieldLabel="CL" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container8" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_30" runat="server" FieldLabel="Ca" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_31" runat="server" FieldLabel="P" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_32" runat="server" FieldLabel="Mg" LabelAlign="Right" LabelWidth="80" IndicatorText="mmol/L" Padding="5" Width="200" />
                            <ext:TextField ID="txt_33" runat="server" FieldLabel="ipth" LabelAlign="Right" LabelWidth="80" IndicatorText="pg/ml" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container9" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_34" runat="server" FieldLabel="铁三项: SF" LabelAlign="Right" LabelWidth="80" IndicatorText="ng/ml" Padding="5" Width="200" />
                            <ext:TextField ID="txt_35" runat="server" FieldLabel="SI" LabelAlign="Right" LabelWidth="80" IndicatorText="ug/dl" Padding="5" Width="200" />
                            <ext:TextField ID="txt_36" runat="server" FieldLabel="TIBC" LabelAlign="Right" LabelWidth="80" IndicatorText="ug/dl" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container10" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_37" runat="server" FieldLabel="抗感染筛查: HBs-Ag" LabelAlign="Right" LabelWidth="140" Padding="5" Width="300" />
                            <ext:TextField ID="txt_38" runat="server" FieldLabel="Anti-HCV" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                            <ext:TextField ID="txt_39" runat="server" FieldLabel="快速血浆反应素" LabelAlign="Right" LabelWidth="110" Padding="5" Width="200" />
                            <ext:TextField ID="txt_40" runat="server" FieldLabel="抗HIV" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container11" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_41" runat="server" FieldLabel="肝炎指标: HBs-Ag" LabelAlign="Right" LabelWidth="140" Padding="5" Width="250" />
                            <ext:TextField ID="txt_42" runat="server" FieldLabel="HBs-Ab" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                            <ext:TextField ID="txt_43" runat="server" FieldLabel="HBe-Ag" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                            <ext:TextField ID="txt_44" runat="server" FieldLabel="HBe-Ab" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                            <ext:TextField ID="txt_45" runat="server" FieldLabel="HBc-Ab" LabelAlign="Right" LabelWidth="80" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="10">
                        <Items>
                            <ext:RadioGroup ID="RadioGroup46" runat="server" FieldLabel="HCV-Ab" LabelAlign="Right" ColumnsNumber="2" Width="200">
                                <Items>
                                    <ext:Radio ID="Radio1" runat="server" BoxLabel="(+)" InputValue="1" />
                                    <ext:Radio ID="Radio2" runat="server" BoxLabel="(-)" InputValue="2" />
                                </Items>
                            </ext:RadioGroup>
                            <ext:RadioGroup ID="RadioGroup47" runat="server" FieldLabel="HCV-RNA" LabelAlign="Right" ColumnsNumber="2" Width="200">
                                <Items>
                                    <ext:Radio ID="Radio3" runat="server" BoxLabel="(+)" InputValue="1" />
                                    <ext:Radio ID="Radio4" runat="server" BoxLabel="(-)" InputValue="2" />
                                </Items>
                            </ext:RadioGroup>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container13" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_48" runat="server" FieldLabel="肝脏B超: LK" LabelAlign="Right" LabelWidth="140" IndicatorText="cm" Padding="5" Width="250" />
                            <ext:TextField ID="txt_49" runat="server" FieldLabel="实质厚" LabelAlign="Right" LabelWidth="80" IndicatorText="cm" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container14" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_50" runat="server" FieldLabel="RK" LabelAlign="Right" LabelWidth="140" IndicatorText="cm" Padding="5" Width="250" />
                            <ext:TextField ID="txt_51" runat="server" FieldLabel="实质厚" LabelAlign="Right" LabelWidth="80" IndicatorText="cm" Padding="5" Width="200" />
                        </Items>
                    </ext:Container>
                    <ext:TextField ID="txt_60" runat="server" FieldLabel="胸片" LabelAlign="Right" LabelWidth="80" Padding="5" Width="1000" />
                    <ext:TextField ID="txt_61" runat="server" FieldLabel="ECG" LabelAlign="Right" LabelWidth="80" Padding="5" Width="1000" />
                    <ext:TextField ID="txt_62" runat="server" FieldLabel="UCG" LabelAlign="Right" LabelWidth="80" Padding="5" Width="1000" />
                    <ext:TextField ID="txt_63" runat="server" FieldLabel="免疫指标" LabelAlign="Right" LabelWidth="80" Padding="5" Width="1000" Text="g+C3:无   、 ASO、CRP、RF:无" />
                    <ext:TextField ID="txt_64" runat="server" FieldLabel="其他" LabelAlign="Right" LabelWidth="80" Padding="5" Width="1000" />
                    </Items>
                    <Buttons>
                        <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                            <DirectEvents>
                                <Click OnEvent="Btn_Submit_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
