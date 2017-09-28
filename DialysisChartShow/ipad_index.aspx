<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_index.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_index" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化系统-平版</title>
    <link href="css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
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
    </script>

    <style type="text/css">
        
        /*Grid Row*/
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 24px;
            line-height: 60px; 
        }
        .x-column-header-text-inner
        {
            font-size: 24px;
        }
        
        /*ComboBox Items*/
        .x-boundlist-item 
        { 
            font-size: 36px;
            line-height: 50px;
        }
        
         /*panel head 自动*/
        .x-panel-header-text {
            font-size: 20px;
            font-weight: bold;
            line-height: 21px;
            color: #04408c;
        }
        
        /*Grid Column*/
        .x-column-header-inner .x-column-header-text
        {
            font-size: 20px;
            line-height: 28px; 
        }
        
        /*文字框加大*/
        .x-border-box .x-form-text
        {
            height: 42px !important;
            font-size: 26px;
            font-weight:bold; 
            line-height: 26px; 
        }
        
        /*文字框頭 對齊右*/
        .x-form-item-label-right
        {
            font-size: 24px; 
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
            line-height: 35px;
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
        
        .x-border-box .x-form-trigger
        {
            height: 36px !important;
            width: 25px !important;
            /*background-image: url("Styles/trigger1.png");
            cursor: pointer;*/
        }
        
        .x-btn-inner-default-small
        {
            font-size:24px;
            line-height:30px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
        
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
		    <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
		        <Items>
                    <ext:FormPanel ID="FormPanel1" runat="server" BodyPadding="2" Title="病患资料" Icon="ApplicationEdit" AutoScroll="true" ButtonAlign="Center">
                        <Items>	
							<ext:Panel ID="Panel1" runat="server" Region="North" Layout="HBoxLayout" Border="false" >
								<Items>
								    <ext:Image ID="Image1" runat="server" ImageUrl="Styles/mark1.jpg" Width="400" Height="95" PaddingSpec="6 0 0 6" />
								    <ext:Panel ID="Panel4" runat="server" Region="Center" Border="false" >
								        <Items>
								            <ext:Container ID="Container1" runat="server" Frame="true" Layout="HBoxLayout" Padding="6">
								                <Items>
								                    <ext:TextField ID="txtTIME" runat="server" FieldLabel="日期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="370" ReadOnly="true" />
								                    <ext:Label ID="GAP1" runat="server" Text="" Width="10" >
								                    </ext:Label>
								                    <ext:TextField ID="txtWEEK" runat="server" FieldLabel="星期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="180" ReadOnly="true" />
								                    <ext:Label ID="GAP2" runat="server" Text="" Width="5" />
                                                    <ext:Label ID="LoginUser" runat="server" Text="" Width="100" Cls="label" />
								                </Items>
								            </ext:Container>
								            <ext:Container ID="Container2" runat="server" Frame="true" Layout="HBoxLayout" Padding="6">
								                <Items>
								                    <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue">
								                        <Items>
								                            <ext:ListItem Value="001" Text="上午" />
								                            <ext:ListItem Value="002" Text="下午" />
								                            <ext:ListItem Value="003" Text="晚班" />
								                        </Items>
								                        <DirectEvents>
								                            <Select OnEvent = "cboTIME_Click" />
								                        </DirectEvents>
								                    </ext:SelectBox>
								                    <ext:Label ID="GAP3" runat="server" Text="" Width="10" />
								                    <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue">
								                        <DirectEvents>
								                            <Select OnEvent = "cboFLOOR_Click" />
								                        </DirectEvents>
								                    </ext:SelectBox>
								                    <ext:Label ID="GAP4" runat="server" Text="" Width="10" />
								                    <ext:SelectBox ID="cboAREA" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue">
								                        <DirectEvents>
								                            <Select OnEvent = "cboAREA_Click" />
								                        </DirectEvents>
								                    </ext:SelectBox>
                                                    <ext:Button ID="BtnLogin" runat="server" Icon="Lock" IconAlign="Left" Text="登出" Width="115" Height="44" Flat="false" >
                                                        <DirectEvents>
                                                            <Click OnEvent="BtnLogout_Click" />
                                                        </DirectEvents>
                                                    </ext:Button>
								                </Items>
								            </ext:Container>
								        </Items>
								    </ext:Panel>
								</Items>
							</ext:Panel>
				            <ext:GridPanel ID="grdBED_LIST" runat="server" Header="false" cls="x-grid-custom" AutoScroll="true" EnableColumnMove="false" EnableColumnResize="false" EnableColumnHide="false" Scroll="Vertical">
				                <Store>
				                    <ext:Store ID="Store1" runat="server" OnReadData="REFRESH_BED" >
				                        <Model>
				                            <ext:Model ID="Model1" runat="server" Name="recordlist2">
				                                <Fields>
				                                    <ext:ModelField Name="BED_NO" Type="String" />
				                                    <ext:ModelField Name="MAC_MODEL" Type="String" />
				                                    <ext:ModelField Name="MAC_TYPE" Type="String" />
				                                    <ext:ModelField Name="MAC_STATE" Type="String" />
				                                    <ext:ModelField Name="PERSON_NAME" Type="String" />
				                                    <ext:ModelField Name="PERSON_ID" Type="String" />
				                                    <ext:ModelField Name="PERSON_WEIGHT" Type="String" />
				                                    <ext:ModelField Name="PERSON_IC" Type="String" />
				                                    <ext:ModelField Name="img_url" Type="String" />
				                                </Fields>
				                            </ext:Model>
				                        </Model>
				                        <Reader>
				                            <ext:ArrayReader />
				                        </Reader>
				                    </ext:Store>
				                </Store>
				                <ColumnModel ID="ColumnModel1" runat="server">
				                    <Columns>
							            <ext:Column ID="Column1" runat="server" Text="床号" DataIndex="BED_NO" Width="70" />
							            <ext:Column ID="Column2" runat="server" Text="厂牌" DataIndex="MAC_MODEL" Width="200" />
							            <ext:Column ID="Column3" runat="server" Text="型号" DataIndex="MAC_TYPE" Width="120">
							                <Renderer Fn="change" />
							            </ext:Column>
							            <ext:Column ID="Column4" runat="server" Text="状态" DataIndex="MAC_STATE" Width="80" />
							            <ext:Column ID="Column5" runat="server" Text="姓名" DataIndex="PERSON_NAME" Width="150" />
							            <ext:Column ID="Column6" runat="server" Text="身份证号" DataIndex="PERSON_ID" Width="220" />
							            <ext:Column ID="Column7" runat="server" Text="体重" DataIndex="PERSON_WEIGHT" Width="70" />
                                        <ext:TemplateColumn ID="TemplateColumn1" runat="server" Text="报到" DataIndex="img_url" TemplateString="<img src='{img_url}'/>" Width="80" />
				                    </Columns>
				                </ColumnModel>
				                <SelectionModel>
				                    <ext:RowSelectionModel ID="RowSelectionModel" runat="server" Mode="Single">
				                        <DirectEvents>
				                            <Select OnEvent="RowSelect">
				                                <ExtraParams>
				                                    <ext:Parameter Name="Values" Value="#{grdBED_LIST}.getRowsValues({ selectedOnly : true })"
				                                        Mode="Raw" Encode="true" />
				                                </ExtraParams>
				                            </Select>
				                        </DirectEvents>
				                    </ext:RowSelectionModel>
				                </SelectionModel>
				            </ext:GridPanel>
                    </Items>
                </ext:FormPanel>
        	</Items>
    	</ext:Viewport>
    </form>       
</body>
</html>
