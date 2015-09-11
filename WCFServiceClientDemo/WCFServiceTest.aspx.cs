using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using WCFServiceClientDemo.WCFService;

namespace WCFServiceClientDemo
{
    public partial class WCFServiceTest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnClick(object sender, EventArgs e)
        {
            UserClient uc = new UserClient();
            string resuslt = uc.ShowName(this.txtName.Text);
            Response.Write(resuslt);
        }
    }
}