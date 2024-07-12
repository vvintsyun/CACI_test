using Microsoft.AspNetCore.Mvc;
using Moq;
using SportScore.API.Controllers;
using SportScore.API.Services;
using SportsScorePredictor;
using Xunit;

public class SportControllerTests
{
    [Fact]
    public async Task PostActionReturnsOkWithCorrectData()
    {
        var scores = new List<string>
        {
            "1001010101111011101111",
            "1001010101111011101111",
        };
        var result = ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 15);

        var mockService = new Mock<ISportService>();
        var dto = new SportScore.API.Dtos.NewSportDto
        {
            Team1 = "Manchester",
            Team2 = "London",
            Scores = scores
        };
        mockService.Setup(x => x.CalculateAndAddAsync(dto)).ReturnsAsync(result.ToString());
        var controller = new SportController(mockService.Object);

        var actionResult = await controller.Post(dto);

        var contentResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(result.ToString(), contentResult.Value);
    }

    [Fact]
    public async Task GetActionReturnsOkWithCorrectData()
    {
        var guid = Guid.NewGuid();
        var scores = new List<string>
        {
            "1001010101111011101111",
            "1001010101111011101111",
        };
        var result = ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 15);

        var mockService = new Mock<ISportService>();
        mockService.Setup(x => x.GetMatchInfoByIdAsync(guid)).ReturnsAsync(result.ToString());
        var controller = new SportController(mockService.Object);

        var actionResult = await controller.GetMatchInfo(guid);

        var contentResult = Assert.IsType<OkObjectResult>(actionResult);
        Assert.Equal(result.ToString(), contentResult.Value);
    }

    [Fact]
    public async Task GetActionReturnsNotFound()
    {
        var guid = Guid.NewGuid();
        var guid1 = Guid.NewGuid();
        var scores = new List<string>
        {
            "1001010101111011101111",
            "1001010101111011101111",
        };
        var result = ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 15);

        var mockService = new Mock<ISportService>();
        mockService.Setup(x => x.GetMatchInfoByIdAsync(guid)).ReturnsAsync(result.ToString());
        var controller = new SportController(mockService.Object);

        var actionResult = await controller.GetMatchInfo(guid1);

        Assert.IsType<NotFoundResult>(actionResult);
    }

    [Fact]
    public async Task DeleteActionReturnsOk()
    {
        var guid = Guid.NewGuid();

        var mockService = new Mock<ISportService>();
        mockService.Setup(x => x.DeleteByIdAsync(guid));
        var controller = new SportController(mockService.Object);

        var actionResult = await controller.Delete(guid);

        var contentResult = Assert.IsType<OkResult>(actionResult);
    }
}