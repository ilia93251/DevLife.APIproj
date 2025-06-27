namespace DevLife.APIproj.Models
{
    public class BugChaseScore
    {
        public Guid Id { get; set; } = Guid.NewGuid();

        public string Username { get; set; } = string.Empty;

        public int Score { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;


    }
}
