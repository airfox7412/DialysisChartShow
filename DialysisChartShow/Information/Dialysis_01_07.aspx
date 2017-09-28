<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_01_07.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_01_07" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">        
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
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="转归情况" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="转归情况" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
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
                                                <ext:Radio ID="opt_1_1" runat="server" BoxLabel="退出" Name="opt_1" />
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="肾移植" Name="opt_1" />
                                                <ext:Radio ID="opt_1_3" runat="server" BoxLabel="转出" Name="opt_1" />
                                                <ext:Radio ID="opt_1_4" runat="server" BoxLabel="死亡" Name="opt_1" />
                                                <ext:Radio ID="opt_1_5" runat="server" BoxLabel="转入" Name="opt_1" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="退出情况" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
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
                                                <ext:Radio ID="opt_2_1" runat="server" BoxLabel="脱离透析" Name="opt_2" />
                                                <ext:Radio ID="opt_2_2" runat="server" BoxLabel="转为腹膜透析" Name="opt_2" />
                                                <ext:Radio ID="opt_2_3" runat="server" BoxLabel="放弃治疗" Name="opt_2" />
                                                <ext:Radio ID="opt_2_4" runat="server" BoxLabel="其它" Name="opt_2" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="其它退出情况说明" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="转出地点 " Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_4">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_4_1" runat="server" BoxLabel="本市其它医院" Name="opt_4" />
                                                <ext:Radio ID="opt_4_2" runat="server" BoxLabel="本省其它城市医院" Name="opt_4" />
                                                <ext:Radio ID="opt_4_3" runat="server" BoxLabel="外省医院" Name="opt_4" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="转出原因" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_5">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_5_1" runat="server" BoxLabel="医保医院变更" Name="opt_5" />
                                                <ext:Radio ID="opt_5_2" runat="server" BoxLabel="住所变更" Name="opt_5" />
                                                <ext:Radio ID="opt_5_3" runat="server" BoxLabel="患者自主选择" Name="opt_5" />
                                                <ext:Radio ID="opt_5_4" runat="server" BoxLabel="其它" Name="opt_5" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="其它请说明 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="死亡原因" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_7_1" runat="server" BoxLabel="心血管事件" />
                                                <ext:Checkbox ID="chk_7_2" runat="server" BoxLabel="脑血管事件" />
                                                <ext:Checkbox ID="chk_7_3" runat="server" BoxLabel="感染" />
                                                <ext:Checkbox ID="chk_7_4" runat="server" BoxLabel="消化道出血等出血性疾病" />
                                                <ext:Checkbox ID="chk_7_5" runat="server" BoxLabel="其它" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="心血管事件" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_8_1" runat="server" BoxLabel="心律失常" />
                                                <ext:Checkbox ID="chk_8_2" runat="server" BoxLabel="急性左心衰" />
                                                <ext:Checkbox ID="chk_8_3" runat="server" BoxLabel="猝死" />
                                                <ext:Checkbox ID="chk_8_4" runat="server" BoxLabel="其它 " />
                                                <ext:TextField ID="txt_9" runat="server" FieldLabel="其它心血管事件" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet8" runat="server" Flex="1" Title="脑血管事件" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_10_1" runat="server" BoxLabel="脑出血" />
                                                <ext:Checkbox ID="chk_10_2" runat="server" BoxLabel="脑梗塞" />
                                                <ext:Checkbox ID="chk_10_3" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="其它脑血管事件" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet7" runat="server" Flex="1" Title="感染  " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_12_1" runat="server" BoxLabel="肺部感染 " />
                                                <ext:Checkbox ID="chk_12_2" runat="server" BoxLabel="消化道感染 " />
                                                <ext:Checkbox ID="chk_12_3" runat="server" BoxLabel="脓毒血症" />
                                                <ext:Checkbox ID="chk_12_4" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_13" runat="server" FieldLabel="其它感染 " />
                                                <ext:TextField ID="txt_14" runat="server" FieldLabel="其它死亡原因请说明" />
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
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
