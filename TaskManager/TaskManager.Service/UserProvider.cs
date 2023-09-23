using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;

namespace TaskManager.Service
{
    internal class UserProvider
    {
        private readonly IRepository<User> _userRepository;


        internal UserProvider(IRepository<User> repository)
        {
            _userRepository = repository;
        }

        internal void AddUsers(List<User> users)
        {
            foreach (var user in users)
            {
                AddUser(user);
            }
        }

        internal void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        internal User GetUser(int id)
        {
            return _userRepository.Get(id);
        }

        internal IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        internal void RemoveUser(int id)
        {
            var user = GetUser(id);
            _userRepository.Remove(user);
        }
    }
}
