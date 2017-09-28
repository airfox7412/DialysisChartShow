<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_I_Alasamo.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_I_Alasamo" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>季度小结</title>
    <style type="text/css">
        .label_blue .x-label-value 
        {
            color: Blue;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="sel_date" runat="server" />
        
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout" PaddingSpec="0 10 0 10">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="季度小结" AutoScroll="true" ButtonAlign="Center" UI="Info" Width="1000" AutoShow="true">
                    <Items>
                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:DateField ID="dat_1" runat="server" FieldLabel="入院时间" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" Format="yyyy-MM-dd" />
                                <ext:DateField ID="info_date" runat="server" FieldLabel="小结时间" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" Format="yyyy-MM-dd" />
                                <ext:TextField ID="txt_2" runat="server" FieldLabel="透析号" LabelAlign="Right" LabelWidth="80" Padding="5" Width="250" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout">
                            <Items>
                                <ext:FileUploadField ID="UploadDoc" runat="server" Width="400" Icon="Attach" />
                                <ext:Button ID="SaveFile" runat="server" Text="Get File Path" Icon="DiskUpload" UI="Success">
                                    <DirectEvents>
                                        <Click OnEvent="UploadFile_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                    </Items>
                    <Buttons>
                        <ext:Button ID="btn_back" runat="server" Icon="ArrowLeft" Text="返回" Width="100" UI="Warning">
                            <DirectEvents>
                                <Click OnEvent="btn_back_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="DownFile" runat="server" Icon="DoorOpen" Text="下载空白月报" Width="100" UI="Primary">
                            <DirectEvents>
                                <Click OnEvent="DownFile_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Buttons>
                </ext:FormPanel>
                <ext:Window ID="PrintWindow" runat="server" Title="" Width="900" Height="650" Y="5" Modal="true" AutoRender="false" Hidden="true">
                    <Loader ID="Loader6" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                        <LoadMask ShowMask="true" />
                    </Loader>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
