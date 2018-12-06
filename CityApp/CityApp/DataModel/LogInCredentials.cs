using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    public class LogInCredentials
    {
        public string u { get; set; }
        public string p { get; set; }
        public LogInCredentials() { }
        public LogInCredentials(string u, string p)
        {
            this.u = u;
            this.p = p;
        }
    }
}
