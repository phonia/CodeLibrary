using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Infrastructure.DataAcess;
using Infrastructure.Domain;
using Infrastructure.UnitOfWork;

namespace CodeGeneration.Service
{
    public class Sys_objectsService:BaseServices
    {
        public GetAllSys_objectsResponse GetAllSys_objects()
        {
            GetAllSys_objectsResponse response=new GetAllSys_objectsResponse();
            try
            {
                List<Model.Sys_objects> sys_objectList = (_manager.GetUnitOfWorkRepository(typeof(Repository.Sys_objectsRepository)) as Repository.Sys_objectsRepository)
                    .Get("SELECT * FROM sys.objects where type='u'", null);
                response.Sys_ojbectsList = ModeViewMap.MapTo<Model.Sys_objects, View.Sys_objectsView>(sys_objectList);
                response.Message = "success";
                response.IsSuccesss = true;
            }
            catch (Exception ex)
            {
                response.IsSuccesss = false;
                response.Message = ex.Message;
            }
            return response;
        }
    }

    public class GetAllSys_objectsResponse : BaseResponse
    {
        public List<View.Sys_objectsView> Sys_ojbectsList { get; set; }
    }
}
