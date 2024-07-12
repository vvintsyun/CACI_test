using System.Collections.ObjectModel;
using System.Text;

namespace SportsScorePredictor
{
    public class SportResult : IEquatable<SportResult>
    {
        public string Team0 { get; private set; }
        public string Team1 { get; private set; }
        public int PointsTeam0 { get; private set; }
        public int PointsTeam1 { get; private set; }

        public IReadOnlyList<SportResult>? Items { get; private set; }

        public SportResult(int pointsTeam0, int pointsTeam1, string team0, string team1, IList<SportResult> items)
        {
            ValidatePoints(pointsTeam0, pointsTeam1);

            PointsTeam0 = pointsTeam0;
            PointsTeam1 = pointsTeam1;
            Team0 = team0;
            Team1 = team1;

            Items = new ReadOnlyCollection<SportResult>(items);
        }

        public SportResult(int pointsTeam0, int pointsTeam1)
        {
            ValidatePoints(pointsTeam0, pointsTeam1);

            PointsTeam0 = pointsTeam0;
            PointsTeam1 = pointsTeam1;
            Team0 = "Team 0";
            Team1 = "Team 1";

            Items = null;
        }

        public override string? ToString()
        {
            if (Items is null)
            {
                return PointsTeam0 > PointsTeam1
                    ? $"{Team0} won"
                    : $"{Team0} lost";
            }            

            var result = PointsTeam0 > PointsTeam1
                ? "won"
                : "lost";
            var stringBuilder = new StringBuilder();
            foreach (var item in Items)
            {
                stringBuilder.Append($"{item.PointsTeam0} - {item.PointsTeam1}, ");
            }

            return $"{Team0} {result} {Team1}({PointsTeam0} - {PointsTeam1}) Scores: {stringBuilder.ToString(0, stringBuilder.Length - 2)}";
        }

        private void ValidatePoints(int pointsTeam0, int pointsTeam1)
        {
            if (pointsTeam0 < 0 || pointsTeam1 < 0) throw new ArgumentException("points values should be positive");

            if (pointsTeam0 == pointsTeam1)
            {
                throw new ArgumentException("Tie is not allowed");
            }
        }

        public bool Equals(SportResult? other)
        {
            if (other is null) return false;
            if (other.Items?.Count != Items?.Count) return false;
            for (int i = 0; i < Items?.Count; i++)
            {
                if (!Items[i].Equals(other.Items[i])) return false;
            }
            return Team0 == other?.Team0 && Team1 == other?.Team1 && PointsTeam0 == other.PointsTeam0 && PointsTeam1 == other.PointsTeam1;
        }
    }
}
