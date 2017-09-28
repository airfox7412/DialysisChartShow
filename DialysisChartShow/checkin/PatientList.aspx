<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientList.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.PatientList" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化系统-報到</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <style type="text/css">
    .Panellogo .x-autocontainer-innerCt
    {
        /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#1e5799+0,2989d8+100,207cca+100,7db9e8+100 */
        background: #1e5799; /* Old browsers */
        background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
        background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
        background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0 ); /* IE6-9 */ 
    }
    .x-label-value, .x-form-item-label-default
    {
        font-weight:bold;
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
        <ext:Hidden ID="txtWEEK" runat="server" />
        <ext:Hidden ID="sFLOOR" runat="server" />
        <ext:Hidden ID="sAREA" runat="server" />
        <ext:Hidden ID="sBED_NO" runat="server" />
        <ext:Hidden ID="dtFLOOR" runat="server" />
        <ext:Hidden ID="HIGHT" runat="server" />
        <ext:Hidden ID="WIDTH" runat="server" />
        <ext:Hidden ID="ROW_CNT" runat="server" /> 
        <ext:FormPanel runat="server" ID="FormPanel1" Title="FormPanel1" Width="00" Height="0">
            <Items>
                <ext:TextField ID="TextQuery" runat="server" Text="0800933288"/>
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
        <%--<ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>--%>
                <ext:Panel ID="Panel_Left" runat="server" Title="病患报到" Region="North" AutoScroll="true" Header="false" Cls="Panellogo">
                    <Items>
                        <ext:Container ID="Container1" runat="server">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:GridPanel ID="grdBED_LIST" runat="server" Cls="x-grid-custom" Width="1200">
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar1" runat="server">
                                            <Items>
                                                <ext:Label ID="Label1" runat="server" Text="日期" ColumnWidth=".2" />
                                                <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="150">
                                                    <DirectEvents>
                                                        <Select OnEvent = "cboFLOOR_Click" />
                                                    </DirectEvents>
                                                </ext:SelectBox>
                                                <ext:SelectBox ID="cboAREA" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="150">
                                                    <DirectEvents>
                                                        <Select OnEvent="cboAREA_Click" />
                                                    </DirectEvents>
                                                </ext:SelectBox>
                                                <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="150">
                                                    <Items>
                                                        <ext:ListItem Value="001" Text="上午" />
                                                        <ext:ListItem Value="002" Text="下午" />
                                                        <ext:ListItem Value="003" Text="晚班" />
                                                    </Items>
                                                    <DirectEvents>
                                                        <Select OnEvent = "cboTIME_Click" />
                                                    </DirectEvents>
                                                </ext:SelectBox> 
                                                <ext:ComboBox ID="cb_patlist" runat="server" FieldLabel="姓名" LabelWidth="60" IndicatorText="*" IndicatorCls="emptyColor" 
                                                    LabelAlign="Right" DisplayField="patname" ValueField="patname" 
                                                    TypeAhead="false" HideBaseTrigger="true" PageSize="10" MinChars="1" TriggerAction="Query"
                                                    PaddingSpec="2 10 2 2" EmptyText="可直接进行刷卡" EmptyCls="emptyColor">
                                                    <Store>
                                                        <ext:Store ID="Store3" runat="server" AutoLoad="true">
                                                            <Proxy>
                                                                <ext:AjaxProxy Url="../Patinfos.ashx">
                                                                    <ActionMethods Read="POST" />
                                                                    <Reader>
                                                                        <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                                    </Reader>
                                                                </ext:AjaxProxy>
                                                            </Proxy>
                                                            <Model>
                                                                <ext:Model ID="Model3" runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="patic" />
                                                                        <ext:ModelField Name="patname" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                    <ListConfig LoadingText="寻找中...">
                                                        <ItemTpl ID="ItemTpl2" runat="server">
                                                            <Html>
                                                                <div>
                                                                    <h1>{patname}</h1>
                                                                </div>
                                                            </html>
                                                        </ItemTpl>
                                                    </ListConfig>
                                                </ext:ComboBox>
                                                <ext:Button ID="BtnSearch" runat="server" Icon="FolderExplore" IconAlign="Left" Text="查询" Padding="5">
                                                    <DirectEvents>
                                                        <Click OnEvent="BtnSearch_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Store>
                                        <ext:Store ID="Store1" runat="server" OnReadData="REFRESH_BED" >
                                            <Model>
                                                <ext:Model ID="Model1" runat="server" Name="recordlist2">
                                                    <Fields>
                                                        <ext:ModelField Name="AREA" Type="String" />
                                                        <ext:ModelField Name="BED_NO" Type="String" />
                                                        <ext:ModelField Name="MAC_MODEL" Type="String" />
                                                        <ext:ModelField Name="MAC_TYPE" Type="String" />
                                                        <ext:ModelField Name="MAC_STATE" Type="String" />
                                                        <ext:ModelField Name="PERSON_NAME" Type="String" />
                                                        <ext:ModelField Name="PERSON_ID" Type="String" />
                                                        <ext:ModelField Name="PERSON_SEX" Type="String" />
                                                        <ext:ModelField Name="PERSON_HEIGHT" Type="String" />
                                                        <ext:ModelField Name="PERSON_WEIGHT" Type="String" />
                                                        <ext:ModelField Name="PERSON_STATE" Type="String" />
                                                    </Fields>
                                                </ext:Model>
                                            </Model>
                                            <Reader>
                                                <ext:ArrayReader />
                                            </Reader>
                                        </ext:Store>
                                    </Store>
                                    <ColumnModel ID="ColumnModel1" runat="server">
                                        <Columns>
                                            <ext:Column ID="Column1" runat="server" Text="ID" DataIndex="PERSON_ID" Width="70" Visible="false" />
                                            <ext:Column ID="Column2" runat="server" Text="区" DataIndex="AREA" Width="50" />
                                            <ext:Column ID="Column3" runat="server" Text="床号" DataIndex="BED_NO" Width="70" />
                                            <ext:Column ID="Column4" runat="server" Text="透析器型号" DataIndex="MAC_MODEL" Width="200" />
                                            <ext:Column ID="Column5" runat="server" Text="型号" DataIndex="MAC_TYPE" Width="70" />
                                            <ext:Column ID="Column6" runat="server" Text="状态" DataIndex="MAC_STATE" Width="90" />
                                            <ext:Column ID="Column7" runat="server" Text="姓名" DataIndex="PERSON_NAME" Width="130" />
                                            <ext:Column ID="Column8" runat="server" Text="性别" DataIndex="PERSON_SEX" Width="70" />
                                            <ext:Column ID="Column9" runat="server" Text="身高" DataIndex="PERSON_HEIGHT" Width="80" />
                                            <ext:Column ID="Column10" runat="server" Text="体重" DataIndex="PERSON_WEIGHT" Width="80" />
                                            <ext:Column ID="Column11" runat="server" Text="已报到" DataIndex="PERSON_STATE" Flex="1">
                                        </Columns>
                                    </ColumnModel>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                                            <DirectEvents>
                                                <Select OnEvent="RowSelect">
                                                    <ExtraParams>
                                                        <ext:Parameter Name="Values" Value="#{grdBED_LIST}.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                                    </ExtraParams>
                                                </Select>
                                            </DirectEvents>
                                        </ext:RowSelectionModel>
                                    </SelectionModel>
                                </ext:GridPanel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            <%--</Items>
        </ext:Viewport>--%>
    </form>       
</body>
</html>
