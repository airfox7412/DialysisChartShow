<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_00_Add.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_00_Add" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type = "text/css">
    .mylabel
    {
         color:Blue;
    }
    .mylabel1
    {
         font-weight:bold;  
         color:Black;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="新增交班纪录" AutoScroll="true" ButtonAlign="Center" Frame="true" Padding="10">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel1" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:TextField ID="info_user" runat="server" FieldLabel="交办人" IndicatorText="*" />
                                        <ext:TextField ID="info_subject" runat="server" FieldLabel="交办事项" IndicatorText="*" />
                                        <ext:TextArea ID="info_remark" runat="server" FieldLabel="备注" Width="500" />
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="Button_Save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Button_Close" runat="server" Icon="ArrowTurnLeft" Text="返回" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click" />
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
