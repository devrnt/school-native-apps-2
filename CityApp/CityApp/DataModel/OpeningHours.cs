using System;
using System.Collections.Generic;

namespace CityApp.DataModel
{
    public class OpeningHours
    {
        public Days Day;
        public string Van;
        public string Tot;

        public OpeningHours(Days days, string van, string tot)
        {
            Day = days;
            Van = van;
            Tot = tot;
        }
        public override string ToString()
        {
            return string.Format("Van: {0} - Tot: {1} [{2}]", Van, Tot, Day);
        }
    }
}
