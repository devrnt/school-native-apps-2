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

                #region Users
                var users = new List<User>
                {
                    new User("Jonas De Vrient"),
                    new User("Sam Dhondt"),
                    new User("Yanis Ouahab")
                };

                _applicationDbContext.Users.AddRange(users);
                _applicationDbContext.SaveChanges();
                #endregion


                #region Companies
                var companies = new List<Company>
                {
                    new Company(
                        "Swashy", "Hier kunt u terrecht met al uw vuile was. Op 15min. kunt u terug keren met een vers gewassen was. Was u er vorige keer niet bij, kom dan nu zeker langs met uw was.",
                        "was laundry Swashy",
                        Categories.Wasserij,
                        new List<Location> { new Location("Belgium", "Gent", 9000, "Grensstraat", 245) },
                        null, null, null, null),
                    new Company{ Name = "Bank van Gent", Description = "Voor al uw geldzaken", Categorie = Categories.Bank },
                    new Company{ Name = "Bakkerij Koen", Description = "Uw warme bakker", Categorie = Categories.Bakkerij }
                };

                _applicationDbContext.Companies.AddRange(companies);
                _applicationDbContext.SaveChanges();
                #endregion
            }
        }
    }
}
