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
    public partial class emp : Form
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
        public emp()
        {
            InitializeComponent();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            new eview_prod().Show();
            this.Dispose();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            new eplace_order().Show();
            this.Dispose();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new eview_order_details().Show();
            this.Dispose();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            new echange_pass().Show();
            this.Dispose();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            new eview_sorder_detail().Show();
            this.Dispose();
        }

        public void connect1()
        {
            String oradb = "Data Source=LAPTOP-N8ND9ES5;User ID=system;Password=gauri;";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void button6_Click(object sender, EventArgs e)
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

        private void button7_Click(object sender, EventArgs e)
        {
            new Form1().Show();
            this.Dispose();
        }
    }
}
