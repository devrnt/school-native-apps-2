using System;
using System.Collections;
using System.Collections.Generic;

namespace CityAppREST.Models
{
    public interface IUserRepository
    {
        User GetById(int id);
        IEnumerable<User> GetAll();
        void Create(User user);
        void Update(User user);
        void Delete(int id);
    }
}
