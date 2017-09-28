<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_B01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_B01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>实验室检查</title>
    <script type="text/javascript">
        var saveData = function () {
            App.Hidden1.setValue(Ext.encode(App.GridPanel1.getRowsValues({ selectedOnly: false })));
        };

        var saveData2 = function () {
            App.Hidden2.setValue(Ext.encode(App.GridPanel2.getRowsValues({ selectedOnly: false })));
        };

        var saveData21 = function () {
            App.Hidden21.setValue(Ext.encode(App.GridPanel21.getRowsValues({ selectedOnly: false })));
        };

        var saveData22 = function () {
            App.Hidden22.setValue(Ext.encode(App.GridPanel22.getRowsValues({ selectedOnly: false })));
        };
    </script>
    <style type = "text/css">    
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
        <ext:Hidden ID="txtNODE_ID" runat="server" />
        <ext:Hidden ID="txtNODE_TEXT" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />     
        <ext:Viewport ID="Viewport1" runat="server">
            <Items>
                <ext:Panel ID="Panel_1" runat="server" Border="false" Region="Center" ColumnWidth="1">
                    <Items>
                        <ext:Panel ID="PanelU" runat="server" Border="false" Region="North" Height="640" >
                            <Items>
                                <ext:Container ID="Container1" runat="server" Frame="true" Layout="ColumnLayout" >
                                    <Items>
                                        <ext:TextField ID="txtDATE1" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtRESULT_CODE" runat="server" FieldLabel="检查代码" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtRESULT_NAME" runat="server" FieldLabel="检查名称" LabelAlign="Right" LabelWidth="70" Width="170" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtRESULT_UNIT" runat="server" FieldLabel="检查单位" LabelAlign="Right" LabelWidth="90" Width="190" Hidden="true" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtNORMAL" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" LabelWidth="100" Width="300" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtFORMAT" runat="server" FieldLabel="格式" LabelAlign="Right" LabelWidth="50" Width="150" IndicatorText="　　" PaddingSpec="4 0 4 0" ReadOnly="true" />
                                        <ext:Checkbox ID="chkFORMAT" runat="server" BoxLabel="启用四舍五入" PaddingSpec="4 0 4 0" Checked="true" >
                                            <DirectEvents>
                                                <Change OnEvent="hh" />
                                            </DirectEvents>
                                        </ext:Checkbox>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2" runat="server" Frame="true" Layout="ColumnLayout" >
                                    <Items>
                                        <ext:TextField ID="txtTOTAL1" runat="server" FieldLabel="血透人数" LabelAlign="Right" IndicatorText="人" LabelWidth="70" Width="170" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtCHECK" runat="server" FieldLabel="已做人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtCHECK_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" LabelWidth="30" Width="100" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtUNCHECK" runat="server" FieldLabel="未做人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtUNCHECK_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" LabelWidth="30" Width="100" PaddingSpec="4 0 4 0" />
                                        <%--<ext:TextField ID="lstUNCHECK" runat="server" FieldLabel="[未做名单]" Hidden="true" />--%>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container3" runat="server" Frame="true" Layout="ColumnLayout" >
                                    <Items>
                                        <ext:TextField ID="txtCHECK_Y" runat="server" FieldLabel="合格人数" LabelAlign="Right" IndicatorText="人" LabelWidth="270" Width="370" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtCHECK_YP" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" LabelWidth="30" Width="100" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtCHECK_N" runat="server" FieldLabel="不合格人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtCHECK_NP" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" LabelWidth="30" Width="100" PaddingSpec="4 0 4 0" />
                                        <%--<ext:TextField ID="lstCHECK_N" runat="server" FieldLabel="[不合格名单]" Hidden="true" />--%>
                                    </Items>
                                </ext:Container>
                                <%--达标准参考区间值名单--%><%--未达标准参考区间值名单--%>                                        
                                <ext:Container ID="Container6" runat="server" Frame="true" Layout="ColumnLayout" >
                                    <Items>
                                        <ext:Hidden ID="Hidden21" runat="server" />
                                        <ext:GridPanel ID="GridPanel21" runat="server" Title="达标准参考区间值名单" Height="194" PaddingSpec="4 4 4 2" ColumnWidth=".5" > 
                                            <Store>
                                                <ext:Store ID="Store21" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model21" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="姓名" />
                                                                <ext:ModelField Name="身份证号" />
                                                                <ext:ModelField Name="病历号" />
                                                                <ext:ModelField Name="检验代码" />
                                                                <ext:ModelField Name="检验名称" />
                                                                <ext:ModelField Name="检验结果" Type="Float" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                    <Sorters>
                                                        <ext:DataSorter Property="common" Direction="ASC" />
                                                    </Sorters>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel21" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column111" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column112" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column113" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column114" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column115" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column116" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:NumberColumn ID="Column117" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                                </Columns>
                                            </ColumnModel>
                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar21" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill21" runat="server" />
                                                        <ext:Button ID="Button21a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_21" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData21" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button21b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_21" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData21" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button21c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_21" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData21" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                        </ext:GridPanel> 

                                        <ext:Hidden ID="Hidden1" runat="server" />
                                        <ext:GridPanel ID="GridPanel1" runat="server" Title="未达标准参考区间值名单" Height="194" PaddingSpec="4 2 4 4" ColumnWidth=".5" > 
                                            <Store>
                                                <ext:Store ID="Store1" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model1" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="姓名" />
                                                                <ext:ModelField Name="身份证号" />
                                                                <ext:ModelField Name="病历号" />
                                                                <ext:ModelField Name="检验代码" />
                                                                <ext:ModelField Name="检验名称" />
                                                                <ext:ModelField Name="检验结果" Type="Float" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                    <Sorters>
                                                        <ext:DataSorter Property="common" Direction="ASC" />
                                                    </Sorters>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel1" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column1" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column2" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column3" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column4" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column5" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column100" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:NumberColumn ID="Column6" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                                </Columns>
                                            </ColumnModel>
                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar1" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill1" runat="server" />
                                                        <ext:Button ID="Button1a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_1" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button1b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_1" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button1c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_1" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                        </ext:GridPanel> 
                                    </Items>
                                </ext:Container>                                        
                                <ext:Hidden ID="Hidden22" runat="server" /><%--已做参考区间值名单--%>
                                <ext:GridPanel ID="GridPanel22" runat="server" Title="已做参考区间值名单" Height="300" PaddingSpec="4 2 4 2" >
                                    <Store>
                                        <ext:Store ID="Store22" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model22" runat="server" Name="no" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" />
                                                        <ext:ModelField Name="身份证号" />
                                                        <ext:ModelField Name="病历号" />
                                                        <ext:ModelField Name="检验代码" />
                                                        <ext:ModelField Name="检验名称" />
                                                        <ext:ModelField Name="检验结果" Type="Float" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel22" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column118" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column119" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column120" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column121" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column122" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column123" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:NumberColumn ID="Column124" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" Align="Right" />
                                        </Columns>
                                    </ColumnModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar22" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill22" runat="server" />
                                                <ext:Button ID="Button22a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_22" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData22" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button22b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_22" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData22" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button22c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_22" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData22" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                                <ext:Hidden ID="Hidden2" runat="server" /><%--尚未做检验名单--%>
                                <ext:GridPanel ID="GridPanel2" runat="server" Title="尚未做检验名单" Height="292" PaddingSpec="4 0 4 0" Hidden="true" >
                                    <Store>
                                        <ext:Store ID="Store2" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model2" runat="server" Name="UNCHECK" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" />
                                                        <ext:ModelField Name="身份证号" />
                                                        <ext:ModelField Name="病历号" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                            <Sorters>
                                                <ext:DataSorter Property="common" Direction="ASC" />
                                            </Sorters>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel2" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column7" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column8" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column9" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column10" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                        </Columns>
                                    </ColumnModel>
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill2" runat="server" />
                                                <ext:Button ID="Button2a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_2" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData2" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button2b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_2" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData2" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button2c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_2" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData2" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport> 
    </form>
</body>
</html>
