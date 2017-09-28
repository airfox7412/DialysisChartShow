<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="appointment.aspx.cs" Inherits="Dialysis_Chart_Show.appointment" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="dtc.ico"> 
    <title>新排班表-轉檔</title>

    <script type="text/javascript">
        function getScreenResolution() {
            //alert("hello 1");
            PageMethods.setResolution(window.innerWidth, window.innerHeight);
            //alert(window.innerWidth + ',' + window.innerHeight);
            //alert("hello 2");
            Ext.getCmp('WIDTH').setValue(window.innerWidth);
            Ext.getCmp('HIGHT').setValue(window.innerHeight);
            //alert("hello 2");
        }

        var template = 'color:{0};';

        var change = function (value, meta, record, row, col) {
            switch (record.get("MAC_TYPE")) {
                case 'HD':
                    meta.style = Ext.String.format(template, "blue");
                    break;
                case 'HDF':
                    meta.style = Ext.String.format(template, "red");
                    break;
                default:
                    meta.style = Ext.String.format(template, "green");
            }

            return value;
        };

        var change2 = function (value, meta, record, row, col) {
            switch (record.get("PERSON_STATE")) {
                case 'A':
                    meta.style = Ext.String.format(template, "red");
                    break;
                case '开':
                    meta.style = Ext.String.format(template, "red");
                    break;
                case 'S':
                    meta.style = Ext.String.format(template, "black");
                    break;
                case "关":
                    meta.style = Ext.String.format(template, "black");
                    break;
                default:
                    meta.style = Ext.String.format(template, "blue");
            }

            return value;
        };
    </script>

    <style type="text/css">
       
        <%--ComboBox Items--%>
        .x-boundlist-item 
        { 
            font-size: 40px;
        }
         <%--panel head 自动--%>
        .x-panel-header-text {
            font-size: 20px;
            font-weight: bold;
            line-height: 21px;
            color: #04408c;
        }
        <%--cell字型大小  自动 ?--%>
        .x-grid-row .x-grid-cell { 
            font-size: 20px;
        }
        <%--Grid Row--%>
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 24px;
            line-height: 55px; 
        }
        <%--Grid Column--%>
        .x-column-header-inner .x-column-header-text
        {
            font-size: 20px;
            line-height: 28px; 
        }
        
        <%--文字框加大--%>
        .x-border-box .x-form-text
        {
            height: 36px !important;
            font-size: 28px; <%--x-large--%>
        }
        <%--文字框頭 對齊右--%>
        .x-form-item-label-right
        {
            font-size: 24px; <%--x-large--%>
        }
       
        .Text-blue .x-form-field
        {
            color: blue;
        }
        .Text-blue-H .x-form-field
        {
            height: 100px !important;
            color: blue !important;
        }
        
        .label .x-label-value
        {
            height: 35px !important;
            font-size: 32px; 
        }
        /* Text items */
        .big-text .x-toolbar-text {
            font-size: 24px;
        }

        /* Button text */
        .big-text .x-btn-default-toolbar-small .x-btn-inner {
            font-size: 14px;
        }
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        
        <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePageMethods="True" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Hidden ID="sDATE" runat="server" />
        <ext:Hidden ID="sTIME" runat="server" />
        <ext:Hidden ID="sWEEK" runat="server" />
        <ext:Hidden ID="sFLOOR" runat="server" />
        <ext:Hidden ID="sAREA" runat="server" />
        <ext:Hidden ID="sBED_NO" runat="server" />
        <ext:Hidden ID="dtFLOOR" runat="server" />
        <ext:Hidden ID="HIGHT" runat="server" />
        <ext:Hidden ID="WIDTH" runat="server" />
        <ext:Hidden ID="ROW_CNT" runat="server" />
        
        <ext:Viewport ID="Viewport1" runat="server" >
            <Items>
                <%--<ext:TextField ID="TextField1" runat="server" />
                <ext:TextField ID="TextField2" runat="server" />--%>
                         
                <ext:Panel ID="Panel1" runat="server" Region="North" Height="105" Layout="HBoxLayout" Border="false" >
                    <Items>
                        <ext:Panel ID="Panel3" runat="server" Region="West" Width="300" Border="false"  >
                            <Items>
                                <ext:Image ID="Image2" runat="server" ImageUrl="Styles/mark1.jpg" Height="95" PaddingSpec="6 0 0 6" >
                                    <%--<DirectEvents>
                                        <Click OnEvent="Hight_Click" />
                                    </DirectEvents>--%>
                                </ext:Image>
                            </Items>
                        </ext:Panel>
                        <ext:Panel ID="Panel4" runat="server" Region="Center" Border="false" >
                            <Items>
                                <ext:Container ID="Container1" runat="server" Frame="true" Layout="HBoxLayout" Padding="6" >
                                    <Items>
                                      <ext:TextField ID="txtMESSAGE"  runat="server" FieldLabel="訊息:" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="370" />
                                       <ext:TextField ID="txtDATE"  runat="server" FieldLabel="日期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="370" />
                                        <ext:TextField ID="txtTIME" runat="server" FieldLabel="日期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="370" Visible="false" />
                                        <ext:Label ID="GAP1" runat="server" Text="" Width="10" >
                                        </ext:Label>
                                        <ext:TextField ID="txtWEEK" runat="server" FieldLabel="星期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="180" >
                                            <Listeners>
                                                <KeyPress Fn="getScreenResolution()" />
                                            </Listeners>
                                        </ext:TextField>
                                        <ext:Label ID="GAP2" runat="server" Text="" Width="5" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2" runat="server" Frame="true" Layout="HBoxLayout" Padding="6" >
                                    <Items>
                                        <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue">
                                            <Items>
                                                <ext:ListItem Value="001" Text="上午" />
                                                <ext:ListItem Value="002" Text="下午" />
                                                <ext:ListItem Value="003" Text="晚班" />
                                            </Items>
                                            <DirectEvents>
                                                <%--<Select OnEvent = "cboTIME_Click" />--%>
                                            </DirectEvents>
                                        </ext:SelectBox>
                                        <ext:Label ID="GAP3" runat="server" Text="" Width="10" />
                                        <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue"  >
                                            <%--<Items>
                                                <ext:ListItem Value="03" Text="3楼" />
                                                <ext:ListItem Value="05" Text="5楼" />
                                            </Items>--%>
                                            <DirectEvents>
                                                 <%--<Select OnEvent = "cboFLOOR_Click" />--%>
                                            </DirectEvents>
                                        </ext:SelectBox>
                                        <ext:Label ID="GAP4" runat="server" Text="" Width="10" />
                                        <ext:SelectBox ID="cboAREA" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="250" Cls="Text-blue" Visible="false">
                                           <%-- <Items>
                                                <ext:ListItem Value="A" Text="A区" />
                                                <ext:ListItem Value="B" Text="B区" />
                                                <ext:ListItem Value="C" Text="C区" />
                                                <ext:ListItem Value="D" Text="D区" />
                                            </Items>--%>
                                            <DirectEvents>
                                                <%--<Select OnEvent = "cboAREA_Click" />--%>
                                            </DirectEvents>
                                        </ext:SelectBox>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                        
                    </Items>
                </ext:Panel>
                
                <ext:Panel ID="Panel2" runat="server" Region="Center" Title="床位清单" Border="false" AutoScroll="true" Visible="false" >
                    <Items>

                        
                        <%--<ext:GridPanel ID="grdBED_LIST" runat="server" Width="1000" Height="350" >--%>
                        <ext:GridPanel ID="grdBED_LIST" runat="server" Height2="1035" Width="970" AutoScroll="true"  >
                            

                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="Column1" runat="server" Text="床号" DataIndex="BED_NO" Width="70" />
                                    <%--<ext:TemplateColumn ID="TemplateColumn1" runat="server" DataIndex="" MenuDisabled="true" Text="床号 / 厂牌" Width="150" >
                                        <Template ID="Template1" runat="server">
                                            <Html>
						                        <tpl for=".">
							                        {BED_NO}<br />
								                    {MAC_MODEL}<br />
						                        </tpl>
					                        </Html>
                                        </Template>
                                    </ext:TemplateColumn>--%>
                                    <ext:Column ID="Column2" runat="server" Text="厂牌" DataIndex="MAC_MODEL" Width="170" />
                                    <ext:Column ID="Column3" runat="server" Text="型号" DataIndex="MAC_TYPE" Width="70" >
                                        <Renderer Fn="change" />
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" Text="状态" DataIndex="MAC_STATE" Width="90" />
                                    <ext:Column ID="Column5" runat="server" Text="姓名" DataIndex="PERSON_NAME" Width="150" >
                                      <Renderer Fn="change2" />
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" Text="身份证号" DataIndex="PERSON_ID" Width="270" />
                                    <%--<ext:TemplateColumn ID="TemplateColumn2" runat="server" DataIndex="" MenuDisabled="true" Text="姓名 / 身份证号" Width="280" >
                                        <Template ID="Template2" runat="server">
                                            <Html>
						                        <tpl for=".">
							                        {PERSON_NAME}<br />
								                    {PERSON_ID}<br />
						                        </tpl>
					                        </Html>
                                        </Template>
                                    </ext:TemplateColumn>--%>
                                    <ext:Column ID="Column7" runat="server" Text="体重" DataIndex="PERSON_WEIGHT" Width="70" />
                                    <ext:Column ID="Column8" runat="server" Text="开机" DataIndex="PERSON_STATE" Width="60" >
                                        <Renderer Fn="change2" />
                                    </ext:Column>
                                    
                                </Columns>
                            </ColumnModel>

                            <%--<SelectionModel>
                                <ext:CellSelectionModel ID="CellSelectionModel1" runat="server">
                                    <DirectEvents>
                                        <Select OnEvent="RowSelect" >
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="#{grdBED_LIST}.getRowsValues({ selectedOnly : true })"
                                                    Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Select>                        
                                    </DirectEvents>
                                </ext:CellSelectionModel>
                            </SelectionModel>--%>       


   
                            <%--<Plugins>
                                <ext:CellEditing ID="CellEditing2" runat="server" ClicksToEdit="1" />
                            </Plugins>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar" runat="server" Cls="big-text" StoreID="Store1"  />
                            </BottomBar>--%>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>

        <%-- DisplayMsg="当前是第{0}条-第{1}条，共{2}条" --%>
        
        <ext:TaskManager ID="TaskManager1" runat="server" Enabled="true" Visible="false" >
            <Tasks>
                <ext:Task TaskID="tskTIME" Interval="60000"  >
                    <DirectEvents>
                        <%--<Update OnEvent="Timer1_Timer" />--%> 
                    </DirectEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>
    </div>
    </form>
</body>
</html>
