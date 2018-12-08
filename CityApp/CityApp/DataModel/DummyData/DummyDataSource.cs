using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    internal static class DummyDataSource
    {
        public static List<Company> Companies { get; set; } = InitializeData();

        private static List<Company> InitializeData()
        {
            var company = new Company(1,
                "Swashy", "Hier kunt u terrecht met al uw vuile was. Op 15min. kunt u terug keren met een vers gewassen was. Was u er vorige keer niet bij, kom dan nu zeker langs met uw was.",
                "was, laundry, Swashy" ,
                Categories.Wasserij,
                new Owner("van Gestel", "Gerry", new DateTime(1995, 3, 5), "GerryvanGestel@teleworm.be", "gerrygerry", UserType.Owner),
                new List<Location>() { new Location("Belgium", "Gent", 9000, "Grensstraat", 245) },
                null, null, null, null);

            company.Promotions = new List<Promotion>() { new Promotion("Dit is een promotie!"), new Promotion("Nog een promotie zeg") };

            return new List<Company>() {
                company,
                new Company(2, "De Wasserte", "Beschrijving comp2", null, Categories.Wasserij, null, null, null, null, null, new List<Promotion>()),
                new Company(3, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, new List<Promotion>()),
                new Company(4, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, new List<Promotion>()),
                new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, new List<Promotion>())
            };

        }
        private static List<User> DummyUsers()
        {
            return new List<User>() {
                new Visitor("bez", "jef", new DateTime(), "user1Email", "123", UserType.Visitor, new List<Company>()),
                new Owner("owner", "mike", new DateTime(), "MikeMail", "321", UserType.Owner)
            };
        }
    }
}
