using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;

namespace TaskManager.Service
{
    public class UserProvider
    {
        private readonly IRepository<User> _userRepository;

        
        public UserProvider(IRepository<User> repository)
        {
            _userRepository = repository;
        }
        
        public void AddUsers(List<User> users)
        {
            foreach (var user in users)
            {
                AddUser(user);
            }
        }

        public void AddUser(User user)
        {
            _userRepository.Add(user);
        }

        public User GetUser(int id)
        {
            return _userRepository.Get(id);
        }

        public IEnumerable<User> GetUsers()
        {
            return _userRepository.GetAll();
        }

        public void RemoveUser(int id)
        {
            var user = GetUser(id);
            _userRepository.Remove(user);
        }
    }
}
