<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rpt_View_Dialysis.aspx.cs" Inherits="Dialysis_Chart_Show.report.Rpt_View_Dialysis" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>统计分析 From Dialysis_10</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl=""
            Height="50px" ReportSourceID="CrSource" ToolbarImagesFolderUrl="" ToolPanelWidth="200px"
            Width="300px" HasCrystalLogo="False" ToolPanelView="None" />
        <CR:CrystalReportSource ID="CrSource" runat="server">
            <Report FileName="" />
        </CR:CrystalReportSource>
    </div>
    </form>
    
</body>
</html>

