<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_System_07.aspx.cs" Inherits="Dialysis_Chart_Show.Systems.Dialysis_System_07" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>KeyProMag</title>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager runat="server" Theme="Triton" />
        <ext:Window ID="window1" runat="server" Width="320" Height="200" X="100">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server">
                    <Items>
                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="Keyid" runat="server" FieldLabel="key of Id" LabelAlign="Right" PaddingSpec="10 10 10 10" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="Bedno" runat="server" FieldLabel="Number of licenses" LabelAlign="Right" PaddingSpec="10 10 10 10" />
                            </Items>
                        </ext:Container>
                    </Items>
                    <Buttons>
                        <ext:Button ID="BtnOK" runat="server" Text="Ok">
                            <DirectEvents>
                                <Click OnEvent="BtnOK_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
        <ext:Window ID="window2" runat="server" Width="320" Height="200" X="500" Title="Read Key">
            <Items>
                <ext:FormPanel ID="FormPanel2" runat="server">
                    <Items>
                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="New_Keyid" runat="server" FieldLabel="key of Id" LabelAlign="Right" PaddingSpec="10 10 10 10" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container4" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:TextField ID="New_Bedno" runat="server" FieldLabel="Number of licenses" LabelAlign="Right" PaddingSpec="10 10 10 10" />
                            </Items>
                        </ext:Container>
                    </Items>
                    <Buttons>
                        <ext:Button ID="Button2" runat="server" Text="WtoS">
                            <DirectEvents>
                                <Click OnEvent="BtnWtoS_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button1" runat="server" Text="Read Again">
                            <DirectEvents>
                                <Click OnEvent="BtnRead_Click" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="BtnNewKey" runat="server" Text="Ok">
                            <DirectEvents>
                                <Click OnEvent="BtnNewKey_Click" />
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
            </Items>
        </ext:Window>
    </form>
</body>
</html>
