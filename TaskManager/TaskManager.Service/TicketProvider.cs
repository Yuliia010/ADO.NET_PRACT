using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;

namespace TaskManager.Service
{
    public class TicketProvider
    {
        private readonly IRepository<Ticket> _taskRepository;


        public TicketProvider(IRepository<Ticket> repository)
        {
            _taskRepository = repository;
        }

        public void AddTickets(List<Ticket> tasks)
        {
            foreach (var task in tasks)
            {
                AddTicket(task);
            }
        }

        public void AddTicket(Ticket task)
        {
            _taskRepository.Add(task);
        }

        public Ticket GetTicket(int id)
        {
            return _taskRepository.Get(id);
        }

        public IEnumerable<Ticket> GetTasks()
        {
            return _taskRepository.GetAll();
        }

        public void RemoveTicket(int id)
        {
            var ticket = GetTicket(id);
            _taskRepository.Remove(ticket);
        }
    }
}