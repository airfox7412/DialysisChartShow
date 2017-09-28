<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_09_02_detail_Editor.ascx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_09_02_detail_Editor" %>
<style type="text/css">
    .Text-blue .x-form-field
    {
        color: blue;
    }
    
    .Text-red .x-form-field
    {
        color: red;
    }
    
    .Label .x-form-item-label-text
    {
        color:White;
    }
    
    .Text-Black .x-form-field
    {
        color:Black;
        font-weight:bold;        
    }
</style>
<ext:Window ID="DetailsWindow" runat="server" Icon="Group" Title="补登录" Width="350" Height="500" AutoShow="false" Modal="true" Hidden="true" Layout="Fit">
    <Items>
        <ext:FormPanel ID="DataListInfo" runat="server" Title="净化过程明细" Header="false" DefaultAnchor="100%" BodyPadding="5" AutoScroll="true" UI="Primary">
            <Items>
                <ext:TextField ID="OldTime" runat="server" Hidden="true" Cls="Text-red" />
                <ext:TextField ID="PationID" runat="server" FieldLabel="身分证号" ReadOnly="true" Cls="Text-blue" LabelCls="Label" />
                <ext:TextField ID="DialysisDate" runat="server" FieldLabel="日期" ReadOnly="true" Cls="Text-blue" LabelCls="Label" />
                <ext:TextField ID="DialysisTime" runat="server" FieldLabel="时间" Name="DialysisTime" Cls="Text-red" LabelCls="Label" />
                <ext:TextField ID="diagno" runat="server" FieldLabel="电导" Name="diagno" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column4" runat="server" FieldLabel="温度" Name="Column4" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column2" runat="server" FieldLabel="已超滤 kg" Name="Column2" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column23" runat="server" FieldLabel="超滤率 ml/hr" Name="Column23" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column3" runat="server" FieldLabel="跨膜压 mmHg" Name="Column3" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column1" runat="server" FieldLabel="静脉压 mmHg" Name="Column1" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column5" runat="server" FieldLabel="血流量 ml/min" Name="Column5" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column6" runat="server" FieldLabel="T °C" Name="Column6" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column7" runat="server" FieldLabel="P 次/分" Name="Column7" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column8" runat="server" FieldLabel="R 次/分" Name="Column8" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column9" runat="server" FieldLabel="BP mmHg" Name="Column9" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column10" runat="server" FieldLabel="病情及处理" Name="Column10" LabelCls="Label" Cls="Text-Black" />
                <ext:TextField ID="Column11" runat="server" FieldLabel="user" Hidden="true" />
            </Items>
        </ext:FormPanel>
    </Items>
    <Buttons>                                                                               
        <ext:Button ID="BtnDel" runat="server" Text="删除" Icon="Delete" Width="80" UI="Danger">
            <DirectEvents>
                <Click OnEvent="BtnDel_Click">
                    <ExtraParams>
                        <ext:Parameter Name="PationID" Value="#{PationID}.getValue()" Mode="Raw" />
                        <ext:Parameter Name="DialysisDate" Value="#{DialysisDate}.getValue()" Mode="Raw" />
                        <ext:Parameter Name="DialysisTime" Value="#{DialysisTime}.getValue()" Mode="Raw" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="SaveButton" runat="server" Text="储存" Icon="Disk" UI="Success">
            <DirectEvents>
                <Click OnEvent="SaveDataList" Failure="Ext.MessageBox.alert('Saving failed', 'Error during ajax event');">
                    <EventMask ShowMask="true" Target="CustomTarget" CustomTarget="={#{DetailsWindow}.body}" />
                    <ExtraParams>
                        <ext:Parameter Name="PationID" Value="#{PationID}.getValue()" Mode="Raw" />
                        <ext:Parameter Name="DialysisDate" Value="#{DialysisDate}.getValue()" Mode="Raw" />
                        <ext:Parameter Name="DialysisTime" Value="#{DialysisTime}.getValue()" Mode="Raw" />
                    </ExtraParams>
                </Click>
            </DirectEvents>
        </ext:Button>
        <ext:Button ID="CancelButton" runat="server" Text="取消" Icon="Cancel" UI="Success">
            <Listeners>
                <Click Handler="this.up('window').hide();" />
            </Listeners>
        </ext:Button>
    </Buttons>
</ext:Window>
