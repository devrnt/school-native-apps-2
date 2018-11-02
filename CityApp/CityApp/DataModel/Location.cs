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
        public double Latitude { get; set; }
        public double Longitude { get; set; }
        #endregion

        #region === Constructor === 
        public Location(string country, string city, int postal, string street, int number, double latitude, double longitude)
        {
            Country = country;
            City = city;
            Postal = postal;
            Street = street;
            Number = number;
            Latitude = latitude;
            Longitude = longitude;
        }
        #endregion
    }
}
