<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_02_05.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_02_05" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="抗凝剂" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:Label ID="label1" runat="server" Text="注：每3个月填写一次" />
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Layout="AnchorLayout" Title="是否使用"  Collapsible="true" Collapsed="true">
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
                                                <ext:Radio ID="opt_1_1" runat="server" FieldLabel="是否使用" BoxLabel="是" Name="opt_1" />
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="否" Name="opt_1" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="抗凝剂" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
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
                                                <ext:Radio ID="opt_2_1" runat="server" FieldLabel="抗凝剂 " BoxLabel="无抗凝剂" Name="opt_2" />
                                                <ext:Radio ID="opt_2_2" runat="server" BoxLabel="肝素" Name="opt_2" />
                                                <ext:Radio ID="opt_2_3" runat="server" BoxLabel="低分子肝素" Name="opt_2" />
                                                <ext:Radio ID="opt_2_4" runat="server" BoxLabel="枸橼酸" Name="opt_2" />
                                                <ext:Radio ID="opt_2_5" runat="server" BoxLabel="其它" Name="opt_2" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="肝素" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="num_3" runat="server" FieldLabel="........肝素首剂量 " IndicatorText="mg" />
                                                <ext:TextField ID="num_4" runat="server" FieldLabel="........肝素追加剂量 " IndicatorText="mg/h" />
                                                <ext:TextField ID="num_5" runat="server" FieldLabel="........肝素总剂量 " IndicatorText="mg" />
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_6">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_6_1" runat="server" FieldLabel="........低分子肝素 " BoxLabel="低分子肝素钠"
                                                    Name="opt_6" />
                                                <ext:Radio ID="opt_6_2" runat="server" BoxLabel="低分子肝素钙" Name="opt_6" />
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_7">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_7_1" runat="server" FieldLabel="............低分子肝素钠" BoxLabel="法安明"
                                                    Name="opt_7" />
                                                <ext:Radio ID="opt_7_2" runat="server" BoxLabel="吉派林" Name="opt_7" />
                                                <ext:Radio ID="opt_7_3" runat="server" BoxLabel="齐征" Name="opt_7" />
                                                <ext:Radio ID="opt_7_4" runat="server" BoxLabel="希弗全" Name="opt_7" />
                                                <ext:Radio ID="opt_7_5" runat="server" BoxLabel="苏可诺" Name="opt_7" />
                                                <ext:Radio ID="opt_7_6" runat="server" BoxLabel="天道" Name="opt_7" />
                                                <ext:Radio ID="opt_7_7" runat="server" BoxLabel="其它" Name="opt_7" />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="................其它低分子肝素钠" />
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_9">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_9_1" runat="server" FieldLabel="............低分子肝素钙" BoxLabel="速碧林"
                                                    Name="opt_9" />
                                                <ext:Radio ID="opt_9_2" runat="server" BoxLabel="尤尼舒" Name="opt_9" />
                                                <ext:Radio ID="opt_9_3" runat="server" BoxLabel="立迈清" Name="opt_9" />
                                                <ext:Radio ID="opt_9_4" runat="server" BoxLabel="赛博利" Name="opt_9" />
                                                <ext:Radio ID="opt_9_5" runat="server" BoxLabel="博璞青" Name="opt_9" />
                                                <ext:Radio ID="opt_9_6" runat="server" BoxLabel="其它" Name="opt_9" />
                                                <ext:TextField ID="txt_10" runat="server" FieldLabel="................其他低分子肝素钙" />
                                                <ext:TextField ID="num_11" runat="server" FieldLabel="........低分子肝素首剂量" IndicatorText="IU" />
                                                <ext:TextField ID="num_12" runat="server" FieldLabel="........低分子肝素追加剂量" IndicatorText="IU" />
                                                <ext:TextField ID="num_13" runat="server" FieldLabel="........低分子肝素追加时间" IndicatorText="小时" />
                                                <ext:TextField ID="num_14" runat="server" FieldLabel="........低分子肝素总剂量" IndicatorText="IU" />
                                                <ext:TextField ID="txt_15" runat="server" FieldLabel="........其它抗凝剂" />
                                                <ext:TextField ID="txt_16" runat="server" FieldLabel="........首剂量" />
                                                <ext:TextField ID="txt_17" runat="server" FieldLabel="........追加剂量" />
                                                <ext:TextField ID="txt_18" runat="server" FieldLabel="........总剂量" />
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
