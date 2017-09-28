<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_01.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化记录</title>
    <link href="Info.css" rel="stylesheet"/> 
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="Hidden1" runat="server" />
        <ext:ResourceManager ID="ResourceManager2" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血液净化记录" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:GridPanel ID="Grid_clinical1_nurse" runat="server" >
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="25">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="info_date" Type="String" />
                                                <ext:ModelField Name="data_1" Type="String" />
                                                <ext:ModelField Name="data_2" Type="String" />
                                                <ext:ModelField Name="data_3" Type="String" />
                                                <ext:ModelField Name="data_4" Type="String" />
                                                <ext:ModelField Name="data_5" Type="String" />
                                                <ext:ModelField Name="data_6" Type="String" />
                                                <ext:ModelField Name="data_7" Type="String" />
                                                <ext:ModelField Name="data_8" Type="String" />
                                                <ext:ModelField Name="data_9" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="common" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="date" runat="server" Text="日期.." DataIndex="info_date" Width="100" />
                                    <ext:Column ID="diagno" runat="server" Text="诊断" DataIndex="data_1" Width="80"/>
                                    <ext:Column ID="Column4" runat="server" Text="血管通路类型" DataIndex="data_2" Width="100"/>
                                    <ext:Column ID="Column2" runat="server" Text="透析前体重(Kg)" DataIndex="data_3" Width="110" Align="Right"/>
                                    <ext:Column ID="Column3" runat="server" Text="干体重(Kg)" DataIndex="data_4" Width="100" Align="Right"/>
                                    <ext:Column ID="Column1" runat="server" Text="目标定容量(Kg)" DataIndex="data_5" Width="110" Align="Right"/>
                                    <ext:Column ID="Column5" runat="server" Text="透析后体重(Kg)" DataIndex="data_6" Width="110" Align="Right"/>
                                    <ext:Column ID="Column6" runat="server" Text="肝素首量(mg)" DataIndex="data_7" Width="110" Align="Right"/>
                                    <ext:Column ID="Column7" runat="server" Text="追加量(mg/h)" DataIndex="data_8" Width="110" Align="Right"/>
                                    <ext:Column ID="Column8" runat="server" Text="低分子肝素(u)" DataIndex="data_9" Width="110" Align="Right"/>
                                </Columns>
                            </ColumnModel>
                            <SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
                                    <DirectEvents>
                                        <Select OnEvent="RowSelect">
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="#{Grid_clinical1_nurse}.getRowsValues({ selectedOnly : true })"
                                                    Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>
                            <View>
                                <ext:GridView ID="GridView1" runat="server" StripeRows="true">
                                </ext:GridView>
                            </View>
                            <%--<BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar1" runat="server" StoreID="Store1" />
                            </BottomBar>--%>
                        </ext:GridPanel>

                        <ext:Panel ID="Panel1" runat="server" Border="false" Header="false" ButtonAlign="Center" Layout = "ColumnLayout" Hidden = "true">
                            <Items>
                                <ext:SelectBox ID="SelectBox1" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "诊断">
                                    <Items>
                                        <ext:ListItem Text="慢性肾衰" Value="慢性肾衰" />
                                        <ext:ListItem Text="慢性肾功能衰竭" Value="慢性肾功能衰竭" />
                                        <ext:ListItem Text="急性肾衰" Value="急性肾衰" />
                                        <ext:ListItem Text="其它" Value="其它" />
                                    </Items>
                                </ext:SelectBox>
                                <ext:TextField ID="TextField2" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "床号">
                                </ext:TextField>
                                <ext:TextField ID="TextField3" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "机型">
                                </ext:TextField>
                                <ext:TextField ID="TextField4" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "血管通路类型">
                                </ext:TextField>
                                <ext:TextField ID="TextField5" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "透析前体重(Kg)">
                                </ext:TextField>
                                <ext:TextField ID="TextField6" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "干体重(Kg)">
                                </ext:TextField>
                                <ext:TextField ID="TextField7" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "目标定容量(Kg)">
                                </ext:TextField>
                                <ext:TextField ID="TextField8" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "透析后体重(Kg)">
                                </ext:TextField>
                                <ext:TextField ID="TextField9" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"  FieldLabel = "日期" ReadOnly = "true">
                                </ext:TextField>
                                <ext:TextField ID="TextField10" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "透析开始时间">
                                </ext:TextField>
                                <ext:TextField ID="TextField11" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "透析结束时间">
                                </ext:TextField>
                                <ext:TextField ID="TextField12" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "透析时间(小时)">
                                </ext:TextField>
                                <ext:TextField ID="TextField13" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "肝素首量(mg)">
                                </ext:TextField>
                                <ext:TextField ID="TextField14" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "追加量(mg/h)">
                                </ext:TextField>
                                <ext:TextField ID="TextField15" runat="server" ColumnWidth = ".5" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "低分子肝素(u)">
                                </ext:TextField>
                                <ext:Container ID="Container3" runat="server" ColumnWidth="1" Layout="ColumnLayout">
                                    <Items>
                                        <ext:Checkbox ID="Checkbox1" FieldLabel="拟用药" 
                                        LabelAlign="Right" BoxLabel="EPO" runat="server" ColumnWidth=".15" LabelWidth="125">
                                            <DirectEvents>
                                                <Change OnEvent="GetEvent1">
                                                    <EventMask ShowMask="true" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:Checkbox>
                                        <ext:TextField ID="TextCheckbox1" runat="server" ColumnWidth = ".05" LabelAlign="Left" PaddingSpec="4 10 0 2">
                                        </ext:TextField>
                                        <ext:Checkbox ID="Checkbox2" BoxLabel="左卡" runat="server" ColumnWidth=".04" LabelAlign="Left">
                                            <DirectEvents>
                                                <Change OnEvent="GetEvent2">
                                                    <EventMask ShowMask="true" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:Checkbox>
                                        <ext:TextField ID="TextCheckbox2" runat="server" ColumnWidth = ".05" LabelAlign="Left" PaddingSpec="4 10 0 2">
                                        </ext:TextField>
                                        <ext:Checkbox ID="Checkbox3" BoxLabel="铁剂" runat="server" ColumnWidth=".04" LabelAlign="Left">
                                            <DirectEvents>
                                                <Change OnEvent="GetEvent3">
                                                    <EventMask ShowMask="true" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:Checkbox>
                                        <ext:TextField ID="TextCheckbox3" runat="server" ColumnWidth = ".05" LabelAlign="Left" PaddingSpec="4 10 0 2">
                                        </ext:TextField>
                                        <ext:Checkbox ID="Checkbox4" BoxLabel="钙剂" runat="server" ColumnWidth=".04" LabelAlign="Left">
                                            <DirectEvents>
                                                <Change OnEvent="GetEvent4">
                                                    <EventMask ShowMask="true" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:Checkbox>
                                        <ext:TextField ID="TextCheckbox4" runat="server" ColumnWidth = ".05" LabelAlign="Left" PaddingSpec="4 10 0 2">
                                        </ext:TextField>
                                        <ext:Checkbox ID="Checkbox5" BoxLabel="抗菌素/其它" runat="server" ColumnWidth=".07" LabelAlign="Left">
                                            <DirectEvents>
                                                <Change OnEvent="GetEvent5">
                                                    <EventMask ShowMask="true" />
                                                </Change>
                                            </DirectEvents>
                                        </ext:Checkbox>
                                        <ext:TextField ID="TextCheckbox5" runat="server" ColumnWidth = ".05" LabelAlign="Left" PaddingSpec="4 10 0 2">
                                        </ext:TextField>
                                    </Items>
                                </ext:Container>

                                <ext:SelectBox ID="SelectBox3"  runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "透析液钙(mmol/L)">
                                    <Items>
                                        <ext:ListItem Text="1.5" Value="1.5" />
                                        <ext:ListItem Text="1.25" Value="1.25" />
                                        <ext:ListItem Text="2" Value="2" />
                                    </Items>
                                    <Listeners>
                                        <Change Handler="this.removeCls('blue'); this.addCls('height');" Single="true" />
                                    </Listeners>
                                </ext:SelectBox>
                                <ext:SelectBox ID="SelectBox4" runat="server" ColumnWidth=".25" LabelAlign="Right"
                                    LabelWidth="120" PaddingSpec="4 4 4 4" FieldLabel="置换方式">
                                    <Items>
                                        <ext:ListItem Text="前" Value="前" />
                                        <ext:ListItem Text="后" Value="后" />
                                    </Items>
                                    <Listeners>
                                        <Change Handler="this.removeCls('blue'); this.addCls('height');" Single="true" />
                                    </Listeners>
                                </ext:SelectBox>
                                <ext:TextField ID="TextField18" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"    FieldLabel = "置换量(L)">
                                </ext:TextField>
                                <ext:TextField ID="TextField19" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "穿刺者">
                                    <DirectEvents>
                                        <Focus OnEvent="text_click">
                                        </Focus>
                                    </DirectEvents>
                                </ext:TextField>
                                <ext:TextField ID="TextField20" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "上机">
                                     <DirectEvents>
                                        <Focus OnEvent="text_click">
                                        </Focus>
                                    </DirectEvents>
                                </ext:TextField>
                                <ext:TextField ID="TextField21" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "核对">
                                     <DirectEvents>
                                        <Focus OnEvent="text_click">
                                        </Focus>
                                    </DirectEvents>
                                </ext:TextField>
                                <ext:TextField ID="TextField17" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "下机">
                                     <DirectEvents>
                                        <Focus OnEvent="text_click">
                                        </Focus>
                                    </DirectEvents>
                                </ext:TextField>
                                <ext:TextField ID="TextField22" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "巡视者">
                                     <DirectEvents>
                                        <Focus OnEvent="text_click">
                                        </Focus>
                                    </DirectEvents>
                                </ext:TextField>
                                
                                <ext:TextArea ID="TextArea1" runat="server" ColumnWidth = "1" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "透析前症状及处理">
                                </ext:TextArea>
                                <ext:TextField ID="TextField23" runat="server" ColumnWidth = ".25" LabelAlign = "Right"  LabelWidth = "120" PaddingSpec = "4 4 4 4"   FieldLabel = "医生">
                                     <DirectEvents>
                                        <Focus OnEvent="text_click">
                                        </Focus>
                                    </DirectEvents>
                                </ext:TextField>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                            </Buttons>
                        </ext:Panel>
                    </Items>
                </ext:FormPanel>
                <ext:Window ID="Window1" runat="server" Title="请输入工号及密码" Height="230" Closable="false"
                    Width="350" BodyStyle="background-color: #fff;" BodyPadding="5" Modal="true" Hidden = "true" ButtonAlign = "Center">
                    <Items>
                        <ext:TextField ID="TextField1" runat="server" AnchorHorizontal="100%" FieldLabel="工号" ColumnWidth = "1" LabelAlign = "Right" Padding = "5" >
                        </ext:TextField>
                        <ext:TextField ID="TextField16" runat="server" InputType="Password" AnchorHorizontal="100%" FieldLabel="密码" ColumnWidth = "1" LabelAlign = "Right" Padding = "5" >
                            <Listeners>
                                <ValidityChange Handler="this.next().validate();" />
                                <Blur Handler="this.next().validate();" />
                            </Listeners>
                        </ext:TextField>
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
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
