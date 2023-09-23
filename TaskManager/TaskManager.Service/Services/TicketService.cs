using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;
using TaskManager.DAL;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Service.Repositories
{
    
    public class TicketService
    {
        static private TaskManagerContext _context = new TaskManagerContext();
        static private Repository<Ticket> _repository = new Repository<Ticket>(_context);
        static private TicketProvider _provider = new TicketProvider(_repository);

        static  public List<Ticket> GetAllTickets()
        {
            var tickets = _provider.GetTasks().ToList();

            return tickets;
        }
        static public Ticket GetTicket(int id)
        {
            var tickets = GetAllTickets();
            foreach (var ticket in tickets)
            {
                if (ticket.Id == id)
                {
                    return ticket;
                }
            }
            return null;
        }
        static public void Add(Ticket ticket)
        {


            _provider.AddTicket(ticket);
        }

        static public void Update(Ticket ticket)
        {
           
            var existingTicket = _context.Tickets.FirstOrDefault(t => t.Id == ticket.Id);

            if (existingTicket != null)
            {
                
                existingTicket.Summary = ticket.Summary;
                existingTicket.Description = ticket.Description;
                existingTicket.Priority = ticket.Priority;
                existingTicket.DueDateTime = ticket.DueDateTime;
                existingTicket.UserId = ticket.UserId;

               
                _context.SaveChanges();
            }
            else
            {
                
                throw new ArgumentException("Ticket wasnt found!");
            }
        }

        static public void RemoveTicket(int id)
        {
            _provider.RemoveTicket(id);
        }

    }
}
