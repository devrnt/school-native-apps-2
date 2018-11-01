using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    class DummyDataSource
    {
        public static List<Company> Companies { get; set; } = new List<Company>()
        {
            new Company(1, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null),
            new Company(2, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null),
            new Company(3, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null),
            new Company(4, "comp2", "Beschrijving comp2", null, Categories.Bank, null, null, null, null, null, null),
            new Company(5, "comp1", "Beschrijving comp1", null, Categories.Bank, null, null, null, null, null, null)
        };
    }
}
