using System;
using System.Collections.Generic;

namespace CityAppREST.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age => DateTime.Today.Year - BirthDate.Year;
        public string Email { get; set; }
        // Password should not be a String
        public string Password { get; set; }
        public IEnumerable<Company> Companies { get; set; }
        public UserType UserType { get; set; }

        public User(string name, string firstName, string username, DateTime birthDate, String email, String password, IEnumerable<Company> companies, UserType userType)
        {
            Name = name;
            FirstName = firstName;
            Username = username;
            BirthDate = birthDate;
            Email = email;
            Password = password;
            UserType = userType;
            Companies = companies;
        }

        protected User()
        {
        }
    }
}
