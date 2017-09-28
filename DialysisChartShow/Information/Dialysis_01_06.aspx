<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_01_06.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_01_06" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>过敏诊断</title>
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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="过敏诊断" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="诊断日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="过敏诊断" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_1_1" runat="server" BoxLabel="透析器材过敏" FieldLabel="过敏反应" />
                                                <ext:Checkbox ID="chk_1_2" runat="server" BoxLabel="药物过敏 " />
                                                <ext:Checkbox ID="chk_1_3" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="其他过敏反应 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="透析器材过敏" Layout="AnchorLayout"  Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_3_1" runat="server" BoxLabel="透析膜" FieldLabel="透析器材过敏" />
                                                <ext:Checkbox ID="chk_3_2" runat="server" BoxLabel="消毒剂" />
                                                <ext:Checkbox ID="chk_4_1" runat="server" BoxLabel="聚砜膜(PS) " FieldLabel="透析膜" />
                                                <ext:Checkbox ID="chk_4_2" runat="server" BoxLabel="聚甲基丙烯酸甲酯膜（PMMA）" />
                                                <ext:Checkbox ID="chk_4_3" runat="server" BoxLabel="其它合成膜" />
                                                <ext:Checkbox ID="chk_4_4" runat="server" BoxLabel="醋酸纤维膜" />
                                                <ext:Checkbox ID="chk_4_5" runat="server" BoxLabel="血仿膜 " />
                                                <ext:Checkbox ID="chk_4_6" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="其它透析膜说明" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="其它透析膜说明" />
                                                <ext:Checkbox ID="chk_7_1" runat="server" BoxLabel="环氧乙烷 " FieldLabel="消毒剂" />
                                                <ext:Checkbox ID="chk_7_2" runat="server" BoxLabel="过氧乙酸 " />
                                                <ext:Checkbox ID="chk_7_3" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="其它消毒剂说明 " />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="药物过敏" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_9_1" runat="server" BoxLabel="抗生素" FieldLabel="药物过敏 " />
                                                <ext:Checkbox ID="chk_9_2" runat="server" BoxLabel="静脉铁剂" />
                                                <ext:Checkbox ID="chk_9_3" runat="server" BoxLabel="肝素" />
                                                <ext:Checkbox ID="chk_9_4" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_10" runat="server" FieldLabel="抗生素" EmptyText="请填写具体药名" />
                                                <ext:Checkbox ID="chk_11_1" runat="server" BoxLabel="蔗糖铁" FieldLabel="静脉铁剂" />
                                                <ext:Checkbox ID="chk_11_2" runat="server" BoxLabel="右旋糖苷铁" />
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="蔗糖铁" EmptyText="请填写具体药名" />
                                                <ext:TextField ID="txt_13" runat="server" FieldLabel="右旋糖苷铁" EmptyText="请填写具体药名" />
                                                <ext:Radio ID="opt_14_1" runat="server" BoxLabel="肝素诱导的血小板减少症" Name="opt_10" FieldLabel="肝素" />
                                                <ext:Radio ID="opt_14_2" runat="server" BoxLabel="其它" Name="opt_10" />
                                                <ext:TextField ID="txt_15" runat="server" FieldLabel="其它药物过敏说明" />
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
