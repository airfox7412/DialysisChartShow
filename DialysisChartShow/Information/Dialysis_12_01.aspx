<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_12_01.aspx.cs"
    Inherits="Dialysis_Chart_Show.Information.Dialysis_12_01" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">
<html xmlns="http://www.w3.org/1999/xhtml">
<head id="Head1" runat="server">
    <title></title>
    <script language="javascript" type="text/javascript">
        var showResult = function (btn) {
            Ext.Msg.notify("Button Click", "You clicked the " + btn + " button");
        };

        var showResultText = function (btn, text) {
            Ext.Msg.notify("Button Click", "You clicked the " + btn + 'button and entered the text "' + text + '".');
        };
    </script>
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
        <ext:ResourceManager ID="ResourceManager2" runat="server">
        </ext:ResourceManager>
        <ext:Viewport ID="Viewport1" runat="server" Layout="Fit">
            <Items>
                <ext:FormPanel ID="FormPanel1" runat="server" Title="透析用水" AutoScroll="true">
                    <Items>
                        <ext:Panel ID="Panel0" runat="server" Border="false" Header="false" ButtonAlign="Center">
                            <Items>
                                <ext:Panel ID="Panel4" runat="server" Border="false" Header="false">
                                    <Items>
                                        <ext:DateField ID="info_date" runat="server" FieldLabel="检查日期" Format="yyyy-MM-dd">
                                        </ext:DateField>
                                      
                                        <ext:FieldSet ID="FieldSet1" runat="server" Flex="1" Title="细菌菌落计数" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_11" runat="server" FieldLabel="反渗机入口" IndicatorText="CFU/ml"/>
                                                <ext:TextArea  ID="are_12" runat="server" FieldLabel="反渗机入口描述" Width="400"/>
                                                <ext:TextField ID="txt_13" runat="server" FieldLabel="反渗机出口" IndicatorText="CFU/ml"/>
                                                <ext:TextArea  ID="are_14" runat="server" FieldLabel="反渗机出口描述" Width="400"/>
                                                <ext:TextField ID="txt_15" runat="server" FieldLabel="透析机入口" IndicatorText="CFU/ml"/>
                                                <ext:TextArea  ID="are_16" runat="server" FieldLabel="透析机入口描述" Width="400" />
                                             </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet2" runat="server" Flex="1" Title="內毒素檢測" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                                <ext:TextField ID="txt_21" runat="server" FieldLabel="反渗机入口" IndicatorText="EU/ml"/>
                                                <ext:TextArea  ID="are_22" runat="server" FieldLabel="反渗机入口描述" Width="400" />
                                                <ext:TextField ID="txt_23" runat="server" FieldLabel="反渗机出口" IndicatorText="EU/ml"/>
                                                <ext:TextArea  ID="are_24" runat="server" FieldLabel="反渗机出口描述" Width="400"/>
                                                <ext:TextField ID="txt_25" runat="server" FieldLabel="透析机入口" IndicatorText="EU/ml"/>
                                                <ext:TextArea  ID="are_26" runat="server" FieldLabel="透析机入口描述" Width="400"/>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet3" runat="server" Flex="1" Title="余氯檢測" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                               <ext:TextField ID="txt_31" runat="server" FieldLabel="游离氯" IndicatorText="mg/L或ppm"/>
                                               <ext:TextArea  ID="are_32" runat="server" FieldLabel="游离氯描述" Width="400"/>
                                               <ext:TextField ID="txt_33" runat="server" FieldLabel="总氯" IndicatorText="mg/L或ppm"/>
                                               <ext:TextArea  ID="are_34" runat="server" FieldLabel="总氯描述" Width="400"/>
                                            </Items>
                                        </ext:FieldSet>

                                        <ext:FieldSet ID="FieldSet4" runat="server" Flex="1" Title="硬度檢測" Layout="AnchorLayout" Collapsible="true" Collapsed="true">
                                            <Defaults>
                                                <ext:Parameter Name="HideEmptyLabel" Value="false" Mode="Raw" />
                                            </Defaults>
                                            <Items>
                                               <ext:TextField ID="txt_41" runat="server" FieldLabel="硬度" IndicatorText="mg/L或ppm"/>
                                               <ext:TextArea  ID="are_42" runat="server" FieldLabel="硬度描述" Width="400"/>
                                               <ext:TextField ID="txt_43" runat="server" FieldLabel="硬度gpg" IndicatorText="gpg" />
                                               <ext:TextArea  ID="are_44" runat="server" FieldLabel="硬度gpg描述" Width="400" />    
                                            </Items>
                                        </ext:FieldSet>
                                       
                                    </Items>
                                </ext:Panel>
                            </Items>
                            <Buttons>
                                <ext:Button ID="btn_save" runat="server" Icon="Disk" Text="保存" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Submit_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="btn_clear" runat="server" Icon="Disk" Text="重置" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Clear_Click">
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Btn_Close" runat="server" Icon="Disk" Text="关闭" Width="100">
                                    <DirectEvents>
                                        <Click OnEvent="Btn_Close_Click">
                                        </Click>
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
