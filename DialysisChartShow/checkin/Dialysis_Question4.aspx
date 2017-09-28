<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Question4.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Dialysis_Question4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>首诊四问</title>
    <link href="../css/grid.css" rel="stylesheet"/>
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
        .blue-label .x-label-value
        {
            color: Blue;
        }
    </style>
    <script language="javascript" type="text/javascript">
        var showResult = function (btn) {
            Ext.Msg.notify("Button Click", "You clicked the " + btn + " button");
        };

        var showResultText = function (btn, text) {
            Ext.Msg.notify("Button Click", "You clicked the " + btn + 'button and entered the text "' + text + '".');
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">        

        <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN" Theme="Triton" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel_North" runat="server" Title="" Region="North" Header="false" Cls="Panellogo" AutoScroll="true">
                    <Items>
                        <ext:Container ID="Container13" runat="server">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:FormPanel ID="FormPanel1" runat="server" Title="血液净化中心首次接诊医师4问" ButtonAlign="Center" AutoScroll="true" Width="1000" Frame="true"> 
                                    <Items>
                                        <ext:Container ID="Container01" runat="server" Frame="true" Layout="ColumnLayout">
                                            <Items>
                                                <ext:Container ID="Container1" runat="server" ColumnWidth=".1">
                                                    <Items>
                                                        <ext:Image ID="Image1" runat="server" Height="100" Weight="50" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container2" runat="server" ColumnWidth=".9">
                                                    <Items>
                                                        <ext:Container ID="Container20" runat="server" Layout="ColumnLayout">
                                                            <Items>
                                                                <ext:TextField ID="Patient_Name" runat="server" FieldLabel="姓名" LabelWidth="50" Width="150" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" ReadOnly="true" />
                                                                <ext:TextField ID="Patient_Age" runat="server" FieldLabel="年龄" LabelWidth="50" Width="100" LabelAlign="Right" PaddingSpec="5 30 5 5" LabelCls="formLabel" ReadOnly="true" />                              
                                                                <ext:TextField ID="Clinic" runat="server" FieldLabel="科室" LabelWidth="50" Width="100" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" ReadOnly="true" />
                                                                <ext:TextField ID="floor" runat="server" FieldLabel="楼层" LabelWidth="50" Width="100" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" ReadOnly="true" />
                                                                <ext:TextField ID="bedno" runat="server" FieldLabel="床位" LabelWidth="50"  Width="100" LabelAlign="Right" PaddingSpec="5 5 5 5" LabelCls="formLabel" ReadOnly="true" />  
                                                            </Items>
                                                        </ext:Container>
                                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="1.身体有出血？" Layout="AnchorLayout">
                                                            <Items>
                                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Radio ID="opt_1_1" runat="server" FieldLabel="有" LabelWidth="50" LabelAlign="Right" Name="opt_1" Flex="1" />
                                                                        <ext:Radio ID="opt_1_2" runat="server" FieldLabel="无" LabelWidth="50" LabelAlign="Right" Name="opt_1" Flex="1" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Label ID="Label2" runat="server" Text="部位" Cls="blue-label" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Radio ID="opt_2_1" runat="server" FieldLabel="鼻出血" LabelWidth="50" LabelAlign="Right" Name="opt_2" Flex="1" />
                                                                        <ext:Radio ID="opt_2_2" runat="server" FieldLabel="皮下出血" LabelWidth="70" LabelAlign="Right" Name="opt_2" Flex="1" />
                                                                        <ext:Radio ID="opt_2_3" runat="server" FieldLabel="尿血" LabelWidth="50" LabelAlign="Right" Name="opt_2" Flex="1" />
                                                                        <ext:Radio ID="opt_2_4" runat="server" FieldLabel="便血" LabelWidth="50" LabelAlign="Right" Name="opt_2" Flex="1" />
                                                                        <ext:Radio ID="opt_2_5" runat="server" FieldLabel="其他" LabelWidth="50" LabelAlign="Right" Name="opt_2" Flex="1" />
                                                                        <ext:TextField ID="txt_2" runat="server" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Label ID="Label1" runat="server" Text="凝血四项提示 ->  出血倾向:" Cls="blue-label" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Radio ID="opt_3_1" runat="server" FieldLabel="有" LabelWidth="50" LabelAlign="Right" Name="opt_3" Flex="1" />
                                                                        <ext:Radio ID="opt_3_2" runat="server" FieldLabel="无" LabelWidth="50" LabelAlign="Right" Name="opt_3" Flex="1" />
                                                                    </Items>
                                                                </ext:Container>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="2.近一个月有手术吗？" Layout="AnchorLayout">
                                                            <Items>
                                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Radio ID="opt_4_1" runat="server" FieldLabel="有" LabelWidth="50" LabelAlign="Right" Name="opt_4" Flex="1" />
                                                                        <ext:Radio ID="opt_4_2" runat="server" FieldLabel="无" LabelWidth="50" LabelAlign="Right" Name="opt_4" Flex="1" />
                                                                        <ext:TextField ID="txt_4" runat="server" FieldLabel="手术部位" LabelAlign="Right" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Radio ID="opt_5_1" runat="server" FieldLabel="准确" LabelWidth="50" LabelAlign="Right" Name="opt_5" Flex="1" />
                                                                        <ext:Radio ID="opt_5_2" runat="server" FieldLabel="大概" LabelWidth="50" LabelAlign="Right" Name="opt_5" Flex="1" />
                                                                        <ext:DateField ID="dat_5" runat="server" FieldLabel="日期" LabelAlign="Right" Format="yyyy-MM-dd" /> 
                                                                    </Items>
                                                                </ext:Container>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="3.以前有传染病吗？" Layout="AnchorLayout">
                                                            <Items>
                                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Radio ID="opt_6_1" runat="server" FieldLabel="有" LabelWidth="50" LabelAlign="Right" Name="opt_6" Flex="1" />
                                                                        <ext:Radio ID="opt_6_2" runat="server" FieldLabel="无" LabelWidth="50" LabelAlign="Right" Name="opt_6" Flex="1" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Checkbox ID="chk_7_1" runat="server" FieldLabel="乙肝" LabelWidth="50" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_7_2" runat="server" FieldLabel="丙肝" LabelWidth="50" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_7_3" runat="server" FieldLabel="梅毒" LabelWidth="50" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_7_4" runat="server" FieldLabel="爱滋" LabelWidth="50" LabelAlign="Right" Flex="1" />
                                                                    </Items>
                                                                </ext:Container>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="4.最近有什么不舒服吗？" Layout="AnchorLayout">
                                                            <Items>
                                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Checkbox ID="chk_8_1" runat="server" FieldLabel="恶心" LabelWidth="50" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_8_2" runat="server" FieldLabel="呕吐" LabelWidth="50" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_8_3" runat="server" FieldLabel="不能吃饭" LabelWidth="100" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_8_4" runat="server" FieldLabel="浮肿" LabelWidth="50" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_8_5" runat="server" FieldLabel="胸闷口气短" LabelWidth="100" LabelAlign="Right" Flex="1" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container14" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Checkbox ID="chk_9_1" runat="server" FieldLabel="不能平躺" LabelWidth="70" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_9_2" runat="server" FieldLabel="活动后胸闷" LabelWidth="100" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_9_3" runat="server" FieldLabel="突然尿少" LabelWidth="100" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_9_4" runat="server" FieldLabel="其他" LabelWidth="50" LabelAlign="Right" Flex="1" />
                                                                        <ext:TextField ID="txt_9" runat="server" FieldLabel="" LabelAlign="Right" />
                                                                        <ext:TextField ID="txt_10" runat="server" FieldLabel="尿量" LabelWidth="50" LabelAlign="Right" IndicatorText="ml" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container15" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Label ID="Label3" runat="server" Text="浆膜腔积液" Cls="blue-label" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container16" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Radio ID="opt_11_2" runat="server" FieldLabel="无" LabelWidth="50" LabelAlign="Right" Name="opt_11" Flex="1" />
                                                                        <ext:Radio ID="opt_11_1" runat="server" FieldLabel="有" LabelWidth="50" LabelAlign="Right" Name="opt_11" Flex="1" />
                                                                        <ext:Checkbox ID="chk_12_1" runat="server" FieldLabel="部位" LabelWidth="70" LabelAlign="Right" Flex="1" />
                                                                        <ext:TextField ID="txt_12" runat="server" FieldLabel="" LabelAlign="Right" />
                                                                        <ext:Checkbox ID="chk_12_2" runat="server" FieldLabel="暂时无结果" LabelAlign="Right" Flex="1" />
                                                                    </Items>
                                                                </ext:Container>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="管床大夫" Layout="AnchorLayout">
                                                            <Items>
                                                                <ext:Container ID="Container17" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Checkbox ID="chk_13_1" runat="server" FieldLabel="强烈要求" LabelWidth="70" LabelAlign="Right" Flex="1" />
                                                                        <ext:Checkbox ID="chk_13_2" runat="server" FieldLabel="建议" LabelAlign="Right" Flex="1" />
                                                                        <ext:TextField ID="txt_13" runat="server" FieldLabel="" LabelAlign="Right" />
                                                                    </Items>
                                                                </ext:Container>
                                                            </Items>
                                                        </ext:FieldSet>
                                                        <ext:FieldSet ID="FieldSet6" runat="server" Flex="1" Title="评估" Layout="AnchorLayout">
                                                            <Items>
                                                                <ext:Container ID="Container18" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:Radio ID="opt_14_1" runat="server" FieldLabel="容量负担过重" LabelWidth="100" LabelAlign="Right" Name="opt_14" Flex="1" />
                                                                        <ext:Radio ID="opt_14_2" runat="server" FieldLabel="无容量负担过重" LabelWidth="150" LabelAlign="Right" Name="opt_14" Flex="1" />
                                                                    </Items>
                                                                </ext:Container>
                                                                <ext:Container ID="Container19" runat="server" Layout="ColumnLayout">
                                                                    <Items>
                                                                        <ext:TextArea ID="are_14" runat="server" Text="其他注意:" Width="600" />
                                                                    </Items>
                                                                </ext:Container>
                                                            </Items>
                                                        </ext:FieldSet>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                    <Buttons>
                                        <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="Btn_Submit_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="btn_clear" runat="server" Icon="Disk" Text="重置" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="Btn_Clear_Click" />
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Button ID="Btn_Print" runat="server" Icon="Printer" Text="打印" Width="100">
                                            <DirectEvents>
                                                <Click OnEvent="Btn_Print_Click" />
                                            </DirectEvents>
                                        </ext:Button> 
                                    </Buttons>
                                </ext:FormPanel>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
                <ext:Window ID="PrintWindow" runat="server" Title="" Width="900" Height="700" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader6" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
