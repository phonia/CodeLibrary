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

        /// <summary>  
        /// 获取Excel文件数据表列表  
        /// </summary>  
        public  ArrayList GetExcelTables(string ExcelFileName)
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
        public  DataTable InputFromExcel(string ExcelFilePath, string TableName)
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
        public  ArrayList GetExcelTableColumns(string ExcelFileName, string TableName)
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

        public  List<EntityExcelRecord> GetAllRecordFromExcel(String path)
        {
            List<EntityExcelRecord> list = new List<EntityExcelRecord>();
            ArrayList tableList = GetExcelTables(path);
            foreach (var item in tableList)
            {
                DataTable table = InputFromExcel(path, item.ToString());
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
            //循环查找所有数据类型
            foreach (var node in list)
            {
                //排除所有的非类型数据记录
                if (String.IsNullOrWhiteSpace(node.MappingClass)) continue;
                //开始写.cs文件
//              manager.StartNewFile(node.ClassName+".cs"); 

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
                //根据实体的基类写入表头
                if (String.IsNullOrWhiteSpace(node.BaseClass))
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
    public partial class <#=node.ClassName #>:<#=node.BaseClass #>,IAggregateRoot
    {
<#
*/

                }

                //循环写入类型属性
                foreach (var record in list)
                {
                    //具有相同的类型名称、类型的映射基类和类型的参考类型为空的记录为类型的基本属性
                    if (record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.MappingClass)
                        && String.IsNullOrWhiteSpace(record.RefrenceClassName))
                    {
/*
#>
        /// <summary>
        /// <#=record.Description??"" #>
        /// </summary>
        public <#=record.PropertyType #> <#=record.PropertyName #> {get;set;}

<#
 */

                    }
                    //具有相同的类型名称、类型的映射基类为空、类型的参考类型不为空的记录为类型的导航属性
                    if (record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.MappingClass)
                        && !String.IsNullOrWhiteSpace(record.RefrenceClassName))
                    {
                        //code self-navigation property
                        //类型的参考属性为空的记录为类型的泛型导航属性；否则为普通导航属性
                        if (String.IsNullOrWhiteSpace(record.RefrencePropertyName))
                        {
/*
#>
        ///<summary>
        ///<#=record.Description #>
        ///</summary>
        public virtual IList<<#=record.PropertyType #>> <#=record.PropertyName #>s{get;set;}

<#
 */
                        }
                        else
                        {
/*
#>
        /// <summary>
        /// <#=record.Description??"" #>
        /// </summary>
        public <#=record.PropertyType #> <#=record.PropertyName #> {get;set;}

<#
 */
                        }
                    }
                }

                foreach (var record in list)
                {
                    //参考类型不为空、参考属性不为空、类型的参考类型名与查找类型名相同、映射基类为空的记录为类型的导航属性
                    if (!String.IsNullOrWhiteSpace(record.RefrenceClassName)
                        && !String.IsNullOrWhiteSpace(record.RefrencePropertyName)
                        && record.RefrenceClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.MappingClass))
                    {
                        //code navigation property
                        if (record.RefrenceRelation.Equals("*"))
                        {
/*
#>
        ///<summary>
        ///<#=record.Description #>
        ///</summary>
        public virtual IList<<#=record.ClassName #>> <#=record.ClassName #>s{get;set;}

<#
 */
                        }
                        if (record.RefrenceRelation.Equals("N"))
                        {
/*
#>
        ///<summary>
        ///<#=record.Description #>
        ///</summary>
        public virtual IList<<#=record.ClassName #>> <#=record.ClassName #>s{get;set;}

<#
 */
                        }
                        if (record.RefrenceRelation.Equals("1"))
                        { }
                        if (record.RefrenceRelation.Equals("0..1"))
                        { }
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

        public void GenerateEntityModett(List<EntityExcelRecord> list)
        {
            //EntityConfiguration.tt
            foreach (var node in list)
            {
                if (String.IsNullOrWhiteSpace(node.MappingClass)) continue;
/*
				manager.StartNewFile(node.ClassName+".cs");
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
	public class <#=node.ClassName #>Configuration:<#=node.MappingClass #>Configuration<<#=node.ClassName #>>
    {
		public <#=node.ClassName #>Configuration()
        {
<#
 */
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
/*
#>
			ToTable("<#=record.TableName #>");
			HasKey(e=>e.<#=record.PropertyName #>);
<#
 */
                    }
					if (String.IsNullOrWhiteSpace(record.MappingClass)
                        && record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.BaseClass)
						&&!String.IsNullOrWhiteSpace(record.TableName)
						&&record.IsMultiPK
						&&record.IsKey)
						{
							if(count==0)
							{
								count++;
/*
#>
			ToTable("<#=record.TableName #>");
			HasKey(k => new { k.<#=record.PropertyName #><#
 */
							}
							else
							{
/*
#>, k.<#=record.PropertyName #><#
 */
							}
						}
                }
				if(count>0)
				{
/*
#> });<#
 */
				}

                //primitive field
                foreach (var record in list)
                {
                    if (String.IsNullOrWhiteSpace(record.MappingClass)
                        && record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.BaseClass)
						&&!String.IsNullOrWhiteSpace(record.FieldType)
                        &&String.IsNullOrWhiteSpace(record.RefrencePropertyName)
                        &&String.IsNullOrWhiteSpace(record.RefrenceClassName))
                    {
                        //code
                        //区分EntiyType与ComplexType
						if (!String.IsNullOrWhiteSpace(record.TableName))
                        {
/*
#>
			Property(e =>e.<#=record.PropertyName #>).HasColumnName("<#=record.PropertyName #>")<#
 */
						}
                        else if (!String.IsNullOrWhiteSpace(node.MappingClass) && node.MappingClass.Equals("ComplexType"))
                        {
/*
#>
			Property(e =>e.<#=record.PropertyName #>)<#
 */
						}
                        else
                            continue;

                        if (record.IsIdentity)
                        { 
							if(record.PropertyType.Equals("DateTime"))
							{

							}
							else
							{
/*
#>.HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity)<#
 */
							}
						}
                        if (record.Decimal > 0)
                        { }
                        if (!String.IsNullOrWhiteSpace(record.DefaultValue))
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

                foreach (var record in list)
                {
                    if (String.IsNullOrWhiteSpace(record.BaseClass)
						&& record.ClassName.Equals(node.ClassName)
                        && String.IsNullOrWhiteSpace(record.MappingClass)
                        && !String.IsNullOrWhiteSpace(record.RefrenceClassName))
                    {
					    //表自连接
                        if (record.ClassName.Equals(record.RefrenceClassName))
                        {
/*
#>
			HasOptional(e => e.<#=record.PropertyName #>).WithMany(e => e.<#=record.ClassName #>s);
<#
 */
                            continue;
                        }
                        //code N-N
                        if (String.IsNullOrWhiteSpace(record.RefrencePropertyName))
                        {
/*
#>
			HasMany(e => e.<#=record.RefrenceClassName #>s).WithMany(e => e.<#=record.ClassName #>s);
<#
 */
                        }
                        else
                        {
                            //code self 0/0...1
                            if (record.IsNull)
                            {
/*
#>
            HasOptional(e=>e.<#=record.RefrenceClassName #>)<#
 */
							}
                            else
                            {
/*
#>
            HasRequired(e=>e.<#=record.RefrenceClassName #>)<#
 */
							}

                            //code FK 
                            if (record.RefrenceRelation.Equals("*"))
                            {
/*
#>.WithMany(e=>e.<#=record.ClassName #>s);
<#
 */
							}
                            if (record.RefrenceRelation.Equals("N"))
                            {
/*
#>.WithMany(e=>e.<#=record.ClassName #>s);
<#
 */
							}
                            if (record.RefrenceRelation.Equals("1"))
                            {
/*
#>.WithRequiredDependent(e=>e.<#=record.ClassName #>);
<#
 */
							}
                            if (record.RefrenceRelation.Equals("0..1"))
                            {
/*
#>.WithOptional(e=>e.<#=record.ClassName #>);
<#
 */
							 }

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

        public void GenerateDataContexttt(List<EntityExcelRecord> list)
        {
/*
 #>/***********************************************
 * auto-generated code from T4
 * 
 * ********************************************/



/*
using ERPS.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EPRS.Repository
{
    public class DataContext:DbContext,IDisposable
    {

        public DataContext() : base("DataContext") { }

        public DataContext(String connectionStrings) : base(connectionStrings) { }
<#
 */
             
            foreach (var record in list)
            {
                if (record.MappingClass.Equals("EntityType"))
                {
/*
#>
        ///<summary>
        ///<#=record.PropertyName #>
        ///</summary>
        public DbSet<<#=record.ClassName #>> <#=record.ClassName #>Sets { get; set; }

<#
 */
                }
            }
/*
#>
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
<#
 */
            foreach (var record in list)
            {
                if (record.MappingClass.Equals("EntityType")
                    || record.MappingClass.Equals("ComplexType"))
                {
/*
#>
            modelBuilder.Configurations.Add(new <#=record.ClassName #>Configuration());
<#
 */
                }
            }
/*
#>
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }

        public static void InitDataBase()
        {
            Database.SetInitializer(new DropCreateDatabaseIfModelChanges<DataContext>());
        }
    }
}
<#
 */
        }

        public void GenerateRepositorytt(List<EntityExcelRecord> list)
        {
            //var manager = Manager.Create(Host, GenerationEnvironment); 
            foreach (var record in list)
            {
                if (String.IsNullOrWhiteSpace(record.MappingClass)) continue;
                EntityExcelRecord key = null;
                foreach (var node in list)
                {
                    if (node.ClassName.Equals(record.ClassName) && node.IsKey)
                        key = node;
                }
                if (key == null) continue;
                //manager.StartNewFile(key.ClassName + ".cs"); 
/*
#>/*******************************************
* auto-generated code from T4
* 
* ********************************************/

/*
using ERPS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace EPRS.Repository
{
    ///<summary>
    ///<#=record.ClassName #> 仓储类
    ///</summary>
    public partial class <#=record.ClassName #>Repository:EFRepository<<#=record.ClassName #>,<#=key.PropertyType #>>,I<#=record.ClassName #>Repository
    {
        public override void RemoveCascaded(<#=record.ClassName #> entity)
        {
            
        }

        public override void RemoveCascaded(<#=key.PropertyType #> id)
        {
            
        }
    }
}
<#
*/
                //manager.EndBlock();
            }
            //manager.Process(true);
        }

        public void GenerateIRepositorytt(List<EntityExcelRecord> list)
        {
            //var manager = Manager.Create(Host, GenerationEnvironment); 
            foreach (var record in list)
            {
                if (String.IsNullOrWhiteSpace(record.MappingClass)) continue;
                EntityExcelRecord key = null;
                foreach (var node in list)
                {
                    if (node.ClassName.Equals(record.ClassName) && node.IsKey)
                        key = node;
                }
                if (key == null) continue;
                //manager.StartNewFile(record.ClassName + ".cs"); 
/*
#>/*********************************************
* auto-generated code from T4
* 
* ********************************************/

/*
using Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace ERPS.Models
{
    ///<summary>
    ///<#=record.ClassName #> 仓储接口
    ///</summary>
    public interface I<#=record.ClassName #>Repository : IRepository<<#=key.ClassName #>,<#=key.PropertyType #>> 
    {

    }
}
<#
*/
                //manager.EndBlock();
            }
            //manager.Process(true);
        }

        public void GenerateModelDTOtt(List<EntityExcelRecord> list)
        { }

        public void GenerateModelMapDTO(List<EntityExcelRecord> list)
        { }
    }
}
