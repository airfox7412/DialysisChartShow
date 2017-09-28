<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_checkin.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_checkin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
	<title><% =Text_Patient_Name.Text %></title>
    <link href="css/grid.css" rel="stylesheet"/>
    <style type="text/css">
    .Panellogo .x-autocontainer-innerCt
    {
        /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#1e5799+0,2989d8+100,207cca+100,7db9e8+100 */
        background: #1e5799; /* Old browsers */
        background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
        background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
        background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0 ); /* IE6-9 */ 
    }
        
        <%--ComboBox Items--%>
        .x-boundlist-item 
        { 
            font-size: 30px;
            line-height: 30px;
        }
        .x-border-box .x-form-text
        {
            height: 36px !important;
            font-size: x-large;
        }
        .my-Field 
        {
            font-size: 20px;
            height: 30px;
            color: White;
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
        
        .read_field .x-form-field
        {
            background-color: #EEEEEE;
        }
        .x-form-item-label-text
        {
            color:White;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="pid" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="pif_hpack" runat="server" />
        <ext:Hidden ID="DRUG_ROWID" runat="server" />
        <ext:Hidden ID="PAT_IC" runat="server" />
        <ext:Hidden ID="DRUG_CODE" runat="server" />
        <ext:Hidden ID="DialysisTimes" runat="server" />
        <ext:Hidden ID="txt_TargetWeight" runat="server" />
        <ext:Hidden ID="txt_orddate" runat="server" />
        <ext:Hidden ID="hpack" runat="server" />
        
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:FormPanel ID="FormPanel1" runat="server" Title="病患资料" Header="true" Icon="ApplicationEdit" ButtonAlign="Center" Padding="5" Cls="Panellogo">
            <Items>
                <ext:Container ID="Container5" runat="server" Frame="true" Layout="ColumnLayout" Region="Center" >
                    <Items>
                        <ext:Container ID="Container0" runat="server" Layout="FitLayout" ColumnWidth=".15">
                            <Items>
				                <ext:Image ID="Image1" runat="server" />
                                <ext:FileUploadField ID="UploadImage" runat="server" Icon="ImageAdd" ButtonOnly="true" ButtonText="选择" Width="80">
                                    <DirectEvents>
                                        <Change OnEvent="GetPatImg" IsUpload="true" />
                                    </DirectEvents>
                                </ext:FileUploadField>
				            </Items> 
				        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" ColumnWidth=".85">
                            <Items>
                                <ext:TextField ID="area" runat="server"                FieldLabel="床区" LabelWidth="120" Width="200" LabelAlign="Right" LabelCls="my-Field" Cls="read_field" ReadOnly="true" Margin="2" />
                                <ext:TextField ID="bedno" runat="server"               FieldLabel="床号" LabelWidth="100" Width="200" LabelAlign="Right" LabelCls="my-Field" Cls="read_field" ReadOnly="true" Margin="2" />
                                <ext:TextField ID="time" runat="server"                FieldLabel="时段" LabelWidth="100" Width="200" LabelAlign="Right" LabelCls="my-Field" Cls="read_field" ReadOnly="true" Margin="2" />
                                <ext:TextField ID="Text_Patient_Name" runat="server"   FieldLabel="姓名" LabelWidth="120" Width="300" LabelAlign="Right" LabelCls="my-Field" Cls="read_field" ReadOnly="true" Margin="2" />
                                <ext:TextField ID="Text_Patient_Gender" runat="server" FieldLabel="性别" LabelWidth="100" Width="200" LabelAlign="Right" LabelCls="my-Field" Cls="read_field" ReadOnly="true" Margin="2" />
                                <ext:TextField ID="Text_Patient_Age" runat="server"    FieldLabel="年龄" LabelWidth="100" Width="200" LabelAlign="Right" LabelCls="my-Field" Cls="read_field" ReadOnly="true" Margin="2" />
                                <ext:TextField ID="Text_Patient_ID" runat="server"     FieldLabel="身份证号" LabelWidth="120" Width="400" LabelAlign="Right" LabelCls="my-Field" Cls="read_field" ReadOnly="true" Margin="2" />                                                                            
                                <ext:SelectBox ID="cbo_h_type" runat="server"          FieldLabel="血管通路类型" LabelWidth="150" Width="400" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" Margin="2" />
                                <ext:SelectBox ID="cbo_machine_model" runat="server"   FieldLabel="透析器型号" LabelWidth="120" Width="400" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" Margin="2" />
                                <ext:SelectBox ID="cbo_machine_model2" runat="server"  FieldLabel="透析器型号二" LabelWidth="150" Width="400" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" Margin="2" />
                                <ext:SelectBox ID="cbo_hpack3" runat="server"          FieldLabel="管路型号" LabelWidth="120" Width="400" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" Margin="2" />
                                <ext:SelectBox ID="cbo_hpack5" runat="server"          FieldLabel="管路型号二" LabelWidth="150" Width="400" LabelAlign="Right" Cls="read_field" LabelCls="my-Field" Margin="2" />
                                <ext:SelectBox ID="cb_info_date" runat="server"        FieldLabel="透析日期" LabelWidth="120" Width="400" LabelAlign="Right" LabelCls="my-Field" Margin="2" />
                                <ext:SelectBox ID="cbo_diagnosis" runat="server"       FieldLabel="诊断" LabelWidth="150" Width="400" LabelAlign="Right" LabelCls="my-Field" Margin="2" />
                                <ext:SelectBox ID="cbo_Col24" runat="server"           FieldLabel="病情" Width="400" LabelWidth="120" LabelAlign="Right" LabelCls="my-Field" Margin="2">
                                    <Items>
                                        <ext:ListItem Text="一般" Value="一般" />
                                        <ext:ListItem Text="病危" Value="病危" />
                                        <ext:ListItem Text="病重" Value="病重" />
                                    </Items>
                                </ext:SelectBox>
                                <ext:SelectBox ID="cbo_Machinetype" runat="server"     LabelWidth="150" Width="400" FieldLabel="透析方式" LabelAlign="Right" LabelCls="my-Field" Margin="2">
                                    <DirectEvents>
                                        <Change OnEvent="OnChangeType" />
                                    </DirectEvents>
                                </ext:SelectBox>                                
                                <ext:TextField ID="TextTimes" runat="server"           LabelWidth="120" Width="400" FieldLabel="透析次数" LabelAlign="Right" LabelCls="my-Field" ReadOnly="true" Margin="2">
                                    <DirectEvents>
                                        <Tap OnEvent="TextTimes_Click" />
                                    </DirectEvents>
                                </ext:TextField>
                            </Items>
                        </ext:Container>
                    </Items>                            
                </ext:Container>
                 <ext:Container ID="Container4" runat="server" Layout="ColumnLayout">
                    <Items>
                        <ext:TextField ID="txt_weight_before" runat="server" FieldLabel="透析前体重" LabelWidth="120" Width="250" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" LabelCls="my-Field" EmptyText="必要栏位" IndicatorCls="my-Field" Margin="5" />
                        <ext:TextField ID="txt_weight_after" runat="server" FieldLabel="透析后体重" LabelWidth="120" Width="250" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" LabelCls="my-Field" IndicatorCls="my-Field" Margin="5" />                         
                        <ext:SelectBox ID="cb_WeightAfter" runat="server" FieldLabel="历次透析后" LabelWidth="175" Width="425" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                        <ext:TextField ID="txt_weight_after_expect" runat="server" FieldLabel="干体重" LabelWidth="120" Width="250" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" LabelCls="my-Field" IndicatorCls="my-Field" Margin="5" />
                        <ext:ComboBox ID="cb_TargetWeight" runat="server" FieldLabel="目标定容量" LabelWidth="120" Width="380" LabelAlign="Right" LabelCls="my-Field" Margin="5">
                            <DirectEvents>
                                <Change OnEvent="ChangeW" />
                            </DirectEvents>
                        </ext:ComboBox>
                    </Items>                            
                </ext:Container>
                 <ext:Container ID="Container44" runat="server" Layout="ColumnLayout">
                    <Items>
                        <ext:SelectBox ID="SelectDialysisTime" runat="server" FieldLabel="透析时间" LabelWidth="120" Width="300" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                        <ext:SelectBox ID="SelectBox10" runat="server"        FieldLabel="抗凝药物" LabelWidth="120" Width="300" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                        <ext:TextField ID="TextField8" runat="server"         FieldLabel="首次剂量" LabelWidth="120" Width="300" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                        <ext:TextField ID="TextAdd" runat="server"            FieldLabel="追加量"   LabelWidth="120" Width="300" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                        <ext:TextField ID="TextAmount" runat="server"         FieldLabel="总量"     LabelWidth="120" Width="300" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                        <ext:TextField ID="TextField11" runat="server"        FieldLabel="置换量"   LabelWidth="120" Width="300" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                    <Items>
                    	<ext:TextField ID="TextField6" runat="server"    FieldLabel="透析液: 钾" LabelWidth="120" Width="400" LabelAlign="Right" LabelCls="my-Field" IndicatorText="mmol/L" IndicatorCls="my-Field" Margin="5" />
                        <ext:TextField ID="TextField7" runat="server"  FieldLabel="钙"         LabelWidth="100" Width="400" LabelAlign="Right" LabelCls="my-Field" IndicatorText="mmol/L" IndicatorCls="my-Field" Margin="5" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container6" runat="server" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField9" runat="server"  FieldLabel="碳酸氢根"   LabelWidth="120" Width="400" LabelAlign="Right" LabelCls="my-Field" IndicatorText="mmol/L" IndicatorCls="my-Field" Margin="5" />
                        <ext:TextField ID="TextField10" runat="server" FieldLabel="钠"         LabelWidth="100" Width="400" LabelAlign="Right" LabelCls="my-Field" IndicatorText="mmol/L" IndicatorCls="my-Field" Margin="5" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField1" runat="server" FieldLabel="侧位" LabelWidth="120" Width="330" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                        <ext:TextField ID="TextField2" runat="server" FieldLabel="部位" LabelWidth="170" Width="400" LabelAlign="Right" LabelCls="my-Field" Margin="5" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container8" runat="server" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField4" runat="server" FieldLabel="血流速" LabelWidth="120" Width="400" LabelAlign="Right" LabelCls="my-Field" IndicatorText="ml/min" IndicatorCls="my-Field" Margin="5" />
                        <ext:TextField ID="TextField5" runat="server" FieldLabel="尿量" LabelWidth="100" Width="400" LabelAlign="Right" LabelCls="my-Field" IndicatorText="ml/24h" IndicatorCls="my-Field" Margin="5" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container14" runat="server" Layout="HBoxLayout">
                    <Items>
                        <ext:TextArea ID="TextArea1" runat="server" FieldLabel="透析前症状</br></br>及处理" LabelWidth="120" LabelAlign="Right" PaddingSpec="5 20 5 5" LabelCls="my-Field" ColumnWidth="1" Flex="1" />
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container15" runat="server" Layout="ColumnLayout" PaddingSpec="5">
                    <Items>                        
                        <ext:Button ID="Btnsave" runat="server" Text="血液净化存盘" Icon="Disk" UI="Success" Scale="Large" Margin="5" ColumnWidth=".25">
                            <DirectEvents>
                                <Click OnEvent="Btnsave_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="BtnMemoAdd" runat="server" Text="透析前医嘱" Icon="DateEdit" UI="Info" Scale="Large" Margin="5" ColumnWidth=".25">
                            <DirectEvents>
                                <Click OnEvent="BtnMemoAdd_Click" />
                            </DirectEvents>
                        </ext:Button>                                                                        
                        <ext:Button ID="Button7" runat="server" Text="重读" Icon="PlayGreen" UI="Info" Scale="Large" Margin="5" ColumnWidth=".25">
                            <ToolTips>
                                <ext:ToolTip ID="ToolTip1" runat="server" Html="重读治疗计画" />
                            </ToolTips>
                            <DirectEvents>
                                <Click OnEvent="BtnReloadData_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="BtnBlood" runat="server" Text="血压量测" Icon="HeartConnect" UI="Info" Scale="Large" Margin="5" ColumnWidth=".25">
                            <DirectEvents>
                                <Click OnEvent="BtnBloodAdd_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button8" runat="server" Text="回首页" Icon="House" UI="Info" Scale="Large" Margin="5" ColumnWidth=".25">
                            <DirectEvents>
                                <Click OnEvent="BtnBack_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items> 
                </ext:Container>
            </Items>
        </ext:FormPanel>        
        <ext:TabPanel ID="TabPanel1" runat="server" UI="Warning">
            <Items>
                <ext:Panel ID="Panel_Long" runat="server" Title="长期医嘱用药" Height="500" Cls="Panellogo">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                    </LayoutConfig>
                    <DirectEvents>
                        <Activate OnEvent="tab1_activate" Single="true" />
                    </DirectEvents>
                    <Loader ID="Loader_Long" runat="server" Mode="Frame">
                        <LoadMask ShowMask="true" Msg="读取中..." />
                    </Loader>
                </ext:Panel>
                <ext:Panel ID="Panel_Short" runat="server" Title="短期医嘱用药" Height="500">
                    <LayoutConfig>
                        <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                    </LayoutConfig>
                    <DirectEvents>
                        <Activate OnEvent="tab2_activate" Single="true" />
                    </DirectEvents>
                    <Loader ID="Loader_Short" runat="server" Mode="Frame">
                        <LoadMask ShowMask="true" Msg="读取中..." />
                    </Loader>
                </ext:Panel>
            </Items>
        </ext:TabPanel>
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
        <ext:Window ID="Window2" runat="server" Width="800" Height="200" BodyPadding="5" Modal="true" Hidden="true" UI="Info">
            <Items>
                <ext:TextArea ID="TextArea2" runat="server" FieldLabel="透析前医嘱" LabelWidth="200" ColumnWidth="1" LabelAlign="Right" Width="750" AutoHeight="true" Flex="1" />
            </Items>
            <Buttons>
                <ext:Button ID="BtnSaveW" runat="server" Icon="Accept" Text="确认" Width="150" Height="50">
                    <DirectEvents>
                        <Click OnEvent="BtnSaveW_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="BtnCancel2" runat="server" Icon="Cancel" Text="取消" Width="150" Height="50">
                    <DirectEvents>
                        <Click OnEvent="BtnCancel2_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Window ID="Window4" runat="server" Title="血压量测" Width="800" Height="150" BodyPadding="5" Modal="true" Hidden="true" UI="Info">
            <Items>
                <ext:Container ID="Container7" runat="server" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField37" runat="server" FieldLabel="透前血压" IndicatorText="mmhg" LabelWidth="100" ColumnWidth=".3" LabelAlign="Right" PaddingSpec="10 20 5 5" Flex="1" />
                        <ext:TextField ID="TextField38" runat="server" FieldLabel="透后血压" IndicatorText="mmhg" LabelWidth="100" ColumnWidth=".3" LabelAlign="Right" PaddingSpec="10 20 5 5" Flex="1" />
                        <ext:SelectBox ID="SelectBox39" runat="server" FieldLabel="血管通路情况" LabelWidth="100" ColumnWidth=".3" LabelAlign="Right" PaddingSpec="10 20 5 5" Flex="1" />
                    </Items>
                </ext:Container>
            </Items>
            <Buttons>
                <ext:Button ID="BtnSaveB" runat="server" Icon="Accept" Text="确认" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="BtnSaveB_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="BtnCancelB" runat="server" Icon="Cancel" Text="取消" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="BtnCancelB_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>
</body>
</html>