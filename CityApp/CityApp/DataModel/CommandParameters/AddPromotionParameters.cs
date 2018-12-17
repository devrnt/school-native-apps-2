using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel.CommandParameters
{
    class AddPromotionParameters
    {
        public string Description { get; set; }
        public Discount Discount { get; set; }
    }
}
