using System.Data.SqlClient;
using System;
using System.Windows.Forms;
using System.Data;
using System.Drawing;

namespace Bank_MNGMT
{
    public partial class Customer : Form
    {
        public Customer()
        {
            InitializeComponent();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                if (string.IsNullOrWhiteSpace(NameTbl.Text) || string.IsNullOrWhiteSpace(PhoneTbl.Text) || string.IsNullOrWhiteSpace(EmailTbl.Text) || string.IsNullOrWhiteSpace(AddressTbl.Text))
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                if (btnAdd.Text == "Update" && btnAdd.Tag != null)
                {
                    int id = (int)btnAdd.Tag;
                    SqlCommand cmd = new SqlCommand("UPDATE customer SET name=@name, phone=@phone, email=@email, address=@address WHERE id=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", NameTbl.Text);
                    cmd.Parameters.AddWithValue("@phone", PhoneTbl.Text);
                    cmd.Parameters.AddWithValue("@email", EmailTbl.Text);
                    cmd.Parameters.AddWithValue("@address", AddressTbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer updated!");
                    btnAdd.Text = "Add";
                    btnAdd.Tag = null;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO customer (name, phone, email, address) VALUES (@name, @phone, @email, @address)", con);
                    cmd.Parameters.AddWithValue("@name", NameTbl.Text);
                    cmd.Parameters.AddWithValue("@phone", PhoneTbl.Text);
                    cmd.Parameters.AddWithValue("@email", EmailTbl.Text);
                    cmd.Parameters.AddWithValue("@address", AddressTbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New Customer Added Successfully!");
                }

                RefreshData();
                NameTbl.Clear();
                PhoneTbl.Clear();
                EmailTbl.Clear();
                AddressTbl.Clear();
            }
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            this.customerTableAdapter.Fill(this.bank_dbDataSet8.customer);
            RefreshData();
        }

        private void RefreshData()
        {
            using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM Customer", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView.DataSource = dt;

                // Remove existing Edit/Delete columns if they exist
                if (dataGridView.Columns.Contains("Edit"))
                    dataGridView.Columns.Remove("Edit");

                if (dataGridView.Columns.Contains("Delete"))
                    dataGridView.Columns.Remove("Delete");

                AddEditDeleteButtons();
            }
        }

        private void AddEditDeleteButtons()
        {
            DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
            editBtn.HeaderText = "Edit";
            editBtn.Text = "Edit";
            editBtn.Name = "Edit";
            editBtn.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(editBtn);

            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.HeaderText = "Delete";
            deleteBtn.Text = "Delete";
            deleteBtn.Name = "Delete";
            deleteBtn.UseColumnTextForButtonValue = true;
            dataGridView.Columns.Add(deleteBtn);

            dataGridView.CellClick -= dataGridView_CellClick; // Avoid duplicate event binding
            dataGridView.CellClick += dataGridView_CellClick;
        }

        private void dataGridView_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= dataGridView.Rows.Count)
                return;

            object cellValue = dataGridView.Rows[e.RowIndex].Cells[0].Value;
            if (cellValue == null || !int.TryParse(cellValue.ToString(), out int id))
                return;

            string columnName = dataGridView.Columns[e.ColumnIndex].Name;

            if (columnName == "Delete")
            {
                var confirm = MessageBox.Show("Do you want to delete this record?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM customer WHERE id=@id", con);
                        cmd.Parameters.AddWithValue("@id", id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Customer deleted!");
                        RefreshData();
                    }
                }
            }
            else if (columnName == "Edit")
            {
                string name = dataGridView.Rows[e.RowIndex].Cells[1].Value.ToString();
                string phone = dataGridView.Rows[e.RowIndex].Cells[2].Value.ToString();
                string email = dataGridView.Rows[e.RowIndex].Cells[3].Value.ToString();
                string address = dataGridView.Rows[e.RowIndex].Cells[4].Value.ToString();

                NameTbl.Text = name;
                PhoneTbl.Text = phone;
                EmailTbl.Text = email;
                AddressTbl.Text = address;

                btnAdd.Text = "Update";
                btnAdd.Tag = id;
            }
        }

        private void lblLog_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login lg = new Login();
            lg.Show();
        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
        }

        private void btnAcc_Click(object sender, EventArgs e)
        {
            this.Hide();
            Account account = new Account();
            account.Show();
        }

        private void btnTrans_Click(object sender, EventArgs e)
        {
            this.Hide();
            Transaction transaction = new Transaction();
            transaction.Show();
        }

        private void btnLoan_Click(object sender, EventArgs e)
        {
            this.Hide();
            Loans loans = new Loans();
            loans.Show();
        }

        private void btnEmp_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee employee = new Employee();
            employee.Show();
        }

        private void btnDash_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.ForeColor = Color.Red;
        }

        private void lblLog_MouseEnter(object sender, EventArgs e)
        {
            lblLog.ForeColor = Color.Blue;
        }
    }
}
