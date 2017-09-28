<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_22.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_09_22" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="回诊记录" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="回诊记录" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_1" runat="server" FieldLabel="血压" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="体重" />
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_3_1" runat="server" FieldLabel="导管出口情形" BoxLabel="perfect " Name="opt_3" />
                                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="good " Name="opt_3" />
                                                        <ext:Radio ID="opt_3_3" runat="server" BoxLabel="equivocal " Name="opt_3" />
                                                        <ext:Radio ID="opt_3_4" runat="server" BoxLabel="pain " Name="opt_3" />
                                                        <ext:Radio ID="opt_3_5" runat="server" BoxLabel="discharge " Name="opt_3" />
                                                        <ext:Radio ID="opt_3_6" runat="server" BoxLabel="infection " Name="opt_3" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="皮下隧道状况" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="cuff状况" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="下肢水肿状况" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="生活活动状况" />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="睡眠状况" />
                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_1" runat="server" FieldLabel="排便情形" BoxLabel="正常 " Name="opt_9" />
                                                        <ext:Radio ID="opt_9_2" runat="server" BoxLabel="腹泻 " Name="opt_9" />
                                                        <ext:Radio ID="opt_9_3" runat="server" BoxLabel="便秘 " Name="opt_9" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_10" runat="server" FieldLabel="记录完整" />
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="遵从换液医嘱" />
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="其他" />
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
