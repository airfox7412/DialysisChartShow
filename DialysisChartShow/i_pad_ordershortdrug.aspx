<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="i_pad_ordershortdrug.aspx.cs" Inherits="Dialysis_Chart_Show.i_pad_doclogin" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
     <meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8,Auto-Rotate=Disable" />
    <title></title>
    <style type="text/css">
        
        <%--表頭--%>
        .label .x-label-value
        {
            width: 120px !important;
            height: 35px !important;
            font-size: 21px;  <%--Not xx-large  Alex Shen20151214--%>
            color: #178951;
        }
        .label2 .x-label-value
        {
            font-size: 20px;  <%--Not xx-large  Alex Shen20151214--%>
            display:block;
            height:24px;
            text-align:right;
            width:160px;
        }
        <%--文字框加大--%>
        .x-border-box .x-form-text
        {
            height: 32px !important;
            font-size: 24px; <%--x-large--%>
        }
        <%--文字框頭 對齊右--%>
        .x-form-item-label-right
        {
            font-size: 24px; <%--x-large--%>
        }
        <%--文字框尾--%>
        .x-field-indicator
        {
            font-size: 24px; <%--x-large--%>
        }
        <%--Windows使用--%>
        .x-window-header-text-default
        {
            font-size: 18px;  <%--Not x-large  Alex Shen20151214--%> 
            line-height: 36px;
        }
        .x-box-item
        {
            height: 36px !important;
        }
        .x-btn .x-btn-center .x-btn-inner
        {
            font-size: 24px;  <%--x-large--%>
        }
        <%--OPT與CHK--%>
        .x-form-item
        {
            font-size: 18px;  <%--Not x-large  Alex Shen20151214--%> 
            height: 24px;
            <%--font: normal 25px tahoma,arial,verdana,sans-serif;--%>
        }

        .Xx-tool img
        {
            height: 35px;
            width: 30px;
        }
        .Xx-fieldset-header .x-fieldset-header-text
        {
            font-size: 18px; <%--large--%>
        }
        .Xx-form-display-field
        {
            font-size: 20px;
        }
                
        .red
        {
            color: Red;
        }
        .red .x-form-field
        {
            color: blue;
        }
        
        
        .Text-black .x-form-field
        {
            color: black;
        }
        .Text-black-H .x-form-field
        {
            height: 100px !important;
            color: black !important;
        }
        .Text-red .x-form-field
        {
            color: red;
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
        .red-text {
            color     : red;
            font-size : xx-large;
        }
        
        

        .CheckBox-red
        {
            color: Red;
            font-size: 24px;
            height: 24px;
        }
        
        .x-border-box .x-form-trigger
        {
            height: 30px !important;
            width: 17px !important;
            background-image: url("./Styles/trigger.png");
            cursor: pointer;
        }
        .x-form-checkbox, .x-form-radio
        {
            width: 34px;
            height: 34px;
            background-image: url("./Styles/che_btn.png");
        }
        
        #ImageButton1, #ImageBtn_Home, #ImageBtn_back, #ImageBtn_save
        {
            height: 50px !important;
        }

        .x-panel-header-text-default
        {
            font-size: 32px;  <%--xx-large--%>
            line-height: 36px;
        }
        <%--panel head 自动--%>
        .Xx-panel-header-text {
            font-size: 32px;
            line-height: 36px;
        }
        <%--Grid Row--%>
        .x-grid-with-row-lines .x-grid-cell-inner
        {
            font-size: 26px;
            line-height: 28px; 
        }
        <%--Grid Column--%>
        .x-column-header-inner .x-column-header-text
        {
            font-size: 18px; <%--large--%>
            line-height: 28px; 
        }
        
        .x-boundlist-item 
        { 
            font-size: 24px; 
            color: blue
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
     <ext:ResourceManager ID="ResourceManager1" runat="server" />
    <div>
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="patient_name" runat="server" />
        <ext:Hidden ID="patient_sex" runat="server" />
        <ext:Hidden ID="pat_docname" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="page" runat="server" />
     <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>     
               <ext:FormPanel ID="FormPanel1" runat="server" ButtonAlign="Center" Padding="5" Layout="ColumnLayout" 
                    Title="短期医嘱用药" BodyStyle="background-color:#EBF5FF !important;" AutoScroll="true">
                   <Items>
                        <ext:Label ID="Label1" runat="server" Text="姓名:" ColumnWidth=".08" Cls="label" />
                        <ext:Label ID="Label2" runat="server" Text=" " ColumnWidth=".15" Cls="label" />
                        <ext:Label ID="Label3" runat="server" Text="身份证号:" ColumnWidth=".15" Cls="label" />
                        <ext:Label ID="Label4" runat="server" Text=" " ColumnWidth=".4" Cls="label" />
                        <ext:Label ID="Label5" runat="server" Text="性别:" ColumnWidth=".08" Cls="label" />
                        <ext:Label ID="Label6" runat="server" Text=" " ColumnWidth=".05" Cls="label" />

                        <ext:TextField ID="txt_orddate" runat="server" FieldLabel="日期" ReadOnly="true" LabelWidth="120" ColumnWidth=".34" LabelAlign="Right" PaddingSpec="25 2 0 2" Cls="Text-blue" >
                        </ext:TextField>
                        <ext:TextField ID="txt_ordtime" runat="server" FieldLabel="记录时间" ReadOnly="true" LabelWidth="120" ColumnWidth=".33" LabelAlign="Right" PaddingSpec="25 2 0 2" Cls="Text-blue" >
                        </ext:TextField>
                        <ext:TextField ID="txt_orddoc" runat="server" FieldLabel="经治医生" ReadOnly="true" LabelWidth="120" ColumnWidth=".33" LabelAlign="Right" PaddingSpec="25 2 0 2" Cls="Text-blue">
                        </ext:TextField>

                        <ext:SelectBox ID="cbo_druggrp" runat="server" ColumnWidth=".5" FieldLabel="药品分类" 
							IndicatorText="*" IndicatorCls="red-text" 
							Cls="Text-blue" LabelWidth="160" LabelAlign="Right" PaddingSpec="10 50 0 2">
                              <DirectEvents>
                                <Change OnEvent="cmb_onchange" />
                            </DirectEvents>
                        </ext:SelectBox>
                        <ext:SelectBox ID="cbo_druglist" runat="server" ColumnWidth=".5" FieldLabel="药品名称" 
							IndicatorText="*" IndicatorCls="red-text" 
							Cls="Text-blue" LabelWidth="160" LabelAlign="Right" PaddingSpec="10 50 0 2" >
                        </ext:SelectBox>

                        <ext:TextField ID="txt_ordcount" runat="server" FieldLabel="剂量" 
							IndicatorText="*" IndicatorCls="red-text" Cls="Text-blue" 
							LabelWidth="100" ColumnWidth=".25" LabelAlign="Right" PaddingSpec="5 2 0 2" >
                        </ext:TextField>

                        <ext:SelectBox ID="cmd_medway" runat="server" ColumnWidth=".35" FieldLabel="給藥方式"  
							IndicatorText="*" IndicatorCls="red-text" Cls="Text-blue" 
							LabelWidth="160" LabelAlign="Right" PaddingSpec="5 2 0 2" >
                        </ext:SelectBox>                        

                        <ext:SelectBox ID="cmb_ordfreq" runat="server" ColumnWidth=".35" FieldLabel="频率"  
							IndicatorText="*" IndicatorCls="red-text" Cls="Text-blue" 
							LabelWidth="160" LabelAlign="Right" PaddingSpec="5 2 0 2" >
                        </ext:SelectBox>


                        
<%--                        <ext:TextField ID="txt_nuser_stfcode" runat="server" ColumnWidth=".2" FieldLabel="*護士工號" Cls="Text-blue" LabelWidth="160" LabelAlign="Right" PaddingSpec="10 2 0 2">
                              <DirectEvents>
                                <Change OnEvent="cmb_stfcode" />
                             </DirectEvents>
                        </ext:TextField> 

                        <ext:TextField ID="txt_nuser_name" runat="server" ColumnWidth=".2" FieldLabel="" readonly="true"  Cls="Text-blue" LabelWidth="160" LabelAlign="Right" PaddingSpec="10 2 0 2">
                        </ext:TextField> --%>

                         <ext:TextArea ID="txt_ordremark" runat="server" FieldLabel="备注" ColumnWidth="1" LabelAlign="Right" PaddingSpec="10 2 0 2" Cls="Text-blue-H" LabelWidth="220" >
                         </ext:TextArea>
                        <ext:ImageButton ID="img_btnsave" runat="server" ImageUrl="Styles/savesm.png" Height="50" ColumnWidth="1" OverImageUrl="Styles/savesmover.png" >
                           <DirectEvents>
                                        <Click OnEvent="Btn_save_drg_Click" />
                            </DirectEvents>
                        </ext:ImageButton>
                          <ext:GridPanel ID="Grid_Show_ORDSHORT" runat="server" Title="短期医嘱用药" Margins="0 0 5 5" ColumnWidth="1" Collapsible="true" >
                           <Store>
                                <ext:Store ID="Store" runat="server">
                                    <Model>
                                        <ext:Model ID="Model" runat="server" Name="shortterm_ordermgt">
                                            <Fields>
                                                <ext:ModelField Name="shord_id" Type="String" />
                                                <ext:ModelField Name="shord_dateord" Type="String" />
                                                <ext:ModelField Name="shord_timeord" Type="String" />
                                                <ext:ModelField Name="shord_usr1" Type="String" />
                                                <ext:ModelField Name="drg_name" Type="String" />
                                                <ext:ModelField Name="shord_intake" Type="String" />
                                                <ext:ModelField Name="shord_freq" Type="String" />
                                                <ext:ModelField Name="shord_medway" Type="String" />
                                                <ext:ModelField Name="shord_nurs" Type="String" />
                                                <ext:ModelField Name="genst_desc" Type="String" />
                                                <ext:ModelField Name="shord_comment" Type="String" />
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
                                    <ext:Column ID="Column1" runat="server" DataIndex="shord_dateord" Text="日期" Width="150" />
                                    <ext:Column ID="Column2" runat="server" DataIndex="shord_timeord" Text="记录时间" Width="150" />
                                    <ext:Column ID="Column6" runat="server" DataIndex="shord_usr1" Text="经治医生" Width="150" />
                                    <ext:Column ID="Column10" runat="server" DataIndex="drg_name" Text="药品名称" Width="200" />
                                    <ext:Column ID="Column11" runat="server" DataIndex="shord_intake" Text="剂量" Width="200" />
                                    <ext:Column ID="Column4" runat="server" DataIndex="shord_freq" Text="频率" Width="200" />
                                    <ext:Column ID="Column7" runat="server" DataIndex="shord_medway" Text="給藥方式" Width="200" />
                                    <ext:Column ID="Column8" runat="server" DataIndex="shord_nurs" Text="執行護士" Width="200" />
                                    <ext:Column ID="Column3" runat="server" DataIndex="genst_desc" Text="是否停用" Width="200" />
                                     <ext:Column ID="Column5" runat="server" DataIndex="shord_comment" Text="备注" Width="200" />
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                          
                              <ext:ImageButton ID="ImageBtn_Home" runat="server" ImageUrl="Styles/home.png" Height="50"
                                ColumnWidth=".5" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/homeover.png" >
                                <DirectEvents>
                                    <Click OnEvent="Btn_home_Click" />
                                </DirectEvents>
                              </ext:ImageButton>
                            <ext:ImageButton ID="ImageBtn_back" runat="server" ImageUrl="Styles/back2.png" Height="50"
                                ColumnWidth=".5" Margins="0 0 0 0" Flex="1" OverImageUrl="Styles/back2over.png" >
                                <DirectEvents>
                                    <Click OnEvent="Btn_back_Click" />
                                </DirectEvents>
                            </ext:ImageButton>
                         
                  </Items>
                </ext:FormPanel>
           </Items>
      </ext:Viewport>
    </div>
    </form>
</body>
</html>
