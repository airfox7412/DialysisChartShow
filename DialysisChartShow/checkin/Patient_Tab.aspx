<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Patient_Tab.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.Patient_Tab" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <meta name="viewport" content="width=device-width, initial-scale=0.8, user-scalable=no, minimum-scale=0.8, maximum-scale=0.8,Auto-Rotate=Disable" />
    <title>病患報到</title>
    <link href="henan.css" rel="stylesheet"/>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:Hidden ID="patient_id" runat="server" />
        <ext:Hidden ID="patient_name" runat="server" />
        <ext:Hidden ID="machine_type" runat="server" />
        <ext:Hidden ID="machine_model" runat="server" />
        <ext:Hidden ID="bedno" runat="server" />
        <ext:Hidden ID="floor" runat="server" />
        <ext:Hidden ID="area" runat="server" />
        <ext:Hidden ID="time" runat="server" />
        <ext:Hidden ID="daytyp" runat="server" />
        <ext:Hidden ID="hpack" runat="server" />
        <ext:Hidden ID="hpack3" runat="server" />
        <ext:Hidden ID="patient_weight" runat="server" />

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Neptune" />
        <ext:TabPanel ID="TabPanel1" runat="server" Region="Center" TabPosition="Top" EnableTabScroll="true" Height="50" Cls="mid-tab-text">
            <Items>
            </Items>
        </ext:TabPanel>
    </div>
    </form>
</body>
</html>