<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_02_01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_02_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>血管通路</title>
    <style type="text/css">
        .x-grid-custom .x-grid-item TD {
            color: Black;
            font-size : 14px;
            font-weight:bold;
        }

        .x-grid-custom .x-column-header {
            /*background : #718CA1 url(css/header_sprite.png) repeat scroll 0 bottom;*/
            background-color: #5ABCE0;
            font-size: 16px;
            border-left-color  : #6085A5;
            border-right-color : #728BA1;
        }

        .x-grid-custom .x-column-header-over {
            /*background : #ebf3fd url(css/header_sprite_over.png) repeat 0 bottom !important;*/
        }

        .x-grid-custom .x-column-header div {
            font-size  : 16px;
            color: Black;
        }

        .x-grid-custom .company-link {
            color : #0E3D4F;
        }

        .x-grid-custom .x-column-header-trigger {
            /*background : #718CA1 url(css/grid3-hd-btn.png) no-repeat left center;*/
            color:Blue;
        }

        .x-grid-custom .x-grid-item-alt .x-grid-cell {
            background-color : #DAE2E8;
        }

        .x-grid-custom .x-grid-item-over .x-grid-cell {
            border-color : #728BA1;
            /*background   : url(css/row-over.png);*/
            background-color: Yellow;
        }

        .x-grid-custom .x-grid-item-selected .x-grid-cell {
            /*background   : url(css/row-selected.png) repeat-x scroll 0 0 #7BBBCF;*/
            background-color:#5ABCE0;
            border-color : #728BA1;
            border-style : solid;
        }
        .x-grid-custom .x-grid-editor .x-form-text
        {
            font-size:14px;
            font-weight:bold;
            color:Blue;
        }

        .x-grid-custom .x-grid-item-selected TD {
            color : Black;
        }

        .x-grid-custom .x-toolbar .x-toolbar-text {
            color : Black;
        }
        
        table, th, td {
            border-width:1px;
            border-style: outset;
            border-color:Gray;            
        }
        .trheadcolor
        {
            background-color:#5ABCE0;
        }
        p {
            display: block;
            font-size:14px;
            font-weight:bold; 
            margin:5px 5px 5px 5px;           
        }
        </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血管通路" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Title="血管通路" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="血管通路类型" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button8" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_1">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_1_1" runat="server" BoxLabel="临时中心静脉置管" Name="opt_1" />
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="长期中心静脉置管" Name="opt_1" />
                                                <ext:Radio ID="opt_1_3" runat="server" BoxLabel="自体内瘘" Name="opt_1" />
                                                <ext:Radio ID="opt_1_4" runat="server" BoxLabel="移植血管" Name="opt_1" />
                                                <ext:Radio ID="opt_1_5" runat="server" BoxLabel="其它" Name="opt_1" />
                                                <%--<ext:Radio ID="opt_1_1" runat="server" FieldLabel="通路类型" BoxLabel="自体内瘘" Name="opt_1" />
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="移植内瘘" Name="opt_1" />
                                                <ext:Radio ID="opt_1_3" runat="server" BoxLabel="直接穿刺V-V" Name="opt_1" />
                                                <ext:Radio ID="opt_1_4" runat="server" BoxLabel="直接穿刺A-V" Name="opt_1" />
                                                <ext:Radio ID="opt_1_5" runat="server" BoxLabel="颈内V长期导管" Name="opt_1" />
                                                <ext:Radio ID="opt_1_6" runat="server" BoxLabel="颈内V临时导管" Name="opt_1" />
                                                <ext:Radio ID="opt_1_7" runat="server" BoxLabel="颈外V长期导管" Name="opt_1" />
                                                <ext:Radio ID="opt_1_8" runat="server" BoxLabel="劲外V临时导管" Name="opt_1" />
                                                <ext:Radio ID="opt_1_9" runat="server" BoxLabel="股V长期导管" Name="opt_1" />
                                                <ext:Radio ID="opt_1_10" runat="server" BoxLabel="股V临时导管" Name="opt_1" />--%>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="血管通路位置（左-右）" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button1" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_2">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_2_1" runat="server" BoxLabel="左" Name="opt_2" />
                                                <ext:Radio ID="opt_2_2" runat="server" BoxLabel="右" Name="opt_2" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="血管通路位置" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_3">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_3_1" runat="server" BoxLabel="颈内静脉" Name="opt_3" />
                                                <ext:Radio ID="opt_3_2" runat="server" BoxLabel="股静脉" Name="opt_3" />
                                                <ext:Radio ID="opt_3_3" runat="server" BoxLabel="锁骨下静脉" Name="opt_3" />
                                                <ext:Radio ID="opt_3_4" runat="server" BoxLabel="颈外静脉" Name="opt_3" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="中心静脉置管方法" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_4">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_4_1" runat="server" BoxLabel="超声介入" Name="opt_4" />
                                                <ext:Radio ID="opt_4_2" runat="server" BoxLabel="X线介入" Name="opt_4" />
                                                <ext:Radio ID="opt_4_3" runat="server" BoxLabel="盲穿" Name="opt_4" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="血管通路位置" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_5">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_5_1" runat="server" BoxLabel="前臂" Name="opt_5" />
                                                <ext:Radio ID="opt_5_2" runat="server" BoxLabel="上臂" Name="opt_5" />
                                                <ext:Radio ID="opt_5_3" runat="server" BoxLabel="下肢" Name="opt_5" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Title="改变原因" Layout="AnchorLayout" Collapsible="true" Collapsed="true" Flex="1">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_6_1" runat="server" BoxLabel="内瘘成熟" />
                                                <ext:Checkbox ID="chk_6_2" runat="server" BoxLabel="内瘘堵塞" />
                                                <ext:Checkbox ID="chk_6_3" runat="server" BoxLabel="内瘘狭窄" />
                                                <ext:Checkbox ID="chk_6_4" runat="server" BoxLabel="导管感染 " />
                                                <ext:Checkbox ID="chk_6_5" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="....其它改变原因 " />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="....其它血管通路类型" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet7" runat="server" Title="照片上传" Layout="AnchorLayout" Collapsible="true" Flex="1">                                          
                                            <Items>
                                                <ext:FileUploadField ID="UploadImage" runat="server" Icon="ImageAdd" ButtonText="选择照片" ButtonOnly="true">
                                                    <DirectEvents>
                                                        <Change OnEvent="GetPatImg" IsUpload="true" />
                                                    </DirectEvents>
                                                </ext:FileUploadField> 
                                                <ext:Image ID="Image1" runat="server" Width="640" Height="480" ImageUrl="" />
                                                <ext:TextField ID="txt_9" runat="server" Hidden="true" />
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_clear" runat="server" Icon="Disk" Text="重置" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Clear_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Btn_Close" runat="server" Icon="Disk" Text="关闭" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
                <ext:Panel ID="Panel1" runat="server" AutoScroll="true" Visible="false">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Cls="x-grid-custom" Layout="FitLayout">
                            <TopBar>
                                <ext:Toolbar ID="Toolbar1" runat="server">
                                    <Items>
                                        <ext:Button ID="btn_Add" runat="server" Text="添加" Icon="Add" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="btn_Add_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                            <Store>
                                <ext:Store ID="Store1" runat="server" OnReadData="RefreshDataSet" IgnoreExtraFields="false">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" />
                                    </Model>
                                </ext:Store>
                            </Store>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
                            </SelectionModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
