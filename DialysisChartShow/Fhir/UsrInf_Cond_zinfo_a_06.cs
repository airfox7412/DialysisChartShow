using System;
using System.Collections.Generic;
using System.Data;
using Dialysis_Chart_Show.tools;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UsrInf_Cond_zinfo_a_06
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


        //過敏診斷 chk_1
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

        //其他過敏反應 txt_2
        private string _txt_2;
        public string txt_2
        {
            get { return _txt_2; }
            set
            {
                if (value == _txt_2) return;
                _txt_2 = value;
            }
        }

        //透析器材過敏 chk_3
        private string _chk_3;
        public string chk_3
        {
            get { return _chk_3; }
            set
            {
                if (value == _chk_3) return;
                _chk_3 = value;
            }
        }

        //透析膜 chk_4
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

        //其它透析膜說明 txt_5
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


        //其它透析膜說明 txt_6
        private string _txt_6;
        public string txt_6
        {
            get { return _txt_6; }
            set
            {
                if (value == _txt_6) return;
                _txt_6 = value;
            }
        }

        //消毒劑 chk_7
        private string _chk_7;
        public string chk_7
        {
            get { return _chk_7; }
            set
            {
                if (value == _chk_7) return;
                _chk_7 = value;
            }
        }

        //其它消毒劑說明 txt_8
        private string _txt_8;
        public string txt_8
        {
            get { return _txt_8; }
            set
            {
                if (value == _txt_8) return;
                _txt_8 = value;
            }
        }

        //藥物過敏 chk_9
        private string _chk_9;
        public string chk_9
        {
            get { return _chk_9; }
            set
            {
                if (value == _chk_9) return;
                _chk_9 = value;
            }
        }

        //抗生素 txt_10
        private string _txt_10;
        public string txt_10
        {
            get { return _txt_10; }
            set
            {
                if (value == _txt_10) return;
                _txt_10 = value;
            }
        }

        //靜脈鐵劑 chk_11
        private string _chk_11;
        public string chk_11
        {
            get { return _chk_11; }
            set
            {
                if (value == _chk_11) return;
                _chk_11 = value;
            }
        }

        //蔗糖鐵 txt_12
        private string _txt_12;
        public string txt_12
        {
            get { return _txt_12; }
            set
            {
                if (value == _txt_12) return;
                _txt_12 = value;
            }
        }

        //右旋糖?鐵 txt_13
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

        //肝素 opt_14
        private int _opt_14;
        public int opt_14
        {
            get { return _opt_14; }
            set
            {
                if (value == _opt_14) return;
                _opt_14 = value;
            }
        }

        //其它藥物過敏說明 txt_15
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


        //以下是UsrInf_Cond_zinfo_a_06 GetData 
        public static List<UsrInf_Cond_zinfo_a_06> UsrInf_Cond_zinfo_a_06_List = null;

        public static List<UsrInf_Cond_zinfo_a_06> GetData(string sPat_id, string sInfo_date)
        {

            DBMysql DbInfo = new DBMysql();
            DataTable MyInfoDt = new DataTable();
            string sSQL = "";
            //sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_06  z  left join   //pat_info p  on  p.pif_id =  z.pat_id ";
            //sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";

            if (!string.IsNullOrWhiteSpace(sPat_id))
            {
                sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_06  z  left join   pat_info p  on  p.pif_id =  z.pat_id ";
                sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";
            }


            MyInfoDt = DbInfo.Query(sSQL);

            UsrInf_Cond_zinfo_a_06_List = null;
            if (UsrInf_Cond_zinfo_a_06_List == null)
            {
                UsrInf_Cond_zinfo_a_06_List = new List<UsrInf_Cond_zinfo_a_06>();
                if (MyInfoDt.Rows.Count > 0)
                {
                    for (int cc = 0; cc <= MyInfoDt.Rows.Count - 1; cc++)
                    {
                        UsrInf_Cond_zinfo_a_06 UsrInf_Cond_zinfo_a_06_00 = new UsrInf_Cond_zinfo_a_06();
                        //塞欄位到  UsrInf_Cond_zinfo_a_06 class
                        /*
                         pat_id     string
                         info_date  string
                         info_user  string
                         chk_1      string 
                         txt_2      string
                         chk_3      string
                         chk_4      string
                         txt_5      string
                         txt_6      string
                         chk_7      string
                         txt_8      string
                         chk_9      string
                         txt_10     string
                         chk_11     string
                         txt_12     string
                         txt_13     string 
                         opt_14     int
                         txt_15     string                          
                         */
                        // Convert.ToInt32(MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim());
                        // MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim();

                        UsrInf_Cond_zinfo_a_06_00.pat_id = MyInfoDt.Rows[cc]["pat_id"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_06_00.info_date = MyInfoDt.Rows[cc]["info_date"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_06_00.info_user = MyInfoDt.Rows[cc]["info_user"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_1"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_1 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_1 = MyInfoDt.Rows[cc]["chk_1"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_06_00.txt_2 = MyInfoDt.Rows[cc]["txt_2"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_3"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_3 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_3 = MyInfoDt.Rows[cc]["chk_3"].ToString().Trim();
                        }

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_4"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_4 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_4 = MyInfoDt.Rows[cc]["chk_4"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_06_00.txt_5 = MyInfoDt.Rows[cc]["txt_5"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_06_00.txt_6 = MyInfoDt.Rows[cc]["txt_6"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_7"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_7 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_7 = MyInfoDt.Rows[cc]["chk_7"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_06_00.txt_8 = MyInfoDt.Rows[cc]["txt_8"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_9"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_9 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_9 = MyInfoDt.Rows[cc]["chk_9"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_06_00.txt_10 = MyInfoDt.Rows[cc]["txt_10"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_11"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_11 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.chk_11 = MyInfoDt.Rows[cc]["chk_11"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_06_00.txt_12 = MyInfoDt.Rows[cc]["txt_12"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_06_00.txt_13 = MyInfoDt.Rows[cc]["txt_13"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_14"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.opt_14 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.opt_14 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_14"].ToString().Trim());
                        }

                        UsrInf_Cond_zinfo_a_06_00.txt_15 = MyInfoDt.Rows[cc]["txt_15"].ToString().Trim();

                        //pat_info 欄位 pif_name
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_name"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.pif_name = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.pif_name = MyInfoDt.Rows[cc]["pif_name"].ToString().Trim();
                        }

                        //pat_info 欄位 pif_ic
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_06_00.pif_ic = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_06_00.pif_ic = MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_06_List.Add(UsrInf_Cond_zinfo_a_06_00);
                    }
                }
            }
            return UsrInf_Cond_zinfo_a_06_List;
        }//GetData
    }
}
