using ExcelHelper;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace CodeFirst
{
    class Program
    {
        static void Main(string[] args)
        {
            List<EntityExcelRecord> list= EntityExcelRecord.GetAllRecordFromExcel("");
        }
    }

    public class EntityExcelRecord
    {
        public String ClassName { get; set; }
        public String BaseClass { get; set; }
        public String MappingClass { get; set; }
        public String TableName { get; set; }
        public String PropertyName { get; set; }
        public String PropertyType { get; set; }
        public String FieldType { get; set; }
        public int? MaxLength { get; set; }
        public int? Decimal { get; set; }
        public bool IsKey { get; set; }
        public bool IsMultiPK { get; set; }
        public bool IsNull { get; set; }
        public bool IsIdentity { get; set; }
        public String DefaultValue { get; set; }
        public String Description { get; set; }
        public String RefrenceClassName { get; set; }
        public String RefrencePropertyName { get; set; }
        public String RefrenceRelation { get; set; }

        public static List<EntityExcelRecord> GetAllRecordFromExcel(String path)
        {
            List<EntityExcelRecord> list = new List<EntityExcelRecord>();
            ArrayList tableList = ExcelOledbHelper.GetExcelTables(@"E:\Code\模型设计.xls");
            foreach (var item in tableList)
            {
                DataTable table = ExcelOledbHelper.InputFromExcel(@"E:\Code\模型设计.xls", item.ToString());
                for (int i = 0; i < table.Rows.Count; i++)
                {
                    EntityExcelRecord record = new EntityExcelRecord();
                    record.ClassName = table.Rows[i]["类型名"] is DBNull ? String.Empty : table.Rows[i]["类型名"].ToString();
                    record.BaseClass = table.Rows[i]["基类"] is DBNull ? String.Empty : table.Rows[i]["基类"].ToString();
                    record.MappingClass = table.Rows[i]["映射类型"] is DBNull ? String.Empty : table.Rows[i]["映射类型"].ToString();
                    record.TableName = table.Rows[i]["所属表"] is DBNull ? String.Empty : table.Rows[i]["所属表"].ToString();
                    record.PropertyName = table.Rows[i]["字段名称"] is DBNull ? String.Empty : table.Rows[i]["字段名称"].ToString();
                    record.PropertyType = table.Rows[i]["属性类型"] is DBNull ? String.Empty : table.Rows[i]["属性类型"].ToString();
                    record.FieldType = table.Rows[i]["字段类型"] is DBNull ? String.Empty : table.Rows[i]["字段类型"].ToString();
                    record.MaxLength = table.Rows[i]["最大长度"] is DBNull ? null : (int?)Convert.ToInt16(table.Rows[i]["最大长度"]);
                    record.Decimal = table.Rows[i]["小数位数"] is DBNull ? null : (int?)Convert.ToInt16(table.Rows[i]["小数位数"]);
                    record.IsKey = table.Rows[i]["主键"] is DBNull ? false : true;
                    record.IsMultiPK = table.Rows[i]["复合主键"] is DBNull ? false : true;
                    record.IsNull = table.Rows[i]["允许空"] is DBNull ? false : true;
                    record.IsIdentity = table.Rows[i]["自增长"] is DBNull ? false : true;
                    record.DefaultValue = table.Rows[i]["默认值"] is DBNull ? String.Empty : table.Rows[i]["默认值"].ToString();
                    record.Description = table.Rows[i]["字段说明"] is DBNull ? String.Empty : table.Rows[i]["字段说明"].ToString();
                    record.RefrenceClassName = table.Rows[i]["参考类型"] is DBNull ? String.Empty : table.Rows[i]["参考类型"].ToString();
                    record.RefrencePropertyName = table.Rows[i]["参考属性"] is DBNull ? String.Empty : table.Rows[i]["参考属性"].ToString();
                    record.RefrenceRelation = table.Rows[i]["类型关系"] is DBNull ? String.Empty : table.Rows[i]["类型关系"].ToString();
                    list.Add(record);
                }
            }
            return list;
        }

        public void GenerateModett(List<EntityExcelRecord> list)
        {
            foreach (var node in list)
            {
                if (String.IsNullOrWhiteSpace(node.MappingClass)) continue;
                //code

                if (String.IsNullOrWhiteSpace(node.BaseClass))
                {
                    //code inherit from object
                }
                else
                {
                    //code inherit from node.BaseClass
                }

                foreach (var record in list)
                {

                    if (record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.MappingClass)
                        && String.IsNullOrWhiteSpace(record.RefrenceClassName))
                    {
                        //code primitive property

                    }
                    if (record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.MappingClass)
                        && !String.IsNullOrWhiteSpace(record.RefrenceClassName))
                    {
                        //code self-navigation property
                        if (String.IsNullOrWhiteSpace(record.RefrencePropertyName))
                        { }
                        else
                        { }
                    }
                }

                foreach (var record in list)
                {
                    if (!String.IsNullOrWhiteSpace(record.RefrenceClassName)
                        && !String.IsNullOrWhiteSpace(record.RefrencePropertyName)
                        && record.RefrenceClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.MappingClass))
                    {
                        //code navigation property
                        if (record.RefrenceRelation.Equals("*"))
                        { }
                        if (record.RefrenceRelation.Equals("N"))
                        { }
                        if (record.RefrenceRelation.Equals("1"))
                        { }
                        if (record.RefrenceRelation.Equals("0..1"))
                        { }
                    }
                }
            }
        }

        public void GenerateEntityModett(List<EntityExcelRecord> list)
        {
            //EntityConfiguration.tt
            foreach (var node in list)
            {
                if (String.IsNullOrWhiteSpace(node.MappingClass)) continue;
                //Table and Key
                int count = 0;
                foreach (var record in list)
                {
                    if (String.IsNullOrWhiteSpace(record.MappingClass)
                        && record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.BaseClass)
                        && !String.IsNullOrWhiteSpace(record.TableName)
                        && !record.IsMultiPK
                        && record.IsKey)
                    {

                    }
                }
                if (count > 0)
                { }

                //primitive field
                foreach (var record in list)
                {
                    if (String.IsNullOrWhiteSpace(record.MappingClass)
                        && record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.BaseClass)
                        && !String.IsNullOrWhiteSpace(record.FieldType)
                        && String.IsNullOrWhiteSpace(record.RefrencePropertyName)
                        && String.IsNullOrWhiteSpace(record.RefrenceClassName))
                    {
                        if (!String.IsNullOrWhiteSpace(record.TableName))
                        { }
                        else if (!String.IsNullOrWhiteSpace(node.MappingClass) && node.MappingClass.Equals("ComplexType"))
                        { }
                        else
                            continue;
                        //code
                        Console.WriteLine(record.PropertyName);
                        if (record.IsIdentity)
                        { }
                        if (record.Decimal > 0)
                        { }
                        if (!String.IsNullOrWhiteSpace(record.DefaultValue))
                        { }
                        if (!String.IsNullOrWhiteSpace(record.Description))
                        { }
                        if (!String.IsNullOrWhiteSpace(record.FieldType))
                        { }
                        if (record.MaxLength > 0)
                        { }
                        if (record.IsNull)
                        { }
                        else
                        { }
                        if (record.PropertyName.Equals(""))
                        { }
                    }
                }

                foreach (var record in list)
                {
                    if (String.IsNullOrWhiteSpace(record.BaseClass)
                        && record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.MappingClass)
                        && !String.IsNullOrWhiteSpace(record.RefrenceClassName))
                    {
                        //自连接
                        if (record.ClassName.Equals(record.RefrenceClassName))
                        {
                            continue;
                        }
                        if (String.IsNullOrWhiteSpace(record.RefrencePropertyName))
                        {
                            //code N-N
                        }
                        else
                        {
                            //code self 0/0...1
                            if (record.IsNull)
                            { }
                            else
                            { }

                            //code FK 
                            if (record.RefrenceRelation.Equals("*"))
                            { }
                            if (record.RefrenceRelation.Equals("N"))
                            { }
                            if (record.RefrenceRelation.Equals("1"))
                            { }
                            if (record.RefrenceRelation.Equals("0..1"))
                            { }

                        }
                    }
                }
                //自连接
            }
        }

        public void GenerateDataContexttt(List<EntityExcelRecord> list)
        {

//#region
/***********************************************
 * auto-generated code from T4
 * 
 * ********************************************/
//#endregion
//using ERPS.Models;
//using System;
//using System.Collections.Generic;
//using System.Data.Entity;
//using System.Linq;
//using System.Text;

//namespace EPRS.Repository
//{
//    public class DataContext:DbContext,IDisposable
//    {

//        public DataContext() : base("DataContext") { }

//        public DataContext(String connectionStrings) : base(connectionStrings) { }
             
            foreach (var record in list)
            {
                if (record.MappingClass.Equals("EntityType"))
                {
        //public DbSet<<#=record.ClassName #> <#=record.ClassName #> { get; set; }
                }
            }
        //protected override void OnModelCreating(DbModelBuilder modelBuilder)
        //{
            foreach (var record in list)
            {
                if (record.MappingClass.Equals("EntityType")
                    || record.MappingClass.Equals("ComplexType"))
                {
            //modelBuilder.Configurations.Add(new <#=record.ClassName #>Configuration());
                }
            }
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        //}
        //public static void InitDataBase()
        //{
            //Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        //}
    //}
//}
        }

        public void GenerateRepositorytt(List<EntityExcelRecord> list)
        { }
    }
}
