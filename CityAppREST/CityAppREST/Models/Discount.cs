namespace CityAppREST.Models
{
    public class Discount
    {
        public int Id { get; set; }
        public string CouponCode { get; set; }
        public string Pdf { get; set; }

        public Discount(string couponCode, string pdf = "")
        {
            CouponCode = couponCode;
            Pdf = pdf;
        }

        protected Discount()
        {
        }

        // function to format the couponCode
        public string Print()
        {
            return $"De kortingscode {CouponCode}";
        }

        public override string ToString()
        {
            return $"{CouponCode} [naam]";
        }
    }
}
