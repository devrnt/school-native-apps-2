using System;
using System.Collections.Generic;

namespace CityAppREST.Models
{
    public class OpeningHours
    {
        public int Id { get; set; }
        public List<Day> Opendagen { get; set; }
        public string Van { get; set; }
        public string Tot { get; set; }

        public OpeningHours(List<Day> opendagen, string van, string tot)
        {
            Opendagen = opendagen;
            Van = van;
            Tot = tot;
        }

        protected OpeningHours()
        {

        }
    }
}
