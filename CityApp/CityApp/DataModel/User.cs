using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    public abstract class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age
        {
            get
            {
                return (DateTime.Today.Year - BirthDate.Year);
            }
        }
        public string Email { get; set; }
        // Password should not be a String
        public string Password { get; set; }
        public UserType TypeOfUser { get; set; }

        protected User(string name, string firstName, DateTime birthDate, String email, String password, UserType typeOfUser)
        {
            Name = name;
            FirstName = firstName;
            BirthDate = birthDate;
            Email = email;
            Password = password;
            TypeOfUser = typeOfUser;
            Username = name.ToLower() + firstName.ToLower();
        }

        protected User()
        {

        }
    }
}
