<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_02_detail.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_02_detail" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>净化过程明细</title>
    <link href="../css/dialysis_01.css" rel="stylesheet"/>
    <style type="text/css">
        .Text-blue .x-form-field
        {
            color: blue;
        }
    
        .Text-red .x-form-field
        {
            color: red;
        }
    
        .Label .x-form-item-label-text
        {
            color:White;
        }
    
        .Text-Black .x-form-field
        {
            color:Black;
            font-weight:bold;        
        }
    </style>
    <script type="text/javascript">
        var DetailsRender = function () {
            return '<img class="imgEdit" ext:qtip="Click to view/edit additional details" style="cursor:pointer;" src="vcard_edit.png" />';
        };

        var cellClick = function (view, cell, columnIndex, record, row, rowIndex, e) {
            var t = e.getTarget(),
            columnId = this.columns[columnIndex].id; // Get column id

            if (t.className == "imgEdit" && columnId == "Details") {
                //the ajax call is allowed
                return true;
            }

            //forbidden
            return false;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="Dialysis_Date" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="净化过程明细" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:GridPanel ID="Grid_DataList" runat="server" EnableColumnMove="false" EnableColumnResize="false" EnableColumnHide="false" Border="true" Cls="x-grid-custom-mid">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="25">
                                    <Model>
                                        <ext:Model ID="Model2" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="dialysis_date" />
                                                <ext:ModelField Name="dialysis_time" />
                                                <ext:ModelField Name="column_7" />
                                                <ext:ModelField Name="column_6" />
                                                <ext:ModelField Name="column_2" />
                                                <ext:ModelField Name="column_3" />
                                                <ext:ModelField Name="column_10" />
                                                <ext:ModelField Name="column_8" />
                                                <ext:ModelField Name="column_4" />
                                                <ext:ModelField Name="cln2_t" />
                                                <ext:ModelField Name="cln2_p" />
                                                <ext:ModelField Name="cln2_r" />
                                                <ext:ModelField Name="cln2_bp" />
                                                <ext:ModelField Name="cln2_rmk" />
                                                <ext:ModelField Name="cln2_user" />
                                                <ext:ModelField Name="cln2_dateadded" />
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
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="date" runat="server" Text="日期" DataIndex="dialysis_date" Hidden="true" />
                                    <ext:Column ID="time" runat="server" Text="时间" DataIndex="dialysis_time" Width="100">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Edit" Icon="Pencil" Style="margin-left:5px !important;" >
                                                <ToolTip Text="补登录" />
                                            </ext:ImageCommand>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="EditDetails" >
                                                <ExtraParams>                                                
                                                    <%--<ext:Parameter Name="dialysis_date" Value="record.data.dialysis_date" Mode="Raw" />
                                                    <ext:Parameter Name="dialysis_time" Value="record.data.dialysis_time" Mode="Raw" />--%>
                                                    <ext:Parameter Name="Values" Value="record.data" Mode="Raw" Encode="true" />
                                                </ExtraParams> 
                                            </Command>
                                        </DirectEvents> 
                                    </ext:Column>
                                    <ext:Column ID="diagno" runat="server" Text="电导" DataIndex="column_7" Align="Right" Width="70" />
                                    <ext:Column ID="Column4" runat="server" Text="温度" DataIndex="column_6" Align="Right" Width="70" />
                                    <ext:Column ID="Column2" runat="server" Text="<div align='Center'>已超滤</br>kg</div>" DataIndex="column_2" Align="Right" Width="80" />
                                    <ext:Column ID="Column13" runat="server" Text="<div align='Center'>超滤率</br>ml/hr</div>" DataIndex="column_3" Align="Right" Width="80" />
                                    <ext:Column ID="Column3" runat="server" Text="<div align='Center'>跨膜压</br>mmHg</div>" DataIndex="column_10" Align="Right" Width="80" />
                                    <ext:Column ID="Column1" runat="server" Text="<div align='Center'>静脉压</br>mmHg</div>" DataIndex="column_8" Align="Right" Width="80" />
                                    <ext:Column ID="Column5" runat="server" Text="<div align='Center'>血流量</br>ml/min</div>" DataIndex="column_4" Align="Right" Width="80"/>
                                    <ext:Column ID="Column6" runat="server" Text="T °C" DataIndex="cln2_t" Align="Right" Width="70" />
                                    <ext:Column ID="Column7" runat="server" Text="P 次/分" DataIndex="cln2_p" Align="Right" Width="80" />
                                    <ext:Column ID="Column8" runat="server" Text="R 次/分" DataIndex="cln2_r" Align="Right" Width="80" />
                                    <ext:Column ID="Column9" runat="server" Text="BP mmHg" DataIndex="cln2_bp" Align="Right" Width="100"/>
                                    <ext:Column ID="Column10" runat="server" Text="病情及处理" DataIndex="cln2_rmk" Align="Left" Width="120" />
                                    <ext:Column ID="Column11" runat="server" Text="记录人" DataIndex="cln2_user" Align="Left" Width="100" />
                                    <ext:Column ID="Column12" runat="server" Text="纪录时间" DataIndex="cln2_dateadded" Width="115" Hidden="true" />                                  
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" DisplayInfo="true" DisplayMsg="显示 净化过程明细 {0} - {1} of {2}" EmptyMsg="没有 净化过程明细 可显示" />
                            </BottomBar> 
                            <View>
                                <ext:GridView ID="GridView1" runat="server" StripeRows="true" />
                            </View>
                        </ext:GridPanel>
                    </Items>
                </ext:FormPanel>
                <ext:Window ID="DetailsWindow" runat="server" Icon="Group" Title="补登录" Width="350" Height="500" Modal="true" Hidden="true" Layout="Fit">
                    <Items>
                        <ext:FormPanel ID="DataListInfo" runat="server" Title="净化过程明细" Header="false" DefaultAnchor="100%" BodyPadding="5" AutoScroll="true" UI="Primary">
                            <Items>
                                <ext:TextField ID="OldTime" runat="server" Hidden="true" Cls="Text-red" />
                                <ext:TextField ID="PationID" runat="server" FieldLabel="身分证号" ReadOnly="true" Cls="Text-blue" LabelCls="Label" />
                                <ext:TextField ID="DialysisDate" runat="server" FieldLabel="日期" ReadOnly="true" Cls="Text-blue" LabelCls="Label" />
                                <ext:TextField ID="DialysisTime" runat="server" FieldLabel="时间" Cls="Text-red" LabelCls="Label" />
                                <ext:TextField ID="TextField1" runat="server" FieldLabel="电导" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField2" runat="server" FieldLabel="温度" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField3" runat="server" FieldLabel="已超滤 kg" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField4" runat="server" FieldLabel="超滤率 ml/hr" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField5" runat="server" FieldLabel="跨膜压 mmHg" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField6" runat="server" FieldLabel="静脉压 mmHg" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField7" runat="server" FieldLabel="血流量 ml/min" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField8" runat="server" FieldLabel="T °C" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField9" runat="server" FieldLabel="P 次/分" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField10" runat="server" FieldLabel="R 次/分" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField11" runat="server" FieldLabel="BP mmHg" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField12" runat="server" FieldLabel="病情及处理" LabelCls="Label" Cls="Text-Black" />
                                <ext:TextField ID="TextField13" runat="server" FieldLabel="user" Hidden="true" />
                            </Items>
                        </ext:FormPanel>
                    </Items>
                    <Buttons>                                                                               
                        <ext:Button ID="BtnDel" runat="server" Text="删除" Icon="Delete" Width="80" UI="Danger">
                            <DirectEvents>
                                <Click OnEvent="BtnDel_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="PationID" Value="#{PationID}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="DialysisDate" Value="#{DialysisDate}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="DialysisTime" Value="#{DialysisTime}.getValue()" Mode="Raw" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="SaveButton" runat="server" Text="储存" Icon="Disk" UI="Success">
                            <DirectEvents>
                                <Click OnEvent="SaveDataList" Failure="Ext.MessageBox.alert('Saving failed', 'Error during ajax event');">
                                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{DetailsWindow}.body}" />
                                    <ExtraParams>
                                        <ext:Parameter Name="PationID" Value="#{PationID}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="DialysisDate" Value="#{DialysisDate}.getValue()" Mode="Raw" />
                                        <ext:Parameter Name="DialysisTime" Value="#{DialysisTime}.getValue()" Mode="Raw" />
                                    </ExtraParams>
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="CancelButton" runat="server" Text="取消" Icon="Cancel" UI="Success">
                            <Listeners>
                                <Click Handler="this.up('window').hide();" />
                            </Listeners>
                        </ext:Button>
                    </Buttons>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
