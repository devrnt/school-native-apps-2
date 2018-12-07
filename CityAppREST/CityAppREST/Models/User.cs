using System;
namespace CityAppREST.Models
{
    public class User
    {
        public int Id { get; set; }
        public string Username { get; set; }

        public User(string username)
        {
            Username = username;
        }

        protected User()
        {
        }
    }
}
