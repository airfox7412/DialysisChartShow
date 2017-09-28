<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="i_pad_default_v4.aspx.cs" Inherits="Dialysis_Chart_Show.i_pad_default_v4" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
<meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8,Auto-Rotate=Disable" />
    <link rel="apple-touch-icon" href="Styles/log_icon.jpg"/>
    <title>血液净化系统</title>
    <style type="text/css">
        label
        {
            font: normal 36px arial !important;
        }
        .x-btn .x-btn-inner
        {
            font-weight: bolder;
            color: #0000AA;
            font-size: 30px;
            background-color:#CAE1FF;
        }
        .x-btn-red .x-btn-inner
        {
            font-weight: bolder;
            color: Red;
            font-size: 30px;
            background-color:#CAE1FF;
        }
        .my-list .x-boundlist-item
        {
            font-weight: bold;
            font-size: 30px;
        }
        .x-form-trigger-input-cell
        {
            font-size: 30px;
        }
    </style>
    <script type="text/javascript">
        function CloseTab() {
            top.opener = null;
            top.window.close();
        }
        function newtab(bedno) {
            //alert(Ext.getCmp('Floor').getValue());
            //alert('<% =Session["Floor"] %>');
            //alert('ipad_PatientList.aspx?editmode=page1&floor=' + Ext.getCmp('Floor').getValue() + '&area=' + Ext.getCmp('Area').getValue() + '&time=' + Ext.getCmp('Time').getValue() + '&bedno=' + bedno);
            
            
            window.open('ipad_PatientList.aspx?editmode=page1&floor=' + Ext.getCmp('Floor').getValue() + '&area=' + Ext.getCmp('Area').getValue() + '&time=' + Ext.getCmp('Time').getValue() + '&bedno=' + bedno + '&daytyp=' + Ext.getCmp('daytyp').getValue());
        }
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <ext:Hidden ID="daytyp" runat="server" />
    <ext:Hidden ID="Floor" runat="server" />
    <ext:Hidden ID="Area" runat="server" />
    <ext:Hidden ID="Time" runat="server" />
    <ext:Hidden ID="Bed_number" runat="server" />
    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Layout="Anchor" Region="Center" Margins="0 5 5 5" BodyPadding="10">
                <Items>
                    <ext:Panel ID="Panel2" runat="server" Layout="HBoxLayout" Margin="10">
                        <Items>
                            <ext:Image ID="Image2" runat="server" Flex="2" ImageUrl="Styles/mark.png" Height="100" />
                            <ext:Label ID="Label1" runat="server" Text="日期:" Cls="lable" Flex="1" Padding="3" Height="100" />
                            <ext:Label ID="Tex_Datetime" runat="server" Cls="lable" Flex="3" Padding="3" Height="100" />
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="PanelFLOOR" runat="server" Layout="HBoxLayout" 
                        Margin="10">
                        <Items>
                            <ext:Label ID="Label2" runat="server" Text="楼层:" Cls="lable" Padding="3" Flex="2" />
                            <ext:ImageButton ID="Btn_2F" runat="server" Flex="5" Cls="x-btn" ToggleGroup="Group1" ImageUrl="Styles/2f.png" PressedImageUrl="Styles/2fp.png" OverImageUrl="Styles/2f.png" Height="100">
                                <DirectEvents>
                                    <Click OnEvent="Btn_2F_Click" />
                                </DirectEvents>
                            </ext:ImageButton>
                            <ext:ImageButton ID="Btn_3F" runat="server" Flex="5" Cls="x-btn" ToggleGroup="Group1" ImageUrl="Styles/3f.png" PressedImageUrl="Styles/3fp.png" OverImageUrl="Styles/3f.png" Height="100">
                                <DirectEvents>
                                    <Click OnEvent="Btn_3F_Click" />
                                </DirectEvents>
                            </ext:ImageButton>
                        </Items>
                    </ext:Panel>

                    <ext:Panel ID="PanelTIME" runat="server" Layout="HBoxLayout" Margin="10">
                        <Items>
                            <ext:Label ID="Label4" runat="server" Text="时段:" Cls="lable" Padding="3" Flex="1" />
                            <ext:ImageButton ID="Btn_Morning" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3" ImageUrl="Styles/morn.png" PressedImageUrl="Styles/mornp.png" OverImageUrl="Styles/morn.png" Height="100">
                                <DirectEvents>
                                    <Click OnEvent="Btn_Morning_Click" />
                                </DirectEvents>
                            </ext:ImageButton>
                            <ext:ImageButton ID="Btn_Afternoon" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3" ImageUrl="Styles/after.png" PressedImageUrl="Styles/afterp.png" OverImageUrl="Styles/after.png" Height="100">
                                <DirectEvents>
                                    <Click OnEvent="Btn_Afternoon_Click" />
                                </DirectEvents>
                            </ext:ImageButton>
                            <ext:ImageButton ID="Btn_Night" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3" ImageUrl="Styles/night.png" PressedImageUrl="Styles/nightp.png" OverImageUrl="Styles/night.png" Height="100">
                                <DirectEvents>
                                    <Click OnEvent="Btn_Night_Click" />
                                </DirectEvents>
                            </ext:ImageButton>
                        </Items>
                    </ext:Panel>

                    <ext:Panel ID="Panel2F" runat="server" Layout="HBoxLayout" Hidden="true" Margin="10" ButtonAlign="Left" >
                        <Items>
                            <ext:Label ID="Label3" runat="server" Text="床号:" Cls="lable" Padding="3"  Flex="1" />
                            <ext:ImageButton ID="Button1" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group2F" ImageUrl="Styles/01.png" PressedImageUrl="Styles/01p.png" OverImageUrl="Styles/01.png" Hidden="true" Height="100" OnClientClick="newtab('01');" />
                            <ext:ImageButton ID="Button2" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group2F" ImageUrl="Styles/02.png" PressedImageUrl="Styles/02p.png" OverImageUrl="Styles/02.png" Hidden="true" Height="100" OnClientClick="newtab('02');" />
                            <ext:Label ID="Label7" runat="server" Text="　" Flex="1" />
                            <ext:Label ID="Label8" runat="server" Text="　" Flex="1" />
                            <ext:Label ID="Label9" runat="server" Text="　" Flex="1" />
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel3F_1" runat="server" Layout="HBoxLayout" Hidden="true" Margin="10" ButtonAlign="Left" >
                        <Items>
                            <ext:Label ID="Label5" runat="server" Text="床号:" Cls="lable" Padding="3"  Flex="1" />
                       <%-- <ext:ImageButton ID="Button3" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/03.png" PressedImageUrl="Styles/03p.png" OverImageUrl="Styles/03.png" Hidden="true" Height="100" OnClientClick="newtab('03');" />
                            <ext:ImageButton ID="Button4" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/04.png" PressedImageUrl="Styles/04p.png" OverImageUrl="Styles/04.png" Hidden="true" Height="100" OnClientClick="newtab('04');" />
                            <ext:ImageButton ID="Button5" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/05.png" PressedImageUrl="Styles/05p.png" OverImageUrl="Styles/05.png" Hidden="true" Height="100" OnClientClick="newtab('05');" />
                            <ext:ImageButton ID="Button6" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/06.png" PressedImageUrl="Styles/06p.png" OverImageUrl="Styles/06.png" Hidden="true" Height="100" OnClientClick="newtab('06');" />
                            <ext:ImageButton ID="Button7" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/07.png" PressedImageUrl="Styles/07p.png" OverImageUrl="Styles/07.png" Hidden="true" Height="100" OnClientClick="newtab('07');" /> --%>
                            <ext:ImageButton ID="Button22" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/22.png" PressedImageUrl="Styles/22p.png" OverImageUrl="Styles/22.png" Hidden2="true" Height="100" OnClientClick="newtab('22');" />
                            <ext:ImageButton ID="Button23" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/23.png" PressedImageUrl="Styles/23p.png" OverImageUrl="Styles/23.png" Hidden2="true" Height="100" OnClientClick="newtab('23');" />
                            <ext:ImageButton ID="Button24" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/24.png" PressedImageUrl="Styles/24p.png" OverImageUrl="Styles/24.png" Hidden2="true" Height="100" OnClientClick="newtab('24');" />
                            <ext:ImageButton ID="Button25" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/25.png" PressedImageUrl="Styles/25p.png" OverImageUrl="Styles/25.png" Hidden2="true" Height="100" OnClientClick="newtab('25');" />
                            <ext:ImageButton ID="Button26" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/26.png" PressedImageUrl="Styles/26p.png" OverImageUrl="Styles/26.png" Hidden2="true" Height="100" OnClientClick="newtab('26');" />
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel3F_2" runat="server" Layout="HBoxLayout" Hidden="true" Margin="10" >
                        <Items>
                            <ext:Label ID="Label6" runat="server" Text="　" Flex="1" />
                       <%-- <ext:ImageButton ID="Button8" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/08.png" PressedImageUrl="Styles/08p.png" OverImageUrl="Styles/08.png" Hidden="true" Height="100" OnClientClick="newtab('08');" />
                            <ext:ImageButton ID="Button9" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/09.png" PressedImageUrl="Styles/09p.png" OverImageUrl="Styles/09.png" Hidden="true" Height="100" OnClientClick="newtab('09');" />
                            <ext:ImageButton ID="Button10" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/10.png" PressedImageUrl="Styles/10p.png" OverImageUrl="Styles/10.png" Hidden="true" Height="100" OnClientClick="newtab('10');" />
                            <ext:ImageButton ID="Button11" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/11.png" PressedImageUrl="Styles/11p.png" OverImageUrl="Styles/11.png" Hidden="true" Height="100" OnClientClick="newtab('11');" />
                            <ext:ImageButton ID="Button12" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/12.png" PressedImageUrl="Styles/12p.png" OverImageUrl="Styles/12.png" Hidden="true" Height="100" OnClientClick="newtab('12');" />
                            <ext:ImageButton ID="Button13" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/13.png" PressedImageUrl="Styles/13p.png" OverImageUrl="Styles/13.png" Hidden2="true" Height="100" OnClientClick="newtab('13');" />
                            <ext:ImageButton ID="Button14" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/14.png" PressedImageUrl="Styles/14p.png" OverImageUrl="Styles/14.png" Hidden2="true" Height="100" OnClientClick="newtab('14');" />
                            <ext:ImageButton ID="Button15" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/15.png" PressedImageUrl="Styles/15p.png" OverImageUrl="Styles/15.png" Hidden2="true" Height="100" OnClientClick="newtab('15');" />
                            <ext:ImageButton ID="Button16" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/16.png" PressedImageUrl="Styles/16p.png" OverImageUrl="Styles/16.png" Hidden2="true" Height="100" OnClientClick="newtab('16');" />
                            <ext:ImageButton ID="Button17" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/17.png" PressedImageUrl="Styles/17p.png" OverImageUrl="Styles/17.png" Hidden2="true" Height="100" OnClientClick="newtab('17');" />
                            <ext:ImageButton ID="Button18" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/18.png" PressedImageUrl="Styles/18p.png" OverImageUrl="Styles/18.png" Hidden2="true" Height="100" OnClientClick="newtab('18');" />
                            <ext:ImageButton ID="Button19" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/19.png" PressedImageUrl="Styles/19p.png" OverImageUrl="Styles/19.png" Hidden2="true" Height="100" OnClientClick="newtab('19');" />
                            <ext:ImageButton ID="Button20" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/20.png" PressedImageUrl="Styles/20p.png" OverImageUrl="Styles/20.png" Hidden2="true" Height="100" OnClientClick="newtab('20');" />
                            <ext:ImageButton ID="Button21" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/21.png" PressedImageUrl="Styles/21p.png" OverImageUrl="Styles/21.png" Hidden2="true" Height="100" OnClientClick="newtab('21');" /> --%>
                            <ext:ImageButton ID="Button27" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/27.png" PressedImageUrl="Styles/27p.png" OverImageUrl="Styles/27.png" Hidden2="true" Height="100" OnClientClick="newtab('27');" />
                            <ext:ImageButton ID="Button28" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/28.png" PressedImageUrl="Styles/28p.png" OverImageUrl="Styles/28.png" Hidden2="true" Height="100" OnClientClick="newtab('28');" />
                            <ext:ImageButton ID="Button29" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/29.png" PressedImageUrl="Styles/29p.png" OverImageUrl="Styles/29.png" Hidden2="true" Height="100" OnClientClick="newtab('29');" />
                            <ext:ImageButton ID="Button30" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/30.png" PressedImageUrl="Styles/30p.png" OverImageUrl="Styles/30.png" Hidden2="true" Height="100" OnClientClick="newtab('30');" />
                            <ext:ImageButton ID="Button31" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/31.png" PressedImageUrl="Styles/31p.png" OverImageUrl="Styles/31.png" Hidden2="true" Height="100" OnClientClick="newtab('31');" />
                       <%-- <ext:ImageButton ID="Button32" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/32.png" PressedImageUrl="Styles/32p.png" OverImageUrl="Styles/32.png" Hidden2="true" Height="100" OnClientClick="newtab('32');" />
                            <ext:ImageButton ID="Button33" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/33.png" PressedImageUrl="Styles/33p.png" OverImageUrl="Styles/33.png" Hidden2="true" Height="100" OnClientClick="newtab('33');" />
                            <ext:ImageButton ID="Button34" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group3F" ImageUrl="Styles/34.png" PressedImageUrl="Styles/34p.png" OverImageUrl="Styles/34.png" Hidden2="true" Height="100" OnClientClick="newtab('34');" /> --%>

                        </Items>
                    </ext:Panel>
                
                </Items>
            </ext:Panel>
        </Items>
    </ext:Viewport>
    </div>
    </form>
</body>
</html>
