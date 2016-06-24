using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Json
{
    public static class JsonConverterWrapper
    {
        public static String ToJson(this object obj)
        {
            if (obj == null) return String.Empty;
            JsonConverter jsonConverter = new JsonConverter(null);
            return jsonConverter.ToJson(obj);
        }
    }
}
