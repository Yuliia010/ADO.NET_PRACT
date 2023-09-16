using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pract_15092023.DAL.Repositories
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);

        void Remove(T entity);  

        IEnumerable<T> GetAll();

        T Get(int id);
    }
}
