<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_05_041.aspx.cs" Inherits="Dialysis_Chart_Show.Dialysis_05_041" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8,Auto-Rotate=Disable" />
    <title></title>
    <style type="text/css">
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="sid" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" MonitorResize="true" Title="查房记录(苏州医院)" BodyStyle="background-color:#EBF5FF !important;" >
            <Items>
                <ext:Container ID="Container22" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextField ID="TextField1" runat="server" FieldLabel="查房人员" LabelWidth="200" ColumnWidth=".30" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue" Flex="1">
                            <DirectEvents>
                                <Focus OnEvent="text_click" />
                            </DirectEvents>
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue'); this.addCls('Text-black');" Single="true" />
                            </Listeners>
                        </ext:TextField>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container25" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items>
                        <ext:TextArea ID="TextArea1" runat="server" FieldLabel="查房记录" ColumnWidth=".8" LabelAlign="Right" PaddingSpec="10 50 0 2" Cls="Text-blue-H" LabelWidth="200" Height="200" Flex="1">
                            <Listeners>
                                <Change Handler="this.removeCls('Text-blue-H'); this.addCls('Text-black-H');" Single="true" />
                            </Listeners>
                        </ext:TextArea>
                    </Items> 
                </ext:Container>
                <ext:Container ID="Container26" runat="server" AnchorHorizontal="100%" Layout="HBoxLayout">
                    <Items> 
                        <ext:ImageButton ID="ImageButton1" runat="server" ImageUrl="~/Styles/save.png" Height="50" ColumnWidth="1" OverImageUrl="~/Styles/saveover.png" >
                            <DirectEvents>
                                <Click OnEvent="Btn_save_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items> 
                </ext:Container>
            </Items>
        </ext:FormPanel>
        <ext:Window ID="Window1" runat="server" Title="请输入工号" Height="160" Closable="false" 
            Width="350" BodyStyle="background-color: #fff;" BodyPadding="5" Modal="true" Hidden="true" ButtonAlign="Center">
            <Items>
                <ext:TextField ID="TextField_UserID" runat="server" FieldLabel="工号" ColumnWidth="1" LabelAlign="Right" Padding="5" />
            </Items>
            <Buttons>
                <ext:Button ID="Button4" runat="server" Icon="Accept" Text="确认" Width="150" Height="40">
                    <DirectEvents>
                        <Click OnEvent="btnDecrypt_Click" />
                    </DirectEvents>
                </ext:Button>
                <ext:Button ID="Button5" runat="server" Icon="Cancel" Text="取消" Width="150" Height="40">
                    <DirectEvents>
                        <Click OnEvent="btnClose_Click" />
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:Window>
    </div>
    </form>
</body>
</html>