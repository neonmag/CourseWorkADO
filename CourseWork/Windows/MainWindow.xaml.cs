using CourseWork.Entity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
using System.Linq;
using System.Reflection.Metadata;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;
using System.Data.Common;
using CourseWork.CRUD;

namespace CourseWork
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public ObservableCollection<Entity.Seller> sellers { get; set; }
        public ObservableCollection<Entity.Product> products { get; set; }
        public ObservableCollection<Entity.Users> users { get; set; }
        public ObservableCollection<Entity.Sales> sales { get; set; }
        public SqlConnection _connection;
        public CrudProducts crudProducts;
        public CrudSellers crudSellers;
        public MainWindow()
        {
            InitializeComponent();
            _connection = new();
            sellers = new();
            products = new();
            users = new();
            sales = new();
            DataContext = this;
            _connection.ConnectionString = App.ConnectionString;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            try
            {
                _connection.Open();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            LoadSellers();
            LoadProducts();
            LoadUsers();
            LoadSales();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (_connection?.State == System.Data.ConnectionState.Open)
            {
                _connection.Close();
            }
        }
        private void LoadUsers()
        {
            SqlCommand cmd = new() { Connection = _connection };
            try
            {
                cmd.CommandText = "SELECT U.* FROM Users U WHERE DeleteDt IS NULL";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    users.Add(new Entity.Users(reader));
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Window will be closed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                this.Close();
            }
        }
        private void LoadSellers()
        {
            SqlCommand cmd = new() { Connection = _connection };
            try
            {
                cmd.CommandText = "SELECT S.* FROM Seller S WHERE DeleteDt IS NULL";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sellers.Add(new Entity.Seller(reader));
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Window will be closed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                this.Close();
            }
        }
        private void LoadSales()
        {
            SqlCommand cmd = new() { Connection = _connection };
            try
            {
                cmd.CommandText = "SELECT S.* FROM Sales S WHERE DeleteDt IS NULL";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    sales.Add(new Entity.Sales(reader));
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Window will be closed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                this.Close();
            }
        }
        private void LoadProducts()
        {
            SqlCommand cmd = new() { Connection = _connection };
            try
            {
                cmd.CommandText = "SELECT P.* FROM Products P WHERE DeleteDt IS NULL";
                var reader = cmd.ExecuteReader();
                while (reader.Read())
                {
                    products.Add(new Entity.Product(reader));
                }
                reader.Close();
                cmd.Dispose();
            }
            catch (SqlException ex)
            {
                MessageBox.Show(
                    ex.Message,
                    "Window will be closed",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
                this.Close();
            }
        }
        private void AddProduct_Click(object sender, RoutedEventArgs e)
        {
            CrudProducts dialog = new();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                if(dialog.Product is not null)
                {
                    String sql = "INSERT INTO Products(Id,Name,Price,Amount,Seller) VALUES (@id,@name,@amount,@price,@seller)";
                    using SqlCommand cmd = new(sql, _connection);
                    cmd.Parameters.AddWithValue("@id", dialog.Product.Id);
                    cmd.Parameters.AddWithValue("@name", dialog.Product.Name);
                    cmd.Parameters.AddWithValue("@amount", dialog.Product.Price);
                    cmd.Parameters.AddWithValue("@price", dialog.Product.Amount);
                    cmd.Parameters.AddWithValue("@seller", dialog.Product.Seller);
                    try
                    {
                        cmd.ExecuteNonQuery();
                        MessageBox.Show("Insert OK");
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        private void AddSeller_Click(object sender, RoutedEventArgs e)
        {
            CrudSellers dialog = new();
            if (dialog.ShowDialog() == true)
            {
                String sql = "INSERT INTO Seller(Id,Name,Surname,Phone) VALUES (@id,@name,@surname,@phone)";
                using SqlCommand cmd = new(sql, _connection);
                cmd.Parameters.AddWithValue("@id", dialog.Seller.Id);
                cmd.Parameters.AddWithValue("@name", dialog.Seller.Name);
                cmd.Parameters.AddWithValue("@surname", dialog.Seller.Surname);
                cmd.Parameters.AddWithValue("@phone", dialog.Seller.Phone);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert OK");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void AddUser_Click(object sender, RoutedEventArgs e)
        {
            CrudUsers dialog = new();
            if (dialog.ShowDialog() == true)
            {
                String sql = "INSERT INTO Users(Id,Name,Surname) VALUES (@id,@name,@surname)";
                using SqlCommand cmd = new(sql, _connection);
                cmd.Parameters.AddWithValue("@id", dialog.User.Id);
                cmd.Parameters.AddWithValue("@name", dialog.User.Name);
                cmd.Parameters.AddWithValue("@surname", dialog.User.Surname);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert OK");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }
        private void ViewProducts_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView item)
            {
                if (item.SelectedItem is Entity.Product product)
                {
                    CrudProducts dialog = new()
                    {
                        Owner = this,
                        Product = product
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        if (dialog.Product is null)
                        {
                            string command =
                                @"UPDATE Products
                                  SET DeleteDt = CURRENT_TIMESTAMP
                                  WHERE Id = @id; ";
                            using SqlCommand cmd = new(command, _connection);
                            cmd.Parameters.AddWithValue("@id", product.Id);
                            ExecuteCommand(cmd, $"Delete: {product.Name} {product.Seller}");
                            products.Clear();
                            LoadProducts();
                        }
                        else
                        {
                            string command =
                                @"UPDATE Products 
                                SET 
                                Name = @name,
                                Seller = @seller, 
                                Amount = @amount, 
                                Price = @price 
                                WHERE Id = @id;";

                            using SqlCommand cmd = new(command, _connection);
                            cmd.Parameters.AddWithValue("@id", product.Id);
                            cmd.Parameters.AddWithValue("@name", product.Name);
                            cmd.Parameters.AddWithValue("@price", product.Price);
                            cmd.Parameters.AddWithValue("@amount", product.Amount);
                            cmd.Parameters.AddWithValue("@seller", product.Seller);
                            ExecuteCommand(cmd, "Update Product");
                            products.Clear();
                            LoadProducts();
                        }
                    }

                }
            }
        }

        private void ExecuteCommand(SqlCommand command, string commandName)
        {
            try
            {
                command.ExecuteNonQuery();
                MessageBox.Show(
                    commandName + " successfully complete",
                    commandName,
                    MessageBoxButton.OK,
                    MessageBoxImage.Information);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            command.Dispose();
        }

        private void ViewSellers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView item)
            {
                if (item.SelectedItem is Entity.Seller seller)
                {
                    CrudSellers dialog = new()
                    {
                        Owner = this,
                        Seller = seller
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        if (dialog.Seller is null)
                        {
                            string command =
                                @"UPDATE Seller
                                  SET DeleteDt = CURRENT_TIMESTAMP
                                  WHERE Id = @id; ";
                            using SqlCommand cmd = new(command, _connection);
                            cmd.Parameters.AddWithValue("@id", seller.Id);
                            ExecuteCommand(cmd, $"Delete: {seller.Name}");
                            products.Clear();
                            LoadProducts();
                        }
                        else
                        {
                            string command =
                                @"UPDATE Seller 
                                SET 
                                Name = @name,
                                Surname = @surname, 
                                Phone = @phone 
                                WHERE Id = @id;";

                            using SqlCommand cmd = new(command, _connection);
                            cmd.Parameters.AddWithValue("@id", seller.Id);
                            cmd.Parameters.AddWithValue("@name", seller.Name);
                            cmd.Parameters.AddWithValue("@phone", seller.Phone);
                            cmd.Parameters.AddWithValue("@surname", seller.Surname);
                            ExecuteCommand(cmd, "Update Seller");
                            products.Clear();
                            LoadSellers();
                        }
                    }

                }
            }
        }

        private void ViewUsers_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView item)
            {
                if (item.SelectedItem is Entity.Users user)
                {
                    CrudUsers dialog = new()
                    {
                        Owner = this,
                        User = user
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        if (dialog.User is null)
                        {
                            string command =
                                @"UPDATE Users
                                  SET DeleteDt = CURRENT_TIMESTAMP
                                  WHERE Id = @id; ";
                            using SqlCommand cmd = new(command, _connection);
                            cmd.Parameters.AddWithValue("@id", user.Id);
                            ExecuteCommand(cmd, $"Delete: {user.Name}");
                            products.Clear();
                            LoadProducts();
                        }
                        else
                        {
                            string command =
                                @"UPDATE Users
                                SET 
                                Name = @name,
                                Surname = @surname 
                                WHERE Id = @id;";

                            using SqlCommand cmd = new(command, _connection);
                            cmd.Parameters.AddWithValue("@id", user.Id);
                            cmd.Parameters.AddWithValue("@name", user.Name);
                            cmd.Parameters.AddWithValue("@surname", user.Surname);
                            ExecuteCommand(cmd, "Update User");
                            products.Clear();
                            LoadUsers();
                        }
                    }

                }
            }
        }

        private void AddSale_Click(object sender, RoutedEventArgs e)
        {
            CrudSales dialog = new();
            dialog.Owner = this;
            if (dialog.ShowDialog() == true)
            {
                String sql = "INSERT INTO Sales(Id,Name,Id_Seller,Id_Buyer,Product) VALUES (@id,@name,@id_seller,@id_buyer,@product)";
                using SqlCommand cmd = new(sql, _connection);
                cmd.Parameters.AddWithValue("@id", dialog.sale.Id);
                cmd.Parameters.AddWithValue("@name", dialog.sale.Name);
                cmd.Parameters.AddWithValue("@id_seller", dialog.sale.Seller);
                cmd.Parameters.AddWithValue("@id_buyer", dialog.sale.Buyer);
                cmd.Parameters.AddWithValue("@product", dialog.sale.Product);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Insert OK");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void ViewSales_MouseDoubleClick(object sender, MouseButtonEventArgs e)
        {
            if (sender is ListView item)
            {
                if (item.SelectedItem is Entity.Sales sale)
                {
                    CrudSales dialog = new()
                    {
                        Owner = this,
                        sale = sale
                    };
                    if (dialog.ShowDialog() == true)
                    {
                        if (dialog.sale is null)
                        {
                            string command =
                                @"UPDATE Sales
                                  SET DeleteDt = CURRENT_TIMESTAMP
                                  WHERE Id = @id; ";
                            using SqlCommand cmd = new(command, _connection);
                            cmd.Parameters.AddWithValue("@id", sale.Id);
                            ExecuteCommand(cmd, $"Delete: {sale.Name}");
                            products.Clear();
                            LoadProducts();
                        }
                        else
                        {
                            string command =
                                @"UPDATE Sales
                                SET 
                                Name = @name,
                                Id_Seller = @seller, 
                                Id_Buyer = @buyer, 
                                Product = @product, 
                                WHERE Id = @id;";

                            using SqlCommand cmd = new(command, _connection);
                            cmd.Parameters.AddWithValue("@id", sale.Id);
                            cmd.Parameters.AddWithValue("@name", sale.Name);
                            cmd.Parameters.AddWithValue("@seller", sale.Seller);
                            cmd.Parameters.AddWithValue("@buyer", sale.Buyer);
                            cmd.Parameters.AddWithValue("@product", sale.Product);
                            ExecuteCommand(cmd, "Update Sale");
                            products.Clear();
                            LoadSales();
                        }
                    }

                }
            }
        }
    }
}
