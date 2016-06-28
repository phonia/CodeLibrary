using System;
using System.Collections.Generic;
using System.IO;
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
            return Serialize(value);
        }

        String Serialize(object value)
        {
            IJsonConverter jsonConverter = GetJsonConverter(value);
            return jsonConverter.Serialize(value);
        }

        IJsonConverter GetJsonConverter(object value)
        {
            if (value is System.String) return new StringJsonConverter();
            if (value.GetType().IsGenericType) return new GenericJsonConverter();
            if (value.GetType().IsArray) return new ArrayJsonConverter();
            throw new Exception("Unkown Type!");
        }


    }

    public interface IJsonConverter
    {
        String Serialize(object value);
    }
}
