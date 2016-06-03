using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SitefinityWebApp
{
    public class OEEntity
    {
        public class Rootobject
        {
            public string version { get; set; }
            public string lastModified { get; set; }
            public Service[] services { get; set; } //
        }

        public class Service
        {
            public string name { get; set; }
            public string address { get; set; }
            public bool useRequest { get; set; }
            public Resource[] resources { get; set; } //
        }

        public class Resource
        {
            public string name { get; set; } // The entity name
            public string path { get; set; }
            public bool autoSave { get; set; }
            public Schema schema { get; set; } //
            public Operation[] operations { get; set; }
        }

        public class Schema
        {
            public string type { get; set; }
            public bool additionalProperties { get; set; }
            public Object properties { get; set; }
        }

        public class SingleProperty {
            public string type { get; set; }
            public string title { get; set; }
        }

        public class Operation
        {
            public string path { get; set; }
            public bool useBeforeImage { get; set; }
            public string type { get; set; }
            public string verb { get; set; }
            public Param[] _params { get; set; }
        }

        public class Param
        {
            public string name { get; set; }
            public string type { get; set; }
        }

        public class Metadata {
            public string entityName { get; set; }
            public string dsName { get; set; }
            public string ttName { get; set; }
            public string primaryKey { get; set; }
        }

    }
}