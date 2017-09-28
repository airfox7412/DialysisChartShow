using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Dialysis_Chart_Show.Information
{
    public class ClassMedicationAudit
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public Meta meta { get; set; }
        public Event @event { get; set; }
        public List<Participant> participant { get; set; }
        public Source source { get; set; }
        public List<Object> @object { get; set; }
    }

    public class ClassClinicalAudit
    {
        public string resourceType { get; set; }
        public string id { get; set; }
        public Meta meta { get; set; }
        public Event @event { get; set; }
        public Source source { get; set; }
        public List<Participant> participant { get; set; }
        public List<Object> @object { get; set; }
    }
}