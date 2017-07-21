using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Oracle.DataAccess.Client;
using Oracle.DataAccess.Types;
using System.Data;

namespace storemanagement
{
    public partial class Form1 : Form
    {
        static int i = 0;
        public static int cust_id1;
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;
        public static string cust_name = "";
        public static string address = "";
        public static string phone=0+"";
        public static string pass = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void label1_Click(object sender, EventArgs e)
        {

        }

        public void connect1()
        {
            String oradb = "Data Source=LAPTOP-N8ND9ES5;User ID=system;Password=gauri;";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            String s= "";
            if (radioButton2.Checked == true)
            {
                connect1();
                comm = new OracleCommand();
                comm.CommandText = "select* from employee";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "employee");
                dt = ds.Tables["employee"];
                int t = dt.Rows.Count;
                dr = dt.Rows[i];
                String s1, s2;
                int len = t;
                int j = 0;
                String user;
                user = textBox1.Text;
                pass = textBox2.Text;
                int flag = 0;
                while (j < len)
                {
                    dr = dt.Rows[j];
                    s1 = dr["emp_ID"].ToString();
                    s2 = dr["password"].ToString();
                    if (s1.Equals(user) && s2.Equals(pass))
                    {
                        // MessageBox.Show("Logged In!");
                        flag = 1;
                        cust_id1 = Int32.Parse(s1);
                        cust_name = dr["emp_name"].ToString();
                        Console.WriteLine(cust_name);
                        address = dr["address"].ToString();
                        comm.CommandText = "select* from emp_phone where emp_id=" + s1 + "";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "emp_phone");
                        dt = ds.Tables["emp_phone"];
                        dr = dt.Rows[i];
                        phone = dr["phone"].ToString();
                        emp f2 = new emp();
                        f2.Show();
                        this.Hide();
                        break;
                    }
                    j++;
                }
                if (flag == 0)
                {
                    MessageBox.Show("Wrong Username/Password");
                    //new Form1().Show();
                }
                //comm.ExecuteNonQuery();
                conn.Close();
            }
            else
            {
                connect1();
                comm = new OracleCommand();
                comm.CommandText = "select* from customer";
                comm.CommandType = CommandType.Text;
                ds = new DataSet();
                da = new OracleDataAdapter(comm.CommandText, conn);
                da.Fill(ds, "customer");
                dt = ds.Tables["customer"];
                int t = dt.Rows.Count;
                dr = dt.Rows[i];
                String s1, s2;
                int len = t;
                int j = 0;
                String user;
                user = textBox1.Text;
                pass = textBox2.Text;
                int flag = 0;
                while (j < len)
                {
                    dr = dt.Rows[j];
                    s1 = dr["cust_ID"].ToString();
                    s2 = dr["password"].ToString();
                    if (s1.Equals(user) && s2.Equals(pass))
                    {
                        // MessageBox.Show("Logged In!");
                        flag = 1;
                        cust_id1 = Int32.Parse(s1);
                        cust_name = dr["cus_name"].ToString();
                        Console.WriteLine(cust_name);
                        address = dr["address"].ToString();
                        comm.CommandText = "select* from cust_phone where cust_id=" + s1 + "";
                        comm.CommandType = CommandType.Text;
                        ds = new DataSet();
                        da = new OracleDataAdapter(comm.CommandText, conn);
                        da.Fill(ds, "cust_phone");
                        dt = ds.Tables["cust_phone"];
                        dr = dt.Rows[i];
                        phone = dr["phone"].ToString();
                        Form2 f2 = new Form2();
                        f2.Show();
                        this.Hide();
                        break;
                    }
                    j++;
                }
                if (flag == 0)
                {
                    MessageBox.Show("Wrong Username/Password");
                    //new Form1().Show();
                }
                //comm.ExecuteNonQuery();
                conn.Close();
            }

        }
    }
}
