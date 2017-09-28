<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="SchTable.aspx.cs" Inherits="Dialysis_Chart_Show.SchTable" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>病患排班</title>
    <%--<link href="css/dialysis.css" rel="stylesheet"/>--%>
    <script type="text/javascript">
        var onToggleButtonBeforeRender = function () {
            App.GridPanel1.getStore().getGroups().each(function (group) {
                App.ButtonToggleMenu.menu.add({
                    xtype: 'menucheckitem',
                    text: group.getGroupKey(),
                    handler: toggleGroup
                });
            });
        };

        var toggleGroup = function (item) {
            var groupName = item.text;
            if (item.checked) {
                App.GridPanel1.groupingFeature.expand(groupName, true);
            } else {
                App.GridPanel1.groupingFeature.collapse(groupName, true);
            }
        };

        var onGroupChange = function (store, grouper) {
            var grouped = store.isGrouped(),
                groupBy = grouper ? grouper.getProperty() : '',
                toggleMenuItems, len, i = 0;

            // Clear grouping button only valid if the store is grouped
            App.GridPanel1.down('[text=Clear Grouping]').setDisabled(!grouped);

            // Sync state of group toggle checkboxes
            if (grouped && groupBy === 'Area') {
                toggleMenuItems = App.GridPanel1.down('button[text=Toggle groups...]').menu.items.items;
                for (len = toggleMenuItems.length; i < len; i++) {
                    toggleMenuItems[i].setChecked(
                        App.GridPanel1.groupingFeature.isExpanded(toggleMenuItems[i].text)
                    );
                }
                App.GridPanel1.down('[text=Toggle groups...]').enable();
            } else {
                App.GridPanel1.down('[text=Toggle groups...]').disable();
            }
        };

        var onGroupCollapse = function (v, n, groupName) {
            if (!App.GridPanel1.down('[text=Toggle groups...]').disabled) {
                App.GridPanel1.down('menucheckitem[text=' + groupName + ']').setChecked(false, true);
            }
        };

        var onGroupExpand = function (v, n, groupName) {
            if (!App.GridPanel1.down('[text=Toggle groups...]').disabled) {
                App.GridPanel1.down('menucheckitem[text=' + groupName + ']').setChecked(true, true);
            }
        };
    </script>
    <style type="text/css">
        .x-grid-custom .x-grid-item TD {
            font-size : 14px;
        }

        .x-grid-custom .x-column-header {
            /*background : #718CA1 url(css/header_sprite.png) repeat scroll 0 bottom;*/
            background-color: #5ABCE0;
            font-size: 16px;
            border-left-color  : #6085A5;
            border-right-color : #728BA1;
        }

        .x-grid-custom .x-column-header-over {
            /*background : #ebf3fd url(css/header_sprite_over.png) repeat 0 bottom !important;*/
        }

        .x-grid-custom .x-column-header div {
            font-size  : 16px;
            color : white;
        }

        .x-grid-custom .company-link {
            color : #0E3D4F;
        }

        .x-grid-custom .x-column-header-trigger {
            /*background : #718CA1 url(css/grid3-hd-btn.png) no-repeat left center;*/
        }

        .x-grid-custom .x-grid-item-alt .x-grid-cell {
            background-color : #DAE2E8;
        }

        .x-grid-custom .x-grid-item-over .x-grid-cell {
            border-color : #728BA1;
            /*background   : url(css/row-over.png);*/
            background-color: Yellow;
        }

        .x-grid-custom .x-grid-item-selected .x-grid-cell {
            /*background   : url(css/row-selected.png) repeat-x scroll 0 0 #7BBBCF;*/
            background-color:Orange;
            border-color : #728BA1;
            border-style : solid;
        }

        .x-grid-custom .x-grid-item-selected TD,
        .x-grid-custom .x-grid-item-selected TD .company-link {
            color : #fff;
        }

        .x-grid-custom .x-toolbar .x-toolbar-text {
            color : #fff !important;
        }
/*
        .x-grid-custom .x-toolbar {
            background : url(css/toolbar-bg.png) repeat-x 0 0 !important;
        }

        .x-grid-custom .x-tbar-loading {
            background-image : url(css/refresh.gif) !important;
        }

        .x-grid-custom .x-tbar-page-first {
          background-image : url(css/page-first.gif) !important;
        }

        .x-grid-custom .x-tbar-page-last {
          background-image : url(css/page-last.gif) !important;
        }

        .x-grid-custom .x-tbar-page-next {
          background-image : url(css/page-next.gif) !important;
        }

        .x-grid-custom .x-tbar-page-prev {
          background-image : url(css/page-prev.gif) !important;
        }

        .x-grid-custom .x-paging-info {
          color : #fff;
        }
*/
    </style>
