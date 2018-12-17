using System.Collections.Generic;

namespace CityApp.DataModel
{
    // can be shops, cafes, restaurants, schools...
    public class Company
    {
        private string keyWordsString;
        private object p;
        #region === Fields and Properties === 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        // Use Category enum
        public Categories Categorie { get; set; }
        public Owner Owner { get; set; }
        public IEnumerable<OpeningHours> OpeningHours { get; set; }
        public LeaveOfAbsence LeaveOfAbsence { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
        public ICollection<Location> Locations { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public ICollection<Event> Events { get; set; }
        public bool HasPromotion
        {
            get
            {
                return Promotions == null ? false : Promotions.Count == 0 ? false : true;
            }
        }
        #endregion

        #region === Constructor ===
        public Company(int id, string name, string description, string keyWords, Categories categorie, Owner owner, ICollection<Location> locations, IEnumerable<OpeningHours> openingHours, LeaveOfAbsence leaveOfAbsence, SocialMedia socialMedia, ICollection<Promotion> promotions)
        {
            Id = id;
            Name = name;
            Description = description;
            KeyWords = keyWords;
            Categorie = categorie;
            Owner = owner;
            Locations = locations;
            OpeningHours = openingHours;
            LeaveOfAbsence = leaveOfAbsence;
            SocialMedia = socialMedia;
            Promotions = promotions;
            Discounts = new List<Discount>();
        }

        public Company(){}

        // Used to send the post request to the server
        public Company(string name, string description, string keyWordsString, Categories categorie, Owner owner, ICollection<Location> locations, IEnumerable<OpeningHours> openingHours, object p, SocialMedia socialMedia, List<Promotion> promotions)
        {
            Name = name;
            Description = description;
            this.keyWordsString = keyWordsString;
            Categorie = categorie;
            Owner = owner;
            Locations = locations;
            OpeningHours = openingHours;
            this.p = p;
            SocialMedia = socialMedia;
            Promotions = promotions;
        }
        #endregion

        #region === Methods === 
        public override string ToString()
        {
            return string.Format("{0}", Name);
        }
        #endregion
    }
}
