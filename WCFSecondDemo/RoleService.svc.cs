using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Utilities;
using Newtonsoft.Json.Linq;

namespace WCFSecondDemo
{
    // 注意: 使用“重构”菜单上的“重命名”命令，可以同时更改代码、svc 和配置文件中的类名“RoleService”。
    // 注意: 为了启动 WCF 测试客户端以测试此服务，请在解决方案资源管理器中选择 RoleService.svc 或 RoleService.svc.cs，然后开始调试。
    //[ServiceBehavior(InstanceContextMode=InstanceContextMode.PerSession)]
    public class RoleService : IRoleService,IDisposable
    {
        public string Get()
        {
            _times++;
            return JsonConvert.SerializeObject(new Role() { Id = "1011", Name = "hy" });
        }

        public List<Role> GetAll()
        {
            _times++;
            List<Role> list = new List<Role>();
            list.Add(new Role() { Id="1011",Name="hy"});
            list.Add(new Role() { Id = "1012", Name = "hy" });
            list.Add(new Role() { Id = "1013", Name = "hy" });
            return list;
        }


        public string AddString(string id, string name)
        {
            _times++;
            return JsonConvert.SerializeObject(new Role() { Id = id, Name = name });
        }

        public string AddJson(string json)
        {
            _times++;
            JArray jarray = JsonConvert.DeserializeObject(json) as JArray;
            List<Role> list = new List<Role>();

            if (jarray != null)
            {
                for (int i = 0; i < jarray.Count; i++)
                {
                    JObject item = jarray[i] as JObject;
                    Role role = new Role();
                    role.Id = item.Property("Id").Value.ToString();
                    role.Name = item.Property("Name").Value.ToString();
                    list.Add(role);
                }
            }
            return JsonConvert.SerializeObject(list);
        }

        private static int _times = 0;

        public string GetTimes()
        {
            return JsonConvert.SerializeObject(_times);
        }

        public void Dispose()
        {
            
        }
    }

    [DataContract]
    public class Role
    {
        [DataMember]
        public string Id { get; set; }
        [DataMember]
        public string Name { get; set; }
    }
}
