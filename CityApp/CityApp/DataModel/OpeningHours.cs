using System;
using System.Collections.Generic;

namespace CityApp.DataModel
{
    public class OpeningHours
    {
        public List<Days> Days;
        public string Van;
        public string Tot;

        public OpeningHours(List<Days> days, string van, string tot)
        {
            Days = days;
            Van = van;
            Tot = tot;
        }
    }
}
