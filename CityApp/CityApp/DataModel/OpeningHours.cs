using System;
using System.Collections.Generic;

namespace CityApp.DataModel
{
    public class OpeningHours
    {
        public List<Days> Opendagen;
        public string Van;
        public string Tot;

        public OpeningHours(List<Days> opendagen, string van, string tot)
        {
            Opendagen = opendagen;
            Van = van;
            Tot = tot;
        }
    }
}
