<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="WebForm_Test.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.WebForm_Test" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html>
<head id="Head1" runat="server">
    <title>病患報到</title>
    <link href="henan.css" rel="stylesheet"/> 
</head>
<body>
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" Locale="zh-CN" />
        <ext:Hidden ID="sTIME" runat="server" />
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="sFLOOR" runat="server" />
        <ext:Hidden ID="sAREA" runat="server" />
        <ext:Hidden ID="sBED_NO" runat="server" />
        <ext:Hidden ID="dtFLOOR" runat="server" />
        <ext:Hidden ID="HIGHT" runat="server" />
        <ext:Hidden ID="WIDTH" runat="server" />
        <ext:Hidden ID="ROW_CNT" runat="server" />

    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout" MonitorResize="true">
        <Items>
            <ext:Panel ID="Panel10" runat="server" Title="病患報到" Region="North" Layout="HBoxLayout" Collapsible="true" Height="150">
                <Items>
                    <ext:Image ID="Image2" runat="server" ImageUrl="../Styles/visit300.jpg" Height="95" Width="250" />
                    <ext:Panel ID="Panel12" runat="server" Region="West" Border="false">
                        <Items>
                            <ext:Container ID="Container1" runat="server" Frame="true" Layout="HBoxLayout" Padding="1" >
                                <Items>
                                    <ext:TextField ID="txtDate" runat="server" FieldLabel="日期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly="true" Width="370" Padding="6" />
                                    <ext:TextField ID="txtWEEK" runat="server" FieldLabel="星期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly="true" Width="180" Padding="6" />
                                </Items>
                            </ext:Container>
                            <ext:Container ID="Container2" runat="server" Frame="true" Layout="HBoxLayout" Padding="1">
                                <Items>
                                    <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" Padding="6">
                                        <Items>
                                            <%--<ext:ListItem Value="001" Text="上午" />
                                            <ext:ListItem Value="002" Text="下午" />
                                            <ext:ListItem Value="003" Text="晚班" />--%>
                                        </Items>
                                        <DirectEvents>
                                            <Select OnEvent = "cboTIME_Click" />
                                        </DirectEvents>
                                    </ext:SelectBox>
                                    <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" Padding="6">
                                    </ext:SelectBox>
                                    <ext:SelectBox ID="cboAREA" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" Padding="6">
                                    </ext:SelectBox>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                    <ext:Panel ID="Panel13" runat="server" Region="West" Border="false">
                        <Items>
                            <ext:Container ID="Container4" runat="server" Frame="true" Layout="HBoxLayout" PaddingSpec="6 15 6 6">
                                <Items>
                                    <ext:Button ID="BtnLogin" runat="server" Icon="Lock" IconAlign="Left" Text="登出" Width="115" Height="50" Flat="false" Cls="big-y-text">
                                    </ext:Button>
                                </Items>
                            </ext:Container>
                        </Items>
                    </ext:Panel>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel_Left" runat="server" Title="清单" Region="West" Layout="FitLayout" Width="250" MinWidth="250" MaxWidth="400" Split="true" Collapsible="true" Padding="3">
                <TopBar>
                    <ext:Toolbar ID="Toolbar1" runat="server">
                        <Items>
                            <ext:SplitButton ID="SplitButton1" runat="server" Text="Menu Button" IconCls="add16">
                                <Menu>
                                    <ext:Menu ID="Menu1" runat="server">
                                        <Items>
                                            <ext:MenuItem ID="MenuItem1" runat="server" Text="Menu Button 1" />
                                        </Items>
                                    </ext:Menu>
                                </Menu>
                            </ext:SplitButton>
                            <ext:ToolbarSeparator />
                            <ext:SplitButton ID="SplitButton2" runat="server" Text="Cut" IconCls="add16">
                                <Menu>
                                    <ext:Menu ID="Menu2" runat="server">
                                        <Items>
                                            <ext:MenuItem ID="MenuItem2" runat="server" Text="Cut Menu Item" />
                                        </Items>
                                    </ext:Menu>
                                </Menu>
                            </ext:SplitButton>
                        </Items>
                    </ext:Toolbar>
                </TopBar>
                <Items>
                    <ext:Panel runat="server" ID="ImagePanel" Cls="images-view" Frame="true" Width="250">
                        <Items>
                            <ext:DataView ID="ImageView" runat="server" AutoScroll="true" MultiSelect="false" OverItemCls="x-view-over" TrackOver="true" ItemSelector="div.thumb-wrap" EmptyText="No images to display">
                                <Store>
                                    <ext:Store ID="Store1" runat="server" PageSize="7">
                                        <Model>
                                            <ext:Model ID="Model1" runat="server">
                                                <Fields>
                                                    <ext:ModelField Name="AREA" Type="String" />
                                                    <ext:ModelField Name="BED_NO" Type="String" />
                                                    <ext:ModelField Name="url" Type="String" />
                                                    <ext:ModelField Name="MAC_MODEL" Type="String" />
                                                    <ext:ModelField Name="MAC_TYPE" Type="String" />
                                                    <ext:ModelField Name="MAC_STATE" Type="String" />
                                                    <ext:ModelField Name="PERSON_NAME" Type="String" />
                                                    <ext:ModelField Name="PERSON_ID" Type="String" />
                                                    <ext:ModelField Name="PERSON_SEX" Type="String" />
                                                    <ext:ModelField Name="PERSON_HEIGHT" Type="String" />
                                                    <ext:ModelField Name="PERSON_WEIGHT" Type="String" />
                                                    <ext:ModelField Name="PERSON_STATE" Type="String" />
                                                </Fields>
                                            </ext:Model>
                                        </Model>
                                    </ext:Store>  
                                </Store>
                                <Tpl ID="Tpl1" runat="server">
                                    <Html>
										<tpl for=".">
											<div class="thumb-wrap" id="{name}">
												<table>
                                                    <tr>
                                                        <td><div class="thumb"><img src="{url}" title="{name}"></div></td>
												        <td><div class="info">
                                                            <span class="x-editable">{PERSON_NAME}</span>
                                                            <span class="x-editable">{PERSON_SEX}</span>
                                                            <span class="x-editable">{PERSON_HEIGHT},{PERSON_WEIGHT}</span>
                                                            <span class="x-editable">{MAC_TYPE}</span>
                                                            <span class="x-editable">{MAC_STATE}</span>
                                                            </div>
                                                        </td>
                                                    </tr>
                                                </table>
											</div>
										</tpl>
										<div class="x-clear"></div>        
									</Html>
                                </Tpl>            
                                <DirectEvents>
                                    <ItemClick OnEvent="RowItemSelect">
                                        <ExtraParams>
                                            <ext:Parameter Name="Values" Value="#{ImageView}.getRowsValues({ selectedOnly : true })" Mode="Raw" Encode="true" />
                                        </ExtraParams>
                                    </ItemClick>
                                </DirectEvents>             
                            </ext:DataView>
                        </Items>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server" StoreID="Store1" HideRefresh="true" Hidden="true" />
                        </BottomBar>
                    </ext:Panel>
                </Items>
            </ext:Panel> 
            <ext:Panel ID="Panel1" runat="server" Region="Center" Border="false">
                <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                    <LoadMask ShowMask="true" />
                </Loader>
                <Items>
                </Items>
            </ext:Panel> 
        </Items>
    </ext:Viewport>
</body>
</html>