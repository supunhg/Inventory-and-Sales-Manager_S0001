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
using System.Linq.Expressions;
using System.Data.SQLite;

namespace fridayGUI_2
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            this.StartPosition = FormStartPosition.CenterScreen;
            LoadData();
        }

        private void LoadData()
        {
            productGridView.Rows.Clear();

            using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
            {
                conn.Open();
                string query = "SELECT * FROM ProductTable";
                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                SQLiteDataReader reader = cmd.ExecuteReader();

                while (reader.Read())
                {
                    string p_id = reader["p_id"].ToString();
                    string p_name = reader["p_name"].ToString();
                    string p_price = reader["p_price"].ToString();

                    productGridView.Rows.Add(p_id, p_name, p_price);
                }
                reader.Close();
            }
        }

        private void productBackButton_Click(object sender, EventArgs e)
        {
            Form form3 = new Form3();
            form3.Show();
            this.Hide();
        }

        private void productSaveButton_Click(object sender, EventArgs e)
        {
            string pid = textBox1.Text;
            string name = textBox2.Text;
            decimal price = numericUpDown1.Value;

            using  (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
            {
                string query = "INSERT INTO ProductTable (p_id, p_name, p_price) VALUES (@p_id, @p_name, @p_price)";

                SQLiteCommand cmd = new SQLiteCommand(query, conn);
                cmd.Parameters.AddWithValue("@p_id", pid);
                cmd.Parameters.AddWithValue("@p_name", name);
                cmd.Parameters.AddWithValue("@p_price", price);

                try
                {
                    conn.Open();

                    int rowsAffected = cmd.ExecuteNonQuery();

                    if (rowsAffected > 0)
                    { 
                        MessageBox.Show("Product saved Successfully!");
                    }
                    else
                    {
                        MessageBox.Show("Failed to add product!");
                    }
                }
                catch (SQLiteException ex) when (ex.ErrorCode == (int)SQLiteErrorCode.Constraint)
                {
                    MessageBox.Show("Product ID exists!");
                }
                catch (SQLiteException ex)
                {
                    MessageBox.Show("SQLite-specific error: " + ex.Message);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("General error: " + ex.Message);
                }
                finally
                {
                    conn.Close();
                }
            }
            LoadData();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text))
            {
                using (SQLiteConnection conn = new SQLiteConnection(DatabaseHelper.connStr))
                {
                    string query = "DELETE FROM ProductTable WHERE p_id = @p_id";
                    SQLiteCommand cmd = new SQLiteCommand(query, conn);
                    cmd.Parameters.AddWithValue("@p_id", this.textBox1.Text);

                    conn.Open();
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Product removed Successfully!");
                    conn.Close();
                    this.textBox1.Clear();
                }
                
            }
            else
            {
                MessageBox.Show("Please enter a product ID to remove!");
                return;
            }
            LoadData();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(textBox1.Text) || !string.IsNullOrEmpty(textBox2.Text) || numericUpDown1.Value != 0)
            {
                this.textBox1.Clear();
                this.textBox2.Clear();
                this.numericUpDown1.Value = 0;
                MessageBox.Show("Fields are cleared successfully!");
                return;
            }
            else
            {
                MessageBox.Show("Fields are already empty!");
                return;
            }
        }
    }
}
