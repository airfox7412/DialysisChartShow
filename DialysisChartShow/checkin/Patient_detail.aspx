<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient_detail.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Patient_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病患报到明细</title>
    <link href="../css/grid.css" rel="stylesheet"/>
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
    
    .formLabel
    {
        color: White;
        font-size:14px;
        font-weight:normal;
    }
    
    .formLabelYellow
    {
        color: Yellow;
        font-size:14px;
        font-weight:normal;
    }
    
    .x-form-text-default
    {
        font-size:14px;
        font-weight:normal;
    }
        
    .Text-blue .x-form-field
    {
        color: Blue;
        font-size:14px;
        font-weight:normal;
    }
    .text-readonly .x-form-text-default
    {
        color:Blue;
        font-size:14px;
        font-weight:normal;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
        <ext:Hidden ID="txt_TargetWeight" runat="server" />
        
        <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN" Theme="CrispTouch"/>
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel_North" runat="server" Title="病患报到" Region="North" Header="false" Cls="Panellogo" AutoScroll="true">
                    <Items>
                        <ext:Container ID="Container13" runat="server">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:TabPanel ID="TabPanel1" runat="server" Width="1200" AutoScroll="true" UI="Success">
                                    <Items>
                                        <%--治疗参数--%>
                                        <ext:Panel ID="Panel1" runat="server" Title="治疗参数" Icon="ApplicationViewList">
                                            <Items>
                                                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Width="1200" Frame="true" UI="Default"> 
                                                    <Items>
                                                        <ext:Container ID="Container01" runat="server" Frame="true" Layout="ColumnLayout" Split="true">
                                                            <Items>
                                                                <ext:Container ID="Container5" runat="server" ColumnWidth=".1">
                                                                    <Items>
                                                                        <ext:Image ID="Image1" runat="server" Height="100" Weight="50" /><%--病患照片--%>
                                                                        <ext:ImageButton ID="ImageBtn_TurnOff" runat="server" ImageUrl="../Styles/dis_Btn40.png" Width="50" Height="40" PaddingSpec="0 5 0 5">
                                                                            <ToolTips>
                                                                                <ext:ToolTip ID="ToolTip1" runat="server" Title="提示说明" Html="<font color='yellow'>开机,关机</font>" Width="80" UI="Success" />
                                                                            </ToolTips>
                                                                            <DirectEvents>
                                                                                <Click OnEvent="ImageBtn_TurnOff_click" />
                                                                            </DirectEvents>
                                                                        </ext:ImageButton>
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container2" runat="server" ColumnWidth=".9">
                                                                    <Items>
                                                                        <ext:Container ID="Container20" runat="server" Layout="ColumnLayout" UI="Default" Padding="10">
                                                                            <Items>
                                                                                <ext:TextField ID="area" runat="server" FieldLabel="床区" LabelWidth="50" Width="100" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Cls="text-readonly" ReadOnly="true" />
                                                                                <ext:TextField ID="bedno" runat="server" FieldLabel="床号" LabelWidth="50"  Width="100" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Cls="text-readonly" ReadOnly="true" />                                
                                                                                <ext:TextField ID="time" runat="server" FieldLabel="时段" LabelWidth="50" Width="100" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Cls="text-readonly" ReadOnly="true" />                                                                            
                                                                                <ext:TextField ID="Tex_Patient_Name" runat="server" FieldLabel="姓名" LabelWidth="50" Width="150" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Cls="text-readonly" ReadOnly="true" />
                                                                                <ext:TextField ID="Tex_Patient_ID" runat="server" FieldLabel="身份证号" LabelWidth="70" Width="250" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Cls="text-readonly" ReadOnly="true" />
                                                                                <ext:TextField ID="Tex_Patient_Gender" runat="server" FieldLabel="性别" LabelWidth="50" Width="100" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Cls="text-readonly" ReadOnly="true" />
                                                                                <ext:TextField ID="Tex_Patient_Age" runat="server" FieldLabel="年龄" LabelWidth="50" Width="100" LabelAlign="Right" PaddingSpec="5 30 5 5" LabelCls="formLabel" Cls="text-readonly" ReadOnly="true" />
                                                                            </Items>
                                                                        </ext:Container>
                                                                        <ext:Container ID="Container18" runat="server" Layout="HBoxLayout" Padding="10">
                                                                            <Items>
                                                                                <ext:SelectBox ID="cbo_Machinetype" runat="server" FieldLabel="透析方式" LabelWidth="60" Width="200" LabelAlign="Right" PaddingSpec="5 30 5 5" LabelCls="formLabel">
                                                                                    <DirectEvents>
                                                                                        <Change OnEvent="OnChangeType" />
                                                                                    </DirectEvents>
                                                                                </ext:SelectBox>
                                                                                <ext:SelectBox ID="cbo_h_type" runat="server" FieldLabel="血管通路类型" LabelWidth="90" Width="250" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelClsExtra="formLabel" />
                                                                                <ext:SelectBox ID="cbo_machine_model" runat="server" FieldLabel="透析器型号" LabelWidth="110" Width="260" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelClsExtra="formLabel" />
                                                                                <ext:SelectBox ID="cbo_machine_model2" runat="server" FieldLabel="透析器型号二" LabelWidth="110" Width="260" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelClsExtra="formLabel" ReadOnly="true" />
                                                                            </Items>
                                                                        </ext:Container>
                                                                        <ext:Container ID="Container17" runat="server" Layout="HBoxLayout">
                                                                            <Items>
                                                                                <ext:SelectBox ID="cb_info_date" runat="server" FieldLabel="透析日期" LabelWidth="70" Width="210" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" />
                                                                                <ext:SelectBox ID="cbo_diagnosis" runat="server" FieldLabel="诊断" LabelWidth="115" Width="275" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel">
                                                                                    <Listeners>
                                                                                        <Select Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                                                                                    </Listeners>
                                                                                </ext:SelectBox>
                                                                                <ext:SelectBox ID="cbo_hpack3" runat="server" FieldLabel="管路型号" LabelWidth="110" Width="260" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelClsExtra="formLabel" />
                                                                                <ext:SelectBox ID="cbo_hpack5" runat="server" FieldLabel="管路型号二" LabelWidth="110" Width="260" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelClsExtra="formLabel" ReadOnly="false" />   
                                                                            </Items>
                                                                        </ext:Container>
                                                                    </Items>                            
                                                                </ext:Container>
                                                            </Items>
                                                        </ext:Container>
                                                        <ext:Container ID="Container4" runat="server" Layout="HBoxLayout" Padding="5">
                                                            <Items>                                                                             
                                                                <ext:TextField ID="TextTimes" runat="server" FieldLabel="透析次数" LabelWidth="80" Width="200" LabelAlign="Right" PaddingSpec="5 5 10 5" LabelCls="formLabel" ReadOnly="true" IndicatorTip="设定透析基数">
                                                                    <DirectEvents>
                                                                        <Tap OnEvent="TextTimes_Click" />
                                                                    </DirectEvents>
                                                                </ext:TextField>
                                                                <ext:SelectBox ID="cb_WeightAfter" runat="server" FieldLabel="历次透析后" LabelWidth="80" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Flex="1" />
                                                                <ext:TextField ID="txt_weight_before" runat="server" FieldLabel="透析前体重" LabelWidth="100" ColumnWidth=".25" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabelYellow" IndicatorCls="formLabel" EmptyText="必要栏位" Flex="1"> 
                                                                    <DirectEvents>
                                                                        <Change OnEvent="text_deactivate" />
                                                                    </DirectEvents>
                                                                    <Listeners>
                                                                        <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" />
                                                                    </Listeners>
                                                                </ext:TextField> 
                                                                <ext:TextField ID="txt_weight_after" runat="server" FieldLabel="透析后体重" LabelWidth="100" ColumnWidth=".25" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" IndicatorCls="formLabel" Flex="1">                             
                                                                    <Listeners>
                                                                        <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" />
                                                                    </Listeners>
                                                                </ext:TextField> 
                                                                <ext:TextField ID="txt_weight_after_expect" runat="server" FieldLabel="干体重" LabelWidth="100" ColumnWidth=".25" MaskRe="[0-9\.]" IndicatorText="kg" LabelAlign="Right" PaddingSpec="5 20 5 5" LabelCls="formLabel" IndicatorCls="formLabel" Flex="1">
                                                                    <DirectEvents>
                                                                        <Change OnEvent="text_deactivate" />
                                                                    </DirectEvents>
                                                                    <Listeners>
                                                                        <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Items> 
                                                        </ext:Container>
                                                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" Padding="5">
                                                            <Items>  
                                                                <ext:SelectBox ID="SelectBox10" runat="server" FieldLabel="抗凝药物" LabelWidth="80" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Flex="1" />
                                                                <ext:ComboBox ID="cb_TargetWeight" runat="server" FieldLabel="目标定容量" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Flex="1">
                                                                    <DirectEvents>
                                                                        <Change OnEvent="ChangeW" />
                                                                    </DirectEvents>
                                                                </ext:ComboBox>
                                                                <ext:TextField ID="TextField8" runat="server" FieldLabel="首次剂量" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Flex="1" />
                                                                <ext:SelectBox ID="SelectDialysisTime" runat="server" FieldLabel="透析时间" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 20 5 5" LabelCls="formLabel" Flex="1" /> 
                                                            </Items> 
                                                        </ext:Container>
                                                        <ext:Container ID="Container61" runat="server" Layout="HBoxLayout">
                                                            <Items>
                                                                <ext:TextField ID="TextAdd" runat="server" FieldLabel="追加量" LabelWidth="85" Width="290" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" />
                                                                <ext:TextField ID="TextAmount" runat="server" FieldLabel="总量" LabelWidth="97" Width="280" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" />
                                                                <ext:TextField ID="TextField11" runat="server" FieldLabel="置换量" LabelWidth="102" Width="285" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" />
                                                                <ext:ComboBox ID="cbo_Col24" runat="server" FieldLabel="病情" LabelWidth="100" Width="280" LabelAlign="Right" PaddingSpec="5 50 5 5" LabelCls="formLabel">
                                                                    <Items>
                                                                        <ext:ListItem Text="一般" Value="一般" />
                                                                        <ext:ListItem Text="病危" Value="病危" />
                                                                        <ext:ListItem Text="病重" Value="病重" />
                                                                    </Items>
                                                                </ext:ComboBox>
                                                            </Items> 
                                                        </ext:Container>
                                                        <ext:Container ID="Container62" runat="server" Layout="HBoxLayout">
                                                            <Items>
                                                                <ext:TextField ID="TextField6" runat="server" FieldLabel="透析液: 钾" LabelWidth="85" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" IndicatorText="mmol/L" IndicatorCls="formLabel" Cls="text-readonly" Flex="1" />
                                                                <ext:TextField ID="TextField7" runat="server" FieldLabel="钙" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" IndicatorText="mmol/L" IndicatorCls="formLabel" Cls="text-readonly" Flex="1" />                                                            
                                                                <ext:TextField ID="TextField9" runat="server" FieldLabel="碳酸氢根" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" IndicatorText="mmol/L" IndicatorCls="formLabel" Cls="text-readonly" Flex="1" />
                                                                <ext:TextField ID="TextField10" runat="server" FieldLabel="钠" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 20 5 5" LabelCls="formLabel" IndicatorText="mmol/L" IndicatorCls="formLabel" Cls="text-readonly" Flex="1" />
                                                            </Items> 
                                                        </ext:Container>
                                                        <ext:Container ID="Container11" runat="server" Layout="HBoxLayout">
                                                            <Items>
                                                                <ext:TextField ID="TextField1" runat="server" FieldLabel="侧位" LabelWidth="85" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Flex="1">                            
                                                                    <Listeners>
                                                                        <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                                <ext:TextField ID="TextField2" runat="server" FieldLabel="部位" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" Flex="1">
                                                                    <Listeners>
                                                                        <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" Single="true" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                                <ext:TextField ID="TextField4" runat="server" FieldLabel="血流速" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 10 5 5" LabelCls="formLabel" IndicatorText="ml/min" IndicatorCls="formLabel" Flex="1">
                                                                    <Listeners>
                                                                        <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" Single="true" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                                <ext:TextField ID="TextField5" runat="server" FieldLabel="尿量" LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 20 5 5" LabelCls="formLabel" IndicatorText="ml/24h" IndicatorCls="formLabel" Flex="1">
                                                                    <Listeners>
                                                                        <Change Handler="this.removeCls('Text-red'); this.addCls('Text-black');" Single="true" />
                                                                    </Listeners>
                                                                </ext:TextField>
                                                            </Items> 
                                                        </ext:Container>
                                                        <ext:Container ID="Container14" runat="server" Layout="HBoxLayout">
                                                            <Items>
                                                                <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                                                                    <Items>
                                                                        <ext:TextArea ID="TextArea1" runat="server" FieldLabel="透析前症状</br>及处理" LabelWidth="85" Width="875" LabelAlign="Right" PaddingSpec="5 80 5 5" LabelCls="formLabel">
                                                                            <Listeners>
                                                                                <Change Handler="this.removeCls('Text-blue-H'); this.addCls('Text-black-H');" Single="true" />
                                                                            </Listeners>
                                                                        </ext:TextArea>
                                                                    </Items> 
                                                                </ext:Container>
                                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Width="210">
                                                                    <Items>
                                                                        <ext:Button ID="BtnMemoAdd" runat="server" Text="透析前医嘱" Icon="DateEdit" Width="130" UI="Info">
                                                                            <DirectEvents>
                                                                                <Click OnEvent="BtnMemoAdd_Click" />
                                                                            </DirectEvents>
                                                                        </ext:Button>                                                                        
                                                                        <ext:Button ID="Button1" runat="server" Text="重读" Icon="PlayGreen" Width="80" UI="Info">
                                                                            <ToolTips>
                                                                                <ext:ToolTip runat="server" Html="重读治疗计画" />
                                                                            </ToolTips>
                                                                            <DirectEvents>
                                                                                <Click OnEvent="BtnReloadData_Click" />
                                                                            </DirectEvents>
                                                                        </ext:Button>
                                                                        <ext:Button ID="BtnBlood" runat="server" Text="血压量测" Icon="HeartConnect" Width="130" UI="Info">
                                                                            <DirectEvents>
                                                                                <Click OnEvent="BtnBloodAdd_Click" />
                                                                            </DirectEvents>
                                                                        </ext:Button>
                                                                    </Items> 
                                                                </ext:Container>
                                                            </Items> 
                                                        </ext:Container>
                                                        <ext:Container ID="Container9" runat="server" Layout="HBoxLayout">
                                                            <Items>
                                                                <ext:TextArea ID="TextArea3" runat="server" FieldLabel="透析小结" LabelWidth="85" Width="875" LabelAlign="Right" PaddingSpec="5 80 5 5" LabelCls="formLabel">
                                                                    <Listeners>
                                                                        <Change Handler="this.removeCls('Text-blue-H'); this.addCls('Text-black-H');" Single="true" />
                                                                    </Listeners>
                                                                </ext:TextArea>
                                                            </Items> 
                                                        </ext:Container>
                                                        <ext:Container ID="Container15" runat="server" PaddingSpec="5 30 10 5">
                                                            <LayoutConfig>
                                                                <ext:CenterLayoutConfig></ext:CenterLayoutConfig>
                                                            </LayoutConfig>
                                                            <Items>                        
                                                                <ext:Button ID="Btnsave" runat="server" Text="血液净化存盘" Icon="Disk" UI="Success">
                                                                    <DirectEvents>
                                                                        <Click OnEvent="Btnsave_Click" />
                                                                    </DirectEvents>
                                                                </ext:Button>
                                                            </Items> 
                                                        </ext:Container>
                                                    </Items>
                                                </ext:FormPanel>
                                            </Items>
                                        </ext:Panel> 
                                        <%--长期医嘱用药--%>                            
                                        <ext:Panel ID="Panel_Long" runat="server" Title="长期医嘱用药" Height="660">
                                            <LayoutConfig>
                                                <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                                            </LayoutConfig>
                                            <DirectEvents>
                                                <Activate OnEvent="tab2_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader_Long" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中..." />
                                            </Loader>
                                        </ext:Panel> 
                                        <%--短期医嘱用药--%>                                  
                                        <ext:Panel ID="Panel_Short" runat="server" Title="短期医嘱用药" Height="660">
                                            <LayoutConfig>
                                                <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                                            </LayoutConfig>
                                            <DirectEvents>
                                                <Activate OnEvent="tab3_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader_Short" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中..." />
                                            </Loader>
                                        </ext:Panel>
                                        <%--异常生化指标--%>    
                                        <ext:Panel ID="Panel4" runat="server" Title="异常生化指标" Height="660">
                                            <DirectEvents>
                                                <Activate OnEvent="tab4_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader4" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                        <%--临床小帮手--%>    
                                        <ext:Panel ID="Panel5" runat="server" Title="临床小帮手"   Height="660">
                                            <DirectEvents>
                                                <Activate OnEvent="tab5_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader5" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                        <%--血管通路--%>    
                                        <ext:Panel ID="Panel6" runat="server" Title="血管通路" Height="660">
                                            <DirectEvents>
                                                <Activate OnEvent="tab6_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader6" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                        <%--当月计价--%>    
                                        <ext:Panel ID="Panel7" runat="server" Title="当月计价" Height="660">
                                            <DirectEvents>
                                                <Activate OnEvent="tab7_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader7" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                        <%--病患基本讯息--%>    
                                        <ext:Panel ID="Panel8" runat="server" Title="病患基本讯息" Height="660">
                                            <DirectEvents>
                                                <Activate OnEvent="tab8_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader8" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                        <%--打印记录表单--%>    
                                        <ext:Panel ID="Panel9" runat="server" Title="打印记录表单" Height="660">
                                            <DirectEvents>
                                                <Activate OnEvent="tab9_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader9" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                        <%--排班查找--%>    
                                        <ext:Panel ID="Panel10" runat="server" Title="排班查找" Height="660">
                                            <DirectEvents>
                                                <Activate OnEvent="tab10_activate" Single="true" />
                                            </DirectEvents>
                                            <Loader ID="Loader1" runat="server" Mode="Frame">
                                                <LoadMask ShowMask="true" Msg="读取中" />
                                            </Loader>
                                        </ext:Panel>
                                    </Items>
                                </ext:TabPanel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <ext:Window ID="Window2" runat="server" Width="800" Height="200" BodyPadding="5" Modal="true" Hidden="true" UI="Info">
            <Items>
                <ext:TextArea ID="TextArea2" runat="server" FieldLabel="透析前医嘱" LabelWidth="130" ColumnWidth="1" LabelAlign="Right" Width="750" AutoHeight="true" Flex="1" />
            </Items>
            <Buttons>
                <ext:Button ID="BtnSaveW" runat="server" Icon="Accept" Text="确认" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="BtnSaveW_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="BtnCancel2" runat="server" Icon="Cancel" Text="取消" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="BtnCancel2_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Window ID="Window3" runat="server" Title="请输入基數" Height="150" Width="350" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center" UI="Danger">
            <Items>
                <ext:TextField ID="TextBaseTimes" runat="server" FieldLabel="透析次数" LabelWidth="130" ColumnWidth="1" LabelAlign="Right" />
            </Items>
            <Buttons>
                <ext:Button ID="btnT" runat="server" Icon="Accept" Text="确认" Width="150" Height="30" UI="Success">
                    <DirectEvents>
                        <Click OnEvent="btnT_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="btnTClose" runat="server" Icon="Cancel" Text="取消" Width="150" Height="30" UI="Warning">
                    <DirectEvents>
                        <Click OnEvent="btnTClose_Click" />
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
        <ext:Window ID="PrintWindow" runat="server" Title="" Width="900" Height="700" Modal="true" AutoRender="false" Hidden="true">
            <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
    </form>
</body>
</html>