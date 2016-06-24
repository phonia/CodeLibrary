using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Json
{
    public  class JsonConverter
    {
        public JsonConverter(JsonConverterSetting setting)
        { }

        public String ToJson(object value)
        {
            if (value is System.String) return StringFormat((String)value);
            return String.Empty;
        }
    }
}
