namespace CityApp.DataModel
{
    public class SocialMedia
    {
        public string Facebook { get; set; }
        public string Twitter { get; set; }
        public string YouTube { get; set; }
        public string Google { get; set; }

        public SocialMedia(string facebook, string twitter, string youTube, string google)
        {
            Facebook = facebook;
            Twitter = twitter;
            YouTube = youTube;
            Google = google;
        }
    }
}