</head>
<body >
    <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN" Theme="Triton" />

    <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
        <Items>
            <ext:Panel ID="Panel1" runat="server" Title="选择" Region="West" Layout="HBoxLayout" Width="200" Split="true" Collapsible="true">
                <Items>
                    <ext:Container ID="Container1" runat="server" PaddingSpec="10 0 50 0">
                        <Items>
                            <ext:ComboBox ID="ComboBox1" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" >
                                <Items>
                                    <ext:ListItem Value="1" Text="1" />
                                    <ext:ListItem Value="2" Text="2" />
                                    <ext:ListItem Value="3" Text="3" />
                                </Items>
                            </ext:ComboBox>
                            <ext:ComboBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" >
                                <Items>
                                    <ext:ListItem Value="001" Text="上午" />
                                    <ext:ListItem Value="002" Text="下午" />
                                    <ext:ListItem Value="003" Text="晚班" />
                                </Items>
                            </ext:ComboBox>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
            <ext:Panel ID="Panel12" runat="server" Region="Center" Header="false">
                <Items>                      
                    <ext:GridPanel ID="GridPanel1" runat="server" Title="排班表" Header="false" Icon="ApplicationViewColumns" SortableColumns="false" Cls="x-grid-custom">
                        <Store>
                            <ext:Store ID="Store1" runat="server" GroupField="Area" PageSize="12">
                                <Model>
                                    <ext:Model ID="Model1" runat="server" IDProperty="BedType">
                                        <Fields>
                                            <ext:ModelField Name="Area" />
                                            <ext:ModelField Name="Machine" />
                                            <ext:ModelField Name="BedType" />
                                            <ext:ModelField Name="Week1" />
                                            <ext:ModelField Name="Week2" />
                                            <ext:ModelField Name="Week3" />
                                            <ext:ModelField Name="Week4" />
                                            <ext:ModelField Name="Week5" />
                                            <ext:ModelField Name="Week6" />
                                            <ext:ModelField Name="Week7" />
                                        </Fields>
                                    </ext:Model>
                                </Model>
                            </ext:Store>
                        </Store>
                        <ColumnModel ID="ColumnModel1" runat="server">
                            <Columns>
                                <ext:Column ID="Column1" runat="server" Text="床/机型" Align="Center" Sortable="false" DataIndex="BedType" Width="100" />
                                <ext:Column ID="Column3" runat="server" Text="周一" Align="Center" Sortable="false" DataIndex="Week1" Width="80" />
                                <ext:Column ID="Column4" runat="server" Text="周二" Align="Center" Sortable="false" DataIndex="Week2" Width="80" />
                                <ext:Column ID="Column5" runat="server" Text="周三" Align="Center" Sortable="false" DataIndex="Week3" Width="80" />
                                <ext:Column ID="Column6" runat="server" Text="周四" Align="Center" Sortable="false" DataIndex="Week4" Width="80" />
                                <ext:Column ID="Column7" runat="server" Text="周五" Align="Center" Sortable="false" DataIndex="Week5" Width="80" />
                                <ext:Column ID="Column8" runat="server" Text="周六" Align="Center" Sortable="false" DataIndex="Week6" Width="80" />
                                <ext:Column ID="Column9" runat="server" Text="周日" Align="Center" Sortable="false" DataIndex="Week7" Width="80" />
                                <ext:Column ID="Column10" runat="server" Text="机器品牌" Align="Center" Sortable="false" DataIndex="Machine" Width="150" />
                            </Columns>
                        </ColumnModel>
                        <Features>
                            <ext:Grouping ID="Grouping1" runat="server" HideGroupedHeader="true" GroupHeaderTplString="{name} / {rows.length} 床" />
                        </Features>
                        <TopBar>
                            <ext:Toolbar ID="Toolbar1" runat="server">
                                <Items>
                                    <ext:TextField ID="timeSec" runat="server" Text=""></ext:TextField>
                                    <ext:Button ID="ButtonToggleMenu" runat="server" Text="展开" DestroyMenu="true">
                                        <Menu>
                                            <ext:Menu ID="Menu1" runat="server" />
                                        </Menu>
                                        <Listeners>
                                            <BeforeRender Fn="onToggleButtonBeforeRender" />
                                        </Listeners>
                                    </ext:Button>
                                </Items>
                            </ext:Toolbar>
                        </TopBar>
                        <SelectionModel>
                            <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single" />
                        </SelectionModel>
                        <BottomBar>
                            <ext:PagingToolbar ID="PagingToolbar1" runat="server" /> 
                        </BottomBar>
                        <View>
                            <ext:GridView ID="GridView1" runat="server" ScrollOffset="2" StripeRows="true" TrackOver="true">
                                <Plugins>
                                    <ext:CellDragDrop ID="CellDragDrop1" runat="server" ApplyEmptyText="false" EnforceType="true" />
                                </Plugins>
                                <Listeners>
                                    <GroupCollapse Fn="onGroupCollapse" />
                                    <GroupExpand Fn="onGroupExpand" />
                                </Listeners>
                            </ext:GridView>
                        </View>
                    </ext:GridPanel>
                </Items>
            </ext:Panel>
        </Items>    
    </ext:Viewport>
</body>
</html>
