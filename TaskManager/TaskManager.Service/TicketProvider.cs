using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;

namespace TaskManager.Service
{
    internal class TicketProvider
    {
        private readonly IRepository<Ticket> _taskRepository;


        internal TicketProvider(IRepository<Ticket> repository)
        {
            _taskRepository = repository;
        }

        internal void AddTickets(List<Ticket> tasks)
        {
            foreach (var task in tasks)
            {
                AddTicket(task);
            }
        }

        internal void AddTicket(Ticket task)
        {
            _taskRepository.Add(task);
        }

        internal Ticket GetTicket(int id)
        {
            return _taskRepository.Get(id);
        }

        internal IEnumerable<Ticket> GetTasks()
        {
            return _taskRepository.GetAll();
        }

        internal void RemoveTicket(int id)
        {
            var ticket = GetTicket(id);
            _taskRepository.Remove(ticket);
        }
    }
}