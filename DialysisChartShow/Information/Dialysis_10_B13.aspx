<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_B13.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_B13" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>HBsAg转阳率</title>
    <script type="text/javascript">
        var saveData10 = function () {
            App.Hidden10.setValue(Ext.encode(App.GridPanel10.getRowsValues({ selectedOnly: false })));
        };

        var saveData11 = function () {
            App.Hidden11.setValue(Ext.encode(App.GridPanel11.getRowsValues({ selectedOnly: false })));
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

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />     
        <ext:Viewport ID="Viewport1" runat="server">
            <Items>
                <ext:Panel ID="Panel_9" runat="server" Border="false" Region="Center" ColumnWidth="1">
                    <Items>
                        <ext:TextField ID="txtDATE9" runat="server" FieldLabel="日期范围" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                        <ext:TextField ID="txtRESULT_CODE3" runat="server" FieldLabel="检查代码" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                        <ext:TextField ID="txtRESULT_NAME3" runat="server" FieldLabel="检查名称" LabelAlign="Right" PaddingSpec="4 0 4 0" />
                        <ext:TextField ID="txtRESULT_UNIT3" runat="server" FieldLabel="检查单位" LabelAlign="Right" Hidden="true" PaddingSpec="4 0 4 0" />
                        <ext:TextField ID="txtNORMAL3" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" PaddingSpec="4 0 4 0" />
                        <ext:TextField ID="txtTOTAL9" runat="server" FieldLabel="受检人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" />
                        <ext:TextField ID="txtPOSITIVE" runat="server" FieldLabel="阳性人数" LabelAlign="Right" IndicatorText="人" LabelWidth="100" Width="200" PaddingSpec="4 0 4 0" />
                        <ext:Hidden ID="Hidden10" runat="server" /><%--阳性名单--%>  
                        <ext:GridPanel ID="GridPanel10" runat="server" Title="阳性名单" Region="Center" Height="200" PaddingSpec="4 0 4 0" >
                            <Store>
                                <ext:Store ID="Store10" runat="server" >
                                    <Model>
                                        <ext:Model ID="Model10" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="姓名" Type="String" />
                                                <ext:ModelField Name="身份证号" Type="String" />
                                                <ext:ModelField Name="病历号" Type="Int" />
                                                <ext:ModelField Name="检验代码" Type="String" />
                                                <ext:ModelField Name="检验名称" Type="String" />
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
                            <ColumnModel ID="ColumnModel10" runat="server"  >
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" Text="编号" runat="server" Width="50" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column77" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column78" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column79" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column80" Header="检验代码" runat="server" DataIndex="检验代码" Width="70" Cls="x-grid-hd-inner"  />
                                    <ext:Column ID="Column101" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner"  />
                                    <ext:NumberColumn ID="Column81" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" Align="Right" />
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
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar10" runat="server" StoreID="Store10" />
                            </BottomBar>
                        </ext:GridPanel> 
                                
                        <ext:Hidden ID="Hidden11" runat="server" /><%--受检名单--%>
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
                                                <ext:ModelField Name="检验结果" Type="Float" />
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
                                    <ext:Column ID="Column82" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column83" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column84" Header="身份证号" runat="server" DataIndex="身份证号" Width="200" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column85" Header="病历号" runat="server" DataIndex="病历号" Width="100" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column86" Header="检验代码" runat="server" DataIndex="检验代码" Width="70" Cls="x-grid-hd-inner"  />
                                    <ext:Column ID="Column102" Header="检验名称" runat="server" DataIndex="检验名称" Width="100" Cls="x-grid-hd-inner"  />
                                    <ext:NumberColumn ID="Column87" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" Align="Right" />
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
            </Items>
        </ext:Viewport> 
    </form>
</body>
</html>
