<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_25.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_09_25" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="护理评估" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="护理评估日期" Format="yyyy-MM-dd">
                                        </ext:DateField>

<%--                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="一、一般资料" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_1" runat="server" FieldLabel="姓名" />
                                                <ext:TextField ID="txt_2" runat="server" FieldLabel="年龄" />
                                                <ext:TextField ID="txt_3" runat="server" FieldLabel="身高" />
                                                <ext:TextField ID="txt_4" runat="server" FieldLabel="体重" />
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="身份" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="职业" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="职位" />
                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_8_1" runat="server" FieldLabel="经济状况" BoxLabel="上 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_2" runat="server" BoxLabel="中 " Name="opt_8" />
                                                        <ext:Radio ID="opt_8_3" runat="server" BoxLabel="下 " Name="opt_8" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_9" runat="server" FieldLabel="经济决定权" />
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_10_1" runat="server" FieldLabel="教育程度" BoxLabel="不识字 " Name="opt_10" />
                                                        <ext:Radio ID="opt_10_2" runat="server" BoxLabel="小学 " Name="opt_10" />
                                                        <ext:Radio ID="opt_10_3" runat="server" BoxLabel="初中 " Name="opt_10" />
                                                        <ext:Radio ID="opt_10_4" runat="server" BoxLabel="高中 " Name="opt_10" />
                                                        <ext:Radio ID="opt_10_5" runat="server" BoxLabel="大专以上 " Name="opt_10" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_11_1" runat="server" FieldLabel="宗教信仰" BoxLabel="无 " Name="opt_11" />
                                                        <ext:Radio ID="opt_11_2" runat="server" BoxLabel="佛教 " Name="opt_11" />
                                                        <ext:Radio ID="opt_11_3" runat="server" BoxLabel="道教 " Name="opt_11" />
                                                        <ext:Radio ID="opt_11_4" runat="server" BoxLabel="回教 " Name="opt_11" />
                                                        <ext:Radio ID="opt_11_5" runat="server" BoxLabel="天主教 " Name="opt_11" />
                                                        <ext:Radio ID="opt_11_6" runat="server" BoxLabel="其他 " Name="opt_11" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="其他" />
                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_1" runat="server" FieldLabel="惯用语言" BoxLabel="国语 " Name="opt_13" />
                                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="闽南语 " Name="opt_13" />
                                                        <ext:Radio ID="opt_13_3" runat="server" BoxLabel="客家语 " Name="opt_13" />
                                                        <ext:Radio ID="opt_13_4" runat="server" BoxLabel="其他 " Name="opt_13" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_14" runat="server" FieldLabel="其他" />
                                            </Items>
                                        </ext:FieldSet>--%>

                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="一、家庭状况" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>

<%--                                                <ext:Container ID="Container21" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_15_1" runat="server" FieldLabel="1.家庭图谱 " BoxLabel="男 " Name="opt_15" />
                                                        <ext:Radio ID="opt_15_2" runat="server" BoxLabel="男本人 " Name="opt_15" />
                                                        <ext:Radio ID="opt_15_3" runat="server" BoxLabel="男死亡 " Name="opt_15" />
                                                        <ext:Radio ID="opt_15_4" runat="server" BoxLabel="女 " Name="opt_15" />
                                                        <ext:Radio ID="opt_15_5" runat="server" BoxLabel="女本人 " Name="opt_15" />
                                                        <ext:Radio ID="opt_15_6" runat="server" BoxLabel="女死亡 " Name="opt_15" />
                                                    </Items>
                                                </ext:Container>--%>

                                                <ext:TextField ID="txt_16" runat="server" FieldLabel="目前同住者" />
                                                <ext:TextField ID="txt_17" runat="server" FieldLabel="主要陪伴者" />
                                                <ext:TextField ID="txt_18" runat="server" FieldLabel="在家烹调者" />

