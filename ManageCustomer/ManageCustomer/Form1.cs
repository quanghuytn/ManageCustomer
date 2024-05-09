using Microsoft.EntityFrameworkCore.Diagnostics;
using ManageCustomer.Models;

namespace ManageCustomer
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            LoadData();
        }

        private void LoadData()
        {
            using (MySaleDBContext context = new MySaleDBContext())
            {
                // lấy dữ liệu từ bảng product
                //C1 - LinQ
                // var data = context.Products.Select(p=>new
                // {
                //     ID = p.ProductId,
                //     Name = p.ProductName,
                //     Price = p.UnitPrice,
                //     Stock = p.UnitsInStock,
                //     Image = p.Image,
                //     CategoryName = p.Category.CategoryName
                // }).OrderBy(p=> p.Price)
                //.ThenByDescending(p=>p.Stock)
                // .ToList();

                //C2 - LinQ
                var data = (from p in context.Products
                                //where p.ProductName.Contains(txtName.Text)
                            orderby p.UnitPrice, p.UnitsInStock descending
                            select new
                            {
                                ID = p.ProductId,
                                Name = p.ProductName,
                                Price = p.UnitPrice,
                                Stock = p.UnitsInStock,
                                Image = p.Image,
                                CategoryName = p.Category.CategoryName
                            }).ToList();

                // add vào dgProduct
                dgProduct.DataSource = data;

                // lấy dữ liệu từ bảng category
                var data2 = context.Categories.ToList();

                cboCategory.DataSource = data2;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

                //binding dữ liệu lên các text
                TxtId.DataBindings.Clear();
                TxtId.DataBindings.Add("Text", data, "ID");

                txtName.DataBindings.Clear();
                txtName.DataBindings.Add("Text", data, "Name");

                txtPrice.DataBindings.Clear();
                txtPrice.DataBindings.Add("Text", data, "Price");

                txtStock.DataBindings.Clear();
                txtStock.DataBindings.Add("Text", data, "Stock");

                txtImage.DataBindings.Clear();
                txtImage.DataBindings.Add("Text", data, "Image");

                cboCategory.DataBindings.Clear();
                cboCategory.DataBindings.Add("Text", data, "CategoryName");
            }
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (MySaleDBContext context = new MySaleDBContext())
            {
                //tao doi tuong moi
                Product p = new Product()
                {
                    ProductName = txtName.Text,
                    UnitPrice = Decimal.Parse(txtPrice.Text),
                    UnitsInStock = Int32.Parse(txtStock.Text),
                    Image = txtImage.Text,
                    CategoryId = (int)cboCategory.SelectedValue
                };

                // add vao db
                context.Products.Add(p);
                if (context.SaveChanges() > 0)
                {
                    MessageBox.Show("Add success");
                }
                else
                {
                    MessageBox.Show("Add fail");
                }
                LoadData();
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            using (MySaleDBContext context = new MySaleDBContext())
            {
                //tao doi tuong moi
                Product p = context.Products.FirstOrDefault(p => p.ProductId == Int32.Parse(TxtId.Text));
                if (p != null)
                {
                    //update
                    p.ProductName = txtName.Text;
                    p.UnitPrice = Decimal.Parse(txtPrice.Text);
                    p.UnitsInStock = Int32.Parse(txtStock.Text);
                    p.Image = txtImage.Text;
                    p.CategoryId = (int)cboCategory.SelectedValue;
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Update success");
                    }
                    else
                    {
                        MessageBox.Show("Update fail");
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }
                LoadData();

            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            using (MySaleDBContext context = new MySaleDBContext())
            {
                //tao doi tuong moi
                Product p = context.Products.FirstOrDefault(p => p.ProductId == Int32.Parse(TxtId.Text));
                if (p != null)
                {
                    //update
                    context.Products.Remove(p);
                    if (context.SaveChanges() > 0)
                    {
                        MessageBox.Show("Delete success");
                    }
                    else
                    {
                        MessageBox.Show("Delete fail");
                    }
                }
                else
                {
                    MessageBox.Show("Not found");
                }
                LoadData();

            }
        }

        private void btnSearch_Click(object sender, EventArgs e)
        {
            using (MySaleDBContext context = new MySaleDBContext())
            {
                //C2 - LinQ
                var data = (from p in context.Products
                            where p.ProductName.Contains(txtName.Text)
                            orderby p.UnitPrice, p.UnitsInStock descending
                            select new
                            {
                                ID = p.ProductId,
                                Name = p.ProductName,
                                Price = p.UnitPrice,
                                Stock = p.UnitsInStock,
                                Image = p.Image,
                                CategoryName = p.Category.CategoryName
                            }).ToList();

                // add vào dgProduct
                dgProduct.DataSource = data;

                // lấy dữ liệu từ bảng category
                var data2 = context.Categories.ToList();

                cboCategory.DataSource = data2;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

                //binding dữ liệu lên các text
                TxtId.DataBindings.Clear();
                TxtId.DataBindings.Add("Text", data, "ID");

                txtName.DataBindings.Clear();
                txtName.DataBindings.Add("Text", data, "Name");

                txtPrice.DataBindings.Clear();
                txtPrice.DataBindings.Add("Text", data, "Price");

                txtStock.DataBindings.Clear();
                txtStock.DataBindings.Add("Text", data, "Stock");

                txtImage.DataBindings.Clear();
                txtImage.DataBindings.Add("Text", data, "Image");

                cboCategory.DataBindings.Clear();
                cboCategory.DataBindings.Add("Text", data, "CategoryName");
            }
        }

        private void btnSearch2_Click(object sender, EventArgs e)
        {
            using (MySaleDBContext context = new MySaleDBContext())
            {
                //C2 - LinQ
                var data = (from p in context.Products
                            where p.ProductName.Contains(txtName.Text) && p.Image.Contains(txtImage.Text)
                            orderby p.UnitPrice, p.UnitsInStock descending
                            select new
                            {
                                ID = p.ProductId,
                                Name = p.ProductName,
                                Price = p.UnitPrice,
                                Stock = p.UnitsInStock,
                                Image = p.Image,
                                CategoryName = p.Category.CategoryName
                            }).ToList();

                // add vào dgProduct
                dgProduct.DataSource = data;

                // lấy dữ liệu từ bảng category
                var data2 = context.Categories.ToList();

                cboCategory.DataSource = data2;
                cboCategory.DisplayMember = "CategoryName";
                cboCategory.ValueMember = "CategoryId";

                //binding dữ liệu lên các text
                TxtId.DataBindings.Clear();
                TxtId.DataBindings.Add("Text", data, "ID");

                txtName.DataBindings.Clear();
                txtName.DataBindings.Add("Text", data, "Name");

                txtPrice.DataBindings.Clear();
                txtPrice.DataBindings.Add("Text", data, "Price");

                txtStock.DataBindings.Clear();
                txtStock.DataBindings.Add("Text", data, "Stock");

                txtImage.DataBindings.Clear();
                txtImage.DataBindings.Add("Text", data, "Image");

                cboCategory.DataBindings.Clear();
                cboCategory.DataBindings.Add("Text", data, "CategoryName");
            }
        }
    }
}