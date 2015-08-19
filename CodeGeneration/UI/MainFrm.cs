using System;
using System.Data;
using System.Data.Common;
using System.Data.Sql;
using System.Data.SqlClient;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace CodeGeneration.UI
{
    public partial class MainFrm : Form
    {
        public MainFrm()
        {
            InitializeComponent();
        }

        private void MainFrm_Load(object sender, EventArgs e)
        {
            Service.Sys_objectsService services = new Service.Sys_objectsService();
            List<View.Sys_objectsView> sys_objectView= services.GetAllSys_objects().Sys_ojbectsList;
            //dataGridView1.AutoGenerateColumns = false;
            dataGridView1.DataSource = sys_objectView;
        }

        private void dataGridView1_SelectionChanged(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows != null&&dataGridView1.SelectedRows.Count>0)
                {
                    View.Sys_objectsView selected = dataGridView1.SelectedRows[0].DataBoundItem as View.Sys_objectsView;
                    Service.Sys_columnsServices services = new Service.Sys_columnsServices();
                    Service.GetSys_columnsResponse reponse = services.GetSys_columnsOfTable(new Service.GetSys_columnsRequest() { TableName = selected.name });
                    dataGridView2.DataSource = reponse.Sys_columnsViewList;
                    dataGridView3.DataSource = reponse.sys_trelationViewList;
                }
            }
            catch(Exception ex)
            { }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                if (dataGridView1.SelectedRows != null && dataGridView1.SelectedRows.Count > 0)
                {
 
                }
            }
            catch (Exception ex)
            { }
        }
    }
}
