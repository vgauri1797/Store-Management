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
    public partial class change_pass : Form
    {
        OracleConnection conn;
        OracleCommand comm;
        OracleDataAdapter da;
        DataSet ds;
        DataTable dt;
        DataRow dr;

        public change_pass()
        {
            InitializeComponent();
        }
        public void connect1()
        {
            String oradb = "Data Source=LAPTOP-N8ND9ES5;User ID=system;Password=gauri;";
            conn = new OracleConnection(oradb);
            conn.Open();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text.Equals(Form1.pass))
            {
                if (textBox3.Text.Equals(textBox2.Text))
                {
                    connect1();
                    OracleCommand cm = new OracleCommand();
                    cm.Connection = conn;
                    cm.CommandText = "update customer set password='"+textBox2.Text+ "' where cust_id= "+Form1.cust_id1+"";
                    cm.CommandType = CommandType.Text;
                    cm.ExecuteNonQuery();
                    MessageBox.Show("Passwords changed!");

                }
                else
                {
                    MessageBox.Show("Passwords don't match");
                }
            }
            else
            {
                MessageBox.Show("Wrong Password");
            }
            
        }

        private void button2_Click(object sender, EventArgs e)
        {
            new Form2().Show();
            this.Dispose();
        }

        private void change_pass_Load(object sender, EventArgs e)
        {

        }
    }
}
