<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_24.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_09_24" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="感染记录及追踪" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="感染日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="感染记录及追踪" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_1_1" runat="server" FieldLabel="来源" BoxLabel="门诊 " Name="opt_1" />
                                                        <ext:Radio ID="opt_1_2" runat="server" BoxLabel="急诊 " Name="opt_1" />
                                                        <ext:Radio ID="opt_1_3" runat="server" BoxLabel="住院 " Name="opt_1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_7_1" runat="server" FieldLabel="感染原因" BoxLabel="导管出口处感染 " Name="opt_7" />
                                                        <ext:Radio ID="opt_7_2" runat="server" BoxLabel="皮下遂道感染 " Name="opt_7" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_2_1" runat="server" FieldLabel="是否培养" BoxLabel="是 " Name="opt_2" />
                                                        <ext:Radio ID="opt_2_2" runat="server" BoxLabel="否 " Name="opt_2" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="培养方式" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="培养结果" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="治疗方式" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="追综" />
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_8_1" runat="server" FieldLabel="感染原因_外" BoxLabel="植管时污染 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_2" runat="server" BoxLabel="透析液过期破损 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_3" runat="server" BoxLabel="换液技术污染 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_4" runat="server" BoxLabel="更换输液管污染 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_5" runat="server" BoxLabel="加药技术污染 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_6" runat="server" BoxLabel="导管撕裂或接头松脱 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_7" runat="server" BoxLabel="导管出口处感染漫延 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_8" runat="server" BoxLabel="隧道感染漫延 " Name="opt_8" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_1" runat="server" FieldLabel="感染原因_内" BoxLabel="肠胃道改变-便秘腹泻 "
                                                            Name="opt_9" />
                                                        <ext:Radio ID="opt_9_2" runat="server" BoxLabel="血路感染-UTI. TB. 肺炎 " Name="opt_9" />
                                                        <ext:Radio ID="opt_9_3" runat="server" BoxLabel="上行性感染-泌尿道. 阴道 " Name="opt_9" />
                                                        <ext:Radio ID="opt_9_4" runat="server" BoxLabel="内脏器官发炎-胰脏炎 " Name="opt_9" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label6" runat="server" Text="后续追踪照护：" Cls="mylabel1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Checkbox ID="chk_10_1" runat="server" BoxLabel="电访 " />
                                                <ext:Checkbox ID="chk_10_2" runat="server" BoxLabel="家访 " />
                                                <ext:Checkbox ID="chk_10_3" runat="server" BoxLabel="换液技术再训练 " />
                                                <ext:Checkbox ID="chk_10_4" runat="server" BoxLabel="加药技术再训练 " />
                                                <ext:Checkbox ID="chk_10_5" runat="server" BoxLabel="换药技术再训练 " />
                                                <ext:Checkbox ID="chk_10_6" runat="server" BoxLabel="给予腹膜炎相关知识卫教 " />
                                                <ext:Checkbox ID="chk_10_7" runat="server" BoxLabel="其他 " />
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="其他" />
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
