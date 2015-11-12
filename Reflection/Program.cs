using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace Reflection
{
    class Program
    {
        static void Main(string[] args)
        {
            //CLASSO input = new CLASSO() { Id = System.Guid.NewGuid(), Name = "hy", Count = 3 };
            //List<int> list = new List<int>(); list.Add(1); list.Add(2); list.Add(3);
            //input.list = list;
            //CLASST output = Convert.Map<CLASSO, CLASST>(input) as CLASST;

            DefaultParm defaultParm = new DefaultParm();
            defaultParm.Set(100);
            defaultParm.Set();
            Console.Read();
        }
    }

    public class DefaultParm
    {
        public int i { get; set; }
        public void Set(int input = 10)
        {
            this.i = input;
        }
    }

    public class CLASSO
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public int Count { get; set; }
        public List<int> list { get; set; }
    }

    public class CLASST
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Count { get; set; }
        public List<string> list { get; set; }
    }


    public class Convert
    {
        protected static Dictionary<string, Type> _baseMap = new Dictionary<string, Type>() 
                                                            {
                                                                {typeof(System.Guid).Name+typeof(System.String).Name,typeof(GuidToString)},
                                                                {typeof(Int32).Name+typeof(System.String).Name,typeof(Int32ToString)}
                                                            };
        protected static Dictionary<String, Action<object>> _genericMap = new Dictionary<string, Action<object>>();

        /// <summary>
        /// 
        /// </summary>
        /// <param name="action"></param>
        /// <param name="Name">List/Map/Queue+Name（object）</param>
        public static void AddGenericMap(Action<object> action,string Name)
        {
            if (!_genericMap.ContainsKey(Name))
            {
                _genericMap.Add(Name, action);
            }
        }


        public static TResult Map<TSource, TResult>(TSource input)
        {
            try
            {
                return (TResult)Map(input, typeof(TSource), typeof(TResult));
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        protected static object Map(object input, Type source, Type result)
        {
            var resultObject = Activator.CreateInstance(result);
            PropertyInfo[] resultProperties = result.GetProperties();
            PropertyInfo[] sourceProperties = source.GetProperties();
            if (resultProperties == null || sourceProperties == null) throw new Exception("error in Map");
            foreach (var item in resultProperties)
            {
                PropertyInfo selected = sourceProperties.Where(it => it.Name == item.Name).FirstOrDefault();
                if (selected == null) continue;
                if (item.PropertyType.IsValueType || item.PropertyType == typeof(System.String))
                {
                    item.SetValue(resultObject, BaseMap(selected.GetValue(input, null), selected.PropertyType, item.PropertyType), null);
                }
                else if (item.PropertyType.IsArray)
                {
                    item.SetValue(resultObject, GenericMap(selected.GetValue(input, null), selected.PropertyType, item.PropertyType), null);
                }
                else if (item.PropertyType.IsGenericType)
                { }
                else if (item.PropertyType.IsClass)
                { }
                else
                {
                    throw new Exception("error in Map");
                }
            }
            return null;
        }

        protected static object GenericMap(object input, Type source, Type result)
        {
            if (input == null) return null;
            if (source.IsValueType || source == typeof(System.String))
            {
                if (!_baseMap.ContainsKey(source.Name + result.Name)) throw new Exception("error in GenericMap");
                IBaseMap baseMap = Activator.CreateInstance(_baseMap[source.Name + result.Name]) as IBaseMap;
                return baseMap.MapToList(input);
            }
            if (source.IsClass)
            {
               //if(_genericMap.ContainsKey(List
            }

            return null;
        }

        protected static object BaseMap(object input, Type source, Type result)
        {
            if (source.Name == result.Name)
                return input;
            if (!_baseMap.ContainsKey(source.Name + result.Name)) throw new Exception("error in BaseMap");
            IBaseMap baseMap = Activator.CreateInstance(_baseMap[source.Name + result.Name]) as IBaseMap;
            return baseMap.Map(input);
        }
    }

    public interface IBaseMap {
        object Map(object input);
        object MapToList(object input);
    }

    public class GuidToString:IBaseMap
    {
        public object Map(object input)
        {
            if (input == null) return null;
            return input.ToString();
        }

        public object MapToList(object input)
        {
            if (input == null) return null;
            List<Guid> iList = (List<Guid>)input;
            List<string> oList = new List<string>();
            foreach (var item in iList)
            {
                oList.Add(item.ToString());
            }
            return oList;
        }
    }

    public class Int32ToString : IBaseMap
    {

        public object Map(object input)
        {
            if (input == null) return null;
            return input.ToString();
        }


        public object MapToList(object input)
        {
            if (input == null) return null;
            List<Int32> iList = (List<Int32>)input;
            List<string> oList = new List<string>();
            foreach (var item in iList)
            {
                oList.Add(item.ToString());
            }
            return oList;
        }
    }
}
