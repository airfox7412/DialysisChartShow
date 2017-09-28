<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_PreSetBase.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Dialysis_PreSetBase" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>处方底板</title>
    <style type="text/css">
    .x-form-item-label-default, .x-field-indicator 
    {
        font-size:15px;
        font-weight:normal;
    }
    .x-form-text-default
    {
        font-size:15px;
        font-weight:normal;
    }
        
    .Text-blue .x-form-field
    {
        color: blue;
        font-size:16px;
        font-weight:normal;
    }
        
    .Text-red .x-form-field
    {
        color: red;
        font-size:16px;
        font-weight:normal;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="Patient_ID" runat="server" />
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="TextBaseTimes" runat="server" />
        <ext:Hidden ID="DialysisTimes" runat="server" />
        <ext:Hidden ID="hpack" runat="server" />
        
        <ext:Hidden ID="txt_orddate" runat="server" />
        <ext:Hidden ID="txt_ordtime" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" DirectMethodNamespace="Dialysis_PreSetEdit" />
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="处方底板" Header="false" >            
            <Items>
                <ext:Container ID="Container11" runat="server" Frame="true" Layout="ColumnLayout" Split="true" Region="Center" >
                    <Items>
                        <ext:Image ID="Image1" runat="server" ColumnWidth=".2" Height="150" Weight="50" />
                        <ext:Container ID="Container12" runat="server" ColumnWidth=".8" Layout="FitLayout">
                            <Items>
                                <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="text_info_date" runat="server" FieldLabel="透析日期" LabelWidth="110" LabelAlign="Right" ColumnWidth=".3" Padding="1" Cls="Text-blue" ReadOnly="true" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:TextField ID="TextTimes" runat="server" FieldLabel="透析次数" LabelWidth="100" LabelAlign="Right" ColumnWidth=".3" Padding="1" LabelCls="my-Field" ReadOnly="true" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:Label runat="server" Text=" " ColumnWidth=".3" MarginSpec="5 50 5 10" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="Tex_Patient_Name" runat="server" FieldLabel="姓名" LabelWidth="110" LabelAlign="Right" Cls="read_field" ColumnWidth=".3" Padding="1" LabelCls="my-Field" ReadOnly="true" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:TextField ID="Tex_Patient_Gender" runat="server" FieldLabel="性别" LabelWidth="100" LabelAlign="Right" Cls="read_field" ColumnWidth=".3" Padding="1" LabelCls="my-Field" ReadOnly="true" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:TextField ID="Tex_Patient_Age" runat="server" FieldLabel="年龄" LabelWidth="100" LabelAlign="Right" Cls="read_field" ColumnWidth=".3" LabelCls="my-Field" ReadOnly="true" MarginSpec="5 50 5 10" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:SelectBox ID="cbo_Machinetype" runat="server" FieldLabel="透析方式" LabelWidth="110" LabelAlign="Right" ColumnWidth=".3" LabelCls="my-Field" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1">
                                            <DirectEvents>
                                                <Change OnEvent="OnChangeType" />
                                            </DirectEvents>
                                        </ext:SelectBox>
                                        <ext:SelectBox ID="cbo_machine_model" runat="server" FieldLabel="透析器型号" LabelWidth="100" LabelAlign="Right" ColumnWidth=".3" LabelCls="my-Field" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:SelectBox ID="cbo_machine_model2" runat="server" FieldLabel="透析器型号二" LabelWidth="100" LabelAlign="Right" ColumnWidth=".3" LabelCls="my-Field" Cls="Text-blue" MarginSpec="5 50 5 10" Flex="1" ReadOnly="true" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container5" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:SelectBox ID="cbo_h_type" runat="server" FieldLabel="血管通路类型" LabelWidth="110" LabelAlign="Right" ColumnWidth=".3" LabelCls="my-Field" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:SelectBox ID="cbo_Tube" runat="server" FieldLabel="管路型号" LabelWidth="100" LabelAlign="Right" ColumnWidth=".3" LabelCls="my-Field" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:SelectBox ID="cbo_Tube2" runat="server" FieldLabel="管路型号二" LabelWidth="100" LabelAlign="Right" ColumnWidth=".3" LabelCls="my-Field" Cls="Text-blue" MarginSpec="5 50 5 10" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="txt_weight_after_expect" runat="server" FieldLabel="干体重" LabelWidth="110" ColumnWidth=".3" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:SelectBox ID="SelectBox10" runat="server" FieldLabel="抗凝药物" LabelWidth="100" ColumnWidth=".3" LabelAlign="Right" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:TextField ID="TextField3" runat="server" FieldLabel="目标定容量" LabelWidth="100" ColumnWidth=".3" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" Cls="Text-blue" MarginSpec="5 50 5 10" Flex="1" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container7" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="TextField8" runat="server" FieldLabel="首次剂量" LabelWidth="110" ColumnWidth=".3" LabelAlign="Right" LabelCls="my-Field" Padding="1" IndicatorText="" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:TextField ID="TextAdd" runat="server" FieldLabel="追加量" LabelWidth="100" ColumnWidth=".3" IndicatorText="" LabelAlign="Right" Padding="1" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:TextField ID="TextAmount" runat="server" FieldLabel="总量" LabelWidth="100" ColumnWidth=".3" IndicatorText="" LabelAlign="Right" PaddingSpec="2 20 2 2" Cls="Text-blue" MarginSpec="5 50 5 10" Flex="1" />
                                    </Items> 
                                </ext:Container> 
                                <ext:Container ID="Container8" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="TextField6" runat="server" FieldLabel="透析液: 钾" LabelWidth="110" IndicatorText="mmol/L" ColumnWidth=".3" LabelAlign="Right" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:TextField ID="TextField7" runat="server" FieldLabel="钙" LabelWidth="100" IndicatorText="mmol/L" ColumnWidth=".3" LabelAlign="Right" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:Label ID="Label1" runat="server" Text=" " ColumnWidth=".3" MarginSpec="5 50 5 10" Flex="1" />
                                    </Items> 
                                </ext:Container>  
                                <ext:Container ID="Container9" runat="server" Layout="HBoxLayout">
                                    <Items>
                                        <ext:TextField ID="TextField9" runat="server" FieldLabel="碳酸氢根" LabelWidth="110" IndicatorText="mmol/L" ColumnWidth=".3" LabelAlign="Right" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:TextField ID="TextField10" runat="server" FieldLabel="钠" LabelWidth="100" IndicatorText="mmol/L" ColumnWidth=".3" LabelAlign="Right" Cls="Text-blue" MarginSpec="5 10 5 10" Flex="1" />
                                        <ext:Label ID="Label2" runat="server" Text=" " ColumnWidth=".3" MarginSpec="5 50 5 10" Flex="1" />
                                    </Items> 
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>                            
                </ext:Container>
                <ext:Container ID="Container99" runat="server" Layout="CenterLayout">
                    <Items>
                        <ext:Button ID="BtnSave" runat="server" Text="存盘" Width="200" Height="50" Cls="ImageBlue" PaddingSpec="10 10 10 10">
                            <DirectEvents>
                                <Click OnEvent="BtnSave_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items> 
                </ext:Container>
            </Items>
        </ext:FormPanel>
    </form>
</body>
</html>