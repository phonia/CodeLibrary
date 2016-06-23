using DotNet.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ConsoleApplication1
{
    class Program
    {
        static void Main(string[] args)
        {
            string s = ConvertJson.ToJson(new TT { Name = "hy", Sex = "Female" });
            Console.WriteLine(s);
        }
    }

    public class TT
    {
        public String Name { get; set; }
        public String Sex { get; set; }
    }
}
