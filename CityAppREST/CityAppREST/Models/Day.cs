using System;
namespace CityAppREST.Models
{
    /// <summary>
    /// Class used to map the Days enum for Entity Framework
    /// </summary>
    public class Day
    {
        public Days DayOfWeek { get; set; }

        protected Day()
        {

        }

        public Day(Days dayOfWeek)
        {
            DayOfWeek = dayOfWeek;
        }
    }
}
