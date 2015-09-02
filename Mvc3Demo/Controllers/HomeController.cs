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

    }
}
