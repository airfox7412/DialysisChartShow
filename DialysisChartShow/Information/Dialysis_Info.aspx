<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Info.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_Info" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>血液净化病例信息登记系统</title>
    <style type="text/css">
        #pnlTableLayout .x-table-layout {
            padding : 0px;
        }

        #pnlTableLayout .x-table-layout td {
            font-size : 11px;
            padding   : 0px;
            vertical-align : top;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton"/> 
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items> 
                <ext:Panel id="pnlTableLayout0" runat="server" Region="North" Header="false" Layout="TableLayout" >
                    <LayoutConfig>
                        <ext:TableLayoutConfig Columns="3" />
                    </LayoutConfig>
                    <Items>                    
                        <ext:Panel id="pnlTableLayout1" runat="server" Region="Center" Header="false" Layout="TableLayout">
                            <LayoutConfig>
                                <ext:TableLayoutConfig Columns="3" />
                            </LayoutConfig>
                            <Items>
                                <ext:Panel ID="Panel_1" runat="server" Header="false" Height="124" RowSpan="4">
                                    <Items>
                                        <ext:Image ID="Image1" runat="server" ImageUrl="../Styles/Menu/btn003a.png" Width="50" Height="124" /><%--工作流程--%>    
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_11" runat="server" Header="false" Width="120" Height="30"><%--排班--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton11" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001a.png" PressedImageUrl="../Styles/Menu/btn001aa.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_12" runat="server" Header="false" Width="120" Height="30"><%--報到--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton2" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001n.png" PressedImageUrl="../Styles/Menu/btn001nn.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_13" runat="server" Header="false" Width="240" Height="30" ColSpan="2"><%--血液淨化--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton13" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn002f.png" PressedImageUrl="../Styles/Menu/btn002ff.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_14" runat="server" Header="false" Width="240" Height="30" ColSpan="2"><%--治療計畫--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton14" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn002e.png" PressedImageUrl="../Styles/Menu/btn002ee.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_15" runat="server" Header="false" Width="240" Height="30" ColSpan="2"><%--領料--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton15" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn002a.png" PressedImageUrl="../Styles/Menu/btn002aa.png" />
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel> 
                        
                        <ext:Panel id="pnlTableLayout2" runat="server" Region="Center" Header="false" Layout="TableLayout">
                            <LayoutConfig>
                                <ext:TableLayoutConfig Columns="3" />
                            </LayoutConfig>
                            <Items>
                                <ext:Panel ID="Panel_2" runat="server" Header="false" Height="124" RowSpan="4">
                                    <Items>
                                        <ext:Image ID="Image2" runat="server" ImageUrl="../Styles/Menu/btn003b.png" Width="50" Height="124" /><%--查詢--%>    
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_21" runat="server" Header="false" Width="120" Height="30"><%--病患總攬--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton21" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001m.png" PressedImageUrl="../Styles/Menu/btn001mm.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_22" runat="server" Header="false" Width="120" Height="30"><%--淨化過程--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton22" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001l.png" PressedImageUrl="../Styles/Menu/btn001ll.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_23" runat="server" Header="false" Width="120" Height="30"><%--血透訊息--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton23" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001k.png" PressedImageUrl="../Styles/Menu/btn001kk.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_24" runat="server" Header="false" Width="120" Height="30"><%--實驗室檢查--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton24" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001j.png" PressedImageUrl="../Styles/Menu/btn001jj.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_25" runat="server" Header="false" Width="120" Height="30"><%--診斷信息--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton25" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001i.png" PressedImageUrl="../Styles/Menu/btn001ii.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_26" runat="server" Header="false" Width="120" Height="30"><%--病歷紀錄--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton26" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001h.png" PressedImageUrl="../Styles/Menu/btn001hh.png" />
                                    </Items>
                                </ext:Panel>
                                <ext:Panel ID="Panel_27" runat="server" Header="false" Width="240" Height="30" ColSpan="2"><%--血透評估表--%>
                                    <Items>
                                        <ext:ImageButton ID="ImageButton27" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn002b.png" PressedImageUrl="../Styles/Menu/btn002bb.png" />
                                    </Items>
                                </ext:Panel>
                            </Items>
                        </ext:Panel> 

                        <ext:Panel id="pnlTableLayout34" runat="server" Region="Center" Header="false" Layout="TableLayout" >
                            <LayoutConfig>
                                <ext:TableLayoutConfig Columns="1" />
                            </LayoutConfig>
                            <Items>                             
                                <ext:Panel id="pnlTableLayout3" runat="server" Region="Center" Header="false" Layout="TableLayout">
                                    <LayoutConfig>
                                        <ext:TableLayoutConfig Columns="3" />
                                    </LayoutConfig>
                                    <Items>
                                        <ext:Panel ID="Panel_3" runat="server" Header="false" Height="62" RowSpan="2">
                                            <Items>
                                                <ext:Image ID="Image3" runat="server" ImageUrl="../Styles/Menu/btn004a.png" Width="50" Height="62" /><%--統計--%>    
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel_31" runat="server" Header="false" Width="120" Height="30"><%--質量分析--%>
                                            <Items>
                                                <ext:ImageButton ID="ImageButton31" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001g.png" PressedImageUrl="../Styles/Menu/btn001gg.png" />
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel_32" runat="server" Header="false" Width="120" Height="30"><%--透析用水--%>
                                            <Items>
                                                <ext:ImageButton ID="ImageButton32" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001b.png" PressedImageUrl="../Styles/Menu/btn001bb.png" />
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel_33" runat="server" Header="false" Width="240" Height="30" ColSpan="2"><%--統計資料上傳--%>
                                            <Items>
                                                <ext:ImageButton ID="ImageButton33" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn002c.png" PressedImageUrl="../Styles/Menu/btn002cc.png" />
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel> 
                                
                                <ext:Panel id="pnlTableLayout4" runat="server" Region="Center" Header="false" Layout="TableLayout">
                                    <LayoutConfig>
                                        <ext:TableLayoutConfig Columns="3" />
                                    </LayoutConfig>
                                    <Items>
                                        <ext:Panel ID="Panel_4" runat="server" Header="false" Height="62" RowSpan="2">
                                            <Items>
                                                <ext:Image ID="Image4" runat="server" ImageUrl="../Styles/Menu/btn004b.png" Width="50" Height="62" /> <%--建檔--%>   
                                            </Items>
                                        </ext:Panel>
                                       <ext:Panel ID="Panel_41" runat="server" Header="false" Width="120" Height="30"><%--患者信息--%>
                                            <Items>
                                                <ext:ImageButton ID="ImageButton41" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001f.png" PressedImageUrl="../Styles/Menu/btn001ff.png" />
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel_42" runat="server" Header="false" Width="120" Height="30"><%--庫存--%>
                                            <Items>
                                                <ext:ImageButton ID="ImageButton42" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001d.png" PressedImageUrl="../Styles/Menu/btn001dd.png" />
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel_43" runat="server" Header="false" Width="120" Height="30"><%--系統設置--%>
                                            <Items>
                                                <ext:ImageButton ID="ImageButton43" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001e.png" PressedImageUrl="../Styles/Menu/btn001ee.png" />
                                            </Items>
                                        </ext:Panel>
                                        <ext:Panel ID="Panel_44" runat="server" Header="false" Width="120" Height="30"><%--退出--%>
                                            <Items>
                                                <ext:ImageButton ID="ImageButton44" runat="server" ToggleGroup="Group1" ImageUrl="../Styles/Menu/btn001c.png" PressedImageUrl="../Styles/Menu/btn001cc.png" />
                                            </Items>
                                        </ext:Panel>
                                    </Items>
                                </ext:Panel>     
                            </Items>
                        </ext:Panel> 

                        <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" ColumnWidth="1">
                            <Items>
                               <ext:Label ID="Lab_name" runat="server" Height="30" ColumnWidth=".15">
                                </ext:Label>
                                <ext:Label ID="Lab_patid" runat="server" Height="30" ColumnWidth=".28">
                                </ext:Label>
                                <ext:Label ID="Lab_sex" runat="server" Height="30" ColumnWidth=".15">
                                </ext:Label>
                                <ext:Label ID="Lab_info_pif_docname" runat="server" Height="30" ColumnWidth=".2">
                                </ext:Label>
                                <ext:Label ID="Lab_info_user_name" runat="server" Height="30" ColumnWidth=".2">
                                 </ext:Label>
                            </Items>
                        </ext:Container>                        
                        
                        <ext:Panel id="Panel_Loader" runat="server" Region="Center" Header="false" Cls="loader">
                            <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                                <LoadMask ShowMask="true" />
                            </Loader>
                        </ext:Panel>

                        <ext:Panel ID="Panel_South" runat="server" Region="South" Cls="Pnlstatusbar">
                            <Items>
                                <ext:Container ID="Containers1" runat="server" Layout="CenterLayout">
                                    <Items>
                                        <ext:Label ID="Label1" runat="server" Text="Copyright © 2015 DATACOM TECHNOLOGY CORP. All rights reserved  (V4.1 20160913)" Cls="Label_copyright1"></ext:Label>
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Containers2" runat="server" Layout="CenterLayout">
                                    <Items>
                                        <ext:Label ID="Label2" runat="server" Text="本产品具备中华人民共和国专利第 3336979 号" Cls="Label_copyright2"></ext:Label>
                                    </Items>
                                </ext:Container>
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
