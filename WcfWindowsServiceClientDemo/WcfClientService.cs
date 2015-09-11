using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Linq;
using System.ServiceProcess;
using System.Text;
using System.ServiceModel;
using WcfWindowsServiceClientDemo.WCFServiceLibray;

namespace WcfWindowsServiceClientDemo
{
    partial class WcfClientService : ServiceBase
    {
        private ServiceHost _serviceHose = null;

        public WcfClientService()
        {
            InitializeComponent();
        }

        protected override void OnStart(string[] args)
        {
            // TODO: 在此处添加代码以启动服务。
            _serviceHose = new ServiceHost(typeof(UserClient));
            _serviceHose.Open();
        }

        protected override void OnStop()
        {
            // TODO: 在此处添加代码以执行停止服务所需的关闭操作。
            _serviceHose.Close();
        }
    }
}
