<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_012_Alasamo.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_012_Alasamo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>阿拉善盟病史</title>
    <style type="text/css">
    .ind-red
    {
        font-size:larger;
        color:Red;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="病历" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:DateField ID="txt_1d" runat="server" FieldLabel="入院日期" LabelAlign="Right" Format="yyyy-MM-dd" Padding="5" />                                                       
                                <ext:TextField ID="txt_1t" runat="server" FieldLabel="入院时间" LabelWidth="60" Padding="5" IndicatorText="*" IndicatorCls="ind-red">
                                    <Plugins>
                                        <ext:InputMask ID="InputMask1" runat="server" Mask="dt:mn">
                                            <MaskSymbols>
                                                <ext:MaskSymbol Name="d" Regex="[0-2]" Placeholder="h" />
                                                <ext:MaskSymbol Name="t" Regex="[0-9]" Placeholder="h" />
                                                <ext:MaskSymbol Name="m" Regex="[0-5]" Placeholder="m" />
                                                <ext:MaskSymbol Name="n" Regex="[0-9]" Placeholder="m" />
                                            </MaskSymbols>
                                        </ext:InputMask>
                                    </Plugins>
                                </ext:TextField>
                            </Items>
                        </ext:Container>
                        <ext:TextField ID="txt_2" runat="server" FieldLabel="肾脏病史" LabelAlign="Right" Padding="5" />
                        <ext:CheckboxGroup ID="chkgroup1" runat="server" ColumnsNumber="6" FieldLabel="原发疾病" LabelAlign="Right">
                            <Items>
                                <ext:Checkbox ID="txt_3_1" runat="server" BoxLabel="慢性肾炎" />
                                <ext:Checkbox ID="txt_3_2" runat="server" BoxLabel="慢性肾盂肾炎" />
                                <ext:Checkbox ID="txt_3_3" runat="server" BoxLabel="慢性间质性肾炎" />
                                <ext:Checkbox ID="txt_3_4" runat="server" BoxLabel="梗阻性肾病" />
                                <ext:Checkbox ID="txt_3_5" runat="server" BoxLabel="RPGNI-I-III-IV-V" />
                                <ext:Checkbox ID="txt_3_6" runat="server" BoxLabel="多囊肾" />
                                <ext:Checkbox ID="txt_3_7" runat="server" BoxLabel="原发性小血管炎" />
                                <ext:Checkbox ID="txt_3_8" runat="server" BoxLabel="SLE" />
                                <ext:Checkbox ID="txt_3_9" runat="server" BoxLabel="SS" />
                                <ext:Checkbox ID="txt_3_10" runat="server" BoxLabel="高血压肾损害" />
                                <ext:Checkbox ID="txt_3_11" runat="server" BoxLabel="糖尿病肾病" />
                                <ext:Checkbox ID="txt_3_12" runat="server" BoxLabel="不详" />
                                <ext:Checkbox ID="txt_3_13" runat="server" BoxLabel="其他" />
                                <ext:TextField ID="txt_3c" runat="server" FieldLabel="" />
                            </Items>
                        </ext:CheckboxGroup>
                        <ext:TextField ID="txt_4" runat="server" FieldLabel="首发症状" LabelAlign="Right" Width="1000" Padding="5" />
                        <ext:TextField ID="txt_5" runat="server" FieldLabel="首发时间" LabelAlign="Right" Padding="5" />
                        <ext:TextField ID="txt_6" runat="server" FieldLabel="发现肾衰时间" LabelAlign="Right" Padding="5" />
                        <ext:TextArea ID="txt_20" runat="server" FieldLabel="简要病情" LabelAlign="Right" Width="1000" Height="100" Padding="5" />
                        <ext:TextField ID="txt_7" runat="server" FieldLabel="目前" LabelAlign="Right" Width="1000" Padding="5" />
                        <ext:TextField ID="txt_8" runat="server" FieldLabel="出血倾向" LabelAlign="Right" Width="1000" Padding="5" />
                        <ext:TextField ID="txt_9" runat="server" FieldLabel="心血管病史" LabelAlign="Right" Width="1000" Padding="5" />
                        <%--<ext:TextField ID="txt_10" runat="server" FieldLabel="目前服药名" LabelAlign="Right" Width="1000" Padding="5" />--%>
                        <ext:TextField ID="txt_11" runat="server" FieldLabel="高血压史" LabelAlign="Right" Width="1000" Padding="5" />
                        <ext:TextField ID="txt_12" runat="server" FieldLabel="脑血管疾病史" LabelAlign="Right" Width="1000" Padding="5" />
                        <ext:TextField ID="txt_21" runat="server" FieldLabel="糖尿病史" LabelAlign="Right" Width="1000" Padding="5" />
                        <%--<ext:TextArea ID="txt_21" runat="server" FieldLabel="既往史" LabelAlign="Right" Width="1000" Height="100" Padding="5" />--%>
                        <ext:TextField ID="txt_13" runat="server" FieldLabel="肝炎病史" LabelAlign="Right" Width="1000" Padding="5" />
                        <ext:TextField ID="txt_14" runat="server" FieldLabel="其他疾病" LabelAlign="Right" Width="1000" Padding="5" />
                        <ext:TextField ID="txt_15" runat="server" FieldLabel="过敏史" LabelAlign="Right" Width="1000" Padding="5" />
                        <ext:TextField ID="txt_22" runat="server" FieldLabel="婚姻状况" LabelAlign="Right" Width="1000" Padding="5" />
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
