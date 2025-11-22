using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Windows.Forms;

namespace Bank_MNGMT
{
    public partial class Employee : Form
    {
        public Employee()
        {
            InitializeComponent();
        }
        //data grid view refresh table func.
        private void Employee_Load(object sender, EventArgs e)
        {
            RefreshData();
        }
        //refreshing the datatable and showing
        private void RefreshData()
        {
            using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
            {
                con.Open();
                SqlDataAdapter sda = new SqlDataAdapter("SELECT * FROM employees", con);
                DataTable dt = new DataTable();
                sda.Fill(dt);
                dataGridView1.DataSource = dt;

                // knopkalary daatagridview tablisasyna birikdirmek
                if (!dataGridView1.Columns.Contains("Edit"))
                    AddEditDeleteButtons();
            }
        }
        //adding edit delete buttons into DGV.
        private void AddEditDeleteButtons()
        {
            // Edit btn. into DGV
            DataGridViewButtonColumn editBtn = new DataGridViewButtonColumn();
            editBtn.HeaderText = "Edit";
            editBtn.Text = "Edit";
            editBtn.Name = "Edit";
            editBtn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(editBtn);

            // Delete btn into DGV.
            DataGridViewButtonColumn deleteBtn = new DataGridViewButtonColumn();
            deleteBtn.HeaderText = "Delete";
            deleteBtn.Text = "Delete";
            deleteBtn.Name = "Delete";
            deleteBtn.UseColumnTextForButtonValue = true;
            dataGridView1.Columns.Add(deleteBtn);
            //CONNECTING EVENT INTO DVG
            dataGridView1.CellClick += dataGridView1_CellClick;
        }



        //Event configuration into DVG Cell_Click
        private void dataGridView1_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                //Selects a db over employee_id [eid]
                int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["eid"].Value);


                //Delete btn config.
                if (dataGridView1.Columns[e.ColumnIndex].Name == "Delete")
                {
                    var confirm = MessageBox.Show("Do you want to delete this record?", "Confirm", MessageBoxButtons.YesNo);
                    if (confirm == DialogResult.Yes)
                    {
                        //no if user choose "No" data not be deleted
                        // delete if user chooses "YES" command;
                        using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
                        {
                            con.Open();
                            SqlCommand cmd = new SqlCommand("DELETE FROM employees WHERE eid=@eid", con);
                            cmd.Parameters.AddWithValue("@eid", id);
                            cmd.ExecuteNonQuery();
                            MessageBox.Show("Employee deleted!");
                            RefreshData();
                        }
                    }
                }
                //Edit btn config./update
                else if (dataGridView1.Columns[e.ColumnIndex].Name == "Edit")
                {
                    string name = dataGridView1.Rows[e.RowIndex].Cells[1].Value.ToString();
                    string position = dataGridView1.Rows[e.RowIndex].Cells[2].Value.ToString();
                    string salary = dataGridView1.Rows[e.RowIndex].Cells[3].Value.ToString();

                    nametbl.Text = name;
                    postbl.Text = position;
                    slrtbl.Text = salary;

                    btnAdd.Text = "Update";
                    btnAdd.Tag = id;
                }
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (SqlConnection con = new SqlConnection("Data Source=BLACKGHOST-PC;Initial Catalog=\"bank db\";Integrated Security=True;Encrypt=False"))
            {
                con.Open();

                //if cell is empty user must fill all over them
                if (string.IsNullOrWhiteSpace(nametbl.Text) || string.IsNullOrWhiteSpace(postbl.Text) || string.IsNullOrWhiteSpace(slrtbl.Text))
                {
                    MessageBox.Show("Please fill all fields!");
                    return;
                }
                //edit/update if user click the edit btn into DGV. the db updated {Edit btn config} 
                if (btnAdd.Text == "Update" && btnAdd.Tag != null)
                {
                    int id = (int)btnAdd.Tag;
                    SqlCommand cmd = new SqlCommand("UPDATE employees SET name=@name, position=@position, salary=@salary WHERE eid=@id", con);
                    cmd.Parameters.AddWithValue("@id", id);
                    cmd.Parameters.AddWithValue("@name", nametbl.Text);
                    cmd.Parameters.AddWithValue("@position", postbl.Text);
                    cmd.Parameters.AddWithValue("@salary", slrtbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Employee updated!");
                    btnAdd.Text = "Add";
                    btnAdd.Tag = null;
                }
                //if just user wants to add new data  /// {Add btn config}
                else
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO employees (name, position, salary) VALUES (@Name, @Position, @Salary)", con);
                    cmd.Parameters.AddWithValue("@Name", nametbl.Text);
                    cmd.Parameters.AddWithValue("@Position", postbl.Text);
                    cmd.Parameters.AddWithValue("@Salary", slrtbl.Text);
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("New Employee Added Successfully!");
                }
                //{After updating or Adding new Data all Cells will be converted into Empty cell }
                RefreshData();
                nametbl.Clear();
                postbl.Clear();
                slrtbl.Clear();
            }
        }

        // Navigation buttons
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

        private void button4_Click(object sender, EventArgs e)
        {
            this.Hide();
            Loans loans = new Loans();
            loans.Show();
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

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void label3_MouseEnter(object sender, EventArgs e)
        {
            label3.ForeColor = Color.Blue;
        }

        private void btnExit_MouseEnter(object sender, EventArgs e)
        {
            btnExit.ForeColor = Color.Red;
        }
    }
}
