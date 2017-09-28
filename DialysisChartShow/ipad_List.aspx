<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ipad_List.aspx.cs" Inherits="Dialysis_Chart_Show.ipad_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化系统-平版</title>
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
            height: 36px !important;
            font-size: 24px;
            line-height: 24px; 
        }
        
        /*文字框頭 對齊右*/
        .x-form-item-label-right
        {
            font-size: 24px;
            color: White; 
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
        
        .x-panel-default .x-autocontainer-innerCt
        {
            background: -moz-linear-gradient(top,  #1e5799 0%, #2989d8 100%, #207cca 100%, #7db9e8 100%); /* FF3.6-15 */
            background: -webkit-linear-gradient(top,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* Chrome10-25,Safari5.1-6 */
            background: linear-gradient(to bottom,  #1e5799 0%,#2989d8 100%,#207cca 100%,#7db9e8 100%); /* W3C, IE10+, FF16+, Chrome26+, Opera12+, Safari7+ */
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
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血液净化">
                    <Items>
                        <ext:Container ID="Container1" runat="server" Frame="true" Layout="ColumnLayout" Split="true">
                            <Items>
                                <ext:Container ID="Container11" runat="server" ColumnWidth=".42">
                                    <Items>
							            <ext:Image ID="Image1" runat="server" ImageUrl="Styles/mark1.jpg" Width="400" Height="95" />
                                    </Items>
                                </ext:Container>
                                <ext:Container ID="Container2" runat="server" ColumnWidth=".58">
							        <Items>
								        <ext:Container ID="Container21" runat="server" Layout="HBoxLayout" Padding="5">
								            <Items>
								                <ext:TextField ID="txtTIME" runat="server" FieldLabel="日期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="360" ReadOnly="true" />
								                <ext:TextField ID="txtWEEK" runat="server" FieldLabel="星期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="180" ReadOnly="true" />
								            </Items>
								        </ext:Container>
								        <ext:Container ID="Container22" runat="server" Layout="HBoxLayout" Padding="5">
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
								                <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue">
								                    <DirectEvents>
								                        <Select OnEvent = "cboFLOOR_Click" />
								                    </DirectEvents>
								                </ext:SelectBox>
								                <ext:SelectBox ID="cboAREA" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue">
								                    <DirectEvents>
								                        <Select OnEvent = "cboAREA_Click" />
								                    </DirectEvents>
								                </ext:SelectBox>
								            </Items>
								        </ext:Container>
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                        <ext:GridPanel ID="grdBED_LIST" runat="server" Header="true" Layout="AutoLayout">
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
				                                <ext:ModelField Name="PERSON_STATE" Type="String" />
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
				                    <ext:Column ID="Column1" runat="server" Text="床号" DataIndex="BED_NO" Width="75" />
				                    <ext:Column ID="Column2" runat="server" Text="厂牌" DataIndex="MAC_MODEL" Width="180" />
				                    <ext:Column ID="Column3" runat="server" Text="型号" DataIndex="MAC_TYPE" Width="100">
				                        <Renderer Fn="change" />
				                    </ext:Column>
				                    <ext:Column ID="Column4" runat="server" Text="状态" DataIndex="MAC_STATE" Width="80" />
				                    <ext:Column ID="Column5" runat="server" Text="姓名" DataIndex="PERSON_NAME" Width="150">
				                        <Renderer Fn="change2" />
				                    </ext:Column>
				                    <ext:Column ID="Column6" runat="server" Text="身份证号" DataIndex="PERSON_ID" Width="250" />
				                    <ext:Column ID="Column7" runat="server" Text="体重" DataIndex="PERSON_WEIGHT" Width="70" />
				                    <ext:Column ID="Column8" runat="server" Text="开机" DataIndex="PERSON_STATE" Width="70" Flex="1">
				                        <Renderer Fn="change2" />
				                    </ext:Column>
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
    </form>       
</body>
</html>
