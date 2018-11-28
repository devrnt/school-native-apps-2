namespace CityApp.DataModel
{
    public class Location
    {
        #region === Properties ===
        public string Country { get; set; }
        public string City { get; set; }
        public int Postal { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        #endregion

        #region === Constructor === 
        public Location(string country, string city, int postal, string street, int number)
        {
            Country = country;
            City = city;
            Postal = postal;
            Street = street;
            Number = number;
        }
        #endregion
    }
}
