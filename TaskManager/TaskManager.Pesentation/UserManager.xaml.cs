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
using TaskManager.App;
using TaskManager.DAL.Entity;
using TaskManager.Pesentation;
using TaskManager.Service.Repositories;

namespace TaskManager.Presentation
{
    public class UserItem
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Login { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
      
    }
    public partial class UserManager : Window
    {
        private TicketManager ticketManager;

        List<User> UsersList;

        public UserManager(TicketManager ticketManager)
        {
            InitializeComponent();
            DefaultSet();
            this.ticketManager = ticketManager;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void DefaultSet()
        {
            ShowAllData();
            
        }

        private void ShowAllData()
        {
            DataContext = this;
            UsersList = UsersService.GetAllUsers();

            var usersItems = new List<UserItem>();

            foreach (var user in UsersList)
            {
                var userItem = new UserItem
                {
                    Id = user.Id,
                    Name = user.Name,
                    Login = user.login,
                    Password = user.password,
                    IsAdmin = user.IsAdmin
                };

                usersItems.Add(userItem);
            }

            UserListView.ItemsSource = usersItems;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ticketManager != null)
            {
                ticketManager.Show();
                this.Close();
            }
        }

        private void Btn_Delete_Click(object sender, RoutedEventArgs e)
        {
            if(UserListView.SelectedItem != null)
            {
                UserItem selectedUserItem = UserListView.SelectedItem as UserItem;

                UsersService.DeleteUser(selectedUserItem.Id);

                ShowAllData();
            }
            else
            {
                MessageBox.Show("Select some item!");
            }

        }
        public void ReturnUser(User editedUser)
        {
            if (editedUser != null)
            {
                var existingUser = UsersService.GetUser(editedUser.login); 

                if (existingUser != null)
                {
                    existingUser.Name = editedUser.Name;
                    existingUser.login = editedUser.login;
                    existingUser.password = editedUser.password;
                    existingUser.IsAdmin = editedUser.IsAdmin;
                    existingUser.Tickets = editedUser.Tickets;

                    UsersService.Update(editedUser); 
                }
                else
                {
                    UsersService.Add(editedUser);
                }

                ShowAllData();
            }

            this.Show();


        }
        private void Btn_AddUser_Click(object sender, RoutedEventArgs e)
        {
            AddUser addUser = new AddUser(this, null);
            addUser.Show();
            this.Hide();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            string login = (UserListView.SelectedItem as UserItem).Login;

            AddUser addUser = new AddUser(this, UsersService.GetUser(login));
            addUser.Show();
            this.Hide();
        }
    }
}
