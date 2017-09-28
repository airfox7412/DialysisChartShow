<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rpt_View_ipad1.aspx.cs" Inherits="Dialysis_Chart_Show.report.Rpt_View_ipad1" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<title><%=Label1.Text%></title>
<style type="text/css">
        input.btnHome
        {
            font-size: 32px;
            color: #3300FF;
            width: 450px;
            height: 45px;
            background-image: url('th.jpeg');
            background-repeat: no-repeat;
            text-align: center
        }
        input.btnBack
        {
            font-size: 32px;
            color: #3300FF;
            width: 450px;
            height: 45px;
        }
    </style>
    <script language="javascript">

        function goLite(FRM, BTN) {
            window.document.forms[FRM].elements[BTN].style.color = "#660000";
            window.document.forms[FRM].elements[BTN].style.backgroundColor = "#CCFF99";
        }

        function goDim(FRM, BTN) {
            window.document.forms[FRM].elements[BTN].style.color = "#3300FF";
            window.document.forms[FRM].elements[BTN].style.backgroundColor = "";
        }

    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
        <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl=""
            Height="50px" ReportSourceID="CrSource" ToolbarImagesFolderUrl="" ToolPanelWidth="200px"
            Width="300px" HasCrystalLogo="False" ToolPanelView="None" />
        <CR:CrystalReportSource ID="CrSource" runat="server">
            <Report FileName="">
            </Report>
        </CR:CrystalReportSource>
    </div>
    <input type="button" id="btn1" runat="server" name="btn1" class="btnHome" value="回首页" title=""
        onclick="window.location.href ='../ipad_Default.aspx'" 
        onmouseover="goLite(this.form.name,this.name)" onmouseout="goDim(this.form.name,this.name)">
    <input type="button" id="btn2" runat="server" name="btn2" class="btnBack" value="上一页" title="" onclick="javascript:window.history.go(-1);"
        onmouseover="goLite(this.form.name,this.name)" onmouseout="goDim(this.form.name,this.name)">
    </form>
</body>
</html>
