using System;
using System.Collections.Generic;
using CityAppREST.Models;

namespace CityAppREST.Data
{
    public class CityAppDataInitializer
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public CityAppDataInitializer(ApplicationDbContext applicationDbContext)
        {
            _applicationDbContext = applicationDbContext;
        }

        public void InitializeData()
        {
            _applicationDbContext.Database.EnsureDeleted();

            if (_applicationDbContext.Database.EnsureCreated())
            {
                var users = new List<User>
                {
                    new User("Jonas De Vrient"),
                    new User("Sam Dhondt"),
                    new User("Yanis Ouahab")
                };

                _applicationDbContext.Users.AddRange(users);
                _applicationDbContext.SaveChanges();
            }
        }
    }
}
