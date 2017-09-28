<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_C01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_C01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>自訂義檢驗</title>
    <script type="text/javascript">

        var saveData13 = function () {
            App.Hidden13.setValue(Ext.encode(App.GridPanel13.getRowsValues({ selectedOnly: false })));
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
                <%--自订参考区间--%>
                <ext:FormPanel ID="FormPanel_11" runat="server" Border="false" Region="Center" ColumnWidth="1">
                    <Items>
                        <ext:Panel ID="Panel2" runat="server" Border="false" Region="North" Height="640" >
                            <Items>
                                <ext:Container ID="Container11" runat="server" Frame="true" Layout="ColumnLayout" Padding="5" >
                                    <Items>
                                        <ext:ComboBox ID="ComboBoxGroup" runat="server" FieldLabel="大分类" LabelWidth="70" DisplayField="GROUP_NAME" LabelAlign="Right" EmptyText="选择一个分类" PaddingSpec="4 10 4 10">
                                            <DirectEvents>
                                                <Select OnEvent="ChangGroup" />
                                            </DirectEvents>
                                        </ext:ComboBox>                                                
                                        <ext:ComboBox ID="cboCODE11" runat="server" FieldLabel="检查名称" LabelWidth="70" LabelAlign="Right" Editable="false" EmptyText="未选择" Width="270" PaddingSpec="4 10 4 10">
                                            <DirectEvents>
                                                <Change OnEvent="cboCODE11_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txtRESULT_LOW" runat="server" FieldLabel="自订参考区间" LabelAlign="Right" LabelWidth="100" Width="200" PaddingSpec="4 0 4 10" />
                                        <ext:TextField ID="txtRESULT_HIGH" runat="server" FieldLabel="~" LabelAlign="Right" LabelWidth="20" Width="120" PaddingSpec="4 10 4 0" />
                                        <ext:Button ID="btn_Query11" runat="server" Icon="Find" Text="查询" Width="80">
                                            <DirectEvents>
                                                <Click OnEvent="btn_Query11_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container18" runat="server" Frame="true" Layout="ColumnLayout" >
                                    <Items>
                                        <ext:TextField ID="txtRESULT_CODE11" runat="server" FieldLabel="检查代码" LabelAlign="Right" LabelWidth="70" Width="170" PaddingSpec="4 0 4 0" />
                                        <ext:TextField ID="txtNORMAL11" runat="server" FieldLabel="生物参考区间" LabelAlign="Right" LabelWidth="100" PaddingSpec="4 0 4 0" Width="300" />
                                    </Items>
                                </ext:Container>                                        
                                <ext:Hidden ID="Hidden13" runat="server" />
                                <ext:GridPanel ID="GridPanel13" runat="server" Title="达自订参考区间值名单" Region="Center" Height="505">
                                    <Store>
                                        <ext:Store ID="Store13" runat="server" PageSize="25">
                                            <Model>
                                                <ext:Model ID="Model13" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="姓名" Type="String" />
                                                        <ext:ModelField Name="身份证号" Type="String" />
                                                        <ext:ModelField Name="病历号" Type="Int" />
                                                        <ext:ModelField Name="检验日期" Type="String" />
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
                                    <ColumnModel ID="ColumnModel13" runat="server">
                                        <Columns>
                                            <ext:Column ID="Column88" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column89" Header="姓名" runat="server" DataIndex="姓名" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column90" Header="身份证号" runat="server" DataIndex="身份证号" Width="150" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column91" Header="病历号" runat="server" DataIndex="病历号" Width="60" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column98" Header="检验日期" runat="server" DataIndex="检验日期" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column92" Header="检验代码" runat="server" DataIndex="检验代码" Width="70" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column99" Header="检验名称" runat="server" DataIndex="检验名称" Width="150" Cls="x-grid-hd-inner"  />
                                            <ext:NumberColumn ID="Column93" Header="检验结果" runat="server" DataIndex="检验结果" Width="100" Cls="x-grid-hd-inner" Align="Right" />
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
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar13" runat="server" StoreID="Store13" />
                                    </BottomBar>
                                </ext:GridPanel>                                         
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport> 
    </form>
</body>
</html>
