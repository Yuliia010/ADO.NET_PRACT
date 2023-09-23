using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;

namespace TaskManager.Service
{
   
    public class ArchiveProvider
    {
        private readonly IRepository<TicketArchive> _archiveRepository;
        public ArchiveProvider(IRepository<TicketArchive> repository)
        {
            _archiveRepository = repository;
        }
        public TicketArchive GetTicket(int id)
        {
            return _archiveRepository.Get(id);
        }

        public IEnumerable<TicketArchive> GetTickets()
        {
            return _archiveRepository.GetAll();
        }

        public void Add(TicketArchive ticket)
        {
            _archiveRepository.Add(ticket);
        }
    }
}
