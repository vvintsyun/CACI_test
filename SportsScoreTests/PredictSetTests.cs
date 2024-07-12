using SportsScorePredictor;
using Xunit;

public class PredictSetTests
{
    [Fact]
    public void PredictSetWinner_ValidScores()
    {
        var expected = new SportResult(7, 15);

        var result = ScorePredictor.PredictSetWinner("1001010101111011101111", 15);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void PredictSetWinner_ValidScoresLose()
    {
        var expected = new SportResult(15, 4);

        var result = ScorePredictor.PredictSetWinner("0101000000000011000", 15);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void PredictSetWinner_ValidScoresAdditionalRounds()
    {
        var expected = new SportResult(17, 15);

        var result = ScorePredictor.PredictSetWinner("01010101010101010101010101010100", 15);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void PredictSetWinner_InvalidScores()
    {
        var ex = Assert.Throws<ArgumentException>(() => ScorePredictor.PredictSetWinner("210", 1));
        Assert.Equal("Invalid score", ex.Message);
    }

    [Fact]
    public void PredictSetWinner_IncorrectPointsToWin()
    {
        var ex = Assert.Throws<ArgumentException>(() => ScorePredictor.PredictSetWinner("0110", -1));
        Assert.Equal("pointsToWin should be positive", ex.Message);
    }

    [Fact]
    public void PredictSetWinner_NoWinner()
    {
        var ex = Assert.Throws<Exception>(() => ScorePredictor.PredictSetWinner("1001010101111011101111", 50));
        Assert.Equal("Winner is not determined", ex.Message);
    }

    [Fact]
    public void PredictSetWinner_Tie()
    {
        var ex = Assert.Throws<Exception>(() => ScorePredictor.PredictSetWinner("010101010101010101010101010101010101", 10));
        Assert.Equal("Winner is not determined", ex.Message);
    }
}