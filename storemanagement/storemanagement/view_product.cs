using Oracle.DataAccess.Client;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace storemanagement
{
    public partial class view_product : Form
    {
        public view_product()
        {
            InitializeComponent();
            string connectionString = "Data Source=LAPTOP-N8ND9ES5;User ID=system;Password=gauri;";
            string sql = "SELECT prod_name as PRODUCT , price as PRICE FROM product";
            OracleConnection connection = new OracleConnection(connectionString);
            OracleDataAdapter dataadapter = new OracleDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();
            dataadapter.Fill(ds, "product");
            connection.Close();
            dataGridView1.DataSource = ds;
            dataGridView1.DataMember = "product";
        }

        private void view_product_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Dispose();
        }
    }
}
