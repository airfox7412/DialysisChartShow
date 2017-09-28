<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_03_04.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_03_04" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="活性维生素D" AutoScroll="true" ButtonAlign="Center">
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
                                                <ext:Button ID="Button16" runat="server" Icon="ScriptDelete" ToolTip="清空">
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
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="活性维生素D" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
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
                                                <ext:Radio ID="opt_2_1" runat="server" BoxLabel="口服" Name="opt_2" FieldLabel="服药方式" />
                                                <ext:Radio ID="opt_2_2" runat="server" BoxLabel="静脉" Name="opt_2" />
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
                                                <ext:Radio ID="opt_3_1" runat="server" BoxLabel="罗盖全" Name="opt_3" FieldLabel="口服" />
                                                <ext:Radio ID="opt_3_2" runat="server" BoxLabel="盖三醇" Name="opt_3" />
                                                <ext:Radio ID="opt_3_3" runat="server" BoxLabel="阿法迪三" Name="opt_3" />
                                                <ext:Radio ID="opt_3_4" runat="server" BoxLabel="其它" Name="opt_3" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="其它请说明" />
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
                                                <ext:Radio ID="opt_5_1" runat="server" BoxLabel="溉纯" Name="opt_5" FieldLabel="静脉" />
                                                <ext:Radio ID="opt_5_2" runat="server" BoxLabel="其它" Name="opt_5" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="其它请说明 " />
                                                <ext:TextField ID="num_7" runat="server" FieldLabel="剂量" IndicatorText="ug" />
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_8">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_8_1" runat="server" BoxLabel="日" Name="opt_8" FieldLabel="单位时间" />
                                                <ext:Radio ID="opt_8_2" runat="server" BoxLabel="周" Name="opt_8" />
                                                <ext:Radio ID="opt_8_3" runat="server" BoxLabel="2周" Name="opt_8" />
                                                <ext:Radio ID="opt_8_4" runat="server" BoxLabel="4周" Name="opt_8" />
                                                <ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_9">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_9_1" runat="server" BoxLabel="停药" Name="opt_9" FieldLabel="用药次数" />
                                                <ext:Radio ID="opt_9_2" runat="server" BoxLabel="1次" Name="opt_9" />
                                                <ext:Radio ID="opt_9_3" runat="server" BoxLabel="2次" Name="opt_9" />
                                                <ext:Radio ID="opt_9_4" runat="server" BoxLabel="3次" Name="opt_9" />
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
