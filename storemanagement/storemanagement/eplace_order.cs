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
    public partial class eplace_order : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        int oid = 1;
        int[] pqty = new int[15];
        int len = 0;
        int tot_amount = 0;
        public eplace_order()
        {
            InitializeComponent();
            fillCombo();
            label2.Text = Form1.cust_name;
            label5.Text = Form1.address;
            label6.Text = Form1.phone.ToString();
            label8.Text = 0 + "";
            textBox3.Text = "PRODUCT NAME\t\tQUANTITY\tPRICE\n";
            textBox1.Text = "";
            oid = calID() + 1;
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
            comm.CommandText = "select* from product";
            comm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(comm.CommandText, conn);
            da.Fill(ds, "product");
            dt = ds.Tables["product"];
            int t = dt.Rows.Count;
            len = t;
            for (i = 0; i < t; i++)
            {
                dr = dt.Rows[i];
                comboBox1.Items.Add(dr["prod_name"]);
                pqty[i + 1] = int.Parse(dr["stock"].ToString());
            }

            //comm.ExecuteNonQuery();
            conn.Close();

        }

        public int calID()
        {
            connect1();
            OracleCommand cm = new OracleCommand();
            cm.Connection = conn;
            cm.CommandText = "select max(porder_id) from purchase_order";
            cm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(cm.CommandText, conn);
            da.Fill(ds, "purchase_order");
            dt = ds.Tables["purchase_order"];
            dr = dt.Rows[0];
            int id = Int32.Parse(dr["max(porder_id)"].ToString());
            cm.ExecuteNonQuery();
            conn.Close();
            return id;
        }

        private void eplace_order_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect1();
            OracleCommand cm = new OracleCommand();
            cm.Connection = conn;
            OracleCommand cm1 = new OracleCommand();
            cm1.Connection = conn;
            cm.CommandText = "select prod_id ,stock from product where prod_name='" + comboBox1.Text + "'";
            cm.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(cm.CommandText, conn);
            da.Fill(ds, "product");
            dt = ds.Tables["product"];
            dr = dt.Rows[0];
            int id = 0;
            id = int.Parse(dr["prod_id"].ToString());
            int qty = int.Parse(dr["stock"].ToString());

           
                OracleCommand cm4 = new OracleCommand();
                cm4.Connection = conn;
                cm4.CommandText = "update product set stock=stock+" + textBox2.Text + " where prod_id=" + id + "";
                cm4.CommandType = CommandType.Text;
                cm4.ExecuteNonQuery();

                cm1.CommandText = "insert into po_details values(" + oid + "," + id + "," + textBox2.Text + ")";
                cm1.CommandType = CommandType.Text;
                cm1.ExecuteNonQuery();

                OracleCommand cm2 = new OracleCommand();
                cm2.Connection = conn;
                cm2.CommandText = "select prod_name, price from product where prod_id=" + id + "";
                cm2.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(cm2.CommandText, conn);
                da.Fill(ds, "product");
                dt = ds.Tables["product"];
                dr = dt.Rows[0];
                int price = int.Parse(dr["price"].ToString());
                string pname = dr["prod_name"].ToString();
                OracleCommand cm3 = new OracleCommand();
                cm3.Connection = conn;
                cm3.CommandText = "tot_price";
                tot_amount += price * int.Parse(textBox2.Text);
                label8.Text = tot_amount + "";
                // cm2.ExecuteNonQuery();
                conn.Close();
                string x = "\n" + pname + "\t\t\t" + textBox2.Text + "\t\t" + price + "\n";
                string s = textBox1.Text;
                s = s + "\r\n" + x + "\n ";
                textBox1.Text = s;
                comboBox1.Items.Remove(comboBox1.Text);
            
        }

        public string extract_date()
        {
            DateTime d = DateTime.Now;
            String s = d.ToString("dd") + "-" + d.ToString("MMMM").Substring(0, 3) + "-" + d.ToString("yyyy");
            return s;

        }

        private void button2_Click(object sender, EventArgs e)
        {
            connect1();
            OracleCommand cm1 = new OracleCommand();
            cm1.Connection = conn;
            String s = extract_date();

            Console.WriteLine(s + Form1.cust_id1);
            cm1.CommandText = "insert into purchase_order values(" + oid + "," + Form1.cust_id1 + ",'" + s + "'," + ((oid % 4) + 1) + ")";
            cm1.CommandType = CommandType.Text;
            cm1.ExecuteNonQuery();

            conn.Close();
            emp f2 = new emp();
            f2.Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connect1();
            OracleCommand cm = new OracleCommand();
            cm.Connection = conn;
            cm.CommandText = "delete from po_details where porder_id=" + oid + "";
            cm.CommandType = CommandType.Text;
            cm.ExecuteNonQuery();

            OracleCommand cm1 = new OracleCommand();
            for (int i = 0; i < len; i++)
            {
                cm1.Connection = conn;
                cm1.CommandText = "update product set stock=" + pqty[i + 1] + " where prod_id=" + (i + 1) + "";
                cm1.CommandType = CommandType.Text;
                cm1.ExecuteNonQuery();
            }
            emp f2 = new emp();
            f2.Show();
            this.Dispose();
            conn.Close();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new emp().Show();
            this.Dispose();
        }
    }
}
