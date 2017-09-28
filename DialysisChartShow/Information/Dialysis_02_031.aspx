<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_02_031.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_02_031" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>實驗室及輔助檢驗</title>
    <link href="../css/grid.css" rel="stylesheet"/>
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

        var showMenu = function (view, node, item, index, e) {
            var menu = App.TreeContextMenu;

            this.menuNode = node;
            menu.nodeName = node.get("text");
            view.getSelectionModel().select(node);

            menu.showAt([e.getXY()[0], e.getXY()[1] + 10]);
            e.stopEvent();
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">    
        <ext:TextField ID="txtPAT_NO" runat="server" Hidden="true" />
        <ext:TextField ID="txtCODE" runat="server" Hidden="true" />
        <ext:TextField ID="txtGROUP" runat="server" Hidden="true" />
        <ext:TextField ID="txtSTATUS" runat="server" Hidden="true" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="PanelT" runat="server" Title="" Region="West" Width="200" MinWidth="200" MaxWidth="200" Split="true" Collapsible="false" >
                    <Items>
                        <ext:Panel ID="TreePanel1Title" runat="server" Title="实验室检查" Region="North" Width="200" MinWidth="200" MaxWidth="200" Split="true" Collapsible="true" Header="false" >
                            <Items>
                                <ext:TreePanel ID="TreePanel1" runat="server" Title="实验室检查" icon="ChartLine" Animate="True" RootVisible="False" Height="450"> 
                                    <DirectEvents>
                                        <ItemClick OnEvent="Node_Click">
                                            <ExtraParams>
                                                <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                                <ext:Parameter Name="rTEXT" Value="record.data.text" Mode="Raw" />
                                            </ExtraParams>
                                        </ItemClick>
                                    </DirectEvents>
                                    <Listeners>
                                        <ItemContextMenu Fn="showMenu" StopEvent="true" />
                                        <RemoteActionRefusal Handler="Ext.Msg.alert('Action refusal', e.message);" />
                                    </Listeners>
                                </ext:TreePanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="PanelC" runat="server" Region="Center" >
                    <Items>
                        <ext:Panel ID="DetailPanel" runat="server" Collapsible="false" Header="true" AutoScroll="true" Frame="false">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:TextField ID="txtGROUP_NAME" runat="server" FieldLabel="群组" LabelWidth="30" Width="150" ReadOnly="true" />
                                        <ext:DateField ID="txtRESULT_DATE" runat="server" FieldLabel="检查日期" LabelWidth="60" Format="yyyy-MM-dd" LabelAlign="Right" Width="200" />
                                        <ext:SelectBox ID="cboRITEM_CODE" runat="server" FieldLabel="检查项目" LabelCls="my-Field" LabelWidth="70" LabelAlign="Right" Hidden="true" />
                                        <ext:Button ID="btnREAD" runat="server" Text="读取" Icon="Page" OnDirectClick="cmdREAD" Width="100" />
                                        <ext:Button ID="btnLAST" runat="server" Text="最近" Icon="Outline" Width="100" >
                                            <DirectEvents>
                                                <Click OnEvent="cmdLAST" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnADD" runat="server" Text="添加" Icon="Add" OnDirectClick="cmdADD" Width="100" />
                                        <ext:Button ID="btnSAVE" runat="server" Text="保存" Icon="Accept" Disabled="true" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="cmdSAVE" Before="return #{Store1}.isDirty();">
                                                    <EventMask ShowMask="true" />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="data" Value="#{Store1}.getChangedData()" Mode="Raw" Encode="true" />
                                                        <ext:Parameter Name="datb" Value="#{Store1}.getRecordsValues()" Mode="Raw" Encode="true" />
                                                    </ExtraParams>
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnCANCEL" runat="server" Text="取消" Icon="Cancel" OnDirectClick="cmdCANCEL" Disabled="true" Width="100" />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Items>
                                <ext:GridPanel ID="GridPanel1" runat="server" SortableColumns="true" Resizable="false" Width="1200" Height="500">
                                    <Store>
                                        <ext:Store ID="Store1" runat="server" ItemID="RESULT_CODE">
                                            <Model>
                                                <ext:Model ID="Model" runat="server" Name="RESULT">
                                                    <Fields>
                                                        <ext:ModelField Name="NO" />
                                                        <ext:ModelField Name="ROW_ID" />
                                                        <ext:ModelField Name="RESULT_DATE" />
                                                        <ext:ModelField Name="RESULT_CLASS" />
                                                        <ext:ModelField Name="RESULT_CODE" />
                                                        <ext:ModelField Name="RESULT_VALUE_T" />
                                                        <ext:ModelField Name="RESULT_VALUE_N" />
                                                        <ext:ModelField Name="PAT_NO" />
                                                        <ext:ModelField Name="RITEM_TYPE" />
                                                        <ext:ModelField Name="RITEM_NAME" />
                                                        <ext:ModelField Name="RITEM_NAME_S" />
                                                        <ext:ModelField Name="RITEM_UNIT" />
                                                        <ext:ModelField Name="RITEM_LOW1" />
                                                        <ext:ModelField Name="RITEM_HIGH1" />
                                                        <ext:ModelField Name="RESULT_DAYS" />
                                                        <ext:ModelField Name="RITEM_WORDS" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                            <Sorters>
                                                <ext:DataSorter Property="ROW_ID" Direction="ASC" />
                                            </Sorters>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel1" runat="server"  >
                                        <Columns>
                                            <ext:Column ID="colPAT_NO" Header="病历号" runat="server" DataIndex="PAT_NO" Hidden="true" />
                                            <ext:Column ID="colRITEM_CLASS" Header="大分类" runat="server" DataIndex="RESULT_CLASS" Hidden="true" />
                                            <ext:RowNumbererColumn ID="colNO" Text="编号" runat="server" Width="50" />
                                            <ext:Column ID="colRESULT_DATE" Header="检验日期" runat="server" DataIndex="RESULT_DATE" Width="120">
                                                <Commands>
                                                    <ext:ImageCommand CommandName="ChartEdit" Icon="BookOpenMark">
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
                                            <ext:Column ID="colRESULT_CODE" Header="检验代码" runat="server" DataIndex="RESULT_CODE" Width="100" >
                                                <Commands>
                                                    <ext:ImageCommand CommandName="ChartLine" Icon="ChartLine">
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
                                            <ext:Column ID="colRITEM_TYPE" Header="小分类" runat="server" DataIndex="RITEM_TYPE" Width="100" />
                                            <ext:Column ID="colRITEM_NAME" Header="检验简称" runat="server" DataIndex="RITEM_NAME_S" Width="120" />
                                            <ext:Column ID="colRITEM_NAME_S" Header="检验名称" runat="server" DataIndex="RITEM_NAME" Width="140" />
                                            <ext:Column ID="colRESULT_VALUE_O" Header="实际检验结果" runat="server" DataIndex="RESULT_VALUE_T" Width="90" tdCls="custom-column" Align="Right" >
                                                <Renderer Fn="change" />
                                            </ext:Column>
                                            <ext:ComponentColumn ID="colRESULT_VALUE_N" runat="server" Editor="true" DataIndex="RESULT_VALUE_N" Flex="1" Text="新检验结果1" Hidden="true" >
                                                <Component>
                                                    <ext:TextField ID="txtRESULT_VALUE_N" runat="server" EnableKeyEvents="true" >
                                                        <Listeners>
                                                            <KeyPress Fn="txt" />
                                                        </Listeners> 
                                                    </ext:TextField>  
                                                </Component>
                                            </ext:ComponentColumn>
                                            <ext:Column ID="colRITEM_UNIT" Header="检验单位" runat="server" DataIndex="RITEM_UNIT" Width="100">
                                                <Commands>
                                                    <ext:ImageCommand CommandName="EditUnit" Icon="Pencil">
                                                        <ToolTip Text="检验单位修改" />
                                                    </ext:ImageCommand>
                                                </Commands>
                                                <DirectEvents>
                                                        <Command OnEvent="modifyUnitHighLow" >
                                                            <ExtraParams>
                                                                <ext:Parameter Name="RESULT_CODE" Value="record.data.RESULT_CODE" Mode="Raw"/>
                                                                <ext:Parameter Name="RITEM_UNIT" Value="record.data.RITEM_UNIT" Mode="Raw"/>
                                                                <ext:Parameter Name="RITEM_LOW1" Value="record.data.RITEM_LOW1" Mode="Raw"/>
                                                                <ext:Parameter Name="RITEM_HIGH1" Value="record.data.RITEM_HIGH1" Mode="Raw"/>
                                                            </ExtraParams> 
                                                        </Command>
                                                </DirectEvents>
                                            </ext:Column>
                                            <ext:Column ID="colRITEM_LOW1" Header="检验低值" runat="server" DataIndex="RITEM_LOW1" Width="100" />
                                            <ext:Column ID="colRITEM_HIGH1" Header="检验高值" runat="server" DataIndex="RITEM_HIGH1" Width="100" />
                                            <ext:Column ID="colRESULT_DAYS" Header="经过时间" runat="server" DataIndex="RESULT_DAYS" Flex="1" />
                                        </Columns>
                                    </ColumnModel>                                                
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Multi">
                                            <Listeners>
                                                <Select Handler="#{btnDelete}.enable();" />
                                                <Deselect Handler="if (!#{GridPanel1}.selModel.hasSelection()) {#{btnDelete}.disable();}" />
                                            </Listeners>
                                        </ext:RowSelectionModel>
                                    </SelectionModel>
                                    <View>
                                        <ext:GridView ID="GridView1" runat="server" StripeRows="true">                   
                                        </ext:GridView>
                                    </View>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>                
                        <ext:Panel ID="Panel19" runat="server" Region="Center" Border="false" Layout="fit" Height="800" ColumnWidth="1">
                            <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>   
                    </Items>
                </ext:Panel>
                <%--右側歷史紀錄--%>
                <ext:Panel ID="PanelR" runat="server" Region="East" Layout = "FitLayout" Title="检验结果歷史資料" Width="680" Split="true" Collapsible="true" Collapsed="true" >
                    <Items>
                        <ext:GridPanel ID="GridPanel2" runat="server" StripeRows="true" TrackMouseOver="true" Region="Center" Height="534" AutoExpandColumn="Name" Border="false">
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
                                    <ext:Column ID="Column1" Header="编号" runat="server" DataIndex="NO" Width="35" Hidden="true" />
                                    <ext:Column ID="Column2" Header="病历号" runat="server" DataIndex="PAT_NO" Width="50" Hidden="true" />
                                    <ext:Column ID="Column3" Header="检验日期" runat="server" DataIndex="RESULT_DATE" Width="80"/>
                                    <ext:Column ID="Column4" Header="检验代码" runat="server" DataIndex="RESULT_CODE" Width="60" Hidden="true" />
                                    <ext:Column ID="Column5" Header="大分类" runat="server" DataIndex="RITEM_CLASS" Width="50" Hidden="true" />
                                    <ext:Column ID="Column6" Header="小分类" runat="server" DataIndex="RITEM_TYPE" Width="100" Hidden="true" />
                                    <ext:Column ID="Column7" Header="检验简称" runat="server" DataIndex="RITEM_NAME" Width="80" />
                                    <ext:Column ID="Column8" Header="检验名称" runat="server" DataIndex="RITEM_NAME_S" Width="80" Hidden="true" />
                                    <ext:Column ID="Column9" Header="检验结果" runat="server" DataIndex="RESULT_VALUE_O" Width="90" tdCls="custom-column" Align="Right" >
                                        <Renderer Fn="change" />
                                    </ext:Column>
                                    <ext:Column ID="Column10" Header="检验单位" runat="server" DataIndex="RITEM_UNIT" Width="80" Hidden="true" />
                                    <ext:Column ID="Column11" Header="检验低值" runat="server" DataIndex="RITEM_LOW1" Width="65" />
                                    <ext:Column ID="Column12" Header="检验高值" runat="server" DataIndex="RITEM_HIGH1" Width="65" />
                                    <ext:Column ID="Column13" Header="经过时间" runat="server" DataIndex="RESULT_DAYS" Width="80" />
                                    <ext:Column ID="Column14" Header="登打时间" runat="server" DataIndex="KIN_DATE" Width="135" />
                                    <ext:Column ID="Column15" Header="登打人" runat="server" DataIndex="KIN_USER" Width="80" />
                                </Columns>
                            </ColumnModel>
                            <View>
                                <ext:GridView ID="GridView2" runat="server" StripeRows="true">                   
                                </ext:GridView>
                            </View>            
                        </ext:GridPanel>
                    </Items>  
                </ext:Panel>
                <%--左側樹狀結構，右鍵選單--%>
                <ext:Menu ID="TreeContextMenu" runat="server">
                    <Items>
                        <ext:Label ID="NodeName" runat="server" Cls="bold-text" />
                        <ext:MenuSeparator />
                        <ext:MenuItem ID="MenuItem1" runat="server" Text="大分类维护" Icon="BookmarkEdit">
                            <DirectEvents>
                                <Click OnEvent="MaintainItemGroup">
                                </Click>
                            </DirectEvents>
                        </ext:MenuItem>
                        <ext:MenuItem ID="MenuItem2" runat="server" Text="小分类维护" Icon="ChartbarEdit">
                            <DirectEvents>
                                <Click OnEvent="MaintainItem">
                                </Click>
                            </DirectEvents>
                        </ext:MenuItem>
                    </Items>
                    <Listeners>
                        <Show Handler="#{NodeName}.setText(this.nodeName+'-维护');" />
                    </Listeners>
                </ext:Menu>
                <%-- 修改單位, 高低值 --%>
                <ext:Window ID="frmUnitRange" runat="server" Title="輸入單位及範圍" Height="300" Width="300" 
                    Closable="false" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center">
                    <Items>
                        <ext:TextField ID="TextCode" runat="server" FieldLabel="檢驗代碼" ColumnWidth="1" LabelAlign="Left" Width="180" Padding="5" />
                        <ext:TextField ID="TextUnit" runat="server" FieldLabel="單位" ColumnWidth="1" LabelAlign="Left" Width="180" Padding="5" />
                        <ext:TextField ID="TextLow" runat="server" FieldLabel="低" ColumnWidth="1" LabelAlign="Left" Width="180" Padding="5" />
                        <ext:TextField ID="TextHigh" runat="server" FieldLabel="高" ColumnWidth="1" LabelAlign="Left" Width="180" Padding="5" />
                    </Items>
                    <Buttons>
                        <ext:Button ID="btnUpdateUnitRange" runat="server" Icon="Accept" Text="确认" Width="120" Height="40">
                            <DirectEvents>
                                <Click OnEvent="btnUpdateUnitRange_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="btnCANCELUnitRange" runat="server" Icon="Cancel" Text="取消" Width="120" Height="40">
                            <DirectEvents>
                                <Click OnEvent="btnCANCELUnitRange_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:Window> 
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
