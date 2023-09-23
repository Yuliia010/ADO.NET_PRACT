using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;

namespace TaskManager.Service
{

    internal class ArchiveProvider
    {
        private readonly IRepository<TicketArchive> _archiveRepository;
        internal ArchiveProvider(IRepository<TicketArchive> repository)
        {
            _archiveRepository = repository;
        }
        internal TicketArchive GetTicket(int id)
        {
            return _archiveRepository.Get(id);
        }

        internal IEnumerable<TicketArchive> GetTickets()
        {
            return _archiveRepository.GetAll();
        }

        internal void Add(TicketArchive ticket)
        {
            _archiveRepository.Add(ticket);
        }
    }
}
