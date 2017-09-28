<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="i_pad_default_new.aspx.cs" Inherits="Dialysis_Chart_Show.i_pad_default_new" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
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
            top.opener=null;
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
                <ext:Panel ID="Panel3" runat="server" Layout="HBoxLayout" 
                    Margin="10">
                    <Items>
                        <ext:Label ID="Label2" runat="server" Text="楼层:" Cls="lable" Padding="3" Flex="2" />
                        <ext:ImageButton ID="Btn_3F" runat="server" Flex="5" Cls="x-btn" ToggleGroup="Group1" ImageUrl="Styles/3f.png" PressedImageUrl="Styles/3fp.png" OverImageUrl="Styles/3f.png" Height="100">
                            <DirectEvents>
                                <Click OnEvent="Btn_3F_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_5F" runat="server" Flex="5" Cls="x-btn" ToggleGroup="Group1" ImageUrl="Styles/5f.png" PressedImageUrl="Styles/5fp.png" OverImageUrl="Styles/5f.png" Height="100">
                            <DirectEvents>
                                <Click OnEvent="Btn_5F_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel4" runat="server" Layout="HBoxLayout" Margin="10">
                    <Items>
                        <ext:Label ID="Label3" runat="server" Text="床区:" Cls="lable" ColumnWidth=".15" Padding="3" Height="100" Flex="1" />
                        <ext:ImageButton ID="Btn_A" runat="server" Flex="1" Cls="x-btn" Height="100" ToggleGroup="Group2" Hidden="true" ImageUrl="Styles/a.png" PressedImageUrl="Styles/ap.png" OverImageUrl="Styles/a.png">
                            <DirectEvents>
                                <Click OnEvent="Btn_A_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_B" runat="server" Flex="1" Cls="x-btn" Height="100" ToggleGroup="Group2" Hidden="true" ImageUrl="Styles/b.png" PressedImageUrl="Styles/bp.png" OverImageUrl="Styles/b.png">
                            <DirectEvents>
                                <Click OnEvent="Btn_B_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_C" runat="server" Flex="1" Cls="x-btn" Height="100" ToggleGroup="Group2" Hidden="true" ImageUrl="Styles/c.png" PressedImageUrl="Styles/cp.png" OverImageUrl="Styles/c.png">
                            <DirectEvents>
                                <Click OnEvent="Btn_C_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_D" runat="server" Flex="1" Cls="x-btn" Height="100" ToggleGroup="Group2" Hidden="true" ImageUrl="Styles/d.png" PressedImageUrl="Styles/dp.png" OverImageUrl="Styles/d.png">
                            <DirectEvents>
                                <Click OnEvent="Btn_D_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_VIP" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group2" Hidden="true" ImageUrl="Styles/vip.png" PressedImageUrl="Styles/vipp.png" OverImageUrl="Styles/vip.png"
                            Height="100">
                            <DirectEvents>
                                <Click OnEvent="Btn_VIP_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_E" runat="server" Flex="1" Cls="x-btn" Height="100" ToggleGroup="Group2" Hidden="true" ImageUrl="Styles/e.png" PressedImageUrl="Styles/ep.png" OverImageUrl="Styles/e.png">
                            <DirectEvents>
                                <Click OnEvent="Btn_E_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_F" runat="server" Flex="1" Cls="x-btn" Height="100" ToggleGroup="Group2" Hidden="true" ImageUrl="Styles/f.png" PressedImageUrl="Styles/fp.png" OverImageUrl="Styles/f.png">
                            <DirectEvents>
                                <Click OnEvent="Btn_F_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                        <ext:ImageButton ID="Btn_G" runat="server" Flex="1" Cls="x-btn" Height="100" ToggleGroup="Group2" Hidden="true" ImageUrl="Styles/g.png" PressedImageUrl="Styles/gp.png" OverImageUrl="Styles/g.png">
                            <DirectEvents>
                                <Click OnEvent="Btn_G_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel5" runat="server" Layout="HBoxLayout" Margin="10">
                    <Items>
                        <ext:Label ID="Label4" runat="server" Text="时段:" Cls="lable" ColumnWidth=".15" Padding="3" Flex="1" />
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
                <ext:Panel ID="Panel6" runat="server" Layout="HBoxLayout" Hidden="true" Margin="10">
                    <Items>
                        <ext:Label ID="Label5" runat="server" Text="床号:" Cls="lable" ColumnWidth=".15" Padding="3" Height="100" Flex="1" />
                        <ext:ImageButton ID="Button1" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/01.png" PressedImageUrl="Styles/01p.png" OverImageUrl="Styles/01.png"
                            Hidden="true" Height="100" OnClientClick="newtab('01');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button2" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/02.png" PressedImageUrl="Styles/02p.png" OverImageUrl="Styles/02.png"
                            Hidden="true" Height="100" OnClientClick="newtab('02');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button3" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/03.png" PressedImageUrl="Styles/03p.png" OverImageUrl="Styles/03.png"
                            Hidden="true" Height="100" OnClientClick="newtab('03');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button4" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/04.png" PressedImageUrl="Styles/04p.png" OverImageUrl="Styles/04.png"
                            Hidden="true" Height="100" OnClientClick="newtab('04');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button5" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/05.png" PressedImageUrl="Styles/05p.png" OverImageUrl="Styles/05.png"
                            Hidden="true" Height="100" OnClientClick="newtab('05');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button11_1" runat="server" Flex="2" Cls="x-btn-red" ToggleGroup="Group4" ImageUrl="Styles/11_1.png" PressedImageUrl="Styles/11_1p.png" OverImageUrl="Styles/11_1.png"
                            Hidden="true" Height="100" OnClientClick="newtab('11-1');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button12" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" ImageUrl="Styles/12.png" PressedImageUrl="Styles/12p.png" OverImageUrl="Styles/12.png"
                            Hidden="true" Height="100" OnClientClick="newtab('12');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button13" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" ImageUrl="Styles/13.png" PressedImageUrl="Styles/13p.png" OverImageUrl="Styles/13.png"
                            Hidden="true" Height="100" OnClientClick="newtab('13');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button14" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/14.png" PressedImageUrl="Styles/14p.png" OverImageUrl="Styles/14.png"
                            Hidden="true" Height="100" OnClientClick="newtab('14');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button15" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/15.png" PressedImageUrl="Styles/15p.png" OverImageUrl="Styles/15.png"
                            Hidden="true" Height="100" OnClientClick="newtab('15');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button16" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/16.png" PressedImageUrl="Styles/16p.png" OverImageUrl="Styles/16.png"
                            Hidden="true" Height="100" OnClientClick="newtab('16');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button17" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/17.png" PressedImageUrl="Styles/17p.png" OverImageUrl="Styles/17.png"
                            Hidden="true" Height="100" OnClientClick="newtab('17');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button18" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/18.png" PressedImageUrl="Styles/18p.png" OverImageUrl="Styles/18.png"
                            Hidden="true" Height="100" OnClientClick="newtab('18');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button19" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/19.png" PressedImageUrl="Styles/19p.png" OverImageUrl="Styles/19.png"
                            Hidden="true" Height="100" OnClientClick="newtab('19');">
                        </ext:ImageButton>
                        
                    
                        <ext:ImageButton ID="Button31" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/31.png" PressedImageUrl="Styles/31p.png" OverImageUrl="Styles/31.png"
                            Height="100" OnClientClick="newtab('31');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button32" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/32.png" PressedImageUrl="Styles/32p.png" OverImageUrl="Styles/32.png"
                            Height="100" OnClientClick="newtab('32');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button33" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/33.png" PressedImageUrl="Styles/33p.png" OverImageUrl="Styles/33.png"
                            Height="100" OnClientClick="newtab('33');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button34" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/34.png" PressedImageUrl="Styles/34p.png" OverImageUrl="Styles/34.png"
                            Height="100" OnClientClick="newtab('34');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button35" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/35.png" PressedImageUrl="Styles/35p.png" OverImageUrl="Styles/35.png"
                            Height="100" OnClientClick="newtab('35');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button36" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/36.png" PressedImageUrl="Styles/36p.png" OverImageUrl="Styles/36.png"
                            Height="100" OnClientClick="newtab('36');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button37" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/37.png" PressedImageUrl="Styles/37p.png" OverImageUrl="Styles/37.png"
                            Height="100" OnClientClick="newtab('37');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button38" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/38.png" PressedImageUrl="Styles/38p.png" OverImageUrl="Styles/38.png"
                            Height="100" OnClientClick="newtab('38');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button46_1" runat="server" Flex="2" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/46_1.png" PressedImageUrl="Styles/46_1p.png" OverImageUrl="Styles/46_1.png"
                            Height="100" OnClientClick="newtab('46-1');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button47" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/47.png" PressedImageUrl="Styles/47p.png" OverImageUrl="Styles/47.png"
                            Height="100" OnClientClick="newtab('47');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button48" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/48.png" PressedImageUrl="Styles/48p.png" OverImageUrl="Styles/48.png"
                            Height="100" OnClientClick="newtab('48');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button49" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/49.png" PressedImageUrl="Styles/49p.png" OverImageUrl="Styles/49.png"
                            Height="100" OnClientClick="newtab('49');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button50" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/50.png" PressedImageUrl="Styles/50p.png" OverImageUrl="Styles/50.png"
                            Height="100" OnClientClick="newtab('50');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button51" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/51.png" PressedImageUrl="Styles/51p.png" OverImageUrl="Styles/51.png"
                            Height="100" OnClientClick="newtab('51');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Buttonv1" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/v1.png" PressedImageUrl="Styles/v1p.png" OverImageUrl="Styles/v1.png"
                            Height="100" OnClientClick="newtab('v1');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Buttonv2" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/v2.png" PressedImageUrl="Styles/v2p.png" OverImageUrl="Styles/v2.png"
                            Height="100" OnClientClick="newtab('v2');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Buttonv3" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/v3.png" PressedImageUrl="Styles/v3p.png" OverImageUrl="Styles/v3.png"
                            Height="100" OnClientClick="newtab('v3');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Buttonv4" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/v4.png" PressedImageUrl="Styles/v4p.png" OverImageUrl="Styles/v4.png"
                            Height="100" OnClientClick="newtab('v4');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button81" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/81.png" PressedImageUrl="Styles/81p.png" OverImageUrl="Styles/81.png"
                            Height="100" OnClientClick="newtab('81');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button82" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/82.png" PressedImageUrl="Styles/82p.png" OverImageUrl="Styles/82.png"
                            Height="100" OnClientClick="newtab('82');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button83" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/83.png" PressedImageUrl="Styles/83p.png" OverImageUrl="Styles/83.png"
                            Height="100" OnClientClick="newtab('83');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button84" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/84.png" PressedImageUrl="Styles/84p.png" OverImageUrl="Styles/84.png"
                            Height="100" OnClientClick="newtab('84');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button85" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/85.png" PressedImageUrl="Styles/85p.png" OverImageUrl="Styles/85.png"
                            Height="100" OnClientClick="newtab('85');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button86" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/86.png" PressedImageUrl="Styles/86p.png" OverImageUrl="Styles/86.png"
                            Height="100" OnClientClick="newtab('86');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button93" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/93.png" PressedImageUrl="Styles/93p.png" OverImageUrl="Styles/93.png"
                            Height="100" OnClientClick="newtab('93');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button94" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/94.png" PressedImageUrl="Styles/94p.png" OverImageUrl="Styles/94.png"
                            Height="100" OnClientClick="newtab('94');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button95" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/95.png" PressedImageUrl="Styles/95p.png" OverImageUrl="Styles/95.png"
                            Height="100" OnClientClick="newtab('95');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button95_1" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/95_1.png" PressedImageUrl="Styles/95_1p.png" OverImageUrl="Styles/95_1.png"
                            Height="100" OnClientClick="newtab('95-1');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button96" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/96.png" PressedImageUrl="Styles/96p.png" OverImageUrl="Styles/96.png"
                            Height="100" OnClientClick="newtab('96');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button97" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/97.png" PressedImageUrl="Styles/97p.png" OverImageUrl="Styles/97.png"
                            Height="100" OnClientClick="newtab('97');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button105" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/105.png" PressedImageUrl="Styles/105p.png" OverImageUrl="Styles/105.png"
                            Height="100" OnClientClick="newtab('105');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button106" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/106.png" PressedImageUrl="Styles/106p.png" OverImageUrl="Styles/106.png"
                            Height="100" OnClientClick="newtab('106');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button107" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/107.png" PressedImageUrl="Styles/107p.png" OverImageUrl="Styles/107.png"
                            Height="100" OnClientClick="newtab('107');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button108" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/108.png" PressedImageUrl="Styles/108p.png" OverImageUrl="Styles/108.png"
                            Height="100" OnClientClick="newtab('108');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button109" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/109.png" PressedImageUrl="Styles/109p.png" OverImageUrl="Styles/109.png"
                            Height="100" OnClientClick="newtab('109');">
                        </ext:ImageButton>
                        
                        <%--<ext:ComboBox ID="com_Select_Time" runat="server" Width="200" Flex="2" Height="120"
                            LabelAlign="Right" Cls="cntrlFont">
                            <SelectedItems>
                            </SelectedItems>
                            <Items>
                            </Items>
                            <DirectEvents>
                                <Select OnEvent="com_Select_Time_Click" />
                            </DirectEvents>
                            <ListConfig ID="ListConfig1" runat="server" Cls="my-list" />
                        </ext:ComboBox>--%>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel7" runat="server" Layout="HBoxLayout" Margin="10" Hidden="true">
                    <Items>
                        <ext:ImageButton ID="Button6" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/06.png" PressedImageUrl="Styles/06p.png" OverImageUrl="Styles/06.png"
                            Hidden="true" Height="100" OnClientClick="newtab('06');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button7" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/07.png" PressedImageUrl="Styles/07p.png" OverImageUrl="Styles/07.png"
                            Hidden="true" Height="100" OnClientClick="newtab('07');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button8" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/08.png" PressedImageUrl="Styles/08p.png" OverImageUrl="Styles/08.png"
                            Hidden="true" Height="100" OnClientClick="newtab('08');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button9" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/09.png" PressedImageUrl="Styles/09p.png" OverImageUrl="Styles/09.png"
                            Hidden="true" Height="100" OnClientClick="newtab('09');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button10" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" ImageUrl="Styles/10.png" PressedImageUrl="Styles/10p.png" OverImageUrl="Styles/10.png"
                            Hidden="true" Height="100" OnClientClick="newtab('10');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button11" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" ImageUrl="Styles/11.png" PressedImageUrl="Styles/11p.png" OverImageUrl="Styles/11.png"
                            Hidden="true" Height="100" OnClientClick="newtab('11');">
                        </ext:ImageButton>

                        <ext:ImageButton ID="Button20" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/20.png" PressedImageUrl="Styles/20p.png" OverImageUrl="Styles/20.png"
                            Hidden="true" Height="100" OnClientClick="newtab('20');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button21" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/21.png" PressedImageUrl="Styles/21p.png" OverImageUrl="Styles/21.png"
                            Hidden="true" Height="100" OnClientClick="newtab('21');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button22" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/22.png" PressedImageUrl="Styles/22p.png" OverImageUrl="Styles/22.png"
                            Hidden="true" Height="100" OnClientClick="newtab('22');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button23" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/23.png" PressedImageUrl="Styles/23p.png" OverImageUrl="Styles/23.png"
                            Hidden="true" Height="100" OnClientClick="newtab('23');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button24" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/24.png" PressedImageUrl="Styles/24p.png" OverImageUrl="Styles/24.png"
                            Hidden="true" Height="100" OnClientClick="newtab('24');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button25" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/25.png" PressedImageUrl="Styles/25p.png" OverImageUrl="Styles/25.png"
                            Hidden="true" Height="100" OnClientClick="newtab('25');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button26" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/26.png" PressedImageUrl="Styles/26p.png" OverImageUrl="Styles/26.png"
                            Hidden="true" Height="100" OnClientClick="newtab('26');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button27" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" ImageUrl="Styles/27.png" PressedImageUrl="Styles/27p.png" OverImageUrl="Styles/27.png"
                            Hidden="true" Height="100" OnClientClick="newtab('27');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button28" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" ImageUrl="Styles/28.png" PressedImageUrl="Styles/28p.png" OverImageUrl="Styles/28.png"
                            Hidden="true" Height="100" OnClientClick="newtab('28');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button29" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" ImageUrl="Styles/29.png" PressedImageUrl="Styles/29p.png" OverImageUrl="Styles/29.png"
                            Hidden="true" Height="100" OnClientClick="newtab('29');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button30" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" ImageUrl="Styles/30.png" PressedImageUrl="Styles/30p.png" OverImageUrl="Styles/30.png"
                            Hidden="true" Height="100" OnClientClick="newtab('30');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button39" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/39.png" PressedImageUrl="Styles/39p.png" OverImageUrl="Styles/39.png"
                            Height="100" OnClientClick="newtab('39');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button40" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/40.png" PressedImageUrl="Styles/40p.png" OverImageUrl="Styles/40.png"
                            Height="100" OnClientClick="newtab('40');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button41" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/41.png" PressedImageUrl="Styles/41p.png" OverImageUrl="Styles/41.png"
                            Height="100" OnClientClick="newtab('41');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button42" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/42.png" PressedImageUrl="Styles/42.png" OverImageUrl="Styles/42.png"
                            Height="100" OnClientClick="newtab('42');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button43" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/43.png" PressedImageUrl="Styles/43p.png" OverImageUrl="Styles/43.png"
                            Height="100" OnClientClick="newtab('43');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button44" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/44.png" PressedImageUrl="Styles/44p.png" OverImageUrl="Styles/44.png"
                            Height="100" OnClientClick="newtab('44');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button45" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/45.png" PressedImageUrl="Styles/45.png" OverImageUrl="Styles/45.png"
                            Height="100" OnClientClick="newtab('45');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button46" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/46.png" PressedImageUrl="Styles/46p.png" OverImageUrl="Styles/496.png"
                            Height="100" OnClientClick="newtab('46');">
                        </ext:ImageButton>

                        <ext:ImageButton ID="Button51_1" runat="server" Flex="2" Cls="x-btn-red" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/51_1.png" PressedImageUrl="Styles/51_1p.png" OverImageUrl="Styles/51_1.png"
                            Height="100" OnClientClick="newtab('51-1');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button52" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/52.png" PressedImageUrl="Styles/52p.png" OverImageUrl="Styles/52.png"
                            Height="100" OnClientClick="newtab('52');">
                        </ext:ImageButton>

                        <ext:ImageButton ID="Button53" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/53.png" PressedImageUrl="Styles/53p.png" OverImageUrl="Styles/53.png"
                            Height="100" OnClientClick="newtab('53');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button54" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/54.png" PressedImageUrl="Styles/54p.png" OverImageUrl="Styles/54.png"
                            Height="100" OnClientClick="newtab('54');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button55" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4" Hidden="true" ImageUrl="Styles/55.png" PressedImageUrl="Styles/55p.png" OverImageUrl="Styles/55.png"
                            Height="100" OnClientClick="newtab('55');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button56" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/56.png" PressedImageUrl="Styles/56p.png" OverImageUrl="Styles/56.png"
                            Height="100" OnClientClick="newtab('56');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button57" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/57.png" PressedImageUrl="Styles/57p.png" OverImageUrl="Styles/57.png"
                            Height="100" OnClientClick="newtab('57');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button58" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/58.png" PressedImageUrl="Styles/58p.png" OverImageUrl="Styles/58.png"
                            Height="100" OnClientClick="newtab('58');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button87" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/87.png" PressedImageUrl="Styles/87p.png" OverImageUrl="Styles/87.png"
                            Height="100" OnClientClick="newtab('87');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button88" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/88.png" PressedImageUrl="Styles/88p.png" OverImageUrl="Styles/88.png"
                            Height="100" OnClientClick="newtab('88');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button89" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/89.png" PressedImageUrl="Styles/89p.png" OverImageUrl="Styles/89.png"
                            Height="100" OnClientClick="newtab('89');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button90" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/90.png" PressedImageUrl="Styles/90p.png" OverImageUrl="Styles/90.png"
                            Height="100" OnClientClick="newtab('90');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button91" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/91.png" PressedImageUrl="Styles/91p.png" OverImageUrl="Styles/91.png"
                            Height="100" OnClientClick="newtab('91');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button92" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/92.png" PressedImageUrl="Styles/92p.png" OverImageUrl="Styles/92.png"
                            Height="100" OnClientClick="newtab('92');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button98" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/98.png" PressedImageUrl="Styles/98p.png" OverImageUrl="Styles/98.png"
                            Height="100" OnClientClick="newtab('98');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button99" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/99.png" PressedImageUrl="Styles/99p.png" OverImageUrl="Styles/99.png"
                            Height="100" OnClientClick="newtab('99');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button100" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/100.png" PressedImageUrl="Styles/100p.png" OverImageUrl="Styles/100.png"
                            Height="100" OnClientClick="newtab('100');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button101" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/101.png" PressedImageUrl="Styles/101p.png" OverImageUrl="Styles/101.png"
                            Height="100" OnClientClick="newtab('101');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button102" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/102.png" PressedImageUrl="Styles/102p.png" OverImageUrl="Styles/102.png"
                            Height="100" OnClientClick="newtab('102');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button103" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/103.png" PressedImageUrl="Styles/103p.png" OverImageUrl="Styles/103.png"
                            Height="100" OnClientClick="newtab('103');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button104" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/104.png" PressedImageUrl="Styles/104p.png" OverImageUrl="Styles/104.png"
                            Height="100" OnClientClick="newtab('104');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button110" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/110.png" PressedImageUrl="Styles/110p.png" OverImageUrl="Styles/110.png"
                            Height="100" OnClientClick="newtab('110');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button111" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/111.png" PressedImageUrl="Styles/111p.png" OverImageUrl="Styles/111.png"
                            Height="100" OnClientClick="newtab('111');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button112" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/112.png" PressedImageUrl="Styles/112p.png" OverImageUrl="Styles/112.png"
                            Height="100" OnClientClick="newtab('112');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button113" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/113.png" PressedImageUrl="Styles/113p.png" OverImageUrl="Styles/113.png"
                            Height="100" OnClientClick="newtab('113');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button114" runat="server" Flex="1" Cls="x-btn-red" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/114.png" PressedImageUrl="Styles/114p.png" OverImageUrl="Styles/114.png"
                            Height="100" OnClientClick="newtab('114');">
                        </ext:ImageButton>
                        <ext:ImageButton ID="Button115" runat="server" Flex="1" Cls="x-btn" ToggleGroup="Group4"
                            Hidden="true" ImageUrl="Styles/115.png" PressedImageUrl="Styles/115p.png" OverImageUrl="Styles/115.png"
                            Height="100" OnClientClick="newtab('115');">
                        </ext:ImageButton>

                    </Items>
                </ext:Panel>
                <%--<ext:Panel ID="Panel8" runat="server" Layout="HBoxLayout" 
                    Margin="10">
                    <Items>
                        <ext:Button ID="Btn_clear" runat="server" Text="清除" Flex="1" Cls="x-btn-c" 
                            Height="100">
                            <DirectEvents>
                                <Click OnEvent="Btn_clear_Click">
                                </Click>
                            </DirectEvents>
                        </ext:Button>
                    </Items>
                </ext:Panel>--%>
            </Items>
        </ext:Panel>
        </Items>
        </ext:Viewport>
        <%--<ext:TaskManager ID="TaskManager1" runat="server">
            <Tasks>
                <ext:Task TaskID="servertime" Interval="2000" >
                    <DirectEvents>
                        <Update OnEvent="Timer1_Timer" />
                    </DirectEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>--%>
    </div>
    </form>
</body>
</html>
