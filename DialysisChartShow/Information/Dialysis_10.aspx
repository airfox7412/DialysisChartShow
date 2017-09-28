<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10" %>

<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304"
    Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>质量分析统计</title>
    <script type="text/javascript">

        var saveData4 = function () {
            App.Hidden4.setValue(Ext.encode(App.GridPanel4.getRowsValues({ selectedOnly: false })));
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

        var saveData12 = function () {
            App.Hidden12.setValue(Ext.encode(App.GridPanel12.getRowsValues({ selectedOnly: false })));
        };

        var saveData14 = function () {
            App.Hidden14.setValue(Ext.encode(App.GridPanel14.getRowsValues({ selectedOnly: false })));
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

        var saveData21 = function () {
            App.Hidden21.setValue(Ext.encode(App.GridPanel21.getRowsValues({ selectedOnly: false })));
        };

        var saveData22 = function () {
            App.Hidden22.setValue(Ext.encode(App.GridPanel22.getRowsValues({ selectedOnly: false })));
        };


        //20150627 ANDY
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
     
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <%--樹狀選單--%>
                <ext:Panel ID="PanelL" runat="server" Title ="质量分析" Header="false" Region="West" Width="250" bodyStyle="background-color:#dfe8f6" Split="true" Collapsible="False" PaddingSpec="5 5 5 5">
                    <Items>
                        <ext:DateField ID="beg_date" runat="server" FieldLabel="开始日期" LabelWidth="60" LabelAlign="Right" Format="yyyy-MM-dd" Vtype="daterange">
                            <DirectEvents>
                                <Change OnEvent="SaveBeginDate" />
                            </DirectEvents>
                        </ext:DateField>
                        <ext:DateField ID="end_date" runat="server" FieldLabel="结束日期" LabelWidth="60" LabelAlign="Right" Format="yyyy-MM-dd" Vtype="daterange">
                            <DirectEvents>
                                <Change OnEvent="SaveEndDate" />
                            </DirectEvents>
                        </ext:DateField>
                        <ext:TreePanel ID="TreePanel1" runat="server" Cls="myTreePanel" OnReadData="NodeLoad">
                            <DirectEvents>
                                <ItemClick OnEvent="Node_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                        <ext:Parameter Name="rTEXT" Value="record.data.text" Mode="Raw" />
                                    </ExtraParams>
                                </ItemClick>
                            </DirectEvents>
                        </ext:TreePanel>     
                    </Items>
                </ext:Panel>
                <%--載入內容--%>
                <ext:Panel ID="Panel_Center" runat="server" Region="Center" Layout="FitLayout" AnchorHorizontal="100%" AnchorVertical="100%">
                    <Loader ID="Loader1" runat="server" Mode="Frame" ManuallyTriggered="true" AutoLoad="true" Url="">
                        <LoadMask ShowMask="true" />
                    </Loader> 
                </ext:Panel>
                <%--未知內容--%>
                <%--<ext:Panel ID="PanelR" runat="server" Border="false" Region="Center" ColumnWidth="1" >
                    <Loader ID="Loader3" runat="server" Mode="Frame" ManuallyTriggered="true" AutoLoad="true" Url="">
                        <LoadMask ShowMask="true" />
                    </Loader> 
                    <Items>--%>
                        <%--實驗室檢查--%>
                        <%--<ext:Panel ID="Panel_3" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query3" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query3_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtDATE3" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtTOTAL3" runat="server" FieldLabel="血透人次" LabelAlign="Right" IndicatorText="人次" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtHD" runat="server" FieldLabel="瘘管重建人次" LabelAlign="Right" IndicatorText="人次" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtHD_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" PaddingSpec="4 0 4 0" />
                                <ext:Hidden ID="Hidden4" runat="server" />
                                <ext:GridPanel ID="GridPanel4" runat="server" Title="瘘管重建率" Region="Center" Height="400" PaddingSpec="4 0 4 0" >
                                    <Store>
                                        <ext:Store ID="Store4" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model4" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" />
                                                        <ext:ModelField Name="性别" />
                                                        <ext:ModelField Name="身份证号" />
                                                        <ext:ModelField Name="病历号" />
                                                        <ext:ModelField Name="重建日期" />
                                                        <ext:ModelField Name="重建原因" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel4" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column22" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column23" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column24" Header="性别" runat="server" DataIndex="性别" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column25" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column26" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                            <ext:Column ID="Column31" Header="重建日期" runat="server" DataIndex="重建日期" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column32" Header="重建原因" runat="server" DataIndex="重建原因" Width="400" Cls="x-grid-hd-inner" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar4" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill4" runat="server" />
                                                <ext:Button ID="Button4a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_4" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData4" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button4b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_4" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData4" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button4c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_4" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData4" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>--%>

                        <%--實驗室檢查--%>
                        <%--<ext:Panel ID="Panel_5" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query5" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query5_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtDATE5" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtTOTAL5" runat="server" FieldLabel="血透人次" LabelAlign="Right" IndicatorText="人次" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtSYMPTON" runat="server" FieldLabel="有症状人次" LabelAlign="Right" IndicatorText="人次" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtSYMPTON_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" PaddingSpec="4 0 4 0" />
                                <ext:Hidden ID="Hidden6" runat="server" />
                                <ext:GridPanel ID="GridPanel6" runat="server" Title="血透中有症状" Region="Center" Height="400" PaddingSpec="4 0 4 0" >
                                    <Store>
                                        <ext:Store ID="Store6" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model6" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" />
                                                        <ext:ModelField Name="性别" />
                                                        <ext:ModelField Name="身份证号" />
                                                        <ext:ModelField Name="病历号" />
                                                        <ext:ModelField Name="血透日期" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel6" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column27" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column28" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column29" Header="性别" runat="server" DataIndex="性别" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column30" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column38" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                            <ext:Column ID="Column39" Header="血透日期" runat="server" DataIndex="血透日期" Width="80" Cls="x-grid-hd-inner" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar6" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill6" runat="server" />
                                                <ext:Button ID="Button6a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_6" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData6" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button6b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_6" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData6" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button6c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_6" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData6" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>--%>

                        <%--實驗室檢查--%>
                        <%--<ext:Panel ID="Panel_6" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query6" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query6_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtGridPanel7" runat="server" />
                                <ext:TextField ID="txtDATE6" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtTOTAL6" runat="server" FieldLabel="血透月数" LabelAlign="Right" IndicatorText="" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtSYMPTON2" runat="server" FieldLabel="有症状月数" LabelAlign="Right" IndicatorText="" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtSYMPTON2_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" PaddingSpec="4 0 4 0" />
                                <ext:Hidden ID="Hidden7" runat="server" />
                                <ext:GridPanel ID="GridPanel7" runat="server" Title="每月统计" Region="Center" Height="400" PaddingSpec="4 0 4 0" >
                                    <Store>
                                        <ext:Store ID="Store7" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model7" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="年月" />
                                                        <ext:ModelField Name="总人数" />
                                                        <ext:ModelField Name="异常人数" />
                                                        <ext:ModelField Name="异常比例" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel7" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column40" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column41" Header="年月" runat="server" DataIndex="年月" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column45" Header="总人数" runat="server" DataIndex="总人数" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column46" Header="异常人数" runat="server" DataIndex="异常人数" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column47" Header="异常比例" runat="server" DataIndex="异常比例" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar7" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill7" runat="server" />
                                                <ext:Button ID="Button7a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_7" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData7" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button7b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_7" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData7" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button7c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_7" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData7" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>--%>

                        <%--實驗室檢查--%>
                        <%--<ext:Panel ID="Panel_7" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query7" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query7_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtDATE7" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtTOTAL7" runat="server" FieldLabel="血透月数" LabelAlign="Right" IndicatorText="" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtCHANGE" runat="server" FieldLabel="有转归月数" LabelAlign="Right" IndicatorText="" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtCHANGE_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:Hidden ID="Hidden8" runat="server" />
                                <ext:GridPanel ID="GridPanel8" runat="server" Title="转归率" Region="Center" Height="400" >
                                    <Store>
                                        <ext:Store ID="Store8" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model8" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="年月" />
                                                        <ext:ModelField Name="转归人数" />
                                                        <ext:ModelField Name="退出人数" />
                                                        <ext:ModelField Name="退出人数P" />
                                                        <ext:ModelField Name="肾移植人数" />
                                                        <ext:ModelField Name="肾移植人数P" />
                                                        <ext:ModelField Name="转出人数" />
                                                        <ext:ModelField Name="转出人数P" />
                                                        <ext:ModelField Name="死亡人数" />
                                                        <ext:ModelField Name="死亡人数P" />
                                                        <ext:ModelField Name="转入人数" />
                                                        <ext:ModelField Name="转入人数P" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel8" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column44" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column48" Header="年月" runat="server" DataIndex="年月" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column49" Header="转归人数" runat="server" DataIndex="转归人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column50" Header="退出人数" runat="server" DataIndex="退出人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column51" Header="退出％" runat="server" DataIndex="退出人数P" Width="50" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column52" Header="肾移植人数" runat="server" DataIndex="肾移植人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column53" Header="肾移植％" runat="server" DataIndex="肾移植人数P" Width="50" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column54" Header="转出人数" runat="server" DataIndex="转出人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column55" Header="转出％" runat="server" DataIndex="转出人数P" Width="50" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column56" Header="死亡人数" runat="server" DataIndex="死亡人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column57" Header="死亡％" runat="server" DataIndex="死亡人数P" Width="50" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column58" Header="转入人数" runat="server" DataIndex="转入人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column59" Header="转入％" runat="server" DataIndex="转入人数P" Width="50" Cls="x-grid-hd-inner" Align="Right" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar8" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill8" runat="server" />
                                                <ext:Button ID="Button8a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_8" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData8" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button8b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_8" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData8" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button8c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_8" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData8" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>--%>

                        <%--實驗室檢查--%>
                        <%--<ext:Panel ID="Panel_8" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query8" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query8_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtDATE8" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_CODE2" runat="server" FieldLabel="检查代码" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_NAME2" runat="server" FieldLabel="检查名称" LabelAlign="Right" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_UNIT2" runat="server" FieldLabel="检查单位" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtNORMAL2" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" Width="300" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtTOTAL8" runat="server" FieldLabel="血透月数" LabelAlign="Right" IndicatorText="" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="TextField4" runat="server" FieldLabel="有转归月数" LabelAlign="Right" IndicatorText="" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="TextField5" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:Hidden ID="Hidden9" runat="server" />
                                <ext:GridPanel ID="GridPanel9" runat="server" Title="每月统计" Region="Center" Height="400" PaddingSpec="4 0 4 0" >
                                    <Store>
                                        <ext:Store ID="Store9" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model9" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="年月" />
                                                        <ext:ModelField Name="血透人数" />
                                                        <ext:ModelField Name="已做人数" />
                                                        <ext:ModelField Name="已做人数P" />
                                                        <ext:ModelField Name="未做人数" />
                                                        <ext:ModelField Name="未做人数P" />
                                                        <ext:ModelField Name="合格人数" />
                                                        <ext:ModelField Name="合格人数P" />
                                                        <ext:ModelField Name="不合格人数" />
                                                        <ext:ModelField Name="不合格人数P" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel9" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column60" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column61" Header="年月" runat="server" DataIndex="年月" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column62" Header="血透人数" runat="server" DataIndex="血透人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column63" Header="已做人数" runat="server" DataIndex="已做人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column68" Header="已做％" runat="server" DataIndex="已做人数P" Width="60" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column70" Header="未做人数" runat="server" DataIndex="未做人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column65" Header="未做％" runat="server" DataIndex="未做人数P" Width="60" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column67" Header="合格人数" runat="server" DataIndex="合格人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column64" Header="合格％" runat="server" DataIndex="合格人数P" Width="60" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column66" Header="不合格人数" runat="server" DataIndex="不合格人数" Width="80" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column69" Header="不合格％" runat="server" DataIndex="不合格人数P" Width="60" Cls="x-grid-hd-inner" Align="Right" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar9" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill9" runat="server" />
                                                <ext:Button ID="Button9a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_9" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData9" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button9b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_9" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData9" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button9c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_9" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData9" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>--%>

                        <%--每月统计--%>
                        <%--<ext:Panel ID="Panel_10" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <%--<ext:Button ID="btn_Query10" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query10_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtDATE10" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtTOTAL10" runat="server" FieldLabel="血透月数" LabelAlign="Right" IndicatorText="" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_CODE4" runat="server" FieldLabel="检查代码" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_NAME4" runat="server" FieldLabel="检查名称" LabelAlign="Right" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_UNIT4" runat="server" FieldLabel="检查单位" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtNORMAL4" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" PaddingSpec="4 0 4 0" />
                                <ext:Hidden ID="Hidden12" runat="server" />
                                <ext:GridPanel ID="GridPanel12" runat="server" Title="每月统计" Region="Center" Height="400" PaddingSpec="4 0 4 0" >
                                    <Store>
                                        <ext:Store ID="Store12" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model12" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="年月" />
                                                        <ext:ModelField Name="受检人数" />
                                                        <ext:ModelField Name="阳性人数" />
                                                        <ext:ModelField Name="阳性比例" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel12" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column71" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column72" Header="年月" runat="server" DataIndex="年月" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column73" Header="受检人数" runat="server" DataIndex="受检人数" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column74" Header="阳性人数" runat="server" DataIndex="阳性人数" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                            <ext:Column ID="Column75" Header="阳性比例" runat="server" DataIndex="阳性比例" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar12" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill12" runat="server" />
                                                <ext:Button ID="Button12a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_12" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData12" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button12b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_12" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData12" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button12c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_12" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData12" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>--%>

                        <%--I.血透月报表--%>
                        <%--<ext:Panel ID="Panel_I" runat="server" Border="false" Region="Center" ColumnWidth="1">
                            <Loader ID="LoaderI" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>--%>
                <%--</Items>
                </ext:Panel>--%>
                <ext:Window ID="Window1" runat="server" Title=""  Width="900" Height="700" Y="10" Modal="true" AutoRender="false" Collapsible="true" Maximizable="true" Hidden="true" >
                    <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window> 
                <ext:Window ID="Window2" runat="server" Title="病患查找" Width="600" Height="330" Modal="true" Hidden="true" CloseAction="Hide" UI="Success" Resizable="false">
                    <Items>
                        <ext:FormPanel ID="FormPanel1" runat="server"> 
                            <Items>                   
                                <ext:GridPanel ID="GridList" runat="server" Cls="x-grid-custom" AutoScroll="true">
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar14" runat="server">
                                            <Items>
                                                <ext:ComboBox ID="SearchName" runat="server" FieldLabel="姓名" LabelWidth="30" Width="170" Cls="Text-blue" LabelAlign="Right"
                                                    DisplayField="patname" ValueField="patname" TypeAhead="false" HideTrigger="true" MinChars="1" TriggerAction="Query">                            
                                                    <ListConfig LoadingText="寻找中...">
                                                        <ItemTpl ID="ItemTpl1" runat="server">
                                                            <Html>
                                                                <div>{patname}</div>
                                                            </html>
                                                        </ItemTpl>
                                                    </ListConfig>
                                                    <Store>
                                                        <ext:Store ID="Store16" runat="server" AutoLoad="false">
                                                            <Proxy>
                                                                <ext:AjaxProxy Url="Patinfos.ashx">
                                                                    <ActionMethods Read="POST" />
                                                                    <Reader>
                                                                        <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                                    </Reader>
                                                                </ext:AjaxProxy>
                                                            </Proxy>
                                                            <Model>
                                                                <ext:Model ID="Model16" runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="patic" />
                                                                        <ext:ModelField Name="patname" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                </ext:ComboBox>
                                                <ext:TextField ID="SearchID" runat="server" FieldLabel="身份证号" LabelWidth="80" LabelAlign="Right" Width="270" />
                                                <ext:Button ID="btn_Query" runat="server" Text="查找" Icon="Find" Width="100" UI="Success">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Query_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Store>
                                        <ext:Store ID="Store17" runat="server" PageSize="10">
                                        <Model>
                                            <ext:Model ID="Model17" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="pat_id" />
                                                    <ext:ModelField Name="pif_name" />
                                                    <ext:ModelField Name="pif_sex" />
                                                    <ext:ModelField Name="pif_dob" />
                                                    <ext:ModelField Name="pat_ic" />                                                     
                                                    <ext:ModelField Name="pif_docname" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                        <Reader>
                                            <ext:ArrayReader />
                                        </Reader>
                                        <Sorters>
                                            <ext:DataSorter Property="pat_id" Direction="ASC" />
                                        </Sorters>
                                    </ext:Store>
                                    </Store>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                    <ColumnModel ID="ColumnModel14" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="序" Width="50" />
                                            <ext:Column ID="Column76" runat="server" Text="姓名" DataIndex="pif_name" Width="80" />
                                            <ext:Column ID="Column94" runat="server" Text="性别" DataIndex="pif_sex" Width="60" />                                            
                                            <ext:Column ID="Column95" runat="server" Text="出生日期" DataIndex="pif_dob" Width="110" />
                                            <ext:Column ID="Column96" runat="server" Text="身份证号" DataIndex="pat_ic" Width="190" />
                                            <ext:Column ID="Column97" runat="server" Text="经治医生" DataIndex="pif_docname" Region="Center" Flex="1" />
                                        </Columns>
                                    </ColumnModel>
                                    <Plugins>
                                        <ext:BufferedRenderer ID="BufferedRenderer1" runat="server" />
                                    </Plugins>
                                    <View>
                                        <ext:GridView ID="GridView1" runat="server" TrackOver="false" />
                                    </View>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">                                
                                            <DirectEvents>
                                                <Select OnEvent="Dialysis_detail">
                                                    <EventMask ShowMask="true" Msg="处理中….." Target="CustomTarget" CustomTarget="#{pnlTableLayout}" />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="Values" Value="#{GridList}.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                                    </ExtraParams>
                                                </Select>
                                            </DirectEvents>
                                        </ext:RowSelectionModel>
                                    </SelectionModel> 
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store17" />
                                    </BottomBar>
                                </ext:GridPanel>
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <DirectEvents>
                        <BeforeClose OnEvent="Win_Close" />
                    </DirectEvents>
                </ext:Window> 
            </Items>
        </ext:Viewport> 
    </form>
</body>
</html>
