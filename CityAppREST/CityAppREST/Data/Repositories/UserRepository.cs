using System;
using System.Collections.Generic;
using System.Linq;
using CityAppREST.Models;
using Microsoft.EntityFrameworkCore;

namespace CityAppREST.Data.Repositories
{
    public class UserRepository : IRepository<User>
    {
        readonly ApplicationDbContext _applicationDbContext;
        readonly DbSet<User> _users;

        public UserRepository(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
            _users = _applicationDbContext.Users;
        }

        public User Create(User toCreate)
        {
            _users.Add(toCreate);
            SaveChanges();
            return toCreate;
        }

        public User Delete(int id)
        {
            User user = _users.FirstOrDefault(u => u.Id == id);
            if (user != null)
            {
                _users.Remove(user);
                SaveChanges();
            }
            return user;
        }

        public IEnumerable<User> GetAll()
        {
            return _users.ToList();
        }

        public User GetById(int id)
        {
            return _users.FirstOrDefault(u => u.Id == id);
        }

        public void SaveChanges()
        {
            _applicationDbContext.SaveChanges();
        }

        public User Update(User toUpdate)
        {
            User user = _users.FirstOrDefault(u => u.Id == toUpdate.Id);

            if (user != null)
            {
                user.Name = toUpdate.Name;
                SaveChanges();
            }

            return user;
        }
    }
}
