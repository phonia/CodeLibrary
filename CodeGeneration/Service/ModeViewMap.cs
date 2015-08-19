using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace CodeGeneration.Service
{
    public class ModeViewMap
    {
        public static List<T2> MapTo<T1, T2>(List<T1> T1list)
        {
            List<T2> T2List = new List<T2>();
            foreach (T1 item in T1list)
            {
                T2List.Add(MapTo<T1, T2>(item));
            }
            return T2List;
        }

        private static T2 MapTo<T1, T2>(T1 ti)
        {
            Type t1 = typeof(T1);
            Type t2 = typeof(T2);

            PropertyInfo[] properties1 = t1.GetProperties();
            PropertyInfo[] properties2 = t2.GetProperties();
            T2 to = (T2)Activator.CreateInstance(t2);

            foreach (PropertyInfo item in properties2)
            {
                PropertyInfo node = properties1.Where(it => it.Name.ToLower() == item.Name.ToLower()).FirstOrDefault();
                if (node != null)
                {
                    item.SetValue(to, node.GetValue(ti, null).ToString(), null);
                }
            }

            return to;
        }
    }
}
