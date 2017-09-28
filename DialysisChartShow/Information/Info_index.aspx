<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Info_index.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Info_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化病例信息登记系统</title>
    <style type="text/css">
    .x-grid-custom .x-grid-row-alt .x-grid-cell {
            background-color : #DAE2E8;
            <%--background-color : white;--%>
        }
    </style>
    <script>
        var myRenderer = function (value, metadata) {
            if (value === "") {
                metadata.style = "color : red;"
            }
            return value;
        };

//        var getRowClass = function (record, index, rowParams, store) {
//            if (record.get("info_date") === "") {
//                return "my-highlighted-row";
//            }
//        };

        var prepareCellCommand = function (grid, command, record, row, col, value) {
            if (command.command == 'BiochemicalIndicators' && record.get("txt_101") == "I") {
                command.iconCls = "icon-moneyeuro";
            } 
            else if (command.command == 'DoctorHelp' && record.get("txt_101") == "I") {
                command.iconCls = "icon-moneyeuro";
            }
        };


    </script>
    <style type="text/css">
        .my-highlighted-row .x-grid-cell {
            background-color: pink !important;
        }
    </style>

</head>
<body style="height: 208px">
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="AbsoluteLayout">
            <Items>
                
                        <ext:Panel ID="Panel2" runat="server" Region="North" Border="false" Height="110" Layout="ColumnLayout">
                            <Items>
                                <ext:Container ID="Container4" runat="server"  ColumnWidth="1" >
                            <Items>
                                <ext:Image ID="Image1" runat="server" ImageUrl="logo001Big1.jpg" Height="80" ColumnWidth="1">
                                </ext:Image>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" ColumnWidth="1" Height="20">
                            <Items>
                                
                                <ext:TextField ID="Text_Name" runat="server" FieldLabel="姓名" LabelAlign="Right" LabelWidth="60"
                                    ColumnWidth=".15">
                                </ext:TextField>
                                <ext:SelectBox ID="Cbo_Gender" FieldLabel="性别" runat="server" LabelWidth="60" LabelAlign="Right"
                                    ColumnWidth=".15">
                                    <Items>
                                        <ext:ListItem Value="M" Text="男" />
                                        <ext:ListItem Value="F" Text="女" />
                                    </Items>
                                </ext:SelectBox>
                                <%--<ext:TextField ID="Text_" runat="server" LabelWidth="50" LabelAlign="Right" FieldLabel="病历号">
                                </ext:TextField>--%>
                                <ext:TextField ID="Text_ID" runat="server" LabelWidth="60" LabelAlign="Right" FieldLabel="身份证号"
                                    ColumnWidth=".15">
                                </ext:TextField>
                                
                                <ext:Label ID="Label1" runat="server" ColumnWidth =".05" />
                                   
                                <ext:Button ID="btn_Query" runat="server" Icon="Find" Text="历史病患" ColumnWidth=".1" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>                                 

                                <ext:Button ID="btn_Query1" runat="server" Icon="Find" Text="当前病患" ColumnWidth=".1" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Query1_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>     
                                                                                       
                                <ext:Button ID="Button1" runat="server" Icon="Find" Text="统计" ColumnWidth=".1" Hidden="true" >
                                    <DirectEvents>
                                        <Click OnEvent="btn_Statistics_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>

                                <ext:Button ID="btn_Query3" runat="server" Icon="Find" Text="未检测名单查詢"  ColumnWidth=".1">
                                    <DirectEvents>
                                        <Click OnEvent="ShowBioNotCheckedList">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>    

                                <ext:TextField ID="txtURL" runat="server" ColumnWidth=".2" />
                                <ext:TextField ID="txtUSER" runat="server" ColumnWidth=".1" />
                            </Items>
                        </ext:Container>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="pnlTableLayout" runat="server" Border="false" Y="110" Layout="fit" AnchorHorizontal="100%" AnchorVertical="100%">
                            <Items>
                            <ext:Panel ID="Panel3" runat="server" AutoScroll="true"  >
                            <Items>
                        
                        
                        
                                <ext:GridPanel ID="GridList" runat="server" Padding="3" Height="540"  ColumnWidth="1"  Cls="x-grid-custom">
                                    <Store>
                                        <ext:Store ID="Store1" runat="server" PageSize="20" OnReadData="MyData_Refresh">
                                            <Model>
                                                <ext:Model ID="Model3" runat="server" Name="recordlist2">
                                                    <Fields>
                                                        <ext:ModelField Name="序号" />
                                                        <ext:ModelField Name="pat_id" Type="String" />
                                                        <ext:ModelField Name="pif_name" Type="String" />
                                                        <ext:ModelField Name="pif_sex" Type="String" />
                                                        <ext:ModelField Name="pif_dob"  Type="String" />
                                                        <ext:ModelField Name="pat_ic" Type="String" />
                                                        <ext:ModelField Name="txt_10" Type="String" />
                                                        <ext:ModelField Name="next_visit_date" Type="String" />
                                                        <ext:ModelField Name="dat_9" Type="String" />
                                                        <ext:ModelField Name="info_date1" Type="String" />
                                                        <ext:ModelField Name="txt_101" Type="String" />
                                                        <ext:ModelField Name="opt_52"      Type="String" />
                                                        <ext:ModelField Name="info_date" Type="String" />    
                                                        <ext:ModelField Name="BIO_NotChecked" Type="String" />                                                       
                                                        <ext:ModelField Name="pif_docname" Type="String" />                                                       
                                                        <ext:ModelField Name="info_survey_date" Type="String" />
                                                        <ext:ModelField Name="info_date" Type="String" />
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
                                    <ColumnModel ID="ColumnModel3" runat="server">
                                        <Columns>
                                            
                                            <ext:Column ID="Column7" runat="server" Text="序号"       DataIndex="序号"     Width="45" Visible="true" />
                                            <ext:Column ID="Column8" runat="server" Text="pat_id"     DataIndex="pat_id"   Width="35" Visible="false"/>
                                            <ext:Column ID="Column1" runat="server" Text="姓名"       DataIndex="pif_name" Width="100" />
                                            <ext:Column ID="Column2" runat="server" Text="性别"       DataIndex="pif_sex"  Width="40" />                                            
                                            <ext:Column ID="Column3" runat="server" Text="出生日期"   DataIndex="pif_dob"  Width="80" />
                                            <ext:Column ID="Column4" runat="server" Text="身份证号"   DataIndex="pat_ic"   Width="150" />                                            
                                            <ext:Column ID="Column10" runat="server" Text="血透/腹透" DataIndex="txt_10"   Width="60" />
                                            <ext:Column ID="Column16" runat="server" Text="下次随访日期" DataIndex="next_visit_date"        Width="100" />
                                            <ext:Column ID="Column12" runat="server" Text="首次透析日期" DataIndex="dat_9"        Width="100" />
                                            <ext:Column ID="Column11" runat="server" Text="本院透析日期" DataIndex="info_date1"   Width="100" />
                                            <ext:Column ID="Column15" runat="server" Text="生化指标" Width="60" DataIndex="txt_101">
                                                <Commands>
                                                    <ext:ImageCommand Icon="ChartLine" CommandName="BiochemicalIndicators" >
                                                        <ToolTip Text="超过或低于标准参考值" />
                                                    </ext:ImageCommand>
                                                </Commands>
                                                <PrepareCommand Fn="prepareCellCommand" />
