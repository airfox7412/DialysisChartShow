<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_02_07.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_02_07" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="合用其它透析模式" AutoScroll="true"
                    ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="处方日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Checkbox ID="chk_1_1" runat="server" FieldLabel="合用其它透析模式" BoxLabel="无" />
                                                <ext:Checkbox ID="chk_1_2" runat="server" BoxLabel="腹膜透析" />
                                                <ext:Checkbox ID="chk_1_3" runat="server" BoxLabel="结肠透析" />
                                                <ext:Checkbox ID="chk_1_4" runat="server" BoxLabel="血液灌注(HP)" />
                                                <ext:Checkbox ID="chk_1_5" runat="server" BoxLabel="血浆置换(PE)" />
                                                <ext:Checkbox ID="chk_1_6" runat="server" BoxLabel="连续性肾替代性治疗(CRRT)" />
                                                <ext:Checkbox ID="chk_1_7" runat="server" BoxLabel="PA" />
                                                <ext:Checkbox ID="chk_1_8" runat="server" BoxLabel="其它" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="其它" />
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
