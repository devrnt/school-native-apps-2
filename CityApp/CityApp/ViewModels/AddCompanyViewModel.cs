using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;

namespace CityApp.ViewModels
{

    public class AddCompanyViewModel
    {
        public List<Categories> AllCategories
        {
            get
            {
                List<Categories> cats = new List<Categories>();
                foreach (Categories cat in Enum.GetValues(typeof(Categories)))
                {
                    cats.Add(cat);
                }
                return cats;
            }
        }
        public AddCompanyViewModel() { }
        public void CreateCompany(string name, string description, string keyWordsString, Categories categorie, Owner owner, List<Location> locations, List<OpeningHours> openingHours, string leaveOfAbsence, SocialMedia socialMedia)
        {
            int id = 0;
            List<Promotion> promotions = null;
            Company co = new Company(id, name, description, keyWordsString, categorie, owner, locations, openingHours, null, socialMedia, promotions);
            DummyDataSource.Companies.Add(co);
        }
    }
}
