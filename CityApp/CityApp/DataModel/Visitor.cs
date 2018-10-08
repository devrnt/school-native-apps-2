using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel {
	public class Visitor : User{
		public Visitor(string name, string firstName, DateTime birthDate, String email, String password, UserType typeOfUser) : base(name, firstName, birthDate, email, password, typeOfUser) {

		}
	}
}
