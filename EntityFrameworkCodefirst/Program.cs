using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;

namespace EntityFrameworkCodefirst
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<Person> persons = new List<Person>();
            //persons.Add(new Manager() { Id = System.Guid.NewGuid(), Name = "zr" });
            //persons.Add(new staff() { Id = System.Guid.NewGuid(), Name = "hy" });
            //using (var data = new DataContext())
            //{
            //    List<Person> list = data.PersonSet.ToList();
            //}
            //int i = 100;

            //Action<int> dd = (it) => { i++; Console.Write(it + i); };

            //dd(0);
            //Animal a = new Bird();
            //Console.Write(a.Name+"/  "+a.Show()+"///"+a.GetType().ToString()+"////"+i.ToString());
            //Program p = new Program();
            //ThreadPool.QueueUserWorkItem(new WaitCallback(p.Proc), "sdfds");
            try
            {
                using (US us = new US())
                {
                    Console.Write("test//");
                }
            }
            catch (Exception ex)
            {
                Console.Write("sdfsdfsdfsd");
            }
        }

        public void  Proc(object arg)
        {
            Console.Write(Thread.CurrentThread.ManagedThreadId + arg.ToString());
        }
    }

    class US : IDisposable
    {
        public US()
        {
            throw new Exception("Create!");
        }

        public void Dispose()
        {
            
        }
    }

    public class Animal
    {
        public string Name { get; set; }
        public Animal()
        {
            Name = "Animal";
        }
        public virtual string Show()
        {
            return Name;
        }
    }

    public class Bird : Animal
    {
        public String Name { get; set; }
        public Bird()
        {
            Name = "Bird";
        }

        public override string Show()
        {
            return Name;
        }
    }
}
