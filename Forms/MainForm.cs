using System;
using System.Drawing;
using System.Windows.Forms;
using ProductApp.Models;
using ProductApp.Services;
using ProductApp.Data;
using System.Collections.Generic;

namespace ProductApp
{
    public class MainForm : Form
    {
        private readonly ProductService _service;

        private TextBox txtName;
        private NumericUpDown numPrice;
        private NumericUpDown numStock;
        private Button btnAdd;

        private TextBox txtSearch;
        private Button btnSearch;

        private ListBox lstProducts;

        public MainForm()
        {
            _service = new ProductService(new InMemoryProductData());
            InitializeComponents();
        }

        private void InitializeComponents()
        {
            Text = "Product Inventory";
            Size = new Size(600, 400);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblName = new Label { Text = "Name:", Location = new Point(20, 20), AutoSize = true };
            txtName = new TextBox { Location = new Point(80, 20), Width = 150 };

            Label lblPrice = new Label { Text = "Price:", Location = new Point(20, 60), AutoSize = true };
            numPrice = new NumericUpDown
            {
                Location = new Point(80, 60),
                DecimalPlaces = 2,
                Maximum = 100000,
                Width = 150
            };

            Label lblStock = new Label { Text = "Stock:", Location = new Point(20, 100), AutoSize = true };
            numStock = new NumericUpDown
            {
                Location = new Point(80, 100),
                Maximum = 10000,
                Width = 150
            };

            btnAdd = new Button { Text = "Add Product", Location = new Point(80, 140), Width = 150 };
            btnAdd.Click += BtnAdd_Click;

            Label lblSearch = new Label { Text = "Search:", Location = new Point(300, 20), AutoSize = true };
            txtSearch = new TextBox { Location = new Point(360, 20), Width = 150 };

            btnSearch = new Button { Text = "Search", Location = new Point(360, 60), Width = 150 };
            btnSearch.Click += BtnSearch_Click;

            lstProducts = new ListBox { Location = new Point(20, 200), Width = 520, Height = 140 };

            Controls.AddRange(new Control[]
            {
                lblName, txtName,
                lblPrice, numPrice,
                lblStock, numStock,
                btnAdd,
                lblSearch, txtSearch, btnSearch,
                lstProducts
            });

            txtName.TabIndex = 0;
            txtName.Focus();
        }

        private void BtnAdd_Click(object sender, EventArgs e)
        {
            var name = txtName.Text.Trim();
            var price = numPrice.Value;
            var stock = (int)numStock.Value;

            if (string.IsNullOrWhiteSpace(name))
            {
                MessageBox.Show("Product name is required.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtName.Focus();
                return;
            }

            _service.AddProduct(name, price, stock);
            RefreshProductList(_service.GetAllProducts());

            txtName.Clear();
            numPrice.Value = 0;
            numStock.Value = 0;
            txtName.Focus();
        }

        private void BtnSearch_Click(object sender, EventArgs e)
        {
            var keyword = txtSearch.Text.Trim();
            var results = _service.SearchProduct(keyword);

            if (string.IsNullOrWhiteSpace(keyword))
            {
                MessageBox.Show("Please enter a search term.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                txtSearch.Focus();
                return;
            }

            RefreshProductList(results);
        }

        private void RefreshProductList(IEnumerable<Product> products)
        {
            lstProducts.Items.Clear();
            foreach (var p in products)
            {
                lstProducts.Items.Add($"{p.Name} | Price: {p.Price:C} | Stock: {p.Stock}");
            }
        }
    }
}
