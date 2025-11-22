using System.Drawing;
using System.Windows.Forms;

namespace Bank_MNGMT
{
    public partial class Main : Form
    {
        public Main()
        {
            InitializeComponent();
        }

        private void label3_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }

        private void btnCust_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            Customer customer = new Customer();
            customer.Show();
        }

        private void btnAcc_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            Account account = new Account();
            account.Show();
        }

        private void btnTrans_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            Transaction transaction = new Transaction();
            transaction.Show();
        }

        private void btnLoan_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            Loans loans = new Loans(); 
            loans.Show();
        }

        private void btnEmp_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            Employee employee = new Employee();
            employee.Show();
        }

        private void btnDash_Click(object sender, System.EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void btnExit_Click(object sender, System.EventArgs e)
        {
            Application.Exit();
        }

        private void label3_MouseEnter(object sender, System.EventArgs e)
        {
            label3.ForeColor = Color.Blue;
        }

        private void btnExit_MouseEnter(object sender, System.EventArgs e)
        {
            btnExit.ForeColor = Color.Red;
        }
    }
}
