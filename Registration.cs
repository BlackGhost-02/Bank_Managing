using System;
using System.Data;
using System.Data.SqlClient; //sql connection
using System.Drawing;
using System.Windows.Forms;
using System.Xml;

namespace Bank_MNGMT
{
    public partial class Registration : Form
    {
        public Registration()
        {
            InitializeComponent();
        }

        private void btnregis_Click(object sender, EventArgs e)
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
                    SqlCommand cmd = new SqlCommand("insert into logintab values(@username, @password)", con);
                    cmd.Parameters.AddWithValue("@username", txtUsername.Text);
                    cmd.Parameters.AddWithValue("@password", txtPassword.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Registration Successfuly!");
                    txtUsername.Text = "";
                    txtPassword.Text = "";
                    
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }

        private void label2_MouseEnter(object sender, EventArgs e)
        {
            label2.ForeColor = Color.Blue;
        }
    }

    //Registration btn configuration and connection with SQL db
    //private void btnreg_Click(object sender, EventArgs e)
    //{
    //    SqlConnection conn = new SqlConnection(@"Data Source=BLACKGHOST-PC;Initial Catalog=""bank db"";Integrated Security=True;Encrypt=False");
    //    conn.Open();
    //    SqlCommand cnn = new SqlCommand("insert into logintab values(@name, @surname, @contact, @email, @username, @password)", conn);
    //    _ = cnn.Parameters.AddWithValue("@name", textBox2.Text);
    //    _ = cnn.Parameters.AddWithValue("@surname", textBox3.Text);
    //    _ = cnn.Parameters.AddWithValue("@contact", textBox4.Text);
    //    _ = cnn.Parameters.AddWithValue("@email", textBox5.Text);
    //    _ = cnn.Parameters.AddWithValue("@username", textBox6.Text);
    //    _ = cnn.Parameters.AddWithValue("@password", textBox7.Text);
    //    _ = cnn.ExecuteNonQuery();
    //    conn.Close();
    //    //the message when when record Saved
    //    _ = MessageBox.Show("Record Saved Successfully!!!");
    //}
}

