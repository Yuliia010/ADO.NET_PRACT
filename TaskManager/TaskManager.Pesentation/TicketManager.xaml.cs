using Microsoft.VisualBasic;
using System;
using System.Collections.Generic;
using System.ComponentModel;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using TaskManager.DAL.Entity;
using TaskManager.Pesentation;
using TaskManager.Presentation;
using TaskManager.Service.Repositories;

namespace TaskManager.App
{
    public class TicketItem
    {
        public int Id { get; set; }
        public string Summary { get; set; }
        public string Description { get; set; }
        public string Assigned { get; set; }
        public int Priority { get; set; }
        public DateTime DueDate { get; set; }
        public string DueDateString => DueDate.ToShortDateString();
    }
    public partial class TicketManager : Window
    {
        private LogIn logIn;

        public string UserName { get; set; }
        private User currentUser = new User();
        public TicketManager(User user, LogIn logIn)
        {
            InitializeComponent();
            DefaultSet(user);
            this.logIn = logIn;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
        }

        private void DefaultSet(User user)
        {
            currentUser = user;
            DataContext = this;
            UserName = currentUser.Name;

            ShowAllData();
            AdminMode();

        }

        private void AdminMode()
        {
            if(!currentUser.IsAdmin)
            {
                Btn_UsersManager.Visibility = Visibility.Hidden;
            }
        }
        private void ShowAllData()
        {
            var ticketItems = new List<TicketItem>();
            
            foreach (var ticket in TicketService.GetAllTickets())
            {
                var ticketItem = new TicketItem
                {
                    Id = ticket.Id,
                    Summary = ticket.Summary,
                    Description = ticket.Description,
                    Assigned = UsersService.GetUserName(ticket.UserId),
                    Priority = ticket.Priority,
                    DueDate = ticket.DueDateTime,
                };

                ticketItems.Add(ticketItem);
            }

            TicketListView.ItemsSource = ticketItems;
        }

        private void Btn_UsersManager_Click(object sender, RoutedEventArgs e)
        {
            UserManager userManager = new UserManager(this);
            userManager.Show();
            this.Hide();
        }
        private void Window_Closed(object sender, EventArgs e)
        {
            if (logIn != null)
            {
                logIn.Show();
                this.Close();
            }
        }

        private async void Btn_Close_Click(object sender, RoutedEventArgs e)
        {
            if (TicketListView.SelectedItem != null)
            {
                int id = (TicketListView.SelectedItem as TicketItem).Id;

                await Task.Run(() => ArchiveService.IntoArchive(id));
                TicketService.RemoveTicket(id);
                ShowAllData();
            }
        }

        public void ReturnedTicket(Ticket editedTicket)
        {
            if (editedTicket != null)
            {
                var existingTicket = TicketService.GetTicket(editedTicket.Id);
                
                    if (existingTicket != null)
                    {
                        existingTicket.Summary = editedTicket.Summary;
                        existingTicket.Description = editedTicket.Description;
                        existingTicket.Priority = editedTicket.Priority;
                        existingTicket.DueDateTime = editedTicket.DueDateTime;
                        existingTicket.UserId = editedTicket.UserId;
                        existingTicket.User = editedTicket.User;

                        TicketService.Update(existingTicket);
                    }
                    else
                    {
                        TicketService.Add(editedTicket);
                    }
                
                

                ShowAllData();
            }

            this.Show();
        }

        private void Btn_NewTicket_Click(object sender, RoutedEventArgs e)
        {
            
            TicketCreate ticketCreate = new TicketCreate(this, null);
            ticketCreate.Show();
            this.Hide();
        }

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            if (TicketListView.SelectedItem != null)
            {
                int id = (TicketListView.SelectedItem as TicketItem).Id;

                TicketCreate ticketCreate = new TicketCreate(this, TicketService.GetTicket(id));
                ticketCreate.Show();
                this.Hide();
            }
                
        }

        private void BtnArchive_Click(object sender, RoutedEventArgs e)
        {
            Archive archive = new Archive(this);
            archive.Show();
            this.Hide();
        }

      
    }
}

    


