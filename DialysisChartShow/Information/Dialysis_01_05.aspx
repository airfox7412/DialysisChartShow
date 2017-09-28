﻿<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_01_05.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_01_05" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>肿瘤诊断</title>
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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="肿瘤诊断" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="诊断" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_1_1" runat="server" BoxLabel="无" />
                                                <ext:Checkbox ID="chk_1_2" runat="server" BoxLabel="消化系统 " />
                                                <ext:Checkbox ID="chk_1_3" runat="server" BoxLabel="呼吸系统 " />
                                                <ext:Checkbox ID="chk_1_4" runat="server" BoxLabel="血液系统 " />
                                                <ext:Checkbox ID="chk_1_5" runat="server" BoxLabel="泌尿生殖系统" />
                                                <ext:Checkbox ID="chk_1_6" runat="server" BoxLabel="神经系统 " />
                                                <ext:Checkbox ID="chk_1_7" runat="server" BoxLabel="骨骼肌肉系统 " />
                                                <ext:Checkbox ID="chk_1_8" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="消化系统诊断" Width="500" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="呼吸系统诊断" Width="500" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="血液系统诊断" Width="500" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="泌尿系统诊断" Width="500" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="神经系统诊断" Width="500" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="骨骼肌肉系统诊断" Width="500" />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="其它诊断" Width="500" />
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
