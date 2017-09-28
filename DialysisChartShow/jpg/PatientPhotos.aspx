<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatientPhotos.aspx.cs" Inherits="Dialysis_Chart_Show.jpg.PatientPhotos" %>

<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .x-label-value { vertical-align: middle; font-size:300%; color:#0000FF;}
        
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager1" runat="server" />
        <ext:Panel ID="Panel1" runat="server" Height="800" >
            <Items>

                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" Height="60">
                    <Items>
                        <ext:Label ID="Lab_pv_floor" runat="server" ColumnWidth=".3" >
                        </ext:Label>
                        <ext:Label ID="Lab_pv_sec" runat="server"  ColumnWidth=".3" >
                        </ext:Label>
                        <ext:Label ID="Lab_pv_bedno" runat="server"  ColumnWidth=".4" >
                        </ext:Label>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" Height="600">
                    <Items>
                        
                        <ext:Image ID="Image1" runat="server" ColumnWidth=".5" Height="600">
                        </ext:Image>
                    </Items>
                </ext:Container>
                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout">
                    <Items>
                        <ext:Label ID="Lab_pif_name" runat="server" ColumnWidth=".20" >
                        </ext:Label>
                        <ext:Label ID="Lab_pif_ic" runat="server" ColumnWidth=".30" >
                        </ext:Label>
                        <ext:Label ID="Lab_age" runat="server" ColumnWidth=".25" >
                        </ext:Label>
                        <ext:Label ID="Lab_pif_sex" runat="server" ColumnWidth=".25" >
                        </ext:Label>
                    </Items>
                </ext:Container>
            </Items>
        </ext:Panel>
        
    </div>
    </form>
</body>
</html>
