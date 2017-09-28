<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_05_02.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_05_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>血管通路纪录</title>
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
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血管通路纪录" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center" Layout="HBoxLayout">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false" Width="600">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="记录日期" Format="yyyy-MM-dd" />
                                        <ext:TextArea ID="are_7" runat="server" FieldLabel="普通情况记录(限字数350)" Width="543" Height="120" />
                                        <ext:TextArea ID="are_1" runat="server" FieldLabel="手术操作记录(限字数350)" Width="543" Height="120" />
                                        <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_8_1" runat="server" FieldLabel="是否廔管重建？" BoxLabel="是　" Name="opt_8" >
                                                    <DirectEvents>
                                                        <Change OnEvent="ii"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_8_2" runat="server" BoxLabel="否　" Name="opt_8" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_9_1" runat="server" FieldLabel="重建原因" LabelAlign="Right" BoxLabel="导管感染　" Name="opt_9" />
                                                <ext:Radio ID="opt_9_2" runat="server" BoxLabel="内瘘阻塞　" Name="opt_9" />
                                                <ext:Radio ID="opt_9_3" runat="server" BoxLabel="血流量过小(内瘘狭窄)　" Name="opt_9" />
                                                <ext:Radio ID="opt_9_4" runat="server" BoxLabel="血流量过大(内瘘成熟)　" Name="opt_9" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_9_5" runat="server" FieldLabel="　" LabelAlign="Right" BoxLabel="长期导管移位　" Name="opt_9" />
                                                <ext:Radio ID="opt_9_6" runat="server" BoxLabel="窃流症候群　" Name="opt_9" />
                                                <ext:Radio ID="opt_9_7" runat="server" BoxLabel="其他" Name="opt_9" >
                                                <DirectEvents>
                                                        <Change OnEvent="jj"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:TextField ID="txt_10" runat="server" FieldLabel="其他" LabelAlign="Right" LabelWidth="50" Hidden="true" />
                                            </Items>
                                        </ext:Container>
                                        <ext:TextArea ID="are_2" runat="server" FieldLabel="住院病情记录(限字数350)" Width="543" Height="120" />
                                        <ext:TextArea ID="are_11" runat="server" FieldLabel="普通情况记录" Width="543" Height="120" Hidden="true" />
                                        <ext:TextArea ID="are_3" runat="server" FieldLabel="特殊病情记录及治疗(限字数350)" Width="543" Height="120" />
                                        <ext:TextArea ID="are_4" runat="server" FieldLabel="特殊治疗" Width="400" Hidden="true" />
                                        <ext:TextArea ID="are_5" runat="server" FieldLabel="抢救记录(限字数350)" Width="543" Height="120" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel1" runat="server" Border="false" Header="false" Width="360">
                                    <Items>
                                        <ext:Container ID="Container21" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:Radio ID="opt_13_1" runat="server" FieldLabel="手术方式-前臂" BoxLabel="左　" Name="opt_13" />
                                                <ext:Radio ID="opt_13_2" runat="server" BoxLabel="右　" Name="opt_13" />
                                           </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:ImageButton ID="Btn_pic1" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group1" 
                                                    ImageUrl="../Styles/PIC_1.jpg" OverImageUrl="../Styles/PIC_1.jpg" Height="300">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_pic1_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                                <ext:ImageButton ID="Btn_pic2" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group1"
                                                    ImageUrl="../Styles/PIC_2.jpg" OverImageUrl="../Styles/PIC_2.jpg" Height="300">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_pic2_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container11" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:Label ID="Label1" runat="server" Flex="1" Text="桡动脉-头静脉端端吻合" />
                                                <ext:Label ID="Label2" runat="server" Flex="1" Text="桡动脉-头静脉端侧吻合" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label11" runat="server" Flex="1" Text="　" />
                                        <ext:Container ID="Container22" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:Radio ID="opt_14_1" runat="server" FieldLabel="手术方式-上臂" BoxLabel="左　" Name="opt_14" />
                                                <ext:Radio ID="opt_14_2" runat="server" BoxLabel="右　" Name="opt_14" />
                                           </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:ImageButton ID="Btn_pic3" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group1"
                                                    ImageUrl="../Styles/PIC_3.jpg" OverImageUrl="../Styles/PIC_3.jpg" Height="300">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_pic3_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                                <ext:ImageButton ID="Btn_pic4" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group1" 
                                                    ImageUrl="../Styles/PIC_4.jpg" OverImageUrl="../Styles/PIC_4.jpg" Height="300">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_pic4_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container12" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:Label ID="Label3" runat="server" Flex="1" Text="肱动脉-头静脉端侧吻合(1)" />
                                                <ext:Label ID="Label4" runat="server" Flex="1" Text="肱动脉-头静脉端侧吻合(2)" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label12" runat="server" Flex="1" Text="　" />
                                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:ImageButton ID="Btn_pic5" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group1"
                                                    ImageUrl="../Styles/PIC_5.jpg" OverImageUrl="../Styles/PIC_5.jpg" Height="300">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_pic5_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                                <ext:ImageButton ID="Btn_pic6" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group1" 
                                                    ImageUrl="../Styles/PIC_6.jpg" OverImageUrl="../Styles/PIC_6.jpg" Height="300">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_pic6_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container13" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:Label ID="Label5" runat="server" Flex="1" Text="肱动脉-正中静脉端侧吻合(近)" />
                                                <ext:Label ID="Label6" runat="server" Flex="1" Text="肱动脉-正中静脉端侧吻合(远)" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label13" runat="server" Flex="1" Text="　" />
                                        <ext:Container ID="Container4" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:ImageButton ID="Btn_pic7" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group1"
                                                    ImageUrl="../Styles/PIC_7.jpg" OverImageUrl="../Styles/PIC_7.jpg" Height="300">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_pic7_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                                <ext:ImageButton ID="Btn_pic8" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group1"
                                                    ImageUrl="../Styles/PIC_8.jpg" OverImageUrl="../Styles/PIC_8.jpg" Height="300">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_pic8_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container14" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:Label ID="Label7" runat="server" Flex="1" Text="肱动脉-正中静脉侧侧吻合" />
                                                <ext:Label ID="Label8" runat="server" Flex="1" Text="" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label14" runat="server" Flex="1" Text="　" />
                                        <ext:Container ID="Container5" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:ImageButton ID="Btn_picA" runat="server" Cls="x-btn" Height="550" Flex="1">
                                                    <DirectEvents>
                                                        <Click OnEvent="Btn_picA_Click" />
                                                    </DirectEvents>
                                                </ext:ImageButton>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" >
                                            <Items>
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="備註" LabelAlign="Right" LabelWidth="40" Flex="1" />
                                            </Items>
                                        </ext:Container>
                                        <ext:TextField ID="txt_6" runat="server" Width="400" Hidden="true" />
                                    </Items>
                                </ext:Panel>
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
                                <ext:Button ID="Btn_Close" runat="server" Icon="Disk" Text="关闭" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
