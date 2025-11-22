using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient; //db connection 

namespace Bank_MNGMT
{
    public partial class Login : Form
    {
        public Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False");
            con.Open();
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("Missing Data Filling!");
            }
            else
            {
                try
                {
                    string Name = txtUsername.Text;
                    string Phone = txtPassword.Text;
                    SqlCommand cmd = new SqlCommand("select username, password from logintab where username='" + txtUsername.Text + "'and password='" + txtPassword.Text + "'", con);
                    SqlDataAdapter sda = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    sda.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        this.Hide();
                        Main mn = new Main();
                        mn.Show();
                    }
                    else
                    {
                        MessageBox.Show("Login Failed! try again.");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnregis_Click(object sender, EventArgs e)
        {
            this.Hide();
            Registration rg = new Registration();
            rg.Show();
        }


        private void label4_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label4_MouseEnter(object sender, EventArgs e)
        {
            label4.ForeColor = Color.Red;
        }
    }
}

