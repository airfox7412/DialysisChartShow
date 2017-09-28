<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_history_Henan.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_history_Henan" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=Label2.Text%></title>
    <link href="css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        function isTime() {
            var str = App.TextFieldTime.getValue();
            var a = str.match(/^(\d{1,2})(:)?(\d{1,2})\2(\d{1,2})$/);
            if (a == null) { alert('输入的参数不是时间格式'); return false; }
            if (a[1] > 24 || a[3] > 60 || a[4] > 60) {
                alert("时间格式不对");
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
            font-size: 24px;
            display:block;
            height:24px;
            text-align:right;
            width:160px;
        }
                
        <%--Grid Row--%>
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 20px;
            line-height: 24px; 
        }
        .x-column-header-text-inner
        {
            font-size: 18px;
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
        .x-btn-button
        {
            height:32px;
            width:32px;
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
        
        <%--Button Text--%>
        .x-btn .x-btn-center .x-btn-inner
        {
            font-size: large;
        }
        
        #ImageButton1, #ImageBtn_save, #Btn_Home, #Btn_detail01, #Btn_detail_Click, #Btn_detail, #Btn_detail02, #Btn_back, #ImageButton2, #ImageButton3, #ImageButton4, #ImageButton5, #ImageButton6
        {
            height: 50px !important;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        	
        <ext:FormPanel ID="FormPanel1" runat="server" Title="血液净化记录" ButtonAlign="Center" Padding="5" MonitorResize="true">
            <Items>
                <ext:Container ID="Container00" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:ImageButton ID="Btn_Home" runat="server" ImageUrl="Styles/home1.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/homeover.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_Home_Click" Single="false" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_detail01" runat="server" ImageUrl="Styles/detail01over.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_detail01_Click" Single="false" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_detail" runat="server" ImageUrl="Styles/detail.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detailover.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail_Click" Single="false" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_detail02" runat="server" ImageUrl="Styles/detail02.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/detail02over.png" Flex="1" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail02_Click" Single="false" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_back" runat="server" ImageUrl="Styles/back2.png" Height="50" PaddingSpec="0 0 0 0" OverImageUrl="Styles/back2over.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_back_Click" Single="false" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container1" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="Label7" runat="server" ColumnWidth=".1" Cls="label" PaddingSpec="10 2 5 2" Flex="1"/>
                        <ext:Label ID="Label1" runat="server" Text="姓名:" ColumnWidth=".1" Cls="label" PaddingSpec="10 2 5 2" Flex="1"/>
                        <ext:Label ID="Label2" runat="server" ColumnWidth=".2" Cls="label" PaddingSpec="10 2 5 2" Flex="1"/>
                        <ext:Label ID="Label3" runat="server" Text="   楼层:" ColumnWidth=".15" Cls="label" PaddingSpec="10 2 5 2" Flex="1"/>
                        <ext:Label ID="Label4" runat="server" ColumnWidth=".15" Cls="label" PaddingSpec="10 2 5 2" Flex="1"/>
                        <ext:Label ID="Label5" runat="server" Text="   床号:" ColumnWidth=".15" Cls="label" PaddingSpec="10 2 5 2" Flex="1"/>
                        <ext:Label ID="Label6" runat="server" ColumnWidth=".15" Cls="label" PaddingSpec="10 2 5 2" Flex="1"/>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container2" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="info_date1" runat="server" FieldLabel="日期" LabelWidth="160" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" ReadOnly="true" Flex="1"/>
                        <ext:SelectBox ID="cbo_dialysis_type" runat="server" FieldLabel="透析方式" LabelWidth="160" ColumnWidth=".5" Cls="Text-blue" LabelAlign="Right" PaddingSpec="10 50 0 2" Flex="1">
                            <DirectEvents>
                                <Change OnEvent="cbo_dialysis_Change" />
                            </DirectEvents>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container121" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="cbo_hpack2" runat="server" ColumnWidth=".5" FieldLabel="透析器型号" LabelWidth="160" Cls="Text-blue" LabelAlign="Right" LabelCls="my-Field" PaddingSpec="10 50 0 2" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_hpack4" runat="server" ColumnWidth=".5" FieldLabel="透析器型号二" LabelWidth="160" Cls="Text-blue" LabelAlign="Right" LabelCls="my-Field" PaddingSpec="10 50 0 2" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container3" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="cbo_hpack3" runat="server" FieldLabel="管路型号" LabelWidth="160" LabelAlign="Right" ColumnWidth=".48" LabelCls="my-Field" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_hpack5" runat="server" FieldLabel="管路型号二" LabelWidth="160" LabelAlign="Right" ColumnWidth=".48" LabelCls="my-Field" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <Listeners>
                                <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container4" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="txt_weight_before" runat="server" FieldLabel="透析前体重" LabelWidth="160" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-red" Flex="1" />
                        <ext:TextField ID="txt_weight_after_expect" runat="server" FieldLabel="干体重" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container5" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="txt_weight_after" runat="server" FieldLabel="透析后体重" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                        <ext:TextField ID="TextField3" runat="server" FieldLabel="目标定容量" ColumnWidth=".5" IndicatorText="kg" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-red" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container6" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField5" runat="server" FieldLabel="透析开始" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" ReadOnly="false" Flex="1" />
                        <ext:SelectBox ID="SelectBox10" runat="server" FieldLabel="抗凝药物" LabelWidth="160" LabelAlign="Right" ColumnWidth=".5" LabelCls="my-Field" PaddingSpec="10 50 0 2" Cls="Text-blue" IndicatorText="" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container7" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField6" runat="server" FieldLabel="透析结束" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" ReadOnly="false" Flex="1" />
                        <ext:TextField ID="TextField8" runat="server" FieldLabel="首次剂量" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container8" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:TextField ID="TextField7" runat="server" FieldLabel="透析时间" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="小时" Cls="Text-blue" LabelWidth="160" ReadOnly="true" Flex="1" />
                        <ext:TextField ID="TextAdd" runat="server" FieldLabel="追加量" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container9" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:Label ID="sp1" runat="server" Text=" " ColumnWidth=".5" PaddingSpec="10 50 0 2" Flex="1" Hidden="true" />
                        <ext:TextField ID="TextField11" runat="server" FieldLabel="置换量" LabelWidth="160" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="L" Cls="Text-blue" Flex="1" />
                        <ext:TextField ID="TextAmount" runat="server" FieldLabel="总量" LabelWidth="160" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" IndicatorText="" Cls="Text-blue" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container10" runat="server" FieldLabel="" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:SelectBox ID="cbo_diagnosis" runat="server" ColumnWidth=".5" FieldLabel="诊断" LabelWidth="160" Cls="Text-blue" LabelAlign="Right" LabelCls="my-Field" PaddingSpec="10 50 0 2" Flex="1" />
                        <ext:SelectBox ID="cbo_h_type" runat="server" FieldLabel="血管通路类型" LabelWidth="160" LabelAlign="Right" ColumnWidth="0.52" LabelCls="my-Field" PaddingSpec="10 50 0 2" Cls="Text-blue" DisplayField="state" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container11" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                    	<ext:TextField ID="TextField2" runat="server" FieldLabel="侧位" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                        <ext:TextField ID="TextField4" runat="server" FieldLabel="部位" ColumnWidth=".5" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container12" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField9" runat="server" FieldLabel="血流速" ColumnWidth=".5" IndicatorText="mmol/L" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                        <ext:TextField ID="TextField10" runat="server" FieldLabel="尿量" ColumnWidth=".5" IndicatorText="ml/24h" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" LabelWidth="160" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container21" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>          	    
                        <ext:TextField ID="TextField12" runat="server" FieldLabel="穿刺者" LabelWidth="100" LabelAlign="Right" PaddingSpec="10 10 10 2" Cls="Text-blue" EnableKeyEvents="true" Flex="1" />
                        <ext:TextField ID="TextField13" runat="server" FieldLabel="上机者" LabelWidth="80" LabelAlign="Right" PaddingSpec="10 10 10 2" Cls="Text-blue" EnableKeyEvents="true" Flex="1" />
                        <ext:TextField ID="TextField131" runat="server" FieldLabel="核對者" LabelWidth="80" LabelAlign="Right" PaddingSpec="10 10 10 2" Cls="Text-blue" EnableKeyEvents="true" Flex="1" />
                        <ext:TextField ID="TextField24" runat="server" FieldLabel="下机者" LabelWidth="80" LabelAlign="Right" PaddingSpec="10 50 10 2" Cls="Text-blue" EnableKeyEvents="true" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container22" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField14" runat="server" FieldLabel="用药核对护士" LabelWidth="170" ColumnWidth=".34" LabelAlign="Right" PaddingSpec="10 50 10 2" Cls="Text-blue" EnableKeyEvents="true" Flex="1" />
                        <ext:TextField ID="TextField25" runat="server" FieldLabel="用药护士" LabelWidth="140" ColumnWidth=".32" LabelAlign="Right" PaddingSpec="10 50 10 2" Cls="Text-blue" EnableKeyEvents="true" Flex="1" />
                        <ext:TextField ID="TextField23" runat="server" FieldLabel="医生" LabelWidth="110" ColumnWidth=".30" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" EnableKeyEvents="true" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container23" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextArea ID="TextArea1" runat="server" FieldLabel="透析前症状及处理" ColumnWidth=".8" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue-H" LabelWidth="230" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:GridPanel ID="Grid_Show_TPRBP" runat="server" Title="净化明细" Margins="0 0 5 5" ColumnWidth="1" Cls="x-grid-custom" AutoScroll="true" RowLines="true" UI="Success">
                    <Store>
                        <ext:Store ID="Store" runat="server">
                            <Model>
                                <ext:Model ID="Model" runat="server" Name="PastSickness">
                                    <Fields>
                                        <ext:ModelField Name="dialysis_date" Type="String"/>
                                        <ext:ModelField Name="dialysis_time" Type="String" />
                                        <ext:ModelField Name="column_7" Type="String" />
                                        <ext:ModelField Name="column_6" Type="String" />
                                        <ext:ModelField Name="column_3" Type="String" />
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
                            <ext:Column ID="Column_7" runat="server" DataIndex="column_7" Text="电导" Width="80" />
                            <ext:Column ID="Column_6" runat="server" DataIndex="column_6" Text="温度" Width="80" />
                            <ext:Column ID="Column_3" runat="server" DataIndex="column_3" Text="超滤量" Width="80" />
                            <ext:Column ID="Column_10" runat="server" DataIndex="column_10" Text="跨膜压" Width="80" />
                            <ext:Column ID="Column_8" runat="server" DataIndex="column_8" Text="静脉压" Width="80" />
                            <ext:Column ID="Column_4" runat="server" DataIndex="column_4" Text="血流量" Width="80" />
                            <ext:Column ID="Column1" runat="server" DataIndex="cln2_t" Text="T ℃" Width="80" />
                            <ext:Column ID="Column2" runat="server" DataIndex="cln2_p" Text="P 次/分" Width="70" />
                            <ext:Column ID="Column6" runat="server" DataIndex="cln2_r" Text="R 次/分" Width="70" />
                            <ext:Column ID="Column10" runat="server" DataIndex="cln2_bp" Text="BP mmHg" Width="120" />
                            <ext:Column ID="Column11" runat="server" DataIndex="cln2_rmk" Text="病情及处理" Width="300" />
                            <ext:Column ID="Column4" runat="server" DataIndex="cln2_user" Text="记录人" Width="150" />
                            <ext:Column ID="Column5" runat="server" DataIndex="cln2_dateadded" Text="记录时间" Width="150" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">
                        </ext:RowSelectionModel>
                    </SelectionModel>
                </ext:GridPanel>
                <ext:Container ID="Container99" runat="server" Layout="HBoxLayout">
                    <Items> 
                        <ext:ImageButton ID="ImageButton2" runat="server" ImageUrl="Styles/home1.png" Height="50" Weight="300" Margins="0 0 0 0" OverImageUrl="Styles/homeover.png" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Btn_Home_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageButton3" runat="server" ImageUrl="Styles/detail01over.png" Height="50" Weight="300" Margins="0 0 0 0" Flex="1">
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
    </form>
</body>
</html>