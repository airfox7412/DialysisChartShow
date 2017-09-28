<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="CheckinList.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.CheckinList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化系统-報到</title>
    <script type="text/javascript">
        var template = 'color:{0};';
        var change = function (value, meta, record, row, col) {
            switch (record.get("MAC_TYPE")) {
                case 'HD':
                    meta.style = Ext.String.format(template, "blue");
                    break;
                case 'HDF':
                    meta.style = Ext.String.format(template, "red");
                    break;
                default:
                    meta.style = Ext.String.format(template, "green");
            }

            return value;
        };

        var change2 = function (value, meta, record, row, col) {
            switch (record.get("PERSON_STATE")) {
                case 'A':
                    meta.style = Ext.String.format(template, "red");
                    break;
                case '开':
                    meta.style = Ext.String.format(template, "red");
                    break;
                case 'S':
                    meta.style = Ext.String.format(template, "black");
                    break;
                case "关":
                    meta.style = Ext.String.format(template, "black");
                    break;
                default:
                    meta.style = Ext.String.format(template, "blue");
            }

            return value;
        };

        var enterKeyPressHandler = function (f, e) {
            if (e.getKey() == e.ENTER) {
                Ext.Net.DirectMethods.SearchResult();
                e.stopEvent();
            }
        };
    </script>
    <style type="text/css">
        <%--ComboBox Items--%>
        .x-boundlist-item 
        { 
            font-size: 28px;
            height: 32px;
        }
         <%--panel head 自动--%>
        .x-panel-header-text {
            font-size: 20px;
            font-weight: bold;
            line-height: 21px;
            color: #04408c;
        }
        <%--cell字型大小 自动--%>
        .x-grid-row .x-grid-cell { 
            font-size: 24px;
        }
        <%--Grid Row--%>
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 24px;
            line-height: 40px; 
        }
        <%--Grid Column--%>
        .x-column-header-inner .x-column-header-text
        {
            font-size: 20px;
            line-height: 28px; 
        }
        
        <%--文字框加大--%>
        .x-border-box .x-form-text
        {
            height: 36px !important;
            font-size: 28px; 
        }
        <%--文字框頭 對齊右--%>
        .x-form-item-label-right
        {
            font-size: 24px; <%--x-large--%>
        }
       
        .Text-blue .x-form-field
        {
            color: blue;
        }
        .Text-blue-H .x-form-field
        {
            height: 24px;
            color: blue;
        }
        
        .label .x-label-value
        {
            height: 35px !important;
            font-size: 32px; 
        }
        /* Text items */
        .big-text .x-toolbar-text {
            font-size: 24px;
        }

        /* Button text */
        .big-text .x-btn-default-toolbar-small .x-btn-inner {
            font-size: 14px;
        }
        
        .x-border-box .x-form-trigger
        {
            height: 36px !important;
            width: 17px !important;
            background-image: url("../Styles/trigger.png");
            cursor: pointer;
        }
        
        .x-form-checkbox, .x-form-radio
        {
            width: 26px;
            height: 26px;
            background-image: url("../Styles/che_btn.png");
        }
       
        .Text-red .x-form-field
        {
            color: red;
        }
        .Text-red-H .x-form-field
        {
            height: 24px;
            color: red;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" />
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="sTIME" runat="server" />
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="sFLOOR" runat="server" />
        <ext:Hidden ID="sAREA" runat="server" />
        <ext:Hidden ID="sBED_NO" runat="server" />
        <ext:Hidden ID="dtFLOOR" runat="server" />
        <ext:Hidden ID="HIGHT" runat="server" />
        <ext:Hidden ID="WIDTH" runat="server" />
        <ext:Hidden ID="ROW_CNT" runat="server" />

        <ext:Panel ID="Panel1_w" runat="server" Region="West" Layout="HBoxLayout" Title="報到" Split="true" Collapsible="true">
            <Items>
                <ext:Panel ID="Panel11" runat="server" Region="West" Width="300">
                    <Items>
                        <ext:Image ID="Image2" runat="server" ImageUrl="../Styles/visit300.jpg" Height="95" PaddingSpec="6 0 0 6" />
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel2" runat="server" Region="Center" Border="false" >
                    <Items>
                        <ext:Container ID="Container1" runat="server" Frame="true" Layout="HBoxLayout" Padding="6" >
                            <Items>
                                <ext:DateField ID="txtTIME" runat="server" Vtype="daterange" FieldLabel="日期" EnableKeyEvents="true" Format="yyyy-MM-dd" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" Width="370" ReadOnly="true">
                                    <DirectEvents>
                                        <Change OnEvent="ChangeDate_Click"></Change>
                                    </DirectEvents>
                                </ext:DateField>
                                <ext:Label ID="GAP1" runat="server" Text="" Width="10" />
                                <ext:TextField ID="txtWEEK" runat="server" FieldLabel="星期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly="true" Width="180" />
                                <ext:Label ID="GAP2" runat="server" Text="" Width="10" />
                                <ext:Button ID="BtnSearch" runat="server" Icon="FolderExplore" IconAlign="Left" Text="查询" Scale="Medium" UI="Primary">
                                    <DirectEvents>
                                        <Click OnEvent="BtnSearch_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button runat="server" ID="BtnPreset" Icon="FolderExplore" IconAlign="Left" Text="处方模板" Scale="Medium" UI="Primary">
                                    <DirectEvents>
                                        <Click OnEvent="BtnPreset_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Frame="true" Layout="HBoxLayout" Padding="6" >
                            <Items>
                                <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" >
                                    <Items>
                                        <ext:ListItem Value="001" Text="上午" />
                                        <ext:ListItem Value="002" Text="下午" />
                                        <ext:ListItem Value="003" Text="晚班" />
                                    </Items>
                                    <DirectEvents>
                                        <Select OnEvent = "cboTIME_Click" />
                                    </DirectEvents>
                                </ext:SelectBox>
                                <ext:Label ID="GAP3" runat="server" Text="" Width="10" />
                                <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" >
                                    <DirectEvents>
                                        <Select OnEvent = "cboFLOOR_Click" />
                                    </DirectEvents>
                                </ext:SelectBox>
                                <ext:Label ID="Label2" runat="server" Text="" Width="10" />
                                <ext:SelectBox ID="cboAREA" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" >
                                    <DirectEvents>
                                        <Select OnEvent = "cboAREA_Click" />
                                    </DirectEvents>
                                </ext:SelectBox>
                                <ext:Label ID="Label3" runat="server" Text="" Width="10" />
                                <ext:Button ID="BtnLogin" runat="server" Icon="Lock" IconAlign="Left" Text="登出" Scale="Medium" UI="Danger" >
                                    <DirectEvents>
                                        <Click OnEvent="BtnLogout_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:FormPanel runat="server" ID="FormPanel1" Title="FormPanel1" Width="0" Height="0">
                                    <Items>
                                        <ext:TextField runat="server" ID="TextQuery" />
                                    </Items>
                                    <Buttons>
                                        <ext:Button runat="server" ID="BtnQuery" Type="Submit" Text="">
                                            <DirectEvents>
                                                <Click OnEvent="BtnQuery_Click">
                                                    <EventMask ShowMask="true" MinDelay="500" CustomTarget="FormPanel1" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Buttons>
                                </ext:FormPanel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Panel>
        <ext:Window ID="Window1" runat="server" Title="" Hidden="true" Width="360" Height="120">
            <Items> 
                <ext:ComboBox ID="cb_patlist" runat="server" FieldLabel="姓名" LabelWidth="120" ColumnWidth="1" IndicatorText="*" IndicatorCls="Text-red" Cls="Text-blue" LabelAlign="Right" PaddingSpec="2 10 2 2"
                    DisplayField="patname" ValueField="patname" TypeAhead="false" PageSize="20" HideBaseTrigger="true" MinChars="1" TriggerAction="Query">
                    <Store>
                        <ext:Store ID="Store2" runat="server" AutoLoad="true">
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
                    <ListConfig LoadingText="寻找中...">
                        <ItemTpl ID="ItemTpl1" runat="server">
                            <Html>
                                <div>
                                    <h1>{patname}</h1>
                                </div>
                            </html>
                        </ItemTpl>
                    </ListConfig>
                </ext:ComboBox>
                <%--<ext:TextField ID="pname" runat="server" FieldLabel="寻找" LabelAlign="Right" ColumnWidth=".5" Padding="2">--%>
                    <%--<Listeners>--%>
                        <%--<KeyDown Fn="enterKeyPressHandler" />--%>
                    <%--</Listeners>--%>
                <%--</ext:TextField>--%>
            </Items>
            <Buttons>
                <ext:Button ID="BtnQueryPat" runat="server" Icon="Accept" Text="确认" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="BtnQueryPat_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="BtnCancel2" runat="server" Icon="Cancel" Text="取消" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="BtnCancel_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </form>       
</body>
</html>
