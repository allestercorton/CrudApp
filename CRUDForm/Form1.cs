using System;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data;

namespace CRUDForm
{
    public partial class Form1 : Form
    {
        // Define a readonly SqlConnection object named "conn" with the specified connection string.
        private readonly SqlConnection conn = new SqlConnection("Data Source=EVERGREEN\\SQLEXPRESS;Initial Catalog=CarMagement;Integrated Security=True;");

        public Form1()
        {
            InitializeComponent();
            FetchData(); // Call the FetchData method to display data in the DataGridView when the form loads
        }

        private void FetchData()
        {
            try
            {
                conn.Open();
                SqlCommand cmd = new SqlCommand("SELECT * FROM CarsInfo", conn);
                SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                DataTable dataTable = new DataTable();
                adapter.Fill(dataTable);
                dataGridView1.DataSource = dataTable;
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {

        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "INSERT INTO CarsInfo VALUES(@ID, @Brand, @Color);";
                    cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@Brand", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Color", textBox3.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";

                    MessageBox.Show(rowsAffected > 0 ? "Create Success" : "Create Failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
                // Refresh the DataGridView after insertion
                FetchData();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "UPDATE CarsInfo SET Brand = @Brand, Color = @Color WHERE ID = @ID;";
                    cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    cmd.Parameters.AddWithValue("@Brand", textBox2.Text);
                    cmd.Parameters.AddWithValue("@Color", textBox3.Text);

                    int rowsAffected = cmd.ExecuteNonQuery();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";

                    MessageBox.Show(rowsAffected > 0 ? "Update Success" : "Update Failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
                // Refresh the DataGridView after update
                FetchData();
            }
        }

        private void button3_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    cmd.CommandText = "DELETE FROM CarsInfo WHERE ID = @ID";
                    cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));

                    int rowsAffected = cmd.ExecuteNonQuery();

                    textBox1.Text = "";
                    textBox2.Text = "";
                    textBox3.Text = "";

                    MessageBox.Show(rowsAffected > 0 ? "Delete Success" : "Delete Failed");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
                // Refresh the DataGridView after deletion
                FetchData();
            }
        }

        private void button4_Click(object sender, EventArgs e)
        {
            try
            {
                conn.Open();
                using (SqlCommand cmd = conn.CreateCommand())
                {
                    if (textBox1.Text == "0")
                    {
                        // Display all data if ID is 0
                        cmd.CommandText = "SELECT * FROM CarsInfo";
                    }
                    else
                    {
                        // Otherwise, filter by ID
                        cmd.CommandText = "SELECT * FROM CarsInfo WHERE ID = @ID";
                        cmd.Parameters.AddWithValue("@ID", int.Parse(textBox1.Text));
                    }

                    SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                    DataTable dataTable = new DataTable();
                    adapter.Fill(dataTable);

                    if (dataTable.Rows.Count > 0)
                    {
                        dataGridView1.DataSource = dataTable;
                    }
                    else
                    {
                        MessageBox.Show("No data found for the specified ID.");
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("An error occurred: " + ex.Message);
            }
            finally
            {
                conn.Close();
            }
        }

        // DATAGRID
        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }
    }
}
