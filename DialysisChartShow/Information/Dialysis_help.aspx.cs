using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Ext.Net;
using Dialysis_Chart_Show.tools;
using System.Data;
using System.Configuration;
using System.Text;
using System.IO;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_help : BaseForm
    {
        string pif_name, pif_sex, pif_dob;
        int age;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                Random r = new Random();
                if (!String.IsNullOrEmpty(_PAT_IC))
                {
                    string sSQL = "SELECT * FROM pat_info ";
                    sSQL += "WHERE pif_ic='" + _PAT_IC + "'";
                    DataTable dt = db.Query(sSQL);

                    if(dt.Rows.Count>0)
                    {
                        pif_name = dt.Rows[0]["pif_name"].ToString();
                        if(dt.Rows[0]["pif_sex"].ToString()=="M")
                            pif_sex = "男";
                        else
                            pif_sex = "女";
                        int thisyear = int.Parse(DateTime.Now.ToString("yyyy"));
                        age = thisyear - int.Parse(dt.Rows[0]["pif_dob"].ToString().Substring(0, 4));
                    }
                    dt.Clear();
                    FormPanel1.Title = "临床小帮手 (" + _PAT_IC + ")";
                }

                helper1.Text += setColorFont(System.Drawing.Color.Blue, "姓名:" + pif_name + " (" + pif_sex + ", " + age.ToString() + ")", 6);

                //長期透析要件
                helper1.Text += setColorFont(System.Drawing.Color.Red, "長期透析要件:", 5);
                int v1 = r.Next(530, 850);
                string str1 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                if (v1 > 707 || (v1 < 707 && v1 > 530))
                {
                    str1 += "该病患符合长期透析要件";
                    if (v1 < 707 && v1 > 530)
                    {
                        str1 += "，外加并发症";
                    }
                    else
                    {
                        str1 += "不需长期透析";
                    }
                    helper1.Text += setColorFont(System.Drawing.Color.Black, str1, 3);
                }

                //安置血管通路時機
                helper1.Text += setColorFont(System.Drawing.Color.Red, "安置血管通路時機:", 5);
                int v2 = r.Next(1, 2); //1.糖尿病 2.非糖尿病
                int v3 = r.Next(1, 2); //1.半年内须要透析 2.两个月内须要透析
                string str2 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                if (v1 > 354 || v2 == 1)
                {
                    if (v3 == 1)
                    {
                        str2 += "糖尿病患者，半年内须要透析，自体动静脉廔管手术";
                    }
                    else
                    {
                        str2 += "糖尿病患者，半年内须要透析，人工血管手术";
                    }
                }
                else
                {
                    if (v3 == 1)
                    {
                        str2 += "非糖尿病患者，半年内须要透析，自体动静脉廔管手术";
                    }
                    else
                    {
                        str2 += "非糖尿病患者，两个月内须要透析，人工血管手术";
                    }
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str2, 3);

                //透析充分性
                helper1.Text += setColorFont(System.Drawing.Color.Red, "透析充分性:", 5);
                string str3 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                if (v2 == 1)
                {
                    str3 += "透析量不足";
                    if (v3 == 1)
                    {
                        str3 += "，增加透析时间或用HDF 可以改善";
                    }
                    else
                    {
                        str3 += "，采用口服或静脉注射维生素D";
                    }
                }
                else
                {
                    str3 += "透析量充足";
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str3, 3);

                //透析液
                helper1.Text += setColorFont(System.Drawing.Color.Red, "透析液:", 5);
                int v4 = r.Next(1, 3);
                string str4 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;建议采用透析液钙浓度";
                if (v4 == 1)
                {
                    str4 += "3.5mEq/L";
                }
                else if(v4==2)
                {
                    str4 += "2.5mEq/L"; 
                }
                else
                {
                    str4 += "3.0mEq/L";
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str4, 3);

                //贫血状况
                helper1.Text += setColorFont(System.Drawing.Color.Red, "贫血状况:", 5);
                int v5 = r.Next(1, 3);
                string str5 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;剂量调整，";
                if (v5 == 1)
                {
                    str5 += "增加50% EPO 剂量";
                }
                else if (v5 == 2)
                {
                    str5 += "减少25% EPO 剂量";
                }
                else
                {
                    str5 += "维持原EPO 剂量";
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str5, 3);

                //铁状况
                helper1.Text += setColorFont(System.Drawing.Color.Red, "铁状况:", 5);
                int v6 = r.Next(1, 3);
                string str6 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                if (v6 == 1)
                {
                    str6 += "绝对贫血";
                }
                else if (v6 == 2)
                {
                    str6 += "功能性贫血，给铁剂可以改善";
                }
                else
                {
                    str6 += "发炎造成贫血，给铁剂不能改善";
                }
                string str61 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                if (v5 == 1)
                {
                    str61 += "口服铁剂: 成人每天 200mg ,儿童每天 2 - 3mg/kg 分2 至 3 次<br>";
                    str61 += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;注意:空腹,不可混其他药物及食物吃,不可再饭前一小时及饭后两小时内服用";
                }
                else if (v5 == 2)
                {
                    str61 += "静脉注射铁剂: 使用 1.0g IV iron 8-10 周<br>";
                    str61 += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;如果10周后 数字与上面相同，可以用同剂量 再用8-10周";

                }
                else
                {
                    str61 += "铁剂过敏及感染: 第一次用小剂量试验:成人:25mg, 10mg 体重<10kg 儿童 ,15mg 10kg<体重<20kg<br>";
                    str61 += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;使用量>=500mg 静脉注射要1小时以上";
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str6, 3);
                helper1.Text += setColorFont(System.Drawing.Color.Black, str61, 3);

                //肾性骨病变
                helper1.Text += setColorFont(System.Drawing.Color.Red, "肾性骨病变:", 5);
                string str7 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                int v7 = r.Next(1, 6);
                switch (v7)
                {
                    case 1:
                        str7 += "建议:检查<br>";
                        str7 += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;1.病人血管通路的重覆循环过高及血液流速太低<br>";
                        str7 += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;2.透析时间与透析量是否足够<br>";
                        str7 += "&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;3.考虑实施HDF治疗";
                        break;
                    case 2:
                        str7 += "建议: 可能引起软组织与冠状动脉的钙化 死亡率增加27%";
                        break;
                    case 3:
                        str7 += "建议:提供碳酸钙片 或采用 高钙浓度(3.5mEq/L)的透析液 或 活性维生素D，尚须注意iPTH 及骨骼状态";
                        break;
                    case 4:
                        str7 += "透析液增加镁离子浓度(标准 0.5 - 1.0mEq/L)";
                        break;
                    case 5:
                        str7 += "透析液减少镁离子浓度(标准 0.5 - 1.0mEq/L)";
                        break;
                    case 6:
                        str7 += "建议:口服 或 静脉注射活性维生素 D";
                        break;
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str7, 3);

                //肝指标
                helper1.Text += setColorFont(System.Drawing.Color.Red, "肝指标:", 5);
                string str8 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                int v8 = r.Next(1, 100);
                if(v8>50)
                {
                        str8 += "肝功能正常";
                }
                else{
                        str8 += "肝功能异常";
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str8, 3);

                //心脏血管并发症
                helper1.Text += setColorFont(System.Drawing.Color.Red, "心脏血管并发症:", 5);
                string str9 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                int v9 = r.Next(1, 4);
                switch (v9)
                {
                    case 1:
                        str9 += "总胆固醇 <240mg/dl 容易产生动脉硬化";
                        break;
                    case 2:
                        str9 += "低密度胆固醇>160mg/dl  容易产生动脉硬化";
                        break;
                    case 3:
                        str9 += "高密度胆固醇<L40mg/dl  容易产生动脉硬化";
                        break;
                    case 4:
                        str9 += "建议采用降血酯药物如果无效可以考虑食用鱼油或使用低分子量肝素做为透析时的抗拟剂";
                        break;
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str9, 3);

                //营养状况
                helper1.Text += setColorFont(System.Drawing.Color.Red, "营养状况:", 5);
                string str10 = "&nbsp;&nbsp;&nbsp;&nbsp;★&nbsp;";
                int v10 = r.Next(1, 3);
                switch (v10)
                {
                    case 1:
                        str10 += "蛋白热量营养不良会增加死亡危险";
                        break;
                    case 2:
                        str10 += "会增加死亡危险";
                        break;
                    case 3:
                        str10 += "应评估是否有蛋白热量营养不良或 骨骼肌的流失";
                        break;
                }
                helper1.Text += setColorFont(System.Drawing.Color.Black, str10, 3);
            }
        }

        private string setColorFont(System.Drawing.Color c, string content, int size)
        {
            return "<font color=" + HexConverter(c) + " size=" + size.ToString() + ">" + content + "</font><br>";
        }

        private string setColorFont10(System.Drawing.Color c, string content)
        {            
            return "<font color=" + HexConverter(c) + " size=10>" + content + "</font><br>";
        }

        private static String HexConverter(System.Drawing.Color c)
        {
            return "#" + c.R.ToString("X2") + c.G.ToString("X2") + c.B.ToString("X2");
        }

        private static String RGBConverter(System.Drawing.Color c)
        {
            return "RGB(" + c.R.ToString() + "," + c.G.ToString() + "," + c.B.ToString() + ")";
        }
    }
}