<%--                                                <ext:Container ID="Container22" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_19_1" runat="server" FieldLabel="5.家庭计划" BoxLabel="有 " Name="opt_19" />
                                                        <ext:Radio ID="opt_19_2" runat="server" BoxLabel="无 " Name="opt_19" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container23" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_20_1" runat="server" FieldLabel="家庭计划" BoxLabel="IUD " Name="opt_20" />
                                                        <ext:Radio ID="opt_20_2" runat="server" BoxLabel="结扎 " Name="opt_20" />
                                                        <ext:Radio ID="opt_20_3" runat="server" BoxLabel="其他 " Name="opt_20" />
                                                        <ext:TextField ID="txt_21" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>--%>

                                                <ext:Container ID="Container24" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label24" runat="server" Text="家庭病史家庭成员中是否有下列疾病" Cls="mylabel1" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Checkbox ID="chk_22_1" runat="server" BoxLabel="心脏病" />
                                                <ext:Checkbox ID="chk_22_2" runat="server" BoxLabel="高血压" />
                                                <ext:Checkbox ID="chk_22_3" runat="server" BoxLabel="糖尿病" />
                                                <ext:Checkbox ID="chk_22_4" runat="server" BoxLabel="肺结核" />
                                                <ext:Checkbox ID="chk_22_5" runat="server" BoxLabel="癌症" />
                                                <ext:Checkbox ID="chk_22_6" runat="server" BoxLabel="气喘" />
                                                <ext:Checkbox ID="chk_22_7" runat="server" BoxLabel="癫痫" />
                                                <ext:Checkbox ID="chk_22_8" runat="server" BoxLabel="精神分裂" />
                                                <ext:Checkbox ID="chk_22_9" runat="server" BoxLabel="过敏" />
                                                <ext:Checkbox ID="chk_22_10" runat="server" BoxLabel="酗酒" />
                                                <ext:Checkbox ID="chk_22_11" runat="server" BoxLabel="肾脏病" />
                                                <ext:Checkbox ID="chk_22_12" runat="server" BoxLabel="肝脏病" />
                                                <ext:Checkbox ID="chk_22_13" runat="server" BoxLabel="消化性溃疡" />
                                                <ext:Checkbox ID="chk_22_14" runat="server" BoxLabel="其他" />
                                                <ext:TextField ID="txt_23" runat="server" FieldLabel="其他" />
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="二、过去病史" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container31" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_24_1" runat="server" FieldLabel="1.住院次数" BoxLabel="无 " Name="opt_24" />
                                                        <ext:Radio ID="opt_24_2" runat="server" BoxLabel="有" Name="opt_24" />
                                                        <ext:TextField ID="txt_25" runat="server" FieldLabel="" IndicatorText="次，原因" />
                                                        <ext:TextField ID="txt_26" runat="server" FieldLabel="" IndicatorText="，住院地点：" />
                                                        <ext:TextField ID="txt_27" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container32" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_28" runat="server" FieldLabel="2.内科病史" IndicatorText="无" />
                                                        <ext:TextField ID="txt_29" runat="server" FieldLabel="" IndicatorText="不详" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container33" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_30" runat="server" FieldLabel="高血压 发病时间" IndicatorText="年，服药：" />
                                                        <ext:Radio ID="opt_31_1" runat="server" FieldLabel="" BoxLabel="无 " Name="opt_31" />
                                                        <ext:Radio ID="opt_31_2" runat="server" BoxLabel="规则  " Name="opt_31" />
                                                        <ext:Radio ID="opt_31_3" runat="server" BoxLabel="不规则：" Name="opt_31" />
                                                        <ext:TextField ID="txt_32" runat="server" FieldLabel="" IndicatorText="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container34" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_33" runat="server" FieldLabel="心脏病 发病时间" IndicatorText="年，服药：" />
                                                        <ext:Radio ID="opt_34_1" runat="server" FieldLabel="" BoxLabel="无 " Name="opt_34" />
                                                        <ext:Radio ID="opt_34_2" runat="server" BoxLabel="规则  " Name="opt_34" />
                                                        <ext:Radio ID="opt_34_3" runat="server" BoxLabel="不规则：" Name="opt_34" />
                                                        <ext:TextField ID="txt_35" runat="server" FieldLabel="" IndicatorText="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container35" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_36" runat="server" FieldLabel="糖尿病 发病时间" IndicatorText="年，服药：" />
                                                        <ext:Radio ID="opt_37_1" runat="server" FieldLabel="" BoxLabel="无 " Name="opt_37" />
                                                        <ext:Radio ID="opt_37_2" runat="server" BoxLabel="规则  " Name="opt_37" />
                                                        <ext:Radio ID="opt_37_3" runat="server" BoxLabel="不规则：" Name="opt_37" />
                                                        <ext:TextField ID="txt_38" runat="server" FieldLabel="" IndicatorText="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container36" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_39" runat="server" FieldLabel="气喘病 发病时间" IndicatorText="年，服药：" />
                                                        <ext:Radio ID="opt_40_1" runat="server" FieldLabel="" BoxLabel="无 " Name="opt_40" />
                                                        <ext:Radio ID="opt_40_2" runat="server" BoxLabel="规则  " Name="opt_40" />
                                                        <ext:Radio ID="opt_40_3" runat="server" BoxLabel="不规则：" Name="opt_40" />
                                                        <ext:TextField ID="txt_41" runat="server" FieldLabel="" IndicatorText="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:TextField ID="txt_42" runat="server" FieldLabel="其他" />
                                                <ext:Container ID="Container37" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_43_1" runat="server" FieldLabel="3.外科病史" BoxLabel="无 " Name="opt_43" />
                                                        <ext:Radio ID="opt_43_2" runat="server" BoxLabel="外伤，手术或外科疾病发生时间，处理情况及目前状况：" Name="opt_43" />
                                                        <ext:TextField ID="txt_44" runat="server" FieldLabel="" IndicatorText="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container38" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_45_1" runat="server" FieldLabel="4.过敏史：药物" BoxLabel="无 " Name="opt_45" />
                                                        <ext:Radio ID="opt_45_2" runat="server" BoxLabel="有" Name="opt_45" />
                                                        <ext:TextField ID="txt_46" runat="server" FieldLabel="" IndicatorText="，食物：" />
                                                        <ext:Radio ID="opt_47_1" runat="server" FieldLabel="" BoxLabel="无 " Name="opt_47" />
                                                        <ext:Radio ID="opt_47_2" runat="server" BoxLabel="有" Name="opt_47" />
                                                        <ext:TextField ID="txt_48" runat="server" FieldLabel="" IndicatorText="，其他：" />
                                                        <ext:TextField ID="txt_49" runat="server" FieldLabel="" IndicatorText="" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="三、日常生活" Layout="AnchorLayout"
                                            Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Container ID="Container41" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Labe41" runat="server" Text="1.饮食： (1)饮食种类：" Cls="mylabel1" />
                                                        <ext:Checkbox ID="chk_50_1" runat="server" BoxLabel="普通" />
                                                        <ext:Checkbox ID="chk_50_2" runat="server" BoxLabel="软质" />
                                                        <ext:Checkbox ID="chk_50_3" runat="server" BoxLabel="流质" />
                                                        <ext:Checkbox ID="chk_50_4" runat="server" BoxLabel="素食" />
                                                        <ext:Checkbox ID="chk_50_5" runat="server" BoxLabel="早素" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container42" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label1" runat="server" Text="(2)饮食偏好：" Cls="mylabel1" />
                                                        <ext:Checkbox ID="chk_51_1" runat="server" BoxLabel="无" />
                                                        <ext:Checkbox ID="chk_51_2" runat="server" BoxLabel="肉类" />
                                                        <ext:Checkbox ID="chk_51_3" runat="server" BoxLabel="蔬菜" />
                                                        <ext:Checkbox ID="chk_51_4" runat="server" BoxLabel="海鲜" />
                                                        <ext:Checkbox ID="chk_51_5" runat="server" BoxLabel="蛋奶类" />
                                                        <ext:Checkbox ID="chk_51_6" runat="server" BoxLabel="甜食" />
                                                        <ext:Checkbox ID="chk_51_7" runat="server" BoxLabel="咸食" />
                                                        <ext:Checkbox ID="chk_51_8" runat="server" BoxLabel="辛辣" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container43" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label43" runat="server" Text="(3)忌讳食物：" Cls="mylabel1" />
                                                        <ext:Radio ID="opt_52_1" runat="server" FieldLabel="" BoxLabel="无 " Name="opt_52" />
                                                        <ext:Radio ID="opt_52_2" runat="server" BoxLabel="有：" Name="opt_52" />
                                                        <ext:TextField ID="txt_53" runat="server" FieldLabel="" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container44" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_54" runat="server" FieldLabel="(4)餐数" IndicatorText="次/天" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container45" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label45" runat="server" Text="(5)营养知识：了解程度：" Cls="mylabel1" />
                                                        <ext:Radio ID="opt_55_1" runat="server" FieldLabel="" BoxLabel="了解 " Name="opt_55" />
                                                        <ext:Radio ID="opt_55_2" runat="server" BoxLabel="部分了解 " Name="opt_55" />
                                                        <ext:Radio ID="opt_55_3" runat="server" BoxLabel="完全不了解 " Name="opt_55" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container46" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label46" runat="server" Text="营养知识：配合程度：" Cls="mylabel1" />
                                                        <ext:Radio ID="opt_56_1" runat="server" FieldLabel="" BoxLabel="配合 " Name="opt_56" />
                                                        <ext:Radio ID="opt_56_2" runat="server" BoxLabel="部分配合 " Name="opt_56" />
                                                        <ext:Radio ID="opt_56_3" runat="server" BoxLabel="完全不配合 " Name="opt_56" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container47" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label47" runat="server" Text="2.睡眠：睡眠习惯" Cls="mylabel1" />
                                                        <ext:TextField ID="txt_57" runat="server" FieldLabel="" IndicatorText="小时/天" />
                                                        <ext:Checkbox ID="chk_58_1" runat="server" BoxLabel="正常" />
                                                        <ext:Checkbox ID="chk_58_2" runat="server" BoxLabel="不易入睡" />
                                                        <ext:Checkbox ID="chk_58_3" runat="server" BoxLabel="继续睡眠" />
                                                        <ext:Checkbox ID="chk_58_4" runat="server" BoxLabel="日夜颠倒" />
                                                        <ext:Checkbox ID="chk_58_5" runat="server" BoxLabel="使用药物" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container48" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label48" runat="server" Text="3.排泄：(1)解尿型态：尿量" Cls="mylabel1" />
                                                        <ext:Radio ID="opt_59_1" runat="server" FieldLabel="" BoxLabel="无 " Name="opt_59" />
                                                        <ext:Radio ID="opt_59_2" runat="server" BoxLabel="有  约" Name="opt_59" />
                                                        <ext:TextField ID="txt_60" runat="server" FieldLabel="" IndicatorText="CC/天 颜色：" />
                                                        <ext:TextField ID="txt_61" runat="server" FieldLabel="" />
                                                        <ext:Checkbox ID="chk_62_1" runat="server" BoxLabel="尿失禁" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container49" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label49" runat="server" Text="(2)排便型态：次数" Cls="mylabel1" />
                                                        <ext:TextField ID="txt_63" runat="server" FieldLabel="" IndicatorText="次/天 " />
                                                        <ext:Checkbox ID="chk_62_2" runat="server" BoxLabel="自解" />
                                                        <ext:Checkbox ID="chk_62_3" runat="server" BoxLabel="依赖药物" />
                                                        <ext:Checkbox ID="chk_62_4" runat="server" BoxLabel="疼痛" />
                                                        <ext:Checkbox ID="chk_62_5" runat="server" BoxLabel="痔疮" />
                                                        <ext:Checkbox ID="chk_62_6" runat="server" BoxLabel="排便失禁" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container50" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Label ID="Label50" runat="server" Text="4.日常生活：(1)" Cls="mylabel1" />
                                                        <ext:Checkbox ID="chk_64_1" runat="server" BoxLabel="可自行活动    (2)" />
                                                        <ext:Checkbox ID="chk_64_2" runat="server" BoxLabel="需协助：" />
                                                        <ext:Checkbox ID="chk_64_3" runat="server" BoxLabel="行动" />
                                                        <ext:Checkbox ID="chk_64_4" runat="server" BoxLabel="进食" />
                                                        <ext:Checkbox ID="chk_64_5" runat="server" BoxLabel="更衣" />
                                                        <ext:Checkbox ID="chk_64_6" runat="server" BoxLabel="如厕" />
                                                        <ext:Checkbox ID="chk_64_7" runat="server" BoxLabel="沐浴    (3)" />
                                                        <ext:Checkbox ID="chk_64_8" runat="server" BoxLabel="完全依赖：" />
                                                        <ext:Checkbox ID="chk_64_9" runat="server" BoxLabel="行动" />
                                                        <ext:Checkbox ID="chk_64_10" runat="server" BoxLabel="进食" />
                                                        <ext:Checkbox ID="chk_64_11" runat="server" BoxLabel="更衣" />
                                                        <ext:Checkbox ID="chk_64_12" runat="server" BoxLabel="如厕" />
                                                        <ext:Checkbox ID="chk_64_13" runat="server" BoxLabel="沐浴" />
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
