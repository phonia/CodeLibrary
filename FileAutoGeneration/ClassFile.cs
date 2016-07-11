using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace FileAutoGeneration
{
    public class ClassFile
    {
        public List<String> FileName { get; set; }

        public String Path { get; set; }

        public List<String> UsingNameSpace { get; set; }

        public String NameSpace { get; set; }

        public String BaseClasss { get; set; }

        public List<String> Interfaces { get; set; }

        public ClassFile(List<String> fileName,String path,
            List<String> usingNamespace,String nameSpace,String BaseClass="",List<String> interfaces=null)
        {
            this.FileName = fileName;
            this.Path = path;
            this.UsingNameSpace = usingNamespace;
            this.NameSpace = nameSpace;
            this.BaseClasss = BaseClass;
            this.Interfaces = interfaces;
        }

        public void AutoGenerating()
        {
            if (FileName == null || FileName.Count <= 0) return;
            if (String.IsNullOrWhiteSpace(Path)) return;
            if (!Directory.Exists(Path)) return;
            if (File.Exists(Path +"\\"+ FileName+"CS")) return;
            for (int i = 0; i < FileName.Count; i++)
            {
                using (FileStream fs = new FileStream(Path + "\\" + FileName[i]+".CS", FileMode.OpenOrCreate))
                {
                    if (fs.CanWrite)
                    {
                        var sw = new StreamWriter(fs);

                        if (UsingNameSpace != null && UsingNameSpace.Count >= 0)
                        {
                            foreach (var node in UsingNameSpace)
                            {
                                sw.WriteLine(node);
                            }
                        }
                        sw.WriteLine(String.Empty);

                        if (!String.IsNullOrWhiteSpace(NameSpace))
                        {
                            sw.WriteLine("namespace " + NameSpace);
                        }
                        sw.WriteLine("{");

                        sw.WriteLine("    public class "+FileName[i]);

                        if (!String.IsNullOrWhiteSpace(BaseClasss))
                        {
                            sw.WriteLine(": "+BaseClasss);

                            if (Interfaces != null && Interfaces.Count >= 0)
                            {
                                foreach (var node in Interfaces)
                                {
                                    sw.WriteLine(", "+node);
                                }
                            }
                        }
                        else
                        {
                            if (Interfaces != null && Interfaces.Count >= 0)
                            {
                                for (int j = 0; j < Interfaces.Count; j++)
                                {
                                    if (j == 0) sw.WriteLine(": "+Interfaces[0]);
                                    sw.WriteLine(", "+Interfaces[j]);
                                }
                            }
                        }
                        sw.WriteLine("    {");

                        sw.WriteLine(String.Empty);

                        sw.WriteLine("    }");
                        sw.WriteLine("}");

                        sw.Dispose();
                        sw.Close();
                    }
                }
            }
        }
    }
}
