using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Runtime.InteropServices;
using System.Reflection;

namespace Infrastructure.Domain
{
    public enum DBFieldCategory
    {
        Usual=1,
        PKey=2,
        FKey=3,
        TRKey=4
    }

    public enum TableRelation
    { }

    public class DBTableAttribute : Attribute
    { }

    public class DBFieldAttribute : Attribute
    {
        public DBFieldCategory DBFieldCategory { get; private set; }
        public bool IsIdentity { get; private set; }
        public bool IsNull { get; private set; }
        public Int32 length { get; private set; }
        public Type Table { get; private set; }
        public string FkeyField { get; private set; }

        public DBFieldAttribute(DBFieldCategory dbFieldCategory, bool isIdentity,
            bool isNull, Int32 length)
            : this(dbFieldCategory, isIdentity, isNull, length, null, string.Empty)
        { }

        public DBFieldAttribute(DBFieldCategory dbFieldCategory, bool isIdentity,
            bool isNull, Int32 length,Type table,string fkeyField)
        {
            this.DBFieldCategory = dbFieldCategory;
            this.IsIdentity = isIdentity;
            this.IsNull = isNull;
            this.length = length;
            if (table != null)
                this.Table = table;
            if (FkeyField != string.Empty)
                this.FkeyField = fkeyField;
        }
    }
}
