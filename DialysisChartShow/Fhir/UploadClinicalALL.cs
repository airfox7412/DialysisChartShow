using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Dialysis_Chart_Show.tools;
using NLog;
using Dialysis2FHIR_v13;

namespace Dialysis_Chart_Show.Fhir
{
    class UploadClinicalALL
    {
        private static Logger logger = LogManager.GetCurrentClassLogger();
        //private string orgId = Cbo_Hospital.SelectedItem.Value;
        //private string orgName = Cbo_Hospital.Text;
        private string orgId = "H32000800.8066";
        private string orgName = "南京医科大学血液净化中心";
        private static object syncHandle = new object();
        private static object syncHandle1 = new object();
        private static object syncHandle2 = new object();
        private static object syncHandlest1 = new object();
        private static object syncHandlest2 = new object();
        private static string sChoiseHospital;

        public UploadClinicalALL()
        {
            //FOR Condition SecurityEvent 
            ConcurrentBag<AuditObject> CondSuccessList01 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondFailList01 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondSuccessList02 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondFailList02 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondSuccessList03 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondFailList03 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondSuccessList04 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondFailList04 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondSuccessList05 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondFailList05 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondSuccessList06 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondFailList06 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondSuccessList07 = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> CondFailList07 = new ConcurrentBag<AuditObject>();

            string MyOrgId = orgId;
            string MyOrgName = orgName;


            UploadAdmin uploadAdmin = new UploadAdmin();
            DBMysql db = new DBMysql();
            DataTable listMedicationDT = new DataTable();
            DataTable stListMedicationDT = new DataTable();

            List<AuditObject> ulClinicalList = uploadAdmin.AuditEventSearchNewestSuccessList(orgId, "UL_CLINICAL");
            List<AuditObject> updateClinicalList = ulClinicalList;

            ConcurrentBag<AuditObject> successList = new ConcurrentBag<AuditObject>();
            ConcurrentBag<AuditObject> failList = new ConcurrentBag<AuditObject>();
            /*Test Issue
             * 1.0 Test Condition Only  set  if (TestTarget != "ConditionTest")   remark   //if (TestOBSProcess) ， it will go the else part clause
             * 2.0 Test non-Condition (Observation and ListObservation)  set   if (TestOBSProcess)  remark  if (TestTarget != "ConditionTest")
             * 2.1  TestListOBS = true :   non-Condition  &  test for  ListObservation 
             * 2.2  TestListOBS = false :  non-Condition  &  test for  Observation
             * 2.3  Observation +  ListObservation , please remark the return  of Observation  
             * To Run All consider remove if condition above then test
             */
            string TestTarget = "ConditionTest";
            bool TestOBSProcess = true;

            //TestOBSProcess 永遠設定為bool TestOBSProcess = true;   
            //bool TestListOBS = true; //true for test ListObservation  
            //bool TestListOBS = false;  //false for test Observation

            if (TestTarget != "ConditionTest")  // for test Condition/~Condition(Observation/ListObservation) 
            //if (TestOBSProcess)                   // for test Observation and ListObservation
            {
                ArrayList List0 = new ArrayList();
                ArrayList List1 = new ArrayList();

                DBMysql LabDb = new DBMysql();
                DataTable MyLabDt = new DataTable();

                DBMysql LabObs = new DBMysql();
                DataTable MyLabObsDt = new DataTable();

                UserInfoGroupLab UserInfoGroupLabALL = new UserInfoGroupLab();
                UserInfoGroupLabALL = null;

                UserInfo00 MyUserInfo00 = new UserInfo00();

                //if (!TestListOBS)
                {
                    //ConcurrentBag<AuditObject> successObservation = null;
                    //ConcurrentBag<AuditObject> failObservation = null;

                    /******以下開始拼湊實驗室檢查Observation****/
                    //ArrayList List0 = new ArrayList();
                    //ArrayList List1 = new ArrayList();

                    //DBMysql LabDb = new DBMysql();
                    //DataTable MyLabDt = new DataTable();

                    //DBMysql LabObs = new DBMysql();
                    //DataTable MyLabObsDt = new DataTable();

                    //UserInfoGroupLab UserInfoGroupLabALL = new UserInfoGroupLab();
                    //UserInfoGroupLabALL = null;

                    //MyLabDt = db.Query("select  pat_no , result_date from a_result_log  group by pat_no, result_date ");
                    //先找出病患日期排序，這樣比較易除錯，全部病患，不用篩選
                    //MyLabDt = db.Query(" select * from (select pat_no , result_date from a_result_log  group by pat_no, result_date)aa order by pat_no , result_date ");
                    //Test  pat_no='200' 
                    //MyLabDt = db.Query(" select aa.*, p.* from (select pat_no , result_date from (select * from a_result_log  where 1=1   and  pat_no='200' and result_date='2014-01-16'   )a_result_log  group by pat_no, result_date)aa left join pat_info p  on  p.pif_id =  aa.pat_no   where pif_ic is not null  ");
                    //MyLabDt = db.Query(" select aa.*, p.* from (select pat_no , result_date from (select * from a_result_log  where 1=1   and pat_no='200' and result_date = '2014-01-16'   )a_result_log  group by pat_no, result_date)aa left join pat_info p  on  p.pif_id =  aa.pat_no   where pif_ic is not null  ");
                    MyLabDt = db.Query(" select aa.*, p.* from (select pat_no , result_date from (select * from a_result_log  where 1=1     )a_result_log  group by pat_no, result_date)aa left join pat_info p  on  p.pif_id =  aa.pat_no   where pif_ic is not null  ");

                    //for  KIN_DATE => select * from   a_result_log 
                    //MyLabDt = db.Query(" select aa.*, p.* from (select * from (select * from a_result_log  where 1=1   and  pat_no='200'   )a_result_log  group by pat_no, result_date)aa left join pat_info p  on  p.pif_id =  aa.pat_no   where pif_ic is not null  "); 
                    //MyLabDt = db.Query(" select * from (select pat_no , result_date from (select * from a_result_log where pat_no='200'  and result_date  ='2014-01-16' )a_result_log  group by pat_no, result_date)aa order by pat_no , result_date ");
                    int pat_date_groupCounter = 1;
                    int SecurityEventSuccessCount = 0;
                    Stopwatch sw1 = new Stopwatch();
                    //sw1.Start();
                    if (MyLabDt.Rows.Count > 0)
                    {
                        //for (int i = 0; i <= MyLabDt.Rows.Count - 1; i++)
                        //int
                        pat_date_groupCounter = 1;
                        int ttlCnt = 0;
                        //Stopwatch sw1 = new Stopwatch();
                        sw1.Start();
                        //2015.04.08 multi-thread外圈
                        Parallel.ForEach(MyLabDt.AsEnumerable(), row =>
                        //foreach (DataRow row in MyLabDt.Rows)
                        {
                            //security issue針對 patientId , result_date pair來做比對
                            //ulClinicalList 要去分別現在是測試機還是正式機，目前只有http://192.168.2.101:8000有資料，要去看那個class:UploadAdmin是連線到哪裡
                            //測試時class:UploadAdmin連http://192.168.2.101:8000     class:UserInfo00  //testEndpoint:=Uri("http://192.168.2.101:8000/hapi-fhir-jpaserver/base")     

                            //拆解出資料中比對pif_ic，某病患是否已經在雲端有資料，有資料是行政資料上傳時reference會有病患基本資料、但是更新等時間戳記還沒有
                            AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                            IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                            DateTime obsDateTime = new DateTime();
                            //目前,multi-thread正在處理哪一筆 pair

                            string pat_no = row["pat_no"].ToString().Trim();
                            string pif_ic = row["pif_ic"].ToString().Trim();
                            string result_date = row["result_date"].ToString().Trim();
                            obsDateTime = Convert.ToDateTime(result_date);

                            DateTime lastUpdateDateTime = new DateTime();
                            //lastUpdateDateTime  這個lastUpdateDateTime 可能來自各種資料的上傳，但是因為是一顆按鍵，所以不用管那麼細
                            //行政資料上傳後reference value可能只有patientId 其他資料付之闕如所以existObj會撈不到資料，
                            //但是也要考慮可能有一些資料在，existObj有些東西，注意上傳成功後要在 successList.Add完成後
                            //用AuditObject existObj = ulClinicalList去更新description 臨床資料建檔/更新時間 yyyyMMddHHmmss  (最新稽核記錄日期由此取得)

                            if (existObj != null)
                                if (!String.IsNullOrWhiteSpace(existObj.Desc))
                                {
                                    //string temp = existObj.Desc.ToString();
                                    //e.g  20150414190349
                                    //if (existObj.Desc.ToString().Length == 14)
                                    //    lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                    //else
                                    //以下是錯yyyyMMddhhmmss 應為 yyyyMMddHHmmss
                                    //lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddhhmmss", culture);
                                    lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                    //lastUpdateDateTime =  lastUpdateDateTime
                                    logger.Info(" The lastUpdateDateTime of the patient " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                    //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                                }
                            //for test
                            if (obsDateTime >= lastUpdateDateTime)
                            {
                                //do nothing 
                            }
                            //不用比較時間永遠可以跑
                            if (1 == 1)
                            //if (existObj != null && (existObj.Desc == null || obsDateTime >= lastUpdateDateTime))
                            {
                                //local 的 obsDateTime 跟 上面抓下來的比較 只有日期大於 lastUpdateDateTime 才做，為了效率其餘狀況不做
                                //因為local DB資料只要有存檔更新，就會有新的時間產生，但是KIN_DATE沒意義，祥豪建議還是比較rsult_date
                                //行政資料上傳後目前很多東西沒有，例如第一次上傳後的時間戳記
                                //這邊要包try...catch。雲端上面找到資料，而且找到最後更新時間，跟local DB撈到的時間，localDB撈到的更新時間大於雲端上面那個患者存的時間
                                //才開始做上傳作業，以減少筆數，縮短時間，目前我們要的資料暫放在 Desc(description臨床建檔更新時間，這個上傳程)

                                logger.Info("Reference hit:" + existObj.Reference + "/" + existObj.Name);

                                List<UserInfoObs> UserInfoObsResult = null;
                                lock (syncHandle1)
                                {
                                    //保存一份 pat_no:result_date  資料
                                    List0.Add(row["pat_no"].ToString() + ":" + row["result_date"].ToString());
                                    string sPat_no = row["pat_no"].ToString().Trim();
                                    string sResult_date = row["result_date"].ToString().Trim();
                                    string sPif_id = row["pif_id"].ToString().Trim();
                                    // where pif_ic is not null 過濾掉以下
                                    //if (string.IsNullOrWhiteSpace(sPat_no))
                                    //{
                                    //    logger.Error("The row exists null pat_no");
                                    //    //return;                            
                                    //}
                                    ////沒有pif_id也就沒有pif_ic如何在UserInfo00組patientId
                                    //if (string.IsNullOrWhiteSpace(sPif_id))
                                    //{
                                    //    logger.Error("The row exists null pif_id and pif_ic :" + "Pat_no = " + sPat_no + ", result_date = " + sResult_date);
                                    //    //return;
                                    //}

                                    //先塞observation
                                    //for Observation id setup
                                    //可以用下跑迴圈看  (sPat_no == "4") && (sResult_date == "2014-01-10") 個是很好的範例
                                    //if ((sPat_no == "4") && (sResult_date == "2014-01-10"))
                                    //{
                                    UserInfoObsResult = UserInfoObs.GetData(sPat_no, sResult_date, MyOrgId, MyOrgName);
                                    //*****}
                                    if (UserInfoObsResult.Count > 0) //這對資料有observation
                                    {
                                        //for (int k = 0; k <= UserInfoObsResult.Count - 1; k++)
                                        int obsCounter = 0;
                                        int ExceptionCount = 0;
                                        //2015.04.08 以下給定初值
                                        int iROW_ID = 0;
                                        string sRESULT_DATE = "";
                                        string sRESULT_CODE = "";
                                        double dRESULT_VALUE_N = 0;
                                        int iRESULT_VER = 0;
                                        string sKIN_DATE = "";
                                        string sKIN_USER = "";
                                        string sauthorid = "";
                                        int iPAT_NO = 0;
                                        string spif_name = "";
                                        string spif_ic = "";
                                        string spatientId = "";
                                        string sRITEM_NAME = "";
                                        string sRITEM_UNIT = "";
                                        string sRITEM_LOW1 = "";
                                        string sRITEM_HIGH1 = "";

                                        string SorgId = orgId;
                                        string SorgName = orgName;

                                        //2015.04.14用單執行緒處理，配合例如 ROW_ID = '26848' 
                                        //ParallelOptions po = new ParallelOptions();
                                        //po.MaxDegreeOfParallelism = 1; 
                                        //只跑一個thread
                                        Parallel.ForEach(UserInfoObsResult.AsEnumerable(), rowI =>
                                        //2015.04.14用單執行緒處理，配合例如 ROW_ID = '26848'  多一個 po參數 
                                        //Parallel.ForEach(UserInfoObsResult.AsEnumerable(),po, rowI =>

                                        //foreach (UserInfoObs result in UserInfoObsResult)
                                        {
                                            lock (syncHandle2)
                                            {
                                                //2015.04.07 Frank getdata出來展開至少有10筆 
                                                //變數宣告移位置，multi-thread這個內圈是綁List物件

                                                //羅列出每個欄位，準備塞入observation

                                                iROW_ID = rowI.ROW_ID;
                                                sRESULT_DATE = rowI.RESULT_DATE;
                                                sRESULT_CODE = rowI.RESULT_CODE;
                                                dRESULT_VALUE_N = rowI.RESULT_VALUE_N;
                                                iRESULT_VER = rowI.RESULT_VER;
                                                sKIN_DATE = rowI.KIN_DATE;
                                                sKIN_USER = rowI.KIN_USER;
                                                sauthorid = rowI.authorid;
                                                iPAT_NO = rowI.PAT_NO;
                                                spif_name = rowI.pif_name;
                                                spif_ic = rowI.pif_ic;
                                                spatientId = rowI.patientId;
                                                sRITEM_NAME = rowI.RITEM_NAME;
                                                sRITEM_UNIT = rowI.RITEM_UNIT;
                                                sRITEM_LOW1 = rowI.RITEM_LOW1;
                                                sRITEM_HIGH1 = rowI.RITEM_HIGH1;

                                                SorgId = orgId;
                                                SorgName = orgName;
                                                //for description 臨床資料建檔/更新時間 yyyyMMddHHmmss  (最新稽核記錄日期由此取得)要去比較                        
                                                //KIN_DATE沒意義，祥豪建議還是比較rsult_date
                                                //obsDateTime = DateTime.ParseExact(sKIN_DATE, "yyyy-MM-dd HH:mm:ss", culture);
                                                //obsDateTime = DateTime.ParseExact(sRESULT_DATE, "yyyy-MM-dd hh:mm:ss tt", culture);
                                                //obsDateTime = DateTime.ParseExact(sRESULT_DATE, "yyyyMMdd" , culture);
                                            }

                                            // TODO: Create all Observation in a_result_log
                                            AuditObject existObj00 = new AuditObject();
                                            try
                                            {
                                                //2015.04.15為了測試單執行緒單筆資料除錯宣告在外面
                                                AuditObject ObservationAudit = null;
                                                //2014.04.14單筆呼叫 測試ObservationCreateUpdate00
                                                //if (iROW_ID.ToString() == "26848")
                                                {
                                                    //ObservationAudit 接回是否有Update/Create成功內含success=true資料
                                                    //ObservationAudit = MyUserInfo00.ObservationCreateUpdate00(iROW_ID, sRESULT_DATE, sRESULT_CODE, dRESULT_VALUE_N, iRESULT_VER, sKIN_DATE, sKIN_USER, sauthorid, iPAT_NO, spif_ic, spatientId, sRITEM_NAME, sRITEM_UNIT, sRITEM_LOW1, sRITEM_HIGH1, SorgId, SorgName);
                                                }
                                                //MyUserInfo00.ObservationCreateUpdate00(iROW_ID, sRESULT_DATE, sRESULT_CODE, dRESULT_VALUE_N, iRESULT_VER, sKIN_DATE, sKIN_USER, sauthorid, iPAT_NO, spif_ic, spatientId, sRITEM_NAME, sRITEM_UNIT, sRITEM_LOW1, sRITEM_HIGH1, SorgId, SorgName);
                                                //2015.04.08打開Log以便追查
                                                logger.Info(" Multi-thread run " + " ,PAT_NO= " + iPAT_NO.ToString() + " ,iROW_ID= " + iROW_ID.ToString() + " ,RESULT_DATE= " + sRESULT_DATE.ToString());
                                                logger.Info("Info_index:Try to go ObservationCreateUpdate00 by pat_no:result_date pair  Create/Update:" + obsCounter + "/" + UserInfoObsResult.Count + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + " iROW_ID=" + iROW_ID + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                System.Diagnostics.Debug.WriteLine("Observation Create/Update:" + obsCounter + "/" + UserInfoObsResult.Count + "@" + pat_date_groupCounter + "/" + MyLabDt.Rows.Count);


                                                if (ObservationAudit.Success)
                                                {
                                                    //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                                    AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                                    updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                                    //ObservationAudit.Desc = updateObj.Desc;
                                                    //successList.Add(ObservationAudit);
                                                    //拿updateObj修改過的updateObj.Desc去更新successList
                                                    successList.Add(updateObj);
                                                    //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                    existObj00 = updateObj;
                                                }
                                                else
                                                    failList.Add(ObservationAudit);
                                                obsCounter++;
                                                //logger.Info(" Afert success/fail ObservationAudit now count is = " + obsCounter.ToString());

                                                //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                                                Stopwatch sw = new Stopwatch();
                                                sw.Start();
                                                try
                                                {
                                                    if (successList.Count > 0)
                                                    {

                                                        /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                                            Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                                            new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                                            HttpContext.Current.Request.UserHostAddress);
                                                         */

                                                        logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                                                        //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                                        //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                                        //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                                        //     HttpContext.Current.Request.UserHostAddress);
                                                        //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1

                                                        //時間戳記由ListObservation處理，Observation->ListObservation是連續動作
                                                        /*
                                                        uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                                             updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                                             new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                                             "127.0.0.1");
                                                        */
                                                    }
                                                    /*if (stFailList.Count > 0)
                                                        uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                                            Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                                            new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                                            HttpContext.Current.Request.UserHostAddress);
                                                     */
                                                    SecurityEventSuccessCount++;
                                                }
                                                catch (Exception ex)
                                                {
                                                    logger.Error(ex.Message);
                                                    logger.Error("Audit Create Fail@" + "UL_CLINICAL_OBSERVATION");
                                                }
                                                sw.Stop();
                                                logger.Info("ObservationAudit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                                                //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                                            }
                                            catch (Exception ex)
                                            {
                                                //System.Diagnostics.Debug.WriteLine("Exception:" + ex.Message);
                                                //System.Diagnostics.Debug.WriteLine("Exception@iROW_ID:" + iROW_ID + ", paf_ic:" + spif_ic + ", result_date:" + sRESULT_DATE);
                                                //failMedicatioinList.Add(new AuditObject { Name = lgord_id, Desc = lgord_patic + "_" + lgord_dateord });

                                                //2015.04.10 ERROR NULL new
                                                //failObservation.Add(new AuditObject { Name = iPAT_NO.ToString(), Desc = spif_ic + "_" + sRESULT_DATE });

                                                logger.Error(ex.Message);
                                                logger.Error("Info_index.ObservationCreateUpdate00 Create Fail@" + ":" + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId + " , " + "ExceptionCount = " + ExceptionCount.ToString());
                                                ExceptionCount++;
                                            }
                                            //}
                                        });
                                    }
                                    pat_date_groupCounter++;
                                    //}
                                }
                            }//if existObj != null ....

                        });//外圈Parallel.ForEach(

                    }//MyLabDt.Rows.Count LOOP
                    sw1.Stop();
                    //logger.Info("SecurityEventSuccessCount=" + SecurityEventSuccessCount.ToString());
                    logger.Info("successList.Count=" + successList.Count.ToString());
                    logger.Info("Observation Create/Update exec:" + pat_date_groupCounter + "/" + MyLabDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //為了繼續跑ListObservation 不用return 單獨測時
                    //return;
                }


                /****************ListObservation**********/
                /***以下開始查observation by pat_no,result_date 準備歸類到群組，然後在尾巴加上群組代碼***/
                DBMysql ObsDb = new DBMysql();
                DataTable MysObsDt = new DataTable();

                string sObsPat_no = "";
                string sObsResult_date = "";
                string sObsPif_ic = "";


                //MysObsDt = ObsDb.Query("   select * from (select pat_no , result_date from a_result_log  group by pat_no, result_date )aa  left join (select * from pat_info)bb   on  aa.pat_no = bb.pif_id ");
                //MysObsDt = ObsDb.Query("   select * from (select pat_no , result_date from  a_result_log  group by pat_no, result_date )aa  left join (select * from pat_info)bb   on  aa.pat_no = bb.pif_id   where pif_ic is not null ");
                //MysObsDt = ObsDb.Query("   select * from (select pat_no , result_date from (select * from a_result_log where pat_no ='200')ii  group by pat_no, result_date )aa  left join (select * from pat_info)bb   on  aa.pat_no = bb.pif_id   where pif_ic is not null ");
                //MysObsDt = ObsDb.Query("   select * from (select pat_no , result_date from (select * from a_result_log       where    1=1   and pat_no =200  and result_date = '2014-01-16'       )ii  group by pat_no, result_date )aa  left join (select * from pat_info)bb   on  aa.pat_no = bb.pif_id   where pif_ic is not null ");
                MysObsDt = ObsDb.Query("   select * from (select pat_no , result_date from (select * from a_result_log       where    1=1         )ii  group by pat_no, result_date )aa  left join (select * from pat_info)bb   on  aa.pat_no = bb.pif_id   where pif_ic is not null ");

                int SecurityEventCreateUpdate = 0;
                if (MysObsDt.Rows.Count > 0)
                {
                    //for (int i = 0; i <= MysObsDt.Rows.Count - 1; i++)

                    int count = 0;
                    int obsCounter = 0;
                    int ExceptionCount = 0;
                    Stopwatch sw1 = new Stopwatch();
                    sw1.Start();
                    //*****Parallel.ForEach(MysObsDt.AsEnumerable(), row =>
                    //2015.04.14用單執行緒處理，配合例如 ROW_ID = '26848' 
                    ParallelOptions po = new ParallelOptions();
                    po.MaxDegreeOfParallelism = 1;
                    //只跑一個thread

                    Parallel.ForEach(MysObsDt.AsEnumerable(), row =>

                    //2015.04.14用單執行緒處理，配合單筆驗證例如 ROW_ID = '26848'  多一個 po參數 
                    //Parallel.ForEach(MysObsDt.AsEnumerable(), po, row =>
                    //foreach (DataRow row in MysObsDt.Rows)
                    {
                        AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime obsDateTime = new DateTime();
                        //目前,multi-thread正在處理哪一筆 pair:pat_no, result_date

                        string pat_no = row["pat_no"].ToString().Trim();
                        string pif_ic = row["pif_ic"].ToString().Trim();
                        string result_date = row["result_date"].ToString().Trim();
                        obsDateTime = Convert.ToDateTime(result_date);

                        DateTime lastUpdateDateTime = new DateTime();
                        //lastUpdateDateTime  這個lastUpdateDateTime 可能來自各種資料的上傳，但是因為是一顆按鍵，所以不用管那麼細
                        //行政資料上傳後reference value可能只有patientId 其他資料付之闕如所以existObj會撈不到資料，
                        //但是也要考慮可能有一些資料在，existObj有些東西，注意上傳成功後要在 successList.Add完成後
                        //用AuditObject existObj = ulClinicalList去更新description 臨床資料建檔/更新時間 yyyyMMddHHmmss  (最新稽核記錄日期由此取得)

                        if (existObj != null)
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            {
                                //string temp = existObj.Desc.ToString();
                                //lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                //result_date只有yyyy-mm-dd所以比較時只要到yyyy-mm-dd
                                //lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMdd", culture);
                                //
                                //if (existObj.Desc.Length > 12)  //data format e.g yyyyMMddhhmmss
                                {
                                    //以下是錯yyyyMMddhhmmss 應為 yyyyMMddHHmmss
                                    //lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddhhmmss", culture);

                                    lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                    logger.Info(" The lastUpdateDateTime of the patient: " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                }
                                //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                            }
                        //for test
                        if (obsDateTime >= lastUpdateDateTime)
                        {
                            //do nothing 
                        }
                        //不用比較時間永遠可以跑
                        if (1 == 1)
                        //if (existObj != null && (existObj.Desc == null || obsDateTime >= lastUpdateDateTime))
                        {
                            lock (syncHandle1)
                            {


                                //保存一份 pat_no:result_date  資料
                                List0.Add(row["pat_no"].ToString() + ":" + row["result_date"].ToString());
                                //string sObsPat_no = row["pat_no"].ToString().Trim();
                                //string sObsResult_date = row["result_date"].ToString().Trim();
                                //string sObsPif_ic = row["pif_ic"].ToString().Trim();

                                //string SorgId = orgId;
                                //string SorgName = orgName;

                                // if ((sObsPat_no == "4") && (sObsResult_date == "2014-01-10")) 以下是 testcase
                                //if ((sObsPat_no == "4") && (sObsResult_date == "2014-01-10"))
                                // {


                            }
                            string SorgId = orgId;
                            string SorgName = orgName;
                            AuditObject existObj00 = new AuditObject();
                            try
                            {
                                sObsPat_no = row["pat_no"].ToString().Trim();
                                sObsResult_date = row["result_date"].ToString().Trim();
                                sObsPif_ic = row["pif_ic"].ToString().Trim();

                                //SorgId = orgId;
                                //SorgName = orgName;

                                AuditObject ListObservationAudit = null;
                                if (!(String.IsNullOrWhiteSpace(sObsPat_no) || String.IsNullOrWhiteSpace(sObsResult_date) || String.IsNullOrWhiteSpace(sObsPif_ic)))
                                {
                                    // TODO: Read Observation and Create List
                                    // UserInfo00.ObservationSearchRead00   call  UserInfo00.ListObservationCreateUpdate00(observationList, sObsPat_no, sObsResult_date, sObsPif_ic);
                                    //MyUserInfo00.ObservationSearchRead00(sObsPat_no, sObsResult_date, sObsPif_ic, SorgId, SorgName);
                                    //外圈有try...catch呼叫ObservationSearchRead00時不用
                                    //為了用單執行緒單筆筆測試用AuditObject ListObservationAudit宣告在外面
                                    //AuditObject 
                                    //ListObservationAudit = MyUserInfo00.ObservationSearchRead00(sObsPat_no, sObsResult_date, sObsPif_ic, SorgId, SorgName);
                                    //System.Diagnostics.Debug.WriteLine(count + "/" + (MysObsDt.Rows.Count - 1) + " pat_no:" + sObsPat_no + ", paf_ic:" + sObsPif_ic + ", result_date:" + sObsResult_date);
                                    //count++;
                                    logger.Info("Info_index:Try to go ObservationSearchRead00 by pat_no:result_date pair  Create/Update:" + obsCounter + "/" + MysObsDt.Rows.Count + ",PAT_NO=" + sObsPat_no.ToString() + ",RESULT_DATE=" + sObsResult_date + ",pif_ic=" + sObsPif_ic + ",orgId=" + SorgId);
                                    obsCounter++;

                                    /*********以下要更新UpdateObj.Desc*************/
                                    if (ListObservationAudit.Success)
                                    {
                                        //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                        AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                        updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                        //ObservationAudit.Desc = updateObj.Desc;
                                        //successList.Add(ObservationAudit);
                                        //拿updateObj修改過的updateObj.Desc去更新successList
                                        successList.Add(updateObj);
                                        //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                        existObj00 = updateObj;
                                    }
                                    else
                                        failList.Add(ListObservationAudit);
                                    obsCounter++;
                                    //logger.Info(" Afert success/fail ObservationAudit now count is = " + obsCounter.ToString());

                                    //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_PATIENT
                                    Stopwatch sw = new Stopwatch();
                                    sw.Start();

                                    try
                                    {
                                        if (successList.Count > 0)
                                        {

                                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                                HttpContext.Current.Request.UserHostAddress);
                                             */

                                            logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                                            //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                            //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                            //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                            //     HttpContext.Current.Request.UserHostAddress);
                                            //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1
                                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                                 updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                                 new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                                 "127.0.0.1");
                                            SecurityEventCreateUpdate++;
                                        }
                                        /*if (stFailList.Count > 0)
                                            uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                                Hl7.Fhir.Model.AuditEvent.AuditEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                                new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_PATIENT"),
                                                HttpContext.Current.Request.UserHostAddress);
                                        */
                                    }
                                    catch (Exception ex)
                                    {
                                        logger.Error(ex.Message);
                                        logger.Error("Audit Create Fail@" + "UL_CLINICAL_LISTOBSERVATION");
                                    }
                                    sw.Stop();
                                    logger.Info("ObservationAudit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                                    //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                                    /*********以上是更新UpdateObj.Desc*************/
                                }
                            }
                            catch (Exception ex)
                            {
                                //System.Diagnostics.Debug.WriteLine("Exception:" + ex.Message);
                                //System.Diagnostics.Debug.WriteLine("Exception@pat_no:" + sObsPat_no + ", paf_ic:" + sObsPif_ic + ", result_date:" + sObsResult_date);
                                logger.Error(ex.Message);
                                logger.Error("Info_index.ObservationSearchRead00 Create Fail@" + ":" + ",PAT_NO=" + sObsPat_no.ToString() + ",RESULT_DATE=" + sObsResult_date + ",pif_ic=" + sObsPif_ic + ",orgId=" + SorgId + "ExceptionCount = " + ExceptionCount.ToString());
                                ExceptionCount++;
                            }
                            // }
                        } //if (existObj != null && (existObj.Desc == null || obsDateTime >= lastUpdateDateTime))...
                    });//Parallel.ForEach(...
                    sw1.Stop();
                    //logger.Info("ListObservation Create/Update exec:" + MysObsDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    logger.Info("ListObservation Create/Update exec:" + " ListObsPairCount = " + MysObsDt.Rows.Count + " , total Time cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    logger.Info("AuditEventCreateUpdate RUN count : " + SecurityEventCreateUpdate.ToString());
                    return;


                    //logger.Info("ListObservation Create/Update exec:" + pat_date_groupCounter + "/" + MyLabDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / //1000).ToString() + " seconds");
                    //X.Js.Call("hideControllMask");
                    //X.Js.Call("hideDataMask");
                    //_NotificationShow("上传完成");
                    // return;



                    //2015.04.01.Frank  以下在做啥...如不需要應該移掉........
                    //
                    //DBMysql MyListObsDb = new DBMysql();
                    //DataTable MyQryObsDt = new DataTable();
                    //string sSQL = "";
                    ////沿用上面的 查出來的  PAT_NO, result_date
                    //if (MysObsDt.Rows.Count > 0)
                    //{
                    //    //for (int i = 0; i <= MyLabDt.Rows.Count - 1; i++)
                    //    foreach (DataRow row in MyLabDt.Rows)
                    //    {
                    //        string sPat_no = row["pat_no"].ToString().Trim();
                    //        string sResult_date = row["result_date"].ToString().Trim();

                    //        sSQL = "";
                    //        sSQL = sSQL + "  select GROUP_CODE as ListGROUP_CODE  ,GROUP_NAME as ListGROUP_NAME  ,GROUP_NAME_E,L.pat_no,L.*, I.* , G.* from  ";
                    //        sSQL = sSQL + "  a_result_log   L  left join    a_ritem_setup I  on    L.result_code = I.ritem_code ";
                    //        sSQL = sSQL + "   left join    a_item_group G   on    L.result_code = G.oitem_code ";
                    //        sSQL = sSQL + "   where   L.pat_no = '" + sPat_no + "'   and L.result_date = '" + sResult_date + "' ";
                    //        MyQryObsDt = MyListObsDb.Query(sSQL);

                    //        //for (int jj = 0; jj <= MyQryObsDt.Rows.Count - 1; jj++)
                    //        foreach (DataRow MyQryObsDt_Row in MyQryObsDt.Rows)
                    //        {
                    //            string sObsPat_no = MyQryObsDt_Row["pat_no"].ToString().Trim();
                    //            string sObsResult_date = MyQryObsDt_Row["result_date"].ToString().Trim();
                    //        }

                    //    }

                    //    //if (sPat_no =="4")
                    //    ////=>10筆observation 可以用MyLabObsDt.Rows.Count加入監看式來查  
                    //    //{
                    //    //    MyLabObsDt = db.Query( " select * from a_result_log L, a_ritem_setup I  where L.pat_no = '" + sPat_no + "'  and L.result_date = '" + sResult_date + "' and L.result_code = I.ritem_code ");

                    //    //}
                    //    //var UserInfoGroupLabPairResult = UserInfoGroupLab.GetData(sPat_no, sResult_date);
                    //}


                }

            }//TestTarget != "ConditionTest"
            else
            {
                UserInfo00 MyUserInfo00 = new UserInfo00();

                /****以下是撰寫Condition*****/
                /* 1.先去 zinfo_a_01  zinfo_a_02 等table撈取資料，做為UserInfo00.ConditionCreateUpdate00的參數            
                     By table   zinfo_a_01 zinfo_a_02... zinfo_a_08 因為每個Table欄位都不同，需要個別建class及自己GetData方法
                 */
                DBMysql CondDb = new DBMysql();
                DataTable CondDt = new DataTable();
                string sCondSQL = "";

                Stopwatch swa1_a7 = new Stopwatch();
                swa1_a7.Start();

                int a0107TtlCnt = 0;

                //先拿zinfo_a_01做測試範例

                /**********zinfo_a_01***************/

                /*如何加入SecurityEvent以便加快速度*/
                /* 比較基準是日期，當開始出現日期時，及pat_no，插入必要程式碼，不要破壞原來的程式架構，故意把info_date拉出
                 * 只需順著程式碼當日期出來後，即使是迴圈內也沒關係，如果可以獨立寫成method亦可，簡化程式
                 */

                int a_01_cnt = 0;
                //sCondSQL = sCondSQL + "  select *  from zinfo_a_01  where 1=1    ";
                //沒有pif_ic的不要，因為沒辦法去雲端找LastUpdateTime(ExistObj.Desc)
                sCondSQL = sCondSQL + "  select aa.*, p.*  from zinfo_a_01  aa  left join pat_info p  on  p.pif_id =  aa.pat_id  ";
                sCondSQL = sCondSQL + "  where pif_ic is not null ";
                //sCondSQL = sCondSQL + "  and  pat_id ='200' ";
                //sCondSQL = sCondSQL + "  and  pat_id >='200'   and   pat_id <='210'   ";
                //sCondSQL = sCondSQL + "  and  ( pat_id ='387' or pat_id ='579') ";
                CondDt = CondDb.Query(sCondSQL);

                int i_Security_a_CU01_Audit_cnt = 0;
                int i_AdminSecurity_01 = 0;

                if (CondDt.Rows.Count > 0)
                {
                    a_01_cnt = CondDt.Rows.Count;
                    //測試時先跳過//ainfo_a_01
                    //donothing
                    //}                
                    //else
                    //{

                    int ConditionCreateUpdate00_a_01Counter = 1;
                    int ExceptionCount = 1;

                    int CondCounter = 0;

                    //Stopwatch sw1 = new Stopwatch();
                    //sw1.Start();

                    foreach (DataRow row in CondDt.Rows)
                    {
                        /*SecurityEvent Issue*/
                        AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime infoDateTime = new DateTime();
                        string StrInfo_date = row["info_date"].ToString();
                        infoDateTime = Convert.ToDateTime(StrInfo_date);
                        DateTime lastUpdateDateTime = new DateTime();


                        if (existObj != null)
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            {
                                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                logger.Info(" The lastUpdateDateTime of the patient " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                            }
                        //for test
                        if (infoDateTime >= lastUpdateDateTime)
                        {
                            //do nothing 
                        }
                        //不用比較時間永遠可以跑

                        if (1 == 1)
                        //if (existObj != null && (existObj.Desc == null || infoDateTime >= lastUpdateDateTime))
                        {
                            //符合上述條件以下才做事


                            CondCounter++;
                            string sPat_id = row["pat_id"].ToString().Trim();
                            string sInfo_date = row["info_date"].ToString().Trim();
                            if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                            {
                                //進去class把欄位重整一下，如果多筆也可以都包在UsrInf_Cond_zinfo_a_01_List中
                                //用UsrInf_Cond_zinfo_a_01_List[0] 來取
                                var UsrInf_Cond_zinfo_a_01_List = UsrInf_Cond_zinfo_a_01.GetData(sPat_id, sInfo_date);

                                //這個sPat_id, sInfo_date 條件下從zinfo_a_01找出Web UI哪些項目被勾選或是被填入
                                if (UsrInf_Cond_zinfo_a_01_List.Count > 0)
                                {
                                    //為了防止會有多筆，事實上可能只有一筆，還是用for..loop呼叫，如果只跑一圈也沒關係
                                    for (int kk = 0; kk <= UsrInf_Cond_zinfo_a_01_List.Count - 1; kk++)
                                    {
                                        //羅列出每個欄位，準備當做userInfo00. ConditionCreateUpdate00(many para...)
                                        //many parameters 
                                        string Spat_id = UsrInf_Cond_zinfo_a_01_List[kk].pat_id;
                                        string Sinfo_date = UsrInf_Cond_zinfo_a_01_List[kk].info_date;
                                        string Sinfo_user = UsrInf_Cond_zinfo_a_01_List[kk].info_user;
                                        int Iopt_1 = UsrInf_Cond_zinfo_a_01_List[kk].opt_1;
                                        int Iopt_2 = UsrInf_Cond_zinfo_a_01_List[kk].opt_2;
                                        string Stxt_3 = UsrInf_Cond_zinfo_a_01_List[kk].txt_3;
                                        int Iopt_4 = UsrInf_Cond_zinfo_a_01_List[kk].opt_4;
                                        string Stxt_5 = UsrInf_Cond_zinfo_a_01_List[kk].txt_5;
                                        int Iopt_6 = UsrInf_Cond_zinfo_a_01_List[kk].opt_6;
                                        string Stxt_7 = UsrInf_Cond_zinfo_a_01_List[kk].txt_7;
                                        int Iopt_8 = UsrInf_Cond_zinfo_a_01_List[kk].opt_8;
                                        string Stxt_9 = UsrInf_Cond_zinfo_a_01_List[kk].txt_9;
                                        int Iopt_10 = UsrInf_Cond_zinfo_a_01_List[kk].opt_10;
                                        string Stxt_11 = UsrInf_Cond_zinfo_a_01_List[kk].txt_11;
                                        int Iopt_12 = UsrInf_Cond_zinfo_a_01_List[kk].opt_12;
                                        string Stxt_13 = UsrInf_Cond_zinfo_a_01_List[kk].txt_13;

                                        string Spif_name = UsrInf_Cond_zinfo_a_01_List[kk].pif_name;
                                        string Spif_ic = UsrInf_Cond_zinfo_a_01_List[kk].pif_ic;

                                        string SorgId = orgId;
                                        string SorgName = orgName;

                                        if (string.IsNullOrWhiteSpace(SorgId))
                                        {
                                            logger.Error("SorgId = " + SorgId + " is not existed .");
                                            return;
                                        }

                                        AuditObject ConditionCreateUpdate00_a_01_Audit = null;
                                        AuditObject existObj00 = new AuditObject();

                                        //  pat_id = 387  王岩生  雲端有資料  db:myhaisv3_newnu2  OK on  var patient = client.Read<Patient>("Patient/" + patientId);
                                        //  pad_id = 579  涂成英  雲端有資料
                                        //if ((Spat_id == "387") || (Spat_id == "579"))
                                        //防止Spif_ic空值進入因為 patientId = "PAT32000800.8066." + Spif_ic; 組成架構 ，即使GetData pat_id有值， //pif_ic沒有值，也不用跑UserInfo00.CreateUpdate_a_0X
                                        //屆時到這段var patientPrepared = client.Read<Patient>("Patient/" + patientId);鐵定錯，就不要上傳了
                                        //if (Spat_id == "200")//王凤英
                                        {
                                            if (!(string.IsNullOrWhiteSpace(Spif_ic)))
                                            {

                                                try
                                                {
                                                    ConditionCreateUpdate00_a_01_Audit = MyUserInfo00.ConditionCreateUpdate00_a_01(Spat_id, Sinfo_date, Sinfo_user, Iopt_1, Iopt_2, Stxt_3, Iopt_4, Stxt_5,
                                                             Iopt_6, Stxt_7, Iopt_8, Stxt_9, Iopt_10, Stxt_11, Iopt_12, Stxt_13, Spif_name, Spif_ic, SorgId, SorgName);
                                                    //MyUserInfo00.ConditionCreateUpdate00_a_01(Spat_id, Sinfo_date, Sinfo_user, Iopt_1, Iopt_2, Stxt_3, Iopt_4, Stxt_5,
                                                    //             Iopt_6, Stxt_7, Iopt_8, Stxt_9, Iopt_10, Stxt_11, Iopt_12, Stxt_13, Spif_name, Spif_ic, SorgId, SorgName);

                                                    if (ConditionCreateUpdate00_a_01_Audit.Success) i_Security_a_CU01_Audit_cnt++;
                                                    logger.Info(" Sure a_01_CU_cnt:" + i_Security_a_CU01_Audit_cnt.ToString());
                                                    logger.Info("Info_index:Try to go ConditionCreateUpdate00_a_01 Create/Update:" + ConditionCreateUpdate00_a_01Counter + "/" + CondDt.Rows.Count + ",pat_id=" + Spat_id + ",pif_name=" + Spif_name + ",pif_ic=" + Spif_ic + ",orgId=" + SorgId);


                                                    if (ConditionCreateUpdate00_a_01_Audit.Success)
                                                    {
                                                        //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                                        AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                                        updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                                        //ObservationAudit.Desc = updateObj.Desc;
                                                        //successList.Add(ObservationAudit);
                                                        //拿updateObj修改過的updateObj.Desc去更新successList
                                                        CondSuccessList01.Add(updateObj);
                                                        //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                        //existObj00 = updateObj;
                                                    }
                                                    else
                                                        CondFailList01.Add(ConditionCreateUpdate00_a_01_Audit);
                                                }
                                                catch (Exception ex)
                                                {
                                                    logger.Error(ex.Message);
                                                    logger.Error("Info_index.ConditionCreateUpdate00_a_01 Create Fail@" + ":" + "  pat_id=" + Spat_id + " ,Sinfo_date=" + Sinfo_date + " ,pif_name=" + Spif_name + " ,pif_ic=" + Spif_ic + " ,Info_index ExceptionCount: " + ExceptionCount.ToString());
                                                    ExceptionCount++;
                                                }
                                            }//(!(string.IsNullOrWhiteSpace(Spif_ic)))
                                        }
                                    }
                                }
                            } //if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                        }// 比較時間才做事 if (existObj != null && (existObj.Desc == null || infoDateTime >= lastUpdateDateTime))
                    }// foreach (DataRow row in CondDt.Rows)

                    //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        if (CondSuccessList01.Count > 0)
                        {


                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                             */

                            //logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                            //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                            //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                            //     HttpContext.Current.Request.UserHostAddress);
                            //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1


                            /*if (stFailList.Count > 0)
                                uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                    Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                    new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                    HttpContext.Current.Request.UserHostAddress);
                             */

                            //To observe  Patient new LastUpdateTime   TEST SQL =  
                            //AuditObject MyObj =   updateClinicalList.Find(x => x.Reference == "Patient/PAT32000800.8066.320122195906084425");

                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                            updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                            "127.0.0.1");
                            i_AdminSecurity_01 = CondSuccessList01.Count;
                            logger.Info(" Sure a_01_AdminSecurity_01_cnt:" + i_AdminSecurity_01.ToString());
                        }
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create Fail@" + "UL_CLINICAL_ConditionCreateUpdate00_a_01");
                    }
                    sw.Stop();
                    logger.Info("ConditionCreateUpdate00_a_01_Audit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"


                    //sw1.Stop();
                    //logger.Info("Cond_zinfo_a_01 Create/Update exec:" + CondCounter + "/" + CondDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                }
                /**********zinfo_a_02***************/
                //zinfo_a_02
                int a_02_cnt = 0;
                sCondSQL = "";
                CondDt = null;
                //sCondSQL = sCondSQL + "  select *  from zinfo_a_02  where 1=1    ";
                //沒有pif_ic的不要，因為沒辦法去雲端找LastUpdateTime(ExistObj.Desc)
                sCondSQL = sCondSQL + "  select aa.*, p.*  from zinfo_a_02  aa  left join pat_info p  on  p.pif_id =  aa.pat_id  ";
                sCondSQL = sCondSQL + "  where pif_ic is not null ";
                //sCondSQL = sCondSQL + "  and  pat_id >='200'   and   pat_id <='210'   ";
                //sCondSQL = sCondSQL + "  and  pat_id ='200' ";
                //sCondSQL = sCondSQL + "  and ( pat_id ='387' or pat_id ='579')  ";
                CondDt = CondDb.Query(sCondSQL);
                int i_Security_a_CU02_Audit_cnt = 0;
                int i_AdminSecurity_02 = 0;

                if (CondDt.Rows.Count > 0)
                {
                    a_02_cnt = CondDt.Rows.Count;
                    //測試時先跳過//ainfo_a_02
                    //donothing
                    //}                
                    //else
                    //{

                    int ConditionCreateUpdate00_a_02Counter = 1;
                    int ExceptionCount = 1;

                    int CondCounter = 0;

                    //Stopwatch sw1 = new Stopwatch();
                    //sw1.Start();


                    foreach (DataRow row in CondDt.Rows)
                    {
                        /*SecurityEvent Issue*/
                        AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime infoDateTime = new DateTime();
                        string StrInfo_date = row["info_date"].ToString();
                        infoDateTime = Convert.ToDateTime(StrInfo_date);
                        DateTime lastUpdateDateTime = new DateTime();


                        if (existObj != null)
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            {
                                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                logger.Info(" The lastUpdateDateTime of the patient " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                            }
                        //for test
                        if (infoDateTime >= lastUpdateDateTime)
                        {
                            //do nothing 
                        }
                        //不用比較時間永遠可以跑
                        if (1 == 1)
                        //if (existObj != null && (existObj.Desc == null || obsDateTime >= lastUpdateDateTime))
                        {
                            //符合上述條件以下才做事


                            CondCounter++;
                            string sPat_id = row["pat_id"].ToString().Trim();
                            string sInfo_date = row["info_date"].ToString().Trim();
                            if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                            {
                                //進去class把欄位重整一下，如果多筆也可以都包在UsrInf_Cond_zinfo_a_02_List中
                                //用UsrInf_Cond_zinfo_a_02_List[0] 來取
                                var UsrInf_Cond_zinfo_a_02_List = UsrInf_Cond_zinfo_a_02.GetData(sPat_id, sInfo_date);

                                //這個sPat_id, sInfo_date 條件下從zinfo_a_01找出Web UI哪些項目被勾選或是被填入
                                if (UsrInf_Cond_zinfo_a_02_List.Count > 0)
                                {
                                    //為了防止會有多筆，事實上可能只有一筆，還是用for..loop呼叫，如果只跑一圈也沒關係
                                    for (int kk = 0; kk <= UsrInf_Cond_zinfo_a_02_List.Count - 1; kk++)
                                    {
                                        //羅列出每個欄位，準備當做userInfo00. ConditionCreateUpdate00(many para...)
                                        //many parameters 
                                        string Spat_id = UsrInf_Cond_zinfo_a_02_List[kk].pat_id;
                                        string Sinfo_date = UsrInf_Cond_zinfo_a_02_List[kk].info_date;
                                        string Sinfo_user = UsrInf_Cond_zinfo_a_02_List[kk].info_user;
                                        string Schk_1 = UsrInf_Cond_zinfo_a_02_List[kk].chk_1;
                                        string Schk_2 = UsrInf_Cond_zinfo_a_02_List[kk].chk_2;
                                        string Stxt_3 = UsrInf_Cond_zinfo_a_02_List[kk].txt_3;
                                        string Schk_4 = UsrInf_Cond_zinfo_a_02_List[kk].chk_4;
                                        string Stxt_5 = UsrInf_Cond_zinfo_a_02_List[kk].txt_5;
                                        string Schk_6 = UsrInf_Cond_zinfo_a_02_List[kk].chk_6;
                                        string Stxt_7 = UsrInf_Cond_zinfo_a_02_List[kk].txt_7;
                                        string Schk_8 = UsrInf_Cond_zinfo_a_02_List[kk].chk_8;
                                        string Stxt_9 = UsrInf_Cond_zinfo_a_02_List[kk].txt_9;

                                        string Spif_name = UsrInf_Cond_zinfo_a_02_List[kk].pif_name;
                                        string Spif_ic = UsrInf_Cond_zinfo_a_02_List[kk].pif_ic;

                                        string SorgId = orgId;
                                        string SorgName = orgName;

                                        if (string.IsNullOrWhiteSpace(SorgId))
                                        {
                                            logger.Error("SorgId = " + SorgId + " is not existed .");
                                            return;
                                        }
                                        //  pat_id = 387  王岩生  雲端有資料  db:myhaisv3_newnu2  OK on  var patient = client.Read<Patient>("Patient/" + patientId);
                                        //  pad_id = 579  涂成英  雲端有資料
                                        //if ((Spat_id == "387") || (Spat_id == "579"))
                                        //防止Spif_ic空值進入因為 patientId = "PAT32000800.8066." + Spif_ic; 組成架構 ，即使GetData pat_id有值， //pif_ic沒有值，也不用跑UserInfo00.CreateUpdate_a_0X
                                        //屆時到這段var patientPrepared = client.Read<Patient>("Patient/" + patientId);鐵定錯，就不要上傳了
                                        // if (Spat_id == "200")//王凤英
                                        {
                                            if (!(string.IsNullOrWhiteSpace(Spif_ic)))
                                            {
                                                AuditObject ConditionCreateUpdate00_a_02_Audit = null;
                                                AuditObject existObj00 = new AuditObject();
                                                try
                                                {
                                                    ConditionCreateUpdate00_a_02_Audit = MyUserInfo00.ConditionCreateUpdate00_a_02(Spat_id, Sinfo_date, Sinfo_user, Schk_1, Schk_2, Stxt_3, Schk_4, Stxt_5,
                                                             Schk_6, Stxt_7, Schk_8, Stxt_9, Spif_name, Spif_ic, SorgId, SorgName);
                                                    //MyUserInfo00.ConditionCreateUpdate00_a_02(Spat_id, Sinfo_date, Sinfo_user, Schk_1, Schk_2, Stxt_3, Schk_4, Stxt_5,
                                                    //Schk_6, Stxt_7, Schk_8, Stxt_9, Spif_name, Spif_ic, SorgId, SorgName);

                                                    if (ConditionCreateUpdate00_a_02_Audit.Success) i_Security_a_CU02_Audit_cnt++;
                                                    logger.Info(" Sure a_02_CU_cnt:" + i_Security_a_CU02_Audit_cnt.ToString());

                                                    logger.Info("Info_index:Try to go ConditionCreateUpdate00_a_02 Create/Update:" + ConditionCreateUpdate00_a_02Counter + "/" + CondDt.Rows.Count + ",pat_id=" + Spat_id + ",pif_name=" + Spif_name + ",pif_ic=" + Spif_ic + ",orgId=" + SorgId);
                                                    ConditionCreateUpdate00_a_02Counter++;
                                                    if (ConditionCreateUpdate00_a_02_Audit.Success)
                                                    {
                                                        //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                                        AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                                        updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                                        //ObservationAudit.Desc = updateObj.Desc;
                                                        //successList.Add(ObservationAudit);
                                                        //拿updateObj修改過的updateObj.Desc去更新successList
                                                        CondSuccessList02.Add(updateObj);
                                                        //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                        existObj00 = updateObj;
                                                    }
                                                    else
                                                        CondFailList02.Add(ConditionCreateUpdate00_a_02_Audit);
                                                }

                                                catch (Exception ex)
                                                {
                                                    logger.Error(ex.Message);
                                                    logger.Error("Info_index.ConditionCreateUpdate00_a_02 Create Fail@" + ":" + "  pat_id=" + Spat_id + " ,Sinfo_date=" + Sinfo_date + " ,pif_name=" + Spif_name + " ,pif_ic=" + Spif_ic + " ,Info_index ExceptionCount: " + ExceptionCount.ToString());
                                                    ExceptionCount++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }// if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                        }
                    }
                    //sw1.Stop();

                    //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        if (CondSuccessList02.Count > 0)
                        {

                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                             */

                            //logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                            //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                            //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                            //     HttpContext.Current.Request.UserHostAddress);
                            //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1
                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                 updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                 new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                 "127.0.0.1");
                            i_AdminSecurity_02 = CondSuccessList02.Count;
                            logger.Info(" Sure a_02_AdminSecurity_02_cnt:" + i_AdminSecurity_02.ToString());
                        }
                        /*if (stFailList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                         */

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create Fail@" + "UL_CLINICAL_ConditionCreateUpdate00_a_02");
                    }
                    sw.Stop();
                    logger.Info("ConditionCreateUpdate00_a_02_Audit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                    //logger.Info("Cond_zinfo_a_02 Create/Update exec:" + CondCounter + "/" + CondDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");

                }

                /**********zinfo_a_03***************/
                //zinfo_a_03
                int a_03_cnt = 0;
                sCondSQL = "";
                CondDt = null;
                //沒有pif_ic的不要，因為沒辦法去雲端找LastUpdateTime(ExistObj.Desc)
                sCondSQL = sCondSQL + "  select aa.*, p.*  from zinfo_a_03  aa  left join pat_info p  on  p.pif_id =  aa.pat_id  ";
                sCondSQL = sCondSQL + "  where pif_ic is not null ";
                //sCondSQL = sCondSQL + "  and  pat_id ='200' ";
                //sCondSQL = sCondSQL + "  select *  from zinfo_a_03  where 1=1    ";
                // sCondSQL = sCondSQL + "  and  ( pat_id ='387'  or pat_id ='579' )";
                CondDt = CondDb.Query(sCondSQL);

                int i_Security_a_CU03_Audit_cnt = 0;
                int i_AdminSecurity_03 = 0;

                if (CondDt.Rows.Count > 0)
                {
                    a_03_cnt = CondDt.Rows.Count;
                    //測試時先跳過//ainfo_a_03
                    //donothing
                    //}
                    //else
                    //{

                    int ConditionCreateUpdate00_a_03Counter = 1;
                    int ExceptionCount = 1;
                    int CondCounter = 0;

                    //Stopwatch sw1 = new Stopwatch();
                    //sw1.Start();

                    foreach (DataRow row in CondDt.Rows)
                    {
                        /*SecurityEvent Issue*/
                        AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime infoDateTime = new DateTime();
                        string StrInfo_date = row["info_date"].ToString();
                        infoDateTime = Convert.ToDateTime(StrInfo_date);
                        DateTime lastUpdateDateTime = new DateTime();


                        if (existObj != null)
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            {
                                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                logger.Info(" The lastUpdateDateTime of the patient " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                            }
                        //for test
                        if (infoDateTime >= lastUpdateDateTime)
                        {
                            //do nothing 
                        }
                        //不用比較時間永遠可以跑
                        if (1 == 1)
                        //if (existObj != null && (existObj.Desc == null || infoDateTime >= lastUpdateDateTime))
                        {
                            //符合上述條件以下才做事


                            CondCounter++;
                            string sPat_id = row["pat_id"].ToString().Trim();
                            string sInfo_date = row["info_date"].ToString().Trim();
                            if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                            {
                                //進去class把欄位重整一下，如果多筆也可以都包在UsrInf_Cond_zinfo_a_03_List中
                                //用UsrInf_Cond_zinfo_a_03_List[0] 來取
                                var UsrInf_Cond_zinfo_a_03_List = UsrInf_Cond_zinfo_a_03.GetData(sPat_id, sInfo_date);

                                //這個sPat_id, sInfo_date 條件下從zinfo_a_01找出Web UI哪些項目被勾選或是被填入
                                if (UsrInf_Cond_zinfo_a_03_List.Count > 0)
                                {
                                    //為了防止會有多筆，事實上可能只有一筆，還是用for..loop呼叫，如果只跑一圈也沒關係
                                    for (int kk = 0; kk <= UsrInf_Cond_zinfo_a_03_List.Count - 1; kk++)
                                    {
                                        //羅列出每個欄位，準備當做userInfo00. ConditionCreateUpdate00(many para...)
                                        //many parameters 
                                        string Spat_id = UsrInf_Cond_zinfo_a_03_List[kk].pat_id;
                                        string Sinfo_date = UsrInf_Cond_zinfo_a_03_List[kk].info_date;
                                        string Sinfo_user = UsrInf_Cond_zinfo_a_03_List[kk].info_user;
                                        string Schk_1 = UsrInf_Cond_zinfo_a_03_List[kk].chk_1;
                                        string Schk_2 = UsrInf_Cond_zinfo_a_03_List[kk].chk_2;
                                        string Stxt_3 = UsrInf_Cond_zinfo_a_03_List[kk].txt_3;
                                        string Schk_4 = UsrInf_Cond_zinfo_a_03_List[kk].chk_4;
                                        string Stxt_5 = UsrInf_Cond_zinfo_a_03_List[kk].txt_5;
                                        string Schk_6 = UsrInf_Cond_zinfo_a_03_List[kk].chk_6;
                                        string Stxt_7 = UsrInf_Cond_zinfo_a_03_List[kk].txt_7;
                                        string Schk_8 = UsrInf_Cond_zinfo_a_03_List[kk].chk_8;
                                        string Stxt_9 = UsrInf_Cond_zinfo_a_03_List[kk].txt_9;
                                        string Schk_10 = UsrInf_Cond_zinfo_a_03_List[kk].chk_10;
                                        string Stxt_11 = UsrInf_Cond_zinfo_a_03_List[kk].txt_11;
                                        string Schk_12 = UsrInf_Cond_zinfo_a_03_List[kk].chk_12;
                                        string Stxt_13 = UsrInf_Cond_zinfo_a_03_List[kk].txt_13;
                                        string Stxt_14 = UsrInf_Cond_zinfo_a_03_List[kk].txt_14;
                                        string Stxt_15 = UsrInf_Cond_zinfo_a_03_List[kk].txt_15;

                                        string Spif_name = UsrInf_Cond_zinfo_a_03_List[kk].pif_name;
                                        string Spif_ic = UsrInf_Cond_zinfo_a_03_List[kk].pif_ic;

                                        string SorgId = orgId;
                                        string SorgName = orgName;

                                        if (string.IsNullOrWhiteSpace(SorgId))
                                        {
                                            logger.Error("SorgId = " + SorgId + " is not existed .");
                                            return;
                                        }
                                        //  pat_id = 387  王岩生  雲端有資料  db:myhaisv3_newnu2  OK on  var patient = client.Read<Patient>("Patient/" + patientId);
                                        //  pad-id = 579  涂成英  雲端有資料
                                        //if (Spat_id == "387")
                                        //if ((Spat_id == "387") || (Spat_id == "579"))
                                        //防止Spif_ic空值進入因為 patientId = "PAT32000800.8066." + Spif_ic; 組成架構 ，即使GetData pat_id有值， //pif_ic沒有值，也不用跑UserInfo00.CreateUpdate_a_0X
                                        //屆時到這段var patientPrepared = client.Read<Patient>("Patient/" + patientId);鐵定錯，就不要上傳了
                                        // if (Spat_id == "200")//王凤英
                                        {
                                            if (!(string.IsNullOrWhiteSpace(Spif_ic)))
                                            {
                                                AuditObject ConditionCreateUpdate00_a_03_Audit = null;
                                                AuditObject existObj00 = new AuditObject();
                                                try
                                                {
                                                    ConditionCreateUpdate00_a_03_Audit = MyUserInfo00.ConditionCreateUpdate00_a_03(Spat_id, Sinfo_date, Sinfo_user, Schk_1, Schk_2, Stxt_3, Schk_4, Stxt_5,
                                                     Schk_6, Stxt_7, Schk_8, Stxt_9, Schk_10, Stxt_11, Schk_12, Stxt_13, Stxt_14, Stxt_15, Spif_name, Spif_ic, SorgId, SorgName);
                                                    //MyUserInfo00.ConditionCreateUpdate00_a_03(Spat_id, Sinfo_date, Sinfo_user, Schk_1, Schk_2, Stxt_3, Schk_4, Stxt_5,
                                                    //     Schk_6, Stxt_7, Schk_8, Stxt_9, Schk_10, Stxt_11, Schk_12, Stxt_13, Stxt_14, Stxt_15, Spif_name, Spif_ic, SorgId, SorgName);

                                                    if (ConditionCreateUpdate00_a_03_Audit.Success) i_Security_a_CU03_Audit_cnt++;
                                                    logger.Info(" Sure a_03_CU_cnt:" + i_Security_a_CU03_Audit_cnt.ToString());


                                                    logger.Info("Info_index:Try to go ConditionCreateUpdate00_a_03 Create/Update:" + ConditionCreateUpdate00_a_03Counter + "/" + CondDt.Rows.Count + ",pat_id=" + Spat_id + ",pif_name=" + Spif_name + ",pif_ic=" + Spif_ic + ",orgId=" + SorgId);
                                                    ConditionCreateUpdate00_a_03Counter++;
                                                    if (ConditionCreateUpdate00_a_03_Audit.Success)
                                                    {
                                                        //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                                        AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                                        updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                                        //ObservationAudit.Desc = updateObj.Desc;
                                                        //successList.Add(ObservationAudit);
                                                        //拿updateObj修改過的updateObj.Desc去更新successList
                                                        CondSuccessList03.Add(updateObj);
                                                        //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                        existObj00 = updateObj;
                                                    }
                                                    else
                                                        CondFailList03.Add(ConditionCreateUpdate00_a_03_Audit);


                                                }
                                                catch (Exception ex)
                                                {
                                                    logger.Error(ex.Message);
                                                    logger.Error("Info_index.ConditionCreateUpdate00_a_03 Create Fail@" + ":" + "  pat_id=" + Spat_id + " ,Sinfo_date=" + Sinfo_date + " ,pif_name=" + Spif_name + " ,pif_ic=" + Spif_ic + " ,Info_index ExceptionCount: " + ExceptionCount.ToString());
                                                    ExceptionCount++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }//if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                        }
                    }
                    //sw1.Stop();
                    //logger.Info("Cond_zinfo_a_03 Create/Update exec:" + CondCounter + "/" + CondDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");           
                    //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        if (CondSuccessList03.Count > 0)
                        {

                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                             */

                            //logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                            //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                            //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                            //     HttpContext.Current.Request.UserHostAddress);
                            //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1
                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                 updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                 new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                 "127.0.0.1");
                            i_AdminSecurity_03 = CondSuccessList03.Count;
                            logger.Info(" Sure a_03_AdminSecurity_01_cnt:" + i_AdminSecurity_03.ToString());
                        }
                        /*if (stFailList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                         */

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create Fail@" + "UL_CLINICAL_ConditionCreateUpdate00_a_03");
                    }
                    sw.Stop();
                    logger.Info("ConditionCreateUpdate00_a_03_Audit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"

                }

                /**********zinfo_a_04***************/
                //zinfo_a_04
                int a_04_cnt = 0;
                sCondSQL = "";
                CondDt = null;
                //沒有pif_ic的不要，因為沒辦法去雲端找LastUpdateTime(ExistObj.Desc)
                //sCondSQL = sCondSQL + "  select *  from zinfo_a_04  where 1=1    ";
                sCondSQL = sCondSQL + "  select aa.*, p.*  from zinfo_a_04  aa  left join pat_info p  on  p.pif_id =  aa.pat_id  ";
                sCondSQL = sCondSQL + "  where pif_ic is not null ";
                //sCondSQL = sCondSQL + "  and  pat_id ='200' ";
                //sCondSQL = sCondSQL + "  and ( pat_id ='387'  or pat_id ='579' )  ";
                CondDt = CondDb.Query(sCondSQL);

                int i_Security_a_CU04_Audit_cnt = 0;
                int i_AdminSecurity_04 = 0;

                if (CondDt.Rows.Count > 0)
                {
                    a_04_cnt = CondDt.Rows.Count;
                    //測試時先跳過//ainfo_a_04
                    //donothing
                    //}
                    //else
                    //{

                    int ConditionCreateUpdate00_a_04Counter = 1;
                    int ExceptionCount = 1;
                    int CondCounter = 0;

                    //Stopwatch sw1 = new Stopwatch();
                    //sw1.Start();

                    foreach (DataRow row in CondDt.Rows)
                    {
                        /*SecurityEvent Issue*/
                        AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime infoDateTime = new DateTime();
                        string StrInfo_date = row["info_date"].ToString();
                        infoDateTime = Convert.ToDateTime(StrInfo_date);
                        DateTime lastUpdateDateTime = new DateTime();


                        if (existObj != null)
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            {
                                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                logger.Info(" The lastUpdateDateTime of the patient " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                            }
                        //for test
                        if (infoDateTime >= lastUpdateDateTime)
                        {
                            //do nothing 
                        }
                        //不用比較時間永遠可以跑
                        if (1 == 1)
                        //if (existObj != null && (existObj.Desc == null || infoDateTime >= lastUpdateDateTime))
                        {
                            //符合上述條件以下才做事






                            CondCounter++;
                            string sPat_id = row["pat_id"].ToString().Trim();
                            string sInfo_date = row["info_date"].ToString().Trim();
                            if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                            {
                                //進去class把欄位重整一下，如果多筆也可以都包在UsrInf_Cond_zinfo_a_04_List中
                                //用UsrInf_Cond_zinfo_a_04_List[0] 來取
                                var UsrInf_Cond_zinfo_a_04_List = UsrInf_Cond_zinfo_a_04.GetData(sPat_id, sInfo_date);

                                //這個sPat_id, sInfo_date 條件下從zinfo_a_01找出Web UI哪些項目被勾選或是被填入
                                if (UsrInf_Cond_zinfo_a_04_List.Count > 0)
                                {
                                    //為了防止會有多筆，事實上可能只有一筆，還是用for..loop呼叫，如果只跑一圈也沒關係
                                    for (int kk = 0; kk <= UsrInf_Cond_zinfo_a_04_List.Count - 1; kk++)
                                    {
                                        //羅列出每個欄位，準備當做userInfo00. ConditionCreateUpdate00(many para...)
                                        //many parameters 
                                        string Spat_id = UsrInf_Cond_zinfo_a_04_List[kk].pat_id;
                                        string Sinfo_date = UsrInf_Cond_zinfo_a_04_List[kk].info_date;
                                        string Sinfo_user = UsrInf_Cond_zinfo_a_04_List[kk].info_user;
                                        string Schk_1 = UsrInf_Cond_zinfo_a_04_List[kk].chk_1;
                                        string Stxt_2 = UsrInf_Cond_zinfo_a_04_List[kk].txt_2;

                                        string Spif_name = UsrInf_Cond_zinfo_a_04_List[kk].pif_name;
                                        string Spif_ic = UsrInf_Cond_zinfo_a_04_List[kk].pif_ic;

                                        string SorgId = orgId;
                                        string SorgName = orgName;

                                        if (string.IsNullOrWhiteSpace(SorgId))
                                        {
                                            logger.Error("SorgId = " + SorgId + " is not existed .");
                                            return;
                                        }
                                        //  pat_id = 387  王岩生  雲端有資料  db:myhaisv3_newnu2  OK on  var patient = client.Read<Patient>("Patient/" + patientId);
                                        //  pad_id = 579  涂成英  雲端有資料
                                        //if ((Spat_id == "387") || (Spat_id == "579"))
                                        //防止Spif_ic空值進入因為 patientId = "PAT32000800.8066." + Spif_ic; 組成架構 ，即使GetData pat_id有值， //pif_ic沒有值，也不用跑UserInfo00.CreateUpdate_a_0X
                                        //屆時到這段var patientPrepared = client.Read<Patient>("Patient/" + patientId);鐵定錯，就不要上傳了
                                        // if (Spat_id == "200")//王凤英
                                        {
                                            if (!(string.IsNullOrWhiteSpace(Spif_ic)))
                                            {
                                                AuditObject ConditionCreateUpdate00_a_04_Audit = null;
                                                AuditObject existObj00 = new AuditObject();

                                                try
                                                {
                                                    ConditionCreateUpdate00_a_04_Audit = MyUserInfo00.ConditionCreateUpdate00_a_04(Spat_id, Sinfo_date, Sinfo_user, Schk_1, Stxt_2, Spif_name, Spif_ic, SorgId, SorgName);
                                                    //MyUserInfo00.ConditionCreateUpdate00_a_04(Spat_id, Sinfo_date, Sinfo_user, Schk_1, Stxt_2, Spif_name, Spif_ic, SorgId, SorgName);

                                                    if (ConditionCreateUpdate00_a_04_Audit.Success) i_Security_a_CU04_Audit_cnt++;
                                                    logger.Info(" Sure a_04_CU_cnt:" + i_Security_a_CU04_Audit_cnt.ToString());

                                                    logger.Info("Info_index:Try to go ConditionCreateUpdate00_a_04 Create/Update:" + ConditionCreateUpdate00_a_04Counter + "/" + CondDt.Rows.Count + ",pat_id=" + Spat_id + ",pif_name=" + Spif_name + ",pif_ic=" + Spif_ic + ",orgId=" + SorgId);
                                                    ConditionCreateUpdate00_a_04Counter++;


                                                    if (ConditionCreateUpdate00_a_04_Audit.Success)
                                                    {
                                                        //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                                        AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                                        updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                                        //ObservationAudit.Desc = updateObj.Desc;
                                                        //successList.Add(ObservationAudit);
                                                        //拿updateObj修改過的updateObj.Desc去更新successList
                                                        CondSuccessList04.Add(updateObj);
                                                        //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                        existObj00 = updateObj;
                                                    }
                                                    else
                                                        CondFailList04.Add(ConditionCreateUpdate00_a_04_Audit);

                                                }
                                                catch (Exception ex)
                                                {
                                                    logger.Error(ex.Message);
                                                    logger.Error("Info_index.ConditionCreateUpdate00_a_04 Create Fail@" + ":" + "  pat_id=" + Spat_id + " ,Sinfo_date=" + Sinfo_date + " ,pif_name=" + Spif_name + " ,pif_ic=" + Spif_ic + " ,Info_index ExceptionCount: " + ExceptionCount.ToString());
                                                    ExceptionCount++;
                                                }
                                            }
                                        }

                                    }
                                }
                            }//if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                        }
                    }
                    //sw1.Stop();
                    //logger.Info("Cond_zinfo_a_04 Create/Update exec:" + CondCounter + "/" + CondDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");

                    //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        if (CondSuccessList04.Count > 0)
                        {

                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                             */

                            //logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                            //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                            //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                            //     HttpContext.Current.Request.UserHostAddress);
                            //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1
                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                 updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                 new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                 "127.0.0.1");
                            i_AdminSecurity_04 = CondSuccessList04.Count;
                            logger.Info(" Sure a_04_AdminSecurity_04_cnt:" + i_AdminSecurity_04.ToString());

                        }
                        /*if (stFailList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                         */

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create Fail@" + "UL_CLINICAL_ConditionCreateUpdate00_a_04");
                    }
                    sw.Stop();
                    logger.Info("ObservationAudit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"


                }

                /**********zinfo_a_05***************/
                //zinfo_a_05
                int a_05_cnt = 0;
                sCondSQL = "";
                CondDt = null;
                //沒有pif_ic的不要，因為沒辦法去雲端找LastUpdateTime(ExistObj.Desc)
                sCondSQL = sCondSQL + "  select aa.*, p.*  from zinfo_a_05  aa  left join pat_info p  on  p.pif_id =  aa.pat_id  ";
                sCondSQL = sCondSQL + "  where pif_ic is not null ";
                //sCondSQL = sCondSQL + "  and  pat_id ='200' ";
                //sCondSQL = sCondSQL + "  select *  from zinfo_a_05  where 1=1    ";
                // sCondSQL = sCondSQL + "  and ( pat_id ='387'  or pat_id ='579' ) ";
                CondDt = CondDb.Query(sCondSQL);

                int i_Security_a_CU05_Audit_cnt = 0;
                int i_AdminSecurity_05 = 0;

                if (CondDt.Rows.Count > 0)
                {
                    a_05_cnt = CondDt.Rows.Count;
                    //測試時先跳過//ainfo_a_05
                    //donothing
                    //}
                    //else
                    //{

                    int ConditionCreateUpdate00_a_05Counter = 1;
                    int ExceptionCount = 1;
                    int CondCounter = 0;

                    //Stopwatch sw1 = new Stopwatch();
                    //sw1.Start();

                    foreach (DataRow row in CondDt.Rows)
                    {
                        /*SecurityEvent Issue*/
                        AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime infoDateTime = new DateTime();
                        string StrInfo_date = row["info_date"].ToString();
                        infoDateTime = Convert.ToDateTime(StrInfo_date);
                        DateTime lastUpdateDateTime = new DateTime();


                        if (existObj != null)
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            {
                                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                logger.Info(" The lastUpdateDateTime of the patient " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                            }
                        //for test
                        if (infoDateTime >= lastUpdateDateTime)
                        {
                            //do nothing 
                        }
                        //不用比較時間永遠可以跑
                        if (1 == 1)
                        //if (existObj != null && (existObj.Desc == null || infoDateTime >= lastUpdateDateTime))
                        {
                            //符合上述條件以下才做事

                            CondCounter++;
                            string sPat_id = row["pat_id"].ToString().Trim();
                            string sInfo_date = row["info_date"].ToString().Trim();
                            if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                            {
                                //進去class把欄位重整一下，如果多筆也可以都包在UsrInf_Cond_zinfo_a_05_List中
                                //用UsrInf_Cond_zinfo_a_05_List[0] 來取
                                var UsrInf_Cond_zinfo_a_05_List = UsrInf_Cond_zinfo_a_05.GetData(sPat_id, sInfo_date);

                                //這個sPat_id, sInfo_date 條件下從zinfo_a_01找出Web UI哪些項目被勾選或是被填入
                                if (UsrInf_Cond_zinfo_a_05_List.Count > 0)
                                {
                                    //為了防止會有多筆，事實上可能只有一筆，還是用for..loop呼叫，如果只跑一圈也沒關係
                                    for (int kk = 0; kk <= UsrInf_Cond_zinfo_a_05_List.Count - 1; kk++)
                                    {
                                        //羅列出每個欄位，準備當做userInfo00. ConditionCreateUpdate00(many para...)
                                        //many parameters 
                                        string Spat_id = UsrInf_Cond_zinfo_a_05_List[kk].pat_id;
                                        string Sinfo_date = UsrInf_Cond_zinfo_a_05_List[kk].info_date;
                                        string Sinfo_user = UsrInf_Cond_zinfo_a_05_List[kk].info_user;
                                        string Schk_1 = UsrInf_Cond_zinfo_a_05_List[kk].chk_1;
                                        string Stxt_2 = UsrInf_Cond_zinfo_a_05_List[kk].txt_2;
                                        string Stxt_3 = UsrInf_Cond_zinfo_a_05_List[kk].txt_3;
                                        string Stxt_4 = UsrInf_Cond_zinfo_a_05_List[kk].txt_4;
                                        string Stxt_5 = UsrInf_Cond_zinfo_a_05_List[kk].txt_5;
                                        string Stxt_6 = UsrInf_Cond_zinfo_a_05_List[kk].txt_6;
                                        string Stxt_7 = UsrInf_Cond_zinfo_a_05_List[kk].txt_7;
                                        string Stxt_8 = UsrInf_Cond_zinfo_a_05_List[kk].txt_8;

                                        string SorgId = orgId;
                                        string SorgName = orgName;

                                        if (string.IsNullOrWhiteSpace(SorgId))
                                        {
                                            logger.Error("SorgId = " + SorgId + " is not existed .");
                                            return;
                                        }


                                        string Spif_name = UsrInf_Cond_zinfo_a_05_List[kk].pif_name;
                                        string Spif_ic = UsrInf_Cond_zinfo_a_05_List[kk].pif_ic;
                                        //  pat_id = 387  王岩生  雲端有資料  db:myhaisv3_newnu2  OK on  var patient = client.Read<Patient>("Patient/" + patientId);
                                        //  pad_id = 579  涂成英  雲端有資料
                                        // if ((Spat_id == "387") || (Spat_id == "579"))
                                        //防止Spif_ic空值進入因為 patientId = "PAT32000800.8066." + Spif_ic; 組成架構 ，即使GetData pat_id有值， //pif_ic沒有值，也不用跑UserInfo00.CreateUpdate_a_0X
                                        //屆時到這段var patientPrepared = client.Read<Patient>("Patient/" + patientId);鐵定錯，就不要上傳了
                                        // if (Spat_id == "200")//王凤英
                                        {
                                            if (!(string.IsNullOrWhiteSpace(Spif_ic)))
                                            {
                                                AuditObject ConditionCreateUpdate00_a_05_Audit = null;
                                                AuditObject existObj00 = new AuditObject();
                                                try
                                                {
                                                    ConditionCreateUpdate00_a_05_Audit = MyUserInfo00.ConditionCreateUpdate00_a_05(Spat_id, Sinfo_date, Sinfo_user, Schk_1,
                                                                                              Stxt_2, Stxt_3, Stxt_4, Stxt_5, Stxt_6, Stxt_7, Stxt_8,
                                                                                              Spif_name, Spif_ic, SorgId, SorgName);

                                                    if (ConditionCreateUpdate00_a_05_Audit.Success) i_Security_a_CU05_Audit_cnt++;
                                                    logger.Info(" Sure a_05_CU_cnt:" + i_Security_a_CU05_Audit_cnt.ToString());

                                                    // MyUserInfo00.ConditionCreateUpdate00_a_05(Spat_id, Sinfo_date, Sinfo_user, Schk_1,
                                                    //Stxt_2, Stxt_3, Stxt_4, Stxt_5, Stxt_6, Stxt_7, Stxt_8,
                                                    //Spif_name, Spif_ic, SorgId, SorgName);
                                                    logger.Info("Info_index:Try to go ConditionCreateUpdate00_a_05 Create/Update:" + ConditionCreateUpdate00_a_05Counter + "/" + CondDt.Rows.Count + ",pat_id=" + Spat_id + ",pif_name=" + Spif_name + ",pif_ic=" + Spif_ic + ",orgId=" + SorgId);
                                                    ConditionCreateUpdate00_a_05Counter++;

                                                    if (ConditionCreateUpdate00_a_05_Audit.Success)
                                                    {
                                                        //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                                        AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                                        updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                                        //ObservationAudit.Desc = updateObj.Desc;
                                                        //successList.Add(ObservationAudit);
                                                        //拿updateObj修改過的updateObj.Desc去更新successList
                                                        CondSuccessList05.Add(updateObj);
                                                        //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                        existObj00 = updateObj;
                                                    }
                                                    else
                                                        CondFailList05.Add(ConditionCreateUpdate00_a_05_Audit);


                                                }

                                                catch (Exception ex)
                                                {
                                                    logger.Error(ex.Message);
                                                    logger.Error("Info_index.ConditionCreateUpdate00_a_05 Create Fail@" + ":" + "  pat_id=" + Spat_id + " ,Sinfo_date=" + Sinfo_date + " ,pif_name=" + Spif_name + " ,pif_ic=" + Spif_ic + " ,Info_index ExceptionCount: " + ExceptionCount.ToString());
                                                    ExceptionCount++;
                                                }
                                            }
                                        }

                                    }
                                }
                            }//if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                        }
                    }
                    //sw1.Stop();
                    //logger.Info("Cond_zinfo_a_05 Create/Update exec:" + CondCounter + "/" + CondDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        if (CondSuccessList05.Count > 0)
                        {

                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                             */

                            //logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                            //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                            //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                            //     HttpContext.Current.Request.UserHostAddress);
                            //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1
                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                 updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                 new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                 "127.0.0.1");
                            i_AdminSecurity_05 = CondSuccessList05.Count;
                            logger.Info(" Sure a_05_AdminSecurity_05_cnt:" + i_AdminSecurity_05.ToString());
                        }
                        /*if (stFailList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                         */

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create Fail@" + "UL_CLINICAL_ConditionCreateUpdate00_a_05");
                    }
                    sw.Stop();
                    logger.Info("ObservationAudit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"

                }

                /**********zinfo_a_06***************/
                //zinfo_a_06
                int a_06_cnt = 0;
                sCondSQL = "";
                CondDt = null;
                //沒有pif_ic的不要，因為沒辦法去雲端找LastUpdateTime(ExistObj.Desc)
                sCondSQL = sCondSQL + "  select aa.*, p.*  from zinfo_a_06  aa  left join pat_info p  on  p.pif_id =  aa.pat_id  ";
                sCondSQL = sCondSQL + "  where pif_ic is not null ";
                //sCondSQL = sCondSQL + "  and  pat_id ='200' ";
                //sCondSQL = sCondSQL + "  select *  from zinfo_a_06  where 1=1    ";
                // sCondSQL = sCondSQL + "  and ( pat_id ='387'  or pat_id ='579' ) ";
                CondDt = CondDb.Query(sCondSQL);

                int i_Security_a_CU06_Audit_cnt = 0;
                int i_AdminSecurity_06 = 0;

                if (CondDt.Rows.Count > 0)
                {
                    a_06_cnt = CondDt.Rows.Count;
                    //測試時先跳過//ainfo_a_06
                    //donothing
                    //}
                    //else
                    //{

                    int ConditionCreateUpdate00_a_06Counter = 1;
                    int ExceptionCount = 1;

                    int CondCounter = 0;

                    //Stopwatch sw1 = new Stopwatch();
                    //sw1.Start();

                    foreach (DataRow row in CondDt.Rows)
                    {
                        /*SecurityEvent Issue*/
                        AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime infoDateTime = new DateTime();
                        string StrInfo_date = row["info_date"].ToString();
                        infoDateTime = Convert.ToDateTime(StrInfo_date);
                        DateTime lastUpdateDateTime = new DateTime();


                        if (existObj != null)
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            {
                                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                logger.Info(" The lastUpdateDateTime of the patient " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                            }
                        //for test
                        if (infoDateTime >= lastUpdateDateTime)
                        {
                            //do nothing 
                        }
                        //不用比較時間永遠可以跑
                        if (1 == 1)
                        //if (existObj != null && (existObj.Desc == null || infoDateTime >= lastUpdateDateTime))
                        {
                            //符合上述條件以下才做事






                            CondCounter++;

                            string sPat_id = row["pat_id"].ToString().Trim();
                            string sInfo_date = row["info_date"].ToString().Trim();
                            if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                            {
                                //進去class把欄位重整一下，如果多筆也可以都包在UsrInf_Cond_zinfo_a_06_List中
                                //用UsrInf_Cond_zinfo_a_06_List[0] 來取
                                var UsrInf_Cond_zinfo_a_06_List = UsrInf_Cond_zinfo_a_06.GetData(sPat_id, sInfo_date);

                                //這個sPat_id, sInfo_date 條件下從zinfo_a_01找出Web UI哪些項目被勾選或是被填入
                                if (UsrInf_Cond_zinfo_a_06_List.Count > 0)
                                {
                                    //為了防止會有多筆，事實上可能只有一筆，還是用for..loop呼叫，如果只跑一圈也沒關係
                                    for (int kk = 0; kk <= UsrInf_Cond_zinfo_a_06_List.Count - 1; kk++)
                                    {
                                        //羅列出每個欄位，準備當做userInfo00. ConditionCreateUpdate00(many para...)
                                        //many parameters 
                                        string Spat_id = UsrInf_Cond_zinfo_a_06_List[kk].pat_id;
                                        string Sinfo_date = UsrInf_Cond_zinfo_a_06_List[kk].info_date;
                                        string Sinfo_user = UsrInf_Cond_zinfo_a_06_List[kk].info_user;
                                        string Schk_1 = UsrInf_Cond_zinfo_a_06_List[kk].chk_1;
                                        string Stxt_2 = UsrInf_Cond_zinfo_a_06_List[kk].txt_2;
                                        string Schk_3 = UsrInf_Cond_zinfo_a_06_List[kk].chk_3;
                                        string Schk_4 = UsrInf_Cond_zinfo_a_06_List[kk].chk_4;
                                        string Stxt_5 = UsrInf_Cond_zinfo_a_06_List[kk].txt_5;
                                        string Stxt_6 = UsrInf_Cond_zinfo_a_06_List[kk].txt_6;
                                        string Schk_7 = UsrInf_Cond_zinfo_a_06_List[kk].chk_7;
                                        string Stxt_8 = UsrInf_Cond_zinfo_a_06_List[kk].txt_8;
                                        string Schk_9 = UsrInf_Cond_zinfo_a_06_List[kk].chk_9;
                                        string Stxt_10 = UsrInf_Cond_zinfo_a_06_List[kk].txt_10;
                                        string Schk_11 = UsrInf_Cond_zinfo_a_06_List[kk].chk_11;
                                        string Stxt_12 = UsrInf_Cond_zinfo_a_06_List[kk].txt_12;
                                        string Stxt_13 = UsrInf_Cond_zinfo_a_06_List[kk].txt_13;
                                        int Iopt_14 = UsrInf_Cond_zinfo_a_06_List[kk].opt_14;
                                        string Stxt_15 = UsrInf_Cond_zinfo_a_06_List[kk].txt_15;

                                        string Spif_name = UsrInf_Cond_zinfo_a_06_List[kk].pif_name;
                                        string Spif_ic = UsrInf_Cond_zinfo_a_06_List[kk].pif_ic;

                                        string SorgId = orgId;
                                        string SorgName = orgName;

                                        if (string.IsNullOrWhiteSpace(SorgId))
                                        {
                                            logger.Error("SorgId = " + SorgId + " is not existed .");
                                            return;
                                        }
                                        //  pat_id = 387  王岩生  雲端有資料  db:myhaisv3_newnu2  OK on  var patient = client.Read<Patient>("Patient/" + patientId);
                                        //  pad_id = 579  涂成英  雲端有資料
                                        // if ((Spat_id == "387") || (Spat_id == "579"))
                                        //if ((Spat_id == "387")) 
                                        //if ((Spat_id == "579")) 
                                        //防止Spif_ic空值進入因為 patientId = "PAT32000800.8066." + Spif_ic; 組成架構 ，即使GetData pat_id有值， //pif_ic沒有值，也不用跑UserInfo00.CreateUpdate_a_0X
                                        //屆時到這段var patientPrepared = client.Read<Patient>("Patient/" + patientId);鐵定錯，就不要上傳了
                                        // if (Spat_id == "200")//王凤英
                                        {
                                            if (!(string.IsNullOrWhiteSpace(Spif_ic)))
                                            {

                                                AuditObject ConditionCreateUpdate00_a_06_Audit = null;
                                                AuditObject existObj00 = new AuditObject();
                                                try
                                                {
                                                    ConditionCreateUpdate00_a_06_Audit = MyUserInfo00.ConditionCreateUpdate00_a_06(Spat_id, Sinfo_date, Sinfo_user, Schk_1,
                                                                                                   Stxt_2, Schk_3, Schk_4, Stxt_5, Stxt_6, Schk_7, Stxt_8,
                                                                                                   Schk_9, Stxt_10, Schk_11, Stxt_12, Stxt_13, Iopt_14, Stxt_15,
                                                                                                   Spif_name, Spif_ic, SorgId, SorgName);

                                                    //MyUserInfo00.ConditionCreateUpdate00_a_06(Spat_id, Sinfo_date, Sinfo_user, Schk_1,
                                                    //                                               Stxt_2, Schk_3, Schk_4, Stxt_5, Stxt_6, Schk_7, Stxt_8,
                                                    //                                               Schk_9, Stxt_10, Schk_11, Stxt_12, Stxt_13, Iopt_14, Stxt_15,
                                                    //                                               Spif_name, Spif_ic, SorgId, SorgName);

                                                    if (ConditionCreateUpdate00_a_06_Audit.Success) i_Security_a_CU06_Audit_cnt++;
                                                    logger.Info(" Sure a_06_CU_cnt:" + i_Security_a_CU06_Audit_cnt.ToString());


                                                    logger.Info("Info_index:Try to go ConditionCreateUpdate00_a_06 Create/Update:" + ConditionCreateUpdate00_a_06Counter + "/" + CondDt.Rows.Count + ",pat_id=" + Spat_id + ",pif_name=" + Spif_name + ",pif_ic=" + Spif_ic + ",orgId=" + SorgId);
                                                    ConditionCreateUpdate00_a_06Counter++;
                                                    if (ConditionCreateUpdate00_a_06_Audit.Success)
                                                    {
                                                        //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                                        AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                                        updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                                        //ObservationAudit.Desc = updateObj.Desc;
                                                        //successList.Add(ObservationAudit);
                                                        //拿updateObj修改過的updateObj.Desc去更新successList
                                                        CondSuccessList06.Add(updateObj);
                                                        //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                        existObj00 = updateObj;
                                                    }
                                                    else
                                                        CondFailList06.Add(ConditionCreateUpdate00_a_06_Audit);


                                                }

                                                catch (Exception ex)
                                                {
                                                    logger.Error(ex.Message);
                                                    logger.Error("Info_index.ConditionCreateUpdate00_a_06 Create Fail@" + ":" + "  pat_id=" + Spat_id + " ,Sinfo_date=" + Sinfo_date + " ,pif_name=" + Spif_name + " ,pif_ic=" + Spif_ic + " ,Info_index ExceptionCount: " + ExceptionCount.ToString());
                                                    ExceptionCount++;
                                                }
                                            }
                                        }

                                    }
                                }
                            } //if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                        }
                    }
                    //sw1.Stop();
                    //logger.Info("Cond_zinfo_a_06 Create/Update exec:" + CondCounter + "/" + CondDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");

                    //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        if (CondSuccessList06.Count > 0)
                        {

                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                             */

                            //logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                            //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                            //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                            //     HttpContext.Current.Request.UserHostAddress);
                            //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1
                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                 updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                 new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                 "127.0.0.1");
                            i_AdminSecurity_06 = CondSuccessList06.Count;
                            logger.Info(" Sure a_06_AdminSecurity_06_cnt:" + i_AdminSecurity_06.ToString());
                        }
                        /*if (stFailList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                         */
                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create Fail@" + "UL_CLINICAL_ConditionCreateUpdate00_a_06");
                    }
                    sw.Stop();
                    logger.Info("ObservationAudit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                }

                /**********zinfo_a_07***************/
                //zinfo_a_07
                int a_07_cnt = 0;
                sCondSQL = "";
                CondDt = null;
                //沒有pif_ic的不要，因為沒辦法去雲端找LastUpdateTime(ExistObj.Desc)
                sCondSQL = sCondSQL + "  select aa.*, p.*  from zinfo_a_07  aa  left join pat_info p  on  p.pif_id =  aa.pat_id  ";
                sCondSQL = sCondSQL + "  where pif_ic is not null ";
                //sCondSQL = sCondSQL + "  and  pat_id ='200' ";
                //sCondSQL = sCondSQL + "  select *  from zinfo_a_07  where 1=1    ";
                //sCondSQL = sCondSQL + "  and ( pat_id ='387'  or pat_id ='579' ) ";
                CondDt = CondDb.Query(sCondSQL);

                int i_Security_a_CU07_Audit_cnt = 0;
                int i_AdminSecurity_07 = 0;

                if (CondDt.Rows.Count > 0)
                {


                    a_07_cnt = CondDt.Rows.Count;
                    //測試時先跳過//ainfo_a_07
                    //donothing
                    //}
                    //else
                    //{

                    int ConditionCreateUpdate00_a_07Counter = 1;
                    int ExceptionCount = 1;

                    int CondCounter = 0;

                    //Stopwatch sw1 = new Stopwatch();
                    //sw1.Start();

                    foreach (DataRow row in CondDt.Rows)
                    {

                        /*SecurityEvent Issue*/
                        AuditObject existObj = ulClinicalList.Find(x => x.Reference.Split('.')[2] == row["pif_ic"].ToString());
                        IFormatProvider culture = new System.Globalization.CultureInfo("zh-CN", true);
                        DateTime infoDateTime = new DateTime();
                        string StrInfo_date = row["info_date"].ToString();
                        infoDateTime = Convert.ToDateTime(StrInfo_date);
                        DateTime lastUpdateDateTime = new DateTime();


                        if (existObj != null)
                            if (!String.IsNullOrWhiteSpace(existObj.Desc))
                            {
                                lastUpdateDateTime = DateTime.ParseExact(existObj.Desc, "yyyyMMddHHmmss", culture);
                                logger.Info(" The lastUpdateDateTime of the patient " + existObj.Reference.ToString() + " is " + lastUpdateDateTime);
                                //這個人有資料已經被上傳...1. Create被上傳 2.Update時被上傳 3.不一定是observation被上傳  
                            }
                        //for test
                        if (infoDateTime >= lastUpdateDateTime)
                        {
                            //do nothing 
                        }
                        //不用比較時間永遠可以跑
                        if (1 == 1)
                        //if (existObj != null && (existObj.Desc == null || infoDateTime >= lastUpdateDateTime))
                        {
                            //符合上述條件以下才做事

                            CondCounter++;
                            string sPat_id = row["pat_id"].ToString().Trim();
                            string sInfo_date = row["info_date"].ToString().Trim();
                            if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                            {
                                //進去class把欄位重整一下，如果多筆也可以都包在UsrInf_Cond_zinfo_a_07_List中
                                //用UsrInf_Cond_zinfo_a_07_List[0] 來取
                                var UsrInf_Cond_zinfo_a_07_List = UsrInf_Cond_zinfo_a_07.GetData(sPat_id, sInfo_date);

                                //這個sPat_id, sInfo_date 條件下從zinfo_a_01找出Web UI哪些項目被勾選或是被填入
                                if (UsrInf_Cond_zinfo_a_07_List.Count > 0)
                                {
                                    //為了防止會有多筆，事實上可能只有一筆，還是用for..loop呼叫，如果只跑一圈也沒關係
                                    for (int kk = 0; kk <= UsrInf_Cond_zinfo_a_07_List.Count - 1; kk++)
                                    {

                                        //羅列出每個欄位，準備當做userInfo00. ConditionCreateUpdate00(many para...)
                                        //many parameters 
                                        string Spat_id = UsrInf_Cond_zinfo_a_07_List[kk].pat_id;
                                        string Sinfo_date = UsrInf_Cond_zinfo_a_07_List[kk].info_date;
                                        string Sinfo_user = UsrInf_Cond_zinfo_a_07_List[kk].info_user;
                                        int Iopt_1 = UsrInf_Cond_zinfo_a_07_List[kk].opt_1;
                                        int Iopt_2 = UsrInf_Cond_zinfo_a_07_List[kk].opt_2;
                                        string Stxt_3 = UsrInf_Cond_zinfo_a_07_List[kk].txt_3;
                                        int Iopt_4 = UsrInf_Cond_zinfo_a_07_List[kk].opt_4;
                                        int Iopt_5 = UsrInf_Cond_zinfo_a_07_List[kk].opt_5;
                                        string Stxt_6 = UsrInf_Cond_zinfo_a_07_List[kk].txt_6;
                                        string Schk_7 = UsrInf_Cond_zinfo_a_07_List[kk].chk_7;
                                        string Schk_8 = UsrInf_Cond_zinfo_a_07_List[kk].chk_8;
                                        string Stxt_9 = UsrInf_Cond_zinfo_a_07_List[kk].txt_9;
                                        string Schk_10 = UsrInf_Cond_zinfo_a_07_List[kk].chk_10;
                                        string Stxt_11 = UsrInf_Cond_zinfo_a_07_List[kk].txt_11;
                                        string Schk_12 = UsrInf_Cond_zinfo_a_07_List[kk].chk_12;
                                        string Stxt_13 = UsrInf_Cond_zinfo_a_07_List[kk].txt_13;
                                        string Stxt_14 = UsrInf_Cond_zinfo_a_07_List[kk].txt_14;


                                        string Spif_name = UsrInf_Cond_zinfo_a_07_List[kk].pif_name;
                                        string Spif_ic = UsrInf_Cond_zinfo_a_07_List[kk].pif_ic;

                                        string SorgId = orgId;
                                        string SorgName = orgName;

                                        if (string.IsNullOrWhiteSpace(SorgId))
                                        {
                                            logger.Error("SorgId = " + SorgId + " is not existed .");
                                            return;
                                        }
                                        //  pat_id = 387  王岩生  雲端有資料  db:myhaisv3_newnu2  OK on  var patient = client.Read<Patient>("Patient/" + patientId);
                                        //  pad-id = 579  涂成英  雲端有資料
                                        //if ((Spat_id == "387"))
                                        //if ((Spat_id == "579"))
                                        //if ((Spat_id == "387") || (Spat_id == "579"))
                                        //防止Spif_ic空值進入因為 patientId = "PAT32000800.8066." + Spif_ic; 組成架構 ，即使GetData pat_id有值， //pif_ic沒有值，也不用跑UserInfo00.CreateUpdate_a_0X
                                        //屆時到這段var patientPrepared = client.Read<Patient>("Patient/" + patientId);鐵定錯，就不要上傳了
                                        // if (Spat_id == "200")//王凤英
                                        {
                                            if (!(string.IsNullOrWhiteSpace(Spif_ic)))
                                            {
                                                AuditObject ConditionCreateUpdate00_a_07_Audit = null;
                                                AuditObject existObj00 = new AuditObject();

                                                try
                                                {
                                                    //MyUserInfo00.ConditionCreateUpdate00_a_07(Spat_id, Sinfo_date, Sinfo_user, Iopt_1, Iopt_2, Stxt_3,
                                                    //Iopt_4, Iopt_5, Stxt_6, Schk_7, Schk_8, Stxt_9, Schk_10,
                                                    //Stxt_11, Schk_12, Stxt_13, Stxt_14, Spif_name, Spif_ic, SorgId, SorgName);
                                                    ConditionCreateUpdate00_a_07_Audit = MyUserInfo00.ConditionCreateUpdate00_a_07(Spat_id, Sinfo_date, Sinfo_user, Iopt_1, Iopt_2, Stxt_3,
                                                                                                   Iopt_4, Iopt_5, Stxt_6, Schk_7, Schk_8, Stxt_9, Schk_10,
                                                                                                   Stxt_11, Schk_12, Stxt_13, Stxt_14, Spif_name, Spif_ic, SorgId, SorgName);

                                                    if (ConditionCreateUpdate00_a_07_Audit.Success) i_Security_a_CU07_Audit_cnt++;
                                                    logger.Info(" Sure a_07_CU_cnt:" + i_Security_a_CU07_Audit_cnt.ToString());


                                                    logger.Info("Info_index:Try to go ConditionCreateUpdate00_a_07 Create/Update:" + ConditionCreateUpdate00_a_07Counter + "/" + CondDt.Rows.Count + ",pat_id=" + Spat_id + ",pif_name=" + Spif_name + ",pif_ic=" + Spif_ic + ",orgId=" + SorgId);
                                                    ConditionCreateUpdate00_a_07Counter++;
                                                    if (ConditionCreateUpdate00_a_07_Audit.Success)
                                                    {
                                                        //記得要更新這次的lastUpdateDateTime，其餘不動只要更新Desc                                                    //updateClinicalList整包拿下來後修改某一病患的某個Desc，時間戳記是當下時間										
                                                        AuditObject updateObj = updateClinicalList.Find(x => x.Reference == existObj.Reference);
                                                        updateObj.Desc = DateTime.Now.ToString("yyyyMMddHHmmss");
                                                        //ObservationAudit.Desc = updateObj.Desc;
                                                        //successList.Add(ObservationAudit);
                                                        //拿updateObj修改過的updateObj.Desc去更新successList
                                                        CondSuccessList07.Add(updateObj);
                                                        //logger.Info(" Check this data :description(LastUpdTime) on the Cloud: " + ",PAT_NO=" + iPAT_NO.ToString() + ",RESULT_DATE=" + sRESULT_DATE + ",pif_name=" + spif_name + ",pif_ic=" + spif_ic + ",orgId=" + SorgId);
                                                        existObj00 = updateObj;
                                                    }
                                                    else
                                                        CondFailList07.Add(ConditionCreateUpdate00_a_07_Audit);

                                                }
                                                catch (Exception ex)
                                                {
                                                    logger.Error(ex.Message);
                                                    logger.Error("Info_index.ConditionCreateUpdate00_a_07 Create Fail@" + ":" + "  pat_id=" + Spat_id + " ,Sinfo_date=" + Sinfo_date + " ,pif_name=" + Spif_name + " ,pif_ic=" + Spif_ic + " ,Info_index ExceptionCount: " + ExceptionCount.ToString());
                                                    ExceptionCount++;
                                                }
                                            }
                                        }
                                    }
                                }
                            }//if (!((string.IsNullOrWhiteSpace(sPat_id)) || (string.IsNullOrWhiteSpace(sInfo_date))))
                        }
                    }

                    //sw1.Stop();
                    //logger.Info("Cond_zinfo_a_07 Create/Update exec:" + CondCounter + "/" + CondDt.Rows.Count + " total cost " + (sw1.ElapsedMilliseconds / 1000).ToString() + " seconds");

                    //...BEGIN...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"
                    Stopwatch sw = new Stopwatch();
                    sw.Start();
                    try
                    {
                        if (CondSuccessList07.Count > 0)
                        {

                            /*uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, stSuccessList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                             */

                            //logger.Info("updateObj After detail:" + "Desc=" + existObj00.Desc + ",Reference=" + existObj00.Reference + ",Name=" + existObj00.Name + ",Success=" + existObj00.Success);

                            //uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                            //     updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                            //     new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                            //     HttpContext.Current.Request.UserHostAddress);
                            //避免HttpContext.Current.Request.UserHostAddress 為空寫成 127.0.0.1
                            uploadAdmin.AuditEventCreateUpdate("UL_CLINICAL", null, "Organization", orgId, orgId,
                                 updateClinicalList, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N0,
                                 new Hl7.Fhir.Model.Coding("http://www.datacom.com.tw", "UL_CLINICAL"),
                                 "127.0.0.1");
                            i_AdminSecurity_07 = CondSuccessList07.Count;
                            logger.Info(" Sure a_07_AdminSecurity_07_cnt:" + i_AdminSecurity_07.ToString());
                        }
                        /*if (stFailList.Count > 0)
                            uploadAdmin.AuditEventCreateUpdate(null, "Organization", orgId, orgId, failList.ToList<AuditObject>(),
                                Hl7.Fhir.Model.SecurityEvent.SecurityEventAction.U, Hl7.Fhir.Model.AuditEvent.AuditEventOutcome.N4,
                                new Hl7.Fhir.Model.CodeableConcept("http://www.datacom.com.tw", "UL_PATIENT"),
                                HttpContext.Current.Request.UserHostAddress);
                         */

                    }
                    catch (Exception ex)
                    {
                        logger.Error(ex.Message);
                        logger.Error("Audit Create Fail@" + "UL_CLINICAL_ConditionCreateUpdate00_a_07");
                    }
                    sw.Stop();
                    logger.Info("ObservationAudit Create/Update total cost " + (sw.ElapsedMilliseconds / 1000).ToString() + " seconds");
                    //...END...2015.04.14 更新到雲端 歸類 http://www.datacom.com.tw", "UL_CLINICAL"

                }

                //a_01~a_07  Total Time Cost
                swa1_a7.Stop();
                a0107TtlCnt = a_01_cnt + a_02_cnt + a_03_cnt + a_04_cnt + a_05_cnt + a_06_cnt + a_07_cnt;
                logger.Info("Cond_zinfo_a_01~a07 Create/Update exec:" + " a1_a7 total cost " + (swa1_a7.ElapsedMilliseconds / 1000).ToString() + " seconds");
                logger.Info(" Toal count = " + a0107TtlCnt + ":" + " a1= " + a_01_cnt.ToString() + " ,a2= " + a_02_cnt.ToString() + " ,a3= " + a_03_cnt.ToString() + " ,a4= " + a_04_cnt.ToString() + " ,a5= " + a_05_cnt.ToString() + " ,a6= " + a_06_cnt.ToString() + " ,a7= " + a_07_cnt.ToString());
            }//btn_UploadClinicalALL_click  END
        }
    }
}
