<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="PatEdit.aspx.cs" Inherits="Dialysis_Chart_Show.Information.PatEdit" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
    <style type="text/css">
        .lable-red
        {
            color: #FA0300;
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Hidden ID="hid_PID" runat="server">
        </ext:Hidden>
        <ext:Hidden ID="hid_CONT_ID" runat="server">
        </ext:Hidden>
        <ext:Hidden ID="hid_UID" runat="server">
        </ext:Hidden>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="患者添加/修改" AutoScroll="true" ButtonAlign="Center">
                    <Items>
                        <ext:Panel ID="Panel12" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                
                                <ext:Panel ID="Panel8" runat="server" Height="130" Title="">
                                    <Items>
                                        <ext:Container ID="Container2" runat="server" Layout="Column">
                                            <Items>
                                                <ext:TextField runat="server" ID="pif_name" FieldLabel="姓名" ColumnWidth=".2" LabelWidth="80"
                                            LabelAlign="Right" Padding="2" Cls="lable-red" />
                                            <ext:SelectBox ID="pif_sex" FieldLabel="性别" runat="server" ColumnWidth=".2" LabelWidth="80"
                                     LabelAlign="Right"  Cls="lable-red">

                                    <Items>
                                        <ext:ListItem Value="M" Text="M">
                                        </ext:ListItem>
                                        <ext:ListItem Value="F" Text="F">
                                        </ext:ListItem>
                                    </Items>
                                </ext:SelectBox>
                                        <%--<ext:ComboBox ID="pif_sex" runat="server" FieldLabel="性别" LabelAlign="Right" LabelWidth="80" ColumnWidth=".2" Cls="lable-red">
                                            <SelectedItems>
                                                <ext:ListItem Value="1" Text="M">
                                                </ext:ListItem>
                                                <ext:ListItem Value="1" Text="F">
                                                </ext:ListItem>
                                            </SelectedItems>
                                        </ext:ComboBox>--%>
                                        <ext:TextField runat="server" ID="pif_ic" FieldLabel="身份证号" ColumnWidth=".2" 
                                            LabelWidth="80" LabelAlign="Right" Padding="2" Cls="lable-red">
                                        </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container16" runat="server" Layout="Column">
                                            <Items>
                                                <ext:DateField ID="pif_dob" runat="server" FieldLabel="出生日期" ColumnWidth=".2" Cls="lable-red"
                                                     Padding="2" LabelWidth="80" LabelAlign="Right" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                                <ext:TextField runat="server" ID="pif_mrn" FieldLabel="病历号" ColumnWidth=".2"
                                                    LabelWidth="80" LabelAlign="Right" Padding="2" >
                                                </ext:TextField>
                                                <ext:TextField runat="server" ID="pif_address" FieldLabel="地址" ColumnWidth=".2" LabelWidth="80"
                                                    LabelAlign="Right" Padding="2" >
                                                </ext:TextField>
                                            </Items>
                                        </ext:Container>
                                        <ext:Container ID="Container1" runat="server" Layout="Column">
                                            <Items>
                                                <ext:TextField runat="server" ID="pif_contactperson" FieldLabel="联络人" ColumnWidth=".2" LabelWidth="80"
                                                    LabelAlign="Right" Padding="2" >
                                                </ext:TextField>
                                                <ext:TextField runat="server" ID="pif_contact" FieldLabel="联络人电话" ColumnWidth=".2"
                                                    LabelWidth="80" LabelAlign="Right" Padding="2" >
                                                </ext:TextField>
                                                <ext:DateField ID="info_survey_date" runat="server" FieldLabel="调查日期" ColumnWidth=".2"
                                                     Padding="2" LabelWidth="80" LabelAlign="Right" Format="yyyy-MM-dd">
                                                </ext:DateField>
                                            </Items>
                                        </ext:Container>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                            <ext:Button ID="Button2" runat="server" Icon="Disk" Text="回上一页" Width="100">
                            <DirectEvents>
                            <Click OnEvent="Btn_Back_Click"></Click>
                        </DirectEvents>
                        </ext:Button>
                        <ext:Button ID="Button1" runat="server" Icon="Disk" Text="存盘" Width="100">
                            <DirectEvents>
                            <Click OnEvent="Btn_Submit_Click"></Click>
                        </DirectEvents>
                        </ext:Button>
                         
                    </Buttons>
                        </ext:Panel>
                    </Items>
                    
                </ext:FormPanel>
            </Items>
        </ext:Viewport>
    </div>
    </form>
</body>
</html>
