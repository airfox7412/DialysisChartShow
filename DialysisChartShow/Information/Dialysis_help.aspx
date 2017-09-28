<%@ Page Language="C#" AutoEventWireup="True" CodeBehind="Dialysis_help.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_help" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8,Auto-Rotate=Disable" />
    <title>临床小帮手</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" Layout="ColumnLayout" Header="false" 
                    Title="临床小帮手" BodyStyle="background-color:#EBF5FF !important;" AutoScroll="true">
                    <Items>
                        <ext:HtmlEditor ID="helper1" runat="server" Width="1024" Height="900" ></ext:HtmlEditor>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
