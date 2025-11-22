using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Bank_MNGMT
{
    public partial class Loans : Form
    {
        public Loans()
        {
            InitializeComponent();
        }

        private void Loans_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'bank_dbDataSet11.loans' table. You can move, or remove it, as needed.
            this.loansTableAdapter2.Fill(this.bank_dbDataSet11.loans);
            // TODO: This line of code loads data into the 'bank_dbDataSet4.loans' table. You can move, or remove it, as needed.
            this.loansTableAdapter1.Fill(this.bank_dbDataSet4.loans);
            // TODO: This line of code loads data into the 'bank_dbDataSet3.loans' table. You can move, or remove it, as needed.
            this.loansTableAdapter.Fill(this.bank_dbDataSet3.loans);
            // TODO: This line of code loads data into the 'bank_dbDataSet4.loans' table. You can move, or remove it, as needed.
            RefreshData();

        }

        private void pictureBox1_Click(object sender, EventArgs e)
        {
            this.Hide();
            Main main = new Main();
            main.Show();
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

        private void button5_Click(object sender, EventArgs e)
        {
            this.Hide();
            Employee employee = new Employee();
            employee.Show();
        }

        private void button6_Click(object sender, EventArgs e)
        {
            this.Hide();
            Dashboard dashboard = new Dashboard();
            dashboard.Show();
        }

        private void label3_Click(object sender, EventArgs e)
        {
            this.Hide();
            Login login = new Login();
            login.Show();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                if (string.IsNullOrWhiteSpace(BoxLoType.Text) || string.IsNullOrWhiteSpace(txtAmount.Text) || string.IsNullOrWhiteSpace(txtIntRate.Text) || string.IsNullOrWhiteSpace(txtCustName.Text)) // || string.IsNullOrWhiteSpace(dateTimePicker2.Text)
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }

                if (btnAdd.Text == "Update" && btnAdd.Tag != null)
                {
                    int loan_id = (int)btnAdd.Tag;
                    SqlCommand cmd = new SqlCommand("UPDATE loans SET loan_type=@loan_type, amount=@amount, interest_rate=@interest_rate, loan_date=@loan_date, customer_name=@customer_name  WHERE loan_id=@loan_id", con);
                    cmd.Parameters.AddWithValue("@loan_id", loan_id);
                    cmd.Parameters.AddWithValue("@loan_type", BoxLoType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@interest_rate", txtIntRate.Text);
                    cmd.Parameters.AddWithValue("@loan_date", dateTimePicker2.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtCustName.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Customer updated!");
                    btnAdd.Text = "Add";
                    btnAdd.Tag = null;
                }
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO loans (loan_type, amount, interest_rate, loan_date, customer_name ) VALUES (@loan_type, @amount, @interest_rate, @loan_date, @customer_name)", con);
                    cmd.Parameters.AddWithValue("@loan_type", BoxLoType.SelectedItem.ToString());
                    cmd.Parameters.AddWithValue("@amount", txtAmount.Text);
                    cmd.Parameters.AddWithValue("@interest_rate", txtIntRate.Text);
                    cmd.Parameters.AddWithValue("@loan_date", dateTimePicker2.Value);
                    cmd.Parameters.AddWithValue("@customer_name", txtCustName.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New Customer Added Successfully!");
                }

                RefreshData();
                txtIntRate.Clear();
                txtAmount.Clear();
                txtCustName.Clear();
            }
        }


        private void dateTimePicker2_ValueChanged(object sender, EventArgs e)
        {
            dateTimePicker2.CustomFormat = "dd/mm/yyyy";
        }

        private void dateTimePicker2_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Back)
            {
                dateTimePicker2.CustomFormat = "";
            }
        }

        private void listBox1_Click(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                txtCustName.Text = listBox1.SelectedItem.ToString();
                listBox1.Visible = false;
            }
        }

        private void txtCustName_TextChanged(object sender, EventArgs e)
        {
            string searchText = txtCustName.Text.Trim();

            listBox1.Items.Clear();

            if (searchText.Length == 0)
            {
                listBox1.Visible = false;
                return;
            }

            // SQL baglanyşygy
            using (SqlConnection conn = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
            {
                try
                {
                    conn.Open();
                    SqlCommand cmd = new SqlCommand("SELECT name FROM customer WHERE name LIKE @search + '%'", conn);
                    cmd.Parameters.AddWithValue("@search", searchText);

                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        listBox1.Items.Add(reader.GetString(0));
                    }

                    listBox1.Visible = listBox1.Items.Count > 0;
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ýalňyşlyk: " + ex.Message);
                }
            }
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

        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0 || e.ColumnIndex < 0 || e.RowIndex >= dataGridView1.Rows.Count)
                return;

            object cellValue = dataGridView1.Rows[e.RowIndex].Cells[0].Value;
            if (cellValue == null || !int.TryParse(cellValue.ToString(), out int loan_id))
                return;

            string columnName = dataGridView1.Columns[e.ColumnIndex].Name;

            if (columnName == "Delete")
            {
                var confirm = MessageBox.Show("Do you want to delete this record?", "Confirm", MessageBoxButtons.YesNo);
                if (confirm == DialogResult.Yes)
                {
                    using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("DELETE FROM loans WHERE loan_id=@loan_id", con);
                        cmd.Parameters.AddWithValue("@loan_id", loan_id);
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Account deleted!");
                        RefreshData();
                    }
                }
            }
            else if (columnName == "Edit")
            {
                string customer_name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                string loan_type = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                string amount = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();
                string interest_rate = dataGridView1.Rows[e.RowIndex].Cells[4].Value.ToString();
                string loan_date = dataGridView1.Rows[e.RowIndex].Cells[5].Value.ToString();


                BoxLoType.Text = loan_type;
                txtAmount.Text = amount;
                txtCustName.Text = customer_name;
                txtIntRate.Text = interest_rate;


                btnAdd.Text = "Update";
                btnAdd.Tag = loan_id;
            }
        }
        private void AddEditDeleteButtons()
        {
            DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
            editBtn.HeaderText = "Edit";
            editBtn.Text = "Edit";
            editBtn.Name = "Edit";
            editBtn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editBtn);

            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.HeaderText = "Delete";
            deleteBtn.Text = "Delete";
            deleteBtn.Name = "Delete";
            deleteBtn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteBtn);

            dataGridView1.CellClick -= dataGridView1_CellClick; // Avoid duplicate event binding
            dataGridView1.CellClick += dataGridView1_CellClick;
        }
        private void RefreshData()
        {
            using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM loans", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

                // Remove existing Edit/Delete columns if they exist
                if (dataGridView1.Columns.Contains("Edit"))
                    dataGridView1.Columns.Remove("Edit");

                if (dataGridView1.Columns.Contains("Delete"))
                    dataGridView1.Columns.Remove("Delete");

                AddEditDeleteButtons();
            }
        }


    }
}
