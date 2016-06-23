using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using ExcelHelper;
using System.Data;

namespace ExcelHelperUnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = @"E:\Code\CodeLibrary\ExcelHelperUnitTest\ExcelHelperTestFC.xls";
            string pp = @"E:\Code\CodeLibrary\ExcelHelperUnitTest\ExcelHelperTestSC.xls";
            using (ExcelComHelper excelHelper = new ExcelComHelper())
            {
                DataTable dt = excelHelper.InputExcelData(path);
                //if (dt != null)
                //{
                //    for (int i = 0; i < dt.Rows.Count; i++)
                //    {
                //        for (int j = 0; j < dt.Columns.Count; j++)
                //        {
                //            Console.Write(dt.Rows[i][j].ToString());
                //        }
                //    }
                //}
                excelHelper.OutputToExcel(dt, pp);
            }
        }
    }
}
