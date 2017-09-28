<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_Schedul.aspx.cs" Inherits="Dialysis_Chart_Show.Dialysis_Schedul" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml"> 
<head id="Head1" runat="server">
    <link rel="shortcut icon" href="dtc.ico" /> 
    <title>血液净化系统-平版</title>

    <script type="text/javascript">
        //        var getSize = function () {
        //            var winW = 0, winH = 0;
        //            if (document.body && document.body.offsetWidth) {
        //                winW = document.body.offsetWidth;
        //                winH = document.body.offsetHeight;
        //            }
        //            if (document.compatMode == 'CSS1Compat' && document.documentElement && document.documentElement.offsetWidth) {
        //                winW = document.documentElement.offsetWidth;
        //                winH = document.documentElement.offsetHeight;
        //            }
        //            if (window.innerWidth && window.innerHeight) {
        //                winW = window.innerWidth;
        //                winH = window.innerHeight;
        //            }
        //            return { width: winW, height: winH };
        //        };

        //        window.onload = function () {
        //            var ww = getSize().width;
        //            var hh = getSize().height;
        //            alert(getSize().width + ',' + getSize().height);
        //            //Ext.getCmp('WIDTH').setValue(getSize().width);
        //            //Ext.getCmp('HIGHT').setValue(getSize().height);
        //        };

        function getScreenResolution() {
            //alert("hello 1");
            PageMethods.setResolution(window.innerWidth, window.innerHeight);
            //alert(window.innerWidth + ',' + window.innerHeight);
            //alert("hello 2");
            Ext.getCmp('WIDTH').setValue(window.innerWidth);
            Ext.getCmp('HIGHT').setValue(window.innerHeight);
            //alert("hello 2");
        }




        var prepareCommand = function (grid, command, record, row) {
            //if (record.get("sched_15D") != '') {
            //    command.hidden = true;
            //    alert("test1");
            //}
        }

        var prepareCommand14 = function (grid, command, record, row) {
            //if (record.get("sched_14D") != '') {
            //    command.hidden = true;
            //    alert("test2");
            //}
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
        <ext:Hidden ID="sNAME"   runat="server" />
        <ext:Hidden ID="spatic"   runat="server" />
        <ext:Hidden ID="sYEAR"   runat="server" />
        <ext:Hidden ID="sMON"   runat="server" />    
        <ext:Hidden ID="WFLOOR"   runat="server" />  
        <ext:Hidden ID="WAREA"   runat="server" />   
        <ext:Hidden ID="WYEAR"   runat="server" />     
        <ext:Hidden ID="WMON"   runat="server" />  
         <ext:Hidden ID="WNAME"   runat="server" /> 
         
        
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
                                        <ext:TextField ID="txtTIME" runat="server" FieldLabel="年月" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="370" visible="false"/>
                                        <ext:Label ID="GAP1" runat="server" Text="" Width="10" >
                                        </ext:Label>

                                        <ext:TextField ID="txtTIME1" runat="server" FieldLabel="年月" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="170"  Visible="false"/>
                                        <ext:Label ID="Label1" runat="server" Text="" Width="10"  Visible="false">
                                        </ext:Label>

                                         <ext:SelectBox ID="cboYEAR" FieldLabel="排程年月" runat="server"  LabelWidth="150" LabelAlign="Right" Width="260" Cls="Text-blue">
                                            <Items>
                                                <ext:ListItem Value="001" Text="2013" />
                                                <ext:ListItem Value="002" Text="2014" />
                                                <ext:ListItem Value="003" Text="2015" />
                                                <ext:ListItem Value="004" Text="2016" />
                                                <ext:ListItem Value="005" Text="2017" />
                                                <ext:ListItem Value="006" Text="2018" />
                                                <ext:ListItem Value="007" Text="2019" />
                                                <ext:ListItem Value="008" Text="2020" />  
                                                <ext:ListItem Value="009" Text="2021" /> 
                                                <ext:ListItem Value="010" Text="2022" /> 
                                                <ext:ListItem Value="011" Text="2023" />                                               
                                                <ext:ListItem Value="012" Text="2024" /> 
                                                <ext:ListItem Value="013" Text="2025" /> 
                                                <ext:ListItem Value="014" Text="2026" /> 
                                                <ext:ListItem Value="015" Text="2027" /> 
                                            </Items>
                                            <DirectEvents>
                                                <Select OnEvent = "btn_YEAR_Click" />
                                            </DirectEvents>
                                        </ext:SelectBox>

                                        <ext:SelectBox ID="cboMON" FieldLabel="" runat="server"  LabelWidth="60" LabelAlign="Right" Width="80" Cls="Text-blue">
                                            <Items>
                                                <ext:ListItem Value="001" Text="01" />
                                                <ext:ListItem Value="002" Text="02" />
                                                <ext:ListItem Value="003" Text="03" />
                                                <ext:ListItem Value="004" Text="04" />
                                                <ext:ListItem Value="005" Text="05" />
                                                <ext:ListItem Value="006" Text="06" />
                                                <ext:ListItem Value="007" Text="07" />
                                                <ext:ListItem Value="008" Text="08" /> 
                                                <ext:ListItem Value="009" Text="09" /> 
                                                <ext:ListItem Value="010" Text="10" /> 
                                                <ext:ListItem Value="011" Text="11" />                                               
                                                <ext:ListItem Value="012" Text="12" />  
                                            </Items>
                                            <DirectEvents>
                                                <Select OnEvent = "btn_MON_Click" />
                                            </DirectEvents>
                                        </ext:SelectBox>


                                        <ext:Button ID="btn_Query" runat="server" Icon="Find" Text="查询" ColumnWidth=".3" >
                                          <DirectEvents>
                                             <Click OnEvent="btn_Query_Click">
                                            </Click>
                                         </DirectEvents>
                                       </ext:Button>                                       


                                        <ext:TextField ID="txtWEEK" runat="server" FieldLabel="星期" Cls="Text-blue" LabelWidth="60" LabelAlign="Right" ReadOnly2="true" Width="180" Visible="false">
                                            <Listeners>
                                                <KeyPress Fn="getScreenResolution()" />
                                            </Listeners>
                                        </ext:TextField>
                                        <ext:Label ID="GAP2" runat="server" Text="" Width="5" />
                                    </Items>
                                </ext:Container>

                                <ext:Container ID="Container2" runat="server" Frame="true" Layout="HBoxLayout" Padding="6" >
                                    <Items>
                                        <ext:SelectBox ID="cboTIME" FieldLabel="时段" runat="server"  LabelWidth="60" LabelAlign="Right" Width="180" Cls="Text-blue" Visible="false">
                                            <Items>
                                                <ext:ListItem Value="001" Text="上午" />
                                                <ext:ListItem Value="002" Text="下午" />
                                                <ext:ListItem Value="003" Text="晚班" />
                                            </Items>
                                            <DirectEvents>
                                                <Select OnEvent = "cboTIME_Click" />
                                            </DirectEvents>
                                        </ext:SelectBox>

                                        <ext:Label ID="GAP3" runat="server" Text="" Width="10" Visible="false"/>
                                        <ext:SelectBox ID="cboFLOOR" FieldLabel="楼层/区" runat="server"  LabelWidth="100" LabelAlign="Right" Width="200" Cls="Text-blue" >
                                            <%--<Items>
                                                <ext:ListItem Value="03" Text="3楼" />
                                                <ext:ListItem Value="05" Text="5楼" />
                                            </Items>--%>
                                            <DirectEvents>
                                                <Select OnEvent = "cboFLOOR_Click" />
                                            </DirectEvents>
                                        </ext:SelectBox>
                                        <ext:Label ID="GAP4" runat="server" Text="" Width="10"/>

                                        <ext:SelectBox ID="cboAREA" FieldLabel="" runat="server" LabelWidth="60" LabelAlign="Right" Width="100" Cls="Text-blue" >
                                           <%-- <Items>
                                                <ext:ListItem Value="A" Text="A区" />
                                                <ext:ListItem Value="B" Text="B区" />
                                                <ext:ListItem Value="C" Text="C区" />
                                                <ext:ListItem Value="D" Text="D区" />
                                            </Items>--%>
                                            <DirectEvents>
                                                <Select OnEvent = "cboAREA_Click" />
                                            </DirectEvents>
                                        </ext:SelectBox>
                                        
                                        <ext:SelectBox ID="SelectBox1" FieldLabel="姓名" runat="server"  LabelWidth="100" LabelAlign="Right" Width="250" Cls="Text-blue" >
                                            <%--<Items>
                                                <ext:ListItem Value="03" Text="3楼" />
                                                <ext:ListItem Value="05" Text="5楼" />
                                            </Items>--%>
                                            <DirectEvents>
                                                <Select OnEvent = "cboNAME_Click" />
                                            </DirectEvents>
                                        </ext:SelectBox>

                                        <ext:SelectBox ID="SelectBox2" FieldLabel="身分证号" runat="server"  LabelWidth="120" LabelAlign="Right" Width="250" Cls="Text-blue" >
                                            <%--<Items>
                                                <ext:ListItem Value="03" Text="3楼" />
                                                <ext:ListItem Value="05" Text="5楼" />
                                            </Items>--%>
                                            <DirectEvents>
                                                <Select OnEvent = "cboPATIC_Click" />
                                            </DirectEvents>
                                        </ext:SelectBox>  
                                          
                                         <ext:Label ID="Label2" runat="server" Text="" Width="10"/>

                                         <ext:Button ID="btn_set" runat="server" Icon="Find" Text="重置" ColumnWidth=".3" >
                                          <DirectEvents>
                                             <Click OnEvent="btn_set_Click">
                                            </Click>
                                         </DirectEvents>
                                       </ext:Button>
                                       
                                        <ext:Button ID="btn_save" runat="server" Icon="Find" Text="存盘" ColumnWidth=".3" >
                                          <DirectEvents>
                                             <Click OnEvent="btn_save_Click">
                                            </Click>
                                         </DirectEvents>
                                       </ext:Button>    

                                       <ext:Button ID="btn_print" runat="server" Icon="Find" Text="打印" ColumnWidth=".3" >
                                          <DirectEvents>
                                             <Click OnEvent="btn_print_Click">
                                            </Click>
                                         </DirectEvents>
                                       </ext:Button>      
                                       
                                                                   

                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Panel>
                        
                    </Items>
                </ext:Panel>
                
                <ext:Panel ID="Panel2" runat="server" Region="Center" Title=" " Border="false" AutoScroll="true" Visible="false" >
                    <Items>
                                            
                      
                        <ext:GridPanel ID="grdBED_LIST" runat="server" Height2="1035" Width="970" AutoScroll="true"  >
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
                                    <ext:Column ID="Column1" runat="server" Text=" " DataIndex="BED_NO" Width="30" /> 
                                    <ext:Column ID="Column2" runat="server" Text=" " DataIndex="MAC_MODEL" Width="70" Visible="false"/>
                                    <ext:Column ID="Column3" runat="server" Text=" " DataIndex="MAC_TYPE" Width="60" >
                                        <Renderer Fn="change" />
                                    </ext:Column>
                                    <ext:Column ID="Column4" runat="server" Text="状态" DataIndex="MAC_STATE" Width="90" Visible="false"/>
                                    <ext:Column ID="Column5" runat="server" Text="姓名" DataIndex="PERSON_NAME" Width="90" >
                                      <Renderer Fn="change2" />
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" Text="身份证号" DataIndex="PERSON_ID" Width="270" Visible="false"/> 
                                    <ext:Column ID="Column7" runat="server" Text="体重" DataIndex="PERSON_WEIGHT" Width="70" Visible="false" />
                                    <ext:Column ID="Column8" runat="server" Text="开机" DataIndex="PERSON_STATE" Width="60" Visible="false" >
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
                </ext:Panel>



                 <ext:Panel ID="Panel5" runat="server" Region="Center" Title=" "  Border="false" AutoScroll="true" >
                    <Items>                                            
                        
                        <ext:GridPanel ID="grdBED_LISTN" runat="server" Height2="1035" Width="1270" AutoScroll="true"  >
                            <Store>
                                <ext:Store ID="Store1N" runat="server" OnReadData="REFRESH_BED" >
                               
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" Name="recordlist2N">
                                            <Fields>
                                               <ext:ModelField Name="sched_year" Type="String" />
                                                <ext:ModelField Name="sched_mon" Type="String"/>                                           
                                                <ext:ModelField Name="sched_flr" Type="String" />
                                                <ext:ModelField Name="sched_sec" Type="String"/>
                                                <ext:ModelField Name="sched_bedno" Type="String" />
                                                 <ext:ModelField Name="sched_mactyp" Type="String" />
                                                <ext:ModelField Name="sched_timen" Type="String" />
                                                <ext:ModelField Name="sched_1D_name"   Type="String" />
                                                <ext:ModelField Name="sched_2D_name"   Type="String" />
                                                <ext:ModelField Name="sched_3D_name"   Type="String" />
                                                <ext:ModelField Name="sched_4D_name"   Type="String" />
                                                <ext:ModelField Name="sched_5D"   Type="String" />                                               
                                                <ext:ModelField Name="sched_6D"   Type="String" />
                                                <ext:ModelField Name="sched_7D"   Type="String" />
                                                <ext:ModelField Name="sched_8D"   Type="String" />
                                                <ext:ModelField Name="sched_9D"   Type="String" />
                                                <ext:ModelField Name="sched_10D"   Type="String" />
                                                <ext:ModelField Name="sched_11D"   Type="String" />
                                                <ext:ModelField Name="sched_12D"   Type="String" />
                                                <ext:ModelField Name="sched_13D"   Type="String" />
                                                <ext:ModelField Name="sched_14D"   Type="String" />
                                                <ext:ModelField Name="sched_15D"   Type="String" />                                              
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel2N" runat="server">
                                <Columns>
                                   <ext:Column ID="Column29" runat="server"   Text=""      DataIndex="sched_year" Width="60" Visible="false"/> 
                                    <ext:Column ID="Column30" runat="server"  Text=""      DataIndex="sched_mon" Width="60" Visible="false"/> 
                                    <ext:Column ID="Column9" runat="server"   Text=""      DataIndex="sched_flr" Width="60" Visible="false"/> 
                                    <ext:Column ID="Column25" runat="server"  Text=""      DataIndex="sched_sec" Width="60" Visible="false"/> 
                                    <ext:Column ID="Column27" runat="server"  Text="床"    DataIndex="sched_bedno" Width="60" />
                                    <ext:Column ID="Column28" runat="server"  Text="型別"  DataIndex="sched_mactyp" Width="60" /> 
                                    <ext:Column ID="Column26" runat="server"  Text="午别"  DataIndex="sched_timen" Width="60" /> 
                                    <ext:Column ID="Column10" runat="server" Text="1日" DataIndex="sched_1D_name"   Width="70" />
                                    <ext:Column ID="Column11" runat="server" Text="2日" DataIndex="sched_2D_name"   Width="70" />   
                                    <ext:Column ID="Column12" runat="server" Text="3日" DataIndex="sched_3D_name"   Width="70" />     
                                    <ext:Column ID="Column13" runat="server" Text="4日" DataIndex="sched_4D_name"   Width="70" />     
                                    <ext:Column ID="Column14" runat="server" Text="5日" DataIndex="sched_5D"   Width="70" />   
                                    <ext:Column ID="Column15" runat="server" Text="6日" DataIndex="sched_6D"   Width="70" />     
                                    <ext:Column ID="Column16" runat="server" Text="7日" DataIndex="sched_7D"   Width="70" />     
                                    <ext:Column ID="Column17" runat="server" Text="8日" DataIndex="sched_8D"   Width="70" />     
                                    <ext:Column ID="Column18" runat="server" Text="9日" DataIndex="sched_9D"   Width="70" />     
                                    <ext:Column ID="Column19" runat="server" Text="10日" DataIndex="sched_10D"   Width="70" />     
                                    <ext:Column ID="Column20" runat="server" Text="11日" DataIndex="sched_11D"   Width="70" />     
                                    <ext:Column ID="Column21" runat="server" Text="12日" DataIndex="sched_12D"   Width="70" />  
                                       
                                    <ext:Column ID="Column22" runat="server" Text="13日" DataIndex="sched_13D"   Width="70" >     
                                     <PrepareCommand Fn="prepareCommand" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="ChartEdit" Icon="Pencil" Style="margin-left:5px !important;" >
                                                <ToolTip Text="修改" />
                                            </ext:ImageCommand>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="nurse_Click" >
                                                <ExtraParams> 
                                                    <ext:Parameter Name="sched_year" Value="record.data.sched_year" Mode="Raw"/>    
                                                    <ext:Parameter Name="sched_mon" Value="record.data.sched_mon" Mode="Raw"/>   
                                                    <ext:Parameter Name="sched_flr" Value="record.data.sched_flr" Mode="Raw"/>    
                                                     <ext:Parameter Name="sched_sec" Value="record.data.sched_sec" Mode="Raw"/>   
                                                      <ext:Parameter Name="sched_bedno" Value="record.data.sched_bedno" Mode="Raw"/>   
                                                      <ext:Parameter Name="sched_mactyp" Value="record.data.sched_mactyp" Mode="Raw"/>                                                  
                                                      <ext:Parameter Name="sched_time" Value="record.data.sched_time" Mode="Raw"/>                                                    
                                                    <ext:Parameter Name="sched_13D" Value="record.data.sched_13D" Mode="Raw"/>
                                                     <ext:Parameter Name="select_flag" Value="15" Mode="Raw"/>
                                                </ExtraParams> 
                                            </Command> 
                                        </DirectEvents>
                                     </ext:Column>


                                    <ext:Column ID="Column23" runat="server" Text="14日" DataIndex="sched_14D"   Width="70" >
                                        <PrepareCommand Fn="prepareCommand14" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="ChartEdit" Icon="Pencil" Style="margin-left:5px !important;" >
                                                <ToolTip Text="修改" />
                                            </ext:ImageCommand>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="nurse_Click" >
                                                <ExtraParams> 
                                                    <ext:Parameter Name="sched_year" Value="record.data.sched_year" Mode="Raw"/>    
                                                    <ext:Parameter Name="sched_mon" Value="record.data.sched_mon" Mode="Raw"/>   
                                                    <ext:Parameter Name="sched_flr" Value="record.data.sched_flr" Mode="Raw"/>    
                                                     <ext:Parameter Name="sched_sec" Value="record.data.sched_sec" Mode="Raw"/>   
                                                      <ext:Parameter Name="sched_bedno" Value="record.data.sched_bedno" Mode="Raw"/>   
                                                      <ext:Parameter Name="sched_mactyp" Value="record.data.sched_mactyp" Mode="Raw"/>                                                  
                                                      <ext:Parameter Name="sched_time" Value="record.data.sched_time" Mode="Raw"/>    
                                                    <ext:Parameter Name="sched_14D" Value="record.data.sched_14D" Mode="Raw"/>
                                                    <ext:Parameter Name="select_flag" Value="14" Mode="Raw"/>
                                                </ExtraParams> 
                                            </Command> 
                                        </DirectEvents>  
                                    </ext:Column>

                                     
                                    <ext:Column ID="Column24" runat="server" Text="15日" DataIndex="sched_15D"   Width="70" >
                                        <PrepareCommand Fn="prepareCommand" />
                                        <Commands>
                                            <ext:ImageCommand CommandName="ChartEdit" Icon="Pencil" Style="margin-left:5px !important;" >
                                                <ToolTip Text="修改" />
                                            </ext:ImageCommand>
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="nurse_Click" >
                                                <ExtraParams> 
                                                    <ext:Parameter Name="sched_year" Value="record.data.sched_year" Mode="Raw"/>    
                                                    <ext:Parameter Name="sched_mon" Value="record.data.sched_mon" Mode="Raw"/>   
                                                    <ext:Parameter Name="sched_flr" Value="record.data.sched_flr" Mode="Raw"/>    
                                                     <ext:Parameter Name="sched_sec" Value="record.data.sched_sec" Mode="Raw"/>   
                                                      <ext:Parameter Name="sched_bedno" Value="record.data.sched_bedno" Mode="Raw"/>   
                                                      <ext:Parameter Name="sched_mactyp" Value="record.data.sched_mactyp" Mode="Raw"/>                                                  
                                                      <ext:Parameter Name="sched_time" Value="record.data.sched_time" Mode="Raw"/>                                                    
                                                    <ext:Parameter Name="sched_15D" Value="record.data.sched_15D" Mode="Raw"/>
                                                     <ext:Parameter Name="select_flag" Value="15" Mode="Raw"/>
                                                </ExtraParams> 
                                            </Command> 
                                        </DirectEvents>
                                     </ext:Column>


                                </Columns>
                            </ColumnModel>                            
 
                            <%--<SelectionModel>
                                <ext:RowSelectionModel ID="RowSelectionModel1" runat="server" Mode="Single">
                                    <DirectEvents>
                                        <Select OnEvent="RowSelect">
                                            <ExtraParams>
                                                <ext:Parameter Name="Values" Value="#{grdBED_LISTN}.getRowsValues({ selectedOnly : true })"
                                                    Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Select>
                                    </DirectEvents>
                                </ext:RowSelectionModel>
                            </SelectionModel>--%>
                            
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>



            </Items>
        </ext:Viewport>

        <%-- DisplayMsg="当前是第{0}条-第{1}条，共{2}条" --%>
        
        <ext:TaskManager ID="TaskManager1" runat="server" Enabled="true" >
            <Tasks>
                <ext:Task TaskID="tskTIME" Interval="60000"  >
                    <DirectEvents>
                        <Update OnEvent="Timer1_Timer" />
                    </DirectEvents>                    
                </ext:Task>
            </Tasks>
        </ext:TaskManager>
    </div>
    </form>
</body>
</html>
