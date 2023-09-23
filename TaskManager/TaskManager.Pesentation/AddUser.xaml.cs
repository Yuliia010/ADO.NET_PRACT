using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
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
using TaskManager.App;
using TaskManager.DAL.Entity;
using TaskManager.Presentation;

namespace TaskManager.Pesentation
{
    /// <summary>
    /// Interaction logic for SignUp.xaml
    /// </summary>
    public partial class AddUser : Window
    {
        private UserManager userManager;
        User editUser;
        public AddUser(UserManager userManager, User user)
        {
            this.userManager = userManager;
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;

            if (user != null)
            {
                editUser = user;
                FillForm();

            }
            else
            {
                editUser = new User();
            }
        }

        private void FillForm()
        {
            txtName.Text = editUser.Name;
            txtLogin.Text = editUser.login;
            passBox.Password = editUser.password;
            passBox.Visibility = Visibility.Visible;
            if (editUser.IsAdmin)
            {
                IsAdminBox.IsChecked = true;
            }
            
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (userManager != null)
            {
                userManager.Show();
                this.Close();
            }
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            if (userManager != null)
            {
                userManager.Show();
                this.Close();
            }
        }

        private void Btn_Save_Click(object sender, RoutedEventArgs e)
        {
            editUser.Name = txtName.Text;
            editUser.login = txtLogin.Text;
            editUser.password = passBox.Password;
            editUser.IsAdmin = IsAdminBox.IsChecked == true;

            userManager.ReturnUser(editUser);
            this.Close();
        }
    }
}
