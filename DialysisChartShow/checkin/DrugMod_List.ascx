<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DrugMod_List.ascx.cs" Inherits="Dialysis_Chart_Show.checkin.DrugMod_List" %>

    <ext:Window ID="DetailsWindow" runat="server" Icon="Group" Title="医嘱用药模板" Y="10" Width="700" Height="500" AutoShow="false" Modal="true" Hidden="true" Layout="Fit">
        <Items>
            <ext:Hidden ID="PationID" runat="server" />
            <ext:Hidden ID="DrugKind" runat="server" />
            <ext:Hidden ID="DocName" runat="server" />
            <ext:GridPanel ID="Grid_DrugTerm" runat="server" Region="Center" Height="500" UI="Primary">
                <Store>
                    <ext:Store ID="Store1" runat="server" PageSize="25" > 
                        <Model>
                            <ext:Model ID="Model1" runat="server" IDProperty="sid" >
                                <Fields>
                                    <ext:ModelField Name="sid" />
                                    <ext:ModelField Name="drg_name" />
                                    <ext:ModelField Name="intake" />
                                    <ext:ModelField Name="medway" />
                                    <ext:ModelField Name="freq" />
                                </Fields>
                            </ext:Model>
                        </Model>
                        <Reader>
                            <ext:ArrayReader />
                        </Reader>
                        <Sorters>
                            <ext:DataSorter Property="sid" Direction="ASC" />
                        </Sorters>
                        <Listeners>
                            <Write Handler="Ext.Msg.alert('成功', '保存完成！');" />
                        </Listeners>
                    </ext:Store>
                </Store>
                <ColumnModel ID="ColumnModel1" runat="server" >
                    <Columns>
                        <ext:Column ID="sid" runat="server" DataIndex="sid" Header="序號" Width="70" />
                        <ext:Column ID="drg_code" runat="server" DataIndex="drg_name" Header="医嘱名称" Width="200">
                            <Editor>
                                <ext:TextField ID="TextDrugName" runat="server" AllowBlank="false" />
                            </Editor>
                        </ext:Column>
                        <ext:Column ID="intake" runat="server" DataIndex="intake" Header="用量" Width="100">
                            <Editor>
                                <ext:TextField ID="TextIntake" runat="server" AllowBlank="false" />
                            </Editor>
                        </ext:Column>
                        <ext:Column ID="medway" runat="server" DataIndex="medway" Header="使用方法" Width="90">
                            <Editor>
                                <ext:ComboBox ID="cb_medway" runat="server" TypeAhead="true" SelectOnTab="true" />
                            </Editor>
                        </ext:Column>
                        <ext:Column ID="freq" runat="server" DataIndex="freq" Header="医嘱时间" Width="110">
                            <Editor>
                                <ext:ComboBox ID="cb_ordfreq" runat="server" TypeAhead="true" SelectOnTab="true" />
                            </Editor>
                        </ext:Column>
                    </Columns>
                </ColumnModel>
                <BottomBar>
                    <ext:PagingToolbar ID="PagingToolbar1" runat="server" DisplayInfo="false">
                        <Items>
                            <ext:Button ID="Button1" runat="server" Text="选择" Icon="Accept" StandOut="true" UI="Success">
                                <DirectEvents>
                                    <Click OnEvent="SaveSelRow">
                                        <EventMask ShowMask="true" />
                                    </Click>
                                </DirectEvents>
                            </ext:Button>
                            <ext:Button ID="CancelButton" runat="server" Text="取消" Icon="Cancel" StandOut="true" UI="Warning">
                                <Listeners>
                                    <Click Handler="this.up('window').hide();" />
                                </Listeners>
                            </ext:Button>
                        </Items>
                    </ext:PagingToolbar>
                </BottomBar>
                <SelectionModel>
                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Simple" />
                </SelectionModel>
            </ext:GridPanel>
        </Items>
    </ext:Window>
