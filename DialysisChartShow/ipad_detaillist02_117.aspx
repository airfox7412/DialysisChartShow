<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_detaillist02_117.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_detaillist02_117" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8, Auto-Rotate=Disable" />
    <title><% =Label2.Text%></title>
    <style type="text/css">
        .x-box-item
        {
            height: 36px !important;
        }
        .x-panel-header-text-default
        {
            font-size: xx-large;
            line-height: 36px;
        }
        .x-tool img
        {
            height: 35px;
            width: 30px;
        }
        .blue .x-form-item-body
        {
            height: 50px;
        }
        .height .x-form-item-body
        {
            height: 50px;
        }
        .height2 .x-form-item-body
        {
            height: 50px;
        }
        
        .red .x-form-item-body
        {
            height: 50px;
        }
        .x-form-item-label-left
        {
            font-size: x-large;
        }        
        .x-form-item-label-right
        {
            font-size: x-large;
        }
        .x-field-indicator
        {
            font-size: x-large;
        }
        .x-border-box .x-form-text
        {
            height: 35px !important;
            font-size: x-large;
        }
        .red
        {
            color: Red;
        }
        
        .x-btn .x-btn-center .x-btn-inner
        {
            font-size: x-large;
        }
        .red .x-form-field
        {
            
            height: 35px !important;
        }
        .blue .x-form-field
        {
            width: 230px !important;
            height: 35px !important;
            color: blue;
            background-image: none !important;
        }
        .blue2 .x-form-field
        {
            height: 200px !important;
            color: blue !important;
            background-image: none !important;
        }
        .height .x-form-field
        {
            font-size: xx-large;
            height: 40px !important;
            color: blue;
        }
        .height2 .x-form-field
        {
            height: 100px !important;
            background-image: none !important;
        }
        .x-border-box .x-form-trigger
        {
            height: 35px !important;
            width: 17px !important;
            background-image: url("./Styles/trigger.png");
            cursor: pointer;
        }
        .x-boundlist-item
        {
            font-size: x-large;
        }
        #ImageButton1, #ImageBtn_Home, #ImageBtn_back, #ImageBtn_save
        {
            height: 50px !important;
        }
        
        .x-window-header-text-default
        {
            font-size: x-large;
        }
        .x-form-display-field
        {
            font-size: 20px;
        }
        .label .x-label-value
        {
            width: 120px !important;
            height: 35px !important;
            font-size: xx-large;
            color: #178951;
        }
        .label2 .x-label-value
        {
            font-size: x-large;
        }
        .x-form-item
        {
            font: normal 25px tahoma,arial,verdana,sans-serif;
        }
        .che
        {
            color: Red;
        }
        .che_label
        {
            color:Black !important;
            }
        .x-form-checkbox, .x-form-radio
        {
            width: 25px;
            height: 35px;
            background-image: url("./Styles/che_btn.png");
        }
        .x-fieldset-header .x-fieldset-header-text 
        {
            font-size:large;
        }
        .formlabel
        {
            font-size: xx-large;
            color : Black;
        }
        .read_field .x-form-field
        {
            font-size: xx-large;
            height: 40px !important;
            color: blue;
            background-color: #EEEEEE;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <div>
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="ttt" runat="server" />

        <ext:Hidden ID="patient_name" runat="server" />
        <ext:Hidden ID="machine_type" runat="server" />
        <ext:Hidden ID="patient_weight" runat="server" />
        <ext:Hidden ID="mechine_model" runat="server" />
        <ext:Hidden ID="hpack" runat="server" />
        <ext:Hidden ID="hpack3" runat="server" />
        <ext:Hidden ID="txt_weight_before" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:FormPanel ID="FormPanel1" runat="server" Title="净化过程小结" BodyStyle="background-color:#EBF5FF !important;" AutoScroll="true">
            <Items>
                <ext:Container ID="Container00" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:ImageButton ID="Btn_Home" runat="server" ImageUrl="Styles/home1.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/homeover.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_Home_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_detail01" runat="server" ImageUrl="Styles/detail01.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detail01over.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail01_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_detail" runat="server" ImageUrl="Styles/detail.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detailover.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_detail02" runat="server" ImageUrl="Styles/detail02.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/detail02over.png" Flex="1" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail02_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_back" runat="server" ImageUrl="Styles/back2.png" Height="50" PaddingSpec="0 0 0 0" OverImageUrl="Styles/back2over.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_back_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="20">
                    <Items>
                    <ext:Label ID="Label7" runat="server" ColumnWidth=".1" Cls="label" PaddingSpec="10 2 2 2" />
                    <ext:Label ID="Label1" runat="server" Text="姓名:" ColumnWidth=".1" Cls="label" Margins="10 2 2 2" />
                    <ext:Label ID="Label2" runat="server" ColumnWidth=".2" Cls="label" PaddingSpec="2 2 2 2" />
                    <ext:Label ID="Label3" runat="server" Text="   楼层:" ColumnWidth=".1" Cls="label" PaddingSpec="2 2 2 2" />
                    <ext:Label ID="Label4" runat="server" ColumnWidth=".2" Cls="label" PaddingSpec="2 2 2 2" />
                    <ext:Label ID="Label5" runat="server" Text="   床号:" ColumnWidth=".1" Cls="label" PaddingSpec="2 2 2 2" />
                    <ext:Label ID="Label6" runat="server" ColumnWidth=".2" Cls="label" PaddingSpec="2 2 2 2" />
                        </Items>
                </ext:Container>
                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                            <ext:TextField ID="TextField16" runat="server" FieldLabel="血管通路类型" LabelCls="formlabel" LabelAlign="Right" LabelWidth="220" 
                            ColumnWidth="0.6" Cls="read_field" ReadOnly="true" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                            <ext:TextField ID="TextField2" runat="server" FieldLabel="时间" LabelCls="formlabel" LabelAlign="Right" LabelWidth="220" 
                            ColumnWidth="0.6" Cls="read_field" ReadOnly="true" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container31" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                            <ext:TextField ID="TextField3" runat="server" FieldLabel="历时" LabelCls="formlabel" LabelAlign="Right" LabelWidth="220" 
                            ColumnWidth="0.4" Cls="read_field" ReadOnly="true" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container32" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                            <ext:TextField ID="TextField4" runat="server" FieldLabel="脱水" LabelCls="formlabel" LabelAlign="Right" LabelWidth="220" 
                            ColumnWidth="0.45" Cls="height" IndicatorText="(kg)" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                        <ext:TextField ID="TextField5" runat="server" FieldLabel="高压波动范围" LabelCls="formlabel" LabelAlign="Right" LabelWidth="220" ColumnWidth="0.4" Cls="height"/>
                        <ext:TextField ID="TextField6" runat="server" FieldLabel="~" LabelCls="formlabel" LabelAlign="Right" LabelWidth="10" ColumnWidth="0.17" Cls="height" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                        <ext:TextField ID="TextField7" runat="server" FieldLabel="低压波动范围" LabelCls="formlabel" LabelAlign="Right" LabelWidth="220" ColumnWidth="0.4" Cls="height"/>
                        <ext:TextField ID="TextField8" runat="server" FieldLabel="~" LabelCls="formlabel" LabelAlign="Right" LabelWidth="10" ColumnWidth="0.17" Cls="height" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                        <ext:SelectBox ID="cbo_pressure" runat="server" FieldLabel="血压" LabelCls="formlabel" LabelAlign="Right" LabelWidth="220"
                            Cls="Text-blue" ColumnWidth=".4">
                            <Listeners>
                                <Change Handler="this.removeCls('blue'); this.addCls('height');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_symptom" runat="server" FieldLabel="透析症状" LabelCls="formlabel" LabelWidth="180" LabelAlign="Right" 
                                Cls="Text-blue"  ColumnWidth=".4">
                            <Listeners>
                                <Change Handler="this.removeCls('blue'); this.addCls('height');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                        <ext:TextArea ID="TextArea2" runat="server" FieldLabel="更详细病情及治疗" LabelCls="formlabel" ColumnWidth="1"
                            LabelAlign="Right" PaddingSpec="10 30 2 2" Cls="blue2" LabelWidth="200" />
                    </Items>
                </ext:Container>                        
                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>            
                        <ext:TextField ID="TextField9" runat="server" AnchorHorizontal="100%" FieldLabel="医生" LabelCls="formlabel"
                            ColumnWidth=".4" LabelAlign="Right" Cls="read_field" LabelWidth="160" ReadOnly="true">
                        </ext:TextField>
                        <ext:TextField ID="TextField10" runat="server" AnchorHorizontal="100%" FieldLabel="下机" LabelCls="formlabel"
                            ColumnWidth=".4" LabelAlign="Right" Cls="read_field" LabelWidth="150" ReadOnly="true" >
                        </ext:TextField>
                    </Items>    
                </ext:Container>                        
                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="20">
                    <Items> 
                        <ext:ImageButton ID="ImageBtn_save" runat="server" ImageUrl="Styles/save.png" Height="50"
                            ColumnWidth="1" OverImageUrl="Styles/saveover.png">
                            <DirectEvents>
                                <Click OnEvent="Btn_save_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container99" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:ImageButton ID="ImageButton1" runat="server" ImageUrl="Styles/home1.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/homeover.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_Home_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton2" runat="server" ImageUrl="Styles/detail01.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detail01over.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail01_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton3" runat="server" ImageUrl="Styles/detail.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detailover.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton4" runat="server" ImageUrl="Styles/detail02.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/detail02over.png" Flex="1" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail02_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton5" runat="server" ImageUrl="Styles/back2.png" Height="50" PaddingSpec="0 0 0 0" OverImageUrl="Styles/back2over.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_back_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items> 
                </ext:Container>
            </Items>
        </ext:FormPanel>
        </div>
    </form>
</body>
</html>