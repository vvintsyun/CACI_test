using SportsScorePredictor;
using Xunit;

public class PredictMatchWinnerTests
{
    [Fact]
    public void PredictMatchWinner_ValidScores()
    {        
        var scores = new List<string>
        {
            "1001010101111011101111",
            "1001010101111011101111",
        };
        var results = scores.Select(x => ScorePredictor.PredictSetWinner(x, 15)).ToList();
        var expected = new SportResult(0, 2, "Manchester", "London", results);

        var result = ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 15);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void PredictMatchWinner_ValidScoresLose()
    {
        var scores = new List<string>
        {
            "0000000000111100000",
            "0000000000111100000",
        };
        var results = scores.Select(x => ScorePredictor.PredictSetWinner(x, 15)).ToList();
        var expected = new SportResult(2, 0, "Manchester", "London", results);

        var result = ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 15);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void PredictMatchWinner_ValidScores21()
    {
        var scores = new List<string>
        {
            "10010101011110110",
            "100000000001",
            "10010101011110110",
        };
        var results = scores.Select(x => ScorePredictor.PredictSetWinner(x, 10)).ToList();
        var expected = new SportResult(1, 2, "Manchester", "London", results);

        var result = ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 10);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void PredictMatchWinner_ValidScores20()
    {
        var scores = new List<string>
        {
            "10010101011110110",
            "10010101011110110",
            "100000000001",
        };
        var results = scores.Take(2).Select(x => ScorePredictor.PredictSetWinner(x, 10)).ToList();
        var expected = new SportResult(0, 2, "Manchester", "London", results);

        var result = ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 10);

        Assert.Equal(result, expected);
    }

    [Fact]
    public void PredictMatchWinner_EmptyName()
    {
        Assert.Throws<ArgumentNullException>("team0", () => ScorePredictor.PredictMatchWinner(new List<string>(), string.Empty, "Team 1", 1, 1));
    }

    [Fact]
    public void PredictMatchWinner_EmptyName1()
    {
        Assert.Throws<ArgumentNullException>("team1", () => ScorePredictor.PredictMatchWinner(new List<string>(), "Team 0", string.Empty, 1, 1));
    }

    [Fact]
    public void PredictMatchWinner_IncorrectPointsToWin()
    {
        var ex = Assert.Throws<ArgumentException>(() => ScorePredictor.PredictMatchWinner(new List<string>(), "Team 0", "Team 1", 0, 1));
        Assert.Equal("pointsToWin should be positive", ex.Message);
    }

    [Fact]
    public void PredictMatchWinner_IncorrectPointsToWinSet()
    {
        var ex = Assert.Throws<ArgumentException>(() => ScorePredictor.PredictMatchWinner(new List<string>(), "Team 0", "Team 1", 1, 0));
        Assert.Equal("pointsToWinSet should be positive", ex.Message);
    }

    [Fact]
    public void PredictMatchWinner_NoWinner()
    {
        var scores = new List<string>
        {
            "10010101011110110",
            "10010101011110110",
            "100000000001",
        };

        var ex = Assert.Throws<Exception>(() => ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 3, 10));
        Assert.Equal("Winner is not determined", ex.Message);
    }


    [Fact]
    public void PredictMatchWinner_Tie()
    {
        var scores = new List<string>
        {
            "10010101011110110",
            "100000000001",
        };

        var ex = Assert.Throws<Exception>(() => ScorePredictor.PredictMatchWinner(scores, "Manchester", "London", 2, 10));
        Assert.Equal("Winner is not determined", ex.Message);
    }
}