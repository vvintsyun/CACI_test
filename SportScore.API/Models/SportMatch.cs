namespace SportScore.API.Models
{
    public class SportMatch
    {
        public Guid Id { get; set; }
        public string Team1 { get; set; }
        public string Team2 { get; set; }
        public string Info { get; set; }

        public SportMatch(string team1, string team2, string info)
        {
            Team1 = team1;
            Team2 = team2;
            Info = info;
        }
    }
}
