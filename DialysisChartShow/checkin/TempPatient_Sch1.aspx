<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TempPatient_Sch1.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.TempPatient_Sch1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>临时排班</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
        var prepareCommand1 = function (grid, command, record, row) {
            if (command.command == 'Add' && record.data.Week1 != "") {
                command.hidden = true;
                command.hideMode = 'display'; //you can try 'visibility' also
            }
        };
        var prepareCommand2 = function (grid, command, record, row) {
            if (command.command == 'Add' && record.data.Week2 != "") {
                command.hidden = true;
                command.hideMode = 'display'; //you can try 'visibility' also
            }
        };
        var prepareCommand3 = function (grid, command, record, row) {
            if (command.command == 'Add' && record.data.Week3 != "") {
                command.hidden = true;
                command.hideMode = 'display'; //you can try 'visibility' also
            }
        };
        var prepareCommand4 = function (grid, command, record, row) {
            if (command.command == 'Add' && record.data.Week4 != "") {
                command.hidden = true;
                command.hideMode = 'display'; //you can try 'visibility' also
            }
        };
        var prepareCommand5 = function (grid, command, record, row) {
            if (command.command == 'Add' && record.data.Week5 != "") {
                command.hidden = true;
                command.hideMode = 'display'; //you can try 'visibility' also
            }
        };
        var prepareCommand6 = function (grid, command, record, row) {
            if (command.command == 'Add' && record.data.Week6 != "") {
                command.hidden = true;
                command.hideMode = 'display'; //you can try 'visibility' also
            }
        };
        var prepareCommand7 = function (grid, command, record, row) {
            if (command.command == 'Add' && record.data.Week7 != "") {
                command.hidden = true;
                command.hideMode = 'display'; //you can try 'visibility' also
            }
        };
    </script>
    <style type="text/css">
        .blue-label .x-label-value
        {
            color:Blue;
        }
    </style>
