using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    public class RegisterCredentials
    {
        public string Name { get; set; }
        public string FirstName { get; set; }
        public DateTimeOffset BirthDate { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public Boolean? Type { get; set; }

        public RegisterCredentials(){}
    }
}
