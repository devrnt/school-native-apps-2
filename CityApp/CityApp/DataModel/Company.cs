using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    public class Company
    {
        #region Fields and Properties

        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public ICollection<string> KeyWords { get; set; }
        // Use Category model
        public ICollection<string> Categories { get; set; }
        public Owner Owner { get; set; }
        public Location Location { get; set; }
        public OpeningHours OpeningHours { get; set; }
        public LeaveOfAbsence leaveOfAbsence { get; set; }
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
