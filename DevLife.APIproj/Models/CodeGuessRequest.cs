namespace DevLife.APIproj.Models
{
    public class CodeGuessRequest
    {
       public string Username { get; set; } = string.Empty;

        public string Stack { get; set; } = string.Empty;

        public string SelectedCode { get; set; }= string.Empty;

        public int Bet { get; set; } 
    }
}
