using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dialysis_Chart_Show.Information
{
    public class ClassAuditEventHistory
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public Meta meta { get; set; }
        public string type { get; set; }
        public string total { get; set; }
        //public Link2 link { get; set; }
        public List<Entry> entry { get; set; }
    }

    public class Meta2
    {
        public string lastUpdated { get; set; }
    }

    //public class Link2
    //{
    //    public string relation { get; set; }
    //    public string url { get; set; }
    //}

    public class Entry
    {
        public string fullUrl { get; set; }
        public Resource resource { get; set; }
    }

    public class Resource
    {
        public string resourceType { get; set; }
        public Meta meta { get; set; }
        public Event @event { get; set; }
        public List<Participant> participant { get; set; }
        public Source source { get; set; }
        public List<Object> @object { get; set; }
    }
}