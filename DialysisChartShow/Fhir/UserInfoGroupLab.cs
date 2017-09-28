using System;
using System.Collections.Generic;
using System.Data;
using Dialysis_Chart_Show.tools;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UserInfoGroupLab
    {
        //以下是欄位

        //pat_no
        private String _pat_no;
        public string pat_no
        {
            get { return _pat_no; }
            set
            {
                if (value == _pat_no) return;
                _pat_no = value;
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

        //ListGROUP_CODE
        private String _ListGROUP_CODE;
        public string ListGROUP_CODE
        {
            get { return _ListGROUP_CODE; }
            set
            {
                if (value == _ListGROUP_CODE) return;
                _ListGROUP_CODE = value;
            }
        }


        //ListGROUP_NAME
        private String _ListGROUP_NAME;
        public string ListGROUP_NAME
        {
            get { return _ListGROUP_NAME; }
            set
            {
                if (value == _ListGROUP_NAME) return;
                _ListGROUP_NAME = value;
            }
        }

        //ListGROUP_NAME_E
        private String _ListGROUP_NAME_E;
        public string ListGROUP_NAME_E
        {
            get { return _ListGROUP_NAME_E; }
            set
            {
                if (value == _ListGROUP_NAME_E) return;
                _ListGROUP_NAME_E = value;
            }
        }

        /*
         RESULT_VALUE_T,
         RESULT_VALUE_N,
         RITEM_NAME,
         RITEM_NAME_E,
         RITEM_NAME_C,
         RITEM_NAME_S,
         RITEM_CLASS,
         RITEM_TYPE,
         RITEM_SN,
         RITEM_USED,
         RITEM_UNIT,       
         */


        public static List<UserInfoGroupLab> UserInfoGroupLabList = null;

        public static List<UserInfoGroupLab> GetData(string sPat_no, string sResult_date)
        {
            DBMysql LabGDb = new DBMysql();
            DataTable MyLabGDt = new DataTable();
            string sSQL = " ";
            sSQL = sSQL + " select GROUP_CODE as ListGROUP_CODE  ,GROUP_NAME as ListGROUP_NAME  ,GROUP_NAME_E as ListGROUP_NAME_E ,L.pat_no,L.*, I.* , G.* from   a_result_log   L   ";
            sSQL = sSQL + " left join    a_ritem_setup I  on    L.result_code = I.ritem_code  ";
            sSQL = sSQL + " left join    a_item_group G   on    L.result_code = G.oitem_code  ";
            sSQL = sSQL + " where   L.pat_no = '" + sPat_no + "' and L.result_date = '" + sResult_date + "' ";

            MyLabGDt = LabGDb.Query(sSQL);
            UserInfoGroupLabList = null;

            if (UserInfoGroupLabList == null)
            {

                UserInfoGroupLabList = new List<UserInfoGroupLab>();

                if (MyLabGDt.Rows.Count > 0)
                {
                    for (int kk = 0; kk <= MyLabGDt.Rows.Count - 1; kk++)
                    {
                        UserInfoGroupLab UserInfoGroupLab0 = new UserInfoGroupLab();
                        UserInfoGroupLab0.pat_no = MyLabGDt.Rows[kk]["pat_no"].ToString();
                        UserInfoGroupLab0.RESULT_DATE = MyLabGDt.Rows[kk]["RESULT_DATE"].ToString();
                        UserInfoGroupLab0.RESULT_CODE = MyLabGDt.Rows[kk]["RESULT_DATE"].ToString();
                        UserInfoGroupLab0.ListGROUP_CODE = MyLabGDt.Rows[kk]["ListGROUP_CODE"].ToString();
                        UserInfoGroupLab0.ListGROUP_NAME = MyLabGDt.Rows[kk]["ListGROUP_NAME"].ToString();
                        UserInfoGroupLab0.ListGROUP_NAME_E = MyLabGDt.Rows[kk]["ListGROUP_NAME_E"].ToString();
                        // UserInfoGroupLab0.pat_no = MyLabGDt.Rows[kk]["pat_no"].ToString();
                        //  UserInfoGroupLab0.pat_no = MyLabGDt.Rows[kk]["pat_no"].ToString();
                        UserInfoGroupLabList.Add(UserInfoGroupLab0);
                    }

                }
            }
            return UserInfoGroupLabList;
        }
    }
}
