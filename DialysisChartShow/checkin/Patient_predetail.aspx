<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient_predetail.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Patient_predetail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8,Auto-Rotate=Disable" />
    <title>处方模板</title>
    <style type="text/css">        
        .x-border-box .x-form-text
        {
            height: 32px !important;
            font-size: 24px; 
        }
        
        .x-form-item-label-right
        {
            font-size: 24px; 
        }
        
        .x-field-indicator
        {
            font-size: 24px; 
        }
        
        .x-window-header-text-default
        {
            font-size: 24px; 
            line-height: 24px;
        }
        
        .x-box-item
        {
            height: 36px;
        }
        
        .x-btn .x-btn-center .x-btn-inner
        {
            font-size: 18px;
            color: Blue; 
        }
        
        .x-form-item
        {
            font-size: 24px;
            height: 24px;
        }

        .Xx-tool img
        {
            height: 35px;
            width: 30px;
        }
        .Xx-fieldset-header .x-fieldset-header-text
        {
            font-size: 18px; 
        }
        .Xx-form-display-field
        {
            font-size: 24px;
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
        
        .Text-blue18
        {
            color: Blue;
            font-size: 18px;
            height: 18px
        } 
        
        .Text-green18
        {
            color: Green;
            font-size: 18px;
            height: 18px
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
            height: 24px;
        }

        .Radio-blue
        {
            color: Blue;
            font-size: 24px;
            height: 24px;
        }

        .Radio-black
        {
            color: Black;
            font-size: 24px;
            height: 24px;
        }
        
        .x-border-box .x-form-trigger
        {
            height: 30px !important;
            width: 17px !important;
            background-image: url("../Styles/trigger.png");
            cursor: pointer;
        }
        
        .x-form-checkbox, .x-form-radio
        {
            width: 24px;
            height: 24px;
            background-image: url("../Styles/che_btn.png");
        }
        
        #ImageButton1
        {
            height: 48px !important;
            font-size:24px;
        }

        .x-panel-header-text-default
        {
            font-size: 24px;  
            line-height: 24px;
        }
        
        .Xx-panel-header-text {
            font-size: 24px;
            line-height: 24px;
        }
        
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 16px;
            line-height: 16px; 
        }
        
        .x-column-header-inner .x-column-header-text
        {
            font-size: 16px; 
            line-height: 16px; 
        }
        
        .x-boundlist-item 
        { 
            font-size: 24px; 
            color: blue
        }
        
        .my-Field 
        {
            font-size: 24px;
            height: 32px;
            color: Black;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>    
        <ext:Hidden ID="Tex_Patient_ID" runat="server" />
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="patient_name" runat="server" />
        <ext:Hidden ID="machine_type" runat="server" />
        <ext:Hidden ID="machine_model" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="hpack" runat="server" />
        <ext:Hidden ID="hpack3" runat="server" />
        <ext:Hidden ID="patient_weight" runat="server" />
        <ext:Hidden ID="page" runat="server" />
        <ext:Hidden ID="DialysisTimes" runat="server" />
        <ext:Hidden ID="pif_hpack" runat="server" />
        
        <ext:Hidden ID="txt_orddate" runat="server" />
        <ext:Hidden ID="txt_ordtime" runat="server" />
        <ext:Hidden ID="txt_orddoc" runat="server" />
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="TextBaseTimes" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" DirectMethodNamespace="Patient_detail" />
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="处方模板(河南人民)" BodyStyle="background-color:#EBF5FF !important;" >            
            <Items>
                <ext:Container ID="Container00" runat="server" Layout="HBoxLayout">
                    <Items> 
                        <ext:Button ID="BtnBackVisit" runat="server" Text="返回血液净化报到" ColumnWidth=".5" Height="50" PaddingSpec="5 10 0 2" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="BtnBackVisit_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Label ID="space1" runat="server" ColumnWidth=".5" PaddingSpec="5 10 0 2" Flex="1"></ext:Label>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container11" runat="server" Frame="true" Layout="ColumnLayout" Split="true" Region="Center" >
                    <Items>
                        <ext:Image ID="Image1" runat="server" ColumnWidth=".3" Height="286" Weight="100" />
                        <ext:Container ID="Container12" runat="server" ColumnWidth=".7" Layout="ColumnLayout">
                            <Items>
                                        <ext:SelectBox ID="cb_info_date" runat="server" LabelWidth="160" FieldLabel="透析日期" LabelAlign="Right" Cls="Text-blue" ColumnWidth="1" Padding="2">
                                            <DirectEvents>
                                                <Change OnEvent="ChangeDate"></Change>
                                            </DirectEvents>
                                        </ext:SelectBox>
                                        <ext:TextField ID="Tex_Patient_Name" runat="server"   LabelWidth="160" FieldLabel="姓名" LabelAlign="Right" Cls="read_field" ColumnWidth=".5" Padding="2" LabelCls="my-Field" ReadOnly="true" />
                                        <ext:TextField ID="Tex_Patient_Gender" runat="server" LabelWidth="60" FieldLabel="性别" LabelAlign="Right" Cls="read_field" ColumnWidth=".25" Padding="2" LabelCls="my-Field" ReadOnly="true" />
                                        <ext:TextField ID="Tex_Patient_Age" runat="server"    LabelWidth="60" FieldLabel="年龄" LabelAlign="Right" Cls="read_field" ColumnWidth=".25" Padding="2" LabelCls="my-Field" ReadOnly="true" /> 
                                        <ext:TextField ID="area" runat="server" FieldLabel="床区" LabelWidth="160" ColumnWidth=".4" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" ReadOnly="true" Padding="5" />
                                        <ext:TextField ID="bedno" runat="server" FieldLabel="床号" LabelWidth="80" ColumnWidth=".3" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" ReadOnly="true" Padding="5" />                                
                                        <ext:TextField ID="time" runat="server" FieldLabel="时段" LabelWidth="80" ColumnWidth=".3" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" ReadOnly="true" Padding="5" />
                                        <ext:TextField ID="TextTimes" runat="server" FieldLabel="透析次数" LabelWidth="160" ColumnWidth=".4" LabelAlign="Right" LabelCls="blue-Field" Padding="5" />
                                        <ext:Panel ID="Panel3" runat="server" Layout="HBoxLayout" Margin="10" />
                                        <ext:SelectBox ID="cbo_h_type" runat="server" FieldLabel="血管通路类型" LabelWidth="160" LabelAlign="Right" ColumnWidth="1" LabelCls="my-Field" Cls="read_field" DisplayField="state" Padding="5" />
                                        <ext:SelectBox ID="cbo_machine_model" runat="server" FieldLabel="透析器型号" LabelWidth="160" LabelAlign="Right" ColumnWidth="1" LabelCls="my-Field" Cls="read_field" DisplayField="state" Padding="5" />
                                        <ext:SelectBox ID="cbo_Machinetype" runat="server" FieldLabel="透析方式" LabelWidth="160" LabelAlign="Right" ColumnWidth="1" LabelCls="my-Field" Cls="read_field" DisplayField="state" Padding="5" />
                            </Items>
                        </ext:Container>
                    </Items>                            
                </ext:Container>
                <ext:Container ID="Container4" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="txt_weight_after_expect" runat="server" FieldLabel="干体重" ColumnWidth=".5" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" PaddingSpec="5 10 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                        <ext:SelectBox ID="SelectBox10" runat="server" FieldLabel="抗凝药物" ColumnWidth=".5" LabelWidth="160" Cls="Text-blue" LabelAlign="Right" PaddingSpec="5 10 0 2" Flex="1" />
                        <ext:TextField ID="TextField3" runat="server" FieldLabel="目标定容量" ColumnWidth=".5" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" PaddingSpec="5 10 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container5" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField8" runat="server" FieldLabel="首次剂量" ColumnWidth=".5" LabelWidth="160" LabelAlign="Right" LabelCls="my-Field" PaddingSpec="5 10 0 2" IndicatorText="" Cls="Text-blue" Flex="1" />
                        <ext:TextField ID="TextAdd" runat="server" FieldLabel="追加量" ColumnWidth=".5" IndicatorText="" LabelAlign="Right" PaddingSpec="5 10 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container7" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:Label ID="sp3" runat="server" ColumnWidth=".5" Flex="1"></ext:Label>
                        <ext:TextField ID="TextAmount" runat="server" FieldLabel="总量" ColumnWidth=".5" IndicatorText="" LabelAlign="Right" PaddingSpec="5 10 0 2" Cls="Text-blue" LabelWidth="165" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container99" runat="server" Layout="HBoxLayout">
                    <Items> 
                        <ext:Button ID="BtnDownBack" runat="server" Text="返回血液净化报到" ColumnWidth=".5" Height="50" PaddingSpec="5 10 0 2" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="BtnBackVisit_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="BtnSave" runat="server" Text="存盘" ColumnWidth=".5" Height="50" PaddingSpec="5 10 0 2" Flex="1">
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