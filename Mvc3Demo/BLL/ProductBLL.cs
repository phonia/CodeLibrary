using Mvc3Demo.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace Mvc3Demo.BLL
{
    public class ProductBLL
    {
        public IList<ProductInfo> GetProductList(int pageIndex, int pageSize, out int Total)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Total = (from c in db.Product
                         orderby c.Id
                         select c).Count();
                var items = (from c in db.Product
                             orderby c.Id
                             select c).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
                IList<ProductInfo> ProductInfos = new List<ProductInfo>();
                foreach (var item in items)
                {
                    ProductInfo info = new ProductInfo();
                    info.ID = item.Id;
                    info.ProductTypeID = item.ProductTypeId;
                    info.ProductTypeName = item.ProductType.Name;//导航属性的特点
                    info.Image = item.Image;
                    info.ProductName = item.Name;
                    info.MarketPrice = item.MakePrice;
                    info.NewPrice = item.NewPrice;
                    info.GetDate = item.GetDate.ToShortDateString();
                    info.Enable = item.Enable;
                    ProductInfos.Add(info);
                }

                return ProductInfos;
            }
        }

        public IList<ProductInfo> GetProductsBySerach(int pageIndex, int pageSize, out int Total, System.Guid typeId, System.Guid id, string name, int NewPrice, bool? b)
        {
            using (MyDbContext db = new MyDbContext())
            {
                Total = (from c in db.Product
                         where ((id != System.Guid.Empty) ? c.Id == id : true) && ((typeId != System.Guid.Empty) ? c.ProductTypeId == typeId : true) &&
                             (!string.IsNullOrEmpty(name) ? c.Name.Contains(name) : true) &&
                             ((NewPrice != 0) ? c.NewPrice == NewPrice : true) &&
                             ((b == null) ? true : c.Enable == b)//传值为NULL就是全部
                         orderby c.ProductTypeId
                         select c).Count();
                var items = (from c in db.Product
                             where ((id != System.Guid.Empty) ? c.Id == id : true) && ((typeId != System.Guid.Empty) ? c.ProductTypeId == typeId : true) &&
                            (!string.IsNullOrEmpty(name) ? c.Name.Contains(name) : true) &&
                            ((NewPrice != 0) ? c.NewPrice == NewPrice : true) &&
                            ((b == null) ? true : c.Enable == b)//传值为NULL就是全部
                             orderby c.ProductTypeId//必须在分页前排序
                             select c).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

                List<ProductInfo> ProductInfos = new List<ProductInfo>();
                foreach (var item in items)
                {
                    ProductInfo info = new ProductInfo();
                    info.ID = item.Id;
                    info.ProductTypeID = item.ProductTypeId;
                    info.ProductTypeName = item.ProductType.Name;//导航属性的特点
                    info.Image = item.Image;
                    info.ProductName = item.Name;
                    info.MarketPrice = item.MakePrice;
                    info.NewPrice = item.NewPrice;
                    info.GetDate = item.GetDate.ToShortDateString();
                    info.Enable = item.Enable;
                    ProductInfos.Add(info);
                }

                return ProductInfos;
            }
        }

        public bool GreateProduct(Product info)
        {
            using (MyDbContext db = new MyDbContext())
            {
                bool a = false;
                try
                {
                    db.Product.Add(info);
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

        public bool UpdateProduct(Product info)
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

        public int RemeProduct(string[] Ids)
        {
            using (MyDbContext db = new MyDbContext())
            {
                int num = 0;
                try
                {
                    foreach (string productid in Ids)
                    {
                        System.Guid id = System.Guid.Parse(productid);
                        var item = db.Product.Single(g => g.Id == id);
                        db.Product.Remove(item);
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