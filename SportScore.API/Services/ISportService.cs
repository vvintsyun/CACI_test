using SportScore.API.Dtos;

namespace SportScore.API.Services
{
    public interface ISportService
    {
        Task<string> CalculateAndAddAsync(NewSportDto input);
        Task DeleteByIdAsync(Guid id);
        Task<string?> GetMatchInfoByIdAsync(Guid id);
    }
}
