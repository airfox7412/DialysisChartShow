<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_04_02.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_04_02" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>辅助检查</title>
    <style type="text/css">        
        table, th, td {
            border-width:1px;
            border-style: outset;
            border-color:Gray;            
        }
        .trheadcolor
        {
            background-color:#5ABCE0;
        }
        p {
            display: block;
            font-size:14px;
            font-weight:bold; 
            margin:5px 5px 5px 5px;           
        }
    </style>
</head>
<body>
    <form id="form1" runat="server">
    <div>
        <ext:ResourceManager ID="ResourceManager2" runat="server" />
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="辅助检查" AutoScroll="true" ButtonAlign="Center" >
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="胸部X线检查" Layout="AnchorLayout" Collapsible="true" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:DateField ID="dat_1" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd" />
                                                <ext:Image ID="Image1" runat="server" ImageUrl="" Width="500" Height="300" Hidden="true" PaddingSpec="0 0 5 0" />
                                                <ext:TextField ID="txt_32" runat="server" FieldLabel="图片路径" ReadOnly="true" Hidden="true" />
                                                <ext:TextArea ID="are_33" runat="server" FieldLabel="诊断结果" Width="500" />
                                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:FileUploadField ID="FileUploadField1" runat="server" FieldLabel="选择上传图片" ButtonText="选择" Icon="ImageAdd" Width="310" />
                                                        <ext:Label ID="Label1" runat="server" Width="20" />
                                                        <ext:Button ID="SaveButton1" runat="server" Text="上传" Icon="ImageStar"  >
                                                            <DirectEvents>
                                                                <Click OnEvent="Upload_Click1"
                                                                       Before="if (#{FileUploadField1}.getValue()=='') { return false; } 
                                                                               Ext.Msg.wait('图片上传中...', '上传中');"
                                                                       Failure="Ext.Msg.show({ 
                                                                           title   : '错误', 
                                                                           msg     : '上传过程发生错误', 
                                                                           minWidth: 200, 
                                                                           modal   : true, 
                                                                           icon    : Ext.Msg.ERROR, 
                                                                           buttons : Ext.Msg.OK 
                                                                           });">
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                        <ext:Label ID="Label2" runat="server" Width="20" />
                                                        <ext:Checkbox ID="Checkbox1" runat="server" BoxLabel="覆盖存在" />
                                                    </Items>
                                                </ext:Container>
                                                

                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="心电图检查" Layout="AnchorLayout" Collapsible="true" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:DateField ID="dat_5" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd" />
                                                <ext:Image ID="Image2" runat="server" ImageUrl="" Width="500" Height="300" Hidden="true" PaddingSpec="0 0 5 0"/>
                                                <ext:TextField ID="txt_34" runat="server" FieldLabel="图片路径" ReadOnly="true" Hidden="true" />
                                                <ext:TextArea ID="are_35" runat="server" FieldLabel="诊断结果" Width="500" />
                                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:FileUploadField ID="FileUploadField2" runat="server" FieldLabel="选择上传图片" ButtonText="选择" Icon="ImageAdd" Width="310" />
                                                        <ext:Label ID="Label3" runat="server" Width="20" />
                                                        <ext:Button ID="SaveButton2" runat="server" Text="上传" Icon="ImageStar"  >
                                                            <DirectEvents>
                                                                <Click OnEvent="Upload_Click2"
                                                                       Before="if (#{FileUploadField2}.getValue()=='') { return false; } 
                                                                               Ext.Msg.wait('图片上传中...', '上传中');"
                                                                       Failure="Ext.Msg.show({ 
                                                                           title   : '错误', 
                                                                           msg     : '上传过程发生错误', 
                                                                           minWidth: 200, 
                                                                           modal   : true, 
                                                                           icon    : Ext.Msg.ERROR, 
                                                                           buttons : Ext.Msg.OK 
                                                                           });">
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                        <ext:Label ID="Label4" runat="server" Width="20" />
                                                        <ext:Checkbox ID="Checkbox2" runat="server" BoxLabel="覆盖存在" />
                                                    </Items>
                                                </ext:Container>
                                                

                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="超声心动图检查" Layout="AnchorLayout" Collapsible="true" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>                                                                                                
                                                <ext:DateField ID="dat_8" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd" />
                                                <ext:Image ID="Image3" runat="server" ImageUrl="" Width="500" Height="300" Hidden="true" PaddingSpec="0 0 5 0" />
                                                <ext:TextField ID="txt_36" runat="server" FieldLabel="图片路径" ReadOnly="true" Hidden="true" />
                                                <ext:TextArea ID="are_37" runat="server" FieldLabel="诊断结果" Width="500" />
                                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:FileUploadField ID="FileUploadField3" runat="server" FieldLabel="选择上传图片" ButtonText="选择" Icon="ImageAdd" Width="310" />
                                                        <ext:Label ID="Label5" runat="server" Width="20" />
                                                        <ext:Button ID="SaveButton3" runat="server" Text="上传" Icon="ImageStar"  >
                                                            <DirectEvents>
                                                                <Click OnEvent="Upload_Click3"
                                                                       Before="if (#{FileUploadField3}.getValue()=='') { return false; } 
                                                                               Ext.Msg.wait('图片上传中...', '上传中');"
                                                                       Failure="Ext.Msg.show({ 
                                                                           title   : '错误', 
                                                                           msg     : '上传过程发生错误', 
                                                                           minWidth: 200, 
                                                                           modal   : true, 
                                                                           icon    : Ext.Msg.ERROR, 
                                                                           buttons : Ext.Msg.OK 
                                                                           });">
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                        <ext:Label ID="Label6" runat="server" Width="20" />
                                                        <ext:Checkbox ID="Checkbox3" runat="server" BoxLabel="覆盖存在" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="颈动脉超声" Layout="AnchorLayout" Collapsible="true" >
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>                                                                                                
                                                <ext:DateField ID="dat_26" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd" />
                                                <ext:Image ID="Image4" runat="server" ImageUrl="" Width="500" Height="300" Hidden="true" PaddingSpec="0 0 5 0" />
                                                <ext:TextField ID="txt_38" runat="server" FieldLabel="图片路径" ReadOnly="true" Hidden="true" />
                                                <ext:TextArea ID="are_39" runat="server" FieldLabel="诊断结果" Width="500" />
                                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:FileUploadField ID="FileUploadField4" runat="server" FieldLabel="选择上传图片" ButtonText="选择" Icon="ImageAdd" Width="310" />
                                                        <ext:Label ID="Label7" runat="server" Width="20" />
                                                        <ext:Button ID="SaveButton4" runat="server" Text="上传" Icon="ImageStar"  >
                                                            <DirectEvents>
                                                                <Click OnEvent="Upload_Click4"
                                                                       Before="if (#{FileUploadField4}.getValue()=='') { return false; } 
                                                                               Ext.Msg.wait('图片上传中...', '上传中');"
                                                                       Failure="Ext.Msg.show({ 
                                                                           title   : '错误', 
                                                                           msg     : '上传过程发生错误', 
                                                                           minWidth: 200, 
                                                                           modal   : true, 
                                                                           icon    : Ext.Msg.ERROR, 
                                                                           buttons : Ext.Msg.OK 
                                                                           });">
                                                                </Click>
                                                            </DirectEvents>
                                                        </ext:Button>
                                                        <ext:Label ID="Label8" runat="server" Width="20" />
                                                        <ext:Checkbox ID="Checkbox4" runat="server" BoxLabel="覆盖存在" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:FieldSet>
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_clear" runat="server" Icon="ArrowTurnLeft" Text="返回" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Clear_Click" />
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Btn_Close" runat="server" Icon="Disk" Text="关闭" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click" />
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
