<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_06_012.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_06_012" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>病史</title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ext:ResourceManager ID="ResourceManager2" runat="server" Theme="Default" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="病历" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="记录日期" Format="yyyy-MM-dd" />
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="病史" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextArea ID="are_1" runat="server" FieldLabel="主诉" Width="500" Height="50" />
                                                <ext:TextArea ID="are_2" runat="server" FieldLabel="现病史" EmptyText="限字数350" Width="1000" Height="95" MaxLength="350" EnforceMaxLength="true" />
                                                <ext:TextArea ID="are_3" runat="server" FieldLabel="既往史" EmptyText="限字数350" Width="1000" Height="95" MaxLength="350" EnforceMaxLength="true"/>
                                                <ext:TextArea ID="are_4" runat="server" FieldLabel="家族史" EmptyText="限字数350" Width="1000" Height="95" MaxLength="350" EnforceMaxLength="true"/>
                                                <ext:TextArea ID="are_5" runat="server" FieldLabel="其它重要病史补充" Width="500" Height="50" />
                                                <%--<ext:TextArea ID="are_6" runat="server" FieldLabel="既往诊治经过" Width="500" />--%>
                                                <ext:Radio ID="opt_7_1" runat="server" FieldLabel="药物过敏史" BoxLabel="无" Name="opt_7" />
                                                <ext:Radio ID="opt_7_2" runat="server" BoxLabel="有" Name="opt_7">
                                                    <%--<Listeners>
                                                        <Change Handler="if(this.checked){#{txt_8}.setDisabled(false);}else{#{txt_8}.setDisabled(true);}" />
                                                    </Listeners>--%>
                                                </ext:Radio>
                                                <ext:TextField ID="txt_8" runat="server" Width="500" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet8" runat="server" Flex="1" Title="既往肾替代治疗史" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:DateField ID="dat_9" runat="server" FieldLabel="首次血透日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:TextField ID="txt_10" runat="server" FieldLabel="腹透时间" />
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="CRRT时间" />
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="治疗方案" Width = "500" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet9" runat="server" Flex="1" Title="目前血管通路" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Button ID="Button23" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_13">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <%--<ext:Radio ID="opt_13_1" runat="server" FieldLabel="内瘘" BoxLabel="自体" Name="opt_13" />
                                                <ext:Radio ID="opt_13_2" runat="server" BoxLabel="移植" Name="opt_13" />--%>
                                                <ext:TextField ID="txt_13" runat="server" FieldLabel="内瘘" />
                                                <ext:TextField ID="txt_14" runat="server" FieldLabel="手术时间" />
                                                <ext:Button ID="Button24" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Clear_Radio">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="radname" Value="opt_15">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Click>
                                                    </DirectEvents>
                                                </ext:Button>
                                                <ext:Radio ID="opt_15_1" runat="server" FieldLabel="中心静脉导管" BoxLabel="颈内静脉" Name="opt_15" />
                                                <ext:Radio ID="opt_15_2" runat="server" BoxLabel="锁骨下静脉" Name="opt_15" />
                                                <ext:Radio ID="opt_15_3" runat="server" BoxLabel="股静脉" Name="opt_15" />

                                                <ext:TextField ID="txt_16" runat="server" FieldLabel="留置时间" />
                                                <ext:TextField ID="txt_17" runat="server" FieldLabel="临时穿刺血管" />
                                                <ext:TextField ID="txt_18" runat="server" FieldLabel="穿刺部位" />
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
