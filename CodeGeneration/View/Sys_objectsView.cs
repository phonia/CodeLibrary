using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.View
{
    public class Sys_objectsView
    {
        public string name { get; set; }
        public string object_id { get; set; }
    }

    public class Sys_ColumnsView
    {
        public string Name { get; set; }
        public string Object_id { get; set; }
        public string Columns_id { get; set; }
        public string User_type_id { get; set; }
        public string Max_length { get; set; }
        public string Is_nullable { get; set; }
        public string Is_identity { get; set; }
        public string IsPrimaryKey { get; set; }
    }

    public class Sys_TrelationView
    {
        public string MainName { get; set; }
        public string MainColumns { get; set; }
        public string RefName { get; set; }
        public string RefColumns { get; set; }
    }
}
