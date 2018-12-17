using System;

namespace CityAppREST.Models
{
    public class Event
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public string Image { get; set; }

        public Event(string title, string description, DateTime date, string image)
        {
            Title = title;
            Description = description;
            Date = date;
            Image = image;
        }

        public override string ToString()
        {
            return string.Format("{0}", Description);
        }
    }
}
