using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Collections;
using Ext.Net;
using System.Data;

namespace Dialysis_Chart_Show.Information
{
    public partial class Dialysis_01_07 : BaseForm
    {
        private string _TableName = "zinfo_a_07";
        private string sel_info_date = "";

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!X.IsAjaxRequest)
            {
                sel_info_date = _Request("sel_info_date");
                switch (_Request("editmode"))
                {
                    case "list":
                        FormPanel1.Visible = false;
                        ShowList();
                        break;
                    case "show":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        btn_clear.Visible = false;
                        btn_save.Visible = false;
                        FormPanel1.Title = FormPanel1.Title + "-浏览";
                        break;
                    case "edit":
                        _zInfo_Show(_TableName, _PAT_ID, sel_info_date);
                        btn_clear.Visible = true;
                        btn_save.Visible = true;
                        Btn_Close.Visible = false;
                        if (_Request("editmode2") == "add")
                        {
                            FormPanel1.Title = FormPanel1.Title + "-添加";

                            //2015.04.20 andy
                            //GetData(sender, e);
                        }
                        else
                        {
                            FormPanel1.Title = FormPanel1.Title + "-修改";

                            //2015.04.20 andy
                            //GetData(sender, e);
                        }
                        break;
                    case "delete":
                        _zInfo_Confirm_Delete(sel_info_date);
                        break;
                }
            }
        }

        protected void Btn_Submit_Click(object sander, DirectEventArgs e)
        {
            _zInfo_Save(_TableName, _PAT_ID, info_date.Text);

            //2015.04.27 
            //zinfo_a_07 診斷訊息 死亡原因  
            //           opt_1:??情? :退出，?移植，?出，死亡，?入
            //           chk_7:死亡原因 :心血管事件，?血管事件，感染，消化道出血等出血性疾病，其它
            //
            //常規記錄 
            //zinfo_e_01(常規記錄):chk_55:血管事件,脑血管事件,感染,消化道出血等出血性疾病  
            //zinfo_e_01(常規記錄):chk_52:退出,?移植,?出,死亡,?入 1 2 3 (4) 5
                
            if (chk_7_1.Checked == true || chk_7_2.Checked == true || chk_7_3.Checked == true || chk_7_4.Checked == true || chk_7_5.Checked == true )
            {

            }
            else
            {
                return;
            }
            string wchk_55 = "";
            string wchk52 = "";
            string wtxt56 = "";
            if (chk_7_1.Checked  == true)
            {
                wchk_55 = "10000";
                wchk52 = "4";
                wtxt56  = "";
            }
            if (chk_7_2.Checked == true)
            {
                wchk_55 = "01000";
                wchk52 = "4";
                wtxt56  = "";
            }
            if (chk_7_3.Checked == true)
            {
                wchk_55 = "00100";
                wchk52 = "4";
                wtxt56  = "";
            }
            if (chk_7_4.Checked == true)
            {
                wchk_55 = "00010";
                wchk52 = "4";
                wtxt56  = "";
            }
            if (chk_7_5.Checked == true)
            {
                wchk_55 = "00001";
                wchk52 = "4";
                wtxt56 = "其他";
            }

            string sSQL = "";
            string winfo_date = _Get_YMD(info_date.Text);                      
            sSQL = "  SELECT * ";
            sSQL += " FROM zinfo_e_01 ";
            sSQL += " WHERE pat_id    = '" + _PAT_ID    + "'";
            sSQL += " AND   info_date = '" + winfo_date + "'";
            DataTable dt2 = db.Query(sSQL);
            if (dt2.Rows.Count == 1)
            {
                //常規記錄:病患資料+記錄日期 同步更新 轉出情況Zinfo_a_07
                //chk_7:心血管事件，脑血管事件，感染，消化道出血等出血性疾病，其它
                sSQL = " UPDATE zinfo_e_01 set chk_55 ='" + wchk_55 + "'," +
                       " opt_52 = '" + wchk52 + "'" +
                       " WHERE pat_id    ='" + _PAT_ID    + "'" +
                       " AND   info_date ='" + winfo_date + "'"; 
                db.Excute(sSQL);
            }
            else
            {


                //zinfo_a-07 opt_1:0 是死亡   (退出，肾移植，转出，死亡，转入)
                //zinfo_a-07 chk_7:死亡原因:   心血管事件，脑血管事件，感染，消化道出血等出血性疾病，其它

                //新增 常規記錄 2015.04.27 abdt
                //zinfo_e_01(常規記錄):chk_55:血管事件,脑血管事件,感染,消化道出血等出血性疾病  
                //zinfo_e_01(常規記錄):chk_52:退出,?移植,?出,死亡,?入 1 2 3 (4) 5
                sSQL = "INSERT INTO zinfo_e_01 (";
                sSQL += " pat_id , info_date , info_user , opt_1 ,  num_2 , opt_3 ,num_4 ,opt_5 ,opt_6 ,opt_7 ,";

                sSQL += " num_8  , num_9  , num_10 ,opt_11 ,txt_12 ,num_13 ,opt_14 ,opt_15 ,num_16 ,num_17 ,";
                sSQL += " num_18 , num_19 , opt_20 ,num_21 ,num_22 ,opt_23 ,num_24 ,are_25 ,opt_26 ,txt_27 ,";
                sSQL += " txt_28 , are_29 , opt_30 ,num_31 ,num_32 ,opt_33 ,txt_34 ,txt_35 ,opt_36 ,num_37 ,";
                sSQL += " num_38 , num_39 , txt_40 ,opt_41 ,opt_42 ,txt_43 ,num_44 ,num_45 ,num_46 ,num_47 ,";

                sSQL += " txt_48 , txt_49 , num_50 ,num_51 ,opt_52 ,txt_53 ,txt_54 ,chk_55 ,txt_56 ,num_57 ,num_58";
                sSQL += ") ";


                sSQL += "VALUES('" + _PAT_ID + "','" + winfo_date + "'," + "'" + "'," ;
                sSQL += "'',"; 
                sSQL += "''," +  "''," +  "'',"  +  "''," +  "''," +  "'',"  +  "''," ;
                sSQL += "''," + "''," + "''," + "''," + "''," + "''," + "''," + "''," + "''," + "'',";
                sSQL += "''," + "''," + "''," + "''," + "''," + "''," + "''," + "''," + "''," + "'',";
                sSQL += "''," + "''," + "''," + "''," + "''," + "''," + "''," + "''," + "''," + "'',";
                sSQL += "''," + "''," + "''," + "''," + "''," + "''," + "''," + "''," + "''," + "'',";

                sSQL += "''," + "''," + "'','";

                sSQL += wchk52 + "',";

                sSQL += "''," + "'','";

                sSQL += wchk_55 + "','";

                sSQL += wtxt56 + "',";

                sSQL +=  "'',";   

                sSQL += "'" + "')";
                db.Excute(sSQL);

            }          
            //
            X.Redirect("Dialysis_01_07.aspx?editmode=list");

        }

        protected void Btn_Clear_Click(object sander, DirectEventArgs e)
        {
            _ClearForm();
        }

        protected void Btn_Close_Click(object sander, DirectEventArgs e)
        {
            X.Redirect("Dialysis_01_07.aspx?editmode=list");
        }

        [DirectMethod]
        public void zInfo_delete(string _info_date)
        {
            _zInfo_Delete(_TableName, _PAT_ID, _info_date);

            X.Redirect("Dialysis_01_07.aspx?editmode=list");
        }
        protected void btn_Clear_Radio(object sender, DirectEventArgs e)
        {
            string radioGroupName = e.ExtraParams["radname"].ToString();
            _ClearRadio(radioGroupName);
        }
        private void ShowList()
        {
            string[] col_0 = { "日期", "info_date" };
            string[] col_1 = { "转归情况", "opt_1" };
            string[] col_2 = { "退出情况", "opt_2" };
            string[] col_3 = { "其它退出情况说明", "txt_3" };
            string[] col_4 = { "转出地点", "opt_4" };
            string[] col_5 = { "转出原因", "opt_5" };
            string[] col_6 = { "其它请说明", "txt_6" };
            string[] col_7 = { "死亡原因", "chk_7" };
            string[] col_8 = { "心血管事件", "chk_8" };
            string[] col_9 = { "其它心血管事件", "txt_9" };
            string[] col_10 = { "脑血管事件", "chk_10" };
            string[] col_11 = { "其它脑血管事件", "txt_11" };
            string[] col_12 = { "感染", "chk_12" };
            string[] col_13 = { "其它感染", "txt_13" };
            string[] col_14 = { "其它死亡原因请说明", "txt_14" };

            ArrayList acol = new ArrayList();
            acol.Add(col_0);
            acol.Add(col_1);
            acol.Add(col_2);
            acol.Add(col_3);
            acol.Add(col_4);
            acol.Add(col_5);
            acol.Add(col_6);
            acol.Add(col_7);
            acol.Add(col_8);
            acol.Add(col_9);
            acol.Add(col_10);
            acol.Add(col_11);
            acol.Add(col_12);
            acol.Add(col_13);
            acol.Add(col_14);

            _Fill_Html_Table(_TableName, _PAT_ID, acol, "转归情况列表");
        }
    }
}