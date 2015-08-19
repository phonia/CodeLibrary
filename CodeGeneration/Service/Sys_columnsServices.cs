using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodeGeneration.Service
{
    public class Sys_columnsServices:BaseServices
    {
        public GetSys_columnsResponse GetSys_columnsOfTable(GetSys_columnsRequest request)
        {
            GetSys_columnsResponse response = new GetSys_columnsResponse();
            try
            {
                List<Model.Sys_columns> Sys_columnsList = (_manager.GetUnitOfWorkRepository(typeof(Repository.Sys_columnsRepository)) as Repository.Sys_columnsRepository)
                    .Get("select * from sys.columns t where t.object_id=object_id('"+request.TableName+"') ", null);
                Model.Sys_columns primary = m_dataAcess.Get<Model.Sys_columns>("select tt.*"
                                                                                +"from sysindexes i "
                                                                                +"join sysindexkeys k on i.id = k.id and i.indid = k.indid  "
                                                                                +"join sysobjects o on i.id = o.id  "
                                                                                +"join syscolumns c on i.id=c.id and k.colid = c.colid  "
                                                                                +"join systypes t on c.xusertype=t.xusertype "
                                                                                +"join sys.columns tt on tt.name=c.name and tt.object_id=object_id('Role') "
                                                                                +"where o.xtype = 'U' and o.name='User' "
                                                                                +"and exists(select 1 from sysobjects where xtype = 'PK' and parent_obj=i.id "
                                                                                +" and name =    i.name) "
                                                                                +" order by o.name,k.colid ", null).FirstOrDefault();
                if (primary != null)
                {
                    Sys_columnsList.Where(it => it.name == primary.name).FirstOrDefault().isPrimaryKey = true;
                }
                else
                    throw new Exception("there is no primary key in table "+request.TableName);

                response.Sys_columnsViewList = ModeViewMap.MapTo<Model.Sys_columns, View.Sys_ColumnsView>(Sys_columnsList);

                List<Model.Sys_TRelation> sys_trelationList = (_manager.GetUnitOfWorkRepository(typeof(Repository.Sys_TrelationRepository)) as Repository.Sys_TrelationRepository)
                    .Get("select mainname=(select name from sys.objects mo where mo.object_id=s.fkeyid) "
                        +",maincolumns=(select name from sys.columns mc where mc.object_id=s.fkeyid and mc.column_id=fkey) "
                        +",refname=(select name from sys.objects ro where ro.object_id=s.rkeyid) "
                        +",refcolumns=(select name from sys.columns rc where rc.column_id=rkey and rc.object_id=s.rkeyid) "
                        +"from sysforeignkeys s "
                        +"where s.fkeyid=OBJECT_ID('"+request.TableName+"') or s.rkeyid=OBJECT_ID('"+request.TableName+"')", null);
                response.sys_trelationViewList = ModeViewMap.MapTo<Model.Sys_TRelation, View.Sys_TrelationView>(sys_trelationList);

                response.IsSuccesss = true;
                response.Message = "success";
            }
            catch (Exception ex)
            {
                response.IsSuccesss = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }

    public class GetSys_columnsRequest : BaseRequest
    {
        public string TableName { get; set; }
        public int Object_id { get; set; }
    }

    public class GetSys_columnsResponse : BaseResponse
    {
        public List<View.Sys_ColumnsView> Sys_columnsViewList { get; set; }
        public List<View.Sys_TrelationView> sys_trelationViewList { get; set; }
    }
}
