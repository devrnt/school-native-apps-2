using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    public class Visitor : User
    {
        public ICollection<string> Subscriptions { get; set; }
        public Visitor(string name, string firstName, DateTime birthDate, String email, String password, UserType typeOfUser, List<string> subscriptions) : base(name, firstName, birthDate, email, password, typeOfUser)
        {
            Subscriptions = subscriptions;
        }

        public Visitor():base()
        {
            Subscriptions = new List<string>();
        }
    }
}
