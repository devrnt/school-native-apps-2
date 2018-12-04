using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CityApp.DataModel
{
    public class DiscountCoupon
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public string Pdf { get; set; }

        public DiscountCoupon(string couponCode, string pdf = "")
        {
            CouponCode = couponCode;
            Pdf = pdf;
        }

        // function to format the couponCode
        public string Print()
        {
            return $"De kortingscode {CouponCode}";
        }
    }
}
