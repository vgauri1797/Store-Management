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
    public partial class Form2 : Form
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

        public Form2()
        {
            InitializeComponent();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            place_order po = new place_order();
            po.Show();
            this.Hide();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            view_order_det vod = new view_order_det();
            this.Hide();
            vod.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new view_product().Show();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new change_pass().Show();
            this.Dispose();
        }

        public void connect1()
        {
            String oradb = "Data Source=LAPTOP-N8ND9ES5;User ID=system;Password=gauri;";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            connect1();
            OracleCommand cm2 = new OracleCommand();
            cm2.Connection = conn;
            cm2.CommandText = "select fsale from dual";
            cm2.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(cm2.CommandText, conn);
            da.Fill(ds, "dual");
            dt = ds.Tables["dual"];
            dr = dt.Rows[0];
            MessageBox.Show(dr["fsale"].ToString());
            //comm.ExecuteNonQuery();
            conn.Close();
        }

        private void button5_Click_1(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Dispose();
        }

        private void Form2_Load(object sender, EventArgs e)
        {
            
        }

        private void button6_Click(object sender, EventArgs e)
        {
            connect1();
            OracleCommand cm2 = new OracleCommand();
            cm2.Connection = conn;
            cm2.CommandText = "p_sale";
            cm2.CommandType = CommandType.StoredProcedure;
            OracleParameter param = new OracleParameter("id", "" + Form1.cust_id1);
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.Int32;
            OracleParameter param2 = new OracleParameter("tot", OracleDbType.Int32);
            param2.Direction = ParameterDirection.Output;
            cm2.Parameters.Add(param);
            da = new OracleDataAdapter(cm2);
            ds = new DataSet();
            da.Fill(ds);
            dt = ds.Tables[0];
            dr = dt.Rows[0];
            cm2.ExecuteNonQuery();
            MessageBox.Show(param2.Value.ToString());
            conn.Close();
        }
    }
}
