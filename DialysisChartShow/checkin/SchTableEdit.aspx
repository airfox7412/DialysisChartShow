<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchTableEdit.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.SchTableEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病患排班</title>
    <link href="../css/grid.css" rel="stylesheet"/>

    <script type="text/javascript">
        var change_patcolor = function (value, metadata, record, row, col) {
            if (record.get("pif_kind") == "Y") {
                metadata.style = "color:red;";
            }
            else {
                metadata.style = "color:green;";
            }
            return value;
        };

        var change = function (value, metadata, record, row, col) {
            if (record.get("Kind") == "Y") {
                metadata.style = "color:red;";
            }
            else {
                metadata.style = "color:green;";
            }
            return value;
        };

        var myRenderer1 = function (value, metadata) {
            if (App.sWEEK.value === "1") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer2 = function (value, metadata) {
            if (App.sWEEK.value === "2") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer3 = function (value, metadata) {
            if (App.sWEEK.value === "3") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer4 = function (value, metadata) {
            if (App.sWEEK.value === "4") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer5 = function (value, metadata) {
            if (App.sWEEK.value === "5") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };

        var myRenderer6 = function (value, metadata) {
            if (App.sWEEK.value === "6") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };
        var myRenderer7 = function (value, metadata) {
            if (App.sWEEK.value === "7") {
                if (value.IsChanged) {
                    metadata.style = "color:red;";
                } else {
                    metadata.style = "color:blue;";
                }
            }

            if (value != null) {
                if (value.Name.indexOf("ST") > -1) {
                    metadata.style = "color:green;";
                    value.Name = ChgStr(value.Name);
                }
                return value.Name;
            } else {
                return "";
            }
        };

        function ChgStr(value) {
            var PatName = value.replace(/ST/, " (s)");
            return PatName;
        }

        var prepareCellCommand = function (grid, command, record, row, col, value) {
            if (col > parseInt(App.sWEEK.value) - 2) {
                if (value.Name.indexOf("ST") > -1) {
                    if (command.command == 'Add') {
                        command.iconCls = "";
                    }
                    if (command.command == 'Delete') {
                        command.iconCls = "";
                    }
                }
                else {
                    if (command.command == 'Add' && value.Name != '') {
                        command.iconCls = "icon-Cancle";
                    }
                    if (command.command == 'Delete' && value.Name == '') {
                        command.iconCls = "icon-Add";
                    }
                }
            }
            else {
                if (command.command == 'Add' || command.command == 'Delete') {
                    command.iconCls = "icon-Add";
                }
            }
        };
    </script>
    <script src="<%=ResolveClientUrl("~/Scripts/checkin/SchTable.js") %>" language="javascript" type="text/javascript"></script>
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

        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel12" runat="server" Region="Center" Title="排班表" Header="true" AutoScroll="true">
                    <Items>                    
                        <ext:GridPanel ID="GridPanel1" runat="server" EnableColumnMove="false" EnableColumnResize="false" EnableColumnHide="false" Border="true" 
                            ColumnLines="true" Cls="x-grid-custom" Width="1200" Height="600">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server" UI="Default">
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
                                        <ext:Button ID="Button1" runat="server" Text="完成并返回" Icon="ClockEdit" Width="150">
                                            <DirectEvents>
                                                <Click OnEvent="btnBack_Click">
                                                    <EventMask ShowMask="true" Msg="载入中,请稍待..." MinDelay="300" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server" GroupField="Area">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="BedType">
                                            <Fields>
                                                <ext:ModelField Name="Floor" />
                                                <ext:ModelField Name="Area" />
                                                <ext:ModelField Name="BedType" />
                                                <ext:ModelField Name="BedNo" />
                                                <ext:ModelField Name="MachineType" />
                                                <ext:ModelField Name="Week1" Type="Object" />
                                                <ext:ModelField Name="Week2" Type="Object" />
                                                <ext:ModelField Name="Week3" Type="Object" />
                                                <ext:ModelField Name="Week4" Type="Object" />
                                                <ext:ModelField Name="Week5" Type="Object" />
                                                <ext:ModelField Name="Week6" Type="Object" />
                                                <ext:ModelField Name="Week7" Type="Object" />
                                                <ext:ModelField Name="Machine" />
                                                <ext:ModelField Name="TimeType" />
                                                <ext:ModelField Name="Kind" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Column1" runat="server" Text="床/机型" Align="Right" Sortable="false" DataIndex="BedType" Width="100" Locked="true">
                                        <Renderer Fn="change" />
                                    </ext:Column>
                                    <ext:Column ID="Column3" runat="server" Text="周一" Align="Center" Sortable="false" DataIndex="Week1" Width="130">
                                        <Renderer Fn="myRenderer1" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="Add" Icon="Add" />
                                            <ext:ImageCommand CommandName="Delete" Icon="Cancel" />
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                        <DirectEvents>
                                            <Command OnEvent="DeletePatient">
                                                <ExtraParams>                                         
                                                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw" />
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw" />
                                                    <ext:Parameter Name="Week" Value="1" Mode="Raw"/>
                                                    <ext:Parameter Name="PatientId" Value="record.get('Week1').Id" Mode="Raw" />                                         
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" Text="周二" Align="Center" Sortable="false" DataIndex="Week2" Width="130">
                                        <Renderer Fn="myRenderer2" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="Add" Icon="Add" />
                                            <ext:ImageCommand CommandName="Delete" Icon="Cancel" />
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                        <DirectEvents>
                                            <Command OnEvent="DeletePatient">
                                                <ExtraParams>                                         
                                                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw" />
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw" />
                                                    <ext:Parameter Name="Week" Value="2" Mode="Raw"/>
                                                    <ext:Parameter Name="PatientId" Value="record.get('Week2').Id" Mode="Raw" />                                         
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column5" runat="server" Text="周三" Align="Center" Sortable="false" DataIndex="Week3" Width="130">
                                        <Renderer Fn="myRenderer3" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="Add" Icon="Add" />
                                            <ext:ImageCommand CommandName="Delete" Icon="Cancel" />
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                        <DirectEvents>
                                            <Command OnEvent="DeletePatient">
                                                <ExtraParams>                                         
                                                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw" />
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw" />
                                                    <ext:Parameter Name="Week" Value="3" Mode="Raw"/>
                                                    <ext:Parameter Name="PatientId" Value="record.get('Week3').Id" Mode="Raw" />                                         
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" Text="周四" Align="Center" Sortable="false" DataIndex="Week4" Width="130">
                                        <Renderer Fn="myRenderer4" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="Add" Icon="Add" />
                                            <ext:ImageCommand CommandName="Delete" Icon="Cancel" />
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                        <DirectEvents>
                                            <Command OnEvent="DeletePatient">
                                                <ExtraParams>                                         
                                                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw" />
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw" />
                                                    <ext:Parameter Name="Week" Value="4" Mode="Raw"/>
                                                    <ext:Parameter Name="PatientId" Value="record.get('Week4').Id" Mode="Raw" />                                         
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column7" runat="server" Text="周五" Align="Center" Sortable="false" DataIndex="Week5" Width="130">
                                        <Renderer Fn="myRenderer5" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="Add" Icon="Add" />
                                            <ext:ImageCommand CommandName="Delete" Icon="Cancel" />
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                        <DirectEvents>
                                            <Command OnEvent="DeletePatient">
                                                <ExtraParams>                                         
                                                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw" />
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw" />
                                                    <ext:Parameter Name="Week" Value="5" Mode="Raw"/>
                                                    <ext:Parameter Name="PatientId" Value="record.get('Week5').Id" Mode="Raw" />                                         
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column8" runat="server" Text="周六" Align="Center" Sortable="false" DataIndex="Week6" Width="130">
                                        <Renderer Fn="myRenderer6" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="Add" Icon="Add" />
                                            <ext:ImageCommand CommandName="Delete" Icon="Cancel" />
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                        <DirectEvents>
                                            <Command OnEvent="DeletePatient">
                                                <ExtraParams> 
                                                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw" />
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw" />
                                                    <ext:Parameter Name="Week" Value="6" Mode="Raw"/>
                                                    <ext:Parameter Name="PatientId" Value="record.get('Week6').Id" Mode="Raw" />                                         
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column9" runat="server" Text="周日" Align="Center" Sortable="false" DataIndex="Week7" Width="130">
                                        <Renderer Fn="myRenderer7" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="Add" Icon="Add" />
                                            <ext:ImageCommand CommandName="Delete" Icon="Cancel" />
                                        </Commands>
                                        <PrepareCommand Fn="prepareCellCommand" />
                                        <DirectEvents>
                                            <Command OnEvent="DeletePatient">
                                                <ExtraParams>                                         
                                                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                                                    <ext:Parameter Name="Area" Value="record.data.Area" Mode="Raw" />
                                                    <ext:Parameter Name="BedType" Value="record.data.BedType" Mode="Raw" />
                                                    <ext:Parameter Name="Week" Value="7" Mode="Raw"/>
                                                    <ext:Parameter Name="PatientId" Value="record.get('Week7').Id" Mode="Raw" />                                         
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Column10" runat="server" Text="机器品牌" Align="Left" Sortable="false" DataIndex="Machine" Flex="1" />
                                </Columns>
                            </ColumnModel>
                            <Features>
                                <ext:Grouping ID="Grouping1" runat="server" HideGroupedHeader="true" GroupHeaderTplString="{name}区 / {rows.length} 床" />
                            </Features>
                            <SelectionModel>
                                <ext:RowSelectionModel runat="server" SingleSelect="true" ID="RowSelectionModel1" />
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
                                        <ext:Toolbar ID="Toolbar1" runat="server">
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
                                                    <ext:ModelField Name="pif_kind" />
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
                                            <ext:Column ID="Column2" runat="server" Text="姓名" DataIndex="pif_name" Width="80">
                                                <Renderer Fn="change_patcolor" />
                                            </ext:Column>
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
