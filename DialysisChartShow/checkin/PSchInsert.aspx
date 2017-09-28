<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PSchInsert.aspx.cs" Inherits="Dialysis_Chart_Show.checkin.PSchInsert" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>病患排班</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <script type="text/javascript">
    </script>
</head>
<body >
    <form id="form1" runat="server">
        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Triton" Locale="zh-CN" />
        <ext:Hidden ID="PIC" runat="server" />
        <ext:Hidden ID="PID" runat="server" />

        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:Panel ID="Panel12" runat="server" Header="false" AutoScroll="true" Width="900" Region="North">
                    <TopBar>
                        <ext:Toolbar runat="server">
                            <Items>
                                <ext:TextField ID="PNAME" runat="server" FieldLabel="姓名" LabelWidth="30" ReadOnly="true" Width="130" />
                                <ext:DateField ID="InsertDate" runat="server" Format="yyyy-MM-dd" Width="130">                                
                                    <DirectEvents>
                                        <Change OnEvent="Query_click" />
                                    </DirectEvents>
                                </ext:DateField>
                                <ext:ComboBox ID="timetype" runat="server" FieldLabel="时段" LabelWidth="40" LabelAlign="Right" Width="130">
                                    <Items>
								        <ext:ListItem Value="001" Text="上午" />
								        <ext:ListItem Value="002" Text="下午" />
								        <ext:ListItem Value="003" Text="晚班" />
								    </Items>
                                    <DirectEvents>
                                        <Change OnEvent="Query_click" />
                                    </DirectEvents>
                                </ext:ComboBox>
                                <%--<ext:Button ID="btnQuery" runat="server" Text="寻找" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="btnQuery_click" />
                                    </DirectEvents>
                                </ext:Button>--%>
                            </Items>
                        </ext:Toolbar>
                    </TopBar>
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" EnableColumnMove="false" EnableColumnResize="false" EnableColumnHide="false" Border="true" 
                            ColumnLines="true" Cls="x-grid-custom" Width="400" Height="500" MarginSpec="0 0 10 13">
                            <Store>
                                <ext:Store ID="store1" runat="server">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="bedno">
                                            <Fields>
                                                <ext:ModelField Name="floor" />
                                                <ext:ModelField Name="area" />
                                                <ext:ModelField Name="bedno" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Text="序号" Width="70"  />
                                    <ext:Column ID="Column1" runat="server" Text="楼层" Align="Center" Sortable="false" DataIndex="floor" Width="80" />
                                    <ext:Column ID="Column2" runat="server" Text="区" Align="Center" Sortable="false" DataIndex="area" Width="80" />
                                    <ext:Column ID="Column3" runat="server" Text="空床号" Align="Center" Sortable="false" DataIndex="bedno" Width="80" />                                    
                                    <ext:Column ID="Column4" runat="server" Text="排班" RightCommandAlign="false" Flex="1">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Add" Icon="Add" />
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="Add_Click">
                                                <ExtraParams>                                         
                                                    <ext:Parameter Name="command" Value="command" Mode="Raw" />
                                                    <ext:Parameter Name="floor" Value="record.data.floor" Mode="Raw" />
                                                    <ext:Parameter Name="area" Value="record.data.area" Mode="Raw" />
                                                    <ext:Parameter Name="bedno" Value="record.data.bedno" Mode="Raw" />                                      
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                        </ext:GridPanel>
                    </Items>
                </ext:Panel>
            </Items>    
        </ext:Viewport>
    </form>
</body>
</html>
