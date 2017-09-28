<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_01_HenanS.aspx.cs" Inherits="Dialysis_Chart_Show.Dialysis_09_01_HenanS" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8, Auto-Rotate=Disable" />
    <title><% =Label2.Text%></title>
    <style type="text/css">        
        
        .label .x-label-value
        {
            font-size: large;
            color: #178951;
        }
        
        .formlabel
        {
            font-size: 26px;
            color : Black;
        }
        
        .x-box-item
        {
            height: 26px !important;
        }
        .x-panel-header-text-default
        {
            font-size: large;
            line-height: 26px;
        }
        .x-tool img
        {
            height: 35px;
            width: 30px;
        }
        .x-form-item-label-left
        {
            font-size: large;
        }        
        .x-form-item-label-right
        {
            font-size: large;
        }
        .x-field-indicator
        {
            font-size: large;
        }
        .x-border-box .x-form-text
        {
            height: 26px !important;
            font-size: large;
        }
        .red
        {
            color: Red;
        }
        
        .x-btn .x-btn-center .x-btn-inner
        {
            font-size: large;
        }
        
        .x-border-box .x-form-trigger
        {
            height: 35px !important;
            width: 17px !important;
            cursor: pointer;
        }
        .x-boundlist-item
        {
            font-size: large;
        }
        
        .x-window-header-text-default
        {
            font-size: large;
        }
        .x-form-display-field
        {
            font-size: 20px;
        }
        .x-form-item
        {
            font: normal 25px tahoma,arial,verdana,sans-serif;
        }
        .x-form-checkbox, .x-form-radio
        {
            width: 25px;
            height: 35px;
            background-image: url("../Styles/che_btn.png");
        }
        .x-fieldset-header .x-fieldset-header-text 
        {
            font-size:large;
        }
        
        .read_field .x-form-field
        {
            font-size: large;
            height: 40px !important;
            color: blue;
            background-color: #EEEEEE;
        }
        
        #ImageBtn_back, #ImageBtn_save
        {
            height: 60px !important;
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
        <ext:Hidden ID="info_date1" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:FormPanel ID="FormPanel1" runat="server" Title="净化过程小结" BodyStyle="background-color:#EBF5FF !important;" AutoScroll="true">
            <Items>
                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="20" Hidden="true">
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
                        <ext:TextField ID="TextField16" runat="server" FieldLabel="血管通路类型" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" ColumnWidth=".4" Cls="read_field" ReadOnly="true" />
                        <ext:TextField ID="TextField2" runat="server" FieldLabel="时间" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" ColumnWidth=".4" Cls="read_field" ReadOnly="true" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container31" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                        <ext:TextField ID="TextField3" runat="server" FieldLabel="历时" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" ColumnWidth=".4" Cls="read_field" ReadOnly="true" />
                        <ext:TextField ID="TextField4" runat="server" FieldLabel="脱水" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" ColumnWidth=".4" Cls="read_field" IndicatorText="(kg)" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                        <ext:TextField ID="TextField5" runat="server" FieldLabel="高压波动范围" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" ColumnWidth=".26" Cls="read_field" />
                        <ext:TextField ID="TextField6" runat="server" FieldLabel="~" LabelCls="formlabel" LabelAlign="Right" LabelWidth="10" ColumnWidth=".14" Cls="read_field" />
                        <ext:TextField ID="TextField7" runat="server" FieldLabel="低压波动范" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" ColumnWidth=".26" Cls="read_field" />
                        <ext:TextField ID="TextField8" runat="server" FieldLabel="~" LabelCls="formlabel" LabelAlign="Right" LabelWidth="10" ColumnWidth=".14" Cls="read_field" />
                    </Items>
                </ext:Container>
                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                        <ext:SelectBox ID="cbo_pressure" runat="server" FieldLabel="血压" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" Cls="Text-blue" ColumnWidth=".4">
                            <Listeners>
                                <Change Handler="this.removeCls('blue'); this.addCls('height');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_symptom" runat="server" FieldLabel="透析症状" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" Cls="Text-blue"  ColumnWidth=".4">
                            <Listeners>
                                <Change Handler="this.removeCls('blue'); this.addCls('height');" Single="true" />
                            </Listeners>
                        </ext:SelectBox>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>
                        <ext:TextArea ID="TextArea2" runat="server" FieldLabel="医生纪录" LabelCls="formlabel" LabelAlign="Right" LabelWidth="150" Width="850" />
                    </Items>
                </ext:Container>                        
                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items>            
                        <ext:TextField ID="TextField9" runat="server" FieldLabel="医生" LabelCls="formlabel" ColumnWidth=".4" LabelAlign="Right" Cls="read_field" LabelWidth="150" ReadOnly="true" />
                        <ext:TextField ID="TextField10" runat="server" FieldLabel="用药护士" LabelCls="formlabel" ColumnWidth=".4" LabelAlign="Right" Cls="read_field" LabelWidth="150" ReadOnly="true" />
                    </Items>    
                </ext:Container>                        
                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="10">
                    <Items> 
                        <ext:ImageButton ID="ImageBtn_back" runat="server" ImageUrl="~/Styles/back2.png" OverImageUrl="~/Styles/back2over.png" ColumnWidth=".3" PaddingSpec="10 10 10 10">
                            <DirectEvents>
                                <Click OnEvent="Btn_back_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="ImageBtn_save" runat="server" ImageUrl="~/Styles/save.png" OverImageUrl="~/Styles/saveover.png" ColumnWidth=".3" PaddingSpec="10 10 10 10">
                            <DirectEvents>
                                <Click OnEvent="Btn_save_Click" />
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