﻿using Oracle.DataAccess.Client;
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
    public partial class eview_sorder_detail : Form
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

        public eview_sorder_detail()
        {
            InitializeComponent();
            
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
            comm.CommandText = "select* from sale_order";
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

        private void eview_sorder_detail_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            connect1();
            amount = 0;
            textBox2.Text = "";
            OracleCommand cm2 = new OracleCommand();
            cm2.Connection = conn;
            cm2.CommandText = "select prod_name, price, quantity, cus_name, address, phone from so_details natural join product natural join cust_phone natural join sale_order natural join customer where sorder_id=" + comboBox1.Text + "";
            cm2.CommandType = CommandType.Text;
            ds = new DataSet();
            da = new OracleDataAdapter(cm2.CommandText, conn);
            da.Fill(ds, "so_details natural join product natural join cust_phone natural join sale_order natural join customer");
            dt = ds.Tables["so_details natural join product natural join cust_phone natural join sale_order natural join customer"];
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                dr = dt.Rows[i];
                String name = dr["prod_name"].ToString();
                int price = int.Parse(dr["price"].ToString());
                int qty = int.Parse(dr["quantity"].ToString());
                amount += qty * price;
                label9.Text = amount.ToString();
                label5.Text = dr["cus_name"].ToString();
                label6.Text = dr["address"].ToString();
                label7.Text = dr["phone"].ToString();

                textBox2.Text = textBox2.Text + "\r\n" + name + "\t\t" + qty + "\t\t" + price + "\t\t" + price * qty + "\n";
            }
            


            cm2.ExecuteNonQuery();
            conn.Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new emp().Show();
            this.Dispose();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            connect1();
            OracleCommand cm2 = new OracleCommand();
            cm2.Connection = conn;
            cm2.CommandText = "cancel_sale";
            cm2.CommandType = CommandType.StoredProcedure;
            OracleParameter param = new OracleParameter("id", ""+int.Parse(comboBox1.Text));
            param.Direction = ParameterDirection.Input;
            param.DbType = DbType.Int32;
            cm2.Parameters.Add(param);
            da = new OracleDataAdapter(cm2);
            da.Fill(ds);
            dt = ds.Tables[0];
            dr = dt.Rows[0];
            comboBox1.Items.Remove(comboBox1.Text);
            MessageBox.Show("ORDER ID " + comboBox1.Text + " IS CANCELLED");
            OracleCommand cm1 = new OracleCommand();
            cm1.Connection = conn;
            cm1.CommandText = "delete from sale_order where sorder_id=" + comboBox1.Text + "";
            cm1.CommandType = CommandType.Text;
            cm1.ExecuteNonQuery();
            //comm.ExecuteNonQuery();
            conn.Close();
        }
    }
}
