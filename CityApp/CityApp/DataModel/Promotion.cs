namespace CityApp.DataModel
{
    public class Promotion
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public Promotion(string desription)
        {
            Description = desription;
        }

        public override string ToString()
        {
            return string.Format("{0}", Description);
        }
    }
}
