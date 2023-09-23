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
using TaskManager.Presentation;
using TaskManager.Service;
using TaskManager.Service.Repositories;

namespace TaskManager.Pesentation
{
    /// <summary>
    /// Interaction logic for LogIn.xaml
    /// </summary>
    public partial class LogIn : Window
    {
        UsersService usersRepository = new UsersService();
       
        public LogIn()
        {
            InitializeComponent();
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }
        
        private void Btn_LogIn_Click(object sender, RoutedEventArgs e)
        {
            string login = TxtBox_UserName.Text.Trim();
            string password = PassBox_Password.Password;

            //MessageBox.Show($"Its user {login}, {password}");
            var user = usersRepository.GetUser(login);

            if (user != null)
            {
                if(usersRepository.CheckPassword(user, password))
                {
                    MessageBox.Show("Login success!");

                    TicketManager ticketManager = new TicketManager(user, this);
                    ticketManager.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect input!");
                }

            }
            
        }

        private void Btn_Cancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
