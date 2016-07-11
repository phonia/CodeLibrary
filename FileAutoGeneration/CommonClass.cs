using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FileAutoGeneration
{
    /// <summary>
    /// 未完成
    /// </summary>
    public class CommonClass
    {
        String _tab = "    ";
        String _sapce = " ";
        String _colon = ":";
        String _semicolon = ";";
        String _comma =",";
        public String Path { get; set; }
        public String ClassName{get;set;}
        public List<String> UsingNameSpace { get; set; }
        public String NameSpace { get; set; }
        public String BaseClass { get; set; }
        public List<String> Interfaces { get; set; }
        public Queue<String> Property { get; set; }
        public Queue<String> Method { get; set; }

        public void AutoGenerating()
        {
 
        }
    }
}
