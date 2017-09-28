<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_03.aspx.cs" Inherits="Dialysis_Chart_Show.Ipad.Dialysis_0h_03" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="动静脉内瘘闭塞高危因素评估表" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="评估日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:TextField ID="txt_1" runat="server" FieldLabel="评估护士" IndicatorText="" />
                                        
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="性别" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label2" runat="server" Text="男性" Cls="mylabel2" />
                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_2_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_2" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_2_2" runat="server" BoxLabel="1分" Name="opt_2" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="年龄" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Labe3" runat="server" Text="60岁以上" Cls="mylabel2" />
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_3_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_3" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="1分" Name="opt_3" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="并发症" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label1" runat="server" Text="*DM(糖尿病)" Cls="mylabel2" />
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_4_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_4" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_4_2" runat="server" BoxLabel="1分" Name="opt_4" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_4_3" runat="server" BoxLabel="2分" Name="opt_4" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label3" runat="server" Text="*高血压" Cls="mylabel2" />
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_5" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="1分" Name="opt_5" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_5_3" runat="server" BoxLabel="2分" Name="opt_5" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label4" runat="server" Text="CHOL(胆固醇)>200mg/dl" Cls="mylabel2" />
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_6_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_6" >
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
                                                <ext:Label ID="Label5" runat="server" Text="低血压(透析前BP<90/60mmHg，且>3次/月" Cls="mylabel2" />
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_7_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_7" >
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
                                                <ext:Label ID="Label6" runat="server" Text="抽烟史" Cls="mylabel2" />
                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_8_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_8" >
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
                                                <ext:Label ID="Label7" runat="server" Text="血管硬化" Cls="mylabel2" />
                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_9" >
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
                                                <ext:Label ID="Label8" runat="server" Text="血管感染史" Cls="mylabel2" />
                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_10_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_10" >
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
                                                <ext:Label ID="Label9" runat="server" Text="CVA(心脑血管意外)" Cls="mylabel2" />
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_11_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_11" >
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
                                                <ext:Label ID="Label10" runat="server" Text="CAD(冠心病)" Cls="mylabel2" />
                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_12_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_12" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_12_2" runat="server" BoxLabel="1分" Name="opt_12" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label11" runat="server" Text="TG(甘油三酯)>500mg/dl" Cls="mylabel2" />
                                                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_13" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="1分" Name="opt_13" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="血管条件" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label12" runat="server" Text="*纤细" Cls="mylabel2" />
                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_14_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_14" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_14_2" runat="server" BoxLabel="1分" Name="opt_14" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_14_3" runat="server" BoxLabel="2分" Name="opt_14" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label13" runat="server" Text="*不明显" Cls="mylabel2" />
                                                <ext:Container ID="Container14" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_15_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_15" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_15_2" runat="server" BoxLabel="1分" Name="opt_15" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_15_3" runat="server" BoxLabel="2分" Name="opt_15" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label30" runat="server" Text="*弹性差" Cls="mylabel2" />
                                                <ext:Container ID="Container31" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_16_2" runat="server" BoxLabel="1分" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_16_3" runat="server" BoxLabel="2分" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label31" runat="server" Text="*有血管阻塞史" Cls="mylabel2" />
                                                <ext:Container ID="Container32" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_17_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_17" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_17_2" runat="server" BoxLabel="1分" Name="opt_17" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_17_3" runat="server" BoxLabel="2分" Name="opt_17" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label14" runat="server" Text="*血管使用年限<1年" Cls="mylabel2" />
                                                <ext:Container ID="Container15" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_18_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_18" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_18_2" runat="server" BoxLabel="1分" Name="opt_18" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_18_3" runat="server" BoxLabel="2分" Name="opt_18" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet8" runat="server" Flex="1" Title="药物使用" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label15" runat="server" Text="*降压药" Cls="mylabel2" />
                                                <ext:Container ID="Container16" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_19_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_19" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_19_2" runat="server" BoxLabel="1分" Name="opt_19" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_19_3" runat="server" BoxLabel="2分" Name="opt_19" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label16" runat="server" Text="无肝素透析" Cls="mylabel2" />
                                                <ext:Container ID="Container17" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_20_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_20" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_20_2" runat="server" BoxLabel="1分" Name="opt_20" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label17" runat="server" Text="口服抗凝剂" Cls="mylabel2" />
                                                <ext:Container ID="Container18" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_21_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_21" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_21_2" runat="server" BoxLabel="1分" Name="opt_21" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet9" runat="server" Flex="1" Title="血管护理" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label18" runat="server" Text="*未规则握球运动" Cls="mylabel2" />
                                                <ext:Container ID="Container19" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_22_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_22" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_22_2" runat="server" BoxLabel="1分" Name="opt_22" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_22_3" runat="server" BoxLabel="2分" Name="opt_22" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label19" runat="server" Text="*未规则局部热敷" Cls="mylabel2" />
                                                <ext:Container ID="Container33" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_23_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_23" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_23_2" runat="server" BoxLabel="1分" Name="opt_23" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_23_3" runat="server" BoxLabel="2分" Name="opt_23" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label32" runat="server" Text="穿刺前未习惯洗手" Cls="mylabel2" />
                                                <ext:Container ID="Container20" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_24_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_24" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_24_2" runat="server" BoxLabel="1分" Name="opt_24" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label20" runat="server" Text="每日未检查血管通畅情况" Cls="mylabel2" />
                                                <ext:Container ID="Container21" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_25_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_25" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_25_2" runat="server" BoxLabel="1分" Name="opt_25" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet10" runat="server" Flex="1" Title="血管通路" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label21" runat="server" Text="*Graft(移植)" Cls="mylabel2" />
                                                <ext:Container ID="Container22" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_26_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_26" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_26_2" runat="server" BoxLabel="1分" Name="opt_26" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_26_3" runat="server" BoxLabel="2分" Name="opt_26" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet11" runat="server" Flex="1" Title="穿刺方法" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label24" runat="server" Text="*重复定点穿刺" Cls="mylabel2" />
                                                <ext:Container ID="Container25" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_27_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_27" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_27_2" runat="server" BoxLabel="1分" Name="opt_27" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_27_3" runat="server" BoxLabel="2分" Name="opt_27" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label25" runat="server" Text="不规则纽扣式" Cls="mylabel2" />
                                                <ext:Container ID="Container26" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_28_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_28" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_28_2" runat="server" BoxLabel="1分" Name="opt_28" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label26" runat="server" Text="蚂蚁式" Cls="mylabel2" />
                                                <ext:Container ID="Container27" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_29_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_29" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_29_2" runat="server" BoxLabel="1分" Name="opt_29" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet12" runat="server" Flex="1" Title="透析状况评估" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label27" runat="server" Text="*水多导致透析低血压(当月透析次数>1/4)" Cls="mylabel2" />
                                                <ext:Container ID="Container28" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_30_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_30" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_30_2" runat="server" BoxLabel="1分" Name="opt_30" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_30_3" runat="server" BoxLabel="2分" Name="opt_30" >
                                                           <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                     </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label28" runat="server" Text="*水分增加5%(当月透析次数>1/2)" Cls="mylabel2" />
                                                <ext:Container ID="Container23" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_31_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_31" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_31_2" runat="server" BoxLabel="1分" Name="opt_31" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_31_3" runat="server" BoxLabel="2分" Name="opt_31" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label22" runat="server" Text="*止血不易(加压止血15min以上)" Cls="mylabel2" />
                                                <ext:Container ID="Container24" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_32_1" runat="server" FieldLabel="得分(标准2分)" LabelAlign="Right" BoxLabel="0分" Name="opt_32" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_32_2" runat="server" BoxLabel="1分" Name="opt_32" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_32_3" runat="server" BoxLabel="2分" Name="opt_32" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio2"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label23" runat="server" Text="内瘘感染" Cls="mylabel2" />
                                                <ext:Container ID="Container29" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_33_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_33" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_33_2" runat="server" BoxLabel="1分" Name="opt_33" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label29" runat="server" Text="穿刺困难导致血肿" Cls="mylabel2" />
                                                <ext:Container ID="Container34" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_34_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_34" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_34_2" runat="server" BoxLabel="1分" Name="opt_34" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label33" runat="server" Text="Qb<200ml/min(透析中无法维持)" Cls="mylabel2" />
                                                <ext:Container ID="Container35" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_35_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_35" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_35_2" runat="server" BoxLabel="1分" Name="opt_35" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label34" runat="server" Text="静脉压高(Qb=200ml/min，静脉压>150mmHg)" Cls="mylabel2" />
                                                <ext:Container ID="Container30" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_36_1" runat="server" FieldLabel="得分(标准1分)" LabelAlign="Right" BoxLabel="0分" Name="opt_36" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_36_2" runat="server" BoxLabel="1分" Name="opt_36" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        
                                        <ext:TextField ID="txt_37" runat="server" FieldLabel="总分" IndicatorText="" ReadOnly="true" Cls="mylabel1"  />

                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="血管护理计划" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container36" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_38_1" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="一般危险群" Name="opt_38" Cls="mylabel2" ReadOnly="true" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container37" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label35" runat="server" Text="*评估得分表：总分<10分" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container42" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label38" runat="server" Text="护理计划：" Cls="mylabel2" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container43" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label39" runat="server" Text="1.制订血管护理计划(给内瘘日常护理及如何促进血管功能健康教育单)：根据患者血管功能情况决定握球次数(0-1次/日)，热敷至少1次/日，15min/次。" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container44" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label40" runat="server" Text="2.透析中血管功能异常(静脉压高、止血不易、Qb<200ml/min)：当连续发生3次异常即须提出讨论是否安排PTA(经皮腔内血管成形术)" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container38" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_38_2" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="中危险群(符合以下任一项条件者勾选此项)" Name="opt_38" Cls="mylabel2" ReadOnly="true" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container39" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label36" runat="server" Text="1.评估得分表：总分11-20分" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container45" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label41" runat="server" Text="2.60岁以上、血管性质为Graft" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container46" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label42" runat="server" Text="3.血管条件得分为6分" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container47" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label43" runat="server" Text="4.透析状态评估有2项异常" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container48" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label44" runat="server" Text="护理计划：" Cls="mylabel2" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container49" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label45" runat="server" Text="1.制订血管护理计划(给内瘘日常护理及如何促进血管功能健康教育单)：握球(含Graft)运动至少2次/日(早晚)，至少50下/次及2次(早晚)每次15min。" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container50" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label46" runat="server" Text="2.透析状况评估异常，根据患者问题作护理计划(护理计划单执行及评值)" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container51" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label47" runat="server" Text="*透析中血管功能异常(静脉压高、止血不易、Qb<200ml/min)：当连续发生3次异常即须提出讨论是否安排PTA。" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container40" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_38_3" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="高危险群(符合以下任一项条件者勾选此项)" Name="opt_38" Cls="mylabel2" ReadOnly="true" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container52" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label48" runat="server" Text="1.评估得分表：总分21分以上" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container53" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label49" runat="server" Text="2.男性、60岁以上、血管性质为Graft" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container54" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label50" runat="server" Text="3.血管条件得分为8分" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container55" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label51" runat="server" Text="4.透析状态评估有3项以上异常" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container56" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label52" runat="server" Text="护理计划：" Cls="mylabel2" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container57" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label53" runat="server" Text="1.制订血管护理计划(给内瘘日常护理及如何促进血管功能健康教育单)：握球(含Graft)运动至少2次/日(早晚)，至少50下/次及2次(早晚)每次15min。" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container58" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label54" runat="server" Text="2.透析状况评估异常护理计划(护理计划单执行及评值)" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container59" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label55" runat="server" Text="*水多导致透析低血压" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container60" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label56" runat="server" Text="*水分增加>5%" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container61" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label57" runat="server" Text="*透析中低血压" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container41" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label37" runat="server" Text="*透析中血管功能异常(静脉压高、止血不易、Qb<200ml/min)：当发生1次异常即须提出讨论是否排PTA。" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:Label ID="Label58" runat="server" Text="填表说明：" Cls="mylabel2" />
                                        <ext:Container ID="Container62" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Label ID="Label59" runat="server" Text="1.专责于每月第四周进行评估，并于当周完成。" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container63" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Label ID="Label60" runat="server" Text="2.依照表格项目及标准依次于得分栏给分。" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container64" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Label ID="Label61" runat="server" Text="3.给分标准：加注*者为高发生率危险因素(经过统计>1/2患者有此问题)故标准分为2分，未加注*者(经过统计<1/2患者有此问题)故标准分为1分，若患者无列表的危险因素项目则得分为0分)。" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container65" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Label ID="Label62" runat="server" Text="4.评估完成须统计总分，并于血管护理计划栏依照患者情况以打v方式勾选危险等级并依表列出项目执行护理计划。" />
                                            </Items>
                                        </ext:Container>


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
