using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
//using ExcelHelper;
using System.Data;
using System.Data.OleDb;
using System.Collections;
using System.IO;
using ExcelHelper;

namespace ExcelHelperUnitTest
{
    class Program
    {
        static void Main(string[] args)
        {
            #region Test
            //string path = @"E:\Code\CodeLibrary\ExcelHelperUnitTest\ExcelHelperTestFC.xls";
            //string pp = @"E:\Code\CodeLibrary\ExcelHelperUnitTest\ExcelHelperTestSC.xls";
            //using (ExcelComHelper excelHelper = new ExcelComHelper())
            //{
            //    DataTable dt = excelHelper.InputExcelData(path);
            //    //if (dt != null)
            //    //{
            //    //    for (int i = 0; i < dt.Rows.Count; i++)
            //    //    {
            //    //        for (int j = 0; j < dt.Columns.Count; j++)
            //    //        {
            //    //            Console.Write(dt.Rows[i][j].ToString());
            //    //        }
            //    //    }
            //    //}
            //    excelHelper.OutputToExcel(dt, pp);
            //}
            #endregion

            List<EntityExcelRecord> list = new List<EntityExcelRecord>();
            ArrayList tableList = ExcelOledbHelper.GetExcelTables(@"E:\Code\模型设计.xls");
            foreach (var item in tableList)
            {
                DataTable table = ExcelOledbHelper.InputFromExcel(@"E:\Code\模型设计.xls", item.ToString());
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    EntityExcelRecord record = new EntityExcelRecord();
                    record.ClassName = table.Rows[i]["类型名"] is DBNull ? String.Empty : table.Rows[i]["所属模型"].ToString();
                    record.EntityClass = table.Rows[i]["基类"] is DBNull ? String.Empty : table.Rows[i]["基类"].ToString();
                    record.TableName = table.Rows[i]["所属表"] is DBNull ? String.Empty : table.Rows[i]["所属表"].ToString();
                    record.PropertyName = table.Rows[i]["字段名称"] is DBNull ? String.Empty : table.Rows[i]["字段名称"].ToString();
                    record.PropertyType = table.Rows[i]["属性类型"] is DBNull ? String.Empty : table.Rows[i]["属性类型"].ToString();
                    record.FieldType = table.Rows[i]["字段类型"] is DBNull ? String.Empty : table.Rows[i]["字段类型"].ToString();
                    record.MaxLength = table.Rows[i]["最大长度"] is DBNull ? null : (int?)Convert.ToInt16(table.Rows[i]["最大长度"]);
                    record.ValueRegion = table.Rows[i]["取值范围"] is DBNull ? String.Empty : table.Rows[i]["取值范围"].ToString();
                    record.Decimal = table.Rows[i]["小数位数"] is DBNull ? null : (int?)Convert.ToInt16(table.Rows[i]["小数位数"]);
                    record.IsKey = table.Rows[i]["主键"] is DBNull ? false : true;
                    record.IsMultiPK=table.Rows[i]["复合主键"] is DBNull?false:true;
                    record.IsNull = table.Rows[i]["允许空"] is DBNull ? true : false;
                    record.IsIdentity=table.Rows[i]["自增长"] is DBNull?false:true;
                    record.DefaultValue = table.Rows[i]["默认值"] is DBNull ? String.Empty : table.Rows[i]["默认值"].ToString();
                    record.Description = table.Rows[i]["字段说明"] is DBNull ? String.Empty : table.Rows[i]["字段说明"].ToString();
                    record.RefrenceClassName = table.Rows[i]["参考表"] is DBNull ? String.Empty : table.Rows[i]["参考表"].ToString();
                    record.RefrencePropertyName = table.Rows[i]["参考字段"] is DBNull ? String.Empty : table.Rows[i]["参考字段"].ToString();
                    list.Add(record);
                }

                //Entity.tt
                foreach (var node in list)
                {
                    if (String.IsNullOrWhiteSpace(node.EntityClass)) continue;
                    //code
                    foreach (var record in list)
                    {
                        if (record.ClassName.Equals(node.ClassName) 
                            &&String.IsNullOrWhiteSpace(node.EntityClass)
                            && String.IsNullOrWhiteSpace(record.RefrenceClassName))
                        {
                            //code primitive property and complex propery
                        }
                        if (record.ClassName.Equals(node.ClassName) 
                            &&String.IsNullOrWhiteSpace(node.EntityClass)
                            && !String.IsNullOrWhiteSpace(record.RefrenceClassName))
                        {
                            //code navigation property
                        }
                    }
                }
   
                //EntityConfiguration.tt
                foreach (var node in list)
                {
                    if (String.IsNullOrWhiteSpace(node.EntityClass)) continue;
                    //code
                    if (node.EntityClass.Equals("EntityType"))
                    { }
                    if (node.EntityClass.Equals("ComplexType"))
                    { }
                }

                //DataContext.tt

                //DTO.tt
            }
        }
    }

    public class EntityExcelRecord
    {
        public String ClassName { get; set; }
        public String EntityClass { get; set; }
        public String TableName { get; set; }
        public String PropertyName { get; set; }
        public String PropertyType { get; set; }
        public String FieldType { get; set; }
        public int? MaxLength { get; set; }
        public String ValueRegion { get; set; }
        public int? Decimal { get; set; }
        public bool IsKey { get; set; }
        public bool IsMultiPK { get; set; }
        public bool IsNull { get; set; }
        public bool IsIdentity { get; set; }
        public String DefaultValue { get; set; }
        public String Description { get; set; }
        public String RefrenceClassName { get; set; }
        public String RefrencePropertyName { get; set; }
    }
}
