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
    public partial class view_order_det : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        DataSet ds1;
        DataTable dt1;
        DataRow dr1;
        OracleDataAdapter da1;
        int amount = 0;
        public view_order_det()
        {
            
            InitializeComponent();
            label7.Text = Form1.cust_name;
            label5.Text = Form1.address;
            label6.Text = Form1.phone;
            label9.Text = "" + 0;
            fillCombo();
        }

        public void connect1()
        {
            String oradb = "Data Source=LAPTOP-N8ND9ES5;User ID=system;Password=gauri;";
            conn = new OracleConnection(oradb);
            conn.Open();
        }
        
        public void fillCombo()
        {
            int i = 0;
            connect1();
            comm = new OracleCommand();
            comm.CommandText = "select* from sale_order where cust_id="+Form1.cust_id1;
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "sale_order");
            dt = ds.Tables["sale_order"];
            int t = dt.Rows.Count;
            for (i = 0; i < t; i++)
            {
                dr = dt.Rows[i];
                comboBox1.Items.Add(dr["sorder_id"]);
            }

            conn.Close();

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect1();
            amount = 0;
            textBox2.Text = "";
            OracleCommand cm2 = new OracleCommand();
            cm2.Connection = conn;
            cm2.CommandText = "select prod_name, price, quantity from so_details natural join product where sorder_id=" + comboBox1.Text + "";
            cm2.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(cm2.CommandText, conn);
            da.Fill(ds, "so_details natural join product");
            dt = ds.Tables["so_details natural join product"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                String name = dr["prod_name"].ToString();
                int price = int.Parse(dr["price"].ToString());
                int qty = int.Parse(dr["quantity"].ToString());
                amount += qty * price;
                label9.Text = amount.ToString();

                textBox2.Text = textBox2.Text + "\r\n"+ name + "\t\t" + qty + "\t\t" + price +"\t\t"+ price*qty+ "\n";
            }
            
            cm2.ExecuteNonQuery();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Dispose();
        }

        private void view_order_det_Load(object sender, EventArgs e)
        {

        }

        private void label11_Click(object sender, EventArgs e)
        {

        }
    }
}
