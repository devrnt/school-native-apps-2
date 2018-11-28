using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    static class DummyDataSource
    {
        public static List<Company> Companies { get; set; } = new List<Company>()
        {
            new Company(1,
                "Swashy", "Hier kunt u terrecht met al uw vuile was. Op 15min. kunt u terug keren met een vers gewassen was. Was u er vorige keer niet bij, kom dan nu zeker langs met uw was.",
                new List<string>(){ "was", "laundry", "Swashy"},
                Categories.Wasserij,
                new Owner("van Gestel", "Gerry", new DateTime(1995, 3, 5), "GerryvanGestel@teleworm.be", "gerrygerry", UserType.Owner),
                new List<Location> () {new Location("Belgium", "Gent", 9000, "Grensstraat", 12, 51.076190, 3.710630) },
                null, null, null, null),
            new Company(2, "De Wasserte", "Beschrijving comp2", null, Categories.Wasserij, null, null, null, null, null, null),
            new Company(3, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null),
            new Company(4, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null),
            new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null)
        };
    }
}
