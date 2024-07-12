namespace SportsScorePredictor
{
    public static class ScorePredictor
    {
        public static SportResult PredictSetWinner(string score, int pointsToWin)
        {
            ValidatePredictSetWinner(score, pointsToWin);
            int[] count = new int[2];
            int i;
            for (i = 0; i < score.Length; i++)
            {
                // increase count
                count[score[i] - '0']++;

                // check winning condition
                if (count[0] == pointsToWin && count[1] < pointsToWin - 1 || count[1] == pointsToWin && count[0] < pointsToWin - 1)
                {
                    return new SportResult(count[0], count[1]);
                }

                if (count[0] == pointsToWin - 1 && count[1] == pointsToWin - 1)
                {
                    break;
                }
            }

            for (i++; i < score.Length; i++)
            {
                // increase count
                count[score[i] - '0']++;

                // check for 2 point lead
                if (Math.Abs(count[0] - count[1]) == 2)
                {
                    return new SportResult(count[0], count[1]);
                }
            }

            //not enough points to win or tie
            throw new Exception("Winner is not determined");
        }

        public static SportResult PredictMatchWinner(IList<string> scores, string team0, string team1, int pointsToWin, int pointsToWinSet)
        {
            ValidatePredictMatchWinner(team0, team1, pointsToWin, pointsToWinSet);
            var count = new int[2];
            var sets = new List<SportResult>();

            for (var i = 0; i < scores.Count; i++)
            {
                var setResult = PredictSetWinner(scores[i], pointsToWinSet);
                if (setResult.PointsTeam0 > setResult.PointsTeam1)
                {
                    count[0]++;
                } 
                else
                {
                    count[1]++;
                }
                sets.Add(setResult);
                if (count[0] == pointsToWin || count[1] == pointsToWin)
                {
                    break;
                }
            }

            if (count[0] == pointsToWin || count[1] == pointsToWin)
            {
                return new SportResult(count[0], count[1], team0, team1, sets);
            }

            throw new Exception("Winner is not determined");
        }

        private static void ValidatePredictMatchWinner(string team0, string team1, int pointsToWin, int pointsToWinSet)
        {
            if (string.IsNullOrEmpty(team0)) throw new ArgumentNullException(nameof(team0));
            if (string.IsNullOrEmpty(team1)) throw new ArgumentNullException(nameof(team1));

            if (pointsToWin <= 0) throw new ArgumentException("pointsToWin should be positive");
            if (pointsToWinSet <= 0) throw new ArgumentException("pointsToWinSet should be positive");
        }

        private static void ValidatePredictSetWinner(string score, int pointsToWin)
        {
            if (pointsToWin <= 0) throw new ArgumentException("pointsToWin should be positive");
            foreach(var s in score)
            {
                if (s != '0' && s != '1') throw new ArgumentException("Invalid score");
            }
        }
    }
}