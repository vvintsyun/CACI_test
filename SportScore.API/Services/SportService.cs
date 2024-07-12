using Microsoft.EntityFrameworkCore;
using SportScore.API.Data;
using SportScore.API.Dtos;
using SportScore.API.Models;
using SportsScorePredictor;

namespace SportScore.API.Services
{
    public class SportService : ISportService
    {
        private readonly SportContext _context;

        public SportService(SportContext context)
        {
            _context = context;
        }

        public async Task<string> CalculateAndAddAsync(NewSportDto input)
        {
            if (input.Scores.Count == 0)
            {
                throw new ArgumentException("Scores should not be empty");
            }
            var matchResult = ScorePredictor.PredictMatchWinner(input.Scores, input.Team1, input.Team2, 2, 15).ToString();

            var match = new SportMatch(input.Team1, input.Team2, matchResult);

            _context.Add(match);
            await _context.SaveChangesAsync();

            return matchResult;
        }

        public async Task DeleteByIdAsync(Guid id)
        {
            var match = await _context.Matches
                .FirstOrDefaultAsync(x => x.Id == id);

            if (match is null)
            {
                throw new Exception("Match doesn't exist");
            }

            _context.Remove(match);
            await _context.SaveChangesAsync();
        }

        public async Task<string?> GetMatchInfoByIdAsync(Guid id)
        {
            var match = await _context.Matches
                .FirstOrDefaultAsync(x => x.Id == id);

            return match?.Info;
        }
    }
}
