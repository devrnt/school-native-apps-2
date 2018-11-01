using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CityApp.DataModel;

namespace CityApp.ViewModels
{
    class CompanyDetailsViewModel
    {
        private Company c;

        public CompanyDetailsViewModel(Company c)
        {
            this.c = c;
        }
    }
}
