using SportsScorePredictor;
using Xunit;

public class PredictionTests
{
    [Fact]
    public void PredictWinner_ValidScores()
    {
        // Act
        var expectedOutput = ScorePredictor.PredictWinner("1001010101111011101111", 15);

        // Assert
        Assert.Equal("Team 1 won", expectedOutput);
    }
}