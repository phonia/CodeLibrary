using ExcelHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Text;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            //List<EntityExcelRecord> list= EntityExcelRecord.GetAllRecordFromExcel(@"E:\Code\模型设计.xls");
            Test t = new Test();
        }
    }

    public class Test
    {
        public int i { get; set; }

        public void Print()
        {
            Console.WriteLine("Print!");
        }
    }
}