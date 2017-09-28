using System.Collections.Generic;
using System.Data;
using Dialysis_Chart_Show.tools;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UsrInf_Cond_zinfo_a_05
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

        //診斷 chk_1
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

        //消化系統診斷 txt_2
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

        //呼吸系統診斷 txt_3
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

        //血液系統診斷 txt_4
        private string _txt_4;
        public string txt_4
        {
            get { return _txt_4; }
            set
            {
                if (value == _txt_4) return;
                _txt_4 = value;
            }
        }

        //泌尿系統診斷 txt_5
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

        //神經系統診斷 txt_6
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

        //骨骼肌肉系統診斷 txt_7
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

        //其它診斷 txt_8
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

        //以下是UsrInf_Cond_zinfo_a_05 GetData 
        public static List<UsrInf_Cond_zinfo_a_05> UsrInf_Cond_zinfo_a_05_List = null;

        public static List<UsrInf_Cond_zinfo_a_05> GetData(string sPat_id, string sInfo_date)
        {

            DBMysql DbInfo = new DBMysql();
            DataTable MyInfoDt = new DataTable();
            string sSQL = "";
            //sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_05  z  left join   //pat_info p  on  p.pif_id =  z.pat_id ";
            //sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";

            if (!string.IsNullOrWhiteSpace(sPat_id))
            {
                sSQL = sSQL + " select  z.* ,p.pif_id,pif_name,pif_ic   from zinfo_a_05  z  left join   pat_info p  on  p.pif_id =  z.pat_id ";
                sSQL = sSQL + " where pat_id = '" + sPat_id + "' and info_date = '" + sInfo_date + "' ";
            }

            MyInfoDt = DbInfo.Query(sSQL);

            UsrInf_Cond_zinfo_a_05_List = null;
            if (UsrInf_Cond_zinfo_a_05_List == null)
            {
                UsrInf_Cond_zinfo_a_05_List = new List<UsrInf_Cond_zinfo_a_05>();
                if (MyInfoDt.Rows.Count > 0)
                {
                    for (int cc = 0; cc <= MyInfoDt.Rows.Count - 1; cc++)
                    {
                        UsrInf_Cond_zinfo_a_05 UsrInf_Cond_zinfo_a_05_00 = new UsrInf_Cond_zinfo_a_05();
                        //塞欄位到  UsrInf_Cond_zinfo_a_05 class
                        /*
                         pat_id     string
                         info_date  string
                         info_user  string
                         chk_1  string
                         txt_2 string
                         txt_3 string
                         txt_4 string
                         txt_5 string
                         txt_6 string
                         txt_7 string
                         txt_8 string
                          
                         */
                        // Convert.ToInt32(MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim());
                        // MyInfoDt.Rows[cc]["ROW_ID"].ToString().Trim();

                        UsrInf_Cond_zinfo_a_05_00.pat_id = MyInfoDt.Rows[cc]["pat_id"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_05_00.info_date = MyInfoDt.Rows[cc]["info_date"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_05_00.info_user = MyInfoDt.Rows[cc]["info_user"].ToString().Trim();
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["chk_1"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_05_00.chk_1 = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_05_00.chk_1 = MyInfoDt.Rows[cc]["chk_1"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_05_00.txt_2 = MyInfoDt.Rows[cc]["txt_2"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_05_00.txt_3 = MyInfoDt.Rows[cc]["txt_3"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_05_00.txt_4 = MyInfoDt.Rows[cc]["txt_4"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_05_00.txt_5 = MyInfoDt.Rows[cc]["txt_5"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_05_00.txt_6 = MyInfoDt.Rows[cc]["txt_6"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_05_00.txt_7 = MyInfoDt.Rows[cc]["txt_7"].ToString().Trim();
                        UsrInf_Cond_zinfo_a_05_00.txt_8 = MyInfoDt.Rows[cc]["txt_8"].ToString().Trim();

                        //pat_info 欄位 pif_name
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_name"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_05_00.pif_name = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_05_00.pif_name = MyInfoDt.Rows[cc]["pif_name"].ToString().Trim();
                        }

                        //pat_info 欄位 pif_ic
                        if (string.IsNullOrWhiteSpace((MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim())))
                        {
                            UsrInf_Cond_zinfo_a_05_00.pif_ic = "";
                        }
                        else
                        {
                            UsrInf_Cond_zinfo_a_05_00.pif_ic = MyInfoDt.Rows[cc]["pif_ic"].ToString().Trim();
                        }

                        UsrInf_Cond_zinfo_a_05_List.Add(UsrInf_Cond_zinfo_a_05_00);
                    }

                }

            }
            return UsrInf_Cond_zinfo_a_05_List;
        }//GetData
    }
}
