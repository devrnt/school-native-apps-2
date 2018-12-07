using System.Collections.Generic;

namespace CityAppREST.Models
{
    // can be shops, cafes, restaurants, schools...
    public class Company
    {
        #region === Fields and Properties === 
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string KeyWords { get; set; }
        // Use Category enum
        public Categories Categorie { get; set; }
        //public Owner Owner { get; set; }
        public ICollection<Location> Locations { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public LeaveOfAbsence LeaveOfAbsence { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
        public ICollection<Discount> Discounts { get; set; }
        public bool HasPromotion => Promotions == null || Promotions.Count != 0;
        #endregion

        #region === Constructor ===
        public Company(string name, string description, string keyWords, Categories categorie, ICollection<Location> locations, OpeningHours openingHours, LeaveOfAbsence leaveOfAbsence, SocialMedia socialMedia, ICollection<Promotion> promotions)
        {

            Name = name;
            Description = description;
            KeyWords = keyWords;
            Categorie = categorie;
            //Owner = owner;
            Locations = locations;
            OpeningHours = openingHours;
            LeaveOfAbsence = leaveOfAbsence;
            SocialMedia = socialMedia;
            Promotions = promotions;
            Discounts = new List<Discount>();
        }

        public Company()
        {
            Locations = new List<Location>();
            Promotions = new List<Promotion>();
            Discounts = new List<Discount>();
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
