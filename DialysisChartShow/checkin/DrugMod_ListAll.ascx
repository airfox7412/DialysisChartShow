<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DrugMod_ListAll.ascx.cs" Inherits="Dialysis_Chart_Show.checkin.DrugMod_ListAll" %>

    <script type="text/javascript">
            /* A header Checkbox of CheckboxSelectionModel deals with the current page only.
               This override demonstrates how to take into account all the pages.
               It works with local paging only. It is not going to work with remote paging.

            Ext.selection.CheckboxModel.override({
                selectAll: function(suppressEvent) {
                    var me = this,
                        selections = me.store.getAllRange(), // instead of the getRange call
                        i = 0,
                        len = selections.length,
                        start = me.getSelection().length;

                    me.suspendChanges();
                
                    for (; i < len; i++) {
                        me.doSelect(selections[i], true, suppressEvent);
                    }

                    me.resumeChanges();
                    if (!suppressEvent) {
                        me.maybeFireSelectionChange(me.getSelection().length !== start);
                    }
                },

                deselectAll: Ext.Function.createSequence(Ext.selection.CheckboxModel.prototype.deselectAll, function() {
                    this.view.panel.getSelectionMemory().clearMemory();
                }),

                updateHeaderState: function() {
                    var me = this,
                        store = me.store,
                        storeCount = store.getTotalCount(),
                        views = me.views,
                        hdSelectStatus = false,
                        selectedCount = 0,
                        selected, len, i;
            
                    if (!store.buffered && storeCount > 0) {
                        selected = me.view.panel.getSelectionMemory().selectedIds;
                        hdSelectStatus = true;
                        for (s in selected) {
                            ++selectedCount;
                        }

                        hdSelectStatus = storeCount === selectedCount;
                    }
            
                    if (views && views.length) {
                        me.toggleUiHeader(hdSelectStatus);
                    }
                }
            });
            */
    </script>
    <ext:Window ID="DetailsWindow" runat="server" Icon="Group" Title="医嘱用药模板" Width="600" Height="500" AutoShow="false" Modal="true" Hidden="true" Layout="FitLayout" UI="Success">
        <Items>
            <ext:Hidden ID="PationID" runat="server" />
            <ext:Hidden ID="DrugKind" runat="server" />
            <ext:Hidden ID="DocName" runat="server" />
            <ext:GridPanel ID="Grid_DrugTerm" runat="server" Region="Center" Height="450" UI="Success">
                <Store>
                    <ext:Store ID="DrugStore" runat="server" PageSize="10" > 
                        <Model>
                            <ext:Model ID="DrugModel" runat="server" IDProperty="sid" >
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
                        <ext:Column ID="sid" runat="server" DataIndex="sid" Header="序號" Width="50" />
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
                    <ext:CheckboxSelectionModel ID="CheckboxSelectionModel1" runat="server" Mode="Multi" />
                </SelectionModel>
            </ext:GridPanel>
        </Items>
    </ext:Window>
