<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_PatientList.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_PatientList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title><% =Text_Patient_Name.Text %></title>
    <link href="css/grid.css" rel="stylesheet"/>
    <style type="text/css">
                
        <%--Grid Row--%>
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 20px;
            line-height: 24px; 
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
            font-size: 32px;
        }
        
        .my-TextField .x-form-field
        {
            font-size: 20px !important;
            height: 30px !important;
        }
        .my-TextField-red .x-form-field
        {
            font-size: 20px !important;
            height: 30px !important;
            color: Red;
        }
        .my-Field 
        {
            font-size: 20px;
            height: 30px;
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
        
        #ImageBtn_Home, #ImageBtn_Report, #ImageBtn_detail, #ImageBtn_detail01, #ImageBtn_detail02, #ImageBtn_Pharmacy
        {
            height: 50px !important;
        }
        #ImageBtn_TurnOff .x-btn
        {
            height: 270px !important;
            width: 270px !important;
        }
        #Btn_TurnOn .x-btn-inner
        {
            font-weight: bolder;
            font-size: 26px;
            color: Red;
            line-height:32px;
        }
    </style>

    <script type="text/javascript">
        function CloseTab() {
            top.opener = null;
            top.window.close();
        }

        var prepareCommand = function (grid, command, record, row) {
            if (record.get("lgord_nurs") != '') {
                command.hidden = true;
            }
        }
        var pinEditors = function (btn, pressed) {
            var columnConfig = btn.column,
                column = columnConfig.column;

            if (pressed) {
                column.pinOverComponent();
                column.showComponent(columnConfig.record, true);
            } else {
                column.unpinOverComponent();
                column.hideComponent(true);
            }
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="pid" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="pif_hpack" runat="server" />
        <ext:Hidden ID="DRUG_ROWID" runat="server" />
        <ext:Hidden ID="PAT_IC" runat="server" />
        <ext:Hidden ID="DRUG_CODE" runat="server" />
        <ext:Hidden ID="DialysisTimes" runat="server" />
        
        <ext:ResourceManager ID="ResourceManager1" runat="server" />

        <ext:FormPanel ID="FormPanel1" runat="server" Title="病患资料" Header="false" Icon="ApplicationEdit" ButtonAlign="Center" Padding="5" MonitorResize="true">
            <Items>
                <ext:Container ID="Container5" runat="server" Frame="true" Layout="ColumnLayout" Split="true" Region="Center" >
                    <Items>
                        <ext:Image ID="Image1" runat="server" ColumnWidth=".3" Height="286" Weight="100" />
                        <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" ColumnWidth=".6">
                            <Items>
                                <ext:FileUploadField ID="UploadImage" runat="server" Icon="ImageAdd" ButtonOnly="true" ButtonText="" ColumnWidth=".1">
                                    <DirectEvents>
                                        <Change OnEvent="GetPatImg" IsUpload="true" />
                                    </DirectEvents>
                                </ext:FileUploadField> 
                                <ext:TextField ID="Text_Patient_Name" runat="server"   LabelWidth="50" FieldLabel="姓名" LabelAlign="Right" Cls="read_field" ReadOnly="true" ColumnWidth=".45" Padding="2" LabelCls="my-Field" />
                                <ext:TextField ID="Text_Patient_Gender" runat="server" LabelWidth="60" FieldLabel="性别" LabelAlign="Right" Cls="read_field" ReadOnly="true" ColumnWidth=".23" Padding="2" LabelCls="my-Field" />
                                <ext:TextField ID="Text_Patient_Age" runat="server"    LabelWidth="60" FieldLabel="年龄" LabelAlign="Right" Cls="read_field" ReadOnly="true" ColumnWidth=".235" Padding="2" LabelCls="my-Field" />
                                <ext:TextField ID="Text_Patient_ID" runat="server"     LabelWidth="150" FieldLabel="身份证号" LabelAlign="Right" Cls="read_field" ReadOnly="true" ColumnWidth="1" Padding="2" LabelCls="my-Field" />                                        
                                <ext:Panel ID="Panel3" runat="server" Layout="HBoxLayout" Margin="10" />
                                <ext:SelectBox ID="cbo_h_type" runat="server"         LabelWidth="150" ColumnWidth="1" FieldLabel="血管通路类型" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" Padding="2" />
                                <ext:SelectBox ID="cbo_mechine_model" runat="server"  LabelWidth="150" ColumnWidth="1" FieldLabel="透析器型号" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" Padding="2" />
                                <ext:SelectBox ID="cbo_hpack3" runat="server"         LabelWidth="150" ColumnWidth="1" FieldLabel="管路型号" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" Padding="2" />                                      
                                <ext:TextField ID="Text_area" runat="server"           LabelWidth="60" FieldLabel="床区" LabelAlign="Right" Cls="read_field" ColumnWidth=".3" Padding="2" LabelCls="my-Field" ReadOnly="true" />
                                <ext:TextField ID="Text_Bed_NO" runat="server"         LabelWidth="60" FieldLabel="床号" LabelAlign="Right" Cls="read_field" ColumnWidth=".3" Padding="2" LabelCls="my-Field" ReadOnly="true" />
                                <ext:TextField ID="Text_Machine_type" runat="server"   LabelWidth="100" FieldLabel="透析方式" LabelAlign="Right" Cls="read_field" ColumnWidth=".4" Padding="2" LabelCls="my-Field" ReadOnly="true" />
                                <ext:TextField ID="Text_Time" runat="server"           LabelWidth="60" FieldLabel="时段" LabelAlign="Right" Cls="read_field" ColumnWidth=".3" Padding="2" LabelCls="my-Field" ReadOnly="true" />
                                <ext:TextField ID="Text_Patient_weight" runat="server" LabelWidth="60" FieldLabel="体重" LabelAlign="Right" Cls="my-TextField-red" ColumnWidth=".3" Padding="2" LabelCls="my-Field" IndicatorCls="my-Field" IndicatorText="kg" />
                                <ext:TextField ID="TextTimes" runat="server"          LabelWidth="100" FieldLabel="透析次数" LabelAlign="Right" ColumnWidth=".4" Padding="2" LabelCls="blue-Field" ReadOnly="true">
                                    <DirectEvents>
                                        <Tap OnEvent="TextTimes_Click" />
                                    </DirectEvents>
                                </ext:TextField>
                                <ext:Button ID="Btn_TurnOn" runat="server" Text="血透机开机确认" ColumnWidth="1" Height="50" UI="Info" >
                                    <DirectEvents>
                                        <Click OnEvent="Btn_TurnOn_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Btn_h08" runat="server" Text="评估表存盘" ColumnWidth=".2" Height="50" Hidden="true">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_h08_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Btn_htest" runat="server" Text="化验数据表" ColumnWidth=".2" Height="50" Hidden="true">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_htest_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container3" runat="server" ColumnWidth=".1">
                            <Items>
                                <ext:ImageButton ID="ImageBtn_TurnOff" runat="server" ImageUrl="Styles/dis_Btn.png" Height="90" Width="95" Disabled="true">
                                    <DirectEvents>
                                        <Click OnEvent="ImageBtn_TurnOff_click" />
                                    </DirectEvents>
                                </ext:ImageButton>
                            </Items>
                        </ext:Container>
                    </Items>                            
                </ext:Container>
                <ext:GridPanel ID="Grid_Long_Term" runat="server" Title="长期医嘱用药" Margins="0 0 5 5" Icon="ApplicationFormEdit" Collapsible="true" Height="270" cls="x-grid-custom">
                    <Store>
                        <ext:Store ID="Store" runat="server">
                            <Model>
                                <ext:Model ID="Model" runat="server" Name="longterm_ordermgt">
                                    <Fields>
                                        <ext:ModelField Name="lgord_id" Type="String" />
                                        <ext:ModelField Name="lgord_dateord" Type="String" />
                                        <ext:ModelField Name="lgord_timeord" Type="String" />
                                        <ext:ModelField Name="lgord_usr1" Type="String" />
                                        <ext:ModelField Name="drg_name" Type="String" />
                                        <ext:ModelField Name="lgord_intake" Type="String" />
                                        <ext:ModelField Name="lgord_freq"   Type="String" />
                                        <ext:ModelField Name="lgord_medway" Type="String" />
                                        <ext:ModelField Name="lgord_comment" Type="String" />
                                        <ext:ModelField Name="lgord_nurs" Type="String" />
                                        <ext:ModelField Name="lgord_dtactst" Type="String" />
                                        <ext:ModelField Name="lgord_patic" Type="String" />
                                        <ext:ModelField Name="lgord_drug" Type="String" />
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
                            <ext:Column ID="Column1" runat="server" DataIndex="lgord_dateord" Text="日期" Width="130" />
                            <ext:Column ID="Column2" runat="server" DataIndex="lgord_timeord" Text="记录时间" Width="90" />
                            <ext:Column ID="Column6" runat="server" DataIndex="lgord_usr1" Text="经治医生" Width="90" />
                            <ext:Column ID="Column10" runat="server" DataIndex="drg_name" Text="药品名称" Width="200" />
                            <ext:Column ID="Column16" runat="server" DataIndex="lgord_intake" Text="剂量" Width="100" />
                            <ext:Column ID="Column17" runat="server" DataIndex="lgord_freq" Text="频率" Width="80" />  
                            <ext:Column ID="Column20" runat="server" DataIndex="lgord_medway" Text="给药方式" Width="100" />
                            <ext:Column ID="Column11" runat="server" DataIndex="lgord_comment" Text="备注" Width="100" />
                            <ext:Column ID="Column15" runat="server" DataIndex="lgord_nurs" Header="护士" Width="100" />
                            <ext:Column ID="Column21" runat="server" DataIndex="lgord_dtactst" Header="用药时间" Width="100">
                                <Commands>
                                    <ext:ImageCommand CommandName="Edit" Icon="Pencil">
                                        <ToolTip Text="修改护士" />
                                    </ext:ImageCommand>
                                </Commands>
                                <DirectEvents>
                                    <Command OnEvent="Nurse_Click" >
                                        <ExtraParams>
                                            <ext:Parameter Name="drugkind" Value="'L'" Mode="Raw"/>
                                            <ext:Parameter Name="DRUG_ROWID" Value="record.data.lgord_id" Mode="Raw"/>
                                            <ext:Parameter Name="DRUG_TIME" Value="record.data.lgord_dtactst" Mode="Raw"/>
                                        </ExtraParams> 
                                    </Command> 
                                </DirectEvents>
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
                <ext:GridPanel ID="Grid_Short_Term" runat="server" Title="短期医嘱用药" Margins="0 0 5 5" Icon="ApplicationFormEdit" Collapsible="true" Height="250" cls="x-grid-custom">
                    <Store>
                        <ext:Store ID="Store1" runat="server">
                            <Model>
                                <ext:Model ID="Model1" runat="server" Name="shortterm_ordermgt">
                                    <Fields>
                                        <ext:ModelField Name="shord_id" Type="String" />
                                        <ext:ModelField Name="shord_dateord" Type="String" />
                                        <ext:ModelField Name="record_time" Type="String" />
                                        <ext:ModelField Name="record_person" Type="String" />
                                        <ext:ModelField Name="drug_name" Type="String" />
                                        <ext:ModelField Name="shord_intake" Type="String" />
                                        <ext:ModelField Name="shord_freq"   Type="String" />
                                        <ext:ModelField Name="shord_medway" Type="String" />
                                        <ext:ModelField Name="comment" Type="String" />
                                        <ext:ModelField Name="shord_nurs" Type="String" />
                                        <ext:ModelField Name="shord_dtactst" Type="String" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Reader>
                                <ext:ArrayReader />
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel2" runat="server">
                        <Columns>
                            <ext:Column ID="Column4" runat="server" DataIndex="shord_dateord" Text="日期" Width="130" />
                            <ext:Column ID="Column5" runat="server" DataIndex="record_time" Text="记录时间" Width="90" />
                            <ext:Column ID="Column7" runat="server" DataIndex="record_person" Text="经治医生" Width="90" />
                            <ext:Column ID="Column8" runat="server" DataIndex="drug_name" Text="药品名称" Width="200" />
                            <ext:Column ID="Column22" runat="server" DataIndex="shord_intake" Text="剂量" Width="80" />
                            <ext:Column ID="Column23" runat="server" DataIndex="shord_freq" Text="频率" Width="80" />
                            <ext:Column ID="Column24" runat="server" DataIndex="shord_medway" Text="给药方式" Width="100" />
                            <ext:Column ID="Column12" runat="server" DataIndex="comment" Text="备注" Width="100" />
                            <ext:Column ID="Column25" runat="server" DataIndex="shord_nurs" Header="护士" Width="100" />
                            <ext:Column ID="Column26" runat="server" DataIndex="shord_dtactst" Header="用药时间" Width="100">
                                <Commands>
                                    <ext:ImageCommand CommandName="Edit" Icon="Pencil" Style="margin-left:5px !important;">
                                        <ToolTip Text="修改护士" />
                                    </ext:ImageCommand>
                                </Commands>
                                <DirectEvents>
                                    <Command OnEvent="Nurse_Click" >
                                        <ExtraParams>
                                            <ext:Parameter Name="drugkind" Value="'S'" Mode="Raw"/>
                                            <ext:Parameter Name="DRUG_ROWID" Value="record.data.shord_id" Mode="Raw"/>
                                            <ext:Parameter Name="DRUG_TIME" Value="record.data.shord_dtactst" Mode="Raw"/>
                                        </ExtraParams> 
                                    </Command> 
                                </DirectEvents>
                            </ext:Column>
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" />
                    </SelectionModel>
                </ext:GridPanel>
                <ext:GridPanel ID="Grid_Other_hpack" runat="server" Title="耗材" Margins="0 0 5 5" Icon="ApplicationFormEdit" Collapsible="true" Height="250" cls="x-grid-custom">
                    <Store>
                        <ext:Store ID="Store2" runat="server">
                            <Model>
                                <ext:Model ID="Model2" runat="server" Name="PastSickness">
                                    <Fields>
                                        <ext:ModelField Name="item" Type="String" />
                                        <ext:ModelField Name="number" Type="String" />
                                    </Fields>
                                </ext:Model>
                            </Model>
                            <Reader>
                                <ext:ArrayReader />
                            </Reader>
                        </ext:Store>
                    </Store>
                    <ColumnModel ID="ColumnModel3" runat="server">
                        <Columns>
                            <ext:Column ID="Column13" runat="server" DataIndex="item" Text="项目" Width="300" />
                            <ext:Column ID="Column14" runat="server" DataIndex="number" Text="数量" Width="100" />
                            <ext:Column ID="Column27" runat="server" Flex="1" />
                        </Columns>
                    </ColumnModel>
                    <SelectionModel>
                        <ext:RowSelectionModel ID="RowSelectionModel3" runat="server" Mode="Single" />
                    </SelectionModel>
                </ext:GridPanel>
                <ext:Container ID="Container99" runat="server" Layout="HBoxLayout">
                    <Items> 
                        <ext:ImageButton ID="ImageBtn_Home" runat="server" ImageUrl="Styles/home1.png" Height="50"
                            Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/homeover1.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_Home_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageBtn_detail01" runat="server" ImageUrl="Styles/detail01.png" Height="50"
                            Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detail01over.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail01_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageBtn_detail" runat="server" ImageUrl="Styles/detail.png" Height="50"
                            Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detailover.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageBtn_detail02" runat="server" ImageUrl="Styles/detail02.png" Height="50"
                            Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/detail02over.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_detail02_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageBtn_Report" runat="server" ImageUrl="Styles/back.png" Height="50"
                            Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/backover.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_Report_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                            <ext:ImageButton ID="ImageBtn_Pharmacy" runat="server" ImageUrl="Styles/order.png" Height="50"
                            Weight="300" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/order_over.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_ordershortdrug_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items>
                </ext:Container>
            </Items>
        </ext:FormPanel>
        <ext:Window ID="WindowTime" runat="server" Title="请输入时间" Height="150" Closable="false"
                Width="350" BodyStyle="background-color: #fff;" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center">
                <Items>
                    <ext:Hidden ID="drugkind" runat="server" />
                    <ext:Hidden ID="drugid" runat="server" />
                    <ext:TextField ID="TextExeTime" runat="server" FieldLabel="时间" LabelWidth="100" ColumnWidth=".5" LabelAlign="Right" Padding="5" />
                </Items>
                <Buttons>
                    <ext:Button ID="Button5" runat="server" Icon="Accept" Text="确认" Width="150" Height="30">
                        <DirectEvents>
                            <Click OnEvent="btnTime_Click" />
                        </DirectEvents>
                    </ext:Button>
                    <ext:Button ID="Button6" runat="server" Icon="Cancel" Text="取消" Width="150" Height="30">
                        <DirectEvents>
                            <Click OnEvent="btnTimeClose_Click" />
                        </DirectEvents>
                    </ext:Button>
                </Buttons>
            </ext:Window>
        <%-- 及密码 --%>
        <ext:Window ID="w_LOGIN" runat="server" Title="请输入工号" Height="160" Closable="false"
            Width="350" BodyStyle="background-color: #fff;" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center">
            <Items>
                <ext:TextField ID="w_txtUSER" runat="server" FieldLabel="工号" ColumnWidth="1" LabelAlign="Right" Padding="5" />
            </Items>
            <Buttons>
                <ext:Button ID="w_btnOK" runat="server" Icon="Accept" Text="确认" Width="150" Height="40">
                    <DirectEvents>
                        <Click OnEvent="btnDecrypt_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="w_btnCANCEL" runat="server" Icon="Cancel" Text="取消" Width="150" Height="40">
                    <DirectEvents>
                        <Click OnEvent="btnClose_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <%-- 及密码 --%>
        <ext:Window ID="w_PHARLOGIN" runat="server" Title="请输入工号" Height="230" Closable="false"
            Width="350" BodyStyle="background-color: #fff;" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center">
            <Items>
                <ext:TextField ID="txt_stfcode" runat="server" FieldLabel="工号" ColumnWidth="1" LabelAlign="Right" Padding="5" />
                <ext:TextField ID="w_txtPASS" runat="server" FieldLabel="密码" ColumnWidth="1" LabelAlign="Right" Padding="5" InputType="Password" >
                    <Listeners>
                        <ValidityChange Handler="this.next().validate();" />
                        <Blur Handler="this.next().validate();" />
                    </Listeners>
                </ext:TextField>
            </Items>
            <Buttons>
                <ext:Button ID="Button1" runat="server" Icon="Accept" Text="确认" Width="150" Height="40">
                    <DirectEvents>
                        <Click OnEvent="btnpharlogin_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button2" runat="server" Icon="Cancel" Text="取消" Width="150" Height="40">
                    <DirectEvents>
                        <Click OnEvent="btnShClose_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Window ID="BaseTimes" runat="server" Title="请输入基數" Height="150" Closable="false"
            Width="350" BodyStyle="background-color: #fff;" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center">
            <Items>
                <ext:TextField ID="TextBaseTimes" runat="server" FieldLabel="透析次数" LabelWidth="150" ColumnWidth="1" LabelAlign="Right" Padding="5" />
            </Items>
            <Buttons>
                <ext:Button ID="Button3" runat="server" Icon="Accept" Text="确认" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="btnT_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button4" runat="server" Icon="Cancel" Text="取消" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="btnTClose_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>