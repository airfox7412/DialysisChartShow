<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Rpt_View_ipad.aspx.cs" Inherits="Dialysis_Chart_Show.report.Rpt_View_ipad" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title><%=Label1.Text%></title>
    <style type="text/css">
        .BtnAP
        {
            font-size: large;
            font-weight: bolder;
            color: #ec4d00;
        }
        .BtnDP
        {
            font-size: large;
            font-weight: bolder;
            color: #008080;
        }
        .BtnDC
        {
            font-size: large;
            font-weight: bolder;
            color: #155493;
        }
        .BtnVP
        {
            font-size: large;
            font-weight: bolder;
            color: #ff8040;
        }
        .BtnAll
        {
            font-size: large;
            font-weight: bolder;
        }
            
        .ValidatorCss
        {
            color:Red;
        }
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
        .style1
        {
            width: 298px;
        }
        .style2
        {
            width: 292px;
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
    <div style="width: 1055px">
        <asp:Label ID="Label1" runat="server" Visible="false"></asp:Label>
        <CR:CrystalReportViewer ID="CRViewer" runat="server" AutoDataBind="True" GroupTreeImagesFolderUrl=""
            Height="50px" ReportSourceID="CrSource" ToolbarImagesFolderUrl="" ToolPanelWidth="200px"
            Width="300px" HasCrystalLogo="False" ToolPanelView="None" />
        <CR:CrystalReportSource ID="CrSource" runat="server">
            <Report FileName="">
            </Report>
        </CR:CrystalReportSource>
        <asp:Label ID="Label2" runat="server" Text="开始日期:" Font-Size = "Larger" Font-Bold = "true"></asp:Label>
        <asp:TextBox ID="TextBox1" runat="server" Font-Size = "Larger" Width="150px"></asp:TextBox>
        <asp:Label ID="Label4" runat="server" Text="必填" Font-Size = "Larger" Font-Bold = "true" CssClass = "ValidatorCss"></asp:Label>
        <asp:CustomValidator ID="CustomValidator1" runat="server" 
            ControlToValidate="TextBox1" ErrorMessage="CustomValidator" 
             onservervalidate="CustomValidator1_ServerValidate" ValidateEmptyText="True" CssClass = "ValidatorCss"></asp:CustomValidator>
        <asp:Label ID="Label3" runat="server" Text="结束日期:" Font-Size = "Larger" Font-Bold = "true"></asp:Label>
        <asp:TextBox ID="TextBox2" runat="server" Font-Size = "Larger" Width="150px"></asp:TextBox>
        <asp:Label ID="Label5" runat="server" Text="必填" Font-Size = "Larger" Font-Bold = "true" CssClass = "ValidatorCss"></asp:Label>
        <asp:CustomValidator ID="CustomValidator2" runat="server" 
            ControlToValidate="TextBox2" ErrorMessage="CustomValidator" 
             onservervalidate="CustomValidator2_ServerValidate" ValidateEmptyText="True" CssClass = "ValidatorCss"></asp:CustomValidator>

        <br />
        <table border="0" style="width: 895px">
            <tr>
                <td class="style2">
                    <asp:Button ID="Button1" runat="server" Text="- Arterial pressure" CssClass="BtnAP"
                        Width="300px" OnClick="Button1_Click" Height="40px" />
                </td>
                <td class="style1">
                    <asp:Button ID="Button2" runat="server" Text="- Dialysate pressure" CssClass="BtnDP"
                        Width="300px" OnClick="Button2_Click" Height="40px" />
                </td>
                <td  rowspan = "2">
        <asp:Button ID="Button5" runat="server" Text="- ALL" CssClass = "BtnAll"
            Width="300px" Height="80px" onclick="Button5_Click" />
                </td>
            </tr>
            <tr>
                <td class="style2">
                    <asp:Button ID="Button3" runat="server" Text="- Dialysate conductivity" CssClass="BtnDC"
                        Width="300px" OnClick="Button3_Click" Height="40px" />
                </td>
                <td class="style1">
                    <asp:Button ID="Button4" runat="server" Text="- Venous pressure" CssClass="BtnVP"
                        Width="300px" OnClick="Button4_Click" Height="40px" />
                </td>
            </tr>
        </table>
        <br />
    </div>
    </form>
    <form name="groovyform">
    <input type = "text" 
    <input type="button" name="btn1" class="btnHome" value="回首页" title=""
        onclick="window.location.href ='../ipad_Default.aspx'"
        onmouseover="goLite(this.form.name,this.name)" onmouseout="goDim(this.form.name,this.name)">
    <input type="button" name="btn2" class="btnBack" value="上一页" title="" onclick="javascript:window.history.go(-1);"
        <%--onclick="window.location.href ='../ipad_PatientList.aspx'"--%>
        onmouseover="goLite(this.form.name,this.name)" onmouseout="goDim(this.form.name,this.name)">
    </form>
</body>
</html>
