using Mvc3Demo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc3Demo.BLL
{
    public class ProductTypeBLL
    {

        public IList<ProductTypeInfo> GetProductTypesList()
        {
            using (MyDbContext db = new MyDbContext())
            {

                var items = db.ProductType.ToList();
                IList<ProductTypeInfo> ProductTypeInfos = new List<ProductTypeInfo>();
                foreach (var item in items)
                {
                    ProductTypeInfo info = new ProductTypeInfo();
                    info.ID = item.Id;
                    info.ProductTypeName = item.Name;
                    info.Description = item.Description;

                    ProductTypeInfos.Add(info);
                }

                return ProductTypeInfos;

            }
        }


        public IList<ProductTypeInfo> GetProductTypesList(int pageIndex, int pageSize, out int Total)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Total = (from c in db.ProductType
                         orderby c.Id
                         select c).Count();
                var items = (from c in db.ProductType
                             orderby c.Id
                             select c).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                IList<ProductTypeInfo> ProductTypeInfos = new List<ProductTypeInfo>();
                foreach (var item in items)
                {
                    ProductTypeInfo info = new ProductTypeInfo();
                    info.ID = item.Id;
                    info.ProductTypeName = item.Name;
                    info.Description = item.Description;

                    ProductTypeInfos.Add(info);
                }

                return ProductTypeInfos;
            }
        }

        public IList<ProductTypeInfo> GetProductTypesBySerach(int pageIndex, int pageSize, out int Total, string name)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Total = (from c in db.ProductType
                         where (!string.IsNullOrEmpty(name) ? c.Name.Contains(name) : true)
                         orderby c.Id
                         select c).Count();
                var items = (from c in db.ProductType
                             where (!string.IsNullOrEmpty(name) ? c.Name.Contains(name) : true)
                             orderby c.Id
                             select c).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                IList<ProductTypeInfo> ProductTypeInfos = new List<ProductTypeInfo>();
                foreach (var item in items)
                {
                    ProductTypeInfo info = new ProductTypeInfo();
                    info.ID = item.Id;
                    info.ProductTypeName = item.Name;
                    info.Description = item.Description;

                    ProductTypeInfos.Add(info);
                }

                return ProductTypeInfos;
            }
        }

        public bool GreateProductType(ProductType info)
        {
            using (MyDbContext db = new MyDbContext())
            {
                bool a = false;
                try
                {
                    db.ProductType.Add(info);
                    int b = db.SaveChanges();
                    if (b == 1)
                    { a = true; }
                    return a;
                }

                catch (Exception ex)
                {

                    return a;
                }
            }
        }

        public bool UpdateProductType(ProductType info)
        {
            using (MyDbContext db = new MyDbContext())
            {
                bool a = false;
                try
                {
                    db.Entry(info).State = EntityState.Modified;  //
                    int b = db.SaveChanges();
                    if (b == 1)
                    { a = true; }
                    return a;
                }
                catch (Exception ex)
                {

                    return a;
                }
            }
        }

        public int RemeProductType(string[] Id)
        {
            using (MyDbContext db = new MyDbContext())
            {
                int num = 0;
                try
                {
                    foreach (string typeid in Id)
                    {
                        System.Guid id = System.Guid.Parse(typeid);
                        var item = db.ProductType.Single(g => g.Id == id);
                        db.ProductType.Remove(item);
                        num = db.SaveChanges();

                    }

                    return num;
                }
                catch (Exception ex)
                {

                    return num;
                }

            }
        }
    }
}