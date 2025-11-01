using MySql.Data.MySqlClient;
using System;
using System.Data;
using System.Windows.Forms;

namespace RecipeOrganizer
{
    public partial class Form1 : Form
    {
        DBConnection db = new DBConnection();

        public Form1()
        {
            InitializeComponent();
        }

   
        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();

            try
            {
                db.OpenConnection();
                MessageBox.Show("Connected to MySQL successfully!");
                LoadData();
            }
            catch (Exception ex)
            {
                MessageBox.Show("MySQL Connection Failed: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

       
        private void lblIngredients_Click(object sender, EventArgs e)
        {
            
        }

       
        private void LoadData()
        {
            try
            {
                db.OpenConnection();
                string query = "SELECT * FROM recipes";
                MySqlDataAdapter adapter = new MySqlDataAdapter(query, db.GetConnection());
                DataTable table = new DataTable();
                adapter.Fill(table);
                dgvRecipes.DataSource = table;
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error loading data: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

       
        private void btnAdd_Click_1(object sender, EventArgs e)
        {
            try
            {
                db.OpenConnection();
                string query = "INSERT INTO recipes (recipe_name, ingredients, steps, category) VALUES (@name, @ingredients, @steps, @category)";
                MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@ingredients", txtIngredients.Text);
                cmd.Parameters.AddWithValue("@steps", txtSteps.Text);
                cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Recipe Added Successfully!");
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error adding recipe: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void btnUpdate_Click_1(object sender, EventArgs e)
        {
            if (dgvRecipes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a recipe to update.");
                return;
            }

            int id = Convert.ToInt32(dgvRecipes.SelectedRows[0].Cells["id"].Value);

            try
            {
                db.OpenConnection();
                string query = "UPDATE recipes SET recipe_name=@name, ingredients=@ingredients, steps=@steps, category=@category WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@name", txtName.Text);
                cmd.Parameters.AddWithValue("@ingredients", txtIngredients.Text);
                cmd.Parameters.AddWithValue("@steps", txtSteps.Text);
                cmd.Parameters.AddWithValue("@category", txtCategory.Text);
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Recipe Updated Successfully!");
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error updating recipe: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void btnDelete_Click_1(object sender, EventArgs e)
        {
            if (dgvRecipes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a recipe to delete.");
                return;
            }

            int id = Convert.ToInt32(dgvRecipes.SelectedRows[0].Cells["id"].Value);

            try
            {
                db.OpenConnection();
                string query = "DELETE FROM recipes WHERE id=@id";
                MySqlCommand cmd = new MySqlCommand(query, db.GetConnection());
                cmd.Parameters.AddWithValue("@id", id);
                cmd.ExecuteNonQuery();
                MessageBox.Show("Recipe Deleted Successfully!");
                LoadData();
                ClearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error deleting recipe: " + ex.Message);
            }
            finally
            {
                db.CloseConnection();
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

      
        private void ClearFields()
        {
            txtName.Clear();
            txtIngredients.Clear();
            txtSteps.Clear();
            txtCategory.Clear();
        }

        
        private void dgvRecipes_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex >= 0)
            {
                txtName.Text = dgvRecipes.Rows[e.RowIndex].Cells["recipe_name"].Value.ToString();
                txtIngredients.Text = dgvRecipes.Rows[e.RowIndex].Cells["ingredients"].Value.ToString();
                txtSteps.Text = dgvRecipes.Rows[e.RowIndex].Cells["steps"].Value.ToString();
                txtCategory.Text = dgvRecipes.Rows[e.RowIndex].Cells["category"].Value.ToString();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            if (dgvRecipes.SelectedRows.Count == 0)
            {
                MessageBox.Show("Please select a recipe first!");
                return;
            }

            string name = dgvRecipes.SelectedRows[0].Cells["recipe_name"].Value?.ToString() ?? "";
            string ingredients = dgvRecipes.SelectedRows[0].Cells["ingredients"].Value?.ToString() ?? "";
            string steps = dgvRecipes.SelectedRows[0].Cells["steps"].Value?.ToString() ?? "";
            string category = dgvRecipes.SelectedRows[0].Cells["category"].Value?.ToString() ?? "";

            ViewRecipeForm viewForm = new ViewRecipeForm(name, ingredients, steps, category);
            viewForm.ShowDialog();
        }

        
    }
}
