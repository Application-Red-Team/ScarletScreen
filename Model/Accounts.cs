namespace ScarletScreen.Model
{
    public class Accounts
    {
        public int Id { get; set; }
        public string user { get; set; }
        public string pass { get; set; }
        public string email { get; set; }
        public string Bio { get; set; } = string.Empty;
        public string ProfilePicturePath { get; set; } = "/images/default.png";
        public string tmdbuserid { get; set; } = string.Empty;


    }
}
