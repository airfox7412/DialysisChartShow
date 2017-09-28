<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_B11.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_B11" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script type="text/javascript">

        var saveData3 = function () {
            App.Hidden3.setValue(Ext.encode(App.GridPanel3.getRowsValues({ selectedOnly: false })));
        };

        var saveData4 = function () {
            App.Hidden4.setValue(Ext.encode(App.GridPanel4.getRowsValues({ selectedOnly: false })));
        };

        var saveData5 = function () {
            App.Hidden5.setValue(Ext.encode(App.GridPanel5.getRowsValues({ selectedOnly: false })));
        };

        var saveData6 = function () {
            App.Hidden6.setValue(Ext.encode(App.GridPanel6.getRowsValues({ selectedOnly: false })));
        };

        var saveData7 = function () {
            App.Hidden7.setValue(Ext.encode(App.GridPanel7.getRowsValues({ selectedOnly: false })));
        };

        var saveData8 = function () {
            App.Hidden8.setValue(Ext.encode(App.GridPanel8.getRowsValues({ selectedOnly: false })));
        };

        var saveData9 = function () {
            App.Hidden9.setValue(Ext.encode(App.GridPanel9.getRowsValues({ selectedOnly: false })));
        };

        var saveData10 = function () {
            App.Hidden10.setValue(Ext.encode(App.GridPanel10.getRowsValues({ selectedOnly: false })));
        };

        var saveData11 = function () {
            App.Hidden11.setValue(Ext.encode(App.GridPanel11.getRowsValues({ selectedOnly: false })));
        };

        var saveData12 = function () {
            App.Hidden12.setValue(Ext.encode(App.GridPanel12.getRowsValues({ selectedOnly: false })));
        };

        var saveData13 = function () {
            App.Hidden13.setValue(Ext.encode(App.GridPanel13.getRowsValues({ selectedOnly: false })));
        };

        var saveData14 = function () {
            App.Hidden14.setValue(Ext.encode(App.GridPanel14.getRowsValues({ selectedOnly: false })));
        };

        var saveData15 = function () {
            App.Hidden15.setValue(Ext.encode(App.GridPanel15.getRowsValues({ selectedOnly: false })));
        };

        var saveData16 = function () {
            App.Hidden16.setValue(Ext.encode(App.GridPanel16.getRowsValues({ selectedOnly: false })));
        };

        var saveData17 = function () {
            App.Hidden17.setValue(Ext.encode(App.GridPanel17.getRowsValues({ selectedOnly: false })));
        };

        var saveData18 = function () {
            App.Hidden18.setValue(Ext.encode(App.GridPanel18.getRowsValues({ selectedOnly: false })));
        };


        //20150627 ANDY
        var saveData13Q = function () {
            App.Hidden13Q.setValue(Ext.encode(App.GridPanel13Q.getRowsValues({ selectedOnly: false })));
        };
        //20150627 ANDY
        var saveData14Q = function () {
            App.Hidden14Q.setValue(Ext.encode(App.GridPanel14Q.getRowsValues({ selectedOnly: false })));
        };

        var saveChart = function (btn) {
            Ext.MessageBox.confirm('确认下载', '图表下载为图像 ?', function (choice) {
                if (choice == 'yes') {
                    btn.up('panel').down('chart').save({
                        type: 'image/png'
                    });
                }
            });
        };
  </script>

    <style type = "text/css">
    .myTreePanel 
    {
        border: none;
        padding: 0 10px;
    }
    
    .myTitle
    {
         font-weight:bold;  
         color:Black;
        }
        
    <%--panel head 自动--%>
    .x-panel-header-text {
        font-size: 16px;
        font-weight: bold;
        line-height: 20px;
    }
    <%--cell字型大小  自动 --%>
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
    <form id="form1" runat="server">
        <ext:TextField ID="txtNODE_ID" runat="server" />
        <ext:TextField ID="txtNODE_TEXT" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />     
        <ext:Viewport ID="Viewport1" runat="server">
            <Items>            
                <ext:Panel ID="Panel_2" runat="server" Border="false" Region="Center" ColumnWidth="1">
                    <Items>
                        <ext:TextField ID="txtDATE2" runat="server" FieldLabel="日期范围" LabelAlign="Right" PaddingSpec="4 0 4 0" Width="300" />
                        <ext:TextField ID="txtTOTAL2" runat="server" FieldLabel="血透人数" LabelAlign="Right" IndicatorText="人" PaddingSpec="4 0 4 0" />
                        <ext:TextField ID="txtDIE" runat="server" FieldLabel="死亡人数" LabelAlign="Right" IndicatorText="人" PaddingSpec="4 0 4 0" />
                        <ext:TextField ID="txtDIE_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="%" PaddingSpec="4 0 4 0" />
                        <ext:Hidden ID="Hidden3" runat="server" />
                        <ext:GridPanel ID="GridPanel3" runat="server" Title="死亡原因分析" Region="Center" Height="400" PaddingSpec="4 0 4 0" >
                            <Store>
                                <ext:Store ID="Store3" runat="server" >
                                    <Model>
                                        <ext:Model ID="Model3" runat="server" Name="pif_ic" >
                                            <Fields>
                                                <ext:ModelField Name="编号" Type="Int" />
                                                <ext:ModelField Name="姓名" />
                                                <ext:ModelField Name="身份证号" />
                                                <ext:ModelField Name="病历号" />
                                                <ext:ModelField Name="年龄" />
                                                <ext:ModelField Name="透析年限" />
                                                <ext:ModelField Name="死亡原因" />
                                                <ext:ModelField Name="死亡日期" />
                                                <ext:ModelField Name="生日" />
                                                <ext:ModelField Name="开始透析日期" />
                                                <ext:ModelField Name="性别" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel3" runat="server"  >
                                <Columns>
                                    <ext:Column ID="Column11" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column12" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column21" Header="性别" runat="server" DataIndex="性别" Width="50" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column13" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column14" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                    <ext:Column ID="Column19" Header="生日" runat="server" DataIndex="生日" Width="80" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column15" Header="年龄" runat="server" DataIndex="年龄" Width="40" Cls="x-grid-hd-inner"  />
                                    <ext:Column ID="Column20" Header="开始透析日期" runat="server" DataIndex="开始透析日期" Width="100" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column16" Header="透析年限Q" runat="server" DataIndex="透析年限" Width="90" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column18" Header="死亡日期QQ" runat="server" DataIndex="死亡日期" Width="90" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column17" Header="死亡原因QQQ" runat="server" DataIndex="死亡原因" Cls="x-grid-hd-inner" Flex="1" />
                                </Columns>
                            </ColumnModel>
                                         
                            <TopBar>
                                <ext:Toolbar ID="Toolbar3" runat="server">
                                    <Items>
                                        <ext:ToolbarFill ID="ToolbarFill3" runat="server" />
                                        <ext:Button ID="Button3a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_3" Icon="PageCode">
                                            <Listeners>
                                                <Click Fn="saveData3" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="Button3b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_3" Icon="PageExcel">
                                            <Listeners>
                                                <Click Fn="saveData3" />
                                            </Listeners>
                                        </ext:Button>
                                        <ext:Button ID="Button3c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_3" Icon="PageAttach">
                                            <Listeners>
                                                <Click Fn="saveData3" />
                                            </Listeners>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                        </ext:GridPanel> 
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport> 
    </form>
</body>
</html>
