using Microsoft.VisualBasic;
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
using TaskManager.Service.Repositories;
using TaskManager.Service.Services;

namespace TaskManager.Presentation
{
    /// <summary>
    /// Interaction logic for CommentForm.xaml
    /// </summary>
    public partial class CommentForm : Window
    {
        private TicketManager ticketManager;
        Ticket currentTicket;
        string UserName { get; set; }
        public CommentForm(TicketManager ticketManager, Ticket ticket, string UserName)
        {
            InitializeComponent();
            DataContext = this;
            this.ticketManager = ticketManager;

            currentTicket  = ticket;
            this.UserName = UserName;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            ShowAllData();
        }


        private void ShowAllData()
        {
            var commentItems = new List<Comment>();

            foreach (var comment in CommentService.GetAll(currentTicket))
            {
                commentItems.Add(comment);
            }

            CommentListView.ItemsSource = commentItems;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ticketManager != null)
            {
                ticketManager.Show();
                this.Close();
            }
        }

        private void BtnNew_Click(object sender, RoutedEventArgs e)
        {
            string commentText = Interaction.InputBox("Enter your comment:", "Add comment");

            Comment newComment = new Comment();

            newComment.Info = commentText;
            newComment.UserName = this.UserName;
            newComment.TicketId = currentTicket.Id;
            newComment.Created = DateTime.Now;

            CommentService.Add(newComment);

            ShowAllData();
        }
    }
}
