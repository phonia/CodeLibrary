using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.Domain;

namespace CodeGeneration.Model
{
    [DBTable]
    public partial class Sys_objects
    {
        [DBField(Infrastructure.Domain.DBFieldCategory.PKey,false,false,50)]
        public string name { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.Usual,false,false,4)]
        public int object_id { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.Usual,false,false,2)]
        public string type { get; set; }
    }

    [DBTable]
    public partial class Sys_columns
    {
        [DBField(Infrastructure.Domain.DBFieldCategory.PKey,false,false,50)]
        public string name { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.Usual,false,false,4)]
        public int object_id { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.Usual,false,false,4)]
        public int columns_id { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.Usual,false,false,4)]
        public int user_type_id { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.Usual,false,false,4)]
        public int max_length { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.Usual,false,false,1)]
        public bool is_nullable { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.Usual,false,false,1)]
        public bool is_identity { get; set; }
        public bool isPrimaryKey { get; set; }
    }

    [DBTable]
    public partial class Sys_TRelation
    {
        [DBField(Infrastructure.Domain.DBFieldCategory.PKey,false,false,50)]
        public string mainname { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.FKey, false, false, 50)]
        public string maincolumns { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.FKey, false, false, 50)]
        public string refname { get; set; }
        [DBField(Infrastructure.Domain.DBFieldCategory.FKey, false, false, 50)]
        public string refcolumns { get; set; }
    }
}
