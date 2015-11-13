using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EntityFramworkEnum
{
    class Program
    {
        static void Main(string[] args)
        {
            DataContext data = new DataContext();
            List<User> list = data.Users.ToList();
            Console.Read();
        }
    }
}
