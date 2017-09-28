<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="dry_weightlist.aspx.cs" Inherits="Dialysis_Chart_Show.Information.dry_weightlist" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
    <ext:GridPanel ID="Grid_Weight" runat="server" Title="干体重" Height="367" Width ="200" Icon="ApplicationFormEdit" Cls="x-grid-custom" >
        <Store>
            <ext:Store ID="Store1" runat="server" >
                <Model>
                    <ext:Model ID="Model" runat="server" Name="DryWeight" >
                        <Fields> 
                            <ext:ModelField Name="no" Type="String" />
                            <ext:ModelField Name="date" Type="String" />
                            <ext:ModelField Name="weight" Type="String" />                                                
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
                <ext:Column ID="Column13" runat="server" DataIndex="no" Text="" Width="40" />                                      
                <ext:Column ID="Column1" runat="server" DataIndex="date" Text="日期" Width="80" />                                      
                <ext:Column ID="Column2" runat="server" DataIndex="weight" Text="干体重" Width="60" />                                      
            </Columns>
        </ColumnModel>                            
    </ext:GridPanel>

    </div>
    </form>
</body>
</html>
