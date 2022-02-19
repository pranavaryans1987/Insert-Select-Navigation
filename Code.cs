using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.Sql;
using System.Data.SqlClient;

namespace WindowsFormsApp2
{
    public partial class Form3 : Form
    {
        int currentrecord = 0;
        int totalrecords = 0;
        int flag = 0;
        string username = "";
        DataTable dt = new DataTable();
        public Form3()
        {
            InitializeComponent();
        }

        private void Form3_Load(object sender, EventArgs e)
        {
            display();
        }

        private void display()
        {
            string sql = "select * from login";
            SqlDataAdapter da = new SqlDataAdapter(sql, Class1.cn);
            dt.Reset();
            da.Fill(dt);
            totalrecords = dt.Rows.Count;
            if (totalrecords >= 1)
            {
                navigate();
            }
            else
            {
                MessageBox.Show("No Records In Table!");
                totalrecords = 0;
            }
        }

        private void navigate()
        {
                textBox1.Text = dt.Rows[currentrecord]["username"].ToString();
                textBox2.Text = dt.Rows[currentrecord]["password"].ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            currentrecord = 0;
            navigate();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentrecord = totalrecords - 1;
            navigate();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (currentrecord < totalrecords-1)
            {
                currentrecord++;
                navigate();
            }
            else
            {
                MessageBox.Show("No More Records!");
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            if (currentrecord > 0)
            {
                currentrecord--;
                navigate();
            }
            else 
            {
                MessageBox.Show("Already On First Record!");
            }
        }

        private void button5_Click(object sender, EventArgs e)
        {
            if (flag == 0)
            {
                textBox1.Enabled = true;
                textBox2.Enabled = true;
                textBox1.Focus();
                flag = 1;
                button5.Text = "Save";
                username = textBox1.Text;
            }
            else if(flag == 1)
            {
                string update = "update login set username='" + textBox1.Text + "',password='" + textBox2.Text + "' where username='" + username + "'";
                SqlDataAdapter da = new SqlDataAdapter(update,Class1.cn);
                DataTable dt = new DataTable();
                int a = da.Fill(dt);
                if(a== 0)
                {
                    MessageBox.Show("Record Updated!");
                }
                flag = 0;
                button5.Text = "Update";
                textBox1.Enabled = false;
                textBox2.Enabled = false;
                currentrecord = 0;
                display();
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            username = textBox1.Text;
            string delete = "delete from login where username='" + username + "'";
            SqlDataAdapter da = new SqlDataAdapter(delete, Class1.cn);
            DataTable dt = new DataTable();
            int a = da.Fill(dt);
            if (a == 0)
            {
                totalrecords--;
                MessageBox.Show("Record Deleted!");
                textBox1.Text = "";
                textBox2.Text = "";
                currentrecord = 0;
                display();
            }
        }

        private void Form3_FormClosing(object sender, FormClosingEventArgs e)
        {
            Application.Exit();
        }
    }
}