<%--                                                
                                                <Listeners>
                                                    <Command Handler="Ext.Msg.alert(command, record.data.pif_name);" />
                                                </Listeners>
--%>
                                                <DirectEvents>
                                                    <Command OnEvent="BioIndicators">
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
                                            <ext:Column ID="Column13" runat="server" Text="转归情形"     DataIndex="opt_52"        Width="70" />
                                            <ext:Column ID="Column14" runat="server" Text="转归日期" DataIndex="info_date" Width="100" />
                                            <ext:Column ID="Column17" runat="server" Text="耗材" Width="40" DataIndex="" Region="Center">
                                                <Commands>
                                                    <ext:ImageCommand CommandName="BiochemicalIndicators" Icon="BoxError">
                                                         <ToolTip Text="当月使用药品耗材查询" />
                                                    </ext:ImageCommand> 
                                                </Commands>
                                                <PrepareCommand Fn="prepareCellCommand" />
                                                    <DirectEvents>
                                                    <Command OnEvent="Dialysis_13">
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
                                            <ext:Column ID="Column5" runat="server" Text="经治医生" DataIndex="pif_docname" Width="70">
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
                                            <ext:Column ID="Column6" runat="server" Text="调查日期" DataIndex="info_survey_date" Width="150" hidden="true"/>
                                            <ext:Column ID="Column9" runat="server" Text="DAT" DataIndex="info_date" Width="150" visible = "false">
                                            <Renderer Fn="myRenderer"></Renderer>
                                            </ext:Column>
                                            <ext:CommandColumn ID="CommandColumn1" runat="server" Width="90">
                                                <Commands>
                                                    <ext:GridCommand Icon="Table" CommandName="Dialysis" Text="查看信息 " />
                                                </Commands>
                                                <DirectEvents>
                                                    <Command OnEvent="Dialysis">
                                                        <EventMask ShowMask="true" Msg="处理中….." Target="CustomTarget" CustomTarget="#{FormPanel1}" />
                                                        
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
                                            </ext:CommandColumn>
                                            <ext:CommandColumn ID="CommandColumn2" runat="server" Width="50" Hidden="true" >
                                                <Commands>
                                                    <ext:GridCommand Icon="Delete" CommandName="Delete" Text="删除" />
                                                </Commands>
                                                <DirectEvents>
                                                    <Command OnEvent="Delete">
                                                        <EventMask ShowMask="true" Msg="处理中….." Target="CustomTarget" CustomTarget="#{FormPanel1}" />
                                                        <Confirmation ConfirmRequest="true" Title="提示" Message="确定要删除吗??">
                                                        </Confirmation>
                                                        <ExtraParams>
                                                            <ext:Parameter Name="pat_id" Value="record.data.pat_id" Mode="Raw">
                                                            </ext:Parameter>
                                                        </ExtraParams>
                                                    </Command>
                                                </DirectEvents>
                                            </ext:CommandColumn>
                                            <ext:CommandColumn ID="CommandColumn3" runat="server" Width="50" Hidden="true" >
                                                <Commands>
                                                    <ext:GridCommand Icon="NoteEdit" CommandName="Edit" Text="修改" />
                                                </Commands>
                                                <DirectEvents>
                                                    <Command OnEvent="NoteEdit">
                                                        <EventMask ShowMask="true" Msg="处理中….." Target="CustomTarget" CustomTarget="#{FormPanel1}" />
                                                        
                                                        <ExtraParams>
                                                            <ext:Parameter Name="pat_id" Value="record.data.pat_id" Mode="Raw">
                                                            </ext:Parameter>
                                                        </ExtraParams>
                                                    </Command>
                                                </DirectEvents>
                                            </ext:CommandColumn>
                                        </Columns>
                                    </ColumnModel>
                                   <SelectionModel>
                                    <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Multi" />
                                </SelectionModel>
