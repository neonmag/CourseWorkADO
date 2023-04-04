using CourseWork.Entity;
using System;
using System.Collections.Generic;
using System.Globalization;
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
using System.Xml.Linq;

namespace CourseWork.CRUD
{
    /// <summary>
    /// Interaction logic for CrudSellers.xaml
    /// </summary>
    public partial class CrudSellers : Window
    {
        public Entity.Seller Seller { get; set; }

        public CrudSellers()
        {
            InitializeComponent();
            Seller = null!;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (Seller is null)  // режим додавання (Create)
            {
                Delete.IsEnabled = false;
                Seller = new Seller { Id = Guid.NewGuid() };
            }
            else  // режими редагування чи видалення (U / D)
            {
                Name.Text = Seller.Name;
                Surname.Text = Seller.Surname;
                Phone.Text = Seller.Phone;
                Delete.IsEnabled = true;
            }
            Id.Text = Seller.Id.ToString();
        }

        private void Add_Click(object sender, RoutedEventArgs e)
        {
            if (Name.Text.Equals(String.Empty))
            {
                MessageBox.Show("Name is empty");
                Name.Focus();
                return;
            }
            if (Surname.Text.Equals(String.Empty))
            {
                MessageBox.Show("Surname is empty");
                Surname.Focus();
                return;
            }
            if (Phone.Text.Equals(String.Empty))
            {
                MessageBox.Show("Phone is empty");
                Phone.Focus();
                return;
            }
            this.Seller.Surname = Surname.Text;
            this.Seller.Name = Name.Text;
            this.Seller.Phone = Phone.Text;
            this.DialogResult = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            Seller = null;
            this.DialogResult = true;
        }
    }
}
