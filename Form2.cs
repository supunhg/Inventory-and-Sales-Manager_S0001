using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.SQLite;

namespace fridayGUI_2
{
    public partial class Form2 : Form
    {
        public static int flag = 0;

        public Form2()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadData();
        }

        private int bill_total = 0;

        private void LoadData()
        {
            dataGridView1.Rows.Clear();
            comboBox1.Items.Clear();
            this.comboBox1.Text = "";
            this.textBox2.Clear();

            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
            {
                conn.Open();
                string query = "SELECT p_name FROM ProductTable";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    comboBox1.Items.Add(reader["p_name"].ToString());
                }
                reader.Close();

                string query2 = "SELECT SUM(order_total) AS bill_total FROM OrderTable";
                SQLiteCommand cmd2 = new SQLiteCommand(query2, conn);
                SQLiteDataReader reader2 = cmd2.ExecuteReader();

                while (reader2.Read())
                {
                    if (!reader2.IsDBNull(reader2.GetOrdinal("bill_total")))
                    {
                        bill_total = Convert.ToInt32(reader2["bill_total"]);
                    }
                }
                reader2.Close();

                textBox3.Text = bill_total.ToString();
                string query3 = "SELECT ProductTable.p_name, ProductTable.p_price, OrderTable.o_id, OrderTable.p_qty, OrderTable.order_total FROM OrderTable JOIN ProductTable ON OrderTable.p_id = ProductTable.p_id";
                SQLiteCommand cmd3 = new SQLiteCommand(query3, conn);
                SQLiteDataReader reader3 = cmd3.ExecuteReader();

                while (reader3.Read())
                {
                    dataGridView1.Rows.Add(reader3["o_id"].ToString(), reader3["p_name"].ToString(), reader3["p_price"].ToString(), reader3["p_qty"].ToString(), reader3["order_total"].ToString());

                }

                reader3.Close();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {

            if (this.textBox1.Text == "")
            {
                MessageBox.Show("Please enter an order ID!");
                return;
            }

            if (comboBox1.SelectedItem == null)
            {
                MessageBox.Show("Please select a product.");
                return;
            }

            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
            {
                string sel_query = "SELECT p_id, p_price FROM ProductTable WHERE p_name = @p_name";
                SQLiteCommand cmd_sel = new SQLiteCommand(sel_query, conn);
                cmd_sel.Parameters.AddWithValue("@p_name", this.comboBox1.Text);

                conn.Open();

                string productId = "";
                int price = 0;

                SQLiteDataReader reader = cmd_sel.ExecuteReader();

                if (reader.Read())
                {
                    productId = reader["p_id"].ToString();
                    price = Convert.ToInt32(reader["p_price"]);

                }
                else
                {
                    MessageBox.Show("No records found", "Error");
                }

                reader.Close();
                cmd_sel.ExecuteNonQuery();
                conn.Close();

                string query = "INSERT INTO OrderTable (o_id, o_date, customer_details, p_id, p_qty, order_total) VALUES (@o_id, @o_date, @customerDetails, @productId, @quantity, @total)";
            
                int quantity = Convert.ToInt32(this.numericUpDown1.Value);

                if (quantity == 0)
                {
                    MessageBox.Show("Quantity cannot be zero!");
                    return;
                }

                int total = price * quantity;

                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@o_id", textBox1.Text);
                cmd.Parameters.AddWithValue("@o_date", dateTimePicker1.Value);
                cmd.Parameters.AddWithValue("@customerDetails", richTextBox1.Text);
                cmd.Parameters.AddWithValue("@productId", productId);
                cmd.Parameters.AddWithValue("@quantity", quantity);
                cmd.Parameters.AddWithValue("@total", total);

                try
                {
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order created successfully!");
                    conn.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("An error occurred: " + ex.Message);
                }
                LoadData();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
            { 
                string query = "SELECT p_price FROM ProductTable WHERE p_name = @p_name";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@p_name", this.comboBox1.Text);

                conn.Open();
                SQLiteDataReader reader = cmd.ExecuteReader();

                if (reader.Read())
                {
                    this.textBox2.Text = reader["p_price"].ToString();
                }
            
                reader.Close();
            }     
        }

        private void backButton_Click(object sender, EventArgs e)
        {
            Form3 form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void deleteButton_Click(object sender, EventArgs e)
        {
            if (this.textBox1.Text != "")
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
                { 
                    string query = "DELETE FROM OrderTable WHERE o_id = @o_id";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@o_id", this.textBox1.Text);
            
                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Order removed successfully!");
                    conn.Close();

                    LoadData();
                    this.textBox1.Clear();
                } 
            }
            else
            {
                MessageBox.Show("Please enter an order ID!");
                return;
            }
            
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) || dateTimePicker1.Value != DateTime.Today || !string.IsNullOrWhiteSpace(richTextBox1.Text) || comboBox1.SelectedIndex != -1 || numericUpDown1.Value != numericUpDown1.Minimum) 
            {
                this.textBox1.Clear();
                this.dateTimePicker1.Value = DateTime.Today;
                this.richTextBox1.Clear();
                this.comboBox1.SelectedIndex = -1;
                this.numericUpDown1.Value = numericUpDown1.Minimum;
                this.textBox2.Clear();

                MessageBox.Show("Fields are cleared successfully!");
                return;
            }
            else
            {
                MessageBox.Show("Fields are already empty!");
                return;
            }
        }

        private void printButton_Click(object sender, EventArgs e)
        {
            flag = 0;

            Form4 form4 = new Form4(flag);
            form4.Show();
            this.Hide();
        }
    }
}
