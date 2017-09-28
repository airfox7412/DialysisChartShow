<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_08.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_08" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd" >

<html xmlns="http://www.w3.org/1999/xhtml" >
<head runat="server" >
    <title></title>
</head>
<body>
    <form id="form1" runat="server" >
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server" >
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit" >
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="基本信息" AutoScroll="true" ButtonAlign="Center" >
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" >
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false" >
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="资料类型:血液透析" Hidden="true" />
                                        
                                        <%--血液净化病历--%>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Title="血液净化首次病历" >
                                            <Items>
                                                <ext:Container ID="Container11" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_f_01" runat="server" Text="基本资料" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="F01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container12" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_f_02" runat="server" Text="病史" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="F02" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container13" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_f_03" runat="server" Text="体格检查" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="F03" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container14" runat="server" >
                                                    <Items>
<%--                                                        <ext:Button ID="zinfo_f_04" runat="server" Text="辅助检查" Icon="BulletBlue" Visible="false" >--%>
                                                        <ext:Button ID="zinfo_f_04" runat="server" Text="实验室检查" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="F04" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container15" runat="server" >
                                                    <Items>
<%--                                                        <ext:Button ID="zinfo_f_05" runat="server" Text="诊断" Icon="BulletBlue" Visible="false" >--%>
                                                        <ext:Button ID="zinfo_f_05" runat="server" Text="入院诊断" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="F05" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
												<%--<ext:Container ID="Container16" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_f_06" runat="server" Text="打印" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="F06" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>--%>
                                            </Items>
                                        </ext:FieldSet>
                                        <%--血液净化过程--%>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Title="血液净化过程" >
                                            <Items>
                                                <ext:Container ID="Container21" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="Dialysis_09_01" runat="server" Text="血液净化记录" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="K01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container22" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="Dialysis_09_02" runat="server" Text="净化过程明细" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="K02" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container23" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="Dialysis_09_03" runat="server" Text="血液净化记录表" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="K03" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <%--诊断信息--%>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Title="诊断信息" >
                                            <Items>
                                                <ext:Container ID="Container31" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_a_01" runat="server" Text="原发病诊断" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="A01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container32" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_a_02" runat="server" Text="病理诊断" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="A02" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container33" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_a_03" runat="server" Text="并发症诊断" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="A03" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container34" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_a_04" runat="server" Text="传染病诊断" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="A04" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container35" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_a_05" runat="server" Text="肿瘤诊断" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="A05" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container36" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_a_06" runat="server" Text="过敏诊断" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="A06" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container37" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_a_07" runat="server" Text="转归情况" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="A07" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
												<ext:Container ID="Container38" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_a_08" runat="server" Text="新诊断讯息" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="A08" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <%--血透信息--%>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Title="血透信息" >
                                            <Items>
                                                <ext:Container ID="Container41" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_b_01" runat="server" Text="血管通路" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="B01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container42" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_b_02" runat="server" Text="透析处方" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="B02" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container43" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_b_03" runat="server" Text="血压测量" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="B03" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container44" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_b_04" runat="server" Text="透析充分性" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="B04" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container45" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_b_05" runat="server" Text="抗凝剂" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="B05" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container46" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_b_06" runat="server" Text="干体重" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="B06" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container47" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_b_07" runat="server" Text="合用其它透析模式" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="B07" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <%--治疗信息--%>
                                        <ext:FieldSet ID="FieldSet5" runat="server" Title="治疗信息" >
                                            <Items>
                                                <ext:Container ID="Container59" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="drug_list" runat="server" Text="医嘱用药" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="C09" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>

                                                <ext:Container ID="Container51" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_c_01" runat="server" Text="促红素" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="C01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container52" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_c_02" runat="server" Text="铁剂" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="C02" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container53" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_c_03" runat="server" Text="抗高血压药" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="C03" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container54" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_c_04" runat="server" Text="活性维生素D" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="C04" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container55" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_c_05" runat="server" Text="钙剂" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="C05" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container56" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_c_06" runat="server" Text="降磷药物" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="C06" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container57" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_c_07" runat="server" Text="其它药物治疗" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="C07" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <%--实验室及辅助检查--%>
                                        <ext:FieldSet ID="FieldSet6" runat="server" Title="实验室及辅助检查" >
                                            <Items>
                                                <ext:Container ID="Container61" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_d_01" runat="server" Text="实验室检查" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="D01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container62" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_d_02" runat="server" Text="辅助检查" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="D02" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <%--病程记录--%>
                                        <ext:FieldSet ID="FieldSet7" runat="server" Title="病程记录" >
                                            <Items>
                                                <ext:Container ID="Container71" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_e_01" runat="server" Text="常规记录" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="E01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container72" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_e_02" runat="server" Text="特殊记录" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="E02" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <%--特殊资料--%>
                                        <%--<ext:FieldSet ID="FieldSet9" runat="server" Title="特殊资料" >
                                            <Items>
                                                <ext:Container ID="Container91" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_g_01" runat="server" Text="特殊资料建档" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="G01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>--%>
                                                
                                        <%-- 2014-04-11 ada add begin--%>
                                        <%--血透评估表--%>
                                        <ext:FieldSet ID="FieldSet8" runat="server" Title="血透评估表" >
                                            <Items>
                                                <ext:Container ID="Container81" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_h_01" runat="server" Text="1首次血液透析护理评估措施记录单" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="h01" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container82" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_h_02" runat="server" Text="2血管通路动静脉内瘘物理检查评估表" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="h02" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container83" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_h_03" runat="server" Text="3动静脉内瘘闭塞高危因素评估表" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="h03" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container84" runat="server" >
                                                    <Items>
                                                        <ext:Button ID="zinfo_h_04" runat="server" Text="4血液透析患者皮肤瘙痒评估表(Sergio)" Icon="BulletBlue" Visible="false" >
                                                            <DirectEvents>
                                                                <Click OnEvent="h04" />
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <%-- 2014-04-11 ada add end --%>

                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
        <ext:Window 
            ID="Window1"
            runat="server"
            Title=""
            Width="1000"
            Height="520"
            Y="0"
            Modal="true"
            AutoRender="false"
            Collapsible="true"
            Maximizable="true"
            Hidden="true" >
            <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                            <LoadMask ShowMask="true" />
                        </Loader>
        </ext:Window>
    </div>
    </form>
</body>
</html>
