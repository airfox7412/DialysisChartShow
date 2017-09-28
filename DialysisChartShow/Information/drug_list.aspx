<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="drug_list.aspx.cs" Inherits="Dialysis_Chart_Show.Information.drug_list" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
     <style type="text/css">
    .x-grid-custom .x-grid-row-alt .x-grid-cell {
            background-color : #DAE2E8;
        }
    </style>
    <script>
        var template = '<span style="color:{0};">{1}</span>';

        var ldrug_actst = function (value) {
            return Ext.String.format(template, (value == "使用") ? "green" : "red", value);
        };

        var sdrug_actst = function (value) {
            return Ext.String.format(template, (value == "使用") ? "green" : "red", value);
        };

        
    </script>
</head>
<body>
    <form id="form1" runat="server">
    <div>
       <ext:ResourceManager ID="ResourceManager1" runat="server" />
       <ext:GridPanel ID="Grid_Long_Term" runat="server" Title="长期医嘱用药"  Height = "300"
                            Icon="ApplicationFormEdit" Cls="x-grid-custom"  >
                            <Store>
                                <ext:Store ID="Store" runat="server">
                                    <Model>
                                        <ext:Model ID="Model" runat="server" Name="LongDrugUse">
                                            <Fields> 
                                                <ext:ModelField Name="lno" Type="String" />
                                                <ext:ModelField Name="ldate" Type="String" />
                                                <ext:ModelField Name="lrecord_time" Type="String" />
                                                <ext:ModelField Name="lrecord_person" Type="String" />
                                                <ext:ModelField Name="ldrug_name" Type="String" />
                                                <ext:ModelField Name="ldrug_intake" Type="String" />
                                                <ext:ModelField Name="ldrug_freq" Type="String" />
                                                 <ext:ModelField Name="ldrug_medway" Type="String" />
                                                <ext:ModelField Name="ldrug_actst" Type="String" />
                                                <ext:ModelField Name="ldrug_dtactst" Type="String" />
                                                <ext:ModelField Name="ldrug_usr2" Type="String" />
                                                <ext:ModelField Name="lcomment" Type="String" />
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
                                    <ext:Column ID="Column13" runat="server" DataIndex="lno" Text="" Width="50">
                                     
                                    </ext:Column>                                 
                                    <ext:Column ID="Column1" runat="server" DataIndex="ldate" Text="日期" Width="100">
                                     
                                    </ext:Column>
                                    <ext:Column ID="Column2" runat="server" DataIndex="lrecord_time" Text="记录时间" Width="100">                                      
                                    </ext:Column>
                                    <ext:Column ID="Column6" runat="server" DataIndex="lrecord_person" Text="经治医生" Width="100" >                                       
                                    </ext:Column>
                                    <ext:Column ID="Column10" runat="server" DataIndex="ldrug_name" Text="药品名称" Width="300" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column5" runat="server" DataIndex="ldrug_intake" Text="剂量" Width="100" >                                      
                                    </ext:Column>
                                     <ext:Column ID="Column7" runat="server" DataIndex="ldrug_freq" Text="频率" Width="50" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column3" runat="server" DataIndex="ldrug_medway" Text="给药方式" Width="70" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column8" runat="server" DataIndex="ldrug_actst" Text="是否停用" Width="70"  > 
                                     <Renderer Fn="ldrug_actst" />                                     
                                    </ext:Column>
                                     <ext:Column ID="Column9" runat="server" DataIndex="ldrug_dtactst" Text="停用时间" Width="100" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column12" runat="server" DataIndex="ldrug_usr2" Text="记录人" Width="100" >                                       
                                    </ext:Column>
                                    <ext:Column ID="Column11" runat="server" DataIndex="lcomment" Text="备注" Width="300" >                                        
                                    </ext:Column>                                   
                                </Columns>
                            </ColumnModel>                            
                        </ext:GridPanel>

                         <ext:GridPanel ID="Grid_Short_Term" runat="server" Title="短期医嘱用药"  Height = "300"
                            Icon="ApplicationFormEdit" Cls="x-grid-custom"  >
                            <Store>
                                <ext:Store ID="Store2" runat="server">
                                    <Model>
                                        <ext:Model ID="Model2" runat="server" Name="ShortDrugUse">
                                            <Fields> 
                                                <ext:ModelField Name="sno" Type="String" />
                                                <ext:ModelField Name="sdate" Type="String" />
                                                <ext:ModelField Name="srecord_time" Type="String" />
                                                <ext:ModelField Name="srecord_person" Type="String" />
                                                <ext:ModelField Name="sdrug_name" Type="String" />
                                                <ext:ModelField Name="sdrug_intake" Type="String" />
                                                <ext:ModelField Name="sdrug_freq" Type="String" />
                                                <ext:ModelField Name="sdrug_medway" Type="String" />
                                                <ext:ModelField Name="sdrug_actst" Type="String" />
                                                <ext:ModelField Name="sdrug_dtactst" Type="String" />
                                                <ext:ModelField Name="sdrug_usr2" Type="String" />
                                                <ext:ModelField Name="scomment" Type="String" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                </ext:Store>
                            </Store>
                             <ColumnModel ID="ColumnModel2" runat="server">
                                <Columns>  
                                    <ext:Column ID="Column32" runat="server" DataIndex="sno" Text="" Width="50">                                     
                                    </ext:Column>                                 
                                    <ext:Column ID="Column42" runat="server" DataIndex="sdate" Text="日期" Width="100">                                       
                                    </ext:Column>
                                    <ext:Column ID="Column142" runat="server" DataIndex="srecord_time" Text="记录时间" Width="100">                                      
                                    </ext:Column>
                                    <ext:Column ID="Column152" runat="server" DataIndex="srecord_person" Text="经治医生" Width="100" >                                      
                                    </ext:Column>
                                    <ext:Column ID="Column162" runat="server" DataIndex="sdrug_name" Text="药品名称" Width="300" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column172" runat="server" DataIndex="sdrug_intake" Text="剂量" Width="100" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column182" runat="server" DataIndex="sdrug_freq" Text="频率" Width="50" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column4" runat="server" DataIndex="sdrug_medway" Text="给药方式" Width="70" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column192" runat="server" DataIndex="sdrug_actst" Text="是否停用" Width="70" >
                                     <Renderer Fn="sdrug_actst" />                                        
                                    </ext:Column>
                                     <ext:Column ID="Column202" runat="server" DataIndex="sdrug_dtactst" Text="停用时间" Width="100" >                                       
                                    </ext:Column>
                                     <ext:Column ID="Column212" runat="server" DataIndex="sdrug_usr2" Text="记录人" Width="100" >                                        
                                    </ext:Column>
                                    <ext:Column ID="Column222" runat="server" DataIndex="scomment" Text="备注" Width="300" >                                        
                                    </ext:Column>                                   
                                </Columns>
                            </ColumnModel>                 
                          </ext:GridPanel>
    </div>
    </form>
</body>
</html>
