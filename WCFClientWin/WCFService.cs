using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WCFClientWin.WcfServiceLibrary;

namespace WCFClientWin
{
    public partial class WCFService : Form
    {
        public WCFService()
        {
            InitializeComponent();
        }

        private void WCFService_Load(object sender, EventArgs e)
        {
            UserClient uc = new UserClient();
            MessageBox.Show(uc.ShowNameAgain("hy"));
        }
    }
}
