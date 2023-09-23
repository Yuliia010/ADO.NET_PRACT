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
using System.Xml.Linq;
using TaskManager.App;
using TaskManager.DAL.Entity;
using TaskManager.Service.Repositories;

namespace TaskManager.Presentation
{
    /// <summary>
    /// Interaction logic for TicketCreate.xaml
    /// </summary>
    public partial class TicketCreate : Window
    {
        private TicketManager ticketManager;
        Ticket editTicket;

        private UsersService uService = new UsersService();
        List<User> UsersList => uService.GetAllUsers();

        public TicketCreate(TicketManager ticketManager, Ticket ticket)
        {
            InitializeComponent();
            this.ticketManager = ticketManager;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DefaultSet();

            if (ticket != null)
            {
                editTicket = ticket;
                FillForm();
               
            }
            else
            {
                editTicket = new Ticket();
            }


        }

        public void DefaultSet()
        {
            List<string> userNames = new List<string>();

            foreach (var user in UsersList)
            {
                userNames.Add(user.Name);
            }

            CBox_Assigned.ItemsSource = userNames;

            List<int> priorities = new List<int>();

            for(int i = 1;i<=5;i++)
            {
                priorities.Add(i);
            }
            CBox_Priority.ItemsSource = priorities;

           
        }
        private void FillForm()
        {
            txtSummary.Text = editTicket.Summary;
            txtDescription.Text = editTicket.Description;
            CBox_Priority.Text = editTicket.Priority.ToString();
            datePicker_DueTime.Value = editTicket.DueDateTime;
            CBox_Assigned.Text = uService.GetUserName(editTicket.UserId);
        }

       
        private void Window_Closed(object sender, EventArgs e)
        {
            if (ticketManager != null)
            {
                ticketManager.Show();
                this.Close();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {

            editTicket.Summary = txtSummary.Text;
            editTicket.Description = txtDescription.Text;
            editTicket.Priority = int.Parse(CBox_Priority.Text);
            if (datePicker_DueTime.Value.HasValue)
            {
                editTicket.DueDateTime = datePicker_DueTime.Value.Value;
            }
            else
            {
                editTicket.DueDateTime = DateTime.Now.AddDays(7);
            }
            editTicket.UserId = uService.GetUserIdFromName(CBox_Assigned.Text);
         

            ticketManager.ReturnedTicket(editTicket);
            this.Close();

        }

        private void BtnClose_Click(object sender, RoutedEventArgs e)
        {
            if (ticketManager != null)
            {
                ticketManager.Show();
                this.Close();
            }
        }
    }
}
