<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_10_I_Alasamo_List.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_10_I_Alasamo_List" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title>阿拉善盟血透月报表</title>
</head>
<body>
    <form id="form1" runat="server">

        <ext:ResourceManager ID="ResourceManager1" runat="server" Theme="Default" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="FitLayout">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="血透月报表" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" SortableColumns="false">
                            <Store>
                                <ext:Store ID="Store1" runat="server" PageSize="15">
                                    <Model>
                                        <ext:Model ID="Model1" runat="server">
                                            <Fields>
                                                <ext:ModelField Name="uid" Type="Int" />
                                                <ext:ModelField Name="Month" />
                                                <ext:ModelField Name="Name" />
                                                <ext:ModelField Name="Size" />
                                                <ext:ModelField Name="SaveDateTime" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                    <Sorters>
                                        <ext:DataSorter Property="common" Direction="ASC" />
                                    </Sorters>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server">
                                <Columns>
                                    <ext:Column ID="uid" runat="server" Text="序号" DataIndex="uid" Width="60" />
                                    <ext:Column ID="Month" runat="server" Text="年-月" DataIndex="Month" Width="80" />
                                    <ext:Column ID="Name" runat="server" Text="上传名称" DataIndex="Name" Width="250">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Update" Icon="DiskUpload" />
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="UploadWin">
                                                <ExtraParams>
                                                    <ext:Parameter Name="uid" Value="record.data.uid" Mode="Raw" />
                                                    <ext:Parameter Name="month" Value="record.data.Month" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                    <ext:Column ID="Size" runat="server" Text="上传大小(Byte)" DataIndex="Size" Width="140" />
                                    <ext:Column ID="SaveDateTime" runat="server" Text="上传时间" DataIndex="SaveDateTime" Width="170" />
                                    <ext:Column ID="empty" runat="server" Text="下载" RightCommandAlign="false" Flex="1">
                                        <Commands>
                                            <ext:ImageCommand CommandName="Download" Icon="DiskDownload" />
                                        </Commands>
                                        <DirectEvents>
                                            <Command OnEvent="DownFile_Click">
                                                <ExtraParams>
                                                    <ext:Parameter Name="uid" Value="record.data.uid" Mode="Raw" />
                                                </ExtraParams>
                                            </Command>
                                        </DirectEvents>
                                    </ext:Column>
                                </Columns>
                            </ColumnModel>
                            <BottomBar>
                                <ext:PagingToolbar ID="PagingToolbar" runat="server" StoreID="Store1" />
                            </BottomBar>
                            <View>
                                <ext:GridView ID="GridView1" runat="server" StripeRows="true" />
                            </View>
                            <TopBar>
                                <ext:Toolbar runat="server">
                                    <Items>
                                        <ext:Button ID="Btn_Add" runat="server" Text="新增" Icon="Add" UI="Success">
                                            <DirectEvents>
                                                <Click OnEvent="Btn_Add_Click">
                                                    <EventMask ShowMask="true" />
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                        <ext:Label ID="Label1" runat="server" Text=" " Width="20" />
                                        <ext:Button ID="DownTmpFile" runat="server" Text="下载空白月报" Icon="DiskDownload" Width="100" UI="Info">
                                            <DirectEvents>
                                                <Click OnEvent="DownTmpFile_Click">
                                                </Click>
                                            </DirectEvents>
                                        </ext:Button>
                                    </Items>
                                </ext:Toolbar>
                            </TopBar>
                        </ext:GridPanel>
                    </Items>
                </ext:FormPanel>
                <ext:Window ID="AddWindow" runat="server" Title="新增" Width="330" Height="160" Modal="true" AutoRender="false" Hidden="true" UI="Info">
                    <Items> 
                        <ext:Container ID="Container1" runat="server" Layout="HBoxLayout" Padding="10">
                            <Items>
                                <ext:TextField ID="YearMonth" runat="server" FieldLabel="年-月" LabelWidth="50" Width="120" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container5" runat="server" Layout="HBoxLayout" Padding="10">
                            <Items>
                                <ext:Label ID="Label2" runat="server" Text="选择档案" Width="55" />
                                <ext:FileUploadField ID="UploadDoc" runat="server" Text="选择档案" Width="240" Icon="Attach" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container2" runat="server" Layout="HBoxLayout" Padding="10">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:Button ID="SaveFile" runat="server" Text="开始上传" Icon="DiskUpload" UI="Success">
                                    <DirectEvents>
                                        <Click OnEvent="UploadFile_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Window>
                <ext:Window ID="UpdateWindow" runat="server" Title="更新" Width="330" Height="160" Modal="true" AutoRender="false" Hidden="true" UI="Info">
                    <Items> 
                        <ext:Container ID="Container6" runat="server" Layout="HBoxLayout" Padding="10">
                            <Items>
                                <ext:Hidden ID="upload_uid" runat="server" />
                                <ext:TextField ID="UYearMonth" runat="server" FieldLabel="年-月" LabelWidth="50" Width="120" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container3" runat="server" Layout="HBoxLayout" Padding="10">
                            <Items>
                                <ext:Label ID="LabelUF" runat="server" Text="选择档案" Width="55" />
                                <ext:FileUploadField ID="FileUploadField1" runat="server" Text="选择档案" Width="240" Icon="Attach" />
                            </Items>
                        </ext:Container>
                        <ext:Container ID="Container4" runat="server" Layout="HBoxLayout" Padding="10">
                            <LayoutConfig>
                                <ext:HBoxLayoutConfig Align="Top" Pack="Center" />
                            </LayoutConfig>
                            <Items>
                                <ext:Button ID="Button1" runat="server" Text="开始上传" Icon="DiskUpload" UI="Success">
                                    <DirectEvents>
                                        <Click OnEvent="UpdateFile_Click" />
                                    </DirectEvents>
                                </ext:Button>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Window>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
