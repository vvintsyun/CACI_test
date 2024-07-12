using System.ComponentModel.DataAnnotations;

namespace SportScore.API.Dtos
{
    public class NewSportDto
    {
        [Required]
        public string Team1 { get; set; }
        [Required]
        public string Team2 { get; set; }
        [Required]
        public IList<string> Scores { get; set; }
    }
}
