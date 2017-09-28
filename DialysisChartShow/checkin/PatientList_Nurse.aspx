<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientList_Nurse.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.PatientList_Nurse" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>血液净化系统-查询</title>
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
        <%--cell字型大小 自动--%>
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
            font-size: 28px; 
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
        
        .x-border-box .x-form-trigger
        {
            height: 36px !important;
            width: 17px !important;
            background-image: url("../Styles/trigger.png");
            cursor: pointer;
        }
        
        .x-form-checkbox, .x-form-radio
        {
            width: 26px;
            height: 26px;
            background-image: url("../Styles/che_btn.png");
        }
       
        .Big-Green
        {
            color: Green;
            font-size: 26px;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
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
        <ext:Panel ID="Panel1" runat="server" Region="North" Layout="HBoxLayout" Border="false" >
            <Items>
                <ext:Panel ID="Panel3" runat="server" Region="West" Width="300" Border="false" >
                    <Items>
                        <ext:Image ID="Image2" runat="server" ImageUrl="../Styles/NC2.jpg" Height="95" PaddingSpec="6 0 0 6" >
                        </ext:Image>
                    </Items>
                </ext:Panel>
                <ext:Panel ID="Panel4" runat="server" Region="Center" Border="false" >
                    <Items>
                        <ext:Container ID="Container1" runat="server" Frame="true" Layout="HBoxLayout" Padding="6" >
                            <Items>
                                <ext:DateField ID="txtTIME" runat="server" Vtype="daterange" FieldLabel="日期" EnableKeyEvents="true" Format="yyyy-MM-dd" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" Width="370" ReadOnly="false">
                                    <DirectEvents>
                                        <Change OnEvent="ChangeDate_Click"></Change>
                                    </DirectEvents>
                                </ext:DateField>
                                <ext:Label ID="GAP1" runat="server" Text="" Width="10" >
                                </ext:Label>
                                <ext:TextField ID="txtWEEK" runat="server" FieldLabel="星期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="180" ReadOnly="true" />
                                <ext:Label ID="GAP2" runat="server" Text="" Width="5" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Frame="true" Layout="HBoxLayout" Padding="6" >
                            <Items>
                                <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" >
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
                                <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" >
                                    <DirectEvents>
                                        <Select OnEvent = "cboFLOOR_Click" />
                                    </DirectEvents>
                                </ext:SelectBox>
                                <ext:Label ID="GAP4" runat="server" Text="" Width="10" />
                                <ext:SelectBox ID="cboAREA" FieldLabel="床区" runat="server" LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" >
                                    <DirectEvents>
                                        <Select OnEvent = "cboAREA_Click" />
                                    </DirectEvents>
                                </ext:SelectBox>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Panel>    
        <ext:Panel ID="Panel2" runat="server" Border="false" >
            <Items>
                <ext:GridPanel ID="grdBED_LIST" runat="server" Title="床位清单" Frame="true" AutoHeight="true" >
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
                                        <ext:ModelField Name="cln1_col15" Type="String" />
                                        <ext:ModelField Name="Cln1_col13" Type="String" />
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
                            <ext:Column ID="Column1" runat="server" Text="床号" DataIndex="BED_NO" Width="80" />
                            <ext:Column ID="Column2" runat="server" Text="厂牌" DataIndex="MAC_MODEL" Width="170" />
                            <ext:Column ID="Column3" runat="server" Text="型号" DataIndex="MAC_TYPE" Width="70" />
                            <ext:Column ID="Column4" runat="server" Text="状态" DataIndex="MAC_STATE" Width="90" />
                            <ext:Column ID="Column5" runat="server" Text="姓名" DataIndex="PERSON_NAME" Width="150" />
                            <ext:Column ID="Column6" runat="server" Text="肝素" DataIndex="cln1_col15" Width="270" />
                            <ext:Column ID="Column7" runat="server" Text="剂量" DataIndex="Cln1_col13" Width="150" />
                        </Columns>
                    </ColumnModel>
                </ext:GridPanel>
            </Items>
        </ext:Panel>
    </form>       
</body>
</html>
