<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="Dialysis_081.aspx.cs" Inherits="Dialysis_Chart_Show.Information.Dialysis_081" %>

<!DOCTYPE html PUBLIC "-//W3C//DTD XHTML 1.0 Transitional//EN" "http://www.w3.org/TR/xhtml1/DTD/xhtml1-transitional.dtd">

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title>新诊断信息</title>
    <link href="../css/grid.css" rel="stylesheet"/>
    <style type = "text/css">
    .large-font 
    {
        font-size: 16px !important; 
        height: 22px !important;
    }
    
    .x-panel-header-text {
        font-size: 16px;
        font-weight: bold;
        line-height: 20px;
    }
    
    .TextField-Hide .x-form-field
    {
        color: White;
        background-image: none;
        border-color:White;
    }
    
    .x-grid-row-over .x-grid-cell-inner 
    {
        font-weight : bold;
    }
    </style>

    <script type="text/javascript" >
        var getRowClass = function (record) {
            if (record.phantom) {
                return "new-row";
            }
            if (record.dirty) {
                return "dirty-row";
            }
        };

        var insertRecord = function () {
            var grid = <%= GridPanel1.ClientID %>;
            grid.store.insert(0, {});
            grid.getView().focusRow(0);
            grid.editingPlugin.startEdit(grid.store.getAt(0), grid.columns[0]);
        };

        var departmentRenderer = function (value) {
            var r = App.StoreCombo.getById(value);

            if (Ext.isEmpty(r)) {
                return "";
            }
            return r.data.Name;
        };
    </script>
