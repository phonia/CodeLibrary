using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace MultiThreadAndLock
{
    class Program
    {
        delegate void testDeleget(ReadObject ro,int i);

        static void Main(string[] args)
        {
            ReadObject r = new ReadObject();
            testDeleget[] t = new testDeleget[10];
            for (int i = 0; i < 6; i++)
            {
                t[i] = new testDeleget(test);
            }

            IAsyncResult[] ir = new IAsyncResult[10];
            for (int i = 0; i < 6; i++)
            {
                ir[i] = t[i].BeginInvoke(r, i, null, r);
            }

            for (int i = 0; i < 6; i++)
            {
                t[i].EndInvoke(ir[i]);
            }
            Console.ReadLine();
        }

        static void test(ReadObject ro,int j)
        {
            ro.Add(j);
            Thread.Sleep(1000);
            ro.Remove(j);


            Console.Write("Thread-+" + Thread.CurrentThread.ManagedThreadId + " : ");
            for (int i = 0; i < ro.StrList.Count; i++)
            {
                Console.Write(ro.StrList[i] + " ");
            }
            Console.WriteLine(" /");
        }
    }

    public class ReadObject
    {
        public List<string> StrList { get; set; }
        private static readonly object _lockObejct = new object();

        public ReadObject()
        {
            StrList = new List<string>();
        }

        public void Add(int count)
        {
            lock (_lockObejct)
            {
                for (int i = 0; i < 10; i++)
                {
                    StrList.Add((StrList.Count).ToString());
                    Thread.Sleep(10);
                }
            }
        }

        public void Remove(int count)
        {
            lock (_lockObejct)
            {
                for (int i = 0; i < 10; i++)
                {
                    if (StrList.Count > 0)
                    {
                        StrList.RemoveAt(StrList.Count - 1);
                        Thread.Sleep(10);
                    }
                }
            }
        }
    }
}
