namespace DevLife.APIproj.Models
{
    public class User
    {
        public int Id { get; set; }

        public string Username { get; set; } = string.Empty;

        public string Firstname { get; set; } = string.Empty;

        public string Lastname { get; set; } = string.Empty;

        public DateTime BirthDate { get; set; }

        public string Level { get; set; } = string.Empty;

        public string Stack { get; set; } = string.Empty;

        public string Zodiac { get; set; } = string.Empty;

       public int Coins { get; set; } = 10;
    }
}
