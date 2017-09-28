<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_21.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_09_21" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="植管记录" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="植管日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="植管记录" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_1" runat="server" FieldLabel="植管医院" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="手术植管医生" />
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_3_1" runat="server" FieldLabel="植管方式" BoxLabel="腹腔切开 " Name="opt_3" />
                                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="腹腔镜 " Name="opt_3" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_4_1" runat="server" FieldLabel="导管出口处" BoxLabel="左侧 " Name="opt_4" />
                                                        <ext:Radio ID="opt_4_2" runat="server" BoxLabel="右侧 " Name="opt_4" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_1" runat="server" FieldLabel="Tenckhoff 形式" BoxLabel="one cuff "
                                                            Name="opt_5" />
                                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="two cuff " Name="opt_5" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:DateField ID="dat_6" runat="server" FieldLabel="首次腹膜透析日期" Format="yyyy-MM-dd">
                                                        </ext:DateField>
                                                        <ext:TextField ID="txt_7" runat="server" FieldLabel="首次腹膜透析医院" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:DateField ID="dat_8" runat="server" FieldLabel="终止透析治疗日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:TextArea ID="are_9" runat="server" FieldLabel="终止透析原因" Width="400" />
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_10" runat="server" FieldLabel="主治医师" />
                                                        <ext:TextField ID="txt_11" runat="server" FieldLabel="全责护理师" />
                                                    </Items>
                                                </ext:Container>
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
