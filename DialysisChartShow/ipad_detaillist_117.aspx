<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_detaillist_117.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_detaillist_117" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><% =Label2.Text%></title>
    <link href="css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        function StartTime() {
            var str = App.TextField5.getValue();
            var a = str.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);
            if (a == null) { alert('透析开始不是时间格式'); return false; }
            if (a[1] > 24 || a[3] > 60 || a[4] > 60) {
                alert("透析开始时间格式不对");
                return false
            }
            return true;
        }
        function StopTime() {
            var str = App.TextField6.getValue();
            var a = str.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);
            if (a == null) { alert('透析结束不是时间格式'); return false; }
            if (a[1] > 24 || a[3] > 60 || a[4] > 60) {
                alert("透析结束时间格式不对");
                return false
            }
            return true;
        }
        function isTime() {
            var str = App.TextFieldTime.getValue();
            var a = str.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);
            if (a == null) { alert('纪录时间不是时间格式'); return false; }
            if (a[1] > 24 || a[3] > 60 || a[4] > 60) {
                alert("纪录时间格式不对");
                return false
            }
            return true;
        } 
    </script>
    <style type="text/css"> 
        <%--表頭--%>
        .label .x-label-value
        {
            width: 120px !important;
            height: 35px !important;
            font-size: 32px; 
            color: #178951;
        }
        .label2 .x-label-value
        {
            text-align:right;
            display:block;
            font-size: 24px;
            width:160px;
            height:24px;
            line-height:32px;
        }
                
        <%--Grid Row--%>
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 20px;
            line-height: 24px; 
        }
        .x-column-header-text-inner
        {
            font-size: 16px;
        }
        
        <%--Windows使用--%>
        .x-title-text
        {
        	font-size: 24px;
        }
        .x-window-header-text-default
        {
            font-size: 24px;
            line-height: 24px;
        }
        
        .x-border-box .x-form-text
        {
            height: 36px !important;
            font-size: x-large;
        }
        .x-box-item
        {
            height: 24px !important;
        }
        
        <%--文字框頭 對齊右--%>
        .x-form-item-label-right
        {
            font-size: 24px;
        }
        
        .x-field-indicator
        {
            font-size: 24px;
        }
        
        .Text-blue .x-form-field
        {
            font-size: 20px !important;
            height: 30px !important;
            color: Blue;
        }
        
        .Text-red .x-form-field
        {
            font-size: 20px !important;
            height: 30px !important;
            color: Red;
        }
        .my-Field 
        {
            font-size: 24px;
            color: Black;
        }
        .my-Field-red 
        {
            font-size: 20px;
            height: 30px;
            color: Red;
        }
        .blue-Field 
        {
            font-size: 20px;
            height: 30px;
            color: Blue;
        }

        <%--ComboBox Items--%>
        .x-boundlist-item 
        { 
            font-size: 32px;
            line-height: 32px;
        }
        
        <%--panel head 自动--%>
        .x-panel-header-text {
            font-size: 20px;
            font-weight: bold;
            line-height: 20px;
            color: #04408c;
        }
        
        /* @group Blink */
        .blink {
            font-size: 20px;
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
        .read_field .x-form-field
        {
            background-color: #EEEEEE;
        }
        /* @end */
        
        #ImageBtn_save1, #ImageBtn_save2, #Btn_Home, #Btn_detail01, #Btn_detail_Click, #Btn_detail, #Btn_detail02, #Btn_back, #ImageButton2, #ImageButton3, #ImageButton4, #ImageButton5, #ImageButton6
        {
            height: 50px !important;
        }
        
        #ImageBtn_TurnOff
        {
            height: 100px !important;
        }
		.checkbox_blue .x-form-cb-label-default {
			font-size: 24px;
			color: Blue;
		}
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="patient_name" runat="server" />
        <ext:Hidden ID="machine_type" runat="server" />
        <ext:Hidden ID="hpack" runat="server" />
        <ext:Hidden ID="patient_weight" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />

        <ext:Hidden ID="mechine_model" runat="server" />
        <ext:Hidden ID="Hidden4" runat="server" />
        <ext:Hidden ID="hpack3" runat="server" />
        <ext:Hidden ID="page" runat="server" />
        <ext:Hidden ID="nurse_flag" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />   	
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="血液净化记录" BodyStyle="background-color:#EBF5FF !important;" >
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
                <ext:Container ID="Container0" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="Label7" runat="server" ColumnWidth=".1" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label1" runat="server" Text="姓名:" ColumnWidth=".1" Cls="label" Margins="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label2" runat="server" ColumnWidth=".2" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label3" runat="server" Text="   楼层:" ColumnWidth=".15" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label4" runat="server" ColumnWidth=".15" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label5" runat="server" Text="   床号:" ColumnWidth=".15" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:Label ID="Label6" runat="server" ColumnWidth=".15" Cls="label" PaddingSpec="2 2 5 2" Flex="1"/>
                        <ext:ImageButton ID="ImageBtn_TurnOff" runat="server" ImageUrl="Styles/dis_Btn.png" Flex="1" Hidden="true">
                            <DirectEvents>
                                <Click OnEvent="ImageBtn_TurnOff_click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container1" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="cbo_diagnosis" runat="server" ColumnWidth=".5" FieldLabel="诊断" Cls="Text-blue" LabelWidth="160" LabelAlign="Right" PaddingSpec="10 50 0 2" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_mechine_model" runat="server" ColumnWidth=".5" FieldLabel="透析器型号" Cls="Text-blue" LabelWidth="160" LabelAlign="Right" PaddingSpec="10 50 0 2" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container2" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>                         
                        <ext:SelectBox ID="cbo_dialysis_type" runat="server" ColumnWidth=".5" FieldLabel="透析方式" Cls="Text-blue" LabelWidth="160" LabelAlign="Right" PaddingSpec="10 50 0 2" Flex="1"> 
                            <DirectEvents>
                                <Change OnEvent="text_dtype" />
                            </DirectEvents>
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:TextField ID="txt_weight_before" runat="server" FieldLabel="透析前体重" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-red" LabelWidth="160" Flex="1"> 
                            <DirectEvents>
                                <Change OnEvent="text_deactivate" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container3" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>                         
                        <ext:TextField ID="txt_weight_after_expect" runat="server" FieldLabel="干体重" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1">
                            <DirectEvents>
                                <Change OnEvent="text_deactivate" />
                            </DirectEvents>
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
                <ext:Container ID="Container4" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>                           
                        <ext:TextField ID="txt_weight_after" runat="server" FieldLabel="透析后体重" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1"> 
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="TextTotalCap" runat="server" FieldLabel="总定容量" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-red" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container25" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:SelectBox ID="cb_info_date" runat="server" FieldLabel="透析日期" ColumnWidth=".4" LabelAlign="Right" PaddingSpec="10 100 0 2" LabelWidth="160" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Change OnEvent="ChangDialysisDate"/>
                            </DirectEvents>
                        </ext:SelectBox>
                        <ext:Label ID="space" runat="server" FieldLabel="" ColumnWidth=".6" PaddingSpec="10 100 0 2" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container5" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField5" runat="server" FieldLabel="透析开始" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" LabelWidth="160" Cls="Text-blue" ReadOnly="false" Flex="1">
                            <DirectEvents>
                                <Change OnEvent="text_CalTime" />
                            </DirectEvents>
                        </ext:TextField>
                        <ext:TextField ID="TextField6" runat="server" FieldLabel="透析结束" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" LabelWidth="160" Cls="Text-blue" ReadOnly="false" Flex="1">
                            <DirectEvents>
                                <Change OnEvent="text_CalTime" />
                            </DirectEvents>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container6" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>  
                        <ext:TextField ID="TextField7" runat="server" FieldLabel="透析时间" ColumnWidth=".5" IndicatorText="小时" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" ReadOnly="true" Flex="1"/>
                        <ext:TextField ID="TextField8" runat="server" FieldLabel="封管液" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="mg" Cls="Text-blue" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container7" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField9" runat="server" FieldLabel="追加量" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="mg/h" Cls="Text-blue" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="TextField10" runat="server" FieldLabel="低分子肝素" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="u" Cls="Text-blue" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container8" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="Label8" runat="server" Text="医嘱用药:" ColumnWidth=".15" PaddingSpec="5 0 30 0" Cls="label2" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container26" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="Label10" runat="server" ColumnWidth=".15" Cls="label2" />
                        <ext:Checkbox ID="Checkbox1" BoxLabel="EPO" runat="server" LabelWidth="200" Cls="checkbox_blue" Checked="false" />
                        <ext:SelectBox ID="SelectBoxEPO" runat="server" ColumnWidth=".2" LabelCls="my-Field" PaddingSpec="5 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:Checkbox ID="Checkbox2" BoxLabel="左卡" runat="server" LabelWidth="200" LabelAlign="Left" Cls="checkbox_blue" Checked="false" />
                        <ext:SelectBox ID="SelectBoxLcard" runat="server" ColumnWidth=".2" LabelCls="my-Field" PaddingSpec="5 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container9" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="Label9" runat="server" ColumnWidth=".15" Cls="label2" />
                        <ext:Checkbox ID="Checkbox3" BoxLabel="铁剂" runat="server" ColumnWidth=".1" Cls="checkbox_blue" Checked="false" />
                        <ext:SelectBox ID="SelectBoxFe" runat="server" ColumnWidth=".2" LabelCls="my-Field" PaddingSpec="5 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:Checkbox ID="Checkbox4" BoxLabel="骨化三醇" runat="server" ColumnWidth=".15" Cls="checkbox_blue" Checked="false" />
                        <ext:SelectBox ID="SelectBoxCalcitriol" runat="server" ColumnWidth=".2" LabelCls="my-Field" PaddingSpec="5 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container10" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="Label110" runat="server" ColumnWidth=".15" Cls="label2" />
                        <ext:Checkbox ID="Checkbox5" BoxLabel="弥可保" runat="server" ColumnWidth=".1" Cls="checkbox_blue" Checked="false" />
                        <ext:SelectBox ID="SelectBoxMethycobal" runat="server" ColumnWidth=".2" LabelCls="my-Field" PaddingSpec="5 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:Checkbox ID="Checkbox6" BoxLabel="维生素B12" runat="server" ColumnWidth=".15" Cls="checkbox_blue" Checked="false" />
                        <ext:SelectBox ID="SelectBoxB12" runat="server" ColumnWidth=".2" LabelCls="my-Field" PaddingSpec="5 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container17" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextArea ID="TextArea1" runat="server" FieldLabel="医嘱" ColumnWidth=".8" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue-H" LabelWidth="160" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue-H'); this.addCls('Text-black-H');" Single="true" />
                            </Listeners>
                        </ext:TextArea>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container11" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:SelectBox ID="cbo_SelDialysisNa" runat="server" FieldLabel="可调钠" ColumnWidth=".56" LabelAlign="Right" PaddingSpec="30 50 0 2" IndicatorText="mmol/L" LabelWidth="160" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:TextField ID="TextField11" runat="server" FieldLabel="置换量" ColumnWidth=".43" LabelAlign="Right" PaddingSpec="30 50 0 2" IndicatorText="L" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container12" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:SelectBox ID="cbo_hpack3" runat="server" FieldLabel="管路型号" LabelWidth="160" LabelAlign="Right" ColumnWidth=".48" LabelCls="my-Field" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">                            <Listeners>
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
                <ext:Container ID="Container13" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="cbo_DialysisMachine" runat="server" FieldLabel="透析器凝血" LabelCls="formlabel" LabelAlign="Right" LabelWidth="180"
                                Cls="Text-blue" ColumnWidth=".3" PaddingSpec="10 50 0 2" Flex="1">
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_HeparinPump_a" runat="server" FieldLabel="动脉壶凝血" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" 
                                Cls="Text-blue" ColumnWidth=".3" PaddingSpec="10 50 0 2" Flex="1" >
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_HeparinPump_v" runat="server" FieldLabel="静脉壶凝血" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" 
                                Cls="Text-blue" ColumnWidth=".3" PaddingSpec="10 50 0 2" Flex="1" >
                        </ext:SelectBox>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container14" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextFieldCatheterAccess" runat="server" FieldLabel="置管情况" ColumnWidth=".4" LabelWidth="150" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1" />
                        <ext:TextField ID="TextFieldMuscleAtrophy" runat="server" FieldLabel="内瘘情况" ColumnWidth=".4" LabelWidth="150" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container15" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField13" runat="server" FieldLabel="上机" ReadOnly="true" LabelWidth="110" ColumnWidth=".24" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                        <ext:TextField ID="TextField24" runat="server" FieldLabel="下机" ReadOnly="true" LabelWidth="110" ColumnWidth=".24" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container16" runat="server" FieldLabel="" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField131" runat="server" FieldLabel="管组" ReadOnly="true" LabelWidth="110" ColumnWidth=".24" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
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
                <%--淨化存盤--%>
                <ext:Container ID="Container18" runat="server" Layout="CenterLayout">
                    <Items> 
                        <ext:ImageButton ID="ImageBtn_save1" runat="server" ImageUrl="Styles/save.png" Height="50" OverImageUrl="Styles/saveover.png">
                            <DirectEvents>
                                <Click OnEvent="ImageBtn_save1_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items> 
                </ext:Container>
                <ext:Panel ID="Panel1" runat="server" ButtonAlign="Center" ColumnWidth="1" Layout="AnchorLayout" Title="净化过程明细" BodyStyle="background-color:#EBF5FF !important;">
                    <Items>
                        <ext:TextField ID="TextField1" runat="server" FieldLabel="TIME" LabelWidth="100" ColumnWidth=".1" LabelAlign="Right" Hidden="true" />
                        <ext:Container ID="Container19" runat="server" Layout="HBoxLayout">
                            <Items> 
                                <ext:TextField ID="TextField_7" runat="server" FieldLabel="电导" LabelWidth="100" ColumnWidth=".33" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" IndicatorText="" Flex="1" />
                                <ext:TextField ID="TextField_6" runat="server" FieldLabel="温度" LabelWidth="100" ColumnWidth=".33" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" IndicatorText="℃" Flex="1" />
                                <ext:TextField ID="TextField_2" runat="server" FieldLabel="已超滤" LabelWidth="100" ColumnWidth=".34" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" IndicatorText="L" Flex="1" />
                            </Items> 
                        </ext:Container>
                        <ext:Container ID="Container20" runat="server" Layout="HBoxLayout">
                            <Items> 
                                <ext:TextField ID="TextField_10" runat="server" FieldLabel="跨膜压" LabelWidth="100" ColumnWidth=".33" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" IndicatorText="mmHg" Flex="1" />
                                <ext:TextField ID="TextField_8" runat="server" FieldLabel="静膜压" LabelWidth="100" ColumnWidth=".33" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" IndicatorText="mmHg" Flex="1" />
                                <ext:TextField ID="TextField_4" runat="server" FieldLabel="血流量" LabelWidth="100" ColumnWidth=".34" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" IndicatorText="ml/min" Flex="1" />
                            </Items> 
                        </ext:Container>
                        <ext:Container ID="Container27" runat="server" Layout="HBoxLayout">
                            <Items> 
                                <ext:TextField ID="TextField17" runat="server" FieldLabel="T**" LabelWidth="100" ColumnWidth=".33" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="red" IndicatorText="℃" Flex="1" />
                                <ext:TextField ID="TextField18" runat="server" FieldLabel="P**" LabelWidth="100" ColumnWidth=".33" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="red" IndicatorText="次/分" Flex="1" />
                                <ext:TextField ID="TextField19" runat="server" FieldLabel="R**" LabelWidth="100" ColumnWidth=".34" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="red" IndicatorText="次/分" Flex="1" />
                            </Items> 
                        </ext:Container>
                        <ext:Container ID="Container28" runat="server" Layout="HBoxLayout">
                            <Items> 
                                <ext:TextField ID="TextField20" runat="server" FieldLabel="BP**" MaskRe="[0-9\/]" LabelWidth="100" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="red" IndicatorText="mmHg" Flex="1" />
                                <ext:TextField ID="TextField21" runat="server" FieldLabel="核对护士" LabelWidth="120" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" EnableKeyEvents="true" Flex="1"> 
                                    <DirectEvents>
                                        <KeyPress OnEvent="text_click">
                                            <ExtraParams>
                                                <ext:Parameter Name="keynum" Value="e.getKey()" Mode="Raw" />
                                            </ExtraParams>
                                        </KeyPress>
                                    </DirectEvents>
                                </ext:TextField>
                                <ext:TextField ID="TextFieldTime" runat="server" FieldLabel="纪录时间" MaskRe="[0-9:]" LabelWidth="110" ColumnWidth=".2" LabelAlign="Right" PaddingSpec="10 50 0 2" ReadOnly="false" Flex="1"> 
                                    <Listeners>
                                        <Blur Fn="isTime"></Blur>
                                    </Listeners>
                                </ext:TextField>
                            </Items> 
                        </ext:Container>
                        <ext:Container ID="Container29" runat="server" Layout="HBoxLayout">
                            <Items> 
                                <ext:TextArea ID="TextArea2" runat="server" FieldLabel="病情及处理**" ColumnWidth="1" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-black-H" LabelWidth="180" Flex="1" />
                            </Items> 
                        </ext:Container>
                        <%--淨化過程明細存盤--%>
                        <ext:Container ID="Container30" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                            <Items> 
                                <ext:ImageButton ID="ImageBtn_save2" runat="server" ImageUrl="Styles/savebp.png" Height="50" ColumnWidth="1" OverImageUrl="Styles/savebpover.png" Flex="1">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_save_bp_Click" />
                                    </DirectEvents>
                                </ext:ImageButton>
                            </Items> 
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:GridPanel ID="Grid_Show_TPRBP" runat="server" Title="净化明细" Margins="0 0 5 5" ColumnWidth="1" >
                    <Store>
                        <ext:Store ID="Store" runat="server">
                            <Model>
                                <ext:Model ID="Model" runat="server" Name="PastSickness">
                                    <Fields>
                                        <ext:ModelField Name="cln2_date" Type="String"/>
                                        <ext:ModelField Name="cln2_time" Type="String" />
                                        <ext:ModelField Name="column_7" Type="String" />
                                        <ext:ModelField Name="column_6" Type="String" />
                                        <ext:ModelField Name="column_2" Type="String" />
                                        <ext:ModelField Name="column_10" Type="String" />
                                        <ext:ModelField Name="column_8" Type="String" />
                                        <ext:ModelField Name="column_4" Type="String" />
                                        <ext:ModelField Name="cln2_t" Type="String" />
                                        <ext:ModelField Name="cln2_p" Type="String" />
                                        <ext:ModelField Name="cln2_r" Type="String" />
                                        <ext:ModelField Name="cln2_bp" Type="String" />
                                        <ext:ModelField Name="cln2_rmk" Type="String" />
                                        <ext:ModelField Name="cln2_user" Type="String" />
                                        <ext:ModelField Name="cln2_dateadded" Type="String" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Reader>
                                <ext:ArrayReader />
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel1" runat="server">
                        <Columns>
                            <ext:Column ID="Column_7" runat="server" DataIndex="column_7" Text="电导" Width="80" >
                                <Commands>
                                    <ext:ImageCommand CommandName="ChartEdit" Icon="Pencil" Style="margin-left:5px !important;" >
                                        <ToolTip Text="修改" />
                                    </ext:ImageCommand>
                                </Commands>
                                <DirectEvents>
                                    <Command OnEvent="Edit_Click" >
                                        <ExtraParams>
                                            <ext:Parameter Name="TIME" Value="record.data.cln2_time" Mode="Raw"/>
                                        </ExtraParams> 
                                    </Command> 
                                </DirectEvents>
                            </ext:Column>
                            <ext:Column ID="Column_6" runat="server" DataIndex="column_6" Text="温度" Width="80" />
                            <ext:Column ID="Column_2" runat="server" DataIndex="column_2" Text="已超滤" Width="80" />
                            <ext:Column ID="Column_10" runat="server" DataIndex="column_10" Text="跨膜压" Width="80" />
                            <ext:Column ID="Column_8" runat="server" DataIndex="column_8" Text="静脉压" Width="80" />
                            <ext:Column ID="Column_4" runat="server" DataIndex="column_4" Text="血流量" Width="80" />
                            <ext:Column ID="Column1" runat="server" DataIndex="cln2_t" Text="T ℃" Width="80" />
                            <ext:Column ID="Column2" runat="server" DataIndex="cln2_p" Text="P 次/分" Width="70" />
                            <ext:Column ID="Column6" runat="server" DataIndex="cln2_r" Text="R 次/分" Width="70" />
                            <ext:Column ID="Column10" runat="server" DataIndex="cln2_bp" Text="BP mmHg" Width="120" />
                            <ext:Column ID="Column11" runat="server" DataIndex="cln2_rmk" Text="病情及处理" Width="300" />
                            <ext:Column ID="Column4" runat="server" DataIndex="cln2_user" Text="核对护士" Width="150" />
                            <ext:Column ID="Column5" runat="server" DataIndex="cln2_dateadded" Text="记录时间" Width="150" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">
                        </ext:RowSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
                <ext:Container ID="Container99" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:ImageButton ID="ImageButton2" runat="server" ImageUrl="Styles/home1.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/homeover.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_Home_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton3" runat="server" ImageUrl="Styles/detail01.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detail01over.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail01_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton4" runat="server" ImageUrl="Styles/detail.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detailover.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton5" runat="server" ImageUrl="Styles/detail02.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/detail02over.png" Flex="1" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail02_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton6" runat="server" ImageUrl="Styles/back2.png" Height="50" PaddingSpec="0 0 0 0" OverImageUrl="Styles/back2over.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_back_Click" />
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
        <ext:TaskManager ID="TaskManager1" runat="server" >
            <Tasks>
                <ext:Task TaskID="COUNT_DOWN" Interval="180000" >
                    <DirectEvents>
                        <Update OnEvent="Timer1_Timer" />
                    </DirectEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>
    </div>
    </form>
</body>
</html>