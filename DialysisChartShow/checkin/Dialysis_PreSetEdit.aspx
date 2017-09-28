<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_PreSetEdit.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Dialysis_PreSetEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>处方模板</title>
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
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <ext:Hidden ID="Patient_ID" runat="server" />
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="TextBaseTimes" runat="server" />
        <ext:Hidden ID="DialysisTimes" runat="server" />
        <ext:Hidden ID="hpack" runat="server" />
        <ext:Hidden ID="floor" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" DirectMethodNamespace="Dialysis_PreSetEdit" />
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="处方模板(河南人民)" BodyStyle="background-color:#EBF5FF !important;" >            
            <Items>
                <ext:Container ID="Container11" runat="server" Frame="true" Layout="ColumnLayout" Split="true" Region="Center" >
                    <Items>
                        <ext:Image ID="Image1" runat="server" ColumnWidth=".3" Height="245" Weight="100" />
                        <ext:Container ID="Container12" runat="server" ColumnWidth=".7" Layout="ColumnLayout">
                            <Items>
                                        <ext:TextField ID="text_info_date" runat="server" FieldLabel="透析日期" LabelWidth="160" LabelAlign="Right" Cls="Text-blue" ColumnWidth=".6" Padding="1" />
                                        <ext:TextField ID="TextTimes" runat="server" FieldLabel="透析次数" LabelWidth="120" LabelAlign="Right" LabelCls="blue-Field" ColumnWidth=".4" Padding="1" />
                                        <ext:TextField ID="Tex_Patient_Name" runat="server" FieldLabel="姓名" LabelWidth="160" LabelAlign="Right" Cls="read_field" ColumnWidth=".5" Padding="1" LabelCls="my-Field" ReadOnly="true" />
                                        <ext:TextField ID="Tex_Patient_Gender" runat="server" FieldLabel="性别" LabelWidth="60" LabelAlign="Right" Cls="read_field" ColumnWidth=".25" Padding="1" LabelCls="my-Field" ReadOnly="true" />
                                        <ext:TextField ID="Tex_Patient_Age" runat="server" FieldLabel="年龄" LabelWidth="60" LabelAlign="Right" Cls="read_field" ColumnWidth=".25" Padding="1" LabelCls="my-Field" ReadOnly="true" /> 
                                        <!--<ext:TextField ID="area" runat="server" FieldLabel="床区" LabelWidth="160" ColumnWidth=".4" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" ReadOnly="true" Padding="1" />
                                        <ext:TextField ID="bedno" runat="server" FieldLabel="床号" LabelWidth="80" ColumnWidth=".3" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" ReadOnly="true" Padding="1" />                                
                                        <ext:TextField ID="time" runat="server" FieldLabel="时段" LabelWidth="80" ColumnWidth=".3" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" ReadOnly="true" Padding="1" />-->
                                        <ext:Panel ID="Panel3" runat="server" Layout="HBoxLayout" Margin="10" />
                                        <ext:SelectBox ID="cbo_h_type" runat="server" FieldLabel="血管通路类型" LabelWidth="160" LabelAlign="Right" ColumnWidth="1" LabelCls="my-Field" Cls="read_field" DisplayField="state" Padding="1" />
                                        <ext:SelectBox ID="cbo_machine_model" runat="server" FieldLabel="透析器型号" LabelWidth="160" LabelAlign="Right" ColumnWidth="1" LabelCls="my-Field" Cls="read_field" DisplayField="state" Padding="1" />
                                        <ext:SelectBox ID="cbo_Machinetype" runat="server" FieldLabel="透析方式" LabelWidth="160" LabelAlign="Right" ColumnWidth="1" LabelCls="my-Field" Cls="read_field" DisplayField="state" Padding="1" />
                                        <ext:TextField ID="txt_weight_after_expect" runat="server" FieldLabel="干体重" ColumnWidth=".5" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" Cls="Text-blue" LabelWidth="160" Padding="1" />
                                        <ext:SelectBox ID="SelectBox10" runat="server" FieldLabel="抗凝药物" ColumnWidth=".5" LabelWidth="110" Cls="Text-blue" LabelAlign="Right" Padding="1" />
                                        <ext:TextField ID="TextField3" runat="server" FieldLabel="目标定容量" ColumnWidth=".5" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" Cls="Text-blue" LabelWidth="160" Padding="1" />
                            </Items>
                        </ext:Container>
                    </Items>                            
                </ext:Container>
                <ext:Container ID="Container5" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField8" runat="server" FieldLabel="首次剂量" ColumnWidth=".4" LabelWidth="130" LabelAlign="Right" LabelCls="my-Field" Padding="1" IndicatorText="" Cls="Text-blue" Flex="1" />
                        <ext:TextField ID="TextAdd" runat="server" FieldLabel="追加量" ColumnWidth=".3" LabelWidth="100" IndicatorText="" LabelAlign="Right" Padding="1" Cls="Text-blue" Flex="1" />
                        <ext:TextField ID="TextAmount" runat="server" FieldLabel="总量" ColumnWidth=".3" LabelWidth="100" IndicatorText="" LabelAlign="Right" PaddingSpec="2 20 2 2" Cls="Text-blue" Flex="1" />
                    </Items> 
                </ext:Container> 
                <ext:Container ID="Container1" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField6" runat="server" FieldLabel="透析液: 钾" IndicatorText="mmol/L" ColumnWidth=".5" LabelAlign="Right" Padding="1" Cls="Text-blue" LabelWidth="160" Flex="1" />
                        <ext:TextField ID="TextField7" runat="server" FieldLabel="钙" IndicatorText="mmol/L" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="2 20 2 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>  
                <ext:Container ID="Container8" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField9" runat="server" FieldLabel="碳酸氢根" IndicatorText="mmol/L" ColumnWidth=".5" LabelAlign="Right" Padding="1" Cls="Text-blue" LabelWidth="160" Flex="1" />
                        <ext:TextField ID="TextField10" runat="server" FieldLabel="钠" IndicatorText="mmol/L" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="2 20 2 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container99" runat="server" Layout="HBoxLayout">
                    <Items>
                        <ext:Button ID="BtnSave" runat="server" Text="存盘" ColumnWidth=".5" Height="50" PaddingSpec="10 10 10 10" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="BtnSave_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items> 
                </ext:Container>
            </Items>
        </ext:FormPanel>
    </div>
    </form>
</body>
</html>