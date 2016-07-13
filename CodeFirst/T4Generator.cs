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
    public class T4Generator
    {

        #region Excel
        /// <summary>  
        /// 获取Excel文件数据表列表  
        /// </summary>  
        public ArrayList GetExcelTables(string ExcelFileName)
        {
            DataTable dt = new DataTable();
            ArrayList TablesList = new ArrayList();
            if (File.Exists(ExcelFileName))
            {
                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
                {
                    try
                    {
                        conn.Open();
                        dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Tables, new object[] { null, null, null, "TABLE" });
                    }
                    catch (Exception exp)
                    {
                        throw exp;
                    }

                    //获取数据表个数  
                    int tablecount = dt.Rows.Count;
                    for (int i = 0; i < tablecount; i++)
                    {
                        string tablename = dt.Rows[i][2].ToString().Trim().TrimEnd('$');
                        if (TablesList.IndexOf(tablename) < 0)
                        {
                            TablesList.Add(tablename);
                        }
                    }
                }
            }
            return TablesList;
        }

        /// <summary>  
        /// 将Excel文件导出至DataTable(第一行作为表头)  
        /// </summary>  
        /// <param name="ExcelFilePath">Excel文件路径</param>  
        /// <param name="TableName">数据表名，如果数据表名错误，默认为第一个数据表名</param>  
        public DataTable InputFromExcel(string ExcelFilePath, string TableName)
        {
            if (!File.Exists(ExcelFilePath))
            {
                throw new Exception("Excel文件不存在！");
            }

            //如果数据表名不存在，则数据表名为Excel文件的第一个数据表  
            ArrayList TableList = new ArrayList();
            TableList = GetExcelTables(ExcelFilePath);

            if (TableList.IndexOf(TableName) < 0)
            {
                TableName = TableList[0].ToString().Trim();
            }

            DataTable table = new DataTable();
            OleDbConnection dbcon = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + ExcelFilePath + ";Extended Properties=Excel 8.0");
            OleDbCommand cmd = new OleDbCommand("select * from [" + TableName + "$]", dbcon);
            OleDbDataAdapter adapter = new OleDbDataAdapter(cmd);

            try
            {
                if (dbcon.State == ConnectionState.Closed)
                {
                    dbcon.Open();
                }
                adapter.Fill(table);
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (dbcon.State == ConnectionState.Open)
                {
                    dbcon.Close();
                }
            }
            return table;
        }

        /// <summary>  
        /// 获取Excel文件指定数据表的数据列表  
        /// </summary>  
        /// <param name="ExcelFileName">Excel文件名</param>  
        /// <param name="TableName">数据表名</param>  
        public ArrayList GetExcelTableColumns(string ExcelFileName, string TableName)
        {
            DataTable dt = new DataTable();
            ArrayList ColsList = new ArrayList();
            if (File.Exists(ExcelFileName))
            {
                using (OleDbConnection conn = new OleDbConnection("Provider=Microsoft.Jet.OLEDB.4.0;Extended Properties=Excel 8.0;Data Source=" + ExcelFileName))
                {
                    conn.Open();
                    dt = conn.GetOleDbSchemaTable(OleDbSchemaGuid.Columns, new object[] { null, null, TableName, null });

                    //获取列个数  
                    int colcount = dt.Rows.Count;
                    for (int i = 0; i < colcount; i++)
                    {
                        string colname = dt.Rows[i]["Column_Name"].ToString().Trim();
                        ColsList.Add(colname);
                    }
                }
            }
            return ColsList;
        }
        #endregion

        #region ExcelToClass

        public List<PropertyRecord> GetProperty(String path)
        {
            List<PropertyRecord> list = new List<PropertyRecord>();
            DataTable dt = InputFromExcel(path, "Primitive");
            if (dt == null || dt.Rows.Count <= 0) return list;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                PropertyRecord record = new PropertyRecord();
                record.ClassBase = dt.Rows[i]["基类"] is DBNull ? String.Empty : dt.Rows[i]["基类"].ToString();
                record.ClassMapping = dt.Rows[i]["数据模型"] is DBNull ? String.Empty : dt.Rows[i]["数据模型"].ToString();
                record.ClassName = dt.Rows[i]["类型名称"] is DBNull ? String.Empty : dt.Rows[i]["类型名称"].ToString();
                record.Description = dt.Rows[i]["字段说明"] is DBNull ? String.Empty : dt.Rows[i]["字段说明"].ToString();
                record.FieldType = dt.Rows[i]["字段类型"] is DBNull ? String.Empty : dt.Rows[i]["字段类型"].ToString();
                record.IsIdentity = dt.Rows[i]["是否自增"] is DBNull ? false : true;
                record.IsKey = dt.Rows[i]["主键"] is DBNull ? false : true;
                record.IsMultiKey = dt.Rows[i]["复合主键"] is DBNull ? false : true;
                record.IsNull = dt.Rows[i]["是否为空"] is DBNull ? false : true;
                record.MaxLength = dt.Rows[i]["最大长度"] is DBNull ? 0 : Convert.ToInt32(dt.Rows[i][""]);
                record.pricison = dt.Rows[i]["精度"] is DBNull ? 0 : Convert.ToInt32(dt.Rows[i]["精度"]);
                record.PropertyName = dt.Rows[i]["属性名称"] is DBNull ? String.Empty : dt.Rows[i]["属性名称"].ToString();
                record.PropertyType = dt.Rows[i]["属性类型"] is DBNull ? String.Empty : dt.Rows[i]["属性类型"].ToString();
                record.TableName = dt.Rows[i]["数据表名"] is DBNull ? String.Empty : dt.Rows[i]["数据表名"].ToString();
                list.Add(record);
            }
            return list;
        }

        public List<NavigationRecord> GetNavigation(String path)
        {
            List<NavigationRecord> list = new List<NavigationRecord>();
            DataTable dt = InputFromExcel(path, "Navigtaion");
            if (dt == null || dt.Rows.Count <= 0) return list;
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                NavigationRecord record = new NavigationRecord();
                record.FKClassName = dt.Rows[i]["外键类型"] is DBNull ? String.Empty : dt.Rows[i]["外键类型"].ToString();
                record.FKPropertyName = dt.Rows[i]["外键属性"] is DBNull ? String.Empty : dt.Rows[i]["外键属性"].ToString();
                record.MainClassName = dt.Rows[i]["主键类型"] is DBNull ? String.Empty : dt.Rows[i]["主键类型"].ToString();
                record.MainClassProperty = dt.Rows[i]["主键属性"] is DBNull ? String.Empty : dt.Rows[i]["主键属性"].ToString();
                record.RelationShip = dt.Rows[i]["关系"] is DBNull ? String.Empty : dt.Rows[i]["关系"].ToString();
                record.Description = dt.Rows[i]["说明"] is DBNull ? String.Empty : dt.Rows[i]["说明"].ToString();
                list.Add(record);
            }
            return list;
        }

        public List<ServiceRecord> GetService(String path)
        {
            return null;
        }

        #endregion

        public void GenerateModel(List<PropertyRecord> properties,List<NavigationRecord> navigations)
        {
            foreach (var node in properties)
            {
                if (String.IsNullOrWhiteSpace(node.ClassMapping)) continue;
                //manager.StartNewFile(node.ClassName+".cs"); 
/*
#>/***********************************************
* auto-generated code from T4
* 
* ********************************************/

/*
using Infrastructure;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;

namespace ERPS.Models
{
<#
*/
                if (node.ClassMapping.Equals("Enum"))
                {
/*
#>
    /// <summary>
    /// <#=String.IsNullOrWhiteSpace(node.Description)?"":node.Description #>枚举
    /// </summary>
    public enum <#=node.ClassName #>
    {
<#
*/
                    int count = 0;
                    foreach (var record in properties)
                    {
                        if (String.IsNullOrWhiteSpace(record.ClassMapping) && record.ClassName.Equals(node.ClassName))
                        {
                            if (count != 0)
                            {
                                
/*
#>,
<#
*/
                            }
/*
#>
        [Description("<#=String.IsNullOrWhiteSpace(record.Description)?"":record.Description #>")]
        <#=record.PropertyName #><#  
*/
                            count++;
                        }
                    }
/*
#>

    }
}
<#
                    manager.EndBlock();
*/
                    continue;
                }
                //根据实体的基类写入表头
                if (String.IsNullOrWhiteSpace(node.ClassBase))
                {
/*
#>
    /// <summary>
    /// <#=node.ClassName #> 实体类
    /// </summary>
    [Serializable]
    public partial class <#=node.ClassName #>:EntityBase,IAggregateRoot
    {
<#
*/
                }
                else
                {
                    //code inherit from node.BaseClass
/*
#>
    /// <summary>
    /// <#=node.ClassName #>表实体类
    /// </summary>
    [Serializable]
    public partial class <#=node.ClassName #>:<#=node.ClassBase #>,IAggregateRoot
    {
<#
*/

                }

                //循环写入类型属性
                foreach (var record in properties)
                {
                    //具有相同的类型名称、类型的映射基类为空的记录为类型的基本属性
                    if (record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.ClassMapping))
                    {
/*
#>
        /// <summary>
        /// <#=String.IsNullOrWhiteSpace(record.Description)?"":record.Description #>
        /// </summary>
        public <#=record.PropertyType #> <#=record.PropertyName #> {get;set;}

<#
*/

                    }
                }

                foreach (var record in navigations)
                {
                    if (record.MainClassName != null && record.MainClassName.Equals(node.ClassName))
                    {
/*
#>
        ///<summary>
        ///<#=String.IsNullOrWhiteSpace(record.Description)?"":record.Description #>
        ///</summary>
        public virtual IList<<#=record.FKClassName #>> <#=record.MainClassProperty #>{get;set;}

<#
*/
                    }

                    if (record.FKClassName != null && record.FKClassName.Equals(node.ClassName))
                    {
                        if (record.RelationShip.Equals("*"))
                        {
/*
#>
        ///<summary>
        ///<#=String.IsNullOrWhiteSpace(record.Description)?"":record.Description #>
        ///</summary>
        public virtual IList<<#=record.MainClassName #>> <#=record.FKPropertyName #>{get;set;}

<#
*/
                        }
                        else
                        {
/*
#>
        ///<summary>
        ///<#=String.IsNullOrWhiteSpace(record.Description)?"":record.Description #>
        ///</summary>
        public virtual <#=record.MainClassName #> <#=record.FKPropertyName #>{get;set;}

<#
*/
                        }
                    }
                }
/*
#>
        protected override void Validate()
        {
            throw new NotImplementedException();
        }
    }
}
<#
*/
//manager.EndBlock();
            }
            //manager.Process(true);
        }

        public void GenerateDataContext(List<PropertyRecord> properties)
        {
 
        }

        public void GenerateConfiguration(List<PropertyRecord> properties, List<NavigationRecord> navigations)
        {
            foreach (var node in properties)
            {
                if (String.IsNullOrWhiteSpace(node.ClassMapping)||node.ClassMapping.Equals("Enum")) continue;
                //manager.StartNewFile(node.ClassName+".cs");
/*
#>/***********************************************
* auto-generated code from T4
* 
* ********************************************/

/*
using ERPS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace EPRS.Repository
{
    ///<summary>
    ///<#=node.ClassName #> 实体类映射
    ///</summary>
    public class <#=node.ClassName #>Configuration:<#=node.ClassMapping #>Configuration<<#=node.ClassName #>>
    {
        public <#=node.ClassName #>Configuration()
        {
<#
*/
                int count = 0;
                foreach (var record in properties)
                {
                    if (String.IsNullOrWhiteSpace(record.ClassMapping)
                        &&!String.IsNullOrWhiteSpace(record.TableName)
                        && record.ClassName.Equals(node.ClassName))
                    {
                        if (record.IsMultiKey&&record.IsKey)
                        {
                            if (count <= 0)
                            {
/*
#>, k.<#=record.PropertyName #><#
*/
                            }
                            else
                            {
/*
#>
            ToTable("<#=record.TableName #>");
            HasKey(k => new { k.<#=record.PropertyName #><#
*/
                            }
                            count++;
                        }
                        if (!record.IsMultiKey&&record.IsKey)
                        {
/*
#>
            ToTable("<#=record.TableName #>");
            HasKey(e=>e.<#=record.PropertyName #>);
<#
*/
                        }
                    }
                }
                if (count > 0)
                {
/*
#> });
<#
*/
                }

                foreach (var record in properties)
                {
                    if (String.IsNullOrWhiteSpace(record.ClassMapping)
                        && record.ClassName.Equals(node.ClassName)
                        )
                    {
/*
#>
            Property(e =>e.<#=record.PropertyName #>).HasColumnName("<#=record.PropertyName #>")<#
*/
                        if (record.IsIdentity)
                        {
/*
#>.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)<#
*/
                        }
                        if (record.pricison > 0)
                        { }
                        if (!String.IsNullOrWhiteSpace(record.Description))
                        { }
                        if (record.MaxLength > 0)
                        {
                            /*
                            #>.HasMaxLength(<#=record.MaxLength #>)<#
                             */
                        }
                        if (!String.IsNullOrWhiteSpace(record.FieldType))
                        {
                            /*
                            #>.HasColumnType("<#=record.FieldType #>")<#
                             */
                        }
                        if (record.IsNull)
                        {
                            /*
                            #>.IsOptional()<# 
                             */
                        }
                        else
                        {
                            /*
                            #>.IsRequired()<#
                             */
                        }
                        //特殊属性byte[] RowVersion
                        if (record.PropertyName.Equals("RowVersion"))
                        {
                            /*
                            #>.IsRowVersion()<# 
                             */
                        }
                        /*
                        #>;
<#
                         */
                    }
                }

                foreach (var record in navigations)
                {
                    if (record.FKClassName.Equals(node.ClassName))
                    {
                        if (record.RelationShip.Equals("o"))
                        {
/*
#>
            HasOptional(e=>e.<#=record.FKPropertyName #>).WithMany(e=>e.<#=record.MainClassProperty #>).Map(e=>e.MapKey("<#=record.FKPropertyName #>Id"));<#
    */
                        }
                        if (record.RelationShip.Equals("l"))
                        {
/*
#>
            HasRequired(e=>e.<#=record.FKPropertyName #>).WithMany(e=>e.<#=record.MainClassProperty #>).Map(e=>e.MapKey("<#=record.FKPropertyName #>Id"));<#
    */
                        }
                        if (record.RelationShip.Equals("*"))
                        {
/*
#>
            HasMany(e => e.<#=record.FKPropertyName #>s).WithMany(e=>e.<#=record.MainClassProperty #>);<#
*/
                        }
                    }
                }
/*
#>

        }
    }
}
<#
    */
                //manager.EndBlock();
            }
            //manager.Process(true);
        }

        public void GenerateDTO(List<PropertyRecord> properties, List<NavigationRecord> navigations)
        { }

        public void GenerateIRepository(List<PropertyRecord> properties)
        { }

        public void GenerateRepository(List<PropertyRecord> properties, List<NavigationRecord> navigations)
        { }

        public void GenerateIService(List<PropertyRecord> properties, List<NavigationRecord> navigations)
        { }
    }

    class PropertyRecord
    {
        public String ClassName { get; set; }
        public String ClassBase { get; set; }
        public String ClassMapping { get; set; }
        public String PropertyName { get; set; }
        public String PropertyType { get; set; }
        public String TableName { get; set; }
        public String FieldType { get; set; }
        public bool IsKey { get; set; }
        public bool IsNull { get; set; }
        public bool IsIdentity { get; set; }
        public bool IsMultiKey { get; set; }
        public int MaxLength{get;set;}
        public int pricison { get; set; }
        public String Description { get; set; }
    }

    class NavigationRecord
    {
        public String MainClassName { get; set; }
        public String MainClassProperty { get; set; }
        public String FKClassName { get; set; }
        public String FKPropertyName { get; set; }
        public String RelationShip { get; set; }
        public String Description { get; set; }
    }

    class ServiceRecord
    { }
}
