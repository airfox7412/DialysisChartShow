<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Char_Show.aspx.cs" Inherits="Dialysis_Chart_Show.Char_Show" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    
        <asp:Label ID="Label1" runat="server" Text="身分證"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server"></asp:TextBox>
        <asp:Label ID="Label2" runat="server" Text="日期"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server"></asp:TextBox>
        <br />
    
        <asp:Button ID="Button1" runat="server" onclick="Button1_Click" Text="Button"  />
        <asp:Button ID="Button2" runat="server" Text="Button" onclick="Button2_Click" />
    </div>
    </form>
</body>
</html>
