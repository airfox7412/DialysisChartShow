<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="health_record.aspx.cs" Inherits="Dialysis_Chart_Show.Information.health_record" %>
<%@ Register Assembly="Ext.Net" Namespace="Ext.Net" TagPrefix="ext" %>


<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <form id="form1" runat="server">
    <div>
    <ext:ResourceManager ID="ResourceManager1" runat="server" />
     <ext:Viewport ID="Viewport1" runat="server" Layout="border">
      <Items>
     <ext:Panel ID="Panel1" runat="server"  Region="North" Width="800" MinWidth="200"  Border="false" bodyStyle="background-color:#dfe8f6"
      MaxWidth="1024" Cls="color_title">
            <Items>
             <ext:Container ID="Container2" runat="server" Frame="true"  Layout="ColumnLayout" >
               <Items>
                 <ext:Label ID="Label1" Text = "" runat="server"/>
               </Items>
             </ext:Container>
              <ext:Container ID="Container1" runat="server" Frame="true"  Layout="ColumnLayout" >
               <Items>
                       <ext:Button ID="btn_rptlist" runat="server" Text="Report List" ColumnWidth=".1"  >
                           <DirectEvents>
                             <Click OnEvent = "reload_btn1"></Click>
                          </DirectEvents>
                      </ext:Button>
                       <ext:Button ID="btn_new" runat="server" Text="New" ColumnWidth=".1"  >
                          <DirectEvents>
                             <Click OnEvent = "reload_btn2"></Click>
                          </DirectEvents>
                      </ext:Button>
               </Items>
             </ext:Container>
             </Items>
      </ext:Panel>
           <ext:Panel ID="Panel2" runat="server" Region="Center" Border="false" Layout="fit" 
                        ColumnWidth="1" >
                        <Loader ID="Loader1" runat="server" Mode="Frame" AutoLoad="false" ManuallyTriggered="true">
                            <LoadMask ShowMask="true" />
                        </Loader>
                        
            </ext:Panel>
      </Items>
       </ext:Viewport>
    </div>
    </form>
</body>
</html>
