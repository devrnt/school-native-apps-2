using System;
using System.Collections.Generic;

namespace CityAppREST.Models
{
    public class Owner : User
    {
        public ICollection<Company> Companies;

        #region Constructor
        public Owner(string name, string firstName, DateTime birthDate, String email, String password, UserType typeOfUser, ICollection<Company> companies)
        {
            Companies = companies;
        }

        public Owner(string name, string firstName, DateTime birthDate, String email, String password, UserType typeOfUser)
        {
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
