using System;
using System.Collections.Generic;
using System.Data;
using Dialysis_Chart_Show.tools;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UsrInf_Cond_zinfo_a_07
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


        //轉歸情況 opt_1
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

        //退出情況 opt_2
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

        //其它退出情況說明 txt_3
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

        //轉出地點 opt_4
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

        //轉出原因 opt_5
        private int _opt_5;
        public int opt_5
        {
            get { return _opt_5; }
            set
            {
                if (value == _opt_5) return;
                _opt_5 = value;
            }
        }


        //其它請說明 txt_6
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

        //死亡原因 chk_7
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

        //心血管事件 chk_8
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

        //其它心血管事件 txt_9
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

        //腦血管事件 chk_10
        private string _chk_10;
        public string chk_10
        {
            get { return _chk_10; }
            set
            {
                if (value == _chk_10) return;
                _chk_10 = value;
            }
        }

        //其它腦血管事件 txt_11
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

        //感染 chk_12
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

        //其它感染 txt_13
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

        //其它死亡原因請說明 txt_14
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


        //以下是UsrInf_Cond_zinfo_a_07 GetData 
        public static List<UsrInf_Cond_zinfo_a_07> UsrInf_Cond_zinfo_a_07_List = null;

        public static List<UsrInf_Cond_zinfo_a_07> GetData(string sPat_id, string sInfo_date)
        {

            DBMysql DbInfo = new DBMysql();
            DataTable MyInfoDt = new DataTable();
            string sSQL = "";
            //sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_07  z  left join   //pat_info p  on  p.pif_id =  z.pat_id ";
            //sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";

            if (!string.IsNullOrWhiteSpace(sPat_id))
            {
                sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_07  z  left join   pat_info p  on  p.pif_id =  z.pat_id ";
                sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";
            }


            MyInfoDt = DbInfo.Query(sSQL);

            UsrInf_Cond_zinfo_a_07_List = null;
            if (UsrInf_Cond_zinfo_a_07_List == null)
            {
                UsrInf_Cond_zinfo_a_07_List = new List<UsrInf_Cond_zinfo_a_07>();
                if (MyInfoDt.Rows.Count > 0)
                {
                    for (int cc = 0; cc <= MyInfoDt.Rows.Count - 1; cc++)
                    {
                        UsrInf_Cond_zinfo_a_07 UsrInf_Cond_zinfo_a_07_00 = new UsrInf_Cond_zinfo_a_07();
                        //塞欄位到  UsrInf_Cond_zinfo_a_07 class
                        /*
                         pat_id     string
                         info_date  string
                         info_user  string
							
                             opt_1 int
                             opt_2  int 
                             txt_3  string
                             opt_4  int 
                             opt_5  int 
                             txt_6  string
                             chk_7  string
                             chk_8  string
                             txt_9  string
                             chk_10 string
                             txt_11 string
                             chk_12 string
                             txt_13 string
                             txt_14 string

 
                         */
                        // Convert.ToInt32(MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim());
                        // MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim();

                        UsrInf_Cond_zinfo_a_07_00.pat_id = MyInfoDt.Rows[cc]["pat_id"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_07_00.info_date = MyInfoDt.Rows[cc]["info_date"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_07_00.info_user = MyInfoDt.Rows[cc]["info_user"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_1"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.opt_1 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.opt_1 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_1"].ToString().Trim());
                        }

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_2"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.opt_2 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.opt_2 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_2"].ToString().Trim());
                        }


                        UsrInf_Cond_zinfo_a_07_00.txt_3 = MyInfoDt.Rows[cc]["txt_3"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_4"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.opt_4 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.opt_4 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_4"].ToString().Trim());
                        }

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["opt_5"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.opt_5 = 0;  //0 是沒選 RadioButton
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.opt_5 = Convert.ToInt32(MyInfoDt.Rows[cc]["opt_5"].ToString().Trim());
                        }

                        UsrInf_Cond_zinfo_a_07_00.txt_6 = MyInfoDt.Rows[cc]["txt_6"].ToString().Trim();


                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_7"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.chk_7 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.chk_7 = MyInfoDt.Rows[cc]["chk_7"].ToString().Trim();
                        }

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_8"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.chk_8 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.chk_8 = MyInfoDt.Rows[cc]["chk_8"].ToString().Trim();
                        }


                        UsrInf_Cond_zinfo_a_07_00.txt_9 = MyInfoDt.Rows[cc]["txt_9"].ToString().Trim();


                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_10"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.chk_10 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.chk_10 = MyInfoDt.Rows[cc]["chk_10"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_07_00.txt_11 = MyInfoDt.Rows[cc]["txt_11"].ToString().Trim();

                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_12"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.chk_12 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.chk_12 = MyInfoDt.Rows[cc]["chk_12"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_07_00.txt_13 = MyInfoDt.Rows[cc]["txt_13"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_07_00.txt_14 = MyInfoDt.Rows[cc]["txt_14"].ToString().Trim();


                        //pat_info 欄位 pif_name
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_name"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.pif_name = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.pif_name = MyInfoDt.Rows[cc]["pif_name"].ToString().Trim();
                        }

                        //pat_info 欄位 pif_ic
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_07_00.pif_ic = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_07_00.pif_ic = MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_07_List.Add(UsrInf_Cond_zinfo_a_07_00);
                    }
                }
            }
            return UsrInf_Cond_zinfo_a_07_List;
        }//GetData
    }
}
