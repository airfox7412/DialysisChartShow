<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_03_01.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_03_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="促红素" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:FieldSet ID="FieldSet8" runat="server" Flex="1" Title="是否使用" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_1">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_1_1" runat="server" BoxLabel="是" Name="opt_1" />
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="否" Name="opt_1" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="处方日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="促红素名称" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
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
                                                <ext:Radio ID="opt_2_1" runat="server" BoxLabel="国产" Name="opt_2" FieldLabel="种类" />
                                                <ext:Radio ID="opt_2_2" runat="server" BoxLabel="进口" Name="opt_2" />
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
                                                <ext:Radio ID="opt_3_1" runat="server" BoxLabel="益比奥" Name="opt_3" FieldLabel="国产" />
                                                <ext:Radio ID="opt_3_2" runat="server" BoxLabel="济脉欣" Name="opt_3" />
                                                <ext:Radio ID="opt_3_3" runat="server" BoxLabel="环尔博" Name="opt_3" />
                                                <ext:Radio ID="opt_3_4" runat="server" BoxLabel="宁红欣" Name="opt_3" />
                                                <ext:Radio ID="opt_3_5" runat="server" BoxLabel="依倍" Name="opt_3" />
                                                <ext:Radio ID="opt_3_6" runat="server" BoxLabel="佳林豪" Name="opt_3" />
                                                <ext:Radio ID="opt_3_7" runat="server" BoxLabel="雪达升" Name="opt_3" />
                                                <ext:Radio ID="opt_3_8" runat="server" BoxLabel="怡宝" Name="opt_3" />
                                                <ext:Radio ID="opt_3_9" runat="server" BoxLabel="依普定" Name="opt_3" />
                                                <ext:Radio ID="opt_3_10" runat="server" BoxLabel="其它" Name="opt_3" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="其它国产促红素说明" />
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
                                                <ext:Radio ID="opt_5_1" runat="server" BoxLabel="利血宝" Name="opt_5" FieldLabel="进口" />
                                                <ext:Radio ID="opt_5_2" runat="server" BoxLabel="达依泊汀" Name="opt_5" />
                                                <ext:Radio ID="opt_5_3" runat="server" BoxLabel="促红素受体激动剂" Name="opt_5" />
                                                <ext:Radio ID="opt_5_4" runat="server" BoxLabel="其它" Name="opt_5" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="其它进口促红素说明" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="给药方式及剂量 " Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_7">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_7_1" runat="server" BoxLabel="皮下" Name="opt_7" FieldLabel="给药方式" />
                                                <ext:Radio ID="opt_7_2" runat="server" BoxLabel="静脉" Name="opt_7" />
                                                <ext:TextField ID="num_8" runat="server" FieldLabel="单次剂量" IndicatorText="IU" />
                                                <ext:TextField ID="num_9" runat="server" FieldLabel="单次剂量" IndicatorText="mg" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="用药频率" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button6" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_10">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_10_1" runat="server" BoxLabel="停药" Name="opt_10" FieldLabel="用药次数" />
                                                <ext:Radio ID="opt_10_2" runat="server" BoxLabel="1次" Name="opt_10" />
                                                <ext:Radio ID="opt_10_3" runat="server" BoxLabel="2次" Name="opt_10" />
                                                <ext:Radio ID="opt_10_4" runat="server" BoxLabel="3次" Name="opt_10" />
                                                <ext:Radio ID="opt_10_5" runat="server" BoxLabel="4次" Name="opt_10" />
                                                <ext:Radio ID="opt_10_6" runat="server" BoxLabel="5次" Name="opt_10" />
                                                <ext:Button ID="Button7" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_11">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_11_1" runat="server" BoxLabel="日" Name="opt_11" FieldLabel="单位时间" />
                                                <ext:Radio ID="opt_11_2" runat="server" BoxLabel="周" Name="opt_11" />
                                                <ext:Radio ID="opt_11_3" runat="server" BoxLabel="2周" Name="opt_11" />
                                                <ext:Radio ID="opt_11_4" runat="server" BoxLabel="4周" Name="opt_11" />
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
