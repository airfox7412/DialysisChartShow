using System;
using System.Collections.Generic;
using System.Data;
using Dialysis_Chart_Show.tools;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UserInfoObs
    {
        private int _ROW_ID;
        public int ROW_ID
        {
            get { return _ROW_ID; }
            set
            {
                if (value == _ROW_ID) return;
                _ROW_ID = value;
            }
        }

        //RESULT_DATE
        private String _RESULT_DATE;
        public string RESULT_DATE
        {
            get { return _RESULT_DATE; }
            set
            {
                if (value == _RESULT_DATE) return;
                _RESULT_DATE = value;
            }
        }
        //RESULT_CODE 
        private String _RESULT_CODE;
        public string RESULT_CODE
        {
            get { return _RESULT_CODE; }
            set
            {
                if (value == _RESULT_CODE) return;
                _RESULT_CODE = value;
            }
        }

        //double  RESULT_VALUE_N 
        private double _RESULT_VALUE_N;
        public double RESULT_VALUE_N
        {
            get { return _RESULT_VALUE_N; }
            set
            {
                if (value == _RESULT_VALUE_N) return;
                _RESULT_VALUE_N = value;
            }
        }

        //int RESULT_VER = 0;  
        private int _RESULT_VER;
        public int RESULT_VER
        {
            get { return _RESULT_VER; }
            set
            {
                if (value == _RESULT_VER) return;
                _RESULT_VER = value;
            }
        }

        //string KIN_DATE = "";   
        private string _KIN_DATE;
        public string KIN_DATE
        {
            get { return _KIN_DATE; }
            set
            {
                if (value == _KIN_DATE) return;
                _KIN_DATE = value;
            }
        }

        //string KIN_USER = "";   
        private string _KIN_USER;
        public string KIN_USER
        {
            get { return _KIN_USER; }
            set
            {
                if (value == _KIN_USER) return;
                _KIN_USER = value;
            }
        }

        //string authorid = "P32000800.8066." + KIN_USER;
        private string _authorid;
        public string authorid
        {
            get { return _authorid; }
            set
            {
                if (value == _authorid) return;
                _authorid = value;
            }
        }

        //int PAT_NO = 0;
        private int _PAT_NO;
        public int PAT_NO
        {
            get { return _PAT_NO; }
            set
            {
                if (value == _PAT_NO) return;
                _PAT_NO = value;
            }
        }

        //string pif_name
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

        //string pif_name
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

        //string patientId = "PAT32000800.8066." + PAT_NO;
        private string _patientId;
        public string patientId
        {
            get { return _patientId; }
            set
            {
                if (value == _patientId) return;
                _patientId = value;
            }
        }


        //string RITEM_NAME = "白细胞计数 WBC";   
        private string _RITEM_NAME;
        public string RITEM_NAME
        {
            get { return _RITEM_NAME; }
            set
            {
                if (value == _RITEM_NAME) return;
                _RITEM_NAME = value;
            }
        }

        //string RITEM_UNIT = "x10^9/L";  
        private string _RITEM_UNIT;
        public string RITEM_UNIT
        {
            get { return _RITEM_UNIT; }
            set
            {
                if (value == _RITEM_UNIT) return;
                _RITEM_UNIT = value;
            }
        }


        //string RITEM_LOW1 = "4.0";  
        private string _RITEM_LOW1;
        public string RITEM_LOW1
        {
            get { return _RITEM_LOW1; }
            set
            {
                if (value == _RITEM_LOW1) return;
                _RITEM_LOW1 = value;
            }
        }

        //string RITEM_HIGH1 = "10.0";   
        private string _RITEM_HIGH1;
        public string RITEM_HIGH1
        {
            get { return _RITEM_HIGH1; }
            set
            {
                if (value == _RITEM_HIGH1) return;
                _RITEM_HIGH1 = value;
            }
        }

        public static List<UserInfoObs> UserInfoObsList = null;

        public static List<UserInfoObs> GetData(string sPat_no, string sResult_date, string sOrgId, string sOrgName)
        {
            DBMysql DbInfoObs = new DBMysql();
            DataTable MyInfoObsDt = new DataTable();
            string UsesOrgId = sOrgId.Substring(1).ToString().Trim();
            string UsesOrgName = sOrgName;

            string sSQL = "";
            //sSQL = sSQL + "  select * from a_result_log L, a_ritem_setup I ,pat_info  P  ";
            //sSQL = sSQL + "  where L.pat_no = '" + sPat_no + "' and L.result_date = '" + sResult_date + "'  and L.result_code = I.ritem_code and   P.pif_id =  L.pat_no   ";
            sSQL = sSQL + "    select  * from    ";
            sSQL = sSQL + "    (select * from a_result_log L left join  pat_info  P  on  L.pat_no =   p.pif_id )L  left join  a_ritem_setup I ";
            sSQL = sSQL + "    on  L.result_code = I.ritem_code ";
            sSQL = sSQL + "    where L.pat_no = '" + sPat_no + "' and L.result_date = '" + sResult_date + "'   ";

            MyInfoObsDt = DbInfoObs.Query(sSQL);

            UserInfoObsList = null;

            if (UserInfoObsList == null)
            {
                UserInfoObsList = new List<UserInfoObs>();
                if (MyInfoObsDt.Rows.Count > 0)
                {
                    for (int jj = 0; jj <= MyInfoObsDt.Rows.Count - 1; jj++)
                    {
                        UserInfoObs UserInfoObs0 = new UserInfoObs();
                        //塞欄位  
                        /*
                         int _PAT_NO
                         String _RESULT_DATE;
                         String _RESULT_CODE
                         double  RESULT_VALUE_N
                         int RESULT_VER
                         string KIN_USER
                         string authorid
                         //int PAT_NO 
                         string pif_name
                         string patientId
                         string RITEM_NAME
                         string RITEM_UNIT
                         string RITEM_LOW1
                         string RITEM_HIGH1
                         */

                        // UserInfoObs0.orgId = Info_index.;
                        UserInfoObs0.ROW_ID = Convert.ToInt32(MyInfoObsDt.Rows[jj]["ROW_ID"].ToString().Trim());
                        UserInfoObs0.PAT_NO = Convert.ToInt32(MyInfoObsDt.Rows[jj]["pat_no"].ToString().Trim());
                        UserInfoObs0.RESULT_DATE = MyInfoObsDt.Rows[jj]["RESULT_DATE"].ToString().Trim();
                        UserInfoObs0.RESULT_CODE = MyInfoObsDt.Rows[jj]["RESULT_CODE"].ToString().Trim();
                        UserInfoObs0.RESULT_VALUE_N = Convert.ToDouble(MyInfoObsDt.Rows[jj]["RESULT_VALUE_N"].ToString().Trim());
                        UserInfoObs0.RESULT_VER = Convert.ToInt16(MyInfoObsDt.Rows[jj]["RESULT_VER"].ToString().Trim());
                        UserInfoObs0.KIN_DATE = MyInfoObsDt.Rows[jj]["KIN_DATE"].ToString().Trim();
                        UserInfoObs0.KIN_USER = MyInfoObsDt.Rows[jj]["KIN_USER"].ToString().Trim();
                        //MyUserInfoObs.authorid = MyLabObsDt.Rows[i]["authorid"].ToString().Trim();//string authorid = "P32000800.8066." + KIN_USER;
                        //UserInfoObs0.authorid = "P32000800.8066." + UserInfoObs0.KIN_USER;
                        UserInfoObs0.authorid = "PRAC" + UsesOrgId + "." + UserInfoObs0.KIN_USER;
                        UserInfoObs0.pif_name = MyInfoObsDt.Rows[jj]["pif_name"].ToString().Trim();
                        UserInfoObs0.pif_ic = MyInfoObsDt.Rows[jj]["pif_ic"].ToString().Trim();
                        //MyUserInfoObs.patientId   //string patientId = "PAT32000800.8066." + PAT_NO;
                        //UserInfoObs0.patientId = "PAT32000800.8066." + UserInfoObs0.PAT_NO;
                        UserInfoObs0.patientId = "PAT" + UsesOrgId + "." + UserInfoObs0.PAT_NO;
                        UserInfoObs0.RITEM_NAME = MyInfoObsDt.Rows[jj]["RITEM_NAME"].ToString().Trim();
                        UserInfoObs0.RITEM_UNIT = MyInfoObsDt.Rows[jj]["RITEM_UNIT"].ToString().Trim();
                        UserInfoObs0.RITEM_LOW1 = MyInfoObsDt.Rows[jj]["RITEM_LOW1"].ToString().Trim();
                        UserInfoObs0.RITEM_HIGH1 = MyInfoObsDt.Rows[jj]["RITEM_HIGH1"].ToString().Trim();

                        //ADD 
                        UserInfoObsList.Add(UserInfoObs0);
                    }
                }

            }
            return UserInfoObsList;
        }
    }
}
