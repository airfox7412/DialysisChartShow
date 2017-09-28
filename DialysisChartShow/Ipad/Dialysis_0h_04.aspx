<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_04.aspx.cs" Inherits="Dialysis_Chart_Show.Ipad.Dialysis_0h_04" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血液透析患者皮肤瘙痒评估表(Sergio)" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="评估日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:TextField ID="txt_1" runat="server" FieldLabel="评估护士" IndicatorText="" />
                                        <ext:TextField ID="txt_2" runat="server" FieldLabel="总分" IndicatorText="" ReadOnly="true" />
                                        
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="一、皮肤瘙痒严重程度评分：(2次/日)" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label2" runat="server" Text="皮肤瘙痒严重程度评分" Cls="mylabel2" />
                                        
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_3" runat="server" FieldLabel="分值1" LabelAlign="Right" LabelWidth="35" IndicatorText="" ReadOnly="true"  />
                                                        <ext:TextField ID="txt_4" runat="server" FieldLabel="分值2" LabelAlign="Right" LabelWidth="245" IndicatorText="" ReadOnly="true"  />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_1" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="1.皮肤轻度痒感，无需搔抓(得1分)" Name="opt_5" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio5"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_6_1" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="1.皮肤轻度痒感，无需搔抓(得1分)" Name="opt_6" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio6"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="2.需要搔抓，但无破皮(得2分)" Name="opt_5" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio5"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_6_2" runat="server" BoxLabel="2.需要搔抓，但无破皮(得2分)" Name="opt_6" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio6"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_3" runat="server" BoxLabel="3.搔抓不能缓解(得3分)" Name="opt_5" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio5"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_6_3" runat="server" BoxLabel="3.搔抓不能缓解(得3分)" Name="opt_6" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio6"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                       <ext:Radio ID="opt_5_4" runat="server" BoxLabel="4.有破皮(得4分)" Name="opt_5" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio5"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_6_4" runat="server" BoxLabel="4.有破皮(得4分)" Name="opt_6" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio6"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                       <ext:Radio ID="opt_5_5" runat="server" BoxLabel="5.因抓痒坐立不安(得5分)" Name="opt_5" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio5"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_6_5" runat="server" BoxLabel="5.因抓痒坐立不安(得5分)" Name="opt_6" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio6"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                           </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="二、皮肤瘙痒分布范围评分：(2次/日)" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label1" runat="server" Text="皮肤瘙痒分布范围评分" Cls="mylabel2" />
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_7" runat="server" FieldLabel="分值1" LabelAlign="Right" LabelWidth="35" IndicatorText="" ReadOnly="true" />
                                                        <ext:TextField ID="txt_8" runat="server" FieldLabel="分值2" LabelAlign="Right" LabelWidth="245" IndicatorText="" ReadOnly="true"  />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_1" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="1.单个部位(得1分)" Name="opt_9" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio9"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_10_1" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="1.单个部位(得1分)" Name="opt_10" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio10"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_2" runat="server" BoxLabel="2.多个部位(得2分)" Name="opt_9" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio9"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_10_2" runat="server" BoxLabel="2.多个部位(得2分)" Name="opt_10" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio10"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_3" runat="server" BoxLabel="3.全身瘙痒(得3分)" Name="opt_9" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio9"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_10_3" runat="server" BoxLabel="3.全身瘙痒(得3分)" Name="opt_10" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio10"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="三、皮肤瘙痒发作频率评分：(2次/日)" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label3" runat="server" Text="瘙痒发作频率评分" Cls="mylabel2" />
                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:TextField ID="txt_11" runat="server" FieldLabel="分值1" LabelAlign="Right" LabelWidth="35" IndicatorText="" ReadOnly="true" />
                                                        <ext:TextField ID="txt_12" runat="server" FieldLabel="分值2" LabelAlign="Right" LabelWidth="245"  IndicatorText=""  ReadOnly="true"/>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_1" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="1.每短时发作4次(每次<10分钟)或每长时间发作1次(>10分钟)(得1分)" Name="opt_13" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio13"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_14_1" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="1.每短时发作4次(每次<10分钟)或每长时间发作1次(>10分钟)(得1分)" Name="opt_14" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio14"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="2.每短时发作5次(每次<10分钟)或每长时间发作2次(>10分钟)(得2分)" Name="opt_13" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio13"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_14_2" runat="server" BoxLabel="2.每短时发作5次(每次<10分钟)或每长时间发作2次(>10分钟)(得2分)" Name="opt_14" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio14"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container14" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_3" runat="server" BoxLabel="3.每短时发作6次(每次<10分钟)或每长时间发作3次(>10分钟)(得3分)" Name="opt_13" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio13"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_14_3" runat="server" BoxLabel="3.每短时发作6次(每次<10分钟)或每长时间发作3次(>10分钟)(得3分)" Name="opt_14" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio14"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container15" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_4" runat="server" BoxLabel="4.每短时发作7次(每次<10分钟)或每长时间发作4次(>10分钟)(得4分)" Name="opt_13" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio13"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_14_4" runat="server" BoxLabel="4.每短时发作7次(每次<10分钟)或每长时间发作4次(>10分钟)(得4分)" Name="opt_14" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio14"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container16" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_5" runat="server" BoxLabel="5.每短时发作8次(每次<10分钟)或每长时间发作5次(>10分钟)(得5分)" Name="opt_13" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio13"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_14_5" runat="server" BoxLabel="5.每短时发作8次(每次<10分钟)或每长时间发作5次(>10分钟)(得5分)" Name="opt_14" Width="400" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio14"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="四、皮肤瘙痒至夜间睡眠障碍评分：" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label4" runat="server" Text="皮肤瘙痒至夜间睡眠障碍评分" Cls="mylabel2" />
                                                        <ext:TextField ID="txt_15" runat="server" FieldLabel="分值" LabelAlign="Right" LabelWidth="35" IndicatorText="" ReadOnly="true" />
                                                <ext:Container ID="Container17" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_1" runat="server" FieldLabel="" LabelAlign="Right" BoxLabel="1.因皮肤瘙痒而觉醒1次(得2分)" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio16"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container18" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_2" runat="server" BoxLabel="2.因皮肤瘙痒而觉醒2次(得4分)" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio16"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container19" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_3" runat="server" BoxLabel="3.因皮肤瘙痒而觉醒3次(得6分)" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio16"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container20" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_4" runat="server" BoxLabel="4.因皮肤瘙痒而觉醒4次(得8分)" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio16"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container21" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_5" runat="server" BoxLabel="5.因皮肤瘙痒而觉醒5次(得10分)" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio16"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container22" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_6" runat="server" BoxLabel="6.因皮肤瘙痒而觉醒6次(得12分)" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio16"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container23" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_16_7" runat="server" BoxLabel="7.因皮肤瘙痒而觉醒7次(得14分)" Name="opt_16" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio16"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
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