<%--                                <View>
                                    <ext:GridView ID="GridView1" runat="server" StripeRows="true">  
                                    <GetRowClass Fn="getRowClass"></GetRowClass>                 
                                    </ext:GridView>
                                </View>  --%>          
                                <BottomBar>
                                    <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store1" />
                                </BottomBar>
                                </ext:GridPanel>
                            
                        </Items>
                        </ext:Panel>
                            </Items>
                        </ext:Panel>
              <ext:TextArea ID="TextArea1" runat="server" EmptyText="" FieldLabel="debug Message" Height="85" Frame="True" AutoScroll="True" Width="550" Visible="False" />
                    
            </Items>
        </ext:Viewport>
        <ext:Window 
            ID="Window1"
            runat="server"
            Title=""
            Width="530px"
            Height="330px"
            Y="0"
            Modal="true"
            AutoRender="false"
            Collapsible="true"
            Maximizable="true" >
            <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                            <LoadMask ShowMask="true" />
                        </Loader>
        </ext:Window>
        <ext:Window 
            ID="Window2"
            runat="server"
            Title=""
            Width="600px"
            Height="400px"
            Y="0"
            Modal="true"
            AutoRender="false"
            Collapsible="true"
            Maximizable="true" >
            <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                            <LoadMask ShowMask="true" />
                        </Loader>
        </ext:Window>
        <ext:Window 
            ID="Window3"
            runat="server"
            Title=""
            Width="770px"
            Height="600px"
            Y="0"
            Modal="true"
            AutoRender="false"
            Collapsible="true"
            Maximizable="true" >
            <Loader ID="Loader3" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                            <LoadMask ShowMask="true" />
                        </Loader>
        </ext:Window>

    </div>
    </form>
</body>
</html>

