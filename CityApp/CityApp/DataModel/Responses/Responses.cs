
using System;
using System.Collections.Generic;
/**
* This namespace contains all responses that are returned by the REST-api client
* */
namespace CityApp.DataModel.Responses
{
    public class LoginResponse
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string Token { get; set; }
    }

    public class UserResponse
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string Username { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public long Age { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public List<Company> Companies { get; set; }
        public long UserType { get; set; }
    }
}
