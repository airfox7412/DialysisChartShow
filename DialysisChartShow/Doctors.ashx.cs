using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Ext.Net;

namespace Dialysis_Chart_Show
{
    /// <summary>
    ///Doctors 的摘要描述
    /// </summary>
    public class Doctors : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            context.Response.ContentType = "text/plain";
            int start = 0;
            int limit = 10;
            string sort = string.Empty;
            string dir = string.Empty;
            string query = string.Empty;

            if (!string.IsNullOrEmpty(context.Request["start"]))
            {
                start = int.Parse(context.Request["start"]);
            }

            if (!string.IsNullOrEmpty(context.Request["limit"]))
            {
                limit = int.Parse(context.Request["limit"]);
            }

            if (!string.IsNullOrEmpty(context.Request["sort"]))
            {
                sort = context.Request["sort"];
            }

            if (!string.IsNullOrEmpty(context.Request["dir"]))
            {
                dir = context.Request["dir"];
            }

            if (!string.IsNullOrEmpty(context.Request["query"]))
            {
                query = context.Request["query"].ToUpper();
            }

            Paging<Doctor> Doctors = Doctor.DoctorsPaging(start, limit, sort, dir, query);

            context.Response.Write(string.Format("{{total:{1},'Doctors':{0}}}", JSON.Serialize(Doctors.Data), Doctors.TotalRecords));
        }

        public bool IsReusable
        {
            get
            {
                return false;
            }
        }
    }
}