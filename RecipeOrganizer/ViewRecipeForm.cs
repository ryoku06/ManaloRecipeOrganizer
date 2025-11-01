using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace RecipeOrganizer
{
    public partial class ViewRecipeForm : Form
    {
        public ViewRecipeForm()
        {
            InitializeComponent();
        }
        public ViewRecipeForm(string name, string ingredients, string steps, string category)
        {
            InitializeComponent();

            lblName.Text = "🍽️ " + name;
            lblCategory.Text = "Category: " + category;

            rtbIngredients.Text = ingredients;
            rtbSteps.Text = steps;
        }

        private void ViewRecipeForm_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
