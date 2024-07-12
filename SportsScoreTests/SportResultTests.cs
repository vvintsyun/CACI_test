using SportsScorePredictor;
using Xunit;

public class SportResultTests
{
    [Fact]
    public void SportResult_Correct()
    {
        var expected = new SportResult(7, 15);

        var result = ScorePredictor.PredictSetWinner("1001010101111011101111", 15);

        Assert.Equal(result, expected);
    }
    
    [Fact]
    public void PredictSetWinner_InvalidScores()
    {
        var ex = Assert.Throws<ArgumentException>(() => ScorePredictor.PredictSetWinner("210", 1));
        Assert.Equal("Invalid score", ex.Message);
    }

    [Fact]
    public void PredictSetWinner_NoWinner()
    {
        var ex = Assert.Throws<Exception>(() => ScorePredictor.PredictSetWinner("1001010101111011101111", 50));
        Assert.Equal("Winner is not determined", ex.Message);
    }

    [Fact]
    public void SportResult_Tie()
    {
        var ex = Assert.Throws<ArgumentException>(() => new SportResult(3, 3));
        Assert.Equal("Tie is not allowed", ex.Message);
    }

    [Fact]
    public void SportResult_IncorrectPoints()
    {
        var ex = Assert.Throws<ArgumentException>(() => new SportResult(-1, 3));
        Assert.Equal("points values should be positive", ex.Message);
    }

    [Fact]
    public void SportResult_IncorrectPoints1()
    {
        var ex = Assert.Throws<ArgumentException>(() => new SportResult(3, -1));
        Assert.Equal("points values should be positive", ex.Message);
    }
}