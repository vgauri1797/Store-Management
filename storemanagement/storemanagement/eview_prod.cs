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
    public partial class eview_prod : Form
    {
        //private readonly object dataGridView2;

        public eview_prod()
        {
            InitializeComponent();
            string connectionString = "Data Source=LAPTOP-N8ND9ES5;User ID=system;Password=gauri;";
            string sql = "SELECT prod_id as PRODUCT_ID ,prod_name as PRODUCT, price as PRICE ,stock as QUANTITY from product";
            OracleConnection connection = new OracleConnection(connectionString);
            OracleDataAdapter dataadapter = new OracleDataAdapter(sql, connection);
            DataSet ds = new DataSet();
            connection.Open();
            dataadapter.Fill(ds, "product");
            connection.Close();
            dataGridView2.DataSource = ds;
            dataGridView2.DataMember = "product";
        }

        private void eview_prod_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            new emp().Show();
            this.Dispose();
        }
    }
}
