<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_PreSetShort.aspx.cs" Inherits="Dialysis_Chart_Show.ipad.ipad_PreSetShort" %>
<%@ Register src="../checkin/DrugMod_List.ascx" tagname="WindowGetMod" tagprefix="uc1" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>短期医嘱用药</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <style type="text/css">    
    .formLabel
    {
        color: White;
        font-size:14px;
        font-weight:normal;
    }
    
    .formLabelYellow
    {
        color: Yellow;
        font-size:14px;
        font-weight:normal;
    }
    
    .x-form-text-default
    {
        font-size:14px;
        font-weight:normal;
    }
        
    .Text-blue .x-form-field
    {
        color: Blue;
        font-size:14px;
        font-weight:normal;
    }
    .text-readonly .x-form-text-default
    {
        color:Blue;
        font-size:14px;
        font-weight:normal;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <uc1:WindowGetMod ID="WindowGetMod" runat="server" />
        <ext:Hidden ID="Patient_ID" runat="server" />
        
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel_North" runat="server" Title="短期医嘱用药" Region="North" Header="false" AutoScroll="true">
                    <Items>
                        <ext:GridPanel ID="Grid_Short_Term" runat="server" Title="短期医嘱用药" Header="false" Frame="true" Height="500" Cls="x-grid-custom">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar2" runat="server">
                                    <Items>
                                        <ext:Button ID="Button5" runat="server" Text="调用模板" Icon="FolderMagnify" UI="Primary">
                                            <DirectEvents>
                                                <Click OnEvent="BtnModS_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="BtnAddS" runat="server" Text="增加" Icon="ApplicationFormEdit" UI="Primary">
                                            <DirectEvents>
                                                <Click OnEvent="BtnAddShort_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btnPrintS" runat="server" Icon="PrinterColor" IconAlign="Left" Text="打印" Padding="5" UI="Success" Hidden="true"> 
                                            <DirectEvents>
                                                <Click OnEvent="OnbtnPrintS_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:ToolbarFill ID="ToolbarFill2" runat="server" />
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" Name="PastSickness">
                                            <Fields>
                                                <ext:ModelField Name="shord_id" Type="String" />
                                                <ext:ModelField Name="shord_dateord" Type="String" />
                                                <ext:ModelField Name="shord_timeord" Type="String" />
                                                <ext:ModelField Name="shord_usr1" Type="String" />
                                                <ext:ModelField Name="drug_name" Type="String" />
                                                <ext:ModelField Name="shord_intake" Type="String" />
                                                <ext:ModelField Name="shord_freq"   Type="String" />
                                                <ext:ModelField Name="shord_medway" Type="String" />
                                                <ext:ModelField Name="shord_comment" Type="String" />
                                                <ext:ModelField Name="shord_dtactst" Type="String" />
                                                <ext:ModelField Name="Status" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="shord_dateord" Direction="DESC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel2" runat="server">
                                <Columns>
                                    <ext:Column ID="Column4" runat="server" DataIndex="shord_dateord" Text="日期" Width="100" />  
                                    <ext:Column ID="Column5" runat="server" DataIndex="shord_timeord" Text="记录时间" Width="70" />
                                    <ext:Column ID="Column7" runat="server" DataIndex="shord_usr1" Text="经治医生" Width="70" />
                                    <ext:Column ID="Column8" runat="server" DataIndex="drug_name" Text="药品名称" Width="190" />
                                    <ext:Column ID="Column22" runat="server" DataIndex="shord_intake" Text="剂量" Width="90" />
                                    <ext:Column ID="Column23" runat="server" DataIndex="shord_freq" Text="频率" Width="80" />
                                    <ext:Column ID="Column24" runat="server" DataIndex="shord_medway" Text="给药方式" Width="100" />
                                    <ext:Column ID="Column12" runat="server" DataIndex="shord_comment" Text="备注" Width="100" />
                                    <ext:Column ID="Column18" runat="server" DataIndex="shord_dtactst" Text="停用日期" Width="100" />
                                    <ext:Column ID="Column13" runat="server" DataIndex="Status" Text="状态" Flex="1">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Edit" Icon="Pencil" Style="margin-left:5px !important;" >
                                                <ToolTip Text="修改" />
                                            </ext:ImageCommand>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="EditShort_Click" >
                                                <ExtraParams>
                                                    <ext:Parameter Name="id" Value="record.data.shord_id" Mode="Raw"/>
                                                    <ext:Parameter Name="name" Value="record.data.drug_name" Mode="Raw"/>
                                                    <ext:Parameter Name="intake" Value="record.data.shord_intake" Mode="Raw"/>
                                                    <ext:Parameter Name="freq" Value="record.data.shord_freq" Mode="Raw"/>
                                                    <ext:Parameter Name="medway" Value="record.data.shord_medway" Mode="Raw"/>
                                                    <ext:Parameter Name="comment" Value="record.data.shord_comment" Mode="Raw"/>
                                                </ExtraParams> 
                                            </Command> 
                                        </DirectEvents>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" StoreID="Store1" />
                            </BottomBar>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
        <ext:Window ID="Window_Drug" runat="server" Width="515" Height="380" BodyPadding="5" Modal="true" Hidden="true" UI="Primary">
            <Items>
                <ext:Container ID="Container_Long1" runat="server" Layout="FitLayout">
                    <Items>
                        <ext:Hidden ID="drugkind" runat="server" />
                        <ext:Hidden ID="id" runat="server" />
                        <ext:ComboBox ID="cb_druglist" runat="server" FieldLabel="药品名称" LabelWidth="130" ColumnWidth="1" IndicatorText="*" 
                            LabelAlign="Right" PaddingSpec="2 10 2 2"
                            DisplayField="drugname" ValueField="drugname" TypeAhead="false" PageSize="10" 
                            HideBaseTrigger="true" MinChars="1" TriggerAction="Query">
                            <Store>
                                <ext:Store ID="Store3" runat="server" AutoLoad="true">
                                    <Proxy>
                                        <ext:AjaxProxy Url="../checkin/Drugs.ashx">
                                            <ActionMethods Read="POST" />
                                            <Reader>
                                                <ext:JsonReader RootProperty="drugs" TotalProperty="total" />
                                            </Reader>
                                        </ext:AjaxProxy>
                                    </Proxy>
                                    <Model>
                                        <ext:Model ID="Model3" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="py" />
                                                <ext:ModelField Name="drugname" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ListConfig LoadingText="寻找中...">
                                <ItemTpl ID="ItemTpl1" runat="server">
                                    <Html>
                                        <div>
                                            <h1>{drugname}</h1>
                                        </div>
                                    </html>
                                </ItemTpl>
                            </ListConfig>
                        </ext:ComboBox>
                        <ext:TextField ID="txt_ordcount" runat="server" FieldLabel="剂量" LabelWidth="130" ColumnWidth="1" LabelAlign="Right" PaddingSpec="2 10 2 2" />
                        <ext:SelectBox ID="cb_medway" runat="server" FieldLabel="給藥方式" LabelWidth="130" ColumnWidth="1" LabelAlign="Right" PaddingSpec="2 10 2 2" />                        
                        <ext:SelectBox ID="cb_ordfreq" runat="server" FieldLabel="频率" LabelWidth="130" ColumnWidth="1" LabelAlign="Right" PaddingSpec="2 10 2 2" />                        
                        <ext:TextArea ID="txt_ordremark" runat="server" FieldLabel="备注" LabelWidth="130" ColumnWidth="1" LabelAlign="Right" PaddingSpec="2 10 2 2" />                
                    </Items>
                </ext:Container> 
                <ext:Container ID="Container66" runat="server" Layout="HBoxLayout">
                    <Items>
                        <ext:Button ID="BtnStop" runat="server" Icon="Stop" Text="停用" Width="90">
                            <DirectEvents>
                                <Click OnEvent="BtnStop_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container9" runat="server" Layout="HBoxLayout">
                    <Items>
                        <ext:Button ID="BtnDelete" runat="server" Icon="Delete" Text="删除" Width="90">
                            <DirectEvents>
                                <Click OnEvent="BtnDelete_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
            </Items>
            <Buttons>
                <ext:Button ID="BtnAccept" runat="server" Icon="Accept" Text="确认" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="BtnAccept_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="BtnCancel" runat="server" Icon="Cancel" Text="取消" Width="150" Height="30">
                    <DirectEvents>
                        <Click OnEvent="BtnCancel_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
        <ext:Window ID="PrintWindow" runat="server" Y="10" Width="900" Height="700" Modal="true" AutoRender="false" Hidden="true">
            <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
    </form>
</body>
</html>