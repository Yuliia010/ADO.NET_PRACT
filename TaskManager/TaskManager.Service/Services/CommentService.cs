using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;
using TaskManager.DAL;
using TaskManager.Service.Repositories;

namespace TaskManager.Service.Services
{
    public class CommentService
    {

        static private TaskManagerContext _context = new TaskManagerContext();
        static private Repository<Comment> _repository = new Repository<Comment>(_context);
        static private CommentProvider _commentProvider = new CommentProvider(_repository);
       
        static public List<Comment> GetAll(Ticket ticket)
        {
            List <Comment> list = new List <Comment>();

            foreach(var comment in _commentProvider.GetComments().ToList())
            {
                if(comment.TicketId == ticket.Id)
                {
                    list.Add(comment);
                }
            }

            return list;
        }

        static public void Add(Comment comment)
        {

            _commentProvider.Add(comment);
        }
    }
}
