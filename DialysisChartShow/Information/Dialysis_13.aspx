<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_13.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_13" %>
<%@ Register Assembly="CrystalDecisions.Web, Version=13.0.2000.0, Culture=neutral, PublicKeyToken=692fbea5521e1304" Namespace="CrystalDecisions.Web" TagPrefix="CR" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
        <script type="text/javascript">
          var saveData = function () {
            App.Hidden1.setValue(Ext.encode(App.GridPanel1.getRowsValues({ selectedOnly: false })));
          };

          var saveData2 = function () {
            App.Hidden2.setValue(Ext.encode(App.GridPanel2.getRowsValues({ selectedOnly: false })));
          };

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

          var saveData21 = function () {
            App.Hidden21.setValue(Ext.encode(App.GridPanel21.getRowsValues({ selectedOnly: false })));
          };

          var saveData22 = function () {
            App.Hidden22.setValue(Ext.encode(App.GridPanel22.getRowsValues({ selectedOnly: false })));
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
        
    <%-- panel head 自动 --%>
    .x-panel-header-text {
        font-size: 16px;
        font-weight: bold;
        line-height: 20px;
    }
    <%-- cell字型大小  自动  --%>
    .x-grid-row .x-grid-cell { 
        font-size: 13px;
    }
    <%-- grid column head  手动Cls="x-grid-hd-inner" --%>
    .x-grid-hd-inner {
        font-size: 12px;
        font-weight: bold;
    }
    <%-- grid column 上色  手动tdCls="custom-column" --%>
    .x-grid-row .custom-column { 
        font-weight: bold;
    }
    
    <%-- tree node text size 手动Cls="large-font" --%>
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
    <div>
            <ext:ResourceManager ID="ResourceManager2" runat="server" />
<%--          </ext:ResourceManager> --%>

<ext:Viewport ID="Viewport1" runat="server" Layout="border" >
            <Items>
                                                                
                <ext:Panel ID="PanelL" runat="server" Title ="质量分析" Region="West" bodyStyle="background-color:#dfe8f6" Split="true" Collapsible="true" Cls=".myTitle" BodyBorder="3" ButtonAlign="Center" Visible="False" Width="250">
                    <Items>
                        <ext:TextField ID="txtNODE_ID" runat="server" />  
                        <ext:TextField ID="txtNODE_TEXT" runat="server" /> 
                        <ext:DateField ID="beg_date" runat="server" FieldLabel="开始日期" LabelWidth="60" LabelAlign="Right" Format="yyyy-MM-dd" Vtype="daterange" EnableKeyEvents="true" >
                            <%--<Plugins>
                                <ext:ClearButton ID="ClearButton1" runat="server" />
                            </Plugins>--%>
                            <%--<CustomConfig>
                                <ext:ConfigItem Name="endDateField" Value="end_date" Mode="Value" />
                            </CustomConfig>--%>
                            <%--<Listeners>
                                <KeyUp Fn="onKeyUp" />
                            </Listeners>--%>
                        </ext:DateField>
                        <ext:DateField ID="end_date" runat="server" FieldLabel="结束日期" LabelWidth="60" LabelAlign="Right" Format="yyyy-MM-dd" Vtype="daterange" EnableKeyEvents="true" >
                            <%--<Plugins>
                                <ext:ClearButton ID="ClearButton2" runat="server" />
                            </Plugins>--%>
                            <%--<CustomConfig>
                                <ext:ConfigItem Name="startDateField" Value="beg_date" Mode="Value" />
                            </CustomConfig>--%>
                            <%--<Listeners>
                                <KeyUp Fn="onKeyUp" />
                            </Listeners>--%>
                        </ext:DateField>
                        <ext:TreePanel ID="TreePanel1" runat="server" Cls=".myTreePanel" OnReadData="NodeLoad"  >
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

                <ext:Panel ID="PanelR" runat="server" Border="false" Region="Center" ColumnWidth="1" >
                    <Items>

                        <ext:Panel ID="Panel_1" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Panel ID="PanelU" runat="server" Border="false" Region="North" Height="640" >
                                    <Items>
                                        <ext:Container ID="Container1" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:Button ID="btn_Query1" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Query1_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
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
                                        
                                        <ext:Container ID="Container6" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>

                                        <ext:Hidden ID="Hidden21" runat="server" />
                                        <ext:GridPanel ID="GridPanel21" runat="server" Title="达标准参考区间值名单" Height="194" PaddingSpec="4 4 4 2" ColumnWidth=".5" >
                                            <Store>
                                                <ext:Store ID="Store21" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model21" runat="server" Name="pif_ic" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="姓名" />
                                                                <ext:ModelField Name="身份证号" />
                                                                <ext:ModelField Name="病历号" />
                                                                <ext:ModelField Name="检验代码" />
                                                                <ext:ModelField Name="检验名称" />
                                                                <ext:ModelField Name="检验结果" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel21" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column111" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column112" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column113" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column114" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column115" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column116" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column117" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" />
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
                                                        <ext:Model ID="Model1" runat="server" Name="pif_ic" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="姓名" />
                                                                <ext:ModelField Name="身份证号" />
                                                                <ext:ModelField Name="病历号" />
                                                                <ext:ModelField Name="检验代码" />
                                                                <ext:ModelField Name="检验名称" />
                                                                <ext:ModelField Name="检验结果" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel1" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column1" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column2" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column3" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column4" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column5" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column100" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="Column6" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" />
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
                                        
                                        <ext:Hidden ID="Hidden22" runat="server" />
                                        <ext:GridPanel ID="GridPanel22" runat="server" Title="已做参考区间值名单" Height="300" PaddingSpec="4 2 4 2" >
                                            <Store>
                                                <ext:Store ID="Store22" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model22" runat="server" Name="pif_ic" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="姓名" />
                                                                <ext:ModelField Name="身份证号" />
                                                                <ext:ModelField Name="病历号" />
                                                                <ext:ModelField Name="检验代码" />
                                                                <ext:ModelField Name="检验名称" />
                                                                <ext:ModelField Name="检验结果" />
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
                                                    <ext:Column ID="Column118" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column119" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column120" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column121" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column122" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column123" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column124" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" />
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

                                        <ext:Hidden ID="Hidden2" runat="server" />
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
                
                        <ext:Panel ID="Panel_2" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query2" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query2_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtDATE2" runat="server" FieldLabel="日期范围" LabelAlign="Right" PaddingSpec="4 0 4 0" Width="300" />
                                <ext:TextField ID="txtTOTAL2" runat="server" FieldLabel="血透人数" LabelAlign="Right" IndicatorText="人" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtDIE" runat="server" FieldLabel="死亡人数" LabelAlign="Right" IndicatorText="人" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtDIE_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="‰" PaddingSpec="4 0 4 0" />
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
                                            <ext:Column ID="Column11" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column12" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column21" Header="性别" runat="server" DataIndex="性别" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column13" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column14" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                            <ext:Column ID="Column19" Header="生日" runat="server" DataIndex="生日" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column15" Header="年龄" runat="server" DataIndex="年龄" Width="40" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column20" Header="开始透析日期" runat="server" DataIndex="开始透析日期" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column16" Header="透析年限Q" runat="server" DataIndex="透析年限" Width="60" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column18" Header="死亡日期QQ" runat="server" DataIndex="死亡日期" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column17" Header="死亡原因QQQ" runat="server" DataIndex="死亡原因" Width="200" Cls="x-grid-hd-inner" />
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
                
                        <ext:Panel ID="Panel_3" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
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
                        </ext:Panel>

                        <ext:Panel ID="Panel_4" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query4" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query4_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtDATE4" runat="server" FieldLabel="日期范围" LabelAlign="Right" PaddingSpec="4 0 4 0" Width="300" />
                                <ext:TextField ID="txtTOTAL4" runat="server" FieldLabel="血透人数" LabelAlign="Right" IndicatorText="人" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtHOSP" runat="server" FieldLabel="住院人数" LabelAlign="Right" IndicatorText="人" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtHOSP_P" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="‰" PaddingSpec="4 0 4 0" />
                                <ext:Hidden ID="Hidden5" runat="server" />
                                <ext:GridPanel ID="GridPanel5" runat="server" Title="住院原因分析" Region="Center" Height="400" PaddingSpec="4 0 4 0" >
                                    <Store>
                                        <ext:Store ID="Store5" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model5" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" />
                                                        <ext:ModelField Name="性别" />
                                                        <ext:ModelField Name="身份证号" />
                                                        <ext:ModelField Name="病历号" />
                                                        <ext:ModelField Name="住院日期" />
                                                        <ext:ModelField Name="住院原因" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel5" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column33" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column34" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column35" Header="性别" runat="server" DataIndex="性别" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column36" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column37" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                            <ext:Column ID="Column42" Header="住院日期" runat="server" DataIndex="住院日期" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column43" Header="住院原因" runat="server" DataIndex="住院原因" Width="400" Cls="x-grid-hd-inner" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar5" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill5" runat="server" />
                                                <ext:Button ID="Button5a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_5" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData5" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button5b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_5" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData5" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button5c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_5" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData5" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>

                        <ext:Panel ID="Panel_5" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
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
                        </ext:Panel>

                        <ext:Panel ID="Panel_6" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
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
                        </ext:Panel>

                        <ext:Panel ID="Panel_7" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
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
                        </ext:Panel>

                        <ext:Panel ID="Panel_8" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
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
                        </ext:Panel>

                        <ext:Panel ID="Panel_9" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query9" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query9_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:TextField ID="txtDATE9" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_CODE3" runat="server" FieldLabel="检查代码" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_NAME3" runat="server" FieldLabel="检查名称" LabelAlign="Right" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtRESULT_UNIT3" runat="server" FieldLabel="检查单位" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtNORMAL3" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtTOTAL9" runat="server" FieldLabel="受检人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" />
                                <ext:TextField ID="txtPOSITIVE" runat="server" FieldLabel="阳性人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" />
                                <ext:Hidden ID="Hidden10" runat="server" />
                                <ext:GridPanel ID="GridPanel10" runat="server" Title="阳性名单" Region="Center" Height="200" PaddingSpec="4 0 4 0" >
                                    <Store>
                                        <ext:Store ID="Store10" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model10" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" />
                                                        <ext:ModelField Name="身份证号" />
                                                        <ext:ModelField Name="病历号" />
                                                        <ext:ModelField Name="检验代码" />
                                                        <ext:ModelField Name="检验名称" />
                                                        <ext:ModelField Name="检验结果" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel10" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column76" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column77" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column78" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column79" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column80" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column101" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column81" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar10" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill10" runat="server" />
                                                <ext:Button ID="Button10a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_10" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData10" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button10b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_10" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData10" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button10c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_10" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData10" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                                
                                <ext:Hidden ID="Hidden11" runat="server" />
                                <ext:GridPanel ID="GridPanel11" runat="server" Title="受检名单" Region="Center" Height="200" PaddingSpec="4 0 4 0" >
                                    <Store>
                                        <ext:Store ID="Store11" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model11" runat="server" Name="pif_ic" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" />
                                                        <ext:ModelField Name="身份证号" />
                                                        <ext:ModelField Name="病历号" />
                                                        <ext:ModelField Name="检验代码" />
                                                        <ext:ModelField Name="检验名称" />
                                                        <ext:ModelField Name="检验结果" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel11" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="Column82" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column83" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column84" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column85" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column86" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column102" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column87" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" />
                                        </Columns>
                                    </ColumnModel>
                                         
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar11" runat="server">
                                            <Items>
                                                <ext:ToolbarFill ID="ToolbarFill11" runat="server" />
                                                <ext:Button ID="Button11a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_11" Icon="PageCode">
                                                    <Listeners>
                                                        <Click Fn="saveData11" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button11b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_11" Icon="PageExcel">
                                                    <Listeners>
                                                        <Click Fn="saveData11" />
                                                    </Listeners>
                                                </ext:Button>
                                                <ext:Button ID="Button11c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_11" Icon="PageAttach">
                                                    <Listeners>
                                                        <Click Fn="saveData11" />
                                                    </Listeners>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>

                        <ext:Panel ID="Panel_10" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Button ID="btn_Query10" runat="server" Icon="Find" Text="查询" Hidden="true" PaddingSpec="4 0 4 0" >
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
                        </ext:Panel>

                        <ext:Panel ID="Panel_11" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Panel ID="Panel2" runat="server" Border="false" Region="North" Height="640" >
                                    <Items>
                                        <ext:Container ID="Container11" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:ComboBox ID="ComboBoxGroup1" runat="server" FieldLabel="大分类" LabelAlign="Right" DisplayField="GROUP_NAME" EmptyText="选择一个分类">
                                                    <Store>
                                                        <ext:Store ID="Store20" runat="server">
                                                            <Model>
                                                                <ext:Model ID="Model20" runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="GROUP_NAME" />
                                                                        <ext:ModelField Name="GROUP_CODE" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Select OnEvent="ChangGroup1" />
                                                    </DirectEvents>
                                                </ext:ComboBox>
                                                
                                                <ext:ComboBox ID="cboCODE11" runat="server" FieldLabel="检查名称" LabelWidth="70" LabelAlign="Right" Editable="false" EmptyText="未选择" Width="270" PaddingSpec="4 0 4 0" >
                                                    <DirectEvents>
                                                        <Select OnEvent = "cboCODE11_Click" />
                                                    </DirectEvents>
                                                </ext:ComboBox>

                                                <ext:TextField ID="txtRESULT_LOW" runat="server" FieldLabel="自订参考区间" LabelAlign="Right" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" />
                                                <ext:TextField ID="txtRESULT_HIGH" runat="server" FieldLabel="~" LabelAlign="Right" LabelWidth="20" Width="120" PaddingSpec="4 0 4 0" />
                                                <ext:Label ID="Label1" runat="server" Text =" " Width="40" PaddingSpec="4 0 4 0" />
                                                <ext:Button ID="btn_Query11" runat="server" Icon="Find" Text="查询" Width="80" PaddingSpec="4 0 4 0" >
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Query11_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:TextField ID="txtDATE11" runat="server" FieldLabel="日期范围" LabelAlign="Right" PaddingSpec="4 0 4 0" Hidden="true" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container18" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:TextField ID="txtRESULT_CODE11" runat="server" FieldLabel="检查代码" LabelAlign="Right" LabelWidth="70" Width="170" PaddingSpec="4 0 4 0" />
                                                <ext:TextField ID="txtRESULT_NAME11" runat="server" FieldLabel="检查名称" LabelAlign="Right" LabelWidth="70" PaddingSpec="4 0 4 0" Width="170" Hidden="true" />
                                                <ext:TextField ID="txtRESULT_UNIT11" runat="server" FieldLabel="检查单位" LabelAlign="Right" LabelWidth="100" PaddingSpec="4 0 4 0" Width="200" Hidden="true" />
                                                <ext:TextField ID="txtNORMAL11" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" LabelWidth="100" PaddingSpec="4 0 4 0" Width="300" />
                                            </Items>
                                        </ext:Container>
                                        <%--<ext:Container ID="Container13" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:TextField ID="txtTOTAL11" runat="server" FieldLabel="血透人数" LabelAlign="Right" IndicatorText="人" LabelWidth="70" PaddingSpec="4 0 4 0" Width="170" />
                                                <ext:TextField ID="txtCHECK2" runat="server" FieldLabel="已做人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" PaddingSpec="4 0 4 0" Width="200" />
                                                <ext:TextField ID="txtCHECK_P2" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" LabelWidth="30" PaddingSpec="4 0 4 0" Width="100" />
                                                <ext:TextField ID="txtUNCHECK2" runat="server" FieldLabel="未做人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" PaddingSpec="4 0 4 0" Width="200" />
                                                <ext:TextField ID="txtUNCHECK_P2" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" LabelWidth="30" PaddingSpec="4 0 4 0" Width="100" />
                                                --<ext:TextField ID="lstUNCHECK" runat="server" FieldLabel="[未做名单]" Hidden="true" />--
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container15" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:TextField ID="txtCHECK_Y2" runat="server" FieldLabel="合格人数" LabelAlign="Right" IndicatorText="人" LabelWidth="270" PaddingSpec="4 0 4 0" Width="370" />
                                                <ext:TextField ID="txtCHECK_YP2" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" LabelWidth="30" PaddingSpec="4 0 4 0" Width="100" />
                                                <ext:TextField ID="txtCHECK_N2" runat="server" FieldLabel="不合格人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" PaddingSpec="4 0 4 0" Width="200" />
                                                <ext:TextField ID="txtCHECK_NP2" runat="server" FieldLabel="占" LabelAlign="Right" IndicatorText="％" LabelWidth="30" PaddingSpec="4 0 4 0" Width="100" />
                                                --<ext:TextField ID="lstCHECK_N" runat="server" FieldLabel="[不合格名单]" Hidden="true" />--
                                            </Items>
                                        </ext:Container>--%>
                                        
                                        <ext:Hidden ID="Hidden13" runat="server" />
                                        <ext:GridPanel ID="GridPanel13" runat="server" Title="达自订参考区间值名单" Region="Center" Height="505" PaddingSpec="4 0 4 0" >
                                            <Store>
                                                <ext:Store ID="Store13" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model13" runat="server" Name="pif_ic" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="姓名" />
                                                                <ext:ModelField Name="身份证号" />
                                                                <ext:ModelField Name="病历号" />
                                                                <ext:ModelField Name="检验日期" />
                                                                <ext:ModelField Name="检验代码" />
                                                                <ext:ModelField Name="检验名称" />
                                                                <ext:ModelField Name="检验结果" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel13" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column88" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column89" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column90" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column91" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column98" Header="检验日期" runat="server" DataIndex="检验日期" Width="100" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column92" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column99" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column93" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" />
                                                </Columns>
                                            </ColumnModel>
                                         
                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar13" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill13" runat="server" />
                                                        <ext:Button ID="Button13a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_13" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData13" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button13b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_13" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData13" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button13c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_13" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData13" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                        </ext:GridPanel> 
                                        
                                        <ext:Hidden ID="Hidden14" runat="server" />
                                        <ext:GridPanel ID="GridPanel14" runat="server" Title="尚未做检验名单" Region="Center" Height="292" PaddingSpec="4 0 4 0" Hidden="true" >
                                            <Store>
                                                <ext:Store ID="Store14" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model14" runat="server" Name="UNCHECK" >
                                                            <Fields>
                                                                <ext:ModelField Name="NO" Type="Int" />
                                                                <ext:ModelField Name="pif_name" />
                                                                <ext:ModelField Name="pv_ic" />
                                                                <ext:ModelField Name="pif_id" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>

                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar14" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill14" runat="server" />
                                                        <ext:Button ID="Button14a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_14" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData14" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button14b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_14" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData14" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button14c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_14" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData14" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                    
                                            <ColumnModel ID="ColumnModel14" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column94" Header="编号" runat="server" DataIndex="NO" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column95" Header="姓名" runat="server" DataIndex="pif_name" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column96" Header="身份证号" runat="server" DataIndex="pv_ic" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column97" Header="病历号" runat="server" DataIndex="pif_id" Width="100" Cls="x-grid-hd-inner" />
                                                </Columns>
                                            </ColumnModel>
                                        </ext:GridPanel> 
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>

                        <%--
                        "NO"Type        =编号
                        "Y_M"           =年月
                        "TOTAL"         =受检人数
                        "ERROR"         =阳性人数
                        "ERROR_P"       =阳性比例
                        "pif_name"      =姓名
                        "pif_ic"        =身份证号
                        "PAT_NO"        =病历号
                        "RESULT_DATE"   =检验日期
                        "RESULT_CODE"   =检验代码
                        "RESULT_NAME"   =检验名称
                        "RESULT_VALUE_N"=检验结果
                        "RESULT_VALUE_T"=检验结果
                        --%>

                        <%-- 20150627 ANDY監控指標查詢S --%>
                      <ext:Panel ID="Panel_11Q" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Panel ID="Panel2Q" runat="server" Border="false" Region="North" Height="640" >
                                    <Items>
                                        <ext:Container ID="Container11Q" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:ComboBox ID="cboCODE11Q" runat="server" FieldLabel="年度" LabelWidth="70" LabelAlign="Right" Editable="false" EmptyText="未选择" Width="170" PaddingSpec="4 0 4 0" >
                                                   <Items>
                                                      <ext:ListItem Value="001" Text="   " />
                                                      <ext:ListItem Value="002" Text="2013" />
                                                      <ext:ListItem Value="003" Text="2014" />
                                                      <ext:ListItem Value="004" Text="2015" />
                                                      <ext:ListItem Value="005" Text="2016" />
                                                      <ext:ListItem Value="006" Text="2017" />
                                                      <ext:ListItem Value="007" Text="2018" />
                                                      <ext:ListItem Value="008" Text="2019" />
                                                      <ext:ListItem Value="009" Text="2020" />
                                                      <ext:ListItem Value="010" Text="2021" />
                                                      <ext:ListItem Value="011" Text="2022" />
                                                      <ext:ListItem Value="012" Text="2023" />
                                                      <ext:ListItem Value="013" Text="2024" />
                                                    </Items>
                                                   
                                                    <DirectEvents>
                                                        <Select OnEvent = "cboCODE11Q_Click" />
                                                    </DirectEvents>
                                                </ext:ComboBox>

                                                <ext:ComboBox ID="cboCODE11QT" runat="server" FieldLabel="" LabelWidth="70" LabelAlign="Right" Editable="false" EmptyText="" Width="70" PaddingSpec="4 0 4 0" >
                                                   <Items>
                                                      <ext:ListItem Value="001" Text="  " />
                                                      <ext:ListItem Value="002" Text="  " />
                                                      <ext:ListItem Value="003" Text="月" />
                                                      <ext:ListItem Value="004" Text="季" />
                                                      <ext:ListItem Value="005" Text="半年" />
                                                      <ext:ListItem Value="006" Text="年" />                                                     
                                                    </Items>
                                                   
                                                    <DirectEvents>
                                                        <Select OnEvent = "cboCODE11QT_Click" />
                                                    </DirectEvents>
                                                </ext:ComboBox>

                                                <ext:TextField ID="txtRESULT_LOWQ"  runat="server" FieldLabel="自订参考区间" LabelAlign="Right" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" Hidden="true"/>
                                                <ext:TextField ID="txtRESULT_HIGHQ" runat="server" FieldLabel="~" LabelAlign="Right" LabelWidth="20" Width="120" PaddingSpec="4 0 4 0" Hidden="true"/>
                                                <ext:Label ID="Label1Q" runat="server" Text =" " Width="40" PaddingSpec="4 0 4 0" />
                                                <ext:Button ID="btn_Query11Q" runat="server" Icon="Find" Text="查询" Width="80" PaddingSpec="4 0 4 0" >
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Query11Q_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:TextField ID="txtDATE11Q" runat="server" FieldLabel="日期范围"   LabelAlign="Right" PaddingSpec="4 0 4 0" Hidden="true" />
                                                <ext:Button ID="btn_Print11Q"  runat="server" Icon="Find" Text="打印" Width="80" PaddingSpec="4 0 4 0" >
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Print11Q_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container18Q" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:TextField ID="txtRESULT_CODE11Q" runat="server" FieldLabel="检查代码" LabelAlign="Right" LabelWidth="70" Width="170" PaddingSpec="4 0 4 0" Hidden="true"/>
                                                <ext:TextField ID="txtRESULT_NAME11Q" runat="server" FieldLabel="检查名称" LabelAlign="Right" LabelWidth="70" PaddingSpec="4 0 4 0" Width="170" Hidden="true" />
                                                <ext:TextField ID="txtRESULT_UNIT11Q" runat="server" FieldLabel="检查单位" LabelAlign="Right" LabelWidth="100" PaddingSpec="4 0 4 0" Width="200" Hidden="true" />
                                                <ext:TextField ID="txtNORMAL11Q" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" LabelWidth="100" PaddingSpec="4 0 4 0" Width="300"  Hidden="true" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Hidden ID="Hidden13Q" runat="server" />
                                        <ext:GridPanel ID="GridPanel13Q" runat="server" Title="監控指標數據資料" Region="Center" Height="505" PaddingSpec="4 0 4 0" >
                                            <Store>
                                                <ext:Store ID="Store13Q" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model13Q" runat="server" Name="pif_ic111" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="開始日期" />
                                                                <ext:ModelField Name="結束日期" />
                                                                <ext:ModelField Name="医院" />
                                                                <ext:ModelField Name="類別" />
                                                                <ext:ModelField Name="總人數" />
                                                                <ext:ModelField Name="血清白蛋白 a" />
                                                                <ext:ModelField Name="血清白蛋白 b" />
                                                                <ext:ModelField Name="Hb a" />
                                                                <ext:ModelField Name="Hb b" />
                                                                <ext:ModelField Name="鈣 a" />
                                                                <ext:ModelField Name="鈣 b" />
                                                                <ext:ModelField Name="磷 a" />
                                                                <ext:ModelField Name="磷 b" />
                                                                <ext:ModelField Name="轉鐵蛋白a" />
                                                                <ext:ModelField Name="轉鐵蛋白b" />
                                                                <ext:ModelField Name="鐵蛋白SF a" />
                                                                <ext:ModelField Name="鐵蛋白SF b" />
                                                                <ext:ModelField Name="iPTH a" />
                                                                <ext:ModelField Name="iPTH b" />
                                                                <ext:ModelField Name="KT/V a" />
                                                                <ext:ModelField Name="KT/V b" />
                                                                <ext:ModelField Name="住院率" />                                                                
                                                                <ext:ModelField Name="死亡率" />                                                               
                                                                <ext:ModelField Name="廔管重建率" />
                                                                <ext:ModelField Name="B型肝炎表面抗原轉陽率" />
                                                                <ext:ModelField Name="C型肝炎轉陽率" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel13Q" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column88Q" Header="编号"     runat="server"  DataIndex="编号"    Width="35"  Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column89Q" Header="開始日期" runat="server"  DataIndex="開始日期" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column90Q" Header="結束日期" runat="server"  DataIndex="結束日期" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column91Q" Header="医院"     runat="server"  DataIndex="医院"    Width="100"  Cls="x-grid-hd-inner" /> 
                                                    <ext:Column ID="Column98Q" Header="類別"   runat="server"     DataIndex="類別"   Width="70" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column92Q" Header="總人數"    runat="server"  DataIndex="總人數" Width="50"  Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column99Q" Header="血清白蛋白 a" runat="server"  DataIndex="血清白蛋白 a" Width="90"  Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column93Q" Header="血清白蛋白 b" runat="server"  DataIndex="血清白蛋白 b" Width="90"  Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column138" Header="Hb a"  runat="server" DataIndex="Hb a" Width="60" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column139" Header="Hb b"  runat="server" DataIndex="Hb b" Width="60" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column140" Header="鈣 a" runat="server" DataIndex="鈣 a" Width="60" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column141" Header="鈣 b" runat="server" DataIndex="鈣 b" Width="60" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column143" Header="磷 a" runat="server" DataIndex="磷 a" Width="60" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column144" Header="磷 b" runat="server" DataIndex="磷 b" Width="60" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column145" Header="轉鐵蛋白a" runat="server" DataIndex="轉鐵蛋白a" Width="80" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column146" Header="轉鐵蛋白b" runat="server" DataIndex="轉鐵蛋白b" Width="80" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column147" Header="鐵蛋白SF a" runat="server" DataIndex="鐵蛋白SF a" Width="80" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column148" Header="鐵蛋白SF b" runat="server" DataIndex="鐵蛋白SF b" Width="80" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column149" Header="iPTH a" runat="server" DataIndex="iPTH a" Width="70" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column142" Header="iPTH b" runat="server" DataIndex="iPTH b" Width="70" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column150" Header="KT/V a" runat="server" DataIndex="KT/V a" Width="70" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column151" Header="KT/V b" runat="server" DataIndex="KT/V b" Width="70" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column152" Header="住院率" runat="server" DataIndex="住院率" Width="70" Cls="x-grid-hd-inner" />                                                    
                                                    <ext:Column ID="Column154" Header="死亡率" runat="server" DataIndex="死亡率" Width="70" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column157" Header="廔管重建率" runat="server" DataIndex="廔管重建率" Width="80" Cls="x-grid-hd-inner"/>
                                                    <ext:Column ID="Column153" Header="B型肝炎表面抗原轉陽率" runat="server" DataIndex="B型肝炎表面抗原轉陽率" Width="120" Cls="x-grid-hd-inner"/>
                                                    <ext:Column ID="Column155" Header="C型肝炎轉陽率" runat="server" DataIndex="C型肝炎轉陽率" Width="100" Cls="x-grid-hd-inner" tdCls="custom-columnQ" Align="Left" />
                                                </Columns>
                                            </ColumnModel>
                                         

                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar13Q" runat="server">
                                                    <Items>
                                                        <ext:Label ID="Label13Q" runat="server" Text="　　a:代表受检率　　b: 代表合格率"/>
                                                        <ext:ToolbarFill ID="ToolbarFill13Q" runat="server" />
                                                        <ext:Button ID="Button13aQ" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_13Q" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData13Q" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button13bQ" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_13Q" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData13Q" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button13cQ" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_13Q" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData13Q" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                        </ext:GridPanel> 
                                        
                                        <ext:Hidden ID="sYEAR" runat="server" /> 
                                        <ext:Hidden ID="sQT"   runat="server" />                                         

                                        <ext:Hidden ID="Hidden14Q" runat="server" /> 
                                        <ext:GridPanel ID="GridPanel14Q" runat="server" Title="尚未做检验名单" Region="Center" Height="292" PaddingSpec="4 0 4 0" Hidden="true" >
                                            <%-- mark <Store>
                                                <ext:Store ID="Store14Q" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model14Q" runat="server" Name="UNCHECKQ" >
                                                            <Fields>
                                                                <ext:ModelField Name="NO" Type="Int" />
                                                                <ext:ModelField Name="pif_name" />
                                                                <ext:ModelField Name="pv_ic" />
                                                                <ext:ModelField Name="pif_id" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store> --%>

                                            <%-- mark <TopBar>
                                                <ext:Toolbar ID="Toolbar14Q" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill14Q" runat="server" />
                                                        <ext:Button ID="Button14aQ" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_14Q" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData14Q" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button14bQ" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_14Q" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData14Q" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button14cQ" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_14Q" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData14Q" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar> --%>
                                    
                                            <%--mark <ColumnModel ID="ColumnModel14Q" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column94Q" Header="编号"     runat="server" DataIndex="NO"       Width="35"  Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column95Q" Header="姓名"     runat="server" DataIndex="pif_name" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column96Q" Header="身份证号" runat="server" DataIndex="pv_ic"    Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column97Q" Header="病历号"   runat="server" DataIndex="pif_id"   Width="100" Cls="x-grid-hd-inner" />
                                                </Columns>
                                            </ColumnModel> --%>
                                        </ext:GridPanel> 

                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                        <%-- 監控指標查詢E --%>

                        <ext:Panel ID="Panel_12" runat="server" Border="false" Region="Center" ColumnWidth="1" Hidden="true" >
                            <Items>
                                <ext:Panel ID="Panel3" runat="server" Border="false" Region="North" Height="300" >
                                    <Items>
                                        <ext:Container ID="Container4" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:ComboBox ID="ComboBoxGroup" runat="server" FieldLabel="大分类" DisplayField="GROUP_NAME" LabelAlign="Right" EmptyText="选择一个分类">
                                                    <Store>
                                                        <ext:Store ID="Store19" runat="server">
                                                            <Model>
                                                                <ext:Model ID="Model19" runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="GROUP_NAME" />
                                                                        <ext:ModelField Name="GROUP_CODE" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <DirectEvents>
                                                        <Select OnEvent="ChangGroup" />
                                                    </DirectEvents>
                                                </ext:ComboBox>
                                                <ext:ComboBox ID="cboCODE12" runat="server" FieldLabel="检查名称" EmptyText="未选择" LabelWidth="70" LabelAlign="Right"
                                                    Editable="false" Width="220" PaddingSpec="4 0 4 0">
                                                    <DirectEvents>
                                                        <Select OnEvent="cboCODE12_Click" />
                                                    </DirectEvents>
                                                </ext:ComboBox>
                                                <ext:DateField ID="txtBEG_DATE12" runat="server" FieldLabel="日期范围" LabelWidth="70"
                                                    LabelAlign="Right" IndicatorText="　~　" Format="yyyy-MM-dd" Width="208" PaddingSpec="4 0 4 0" />
                                                <ext:DateField ID="txtEND_DATE12" runat="server" LabelAlign="Right" Format="yyyy-MM-dd"
                                                    Width="100" PaddingSpec="4 0 4 0" />
                                                <ext:TextField ID="txtPERSON_NAME12" runat="server" FieldLabel="姓名" LabelAlign="Right"
                                                    LabelWidth="46" Width="166" PaddingSpec="4 0 4 0" IndicatorText="　　" />
                                                <ext:TextField ID="txtDATE12" runat="server" FieldLabel="日期范围" LabelAlign="Right"
                                                    PaddingSpec="4 0 4 0" Hidden="true" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container5" runat="server" Frame="true" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:TextField ID="txtRESULT_CODE12" runat="server" FieldLabel="检查代码" LabelAlign="Right" LabelWidth="70" Width="170" PaddingSpec="4 0 4 0" ReadOnly="true" />
                                                <ext:TextField ID="txtRESULT_NAME12" runat="server" FieldLabel="检查名称" LabelAlign="Right" LabelWidth="70" PaddingSpec="4 0 4 0" Width="200" Hidden="true" />
                                                <ext:TextField ID="txtRESULT_UNIT12" runat="server" FieldLabel="检查单位" LabelAlign="Right" LabelWidth="100" PaddingSpec="4 0 4 0" Width="200" Hidden="true" />
                                                <ext:TextField ID="txtNORMAL12" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" LabelWidth="100" PaddingSpec="4 0 4 0" Width="300" ReadOnly="true" />
                                                <ext:Label ID="Label2" runat="server" Text="　" PaddingSpec="4 0 4 0" Width="109" />
                                                <ext:Button ID="btn_Query12" runat="server" Icon="Find" Text="查询" OnDirectClick="btn_Query12_Click" Width="91" PaddingSpec="4 0 4 0" >
                                                    <%--<DirectEvents>
                                                        <Click OnEvent="btn_Query12_Click" />
                                                    </DirectEvents>--%>
                                                </ext:Button>
                                            </Items>
                                        </ext:Container>
                                        
                                        <ext:Hidden ID="Hidden15" runat="server" />
                                        <ext:GridPanel ID="GridPanel15" runat="server" Title="曲线波动图" Region="Center" Height="260" PaddingSpec="4 0 4 0" >
                                            <Store>
                                                <ext:Store ID="Store15" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model15" runat="server" Name="pif_ic" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="姓名" />
                                                                <ext:ModelField Name="身份证号" />
                                                                <ext:ModelField Name="病历号" />
                                                                <ext:ModelField Name="检验日期" />
                                                                <ext:ModelField Name="检验代码" />
                                                                <ext:ModelField Name="检验名称" />
                                                                <ext:ModelField Name="检验结果" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel15" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column103" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column104" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column105" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" >
                                                        <Commands>
                                                            <ext:ImageCommand CommandName="ZoomIn" Icon="ZoomIn" Style="margin-left:5px !important;" >
                                                                <ToolTip Text="查询" />
                                                            </ext:ImageCommand>
                                                        </Commands>
                                                        <DirectEvents>
                                                            <Command OnEvent="Find_IC" >
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="PAT_IC" Value="record.data.身份证号" Mode="Raw"/>
                                                                </ExtraParams> 
                                                            </Command> 
                                                        </DirectEvents>
                                                    </ext:Column>
                                                    <ext:Column ID="Column106" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column107" Header="检验日期" runat="server" DataIndex="检验日期" Width="100" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column108" Header="检验代码" runat="server" DataIndex="检验代码" Width="60" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column109" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner"  />
                                                    <ext:Column ID="Column110" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" />
                                                </Columns>
                                            </ColumnModel>
                                         
                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar15" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill15" runat="server" />
                                                        <ext:Button ID="Button15a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_15" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData15" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button15b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_15" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData15" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button15c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_15" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData15" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                        </ext:GridPanel> 
                                        
                                    </Items>
                                </ext:Panel>

                                <ext:Panel ID="Panel1" runat="server" Border="false" Region="Center" Height="340" >
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar26" runat="server">
                                            <Items>
                                                <ext:Button ID="Button1" runat="server" Text="Reload Data" Icon="ArrowRefresh" OnDirectClick="ReloadData" Hidden="true" />
                                                <ext:Button ID="Button2" runat="server" Text="保存" Icon="Disk" Handler="saveChart" Hidden="true" />
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Items>

                                        <ext:Chart ID="Chart1" runat="server" Width="700" Height="300" Shadow="true" Animate="true" >
                                            <LegendConfig Position="Right" />
                                            <Store>
                                                <ext:Store ID="StoreC" runat="server" >                           
                                                    <Model>
                                                        <ext:Model ID="ModelC" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="RESULT_DATE" />
                                                                <ext:ModelField Name="RESULT_VALUE_N" />
                                                                <ext:ModelField Name="RESULT_VALUE_L" />
                                                                <ext:ModelField Name="RESULT_VALUE_H" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                                
                                            </Store>
                                            <Axes>
                                                <ext:CategoryAxis Title="检验日期" Fields="RESULT_DATE" Position="Bottom" >
                                                    <Label Fill="#0000FF" Font="12px Arial Unicode MS" />
                                                    <LabelTitle Fill="#0000FF" Font="24px Arial Unicode MS" />
                                                </ext:CategoryAxis>
                                                <ext:NumericAxis  Title="检验结果" Fields="RESULT_VALUE_N,RESULT_VALUE_L,RESULT_VALUE_H" Position="Left" Minimum="0" Maximum="100" >
                                                    <Label Fill="#0185d7" Font="12px Arial Unicode MS" />
                                                    <LabelTitle Fill="#0185d7" Font="24px Arial Unicode MS" />
                                                        <GridConfig>
                                                            <Odd Opacity="1" Fill="#ddd" Stroke="#bbb" StrokeWidth="0.5" />
                                                        </GridConfig>
                                                </ext:NumericAxis>
                                            </Axes>
                                            <Series>
                                                <ext:LineSeries Axis="Left" Titles="达标上限" XField="RESULT_DATE" YField="RESULT_VALUE_H">
                                                    <HighlightConfig Size="7" Radius="7" />
                                                    <MarkerConfig Type="Cross" Size="4" Radius="4" StrokeWidth="0" />
                                                </ext:LineSeries>
                                                <ext:LineSeries Axis="Left" Titles="检验结果" XField="RESULT_DATE" YField="RESULT_VALUE_N" >
                                                    <HighlightConfig Size="7" Radius="7" />
                                                    <MarkerConfig Type="Circle" Size="4" Radius="4" StrokeWidth="0" />
                                                </ext:LineSeries>
                                                <ext:LineSeries Axis="Left" Titles="达标下限" XField="RESULT_DATE" YField="RESULT_VALUE_L">
                                                    <HighlightConfig Size="7" Radius="7" />
                                                    <MarkerConfig Type="Plus" Size="4" Radius="4" StrokeWidth="0" />
                                                </ext:LineSeries>
                                            </Series>
                                        </ext:Chart>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>



                        <ext:Panel ID="Panel_13" runat="server" Border="false" Layout="ColumnLayout" Hidden="False" >
                            <Items>
                               <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" ColumnWidth="1">
                                    <Items> 
                                        <ext:Hidden ID="Hidden16" runat="server" />
                                        <ext:GridPanel ID="GridPanel16" runat="server" Title="耗材总使用数量" ColumnWidth=".45" Height="296" PaddingSpec="0 4 4 0" Visible="False">
                                            <Store>
                                                <ext:Store ID="Store16" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model16" runat="server" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="耗材名称" />
                                                                <ext:ModelField Name="使用数量" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel16" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column125" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column126" Header="耗材名称" runat="server" DataIndex="耗材名称" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column127" Header="使用数量" runat="server" DataIndex="使用数量" Width="70" Cls="x-grid-hd-inner" Align="Right" />
                                                </Columns>
                                            </ColumnModel>
                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar16" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill16" runat="server" />
                                                        <ext:Button ID="Button16a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_16" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData16" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button16b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_16" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData16" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button16c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_16" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData16" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                        </ext:GridPanel> 
                                        
                                        <ext:Hidden ID="Hidden17" runat="server" />
                                        <ext:GridPanel ID="GridPanel17" runat="server" Title="每日耗材使用数量" ColumnWidth=".55" Height="296" PaddingSpec="0 0 4 4" Visible="False">
                                            <Store>
                                                <ext:Store ID="Store17" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model17" runat="server" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="使用日期" />
                                                                <ext:ModelField Name="耗材名称" />
                                                                <ext:ModelField Name="使用数量" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel17" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column128" Header="编号" runat="server" DataIndex="编号" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column129" Header="耗材名称" runat="server" DataIndex="耗材名称" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column130" Header="使用日期" runat="server" DataIndex="使用日期" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column131" Header="使用数量" runat="server" DataIndex="使用数量" Width="70" Cls="x-grid-hd-inner" Align="Right" />
                                                </Columns>
                                            </ColumnModel>
                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar17" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill17" runat="server" />
                                                        <ext:Button ID="Button17a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_17" Icon="PageCode">
                                                            <Listeners>
                                                                <Click Fn="saveData17" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button17b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_17" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData17" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button17c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_17" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData17" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>
                                        </ext:GridPanel> 
                                    </Items>
                                </ext:Container>

                                        <ext:Hidden ID="Hidden18" runat="server" />
                                        <ext:GridPanel ID="GridPanel18" runat="server" Title="當月藥品耗材使用明细"  ColumnWidth="1" Height="350" PaddingSpec="4 0 4 0" Width="500">
                                            <Store>
                                                <ext:Store ID="Store18" runat="server" >
                                                    <Model>
                                                        <ext:Model ID="Model18" runat="server" >
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="身份证号" />
                                                                <ext:ModelField Name="使用人" />
                                                                <ext:ModelField Name="使用日期" />
                                                                <ext:ModelField Name="耗材名称" />
                                                                <ext:ModelField Name="使用数量" />
                                                                <ext:ModelField Name="金额小计" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                    <Reader>
                                                        <ext:ArrayReader />
                                                    </Reader>
                                                </ext:Store>
                                            </Store>
                                            <ColumnModel ID="ColumnModel18" runat="server"  >
                                                <Columns>
                                                    <ext:Column ID="Column132" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column133" Header="藥品耗材名称" runat="server" DataIndex="耗材名称" Width="200" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column134" Header="血透次数" runat="server" DataIndex="使用日期" Width="100" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column135" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Cls="x-grid-hd-inner" Visible="False" />
                                                    <ext:Column ID="Column137" Header="使用人" runat="server" DataIndex="使用人" Width="70" Cls="x-grid-hd-inner" Visible="False" />
                                                    <ext:Column ID="Column136" Header="使用数量" runat="server" DataIndex="使用数量" Width="70" Cls="x-grid-hd-inner" Align="Right" />
                                                    <ext:Column ID="Column156" Header="金额小计" runat="server" DataIndex="金额小计" Width="70" Cls="x-grid-hd-inner" Align="Right" Hidden="True" />
                                                </Columns>
                                            </ColumnModel>
                                            <TopBar>
                                                <ext:Toolbar ID="Toolbar18" runat="server">
                                                    <Items>
                                                        <ext:ToolbarFill ID="ToolbarFill18" runat="server" />
                                <ext:Label ID="Lab_patid" runat="server" Height="30" ColumnWidth=".28" Region="West" Width="200">
                                </ext:Label>
                                <ext:Label ID="Label3" runat="server" Height="30" ColumnWidth=".28" Region="West" Width="100">
                                </ext:Label>
                                <ext:Label ID="Lab_amount" runat="server" Height="30" ColumnWidth=".28" Region="West" Width="200">
                                </ext:Label>
                                                        <ext:Button ID="Button3" runat="server" Text="金额" AutoPostBack="true" OnClick="Amount" Icon="PageExcel" Enabled="True" Hidden="True">
                                                            <Listeners>
                                                                <Click Fn="saveData18" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button18a" runat="server" Text="To XML" AutoPostBack="true" OnClick="ToXml_18" Icon="PageCode" Enabled="True">
                                                            <Listeners>
                                                                <Click Fn="saveData18" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button18b" runat="server" Text="To Excel" AutoPostBack="true" OnClick="ToExcel_18" Icon="PageExcel">
                                                            <Listeners>
                                                                <Click Fn="saveData18" />
                                                            </Listeners>
                                                        </ext:Button>
                                                        <ext:Button ID="Button18c" runat="server" Text="To CSV" AutoPostBack="true" OnClick="ToCsv_18" Icon="PageAttach">
                                                            <Listeners>
                                                                <Click Fn="saveData18" />
                                                            </Listeners>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Toolbar>
                                            </TopBar>

                                        </ext:GridPanel> 
                            </Items>
                        </ext:Panel>
              <ext:TextArea ID="TextArea2" runat="server" EmptyText="" FieldLabel="debug Message" Height="85" Frame="True" AutoScroll="True" Width="550" Visible="False" />
                    </Items>
                </ext:Panel>

            </Items>
        </ext:Viewport>
        <ext:Window 
            ID="Window1" 
            runat="server"  
            Title=""  
            Width = "1000"
            Height = "520"
            Y="0"
            Modal = "true"
            AutoRender = "false"
            Collapsible = "true"
            Maximizable = "true"
            Hidden = "true" >
        </ext:Window>    

        <ext:Window 
            ID="Window2" 
            runat="server"  
            Title=""  
            Width = "100"
            Height = "52"
            Y="0"
            Modal = "true"
            AutoRender = "false"
            Collapsible = "true"
            Maximizable = "true"
            Hidden = "true" >
<%--             <Items>
              <ext:TextArea ID="TextArea1" runat="server" EmptyText=">> Enter a Message Here <<" FieldLabel="Test Message" Height="85" />
            </Items> --%>
            <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>    
    </div>
    </form>
</body>
</html>
