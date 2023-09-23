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
        
        static private TaskManagerContext _context = new TaskManagerContext();
        static private Repository<TicketArchive> _repository = new Repository<TicketArchive>(_context);
        static private ArchiveProvider archiveProvider = new ArchiveProvider(_repository);
        static public void IntoArchive(int id)
        {
            var ticket = TicketService.GetTicket(id);
            var user = UsersService.GetUser(ticket.UserId);

            TicketArchive ticketArchive = new TicketArchive();

            ticketArchive.Summary = ticket.Summary;
            ticketArchive.Description = ticket.Description;
            ticketArchive.Priority = ticket.Priority;
            ticketArchive.DueDateTime = ticket.DueDateTime.ToString();
            ticketArchive.UserName = user.Name;

            archiveProvider.Add(ticketArchive);


        }

        static public List<TicketArchive> GetAll()
        {
           return  archiveProvider.GetTickets().ToList();
        }
    }
}
