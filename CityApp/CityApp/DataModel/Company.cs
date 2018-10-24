using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    // can be shops, cafes, restaurants, schools...
    public class Company
    {
        #region Fields and Properties
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> KeyWords { get; set; }
        // Use Category enum
        public Categories Categorie { get; set; }
        public Owner Owner { get; set; }
        public ICollection<Location> Locations { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public LeaveOfAbsence LeaveOfAbsence { get; set; }
        public SocialMedia SocialMedia { get; set; }
        public ICollection<Promotion> Promotions { get; set; }
        public bool HasPromotions
        {
            get
            {
                return Promotions.Count != 0;
            }
        }
        #endregion
    }
}
