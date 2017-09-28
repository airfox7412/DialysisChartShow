<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_0h_05.aspx.cs" Inherits="Dialysis_Chart_Show.Ipad.Dialysis_0h_05" %>

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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="疼痛评分表" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel4" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Label ID="Label1" runat="server" Text="疼痛程度：□0分无痛；□1~3分轻微痛；□4~6分比较痛；□7~9分非常痛；□10分剧痛" Cls="mylabel2" />
                                <ext:DateField ID="info_date" runat="server" FieldLabel="评估日期" Format="yyyy-MM-dd" />
                                <ext:TextField ID="txt_1" runat="server" FieldLabel="评估时间" IndicatorText="" />
                                <ext:TextField ID="txt_2" runat="server" FieldLabel="部位" IndicatorText="" />
                                <ext:TextField ID="txt_3" runat="server" FieldLabel="评分" IndicatorText="" />
                                <ext:TextField ID="txt_4" runat="server" FieldLabel="评估护士" IndicatorText="" />
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
