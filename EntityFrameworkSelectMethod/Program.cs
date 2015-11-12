using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;

namespace EntityFrameworkSelectMethod
{
    class Program
    {
        static void Main(string[] args)
        {
            //DataContext data = new DataContext();
            //IQueryable<ProductInfo> products = null;
            List<nnnn> list = new List<nnnn>();
            ////List<nnnn> list= products.Select(it => new nnnn() { Id=it.Id,Name=it.Name,me=it.MessageInfoes}).ToList();
            //var list = products.Select(it => new { Id = it.Id, Name = it.Name, me = it.MessageInfoes }).ToList();
            System.Guid id = System.Guid.Parse("FF984CF7-844C-432F-B0A4-889D152BB6C1");
            ProductInfo ppt = new ProductInfo() { Id = System.Guid.NewGuid(), Name = "hy" };
            for (int i = 0; i < 10; i++)
            {
                MessageInfo me = new MessageInfo() { Id = System.Guid.NewGuid(), Message = "hy" };
                me.ProductId = ppt.Id;
                if (ppt.MessageInfoes == null) ppt.MessageInfoes = new List<MessageInfo>();
                ppt.MessageInfoes.Add(me);
            }

            ProductInfo save = null;
            using (var data = new DataContext())
            {
                //data.ProductInfoes.Add(ppt);
                //data.SaveChanges();
                save = (from b in data.ProductInfoes.Include("MessageInfoes") select b).FirstOrDefault();
               // var dddd = data.ProductInfoes.Select(it => new ProductInfo() {Id=it.Id,MessageInfoes=it.MessageInfoes.ToList() }).ToList();
            }
            (save.MessageInfoes.FirstOrDefault() as MessageInfo).Message = "hychange";
            using (var data = new DataContext())
            {
                data.Entry(save).State = EntityState.Modified;
                //data.ProductInfoes.Remove(save);
                data.SaveChanges();
            }


            Console.Read();
        }
    }

    class nnnn
    {
        public System.Guid Id { get; set; }
        public string Name { get; set; }
        public List<MessageInfo> Me { get; set; }
    }
}
