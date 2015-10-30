using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WCFHelloWorldClient
{
    public partial class WebForm1 : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Hello.HelloKettyClient hc = new Hello.HelloKettyClient();
            Response.Write(hc.Show());
        }
    }
}