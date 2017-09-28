<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="login117.aspx.cs" Inherits="Dialysis_Chart_Show.login117" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">        
        .label .x-label-value
        {
            width: 120px !important;
            height: 24px !important;
            font-size: 24px; 
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
            height: 24px !important;
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
        
        .Text-blue16
        {
            color: blue;
            font-size: 13px;
            height: 13px
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
        
        .red-text {
            color     : red;
            font-size : xx-large;
        }
        
        .x-btn .x-btn-center .x-btn-inner
        {
            font-size: 24px;
            color: Blue
        }
        
        .x-btn 
        {
            height: 50px !important;
            width: 300px !important; 
        }
    </style>
</head>
<body >
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
        <ext:Hidden ID="Tex_Machine_type" runat="server" />
        <ext:Hidden ID="hpack3" runat="server" />
        <ext:Hidden ID="txt_weight_before" runat="server" />
        <ext:Hidden ID="errcnt" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Window 
            ID="Window1" 
            runat="server" 
            Closable="false"
            Resizable="false"
            Icon="Lock" 
            Title="登入窗口"
            Draggable="false"
            Width="380"
            Height="230" 
            Modal="true"
            BodyPadding="20"
            Layout="VBoxLayout">
            <Items>
                <ext:TextField ID="Text_User" runat="server" LabelWidth="120" FieldLabel="医师代号" Width="300" LabelAlign="Right" Cls="read_field" PaddingSpec="5 20 5 5" LabelCls="my-Field" Text="" />
                <ext:TextField ID="Text_Pass" runat="server" LabelWidth="120" FieldLabel="医师密码" Width="300" LabelAlign="Right" Cls="read_field" PaddingSpec="5 20 5 5" LabelCls="my-Field" InputType="Password" Text="" />
                <ext:Button ID="BtnLogin" runat="server" Icon="Accept" Text="确认" Width="300" Height="50" Margin="10" Cls="my-Field" >
                    <DirectEvents>
                        <Click OnEvent="BtnLogin_Click" />
                    </DirectEvents>
                </ext:Button>
            </Items>
        </ext:Window>
    </div>
    </form>
</body>
</html>
