using System;
using System.Collections.Generic;
using Windows.System;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CityApp.DataModel
{
    public class Owner : User
    {
        public ICollection<Company> Companies;
        private Task<string> task;

        #region Constructor
        public Owner(string name, string firstName, DateTime birthDate, String email, String password, UserType userType, ICollection<Company> companies) : base(name, firstName, birthDate, email, password, userType)
        {
            Companies = companies;
        }

        public Owner(string name, string firstName, DateTime birthDate, String email, String password, UserType userType) : base(name, firstName, birthDate, email, password, userType)
        {
        }

        public Owner(Task<string> task)
        {
            this.task = task;
        }

        [JsonConstructor]
        public Owner(int id)
        {
            Id = id;
        }
        #endregion

        #region Methods
        public void AddCompany(Company company)
        {
            Companies.Add(company);
        }
        #endregion
    }
}
