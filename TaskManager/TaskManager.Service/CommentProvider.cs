using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;

namespace TaskManager.Service
{
    internal class CommentProvider
    {
        private readonly IRepository<Comment> _commentRepository;
        internal CommentProvider(IRepository<Comment> repository)
        {
            _commentRepository = repository;
        }
        internal Comment GetComment(int id)
        {
            return _commentRepository.Get(id);
        }

        internal IEnumerable<Comment> GetComments()
        {
            return _commentRepository.GetAll();
        }

        internal void Add(Comment comment)
        {
            _commentRepository.Add(comment);
        }
    }
}
