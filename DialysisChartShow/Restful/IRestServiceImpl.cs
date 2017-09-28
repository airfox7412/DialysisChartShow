using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using System.ServiceModel.Web;
using System.Data;

namespace RestService
{
    [ServiceContract]
    public interface IRestServiceImpl
    {
        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "CreateAppointment")]
        AppointmentResult[] CreateAppointment(InsertAppointmentData[] rData);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SearchAppointment")]
        AppointmentData[] SearchAppointment(SearchRequestData rData);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "DeleteAppointment")]
        DeleteResponseData DeleteAppointment(int[] rData);

        [OperationContract]
        [WebGet(UriTemplate = "ReadAppointment/{startDate}/{period}/{pv_floor}/{pv_sec}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Stream ReadAppointment(string startDate, string period, string pv_floor, string pv_sec);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "CreateAppointmentStatic")]
        AppointmentResult[] CreateAppointmentStatic(InsertAppointmentData[] rData);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "SearchAppointmentStatic")]
        AppointmentData[] SearchAppointmentStatic(SearchRequestData rData);

        [OperationContract]
        [WebInvoke(Method = "POST",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare,
            UriTemplate = "DeleteAppointmentStatic")]
        DeleteResponseData DeleteAppointmentStatic(int[] rData);

        [OperationContract]
        [WebGet(UriTemplate = "ReadAppointmentStatic/{startDate}/{period}/{pv_floor}/{pv_sec}",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Stream ReadAppointmentStatic(string startDate, string period, string pv_floor, string pv_sec);

        [OperationContract]
        [WebGet(UriTemplate = "ReadFhirOrganization",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        DataSet ReadFhirOrganization();

        [OperationContract]
        [WebGet(UriTemplate = "ReadFhirPopulationDistribution/{sTrdate}",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        DataSet ReadFhirPopulationDistribution(string sTrdate);

//=====20160330 add by ssi begin ==========================================================================================================================
        [OperationContract]
        [WebGet(UriTemplate = "ReadFhirDurationDistribution/{sTrdate}",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        DataSet ReadFhirDurationDistribution(string sTrdate);
//=====20160330 add by ssi end ==========================================================================================================================

//=====20160406 add by ssi begin ==========================================================================================================================
        [OperationContract]
        [WebGet(UriTemplate = "ReadFhirMRDistribution/{sTrdate}",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        DataSet ReadFhirMRDistribution(string sTrdate);
//=====20160406 add by ssi end ==========================================================================================================================

//=====20160408 add by ssi begin ==========================================================================================================================
        [OperationContract]
        [WebGet(UriTemplate = "ReadFhirDQDistribution/{sTrdate}",
            ResponseFormat = WebMessageFormat.Xml,
            BodyStyle = WebMessageBodyStyle.Wrapped)]
        DataSet ReadFhirDQDistribution(string sTrdate);
//=====20160408 add by ssi end ==========================================================================================================================

        [OperationContract]
        [WebGet(UriTemplate = "ReadPatient",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Stream ReadPatient();

        [OperationContract]
        [WebGet(UriTemplate = "ReadBed",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Stream ReadBed();

        [OperationContract]
        [WebGet(UriTemplate = "ReadMac",
            ResponseFormat = WebMessageFormat.Json,
            RequestFormat = WebMessageFormat.Json,
            BodyStyle = WebMessageBodyStyle.Bare)]
        Stream ReadMac();
    }
}
