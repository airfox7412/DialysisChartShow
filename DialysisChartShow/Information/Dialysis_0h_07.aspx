<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_07.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_0h_07" %>

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
        <ext:ResourceManager ID="ResourceManager2" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="预防跌倒护理措施评估表" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">

							            <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="评估日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                        <ext:TextField ID="txt_1" runat="server" FieldLabel="评估护士" IndicatorText="" />
                                        <ext:TextField ID="section" runat="server" FieldLabel="病区" IndicatorText="" />
                                        <ext:TextField ID="bed_no" runat="server" FieldLabel="床号" IndicatorText="" />
					
					                    <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="1.提供安全环境:" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label2" runat="server" Text="(1)提供足够灯光" Cls="mylabel2" />												
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_2_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_2" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_2_2" runat="server" BoxLabel="否" Name="opt_2" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														
														<ext:Radio ID="opt_2_3" runat="server" BoxLabel="不需" Name="opt_2" >
                                                        <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
												
												<ext:Label ID="Label3" runat="server" Text="(2)保持病房地面清洁干燥" Cls="mylabel2" />
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_3_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_3" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="否" Name="opt_3" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_3_3" runat="server" BoxLabel="不需" Name="opt_3" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>

                                                <ext:Label ID="Label4" runat="server" Text="(3)病房床旁边走道障碍清除" Cls="mylabel2" />
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_4_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_4" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_4_2" runat="server" BoxLabel="否" Name="opt_4" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_4_3" runat="server" BoxLabel="不需" Name="opt_4" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
			
										    </Items>
										</ext:FieldSet>
					
					
					
					
					
					                    <ext:FieldSet ID="FieldSet5" runat="server" Flex="1" Title="2.预防病人跌倒:" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label5" runat="server" Text="(1)监测病人步态是否平稳" Cls="mylabel2" />												
                                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_5_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_5" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="否" Name="opt_5" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														
														<ext:Radio ID="opt_5_3" runat="server" BoxLabel="不需" Name="opt_5" >
                                                        <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
												
												<ext:Label ID="Label6" runat="server" Text="(2)帮助病人使用约束带" Cls="mylabel2" />
                                                <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_6_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_6" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_6_2" runat="server" BoxLabel="否" Name="opt_6" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_6_3" runat="server" BoxLabel="不需" Name="opt_6" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>

                                                <ext:Label ID="Label7" runat="server" Text="(3)指导呼叫铃的使用" Cls="mylabel2" />
                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_7_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_7" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_7_2" runat="server" BoxLabel="否" Name="opt_7" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_7_3" runat="server" BoxLabel="不需" Name="opt_7" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
												<ext:Label ID="Label8" runat="server" Text="(4)指导病人采去渐进下床方式" Cls="mylabel2" />
                                                <ext:Container ID="Container8" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_8_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_8" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_8_2" runat="server" BoxLabel="否" Name="opt_8" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_8_3" runat="server" BoxLabel="不需" Name="opt_8" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
												<ext:Label ID="Label9" runat="server" Text="(5)提醒病人家属陪伴在旁，若暂时离开病房时需告知责任护士" Cls="mylabel2" />
                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_9_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_9" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_9_2" runat="server" BoxLabel="否" Name="opt_9" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_9_3" runat="server" BoxLabel="不需" Name="opt_9" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
												
												
												
			
										    </Items>
										</ext:FieldSet>
					
					
					                 		<ext:FieldSet ID="FieldSet10" runat="server" Flex="1" Title="3.宣教病人及家属:" Layout="AnchorLayout" Collapsible="true" Collapsed="false">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:Label ID="Label10" runat="server" Text="(1)应注意轮椅及便盆坐椅的固定" Cls="mylabel2" />												
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_10_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_10" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_10_2" runat="server" BoxLabel="否" Name="opt_10" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														
														<ext:Radio ID="opt_10_3" runat="server" BoxLabel="不需" Name="opt_10" >
                                                        <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
												
												<ext:Label ID="Label11" runat="server" Text="2)将物品放置在便于病人拿取处" Cls="mylabel2" />
                                                <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_11_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_11" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_11_2" runat="server" BoxLabel="否" Name="opt_11" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_11_3" runat="server" BoxLabel="不需" Name="opt_11" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>

                                                <ext:Label ID="Label12" runat="server" Text="3)指导床上使用便盆或尿壶的方法" Cls="mylabel2" />
                                                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_12_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_12" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_12_2" runat="server" BoxLabel="否" Name="opt_12" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_12_3" runat="server" BoxLabel="不需" Name="opt_12" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>
												<ext:Label ID="Label13" runat="server" Text="(4)提供病人呼叫及寻救协助的方法" Cls="mylabel2" />
                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" Padding="2">
                                                    <Items>
                                                        <ext:Radio ID="opt_13_1" runat="server" FieldLabel="(填是、否、不需)" LabelAlign="Left" BoxLabel="是" Name="opt_13" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio0"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                        <ext:Radio ID="opt_13_2" runat="server" BoxLabel="否" Name="opt_13" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
														<ext:Radio ID="opt_13_3" runat="server" BoxLabel="不需" Name="opt_13" >
                                                            <DirectEvents>
                                                                <Change OnEvent="_CountRadio1"></Change>
                                                            </DirectEvents>
                                                        </ext:Radio>
                                                    </Items>
                                                </ext:Container>

												
												
												
			
										    </Items>
										</ext:FieldSet>
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					
					                    <ext:TextField ID="txt_37" runat="server" FieldLabel="总分" IndicatorText="" ReadOnly="true" Cls="mylabel1" hidden="true" />
							
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
