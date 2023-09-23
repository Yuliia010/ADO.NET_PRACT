using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;
using TaskManager.DAL;
using System.Security.Principal;
using Microsoft.EntityFrameworkCore;

namespace TaskManager.Service.Repositories
{
    
    public class TicketService
    {
        static TaskManagerContext _context = new TaskManagerContext();
        static Repository<Ticket> _repository = new Repository<Ticket>(_context);
        static TicketProvider _provider = new TicketProvider(_repository);

        public List<Ticket> GetAllTickets()
        {
            var tickets = _provider.GetTasks().ToList();

            return tickets;
        }
        public Ticket GetTicket(int id)
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
        public void Add(Ticket ticket)
        {


            _provider.AddTicket(ticket);
        }

        public void Update(Ticket ticket)
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

        public void RemoveTicket(int id)
        {
            _provider.RemoveTicket(id);
        }

    }
}