</head>
<body>
    <form id="form1" runat="server">

        <ext:ResourceManager ID="ResourceManager1" runat="server" /> 
        <ext:Viewport ID="Viewport1" runat="server" Layout="border" >
            <Items>
                <ext:Panel ID="PanelL" runat="server" Title ="新诊断信息" Region="West" Width="200" MinWidth="200" bodyStyle="background-color:#dfe8f6"
                    MaxWidth="200" Split="true" Collapsible="true" Cls=".myTitle" Weight="90" >
                    <Items>
                        <ext:TextField ID="txtNODE_ID" runat="server" />
                        <ext:TextField ID="txtNODE_TEXT" runat="server" />
                        <ext:TreePanel ID="TreePanel1" runat="server" Cls=".myTreePanel" OnReadData="NodeLoad" RootVisible="false"  >
                            <DirectEvents>
                                <ItemClick OnEvent="Node_Click">
                                    <ExtraParams>
                                        <ext:Parameter Name="rID" Value="record.data.id" Mode="Raw" />
                                        <ext:Parameter Name="rTEXT" Value="record.data.text" Mode="Raw" />
                                    </ExtraParams>
                                </ItemClick>
                            </DirectEvents>
                        </ext:TreePanel> 
                    </Items>
                </ext:Panel>

                <ext:Panel  ID="Panel_5" runat="server" Region="North" Weight="70" Title="　" Hidden="true" >
                    <Items>
                        <ext:Container ID="Container14" runat="server" Layout="HBoxLayout" PaddingSpec="4 4 4 4" >
                            <Items>
                                <ext:DateField ID="info_date" runat="server" FieldLabel="时间日期" Format="yyyy-MM-dd" PaddingSpec="0 20 0 0" />
                                <ext:Button ID="cmdSAVE" runat="server" Text="保存" Icon="Disk" Width="90" OnDirectClick="cmdSAVE_Click" />
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel >

                <ext:Panel ID="Panel_1" runat="server" Title="一、临床诊断" Border="false" Region="Center" Hidden="true" Header="false" >
                    <Items>
                        <ext:Container ID="Container8" runat="server" PaddingSpec="0 0 0 20" >
                            <Items>
                                <ext:Radio ID="opt_1_1" runat="server" BoxLabel="A. 急性肾损伤" Name="opt_1" />
                                <ext:Radio ID="opt_1_2" runat="server" BoxLabel="B. 慢性肾衰竭" Name="opt_1" />
                                <ext:Radio ID="opt_1_3" runat="server" BoxLabel="C. 慢性肾衰竭基础上急性加重" Name="opt_1" />
                                <ext:Container ID="Container5" runat="server" Layout="ColumnLayout" >
                                    <Items>
                                        <ext:Radio ID="opt_1_4" runat="server" BoxLabel="D. 其他" Name="opt_1" OnDirectCheck="jj" />
                                        <ext:TextField ID="txt_2" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel_2" runat="server" Title="二、合并症诊断" Border="false" Region="Center" Hidden="true" Header="false" >
                    <Items>
                        <ext:Container ID="Container20" runat="server" PaddingSpec="4 0 0 20" >
                            <Items>
                                <ext:Label ID="Label7" runat="server" Text="A. 是否合并糖尿病" />
                                <ext:Container ID="Container1" runat="server" Layout="ColumnLayout" PaddingSpec="0 0 4 20" >
                                    <Items>
                                        <ext:Radio ID="opt_3_1" runat="server" BoxLabel="否" Name="opt_3" />
                                        <ext:Radio ID="opt_3_2" runat="server" BoxLabel="是" Name="opt_3" PaddingSpec="0 0 0 20" />
                                    </Items>
                                </ext:Container>

                                <ext:Label ID="Label1" runat="server" Text="B. 是否合并肿瘤" />
                                <ext:Container ID="Container2" runat="server" Layout="ColumnLayout" PaddingSpec="0 0 4 20" >
                                    <Items>
                                        <ext:Radio ID="opt_4_1" runat="server" BoxLabel="否" Name="opt_4" />
                                        <ext:Radio ID="opt_4_2" runat="server" BoxLabel="是" Name="opt_4" PaddingSpec="0 0 0 20" />
                                    </Items>
                                </ext:Container>
                                <ext:Label ID="Label2" runat="server" Text="C. 是否诊断糖尿病肾病" />
                                <ext:Container ID="Container3" runat="server" Layout="ColumnLayout" PaddingSpec="0 0 4 20" >
                                    <Items>
                                        <ext:Radio ID="opt_5_1" runat="server"  BoxLabel="否" Name="opt_5" />
                                        <ext:Radio ID="opt_5_2" runat="server" BoxLabel="是" Name="opt_5" PaddingSpec="0 0 0 20" />
                                    </Items>
                                </ext:Container>
                                <ext:Label ID="Label3" runat="server" Text="D. 是否合并其他慢性疾病" />
                                <ext:Container ID="Container4" runat="server" Layout="ColumnLayout" PaddingSpec="0 0 4 20" >
                                    <Items>
                                        <ext:Radio ID="opt_6_1" runat="server" BoxLabel="否" Name="opt_6" />
                                        <ext:Radio ID="opt_6_2" runat="server" BoxLabel="是" Name="opt_6" PaddingSpec="0 0 0 20" />
                                    </Items>
                                </ext:Container>
                                <ext:Label ID="Label4" runat="server" Text="E. 是否曾选择其他肾脏替代治疗" />
                                <ext:Container ID="Container21" runat="server" PaddingSpec="0 0 4 20" >
                                    <Items>
                                        <ext:Checkbox ID="chk_7_1" runat="server" BoxLabel="肾移植"  />
                                        <ext:Checkbox ID="chk_7_2" runat="server" BoxLabel="腹膜透析"  />
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel_3" runat="server" Title="三、病因诊断" Border="false" AutoScroll="true" Region="Center" Hidden="true" Header="false" >
                    <Items>
                        <ext:Button ID="cmdEXP" runat="server" Text="全部展开" Icon="Accept" Width="100">
                            <DirectEvents>
                                <Click OnEvent="OnExpansion" />
                            </DirectEvents>
                        </ext:Button>
                        <ext:Container ID="Container19" runat="server" PaddingSpec="0 0 0 20" >
                            <Items>
                                <ext:Radio ID="opt_8_1" runat="server" BoxLabel="A. 急性肾衰竭（ICD：N17.901/903）" Name="opt_8" OnDirectCheck="cmd_A" />
                                <ext:Container ID="Container_A" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                    <Items>
                                        <ext:Checkbox ID="chk_9_1" runat="server" BoxLabel="a. 肾前性容量不足" OnDirectCheck="cmd_Aa" />
                                        <ext:Container ID="Container_Aa" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Checkbox ID="chk_10_1" runat="server" BoxLabel="1. 休克（ICD：R57.901）" OnDirectCheck="cmd_Aa1" />
                                                <ext:Container ID="Container_Aa1" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_11_1" runat="server" BoxLabel="a) R57.001 心源性休克" Name="opt_11" />
                                                        <ext:Radio ID="opt_11_2" runat="server" BoxLabel="b) R57.102 低血容量休克" Name="opt_11" />
                                                        <ext:Radio ID="opt_11_3" runat="server" BoxLabel="c) R57.801 感染性休克" Name="opt_11" />
                                                        <ext:Radio ID="opt_11_4" runat="server" BoxLabel="d) R57.802 中毒性休克" Name="opt_11" />
                                                        <ext:Radio ID="opt_11_5" runat="server" BoxLabel="e) T79.401 创伤性休克" Name="opt_11" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Checkbox ID="chk_10_2" runat="server" BoxLabel="2. 低血容量（E86.X01）" OnDirectCheck="cmd_Aa2" />
                                                <ext:Container ID="Container_Aa2" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_12_1" runat="server" BoxLabel="a) Y60.951 手术和医疗中意外切割、针刺、穿孔或出血 NOS" Name="opt_12" />
                                                        <ext:Container ID="Container11" runat="server" Layout="ColumnLayout" >
                                                            <Items>
                                                                <ext:Radio ID="opt_12_2" runat="server" BoxLabel="b) 其他" Name="opt_12" OnDirectCheck="kk" />
                                                                <ext:TextField ID="txt_13" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                            </Items>
                                                        </ext:Container>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>

                                        <ext:Checkbox ID="chk_9_2" runat="server" BoxLabel="b. 肾实质性损伤" OnDirectCheck="cmd_Ab" />
                                        <ext:Container ID="Container_Ab" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Checkbox ID="chk_14_1" runat="server" BoxLabel="1. 肾血管疾病" OnDirectCheck="cmd_Ab1" />
                                                <ext:Container ID="Container_Ab1" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_15_1" runat="server" BoxLabel="a) N28.001 肾动脉栓塞" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_2" runat="server" BoxLabel="b) I67.253 肾动脉粥样硬化症" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_3" runat="server" BoxLabel="c) I70.101 肾动脉狭窄" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_4" runat="server" BoxLabel="d) I72.201 肾动脉动脉瘤" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_5" runat="server" BoxLabel="e) I77.851 肾血管炎（大动脉炎）" Name="opt_15" />
                                                        <ext:Radio ID="opt_15_6" runat="server" BoxLabel="f) I82.301 肾静脉栓塞" Name="opt_15" />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Checkbox ID="chk_14_2" runat="server" BoxLabel="2. 肾微血管疾病（肾小球疾病）" OnDirectCheck="cmd_Ab2" />
                                                <ext:Container ID="Container_Ab2" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_16_1" runat="server" BoxLabel="a) N00.901 急性肾小球肾炎" Name="opt_16" OnDirectCheck="cmd_Ab2a" />
                                                        <ext:Container ID="Container_Ab2a" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                            <Items>
                                                                <ext:Radio ID="opt_17_1" runat="server" BoxLabel="1) N00.902 急性肾炎" Name="opt_17"  />
                                                                <ext:Radio ID="opt_17_2" runat="server" BoxLabel="2) N00.903 急性肾炎综合症" Name="opt_17"  />
                                                                <ext:Radio ID="opt_17_3" runat="server" BoxLabel="3) N00.501 急性膜性增殖性肾小球肾炎" Name="opt_17"  />
                                                                <ext:Radio ID="opt_17_4" runat="server" BoxLabel="4) N00.801 急性肾炎伴坏死性肾小球肾炎损害" Name="opt_17"  />
                                                                <ext:Radio ID="opt_17_5" runat="server" BoxLabel="5) N00.802 急性增殖性肾小球肾炎" Name="opt_17"  />
                                                                <ext:Radio ID="opt_17_6" runat="server" BoxLabel="6) N00.904 链球菌感染后急性肾小球肾炎" Name="opt_17"  />
                                                            </Items>
                                                        </ext:Container>
                                                        
                                                        <ext:Radio ID="opt_16_2" runat="server" BoxLabel="b) N01.905 急进型肾小球肾炎" Name="opt_16" OnDirectCheck="cmd_Ab2b" />
                                                        <ext:Container ID="Container_Ab2b" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                            <Items>
                                                                <ext:Radio ID="opt_18_1" runat="server" BoxLabel="1) N01.151 急进型局灶性肾炎综合症" Name="opt_18"  />
                                                                <ext:Radio ID="opt_18_2" runat="server" BoxLabel="2) N01.951 急进型肾炎综合症" Name="opt_18"  />
                                                                <ext:Radio ID="opt_18_3" runat="server" BoxLabel="3) N01.952 急进型肾小球病" Name="opt_18"  />
                                                                <ext:Radio ID="opt_18_4" runat="server" BoxLabel="4) N01.953 急进型肾炎" Name="opt_18"  />
                                                                <ext:Radio ID="opt_18_5" runat="server" BoxLabel="5) N05.903 血管炎性肾小球肾炎" Name="opt_18"  />
                                                            </Items>
                                                        </ext:Container>
                                                        <ext:Radio ID="opt_16_3" runat="server" BoxLabel="c) N05.904 药物性肾炎"  Name="opt_16"  />
                                                        <ext:Radio ID="opt_16_4" runat="server" BoxLabel="d) N17.052 急性肾小球坏死"  Name="opt_16"  />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Checkbox ID="chk_14_3" runat="server" BoxLabel="3. 肾小管疾病" OnDirectCheck="cmd_Ab3" />
                                                <ext:Container ID="Container_Ab3" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_19_1" runat="server" BoxLabel="a) N12.X02 肾小管病变" Name="opt_19"  />
                                                        <ext:Radio ID="opt_19_2" runat="server" BoxLabel="b) N17.001 急性肾小管坏死" Name="opt_19"  />
                                                        <ext:Radio ID="opt_19_3" runat="server" BoxLabel="c) N17.051 急性肾衰竭伴有肾小管坏死" Name="opt_19"  />
                                                        <ext:Radio ID="opt_19_4" runat="server" BoxLabel="d) 外源性肾毒素致肾小管病变" Name="opt_19" OnDirectCheck="cmd_Ab3d" />
                                                        <ext:Container ID="Container_Ab3d" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                            <Items>
                                                                <ext:Radio ID="opt_20_1" runat="server" BoxLabel="1) N14.051 镇痛剂肾病[镇痛药滥用综合症" Name="opt_20"  />
                                                                <ext:Radio ID="opt_20_2" runat="server" BoxLabel="2) N14.151 其他药物、生物制品引起的肾病变" Name="opt_20"  />
                                                                <ext:Radio ID="opt_20_3" runat="server" BoxLabel="3) N14.251 药物、生物制品引起的肾病变 NOS" Name="opt_20"  />
                                                                <ext:Radio ID="opt_20_4" runat="server" BoxLabel="4) N14.252 药物中毒性肾病变" Name="opt_20"  />
                                                                <ext:Radio ID="opt_20_5" runat="server" BoxLabel="5) N14.351 重金属诱发肾病变" Name="opt_20"  />
                                                                <ext:Radio ID="opt_20_6" runat="server" BoxLabel="6) N14.451 毒性肾病变 NEC" Name="opt_20"  />
                                                            </Items>
                                                        </ext:Container>
                                                        <ext:Radio ID="opt_19_5" runat="server" BoxLabel="e) 内源性肾毒素致肾小管病变" Name="opt_19" OnDirectCheck="cmd_Ab3e" />
                                                        <ext:Container ID="Container_Ab3e" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                            <Items>
                                                                <ext:Radio ID="opt_21_1" runat="server" BoxLabel="1) N28.905 尿酸性肾病（尿酸\肿瘤溶解）" Name="opt_21"  />
                                                                <ext:Radio ID="opt_21_2" runat="server" BoxLabel="2) D59.301 溶血-尿毒症性综合症（血红蛋白尿）" Name="opt_21"  />
                                                                <ext:Radio ID="opt_21_3" runat="server" BoxLabel="3) T79.552 挤压后肾衰竭（肌球蛋白尿）" Name="opt_21"  />
                                                            </Items>
                                                        </ext:Container>
                                                    </Items>
                                                </ext:Container>
                                                
                                                <ext:Checkbox ID="chk_14_4" runat="server" BoxLabel="4. 肾间质疾病" OnDirectCheck="cmd_Ab4" />
                                                <ext:Container ID="Container_Ab4" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_22_1" runat="server" BoxLabel="a) N10.X52 急性传染性间质肾炎" Name="opt_22"  />
                                                        <ext:Radio ID="opt_22_2" runat="server" BoxLabel="b) N10.X53 急性肾小管－间质肾炎" Name="opt_22"  />
                                                        <ext:Radio ID="opt_22_3" runat="server" BoxLabel="c) N12.X01 间质性肾炎" Name="opt_22"  />
                                                    </Items>
                                                </ext:Container>

                                                <ext:Checkbox ID="chk_14_5" runat="server" BoxLabel="5. 其他" OnDirectCheck="cmd_Ab5" />
                                                <ext:Container ID="Container_Ab5" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Checkbox ID="chk_23_1" runat="server" BoxLabel="a) 肾脏手术后" OnDirectCheck="cmd_Ab5a" />
                                                        <ext:Container ID="Container_Ab5a" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                            <Items>
                                                                <ext:Radio ID="opt_24_1" runat="server" BoxLabel="1) 55.4 部分肾切除术" Name="opt_24"  />
                                                                <ext:Radio ID="opt_24_2" runat="server" BoxLabel="2) 55.5 全肾切除术" Name="opt_24"  />
                                                            </Items>
                                                        </ext:Container>
                                                        <ext:Checkbox ID="chk_23_2" runat="server" BoxLabel="b) 肾脏感染性疾病" OnDirectCheck="cmd_Ab5b" />
                                                        <ext:Container ID="Container_Ab5b" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                            <Items>
                                                                <ext:Radio ID="opt_25_1" runat="server" BoxLabel="1) N10.X01 急性肾盂肾炎" Name="opt_25"  />
                                                                <ext:Radio ID="opt_25_2" runat="server" BoxLabel="2) N15.901 肾感染" Name="opt_25"  />
                                                                <ext:Radio ID="opt_25_3" runat="server" BoxLabel="3) N15.902 肾周围感染" Name="opt_25"  />
                                                                <ext:Radio ID="opt_25_4" runat="server" BoxLabel="4) N15.951 肾周炎" Name="opt_25"  />
                                                            </Items>
                                                        </ext:Container>
                                                        <ext:Checkbox ID="chk_23_3" runat="server" BoxLabel="c) N17.851 其他急性肾衰竭" OnDirectCheck="cmd_Ab5c" />
                                                        <ext:Container ID="Container_Ab5c" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                            <Items>
                                                                <ext:Radio ID="opt_26_1" runat="server" BoxLabel="1) N17.101 肾皮质坏死" Name="opt_26"  />
                                                                <ext:Radio ID="opt_26_2" runat="server" BoxLabel="2) N17.151 急性肾皮质坏死" Name="opt_26"  />
                                                                <ext:Radio ID="opt_26_3" runat="server" BoxLabel="3) N17.152 缺血性肾皮质坏死" Name="opt_26"  />
                                                                <ext:Radio ID="opt_26_4" runat="server" BoxLabel="4) N17.153 急性肾衰竭伴急性肾皮质坏死" Name="opt_26"  />
                                                                <ext:Radio ID="opt_26_5" runat="server" BoxLabel="5) N17.252 急性肾衰竭伴有肾髓质坏死" Name="opt_26"  />
                                                            </Items>
                                                        </ext:Container>
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>

                                        <ext:Checkbox ID="chk_9_3" runat="server" BoxLabel="c. 肾后性" OnDirectCheck="cmd_Ac" />
                                        <ext:Container ID="Container_Ac" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Checkbox ID="chk_27_1" runat="server" BoxLabel="1. N13.801 梗阻性肾病" OnDirectCheck="cmd_Ac1" />
                                                <ext:Container ID="Container_Ac1" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_28_1" runat="server" BoxLabel="a) N20.901 泌尿系结石 NOS" Name="opt_28" />
                                                        <ext:Radio ID="opt_28_2" runat="server" BoxLabel="b) N42.951 前列腺疾患 NOS" Name="opt_28" />
                                                        <ext:Radio ID="opt_28_3" runat="server" BoxLabel="c) N13.301 肾盂积水 NOS" Name="opt_28" />
                                                        <ext:Radio ID="opt_28_4" runat="server" BoxLabel="d) N13.502 输尿管梗阻" Name="opt_28" />
                                                        <ext:Container ID="Container16" runat="server" Layout="ColumnLayout" >
                                                            <Items>
                                                                <ext:Radio ID="opt_28_5" runat="server" BoxLabel="e) 其他" Name="opt_28" OnDirectCheck="ll" />
                                                                <ext:TextField ID="txt_29" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                            </Items>
                                                        </ext:Container>
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container17" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:Checkbox ID="chk_27_2" runat="server" BoxLabel="2. N13.851 其他阻塞性尿路病（注明原因）" OnDirectCheck="mm" />
                                                        <ext:TextField ID="txt_30" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Container ID="Container18" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:Checkbox ID="chk_27_3" runat="server" BoxLabel="3. N13.852 其他反流性尿路病（注明原因）" OnDirectCheck="nn" />
                                                        <ext:TextField ID="txt_31" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>
                                
                                    </Items>
                                </ext:Container>
                                <ext:Radio ID="opt_8_2" runat="server" BoxLabel="B. 慢性肾衰竭（ICD：N18.901/905）" Name="opt_8" OnDirectCheck="cmd_B" />
                                <ext:Container ID="Container_B" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                    <Items>
                                        <ext:Checkbox ID="chk_32_1" runat="server" BoxLabel="a. 原发性肾脏疾病" OnDirectCheck="cmd_Ba" />
                                        <ext:Container ID="Container_Ba" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_33_1" runat="server" BoxLabel="1. 慢性肾小球肾炎 N03" Name="opt_33" OnDirectCheck="cmd_Ba1" />
                                                <ext:Container ID="Container_Ba1" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_34_1" runat="server" BoxLabel="a) N03.201 慢性(弥漫性)膜性肾小球肾炎" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_2" runat="server" BoxLabel="b) N03.301 慢性(弥漫性)肾小球膜性增殖性肾小球肾炎" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_3" runat="server" BoxLabel="c) N03.501 慢性膜性增殖性肾小球肾炎" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_4" runat="server" BoxLabel="d) N03.502 慢性膜性增殖性肾炎 NOS" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_5" runat="server" BoxLabel="e) N03.503 慢性(弥漫性)肾小球膜毛细血管性肾小球肾炎" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_6" runat="server" BoxLabel="f) N03.801 慢性增殖性肾小球肾炎" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_7" runat="server" BoxLabel="g) N03.802 慢性肾小球肾炎伴硬化性肾炎" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_8" runat="server" BoxLabel="h) N03.902 慢性肾小球肾炎，急性发作" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_9" runat="server" BoxLabel="i) N03.904 隐匿型肾小球肾炎" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_10" runat="server" BoxLabel="j) N03.951 慢性肾炎综合症" Name="opt_34" />
                                                        <ext:Radio ID="opt_34_11" runat="server" BoxLabel="k) N03.952 慢性肾病 NOS" Name="opt_34" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_33_2" runat="server" BoxLabel="2. 肾病综合症 N04" Name="opt_33" OnDirectCheck="cmd_Ba2" />
                                                <ext:Container ID="Container_Ba2" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_35_1" runat="server" BoxLabel="a) N04.001 肾病综合症伴微小病变型肾小球肾炎" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_2" runat="server" BoxLabel="b) N04.101 肾病综合症伴局灶硬化性肾小球肾炎" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_3" runat="server" BoxLabel="c) N04.201 肾病综合症伴膜性肾小球肾炎" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_4" runat="server" BoxLabel="d) N04.202 肾病型慢性膜性肾小球肾炎" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_5" runat="server" BoxLabel="e) N04.301 肾病综合症伴膜性增殖性肾小球肾炎" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_6" runat="server" BoxLabel="f) N04.401 肾病综合症伴有毛细血管增殖性肾炎" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_7" runat="server" BoxLabel="g) N04.801 肾病综合症伴增殖性肾小球肾炎" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_8" runat="server" BoxLabel="h) N04.802 原发性肾病综合症" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_9" runat="server" BoxLabel="i) N04.901 肾变病" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_10" runat="server" BoxLabel="j) N04.902 肾病型肾炎" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_11" runat="server" BoxLabel="k) N04.903 肾病综合症" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_12" runat="server" BoxLabel="l) N04.951 单纯性肾变病[类脂性病]" Name="opt_35" />
                                                        <ext:Radio ID="opt_35_13" runat="server" BoxLabel="m) N04.952 先天性肾变病综合症" Name="opt_35" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_33_3" runat="server" BoxLabel="3. IgA肾病 N02" Name="opt_33" OnDirectCheck="cmd_Ba3" />
                                                <ext:Container ID="Container_Ba3" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_36_1" runat="server" BoxLabel="a) N02.951 持续性血尿" Name="opt_36" />
                                                        <ext:Radio ID="opt_36_2" runat="server" BoxLabel="b) N02.952 复发性血尿" Name="opt_36" />
                                                        <ext:Radio ID="opt_36_3" runat="server" BoxLabel="c) N02.953 间歇性血尿" Name="opt_36" />
                                                        <ext:Radio ID="opt_36_4" runat="server" BoxLabel="d) N02.954 阵发性血尿" Name="opt_36" />
                                                        <ext:Radio ID="opt_36_5" runat="server" BoxLabel="e) N02.955 良性再生性血尿[家族性良性血尿]" Name="opt_36" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_33_4" runat="server" BoxLabel="4. 孤立性蛋白尿 N06" Name="opt_33" OnDirectCheck="cmd_Ba4" />
                                                <ext:Container ID="Container_Ba4" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_37_1" runat="server" BoxLabel="a) N06.051 孤立性蛋白尿伴有极微的病理改变的肾小球损害" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_2" runat="server" BoxLabel="b) N06.151 孤立性蛋白尿伴有局灶性和节段性透明变性或硬化的肾小球损害" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_3" runat="server" BoxLabel="c) N06.251 孤立性蛋白尿伴有(弥漫性)膜性肾小球损害" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_4" runat="server" BoxLabel="d) N06.351 孤立性蛋白尿伴有(弥漫性)肾小球膜性增殖性肾小球损害" Name="opt_37" />
                                                        <ext:Radio ID="opt_37_5" runat="server" BoxLabel="e) N06.951 孤立性蛋白尿伴有肾小球损害" Name="opt_37" />
                                                    </Items>
                                                </ext:Container>
                                                <ext:Radio ID="opt_33_5" runat="server" BoxLabel="5. 其他" Name="opt_33" OnDirectCheck="cmd_Ba5" />
                                                <ext:Container ID="Container_Ba5" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                                    <Items>
                                                        <ext:Radio ID="opt_38_1" runat="server" BoxLabel="a) N05.101 局灶性节段性增殖性肾小球肾炎" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_2" runat="server" BoxLabel="b) N05.102 局灶性肾炎" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_3" runat="server" BoxLabel="c) N05.103 局灶硬化性肾小球肾炎" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_4" runat="server" BoxLabel="d) N05.151 局灶性肾小球肾炎" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_5" runat="server" BoxLabel="e) N05.201 膜性肾病" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_6" runat="server" BoxLabel="f) N05.202 膜性肾小球肾病" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_7" runat="server" BoxLabel="g) N05.301 弥漫性肾小球膜性增殖性肾小球肾炎" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_8" runat="server" BoxLabel="h) N05.501 膜性增殖性肾小球肾炎 NOS" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_9" runat="server" BoxLabel="i) N05.502 弥漫性肾小球膜毛细血管性肾炎" Name="opt_38" />
                                                        <ext:Radio ID="opt_38_10" runat="server" BoxLabel="j) N05.801 IgM肾病" Name="opt_38" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>

                                        <ext:Checkbox ID="chk_32_2" runat="server" BoxLabel="b. 继发性肾脏疾病" OnDirectCheck="cmd_Bb" />
                                        <ext:Container ID="Container_Bb" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_39_1" runat="server" BoxLabel="1. 糖尿病肾病" Name="opt_39" />
                                                <ext:Radio ID="opt_39_2" runat="server" BoxLabel="2. 狼疮性肾炎" Name="opt_39" />
                                                <ext:Radio ID="opt_39_3" runat="server" BoxLabel="3. 高血压肾损害" Name="opt_39" />
                                                <ext:Radio ID="opt_39_4" runat="server" BoxLabel="4. 肥胖相关性肾病" Name="opt_39" />
                                                <ext:Radio ID="opt_39_5" runat="server" BoxLabel="5. 淀粉样变性" Name="opt_39" />
                                                <ext:Radio ID="opt_39_6" runat="server" BoxLabel="6. 多发骨髓瘤肾病" Name="opt_39" />
                                                <ext:Radio ID="opt_39_7" runat="server" BoxLabel="7. 狼疮性肾炎" Name="opt_39" />
                                                <ext:Radio ID="opt_39_8" runat="server" BoxLabel="8. 系统性血管炎肾脏损害" Name="opt_39" />
                                                <ext:Radio ID="opt_39_9" runat="server" BoxLabel="9. 过敏性紫癜性肾炎" Name="opt_39" />
                                                <ext:Radio ID="opt_39_10" runat="server" BoxLabel="10. 血栓性微血管病" Name="opt_39" />
                                                <ext:Radio ID="opt_39_11" runat="server" BoxLabel="11. 乙型肝炎病毒相关性肾炎" Name="opt_39" />
                                                <ext:Radio ID="opt_39_12" runat="server" BoxLabel="12. 丙型肝炎病毒相关性肾炎" Name="opt_39" />
                                                <ext:Radio ID="opt_39_13" runat="server" BoxLabel="13. HIV相关性肾损害" Name="opt_39" />
                                                <ext:Radio ID="opt_39_14" runat="server" BoxLabel="14. 流行性出血热肾损害" Name="opt_39" />
                                                <ext:Radio ID="opt_39_15" runat="server" BoxLabel="15. 干燥综合症肾损害" Name="opt_39" />
                                                <ext:Radio ID="opt_39_16" runat="server" BoxLabel="16. 硬皮病肾损害" Name="opt_39" />
                                                <ext:Radio ID="opt_39_17" runat="server" BoxLabel="17. 类风湿性关节炎和强直性脊柱炎肾损害" Name="opt_39" />
                                                <ext:Container ID="Container7" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:Radio ID="opt_39_18" runat="server" BoxLabel="18. 其他" Name="opt_39" OnDirectCheck="oo" />
                                                        <ext:TextField ID="txt_40" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>

                                        <ext:Checkbox ID="chk_32_3" runat="server" BoxLabel="c. 遗传性及先天性肾病" OnDirectCheck="cmd_Bc" />
                                        <ext:Container ID="Container_Bc" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_41_1" runat="server" BoxLabel="1. 多囊肾" Name="opt_41" />
                                                <ext:Radio ID="opt_41_2" runat="server" BoxLabel="2. Alport综合症" Name="opt_41" />
                                                <ext:Radio ID="opt_41_3" runat="server" BoxLabel="3. 薄基底膜肾病" Name="opt_41" />
                                                <ext:Radio ID="opt_41_4" runat="server" BoxLabel="4. 近端肾小管损害及Fanconi综合症" Name="opt_41" />
                                                <ext:Radio ID="opt_41_5" runat="server" BoxLabel="5. Bartter综合症" Name="opt_41" />
                                                <ext:Radio ID="opt_41_6" runat="server" BoxLabel="6. Fabry病" Name="opt_41" />
                                                <ext:Radio ID="opt_41_7" runat="server" BoxLabel="7. 脂蛋白肾病" Name="opt_41" />
                                                <ext:Container ID="Container13" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:Radio ID="opt_41_8" runat="server" BoxLabel="8. 其他" Name="opt_41" OnDirectCheck="ss" />
                                                        <ext:TextField ID="txt_42" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>

                                        <ext:Checkbox ID="chk_32_4" runat="server" BoxLabel="d. 肾小管间质疾病" OnDirectCheck="cmd_Bd" />
                                        <ext:Container ID="Container_Bd" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_43_1" runat="server" BoxLabel="1. 慢性肾小管间质性肾炎" Name="opt_43" />
                                                <ext:Radio ID="opt_43_2" runat="server" BoxLabel="2. 肾小管性酸中毒" Name="opt_43" />
                                                <ext:Radio ID="opt_43_3" runat="server" BoxLabel="3. 反流性肾病" Name="opt_43" />
                                                <ext:Radio ID="opt_43_4" runat="server" BoxLabel="4. 梗阻性肾病" Name="opt_43" />
                                                <ext:Radio ID="opt_43_5" runat="server" BoxLabel="5. 马兜铃酸肾病" Name="opt_43" />
                                                <ext:Radio ID="opt_43_6" runat="server" BoxLabel="6. 造影剂肾病" Name="opt_43" />
                                                <ext:Radio ID="opt_43_7" runat="server" BoxLabel="7. 重金属中毒性肾脏损害" Name="opt_43" />
                                                <ext:Radio ID="opt_43_8" runat="server" BoxLabel="8. 放射性肾病及抗肿瘤药物所致的肾损害" Name="opt_43" />
                                                <ext:Container ID="Container9" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:Radio ID="opt_43_9" runat="server" BoxLabel="9. 其他" Name="opt_43" OnDirectCheck="pp" />
                                                        <ext:TextField ID="txt_44" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>

                                        <ext:Checkbox ID="chk_32_5" runat="server" BoxLabel="e. 泌尿系统肿瘤" OnDirectCheck="cmd_Be" />
                                        <ext:Container ID="Container_Be" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_45_1" runat="server" BoxLabel="" Name="opt_45" />
                                            </Items>
                                        </ext:Container>

                                        <ext:Checkbox ID="chk_32_6" runat="server" BoxLabel="f. 泌尿系统感染和结石" OnDirectCheck="cmd_Bf" />
                                        <ext:Container ID="Container_Bf" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_46_1" runat="server" BoxLabel="1. 慢性肾盂肾炎" Name="opt_46" />
                                                <ext:Radio ID="opt_46_2" runat="server" BoxLabel="2. 泌尿系结核" Name="opt_46" />
                                                <ext:Radio ID="opt_46_3" runat="server" BoxLabel="3. 肾结石" Name="opt_46" />
                                                <ext:Container ID="Container10" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:Radio ID="opt_46_4" runat="server" BoxLabel="4. 其他" Name="opt_46" OnDirectCheck="qq" />
                                                        <ext:TextField ID="txt_47" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>

                                        <ext:Checkbox ID="chk_32_7" runat="server" BoxLabel="g. 肾脏切除术后" OnDirectCheck="cmd_Bg" />
                                        <ext:Container ID="Container_Bg" runat="server" PaddingSpec="0 0 0 20" Hidden="true" >
                                            <Items>
                                                <ext:Radio ID="opt_48_1" runat="server" BoxLabel="1. 肾脏肿瘤" Name="opt_48" />
                                                <ext:Radio ID="opt_48_2" runat="server" BoxLabel="2. 肾结核" Name="opt_48" />
                                                <ext:Radio ID="opt_48_3" runat="server" BoxLabel="3. 肾囊肿" Name="opt_48" />
                                                <ext:Radio ID="opt_48_4" runat="server" BoxLabel="4. 肾发育不良" Name="opt_48" />
                                                <ext:Container ID="Container12" runat="server" Layout="ColumnLayout" >
                                                    <Items>
                                                        <ext:Radio ID="opt_48_5" runat="server" BoxLabel="5. 其他" Name="opt_48" OnDirectCheck="rr" />
                                                        <ext:TextField ID="txt_49" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                                    </Items>
                                                </ext:Container>
                                            </Items>
                                        </ext:Container>

                                        <ext:Container ID="Container6" runat="server" Layout="ColumnLayout" >
                                            <Items>
                                                <ext:Checkbox ID="chk_32_8" runat="server" BoxLabel="h. 其他病因" OnDirectCheck="cmd_Bh" />
                                                <ext:TextField ID="txt_50" runat="server" Cls="TextField-Hide" PaddingSpec="0 0 0 20" />
                                            </Items>
                                        </ext:Container>
                                        
                                    </Items>
                                </ext:Container>
                            </Items>
                        </ext:Container>
                    </Items>
                </ext:Panel>

                <ext:Panel ID="Panel_4" runat="server" Title="四、肾脏病理诊断" Border="false" Region="Center" Hidden="true" >
                    <Items>
                        <ext:GridPanel ID="GridPanel1" runat="server" Title="肾脏病理诊断" Header="false" Height="223" ButtonAlign="Left" Cls="x-grid-custom">
                            <Store>
                                <ext:Store ID="Store1" runat="server" ItemID="EXAM_DATE"  >
                                    <Model>
                                        <ext:Model ID="Model1" runat="server" IDProperty="ROW_ID" ClientIdProperty="NEW_ID" >
                                            <Fields>
                                                <ext:ModelField Name="ROW_ID" />
                                                <ext:ModelField Name="EXAM_DATE" />
                                                <ext:ModelField Name="EXAM_NO" />
                                                <ext:ModelField Name="EXAM_RESULT" />
                                                <ext:ModelField Name="EXAM_DOCTOR" />
                                                <ext:ModelField Name="EXAM_HOSPITAL" />
                                            </Fields>
                                        </ext:Model>
                                    </Model>
                                    <Reader>
                                        <ext:ArrayReader />
                                    </Reader>
                                </ext:Store>
                            </Store>
                            <ColumnModel ID="ColumnModel1" runat="server" >
                                <Columns>
                                    <%--<ext:RowNumbererColumn ID="RowNumbererColumn1" runat="server" Width="35" Header="编号" />--%>
                                    <ext:DateColumn ID="Column119" Header="检查时间" runat="server" DataIndex="EXAM_DATE" Width="100" Format="yyyy-MM-dd" >
                                        <Editor>
                                            <ext:DateField ID="TextField16" runat="server" Format="yyyy-MM-dd" />
                                        </Editor>
                                    </ext:DateColumn>
                                    <ext:Column ID="Column120" Header="编号" runat="server" DataIndex="EXAM_NO" Width="100" >
                                        <Editor>
                                            <ext:TextField ID="TextField12" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column121" Header="结论" runat="server" DataIndex="EXAM_RESULT" Width="200" >
                                        <Editor>
                                            <ext:TextArea ID="TextField13" runat="server" Height="100" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column122" Header="医师姓名" runat="server" DataIndex="EXAM_DOCTOR" Width="100" >
                                        <%--<Renderer Fn="departmentRenderer" />--%>
                                        <Editor>
                                            <ext:TextField ID="TextField17" runat="server" />
                                            <%--<ext:SelectBox ID="TextField14" runat="server" StoreID="StoreCombo" DisplayField="Name" ValueField="ID" >
                                                <Store>
                                                    <ext:Store ID="StoreCombo" runat="server" >
                                                    
                                                    </ext:Store>
                                                </Store> 
                                                <Items>
                                                    <ext:ListItem Value="1" Text="赵医师" />
                                                    <ext:ListItem Value="2" Text="钱医师" />
                                                    <ext:ListItem Value="3" Text="孙医师" />
                                                    <ext:ListItem Value="4" Text="李医师" />
                                                    <ext:ListItem Value="5" Text="周医师" />
                                                    <ext:ListItem Value="6" Text="吴医师" />
                                                    <ext:ListItem Value="7" Text="郑医师" />
                                                    <ext:ListItem Value="8" Text="王医师" />
                                                </Items>
                                            </ext:SelectBox>--%>
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column123" Header="医院名称" runat="server" DataIndex="EXAM_HOSPITAL" Flex="1" >
                                        <Editor>
                                            <ext:TextField ID="TextField15" runat="server" />
                                        </Editor>
                                    </ext:Column>
                                    <ext:Column ID="Column1" Header="ROW_ID" runat="server" DataIndex="ROW_ID" Width="100" Hidden="true" />                                    
                                </Columns>
                            </ColumnModel>
                            <Plugins>
                                <ext:CellEditing ID="CellEditing1" runat="server" ClicksToEdit="1" />
                            </Plugins>
                            <Buttons>
                                <ext:Button ID="Button1" runat="server" Text="添加" Icon="Add" Width="90" >
                                    <Listeners>
                                        <Click Fn="insertRecord" />
                                    </Listeners>
                                </ext:Button>
                                <ext:Button ID="Button2" runat="server" Text="保存" Icon="Disk" Width="90" >
                                    <DirectEvents>
                                        <Click OnEvent="tt" Before="return #{Store1}.isDirty();">
                                            <ExtraParams>
                                                <ext:Parameter Name="data" Value="#{Store1}.getChangedData()" Mode="Raw" Encode="true" />
                                                <ext:Parameter Name="datb" Value="#{Store1}.getRecordsValues()" Mode="Raw" Encode="true" />
                                            </ExtraParams>
                                        </Click>
                                    </DirectEvents>
                                </ext:Button>
                                <ext:Button ID="Button3" runat="server" Text="取消" Icon="Cancel" Width="90" OnDirectClick="uu" />
                            </Buttons>
                            
                        </ext:GridPanel> 
                    </Items>
                </ext:Panel>
            </Items>
        </ext:Viewport>
    </form>
</body>
</html>