</head>
<body >
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" Locale="zh-CN" />
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="sTIME" runat="server" />
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="txtWEEK" runat="server" />
        <ext:Hidden ID="sFLOOR" runat="server" />
        <ext:Hidden ID="sAREA" runat="server" />
        <ext:Hidden ID="sBED_NO" runat="server" />
        <ext:Hidden ID="pat_ic" runat="server" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel12" runat="server" Region="Center" Title="排班表" Header="false">
                    <Items>                    
                        <ext:GridPanel ID="GridPanel1" runat="server" ColumnLines="true" Cls="x-grid-custom" Border="true" EnableColumnMove="false" EnableColumnResize="false" EnableColumnHide="false" Height="660">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server" UI="Default">
                                    <Items>
                                        <ext:ComboBox ID="cboFLOOR" FieldLabel="楼层" runat="server" LabelWidth="60" LabelAlign="Right" Width="150" Cls="Text-blue" >
                                            <DirectEvents>
                                                <Select OnEvent = "Query_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cboArea" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="150" Cls="Text-blue" >
                                            <DirectEvents>
                                                <Select OnEvent = "Query_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:ComboBox ID="cboTIME" FieldLabel="时段" runat="server" LabelWidth="60" LabelAlign="Right" Width="150" Cls="Text-blue" >
                                            <Items>
                                                <ext:ListItem Value="001" Text="上午" />
                                                <ext:ListItem Value="002" Text="下午" />
                                                <ext:ListItem Value="003" Text="晚班" />
                                            </Items>
                                            <DirectEvents>
                                                <Select OnEvent = "Query_Click" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:Button ID="btnReset" runat="server" Text="重置" Icon="Cancel" Width="100" Margins="10 20 10 10">
                                            <DirectEvents>
                                                <Click OnEvent="btnReset_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ComboBox ID="txt_name" runat="server" FieldLabel="姓名" LabelWidth="100" Cls="Text-blue" LabelAlign="Right" IndicatorText="*" IndicatorCls="emptyColor" 
                                                DisplayField="patname" ValueField="patname" TypeAhead="false" HideTrigger="true" MinChars="1" TriggerAction="Query" Width="200">
                                            <ListConfig LoadingText="寻找中...">
                                                <ItemTpl ID="ItemTpl11" runat="server">
                                                    <Html>
                                                        <div>{patname}</div>
                                                    </html>
                                                </ItemTpl>                                       
                                            </ListConfig>
                                            <Store>
                                                <ext:Store ID="Store12" runat="server" AutoLoad="false">
                                                    <Proxy>
                                                        <ext:AjaxProxy Url="../Patinfos.ashx">
                                                            <ActionMethods Read="POST" />
                                                            <Reader>
                                                                <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                            </Reader>
                                                        </ext:AjaxProxy>
                                                    </Proxy>
                                                    <Model>
                                                        <ext:Model ID="Model12" runat="server">
                                                            <Fields>
                                                                <ext:ModelField Name="patic" />
                                                                <ext:ModelField Name="patname" />
                                                            </Fields>
                                                        </ext:Model>
                                                    </Model>
                                                </ext:Store>
                                            </Store>
                                            <DirectEvents>
                                                <Change OnEvent="SetPatient" />
                                            </DirectEvents>
                                        </ext:ComboBox>
                                        <ext:TextField ID="txt_ic" runat="server" FieldLabel="身份证号码" LabelWidth="95" LabelAlign="Right" IndicatorText="*" IndicatorCls="emptyColor" Width="300">
                                            <DirectEvents>
                                                <Change OnEvent="SetPatient" />
                                            </DirectEvents>
                                        </ext:TextField>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server" GroupField="Area">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="BedType">
                                            <Fields>
                                                <ext:ModelField Name="Area" />
                                                <ext:ModelField Name="BedType" />
                                                <ext:ModelField Name="Week1" />
                                                <ext:ModelField Name="Week2" />
                                                <ext:ModelField Name="Week3" />
                                                <ext:ModelField Name="Week4" />
                                                <ext:ModelField Name="Week5" />
                                                <ext:ModelField Name="Week6" />
                                                <ext:ModelField Name="Week7" />
                                                <ext:ModelField Name="Machine" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Column1" runat="server" Text="床/机型" Align="Right" Sortable="false" DataIndex="BedType" Width="100" />
                                    <ext:ImageCommandColumn ID="ImageCommandColumn1" runat="server" Text="周一" Align="Center" Sortable="false" DataIndex="Week1" Width="120">
                                        <Commands>
                                            <ext:ImageCommand Icon="Add" CommandName="Add" Text="空床" />
                                        </Commands>
                                        <DirectEvents>                                   
                                            <Command OnEvent="AddBooking">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw"/>
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw"/>
                                                    <ext:Parameter Name="Week" Value="1" Mode="Raw"/>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                        <PrepareCommand Fn="prepareCommand1" />
                                    </ext:ImageCommandColumn>
                                    <ext:ImageCommandColumn ID="ImageCommandColumn2" runat="server" Text="周二" Align="Center" Sortable="false" DataIndex="Week2" Width="120">
                                        <Commands>
                                            <ext:ImageCommand Icon="Add" CommandName="Add" Text="空床" />
                                        </Commands>
                                        <DirectEvents>                                       
                                            <Command OnEvent="AddBooking">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw"/>
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw"/>
                                                    <ext:Parameter Name="Week" Value="2" Mode="Raw"/>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                        <PrepareCommand Fn="prepareCommand2" />
                                    </ext:ImageCommandColumn>
                                    <ext:ImageCommandColumn ID="ImageCommandColumn3" runat="server" Text="周三" Align="Center" Sortable="false" DataIndex="Week3" Width="120">
                                        <Commands>
                                            <ext:ImageCommand Icon="Add" CommandName="Add" Text="空床" />
                                        </Commands>
                                        <DirectEvents>                                       
                                            <Command OnEvent="AddBooking">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw"/>
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw"/>
                                                    <ext:Parameter Name="Week" Value="3" Mode="Raw"/>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                        <PrepareCommand Fn="prepareCommand3" />
                                    </ext:ImageCommandColumn>
                                    <ext:ImageCommandColumn ID="ImageCommandColumn4" runat="server" Text="周四" Align="Center" Sortable="false" DataIndex="Week4" Width="120">
                                        <Commands>
                                            <ext:ImageCommand Icon="Add" CommandName="Add" Text="空床" />
                                        </Commands>
                                        <DirectEvents>                                       
                                            <Command OnEvent="AddBooking">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw"/>
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw"/>
                                                    <ext:Parameter Name="Week" Value="4" Mode="Raw"/>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                        <PrepareCommand Fn="prepareCommand4" />
                                    </ext:ImageCommandColumn>
                                    <ext:ImageCommandColumn ID="ImageCommandColumn5" runat="server" Text="周五" Align="Center" Sortable="false" DataIndex="Week5" Width="120">
                                        <Commands>
                                            <ext:ImageCommand Icon="Add" CommandName="Add" Text="空床" />
                                        </Commands>
                                        <DirectEvents>                                       
                                            <Command OnEvent="AddBooking">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw"/>
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw"/>
                                                    <ext:Parameter Name="Week" Value="5" Mode="Raw"/>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                        <PrepareCommand Fn="prepareCommand5" />
                                    </ext:ImageCommandColumn>
                                    <ext:ImageCommandColumn ID="ImageCommandColumn6" runat="server" Text="周六" Align="Center" Sortable="false" DataIndex="Week6" Width="120">
                                        <Commands>
                                            <ext:ImageCommand Icon="Add" CommandName="Add" Text="空床" />
                                        </Commands>
                                        <DirectEvents>                                       
                                            <Command OnEvent="AddBooking">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw"/>
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw"/>
                                                    <ext:Parameter Name="Week" Value="6" Mode="Raw"/>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                        <PrepareCommand Fn="prepareCommand6" />
                                    </ext:ImageCommandColumn>
                                    <ext:ImageCommandColumn ID="ImageCommandColumn7" runat="server" Text="周日" Align="Center" Sortable="false" DataIndex="Week7" Width="120">
                                        <Commands>
                                            <ext:ImageCommand Icon="Add" CommandName="Add" Text="空床" />
                                        </Commands> 
                                        <DirectEvents>                                       
                                            <Command OnEvent="AddBooking">
                                                <ExtraParams>
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw"/>
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw"/>
                                                    <ext:Parameter Name="Week" Value="7" Mode="Raw"/>
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                        <PrepareCommand Fn="prepareCommand7" />
                                    </ext:ImageCommandColumn>
                                    <ext:Column ID="Column10" runat="server" Text="机器品牌" Align="Left" Sortable="false" DataIndex="Machine" Width="200" Draggable="false" />
                                </Columns>
                            </ColumnModel>
                            <Features>
                                <ext:Grouping ID="Grouping1" runat="server" HideGroupedHeader="true" GroupHeaderTplString="{name} / {rows.length} 床" />
                            </Features>
                            <Plugins>
                                <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1" />
                            </Plugins>
                            <SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel1" runat="server">
                            </ext:CellSelectionModel>
                        </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>    
        </ext:Viewport>
        <%--視窗部分--%>
        <ext:Window ID="Window1" runat="server" Title="病患查找" Width="600" Height="525" Modal="true" Hidden="true" CloseAction="Hide" UI="Success">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server"> 
                    <Items>
                        <ext:Panel ID="pnlTableLayout" runat="server" Header="false" AutoScroll="true" Cls="Panellogo">
                            <Items>                      
                                <ext:GridPanel ID="GridList" runat="server" Cls="x-grid-custom">
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar2" runat="server">
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
                                        <ext:Store ID="Storew" runat="server" PageSize="10">
                                        <Model>
                                            <ext:Model ID="Modelw" runat="server">
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
                                        <Sorters>
                                            <ext:DataSorter Property="pat_id" Direction="ASC" />
                                        </Sorters>
                                    </ext:Store>
                                    </Store>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                    <ColumnModel ID="ColumnModel2" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="序" Width="50" />
                                            <ext:Column ID="Column2" runat="server" Text="姓名" DataIndex="pif_name" Width="80" />
                                            <ext:Column ID="Column11" runat="server" Text="性别" DataIndex="pif_sex" Width="60" />                                            
                                            <ext:Column ID="Column12" runat="server" Text="出生日期" DataIndex="pif_dob" Width="110" />
                                            <ext:Column ID="Column13" runat="server" Text="身份证号" DataIndex="pat_ic" Width="190" />
                                            <ext:Column ID="Column14" runat="server" Text="经治医生" DataIndex="pif_docname" Region="Center" Width="100" />
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single">                                
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
                                        <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Storew" />
                                    </BottomBar>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
            <DirectEvents>
                <BeforeClose OnEvent="Win_Close" />
            </DirectEvents>
        </ext:Window>
        <%--視窗部分--%>
    </form>
</body>
</html>
