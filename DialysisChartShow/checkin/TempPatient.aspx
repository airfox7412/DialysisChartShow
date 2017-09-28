<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="TempPatient.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.TempPatient" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病患基本资料</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <style type="text/css">
    .Panellogo .x-autocontainer-innerCt
    {
        /* Permalink - use to edit and share this gradient: http://colorzilla.com/gradient-editor/#1e5799+0,2989d8+100,207cca+100,7db9e8+100 */
        background: #1e5799; /* Old browsers */
        background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
        background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
        background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
        filter: progid:DXImageTransform.Microsoft.gradient( startColorstr='#1e5799', endColorstr='#7db9e8',GradientType=0 ); /* IE6-9 */ 
    }
    .x-form-text-default 
    {
        font-weight:normal;
    }
    .x-form-item-label-default
    {
        color:White;
    }
    .x-form-cb-label-default
    {
        color:White;
    }
    .x-form-radio-default
    {
        color:Yellow;
    }
    .x-form-checkbox-default
    {
        color:Red;
    }
    
    .Red-Panel .x-box-inner
    {
        /*background-color:Red;*/
        border: solid 1px red;
    }
    
    .Text-blue .x-form-item-label-text {
        color: blue;
    }
    </style>
</head>
<body>
    <form id="form1" runat="server">
        <ext:Hidden ID="pifid" runat="server" Text="" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Locale="zh-CN" Theme="Triton"/>
        <ext:Viewport ID="Viewport1" runat="server" Layout="BorderLayout">
            <Items>
                <ext:Panel ID="Panel_Left" runat="server" Title="病历基本资料" Region="North" Header="false" Cls="Panellogo" Height="700" Width="1100">
                    <Items>
                        <ext:Container ID="Container10" runat="server">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:FormPanel ID="FormPanel1" runat="server" Title="病历基本资料" X="0" Y="500" Frame="true" ButtonAlign="Center" Header="false" Width="1200" Height="660">
                                    <Items>
                                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout" PaddingSpec="10 5 10 5">
                                            <Items>
                                                <ext:DateField ID="info_date" runat="server" FieldLabel="记录日期" LabelAlign="Right" ColumnWidth=".25" Format="yyyy-MM-dd" IndicatorText="*" IndicatorCls="emptyColor" Flex="1" />
                                                <ext:Container ID="Container11" runat="server" Layout="HBoxLayout" ColumnWidth=".2" Flex="1">
                                                    <Items>
                                                        <ext:Radio ID="opt_s1" runat="server" FieldLabel="病患来源" Name="opt_s1" LabelAlign="Right" Width="175" BoxLabel="住院" Checked="true" />
                                                        <ext:Radio ID="opt_s2" runat="server" BoxLabel="门急诊" Name="opt_s1" LabelAlign="Right" Width="130" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:ComboBox ID="ComboBox_ins" runat="server" FieldLabel="保险类别" LabelWidth="95" ColumnWidth=".3" IndicatorText="*" IndicatorCls="emptyColor" LabelAlign="Right" Flex="1" />
                                                <ext:TextField ID="txt_18" runat="server" FieldLabel="其它费用" LabelWidth="95" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="0 70 0 10" Flex="1" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" PaddingSpec="10 5 10 5">
                                            <Items>
                                                <ext:TextField ID="txt_name" runat="server" FieldLabel="姓名" LabelWidth="100" LabelAlign="Right" ColumnWidth=".25" IndicatorText="*" IndicatorCls="emptyColor" Flex="1">
                                                    <DirectEvents>
                                                        <Change OnEvent="QueryPatient" />
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="txt_ic" runat="server" FieldLabel="身份证号码" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" IndicatorText="*" IndicatorCls="emptyColor" Flex="1">
                                                    <DirectEvents>
                                                        <Change OnEvent="QueryPatientIC" />
                                                    </DirectEvents>
                                                </ext:TextField>
                                                <ext:TextField ID="txt_mrn" runat="server" FieldLabel="IC卡号" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" Flex="1" />
                                                <ext:TextField ID="txt_insid" runat="server" FieldLabel="医保号" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" Flex="1" PaddingSpec="0 70 0 10" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" PaddingSpec="10 5 10 5">
                                            <Items>
                                                <ext:Container ID="Container31" runat="server" Layout="HBoxLayout" Width="275">
                                                    <Items>
                                                        <ext:Radio ID="opt_2_1" runat="server" FieldLabel="性别" BoxLabel="男" Name="opt_2" LabelAlign="Right" Width="170" />
                                                        <ext:Radio ID="opt_2_2" runat="server" BoxLabel="女" Name="opt_2" LabelWidth="50" LabelAlign="Right" Width="60" />
                                                        <ext:Button ID="Button2" runat="server" Icon="ScriptDelete" ToolTip="清空">
                                                            <DirectEvents>
                                                                <Click OnEvent="btn_Clear_Radio">
                                                                    <ExtraParams>
                                                                        <ext:Parameter Name="radname" Value="opt_2">
                                                                        </ext:Parameter>
                                                                    </ExtraParams>
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                    </Items>
                                                </ext:Container>
                                                <ext:DateField ID="txt_dob" runat="server" FieldLabel="出生年月" LabelAlign="Right" Format="yyyy-MM-dd" IndicatorText="*" IndicatorCls="emptyColor" Width="280">
                                                    <DirectEvents>
                                                        <Change OnEvent="ChangeBirthday" />
                                                    </DirectEvents>
                                                </ext:DateField>
                                                <ext:TextField ID="num_4" runat="server" FieldLabel="年龄" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" ReadOnly="true" Flex="1" />
                                                <ext:TextField ID="txt_height" runat="server" FieldLabel="身高" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" Flex="1" PaddingSpec="0 70 0 10" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container12" runat="server" Layout="HBoxLayout" PaddingSpec="0 55 0 5">
                                            <Items> 
                                                <ext:Container ID="Container14" runat="server" Layout="HBoxLayout" ColumnWidth=".2" Flex="1">
                                                    <Items>
                                                        <ext:Radio ID="opt_3_1" runat="server" FieldLabel="婚姻状态" Name="opt_3_1" LabelAlign="Right" Width="175" BoxLabel="已婚" Checked="true" />
                                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="未婚" Name="opt_3_1" LabelAlign="Right" Width="130" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container13" runat="server" Layout="HBoxLayout" Border="true" Cls="Red-Panel" Padding="10">
                                                    <Items> 
                                                        <ext:ComboBox ID="ComboBox_grp" runat="server" FieldLabel="血型" LabelWidth="50" LabelAlign="Right" Width="150" />
                                                        <ext:Label ID="Label1" runat="server" Text=" " Width="30" />
                                                        <ext:Checkbox ID="Checkbox_aids" runat="server" BoxLabel="HIV" BoxLabelCls="Red-CheckBox" Width="60">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip1" runat="server" Html="爱滋" UI="Success" />
                                                            </ToolTips>
                                                        </ext:Checkbox>
                                                        <ext:Checkbox ID="Checkbox_syphilis" runat="server" BoxLabel="RPR" BoxLabelCls="Red-CheckBox" Width="60">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip2" runat="server" Html="梅毒" UI="Success" />
                                                            </ToolTips>
                                                        </ext:Checkbox>
                                                        <ext:Checkbox ID="Checkbox_hbv" runat="server" BoxLabel="HBV" BoxLabelCls="Red-CheckBox" Width="60">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip3" runat="server" Html="乙肝" UI="Success" />
                                                            </ToolTips>
                                                        </ext:Checkbox>
                                                        <ext:Checkbox ID="Checkbox_hcv" runat="server" BoxLabel="HCV" BoxLabelCls="Red-CheckBox" Width="60">
                                                            <ToolTips>
                                                                <ext:ToolTip ID="ToolTip4" runat="server" Html="丙肝" UI="Success" />
                                                            </ToolTips>
                                                        </ext:Checkbox>
                                                        <ext:Checkbox ID="Checkbox_diabetic" runat="server" BoxLabel="其他传染病" BoxLabelCls="Red-CheckBox" Width="105" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container4" runat="server" Layout="HBoxLayout" PaddingSpec="10 5 10 5">
                                            <Items>
                                                <ext:ComboBox ID="cbo_h_type" runat="server" FieldLabel="血管通路类型" LabelWidth="100" LabelAlign="Right" ColumnWidth=".25" IndicatorText="*" IndicatorCls="emptyColor" Flex="1" />
                                                <ext:ComboBox ID="cbo_machine_model" runat="server" FieldLabel="透析器型号" LabelWidth="100" LabelAlign="Right" ColumnWidth=".25" IndicatorText="*" IndicatorCls="emptyColor" Flex="1" />
                                                <ext:ComboBox ID="cbo_hpack3" runat="server" FieldLabel="管路型号" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" IndicatorText="*" IndicatorCls="emptyColor" Flex="1" />
                                                <ext:ComboBox ID="cbo_docname" runat="server" FieldLabel="经治医生" LabelWidth="90" LabelAlign="Right" ColumnWidth=".25" IndicatorText="*" IndicatorCls="emptyColor" Flex="1" PaddingSpec="0 70 0 10" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container5" runat="server" Layout="HBoxLayout" PaddingSpec="10 5 10 5">
                                            <Items>
                                                <ext:TextField ID="txt_13" runat="server" FieldLabel="家属姓名" LabelWidth="100" LabelAlign="Right" ColumnWidth=".25" />
                                                <ext:TextField ID="txt_14" runat="server" FieldLabel="家属关系" LabelWidth="100" LabelAlign="Right" ColumnWidth=".25" />
                                                <ext:TextField ID="txt_15" runat="server" FieldLabel="家属电话" LabelWidth="90" LabelAlign="Right" ColumnWidth=".25" PaddingSpec="0 70 0 10" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" PaddingSpec="10 5 10 5">
                                            <Items>
                                                <ext:TextField ID="txt_8" runat="server" FieldLabel="工作单位" LabelWidth="100" LabelAlign="Right" ColumnWidth=".25" />
                                                <ext:TextField ID="txt_address" runat="server" FieldLabel="家庭住址" LabelWidth="90" LabelAlign="Right" ColumnWidth=".25" PaddingSpec="0 70 0 10" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container7" runat="server" Layout="HBoxLayout" PaddingSpec="10 5 10 5">
                                            <Items>
                                                <ext:TextField ID="txt_5" runat="server" FieldLabel="民族" LabelWidth="100" LabelAlign="Right" ColumnWidth=".25" Flex="1" />
                                                <ext:TextField ID="txt_10" runat="server" FieldLabel="联系电话" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" Flex="1" />
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="手机" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" Flex="1" />
                                                <ext:TextField ID="txt_12" runat="server" FieldLabel="邮编" LabelWidth="95" LabelAlign="Right" ColumnWidth=".25" Flex="1" PaddingSpec="0 70 0 10" />
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container8" runat="server" Layout="HBoxLayout" PaddingSpec="10 5 10 5">
                                            <Items>
                                                <ext:TextField ID="txt_9" runat="server" FieldLabel="住院科室" LabelAlign="Right" ColumnWidth=".25" />
                                                <ext:TextField ID="txt_6" runat="server" FieldLabel="住院号" LabelWidth="100" LabelAlign="Right" ColumnWidth=".25" />
                                                <ext:TextField ID="txt_7" runat="server" FieldLabel="透析号" LabelWidth="90" LabelAlign="Right" ColumnWidth=".25" PaddingSpec="0 70 0 10" />
                                            </Items>
                                        </ext:Container>
                                    <ext:Container ID="Container9" runat="server" Layout="HBoxLayout" Padding="10">
                                        <LayoutConfig>
                                            <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                                        </LayoutConfig>
                                        <Items>
                                            <ext:Button ID="BtnClear" runat="server" Icon="Add" Text="新增病患" UI="Warning" Width="100">
                                                <DirectEvents>
                                                    <Click OnEvent="Confirm">
                                                        <Confirmation ConfirmRequest="true" Title="提示" Message="您确定要清除，开始新增病患?" />
                                                    </Click>
                                                    <Click OnEvent="BtnClear_Click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Label ID="Label3" runat="server" Text=" " Width="30" />
                                            <ext:Button ID="BtnSave" runat="server" Icon="Disk" Text="保存" UI="Primary" Width="100">
                                                <DirectEvents>
                                                    <Click OnEvent="BtnSave_Click" />
                                                </DirectEvents>
                                            </ext:Button>
                                            <ext:Label ID="Label4" runat="server" Text=" " Width="30" />
                                            <ext:Button ID="BtnSearch" runat="server" Icon="ApplicationFormEdit" Text="修改" UI="Success" Width="100">
                                                <DirectEvents>
                                                    <Click OnEvent="BtnSearch_Click" />    
                                                </DirectEvents>
                                            </ext:Button>
                                        </Items>
                                    </ext:Container>
                                </Items>
                            </ext:FormPanel>
                        </Items>
                    </ext:Container>
                </Items>
            </ext:Panel>
                <%--視窗部分--%>
                <ext:Window ID="Window1" runat="server" Title="病患查找" Width="600" Height="525" Modal="true" Hidden="true" CloseAction="Hide" UI="Success">
                    <Items>
                        <ext:FormPanel ID="FormPanel2" runat="server"> 
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
                                                                        <ext:AjaxProxy Url="../Patinfos.ashx">
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
                                                        <ext:TextField ID="SearchID" runat="server" FieldLabel="身份证号" LabelWidth="80" LabelAlign="Right" Cls="Text-blue" Width="270" />
                                                        <ext:Button ID="btnQuery" runat="server" Text="查找" Icon="Find" Width="100">
                                                            <DirectEvents>
                                                                <Click OnEvent="btnQuery_Click" />
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
                <%--視窗部分--%>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
