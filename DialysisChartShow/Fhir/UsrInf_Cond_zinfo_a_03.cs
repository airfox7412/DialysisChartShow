﻿using System.Collections.Generic;
using System.Data;
using Dialysis_Chart_Show.tools;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UsrInf_Cond_zinfo_a_03
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

        //併發症分類 chk_1
        private string _chk_1;
        public string chk_1
        {
            get { return _chk_1; }
            set
            {
                if (value == _chk_1) return;
                _chk_1 = value;
            }
        }

        //骨礦物質代謝紊亂 chk_2
        private string _chk_2;
        public string chk_2
        {
            get { return _chk_2; }
            set
            {
                if (value == _chk_2) return;
                _chk_2 = value;
            }
        }

        //其它骨礦物質代謝紊亂 txt_3
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

        //澱粉樣變性 chk_4
        private string _chk_4;
        public string chk_4
        {
            get { return _chk_4; }
            set
            {
                if (value == _chk_4) return;
                _chk_4 = value;
            }
        }

        //其它澱粉樣變性 txt_5
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

        //呼吸系統併發症 chk_6
        private string _chk_6;
        public string chk_6
        {
            get { return _chk_6; }
            set
            {
                if (value == _chk_6) return;
                _chk_6 = value;
            }
        }

        //其它呼吸系統併發症 txt_7
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

        //心血管 chk_8
        private string _chk_8;
        public string chk_8
        {
            get { return _chk_8; }
            set
            {
                if (value == _chk_8) return;
                _chk_8 = value;
            }
        }

        //其它心血管併發症 txt_9
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
        //神經系統 chk_10
        private string _chk_10;
        public string chk_10
        {
            get { return _chk_8; }
            set
            {
                if (value == _chk_10) return;
                _chk_10 = value;
            }
        }
        //其它神經系統並發症 txt_11
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
        //消化系統併發症 chk_12
        private string _chk_12;
        public string chk_12
        {
            get { return _chk_12; }
            set
            {
                if (value == _chk_12) return;
                _chk_12 = value;
            }
        }
        //其他消化系統併發症 txt_13
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
        //其它併發症 txt_14
        private string _txt_14;
        public string txt_14
        {
            get { return _txt_14; }
            set
            {
                if (value == _txt_14) return;
                _txt_14 = value;
            }
        }
        //具體情況描述 txt_15
        private string _txt_15;
        public string txt_15
        {
            get { return _txt_15; }
            set
            {
                if (value == _txt_15) return;
                _txt_15 = value;
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


        //以下是UsrInf_Cond_zinfo_a_03 GetData 
        public static List<UsrInf_Cond_zinfo_a_03> UsrInf_Cond_zinfo_a_03_List = null;

        public static List<UsrInf_Cond_zinfo_a_03> GetData(string sPat_id, string sInfo_date)
        {

            DBMysql DbInfo = new DBMysql();
            DataTable MyInfoDt = new DataTable();
            string sSQL = "";
            //sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_03  z  left join   //pat_info p  on  p.pif_id =  z.pat_id ";
            //sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";

            if (!string.IsNullOrWhiteSpace(sPat_id))
            {
                sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_03  z  left join   pat_info p  on  p.pif_id =  z.pat_id ";
                sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";
            }


            MyInfoDt = DbInfo.Query(sSQL);

            UsrInf_Cond_zinfo_a_03_List = null;
            if (UsrInf_Cond_zinfo_a_03_List == null)
            {
                UsrInf_Cond_zinfo_a_03_List = new List<UsrInf_Cond_zinfo_a_03>();
                if (MyInfoDt.Rows.Count > 0)
                {
                    for (int cc = 0; cc <= MyInfoDt.Rows.Count - 1; cc++)
                    {
                        UsrInf_Cond_zinfo_a_03 UsrInf_Cond_zinfo_a_03_00 = new UsrInf_Cond_zinfo_a_03();
                        //塞欄位到  UsrInf_Cond_zinfo_a_03 class
                        /*

                         pat_id     string
                         info_date  string
                         info_user  string
                         chk_1      string
                         chk_2      string
                         txt_3      string
                         chk_4      string
                         txt_5      string
                         chk_6      string
                         txt_7      string
                         chk_8      string
                         txt_9      string
                         chk_10     string
                         txt_11     string
                         chk_12     string
                         txt_13     string
                         txt_14     string
                         txt_15     string
							
                         */
                        // Convert.ToInt32(MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim());
                        // MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim();

                        UsrInf_Cond_zinfo_a_03_00.pat_id = MyInfoDt.Rows[cc]["pat_id"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_03_00.info_date = MyInfoDt.Rows[cc]["info_date"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_03_00.info_user = MyInfoDt.Rows[cc]["info_user"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_1"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_1 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_1 = MyInfoDt.Rows[cc]["chk_1"].ToString().Trim();
                        }
                        //UsrInf_Cond_zinfo_a_03_00.opt_1 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_1"].ToString().Trim());
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_2"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_2 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_2 = MyInfoDt.Rows[cc]["chk_2"].ToString().Trim();
                        }
                        //UsrInf_Cond_zinfo_a_03_00.opt_2 =

                        UsrInf_Cond_zinfo_a_03_00.txt_3 = MyInfoDt.Rows[cc]["txt_3"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_4"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_4 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_4 = MyInfoDt.Rows[cc]["chk_4"].ToString().Trim();
                        }
                        //UsrInf_Cond_zinfo_a_03_00.opt_4 =
                        UsrInf_Cond_zinfo_a_03_00.txt_5 = MyInfoDt.Rows[cc]["txt_5"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_6"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_6 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_6 = MyInfoDt.Rows[cc]["chk_6"].ToString().Trim();
                        }
                        //UsrInf_Cond_zinfo_a_03_00.opt_6 =

                        UsrInf_Cond_zinfo_a_03_00.txt_7 = MyInfoDt.Rows[cc]["txt_7"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_8"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_8 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_8 = MyInfoDt.Rows[cc]["chk_8"].ToString().Trim();
                        }
                        //UsrInf_Cond_zinfo_a_03_00.opt_8 =
                        UsrInf_Cond_zinfo_a_03_00.txt_9 = MyInfoDt.Rows[cc]["txt_9"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_10"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_10 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_10 = MyInfoDt.Rows[cc]["chk_10"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_03_00.txt_11 = MyInfoDt.Rows[cc]["txt_11"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_12"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_12 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.chk_12 = MyInfoDt.Rows[cc]["chk_12"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_03_00.txt_13 = MyInfoDt.Rows[cc]["txt_13"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_03_00.txt_14 = MyInfoDt.Rows[cc]["txt_14"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_03_00.txt_15 = MyInfoDt.Rows[cc]["txt_15"].ToString().Trim();

                        //pat_info 欄位 pif_name
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_name"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.pif_name = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.pif_name = MyInfoDt.Rows[cc]["pif_name"].ToString().Trim();
                        }

                        //pat_info 欄位 pif_ic
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_03_00.pif_ic = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_03_00.pif_ic = MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_03_List.Add(UsrInf_Cond_zinfo_a_03_00);
                    }

                }

            }
            return UsrInf_Cond_zinfo_a_03_List;
        }//GetData
    }
}
