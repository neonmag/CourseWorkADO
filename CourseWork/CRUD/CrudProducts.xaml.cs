using CourseWork.Entity;
using CourseWork;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.SqlClient;
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
    /// Interaction logic for CrudProducts.xaml
    /// </summary>
    public partial class CrudProducts : Window
    {
        public Entity.Product? Product { get; set; }
        public ObservableCollection<Entity.Seller> Sellers { get; set; }
        public CrudProducts()
        {
            InitializeComponent();
            Product = null!;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Owner is CourseWork.MainWindow owner)
            {
                DataContext = owner;
                this.Sellers = owner.sellers;
            }
            if (Product is null)
            {
                Delete.IsEnabled = false;
                Product = new Product { Id = Guid.NewGuid() };
            }
            else 
            {
                Name.Text = Product.Name;
                Amount.Text = Product.Amount.ToString();
                Price.Text = Product.Price.ToString();
                Delete.IsEnabled = true;
                SellerName.SelectedItem = Sellers
                    .Where(s => s.Id == this.Product.Seller)
                    .FirstOrDefault();
            }
            Id.Text = Product.Id.ToString();
            SellerName.ItemsSource = Sellers;
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Equals(String.Empty))
            {
                MessageBox.Show("Name is empty");
                Name.Focus();
                return;
            }
            if (Price.Text.Equals(String.Empty))
            {
                MessageBox.Show("Price is empty");
                Price.Focus();
                return;
            }
            if (Amount.Text.Equals(String.Empty))
            {
                MessageBox.Show("Amount is empty");
                Amount.Focus();
                return;
            }
            if(SellerName.SelectedItem is null)
            {
                MessageBox.Show("Seller Name is empty");
                SellerName.Focus();
                return;
            }
            this.Product.Amount = Convert.ToInt32(Amount.Text);
            this.Product.Name = Name.Text;
            this.Product.Price = Convert.ToInt32(Price.Text);
            this.Product.Seller = (SellerName.SelectedItem as Entity.Seller).Id;
            this.DialogResult = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Product = null;
            this.DialogResult = true;
        }
    }
}
