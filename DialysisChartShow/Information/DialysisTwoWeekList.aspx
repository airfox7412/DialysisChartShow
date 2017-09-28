<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialysisTwoWeekList.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.DialysisTwoWeekList" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .x-grid-custom .x-grid-row-alt .x-grid-cell
        {
            background-color: #DAE2E8;
        }
    </style>
    <script>

        var template = 'color:{0};';

        var change = function (value, meta, record, row, col) {
            meta.style = Ext.String.format(template, "blue");
            if (record.get("RITEM_HIGH1") != "")
                if (parseFloat(value) > parseFloat(record.get("RITEM_HIGH1")))
                    meta.style = Ext.String.format(template, "red");
            if (record.get("RITEM_LOW1") != "")
                if (parseFloat(value) < parseFloat(record.get("RITEM_LOW1")))
                    meta.style = Ext.String.format(template, "green");
            return value + "  ";
        };

        var ldrug_actst = function (value) {
            return Ext.String.format(template, (value == "使用") ? "green" : "red", value);
        };

        var sdrug_actst = function (value) {
            return Ext.String.format(template, (value == "使用") ? "green" : "red", value);
        };

        var saveData1 = function () {
            App.Hidden1.setValue(Ext.encode(App.Grid_TwoWeek_List.getRowsValues({ selectedOnly: false })));
        };
        
    </script>
    <style type="text/css">
    <%--panel head 自动--%>
    .x-panel-header-text {
        font-size: 16px;
        font-weight: bold;
        line-height: 20px;
    }
    <%--cell字型大小  自动 ?--%>
    .x-grid-row .x-grid-cell { 
        font-size: 13px;
    }
    <%--grid column head  手动Cls="x-grid-hd-inner"--%>
    .x-grid-hd-inner {
        font-size: 12px;
        font-weight: bold;
    }
    <%--grid column 上色  手动tdCls="custom-column"--%>
    .x-grid-row .custom-column { 
        font-weight: bold;
    }
    
    <%--tree node text size 手动Cls="large-font"--%>
    .large-font 
    {
        font-size: 16px !important; 
        height: 22px !important;
    }
    .blue-large-font 
    {
        font-size: 16px !important; 
        height: 22px !important;
        color: blue !important;
    }
    
    .x-status-text 
    {
        font-size:18px !important;
        color: red !important;
    }

    </style>
</head>
<body>
    <form id="formTWL" width="500" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:GridPanel ID="Grid_TwoWeek_List" runat="server" Title="双周排班表 转EXCEL档" Height="550"
            Icon="ApplicationFormEdit" Cls="x-grid-custom" AutoScroll="True">
            <Store>
                <ext:Store ID="Store" runat="server">
                    <Model>
                        <ext:Model ID="Model" runat="server" Name="Checked_List">
                            <Fields>
                                <ext:ModelField Name="星期" Type="String" />
                                <ext:ModelField Name="时间" Type="String" />
                                <ext:ModelField Name="周一" Type="String" />
                                <ext:ModelField Name="周二" Type="String" />
                                <ext:ModelField Name="周三" Type="String" />
                                <ext:ModelField Name="周四" Type="String" />
                                <ext:ModelField Name="周五" Type="String" />
                                <ext:ModelField Name="周六" Type="String" />
                                <ext:ModelField Name="周日" Type="String" />
                                <ext:ModelField Name="下周一" Type="String" />
                                <ext:ModelField Name="下周二" Type="String" />
                                <ext:ModelField Name="下周三" Type="String" />
                                <ext:ModelField Name="下周四" Type="String" />
                                <ext:ModelField Name="下周五" Type="String" />
                                <ext:ModelField Name="下周六" Type="String" />
                                <ext:ModelField Name="下周日" Type="String" />
                            </Fields>
                        </ext:Model>
                    </Model>
                    <Reader>
                        <ext:ArrayReader />
                    </Reader>
                </ext:Store>
            </Store>
            <ColumnModel ID="ColumnModel1" runat="server">
                <Columns>
                    <ext:Column ID="Column01" runat="server" DataIndex="星期" Text="星期" Width="70" />
                    <ext:Column ID="Column02" runat="server" DataIndex="时间" Text="时间" Width="70" />
                    <ext:Column ID="Column03" runat="server" DataIndex="周一" Text="周一" Width="80" />
                    <ext:Column ID="Column04" runat="server" DataIndex="周二" Text="周二" Width="80" />
                    <ext:Column ID="Column05" runat="server" DataIndex="周三" Text="周三" Width="80" />
                    <ext:Column ID="Column06" runat="server" DataIndex="周四" Text="周四" Width="80" />
                    <ext:Column ID="Column07" runat="server" DataIndex="周五" Text="周五" Width="80" />
                    <ext:Column ID="Column08" runat="server" DataIndex="周六" Text="周六" Width="80" />
                    <ext:Column ID="Column09" runat="server" DataIndex="周日" Text="周日" Width="80" />
                    <ext:Column ID="Column10" runat="server" DataIndex="下周一" Text="周一" Width="80" />
                    <ext:Column ID="Column11" runat="server" DataIndex="下周二" Text="周二" Width="80" />
                    <ext:Column ID="Column12" runat="server" DataIndex="下周三" Text="周三" Width="80" />
                    <ext:Column ID="Column13" runat="server" DataIndex="下周四" Text="周四" Width="80" />
                    <ext:Column ID="Column14" runat="server" DataIndex="下周五" Text="周五" Width="80" />
                    <ext:Column ID="Column15" runat="server" DataIndex="下周六" Text="周六" Width="80" />
                    <ext:Column ID="Column16" runat="server" DataIndex="下周日" Text="周日" Width="80" />
                </Columns>
            </ColumnModel>
            <TopBar>
                <ext:Toolbar ID="Toolbar1" runat="server">
                    <Items>
                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                        <ext:Button ID="Button1a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_1"
                            Icon="PageCode">
                            <Listeners>
                                <Click Fn="saveData1" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="Button1b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_1"
                            Icon="PageExcel">
                            <Listeners>
                                <Click Fn="saveData1" />
                            </Listeners>
                        </ext:Button>
                        <ext:Button ID="Button1c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_1"
                            Icon="PageAttach">
                            <Listeners>
                                <Click Fn="saveData1" />
                            </Listeners>
                        </ext:Button>
                    </Items>
                </ext:Toolbar>
            </TopBar>
        </ext:GridPanel>
        <ext:Hidden ID="Hidden1" runat="server" />

    </div>
    </form>
</body>
</html>
