using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Utilities.Json
{
    class StringJsonConverter:IJsonConverter
    {
        public string Serialize(object value)
        {
            return JavaScriptUtils.ToEscapedJavaScriptString((String)value);
        }
    }
}
