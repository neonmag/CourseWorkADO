using CourseWork.Entity;
using CourseWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace CourseWork.CRUD
{
    /// <summary>
    /// Interaction logic for CrudSales.xaml
    /// </summary>
    public partial class CrudSales : Window
    {
        public Entity.Sales? sale { get; set; }
        public ObservableCollection<Entity.Seller> Sellers { get; set; }
        public ObservableCollection<Entity.Users> Users { get; set; }
        public ObservableCollection<Entity.Product> Products { get; set; }
        public CrudSales()
        {
            InitializeComponent();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Equals(String.Empty))
            {
                MessageBox.Show("Name is empty");
                Name.Focus();
                return;
            }
            if (BuyerName.SelectedItem is null)
            {
                MessageBox.Show("Price is empty");
                BuyerName.Focus();
                return;
            }
            if (ProductName.SelectedItem is null)
            {
                MessageBox.Show("Amount is empty");
                ProductName.Focus();
                return;
            }
            if (SellerName.SelectedItem is null)
            {
                MessageBox.Show("Seller Name is empty");
                SellerName.Focus();
                return;
            }
            this.sale.Product = (ProductName.SelectedItem as Entity.Product).Id;
            this.sale.Name = Name.Text;
            this.sale.Buyer = (BuyerName.SelectedItem as Entity.Users).Id;
            this.sale.Seller = (SellerName.SelectedItem as Entity.Seller).Id;
            this.DialogResult = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            sale = null;
            this.DialogResult = true;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Owner is CourseWork.MainWindow owner)
            {
                DataContext = owner;
                this.Sellers = owner.sellers;
                this.Users = owner.users;
                this.Products = owner.products;
            }
            if (sale is null)
            {
                Delete.IsEnabled = false;
                sale = new Sales { Id = Guid.NewGuid() };
            }
            else
            {
                Name.Text = sale.Name;
                Delete.IsEnabled = true;
                SellerName.SelectedItem = Sellers
                    .Where(s => s.Id == this.sale.Seller)
                    .FirstOrDefault();

                BuyerName.SelectedItem = Users
                    .Where(s => s.Id == this.sale.Buyer)
                    .FirstOrDefault();

                ProductName.SelectedItem = Products
                    .Where(s => s.Id == this.sale.Product)
                    .FirstOrDefault();
            }
            Id.Text = sale.Id.ToString();
            SellerName.ItemsSource = Sellers;
            BuyerName.ItemsSource = Users;
            ProductName.ItemsSource = Products;
        }
    }
}
