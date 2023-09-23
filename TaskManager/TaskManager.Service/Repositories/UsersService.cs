using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.Design;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TaskManager.DAL;
using TaskManager.DAL.Entity;
using TaskManager.DAL.Repositories;


namespace TaskManager.Service.Repositories
{
    public class UsersService
    {
        static TaskManagerContext _context = new TaskManagerContext();
        static Repository<User> _repository = new Repository<User>(_context);
        static UserProvider _provider = new UserProvider(_repository);
        private List<User> users => _provider.GetUsers().ToList();

        public bool Add(User user)
        {
            if(user!= null)
            {
                if (_provider.GetUsers().Any(_user => _user.login == user.login))
                {
                    return false;
                }
            }
            
            _provider.AddUser(user);
            return true; 
        }

        public bool CheckPassword(User user, string password)
        {
            if (user.password == password)
                return true;
            return false;
        }

        public bool IsAdmin(string username)
        {
            foreach (var user in users)
            {
                if (user.login == username)
                {
                    if (user.IsAdmin)
                        return true;
                }
            }
            return false;
        }

        public User GetUser(string login)
        {
            foreach (var user in users)
            {
                if (user.login == login)
                {
                    return user;
                }
            }
            return null;
        }
        public User GetUser(int id)
        {
            foreach (var user in users)
            {
                if (user.Id == id)
                {
                    return user;
                }
            }
            return null;
        }

        public int GetUserIdFromName(string userName)
        {
            foreach (var user in users)
            {
                if (user.Name == userName)
                {
                    return user.Id;
                }
            }
            return 0;
        }

        public void DeleteUser(int id)
        {
            _provider.RemoveUser(id);

        }
        public List<User> GetAllUsers()
        {
            return users.ToList();
        }

        public string GetUserName(int id)
        {
            return _provider.GetUser(id).Name;
        }

        public void Update(User user)
        {
            var existingUser = _context.Users.FirstOrDefault(t => t.Id == user.Id);

            if (existingUser != null)
            {
                existingUser.Name = user.Name;
                existingUser.login = user.login;
                existingUser.password = user.password;
                existingUser.IsAdmin = user.IsAdmin;
                existingUser.Ticket = user.Ticket;

                _context.SaveChanges();
            }
            else
            {
                throw new ArgumentException("User wasn't found!");
            }
        }

    }
}
