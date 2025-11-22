using System;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Bank_MNGMT
{
    public partial class Dashboard : Form
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void Dashboard_Load(object sender, System.EventArgs e)
        {
            display();
            display1();
            display2();
            display3();
            display4();

        }
        private void display()
        {
            SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("SELECT COUNT(*) FROM customer", con);
            Int32 cout = Convert.ToInt32(cmd.ExecuteScalar());
            if (cout > 0)
            {
                cust.Text = Convert.ToString(cout.ToString());

            }
            else
            {
                cust.Text = "0";
            }
            con.Close();
        }
        private void display1()
        {
            SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select count(*) from employees", con);
            Int32 cout = Convert.ToInt32(cmd.ExecuteScalar());
            if (cout > 0)
            {
                emp.Text = Convert.ToString(cout.ToString());

            }
            else
            {
                emp.Text = "0";
            }
            con.Close();
        }
        private void display2()
        {
            SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select count(*) from transactions", con);
            Int32 cout = Convert.ToInt32(cmd.ExecuteScalar());
            if (cout > 0)
            {
                trans.Text = Convert.ToString(cout.ToString());

            }
            else
            {
                trans.Text = "0";
            }
            con.Close();
        }
        private void display3()
        {
            SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select count(*) from loans", con);
            Int32 cout = Convert.ToInt32(cmd.ExecuteScalar());
            if (cout > 0)
            {
                loan.Text = Convert.ToString(cout.ToString());

            }
            else
            {
                loan.Text = "0";
            }
            con.Close();
        }
        private void display4()
        {
            SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False");
            con.Open();
            SqlCommand cmd = new SqlCommand("Select count(*) from accounts", con);
            Int32 cout = Convert.ToInt32(cmd.ExecuteScalar());
            if (cout > 0)
            {
                acc.Text = Convert.ToString(cout.ToString());

            }
            else
            {
                acc.Text = "0";
            }
            con.Close();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main ma = new Main();
            ma.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Customer customer = new Customer();
            customer.Show();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
            Account account = new Account();
            account.Show();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Transaction transaction = new Transaction();
            transaction.Show();
        }

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Loans loans = new Loans();
            loans.Show();
        }

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee employee = new Employee();
            employee.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
           Application.Exit();
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.ForeColor = Color.Red;
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Blue;
        }
    }
}
