<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Pat_info_list.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Pat_info_list" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化病例信息登记系统</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        var enterKeyPressHandler = function (f, e) {
            if (e.getKey() == e.ENTER) {
                Ext.Net.DirectMethods.show_history();
                e.stopEvent();
            }
        };
        var myRenderer = function (value, metadata) {
            if (value === "") {
                metadata.style = "color : red;"
            }
            return value;
        };

        var prepareCellCommand = function (grid, command, record, row, col) {
            if (command.command == 'BiochemicalIndicators' && record.get("txt_101") == "I") { //txt_101=生化指标
                command.iconCls = "icon-moneyeuro";
            }
        };
    </script>
    <style type="text/css">
    .x-panel-default
    {
        background: #1e5799; /* Old browsers */
        background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
        background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
        background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0 ); /* IE6-9 */ 
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN" Theme="Triton" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="pnlTableLayout" runat="server" Header="true">
                    <TopBar>
                        <ext:Toolbar ID="Toolbar1" runat="server">
                            <Items>
                                <ext:ComboBox ID="Text_Name" runat="server" FieldLabel="姓名" LabelWidth="50" Width="150" Cls="Text-blue" LabelAlign="Right"
                                    DisplayField="patname" ValueField="patname" TypeAhead="false" HideTrigger="true" MinChars="1" TriggerAction="Query">
                                    <ListConfig LoadingText="寻找中...">
                                        <ItemTpl ID="ItemTpl1" runat="server">
                                            <Html>
                                                <div>{patname}</div>
                                            </html>
                                        </ItemTpl>                                       
                                    </ListConfig>
                                    <Store>
                                        <ext:Store ID="Store2" runat="server" AutoLoad="false">
                                            <Proxy>
                                                <ext:AjaxProxy Url="../Patinfos.ashx">
                                                    <ActionMethods Read="POST" />
                                                    <Reader>
                                                        <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                    </Reader>
                                                </ext:AjaxProxy>
                                            </Proxy>
                                            <Model>
                                                <ext:Model ID="Model2" runat="server">
                                                    <Fields>
                                                        <ext:ModelField Name="patic" />
                                                        <ext:ModelField Name="patname" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                        </ext:Store>
                                    </Store>
                                </ext:ComboBox>
                                <ext:TextField ID="Text_ID" runat="server" FieldLabel="身份证号" LabelWidth="100" LabelAlign="Right" Width="250" /> 
                                <ext:SelectBox ID="Cbo_Gender" runat="server" FieldLabel="性别" LabelWidth="60" LabelAlign="Right" Width="150">
                                    <Items>
                                        <ext:ListItem Value="M" Text="男" />
                                        <ext:ListItem Value="F" Text="女" />
                                    </Items>
                                </ext:SelectBox>                                 
                                <ext:Button ID="btn_Query" runat="server" Icon="Find" Text="历史病患" Width="100" MarginSpec="10 10 5 5">
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_Query1" runat="server" Icon="Find" Text="当前病患" Width="100" MarginSpec="10 10 5 5">
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query1_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_Query3" runat="server" Icon="Find" Text="未检测名单查詢" TextAlign="Center" Width="150" MarginSpec="10 10 5 5">
                                    <DirectEvents>
                                        <Click OnEvent="ShowBioNotCheckedList">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>                                                                    
                                <ext:Button ID="Button1" runat="server" Icon="Find" Text="统计" ColumnWidth=".1" Hidden="true" MarginSpec="10 10 5 5">
                                    <DirectEvents>
                                        <Click OnEvent="btn_Statistics_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button> 
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>                      
                        <ext:GridPanel ID="GridList" runat="server" Cls="x-grid-custom" Header="true">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15">
                                <Model>
                                    <ext:Model ID="Model1" runat="server">
                                        <Fields>
                                            <ext:ModelField Name="pat_id" />
                                            <ext:ModelField Name="pif_name" />
                                            <ext:ModelField Name="pif_sex" />
                                            <ext:ModelField Name="pif_dob" />
                                            <ext:ModelField Name="pat_ic" />
                                            <ext:ModelField Name="txt_10" />
                                            <ext:ModelField Name="FirstDate" />
                                            <ext:ModelField Name="InfoDate" />
                                            <ext:ModelField Name="txt_101" />
                                            <ext:ModelField Name="opt_52" />
                                            <ext:ModelField Name="info_date" />    
                                            <ext:ModelField Name="BIO_NotChecked" />                                                       
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
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="Column1" runat="server" Text="序" Width="70" />
                                    <ext:Column ID="Column3" runat="server" Text="姓名" DataIndex="pif_name" Width="80" />
                                    <ext:Column ID="Column4" runat="server" Text="性别" DataIndex="pif_sex" Width="60" />                                            
                                    <ext:Column ID="Column5" runat="server" Text="出生日期" DataIndex="pif_dob" Width="110" />
                                    <ext:Column ID="Column6" runat="server" Text="身份证号" DataIndex="pat_ic" Width="190" />                                            
                                    <ext:Column ID="Column7" runat="server" Text="血/腹" DataIndex="txt_10" Width="60" />
                                    <ext:Column ID="Column9" runat="server" Text="首次透析日期" DataIndex="FirstDate" Width="120" />
                                    <ext:Column ID="Column10" runat="server" Text="本院透析日期" DataIndex="InfoDate" Width="120" />
                                    <ext:Column ID="Column11" runat="server" Text="生化指标" DataIndex="txt_101" Width="90" RightCommandAlign="false">
                                        <Commands>
                                            <ext:ImageCommand Icon="ChartLine" CommandName="BiochemicalIndicators" >
                                                <ToolTip Text="超过或低于标准参考值" />
                                            </ext:ImageCommand>
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                        <DirectEvents>
                                            <Command OnEvent="BioIndicators">
                                                <ExtraParams>
                                                    <ext:Parameter Name="pat_id" Value="record.data.pat_id" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column12" runat="server" Text="转归情形" DataIndex="opt_52" Width="100" />
                                    <ext:Column ID="Column13" runat="server" Text="转归日期" DataIndex="info_date" Width="100" />
                                    <ext:Column ID="Column14" runat="server" Text="耗材" Width="60" RightCommandAlign="false">
                                        <Commands>
                                            <ext:ImageCommand CommandName="BiochemicalIndicators" Icon="BoxError">
                                                <ToolTip Text="当月使用药品耗材查询" />
                                            </ext:ImageCommand> 
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                            <DirectEvents>
                                            <Command OnEvent="Dialysis_13">
                                                <ExtraParams>
                                                    <ext:Parameter Name="pat_ic" Value="record.data.pat_ic" Mode="Raw">
                                                    </ext:Parameter>
                                                    <ext:Parameter Name="pif_name" Value="record.data.pif_name" Mode="Raw">
                                                    </ext:Parameter>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column15" runat="server" Text="经治医生" DataIndex="pif_docname" RightCommandAlign="false" Flex="1">
                                        <Commands>
                                            <ext:ImageCommand CommandName="DoctorHelp" Icon="BookRed">
                                                <ToolTip Text="临床小帮手" />
                                            </ext:ImageCommand> 
                                        </Commands>                                                
                                        <PrepareCommand Fn="prepareCellCommand" />
                                            <DirectEvents>
                                            <Command OnEvent="Dialysis_help">
                                                <ExtraParams>
                                                    <ext:Parameter Name="pat_id" Value="record.data.pat_id" Mode="Raw">
                                                    </ext:Parameter>
                                                    <ext:Parameter Name="pat_ic" Value="record.data.pat_ic" Mode="Raw">
                                                    </ext:Parameter>
                                                    <ext:Parameter Name="pif_name" Value="record.data.pif_name" Mode="Raw">
                                                    </ext:Parameter>
                                                    <ext:Parameter Name="pif_sex" Value="record.data.pif_sex" Mode="Raw">
                                                    </ext:Parameter>
                                                    <ext:Parameter Name="pif_docname" Value="record.data.pif_docname" Mode="Raw">
                                                    </ext:Parameter>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
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
                                                <ext:Parameter Name="pat_ic" Value="record.data.pat_ic" Mode="Raw">
                                                </ext:Parameter>
                                                <ext:Parameter Name="pif_name" Value="record.data.pif_name" Mode="Raw">
                                                </ext:Parameter>
                                                <ext:Parameter Name="pif_sex" Value="record.data.pif_sex" Mode="Raw">
                                                </ext:Parameter>
                                                <ext:Parameter Name="pif_docname" Value="record.data.pif_docname" Mode="Raw">
                                                </ext:Parameter>
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store1" />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>

                <ext:Window ID="Window1" runat="server" Title="生化指标" Width="600" Height="400" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
                <ext:Window ID="Window2" runat="server" Title="" Width="600" Height="400" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
                <ext:Window ID="Window3" runat="server" Title="" Width="770" Height="600" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader3" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
                <ext:Window ID="Window4" runat="server" Title="當月藥品耗材使用明细" Width="600" Height="500" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader4" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
            </Items>    
        </ext:Viewport>
    </form>
</body>
</html>

