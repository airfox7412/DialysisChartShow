<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_01_Standard.aspx.cs" Inherits="Dialysis_Chart_Show.Dialysis_09_01_Standard" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8,Auto-Rotate=Disable" />
    <title><% =Label2.Text%></title>
    <style type="text/css">
        
        <%--表頭--%>
        .label .x-label-value
        {
            width: 120px !important;
            height: 35px !important;
            font-size: 32px; <%--xx-large--%>
            color: #178951;
        }
        .label2 .x-label-value
        {
            font-size: 24px;
            display:block;
            height:24px;
            text-align:right;
            width:160px;
        }
        <%--文字框加大--%>
        .x-border-box .x-form-text
        {
            height: 32px !important;
            font-size: 24px; <%--x-large--%>
        }
        <%--文字框頭 對齊右--%>
        .x-form-item-label-right
        {
            font-size: 24px; <%--x-large--%>
        }
        <%--文字框尾--%>
        .x-field-indicator
        {
            font-size: 24px; <%--x-large--%>
        }
        <%--Windows使用--%>
        .x-window-header-text-default
        {
            font-size: 24px;  <%--x-large--%>
            line-height: 36px;
        }
        .x-box-item
        {
            height: 36px !important;
        }
        .x-btn .x-btn-center .x-btn-inner
        {
            font-size: 24px;  <%--x-large--%>
        }
        <%--OPT與CHK--%>
        .x-form-item
        {
            font-size: 24px;
            height: 24px;
            <%--font: normal 25px tahoma,arial,verdana,sans-serif;--%>
        }

        .Xx-tool img
        {
            height: 35px;
            width: 30px;
        }
        .Xx-fieldset-header .x-fieldset-header-text
        {
            font-size: 18px; <%--large--%>
        }
        .Xx-form-display-field
        {
            font-size: 20px;
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
            width: 34px;
            height: 34px;
            background-image: url("../Styles/che_btn.png");
        }
        
        #ImageButton1, #ImageBtn_Home, #ImageBtn_back, #ImageBtn_save
        {
            height: 50px !important;
        }

        .x-panel-header-text-default
        {
            font-size: 32px;  <%--xx-large--%>
            line-height: 36px;
        }
        <%--panel head 自动--%>
        .Xx-panel-header-text {
            font-size: 32px;
            line-height: 36px;
        }
        <%--Grid Row--%>
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 26px;
            line-height: 28px; 
        }
        <%--Grid Column--%>
        .x-column-header-inner .x-column-header-text
        {
            font-size: 18px; <%--large--%>
            line-height: 28px; 
        }
        
        .x-boundlist-item 
        { 
            font-size: 24px; 
            color: blue
        }
        
        .my-Field 
        {
            font-size: 24px;
            height: 30px;
            color: Black;
        }
        
        /* @group Blink */
        .blink {
            font-size: 24px;
            height: 30px;
            color: Red;
	        -webkit-animation: blink .75s linear infinite;
	        -moz-animation: blink .75s linear infinite;
	        -ms-animation: blink .75s linear infinite;
	        -o-animation: blink .75s linear infinite;
	         animation: blink .75s linear infinite;
        }
        @-webkit-keyframes blink {
	        0% { opacity: 1; }
	        50% { opacity: 1; }
	        50.01% { opacity: 0; }
	        100% { opacity: 0; }
        }
        @-moz-keyframes blink {
	        0% { opacity: 1; }
	        50% { opacity: 1; }
	        50.01% { opacity: 0; }
	        100% { opacity: 0; }
        }
        @-ms-keyframes blink {
	        0% { opacity: 1; }
	        50% { opacity: 1; }
	        50.01% { opacity: 0; }
	        100% { opacity: 0; }
        }
        @-o-keyframes blink {
	        0% { opacity: 1; }
	        50% { opacity: 1; }
	        50.01% { opacity: 0; }
	        100% { opacity: 0; }
        }
        @keyframes blink {
	        0% { opacity: 1; }
	        50% { opacity: 1; }
	        50.01% { opacity: 0; }
	        100% { opacity: 0; }
        }
        /* @end */
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="patient_name" runat="server" />
        <ext:Hidden ID="machine_type" runat="server" />
        <ext:Hidden ID="mechine_model" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="hpack" runat="server" />
        <ext:Hidden ID="hpack3" runat="server" />
        <ext:Hidden ID="patient_weight" runat="server" />
        <ext:Hidden ID="page" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="血液净化记录(标准版)" BodyStyle="background-color:#EBF5FF !important;" >
            <Items>
                <ext:Container ID="Container0" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="Label7" runat="server" ColumnWidth=".1" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label1" runat="server" Text="姓名:" ColumnWidth=".1" Cls="label" Margins="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label2" runat="server" ColumnWidth=".2" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label3" runat="server" Text="   楼层:" ColumnWidth=".15" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label4" runat="server" ColumnWidth=".15" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label5" runat="server" Text="   床号:" ColumnWidth=".15" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label6" runat="server" ColumnWidth=".15" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container1" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="cbo_diagnosis" runat="server" ColumnWidth=".5" FieldLabel="诊断" LabelWidth="160" Cls="Text-blue" LabelAlign="Right" PaddingSpec="10 50 0 2" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_mechine_model" runat="server" ColumnWidth=".5" FieldLabel="透析器型号" LabelWidth="160" Cls="Text-blue" LabelAlign="Right" PaddingSpec="10 50 0 2" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container2" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="info_date1" runat="server" FieldLabel="日期" LabelWidth="160" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" ReadOnly="true" Flex="1"/>
                        <ext:SelectBox ID="cbo_dialysis_type" runat="server" FieldLabel="透析方式" LabelWidth="160" ColumnWidth=".5" Cls="Text-blue" LabelAlign="Right" PaddingSpec="10 50 0 2" Flex="1"> 
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container3" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="txt_weight_before" runat="server" FieldLabel="透析前体重" LabelWidth="160" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-red" Flex="1"> 
                            <DirectEvents>
                                <Change OnEvent="text_deactivate" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField> 
                        <ext:TextField ID="txt_weight_after_expect" runat="server" FieldLabel="干体重" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1">
                            <DirectEvents>
                                <Change OnEvent="text_deactivate" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container4" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="txt_weight_after" runat="server" FieldLabel="透析后体重" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1"> 
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="TextField3" runat="server" FieldLabel="目标定容量" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-red" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                            <DirectEvents>
                                <blur OnEvent="TextField3_change" />
                            </DirectEvents>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container5" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField5" runat="server" FieldLabel="透析开始" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" ReadOnly="true" Flex="1"/>
                        <ext:TextField ID="TextField8" runat="server" FieldLabel="肝素首量" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="mg" Cls="Text-blue" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container6" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField6" runat="server" FieldLabel="透析结束" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" ReadOnly="true" Flex="1"/>
                        <ext:TextField ID="TextField10" runat="server" FieldLabel="低分子肝素" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="u" Cls="Text-blue" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container7" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField7" runat="server" FieldLabel="透析时间" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="小时" Cls="Text-blue" LabelWidth="160" ReadOnly="true" Flex="1" />
                        <ext:TextField ID="TextField9" runat="server" FieldLabel="追加量" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="mg/h" Cls="Text-blue" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container8" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="SelectBox3" runat="server" FieldLabel="透析液钙" LabelWidth="160" LabelAlign="Right" ColumnWidth=".5" PaddingSpec="10 50 0 2" IndicatorText="mmol/L" Cls="Text-blue" Flex="1">
                            <Items>
                                <ext:ListItem Text="1.5" Value="1.5" />
                                <ext:ListItem Text="1.25" Value="1.25" />
                                <ext:ListItem Text="2" Value="2" />
                            </Items>
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:TextField ID="TextField11" runat="server" FieldLabel="置换量" LabelWidth="160" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="L" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container9" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="cbo_hpack3" runat="server" FieldLabel="管路型号" LabelWidth="160" LabelAlign="Right" ColumnWidth=".48" LabelCls="my-Field" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_h_type" runat="server" FieldLabel="血管通路类型" LabelWidth="175" LabelAlign="Right" ColumnWidth="0.52" LabelCls="my-Field" PaddingSpec="10 50 0 2" Cls="Text-blue" DisplayField="state" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container10" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="aa" runat="server" Text="拟用药:" ColumnWidth=".15" PaddingSpec="10 0 0 0" Cls="label2" />
                        <ext:Checkbox runat="server" ID="Checkbox1" BoxLabel="EPO" ColumnWidth=".25" PaddingSpec="10 0 0 2" CheckedCls="CheckBox-red" Checked="false" />
                        <ext:Checkbox runat="server" ID="Checkbox2" BoxLabel="左卡" ColumnWidth=".25" PaddingSpec="10 0 0 2" CheckedCls="CheckBox-red" Checked="false" />
                        <ext:Checkbox runat="server" ID="Checkbox3" BoxLabel="铁剂" ColumnWidth=".25" PaddingSpec="10 0 0 2" CheckedCls="CheckBox-red" Checked="false" />
                        <ext:Checkbox runat="server" ID="Checkbox4" BoxLabel="钙剂" ColumnWidth=".25" PaddingSpec="10 0 0 2" CheckedCls="CheckBox-red" Checked="false" />
                        <ext:Checkbox runat="server" ID="Checkbox5" BoxLabel="抗菌素/其它" ColumnWidth=".1" CheckedCls="CheckBox-red" Checked="false" />
                        <ext:TextField ID="TextCheckbox5" runat="server" FieldLabel="" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 0 0 2" Cls="Text-blue">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container14" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="cbo_change_type" runat="server" ColumnWidth=".5" FieldLabel="置换方式" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Hidden="true" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container21" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>          	
                        <ext:TextField ID="TextField13" runat="server" FieldLabel="上机者" ReadOnly="true" LabelWidth="90" LabelAlign="Right" PaddingSpec="10 10 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="TextField131" runat="server" FieldLabel="核對者" ReadOnly="true" LabelWidth="90" LabelAlign="Right" PaddingSpec="10 10 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="TextField24" runat="server" FieldLabel="下机者" ReadOnly="true" LabelWidth="90" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container22" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>    
                        <ext:TextField ID="TextField12" runat="server" FieldLabel="穿刺者" ReadOnly="true" LabelWidth="100" LabelAlign="Right" PaddingSpec="10 10 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="TextField25" runat="server" FieldLabel="用药护士" ReadOnly="true" LabelWidth="140" ColumnWidth=".32" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="TextField23" runat="server" FieldLabel="医生" ReadOnly="true" LabelWidth="110" ColumnWidth=".30" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container23" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextArea ID="TextArea1" runat="server" FieldLabel="透析前症状及处理" ColumnWidth=".8" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue-H" LabelWidth="230" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue-H'); this.addCls('Text-black-H');" Single="true" />
                            </Listeners>
                        </ext:TextArea>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container24" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:ImageButton ID="ImageButton1" runat="server" ImageUrl="../Styles/save.png" Height="50" ColumnWidth="1" OverImageUrl="../Styles/saveover.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_save_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items> 
                </ext:Container>
            </Items>
        </ext:FormPanel>
        <ext:Window ID="Window1" runat="server" Title="请输入工号" Height="160" Closable="false" 
            Width="350" BodyStyle="background-color: #fff;" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center">
            <Items>
                <ext:TextField ID="TextField_UserID" runat="server" FieldLabel="工号" ColumnWidth="1" LabelAlign="Right" Padding="5" />
            </Items>
            <Buttons>
                <ext:Button ID="Button4" runat="server" Icon="Accept" Text="确认" Width="150" Height="40">
                    <DirectEvents>
                        <Click OnEvent="btnDecrypt_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button5" runat="server" Icon="Cancel" Text="取消" Width="150" Height="40">
                    <DirectEvents>
                        <Click OnEvent="btnClose_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </div>
    </form>
</body>
</html>