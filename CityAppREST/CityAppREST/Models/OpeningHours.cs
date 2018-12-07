using System;
using System.Collections.Generic;

namespace CityAppREST.Models
{
    public class OpeningHours
    {
        public int Id { get; set; }
        public Day Day { get; set; }
        public DateTime Van { get; set; }
        public DateTime Tot { get; set; }

        public OpeningHours(Day day, DateTime van, DateTime tot)
        {
            Day = day;
            Van = van;
            Tot = tot;
        }

        protected OpeningHours()
        {

        }
    }
}
