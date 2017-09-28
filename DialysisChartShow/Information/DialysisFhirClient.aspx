<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="DialysisFhirClient.aspx.cs" Inherits="Dialysis_Chart_Show.Information.DialysisFhirClient" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>
<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
<title>统计资料上传</title>
<style type="text/css">
    .x-grid-custom .x-grid-row-alt .x-grid-cell {
            background-color : #DAE2E8;
        }
    .x-panel-header-text {
        font-size: 16px;
        font-weight: bold;
        line-height: 20px;
    }
    .x-grid-row .x-grid-cell { 
        font-size: 13px;
    }
    .x-grid-hd-inner {
        font-size: 12px;
        font-weight: bold;
    }
    .x-grid-row .custom-column { 
        font-weight: bold;
    }
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
<script type="text/javascript">
        var template = 'color:{0};';

        var change = function (value, meta, record, row, col) {
            var nowDate = new Date();
            var lastDate1 = new Date();
            var lastDate2 = new Date();
            lastDate1.setMonth(lastDate1.getMonth() - 1);
            lastDate2.setMonth(lastDate2.getMonth() - 2);
            var nowMonth = nowDate.getMonth() + 1;
            var lastMonth1 = lastDate1.getMonth() + 1;
            var lastMonth2 = lastDate2.getMonth() + 1;
            var nowDateMonth = nowDate.getFullYear().toString() + "年" + (nowMonth > 10 ? nowMonth.toString() : "0" + nowMonth.toString());
            var lastDateMonth1 = lastDate1.getFullYear().toString() + "年" + (lastMonth1 > 10 ? lastMonth1.toString() : "0" + lastMonth1.toString());
            var lastDateMonth2 = lastDate2.getFullYear().toString() + "年" + (lastMonth2 > 10 ? lastMonth2.toString() : "0" + lastMonth2.toString());

            meta.style = Ext.String.format(template, "blue");
            if (record.get("年度月份") != "") {
                if ((record.get("年度月份")).substring(0, 7) == nowDateMonth)
                    meta.style = Ext.String.format(template, "red");
                else if ((record.get("年度月份")).substring(0, 7) == lastDateMonth1)
                    meta.style = Ext.String.format(template, "orange");
                else if ((record.get("年度月份")).substring(0, 7) == lastDateMonth2)
                    meta.style = Ext.String.format(template, "green");
            }
            return value + "  ";
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
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN"/>
        <ext:Viewport ID="Viewport1" runat="server" Layout="border">
            <Items>
                <ext:Panel ID="PanelT" runat="server" Title="" Region="West" Width="220" MinWidth="220" 
                    MaxWidth="220" Split="true" Collapsible="false" Layout = "FitLayout" >
                    <Items>
                        <ext:Panel ID="TreePanel1Title" runat="server" Title="历史上传记录" Region="North" 
                            Layout="FitLayout" Split="true" Collapsible="false">
                            <Items>
                                <ext:TreePanel ID="TreePanel1" runat="server" Border="false" Icon="ChartOrganisation" Layout="FitLayout" MinHeight="250" 
                                    AutoScroll="true" Animate="true" RootVisible="false" ContainerScroll="true" >
                                    <DirectEvents>
                                        <ItemClick OnEvent="Empty_Click">
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
                <ext:Panel ID="PanelC" runat="server" Region="Center" Layout="FitLayout" MinWidth="620" MinHeight="220" >
                    <Items>
                        <ext:Panel ID="DetailPanel" runat="server" Region="South" Height="606" Width="800" Collapsible="false" 
                            AutoScroll="true" Border="false" Title="最新上传状态">
                            <Items>
                                <ext:Panel ID="PanelU" runat="server" Region="North" Height="50" BodyPadding="6"
                                    Border="false">
                                    <Items>
                                        <ext:Container ID="Container5" runat="server" Frame="true" Layout="HBoxLayout">
                                            <Items>
                                                <ext:ComboBox ID="cboYEAR" runat="server" FieldLabel="年度" LabelWidth="50" LabelAlign="Right"
                                                    Editable="false" Width="150" PaddingSpec="4 0 4 0">
                                                    <DirectEvents>
                                                        <Select OnEvent="ChangeGroup" />
                                                    </DirectEvents>
                                                </ext:ComboBox>
                                                <ext:ComboBox ID="cboMonth" runat="server" FieldLabel="月份" LabelWidth="50" LabelAlign="Right"
                                                    Editable="false" EmptyText="未选择" Width="150" PaddingSpec="4 0 4 0">
                                                </ext:ComboBox>
                                                <ext:ComboBox ID="cboType" runat="server" FieldLabel="类别" LabelWidth="50" LabelAlign="Right"
                                                    Editable="false" Width="200" PaddingSpec="4 0 4 0" Visible="True" EmptyText="未选择">
                                                    <Items>
                                                        <ext:ListItem Value="ALL" Text="ALL" />
                                                        <ext:ListItem Value="人口分布" Text="人口分布" />
                                                        <ext:ListItem Value="血透年限分布" Text="血透年限分布" />
                                                        <ext:ListItem Value="死亡率" Text="死亡率" />
                                                        <ext:ListItem Value="血液透析品质" Text="血液透析品质" />
                                                        <ext:ListItem Value="医事人员" Text="医事人员" />
                                                        <ext:ListItem Value="病患基本数据" Text="病患基本数据" />
                                                        <ext:ListItem Value="医嘱用药" Text="医嘱用药" />
                                                    </Items>
                                                </ext:ComboBox>
                                                <ext:Button ID="BtnSend" runat="server" Icon="TableGo" Text="传送" Width="80" PaddingSpec="4 0 4 0" Type="Submit">
                                                    <DirectEvents>
                                                        <Click OnEvent="BtnSend_Click" Timeout="100000">
                                                            <EventMask ShowMask="true" Msg="正在处理..." />
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Checkbox ID="chk_01" runat="server" BoxLabel="传送所有尚未<br>传送数据" LabelWidth="200">
                                                    <DirectEvents>
                                                     <%--     <change OnEvent="chk_01_Event" /> --%>
                                                    </DirectEvents>
                                                </ext:Checkbox>
                                                <ext:Label ID="LabelMonth" runat="server" Text="" />
                                                <ext:TextArea ID="are_1" runat="server" FieldLabel="" Width="500" Height="50" Visible="False" LabelWidth="0" />
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="PanelM" runat="server" Region="Center" Border="false" AutoScroll="true">
                                    <Items>
                                        <ext:GridPanel ID="Grid_List" runat="server" StripeRows="true" TrackMouseOver="true" Layout="FitLayout" 
                                            Region="Center" AutoExpandColumn="Name">
                                            <%-- AutoScroll="true" Height="470" --%>
                                            <Store>
                                                <ext:Store ID="Store1" runat="server" PageSize="15" OnReadData="AuditList_Refresh">
                                                    <Model>
                                                        <ext:Model ID="Model" runat="server" Name="RESULT">
                                                            <Fields>
                                                                <ext:ModelField Name="编号" Type="Int" />
                                                                <ext:ModelField Name="最新上传时间" />
                                                                <ext:ModelField Name="年度月份" />
                                                                <ext:ModelField Name="类别" />
                                                                <ext:ModelField Name="状态" />
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
                                                    <ext:Column ID="Column156" Header="编号" runat="server" DataIndex="编号" Width="50" Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column158" Header="年度月份" runat="server" DataIndex="年度月份" Width="130"
                                                        Cls="x-grid-hd-inner" TdCls="custom-column">
                                                        <Renderer Fn="change" />
                                                    </ext:Column>
                                                    <ext:Column ID="Column161" Header="类别" runat="server" DataIndex="类别" Width="150"
                                                        Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column160" Header="最新上传时间" runat="server" DataIndex="最新上传时间" Width="160"
                                                        Cls="x-grid-hd-inner" />
                                                    <ext:Column ID="Column162" Header="状态" runat="server" DataIndex="状态" Width="100" Cls="x-grid-hd-inner" />
                                                </Columns>
                                            </ColumnModel>
                                            <SelectionModel>
                                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Multi">
                                                    <Listeners>
                                                        <Select Handler="#{btnDelete}.enable();" />
                                                        <Deselect Handler="if (!#{GridPanel1}.selModel.hasSelection()) {
                                                                               #{btnDelete}.disable();
                                                                           }" />
                                                    </Listeners>
                                                </ext:RowSelectionModel>
                                            </SelectionModel>
                                            <%--
                                            --%>
                                            <View>
                                                <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                                                </ext:GridView>
                                            </View>
                                            <BottomBar>
                                                <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store1" />
                                            </BottomBar>
                                        </ext:GridPanel>
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                        <ext:Hidden ID="Hidden1" runat="server" />
                        <ext:Panel ID="Panel19" runat="server" Region="Center" Border="false" Layout="fit"
                            Height="800" ColumnWidth="1">
                            <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
