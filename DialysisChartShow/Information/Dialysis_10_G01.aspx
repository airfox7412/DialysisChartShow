<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_G01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_G01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>監控數據查詢</title>
    <script type="text/javascript">

        var saveData13Q = function () {
            App.Hidden13Q.setValue(Ext.encode(App.GridPanel13Q.getRowsValues({ selectedOnly: false })));
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
                <%--品质监控指标查询--%>
                <ext:FormPanel ID="FormPanel_11" runat="server" Border="false" Region="Center" ColumnWidth="1">
                    <Items>
                        <ext:Panel ID="Panel2Q" runat="server" Border="false" Region="North" Height="640" >
                            <Items>
                                <ext:Container ID="Container11Q" runat="server" Frame="true" Layout="ColumnLayout" >
                                    <Items>
                                        <ext:ComboBox ID="cboCODE11Q" runat="server" FieldLabel="年度" LabelWidth="70" LabelAlign="Right" Editable="false" EmptyText="未选择" Width="170" PaddingSpec="4 10 4 10" >
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
                                                <Select OnEvent="cboCODE11Q_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>

                                        <ext:ComboBox ID="cboCODE11QT" runat="server" FieldLabel="" LabelWidth="70" LabelAlign="Right" Editable="false" EmptyText="" Width="70" PaddingSpec="4 10 4 10" >
                                            <Items>
                                                <ext:ListItem Value="001" Text="  " />
                                                <ext:ListItem Value="002" Text="  " />
                                                <ext:ListItem Value="003" Text="月" />
                                                <ext:ListItem Value="004" Text="季" />
                                                <ext:ListItem Value="005" Text="半年" />
                                                <ext:ListItem Value="006" Text="年" />                                                     
                                            </Items>                                                   
                                            <DirectEvents>
                                                <Select OnEvent="cboCODE11QT_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:Button ID="btn_Query11Q" runat="server" Icon="Find" Text="查询" Width="80" PaddingSpec="4 10 4 10" >
                                            <DirectEvents>
                                                <Click OnEvent="btn_Query11Q_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btn_Print11Q"  runat="server" Icon="Find" Text="打印" Width="80" PaddingSpec="4 10 4 10" >
                                            <DirectEvents>
                                                <Click OnEvent="btn_Print11Q_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Container>                          
                                <ext:Hidden ID="Hidden13Q" runat="server" />
                                <ext:GridPanel ID="GridPanel13Q" runat="server" Title="监控指标数据资料查询" Region="Center" Height="505" PaddingSpec="4 0 4 0" >                                        
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
                                    <Store>
                                        <ext:Store ID="Store13Q" runat="server" >
                                            <Model>
                                                <ext:Model ID="Model13Q" runat="server" Name="pif_ic111" >
                                                    <Fields>
                                                        <ext:ModelField Name="编号" Type="Int" />
                                                        <ext:ModelField Name="开始日期" />
                                                        <ext:ModelField Name="结束日期" />
                                                        <ext:ModelField Name="医院" />
                                                        <ext:ModelField Name="类别" />
                                                        <ext:ModelField Name="总人数" />
                                                        <ext:ModelField Name="血清白蛋白 a" />
                                                        <ext:ModelField Name="血清白蛋白 b" />
                                                        <ext:ModelField Name="Hb a" />
                                                        <ext:ModelField Name="Hb b" />
                                                        <ext:ModelField Name="钙 a" />
                                                        <ext:ModelField Name="钙 b" />
                                                        <ext:ModelField Name="磷 a" />
                                                        <ext:ModelField Name="磷 b" />
                                                        <ext:ModelField Name="转铁蛋白a" />
                                                        <ext:ModelField Name="转铁蛋白b" />
                                                        <ext:ModelField Name="铁蛋白SF a" />
                                                        <ext:ModelField Name="铁蛋白SF b" />
                                                        <ext:ModelField Name="iPTH a" />
                                                        <ext:ModelField Name="iPTH b" />
                                                        <ext:ModelField Name="KT/V a" />
                                                        <ext:ModelField Name="KT/V b" />
                                                        <ext:ModelField Name="住院率" />                                                                
                                                        <ext:ModelField Name="死亡率" />                                                               
                                                        <ext:ModelField Name="廔管重建率" />
                                                        <ext:ModelField Name="B型肝炎表面抗原转阳率" />
                                                        <ext:ModelField Name="C型肝炎转阳率" />
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
                                            <ext:Column ID="Column88Q" Header="编号"     runat="server"  DataIndex="编号"    Width="50"  Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column89Q" Header="开始日期" runat="server"  DataIndex="开始日期" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column90Q" Header="结束日期" runat="server"  DataIndex="结束日期" Width="100" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column91Q" Header="医院"     runat="server"  DataIndex="医院"    Width="100"  Cls="x-grid-hd-inner" /> 
                                            <ext:Column ID="Column98Q" Header="类别"   runat="server"     DataIndex="类别"   Width="60" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column92Q" Header="总人数"    runat="server"  DataIndex="总人数" Width="60"  Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column99Q" Header="血清白蛋白 a" runat="server"  DataIndex="血清白蛋白 a" Width="90"  Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column93Q" Header="血清白蛋白 b" runat="server"  DataIndex="血清白蛋白 b" Width="90"  Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column138" Header="Hb a"  runat="server" DataIndex="Hb a" Width="60" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column139" Header="Hb b"  runat="server" DataIndex="Hb b" Width="60" Cls="x-grid-hd-inner"  />
                                            <ext:Column ID="Column140" Header="钙 a" runat="server" DataIndex="钙 a" Width="60" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column141" Header="钙 b" runat="server" DataIndex="钙 b" Width="60" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column143" Header="磷 a" runat="server" DataIndex="磷 a" Width="60" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column144" Header="磷 b" runat="server" DataIndex="磷 b" Width="60" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column145" Header="转铁蛋白a" runat="server" DataIndex="转铁蛋白a" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column146" Header="转铁蛋白b" runat="server" DataIndex="转铁蛋白b" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column147" Header="铁蛋白SF a" runat="server" DataIndex="铁蛋白SF a" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column148" Header="铁蛋白SF b" runat="server" DataIndex="铁蛋白SF b" Width="80" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column149" Header="iPTH a" runat="server" DataIndex="iPTH a" Width="70" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column142" Header="iPTH b" runat="server" DataIndex="iPTH b" Width="70" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column150" Header="KT/V a" runat="server" DataIndex="KT/V a" Width="70" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column151" Header="KT/V b" runat="server" DataIndex="KT/V b" Width="70" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column152" Header="住院率" runat="server" DataIndex="住院率" Width="70" Cls="x-grid-hd-inner" />                                                    
                                            <ext:Column ID="Column154" Header="死亡率" runat="server" DataIndex="死亡率" Width="70" Cls="x-grid-hd-inner" />
                                            <ext:Column ID="Column157" Header="廔管重建率" runat="server" DataIndex="廔管重建率" Width="80" Cls="x-grid-hd-inner"/>
                                            <ext:Column ID="Column153" Header="B型肝炎表面抗原转阳率" runat="server" DataIndex="B型肝炎表面抗原转阳率" Width="120" Cls="x-grid-hd-inner"/>
                                            <ext:Column ID="Column155" Header="C型肝炎转阳率" runat="server" DataIndex="C型肝炎转阳率" Width="110" Cls="x-grid-hd-inner" tdCls="custom-columnQ" Align="Left" />
                                        </Columns>
                                    </ColumnModel>
                                </ext:GridPanel> 
                                        
                                <ext:Hidden ID="sYEAR" runat="server" /> 
                                <ext:Hidden ID="sQT"   runat="server" /> 
                                <ext:Hidden ID="Hidden14Q" runat="server" /> 
                                <ext:GridPanel ID="GridPanel14Q" runat="server" Title="尚未做检验名单" Region="Center" Height="292" PaddingSpec="4 0 4 0" Hidden="true" >                                            
                                </ext:GridPanel> 
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
                <ext:Window ID="Window1" runat="server" Title=""  Width="900" Height="600" Y="10" Modal="true" AutoRender="false" Collapsible="true" Maximizable="true" Hidden="true" >
                    <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window> 
            </Items>
        </ext:Viewport> 
    </form>
</body>
</html>
