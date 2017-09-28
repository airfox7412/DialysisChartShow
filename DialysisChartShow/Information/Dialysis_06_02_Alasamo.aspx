<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_02_Alasamo.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_02_Alasamo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>体检记录</title>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />

    <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
        <Items>
            <ext:FormPanel ID="FormPanel1" runat="server" Title="体检记录" AutoScroll="true" ButtonAlign="Center">
                <Items>
                    <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_1" runat="server" FieldLabel="BP" LabelAlign="Right" LabelWidth="80" IndicatorText="mmHg" Padding="5" />
                            <ext:TextField ID="txt_2" runat="server" FieldLabel="HR" LabelAlign="Right" LabelWidth="40" IndicatorText="次/分" Padding="5" />
                            <ext:TextField ID="txt_3" runat="server" FieldLabel="R" LabelAlign="Right" LabelWidth="50" IndicatorText="次/分" Padding="5" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_4" runat="server" FieldLabel="T" LabelAlign="Right" LabelWidth="80" IndicatorText="℃" Padding="5" />
                            <ext:TextField ID="txt_5" runat="server" FieldLabel="体重" LabelAlign="Right" LabelWidth="65" IndicatorText="kg" Padding="5" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_6" runat="server" FieldLabel="一般状况" LabelAlign="Right" LabelWidth="80" Padding="5" />
                            <ext:TextField ID="txt_7" runat="server" FieldLabel="营养状态" LabelAlign="Right" LabelWidth="80" Padding="5" />
                            <ext:TextField ID="txt_8" runat="server" FieldLabel="贫血面容" LabelAlign="Right" LabelWidth="80" Padding="5" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_9" runat="server" FieldLabel="体位" LabelAlign="Right" LabelWidth="80" Padding="5" />
                            <ext:TextField ID="txt_10" runat="server" FieldLabel="浮肿" LabelAlign="Right" LabelWidth="80" Padding="5" />
                            <ext:TextField ID="txt_11" runat="server" FieldLabel="部位" LabelAlign="Right" LabelWidth="80" Padding="5" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                        <Items>
                            <ext:TextField ID="txt_12" runat="server" FieldLabel="程度" LabelAlign="Right" LabelWidth="80" Padding="5" />
                            <ext:TextField ID="txt_13" runat="server" FieldLabel="部位及描述" LabelAlign="Right" LabelWidth="80" Padding="5" />
                        </Items>
                    </ext:Container>
                    <ext:TextField ID="txt_20" runat="server" FieldLabel="肺部" LabelAlign="Right" LabelWidth="80" Padding="5" Width="1000" />
                    <ext:TextField ID="txt_21" runat="server" FieldLabel="心脏" LabelAlign="Right" LabelWidth="80" Padding="5" Width="1000" />
                    <ext:Label ID="Label3" runat="server" Text="腹部" Padding="5" />
                    <ext:RadioGroup ID="RadioGroup14" runat="server" ColumnsNumber="8" FieldLabel="腹水症" LabelAlign="Right">
                        <Items>
                            <ext:Radio runat="server" BoxLabel="阳性" InputValue="1" />
                            <ext:Radio runat="server" BoxLabel="阴性" InputValue="2" />
                        </Items>
                    </ext:RadioGroup>
                    <ext:RadioGroup ID="RadioGroup15" runat="server" ColumnsNumber="8" FieldLabel="肝颈回流症" LabelAlign="Right">
                        <Items>
                            <ext:Radio runat="server" BoxLabel="无" InputValue="1" />
                            <ext:Radio runat="server" BoxLabel="有" InputValue="2" />
                        </Items>
                    </ext:RadioGroup>
                    <ext:Container ID="Container16" runat="server" Layout="ColumnLayout">
                        <Items>
                            <ext:RadioGroup ID="RadioGroup16_1" runat="server" FieldLabel="肝脏" LabelAlign="Right" ColumnsNumber="2" Width="300">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="不大" InputValue="1" />
                                    <ext:Radio runat="server" BoxLabel="大" InputValue="2" />
                                </Items>
                            </ext:RadioGroup>
                            <ext:RadioGroup ID="RadioGroup16_2" runat="server" ColumnsNumber="2" Width="200">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="无压痛" InputValue="3" />
                                    <ext:Radio runat="server" BoxLabel="压痛" InputValue="4" />
                                </Items>
                            </ext:RadioGroup>
                            <ext:RadioGroup ID="RadioGroup16_3" runat="server" ColumnsNumber="2" Width="200">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="无叩痛" InputValue="5" />
                                    <ext:Radio runat="server" BoxLabel="叩痛"  InputValue="6"/>
                                </Items>
                            </ext:RadioGroup>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container17" runat="server" Layout="ColumnLayout">
                        <Items>
                            <ext:RadioGroup ID="RadioGroup17_1" runat="server" FieldLabel="脾脏" LabelAlign="Right" ColumnsNumber="2" Width="300">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="不大" InputValue="1" />
                                    <ext:Radio runat="server" BoxLabel="大" InputValue="2" />
                                </Items>
                            </ext:RadioGroup>
                            <ext:RadioGroup ID="RadioGroup17_2" runat="server" ColumnsNumber="2" Width="200">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="无压痛" InputValue="3" />
                                    <ext:Radio runat="server" BoxLabel="压痛" InputValue="4" />
                                </Items>
                            </ext:RadioGroup>
                            <ext:RadioGroup ID="RadioGroup17_3" runat="server" ColumnsNumber="2" Width="200">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="无叩痛" InputValue="5" />
                                    <ext:Radio runat="server" BoxLabel="叩痛" InputValue="6" />
                                </Items>
                            </ext:RadioGroup>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container18" runat="server" Layout="ColumnLayout">
                        <Items>
                            <ext:RadioGroup ID="RadioGroup18_1" runat="server" FieldLabel="肾脏" LabelAlign="Right" ColumnsNumber="2" Width="300">
                                <Items>
                                    <ext:Radio ID="Checkbox1" runat="server" BoxLabel="不大" InputValue="1" />
                                    <ext:Radio ID="Checkbox2" runat="server" BoxLabel="大" InputValue="2" />
                                </Items>
                            </ext:RadioGroup>
                            <ext:RadioGroup ID="RadioGroup18_2" runat="server" ColumnsNumber="2" Width="200">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="无压痛" InputValue="3" />
                                    <ext:Radio runat="server" BoxLabel="压痛" InputValue="4" />
                                </Items>
                            </ext:RadioGroup>
                            <ext:RadioGroup ID="RadioGroup18_3" runat="server" ColumnsNumber="2" Width="200">
                                <Items>
                                    <ext:Radio runat="server" BoxLabel="无叩痛" InputValue="5" />
                                    <ext:Radio runat="server" BoxLabel="叩痛" InputValue="6" />
                                </Items>
                            </ext:RadioGroup>
                        </Items>
                    </ext:Container>
                    <ext:TextField ID="txt_22" runat="server" FieldLabel="其他" LabelAlign="Right" LabelWidth="80" Padding="5" Width="1000" />
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
