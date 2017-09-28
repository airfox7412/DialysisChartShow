using System;
using System.Collections.Generic;
using System.Web;
using System.Web.Services;
using Ext.Net;

namespace Dialysis_Chart_Show
{
    /// <summary>
    ///PracInfos 的摘要描述
    /// </summary>
    [WebService(Namespace = "http://www.datacom.com.tw/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    public class PracInfos : IHttpHandler
    {

        public void ProcessRequest(HttpContext context)
        {
            //context.Response.ContentType = "text/plain";
            //context.Response.Write("Hello World");
            context.Response.ContentType = "text/json";

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

            Paging<PracInfo> PracInfos = PracInfo.PracInfosPaging(start, limit, sort, dir, query);

            context.Response.Write(string.Format("{{total:{1},'PracInfos':{0}}}", JSON.Serialize(PracInfos.Data), PracInfos.TotalRecords));
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