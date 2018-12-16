using System;
using System.Collections.Generic;
using Windows.System;
using System.Linq;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    public class Owner : User
    {
        public ICollection<Company> Companies;
        private Task<string> task;

        #region Constructor
        public Owner(string name, string firstName, DateTime birthDate, String email, String password, UserType typeOfUser, ICollection<Company> companies) : base(name, firstName, birthDate, email, password, typeOfUser)
        {
            Companies = companies;
        }

        public Owner(string name, string firstName, DateTime birthDate, String email, String password, UserType typeOfUser) : base(name, firstName, birthDate, email, password, typeOfUser)
        {
        }

        public Owner(Task<string> task)
        {
            this.task = task;
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
