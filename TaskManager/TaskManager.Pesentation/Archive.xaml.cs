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
    /// <summary>
    /// Interaction logic for Archive.xaml
    /// </summary>
    public partial class Archive : Window
    {
        private TicketManager ticketManager;
        List<TicketArchive> TicketList;
        public Archive(TicketManager ticketManager)
        {
            InitializeComponent();
            this.ticketManager = ticketManager;
            WindowStartupLocation = WindowStartupLocation.CenterScreen;
            DataContext = this;

            ShowAllData();

        }

        private void ShowAllData()
        {
            var ticketItems = new List<TicketArchive>();
            TicketList = ArchiveService.GetAll();
            foreach (var ticket in TicketList)
            {
                var ticketItem = new TicketArchive
                {
                    Id = ticket.Id,
                    Summary = ticket.Summary,
                    Description = ticket.Description,
                    UserName = ticket.UserName,
                    Priority = ticket.Priority,
                    DueDateTime = ticket.DueDateTime,
                };

                ticketItems.Add(ticketItem);
            }

            TicketListView.ItemsSource = ticketItems;
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            if (ticketManager != null)
            {
                ticketManager.Show();
                this.Close();
            }
        }
    }
}
