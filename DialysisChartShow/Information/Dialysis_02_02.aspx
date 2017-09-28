<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_02_02.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_02_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>透析处方</title>
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
                <ext:FormPanel ID="FormPanel1" runat="server" Title=" 透析处方（至少每3个月填写一次，每次处方改变也须填写）"
                    AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:Label ID="label1" runat="server" Text="【注：】至少每3个月填写一次，每次处方改变也须填写" />
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="处方日期" Format="yyyy-MM-dd" />
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="处方" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="num_1" runat="server" FieldLabel="HD次数" IndicatorText="次/每周【填写平均每周透析次数，可填写小数】" />
                                                <ext:TextField ID="num_2" runat="server" FieldLabel="HD治疗时间" IndicatorText="小时/次" />
                                                <ext:Button ID="Button8" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_3">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_3_1" runat="server" FieldLabel="HDF次数" BoxLabel="1次/周" Name="opt_3" />
                                                <ext:Radio ID="opt_3_2" runat="server" BoxLabel="1次/2周" Name="opt_3" />
                                                <ext:Radio ID="opt_3_3" runat="server" BoxLabel="1次/4周" Name="opt_3" />
                                                <ext:Radio ID="opt_3_4" runat="server" BoxLabel="其它" Name="opt_3" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="....其它HDF次数说明" />
                                                <ext:TextField ID="num_5" runat="server" FieldLabel="HDF治疗时间" IndicatorText="小时/次" />
                                                <ext:Button ID="Button1" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_6">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_6_1" runat="server" FieldLabel="HP次数" BoxLabel="1次/周" Name="opt_6" />
                                                <ext:Radio ID="opt_6_2" runat="server" BoxLabel="1次/2周" Name="opt_6" />
                                                <ext:Radio ID="opt_6_3" runat="server" BoxLabel="1次/4周" Name="opt_6" />
                                                <ext:Radio ID="opt_6_4" runat="server" BoxLabel="其它" Name="opt_6" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="....其它HP次数说明" />
                                                <ext:TextField ID="num_8" runat="server" FieldLabel="HP治疗时间" IndicatorText="小时/次" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="透析浓缩液" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_9_1" runat="server" FieldLabel="透析浓缩液" BoxLabel="透析浓缩A液" />
                                                <ext:Checkbox ID="chk_9_2" runat="server" BoxLabel="透析浓缩B液" />
                                                <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_10">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_10_1" runat="server" FieldLabel="....透析浓缩A液" BoxLabel="商品化成品"
                                                    Name="opt_10" />
                                                <ext:Radio ID="opt_10_2" runat="server" BoxLabel="商品化干粉" Name="opt_10" />
                                                <ext:Radio ID="opt_10_3" runat="server" BoxLabel="中心供液" Name="opt_10" />
                                                <ext:Radio ID="opt_10_4" runat="server" BoxLabel="自行配制" Name="opt_10" />
                                                <ext:Button ID="Button3" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_11">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_11_1" runat="server" FieldLabel="....透析液钾离子浓度" BoxLabel="2.0 mmol/L"
                                                    Name="opt_11" />
                                                <ext:Radio ID="opt_11_2" runat="server" BoxLabel="3.0 mmol/L" Name="opt_11" />
                                                <ext:Radio ID="opt_11_3" runat="server" BoxLabel="4.0 mmol/L" Name="opt_11" />
                                                <ext:Radio ID="opt_11_4" runat="server" BoxLabel="其它" Name="opt_11" />
                                                <ext:TextField ID="num_12" runat="server" FieldLabel="........其他钾离子浓度值" IndicatorText="mmol/L" />
                                                <ext:Button ID="Button4" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_13">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_13_1" runat="server" FieldLabel="....透析液钙离子浓度" BoxLabel="1.25 mmol/L"
                                                    Name="opt_13" />
                                                <ext:Radio ID="opt_13_2" runat="server" BoxLabel="1.5 mmol/L" Name="opt_13" />
                                                <ext:Radio ID="opt_13_3" runat="server" BoxLabel="1.75 mmol/L" Name="opt_13" />
                                                <ext:Radio ID="opt_13_4" runat="server" BoxLabel="其它" Name="opt_13" />
                                                <ext:TextField ID="num_14" runat="server" FieldLabel="........其他鈣离子浓度值" IndicatorText="mmol/L" />
                                                <ext:Button ID="Button5" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_15">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_15_1" runat="server" FieldLabel="....含糖透析液" BoxLabel="是" Name="opt_15" />
                                                <ext:Radio ID="opt_15_2" runat="server" BoxLabel="否" Name="opt_15" />
                                                <ext:Button ID="Button9" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_16">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_16_1" runat="server" FieldLabel="....氨基酸透析液" BoxLabel="是" Name="opt_16" />
                                                <ext:Radio ID="opt_16_2" runat="server" BoxLabel="否" Name="opt_16" />
                                                <ext:Button ID="Button10" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_17">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_17_1" runat="server" FieldLabel="....透析浓缩B液" BoxLabel="商品化成品"
                                                    Name="opt_17" />
                                                <ext:Radio ID="opt_17_2" runat="server" BoxLabel="商品化干粉" Name="opt_17" />
                                                <ext:Radio ID="opt_17_3" runat="server" BoxLabel="自行配制" Name="opt_17" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="透析器、滤器" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_18_1" runat="server" FieldLabel="类型" BoxLabel="国产" />
                                                <ext:Checkbox ID="chk_18_2" runat="server" BoxLabel="进口" />
                                                <ext:Button ID="Button6" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_19">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_19_1" runat="server" FieldLabel="通量" BoxLabel="高通量" Name="opt_19" />
                                                <ext:Radio ID="opt_19_2" runat="server" BoxLabel="低通量" Name="opt_19" />
                                                <ext:Button ID="Button11" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_20">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_20_1" runat="server" FieldLabel="使用" BoxLabel="一次性使用" Name="opt_20" />
                                                <ext:Radio ID="opt_20_2" runat="server" BoxLabel="复用" Name="opt_20" />
                                                <ext:Checkbox ID="chk_21_1" runat="server" FieldLabel="透析膜" BoxLabel="聚砜膜 " />
                                                <ext:Checkbox ID="chk_21_2" runat="server" BoxLabel="聚甲基丙烯酸甲酯膜" />
                                                <ext:Checkbox ID="chk_21_3" runat="server" BoxLabel="其它合成膜" />
                                                <ext:Checkbox ID="chk_21_4" runat="server" BoxLabel="醋酸纤维膜" />
                                                <ext:Checkbox ID="chk_21_5" runat="server" BoxLabel="血仿膜" />
                                                <ext:Checkbox ID="chk_21_6" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_22" runat="server" FieldLabel="....其它请说明" />
                                                <ext:Button ID="Button7" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_23">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_23_1" runat="server" FieldLabel="膜面积" BoxLabel="<1.2 (m^2)" Name="opt_23" />
                                                <ext:Radio ID="opt_23_2" runat="server" BoxLabel="1.2--- (m^2)" Name="opt_23" />
                                                <ext:Radio ID="opt_23_3" runat="server" BoxLabel="1.4--- (m^2)" Name="opt_23" />
                                                <ext:Radio ID="opt_23_4" runat="server" BoxLabel="1.6--- (m^2)" Name="opt_23" />
                                                <ext:Radio ID="opt_23_5" runat="server" BoxLabel=">1.8 (m^2)" Name="opt_23" />
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
