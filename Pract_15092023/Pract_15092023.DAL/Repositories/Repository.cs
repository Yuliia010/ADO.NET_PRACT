using Microsoft.EntityFrameworkCore;
using Pract_15092023.DAL.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_15092023.DAL.Repositories
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly DbContext _context;
        
        private readonly DbSet<T> _dbSet;
        public Repository(DbContext context)
        {
            _context = context;
            _dbSet = _context.Set<T>();
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public T Get(int id)
        {
            return _dbSet.Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _dbSet;
        }

        public void Remove(T entity)
        {
            _dbSet.Remove(entity);
        }



        //public List<Student> GetAllStudents()
        //{
        //    return context.Students.ToList();
        //}
        //public List<StudentCard> GetAllStudentCards()
        //{
        //    return context.StudentCards.ToList();
        //}
    }
}
