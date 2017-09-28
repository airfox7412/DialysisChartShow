using System;
using System.Collections.Generic;
using System.Data;
using Dialysis_Chart_Show.tools;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UsrInf_Cond_zinfo_a_01
    {
        //病患資料序號
        private string _pat_id;
        public string pat_id
        {
            get { return _pat_id; }
            set
            {
                if (value == _pat_id) return;
                _pat_id = value;
            }
        }

        //登錄日期
        private string _info_date;
        public string info_date
        {
            get { return _info_date; }
            set
            {
                if (value == _info_date) return;
                _info_date = value;
            }
        }

        //登錄人員
        private string _info_user;
        public string info_user
        {
            get { return _info_user; }
            set
            {
                if (value == _info_user) return;
                _info_user = value;
            }
        }

        //原發病診斷分類opt_1
        private int _opt_1;
        public int opt_1
        {
            get { return _opt_1; }
            set
            {
                if (value == _opt_1) return;
                _opt_1 = value;
            }
        }

        //原發性腎小球疾病opt_2
        private int _opt_2;
        public int opt_2
        {
            get { return _opt_2; }
            set
            {
                if (value == _opt_2) return;
                _opt_2 = value;
            }
        }

        //其它原發性腎小球腎病txt_3
        private string _txt_3;
        public string txt_3
        {
            get { return _txt_3; }
            set
            {
                if (value == _txt_3) return;
                _txt_3 = value;
            }
        }

        //繼發性腎小球疾病opt_4
        private int _opt_4;
        public int opt_4
        {
            get { return _opt_4; }
            set
            {
                if (value == _opt_4) return;
                _opt_4 = value;
            }
        }

        //其它繼發性腎小球腎病說明txt_5
        private string _txt_5;
        public string txt_5
        {
            get { return _txt_5; }
            set
            {
                if (value == _txt_5) return;
                _txt_5 = value;
            }
        }

        //遺傳性及先天性腎病opt_6
        private int _opt_6;
        public int opt_6
        {
            get { return _opt_6; }
            set
            {
                if (value == _opt_6) return;
                _opt_6 = value;
            }
        }

        //其它遺傳性及先天性腎病txt_7
        private string _txt_7;
        public string txt_7
        {
            get { return _txt_7; }
            set
            {
                if (value == _txt_7) return;
                _txt_7 = value;
            }
        }

        //腎小管間質疾病opt_8
        private int _opt_8;
        public int opt_8
        {
            get { return _opt_8; }
            set
            {
                if (value == _opt_8) return;
                _opt_8 = value;
            }
        }

        //其他腎小管間質疾病txt_9
        private string _txt_9;
        public string txt_9
        {
            get { return _txt_9; }
            set
            {
                if (value == _txt_9) return;
                _txt_9 = value;
            }
        }

        //泌尿系感染和結石opt_10
        private int _opt_10;
        public int opt_10
        {
            get { return _opt_10; }
            set
            {
                if (value == _opt_10) return;
                _opt_10 = value;
            }
        }

        //其它泌尿系感染和結石txt_11
        private string _txt_11;
        public string txt_11
        {
            get { return _txt_11; }
            set
            {
                if (value == _txt_11) return;
                _txt_11 = value;
            }
        }

        //腎臟切除原因opt_12
        private int _opt_12;
        public int opt_12
        {
            get { return _opt_12; }
            set
            {
                if (value == _opt_12) return;
                _opt_12 = value;
            }
        }

        //其他腎臟切除原因txt_13
        private string _txt_13;
        public string txt_13
        {
            get { return _txt_13; }
            set
            {
                if (value == _txt_13) return;
                _txt_13 = value;
            }
        }
        //病患id from   pat_info Table
        private string _pif_id;
        public string pif_id
        {
            get { return _pif_id; }
            set
            {
                if (value == _pif_id) return;
                _pif_id = value;
            }
        }

        //病患Name from   pat_info Table
        private string _pif_name;
        public string pif_name
        {
            get { return _pif_name; }
            set
            {
                if (value == _pif_name) return;
                _pif_name = value;
            }
        }

        //病患pif_ic from   pat_info Table
        private string _pif_ic;
        public string pif_ic
        {
            get { return _pif_ic; }
            set
            {
                if (value == _pif_ic) return;
                _pif_ic = value;
            }
        }


        //以下是UsrInf_Cond_zinfo_a_01 GetData 
        public static List<UsrInf_Cond_zinfo_a_01> UsrInf_Cond_zinfo_a_01_List = null;

        public static List<UsrInf_Cond_zinfo_a_01> GetData(string sPat_id, string sInfo_date)
        {

            DBMysql DbInfo = new DBMysql();
            DataTable MyInfoDt = new DataTable();
            string sSQL = "";
            //sSQL = sSQL + "  select * from zinfo_a_01  ";
            //sSQL = sSQL + "  where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";

            if (!string.IsNullOrWhiteSpace(sPat_id))
            {
                sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_01  z  left join   pat_info p  on  p.pif_id =  z.pat_id ";
                sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";
            }

            MyInfoDt = DbInfo.Query(sSQL);

            UsrInf_Cond_zinfo_a_01_List = null;
            if (UsrInf_Cond_zinfo_a_01_List == null)
            {
                UsrInf_Cond_zinfo_a_01_List = new List<UsrInf_Cond_zinfo_a_01>();
                if (MyInfoDt.Rows.Count > 0)
                {
                    for (int cc = 0; cc <= MyInfoDt.Rows.Count - 1; cc++)
                    {
                        UsrInf_Cond_zinfo_a_01 UsrInf_Cond_zinfo_a_01_00 = new UsrInf_Cond_zinfo_a_01();
                        //塞欄位到  UsrInf_Cond_zinfo_a_01 class
                        /*
                         pat_id     string
                         info_date  string
                         info_user  string
                            
                         opt_1      int
                         opt_2      int 
                         txt_3      string
                         opt_4      int
                         txt_5      string
                         opt_6      int
                         txt_7      string
                         opt_8      int 
                         txt_9      string
                         opt_10     int
                         txt_11     string
                         opt_12     int
                         txt_13     string                      
                         */
                        // Convert.ToInt32(MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim());
                        // MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim();

                        UsrInf_Cond_zinfo_a_01_00.pat_id = MyInfoDt.Rows[cc]["pat_id"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_01_00.info_date = MyInfoDt.Rows[cc]["info_date"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_01_00.info_user = MyInfoDt.Rows[cc]["info_user"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_1"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_1 = 0;
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_1 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_1"].ToString().Trim());
                        }
                        //UsrInf_Cond_zinfo_a_01_00.opt_1 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_1"].ToString().Trim());
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_2"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_2 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_2 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_2"].ToString().Trim());
                        }
                        //UsrInf_Cond_zinfo_a_01_00.opt_2 =

                        UsrInf_Cond_zinfo_a_01_00.txt_3 = MyInfoDt.Rows[cc]["txt_3"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_4"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_4 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_4 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_4"].ToString().Trim());
                        }
                        //UsrInf_Cond_zinfo_a_01_00.opt_4 =
                        UsrInf_Cond_zinfo_a_01_00.txt_5 = MyInfoDt.Rows[cc]["txt_5"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_6"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_6 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_6 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_6"].ToString().Trim());
                        }
                        //UsrInf_Cond_zinfo_a_01_00.opt_6 =

                        UsrInf_Cond_zinfo_a_01_00.txt_7 = MyInfoDt.Rows[cc]["txt_7"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_8"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_8 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_8 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_8"].ToString().Trim());
                        }
                        //UsrInf_Cond_zinfo_a_01_00.opt_8 =
                        UsrInf_Cond_zinfo_a_01_00.txt_9 = MyInfoDt.Rows[cc]["txt_9"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_10"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_10 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_10 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_10"].ToString().Trim());
                        }
                        //UsrInf_Cond_zinfo_a_01_00.opt_10  = 
                        UsrInf_Cond_zinfo_a_01_00.txt_11 = MyInfoDt.Rows[cc]["txt_11"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_12"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_12 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.opt_12 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_12"].ToString().Trim());
                        }
                        //UsrInf_Cond_zinfo_a_01_00.opt_12  = 
                        UsrInf_Cond_zinfo_a_01_00.txt_13 = MyInfoDt.Rows[cc]["txt_13"].ToString().Trim();


                        //其他可能接下來會用到的欄位
                        //pat_info 欄位 pif_name
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_name"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.pif_name = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.pif_name = MyInfoDt.Rows[cc]["pif_name"].ToString().Trim();
                        }

                        //pat_info 欄位 pif_ic
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_01_00.pif_ic = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_01_00.pif_ic = MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim();
                        }
                        //每跑一迴圈暫時收到UsrInf_Cond_zinfo_a_01_00容器裡面
                        UsrInf_Cond_zinfo_a_01_List.Add(UsrInf_Cond_zinfo_a_01_00);
                    }

                }

            }
            return UsrInf_Cond_zinfo_a_01_List;
        }//GetData
    }
}
