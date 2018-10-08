using System;
using System.Collections.Generic;
using Windows.System;
using System.Linq;

namespace CityApp.DataModel {
	public class Owner : User {
		public ICollection<Company> Companies;

		#region Constructor
		public Owner(string name, string firstName, DateTime birthDate, String email, String password, UserType typeOfUser, ICollection<Company> companies):base(name, firstName, birthDate ,email,password,typeOfUser) {
			Companies = companies;
		}
		#endregion

		#region Methods
		public void AddCompany(Company company) {
			Companies.Add(company);
		}
        #endregion
	}
}