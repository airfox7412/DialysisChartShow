<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Info.aspx.cs" Inherits="Dialysis_Chart_Show.Dialysis_Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化病例信息登记系统</title>    
    <link href="css/grid.css" rel="stylesheet"/>

    <style type="text/css">               
        .x-grid-custom-mid .x-grid-item TD {
            color: Black;
            font-size : 14px;
            font-weight:bold;
        }

        .x-grid-custom-mid .x-column-header {
            /*background : #718CA1 url(css/header_sprite.png) repeat scroll 0 bottom;*/
            background-color: #5ABCE0;
            font-size: 12px;
            border-left-color  : #6085A5;
            border-right-color : #728BA1;
        }

        .x-grid-custom-mid .x-column-header-over {
            /*background : #ebf3fd url(css/header_sprite_over.png) repeat 0 bottom !important;*/
        }

        .x-grid-custom-mid .x-column-header div {
            font-size  : 12px;
            color: Black;
        }

        .x-grid-custom-mid .company-link {
            color : #0E3D4F;
        }

        .x-grid-custom-mid .x-column-header-trigger {
            /*background : #718CA1 url(css/grid3-hd-btn.png) no-repeat left center;*/
            color:Blue;
        }

        .x-grid-custom-mid .x-grid-item-alt .x-grid-cell {
            background-color : #DAE2E8;
        }

        .x-grid-custom-mid .x-grid-item-over .x-grid-cell {
            border-color : #728BA1;
            /*background   : url(css/row-over.png);*/
            background-color: Yellow;
        }

        .x-grid-custom-mid .x-grid-item-selected .x-grid-cell {
            /*background   : url(css/row-selected.png) repeat-x scroll 0 0 #7BBBCF;*/
            background-color:#5ABCE0;
            border-color : #728BA1;
            border-style : solid;
        }
        .x-grid-custom-mid .x-grid-editor .x-form-text
        {
            font-size:12px;
            font-weight:bold;
            color:Blue;
        }

        .x-tip .x-tip-body 
        {
            font-size: 16px;
            background-color:#157FCC;
        }
        
        .x-tip .x-tip-header-text 
        {
            background-color:#157FCC;
        }        
       
       .x-btn-icon-left > .x-btn-icon-el-default-small, .x-btn-icon-right > .x-btn-icon-el-default-small
        {
            height:32px;
            width:32px;
        }
        
        .x-btn .x-btn-menu-active.x-btn-default-small, .x-btn.x-btn-pressed.x-btn-default-small
        {
            border-color:Gray;
            background-color:transparent;
        }
        
        .head_label .x-label-value
        {
            font-size: 18px; 
            color: White;
            font-weight:bolder;
        }
        
        .Label_copyright1 .x-label-value
        {
            color:White;
        }    
        .Label_copyright2 .x-label-value
        {
            color:Navy;
        }
        #Panel_South .x-autocontainer-innerCt
        {
            background-color: #5ABCE0;
        }
        .Panellogo .x-autocontainer-innerCt
        {
            /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#1e5799+0,2989d8+100,207cca+100,7db9e8+100 */
            background: #1e5799; /* Old browsers */
            background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0 ); /* IE6-9 */ 
        }
        .x-panel-default
        {
            background: #1e5799; /* Old browsers */
            background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
            filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0 ); /* IE6-9 */ 
        }
        
        #Panel_Group2 .x-autocontainer-innerCt
        {
            background: #5ABCE0;
        }
        
        #Panel_Group1 .x-autocontainer-innerCt
        {
            background-color: White; /* #5ABCE0; */
        }
        
        #ButtonGroup1 .x-box-layout-ct
        {
            border-top-left-radius:10px; /*設定左上角*/
            border-top-right-radius:10px; /*設定右上角*/
            border-bottom-right-radius:0px; /*設定右下角*/
            border-bottom-left-radius:0px; /*設定左下角*/
        }
        
        #ButtonGroup2 .x-box-layout-ct
        {
            border-top-left-radius:10px; /*設定左上角*/
            border-top-right-radius:10px; /*設定右上角*/
            border-bottom-right-radius:0px; /*設定右下角*/
            border-bottom-left-radius:0px; /*設定左下角*/
        }
        
        #ButtonGroup3 .x-box-layout-ct
        {
            border-top-left-radius:10px; /*設定左上角*/
            border-top-right-radius:10px; /*設定右上角*/
            border-bottom-right-radius:0px; /*設定右下角*/
            border-bottom-left-radius:0px; /*設定左下角*/
        }
        
        #ButtonGroup4 .x-box-layout-ct
        {
            border-top-left-radius:10px; /*設定左上角*/
            border-top-right-radius:10px; /*設定右上角*/
            border-bottom-right-radius:0px; /*設定右下角*/
            border-bottom-left-radius:0px; /*設定左下角*/
        }
        .x-btn-default-small
        {
            border-top-left-radius:0px; /*設定左上角*/
            border-top-right-radius:0px; /*設定右上角*/
            border-bottom-right-radius:6px; /*設定右下角*/
            border-bottom-left-radius:6px; /*設定左下角*/
        }
        
        @font-face 
        {
          font-family: 'webfont';
          src: url('//at.alicdn.com/t/funj9ig0ymk7qfr.eot'); /* IE9*/
          src: url('//at.alicdn.com/t/funj9ig0ymk7qfr.eot?#iefix') format('embedded-opentype'), /* IE6-IE8 */
          url('//at.alicdn.com/t/funj9ig0ymk7qfr.woff') format('woff'), /* chrome、firefox */
          url('//at.alicdn.com/t/funj9ig0ymk7qfr.ttf') format('truetype'), /* chrome、firefox、opera、Safari, Android, iOS 4.2+*/
          url('//at.alicdn.com/t/funj9ig0ymk7qfr.svg#NotoSansHans-Black') format('svg'); /* iOS 4.1- */
        }
        .logo .x-label-value {
            vertical-align: middle;
            color: white;
            font-size: xx-large;
            line-height: 50px;
            padding:0 0 0 280px;
            font-family: 'webfont';
        }
        .logop {
            vertical-align: middle;
            line-height: 40px;
        }        
    
        .Text-blue .x-form-item-label-text {
            color: blue;
        }
    </style>

    <script type="text/javascript">
        var enterKeyPressHandler = function (f, e) {
            if (e.getKey() == e.ENTER) {
                Ext.Net.DirectMethods.show_history();
                e.stopEvent();
            }
        };
        var myRenderer = function (value, metadata) {
            if (value === "") {
                metadata.style = "color : red;"
            }
            return value;
        };

        var prepareCellCommand = function (grid, command, record, row, col) {
            if (command.command == 'BiochemicalIndicators' && record.get("txt_101") == "I") { //txt_101=生化指标
                command.iconCls = "icon-moneyeuro";
            }
        };

        var onShow = function (toolTip, grid) {
            var view = grid.getView(),
                record = view.getRecord(toolTip.triggerElement),
                data = Ext.encode(record.data);
            toolTip.update("<font size='5'>"+data+"</font>");
        };     
    </script>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="sTIME" runat="server" />
        <ext:Hidden ID="sFLOOR" runat="server" />
        <ext:Hidden ID="sAREA" runat="server" />
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="txtWEEK" runat="server" />
        <ext:Hidden ID="ROW_CNT" runat="server" />  

        <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN" Theme="Triton"/>
        <ext:FormPanel runat="server" ID="FormPanel2" Width="0" Height="0" Enabled="false">
            <Items>
                <ext:TextField ID="TextQuery" runat="server" Text="0800933288"/>
            </Items>
            <Buttons>
                <ext:Button runat="server" ID="BtnQuery" Type="Submit" Text="">
                    <DirectEvents>
                        <Click OnEvent="BtnQuery_Click">
                            <EventMask ShowMask="true" MinDelay="500" CustomTarget="FormPanel2" />
                        </Click>
                    </DirectEvents>
                </ext:Button>
            </Buttons>
        </ext:FormPanel>  

        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel_North" runat="server" Region="North" Cls="Panellogo">
                <Items>
                    <ext:Container ID="Container1" runat="server">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Image ID="ImageLogo" runat="server" ImageUrl="Styles/logo_wj200.jpg" Height="50" Width="220" />
                            <ext:Label ID="LabelLogo" runat="server" Text="血液透析中心管理系统" Cls="logo" Width="830" />
                            <ext:Image ID="Image1" runat="server" ImageUrl="Styles/logo_pic.jpg" Cls="logop" Width="150" />
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container2" runat="server">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Panel ID="Panel_Group" runat="server" Layout="HBoxLayout">
                                <Items>
                                    <ext:Panel ID="ButtonGroup1" runat="server" Title="工作流程" TitleAlign="Center" Region="West" PaddingSpec="2 30 2 2">
                                        <Items>
                                            <%--<ext:Button ID="Button2" runat="server" Text="Pictos Anchor" GlyphSpec="x0061@Pictos" Scale="Large" />--%>
                                            <ext:Button ID="Button_11" runat="server" IconUrl="Styles/PNG/16/Calender.png" Scale="Small" IconAlign="Top" Text="排班" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="排班" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton11_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_12" runat="server" IconUrl="Styles/PNG/16/Checkin.png" Scale="Small" IconAlign="Top" Text="报到" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="报到" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton12_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button1" runat="server" IconUrl="Styles/PNG/16/temporary.png" Scale="Small" IconAlign="Top" Text="临时" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="临时排班" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton121_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_13" runat="server" IconUrl="Styles/PNG/16/Add_Green.png" Scale="Small" IconAlign="Top" Text="治疗" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="治疗计划" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton13_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_141" runat="server" IconUrl="Styles/PNG/16/survey.png" Scale="Small" IconAlign="Top" Text="四问" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="首诊四问" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton141_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="ButtonGroup2" runat="server" Title="病历" TitleAlign="Center" Region="West" PaddingSpec="2 30 2 2">
                                        <Items>
                                            <ext:Button ID="Button_21" runat="server" IconUrl="Styles/PNG/16/people.png" Scale="Small" IconAlign="Top" Text="病史" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="病史" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton21_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_41" runat="server" IconUrl="Styles/PNG/16/Patient_Book.png" Scale="Small" IconAlign="Top" Text="信息" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="患者信息纪录" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton41_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_22" runat="server" IconUrl="Styles/PNG/16/graph.png" Scale="Small" IconAlign="Top" Text="纪录" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="纪录表" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton22_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_23" runat="server" IconUrl="Styles/PNG/16/piechart.png" Scale="Small" IconAlign="Top" Text="讯息" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="血透讯息" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton23_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_24" runat="server" IconUrl="Styles/PNG/16/Exam.png" Scale="Small" IconAlign="Top" Text="检查" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="实验室检查" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton24_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_25" runat="server" IconUrl="Styles/PNG/16/Tutorial.png" Scale="Small" IconAlign="Top" Text="诊断" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="诊断信息" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton25_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_26" runat="server" IconUrl="Styles/PNG/16/Dialysis_Info.png" Scale="Small" IconAlign="Top" Text="病历" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="病历纪录" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton26_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_27" runat="server" IconUrl="Styles/PNG/16/Paste.png" Scale="Small" IconAlign="Top" Text="评估" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="血透评估表" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton27_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="ButtonGroup3" runat="server" Title="统计" TitleAlign="Center" Region="West" PaddingSpec="2 30 2 2">
                                        <Items>
                                            <ext:Button ID="Button_31" runat="server" IconUrl="Styles/PNG/16/Stats.png" Scale="Small" IconAlign="Top" Text="质量" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="质量分析" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton31_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_32" runat="server" IconUrl="Styles/PNG/16/DialysisWater.png" Scale="Small" IconAlign="Top" Text="用水" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="用水纪录" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton32_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_33" runat="server" IconUrl="Styles/PNG/16/upload.png" Scale="Small" IconAlign="Top" Text="上传" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="数据上传" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton33_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                        </Items>
                                    </ext:Panel>
                                    <ext:Panel ID="ButtonGroup4" runat="server" Title="管理" TitleAlign="Center" Region="West" PaddingSpec="2 2 2 2">
                                        <Items>
                                            <ext:Button ID="Button_14" runat="server" IconUrl="Styles/PNG/16/Refresh.png" Scale="Small" IconAlign="Top" Text="模版" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="处方模版" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton14_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_15" runat="server" IconUrl="Styles/PNG/16/download.png" Scale="Small" IconAlign="Top" Text="领料" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="预估领料" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton15_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_42" runat="server" IconUrl="Styles/PNG/16/database.png" Scale="Small" IconAlign="Top" Text="库存" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="库存" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton42_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button45" runat="server" IconUrl="Styles/PNG/16/Dialysis.png" Scale="Small" IconAlign="Top" Text="设备" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="设备" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton45_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_43" runat="server" IconUrl="Styles/PNG/16/Tools.png" Scale="Small" IconAlign="Top" Text="系统" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="系统设置" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton43_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Button ID="Button_44" runat="server" IconUrl="Styles/PNG/16/Stop.png" Scale="Small" IconAlign="Top" Text="退出" ToggleGroup="ButtonGroup">
                                                <QTipCfg Text="退出" />
                                                <DirectEvents>
                                                    <Click OnEvent="ImageButton44_click" />
                                                </DirectEvents>
                                            </ext:Button>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:Panel>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Container7" runat="server">
                        <LayoutConfig>
                            <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                        </LayoutConfig>
                        <Items>
                            <ext:Container ID="Container3" runat="server" Width="1200">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="Top" Pack="End" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Label ID="Lab_patid" runat="server" Text="身分证号" Cls="head_label" Width="400" Hidden="true" />
                                    <ext:Label ID="Lab_sex" runat="server" Text="性别" Cls="head_label" Width="200" Hidden="true" />
                                    <ext:Label ID="Lab_docname" runat="server" Text="经治医师" Cls="head_label" Width="250" Hidden="true" />
                                    <ext:Label ID="Lab_name" runat="server" Text="" Cls="head_label" Height="20" Width="200" DefaultAlign="right" />
                                    <ext:Label ID="Lab_user_name" runat="server" Text="使用人" Cls="head_label" Width="150" DefaultAlign="right" />
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
            <%--中央部分--%>
            <ext:Panel id="Panel1" runat="server" Region="Center" Layout="FitLayout" Cls="Panellogo">
                <Items>
                    <ext:Panel id="Panel_Loader1" runat="server" Region="North" Hidden="true" Cls="Panellogo">
                        <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" Url="checkin/SchMenu.aspx">
                            <LoadMask ShowMask="true" Msg="读取中" />
                        </Loader>
                    </ext:Panel>
                    <ext:Panel id="Panel_Loader2" runat="server" Region="North" Hidden="false" AutoScroll="true" Cls="Panellogo">
                        <Items>
                            <ext:Container ID="Container4" runat="server">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                                </LayoutConfig>
                                <Items>                   
                                    <ext:GridPanel ID="GridPanel1" runat="server" Cls="x-grid-custom" Frame="true" Width="1200">
                                        <TopBar>
                                            <ext:Toolbar ID="Toolbar1" runat="server">
                                                <Items>
                                                    <ext:ComboBox ID="Text_Name" runat="server" FieldLabel="姓名" LabelWidth="50" Width="150" LabelCls="Text-blue" LabelAlign="Right"
                                                        DisplayField="patname" ValueField="patname" TypeAhead="false" HideTrigger="true" MinChars="1" TriggerAction="Query">
                                                        <ListConfig LoadingText="寻找中...">
                                                            <ItemTpl ID="ItemTpl11" runat="server">
                                                                <Html>
                                                                    <div>{patname}</div>
                                                                </html>
                                                            </ItemTpl>                                       
                                                        </ListConfig>
                                                        <Store>
                                                            <ext:Store ID="Store12" runat="server" AutoLoad="false">
                                                                <Proxy>
                                                                    <ext:AjaxProxy Url="~/Patinfos.ashx">
                                                                        <ActionMethods Read="POST" />
                                                                        <Reader>
                                                                            <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                                        </Reader>
                                                                    </ext:AjaxProxy>
                                                                </Proxy>
                                                                <Model>
                                                                    <ext:Model ID="Model12" runat="server">
                                                                        <Fields>
                                                                            <ext:ModelField Name="patic" />
                                                                            <ext:ModelField Name="patname" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                            </ext:Store>
                                                        </Store>
                                                    </ext:ComboBox>
                                                    <ext:TextField ID="Text_ID" runat="server" FieldLabel="身份证号" LabelWidth="80" LabelAlign="Right" LabelCls="Text-blue" Width="250" /> 
                                                    <ext:SelectBox ID="Cbo_Gender" runat="server" FieldLabel="性别" LabelWidth="50" LabelAlign="Right" Width="130">
                                                        <Items>
                                                            <ext:ListItem Value="M" Text="男" />
                                                            <ext:ListItem Value="F" Text="女" />
                                                        </Items>
                                                    </ext:SelectBox> 
                                                    <ext:SelectBox ID="cbo_Status" runat="server" FieldLabel="转归情形" LabelWidth="80" LabelAlign="Right" Width="180">
                                                        <Items>
                                                            <ext:ListItem Value="1" Text="退出" />
                                                            <ext:ListItem Value="2" Text="肾移植" />
                                                            <ext:ListItem Value="3" Text="转出" />
                                                            <ext:ListItem Value="4" Text="死亡" />
                                                            <ext:ListItem Value="5" Text="转入" />
                                                        </Items>
                                                    </ext:SelectBox>   
                                                    <ext:Button ID="btn_QueryHistory" runat="server" Icon="Zoom" Text="历史病患" Width="100" MarginSpec="10 10 5 5" UI="Success">
                                                        <DirectEvents>
                                                            <Click OnEvent="btn_QueryHistory_Click">
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button ID="btn_QueryNow" runat="server" Icon="Script" Text="当前病患" Width="100" MarginSpec="10 10 5 5" UI="Info">
                                                        <DirectEvents>
                                                            <Click OnEvent="btn_QueryNow_Click">
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                    <ext:Button ID="BtnReset" runat="server" Icon="ArrowRotateAnticlockWise" Text="重置" Width="100" MarginSpec="10 10 5 5" UI="Warning">
                                                        <DirectEvents>
                                                            <Click OnEvent="BtnReset_Click">
                                                            </Click>
                                                        </DirectEvents>
                                                    </ext:Button>
                                                </Items>
                                            </ext:Toolbar>
                                        </TopBar>
                                        <Store>
                                            <ext:Store ID="Store3" runat="server" PageSize="15">
                                                <Model>
                                                    <ext:Model ID="Model3" runat="server">
                                                        <Fields>
                                                            <ext:ModelField Name="pat_id" />
                                                            <ext:ModelField Name="pif_name" />
                                                            <ext:ModelField Name="pif_sex" />
                                                            <ext:ModelField Name="pif_dob" />
                                                            <ext:ModelField Name="pat_ic" />
                                                            <ext:ModelField Name="txt_10" />
                                                            <ext:ModelField Name="FirstDate" />
                                                            <ext:ModelField Name="InfoDate" />
                                                            <ext:ModelField Name="txt_101" />
                                                            <ext:ModelField Name="opt_52" />
                                                            <ext:ModelField Name="info_date" />                                                      
                                                            <ext:ModelField Name="pif_docname" />
                                                        </Fields>
                                                    </ext:Model>
                                                </Model>
                                                <Reader>
                                                    <ext:ArrayReader />
                                                </Reader>
                                                <Sorters>
                                                    <ext:DataSorter Property="pat_id" Direction="ASC" />
                                                </Sorters>
                                        </ext:Store>
                                        </Store>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                        <ColumnModel ID="ColumnModel2" runat="server">
                                            <Columns>
                                                <ext:RowNumbererColumn ID="Column11" runat="server" Text="序" Width="60" />
                                                <ext:Column ID="Column12" runat="server" Text="姓名" DataIndex="pif_name" Width="80" />
                                                <ext:Column ID="Column13" runat="server" Text="性别" DataIndex="pif_sex" Width="60" />                                            
                                                <ext:Column ID="Column14" runat="server" Text="出生日期" DataIndex="pif_dob" Width="100" />
                                                <ext:Column ID="Column15" runat="server" Text="身份证号" DataIndex="pat_ic" Width="170" />                                           
                                                <ext:Column ID="Column16" runat="server" Text="血/腹" DataIndex="txt_10" Width="60" />
                                                <ext:Column ID="Column17" runat="server" Text="首次透析日期" DataIndex="FirstDate" Width="115" />
                                                <%--<ext:Column ID="Column18" runat="server" Text="本院透析日期" DataIndex="InfoDate" Width="115" />--%>
                                                <ext:Column ID="Column19" runat="server" Text="生化指标" DataIndex="txt_101" Width="90" RightCommandAlign="false">
                                                    <Commands>
                                                        <ext:ImageCommand Icon="ChartLine" CommandName="BiochemicalIndicators" >
                                                            <ToolTip Text="超过或低于标准参考值" />
                                                        </ext:ImageCommand>
                                                    </Commands>
                                                    <PrepareCommand Fn="prepareCellCommand" />
                                                    <DirectEvents>
                                                        <Command OnEvent="BioIndicators">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="pat_id" Value="record.data.pat_id" Mode="Raw" />
                                                            </ExtraParams>
                                                        </Command>
                                                    </DirectEvents>
                                                </ext:Column>
                                                <ext:Column ID="Column20" runat="server" Text="转归情形" DataIndex="opt_52" Width="90" />
                                                <ext:Column ID="Column21" runat="server" Text="转归日期" DataIndex="info_date" Width="100" />
                                                <ext:Column ID="Column22" runat="server" Text="耗材" Width="60" RightCommandAlign="false">
                                                    <Commands>
                                                        <ext:ImageCommand CommandName="BiochemicalIndicators" Icon="BoxError">
                                                            <ToolTip Text="当月使用药品耗材查询" />
                                                        </ext:ImageCommand> 
                                                    </Commands>
                                                    <PrepareCommand Fn="prepareCellCommand" />
                                                        <DirectEvents>
                                                        <Command OnEvent="Dialysis_13">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="pat_ic" Value="record.data.pat_ic" Mode="Raw">
                                                                </ext:Parameter>
                                                                <ext:Parameter Name="pif_name" Value="record.data.pif_name" Mode="Raw">
                                                                </ext:Parameter>
                                                            </ExtraParams>
                                                        </Command>
                                                    </DirectEvents>
                                                </ext:Column>
                                                <ext:Column ID="Column23" runat="server" Text="经治医生" DataIndex="pif_docname"  Width="90" />
                                                <ext:Column ID="Column2" runat="server" Text="临床指引" RightCommandAlign="false" Flex="1">
                                                   <Commands>
                                                        <ext:ImageCommand CommandName="DoctorHelp" Icon="BookRed">
                                                            <ToolTip Text="临床小帮手" />
                                                        </ext:ImageCommand> 
                                                    </Commands>                                                
                                                    <PrepareCommand Fn="prepareCellCommand" />
                                                    <DirectEvents>
                                                        <Command OnEvent="Dialysis_help">
                                                            <ExtraParams>
                                                                <ext:Parameter Name="pat_ic" Value="record.data.pat_ic" Mode="Raw" />
                                                                <ext:Parameter Name="pif_name" Value="record.data.pif_name" Mode="Raw" />
                                                            </ExtraParams>
                                                        </Command>
                                                    </DirectEvents>
                                                </ext:Column>
                                            </Columns>
                                        </ColumnModel>
                                        <Plugins>
                                            <ext:BufferedRenderer ID="BufferedRenderer2" runat="server" />
                                        </Plugins>
                                        <View>
                                            <ext:GridView ID="GridView2" runat="server" TrackOver="false" />
                                        </View>
                                        <SelectionModel>
                                            <ext:RowSelectionModel ID="RowSelectionModel2" runat="server" Mode="Single" >
                                                <DirectEvents>
                                                    <Select OnEvent="Select_PatInfo">
                                                        <EventMask ShowMask="true" Msg="处理中….." Target="CustomTarget" CustomTarget="#{pnlTableLayout}" />                                                        
                                                        <ExtraParams>
                                                            <ext:Parameter Name="pat_ic" Value="record.data.pat_ic" Mode="Raw" />
                                                            <ext:Parameter Name="pif_name" Value="record.data.pif_name" Mode="Raw" />
                                                            <ext:Parameter Name="pif_sex" Value="record.data.pif_sex" Mode="Raw" />
                                                            <ext:Parameter Name="pif_docname" Value="record.data.pif_docname" Mode="Raw" />
                                                            <ext:Parameter Name="pat_id" Value="record.data.pat_id" Mode="Raw" />
                                                        </ExtraParams>
                                                    </Select>
                                                </DirectEvents>
                                            </ext:RowSelectionModel>
                                        </SelectionModel>      
                                        <BottomBar>
                                            <ext:PagingToolbar ID="PagingToolbar1" runat="server" StoreID="Store3" />
                                        </BottomBar>
                                    </ext:GridPanel>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                    <%--病患报到--%>
                    <ext:Panel id="Panel_Loader3" runat="server" Region="North" Hidden="false" AutoScroll="true" Cls="Panellogo">
                        <Items>
                            <ext:Container ID="Container5" runat="server">
                                <LayoutConfig>
                                    <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                                </LayoutConfig>
                                <Items>
                                    <ext:Panel ID="Panel_Left" runat="server" Title="病患报到" Region="North" AutoScroll="true" Header="false" Cls="Panellogo">
                                        <Items>
                                            <ext:Container ID="Container6" runat="server">
                                                <LayoutConfig>
                                                    <ext:HBoxLayoutConfig Align="StretchMax" Pack="Center" />
                                                </LayoutConfig>
                                                <Items>
                                                    <ext:GridPanel ID="grdBED_LIST" runat="server" Cls="x-grid-custom" Width="1200">
                                                        <TopBar>
                                                            <ext:Toolbar ID="Toolbar3" runat="server">
                                                                <Items>
                                                                    <ext:Label ID="Label_Date" runat="server" Text="日期" ColumnWidth=".2" />
                                                                    <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="150">
                                                                        <DirectEvents>
                                                                            <Select OnEvent = "cboFLOOR_Click" />
                                                                        </DirectEvents>
                                                                    </ext:SelectBox>
                                                                    <ext:SelectBox ID="cboAREA" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="150">
                                                                        <DirectEvents>
                                                                            <Select OnEvent="cboAREA_Click" />
                                                                        </DirectEvents>
                                                                    </ext:SelectBox>
                                                                    <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="150">
                                                                        <Items>
                                                                            <ext:ListItem Value="001" Text="上午" />
                                                                            <ext:ListItem Value="002" Text="下午" />
                                                                            <ext:ListItem Value="003" Text="晚班" />
                                                                        </Items>
                                                                        <DirectEvents>
                                                                            <Select OnEvent = "cboTIME_Click" />
                                                                        </DirectEvents>
                                                                    </ext:SelectBox> 
                                                                    <ext:ComboBox ID="cb_patlist" runat="server" FieldLabel="姓名" LabelWidth="60" IndicatorText="*" IndicatorCls="emptyColor" 
                                                                        LabelAlign="Right" DisplayField="patname" ValueField="patname" 
                                                                        TypeAhead="false" HideBaseTrigger="true" PageSize="10" MinChars="1" TriggerAction="Query"
                                                                        PaddingSpec="2 10 2 2" EmptyText="可直接进行刷卡" EmptyCls="emptyColor">
                                                                        <Store>
                                                                            <ext:Store ID="Store4" runat="server" AutoLoad="true">
                                                                                <Proxy>
                                                                                    <ext:AjaxProxy Url="Patinfos.ashx">
                                                                                        <ActionMethods Read="POST" />
                                                                                        <Reader>
                                                                                            <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                                                        </Reader>
                                                                                    </ext:AjaxProxy>
                                                                                </Proxy>
                                                                                <Model>
                                                                                    <ext:Model ID="Model4" runat="server">
                                                                                        <Fields>
                                                                                            <ext:ModelField Name="patic" />
                                                                                            <ext:ModelField Name="patname" />
                                                                                        </Fields>
                                                                                    </ext:Model>
                                                                                </Model>
                                                                            </ext:Store>
                                                                        </Store>
                                                                        <ListConfig LoadingText="寻找中...">
                                                                            <ItemTpl ID="ItemTpl2" runat="server">
                                                                                <Html>
                                                                                    <div>
                                                                                        <h1>{patname}</h1>
                                                                                    </div>
                                                                                </html>
                                                                            </ItemTpl>
                                                                        </ListConfig>
                                                                    </ext:ComboBox>
                                                                    <ext:Button ID="BtnSearch" runat="server" Icon="FolderExplore" IconAlign="Left" Text="查询" Padding="5" UI="Success">
                                                                        <DirectEvents>
                                                                            <Click OnEvent="BtnSearch_Click" />
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                    <ext:Button ID="btnPrint" runat="server" Icon="PrinterColor" IconAlign="Left" Text="打印" Padding="5" UI="Success">                                                                        
                                                                        <DirectEvents>
                                                                            <Click OnEvent="OnbtnPrint_Click">
                                                                                <EventMask ShowMask="true" />
                                                                            </Click>
                                                                        </DirectEvents>
                                                                    </ext:Button>
                                                                </Items>
                                                            </ext:Toolbar>
                                                        </TopBar>
                                                        <Store>
                                                            <ext:Store ID="Store5" runat="server" OnReadData="REFRESH_BED" >
                                                                <Model>
                                                                    <ext:Model ID="Model5" runat="server" Name="recordlist2">
                                                                        <Fields>
                                                                            <ext:ModelField Name="AREA" Type="String" />
                                                                            <ext:ModelField Name="BED_NO" Type="String" />
                                                                            <ext:ModelField Name="MAC_MODEL" Type="String" />
                                                                            <ext:ModelField Name="MAC_TYPE" Type="String" />
                                                                            <ext:ModelField Name="MAC_STATE" Type="String" />
                                                                            <ext:ModelField Name="PERSON_NAME" Type="String" />
                                                                            <ext:ModelField Name="PERSON_IC" Type="String" />
                                                                            <ext:ModelField Name="PERSON_SEX" Type="String" />
                                                                            <ext:ModelField Name="PERSON_HEIGHT" Type="String" />
                                                                            <ext:ModelField Name="PERSON_WEIGHT" Type="String" />
                                                                            <ext:ModelField Name="img_url" Type="String" />
                                                                            <ext:ModelField Name="PERSON_STATE" Type="String" />
                                                                            <ext:ModelField Name="PERSON_ID" Type="String" />
                                                                        </Fields>
                                                                    </ext:Model>
                                                                </Model>
                                                                <Reader>
                                                                    <ext:ArrayReader />
                                                                </Reader>
                                                            </ext:Store>
                                                        </Store>
                                                        <ColumnModel ID="ColumnModel3" runat="server">
                                                            <Columns>
                                                                <ext:Column ID="Column8" runat="server" Text="IC" DataIndex="PERSON_IC" Width="70" Visible="false" />
                                                                <ext:Column ID="Column9" runat="server" Text="区" DataIndex="AREA" Width="50" />
                                                                <ext:Column ID="Column10" runat="server" Text="床号" DataIndex="BED_NO" Width="70" />
                                                                <ext:Column ID="Column24" runat="server" Text="透析器型号" DataIndex="MAC_MODEL" Width="200" />
                                                                <ext:Column ID="Column25" runat="server" Text="透析方式" DataIndex="MAC_TYPE" Width="70" />
                                                                <ext:Column ID="Column26" runat="server" Text="状态" DataIndex="MAC_STATE" Width="90" />
                                                                <ext:Column ID="Column27" runat="server" Text="姓名" DataIndex="PERSON_NAME" Width="130" />
                                                                <ext:Column ID="Column28" runat="server" Text="性别" DataIndex="PERSON_SEX" Width="70" />
                                                                <ext:Column ID="Column29" runat="server" Text="身高" DataIndex="PERSON_HEIGHT" Width="80" />
                                                                <ext:Column ID="Column30" runat="server" Text="体重" DataIndex="PERSON_WEIGHT" Width="80" /> 
                                                                <ext:TemplateColumn ID="TemplateColumn1" runat="server" Text="已报到" DataIndex="img_url" TemplateString="<img src='{img_url}'/>" Width="80" />
                                                                <ext:TemplateColumn ID="TemplateColumn2" runat="server" Text="开机" DataIndex="PERSON_STATE" TemplateString="<img src='{PERSON_STATE}'/>" Flex="1" />
                                                            </Columns>
                                                        </ColumnModel>
                                                        <SelectionModel>
                                                            <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                                                                <DirectEvents>
                                                                    <Select OnEvent="RowSelect">
                                                                        <ExtraParams>
                                                                            <ext:Parameter Name="Values" Value="#{grdBED_LIST}.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                                                        </ExtraParams>
                                                                    </Select>
                                                                </DirectEvents>
                                                            </ext:RowSelectionModel>
                                                        </SelectionModel>
                                                    </ext:GridPanel>
                                                </Items>
                                            </ext:Container>
                                        </Items>
                                    </ext:Panel>
                                </Items>
                            </ext:Container>  
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <%--中央部分--%>
            <%--下方版權宣告部分--%>
            <ext:Panel ID="Panel_South" runat="server" Region="South" Cls="Panellogo">
                <Items>
                    <ext:Container ID="Containers11" runat="server" Layout="CenterLayout">
                        <Items>
                            <ext:Label ID="Label1" runat="server" Text="Copyright © 2015 DATACOM TECHNOLOGY CORP. All rights reserved  (V4.1 20160913)" Cls="Label_copyright1"></ext:Label>
                        </Items>
                    </ext:Container>
                    <ext:Container ID="Containers22" runat="server" Layout="CenterLayout">
                        <Items>
                            <ext:Label ID="Label2" runat="server" Text="本产品具备中华人民共和国专利第 3336979 号" Cls="Label_copyright2"></ext:Label>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
            <%--下方版權宣告部分--%>
        </Items>
    </ext:Viewport>
        <%--視窗部分--%>
        <ext:Window ID="Window1" runat="server" Title="病患查找" Width="600" Height="525" Modal="true" Hidden="true" CloseAction="Hide" UI="Success">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server"> 
                    <Items>
                        <ext:Panel ID="pnlTableLayout" runat="server" Header="false" AutoScroll="true" Cls="Panellogo">
                            <Items>                      
                                <ext:GridPanel ID="GridList" runat="server" Cls="x-grid-custom">
                                    <TopBar>
                                        <ext:Toolbar ID="Toolbar2" runat="server">
                                            <Items>
                                                <ext:ComboBox ID="SearchName" runat="server" FieldLabel="姓名" LabelWidth="30" Width="170" Cls="Text-blue" LabelAlign="Right"
                                                    DisplayField="patname" ValueField="patname" TypeAhead="false" HideTrigger="true" MinChars="1" TriggerAction="Query">                            
                                                    <ListConfig LoadingText="寻找中...">
                                                        <ItemTpl ID="ItemTpl1" runat="server">
                                                            <Html>
                                                                <div>{patname}</div>
                                                            </html>
                                                        </ItemTpl>
                                                    </ListConfig>
                                                    <Store>
                                                        <ext:Store ID="Store2" runat="server" AutoLoad="false">
                                                            <Proxy>
                                                                <ext:AjaxProxy Url="Patinfos.ashx">
                                                                    <ActionMethods Read="POST" />
                                                                    <Reader>
                                                                        <ext:JsonReader RootProperty="Patinfos" TotalProperty="total" />
                                                                    </Reader>
                                                                </ext:AjaxProxy>
                                                            </Proxy>
                                                            <Model>
                                                                <ext:Model ID="Model2" runat="server">
                                                                    <Fields>
                                                                        <ext:ModelField Name="patic" />
                                                                        <ext:ModelField Name="patname" />
                                                                    </Fields>
                                                                </ext:Model>
                                                            </Model>
                                                        </ext:Store>
                                                    </Store>
                                                </ext:ComboBox>
                                                <ext:TextField ID="SearchID" runat="server" FieldLabel="身份证号" LabelWidth="80" LabelAlign="Right" Width="270" />
                                                <ext:Button ID="btn_Query" runat="server" Text="查找" Icon="Find" Width="100" UI="Success">
                                                    <DirectEvents>
                                                        <Click OnEvent="btn_Query_Click" />
                                                    </DirectEvents>
                                                </ext:Button>
                                            </Items>
                                        </ext:Toolbar>
                                    </TopBar>
                                    <Store>
                                        <ext:Store ID="Store1" runat="server" PageSize="10">
                                        <Model>
                                            <ext:Model ID="Model1" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="pat_id" />
                                                    <ext:ModelField Name="pif_name" />
                                                    <ext:ModelField Name="pif_sex" />
                                                    <ext:ModelField Name="pif_dob" />
                                                    <ext:ModelField Name="pat_ic" />                                                     
                                                    <ext:ModelField Name="pif_docname" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                        <Reader>
                                            <ext:ArrayReader />
                                        </Reader>
                                        <Sorters>
                                            <ext:DataSorter Property="pat_id" Direction="ASC" />
                                        </Sorters>
                                    </ext:Store>
                                    </Store>                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                    
                                    <ColumnModel ID="ColumnModel1" runat="server">
                                        <Columns>
                                            <ext:RowNumbererColumn ID="Column1" runat="server" Text="序" Width="50" />
                                            <ext:Column ID="Column3" runat="server" Text="姓名" DataIndex="pif_name" Width="80" />
                                            <ext:Column ID="Column4" runat="server" Text="性别" DataIndex="pif_sex" Width="60" />                                            
                                            <ext:Column ID="Column5" runat="server" Text="出生日期" DataIndex="pif_dob" Width="110" />
                                            <ext:Column ID="Column6" runat="server" Text="身份证号" DataIndex="pat_ic" Width="190" />
                                            <ext:Column ID="Column7" runat="server" Text="经治医生" DataIndex="pif_docname" Region="Center" Width="100" />
                                        </Columns>
                                    </ColumnModel>
                                    <Plugins>
                                        <ext:BufferedRenderer ID="BufferedRenderer1" runat="server" />
                                    </Plugins>
                                    <View>
                                        <ext:GridView ID="GridView1" runat="server" TrackOver="false" />
                                    </View>
                                    <SelectionModel>
                                        <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">                                
                                            <DirectEvents>
                                                <Select OnEvent="Dialysis_detail">
                                                    <EventMask ShowMask="true" Msg="处理中….." Target="CustomTarget" CustomTarget="#{pnlTableLayout}" />
                                                    <ExtraParams>
                                                        <ext:Parameter Name="Values" Value="#{GridList}.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                                    </ExtraParams>
                                                </Select>
                                            </DirectEvents>
                                        </ext:RowSelectionModel>
                                    </SelectionModel> 
                                    <BottomBar>
                                        <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store1" />
                                    </BottomBar>
                                </ext:GridPanel>
                            </Items>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
            </Items>
            <DirectEvents>
                <BeforeClose OnEvent="Win_Close" />
            </DirectEvents>
        </ext:Window>
        <ext:Window ID="Window2" runat="server" Title="生化指标" Width="600" Height="400" Modal="true" AutoRender="false" Hidden="true">
            <Loader ID="Loader2" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
        <ext:Window ID="Window3" runat="server" Title="临床小帮手" Width="600" Height="700" Modal="true" AutoRender="false" Hidden="true">
            <Loader ID="Loader3" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
        <ext:Window ID="Window4" runat="server" Title="" Width="770" Height="600" Modal="true" AutoRender="false" Hidden="true">
            <Loader ID="Loader4" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
        <ext:Window ID="Window5" runat="server" Title="當月藥品耗材使用明细" Width="600" Height="500" Modal="true" AutoRender="false" Hidden="true">
            <Loader ID="Loader5" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
        <ext:Window ID="PrintWindow" runat="server" Title="" Width="900" Height="700" Modal="true" AutoRender="false" Hidden="true">
            <Loader ID="Loader6" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true" >
                <LoadMask ShowMask="true" />
            </Loader>
        </ext:Window>
        <%--視窗部分--%>
        <%--<ext:ToolTip runat="server" Target="={#{GridPanel1}.getView().el}" Delegate="={#{GridPanel1}.getView().itemSelector}" TrackMouse="true" 
            Html="<img src='patimages/20130529b4c67e94ae.jpg' style='margin:1px solid grey;padding:1px;height:100px;'/>" />--%>
    </form>
</body>
</html>
