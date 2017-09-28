using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dialysis_Chart_Show.Information
{
    public class ClassAuditEvent
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public Meta meta { get; set; }
        public Event @event { get; set; }
        public List<Participant> participant { get; set; }
        public Source source { get; set; }
        public List<Object> @object { get; set; }
    }

    public class Meta
    {
        public string versionId { get; set; }
        public string lastUpdated { get; set; }
    }

    public class Type
    {
        public string system { get; set; }
        public string code { get; set; }
    }

    public class Subtype
    {
        public string system { get; set; }
        public string code { get; set; }
    }

    public class Event
    {
        public Type type { get; set; }
        public List<Subtype> subtype { get; set; }
        public string action { get; set; }
        public string dateTime { get; set; }
        public string outcome { get; set; }
        public string outcomeDesc { get; set; }
    }

    public class Reference
    {
        public string reference { get; set; }
    }

    public class UserId
    {
        public string value { get; set; }
    }

    public class Network
    {
        public string address { get; set; }
        public string type { get; set; }
    }

    public class Participant
    {
        public Reference reference { get; set; }
        public UserId userId { get; set; }
        public Network network { get; set; }
    }

    public class Identifier
    {
        public string value { get; set; }
    }

    public class Source
    {
        public string site { get; set; }
        public Identifier identifier { get; set; }
    }

    public class Identifier2
    {
        public string system { get; set; }
        public string value { get; set; }
    }

    public class Reference2
    {
        public string reference { get; set; }
    }

    public class Object
    {
        public Identifier2 identifier { get; set; }
        public Reference2 reference { get; set; }
        public string name { get; set; }
        public string description { get; set; }
    }

}