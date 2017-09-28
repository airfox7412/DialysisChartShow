<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="loginXian.aspx.cs" Inherits="Dialysis_Chart_Show.loginXian" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>登入窗口</title>
    <style type="text/css"> 
        .x-btn-inner-default-small
        {
            font-size:24px;
            line-height:30px;
        }
        
        .x-form-item-label-default
        {
            font-size:24px;
            line-height:30px;
        }
        
        .x-form-text-default
        {
            font-size:24px;
            line-height:30px;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server">
        <ext:Hidden ID="AUTH" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Window ID="Window1" runat="server" Closable="false" Resizable="false" Icon="Lock" Title="登入窗口" Draggable="false" Width="360" Height="220" Modal="true" BodyPadding="20" Layout="VBoxLayout">
            <Items>
                <ext:TextField ID="Text_User" runat="server" LabelWidth="120" FieldLabel="工号" Width="300" LabelAlign="Right" PaddingSpec="5 20 5 5" Text="" EnableKeyEvents="true">
                    <DirectEvents>
                        <KeyPress OnEvent="text_click">
                            <ExtraParams>
                                <ext:Parameter Name="keynum" Value="e.getKey()" Mode="Raw" />
                            </ExtraParams>
                        </KeyPress>
                    </DirectEvents>
                </ext:TextField>
                <ext:Button ID="BtnLogin" runat="server" Icon="Accept" Text="确认" Width="300" Height="50" Margin="10">
                    <DirectEvents>
                        <Click OnEvent="BtnLogin_Click" />
                    </DirectEvents>
                </ext:Button>
            </Items>
        </ext:Window>
    </form>
</body>
</html>
