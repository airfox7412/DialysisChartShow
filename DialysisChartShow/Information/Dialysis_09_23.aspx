<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_23.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_09_23" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="腹膜炎记录" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="初次腹膜透析日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="腹膜炎记录" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_1_1" runat="server" FieldLabel="治疗模式" BoxLabel="CAPD " Name="opt_1" />
                                                        <ext:Radio ID="opt_1_2" runat="server" BoxLabel="CCPD " Name="opt_1" />
                                                        <ext:Radio ID="opt_1_3" runat="server" BoxLabel="NIPD " Name="opt_1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_2_1" runat="server" FieldLabel="主要换液者" BoxLabel="自行 " Name="opt_2" />
                                                        <ext:Radio ID="opt_2_2" runat="server" BoxLabel="他人 " Name="opt_2" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="第几次感染" />
                                                <ext:DateField ID="dat_4" runat="server" FieldLabel="上次感染日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_1" runat="server" FieldLabel="有无长菌" BoxLabel="有 " Name="opt_5" />
                                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="无 " Name="opt_5" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="菌种" />
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_7_1" runat="server" FieldLabel="病患是否有住院" BoxLabel="是 " Name="opt_7" />
                                                        <ext:Radio ID="opt_7_2" runat="server" BoxLabel="否 " Name="opt_7" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:DateField ID="dat_8" runat="server" FieldLabel="住院日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:DateField ID="dat_9" runat="server" FieldLabel="出院日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:DateField ID="dat_10" runat="server" FieldLabel="感染日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_11_1" runat="server" FieldLabel="合并 Exit inf" BoxLabel="是 " Name="opt_11" />
                                                        <ext:Radio ID="opt_11_2" runat="server" BoxLabel="否 " Name="opt_11" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_12_1" runat="server" FieldLabel="合并Tunnel inf" BoxLabel="是 " Name="opt_12" />
                                                        <ext:Radio ID="opt_12_2" runat="server" BoxLabel="否 " Name="opt_12" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label17" runat="server" Text="感染原因：" Cls="mylabel1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Checkbox ID="chk_13_1" runat="server" BoxLabel="操作/换药技术不当 " />
                                                <ext:Checkbox ID="chk_13_2" runat="server" BoxLabel="导管出口处感染 " />
                                                <ext:Checkbox ID="chk_13_3" runat="server" BoxLabel="隧道感染 " />
                                                <ext:Checkbox ID="chk_13_4" runat="server" BoxLabel="腹部内在器官炎症 " />
                                                <ext:Checkbox ID="chk_13_5" runat="server" BoxLabel="换管48小时内 " />
                                                <ext:Checkbox ID="chk_13_6" runat="server" BoxLabel="便秘 " />
                                                <ext:Checkbox ID="chk_13_7" runat="server" BoxLabel="外科手术后 " />
                                                <ext:Checkbox ID="chk_13_8" runat="server" BoxLabel="其他原因 " />
                                                <ext:TextField ID="txt_14" runat="server" FieldLabel="其他" />
                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label8" runat="server" Text="临床症状：" Cls="mylabel1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Checkbox ID="chk_15_1" runat="server" BoxLabel="发烧＞37.8度 " />
                                                <ext:Checkbox ID="chk_15_2" runat="server" BoxLabel="透析液混浊 " />
                                                <ext:Checkbox ID="chk_15_3" runat="server" BoxLabel="腹痛 " />
                                                <ext:Checkbox ID="chk_15_4" runat="server" BoxLabel="血性透析液 " />
                                                <ext:Checkbox ID="chk_15_5" runat="server" BoxLabel="恶心、呕吐 " />
                                                <ext:Checkbox ID="chk_15_6" runat="server" BoxLabel="腹泻 " />
                                                <ext:Checkbox ID="chk_15_7" runat="server" BoxLabel="血压下降 " />
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

