<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_041.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_041" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>

    <script type="text/javascript">
        var template = 'color:{0};';

        var change = function (value, meta, record, row, col) {
            meta.style = Ext.String.format(template, "blue");
            if (record.get("RITEM_HIGH1") != "")
                if (parseFloat(value) > parseFloat(record.get("RITEM_HIGH1")))
                    meta.style = Ext.String.format(template, "red");
            if (record.get("RITEM_LOW1") != "")
                if (parseFloat(value) < parseFloat(record.get("RITEM_LOW1")))
                    meta.style = Ext.String.format(template, "green");
            return value + "  ";
        };

        var change2 = function (value, meta, record, row, col) {
            meta.style = Ext.String.format(template, "blue");
            if (record.get("RITEM_HIGH1") != "")
                if (parseFloat(value) > parseFloat(record.get("RITEM_HIGH1")))
                    meta.style = Ext.String.format(template, "red");
            if (record.get("RITEM_LOW1") != "")
                if (parseFloat(value) < parseFloat(record.get("RITEM_LOW1")))
                    meta.style = Ext.String.format(template, "green");
            return value + "  " + record.get("RITEM_UNIT");
        };

        var txtTT = function (item, e) {
            var s = item.value, t = '', l = s.length;
            switch (e.getKey()) {
                case 43:
                    t = "，"
                    e.stopEvent();
                    break;
                case e.P:
                    t = 'POSITIVE';
                    e.stopEvent();
                    break;
                case e.N:
                    t = 'NEGATIVE';
                    e.stopEvent();
                    break;
            }
            if (t == '，')
                item.setValue(s + t);
            else if (t != '')
                if (l > 0)
                    if (s.substr(l - 1, 1) == ',')
                        item.setValue(s + t);
                    else
                        item.setValue(t);
                else
                    item.setValue(t);
            else
                item.setValue(e.getKey().toString());
        };

        var txt = function (item, e) {
            var s = "阴性|弱阳性|阳性|强阳性|未检".split("|");
            var t = item.value;
            if (e.getKey() >= 112 && e.getKey() <= 116) {
                item.setValue(t + s[e.getKey() - 112]);
                e.stopEvent();
            }
        };

        var onCommand = function (column, command, record, recordIndex, cellIndex) {
            Ext.Msg.alert('Command', 'Command = ' + command + '<br/>' + 'Code = ' + record.data.RESULT_CODE);
        };
    </script>

    <style type = "text/css">
    <%--panel head 自动--%>
    .x-panel-header-text {
        font-size: 16px;
        font-weight: bold;
        line-height: 20px;
    }
    <%--cell字型大小  自动 ?--%>
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
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="PanelT" runat="server" Title="." Region="West" Width="180" MinWidth="180" MaxWidth="180" Split="true" Collapsible="true" >
                    <Items>
                        <ext:TreePanel ID="TreePanel1" runat="server" Border="false" >
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

                <ext:Panel ID="PanelR" runat="server" Region="East" Layout = "FitLayout" Title="." Width="655" Split="true" Collapsible="true" Collapsed="true" >
                    <Items>
                        <ext:GridPanel ID="GridPanel2" runat="server" Title="." StripeRows="true" TrackMouseOver="true" Region="Center" Height="534"  AutoExpandColumn="Name" Border="false" >
                            <Store>
                                <ext:Store ID="Store2" runat="server" >
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" Name="RESULT_LOG" >
                                            <Fields>
                                                <ext:ModelField Name="NO" Type="Int" />
                                                <ext:ModelField Name="PAT_NO" />
                                                <ext:ModelField Name="RESULT_DATE" />
                                                <ext:ModelField Name="RESULT_CODE" />
                                                <ext:ModelField Name="RITEM_CLASS" />
                                                <ext:ModelField Name="RITEM_TYPE" />
                                                <ext:ModelField Name="RITEM_NAME" />
                                                <ext:ModelField Name="RITEM_NAME_S" />
                                                <ext:ModelField Name="RESULT_VALUE_O" />
                                                <ext:ModelField Name="RITEM_UNIT" />
                                                <ext:ModelField Name="RESULT_VALUE_N" />
                                                <ext:ModelField Name="RITEM_LOW1" />
                                                <ext:ModelField Name="RITEM_HIGH1" />
                                                <ext:ModelField Name="RESULT_DAYS" />
                                                <ext:ModelField Name="ROW_ID" />
                                                <ext:ModelField Name="KIN_DATE" />
                                                <ext:ModelField Name="KIN_USER" />
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
                                    <ext:Column ID="Column1" Header="编号" runat="server" DataIndex="NO" Width="35" Cls="x-grid-hd-inner" Hidden="true" />
                                    <ext:Column ID="Column2" Header="病历号" runat="server" DataIndex="PAT_NO" Width="50" Cls="x-grid-hd-inner" Hidden="true" />
                                    <ext:Column ID="Column3" Header="检查日期" runat="server" DataIndex="RESULT_DATE" Width="80" Cls="x-grid-hd-inner"/>
                                    <ext:Column ID="Column4" Header="检查代码" runat="server" DataIndex="RESULT_CODE" Width="60" Cls="x-grid-hd-inner" Hidden="true" />
                                    <ext:Column ID="Column5" Header="大分类" runat="server" DataIndex="RITEM_CLASS" Width="50" Cls="x-grid-hd-inner" Hidden="true" />
                                    <ext:Column ID="Column6" Header="小分类" runat="server" DataIndex="RITEM_TYPE" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                    <ext:Column ID="Column7" Header="检查简称" runat="server" DataIndex="RITEM_NAME" Width="80" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column8" Header="检查名称" runat="server" DataIndex="RITEM_NAME_S" Width="80" Cls="x-grid-hd-inner" Hidden="true" />
                                    <ext:Column ID="Column9" Header="检查结果" runat="server" DataIndex="RESULT_VALUE_O" Width="65" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" >
                                        <Renderer Fn="change" />
                                    </ext:Column>
                                    <ext:Column ID="Column10" Header="检查单位" runat="server" DataIndex="RITEM_UNIT" Width="80" Cls="x-grid-hd-inner" Hidden="true" />
                                    <ext:Column ID="Column11" Header="检查低值" runat="server" DataIndex="RITEM_LOW1" Width="65" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column12" Header="检查高值" runat="server" DataIndex="RITEM_HIGH1" Width="65" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column13" Header="经过时间" runat="server" DataIndex="RESULT_DAYS" Width="80" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column14" Header="登打时间" runat="server" DataIndex="KIN_DATE" Width="135" Cls="x-grid-hd-inner" />
                                    <ext:Column ID="Column15" Header="登打人" runat="server" DataIndex="KIN_USER" Width="80" Cls="x-grid-hd-inner" />
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:GridView ID="GridView2" runat="server" StripeRows="true">                   
                                </ext:GridView>
                            </View>            
                        </ext:GridPanel>
                    </Items>  
                </ext:Panel> 

                <ext:Panel ID="PanelC" runat="server" Region="Center" >
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Region="South" Height="606" Collapsible="true" Border="false" >
                            <Items>
                                <ext:Panel ID="PanelU" runat="server" Region="North" Height="35" BodyPadding="6" Border="false" >
                                    <Items>
                                        <ext:Container ID="Container5" runat="server" Frame="true" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:TextField ID="txtGROUP_NAME" runat="server" FieldLabel="群组" LabelWidth="30" Width="120" ReadOnly="true" />
                                                <ext:TextField ID="txtPAT_NO" runat="server" Width="50" />
                                                <ext:TextField ID="txtCODE" runat="server" Width="50" />
                                                <ext:TextField ID="txtGROUP" runat="server" Width="50" />
                                                <ext:TextField ID="txtSTATUS" runat="server" Width="50" />
                                                <ext:DateField ID="txtRESULT_DATE" runat="server" FieldLabel="检查日期" LabelWidth="60" Format="yyyy-MM-dd"  LabelAlign="Right" Width="180" />
                                                <ext:SelectBox ID="cboRITEM_CODE" runat="server" FieldLabel="检查项目" LabelCls="my-Field" LabelWidth = "70"  LabelAlign="Right" Hidden="true" />
                                                
                                                <ext:Label ID="GAP1" runat="server" Text="" Width="10" />
                                                <ext:Button ID="btnREAD" runat="server" Text="读取" Icon="Page" Width="70"  OnDirectClick="cmdREAD" />
                                                <ext:Label ID="GAP2" runat="server" Text="" Width="10" />
                                                <ext:Button ID="btnLAST" runat="server" Text="最近" Icon="Outline" Width="70" Hidden="true" >
                                                    <DirectEvents>
                                                        <Click OnEvent="cmdLAST" />
                                                    </DirectEvents>
                                                </ext:Button>                                        
                                                <ext:Label ID="GAP3" runat="server" Text="" Width="10" />
                                                <ext:Button ID="btnADD" runat="server" Text="添加" Icon="Add" Width="70" OnDirectClick="cmdADD" Hidden="true" >
                                                </ext:Button>                                        
                                                <ext:Label ID="GAP4" runat="server" Text="　" Width="10" />
                                                <ext:Button ID="btnSAVE" runat="server" Text="保存" Icon="Accept" Width="70" Disabled="true" Hidden="true" >
                                                    <DirectEvents>
                                                        <Click OnEvent="cmdSAVE" Before="return #{Store1}.isDirty();">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="data" Value="#{Store1}.getChangedData()" Mode="Raw" Encode="true" />
                                                                <ext:Parameter Name="datb" Value="#{Store1}.getRecordsValues()" Mode="Raw" Encode="true" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>   
                                                <ext:Label ID="GAP5" runat="server" Text="" Width="10" />
                                                <ext:Button ID="btnCANCEL" runat="server" Text="取消" Icon="Cancel" Width="70" OnDirectClick="cmdCANCEL" Disabled="true" Hidden="true" >
                                                </ext:Button>                                        
                                                <ext:Label ID="GAP6" runat="server" Text="" Width="10" />
                                                <ext:Button ID="btnCALC" runat="server" Text="计算" Icon="Calculator" Width="70" Disabled="true" Hidden="true" >
                                                    <DirectEvents>
                                                        <Click OnEvent="cmdCALC" >
                                                            <ExtraParams>
                                                                <ext:Parameter Name="datb" Value="#{Store1}.getRecordsValues()" Mode="Raw" Encode="true" />
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>   
                                            </Items>
                                        </ext:Container> 
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Items>
                                <ext:Panel ID="PanelM" runat="server" Region="Center" Border="false" >
                                    <Items>
                                        <ext:GridPanel ID="GridPanel1" runat="server" Title="检查结果" StripeRows="true" TrackMouseOver="true" Region="Center" Height="534"  AutoExpandColumn="Name" >
                                            <Store>
                                                <ext:Store ID="Store1" runat="server" ItemID="RESULT_CODE"  >
                                                    <Model>
                                                        <ext:Model ID="Model" runat="server" Name="RESULT" >
                                                            <Fields>
                                                                <ext:ModelField Name="NO" Type="Int" />
                                                                <ext:ModelField Name="PAT_NO" />
                                                                <ext:ModelField Name="RESULT_DATE" />
                                                                <ext:ModelField Name="RESULT_CODE" />
                                                                <ext:ModelField Name="RITEM_CLASS" />
                                                                <ext:ModelField Name="RITEM_TYPE" />
                                                                <ext:ModelField Name="RITEM_NAME" />
                                                                <ext:ModelField Name="RITEM_NAME_S" />
                                                                <ext:ModelField Name="RESULT_VALUE_O" />
                                                                <ext:ModelField Name="RITEM_UNIT" />
                                                                <ext:ModelField Name="RESULT_VALUE_N" />
                                                                <ext:ModelField Name="RITEM_LOW1" />
                                                                <ext:ModelField Name="RITEM_HIGH1" />
                                                                <ext:ModelField Name="RESULT_DAYS" />
                                                                <ext:ModelField Name="ROW_ID" />
                                                                <ext:ModelField Name="RESULT_VALUE" />
                                                                <ext:ModelField Name="RITEM_WORDS" />
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
                                                    <ext:Column ID="colNO" Header="编号" runat="server" DataIndex="NO" Width="35" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="colPAT_NO" Header="病历号" runat="server" DataIndex="PAT_NO" Width="50" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="colRESULT_DATE" Header="检查日期" runat="server" DataIndex="RESULT_DATE" Width="110" Cls="x-grid-hd-inner" >
                                                        <Commands>
                                                            <ext:ImageCommand CommandName="ChartEdit" Icon="Pencil" Style="margin-left:5px !important;" >
                                                                <ToolTip Text="修改历程" />
                                                            </ext:ImageCommand>
                                                        </Commands>
                                                        <DirectEvents>
                                                            <Command OnEvent="logEDIT" >
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="PAT_NO" Value="record.data.PAT_NO" Mode="Raw"/>
                                                                    <ext:Parameter Name="RESULT_DATE" Value="record.data.RESULT_DATE" Mode="Raw"/>
                                                                    <ext:Parameter Name="RESULT_CODE" Value="record.data.RESULT_CODE" Mode="Raw"/>
                                                                </ExtraParams> 
                                                            </Command> 
                                                        </DirectEvents>
                                                    </ext:Column> 
                                                    <ext:Column ID="colRESULT_CODE" Header="检查代码" runat="server" DataIndex="RESULT_CODE" Width="70" Cls="x-grid-hd-inner" >
                                                        <Commands>
                                                            <ext:ImageCommand CommandName="ChartLine" Icon="ChartLine" Style="margin-left:5px !important;" >
                                                                <ToolTip Text="历次检查" />
                                                            </ext:ImageCommand>
                                                        </Commands>
                                                        
                                                        <DirectEvents>
                                                            <Command OnEvent="logEXAM" >
                                                                <ExtraParams>
                                                                    <ext:Parameter Name="PAT_NO" Value="record.data.PAT_NO" Mode="Raw"/>
                                                                    <ext:Parameter Name="RESULT_DATE" Value="record.data.RESULT_DATE" Mode="Raw"/>
                                                                    <ext:Parameter Name="RESULT_CODE" Value="record.data.RESULT_CODE" Mode="Raw"/>
                                                                </ExtraParams> 
                                                            </Command> 
                                                        </DirectEvents>
                                                    </ext:Column>
                                                    <ext:Column ID="colRITEM_CLASS" Header="大分类" runat="server" DataIndex="RITEM_CLASS" Width="50" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="colRITEM_TYPE" Header="小分类" runat="server" DataIndex="RITEM_TYPE" Width="100" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="colRITEM_NAME" Header="检查简称" runat="server" DataIndex="RITEM_NAME" Width="80" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="colRITEM_NAME_S" Header="检查名称" runat="server" DataIndex="RITEM_NAME_S" Width="80" Cls="x-grid-hd-inner" Hidden="true" />
                                                    <ext:Column ID="colRESULT_VALUE_O" Header="实际检查结果" runat="server" DataIndex="RESULT_VALUE_O" Width="90" Cls="x-grid-hd-inner" tdCls="custom-column" Align="Right" >
                                                        <Renderer Fn="change" />
                                                    </ext:Column>
                                                    <ext:Column ID="colRITEM_UNIT" Header="检查单位" runat="server" DataIndex="RITEM_UNIT" Width="80" Cls="x-grid-hd-inner" />
                                                    <ext:ComponentColumn ID="colRESULT_VALUE_N" runat="server" Editor="true" DataIndex="RESULT_VALUE_N" Flex="1" Cls="x-grid-hd-inner" Text="新检查结果" Hidden="true" >
                                                        <Component>
                                                            <ext:TextField ID="txtRESULT_VALUE_N" runat="server" EnableKeyEvents="true" >
                                                                <Listeners>
                                                                    <KeyPress Fn="txt" />
                                                                </Listeners> 
                                                            </ext:TextField>  
                                                        </Component>
                                                    </ext:ComponentColumn>

                                                    <ext:Column ID="colRITEM_LOW1" Header="检查低值" runat="server" DataIndex="RITEM_LOW1" Width="80" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="colRITEM_HIGH1" Header="检查高值" runat="server" DataIndex="RITEM_HIGH1" Width="80" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="colRESULT_DAYS" Header="经过时间" runat="server" DataIndex="RESULT_DAYS" Width="100" Cls="x-grid-hd-inner" />
                                                </Columns>
                                            </ColumnModel>
                                            <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" SingleSelect="true">
                                   
                                                </ext:RowSelectionModel>
                                            </SelectionModel>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
