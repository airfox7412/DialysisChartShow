<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_02_03.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_02_03" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title=" 血压测量（血透）" AutoScroll="true"
                    ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd"
                                            Note="【注意：每月登记一次】">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="血压测量" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Radio ID="opt_1_1" runat="server" FieldLabel="测量部位" BoxLabel="上肢" Name="opt_1" />
                                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="下肢" Name="opt_1" />
                                                <ext:TextField ID="num_1" runat="server" FieldLabel="透析前收缩压" IndicatorText="mmHg" />
                                                <ext:TextField ID="num_2" runat="server" FieldLabel="透析前舒张压" IndicatorText="mmHg" />
                                                <ext:TextField ID="num_3" runat="server" FieldLabel="透析后收缩压" IndicatorText="mmHg" />
                                                <ext:TextField ID="num_4" runat="server" FieldLabel="透析后舒张压" IndicatorText="mmHg" />
                                                <ext:TextField ID="num_5" runat="server" FieldLabel="非透析日收缩压" IndicatorText="mmHg" />
                                                <ext:TextField ID="num_6" runat="server" FieldLabel="非透析日舒张压" IndicatorText="mmHg" />
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
