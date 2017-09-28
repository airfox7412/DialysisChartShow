<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_06.aspx.cs" Inherits="Dialysis_Chart_Show.Ipad.Dialysis_0h_06" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type = "text/css">
    .mylabel3
    {
         color:Olive;
        }
    .mylabel2
    {
         color:Blue;
        }
    .mylabel1
    {
         font-weight:bold;  
         color:Black;
        }
            
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
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="pid_id" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="住院病人预防跌倒护理评估表" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel4" runat="server" Border="false" Header="false" ButtonAlign="Center">
							<Items>
                                <ext:DateField ID="info_date" runat="server" FieldLabel="评估日期" Format="yyyy-MM-dd" />
                                <ext:TextField ID="txt_1" runat="server" FieldLabel="评估护士" IndicatorText="" />
                                <ext:TextField ID="section" runat="server" FieldLabel="病区" IndicatorText="" />
                                <ext:TextField ID="bed_no" runat="server" FieldLabel="床号" IndicatorText="" />
                                <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="评估内容" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                    <Defaults>
                                        <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                    </Defaults>
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="*身体虚弱:==得分(否填0是填1)" Cls="mylabel2" />
                                        <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_4_1" runat="server" BoxLabel="0分" Name="opt_4" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio0"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_4_2" runat="server" BoxLabel="1分" Name="opt_4" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio1"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>

                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label3" runat="server" Text="*在家或住院有跌倒病史:==得分(无填0有填1)" Cls="mylabel2" />
                                        <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_5_1" runat="server" BoxLabel="0分" Name="opt_5" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio0"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_5_2" runat="server" BoxLabel="1分" Name="opt_5" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio1"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>

                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label4" runat="server" Text="*意识状态:==得分(清醒或深昏迷填0有意识障碍填1)" Cls="mylabel2" />
                                        <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_6_1" runat="server" BoxLabel="0分" Name="opt_6" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio0"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_6_2" runat="server" BoxLabel="1分" Name="opt_6" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio1"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label5" runat="server" Text="*行为能力:==得分(稳定自主或完全无法移动填0无法稳定行走填1)" Cls="mylabel2" />
                                        <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_7_1" runat="server" BoxLabel="0分" Name="opt_7" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio0"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_7_2" runat="server" BoxLabel="1分" Name="opt_7" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio1"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label6" runat="server" Text="*睡眠型态:==得分(正常填0睡眠型态混乱或使用镇定安眠药物填1)" Cls="mylabel2" />
                                        <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_8_1" runat="server" BoxLabel="0分" Name="opt_8" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio0"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_8_2" runat="server" BoxLabel="1分" Name="opt_8" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio1"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label7" runat="server" Text="*有体位性低血压:==得分(无填0有填1)" Cls="mylabel2" />
                                        <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_9_1" runat="server" BoxLabel="0分" Name="opt_9" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio0"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_9_2" runat="server" BoxLabel="1分" Name="opt_9" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio1"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label8" runat="server" Text="*使用易导致嗜睡之药物:==得分(无填0有填1)" Cls="mylabel2" />
                                        <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_10_1" runat="server" BoxLabel="0分" Name="opt_10" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio0"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_10_2" runat="server" BoxLabel="1分" Name="opt_10" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio1"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                            </Items>
                                        </ext:Container>
                                        <ext:Label ID="Label9" runat="server" Text="*排尿或排便需他人协助:==得分(不需填0需填1)" Cls="mylabel2" />
                                        <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_11_1" runat="server" BoxLabel="0分" Name="opt_11" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio0"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                                <ext:Radio ID="opt_11_2" runat="server" BoxLabel="1分" Name="opt_11" >
                                                    <DirectEvents>
                                                        <Change OnEvent="_CountRadio1"></Change>
                                                    </DirectEvents>
                                                </ext:Radio>
                                            </Items>
                                        </ext:Container>

                                    </Items>
                                </ext:FieldSet>


                                        
                                <ext:TextField ID="txt_37" runat="server" FieldLabel="总分" IndicatorText="" ReadOnly="true" Cls="mylabel1"  />



                                <ext:Label ID="Label58" runat="server" Text="填表说明：" Cls="mylabel2" />
                                <ext:Container ID="Container62" runat="server" Layout="ColumnLayout" Padding="2">
                                    <Items>
                                        <ext:Label ID="Label59" runat="server" Text="1.评估时间" />
										<ext:Label ID="Label59_1" runat="server" Text="(1).新住院或转科病人。" />
										<ext:Label ID="Label59_2" runat="server" Text="(2).病人意识状态或病情转变时。" />
										<ext:Label ID="Label59_3" runat="server" Text="(3).使用易导致病人意识状态改变的药物。" />
										<ext:Label ID="Label59_4" runat="server" Text="(4).每月应重新评估一次。" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container63" runat="server" Layout="ColumnLayout" Padding="2">
                                    <Items>
                                        <ext:Label ID="Label60" runat="server" Text="2.上述评估内容，评估总分≥1分，都应在护理纪录中呈现护理问题*高危险性伤害/跌倒*，并持续评价。" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container64" runat="server" Layout="ColumnLayout" Padding="2">
                                    <Items>
                                        <ext:Label ID="Label61" runat="server" Text="3.如有下列条件，年龄≥65岁Hb<110g/L、有头晕症状、有使用利尿或缓泻剂等病人，请填写预防跌倒护理措施评估表，并列入交班中。" />
                                    </Items>
                                </ext:Container>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_close" runat="server" Icon="Disk" Text="关闭" Width="100">
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
    </form>
</body>
</html>
