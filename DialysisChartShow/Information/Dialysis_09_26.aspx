<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_26.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_09_26" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="随访记录" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="家访日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="一、基本资料" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_1" runat="server" FieldLabel="评估者" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="家访次数" />
                                                <ext:DateField ID="dat_3" runat="server" FieldLabel="下次家访日期" Format="yyyy-MM-dd">
                                                </ext:DateField>
<%--                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="年龄" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="体重" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="身高" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="地址" />
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="电话" />--%>
                                                <ext:DateField ID="dat_9" runat="server" FieldLabel="腹膜透析起始日" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:TextField ID="txt_10" runat="server" FieldLabel="曾接受血液透析" />
                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_11" runat="server" FieldLabel="多久" IndicatorText="年  " />
                                                        <ext:TextField ID="txt_12" runat="server" FieldLabel="" IndicatorText="月" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_1" runat="server" FieldLabel="使用系统" BoxLabel="Ultra-Bag " Name="opt_13" />
                                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="Home Choice " Name="opt_13" />
                                                        <ext:Radio ID="opt_13_3" runat="server" BoxLabel="Quantum " Name="opt_13" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container14" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_14_1" runat="server" FieldLabel="处方" BoxLabel="CAPD " Name="opt_14" />
                                                        <ext:Radio ID="opt_14_2" runat="server" BoxLabel="NIPD " Name="opt_14" />
                                                        <ext:Radio ID="opt_14_3" runat="server" BoxLabel="CCPD " Name="opt_14" />
                                                        <ext:Radio ID="opt_14_4" runat="server" BoxLabel="CCPD+Day Exchange " Name="opt_14" />
                                                        <ext:Radio ID="opt_14_5" runat="server" BoxLabel="Quantum " Name="opt_14" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container15" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_15_1" runat="server" FieldLabel="浓度" BoxLabel="1.5%" Name="opt_15" />
                                                        <ext:TextField ID="txt_16" runat="server" FieldLabel="" IndicatorText="包 " />
                                                        <ext:Radio ID="opt_15_2" runat="server" BoxLabel="2.5%" Name="opt_15" />
                                                        <ext:TextField ID="txt_17" runat="server" FieldLabel="" IndicatorText="包 " />
                                                        <ext:Radio ID="opt_15_3" runat="server" BoxLabel="4.25%" Name="opt_15" />
                                                        <ext:TextField ID="txt_18" runat="server" FieldLabel="" IndicatorText="包 " />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container19" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_19_1" runat="server" FieldLabel="换液次数" BoxLabel="3次 " Name="opt_19" />
                                                        <ext:Radio ID="opt_19_2" runat="server" BoxLabel="4次 " Name="opt_19" />
                                                        <ext:Radio ID="opt_19_3" runat="server" BoxLabel="5次 " Name="opt_19" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container20" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_20_1" runat="server" FieldLabel="容量" BoxLabel="1.5L " Name="opt_20" />
                                                        <ext:Radio ID="opt_20_2" runat="server" BoxLabel="2L " Name="opt_20" />
                                                        <ext:Radio ID="opt_20_3" runat="server" BoxLabel="2.5L " Name="opt_20" />
                                                        <ext:Radio ID="opt_20_4" runat="server" BoxLabel="5L " Name="opt_20" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container21" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_21_1" runat="server" FieldLabel="腹膜炎" BoxLabel="无 " Name="opt_21" />
                                                        <ext:Radio ID="opt_21_2" runat="server" BoxLabel="有 " Name="opt_21" />
                                                        <ext:TextField ID="txt_22" runat="server" FieldLabel="最后一次发生日期" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container23" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_23" runat="server" FieldLabel="菌种  " />
                                                        <ext:TextField ID="txt_24" runat="server" FieldLabel="原因" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container25" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_25_1" runat="server" FieldLabel="Exit Site Infection" BoxLabel="无 "
                                                            Name="opt_25" />
                                                        <ext:Radio ID="opt_25_2" runat="server" BoxLabel="有 " Name="opt_25" />
                                                        <ext:TextField ID="txt_26" runat="server" FieldLabel="最后一次发生日期" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_27" runat="server" FieldLabel="菌种  " />
                                                        <ext:TextField ID="txt_28" runat="server" FieldLabel="原因" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="二、目前情形" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container29" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_29_1" runat="server" FieldLabel="行动程度" BoxLabel="正常 " Name="opt_29" />
                                                        <ext:Radio ID="opt_29_2" runat="server" BoxLabel="需人扶持 " Name="opt_29" />
                                                        <ext:Radio ID="opt_29_3" runat="server" BoxLabel="轮椅 " Name="opt_29" />
                                                        <ext:Radio ID="opt_29_4" runat="server" BoxLabel="助行器 " Name="opt_29" />
                                                        <ext:Radio ID="opt_29_5" runat="server" BoxLabel="长期卧床 " Name="opt_29" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container30" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_30_1" runat="server" FieldLabel="意识形态" BoxLabel="清醒 " Name="opt_30" />
                                                        <ext:Radio ID="opt_30_2" runat="server" BoxLabel="嗜睡 " Name="opt_30" />
                                                        <ext:Radio ID="opt_30_3" runat="server" BoxLabel="混乱 " Name="opt_30" />
                                                        <ext:Radio ID="opt_30_4" runat="server" BoxLabel="昏迷 " Name="opt_30" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_31" runat="server" FieldLabel="不适症状" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="三、换液情形" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container32" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_32_1" runat="server" FieldLabel="换液操作者" BoxLabel="病人本人 " Name="opt_32" />
                                                        <ext:Radio ID="opt_32_2" runat="server" BoxLabel="父母 " Name="opt_32" />
                                                        <ext:Radio ID="opt_32_3" runat="server" BoxLabel="兄弟姊妹 " Name="opt_32" />
                                                        <ext:Radio ID="opt_32_4" runat="server" BoxLabel="儿女 " Name="opt_32" />
                                                        <ext:Radio ID="opt_32_5" runat="server" BoxLabel="孙子女 " Name="opt_32" />
                                                        <ext:Radio ID="opt_32_6" runat="server" BoxLabel="其他亲戚 " Name="opt_32" />
                                                        <ext:Radio ID="opt_32_7" runat="server" BoxLabel="朋友 " Name="opt_32" />
                                                        <ext:Radio ID="opt_32_8" runat="server" BoxLabel="其他 " Name="opt_32" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_33" runat="server" FieldLabel="每日平均脱水量" />
                                                <ext:TextField ID="txt_34" runat="server" FieldLabel="平均留置时间" />
                                                <ext:Container ID="Container35" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_35_1" runat="server" FieldLabel="加药状况" BoxLabel="无 " Name="opt_35" />
                                                        <ext:Radio ID="opt_35_2" runat="server" BoxLabel="胰岛素 " Name="opt_35" />
                                                        <ext:Radio ID="opt_35_3" runat="server" BoxLabel="肝素 " Name="opt_35" />
                                                        <ext:Radio ID="opt_35_4" runat="server" BoxLabel="抗生素 " Name="opt_35" />
                                                        <ext:Radio ID="opt_35_5" runat="server" BoxLabel="其他 " Name="opt_35" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="四、生活状况" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container36" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_36_1" runat="server" FieldLabel="复健情形" BoxLabel="全职工作 " Name="opt_36" />
                                                        <ext:Radio ID="opt_36_2" runat="server" BoxLabel="兼职工作 " Name="opt_36" />
                                                        <ext:Radio ID="opt_36_3" runat="server" BoxLabel="家管 " Name="opt_36" />
                                                        <ext:Radio ID="opt_36_4" runat="server" BoxLabel="学生 " Name="opt_36" />
                                                        <ext:Radio ID="opt_36_5" runat="server" BoxLabel="其他 " Name="opt_36" />
                                                        <ext:TextField ID="txt_37" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container38" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_38_1" runat="server" FieldLabel="与家人互动" BoxLabel="好 " Name="opt_38" />
                                                        <ext:Radio ID="opt_38_2" runat="server" BoxLabel="尚可 " Name="opt_38" />
                                                        <ext:Radio ID="opt_38_3" runat="server" BoxLabel="不好 " Name="opt_38" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container39" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_39_1" runat="server" FieldLabel="睡眠习惯" BoxLabel="正常 " Name="opt_39" />
                                                        <ext:Radio ID="opt_39_2" runat="server" BoxLabel="失眠(使用安眠药) " Name="opt_39" />
                                                        <ext:Radio ID="opt_39_3" runat="server" BoxLabel="失眠(无使用安眠药) " Name="opt_39" />
                                                        <ext:Radio ID="opt_39_4" runat="server" BoxLabel="睡眠时数 " Name="opt_39" />
                                                        <ext:TextField ID="txt_40" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container41" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_41_1" runat="server" FieldLabel="饮食种类" BoxLabel="一般 " Name="opt_41" />
                                                        <ext:Radio ID="opt_41_2" runat="server" BoxLabel="素食 " Name="opt_41" />
                                                        <ext:Radio ID="opt_41_3" runat="server" BoxLabel="软质 " Name="opt_41" />
                                                        <ext:Radio ID="opt_41_4" runat="server" BoxLabel="流质 " Name="opt_41" />
                                                        <ext:Radio ID="opt_41_5" runat="server" BoxLabel="其他 " Name="opt_41" />
                                                        <ext:TextField ID="txt_42" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container43" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_43_1" runat="server" FieldLabel="营养补充品" BoxLabel="无 " Name="opt_43" />
                                                        <ext:Radio ID="opt_43_2" runat="server" BoxLabel="有 " Name="opt_43" />
                                                        <ext:TextField ID="txt_44" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container45" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_45_1" runat="server" FieldLabel="中药" BoxLabel="无 " Name="opt_45" />
                                                        <ext:Radio ID="opt_45_2" runat="server" BoxLabel="有 " Name="opt_45" />
                                                        <ext:TextField ID="txt_46" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container47" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_47_1" runat="server" FieldLabel="食欲" BoxLabel="佳 " Name="opt_47" />
                                                        <ext:Radio ID="opt_47_2" runat="server" BoxLabel="尚可 " Name="opt_47" />
                                                        <ext:Radio ID="opt_47_3" runat="server" BoxLabel="不好 " Name="opt_47" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container48" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_48" runat="server" FieldLabel="每日水分摄取量  " IndicatorText="ml " />
                                                        <ext:TextField ID="txt_49" runat="server" FieldLabel="尿量" IndicatorText="ml " />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:Container ID="Container50" runat="server" Layout="ColumnLayout" Padding="2">
                                            <Items>
                                                <ext:Radio ID="opt_50_1" runat="server" FieldLabel="排便情形" BoxLabel="正常 " Name="opt_50" />
                                                <ext:Radio ID="opt_50_2" runat="server" BoxLabel="便秘 " Name="opt_50" />
                                                <ext:Radio ID="opt_50_3" runat="server" BoxLabel="腹泻 " Name="opt_50" />
                                                <ext:Radio ID="opt_50_4" runat="server" BoxLabel="其他 " Name="opt_50" />
                                                <ext:TextField ID="txt_51" runat="server" FieldLabel="" />
                                            </Items>
                                        </ext:Container>
                                        <ext:FieldSet ID="FieldSet11" runat="server" Flex="1" Title="一、居家环境卫生评估" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container52" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_52_1" runat="server" FieldLabel="地板清洁干燥" BoxLabel="有 " Name="opt_52" LabelWidth="150" />
                                                        <ext:Radio ID="opt_52_2" runat="server" BoxLabel="无 " Name="opt_52" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container53" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_53_1" runat="server" FieldLabel="铺设地毯" BoxLabel="有 " Name="opt_53" LabelWidth="150" />
                                                        <ext:Radio ID="opt_53_2" runat="server" BoxLabel="无 " Name="opt_53" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container54" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_54_1" runat="server" FieldLabel="饲养宠物(家禽或家畜)" BoxLabel="有 " Name="opt_54" LabelWidth="150" />
                                                        <ext:Radio ID="opt_54_2" runat="server" BoxLabel="无 " Name="opt_54" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container55" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_55_1" runat="server" FieldLabel="蚂蚁、蟑螂、蜘蛛" BoxLabel="有 " Name="opt_55" LabelWidth="150" />
                                                        <ext:Radio ID="opt_55_2" runat="server" BoxLabel="无 " Name="opt_55" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container56" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_56_1" runat="server" FieldLabel="有特定的换液房间" BoxLabel="有 " Name="opt_56" LabelWidth="150" />
                                                        <ext:Radio ID="opt_56_2" runat="server" BoxLabel="无 " Name="opt_56" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container57" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_57_1" runat="server" FieldLabel="洗手台邻近换液室" BoxLabel="有 " Name="opt_57" LabelWidth="150" />
                                                        <ext:Radio ID="opt_57_2" runat="server" BoxLabel="无 " Name="opt_57" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container58" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_58_1" runat="server" FieldLabel="洗手台清洁" BoxLabel="有 " Name="opt_58" LabelWidth="150" />
                                                        <ext:Radio ID="opt_58_2" runat="server" BoxLabel="无 " Name="opt_58" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet12" runat="server" Flex="1" Title="二、换液间环境评估" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container59" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_59_1" runat="server" FieldLabel="天花板、灰尘/蜘蛛网" BoxLabel="有 " Name="opt_59" LabelWidth="150" />
                                                        <ext:Radio ID="opt_59_2" runat="server" BoxLabel="无 " Name="opt_59" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container60" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_60_1" runat="server" FieldLabel="冷气口/风扇灰尘" BoxLabel="有 " Name="opt_60" LabelWidth="150" />
                                                        <ext:Radio ID="opt_60_2" runat="server" BoxLabel="无 " Name="opt_60" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container61" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_61_1" runat="server" FieldLabel="窗帘灰尘" BoxLabel="有 " Name="opt_61" LabelWidth="150" />
                                                        <ext:Radio ID="opt_61_2" runat="server" BoxLabel="无 " Name="opt_61" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container62" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_62_1" runat="server" FieldLabel="窗帘定期拆洗" BoxLabel="有 " Name="opt_62" LabelWidth="150" />
                                                        <ext:Radio ID="opt_62_2" runat="server" BoxLabel="无 " Name="opt_62" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container63" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_63_1" runat="server" FieldLabel="室内摆设整齐" BoxLabel="有 " Name="opt_63" LabelWidth="150" />
                                                        <ext:Radio ID="opt_63_2" runat="server" BoxLabel="无 " Name="opt_63" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container64" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_64_1" runat="server" FieldLabel="墙壁清洁干燥" BoxLabel="有 " Name="opt_64" LabelWidth="150" />
                                                        <ext:Radio ID="opt_64_2" runat="server" BoxLabel="无 " Name="opt_64" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container65" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_65_1" runat="server" FieldLabel="地板清洁干燥" BoxLabel="有 " Name="opt_65" LabelWidth="150" />
                                                        <ext:Radio ID="opt_65_2" runat="server" BoxLabel="无 " Name="opt_65" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container66" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_66_1" runat="server" FieldLabel="特定的换液桌面" BoxLabel="有 " Name="opt_66" LabelWidth="150" />
                                                        <ext:Radio ID="opt_66_2" runat="server" BoxLabel="无 " Name="opt_66" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container67" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_67_1" runat="server" FieldLabel="干净的换液桌面无多余物" BoxLabel="有 " Name="opt_67" LabelWidth="150" />
                                                        <ext:Radio ID="opt_67_2" runat="server" BoxLabel="无 " Name="opt_67" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container68" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_68_1" runat="server" FieldLabel="换液桌靠近门窗口" BoxLabel="有 " Name="opt_68" LabelWidth="150" />
                                                        <ext:Radio ID="opt_68_2" runat="server" BoxLabel="无 " Name="opt_68" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container69" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_69_1" runat="server" FieldLabel="承接引流袋的容器" BoxLabel="有 " Name="opt_69" LabelWidth="150" />
                                                        <ext:Radio ID="opt_69_2" runat="server" BoxLabel="无 " Name="opt_69" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container70" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_70_1" runat="server" FieldLabel="挂勾/衣架/点滴架" BoxLabel="有 " Name="opt_70" LabelWidth="150" />
                                                        <ext:Radio ID="opt_70_2" runat="server" BoxLabel="无 " Name="opt_70" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container71" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_71_1" runat="server" FieldLabel="有加盖的垃圾桶" BoxLabel="有 " Name="opt_71" LabelWidth="150" />
                                                        <ext:Radio ID="opt_71_2" runat="server" BoxLabel="无 " Name="opt_71" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet13" runat="server" Flex="1" Title="三、换液技术执行评估" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container72" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_72_1" runat="server" FieldLabel="关闭门窗" BoxLabel="不正确 " Name="opt_72" LabelWidth="150" />
                                                        <ext:Radio ID="opt_72_2" runat="server" BoxLabel="部分正确 " Name="opt_72" />
                                                        <ext:Radio ID="opt_72_3" runat="server" BoxLabel="完全正确 " Name="opt_72" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container73" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_73_1" runat="server" FieldLabel="关闭冷气/电风扇" BoxLabel="不正确 " Name="opt_73" LabelWidth="150" />
                                                        <ext:Radio ID="opt_73_2" runat="server" BoxLabel="部分正确 " Name="opt_73" />
                                                        <ext:Radio ID="opt_73_3" runat="server" BoxLabel="完全正确 " Name="opt_73" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container74" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_74_1" runat="server" FieldLabel="用物准备齐全" BoxLabel="不正确 " Name="opt_74" LabelWidth="150" />
                                                        <ext:Radio ID="opt_74_2" runat="server" BoxLabel="部分正确 " Name="opt_74" />
                                                        <ext:Radio ID="opt_74_3" runat="server" BoxLabel="完全正确 " Name="opt_74" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container75" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_75_1" runat="server" FieldLabel="洗手" BoxLabel="不正确 " Name="opt_75" LabelWidth="150" />
                                                        <ext:Radio ID="opt_75_2" runat="server" BoxLabel="部分正确 " Name="opt_75" />
                                                        <ext:Radio ID="opt_75_3" runat="server" BoxLabel="完全正确 " Name="opt_75" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container76" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_76_1" runat="server" FieldLabel="指甲清洁度" BoxLabel="不正确 " Name="opt_76" LabelWidth="150" />
                                                        <ext:Radio ID="opt_76_2" runat="server" BoxLabel="部分正确 " Name="opt_76" />
                                                        <ext:Radio ID="opt_76_3" runat="server" BoxLabel="完全正确 " Name="opt_76" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container77" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_77_1" runat="server" FieldLabel="以酒精清洁桌面正确性" BoxLabel="不正确 " Name="opt_77" LabelWidth="150" />
                                                        <ext:Radio ID="opt_77_2" runat="server" BoxLabel="部分正确 " Name="opt_77" />
                                                        <ext:Radio ID="opt_77_3" runat="server" BoxLabel="完全正确 " Name="opt_77" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container78" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_78_1" runat="server" FieldLabel="使用清洁剂/香皂" BoxLabel="不正确 " Name="opt_78" LabelWidth="150" />
                                                        <ext:Radio ID="opt_78_2" runat="server" BoxLabel="部分正确 " Name="opt_78" />
                                                        <ext:Radio ID="opt_78_3" runat="server" BoxLabel="完全正确 " Name="opt_78" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container79" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_79_1" runat="server" FieldLabel="流动的水龙头" BoxLabel="不正确 " Name="opt_79" LabelWidth="150" />
                                                        <ext:Radio ID="opt_79_2" runat="server" BoxLabel="部分正确 " Name="opt_79" />
                                                        <ext:Radio ID="opt_79_3" runat="server" BoxLabel="完全正确 " Name="opt_79" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container80" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_80_1" runat="server" FieldLabel="正确洗手步骤" BoxLabel="不正确 " Name="opt_80" LabelWidth="150" />
                                                        <ext:Radio ID="opt_80_2" runat="server" BoxLabel="部分正确 " Name="opt_80" />
                                                        <ext:Radio ID="opt_80_3" runat="server" BoxLabel="完全正确 " Name="opt_80" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container81" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_81_1" runat="server" FieldLabel="以纸巾关水龙头、门" BoxLabel="不正确 " Name="opt_81" LabelWidth="150" />
                                                        <ext:Radio ID="opt_81_2" runat="server" BoxLabel="部分正确 " Name="opt_81" />
                                                        <ext:Radio ID="opt_81_3" runat="server" BoxLabel="完全正确 " Name="opt_81" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container82" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_82_1" runat="server" FieldLabel="戴口罩(包括周遭人)" BoxLabel="不正确 " Name="opt_82" LabelWidth="150" />
                                                        <ext:Radio ID="opt_82_2" runat="server" BoxLabel="部分正确 " Name="opt_82" />
                                                        <ext:Radio ID="opt_82_3" runat="server" BoxLabel="完全正确 " Name="opt_82" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container83" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_83_1" runat="server" FieldLabel="正确加温步骤" BoxLabel="不正确 " Name="opt_83" LabelWidth="150" />
                                                        <ext:Radio ID="opt_83_2" runat="server" BoxLabel="部分正确 " Name="opt_83" />
                                                        <ext:Radio ID="opt_83_3" runat="server" BoxLabel="完全正确 " Name="opt_83" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container84" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_84_1" runat="server" FieldLabel="确实检查透析液(五项)" BoxLabel="不正确 "
                                                            Name="opt_84" LabelWidth="150" />
                                                        <ext:Radio ID="opt_84_2" runat="server" BoxLabel="部分正确 " Name="opt_84" />
                                                        <ext:Radio ID="opt_84_3" runat="server" BoxLabel="完全正确 " Name="opt_84" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container85" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_85_1" runat="server" FieldLabel="遵守无菌技术" BoxLabel="不正确 " Name="opt_85" LabelWidth="150" />
                                                        <ext:Radio ID="opt_85_2" runat="server" BoxLabel="部分正确 " Name="opt_85" />
                                                        <ext:Radio ID="opt_85_3" runat="server" BoxLabel="完全正确 " Name="opt_85" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container86" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_86_1" runat="server" FieldLabel="标准换液步骤" BoxLabel="不正确 " Name="opt_86" LabelWidth="150" />
                                                        <ext:Radio ID="opt_86_2" runat="server" BoxLabel="部分正确 " Name="opt_86" />
                                                        <ext:Radio ID="opt_86_3" runat="server" BoxLabel="完全正确 " Name="opt_86" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container87" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_87_1" runat="server" FieldLabel="检查引流袋" BoxLabel="不正确 " Name="opt_87" LabelWidth="150" />
                                                        <ext:Radio ID="opt_87_2" runat="server" BoxLabel="部分正确 " Name="opt_87" />
                                                        <ext:Radio ID="opt_87_3" runat="server" BoxLabel="完全正确 " Name="opt_87" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container88" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_88_1" runat="server" FieldLabel="引流袋秤重(磅秤)" BoxLabel="不正确 " Name="opt_88" LabelWidth="150" />
                                                        <ext:Radio ID="opt_88_2" runat="server" BoxLabel="部分正确 " Name="opt_88" />
                                                        <ext:Radio ID="opt_88_3" runat="server" BoxLabel="完全正确 " Name="opt_88" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container89" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_89_1" runat="server" FieldLabel="引流液倒入马桶" BoxLabel="不正确 " Name="opt_89" LabelWidth="150" />
                                                        <ext:Radio ID="opt_89_2" runat="server" BoxLabel="部分正确 " Name="opt_89" />
                                                        <ext:Radio ID="opt_89_3" runat="server" BoxLabel="完全正确 " Name="opt_89" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container90" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_90_1" runat="server" FieldLabel="每次引流量" BoxLabel="不正确 " Name="opt_90" LabelWidth="150" />
                                                        <ext:Radio ID="opt_90_2" runat="server" BoxLabel="部分正确 " Name="opt_90" />
                                                        <ext:Radio ID="opt_90_3" runat="server" BoxLabel="完全正确 " Name="opt_90" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container91" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_91_1" runat="server" FieldLabel="每次葡萄糖浓度" BoxLabel="不正确 " Name="opt_91" LabelWidth="150" />
                                                        <ext:Radio ID="opt_91_2" runat="server" BoxLabel="部分正确 " Name="opt_91" />
                                                        <ext:Radio ID="opt_91_3" runat="server" BoxLabel="完全正确 " Name="opt_91" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container92" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_92_1" runat="server" FieldLabel="每天血压" BoxLabel="不正确 " Name="opt_92" LabelWidth="150" />
                                                        <ext:Radio ID="opt_92_2" runat="server" BoxLabel="部分正确 " Name="opt_92" />
                                                        <ext:Radio ID="opt_92_3" runat="server" BoxLabel="完全正确 " Name="opt_92" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container93" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_93_1" runat="server" FieldLabel="每天体重" BoxLabel="不正确 " Name="opt_93" LabelWidth="150" />
                                                        <ext:Radio ID="opt_93_2" runat="server" BoxLabel="部分正确 " Name="opt_93" />
                                                        <ext:Radio ID="opt_93_3" runat="server" BoxLabel="完全正确 " Name="opt_93" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container94" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_94_1" runat="server" FieldLabel="异常状况" BoxLabel="不正确 " Name="opt_94" LabelWidth="150" />
                                                        <ext:Radio ID="opt_94_2" runat="server" BoxLabel="部分正确 " Name="opt_94" />
                                                        <ext:Radio ID="opt_94_3" runat="server" BoxLabel="完全正确 " Name="opt_94" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet14" runat="server" Flex="1" Title="四、导管出口处护理评估" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container95" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_95_1" runat="server" FieldLabel="确认是否过期正确打开敷料" BoxLabel="不正确 "
                                                            Name="opt_95" LabelWidth="150" />
                                                        <ext:Radio ID="opt_95_2" runat="server" BoxLabel="部分正确 " Name="opt_95" />
                                                        <ext:Radio ID="opt_95_3" runat="server" BoxLabel="完全正确 " Name="opt_95" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container96" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_96_1" runat="server" FieldLabel="检查导管出口处" BoxLabel="不正确 " Name="opt_96" LabelWidth="150" />
                                                        <ext:Radio ID="opt_96_2" runat="server" BoxLabel="部分正确 " Name="opt_96" />
                                                        <ext:Radio ID="opt_96_3" runat="server" BoxLabel="完全正确 " Name="opt_96" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container97" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_97_1" runat="server" FieldLabel="检查管隧" BoxLabel="不正确 " Name="opt_97" LabelWidth="150" />
                                                        <ext:Radio ID="opt_97_2" runat="server" BoxLabel="部分正确 " Name="opt_97" />
                                                        <ext:Radio ID="opt_97_3" runat="server" BoxLabel="完全正确 " Name="opt_97" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container98" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_98_1" runat="server" FieldLabel="正确消毒伤口" BoxLabel="不正确 " Name="opt_98" LabelWidth="150" />
                                                        <ext:Radio ID="opt_98_2" runat="server" BoxLabel="部分正确 " Name="opt_98" />
                                                        <ext:Radio ID="opt_98_3" runat="server" BoxLabel="完全正确 " Name="opt_98" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container99" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_99_1" runat="server" FieldLabel="正确包裹纱布" BoxLabel="不正确 " Name="opt_99" LabelWidth="150" />
                                                        <ext:Radio ID="opt_99_2" runat="server" BoxLabel="部分正确 " Name="opt_99" />
                                                        <ext:Radio ID="opt_99_3" runat="server" BoxLabel="完全正确 " Name="opt_99" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container100" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_100_1" runat="server" FieldLabel="正确固定导管" BoxLabel="不正确 " Name="opt_100" LabelWidth="150" />
                                                        <ext:Radio ID="opt_100_2" runat="server" BoxLabel="部分正确 " Name="opt_100" />
                                                        <ext:Radio ID="opt_100_3" runat="server" BoxLabel="完全正确 " Name="opt_100" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet15" runat="server" Flex="1" Title="五、透析液储藏方式" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container101" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_101_1" runat="server" FieldLabel="堆放整齐" BoxLabel="不正确 " Name="opt_101" LabelWidth="150" />
                                                        <ext:Radio ID="opt_101_2" runat="server" BoxLabel="部分正确 " Name="opt_101" />
                                                        <ext:Radio ID="opt_101_3" runat="server" BoxLabel="完全正确 " Name="opt_101" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container102" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_102_1" runat="server" FieldLabel="通风" BoxLabel="不正确 " Name="opt_102" LabelWidth="150" />
                                                        <ext:Radio ID="opt_102_2" runat="server" BoxLabel="部分正确 " Name="opt_102" />
                                                        <ext:Radio ID="opt_102_3" runat="server" BoxLabel="完全正确 " Name="opt_102" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container103" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_103_1" runat="server" FieldLabel="避免日晒" BoxLabel="不正确 " Name="opt_103" LabelWidth="150" />
                                                        <ext:Radio ID="opt_103_2" runat="server" BoxLabel="部分正确 " Name="opt_103" />
                                                        <ext:Radio ID="opt_103_3" runat="server" BoxLabel="完全正确 " Name="opt_103" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container104" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_104_1" runat="server" FieldLabel="避免潮湿" BoxLabel="不正确 " Name="opt_104" LabelWidth="150" />
                                                        <ext:Radio ID="opt_104_2" runat="server" BoxLabel="部分正确 " Name="opt_104" />
                                                        <ext:Radio ID="opt_104_3" runat="server" BoxLabel="完全正确 " Name="opt_104" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container105" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_105_1" runat="server" FieldLabel="蚂蚁、蟑螂、蜘蛛" BoxLabel="不正确 " Name="opt_105" LabelWidth="150" />
                                                        <ext:Radio ID="opt_105_2" runat="server" BoxLabel="部分正确 " Name="opt_105" />
                                                        <ext:Radio ID="opt_105_3" runat="server" BoxLabel="完全正确 " Name="opt_105" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet16" runat="server" Flex="1" Title="六、医嘱遵从性" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container106" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_106_1" runat="server" FieldLabel="口服药库存" BoxLabel="不正确 " Name="opt_106" LabelWidth="150" />
                                                        <ext:Radio ID="opt_106_2" runat="server" BoxLabel="部分正确 " Name="opt_106" />
                                                        <ext:Radio ID="opt_106_3" runat="server" BoxLabel="完全正确 " Name="opt_106" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container107" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_107_1" runat="server" FieldLabel="透析液库存量" BoxLabel="不正确 " Name="opt_107" LabelWidth="150" />
                                                        <ext:Radio ID="opt_107_2" runat="server" BoxLabel="部分正确 " Name="opt_107" />
                                                        <ext:Radio ID="opt_107_3" runat="server" BoxLabel="完全正确 " Name="opt_107" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container108" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_108_1" runat="server" FieldLabel="EPO储存方式" BoxLabel="不正确 " Name="opt_108" LabelWidth="150" />
                                                        <ext:Radio ID="opt_108_2" runat="server" BoxLabel="部分正确 " Name="opt_108" />
                                                        <ext:Radio ID="opt_108_3" runat="server" BoxLabel="完全正确 " Name="opt_108" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container109" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_109_1" runat="server" FieldLabel="磷结合剂的正确/按时使用" BoxLabel="不正确 "
                                                            Name="opt_109" LabelWidth="150" />
                                                        <ext:Radio ID="opt_109_2" runat="server" BoxLabel="部分正确 " Name="opt_109" />
                                                        <ext:Radio ID="opt_109_3" runat="server" BoxLabel="完全正确 " Name="opt_109" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container110" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_110_1" runat="server" FieldLabel="正确的换液次数" BoxLabel="不正确 " Name="opt_110" LabelWidth="150" />
                                                        <ext:Radio ID="opt_110_2" runat="server" BoxLabel="部分正确 " Name="opt_110" />
                                                        <ext:Radio ID="opt_110_3" runat="server" BoxLabel="完全正确 " Name="opt_110" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet21" runat="server" Flex="1" Title="一、居家环境卫生" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_111" runat="server" FieldLabel="评值结果" Width="400" />
                                                <ext:TextArea ID="are_112" runat="server" FieldLabel="护理措施及建议" Width="400" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet22" runat="server" Flex="1" Title="二、换液环境评估" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_113" runat="server" FieldLabel="评值结果" Width="400" />
                                                <ext:TextArea ID="are_114" runat="server" FieldLabel="护理措施及建议" Width="400" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet23" runat="server" Flex="1" Title="三、换液技术执行评估" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_115" runat="server" FieldLabel="(1)准备环境" Width="400" />
                                                <ext:TextField ID="txt_116" runat="server" FieldLabel="(2)换液技术" Width="400" />
                                                <ext:TextField ID="txt_117" runat="server" FieldLabel="(3)结束换液技术后评估" Width="400" />
                                                <ext:TextField ID="txt_118" runat="server" FieldLabel="(4)记录" Width="400" />
                                                <ext:TextArea ID="are_119" runat="server" FieldLabel="护理措施及建议" Width="400" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet24" runat="server" Flex="1" Title="三、导管出口处护理评估" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_120" runat="server" FieldLabel="评值结果" Width="400" />
                                                <ext:TextArea ID="are_121" runat="server" FieldLabel="护理措施及建议" Width="400" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet25" runat="server" Flex="1" Title="四、透析液储藏方式" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_122" runat="server" FieldLabel="评值结果" Width="400" />
                                                <ext:TextArea ID="are_123" runat="server" FieldLabel="护理措施及建议" Width="400" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet26" runat="server" Flex="1" Title="五、医嘱遵从性" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_124" runat="server" FieldLabel="评值结果" Width="400" />
                                                <ext:TextArea ID="are_125" runat="server" FieldLabel="护理措施及建议" Width="400" />
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
