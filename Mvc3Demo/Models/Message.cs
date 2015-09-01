using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Mvc3Demo.Models
{
    [Serializable()]
    public class Message
    {
        /// <summary>
        /// 信息提示，当提交成功的时候返回给前台json的格式数据
        /// 用到相关json类库：JavaScriptSerializer json = new JavaScriptSerializer();
        /// </summary>
        /// <param name="success"></param>
        /// <param name="msg"></param>
        public Message(bool success, string msg)
        {
            this.msg = msg;
            this.success = success;
        }
        public bool success { get; set; }
        public string msg { get; set; }
    }

    [Serializable()]
    public class ProductInfo
    {
        public System.Guid ID { get; set; }
        public System.Guid ProductTypeID { get; set; }
        public string ProductTypeName { get; set; }
        public string ProductName { get; set; }
        public string Image { get; set; }
        public decimal MarketPrice { get; set; }
        public decimal NewPrice { get; set; }
        public string GetDate { get; set; }
        public bool? Enable { get; set; }
    }

    [Serializable()]
    public class ProductData
    {
        public int total { get; set; }
        public IList<ProductInfo> rows { get; set; }
    }

    [Serializable()]
    public class ProductTypeInfo
    {
        public System.Guid ID { get; set; }
        public string ProductTypeName { get; set; }
        public string Description { get; set; }
    }

    [Serializable()]
    public class ProductTypeData
    {
        public int total { get; set; }
        public IList<ProductTypeInfo> rows { get; set; }
    }
}