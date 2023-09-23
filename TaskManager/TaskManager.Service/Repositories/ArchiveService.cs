using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL;
using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;

namespace TaskManager.Service.Repositories
{
    public class ArchiveService
    {
        TicketService ticketService = new TicketService();
        UsersService userService = new UsersService();
        static TaskManagerContext _context = new TaskManagerContext();
        static Repository<TicketArchive> _repository = new Repository<TicketArchive>(_context);
        ArchiveProvider archiveProvider = new ArchiveProvider(_repository);
        public void IntoArchive(int id)
        {
            var ticket = ticketService.GetTicket(id);
            var user =  userService.GetUser(ticket.UserId);

            TicketArchive ticketArchive = new TicketArchive();

            ticketArchive.Summary = ticket.Summary;
            ticketArchive.Description = ticket.Description;
            ticketArchive.Priority = ticket.Priority;
            ticketArchive.DueDateTime = ticket.DueDateTime.ToString();
            ticketArchive.UserName = user.Name;

            archiveProvider.Add(ticketArchive);


        }

        public List<TicketArchive> GetAll()
        {
           return  archiveProvider.GetTickets().ToList();
        }
    }
}
