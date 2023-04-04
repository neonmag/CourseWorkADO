using CourseWork.Entity;
using System;
using System.Collections.Generic;
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
    /// Interaction logic for CrudUsers.xaml
    /// </summary>
    public partial class CrudUsers : Window
    {
        public Entity.Users User { get; set; }
        public CrudUsers()
        {
            InitializeComponent();
            User = null!;
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            if (User is null)
            {
                Delete.IsEnabled = false;
                User = new Users { Id = Guid.NewGuid() };
            }
            else
            {
                Name.Text = User.Name;
                Surname.Text = User.Surname;
                Delete.IsEnabled = true;
            }
            Id.Text = User.Id.ToString();
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
            this.User.Surname = Surname.Text;
            this.User.Name = Name.Text;
            this.DialogResult = true;
        }

        private void Delete_Click(object sender, RoutedEventArgs e)
        {
            User = null;
            this.DialogResult = true;
        }
    }
}
