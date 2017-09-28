<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_EvaForm.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_EvaForm" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化评估表</title>
    <style type="text/css">
	    .x-title-text
		{
            font-size: 26px;
		    color: Yellow;
            line-height:32px;
		}
		
        #Button1 .x-btn-inner, #Button2 .x-btn-inner, #Button3 .x-btn-inner, #Button4 .x-btn-inner, #Button5 .x-btn-inner, #Button6 .x-btn-inner, #Button7 .x-btn-inner
        {
            font-weight: bolder;
            font-size: 26px;
            line-height:32px;
        }
		
        #Button8 .x-btn-inner
        {
            font-weight: bolder;
            font-size: 26px;
            color:Lime;
            line-height:32px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="pid" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="info_date" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Aria" />
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="血液净化评估表" BodyStyle="background-color:#EBF5FF !important;">
            <Items>
                <ext:Container ID="Container8" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" Hidden="true" >
                    <Items>
                        <ext:Button ID="Button8" runat="server" Text="返回病患资料" Height="100" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Button8_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container01" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:Button ID="Button1" runat="server" Text="(1)首次血液透析护理评估措施记录单" Height="100" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Button1_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container2" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:Button ID="Button2" runat="server" Text="(2)血管通路动静脉内瘘物理检查评估表" Height="100" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Button2_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container3" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:Button ID="Button3" runat="server" Text="(3)动静脉内瘘闭塞高危因素评估表" Height="100" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Button3_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container4" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:Button ID="Button4" runat="server" Text="(4)血液透析患者皮肤瘙痒评估表(Sergio)" Height="100" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Button4_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container5" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:Button ID="Button5" runat="server" Text="(5)疼痛评分表" Height="100" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Button5_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container6" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:Button ID="Button6" runat="server" Text="(6)住院病人预防跌倒护理评估表" Height="100" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Button6_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container7" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout" >
                    <Items>
                        <ext:Button ID="Button7" runat="server" Text="(7)预防跌倒护理措施评估表" Height="100" Flex="1">
                            <DirectEvents>
                                <Click OnEvent="Button7_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Container>
            </Items>
        </ext:FormPanel>
    </div>
    </form>
</body>
</html>
