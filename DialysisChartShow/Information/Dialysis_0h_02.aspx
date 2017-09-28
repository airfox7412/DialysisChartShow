<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_02.aspx.cs" 
    Inherits="Dialysis_Chart_Show.Information.Dialysis_0h_02" %>

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
    <div>

        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血管通路动静脉内瘘物理检查评估表" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="诊断日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="一、患者基本信息" Layout="AnchorLayout" Collapsible="true" Collapsed="false"  >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_1" runat="server" FieldLabel="初始透析日期" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="身高" IndicatorText="cm" />

                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_3_1" runat="server" FieldLabel="婚姻状况" BoxLabel="未婚" Name="opt_3" />
                                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="已婚" Name="opt_3" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_4_1" runat="server" FieldLabel="文化程度" BoxLabel="初中以下" Name="opt_4" />
                                                        <ext:Radio ID="opt_4_2" runat="server" BoxLabel="高中" Name="opt_4" />
                                                        <ext:Radio ID="opt_4_3" runat="server" BoxLabel="大学" Name="opt_4" />
                                                        <ext:Radio ID="opt_4_4" runat="server" BoxLabel="本科以上" Name="opt_4" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_1" runat="server" FieldLabel="依从性" BoxLabel="好" Name="opt_5" />
                                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="中" Name="opt_5" />
                                                        <ext:Radio ID="opt_5_3" runat="server" BoxLabel="差" Name="opt_5" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_6_1" runat="server" FieldLabel="生活状态" BoxLabel="被抚养" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_2" runat="server" BoxLabel="独立工作" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_3" runat="server" BoxLabel="因病半工作" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_4" runat="server" BoxLabel="因病不工作" Name="opt_6" />
                                                        <ext:Radio ID="opt_6_5" runat="server" BoxLabel="已退休" Name="opt_6" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_7_1" runat="server" FieldLabel="医保类型" BoxLabel="居民" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_2" runat="server" BoxLabel="职工" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_3" runat="server" BoxLabel="市公费" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_4" runat="server" BoxLabel="农保" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_5" runat="server" BoxLabel="自费" Name="opt_7" />
                                                        <ext:Radio ID="opt_7_6" runat="server" BoxLabel="其他" Name="opt_7" />
                                                        <ext:TextField ID="txt_8" runat="server"  />
                                                    </Items>
                                                </ext:Container>

                                        <%-- </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="透析相关病史" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items> --%>

                                                <ext:Label ID="Label11" runat="server" Text="透析相关病史" Cls="mylabel2" />
                                                
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_9_1" runat="server" FieldLabel="原发病" BoxLabel="慢性肾小球肾炎" />
                                                        <ext:Checkbox ID="chk_9_2" runat="server" BoxLabel="糖尿病" />
                                                        <ext:Checkbox ID="chk_9_3" runat="server" BoxLabel="多囊肾" />
                                                        <ext:Checkbox ID="chk_9_4" runat="server" BoxLabel="高血压肾硬化" />
                                                        <ext:Checkbox ID="chk_9_5" runat="server" BoxLabel="狼疮性肾炎" />
                                                        <ext:Checkbox ID="chk_9_6" runat="server" BoxLabel="痛风性肾损害" />
                                                        <ext:Checkbox ID="chk_9_7" runat="server" BoxLabel="免疫系统疾病" />
                                                        <ext:Checkbox ID="chk_9_8" runat="server" BoxLabel="遗传性疾病" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_10" runat="server" LabelAlign="Right" FieldLabel="其它" />
                                                
                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_11_1" runat="server" FieldLabel="并发症" BoxLabel="糖尿病" />
                                                        <ext:Checkbox ID="chk_11_2" runat="server" BoxLabel="高血压" />
                                                        <ext:Checkbox ID="chk_11_3" runat="server" BoxLabel="CVD" />
                                                        <ext:Checkbox ID="chk_11_4" runat="server" BoxLabel="CHD" />
                                                        <ext:Checkbox ID="chk_11_5" runat="server" BoxLabel="PUD" />
                                                        <ext:Checkbox ID="chk_11_6" runat="server" BoxLabel="CBVD" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_12_1" runat="server" FieldLabel="透析年限" BoxLabel="1年内" Name="opt_12" />
                                                        <ext:Radio ID="opt_12_2" runat="server" BoxLabel="5年内" Name="opt_12" />
                                                        <ext:Radio ID="opt_12_3" runat="server" BoxLabel="5-10年内" Name="opt_12" />
                                                        <ext:Radio ID="opt_12_4" runat="server" BoxLabel="10-15年内" Name="opt_12" />
                                                        <ext:Radio ID="opt_12_5" runat="server" BoxLabel="15-20年内" Name="opt_12" />
                                                        <ext:Radio ID="opt_12_6" runat="server" BoxLabel="20年以上" Name="opt_12" />
                                                    </Items>
                                                </ext:Container>
                                                 
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="二、血管通路的物理检查" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label2" runat="server" Text="1.血管通路基本信息" Cls="mylabel1" />
                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_1" runat="server" FieldLabel="血管通路方式" BoxLabel="自体动静脉内瘘" Name="opt_13" />
                                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="自体移植动静脉内瘘" Name="opt_13" />
                                                        <ext:Radio ID="opt_13_3" runat="server" BoxLabel="人造移植动静脉内瘘" Name="opt_13" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_14_1" runat="server" FieldLabel="血管通路动静脉内瘘位置" BoxLabel="左侧低位瘘" Name="opt_14" />
                                                        <ext:Radio ID="opt_14_2" runat="server" BoxLabel="左侧高位瘘" Name="opt_14" />
                                                        <ext:Radio ID="opt_14_3" runat="server" BoxLabel="右侧低位瘘" Name="opt_14" />
                                                        <ext:Radio ID="opt_14_4" runat="server" BoxLabel="右侧高位瘘" Name="opt_14" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                        <ext:TextField ID="txt_15" runat="server" FieldLabel="血管通路动静脉内瘘手术次数" LabelWidth="220"  IndicatorText="次" />
                                                        <ext:TextField ID="txt_16" runat="server" FieldLabel="现在血管通路使用年限(包括手术干预等)" LabelWidth ="220" IndicatorText="年" />
                                            
                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_17_1" runat="server" FieldLabel="感染史" BoxLabel="无" Name="opt_17" />
                                                        <ext:Radio ID="opt_17_2" runat="server" BoxLabel="有" Name="opt_17" >
                                                            <DirectEvents>
                                                                <Change OnEvent="ii"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="2">
                                                            <Items>
                                                                <ext:Radio ID="opt_18_1" runat="server"  LabelWidth ="50" FieldLabel="若有为" LabelAlign ="Right" BoxLabel="局部感染" Name="opt_18" Hidden="true"  />
                                                                <ext:Radio ID="opt_18_2" runat="server" BoxLabel="全身感染" Name="opt_18" Hidden="true" />
                                                                <ext:Radio ID="opt_18_3" runat="server" BoxLabel="其它" Name="opt_18" Hidden="true" />
                                                                <ext:TextField ID="txt_19" runat="server" FieldLabel="" Name="" Hidden="true" />
                                                            </Items>
                                                        </ext:Container>

                                                    </Items>
                                                </ext:Container>

                                                <%--<ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_17_1" runat="server" FieldLabel="若有为" LabelAlign ="Right" BoxLabel="局部感染" Name="opt_17" Hidden="true"  />
                                                        <ext:Radio ID="opt_17_2" runat="server" BoxLabel="全身感染" Name="opt_17" Hidden="true" />
                                                        <ext:Radio ID="opt_17_3" runat="server" BoxLabel="其它" Name="opt_17" Hidden="true" />
                                                        <ext:TextField ID="txt_18" runat="server" FieldLabel="" Name="" Hidden="true" />
                                                    </Items>
                                                </ext:Container>--%>

                                                <ext:Label ID="Label1" runat="server" Text="既往血管通路病史" Cls="mylabel1" />
                                                
                                                <ext:TextField ID="txt_20" runat="server" LabelWidth ="160" FieldLabel="1)中央静脉置管史，置管次数" />
                                                <ext:TextField ID="txt_21" runat="server" LabelWidth ="160" LabelAlign="Right" FieldLabel="置管留置时间" />
                                                <ext:TextField ID="txt_22" runat="server" LabelWidth ="160" LabelAlign="Right" FieldLabel="置管位置" />
                                                <ext:TextField ID="txt_23" runat="server" LabelWidth ="160" FieldLabel="2)瘘闭塞史，闭塞瘘次数" />
                                                <ext:TextField ID="txt_24" runat="server" LabelWidth ="160" LabelAlign="Right" FieldLabel="闭塞原因" Width ="500" />
                                                <ext:TextField ID="txt_25" runat="server" LabelWidth ="160" FieldLabel="3)若有瘘修补史，修补原因" Width ="500" />

                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_26_1" runat="server" LabelWidth ="160" FieldLabel="4)其他血管手术或介入史" BoxLabel="PICC" />
                                                        <ext:Checkbox ID="chk_26_2" runat="server" BoxLabel="起搏器安装史" />
                                                        <ext:Checkbox ID="chk_26_3" runat="server" BoxLabel="化疗" />
                                                        <ext:Checkbox ID="chk_26_4" runat="server" BoxLabel="血管手术" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_27" runat="server" LabelWidth ="160" LabelAlign="Right" FieldLabel="其它" />
                                                
                                                <ext:Label ID="Label3" runat="server" Text="2.血管通路物理检查" Cls="mylabel1" />
                                                <ext:Container ID="Container30" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label4" runat="server" Text="1)视诊" Cls="mylabel2" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container14" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_28_1" runat="server" FieldLabel="直径的评估" BoxLabel="<=2.5cm" Name="opt_28" />
                                                        <ext:Radio ID="opt_28_2" runat="server" BoxLabel=">2.5cm" Name="opt_28" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container15" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_29_1" runat="server" FieldLabel="深度的评估" BoxLabel="浅" Name="opt_29" />
                                                        <ext:Radio ID="opt_29_2" runat="server" BoxLabel="较浅" Name="opt_29" />
                                                        <ext:Radio ID="opt_29_3" runat="server" BoxLabel="深" Name="opt_29" />
                                                        <ext:Radio ID="opt_29_4" runat="server" BoxLabel="很深" Name="opt_29" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container16" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_30_1" runat="server" FieldLabel="穿刺可用长度评估" BoxLabel="<5cm" Name="opt_30" />
                                                        <ext:Radio ID="opt_30_2" runat="server" BoxLabel="5-10cm" Name="opt_30" />
                                                        <ext:Radio ID="opt_30_3" runat="server" BoxLabel=">10cm" Name="opt_30" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container17" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_31_1" runat="server" FieldLabel="侧枝血管" BoxLabel="无" Name="opt_31" />
                                                        <ext:Radio ID="opt_31_2" runat="server" BoxLabel="有" Name="opt_31" >
                                                            <DirectEvents>
                                                                <Change OnEvent="kk"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Container ID="Container18" runat="server" Layout="ColumnLayout" Padding="2">
                                                            <Items>
                                                                <ext:Radio ID="opt_32_1" runat="server" LabelWidth ="255" FieldLabel="若有侧枝血管，结合触诊评估结果：侧枝直径" LabelAlign="Right" BoxLabel="<=1/4主干直径" Name="opt_32" Hidden="true" />
                                                                <ext:Radio ID="opt_32_2" runat="server" BoxLabel=">1/4主干直径" Name="opt_32" Hidden="true" />
                                                            </Items>
                                                        </ext:Container>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container19" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_33_1" runat="server" LabelWidth ="105" FieldLabel="动脉瘤/假性动脉瘤" BoxLabel="无" Name="opt_33" />
                                                        <ext:Radio ID="opt_33_2" runat="server" BoxLabel="有" Name="opt_33" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container29" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label7" runat="server" Text="若有动脉瘤/假性动脉瘤，特点：" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Label ID="Label10" runat="server" Text="1)形状：" Cls="mylabel3" />
                                                        <ext:Radio ID="opt_34_1" runat="server" BoxLabel="条索状，直径>1cm" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_2" runat="server" BoxLabel="条索状，直径<1cm" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_3" runat="server" BoxLabel="球状，直径>3cm" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_4" runat="server" BoxLabel="球状，直径<3cm" Name="opt_34" />
                                                
                                                <ext:Label ID="Label9" runat="server" Text="2)狭窄：" Cls="mylabel3" />
                                                <ext:Container ID="Container21" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_35_1" runat="server" LabelAlign="Right" FieldLabel="部位" BoxLabel="瘘口" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_2" runat="server" LabelAlign="Right" BoxLabel="瘘口上" Name="opt_35" />
                                                        <ext:TextField ID="txt_36" runat="server" FieldLabel="" IndicatorText="cm" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container38" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_37_1" runat="server" LabelAlign="Right" FieldLabel="形状" BoxLabel="点状" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_2" runat="server" LabelAlign="Right" BoxLabel="段状" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_3" runat="server" LabelAlign="Right" BoxLabel="串珠状" Name="opt_37" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Label ID="Label8" runat="server" Text="3)皮肤：" Cls="mylabel3" />
                                                <ext:Container ID="Container22" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_38_1" runat="server" LabelAlign="Right" FieldLabel="颜色" BoxLabel="正常" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_2" runat="server" BoxLabel="色素沉着" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_3" runat="server" BoxLabel="色素脱失" Name="opt_38" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container23" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_39_1" runat="server" LabelAlign="Right" FieldLabel="厚度" BoxLabel="正常" Name="opt_39" />
                                                        <ext:Radio ID="opt_39_2" runat="server" BoxLabel="变薄" Name="opt_39" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container24" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_40_1" runat="server" LabelAlign="Right" FieldLabel="破溃" BoxLabel="无" Name="opt_40" />
                                                        <ext:Radio ID="opt_40_2" runat="server" BoxLabel="有" Name="opt_40" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container25" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_41_1" runat="server" LabelAlign="Right" FieldLabel="出血" BoxLabel="有" Name="opt_41" />
                                                        <ext:Radio ID="opt_41_2" runat="server" BoxLabel="无" Name="opt_41" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container26" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_42_1" runat="server" FieldLabel="局部血管塌陷" BoxLabel="无" Name="opt_42" />
                                                        <ext:Radio ID="opt_42_2" runat="server" BoxLabel="有" Name="opt_42" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container27" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_43_1" runat="server" FieldLabel="血管迂曲" BoxLabel="无" Name="opt_43" />
                                                        <ext:Radio ID="opt_43_2" runat="server" BoxLabel="有" Name="opt_43" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container28" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_44_1" runat="server" FieldLabel="感染" BoxLabel="无" Name="opt_44" />
                                                        <ext:Radio ID="opt_44_2" runat="server" BoxLabel="有" Name="opt_44" >
                                                            <DirectEvents>
                                                                <Change OnEvent="hh"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_45" runat="server" FieldLabel="如有感染，特点为" LabelAlign="Right" LabelWidth="110" IndicatorText="" Hidden="true" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Label ID="Label12" runat="server" Text="内瘘侧枝体末端检查，并与对侧相比较有无异常：" />
                                                <ext:Container ID="Container20" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_46_1" runat="server" FieldLabel="　" LabelAlign="Right" BoxLabel="无" Name="opt_46" />
                                                        <ext:Radio ID="opt_46_2" runat="server" BoxLabel="有" Name="opt_46" >
                                                            <%--<DirectEvents>
                                                                <Change OnEvent="kk"></Change>
                                                            </DirectEvents>--%>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container33" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Checkbox ID="chk_47_1" runat="server" FieldLabel="　" LabelAlign="Right" BoxLabel="苍白" />
                                                        <ext:Checkbox ID="chk_47_2" runat="server" BoxLabel="肿胀" />
                                                        <ext:Checkbox ID="chk_47_3" runat="server" BoxLabel="静脉曲张" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_48" runat="server" FieldLabel="其他" LabelAlign="Right" IndicatorText="" />
                                                
                                                <ext:Label ID="Label13" runat="server" Text="观察肩部、颈部、胸部有无血管扩张及颜面是否水肿：" />
                                                <ext:Container ID="Container34" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_49_1" runat="server" FieldLabel="　" LabelAlign="Right" BoxLabel="无" Name="opt_49" />
                                                        <ext:Radio ID="opt_49_2" runat="server" BoxLabel="有" Name="opt_49" >
                                                            <DirectEvents>
                                                                <Change OnEvent="jj"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:TextField ID="txt_50" runat="server" FieldLabel="若有，分布区域及特点为" LabelWidth="150" LabelAlign="Right" IndicatorText="" Hidden="true" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container35" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label14" runat="server" Text="肢体抬高试验" Cls="mylabel1" />
                                                   </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container36" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label15" runat="server" Text="方法1：将手臂自然下垂或自然放置观察内瘘处通路情况，然后将手臂抬高至心脏水平以上，再观察通路情况" />
                                                   </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container37" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label16" runat="server" Text="方法2：患者取卧位，举起内瘘侧上肢，与身体约呈90°，观察瘘体及流出段血管塌陷情况" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_51_1" runat="server" FieldLabel="结果" LabelAlign="Right" BoxLabel="正常的通路下垂或自然放置时通路处扩张，抬高后会塌陷(即使内瘘压力很大，也会变得松弛)" Name="opt_51" />
                                                <ext:Radio ID="opt_51_2" runat="server" BoxLabel="存在狭窄的通路下垂或自然放置时会扩张，抬高后近端血管会塌陷，而远端血管仍会扩张" Name="opt_51" />
                                                <ext:Label ID="Label17" runat="server" Text="视诊血管通路物理检查小结：" Cls="mylabel1" />
                                                <ext:TextField ID="txt_52" runat="server" FieldLabel="" IndicatorText="" Width="500" />

                                                <ext:Label ID="Label21" runat="server" Text="2)触诊" Cls = "mylabel2" />
                                                <ext:Container ID="Container31" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                <ext:Label ID="Label5" runat="server" Text="用手指指腹依次触摸流入段，瘘体与流出段，结合视诊评估血管的粗细，张力是否正常" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container39" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_53_1" runat="server" FieldLabel="搏动" LabelAlign="Left" BoxLabel="无" Name="opt_53" />
                                                        <ext:Radio ID="opt_53_2" runat="server" BoxLabel="有" Name="opt_53" >
                                                            <DirectEvents>
                                                                <Change OnEvent="ll"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Container ID="Container40" runat="server" Layout="ColumnLayout" Padding="2">
                                                            <Items>
                                                                <ext:Radio ID="opt_54_1" runat="server" LabelWidth ="100" FieldLabel="若有搏动，特点" LabelAlign="Right" BoxLabel="柔和有力，易于触摸" Name="opt_54" Hidden="true" />
                                                                <ext:Radio ID="opt_54_2" runat="server" BoxLabel="水冲脉" Name="opt_54" Hidden="true" />
                                                                <ext:Radio ID="opt_54_3" runat="server" BoxLabel="微弱、很难触摸到" Name="opt_54" Hidden="true" />
                                                            </Items>
                                                        </ext:Container>
                                                    </Items>
                                                </ext:Container>
                                             
                                                <ext:Container ID="Container41" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label22" runat="server" Text="搏动增强试验" Cls="mylabel1" />
                                                   </Items>
                                                </ext:Container>
                                                
                                                <ext:Container ID="Container42" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label23" runat="server" Text="方法：用一只手将吻合口上方几厘米的血流阻断，并同时用另一只手来感受其他部位的搏动强度并进行比较，以判断通路中是否存在问题" />
                                                   </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_55_1" runat="server" FieldLabel="结果" LabelAlign="Right" BoxLabel="正常的通路，搏动的强弱与血流阻断的力道呈正比(即使通路存在狭窄)" Name="opt_55" />
                                                <ext:Radio ID="opt_55_2" runat="server" BoxLabel="如果搏动的强弱与血流阻断的力道没有任何关联(提示通路闭塞)" Name="opt_55" />
                                                
                                                <ext:Container ID="Container43" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_56_1" runat="server" FieldLabel="震动" LabelAlign="Left" BoxLabel="无" Name="opt_56" />
                                                        <ext:Radio ID="opt_56_2" runat="server" BoxLabel="有" Name="opt_56" >
                                                            <DirectEvents>
                                                                <Change OnEvent="mm"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                
                                                        <ext:Label ID="Label24" runat="server" Text="若有震动，特点：" Hidden="true" />
                                                
                                                <ext:Container ID="Container44" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_57_1" runat="server" FieldLabel="连续性" LabelAlign="Right" BoxLabel="连续" Name="opt_57" Hidden="true" />
                                                        <ext:Radio ID="opt_57_2" runat="server" BoxLabel="不连续" Name="opt_57" Hidden="true" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container45" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_58_1" runat="server" FieldLabel="双期" LabelAlign="Right" BoxLabel="有收缩期和舒张期" Name="opt_58" Hidden="true" />
                                                        <ext:Radio ID="opt_58_2" runat="server" BoxLabel="仅存在收缩期" Name="opt_58" Hidden="true" />
                                                        <ext:Radio ID="opt_58_3" runat="server" BoxLabel="没有" Name="opt_58" Hidden="true" />
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Label ID="Label26" runat="server" Text="连续阻断试验" Cls="mylabel1" />
                                                <ext:Container ID="Container46" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label25" runat="server" Text="方法：用一只手将血流量阻断，用另一只手来感受上游血管有无震颤" />
                                                   </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container47" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label27" runat="server" Text="结果：如果存在震颤说明这段距离内的血管存在分支；如果没有震颤说明这段血管内不存在分支，并移动不同的点进行判断" />
                                                   </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_59_1" runat="server" FieldLabel="是否存在侧枝静脉" BoxLabel="无" Name="opt_59" />
                                                <ext:Radio ID="opt_59_2" runat="server" BoxLabel="有" Name="opt_59" />
                                                
                                                <ext:Label ID="Label20" runat="server" Text="触诊物理检查小结：" Cls="mylabel1" />
                                                <ext:TextField ID="txt_60" runat="server" FieldLabel="" IndicatorText="" Width="500" />

                                                <ext:Label ID="Label6" runat="server" Text="3)听诊" Cls = "mylabel2" />
                                                <ext:Container ID="Container48" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_61_1" runat="server" FieldLabel="杂音的音调" BoxLabel="低" Name="opt_61" />
                                                        <ext:Radio ID="opt_61_2" runat="server" BoxLabel="高" Name="opt_61" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container49" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_62_1" runat="server" FieldLabel="杂音的分期" BoxLabel="存在收缩期和舒张期" Name="opt_62" />
                                                        <ext:Radio ID="opt_62_2" runat="server" BoxLabel="仅存在收缩期" Name="opt_62" />
                                                        <ext:Radio ID="opt_62_3" runat="server" BoxLabel="没有收缩期和舒张期" Name="opt_62" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container50" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_63_1" runat="server" FieldLabel="杂音的连续性" BoxLabel="连续" Name="opt_63" />
                                                        <ext:Radio ID="opt_63_2" runat="server" BoxLabel="不连续" Name="opt_63" />
                                                    </Items>
                                                </ext:Container>
                                             
                                                
                                                <ext:Container ID="Container32" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Label ID="Label19" runat="server" Text="听诊物理检查小结：" Cls="mylabel1" />
                                                <ext:TextField ID="txt_64" runat="server" FieldLabel="" IndicatorText="" Width="500" />

                                                
                                                <ext:Label ID="Label18" runat="server" Text="3.血管通路物理检查诊断：" Cls="mylabel1" />
                                                <ext:TextField ID="txt_65" runat="server" FieldLabel="" IndicatorText="" Width="500" />
                                                <ext:TextField ID="txt_66" runat="server" FieldLabel="" IndicatorText="" Width="500" />
                                                <ext:TextField ID="txt_67" runat="server" FieldLabel="" IndicatorText="" Width="500" />

                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_print" runat="server" Icon="Printer" Text="打印" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Print_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
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
    
    </div>
    </form>
</body>
</html>
