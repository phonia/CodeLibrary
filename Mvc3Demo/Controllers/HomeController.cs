using Mvc3Demo.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Runtime.Serialization;

namespace Mvc3Demo.Controllers
{
    public class HomeController : Controller
    {


        private BLL.ProductTypeBLL productTypeBLL = new BLL.ProductTypeBLL();
        private BLL.ProductBLL productBLL = new BLL.ProductBLL();
        private JavaScriptSerializer json = new JavaScriptSerializer();

        //
        // GET: /Home/

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult LoadProductTypejson()
        {

            int row = int.Parse(Request["rows"].ToString());
            int pageindex = int.Parse(Request["page"].ToString());
            ProductTypeData ProductTypejson = new ProductTypeData();
            int total;
            ProductTypejson.rows = productTypeBLL.GetProductTypesList(pageindex, row, out total);
            ProductTypejson.total = total;
            string Str = json.Serialize(ProductTypejson);//   
            return Content(Str, "text/html;charset=UTF-8");

        }

        public ActionResult LoadProductjson()
        {
            //string sort = Request["sort"].ToString();暂时实现不了动态的字段倒/正排序 除非是sql语句 用Switch显得很臃肿
            //sort = (!string.IsNullOrEmpty(sort) ? sort : "ProductId");
            //string order = Request["order"].ToString();
            //order = (!string.IsNullOrEmpty(order) ? order : "ascending");
            int row = int.Parse(Request["rows"].ToString());
            int pageindex = int.Parse(Request["page"].ToString());
            ProductData Productjson = new ProductData();
            int total;
            Productjson.rows = productBLL.GetProductList(pageindex, row, out total);
            //Productjson.rows = productBLL.GetProductList(1,5 , out total);
            Productjson.total = total;
            //string Str = JsonConvert.SerializeObject(Productjson);
            JsonResult jr = new JsonResult();
            jr.Data = Productjson;
            jr.ContentType = "text/html;charset=UTF-8";
            return jr;
        }

        public ActionResult Products()
        {
            return View();
        }

        public ActionResult CreateProductType(FormCollection collection)
        {
            //int ProductTypeId = Convert.ToInt32(collection.Get("Id"));
            String ProductTypeName = collection.Get("Name");//是控件的Name属性而不是id属性
            String Description = collection.Get("Description");
            ProductType info = new ProductType();
            info.Name = ProductTypeName;
            info.Description = Description;

            Message msg;
            if (productTypeBLL.GreateProductType(info))
            {
                msg = new Message(true, "添加" + ProductTypeName + "信息成功！");
            }
            else
            {
                msg = new Message(false, "添加产品类型失败，操作有误");
            }
            string Str = json.Serialize(msg);//   
            return Content(Str, "text/html;charset=UTF-8");
        }

        /// ProductType的commbox列表
        /// </summary>
        /// <returns></returns>
        public ActionResult GetTypesList()
        {
            IList<ProductTypeInfo> ProductTypes = productTypeBLL.GetProductTypesList();
            //System.Web.Extentions的DLL中
            JavaScriptSerializer json = new JavaScriptSerializer();
            string Str = json.Serialize(ProductTypes);//   方法一
            return Content(Str, "text/html;charset=UTF-8");
        }

        public ActionResult GetTestTree()
        {
            string rel = "";
            //using (MyDbContext context = new MyDbContext())
            //{
            //    var cd = (from b in context.TestTree.Where(it => true) select b).FirstOrDefault();
            //    rel = json.Serialize(cd);
            //}


            node n1 = new node() { id = 0, text = "0" };
            node n2 = new node() { id = 1, text = "1" };
            node n3 = new node() { id = 2, text = "2" };
            n3.children = new List<node>();
            n3.children.Add(n1);
            n3.children.Add(n2);
            List<node> list = new List<node>();
            list.Add(n3);

            return Content(json.Serialize(list), "text/html:charset=utf-8");
        }

        public ActionResult TestTree()
        {
            return View();
        }

    }

    [Serializable]
    public class node
    {
        public int id { get; set; }
        public string text { get; set; }
        public IList<node> children { get; set; }
    }
}
