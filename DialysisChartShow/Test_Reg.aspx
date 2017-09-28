<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Test_Reg.aspx.cs" Inherits="Dialysis_Chart_Show.Test_Reg" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>登入窗口</title>
</head>
<body >
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel_North" runat="server" Region="North">
                    <Items>
                        <ext:TextArea ID="txtReg" runat="server" Height="300" Width="1000" />
                    </Items>
                </ext:Panel>
            </Items>    
        </ext:Viewport>
    </form>
</body>
</html>
