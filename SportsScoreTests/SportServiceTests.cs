using Microsoft.EntityFrameworkCore;
using SportScore.API.Data;
using SportScore.API.Models;
using SportScore.API.Services;
using SportsScorePredictor;
using Xunit;

public class SportServiceTests
{
    [Fact]
    public async Task SportServiceDelete_Correct()
    {
        await PrepareData();

        var service = new SportService(SportContext);

        Assert.NotNull(await service.GetMatchInfoByIdAsync(ExistingMatch.Id));
        await service.DeleteByIdAsync(ExistingMatch.Id);

        Assert.Null(await service.GetMatchInfoByIdAsync(ExistingMatch.Id));
    }

    [Fact]
    public async Task SportServiceDelete_NotExist()
    {
        var service = new SportService(SportContext);
        
        var ex = await Assert.ThrowsAsync<Exception>(async () => await service.DeleteByIdAsync(Guid.Empty));
        Assert.Equal("Match doesn't exist", ex.Message);
    }

    [Fact]
    public async Task SportServiceGetMatchInfo_Correct()
    {
        await PrepareData();

        var service = new SportService(SportContext);

        Assert.NotNull(await service.GetMatchInfoByIdAsync(ExistingMatch.Id));
        Assert.Null(await service.GetMatchInfoByIdAsync(Guid.Empty));
    }
    
    [Fact]
    public async Task SportServiceCalculateAndAdd_Correct()
    {
        var service = new SportService(SportContext);

        var scores = new List<string>
        {
            "1001010101111011101111",
            "1001010101111011101111",
        };
        var result = ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 15);

        var dto = new SportScore.API.Dtos.NewSportDto
        {
            Team1 = "Manchester",
            Team2 = "London",
            Scores = scores
        };
        var matchesCount = await SportContext.Matches.CountAsync();

        Assert.Equal(result.ToString(), await service.CalculateAndAddAsync(dto));
        Assert.Equal(matchesCount + 1, await SportContext.Matches.CountAsync());
    }

    [Fact]
    public async Task SportServiceCalculateAndAdd_EmptyScores()
    {
        var service = new SportService(SportContext);

        var dto = new SportScore.API.Dtos.NewSportDto
        {
            Team1 = "Manchester",
            Team2 = "London",
            Scores = new List<string>()
        };

        var ex = await Assert.ThrowsAsync<ArgumentException>(async () => await service.CalculateAndAddAsync(dto));
        Assert.Equal("Scores should not be empty", ex.Message);
    }


    private SportMatch ExistingMatch { get; set; }
    private SportContext SportContext { get; set; } = new SportContext();
    private async Task PrepareData()
    {
        var scores = new List<string>
            {
                "1001010101111011101111",
                "1001010101111011101111",
            };
        var matchResult = ScorePredictor.PredictMatchWinner(scores, "Team 1", "Team 2", 2, 15).ToString();
        ExistingMatch = new SportMatch("Team 1", "Team 2", matchResult);
        SportContext.Matches.Add(ExistingMatch);

        await SportContext.SaveChangesAsync();
    }
}